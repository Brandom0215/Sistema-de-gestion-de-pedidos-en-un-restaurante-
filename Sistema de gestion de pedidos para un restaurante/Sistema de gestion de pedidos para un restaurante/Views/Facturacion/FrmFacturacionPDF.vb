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

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
            _carpetaFacturas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasEmitidas")
            If Not Directory.Exists(_carpetaFacturas) Then
                Directory.CreateDirectory(_carpetaFacturas)
            End If
        End Sub

        Private Sub FrmFacturacionPDF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarPedidosPagados()
        End Sub

        ''' <summary>
        ''' Aplica la paleta visual oficial y fuentes modernas a todos los controles.
        ''' </summary>
        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
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
                Dim vista As New DataView(dtPagados)

                If Not String.IsNullOrWhiteSpace(txtBuscar.Text) Then
                    Dim criterio As String = txtBuscar.Text.Trim().Replace("'", "''")
                    Dim idNum As Integer
                    If Integer.TryParse(criterio, idNum) Then
                        vista.RowFilter = $"ID = {idNum} OR Cliente LIKE '%{criterio}%' OR NumeroFactura LIKE '%{criterio}%'"
                    Else
                        vista.RowFilter = $"Cliente LIKE '%{criterio}%' OR NumeroFactura LIKE '%{criterio}%' OR Mesa LIKE '%{criterio}%'"
                    End If
                End If

                dgvPedidosFacturar.DataSource = vista

                If dgvPedidosFacturar.Columns.Count > 0 Then
                    If dgvPedidosFacturar.Columns.Contains("ID") Then dgvPedidosFacturar.Columns("ID").Width = 45
                    If dgvPedidosFacturar.Columns.Contains("Cliente") Then dgvPedidosFacturar.Columns("Cliente").Width = 130
                    If dgvPedidosFacturar.Columns.Contains("Mesa") Then dgvPedidosFacturar.Columns("Mesa").Width = 60
                    If dgvPedidosFacturar.Columns.Contains("PlatoPrincipal") Then dgvPedidosFacturar.Columns("PlatoPrincipal").Width = 140
                    If dgvPedidosFacturar.Columns.Contains("Total") Then
                        dgvPedidosFacturar.Columns("Total").DefaultCellStyle.Format = "C2"
                        dgvPedidosFacturar.Columns("Total").Width = 70
                    End If
                    If dgvPedidosFacturar.Columns.Contains("NumeroFactura") Then
                        dgvPedidosFacturar.Columns("NumeroFactura").HeaderText = "No. Factura"
                        dgvPedidosFacturar.Columns("NumeroFactura").Width = 100
                    End If

                    ' Ocultar columnas accesorias
                    Dim columnasOcultas = {"Acompanamientos", "TipoServicio", "FechaHora", "PrecioUnitario", "Subtotal",
                                           "Impuesto", "Estado", "MetodoPago", "MontoRecibido", "Cambio", "Facturado",
                                           "RUC_Cedula", "RazonSocial", "DireccionFiscal", "TelefonoCliente", "CorreoCliente"}
                    For Each col In columnasOcultas
                        If dgvPedidosFacturar.Columns.Contains(col) Then
                            dgvPedidosFacturar.Columns(col).Visible = False
                        End If
                    Next
                End If

                If vista.Count = 0 Then
                    LimpiarFormulario()
                End If
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al cargar las órdenes pagadas: {ex.Message}", "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub dgvPedidosFacturar_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPedidosFacturar.SelectionChanged
            If dgvPedidosFacturar.SelectedRows.Count > 0 Then
                Dim row = dgvPedidosFacturar.SelectedRows(0)
                _idPedidoSeleccionado = Convert.ToInt32(row.Cells("ID").Value)
                _pedidoSeleccionadoRow = PedidoDAO.ObtenerPedidoPorId(_idPedidoSeleccionado)

                If _pedidoSeleccionadoRow IsNot Nothing Then
                    CargarDatosEnControles(_pedidoSeleccionadoRow)
                    ActualizarTicketVisual()
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
                ' Inalterabilidad de Comprobante (RN-010)
                txtRucCedula.ReadOnly = True
                txtRazonSocial.ReadOnly = True
                txtDireccion.ReadOnly = True
                txtTelefono.ReadOnly = True
                txtCorreo.ReadOnly = True
                btnImprimir.Text = "🖨️ Re-Imprimir Ticket"
            Else
                txtRucCedula.ReadOnly = False
                txtRazonSocial.ReadOnly = False
                txtDireccion.ReadOnly = False
                txtTelefono.ReadOnly = False
                txtCorreo.ReadOnly = False
                btnImprimir.Text = "🖨️ Imprimir Ticket"
            End If
        End Sub

        Private Sub ActualizarTicketVisual()
            If _pedidoSeleccionadoRow Is Nothing Then
                lblTicketCorrelativo.Text = "FACTURA: (Sin Selección)"
                lblTicketFecha.Text = "Fecha de Emisión: --/--/----"
                lblTicketMetodo.Text = "Método de Pago: --"
                lblTicketCliente.Text = "Cliente: Consumidor Final"
                lblTicketRucCliente.Text = "RUC / Cédula: --"
                lblTicketServicio.Text = "Servicio: --"
                lblTicketPlato.Text = "1 x (Plato del Menú)"
                lblTicketAcomp.Text = "+ Acompañamientos"
                lblTicketPrecioPlato.Text = "Importe: $0.00"
                lblTicketSubtotal.Text = "Subtotal Gravable: $0.00"
                lblTicketImpuesto.Text = "ITBMS (7%): $0.00"
                lblTicketTotal.Text = "TOTAL PAGADO: $0.00"
                Return
            End If

            Dim yaFacturado = CBool(_pedidoSeleccionadoRow("Facturado"))
            Dim numFactura = If(yaFacturado AndAlso Not String.IsNullOrEmpty(_pedidoSeleccionadoRow("NumeroFactura").ToString()),
                                _pedidoSeleccionadoRow("NumeroFactura").ToString(),
                                PedidoDAO.ObtenerProximoNumeroFactura())

            Dim total As Decimal = Convert.ToDecimal(_pedidoSeleccionadoRow("Total"))
            Dim subtotal As Decimal = Math.Round(total / 1.07D, 2)
            Dim impuesto As Decimal = Math.Round(total - subtotal, 2)

            lblTicketCorrelativo.Text = $"FACTURA: {numFactura}"
            lblTicketFecha.Text = $"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
            lblTicketMetodo.Text = $"Método de Pago: {_pedidoSeleccionadoRow("MetodoPago")}"
            lblTicketCliente.Text = $"Cliente: {If(String.IsNullOrWhiteSpace(txtRazonSocial.Text), _pedidoSeleccionadoRow("Cliente").ToString(), txtRazonSocial.Text.Trim())}"
            lblTicketRucCliente.Text = $"RUC / Cédula: {If(String.IsNullOrWhiteSpace(txtRucCedula.Text), "Consumidor Final", txtRucCedula.Text.Trim())}"
            lblTicketServicio.Text = $"Servicio: {_pedidoSeleccionadoRow("TipoServicio")} • Mesa {_pedidoSeleccionadoRow("Mesa")}"
            lblTicketPlato.Text = $"1 x {_pedidoSeleccionadoRow("PlatoPrincipal")}"
            lblTicketAcomp.Text = $"+ {_pedidoSeleccionadoRow("Acompanamientos")}"
            lblTicketPrecioPlato.Text = $"Importe: ${total:N2}"
            lblTicketSubtotal.Text = $"Subtotal Gravable: ${subtotal:N2}"
            lblTicketImpuesto.Text = $"ITBMS (7%): ${impuesto:N2}"
            lblTicketTotal.Text = $"TOTAL PAGADO: ${total:N2}"
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
                        MessageBox.Show($"🖨️ ¡Comprobante '{correlativo}' enviado exitosamente a la impresora '{_printDocument.PrinterSettings.PrinterName}'!", "Impresión Completada", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            g.DrawString($"FACTURA FISCAL: {numFactura}", fuenteNegrita, brush, x, y)
            y += 16
            g.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"Método de Pago: {_pedidoSeleccionadoRow("MetodoPago")}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"Servicio: {_pedidoSeleccionadoRow("TipoServicio")} • Mesa: {_pedidoSeleccionadoRow("Mesa")}", fuenteCuerpo, brush, x, y)
            y += 18

            ' Datos Cliente
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString($"Cliente: {txtRazonSocial.Text.Trim()}", fuenteNegrita, brush, x, y)
            y += 14
            g.DrawString($"RUC/Cédula: {txtRucCedula.Text.Trim()}", fuenteCuerpo, brush, x, y)
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
            g.DrawString("Subtotal Gravable:", fuenteCuerpo, brush, x, y)
            g.DrawString($"${subtotal:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 16

            g.DrawString("ITBMS (7%):", fuenteCuerpo, brush, x, y)
            g.DrawString($"${impuesto:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 16

            g.DrawString("TOTAL A PAGAR:", fuenteGrande, brush, x, y)
            g.DrawString($"${total:N2}", fuenteGrande, brush, New RectangleF(x, y, anchoTicket, 24), sfDerecha)
            y += 26

            ' Pie Legal
            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 8
            g.DrawString("🟢 COMPROBANTE FISCAL DIGITAL VALIDO", fuenteNegrita, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
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

                ' 1. Redactar mensaje cortés y formal para el cliente
                Dim cuerpoTexto As String = $"Estimado(a) {nombreCliente}:{vbCrLf}{vbCrLf}" &
                                           $"Esperamos que haya disfrutado de su experiencia en Restaurante ""El Buen Sazón"".{vbCrLf}{vbCrLf}" &
                                           $"Le hacemos entrega formal de su comprobante fiscal correspondiente a su consumo:{vbCrLf}" &
                                           $"• Factura N°: {correlativo}{vbCrLf}" &
                                           $"• Total Pagado: ${total:N2}{vbCrLf}" &
                                           $"• Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm:ss}{vbCrLf}{vbCrLf}" &
                                           $"En el archivo adjunto encontrará el documento PDF oficial con el desglose de su orden e impuestos.{vbCrLf}{vbCrLf}" &
                                           $"Agradecemos sinceramente su preferencia y esperamos tener el placer de atenderle nuevamente muy pronto.{vbCrLf}{vbCrLf}" &
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
                emlContent.AppendLine($"Subject: Factura Fiscal Digital {correlativo} - Restaurante El Buen Sazón")
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
                    Dim asuntoMailto = Uri.EscapeDataString($"Factura Fiscal Digital {correlativo} - Restaurante El Buen Sazón")
                    Dim cuerpoMailto = Uri.EscapeDataString(cuerpoTexto)
                    Dim mailtoUrl = $"mailto:{correoCliente}?subject={asuntoMailto}&body={cuerpoMailto}"
                    Try
                        Process.Start(New ProcessStartInfo(mailtoUrl) With {.UseShellExecute = True})
                    Catch exMailto As Exception
                        ' Continuar
                    End Try
                End Try

                ' 4. Diálogo amigable con el usuario sin exponer rutas técnicas
                Dim msgConfirmacion As String = $"📧 ¡Factura preparada exitosamente para el cliente!" & vbCrLf & vbCrLf &
                                                $"• Se preparó el correo cortés con la factura adjunta para: {correoCliente}" & vbCrLf &
                                                $"• El archivo PDF se guardó de forma segura en la carpeta predeterminada del sistema." & vbCrLf & vbCrLf &
                                                "¿Desea abrir la carpeta predeterminada para verificar el archivo PDF de la factura?"

                Dim respuesta = MessageBox.Show(msgConfirmacion, "Factura Lista para Envío", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If respuesta = DialogResult.Yes Then
                    Process.Start("explorer.exe", $"/select,""{rutaArchivo}""")
                End If

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
                MessageBox.Show("Por favor, seleccione una orden cobrada de la lista para emitir o imprimir la factura.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            ' Requisito RN-009: RUC/Cédula y Razón Social obligatorios para factura fiscal
            If String.IsNullOrWhiteSpace(txtRucCedula.Text) Then
                MessageBox.Show("El RUC o Cédula es un campo fiscal obligatorio (RN-009). Ingrese un valor válido.", "Validación Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRucCedula.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(txtRazonSocial.Text) Then
                MessageBox.Show("El Nombre o Razón Social es obligatorio para emitir la factura.", "Validación Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            btnImprimir.Text = "🖨️ Imprimir Ticket"
            ActualizarTicketVisual()
            If dgvPedidosFacturar.SelectedRows.Count > 0 Then
                dgvPedidosFacturar.ClearSelection()
            End If
        End Sub

    End Class
End Namespace
