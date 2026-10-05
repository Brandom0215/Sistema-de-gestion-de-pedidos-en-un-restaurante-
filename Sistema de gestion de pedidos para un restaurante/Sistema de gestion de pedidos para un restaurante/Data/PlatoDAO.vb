Imports System
Imports System.Collections.Generic
Imports System.Data

Namespace Data
    ''' <summary>
    ''' Estructura del Modelo de Plato del Menú en memoria.
    ''' </summary>
    Public Class PlatoItem
        Public Property ID As Integer
        Public Property Nombre As String
        Public Property Categoria As String
        Public Property Precio As Decimal
        Public Property TiempoCoccion As String
        Public Property Disponible As Boolean
        Public Property Descripcion As String

        Public Sub New(id As Integer, nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String)
            Me.ID = id
            Me.Nombre = nombre
            Me.Categoria = categoria
            Me.Precio = precio
            Me.TiempoCoccion = tiempo
            Me.Disponible = disponible
            Me.Descripcion = descripcion
        End Sub
    End Class

    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) en memoria para el Catálogo de Productos y Menú (RF-012 / CU-007).
    ''' </summary>
    Public Module PlatoDAO

        Private ReadOnly _tablaPlatos As DataTable
        Private _ultimoId As Integer = 0

        Sub New()
            _tablaPlatos = New DataTable("Platos")
            _tablaPlatos.Columns.Add("ID", GetType(Integer))
            _tablaPlatos.Columns.Add("Nombre", GetType(String))
            _tablaPlatos.Columns.Add("Categoria", GetType(String))
            _tablaPlatos.Columns.Add("Precio", GetType(Decimal))
            _tablaPlatos.Columns.Add("TiempoCoccion", GetType(String))
            _tablaPlatos.Columns.Add("Estado", GetType(String))
            _tablaPlatos.Columns.Add("Descripcion", GetType(String))

            ' Cargar catálogo inicial de demostración del restaurante El Buen Sazón
            AgregarPlatoDemostracion("Sancocho Criollo Gourmet", "Especialidades", 450.0D, "15 min", True, "Sancocho dominicano tradicional con carnes seleccionadas y víveres frescos.")
            AgregarPlatoDemostracion("Chivo Liniero Guisado", "Platos Fuertes", 650.0D, "20 min", True, "Chivo liniero tierno guisado a fuego lento con orégano silvestre.")
            AgregarPlatoDemostracion("Mofongo Especial El Buen Sazon", "Autóctonos", 550.0D, "12 min", True, "Plátano verde majado con ajo silvestre, chicharrón crujiente y caldo de la casa.")
            AgregarPlatoDemostracion("Jarra de Jugo Natural de Chinola", "Bebidas", 200.0D, "5 min", True, "Jarra de 1 litro de chinola 100% natural servida con hielo rústico.")
            AgregarPlatoDemostracion("Flan de Leche Artesanal", "Postres", 180.0D, "5 min", False, "Flan de leche condensada tradicional con caramelo suave (Agotado por el momento).")
        End Sub

        Private Sub AgregarPlatoDemostracion(nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String)
            _ultimoId += 1
            Dim dr As DataRow = _tablaPlatos.NewRow()
            dr("ID") = _ultimoId
            dr("Nombre") = nombre
            dr("Categoria") = categoria
            dr("Precio") = precio
            dr("TiempoCoccion") = tiempo
            dr("Estado") = If(disponible, "Disponible", "Agotado")
            dr("Descripcion") = descripcion
            _tablaPlatos.Rows.Add(dr)
        End Sub

        ''' <summary>
        ''' Obtiene la lista completa de platos del catálogo en memoria.
        ''' </summary>
        Public Function ObtenerTodos() As DataTable
            Return _tablaPlatos.Copy()
        End Function

        ''' <summary>
        ''' Agrega un nuevo plato al catálogo en memoria.
        ''' </summary>
        Public Function Guardar(nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String) As Boolean
            Try
                _ultimoId += 1
                Dim dr As DataRow = _tablaPlatos.NewRow()
                dr("ID") = _ultimoId
                dr("Nombre") = nombre.Trim()
                dr("Categoria") = categoria.Trim()
                dr("Precio") = precio
                dr("TiempoCoccion") = If(String.IsNullOrWhiteSpace(tiempo), "15 min", tiempo.Trim())
                dr("Estado") = If(disponible, "Disponible", "Agotado")
                dr("Descripcion") = If(String.IsNullOrWhiteSpace(descripcion), "Sin descripción", descripcion.Trim())
                _tablaPlatos.Rows.Add(dr)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Modifica un plato existente en el catálogo.
        ''' </summary>
        Public Function Actualizar(id As Integer, nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String) As Boolean
            For Each row As DataRow In _tablaPlatos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    row("Nombre") = nombre.Trim()
                    row("Categoria") = categoria.Trim()
                    row("Precio") = precio
                    row("TiempoCoccion") = tiempo.Trim()
                    row("Estado") = If(disponible, "Disponible", "Agotado")
                    row("Descripcion") = descripcion.Trim()
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Elimina un plato del catálogo por su ID.
        ''' </summary>
        Public Function Eliminar(id As Integer) As Boolean
            For i As Integer = _tablaPlatos.Rows.Count - 1 To 0 Step -1
                If Convert.ToInt32(_tablaPlatos.Rows(i)("ID")) = id Then
                    _tablaPlatos.Rows.RemoveAt(i)
                    Return True
                End If
            Next
            Return False
        End Function

    End Module
End Namespace
