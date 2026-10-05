Imports System

Namespace Models
    ''' <summary>
    ''' Modelo de entidad orientado a objetos que representa un plato del menú del restaurante (POO).
    ''' Encapsula atributos, validaciones y reglas de negocio de catálogo gastronómico.
    ''' </summary>
    Public Class PlatoModel

        Public Property ID As Integer
        Public Property Nombre As String
        Public Property Categoria As String
        Public Property Precio As Decimal
        Public Property TiempoCoccion As String
        Public Property Disponible As Boolean
        Public Property Descripcion As String

        Public Sub New()
            Me.ID = 0
            Me.Nombre = String.Empty
            Me.Categoria = "Especialidades"
            Me.Precio = 0D
            Me.TiempoCoccion = "15 min"
            Me.Disponible = True
            Me.Descripcion = String.Empty
        End Sub

        Public Sub New(id As Integer, nombre As String, categoria As String, precio As Decimal, tiempo As String, disponible As Boolean, descripcion As String)
            Me.ID = id
            Me.Nombre = If(String.IsNullOrWhiteSpace(nombre), "Plato", nombre.Trim())
            Me.Categoria = If(String.IsNullOrWhiteSpace(categoria), "General", categoria.Trim())
            Me.Precio = Math.Max(0D, precio)
            Me.TiempoCoccion = If(String.IsNullOrWhiteSpace(tiempo), "15 min", tiempo.Trim())
            Me.Disponible = disponible
            Me.Descripcion = If(String.IsNullOrWhiteSpace(descripcion), "Sin descripción", descripcion.Trim())
        End Sub

        ''' <summary>
        ''' Formatea el precio con símbolo de moneda oficial.
        ''' </summary>
        Public Function FormatearPrecio() As String
            Return Me.Precio.ToString("C2")
        End Function

        ''' <summary>
        ''' Valida que los datos del plato sean coherentes para el catálogo.
        ''' </summary>
        Public Function Validar(ByRef mensajeError As String) As Boolean
            If String.IsNullOrWhiteSpace(Me.Nombre) Then
                mensajeError = "El nombre del plato no puede estar vacío."
                Return False
            End If

            If Me.Precio <= 0D Then
                mensajeError = "El precio del plato debe ser mayor a cero."
                Return False
            End If

            If String.IsNullOrWhiteSpace(Me.Categoria) Then
                mensajeError = "Debe asignar una categoría al plato."
                Return False
            End If

            mensajeError = String.Empty
            Return True
        End Function

    End Class
End Namespace
