Imports System

Namespace Models
    ''' <summary>
    ''' Modelo fuertemente tipado para Colaboradores y Empleados del restaurante.
    ''' </summary>
    Public Class EmpleadoModel

        Public Property IdEmpleado As Integer
        Public Property NombreCompleto As String
        Public Property Cedula As String
        Public Property Cargo As String ' Cocinero, Mesero, Aseador, Cajero, Administrador
        Public Property SalarioMensual As Decimal
        Public Property TipoSalario As String = "MENSUAL"
        Public Property FechaIngreso As DateTime
        Public Property Activo As Boolean

        Public Property RucCedula As String
            Get
                Return Cedula
            End Get
            Set(value As String)
                Cedula = value
            End Set
        End Property

        Public Sub New()
            Activo = True
            FechaIngreso = DateTime.Today
        End Sub

        Public Sub New(id As Integer, nombre As String, cedula As String, cargo As String, salario As Decimal, ingreso As DateTime, activo As Boolean)
            Me.IdEmpleado = id
            Me.NombreCompleto = If(nombre, "").Trim()
            Me.Cedula = If(cedula, "").Trim()
            Me.Cargo = If(cargo, "").Trim()
            Me.SalarioMensual = Math.Round(salario, 2)
            Me.FechaIngreso = ingreso
            Me.Activo = activo
        End Sub

        Public Overrides Function ToString() As String
            Return $"{NombreCompleto} ({Cargo} - {SalarioMensual:C2})"
        End Function

    End Class
End Namespace

