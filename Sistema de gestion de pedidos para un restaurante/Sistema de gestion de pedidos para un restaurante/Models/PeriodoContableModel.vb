Imports System

Namespace Models
    ''' <summary>
    ''' Modelo fuertemente tipado para el control de Períodos Contables y Cierres Mensuales.
    ''' </summary>
    Public Class PeriodoContableModel

        Public Property IdPeriodo As Integer
        Public Property Anio As Integer
        Public Property Mes As Integer
        Public Property FechaInicio As DateTime
        Public Property FechaFin As DateTime
        Public Property Estado As String ' ABIERTO, CERRADO, BLOQUEADO
        Public Property FechaCierre As DateTime?
        Public Property UsuarioCierre As Integer?

        Public Sub New()
            Estado = "ABIERTO"
        End Sub

        Public Sub New(id As Integer, anio As Integer, mes As Integer, inicio As DateTime, fin As DateTime, estado As String)
            Me.IdPeriodo = id
            Me.Anio = anio
            Me.Mes = mes
            Me.FechaInicio = inicio
            Me.FechaFin = fin
            Me.Estado = If(estado, "ABIERTO").Trim().ToUpperInvariant()
        End Sub

        Public ReadOnly Property EstaAbierto As Boolean
            Get
                Return Estado = "ABIERTO"
            End Get
        End Property

        Public ReadOnly Property NombrePeriodo As String
            Get
                Dim mesNombre As String = New DateTime(Anio, Mes, 1).ToString("MMMM yyyy")
                Return Char.ToUpper(mesNombre(0)) & mesNombre.Substring(1)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return $"{NombrePeriodo} [{Estado}]"
        End Function

    End Class
End Namespace

