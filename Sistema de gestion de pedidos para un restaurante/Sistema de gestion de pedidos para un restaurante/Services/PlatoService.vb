Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Npgsql
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Implementación orientada a objetos del catálogo gastronómico en PostgreSQL (POO).
    ''' </summary>
    Public Class PlatoService
        Implements IPlatoService

        Private ReadOnly _platosLocales As New List(Of PlatoModel)()
        Private _ultimoIdLocal As Integer = 12

        Public Sub New()
            ' Catálogo gastronómico de contingencia local (cuando no hay conexión al servidor central)
            _platosLocales.Add(New PlatoModel(1, "Hojaldre con Queso Blanco y Salchicha", "Desayunos", 3.5D, "10 min", True, "Hojaldre frita crujiente acompañada de queso blanco artesanal y salchichas guisadas."))
            _platosLocales.Add(New PlatoModel(2, "Carimañola de Carne Molida", "Desayunos", 2.5D, "8 min", True, "Fritura tradicional de yuca rellena de carne molida sazonada al estilo panameño."))
            _platosLocales.Add(New PlatoModel(3, "Tortilla de Maíz con Chicharrón", "Desayunos", 3.0D, "10 min", True, "Tortilla de maíz amarillo asada a la leña servida con chicharrón crujiente."))
            _platosLocales.Add(New PlatoModel(4, "Tamal Panameño en Hoja de Bijao", "Desayunos", 4.0D, "12 min", True, "Tamal de maíz pilado relleno de pollo guisado, aceitunas, alcaparras y pasas."))
            _platosLocales.Add(New PlatoModel(5, "Pescado Frito con Patacones", "Almuerzos", 10.5D, "18 min", True, "Pescado entero frito al punto dorado servido con patacones crujientes y ensalada de feria."))
            _platosLocales.Add(New PlatoModel(6, "Sancocho Panameño de Gallina Criolla", "Almuerzos", 7.5D, "15 min", True, "Sancocho tradicional de gallina de patio con yuca, ñame, culantro y arroz blanco."))
            _platosLocales.Add(New PlatoModel(7, "Ropa Vieja con Arroz con Guandú", "Almuerzos", 8.5D, "15 min", True, "Carne desmechada en salsa criolla acompañada de arroz con guandú de olor y plátano tentación."))
            _platosLocales.Add(New PlatoModel(8, "Arroz con Pollo y Ensalada de Feria", "Almuerzos", 6.5D, "12 min", True, "Arroz con pollo sazonado con vegetales frescos y ensalada roja de remolacha."))
            _platosLocales.Add(New PlatoModel(9, "Bistec Picado con Hojaldres Calientes", "Cenas", 7.0D, "12 min", True, "Tiras de carne de res salteadas con cebolla y pimentón servidas con hojaldres recién fritas."))
            _platosLocales.Add(New PlatoModel(10, "Corvina a la Tipileña con Patacones", "Cenas", 11.0D, "20 min", True, "Filete de corvina en salsa de tomate criollo, ají chombo y especias panameñas."))
            _platosLocales.Add(New PlatoModel(11, "Lengua Guisada con Arroz y Tajadas", "Cenas", 8.0D, "15 min", True, "Lengua de res tierna guisada en vino y vegetales con tajadas de plátano maduro."))
            _platosLocales.Add(New PlatoModel(12, "Saao de Cerdo con Yuca al Mojo", "Cenas", 6.0D, "12 min", True, "Cerdo frito en trozos sazonado con ajo y limón servido con yuca suave al mojo."))
        End Sub

        Public Function ObtenerTodos() As IReadOnlyList(Of PlatoModel) Implements IPlatoService.ObtenerTodos
            If ConexionBD.DebeUsarPostgreSQL() Then
                Dim lista As New List(Of PlatoModel)()
                Try
                    Dim sql As String =
                        "SELECT p.id_plato, p.nombre_plato, c.nombre_categoria, p.precio, COALESCE(p.tiempo_preparacion, '15 min') AS tiempo_preparacion, COALESCE(p.disponible, TRUE) AS disponible, COALESCE(p.descripcion, '') AS descripcion " &
                        "FROM platos p " &
                        "LEFT JOIN categorias c ON p.id_categoria = c.id_categoria " &
                        "ORDER BY p.id_plato ASC;"

                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each row As DataRow In dt.Rows
                            Dim id As Integer = Convert.ToInt32(row("id_plato"))
                            Dim nombre As String = row("nombre_plato").ToString()
                            Dim cat As String = If(row("nombre_categoria") IsNot DBNull.Value, row("nombre_categoria").ToString(), "General")
                            Dim precio As Decimal = Convert.ToDecimal(row("precio"))
                            Dim tiempo As String = row("tiempo_preparacion").ToString()
                            Dim disp As Boolean = Convert.ToBoolean(row("disponible"))
                            Dim desc As String = row("descripcion").ToString()

                            lista.Add(New PlatoModel(id, nombre, cat, precio, tiempo, disp, desc))
                        Next
                        Return lista.AsReadOnly()
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If
            Return _platosLocales.AsReadOnly()
        End Function

        Public Function ObtenerPorId(id As Integer) As PlatoModel Implements IPlatoService.ObtenerPorId
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String =
                        "SELECT p.id_plato, p.nombre_plato, c.nombre_categoria, p.precio, COALESCE(p.tiempo_preparacion, '15 min') AS tiempo_preparacion, COALESCE(p.disponible, TRUE) AS disponible, COALESCE(p.descripcion, '') AS descripcion " &
                        "FROM platos p " &
                        "LEFT JOIN categorias c ON p.id_categoria = c.id_categoria " &
                        "WHERE p.id_plato = @id LIMIT 1;"

                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, New NpgsqlParameter("@id", id))
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim row = dt.Rows(0)
                        Return New PlatoModel(
                            Convert.ToInt32(row("id_plato")),
                            row("nombre_plato").ToString(),
                            If(row("nombre_categoria") IsNot DBNull.Value, row("nombre_categoria").ToString(), "General"),
                            Convert.ToDecimal(row("precio")),
                            row("tiempo_preparacion").ToString(),
                            Convert.ToBoolean(row("disponible")),
                            row("descripcion").ToString()
                        )
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            For Each p In _platosLocales
                If p.ID = id Then Return p
            Next
            Return Nothing
        End Function

        Public Function Guardar(plato As PlatoModel) As Boolean Implements IPlatoService.Guardar
            If plato Is Nothing Then Return False
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim idCat As Integer = ObtenerOInsertarIdCategoria(plato.Categoria)
                    Dim sql As String = "INSERT INTO platos (nombre_plato, id_categoria, precio, tiempo_preparacion, disponible, descripcion) " &
                                         "VALUES (@nombre, @idCat, @precio, @tiempo, @disp, @desc) RETURNING id_plato;"

                    Dim p1 As New NpgsqlParameter("@nombre", plato.Nombre.Trim())
                    Dim p2 As New NpgsqlParameter("@idCat", idCat)
                    Dim p3 As New NpgsqlParameter("@precio", plato.Precio)
                    Dim p4 As New NpgsqlParameter("@tiempo", plato.TiempoCoccion.Trim())
                    Dim p5 As New NpgsqlParameter("@disp", plato.Disponible)
                    Dim p6 As New NpgsqlParameter("@desc", plato.Descripcion.Trim())

                    Dim dtRes = ConexionBD.EjecutarConsultaDataTable(sql, p1, p2, p3, p4, p5, p6)
                    If dtRes IsNot Nothing AndAlso dtRes.Rows.Count > 0 Then
                        plato.ID = Convert.ToInt32(dtRes.Rows(0)(0))
                        Return True
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            _ultimoIdLocal += 1
            plato.ID = _ultimoIdLocal
            _platosLocales.Add(plato)
            Return True
        End Function

        Public Function Actualizar(plato As PlatoModel) As Boolean Implements IPlatoService.Actualizar
            If plato Is Nothing Then Return False
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim idCat As Integer = ObtenerOInsertarIdCategoria(plato.Categoria)
                    Dim sql As String = "UPDATE platos SET nombre_plato = @nombre, id_categoria = @idCat, precio = @precio, tiempo_preparacion = @tiempo, disponible = @disp, descripcion = @desc WHERE id_plato = @id;"

                    Dim p1 As New NpgsqlParameter("@nombre", plato.Nombre.Trim())
                    Dim p2 As New NpgsqlParameter("@idCat", idCat)
                    Dim p3 As New NpgsqlParameter("@precio", plato.Precio)
                    Dim p4 As New NpgsqlParameter("@tiempo", plato.TiempoCoccion.Trim())
                    Dim p5 As New NpgsqlParameter("@disp", plato.Disponible)
                    Dim p6 As New NpgsqlParameter("@desc", plato.Descripcion.Trim())
                    Dim p7 As New NpgsqlParameter("@id", plato.ID)

                    Dim filas = ConexionBD.EjecutarComando(sql, p1, p2, p3, p4, p5, p6, p7)
                    If filas > 0 Then Return True
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            For Each p In _platosLocales
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
            Return True
        End Function

        Public Function Eliminar(id As Integer) As Boolean Implements IPlatoService.Eliminar
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim filas = ConexionBD.EjecutarComando("DELETE FROM platos WHERE id_plato = @id;", New NpgsqlParameter("@id", id))
                    If filas > 0 Then Return True
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            For i As Integer = _platosLocales.Count - 1 To 0 Step -1
                If _platosLocales(i).ID = id Then
                    _platosLocales.RemoveAt(i)
                    Return True
                End If
            Next
            Return True
        End Function

        Public Function ObtenerDataTable() As DataTable Implements IPlatoService.ObtenerDataTable
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String =
                        "SELECT " &
                        "  p.id_plato AS ""ID"", " &
                        "  p.nombre_plato AS ""Nombre"", " &
                        "  COALESCE(c.nombre_categoria, 'General') AS ""Categoria"", " &
                        "  p.precio AS ""Precio"", " &
                        "  COALESCE(p.tiempo_preparacion, '15 min') AS ""TiempoCoccion"", " &
                        "  CASE WHEN COALESCE(p.disponible, TRUE) THEN 'Disponible' ELSE 'Agotado' END AS ""Estado"", " &
                        "  COALESCE(p.descripcion, '') AS ""Descripcion"" " &
                        "FROM platos p " &
                        "LEFT JOIN categorias c ON p.id_categoria = c.id_categoria " &
                        "ORDER BY p.id_plato ASC;"

                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Return dt
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            ' Generar tabla desde catálogo local si el servidor no está en la red actual
            Dim dtLocal As New DataTable("Platos")
            dtLocal.Columns.Add("ID", GetType(Integer))
            dtLocal.Columns.Add("Nombre", GetType(String))
            dtLocal.Columns.Add("Categoria", GetType(String))
            dtLocal.Columns.Add("Precio", GetType(Decimal))
            dtLocal.Columns.Add("TiempoCoccion", GetType(String))
            dtLocal.Columns.Add("Estado", GetType(String))
            dtLocal.Columns.Add("Descripcion", GetType(String))

            For Each p In _platosLocales
                Dim dr = dtLocal.NewRow()
                dr("ID") = p.ID
                dr("Nombre") = p.Nombre
                dr("Categoria") = p.Categoria
                dr("Precio") = p.Precio
                dr("TiempoCoccion") = p.TiempoCoccion
                dr("Estado") = If(p.Disponible, "Disponible", "Agotado")
                dr("Descripcion") = p.Descripcion
                dtLocal.Rows.Add(dr)
            Next

            Return dtLocal
        End Function

        Private Function ObtenerOInsertarIdCategoria(nombreCategoria As String) As Integer
            Dim catLimpia As String = If(String.IsNullOrWhiteSpace(nombreCategoria), "General", nombreCategoria.Trim())
            Dim sqlSel As String = "SELECT id_categoria FROM categorias WHERE LOWER(nombre_categoria) = LOWER(@cat) LIMIT 1;"
            Dim dt = ConexionBD.EjecutarConsultaDataTable(sqlSel, New NpgsqlParameter("@cat", catLimpia))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)(0))
            End If

            Dim sqlIns As String = "INSERT INTO categorias (nombre_categoria, descripcion) VALUES (@cat, @desc) RETURNING id_categoria;"
            Dim dtIns = ConexionBD.EjecutarConsultaDataTable(sqlIns, New NpgsqlParameter("@cat", catLimpia), New NpgsqlParameter("@desc", "Categoría del Menú"))
            If dtIns IsNot Nothing AndAlso dtIns.Rows.Count > 0 Then
                Return Convert.ToInt32(dtIns.Rows(0)(0))
            End If

            Return 1
        End Function

    End Class
End Namespace
