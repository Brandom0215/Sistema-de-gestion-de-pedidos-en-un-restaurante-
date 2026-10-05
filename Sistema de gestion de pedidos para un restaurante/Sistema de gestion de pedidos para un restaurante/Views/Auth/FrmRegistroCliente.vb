Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Auth
    ''' <summary>
    ''' Formulario de Registro para Clientes del Restaurante.
    ''' Permite capturar Nombre, Teléfono, Correo/Usuario y Contraseña en RestauranteDB.
    ''' </summary>
    Public Class FrmRegistroCliente

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmRegistroCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
        End Sub

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCardRegistro.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlCardRegistro)

            lblTituloRegistro.ForeColor = ThemeConfig.ColorSecondary
            lblSubtituloRegistro.ForeColor = ThemeConfig.ColorTextMuted
            lblNombreCompleto.ForeColor = ThemeConfig.ColorNeutralDark
            lblTelefono.ForeColor = ThemeConfig.ColorNeutralDark
            lblCorreo.ForeColor = ThemeConfig.ColorNeutralDark
            lblPassword.ForeColor = ThemeConfig.ColorNeutralDark

            ThemeConfig.EstilizarBotonPrimario(btnRegistrar)
            ThemeConfig.EstilizarBotonSecundario(btnVolverLogin)
        End Sub

        Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
            If String.IsNullOrWhiteSpace(txtNombreCompleto.Text) Then
                MessageBox.Show("Por favor, ingrese su nombre completo.", "Registro de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNombreCompleto.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtCorreo.Text) Then
                MessageBox.Show("Por favor, ingrese su correo electrónico o usuario.", "Registro de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCorreo.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                MessageBox.Show("Por favor, cree una contraseña de acceso.", "Registro de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Return
            End If

            Dim nombreCliente As String = txtNombreCompleto.Text.Trim()
            Dim telefonoCliente As String = txtTelefono.Text.Trim()
            Dim correoCliente As String = txtCorreo.Text.Trim()
            Dim passwordCliente As String = txtPassword.Text.Trim()

            Dim exito As Boolean = Data.UsuarioDAO.RegistrarCliente(nombreCliente, telefonoCliente, correoCliente, passwordCliente)
            If Not exito Then
                MessageBox.Show("No se pudo registrar la cuenta. Es posible que el correo/usuario ya exista en RestauranteDB.", "Error de Registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            MessageBox.Show($"¡Bienvenido {nombreCliente}! Tu cuenta ha sido registrada con éxito en RestauranteDB.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Abrir FrmHome en Modo Cliente pasando sus datos
            Dim mainHome As New FrmHome("📲 Cliente / Autoatención", nombreCliente)
            Me.Hide()
            mainHome.ShowDialog()
            Me.Close()
        End Sub


        Private Sub btnVolverLogin_Click(sender As Object, e As EventArgs) Handles btnVolverLogin.Click
            Dim frmLog As New FrmLogin()
            Me.Hide()
            frmLog.ShowDialog()
            Me.Close()
        End Sub

    End Class
End Namespace
