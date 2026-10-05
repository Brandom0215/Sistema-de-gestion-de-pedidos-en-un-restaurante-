Namespace Models
    ''' <summary>
    ''' Enumeración fuertemente tipada para los estados del ciclo de vida de un pedido en cocina.
    ''' Prefijo de módulo: Ccn
    ''' </summary>
    Public Enum CcnEstadoPedidoEnum
        ''' <summary> Pedido recién recibido en el sistema, en espera de cocina. </summary>
        Recibido = 1

        ''' <summary> Pedido aceptado por el personal de cocina, actualmente en preparación. </summary>
        EnPreparacion = 2

        ''' <summary> Pedido terminado por el chef, listo para ser entregado en mesa o despacho. </summary>
        Listo = 3

        ''' <summary> Pedido despachado/cerrado satisfactoriamente. </summary>
        Entregado = 4
    End Enum
End Namespace
