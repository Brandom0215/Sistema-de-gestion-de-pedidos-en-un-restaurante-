Namespace Views.Cocina
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCcnMonitorCocina
        Inherits System.Windows.Forms.Form

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.pnlCcnHeaderPrincipal = New System.Windows.Forms.Panel()
            Me.pnlCcnKpisContenedor = New System.Windows.Forms.Panel()
            Me.pnlCcnKpiVentas = New System.Windows.Forms.Panel()
            Me.lblCcnKpiVentasValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiVentasTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiTMedio = New System.Windows.Forms.Panel()
            Me.lblCcnKpiTMedioValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiTMedioTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiListos = New System.Windows.Forms.Panel()
            Me.lblCcnKpiListosValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiListosTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiEnCocina = New System.Windows.Forms.Panel()
            Me.lblCcnKpiEnCocinaValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiEnCocinaTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiActivos = New System.Windows.Forms.Panel()
            Me.lblCcnKpiActivosValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiActivosTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnTitulosHeader = New System.Windows.Forms.Panel()
            Me.lblCcnTituloPrincipal = New System.Windows.Forms.Label()
            Me.lblCcnSubtituloVivo = New System.Windows.Forms.Label()
            Me.pnlCcnBarraFiltros = New System.Windows.Forms.Panel()
            Me.pnlCcnAccionesDerecha = New System.Windows.Forms.Panel()
            Me.btnCcnRefrescarManual = New System.Windows.Forms.Button()
            Me.btnCcnAlertaSonora = New System.Windows.Forms.Button()
            Me.cboCcnCriterioOrden = New System.Windows.Forms.ComboBox()
            Me.lblCcnOrdenEtiqueta = New System.Windows.Forms.Label()
            Me.pnlCcnChipsFiltro = New System.Windows.Forms.Panel()
            Me.btnCcnFiltroEntregas = New System.Windows.Forms.Button()
            Me.btnCcnFiltroMesa = New System.Windows.Forms.Button()
            Me.btnCcnFiltroTodos = New System.Windows.Forms.Button()
            Me.flpCcnContenedorComandas = New System.Windows.Forms.FlowLayoutPanel()
            Me.tmrCcnActualizadorRealTime = New System.Windows.Forms.Timer(Me.components)
            Me.pnlCcnHeaderPrincipal.SuspendLayout()
            Me.pnlCcnKpisContenedor.SuspendLayout()
            Me.pnlCcnKpiVentas.SuspendLayout()
            Me.pnlCcnKpiTMedio.SuspendLayout()
            Me.pnlCcnKpiListos.SuspendLayout()
            Me.pnlCcnKpiEnCocina.SuspendLayout()
            Me.pnlCcnKpiActivos.SuspendLayout()
            Me.pnlCcnTitulosHeader.SuspendLayout()
            Me.pnlCcnBarraFiltros.SuspendLayout()
            Me.pnlCcnAccionesDerecha.SuspendLayout()
            Me.pnlCcnChipsFiltro.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlCcnHeaderPrincipal
            '
            Me.pnlCcnHeaderPrincipal.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.pnlCcnHeaderPrincipal.Controls.Add(Me.pnlCcnKpisContenedor)
            Me.pnlCcnHeaderPrincipal.Controls.Add(Me.pnlCcnTitulosHeader)
            Me.pnlCcnHeaderPrincipal.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnHeaderPrincipal.Location = New System.Drawing.Point(0, 0)
            Me.pnlCcnHeaderPrincipal.Name = "pnlCcnHeaderPrincipal"
            Me.pnlCcnHeaderPrincipal.Padding = New System.Windows.Forms.Padding(18, 14, 18, 10)
            Me.pnlCcnHeaderPrincipal.Size = New System.Drawing.Size(1080, 84)
            Me.pnlCcnHeaderPrincipal.TabIndex = 0
            '
            'pnlCcnKpisContenedor
            '
            Me.pnlCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiVentas)
            Me.pnlCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiTMedio)
            Me.pnlCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiListos)
            Me.pnlCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiEnCocina)
            Me.pnlCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiActivos)
            Me.pnlCcnKpisContenedor.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlCcnKpisContenedor.Location = New System.Drawing.Point(510, 14)
            Me.pnlCcnKpisContenedor.Name = "pnlCcnKpisContenedor"
            Me.pnlCcnKpisContenedor.Size = New System.Drawing.Size(552, 60)
            Me.pnlCcnKpisContenedor.TabIndex = 1
            '
            'pnlCcnKpiVentas
            '
            Me.pnlCcnKpiVentas.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.pnlCcnKpiVentas.Controls.Add(Me.lblCcnKpiVentasValor)
            Me.pnlCcnKpiVentas.Controls.Add(Me.lblCcnKpiVentasTitulo)
            Me.pnlCcnKpiVentas.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlCcnKpiVentas.Location = New System.Drawing.Point(422, 0)
            Me.pnlCcnKpiVentas.Name = "pnlCcnKpiVentas"
            Me.pnlCcnKpiVentas.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
            Me.pnlCcnKpiVentas.Size = New System.Drawing.Size(130, 60)
            Me.pnlCcnKpiVentas.TabIndex = 4
            '
            'lblCcnKpiVentasValor
            '
            Me.lblCcnKpiVentasValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiVentasValor.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiVentasValor.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnKpiVentasValor.Location = New System.Drawing.Point(8, 24)
            Me.lblCcnKpiVentasValor.Name = "lblCcnKpiVentasValor"
            Me.lblCcnKpiVentasValor.Size = New System.Drawing.Size(114, 30)
            Me.lblCcnKpiVentasValor.TabIndex = 1
            Me.lblCcnKpiVentasValor.Text = "$1,420.00"
            Me.lblCcnKpiVentasValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCcnKpiVentasTitulo
            '
            Me.lblCcnKpiVentasTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiVentasTitulo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnKpiVentasTitulo.ForeColor = System.Drawing.Color.FromArgb(117, 70, 50)
            Me.lblCcnKpiVentasTitulo.Location = New System.Drawing.Point(8, 6)
            Me.lblCcnKpiVentasTitulo.Name = "lblCcnKpiVentasTitulo"
            Me.lblCcnKpiVentasTitulo.Size = New System.Drawing.Size(114, 18)
            Me.lblCcnKpiVentasTitulo.TabIndex = 0
            Me.lblCcnKpiVentasTitulo.Text = "💰 Ventas Turno"
            Me.lblCcnKpiVentasTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCcnKpiTMedio
            '
            Me.pnlCcnKpiTMedio.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiTMedio.Controls.Add(Me.lblCcnKpiTMedioValor)
            Me.pnlCcnKpiTMedio.Controls.Add(Me.lblCcnKpiTMedioTitulo)
            Me.pnlCcnKpiTMedio.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnKpiTMedio.Location = New System.Drawing.Point(312, 0)
            Me.pnlCcnKpiTMedio.Name = "pnlCcnKpiTMedio"
            Me.pnlCcnKpiTMedio.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
            Me.pnlCcnKpiTMedio.Size = New System.Drawing.Size(100, 60)
            Me.pnlCcnKpiTMedio.TabIndex = 3
            '
            'lblCcnKpiTMedioValor
            '
            Me.lblCcnKpiTMedioValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiTMedioValor.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiTMedioValor.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnKpiTMedioValor.Location = New System.Drawing.Point(8, 24)
            Me.lblCcnKpiTMedioValor.Name = "lblCcnKpiTMedioValor"
            Me.lblCcnKpiTMedioValor.Size = New System.Drawing.Size(84, 30)
            Me.lblCcnKpiTMedioValor.TabIndex = 1
            Me.lblCcnKpiTMedioValor.Text = "14 min"
            Me.lblCcnKpiTMedioValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCcnKpiTMedioTitulo
            '
            Me.lblCcnKpiTMedioTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiTMedioTitulo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnKpiTMedioTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnKpiTMedioTitulo.Location = New System.Drawing.Point(8, 6)
            Me.lblCcnKpiTMedioTitulo.Name = "lblCcnKpiTMedioTitulo"
            Me.lblCcnKpiTMedioTitulo.Size = New System.Drawing.Size(84, 18)
            Me.lblCcnKpiTMedioTitulo.TabIndex = 0
            Me.lblCcnKpiTMedioTitulo.Text = "⏱ T. Medio"
            Me.lblCcnKpiTMedioTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCcnKpiListos
            '
            Me.pnlCcnKpiListos.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiListos.Controls.Add(Me.lblCcnKpiListosValor)
            Me.pnlCcnKpiListos.Controls.Add(Me.lblCcnKpiListosTitulo)
            Me.pnlCcnKpiListos.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnKpiListos.Location = New System.Drawing.Point(208, 0)
            Me.pnlCcnKpiListos.Name = "pnlCcnKpiListos"
            Me.pnlCcnKpiListos.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
            Me.pnlCcnKpiListos.Size = New System.Drawing.Size(100, 60)
            Me.pnlCcnKpiListos.TabIndex = 2
            '
            'lblCcnKpiListosValor
            '
            Me.lblCcnKpiListosValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiListosValor.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiListosValor.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblCcnKpiListosValor.Location = New System.Drawing.Point(8, 24)
            Me.lblCcnKpiListosValor.Name = "lblCcnKpiListosValor"
            Me.lblCcnKpiListosValor.Size = New System.Drawing.Size(84, 30)
            Me.lblCcnKpiListosValor.TabIndex = 1
            Me.lblCcnKpiListosValor.Text = "2"
            Me.lblCcnKpiListosValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCcnKpiListosTitulo
            '
            Me.lblCcnKpiListosTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiListosTitulo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnKpiListosTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnKpiListosTitulo.Location = New System.Drawing.Point(8, 6)
            Me.lblCcnKpiListosTitulo.Name = "lblCcnKpiListosTitulo"
            Me.lblCcnKpiListosTitulo.Size = New System.Drawing.Size(84, 18)
            Me.lblCcnKpiListosTitulo.TabIndex = 0
            Me.lblCcnKpiListosTitulo.Text = "✔ Listos"
            Me.lblCcnKpiListosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCcnKpiEnCocina
            '
            Me.pnlCcnKpiEnCocina.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiEnCocina.Controls.Add(Me.lblCcnKpiEnCocinaValor)
            Me.pnlCcnKpiEnCocina.Controls.Add(Me.lblCcnKpiEnCocinaTitulo)
            Me.pnlCcnKpiEnCocina.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnKpiEnCocina.Location = New System.Drawing.Point(104, 0)
            Me.pnlCcnKpiEnCocina.Name = "pnlCcnKpiEnCocina"
            Me.pnlCcnKpiEnCocina.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
            Me.pnlCcnKpiEnCocina.Size = New System.Drawing.Size(104, 60)
            Me.pnlCcnKpiEnCocina.TabIndex = 1
            '
            'lblCcnKpiEnCocinaValor
            '
            Me.lblCcnKpiEnCocinaValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiEnCocinaValor.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiEnCocinaValor.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblCcnKpiEnCocinaValor.Location = New System.Drawing.Point(8, 24)
            Me.lblCcnKpiEnCocinaValor.Name = "lblCcnKpiEnCocinaValor"
            Me.lblCcnKpiEnCocinaValor.Size = New System.Drawing.Size(88, 30)
            Me.lblCcnKpiEnCocinaValor.TabIndex = 1
            Me.lblCcnKpiEnCocinaValor.Text = "3"
            Me.lblCcnKpiEnCocinaValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCcnKpiEnCocinaTitulo
            '
            Me.lblCcnKpiEnCocinaTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiEnCocinaTitulo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnKpiEnCocinaTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnKpiEnCocinaTitulo.Location = New System.Drawing.Point(8, 6)
            Me.lblCcnKpiEnCocinaTitulo.Name = "lblCcnKpiEnCocinaTitulo"
            Me.lblCcnKpiEnCocinaTitulo.Size = New System.Drawing.Size(88, 18)
            Me.lblCcnKpiEnCocinaTitulo.TabIndex = 0
            Me.lblCcnKpiEnCocinaTitulo.Text = "🍳 En Cocina"
            Me.lblCcnKpiEnCocinaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCcnKpiActivos
            '
            Me.pnlCcnKpiActivos.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiActivos.Controls.Add(Me.lblCcnKpiActivosValor)
            Me.pnlCcnKpiActivos.Controls.Add(Me.lblCcnKpiActivosTitulo)
            Me.pnlCcnKpiActivos.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnKpiActivos.Location = New System.Drawing.Point(0, 0)
            Me.pnlCcnKpiActivos.Name = "pnlCcnKpiActivos"
            Me.pnlCcnKpiActivos.Padding = New System.Windows.Forms.Padding(8, 6, 8, 6)
            Me.pnlCcnKpiActivos.Size = New System.Drawing.Size(100, 60)
            Me.pnlCcnKpiActivos.TabIndex = 0
            '
            'lblCcnKpiActivosValor
            '
            Me.lblCcnKpiActivosValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiActivosValor.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiActivosValor.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnKpiActivosValor.Location = New System.Drawing.Point(8, 24)
            Me.lblCcnKpiActivosValor.Name = "lblCcnKpiActivosValor"
            Me.lblCcnKpiActivosValor.Size = New System.Drawing.Size(84, 30)
            Me.lblCcnKpiActivosValor.TabIndex = 1
            Me.lblCcnKpiActivosValor.Text = "8"
            Me.lblCcnKpiActivosValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCcnKpiActivosTitulo
            '
            Me.lblCcnKpiActivosTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiActivosTitulo.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnKpiActivosTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnKpiActivosTitulo.Location = New System.Drawing.Point(8, 6)
            Me.lblCcnKpiActivosTitulo.Name = "lblCcnKpiActivosTitulo"
            Me.lblCcnKpiActivosTitulo.Size = New System.Drawing.Size(84, 18)
            Me.lblCcnKpiActivosTitulo.TabIndex = 0
            Me.lblCcnKpiActivosTitulo.Text = "📋 Activos"
            Me.lblCcnKpiActivosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pnlCcnTitulosHeader
            '
            Me.pnlCcnTitulosHeader.Controls.Add(Me.lblCcnTituloPrincipal)
            Me.pnlCcnTitulosHeader.Controls.Add(Me.lblCcnSubtituloVivo)
            Me.pnlCcnTitulosHeader.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnTitulosHeader.Location = New System.Drawing.Point(18, 14)
            Me.pnlCcnTitulosHeader.Name = "pnlCcnTitulosHeader"
            Me.pnlCcnTitulosHeader.Size = New System.Drawing.Size(460, 60)
            Me.pnlCcnTitulosHeader.TabIndex = 0
            '
            'lblCcnTituloPrincipal
            '
            Me.lblCcnTituloPrincipal.AutoSize = True
            Me.lblCcnTituloPrincipal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnTituloPrincipal.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnTituloPrincipal.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnTituloPrincipal.Location = New System.Drawing.Point(0, 20)
            Me.lblCcnTituloPrincipal.Name = "lblCcnTituloPrincipal"
            Me.lblCcnTituloPrincipal.Size = New System.Drawing.Size(298, 28)
            Me.lblCcnTituloPrincipal.TabIndex = 1
            Me.lblCcnTituloPrincipal.Text = "Monitor de Comandas & Cocina"
            '
            'lblCcnSubtituloVivo
            '
            Me.lblCcnSubtituloVivo.AutoSize = True
            Me.lblCcnSubtituloVivo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnSubtituloVivo.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnSubtituloVivo.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnSubtituloVivo.Location = New System.Drawing.Point(0, 0)
            Me.lblCcnSubtituloVivo.Name = "lblCcnSubtituloVivo"
            Me.lblCcnSubtituloVivo.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
            Me.lblCcnSubtituloVivo.Size = New System.Drawing.Size(325, 20)
            Me.lblCcnSubtituloVivo.TabIndex = 0
            Me.lblCcnSubtituloVivo.Text = "🔴 DESPACHO & COCINA EN VIVO  /  Servicio Almuerzo"
            '
            'pnlCcnBarraFiltros
            '
            Me.pnlCcnBarraFiltros.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.pnlCcnBarraFiltros.Controls.Add(Me.pnlCcnAccionesDerecha)
            Me.pnlCcnBarraFiltros.Controls.Add(Me.pnlCcnChipsFiltro)
            Me.pnlCcnBarraFiltros.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnBarraFiltros.Location = New System.Drawing.Point(0, 84)
            Me.pnlCcnBarraFiltros.Name = "pnlCcnBarraFiltros"
            Me.pnlCcnBarraFiltros.Padding = New System.Windows.Forms.Padding(18, 4, 18, 10)
            Me.pnlCcnBarraFiltros.Size = New System.Drawing.Size(1080, 48)
            Me.pnlCcnBarraFiltros.TabIndex = 1
            '
            'pnlCcnAccionesDerecha
            '
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.btnCcnRefrescarManual)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.btnCcnAlertaSonora)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.cboCcnCriterioOrden)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.lblCcnOrdenEtiqueta)
            Me.pnlCcnAccionesDerecha.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlCcnAccionesDerecha.Location = New System.Drawing.Point(620, 4)
            Me.pnlCcnAccionesDerecha.Name = "pnlCcnAccionesDerecha"
            Me.pnlCcnAccionesDerecha.Size = New System.Drawing.Size(442, 34)
            Me.pnlCcnAccionesDerecha.TabIndex = 1
            '
            'btnCcnRefrescarManual
            '
            Me.btnCcnRefrescarManual.BackColor = System.Drawing.Color.White
            Me.btnCcnRefrescarManual.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnRefrescarManual.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnRefrescarManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnRefrescarManual.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnCcnRefrescarManual.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnRefrescarManual.Location = New System.Drawing.Point(398, 2)
            Me.btnCcnRefrescarManual.Name = "btnCcnRefrescarManual"
            Me.btnCcnRefrescarManual.Size = New System.Drawing.Size(40, 28)
            Me.btnCcnRefrescarManual.TabIndex = 3
            Me.btnCcnRefrescarManual.Text = "🔄"
            Me.btnCcnRefrescarManual.UseVisualStyleBackColor = False
            '
            'btnCcnAlertaSonora
            '
            Me.btnCcnAlertaSonora.BackColor = System.Drawing.Color.White
            Me.btnCcnAlertaSonora.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnAlertaSonora.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnAlertaSonora.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnAlertaSonora.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnCcnAlertaSonora.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnAlertaSonora.Location = New System.Drawing.Point(352, 2)
            Me.btnCcnAlertaSonora.Name = "btnCcnAlertaSonora"
            Me.btnCcnAlertaSonora.Size = New System.Drawing.Size(40, 28)
            Me.btnCcnAlertaSonora.TabIndex = 2
            Me.btnCcnAlertaSonora.Text = "🔔"
            Me.btnCcnAlertaSonora.UseVisualStyleBackColor = False
            '
            'cboCcnCriterioOrden
            '
            Me.cboCcnCriterioOrden.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCcnCriterioOrden.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.cboCcnCriterioOrden.FormattingEnabled = True
            Me.cboCcnCriterioOrden.Location = New System.Drawing.Point(88, 5)
            Me.cboCcnCriterioOrden.Name = "cboCcnCriterioOrden"
            Me.cboCcnCriterioOrden.Size = New System.Drawing.Size(256, 23)
            Me.cboCcnCriterioOrden.TabIndex = 1
            '
            'lblCcnOrdenEtiqueta
            '
            Me.lblCcnOrdenEtiqueta.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
            Me.lblCcnOrdenEtiqueta.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnOrdenEtiqueta.Location = New System.Drawing.Point(12, 6)
            Me.lblCcnOrdenEtiqueta.Name = "lblCcnOrdenEtiqueta"
            Me.lblCcnOrdenEtiqueta.Size = New System.Drawing.Size(70, 20)
            Me.lblCcnOrdenEtiqueta.TabIndex = 0
            Me.lblCcnOrdenEtiqueta.Text = "⇅ Orden:"
            Me.lblCcnOrdenEtiqueta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'pnlCcnChipsFiltro
            '
            Me.pnlCcnChipsFiltro.Controls.Add(Me.btnCcnFiltroEntregas)
            Me.pnlCcnChipsFiltro.Controls.Add(Me.btnCcnFiltroMesa)
            Me.pnlCcnChipsFiltro.Controls.Add(Me.btnCcnFiltroTodos)
            Me.pnlCcnChipsFiltro.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnChipsFiltro.Location = New System.Drawing.Point(18, 4)
            Me.pnlCcnChipsFiltro.Name = "pnlCcnChipsFiltro"
            Me.pnlCcnChipsFiltro.Size = New System.Drawing.Size(520, 34)
            Me.pnlCcnChipsFiltro.TabIndex = 0
            '
            'btnCcnFiltroEntregas
            '
            Me.btnCcnFiltroEntregas.BackColor = System.Drawing.Color.White
            Me.btnCcnFiltroEntregas.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnFiltroEntregas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnFiltroEntregas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnFiltroEntregas.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.btnCcnFiltroEntregas.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnFiltroEntregas.Location = New System.Drawing.Point(318, 2)
            Me.btnCcnFiltroEntregas.Name = "btnCcnFiltroEntregas"
            Me.btnCcnFiltroEntregas.Size = New System.Drawing.Size(146, 28)
            Me.btnCcnFiltroEntregas.TabIndex = 2
            Me.btnCcnFiltroEntregas.Text = "🛍 Entregas (2)"
            Me.btnCcnFiltroEntregas.UseVisualStyleBackColor = False
            '
            'btnCcnFiltroMesa
            '
            Me.btnCcnFiltroMesa.BackColor = System.Drawing.Color.White
            Me.btnCcnFiltroMesa.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnFiltroMesa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnFiltroMesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnFiltroMesa.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.btnCcnFiltroMesa.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnFiltroMesa.Location = New System.Drawing.Point(164, 2)
            Me.btnCcnFiltroMesa.Name = "btnCcnFiltroMesa"
            Me.btnCcnFiltroMesa.Size = New System.Drawing.Size(146, 28)
            Me.btnCcnFiltroMesa.TabIndex = 1
            Me.btnCcnFiltroMesa.Text = "🍽 Mesa / Salón (5)"
            Me.btnCcnFiltroMesa.UseVisualStyleBackColor = False
            '
            'btnCcnFiltroTodos
            '
            Me.btnCcnFiltroTodos.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.btnCcnFiltroTodos.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnFiltroTodos.FlatAppearance.BorderSize = 0
            Me.btnCcnFiltroTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnFiltroTodos.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnCcnFiltroTodos.ForeColor = System.Drawing.Color.White
            Me.btnCcnFiltroTodos.Location = New System.Drawing.Point(0, 2)
            Me.btnCcnFiltroTodos.Name = "btnCcnFiltroTodos"
            Me.btnCcnFiltroTodos.Size = New System.Drawing.Size(156, 28)
            Me.btnCcnFiltroTodos.TabIndex = 0
            Me.btnCcnFiltroTodos.Text = "Todos los pedidos (8)"
            Me.btnCcnFiltroTodos.UseVisualStyleBackColor = False
            '
            'flpCcnContenedorComandas
            '
            Me.flpCcnContenedorComandas.AutoScroll = True
            Me.flpCcnContenedorComandas.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.flpCcnContenedorComandas.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpCcnContenedorComandas.Location = New System.Drawing.Point(0, 132)
            Me.flpCcnContenedorComandas.Name = "flpCcnContenedorComandas"
            Me.flpCcnContenedorComandas.Padding = New System.Windows.Forms.Padding(16, 6, 16, 16)
            Me.flpCcnContenedorComandas.Size = New System.Drawing.Size(1080, 528)
            Me.flpCcnContenedorComandas.TabIndex = 2
            '
            'tmrCcnActualizadorRealTime
            '
            Me.tmrCcnActualizadorRealTime.Interval = 10000
            '
            'FrmCcnMonitorCocina
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(1080, 660)
            Me.Controls.Add(Me.flpCcnContenedorComandas)
            Me.Controls.Add(Me.pnlCcnBarraFiltros)
            Me.Controls.Add(Me.pnlCcnHeaderPrincipal)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmCcnMonitorCocina"
            Me.Text = "Monitor de Cocina KDS"
            Me.pnlCcnHeaderPrincipal.ResumeLayout(False)
            Me.pnlCcnKpisContenedor.ResumeLayout(False)
            Me.pnlCcnKpiVentas.ResumeLayout(False)
            Me.pnlCcnKpiTMedio.ResumeLayout(False)
            Me.pnlCcnKpiListos.ResumeLayout(False)
            Me.pnlCcnKpiEnCocina.ResumeLayout(False)
            Me.pnlCcnKpiActivos.ResumeLayout(False)
            Me.pnlCcnTitulosHeader.ResumeLayout(False)
            Me.pnlCcnTitulosHeader.PerformLayout()
            Me.pnlCcnBarraFiltros.ResumeLayout(False)
            Me.pnlCcnAccionesDerecha.ResumeLayout(False)
            Me.pnlCcnChipsFiltro.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCcnHeaderPrincipal As System.Windows.Forms.Panel
        Friend WithEvents pnlCcnTitulosHeader As System.Windows.Forms.Panel
        Friend WithEvents lblCcnTituloPrincipal As System.Windows.Forms.Label
        Friend WithEvents lblCcnSubtituloVivo As System.Windows.Forms.Label
        Friend WithEvents pnlCcnKpisContenedor As System.Windows.Forms.Panel
        Friend WithEvents pnlCcnKpiActivos As System.Windows.Forms.Panel
        Friend WithEvents lblCcnKpiActivosTitulo As System.Windows.Forms.Label
        Friend WithEvents lblCcnKpiActivosValor As System.Windows.Forms.Label
        Friend WithEvents pnlCcnKpiEnCocina As System.Windows.Forms.Panel
        Friend WithEvents lblCcnKpiEnCocinaTitulo As System.Windows.Forms.Label
        Friend WithEvents lblCcnKpiEnCocinaValor As System.Windows.Forms.Label
        Friend WithEvents pnlCcnKpiListos As System.Windows.Forms.Panel
        Friend WithEvents lblCcnKpiListosTitulo As System.Windows.Forms.Label
        Friend WithEvents lblCcnKpiListosValor As System.Windows.Forms.Label
        Friend WithEvents pnlCcnKpiTMedio As System.Windows.Forms.Panel
        Friend WithEvents lblCcnKpiTMedioTitulo As System.Windows.Forms.Label
        Friend WithEvents lblCcnKpiTMedioValor As System.Windows.Forms.Label
        Friend WithEvents pnlCcnKpiVentas As System.Windows.Forms.Panel
        Friend WithEvents lblCcnKpiVentasTitulo As System.Windows.Forms.Label
        Friend WithEvents lblCcnKpiVentasValor As System.Windows.Forms.Label
        Friend WithEvents pnlCcnBarraFiltros As System.Windows.Forms.Panel
        Friend WithEvents pnlCcnChipsFiltro As System.Windows.Forms.Panel
        Friend WithEvents btnCcnFiltroTodos As System.Windows.Forms.Button
        Friend WithEvents btnCcnFiltroMesa As System.Windows.Forms.Button
        Friend WithEvents btnCcnFiltroEntregas As System.Windows.Forms.Button
        Friend WithEvents pnlCcnAccionesDerecha As System.Windows.Forms.Panel
        Friend WithEvents lblCcnOrdenEtiqueta As System.Windows.Forms.Label
        Friend WithEvents cboCcnCriterioOrden As System.Windows.Forms.ComboBox
        Friend WithEvents btnCcnAlertaSonora As System.Windows.Forms.Button
        Friend WithEvents btnCcnRefrescarManual As System.Windows.Forms.Button
        Friend WithEvents flpCcnContenedorComandas As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents tmrCcnActualizadorRealTime As System.Windows.Forms.Timer
    End Class
End Namespace
