Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Cocina
    ''' <summary>
    ''' Diálogo modal para registrar una nueva orden o comanda desde el Monitor de Cocina o Autoatención de Clientes.
    ''' Conecta de forma inmediata con:
    ''' - Cocina: Genera la comanda en estado Recibido.
    ''' - Caja: Registra la orden como Pendiente de cobro o Cobrada.
    ''' - Facturación: Queda disponible para emitir comprobante una vez pagada.
    ''' </summary>
    Public Class FrmCcnNuevoPedidoDialog

        ''' <summary> Precios unitarios de platos precargados en el menú </summary>
        Private ReadOnly _dicPreciosPlatos As New Dictionary(Of String, Decimal)()

        ''' <summary> Nombre del usuario cliente sugerido en caso de sesión activa </summary>
        Private ReadOnly _strNombreClienteSugerido As String

        Public Sub New()
            Me.New(String.Empty)
        End Sub

        Public Sub New(ByVal strClienteSugerido As String)
            InitializeComponent()
            _strNombreClienteSugerido = strClienteSugerido
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmCcnNuevoPedidoDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarCatalogoPlatos()
            InicializarCombos()

            If Not String.IsNullOrWhiteSpace(_strNombreClienteSugerido) Then
                txtCcnCliente.Text = _strNombreClienteSugerido
            Else
                txtCcnCliente.Text = "Cliente General"
            End If

            ActualizarTotalCalculado()
        End Sub

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCcnHeaderModal.BackColor = ThemeConfig.ColorBackgroundSidebar
            pnlCcnContenedorForm.BackColor = Color.White
            pnlCcnBotonesAccion.BackColor = ThemeConfig.ColorBackgroundSidebar

            lblCcnTituloModal.ForeColor = ThemeConfig.ColorNeutralDark
            lblCcnTituloModal.Font = ThemeConfig.ObtenerFuenteTitulo(13.0F, FontStyle.Bold)
            lblCcnSubtituloModal.ForeColor = ThemeConfig.ColorTextMuted

            ThemeConfig.EstilizarBotonPrimario(btnCcnGuardarPedido)
            ThemeConfig.EstilizarBotonSecundario(btnCcnCancelar)
        End Sub

        Private Sub CargarCatalogoPlatos()
            _dicPreciosPlatos.Clear()
            _dicPreciosPlatos.Add("Bife de Chorizo a la Brasa ($24.00)", 24.0D)
            _dicPreciosPlatos.Add("Pollo al Limón y Romero a la Leña ($26.00)", 26.0D)
            _dicPreciosPlatos.Add("Risotto de Hongos Silvestres ($24.00)", 24.0D)
            _dicPreciosPlatos.Add("Ceviche Mixto Tradicional ($16.50)", 16.5D)
            _dicPreciosPlatos.Add("Pasta Fresca al Pesto de Pistacho ($18.00)", 18.0D)
            _dicPreciosPlatos.Add("Hamburguesa Gourmet Rústica ($12.00)", 12.0D)
            _dicPreciosPlatos.Add("Lomo a la Brasa Tradicional ($18.50)", 18.5D)
            _dicPreciosPlatos.Add("Ensalada Oliva & Burrata ($12.50)", 12.5D)
            _dicPreciosPlatos.Add("Focaccia Artesanal de Romero ($8.00)", 8.0D)

            cboCcnPlato.Items.Clear()
            For Each strPlato In _dicPreciosPlatos.Keys
                cboCcnPlato.Items.Add(strPlato)
            Next
            cboCcnPlato.SelectedIndex = 0
        End Sub

        Private Sub InicializarCombos()
            ' Tipos de servicio
            cboCcnTipoServicio.Items.Clear()
            cboCcnTipoServicio.Items.Add("🍽 Comer en el Sitio (Mesa)")
            cboCcnTipoServicio.Items.Add("🛍 Para Llevar (Entregas)")
            cboCcnTipoServicio.SelectedIndex = 0

            ' Estados de pago inicial
            cboCcnEstadoPago.Items.Clear()
            cboCcnEstadoPago.Items.Add("🔴 Pendiente de Pago (Cobrar en Caja)")
            cboCcnEstadoPago.Items.Add("🟢 Pagado de Inmediato - Efectivo")
            cboCcnEstadoPago.Items.Add("🟢 Pagado de Inmediato - Tarjeta POS")
            cboCcnEstadoPago.Items.Add("🟢 Pagado de Inmediato - Web / Stripe")
            cboCcnEstadoPago.SelectedIndex = 0
        End Sub

        Private Sub cboCcnTipoServicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCcnTipoServicio.SelectedIndexChanged
            If cboCcnTipoServicio.SelectedIndex = 1 Then
                txtCcnMesa.Text = "ENTREGAS"
            Else
                If txtCcnMesa.Text.Equals("ENTREGAS", StringComparison.OrdinalIgnoreCase) Then
                    txtCcnMesa.Text = "Mesa 05"
                End If
            End If
        End Sub

        Private Sub cboCcnPlato_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCcnPlato.SelectedIndexChanged
            ActualizarTotalCalculado()
        End Sub

        Private Sub numCcnCantidad_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numCcnCantidad.ValueChanged
            ActualizarTotalCalculado()
        End Sub

        Private Sub ActualizarTotalCalculado()
            Dim decPrecioUnitario As Decimal = 24.0D
            If cboCcnPlato.SelectedItem IsNot Nothing AndAlso _dicPreciosPlatos.ContainsKey(cboCcnPlato.SelectedItem.ToString()) Then
                decPrecioUnitario = _dicPreciosPlatos(cboCcnPlato.SelectedItem.ToString())
            End If

            Dim intCantidad As Integer = Convert.ToInt32(numCcnCantidad.Value)
            Dim decTotalCalculado As Decimal = decPrecioUnitario * intCantidad
            lblCcnResumenTotal.Text = $"Total a Pagar: ${decTotalCalculado:N2}"
        End Sub

        Private Sub btnCcnGuardarPedido_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnGuardarPedido.Click
            Dim strCliente As String = txtCcnCliente.Text.Trim()
            If String.IsNullOrWhiteSpace(strCliente) Then
                MessageBox.Show("Por favor ingrese el nombre del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCcnCliente.Focus()
                Return
            End If

            Dim strMesa As String = txtCcnMesa.Text.Trim()
            If String.IsNullOrWhiteSpace(strMesa) Then
                strMesa = If(cboCcnTipoServicio.SelectedIndex = 1, "ENTREGAS", "Mesa 01")
            End If

            Dim strServicio As String = If(cboCcnTipoServicio.SelectedIndex = 1, "Para Llevar", "Comer en el Sitio")
            Dim strPlatoSeleccionado As String = cboCcnPlato.SelectedItem.ToString()
            Dim intCantidad As Integer = Convert.ToInt32(numCcnCantidad.Value)

            ' Formar descripción del plato con su cantidad si es > 1
            Dim strPlatoFinal As String = If(intCantidad > 1, $"{intCantidad}x {strPlatoSeleccionado}", strPlatoSeleccionado)

            ' Acompañamiento y notas
            Dim strNotas As String = txtCcnNotas.Text.Trim()
            If chkCcnAlergenoCeliaco.Checked Then
                strNotas = If(String.IsNullOrWhiteSpace(strNotas), "CELÍACO: Estrictamente Sin Gluten", $"CELÍACO: Estrictamente Sin Gluten. {strNotas}")
            End If
            If String.IsNullOrWhiteSpace(strNotas) Then
                strNotas = "Orden estándar de cocina"
            End If

            ' Calcular total
            Dim decPrecioUnitario As Decimal = If(_dicPreciosPlatos.ContainsKey(strPlatoSeleccionado), _dicPreciosPlatos(strPlatoSeleccionado), 20.0D)
            Dim decTotal As Decimal = decPrecioUnitario * intCantidad

            ' Determinar estado de pago
            Dim strEstadoPago As String = "PENDIENTE"
            Dim strMetodoPago As String = ""

            Select Case cboCcnEstadoPago.SelectedIndex
                Case 1
                    strEstadoPago = "PAGADO"
                    strMetodoPago = "Efectivo"
                Case 2
                    strEstadoPago = "PAGADO"
                    strMetodoPago = "Tarjeta POS"
                Case 3
                    strEstadoPago = "PAGADO"
                    strMetodoPago = "Web / Stripe"
                Case Else
                    strEstadoPago = "PENDIENTE"
                    strMetodoPago = ""
            End Select

            ' Guardar en el repositorio centralizado DAO
            Dim blnGuardado As Boolean = PedidoDAO.Guardar(
                strCliente, strMesa, strPlatoFinal, strNotas, strServicio,
                decTotal, strEstadoPago, strMetodoPago, "RECIBIDO"
            )

            If blnGuardado Then
                Dim strDetallePago As String = If(strEstadoPago = "PAGADO", $"Pagado con {strMetodoPago}", "Pendiente de cobro (disponible en Caja)")
                MessageBox.Show($"¡Comanda generada exitosamente!{vbCrLf}{vbCrLf}Cliente: {strCliente}{vbCrLf}Mesa: {strMesa} ({strServicio}){vbCrLf}Plato: {strPlatoFinal}{vbCrLf}Total: ${decTotal:N2}{vbCrLf}Estado Pago: {strDetallePago}", "Orden Recibida en Cocina", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("No se pudo registrar la comanda en memoria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        Private Sub btnCcnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

    End Class
End Namespace
