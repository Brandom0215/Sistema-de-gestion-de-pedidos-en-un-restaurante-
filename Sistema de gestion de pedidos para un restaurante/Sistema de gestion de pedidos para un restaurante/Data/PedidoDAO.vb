Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports Npgsql

Namespace Data
    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) en memoria centralizado para la gestión de pedidos,
    ''' comanda de cocina KDS bajo la Ley de FIFO, cobros en caja y facturación electrónica digital.
    ''' Mantiene sincronizadas la cabecera (pedidos) y el detalle de ítems/menú (detalle_pedidos),
    ''' reflejando el esquema de base de datos relacional.
    ''' </summary>
    Public Module PedidoDAO

        Private ReadOnly _tablaPedidos As DataTable
        Private ReadOnly _tablaDetallePedidos As DataTable
        Private _ultimoId As Integer = 0
        Private _ultimoIdDetalle As Integer = 0
        Private _contadorFacturas As Integer = 1000

        ' Suscripciones de eventos para reactividad en tiempo real (KDS, Caja, Facturación)
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

#Region "RESPALDO_LOCAL_CONTINGENCIA (Fácil de remover cuando el servidor UTP esté siempre disponible)"
        Sub New()
            ' 1. Tabla Cabecera de Pedidos (equivalente a 'pedidos')
            _tablaPedidos = New DataTable("Pedidos")
            _tablaPedidos.Columns.Add("ID", GetType(Integer))
            _tablaPedidos.Columns.Add("Cliente", GetType(String))
            _tablaPedidos.Columns.Add("Mesa", GetType(String))
            _tablaPedidos.Columns.Add("PlatoPrincipal", GetType(String))
            _tablaPedidos.Columns.Add("Acompanamientos", GetType(String))
            _tablaPedidos.Columns.Add("TipoServicio", GetType(String))
            _tablaPedidos.Columns.Add("FechaHora", GetType(String))
            _tablaPedidos.Columns.Add("FechaCobro", GetType(String))
            _tablaPedidos.Columns.Add("PrecioUnitario", GetType(Decimal))
            _tablaPedidos.Columns.Add("Subtotal", GetType(Decimal))
            _tablaPedidos.Columns.Add("Impuesto", GetType(Decimal))
            _tablaPedidos.Columns.Add("Total", GetType(Decimal))
            _tablaPedidos.Columns.Add("Estado", GetType(String)) ' PENDIENTE, PAGADO
            _tablaPedidos.Columns.Add("EstadoCocina", GetType(String)) ' RECIBIDO, EN_PREPARACION, LISTO, ENTREGADO
            _tablaPedidos.Columns.Add("MetodoPago", GetType(String)) ' Efectivo, Tarjeta POS, Transferencia / QR
            _tablaPedidos.Columns.Add("MontoRecibido", GetType(Decimal))
            _tablaPedidos.Columns.Add("Cambio", GetType(Decimal))
            _tablaPedidos.Columns.Add("Facturado", GetType(Boolean))
            _tablaPedidos.Columns.Add("NumeroFactura", GetType(String))
            _tablaPedidos.Columns.Add("RUC_Cedula", GetType(String))
            _tablaPedidos.Columns.Add("RazonSocial", GetType(String))
            _tablaPedidos.Columns.Add("DireccionFiscal", GetType(String))
            _tablaPedidos.Columns.Add("TelefonoCliente", GetType(String))
            _tablaPedidos.Columns.Add("CorreoCliente", GetType(String))
            _tablaPedidos.PrimaryKey = New DataColumn() {_tablaPedidos.Columns("ID")}

            ' 2. Tabla Detalle de Pedidos (equivalente a 'detalle_pedidos' en BD)
            _tablaDetallePedidos = New DataTable("DetallePedidos")
            _tablaDetallePedidos.Columns.Add("ID", GetType(Integer))
            _tablaDetallePedidos.Columns.Add("IdPedido", GetType(Integer))
            _tablaDetallePedidos.Columns.Add("Plato", GetType(String))
            _tablaDetallePedidos.Columns.Add("Cantidad", GetType(Integer))
            _tablaDetallePedidos.Columns.Add("PrecioUnitario", GetType(Decimal))
            _tablaDetallePedidos.Columns.Add("Subtotal", GetType(Decimal))
            _tablaDetallePedidos.Columns.Add("Acompanamientos", GetType(String))
            _tablaDetallePedidos.Columns.Add("EsAlertaCeliaco", GetType(Boolean))
            _tablaDetallePedidos.Columns.Add("MensajeAlerta", GetType(String))
            _tablaDetallePedidos.PrimaryKey = New DataColumn() {_tablaDetallePedidos.Columns("ID")}

            ' Cargar pedidos iniciales ordenados cronológicamente respetando la Ley de FIFO
            ' (El más antiguo llegó hace 18 min -> Posición FIFO #1)
            CargarDatosDemostracionPanamenos()
        End Sub

        Private Sub CargarDatosDemostracionPanamenos()
            ' Pedido #1: Llegó hace 18 minutos (FIFO #1)
            Dim id1 = RegistrarPedidoDemo("Carlos Mendoza", "Mesa 04", "En Mesa", "PAGADO", "Tarjeta POS", "EN_PREPARACION", DateTime.Now.AddMinutes(-18))
            AgregarItemDetalle(id1, "Sancocho Panameño de Gallina Criolla", 1, 7.50D, "Con arroz blanco y culantro")
            AgregarItemDetalle(id1, "Chicha de Nance", 1, 2.00D, "Bebida típica artesanal")
            FinalizarCalculoCabecera(id1)

            ' Pedido #2: Llegó hace 12 minutos (FIFO #2)
            Dim id2 = RegistrarPedidoDemo("María Fernández", "Mesa 09", "En Mesa", "PENDIENTE", "", "RECIBIDO", DateTime.Now.AddMinutes(-12))
            AgregarItemDetalle(id2, "Pescado Frito con Patacones", 1, 10.50D, "Patacones crocantes", True, "CELÍACO: Estrictamente Sin Gluten")
            FinalizarCalculoCabecera(id2)

            ' Pedido #3: Llegó hace 7 minutos (FIFO #3)
            Dim id3 = RegistrarPedidoDemo("Roberto Gómez", "Mesa 02", "En Mesa", "PENDIENTE", "", "EN_PREPARACION", DateTime.Now.AddMinutes(-7))
            AgregarItemDetalle(id3, "Ropa Vieja con Arroz con Guandú", 1, 8.50D, "Plátano tentación")
            AgregarItemDetalle(id3, "Chicha de Limón c/ Raspadura", 1, 1.75D, "Bebida típica")
            FinalizarCalculoCabecera(id3)

            ' Pedido #4: Llegó hace 2 minutos (FIFO #4)
            Dim id4 = RegistrarPedidoDemo("Ana Lucía Torres", "Llevar", "Para Llevar", "PAGADO", "Efectivo", "RECIBIDO", DateTime.Now.AddMinutes(-2))
            AgregarItemDetalle(id4, "Hojaldre con Queso Blanco y Salchicha", 2, 3.50D, "Empacado térmico")
            AgregarItemDetalle(id4, "Soda Nacional", 1, 1.50D, "Bebida fría")
            FinalizarCalculoCabecera(id4)
        End Sub

        Private Function RegistrarPedidoDemo(cliente As String, mesa As String, servicio As String,
                                             estadoPago As String, metodoPago As String,
                                             estadoCocina As String, horaRegistro As DateTime) As Integer
            _ultimoId += 1
            Dim dr As DataRow = _tablaPedidos.NewRow()
            dr("ID") = _ultimoId
            dr("Cliente") = cliente
            dr("Mesa") = mesa
            dr("PlatoPrincipal") = ""
            dr("Acompanamientos") = ""
            dr("TipoServicio") = servicio
            dr("FechaHora") = horaRegistro.ToString("yyyy-MM-dd HH:mm:ss")
            dr("FechaCobro") = If(estadoPago = "PAGADO", horaRegistro.ToString("yyyy-MM-dd HH:mm:ss"), "")
            dr("PrecioUnitario") = 0D
            dr("Subtotal") = 0D
            dr("Impuesto") = 0D
            dr("Total") = 0D
            dr("Estado") = estadoPago
            dr("EstadoCocina") = estadoCocina
            dr("MetodoPago") = metodoPago
            dr("MontoRecibido") = 0D
            dr("Cambio") = 0D
            dr("Facturado") = False
            dr("NumeroFactura") = ""
            dr("RUC_Cedula") = ""
            dr("RazonSocial") = ""
            dr("DireccionFiscal") = ""
            dr("TelefonoCliente") = ""
            dr("CorreoCliente") = ""
            _tablaPedidos.Rows.Add(dr)
            Return _ultimoId
        End Function

        Private Sub AgregarItemDetalle(idPedido As Integer, plato As String, cantidad As Integer,
                                       precioUnitario As Decimal, notas As String,
                                       Optional esCeliaco As Boolean = False,
                                       Optional mensajeAlerta As String = "")
            _ultimoIdDetalle += 1
            Dim subtotal As Decimal = Math.Round(cantidad * precioUnitario, 2)

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    ' Intento de persistencia en detalle_pedidos de PostgreSQL
                    Dim sqlDetalle As String = "INSERT INTO detalle_pedidos (id_pedido, nombre_plato, cantidad, precio_unitario, subtotal, acompanamientos) " &
                                               "VALUES (@idPed, @plato, @cant, @precio, @subtotal, @acomp);"
                    Dim pIdPed As New NpgsqlParameter("@idPed", idPedido)
                    Dim pPlato As New NpgsqlParameter("@plato", plato.Trim())
                    Dim pCant As New NpgsqlParameter("@cant", cantidad)
                    Dim pPrecio As New NpgsqlParameter("@precio", precioUnitario)
                    Dim pSub As New NpgsqlParameter("@subtotal", subtotal)
                    Dim pAcomp As New NpgsqlParameter("@acomp", notas.Trim())
                    ConexionBD.EjecutarComando(sqlDetalle, pIdPed, pPlato, pCant, pPrecio, pSub, pAcomp)
                Catch ex As Exception
                    ' Respaldo si la columna es id_plato en lugar de nombre_plato
                    Try
                        Dim sqlFallback As String = "INSERT INTO detalle_pedidos (id_pedido, cantidad, precio_unitario, subtotal, acompanamientos) " &
                                                    "VALUES (@idPed, @cant, @precio, @subtotal, @acomp);"
                        Dim pIdPed As New NpgsqlParameter("@idPed", idPedido)
                        Dim pCant As New NpgsqlParameter("@cant", cantidad)
                        Dim pPrecio As New NpgsqlParameter("@precio", precioUnitario)
                        Dim pSub As New NpgsqlParameter("@subtotal", subtotal)
                        Dim pAcomp As New NpgsqlParameter("@acomp", notas.Trim())
                        ConexionBD.EjecutarComando(sqlFallback, pIdPed, pCant, pPrecio, pSub, pAcomp)
                    Catch ex2 As Exception
                    End Try
                End Try
            End If

            Dim drDet As DataRow = _tablaDetallePedidos.NewRow()
            drDet("ID") = _ultimoIdDetalle
            drDet("IdPedido") = idPedido
            drDet("Plato") = plato.Trim()
            drDet("Cantidad") = cantidad
            drDet("PrecioUnitario") = precioUnitario
            drDet("Subtotal") = subtotal
            drDet("Acompanamientos") = notas.Trim()
            drDet("EsAlertaCeliaco") = esCeliaco
            drDet("MensajeAlerta") = mensajeAlerta.Trim()
            _tablaDetallePedidos.Rows.Add(drDet)
        End Sub

        Private Sub FinalizarCalculoCabecera(idPedido As Integer)
            Dim drPedido = ObtenerPedidoPorId(idPedido)
            If drPedido Is Nothing Then Return

            Dim rowsDetalles = _tablaDetallePedidos.Select($"IdPedido = {idPedido}")
            Dim decTotal As Decimal = 0D
            Dim listaPlatos As New List(Of String)()
            Dim listaAcomp As New List(Of String)()

            For Each d In rowsDetalles
                decTotal += Convert.ToDecimal(d("Subtotal"))
                Dim cant = Convert.ToInt32(d("Cantidad"))
                Dim nombre = d("Plato").ToString()
                listaPlatos.Add($"{cant}x {nombre}")
                Dim ac = d("Acompanamientos").ToString()
                If Not String.IsNullOrWhiteSpace(ac) Then listaAcomp.Add(ac)
            Next

            drPedido("PlatoPrincipal") = String.Join(" + ", listaPlatos)
            drPedido("Acompanamientos") = If(listaAcomp.Count > 0, String.Join(", ", listaAcomp), "Sin notas adicionales")
            drPedido("Total") = decTotal
            drPedido("PrecioUnitario") = decTotal
            drPedido("Subtotal") = Math.Round(decTotal / 1.07D, 2)
            drPedido("Impuesto") = Math.Round(decTotal - CDec(drPedido("Subtotal")), 2)
            If drPedido("Estado").ToString() = "PAGADO" Then
                drPedido("MontoRecibido") = decTotal
            End If
        End Sub
#End Region

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
        ''' Sincroniza un conjunto de registros obtenidos desde PostgreSQL con la tabla local en memoria.
        ''' </summary>
        Private Sub SincronizarTablaPedidosDesdeBD(dtBD As DataTable)
            If dtBD Is Nothing Then Return
            SyncLock _tablaPedidos
                For Each rowBD As DataRow In dtBD.Rows
                    Dim id As Integer = Convert.ToInt32(rowBD("ID"))
                    If id > _ultimoId Then _ultimoId = id

                    Dim filaExistente As DataRow = _tablaPedidos.Rows.Find(id)

                    If filaExistente Is Nothing Then
                        filaExistente = _tablaPedidos.NewRow()
                        filaExistente("ID") = id
                        _tablaPedidos.Rows.Add(filaExistente)
                    End If

                    For Each col As DataColumn In dtBD.Columns
                        If _tablaPedidos.Columns.Contains(col.ColumnName) AndAlso rowBD(col) IsNot DBNull.Value Then
                            Try
                                filaExistente(col.ColumnName) = rowBD(col)
                            Catch
                            End Try
                        End If
                    Next
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Sincroniza las líneas de detalle obtenidas desde PostgreSQL con la tabla local.
        ''' </summary>
        Private Sub SincronizarTablaDetallesDesdeBD(dtDetalleBD As DataTable)
            If dtDetalleBD Is Nothing Then Return
            SyncLock _tablaDetallePedidos
                For Each rowBD As DataRow In dtDetalleBD.Rows
                    Dim idDet As Integer = Convert.ToInt32(rowBD("ID"))
                    If idDet > _ultimoIdDetalle Then _ultimoIdDetalle = idDet

                    Dim filaExistente As DataRow = _tablaDetallePedidos.Rows.Find(idDet)

                    If filaExistente Is Nothing Then
                        filaExistente = _tablaDetallePedidos.NewRow()
                        filaExistente("ID") = idDet
                        _tablaDetallePedidos.Rows.Add(filaExistente)
                    End If

                    For Each col As DataColumn In dtDetalleBD.Columns
                        If _tablaDetallePedidos.Columns.Contains(col.ColumnName) AndAlso rowBD(col) IsNot DBNull.Value Then
                            Try
                                filaExistente(col.ColumnName) = rowBD(col)
                            Catch
                            End Try
                        End If
                    Next

                    Dim strPlato As String = If(filaExistente("Plato") IsNot DBNull.Value, filaExistente("Plato").ToString(), "")
                    Dim blnCeliaco As Boolean = strPlato.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0 OrElse strPlato.IndexOf("SIN GLUTEN", StringComparison.OrdinalIgnoreCase) >= 0
                    filaExistente("EsAlertaCeliaco") = blnCeliaco
                    filaExistente("MensajeAlerta") = If(blnCeliaco, "CELÍACO: Estrictamente Sin Gluten", "")
                Next
            End SyncLock
        End Sub

        ''' <summary>
        ''' Obtiene la tabla completa de pedidos registrados. Consulta PostgreSQL si está activo; si no, retorna el registro local.
        ''' </summary>
        Public Function ObtenerTodos() As DataTable
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
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

                    Dim dtPG = ConexionBD.EjecutarConsultaDataTable(sql)
                    If dtPG IsNot Nothing AndAlso dtPG.Columns.Count > 0 Then
                        SincronizarTablaPedidosDesdeBD(dtPG)

                        ' Cargar también los detalles del pedido desde PostgreSQL
                        Try
                            Dim sqlDetalles As String =
                                "SELECT " &
                                "  dp.id_detalle AS ""ID"", " &
                                "  dp.id_pedido AS ""IdPedido"", " &
                                "  COALESCE(dp.nombre_plato, pl.nombre_plato, 'Plato del Menú') AS ""Plato"", " &
                                "  dp.cantidad AS ""Cantidad"", " &
                                "  dp.precio_unitario AS ""PrecioUnitario"", " &
                                "  dp.subtotal AS ""Subtotal"", " &
                                "  COALESCE(dp.acompanamientos, '') AS ""Acompanamientos"" " &
                                "FROM detalle_pedidos dp " &
                                "LEFT JOIN platos pl ON dp.id_plato = pl.id_plato;"

                            Dim dtDetallesPG = ConexionBD.EjecutarConsultaDataTable(sqlDetalles)
                            If dtDetallesPG IsNot Nothing Then
                                SincronizarTablaDetallesDesdeBD(dtDetallesPG)
                            End If
                        Catch
                        End Try

                        Return dtPG
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            Return _tablaPedidos.Copy()
        End Function

        ''' <summary>
        ''' Obtiene la tabla completa de detalles de pedidos registrados.
        ''' </summary>
        Public Function ObtenerDetallesTodos() As DataTable
            Return _tablaDetallePedidos.Copy()
        End Function

        ''' <summary>
        ''' Obtiene únicamente los pedidos con estado PENDIENTE para su cobro en Caja.
        ''' Consulta activamente la BD PostgreSQL del servidor si está habilitada, con respaldo local.
        ''' </summary>
        Public Function ObtenerPedidosPendientes() As DataTable
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
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

                    Dim dtPG = ConexionBD.EjecutarConsultaDataTable(sql)
                    If dtPG IsNot Nothing AndAlso dtPG.Columns.Count > 0 Then
                        SincronizarTablaPedidosDesdeBD(dtPG)
                        Return dtPG
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            Dim vista As New DataView(_tablaPedidos) With {
                .RowFilter = "Estado = 'PENDIENTE'"
            }
            Return vista.ToTable()
        End Function

        ''' <summary>
        ''' Obtiene pedidos cobrados/pagados elegibles para visualización y facturación.
        ''' Consulta activamente la BD PostgreSQL del servidor si está habilitada, con respaldo local.
        ''' </summary>
        Public Function ObtenerPedidosPagados() As DataTable
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
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

                    Dim dtPG = ConexionBD.EjecutarConsultaDataTable(sql)
                    If dtPG IsNot Nothing AndAlso dtPG.Columns.Count > 0 Then
                        SincronizarTablaPedidosDesdeBD(dtPG)
                        Return dtPG
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            Dim vista As New DataView(_tablaPedidos) With {
                .RowFilter = "Estado = 'PAGADO'"
            }
            Return vista.ToTable()
        End Function

        ''' <summary>
        ''' Busca un pedido por su identificador único. Si está habilitado PostgreSQL y no existe en caché, consulta la BD.
        ''' </summary>
        Public Function ObtenerPedidoPorId(id As Integer) As DataRow
            Dim rowEnMemoria As DataRow = _tablaPedidos.Rows.Find(id)
            If rowEnMemoria IsNot Nothing Then
                Return rowEnMemoria
            End If

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
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
                        SincronizarTablaPedidosDesdeBD(dt)
                        Return _tablaPedidos.Rows.Find(id)
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Registra un pedido completo desde el Módulo de Cliente con su desglose exacto
        ''' de platos del menú, cantidades, extras y total general calculado.
        ''' Despacha de inmediato la notificación en tiempo real a Cocina (KDS).
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
            Try
                _ultimoId += 1
                Dim idNuevo As Integer = _ultimoId

                Dim strCliente As String = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
                Dim strMesa As String = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
                Dim strServicio As String = If(String.IsNullOrWhiteSpace(servicio), "En Mesa", servicio.Trim())
                Dim strCorreo As String = If(String.IsNullOrWhiteSpace(correoCliente), "cliente@restaurante.com", correoCliente.Trim())

                ' Si PostgreSQL está activo, persistir en la base de datos y obtener el ID generado
                If ConexionBD.DebeUsarPostgreSQL() Then
                    Try
                        Dim sqlPG As String = "INSERT INTO pedidos (nombre_cliente, mesa_o_servicio, tipo_servicio, estado, estado_cocina, total, monto_recibido, cambio, metodo_pago, correo_cliente) " &
                                              "VALUES (@cliente, @mesa, @servicio, @estado, @estadoCocina, @total, @monto, @cambio, @metodo, @correo) RETURNING id_pedido;"
                        Dim pCliente As New NpgsqlParameter("@cliente", strCliente)
                        Dim pMesa As New NpgsqlParameter("@mesa", strMesa)
                        Dim pServicio As New NpgsqlParameter("@servicio", strServicio)
                        Dim pEstado As New NpgsqlParameter("@estado", If(estadoPago.Equals("PAGADO", StringComparison.OrdinalIgnoreCase), "Pagado", "Pendiente"))
                        Dim pEstadoCocina As New NpgsqlParameter("@estadoCocina", If(String.IsNullOrWhiteSpace(estadoCocina), "RECIBIDO", estadoCocina.Trim().ToUpper()))
                        Dim pTotal As New NpgsqlParameter("@total", total)
                        Dim pMonto As New NpgsqlParameter("@monto", If(estadoPago.Equals("PAGADO", StringComparison.OrdinalIgnoreCase), total, 0D))
                        Dim pCambio As New NpgsqlParameter("@cambio", 0D)
                        Dim pMetodo As New NpgsqlParameter("@metodo", If(String.IsNullOrWhiteSpace(metodoPago), "Efectivo", metodoPago))
                        Dim pCorreo As New NpgsqlParameter("@correo", strCorreo)

                        Using conn = ConexionBD.CrearConexion()
                            conn.Open()
                            Using cmd As New NpgsqlCommand(sqlPG, conn)
                                cmd.Parameters.AddRange({pCliente, pMesa, pServicio, pEstado, pEstadoCocina, pTotal, pMonto, pCambio, pMetodo, pCorreo})
                                Dim res = cmd.ExecuteScalar()
                                If res IsNot Nothing AndAlso Not DBNull.Value.Equals(res) Then
                                    idNuevo = Convert.ToInt32(res)
                                    If idNuevo > _ultimoId Then _ultimoId = idNuevo
                                End If
                            End Using
                        End Using
                    Catch ex As Exception
                        ConexionBD.RegistrarFalloServidor(ex.Message)
                    End Try
                End If

                Dim listaResumenPlatos As New List(Of String)()

                ' 1. Insertar líneas detalladas del carrito
                If tablaCarrito IsNot Nothing AndAlso tablaCarrito.Rows.Count > 0 Then
                    For Each r As DataRow In tablaCarrito.Rows
                        Dim strPlato As String = r("Plato").ToString()
                        Dim intCant As Integer = Convert.ToInt32(r("Cant"))
                        Dim decPrecio As Decimal = Convert.ToDecimal(r("Precio"))

                        Dim blnCeliaco As Boolean = strPlato.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                   strPlato.IndexOf("SIN GLUTEN", StringComparison.OrdinalIgnoreCase) >= 0

                        AgregarItemDetalle(idNuevo, strPlato, intCant, decPrecio, "", blnCeliaco, If(blnCeliaco, "CELÍACO: Estrictamente Sin Gluten", ""))
                        listaResumenPlatos.Add($"{intCant}x {strPlato}")
                    Next
                End If

                ' 2. Insertar líneas detalladas de extras y bebidas
                Dim listaNotasExtras As New List(Of String)()
                If listaExtras IsNot Nothing AndAlso listaExtras.Count > 0 Then
                    For Each t In listaExtras
                        Dim strExtraNombre As String = t.Item1
                        Dim intExtraCant As Integer = t.Item2
                        Dim decExtraPrecio As Decimal = t.Item3

                        AgregarItemDetalle(idNuevo, strExtraNombre, intExtraCant, decExtraPrecio, "Bebida / Acompañamiento")
                        listaNotasExtras.Add($"{intExtraCant}x {strExtraNombre}")
                    Next
                End If

                ' 3. Insertar Cabecera en _tablaPedidos
                Dim dr As DataRow = _tablaPedidos.NewRow()
                dr("ID") = idNuevo
                dr("Cliente") = strCliente
                dr("Mesa") = strMesa
                dr("PlatoPrincipal") = If(listaResumenPlatos.Count > 0, String.Join(" + ", listaResumenPlatos), "Plato del Menú")
                dr("Acompanamientos") = If(listaNotasExtras.Count > 0, String.Join(", ", listaNotasExtras), "Sin Extras")
                dr("TipoServicio") = strServicio
                dr("FechaHora") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                dr("FechaCobro") = If(estadoPago = "PAGADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "")
                dr("PrecioUnitario") = total
                dr("Subtotal") = Math.Round(total / 1.07D, 2)
                dr("Impuesto") = Math.Round(total - CDec(dr("Subtotal")), 2)
                dr("Total") = total
                dr("Estado") = estadoPago
                dr("EstadoCocina") = estadoCocina
                dr("MetodoPago") = If(String.IsNullOrWhiteSpace(metodoPago), "Pago en Caja (Efectivo / Tarjeta)", metodoPago)
                dr("MontoRecibido") = If(estadoPago = "PAGADO", total, 0D)
                dr("Cambio") = 0D
                dr("Facturado") = False
                dr("NumeroFactura") = ""
                dr("RUC_Cedula") = ""
                dr("RazonSocial") = ""
                dr("DireccionFiscal") = ""
                dr("TelefonoCliente") = ""
                dr("CorreoCliente") = strCorreo

                _tablaPedidos.Rows.Add(dr)

                ' 4. Notificar a observadores en tiempo real (KDS Cocina)
                DispararPedidoRegistrado(idNuevo)

                Return idNuevo
            Catch ex As Exception
                Return -1
            End Try
        End Function

        ''' <summary>
        ''' Sobrecarga de compatibilidad para registrar un pedido.
        ''' Retorna True si la operación fue exitosa.
        ''' </summary>
        Public Function Guardar(cliente As String, mesa As String, plato As String, acomp As String, servicio As String,
                                Optional total As Decimal = 0D,
                                Optional estadoPago As String = "PENDIENTE",
                                Optional metodoPago As String = "",
                                Optional estadoCocina As String = "RECIBIDO") As Boolean
            Try
                _ultimoId += 1
                Dim idNuevo As Integer = _ultimoId

                Dim strCliente As String = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
                Dim strMesa As String = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
                Dim strPlato As String = If(String.IsNullOrWhiteSpace(plato), "Plato del Menú", plato.Trim())
                Dim strAcomp As String = If(String.IsNullOrWhiteSpace(acomp), "Sin acompañamiento", acomp.Trim())
                Dim strServicio As String = If(String.IsNullOrWhiteSpace(servicio), "En Mesa", servicio.Trim())

                Dim precioFinal As Decimal = If(total > 0D, total, ExtraerPrecioPlato(strPlato))

                If ConexionBD.DebeUsarPostgreSQL() Then
                    Try
                        Dim sqlPG As String = "INSERT INTO pedidos (nombre_cliente, mesa_o_servicio, tipo_servicio, estado, estado_cocina, total, monto_recibido, cambio, metodo_pago) " &
                                              "VALUES (@cliente, @mesa, @servicio, @estado, @estadoCocina, @total, @monto, @cambio, @metodo) RETURNING id_pedido;"
                        Dim pCliente As New NpgsqlParameter("@cliente", strCliente)
                        Dim pMesa As New NpgsqlParameter("@mesa", strMesa)
                        Dim pServicio As New NpgsqlParameter("@servicio", strServicio)
                        Dim pEstado As New NpgsqlParameter("@estado", If(estadoPago.Equals("PAGADO", StringComparison.OrdinalIgnoreCase), "Pagado", "Pendiente"))
                        Dim pEstadoCocina As New NpgsqlParameter("@estadoCocina", If(String.IsNullOrWhiteSpace(estadoCocina), "RECIBIDO", estadoCocina.Trim().ToUpper()))
                        Dim pTotal As New NpgsqlParameter("@total", precioFinal)
                        Dim pMonto As New NpgsqlParameter("@monto", If(estadoPago.Equals("PAGADO", StringComparison.OrdinalIgnoreCase), precioFinal, 0D))
                        Dim pCambio As New NpgsqlParameter("@cambio", 0D)
                        Dim pMetodo As New NpgsqlParameter("@metodo", If(String.IsNullOrWhiteSpace(metodoPago), "Efectivo", metodoPago))

                        Using conn = ConexionBD.CrearConexion()
                            conn.Open()
                            Using cmd As New NpgsqlCommand(sqlPG, conn)
                                cmd.Parameters.AddRange({pCliente, pMesa, pServicio, pEstado, pEstadoCocina, pTotal, pMonto, pCambio, pMetodo})
                                Dim res = cmd.ExecuteScalar()
                                If res IsNot Nothing AndAlso Not DBNull.Value.Equals(res) Then
                                    idNuevo = Convert.ToInt32(res)
                                    If idNuevo > _ultimoId Then _ultimoId = idNuevo
                                End If
                            End Using
                        End Using
                    Catch ex As Exception
                        ConexionBD.RegistrarFalloServidor(ex.Message)
                    End Try
                End If

                Dim dr As DataRow = _tablaPedidos.NewRow()
                dr("ID") = idNuevo
                dr("Cliente") = strCliente
                dr("Mesa") = strMesa
                dr("PlatoPrincipal") = strPlato
                dr("Acompanamientos") = strAcomp
                dr("TipoServicio") = strServicio
                dr("FechaHora") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                dr("FechaCobro") = If(estadoPago = "PAGADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "")
                dr("PrecioUnitario") = precioFinal
                dr("Subtotal") = Math.Round(precioFinal / 1.07D, 2)
                dr("Impuesto") = Math.Round(precioFinal - CDec(dr("Subtotal")), 2)
                dr("Total") = precioFinal
                dr("Estado") = estadoPago
                dr("EstadoCocina") = estadoCocina
                dr("MetodoPago") = metodoPago
                dr("MontoRecibido") = If(estadoPago = "PAGADO", precioFinal, 0D)
                dr("Cambio") = 0D
                dr("Facturado") = False
                dr("NumeroFactura") = ""
                dr("RUC_Cedula") = ""
                dr("RazonSocial") = ""
                dr("DireccionFiscal") = ""
                dr("TelefonoCliente") = ""
                dr("CorreoCliente") = ""

                _tablaPedidos.Rows.Add(dr)

                ' Crear línea en detalle_pedidos
                Dim blnCeliaco As Boolean = strAcomp.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                           strPlato.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0
                AgregarItemDetalle(idNuevo, strPlato, 1, precioFinal, strAcomp, blnCeliaco, If(blnCeliaco, "CELÍACO: Estrictamente Sin Gluten", ""))

                DispararPedidoRegistrado(idNuevo)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Actualiza la información de un pedido existente por ID.
        ''' </summary>
        Public Function Actualizar(id As Integer, cliente As String, mesa As String, plato As String, acomp As String, servicio As String) As Boolean
            Dim row = _tablaPedidos.Rows.Find(id)
            If row IsNot Nothing Then
                row("Cliente") = cliente.Trim()
                row("Mesa") = mesa.Trim()
                row("PlatoPrincipal") = plato.Trim()
                row("Acompanamientos") = acomp.Trim()
                row("TipoServicio") = servicio.Trim()

                Dim precioFinal As Decimal = ExtraerPrecioPlato(plato)
                row("PrecioUnitario") = precioFinal
                row("Subtotal") = Math.Round(precioFinal / 1.07D, 2)
                row("Impuesto") = Math.Round(precioFinal - CDec(row("Subtotal")), 2)
                row("Total") = precioFinal

                DispararPedidoModificado(id)
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Elimina un pedido por ID de la tabla en memoria y en la base de datos PostgreSQL.
        ''' </summary>
        Public Function Eliminar(id As Integer) As Boolean
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    ' Eliminar detalles y cabecera en PostgreSQL
                    Dim pIdDet As New NpgsqlParameter("@id", id)
                    ConexionBD.EjecutarComando("DELETE FROM detalle_pedidos WHERE id_pedido = @id;", pIdDet)

                    Dim pIdPed As New NpgsqlParameter("@id", id)
                    ConexionBD.EjecutarComando("DELETE FROM pedidos WHERE id_pedido = @id;", pIdPed)
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            Dim rowEliminar = _tablaPedidos.Rows.Find(id)
            If rowEliminar IsNot Nothing Then
                _tablaPedidos.Rows.Remove(rowEliminar)

                ' Eliminar detalles en cascada
                For j As Integer = _tablaDetallePedidos.Rows.Count - 1 To 0 Step -1
                    If Convert.ToInt32(_tablaDetallePedidos.Rows(j)("IdPedido")) = id Then
                        _tablaDetallePedidos.Rows.RemoveAt(j)
                    End If
                Next

                DispararPedidoModificado(id)
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Confirma el pago de un pedido en Caja (RF-011, CU-006) y lo sincroniza para cocina y BD PostgreSQL.
        ''' </summary>
        Public Function ConfirmarCobro(id As Integer, metodoPago As String, montoRecibido As Decimal, cambio As Decimal) As Boolean
            Dim exitoBD As Boolean = False
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "UPDATE pedidos SET estado = 'Pagado', metodo_pago = @metodo, monto_recibido = @monto, cambio = @cambio, fecha_cobro = CURRENT_TIMESTAMP WHERE id_pedido = @id;"
                    Dim p1 As New NpgsqlParameter("@metodo", metodoPago)
                    Dim p2 As New NpgsqlParameter("@monto", montoRecibido)
                    Dim p3 As New NpgsqlParameter("@cambio", cambio)
                    Dim p4 As New NpgsqlParameter("@id", id)
                    Dim filas = ConexionBD.EjecutarComando(sql, p1, p2, p3, p4)
                    exitoBD = (filas > 0)
                Catch ex As Exception
                    ' Respaldo si no existe la columna fecha_cobro en el esquema de BD
                    Try
                        Dim sqlBasico As String = "UPDATE pedidos SET estado = 'Pagado', metodo_pago = @metodo, monto_recibido = @monto, cambio = @cambio WHERE id_pedido = @id;"
                        Dim p1 As New NpgsqlParameter("@metodo", metodoPago)
                        Dim p2 As New NpgsqlParameter("@monto", montoRecibido)
                        Dim p3 As New NpgsqlParameter("@cambio", cambio)
                        Dim p4 As New NpgsqlParameter("@id", id)
                        Dim filas = ConexionBD.EjecutarComando(sqlBasico, p1, p2, p3, p4)
                        exitoBD = (filas > 0)
                    Catch ex2 As Exception
                        ConexionBD.RegistrarFalloServidor(ex2.Message)
                    End Try
                End Try
            End If

            ' Sincronizar en memoria para actualización inmediata de vistas y cocina KDS
            Dim rowCobro = _tablaPedidos.Rows.Find(id)
            Dim encontradoMemoria As Boolean = False
            If rowCobro IsNot Nothing Then
                rowCobro("Estado") = "PAGADO"
                rowCobro("FechaCobro") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                rowCobro("MetodoPago") = metodoPago
                rowCobro("MontoRecibido") = montoRecibido
                rowCobro("Cambio") = cambio
                encontradoMemoria = True
            End If

            DispararPedidoModificado(id)
            DispararPedidoRegistrado(id)
            Return (exitoBD OrElse encontradoMemoria)
        End Function

        ''' <summary>
        ''' Registra los datos fiscales y genera el número correlativo fiscal único de factura.
        ''' Sincroniza tanto en la base de datos PostgreSQL como en la memoria local.
        ''' </summary>
        Public Function RegistrarFactura(id As Integer, rucCedula As String, razonSocial As String, direccion As String, telefono As String, correo As String) As String
            _contadorFacturas += 1
            Dim correlativo = $"FAC-2026-{_contadorFacturas}"

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "UPDATE pedidos SET facturado = TRUE, numero_factura = @num, ruc_cedula = @ruc, razon_social = @razon, direccion_fiscal = @dir, telefono_cliente = @tel, correo_cliente = @correo WHERE id_pedido = @id;"
                    Dim p1 As New NpgsqlParameter("@num", correlativo)
                    Dim p2 As New NpgsqlParameter("@ruc", rucCedula.Trim())
                    Dim p3 As New NpgsqlParameter("@razon", razonSocial.Trim())
                    Dim p4 As New NpgsqlParameter("@dir", direccion.Trim())
                    Dim p5 As New NpgsqlParameter("@tel", telefono.Trim())
                    Dim p6 As New NpgsqlParameter("@correo", correo.Trim())
                    Dim p7 As New NpgsqlParameter("@id", id)
                    ConexionBD.EjecutarComando(sql, p1, p2, p3, p4, p5, p6, p7)
                Catch ex As Exception
                    ConexionBD.UltimoMensajeEstado = $"Error al actualizar datos fiscales en PostgreSQL: {ex.Message}"
                End Try
            End If

            Dim rowFac = _tablaPedidos.Rows.Find(id)
            If rowFac IsNot Nothing Then
                If CBool(rowFac("Facturado")) AndAlso Not String.IsNullOrEmpty(rowFac("NumeroFactura").ToString()) Then
                    Return rowFac("NumeroFactura").ToString()
                End If

                rowFac("Facturado") = True
                rowFac("NumeroFactura") = correlativo
                rowFac("RUC_Cedula") = rucCedula.Trim()
                rowFac("RazonSocial") = razonSocial.Trim()
                rowFac("DireccionFiscal") = direccion.Trim()
                rowFac("TelefonoCliente") = telefono.Trim()
                rowFac("CorreoCliente") = correo.Trim()
                DispararPedidoModificado(id)
                Return correlativo
            End If

            DispararPedidoModificado(id)
            Return correlativo
        End Function

        ''' <summary>
        ''' Obtiene el próximo correlativo fiscal estimado.
        ''' </summary>
        Public Function ObtenerProximoNumeroFactura() As String
            Return $"FAC-2026-{_contadorFacturas + 1}"
        End Function

        ''' <summary>
        ''' Actualiza el estado de la comanda en cocina (RECIBIDO, EN_PREPARACION, LISTO, ENTREGADO).
        ''' Regla de integridad: No se permite marcar como ENTREGADO si el pedido no está PAGADO.
        ''' Persiste de forma inmediata en PostgreSQL y en memoria local.
        ''' </summary>
        Public Function ActualizarEstadoCocina(id As Integer, nuevoEstadoCocina As String) As Boolean
            Dim estadoCocinaNormalizado As String = nuevoEstadoCocina.Trim().ToUpper()

            ' 1. Verificar estado actual de pago antes de permitir ENTREGADO
            Dim pedidoRow As DataRow = ObtenerPedidoPorId(id)
            If pedidoRow IsNot Nothing Then
                Dim estadoPago = pedidoRow("Estado").ToString().ToUpper()
                If estadoCocinaNormalizado = "ENTREGADO" AndAlso estadoPago <> "PAGADO" Then
                    ' Bloqueo estricto: Todo producto debe ser pagado antes de ser entregado o despachado
                    Return False
                End If
            End If

            ' 2. Persistir en la base de datos PostgreSQL si está conectada
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "UPDATE pedidos SET estado_cocina = @estadoCocina WHERE id_pedido = @id;"
                    Dim pEstado As New NpgsqlParameter("@estadoCocina", estadoCocinaNormalizado)
                    Dim pId As New NpgsqlParameter("@id", id)
                    ConexionBD.EjecutarComando(sql, pEstado, pId)
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            ' 3. Sincronizar en memoria y notificar a los observadores reactivos
            Dim rowCocina = _tablaPedidos.Rows.Find(id)
            If rowCocina IsNot Nothing Then
                rowCocina("EstadoCocina") = estadoCocinaNormalizado
                DispararPedidoModificado(id)
                Return True
            End If

            DispararPedidoModificado(id)
            Return True
        End Function

        ''' <summary>
        ''' Obtiene todas las órdenes del repositorio mapeadas al modelo de comanda de cocina CcnPedidoModel.
        ''' Incluye el desglose individual de platos desde _tablaDetallePedidos.
        ''' </summary>
        Public Function ObtenerComandasCocina() As List(Of Models.CcnPedidoModel)
            If ConexionBD.DebeUsarPostgreSQL() Then
                ' Asegurar sincronización activa de pedidos desde la base de datos
                ObtenerTodos()
            End If

            Dim lista As New List(Of Models.CcnPedidoModel)()

            For Each row As DataRow In _tablaPedidos.Rows
                Dim id As Integer = Convert.ToInt32(row("ID"))
                Dim strEstadoPago As String = If(row("Estado") IsNot DBNull.Value, row("Estado").ToString().ToUpper(), "PENDIENTE")
                Dim strMetodo As String = If(row("MetodoPago") IsNot DBNull.Value, row("MetodoPago").ToString(), "Pendiente")

                ' El pedido se visualiza en Cocina KDS. Si está PENDIENTE de pago, la comanda
                ' muestra el indicador [NO PAGADO (Cobrar en Caja)] y bloquea la entrega hasta que Caja confirme cobro.

                Dim strCodigo As String = $"#08-{1040 + id}"
                Dim strMesa As String = If(row("Mesa") IsNot DBNull.Value, row("Mesa").ToString(), "Mesa 01")
                Dim strCliente As String = If(row("Cliente") IsNot DBNull.Value, row("Cliente").ToString(), "Cliente General")
                Dim strServicio As String = If(row("TipoServicio") IsNot DBNull.Value, row("TipoServicio").ToString(), "Comer en el Sitio")
                Dim blnPagado As Boolean = (strEstadoPago = "PAGADO")
                Dim blnFacturado As Boolean = (row("Facturado") IsNot DBNull.Value AndAlso CBool(row("Facturado")))
                Dim strNumFactura As String = If(row("NumeroFactura") IsNot DBNull.Value, row("NumeroFactura").ToString(), "")

                Dim strEstadoCocina As String = If(_tablaPedidos.Columns.Contains("EstadoCocina") AndAlso row("EstadoCocina") IsNot DBNull.Value, row("EstadoCocina").ToString().ToUpper(), "RECIBIDO")
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
                If row("FechaHora") IsNot DBNull.Value Then
                    Dim strHora As String = row("FechaHora").ToString()
                    Dim parsedDt As DateTime
                    If DateTime.TryParse(strHora, parsedDt) Then
                        dtHora = parsedDt
                    Else
                        Dim ts As TimeSpan
                        If TimeSpan.TryParse(strHora, ts) Then
                            dtHora = DateTime.Today.Add(ts)
                        End If
                    End If
                End If

                Dim objComanda As New Models.CcnPedidoModel(
                    id, strCodigo, strMesa, "Mozo General", strCliente, strServicio,
                    strMetodo, blnPagado, dtHora, enumEstado
                )
                objComanda.BlnFacturado = blnFacturado
                objComanda.StrNumeroFactura = strNumFactura

                ' Cargar platos detallados desde _tablaDetallePedidos
                Dim rowsDetalle = _tablaDetallePedidos.Select($"IdPedido = {id}")
                If rowsDetalle.Length > 0 Then
                    For Each r In rowsDetalle
                        Dim cant = Convert.ToInt32(r("Cantidad"))
                        Dim plato = r("Plato").ToString()
                        Dim precioUnit = Convert.ToDecimal(r("PrecioUnitario"))
                        Dim acomp = If(r("Acompanamientos") IsNot DBNull.Value, r("Acompanamientos").ToString(), "")
                        Dim celiaco = (r("EsAlertaCeliaco") IsNot DBNull.Value AndAlso CBool(r("EsAlertaCeliaco")))
                        Dim msgAlerta = If(r("MensajeAlerta") IsNot DBNull.Value, r("MensajeAlerta").ToString(), "")

                        objComanda.LstDetallePlatos.Add(New Models.CcnItemPedidoModel(cant, plato, acomp, precioUnit, celiaco, msgAlerta))
                    Next
                Else
                    Dim strPlato As String = If(row("PlatoPrincipal") IsNot DBNull.Value, row("PlatoPrincipal").ToString(), "Plato del Menú")
                    Dim strAcomp As String = If(row("Acompanamientos") IsNot DBNull.Value, row("Acompanamientos").ToString(), "")
                    Dim decPrecio As Decimal = If(row("Total") IsNot DBNull.Value, Convert.ToDecimal(row("Total")), 15.0D)
                    Dim blnCelíaco As Boolean = strAcomp.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0 OrElse strPlato.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0

                    objComanda.LstDetallePlatos.Add(New Models.CcnItemPedidoModel(1, strPlato, strAcomp, decPrecio, blnCelíaco, If(blnCelíaco, "CELÍACO: Estrictamente Sin Gluten", "")))
                End If

                lista.Add(objComanda)
            Next

            Return lista
        End Function

    End Module
End Namespace
