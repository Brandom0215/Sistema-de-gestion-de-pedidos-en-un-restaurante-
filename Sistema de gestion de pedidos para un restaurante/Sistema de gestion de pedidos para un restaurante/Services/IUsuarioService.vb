Imports System.Collections.Generic
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Contrato de interfaz para el servicio de usuarios y autenticación (POO / Inversión de Dependencias).
    ''' </summary>
    Public Interface IUsuarioService

        Function Autenticar(usuarioInput As String, passwordInput As String, ByRef rolRetornado As String) As Boolean

        Function RegistrarCliente(nombre As String, telefono As String, correo As String, password As String) As Boolean

        Function ObtenerTodos() As IReadOnlyList(Of UsuarioModel)

        Function BuscarPorCorreo(correo As String) As UsuarioModel

    End Interface
End Namespace
