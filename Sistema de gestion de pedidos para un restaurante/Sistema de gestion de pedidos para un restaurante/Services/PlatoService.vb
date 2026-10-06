Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Implementación orientada a objetos del catálogo gastronómico autóctono panameño (POO).
    ''' </summary>
    Public Class PlatoService
        Implements IPlatoService

        Private ReadOnly _platos As New List(Of PlatoModel)()
        Private _ultimoId As Integer = 0

        Public Sub New()
            ' Catálogo gastronómico 100% típico panameño categorizado por Desayunos, Almuerzos y Cenas
            
            ' Desayunos Panameños
            Guardar(New PlatoModel(0, "Hojaldre con Queso Blanco y Salchicha", "Desayunos", 3.5D, "10 min", True, "Hojaldre frita crujiente acompañada de queso blanco artesanal y salchichas guisadas."))
            Guardar(New PlatoModel(0, "Carimañola de Carne Molida", "Desayunos", 2.5D, "8 min", True, "Fritura tradicional de yuca rellena de carne molida sazonada al estilo panameño."))
            Guardar(New PlatoModel(0, "Tortilla de Maíz con Chicharrón", "Desayunos", 3.0D, "10 min", True, "Tortilla de maíz amarillo asada a la leña servida con chicharrón crujiente."))
            Guardar(New PlatoModel(0, "Tamal Panameño en Hoja de Bijao", "Desayunos", 4.0D, "12 min", True, "Tamal de maíz pilado relleno de pollo guisado, aceitunas, alcaparras y pasas."))

            ' Almuerzos Panameños
            Guardar(New PlatoModel(0, "Pescado Frito con Patacones", "Almuerzos", 10.5D, "18 min", True, "Pescado entero frito al punto dorado servido con patacones crujientes y ensalada de feria."))
            Guardar(New PlatoModel(0, "Sancocho Panameño de Gallina Criolla", "Almuerzos", 7.5D, "15 min", True, "Sancocho tradicional de gallina de patio con yuca, ñame, culantro y arroz blanco."))
            Guardar(New PlatoModel(0, "Ropa Vieja con Arroz con Guandú", "Almuerzos", 8.5D, "15 min", True, "Carne desmechada en salsa criolla acompañada de arroz con guandú de olor y plátano tentación."))
            Guardar(New PlatoModel(0, "Arroz con Pollo y Ensalada de Feria", "Almuerzos", 6.5D, "12 min", True, "Arroz con pollo sazonado con vegetales frescos y ensalada roja de remolacha."))

            ' Cenas Panameñas
            Guardar(New PlatoModel(0, "Bistec Picado con Hojaldres Calientes", "Cenas", 7.0D, "12 min", True, "Tiras de carne de res salteadas con cebolla y pimentón servidas con hojaldres recién fritas."))
            Guardar(New PlatoModel(0, "Corvina a la Tipileña con Patacones", "Cenas", 11.0D, "20 min", True, "Filete de corvina en salsa de tomate criollo, ají chombo y especias panameñas."))
            Guardar(New PlatoModel(0, "Lengua Guisada con Arroz y Tajadas", "Cenas", 8.0D, "15 min", True, "Lengua de res tierna guisada en vino y vegetales con tajadas de plátano maduro."))
            Guardar(New PlatoModel(0, "Saao de Cerdo con Yuca al Mojo", "Cenas", 6.0D, "12 min", True, "Cerdo frito en trozos sazonado con ajo y limón servido con yuca suave al mojo."))
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
