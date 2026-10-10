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
            ConfigurarValidacionesEntrada()
            CargarDatosPedido()
        End Sub

        ''' <summary>
        ''' Configura validaciones en tiempo real y límites de caracteres según las columnas de la BD.
        ''' </summary>
        Private Sub ConfigurarValidacionesEntrada()
            ValidadorEntrada.ConfigurarCampoRucCedula(txtRucCedula, 30)
            ValidadorEntrada.ConfigurarCampoRazonSocial(txtRazonSocial, 100)
            ValidadorEntrada.ConfigurarCampoTelefono(txtTelefono, 30)
            ValidadorEntrada.ConfigurarCampoCorreo(txtCorreo, 100)
            ValidadorEntrada.ConfigurarCampoDireccion(txtDireccion, 150)
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
                MessageBox.Show($"No se encontró la información del pedido #{_idPedido}.", "Pedido No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                Return
            End If

            lblTitulo.Text = $"🧾 Recibo de Pago — Pedido #{_idPedido}"

            Dim cliente As String = _pedidoRow("Cliente").ToString()
            Dim ruc As String = If(_pedidoRow("RUC_Cedula") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("RUC_Cedula").ToString()), _pedidoRow("RUC_Cedula").ToString(), "8-800-1234")
            Dim razon As String = If(_pedidoRow("RazonSocial") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("RazonSocial").ToString()), _pedidoRow("RazonSocial").ToString(), cliente)
            Dim tel As String = If(_pedidoRow("TelefonoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("TelefonoCliente").ToString()), _pedidoRow("TelefonoCliente").ToString(), "+507 6200-1122")
            Dim correo As String = If(_pedidoRow("CorreoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("CorreoCliente").ToString()), _pedidoRow("CorreoCliente").ToString(), "cliente@restaurante.com")
            Dim dir As String = If(_pedidoRow("DireccionFiscal") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(_pedidoRow("DireccionFiscal").ToString()), _pedidoRow("DireccionFiscal").ToString(), "Ciudad de Panamá")

            ' Garantizar que el nombre cargado por defecto no contenga dígitos (ej: "Mesa 01")
            If Not ValidadorEntrada.EsNombreClienteValido(razon) Then
                Dim razonSinDigitos = New String(razon.Where(Function(c) Not Char.IsDigit(c)).ToArray()).Trim()
                If ValidadorEntrada.EsNombreClienteValido(razonSinDigitos) Then
                    razon = razonSinDigitos
                Else
                    razon = "Consumidor Final"
                End If
            End If

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
                btnImprimirTicket.Text = "🖨️ Reimprimir Recibo"
            End If

            ActualizarTicketVisual()
        End Sub

        Private Sub ActualizarTicketVisual()
            If _pedidoRow Is Nothing Then Return

            Dim numFactura As String = If(_pedidoRow("NumeroFactura") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(_pedidoRow("NumeroFactura").ToString()),
                                          _pedidoRow("NumeroFactura").ToString(),
                                          PedidoDAO.ObtenerProximoNumeroFactura())

            lblTicketCorrelativo.Text = $"RECIBO / FACTURA: {numFactura}"
            lblTicketFecha.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
            lblTicketRuc.Text = $"Cédula / RUC: {txtRucCedula.Text.Trim()}"
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

            lblTicketSubtotal.Text = $"Subtotal: ${subtotal:N2}"
            lblTicketImpuesto.Text = $"Impuesto ITBMS (7%): ${impuesto:N2}"
            lblTicketTotal.Text = $"TOTAL: ${total:N2}"
            lblTicketRecibido.Text = $"Dinero recibido: ${recibido:N2}"
            lblTicketCambio.Text = $"Cambio / Vuelto: ${cambio:N2}"
        End Sub

        Private Sub txtDatos_TextChanged(sender As Object, e As EventArgs) Handles txtRucCedula.TextChanged, txtRazonSocial.TextChanged
            ActualizarTicketVisual()
        End Sub

        Private Sub txtRucCedula_Leave(sender As Object, e As EventArgs) Handles txtRucCedula.Leave
            Dim ruc = txtRucCedula.Text.Trim()
            If ruc.Length >= 3 Then
                Dim clienteExistente = ClienteFiscalDAO.BuscarPorRuc(ruc)
                If clienteExistente IsNot Nothing Then
                    txtRazonSocial.Text = clienteExistente.RazonSocial
                    If Not String.IsNullOrWhiteSpace(clienteExistente.Telefono) Then txtTelefono.Text = clienteExistente.Telefono
                    If Not String.IsNullOrWhiteSpace(clienteExistente.Correo) Then txtCorreo.Text = clienteExistente.Correo
                    If Not String.IsNullOrWhiteSpace(clienteExistente.DireccionFiscal) Then txtDireccion.Text = clienteExistente.DireccionFiscal
                    ActualizarTicketVisual()
                End If
            End If
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

        ''' <summary>
        ''' Realiza validaciones lógicas de los datos del cliente antes de emitir o despachar el comprobante.
        ''' </summary>
        Private Function ValidarDatosFiscales(requiereCorreo As Boolean) As Boolean
            If Not ValidadorEntrada.EsRucCedulaValida(txtRucCedula.Text) Then
                MessageBox.Show("Por favor indique una cédula o RUC válido (mínimo 3 caracteres, ej: 8-800-1234).", "Cédula / RUC Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRucCedula.Focus()
                Return False
            End If

            If Not ValidadorEntrada.EsRazonSocialValida(txtRazonSocial.Text) Then
                MessageBox.Show("Por favor indique un nombre o razón social válida (entre 2 y 100 caracteres).", "Nombre / Razón Social Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRazonSocial.Focus()
                Return False
            End If

            If Not String.IsNullOrWhiteSpace(txtTelefono.Text) AndAlso Not ValidadorEntrada.EsTelefonoValido(txtTelefono.Text, True) Then
                MessageBox.Show("El teléfono ingresado no tiene un formato válido (debe contener entre 7 y 15 dígitos numéricos).", "Teléfono Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtTelefono.Focus()
                Return False
            End If

            If requiereCorreo OrElse Not String.IsNullOrWhiteSpace(txtCorreo.Text) Then
                If Not ValidadorEntrada.EsCorreoValido(txtCorreo.Text) Then
                    MessageBox.Show("El correo electrónico ingresado no es válido. Por favor verifique el formato ingresado (ej: cliente@gmail.com).", "Correo Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtCorreo.Focus()
                    Return False
                End If
            End If

            If Not ValidadorEntrada.EsDireccionValida(txtDireccion.Text, True) Then
                MessageBox.Show("La dirección ingresada supera el límite permitido (máximo 150 caracteres).", "Dirección Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtDireccion.Focus()
                Return False
            End If

            Return True
        End Function

        Private Sub btnImprimirTicket_Click(sender As Object, e As EventArgs) Handles btnImprimirTicket.Click
            If Not ValidarDatosFiscales(False) Then Return

            Try
                AsegurarEmisionFactura()
                ActualizarTicketVisual()

                Using pd As New PrintDialog()
                    pd.Document = _printDocument
                    pd.UseEXDialog = True

                    If pd.ShowDialog(Me) = DialogResult.OK Then
                        _printDocument.Print()
                        Dim correlativo = _pedidoRow("NumeroFactura").ToString()
                        MessageBox.Show($"🖨️ ¡Recibo '{correlativo}' enviado a la impresora con éxito!", "Impresión Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            g.DrawString($"RECIBO / FACTURA: {numFactura}", fuenteNegrita, brush, x, y)
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
            g.DrawString($"Cédula / RUC: {txtRucCedula.Text.Trim()}", fuenteCuerpo, brush, x, y)
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
            g.DrawString($"Subtotal:", fuenteCuerpo, brush, x, y)
            g.DrawString($"${subtotal:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 14

            g.DrawString($"Impuesto ITBMS (7%):", fuenteCuerpo, brush, x, y)
            g.DrawString($"${impuesto:N2}", fuenteCuerpo, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 16

            g.DrawString($"TOTAL:", fuenteNegrita, brush, x, y)
            g.DrawString($"${total:N2}", fuenteNegrita, brush, New RectangleF(x, y, anchoTicket, 16), sfDerecha)
            y += 24

            Dim sfCentroPie As New StringFormat With {.Alignment = StringAlignment.Center}
            g.DrawString("¡Gracias por su visita al Buen Sazón!", fuenteNegrita, brush, New RectangleF(x, y, anchoTicket, 16), sfCentroPie)
            e.HasMorePages = False
        End Sub

        Private Sub btnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles btnEnviarCorreo.Click
            If Not ValidarDatosFiscales(True) Then Return

            Dim correoDestino = txtCorreo.Text.Trim()

            Try
                Dim rutaArchivo = AsegurarEmisionFactura()
                ActualizarTicketVisual()

                Dim correlativo = _pedidoRow("NumeroFactura").ToString()
                Dim cliente = txtRazonSocial.Text.Trim()
                Dim total = Convert.ToDecimal(_pedidoRow("Total"))
                Dim nombrePdf = Path.GetFileName(rutaArchivo)

                ' 1. Redactar mensaje cortés y claro para el cliente
                Dim cuerpoTexto As String = $"Estimado(a) {cliente}:{vbCrLf}{vbCrLf}" &
                                           $"Esperamos que haya disfrutado de su experiencia en Restaurante ""El Buen Sazón"".{vbCrLf}{vbCrLf}" &
                                           $"Le enviamos el recibo de compra correspondiente a su consumo:{vbCrLf}" &
                                           $"• Recibo / Factura N°: {correlativo}{vbCrLf}" &
                                           $"• Total: ${total:N2}{vbCrLf}" &
                                           $"• Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}{vbCrLf}{vbCrLf}" &
                                           $"En el archivo adjunto encontrará su documento en PDF con el detalle de su pedido.{vbCrLf}{vbCrLf}" &
                                           $"¡Gracias por su visita y preferencia! Esperamos atenderle nuevamente pronto.{vbCrLf}{vbCrLf}" &
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
                emlContent.AppendLine($"Subject: Recibo de Pago {correlativo} - Restaurante El Buen Sazón")
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
                    Dim asuntoMailto = Uri.EscapeDataString($"Recibo de Pago {correlativo} - Restaurante El Buen Sazón")
                    Dim cuerpoMailto = Uri.EscapeDataString(cuerpoTexto)
                    Dim mailtoUrl = $"mailto:{correoDestino}?subject={asuntoMailto}&body={cuerpoMailto}"
                    Try
                        Process.Start(New ProcessStartInfo(mailtoUrl) With {.UseShellExecute = True})
                    Catch
                    End Try
                End Try

                ' 4. Diálogo amigable de confirmación para el usuario
                Dim msgConfirmacion As String = $"📧 ¡El recibo fue preparado con éxito!" & vbCrLf & vbCrLf &
                                                $"Se preparó el correo con la factura en PDF adjunta para:{vbCrLf}{correoDestino}"

                MessageBox.Show(msgConfirmacion, "Recibo preparado para envío", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
