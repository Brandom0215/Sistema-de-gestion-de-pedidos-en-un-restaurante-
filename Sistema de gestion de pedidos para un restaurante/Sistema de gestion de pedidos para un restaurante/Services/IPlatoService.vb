Imports System.Collections.Generic
Imports System.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Contrato de interfaz para el catálogo gastronómico de platos (POO).
    ''' </summary>
    Public Interface IPlatoService

        Function ObtenerTodos() As IReadOnlyList(Of PlatoModel)

        Function ObtenerPorId(id As Integer) As PlatoModel

        Function Guardar(plato As PlatoModel) As Boolean

        Function Actualizar(plato As PlatoModel) As Boolean

        Function Eliminar(id As Integer) As Boolean

        Function ObtenerDataTable() As DataTable

    End Interface
End Namespace
