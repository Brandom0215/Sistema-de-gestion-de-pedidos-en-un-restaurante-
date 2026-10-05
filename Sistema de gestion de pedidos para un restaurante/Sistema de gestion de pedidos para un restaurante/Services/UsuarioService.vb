Imports System
Imports System.Collections.Generic
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Implementación orientada a objetos del servicio de gestión de usuarios en memoria (POO).
    ''' </summary>
    Public Class UsuarioService
        Implements IUsuarioService

        Private ReadOnly _usuarios As New List(Of UsuarioModel)()

        Public Sub New()
            ' Cargar usuarios semilla del restaurante
            _usuarios.Add(New UsuarioModel("Administrador Principal", "admin", "1234", "809-555-0101", RolUsuarioEnum.Administrador))
            _usuarios.Add(New UsuarioModel("Cajero de Turno", "cajero", "1234", "809-555-0102", RolUsuarioEnum.Cajero))
            _usuarios.Add(New UsuarioModel("Chef Ejecutivo", "cocina", "1234", "809-555-0103", RolUsuarioEnum.Cocina))
            _usuarios.Add(New UsuarioModel("Cliente Ejemplo", "cliente", "1234", "809-555-0104", RolUsuarioEnum.Cliente))
        End Sub

        Public Function Autenticar(usuarioInput As String, passwordInput As String, ByRef rolRetornado As String) As Boolean Implements IUsuarioService.Autenticar
            If String.IsNullOrWhiteSpace(usuarioInput) OrElse String.IsNullOrWhiteSpace(passwordInput) Then
                Return False
            End If

            Dim inputUsr As String = usuarioInput.Trim().ToLower()
            Dim inputPwd As String = passwordInput.Trim()

            For Each u As UsuarioModel In _usuarios
                If u.Correo.ToLower() = inputUsr AndAlso u.ValidarPassword(inputPwd) Then
                    rolRetornado = u.ObtenerEtiquetaRol()
                    Return True
                End If
            Next

            Return False
        End Function

        Public Function RegistrarCliente(nombre As String, telefono As String, correo As String, password As String) As Boolean Implements IUsuarioService.RegistrarCliente
            If String.IsNullOrWhiteSpace(correo) OrElse String.IsNullOrWhiteSpace(password) Then
                Return False
            End If

            Dim nuevoCorreo As String = correo.Trim().ToLower()
            For Each u As UsuarioModel In _usuarios
                If u.Correo.Equals(nuevoCorreo, StringComparison.OrdinalIgnoreCase) Then
                    Return False ' Correo ya registrado
                End If
            Next

            Dim nuevoCliente As New UsuarioModel(
                If(String.IsNullOrWhiteSpace(nombre), "Cliente", nombre.Trim()),
                nuevoCorreo,
                password.Trim(),
                If(String.IsNullOrWhiteSpace(telefono), String.Empty, telefono.Trim()),
                RolUsuarioEnum.Cliente
            )

            _usuarios.Add(nuevoCliente)
            Return True
        End Function

        Public Function ObtenerTodos() As IReadOnlyList(Of UsuarioModel) Implements IUsuarioService.ObtenerTodos
            Return _usuarios.AsReadOnly()
        End Function

        Public Function BuscarPorCorreo(correo As String) As UsuarioModel Implements IUsuarioService.BuscarPorCorreo
            If String.IsNullOrWhiteSpace(correo) Then Return Nothing
            Dim target As String = correo.Trim().ToLower()
            For Each u As UsuarioModel In _usuarios
                If u.Correo.Equals(target, StringComparison.OrdinalIgnoreCase) Then
                    Return u
                End If
            Next
            Return Nothing
        End Function

    End Class
End Namespace
