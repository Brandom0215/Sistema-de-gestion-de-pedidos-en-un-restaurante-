Imports System

Namespace Models
    ''' <summary>
    ''' Modelo fuertemente tipado para las Cuentas del Catálogo Contable (Plan de Cuentas).
    ''' </summary>
    Public Class CuentaContableModel

        Public Property CodigoCuenta As String
        Public Property NombreCuenta As String
        Public Property Tipo As String ' ACTIVO, PASIVO, PATRIMONIO, INGRESOS, GASTOS
        Public Property Naturaleza As String ' DEUDORA, ACREEDORA
        Public Property Nivel As Integer ' 1, 2, 3, 4
        Public Property IdCuentaPadre As String
        Public Property PermiteMovimiento As Boolean
        Public Property Activo As Boolean

        Public ReadOnly Property EsDeudora As Boolean
            Get
                Return String.Equals(Naturaleza, "DEUDORA", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public ReadOnly Property EsAcreedora As Boolean
            Get
                Return String.Equals(Naturaleza, "ACREEDORA", StringComparison.OrdinalIgnoreCase)
            End Get
        End Property

        Public Sub New()
            PermiteMovimiento = True
            Activo = True
        End Sub

        Public Sub New(codigo As String, nombre As String, tipo As String, naturaleza As String, nivel As Integer, padre As String, permiteMov As Boolean, activo As Boolean)
            Me.CodigoCuenta = If(codigo, "").Trim()
            Me.NombreCuenta = If(nombre, "").Trim()
            Me.Tipo = If(tipo, "").Trim().ToUpperInvariant()
            Me.Naturaleza = If(naturaleza, "DEUDORA").Trim().ToUpperInvariant()
            Me.Nivel = nivel
            Me.IdCuentaPadre = If(padre, "").Trim()
            Me.PermiteMovimiento = permiteMov
            Me.Activo = activo
        End Sub

        Public Overrides Function ToString() As String
            Return $"{CodigoCuenta} - {NombreCuenta}"
        End Function

    End Class
End Namespace

