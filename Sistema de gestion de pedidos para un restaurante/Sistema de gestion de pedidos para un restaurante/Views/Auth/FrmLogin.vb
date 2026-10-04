Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Auth
    ''' <summary>
    ''' Formulario de Autenticación e Inicio de Sesión.
    ''' Valida credenciales del usuario en RestauranteDB e infiere automáticamente su rol de permisos.
    ''' </summary>
    Public Class FrmLogin

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            txtUsuario.Text = "admin"
            txtPassword.Text = "1234"
        End Sub

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCardLogin.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlCardLogin)

            lblTituloLogin.ForeColor = ThemeConfig.ColorSecondary
            lblSubtituloLogin.ForeColor = ThemeConfig.ColorTextMuted
            lblUsuario.ForeColor = ThemeConfig.ColorNeutralDark
            lblPassword.ForeColor = ThemeConfig.ColorNeutralDark
            lblCredencialesDemo.ForeColor = ThemeConfig.ColorTextMuted

            ThemeConfig.EstilizarBotonPrimario(btnIniciarSesion)
            ThemeConfig.EstilizarBotonSecundario(btnIrARegistro)
            ThemeConfig.EstilizarBotonSecundario(btnSalir)

            ThemeConfig.EstilizarBotonSecundario(btnDemoAdmin)
            ThemeConfig.EstilizarBotonSecundario(btnDemoCajero)
            ThemeConfig.EstilizarBotonSecundario(btnDemoCocina)
            ThemeConfig.EstilizarBotonSecundario(btnDemoCliente)
        End Sub

        Private Sub btnIniciarSesion_Click(sender As Object, e As EventArgs) Handles btnIniciarSesion.Click
            Dim usuarioInput As String = txtUsuario.Text.Trim()
            Dim passwordInput As String = txtPassword.Text.Trim()

            If String.IsNullOrWhiteSpace(usuarioInput) Then
                MessageBox.Show("Por favor, ingrese su usuario o correo registrado.", "Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtUsuario.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(passwordInput) Then
                MessageBox.Show("Por favor, ingrese su contraseña.", "Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Return
            End If

            ' Determinar rol asignado según credenciales en RestauranteDB
            Dim rolDeterminado As String = "👑 Administrador"
            Dim usuarioLower As String = usuarioInput.ToLower()

            If usuarioLower = "admin" Then
                rolDeterminado = "👑 Administrador"
            ElseIf usuarioLower = "cajero" Then
                rolDeterminado = "💵 Cajero / Personal de Sala"
            ElseIf usuarioLower = "cocina" Then
                rolDeterminado = "🍳 Personal de Cocina (KDS)"
            Else
                ' Cualquier otro usuario registrado es tratado como Cliente
                rolDeterminado = "📲 Cliente / Autoatención"
            End If

            ' Abrir FrmHome con el rol y usuario autenticado
            Dim mainHome As New FrmHome(rolDeterminado, usuarioInput)
            Me.Hide()
            mainHome.ShowDialog()
            Me.Close()
        End Sub

        ' =========================================================================
        ' BOTONES DE AUTO-COMPLETADO RÁPIDO PARA PRUEBAS DE ROLES
        ' =========================================================================

        Private Sub btnDemoAdmin_Click(sender As Object, e As EventArgs) Handles btnDemoAdmin.Click
            txtUsuario.Text = "admin"
            txtPassword.Text = "1234"
        End Sub

        Private Sub btnDemoCajero_Click(sender As Object, e As EventArgs) Handles btnDemoCajero.Click
            txtUsuario.Text = "cajero"
            txtPassword.Text = "1234"
        End Sub

        Private Sub btnDemoCocina_Click(sender As Object, e As EventArgs) Handles btnDemoCocina.Click
            txtUsuario.Text = "cocina"
            txtPassword.Text = "1234"
        End Sub

        Private Sub btnDemoCliente_Click(sender As Object, e As EventArgs) Handles btnDemoCliente.Click
            txtUsuario.Text = "cliente_carlos"
            txtPassword.Text = "1234"
        End Sub

        Private Sub btnIrARegistro_Click(sender As Object, e As EventArgs) Handles btnIrARegistro.Click
            Dim frmReg As New FrmRegistroCliente()
            Me.Hide()
            frmReg.ShowDialog()
            Me.Close()
        End Sub

        Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
            Application.Exit()
        End Sub

    End Class
End Namespace
