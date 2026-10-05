Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Auth
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmRegistroCliente
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
            Me.pnlCardRegistro = New System.Windows.Forms.Panel()
            Me.lblLogo = New System.Windows.Forms.Label()
            Me.lblTituloRegistro = New System.Windows.Forms.Label()
            Me.lblSubtituloRegistro = New System.Windows.Forms.Label()
            Me.lblNombreCompleto = New System.Windows.Forms.Label()
            Me.txtNombreCompleto = New System.Windows.Forms.TextBox()
            Me.lblTelefono = New System.Windows.Forms.Label()
            Me.txtTelefono = New System.Windows.Forms.TextBox()
            Me.lblCorreo = New System.Windows.Forms.Label()
            Me.txtCorreo = New System.Windows.Forms.TextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.btnRegistrar = New System.Windows.Forms.Button()
            Me.btnVolverLogin = New System.Windows.Forms.Button()

            Me.pnlCardRegistro.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlCardRegistro
            '
            Me.pnlCardRegistro.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.pnlCardRegistro.BackColor = System.Drawing.Color.White
            Me.pnlCardRegistro.Controls.Add(Me.btnVolverLogin)
            Me.pnlCardRegistro.Controls.Add(Me.btnRegistrar)
            Me.pnlCardRegistro.Controls.Add(Me.txtPassword)
            Me.pnlCardRegistro.Controls.Add(Me.lblPassword)
            Me.pnlCardRegistro.Controls.Add(Me.txtCorreo)
            Me.pnlCardRegistro.Controls.Add(Me.lblCorreo)
            Me.pnlCardRegistro.Controls.Add(Me.txtTelefono)
            Me.pnlCardRegistro.Controls.Add(Me.lblTelefono)
            Me.pnlCardRegistro.Controls.Add(Me.txtNombreCompleto)
            Me.pnlCardRegistro.Controls.Add(Me.lblNombreCompleto)
            Me.pnlCardRegistro.Controls.Add(Me.lblSubtituloRegistro)
            Me.pnlCardRegistro.Controls.Add(Me.lblTituloRegistro)
            Me.pnlCardRegistro.Controls.Add(Me.lblLogo)
            Me.pnlCardRegistro.Location = New System.Drawing.Point(40, 20)
            Me.pnlCardRegistro.Name = "pnlCardRegistro"
            Me.pnlCardRegistro.Padding = New System.Windows.Forms.Padding(30)
            Me.pnlCardRegistro.Size = New System.Drawing.Size(450, 580)
            Me.pnlCardRegistro.TabIndex = 0
            '
            ' lblLogo
            '
            Me.lblLogo.AutoSize = True
            Me.lblLogo.Font = New System.Drawing.Font("Segoe UI", 26.0F)
            Me.lblLogo.Location = New System.Drawing.Point(195, 12)
            Me.lblLogo.Name = "lblLogo"
            Me.lblLogo.Size = New System.Drawing.Size(60, 47)
            Me.lblLogo.TabIndex = 0
            Me.lblLogo.Text = "👤"
            Me.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTituloRegistro
            '
            Me.lblTituloRegistro.Font = New System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold)
            Me.lblTituloRegistro.Location = New System.Drawing.Point(30, 65)
            Me.lblTituloRegistro.Name = "lblTituloRegistro"
            Me.lblTituloRegistro.Size = New System.Drawing.Size(390, 28)
            Me.lblTituloRegistro.TabIndex = 1
            Me.lblTituloRegistro.Text = "Registro de Nuevo Cliente"
            Me.lblTituloRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblSubtituloRegistro
            '
            Me.lblSubtituloRegistro.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloRegistro.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloRegistro.Location = New System.Drawing.Point(30, 95)
            Me.lblSubtituloRegistro.Name = "lblSubtituloRegistro"
            Me.lblSubtituloRegistro.Size = New System.Drawing.Size(390, 20)
            Me.lblSubtituloRegistro.TabIndex = 2
            Me.lblSubtituloRegistro.Text = "Crea tu cuenta para realizar pedidos e historial en el restaurante."
            Me.lblSubtituloRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblNombreCompleto
            '
            Me.lblNombreCompleto.AutoSize = True
            Me.lblNombreCompleto.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblNombreCompleto.Location = New System.Drawing.Point(30, 135)
            Me.lblNombreCompleto.Name = "lblNombreCompleto"
            Me.lblNombreCompleto.Size = New System.Drawing.Size(117, 15)
            Me.lblNombreCompleto.TabIndex = 3
            Me.lblNombreCompleto.Text = "Nombre Completo:"
            '
            ' txtNombreCompleto
            '
            Me.txtNombreCompleto.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtNombreCompleto.Location = New System.Drawing.Point(30, 153)
            Me.txtNombreCompleto.Name = "txtNombreCompleto"
            Me.txtNombreCompleto.Size = New System.Drawing.Size(390, 26)
            Me.txtNombreCompleto.TabIndex = 4
            '
            ' lblTelefono
            '
            Me.lblTelefono.AutoSize = True
            Me.lblTelefono.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblTelefono.Location = New System.Drawing.Point(30, 218)
            Me.lblTelefono.Name = "lblTelefono"
            Me.lblTelefono.Size = New System.Drawing.Size(126, 15)
            Me.lblTelefono.TabIndex = 5
            Me.lblTelefono.Text = "Número de Teléfono:"
            '
            ' txtTelefono
            '
            Me.txtTelefono.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtTelefono.Location = New System.Drawing.Point(30, 236)
            Me.txtTelefono.Name = "txtTelefono"
            Me.txtTelefono.Size = New System.Drawing.Size(390, 26)
            Me.txtTelefono.TabIndex = 6
            '
            ' lblCorreo
            '
            Me.lblCorreo.AutoSize = True
            Me.lblCorreo.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblCorreo.Location = New System.Drawing.Point(30, 301)
            Me.lblCorreo.Name = "lblCorreo"
            Me.lblCorreo.Size = New System.Drawing.Size(117, 15)
            Me.lblCorreo.TabIndex = 7
            Me.lblCorreo.Text = "Correo o Usuario:"
            '
            ' txtCorreo
            '
            Me.txtCorreo.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtCorreo.Location = New System.Drawing.Point(30, 319)
            Me.txtCorreo.Name = "txtCorreo"
            Me.txtCorreo.Size = New System.Drawing.Size(390, 26)
            Me.txtCorreo.TabIndex = 8
            '
            ' lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblPassword.Location = New System.Drawing.Point(30, 384)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(72, 15)
            Me.lblPassword.TabIndex = 9
            Me.lblPassword.Text = "Contraseña:"
            '
            ' txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtPassword.Location = New System.Drawing.Point(30, 402)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPassword.Size = New System.Drawing.Size(390, 26)
            Me.txtPassword.TabIndex = 10
            Me.txtPassword.UseSystemPasswordChar = True
            '
            ' btnRegistrar
            '
            Me.btnRegistrar.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnRegistrar.Location = New System.Drawing.Point(30, 465)
            Me.btnRegistrar.Name = "btnRegistrar"
            Me.btnRegistrar.Size = New System.Drawing.Size(390, 42)
            Me.btnRegistrar.TabIndex = 11
            Me.btnRegistrar.Text = "Crear Cuenta e Iniciar Pedido"
            Me.btnRegistrar.UseVisualStyleBackColor = True
            '
            ' btnVolverLogin
            '
            Me.btnVolverLogin.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.btnVolverLogin.Location = New System.Drawing.Point(30, 518)
            Me.btnVolverLogin.Name = "btnVolverLogin"
            Me.btnVolverLogin.Size = New System.Drawing.Size(390, 36)
            Me.btnVolverLogin.TabIndex = 12
            Me.btnVolverLogin.Text = "Volver a Inicio de Sesión"
            Me.btnVolverLogin.UseVisualStyleBackColor = True
            '
            ' FrmRegistroCliente
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(530, 620)
            Me.Controls.Add(Me.pnlCardRegistro)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "FrmRegistroCliente"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Registro de Cliente — Restaurante"
            Me.pnlCardRegistro.ResumeLayout(False)
            Me.pnlCardRegistro.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCardRegistro As Panel
        Friend WithEvents lblLogo As Label
        Friend WithEvents lblTituloRegistro As Label
        Friend WithEvents lblSubtituloRegistro As Label
        Friend WithEvents lblNombreCompleto As Label
        Friend WithEvents txtNombreCompleto As TextBox
        Friend WithEvents lblTelefono As Label
        Friend WithEvents txtTelefono As TextBox
        Friend WithEvents lblCorreo As Label
        Friend WithEvents txtCorreo As TextBox
        Friend WithEvents lblPassword As Label
        Friend WithEvents txtPassword As TextBox
        Friend WithEvents btnRegistrar As Button
        Friend WithEvents btnVolverLogin As Button
    End Class
End Namespace
