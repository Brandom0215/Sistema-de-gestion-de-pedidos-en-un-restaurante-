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
            CargarTarjetasPlatos()

            If Not String.IsNullOrWhiteSpace(_nombreUsuarioSesion) AndAlso Not _nombreUsuarioSesion.Equals("Invitado", StringComparison.OrdinalIgnoreCase) Then
                txtNombreCliente.Text = _nombreUsuarioSesion
            Else
                txtNombreCliente.Text = "Cliente Invitado"
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

            ' Estilizado de la grilla del carrito
            dgvCarrito.BackgroundColor = Color.White
            dgvCarrito.DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 236, 231)
            dgvCarrito.DefaultCellStyle.SelectionForeColor = ThemeConfig.ColorNeutralDark
            dgvCarrito.ColumnHeadersDefaultCellStyle.BackColor = ThemeConfig.ColorSecondary
            dgvCarrito.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvCarrito.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(8.5F, FontStyle.Bold)
            dgvCarrito.EnableHeadersVisualStyles = False
            dgvCarrito.RowTemplate.Height = 24
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
                dgvCarrito.Columns("Plato").Width = 170
                dgvCarrito.Columns("Cant").Width = 45
                dgvCarrito.Columns("Precio").Width = 70
                dgvCarrito.Columns("Subtotal").Width = 80

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
            cboTipoServicio.Items.Add("En Mesa")
            cboTipoServicio.Items.Add("Para Llevar")
            cboTipoServicio.Items.Add("Delivery")
            cboTipoServicio.SelectedIndex = 0
        End Sub

        Private Sub cboCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategoria.SelectedIndexChanged
            CargarTarjetasPlatos()
        End Sub

        Private Sub txtBuscarPlato_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarPlato.TextChanged
            CargarTarjetasPlatos()
        End Sub

        ''' <summary>
        ''' Renderiza dinámicamente las tarjetas gastronómicas con PictureBox en el FlowLayoutPanel.
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

            ' Calcular el ancho óptimo para 4 columnas dinámicas
            Dim intAnchoDisponible As Integer = flpCatalogoTarjetas.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 24
            If intAnchoDisponible <= 0 Then intAnchoDisponible = 680
            Dim intAnchoTarjeta As Integer = (intAnchoDisponible \ 4) - 10
            If intAnchoTarjeta < 165 Then intAnchoTarjeta = 165

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

                ' Crear Tarjeta de Plato (Ancho dinámico responsivo para 4 columnas)
                Dim cardPlato As New Panel With {
                    .Width = intAnchoTarjeta,
                    .Height = 230,
                    .BackColor = Color.White,
                    .Margin = New Padding(4)
                }
                ThemeConfig.AplicarEstiloTarjeta(cardPlato)

                Dim intAnchoInner As Integer = intAnchoTarjeta - 14

                ' Imagen vectorial o real mediante PictureBox
                Dim picFoto As New PictureBox With {
                    .Width = intAnchoInner,
                    .Height = 88,
                    .Location = New Point(7, 7),
                    .SizeMode = PictureBoxSizeMode.CenterImage,
                    .Image = CcnImagenPlatoHelper.GenerarImagenPlato(nombrePlato, intAnchoInner, 88)
                }

                ' Título del Plato
                Dim lblNombre As New Label With {
                    .Text = nombrePlato,
                    .Font = ThemeConfig.ObtenerFuenteSubtitulo(8.5F, FontStyle.Bold),
                    .ForeColor = ThemeConfig.ColorNeutralDark,
                    .Location = New Point(7, 98),
                    .Size = New Size(intAnchoInner, 32),
                    .UseMnemonic = False
                }

                ' Etiqueta de Categoría
                Dim lblCat As New Label With {
                    .Text = categoria,
                    .Font = ThemeConfig.ObtenerFuenteCuerpo(7.5F, FontStyle.Bold),
                    .ForeColor = ThemeConfig.ColorPrimary,
                    .Location = New Point(7, 132),
                    .Size = New Size(intAnchoInner \ 2, 16),
                    .UseMnemonic = False
                }

                ' Precio
                Dim lblPrecio As New Label With {
                    .Text = $"$ {precio:N2}",
                    .Font = ThemeConfig.ObtenerFuenteTitulo(9.5F, FontStyle.Bold),
                    .ForeColor = ThemeConfig.ColorSecondary,
                    .Location = New Point(7 + (intAnchoInner \ 2), 130),
                    .Size = New Size(intAnchoInner \ 2, 20),
                    .TextAlign = ContentAlignment.TopRight,
                    .UseMnemonic = False
                }

                ' Selector de Cantidad
                Dim numCant As New NumericUpDown With {
                    .Location = New Point(7, 154),
                    .Width = 45,
                    .Minimum = 1,
                    .Maximum = 50,
                    .Value = 1,
                    .Font = ThemeConfig.ObtenerFuenteCuerpo(8.5F, FontStyle.Regular)
                }

                ' Botón Agregar al Carrito
                Dim btnAgregar As New Button With {
                    .Text = "🛒 Agregar",
                    .Location = New Point(56, 152),
                    .Size = New Size(intAnchoInner - 49, 28),
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

            Dim intAnchoTarjeta As Integer = (widthDisponible \ 4) - 10
            If intAnchoTarjeta < 165 Then intAnchoTarjeta = 165

            For Each ctrl As Control In flpCatalogoTarjetas.Controls
                If TypeOf ctrl Is Panel Then
                    Dim card As Panel = CType(ctrl, Panel)
                    card.Width = intAnchoTarjeta
                    Dim intAnchoInner As Integer = intAnchoTarjeta - 14

                    For Each inner As Control In card.Controls
                        If TypeOf inner Is PictureBox Then
                            inner.Width = intAnchoInner
                        ElseIf TypeOf inner Is Label AndAlso inner.Location.Y < 120 Then
                            inner.Width = intAnchoInner
                        ElseIf TypeOf inner Is Label AndAlso CType(inner, Label).TextAlign = ContentAlignment.TopRight Then
                            inner.Left = 7 + (intAnchoInner \ 2)
                            inner.Width = intAnchoInner \ 2
                        ElseIf TypeOf inner Is Button Then
                            inner.Width = intAnchoInner - 49
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

            Dim mesaODireccion As String = txtMesa.Text.Trim()
            If String.IsNullOrWhiteSpace(mesaODireccion) Then
                MostrarMensajeAdvertencia("Por favor ingrese el número de mesa o dirección de entrega.", "Mesa / Dirección Requerida")
                txtMesa.Focus()
                Return
            End If

            ' Resumen del pedido
            Dim listaPlatosResumen As New List(Of String)()
            For Each row As DataRow In _tablaCarrito.Rows
                listaPlatosResumen.Add($"{row("Cant")}x {row("Plato")}")
            Next
            Dim descripcionPlatos As String = String.Join(" + ", listaPlatosResumen)
            Dim servicio As String = cboTipoServicio.SelectedItem.ToString()
            Dim extras As String = ObtenerExtrasSeleccionados()

            ' Registrar pedido completo en el DAO compartido
            Dim idPedidoGenerado As Integer = PedidoDAO.Guardar(nombreCliente, mesaODireccion, descripcionPlatos, extras, servicio)

            MostrarMensajeExito($"¡Pedido #{idPedidoGenerado} registrado con éxito!{vbCrLf}{vbCrLf}Cliente: {nombreCliente}{vbCrLf}Servicio: {servicio} ({mesaODireccion}){vbCrLf}Ítems en Carrito: {descripcionPlatos}{vbCrLf}Extras: {extras}{vbCrLf}Total: {lblMontoTotal.Text}{vbCrLf}{vbCrLf}Su comanda ha sido enviada al Monitor de Cocina KDS y Caja.", "Pedido Confirmado")

            ' Limpiar carrito e insumos
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
