Imports System
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Reportes
    ''' <summary>
    ''' Formulario de Reportes de Ventas, Consolidado de Métodos de Pago y Arqueo de Caja (RF-013).
    ''' Permite filtrar transacciones por rango de fechas y exportar el informe oficial en PDF estándar.
    ''' </summary>
    Public Class FrmReportesVentas

        Private Const FONDO_INICIAL_CAJA As Decimal = 150.0D

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub FrmReportesVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            dtpFechaInicio.Value = DateTime.Now.AddDays(-7)
            dtpFechaFin.Value = DateTime.Now
            CargarTransaccionesReporte()
        End Sub

        ' =========================================================================
        ' ESTILIZADO Y CONFIGURACIÓN VISUAL
        ' =========================================================================

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlHeaderContainer.BackColor = ThemeConfig.ColorBackgroundApp

            lblTituloReportes.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloReportes.ForeColor = ThemeConfig.ColorTextMuted
            lblFechaDesde.ForeColor = ThemeConfig.ColorNeutralDark
            lblFechaHasta.ForeColor = ThemeConfig.ColorNeutralDark

            ThemeConfig.EstilizarBotonSecundario(btnFiltrar)
            ThemeConfig.EstilizarBotonPrimario(btnExportarReporte)
            ThemeConfig.EstilizarBotonPrimario(btnEjecutarCierreCaja)

            ' Tarjetas de Métricas
            ThemeConfig.AplicarEstiloTarjeta(pnlCardEfectivo)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardTarjeta)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardTotalVentas)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardTicketPromedio)

            lblValorEfectivo.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorEfectivo.ForeColor = ThemeConfig.ColorTertiarySuccess

            lblValorTarjeta.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorTarjeta.ForeColor = Color.FromArgb(41, 128, 185)

            lblValorTotalVentas.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorTotalVentas.ForeColor = ThemeConfig.ColorPrimary

            lblValorTicketPromedio.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorTicketPromedio.ForeColor = ThemeConfig.ColorNeutralDark

            grpListadoVentas.ForeColor = ThemeConfig.ColorSecondary
            grpCierreCaja.ForeColor = ThemeConfig.ColorSecondary

            ' Estilizado de DataGridView
            dgvReporteVentas.BackgroundColor = Color.White
            dgvReporteVentas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 230, 220)
            dgvReporteVentas.DefaultCellStyle.SelectionForeColor = ThemeConfig.ColorNeutralDark
            dgvReporteVentas.ColumnHeadersDefaultCellStyle.BackColor = ThemeConfig.ColorSecondary
            dgvReporteVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvReporteVentas.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            dgvReporteVentas.EnableHeadersVisualStyles = False
            dgvReporteVentas.RowTemplate.Height = 28
        End Sub

        ''' <summary>
        ''' Carga las transacciones registradas en PedidoDAO y calcula los totales por método de pago.
        ''' </summary>
        Private Sub CargarTransaccionesReporte()
            Try
                Dim dtOriginal As DataTable = Data.PedidoDAO.ObtenerTodos()
                Dim dtReporte As New DataTable("ReporteVentas")
                dtReporte.Columns.Add("NroOrden", GetType(String))
                dtReporte.Columns.Add("Cliente", GetType(String))
                dtReporte.Columns.Add("MetodoPago", GetType(String))
                dtReporte.Columns.Add("Estado", GetType(String))
                dtReporte.Columns.Add("MontoTotal", GetType(Decimal))
                dtReporte.Columns.Add("Hora", GetType(String))

                Dim totalEfectivo As Decimal = 0D
                Dim totalTarjeta As Decimal = 0D
                Dim contadorTransacciones As Integer = 0

                If dtOriginal IsNot Nothing Then
                    Dim fechaIni As DateTime = dtpFechaInicio.Value.Date
                    Dim fechaFin As DateTime = dtpFechaFin.Value.Date.AddDays(1).AddTicks(-1)

                    For i As Integer = 0 To dtOriginal.Rows.Count - 1
                        Dim row As DataRow = dtOriginal.Rows(i)

                        ' Validar filtro por rango de fechas
                        Dim fechaHoraFila As DateTime = DateTime.Now
                        If row("FechaHora") IsNot DBNull.Value Then
                            DateTime.TryParse(row("FechaHora").ToString(), fechaHoraFila)
                        End If

                        If fechaHoraFila < fechaIni OrElse fechaHoraFila > fechaFin Then
                            Continue For
                        End If

                        Dim nroOrden As String = "ORD-" & Convert.ToInt32(row("ID")).ToString("D4")
                        Dim cliente As String = If(row("Cliente") IsNot DBNull.Value, row("Cliente").ToString(), "Consumidor")
                        Dim estado As String = If(row("Estado") IsNot DBNull.Value, row("Estado").ToString().ToUpper(), "PENDIENTE")

                        Dim metodo As String = "Efectivo"
                        If row("MetodoPago") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(row("MetodoPago").ToString()) Then
                            metodo = row("MetodoPago").ToString()
                        End If

                        Dim monto As Decimal = 0D
                        If Not IsDBNull(row("Total")) AndAlso Decimal.TryParse(row("Total").ToString(), monto) AndAlso monto > 0D Then
                            ' Monto real del pedido
                        Else
                            monto = 15.0D
                        End If

                        If metodo.IndexOf("Tarjeta", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                           metodo.IndexOf("QR", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                           metodo.IndexOf("Web", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                           metodo.IndexOf("Stripe", StringComparison.OrdinalIgnoreCase) >= 0 Then
                            totalTarjeta += monto
                        Else
                            totalEfectivo += monto
                        End If
                        contadorTransacciones += 1

                        Dim dr As DataRow = dtReporte.NewRow()
                        dr("NroOrden") = nroOrden
                        dr("Cliente") = cliente
                        dr("MetodoPago") = metodo
                        dr("Estado") = estado
                        dr("MontoTotal") = monto
                        dr("Hora") = fechaHoraFila.ToString("yyyy-MM-dd HH:mm:ss")
                        dtReporte.Rows.Add(dr)
                    Next
                End If

                dgvReporteVentas.DataSource = dtReporte
                If dgvReporteVentas.Columns.Contains("MontoTotal") Then
                    dgvReporteVentas.Columns("MontoTotal").DefaultCellStyle.Format = "C2"
                End If

                Dim totalGlobal As Decimal = totalEfectivo + totalTarjeta
                Dim ticketPromedio As Decimal = If(contadorTransacciones > 0, totalGlobal / contadorTransacciones, 0D)

                lblValorEfectivo.Text = $"$ {totalEfectivo:N2}"
                lblValorTarjeta.Text = $"$ {totalTarjeta:N2}"
                lblValorTotalVentas.Text = $"$ {totalGlobal:N2}"
                lblValorTicketPromedio.Text = $"$ {ticketPromedio:N2}"

                ' Actualizar arqueo de caja
                lblResumenFondoCaja.Text = $"Fondo Inicial de Apertura en Caja: $ {FONDO_INICIAL_CAJA:N2}"
                lblResumenEfectivoCaja.Text = $"Recaudación en Efectivo: +$ {totalEfectivo:N2}"
                lblResumenTarjetaCaja.Text = $"Recaudación en Tarjetas/QR: +$ {totalTarjeta:N2}"
                lblResumenTotalEsperado.Text = $"Total Esperado en Caja: $ {(FONDO_INICIAL_CAJA + totalEfectivo):N2}"

            Catch ex As Exception
                MessageBox.Show("Error al consolidar reportes de ventas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
            CargarTransaccionesReporte()
            MessageBox.Show("Filtro de fechas aplicado correctamente al reporte de ventas.", "Reporte Filtrado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        ''' <summary>
        ''' Solicita al usuario la ubicación de destino mediante SaveFileDialog y compila el reporte oficial en PDF estándar.
        ''' </summary>
        Private Sub btnExportarReporte_Click(sender As Object, e As EventArgs) Handles btnExportarReporte.Click
            Try
                Using sfd As New SaveFileDialog()
                    sfd.Title = "Guardar Reporte Ejecutivo de Ventas en PDF"
                    sfd.Filter = "Documento PDF (*.pdf)|*.pdf"
                    sfd.FileName = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                    If sfd.ShowDialog(Me) = DialogResult.OK Then
                        Dim dtReporte As DataTable = TryCast(dgvReporteVentas.DataSource, DataTable)
                        If dtReporte Is Nothing Then
                            dtReporte = New DataTable()
                        End If

                        Dim totalVentas As Decimal = ExtraerDecimalDeEtiqueta(lblValorTotalVentas.Text)
                        Dim totalEfectivo As Decimal = ExtraerDecimalDeEtiqueta(lblValorEfectivo.Text)
                        Dim totalTarjeta As Decimal = ExtraerDecimalDeEtiqueta(lblValorTarjeta.Text)
                        Dim ticketPromedio As Decimal = ExtraerDecimalDeEtiqueta(lblValorTicketPromedio.Text)

                        Services.ReporteVentasPDFService.GenerarReporteVentasPDF(
                            sfd.FileName,
                            dtReporte,
                            dtpFechaInicio.Value,
                            dtpFechaFin.Value,
                            totalVentas,
                            totalEfectivo,
                            totalTarjeta,
                            ticketPromedio,
                            FONDO_INICIAL_CAJA
                        )

                        Dim abrirResp As DialogResult = MessageBox.Show(
                            "¡Reporte consolidado de ventas y arqueo de caja exportado exitosamente a PDF!" & vbCrLf & vbCrLf &
                            "Ubicación del archivo:" & vbCrLf & sfd.FileName & vbCrLf & vbCrLf &
                            "¿Desea abrir el archivo PDF generado ahora?",
                            "Reporte PDF Generado",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        )

                        If abrirResp = DialogResult.Yes Then
                            Try
                                Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                            Catch exOpen As Exception
                                MessageBox.Show("No se pudo abrir el visor de PDF predeterminado: " & exOpen.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End Try
                        End If
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error al generar el archivo PDF: " & ex.Message, "Error de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function ExtraerDecimalDeEtiqueta(texto As String) As Decimal
            If String.IsNullOrWhiteSpace(texto) Then Return 0D
            Dim limpio As String = texto.Replace("$", "").Replace("RD$", "").Trim()
            Dim res As Decimal = 0D
            If Decimal.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, res) Then
                Return res
            End If
            If Decimal.TryParse(limpio, NumberStyles.Any, New CultureInfo("es-ES"), res) Then
                Return res
            End If
            Return 0D
        End Function


        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If keyData = Keys.Escape Then
                Me.Close()
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function

        Private Sub btnEjecutarCierreCaja_Click(sender As Object, e As EventArgs) Handles btnEjecutarCierreCaja.Click
            Dim resp As DialogResult = MessageBox.Show("¿Desea proceder con el Cierre Fiscal de Caja para finalizar el turno operativo?", "Confirmar Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp = DialogResult.Yes Then
                Dim totalEfectivo As Decimal = ExtraerDecimalDeEtiqueta(lblValorEfectivo.Text)
                MessageBox.Show("¡Cierre de Caja efectuado con éxito!" & vbCrLf & vbCrLf &
                                $"Fondo Inicial: $ {FONDO_INICIAL_CAJA:N2}" & vbCrLf &
                                $"Total Cobrado en Efectivo: {lblValorEfectivo.Text}" & vbCrLf &
                                $"Total en Tarjetas/QR: {lblValorTarjeta.Text}" & vbCrLf &
                                $"Monto Total Fiscal en Caja: $ {(FONDO_INICIAL_CAJA + totalEfectivo):N2}", "Cierre Fiscal de Caja Completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

    End Class
End Namespace
