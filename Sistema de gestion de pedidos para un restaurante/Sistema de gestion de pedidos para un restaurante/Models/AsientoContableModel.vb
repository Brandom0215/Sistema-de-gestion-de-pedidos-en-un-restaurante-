Imports System
Imports System.Collections.Generic

Namespace Models
    ''' <summary>
    ''' Modelo fuertemente tipado para el Detalle de Asiento Contable (Línea de Partida Doble).
    ''' </summary>
    Public Class AsientoDetalleModel

        Public Property IdDetalle As Integer
        Public Property IdAsiento As Integer
        Public Property CodigoCuenta As String
        Public Property NombreCuenta As String
        Public Property DescripcionLinea As String
        Public Property Debe As Decimal
        Public Property Haber As Decimal

        Public Sub New()
            Debe = 0D
            Haber = 0D
        End Sub

        Public Sub New(codigoCuenta As String, descripcion As String, debe As Decimal, haber As Decimal)
            Me.CodigoCuenta = If(codigoCuenta, "").Trim()
            Me.DescripcionLinea = If(descripcion, "").Trim()
            Me.Debe = Math.Round(debe, 4)
            Me.Haber = Math.Round(haber, 4)
        End Sub

        Public Overrides Function ToString() As String
            Return $"{CodigoCuenta} | {DescripcionLinea} | Debe: {Debe:C2} | Haber: {Haber:C2}"
        End Function

    End Class

    ''' <summary>
    ''' Modelo fuertemente tipado para la Cabecera de Asientos Contables (Libro Diario).
    ''' </summary>
    Public Class AsientoContableModel

        Public Property IdAsiento As Integer
        Public Property NumeroAsiento As String
        Public Property IdPeriodo As Integer
        Public Property FechaAsiento As DateTime
        Public Property Concepto As String
        Public Property OrigenModulo As String ' VENTAS, DEVOLUCIONES, CAJA, GASTOS, NOMINA, AJUSTE, CIERRE
        Public Property OrigenId As Integer?
        Public Property TotalDebe As Decimal
        Public Property TotalHaber As Decimal
        Public Property Estado As String ' BORRADOR, ASENTADO, ANULADO
        Public Property CreadoPor As Integer?
        Public Property FechaCreacion As DateTime
        Public Property AnuladoPor As Integer?
        Public Property FechaAnulacion As DateTime?
        Public Property MotivoAnulacion As String

        Public Property Lineas As New List(Of AsientoDetalleModel)()

        Public Sub New()
            FechaAsiento = DateTime.Today
            FechaCreacion = DateTime.Now
            Estado = "ASENTADO"
        End Sub

        ''' <summary>
        ''' Verifica si el asiento cumple con la regla de oro de la partida doble: Suma Debe = Suma Haber.
        ''' </summary>
        Public ReadOnly Property EstaCuadrado As Boolean
            Get
                Dim dif As Decimal = Math.Abs(TotalDebe - TotalHaber)
                Return dif < 0.0001D
            End Get
        End Property

        ''' <summary>
        ''' Recalcula los totales de Debe y Haber a partir de las líneas del detalle.
        ''' </summary>
        Public Sub RecalcularTotales()
            Dim sumaDebe As Decimal = 0D
            Dim sumaHaber As Decimal = 0D
            For Each linea In Lineas
                sumaDebe += linea.Debe
                sumaHaber += linea.Haber
            Next
            TotalDebe = Math.Round(sumaDebe, 4)
            TotalHaber = Math.Round(sumaHaber, 4)
        End Sub

        Public Overrides Function ToString() As String
            Return $"{NumeroAsiento} ({FechaAsiento:yyyy-MM-dd}): {Concepto} | Total: {TotalDebe:C2}"
        End Function

    End Class
End Namespace

