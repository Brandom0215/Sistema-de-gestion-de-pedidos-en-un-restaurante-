Namespace Views.Dashboards
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmDashboardGeneral
        Inherits System.Windows.Forms.Form

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        ' Contenedor de Encabezado
        Friend WithEvents pnlHeaderContainer As System.Windows.Forms.Panel
        Friend WithEvents lblTituloDashboard As System.Windows.Forms.Label
        Friend WithEvents lblSubtituloDashboard As System.Windows.Forms.Label
        Friend WithEvents btnRefrescar As System.Windows.Forms.Button

        ' Layout Principal
        Friend WithEvents tlpMainLayout As System.Windows.Forms.TableLayoutPanel

        ' 3 Tarjetas de Métricas Ejecutivas
        Friend WithEvents pnlCardPedidosHoy As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderPedidosHoy As System.Windows.Forms.Label
        Friend WithEvents lblValorPedidosHoy As System.Windows.Forms.Label
        Friend WithEvents lblDetallePedidosHoy As System.Windows.Forms.Label

        Friend WithEvents pnlCardCocinaKDS As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderCocinaKDS As System.Windows.Forms.Label
        Friend WithEvents lblValorCocinaKDS As System.Windows.Forms.Label
        Friend WithEvents lblDetalleCocinaKDS As System.Windows.Forms.Label

        Friend WithEvents pnlCardVentasTotales As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderVentasTotales As System.Windows.Forms.Label
        Friend WithEvents lblValorVentasTotales As System.Windows.Forms.Label
        Friend WithEvents lblDetalleVentasTotales As System.Windows.Forms.Label

        ' Sección "Platos Más Pedidos"
        Friend WithEvents pnlCardPlatosMasPedidos As System.Windows.Forms.Panel
        Friend WithEvents lblTituloPlatosMasPedidos As System.Windows.Forms.Label
        Friend WithEvents lblSubtituloPlatosMasPedidos As System.Windows.Forms.Label

        ' Lista de Platos Destacados
        Friend WithEvents pnlItemPlato1 As System.Windows.Forms.Panel
        Friend WithEvents lblNombrePlato1 As System.Windows.Forms.Label
        Friend WithEvents lblCategoriaPlato1 As System.Windows.Forms.Label
        Friend WithEvents lblOrdenesPlato1 As System.Windows.Forms.Label
        Friend WithEvents lblPrecioPlato1 As System.Windows.Forms.Label

        Friend WithEvents pnlItemPlato2 As System.Windows.Forms.Panel
        Friend WithEvents lblNombrePlato2 As System.Windows.Forms.Label
        Friend WithEvents lblCategoriaPlato2 As System.Windows.Forms.Label
        Friend WithEvents lblOrdenesPlato2 As System.Windows.Forms.Label
        Friend WithEvents lblPrecioPlato2 As System.Windows.Forms.Label

        Friend WithEvents pnlItemPlato3 As System.Windows.Forms.Panel
        Friend WithEvents lblNombrePlato3 As System.Windows.Forms.Label
        Friend WithEvents lblCategoriaPlato3 As System.Windows.Forms.Label
        Friend WithEvents lblOrdenesPlato3 As System.Windows.Forms.Label
        Friend WithEvents lblPrecioPlato3 As System.Windows.Forms.Label

        Friend WithEvents pnlItemPlato4 As System.Windows.Forms.Panel
        Friend WithEvents lblNombrePlato4 As System.Windows.Forms.Label
        Friend WithEvents lblCategoriaPlato4 As System.Windows.Forms.Label
        Friend WithEvents lblOrdenesPlato4 As System.Windows.Forms.Label
        Friend WithEvents lblPrecioPlato4 As System.Windows.Forms.Label

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeaderContainer = New System.Windows.Forms.Panel()
            Me.lblTituloDashboard = New System.Windows.Forms.Label()
            Me.lblSubtituloDashboard = New System.Windows.Forms.Label()
            Me.btnRefrescar = New System.Windows.Forms.Button()

            Me.tlpMainLayout = New System.Windows.Forms.TableLayoutPanel()

            ' Instanciación Métricas
            Me.pnlCardPedidosHoy = New System.Windows.Forms.Panel()
            Me.lblHeaderPedidosHoy = New System.Windows.Forms.Label()
            Me.lblValorPedidosHoy = New System.Windows.Forms.Label()
            Me.lblDetallePedidosHoy = New System.Windows.Forms.Label()

            Me.pnlCardCocinaKDS = New System.Windows.Forms.Panel()
            Me.lblHeaderCocinaKDS = New System.Windows.Forms.Label()
            Me.lblValorCocinaKDS = New System.Windows.Forms.Label()
            Me.lblDetalleCocinaKDS = New System.Windows.Forms.Label()

            Me.pnlCardVentasTotales = New System.Windows.Forms.Panel()
            Me.lblHeaderVentasTotales = New System.Windows.Forms.Label()
            Me.lblValorVentasTotales = New System.Windows.Forms.Label()
            Me.lblDetalleVentasTotales = New System.Windows.Forms.Label()

            ' Instanciación Platos Más Pedidos
            Me.pnlCardPlatosMasPedidos = New System.Windows.Forms.Panel()
            Me.lblTituloPlatosMasPedidos = New System.Windows.Forms.Label()
            Me.lblSubtituloPlatosMasPedidos = New System.Windows.Forms.Label()

            Me.pnlItemPlato1 = New System.Windows.Forms.Panel()
            Me.lblNombrePlato1 = New System.Windows.Forms.Label()
            Me.lblCategoriaPlato1 = New System.Windows.Forms.Label()
            Me.lblOrdenesPlato1 = New System.Windows.Forms.Label()
            Me.lblPrecioPlato1 = New System.Windows.Forms.Label()

            Me.pnlItemPlato2 = New System.Windows.Forms.Panel()
            Me.lblNombrePlato2 = New System.Windows.Forms.Label()
            Me.lblCategoriaPlato2 = New System.Windows.Forms.Label()
            Me.lblOrdenesPlato2 = New System.Windows.Forms.Label()
            Me.lblPrecioPlato2 = New System.Windows.Forms.Label()

            Me.pnlItemPlato3 = New System.Windows.Forms.Panel()
            Me.lblNombrePlato3 = New System.Windows.Forms.Label()
            Me.lblCategoriaPlato3 = New System.Windows.Forms.Label()
            Me.lblOrdenesPlato3 = New System.Windows.Forms.Label()
            Me.lblPrecioPlato3 = New System.Windows.Forms.Label()

            Me.pnlItemPlato4 = New System.Windows.Forms.Panel()
            Me.lblNombrePlato4 = New System.Windows.Forms.Label()
            Me.lblCategoriaPlato4 = New System.Windows.Forms.Label()
            Me.lblOrdenesPlato4 = New System.Windows.Forms.Label()
            Me.lblPrecioPlato4 = New System.Windows.Forms.Label()

            Me.pnlHeaderContainer.SuspendLayout()
            Me.tlpMainLayout.SuspendLayout()
            Me.pnlCardPedidosHoy.SuspendLayout()
            Me.pnlCardCocinaKDS.SuspendLayout()
            Me.pnlCardVentasTotales.SuspendLayout()

            Me.pnlCardPlatosMasPedidos.SuspendLayout()
            Me.pnlItemPlato1.SuspendLayout()
            Me.pnlItemPlato2.SuspendLayout()
            Me.pnlItemPlato3.SuspendLayout()
            Me.pnlItemPlato4.SuspendLayout()
            Me.SuspendLayout()

            ' 
            ' pnlHeaderContainer
            ' 
            Me.pnlHeaderContainer.Controls.Add(Me.btnRefrescar)
            Me.pnlHeaderContainer.Controls.Add(Me.lblSubtituloDashboard)
            Me.pnlHeaderContainer.Controls.Add(Me.lblTituloDashboard)
            Me.pnlHeaderContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderContainer.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeaderContainer.Name = "pnlHeaderContainer"
            Me.pnlHeaderContainer.Size = New System.Drawing.Size(980, 75)
            Me.pnlHeaderContainer.TabIndex = 0

            ' 
            ' btnRefrescar
            ' 
            Me.btnRefrescar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRefrescar.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnRefrescar.Location = New System.Drawing.Point(810, 18)
            Me.btnRefrescar.Name = "btnRefrescar"
            Me.btnRefrescar.Size = New System.Drawing.Size(150, 38)
            Me.btnRefrescar.TabIndex = 2
            Me.btnRefrescar.Text = "Actualizar Datos"
            Me.btnRefrescar.UseVisualStyleBackColor = True

            ' 
            ' lblTituloDashboard
            ' 
            Me.lblTituloDashboard.AutoSize = True
            Me.lblTituloDashboard.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloDashboard.Location = New System.Drawing.Point(20, 12)
            Me.lblTituloDashboard.Name = "lblTituloDashboard"
            Me.lblTituloDashboard.Size = New System.Drawing.Size(350, 37)
            Me.lblTituloDashboard.Text = "Panel de Control Principal"
            Me.lblTituloDashboard.UseMnemonic = False

            ' 
            ' lblSubtituloDashboard
            ' 
            Me.lblSubtituloDashboard.AutoSize = True
            Me.lblSubtituloDashboard.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloDashboard.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloDashboard.Location = New System.Drawing.Point(22, 48)
            Me.lblSubtituloDashboard.Name = "lblSubtituloDashboard"
            Me.lblSubtituloDashboard.Size = New System.Drawing.Size(430, 20)
            Me.lblSubtituloDashboard.Text = "Monitoreo operativo y platos destacados de mayor demanda"
            Me.lblSubtituloDashboard.UseMnemonic = False

            ' 
            ' tlpMainLayout
            ' 
            Me.tlpMainLayout.AutoScroll = True
            Me.tlpMainLayout.ColumnCount = 3
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33!))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33!))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34!))
            
            ' Fila 0: 3 Métricas principales (Fondo holgado de 135px)
            Me.tlpMainLayout.Controls.Add(Me.pnlCardPedidosHoy, 0, 0)
            Me.tlpMainLayout.Controls.Add(Me.pnlCardCocinaKDS, 1, 0)
            Me.tlpMainLayout.Controls.Add(Me.pnlCardVentasTotales, 2, 0)
            
            ' Fila 1: Tarjeta "Platos Más Pedidos" expandida
            Me.tlpMainLayout.Controls.Add(Me.pnlCardPlatosMasPedidos, 0, 1)
            Me.tlpMainLayout.SetColumnSpan(Me.pnlCardPlatosMasPedidos, 3)

            Me.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMainLayout.Location = New System.Drawing.Point(0, 75)
            Me.tlpMainLayout.Name = "tlpMainLayout"
            Me.tlpMainLayout.Padding = New System.Windows.Forms.Padding(15, 5, 15, 15)
            Me.tlpMainLayout.RowCount = 2
            Me.tlpMainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
            Me.tlpMainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 380.0!))
            Me.tlpMainLayout.Size = New System.Drawing.Size(980, 585)
            Me.tlpMainLayout.TabIndex = 1

            ' 
            ' pnlCardPedidosHoy
            ' 
            Me.pnlCardPedidosHoy.Controls.Add(Me.lblDetallePedidosHoy)
            Me.pnlCardPedidosHoy.Controls.Add(Me.lblValorPedidosHoy)
            Me.pnlCardPedidosHoy.Controls.Add(Me.lblHeaderPedidosHoy)
            Me.pnlCardPedidosHoy.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardPedidosHoy.Location = New System.Drawing.Point(18, 8)
            Me.pnlCardPedidosHoy.Name = "pnlCardPedidosHoy"
            Me.pnlCardPedidosHoy.Size = New System.Drawing.Size(304, 124)

            ' 
            ' lblHeaderPedidosHoy
            ' 
            Me.lblHeaderPedidosHoy.AutoSize = True
            Me.lblHeaderPedidosHoy.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderPedidosHoy.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderPedidosHoy.Location = New System.Drawing.Point(14, 12)
            Me.lblHeaderPedidosHoy.Text = "PEDIDOS REGISTRADOS HOY"
            Me.lblHeaderPedidosHoy.UseMnemonic = False

            ' 
            ' lblValorPedidosHoy
            ' 
            Me.lblValorPedidosHoy.AutoSize = True
            Me.lblValorPedidosHoy.Font = New System.Drawing.Font("Segoe UI", 22.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorPedidosHoy.Location = New System.Drawing.Point(10, 32)
            Me.lblValorPedidosHoy.Text = "0"
            Me.lblValorPedidosHoy.UseMnemonic = False

            ' 
            ' lblDetallePedidosHoy
            ' 
            Me.lblDetallePedidosHoy.AutoSize = True
            Me.lblDetallePedidosHoy.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblDetallePedidosHoy.ForeColor = System.Drawing.Color.DimGray
            Me.lblDetallePedidosHoy.Location = New System.Drawing.Point(14, 82)
            Me.lblDetallePedidosHoy.Text = "Órdenes en mesa y para llevar (RF-001)"
            Me.lblDetallePedidosHoy.UseMnemonic = False

            ' 
            ' pnlCardCocinaKDS
            ' 
            Me.pnlCardCocinaKDS.Controls.Add(Me.lblDetalleCocinaKDS)
            Me.pnlCardCocinaKDS.Controls.Add(Me.lblValorCocinaKDS)
            Me.pnlCardCocinaKDS.Controls.Add(Me.lblHeaderCocinaKDS)
            Me.pnlCardCocinaKDS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardCocinaKDS.Location = New System.Drawing.Point(328, 8)
            Me.pnlCardCocinaKDS.Name = "pnlCardCocinaKDS"
            Me.pnlCardCocinaKDS.Size = New System.Drawing.Size(304, 124)

            ' 
            ' lblHeaderCocinaKDS
            ' 
            Me.lblHeaderCocinaKDS.AutoSize = True
            Me.lblHeaderCocinaKDS.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderCocinaKDS.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderCocinaKDS.Location = New System.Drawing.Point(14, 12)
            Me.lblHeaderCocinaKDS.Text = "COMANDAS EN COCINA (KDS)"
            Me.lblHeaderCocinaKDS.UseMnemonic = False

            ' 
            ' lblValorCocinaKDS
            ' 
            Me.lblValorCocinaKDS.AutoSize = True
            Me.lblValorCocinaKDS.Font = New System.Drawing.Font("Segoe UI", 22.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorCocinaKDS.Location = New System.Drawing.Point(10, 32)
            Me.lblValorCocinaKDS.Text = "0"
            Me.lblValorCocinaKDS.UseMnemonic = False

            ' 
            ' lblDetalleCocinaKDS
            ' 
            Me.lblDetalleCocinaKDS.AutoSize = True
            Me.lblDetalleCocinaKDS.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblDetalleCocinaKDS.ForeColor = System.Drawing.Color.DimGray
            Me.lblDetalleCocinaKDS.Location = New System.Drawing.Point(14, 82)
            Me.lblDetalleCocinaKDS.Text = "En preparación activa con FIFO (RF-008)"
            Me.lblDetalleCocinaKDS.UseMnemonic = False

            ' 
            ' pnlCardVentasTotales
            ' 
            Me.pnlCardVentasTotales.Controls.Add(Me.lblDetalleVentasTotales)
            Me.pnlCardVentasTotales.Controls.Add(Me.lblValorVentasTotales)
            Me.pnlCardVentasTotales.Controls.Add(Me.lblHeaderVentasTotales)
            Me.pnlCardVentasTotales.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardVentasTotales.Location = New System.Drawing.Point(638, 8)
            Me.pnlCardVentasTotales.Name = "pnlCardVentasTotales"
            Me.pnlCardVentasTotales.Size = New System.Drawing.Size(304, 124)

            ' 
            ' lblHeaderVentasTotales
            ' 
            Me.lblHeaderVentasTotales.AutoSize = True
            Me.lblHeaderVentasTotales.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderVentasTotales.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderVentasTotales.Location = New System.Drawing.Point(14, 12)
            Me.lblHeaderVentasTotales.Text = "VENTAS TOTALES CONFIRMADAS"
            Me.lblHeaderVentasTotales.UseMnemonic = False

            ' 
            ' lblValorVentasTotales
            ' 
            Me.lblValorVentasTotales.AutoSize = True
            Me.lblValorVentasTotales.Font = New System.Drawing.Font("Segoe UI", 22.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorVentasTotales.Location = New System.Drawing.Point(10, 32)
            Me.lblValorVentasTotales.Text = "$0.00"
            Me.lblValorVentasTotales.UseMnemonic = False

            ' 
            ' lblDetalleVentasTotales
            ' 
            Me.lblDetalleVentasTotales.AutoSize = True
            Me.lblDetalleVentasTotales.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblDetalleVentasTotales.ForeColor = System.Drawing.Color.DimGray
            Me.lblDetalleVentasTotales.Location = New System.Drawing.Point(14, 82)
            Me.lblDetalleVentasTotales.Text = "Pagos autorizados en caja (RF-006)"
            Me.lblDetalleVentasTotales.UseMnemonic = False

            ' 
            ' pnlCardPlatosMasPedidos
            ' 
            Me.pnlCardPlatosMasPedidos.Controls.Add(Me.pnlItemPlato4)
            Me.pnlCardPlatosMasPedidos.Controls.Add(Me.pnlItemPlato3)
            Me.pnlCardPlatosMasPedidos.Controls.Add(Me.pnlItemPlato2)
            Me.pnlCardPlatosMasPedidos.Controls.Add(Me.pnlItemPlato1)
            Me.pnlCardPlatosMasPedidos.Controls.Add(Me.lblSubtituloPlatosMasPedidos)
            Me.pnlCardPlatosMasPedidos.Controls.Add(Me.lblTituloPlatosMasPedidos)
            Me.pnlCardPlatosMasPedidos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardPlatosMasPedidos.Location = New System.Drawing.Point(18, 145)
            Me.pnlCardPlatosMasPedidos.Name = "pnlCardPlatosMasPedidos"
            Me.pnlCardPlatosMasPedidos.Size = New System.Drawing.Size(944, 365)
            Me.pnlCardPlatosMasPedidos.TabIndex = 2

            ' 
            ' lblTituloPlatosMasPedidos
            ' 
            Me.lblTituloPlatosMasPedidos.AutoSize = True
            Me.lblTituloPlatosMasPedidos.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloPlatosMasPedidos.Location = New System.Drawing.Point(20, 15)
            Me.lblTituloPlatosMasPedidos.Name = "lblTituloPlatosMasPedidos"
            Me.lblTituloPlatosMasPedidos.Size = New System.Drawing.Size(240, 30)
            Me.lblTituloPlatosMasPedidos.Text = "Platos Más Pedidos"
            Me.lblTituloPlatosMasPedidos.UseMnemonic = False

            ' 
            ' lblSubtituloPlatosMasPedidos
            ' 
            Me.lblSubtituloPlatosMasPedidos.AutoSize = True
            Me.lblSubtituloPlatosMasPedidos.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloPlatosMasPedidos.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloPlatosMasPedidos.Location = New System.Drawing.Point(22, 45)
            Me.lblSubtituloPlatosMasPedidos.Name = "lblSubtituloPlatosMasPedidos"
            Me.lblSubtituloPlatosMasPedidos.Size = New System.Drawing.Size(350, 20)
            Me.lblSubtituloPlatosMasPedidos.Text = "Demanda acumulada del servicio (RF-013)"
            Me.lblSubtituloPlatosMasPedidos.UseMnemonic = False

            ' 
            ' pnlItemPlato1 (Anclaje dinámico horizontal completo)
            ' 
            Me.pnlItemPlato1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlItemPlato1.Controls.Add(Me.lblPrecioPlato1)
            Me.pnlItemPlato1.Controls.Add(Me.lblOrdenesPlato1)
            Me.pnlItemPlato1.Controls.Add(Me.lblCategoriaPlato1)
            Me.pnlItemPlato1.Controls.Add(Me.lblNombrePlato1)
            Me.pnlItemPlato1.Location = New System.Drawing.Point(20, 75)
            Me.pnlItemPlato1.Name = "pnlItemPlato1"
            Me.pnlItemPlato1.Size = New System.Drawing.Size(904, 60)

            ' 
            ' lblNombrePlato1
            ' 
            Me.lblNombrePlato1.AutoSize = True
            Me.lblNombrePlato1.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombrePlato1.Location = New System.Drawing.Point(15, 10)
            Me.lblNombrePlato1.Text = "Bife de Chorizo Angus (400g)"
            Me.lblNombrePlato1.UseMnemonic = False

            ' 
            ' lblCategoriaPlato1
            ' 
            Me.lblCategoriaPlato1.AutoSize = True
            Me.lblCategoriaPlato1.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblCategoriaPlato1.ForeColor = System.Drawing.Color.Gray
            Me.lblCategoriaPlato1.Location = New System.Drawing.Point(15, 33)
            Me.lblCategoriaPlato1.Text = "Parrilla • T. cocción 18 min"
            Me.lblCategoriaPlato1.UseMnemonic = False

            ' 
            ' lblOrdenesPlato1
            ' 
            Me.lblOrdenesPlato1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblOrdenesPlato1.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblOrdenesPlato1.ForeColor = System.Drawing.Color.Chocolate
            Me.lblOrdenesPlato1.Location = New System.Drawing.Point(660, 15)
            Me.lblOrdenesPlato1.Size = New System.Drawing.Size(110, 30)
            Me.lblOrdenesPlato1.Text = "14 ord."
            Me.lblOrdenesPlato1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblOrdenesPlato1.UseMnemonic = False

            ' 
            ' lblPrecioPlato1
            ' 
            Me.lblPrecioPlato1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPrecioPlato1.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblPrecioPlato1.Location = New System.Drawing.Point(780, 15)
            Me.lblPrecioPlato1.Size = New System.Drawing.Size(110, 30)
            Me.lblPrecioPlato1.Text = "$38.00 c/u"
            Me.lblPrecioPlato1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblPrecioPlato1.UseMnemonic = False

            ' 
            ' pnlItemPlato2
            ' 
            Me.pnlItemPlato2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlItemPlato2.Controls.Add(Me.lblPrecioPlato2)
            Me.pnlItemPlato2.Controls.Add(Me.lblOrdenesPlato2)
            Me.pnlItemPlato2.Controls.Add(Me.lblCategoriaPlato2)
            Me.pnlItemPlato2.Controls.Add(Me.lblNombrePlato2)
            Me.pnlItemPlato2.Location = New System.Drawing.Point(20, 145)
            Me.pnlItemPlato2.Name = "pnlItemPlato2"
            Me.pnlItemPlato2.Size = New System.Drawing.Size(904, 60)

            ' 
            ' lblNombrePlato2
            ' 
            Me.lblNombrePlato2.AutoSize = True
            Me.lblNombrePlato2.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombrePlato2.Location = New System.Drawing.Point(15, 10)
            Me.lblNombrePlato2.Text = "Pulpo a la Brasa con Pimentón"
            Me.lblNombrePlato2.UseMnemonic = False

            ' 
            ' lblCategoriaPlato2
            ' 
            Me.lblCategoriaPlato2.AutoSize = True
            Me.lblCategoriaPlato2.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblCategoriaPlato2.ForeColor = System.Drawing.Color.Gray
            Me.lblCategoriaPlato2.Location = New System.Drawing.Point(15, 33)
            Me.lblCategoriaPlato2.Text = "Parrilla • Especialidad de la casa"
            Me.lblCategoriaPlato2.UseMnemonic = False

            ' 
            ' lblOrdenesPlato2
            ' 
            Me.lblOrdenesPlato2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblOrdenesPlato2.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblOrdenesPlato2.ForeColor = System.Drawing.Color.Chocolate
            Me.lblOrdenesPlato2.Location = New System.Drawing.Point(660, 15)
            Me.lblOrdenesPlato2.Size = New System.Drawing.Size(110, 30)
            Me.lblOrdenesPlato2.Text = "9 ord."
            Me.lblOrdenesPlato2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblOrdenesPlato2.UseMnemonic = False

            ' 
            ' lblPrecioPlato2
            ' 
            Me.lblPrecioPlato2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPrecioPlato2.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblPrecioPlato2.Location = New System.Drawing.Point(780, 15)
            Me.lblPrecioPlato2.Size = New System.Drawing.Size(110, 30)
            Me.lblPrecioPlato2.Text = "$42.50 c/u"
            Me.lblPrecioPlato2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblPrecioPlato2.UseMnemonic = False

            ' 
            ' pnlItemPlato3
            ' 
            Me.pnlItemPlato3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlItemPlato3.Controls.Add(Me.lblPrecioPlato3)
            Me.pnlItemPlato3.Controls.Add(Me.lblOrdenesPlato3)
            Me.pnlItemPlato3.Controls.Add(Me.lblCategoriaPlato3)
            Me.pnlItemPlato3.Controls.Add(Me.lblNombrePlato3)
            Me.pnlItemPlato3.Location = New System.Drawing.Point(20, 215)
            Me.pnlItemPlato3.Name = "pnlItemPlato3"
            Me.pnlItemPlato3.Size = New System.Drawing.Size(904, 60)

            ' 
            ' lblNombrePlato3
            ' 
            Me.lblNombrePlato3.AutoSize = True
            Me.lblNombrePlato3.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombrePlato3.Location = New System.Drawing.Point(15, 10)
            Me.lblNombrePlato3.Text = "Risotto de Hongos Silvestres"
            Me.lblNombrePlato3.UseMnemonic = False

            ' 
            ' lblCategoriaPlato3
            ' 
            Me.lblCategoriaPlato3.AutoSize = True
            Me.lblCategoriaPlato3.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblCategoriaPlato3.ForeColor = System.Drawing.Color.Gray
            Me.lblCategoriaPlato3.Location = New System.Drawing.Point(15, 33)
            Me.lblCategoriaPlato3.Text = "Sartenes • Plato vegetariano"
            Me.lblCategoriaPlato3.UseMnemonic = False

            ' 
            ' lblOrdenesPlato3
            ' 
            Me.lblOrdenesPlato3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblOrdenesPlato3.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblOrdenesPlato3.ForeColor = System.Drawing.Color.Chocolate
            Me.lblOrdenesPlato3.Location = New System.Drawing.Point(660, 15)
            Me.lblOrdenesPlato3.Size = New System.Drawing.Size(110, 30)
            Me.lblOrdenesPlato3.Text = "8 ord."
            Me.lblOrdenesPlato3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblOrdenesPlato3.UseMnemonic = False

            ' 
            ' lblPrecioPlato3
            ' 
            Me.lblPrecioPlato3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPrecioPlato3.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblPrecioPlato3.Location = New System.Drawing.Point(780, 15)
            Me.lblPrecioPlato3.Size = New System.Drawing.Size(110, 30)
            Me.lblPrecioPlato3.Text = "$29.00 c/u"
            Me.lblPrecioPlato3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblPrecioPlato3.UseMnemonic = False

            ' 
            ' pnlItemPlato4
            ' 
            Me.pnlItemPlato4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlItemPlato4.Controls.Add(Me.lblPrecioPlato4)
            Me.pnlItemPlato4.Controls.Add(Me.lblOrdenesPlato4)
            Me.pnlItemPlato4.Controls.Add(Me.lblCategoriaPlato4)
            Me.pnlItemPlato4.Controls.Add(Me.lblNombrePlato4)
            Me.pnlItemPlato4.Location = New System.Drawing.Point(20, 285)
            Me.pnlItemPlato4.Name = "pnlItemPlato4"
            Me.pnlItemPlato4.Size = New System.Drawing.Size(904, 60)

            ' 
            ' lblNombrePlato4
            ' 
            Me.lblNombrePlato4.AutoSize = True
            Me.lblNombrePlato4.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombrePlato4.Location = New System.Drawing.Point(15, 10)
            Me.lblNombrePlato4.Text = "Vino Reserva de la Casa"
            Me.lblNombrePlato4.UseMnemonic = False

            ' 
            ' lblCategoriaPlato4
            ' 
            Me.lblCategoriaPlato4.AutoSize = True
            Me.lblCategoriaPlato4.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblCategoriaPlato4.ForeColor = System.Drawing.Color.Gray
            Me.lblCategoriaPlato4.Location = New System.Drawing.Point(15, 33)
            Me.lblCategoriaPlato4.Text = "Cava • Selección especial"
            Me.lblCategoriaPlato4.UseMnemonic = False

            ' 
            ' lblOrdenesPlato4
            ' 
            Me.lblOrdenesPlato4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblOrdenesPlato4.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblOrdenesPlato4.ForeColor = System.Drawing.Color.Chocolate
            Me.lblOrdenesPlato4.Location = New System.Drawing.Point(660, 15)
            Me.lblOrdenesPlato4.Size = New System.Drawing.Size(110, 30)
            Me.lblOrdenesPlato4.Text = "18 copas"
            Me.lblOrdenesPlato4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblOrdenesPlato4.UseMnemonic = False

            ' 
            ' lblPrecioPlato4
            ' 
            Me.lblPrecioPlato4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblPrecioPlato4.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblPrecioPlato4.Location = New System.Drawing.Point(780, 15)
            Me.lblPrecioPlato4.Size = New System.Drawing.Size(110, 30)
            Me.lblPrecioPlato4.Text = "$8.50 copa"
            Me.lblPrecioPlato4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblPrecioPlato4.UseMnemonic = False

            ' 
            ' FrmDashboardGeneral
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0F, 20.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(980, 660)
            Me.Controls.Add(Me.tlpMainLayout)
            Me.Controls.Add(Me.pnlHeaderContainer)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmDashboardGeneral"
            Me.Text = "Dashboard General"

            Me.pnlHeaderContainer.ResumeLayout(False)
            Me.pnlHeaderContainer.PerformLayout()
            Me.tlpMainLayout.ResumeLayout(False)

            Me.pnlCardPedidosHoy.ResumeLayout(False)
            Me.pnlCardPedidosHoy.PerformLayout()
            Me.pnlCardCocinaKDS.ResumeLayout(False)
            Me.pnlCardCocinaKDS.PerformLayout()
            Me.pnlCardVentasTotales.ResumeLayout(False)
            Me.pnlCardVentasTotales.PerformLayout()

            Me.pnlCardPlatosMasPedidos.ResumeLayout(False)
            Me.pnlCardPlatosMasPedidos.PerformLayout()
            Me.pnlItemPlato1.ResumeLayout(False)
            Me.pnlItemPlato1.PerformLayout()
            Me.pnlItemPlato2.ResumeLayout(False)
            Me.pnlItemPlato2.PerformLayout()
            Me.pnlItemPlato3.ResumeLayout(False)
            Me.pnlItemPlato3.PerformLayout()
            Me.pnlItemPlato4.ResumeLayout(False)
            Me.pnlItemPlato4.PerformLayout()

            Me.ResumeLayout(False)

        End Sub
    End Class
End Namespace
