Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Facturacion
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmFacturacionPDF
        Inherits Sistema_de_gestion_de_pedidos_para_un_restaurante.Views.Common.FrmBaseForm

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
            Me.btnVolverCaja = New System.Windows.Forms.Button()
            Me.lblTituloHeader = New System.Windows.Forms.Label()
            Me.lblSubtituloHeader = New System.Windows.Forms.Label()
            Me.pnlContenedor = New System.Windows.Forms.Panel()
            Me.pnlColumnaIzquierda = New System.Windows.Forms.Panel()
            Me.grpSeleccion = New System.Windows.Forms.GroupBox()
            Me.dgvPedidosFacturar = New System.Windows.Forms.DataGridView()
            Me.pnlFiltro = New System.Windows.Forms.Panel()
            Me.btnRefrescar = New System.Windows.Forms.Button()
            Me.btnBuscar = New System.Windows.Forms.Button()
            Me.txtBuscar = New System.Windows.Forms.TextBox()
            Me.lblBuscar = New System.Windows.Forms.Label()
            Me.grpDatosFiscales = New System.Windows.Forms.GroupBox()
            Me.lblRuc = New System.Windows.Forms.Label()
            Me.txtRucCedula = New System.Windows.Forms.TextBox()
            Me.lblRazonSocial = New System.Windows.Forms.Label()
            Me.txtRazonSocial = New System.Windows.Forms.TextBox()
            Me.lblTelefono = New System.Windows.Forms.Label()
            Me.txtTelefono = New System.Windows.Forms.TextBox()
            Me.lblCorreo = New System.Windows.Forms.Label()
            Me.txtCorreo = New System.Windows.Forms.TextBox()
            Me.lblDireccion = New System.Windows.Forms.Label()
            Me.txtDireccion = New System.Windows.Forms.TextBox()
            Me.pnlAcciones = New System.Windows.Forms.TableLayoutPanel()
            Me.btnImprimir = New System.Windows.Forms.Button()
            Me.btnGuardarComo = New System.Windows.Forms.Button()
            Me.btnEnviarCorreo = New System.Windows.Forms.Button()
            Me.btnLimpiar = New System.Windows.Forms.Button()
            Me.grpVistaPrevia = New System.Windows.Forms.GroupBox()
            Me.pnlComprobanteVisual = New System.Windows.Forms.Panel()
            Me.lblTicketRestaurante = New System.Windows.Forms.Label()
            Me.lblTicketSubtitulo = New System.Windows.Forms.Label()
            Me.lblTicketRucEmpresa = New System.Windows.Forms.Label()
            Me.pnlTicketSep1 = New System.Windows.Forms.Panel()
            Me.lblTicketCorrelativo = New System.Windows.Forms.Label()
            Me.lblTicketFecha = New System.Windows.Forms.Label()
            Me.lblTicketMetodo = New System.Windows.Forms.Label()
            Me.pnlTicketSep2 = New System.Windows.Forms.Panel()
            Me.lblTicketCliente = New System.Windows.Forms.Label()
            Me.lblTicketRucCliente = New System.Windows.Forms.Label()
            Me.lblTicketServicio = New System.Windows.Forms.Label()
            Me.pnlTicketSep3 = New System.Windows.Forms.Panel()
            Me.lblTicketPlato = New System.Windows.Forms.Label()
            Me.lblTicketAcomp = New System.Windows.Forms.Label()
            Me.lblTicketPrecioPlato = New System.Windows.Forms.Label()
            Me.pnlTicketSep4 = New System.Windows.Forms.Panel()
            Me.lblTicketSubtotal = New System.Windows.Forms.Label()
            Me.lblTicketImpuesto = New System.Windows.Forms.Label()
            Me.lblTicketTotal = New System.Windows.Forms.Label()
            Me.pnlTicketSep5 = New System.Windows.Forms.Panel()
            Me.lblTicketEstadoFiscal = New System.Windows.Forms.Label()
            Me.lblTicketPie = New System.Windows.Forms.Label()
            Me.pnlHeader.SuspendLayout()
            Me.pnlContenedor.SuspendLayout()
            Me.pnlColumnaIzquierda.SuspendLayout()
            Me.grpSeleccion.SuspendLayout()
            CType(Me.dgvPedidosFacturar, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlFiltro.SuspendLayout()
            Me.grpDatosFiscales.SuspendLayout()
            Me.pnlAcciones.SuspendLayout()
            Me.grpVistaPrevia.SuspendLayout()
            Me.pnlComprobanteVisual.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlHeader
            '
            Me.pnlHeader.Controls.Add(Me.btnVolverCaja)
            Me.pnlHeader.Controls.Add(Me.lblSubtituloHeader)
            Me.pnlHeader.Controls.Add(Me.lblTituloHeader)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(20, 20)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(960, 65)
            Me.pnlHeader.TabIndex = 0
            '
            ' btnVolverCaja
            '
            Me.btnVolverCaja.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnVolverCaja.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnVolverCaja.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnVolverCaja.Location = New System.Drawing.Point(800, 12)
            Me.btnVolverCaja.Name = "btnVolverCaja"
            Me.btnVolverCaja.Size = New System.Drawing.Size(155, 38)
            Me.btnVolverCaja.TabIndex = 2
            Me.btnVolverCaja.Text = "← Volver a Caja"
            Me.btnVolverCaja.UseVisualStyleBackColor = True
            '
            ' lblTituloHeader
            '
            Me.lblTituloHeader.AutoSize = True
            Me.lblTituloHeader.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloHeader.Location = New System.Drawing.Point(0, 0)
            Me.lblTituloHeader.Name = "lblTituloHeader"
            Me.lblTituloHeader.Size = New System.Drawing.Size(460, 30)
            Me.lblTituloHeader.TabIndex = 0
            Me.lblTituloHeader.Text = "Historial de Facturas"
            '
            ' lblSubtituloHeader
            '
            Me.lblSubtituloHeader.AutoSize = True
            Me.lblSubtituloHeader.Font = New System.Drawing.Font("Segoe UI", 10.5F)
            Me.lblSubtituloHeader.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloHeader.Location = New System.Drawing.Point(3, 34)
            Me.lblSubtituloHeader.Name = "lblSubtituloHeader"
            Me.lblSubtituloHeader.Size = New System.Drawing.Size(710, 20)
            Me.lblSubtituloHeader.TabIndex = 1
            Me.lblSubtituloHeader.Text = "Consulte pedidos cobrados, reimprima recibos o descargue copias en PDF."
            '
            ' pnlContenedor
            '
            Me.pnlContenedor.Controls.Add(Me.grpVistaPrevia)
            Me.pnlContenedor.Controls.Add(Me.pnlColumnaIzquierda)
            Me.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenedor.Location = New System.Drawing.Point(20, 85)
            Me.pnlContenedor.Name = "pnlContenedor"
            Me.pnlContenedor.Size = New System.Drawing.Size(960, 560)
            Me.pnlContenedor.TabIndex = 1
            '
            ' pnlColumnaIzquierda
            '
            Me.pnlColumnaIzquierda.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlColumnaIzquierda.Controls.Add(Me.pnlAcciones)
            Me.pnlColumnaIzquierda.Controls.Add(Me.grpDatosFiscales)
            Me.pnlColumnaIzquierda.Controls.Add(Me.grpSeleccion)
            Me.pnlColumnaIzquierda.Location = New System.Drawing.Point(0, 5)
            Me.pnlColumnaIzquierda.Name = "pnlColumnaIzquierda"
            Me.pnlColumnaIzquierda.Size = New System.Drawing.Size(540, 550)
            Me.pnlColumnaIzquierda.TabIndex = 0
            '
            ' grpSeleccion
            '
            Me.grpSeleccion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpSeleccion.Controls.Add(Me.dgvPedidosFacturar)
            Me.grpSeleccion.Controls.Add(Me.pnlFiltro)
            Me.grpSeleccion.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.grpSeleccion.Location = New System.Drawing.Point(0, 0)
            Me.grpSeleccion.Name = "grpSeleccion"
            Me.grpSeleccion.Padding = New System.Windows.Forms.Padding(10)
            Me.grpSeleccion.Size = New System.Drawing.Size(540, 240)
            Me.grpSeleccion.TabIndex = 0
            Me.grpSeleccion.TabStop = False
            Me.grpSeleccion.Text = "1. Seleccione un pedido cobrado"
            '
            ' pnlFiltro
            '
            Me.pnlFiltro.Controls.Add(Me.btnRefrescar)
            Me.pnlFiltro.Controls.Add(Me.btnBuscar)
            Me.pnlFiltro.Controls.Add(Me.txtBuscar)
            Me.pnlFiltro.Controls.Add(Me.lblBuscar)
            Me.pnlFiltro.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltro.Location = New System.Drawing.Point(10, 26)
            Me.pnlFiltro.Name = "pnlFiltro"
            Me.pnlFiltro.Size = New System.Drawing.Size(520, 42)
            Me.pnlFiltro.TabIndex = 0
            '
            ' lblBuscar
            '
            Me.lblBuscar.AutoSize = True
            Me.lblBuscar.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblBuscar.Location = New System.Drawing.Point(3, 10)
            Me.lblBuscar.Name = "lblBuscar"
            Me.lblBuscar.Size = New System.Drawing.Size(105, 20)
            Me.lblBuscar.TabIndex = 0
            Me.lblBuscar.Text = "Buscar pedido:"
            '
            ' txtBuscar
            '
            Me.txtBuscar.Font = New System.Drawing.Font("Segoe UI", 12.0F)
            Me.txtBuscar.Location = New System.Drawing.Point(114, 6)
            Me.txtBuscar.Name = "txtBuscar"
            Me.txtBuscar.Size = New System.Drawing.Size(180, 29)
            Me.txtBuscar.TabIndex = 1
            '
            ' btnBuscar
            '
            Me.btnBuscar.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnBuscar.Location = New System.Drawing.Point(300, 5)
            Me.btnBuscar.Name = "btnBuscar"
            Me.btnBuscar.Size = New System.Drawing.Size(95, 32)
            Me.btnBuscar.TabIndex = 2
            Me.btnBuscar.Text = "Buscar"
            Me.btnBuscar.UseVisualStyleBackColor = True
            '
            ' btnRefrescar
            '
            Me.btnRefrescar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRefrescar.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnRefrescar.Location = New System.Drawing.Point(405, 5)
            Me.btnRefrescar.Name = "btnRefrescar"
            Me.btnRefrescar.Size = New System.Drawing.Size(110, 32)
            Me.btnRefrescar.TabIndex = 3
            Me.btnRefrescar.Text = "Actualizar"
            Me.btnRefrescar.UseVisualStyleBackColor = True
            '
            ' dgvPedidosFacturar
            '
            Me.dgvPedidosFacturar.AllowUserToAddRows = False
            Me.dgvPedidosFacturar.AllowUserToDeleteRows = False
            Me.dgvPedidosFacturar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None
            Me.dgvPedidosFacturar.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.dgvPedidosFacturar.BackgroundColor = System.Drawing.Color.White
            Me.dgvPedidosFacturar.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgvPedidosFacturar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvPedidosFacturar.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPedidosFacturar.Location = New System.Drawing.Point(10, 68)
            Me.dgvPedidosFacturar.MultiSelect = False
            Me.dgvPedidosFacturar.Name = "dgvPedidosFacturar"
            Me.dgvPedidosFacturar.ReadOnly = True
            Me.dgvPedidosFacturar.RowHeadersVisible = False
            Me.dgvPedidosFacturar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvPedidosFacturar.Size = New System.Drawing.Size(520, 162)
            Me.dgvPedidosFacturar.TabIndex = 1
            '
            ' grpDatosFiscales
            '
            Me.grpDatosFiscales.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpDatosFiscales.Controls.Add(Me.txtDireccion)
            Me.grpDatosFiscales.Controls.Add(Me.lblDireccion)
            Me.grpDatosFiscales.Controls.Add(Me.txtCorreo)
            Me.grpDatosFiscales.Controls.Add(Me.lblCorreo)
            Me.grpDatosFiscales.Controls.Add(Me.txtTelefono)
            Me.grpDatosFiscales.Controls.Add(Me.lblTelefono)
            Me.grpDatosFiscales.Controls.Add(Me.txtRazonSocial)
            Me.grpDatosFiscales.Controls.Add(Me.lblRazonSocial)
            Me.grpDatosFiscales.Controls.Add(Me.txtRucCedula)
            Me.grpDatosFiscales.Controls.Add(Me.lblRuc)
            Me.grpDatosFiscales.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.grpDatosFiscales.Location = New System.Drawing.Point(0, 245)
            Me.grpDatosFiscales.Name = "grpDatosFiscales"
            Me.grpDatosFiscales.Padding = New System.Windows.Forms.Padding(12)
            Me.grpDatosFiscales.Size = New System.Drawing.Size(540, 220)
            Me.grpDatosFiscales.TabIndex = 1
            Me.grpDatosFiscales.TabStop = False
            Me.grpDatosFiscales.Text = "2. Datos del Cliente / Facturación (Opcional)"
            '
            ' lblRuc
            '
            Me.lblRuc.AutoSize = True
            Me.lblRuc.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblRuc.Location = New System.Drawing.Point(12, 30)
            Me.lblRuc.Name = "lblRuc"
            Me.lblRuc.Size = New System.Drawing.Size(100, 19)
            Me.lblRuc.TabIndex = 0
            Me.lblRuc.Text = "Cédula o RUC:"
            '
            ' txtRucCedula
            '
            Me.txtRucCedula.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.txtRucCedula.Location = New System.Drawing.Point(145, 26)
            Me.txtRucCedula.Name = "txtRucCedula"
            Me.txtRucCedula.Size = New System.Drawing.Size(180, 27)
            Me.txtRucCedula.TabIndex = 1
            '
            ' lblRazonSocial
            '
            Me.lblRazonSocial.AutoSize = True
            Me.lblRazonSocial.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblRazonSocial.Location = New System.Drawing.Point(12, 65)
            Me.lblRazonSocial.Name = "lblRazonSocial"
            Me.lblRazonSocial.Size = New System.Drawing.Size(130, 19)
            Me.lblRazonSocial.TabIndex = 2
            Me.lblRazonSocial.Text = "Nombre del cliente:"
            '
            ' txtRazonSocial
            '
            Me.txtRazonSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtRazonSocial.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.txtRazonSocial.Location = New System.Drawing.Point(145, 61)
            Me.txtRazonSocial.Name = "txtRazonSocial"
            Me.txtRazonSocial.Size = New System.Drawing.Size(380, 27)
            Me.txtRazonSocial.TabIndex = 3
            '
            ' lblTelefono
            '
            Me.lblTelefono.AutoSize = True
            Me.lblTelefono.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblTelefono.Location = New System.Drawing.Point(12, 100)
            Me.lblTelefono.Name = "lblTelefono"
            Me.lblTelefono.Size = New System.Drawing.Size(63, 19)
            Me.lblTelefono.TabIndex = 4
            Me.lblTelefono.Text = "Teléfono:"
            '
            ' txtTelefono
            '
            Me.txtTelefono.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.txtTelefono.Location = New System.Drawing.Point(145, 96)
            Me.txtTelefono.Name = "txtTelefono"
            Me.txtTelefono.Size = New System.Drawing.Size(180, 27)
            Me.txtTelefono.TabIndex = 5
            '
            ' lblCorreo
            '
            Me.lblCorreo.AutoSize = True
            Me.lblCorreo.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblCorreo.Location = New System.Drawing.Point(12, 135)
            Me.lblCorreo.Name = "lblCorreo"
            Me.lblCorreo.Size = New System.Drawing.Size(124, 19)
            Me.lblCorreo.TabIndex = 6
            Me.lblCorreo.Text = "Correo electrónico:"
            '
            ' txtCorreo
            '
            Me.txtCorreo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtCorreo.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.txtCorreo.Location = New System.Drawing.Point(145, 131)
            Me.txtCorreo.Name = "txtCorreo"
            Me.txtCorreo.Size = New System.Drawing.Size(380, 27)
            Me.txtCorreo.TabIndex = 7
            '
            ' lblDireccion
            '
            Me.lblDireccion.AutoSize = True
            Me.lblDireccion.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblDireccion.Location = New System.Drawing.Point(12, 170)
            Me.lblDireccion.Name = "lblDireccion"
            Me.lblDireccion.Size = New System.Drawing.Size(68, 19)
            Me.lblDireccion.TabIndex = 8
            Me.lblDireccion.Text = "Dirección:"
            '
            ' txtDireccion
            Me.txtDireccion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtDireccion.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.txtDireccion.Location = New System.Drawing.Point(145, 166)
            Me.txtDireccion.Name = "txtDireccion"
            Me.txtDireccion.Size = New System.Drawing.Size(380, 27)
            Me.txtDireccion.TabIndex = 9
            '
            ' pnlAcciones
            '
            Me.pnlAcciones.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlAcciones.ColumnCount = 2
            Me.pnlAcciones.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
            Me.pnlAcciones.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
            Me.pnlAcciones.RowCount = 2
            Me.pnlAcciones.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
            Me.pnlAcciones.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
            Me.pnlAcciones.Controls.Add(Me.btnImprimir, 0, 0)
            Me.pnlAcciones.Controls.Add(Me.btnEnviarCorreo, 1, 0)
            Me.pnlAcciones.Controls.Add(Me.btnGuardarComo, 0, 1)
            Me.pnlAcciones.Controls.Add(Me.btnLimpiar, 1, 1)
            Me.pnlAcciones.Location = New System.Drawing.Point(0, 455)
            Me.pnlAcciones.Name = "pnlAcciones"
            Me.pnlAcciones.Size = New System.Drawing.Size(540, 92)
            Me.pnlAcciones.TabIndex = 2
            '
            ' btnImprimir
            '
            Me.btnImprimir.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.btnImprimir.Location = New System.Drawing.Point(3, 3)
            Me.btnImprimir.Margin = New System.Windows.Forms.Padding(3)
            Me.btnImprimir.Name = "btnImprimir"
            Me.btnImprimir.Size = New System.Drawing.Size(264, 40)
            Me.btnImprimir.TabIndex = 0
            Me.btnImprimir.Text = "Imprimir Recibo"
            Me.btnImprimir.UseVisualStyleBackColor = True
            '
            ' btnEnviarCorreo
            '
            Me.btnEnviarCorreo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnEnviarCorreo.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.btnEnviarCorreo.Location = New System.Drawing.Point(273, 3)
            Me.btnEnviarCorreo.Margin = New System.Windows.Forms.Padding(3)
            Me.btnEnviarCorreo.Name = "btnEnviarCorreo"
            Me.btnEnviarCorreo.Size = New System.Drawing.Size(264, 40)
            Me.btnEnviarCorreo.TabIndex = 1
            Me.btnEnviarCorreo.Text = "Enviar por Correo"
            Me.btnEnviarCorreo.UseVisualStyleBackColor = True
            '
            ' btnGuardarComo
            '
            Me.btnGuardarComo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnGuardarComo.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.btnGuardarComo.Location = New System.Drawing.Point(3, 49)
            Me.btnGuardarComo.Margin = New System.Windows.Forms.Padding(3)
            Me.btnGuardarComo.Name = "btnGuardarComo"
            Me.btnGuardarComo.Size = New System.Drawing.Size(264, 40)
            Me.btnGuardarComo.TabIndex = 2
            Me.btnGuardarComo.Text = "Guardar en PDF"
            Me.btnGuardarComo.UseVisualStyleBackColor = True
            '
            ' btnLimpiar
            '
            Me.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnLimpiar.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnLimpiar.Location = New System.Drawing.Point(273, 49)
            Me.btnLimpiar.Margin = New System.Windows.Forms.Padding(3)
            Me.btnLimpiar.Name = "btnLimpiar"
            Me.btnLimpiar.Size = New System.Drawing.Size(264, 40)
            Me.btnLimpiar.TabIndex = 3
            Me.btnLimpiar.Text = "Limpiar Selección"
            Me.btnLimpiar.UseVisualStyleBackColor = True
            '
            ' grpVistaPrevia
            '
            Me.grpVistaPrevia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpVistaPrevia.Controls.Add(Me.pnlComprobanteVisual)
            Me.grpVistaPrevia.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpVistaPrevia.Location = New System.Drawing.Point(555, 5)
            Me.grpVistaPrevia.Name = "grpVistaPrevia"
            Me.grpVistaPrevia.Padding = New System.Windows.Forms.Padding(12)
            Me.grpVistaPrevia.Size = New System.Drawing.Size(405, 550)
            Me.grpVistaPrevia.TabIndex = 1
            Me.grpVistaPrevia.TabStop = False
            Me.grpVistaPrevia.Text = "Vista previa del recibo / factura"
            '
            ' pnlComprobanteVisual
            '
            Me.pnlComprobanteVisual.AutoScroll = True
            Me.pnlComprobanteVisual.BackColor = System.Drawing.Color.White
            Me.pnlComprobanteVisual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketPie)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketEstadoFiscal)
            Me.pnlComprobanteVisual.Controls.Add(Me.pnlTicketSep5)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketTotal)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketImpuesto)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketSubtotal)
            Me.pnlComprobanteVisual.Controls.Add(Me.pnlTicketSep4)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketPrecioPlato)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketAcomp)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketPlato)
            Me.pnlComprobanteVisual.Controls.Add(Me.pnlTicketSep3)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketServicio)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketRucCliente)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketCliente)
            Me.pnlComprobanteVisual.Controls.Add(Me.pnlTicketSep2)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketMetodo)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketFecha)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketCorrelativo)
            Me.pnlComprobanteVisual.Controls.Add(Me.pnlTicketSep1)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketRucEmpresa)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketSubtitulo)
            Me.pnlComprobanteVisual.Controls.Add(Me.lblTicketRestaurante)
            Me.pnlComprobanteVisual.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlComprobanteVisual.Location = New System.Drawing.Point(12, 30)
            Me.pnlComprobanteVisual.Name = "pnlComprobanteVisual"
            Me.pnlComprobanteVisual.Padding = New System.Windows.Forms.Padding(15)
            Me.pnlComprobanteVisual.Size = New System.Drawing.Size(381, 508)
            Me.pnlComprobanteVisual.TabIndex = 0
            '
            ' lblTicketRestaurante
            '
            Me.lblTicketRestaurante.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketRestaurante.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblTicketRestaurante.Location = New System.Drawing.Point(15, 15)
            Me.lblTicketRestaurante.Name = "lblTicketRestaurante"
            Me.lblTicketRestaurante.Size = New System.Drawing.Size(349, 24)
            Me.lblTicketRestaurante.TabIndex = 0
            Me.lblTicketRestaurante.Text = "RESTAURANTE EL BUEN SAZÓN"
            Me.lblTicketRestaurante.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTicketSubtitulo
            '
            Me.lblTicketSubtitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketSubtitulo.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketSubtitulo.ForeColor = System.Drawing.Color.Gray
            Me.lblTicketSubtitulo.Location = New System.Drawing.Point(15, 39)
            Me.lblTicketSubtitulo.Name = "lblTicketSubtitulo"
            Me.lblTicketSubtitulo.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketSubtitulo.TabIndex = 1
            Me.lblTicketSubtitulo.Text = "Sabor Tradicional & Excelencia Gastronómica"
            Me.lblTicketSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTicketRucEmpresa
            '
            Me.lblTicketRucEmpresa.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketRucEmpresa.Font = New System.Drawing.Font("Segoe UI", 7.5F)
            Me.lblTicketRucEmpresa.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketRucEmpresa.Location = New System.Drawing.Point(15, 55)
            Me.lblTicketRucEmpresa.Name = "lblTicketRucEmpresa"
            Me.lblTicketRucEmpresa.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketRucEmpresa.TabIndex = 2
            Me.lblTicketRucEmpresa.Text = "RUC: 155698421-2-2024 DV 89  •  Tel: 223-9000  •  Panamá"
            Me.lblTicketRucEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' pnlTicketSep1
            '
            Me.pnlTicketSep1.BackColor = System.Drawing.Color.LightGray
            Me.pnlTicketSep1.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTicketSep1.Location = New System.Drawing.Point(15, 71)
            Me.pnlTicketSep1.Name = "pnlTicketSep1"
            Me.pnlTicketSep1.Size = New System.Drawing.Size(349, 1)
            Me.pnlTicketSep1.TabIndex = 3
            '
            ' lblTicketCorrelativo
            '
            Me.lblTicketCorrelativo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketCorrelativo.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblTicketCorrelativo.Location = New System.Drawing.Point(15, 72)
            Me.lblTicketCorrelativo.Name = "lblTicketCorrelativo"
            Me.lblTicketCorrelativo.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblTicketCorrelativo.Size = New System.Drawing.Size(349, 25)
            Me.lblTicketCorrelativo.TabIndex = 4
            Me.lblTicketCorrelativo.Text = "FACTURA: FAC-2026-0000"
            '
            ' lblTicketFecha
            '
            Me.lblTicketFecha.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketFecha.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketFecha.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketFecha.Location = New System.Drawing.Point(15, 97)
            Me.lblTicketFecha.Name = "lblTicketFecha"
            Me.lblTicketFecha.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketFecha.TabIndex = 5
            Me.lblTicketFecha.Text = "Fecha de Emisión: --/--/----"
            '
            ' lblTicketMetodo
            '
            Me.lblTicketMetodo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketMetodo.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketMetodo.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketMetodo.Location = New System.Drawing.Point(15, 113)
            Me.lblTicketMetodo.Name = "lblTicketMetodo"
            Me.lblTicketMetodo.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketMetodo.TabIndex = 6
            Me.lblTicketMetodo.Text = "Método de Pago: --"
            '
            ' pnlTicketSep2
            '
            Me.pnlTicketSep2.BackColor = System.Drawing.Color.LightGray
            Me.pnlTicketSep2.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTicketSep2.Location = New System.Drawing.Point(15, 129)
            Me.pnlTicketSep2.Name = "pnlTicketSep2"
            Me.pnlTicketSep2.Size = New System.Drawing.Size(349, 1)
            Me.pnlTicketSep2.TabIndex = 7
            '
            ' lblTicketCliente
            '
            Me.lblTicketCliente.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketCliente.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblTicketCliente.Location = New System.Drawing.Point(15, 130)
            Me.lblTicketCliente.Name = "lblTicketCliente"
            Me.lblTicketCliente.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
            Me.lblTicketCliente.Size = New System.Drawing.Size(349, 20)
            Me.lblTicketCliente.TabIndex = 8
            Me.lblTicketCliente.Text = "Cliente: Consumidor Final"
            '
            ' lblTicketRucCliente
            '
            Me.lblTicketRucCliente.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketRucCliente.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketRucCliente.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketRucCliente.Location = New System.Drawing.Point(15, 150)
            Me.lblTicketRucCliente.Name = "lblTicketRucCliente"
            Me.lblTicketRucCliente.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketRucCliente.TabIndex = 9
            Me.lblTicketRucCliente.Text = "RUC / Cédula: --"
            '
            ' lblTicketServicio
            '
            Me.lblTicketServicio.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketServicio.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketServicio.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketServicio.Location = New System.Drawing.Point(15, 166)
            Me.lblTicketServicio.Name = "lblTicketServicio"
            Me.lblTicketServicio.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketServicio.TabIndex = 10
            Me.lblTicketServicio.Text = "Servicio: En Mesa • Mesa 00"
            '
            ' pnlTicketSep3
            '
            Me.pnlTicketSep3.BackColor = System.Drawing.Color.LightGray
            Me.pnlTicketSep3.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTicketSep3.Location = New System.Drawing.Point(15, 182)
            Me.pnlTicketSep3.Name = "pnlTicketSep3"
            Me.pnlTicketSep3.Size = New System.Drawing.Size(349, 1)
            Me.pnlTicketSep3.TabIndex = 11
            '
            ' lblTicketPlato
            '
            Me.lblTicketPlato.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketPlato.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblTicketPlato.Location = New System.Drawing.Point(15, 183)
            Me.lblTicketPlato.Name = "lblTicketPlato"
            Me.lblTicketPlato.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
            Me.lblTicketPlato.Size = New System.Drawing.Size(349, 20)
            Me.lblTicketPlato.TabIndex = 12
            Me.lblTicketPlato.Text = "1 x (Plato del Menú)"
            '
            ' lblTicketAcomp
            '
            Me.lblTicketAcomp.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketAcomp.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketAcomp.ForeColor = System.Drawing.Color.Gray
            Me.lblTicketAcomp.Location = New System.Drawing.Point(15, 203)
            Me.lblTicketAcomp.Name = "lblTicketAcomp"
            Me.lblTicketAcomp.Size = New System.Drawing.Size(349, 16)
            Me.lblTicketAcomp.TabIndex = 13
            Me.lblTicketAcomp.Text = "+ Acompañamientos"
            '
            ' lblTicketPrecioPlato
            '
            Me.lblTicketPrecioPlato.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketPrecioPlato.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblTicketPrecioPlato.Location = New System.Drawing.Point(15, 219)
            Me.lblTicketPrecioPlato.Name = "lblTicketPrecioPlato"
            Me.lblTicketPrecioPlato.Size = New System.Drawing.Size(349, 18)
            Me.lblTicketPrecioPlato.TabIndex = 14
            Me.lblTicketPrecioPlato.Text = "Importe: $0.00"
            Me.lblTicketPrecioPlato.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' pnlTicketSep4
            '
            Me.pnlTicketSep4.BackColor = System.Drawing.Color.LightGray
            Me.pnlTicketSep4.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTicketSep4.Location = New System.Drawing.Point(15, 237)
            Me.pnlTicketSep4.Name = "pnlTicketSep4"
            Me.pnlTicketSep4.Size = New System.Drawing.Size(349, 1)
            Me.pnlTicketSep4.TabIndex = 15
            '
            ' lblTicketSubtotal
            '
            Me.lblTicketSubtotal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketSubtotal.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblTicketSubtotal.Location = New System.Drawing.Point(15, 238)
            Me.lblTicketSubtotal.Name = "lblTicketSubtotal"
            Me.lblTicketSubtotal.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
            Me.lblTicketSubtotal.Size = New System.Drawing.Size(349, 20)
            Me.lblTicketSubtotal.TabIndex = 16
            Me.lblTicketSubtotal.Text = "Subtotal: $0.00"
            Me.lblTicketSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblTicketImpuesto
            '
            Me.lblTicketImpuesto.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketImpuesto.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblTicketImpuesto.Location = New System.Drawing.Point(15, 258)
            Me.lblTicketImpuesto.Name = "lblTicketImpuesto"
            Me.lblTicketImpuesto.Size = New System.Drawing.Size(349, 18)
            Me.lblTicketImpuesto.TabIndex = 17
            Me.lblTicketImpuesto.Text = "Impuesto ITBMS (7%): $0.00"
            Me.lblTicketImpuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblTicketTotal
            '
            Me.lblTicketTotal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketTotal.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblTicketTotal.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblTicketTotal.Location = New System.Drawing.Point(15, 276)
            Me.lblTicketTotal.Name = "lblTicketTotal"
            Me.lblTicketTotal.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
            Me.lblTicketTotal.Size = New System.Drawing.Size(349, 28)
            Me.lblTicketTotal.TabIndex = 18
            Me.lblTicketTotal.Text = "TOTAL: $0.00"
            Me.lblTicketTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' pnlTicketSep5
            '
            Me.pnlTicketSep5.BackColor = System.Drawing.Color.LightGray
            Me.pnlTicketSep5.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTicketSep5.Location = New System.Drawing.Point(15, 304)
            Me.pnlTicketSep5.Name = "pnlTicketSep5"
            Me.pnlTicketSep5.Size = New System.Drawing.Size(349, 1)
            Me.pnlTicketSep5.TabIndex = 19
            '
            ' lblTicketEstadoFiscal
            '
            Me.lblTicketEstadoFiscal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketEstadoFiscal.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblTicketEstadoFiscal.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblTicketEstadoFiscal.Location = New System.Drawing.Point(15, 305)
            Me.lblTicketEstadoFiscal.Name = "lblTicketEstadoFiscal"
            Me.lblTicketEstadoFiscal.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblTicketEstadoFiscal.Size = New System.Drawing.Size(349, 24)
            Me.lblTicketEstadoFiscal.TabIndex = 20
            Me.lblTicketEstadoFiscal.Text = "🟢 PEDIDO COBRADO Y REGISTRADO"
            Me.lblTicketEstadoFiscal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTicketPie
            '
            Me.lblTicketPie.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketPie.Font = New System.Drawing.Font("Segoe UI", 7.5F)
            Me.lblTicketPie.ForeColor = System.Drawing.Color.Gray
            Me.lblTicketPie.Location = New System.Drawing.Point(15, 329)
            Me.lblTicketPie.Name = "lblTicketPie"
            Me.lblTicketPie.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
            Me.lblTicketPie.Size = New System.Drawing.Size(349, 30)
            Me.lblTicketPie.TabIndex = 21
            Me.lblTicketPie.Text = "¡Gracias por su visita y preferencia!" & vbCrLf & "Restaurante El Buen Sazón"
            Me.lblTicketPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' FrmFacturacionPDF
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(1000, 665)
            Me.Controls.Add(Me.pnlContenedor)
            Me.Controls.Add(Me.pnlHeader)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmFacturacionPDF"
            Me.Padding = New System.Windows.Forms.Padding(20)
            Me.Text = "Historial de Facturas"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlContenedor.ResumeLayout(False)
            Me.pnlColumnaIzquierda.ResumeLayout(False)
            Me.grpSeleccion.ResumeLayout(False)
            CType(Me.dgvPedidosFacturar, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlFiltro.ResumeLayout(False)
            Me.pnlFiltro.PerformLayout()
            Me.grpDatosFiscales.ResumeLayout(False)
            Me.grpDatosFiscales.PerformLayout()
            Me.pnlAcciones.ResumeLayout(False)
            Me.grpVistaPrevia.ResumeLayout(False)
            Me.pnlComprobanteVisual.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeader As Panel
        Friend WithEvents lblTituloHeader As Label
        Friend WithEvents lblSubtituloHeader As Label
        Friend WithEvents pnlContenedor As Panel
        Friend WithEvents pnlColumnaIzquierda As Panel
        Friend WithEvents grpSeleccion As GroupBox
        Friend WithEvents pnlFiltro As Panel
        Friend WithEvents lblBuscar As Label
        Friend WithEvents txtBuscar As TextBox
        Friend WithEvents btnBuscar As Button
        Friend WithEvents btnRefrescar As Button
        Friend WithEvents dgvPedidosFacturar As DataGridView
        Friend WithEvents grpDatosFiscales As GroupBox
        Friend WithEvents lblRuc As Label
        Friend WithEvents txtRucCedula As TextBox
        Friend WithEvents lblRazonSocial As Label
        Friend WithEvents txtRazonSocial As TextBox
        Friend WithEvents lblTelefono As Label
        Friend WithEvents txtTelefono As TextBox
        Friend WithEvents lblCorreo As Label
        Friend WithEvents txtCorreo As TextBox
        Friend WithEvents lblDireccion As Label
        Friend WithEvents txtDireccion As TextBox
        Friend WithEvents pnlAcciones As TableLayoutPanel
        Friend WithEvents btnImprimir As Button
        Friend WithEvents btnGuardarComo As Button
        Friend WithEvents btnEnviarCorreo As Button
        Friend WithEvents btnLimpiar As Button
        Friend WithEvents btnVolverCaja As Button
        Friend WithEvents grpVistaPrevia As GroupBox
        Friend WithEvents pnlComprobanteVisual As Panel
        Friend WithEvents lblTicketRestaurante As Label
        Friend WithEvents lblTicketSubtitulo As Label
        Friend WithEvents lblTicketRucEmpresa As Label
        Friend WithEvents pnlTicketSep1 As Panel
        Friend WithEvents lblTicketCorrelativo As Label
        Friend WithEvents lblTicketFecha As Label
        Friend WithEvents lblTicketMetodo As Label
        Friend WithEvents pnlTicketSep2 As Panel
        Friend WithEvents lblTicketCliente As Label
        Friend WithEvents lblTicketRucCliente As Label
        Friend WithEvents lblTicketServicio As Label
        Friend WithEvents pnlTicketSep3 As Panel
        Friend WithEvents lblTicketPlato As Label
        Friend WithEvents lblTicketAcomp As Label
        Friend WithEvents lblTicketPrecioPlato As Label
        Friend WithEvents pnlTicketSep4 As Panel
        Friend WithEvents lblTicketSubtotal As Label
        Friend WithEvents lblTicketImpuesto As Label
        Friend WithEvents lblTicketTotal As Label
        Friend WithEvents pnlTicketSep5 As Panel
        Friend WithEvents lblTicketEstadoFiscal As Label
        Friend WithEvents lblTicketPie As Label
    End Class
End Namespace
