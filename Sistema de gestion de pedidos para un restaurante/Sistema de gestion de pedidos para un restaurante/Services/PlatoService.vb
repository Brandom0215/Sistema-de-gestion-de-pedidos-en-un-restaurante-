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

        Public Function ObtenerTodos() As IReadOnlyList(Of PlatoModel) Implements IPlatoService.ObtenerTodos
            Dim lista As New List(Of PlatoModel)()
            Try
                Dim sql As String =
                    "SELECT p.id_plato, p.nombre_plato, c.nombre_categoria, p.precio, COALESCE(p.tiempo_preparacion, '15 min') AS tiempo_preparacion, COALESCE(p.disponible, TRUE) AS disponible, COALESCE(p.descripcion, '') AS descripcion " &
                    "FROM platos p " &
                    "LEFT JOIN categorias c ON p.id_categoria = c.id_categoria " &
                    "ORDER BY p.id_plato ASC;"

                Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                If dt IsNot Nothing Then
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
                End If
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
            End Try
            Return lista.AsReadOnly()
        End Function

        Public Function ObtenerPorId(id As Integer) As PlatoModel Implements IPlatoService.ObtenerPorId
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
            Return Nothing
        End Function

        Public Function Guardar(plato As PlatoModel) As Boolean Implements IPlatoService.Guardar
            If plato Is Nothing Then Return False
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
                Throw
            End Try
            Return False
        End Function

        Public Function Actualizar(plato As PlatoModel) As Boolean Implements IPlatoService.Actualizar
            If plato Is Nothing Then Return False
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
                Return (filas > 0)
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        Public Function Eliminar(id As Integer) As Boolean Implements IPlatoService.Eliminar
            Try
                Dim filas = ConexionBD.EjecutarComando("DELETE FROM platos WHERE id_plato = @id;", New NpgsqlParameter("@id", id))
                Return (filas > 0)
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        Public Function ObtenerDataTable() As DataTable Implements IPlatoService.ObtenerDataTable
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

            Return ConexionBD.EjecutarConsultaDataTable(sql)
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
