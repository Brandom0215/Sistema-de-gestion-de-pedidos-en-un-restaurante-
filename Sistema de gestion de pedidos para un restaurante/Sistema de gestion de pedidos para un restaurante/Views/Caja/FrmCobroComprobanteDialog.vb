Imports System
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Caja
    ''' <summary>
    ''' Diálogo modal inmediato de Emisión de Comprobante Fiscal tras el cobro en Caja (Flujo POS del Mundo Real).
    ''' Se enfoca exclusivamente en la comanda que se acaba de cobrar, permitiendo imprimir el ticket fiscal
    ''' o enviarlo por correo electrónico al cliente en segundos, sin intermediación de tablas ni navegación externa.
    ''' </summary>
    Public Class FrmCobroComprobanteDialog

        Private ReadOnly _idPedido As Integer
        Private _pedidoRow As DataRow
        Private ReadOnly _carpetaFacturas As String
        Private WithEvents _printDocument As New PrintDocument()

        Public Sub New(idPedido As Integer)
            InitializeComponent()
            _idPedido = idPedido
            _carpetaFacturas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasEmitidas")
            If Not Directory.Exists(_carpetaFacturas) Then
                Directory.CreateDirectory(_carpetaFacturas)
            End If
        End Sub

        Private Sub FrmCobroComprobanteDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarDatosPedido()
        End Sub

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlTop.BackColor = ThemeConfig.ColorBackgroundCard
            pnlContenedor.BackColor = ThemeConfig.ColorBackgroundApp

            lblTitulo.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtitulo.ForeColor = ThemeConfig.ColorTextMuted

            grpDatosFiscales.ForeColor = ThemeConfig.ColorSecondary
            grpVistaPrevia.ForeColor = ThemeConfig.ColorSecondary

            ThemeConfig.EstilizarBotonPrimario(btnImprimirTicket)
            ThemeConfig.EstilizarBotonSecundario(btnEnviarCorreo)
            ThemeConfig.EstilizarBotonSecundario(btnCerrar)

            pnlTicketVisual.BackColor = Color.White
        End Sub

        Private Sub CargarDatosPedido()
            _pedidoRow = PedidoDAO.ObtenerPedidoPorId(_idPedido)
            If _pedidoRow Is Nothing Then
                MessageBox.Show($"No se encontró la información de la comanda #{_idPedido}.", "Comanda No Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            lblTitulo.Text = $"🧾 Emisión de Comprobante Fiscal — Comanda #{_idPedido}"

            Dim cliente As String = _pedidoRow("Cliente").ToString()
            Dim ruc As String = If(_pedidoRow("RUC_Cedula") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("RUC_Cedula").ToString()), _pedidoRow("RUC_Cedula").ToString(), "8-800-1234")
            Dim razon As String = If(_pedidoRow("RazonSocial") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("RazonSocial").ToString()), _pedidoRow("RazonSocial").ToString(), cliente)
            Dim tel As String = If(_pedidoRow("TelefonoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("TelefonoCliente").ToString()), _pedidoRow("TelefonoCliente").ToString(), "+507 6200-1122")
            Dim correo As String = If(_pedidoRow("CorreoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("CorreoCliente").ToString()), _pedidoRow("CorreoCliente").ToString(), "cliente@restaurante.com")
            Dim dir As String = If(_pedidoRow("DireccionFiscal") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("DireccionFiscal").ToString()), _pedidoRow("DireccionFiscal").ToString(), "Ciudad de Panamá")

            txtRucCedula.Text = ruc
            txtRazonSocial.Text = razon
            txtTelefono.Text = tel
            txtCorreo.Text = correo
            txtDireccion.Text = dir

            Dim facturado As Boolean = (_pedidoRow("Facturado") IsNot DBNull.Value AndAlso CBool(_pedidoRow("Facturado")))
            If facturado Then
                txtRucCedula.ReadOnly = True
                txtRazonSocial.ReadOnly = True
                txtTelefono.ReadOnly = True
                txtCorreo.ReadOnly = True
                txtDireccion.ReadOnly = True
                btnImprimirTicket.Text = "🖨️ Re-Imprimir Ticket Fiscal"
            End If

            ActualizarTicketVisual()
        End Sub

        Private Sub ActualizarTicketVisual()
            If _pedidoRow Is Nothing Then Return

            Dim numFactura As String = If(_pedidoRow("NumeroFactura") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(_pedidoRow("NumeroFactura").ToString()),
                                          _pedidoRow("NumeroFactura").ToString(),
                                          PedidoDAO.ObtenerProximoNumeroFactura())

            lblTicketCorrelativo.Text = $"FACTURA FISCAL: {numFactura}"
            lblTicketFecha.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
            lblTicketRuc.Text = $"RUC / Cédula: {txtRucCedula.Text.Trim()}"
            lblTicketCliente.Text = $"Cliente: {txtRazonSocial.Text.Trim()}"
            lblTicketMetodo.Text = $"Forma de Pago: {_pedidoRow("MetodoPago")}"
            lblTicketMesa.Text = $"Servicio: {_pedidoRow("TipoServicio")} • {_pedidoRow("Mesa")}"

            Dim plato As String = _pedidoRow("PlatoPrincipal").ToString()
            Dim acomp As String = _pedidoRow("Acompanamientos").ToString()
            lblTicketPlato.Text = $"1 x {plato}"
            lblTicketAcomp.Text = If(String.IsNullOrWhiteSpace(acomp), "   Sin acompañamiento", $"   + {acomp}")

            Dim total As Decimal = Convert.ToDecimal(_pedidoRow("Total"))
            Dim subtotal As Decimal = If(_pedidoRow("Subtotal") IsNot DBNull.Value, Convert.ToDecimal(_pedidoRow("Subtotal")), Math.Round(total / 1.07D, 2))
            Dim impuesto As Decimal = If(_pedidoRow("Impuesto") IsNot DBNull.Value, Convert.ToDecimal(_pedidoRow("Impuesto")), Math.Round(total - subtotal, 2))
            Dim recibido As Decimal = If(_pedidoRow("MontoRecibido") IsNot DBNull.Value AndAlso Convert.ToDecimal(_pedidoRow("MontoRecibido")) > 0D, Convert.ToDecimal(_pedidoRow("MontoRecibido")), total)
            Dim cambio As Decimal = If(_pedidoRow("Cambio") IsNot DBNull.Value, Convert.ToDecimal(_pedidoRow("Cambio")), 0D)

            lblTicketSubtotal.Text = $"Subtotal Gravable: ${subtotal:N2}"
            lblTicketImpuesto.Text = $"ITBMS (7%): ${impuesto:N2}"
            lblTicketTotal.Text = $"TOTAL PAGADO: ${total:N2}"
            lblTicketRecibido.Text = $"Monto Recibido: ${recibido:N2}"
            lblTicketCambio.Text = $"Vuelto / Cambio: ${cambio:N2}"
        End Sub

        Private Sub txtDatos_TextChanged(sender As Object, e As EventArgs) Handles txtRucCedula.TextChanged, txtRazonSocial.TextChanged
            ActualizarTicketVisual()
        End Sub

        Private Function AsegurarEmisionFactura() As String
            Dim correlativo As String = PedidoDAO.RegistrarFactura(_idPedido, txtRucCedula.Text.Trim(),
                                                                  txtRazonSocial.Text.Trim(), txtDireccion.Text.Trim(),
                                                                  txtTelefono.Text.Trim(), txtCorreo.Text.Trim())
            _pedidoRow = PedidoDAO.ObtenerPedidoPorId(_idPedido)

            Dim nombreArchivo = $"Factura_{correlativo}.pdf"
            Dim rutaArchivo = Path.Combine(_carpetaFacturas, nombreArchivo)
            FacturaPDFService.GenerarFacturaPDF(rutaArchivo, _pedidoRow)
            Return rutaArchivo
        End Function

        Private Sub btnImprimirTicket_Click(sender As Object, e As EventArgs) Handles btnImprimirTicket.Click
            If String.IsNullOrWhiteSpace(txtRucCedula.Text) OrElse String.IsNullOrWhiteSpace(txtRazonSocial.Text) Then
                MessageBox.Show("Por favor indique al menos el RUC/Cédula y Nombre/Razón Social del cliente.", "Datos Fiscales Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                AsegurarEmisionFactura()
                ActualizarTicketVisual()

                Using pd As New PrintDialog()
                    pd.Document = _printDocument
                    pd.UseEXDialog = True

                    If pd.ShowDialog(Me) = DialogResult.OK Then
                        _printDocument.Print()
                        Dim correlativo = _pedidoRow("NumeroFactura").ToString()
                        MessageBox.Show($"🖨️ ¡Comprobante '{correlativo}' enviado exitosamente a la impresora '{_printDocument.PrinterSettings.PrinterName}'!", "Impresión Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al enviar a la impresora: {ex.Message}", "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub _printDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles _printDocument.PrintPage
            If _pedidoRow Is Nothing Then Return

            Dim g = e.Graphics
            Dim fuenteTitulo As New Font("Segoe UI", 11.0F, FontStyle.Bold)
            Dim fuenteSub As New Font("Segoe UI", 8.0F, FontStyle.Regular)
            Dim fuenteNegrita As New Font("Segoe UI", 8.5F, FontStyle.Bold)
            Dim fuenteCuerpo As New Font("Segoe UI", 8.0F, FontStyle.Regular)

            Dim brush As Brush = Brushes.Black
            Dim y As Single = 15.0F
            Dim x As Single = 20.0F
            Dim anchoTicket As Single = 260.0F

            Dim sfCentrado As New StringFormat With {.Alignment = StringAlignment.Center}
            g.DrawString("RESTAURANTE EL BUEN SAZÓN", fuenteTitulo, brush, New RectangleF(x, y, anchoTicket, 20), sfCentrado)
            y += 20
            g.DrawString("Sabor Tradicional & Excelencia Gastronómica", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 14
            g.DrawString("RUC: 155698421-2-2024 DV 89  •  Tel: 223-9000", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 14
            g.DrawString("Ciudad de Panamá, Rep. de Panamá", fuenteSub, brush, New RectangleF(x, y, anchoTicket, 15), sfCentrado)
            y += 18

            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6

            Dim numFactura = _pedidoRow("NumeroFactura").ToString()
            g.DrawString($"FACTURA FISCAL: {numFactura}", fuenteNegrita, brush, x, y)
            y += 16
            g.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"Método de Pago: {_pedidoRow("MetodoPago")}", fuenteCuerpo, brush, x, y)
            y += 14
            g.DrawString($"Servicio: {_pedidoRow("TipoServicio")} • {_pedidoRow("Mesa")}", fuenteCuerpo, brush, x, y)
            y += 18

            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString($"Cliente: {txtRazonSocial.Text.Trim()}", fuenteNegrita, brush, x, y)
            y += 14
            g.DrawString($"RUC/Cédula: {txtRucCedula.Text.Trim()}", fuenteCuerpo, brush, x, y)
            y += 18

            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString("CANT  DESCRIPCIÓN                   TOTAL", fuenteNegrita, brush, x, y)
            y += 16
            g.DrawLine(Pens.LightGray, x, y, x + anchoTicket, y)
            y += 5

            Dim total = Convert.ToDecimal(_pedidoRow("Total"))
            Dim subtotal = If(_pedidoRow("Subtotal") IsNot DBNull.Value, Convert.ToDecimal(_pedidoRow("Subtotal")), Math.Round(total / 1.07D, 2))
            Dim impuesto = If(_pedidoRow("Impuesto") IsNot DBNull.Value, Convert.ToDecimal(_pedidoRow("Impuesto")), Math.Round(total - subtotal, 2))

            g.DrawString($"1 x   {_pedidoRow("PlatoPrincipal")}", fuenteCuerpo, brush, x, y)
            y += 14
            Dim sfDerecha As New StringFormat With {.Alignment = StringAlignment.Far}
            g.DrawString($"${total:N2}", fuenteNegrita, brush, New RectangleF(x, y - 14, anchoTicket, 16), sfDerecha)

            Dim acomp = _pedidoRow("Acompanamientos").ToString()
            If Not String.IsNullOrWhiteSpace(acomp) Then
                g.DrawString($"      ({acomp})", fuenteSub, brush, x, y)
                y += 14
            End If
            y += 8

            g.DrawLine(Pens.Gray, x, y, x + anchoTicket, y)
            y += 6
            g.DrawString($"Subtotal Gravable:", fuenteCuerpo, brush, x, y)
            g.DrawString($"${subtotal:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 14

            g.DrawString($"ITBMS (7%):", fuenteCuerpo, brush, x, y)
            g.DrawString($"${impuesto:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 16

            g.DrawString($"TOTAL PAGADO:", fuenteNegrita, brush, x, y)
            g.DrawString($"${total:N2}", fuenteNegrita, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 24

            Dim sfCentroPie As New StringFormat With {.Alignment = StringAlignment.Center}
            g.DrawString("¡Gracias por su visita al Buen Sazón!", fuenteNegrita, brush, New RectangleF(x, y, anchoTicket, 16), sfCentroPie)
            e.HasMorePages = False
        End Sub

        Private Sub btnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles btnEnviarCorreo.Click
            Dim correoDestino = txtCorreo.Text.Trim()
            If String.IsNullOrWhiteSpace(correoDestino) OrElse Not correoDestino.Contains("@") Then
                MessageBox.Show("Por favor indique una dirección de correo electrónico válida para el cliente.", "Correo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCorreo.Focus()
                Return
            End If

            Try
                Dim rutaArchivo = AsegurarEmisionFactura()
                ActualizarTicketVisual()

                Dim correlativo = _pedidoRow("NumeroFactura").ToString()
                Dim cliente = txtRazonSocial.Text.Trim()
                Dim total = Convert.ToDecimal(_pedidoRow("Total"))
                Dim nombrePdf = Path.GetFileName(rutaArchivo)

                ' 1. Redactar mensaje cortés y formal para el cliente
                Dim cuerpoTexto As String = $"Estimado(a) {cliente}:{vbCrLf}{vbCrLf}" &
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

                emlContent.AppendLine($"To: {correoDestino}")
                emlContent.AppendLine($"Subject: Factura Fiscal Digital {correlativo} - Restaurante El Buen Sazón")
                emlContent.AppendLine("X-Unsent: 1")
                emlContent.AppendLine("MIME-Version: 1.0")
                emlContent.AppendLine($"Content-Type: multipart/mixed; boundary=""{boundary}""")
                emlContent.AppendLine()

                ' Parte 1: Mensaje de texto
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
                End Try

                ' 3. Abrir la ventana de correo nativa con el mensaje y el PDF ya adjuntado
                Try
                    Dim psiEml As New ProcessStartInfo(rutaEml) With {.UseShellExecute = True}
                    Process.Start(psiEml)
                Catch exEml As Exception
                    ' Fallback por mailto
                    Dim asuntoMailto = Uri.EscapeDataString($"Factura Fiscal Digital {correlativo} - Restaurante El Buen Sazón")
                    Dim cuerpoMailto = Uri.EscapeDataString(cuerpoTexto)
                    Dim mailtoUrl = $"mailto:{correoDestino}?subject={asuntoMailto}&body={cuerpoMailto}"
                    Try
                        Process.Start(New ProcessStartInfo(mailtoUrl) With {.UseShellExecute = True})
                    Catch
                    End Try
                End Try

                ' 4. Diálogo amigable con el usuario sin exponer rutas técnicas
                Dim msgConfirmacion As String = $"📧 ¡Factura preparada exitosamente para el cliente!" & vbCrLf & vbCrLf &
                                                $"• Se preparó el correo cortés con la factura adjunta para: {correoDestino}" & vbCrLf &
                                                $"• El archivo PDF se guardó de forma segura en la carpeta predeterminada del sistema." & vbCrLf & vbCrLf &
                                                "¿Desea abrir la carpeta predeterminada para verificar el archivo PDF de la factura?"

                If MessageBox.Show(msgConfirmacion, "Factura Lista para Envío", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    If File.Exists(rutaArchivo) Then
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo("explorer.exe", $"/select,""{rutaArchivo}""") With {.UseShellExecute = True})
                    Else
                        System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo("explorer.exe", _carpetaFacturas) With {.UseShellExecute = True})
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al preparar el correo electrónico: {ex.Message}", "Error al Enviar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If keyData = Keys.Escape Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function

    End Class
End Namespace
