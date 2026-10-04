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
            Me.lblCredencialesDemo = New System.Windows.Forms.Label()
            Me.flpCredencialesDemo = New System.Windows.Forms.FlowLayoutPanel()
            Me.btnDemoAdmin = New System.Windows.Forms.Button()
            Me.btnDemoCajero = New System.Windows.Forms.Button()
            Me.btnDemoCocina = New System.Windows.Forms.Button()
            Me.btnDemoCliente = New System.Windows.Forms.Button()
            Me.btnIniciarSesion = New System.Windows.Forms.Button()
            Me.btnIrARegistro = New System.Windows.Forms.Button()
            Me.btnSalir = New System.Windows.Forms.Button()

            Me.pnlCardLogin.SuspendLayout()
            Me.flpCredencialesDemo.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlCardLogin
            '
            Me.pnlCardLogin.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.pnlCardLogin.BackColor = System.Drawing.Color.White
            Me.pnlCardLogin.Controls.Add(Me.btnSalir)
            Me.pnlCardLogin.Controls.Add(Me.btnIrARegistro)
            Me.pnlCardLogin.Controls.Add(Me.btnIniciarSesion)
            Me.pnlCardLogin.Controls.Add(Me.flpCredencialesDemo)
            Me.pnlCardLogin.Controls.Add(Me.lblCredencialesDemo)
            Me.pnlCardLogin.Controls.Add(Me.txtPassword)
            Me.pnlCardLogin.Controls.Add(Me.lblPassword)
            Me.pnlCardLogin.Controls.Add(Me.txtUsuario)
            Me.pnlCardLogin.Controls.Add(Me.lblUsuario)
            Me.pnlCardLogin.Controls.Add(Me.lblSubtituloLogin)
            Me.pnlCardLogin.Controls.Add(Me.lblTituloLogin)
            Me.pnlCardLogin.Controls.Add(Me.lblLogoEmpresa)
            Me.pnlCardLogin.Location = New System.Drawing.Point(50, 20)
            Me.pnlCardLogin.Name = "pnlCardLogin"
            Me.pnlCardLogin.Padding = New System.Windows.Forms.Padding(30)
            Me.pnlCardLogin.Size = New System.Drawing.Size(450, 550)
            Me.pnlCardLogin.TabIndex = 0
            '
            ' lblLogoEmpresa
            '
            Me.lblLogoEmpresa.AutoSize = True
            Me.lblLogoEmpresa.Font = New System.Drawing.Font("Segoe UI", 28.0F)
            Me.lblLogoEmpresa.Location = New System.Drawing.Point(195, 15)
            Me.lblLogoEmpresa.Name = "lblLogoEmpresa"
            Me.lblLogoEmpresa.Size = New System.Drawing.Size(60, 51)
            Me.lblLogoEmpresa.TabIndex = 0
            Me.lblLogoEmpresa.Text = "🍷"
            Me.lblLogoEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            ' lblTituloLogin
            '
            Me.lblTituloLogin.Font = New System.Drawing.Font("Segoe UI", 15.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloLogin.Location = New System.Drawing.Point(30, 68)
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
            Me.lblSubtituloLogin.Location = New System.Drawing.Point(30, 96)
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
            Me.lblUsuario.Location = New System.Drawing.Point(30, 130)
            Me.lblUsuario.Name = "lblUsuario"
            Me.lblUsuario.Size = New System.Drawing.Size(135, 15)
            Me.lblUsuario.TabIndex = 3
            Me.lblUsuario.Text = "Usuario / Correo:"
            '
            ' txtUsuario
            '
            Me.txtUsuario.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtUsuario.Location = New System.Drawing.Point(30, 148)
            Me.txtUsuario.Name = "txtUsuario"
            Me.txtUsuario.Size = New System.Drawing.Size(390, 24)
            Me.txtUsuario.TabIndex = 4
            '
            ' lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblPassword.Location = New System.Drawing.Point(30, 182)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(72, 15)
            Me.lblPassword.TabIndex = 5
            Me.lblPassword.Text = "Contraseña:"
            '
            ' txtPassword
            '
            Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.txtPassword.Location = New System.Drawing.Point(30, 200)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPassword.Size = New System.Drawing.Size(390, 24)
            Me.txtPassword.TabIndex = 6
            Me.txtPassword.UseSystemPasswordChar = True
            '
            ' lblCredencialesDemo
            '
            Me.lblCredencialesDemo.AutoSize = True
            Me.lblCredencialesDemo.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold)
            Me.lblCredencialesDemo.ForeColor = System.Drawing.Color.Gray
            Me.lblCredencialesDemo.Location = New System.Drawing.Point(30, 236)
            Me.lblCredencialesDemo.Name = "lblCredencialesDemo"
            Me.lblCredencialesDemo.Size = New System.Drawing.Size(260, 15)
            Me.lblCredencialesDemo.TabIndex = 7
            Me.lblCredencialesDemo.Text = "Credenciales de prueba (clic para auto-rellenar):"
            '
            ' flpCredencialesDemo
            '
            Me.flpCredencialesDemo.Controls.Add(Me.btnDemoAdmin)
            Me.flpCredencialesDemo.Controls.Add(Me.btnDemoCajero)
            Me.flpCredencialesDemo.Controls.Add(Me.btnDemoCocina)
            Me.flpCredencialesDemo.Controls.Add(Me.btnDemoCliente)
            Me.flpCredencialesDemo.Location = New System.Drawing.Point(30, 255)
            Me.flpCredencialesDemo.Name = "flpCredencialesDemo"
            Me.flpCredencialesDemo.Size = New System.Drawing.Size(390, 42)
            Me.flpCredencialesDemo.TabIndex = 8
            '
            ' btnDemoAdmin
            '
            Me.btnDemoAdmin.Font = New System.Drawing.Font("Segoe UI", 8.2F)
            Me.btnDemoAdmin.Location = New System.Drawing.Point(0, 0)
            Me.btnDemoAdmin.Margin = New System.Windows.Forms.Padding(0, 0, 6, 0)
            Me.btnDemoAdmin.Name = "btnDemoAdmin"
            Me.btnDemoAdmin.Size = New System.Drawing.Size(90, 32)
            Me.btnDemoAdmin.TabIndex = 0
            Me.btnDemoAdmin.Text = "👑 Admin"
            Me.btnDemoAdmin.UseVisualStyleBackColor = True
            '
            ' btnDemoCajero
            '
            Me.btnDemoCajero.Font = New System.Drawing.Font("Segoe UI", 8.2F)
            Me.btnDemoCajero.Location = New System.Drawing.Point(96, 0)
            Me.btnDemoCajero.Margin = New System.Windows.Forms.Padding(0, 0, 6, 0)
            Me.btnDemoCajero.Name = "btnDemoCajero"
            Me.btnDemoCajero.Size = New System.Drawing.Size(90, 32)
            Me.btnDemoCajero.TabIndex = 1
            Me.btnDemoCajero.Text = "💵 Cajero"
            Me.btnDemoCajero.UseVisualStyleBackColor = True
            '
            ' btnDemoCocina
            '
            Me.btnDemoCocina.Font = New System.Drawing.Font("Segoe UI", 8.2F)
            Me.btnDemoCocina.Location = New System.Drawing.Point(192, 0)
            Me.btnDemoCocina.Margin = New System.Windows.Forms.Padding(0, 0, 6, 0)
            Me.btnDemoCocina.Name = "btnDemoCocina"
            Me.btnDemoCocina.Size = New System.Drawing.Size(90, 32)
            Me.btnDemoCocina.TabIndex = 2
            Me.btnDemoCocina.Text = "🍳 Cocina"
            Me.btnDemoCocina.UseVisualStyleBackColor = True
            '
            ' btnDemoCliente
            '
            Me.btnDemoCliente.Font = New System.Drawing.Font("Segoe UI", 8.2F)
            Me.btnDemoCliente.Location = New System.Drawing.Point(288, 0)
            Me.btnDemoCliente.Margin = New System.Windows.Forms.Padding(0)
            Me.btnDemoCliente.Name = "btnDemoCliente"
            Me.btnDemoCliente.Size = New System.Drawing.Size(95, 32)
            Me.btnDemoCliente.TabIndex = 3
            Me.btnDemoCliente.Text = "📲 Cliente"
            Me.btnDemoCliente.UseVisualStyleBackColor = True
            '
            ' btnIniciarSesion
            '
            Me.btnIniciarSesion.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnIniciarSesion.Location = New System.Drawing.Point(30, 310)
            Me.btnIniciarSesion.Name = "btnIniciarSesion"
            Me.btnIniciarSesion.Size = New System.Drawing.Size(390, 42)
            Me.btnIniciarSesion.TabIndex = 9
            Me.btnIniciarSesion.Text = "🔓 Iniciar Sesión"
            Me.btnIniciarSesion.UseVisualStyleBackColor = True
            '
            ' btnIrARegistro
            '
            Me.btnIrARegistro.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.btnIrARegistro.Location = New System.Drawing.Point(30, 365)
            Me.btnIrARegistro.Name = "btnIrARegistro"
            Me.btnIrARegistro.Size = New System.Drawing.Size(390, 36)
            Me.btnIrARegistro.TabIndex = 10
            Me.btnIrARegistro.Text = "📝 ¿No tienes cuenta? Regístrate aquí"
            Me.btnIrARegistro.UseVisualStyleBackColor = True
            '
            ' btnSalir
            '
            Me.btnSalir.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.btnSalir.ForeColor = System.Drawing.Color.DarkGray
            Me.btnSalir.Location = New System.Drawing.Point(30, 415)
            Me.btnSalir.Name = "btnSalir"
            Me.btnSalir.Size = New System.Drawing.Size(390, 32)
            Me.btnSalir.TabIndex = 11
            Me.btnSalir.Text = "❌ Salir de la Aplicación"
            Me.btnSalir.UseVisualStyleBackColor = True
            '
            ' FrmLogin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.ClientSize = New System.Drawing.Size(550, 590)
            Me.Controls.Add(Me.pnlCardLogin)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.Name = "FrmLogin"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Autenticación — Sistema de Gestión de Pedidos"
            Me.pnlCardLogin.ResumeLayout(False)
            Me.pnlCardLogin.PerformLayout()
            Me.flpCredencialesDemo.ResumeLayout(False)
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
        Friend WithEvents lblCredencialesDemo As Label
        Friend WithEvents flpCredencialesDemo As FlowLayoutPanel
        Friend WithEvents btnDemoAdmin As Button
        Friend WithEvents btnDemoCajero As Button
        Friend WithEvents btnDemoCocina As Button
        Friend WithEvents btnDemoCliente As Button
        Friend WithEvents btnIniciarSesion As Button
        Friend WithEvents btnIrARegistro As Button
        Friend WithEvents btnSalir As Button
    End Class
End Namespace
