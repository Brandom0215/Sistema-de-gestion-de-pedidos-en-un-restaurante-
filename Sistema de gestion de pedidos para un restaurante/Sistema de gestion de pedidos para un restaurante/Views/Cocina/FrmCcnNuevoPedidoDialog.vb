Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
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
        End Sub

        Private Sub FrmCcnNuevoPedidoDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            ConfigurarValidacionesEntrada()
            CargarCatalogoPlatos()
            InicializarCombos()

            If Not String.IsNullOrWhiteSpace(_strNombreClienteSugerido) Then
                txtCcnCliente.Text = _strNombreClienteSugerido
            Else
                txtCcnCliente.Text = "Cliente General"
            End If

            ActualizarTotalCalculado()
        End Sub

        ''' <summary>
        ''' Configura validaciones en tiempo real (KeyPress) y límites de longitud (MaxLength)
        ''' para los campos de la comanda manual, protegiendo la base de datos contra desbordamientos.
        ''' </summary>
        Private Sub ConfigurarValidacionesEntrada()
            ValidadorEntrada.ConfigurarCampoNombreCliente(txtCcnCliente, 40)
            ValidadorEntrada.ConfigurarCampoMesa(txtCcnMesa, 30)
            ValidadorEntrada.ConfigurarCampoDescripcion(txtCcnNotas, 150)
        End Sub

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
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
            Dim dtPlatos As DataTable = PlatoDAO.ObtenerTodos()
            If dtPlatos IsNot Nothing AndAlso dtPlatos.Rows.Count > 0 Then
                For Each row As DataRow In dtPlatos.Rows
                    Dim nombre As String = row("Nombre").ToString()
                    Dim precio As Decimal = Convert.ToDecimal(row("Precio"))
                    Dim etiqueta As String = $"{nombre} (${precio:N2})"
                    If Not _dicPreciosPlatos.ContainsKey(etiqueta) Then
                        _dicPreciosPlatos.Add(etiqueta, precio)
                    End If
                Next
            End If

            cboCcnPlato.Items.Clear()
            For Each strPlato In _dicPreciosPlatos.Keys
                cboCcnPlato.Items.Add(strPlato)
            Next
            If cboCcnPlato.Items.Count > 0 Then
                cboCcnPlato.SelectedIndex = 0
            End If
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
            If Not ValidadorEntrada.EsNombreClienteValido(strCliente) Then
                MessageBox.Show("Por favor ingrese un nombre de cliente válido (entre 2 y 40 caracteres, solo letras).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCcnCliente.Focus()
                Return
            End If

            Dim strMesa As String = txtCcnMesa.Text.Trim()
            If String.IsNullOrWhiteSpace(strMesa) Then
                strMesa = If(cboCcnTipoServicio.SelectedIndex = 1, "ENTREGAS", "Mesa 01")
            End If
            If Not ValidadorEntrada.EsMesaValida(strMesa) Then
                MessageBox.Show("Por favor indique una mesa o identificador de entrega válido (entre 1 y 30 caracteres).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCcnMesa.Focus()
                Return
            End If

            If cboCcnPlato.SelectedIndex < 0 OrElse cboCcnPlato.SelectedItem Is Nothing Then
                MessageBox.Show("Por favor seleccione un plato del menú.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cboCcnPlato.Focus()
                Return
            End If

            Dim strServicio As String = If(cboCcnTipoServicio.SelectedIndex = 1, "Para Llevar", "Comer en el Sitio")
            Dim strPlatoSeleccionado As String = cboCcnPlato.SelectedItem.ToString()
            Dim intCantidad As Integer = Convert.ToInt32(numCcnCantidad.Value)
            If intCantidad <= 0 Then
                MessageBox.Show("La cantidad a ordenar debe ser al menos 1.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                numCcnCantidad.Focus()
                Return
            End If

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
                MostrarMensajeExito($"¡Comanda generada exitosamente!{vbCrLf}{vbCrLf}Cliente: {strCliente}{vbCrLf}Mesa: {strMesa} ({strServicio}){vbCrLf}Plato: {strPlatoFinal}{vbCrLf}Total: ${decTotal:N2}{vbCrLf}Estado Pago: {strDetallePago}", "Orden Recibida en Cocina")
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MostrarMensajeError("No se pudo registrar la comanda en la base de datos.", "Error")
            End If
        End Sub

        Private Sub btnCcnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnCancelar.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

    End Class
End Namespace
