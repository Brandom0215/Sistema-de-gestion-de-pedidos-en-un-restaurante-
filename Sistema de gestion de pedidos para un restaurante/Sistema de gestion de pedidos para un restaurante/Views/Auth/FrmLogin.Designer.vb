Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Auth
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmLogin
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
            Me.pnlCardLogin = New System.Windows.Forms.Panel()
            Me.lblLogoEmpresa = New System.Windows.Forms.Label()
            Me.lblTituloLogin = New System.Windows.Forms.Label()
            Me.lblSubtituloLogin = New System.Windows.Forms.Label()
            Me.lblUsuario = New System.Windows.Forms.Label()
            Me.txtUsuario = New System.Windows.Forms.TextBox()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.btnIniciarSesion = New System.Windows.Forms.Button()
            Me.btnIrARegistro = New System.Windows.Forms.Button()
            Me.btnIngresarInvitado = New System.Windows.Forms.Button()
            Me.btnSalir = New System.Windows.Forms.Button()

            Me.pnlCardLogin.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlCardLogin
            '
            Me.pnlCardLogin.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.pnlCardLogin.BackColor = System.Drawing.Color.White
            Me.pnlCardLogin.Controls.Add(Me.btnSalir)
            Me.pnlCardLogin.Controls.Add(Me.btnIngresarInvitado)
            Me.pnlCardLogin.Controls.Add(Me.btnIrARegistro)
            Me.pnlCardLogin.Controls.Add(Me.btnIniciarSesion)
            Me.pnlCardLogin.Controls.Add(Me.txtPassword)
            Me.pnlCardLogin.Controls.Add(Me.lblPassword)
            Me.pnlCardLogin.Controls.Add(Me.txtUsuario)
            Me.pnlCardLogin.Controls.Add(Me.lblUsuario)
            Me.pnlCardLogin.Controls.Add(Me.lblSubtituloLogin)
            Me.pnlCardLogin.Controls.Add(Me.lblTituloLogin)
            Me.pnlCardLogin.Controls.Add(Me.lblLogoEmpresa)
            Me.pnlCardLogin.Location = New System.Drawing.Point(40, 15)
            Me.pnlCardLogin.Name = "pnlCardLogin"
            Me.pnlCardLogin.Padding = New System.Windows.Forms.Padding(30)
            Me.pnlCardLogin.Size = New System.Drawing.Size(440, 480)
            Me.pnlCardLogin.TabIndex = 0
            '
            ' lblLogoEmpresa
            '
            Me.lblLogoEmpresa.AutoSize = True
            Me.lblLogoEmpresa.Font = New System.Drawing.Font("Segoe UI", 28.0F)
            Me.lblLogoEmpresa.Location = New System.Drawing.Point(190, 10)
            Me.lblLogoEmpresa.Name = "lblLogoEmpresa"
            Me.lblLogoEmpresa.Size = New System.Drawing.Size(60, 51)
            Me.lblLogoEmpresa.TabIndex = 0
            Me.lblLogoEmpresa.Text = "🍷"
            Me.lblLogoEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTituloLogin
            '
            Me.lblTituloLogin.Font = New System.Drawing.Font("Segoe UI", 15.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloLogin.Location = New System.Drawing.Point(25, 62)
            Me.lblTituloLogin.Name = "lblTituloLogin"
            Me.lblTituloLogin.Size = New System.Drawing.Size(390, 28)
            Me.lblTituloLogin.TabIndex = 1
            Me.lblTituloLogin.Text = "Restaurante El Buen Sazón"
            Me.lblTituloLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblSubtituloLogin
            '
            Me.lblSubtituloLogin.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloLogin.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloLogin.Location = New System.Drawing.Point(25, 92)
            Me.lblSubtituloLogin.Name = "lblSubtituloLogin"
            Me.lblSubtituloLogin.Size = New System.Drawing.Size(390, 20)
            Me.lblSubtituloLogin.TabIndex = 2
            Me.lblSubtituloLogin.Text = "Sistema de Gestión de Pedidos y Autenticación"
            Me.lblSubtituloLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblUsuario
            '
            Me.lblUsuario.AutoSize = True
            Me.lblUsuario.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblUsuario.Location = New System.Drawing.Point(25, 130)
            Me.lblUsuario.Name = "lblUsuario"
            Me.lblUsuario.Size = New System.Drawing.Size(135, 15)
            Me.lblUsuario.TabIndex = 3
            Me.lblUsuario.Text = "Usuario / Correo:"
            '
            ' txtUsuario
            '
            Me.txtUsuario.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtUsuario.Location = New System.Drawing.Point(25, 148)
            Me.txtUsuario.Name = "txtUsuario"
            Me.txtUsuario.Size = New System.Drawing.Size(390, 26)
            Me.txtUsuario.TabIndex = 4
            '
            ' lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblPassword.Location = New System.Drawing.Point(25, 207)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(72, 15)
            Me.lblPassword.TabIndex = 5
            Me.lblPassword.Text = "Contraseña:"
            '
            ' txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtPassword.Location = New System.Drawing.Point(25, 225)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPassword.Size = New System.Drawing.Size(390, 26)
            Me.txtPassword.TabIndex = 6
            Me.txtPassword.UseSystemPasswordChar = True
            '
            ' btnIniciarSesion
            '
            Me.btnIniciarSesion.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnIniciarSesion.Location = New System.Drawing.Point(25, 280)
            Me.btnIniciarSesion.Name = "btnIniciarSesion"
            Me.btnIniciarSesion.Size = New System.Drawing.Size(390, 40)
            Me.btnIniciarSesion.TabIndex = 7
            Me.btnIniciarSesion.Text = "Iniciar Sesión"
            Me.btnIniciarSesion.UseVisualStyleBackColor = True
            '
            ' btnIrARegistro
            '
            Me.btnIrARegistro.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.btnIrARegistro.Location = New System.Drawing.Point(25, 328)
            Me.btnIrARegistro.Name = "btnIrARegistro"
            Me.btnIrARegistro.Size = New System.Drawing.Size(390, 34)
            Me.btnIrARegistro.TabIndex = 8
            Me.btnIrARegistro.Text = "¿No tienes cuenta? Regístrate aquí"
            Me.btnIrARegistro.UseVisualStyleBackColor = True
            '
            ' btnIngresarInvitado
            '
            Me.btnIngresarInvitado.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnIngresarInvitado.Location = New System.Drawing.Point(25, 370)
            Me.btnIngresarInvitado.Name = "btnIngresarInvitado"
            Me.btnIngresarInvitado.Size = New System.Drawing.Size(390, 34)
            Me.btnIngresarInvitado.TabIndex = 9
            Me.btnIngresarInvitado.Text = "Explorar Menú como Invitado"
            Me.btnIngresarInvitado.UseVisualStyleBackColor = True
            '
            ' btnSalir
            '
            Me.btnSalir.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.btnSalir.ForeColor = System.Drawing.Color.DarkGray
            Me.btnSalir.Location = New System.Drawing.Point(25, 412)
            Me.btnSalir.Name = "btnSalir"
            Me.btnSalir.Size = New System.Drawing.Size(390, 30)
            Me.btnSalir.TabIndex = 10
            Me.btnSalir.Text = "Salir de la Aplicación"
            Me.btnSalir.UseVisualStyleBackColor = True
            '
            ' FrmLogin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(520, 510)
            Me.Controls.Add(Me.pnlCardLogin)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "FrmLogin"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Autenticación — Sistema de Gestión de Pedidos"
            Me.pnlCardLogin.ResumeLayout(False)
            Me.pnlCardLogin.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCardLogin As Panel
        Friend WithEvents lblLogoEmpresa As Label
        Friend WithEvents lblTituloLogin As Label
        Friend WithEvents lblSubtituloLogin As Label
        Friend WithEvents lblUsuario As Label
        Friend WithEvents txtUsuario As TextBox
        Friend WithEvents lblPassword As Label
        Friend WithEvents txtPassword As TextBox
        Friend WithEvents btnIniciarSesion As Button
        Friend WithEvents btnIrARegistro As Button
        Friend WithEvents btnIngresarInvitado As Button
        Friend WithEvents btnSalir As Button
    End Class
End Namespace
