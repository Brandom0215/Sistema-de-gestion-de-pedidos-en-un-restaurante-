Imports System
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Caja
    ''' <summary>
    ''' Formulario de Caja & Procesamiento de Pagos (RF-006, RF-011, CU-006).
    ''' Permite al Cajero inspeccionar pedidos en estado PENDIENTE, calcular vuelto/cambio en efectivo
    ''' o procesar pagos electrónicos, confirmando la venta y liberando la orden a cocina.
    ''' </summary>
    Public Class FrmCajaCobros

        Private _idPedidoSeleccionado As Integer = 0
        Private _totalAPagar As Decimal = 0D
        Private _subtotalGravable As Decimal = 0D
        Private _impuestoCalculado As Decimal = 0D

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub FrmCajaCobros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarPedidosPendientes()

            ' Suscribir a notificaciones de persistencia en tiempo real de PedidoDAO
            PedidoDAO.SuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
            PedidoDAO.SuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)
        End Sub

        Private Sub FrmCajaCobros_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            PedidoDAO.DesuscribirPedidoRegistrado(AddressOf OnPedidoActualizadoDesdeDAO)
            PedidoDAO.DesuscribirPedidoModificado(AddressOf OnPedidoActualizadoDesdeDAO)
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
        ''' Aplica la paleta visual oficial y estilos de controles.
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

            ' Estilizado de la grilla
            dgvPedidosPendientes.BackgroundColor = Color.White
            dgvPedidosPendientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 230, 220)
            dgvPedidosPendientes.DefaultCellStyle.SelectionForeColor = ThemeConfig.ColorNeutralDark
            dgvPedidosPendientes.ColumnHeadersDefaultCellStyle.BackColor = ThemeConfig.ColorSecondary
            dgvPedidosPendientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvPedidosPendientes.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            dgvPedidosPendientes.EnableHeadersVisualStyles = False
            dgvPedidosPendientes.RowTemplate.Height = 28
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
                lblTotalPendientes.Text = $"Comandas pendientes de cobro: {vista.Count}"

                If dgvPedidosPendientes.Columns.Count > 0 Then
                    If dgvPedidosPendientes.Columns.Contains("ID") Then
                        dgvPedidosPendientes.Columns("ID").HeaderText = "ID"
                        dgvPedidosPendientes.Columns("ID").Width = 50
                    End If
                    If dgvPedidosPendientes.Columns.Contains("FechaHora") Then
                        dgvPedidosPendientes.Columns("FechaHora").HeaderText = "Fecha / Hora"
                        dgvPedidosPendientes.Columns("FechaHora").Width = 140
                    End If
                    If dgvPedidosPendientes.Columns.Contains("Cliente") Then
                        dgvPedidosPendientes.Columns("Cliente").HeaderText = "Cliente"
                        dgvPedidosPendientes.Columns("Cliente").Width = 160
                    End If
                    If dgvPedidosPendientes.Columns.Contains("Mesa") Then
                        dgvPedidosPendientes.Columns("Mesa").HeaderText = "Mesa"
                        dgvPedidosPendientes.Columns("Mesa").Width = 80
                    End If
                    If dgvPedidosPendientes.Columns.Contains("PlatoPrincipal") Then
                        dgvPedidosPendientes.Columns("PlatoPrincipal").HeaderText = "Plato / Comanda"
                        dgvPedidosPendientes.Columns("PlatoPrincipal").Width = 210
                    End If
                    If dgvPedidosPendientes.Columns.Contains("Total") Then
                        dgvPedidosPendientes.Columns("Total").HeaderText = "Total"
                        dgvPedidosPendientes.Columns("Total").DefaultCellStyle.Format = "C2"
                        dgvPedidosPendientes.Columns("Total").Width = 85
                    End If

                    ' Ocultar columnas secundarias para mantener la grilla despejada
                    Dim columnasOcultas = {"Acompanamientos", "TipoServicio", "PrecioUnitario", "Subtotal", "Impuesto",
                                           "Estado", "MetodoPago", "MontoRecibido", "Cambio", "Facturado", "NumeroFactura",
                                           "RUC_Cedula", "RazonSocial", "DireccionFiscal", "TelefonoCliente", "CorreoCliente", "FechaCobro"}
                    For Each col In columnasOcultas
                        If dgvPedidosPendientes.Columns.Contains(col) Then
                            dgvPedidosPendientes.Columns(col).Visible = False
                        End If
                    Next
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

                lblDetallePedidoId.Text = $"Comanda #{_idPedidoSeleccionado}"
                lblDetalleCliente.Text = $"Cliente: {cliente}"
                lblDetalleMesa.Text = $"Modalidad: {servicio} • Mesa: {mesa}"
                lblDetallePlato.Text = $"Plato: {plato}"
                lblDetalleAcomp.Text = $"Acomp: {acomp}"

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
            If Decimal.TryParse(montoStr, NumberStyles.Any, CultureInfo.CurrentCulture, montoRecibido) OrElse
               Decimal.TryParse(montoStr, NumberStyles.Any, CultureInfo.InvariantCulture, montoRecibido) Then

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
                MessageBox.Show("Por favor, seleccione una comanda pendiente de la lista para proceder con el cobro.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim metodo As String = "Efectivo"
            Dim montoRecibido As Decimal = _totalAPagar
            Dim cambio As Decimal = 0D

            If rbEfectivo.Checked Then
                metodo = "Efectivo"
                Dim montoStr As String = txtMontoRecibido.Text.Trim()
                If String.IsNullOrWhiteSpace(montoStr) Then
                    MessageBox.Show("Por favor, ingrese el monto en efectivo recibido por el cliente.", "Dato Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtMontoRecibido.Focus()
                    Return
                End If

                If Not (Decimal.TryParse(montoStr, NumberStyles.Any, CultureInfo.CurrentCulture, montoRecibido) OrElse
                        Decimal.TryParse(montoStr, NumberStyles.Any, CultureInfo.InvariantCulture, montoRecibido)) Then
                    MessageBox.Show("El monto ingresado no es un número válido. Verifique el formato.", "Monto Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtMontoRecibido.Focus()
                    Return
                End If

                If montoRecibido < _totalAPagar Then
                    Dim faltante As Decimal = _totalAPagar - montoRecibido
                    MessageBox.Show($"El monto recibido (${montoRecibido:N2}) es insuficiente para cubrir el total de ${_totalAPagar:N2}. Faltan ${faltante:N2}.", "Pago Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtMontoRecibido.Focus()
                    Return
                End If

                cambio = montoRecibido - _totalAPagar
            ElseIf rbTarjeta.Checked Then
                metodo = "Tarjeta POS"
                montoRecibido = _totalAPagar
                cambio = 0D
            Else
                metodo = "Transferencia / QR"
                montoRecibido = _totalAPagar
                cambio = 0D
            End If

            ' Confirmación de operación crítica
            Dim mensajePregunta As String = $"¿Desea confirmar el cobro de la Comanda #{_idPedidoSeleccionado}?" & vbCrLf & vbCrLf &
                                            $"• Total a Cobrar: ${_totalAPagar:N2}" & vbCrLf &
                                            $"• Método de Pago: {metodo}" & vbCrLf &
                                            If(metodo = "Efectivo", $"• Recibido: ${montoRecibido:N2} | Vuelto: ${cambio:N2}{vbCrLf}", "") &
                                            $"• Destino: Transmisión inmediata a Cocina KDS"

            If ConfirmarAccion(mensajePregunta, "Confirmar Transacción en Caja") Then
                Dim idCobrado As Integer = _idPedidoSeleccionado
                Dim exito = PedidoDAO.ConfirmarCobro(idCobrado, metodo, montoRecibido, cambio)
                If exito Then
                    Dim mensajeExito As String = $"¡Cobro de la Comanda #{idCobrado} registrado exitosamente!" & vbCrLf & vbCrLf &
                                                 $"La orden ha sido autorizada y enviada a la pantalla de cocina (KDS)." & vbCrLf &
                                                 If(cambio > 0, $"Entregar cambio al cliente: ${cambio:N2}", "")
                    MostrarMensajeExito(mensajeExito, "Cobro Exitoso")
                    LimpiarDetalle()
                    CargarPedidosPendientes()

                    ' Flujo POS del Mundo Real: Diálogo inmediato de comprobante fiscal para la orden cobrada
                    If ConfirmarAccion($"¿Desea emitir o imprimir el comprobante fiscal de la Comanda #{idCobrado} ahora?", "Emisión de Comprobante Fiscal") Then
                        Using dlg As New FrmCobroComprobanteDialog(idCobrado)
                            dlg.ShowDialog(Me)
                        End Using
                    End If
                Else
                    MostrarMensajeError("No se pudo actualizar el estado del pedido en el repositorio.", "Error al Cobrar")
                End If
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

            lblDetallePedidoId.Text = "Comanda: (Sin Selección)"
            lblDetalleCliente.Text = "Cliente: Seleccione una orden"
            lblDetalleMesa.Text = "Mesa / Servicio: --"
            lblDetallePlato.Text = "Plato: Ninguno"
            lblDetalleAcomp.Text = "Acomp: --"

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
