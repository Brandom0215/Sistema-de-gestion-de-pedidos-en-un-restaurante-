Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Reportes
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmReportesVentas
        Inherits Sistema_de_gestion_de_pedidos_para_un_restaurante.Views.Common.FrmBaseForm

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

        ' Encabezado y Filtros
        Friend WithEvents pnlHeaderContainer As System.Windows.Forms.Panel
        Friend WithEvents lblTituloReportes As System.Windows.Forms.Label
        Friend WithEvents lblSubtituloReportes As System.Windows.Forms.Label
        Friend WithEvents lblFechaDesde As System.Windows.Forms.Label
        Friend WithEvents dtpFechaInicio As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblFechaHasta As System.Windows.Forms.Label
        Friend WithEvents dtpFechaFin As System.Windows.Forms.DateTimePicker
        Friend WithEvents btnFiltrar As System.Windows.Forms.Button
        Friend WithEvents btnExportarReporte As System.Windows.Forms.Button

        ' Layout Principal
        Friend WithEvents tlpMainLayout As System.Windows.Forms.TableLayoutPanel

        ' 4 Tarjetas de Métricas de Ventas
        Friend WithEvents pnlCardEfectivo As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderEfectivo As System.Windows.Forms.Label
        Friend WithEvents lblValorEfectivo As System.Windows.Forms.Label

        Friend WithEvents pnlCardTarjeta As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTarjeta As System.Windows.Forms.Label
        Friend WithEvents lblValorTarjeta As System.Windows.Forms.Label

        Friend WithEvents pnlCardTotalVentas As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTotalVentas As System.Windows.Forms.Label
        Friend WithEvents lblValorTotalVentas As System.Windows.Forms.Label

        Friend WithEvents pnlCardTicketPromedio As System.Windows.Forms.Panel
        Friend WithEvents lblHeaderTicketPromedio As System.Windows.Forms.Label
        Friend WithEvents lblValorTicketPromedio As System.Windows.Forms.Label

        ' Layout Inferior (Tabla de Transacciones + Panel Cierre de Caja)
        Friend WithEvents tlpBottomLayout As System.Windows.Forms.TableLayoutPanel

        Friend WithEvents grpListadoVentas As System.Windows.Forms.GroupBox
        Friend WithEvents dgvReporteVentas As System.Windows.Forms.DataGridView

        Friend WithEvents grpCierreCaja As System.Windows.Forms.GroupBox
        Friend WithEvents lblInfoArqueo As System.Windows.Forms.Label
        Friend WithEvents lblResumenFondoCaja As System.Windows.Forms.Label
        Friend WithEvents lblResumenEfectivoCaja As System.Windows.Forms.Label
        Friend WithEvents lblResumenTarjetaCaja As System.Windows.Forms.Label
        Friend WithEvents lblResumenTotalEsperado As System.Windows.Forms.Label
        Friend WithEvents btnEjecutarCierreCaja As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeaderContainer = New System.Windows.Forms.Panel()
            Me.lblTituloReportes = New System.Windows.Forms.Label()
            Me.lblSubtituloReportes = New System.Windows.Forms.Label()
            Me.lblFechaDesde = New System.Windows.Forms.Label()
            Me.dtpFechaInicio = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaHasta = New System.Windows.Forms.Label()
            Me.dtpFechaFin = New System.Windows.Forms.DateTimePicker()
            Me.btnFiltrar = New System.Windows.Forms.Button()
            Me.btnExportarReporte = New System.Windows.Forms.Button()

            Me.tlpMainLayout = New System.Windows.Forms.TableLayoutPanel()

            Me.pnlCardEfectivo = New System.Windows.Forms.Panel()
            Me.lblHeaderEfectivo = New System.Windows.Forms.Label()
            Me.lblValorEfectivo = New System.Windows.Forms.Label()

            Me.pnlCardTarjeta = New System.Windows.Forms.Panel()
            Me.lblHeaderTarjeta = New System.Windows.Forms.Label()
            Me.lblValorTarjeta = New System.Windows.Forms.Label()

            Me.pnlCardTotalVentas = New System.Windows.Forms.Panel()
            Me.lblHeaderTotalVentas = New System.Windows.Forms.Label()
            Me.lblValorTotalVentas = New System.Windows.Forms.Label()

            Me.pnlCardTicketPromedio = New System.Windows.Forms.Panel()
            Me.lblHeaderTicketPromedio = New System.Windows.Forms.Label()
            Me.lblValorTicketPromedio = New System.Windows.Forms.Label()

            Me.tlpBottomLayout = New System.Windows.Forms.TableLayoutPanel()
            Me.grpListadoVentas = New System.Windows.Forms.GroupBox()
            Me.dgvReporteVentas = New System.Windows.Forms.DataGridView()

            Me.grpCierreCaja = New System.Windows.Forms.GroupBox()
            Me.lblInfoArqueo = New System.Windows.Forms.Label()
            Me.lblResumenFondoCaja = New System.Windows.Forms.Label()
            Me.lblResumenEfectivoCaja = New System.Windows.Forms.Label()
            Me.lblResumenTarjetaCaja = New System.Windows.Forms.Label()
            Me.lblResumenTotalEsperado = New System.Windows.Forms.Label()
            Me.btnEjecutarCierreCaja = New System.Windows.Forms.Button()

            Me.pnlHeaderContainer.SuspendLayout()
            Me.tlpMainLayout.SuspendLayout()
            Me.pnlCardEfectivo.SuspendLayout()
            Me.pnlCardTarjeta.SuspendLayout()
            Me.pnlCardTotalVentas.SuspendLayout()
            Me.pnlCardTicketPromedio.SuspendLayout()
            Me.tlpBottomLayout.SuspendLayout()
            Me.grpListadoVentas.SuspendLayout()
            Me.grpCierreCaja.SuspendLayout()
            CType(Me.dgvReporteVentas, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()

            ' 
            ' pnlHeaderContainer
            ' 
            Me.pnlHeaderContainer.Controls.Add(Me.btnExportarReporte)
            Me.pnlHeaderContainer.Controls.Add(Me.btnFiltrar)
            Me.pnlHeaderContainer.Controls.Add(Me.dtpFechaFin)
            Me.pnlHeaderContainer.Controls.Add(Me.lblFechaHasta)
            Me.pnlHeaderContainer.Controls.Add(Me.dtpFechaInicio)
            Me.pnlHeaderContainer.Controls.Add(Me.lblFechaDesde)
            Me.pnlHeaderContainer.Controls.Add(Me.lblSubtituloReportes)
            Me.pnlHeaderContainer.Controls.Add(Me.lblTituloReportes)
            Me.pnlHeaderContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderContainer.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeaderContainer.Name = "pnlHeaderContainer"
            Me.pnlHeaderContainer.Size = New System.Drawing.Size(980, 75)
            Me.pnlHeaderContainer.TabIndex = 0

            ' 
            ' lblTituloReportes
            ' 
            Me.lblTituloReportes.AutoSize = True
            Me.lblTituloReportes.Font = New System.Drawing.Font("Segoe UI", 15.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloReportes.Location = New System.Drawing.Point(20, 10)
            Me.lblTituloReportes.Name = "lblTituloReportes"
            Me.lblTituloReportes.Size = New System.Drawing.Size(370, 35)
            Me.lblTituloReportes.Text = "Reportes de Ventas y Arqueo"
            Me.lblTituloReportes.UseMnemonic = False

            ' 
            ' lblSubtituloReportes
            ' 
            Me.lblSubtituloReportes.AutoSize = True
            Me.lblSubtituloReportes.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblSubtituloReportes.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloReportes.Location = New System.Drawing.Point(22, 45)
            Me.lblSubtituloReportes.Name = "lblSubtituloReportes"
            Me.lblSubtituloReportes.Size = New System.Drawing.Size(340, 19)
            Me.lblSubtituloReportes.Text = "Consolidado fiscal y métodos de pago (RF-013)"
            Me.lblSubtituloReportes.UseMnemonic = False

            ' 
            ' lblFechaDesde
            ' 
            Me.lblFechaDesde.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblFechaDesde.AutoSize = True
            Me.lblFechaDesde.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblFechaDesde.Location = New System.Drawing.Point(385, 27)
            Me.lblFechaDesde.Name = "lblFechaDesde"
            Me.lblFechaDesde.Size = New System.Drawing.Size(55, 19)
            Me.lblFechaDesde.Text = "Desde:"

            ' 
            ' dtpFechaInicio
            ' 
            Me.dtpFechaInicio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpFechaInicio.Location = New System.Drawing.Point(438, 23)
            Me.dtpFechaInicio.Name = "dtpFechaInicio"
            Me.dtpFechaInicio.Size = New System.Drawing.Size(100, 27)
            Me.dtpFechaInicio.TabIndex = 1

            ' 
            ' lblFechaHasta
            ' 
            Me.lblFechaHasta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblFechaHasta.AutoSize = True
            Me.lblFechaHasta.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblFechaHasta.Location = New System.Drawing.Point(548, 27)
            Me.lblFechaHasta.Name = "lblFechaHasta"
            Me.lblFechaHasta.Size = New System.Drawing.Size(51, 19)
            Me.lblFechaHasta.Text = "Hasta:"

            ' 
            ' dtpFechaFin
            ' 
            Me.dtpFechaFin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpFechaFin.Location = New System.Drawing.Point(598, 23)
            Me.dtpFechaFin.Name = "dtpFechaFin"
            Me.dtpFechaFin.Size = New System.Drawing.Size(100, 27)
            Me.dtpFechaFin.TabIndex = 2

            ' 
            ' btnFiltrar
            ' 
            Me.btnFiltrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnFiltrar.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnFiltrar.Location = New System.Drawing.Point(710, 21)
            Me.btnFiltrar.Name = "btnFiltrar"
            Me.btnFiltrar.Size = New System.Drawing.Size(110, 31)
            Me.btnFiltrar.TabIndex = 3
            Me.btnFiltrar.Text = "Filtrar Fechas"
            Me.btnFiltrar.UseVisualStyleBackColor = True

            ' 
            ' btnExportarReporte
            ' 
            Me.btnExportarReporte.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExportarReporte.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnExportarReporte.Location = New System.Drawing.Point(828, 21)
            Me.btnExportarReporte.Name = "btnExportarReporte"
            Me.btnExportarReporte.Size = New System.Drawing.Size(135, 31)
            Me.btnExportarReporte.TabIndex = 4
            Me.btnExportarReporte.Text = "📄 Guardar PDF"
            Me.btnExportarReporte.UseVisualStyleBackColor = True

            ' 
            ' tlpMainLayout
            ' 
            Me.tlpMainLayout.ColumnCount = 4
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            
            Me.tlpMainLayout.Controls.Add(Me.pnlCardEfectivo, 0, 0)
            Me.tlpMainLayout.Controls.Add(Me.pnlCardTarjeta, 1, 0)
            Me.tlpMainLayout.Controls.Add(Me.pnlCardTotalVentas, 2, 0)
            Me.tlpMainLayout.Controls.Add(Me.pnlCardTicketPromedio, 3, 0)

            Me.tlpMainLayout.Controls.Add(Me.tlpBottomLayout, 0, 1)
            Me.tlpMainLayout.SetColumnSpan(Me.tlpBottomLayout, 4)

            Me.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMainLayout.Location = New System.Drawing.Point(0, 75)
            Me.tlpMainLayout.Name = "tlpMainLayout"
            Me.tlpMainLayout.Padding = New System.Windows.Forms.Padding(15, 5, 15, 15)
            Me.tlpMainLayout.RowCount = 2
            Me.tlpMainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110.0!))
            Me.tlpMainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMainLayout.Size = New System.Drawing.Size(980, 585)
            Me.tlpMainLayout.TabIndex = 1

            ' 
            ' pnlCardEfectivo
            ' 
            Me.pnlCardEfectivo.Controls.Add(Me.lblValorEfectivo)
            Me.pnlCardEfectivo.Controls.Add(Me.lblHeaderEfectivo)
            Me.pnlCardEfectivo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardEfectivo.Location = New System.Drawing.Point(18, 8)
            Me.pnlCardEfectivo.Name = "pnlCardEfectivo"
            Me.pnlCardEfectivo.Size = New System.Drawing.Size(221, 104)

            ' 
            ' lblHeaderEfectivo
            ' 
            Me.lblHeaderEfectivo.AutoSize = True
            Me.lblHeaderEfectivo.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderEfectivo.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderEfectivo.Location = New System.Drawing.Point(12, 12)
            Me.lblHeaderEfectivo.Text = "VENTAS EN EFECTIVO"

            ' 
            ' lblValorEfectivo
            ' 
            Me.lblValorEfectivo.AutoSize = True
            Me.lblValorEfectivo.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorEfectivo.Location = New System.Drawing.Point(10, 36)
            Me.lblValorEfectivo.Text = "$0.00"

            ' 
            ' pnlCardTarjeta
            ' 
            Me.pnlCardTarjeta.Controls.Add(Me.lblValorTarjeta)
            Me.pnlCardTarjeta.Controls.Add(Me.lblHeaderTarjeta)
            Me.pnlCardTarjeta.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardTarjeta.Location = New System.Drawing.Point(245, 8)
            Me.pnlCardTarjeta.Name = "pnlCardTarjeta"
            Me.pnlCardTarjeta.Size = New System.Drawing.Size(221, 104)

            ' 
            ' lblHeaderTarjeta
            ' 
            Me.lblHeaderTarjeta.AutoSize = True
            Me.lblHeaderTarjeta.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTarjeta.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderTarjeta.Location = New System.Drawing.Point(12, 12)
            Me.lblHeaderTarjeta.Text = "VENTAS CON TARJETA / QR"

            ' 
            ' lblValorTarjeta
            ' 
            Me.lblValorTarjeta.AutoSize = True
            Me.lblValorTarjeta.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorTarjeta.Location = New System.Drawing.Point(10, 36)
            Me.lblValorTarjeta.Text = "$0.00"

            ' 
            ' pnlCardTotalVentas
            ' 
            Me.pnlCardTotalVentas.Controls.Add(Me.lblValorTotalVentas)
            Me.pnlCardTotalVentas.Controls.Add(Me.lblHeaderTotalVentas)
            Me.pnlCardTotalVentas.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardTotalVentas.Location = New System.Drawing.Point(472, 8)
            Me.pnlCardTotalVentas.Name = "pnlCardTotalVentas"
            Me.pnlCardTotalVentas.Size = New System.Drawing.Size(221, 104)

            ' 
            ' lblHeaderTotalVentas
            ' 
            Me.lblHeaderTotalVentas.AutoSize = True
            Me.lblHeaderTotalVentas.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTotalVentas.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderTotalVentas.Location = New System.Drawing.Point(12, 12)
            Me.lblHeaderTotalVentas.Text = "INGRESOS TOTALES"

            ' 
            ' lblValorTotalVentas
            ' 
            Me.lblValorTotalVentas.AutoSize = True
            Me.lblValorTotalVentas.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorTotalVentas.Location = New System.Drawing.Point(10, 36)
            Me.lblValorTotalVentas.Text = "$0.00"

            ' 
            ' pnlCardTicketPromedio
            ' 
            Me.pnlCardTicketPromedio.Controls.Add(Me.lblValorTicketPromedio)
            Me.pnlCardTicketPromedio.Controls.Add(Me.lblHeaderTicketPromedio)
            Me.pnlCardTicketPromedio.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardTicketPromedio.Location = New System.Drawing.Point(699, 8)
            Me.pnlCardTicketPromedio.Name = "pnlCardTicketPromedio"
            Me.pnlCardTicketPromedio.Size = New System.Drawing.Size(223, 104)

            ' 
            ' lblHeaderTicketPromedio
            ' 
            Me.lblHeaderTicketPromedio.AutoSize = True
            Me.lblHeaderTicketPromedio.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblHeaderTicketPromedio.ForeColor = System.Drawing.Color.Gray
            Me.lblHeaderTicketPromedio.Location = New System.Drawing.Point(12, 12)
            Me.lblHeaderTicketPromedio.Text = "TICKET PROMEDIO"

            ' 
            ' lblValorTicketPromedio
            ' 
            Me.lblValorTicketPromedio.AutoSize = True
            Me.lblValorTicketPromedio.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
            Me.lblValorTicketPromedio.Location = New System.Drawing.Point(10, 36)
            Me.lblValorTicketPromedio.Text = "$0.00"

            ' 
            ' tlpBottomLayout
            ' 
            Me.tlpBottomLayout.ColumnCount = 2
            Me.tlpBottomLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
            Me.tlpBottomLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
            Me.tlpBottomLayout.Controls.Add(Me.grpListadoVentas, 0, 0)
            Me.tlpBottomLayout.Controls.Add(Me.grpCierreCaja, 1, 0)
            Me.tlpBottomLayout.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpBottomLayout.Location = New System.Drawing.Point(18, 120)
            Me.tlpBottomLayout.Name = "tlpBottomLayout"
            Me.tlpBottomLayout.RowCount = 1
            Me.tlpBottomLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpBottomLayout.Size = New System.Drawing.Size(944, 450)
            Me.tlpBottomLayout.TabIndex = 1

            ' 
            ' grpListadoVentas
            ' 
            Me.grpListadoVentas.Controls.Add(Me.dgvReporteVentas)
            Me.grpListadoVentas.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpListadoVentas.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.grpListadoVentas.Location = New System.Drawing.Point(3, 3)
            Me.grpListadoVentas.Name = "grpListadoVentas"
            Me.grpListadoVentas.Padding = New System.Windows.Forms.Padding(12)
            Me.grpListadoVentas.Size = New System.Drawing.Size(607, 444)
            Me.grpListadoVentas.TabIndex = 0
            Me.grpListadoVentas.TabStop = False
            Me.grpListadoVentas.Text = "Historial de Transacciones Procesadas"

            ' 
            ' dgvReporteVentas
            ' 
            Me.dgvReporteVentas.AllowUserToAddRows = False
            Me.dgvReporteVentas.AllowUserToDeleteRows = False
            Me.dgvReporteVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvReporteVentas.BackgroundColor = System.Drawing.Color.White
            Me.dgvReporteVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvReporteVentas.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvReporteVentas.Location = New System.Drawing.Point(12, 35)
            Me.dgvReporteVentas.MultiSelect = False
            Me.dgvReporteVentas.Name = "dgvReporteVentas"
            Me.dgvReporteVentas.ReadOnly = True
            Me.dgvReporteVentas.RowHeadersVisible = False
            Me.dgvReporteVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvReporteVentas.Size = New System.Drawing.Size(583, 397)
            Me.dgvReporteVentas.TabIndex = 0

            ' 
            ' grpCierreCaja
            ' 
            Me.grpCierreCaja.Controls.Add(Me.btnEjecutarCierreCaja)
            Me.grpCierreCaja.Controls.Add(Me.lblResumenTotalEsperado)
            Me.grpCierreCaja.Controls.Add(Me.lblResumenTarjetaCaja)
            Me.grpCierreCaja.Controls.Add(Me.lblResumenEfectivoCaja)
            Me.grpCierreCaja.Controls.Add(Me.lblResumenFondoCaja)
            Me.grpCierreCaja.Controls.Add(Me.lblInfoArqueo)
            Me.grpCierreCaja.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpCierreCaja.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.grpCierreCaja.Location = New System.Drawing.Point(616, 3)
            Me.grpCierreCaja.Name = "grpCierreCaja"
            Me.grpCierreCaja.Padding = New System.Windows.Forms.Padding(15)
            Me.grpCierreCaja.Size = New System.Drawing.Size(325, 444)
            Me.grpCierreCaja.TabIndex = 1
            Me.grpCierreCaja.TabStop = False
            Me.grpCierreCaja.Text = "Arqueo y Cierre de Turno"

            ' 
            ' lblInfoArqueo
            ' 
            Me.lblInfoArqueo.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblInfoArqueo.ForeColor = System.Drawing.Color.Gray
            Me.lblInfoArqueo.Location = New System.Drawing.Point(15, 30)
            Me.lblInfoArqueo.Name = "lblInfoArqueo"
            Me.lblInfoArqueo.Size = New System.Drawing.Size(295, 38)
            Me.lblInfoArqueo.Text = "Balance consolidado para la entrega del turno fiscal en caja."

            ' 
            ' lblResumenFondoCaja
            ' 
            Me.lblResumenFondoCaja.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblResumenFondoCaja.Location = New System.Drawing.Point(15, 80)
            Me.lblResumenFondoCaja.Name = "lblResumenFondoCaja"
            Me.lblResumenFondoCaja.Size = New System.Drawing.Size(295, 25)
            Me.lblResumenFondoCaja.Text = "Fondo Inicial de Caja: RD$ 2,500.00"

            ' 
            ' lblResumenEfectivoCaja
            ' 
            Me.lblResumenEfectivoCaja.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblResumenEfectivoCaja.Location = New System.Drawing.Point(15, 115)
            Me.lblResumenEfectivoCaja.Name = "lblResumenEfectivoCaja"
            Me.lblResumenEfectivoCaja.Size = New System.Drawing.Size(295, 25)
            Me.lblResumenEfectivoCaja.Text = "Ventas en Efectivo: RD$ 0.00"

            ' 
            ' lblResumenTarjetaCaja
            ' 
            Me.lblResumenTarjetaCaja.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblResumenTarjetaCaja.Location = New System.Drawing.Point(15, 150)
            Me.lblResumenTarjetaCaja.Name = "lblResumenTarjetaCaja"
            Me.lblResumenTarjetaCaja.Size = New System.Drawing.Size(295, 25)
            Me.lblResumenTarjetaCaja.Text = "Ventas en Tarjeta/QR: RD$ 0.00"

            ' 
            ' lblResumenTotalEsperado
            ' 
            Me.lblResumenTotalEsperado.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblResumenTotalEsperado.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblResumenTotalEsperado.Location = New System.Drawing.Point(15, 195)
            Me.lblResumenTotalEsperado.Name = "lblResumenTotalEsperado"
            Me.lblResumenTotalEsperado.Size = New System.Drawing.Size(295, 30)
            Me.lblResumenTotalEsperado.Text = "Total en Caja: RD$ 2,500.00"

            ' 
            ' btnEjecutarCierreCaja
            ' 
            Me.btnEjecutarCierreCaja.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnEjecutarCierreCaja.Location = New System.Drawing.Point(15, 250)
            Me.btnEjecutarCierreCaja.Name = "btnEjecutarCierreCaja"
            Me.btnEjecutarCierreCaja.Size = New System.Drawing.Size(295, 45)
            Me.btnEjecutarCierreCaja.TabIndex = 0
            Me.btnEjecutarCierreCaja.Text = "Ejecutar Cierre Fiscal de Caja"
            Me.btnEjecutarCierreCaja.UseVisualStyleBackColor = True

            ' 
            ' FrmReportesVentas
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0F, 20.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(980, 660)
            Me.Controls.Add(Me.tlpMainLayout)
            Me.Controls.Add(Me.pnlHeaderContainer)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowInTaskbar = False
            Me.Name = "FrmReportesVentas"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.KeyPreview = True
            Me.Text = "Reportes de Ventas y Arqueo de Caja (RF-013)"

            Me.pnlHeaderContainer.ResumeLayout(False)
            Me.pnlHeaderContainer.PerformLayout()
            Me.tlpMainLayout.ResumeLayout(False)

            Me.pnlCardEfectivo.ResumeLayout(False)
            Me.pnlCardEfectivo.PerformLayout()
            Me.pnlCardTarjeta.ResumeLayout(False)
            Me.pnlCardTarjeta.PerformLayout()
            Me.pnlCardTotalVentas.ResumeLayout(False)
            Me.pnlCardTotalVentas.PerformLayout()
            Me.pnlCardTicketPromedio.ResumeLayout(False)
            Me.pnlCardTicketPromedio.PerformLayout()

            Me.tlpBottomLayout.ResumeLayout(False)
            Me.grpListadoVentas.ResumeLayout(False)
            Me.grpCierreCaja.ResumeLayout(False)
            CType(Me.dgvReporteVentas, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
    End Class
End Namespace
