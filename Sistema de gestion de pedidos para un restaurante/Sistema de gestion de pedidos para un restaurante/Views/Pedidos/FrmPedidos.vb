Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Pedidos
    ''' <summary>
    ''' Formulario de Gestión de Pedidos de Restaurante.
    ''' Implementa los controles exigidos: TextBox para Cliente y Mesa, ComboBox para Plato Principal,
    ''' CheckBoxes para Acompañamientos, OptionButtons para Tipo de Servicio y operaciones CRUD.
    ''' </summary>
    Public Class FrmPedidos

        ''' <summary> Tabla de datos en memoria para simular/gestionar los pedidos de RestauranteDB </summary>
        Private _tablaPedidos As DataTable

        ''' <summary> ID autoincremental para nuevos registros </summary>
        Private _contadorId As Integer = 1

        ''' <summary> ID del pedido seleccionado para edición o eliminación </summary>
        Private _idPedidoSeleccionado As Integer = 0

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmPedidos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            InicializarCatalogoPlatos()
            InicializarEstructuraTabla()
            CargarDatosDemostrativos()
            RefrescarGrilla()
        End Sub

        ' =========================================================================
        ' INICIALIZACIÓN Y CONFIGURACIÓN VISUAL
        ' =========================================================================

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlHeader.BackColor = ThemeConfig.ColorBackgroundApp

            lblTituloHeader.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloHeader.ForeColor = ThemeConfig.ColorTextMuted

            grpFormulario.ForeColor = ThemeConfig.ColorSecondary
            grpListado.ForeColor = ThemeConfig.ColorSecondary
            grpAcompanamientos.ForeColor = ThemeConfig.ColorNeutralDark
            grpTipoServicio.ForeColor = ThemeConfig.ColorNeutralDark

            ThemeConfig.EstilizarBotonPrimario(btnGuardar)
            ThemeConfig.EstilizarBotonSecundario(btnActualizar)
            ThemeConfig.EstilizarBotonEliminar(btnEliminar)
            ThemeConfig.EstilizarBotonSecundario(btnBuscar)
            ThemeConfig.EstilizarBotonSecundario(btnMostrarTodo)
            ThemeConfig.EstilizarBotonPrimario(btnVerConfirmado)

            ' Estilizado de la grilla
            dgvPedidos.BackgroundColor = Color.White
            dgvPedidos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 230, 220)
            dgvPedidos.DefaultCellStyle.SelectionForeColor = ThemeConfig.ColorNeutralDark
            dgvPedidos.ColumnHeadersDefaultCellStyle.BackColor = ThemeConfig.ColorSecondary
            dgvPedidos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvPedidos.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            dgvPedidos.EnableHeadersVisualStyles = False
            dgvPedidos.RowTemplate.Height = 28
        End Sub

        Private Sub InicializarCatalogoPlatos()
            cboPlatoPrincipal.Items.Clear()
            cboPlatoPrincipal.Items.Add("Lomo a la Brasa ($18.50)")
            cboPlatoPrincipal.Items.Add("Pollo Abrasado Artesanal ($14.00)")
            cboPlatoPrincipal.Items.Add("Ceviche Mixto Tradicional ($16.50)")
            cboPlatoPrincipal.Items.Add("Hamburguesa Gourmet Rústica ($12.00)")
            cboPlatoPrincipal.Items.Add("Fettuccine a la Huancaína ($15.00)")
            cboPlatoPrincipal.Items.Add("Ensalada César con Pollo ($11.50)")
            cboPlatoPrincipal.SelectedIndex = 0
        End Sub

        Private Sub InicializarEstructuraTabla()
            _tablaPedidos = New DataTable("Pedidos")
            _tablaPedidos.Columns.Add("ID", GetType(Integer))
            _tablaPedidos.Columns.Add("Cliente", GetType(String))
            _tablaPedidos.Columns.Add("Mesa", GetType(String))
            _tablaPedidos.Columns.Add("PlatoPrincipal", GetType(String))
            _tablaPedidos.Columns.Add("Acompanamientos", GetType(String))
            _tablaPedidos.Columns.Add("TipoServicio", GetType(String))
            _tablaPedidos.Columns.Add("FechaHora", GetType(String))
        End Sub

        Private Sub CargarDatosDemostrativos()
            AgregarFilaDemostrativa("Carlos Mendoza", "04", "Lomo a la Brasa ($18.50)", "Papas Fritas, Ensalada Fresca", "En Mesa")
            AgregarFilaDemostrativa("María Fernández", "02", "Ceviche Mixto Tradicional ($16.50)", "Salsas de la Casa", "En Mesa")
            AgregarFilaDemostrativa("Roberto Gómez", "Delivery", "Pollo Abrasado Artesanal ($14.00)", "Papas Fritas, Arroz con Choclo", "Delivery")
            AgregarFilaDemostrativa("Ana Lucia Torres", "Llevar", "Hamburguesa Gourmet Rústica ($12.00)", "Papas Fritas", "Para Llevar")
        End Sub

        Private Sub AgregarFilaDemostrativa(cliente As String, mesa As String, plato As String, acomp As String, servicio As String)
            Dim dr As DataRow = _tablaPedidos.NewRow()
            dr("ID") = _contadorId
            dr("Cliente") = cliente
            dr("Mesa") = mesa
            dr("PlatoPrincipal") = plato
            dr("Acompanamientos") = acomp
            dr("TipoServicio") = servicio
            dr("FechaHora") = DateTime.Now.AddMinutes(-_contadorId * 10).ToString("HH:mm:ss")
            _tablaPedidos.Rows.Add(dr)
            _contadorId += 1
        End Sub

        ' =========================================================================
        ' LÓGICA DE OPERACIONES CRUD (FUNCIONES ESPECÍFICAS)
        ' =========================================================================

        ''' <summary>
        ''' btnGuardar: Insertar un nuevo pedido con los datos del cliente, los platos seleccionados y el tipo de servicio.
        ''' </summary>
        Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
            If Not ValidarFormulario() Then Return

            Dim dr As DataRow = _tablaPedidos.NewRow()
            dr("ID") = _contadorId
            dr("Cliente") = txtNombreCliente.Text.Trim()
            dr("Mesa") = txtMesa.Text.Trim()
            dr("PlatoPrincipal") = cboPlatoPrincipal.SelectedItem.ToString()
            dr("Acompanamientos") = ObtenerAcompanamientosSeleccionados()
            dr("TipoServicio") = ObtenerTipoServicioSeleccionado()
            dr("FechaHora") = DateTime.Now.ToString("HH:mm:ss")
            _tablaPedidos.Rows.Add(dr)

            ' Sincronizar en el repositorio compartido para Caja y Facturación
            Data.PedidoDAO.Guardar(txtNombreCliente.Text.Trim(), txtMesa.Text.Trim(), cboPlatoPrincipal.SelectedItem.ToString(), ObtenerAcompanamientosSeleccionados(), ObtenerTipoServicioSeleccionado())

            MessageBox.Show($"¡Pedido #{_contadorId} guardado exitosamente en RestauranteDB!", "Pedido Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            _contadorId += 1
            LimpiarFormulario()
            RefrescarGrilla()
        End Sub

        ''' <summary>
        ''' btnActualizar: Modificar un pedido existente (cambiar acompañamientos, cliente o tipo de servicio).
        ''' </summary>
        Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
            If _idPedidoSeleccionado <= 0 Then
                MessageBox.Show("Por favor, seleccione un pedido de la lista para actualizar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If Not ValidarFormulario() Then Return

            Dim filas() As DataRow = _tablaPedidos.Select($"ID = {_idPedidoSeleccionado}")
            If filas.Length > 0 Then
                Dim dr As DataRow = filas(0)
                dr("Cliente") = txtNombreCliente.Text.Trim()
                dr("Mesa") = txtMesa.Text.Trim()
                dr("PlatoPrincipal") = cboPlatoPrincipal.SelectedItem.ToString()
                dr("Acompanamientos") = ObtenerAcompanamientosSeleccionados()
                dr("TipoServicio") = ObtenerTipoServicioSeleccionado()

                ' Sincronizar actualización en PedidoDAO
                Data.PedidoDAO.Actualizar(_idPedidoSeleccionado, txtNombreCliente.Text.Trim(), txtMesa.Text.Trim(), cboPlatoPrincipal.SelectedItem.ToString(), ObtenerAcompanamientosSeleccionados(), ObtenerTipoServicioSeleccionado())

                MessageBox.Show($"¡Pedido #{_idPedidoSeleccionado} actualizado correctamente!", "Pedido Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LimpiarFormulario()
                RefrescarGrilla()
            End If
        End Sub

        ''' <summary>
        ''' btnEliminar: Eliminar un pedido seleccionado de la base de datos.
        ''' </summary>
        Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
            If _idPedidoSeleccionado <= 0 Then
                MessageBox.Show("Por favor, seleccione un pedido de la lista para eliminar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim confirmacion = MessageBox.Show($"¿Está seguro de eliminar el Pedido #{_idPedidoSeleccionado}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirmacion = DialogResult.Yes Then
                Dim filas() As DataRow = _tablaPedidos.Select($"ID = {_idPedidoSeleccionado}")
                If filas.Length > 0 Then
                    _tablaPedidos.Rows.Remove(filas(0))
                    ' Sincronizar eliminación en PedidoDAO
                    Data.PedidoDAO.Eliminar(_idPedidoSeleccionado)

                    MessageBox.Show($"El pedido #{_idPedidoSeleccionado} ha sido eliminado.", "Pedido Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LimpiarFormulario()
                    RefrescarGrilla()
                End If
            End If
        End Sub

        ''' <summary>
        ''' btnBuscar: Buscar un pedido por número (ID) o nombre de cliente.
        ''' </summary>
        Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
            Dim busqueda As String = txtNombreCliente.Text.Trim()
            If String.IsNullOrEmpty(busqueda) Then
                MessageBox.Show("Ingrese el nombre del cliente o ID en el campo 'Nombre del Cliente' para buscar.", "Búsqueda vacía", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim vista As New DataView(_tablaPedidos)
            Dim idBusqueda As Integer
            If Integer.TryParse(busqueda, idBusqueda) Then
                vista.RowFilter = $"ID = {idBusqueda}"
            Else
                vista.RowFilter = $"Cliente LIKE '%{busqueda.Replace("'", "''")}%'"
            End If

            dgvPedidos.DataSource = vista
            lblTotalRegistros.Text = $"Resultados encontrados: {vista.Count}"
        End Sub

        ''' <summary>
        ''' btnMostrarTodo: Mostrar todos los pedidos registrados limpiando filtros de búsqueda.
        ''' </summary>
        Private Sub btnMostrarTodo_Click(sender As Object, e As EventArgs) Handles btnMostrarTodo.Click
            LimpiarFormulario()
            RefrescarGrilla()
        End Sub

        ''' <summary>
        ''' Abertura de la Segunda Interfaz de Confirmación (con la imagen del plato principal PictureBox).
        ''' </summary>
        Private Sub btnVerConfirmado_Click(sender As Object, e As EventArgs) Handles btnVerConfirmado.Click
            Dim idPedido As Integer = _idPedidoSeleccionado
            Dim cliente As String = txtNombreCliente.Text.Trim()
            Dim mesa As String = txtMesa.Text.Trim()
            Dim plato As String = If(cboPlatoPrincipal.SelectedItem IsNot Nothing, cboPlatoPrincipal.SelectedItem.ToString(), "Lomo a la Brasa")
            Dim acomp As String = ObtenerAcompanamientosSeleccionados()
            Dim servicio As String = ObtenerTipoServicioSeleccionado()

            If idPedido <= 0 AndAlso dgvPedidos.SelectedRows.Count > 0 Then
                Dim row As DataGridViewRow = dgvPedidos.SelectedRows(0)
                idPedido = Convert.ToInt32(row.Cells("ID").Value)
                cliente = row.Cells("Cliente").Value.ToString()
                mesa = row.Cells("Mesa").Value.ToString()
                plato = row.Cells("PlatoPrincipal").Value.ToString()
                acomp = row.Cells("Acompanamientos").Value.ToString()
                servicio = row.Cells("TipoServicio").Value.ToString()
            End If

            If String.IsNullOrEmpty(cliente) Then cliente = "Cliente General"
            If String.IsNullOrEmpty(mesa) Then mesa = "01"

            ' Abrir Segunda Interfaz exigida por el profesor
            Using frmConfirmado As New FrmPedidoConfirmado(idPedido, cliente, mesa, plato, acomp, servicio)
                frmConfirmado.ShowDialog(Me)
            End Using
        End Sub

        ' =========================================================================
        ' EVENTOS DE INTERFAZ Y MÉTODOS AUXILIARES
        ' =========================================================================

        Private Sub dgvPedidos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPedidos.SelectionChanged
            If dgvPedidos.SelectedRows.Count > 0 Then
                Dim row As DataGridViewRow = dgvPedidos.SelectedRows(0)
                _idPedidoSeleccionado = Convert.ToInt32(row.Cells("ID").Value)

                txtNombreCliente.Text = row.Cells("Cliente").Value.ToString()
                txtMesa.Text = row.Cells("Mesa").Value.ToString()

                Dim platoStr As String = row.Cells("PlatoPrincipal").Value.ToString()
                If cboPlatoPrincipal.Items.Contains(platoStr) Then
                    cboPlatoPrincipal.SelectedItem = platoStr
                End If

                Dim acompStr As String = row.Cells("Acompanamientos").Value.ToString()
                chkPapasFritas.Checked = acompStr.Contains("Papas Fritas")
                chkEnsalada.Checked = acompStr.Contains("Ensalada")
                chkArroz.Checked = acompStr.Contains("Arroz")
                chkSalsas.Checked = acompStr.Contains("Salsas")

                Dim servicioStr As String = row.Cells("TipoServicio").Value.ToString()
                rbEnMesa.Checked = (servicioStr = "En Mesa")
                rbParaLlevar.Checked = (servicioStr = "Para Llevar")
                rbDelivery.Checked = (servicioStr = "Delivery")
            End If
        End Sub

        Private Function ValidarFormulario() As Boolean
            If String.IsNullOrWhiteSpace(txtNombreCliente.Text) Then
                MessageBox.Show("El nombre del cliente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtNombreCliente.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(txtMesa.Text) Then
                MessageBox.Show("El número de mesa o indicador de servicio es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtMesa.Focus()
                Return False
            End If

            If cboPlatoPrincipal.SelectedItem Is Nothing Then
                MessageBox.Show("Seleccione un plato principal del catálogo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cboPlatoPrincipal.Focus()
                Return False
            End If

            Return True
        End Function

        Private Function ObtenerAcompanamientosSeleccionados() As String
            Dim lista As New List(Of String)()
            If chkPapasFritas.Checked Then lista.Add("Papas Fritas")
            If chkEnsalada.Checked Then lista.Add("Ensalada Fresca")
            If chkArroz.Checked Then lista.Add("Arroz con Choclo")
            If chkSalsas.Checked Then lista.Add("Salsas de la Casa")

            If lista.Count = 0 Then Return "Sin Acompañamiento"
            Return String.Join(", ", lista)
        End Function

        Private Function ObtenerTipoServicioSeleccionado() As String
            If rbParaLlevar.Checked Then Return "Para Llevar"
            If rbDelivery.Checked Then Return "Delivery"
            Return "En Mesa"
        End Function

        Private Sub LimpiarFormulario()
            _idPedidoSeleccionado = 0
            txtNombreCliente.Clear()
            txtMesa.Clear()
            If cboPlatoPrincipal.Items.Count > 0 Then cboPlatoPrincipal.SelectedIndex = 0
            chkPapasFritas.Checked = False
            chkEnsalada.Checked = False
            chkArroz.Checked = False
            chkSalsas.Checked = False
            rbEnMesa.Checked = True
            txtNombreCliente.Focus()
        End Sub

        Private Sub RefrescarGrilla()
            Dim vista As New DataView(_tablaPedidos)
            dgvPedidos.DataSource = vista
            lblTotalRegistros.Text = $"Total de Pedidos Registrados: {_tablaPedidos.Rows.Count}"

            If dgvPedidos.Columns.Count > 0 Then
                dgvPedidos.Columns("ID").Width = 45
                dgvPedidos.Columns("Cliente").Width = 130
                dgvPedidos.Columns("Mesa").Width = 60
                dgvPedidos.Columns("PlatoPrincipal").Width = 160
                dgvPedidos.Columns("TipoServicio").Width = 85
                dgvPedidos.Columns("FechaHora").Width = 70
            End If
        End Sub

    End Class
End Namespace
