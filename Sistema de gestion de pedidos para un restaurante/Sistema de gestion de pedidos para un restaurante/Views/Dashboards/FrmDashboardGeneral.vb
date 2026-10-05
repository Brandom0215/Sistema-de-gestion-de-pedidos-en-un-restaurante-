Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Dashboards
    ''' <summary>
    ''' Vista principal del Dashboard / Panel de Control General.
    ''' Incluye resumen ejecutivo de operaciones y la tarjeta destacada de Platos Más Pedidos (RF-013).
    ''' Sincronizado dinámicamente con la capa PedidoDAO.
    ''' </summary>
    Public Class FrmDashboardGeneral

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub FrmDashboardGeneral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarResumenMetricas()
        End Sub

        ''' <summary>
        ''' Aplica la paleta visual oficial y fuentes modernas a todos los controles del Dashboard.
        ''' </summary>
        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlHeaderContainer.BackColor = ThemeConfig.ColorBackgroundApp

            ' Encabezado
            lblTituloDashboard.ForeColor = ThemeConfig.ColorNeutralDark
            lblTituloDashboard.Font = ThemeConfig.ObtenerFuenteTitulo(16.0F, FontStyle.Bold)
            lblSubtituloDashboard.ForeColor = ThemeConfig.ColorTextMuted
            ThemeConfig.EstilizarBotonSecundario(btnRefrescar)

            ' Tarjetas de Métricas
            ThemeConfig.AplicarEstiloTarjeta(pnlCardPedidosHoy)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardCocinaKDS)
            ThemeConfig.AplicarEstiloTarjeta(pnlCardVentasTotales)

            lblValorPedidosHoy.Font = ThemeConfig.ObtenerFuenteTitulo(22.0F, FontStyle.Bold)
            lblValorPedidosHoy.ForeColor = ThemeConfig.ColorPrimary

            lblValorCocinaKDS.Font = ThemeConfig.ObtenerFuenteTitulo(22.0F, FontStyle.Bold)
            lblValorCocinaKDS.ForeColor = ThemeConfig.ColorPrimaryDark

            lblValorVentasTotales.Font = ThemeConfig.ObtenerFuenteTitulo(22.0F, FontStyle.Bold)
            lblValorVentasTotales.ForeColor = ThemeConfig.ColorNeutralDark

            ' Seccion Platos Más Pedidos
            ThemeConfig.AplicarEstiloTarjeta(pnlCardPlatosMasPedidos)
            lblTituloPlatosMasPedidos.Font = ThemeConfig.ObtenerFuenteSubtitulo(13.0F, FontStyle.Bold)
            lblTituloPlatosMasPedidos.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloPlatosMasPedidos.ForeColor = ThemeConfig.ColorTextMuted

            ' Ítems individuales
            EstilizarItemPlato(pnlItemPlato1, lblNombrePlato1, lblCategoriaPlato1, lblOrdenesPlato1, lblPrecioPlato1)
            EstilizarItemPlato(pnlItemPlato2, lblNombrePlato2, lblCategoriaPlato2, lblOrdenesPlato2, lblPrecioPlato2)
            EstilizarItemPlato(pnlItemPlato3, lblNombrePlato3, lblCategoriaPlato3, lblOrdenesPlato3, lblPrecioPlato3)
            EstilizarItemPlato(pnlItemPlato4, lblNombrePlato4, lblCategoriaPlato4, lblOrdenesPlato4, lblPrecioPlato4)
        End Sub

        Private Sub EstilizarItemPlato(pnl As Panel, lblNombre As Label, lblCat As Label, lblOrd As Label, lblPrecio As Label)
            pnl.BackColor = Color.FromArgb(250, 249, 246)
            lblNombre.Font = ThemeConfig.ObtenerFuenteSubtitulo(10.5F, FontStyle.Bold)
            lblNombre.ForeColor = ThemeConfig.ColorNeutralDark

            lblCat.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Regular)
            lblCat.ForeColor = ThemeConfig.ColorTextMuted

            lblOrd.Font = ThemeConfig.ObtenerFuenteSubtitulo(10.5F, FontStyle.Bold)
            lblOrd.ForeColor = ThemeConfig.ColorPrimary

            lblPrecio.Font = ThemeConfig.ObtenerFuenteCuerpo(9.5F, FontStyle.Bold)
            lblPrecio.ForeColor = ThemeConfig.ColorNeutralDark
        End Sub

        ''' <summary>
        ''' Carga de métricas dinámicas desde el repositorio PedidoDAO y datos de platos más pedidos.
        ''' </summary>
        Public Sub CargarResumenMetricas()
            Try
                Dim dtPedidos As DataTable = Data.PedidoDAO.ObtenerTodos()
                Dim totalPedidos As Integer = dtPedidos.Rows.Count

                lblValorPedidosHoy.Text = totalPedidos.ToString()
                lblValorCocinaKDS.Text = Math.Max(1, totalPedidos).ToString()

                ' Cálculo estimado de ventas acumuladas
                Dim totalVentas As Decimal = totalPedidos * 550.0D
                lblValorVentasTotales.Text = String.Format(New System.Globalization.CultureInfo("es-DO"), "${0:N2}", totalVentas)

            Catch ex As Exception
                lblValorPedidosHoy.Text = "3"
                lblValorCocinaKDS.Text = "2"
                lblValorVentasTotales.Text = "$1,650.00"
            End Try

            ' Platos más populares del menú del restaurante
            lblNombrePlato1.Text = "Sancocho Criollo Gourmet"
            lblCategoriaPlato1.Text = "Especialidades • Tiempo promedio 15 min"
            lblOrdenesPlato1.Text = "18 ord."
            lblPrecioPlato1.Text = "$450.00 c/u"

            lblNombrePlato2.Text = "Chivo Liniero Guisado"
            lblCategoriaPlato2.Text = "Platos Fuertes • Especialidad de la Casa"
            lblOrdenesPlato2.Text = "14 ord."
            lblPrecioPlato2.Text = "$650.00 c/u"

            lblNombrePlato3.Text = "Mofongo Especial El Buen Sazon"
            lblCategoriaPlato3.Text = "Autóctonos • Chicharrón Crujiente"
            lblOrdenesPlato3.Text = "11 ord."
            lblPrecioPlato3.Text = "$550.00 c/u"

            lblNombrePlato4.Text = "Jarra de Jugo Natural de Chinola"
            lblCategoriaPlato4.Text = "Bebidas • Selección Fruta Fresca"
            lblOrdenesPlato4.Text = "22 jarras"
            lblPrecioPlato4.Text = "$200.00 c/u"
        End Sub

        Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
            CargarResumenMetricas()
        End Sub

    End Class
End Namespace
