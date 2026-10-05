Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Auth
    ''' <summary>
    ''' Formulario Profesional de Autenticación e Inicio de Sesión.
    ''' Valida credenciales del usuario e infiere automáticamente su rol de permisos.
    ''' </summary>
    Public Class FrmLogin

        ''' <summary> Nombre de usuario autenticado exitosamente en el diálogo </summary>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Property UsuarioAutenticado As String = String.Empty

        ''' <summary> Rol de permisos obtenido tras la autenticación </summary>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Property RolAutenticado As String = String.Empty

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            txtUsuario.Text = ""
            txtPassword.Text = ""
        End Sub

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlCardLogin.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlCardLogin)

            lblTituloLogin.ForeColor = ThemeConfig.ColorSecondary
            lblSubtituloLogin.ForeColor = ThemeConfig.ColorTextMuted
            lblUsuario.ForeColor = ThemeConfig.ColorNeutralDark
            lblPassword.ForeColor = ThemeConfig.ColorNeutralDark

            ThemeConfig.EstilizarBotonPrimario(btnIniciarSesion)
            ThemeConfig.EstilizarBotonSecundario(btnSalir)
        End Sub

        Private Sub btnIniciarSesion_Click(sender As Object, e As EventArgs) Handles btnIniciarSesion.Click
            Dim usuarioInput As String = txtUsuario.Text.Trim()
            Dim passwordInput As String = txtPassword.Text.Trim()

            If String.IsNullOrWhiteSpace(usuarioInput) Then
                MostrarMensajeAdvertencia("Por favor, ingrese su usuario o correo registrado.", "Autenticación")
                txtUsuario.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(passwordInput) Then
                MostrarMensajeAdvertencia("Por favor, ingrese su contraseña.", "Autenticación")
                txtPassword.Focus()
                Return
            End If

            ' Validar credenciales mediante UsuarioDAO / UsuarioService
            Dim rolDeterminado As String = "Administrador"
            Dim autenticado As Boolean = Data.UsuarioDAO.Autenticar(usuarioInput, passwordInput, rolDeterminado)

            If Not autenticado Then
                MostrarMensajeError("Credenciales incorrectas. Verifique su usuario y contraseña.", "Autenticación Fallida")
                Return
            End If

            UsuarioAutenticado = usuarioInput
            RolAutenticado = rolDeterminado

            If Me.Modal Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim mainHome As New FrmHome(rolDeterminado, usuarioInput)
                Me.Hide()
                mainHome.ShowDialog()
                Me.Close()
            End If
        End Sub

        Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
            If Me.Modal Then
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            Else
                Me.Close()
            End If
        End Sub

    End Class
End Namespace
