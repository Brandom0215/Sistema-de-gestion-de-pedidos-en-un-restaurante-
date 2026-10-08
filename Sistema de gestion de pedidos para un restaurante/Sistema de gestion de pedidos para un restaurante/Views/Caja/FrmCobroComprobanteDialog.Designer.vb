Namespace Views.Caja
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmCobroComprobanteDialog
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
            Me.pnlTop = New System.Windows.Forms.Panel()
            Me.lblSubtitulo = New System.Windows.Forms.Label()
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.pnlContenedor = New System.Windows.Forms.Panel()
            Me.pnlDerecha = New System.Windows.Forms.Panel()
            Me.grpVistaPrevia = New System.Windows.Forms.GroupBox()
            Me.pnlTicketVisual = New System.Windows.Forms.Panel()
            Me.lblTicketPie = New System.Windows.Forms.Label()
            Me.pnlSep5 = New System.Windows.Forms.Panel()
            Me.lblTicketCambio = New System.Windows.Forms.Label()
            Me.lblTicketRecibido = New System.Windows.Forms.Label()
            Me.lblTicketTotal = New System.Windows.Forms.Label()
            Me.lblTicketImpuesto = New System.Windows.Forms.Label()
            Me.lblTicketSubtotal = New System.Windows.Forms.Label()
            Me.pnlSep4 = New System.Windows.Forms.Panel()
            Me.lblTicketPlato = New System.Windows.Forms.Label()
            Me.lblTicketAcomp = New System.Windows.Forms.Label()
            Me.pnlSep3 = New System.Windows.Forms.Panel()
            Me.lblTicketMesa = New System.Windows.Forms.Label()
            Me.lblTicketMetodo = New System.Windows.Forms.Label()
            Me.lblTicketCliente = New System.Windows.Forms.Label()
            Me.lblTicketRuc = New System.Windows.Forms.Label()
            Me.lblTicketFecha = New System.Windows.Forms.Label()
            Me.lblTicketCorrelativo = New System.Windows.Forms.Label()
            Me.pnlSep2 = New System.Windows.Forms.Panel()
            Me.lblTicketCiudad = New System.Windows.Forms.Label()
            Me.lblTicketRucEmpresa = New System.Windows.Forms.Label()
            Me.lblTicketSubtitulo = New System.Windows.Forms.Label()
            Me.lblTicketNombreEmpresa = New System.Windows.Forms.Label()
            Me.pnlIzquierda = New System.Windows.Forms.Panel()
            Me.pnlBotones = New System.Windows.Forms.Panel()
            Me.btnCerrar = New System.Windows.Forms.Button()
            Me.btnEnviarCorreo = New System.Windows.Forms.Button()
            Me.btnImprimirTicket = New System.Windows.Forms.Button()
            Me.grpDatosFiscales = New System.Windows.Forms.GroupBox()
            Me.txtDireccion = New System.Windows.Forms.TextBox()
            Me.lblDireccion = New System.Windows.Forms.Label()
            Me.txtCorreo = New System.Windows.Forms.TextBox()
            Me.lblCorreo = New System.Windows.Forms.Label()
            Me.txtTelefono = New System.Windows.Forms.TextBox()
            Me.lblTelefono = New System.Windows.Forms.Label()
            Me.txtRazonSocial = New System.Windows.Forms.TextBox()
            Me.lblRazonSocial = New System.Windows.Forms.Label()
            Me.txtRucCedula = New System.Windows.Forms.TextBox()
            Me.lblRuc = New System.Windows.Forms.Label()
            Me.pnlTop.SuspendLayout()
            Me.pnlContenedor.SuspendLayout()
            Me.pnlDerecha.SuspendLayout()
            Me.grpVistaPrevia.SuspendLayout()
            Me.pnlTicketVisual.SuspendLayout()
            Me.pnlIzquierda.SuspendLayout()
            Me.pnlBotones.SuspendLayout()
            Me.grpDatosFiscales.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlTop
            '
            Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(237, Byte), Integer))
            Me.pnlTop.Controls.Add(Me.lblSubtitulo)
            Me.pnlTop.Controls.Add(Me.lblTitulo)
            Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTop.Location = New System.Drawing.Point(0, 0)
            Me.pnlTop.Name = "pnlTop"
            Me.pnlTop.Padding = New System.Windows.Forms.Padding(18, 14, 18, 10)
            Me.pnlTop.Size = New System.Drawing.Size(810, 64)
            Me.pnlTop.TabIndex = 0
            '
            ' lblSubtitulo
            '
            Me.lblSubtitulo.AutoSize = True
            Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtitulo.ForeColor = System.Drawing.Color.DimGray
            Me.lblSubtitulo.Location = New System.Drawing.Point(18, 38)
            Me.lblSubtitulo.Name = "lblSubtitulo"
            Me.lblSubtitulo.Size = New System.Drawing.Size(540, 15)
            Me.lblSubtitulo.TabIndex = 1
            Me.lblSubtitulo.Text = "Pedido cobrado con éxito. Puede imprimir o enviar el recibo al cliente."
            '
            ' lblTitulo
            '
            Me.lblTitulo.AutoSize = True
            Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold)
            Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.lblTitulo.Location = New System.Drawing.Point(18, 10)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(325, 25)
            Me.lblTitulo.TabIndex = 0
            Me.lblTitulo.Text = "🧾 Recibo de Pago"
            '
            ' pnlContenedor
            '
            Me.pnlContenedor.Controls.Add(Me.pnlDerecha)
            Me.pnlContenedor.Controls.Add(Me.pnlIzquierda)
            Me.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenedor.Location = New System.Drawing.Point(0, 64)
            Me.pnlContenedor.Name = "pnlContenedor"
            Me.pnlContenedor.Padding = New System.Windows.Forms.Padding(16, 10, 16, 14)
            Me.pnlContenedor.Size = New System.Drawing.Size(810, 526)
            Me.pnlContenedor.TabIndex = 1
            '
            ' pnlDerecha
            '
            Me.pnlDerecha.Controls.Add(Me.grpVistaPrevia)
            Me.pnlDerecha.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlDerecha.Location = New System.Drawing.Point(440, 10)
            Me.pnlDerecha.Name = "pnlDerecha"
            Me.pnlDerecha.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
            Me.pnlDerecha.Size = New System.Drawing.Size(354, 502)
            Me.pnlDerecha.TabIndex = 1
            '
            ' grpVistaPrevia
            '
            Me.grpVistaPrevia.Controls.Add(Me.pnlTicketVisual)
            Me.grpVistaPrevia.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpVistaPrevia.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpVistaPrevia.Location = New System.Drawing.Point(6, 0)
            Me.grpVistaPrevia.Name = "grpVistaPrevia"
            Me.grpVistaPrevia.Padding = New System.Windows.Forms.Padding(10, 6, 10, 8)
            Me.grpVistaPrevia.Size = New System.Drawing.Size(348, 502)
            Me.grpVistaPrevia.TabIndex = 0
            Me.grpVistaPrevia.TabStop = False
            Me.grpVistaPrevia.Text = "Vista previa del recibo"
            '
            ' pnlTicketVisual
            '
            Me.pnlTicketVisual.AutoScroll = True
            Me.pnlTicketVisual.BackColor = System.Drawing.Color.White
            Me.pnlTicketVisual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketPie)
            Me.pnlTicketVisual.Controls.Add(Me.pnlSep5)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketCambio)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketRecibido)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketTotal)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketImpuesto)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketSubtotal)
            Me.pnlTicketVisual.Controls.Add(Me.pnlSep4)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketPlato)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketAcomp)
            Me.pnlTicketVisual.Controls.Add(Me.pnlSep3)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketMesa)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketMetodo)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketCliente)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketRuc)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketFecha)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketCorrelativo)
            Me.pnlTicketVisual.Controls.Add(Me.pnlSep2)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketCiudad)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketRucEmpresa)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketSubtitulo)
            Me.pnlTicketVisual.Controls.Add(Me.lblTicketNombreEmpresa)
            Me.pnlTicketVisual.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlTicketVisual.Location = New System.Drawing.Point(10, 22)
            Me.pnlTicketVisual.Name = "pnlTicketVisual"
            Me.pnlTicketVisual.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
            Me.pnlTicketVisual.Size = New System.Drawing.Size(328, 472)
            Me.pnlTicketVisual.TabIndex = 0
            '
            ' lblTicketPie
            '
            Me.lblTicketPie.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketPie.Font = New System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Italic)
            Me.lblTicketPie.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketPie.Location = New System.Drawing.Point(12, 420)
            Me.lblTicketPie.Name = "lblTicketPie"
            Me.lblTicketPie.Size = New System.Drawing.Size(302, 34)
            Me.lblTicketPie.TabIndex = 21
            Me.lblTicketPie.Text = "¡Gracias por su visita y preferencia!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Restaurante El Buen Sazón"
            Me.lblTicketPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' pnlSep5
            '
            Me.pnlSep5.BackColor = System.Drawing.Color.LightGray
            Me.pnlSep5.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSep5.Location = New System.Drawing.Point(12, 412)
            Me.pnlSep5.Name = "pnlSep5"
            Me.pnlSep5.Size = New System.Drawing.Size(302, 8)
            Me.pnlSep5.TabIndex = 20
            '
            ' lblTicketCambio
            '
            Me.lblTicketCambio.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketCambio.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketCambio.Location = New System.Drawing.Point(12, 394)
            Me.lblTicketCambio.Name = "lblTicketCambio"
            Me.lblTicketCambio.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketCambio.TabIndex = 19
            Me.lblTicketCambio.Text = "Vuelto / Cambio: $0.00"
            Me.lblTicketCambio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblTicketRecibido
            '
            Me.lblTicketRecibido.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketRecibido.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketRecibido.Location = New System.Drawing.Point(12, 376)
            Me.lblTicketRecibido.Name = "lblTicketRecibido"
            Me.lblTicketRecibido.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketRecibido.TabIndex = 18
            Me.lblTicketRecibido.Text = "Monto Recibido: $0.00"
            Me.lblTicketRecibido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblTicketTotal
            '
            Me.lblTicketTotal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketTotal.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblTicketTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(72, Byte), Integer))
            Me.lblTicketTotal.Location = New System.Drawing.Point(12, 350)
            Me.lblTicketTotal.Name = "lblTicketTotal"
            Me.lblTicketTotal.Size = New System.Drawing.Size(302, 26)
            Me.lblTicketTotal.TabIndex = 17
            Me.lblTicketTotal.Text = "TOTAL PAGADO: $0.00"
            Me.lblTicketTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblTicketImpuesto
            '
            Me.lblTicketImpuesto.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketImpuesto.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketImpuesto.Location = New System.Drawing.Point(12, 332)
            Me.lblTicketImpuesto.Name = "lblTicketImpuesto"
            Me.lblTicketImpuesto.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketImpuesto.TabIndex = 16
            Me.lblTicketImpuesto.Text = "ITBMS (7%): $0.00"
            Me.lblTicketImpuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' lblTicketSubtotal
            '
            Me.lblTicketSubtotal.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketSubtotal.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketSubtotal.Location = New System.Drawing.Point(12, 314)
            Me.lblTicketSubtotal.Name = "lblTicketSubtotal"
            Me.lblTicketSubtotal.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketSubtotal.TabIndex = 15
            Me.lblTicketSubtotal.Text = "Subtotal Gravable: $0.00"
            Me.lblTicketSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            ' pnlSep4
            '
            Me.pnlSep4.BackColor = System.Drawing.Color.LightGray
            Me.pnlSep4.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSep4.Location = New System.Drawing.Point(12, 306)
            Me.pnlSep4.Name = "pnlSep4"
            Me.pnlSep4.Size = New System.Drawing.Size(302, 8)
            Me.pnlSep4.TabIndex = 14
            '
            ' lblTicketAcomp
            '
            Me.lblTicketAcomp.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketAcomp.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Italic)
            Me.lblTicketAcomp.ForeColor = System.Drawing.Color.Gray
            Me.lblTicketAcomp.Location = New System.Drawing.Point(12, 288)
            Me.lblTicketAcomp.Name = "lblTicketAcomp"
            Me.lblTicketAcomp.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketAcomp.TabIndex = 13
            Me.lblTicketAcomp.Text = "   Extras: Ninguno"
            '
            ' lblTicketPlato
            '
            Me.lblTicketPlato.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketPlato.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblTicketPlato.Location = New System.Drawing.Point(12, 268)
            Me.lblTicketPlato.Name = "lblTicketPlato"
            Me.lblTicketPlato.Size = New System.Drawing.Size(302, 20)
            Me.lblTicketPlato.TabIndex = 12
            Me.lblTicketPlato.Text = "1 x Plato Gastronómico"
            '
            ' pnlSep3
            '
            Me.pnlSep3.BackColor = System.Drawing.Color.LightGray
            Me.pnlSep3.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSep3.Location = New System.Drawing.Point(12, 260)
            Me.pnlSep3.Name = "pnlSep3"
            Me.pnlSep3.Size = New System.Drawing.Size(302, 8)
            Me.pnlSep3.TabIndex = 11
            '
            ' lblTicketMesa
            '
            Me.lblTicketMesa.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketMesa.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketMesa.Location = New System.Drawing.Point(12, 242)
            Me.lblTicketMesa.Name = "lblTicketMesa"
            Me.lblTicketMesa.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketMesa.TabIndex = 10
            Me.lblTicketMesa.Text = "Servicio: En Mesa • Mesa 01"
            '
            ' lblTicketMetodo
            '
            Me.lblTicketMetodo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketMetodo.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketMetodo.Location = New System.Drawing.Point(12, 224)
            Me.lblTicketMetodo.Name = "lblTicketMetodo"
            Me.lblTicketMetodo.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketMetodo.TabIndex = 9
            Me.lblTicketMetodo.Text = "Forma de Pago: Efectivo"
            '
            ' lblTicketCliente
            '
            Me.lblTicketCliente.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketCliente.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblTicketCliente.Location = New System.Drawing.Point(12, 206)
            Me.lblTicketCliente.Name = "lblTicketCliente"
            Me.lblTicketCliente.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketCliente.TabIndex = 8
            Me.lblTicketCliente.Text = "Cliente: Consumidor Final"
            '
            ' lblTicketRuc
            '
            Me.lblTicketRuc.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketRuc.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketRuc.Location = New System.Drawing.Point(12, 188)
            Me.lblTicketRuc.Name = "lblTicketRuc"
            Me.lblTicketRuc.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketRuc.TabIndex = 7
            Me.lblTicketRuc.Text = "RUC/Cédula: 8-800-1234"
            '
            ' lblTicketFecha
            '
            Me.lblTicketFecha.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketFecha.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketFecha.Location = New System.Drawing.Point(12, 170)
            Me.lblTicketFecha.Name = "lblTicketFecha"
            Me.lblTicketFecha.Size = New System.Drawing.Size(302, 18)
            Me.lblTicketFecha.TabIndex = 6
            Me.lblTicketFecha.Text = "Fecha: 05/10/2026 22:30"
            '
            ' lblTicketCorrelativo
            '
            Me.lblTicketCorrelativo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketCorrelativo.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblTicketCorrelativo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(50, Byte), Integer))
            Me.lblTicketCorrelativo.Location = New System.Drawing.Point(12, 150)
            Me.lblTicketCorrelativo.Name = "lblTicketCorrelativo"
            Me.lblTicketCorrelativo.Size = New System.Drawing.Size(302, 20)
            Me.lblTicketCorrelativo.TabIndex = 5
            Me.lblTicketCorrelativo.Text = "FACTURA: FAC-2026-1001"
            '
            ' pnlSep2
            '
            Me.pnlSep2.BackColor = System.Drawing.Color.LightGray
            Me.pnlSep2.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSep2.Location = New System.Drawing.Point(12, 142)
            Me.pnlSep2.Name = "pnlSep2"
            Me.pnlSep2.Size = New System.Drawing.Size(302, 8)
            Me.pnlSep2.TabIndex = 4
            '
            ' lblTicketCiudad
            '
            Me.lblTicketCiudad.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketCiudad.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketCiudad.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketCiudad.Location = New System.Drawing.Point(12, 126)
            Me.lblTicketCiudad.Name = "lblTicketCiudad"
            Me.lblTicketCiudad.Size = New System.Drawing.Size(302, 16)
            Me.lblTicketCiudad.TabIndex = 3
            Me.lblTicketCiudad.Text = "Ciudad de Panamá, Rep. de Panamá"
            Me.lblTicketCiudad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTicketRucEmpresa
            '
            Me.lblTicketRucEmpresa.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketRucEmpresa.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketRucEmpresa.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketRucEmpresa.Location = New System.Drawing.Point(12, 110)
            Me.lblTicketRucEmpresa.Name = "lblTicketRucEmpresa"
            Me.lblTicketRucEmpresa.Size = New System.Drawing.Size(302, 16)
            Me.lblTicketRucEmpresa.TabIndex = 2
            Me.lblTicketRucEmpresa.Text = "RUC: 155698421-2-2024 DV 89  •  Tel: 223-9000"
            Me.lblTicketRucEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTicketSubtitulo
            '
            Me.lblTicketSubtitulo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketSubtitulo.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTicketSubtitulo.ForeColor = System.Drawing.Color.DimGray
            Me.lblTicketSubtitulo.Location = New System.Drawing.Point(12, 94)
            Me.lblTicketSubtitulo.Name = "lblTicketSubtitulo"
            Me.lblTicketSubtitulo.Size = New System.Drawing.Size(302, 16)
            Me.lblTicketSubtitulo.TabIndex = 1
            Me.lblTicketSubtitulo.Text = "Sabor Tradicional & Excelencia Gastronómica"
            Me.lblTicketSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTicketNombreEmpresa
            '
            Me.lblTicketNombreEmpresa.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTicketNombreEmpresa.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblTicketNombreEmpresa.ForeColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(72, Byte), Integer))
            Me.lblTicketNombreEmpresa.Location = New System.Drawing.Point(12, 10)
            Me.lblTicketNombreEmpresa.Name = "lblTicketNombreEmpresa"
            Me.lblTicketNombreEmpresa.Size = New System.Drawing.Size(302, 22)
            Me.lblTicketNombreEmpresa.TabIndex = 0
            Me.lblTicketNombreEmpresa.Text = "RESTAURANTE EL BUEN SAZÓN"
            Me.lblTicketNombreEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' pnlIzquierda
            '
            Me.pnlIzquierda.Controls.Add(Me.pnlBotones)
            Me.pnlIzquierda.Controls.Add(Me.grpDatosFiscales)
            Me.pnlIzquierda.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlIzquierda.Location = New System.Drawing.Point(16, 10)
            Me.pnlIzquierda.Name = "pnlIzquierda"
            Me.pnlIzquierda.Padding = New System.Windows.Forms.Padding(0, 0, 8, 0)
            Me.pnlIzquierda.Size = New System.Drawing.Size(424, 502)
            Me.pnlIzquierda.TabIndex = 0
            '
            ' pnlBotones
            '
            Me.pnlBotones.Controls.Add(Me.btnCerrar)
            Me.pnlBotones.Controls.Add(Me.btnEnviarCorreo)
            Me.pnlBotones.Controls.Add(Me.btnImprimirTicket)
            Me.pnlBotones.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlBotones.Location = New System.Drawing.Point(0, 310)
            Me.pnlBotones.Name = "pnlBotones"
            Me.pnlBotones.Padding = New System.Windows.Forms.Padding(4, 14, 4, 4)
            Me.pnlBotones.Size = New System.Drawing.Size(416, 192)
            Me.pnlBotones.TabIndex = 1
            '
            ' btnCerrar
            '
            Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCerrar.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnCerrar.Location = New System.Drawing.Point(4, 106)
            Me.btnCerrar.Name = "btnCerrar"
            Me.btnCerrar.Size = New System.Drawing.Size(408, 44)
            Me.btnCerrar.TabIndex = 2
            Me.btnCerrar.Text = "✓ Listo / Continuar"
            Me.btnCerrar.UseVisualStyleBackColor = True
            '
            ' btnEnviarCorreo
            '
            Me.btnEnviarCorreo.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnEnviarCorreo.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnEnviarCorreo.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnEnviarCorreo.Location = New System.Drawing.Point(4, 60)
            Me.btnEnviarCorreo.Name = "btnEnviarCorreo"
            Me.btnEnviarCorreo.Size = New System.Drawing.Size(408, 46)
            Me.btnEnviarCorreo.TabIndex = 1
            Me.btnEnviarCorreo.Text = "✉️ Enviar por Correo"
            Me.btnEnviarCorreo.UseVisualStyleBackColor = True
            '
            ' btnImprimirTicket
            '
            Me.btnImprimirTicket.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnImprimirTicket.Dock = System.Windows.Forms.DockStyle.Top
            Me.btnImprimirTicket.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnImprimirTicket.Location = New System.Drawing.Point(4, 14)
            Me.btnImprimirTicket.Name = "btnImprimirTicket"
            Me.btnImprimirTicket.Size = New System.Drawing.Size(408, 46)
            Me.btnImprimirTicket.TabIndex = 0
            Me.btnImprimirTicket.Text = "🖨️ Imprimir Recibo"
            Me.btnImprimirTicket.UseVisualStyleBackColor = True
            '
            ' grpDatosFiscales
            '
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
            Me.grpDatosFiscales.Dock = System.Windows.Forms.DockStyle.Top
            Me.grpDatosFiscales.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.grpDatosFiscales.Location = New System.Drawing.Point(0, 0)
            Me.grpDatosFiscales.Name = "grpDatosFiscales"
            Me.grpDatosFiscales.Padding = New System.Windows.Forms.Padding(12, 8, 12, 10)
            Me.grpDatosFiscales.Size = New System.Drawing.Size(416, 310)
            Me.grpDatosFiscales.TabIndex = 0
            Me.grpDatosFiscales.TabStop = False
            Me.grpDatosFiscales.Text = "Datos del Cliente (Opcional)"
            '
            ' txtDireccion
            '
            Me.txtDireccion.Dock = System.Windows.Forms.DockStyle.Top
            Me.txtDireccion.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtDireccion.Location = New System.Drawing.Point(12, 260)
            Me.txtDireccion.MaxLength = 80
            Me.txtDireccion.Name = "txtDireccion"
            Me.txtDireccion.Size = New System.Drawing.Size(392, 23)
            Me.txtDireccion.TabIndex = 9
            '
            ' lblDireccion
            '
            Me.lblDireccion.AutoSize = True
            Me.lblDireccion.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblDireccion.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblDireccion.ForeColor = System.Drawing.Color.DimGray
            Me.lblDireccion.Location = New System.Drawing.Point(12, 241)
            Me.lblDireccion.Name = "lblDireccion"
            Me.lblDireccion.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblDireccion.Size = New System.Drawing.Size(60, 19)
            Me.lblDireccion.TabIndex = 8
            Me.lblDireccion.Text = "Dirección:"
            '
            ' txtCorreo
            '
            Me.txtCorreo.Dock = System.Windows.Forms.DockStyle.Top
            Me.txtCorreo.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtCorreo.Location = New System.Drawing.Point(12, 218)
            Me.txtCorreo.MaxLength = 100
            Me.txtCorreo.Name = "txtCorreo"
            Me.txtCorreo.Size = New System.Drawing.Size(392, 23)
            Me.txtCorreo.TabIndex = 7
            '
            ' lblCorreo
            '
            Me.lblCorreo.AutoSize = True
            Me.lblCorreo.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCorreo.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblCorreo.ForeColor = System.Drawing.Color.DimGray
            Me.lblCorreo.Location = New System.Drawing.Point(12, 199)
            Me.lblCorreo.Name = "lblCorreo"
            Me.lblCorreo.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblCorreo.Size = New System.Drawing.Size(161, 19)
            Me.lblCorreo.TabIndex = 6
            Me.lblCorreo.Text = "Correo electrónico (Para envío):"
            '
            ' txtTelefono
            '
            Me.txtTelefono.Dock = System.Windows.Forms.DockStyle.Top
            Me.txtTelefono.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtTelefono.Location = New System.Drawing.Point(12, 176)
            Me.txtTelefono.MaxLength = 30
            Me.txtTelefono.Name = "txtTelefono"
            Me.txtTelefono.Size = New System.Drawing.Size(392, 23)
            Me.txtTelefono.TabIndex = 5
            '
            ' lblTelefono
            '
            Me.lblTelefono.AutoSize = True
            Me.lblTelefono.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblTelefono.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblTelefono.ForeColor = System.Drawing.Color.DimGray
            Me.lblTelefono.Location = New System.Drawing.Point(12, 157)
            Me.lblTelefono.Name = "lblTelefono"
            Me.lblTelefono.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblTelefono.Size = New System.Drawing.Size(55, 19)
            Me.lblTelefono.TabIndex = 4
            Me.lblTelefono.Text = "Teléfono:"
            '
            ' txtRazonSocial
            '
            Me.txtRazonSocial.Dock = System.Windows.Forms.DockStyle.Top
            Me.txtRazonSocial.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtRazonSocial.Location = New System.Drawing.Point(12, 94)
            Me.txtRazonSocial.MaxLength = 40
            Me.txtRazonSocial.Name = "txtRazonSocial"
            Me.txtRazonSocial.Size = New System.Drawing.Size(392, 23)
            Me.txtRazonSocial.TabIndex = 3
            '
            ' lblRazonSocial
            '
            Me.lblRazonSocial.AutoSize = True
            Me.lblRazonSocial.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblRazonSocial.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblRazonSocial.ForeColor = System.Drawing.Color.DimGray
            Me.lblRazonSocial.Location = New System.Drawing.Point(12, 75)
            Me.lblRazonSocial.Name = "lblRazonSocial"
            Me.lblRazonSocial.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
            Me.lblRazonSocial.Size = New System.Drawing.Size(110, 19)
            Me.lblRazonSocial.TabIndex = 2
            Me.lblRazonSocial.Text = "Nombre del cliente:"
            '
            ' txtRucCedula
            '
            Me.txtRucCedula.Dock = System.Windows.Forms.DockStyle.Top
            Me.txtRucCedula.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.txtRucCedula.Location = New System.Drawing.Point(12, 43)
            Me.txtRucCedula.MaxLength = 30
            Me.txtRucCedula.Name = "txtRucCedula"
            Me.txtRucCedula.Size = New System.Drawing.Size(392, 23)
            Me.txtRucCedula.TabIndex = 1
            '
            ' lblRuc
            '
            Me.lblRuc.AutoSize = True
            Me.lblRuc.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblRuc.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblRuc.ForeColor = System.Drawing.Color.DimGray
            Me.lblRuc.Location = New System.Drawing.Point(12, 24)
            Me.lblRuc.Name = "lblRuc"
            Me.lblRuc.Size = New System.Drawing.Size(78, 13)
            Me.lblRuc.TabIndex = 0
            Me.lblRuc.Text = "Cédula o RUC:"
            '
            ' FrmCobroComprobanteDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(237, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(810, 590)
            Me.Controls.Add(Me.pnlContenedor)
            Me.Controls.Add(Me.pnlTop)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmCobroComprobanteDialog"
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Recibo de Pago"
            Me.pnlTop.ResumeLayout(False)
            Me.pnlTop.PerformLayout()
            Me.pnlContenedor.ResumeLayout(False)
            Me.pnlDerecha.ResumeLayout(False)
            Me.grpVistaPrevia.ResumeLayout(False)
            Me.pnlTicketVisual.ResumeLayout(False)
            Me.pnlIzquierda.ResumeLayout(False)
            Me.pnlBotones.ResumeLayout(False)
            Me.grpDatosFiscales.ResumeLayout(False)
            Me.grpDatosFiscales.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlTop As Panel
        Friend WithEvents lblTitulo As Label
        Friend WithEvents lblSubtitulo As Label
        Friend WithEvents pnlContenedor As Panel
        Friend WithEvents pnlIzquierda As Panel
        Friend WithEvents pnlDerecha As Panel
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
        Friend WithEvents pnlBotones As Panel
        Friend WithEvents btnImprimirTicket As Button
        Friend WithEvents btnEnviarCorreo As Button
        Friend WithEvents btnCerrar As Button
        Friend WithEvents grpVistaPrevia As GroupBox
        Friend WithEvents pnlTicketVisual As Panel
        Friend WithEvents lblTicketNombreEmpresa As Label
        Friend WithEvents lblTicketSubtitulo As Label
        Friend WithEvents lblTicketRucEmpresa As Label
        Friend WithEvents lblTicketCiudad As Label
        Friend WithEvents pnlSep2 As Panel
        Friend WithEvents lblTicketCorrelativo As Label
        Friend WithEvents lblTicketFecha As Label
        Friend WithEvents lblTicketRuc As Label
        Friend WithEvents lblTicketCliente As Label
        Friend WithEvents lblTicketMetodo As Label
        Friend WithEvents lblTicketMesa As Label
        Friend WithEvents pnlSep3 As Panel
        Friend WithEvents lblTicketPlato As Label
        Friend WithEvents lblTicketAcomp As Label
        Friend WithEvents pnlSep4 As Panel
        Friend WithEvents lblTicketSubtotal As Label
        Friend WithEvents lblTicketImpuesto As Label
        Friend WithEvents lblTicketTotal As Label
        Friend WithEvents lblTicketRecibido As Label
        Friend WithEvents lblTicketCambio As Label
        Friend WithEvents pnlSep5 As Panel
        Friend WithEvents lblTicketPie As Label
    End Class
End Namespace
