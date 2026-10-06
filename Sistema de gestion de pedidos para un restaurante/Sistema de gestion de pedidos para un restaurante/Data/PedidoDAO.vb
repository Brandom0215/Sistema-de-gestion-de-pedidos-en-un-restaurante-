Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Text.RegularExpressions

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
            Dim drDet As DataRow = _tablaDetallePedidos.NewRow()
            drDet("ID") = _ultimoIdDetalle
            drDet("IdPedido") = idPedido
            drDet("Plato") = plato.Trim()
            drDet("Cantidad") = cantidad
            drDet("PrecioUnitario") = precioUnitario
            drDet("Subtotal") = Math.Round(cantidad * precioUnitario, 2)
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
        ''' Obtiene la tabla completa de pedidos registrados.
        ''' </summary>
        Public Function ObtenerTodos() As DataTable
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
        ''' </summary>
        Public Function ObtenerPedidosPendientes() As DataTable
            Dim vista As New DataView(_tablaPedidos) With {
                .RowFilter = "Estado = 'PENDIENTE'"
            }
            Return vista.ToTable()
        End Function

        ''' <summary>
        ''' Obtiene pedidos cobrados/pagados elegibles para visualización y facturación.
        ''' </summary>
        Public Function ObtenerPedidosPagados() As DataTable
            Dim vista As New DataView(_tablaPedidos) With {
                .RowFilter = "Estado = 'PAGADO'"
            }
            Return vista.ToTable()
        End Function

        ''' <summary>
        ''' Busca un pedido por su identificador único.
        ''' </summary>
        Public Function ObtenerPedidoPorId(id As Integer) As DataRow
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    Return row
                End If
            Next
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
                                              Optional estadoCocina As String = "RECIBIDO") As Integer
            Try
                _ultimoId += 1
                Dim idNuevo As Integer = _ultimoId

                Dim strCliente As String = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
                Dim strMesa As String = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
                Dim strServicio As String = If(String.IsNullOrWhiteSpace(servicio), "En Mesa", servicio.Trim())

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
                dr("MetodoPago") = metodoPago
                dr("MontoRecibido") = If(estadoPago = "PAGADO", total, 0D)
                dr("Cambio") = 0D
                dr("Facturado") = False
                dr("NumeroFactura") = ""
                dr("RUC_Cedula") = ""
                dr("RazonSocial") = ""
                dr("DireccionFiscal") = ""
                dr("TelefonoCliente") = ""
                dr("CorreoCliente") = ""

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
        ''' Retorna el ID numérico asignado al pedido.
        ''' </summary>
        Public Function Guardar(cliente As String, mesa As String, plato As String, acomp As String, servicio As String,
                                Optional total As Decimal = 0D,
                                Optional estadoPago As String = "PENDIENTE",
                                Optional metodoPago As String = "",
                                Optional estadoCocina As String = "RECIBIDO") As Integer
            Try
                _ultimoId += 1
                Dim idNuevo As Integer = _ultimoId

                Dim strCliente As String = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
                Dim strMesa As String = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
                Dim strPlato As String = If(String.IsNullOrWhiteSpace(plato), "Plato del Menú", plato.Trim())
                Dim strAcomp As String = If(String.IsNullOrWhiteSpace(acomp), "Sin acompañamiento", acomp.Trim())
                Dim strServicio As String = If(String.IsNullOrWhiteSpace(servicio), "En Mesa", servicio.Trim())

                Dim precioFinal As Decimal = If(total > 0D, total, ExtraerPrecioPlato(strPlato))

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
                Return idNuevo
            Catch ex As Exception
                Return -1
            End Try
        End Function

        ''' <summary>
        ''' Actualiza la información de un pedido existente por ID.
        ''' </summary>
        Public Function Actualizar(id As Integer, cliente As String, mesa As String, plato As String, acomp As String, servicio As String) As Boolean
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
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
            Next
            Return False
        End Function

        ''' <summary>
        ''' Elimina un pedido por ID de la tabla en memoria.
        ''' </summary>
        Public Function Eliminar(id As Integer) As Boolean
            For i As Integer = _tablaPedidos.Rows.Count - 1 To 0 Step -1
                If Convert.ToInt32(_tablaPedidos.Rows(i)("ID")) = id Then
                    _tablaPedidos.Rows.RemoveAt(i)

                    ' Eliminar detalles en cascada
                    For j As Integer = _tablaDetallePedidos.Rows.Count - 1 To 0 Step -1
                        If Convert.ToInt32(_tablaDetallePedidos.Rows(j)("IdPedido")) = id Then
                            _tablaDetallePedidos.Rows.RemoveAt(j)
                        End If
                    Next

                    DispararPedidoModificado(id)
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Confirma el pago de un pedido en Caja (RF-011, CU-006) y lo sincroniza para cocina.
        ''' </summary>
        Public Function ConfirmarCobro(id As Integer, metodoPago As String, montoRecibido As Decimal, cambio As Decimal) As Boolean
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    row("Estado") = "PAGADO"
                    row("FechaCobro") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    row("MetodoPago") = metodoPago
                    row("MontoRecibido") = montoRecibido
                    row("Cambio") = cambio
                    DispararPedidoModificado(id)
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Registra los datos fiscales y genera el número correlativo fiscal único de factura.
        ''' </summary>
        Public Function RegistrarFactura(id As Integer, rucCedula As String, razonSocial As String, direccion As String, telefono As String, correo As String) As String
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    If CBool(row("Facturado")) AndAlso Not String.IsNullOrEmpty(row("NumeroFactura").ToString()) Then
                        Return row("NumeroFactura").ToString()
                    End If

                    _contadorFacturas += 1
                    Dim correlativo = $"FAC-2026-{_contadorFacturas}"
                    row("Facturado") = True
                    row("NumeroFactura") = correlativo
                    row("RUC_Cedula") = rucCedula.Trim()
                    row("RazonSocial") = razonSocial.Trim()
                    row("DireccionFiscal") = direccion.Trim()
                    row("TelefonoCliente") = telefono.Trim()
                    row("CorreoCliente") = correo.Trim()
                    DispararPedidoModificado(id)
                    Return correlativo
                End If
            Next
            Return String.Empty
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
        ''' </summary>
        Public Function ActualizarEstadoCocina(id As Integer, nuevoEstadoCocina As String) As Boolean
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    Dim estadoCocinaNormalizado As String = nuevoEstadoCocina.Trim().ToUpper()
                    If estadoCocinaNormalizado = "ENTREGADO" AndAlso row("Estado").ToString().ToUpper() <> "PAGADO" Then
                        ' Bloqueo a nivel de capa de datos: No se puede despachar sin pago previo
                        Return False
                    End If

                    row("EstadoCocina") = estadoCocinaNormalizado
                    DispararPedidoModificado(id)
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Obtiene todas las órdenes del repositorio mapeadas al modelo de comanda de cocina CcnPedidoModel.
        ''' Incluye el desglose individual de platos desde _tablaDetallePedidos.
        ''' </summary>
        Public Function ObtenerComandasCocina() As List(Of Models.CcnPedidoModel)
            Dim lista As New List(Of Models.CcnPedidoModel)()

            For Each row As DataRow In _tablaPedidos.Rows
                Dim id As Integer = Convert.ToInt32(row("ID"))
                Dim strCodigo As String = $"#08-{1040 + id}"
                Dim strMesa As String = If(row("Mesa") IsNot DBNull.Value, row("Mesa").ToString(), "Mesa 01")
                Dim strCliente As String = If(row("Cliente") IsNot DBNull.Value, row("Cliente").ToString(), "Cliente General")
                Dim strServicio As String = If(row("TipoServicio") IsNot DBNull.Value, row("TipoServicio").ToString(), "Comer en el Sitio")
                Dim strMetodo As String = If(row("MetodoPago") IsNot DBNull.Value, row("MetodoPago").ToString(), "Pendiente")
                Dim blnPagado As Boolean = (row("Estado").ToString().ToUpper() = "PAGADO")
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
