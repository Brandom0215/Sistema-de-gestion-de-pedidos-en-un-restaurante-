Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Auth
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmLogin
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
            Me.btnSalir = New System.Windows.Forms.Button()

            Me.pnlCardLogin.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlCardLogin
            Me.pnlCardLogin.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.pnlCardLogin.BackColor = System.Drawing.Color.White
            Me.pnlCardLogin.Controls.Add(Me.btnSalir)
            Me.pnlCardLogin.Controls.Add(Me.btnIniciarSesion)
            Me.pnlCardLogin.Controls.Add(Me.txtPassword)
            Me.pnlCardLogin.Controls.Add(Me.lblPassword)
            Me.pnlCardLogin.Controls.Add(Me.txtUsuario)
            Me.pnlCardLogin.Controls.Add(Me.lblUsuario)
            Me.pnlCardLogin.Controls.Add(Me.lblSubtituloLogin)
            Me.pnlCardLogin.Controls.Add(Me.lblTituloLogin)
            Me.pnlCardLogin.Controls.Add(Me.lblLogoEmpresa)
            Me.pnlCardLogin.Location = New System.Drawing.Point(25, 20)
            Me.pnlCardLogin.Name = "pnlCardLogin"
            Me.pnlCardLogin.Padding = New System.Windows.Forms.Padding(30)
            Me.pnlCardLogin.Size = New System.Drawing.Size(500, 460)
            Me.pnlCardLogin.TabIndex = 0
            '
            ' lblLogoEmpresa
            '
            Me.lblLogoEmpresa.AutoSize = True
            Me.lblLogoEmpresa.Font = New System.Drawing.Font("Segoe UI", 30.0F)
            Me.lblLogoEmpresa.Location = New System.Drawing.Point(220, 10)
            Me.lblLogoEmpresa.Name = "lblLogoEmpresa"
            Me.lblLogoEmpresa.Size = New System.Drawing.Size(60, 54)
            Me.lblLogoEmpresa.TabIndex = 0
            Me.lblLogoEmpresa.Text = "🍷"
            Me.lblLogoEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTituloLogin
            '
            Me.lblTituloLogin.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloLogin.Location = New System.Drawing.Point(25, 65)
            Me.lblTituloLogin.Name = "lblTituloLogin"
            Me.lblTituloLogin.Size = New System.Drawing.Size(450, 34)
            Me.lblTituloLogin.TabIndex = 1
            Me.lblTituloLogin.Text = "Restaurante El Buen Sazón"
            Me.lblTituloLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblSubtituloLogin
            '
            Me.lblSubtituloLogin.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblSubtituloLogin.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloLogin.Location = New System.Drawing.Point(25, 102)
            Me.lblSubtituloLogin.Name = "lblSubtituloLogin"
            Me.lblSubtituloLogin.Size = New System.Drawing.Size(450, 22)
            Me.lblSubtituloLogin.TabIndex = 2
            Me.lblSubtituloLogin.Text = "Acceso Exclusivo para Personal (Cocina, Caja, Admin)"
            Me.lblSubtituloLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblUsuario
            '
            Me.lblUsuario.AutoSize = True
            Me.lblUsuario.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblUsuario.Location = New System.Drawing.Point(30, 142)
            Me.lblUsuario.Name = "lblUsuario"
            Me.lblUsuario.Size = New System.Drawing.Size(125, 19)
            Me.lblUsuario.TabIndex = 3
            Me.lblUsuario.Text = "Usuario / Correo:"
            '
            ' txtUsuario
            '
            Me.txtUsuario.Font = New System.Drawing.Font("Segoe UI", 12.0F)
            Me.txtUsuario.Location = New System.Drawing.Point(30, 164)
            Me.txtUsuario.Name = "txtUsuario"
            Me.txtUsuario.Size = New System.Drawing.Size(440, 29)
            Me.txtUsuario.TabIndex = 4
            '
            ' lblPassword (AMPLIA SEPARACIÓN MARGEN VERTICAL DE 32px)
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblPassword.Location = New System.Drawing.Point(30, 226)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(91, 19)
            Me.lblPassword.TabIndex = 5
            Me.lblPassword.Text = "Contraseña:"
            '
            ' txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 12.0F)
            Me.txtPassword.Location = New System.Drawing.Point(30, 248)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPassword.Size = New System.Drawing.Size(440, 29)
            Me.txtPassword.TabIndex = 6
            Me.txtPassword.UseSystemPasswordChar = True
            '
            ' btnIniciarSesion (BOTÓN PRIMARIO TÁCTIL ALTURA 52px)
            '
            Me.btnIniciarSesion.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.btnIniciarSesion.Location = New System.Drawing.Point(30, 314)
            Me.btnIniciarSesion.Name = "btnIniciarSesion"
            Me.btnIniciarSesion.Size = New System.Drawing.Size(440, 52)
            Me.btnIniciarSesion.TabIndex = 7
            Me.btnIniciarSesion.Text = "Iniciar Sesión"
            Me.btnIniciarSesion.UseVisualStyleBackColor = True
            '
            ' btnSalir (BOTÓN SECUNDARIO TÁCTIL ALTURA 44px)
            '
            Me.btnSalir.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.btnSalir.Location = New System.Drawing.Point(30, 380)
            Me.btnSalir.Name = "btnSalir"
            Me.btnSalir.Size = New System.Drawing.Size(440, 44)
            Me.btnSalir.TabIndex = 8
            Me.btnSalir.Text = "Volver al Menú Principal"
            Me.btnSalir.UseVisualStyleBackColor = True
            '
            ' FrmLogin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(550, 500)
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
        Friend WithEvents btnSalir As Button
    End Class
End Namespace
