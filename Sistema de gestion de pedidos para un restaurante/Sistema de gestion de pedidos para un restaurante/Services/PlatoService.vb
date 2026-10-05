Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Implementación orientada a objetos del catálogo de platos del restaurante (POO).
    ''' </summary>
    Public Class PlatoService
        Implements IPlatoService

        Private ReadOnly _platos As New List(Of PlatoModel)()
        Private _ultimoId As Integer = 0

        Public Sub New()
            ' Catálogo inicial gastronómico
            Guardar(New PlatoModel(0, "Sancocho Criollo Gourmet", "Especialidades", 450.0D, "15 min", True, "Sancocho dominicano tradicional con carnes seleccionadas y víveres frescos."))
            Guardar(New PlatoModel(0, "Chivo Liniero Guisado", "Platos Fuertes", 650.0D, "20 min", True, "Chivo liniero tierno guisado a fuego lento con orégano silvestre."))
            Guardar(New PlatoModel(0, "Mofongo Especial El Buen Sazón", "Autóctonos", 550.0D, "12 min", True, "Plátano verde majado con ajo silvestre, chicharrón crujiente y caldo de la casa."))
            Guardar(New PlatoModel(0, "Jarra de Jugo Natural de Chinola", "Bebidas", 200.0D, "5 min", True, "Jarra de 1 litro de chinola 100% natural servida con hielo rústico."))
            Guardar(New PlatoModel(0, "Flan de Leche Artesanal", "Postres", 180.0D, "5 min", False, "Flan de leche condensada tradicional con caramelo suave (Agotado por el momento)."))
        End Sub

        Public Function ObtenerTodos() As IReadOnlyList(Of PlatoModel) Implements IPlatoService.ObtenerTodos
            Return _platos.AsReadOnly()
        End Function

        Public Function ObtenerPorId(id As Integer) As PlatoModel Implements IPlatoService.ObtenerPorId
            For Each p In _platos
                If p.ID = id Then Return p
            Next
            Return Nothing
        End Function

        Public Function Guardar(plato As PlatoModel) As Boolean Implements IPlatoService.Guardar
            If plato Is Nothing Then Return False
            _ultimoId += 1
            plato.ID = _ultimoId
            _platos.Add(plato)
            Return True
        End Function

        Public Function Actualizar(plato As PlatoModel) As Boolean Implements IPlatoService.Actualizar
            If plato Is Nothing Then Return False
            For Each p In _platos
                If p.ID = plato.ID Then
                    p.Nombre = plato.Nombre
                    p.Categoria = plato.Categoria
                    p.Precio = plato.Precio
                    p.TiempoCoccion = plato.TiempoCoccion
                    p.Disponible = plato.Disponible
                    p.Descripcion = plato.Descripcion
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function Eliminar(id As Integer) As Boolean Implements IPlatoService.Eliminar
            For i As Integer = _platos.Count - 1 To 0 Step -1
                If _platos(i).ID = id Then
                    _platos.RemoveAt(i)
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function ObtenerDataTable() As DataTable Implements IPlatoService.ObtenerDataTable
            Dim dt As New DataTable("Platos")
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Nombre", GetType(String))
            dt.Columns.Add("Categoria", GetType(String))
            dt.Columns.Add("Precio", GetType(Decimal))
            dt.Columns.Add("TiempoCoccion", GetType(String))
            dt.Columns.Add("Estado", GetType(String))
            dt.Columns.Add("Descripcion", GetType(String))

            For Each p In _platos
                Dim dr = dt.NewRow()
                dr("ID") = p.ID
                dr("Nombre") = p.Nombre
                dr("Categoria") = p.Categoria
                dr("Precio") = p.Precio
                dr("TiempoCoccion") = p.TiempoCoccion
                dr("Estado") = If(p.Disponible, "Disponible", "Agotado")
                dr("Descripcion") = p.Descripcion
                dt.Rows.Add(dr)
            Next

            Return dt
        End Function

    End Class
End Namespace
