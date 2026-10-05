Imports System
Imports System.Collections.Generic
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services

Namespace Data
    ''' <summary>
    ''' Fachada DAO de Acceso a Datos de Usuarios.
    ''' Delega en la arquitectura orientada a objetos (IUsuarioService / UsuarioService),
    ''' garantizando reutilización, POO y compatibilidad retroactiva.
    ''' </summary>
    Public Module UsuarioDAO

        Private ReadOnly _servicioUsuarios As IUsuarioService = New UsuarioService()

        ''' <summary>
        ''' Instancia del servicio orientada a objetos para acceso fuertemente tipado.
        ''' </summary>
        Public ReadOnly Property Servicio As IUsuarioService
            Get
                Return _servicioUsuarios
            End Get
        End Property

        ''' <summary>
        ''' Autentica un usuario por credenciales contra la lista en memoria.
        ''' </summary>
        Public Function Autenticar(usuarioInput As String, passwordInput As String, ByRef rolRetornado As String) As Boolean
            Return _servicioUsuarios.Autenticar(usuarioInput, passwordInput, rolRetornado)
        End Function

        ''' <summary>
        ''' Registra un nuevo cliente en la lista en memoria.
        ''' </summary>
        Public Function RegistrarCliente(nombre As String, telefono As String, correo As String, password As String) As Boolean
            Return _servicioUsuarios.RegistrarCliente(nombre, telefono, correo, password)
        End Function

        ''' <summary>
        ''' Obtiene la colección completa de usuarios en memoria como modelos POO.
        ''' </summary>
        Public Function ObtenerTodos() As IReadOnlyList(Of UsuarioModel)
            Return _servicioUsuarios.ObtenerTodos()
        End Function

    End Module
End Namespace
