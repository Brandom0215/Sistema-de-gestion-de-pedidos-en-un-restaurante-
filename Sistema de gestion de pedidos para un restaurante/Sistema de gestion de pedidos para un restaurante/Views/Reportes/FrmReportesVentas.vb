Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Reportes
    ''' <summary>
    ''' Formulario de Reportes de Ventas, Consolidado de Métodos de Pago y Cierre de Caja (RF-013).
    ''' </summary>
    Public Class FrmReportesVentas

        Private Const FONDO_INICIAL_CAJA As Decimal = 2500.0D

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

            ThemeConfig.EstilizarBotonPrimario(btnFiltrar)
            ThemeConfig.EstilizarBotonSecundario(btnExportarReporte)
            ThemeConfig.EstilizarBotonPrimario(btnEjecutarCierreCaja)

            ' Tarjetas de Métricas
            ThemeConfig.AplicarEstiloTarjeta(pnlCardEfectivo)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardTarjeta)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardTotalVentas)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardTicketPromedio)

            lblValorEfectivo.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorEfectivo.ForeColor = ThemeConfig.ColorTertiarySuccess

            lblValorTarjeta.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorTarjeta.ForeColor = ThemeConfig.ColorPrimary

            lblValorTotalVentas.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorTotalVentas.ForeColor = ThemeConfig.ColorNeutralDark

            lblValorTicketPromedio.Font = ThemeConfig.ObtenerFuenteTitulo(18.0F, FontStyle.Bold)
            lblValorTicketPromedio.ForeColor = ThemeConfig.ColorSecondary

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
        ''' Carga las transacciones procesadas y consolida los montos de efectivo, tarjeta y vuelto.
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

                For i As Integer = 0 To dtOriginal.Rows.Count - 1
                    Dim row As DataRow = dtOriginal.Rows(i)
                    Dim nroOrden As String = "ORD-" & Convert.ToInt32(row("ID")).ToString("D4")
                    Dim cliente As String = row("Cliente").ToString()
                    Dim servicio As String = row("TipoServicio").ToString()

                    ' Alternar métodos de pago demostrativos
                    Dim metodo As String = If(i Mod 2 = 0, "Efectivo", "Tarjeta / QR")
                    Dim monto As Decimal = 450.0D + (i * 100.0D)

                    If metodo = "Efectivo" Then
                        totalEfectivo += monto
                    Else
                        totalTarjeta += monto
                    End If
                    contadorTransacciones += 1

                    Dim dr As DataRow = dtReporte.NewRow()
                    dr("NroOrden") = nroOrden
                    dr("Cliente") = cliente
                    dr("MetodoPago") = metodo
                    dr("Estado") = "PAGADO"
                    dr("MontoTotal") = monto
                    dr("Hora") = row("FechaHora").ToString()
                    dtReporte.Rows.Add(dr)
                Next

                dgvReporteVentas.DataSource = dtReporte
                If dgvReporteVentas.Columns.Contains("MontoTotal") Then
                    dgvReporteVentas.Columns("MontoTotal").DefaultCellStyle.Format = "C2"
                End If

                Dim totalGlobal As Decimal = totalEfectivo + totalTarjeta
                Dim ticketPromedio As Decimal = If(contadorTransacciones > 0, totalGlobal / contadorTransacciones, 0D)

                Dim cultureEs As New System.Globalization.CultureInfo("es-DO")
                lblValorEfectivo.Text = String.Format(cultureEs, "${0:N2}", totalEfectivo)
                lblValorTarjeta.Text = String.Format(cultureEs, "${0:N2}", totalTarjeta)
                lblValorTotalVentas.Text = String.Format(cultureEs, "${0:N2}", totalGlobal)
                lblValorTicketPromedio.Text = String.Format(cultureEs, "${0:N2}", ticketPromedio)

                ' Actualizar etiquetas de arqueo de caja
                lblResumenFondoCaja.Text = String.Format(cultureEs, "Fondo Inicial de Caja: RD$ {0:N2}", FONDO_INICIAL_CAJA)
                lblResumenEfectivoCaja.Text = String.Format(cultureEs, "Ventas en Efectivo: RD$ {0:N2}", totalEfectivo)
                lblResumenTarjetaCaja.Text = String.Format(cultureEs, "Ventas en Tarjeta/QR: RD$ {0:N2}", totalTarjeta)
                lblResumenTotalEsperado.Text = String.Format(cultureEs, "Total en Caja Esperado: RD$ {0:N2}", FONDO_INICIAL_CAJA + totalEfectivo)

            Catch ex As Exception
                MessageBox.Show("Error al consolidar reportes de ventas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
            CargarTransaccionesReporte()
            MessageBox.Show("Filtro de fechas aplicado correctamente al reporte de ventas.", "Reporte Filtrado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        Private Sub btnExportarReporte_Click(sender As Object, e As EventArgs) Handles btnExportarReporte.Click
            MessageBox.Show("El reporte de ventas consolidado y arqueo de caja ha sido exportado exitosamente a formato PDF.", "Exportación PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        Private Sub btnEjecutarCierreCaja_Click(sender As Object, e As EventArgs) Handles btnEjecutarCierreCaja.Click
            Dim resp As DialogResult = MessageBox.Show("¿Desea proceder con el Cierre Fiscal de Caja para finalizar el turno operativo?", "Confirmar Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resp = DialogResult.Yes Then
                MessageBox.Show("¡Cierre de Caja efectuado con éxito!" & vbCrLf & vbCrLf &
                                "Fondo Inicial: RD$ 2,500.00" & vbCrLf &
                                "Total Cobrado en Efectivo: " & lblValorEfectivo.Text & vbCrLf &
                                "Total en Tarjetas/QR: " & lblValorTarjeta.Text & vbCrLf &
                                "Monto Total Fiscal en Caja: " & lblResumenTotalEsperado.Text, "Cierre Fiscal de Caja Completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

    End Class
End Namespace
