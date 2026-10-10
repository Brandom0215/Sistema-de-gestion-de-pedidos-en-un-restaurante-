Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Auth
    ''' <summary>
    ''' Formulario de Registro para Clientes del Restaurante.
    ''' Permite capturar Nombre, Teléfono, Correo/Usuario y Contraseña en RestauranteDB.
    ''' </summary>
    Public Class FrmRegistroCliente

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub FrmRegistroCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            ConfigurarValidacionesEntrada()
        End Sub

        ''' <summary>
        ''' Configura validaciones de entrada en tiempo real y límites de longitud para proteger la BD.
        ''' </summary>
        Private Sub ConfigurarValidacionesEntrada()
            ValidadorEntrada.ConfigurarCampoNombreCliente(txtNombreCompleto, 40)
            ValidadorEntrada.ConfigurarCampoTelefono(txtTelefono, 30)
            ValidadorEntrada.ConfigurarCampoCorreo(txtCorreo, 50)
            ValidadorEntrada.ConfigurarCampoPassword(txtPassword, 50)
        End Sub

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
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

        ''' <summary> Nombre del cliente registrado con éxito </summary>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Property NombreClienteRegistrado As String = String.Empty

        Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
            Dim nombreCliente As String = txtNombreCompleto.Text.Trim()
            If Not ValidadorEntrada.EsNombreClienteValido(nombreCliente) Then
                MostrarMensajeAdvertencia("Por favor, ingrese un nombre completo válido (entre 2 y 40 caracteres, solo letras).", "Registro de Cliente")
                txtNombreCompleto.Focus()
                Return
            End If

            Dim telefonoCliente As String = txtTelefono.Text.Trim()
            If Not ValidadorEntrada.EsTelefonoValido(telefonoCliente, False) Then
                MostrarMensajeAdvertencia("Por favor, ingrese un número de teléfono válido (entre 7 y 15 dígitos numéricos).", "Registro de Cliente")
                txtTelefono.Focus()
                Return
            End If

            Dim correoCliente As String = txtCorreo.Text.Trim()
            If Not ValidadorEntrada.EsCorreoValido(correoCliente) Then
                MostrarMensajeAdvertencia("Por favor, ingrese un correo electrónico válido (ej: usuario@gmail.com).", "Registro de Cliente")
                txtCorreo.Focus()
                Return
            End If

            Dim passwordCliente As String = txtPassword.Text.Trim()
            If Not ValidadorEntrada.EsPasswordValido(passwordCliente, 4, 50) Then
                MostrarMensajeAdvertencia("La contraseña debe contener al menos 4 caracteres.", "Registro de Cliente")
                txtPassword.Focus()
                Return
            End If

            Dim exito As Boolean = Data.UsuarioDAO.RegistrarCliente(nombreCliente, telefonoCliente, correoCliente, passwordCliente)
            If Not exito Then
                MostrarMensajeError("No se pudo registrar la cuenta. Es posible que el correo/usuario ya exista en el sistema.", "Error de Registro")
                Return
            End If

            MostrarMensajeExito($"¡Bienvenido {nombreCliente}! Tu cuenta ha sido registrada con éxito.", "Registro Exitoso")

            NombreClienteRegistrado = nombreCliente
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub btnVolverLogin_Click(sender As Object, e As EventArgs) Handles btnVolverLogin.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

    End Class
End Namespace
