Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Pedidos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmPedidos
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
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

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
        'Se puede modificar usando el Diseñador de Windows Forms.  
        'No lo modifique con el editor de código.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeader = New System.Windows.Forms.Panel()
            Me.lblTituloHeader = New System.Windows.Forms.Label()
            Me.lblSubtituloHeader = New System.Windows.Forms.Label()
            Me.tlpMainLayout = New System.Windows.Forms.TableLayoutPanel()
            
            ' Panel Izquierdo: Formulario
            Me.pnlFormularioContainer = New System.Windows.Forms.Panel()
            Me.grpFormulario = New System.Windows.Forms.GroupBox()
            Me.lblNombreCliente = New System.Windows.Forms.Label()
            Me.txtNombreCliente = New System.Windows.Forms.TextBox()
            Me.lblMesa = New System.Windows.Forms.Label()
            Me.txtMesa = New System.Windows.Forms.TextBox()
            Me.lblPlatoPrincipal = New System.Windows.Forms.Label()
            Me.cboPlatoPrincipal = New System.Windows.Forms.ComboBox()
            
            ' Acompañamientos CheckBoxes
            Me.grpAcompanamientos = New System.Windows.Forms.GroupBox()
            Me.chkPapasFritas = New System.Windows.Forms.CheckBox()
            Me.chkEnsalada = New System.Windows.Forms.CheckBox()
            Me.chkArroz = New System.Windows.Forms.CheckBox()
            Me.chkSalsas = New System.Windows.Forms.CheckBox()
            
            ' Tipo Servicio RadioButtons
            Me.grpTipoServicio = New System.Windows.Forms.GroupBox()
            Me.rbEnMesa = New System.Windows.Forms.RadioButton()
            Me.rbParaLlevar = New System.Windows.Forms.RadioButton()
            Me.rbDelivery = New System.Windows.Forms.RadioButton()
            
            ' Panel Botones CRUD
            Me.pnlBotonesCrud = New System.Windows.Forms.FlowLayoutPanel()
            Me.btnGuardar = New System.Windows.Forms.Button()
            Me.btnActualizar = New System.Windows.Forms.Button()
            Me.btnEliminar = New System.Windows.Forms.Button()
            Me.btnBuscar = New System.Windows.Forms.Button()
            Me.btnMostrarTodo = New System.Windows.Forms.Button()

            ' Panel Derecho: Grilla y Confirmación
            Me.pnlGrillaContainer = New System.Windows.Forms.Panel()
            Me.grpListado = New System.Windows.Forms.GroupBox()
            Me.dgvPedidos = New System.Windows.Forms.DataGridView()
            Me.pnlAccionesInferiores = New System.Windows.Forms.Panel()
            Me.btnVerConfirmado = New System.Windows.Forms.Button()
            Me.lblTotalRegistros = New System.Windows.Forms.Label()

            Me.pnlHeader.SuspendLayout()
            Me.tlpMainLayout.SuspendLayout()
            Me.pnlFormularioContainer.SuspendLayout()
            Me.grpFormulario.SuspendLayout()
            Me.grpAcompanamientos.SuspendLayout()
            Me.grpTipoServicio.SuspendLayout()
            Me.pnlBotonesCrud.SuspendLayout()
            Me.pnlGrillaContainer.SuspendLayout()
            Me.grpListado.SuspendLayout()
            CType(Me.dgvPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlAccionesInferiores.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlHeader
            '
            Me.pnlHeader.Controls.Add(Me.lblSubtituloHeader)
            Me.pnlHeader.Controls.Add(Me.lblTituloHeader)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(20, 20)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(1000, 55)
            Me.pnlHeader.TabIndex = 0
            '
            ' lblTituloHeader
            '
            Me.lblTituloHeader.AutoSize = True
            Me.lblTituloHeader.Font = New System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold)
            Me.lblTituloHeader.Location = New System.Drawing.Point(0, 0)
            Me.lblTituloHeader.Name = "lblTituloHeader"
            Me.lblTituloHeader.Size = New System.Drawing.Size(320, 25)
            Me.lblTituloHeader.TabIndex = 0
            Me.lblTituloHeader.Text = "Gestión de Pedidos del Restaurante"
            Me.lblTituloHeader.UseMnemonic = False
            '
            ' lblSubtituloHeader
            '
            Me.lblSubtituloHeader.AutoSize = True
            Me.lblSubtituloHeader.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloHeader.Location = New System.Drawing.Point(0, 28)
            Me.lblSubtituloHeader.Name = "lblSubtituloHeader"
            Me.lblSubtituloHeader.Size = New System.Drawing.Size(560, 15)
            Me.lblSubtituloHeader.TabIndex = 1
            Me.lblSubtituloHeader.Text = "Registro, actualización, consulta y previsualización de comandas activas en el sistema."
            Me.lblSubtituloHeader.UseMnemonic = False
            '
            ' tlpMainLayout
            '
            Me.tlpMainLayout.ColumnCount = 2
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 380.0F))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
            Me.tlpMainLayout.Controls.Add(Me.pnlFormularioContainer, 0, 0)
            Me.tlpMainLayout.Controls.Add(Me.pnlGrillaContainer, 1, 0)
            Me.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMainLayout.Location = New System.Drawing.Point(20, 75)
            Me.tlpMainLayout.Name = "tlpMainLayout"
            Me.tlpMainLayout.RowCount = 1
            Me.tlpMainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
            Me.tlpMainLayout.Size = New System.Drawing.Size(1000, 585)
            Me.tlpMainLayout.TabIndex = 1
            '
            ' pnlFormularioContainer
            '
            Me.pnlFormularioContainer.Controls.Add(Me.grpFormulario)
            Me.pnlFormularioContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlFormularioContainer.Location = New System.Drawing.Point(0, 0)
            Me.pnlFormularioContainer.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
            Me.pnlFormularioContainer.Name = "pnlFormularioContainer"
            Me.pnlFormularioContainer.Size = New System.Drawing.Size(370, 585)
            Me.pnlFormularioContainer.TabIndex = 0
            '
            ' grpFormulario
            '
            Me.grpFormulario.Controls.Add(Me.pnlBotonesCrud)
            Me.grpFormulario.Controls.Add(Me.grpTipoServicio)
            Me.grpFormulario.Controls.Add(Me.grpAcompanamientos)
            Me.grpFormulario.Controls.Add(Me.cboPlatoPrincipal)
            Me.grpFormulario.Controls.Add(Me.lblPlatoPrincipal)
            Me.grpFormulario.Controls.Add(Me.txtMesa)
            Me.grpFormulario.Controls.Add(Me.lblMesa)
            Me.grpFormulario.Controls.Add(Me.txtNombreCliente)
            Me.grpFormulario.Controls.Add(Me.lblNombreCliente)
            Me.grpFormulario.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpFormulario.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpFormulario.Location = New System.Drawing.Point(0, 0)
            Me.grpFormulario.Name = "grpFormulario"
            Me.grpFormulario.Padding = New System.Windows.Forms.Padding(12)
            Me.grpFormulario.Size = New System.Drawing.Size(370, 585)
            Me.grpFormulario.TabIndex = 0
            Me.grpFormulario.TabStop = False
            Me.grpFormulario.Text = "Datos del Pedido"
            '
            ' lblNombreCliente
            '
            Me.lblNombreCliente.AutoSize = True
            Me.lblNombreCliente.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Regular)
            Me.lblNombreCliente.Location = New System.Drawing.Point(15, 25)
            Me.lblNombreCliente.Name = "lblNombreCliente"
            Me.lblNombreCliente.Size = New System.Drawing.Size(113, 15)
            Me.lblNombreCliente.TabIndex = 0
            Me.lblNombreCliente.Text = "Nombre del Cliente:"
            Me.lblNombreCliente.UseMnemonic = False
            '
            ' txtNombreCliente
            '
            Me.txtNombreCliente.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtNombreCliente.Location = New System.Drawing.Point(15, 43)
            Me.txtNombreCliente.Name = "txtNombreCliente"
            Me.txtNombreCliente.Size = New System.Drawing.Size(335, 24)
            Me.txtNombreCliente.TabIndex = 1
            '
            ' lblMesa
            '
            Me.lblMesa.AutoSize = True
            Me.lblMesa.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Regular)
            Me.lblMesa.Location = New System.Drawing.Point(15, 75)
            Me.lblMesa.Name = "lblMesa"
            Me.lblMesa.Size = New System.Drawing.Size(100, 15)
            Me.lblMesa.TabIndex = 2
            Me.lblMesa.Text = "Número de Mesa:"
            Me.lblMesa.UseMnemonic = False
            '
            ' txtMesa
            '
            Me.txtMesa.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtMesa.Location = New System.Drawing.Point(15, 93)
            Me.txtMesa.Name = "txtMesa"
            Me.txtMesa.Size = New System.Drawing.Size(335, 24)
            Me.txtMesa.TabIndex = 3
            '
            ' lblPlatoPrincipal
            '
            Me.lblPlatoPrincipal.AutoSize = True
            Me.lblPlatoPrincipal.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Regular)
            Me.lblPlatoPrincipal.Location = New System.Drawing.Point(15, 125)
            Me.lblPlatoPrincipal.Name = "lblPlatoPrincipal"
            Me.lblPlatoPrincipal.Size = New System.Drawing.Size(87, 15)
            Me.lblPlatoPrincipal.TabIndex = 4
            Me.lblPlatoPrincipal.Text = "Plato Principal:"
            Me.lblPlatoPrincipal.UseMnemonic = False
            '
            ' cboPlatoPrincipal
            '
            Me.cboPlatoPrincipal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboPlatoPrincipal.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.cboPlatoPrincipal.FormattingEnabled = True
            Me.cboPlatoPrincipal.Location = New System.Drawing.Point(15, 143)
            Me.cboPlatoPrincipal.Name = "cboPlatoPrincipal"
            Me.cboPlatoPrincipal.Size = New System.Drawing.Size(335, 25)
            Me.cboPlatoPrincipal.TabIndex = 5
            '
            ' grpAcompanamientos
            '
            Me.grpAcompanamientos.Controls.Add(Me.chkSalsas)
            Me.grpAcompanamientos.Controls.Add(Me.chkArroz)
            Me.grpAcompanamientos.Controls.Add(Me.chkEnsalada)
            Me.grpAcompanamientos.Controls.Add(Me.chkPapasFritas)
            Me.grpAcompanamientos.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpAcompanamientos.Location = New System.Drawing.Point(15, 180)
            Me.grpAcompanamientos.Name = "grpAcompanamientos"
            Me.grpAcompanamientos.Size = New System.Drawing.Size(335, 85)
            Me.grpAcompanamientos.TabIndex = 6
            Me.grpAcompanamientos.TabStop = False
            Me.grpAcompanamientos.Text = "Acompañamientos"
            '
            ' chkPapasFritas
            '
            Me.chkPapasFritas.AutoSize = True
            Me.chkPapasFritas.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.chkPapasFritas.Location = New System.Drawing.Point(15, 24)
            Me.chkPapasFritas.Name = "chkPapasFritas"
            Me.chkPapasFritas.Size = New System.Drawing.Size(88, 19)
            Me.chkPapasFritas.TabIndex = 0
            Me.chkPapasFritas.Text = "Papas Fritas"
            Me.chkPapasFritas.UseVisualStyleBackColor = True
            Me.chkPapasFritas.UseMnemonic = False
            '
            ' chkEnsalada
            '
            Me.chkEnsalada.AutoSize = True
            Me.chkEnsalada.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.chkEnsalada.Location = New System.Drawing.Point(170, 24)
            Me.chkEnsalada.Name = "chkEnsalada"
            Me.chkEnsalada.Size = New System.Drawing.Size(102, 19)
            Me.chkEnsalada.TabIndex = 1
            Me.chkEnsalada.Text = "Ensalada Fresca"
            Me.chkEnsalada.UseVisualStyleBackColor = True
            Me.chkEnsalada.UseMnemonic = False
            '
            ' chkArroz
            '
            Me.chkArroz.AutoSize = True
            Me.chkArroz.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.chkArroz.Location = New System.Drawing.Point(15, 50)
            Me.chkArroz.Name = "chkArroz"
            Me.chkArroz.Size = New System.Drawing.Size(115, 19)
            Me.chkArroz.TabIndex = 2
            Me.chkArroz.Text = "Arroz con Choclo"
            Me.chkArroz.UseVisualStyleBackColor = True
            Me.chkArroz.UseMnemonic = False
            '
            ' chkSalsas
            '
            Me.chkSalsas.AutoSize = True
            Me.chkSalsas.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.chkSalsas.Location = New System.Drawing.Point(170, 50)
            Me.chkSalsas.Name = "chkSalsas"
            Me.chkSalsas.Size = New System.Drawing.Size(109, 19)
            Me.chkSalsas.TabIndex = 3
            Me.chkSalsas.Text = "Salsas de la Casa"
            Me.chkSalsas.UseVisualStyleBackColor = True
            Me.chkSalsas.UseMnemonic = False
            '
            ' grpTipoServicio
            '
            Me.grpTipoServicio.Controls.Add(Me.rbDelivery)
            Me.grpTipoServicio.Controls.Add(Me.rbParaLlevar)
            Me.grpTipoServicio.Controls.Add(Me.rbEnMesa)
            Me.grpTipoServicio.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpTipoServicio.Location = New System.Drawing.Point(15, 275)
            Me.grpTipoServicio.Name = "grpTipoServicio"
            Me.grpTipoServicio.Size = New System.Drawing.Size(335, 65)
            Me.grpTipoServicio.TabIndex = 7
            Me.grpTipoServicio.TabStop = False
            Me.grpTipoServicio.Text = "Tipo de Servicio"
            '
            ' rbEnMesa
            '
            Me.rbEnMesa.AutoSize = True
            Me.rbEnMesa.Checked = True
            Me.rbEnMesa.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.rbEnMesa.Location = New System.Drawing.Point(15, 25)
            Me.rbEnMesa.Name = "rbEnMesa"
            Me.rbEnMesa.Size = New System.Drawing.Size(69, 19)
            Me.rbEnMesa.TabIndex = 0
            Me.rbEnMesa.TabStop = True
            Me.rbEnMesa.Text = "En Mesa"
            Me.rbEnMesa.UseVisualStyleBackColor = True
            Me.rbEnMesa.UseMnemonic = False
            '
            ' rbParaLlevar
            '
            Me.rbParaLlevar.AutoSize = True
            Me.rbParaLlevar.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.rbParaLlevar.Location = New System.Drawing.Point(115, 25)
            Me.rbParaLlevar.Name = "rbParaLlevar"
            Me.rbParaLlevar.Size = New System.Drawing.Size(81, 19)
            Me.rbParaLlevar.TabIndex = 1
            Me.rbParaLlevar.Text = "Para Llevar"
            Me.rbParaLlevar.UseVisualStyleBackColor = True
            Me.rbParaLlevar.UseMnemonic = False
            '
            ' rbDelivery
            '
            Me.rbDelivery.AutoSize = True
            Me.rbDelivery.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.rbDelivery.Location = New System.Drawing.Point(220, 25)
            Me.rbDelivery.Name = "rbDelivery"
            Me.rbDelivery.Size = New System.Drawing.Size(67, 19)
            Me.rbDelivery.TabIndex = 2
            Me.rbDelivery.Text = "Delivery"
            Me.rbDelivery.UseVisualStyleBackColor = True
            Me.rbDelivery.UseMnemonic = False
            '
            ' pnlBotonesCrud
            '
            Me.pnlBotonesCrud.Controls.Add(Me.btnGuardar)
            Me.pnlBotonesCrud.Controls.Add(Me.btnActualizar)
            Me.pnlBotonesCrud.Controls.Add(Me.btnEliminar)
            Me.pnlBotonesCrud.Controls.Add(Me.btnBuscar)
            Me.pnlBotonesCrud.Controls.Add(Me.btnMostrarTodo)
            Me.pnlBotonesCrud.Location = New System.Drawing.Point(15, 350)
            Me.pnlBotonesCrud.Name = "pnlBotonesCrud"
            Me.pnlBotonesCrud.Size = New System.Drawing.Size(335, 220)
            Me.pnlBotonesCrud.TabIndex = 8
            '
            ' btnGuardar
            '
            Me.btnGuardar.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnGuardar.Location = New System.Drawing.Point(0, 0)
            Me.btnGuardar.Margin = New System.Windows.Forms.Padding(0, 0, 5, 8)
            Me.btnGuardar.Name = "btnGuardar"
            Me.btnGuardar.Size = New System.Drawing.Size(160, 38)
            Me.btnGuardar.TabIndex = 0
            Me.btnGuardar.Text = "💾 Guardar Pedido"
            Me.btnGuardar.UseVisualStyleBackColor = True
            Me.btnGuardar.UseMnemonic = False
            '
            ' btnActualizar
            '
            Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnActualizar.Location = New System.Drawing.Point(165, 0)
            Me.btnActualizar.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
            Me.btnActualizar.Name = "btnActualizar"
            Me.btnActualizar.Size = New System.Drawing.Size(160, 38)
            Me.btnActualizar.TabIndex = 1
            Me.btnActualizar.Text = "✏️ Actualizar Pedido"
            Me.btnActualizar.UseVisualStyleBackColor = True
            Me.btnActualizar.UseMnemonic = False
            '
            ' btnEliminar
            '
            Me.btnEliminar.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnEliminar.Location = New System.Drawing.Point(0, 46)
            Me.btnEliminar.Margin = New System.Windows.Forms.Padding(0, 0, 5, 8)
            Me.btnEliminar.Name = "btnEliminar"
            Me.btnEliminar.Size = New System.Drawing.Size(160, 38)
            Me.btnEliminar.TabIndex = 2
            Me.btnEliminar.Text = "🗑️ Eliminar Pedido"
            Me.btnEliminar.UseVisualStyleBackColor = True
            Me.btnEliminar.UseMnemonic = False
            '
            ' btnBuscar
            '
            Me.btnBuscar.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnBuscar.Location = New System.Drawing.Point(165, 46)
            Me.btnBuscar.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
            Me.btnBuscar.Name = "btnBuscar"
            Me.btnBuscar.Size = New System.Drawing.Size(160, 38)
            Me.btnBuscar.TabIndex = 3
            Me.btnBuscar.Text = "🔍 Buscar Pedido"
            Me.btnBuscar.UseVisualStyleBackColor = True
            Me.btnBuscar.UseMnemonic = False
            '
            ' btnMostrarTodo
            '
            Me.btnMostrarTodo.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnMostrarTodo.Location = New System.Drawing.Point(0, 92)
            Me.btnMostrarTodo.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
            Me.btnMostrarTodo.Name = "btnMostrarTodo"
            Me.btnMostrarTodo.Size = New System.Drawing.Size(325, 38)
            Me.btnMostrarTodo.TabIndex = 4
            Me.btnMostrarTodo.Text = "📋 Mostrar Todos los Pedidos"
            Me.btnMostrarTodo.UseVisualStyleBackColor = True
            Me.btnMostrarTodo.UseMnemonic = False
            '
            ' pnlGrillaContainer
            '
            Me.pnlGrillaContainer.Controls.Add(Me.grpListado)
            Me.pnlGrillaContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlGrillaContainer.Location = New System.Drawing.Point(380, 0)
            Me.pnlGrillaContainer.Margin = New System.Windows.Forms.Padding(0)
            Me.pnlGrillaContainer.Name = "pnlGrillaContainer"
            Me.pnlGrillaContainer.Size = New System.Drawing.Size(620, 585)
            Me.pnlGrillaContainer.TabIndex = 1
            '
            ' grpListado
            '
            Me.grpListado.Controls.Add(Me.dgvPedidos)
            Me.grpListado.Controls.Add(Me.pnlAccionesInferiores)
            Me.grpListado.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpListado.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpListado.Location = New System.Drawing.Point(0, 0)
            Me.grpListado.Name = "grpListado"
            Me.grpListado.Padding = New System.Windows.Forms.Padding(12)
            Me.grpListado.Size = New System.Drawing.Size(620, 585)
            Me.grpListado.TabIndex = 0
            Me.grpListado.TabStop = False
            Me.grpListado.Text = "Pedidos Registrados en RestauranteDB"
            '
            ' dgvPedidos
            '
            Me.dgvPedidos.AllowUserToAddRows = False
            Me.dgvPedidos.AllowUserToDeleteRows = False
            Me.dgvPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvPedidos.BackgroundColor = System.Drawing.Color.White
            Me.dgvPedidos.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPedidos.Location = New System.Drawing.Point(12, 30)
            Me.dgvPedidos.MultiSelect = False
            Me.dgvPedidos.Name = "dgvPedidos"
            Me.dgvPedidos.ReadOnly = True
            Me.dgvPedidos.RowHeadersVisible = False
            Me.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvPedidos.Size = New System.Drawing.Size(596, 493)
            Me.dgvPedidos.TabIndex = 0
            '
            ' pnlAccionesInferiores
            '
            Me.pnlAccionesInferiores.Controls.Add(Me.lblTotalRegistros)
            Me.pnlAccionesInferiores.Controls.Add(Me.btnVerConfirmado)
            Me.pnlAccionesInferiores.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlAccionesInferiores.Location = New System.Drawing.Point(12, 523)
            Me.pnlAccionesInferiores.Name = "pnlAccionesInferiores"
            Me.pnlAccionesInferiores.Size = New System.Drawing.Size(596, 50)
            Me.pnlAccionesInferiores.TabIndex = 1
            '
            ' btnVerConfirmado
            '
            Me.btnVerConfirmado.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnVerConfirmado.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnVerConfirmado.Location = New System.Drawing.Point(346, 8)
            Me.btnVerConfirmado.Name = "btnVerConfirmado"
            Me.btnVerConfirmado.Size = New System.Drawing.Size(250, 36)
            Me.btnVerConfirmado.TabIndex = 0
            Me.btnVerConfirmado.Text = "🖼️ Ver Pedido Confirmado (PictureBox)"
            Me.btnVerConfirmado.UseVisualStyleBackColor = True
            Me.btnVerConfirmado.UseMnemonic = False
            '
            ' lblTotalRegistros
            '
            Me.lblTotalRegistros.AutoSize = True
            Me.lblTotalRegistros.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Regular)
            Me.lblTotalRegistros.Location = New System.Drawing.Point(5, 18)
            Me.lblTotalRegistros.Name = "lblTotalRegistros"
            Me.lblTotalRegistros.Size = New System.Drawing.Size(140, 15)
            Me.lblTotalRegistros.TabIndex = 1
            Me.lblTotalRegistros.Text = "Total de Pedidos: 0"
            Me.lblTotalRegistros.UseMnemonic = False
            '
            ' FrmPedidos
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1040, 680)
            Me.Controls.Add(Me.tlpMainLayout)
            Me.Controls.Add(Me.pnlHeader)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmPedidos"
            Me.Padding = New System.Windows.Forms.Padding(20)
            Me.Text = "Gestión de Pedidos"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.tlpMainLayout.ResumeLayout(False)
            Me.pnlFormularioContainer.ResumeLayout(False)
            Me.grpFormulario.ResumeLayout(False)
            Me.grpFormulario.PerformLayout()
            Me.grpAcompanamientos.ResumeLayout(False)
            Me.grpAcompanamientos.PerformLayout()
            Me.grpTipoServicio.ResumeLayout(False)
            Me.grpTipoServicio.PerformLayout()
            Me.pnlBotonesCrud.ResumeLayout(False)
            Me.pnlGrillaContainer.ResumeLayout(False)
            Me.grpListado.ResumeLayout(False)
            CType(Me.dgvPedidos, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlAccionesInferiores.ResumeLayout(False)
            Me.pnlAccionesInferiores.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As Panel
        Friend WithEvents lblTituloHeader As Label
        Friend WithEvents lblSubtituloHeader As Label
        Friend WithEvents tlpMainLayout As TableLayoutPanel
        Friend WithEvents pnlFormularioContainer As Panel
        Friend WithEvents grpFormulario As GroupBox
        Friend WithEvents lblNombreCliente As Label
        Friend WithEvents txtNombreCliente As TextBox
        Friend WithEvents lblMesa As Label
        Friend WithEvents txtMesa As TextBox
        Friend WithEvents lblPlatoPrincipal As Label
        Friend WithEvents cboPlatoPrincipal As ComboBox
        Friend WithEvents grpAcompanamientos As GroupBox
        Friend WithEvents chkPapasFritas As CheckBox
        Friend WithEvents chkEnsalada As CheckBox
        Friend WithEvents chkArroz As CheckBox
        Friend WithEvents chkSalsas As CheckBox
        Friend WithEvents grpTipoServicio As GroupBox
        Friend WithEvents rbEnMesa As RadioButton
        Friend WithEvents rbParaLlevar As RadioButton
        Friend WithEvents rbDelivery As RadioButton
        Friend WithEvents pnlBotonesCrud As FlowLayoutPanel
        Friend WithEvents btnGuardar As Button
        Friend WithEvents btnActualizar As Button
        Friend WithEvents btnEliminar As Button
        Friend WithEvents btnBuscar As Button
        Friend WithEvents btnMostrarTodo As Button
        Friend WithEvents pnlGrillaContainer As Panel
        Friend WithEvents grpListado As GroupBox
        Friend WithEvents dgvPedidos As DataGridView
        Friend WithEvents pnlAccionesInferiores As Panel
        Friend WithEvents btnVerConfirmado As Button
        Friend WithEvents lblTotalRegistros As Label
    End Class
End Namespace
