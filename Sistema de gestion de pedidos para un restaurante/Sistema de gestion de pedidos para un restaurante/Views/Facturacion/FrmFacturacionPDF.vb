Imports System
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Facturacion
    ''' <summary>
    ''' Formulario de Facturación & Comprobantes Digitales PDF (RF-007, RN-009, RN-010, CU-004).
    ''' Implementa emisión fiscal con impresión real a impresora física/virtual (PrintDialog/PrintDocument),
    ''' compilación nativa de documentos PDF, y despacho opcional por correo electrónico (RN-011).
    ''' </summary>
    Public Class FrmFacturacionPDF

        Private _idPedidoSeleccionado As Integer = 0
        Private _pedidoSeleccionadoRow As DataRow = Nothing
        Private _carpetaFacturas As String
        Private WithEvents _printDocument As New PrintDocument()
        Private _idPedidoInicial As Integer = 0
        Private _bloquearSelectionChanged As Boolean = False

        Public Sub New()
            Me.New(0)
        End Sub

        Public Sub New(idPedidoInicial As Integer)
            InitializeComponent()
            _idPedidoInicial = idPedidoInicial
            _carpetaFacturas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasEmitidas")
            If Not Directory.Exists(_carpetaFacturas) Then
                Directory.CreateDirectory(_carpetaFacturas)
            End If
        End Sub

        Private Sub FrmFacturacionPDF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarPedidosPagados()

            PedidoDAO.SuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)
            PedidoDAO.SuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
        End Sub

        Private Sub FrmFacturacionPDF_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            PedidoDAO.DesuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)
            PedidoDAO.DesuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
        End Sub

        Private Sub OnPedidoActualizadoDesdeDAO(idPedido As Integer)
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Of Integer)(AddressOf OnPedidoActualizadoDesdeDAO), idPedido)
                Return
            End If

            Dim idActual As Integer = _idPedidoSeleccionado
            CargarPedidosPagados()
            If idActual > 0 Then
                SeleccionarPedido(idActual)
            End If
        End Sub

        Protected Overrides Sub OnVisibleChanged(e As EventArgs)
            MyBase.OnVisibleChanged(e)
            If Me.Visible AndAlso _idPedidoInicial > 0 Then
                Me.BeginInvoke(New Action(Sub()
                                             SeleccionarPedido(_idPedidoInicial)
                                             _idPedidoInicial = 0
                                         End Sub))
            End If
        End Sub

        ''' <summary>
        ''' Aplica la paleta visual oficial y fuentes modernas a todos los controles.
        ''' </summary>
        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlHeader.BackColor = ThemeConfig.ColorBackgroundApp
            pnlContenedor.BackColor = ThemeConfig.ColorBackgroundApp

            lblTituloHeader.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloHeader.ForeColor = ThemeConfig.ColorTextMuted

            grpSeleccion.ForeColor = ThemeConfig.ColorSecondary
            grpDatosFiscales.ForeColor = ThemeConfig.ColorSecondary
            grpVistaPrevia.ForeColor = ThemeConfig.ColorSecondary

            ' Estilizado de la barra de acciones
            ThemeConfig.EstilizarBotonPrimario(btnImprimir)
            ThemeConfig.EstilizarBotonSecundario(btnGuardarComo)
            ThemeConfig.EstilizarBotonSecundario(btnEnviarCorreo)
            ThemeConfig.EstilizarBotonSecundario(btnLimpiar)
            ThemeConfig.EstilizarBotonSecundario(btnBuscar)
            ThemeConfig.EstilizarBotonSecundario(btnRefrescar)
            ThemeConfig.EstilizarBotonSecundario(btnVolverCaja)

            pnlComprobanteVisual.BackColor = ThemeConfig.ColorBackgroundCard

            ' Estilizado de la grilla
            dgvPedidosFacturar.BackgroundColor = Color.White
            dgvPedidosFacturar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 230, 220)
            dgvPedidosFacturar.DefaultCellStyle.SelectionForeColor = ThemeConfig.ColorNeutralDark
            dgvPedidosFacturar.ColumnHeadersDefaultCellStyle.BackColor = ThemeConfig.ColorSecondary
            dgvPedidosFacturar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvPedidosFacturar.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            dgvPedidosFacturar.EnableHeadersVisualStyles = False
            dgvPedidosFacturar.RowTemplate.Height = 26
        End Sub

        ''' <summary>
        ''' Carga las órdenes cobradas y pagadas elegibles para facturación.
        ''' </summary>
        Public Sub CargarPedidosPagados()
            Try
                Dim dtPagados = PedidoDAO.ObtenerPedidosPagados()
                For Each r As DataRow In dtPagados.Rows
                    If r.Table.Columns.Contains("FechaCobro") AndAlso String.IsNullOrWhiteSpace(r("FechaCobro").ToString()) Then
                        If r.Table.Columns.Contains("FechaHora") Then
                            r("FechaCobro") = r("FechaHora")
                        End If
                    End If
                Next

                Dim vista As New DataView(dtPagados)
                vista.Sort = "ID DESC"

                If Not String.IsNullOrWhiteSpace(txtBuscar.Text) Then
                    Dim criterio As String = txtBuscar.Text.Trim().Replace("'", "''")
                    Dim idNum As Integer
                    If Integer.TryParse(criterio, idNum) Then
                        vista.RowFilter = $"ID = {idNum} OR Cliente LIKE '%{criterio}%' OR NumeroFactura LIKE '%{criterio}%'"
                    Else
                        vista.RowFilter = $"Cliente LIKE '%{criterio}%' OR NumeroFactura LIKE '%{criterio}%' OR Mesa LIKE '%{criterio}%'"
                    End If
                End If

                _bloquearSelectionChanged = True
                dgvPedidosFacturar.DataSource = vista

                If dgvPedidosFacturar.Columns.Count > 0 Then
                    If dgvPedidosFacturar.Columns.Contains("ID") Then
                        dgvPedidosFacturar.Columns("ID").HeaderText = "ID"
                        dgvPedidosFacturar.Columns("ID").Width = 50
                    End If
                    If dgvPedidosFacturar.Columns.Contains("NumeroFactura") Then
                        dgvPedidosFacturar.Columns("NumeroFactura").HeaderText = "No. Factura"
                        dgvPedidosFacturar.Columns("NumeroFactura").Width = 115
                    End If
                    If dgvPedidosFacturar.Columns.Contains("FechaCobro") Then
                        dgvPedidosFacturar.Columns("FechaCobro").HeaderText = "Fecha de cobro"
                        dgvPedidosFacturar.Columns("FechaCobro").Width = 140
                    End If
                    If dgvPedidosFacturar.Columns.Contains("Cliente") Then
                        dgvPedidosFacturar.Columns("Cliente").HeaderText = "Cliente"
                        dgvPedidosFacturar.Columns("Cliente").Width = 160
                    End If
                    If dgvPedidosFacturar.Columns.Contains("Mesa") Then
                        dgvPedidosFacturar.Columns("Mesa").HeaderText = "Mesa"
                        dgvPedidosFacturar.Columns("Mesa").Width = 80
                    End If
                    If dgvPedidosFacturar.Columns.Contains("PlatoPrincipal") Then
                        dgvPedidosFacturar.Columns("PlatoPrincipal").HeaderText = "Pedido / Consumo"
                        dgvPedidosFacturar.Columns("PlatoPrincipal").Width = 200
                    End If
                    If dgvPedidosFacturar.Columns.Contains("MetodoPago") Then
                        dgvPedidosFacturar.Columns("MetodoPago").HeaderText = "Forma de pago"
                        dgvPedidosFacturar.Columns("MetodoPago").Width = 110
                    End If
                    If dgvPedidosFacturar.Columns.Contains("Total") Then
                        dgvPedidosFacturar.Columns("Total").HeaderText = "Total"
                        dgvPedidosFacturar.Columns("Total").DefaultCellStyle.Format = "C2"
                        dgvPedidosFacturar.Columns("Total").Width = 85
                    End If

                    ' Ocultar columnas accesorias
                    Dim columnasOcultas = {"Acompanamientos", "TipoServicio", "FechaHora", "PrecioUnitario", "Subtotal",
                                           "Impuesto", "Estado", "MontoRecibido", "Cambio", "Facturado",
                                           "RUC_Cedula", "RazonSocial", "DireccionFiscal", "TelefonoCliente", "CorreoCliente"}
                    For Each col In columnasOcultas
                        If dgvPedidosFacturar.Columns.Contains(col) Then
                            dgvPedidosFacturar.Columns(col).Visible = False
                        End If
                    Next

                    ' Preselección automática del pedido (flujo directo de cobro en caja)
                    _bloquearSelectionChanged = False
                    SeleccionarPedido(_idPedidoInicial)
                Else
                    _bloquearSelectionChanged = False
                End If

                If vista.Count = 0 Then
                    LimpiarFormulario()
                End If
            Catch ex As Exception
                _bloquearSelectionChanged = False
                MessageBox.Show($"Ocurrió un error al cargar las órdenes pagadas: {ex.Message}", "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Selecciona explícitamente una comanda en la tabla de facturación y carga sus datos.
        ''' </summary>
        Public Sub SeleccionarPedido(idPedido As Integer)
            If dgvPedidosFacturar Is Nothing OrElse dgvPedidosFacturar.Rows.Count = 0 Then Return

            Dim filaSeleccionar As DataGridViewRow = Nothing
            If idPedido > 0 Then
                For Each r As DataGridViewRow In dgvPedidosFacturar.Rows
                    Dim currentId As Integer = 0
                    Dim rv = TryCast(r.DataBoundItem, DataRowView)
                    If rv IsNot Nothing AndAlso rv.Row.Table.Columns.Contains("ID") Then
                        currentId = Convert.ToInt32(rv("ID"))
                    ElseIf r.Cells("ID").Value IsNot Nothing Then
                        currentId = Convert.ToInt32(r.Cells("ID").Value)
                    End If

                    If currentId = idPedido Then
                        filaSeleccionar = r
                        Exit For
                    End If
                Next
            End If

            If filaSeleccionar Is Nothing AndAlso dgvPedidosFacturar.Rows.Count > 0 Then
                filaSeleccionar = dgvPedidosFacturar.Rows(0)
            End If

            If filaSeleccionar IsNot Nothing Then
                _bloquearSelectionChanged = True
                Try
                    dgvPedidosFacturar.ClearSelection()
                    filaSeleccionar.Selected = True

                    For Each celda As DataGridViewCell In filaSeleccionar.Cells
                        If celda.Visible Then
                            dgvPedidosFacturar.CurrentCell = celda
                            Exit For
                        End If
                    Next
                Catch
                Finally
                    _bloquearSelectionChanged = False
                End Try

                Dim finalId As Integer = 0
                Dim rvSelected = TryCast(filaSeleccionar.DataBoundItem, DataRowView)
                If rvSelected IsNot Nothing AndAlso rvSelected.Row.Table.Columns.Contains("ID") Then
                    finalId = Convert.ToInt32(rvSelected("ID"))
                ElseIf filaSeleccionar.Cells("ID").Value IsNot Nothing Then
                    finalId = Convert.ToInt32(filaSeleccionar.Cells("ID").Value)
                End If

                If finalId > 0 Then
                    _idPedidoSeleccionado = finalId
                    _pedidoSeleccionadoRow = PedidoDAO.ObtenerPedidoPorId(_idPedidoSeleccionado)
                    If _pedidoSeleccionadoRow IsNot Nothing Then
                        CargarDatosEnControles(_pedidoSeleccionadoRow)
                        ActualizarTicketVisual()
                    End If
                End If

                ' Mover el foco a un campo de entrada para evitar que DataGridView fuerce el cursor a (0, 0)
                txtRucCedula.Focus()
            End If
        End Sub

        Private Sub btnVolverCaja_Click(sender As Object, e As EventArgs) Handles btnVolverCaja.Click
            Dim formHome = TryCast(Me.ParentForm, FrmHome)
            If formHome IsNot Nothing Then
                formHome.AbrirFormularioEnPanel(Of Caja.FrmCajaCobros)()
            End If
        End Sub

        Private Sub dgvPedidosFacturar_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPedidosFacturar.SelectionChanged
            If _bloquearSelectionChanged Then Return
            If dgvPedidosFacturar.SelectedRows.Count > 0 Then
                Dim row = dgvPedidosFacturar.SelectedRows(0)
                Dim idRow As Integer = 0
                Dim rv = TryCast(row.DataBoundItem, DataRowView)
                If rv IsNot Nothing AndAlso rv.Row.Table.Columns.Contains("ID") Then
                    idRow = Convert.ToInt32(rv("ID"))
                ElseIf row.Cells("ID").Value IsNot Nothing Then
                    idRow = Convert.ToInt32(row.Cells("ID").Value)
                End If

                If idRow > 0 Then
                    _idPedidoSeleccionado = idRow
                    _pedidoSeleccionadoRow = PedidoDAO.ObtenerPedidoPorId(_idPedidoSeleccionado)

                    If _pedidoSeleccionadoRow IsNot Nothing Then
                        CargarDatosEnControles(_pedidoSeleccionadoRow)
                        ActualizarTicketVisual()
                    End If
                End If
            End If
        End Sub

        Private Sub CargarDatosEnControles(row As DataRow)
            Dim cliente = row("Cliente").ToString()
            Dim ruc = If(row("RUC_Cedula") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(row("RUC_Cedula").ToString()), row("RUC_Cedula").ToString(), "8-800-1234")
            Dim razon = If(row("RazonSocial") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(row("RazonSocial").ToString()), row("RazonSocial").ToString(), cliente)
            Dim tel = If(row("TelefonoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(row("TelefonoCliente").ToString()), row("TelefonoCliente").ToString(), "+507 6200-1122")
            Dim correo = If(row("CorreoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(row("CorreoCliente").ToString()), row("CorreoCliente").ToString(), "cliente@restaurante.com")
            Dim dir = If(row("DireccionFiscal") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(row("DireccionFiscal").ToString()), row("DireccionFiscal").ToString(), "Ciudad de Panamá")

            txtRucCedula.Text = ruc
            txtRazonSocial.Text = razon
            txtTelefono.Text = tel
            txtCorreo.Text = correo
            txtDireccion.Text = dir

            Dim yaFacturado = CBool(row("Facturado"))
            If yaFacturado Then
                txtRucCedula.ReadOnly = True
                txtRazonSocial.ReadOnly = True
                txtDireccion.ReadOnly = True
                txtTelefono.ReadOnly = True
                txtCorreo.ReadOnly = True
                btnImprimir.Text = "🖨️ Reimprimir Recibo"
            Else
                txtRucCedula.ReadOnly = False
                txtRazonSocial.ReadOnly = False
                txtDireccion.ReadOnly = False
                txtTelefono.ReadOnly = False
                txtCorreo.ReadOnly = False
                btnImprimir.Text = "🖨️ Imprimir Recibo"
            End If
        End Sub

        Private Sub ActualizarTicketVisual()
            If _pedidoSeleccionadoRow Is Nothing Then
                lblTicketCorrelativo.Text = "RECIBO: (Sin Selección)"
                lblTicketFecha.Text = "Fecha de Emisión: --/--/----"
                lblTicketMetodo.Text = "Forma de Pago: --"
                lblTicketCliente.Text = "Cliente: Consumidor Final"
                lblTicketRucCliente.Text = "Cédula / RUC: --"
                lblTicketServicio.Text = "Servicio: --"
                lblTicketPlato.Text = "1 x (Pedido del Menú)"
                lblTicketAcomp.Text = "+ Acompañamientos"
                lblTicketPrecioPlato.Text = "Importe: $0.00"
                lblTicketSubtotal.Text = "Subtotal: $0.00"
                lblTicketImpuesto.Text = "Impuesto ITBMS (7%): $0.00"
                lblTicketTotal.Text = "TOTAL: $0.00"
                Return
            End If

            Dim yaFacturado = CBool(_pedidoSeleccionadoRow("Facturado"))
            Dim numFactura = If(yaFacturado AndAlso Not String.IsNullOrEmpty(_pedidoSeleccionadoRow("NumeroFactura").ToString()),
                                _pedidoSeleccionadoRow("NumeroFactura").ToString(),
                                PedidoDAO.ObtenerProximoNumeroFactura())

            Dim total As Decimal = Convert.ToDecimal(_pedidoSeleccionadoRow("Total"))
            Dim subtotal As Decimal = Math.Round(total / 1.07D, 2)
            Dim impuesto As Decimal = Math.Round(total - subtotal, 2)

            lblTicketCorrelativo.Text = $"RECIBO / FACTURA: {numFactura}"
            lblTicketFecha.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
            lblTicketMetodo.Text = $"Forma de Pago: {_pedidoSeleccionadoRow("MetodoPago")}"
            lblTicketCliente.Text = $"Cliente: {If(String.IsNullOrWhiteSpace(txtRazonSocial.Text), _pedidoSeleccionadoRow("Cliente").ToString(), txtRazonSocial.Text.Trim())}"
            lblTicketRucCliente.Text = $"Cédula / RUC: {If(String.IsNullOrWhiteSpace(txtRucCedula.Text), "Consumidor Final", txtRucCedula.Text.Trim())}"
            lblTicketServicio.Text = $"Servicio: {_pedidoSeleccionadoRow("TipoServicio")} • Mesa {_pedidoSeleccionadoRow("Mesa")}"
            lblTicketPlato.Text = $"1 x {_pedidoSeleccionadoRow("PlatoPrincipal")}"
            lblTicketAcomp.Text = $"+ {_pedidoSeleccionadoRow("Acompanamientos")}"
            lblTicketPrecioPlato.Text = $"Importe: ${total:N2}"
            lblTicketSubtotal.Text = $"Subtotal: ${subtotal:N2}"
            lblTicketImpuesto.Text = $"Impuesto ITBMS (7%): ${impuesto:N2}"
            lblTicketTotal.Text = $"TOTAL: ${total:N2}"
        End Sub

        Private Sub txtDatosFiscales_TextChanged(sender As Object, e As EventArgs) Handles txtRucCedula.TextChanged, txtRazonSocial.TextChanged
            If _pedidoSeleccionadoRow IsNot Nothing Then
                ActualizarTicketVisual()
            End If
        End Sub

        ''' <summary>
        ''' Asegura que el pedido tenga correlativo asignado y su archivo PDF generado en disco.
        ''' </summary>
        Private Function AsegurarEmisionFactura() As String
            Dim correlativo = PedidoDAO.RegistrarFactura(_idPedidoSeleccionado, txtRucCedula.Text.Trim(),
                                                        txtRazonSocial.Text.Trim(), txtDireccion.Text.Trim(),
                                                        txtTelefono.Text.Trim(), txtCorreo.Text.Trim())
            _pedidoSeleccionadoRow = PedidoDAO.ObtenerPedidoPorId(_idPedidoSeleccionado)

            Dim nombreArchivo = $"Factura_{correlativo}.pdf"
            Dim rutaArchivo = Path.Combine(_carpetaFacturas, nombreArchivo)
            FacturaPDFService.GenerarFacturaPDF(rutaArchivo, _pedidoSeleccionadoRow)
            Return rutaArchivo
        End Function

        ' =========================================================================
        ' 1. IMPRESIÓN FÍSICA / REAL A IMPRESORA (PrintDialog & PrintDocument)
        ' =========================================================================

        Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
            If Not ValidarFormulario() Then Return

            Try
                Dim rutaArchivo = AsegurarEmisionFactura()
                Dim correlativo = _pedidoSeleccionadoRow("NumeroFactura").ToString()

                ' Configurar y mostrar el cuadro de diálogo oficial de Windows para seleccionar impresora
                Using pd As New PrintDialog()
                    pd.Document = _printDocument
                    pd.UseEXDialog = True

                    If pd.ShowDialog(Me) = DialogResult.OK Then
                        _printDocument.Print()
                        MessageBox.Show($"🖨️ ¡Recibo '{correlativo}' enviado a la impresora con éxito!", "Impresión Completada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using

                CargarPedidosPagados()
                ActualizarTicketVisual()
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al enviar el trabajo a la impresora: {ex.Message}", "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Dibuja el comprobante fiscal térmico en el motor de impresión nativo de Windows (GDI+).
        ''' </summary>
        Private Sub _printDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles _printDocument.PrintPage
            If _pedidoSeleccionadoRow Is Nothing Then Return

            Dim g = e.Graphics
            Dim fuenteTitulo As New Font("Segoe UI", 11.0F, FontStyle.Bold)
            Dim fuenteSub As New Font("Segoe UI", 8.0F, FontStyle.Regular)
            Dim fuenteNegrita As New Font("Segoe UI", 8.5F, FontStyle.Bold)
            Dim fuenteCuerpo As New Font("Segoe UI", 8.0F, FontStyle.Regular)
            Dim fuenteGrande As New Font("Segoe UI", 12.0F, FontStyle.Bold)

            Dim brush As Brush = Brushes.Black
            Dim y As Single = 15.0F
            Dim x As Single = 20.0F
            Dim anchoTicket As Single = 260.0F

            ' Encabezado
            Dim sfCentrado As New StringFormat With {.Alignment = StringAlignment.Center}
            g.DrawString("RESTAURANTE EL BUEN SAZÓN", fuenteTitulo, brush, New RectangleF(x, y, anchoTicket, 20), sfCentrado)
            y += 20
            g.DrawString("Sabor Tradicional & Excelencia Gastronómica", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 14
            g.DrawString("RUC: 155698421-2-2024 DV 89  •  Tel: 223-9000", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 14
            g.DrawString("Ciudad de Panamá, Rep. de Panamá", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 18

            ' Separador
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6

            ' Datos Comprobante
            Dim numFactura = _pedidoSeleccionadoRow("NumeroFactura").ToString()
            g.DrawString($"RECIBO / FACTURA: {numFactura}", fuenteNegrita, brush, x, y)
            y += 16
            g.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"Forma de Pago: {_pedidoSeleccionadoRow("MetodoPago")}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"Servicio: {_pedidoSeleccionadoRow("TipoServicio")} • Mesa: {_pedidoSeleccionadoRow("Mesa")}", fuenteCuerpo, brush, x, y)
            y += 18

            ' Datos Cliente
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString($"Cliente: {txtRazonSocial.Text.Trim()}", fuenteNegrita, brush, x, y)
            y += 14
            g.DrawString($"Cédula / RUC: {txtRucCedula.Text.Trim()}", fuenteCuerpo, brush, x, y)
            y += 14
            If Not String.IsNullOrWhiteSpace(txtTelefono.Text) Then
                g.DrawString($"Tel: {txtTelefono.Text.Trim()}", fuenteCuerpo, brush, x, y)
                y += 14
            End If
            y += 4

            ' Detalle de Productos
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString("CANT  DESCRIPCIÓN                   TOTAL", fuenteNegrita, brush, x, y)
            y += 16
            g.DrawLine(Pens.LightGray, x, y, x + anchoTicket, y)
            y += 5

            Dim total = Convert.ToDecimal(_pedidoSeleccionadoRow("Total"))
            Dim subtotal = Math.Round(total / 1.07D, 2)
            Dim impuesto = Math.Round(total - subtotal, 2)

            g.DrawString($"1 x   {_pedidoSeleccionadoRow("PlatoPrincipal")}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"      ({_pedidoSeleccionadoRow("Acompanamientos")})", fuenteSub, brush, x, y)
            y += 14
            Dim sfDerecha As New StringFormat With {.Alignment = StringAlignment.Far}
            g.DrawString($"${total:N2}", fuenteNegrita, brush, New RectangleF(x, y - 28, anchoTicket, 16), sfDerecha)
            y += 6

            ' Totales
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString("Subtotal:", fuenteCuerpo, brush, x, y)
            g.DrawString($"${subtotal:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 16

            g.DrawString("Impuesto ITBMS (7%):", fuenteCuerpo, brush, x, y)
            g.DrawString($"${impuesto:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 16

            g.DrawString("TOTAL:", fuenteGrande, brush, x, y)
            g.DrawString($"${total:N2}", fuenteGrande, brush, New RectangleF(x, y, anchoTicket, 24), sfDerecha)
            y += 26

            ' Pie Legal
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 8
            g.DrawString("🟢 RECIBO / FACTURA REGISTRADA", fuenteNegrita, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 16
            g.DrawString("¡Muchas gracias por su preferencia!", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 14
            g.DrawString("Conserve este ticket para sus registros.", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)

            e.HasMorePages = False
        End Sub


        ' =========================================================================
        ' 3. ENVÍO REAL / PREPARACIÓN POR CORREO ELECTRÓNICO (RN-011)
        ' =========================================================================

        Private Sub btnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles btnEnviarCorreo.Click
            If Not ValidarFormulario() Then Return

            Dim correoCliente = txtCorreo.Text.Trim()
            If String.IsNullOrWhiteSpace(correoCliente) OrElse Not correoCliente.Contains("@") OrElse Not correoCliente.Contains(".") Then
                MessageBox.Show("Por favor, ingrese un correo electrónico válido para enviar la factura (ej: cliente@dominio.com).", "Correo Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCorreo.Focus()
                Return
            End If

            Try
                Dim rutaArchivo = AsegurarEmisionFactura()
                Dim correlativo = _pedidoSeleccionadoRow("NumeroFactura").ToString()
                Dim total = Convert.ToDecimal(_pedidoSeleccionadoRow("Total"))
                Dim nombreCliente = txtRazonSocial.Text.Trim()
                Dim nombrePdf = Path.GetFileName(rutaArchivo)

                ' 1. Redactar mensaje cortés y claro para el cliente
                Dim cuerpoTexto As String = $"Estimado(a) {nombreCliente}:{vbCrLf}{vbCrLf}" &
                                           $"Esperamos que haya disfrutado de su experiencia en Restaurante ""El Buen Sazón"".{vbCrLf}{vbCrLf}" &
                                           $"Le enviamos el recibo de compra correspondiente a su consumo:{vbCrLf}" &
                                           $"• Recibo / Factura N°: {correlativo}{vbCrLf}" &
                                           $"• Total: ${total:N2}{vbCrLf}" &
                                           $"• Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}{vbCrLf}{vbCrLf}" &
                                           $"En el archivo adjunto encontrará el documento PDF con el detalle de su pedido.{vbCrLf}{vbCrLf}" &
                                           $"Agradecemos sinceramente su preferencia y esperamos tener el placer de atenderle nuevamente pronto.{vbCrLf}{vbCrLf}" &
                                           $"Atentamente,{vbCrLf}" &
                                           $"Restaurante ""El Buen Sazón""{vbCrLf}" &
                                           $"Teléfono: (+507) 223-9000 | Ciudad de Panamá"

                ' 2. Construir archivo .EML estándar con el PDF adjunto (MIME multipart/mixed)
                Dim pdfBytes As Byte() = File.ReadAllBytes(rutaArchivo)
                Dim base64Pdf As String = Convert.ToBase64String(pdfBytes)

                Dim sbMimePdf As New System.Text.StringBuilder()
                For i As Integer = 0 To base64Pdf.Length - 1 Step 76
                    Dim longitud As Integer = Math.Min(76, base64Pdf.Length - i)
                    sbMimePdf.AppendLine(base64Pdf.Substring(i, longitud))
                Next

                Dim boundary = "----=_Part_" & Guid.NewGuid().ToString("N")
                Dim emlContent As New System.Text.StringBuilder()

                emlContent.AppendLine($"To: {correoCliente}")
                emlContent.AppendLine($"Subject: Recibo de Pago {correlativo} - Restaurante El Buen Sazón")
                emlContent.AppendLine("X-Unsent: 1")
                emlContent.AppendLine("MIME-Version: 1.0")
                emlContent.AppendLine($"Content-Type: multipart/mixed; boundary=""{boundary}""")
                emlContent.AppendLine()

                ' Parte 1: Mensaje de texto cortés
                emlContent.AppendLine($"--{boundary}")
                emlContent.AppendLine("Content-Type: text/plain; charset=""utf-8""")
                emlContent.AppendLine("Content-Transfer-Encoding: 8bit")
                emlContent.AppendLine()
                emlContent.AppendLine(cuerpoTexto)
                emlContent.AppendLine()

                ' Parte 2: Archivo PDF adjunto
                emlContent.AppendLine($"--{boundary}")
                emlContent.AppendLine($"Content-Type: application/pdf; name=""{nombrePdf}""")
                emlContent.AppendLine("Content-Transfer-Encoding: base64")
                emlContent.AppendLine($"Content-Disposition: attachment; filename=""{nombrePdf}""")
                emlContent.AppendLine()
                emlContent.Append(sbMimePdf.ToString())
                emlContent.AppendLine()
                emlContent.AppendLine($"--{boundary}--")

                Dim rutaEml = Path.Combine(_carpetaFacturas, $"Envio_{correlativo}.eml")
                File.WriteAllText(rutaEml, emlContent.ToString(), System.Text.Encoding.UTF8)

                ' Copiar el archivo como objeto al portapapeles para facilitar pegar el adjunto en cualquier webmail
                Try
                    Dim coleccionArchivos As New System.Collections.Specialized.StringCollection()
                    coleccionArchivos.Add(rutaArchivo)
                    Clipboard.SetFileDropList(coleccionArchivos)
                Catch exClipboard As Exception
                    ' Continuar si el portapapeles del sistema está ocupado
                End Try

                ' 3. Abrir la ventana de correo nativa con el mensaje y el PDF ya adjuntado
                Dim correoAbierto As Boolean = False
                Try
                    Dim psiEml As New ProcessStartInfo(rutaEml) With {
                        .UseShellExecute = True
                    }
                    Process.Start(psiEml)
                    correoAbierto = True
                Catch exEml As Exception
                    ' Fallback por mailto si no hay cliente .eml asociado
                    Dim asuntoMailto = Uri.EscapeDataString($"Recibo de Pago {correlativo} - Restaurante El Buen Sazón")
                    Dim cuerpoMailto = Uri.EscapeDataString(cuerpoTexto)
                    Dim mailtoUrl = $"mailto:{correoCliente}?subject={asuntoMailto}&body={cuerpoMailto}"
                    Try
                        Process.Start(New ProcessStartInfo(mailtoUrl) With {.UseShellExecute = True})
                    Catch exMailto As Exception
                        ' Continuar
                    End Try
                End Try

                ' 4. Diálogo amigable de confirmación para el usuario
                Dim msgConfirmacion As String = $"📧 ¡El recibo fue preparado con éxito!" & vbCrLf & vbCrLf &
                                                $"Se preparó el correo con la factura en PDF adjunta para:{vbCrLf}{correoCliente}"

                MessageBox.Show(msgConfirmacion, "Recibo preparado para envío", MessageBoxButtons.OK, MessageBoxIcon.Information)

                CargarPedidosPagados()
                ActualizarTicketVisual()
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al preparar el envío de la factura: {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ' =========================================================================
        ' 4. GUARDAR COPIA PDF EN DIRECTORIO ESPECÍFICO
        ' =========================================================================

        Private Sub btnGuardarComo_Click(sender As Object, e As EventArgs) Handles btnGuardarComo.Click
            If Not ValidarFormulario() Then Return

            Try
                AsegurarEmisionFactura()
                Dim correlativo = _pedidoSeleccionadoRow("NumeroFactura").ToString()

                Using sfd As New SaveFileDialog()
                    sfd.Filter = "Documentos PDF (*.pdf)|*.pdf"
                    sfd.FileName = $"Factura_{correlativo}.pdf"
                    sfd.Title = "Guardar Copia de Factura Fiscal PDF"

                    If sfd.ShowDialog(Me) = DialogResult.OK Then
                        FacturaPDFService.GenerarFacturaPDF(sfd.FileName, _pedidoSeleccionadoRow)
                        MessageBox.Show($"¡Archivo guardado exitosamente en:{vbCrLf}{sfd.FileName}!", "Factura Exportada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        AbrirArchivo(sfd.FileName)
                    End If
                End Using

                CargarPedidosPagados()
                ActualizarTicketVisual()
            Catch ex As Exception
                MessageBox.Show($"Error al exportar archivo: {ex.Message}", "Error al Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function ValidarFormulario() As Boolean
            If _idPedidoSeleccionado <= 0 OrElse _pedidoSeleccionadoRow Is Nothing Then
                MessageBox.Show("Por favor, seleccione un pedido cobrado de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If String.IsNullOrWhiteSpace(txtRucCedula.Text) Then
                MessageBox.Show("Por favor ingrese la cédula o RUC del cliente.", "Datos del Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRucCedula.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(txtRazonSocial.Text) Then
                MessageBox.Show("Por favor ingrese el nombre del cliente.", "Datos del Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRazonSocial.Focus()
                Return False
            End If

            Return True
        End Function

        Private Sub AbrirArchivo(ruta As String)
            Try
                Dim psi As New ProcessStartInfo(ruta) With {
                    .UseShellExecute = True
                }
                Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show($"No se pudo abrir automáticamente el visor de PDF: {ex.Message}{vbCrLf}El archivo se encuentra en: {ruta}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End Sub

        Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
            CargarPedidosPagados()
        End Sub

        Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
            txtBuscar.Clear()
            LimpiarFormulario()
            CargarPedidosPagados()
        End Sub

        Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
            LimpiarFormulario()
        End Sub

        Private Sub LimpiarFormulario()
            _idPedidoSeleccionado = 0
            _pedidoSeleccionadoRow = Nothing
            txtRucCedula.Clear()
            txtRazonSocial.Clear()
            txtTelefono.Clear()
            txtCorreo.Clear()
            txtDireccion.Clear()
            txtRucCedula.ReadOnly = False
            txtRazonSocial.ReadOnly = False
            txtDireccion.ReadOnly = False
            txtTelefono.ReadOnly = False
            txtCorreo.ReadOnly = False
            btnImprimir.Text = "🖨️ Imprimir Recibo"
            ActualizarTicketVisual()
            If dgvPedidosFacturar.SelectedRows.Count > 0 Then
                dgvPedidosFacturar.ClearSelection()
            End If
        End Sub

    End Class
End Namespace
