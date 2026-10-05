Imports System

Namespace Models
    ''' <summary>
    ''' Modelo de entidad orientado a objetos que representa a un usuario del sistema (POO).
    ''' Encapsula credenciales, rol fuertemente tipado y reglas de autorización.
    ''' </summary>
    Public Class UsuarioModel

        Public Property Nombre As String
        Public Property Correo As String
        Public Property Password As String
        Public Property Telefono As String
        Public Property Rol As RolUsuarioEnum
        Public Property FechaRegistro As DateTime

        Public Sub New()
            Me.Nombre = "Invitado"
            Me.Correo = String.Empty
            Me.Password = String.Empty
            Me.Telefono = String.Empty
            Me.Rol = RolUsuarioEnum.Cliente
            Me.FechaRegistro = DateTime.Now
        End Sub

        Public Sub New(nombre As String, correo As String, password As String, telefono As String, rol As RolUsuarioEnum)
            Me.Nombre = If(String.IsNullOrWhiteSpace(nombre), "Usuario", nombre.Trim())
            Me.Correo = If(String.IsNullOrWhiteSpace(correo), String.Empty, correo.Trim().ToLower())
            Me.Password = If(String.IsNullOrWhiteSpace(password), String.Empty, password.Trim())
            Me.Telefono = If(String.IsNullOrWhiteSpace(telefono), String.Empty, telefono.Trim())
            Me.Rol = rol
            Me.FechaRegistro = DateTime.Now
        End Sub

        Public Sub New(nombre As String, correo As String, password As String, telefono As String, rolTexto As String)
            Me.New(nombre, correo, password, telefono, RolUsuarioExtensions.ParsearRol(rolTexto))
        End Sub

        ''' <summary>
        ''' Valida si la contraseña provista coincide con la del usuario.
        ''' </summary>
        Public Function ValidarPassword(passwordInput As String) As Boolean
            If String.IsNullOrEmpty(passwordInput) Then Return False
            Return Me.Password.Equals(passwordInput.Trim())
        End Function

        ''' <summary>
        ''' Obtiene la inicial del nombre para mostrar en el avatar gráfico.
        ''' </summary>
        Public Function ObtenerInicial() As String
            If Not String.IsNullOrWhiteSpace(Me.Nombre) Then
                Return Char.ToUpper(Me.Nombre.Trim()(0)).ToString()
            End If
            Return "U"
        End Function

        ''' <summary>
        ''' Determina si el usuario pertenece al personal del restaurante (Staff).
        ''' </summary>
        Public Function EsPersonal() As Boolean
            Return RolUsuarioExtensions.EsPersonal(Me.Rol)
        End Function

        ''' <summary>
        ''' Determina si el usuario es un cliente de autoatención.
        ''' </summary>
        Public Function EsCliente() As Boolean
            Return RolUsuarioExtensions.EsCliente(Me.Rol)
        End Function

        ''' <summary>
        ''' Determina si el usuario posee privilegios administrativos completos.
        ''' </summary>
        Public Function EsAdministrador() As Boolean
            Return Me.Rol = RolUsuarioEnum.Administrador
        End Function

        ''' <summary>
        ''' Retorna el nombre formateado del rol para visualización en pantalla.
        ''' </summary>
        Public Function ObtenerEtiquetaRol() As String
            Return RolUsuarioExtensions.ObtenerEtiqueta(Me.Rol)
        End Function

    End Class
End Namespace
