Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Auth
    ''' <summary>
    ''' Formulario Profesional de Autenticación e Inicio de Sesión.
    ''' Valida credenciales del usuario e infiere automáticamente su rol de permisos.
    ''' </summary>
    Public Class FrmLogin

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            txtUsuario.Text = ""
            txtPassword.Text = ""
        End Sub

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCardLogin.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlCardLogin)

            lblTituloLogin.ForeColor = ThemeConfig.ColorSecondary
            lblSubtituloLogin.ForeColor = ThemeConfig.ColorTextMuted
            lblUsuario.ForeColor = ThemeConfig.ColorNeutralDark
            lblPassword.ForeColor = ThemeConfig.ColorNeutralDark

            ThemeConfig.EstilizarBotonPrimario(btnIniciarSesion)
            ThemeConfig.EstilizarBotonSecundario(btnIrARegistro)
            ThemeConfig.EstilizarBotonSecundario(btnSalir)
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

            ' Validar credenciales mediante UsuarioDAO
            Dim rolDeterminado As String = "👑 Administrador"
            Dim autenticado As Boolean = Data.UsuarioDAO.Autenticar(usuarioInput, passwordInput, rolDeterminado)

            If Not autenticado Then
                MessageBox.Show("Credenciales incorrectas. Verifique su usuario y contraseña.", "Autenticación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Abrir FrmHome con el rol y usuario autenticado
            Dim mainHome As New FrmHome(rolDeterminado, usuarioInput)
            Me.Hide()
            mainHome.ShowDialog()
            Me.Close()
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
