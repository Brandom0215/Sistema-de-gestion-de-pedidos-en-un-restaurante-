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

        ''' <summary> Indicador estricto de si el pedido ya fue pagado o está pendiente de cobro </summary>
        Public Property BlnEstaPagado As Boolean

        ''' <summary> Fecha y hora exacta de registro de la comanda </summary>
        Public Property DtHoraRegistro As DateTime

        ''' <summary> Estado actual en el ciclo de vida de cocina </summary>
        Public Property EnumEstado As CcnEstadoPedidoEnum

        ''' <summary> Lista de platos o productos pertenecientes a la comanda </summary>
        Public Property LstDetallePlatos As List(Of CcnItemPedidoModel)

        ''' <summary> Indicador de si el pedido cuenta con comprobante fiscal generado en Facturación </summary>
        Public Property BlnFacturado As Boolean

        ''' <summary> Número fiscal de factura asignado (ej. FAC-2026-1001) </summary>
        Public Property StrNumeroFactura As String

        Public Sub New()
            Me.IntIdPedido = 0
            Me.StrCodigoComanda = String.Empty
            Me.StrMesaCliente = String.Empty
            Me.StrNombreMozo = String.Empty
            Me.StrNombreCliente = String.Empty
            Me.StrTipoServicio = "Comer en el Sitio"
            Me.StrMetodoPago = "Pendiente"
            Me.BlnEstaPagado = False
            Me.BlnFacturado = False
            Me.StrNumeroFactura = String.Empty
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
                       ByVal blnEstaPagado As Boolean,
                       ByVal dtHoraRegistro As DateTime,
                       ByVal enumEstado As CcnEstadoPedidoEnum)
            Me.IntIdPedido = intIdPedido
            Me.StrCodigoComanda = strCodigoComanda
            Me.StrMesaCliente = strMesaCliente
            Me.StrNombreMozo = strNombreMozo
            Me.StrNombreCliente = strNombreCliente
            Me.StrTipoServicio = strTipoServicio
            Me.StrMetodoPago = strMetodoPago
            Me.BlnEstaPagado = blnEstaPagado
            Me.DtHoraRegistro = dtHoraRegistro
            Me.EnumEstado = enumEstado
            Me.LstDetallePlatos = New List(Of CcnItemPedidoModel)()
        End Sub

        ''' <summary>
        ''' Obtiene el nombre del primer plato principal para ilustrar o previsualizar.
        ''' </summary>
        Public Function ObtenerPlatoPrincipalNombre() As String
            If Me.LstDetallePlatos IsNot Nothing AndAlso Me.LstDetallePlatos.Count > 0 Then
                Return Me.LstDetallePlatos(0).StrNombrePlato
            End If
            Return "Plato del Día"
        End Function

        ''' <summary>
        ''' Retorna el formato descriptivo: '🪑 Comer en el Sitio — Mesa 04' o '🛍 Para Llevar / Entregas'.
        ''' </summary>
        Public Function ObtenerEtiquetaServicioMesa() As String
            If Me.StrTipoServicio.IndexOf("Llevar", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               Me.StrTipoServicio.IndexOf("Entrega", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Return "🛍 Para Llevar"
            Else
                Dim strMesa As String = If(String.IsNullOrWhiteSpace(Me.StrMesaCliente), "Mesa 01", Me.StrMesaCliente)
                Return $"🪑 Comer en Local • {strMesa}"
            End If
        End Function

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
        ''' Calcula el subtotal gravable antes de impuestos (asumiendo 7% ITBIS).
        ''' </summary>
        Public Function CalcularSubtotal() As Decimal
            Dim decTotal As Decimal = Me.CalcularTotal()
            Return Math.Round(decTotal / 1.07D, 2)
        End Function

        ''' <summary>
        ''' Calcula el valor del impuesto aplicado a la comanda.
        ''' </summary>
        Public Function CalcularImpuesto() As Decimal
            Dim decTotal As Decimal = Me.CalcularTotal()
            Dim decSubtotal As Decimal = Me.CalcularSubtotal()
            Return Math.Round(decTotal - decSubtotal, 2)
        End Function

        ''' <summary>
        ''' Calcula el cambio o vuelto para el cliente a partir del monto entregado en efectivo.
        ''' </summary>
        Public Function CalcularCambio(decMontoRecibido As Decimal) As Decimal
            Dim decTotal As Decimal = Me.CalcularTotal()
            If decMontoRecibido > decTotal Then
                Return Math.Round(decMontoRecibido - decTotal, 2)
            End If
            Return 0D
        End Function

        ''' <summary>
        ''' Determina si el pedido puede avanzar al siguiente estado operativo en cocina.
        ''' </summary>
        Public Function PuedeAvanzarEstado() As Boolean
            Return Me.EnumEstado < CcnEstadoPedidoEnum.Entregado
        End Function

        ''' <summary>
        ''' Obtiene el siguiente estado del ciclo de vida en cocina según la máquina de estados de negocio (POO).
        ''' </summary>
        Public Function ObtenerProximoEstado() As CcnEstadoPedidoEnum
            Select Case Me.EnumEstado
                Case CcnEstadoPedidoEnum.Recibido
                    Return CcnEstadoPedidoEnum.EnPreparacion
                Case CcnEstadoPedidoEnum.EnPreparacion
                    Return CcnEstadoPedidoEnum.Listo
                Case CcnEstadoPedidoEnum.Listo
                    Return CcnEstadoPedidoEnum.Entregado
                Case Else
                    Return Me.EnumEstado
            End Select
        End Function

        ''' <summary>
        ''' Avanza la comanda hacia la siguiente fase de su ciclo de vida en cocina (POO).
        ''' </summary>
        Public Function AvanzarSiguienteEstado() As Boolean
            If Not Me.PuedeAvanzarEstado() Then Return False
            Me.EnumEstado = Me.ObtenerProximoEstado()
            Return True
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
