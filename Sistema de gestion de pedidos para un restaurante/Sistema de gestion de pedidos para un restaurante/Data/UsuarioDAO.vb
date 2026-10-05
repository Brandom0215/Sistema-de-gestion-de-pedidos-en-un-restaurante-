Imports System
Imports System.Collections.Generic

Namespace Data
    ''' <summary>
    ''' Estructura de modelo de usuario en memoria.
    ''' </summary>
    Public Class UsuarioItem
        Public Property Nombre As String
        Public Property Correo As String
        Public Property Password As String
        Public Property Telefono As String
        Public Property Rol As String

        Public Sub New(nombre As String, correo As String, password As String, telefono As String, rol As String)
            Me.Nombre = nombre
            Me.Correo = correo
            Me.Password = password
            Me.Telefono = telefono
            Me.Rol = rol
        End Sub
    End Class

    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) totalmente en memoria para autenticación y registro de usuarios.
    ''' No requiere base de datos ni servicios externos.
    ''' </summary>
    Public Module UsuarioDAO

        Private ReadOnly _usuariosEnMemoria As New List(Of UsuarioItem)()

        Sub New()
            ' Cargar usuarios preprogramados en el sistema
            _usuariosEnMemoria.Add(New UsuarioItem("Administrador Principal", "admin", "1234", "809-555-0101", "👑 Administrador"))
            _usuariosEnMemoria.Add(New UsuarioItem("Cajero de Turno", "cajero", "1234", "809-555-0102", "💵 Cajero / Personal de Sala"))
            _usuariosEnMemoria.Add(New UsuarioItem("Chef Ejecutivo", "cocina", "1234", "809-555-0103", "🍳 Personal de Cocina (KDS)"))
            _usuariosEnMemoria.Add(New UsuarioItem("Cliente Ejemplo", "cliente", "1234", "809-555-0104", "📲 Cliente / Autoatención"))
        End Sub

        ''' <summary>
        ''' Autentica un usuario por credenciales contra la lista en memoria.
        ''' </summary>
        Public Function Autenticar(usuarioInput As String, passwordInput As String, ByRef rolRetornado As String) As Boolean
            If String.IsNullOrWhiteSpace(usuarioInput) OrElse String.IsNullOrWhiteSpace(passwordInput) Then
                Return False
            End If

            Dim inputUsr As String = usuarioInput.Trim().ToLower()
            Dim inputPwd As String = passwordInput.Trim()

            For Each u As UsuarioItem In _usuariosEnMemoria
                If u.Correo.ToLower() = inputUsr AndAlso u.Password = inputPwd Then
                    rolRetornado = u.Rol
                    Return True
                End If
            Next

            Return False
        End Function

        ''' <summary>
        ''' Registra un nuevo cliente en la lista en memoria.
        ''' </summary>
        Public Function RegistrarCliente(nombre As String, telefono As String, correo As String, password As String) As Boolean
            If String.IsNullOrWhiteSpace(correo) OrElse String.IsNullOrWhiteSpace(password) Then
                Return False
            End If

            Dim nuevoCorreo As String = correo.Trim()
            For Each u As UsuarioItem In _usuariosEnMemoria
                If u.Correo.Equals(nuevoCorreo, StringComparison.OrdinalIgnoreCase) Then
                    Return False ' Correo ya registrado
                End If
            Next

            Dim nuevoCliente As New UsuarioItem(
                If(String.IsNullOrWhiteSpace(nombre), "Cliente", nombre.Trim()),
                nuevoCorreo,
                password.Trim(),
                If(String.IsNullOrWhiteSpace(telefono), "", telefono.Trim()),
                "📲 Cliente / Autoatención"
            )

            _usuariosEnMemoria.Add(nuevoCliente)
            Return True
        End Function

    End Module
End Namespace
