Imports System

Namespace Models
    ''' <summary>
    ''' Modelo de datos fuertemente tipado para Clientes Fiscales y Contribuyentes (DGI Panamá).
    ''' Centraliza la información fiscal para evitar clientes duplicados y permitir facturación oficial.
    ''' </summary>
    Public Class ClienteFiscalModel

        Public Property IdClienteFiscal As Integer
        Public Property RucCedula As String
        Public Property Dv As String
        Public Property TipoPersona As String ' NATURAL, JURIDICA, EXTRANJERO, CONSUMIDOR_FINAL
        Public Property RazonSocial As String
        Public Property DireccionFiscal As String
        Public Property Telefono As String
        Public Property Correo As String
        Public Property FechaRegistro As DateTime
        Public Property Activo As Boolean

        Public Sub New()
            TipoPersona = "NATURAL"
            Activo = True
            FechaRegistro = DateTime.Now
        End Sub

        Public Sub New(id As Integer, ruc As String, dv As String, tipo As String, razon As String, direccion As String, tel As String, email As String, activo As Boolean)
            Me.IdClienteFiscal = id
            Me.RucCedula = If(ruc, "").Trim().ToUpperInvariant()
            Me.Dv = If(dv, "").Trim()
            Me.TipoPersona = If(tipo, "NATURAL").Trim().ToUpperInvariant()
            Me.RazonSocial = If(razon, "").Trim()
            Me.DireccionFiscal = If(direccion, "").Trim()
            Me.Telefono = If(tel, "").Trim()
            Me.Correo = If(email, "").Trim().ToLowerInvariant()
            Me.Activo = activo
            Me.FechaRegistro = DateTime.Now
        End Sub

        ''' <summary>
        ''' Retorna el identificador tributario formateado con su DV si existe (Ej: 8-800-1234 DV 55).
        ''' </summary>
        Public ReadOnly Property RucConDv As String
            Get
                If String.IsNullOrWhiteSpace(Dv) Then
                    Return RucCedula
                End If
                Return $"{RucCedula} DV {Dv}"
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return $"{RazonSocial} ({RucConDv})"
        End Function

    End Class
End Namespace

