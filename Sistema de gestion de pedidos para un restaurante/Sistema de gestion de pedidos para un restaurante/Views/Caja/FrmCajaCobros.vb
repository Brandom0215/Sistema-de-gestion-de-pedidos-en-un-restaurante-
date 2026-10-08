Imports System
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Caja
    ''' <summary>
    ''' Formulario de Caja & Procesamiento de Pagos Responsivo Táctil (RF-006, RF-011, CU-006).
    ''' Permite al Cajero inspeccionar pedidos en estado PENDIENTE con controles de toque amplios,
    ''' calcular vuelto/cambio en efectivo o procesar pagos electrónicos, confirmando la venta y liberando la orden a cocina.
    ''' </summary>
    Public Class FrmCajaCobros

        Private _idPedidoSeleccionado As Integer = 0
        Private _totalAPagar As Decimal = 0D
        Private _subtotalGravable As Decimal = 0D
        Private _impuestoCalculado As Decimal = 0D

        ' Indicador visual de estado de conexión al servidor
        Private _btnBadgeConexion As Button

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private WithEvents _tmrAutoRefresh As Timer

        Private Sub FrmCajaCobros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            ConfigurarValidacionesEntrada()
            InicializarControlesConexionBD()
            VerificarConexionAutomatica()
            CargarPedidosPendientes()

            ' Suscribir a notificaciones de persistencia en tiempo real de PedidoDAO
            PedidoDAO.SuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
            PedidoDAO.SuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)

            _tmrAutoRefresh = New Timer() With {.Interval = 3000, .Enabled = True}
        End Sub

        ''' <summary>
        ''' Configura validaciones en tiempo real y límites de longitud para evitar desbordamientos y caracteres inválidos.
        ''' </summary>
        Private Sub ConfigurarValidacionesEntrada()
            ValidadorEntrada.ConfigurarCampoBusqueda(txtBuscar, 50)
            ValidadorEntrada.ConfigurarCampoMoneda(txtMontoRecibido, 11, 8, 2)
        End Sub

        Private Sub _tmrAutoRefresh_Tick(sender As Object, e As EventArgs) Handles _tmrAutoRefresh.Tick
            Try
                CargarPedidosPendientes()
            Catch
            End Try
        End Sub

        Private Sub dgvPedidosPendientes_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvPedidosPendientes.CellFormatting
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                Dim colName As String = dgvPedidosPendientes.Columns(e.ColumnIndex).Name
                If colName = "FechaHora" AndAlso e.Value IsNot Nothing Then
                    Dim dtVal As DateTime
                    If DateTime.TryParse(e.Value.ToString(), dtVal) Then
                        e.Value = dtVal.ToString("HH:mm:ss")
                        e.FormattingApplied = True
                    End If
                ElseIf colName = "EstadoCocina" AndAlso e.Value IsNot Nothing Then
                    Dim st = e.Value.ToString().Trim().ToUpper()
                    Select Case st
                        Case "RECIBIDO"
                            e.Value = "En Espera"
                            e.CellStyle.ForeColor = Color.FromArgb(180, 100, 0)
                            e.CellStyle.BackColor = Color.FromArgb(255, 248, 225)
                        Case "EN_PREPARACION", "ENPREPARACION"
                            e.Value = "Cocinando"
                            e.CellStyle.ForeColor = Color.FromArgb(190, 80, 0)
                            e.CellStyle.BackColor = Color.FromArgb(254, 237, 220)
                        Case "LISTO"
                            e.Value = "Listo"
                            e.CellStyle.ForeColor = Color.FromArgb(34, 139, 34)
                            e.CellStyle.BackColor = Color.FromArgb(232, 245, 233)
                        Case "ENTREGADO", "DESPACHADO"
                            e.Value = "Entregado"
                            e.CellStyle.ForeColor = Color.FromArgb(47, 79, 79)
                            e.CellStyle.BackColor = Color.FromArgb(240, 244, 248)
                    End Select
                    e.CellStyle.Font = ThemeConfig.ObtenerFuenteCuerpo(9.5F, FontStyle.Bold)
                    e.FormattingApplied = True
                End If
            End If
        End Sub

        Private Sub FrmCajaCobros_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            If _tmrAutoRefresh IsNot Nothing Then
                _tmrAutoRefresh.Stop()
                _tmrAutoRefresh.Dispose()
            End If
            PedidoDAO.DesuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
            PedidoDAO.DesuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)
            ConexionBD.DesuscribirModoConexionCambiado(AddressOf OnModoConexionCambiado)
        End Sub

        ''' <summary>
        ''' Configura la insignia visual de estado de conexión en el encabezado de Caja.
        ''' </summary>
        Private Sub InicializarControlesConexionBD()
            _btnBadgeConexion = New Button() With {
                .Size = New Size(230, 36),
                .Location = New Point(pnlHeader.Width - 245, 12),
                .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
                .Font = ThemeConfig.ObtenerFuenteSubtitulo(8.5F, FontStyle.Bold),
                .Cursor = Cursors.Hand,
                .FlatStyle = FlatStyle.Flat,
                .UseVisualStyleBackColor = False
            }
            AddHandler _btnBadgeConexion.Click, AddressOf BtnBadgeConexion_Click
            pnlHeader.Controls.Add(_btnBadgeConexion)

            ActualizarVisualEstadoBD()
            ConexionBD.SuscribirModoConexionCambiado(AddressOf OnModoConexionCambiado)
        End Sub

        ''' <summary>
        ''' Valida la disponibilidad del servidor central de manera automática al abrir la vista.
        ''' Si no se detecta red universitaria, activa de inmediato el modo local de respaldo y notifica amigablemente.
        ''' </summary>
        Private Sub VerificarConexionAutomatica()
            If ConexionBD.ModoConexion = ModoConexionEnum.ServidorPrincipal Then
                Dim msgPrueba As String = ""
                Dim disponible As Boolean = ConexionBD.ProbarConexion(msgPrueba)
                If Not disponible Then
                    ConexionBD.RegistrarFalloServidor()
                    If Not ConexionBD.AvisoContingenciaMostrado Then
                        ConexionBD.AvisoContingenciaMostrado = True
                        MessageBox.Show(
                            "No fue posible establecer conexión con el servidor principal del restaurante en este momento." & vbCrLf & vbCrLf &
                            "Para que puedas seguir registrando cobros y atendiendo a los clientes sin interrupciones, el sistema continuará en modo local automáticamente.",
                            "Aviso del Sistema",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        )
                    End If
                End If
            End If
            ActualizarVisualEstadoBD()
        End Sub

        Private Sub ActualizarVisualEstadoBD()
            If _btnBadgeConexion Is Nothing Then Return

            If ConexionBD.ModoConexion = ModoConexionEnum.ServidorPrincipal Then
                _btnBadgeConexion.Text = $"Servidor Conectado ({ConexionBD.Host})"
                _btnBadgeConexion.BackColor = Color.FromArgb(232, 245, 233)
                _btnBadgeConexion.ForeColor = Color.FromArgb(27, 94, 32)
                _btnBadgeConexion.FlatAppearance.BorderColor = Color.FromArgb(76, 175, 80)
            Else
                _btnBadgeConexion.Text = "Modo Local (Sin Conexión)"
                _btnBadgeConexion.BackColor = Color.FromArgb(255, 248, 225)
                _btnBadgeConexion.ForeColor = Color.FromArgb(179, 90, 0)
                _btnBadgeConexion.FlatAppearance.BorderColor = Color.FromArgb(255, 179, 0)
            End If
        End Sub

        ''' <summary>
        ''' Al hacer clic sobre la insignia, permite comprobar o restablecer la conexión con el servidor principal.
        ''' </summary>
        Private Sub BtnBadgeConexion_Click(sender As Object, e As EventArgs)
            If ConexionBD.ModoConexion = ModoConexionEnum.ServidorPrincipal Then
                MessageBox.Show(
                    $"El sistema se encuentra conectado activamente al servidor principal ({ConexionBD.Host}:{ConexionBD.Puerto})." & vbCrLf &
                    "Todos los cobros y operaciones se guardan directamente en la base de datos central.",
                    "Servidor Principal Conectado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )
                Return
            End If

            If MessageBox.Show(
                "¿Deseas verificar si ya es posible conectarse al servidor principal del restaurante?",
                "Comprobar Conexión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) = DialogResult.Yes Then

                Cursor = Cursors.WaitCursor
                Dim resultado As String = ""
                Dim reconectado As Boolean = ConexionBD.ReintentarConexionServidor(resultado)
                Cursor = Cursors.Default

                ActualizarVisualEstadoBD()
                If reconectado Then
                    LimpiarDetalle()
                    CargarPedidosPendientes()
                    MessageBox.Show(resultado, "Conexión Restablecida", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(resultado, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End Sub

        Private Sub OnModoConexionCambiado(nuevoModo As ModoConexionEnum)
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Of ModoConexionEnum)(AddressOf OnModoConexionCambiado), nuevoModo)
                Return
            End If
            ActualizarVisualEstadoBD()
            CargarPedidosPendientes()
        End Sub

        ''' <summary>
        ''' Notificación reactiva inmediata cuando se registra o actualiza un pedido en el sistema.
        ''' </summary>
        Private Sub OnPedidoActualizadoDesdeDAO(idPedido As Integer)
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Of Integer)(AddressOf OnPedidoActualizadoDesdeDAO), idPedido)
                Return
            End If

            CargarPedidosPendientes()
        End Sub

        ''' <summary>
        ''' Aplica la paleta visual oficial y estilos de controles optimizados para toque táctil.
        ''' </summary>
        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlHeader.BackColor = ThemeConfig.ColorBackgroundApp
            pnlContenedor.BackColor = ThemeConfig.ColorBackgroundApp

            lblTituloHeader.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloHeader.ForeColor = ThemeConfig.ColorTextMuted

            grpListado.ForeColor = ThemeConfig.ColorSecondary
            grpDetalleCobro.ForeColor = ThemeConfig.ColorSecondary

            ThemeConfig.EstilizarBotonPrimario(btnConfirmarCobro)
            ThemeConfig.EstilizarBotonSecundario(btnBuscar)
            ThemeConfig.EstilizarBotonSecundario(btnRefrescar)
            ThemeConfig.EstilizarBotonSecundario(btnLimpiar)

            pnlCardCobro.BackColor = ThemeConfig.ColorBackgroundCard
            pnlTotalDestacado.BackColor = ThemeConfig.ColorPrimaryLight

            lblTotalEtiqueta.ForeColor = ThemeConfig.ColorSecondary
            lblTotalValor.ForeColor = ThemeConfig.ColorPrimary

            ' Habilitar doble buffer en paneles contenedor
            ThemeConfig.HabilitarDobleBuffer(pnlCardCobro)
            ThemeConfig.HabilitarDobleBuffer(pnlContenedor)

            ' Configurar la grilla del cajero optimizada para interacción táctil (filas de 46px)
            ThemeConfig.ConfigurarGrillaTouch(dgvPedidosPendientes)
            dgvPedidosPendientes.RowHeadersVisible = False
            dgvPedidosPendientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvPedidosPendientes.RowTemplate.Height = 46
            dgvPedidosPendientes.ColumnHeadersHeight = 44
            dgvPedidosPendientes.DefaultCellStyle.Font = ThemeConfig.ObtenerFuenteCuerpo(11.0F, FontStyle.Regular)
            dgvPedidosPendientes.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(11.0F, FontStyle.Bold)
            dgvPedidosPendientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 248, 245)
        End Sub

        ''' <summary>
        ''' Carga las órdenes pendientes de pago desde el repositorio DAO en memoria.
        ''' </summary>
        Public Sub CargarPedidosPendientes()
            Try
                Dim dtPendientes = PedidoDAO.ObtenerPedidosPendientes()
                Dim vista As New DataView(dtPendientes)

                If Not String.IsNullOrWhiteSpace(txtBuscar.Text) Then
                    Dim criterio As String = txtBuscar.Text.Trim().Replace("'", "''")
                    Dim idNum As Integer
                    If Integer.TryParse(criterio, idNum) Then
                        vista.RowFilter = $"ID = {idNum} OR Cliente LIKE '%{criterio}%'"
                    Else
                        vista.RowFilter = $"Cliente LIKE '%{criterio}%' OR Mesa LIKE '%{criterio}%'"
                    End If
                End If

                dgvPedidosPendientes.DataSource = vista
                lblTotalPendientes.Text = $"Pedidos pendientes de cobro: {vista.Count}"

                If dgvPedidosPendientes.Columns.Count > 0 Then
                    dgvPedidosPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                    Dim columnasVisibles = New HashSet(Of String) From {"ID", "Cliente", "Mesa", "PlatoPrincipal", "EstadoCocina", "FechaHora", "Total"}
                    For Each col As DataGridViewColumn In dgvPedidosPendientes.Columns
                        If Not columnasVisibles.Contains(col.Name) Then
                            col.Visible = False
                        End If
                    Next

                    If dgvPedidosPendientes.Columns.Contains("ID") Then
                        With dgvPedidosPendientes.Columns("ID")
                            .HeaderText = "ID"
                            .FillWeight = 8
                            .MinimumWidth = 45
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .DisplayIndex = 0
                        End With
                    End If
                    If dgvPedidosPendientes.Columns.Contains("Cliente") Then
                        With dgvPedidosPendientes.Columns("Cliente")
                            .HeaderText = "Cliente"
                            .FillWeight = 22
                            .MinimumWidth = 100
                            .DisplayIndex = 1
                        End With
                    End If
                    If dgvPedidosPendientes.Columns.Contains("Mesa") Then
                        With dgvPedidosPendientes.Columns("Mesa")
                            .HeaderText = "Mesa / Servicio"
                            .FillWeight = 16
                            .MinimumWidth = 90
                            .DisplayIndex = 2
                        End With
                    End If
                    If dgvPedidosPendientes.Columns.Contains("PlatoPrincipal") Then
                        With dgvPedidosPendientes.Columns("PlatoPrincipal")
                            .HeaderText = "Pedido / Consumo"
                            .FillWeight = 24
                            .MinimumWidth = 110
                            .DisplayIndex = 3
                        End With
                    End If
                    If dgvPedidosPendientes.Columns.Contains("EstadoCocina") Then
                        With dgvPedidosPendientes.Columns("EstadoCocina")
                            .HeaderText = "Cocina"
                            .FillWeight = 14
                            .MinimumWidth = 85
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .DisplayIndex = 4
                        End With
                    End If
                    If dgvPedidosPendientes.Columns.Contains("FechaHora") Then
                        With dgvPedidosPendientes.Columns("FechaHora")
                            .HeaderText = "Hora"
                            .FillWeight = 10
                            .MinimumWidth = 65
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            .DisplayIndex = 5
                        End With
                    End If
                    If dgvPedidosPendientes.Columns.Contains("Total") Then
                        With dgvPedidosPendientes.Columns("Total")
                            .HeaderText = "Total"
                            .DefaultCellStyle.Format = "C2"
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                            .FillWeight = 10
                            .MinimumWidth = 75
                            .DisplayIndex = 6
                        End With
                    End If
                End If

                If vista.Count = 0 Then
                    LimpiarDetalle()
                End If
            Catch ex As Exception
                MessageBox.Show($"Ocurrió un error al cargar las comandas pendientes: {ex.Message}", "Error en Caja", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub dgvPedidosPendientes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPedidosPendientes.SelectionChanged
            If dgvPedidosPendientes.SelectedRows.Count > 0 Then
                Dim row = dgvPedidosPendientes.SelectedRows(0)
                CargarDatosPedidoSeleccionado(row)
            End If
        End Sub

        Private Sub CargarDatosPedidoSeleccionado(row As DataGridViewRow)
            Try
                _idPedidoSeleccionado = Convert.ToInt32(row.Cells("ID").Value)
                Dim cliente As String = row.Cells("Cliente").Value.ToString()
                Dim mesa As String = row.Cells("Mesa").Value.ToString()
                Dim servicio As String = If(row.Cells("TipoServicio").Value IsNot Nothing, row.Cells("TipoServicio").Value.ToString(), "En Mesa")
                Dim plato As String = row.Cells("PlatoPrincipal").Value.ToString()
                Dim acomp As String = If(row.Cells("Acompanamientos").Value IsNot Nothing, row.Cells("Acompanamientos").Value.ToString(), "")

                _totalAPagar = Convert.ToDecimal(row.Cells("Total").Value)
                _subtotalGravable = Math.Round(_totalAPagar / 1.07D, 2)
                _impuestoCalculado = Math.Round(_totalAPagar - _subtotalGravable, 2)

                Dim estadoCocinaStr As String = If(row.Cells("EstadoCocina").Value IsNot Nothing, row.Cells("EstadoCocina").Value.ToString(), "RECIBIDO")
                Dim estadoCocinaTexto As String = "En Espera"
                Select Case estadoCocinaStr.Trim().ToUpper()
                    Case "EN_PREPARACION", "ENPREPARACION" : estadoCocinaTexto = "En Preparación en Cocina"
                    Case "LISTO" : estadoCocinaTexto = "Listo para Servir / Despachar"
                    Case "ENTREGADO", "DESPACHADO" : estadoCocinaTexto = "Ya Entregado al Cliente"
                    Case Else : estadoCocinaTexto = "En Cola de Cocina"
                End Select

                lblDetallePedidoId.Text = $"Pedido #{_idPedidoSeleccionado}"
                lblDetalleCliente.Text = $"Cliente: {cliente}"
                lblDetalleMesa.Text = $"Servicio: {servicio} • Mesa: {mesa} • Estado: {estadoCocinaTexto}"
                lblDetallePlato.Text = $"Consumo: {plato}"
                lblDetalleAcomp.Text = $"Extras: {If(String.IsNullOrWhiteSpace(acomp), "--", acomp)}"

                lblSubtotalValor.Text = $"${_subtotalGravable:N2}"
                lblImpuestoValor.Text = $"${_impuestoCalculado:N2}"
                lblTotalValor.Text = $"${_totalAPagar:N2}"

                ActualizarModoPago()
            Catch ex As Exception
                LimpiarDetalle()
            End Try
        End Sub

        Private Sub ActualizarModoPago()
            If rbEfectivo.Checked Then
                txtMontoRecibido.Enabled = True
                txtMontoRecibido.Text = ""
                lblCambioValor.Text = "$0.00"
                txtMontoRecibido.Focus()
            Else
                ' En Tarjeta o Digital, el monto recibido es exactamente el total
                txtMontoRecibido.Enabled = False
                txtMontoRecibido.Text = _totalAPagar.ToString("N2", CultureInfo.InvariantCulture)
                lblCambioValor.Text = "$0.00"
            End If
        End Sub

        Private Sub rbMetodoPago_CheckedChanged(sender As Object, e As EventArgs) Handles rbEfectivo.CheckedChanged, rbTarjeta.CheckedChanged, rbDigital.CheckedChanged
            Dim rb = TryCast(sender, RadioButton)
            If rb IsNot Nothing AndAlso rb.Checked Then
                ActualizarModoPago()
            End If
        End Sub

        Private Sub txtMontoRecibido_TextChanged(sender As Object, e As EventArgs) Handles txtMontoRecibido.TextChanged
            If Not rbEfectivo.Checked Then Return

            Dim montoStr As String = txtMontoRecibido.Text.Trim()
            If String.IsNullOrWhiteSpace(montoStr) Then
                lblCambioValor.Text = "$0.00"
                lblCambioValor.ForeColor = ThemeConfig.ColorTertiarySuccess
                Return
            End If

            Dim montoRecibido As Decimal
            If ValidadorEntrada.EsMontoValido(montoStr, montoRecibido, 0.00D, 999999.99D) Then
                Dim cambio As Decimal = montoRecibido - _totalAPagar
                If cambio >= 0 Then
                    lblCambioValor.Text = $"${cambio:N2}"
                    lblCambioValor.ForeColor = ThemeConfig.ColorTertiarySuccess
                Else
                    lblCambioValor.Text = $"Faltan: ${Math.Abs(cambio):N2}"
                    lblCambioValor.ForeColor = ThemeConfig.ColorDanger
                End If
            Else
                lblCambioValor.Text = "Monto Inválido"
                lblCambioValor.ForeColor = ThemeConfig.ColorDanger
            End If
        End Sub

        ''' <summary>
        ''' Confirma el cobro de la comanda, actualiza su estado a PAGADO y la libera para preparación en Cocina.
        ''' </summary>
        Private Sub btnConfirmarCobro_Click(sender As Object, e As EventArgs) Handles btnConfirmarCobro.Click
            If _idPedidoSeleccionado <= 0 Then
                MessageBox.Show("Por favor, elija un pedido pendiente de la lista para cobrar.", "Seleccione un pedido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim metodo As String = "Efectivo"
            Dim montoRecibido As Decimal = _totalAPagar
            Dim cambio As Decimal = 0D

            If rbEfectivo.Checked Then
                metodo = "Efectivo"
                Dim montoStr As String = txtMontoRecibido.Text.Trim()
                If String.IsNullOrWhiteSpace(montoStr) Then
                    MessageBox.Show("Por favor, ingrese el dinero recibido en efectivo.", "Monto requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtMontoRecibido.Focus()
                    Return
                End If

                If Not ValidadorEntrada.EsMontoValido(montoStr, montoRecibido, 0.01D, 999999.99D) Then
                    MessageBox.Show("El valor ingresado no es válido. Escriba un monto numérico correcto (ej: 15.50).", "Monto no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtMontoRecibido.Focus()
                    Return
                End If

                If montoRecibido < _totalAPagar Then
                    Dim faltante As Decimal = _totalAPagar - montoRecibido
                    MessageBox.Show($"El dinero recibido (${montoRecibido:N2}) no cubre el total de ${_totalAPagar:N2}. Faltan ${faltante:N2}.", "Falta dinero", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtMontoRecibido.Focus()
                    Return
                End If

                cambio = montoRecibido - _totalAPagar
            ElseIf rbTarjeta.Checked Then
                metodo = "Tarjeta"
                montoRecibido = _totalAPagar
                cambio = 0D
            Else
                metodo = "Transferencia / QR"
                montoRecibido = _totalAPagar
                cambio = 0D
            End If

            ' Confirmación amigable para el cajero
            Dim mensajePregunta As String = $"¿Desea confirmar el cobro del Pedido #{_idPedidoSeleccionado}?" & vbCrLf & vbCrLf &
                                            $"• Total a pagar: ${_totalAPagar:N2}" & vbCrLf &
                                            $"• Forma de pago: {metodo}" & vbCrLf &
                                            If(metodo = "Efectivo", $"• Recibido: ${montoRecibido:N2} | Cambio: ${cambio:N2}{vbCrLf}", "") &
                                            $"• El pedido pasará a cocina para su preparación."

            If ConfirmarAccion(mensajePregunta, "Confirmar cobro") Then
                Dim idCobrado As Integer = _idPedidoSeleccionado
                Dim exito = PedidoDAO.ConfirmarCobro(idCobrado, metodo, montoRecibido, cambio)
                If exito Then
                    LimpiarDetalle()
                    CargarPedidosPendientes()

                    ' Unificar la confirmación del cobro y la pregunta de facturación en una sola ventana
                    Dim textoCobroFactura As String = $"¡Cobro del pedido #{idCobrado} registrado con éxito!" & vbCrLf &
                                                      $"El pedido ya fue enviado a la cocina." &
                                                      If(cambio > 0, $"{vbCrLf}Cambio a entregar al cliente: ${cambio:N2}", "") & vbCrLf & vbCrLf &
                                                      "¿Desea facturar e imprimir el recibo de este pedido?"

                    If ConfirmarAccion(textoCobroFactura, "Cobro exitoso") Then
                        Using dlg As New FrmCobroComprobanteDialog(idCobrado)
                            dlg.ShowDialog(Me)
                        End Using
                    End If
                Else
                    MostrarMensajeError("No se pudo actualizar el estado del pedido.", "Error al cobrar")
                End If
            End If
        End Sub

        Private Sub txtBuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscar.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                CargarPedidosPendientes()
            End If
        End Sub

        Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
            CargarPedidosPendientes()
        End Sub

        Private Sub btnRefrescar_Click(sender As Object, e As EventArgs) Handles btnRefrescar.Click
            txtBuscar.Clear()
            LimpiarDetalle()
            CargarPedidosPendientes()
        End Sub

        Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
            LimpiarDetalle()
        End Sub

        Private Sub LimpiarDetalle()
            _idPedidoSeleccionado = 0
            _totalAPagar = 0D
            _subtotalGravable = 0D
            _impuestoCalculado = 0D

            lblDetallePedidoId.Text = "Pedido: (Ninguno seleccionado)"
            lblDetalleCliente.Text = "Cliente: Seleccione un pedido"
            lblDetalleMesa.Text = "Mesa / Servicio: --"
            lblDetallePlato.Text = "Consumo: Ninguno"
            lblDetalleAcomp.Text = "Extras: --"

            lblSubtotalValor.Text = "$0.00"
            lblImpuestoValor.Text = "$0.00"
            lblTotalValor.Text = "$0.00"

            txtMontoRecibido.Clear()
            lblCambioValor.Text = "$0.00"
            lblCambioValor.ForeColor = ThemeConfig.ColorTertiarySuccess
            rbEfectivo.Checked = True

            If dgvPedidosPendientes.SelectedRows.Count > 0 Then
                dgvPedidosPendientes.ClearSelection()
            End If
        End Sub

    End Class
End Namespace
