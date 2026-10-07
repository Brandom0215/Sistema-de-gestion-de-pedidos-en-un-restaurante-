Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Pedidos
    ''' <summary>
    ''' Formulario de Menú Digital y Carrito de Pedidos Responsivo.
    ''' Presenta un catálogo visual interactivo de platos con imágenes (PictureBox),
    ''' filtrado dinámico por categorías, extras con cantidades y un carrito de compras.
    ''' </summary>
    Public Class FrmClienteMenu

        Private ReadOnly _nombreUsuarioSesion As String
        Private _tablaCarrito As DataTable

        Public Sub New()
            Me.New("Invitado")
        End Sub

        Public Sub New(nombreUsuario As String)
            InitializeComponent()
            _nombreUsuarioSesion = If(String.IsNullOrWhiteSpace(nombreUsuario), "Invitado", nombreUsuario)
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmClienteMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            InicializarEstructuraCarrito()
            CargarCategorias()
            CargarTiposServicio()
            CargarMetodosPago()
            CargarTarjetasPlatos()

            If Not String.IsNullOrWhiteSpace(_nombreUsuarioSesion) AndAlso Not _nombreUsuarioSesion.Equals("Invitado", StringComparison.OrdinalIgnoreCase) Then
                txtNombreCliente.Text = _nombreUsuarioSesion
                txtCorreoCliente.Text = $"{_nombreUsuarioSesion.ToLowerInvariant().Replace(" ", ".")}@correo.com"
            Else
                txtNombreCliente.Text = "Cliente Invitado"
                txtCorreoCliente.Text = "cliente@correo.com"
            End If

            txtMesa.Text = "Mesa 01"
            CalcularTotalGeneral()
        End Sub

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlHeader.BackColor = Color.White

            lblTituloHeader.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloHeader.ForeColor = ThemeConfig.ColorTextMuted

            pnlFiltrosBarra.BackColor = Color.White
            grpCarrito.ForeColor = ThemeConfig.ColorSecondary
            grpExtrasYBebidas.ForeColor = ThemeConfig.ColorNeutralDark
            grpDatosCliente.ForeColor = ThemeConfig.ColorNeutralDark
            pnlResumenYConfirmacion.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlResumenYConfirmacion)

            ThemeConfig.EstilizarBotonPrimario(btnConfirmarPedido)
            ThemeConfig.EstilizarBotonSecundario(btnEliminarItemCarrito)
            ThemeConfig.EstilizarBotonSecundario(btnVaciarCarrito)

            ' Configurar casillas de selección táctiles con cuadro gigante de 28x28px
            ThemeConfig.AplicarDibujoTouchCheckBox(chkExtraSoda)
            ThemeConfig.AplicarDibujoTouchCheckBox(chkExtraJugo)
            ThemeConfig.AplicarDibujoTouchCheckBox(chkExtraPapas)
            ThemeConfig.AplicarDibujoTouchCheckBox(chkExtraEnsalada)

            ' Transformar selecciones numéricas en Steppers Táctiles gigantes [-] [1] [+]
            ThemeConfig.ReemplazarNumericUpDownConTouchStepper(numExtraSoda)
            ThemeConfig.ReemplazarNumericUpDownConTouchStepper(numExtraJugo)
            ThemeConfig.ReemplazarNumericUpDownConTouchStepper(numExtraPapas)
            ThemeConfig.ReemplazarNumericUpDownConTouchStepper(numExtraEnsalada)

            ' Configurar la grilla del carrito optimizada para interacción táctil
            ThemeConfig.ConfigurarGrillaTouch(dgvCarrito)
        End Sub

        Private Sub InicializarEstructuraCarrito()
            _tablaCarrito = New DataTable("Carrito")
            _tablaCarrito.Columns.Add("ID", GetType(Integer))
            _tablaCarrito.Columns.Add("Plato", GetType(String))
            _tablaCarrito.Columns.Add("Cant", GetType(Integer))
            _tablaCarrito.Columns.Add("Precio", GetType(Decimal))
            _tablaCarrito.Columns.Add("Subtotal", GetType(Decimal))

            dgvCarrito.DataSource = _tablaCarrito
            FormatearGrillaCarrito()
        End Sub

        Private Sub FormatearGrillaCarrito()
            If dgvCarrito.Columns.Count > 0 Then
                dgvCarrito.Columns("ID").Visible = False
                dgvCarrito.Columns("Plato").Width = 160
                dgvCarrito.Columns("Cant").Width = 55
                dgvCarrito.Columns("Precio").Width = 75
                dgvCarrito.Columns("Subtotal").Width = 85

                dgvCarrito.Columns("Precio").DefaultCellStyle.Format = "$ #,##0.00"
                dgvCarrito.Columns("Subtotal").DefaultCellStyle.Format = "$ #,##0.00"
            End If
        End Sub

        Private Sub CargarCategorias()
            cboCategoria.Items.Clear()
            cboCategoria.Items.Add("Todas las Categorías")
            cboCategoria.Items.Add("Desayunos")
            cboCategoria.Items.Add("Almuerzos")
            cboCategoria.Items.Add("Cenas")
            cboCategoria.SelectedIndex = 0
        End Sub

        Private Sub CargarTiposServicio()
            cboTipoServicio.Items.Clear()
            cboTipoServicio.Items.Add("Comer en Mesa")
            cboTipoServicio.Items.Add("Para Llevar")
            cboTipoServicio.SelectedIndex = 0
        End Sub

        Private Sub CargarMetodosPago()
            cboMetodoPago.Items.Clear()
            cboMetodoPago.Items.Add("Pago en Caja (Efectivo / Tarjeta)")
            cboMetodoPago.Items.Add("Pago por QR / Yappy")
            cboMetodoPago.Items.Add("Transferencia Bancaria")
            cboMetodoPago.SelectedIndex = 0
        End Sub

        Private Sub cboCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategoria.SelectedIndexChanged
            CargarTarjetasPlatos()
        End Sub

        Private Sub txtBuscarPlato_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarPlato.TextChanged
            CargarTarjetasPlatos()
        End Sub

        ''' <summary>
        ''' Renderiza dinámicamente las tarjetas gastronómicas optimizadas para pantallas táctiles.
        ''' </summary>
        Private Sub CargarTarjetasPlatos()
            flpCatalogoTarjetas.SuspendLayout()
            flpCatalogoTarjetas.Controls.Clear()

            Dim dtPlatos As DataTable = PlatoDAO.ObtenerTodos()
            If dtPlatos Is Nothing OrElse dtPlatos.Rows.Count = 0 Then
                flpCatalogoTarjetas.ResumeLayout(True)
                Return
            End If

            Dim categoriaFiltro As String = cboCategoria.SelectedItem.ToString()
            Dim busqueda As String = txtBuscarPlato.Text.Trim().ToLowerInvariant()

            ' Calcular el ancho óptimo para 3 o 4 columnas táctiles según la resolución
            Dim intAnchoDisponible As Integer = flpCatalogoTarjetas.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 24
            If intAnchoDisponible <= 0 Then intAnchoDisponible = 680

            Dim numColumnas As Integer = If(intAnchoDisponible < 650, 2, If(intAnchoDisponible < 950, 3, 4))
            Dim intAnchoTarjeta As Integer = (intAnchoDisponible \ numColumnas) - 12
            If intAnchoTarjeta < 200 Then intAnchoTarjeta = 200

            For Each row As DataRow In dtPlatos.Rows
                Dim idPlato As Integer = Convert.ToInt32(row("ID"))
                Dim nombrePlato As String = row("Nombre").ToString()
                Dim categoria As String = row("Categoria").ToString()
                Dim precio As Decimal = Convert.ToDecimal(row("Precio"))
                Dim estado As String = row("Estado").ToString()

                ' Aplicar filtro de categoría y búsqueda por texto
                If Not categoriaFiltro.Equals("Todas las Categorías") AndAlso Not categoria.Equals(categoriaFiltro, StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                If Not String.IsNullOrEmpty(busqueda) AndAlso Not nombrePlato.ToLowerInvariant().Contains(busqueda) Then
                    Continue For
                End If

                If Not estado.Equals("Disponible", StringComparison.OrdinalIgnoreCase) Then
                    Continue For
                End If

                ' Crear Tarjeta de Plato Táctil (Dimensiones amplias para toque fácil)
                Dim cardPlato As New Panel With {
                    .Width = intAnchoTarjeta,
                    .Height = 330,
                    .BackColor = Color.White,
                    .Margin = New Padding(6)
                }
                ThemeConfig.AplicarEstiloTarjeta(cardPlato)

                Dim intAnchoInner As Integer = intAnchoTarjeta - 16

                ' Imagen representativa del plato
                Dim picFoto As New PictureBox With {
                    .Width = intAnchoInner,
                    .Height = 115,
                    .Location = New Point(8, 8),
                    .SizeMode = PictureBoxSizeMode.CenterImage,
                    .Image = CcnImagenPlatoHelper.GenerarImagenPlato(nombrePlato, intAnchoInner, 115)
                }

                ' Título del Plato con tipografía amplia táctil
                Dim lblNombre As New Label With {
                    .Text = nombrePlato,
                    .Font = ThemeConfig.ObtenerFuenteSubtitulo(10.5F, FontStyle.Bold),
                    .ForeColor = ThemeConfig.ColorNeutralDark,
                    .Location = New Point(8, 128),
                    .Size = New Size(intAnchoInner, 34),
                    .UseMnemonic = False
                }

                ' Etiqueta de Categoría
                Dim lblCat As New Label With {
                    .Text = categoria,
                    .Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Bold),
                    .ForeColor = ThemeConfig.ColorPrimary,
                    .Location = New Point(8, 166),
                    .Size = New Size(intAnchoInner \ 2, 20),
                    .UseMnemonic = False
                }

                ' Precio amplio destacado
                Dim lblPrecio As New Label With {
                    .Text = $"$ {precio:N2}",
                    .Font = ThemeConfig.ObtenerFuenteTitulo(11.5F, FontStyle.Bold),
                    .ForeColor = ThemeConfig.ColorSecondary,
                    .Location = New Point(8 + (intAnchoInner \ 2), 164),
                    .Size = New Size(intAnchoInner \ 2, 22),
                    .TextAlign = ContentAlignment.TopRight,
                    .UseMnemonic = False
                }

                ' Selector de Cantidad Táctil (NumericUpDown amplio) - Centrado Horizontall
                Dim intXStepper As Integer = (intAnchoTarjeta - 130) \ 2
                Dim numCant As New NumericUpDown With {
                    .Location = New Point(intXStepper, 194),
                    .Width = 130,
                    .Height = 40,
                    .Minimum = 1,
                    .Maximum = 50,
                    .Value = 1,
                    .Font = ThemeConfig.ObtenerFuenteSubtitulo(12.5F, FontStyle.Bold)
                }

                ' Botón Agregar al Carrito (Ancho completo centrado, Fila Inferior)
                Dim btnAgregar As New Button With {
                    .Text = "Agregar al Pedido",
                    .Location = New Point(8, 242),
                    .Size = New Size(intAnchoInner, 44),
                    .UseMnemonic = False
                }
                ThemeConfig.EstilizarBotonPrimario(btnAgregar)

                ' Evento clic para agregar al carrito
                AddHandler btnAgregar.Click, Sub(s, ev)
                                                 AgregarAlCarrito(idPlato, nombrePlato, Convert.ToInt32(numCant.Value), precio)
                                             End Sub

                cardPlato.Controls.Add(picFoto)
                cardPlato.Controls.Add(lblNombre)
                cardPlato.Controls.Add(lblCat)
                cardPlato.Controls.Add(lblPrecio)
                cardPlato.Controls.Add(numCant)
                cardPlato.Controls.Add(btnAgregar)

                ' Transformar selector numérico en Stepper Táctil gigante [-] [1] [+]
                ThemeConfig.ReemplazarNumericUpDownConTouchStepper(numCant)

                flpCatalogoTarjetas.Controls.Add(cardPlato)
            Next

            flpCatalogoTarjetas.ResumeLayout(True)
        End Sub

        Private Sub flpCatalogoTarjetas_Resize(sender As Object, e As EventArgs) Handles flpCatalogoTarjetas.Resize
            AjustarTamanoTarjetas()
        End Sub

        Private Sub AjustarTamanoTarjetas()
            If flpCatalogoTarjetas Is Nothing OrElse flpCatalogoTarjetas.Controls.Count = 0 Then Return
            flpCatalogoTarjetas.SuspendLayout()

            Dim widthDisponible As Integer = flpCatalogoTarjetas.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 24
            If widthDisponible <= 0 Then
                flpCatalogoTarjetas.ResumeLayout(True)
                Return
            End If

            Dim numColumnas As Integer = If(widthDisponible < 650, 2, If(widthDisponible < 950, 3, 4))
            Dim intAnchoTarjeta As Integer = (widthDisponible \ numColumnas) - 12
            If intAnchoTarjeta < 200 Then intAnchoTarjeta = 200

            For Each ctrl As Control In flpCatalogoTarjetas.Controls
                If TypeOf ctrl Is Panel Then
                    Dim card As Panel = CType(ctrl, Panel)
                    card.Width = intAnchoTarjeta
                    Dim intAnchoInner As Integer = intAnchoTarjeta - 16

                    For Each inner As Control In card.Controls
                        If TypeOf inner Is PictureBox Then
                            inner.Left = 8
                            inner.Width = intAnchoInner
                        ElseIf TypeOf inner Is Label AndAlso inner.Location.Y < 150 Then
                            inner.Left = 8
                            inner.Width = intAnchoInner
                        ElseIf TypeOf inner Is Label AndAlso CType(inner, Label).TextAlign = ContentAlignment.TopRight Then
                            inner.Left = 8 + (intAnchoInner \ 2)
                            inner.Width = intAnchoInner \ 2
                        ElseIf TypeOf inner Is Button Then
                            inner.Left = 8
                            inner.Width = intAnchoInner
                        ElseIf TypeOf inner Is Panel AndAlso Equals(inner.Tag, "TouchStepper") Then
                            inner.Left = (intAnchoTarjeta - inner.Width) \ 2
                        End If
                    Next
                End If
            Next

            flpCatalogoTarjetas.ResumeLayout(True)
        End Sub

        Private Sub AgregarAlCarrito(idPlato As Integer, nombrePlato As String, cantidad As Integer, precioUnitario As Decimal)
            Dim filasExistentes() As DataRow = _tablaCarrito.Select($"ID = {idPlato}")
            If filasExistentes.Length > 0 Then
                Dim dr As DataRow = filasExistentes(0)
                Dim cantActual As Integer = Convert.ToInt32(dr("Cant"))
                Dim nuevaCant As Integer = cantActual + cantidad
                dr("Cant") = nuevaCant
                dr("Subtotal") = nuevaCant * precioUnitario
            Else
                Dim dr As DataRow = _tablaCarrito.NewRow()
                dr("ID") = idPlato
                dr("Plato") = nombrePlato
                dr("Cant") = cantidad
                dr("Precio") = precioUnitario
                dr("Subtotal") = cantidad * precioUnitario
                _tablaCarrito.Rows.Add(dr)
            End If

            CalcularTotalGeneral()
        End Sub

        Private Sub btnEliminarItemCarrito_Click(sender As Object, e As EventArgs) Handles btnEliminarItemCarrito.Click
            If dgvCarrito.SelectedRows.Count > 0 Then
                For Each r As DataGridViewRow In dgvCarrito.SelectedRows
                    dgvCarrito.Rows.Remove(r)
                Next
                CalcularTotalGeneral()
            Else
                MostrarMensajeAdvertencia("Seleccione un elemento del carrito para eliminar.", "Selección Requerida")
            End If
        End Sub

        Private Sub btnVaciarCarrito_Click(sender As Object, e As EventArgs) Handles btnVaciarCarrito.Click
            If _tablaCarrito.Rows.Count > 0 Then
                _tablaCarrito.Rows.Clear()
                CalcularTotalGeneral()
            End If
        End Sub

        ' Habilitación de NumericUpDown según CheckBox de extras
        Private Sub chkExtraSoda_CheckedChanged(sender As Object, e As EventArgs) Handles chkExtraSoda.CheckedChanged
            numExtraSoda.Enabled = chkExtraSoda.Checked
            CalcularTotalGeneral()
        End Sub

        Private Sub chkExtraJugo_CheckedChanged(sender As Object, e As EventArgs) Handles chkExtraJugo.CheckedChanged
            numExtraJugo.Enabled = chkExtraJugo.Checked
            CalcularTotalGeneral()
        End Sub

        Private Sub chkExtraPapas_CheckedChanged(sender As Object, e As EventArgs) Handles chkExtraPapas.CheckedChanged
            numExtraPapas.Enabled = chkExtraPapas.Checked
            CalcularTotalGeneral()
        End Sub

        Private Sub chkExtraEnsalada_CheckedChanged(sender As Object, e As EventArgs) Handles chkExtraEnsalada.CheckedChanged
            numExtraEnsalada.Enabled = chkExtraEnsalada.Checked
            CalcularTotalGeneral()
        End Sub

        Private Sub numExtra_ValueChanged(sender As Object, e As EventArgs) Handles numExtraSoda.ValueChanged, numExtraJugo.ValueChanged, numExtraPapas.ValueChanged, numExtraEnsalada.ValueChanged
            CalcularTotalGeneral()
        End Sub

        Private Sub CalcularTotalGeneral()
            If _tablaCarrito Is Nothing OrElse _tablaCarrito.Rows Is Nothing Then Return

            Dim subtotalCarrito As Decimal = 0D
            For Each row As DataRow In _tablaCarrito.Rows
                subtotalCarrito += Convert.ToDecimal(row("Subtotal"))
            Next

            Dim extras As Decimal = 0D
            If chkExtraSoda.Checked Then extras += (1.50D * Convert.ToDecimal(numExtraSoda.Value))
            If chkExtraJugo.Checked Then extras += (2.00D * Convert.ToDecimal(numExtraJugo.Value))
            If chkExtraPapas.Checked Then extras += (1.75D * Convert.ToDecimal(numExtraPapas.Value))
            If chkExtraEnsalada.Checked Then extras += (1.75D * Convert.ToDecimal(numExtraEnsalada.Value))

            Dim totalFinal As Decimal = subtotalCarrito + extras
            lblMontoTotal.Text = $"$ {totalFinal:N2}"
        End Sub

        Private Function ObtenerExtrasSeleccionados() As String
            Dim lista As New List(Of String)()
            If chkExtraSoda.Checked Then lista.Add($"{numExtraSoda.Value}x Soda Nacional ($1.50)")
            If chkExtraJugo.Checked Then lista.Add($"{numExtraJugo.Value}x Chicha de Nance ($2.00)")
            If chkExtraPapas.Checked Then lista.Add($"{numExtraPapas.Value}x Chicha de Limón c/ Raspadura ($1.75)")
            If chkExtraEnsalada.Checked Then lista.Add($"{numExtraEnsalada.Value}x Chicha de Naranja ($1.75)")

            If lista.Count = 0 Then Return "Sin Extras"
            Return String.Join(", ", lista)
        End Function

        Private Function ObtenerExtrasEstructurados() As List(Of Tuple(Of String, Integer, Decimal))
            Dim lista As New List(Of Tuple(Of String, Integer, Decimal))()
            If chkExtraSoda.Checked Then lista.Add(Tuple.Create("Soda Nacional (Bebida)", Convert.ToInt32(numExtraSoda.Value), 1.50D))
            If chkExtraJugo.Checked Then lista.Add(Tuple.Create("Chicha de Nance (Bebida)", Convert.ToInt32(numExtraJugo.Value), 2.00D))
            If chkExtraPapas.Checked Then lista.Add(Tuple.Create("Chicha de Limón c/ Raspadura (Bebida)", Convert.ToInt32(numExtraPapas.Value), 1.75D))
            If chkExtraEnsalada.Checked Then lista.Add(Tuple.Create("Chicha de Naranja (Bebida)", Convert.ToInt32(numExtraEnsalada.Value), 1.75D))
            Return lista
        End Function

        Private Sub btnConfirmarPedido_Click(sender As Object, e As EventArgs) Handles btnConfirmarPedido.Click
            If _tablaCarrito.Rows.Count = 0 Then
                MostrarMensajeAdvertencia("El carrito de compras está vacío. Agregue al menos un plato.", "Carrito Vacío")
                Return
            End If

            Dim nombreCliente As String = txtNombreCliente.Text.Trim()
            If String.IsNullOrWhiteSpace(nombreCliente) Then
                MostrarMensajeAdvertencia("Por favor ingrese su nombre para registrar el pedido.", "Nombre Requerido")
                txtNombreCliente.Focus()
                Return
            End If

            Dim correoCliente As String = txtCorreoCliente.Text.Trim()
            If String.IsNullOrWhiteSpace(correoCliente) OrElse Not correoCliente.Contains("@") OrElse Not correoCliente.Contains(".") Then
                MostrarMensajeAdvertencia("Por favor ingrese un correo electrónico válido para la facturación digital.", "Correo Inválido")
                txtCorreoCliente.Focus()
                Return
            End If

            Dim mesaODireccion As String = txtMesa.Text.Trim()
            If String.IsNullOrWhiteSpace(mesaODireccion) Then
                MostrarMensajeAdvertencia("Por favor ingrese el número de mesa o identificador de entrega.", "Mesa Requerida")
                txtMesa.Focus()
                Return
            End If

            ' Calcular total final con precisión decimal
            Dim subtotalCarrito As Decimal = 0D
            Dim listaPlatosResumen As New List(Of String)()
            For Each row As DataRow In _tablaCarrito.Rows
                subtotalCarrito += Convert.ToDecimal(row("Subtotal"))
                listaPlatosResumen.Add($"{row("Cant")}x {row("Plato")}")
            Next

            Dim extrasMonto As Decimal = 0D
            If chkExtraSoda.Checked Then extrasMonto += (1.50D * Convert.ToDecimal(numExtraSoda.Value))
            If chkExtraJugo.Checked Then extrasMonto += (2.00D * Convert.ToDecimal(numExtraJugo.Value))
            If chkExtraPapas.Checked Then extrasMonto += (1.75D * Convert.ToDecimal(numExtraPapas.Value))
            If chkExtraEnsalada.Checked Then extrasMonto += (1.75D * Convert.ToDecimal(numExtraEnsalada.Value))

            Dim totalFinal As Decimal = subtotalCarrito + extrasMonto
            Dim descripcionPlatos As String = String.Join(" + ", listaPlatosResumen)
            Dim servicio As String = If(cboTipoServicio.SelectedItem IsNot Nothing, cboTipoServicio.SelectedItem.ToString(), "Comer en Mesa")
            Dim metodoPago As String = If(cboMetodoPago.SelectedItem IsNot Nothing, cboMetodoPago.SelectedItem.ToString(), "Pago en Caja (Efectivo / Tarjeta)")
            Dim extrasTexto As String = ObtenerExtrasSeleccionados()
            Dim extrasEstructurados As List(Of Tuple(Of String, Integer, Decimal)) = ObtenerExtrasEstructurados()

            ' 1. Mostrar pantalla de Resumen y Rectificación antes de confirmar definitivamente
            Using dlgResumen As New FrmClienteResumenPedidoDialog(nombreCliente, correoCliente, $"{servicio} ({mesaODireccion})", metodoPago, _tablaCarrito, extrasEstructurados, totalFinal)
                If dlgResumen.ShowDialog(Me) <> DialogResult.OK Then
                    ' El usuario decidió modificar su pedido o cancelar la confirmación
                    Return
                End If
            End Using

            ' 2. Procesar según el Método de Pago Seleccionado
            Dim esDigital As Boolean = metodoPago.IndexOf("QR", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                      metodoPago.IndexOf("Yappy", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                      metodoPago.IndexOf("Transferencia", StringComparison.OrdinalIgnoreCase) >= 0

            If esDigital Then
                ' A. PASARELA DIGITAL DE PAGO (QR / Yappy / Transferencia)
                Using dlgQR As New FrmClientePasarelaQRDialog(nombreCliente, totalFinal, metodoPago)
                    If dlgQR.ShowDialog(Me) <> DialogResult.OK Then
                        MostrarMensajeAdvertencia("La transacción de pago digital no fue completada. Su pedido continúa en el carrito.", "Pago Cancelado o Expirado")
                        Return
                    End If
                End Using

                ' Pago confirmado por la pasarela: Registrar como PAGADO directamente
                Dim idPedidoGenerado As Integer = PedidoDAO.GuardarPedidoCompleto(
                    nombreCliente,
                    mesaODireccion,
                    servicio,
                    totalFinal,
                    _tablaCarrito,
                    extrasEstructurados,
                    "PAGADO",
                    metodoPago,
                    "RECIBIDO",
                    correoCliente
                )

                If idPedidoGenerado > 0 Then
                    ' Emisión automática de factura electrónica digital PDF
                    Dim numFactura As String = PedidoDAO.RegistrarFactura(idPedidoGenerado, "8-800-1234", nombreCliente, "Ciudad de Panamá", "+507 6200-1122", correoCliente)
                    Try
                        Dim carpetaFacturas As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasEmitidas")
                        If Not IO.Directory.Exists(carpetaFacturas) Then IO.Directory.CreateDirectory(carpetaFacturas)
                        Dim rutaArchivoPDF As String = IO.Path.Combine(carpetaFacturas, $"Factura_{numFactura}.pdf")
                        Dim drPedido As DataRow = PedidoDAO.ObtenerPedidoPorId(idPedidoGenerado)
                        If drPedido IsNot Nothing Then
                            Services.FacturaPDFService.GenerarFacturaPDF(rutaArchivoPDF, drPedido)
                        End If
                    Catch
                    End Try

                    MostrarMensajeExito(
                        $"¡Pago Aprobado y Transacción Exitosa por Pasarela Digital!{vbCrLf}{vbCrLf}" &
                        $"N° Pedido: #{idPedidoGenerado}{vbCrLf}" &
                        $"N° Factura PDF: {numFactura}{vbCrLf}" &
                        $"Cliente: {nombreCliente}{vbCrLf}" &
                        $"Correo: {correoCliente}{vbCrLf}" &
                        $"Total Cobrado: ${totalFinal:N2}{vbCrLf}{vbCrLf}" &
                        $"⚡ Su comanda ha sido enviada automáticamente al Monitor de Cocina KDS para su preparación inmediata sin intermediarios y su factura PDF ha sido generada.",
                        "Pago & Comanda Confirmados"
                    )

                    LimpiarCarritoYExtras()
                Else
                    MostrarMensajeAdvertencia("Ocurrió un error al registrar el pedido pagado en la base de datos.", "Error de Registro")
                End If
            Else
                ' B. PAGO EN CAJA (EFECTIVO / TARJETA PRESENCIAL)
                ' Pausa de Seguridad: Se guarda como PENDIENTE. No va a cocina hasta que el cajero confirme cobro
                Dim idPedidoGenerado As Integer = PedidoDAO.GuardarPedidoCompleto(
                    nombreCliente,
                    mesaODireccion,
                    servicio,
                    totalFinal,
                    _tablaCarrito,
                    extrasEstructurados,
                    "PENDIENTE",
                    metodoPago,
                    "RECIBIDO",
                    correoCliente
                )

                If idPedidoGenerado > 0 Then
                    MostrarMensajeExito(
                        $"¡Pedido #{idPedidoGenerado} registrado con Pausa de Seguridad!{vbCrLf}{vbCrLf}" &
                        $"Cliente: {nombreCliente}{vbCrLf}" &
                        $"Estado: Esperando Pago en Caja{vbCrLf}" &
                        $"Servicio: {servicio} ({mesaODireccion}){vbCrLf}" &
                        $"Total a Pagar en Caja: ${totalFinal:N2}{vbCrLf}{vbCrLf}" &
                        $"Por favor acérquese a la caja para realizar su pago. En cuanto el cajero presione 'Confirmar Cobro', la comanda pasará automáticamente a la cocina y se emitirá la factura.",
                        "Orden Registrada en Espera de Pago"
                    )

                    LimpiarCarritoYExtras()
                Else
                    MostrarMensajeAdvertencia("Ocurrió un error al registrar el pedido pendiente en la base de datos.", "Error de Registro")
                End If
            End If
        End Sub

        Private Sub LimpiarCarritoYExtras()
            _tablaCarrito.Rows.Clear()
            chkExtraSoda.Checked = False
            chkExtraJugo.Checked = False
            chkExtraPapas.Checked = False
            chkExtraEnsalada.Checked = False
            numExtraSoda.Value = 1
            numExtraJugo.Value = 1
            numExtraPapas.Value = 1
            numExtraEnsalada.Value = 1
            CalcularTotalGeneral()
        End Sub

    End Class
End Namespace
