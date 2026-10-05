Imports System
Imports System.Data

Namespace Data
    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) totalmente en memoria para la gestión CRUD de pedidos.
    ''' Funciona 100% aislado sin requerir base de datos ni servicios externos.
    ''' </summary>
    Public Module PedidoDAO

        Private ReadOnly _tablaPedidos As DataTable
        Private _ultimoId As Integer = 0

        Sub New()
            _tablaPedidos = New DataTable("Pedidos")
            _tablaPedidos.Columns.Add("ID", GetType(Integer))
            _tablaPedidos.Columns.Add("Cliente", GetType(String))
            _tablaPedidos.Columns.Add("Mesa", GetType(String))
            _tablaPedidos.Columns.Add("PlatoPrincipal", GetType(String))
            _tablaPedidos.Columns.Add("Acompanamientos", GetType(String))
            _tablaPedidos.Columns.Add("TipoServicio", GetType(String))
            _tablaPedidos.Columns.Add("FechaHora", GetType(String))

            ' Cargar pedidos iniciales de demostración
            AgregarPedidoDemostracion("Juan Pérez", "Mesa 04", "Sancocho Criollo Gourmet", "Arroz Blanco, Tobitas de Plátano", "Comer en Local", 450.0D)
            AgregarPedidoDemostracion("María Rodríguez", "Mesa 02", "Chivo Liniero Guisado", "Mofongo de Plátano Verde", "Comer en Local", 650.0D)
            AgregarPedidoDemostracion("Carlos Gómez", "Para Llevar", "Mofongo Especial El Buen Sazoncito", "Chicharrón Crujiente", "Para Llevar", 550.0D)
        End Sub

        Private Sub AgregarPedidoDemostracion(cliente As String, mesa As String, plato As String, acomp As String, servicio As String, total As Decimal)
            _ultimoId += 1
            Dim dr As DataRow = _tablaPedidos.NewRow()
            dr("ID") = _ultimoId
            dr("Cliente") = cliente
            dr("Mesa") = mesa
            dr("PlatoPrincipal") = plato
            dr("Acompanamientos") = acomp
            dr("TipoServicio") = servicio
            dr("FechaHora") = DateTime.Now.ToString("HH:mm:ss")
            _tablaPedidos.Rows.Add(dr)
        End Sub

        ''' <summary>
        ''' Obtiene la tabla en memoria de pedidos registrados.
        ''' </summary>
        Public Function ObtenerTodos() As DataTable
            Return _tablaPedidos.Copy()
        End Function

        ''' <summary>
        ''' Agrega un nuevo pedido en memoria.
        ''' </summary>
        Public Function Guardar(cliente As String, mesa As String, plato As String, acomp As String, servicio As String, total As Decimal) As Boolean
            Try
                _ultimoId += 1
                Dim dr As DataRow = _tablaPedidos.NewRow()
                dr("ID") = _ultimoId
                dr("Cliente") = If(String.IsNullOrWhiteSpace(cliente), "Cliente General", cliente.Trim())
                dr("Mesa") = If(String.IsNullOrWhiteSpace(mesa), "Mesa 01", mesa.Trim())
                dr("PlatoPrincipal") = If(String.IsNullOrWhiteSpace(plato), "Plato del Día", plato.Trim())
                dr("Acompanamientos") = If(String.IsNullOrWhiteSpace(acomp), "Sin acompañamiento", acomp.Trim())
                dr("TipoServicio") = If(String.IsNullOrWhiteSpace(servicio), "Comer en Local", servicio.Trim())
                dr("FechaHora") = DateTime.Now.ToString("HH:mm:ss")
                _tablaPedidos.Rows.Add(dr)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Actualiza un pedido en memoria por su ID.
        ''' </summary>
        Public Function Actualizar(id As Integer, cliente As String, mesa As String, plato As String, acomp As String, servicio As String) As Boolean
            For Each row As DataRow In _tablaPedidos.Rows
                If Convert.ToInt32(row("ID")) = id Then
                    row("Cliente") = cliente.Trim()
                    row("Mesa") = mesa.Trim()
                    row("PlatoPrincipal") = plato.Trim()
                    row("Acompanamientos") = acomp.Trim()
                    row("TipoServicio") = servicio.Trim()
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Elimina un pedido por ID de la tabla en memoria.
        ''' </summary>
        Public Function Eliminar(id As Integer) As Boolean
            For i As Integer = _tablaPedidos.Rows.Count - 1 To 0 Step -1
                If Convert.ToInt32(_tablaPedidos.Rows(i)("ID")) = id Then
                    _tablaPedidos.Rows.RemoveAt(i)
                    Return True
                End If
            Next
            Return False
        End Function

    End Module
End Namespace
