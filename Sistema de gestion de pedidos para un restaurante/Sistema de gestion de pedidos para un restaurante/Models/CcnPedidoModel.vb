Imports System
Imports System.Collections.Generic

Namespace Models
    ''' <summary>
    ''' Modelo de entidad para la comanda de cocina.
    ''' Encapsula la información del pedido, estado, tiempos, cliente, mesa y platos solicitados.
    ''' Prefijo de módulo: Ccn
    ''' </summary>
    Public Class CcnPedidoModel

        ''' <summary> Identificador numérico único del pedido en el sistema </summary>
        Public Property IntIdPedido As Integer

        ''' <summary> Código correlativo visual de comanda (ej. #08-1042) </summary>
        Public Property StrCodigoComanda As String

        ''' <summary> Identificación del lugar (ej. MESA 04 o ENTREGAS) </summary>
        Public Property StrMesaCliente As String

        ''' <summary> Nombre del mozo o cajero que atendió la orden </summary>
        Public Property StrNombreMozo As String

        ''' <summary> Nombre del cliente asociado al pedido </summary>
        Public Property StrNombreCliente As String

        ''' <summary> Tipo de servicio (Mesa / Salón, Entregas) </summary>
        Public Property StrTipoServicio As String

        ''' <summary> Método de pago (Tarjeta Crédito, Efectivo pendiente, Pagado Web) </summary>
        Public Property StrMetodoPago As String

        ''' <summary> Fecha y hora exacta de registro de la comanda </summary>
        Public Property DtHoraRegistro As DateTime

        ''' <summary> Estado actual en el ciclo de vida de cocina </summary>
        Public Property EnumEstado As CcnEstadoPedidoEnum

        ''' <summary> Lista de platos o productos pertenecientes a la comanda </summary>
        Public Property LstDetallePlatos As List(Of CcnItemPedidoModel)

        Public Sub New()
            Me.IntIdPedido = 0
            Me.StrCodigoComanda = String.Empty
            Me.StrMesaCliente = String.Empty
            Me.StrNombreMozo = String.Empty
            Me.StrNombreCliente = String.Empty
            Me.StrTipoServicio = "Mesa / Salón"
            Me.StrMetodoPago = "Pendiente"
            Me.DtHoraRegistro = DateTime.Now
            Me.EnumEstado = CcnEstadoPedidoEnum.Recibido
            Me.LstDetallePlatos = New List(Of CcnItemPedidoModel)()
        End Sub

        Public Sub New(ByVal intIdPedido As Integer,
                       ByVal strCodigoComanda As String,
                       ByVal strMesaCliente As String,
                       ByVal strNombreMozo As String,
                       ByVal strNombreCliente As String,
                       ByVal strTipoServicio As String,
                       ByVal strMetodoPago As String,
                       ByVal dtHoraRegistro As DateTime,
                       ByVal enumEstado As CcnEstadoPedidoEnum)
            Me.IntIdPedido = intIdPedido
            Me.StrCodigoComanda = strCodigoComanda
            Me.StrMesaCliente = strMesaCliente
            Me.StrNombreMozo = strNombreMozo
            Me.StrNombreCliente = strNombreCliente
            Me.StrTipoServicio = strTipoServicio
            Me.StrMetodoPago = strMetodoPago
            Me.DtHoraRegistro = dtHoraRegistro
            Me.EnumEstado = enumEstado
            Me.LstDetallePlatos = New List(Of CcnItemPedidoModel)()
        End Sub

        ''' <summary>
        ''' Calcula la sumatoria económica total de la comanda.
        ''' </summary>
        Public Function CalcularTotal() As Decimal
            Dim decTotal As Decimal = 0D
            If Me.LstDetallePlatos IsNot Nothing Then
                For Each objItem As CcnItemPedidoModel In Me.LstDetallePlatos
                    decTotal += objItem.CalcularSubtotal()
                Next
            End If
            Return decTotal
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de minutos transcurridos desde que se emitió el pedido.
        ''' </summary>
        Public Function ObtenerMinutosTranscurridos() As Integer
            Dim objDiferencia As TimeSpan = DateTime.Now.Subtract(Me.DtHoraRegistro)
            Dim intMinutos As Integer = Convert.ToInt32(Math.Floor(objDiferencia.TotalMinutes))
            Return Math.Max(0, intMinutos)
        End Function

        ''' <summary>
        ''' Retorna el tiempo en formato amigable para monitor KDS (ej. 13:45 (Hace 8m)).
        ''' </summary>
        Public Function FormatearTiempoTranscurrido() As String
            Dim strHoraTexto As String = Me.DtHoraRegistro.ToString("HH:mm")
            Dim intMinutos As Integer = Me.ObtenerMinutosTranscurridos()
            Return $"{strHoraTexto} (Hace {intMinutos}m)"
        End Function

    End Class
End Namespace
