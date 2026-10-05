Imports System
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Facturacion
    ''' <summary>
    ''' Formulario de Facturación & Comprobantes Digitales PDF (RF-007, RN-009, RN-010, CU-004).
    ''' Permite al Cajero o Administrador emitir comprobantes fiscales con número correlativo único e inalterable,
    ''' validar datos fiscales del receptor y generar documentos PDF estándar.
    ''' </summary>
    Public Class FrmFacturacionPDF

        Private _idPedidoSeleccionado As Integer = 0
        Private _pedidoSeleccionadoRow As DataRow = Nothing
        Private _carpetaFacturas As String

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

            ThemeConfig.EstilizarBotonPrimario(btnGenerarPDF)
            ThemeConfig.EstilizarBotonSecundario(btnGuardarComo)
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
                btnGenerarPDF.Text = "🖨️ Re-Imprimir Factura Fiscal (PDF)"
            Else
                txtRucCedula.ReadOnly = False
                txtRazonSocial.ReadOnly = False
                txtDireccion.ReadOnly = False
                txtTelefono.ReadOnly = False
                txtCorreo.ReadOnly = False
                btnGenerarPDF.Text = "🖨️ Imprimir Factura Fiscal (PDF)"
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

            Dim id = Convert.ToInt32(_pedidoSeleccionadoRow("ID"))
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
        ''' Genera la factura en PDF inalterable y la abre de inmediato en el visor del sistema.
        ''' </summary>
        Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
            If Not ValidarFormulario() Then Return

            Try
                ' 1. Registrar datos fiscales y asignar correlativo inalterable (RN-009, RN-010)
                Dim correlativo = PedidoDAO.RegistrarFactura(_idPedidoSeleccionado, txtRucCedula.Text.Trim(),
                                                            txtRazonSocial.Text.Trim(), txtDireccion.Text.Trim(),
                                                            txtTelefono.Text.Trim(), txtCorreo.Text.Trim())

                ' Refrescar fila
                _pedidoSeleccionadoRow = PedidoDAO.ObtenerPedidoPorId(_idPedidoSeleccionado)

                ' 2. Generar el archivo PDF físico
                Dim nombreArchivo = $"Factura_{correlativo}.pdf"
                Dim rutaArchivo = Path.Combine(_carpetaFacturas, nombreArchivo)

                FacturaPDFService.GenerarFacturaPDF(rutaArchivo, _pedidoSeleccionadoRow)

                ' 3. Notificar simulación de impresión y apertura
                Dim msg = $"🖨️ ¡Comprobante fiscal '{correlativo}' enviado a cola de impresión fiscal!" & vbCrLf & vbCrLf &
                          $"• Archivo PDF generado: {rutaArchivo}" & vbCrLf & vbCrLf &
                          "¿Desea previsualizar el documento fiscal en pantalla ahora mismo?"

                Dim respuesta = MessageBox.Show(msg, "Impresión Fiscal Digital", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If respuesta = DialogResult.Yes Then
                    AbrirArchivo(rutaArchivo)
                End If

                CargarPedidosPagados()
                ActualizarTicketVisual()
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al compilar el documento PDF: {ex.Message}", "Error de Generación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Permite guardar el archivo PDF con un diálogo en la ubicación que elija el usuario.
        ''' </summary>
        Private Sub btnGuardarComo_Click(sender As Object, e As EventArgs) Handles btnGuardarComo.Click
            If Not ValidarFormulario() Then Return

            Try
                Dim correlativo = PedidoDAO.RegistrarFactura(_idPedidoSeleccionado, txtRucCedula.Text.Trim(),
                                                            txtRazonSocial.Text.Trim(), txtDireccion.Text.Trim(),
                                                            txtTelefono.Text.Trim(), txtCorreo.Text.Trim())

                _pedidoSeleccionadoRow = PedidoDAO.ObtenerPedidoPorId(_idPedidoSeleccionado)

                Using sfd As New SaveFileDialog()
                    sfd.Filter = "Documentos PDF (*.pdf)|*.pdf"
                    sfd.FileName = $"Factura_{correlativo}.pdf"
                    sfd.Title = "Guardar Factura Fiscal PDF"

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
                MessageBox.Show("Por favor, seleccione una orden cobrada de la lista para emitir la factura.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            btnGenerarPDF.Text = "🖨️ Imprimir Factura Fiscal (PDF)"
            ActualizarTicketVisual()
            If dgvPedidosFacturar.SelectedRows.Count > 0 Then
                dgvPedidosFacturar.ClearSelection()
            End If
        End Sub

    End Class
End Namespace
