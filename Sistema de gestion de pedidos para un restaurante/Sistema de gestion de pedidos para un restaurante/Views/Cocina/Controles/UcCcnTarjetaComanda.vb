Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Cocina.Controles
    ''' <summary>
    ''' Control de usuario que representa una tarjeta de comanda en el Monitor de Cocina (KDS).
    ''' Aplica principios de encapsulación, notación húngara y paleta oficial.
    ''' Prefijo de módulo: Ccn
    ''' </summary>
    Public Class UcCcnTarjetaComanda

        ''' <summary> Referencia fuertemente tipada al pedido cargado </summary>
        Private _objPedidoModel As CcnPedidoModel

        ''' <summary> Evento disparado cuando el personal de cocina avanza el estado del pedido </summary>
        Public Event CcnCambioEstadoSolicitado(ByVal sender As Object, ByVal intIdPedido As Integer, ByVal enumNuevoEstado As CcnEstadoPedidoEnum)

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        ''' <summary>
        ''' Enlaza y renderiza visualmente un modelo de pedido en la tarjeta.
        ''' </summary>
        ''' <param name="objPedido">Instancia del modelo de comanda de cocina</param>
        Public Sub CargarComanda(ByVal objPedido As CcnPedidoModel)
            If objPedido Is Nothing Then Return

            _objPedidoModel = objPedido

            ' 1. Configuración de identificador y etiquetas principales
            If objPedido.StrTipoServicio.IndexOf("Entrega", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               objPedido.StrTipoServicio.IndexOf("Llevar", StringComparison.OrdinalIgnoreCase) >= 0 Then
                lblCcnTagMesa.Text = $"🛍 ENTREGAS  {objPedido.StrCodigoComanda}"
                lblCcnMozoOCliente.Text = $"Cliente: {objPedido.StrNombreCliente}"
            Else
                lblCcnTagMesa.Text = $"{objPedido.StrMesaCliente.ToUpper()}  {objPedido.StrCodigoComanda}"
                lblCcnMozoOCliente.Text = If(Not String.IsNullOrWhiteSpace(objPedido.StrNombreMozo), $"Mozo: {objPedido.StrNombreMozo}", $"Cliente: {objPedido.StrNombreCliente}")
            End If

            ' 2. Tiempo transcurrido con alerta de semaforización
            Dim intMinutos As Integer = objPedido.ObtenerMinutosTranscurridos()
            lblCcnTiempoTranscurrido.Text = $"⏱ {objPedido.FormatearTiempoTranscurrido()}"

            If intMinutos >= 15 AndAlso objPedido.EnumEstado <> CcnEstadoPedidoEnum.Listo Then
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorDanger
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Bold)
            Else
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorTextMuted
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.0F, FontStyle.Regular)
            End If

            ' 3. Estado visual, badges de semáforo y configuración del botón de acción
            ConfigurarEstadoVisual(objPedido.EnumEstado)

            ' 4. Métodos de pago y total económico
            lblCcnMetodoPago.Text = $"💳 {objPedido.StrMetodoPago}"
            lblCcnMontoTotal.Text = $"Total: ${objPedido.CalcularTotal():N2}"

            ' 5. Renderizado desacoplado de la lista de platos
            RenderizarItemsComanda(objPedido.LstDetallePlatos)
        End Sub

        ''' <summary>
        ''' Configura la estética, colores y texto de acción según el estado del pedido.
        ''' </summary>
        Private Sub ConfigurarEstadoVisual(ByVal enumEstado As CcnEstadoPedidoEnum)
            Select Case enumEstado
                Case CcnEstadoPedidoEnum.Recibido
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorWarning
                    lblCcnBadgeEstado.Text = "⏳ Pendiente"
                    lblCcnBadgeEstado.BackColor = Color.FromArgb(254, 243, 230)
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorWarning
                    btnCcnAccionPrincipal.Text = "🍳 Enviar a Cocina"
                    btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorPrimary

                Case CcnEstadoPedidoEnum.EnPreparacion
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorPrimary
                    lblCcnBadgeEstado.Text = "🔥 En Cocina"
                    lblCcnBadgeEstado.BackColor = ThemeConfig.ColorPrimaryLight
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorPrimaryDark
                    btnCcnAccionPrincipal.Text = "✔ Marcar Listo"
                    btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorTertiarySuccess

                Case CcnEstadoPedidoEnum.Listo
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorTertiarySuccess
                    lblCcnBadgeEstado.Text = "✔ Listo para Entrega"
                    lblCcnBadgeEstado.BackColor = ThemeConfig.ColorTertiaryLight
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorTertiarySuccess
                    btnCcnAccionPrincipal.Text = "📦 Entregar & Finalizar"
                    btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorSecondary

                Case Else
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorBorder
                    lblCcnBadgeEstado.Text = "Despachado"
                    lblCcnBadgeEstado.BackColor = ThemeConfig.ColorBackgroundSidebar
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorTextMuted
                    btnCcnAccionPrincipal.Visible = False
            End Select
        End Sub

        ''' <summary>
        ''' Dibuja cada plato de la comanda dentro del panel de flujo.
        ''' </summary>
        Private Sub RenderizarItemsComanda(ByVal lstPlatos As List(Of CcnItemPedidoModel))
            flpCcnPlatos.SuspendLayout()
            flpCcnPlatos.Controls.Clear()

            If lstPlatos IsNot Nothing Then
                For Each objItem As CcnItemPedidoModel In lstPlatos
                    Dim pnlItemFila As New Panel With {
                        .Width = 300,
                        .AutoSize = True,
                        .Margin = New Padding(0, 0, 0, 8),
                        .BackColor = Color.Transparent
                    }

                    ' Encabezado del plato con cantidad y precio
                    Dim lblNombreFila As New Label With {
                        .Text = $"{objItem.IntCantidad}x  {objItem.StrNombrePlato}",
                        .Font = ThemeConfig.ObtenerFuenteCuerpo(9.0F, FontStyle.Bold),
                        .ForeColor = ThemeConfig.ColorNeutralDark,
                        .Dock = DockStyle.Top,
                        .AutoSize = True
                    }
                    pnlItemFila.Controls.Add(lblNombreFila)

                    ' Precio a la derecha opcional
                    Dim lblPrecioFila As New Label With {
                        .Text = $"${objItem.CalcularSubtotal():N2}",
                        .Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Regular),
                        .ForeColor = ThemeConfig.ColorTextMuted,
                        .Dock = DockStyle.Right,
                        .AutoSize = True
                    }
                    pnlItemFila.Controls.Add(lblPrecioFila)

                    ' Notas / acompañamientos
                    If Not String.IsNullOrWhiteSpace(objItem.StrNotasAcompanamiento) Then
                        Dim lblNotasFila As New Label With {
                            .Text = $"   {objItem.StrNotasAcompanamiento}",
                            .Font = ThemeConfig.ObtenerFuenteCuerpo(8.0F, FontStyle.Regular),
                            .ForeColor = ThemeConfig.ColorTextMuted,
                            .Dock = DockStyle.Bottom,
                            .AutoSize = True
                        }
                        pnlItemFila.Controls.Add(lblNotasFila)
                    End If

                    ' Alerta de celíaco / alergia si aplica
                    If objItem.BlnEsAlertaCeliaco Then
                        Dim pnlAlerta As New Panel With {
                            .BackColor = Color.FromArgb(255, 248, 230),
                            .BorderStyle = BorderStyle.FixedSingle,
                            .Dock = DockStyle.Bottom,
                            .Height = 26,
                            .Margin = New Padding(0, 4, 0, 4)
                        }

                        Dim lblAlerta As New Label With {
                            .Text = If(Not String.IsNullOrWhiteSpace(objItem.StrMensajeAlerta), $"⚠ {objItem.StrMensajeAlerta}", "⚠ CELÍACO: Estrictamente Sin Gluten"),
                            .ForeColor = Color.FromArgb(180, 100, 20),
                            .Font = ThemeConfig.ObtenerFuenteCuerpo(7.8F, FontStyle.Bold),
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleLeft,
                            .Padding = New Padding(4, 0, 0, 0)
                        }
                        pnlAlerta.Controls.Add(lblAlerta)
                        pnlItemFila.Controls.Add(pnlAlerta)
                    End If

                    flpCcnPlatos.Controls.Add(pnlItemFila)
                Next
            End If

            flpCcnPlatos.ResumeLayout(True)
        End Sub

        ''' <summary>
        ''' Controla el clic en el botón de acción para avanzar el pedido en el ciclo de vida de cocina.
        ''' </summary>
        Private Sub btnCcnAccionPrincipal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnAccionPrincipal.Click
            If _objPedidoModel Is Nothing Then Return

            Dim enumProximoEstado As CcnEstadoPedidoEnum

            Select Case _objPedidoModel.EnumEstado
                Case CcnEstadoPedidoEnum.Recibido
                    enumProximoEstado = CcnEstadoPedidoEnum.EnPreparacion
                Case CcnEstadoPedidoEnum.EnPreparacion
                    enumProximoEstado = CcnEstadoPedidoEnum.Listo
                Case CcnEstadoPedidoEnum.Listo
                    enumProximoEstado = CcnEstadoPedidoEnum.Entregado
                Case Else
                    Return
            End Select

            RaiseEvent CcnCambioEstadoSolicitado(Me, _objPedidoModel.IntIdPedido, enumProximoEstado)
        End Sub

    End Class
End Namespace
