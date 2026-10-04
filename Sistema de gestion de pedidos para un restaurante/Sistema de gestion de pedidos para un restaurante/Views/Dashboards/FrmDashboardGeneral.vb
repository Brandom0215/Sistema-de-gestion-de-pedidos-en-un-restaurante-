Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Dashboards
    ''' <summary>
    ''' Vista principal del Dashboard / Panel de Control General.
    ''' Incluye resumen ejecutivo de operaciones y la tarjeta destacada de Platos Más Pedidos (RF-013).
    ''' </summary>
    Public Class FrmDashboardGeneral

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmDashboardGeneral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarResumenMetricas()
        End Sub

        ''' <summary>
        ''' Aplica la paleta visual oficial y fuentes modernas a todos los controles del Dashboard.
        ''' </summary>
        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlHeaderContainer.BackColor = ThemeConfig.ColorBackgroundApp

            ' Encabezado
            lblTituloDashboard.ForeColor = ThemeConfig.ColorNeutralDark
            lblTituloDashboard.Font = ThemeConfig.ObtenerFuenteTitulo(16.0F, FontStyle.Bold)
            lblSubtituloDashboard.ForeColor = ThemeConfig.ColorTextMuted

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
        ''' Carga de métricas y datos de platos más pedidos.
        ''' </summary>
        Public Sub CargarResumenMetricas()
            lblValorPedidosHoy.Text = "0"
            lblValorCocinaKDS.Text = "0"
            lblValorVentasTotales.Text = "$0.00"

            ' Datos de demostración basados en el menú del restaurante
            lblNombrePlato1.Text = "Bife de Chorizo Angus (400g)"
            lblCategoriaPlato1.Text = "Parrilla • T. cocción 18 min"
            lblOrdenesPlato1.Text = "14 ord."
            lblPrecioPlato1.Text = "$38.00 c/u"

            lblNombrePlato2.Text = "Pulpo a la Brasa con Pimentón"
            lblCategoriaPlato2.Text = "Parrilla • Especialidad de la casa"
            lblOrdenesPlato2.Text = "9 ord."
            lblPrecioPlato2.Text = "$42.50 c/u"

            lblNombrePlato3.Text = "Risotto de Hongos Silvestres"
            lblCategoriaPlato3.Text = "Sartenes • Plato vegetariano"
            lblOrdenesPlato3.Text = "8 ord."
            lblPrecioPlato3.Text = "$29.00 c/u"

            lblNombrePlato4.Text = "Vino Reserva de la Casa"
            lblCategoriaPlato4.Text = "Cava • Selección especial"
            lblOrdenesPlato4.Text = "18 copas"
            lblPrecioPlato4.Text = "$8.50 copa"
        End Sub

    End Class
End Namespace
