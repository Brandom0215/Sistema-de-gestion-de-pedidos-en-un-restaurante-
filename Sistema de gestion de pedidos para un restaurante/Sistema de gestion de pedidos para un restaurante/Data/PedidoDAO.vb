Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports Npgsql

Namespace Data
    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) centralizado para la gestión de pedidos en PostgreSQL.
    ''' PostgreSQL es la ÚNICA fuente de verdad.
    ''' </summary>
    Public Module PedidoDAO

        ' Suscripciones de eventos para reactividad en la UI local
        Private ReadOnly _listenersPedidoRegistrado As New List(Of Action(Of Integer))()
        Private ReadOnly _listenersPedidoModificado As New List(Of Action(Of Integer))()

        Public Sub SuscribirPedidoRegistrado(callback As Action(Of Integer))
            If callback IsNot Nothing AndAlso Not _listenersPedidoRegistrado.Contains(callback) Then
                _listenersPedidoRegistrado.Add(callback)
            End If
        End Sub

        Public Sub DesuscribirPedidoRegistrado(callback As Action(Of Integer))
            _listenersPedidoRegistrado.Remove(callback)
        End Sub

        Public Sub SuscribirPedidoModificado(callback As Action(Of Integer))
            If callback IsNot Nothing AndAlso Not _listenersPedidoModificado.Contains(callback) Then
                _listenersPedidoModificado.Add(callback)
            End If
        End Sub

        Public Sub DesuscribirPedidoModificado(callback As Action(Of Integer))
            _listenersPedidoModificado.Remove(callback)
        End Sub

        Private Sub DispararPedidoRegistrado(id As Integer)
            For Each cb In _listenersPedidoRegistrado.ToArray()
                Try
                    cb.Invoke(id)
                Catch
                End Try
            Next
        End Sub

        Private Sub DispararPedidoModificado(id As Integer)
            For Each cb In _listenersPedidoModificado.ToArray()
                Try
                    cb.Invoke(id)
                Catch
                End Try
            Next
        End Sub


        Private Function NormalizarTipoServicio(servicio As String) As String
            If String.IsNullOrWhiteSpace(servicio) Then Return "En Mesa"
            If servicio.IndexOf("Llevar", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "Para Llevar"
            If servicio.IndexOf("Delivery", StringComparison.OrdinalIgnoreCase) >= 0 OrElse servicio.IndexOf("domicilio", StringComparison.OrdinalIgnoreCase) >= 0 Then Return "Delivery"
            Return "En Mesa"
        End Function

        ''' <summary>
        ''' Extrae el precio numérico del texto del plato.
        ''' </summary>
        Public Function ExtraerPrecioPlato(plato As String) As Decimal
            If String.IsNullOrWhiteSpace(plato) Then Return 15.0D
            Try
                Dim match = Regex.Match(plato, "\$\s*([0-9]+(?:\.[0-9]{1,2})?)")
                If match.Success Then
                    Dim valorStr = match.Groups(1).Value
                    Dim resultado As Decimal
                    If Decimal.TryParse(valorStr, NumberStyles.Any, CultureInfo.InvariantCulture, resultado) Then
                        Return resultado
                    End If
                End If
            Catch ex As Exception
            End Try
            Return 15.0D
        End Function

        ''' <summary>
        ''' <summary>
        ''' Obtiene la tabla completa de pedidos directamente desde PostgreSQL.
        ''' </summary>
        ''' </summary>
        Public Function ObtenerTodos() As DataTable
            Dim sql As String =
                "SELECT " &
                "  p.id_pedido AS ""ID"", " &
                "  p.nombre_cliente AS ""Cliente"", " &
                "  p.mesa_o_servicio AS ""Mesa"", " &
                "  COALESCE((SELECT STRING_AGG(COALESCE(dp.nombre_plato, pl.nombre_plato, 'Plato del Menú'), ' + ') FROM detalle_pedidos dp LEFT JOIN platos pl ON dp.id_plato = pl.id_plato WHERE dp.id_pedido = p.id_pedido), 'Plato del Menú') AS ""PlatoPrincipal"", " &
                "  COALESCE((SELECT STRING_AGG(dp.acompanamientos, ', ') FROM detalle_pedidos dp WHERE dp.id_pedido = p.id_pedido AND dp.acompanamientos IS NOT NULL AND dp.acompanamientos <> ''), 'Sin notas adicionales') AS ""Acompanamientos"", " &
                "  p.tipo_servicio AS ""TipoServicio"", " &
                "  TO_CHAR(p.fecha_hora, 'YYYY-MM-DD HH24:MI:SS') AS ""FechaHora"", " &
                "  COALESCE(TO_CHAR(p.fecha_cobro, 'YYYY-MM-DD HH24:MI:SS'), '') AS ""FechaCobro"", " &
                "  p.total AS ""PrecioUnitario"", " &
                "  ROUND(p.total / 1.07, 2) AS ""Subtotal"", " &
                "  ROUND(p.total - ROUND(p.total / 1.07, 2), 2) AS ""Impuesto"", " &
                "  p.total AS ""Total"", " &
                "  UPPER(p.estado) AS ""Estado"", " &
                "  COALESCE(p.estado_cocina, 'RECIBIDO') AS ""EstadoCocina"", " &
                "  COALESCE(p.metodo_pago, 'Efectivo') AS ""MetodoPago"", " &
                "  COALESCE(p.monto_recibido, 0.00) AS ""MontoRecibido"", " &
                "  COALESCE(p.cambio, 0.00) AS ""Cambio"", " &
                "  COALESCE(p.facturado, (UPPER(p.estado) = 'PAGADO')) AS ""Facturado"", " &
                "  COALESCE(p.numero_factura, '') AS ""NumeroFactura"", " &
                "  COALESCE(p.ruc_cedula, '8-800-1234') AS ""RUC_Cedula"", " &
                "  COALESCE(p.razon_social, p.nombre_cliente) AS ""RazonSocial"", " &
                "  COALESCE(p.direccion_fiscal, 'Ciudad de Panamá') AS ""DireccionFiscal"", " &
                "  COALESCE(p.telefono_cliente, '+507 6200-1122') AS ""TelefonoCliente"", " &
                "  COALESCE(p.correo_cliente, 'cliente@restaurante.com') AS ""CorreoCliente"" " &
                "FROM pedidos p " &
                "ORDER BY p.id_pedido DESC;"

            Return ConexionBD.EjecutarConsultaDataTable(sql)
        End Function

        ''' <summary>
        ''' Obtiene la tabla completa de detalles de pedidos desde PostgreSQL.
        ''' </summary>
        Public Function ObtenerDetallesTodos() As DataTable
            Dim sql As String =
                "SELECT " &
                "  dp.id_detalle AS ""ID"", " &
                "  dp.id_pedido AS ""IdPedido"", " &
                "  COALESCE(dp.nombre_plato, pl.nombre_plato, 'Plato del Menú') AS ""Plato"", " &
                "  dp.cantidad AS ""Cantidad"", " &
                "  dp.precio_unitario AS ""PrecioUnitario"", " &
                "  dp.subtotal AS ""Subtotal"", " &
                "  COALESCE(dp.acompanamientos, '') AS ""Acompanamientos"" " &
                "FROM detalle_pedidos dp " &
                "LEFT JOIN platos pl ON dp.id_plato = pl.id_plato " &
                "ORDER BY dp.id_detalle ASC;"

            Return ConexionBD.EjecutarConsultaDataTable(sql)
        End Function

        ''' <summary>
        ''' Obtiene únicamente los pedidos con estado PENDIENTE desde PostgreSQL.
        ''' </summary>
        Public Function ObtenerPedidosPendientes() As DataTable
            Dim sql As String =
                "SELECT " &
                "  p.id_pedido AS ""ID"", " &
                "  p.nombre_cliente AS ""Cliente"", " &
                "  p.mesa_o_servicio AS ""Mesa"", " &
                "  COALESCE((SELECT STRING_AGG(COALESCE(dp.nombre_plato, pl.nombre_plato, 'Plato del Menú'), ' + ') FROM detalle_pedidos dp LEFT JOIN platos pl ON dp.id_plato = pl.id_plato WHERE dp.id_pedido = p.id_pedido), 'Plato del Menú') AS ""PlatoPrincipal"", " &
                "  COALESCE((SELECT STRING_AGG(dp.acompanamientos, ', ') FROM detalle_pedidos dp WHERE dp.id_pedido = p.id_pedido AND dp.acompanamientos IS NOT NULL AND dp.acompanamientos <> ''), 'Sin notas adicionales') AS ""Acompanamientos"", " &
                "  p.tipo_servicio AS ""TipoServicio"", " &
                "  TO_CHAR(p.fecha_hora, 'YYYY-MM-DD HH24:MI:SS') AS ""FechaHora"", " &
                "  '' AS ""FechaCobro"", " &
                "  p.total AS ""PrecioUnitario"", " &
                "  ROUND(p.total / 1.07, 2) AS ""Subtotal"", " &
                "  ROUND(p.total - ROUND(p.total / 1.07, 2), 2) AS ""Impuesto"", " &
                "  p.total AS ""Total"", " &
                "  UPPER(p.estado) AS ""Estado"", " &
                "  COALESCE(p.estado_cocina, 'RECIBIDO') AS ""EstadoCocina"", " &
                "  COALESCE(p.metodo_pago, 'Efectivo') AS ""MetodoPago"", " &
                "  COALESCE(p.monto_recibido, 0.00) AS ""MontoRecibido"", " &
                "  COALESCE(p.cambio, 0.00) AS ""Cambio"", " &
                "  FALSE AS ""Facturado"", " &
                "  '' AS ""NumeroFactura"", " &
                "  '' AS ""RUC_Cedula"", " &
                "  '' AS ""RazonSocial"", " &
                "  '' AS ""DireccionFiscal"", " &
                "  '' AS ""TelefonoCliente"", " &
                "  '' AS ""CorreoCliente"" " &
                "FROM pedidos p " &
                "WHERE UPPER(p.estado) = 'PENDIENTE' " &
                "ORDER BY p.id_pedido ASC;"

            Return ConexionBD.EjecutarConsultaDataTable(sql)
        End Function

        ''' <summary>
        ''' Obtiene pedidos cobrados/pagados desde PostgreSQL.
        ''' </summary>
        Public Function ObtenerPedidosPagados() As DataTable
            Dim sql As String =
                "SELECT " &
                "  p.id_pedido AS ""ID"", " &
                "  p.nombre_cliente AS ""Cliente"", " &
                "  p.mesa_o_servicio AS ""Mesa"", " &
                "  COALESCE((SELECT STRING_AGG(COALESCE(dp.nombre_plato, pl.nombre_plato, 'Plato del Menú'), ' + ') FROM detalle_pedidos dp LEFT JOIN platos pl ON dp.id_plato = pl.id_plato WHERE dp.id_pedido = p.id_pedido), 'Plato del Menú') AS ""PlatoPrincipal"", " &
                "  COALESCE((SELECT STRING_AGG(dp.acompanamientos, ', ') FROM detalle_pedidos dp WHERE dp.id_pedido = p.id_pedido AND dp.acompanamientos IS NOT NULL AND dp.acompanamientos <> ''), 'Sin notas adicionales') AS ""Acompanamientos"", " &
                "  p.tipo_servicio AS ""TipoServicio"", " &
                "  TO_CHAR(p.fecha_hora, 'YYYY-MM-DD HH24:MI:SS') AS ""FechaHora"", " &
                "  COALESCE(TO_CHAR(p.fecha_cobro, 'YYYY-MM-DD HH24:MI:SS'), TO_CHAR(p.fecha_hora, 'YYYY-MM-DD HH24:MI:SS')) AS ""FechaCobro"", " &
                "  p.total AS ""PrecioUnitario"", " &
                "  ROUND(p.total / 1.07, 2) AS ""Subtotal"", " &
                "  ROUND(p.total - ROUND(p.total / 1.07, 2), 2) AS ""Impuesto"", " &
                "  p.total AS ""Total"", " &
                "  'PAGADO' AS ""Estado"", " &
                "  COALESCE(p.estado_cocina, 'RECIBIDO') AS ""EstadoCocina"", " &
                "  COALESCE(p.metodo_pago, 'Efectivo') AS ""MetodoPago"", " &
                "  COALESCE(p.monto_recibido, p.total) AS ""MontoRecibido"", " &
                "  COALESCE(p.cambio, 0.00) AS ""Cambio"", " &
                "  COALESCE(p.facturado, TRUE) AS ""Facturado"", " &
                "  COALESCE(p.numero_factura, CONCAT('FAC-2026-', (1000 + p.id_pedido)::text)) AS ""NumeroFactura"", " &
                "  COALESCE(p.ruc_cedula, '8-800-1234') AS ""RUC_Cedula"", " &
                "  COALESCE(p.razon_social, p.nombre_cliente) AS ""RazonSocial"", " &
                "  COALESCE(p.direccion_fiscal, 'Ciudad de Panamá') AS ""DireccionFiscal"", " &
                "  COALESCE(p.telefono_cliente, '+507 6200-1122') AS ""TelefonoCliente"", " &
                "  COALESCE(p.correo_cliente, 'cliente@restaurante.com') AS ""CorreoCliente"" " &
                "FROM pedidos p " &
                "WHERE UPPER(p.estado) = 'PAGADO' " &
                "ORDER BY p.id_pedido DESC;"

            Return ConexionBD.EjecutarConsultaDataTable(sql)
        End Function

        ''' <summary>
        ''' Busca un pedido por su identificador único en PostgreSQL.
        ''' </summary>
        Public Function ObtenerPedidoPorId(id As Integer) As DataRow
            Dim sql As String =
                "SELECT " &
                "  p.id_pedido AS ""ID"", " &
                "  p.nombre_cliente AS ""Cliente"", " &
                "  p.mesa_o_servicio AS ""Mesa"", " &
                "  COALESCE((SELECT STRING_AGG(COALESCE(dp.nombre_plato, pl.nombre_plato, 'Plato del Menú'), ' + ') FROM detalle_pedidos dp LEFT JOIN platos pl ON dp.id_plato = pl.id_plato WHERE dp.id_pedido = p.id_pedido), 'Plato del Menú') AS ""PlatoPrincipal"", " &
                "  COALESCE((SELECT STRING_AGG(dp.acompanamientos, ', ') FROM detalle_pedidos dp WHERE dp.id_pedido = p.id_pedido AND dp.acompanamientos IS NOT NULL AND dp.acompanamientos <> ''), 'Sin notas adicionales') AS ""Acompanamientos"", " &
                "  p.tipo_servicio AS ""TipoServicio"", " &
                "  TO_CHAR(p.fecha_hora, 'YYYY-MM-DD HH24:MI:SS') AS ""FechaHora"", " &
                "  COALESCE(TO_CHAR(p.fecha_cobro, 'YYYY-MM-DD HH24:MI:SS'), TO_CHAR(p.fecha_hora, 'YYYY-MM-DD HH24:MI:SS')) AS ""FechaCobro"", " &
                "  p.total AS ""PrecioUnitario"", " &
                "  ROUND(p.total / 1.07, 2) AS ""Subtotal"", " &
                "  ROUND(p.total - ROUND(p.total / 1.07, 2), 2) AS ""Impuesto"", " &
                "  p.total AS ""Total"", " &
                "  UPPER(p.estado) AS ""Estado"", " &
                "  COALESCE(p.estado_cocina, 'RECIBIDO') AS ""EstadoCocina"", " &
                "  COALESCE(p.metodo_pago, 'Efectivo') AS ""MetodoPago"", " &
                "  COALESCE(p.monto_recibido, p.total) AS ""MontoRecibido"", " &
                "  COALESCE(p.cambio, 0.00) AS ""Cambio"", " &
                "  COALESCE(p.facturado, (UPPER(p.estado) = 'PAGADO')) AS ""Facturado"", " &
                "  COALESCE(p.numero_factura, CONCAT('FAC-2026-', (1000 + p.id_pedido)::text)) AS ""NumeroFactura"", " &
                "  COALESCE(p.ruc_cedula, '8-800-1234') AS ""RUC_Cedula"", " &
                "  COALESCE(p.razon_social, p.nombre_cliente) AS ""RazonSocial"", " &
                "  COALESCE(p.direccion_fiscal, 'Ciudad de Panamá') AS ""DireccionFiscal"", " &
                "  COALESCE(p.telefono_cliente, '+507 6200-1122') AS ""TelefonoCliente"", " &
                "  COALESCE(p.correo_cliente, 'cliente@restaurante.com') AS ""CorreoCliente"" " &
                "FROM pedidos p " &
                "WHERE p.id_pedido = @id LIMIT 1;"

            Dim pId As New NpgsqlParameter("@id", id)
            Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, pId)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Registra un pedido completo en PostgreSQL usando una única transacción relacional.
        ''' </summary>
        Public Function GuardarPedidoCompleto(cliente As String,
                                              mesa As String,
                                              servicio As String,
                                              total As Decimal,
                                              tablaCarrito As DataTable,
                                              listaExtras As List(Of Tuple(Of String, Integer, Decimal)),
                                              Optional estadoPago As String = "PENDIENTE",
                                              Optional metodoPago As String = "",
                                              Optional estadoCocina As String = "RECIBIDO",
                                              Optional correoCliente As String = "") As Integer

            Dim strCliente As String = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
            Dim strMesa As String = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
            Dim strServicio As String = NormalizarTipoServicio(servicio)
            Dim strCorreo As String = If(String.IsNullOrWhiteSpace(correoCliente), "cliente@restaurante.com", correoCliente.Trim())
            Dim strEstado As String = If(estadoPago.Equals("PAGADO", StringComparison.OrdinalIgnoreCase), "Pagado", "Pendiente")
            Dim strEstadoCocina As String = If(String.IsNullOrWhiteSpace(estadoCocina), "RECIBIDO", estadoCocina.Trim().ToUpper())
            Dim strMetodo As String = If(String.IsNullOrWhiteSpace(metodoPago), "Efectivo", metodoPago.Trim())

            Using conn As NpgsqlConnection = ConexionBD.CrearConexion()
                conn.Open()
                Using trans As NpgsqlTransaction = conn.BeginTransaction()
                    Try
                        Dim sqlCabecera As String =
                            "INSERT INTO pedidos (nombre_cliente, mesa_o_servicio, tipo_servicio, estado, estado_cocina, total, monto_recibido, cambio, metodo_pago, correo_cliente) " &
                            "VALUES (@cliente, @mesa, @servicio, @estado, @estadoCocina, @total, @monto, @cambio, @metodo, @correo) RETURNING id_pedido;"

                        Dim idNuevo As Integer = 0
                        Using cmdCab As New NpgsqlCommand(sqlCabecera, conn, trans)
                            cmdCab.Parameters.AddWithValue("@cliente", strCliente)
                            cmdCab.Parameters.AddWithValue("@mesa", strMesa)
                            cmdCab.Parameters.AddWithValue("@servicio", strServicio)
                            cmdCab.Parameters.AddWithValue("@estado", strEstado)
                            cmdCab.Parameters.AddWithValue("@estadoCocina", strEstadoCocina)
                            cmdCab.Parameters.AddWithValue("@total", total)
                            cmdCab.Parameters.AddWithValue("@monto", If(strEstado = "Pagado", total, 0D))
                            cmdCab.Parameters.AddWithValue("@cambio", 0D)
                            cmdCab.Parameters.AddWithValue("@metodo", strMetodo)
                            cmdCab.Parameters.AddWithValue("@correo", strCorreo)

                            Dim res = cmdCab.ExecuteScalar()
                            idNuevo = Convert.ToInt32(res)
                        End Using

                        Dim sqlDetalle As String =
                            "INSERT INTO detalle_pedidos (id_pedido, id_plato, nombre_plato, cantidad, precio_unitario, acompanamientos) " &
                            "VALUES (@idPed, (SELECT id_plato FROM platos WHERE LOWER(nombre_plato) = LOWER(@plato) LIMIT 1), @plato, @cant, @precio, @acomp);"

                        ' 1. Líneas de carrito
                        If tablaCarrito IsNot Nothing AndAlso tablaCarrito.Rows.Count > 0 Then
                            For Each r As DataRow In tablaCarrito.Rows
                                Dim strPlato As String = r("Plato").ToString().Trim()
                                Dim intCant As Integer = Convert.ToInt32(r("Cant"))
                                Dim decPrecio As Decimal = Convert.ToDecimal(r("Precio"))

                                Using cmdDet As New NpgsqlCommand(sqlDetalle, conn, trans)
                                    cmdDet.Parameters.AddWithValue("@idPed", idNuevo)
                                    cmdDet.Parameters.AddWithValue("@plato", strPlato)
                                    cmdDet.Parameters.AddWithValue("@cant", intCant)
                                    cmdDet.Parameters.AddWithValue("@precio", decPrecio)
                                    cmdDet.Parameters.AddWithValue("@acomp", "")
                                    cmdDet.ExecuteNonQuery()
                                End Using
                            Next
                        End If

                        ' 2. Líneas de extras / bebidas
                        If listaExtras IsNot Nothing AndAlso listaExtras.Count > 0 Then
                            For Each t In listaExtras
                                Dim strExtraNombre As String = t.Item1.Trim()
                                Dim intExtraCant As Integer = t.Item2
                                Dim decExtraPrecio As Decimal = t.Item3

                                Using cmdDet As New NpgsqlCommand(sqlDetalle, conn, trans)
                                    cmdDet.Parameters.AddWithValue("@idPed", idNuevo)
                                    cmdDet.Parameters.AddWithValue("@plato", strExtraNombre)
                                    cmdDet.Parameters.AddWithValue("@cant", intExtraCant)
                                    cmdDet.Parameters.AddWithValue("@precio", decExtraPrecio)
                                    cmdDet.Parameters.AddWithValue("@acomp", "Bebida / Acompañamiento")
                                    cmdDet.ExecuteNonQuery()
                                End Using
                            Next
                        End If

                        trans.Commit()
                        DispararPedidoRegistrado(idNuevo)
                        Return idNuevo
                    Catch ex As Exception
                        trans.Rollback()
                        ConexionBD.RegistrarFalloServidor(ex.Message)
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Sobrecarga de compatibilidad para guardar un pedido simple en PostgreSQL.
        ''' </summary>
        Public Function Guardar(cliente As String, mesa As String, plato As String, acomp As String, servicio As String,
                                Optional total As Decimal = 0D,
                                Optional estadoPago As String = "PENDIENTE",
                                Optional metodoPago As String = "",
                                Optional estadoCocina As String = "RECIBIDO") As Boolean

            Dim strCliente As String = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
            Dim strMesa As String = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
            Dim strPlato As String = If(String.IsNullOrWhiteSpace(plato), "Plato del Menú", plato.Trim())
            Dim strAcomp As String = If(String.IsNullOrWhiteSpace(acomp), "Sin acompañamiento", acomp.Trim())
            Dim strServicio As String = NormalizarTipoServicio(servicio)
            Dim precioFinal As Decimal = If(total > 0D, total, ExtraerPrecioPlato(strPlato))
            Dim strEstado As String = If(estadoPago.Equals("PAGADO", StringComparison.OrdinalIgnoreCase), "Pagado", "Pendiente")
            Dim strEstadoCocina As String = If(String.IsNullOrWhiteSpace(estadoCocina), "RECIBIDO", estadoCocina.Trim().ToUpper())
            Dim strMetodo As String = If(String.IsNullOrWhiteSpace(metodoPago), "Efectivo", metodoPago.Trim())

            Using conn As NpgsqlConnection = ConexionBD.CrearConexion()
                conn.Open()
                Using trans As NpgsqlTransaction = conn.BeginTransaction()
                    Try
                        Dim sqlCabecera As String =
                            "INSERT INTO pedidos (nombre_cliente, mesa_o_servicio, tipo_servicio, estado, estado_cocina, total, monto_recibido, cambio, metodo_pago) " &
                            "VALUES (@cliente, @mesa, @servicio, @estado, @estadoCocina, @total, @monto, @cambio, @metodo) RETURNING id_pedido;"

                        Dim idNuevo As Integer = 0
                        Using cmdCab As New NpgsqlCommand(sqlCabecera, conn, trans)
                            cmdCab.Parameters.AddWithValue("@cliente", strCliente)
                            cmdCab.Parameters.AddWithValue("@mesa", strMesa)
                            cmdCab.Parameters.AddWithValue("@servicio", strServicio)
                            cmdCab.Parameters.AddWithValue("@estado", strEstado)
                            cmdCab.Parameters.AddWithValue("@estadoCocina", strEstadoCocina)
                            cmdCab.Parameters.AddWithValue("@total", precioFinal)
                            cmdCab.Parameters.AddWithValue("@monto", If(strEstado = "Pagado", precioFinal, 0D))
                            cmdCab.Parameters.AddWithValue("@cambio", 0D)
                            cmdCab.Parameters.AddWithValue("@metodo", strMetodo)
                            idNuevo = Convert.ToInt32(cmdCab.ExecuteScalar())
                        End Using

                        Dim sqlDetalle As String =
                            "INSERT INTO detalle_pedidos (id_pedido, id_plato, nombre_plato, cantidad, precio_unitario, acompanamientos) " &
                            "VALUES (@idPed, (SELECT id_plato FROM platos WHERE LOWER(nombre_plato) = LOWER(@plato) LIMIT 1), @plato, 1, @precio, @acomp);"

                        Using cmdDet As New NpgsqlCommand(sqlDetalle, conn, trans)
                            cmdDet.Parameters.AddWithValue("@idPed", idNuevo)
                            cmdDet.Parameters.AddWithValue("@plato", strPlato)
                            cmdDet.Parameters.AddWithValue("@precio", precioFinal)
                            cmdDet.Parameters.AddWithValue("@acomp", strAcomp)
                            cmdDet.ExecuteNonQuery()
                        End Using

                        trans.Commit()
                        DispararPedidoRegistrado(idNuevo)
                        Return True
                    Catch ex As Exception
                        trans.Rollback()
                        ConexionBD.RegistrarFalloServidor(ex.Message)
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Actualiza un pedido en la base de datos PostgreSQL.
        ''' </summary>
        Public Function Actualizar(id As Integer, cliente As String, mesa As String, plato As String, acomp As String, servicio As String) As Boolean
            Dim strCliente As String = cliente.Trim()
            Dim strMesa As String = mesa.Trim()
            Dim strPlato As String = plato.Trim()
            Dim strAcomp As String = acomp.Trim()
            Dim strServicio As String = NormalizarTipoServicio(servicio)
            Dim precioFinal As Decimal = ExtraerPrecioPlato(strPlato)

            Using conn As NpgsqlConnection = ConexionBD.CrearConexion()
                conn.Open()
                Using trans As NpgsqlTransaction = conn.BeginTransaction()
                    Try
                        Dim sqlCab As String =
                            "UPDATE pedidos SET nombre_cliente = @c, mesa_o_servicio = @m, tipo_servicio = @s, total = @total WHERE id_pedido = @id;"
                        Using cmdCab As New NpgsqlCommand(sqlCab, conn, trans)
                            cmdCab.Parameters.AddWithValue("@c", strCliente)
                            cmdCab.Parameters.AddWithValue("@m", strMesa)
                            cmdCab.Parameters.AddWithValue("@s", strServicio)
                            cmdCab.Parameters.AddWithValue("@total", precioFinal)
                            cmdCab.Parameters.AddWithValue("@id", id)
                            cmdCab.ExecuteNonQuery()
                        End Using

                        ' Actualizar detalles mediante DELETE + INSERT
                        Dim sqlDel As String = "DELETE FROM detalle_pedidos WHERE id_pedido = @id;"
                        Using cmdDel As New NpgsqlCommand(sqlDel, conn, trans)
                            cmdDel.Parameters.AddWithValue("@id", id)
                            cmdDel.ExecuteNonQuery()
                        End Using

                        Dim sqlIns As String =
                            "INSERT INTO detalle_pedidos (id_pedido, id_plato, nombre_plato, cantidad, precio_unitario, acompanamientos) " &
                            "VALUES (@id, (SELECT id_plato FROM platos WHERE LOWER(nombre_plato) = LOWER(@plato) LIMIT 1), @plato, 1, @precio, @acomp);"
                        Using cmdIns As New NpgsqlCommand(sqlIns, conn, trans)
                            cmdIns.Parameters.AddWithValue("@id", id)
                            cmdIns.Parameters.AddWithValue("@plato", strPlato)
                            cmdIns.Parameters.AddWithValue("@precio", precioFinal)
                            cmdIns.Parameters.AddWithValue("@acomp", strAcomp)
                            cmdIns.ExecuteNonQuery()
                        End Using

                        trans.Commit()
                        DispararPedidoModificado(id)
                        Return True
                    Catch ex As Exception
                        trans.Rollback()
                        ConexionBD.RegistrarFalloServidor(ex.Message)
                        Throw
                    End Try
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Elimina un pedido por ID en la base de datos PostgreSQL.
        ''' </summary>
        Public Function Eliminar(id As Integer) As Boolean
            Try
                Dim pId As New NpgsqlParameter("@id", id)
                ConexionBD.EjecutarComando("DELETE FROM detalle_pedidos WHERE id_pedido = @id;", pId)
                Dim pId2 As New NpgsqlParameter("@id", id)
                Dim filas = ConexionBD.EjecutarComando("DELETE FROM pedidos WHERE id_pedido = @id;", pId2)
                If filas > 0 Then
                    DispararPedidoModificado(id)
                    Return True
                End If
                Return False
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        ''' <summary>
        ''' <summary>
        ''' Confirma el pago de un pedido en Caja actualizando directamente PostgreSQL.
        ''' </summary>
        ''' </summary>
        Public Function ConfirmarCobro(id As Integer, metodoPago As String, montoRecibido As Decimal, cambio As Decimal) As Boolean
            Try
                Dim sql As String = "UPDATE pedidos SET estado = 'Pagado', metodo_pago = @metodo, monto_recibido = @monto, cambio = @cambio, fecha_cobro = CURRENT_TIMESTAMP WHERE id_pedido = @id;"
                Dim p1 As New NpgsqlParameter("@metodo", metodoPago.Trim())
                Dim p2 As New NpgsqlParameter("@monto", montoRecibido)
                Dim p3 As New NpgsqlParameter("@cambio", cambio)
                Dim p4 As New NpgsqlParameter("@id", id)
                Dim filas = ConexionBD.EjecutarComando(sql, p1, p2, p3, p4)

                If filas > 0 Then
                    DispararPedidoModificado(id)
                    DispararPedidoRegistrado(id)
                    Return True
                End If
                Return False
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Registra los datos fiscales y genera el correlativo usando la secuencia seq_factura de PostgreSQL.
        ''' </summary>
        Public Function RegistrarFactura(id As Integer, rucCedula As String, razonSocial As String, direccion As String, telefono As String, correo As String) As String
            Try
                ' 1. Si ya está facturado en la BD, retornar el número existente
                Dim dtExistente = ConexionBD.EjecutarConsultaDataTable("SELECT facturado, numero_factura FROM pedidos WHERE id_pedido = @id;", New NpgsqlParameter("@id", id))
                If dtExistente IsNot Nothing AndAlso dtExistente.Rows.Count > 0 Then
                    Dim row = dtExistente.Rows(0)
                    If row("facturado") IsNot DBNull.Value AndAlso CBool(row("facturado")) AndAlso Not String.IsNullOrWhiteSpace(row("numero_factura").ToString()) Then
                        Return row("numero_factura").ToString()
                    End If
                End If

                ' 2. Obtener el siguiente valor de la secuencia seq_factura
                Dim numSeq As Long = 1001
                Dim dtSeq = ConexionBD.EjecutarConsultaDataTable("SELECT nextval('seq_factura');")
                If dtSeq IsNot Nothing AndAlso dtSeq.Rows.Count > 0 Then
                    numSeq = Convert.ToInt64(dtSeq.Rows(0)(0))
                End If

                Dim correlativo As String = $"FAC-2026-{numSeq}"

                Dim sql As String = "UPDATE pedidos SET facturado = TRUE, numero_factura = @num, ruc_cedula = @ruc, razon_social = @razon, direccion_fiscal = @dir, telefono_cliente = @tel, correo_cliente = @correo WHERE id_pedido = @id;"
                Dim p1 As New NpgsqlParameter("@num", correlativo)
                Dim p2 As New NpgsqlParameter("@ruc", rucCedula.Trim())
                Dim p3 As New NpgsqlParameter("@razon", razonSocial.Trim())
                Dim p4 As New NpgsqlParameter("@dir", direccion.Trim())
                Dim p5 As New NpgsqlParameter("@tel", telefono.Trim())
                Dim p6 As New NpgsqlParameter("@correo", correo.Trim())
                Dim p7 As New NpgsqlParameter("@id", id)

                ConexionBD.EjecutarComando(sql, p1, p2, p3, p4, p5, p6, p7)

                DispararPedidoModificado(id)
                Return correlativo
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Obtiene el próximo correlativo fiscal estimado consultando el estado actual de seq_factura sin consumirla.
        ''' </summary>
        Public Function ObtenerProximoNumeroFactura() As String
            Try
                Dim sql As String = "SELECT COALESCE((SELECT last_value FROM seq_factura), 1000) + 1;"
                Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim proxVal = Convert.ToInt64(dt.Rows(0)(0))
                    Return $"FAC-2026-{proxVal}"
                End If
            Catch ex As Exception
            End Try
            Return "FAC-2026-1001"
        End Function

        ''' <summary>
        ''' Actualiza el estado de cocina en PostgreSQL con validación de regla de negocio.
        ''' </summary>
        Public Function ActualizarEstadoCocina(id As Integer, nuevoEstadoCocina As String) As Boolean
            Dim estadoCocinaNormalizado As String = nuevoEstadoCocina.Trim().ToUpper()

            Try
                Dim sql As String =
                    "UPDATE pedidos " &
                    "SET estado_cocina = @estadoCocina " &
                    "WHERE id_pedido = @id " &
                    "  AND (@estadoCocina <> 'ENTREGADO' OR UPPER(estado) = 'PAGADO');"

                Dim pEstado As New NpgsqlParameter("@estadoCocina", estadoCocinaNormalizado)
                Dim pId As New NpgsqlParameter("@id", id)

                Dim filas = ConexionBD.EjecutarComando(sql, pEstado, pId)
                If filas > 0 Then
                    DispararPedidoModificado(id)
                    Return True
                End If
                Return False
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Obtiene las comandas activas de cocina consultando directamente PostgreSQL.
        ''' </summary>
        Public Function ObtenerComandasCocina() As List(Of Models.CcnPedidoModel)
            Dim lista As New List(Of Models.CcnPedidoModel)()

            Dim sqlCabecera As String =
                "SELECT " &
                "  p.id_pedido, " &
                "  p.nombre_cliente, " &
                "  p.mesa_o_servicio, " &
                "  p.tipo_servicio, " &
                "  UPPER(p.estado) AS estado_pago, " &
                "  COALESCE(p.estado_cocina, 'RECIBIDO') AS estado_cocina, " &
                "  COALESCE(p.metodo_pago, 'Efectivo') AS metodo_pago, " &
                "  p.fecha_hora, " &
                "  COALESCE(p.facturado, FALSE) AS facturado, " &
                "  COALESCE(p.numero_factura, '') AS numero_factura " &
                "FROM pedidos p " &
                "ORDER BY p.id_pedido ASC;"

            Dim dtCab = ConexionBD.EjecutarConsultaDataTable(sqlCabecera)
            If dtCab Is Nothing OrElse dtCab.Rows.Count = 0 Then Return lista

            Dim dtDet = ObtenerDetallesTodos()

            For Each row As DataRow In dtCab.Rows
                Dim id As Integer = Convert.ToInt32(row("id_pedido"))
                Dim strEstadoPago As String = row("estado_pago").ToString()
                Dim strMetodo As String = row("metodo_pago").ToString()
                Dim strCodigo As String = $"#08-{1040 + id}"
                Dim strMesa As String = row("mesa_o_servicio").ToString()
                Dim strCliente As String = row("nombre_cliente").ToString()
                Dim strServicio As String = row("tipo_servicio").ToString()
                Dim blnPagado As Boolean = (strEstadoPago = "PAGADO")
                Dim blnFacturado As Boolean = CBool(row("facturado"))
                Dim strNumFactura As String = row("numero_factura").ToString()

                Dim strEstadoCocina As String = row("estado_cocina").ToString().ToUpper()
                Dim enumEstado As Models.CcnEstadoPedidoEnum = Models.CcnEstadoPedidoEnum.Recibido
                Select Case strEstadoCocina
                    Case "EN_PREPARACION", "ENPREPARACION"
                        enumEstado = Models.CcnEstadoPedidoEnum.EnPreparacion
                    Case "LISTO"
                        enumEstado = Models.CcnEstadoPedidoEnum.Listo
                    Case "ENTREGADO", "DESPACHADO"
                        enumEstado = Models.CcnEstadoPedidoEnum.Entregado
                    Case Else
                        enumEstado = Models.CcnEstadoPedidoEnum.Recibido
                End Select

                Dim dtHora As DateTime = DateTime.Now
                If row("fecha_hora") IsNot DBNull.Value Then
                    DateTime.TryParse(row("fecha_hora").ToString(), dtHora)
                End If

                Dim objComanda As New Models.CcnPedidoModel(
                    id, strCodigo, strMesa, "Mozo General", strCliente, strServicio,
                    strMetodo, blnPagado, dtHora, enumEstado
                )
                objComanda.BlnFacturado = blnFacturado
                objComanda.StrNumeroFactura = strNumFactura

                ' Cargar ítems detallados de este pedido
                If dtDet IsNot Nothing Then
                    Dim rowsDetalle = dtDet.Select($"IdPedido = {id}")
                    For Each r In rowsDetalle
                        Dim cant = Convert.ToInt32(r("Cantidad"))
                        Dim plato = r("Plato").ToString()
                        Dim precioUnit = Convert.ToDecimal(r("PrecioUnitario"))
                        Dim acomp = r("Acompanamientos").ToString()
                        Dim celiaco = (plato.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0 OrElse plato.IndexOf("SIN GLUTEN", StringComparison.OrdinalIgnoreCase) >= 0)

                        objComanda.LstDetallePlatos.Add(New Models.CcnItemPedidoModel(cant, plato, acomp, precioUnit, celiaco, If(celiaco, "CELÍACO: Estrictamente Sin Gluten", "")))
                    Next
                End If

                lista.Add(objComanda)
            Next

            Return lista
        End Function

    End Module
End Namespace
