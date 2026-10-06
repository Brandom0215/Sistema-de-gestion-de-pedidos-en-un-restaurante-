Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Cocina.Controles
    ''' <summary>
    ''' Control de usuario unificado para la comanda de cocina.
    ''' Muestra: Nombre del cliente, imagen del plato, detalle de pedidos, estado de pago, total
    ''' y modalidad de servicio (Comer en el sitio con mesa o Para llevar).
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
            ThemeConfig.HabilitarDobleBuffer(pnlCcnCuerpoPlatos)
        End Sub

        ''' <summary>
        ''' Enlaza y renderiza visualmente un modelo de pedido en la comanda.
        ''' </summary>
        ''' <param name="objPedido">Instancia del modelo de comanda de cocina</param>
        Public Sub CargarComanda(ByVal objPedido As CcnPedidoModel)
            If objPedido Is Nothing Then Return

            _objPedidoModel = objPedido

            ' 1. Identificador de comanda en cabecera con distintivo de Turno (Orden de Llegada)
            Dim strPrefijoTurno As String = If(objPedido.IntPosicionFifo > 0, $"#Turno-{objPedido.IntPosicionFifo:00} • ", "")
            If objPedido.StrTipoServicio.IndexOf("Llevar", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               objPedido.StrTipoServicio.IndexOf("Entrega", StringComparison.OrdinalIgnoreCase) >= 0 Then
                lblCcnTagMesa.Text = $"{strPrefijoTurno}🛍 LLEVAR {objPedido.StrCodigoComanda}"
            Else
                Dim strMesa As String = If(String.IsNullOrWhiteSpace(objPedido.StrMesaCliente), "MESA", objPedido.StrMesaCliente.ToUpper())
                lblCcnTagMesa.Text = $"{strPrefijoTurno}{strMesa} {objPedido.StrCodigoComanda}"
            End If

            ' 2. Imagen del plato principal gastronómico
            Dim strPlatoPrincipal As String = objPedido.ObtenerPlatoPrincipalNombre()
            picCcnMiniaturaPlato.Image = CcnImagenPlatoHelper.GenerarImagenPlato(strPlatoPrincipal, picCcnMiniaturaPlato.Width, picCcnMiniaturaPlato.Height)

            ' 3. Metadatos unificados: Cliente, Servicio y Tiempo de Espera
            lblCcnNombreCliente.Text = $"👤 Cliente: {If(String.IsNullOrWhiteSpace(objPedido.StrNombreCliente), "Cliente General", objPedido.StrNombreCliente)}"
            lblCcnTipoServicioMesa.Text = objPedido.ObtenerEtiquetaServicioMesa()

            ' Tiempo transcurrido con alerta de semaforización según el orden de llegada
            Dim intMinutos As Integer = objPedido.ObtenerMinutosTranscurridos()
            Dim strEsperaInfo As String = If(objPedido.IntPosicionFifo > 0, $"⏱ Turno #{objPedido.IntPosicionFifo}: {objPedido.FormatearTiempoTranscurrido()}", $"⏱ Espera: {objPedido.FormatearTiempoTranscurrido()}")
            lblCcnTiempoTranscurrido.Text = strEsperaInfo

            If intMinutos >= 15 AndAlso objPedido.EnumEstado <> CcnEstadoPedidoEnum.Listo Then
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorDanger
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Bold)
            ElseIf intMinutos >= 10 AndAlso objPedido.EnumEstado <> CcnEstadoPedidoEnum.Listo Then
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorWarning
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Bold)
            Else
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorTextMuted
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.0F, FontStyle.Regular)
            End If

            ' 4. Estado de Preparación y Badge
            ConfigurarEstadoVisual(objPedido.EnumEstado)

            ' 5. Estado de Pago (Pagado / No Pagado) y Facturación
            If objPedido.BlnEstaPagado Then
                Dim strDetallePago As String = If(Not String.IsNullOrWhiteSpace(objPedido.StrMetodoPago), $"🟢 PAGADO ({objPedido.StrMetodoPago})", "🟢 PAGADO")
                If objPedido.BlnFacturado AndAlso Not String.IsNullOrWhiteSpace(objPedido.StrNumeroFactura) Then
                    strDetallePago &= $" • 🧾 {objPedido.StrNumeroFactura}"
                End If
                lblCcnBadgePago.Text = strDetallePago
                lblCcnBadgePago.BackColor = ThemeConfig.ColorTertiaryLight
                lblCcnBadgePago.ForeColor = ThemeConfig.ColorTertiarySuccess
            Else
                lblCcnBadgePago.Text = "🔴 NO PAGADO (Cobrar en Caja)"
                lblCcnBadgePago.BackColor = Color.FromArgb(252, 235, 235)
                lblCcnBadgePago.ForeColor = ThemeConfig.ColorDanger
            End If

            lblCcnMontoTotal.Text = $"Total: ${objPedido.CalcularTotal():N2}"

            ' 6. Renderizado del detalle de platos del pedido
            RenderizarItemsComanda(objPedido.LstDetallePlatos)
        End Sub

        ''' <summary>
        ''' Configura la estética, colores y texto de acción según el estado del pedido.
        ''' </summary>
        Private Sub ConfigurarEstadoVisual(ByVal enumEstado As CcnEstadoPedidoEnum)
            Select Case enumEstado
                Case CcnEstadoPedidoEnum.Recibido
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorWarning
                    lblCcnBadgeEstado.Text = "⏳ En Espera"
                    lblCcnBadgeEstado.BackColor = Color.FromArgb(254, 243, 230)
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorWarning
                    btnCcnAccionPrincipal.Text = "👨‍🍳 Iniciar Preparación"
                    btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorPrimary

                Case CcnEstadoPedidoEnum.EnPreparacion
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorPrimary
                    lblCcnBadgeEstado.Text = "🔥 En Preparación"
                    lblCcnBadgeEstado.BackColor = ThemeConfig.ColorPrimaryLight
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorPrimaryDark
                    btnCcnAccionPrincipal.Text = "✔ Marcar Listo"
                    btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorTertiarySuccess

                Case CcnEstadoPedidoEnum.Listo
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorTertiarySuccess
                    lblCcnBadgeEstado.Text = "✔ Listo para Servir"
                    lblCcnBadgeEstado.BackColor = ThemeConfig.ColorTertiaryLight
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorTertiarySuccess
                    If _objPedidoModel IsNot Nothing AndAlso _objPedidoModel.BlnEstaPagado Then
                        btnCcnAccionPrincipal.Text = "📦 Despachar / Entregado"
                        btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorSecondary
                    Else
                        btnCcnAccionPrincipal.Text = "🔒 Bloqueado (Cobrar en Caja)"
                        btnCcnAccionPrincipal.BackColor = ThemeConfig.ColorDanger
                    End If

                Case Else
                    pnlCcnBordeSuperior.BackColor = ThemeConfig.ColorBorder
                    lblCcnBadgeEstado.Text = "Despachado"
                    lblCcnBadgeEstado.BackColor = ThemeConfig.ColorBackgroundSidebar
                    lblCcnBadgeEstado.ForeColor = ThemeConfig.ColorTextMuted
                    btnCcnAccionPrincipal.Visible = False
            End Select
        End Sub

        ''' <summary>
        ''' Dibuja cada plato de la comanda dentro del panel vertical garantizando 100% de visibilidad del texto.
        ''' </summary>
        Private Sub RenderizarItemsComanda(ByVal lstPlatos As List(Of CcnItemPedidoModel))
            pnlCcnCuerpoPlatos.SuspendLayout()
            pnlCcnCuerpoPlatos.Controls.Clear()

            If lstPlatos IsNot Nothing AndAlso lstPlatos.Count > 0 Then
                ' Iteramos en reversa para que al apilar con Dock = DockStyle.Top se muestren de arriba hacia abajo
                For intIdx As Integer = lstPlatos.Count - 1 To 0 Step -1
                    Dim objItem As CcnItemPedidoModel = lstPlatos(intIdx)

                    Dim pnlFilaPlato As New Panel With {
                        .Dock = DockStyle.Top,
                        .AutoSize = True,
                        .AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        .Padding = New Padding(0, 3, 0, 8),
                        .BackColor = Color.Transparent
                    }

                    ' 1. Fila de Encabezado del Plato (Nombre a la izquierda, Precio a la derecha)
                    Dim pnlHeaderPlato As New Panel With {
                        .Dock = DockStyle.Top,
                        .Height = 24,
                        .BackColor = Color.Transparent
                    }

                    Dim lblPrecioFila As New Label With {
                        .Text = $"${objItem.CalcularSubtotal():N2}",
                        .Font = ThemeConfig.ObtenerFuenteCuerpo(9.0F, FontStyle.Regular),
                        .ForeColor = ThemeConfig.ColorTextMuted,
                        .Dock = DockStyle.Right,
                        .Width = 65,
                        .TextAlign = ContentAlignment.TopRight,
                        .UseMnemonic = False
                    }
                    pnlHeaderPlato.Controls.Add(lblPrecioFila)

                    Dim lblNombreFila As New Label With {
                        .Text = $"{objItem.IntCantidad}x  {objItem.StrNombrePlato}",
                        .Font = ThemeConfig.ObtenerFuenteCuerpo(9.5F, FontStyle.Bold),
                        .ForeColor = ThemeConfig.ColorNeutralDark,
                        .Dock = DockStyle.Fill,
                        .TextAlign = ContentAlignment.MiddleLeft,
                        .UseMnemonic = False
                    }
                    pnlHeaderPlato.Controls.Add(lblNombreFila)

                    pnlFilaPlato.Controls.Add(pnlHeaderPlato)

                    ' 2. Fila de Notas culinarias y acompañamientos
                    If Not String.IsNullOrWhiteSpace(objItem.StrNotasAcompanamiento) Then
                        Dim lblNotasFila As New Label With {
                            .Text = $"   {objItem.StrNotasAcompanamiento}",
                            .Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Regular),
                            .ForeColor = ThemeConfig.ColorTextMuted,
                            .Dock = DockStyle.Top,
                            .AutoSize = True,
                            .Padding = New Padding(10, 1, 0, 2),
                            .UseMnemonic = False
                        }
                        pnlFilaPlato.Controls.Add(lblNotasFila)
                    End If

                    ' 3. Tarjeta de advertencia especial (ej. Alergias, Celíacos)
                    If objItem.BlnEsAlertaCeliaco Then
                        Dim pnlAlerta As New Panel With {
                            .BackColor = Color.FromArgb(255, 248, 230),
                            .BorderStyle = BorderStyle.FixedSingle,
                            .Dock = DockStyle.Top,
                            .Height = 26,
                            .Margin = New Padding(0, 4, 0, 4)
                        }

                        Dim lblAlerta As New Label With {
                            .Text = If(Not String.IsNullOrWhiteSpace(objItem.StrMensajeAlerta), $" ⚠ {objItem.StrMensajeAlerta}", " ⚠ CELÍACO: Estrictamente Sin Gluten"),
                            .ForeColor = Color.FromArgb(180, 100, 20),
                            .Font = ThemeConfig.ObtenerFuenteCuerpo(8.2F, FontStyle.Bold),
                            .Dock = DockStyle.Fill,
                            .TextAlign = ContentAlignment.MiddleLeft,
                            .UseMnemonic = False
                        }
                        pnlAlerta.Controls.Add(lblAlerta)
                        pnlFilaPlato.Controls.Add(pnlAlerta)
                    End If

                    pnlCcnCuerpoPlatos.Controls.Add(pnlFilaPlato)
                Next
            End If

            pnlCcnCuerpoPlatos.ResumeLayout(True)
        End Sub

        ''' <summary>
        ''' Controla el clic en el botón de acción para avanzar el pedido en el ciclo de vida de cocina.
        ''' </summary>
        Private Sub btnCcnAccionPrincipal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnAccionPrincipal.Click
            If _objPedidoModel Is Nothing Then Return

            ' Validación estricta de cobro previo al despacho:
            If _objPedidoModel.EnumEstado = CcnEstadoPedidoEnum.Listo AndAlso Not _objPedidoModel.BlnEstaPagado Then
                MessageBox.Show(
                    $"⛔ DESPACHO BLOQUEADO:{Environment.NewLine}{Environment.NewLine}" &
                    $"La comanda {_objPedidoModel.StrCodigoComanda} para {_objPedidoModel.StrNombreCliente} ({_objPedidoModel.ObtenerEtiquetaServicioMesa()}) " &
                    $"aún NO ha sido pagada en Caja (Monto: ${_objPedidoModel.CalcularTotal():N2}).{Environment.NewLine}{Environment.NewLine}" &
                    $"Por política del restaurante, todo producto debe ser pagado antes de ser entregado o despachado al cliente. " &
                    $"El botón se habilitará automáticamente al registrar el cobro en Caja.",
                    "Validación de Pago Requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Return
            End If

            If Not _objPedidoModel.PuedeAvanzarEstado() Then Return

            Dim enumProximoEstado As CcnEstadoPedidoEnum = _objPedidoModel.ObtenerProximoEstado()
            RaiseEvent CcnCambioEstadoSolicitado(Me, _objPedidoModel.IntIdPedido, enumProximoEstado)
        End Sub

        ''' <summary>
        ''' Clic en la miniatura para visualizar la imagen ampliada del plato.
        ''' </summary>
        Private Sub picCcnMiniaturaPlato_Click(ByVal sender As Object, ByVal e As EventArgs) Handles picCcnMiniaturaPlato.Click
            If _objPedidoModel Is Nothing Then Return
            Dim strPlato As String = _objPedidoModel.ObtenerPlatoPrincipalNombre()

            Using frmZoom As New Form With {
                .Text = $"Plato: {strPlato}",
                .Size = New Size(380, 320),
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.FixedDialog,
                .MaximizeBox = False,
                .MinimizeBox = False,
                .BackColor = ThemeConfig.ColorBackgroundCard
            }
                Dim picGrande As New PictureBox With {
                    .Dock = DockStyle.Fill,
                    .SizeMode = PictureBoxSizeMode.CenterImage,
                    .Image = CcnImagenPlatoHelper.GenerarImagenPlato(strPlato, 340, 260)
                }
                frmZoom.Controls.Add(picGrande)
                frmZoom.ShowDialog(Me)
            End Using
        End Sub

        ''' <summary>
        ''' Actualiza el cronómetro y la semaforización de la comanda sin reconstruir los controles.
        ''' </summary>
        Public Sub ActualizarCronometro()
            If _objPedidoModel Is Nothing Then Return

            Dim intMinutos As Integer = _objPedidoModel.ObtenerMinutosTranscurridos()
            Dim strEsperaInfo As String = If(_objPedidoModel.IntPosicionFifo > 0, $"⏱ Turno #{_objPedidoModel.IntPosicionFifo}: {_objPedidoModel.FormatearTiempoTranscurrido()}", $"⏱ Espera: {_objPedidoModel.FormatearTiempoTranscurrido()}")
            lblCcnTiempoTranscurrido.Text = strEsperaInfo

            If intMinutos >= 15 AndAlso _objPedidoModel.EnumEstado <> CcnEstadoPedidoEnum.Listo Then
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorDanger
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Bold)
            ElseIf intMinutos >= 10 AndAlso _objPedidoModel.EnumEstado <> CcnEstadoPedidoEnum.Listo Then
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorWarning
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Bold)
            Else
                lblCcnTiempoTranscurrido.ForeColor = ThemeConfig.ColorTextMuted
                lblCcnTiempoTranscurrido.Font = ThemeConfig.ObtenerFuenteCuerpo(8.0F, FontStyle.Regular)
            End If
        End Sub

    End Class
End Namespace
