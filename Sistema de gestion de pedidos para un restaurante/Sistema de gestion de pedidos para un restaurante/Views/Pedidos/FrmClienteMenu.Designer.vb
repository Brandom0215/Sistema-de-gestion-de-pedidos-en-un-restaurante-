Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Pedidos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmClienteMenu
        Inherits Views.Common.FrmBaseForm

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
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblTituloHeader = New System.Windows.Forms.Label()
            Me.lblSubtituloHeader = New System.Windows.Forms.Label()

            Me.pnlContenedorPrincipal = New System.Windows.Forms.Panel()
            
            ' Panel Derecho: Carrito y Datos de Entrega
            Me.pnlDerecho = New System.Windows.Forms.Panel()
            Me.grpCarrito = New System.Windows.Forms.GroupBox()
            Me.dgvCarrito = New System.Windows.Forms.DataGridView()
            Me.btnEliminarItemCarrito = New System.Windows.Forms.Button()
            Me.btnVaciarCarrito = New System.Windows.Forms.Button()

            Me.grpExtrasYBebidas = New System.Windows.Forms.GroupBox()
            Me.chkExtraSoda = New System.Windows.Forms.CheckBox()
            Me.numExtraSoda = New System.Windows.Forms.NumericUpDown()
            Me.chkExtraJugo = New System.Windows.Forms.CheckBox()
            Me.numExtraJugo = New System.Windows.Forms.NumericUpDown()
            Me.chkExtraPapas = New System.Windows.Forms.CheckBox()
            Me.numExtraPapas = New System.Windows.Forms.NumericUpDown()
            Me.chkExtraEnsalada = New System.Windows.Forms.CheckBox()
            Me.numExtraEnsalada = New System.Windows.Forms.NumericUpDown()

            Me.grpDatosCliente = New System.Windows.Forms.GroupBox()
            Me.lblNombreCliente = New System.Windows.Forms.Label()
            Me.txtNombreCliente = New System.Windows.Forms.TextBox()
            Me.lblMesa = New System.Windows.Forms.Label()
            Me.txtMesa = New System.Windows.Forms.TextBox()
            Me.lblTipoServicio = New System.Windows.Forms.Label()
            Me.cboTipoServicio = New System.Windows.Forms.ComboBox()

            Me.pnlResumenYConfirmacion = New System.Windows.Forms.Panel()
            Me.lblEtiquetaTotal = New System.Windows.Forms.Label()
            Me.lblMontoTotal = New System.Windows.Forms.Label()
            Me.btnConfirmarPedido = New System.Windows.Forms.Button()

            ' Panel Izquierdo: Categorías y Tarjetas de Platos
            Me.pnlIzquierdo = New System.Windows.Forms.Panel()
            Me.pnlFiltrosBarra = New System.Windows.Forms.Panel()
            Me.lblCategoria = New System.Windows.Forms.Label()
            Me.cboCategoria = New System.Windows.Forms.ComboBox()
            Me.lblBuscarPlato = New System.Windows.Forms.Label()
            Me.txtBuscarPlato = New System.Windows.Forms.TextBox()
            Me.flpCatalogoTarjetas = New System.Windows.Forms.FlowLayoutPanel()

            Me.pnlHeader.SuspendLayout()
            Me.pnlContenedorPrincipal.SuspendLayout()
            Me.pnlDerecho.SuspendLayout()
            Me.grpCarrito.SuspendLayout()
            CType(Me.dgvCarrito, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpExtrasYBebidas.SuspendLayout()
            CType(Me.numExtraSoda, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.numExtraJugo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.numExtraPapas, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.numExtraEnsalada, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpDatosCliente.SuspendLayout()
            Me.pnlResumenYConfirmacion.SuspendLayout()
            Me.pnlIzquierdo.SuspendLayout()
            Me.pnlFiltrosBarra.SuspendLayout()
            Me.SuspendLayout()

            ' pnlHeader
            Me.pnlHeader.BackColor = System.Drawing.Color.White
            Me.pnlHeader.Controls.Add(Me.lblSubtituloHeader)
            Me.pnlHeader.Controls.Add(Me.lblTituloHeader)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(1200, 60)
            Me.pnlHeader.TabIndex = 0

            ' lblTituloHeader
            Me.lblTituloHeader.AutoSize = True
            Me.lblTituloHeader.Font = New System.Drawing.Font("Segoe UI", 14.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloHeader.Location = New System.Drawing.Point(20, 10)
            Me.lblTituloHeader.Name = "lblTituloHeader"
            Me.lblTituloHeader.Size = New System.Drawing.Size(350, 25)
            Me.lblTituloHeader.TabIndex = 0
            Me.lblTituloHeader.Text = "Menú Digital & Carrito de Pedidos"

            ' lblSubtituloHeader
            Me.lblSubtituloHeader.AutoSize = True
            Me.lblSubtituloHeader.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblSubtituloHeader.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloHeader.Location = New System.Drawing.Point(20, 35)
            Me.lblSubtituloHeader.Name = "lblSubtituloHeader"
            Me.lblSubtituloHeader.Size = New System.Drawing.Size(580, 15)
            Me.lblSubtituloHeader.TabIndex = 1
            Me.lblSubtituloHeader.Text = "Explore los platos visuales por categoría, agregue múltiples ítems al carrito y confirme su pedido."

            ' pnlContenedorPrincipal (DOCKING ORDENADO PARA EVITAR COLISIONES)
            Me.pnlContenedorPrincipal.Controls.Add(Me.pnlIzquierdo)
            Me.pnlContenedorPrincipal.Controls.Add(Me.pnlDerecho)
            Me.pnlContenedorPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenedorPrincipal.Location = New System.Drawing.Point(0, 60)
            Me.pnlContenedorPrincipal.Name = "pnlContenedorPrincipal"
            Me.pnlContenedorPrincipal.Padding = New System.Windows.Forms.Padding(10)
            Me.pnlContenedorPrincipal.Size = New System.Drawing.Size(1200, 620)
            Me.pnlContenedorPrincipal.TabIndex = 1

            ' pnlDerecho (ANCLADO A LA DERECHA SIN SOLAPAMIENTO)
            Me.pnlDerecho.Controls.Add(Me.pnlResumenYConfirmacion)
            Me.pnlDerecho.Controls.Add(Me.grpDatosCliente)
            Me.pnlDerecho.Controls.Add(Me.grpExtrasYBebidas)
            Me.pnlDerecho.Controls.Add(Me.grpCarrito)
            Me.pnlDerecho.Dock = System.Windows.Forms.DockStyle.Right
            Me.pnlDerecho.Location = New System.Drawing.Point(720, 10)
            Me.pnlDerecho.Name = "pnlDerecho"
            Me.pnlDerecho.Size = New System.Drawing.Size(470, 600)
            Me.pnlDerecho.TabIndex = 1

            ' grpCarrito
            Me.grpCarrito.Controls.Add(Me.btnVaciarCarrito)
            Me.grpCarrito.Controls.Add(Me.btnEliminarItemCarrito)
            Me.grpCarrito.Controls.Add(Me.dgvCarrito)
            Me.grpCarrito.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpCarrito.Location = New System.Drawing.Point(0, 0)
            Me.grpCarrito.Name = "grpCarrito"
            Me.grpCarrito.Size = New System.Drawing.Size(470, 225)
            Me.grpCarrito.TabIndex = 0
            Me.grpCarrito.TabStop = False
            Me.grpCarrito.Text = "🛒 Carrito de Compras"

            ' dgvCarrito
            Me.dgvCarrito.AllowUserToAddRows = False
            Me.dgvCarrito.AllowUserToDeleteRows = False
            Me.dgvCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvCarrito.Location = New System.Drawing.Point(10, 22)
            Me.dgvCarrito.MultiSelect = False
            Me.dgvCarrito.Name = "dgvCarrito"
            Me.dgvCarrito.ReadOnly = True
            Me.dgvCarrito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvCarrito.Size = New System.Drawing.Size(450, 155)
            Me.dgvCarrito.TabIndex = 0

            ' btnEliminarItemCarrito
            Me.btnEliminarItemCarrito.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.btnEliminarItemCarrito.Location = New System.Drawing.Point(10, 184)
            Me.btnEliminarItemCarrito.Name = "btnEliminarItemCarrito"
            Me.btnEliminarItemCarrito.Size = New System.Drawing.Size(140, 32)
            Me.btnEliminarItemCarrito.TabIndex = 1
            Me.btnEliminarItemCarrito.Text = "Eliminar Selección"
            Me.btnEliminarItemCarrito.UseVisualStyleBackColor = True

            ' btnVaciarCarrito
            Me.btnVaciarCarrito.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.btnVaciarCarrito.Location = New System.Drawing.Point(160, 184)
            Me.btnVaciarCarrito.Name = "btnVaciarCarrito"
            Me.btnVaciarCarrito.Size = New System.Drawing.Size(110, 32)
            Me.btnVaciarCarrito.TabIndex = 2
            Me.btnVaciarCarrito.Text = "Vaciar Carrito"
            Me.btnVaciarCarrito.UseVisualStyleBackColor = True

            ' grpExtrasYBebidas (CON CANTIDAD PARA CADA EXTRA)
            Me.grpExtrasYBebidas.Controls.Add(Me.numExtraEnsalada)
            Me.grpExtrasYBebidas.Controls.Add(Me.chkExtraEnsalada)
            Me.grpExtrasYBebidas.Controls.Add(Me.numExtraPapas)
            Me.grpExtrasYBebidas.Controls.Add(Me.chkExtraPapas)
            Me.grpExtrasYBebidas.Controls.Add(Me.numExtraJugo)
            Me.grpExtrasYBebidas.Controls.Add(Me.chkExtraJugo)
            Me.grpExtrasYBebidas.Controls.Add(Me.numExtraSoda)
            Me.grpExtrasYBebidas.Controls.Add(Me.chkExtraSoda)
            Me.grpExtrasYBebidas.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpExtrasYBebidas.Location = New System.Drawing.Point(0, 235)
            Me.grpExtrasYBebidas.Name = "grpExtrasYBebidas"
            Me.grpExtrasYBebidas.Size = New System.Drawing.Size(470, 115)
            Me.grpExtrasYBebidas.TabIndex = 1
            Me.grpExtrasYBebidas.TabStop = False
            Me.grpExtrasYBebidas.Text = "Extras & Bebidas (Selección y Cantidad)"

            ' chkExtraSoda
            Me.chkExtraSoda.AutoSize = True
            Me.chkExtraSoda.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.chkExtraSoda.Location = New System.Drawing.Point(10, 22)
            Me.chkExtraSoda.Name = "chkExtraSoda"
            Me.chkExtraSoda.Size = New System.Drawing.Size(160, 19)
            Me.chkExtraSoda.TabIndex = 0
            Me.chkExtraSoda.Text = "Soda Nacional ($1.50)"

            ' numExtraSoda
            Me.numExtraSoda.Enabled = False
            Me.numExtraSoda.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.numExtraSoda.Location = New System.Drawing.Point(175, 20)
            Me.numExtraSoda.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numExtraSoda.Name = "numExtraSoda"
            Me.numExtraSoda.Size = New System.Drawing.Size(45, 23)
            Me.numExtraSoda.TabIndex = 1
            Me.numExtraSoda.Value = New Decimal(New Integer() {1, 0, 0, 0})

            ' chkExtraJugo
            Me.chkExtraJugo.AutoSize = True
            Me.chkExtraJugo.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.chkExtraJugo.Location = New System.Drawing.Point(235, 22)
            Me.chkExtraJugo.Name = "chkExtraJugo"
            Me.chkExtraJugo.Size = New System.Drawing.Size(175, 19)
            Me.chkExtraJugo.TabIndex = 2
            Me.chkExtraJugo.Text = "Chicha de Nance ($2.00)"

            ' numExtraJugo
            Me.numExtraJugo.Enabled = False
            Me.numExtraJugo.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.numExtraJugo.Location = New System.Drawing.Point(415, 20)
            Me.numExtraJugo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numExtraJugo.Name = "numExtraJugo"
            Me.numExtraJugo.Size = New System.Drawing.Size(45, 23)
            Me.numExtraJugo.TabIndex = 3
            Me.numExtraJugo.Value = New Decimal(New Integer() {1, 0, 0, 0})

            ' chkExtraPapas
            Me.chkExtraPapas.AutoSize = True
            Me.chkExtraPapas.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.chkExtraPapas.Location = New System.Drawing.Point(10, 65)
            Me.chkExtraPapas.Name = "chkExtraPapas"
            Me.chkExtraPapas.Size = New System.Drawing.Size(160, 19)
            Me.chkExtraPapas.TabIndex = 4
            Me.chkExtraPapas.Text = "Limón c/ Raspadura ($1.75)"

            ' numExtraPapas
            Me.numExtraPapas.Enabled = False
            Me.numExtraPapas.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.numExtraPapas.Location = New System.Drawing.Point(175, 63)
            Me.numExtraPapas.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numExtraPapas.Name = "numExtraPapas"
            Me.numExtraPapas.Size = New System.Drawing.Size(45, 23)
            Me.numExtraPapas.TabIndex = 5
            Me.numExtraPapas.Value = New Decimal(New Integer() {1, 0, 0, 0})

            ' chkExtraEnsalada
            Me.chkExtraEnsalada.AutoSize = True
            Me.chkExtraEnsalada.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.chkExtraEnsalada.Location = New System.Drawing.Point(235, 65)
            Me.chkExtraEnsalada.Name = "chkExtraEnsalada"
            Me.chkExtraEnsalada.Size = New System.Drawing.Size(175, 19)
            Me.chkExtraEnsalada.TabIndex = 6
            Me.chkExtraEnsalada.Text = "Chicha de Naranja ($1.75)"

            ' numExtraEnsalada
            Me.numExtraEnsalada.Enabled = False
            Me.numExtraEnsalada.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.numExtraEnsalada.Location = New System.Drawing.Point(415, 63)
            Me.numExtraEnsalada.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numExtraEnsalada.Name = "numExtraEnsalada"
            Me.numExtraEnsalada.Size = New System.Drawing.Size(45, 23)
            Me.numExtraEnsalada.TabIndex = 7
            Me.numExtraEnsalada.Value = New Decimal(New Integer() {1, 0, 0, 0})

            ' grpDatosCliente
            Me.grpDatosCliente.Controls.Add(Me.cboTipoServicio)
            Me.grpDatosCliente.Controls.Add(Me.lblTipoServicio)
            Me.grpDatosCliente.Controls.Add(Me.txtMesa)
            Me.grpDatosCliente.Controls.Add(Me.lblMesa)
            Me.grpDatosCliente.Controls.Add(Me.txtNombreCliente)
            Me.grpDatosCliente.Controls.Add(Me.lblNombreCliente)
            Me.grpDatosCliente.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpDatosCliente.Location = New System.Drawing.Point(0, 360)
            Me.grpDatosCliente.Name = "grpDatosCliente"
            Me.grpDatosCliente.Size = New System.Drawing.Size(470, 130)
            Me.grpDatosCliente.TabIndex = 2
            Me.grpDatosCliente.TabStop = False
            Me.grpDatosCliente.Text = "Datos del Cliente & Entrega (Sin Registro)"

            ' lblNombreCliente
            Me.lblNombreCliente.AutoSize = True
            Me.lblNombreCliente.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombreCliente.Location = New System.Drawing.Point(10, 20)
            Me.lblNombreCliente.Name = "lblNombreCliente"
            Me.lblNombreCliente.Size = New System.Drawing.Size(117, 15)
            Me.lblNombreCliente.TabIndex = 0
            Me.lblNombreCliente.Text = "Nombre del Cliente:"

            ' txtNombreCliente
            Me.txtNombreCliente.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtNombreCliente.Location = New System.Drawing.Point(10, 38)
            Me.txtNombreCliente.Name = "txtNombreCliente"
            Me.txtNombreCliente.Size = New System.Drawing.Size(445, 23)
            Me.txtNombreCliente.TabIndex = 1

            ' lblMesa
            Me.lblMesa.AutoSize = True
            Me.lblMesa.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblMesa.Location = New System.Drawing.Point(10, 68)
            Me.lblMesa.Name = "lblMesa"
            Me.lblMesa.Size = New System.Drawing.Size(118, 15)
            Me.lblMesa.TabIndex = 2
            Me.lblMesa.Text = "N° Mesa / Dirección:"

            ' txtMesa
            Me.txtMesa.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtMesa.Location = New System.Drawing.Point(10, 86)
            Me.txtMesa.Name = "txtMesa"
            Me.txtMesa.Size = New System.Drawing.Size(205, 23)
            Me.txtMesa.TabIndex = 3

            ' lblTipoServicio
            Me.lblTipoServicio.AutoSize = True
            Me.lblTipoServicio.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblTipoServicio.Location = New System.Drawing.Point(235, 68)
            Me.lblTipoServicio.Name = "lblTipoServicio"
            Me.lblTipoServicio.Size = New System.Drawing.Size(95, 15)
            Me.lblTipoServicio.TabIndex = 4
            Me.lblTipoServicio.Text = "Tipo de Servicio:"

            ' cboTipoServicio
            Me.cboTipoServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboTipoServicio.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.cboTipoServicio.FormattingEnabled = True
            Me.cboTipoServicio.Location = New System.Drawing.Point(235, 86)
            Me.cboTipoServicio.Name = "cboTipoServicio"
            Me.cboTipoServicio.Size = New System.Drawing.Size(220, 23)
            Me.cboTipoServicio.TabIndex = 5

            ' pnlResumenYConfirmacion
            Me.pnlResumenYConfirmacion.BackColor = System.Drawing.Color.White
            Me.pnlResumenYConfirmacion.Controls.Add(Me.btnConfirmarPedido)
            Me.pnlResumenYConfirmacion.Controls.Add(Me.lblMontoTotal)
            Me.pnlResumenYConfirmacion.Controls.Add(Me.lblEtiquetaTotal)
            Me.pnlResumenYConfirmacion.Location = New System.Drawing.Point(0, 500)
            Me.pnlResumenYConfirmacion.Name = "pnlResumenYConfirmacion"
            Me.pnlResumenYConfirmacion.Size = New System.Drawing.Size(470, 95)
            Me.pnlResumenYConfirmacion.TabIndex = 3

            ' lblEtiquetaTotal
            Me.lblEtiquetaTotal.AutoSize = True
            Me.lblEtiquetaTotal.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblEtiquetaTotal.Location = New System.Drawing.Point(10, 12)
            Me.lblEtiquetaTotal.Name = "lblEtiquetaTotal"
            Me.lblEtiquetaTotal.Size = New System.Drawing.Size(140, 20)
            Me.lblEtiquetaTotal.TabIndex = 0
            Me.lblEtiquetaTotal.Text = "TOTAL A PAGAR:"

            ' lblMontoTotal
            Me.lblMontoTotal.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoTotal.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblMontoTotal.Location = New System.Drawing.Point(160, 8)
            Me.lblMontoTotal.Name = "lblMontoTotal"
            Me.lblMontoTotal.Size = New System.Drawing.Size(295, 30)
            Me.lblMontoTotal.TabIndex = 1
            Me.lblMontoTotal.Text = "$ 0.00"
            Me.lblMontoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' btnConfirmarPedido
            Me.btnConfirmarPedido.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnConfirmarPedido.Location = New System.Drawing.Point(10, 45)
            Me.btnConfirmarPedido.Name = "btnConfirmarPedido"
            Me.btnConfirmarPedido.Size = New System.Drawing.Size(445, 40)
            Me.btnConfirmarPedido.TabIndex = 2
            Me.btnConfirmarPedido.Text = "Confirmar y Enviar Carrito"
            Me.btnConfirmarPedido.UseVisualStyleBackColor = True

            ' pnlIzquierdo (COMPLETAMENTE LIBRE A LA IZQUIERDA)
            Me.pnlIzquierdo.Controls.Add(Me.flpCatalogoTarjetas)
            Me.pnlIzquierdo.Controls.Add(Me.pnlFiltrosBarra)
            Me.pnlIzquierdo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlIzquierdo.Location = New System.Drawing.Point(10, 10)
            Me.pnlIzquierdo.Name = "pnlIzquierdo"
            Me.pnlIzquierdo.Size = New System.Drawing.Size(740, 600)
            Me.pnlIzquierdo.TabIndex = 0

            ' pnlFiltrosBarra
            Me.pnlFiltrosBarra.BackColor = System.Drawing.Color.White
            Me.pnlFiltrosBarra.Controls.Add(Me.txtBuscarPlato)
            Me.pnlFiltrosBarra.Controls.Add(Me.lblBuscarPlato)
            Me.pnlFiltrosBarra.Controls.Add(Me.cboCategoria)
            Me.pnlFiltrosBarra.Controls.Add(Me.lblCategoria)
            Me.pnlFiltrosBarra.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltrosBarra.Location = New System.Drawing.Point(0, 0)
            Me.pnlFiltrosBarra.Name = "pnlFiltrosBarra"
            Me.pnlFiltrosBarra.Size = New System.Drawing.Size(740, 60)
            Me.pnlFiltrosBarra.TabIndex = 0

            ' lblCategoria
            Me.lblCategoria.AutoSize = True
            Me.lblCategoria.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblCategoria.Location = New System.Drawing.Point(10, 8)
            Me.lblCategoria.Name = "lblCategoria"
            Me.lblCategoria.Size = New System.Drawing.Size(63, 15)
            Me.lblCategoria.TabIndex = 0
            Me.lblCategoria.Text = "Categoría:"

            ' cboCategoria
            Me.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCategoria.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.cboCategoria.FormattingEnabled = True
            Me.cboCategoria.Location = New System.Drawing.Point(10, 26)
            Me.cboCategoria.Name = "cboCategoria"
            Me.cboCategoria.Size = New System.Drawing.Size(230, 23)
            Me.cboCategoria.TabIndex = 1

            ' lblBuscarPlato
            Me.lblBuscarPlato.AutoSize = True
            Me.lblBuscarPlato.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblBuscarPlato.Location = New System.Drawing.Point(260, 8)
            Me.lblBuscarPlato.Name = "lblBuscarPlato"
            Me.lblBuscarPlato.Text = "Buscar Plato:"
            Me.lblBuscarPlato.Size = New System.Drawing.Size(76, 15)

            ' txtBuscarPlato
            Me.txtBuscarPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtBuscarPlato.Location = New System.Drawing.Point(260, 26)
            Me.txtBuscarPlato.Name = "txtBuscarPlato"
            Me.txtBuscarPlato.Size = New System.Drawing.Size(260, 23)
            Me.txtBuscarPlato.TabIndex = 3

            ' flpCatalogoTarjetas
            Me.flpCatalogoTarjetas.AutoScroll = True
            Me.flpCatalogoTarjetas.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.flpCatalogoTarjetas.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpCatalogoTarjetas.Location = New System.Drawing.Point(0, 60)
            Me.flpCatalogoTarjetas.Name = "flpCatalogoTarjetas"
            Me.flpCatalogoTarjetas.Padding = New System.Windows.Forms.Padding(8)
            Me.flpCatalogoTarjetas.Size = New System.Drawing.Size(740, 540)
            Me.flpCatalogoTarjetas.TabIndex = 1

            ' FrmClienteMenu
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(1200, 680)
            Me.Controls.Add(Me.pnlContenedorPrincipal)
            Me.Controls.Add(Me.pnlHeader)
            Me.Name = "FrmClienteMenu"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Menú Digital & Carrito de Pedidos"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlContenedorPrincipal.ResumeLayout(False)
            Me.pnlDerecho.ResumeLayout(False)
            Me.grpCarrito.ResumeLayout(False)
            CType(Me.dgvCarrito, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpExtrasYBebidas.ResumeLayout(False)
            Me.grpExtrasYBebidas.PerformLayout()
            CType(Me.numExtraSoda, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.numExtraJugo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.numExtraPapas, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.numExtraEnsalada, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpDatosCliente.ResumeLayout(False)
            Me.grpDatosCliente.PerformLayout()
            Me.pnlResumenYConfirmacion.ResumeLayout(False)
            Me.pnlResumenYConfirmacion.PerformLayout()
            Me.pnlIzquierdo.ResumeLayout(False)
            Me.pnlFiltrosBarra.ResumeLayout(False)
            Me.pnlFiltrosBarra.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As Panel
        Friend WithEvents lblTituloHeader As Label
        Friend WithEvents lblSubtituloHeader As Label
        Friend WithEvents pnlContenedorPrincipal As Panel
        Friend WithEvents pnlIzquierdo As Panel
        Friend WithEvents pnlFiltrosBarra As Panel
        Friend WithEvents lblCategoria As Label
        Friend WithEvents cboCategoria As ComboBox
        Friend WithEvents lblBuscarPlato As Label
        Friend WithEvents txtBuscarPlato As TextBox
        Friend WithEvents flpCatalogoTarjetas As FlowLayoutPanel
        Friend WithEvents pnlDerecho As Panel
        Friend WithEvents grpCarrito As GroupBox
        Friend WithEvents dgvCarrito As DataGridView
        Friend WithEvents btnEliminarItemCarrito As Button
        Friend WithEvents btnVaciarCarrito As Button
        Friend WithEvents grpExtrasYBebidas As GroupBox
        Friend WithEvents chkExtraSoda As CheckBox
        Friend WithEvents numExtraSoda As NumericUpDown
        Friend WithEvents chkExtraJugo As CheckBox
        Friend WithEvents numExtraJugo As NumericUpDown
        Friend WithEvents chkExtraPapas As CheckBox
        Friend WithEvents numExtraPapas As NumericUpDown
        Friend WithEvents chkExtraEnsalada As CheckBox
        Friend WithEvents numExtraEnsalada As NumericUpDown
        Friend WithEvents grpDatosCliente As GroupBox
        Friend WithEvents lblNombreCliente As Label
        Friend WithEvents txtNombreCliente As TextBox
        Friend WithEvents lblMesa As Label
        Friend WithEvents txtMesa As TextBox
        Friend WithEvents lblTipoServicio As Label
        Friend WithEvents cboTipoServicio As ComboBox
        Friend WithEvents pnlResumenYConfirmacion As Panel
        Friend WithEvents lblEtiquetaTotal As Label
        Friend WithEvents lblMontoTotal As Label
        Friend WithEvents btnConfirmarPedido As Button
    End Class
End Namespace
