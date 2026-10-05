Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Caja
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCajaCobros
        Inherits System.Windows.Forms.Form

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
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblTituloHeader = New System.Windows.Forms.Label()
            Me.lblSubtituloHeader = New System.Windows.Forms.Label()
            Me.pnlContenedor = New System.Windows.Forms.Panel()
            Me.grpListado = New System.Windows.Forms.GroupBox()
            Me.pnlFiltros = New System.Windows.Forms.Panel()
            Me.lblBuscar = New System.Windows.Forms.Label()
            Me.txtBuscar = New System.Windows.Forms.TextBox()
            Me.btnBuscar = New System.Windows.Forms.Button()
            Me.btnRefrescar = New System.Windows.Forms.Button()
            Me.dgvPedidosPendientes = New System.Windows.Forms.DataGridView()
            Me.lblTotalPendientes = New System.Windows.Forms.Label()
            Me.grpDetalleCobro = New System.Windows.Forms.GroupBox()
            Me.pnlCardCobro = New System.Windows.Forms.Panel()
            Me.lblDetallePedidoId = New System.Windows.Forms.Label()
            Me.lblDetalleCliente = New System.Windows.Forms.Label()
            Me.lblDetalleMesa = New System.Windows.Forms.Label()
            Me.lblDetallePlato = New System.Windows.Forms.Label()
            Me.lblDetalleAcomp = New System.Windows.Forms.Label()
            Me.pnlSeparador1 = New System.Windows.Forms.Panel()
            Me.lblEtiquetaMetodo = New System.Windows.Forms.Label()
            Me.pnlMetodosPago = New System.Windows.Forms.Panel()
            Me.rbEfectivo = New System.Windows.Forms.RadioButton()
            Me.rbTarjeta = New System.Windows.Forms.RadioButton()
            Me.rbDigital = New System.Windows.Forms.RadioButton()
            Me.pnlSeparador2 = New System.Windows.Forms.Panel()
            Me.lblSubtotalEtiqueta = New System.Windows.Forms.Label()
            Me.lblSubtotalValor = New System.Windows.Forms.Label()
            Me.lblImpuestoEtiqueta = New System.Windows.Forms.Label()
            Me.lblImpuestoValor = New System.Windows.Forms.Label()
            Me.pnlTotalDestacado = New System.Windows.Forms.Panel()
            Me.lblTotalEtiqueta = New System.Windows.Forms.Label()
            Me.lblTotalValor = New System.Windows.Forms.Label()
            Me.pnlCalculoCaja = New System.Windows.Forms.Panel()
            Me.lblMontoRecibidoEtiqueta = New System.Windows.Forms.Label()
            Me.txtMontoRecibido = New System.Windows.Forms.TextBox()
            Me.lblCambioEtiqueta = New System.Windows.Forms.Label()
            Me.lblCambioValor = New System.Windows.Forms.Label()
            Me.btnConfirmarCobro = New System.Windows.Forms.Button()
            Me.btnLimpiar = New System.Windows.Forms.Button()
            Me.pnlHeader.SuspendLayout()
            Me.pnlContenedor.SuspendLayout()
            Me.grpListado.SuspendLayout()
            Me.pnlFiltros.SuspendLayout()
            CType(Me.dgvPedidosPendientes, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpDetalleCobro.SuspendLayout()
            Me.pnlCardCobro.SuspendLayout()
            Me.pnlMetodosPago.SuspendLayout()
            Me.pnlTotalDestacado.SuspendLayout()
            Me.pnlCalculoCaja.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlHeader
            '
            Me.pnlHeader.Controls.Add(Me.lblSubtituloHeader)
            Me.pnlHeader.Controls.Add(Me.lblTituloHeader)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(20, 20)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(960, 65)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblTituloHeader
            '
            Me.lblTituloHeader.AutoSize = True
            Me.lblTituloHeader.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloHeader.Location = New System.Drawing.Point(0, 0)
            Me.lblTituloHeader.Name = "lblTituloHeader"
            Me.lblTituloHeader.Size = New System.Drawing.Size(350, 30)
            Me.lblTituloHeader.TabIndex = 0
            Me.lblTituloHeader.Text = "💵 Caja & Procesamiento de Pagos"
            '
            ' lblSubtituloHeader
            '
            Me.lblSubtituloHeader.AutoSize = True
            Me.lblSubtituloHeader.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblSubtituloHeader.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloHeader.Location = New System.Drawing.Point(3, 34)
            Me.lblSubtituloHeader.Name = "lblSubtituloHeader"
            Me.lblSubtituloHeader.Size = New System.Drawing.Size(640, 17)
            Me.lblSubtituloHeader.TabIndex = 1
            Me.lblSubtituloHeader.Text = "Aprobación de pagos en efectivo, verificación de cobros y liberación de pedidos hacia cocina KDS (RF-011, CU-006)."
            '
            ' pnlContenedor
            '
            Me.pnlContenedor.Controls.Add(Me.grpListado)
            Me.pnlContenedor.Controls.Add(Me.grpDetalleCobro)
            Me.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenedor.Location = New System.Drawing.Point(20, 85)
            Me.pnlContenedor.Name = "pnlContenedor"
            Me.pnlContenedor.Size = New System.Drawing.Size(960, 560)
            Me.pnlContenedor.TabIndex = 1
            '
            ' grpListado
            '
            Me.grpListado.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpListado.Controls.Add(Me.dgvPedidosPendientes)
            Me.grpListado.Controls.Add(Me.pnlFiltros)
            Me.grpListado.Controls.Add(Me.lblTotalPendientes)
            Me.grpListado.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpListado.Location = New System.Drawing.Point(0, 5)
            Me.grpListado.Name = "grpListado"
            Me.grpListado.Padding = New System.Windows.Forms.Padding(12)
            Me.grpListado.Size = New System.Drawing.Size(560, 545)
            Me.grpListado.TabIndex = 0
            Me.grpListado.TabStop = False
            Me.grpListado.Text = "Comandas Pendientes de Pago (Por Cobrar)"
            '
            ' pnlFiltros
            '
            Me.pnlFiltros.Controls.Add(Me.btnRefrescar)
            Me.pnlFiltros.Controls.Add(Me.btnBuscar)
            Me.pnlFiltros.Controls.Add(Me.txtBuscar)
            Me.pnlFiltros.Controls.Add(Me.lblBuscar)
            Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltros.Location = New System.Drawing.Point(12, 30)
            Me.pnlFiltros.Name = "pnlFiltros"
            Me.pnlFiltros.Size = New System.Drawing.Size(536, 42)
            Me.pnlFiltros.TabIndex = 0
            '
            ' lblBuscar
            '
            Me.lblBuscar.AutoSize = True
            Me.lblBuscar.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblBuscar.Location = New System.Drawing.Point(0, 12)
            Me.lblBuscar.Name = "lblBuscar"
            Me.lblBuscar.Size = New System.Drawing.Size(98, 15)
            Me.lblBuscar.TabIndex = 0
            Me.lblBuscar.Text = "Buscar Pedido/ID:"
            '
            ' txtBuscar
            '
            Me.txtBuscar.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtBuscar.Location = New System.Drawing.Point(105, 8)
            Me.txtBuscar.Name = "txtBuscar"
            Me.txtBuscar.Size = New System.Drawing.Size(180, 24)
            Me.txtBuscar.TabIndex = 1
            '
            ' btnBuscar
            '
            Me.btnBuscar.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.btnBuscar.Location = New System.Drawing.Point(292, 7)
            Me.btnBuscar.Name = "btnBuscar"
            Me.btnBuscar.Size = New System.Drawing.Size(85, 27)
            Me.btnBuscar.TabIndex = 2
            Me.btnBuscar.Text = "🔍 Buscar"
            Me.btnBuscar.UseVisualStyleBackColor = True
            '
            ' btnRefrescar
            '
            Me.btnRefrescar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRefrescar.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.btnRefrescar.Location = New System.Drawing.Point(426, 7)
            Me.btnRefrescar.Name = "btnRefrescar"
            Me.btnRefrescar.Size = New System.Drawing.Size(105, 27)
            Me.btnRefrescar.TabIndex = 3
            Me.btnRefrescar.Text = "🔄 Actualizar"
            Me.btnRefrescar.UseVisualStyleBackColor = True
            '
            ' dgvPedidosPendientes
            '
            Me.dgvPedidosPendientes.AllowUserToAddRows = False
            Me.dgvPedidosPendientes.AllowUserToDeleteRows = False
            Me.dgvPedidosPendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvPedidosPendientes.BackgroundColor = System.Drawing.Color.White
            Me.dgvPedidosPendientes.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgvPedidosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvPedidosPendientes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPedidosPendientes.Location = New System.Drawing.Point(12, 72)
            Me.dgvPedidosPendientes.MultiSelect = False
            Me.dgvPedidosPendientes.Name = "dgvPedidosPendientes"
            Me.dgvPedidosPendientes.ReadOnly = True
            Me.dgvPedidosPendientes.RowHeadersVisible = False
            Me.dgvPedidosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvPedidosPendientes.Size = New System.Drawing.Size(536, 435)
            Me.dgvPedidosPendientes.TabIndex = 1
            '
            ' lblTotalPendientes
            '
            Me.lblTotalPendientes.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.lblTotalPendientes.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblTotalPendientes.ForeColor = System.Drawing.Color.Gray
            Me.lblTotalPendientes.Location = New System.Drawing.Point(12, 507)
            Me.lblTotalPendientes.Name = "lblTotalPendientes"
            Me.lblTotalPendientes.Size = New System.Drawing.Size(536, 26)
            Me.lblTotalPendientes.TabIndex = 2
            Me.lblTotalPendientes.Text = "Pedidos pendientes: 0"
            Me.lblTotalPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            ' grpDetalleCobro
            '
            Me.grpDetalleCobro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpDetalleCobro.Controls.Add(Me.pnlCardCobro)
            Me.grpDetalleCobro.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpDetalleCobro.Location = New System.Drawing.Point(575, 5)
            Me.grpDetalleCobro.Name = "grpDetalleCobro"
            Me.grpDetalleCobro.Padding = New System.Windows.Forms.Padding(12)
            Me.grpDetalleCobro.Size = New System.Drawing.Size(385, 545)
            Me.grpDetalleCobro.TabIndex = 1
            Me.grpDetalleCobro.TabStop = False
            Me.grpDetalleCobro.Text = "Terminal de Cobro & Autorización"
            '
            ' pnlCardCobro
            '
            Me.pnlCardCobro.AutoScroll = True
            Me.pnlCardCobro.Controls.Add(Me.btnLimpiar)
            Me.pnlCardCobro.Controls.Add(Me.btnConfirmarCobro)
            Me.pnlCardCobro.Controls.Add(Me.pnlCalculoCaja)
            Me.pnlCardCobro.Controls.Add(Me.pnlTotalDestacado)
            Me.pnlCardCobro.Controls.Add(Me.lblImpuestoValor)
            Me.pnlCardCobro.Controls.Add(Me.lblImpuestoEtiqueta)
            Me.pnlCardCobro.Controls.Add(Me.lblSubtotalValor)
            Me.pnlCardCobro.Controls.Add(Me.lblSubtotalEtiqueta)
            Me.pnlCardCobro.Controls.Add(Me.pnlSeparador2)
            Me.pnlCardCobro.Controls.Add(Me.pnlMetodosPago)
            Me.pnlCardCobro.Controls.Add(Me.lblEtiquetaMetodo)
            Me.pnlCardCobro.Controls.Add(Me.pnlSeparador1)
            Me.pnlCardCobro.Controls.Add(Me.lblDetalleAcomp)
            Me.pnlCardCobro.Controls.Add(Me.lblDetallePlato)
            Me.pnlCardCobro.Controls.Add(Me.lblDetalleMesa)
            Me.pnlCardCobro.Controls.Add(Me.lblDetalleCliente)
            Me.pnlCardCobro.Controls.Add(Me.lblDetallePedidoId)
            Me.pnlCardCobro.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardCobro.Location = New System.Drawing.Point(12, 30)
            Me.pnlCardCobro.Name = "pnlCardCobro"
            Me.pnlCardCobro.Size = New System.Drawing.Size(361, 503)
            Me.pnlCardCobro.TabIndex = 0
            '
            ' lblDetallePedidoId
            '
            Me.lblDetallePedidoId.AutoSize = True
            Me.lblDetallePedidoId.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetallePedidoId.Location = New System.Drawing.Point(5, 5)
            Me.lblDetallePedidoId.Name = "lblDetallePedidoId"
            Me.lblDetallePedidoId.Size = New System.Drawing.Size(185, 21)
            Me.lblDetallePedidoId.TabIndex = 0
            Me.lblDetallePedidoId.Text = "Comanda: (Sin Selección)"
            '
            ' lblDetalleCliente
            '
            Me.lblDetalleCliente.AutoSize = True
            Me.lblDetalleCliente.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblDetalleCliente.Location = New System.Drawing.Point(6, 32)
            Me.lblDetalleCliente.Name = "lblDetalleCliente"
            Me.lblDetalleCliente.Size = New System.Drawing.Size(155, 15)
            Me.lblDetalleCliente.TabIndex = 1
            Me.lblDetalleCliente.Text = "Cliente: Seleccione una orden"
            '
            ' lblDetalleMesa
            '
            Me.lblDetalleMesa.AutoSize = True
            Me.lblDetalleMesa.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblDetalleMesa.Location = New System.Drawing.Point(6, 52)
            Me.lblDetalleMesa.Name = "lblDetalleMesa"
            Me.lblDetalleMesa.Size = New System.Drawing.Size(110, 15)
            Me.lblDetalleMesa.TabIndex = 2
            Me.lblDetalleMesa.Text = "Mesa / Servicio: --"
            '
            ' lblDetallePlato
            '
            Me.lblDetallePlato.AutoSize = True
            Me.lblDetallePlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetallePlato.Location = New System.Drawing.Point(6, 74)
            Me.lblDetallePlato.Name = "lblDetallePlato"
            Me.lblDetallePlato.Size = New System.Drawing.Size(100, 15)
            Me.lblDetallePlato.TabIndex = 3
            Me.lblDetallePlato.Text = "Plato: Ninguno"
            '
            ' lblDetalleAcomp
            '
            Me.lblDetalleAcomp.AutoSize = True
            Me.lblDetalleAcomp.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblDetalleAcomp.ForeColor = System.Drawing.Color.DimGray
            Me.lblDetalleAcomp.Location = New System.Drawing.Point(6, 94)
            Me.lblDetalleAcomp.Name = "lblDetalleAcomp"
            Me.lblDetalleAcomp.Size = New System.Drawing.Size(130, 15)
            Me.lblDetalleAcomp.TabIndex = 4
            Me.lblDetalleAcomp.Text = "Acomp: --"
            '
            ' pnlSeparador1
            '
            Me.pnlSeparador1.BackColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.pnlSeparador1.Location = New System.Drawing.Point(8, 118)
            Me.pnlSeparador1.Name = "pnlSeparador1"
            Me.pnlSeparador1.Size = New System.Drawing.Size(345, 1)
            Me.pnlSeparador1.TabIndex = 5
            '
            ' lblEtiquetaMetodo
            '
            Me.lblEtiquetaMetodo.AutoSize = True
            Me.lblEtiquetaMetodo.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblEtiquetaMetodo.Location = New System.Drawing.Point(6, 126)
            Me.lblEtiquetaMetodo.Name = "lblEtiquetaMetodo"
            Me.lblEtiquetaMetodo.Size = New System.Drawing.Size(105, 15)
            Me.lblEtiquetaMetodo.TabIndex = 6
            Me.lblEtiquetaMetodo.Text = "Método de Pago:"
            '
            ' pnlMetodosPago
            '
            Me.pnlMetodosPago.Controls.Add(Me.rbDigital)
            Me.pnlMetodosPago.Controls.Add(Me.rbTarjeta)
            Me.pnlMetodosPago.Controls.Add(Me.rbEfectivo)
            Me.pnlMetodosPago.Location = New System.Drawing.Point(8, 145)
            Me.pnlMetodosPago.Name = "pnlMetodosPago"
            Me.pnlMetodosPago.Size = New System.Drawing.Size(345, 30)
            Me.pnlMetodosPago.TabIndex = 7
            '
            ' rbEfectivo
            '
            Me.rbEfectivo.AutoSize = True
            Me.rbEfectivo.Checked = True
            Me.rbEfectivo.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.rbEfectivo.Location = New System.Drawing.Point(3, 5)
            Me.rbEfectivo.Name = "rbEfectivo"
            Me.rbEfectivo.Size = New System.Drawing.Size(83, 19)
            Me.rbEfectivo.TabIndex = 0
            Me.rbEfectivo.TabStop = True
            Me.rbEfectivo.Text = "💵 Efectivo"
            Me.rbEfectivo.UseVisualStyleBackColor = True
            '
            ' rbTarjeta
            '
            Me.rbTarjeta.AutoSize = True
            Me.rbTarjeta.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.rbTarjeta.Location = New System.Drawing.Point(95, 5)
            Me.rbTarjeta.Name = "rbTarjeta"
            Me.rbTarjeta.Size = New System.Drawing.Size(95, 19)
            Me.rbTarjeta.TabIndex = 1
            Me.rbTarjeta.Text = "💳 Tarjeta POS"
            Me.rbTarjeta.UseVisualStyleBackColor = True
            '
            ' rbDigital
            '
            Me.rbDigital.AutoSize = True
            Me.rbDigital.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.rbDigital.Location = New System.Drawing.Point(200, 5)
            Me.rbDigital.Name = "rbDigital"
            Me.rbDigital.Size = New System.Drawing.Size(125, 19)
            Me.rbDigital.TabIndex = 2
            Me.rbDigital.Text = "📲 QR / Transf."
            Me.rbDigital.UseVisualStyleBackColor = True
            '
            ' pnlSeparador2
            '
            Me.pnlSeparador2.BackColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.pnlSeparador2.Location = New System.Drawing.Point(8, 182)
            Me.pnlSeparador2.Name = "pnlSeparador2"
            Me.pnlSeparador2.Size = New System.Drawing.Size(345, 1)
            Me.pnlSeparador2.TabIndex = 8
            '
            ' lblSubtotalEtiqueta
            '
            Me.lblSubtotalEtiqueta.AutoSize = True
            Me.lblSubtotalEtiqueta.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtotalEtiqueta.Location = New System.Drawing.Point(8, 193)
            Me.lblSubtotalEtiqueta.Name = "lblSubtotalEtiqueta"
            Me.lblSubtotalEtiqueta.Size = New System.Drawing.Size(107, 15)
            Me.lblSubtotalEtiqueta.TabIndex = 9
            Me.lblSubtotalEtiqueta.Text = "Subtotal Gravable:"
            '
            ' lblSubtotalValor
            '
            Me.lblSubtotalValor.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtotalValor.Location = New System.Drawing.Point(220, 193)
            Me.lblSubtotalValor.Name = "lblSubtotalValor"
            Me.lblSubtotalValor.Size = New System.Drawing.Size(130, 15)
            Me.lblSubtotalValor.TabIndex = 10
            Me.lblSubtotalValor.Text = "$0.00"
            Me.lblSubtotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblImpuestoEtiqueta
            '
            Me.lblImpuestoEtiqueta.AutoSize = True
            Me.lblImpuestoEtiqueta.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblImpuestoEtiqueta.Location = New System.Drawing.Point(8, 214)
            Me.lblImpuestoEtiqueta.Name = "lblImpuestoEtiqueta"
            Me.lblImpuestoEtiqueta.Size = New System.Drawing.Size(95, 15)
            Me.lblImpuestoEtiqueta.TabIndex = 11
            Me.lblImpuestoEtiqueta.Text = "ITBMS (7.00%):"
            '
            ' lblImpuestoValor
            '
            Me.lblImpuestoValor.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblImpuestoValor.Location = New System.Drawing.Point(220, 214)
            Me.lblImpuestoValor.Name = "lblImpuestoValor"
            Me.lblImpuestoValor.Size = New System.Drawing.Size(130, 15)
            Me.lblImpuestoValor.TabIndex = 12
            Me.lblImpuestoValor.Text = "$0.00"
            Me.lblImpuestoValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' pnlTotalDestacado
            '
            Me.pnlTotalDestacado.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.pnlTotalDestacado.Controls.Add(Me.lblTotalValor)
            Me.pnlTotalDestacado.Controls.Add(Me.lblTotalEtiqueta)
            Me.pnlTotalDestacado.Location = New System.Drawing.Point(8, 240)
            Me.pnlTotalDestacado.Name = "pnlTotalDestacado"
            Me.pnlTotalDestacado.Size = New System.Drawing.Size(345, 45)
            Me.pnlTotalDestacado.TabIndex = 13
            '
            ' lblTotalEtiqueta
            '
            Me.lblTotalEtiqueta.AutoSize = True
            Me.lblTotalEtiqueta.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblTotalEtiqueta.Location = New System.Drawing.Point(12, 12)
            Me.lblTotalEtiqueta.Name = "lblTotalEtiqueta"
            Me.lblTotalEtiqueta.Size = New System.Drawing.Size(130, 20)
            Me.lblTotalEtiqueta.TabIndex = 0
            Me.lblTotalEtiqueta.Text = "TOTAL A COBRAR:"
            '
            ' lblTotalValor
            '
            Me.lblTotalValor.Font = New System.Drawing.Font("Segoe UI", 15.0F, System.Drawing.FontStyle.Bold)
            Me.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblTotalValor.Location = New System.Drawing.Point(165, 6)
            Me.lblTotalValor.Name = "lblTotalValor"
            Me.lblTotalValor.Size = New System.Drawing.Size(170, 32)
            Me.lblTotalValor.TabIndex = 1
            Me.lblTotalValor.Text = "$0.00"
            Me.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' pnlCalculoCaja
            '
            Me.pnlCalculoCaja.BackColor = System.Drawing.Color.FromArgb(250, 249, 246)
            Me.pnlCalculoCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCalculoCaja.Controls.Add(Me.lblCambioValor)
            Me.pnlCalculoCaja.Controls.Add(Me.lblCambioEtiqueta)
            Me.pnlCalculoCaja.Controls.Add(Me.txtMontoRecibido)
            Me.pnlCalculoCaja.Controls.Add(Me.lblMontoRecibidoEtiqueta)
            Me.pnlCalculoCaja.Location = New System.Drawing.Point(8, 295)
            Me.pnlCalculoCaja.Name = "pnlCalculoCaja"
            Me.pnlCalculoCaja.Size = New System.Drawing.Size(345, 95)
            Me.pnlCalculoCaja.TabIndex = 14
            '
            ' lblMontoRecibidoEtiqueta
            '
            Me.lblMontoRecibidoEtiqueta.AutoSize = True
            Me.lblMontoRecibidoEtiqueta.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoRecibidoEtiqueta.Location = New System.Drawing.Point(12, 16)
            Me.lblMontoRecibidoEtiqueta.Name = "lblMontoRecibidoEtiqueta"
            Me.lblMontoRecibidoEtiqueta.Size = New System.Drawing.Size(115, 15)
            Me.lblMontoRecibidoEtiqueta.TabIndex = 0
            Me.lblMontoRecibidoEtiqueta.Text = "Efectivo Recibido ($):"
            '
            ' txtMontoRecibido
            '
            Me.txtMontoRecibido.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.txtMontoRecibido.Location = New System.Drawing.Point(165, 10)
            Me.txtMontoRecibido.Name = "txtMontoRecibido"
            Me.txtMontoRecibido.Size = New System.Drawing.Size(165, 27)
            Me.txtMontoRecibido.TabIndex = 1
            Me.txtMontoRecibido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            '
            ' lblCambioEtiqueta
            '
            Me.lblCambioEtiqueta.AutoSize = True
            Me.lblCambioEtiqueta.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblCambioEtiqueta.Location = New System.Drawing.Point(12, 55)
            Me.lblCambioEtiqueta.Name = "lblCambioEtiqueta"
            Me.lblCambioEtiqueta.Size = New System.Drawing.Size(122, 17)
            Me.lblCambioEtiqueta.TabIndex = 2
            Me.lblCambioEtiqueta.Text = "Cambio a Devolver:"
            '
            ' lblCambioValor
            '
            Me.lblCambioValor.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblCambioValor.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblCambioValor.Location = New System.Drawing.Point(165, 52)
            Me.lblCambioValor.Name = "lblCambioValor"
            Me.lblCambioValor.Size = New System.Drawing.Size(165, 23)
            Me.lblCambioValor.TabIndex = 3
            Me.lblCambioValor.Text = "$0.00"
            Me.lblCambioValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' btnConfirmarCobro
            '
            Me.btnConfirmarCobro.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnConfirmarCobro.Location = New System.Drawing.Point(8, 405)
            Me.btnConfirmarCobro.Name = "btnConfirmarCobro"
            Me.btnConfirmarCobro.Size = New System.Drawing.Size(345, 42)
            Me.btnConfirmarCobro.TabIndex = 15
            Me.btnConfirmarCobro.Text = "✅ Confirmar Cobro & Liberar a Cocina"
            Me.btnConfirmarCobro.UseVisualStyleBackColor = True
            '
            ' btnLimpiar
            '
            Me.btnLimpiar.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.btnLimpiar.Location = New System.Drawing.Point(8, 455)
            Me.btnLimpiar.Name = "btnLimpiar"
            Me.btnLimpiar.Size = New System.Drawing.Size(345, 32)
            Me.btnLimpiar.TabIndex = 16
            Me.btnLimpiar.Text = "🧹 Limpiar Selección"
            Me.btnLimpiar.UseVisualStyleBackColor = True
            '
            ' FrmCajaCobros
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(1000, 665)
            Me.Controls.Add(Me.pnlContenedor)
            Me.Controls.Add(Me.pnlHeader)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmCajaCobros"
            Me.Padding = New System.Windows.Forms.Padding(20)
            Me.Text = "Caja & Procesamiento de Pagos"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlContenedor.ResumeLayout(False)
            Me.grpListado.ResumeLayout(False)
            Me.pnlFiltros.ResumeLayout(False)
            Me.pnlFiltros.PerformLayout()
            CType(Me.dgvPedidosPendientes, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpDetalleCobro.ResumeLayout(False)
            Me.pnlCardCobro.ResumeLayout(False)
            Me.pnlCardCobro.PerformLayout()
            Me.pnlMetodosPago.ResumeLayout(False)
            Me.pnlMetodosPago.PerformLayout()
            Me.pnlTotalDestacado.ResumeLayout(False)
            Me.pnlTotalDestacado.PerformLayout()
            Me.pnlCalculoCaja.ResumeLayout(False)
            Me.pnlCalculoCaja.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As Panel
        Friend WithEvents lblTituloHeader As Label
        Friend WithEvents lblSubtituloHeader As Label
        Friend WithEvents pnlContenedor As Panel
        Friend WithEvents grpListado As GroupBox
        Friend WithEvents pnlFiltros As Panel
        Friend WithEvents lblBuscar As Label
        Friend WithEvents txtBuscar As TextBox
        Friend WithEvents btnBuscar As Button
        Friend WithEvents btnRefrescar As Button
        Friend WithEvents dgvPedidosPendientes As DataGridView
        Friend WithEvents lblTotalPendientes As Label
        Friend WithEvents grpDetalleCobro As GroupBox
        Friend WithEvents pnlCardCobro As Panel
        Friend WithEvents lblDetallePedidoId As Label
        Friend WithEvents lblDetalleCliente As Label
        Friend WithEvents lblDetalleMesa As Label
        Friend WithEvents lblDetallePlato As Label
        Friend WithEvents lblDetalleAcomp As Label
        Friend WithEvents pnlSeparador1 As Panel
        Friend WithEvents lblEtiquetaMetodo As Label
        Friend WithEvents pnlMetodosPago As Panel
        Friend WithEvents rbEfectivo As RadioButton
        Friend WithEvents rbTarjeta As RadioButton
        Friend WithEvents rbDigital As RadioButton
        Friend WithEvents pnlSeparador2 As Panel
        Friend WithEvents lblSubtotalEtiqueta As Label
        Friend WithEvents lblSubtotalValor As Label
        Friend WithEvents lblImpuestoEtiqueta As Label
        Friend WithEvents lblImpuestoValor As Label
        Friend WithEvents pnlTotalDestacado As Panel
        Friend WithEvents lblTotalEtiqueta As Label
        Friend WithEvents lblTotalValor As Label
        Friend WithEvents pnlCalculoCaja As Panel
        Friend WithEvents lblMontoRecibidoEtiqueta As Label
        Friend WithEvents txtMontoRecibido As TextBox
        Friend WithEvents lblCambioEtiqueta As Label
        Friend WithEvents lblCambioValor As Label
        Friend WithEvents btnConfirmarCobro As Button
        Friend WithEvents btnLimpiar As Button
    End Class
End Namespace
