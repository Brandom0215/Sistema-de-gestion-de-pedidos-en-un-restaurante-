Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Caja
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCajaCobros
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
            Me.lblTituloHeader = New System.Windows.Forms.Label()
            Me.lblSubtituloHeader = New System.Windows.Forms.Label()
            Me.pnlContenedor = New System.Windows.Forms.Panel()
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

            Me.grpListado = New System.Windows.Forms.GroupBox()
            Me.pnlFiltros = New System.Windows.Forms.Panel()
            Me.lblBuscar = New System.Windows.Forms.Label()
            Me.txtBuscar = New System.Windows.Forms.TextBox()
            Me.btnBuscar = New System.Windows.Forms.Button()
            Me.btnRefrescar = New System.Windows.Forms.Button()
            Me.dgvPedidosPendientes = New System.Windows.Forms.DataGridView()
            Me.lblTotalPendientes = New System.Windows.Forms.Label()

            Me.pnlHeader.SuspendLayout()
            Me.pnlContenedor.SuspendLayout()
            Me.grpDetalleCobro.SuspendLayout()
            Me.pnlCardCobro.SuspendLayout()
            Me.pnlMetodosPago.SuspendLayout()
            Me.pnlTotalDestacado.SuspendLayout()
            Me.pnlCalculoCaja.SuspendLayout()
            Me.grpListado.SuspendLayout()
            Me.pnlFiltros.SuspendLayout()
            CType(Me.dgvPedidosPendientes, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()

            ' pnlHeader (OCULTO PARA MANTENER TITULO UNIFICADO EN BARRA SUPERIOR)
            Me.pnlHeader.Controls.Add(Me.lblSubtituloHeader)
            Me.pnlHeader.Controls.Add(Me.lblTituloHeader)
            Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeader.Name = "pnlHeader"
            Me.pnlHeader.Size = New System.Drawing.Size(1200, 75)
            Me.pnlHeader.TabIndex = 0
            Me.pnlHeader.Visible = False

            ' lblTituloHeader
            Me.lblTituloHeader.AutoSize = True
            Me.lblTituloHeader.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloHeader.Location = New System.Drawing.Point(20, 12)
            Me.lblTituloHeader.Name = "lblTituloHeader"
            Me.lblTituloHeader.Size = New System.Drawing.Size(350, 30)
            Me.lblTituloHeader.TabIndex = 0
            Me.lblTituloHeader.Text = "Caja & Procesamiento de Pagos"

            ' lblSubtituloHeader
            Me.lblSubtituloHeader.AutoSize = True
            Me.lblSubtituloHeader.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblSubtituloHeader.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloHeader.Location = New System.Drawing.Point(20, 44)
            Me.lblSubtituloHeader.Name = "lblSubtituloHeader"
            Me.lblSubtituloHeader.Size = New System.Drawing.Size(640, 17)
            Me.lblSubtituloHeader.TabIndex = 1
            Me.lblSubtituloHeader.Text = "Cobro de pedidos pendientes, cálculo de cambio y emisión de facturación digital."

            ' pnlContenedor (DOCKING Y ESPACIADO OPTIMIZADO PARA TOUCH)
            Me.pnlContenedor.Controls.Add(Me.grpListado)
            Me.pnlContenedor.Controls.Add(Me.grpDetalleCobro)
            Me.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenedor.Location = New System.Drawing.Point(0, 75)
            Me.pnlContenedor.Name = "pnlContenedor"
            Me.pnlContenedor.Padding = New System.Windows.Forms.Padding(10)
            Me.pnlContenedor.Size = New System.Drawing.Size(1200, 605)
            Me.pnlContenedor.TabIndex = 1

            ' grpDetalleCobro (PANEL DERECHO DE COBRO TÁCTIL ANCHO 470px)
            Me.grpDetalleCobro.Controls.Add(Me.pnlCardCobro)
            Me.grpDetalleCobro.Dock = System.Windows.Forms.DockStyle.Right
            Me.grpDetalleCobro.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.grpDetalleCobro.Location = New System.Drawing.Point(720, 10)
            Me.grpDetalleCobro.Name = "grpDetalleCobro"
            Me.grpDetalleCobro.Padding = New System.Windows.Forms.Padding(10)
            Me.grpDetalleCobro.Size = New System.Drawing.Size(470, 585)
            Me.grpDetalleCobro.TabIndex = 1
            Me.grpDetalleCobro.TabStop = False
            Me.grpDetalleCobro.Text = "Detalle de la Cuenta & Procesamiento de Pago"

            ' pnlCardCobro
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
            Me.pnlCardCobro.Location = New System.Drawing.Point(10, 30)
            Me.pnlCardCobro.Name = "pnlCardCobro"
            Me.pnlCardCobro.Size = New System.Drawing.Size(450, 545)
            Me.pnlCardCobro.TabIndex = 0

            ' lblDetallePedidoId
            Me.lblDetallePedidoId.AutoSize = True
            Me.lblDetallePedidoId.Font = New System.Drawing.Font("Segoe UI", 14.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetallePedidoId.Location = New System.Drawing.Point(8, 8)
            Me.lblDetallePedidoId.Name = "lblDetallePedidoId"
            Me.lblDetallePedidoId.Size = New System.Drawing.Size(300, 25)
            Me.lblDetallePedidoId.TabIndex = 0
            Me.lblDetallePedidoId.Text = "Pedido: (Ninguno seleccionado)"

            ' lblDetalleCliente
            Me.lblDetalleCliente.AutoSize = True
            Me.lblDetalleCliente.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblDetalleCliente.Location = New System.Drawing.Point(8, 40)
            Me.lblDetalleCliente.Name = "lblDetalleCliente"
            Me.lblDetalleCliente.Size = New System.Drawing.Size(200, 20)
            Me.lblDetalleCliente.TabIndex = 1
            Me.lblDetalleCliente.Text = "Cliente: Seleccione un pedido"

            ' lblDetalleMesa
            Me.lblDetalleMesa.AutoSize = True
            Me.lblDetalleMesa.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblDetalleMesa.Location = New System.Drawing.Point(8, 68)
            Me.lblDetalleMesa.Name = "lblDetalleMesa"
            Me.lblDetalleMesa.Size = New System.Drawing.Size(140, 20)
            Me.lblDetalleMesa.TabIndex = 2
            Me.lblDetalleMesa.Text = "Mesa / Servicio: --"

            ' lblDetallePlato
            Me.lblDetallePlato.AutoSize = True
            Me.lblDetallePlato.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetallePlato.Location = New System.Drawing.Point(8, 98)
            Me.lblDetallePlato.Name = "lblDetallePlato"
            Me.lblDetallePlato.Size = New System.Drawing.Size(150, 20)
            Me.lblDetallePlato.TabIndex = 3
            Me.lblDetallePlato.Text = "Consumo: Ninguno"

            ' lblDetalleAcomp
            Me.lblDetalleAcomp.AutoSize = True
            Me.lblDetalleAcomp.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblDetalleAcomp.ForeColor = System.Drawing.Color.DimGray
            Me.lblDetalleAcomp.Location = New System.Drawing.Point(8, 126)
            Me.lblDetalleAcomp.Name = "lblDetalleAcomp"
            Me.lblDetalleAcomp.Size = New System.Drawing.Size(65, 19)
            Me.lblDetalleAcomp.TabIndex = 4
            Me.lblDetalleAcomp.Text = "Extras: --"

            ' pnlSeparador1
            Me.pnlSeparador1.BackColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.pnlSeparador1.Location = New System.Drawing.Point(8, 155)
            Me.pnlSeparador1.Name = "pnlSeparador1"
            Me.pnlSeparador1.Size = New System.Drawing.Size(430, 1)
            Me.pnlSeparador1.TabIndex = 5

            ' lblEtiquetaMetodo
            Me.lblEtiquetaMetodo.AutoSize = True
            Me.lblEtiquetaMetodo.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblEtiquetaMetodo.Location = New System.Drawing.Point(8, 164)
            Me.lblEtiquetaMetodo.Name = "lblEtiquetaMetodo"
            Me.lblEtiquetaMetodo.Size = New System.Drawing.Size(121, 20)
            Me.lblEtiquetaMetodo.TabIndex = 6
            Me.lblEtiquetaMetodo.Text = "Forma de Pago:"

            ' pnlMetodosPago (OPCIONES DE PAGO TÁCTILES AMPLIAS)
            Me.pnlMetodosPago.Controls.Add(Me.rbDigital)
            Me.pnlMetodosPago.Controls.Add(Me.rbTarjeta)
            Me.pnlMetodosPago.Controls.Add(Me.rbEfectivo)
            Me.pnlMetodosPago.Location = New System.Drawing.Point(8, 186)
            Me.pnlMetodosPago.Name = "pnlMetodosPago"
            Me.pnlMetodosPago.Size = New System.Drawing.Size(430, 44)
            Me.pnlMetodosPago.TabIndex = 7

            ' rbEfectivo
            Me.rbEfectivo.AutoSize = True
            Me.rbEfectivo.Checked = True
            Me.rbEfectivo.Cursor = System.Windows.Forms.Cursors.Hand
            Me.rbEfectivo.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Bold)
            Me.rbEfectivo.Location = New System.Drawing.Point(4, 6)
            Me.rbEfectivo.Name = "rbEfectivo"
            Me.rbEfectivo.Size = New System.Drawing.Size(96, 29)
            Me.rbEfectivo.TabIndex = 0
            Me.rbEfectivo.TabStop = True
            Me.rbEfectivo.Text = "Efectivo"
            Me.rbEfectivo.UseVisualStyleBackColor = True

            ' rbTarjeta
            Me.rbTarjeta.AutoSize = True
            Me.rbTarjeta.Cursor = System.Windows.Forms.Cursors.Hand
            Me.rbTarjeta.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Bold)
            Me.rbTarjeta.Location = New System.Drawing.Point(124, 6)
            Me.rbTarjeta.Name = "rbTarjeta"
            Me.rbTarjeta.Size = New System.Drawing.Size(90, 29)
            Me.rbTarjeta.TabIndex = 1
            Me.rbTarjeta.Text = "Tarjeta"
            Me.rbTarjeta.UseVisualStyleBackColor = True

            ' rbDigital
            Me.rbDigital.AutoSize = True
            Me.rbDigital.Cursor = System.Windows.Forms.Cursors.Hand
            Me.rbDigital.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Bold)
            Me.rbDigital.Location = New System.Drawing.Point(234, 6)
            Me.rbDigital.Name = "rbDigital"
            Me.rbDigital.Size = New System.Drawing.Size(195, 29)
            Me.rbDigital.TabIndex = 2
            Me.rbDigital.Text = "QR / Transferencia"
            Me.rbDigital.UseVisualStyleBackColor = True

            ' pnlSeparador2
            Me.pnlSeparador2.BackColor = System.Drawing.Color.FromArgb(226, 221, 213)
            Me.pnlSeparador2.Location = New System.Drawing.Point(8, 236)
            Me.pnlSeparador2.Name = "pnlSeparador2"
            Me.pnlSeparador2.Size = New System.Drawing.Size(430, 1)
            Me.pnlSeparador2.TabIndex = 8

            ' lblSubtotalEtiqueta
            Me.lblSubtotalEtiqueta.AutoSize = True
            Me.lblSubtotalEtiqueta.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblSubtotalEtiqueta.Location = New System.Drawing.Point(8, 245)
            Me.lblSubtotalEtiqueta.Name = "lblSubtotalEtiqueta"
            Me.lblSubtotalEtiqueta.Size = New System.Drawing.Size(68, 20)
            Me.lblSubtotalEtiqueta.TabIndex = 9
            Me.lblSubtotalEtiqueta.Text = "Subtotal:"

            ' lblSubtotalValor
            Me.lblSubtotalValor.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblSubtotalValor.Location = New System.Drawing.Point(240, 245)
            Me.lblSubtotalValor.Name = "lblSubtotalValor"
            Me.lblSubtotalValor.Size = New System.Drawing.Size(195, 20)
            Me.lblSubtotalValor.TabIndex = 10
            Me.lblSubtotalValor.Text = "$0.00"
            Me.lblSubtotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' lblImpuestoEtiqueta
            Me.lblImpuestoEtiqueta.AutoSize = True
            Me.lblImpuestoEtiqueta.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblImpuestoEtiqueta.Location = New System.Drawing.Point(8, 272)
            Me.lblImpuestoEtiqueta.Name = "lblImpuestoEtiqueta"
            Me.lblImpuestoEtiqueta.Size = New System.Drawing.Size(145, 20)
            Me.lblImpuestoEtiqueta.TabIndex = 11
            Me.lblImpuestoEtiqueta.Text = "Impuesto ITBMS (7%):"

            ' lblImpuestoValor
            Me.lblImpuestoValor.Font = New System.Drawing.Font("Segoe UI", 11.0F)
            Me.lblImpuestoValor.Location = New System.Drawing.Point(240, 272)
            Me.lblImpuestoValor.Name = "lblImpuestoValor"
            Me.lblImpuestoValor.Size = New System.Drawing.Size(195, 20)
            Me.lblImpuestoValor.TabIndex = 12
            Me.lblImpuestoValor.Text = "$0.00"
            Me.lblImpuestoValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' pnlTotalDestacado
            Me.pnlTotalDestacado.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.pnlTotalDestacado.Controls.Add(Me.lblTotalValor)
            Me.pnlTotalDestacado.Controls.Add(Me.lblTotalEtiqueta)
            Me.pnlTotalDestacado.Location = New System.Drawing.Point(8, 302)
            Me.pnlTotalDestacado.Name = "pnlTotalDestacado"
            Me.pnlTotalDestacado.Size = New System.Drawing.Size(430, 52)
            Me.pnlTotalDestacado.TabIndex = 13

            ' lblTotalEtiqueta
            Me.lblTotalEtiqueta.AutoSize = True
            Me.lblTotalEtiqueta.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblTotalEtiqueta.Location = New System.Drawing.Point(12, 16)
            Me.lblTotalEtiqueta.Name = "lblTotalEtiqueta"
            Me.lblTotalEtiqueta.Size = New System.Drawing.Size(142, 21)
            Me.lblTotalEtiqueta.TabIndex = 0
            Me.lblTotalEtiqueta.Text = "TOTAL A PAGAR:"

            ' lblTotalValor
            Me.lblTotalValor.Font = New System.Drawing.Font("Segoe UI", 20.0F, System.Drawing.FontStyle.Bold)
            Me.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblTotalValor.Location = New System.Drawing.Point(180, 8)
            Me.lblTotalValor.Name = "lblTotalValor"
            Me.lblTotalValor.Size = New System.Drawing.Size(240, 36)
            Me.lblTotalValor.TabIndex = 1
            Me.lblTotalValor.Text = "$0.00"
            Me.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' pnlCalculoCaja (CUADRO DE ENTRADA Y CAMBIO TÁCTIL)
            Me.pnlCalculoCaja.BackColor = System.Drawing.Color.FromArgb(250, 249, 246)
            Me.pnlCalculoCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCalculoCaja.Controls.Add(Me.lblCambioValor)
            Me.pnlCalculoCaja.Controls.Add(Me.lblCambioEtiqueta)
            Me.pnlCalculoCaja.Controls.Add(Me.txtMontoRecibido)
            Me.pnlCalculoCaja.Controls.Add(Me.lblMontoRecibidoEtiqueta)
            Me.pnlCalculoCaja.Location = New System.Drawing.Point(8, 362)
            Me.pnlCalculoCaja.Name = "pnlCalculoCaja"
            Me.pnlCalculoCaja.Size = New System.Drawing.Size(430, 100)
            Me.pnlCalculoCaja.TabIndex = 14

            ' lblMontoRecibidoEtiqueta
            Me.lblMontoRecibidoEtiqueta.AutoSize = True
            Me.lblMontoRecibidoEtiqueta.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoRecibidoEtiqueta.Location = New System.Drawing.Point(12, 16)
            Me.lblMontoRecibidoEtiqueta.Name = "lblMontoRecibidoEtiqueta"
            Me.lblMontoRecibidoEtiqueta.Size = New System.Drawing.Size(152, 20)
            Me.lblMontoRecibidoEtiqueta.TabIndex = 0
            Me.lblMontoRecibidoEtiqueta.Text = "Dinero Recibido ($):"

            ' txtMontoRecibido
            Me.txtMontoRecibido.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.txtMontoRecibido.Location = New System.Drawing.Point(180, 10)
            Me.txtMontoRecibido.Name = "txtMontoRecibido"
            Me.txtMontoRecibido.Size = New System.Drawing.Size(235, 36)
            Me.txtMontoRecibido.TabIndex = 1
            Me.txtMontoRecibido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right

            ' lblCambioEtiqueta
            Me.lblCambioEtiqueta.AutoSize = True
            Me.lblCambioEtiqueta.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblCambioEtiqueta.Location = New System.Drawing.Point(12, 60)
            Me.lblCambioEtiqueta.Name = "lblCambioEtiqueta"
            Me.lblCambioEtiqueta.Size = New System.Drawing.Size(130, 20)
            Me.lblCambioEtiqueta.TabIndex = 2
            Me.lblCambioEtiqueta.Text = "Cambio / Vuelto:"

            ' lblCambioValor
            Me.lblCambioValor.Font = New System.Drawing.Font("Segoe UI", 16.0F, System.Drawing.FontStyle.Bold)
            Me.lblCambioValor.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblCambioValor.Location = New System.Drawing.Point(180, 56)
            Me.lblCambioValor.Name = "lblCambioValor"
            Me.lblCambioValor.Size = New System.Drawing.Size(235, 30)
            Me.lblCambioValor.TabIndex = 3
            Me.lblCambioValor.Text = "$0.00"
            Me.lblCambioValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' btnConfirmarCobro (BOTÓN GIGANTE TÁCTIL)
            Me.btnConfirmarCobro.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Bold)
            Me.btnConfirmarCobro.Location = New System.Drawing.Point(8, 472)
            Me.btnConfirmarCobro.Name = "btnConfirmarCobro"
            Me.btnConfirmarCobro.Size = New System.Drawing.Size(430, 52)
            Me.btnConfirmarCobro.TabIndex = 15
            Me.btnConfirmarCobro.Text = "Confirmar y Procesar Cobro"
            Me.btnConfirmarCobro.UseVisualStyleBackColor = True

            ' btnLimpiar
            Me.btnLimpiar.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.btnLimpiar.Location = New System.Drawing.Point(8, 532)
            Me.btnLimpiar.Name = "btnLimpiar"
            Me.btnLimpiar.Size = New System.Drawing.Size(430, 40)
            Me.btnLimpiar.TabIndex = 16
            Me.btnLimpiar.Text = "Limpiar Selección"
            Me.btnLimpiar.UseVisualStyleBackColor = True

            ' grpListado (LISTA DE PEDIDOS PENDIENTES TÁCTIL IZQUIERDA)
            Me.grpListado.Controls.Add(Me.dgvPedidosPendientes)
            Me.grpListado.Controls.Add(Me.pnlFiltros)
            Me.grpListado.Controls.Add(Me.lblTotalPendientes)
            Me.grpListado.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpListado.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.grpListado.Location = New System.Drawing.Point(10, 10)
            Me.grpListado.Name = "grpListado"
            Me.grpListado.Padding = New System.Windows.Forms.Padding(10)
            Me.grpListado.Size = New System.Drawing.Size(700, 585)
            Me.grpListado.TabIndex = 0
            Me.grpListado.TabStop = False
            Me.grpListado.Text = "Pedidos Pendientes de Pago"

            ' pnlFiltros (BARRA DE BÚSQUEDA Y ACCIONES TÁCTIL DE 60px)
            Me.pnlFiltros.Controls.Add(Me.btnRefrescar)
            Me.pnlFiltros.Controls.Add(Me.btnBuscar)
            Me.pnlFiltros.Controls.Add(Me.txtBuscar)
            Me.pnlFiltros.Controls.Add(Me.lblBuscar)
            Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltros.Location = New System.Drawing.Point(10, 30)
            Me.pnlFiltros.Name = "pnlFiltros"
            Me.pnlFiltros.Size = New System.Drawing.Size(680, 60)
            Me.pnlFiltros.TabIndex = 0

            ' lblBuscar
            Me.lblBuscar.AutoSize = True
            Me.lblBuscar.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblBuscar.Location = New System.Drawing.Point(8, 18)
            Me.lblBuscar.Name = "lblBuscar"
            Me.lblBuscar.Size = New System.Drawing.Size(110, 20)
            Me.lblBuscar.TabIndex = 0
            Me.lblBuscar.Text = "Buscar Pedido:"

            ' txtBuscar
            Me.txtBuscar.Font = New System.Drawing.Font("Segoe UI", 14.0F)
            Me.txtBuscar.Location = New System.Drawing.Point(125, 12)
            Me.txtBuscar.Name = "txtBuscar"
            Me.txtBuscar.Size = New System.Drawing.Size(220, 32)
            Me.txtBuscar.TabIndex = 1

            ' btnBuscar
            Me.btnBuscar.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.btnBuscar.Location = New System.Drawing.Point(355, 10)
            Me.btnBuscar.Name = "btnBuscar"
            Me.btnBuscar.Size = New System.Drawing.Size(120, 38)
            Me.btnBuscar.TabIndex = 2
            Me.btnBuscar.Text = "Buscar"
            Me.btnBuscar.UseVisualStyleBackColor = True

            ' btnRefrescar
            Me.btnRefrescar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnRefrescar.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.btnRefrescar.Location = New System.Drawing.Point(535, 10)
            Me.btnRefrescar.Name = "btnRefrescar"
            Me.btnRefrescar.Size = New System.Drawing.Size(135, 38)
            Me.btnRefrescar.TabIndex = 3
            Me.btnRefrescar.Text = "Actualizar"
            Me.btnRefrescar.UseVisualStyleBackColor = True

            ' dgvPedidosPendientes (GRILLA CON FILAS TÁCTILES ALTAS)
            Me.dgvPedidosPendientes.AllowUserToAddRows = False
            Me.dgvPedidosPendientes.AllowUserToDeleteRows = False
            Me.dgvPedidosPendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvPedidosPendientes.BackgroundColor = System.Drawing.Color.White
            Me.dgvPedidosPendientes.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgvPedidosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvPedidosPendientes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPedidosPendientes.Location = New System.Drawing.Point(10, 90)
            Me.dgvPedidosPendientes.MultiSelect = False
            Me.dgvPedidosPendientes.Name = "dgvPedidosPendientes"
            Me.dgvPedidosPendientes.ReadOnly = True
            Me.dgvPedidosPendientes.RowHeadersVisible = False
            Me.dgvPedidosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvPedidosPendientes.Size = New System.Drawing.Size(680, 453)
            Me.dgvPedidosPendientes.TabIndex = 1

            ' lblTotalPendientes
            Me.lblTotalPendientes.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.lblTotalPendientes.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblTotalPendientes.ForeColor = System.Drawing.Color.Gray
            Me.lblTotalPendientes.Location = New System.Drawing.Point(10, 543)
            Me.lblTotalPendientes.Name = "lblTotalPendientes"
            Me.lblTotalPendientes.Size = New System.Drawing.Size(680, 32)
            Me.lblTotalPendientes.TabIndex = 2
            Me.lblTotalPendientes.Text = "Pedidos pendientes de cobro: 0"
            Me.lblTotalPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

            ' FrmCajaCobros
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(1200, 680)
            Me.Controls.Add(Me.pnlContenedor)
            Me.Controls.Add(Me.pnlHeader)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "FrmCajaCobros"
            Me.Text = "Caja & Procesamiento de Pagos"
            Me.pnlHeader.ResumeLayout(False)
            Me.pnlHeader.PerformLayout()
            Me.pnlContenedor.ResumeLayout(False)
            Me.grpDetalleCobro.ResumeLayout(False)
            Me.pnlCardCobro.ResumeLayout(False)
            Me.pnlCardCobro.PerformLayout()
            Me.pnlMetodosPago.ResumeLayout(False)
            Me.pnlMetodosPago.PerformLayout()
            Me.pnlTotalDestacado.ResumeLayout(False)
            Me.pnlTotalDestacado.PerformLayout()
            Me.pnlCalculoCaja.ResumeLayout(False)
            Me.pnlCalculoCaja.PerformLayout()
            Me.grpListado.ResumeLayout(False)
            Me.pnlFiltros.ResumeLayout(False)
            Me.pnlFiltros.PerformLayout()
            CType(Me.dgvPedidosPendientes, System.ComponentModel.ISupportInitialize).EndInit()
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
