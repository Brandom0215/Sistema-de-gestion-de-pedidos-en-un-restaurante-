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
            Me.flpCcnKpisContenedor = New System.Windows.Forms.FlowLayoutPanel()
            Me.pnlCcnKpiActivos = New System.Windows.Forms.Panel()
            Me.lblCcnKpiActivosValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiActivosTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiEnCocina = New System.Windows.Forms.Panel()
            Me.lblCcnKpiEnCocinaValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiEnCocinaTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiListos = New System.Windows.Forms.Panel()
            Me.lblCcnKpiListosValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiListosTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiTMedio = New System.Windows.Forms.Panel()
            Me.lblCcnKpiTMedioValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiTMedioTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnKpiVentas = New System.Windows.Forms.Panel()
            Me.lblCcnKpiVentasValor = New System.Windows.Forms.Label()
            Me.lblCcnKpiVentasTitulo = New System.Windows.Forms.Label()
            Me.pnlCcnTitulosHeader = New System.Windows.Forms.Panel()
            Me.lblCcnTituloPrincipal = New System.Windows.Forms.Label()
            Me.lblCcnSubtituloVivo = New System.Windows.Forms.Label()
            Me.pnlCcnBarraFiltros = New System.Windows.Forms.Panel()
            Me.pnlCcnAccionesDerecha = New System.Windows.Forms.Panel()
            Me.btnCcnNuevoPedido = New System.Windows.Forms.Button()
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
            Me.flpCcnKpisContenedor.SuspendLayout()
            Me.pnlCcnKpiActivos.SuspendLayout()
            Me.pnlCcnKpiEnCocina.SuspendLayout()
            Me.pnlCcnKpiListos.SuspendLayout()
            Me.pnlCcnKpiTMedio.SuspendLayout()
            Me.pnlCcnKpiVentas.SuspendLayout()
            Me.pnlCcnTitulosHeader.SuspendLayout()
            Me.pnlCcnBarraFiltros.SuspendLayout()
            Me.pnlCcnAccionesDerecha.SuspendLayout()
            Me.pnlCcnChipsFiltro.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlCcnHeaderPrincipal
            '
            Me.pnlCcnHeaderPrincipal.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.pnlCcnHeaderPrincipal.Controls.Add(Me.flpCcnKpisContenedor)
            Me.pnlCcnHeaderPrincipal.Controls.Add(Me.pnlCcnTitulosHeader)
            Me.pnlCcnHeaderPrincipal.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnHeaderPrincipal.Location = New System.Drawing.Point(0, 0)
            Me.pnlCcnHeaderPrincipal.Name = "pnlCcnHeaderPrincipal"
            Me.pnlCcnHeaderPrincipal.Padding = New System.Windows.Forms.Padding(18, 10, 18, 8)
            Me.pnlCcnHeaderPrincipal.Size = New System.Drawing.Size(1100, 96)
            Me.pnlCcnHeaderPrincipal.TabIndex = 0
            '
            'flpCcnKpisContenedor
            '
            Me.flpCcnKpisContenedor.AutoSize = True
            Me.flpCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiActivos)
            Me.flpCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiEnCocina)
            Me.flpCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiListos)
            Me.flpCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiTMedio)
            Me.flpCcnKpisContenedor.Controls.Add(Me.pnlCcnKpiVentas)
            Me.flpCcnKpisContenedor.Dock = System.Windows.Forms.DockStyle.Right
            Me.flpCcnKpisContenedor.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
            Me.flpCcnKpisContenedor.Location = New System.Drawing.Point(470, 10)
            Me.flpCcnKpisContenedor.Name = "flpCcnKpisContenedor"
            Me.flpCcnKpisContenedor.Size = New System.Drawing.Size(612, 78)
            Me.flpCcnKpisContenedor.TabIndex = 1
            Me.flpCcnKpisContenedor.WrapContents = False
            '
            'pnlCcnKpiActivos
            '
            Me.pnlCcnKpiActivos.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiActivos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnKpiActivos.Controls.Add(Me.lblCcnKpiActivosValor)
            Me.pnlCcnKpiActivos.Controls.Add(Me.lblCcnKpiActivosTitulo)
            Me.pnlCcnKpiActivos.Location = New System.Drawing.Point(0, 0)
            Me.pnlCcnKpiActivos.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
            Me.pnlCcnKpiActivos.Name = "pnlCcnKpiActivos"
            Me.pnlCcnKpiActivos.Padding = New System.Windows.Forms.Padding(4)
            Me.pnlCcnKpiActivos.Size = New System.Drawing.Size(104, 76)
            Me.pnlCcnKpiActivos.TabIndex = 0
            '
            'lblCcnKpiActivosValor
            '
            Me.lblCcnKpiActivosValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiActivosValor.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiActivosValor.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnKpiActivosValor.Location = New System.Drawing.Point(4, 26)
            Me.lblCcnKpiActivosValor.Name = "lblCcnKpiActivosValor"
            Me.lblCcnKpiActivosValor.Size = New System.Drawing.Size(94, 44)
            Me.lblCcnKpiActivosValor.TabIndex = 1
            Me.lblCcnKpiActivosValor.Text = "8"
            Me.lblCcnKpiActivosValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiActivosValor.UseMnemonic = False
            '
            'lblCcnKpiActivosTitulo
            '
            Me.lblCcnKpiActivosTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiActivosTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiActivosTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnKpiActivosTitulo.Location = New System.Drawing.Point(4, 4)
            Me.lblCcnKpiActivosTitulo.Name = "lblCcnKpiActivosTitulo"
            Me.lblCcnKpiActivosTitulo.Size = New System.Drawing.Size(94, 22)
            Me.lblCcnKpiActivosTitulo.TabIndex = 0
            Me.lblCcnKpiActivosTitulo.Text = "📋 Activos"
            Me.lblCcnKpiActivosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiActivosTitulo.UseMnemonic = False
            '
            'pnlCcnKpiEnCocina
            '
            Me.pnlCcnKpiEnCocina.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiEnCocina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnKpiEnCocina.Controls.Add(Me.lblCcnKpiEnCocinaValor)
            Me.pnlCcnKpiEnCocina.Controls.Add(Me.lblCcnKpiEnCocinaTitulo)
            Me.pnlCcnKpiEnCocina.Location = New System.Drawing.Point(112, 0)
            Me.pnlCcnKpiEnCocina.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
            Me.pnlCcnKpiEnCocina.Name = "pnlCcnKpiEnCocina"
            Me.pnlCcnKpiEnCocina.Padding = New System.Windows.Forms.Padding(4)
            Me.pnlCcnKpiEnCocina.Size = New System.Drawing.Size(118, 76)
            Me.pnlCcnKpiEnCocina.TabIndex = 1
            '
            'lblCcnKpiEnCocinaValor
            '
            Me.lblCcnKpiEnCocinaValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiEnCocinaValor.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiEnCocinaValor.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblCcnKpiEnCocinaValor.Location = New System.Drawing.Point(4, 26)
            Me.lblCcnKpiEnCocinaValor.Name = "lblCcnKpiEnCocinaValor"
            Me.lblCcnKpiEnCocinaValor.Size = New System.Drawing.Size(108, 44)
            Me.lblCcnKpiEnCocinaValor.TabIndex = 1
            Me.lblCcnKpiEnCocinaValor.Text = "3"
            Me.lblCcnKpiEnCocinaValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiEnCocinaValor.UseMnemonic = False
            '
            'lblCcnKpiEnCocinaTitulo
            '
            Me.lblCcnKpiEnCocinaTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiEnCocinaTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiEnCocinaTitulo.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnKpiEnCocinaTitulo.Location = New System.Drawing.Point(4, 4)
            Me.lblCcnKpiEnCocinaTitulo.Name = "lblCcnKpiEnCocinaTitulo"
            Me.lblCcnKpiEnCocinaTitulo.Size = New System.Drawing.Size(108, 22)
            Me.lblCcnKpiEnCocinaTitulo.TabIndex = 0
            Me.lblCcnKpiEnCocinaTitulo.Text = "🍳 En Cocina"
            Me.lblCcnKpiEnCocinaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiEnCocinaTitulo.UseMnemonic = False
            '
            'pnlCcnKpiListos
            '
            Me.pnlCcnKpiListos.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiListos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnKpiListos.Controls.Add(Me.lblCcnKpiListosValor)
            Me.pnlCcnKpiListos.Controls.Add(Me.lblCcnKpiListosTitulo)
            Me.pnlCcnKpiListos.Location = New System.Drawing.Point(238, 0)
            Me.pnlCcnKpiListos.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
            Me.pnlCcnKpiListos.Name = "pnlCcnKpiListos"
            Me.pnlCcnKpiListos.Padding = New System.Windows.Forms.Padding(4)
            Me.pnlCcnKpiListos.Size = New System.Drawing.Size(104, 76)
            Me.pnlCcnKpiListos.TabIndex = 2
            '
            'lblCcnKpiListosValor
            '
            Me.lblCcnKpiListosValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiListosValor.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiListosValor.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblCcnKpiListosValor.Location = New System.Drawing.Point(4, 26)
            Me.lblCcnKpiListosValor.Name = "lblCcnKpiListosValor"
            Me.lblCcnKpiListosValor.Size = New System.Drawing.Size(94, 44)
            Me.lblCcnKpiListosValor.TabIndex = 1
            Me.lblCcnKpiListosValor.Text = "2"
            Me.lblCcnKpiListosValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiListosValor.UseMnemonic = False
            '
            'lblCcnKpiListosTitulo
            '
            Me.lblCcnKpiListosTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiListosTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiListosTitulo.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblCcnKpiListosTitulo.Location = New System.Drawing.Point(4, 4)
            Me.lblCcnKpiListosTitulo.Name = "lblCcnKpiListosTitulo"
            Me.lblCcnKpiListosTitulo.Size = New System.Drawing.Size(94, 22)
            Me.lblCcnKpiListosTitulo.TabIndex = 0
            Me.lblCcnKpiListosTitulo.Text = "✔ Listos"
            Me.lblCcnKpiListosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiListosTitulo.UseMnemonic = False
            '
            'pnlCcnKpiTMedio
            '
            Me.pnlCcnKpiTMedio.BackColor = System.Drawing.Color.White
            Me.pnlCcnKpiTMedio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnKpiTMedio.Controls.Add(Me.lblCcnKpiTMedioValor)
            Me.pnlCcnKpiTMedio.Controls.Add(Me.lblCcnKpiTMedioTitulo)
            Me.pnlCcnKpiTMedio.Location = New System.Drawing.Point(350, 0)
            Me.pnlCcnKpiTMedio.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
            Me.pnlCcnKpiTMedio.Name = "pnlCcnKpiTMedio"
            Me.pnlCcnKpiTMedio.Padding = New System.Windows.Forms.Padding(4)
            Me.pnlCcnKpiTMedio.Size = New System.Drawing.Size(116, 76)
            Me.pnlCcnKpiTMedio.TabIndex = 3
            '
            'lblCcnKpiTMedioValor
            '
            Me.lblCcnKpiTMedioValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiTMedioValor.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiTMedioValor.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnKpiTMedioValor.Location = New System.Drawing.Point(4, 26)
            Me.lblCcnKpiTMedioValor.Name = "lblCcnKpiTMedioValor"
            Me.lblCcnKpiTMedioValor.Size = New System.Drawing.Size(106, 44)
            Me.lblCcnKpiTMedioValor.TabIndex = 1
            Me.lblCcnKpiTMedioValor.Text = "14 min"
            Me.lblCcnKpiTMedioValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiTMedioValor.UseMnemonic = False
            '
            'lblCcnKpiTMedioTitulo
            '
            Me.lblCcnKpiTMedioTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiTMedioTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiTMedioTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnKpiTMedioTitulo.Location = New System.Drawing.Point(4, 4)
            Me.lblCcnKpiTMedioTitulo.Name = "lblCcnKpiTMedioTitulo"
            Me.lblCcnKpiTMedioTitulo.Size = New System.Drawing.Size(106, 22)
            Me.lblCcnKpiTMedioTitulo.TabIndex = 0
            Me.lblCcnKpiTMedioTitulo.Text = "⏱ T. Medio"
            Me.lblCcnKpiTMedioTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiTMedioTitulo.UseMnemonic = False
            '
            'pnlCcnKpiVentas
            '
            Me.pnlCcnKpiVentas.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.pnlCcnKpiVentas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnKpiVentas.Controls.Add(Me.lblCcnKpiVentasValor)
            Me.pnlCcnKpiVentas.Controls.Add(Me.lblCcnKpiVentasTitulo)
            Me.pnlCcnKpiVentas.Location = New System.Drawing.Point(474, 0)
            Me.pnlCcnKpiVentas.Margin = New System.Windows.Forms.Padding(0)
            Me.pnlCcnKpiVentas.Name = "pnlCcnKpiVentas"
            Me.pnlCcnKpiVentas.Padding = New System.Windows.Forms.Padding(4)
            Me.pnlCcnKpiVentas.Size = New System.Drawing.Size(138, 76)
            Me.pnlCcnKpiVentas.TabIndex = 4
            '
            'lblCcnKpiVentasValor
            '
            Me.lblCcnKpiVentasValor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnKpiVentasValor.Font = New System.Drawing.Font("Segoe UI", 14.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiVentasValor.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnKpiVentasValor.Location = New System.Drawing.Point(4, 26)
            Me.lblCcnKpiVentasValor.Name = "lblCcnKpiVentasValor"
            Me.lblCcnKpiVentasValor.Size = New System.Drawing.Size(128, 44)
            Me.lblCcnKpiVentasValor.TabIndex = 1
            Me.lblCcnKpiVentasValor.Text = "$1,420.00"
            Me.lblCcnKpiVentasValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiVentasValor.UseMnemonic = False
            '
            'lblCcnKpiVentasTitulo
            '
            Me.lblCcnKpiVentasTitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnKpiVentasTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnKpiVentasTitulo.ForeColor = System.Drawing.Color.FromArgb(117, 70, 50)
            Me.lblCcnKpiVentasTitulo.Location = New System.Drawing.Point(4, 4)
            Me.lblCcnKpiVentasTitulo.Name = "lblCcnKpiVentasTitulo"
            Me.lblCcnKpiVentasTitulo.Size = New System.Drawing.Size(128, 22)
            Me.lblCcnKpiVentasTitulo.TabIndex = 0
            Me.lblCcnKpiVentasTitulo.Text = "💰 Ventas Turno"
            Me.lblCcnKpiVentasTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnKpiVentasTitulo.UseMnemonic = False
            '
            'pnlCcnTitulosHeader
            '
            Me.pnlCcnTitulosHeader.Controls.Add(Me.lblCcnTituloPrincipal)
            Me.pnlCcnTitulosHeader.Controls.Add(Me.lblCcnSubtituloVivo)
            Me.pnlCcnTitulosHeader.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnTitulosHeader.Location = New System.Drawing.Point(18, 10)
            Me.pnlCcnTitulosHeader.Name = "pnlCcnTitulosHeader"
            Me.pnlCcnTitulosHeader.Size = New System.Drawing.Size(360, 78)
            Me.pnlCcnTitulosHeader.TabIndex = 0
            '
            'lblCcnTituloPrincipal
            '
            Me.lblCcnTituloPrincipal.AutoSize = True
            Me.lblCcnTituloPrincipal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnTituloPrincipal.Font = New System.Drawing.Font("Segoe UI", 16.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnTituloPrincipal.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnTituloPrincipal.Location = New System.Drawing.Point(0, 24)
            Me.lblCcnTituloPrincipal.Name = "lblCcnTituloPrincipal"
            Me.lblCcnTituloPrincipal.Size = New System.Drawing.Size(332, 30)
            Me.lblCcnTituloPrincipal.TabIndex = 1
            Me.lblCcnTituloPrincipal.Text = "Monitor de Comandas & Cocina"
            Me.lblCcnTituloPrincipal.UseMnemonic = False
            '
            'lblCcnSubtituloVivo
            '
            Me.lblCcnSubtituloVivo.AutoSize = True
            Me.lblCcnSubtituloVivo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnSubtituloVivo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnSubtituloVivo.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnSubtituloVivo.Location = New System.Drawing.Point(0, 0)
            Me.lblCcnSubtituloVivo.Name = "lblCcnSubtituloVivo"
            Me.lblCcnSubtituloVivo.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
            Me.lblCcnSubtituloVivo.Size = New System.Drawing.Size(342, 24)
            Me.lblCcnSubtituloVivo.TabIndex = 0
            Me.lblCcnSubtituloVivo.Text = "🔴 DESPACHO & COCINA EN VIVO  /  Servicio Almuerzo"
            Me.lblCcnSubtituloVivo.UseMnemonic = False
            '
            'pnlCcnBarraFiltros
            '
            Me.pnlCcnBarraFiltros.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.pnlCcnBarraFiltros.Controls.Add(Me.pnlCcnAccionesDerecha)
            Me.pnlCcnBarraFiltros.Controls.Add(Me.pnlCcnChipsFiltro)
            Me.pnlCcnBarraFiltros.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnBarraFiltros.Location = New System.Drawing.Point(0, 96)
            Me.pnlCcnBarraFiltros.Name = "pnlCcnBarraFiltros"
            Me.pnlCcnBarraFiltros.Padding = New System.Windows.Forms.Padding(18, 4, 18, 8)
            Me.pnlCcnBarraFiltros.Size = New System.Drawing.Size(1100, 46)
            Me.pnlCcnBarraFiltros.TabIndex = 1
            '
            'pnlCcnAccionesDerecha
            '
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.btnCcnNuevoPedido)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.btnCcnRefrescarManual)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.btnCcnAlertaSonora)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.cboCcnCriterioOrden)
            Me.pnlCcnAccionesDerecha.Controls.Add(Me.lblCcnOrdenEtiqueta)
            Me.pnlCcnAccionesDerecha.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlCcnAccionesDerecha.Location = New System.Drawing.Point(475, 4)
            Me.pnlCcnAccionesDerecha.Name = "pnlCcnAccionesDerecha"
            Me.pnlCcnAccionesDerecha.Size = New System.Drawing.Size(607, 34)
            Me.pnlCcnAccionesDerecha.TabIndex = 1
            '
            'btnCcnNuevoPedido
            '
            Me.btnCcnNuevoPedido.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.btnCcnNuevoPedido.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnNuevoPedido.FlatAppearance.BorderSize = 0
            Me.btnCcnNuevoPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnNuevoPedido.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.btnCcnNuevoPedido.ForeColor = System.Drawing.Color.White
            Me.btnCcnNuevoPedido.Location = New System.Drawing.Point(446, 2)
            Me.btnCcnNuevoPedido.Name = "btnCcnNuevoPedido"
            Me.btnCcnNuevoPedido.Size = New System.Drawing.Size(155, 30)
            Me.btnCcnNuevoPedido.TabIndex = 4
            Me.btnCcnNuevoPedido.Text = "➕ Nueva Comanda"
            Me.btnCcnNuevoPedido.UseVisualStyleBackColor = False
            Me.btnCcnNuevoPedido.UseMnemonic = False
            '
            'btnCcnRefrescarManual
            '
            Me.btnCcnRefrescarManual.BackColor = System.Drawing.Color.White
            Me.btnCcnRefrescarManual.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnRefrescarManual.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnRefrescarManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnRefrescarManual.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnCcnRefrescarManual.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnRefrescarManual.Location = New System.Drawing.Point(398, 2)
            Me.btnCcnRefrescarManual.Name = "btnCcnRefrescarManual"
            Me.btnCcnRefrescarManual.Size = New System.Drawing.Size(40, 30)
            Me.btnCcnRefrescarManual.TabIndex = 3
            Me.btnCcnRefrescarManual.Text = "🔄"
            Me.btnCcnRefrescarManual.UseVisualStyleBackColor = False
            Me.btnCcnRefrescarManual.UseMnemonic = False
            '
            'btnCcnAlertaSonora
            '
            Me.btnCcnAlertaSonora.BackColor = System.Drawing.Color.White
            Me.btnCcnAlertaSonora.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnAlertaSonora.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnAlertaSonora.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnAlertaSonora.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnCcnAlertaSonora.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnAlertaSonora.Location = New System.Drawing.Point(352, 2)
            Me.btnCcnAlertaSonora.Name = "btnCcnAlertaSonora"
            Me.btnCcnAlertaSonora.Size = New System.Drawing.Size(40, 30)
            Me.btnCcnAlertaSonora.TabIndex = 2
            Me.btnCcnAlertaSonora.Text = "🔔"
            Me.btnCcnAlertaSonora.UseVisualStyleBackColor = False
            Me.btnCcnAlertaSonora.UseMnemonic = False
            '
            'cboCcnCriterioOrden
            '
            Me.cboCcnCriterioOrden.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCcnCriterioOrden.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular)
            Me.cboCcnCriterioOrden.FormattingEnabled = True
            Me.cboCcnCriterioOrden.Location = New System.Drawing.Point(88, 5)
            Me.cboCcnCriterioOrden.Name = "cboCcnCriterioOrden"
            Me.cboCcnCriterioOrden.Size = New System.Drawing.Size(256, 25)
            Me.cboCcnCriterioOrden.TabIndex = 1
            '
            'lblCcnOrdenEtiqueta
            '
            Me.lblCcnOrdenEtiqueta.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnOrdenEtiqueta.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnOrdenEtiqueta.Location = New System.Drawing.Point(12, 6)
            Me.lblCcnOrdenEtiqueta.Name = "lblCcnOrdenEtiqueta"
            Me.lblCcnOrdenEtiqueta.Size = New System.Drawing.Size(70, 22)
            Me.lblCcnOrdenEtiqueta.TabIndex = 0
            Me.lblCcnOrdenEtiqueta.Text = "⇅ Orden:"
            Me.lblCcnOrdenEtiqueta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblCcnOrdenEtiqueta.UseMnemonic = False
            '
            'pnlCcnChipsFiltro
            '
            Me.pnlCcnChipsFiltro.Controls.Add(Me.btnCcnFiltroEntregas)
            Me.pnlCcnChipsFiltro.Controls.Add(Me.btnCcnFiltroMesa)
            Me.pnlCcnChipsFiltro.Controls.Add(Me.btnCcnFiltroTodos)
            Me.pnlCcnChipsFiltro.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlCcnChipsFiltro.Location = New System.Drawing.Point(18, 4)
            Me.pnlCcnChipsFiltro.Name = "pnlCcnChipsFiltro"
            Me.pnlCcnChipsFiltro.Size = New System.Drawing.Size(530, 34)
            Me.pnlCcnChipsFiltro.TabIndex = 0
            '
            'btnCcnFiltroEntregas
            '
            Me.btnCcnFiltroEntregas.BackColor = System.Drawing.Color.White
            Me.btnCcnFiltroEntregas.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnFiltroEntregas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnFiltroEntregas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnFiltroEntregas.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular)
            Me.btnCcnFiltroEntregas.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnFiltroEntregas.Location = New System.Drawing.Point(324, 2)
            Me.btnCcnFiltroEntregas.Name = "btnCcnFiltroEntregas"
            Me.btnCcnFiltroEntregas.Size = New System.Drawing.Size(150, 30)
            Me.btnCcnFiltroEntregas.TabIndex = 2
            Me.btnCcnFiltroEntregas.Text = "🛍 Entregas (2)"
            Me.btnCcnFiltroEntregas.UseVisualStyleBackColor = False
            Me.btnCcnFiltroEntregas.UseMnemonic = False
            '
            'btnCcnFiltroMesa
            '
            Me.btnCcnFiltroMesa.BackColor = System.Drawing.Color.White
            Me.btnCcnFiltroMesa.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnFiltroMesa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnFiltroMesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnFiltroMesa.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular)
            Me.btnCcnFiltroMesa.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnFiltroMesa.Location = New System.Drawing.Point(166, 2)
            Me.btnCcnFiltroMesa.Name = "btnCcnFiltroMesa"
            Me.btnCcnFiltroMesa.Size = New System.Drawing.Size(150, 30)
            Me.btnCcnFiltroMesa.TabIndex = 1
            Me.btnCcnFiltroMesa.Text = "🍽 Mesa / Salón (5)"
            Me.btnCcnFiltroMesa.UseVisualStyleBackColor = False
            Me.btnCcnFiltroMesa.UseMnemonic = False
            '
            'btnCcnFiltroTodos
            '
            Me.btnCcnFiltroTodos.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.btnCcnFiltroTodos.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnFiltroTodos.FlatAppearance.BorderSize = 0
            Me.btnCcnFiltroTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnFiltroTodos.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnCcnFiltroTodos.ForeColor = System.Drawing.Color.White
            Me.btnCcnFiltroTodos.Location = New System.Drawing.Point(0, 2)
            Me.btnCcnFiltroTodos.Name = "btnCcnFiltroTodos"
            Me.btnCcnFiltroTodos.Size = New System.Drawing.Size(158, 30)
            Me.btnCcnFiltroTodos.TabIndex = 0
            Me.btnCcnFiltroTodos.Text = "Todos los pedidos (8)"
            Me.btnCcnFiltroTodos.UseVisualStyleBackColor = False
            Me.btnCcnFiltroTodos.UseMnemonic = False
            '
            'flpCcnContenedorComandas
            '
            Me.flpCcnContenedorComandas.AutoScroll = True
            Me.flpCcnContenedorComandas.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.flpCcnContenedorComandas.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpCcnContenedorComandas.Location = New System.Drawing.Point(0, 142)
            Me.flpCcnContenedorComandas.Name = "flpCcnContenedorComandas"
            Me.flpCcnContenedorComandas.Padding = New System.Windows.Forms.Padding(16, 8, 16, 16)
            Me.flpCcnContenedorComandas.Size = New System.Drawing.Size(1100, 518)
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
            Me.ClientSize = New System.Drawing.Size(1100, 660)
            Me.Controls.Add(Me.flpCcnContenedorComandas)
            Me.Controls.Add(Me.pnlCcnBarraFiltros)
            Me.Controls.Add(Me.pnlCcnHeaderPrincipal)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmCcnMonitorCocina"
            Me.Text = "Monitor de Cocina KDS"
            Me.pnlCcnHeaderPrincipal.ResumeLayout(False)
            Me.pnlCcnHeaderPrincipal.PerformLayout()
            Me.flpCcnKpisContenedor.ResumeLayout(False)
            Me.pnlCcnKpiActivos.ResumeLayout(False)
            Me.pnlCcnKpiEnCocina.ResumeLayout(False)
            Me.pnlCcnKpiListos.ResumeLayout(False)
            Me.pnlCcnKpiTMedio.ResumeLayout(False)
            Me.pnlCcnKpiVentas.ResumeLayout(False)
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
        Friend WithEvents flpCcnKpisContenedor As System.Windows.Forms.FlowLayoutPanel
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
        Friend WithEvents btnCcnNuevoPedido As System.Windows.Forms.Button
        Friend WithEvents flpCcnContenedorComandas As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents tmrCcnActualizadorRealTime As System.Windows.Forms.Timer
    End Class
End Namespace
