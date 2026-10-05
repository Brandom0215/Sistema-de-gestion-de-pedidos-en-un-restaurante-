Imports System
Imports System.Data
Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace Data
    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) en memoria centralizado para la gestión de pedidos,
    ''' cobros en caja y facturación electrónica digital (RF-006, RF-007, RF-011, CU-004, CU-006).
    ''' Proporciona persistencia volátil compartida entre módulos sin dependencias de red o servidores externos.
    ''' </summary>
    Public Module PedidoDAO

        Private ReadOnly _tablaPedidos As DataTable
        Private _ultimoId As Integer = 0
        Private _contadorFacturas As Integer = 1000

        Sub New()
            _tablaPedidos = New DataTable("Pedidos")
            _tablaPedidos.Columns.Add("ID", GetType(Integer))
            _tablaPedidos.Columns.Add("Cliente", GetType(String))
            _tablaPedidos.Columns.Add("Mesa", GetType(String))
            _tablaPedidos.Columns.Add("PlatoPrincipal", GetType(String))
            _tablaPedidos.Columns.Add("Acompanamientos", GetType(String))
            _tablaPedidos.Columns.Add("TipoServicio", GetType(String))
            _tablaPedidos.Columns.Add("FechaHora", GetType(String))
            _tablaPedidos.Columns.Add("PrecioUnitario", GetType(Decimal))
            _tablaPedidos.Columns.Add("Subtotal", GetType(Decimal))
            _tablaPedidos.Columns.Add("Impuesto", GetType(Decimal))
            _tablaPedidos.Columns.Add("Total", GetType(Decimal))
            _tablaPedidos.Columns.Add("Estado", GetType(String)) ' PENDIENTE, PAGADO, EN_PREPARACION, LISTO
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

            ' Cargar pedidos iniciales de demostración con estados variados vinculados a Cocina, Caja y Facturación
            AgregarPedidoInicial("Carlos Mendoza", "Mesa 04", "Bife de Chorizo a la Brasa ($24.00)", "Término medio, papas rústicas al romero", "Comer en el Sitio", 24.00D, "PAGADO", "Tarjeta Crédito", 24.00D, 0.0D, False, "", "EN_PREPARACION")
            AgregarPedidoInicial("María Fernández", "Mesa 09", "Risotto de Hongos Silvestres ($24.00)", "CELÍACO: Estrictamente Sin Gluten", "Comer en el Sitio", 24.00D, "PENDIENTE", "", 0, 0, False, "", "RECIBIDO")
            AgregarPedidoInicial("Sofía Alarcón", "ENTREGAS", "Pollo al Limón y Romero a la Leña ($26.00)", "Empacado en contenedor térmico kraft", "Para Llevar", 26.00D, "PAGADO", "Pagado Web (Stripe)", 26.00D, 0.0D, True, "FAC-2026-1001", "LISTO")
            AgregarPedidoInicial("Ana Lucía Torres", "Llevar", "Hamburguesa Gourmet Rústica ($12.00)", "Papas Fritas crujientes con hierbas", "Para Llevar", 12.00D, "PAGADO", "Efectivo", 20.00D, 8.00D, False, "", "RECIBIDO")
            AgregarPedidoInicial("Roberto Gómez", "Mesa 02", "Ceviche Mixto Tradicional ($16.50)", "Salsas de la Casa y maíz crocante", "Comer en el Sitio", 16.50D, "PENDIENTE", "", 0, 0, False, "", "EN_PREPARACION")
        End Sub

        Private Sub AgregarPedidoInicial(cliente As String, mesa As String, plato As String, acomp As String, servicio As String,
                                         precioBase As Decimal, estado As String, metodoPago As String, montoRecibido As Decimal,
                                         cambio As Decimal, facturado As Boolean, numFactura As String,
                                         Optional estadoCocina As String = "RECIBIDO")
            _ultimoId += 1
            Dim dr As DataRow = _tablaPedidos.NewRow()
            dr("ID") = _ultimoId
            dr("Cliente") = cliente
            dr("Mesa") = mesa
            dr("PlatoPrincipal") = plato
            dr("Acompanamientos") = acomp
            dr("TipoServicio") = servicio
            dr("FechaHora") = DateTime.Now.AddMinutes(-_ultimoId * 8).ToString("HH:mm:ss")
            dr("PrecioUnitario") = precioBase
            dr("Subtotal") = Math.Round(precioBase / 1.07D, 2)
            dr("Impuesto") = Math.Round(precioBase - CDec(dr("Subtotal")), 2)
            dr("Total") = precioBase
            dr("Estado") = estado
            dr("EstadoCocina") = estadoCocina
            dr("MetodoPago") = metodoPago
            dr("MontoRecibido") = montoRecibido
            dr("Cambio") = cambio
            dr("Facturado") = facturado
            dr("NumeroFactura") = numFactura
            dr("RUC_Cedula") = If(facturado, "8-854-1234", "")
            dr("RazonSocial") = If(facturado, cliente, "")
            dr("DireccionFiscal") = If(facturado, "Ciudad de Panamá", "")
            dr("TelefonoCliente") = If(facturado, "+507 6890-1122", "")
            dr("CorreoCliente") = If(facturado, "cliente@ejemplo.com", "")
            _tablaPedidos.Rows.Add(dr)
        End Sub

        ''' <summary>
        ''' Extrae el precio numérico del texto del plato (ej. 'Lomo a la Brasa ($18.50)' -> 18.50).
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
                ' Continuar con valor por defecto
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
        ''' Agrega un nuevo pedido al repositorio en memoria centralizado.
        ''' Soporta estados iniciales de pago y de cocina.
        ''' </summary>
        Public Function Guardar(cliente As String, mesa As String, plato As String, acomp As String, servicio As String,
                                Optional total As Decimal = 0D,
                                Optional estadoPago As String = "PENDIENTE",
                                Optional metodoPago As String = "",
                                Optional estadoCocina As String = "RECIBIDO") As Boolean
            Try
                _ultimoId += 1
                Dim dr As DataRow = _tablaPedidos.NewRow()
                dr("ID") = _ultimoId
                dr("Cliente") = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
                dr("Mesa") = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
                dr("PlatoPrincipal") = If(String.IsNullOrWhiteSpace(plato), "Plato del Día", plato.Trim())
                dr("Acompanamientos") = If(String.IsNullOrWhiteSpace(acomp), "Sin acompañamiento", acomp.Trim())
                dr("TipoServicio") = If(String.IsNullOrWhiteSpace(servicio), "En Mesa", servicio.Trim())
                dr("FechaHora") = DateTime.Now.ToString("HH:mm:ss")

                Dim precioFinal As Decimal = If(total > 0D, total, ExtraerPrecioPlato(dr("PlatoPrincipal").ToString()))
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
                Return True
            Catch ex As Exception
                Return False
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
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Confirma el pago de un pedido en Caja (RF-011, CU-006) y lo libera para cocina.
        ''' </summary>
        Public Function ConfirmarCobro(id As Integer, metodoPago As String, montoRecibido As Decimal, cambio As Decimal) As Boolean
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    row("Estado") = "PAGADO"
                    row("MetodoPago") = metodoPago
                    row("MontoRecibido") = montoRecibido
                    row("Cambio") = cambio
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Registra los datos fiscales y genera el número correlativo fiscal único de factura (CU-004, RN-009, RN-010).
        ''' </summary>
        Public Function RegistrarFactura(id As Integer, rucCedula As String, razonSocial As String, direccion As String, telefono As String, correo As String) As String
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    If CBool(row("Facturado")) AndAlso Not String.IsNullOrEmpty(row("NumeroFactura").ToString()) Then
                        ' Si ya fue facturada, el correlativo es inalterable (RN-010)
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
        ''' </summary>
        Public Function ActualizarEstadoCocina(id As Integer, nuevoEstadoCocina As String) As Boolean
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    row("EstadoCocina") = nuevoEstadoCocina.Trim().ToUpper()
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Obtiene todas las órdenes del repositorio mapeadas al modelo de comanda de cocina CcnPedidoModel.
        ''' Conecta información de Cliente, Estado de Pago en Caja y Facturación electrónica.
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

                Dim dtHora As DateTime = DateTime.Now.AddMinutes(-id * 4)
                If row("FechaHora") IsNot DBNull.Value Then
                    Dim strHora As String = row("FechaHora").ToString()
                    Dim ts As TimeSpan
                    If TimeSpan.TryParse(strHora, ts) Then
                        dtHora = DateTime.Today.Add(ts)
                    End If
                End If

                Dim objComanda As New Models.CcnPedidoModel(
                    id, strCodigo, strMesa, "Mozo General", strCliente, strServicio,
                    strMetodo, blnPagado, dtHora, enumEstado
                )
                objComanda.BlnFacturado = blnFacturado
                objComanda.StrNumeroFactura = strNumFactura

                Dim strPlato As String = If(row("PlatoPrincipal") IsNot DBNull.Value, row("PlatoPrincipal").ToString(), "Plato del Día")
                Dim strAcomp As String = If(row("Acompanamientos") IsNot DBNull.Value, row("Acompanamientos").ToString(), "")
                Dim decPrecio As Decimal = If(row("Total") IsNot DBNull.Value, Convert.ToDecimal(row("Total")), 15.0D)

                Dim blnCelíaco As Boolean = strAcomp.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0 OrElse strPlato.IndexOf("CELÍACO", StringComparison.OrdinalIgnoreCase) >= 0

                objComanda.LstDetallePlatos.Add(New Models.CcnItemPedidoModel(1, strPlato, strAcomp, decPrecio, blnCelíaco, If(blnCelíaco, "CELÍACO: Estrictamente Sin Gluten", "")))

                lista.Add(objComanda)
            Next

            Return lista
        End Function

    End Module
End Namespace
