Namespace Views.Cocina
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCcnNuevoPedidoDialog
        Inherits Sistema_de_gestion_de_pedidos_para_un_restaurante.Views.Common.FrmBaseForm

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

        Friend WithEvents pnlCcnHeaderModal As System.Windows.Forms.Panel
        Friend WithEvents lblCcnTituloModal As System.Windows.Forms.Label
        Friend WithEvents lblCcnSubtituloModal As System.Windows.Forms.Label
        Friend WithEvents pnlCcnContenedorForm As System.Windows.Forms.Panel

        Friend WithEvents lblCcnEtiquetaCliente As System.Windows.Forms.Label
        Friend WithEvents txtCcnCliente As System.Windows.Forms.TextBox

        Friend WithEvents lblCcnEtiquetaServicio As System.Windows.Forms.Label
        Friend WithEvents cboCcnTipoServicio As System.Windows.Forms.ComboBox

        Friend WithEvents lblCcnEtiquetaMesa As System.Windows.Forms.Label
        Friend WithEvents txtCcnMesa As System.Windows.Forms.TextBox

        Friend WithEvents lblCcnEtiquetaPlato As System.Windows.Forms.Label
        Friend WithEvents cboCcnPlato As System.Windows.Forms.ComboBox

        Friend WithEvents lblCcnEtiquetaCantidad As System.Windows.Forms.Label
        Friend WithEvents numCcnCantidad As System.Windows.Forms.NumericUpDown

        Friend WithEvents lblCcnEtiquetaNotas As System.Windows.Forms.Label
        Friend WithEvents txtCcnNotas As System.Windows.Forms.TextBox
        Friend WithEvents chkCcnAlergenoCeliaco As System.Windows.Forms.CheckBox

        Friend WithEvents lblCcnEtiquetaPago As System.Windows.Forms.Label
        Friend WithEvents cboCcnEstadoPago As System.Windows.Forms.ComboBox

        Friend WithEvents pnlCcnResumenTotal As System.Windows.Forms.Panel
        Friend WithEvents lblCcnResumenTotal As System.Windows.Forms.Label

        Friend WithEvents pnlCcnBotonesAccion As System.Windows.Forms.Panel
        Friend WithEvents btnCcnGuardarPedido As System.Windows.Forms.Button
        Friend WithEvents btnCcnCancelar As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlCcnHeaderModal = New System.Windows.Forms.Panel()
            Me.lblCcnSubtituloModal = New System.Windows.Forms.Label()
            Me.lblCcnTituloModal = New System.Windows.Forms.Label()
            Me.pnlCcnContenedorForm = New System.Windows.Forms.Panel()
            Me.pnlCcnResumenTotal = New System.Windows.Forms.Panel()
            Me.lblCcnResumenTotal = New System.Windows.Forms.Label()
            Me.cboCcnEstadoPago = New System.Windows.Forms.ComboBox()
            Me.lblCcnEtiquetaPago = New System.Windows.Forms.Label()
            Me.chkCcnAlergenoCeliaco = New System.Windows.Forms.CheckBox()
            Me.txtCcnNotas = New System.Windows.Forms.TextBox()
            Me.lblCcnEtiquetaNotas = New System.Windows.Forms.Label()
            Me.numCcnCantidad = New System.Windows.Forms.NumericUpDown()
            Me.lblCcnEtiquetaCantidad = New System.Windows.Forms.Label()
            Me.cboCcnPlato = New System.Windows.Forms.ComboBox()
            Me.lblCcnEtiquetaPlato = New System.Windows.Forms.Label()
            Me.txtCcnMesa = New System.Windows.Forms.TextBox()
            Me.lblCcnEtiquetaMesa = New System.Windows.Forms.Label()
            Me.cboCcnTipoServicio = New System.Windows.Forms.ComboBox()
            Me.lblCcnEtiquetaServicio = New System.Windows.Forms.Label()
            Me.txtCcnCliente = New System.Windows.Forms.TextBox()
            Me.lblCcnEtiquetaCliente = New System.Windows.Forms.Label()
            Me.pnlCcnBotonesAccion = New System.Windows.Forms.Panel()
            Me.btnCcnCancelar = New System.Windows.Forms.Button()
            Me.btnCcnGuardarPedido = New System.Windows.Forms.Button()
            Me.pnlCcnHeaderModal.SuspendLayout()
            Me.pnlCcnContenedorForm.SuspendLayout()
            Me.pnlCcnResumenTotal.SuspendLayout()
            CType(Me.numCcnCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlCcnBotonesAccion.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlCcnHeaderModal
            '
            Me.pnlCcnHeaderModal.BackColor = System.Drawing.Color.FromArgb(248, 246, 242)
            Me.pnlCcnHeaderModal.Controls.Add(Me.lblCcnSubtituloModal)
            Me.pnlCcnHeaderModal.Controls.Add(Me.lblCcnTituloModal)
            Me.pnlCcnHeaderModal.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnHeaderModal.Location = New System.Drawing.Point(0, 0)
            Me.pnlCcnHeaderModal.Name = "pnlCcnHeaderModal"
            Me.pnlCcnHeaderModal.Padding = New System.Windows.Forms.Padding(20, 15, 20, 12)
            Me.pnlCcnHeaderModal.Size = New System.Drawing.Size(520, 75)
            Me.pnlCcnHeaderModal.TabIndex = 0
            '
            'lblCcnSubtituloModal
            '
            Me.lblCcnSubtituloModal.AutoSize = True
            Me.lblCcnSubtituloModal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnSubtituloModal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnSubtituloModal.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnSubtituloModal.Location = New System.Drawing.Point(20, 39)
            Me.lblCcnSubtituloModal.Name = "lblCcnSubtituloModal"
            Me.lblCcnSubtituloModal.Size = New System.Drawing.Size(370, 15)
            Me.lblCcnSubtituloModal.TabIndex = 1
            Me.lblCcnSubtituloModal.Text = "Conecta automáticamente con Cocina (KDS), Caja (Cobros) y Facturación"
            Me.lblCcnSubtituloModal.UseMnemonic = False
            '
            'lblCcnTituloModal
            '
            Me.lblCcnTituloModal.AutoSize = True
            Me.lblCcnTituloModal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnTituloModal.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnTituloModal.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnTituloModal.Location = New System.Drawing.Point(20, 15)
            Me.lblCcnTituloModal.Name = "lblCcnTituloModal"
            Me.lblCcnTituloModal.Size = New System.Drawing.Size(306, 24)
            Me.lblCcnTituloModal.TabIndex = 0
            Me.lblCcnTituloModal.Text = "➕ Nueva Orden / Comanda de Pedido"
            Me.lblCcnTituloModal.UseMnemonic = False
            '
            'pnlCcnContenedorForm
            '
            Me.pnlCcnContenedorForm.BackColor = System.Drawing.Color.White
            Me.pnlCcnContenedorForm.Controls.Add(Me.pnlCcnResumenTotal)
            Me.pnlCcnContenedorForm.Controls.Add(Me.cboCcnEstadoPago)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaPago)
            Me.pnlCcnContenedorForm.Controls.Add(Me.chkCcnAlergenoCeliaco)
            Me.pnlCcnContenedorForm.Controls.Add(Me.txtCcnNotas)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaNotas)
            Me.pnlCcnContenedorForm.Controls.Add(Me.numCcnCantidad)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaCantidad)
            Me.pnlCcnContenedorForm.Controls.Add(Me.cboCcnPlato)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaPlato)
            Me.pnlCcnContenedorForm.Controls.Add(Me.txtCcnMesa)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaMesa)
            Me.pnlCcnContenedorForm.Controls.Add(Me.cboCcnTipoServicio)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaServicio)
            Me.pnlCcnContenedorForm.Controls.Add(Me.txtCcnCliente)
            Me.pnlCcnContenedorForm.Controls.Add(Me.lblCcnEtiquetaCliente)
            Me.pnlCcnContenedorForm.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCcnContenedorForm.Location = New System.Drawing.Point(0, 75)
            Me.pnlCcnContenedorForm.Name = "pnlCcnContenedorForm"
            Me.pnlCcnContenedorForm.Padding = New System.Windows.Forms.Padding(24, 15, 24, 15)
            Me.pnlCcnContenedorForm.Size = New System.Drawing.Size(520, 480)
            Me.pnlCcnContenedorForm.TabIndex = 1
            '
            'pnlCcnResumenTotal
            '
            Me.pnlCcnResumenTotal.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.pnlCcnResumenTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnResumenTotal.Controls.Add(Me.lblCcnResumenTotal)
            Me.pnlCcnResumenTotal.Location = New System.Drawing.Point(24, 420)
            Me.pnlCcnResumenTotal.Name = "pnlCcnResumenTotal"
            Me.pnlCcnResumenTotal.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
            Me.pnlCcnResumenTotal.Size = New System.Drawing.Size(472, 44)
            Me.pnlCcnResumenTotal.TabIndex = 15
            '
            'lblCcnResumenTotal
            '
            Me.lblCcnResumenTotal.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCcnResumenTotal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnResumenTotal.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnResumenTotal.Location = New System.Drawing.Point(12, 8)
            Me.lblCcnResumenTotal.Name = "lblCcnResumenTotal"
            Me.lblCcnResumenTotal.Size = New System.Drawing.Size(446, 26)
            Me.lblCcnResumenTotal.TabIndex = 0
            Me.lblCcnResumenTotal.Text = "Total a Pagar: $24.00"
            Me.lblCcnResumenTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblCcnResumenTotal.UseMnemonic = False
            '
            'cboCcnEstadoPago
            '
            Me.cboCcnEstadoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCcnEstadoPago.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.cboCcnEstadoPago.FormattingEnabled = True
            Me.cboCcnEstadoPago.Location = New System.Drawing.Point(24, 380)
            Me.cboCcnEstadoPago.Name = "cboCcnEstadoPago"
            Me.cboCcnEstadoPago.Size = New System.Drawing.Size(472, 25)
            Me.cboCcnEstadoPago.TabIndex = 14
            '
            'lblCcnEtiquetaPago
            '
            Me.lblCcnEtiquetaPago.AutoSize = True
            Me.lblCcnEtiquetaPago.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaPago.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaPago.Location = New System.Drawing.Point(24, 358)
            Me.lblCcnEtiquetaPago.Name = "lblCcnEtiquetaPago"
            Me.lblCcnEtiquetaPago.Size = New System.Drawing.Size(201, 17)
            Me.lblCcnEtiquetaPago.TabIndex = 13
            Me.lblCcnEtiquetaPago.Text = "💳 Estado Inicial de Pago / Caja:"
            Me.lblCcnEtiquetaPago.UseMnemonic = False
            '
            'chkCcnAlergenoCeliaco
            '
            Me.chkCcnAlergenoCeliaco.AutoSize = True
            Me.chkCcnAlergenoCeliaco.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
            Me.chkCcnAlergenoCeliaco.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.chkCcnAlergenoCeliaco.Location = New System.Drawing.Point(24, 325)
            Me.chkCcnAlergenoCeliaco.Name = "chkCcnAlergenoCeliaco"
            Me.chkCcnAlergenoCeliaco.Size = New System.Drawing.Size(325, 19)
            Me.chkCcnAlergenoCeliaco.TabIndex = 12
            Me.chkCcnAlergenoCeliaco.Text = "⚠ Alerta Médica: Cliente Celíaco / Sin TACC ni Gluten"
            Me.chkCcnAlergenoCeliaco.UseVisualStyleBackColor = True
            '
            'txtCcnNotas
            '
            Me.txtCcnNotas.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.txtCcnNotas.Location = New System.Drawing.Point(24, 288)
            Me.txtCcnNotas.Name = "txtCcnNotas"
            Me.txtCcnNotas.Size = New System.Drawing.Size(472, 25)
            Me.txtCcnNotas.TabIndex = 11
            '
            'lblCcnEtiquetaNotas
            '
            Me.lblCcnEtiquetaNotas.AutoSize = True
            Me.lblCcnEtiquetaNotas.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaNotas.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaNotas.Location = New System.Drawing.Point(24, 266)
            Me.lblCcnEtiquetaNotas.Name = "lblCcnEtiquetaNotas"
            Me.lblCcnEtiquetaNotas.Size = New System.Drawing.Size(250, 17)
            Me.lblCcnEtiquetaNotas.TabIndex = 10
            Me.lblCcnEtiquetaNotas.Text = "📝 Acompañamiento / Notas de Cocina:"
            Me.lblCcnEtiquetaNotas.UseMnemonic = False
            '
            'numCcnCantidad
            '
            Me.numCcnCantidad.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.numCcnCantidad.Location = New System.Drawing.Point(396, 226)
            Me.numCcnCantidad.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
            Me.numCcnCantidad.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numCcnCantidad.Name = "numCcnCantidad"
            Me.numCcnCantidad.Size = New System.Drawing.Size(100, 25)
            Me.numCcnCantidad.TabIndex = 9
            Me.numCcnCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.numCcnCantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'lblCcnEtiquetaCantidad
            '
            Me.lblCcnEtiquetaCantidad.AutoSize = True
            Me.lblCcnEtiquetaCantidad.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaCantidad.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaCantidad.Location = New System.Drawing.Point(396, 204)
            Me.lblCcnEtiquetaCantidad.Name = "lblCcnEtiquetaCantidad"
            Me.lblCcnEtiquetaCantidad.Size = New System.Drawing.Size(68, 17)
            Me.lblCcnEtiquetaCantidad.TabIndex = 8
            Me.lblCcnEtiquetaCantidad.Text = "Cantidad:"
            Me.lblCcnEtiquetaCantidad.UseMnemonic = False
            '
            'cboCcnPlato
            '
            Me.cboCcnPlato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCcnPlato.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.cboCcnPlato.FormattingEnabled = True
            Me.cboCcnPlato.Location = New System.Drawing.Point(24, 226)
            Me.cboCcnPlato.Name = "cboCcnPlato"
            Me.cboCcnPlato.Size = New System.Drawing.Size(356, 25)
            Me.cboCcnPlato.TabIndex = 7
            '
            'lblCcnEtiquetaPlato
            '
            Me.lblCcnEtiquetaPlato.AutoSize = True
            Me.lblCcnEtiquetaPlato.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaPlato.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaPlato.Location = New System.Drawing.Point(24, 204)
            Me.lblCcnEtiquetaPlato.Name = "lblCcnEtiquetaPlato"
            Me.lblCcnEtiquetaPlato.Size = New System.Drawing.Size(183, 17)
            Me.lblCcnEtiquetaPlato.TabIndex = 6
            Me.lblCcnEtiquetaPlato.Text = "🍲 Plato Principal del Menú:"
            Me.lblCcnEtiquetaPlato.UseMnemonic = False
            '
            'txtCcnMesa
            '
            Me.txtCcnMesa.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.txtCcnMesa.Location = New System.Drawing.Point(276, 162)
            Me.txtCcnMesa.Name = "txtCcnMesa"
            Me.txtCcnMesa.Size = New System.Drawing.Size(220, 25)
            Me.txtCcnMesa.TabIndex = 5
            Me.txtCcnMesa.Text = "Mesa 05"
            '
            'lblCcnEtiquetaMesa
            '
            Me.lblCcnEtiquetaMesa.AutoSize = True
            Me.lblCcnEtiquetaMesa.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaMesa.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaMesa.Location = New System.Drawing.Point(276, 140)
            Me.lblCcnEtiquetaMesa.Name = "lblCcnEtiquetaMesa"
            Me.lblCcnEtiquetaMesa.Size = New System.Drawing.Size(147, 17)
            Me.lblCcnEtiquetaMesa.TabIndex = 4
            Me.lblCcnEtiquetaMesa.Text = "🪑 Ubicación / Mesa:"
            Me.lblCcnEtiquetaMesa.UseMnemonic = False
            '
            'cboCboTipoServicio
            '
            Me.cboCcnTipoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCcnTipoServicio.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.cboCcnTipoServicio.FormattingEnabled = True
            Me.cboCcnTipoServicio.Location = New System.Drawing.Point(24, 162)
            Me.cboCcnTipoServicio.Name = "cboCcnTipoServicio"
            Me.cboCcnTipoServicio.Size = New System.Drawing.Size(236, 25)
            Me.cboCcnTipoServicio.TabIndex = 3
            '
            'lblCcnEtiquetaServicio
            '
            Me.lblCcnEtiquetaServicio.AutoSize = True
            Me.lblCcnEtiquetaServicio.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaServicio.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaServicio.Location = New System.Drawing.Point(24, 140)
            Me.lblCcnEtiquetaServicio.Name = "lblCcnEtiquetaServicio"
            Me.lblCcnEtiquetaServicio.Size = New System.Drawing.Size(133, 17)
            Me.lblCcnEtiquetaServicio.TabIndex = 2
            Me.lblCcnEtiquetaServicio.Text = "🛎 Tipo de Servicio:"
            Me.lblCcnEtiquetaServicio.UseMnemonic = False
            '
            'txtCcnCliente
            '
            Me.txtCcnCliente.Font = New System.Drawing.Font("Segoe UI", 10.0!)
            Me.txtCcnCliente.Location = New System.Drawing.Point(24, 98)
            Me.txtCcnCliente.Name = "txtCcnCliente"
            Me.txtCcnCliente.Size = New System.Drawing.Size(472, 25)
            Me.txtCcnCliente.TabIndex = 1
            '
            'lblCcnEtiquetaCliente
            '
            Me.lblCcnEtiquetaCliente.AutoSize = True
            Me.lblCcnEtiquetaCliente.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnEtiquetaCliente.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnEtiquetaCliente.Location = New System.Drawing.Point(24, 76)
            Me.lblCcnEtiquetaCliente.Name = "lblCcnEtiquetaCliente"
            Me.lblCcnEtiquetaCliente.Size = New System.Drawing.Size(147, 17)
            Me.lblCcnEtiquetaCliente.TabIndex = 0
            Me.lblCcnEtiquetaCliente.Text = "👤 Nombre del Cliente:"
            Me.lblCcnEtiquetaCliente.UseMnemonic = False
            '
            'pnlCcnBotonesAccion
            '
            Me.pnlCcnBotonesAccion.BackColor = System.Drawing.Color.FromArgb(248, 246, 242)
            Me.pnlCcnBotonesAccion.Controls.Add(Me.btnCcnCancelar)
            Me.pnlCcnBotonesAccion.Controls.Add(Me.btnCcnGuardarPedido)
            Me.pnlCcnBotonesAccion.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlCcnBotonesAccion.Location = New System.Drawing.Point(0, 555)
            Me.pnlCcnBotonesAccion.Name = "pnlCcnBotonesAccion"
            Me.pnlCcnBotonesAccion.Padding = New System.Windows.Forms.Padding(20, 10, 20, 12)
            Me.pnlCcnBotonesAccion.Size = New System.Drawing.Size(520, 65)
            Me.pnlCcnBotonesAccion.TabIndex = 2
            '
            'btnCcnCancelar
            '
            Me.btnCcnCancelar.BackColor = System.Drawing.Color.White
            Me.btnCcnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnCancelar.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnCcnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.btnCcnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnCancelar.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnCcnCancelar.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.btnCcnCancelar.Location = New System.Drawing.Point(220, 10)
            Me.btnCcnCancelar.Name = "btnCcnCancelar"
            Me.btnCcnCancelar.Size = New System.Drawing.Size(110, 43)
            Me.btnCcnCancelar.TabIndex = 1
            Me.btnCcnCancelar.Text = "Cancelar"
            Me.btnCcnCancelar.UseVisualStyleBackColor = False
            Me.btnCcnCancelar.UseMnemonic = False
            '
            'btnCcnGuardarPedido
            '
            Me.btnCcnGuardarPedido.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.btnCcnGuardarPedido.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnGuardarPedido.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnCcnGuardarPedido.FlatAppearance.BorderSize = 0
            Me.btnCcnGuardarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnGuardarPedido.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
            Me.btnCcnGuardarPedido.ForeColor = System.Drawing.Color.White
            Me.btnCcnGuardarPedido.Location = New System.Drawing.Point(340, 10)
            Me.btnCcnGuardarPedido.Name = "btnCcnGuardarPedido"
            Me.btnCcnGuardarPedido.Size = New System.Drawing.Size(160, 43)
            Me.btnCcnGuardarPedido.TabIndex = 0
            Me.btnCcnGuardarPedido.Text = "🍳 Enviar a Cocina"
            Me.btnCcnGuardarPedido.UseVisualStyleBackColor = False
            Me.btnCcnGuardarPedido.UseMnemonic = False
            '
            'FrmCcnNuevoPedidoDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(520, 620)
            Me.Controls.Add(Me.pnlCcnContenedorForm)
            Me.Controls.Add(Me.pnlCcnBotonesAccion)
            Me.Controls.Add(Me.pnlCcnHeaderModal)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmCcnNuevoPedidoDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Registrar Pedido / Comanda"
            Me.pnlCcnHeaderModal.ResumeLayout(False)
            Me.pnlCcnHeaderModal.PerformLayout()
            Me.pnlCcnContenedorForm.ResumeLayout(False)
            Me.pnlCcnContenedorForm.PerformLayout()
            Me.pnlCcnResumenTotal.ResumeLayout(False)
            CType(Me.numCcnCantidad, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlCcnBotonesAccion.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
    End Class
End Namespace
