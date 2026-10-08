Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services

Namespace Data
    ''' <summary>
    ''' Fachada DAO de Acceso al Catálogo de Productos y Menú (RF-012 / CU-007).
    ''' Conecta la interfaz con el servicio orientado a objetos PlatoService.
    ''' </summary>
    Public Module PlatoDAO

        Private ReadOnly _servicioPlatos As IPlatoService = New PlatoService()

        ''' <summary>
        ''' Instancia del servicio orientada a objetos para acceso fuertemente tipado.
        ''' </summary>
        Public ReadOnly Property Servicio As IPlatoService
            Get
                Return _servicioPlatos
            End Get
        End Property

        ''' <summary>
        ''' Obtiene la lista completa de platos del catálogo como DataTable para enlace visual.
        ''' </summary>
        Public Function ObtenerTodos() As DataTable
            Return _servicioPlatos.ObtenerDataTable()
        End Function

        ''' <summary>
        ''' Obtiene todos los platos como colección fuertemente tipada de modelos POO.
        ''' </summary>
        Public Function ObtenerListaModelos() As IReadOnlyList(Of PlatoModel)
            Return _servicioPlatos.ObtenerTodos()
        End Function

        ''' <summary>
        ''' Agrega un nuevo plato al catálogo en memoria.
        ''' </summary>
        Public Function Guardar(nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String) As Boolean
            Dim nuevoPlato As New PlatoModel(0, nombre, categoria, precio, tiempo, disponible, descripcion)
            Return _servicioPlatos.Guardar(nuevoPlato)
        End Function

        ''' <summary>
        ''' Modifica un plato existente en el catálogo.
        ''' </summary>
        Public Function Actualizar(id As Integer, nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String) As Boolean
            Dim platoActualizado As New PlatoModel(id, nombre, categoria, precio, tiempo, disponible, descripcion)
            Return _servicioPlatos.Actualizar(platoActualizado)
        End Function

        ''' <summary>
        ''' Elimina un plato del catálogo por su ID.
        ''' </summary>
        Public Function Eliminar(id As Integer) As Boolean
            Return _servicioPlatos.Eliminar(id)
        End Function

        ''' <summary>
        ''' Obtiene el ID del plato según su nombre en el catálogo. Retorna 0 si no se encuentra.
        ''' </summary>
        Public Function ObtenerIdPorNombre(nombre As String) As Integer
            If String.IsNullOrWhiteSpace(nombre) Then Return 0
            Dim lista = _servicioPlatos.ObtenerTodos()
            For Each p In lista
                If String.Equals(p.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase) Then
                    Return p.ID
                End If
            Next
            Return 0
        End Function

    End Module
End Namespace
