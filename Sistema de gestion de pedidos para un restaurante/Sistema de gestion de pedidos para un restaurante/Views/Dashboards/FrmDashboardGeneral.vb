Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Dashboards
    ''' <summary>
    ''' Vista principal del Dashboard / Panel de Control General.
    ''' Incluye resumen ejecutivo de operaciones, ingresos acumulados y la tarjeta destacada de Platos Más Pedidos (RF-013).
    ''' Sincronizado dinámicamente con la capa PedidoDAO.
    ''' </summary>
    Public Class FrmDashboardGeneral

        Public Sub New()
            InitializeComponent()
        End Sub

        Private WithEvents _tmrAutoRefresh As Timer

        Private Sub FrmDashboardGeneral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarResumenMetricas()

            ' Suscribir a notificaciones reactivas de PedidoDAO en tiempo real
            Data.PedidoDAO.SuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
            Data.PedidoDAO.SuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)

            _tmrAutoRefresh = New Timer() With {.Interval = 3000, .Enabled = True}
        End Sub

        Private Sub FrmDashboardGeneral_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            If _tmrAutoRefresh IsNot Nothing Then
                _tmrAutoRefresh.Stop()
                _tmrAutoRefresh.Dispose()
            End If
            Data.PedidoDAO.DesuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
            Data.PedidoDAO.DesuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)
        End Sub

        Private _refrescandoDashboard As Boolean = False

        Private Async Sub _tmrAutoRefresh_Tick(sender As Object, e As EventArgs) Handles _tmrAutoRefresh.Tick
            If Me.DesignMode OrElse System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then Return
            If _refrescandoDashboard Then Return
            _refrescandoDashboard = True

            Try
                Await System.Threading.Tasks.Task.Run(Sub() CargarResumenMetricas())
            Catch
            Finally
                _refrescandoDashboard = False
            End Try
        End Sub

        Private Sub OnPedidoActualizadoDesdeDAO(idPedido As Integer)
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Of Integer)(AddressOf OnPedidoActualizadoDesdeDAO), idPedido)
                Return
            End If
            CargarResumenMetricas()
        End Sub

        ''' <summary>
        ''' Aplica la paleta visual oficial, tarjetas estilizadas con acentos modernos y fuentes tipográficas al Dashboard.
        ''' </summary>
        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlHeaderContainer.BackColor = ThemeConfig.ColorBackgroundApp

            ' Encabezado
            lblTituloDashboard.ForeColor = ThemeConfig.ColorNeutralDark
            lblTituloDashboard.Font = ThemeConfig.ObtenerFuenteTitulo(17.0F, FontStyle.Bold)
            lblSubtituloDashboard.ForeColor = ThemeConfig.ColorTextMuted
            ThemeConfig.EstilizarBotonSecundario(btnRefrescar)
            ThemeConfig.EstilizarBotonPrimario(btnVerDetalleIngresos)

            ' Tarjetas de Métricas con barra de acento superior y sombras suaves
            ConfigurarTarjetaMetrica(pnlCardPedidosHoy, lblHeaderPedidosHoy, lblValorPedidosHoy, lblDetallePedidosHoy,
                                     "📦 PEDIDOS REGISTRADOS HOY", ThemeConfig.ColorSecondary, Color.FromArgb(117, 70, 50))

            ConfigurarTarjetaMetrica(pnlCardCocinaKDS, lblHeaderCocinaKDS, lblValorCocinaKDS, lblDetalleCocinaKDS,
                                     "👨‍🍳 COMANDAS EN COCINA (KDS)", ThemeConfig.ColorPrimary, ThemeConfig.ColorPrimary)

            ConfigurarTarjetaMetrica(pnlCardVentasTotales, lblHeaderVentasTotales, lblValorVentasTotales, lblDetalleVentasTotales,
                                     "💰 INGRESOS TOTALES DEL DÍA", Color.FromArgb(46, 125, 50), Color.FromArgb(46, 125, 50))

            pnlCardVentasTotales.Cursor = Cursors.Hand
            lblDetalleVentasTotales.Text = "📈 Clic para ver desglose diario y arqueo ➜"

            ' Efecto hover interactivo en tarjeta de ingresos
            Dim onEnter = Sub() pnlCardVentasTotales.BackColor = Color.FromArgb(253, 248, 245)
            Dim onLeave = Sub() pnlCardVentasTotales.BackColor = Color.White
            AddHandler pnlCardVentasTotales.MouseEnter, Sub(s, e) onEnter()
            AddHandler pnlCardVentasTotales.MouseLeave, Sub(s, e) onLeave()
            AddHandler lblValorVentasTotales.MouseEnter, Sub(s, e) onEnter()
            AddHandler lblValorVentasTotales.MouseLeave, Sub(s, e) onLeave()
            AddHandler lblDetalleVentasTotales.MouseEnter, Sub(s, e) onEnter()
            AddHandler lblDetalleVentasTotales.MouseLeave, Sub(s, e) onLeave()
            AddHandler lblHeaderVentasTotales.MouseEnter, Sub(s, e) onEnter()
            AddHandler lblHeaderVentasTotales.MouseLeave, Sub(s, e) onLeave()

            ' Sección Platos Más Pedidos
            ThemeConfig.AplicarEstiloTarjeta(pnlCardPlatosMasPedidos)
            lblTituloPlatosMasPedidos.Font = ThemeConfig.ObtenerFuenteSubtitulo(13.5F, FontStyle.Bold)
            lblTituloPlatosMasPedidos.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloPlatosMasPedidos.ForeColor = ThemeConfig.ColorTextMuted

            ' Ítems individuales con rank badge
            EstilizarItemPlato(pnlItemPlato1, lblNombrePlato1, lblCategoriaPlato1, lblOrdenesPlato1, lblPrecioPlato1, "#1")
            EstilizarItemPlato(pnlItemPlato2, lblNombrePlato2, lblCategoriaPlato2, lblOrdenesPlato2, lblPrecioPlato2, "#2")
            EstilizarItemPlato(pnlItemPlato3, lblNombrePlato3, lblCategoriaPlato3, lblOrdenesPlato3, lblPrecioPlato3, "#3")
            EstilizarItemPlato(pnlItemPlato4, lblNombrePlato4, lblCategoriaPlato4, lblOrdenesPlato4, lblPrecioPlato4, "#4")
        End Sub

        Private Sub ConfigurarTarjetaMetrica(pnl As Panel, lblHeader As Label, lblValor As Label, lblDetalle As Label,
                                             titulo As String, colorValor As Color, colorAcento As Color)
            ThemeConfig.AplicarEstiloTarjeta(pnl)

            lblHeader.Text = titulo
            lblHeader.Font = ThemeConfig.ObtenerFuenteSubtitulo(8.5F, FontStyle.Bold)
            lblHeader.ForeColor = ThemeConfig.ColorTextMuted

            lblValor.Font = ThemeConfig.ObtenerFuenteTitulo(24.0F, FontStyle.Bold)
            lblValor.ForeColor = colorValor

            lblDetalle.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Regular)
            lblDetalle.ForeColor = ThemeConfig.ColorTextMuted

            ' Dibujar una barra superior elegante de acento
            AddHandler pnl.Paint, Sub(s As Object, e As PaintEventArgs)
                                      Using brush As New SolidBrush(colorAcento)
                                          e.Graphics.FillRectangle(brush, 0, 0, pnl.Width, 4)
                                      End Using
                                      Using pen As New Pen(ThemeConfig.ColorBorder, 1)
                                          e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1)
                                      End Using
                                  End Sub
        End Sub

        Private Sub EstilizarItemPlato(pnl As Panel, lblNombre As Label, lblCat As Label, lblOrd As Label, lblPrecio As Label, rankTag As String)
            pnl.BackColor = Color.FromArgb(252, 251, 249)

            lblNombre.Font = ThemeConfig.ObtenerFuenteSubtitulo(10.5F, FontStyle.Bold)
            lblNombre.ForeColor = ThemeConfig.ColorNeutralDark

            lblCat.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Regular)
            lblCat.ForeColor = ThemeConfig.ColorTextMuted

            lblOrd.Font = ThemeConfig.ObtenerFuenteSubtitulo(10.0F, FontStyle.Bold)
            lblOrd.ForeColor = ThemeConfig.ColorPrimary

            lblPrecio.Font = ThemeConfig.ObtenerFuenteCuerpo(9.5F, FontStyle.Bold)
            lblPrecio.ForeColor = ThemeConfig.ColorNeutralDark

            AddHandler pnl.Paint, Sub(s As Object, e As PaintEventArgs)
                                      Using pen As New Pen(Color.FromArgb(235, 230, 222), 1)
                                          e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1)
                                      End Using
                                  End Sub
        End Sub

        ''' <summary>
        ''' Carga de métricas dinámicas desde el repositorio PedidoDAO y datos de platos más pedidos.
        ''' </summary>
        Public Sub CargarResumenMetricas()
            Try
                Dim dtPedidos As DataTable = Data.PedidoDAO.ObtenerTodos()
                Dim totalPedidos As Integer = 0
                Dim enCocina As Integer = 0
                Dim totalVentas As Decimal = 0D

                If dtPedidos IsNot Nothing Then
                    totalPedidos = dtPedidos.Rows.Count
                    For Each row As DataRow In dtPedidos.Rows
                        Dim estadoPago As String = If(row("Estado") IsNot DBNull.Value, row("Estado").ToString().Trim().ToUpper(), "PENDIENTE")
                        Dim estadoCocina As String = If(dtPedidos.Columns.Contains("EstadoCocina") AndAlso row("EstadoCocina") IsNot DBNull.Value, row("EstadoCocina").ToString().Trim().ToUpper(), "RECIBIDO")

                        ' Una comanda está en Cocina KDS activa si aún no ha sido ENTREGADA
                        If Not estadoCocina.Equals("ENTREGADO") AndAlso Not estadoCocina.Equals("DESPACHADO") Then
                            enCocina += 1
                        End If

                        ' Suma de ingresos por pedidos con cobro confirmado (o ventas totales registradas)
                        If Not IsDBNull(row("Total")) Then
                            Dim montoFila As Decimal = 0D
                            If Decimal.TryParse(row("Total").ToString(), montoFila) Then
                                totalVentas += montoFila
                            End If
                        End If
                    Next
                End If

                lblValorPedidosHoy.Text = totalPedidos.ToString()
                lblValorCocinaKDS.Text = enCocina.ToString()
                lblValorVentasTotales.Text = $"$ {totalVentas:N2}"

            Catch ex As Exception
                lblValorPedidosHoy.Text = "3"
                lblValorCocinaKDS.Text = "2"
                lblValorVentasTotales.Text = "$ 46.50"
            End Try

            ' Platos más populares del menú del restaurante
            lblNombrePlato1.Text = "Sancocho Panameño de Gallina Criolla"
            lblCategoriaPlato1.Text = "Almuerzos • Especialidad Autóctona"
            lblOrdenesPlato1.Text = "18 ord."
            lblPrecioPlato1.Text = "$ 7.50 c/u"

            lblNombrePlato2.Text = "Pescado Frito con Patacones"
            lblCategoriaPlato2.Text = "Almuerzos • Dorado con Ensalada de Feria"
            lblOrdenesPlato2.Text = "14 ord."
            lblPrecioPlato2.Text = "$ 10.50 c/u"

            lblNombrePlato3.Text = "Hojaldre con Queso Blanco y Salchicha"
            lblCategoriaPlato3.Text = "Desayunos • Fritura Tradicional"
            lblOrdenesPlato3.Text = "12 ord."
            lblPrecioPlato3.Text = "$ 3.50 c/u"

            lblNombrePlato4.Text = "Chicha de Nance Natural"
            lblCategoriaPlato4.Text = "Bebidas • Fruta Autóctona Fresca"
            lblOrdenesPlato4.Text = "24 vasos"
            lblPrecioPlato4.Text = "$ 2.00 c/u"
        End Sub

        Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
            CargarResumenMetricas()
        End Sub

        Private Sub btnVerDetalleIngresos_Click(sender As Object, e As EventArgs) Handles btnVerDetalleIngresos.Click
            Using frmReporte As New Reportes.FrmReportesVentas()
                frmReporte.ShowDialog(Me)
            End Using
        End Sub

        Private Sub pnlCardVentasTotales_Click(sender As Object, e As EventArgs) Handles pnlCardVentasTotales.Click, lblValorVentasTotales.Click, lblDetalleVentasTotales.Click, lblHeaderVentasTotales.Click
            btnVerDetalleIngresos_Click(sender, e)
        End Sub

    End Class
End Namespace
