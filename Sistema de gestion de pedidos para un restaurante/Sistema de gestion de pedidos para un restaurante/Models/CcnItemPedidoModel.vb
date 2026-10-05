Imports System

Namespace Models
    ''' <summary>
    ''' Modelo orientado a objetos que representa un ítem o plato individual dentro de una comanda de cocina.
    ''' Prefijo de módulo: Ccn
    ''' Aplica notación húngara y nomenclatura descriptiva.
    ''' </summary>
    Public Class CcnItemPedidoModel

        ''' <summary> Cantidad solicitada del producto </summary>
        Public Property IntCantidad As Integer

        ''' <summary> Nombre descriptivo del plato o producto gastronómico </summary>
        Public Property StrNombrePlato As String

        ''' <summary> Notas de cocina, término de cocción o acompañamientos </summary>
        Public Property StrNotasAcompanamiento As String

        ''' <summary> Precio unitario del plato </summary>
        Public Property DecPrecioUnitario As Decimal

        ''' <summary> Indicador de advertencia médica o dietaria (ej. Celíaco, Sin Gluten, Alergia) </summary>
        Public Property BlnEsAlertaCeliaco As Boolean

        ''' <summary> Mensaje descriptivo de la alerta alimentaria si aplica </summary>
        Public Property StrMensajeAlerta As String

        Public Sub New()
            Me.IntCantidad = 1
            Me.StrNombrePlato = String.Empty
            Me.StrNotasAcompanamiento = String.Empty
            Me.DecPrecioUnitario = 0D
            Me.BlnEsAlertaCeliaco = False
            Me.StrMensajeAlerta = String.Empty
        End Sub

        Public Sub New(ByVal intCantidad As Integer,
                       ByVal strNombrePlato As String,
                       ByVal strNotasAcompanamiento As String,
                       ByVal decPrecioUnitario As Decimal,
                       Optional ByVal blnEsAlertaCeliaco As Boolean = False,
                       Optional ByVal strMensajeAlerta As String = "")
            Me.IntCantidad = intCantidad
            Me.StrNombrePlato = strNombrePlato
            Me.StrNotasAcompanamiento = strNotasAcompanamiento
            Me.DecPrecioUnitario = decPrecioUnitario
            Me.BlnEsAlertaCeliaco = blnEsAlertaCeliaco
            Me.StrMensajeAlerta = strMensajeAlerta
        End Sub

        ''' <summary>
        ''' Calcula el subtotal monetario de la línea de pedido.
        ''' </summary>
        Public Function CalcularSubtotal() As Decimal
            Return Me.IntCantidad * Me.DecPrecioUnitario
        End Function

    End Class
End Namespace
