Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Catalogo
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCatalogo
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

        ' Encabezado
        Friend WithEvents pnlHeaderContainer As System.Windows.Forms.Panel
        Friend WithEvents lblTituloCatalogo As System.Windows.Forms.Label
        Friend WithEvents lblSubtituloCatalogo As System.Windows.Forms.Label
        Friend WithEvents txtBuscarPlato As System.Windows.Forms.TextBox
        Friend WithEvents btnBuscarPlato As System.Windows.Forms.Button
        Friend WithEvents btnMostrarTodo As System.Windows.Forms.Button

        ' Layout Principal
        Friend WithEvents tlpMainLayout As System.Windows.Forms.TableLayoutPanel

        ' Grupo Formulario
        Friend WithEvents grpFormularioPlato As System.Windows.Forms.GroupBox
        Friend WithEvents lblNombrePlato As System.Windows.Forms.Label
        Friend WithEvents txtNombrePlato As System.Windows.Forms.TextBox
        Friend WithEvents lblCategoria As System.Windows.Forms.Label
        Friend WithEvents cboCategoria As System.Windows.Forms.ComboBox
        Friend WithEvents lblPrecio As System.Windows.Forms.Label
        Friend WithEvents txtPrecio As System.Windows.Forms.TextBox
        Friend WithEvents lblTiempoCoccion As System.Windows.Forms.Label
        Friend WithEvents txtTiempoCoccion As System.Windows.Forms.TextBox
        Friend WithEvents chkDisponible As System.Windows.Forms.CheckBox
        Friend WithEvents lblDescripcion As System.Windows.Forms.Label
        Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox

        ' Botones CRUD
        Friend WithEvents pnlBotonesAccion As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents btnGuardarPlato As System.Windows.Forms.Button
        Friend WithEvents btnActualizarPlato As System.Windows.Forms.Button
        Friend WithEvents btnEliminarPlato As System.Windows.Forms.Button
        Friend WithEvents btnLimpiarPlato As System.Windows.Forms.Button

        ' Grupo Grilla Listado
        Friend WithEvents grpListadoPlatos As System.Windows.Forms.GroupBox
        Friend WithEvents dgvCatalogo As System.Windows.Forms.DataGridView

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeaderContainer = New System.Windows.Forms.Panel()
            Me.lblTituloCatalogo = New System.Windows.Forms.Label()
            Me.lblSubtituloCatalogo = New System.Windows.Forms.Label()
            Me.txtBuscarPlato = New System.Windows.Forms.TextBox()
            Me.btnBuscarPlato = New System.Windows.Forms.Button()
            Me.btnMostrarTodo = New System.Windows.Forms.Button()

            Me.tlpMainLayout = New System.Windows.Forms.TableLayoutPanel()

            Me.grpFormularioPlato = New System.Windows.Forms.GroupBox()
            Me.lblNombrePlato = New System.Windows.Forms.Label()
            Me.txtNombrePlato = New System.Windows.Forms.TextBox()
            Me.lblCategoria = New System.Windows.Forms.Label()
            Me.cboCategoria = New System.Windows.Forms.ComboBox()
            Me.lblPrecio = New System.Windows.Forms.Label()
            Me.txtPrecio = New System.Windows.Forms.TextBox()
            Me.lblTiempoCoccion = New System.Windows.Forms.Label()
            Me.txtTiempoCoccion = New System.Windows.Forms.TextBox()
            Me.chkDisponible = New System.Windows.Forms.CheckBox()
            Me.lblDescripcion = New System.Windows.Forms.Label()
            Me.txtDescripcion = New System.Windows.Forms.TextBox()

            Me.pnlBotonesAccion = New System.Windows.Forms.FlowLayoutPanel()
            Me.btnGuardarPlato = New System.Windows.Forms.Button()
            Me.btnActualizarPlato = New System.Windows.Forms.Button()
            Me.btnEliminarPlato = New System.Windows.Forms.Button()
            Me.btnLimpiarPlato = New System.Windows.Forms.Button()

            Me.grpListadoPlatos = New System.Windows.Forms.GroupBox()
            Me.dgvCatalogo = New System.Windows.Forms.DataGridView()

            Me.pnlHeaderContainer.SuspendLayout()
            Me.tlpMainLayout.SuspendLayout()
            Me.grpFormularioPlato.SuspendLayout()
            Me.pnlBotonesAccion.SuspendLayout()
            Me.grpListadoPlatos.SuspendLayout()
            CType(Me.dgvCatalogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()

            ' 
            ' pnlHeaderContainer
            ' 
            Me.pnlHeaderContainer.Controls.Add(Me.btnMostrarTodo)
            Me.pnlHeaderContainer.Controls.Add(Me.btnBuscarPlato)
            Me.pnlHeaderContainer.Controls.Add(Me.txtBuscarPlato)
            Me.pnlHeaderContainer.Controls.Add(Me.lblSubtituloCatalogo)
            Me.pnlHeaderContainer.Controls.Add(Me.lblTituloCatalogo)
            Me.pnlHeaderContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderContainer.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeaderContainer.Name = "pnlHeaderContainer"
            Me.pnlHeaderContainer.Size = New System.Drawing.Size(980, 75)
            Me.pnlHeaderContainer.TabIndex = 0

            ' 
            ' lblTituloCatalogo
            ' 
            Me.lblTituloCatalogo.AutoSize = True
            Me.lblTituloCatalogo.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloCatalogo.Location = New System.Drawing.Point(20, 12)
            Me.lblTituloCatalogo.Name = "lblTituloCatalogo"
            Me.lblTituloCatalogo.Size = New System.Drawing.Size(350, 37)
            Me.lblTituloCatalogo.Text = "Catálogo de Productos y Menú"
            Me.lblTituloCatalogo.UseMnemonic = False

            ' 
            ' lblSubtituloCatalogo
            ' 
            Me.lblSubtituloCatalogo.AutoSize = True
            Me.lblSubtituloCatalogo.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloCatalogo.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloCatalogo.Location = New System.Drawing.Point(22, 48)
            Me.lblSubtituloCatalogo.Name = "lblSubtituloCatalogo"
            Me.lblSubtituloCatalogo.Size = New System.Drawing.Size(430, 20)
            Me.lblSubtituloCatalogo.Text = "Administración de platos, precios, categorías y disponibilidad (RF-012)"
            Me.lblSubtituloCatalogo.UseMnemonic = False

            ' 
            ' txtBuscarPlato
            ' 
            Me.txtBuscarPlato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtBuscarPlato.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtBuscarPlato.Location = New System.Drawing.Point(580, 24)
            Me.txtBuscarPlato.Name = "txtBuscarPlato"
            Me.txtBuscarPlato.PlaceholderText = "Buscar por plato..."
            Me.txtBuscarPlato.Size = New System.Drawing.Size(190, 29)
            Me.txtBuscarPlato.TabIndex = 1

            ' 
            ' btnBuscarPlato
            ' 
            Me.btnBuscarPlato.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnBuscarPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnBuscarPlato.Location = New System.Drawing.Point(778, 22)
            Me.btnBuscarPlato.Name = "btnBuscarPlato"
            Me.btnBuscarPlato.Size = New System.Drawing.Size(85, 33)
            Me.btnBuscarPlato.TabIndex = 2
            Me.btnBuscarPlato.Text = "Buscar"
            Me.btnBuscarPlato.UseVisualStyleBackColor = True

            ' 
            ' btnMostrarTodo
            ' 
            Me.btnMostrarTodo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnMostrarTodo.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.btnMostrarTodo.Location = New System.Drawing.Point(870, 22)
            Me.btnMostrarTodo.Name = "btnMostrarTodo"
            Me.btnMostrarTodo.Size = New System.Drawing.Size(90, 33)
            Me.btnMostrarTodo.TabIndex = 3
            Me.btnMostrarTodo.Text = "Todos"
            Me.btnMostrarTodo.UseVisualStyleBackColor = True

            ' 
            ' tlpMainLayout
            ' 
            Me.tlpMainLayout.ColumnCount = 2
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 390.0!))
            Me.tlpMainLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMainLayout.Controls.Add(Me.grpFormularioPlato, 0, 0)
            Me.tlpMainLayout.Controls.Add(Me.grpListadoPlatos, 1, 0)
            Me.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMainLayout.Location = New System.Drawing.Point(0, 75)
            Me.tlpMainLayout.Name = "tlpMainLayout"
            Me.tlpMainLayout.Padding = New System.Windows.Forms.Padding(15, 5, 15, 15)
            Me.tlpMainLayout.RowCount = 1
            Me.tlpMainLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMainLayout.Size = New System.Drawing.Size(980, 585)
            Me.tlpMainLayout.TabIndex = 1

            ' 
            ' grpFormularioPlato
            ' 
            Me.grpFormularioPlato.Controls.Add(Me.pnlBotonesAccion)
            Me.grpFormularioPlato.Controls.Add(Me.txtDescripcion)
            Me.grpFormularioPlato.Controls.Add(Me.lblDescripcion)
            Me.grpFormularioPlato.Controls.Add(Me.chkDisponible)
            Me.grpFormularioPlato.Controls.Add(Me.txtTiempoCoccion)
            Me.grpFormularioPlato.Controls.Add(Me.lblTiempoCoccion)
            Me.grpFormularioPlato.Controls.Add(Me.txtPrecio)
            Me.grpFormularioPlato.Controls.Add(Me.lblPrecio)
            Me.grpFormularioPlato.Controls.Add(Me.cboCategoria)
            Me.grpFormularioPlato.Controls.Add(Me.lblCategoria)
            Me.grpFormularioPlato.Controls.Add(Me.txtNombrePlato)
            Me.grpFormularioPlato.Controls.Add(Me.lblNombrePlato)
            Me.grpFormularioPlato.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpFormularioPlato.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.grpFormularioPlato.Location = New System.Drawing.Point(18, 8)
            Me.grpFormularioPlato.Name = "grpFormularioPlato"
            Me.grpFormularioPlato.Size = New System.Drawing.Size(384, 559)
            Me.grpFormularioPlato.TabIndex = 0
            Me.grpFormularioPlato.TabStop = False
            Me.grpFormularioPlato.Text = "Registro y Edición de Plato"

            ' 
            ' lblNombrePlato
            ' 
            Me.lblNombrePlato.AutoSize = True
            Me.lblNombrePlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblNombrePlato.Location = New System.Drawing.Point(15, 32)
            Me.lblNombrePlato.Text = "Nombre del Plato:"

            ' 
            ' txtNombrePlato
            ' 
            Me.txtNombrePlato.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtNombrePlato.Location = New System.Drawing.Point(15, 52)
            Me.txtNombrePlato.Name = "txtNombrePlato"
            Me.txtNombrePlato.Size = New System.Drawing.Size(350, 26)
            Me.txtNombrePlato.TabIndex = 0

            ' 
            ' lblCategoria
            ' 
            Me.lblCategoria.AutoSize = True
            Me.lblCategoria.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblCategoria.Location = New System.Drawing.Point(15, 104)
            Me.lblCategoria.Text = "Categoría del Menú:"

            ' 
            ' cboCategoria
            ' 
            Me.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCategoria.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.cboCategoria.FormattingEnabled = True
            Me.cboCategoria.Location = New System.Drawing.Point(15, 124)
            Me.cboCategoria.Name = "cboCategoria"
            Me.cboCategoria.Size = New System.Drawing.Size(350, 29)
            Me.cboCategoria.TabIndex = 1

            ' 
            ' lblPrecio
            ' 
            Me.lblPrecio.AutoSize = True
            Me.lblPrecio.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblPrecio.Location = New System.Drawing.Point(15, 178)
            Me.lblPrecio.Text = "Precio de Venta (RD$):"

            ' 
            ' txtPrecio
            ' 
            Me.txtPrecio.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtPrecio.Location = New System.Drawing.Point(15, 198)
            Me.txtPrecio.Name = "txtPrecio"
            Me.txtPrecio.Size = New System.Drawing.Size(165, 26)
            Me.txtPrecio.TabIndex = 2

            ' 
            ' lblTiempoCoccion
            ' 
            Me.lblTiempoCoccion.AutoSize = True
            Me.lblTiempoCoccion.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblTiempoCoccion.Location = New System.Drawing.Point(198, 178)
            Me.lblTiempoCoccion.Text = "Tiempo Estimado:"

            ' 
            ' txtTiempoCoccion
            ' 
            Me.txtTiempoCoccion.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtTiempoCoccion.Location = New System.Drawing.Point(198, 198)
            Me.txtTiempoCoccion.Name = "txtTiempoCoccion"
            Me.txtTiempoCoccion.Size = New System.Drawing.Size(167, 26)
            Me.txtTiempoCoccion.TabIndex = 3

            ' 
            ' chkDisponible
            ' 
            Me.chkDisponible.AutoSize = True
            Me.chkDisponible.Checked = True
            Me.chkDisponible.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkDisponible.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.chkDisponible.Location = New System.Drawing.Point(15, 248)
            Me.chkDisponible.Name = "chkDisponible"
            Me.chkDisponible.Size = New System.Drawing.Size(260, 24)
            Me.chkDisponible.TabIndex = 4
            Me.chkDisponible.Text = "Disponible para pedidos (RN-001)"
            Me.chkDisponible.UseVisualStyleBackColor = True

            ' 
            ' lblDescripcion
            ' 
            Me.lblDescripcion.AutoSize = True
            Me.lblDescripcion.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblDescripcion.Location = New System.Drawing.Point(15, 290)
            Me.lblDescripcion.Text = "Descripción / Insumos:"

            ' 
            ' txtDescripcion
            ' 
            Me.txtDescripcion.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtDescripcion.Location = New System.Drawing.Point(15, 310)
            Me.txtDescripcion.Multiline = True
            Me.txtDescripcion.Name = "txtDescripcion"
            Me.txtDescripcion.Size = New System.Drawing.Size(350, 65)
            Me.txtDescripcion.TabIndex = 5

            ' 
            ' pnlBotonesAccion
            ' 
            Me.pnlBotonesAccion.Controls.Add(Me.btnGuardarPlato)
            Me.pnlBotonesAccion.Controls.Add(Me.btnActualizarPlato)
            Me.pnlBotonesAccion.Controls.Add(Me.btnEliminarPlato)
            Me.pnlBotonesAccion.Controls.Add(Me.btnLimpiarPlato)
            Me.pnlBotonesAccion.Location = New System.Drawing.Point(15, 395)
            Me.pnlBotonesAccion.Name = "pnlBotonesAccion"
            Me.pnlBotonesAccion.Size = New System.Drawing.Size(350, 140)
            Me.pnlBotonesAccion.TabIndex = 6

            ' 
            ' btnGuardarPlato
            ' 
            Me.btnGuardarPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnGuardarPlato.Location = New System.Drawing.Point(0, 0)
            Me.btnGuardarPlato.Margin = New System.Windows.Forms.Padding(0, 0, 10, 10)
            Me.btnGuardarPlato.Name = "btnGuardarPlato"
            Me.btnGuardarPlato.Size = New System.Drawing.Size(165, 42)
            Me.btnGuardarPlato.TabIndex = 0
            Me.btnGuardarPlato.Text = "Guardar Plato"
            Me.btnGuardarPlato.UseVisualStyleBackColor = True

            ' 
            ' btnActualizarPlato
            ' 
            Me.btnActualizarPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnActualizarPlato.Location = New System.Drawing.Point(175, 0)
            Me.btnActualizarPlato.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
            Me.btnActualizarPlato.Name = "btnActualizarPlato"
            Me.btnActualizarPlato.Size = New System.Drawing.Size(165, 42)
            Me.btnActualizarPlato.TabIndex = 1
            Me.btnActualizarPlato.Text = "Actualizar"
            Me.btnActualizarPlato.UseVisualStyleBackColor = True

            ' 
            ' btnEliminarPlato
            ' 
            Me.btnEliminarPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnEliminarPlato.Location = New System.Drawing.Point(0, 52)
            Me.btnEliminarPlato.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
            Me.btnEliminarPlato.Name = "btnEliminarPlato"
            Me.btnEliminarPlato.Size = New System.Drawing.Size(165, 42)
            Me.btnEliminarPlato.TabIndex = 2
            Me.btnEliminarPlato.Text = "Eliminar"
            Me.btnEliminarPlato.UseVisualStyleBackColor = True

            ' 
            ' btnLimpiarPlato
            ' 
            Me.btnLimpiarPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.btnLimpiarPlato.Location = New System.Drawing.Point(175, 52)
            Me.btnLimpiarPlato.Margin = New System.Windows.Forms.Padding(0)
            Me.btnLimpiarPlato.Name = "btnLimpiarPlato"
            Me.btnLimpiarPlato.Size = New System.Drawing.Size(165, 42)
            Me.btnLimpiarPlato.TabIndex = 3
            Me.btnLimpiarPlato.Text = "Limpiar"
            Me.btnLimpiarPlato.UseVisualStyleBackColor = True

            ' 
            ' grpListadoPlatos
            ' 
            Me.grpListadoPlatos.Controls.Add(Me.dgvCatalogo)
            Me.grpListadoPlatos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpListadoPlatos.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.grpListadoPlatos.Location = New System.Drawing.Point(408, 8)
            Me.grpListadoPlatos.Name = "grpListadoPlatos"
            Me.grpListadoPlatos.Padding = New System.Windows.Forms.Padding(12)
            Me.grpListadoPlatos.Size = New System.Drawing.Size(554, 559)
            Me.grpListadoPlatos.TabIndex = 1
            Me.grpListadoPlatos.TabStop = False
            Me.grpListadoPlatos.Text = "Lista de Platos del Menú"

            ' 
            ' dgvCatalogo
            ' 
            Me.dgvCatalogo.AllowUserToAddRows = False
            Me.dgvCatalogo.AllowUserToDeleteRows = False
            Me.dgvCatalogo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvCatalogo.BackgroundColor = System.Drawing.Color.White
            Me.dgvCatalogo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvCatalogo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvCatalogo.Location = New System.Drawing.Point(12, 35)
            Me.dgvCatalogo.MultiSelect = False
            Me.dgvCatalogo.Name = "dgvCatalogo"
            Me.dgvCatalogo.ReadOnly = True
            Me.dgvCatalogo.RowHeadersVisible = False
            Me.dgvCatalogo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvCatalogo.Size = New System.Drawing.Size(530, 512)
            Me.dgvCatalogo.TabIndex = 0

            ' 
            ' FrmCatalogo
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0F, 20.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(980, 660)
            Me.Controls.Add(Me.tlpMainLayout)
            Me.Controls.Add(Me.pnlHeaderContainer)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmCatalogo"
            Me.Text = "Catálogo de Productos"

            Me.pnlHeaderContainer.ResumeLayout(False)
            Me.pnlHeaderContainer.PerformLayout()
            Me.tlpMainLayout.ResumeLayout(False)
            Me.grpFormularioPlato.ResumeLayout(False)
            Me.grpFormularioPlato.PerformLayout()
            Me.pnlBotonesAccion.ResumeLayout(False)
            Me.grpListadoPlatos.ResumeLayout(False)
            CType(Me.dgvCatalogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
    End Class
End Namespace
