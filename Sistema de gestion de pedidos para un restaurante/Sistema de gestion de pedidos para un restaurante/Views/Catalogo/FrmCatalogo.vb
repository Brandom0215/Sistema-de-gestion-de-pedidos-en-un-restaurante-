Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Catalogo
    ''' <summary>
    ''' Formulario de Administración de Catálogo de Productos y Menú (RF-012 / CU-007).
    ''' Permite gestionar la creación, edición, desactivación/disponibilidad y eliminación de platos del restaurante.
    ''' </summary>
    Public Class FrmCatalogo

        ''' <summary> ID del plato seleccionado en la grilla para edición o eliminación </summary>
        Private _idPlatoSeleccionado As Integer = 0

        Public Sub New()
            InitializeComponent()
        End Sub

        Private WithEvents _tmrAutoRefresh As Timer
        Private _refrescandoCatalogo As Boolean = False

        Private Sub FrmCatalogo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarCategorias()
            RefrescarGrilla()

            _tmrAutoRefresh = New Timer() With {.Interval = 3000, .Enabled = True}
        End Sub

        Private Sub FrmCatalogo_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            If _tmrAutoRefresh IsNot Nothing Then
                _tmrAutoRefresh.Stop()
                _tmrAutoRefresh.Dispose()
            End If
        End Sub

        Private Async Sub _tmrAutoRefresh_Tick(sender As Object, e As EventArgs) Handles _tmrAutoRefresh.Tick
            If Me.DesignMode OrElse System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then Return
            If _refrescandoCatalogo Then Return
            _refrescandoCatalogo = True

            Try
                Dim dt = Await System.Threading.Tasks.Task.Run(Function() Data.PlatoDAO.ObtenerTodos())
                If Not Me.IsDisposed AndAlso Me.IsHandleCreated Then
                    Dim intScrollRow As Integer = If(dgvCatalogo.FirstDisplayedScrollingRowIndex >= 0, dgvCatalogo.FirstDisplayedScrollingRowIndex, 0)
                    Dim intSelectedId As Integer = _idPlatoSeleccionado

                    dgvCatalogo.DataSource = dt
                    If dgvCatalogo.Columns.Contains("Precio") Then
                        dgvCatalogo.Columns("Precio").DefaultCellStyle.Format = "C2"
                    End If

                    If intScrollRow < dgvCatalogo.Rows.Count Then
                        dgvCatalogo.FirstDisplayedScrollingRowIndex = intScrollRow
                    End If
                    For Each row As DataGridViewRow In dgvCatalogo.Rows
                        If Convert.ToInt32(row.Cells("ID").Value) = intSelectedId Then
                            row.Selected = True
                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception
            Finally
                _refrescandoCatalogo = False
            End Try
        End Sub

        ' =========================================================================
        ' ESTILIZADO Y CONFIGURACIÓN VISUAL
        ' =========================================================================

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlHeaderContainer.BackColor = ThemeConfig.ColorBackgroundApp

            lblTituloCatalogo.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtituloCatalogo.ForeColor = ThemeConfig.ColorTextMuted

            grpFormularioPlato.ForeColor = ThemeConfig.ColorSecondary
            grpListadoPlatos.ForeColor = ThemeConfig.ColorSecondary

            lblNombrePlato.ForeColor = ThemeConfig.ColorNeutralDark
            lblCategoria.ForeColor = ThemeConfig.ColorNeutralDark
            lblPrecio.ForeColor = ThemeConfig.ColorNeutralDark
            lblTiempoCoccion.ForeColor = ThemeConfig.ColorNeutralDark
            lblDescripcion.ForeColor = ThemeConfig.ColorNeutralDark

            ThemeConfig.EstilizarBotonPrimario(btnGuardarPlato)
            ThemeConfig.EstilizarBotonSecundario(btnActualizarPlato)
            ThemeConfig.EstilizarBotonEliminar(btnEliminarPlato)
            ThemeConfig.EstilizarBotonSecundario(btnLimpiarPlato)
            ThemeConfig.EstilizarBotonSecundario(btnBuscarPlato)
            ThemeConfig.EstilizarBotonSecundario(btnMostrarTodo)

            ' Estilizado de la DataGridView
            dgvCatalogo.BackgroundColor = Color.White
            dgvCatalogo.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 230, 220)
            dgvCatalogo.DefaultCellStyle.SelectionForeColor = ThemeConfig.ColorNeutralDark
            dgvCatalogo.ColumnHeadersDefaultCellStyle.BackColor = ThemeConfig.ColorSecondary
            dgvCatalogo.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgvCatalogo.ColumnHeadersDefaultCellStyle.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            dgvCatalogo.EnableHeadersVisualStyles = False
            dgvCatalogo.RowTemplate.Height = 28
        End Sub

        Private Sub CargarCategorias()
            cboCategoria.Items.Clear()
            cboCategoria.Items.Add("Especialidades")
            cboCategoria.Items.Add("Platos Fuertes")
            cboCategoria.Items.Add("Autóctonos")
            cboCategoria.Items.Add("Bebidas")
            cboCategoria.Items.Add("Postres")
            cboCategoria.SelectedIndex = 0
        End Sub

        Private Sub RefrescarGrilla()
            Dim dt As DataTable = Data.PlatoDAO.ObtenerTodos()
            dgvCatalogo.DataSource = dt

            If dgvCatalogo.Columns.Contains("Precio") Then
                dgvCatalogo.Columns("Precio").DefaultCellStyle.Format = "C2"
            End If
        End Sub

        ' =========================================================================
        ' OPERACIONES CRUD Y EVENTOS DE BOTONES
        ' =========================================================================

        Private Sub btnGuardarPlato_Click(sender As Object, e As EventArgs) Handles btnGuardarPlato.Click
            If String.IsNullOrWhiteSpace(txtNombrePlato.Text) Then
                MostrarMensajeAdvertencia("Por favor, ingrese el nombre del plato.", "Validación")
                txtNombrePlato.Focus()
                Return
            End If

            Dim precioValido As Decimal = 0D
            If Not Decimal.TryParse(txtPrecio.Text.Trim(), precioValido) OrElse precioValido <= 0 Then
                MostrarMensajeAdvertencia("Por favor, ingrese un precio válido mayor a 0.", "Validación")
                txtPrecio.Focus()
                Return
            End If

            Dim nombre As String = txtNombrePlato.Text.Trim()
            Dim categoria As String = cboCategoria.SelectedItem.ToString()
            Dim tiempo As String = txtTiempoCoccion.Text.Trim()
            Dim disponible As Boolean = chkDisponible.Checked
            Dim descripcion As String = txtDescripcion.Text.Trim()

            Dim exito As Boolean = Data.PlatoDAO.Guardar(nombre, categoria, precioValido, tiempo, disponible, descripcion)
            If exito Then
                MostrarMensajeExito($"¡Plato '{nombre}' guardado exitosamente en el catálogo!", "Catálogo Actualizado")
                LimpiarFormulario()
                RefrescarGrilla()
            Else
                MostrarMensajeError("No se pudo guardar el plato en el catálogo.", "Error")
            End If
        End Sub

        Private Sub btnActualizarPlato_Click(sender As Object, e As EventArgs) Handles btnActualizarPlato.Click
            If _idPlatoSeleccionado <= 0 Then
                MostrarMensajeAdvertencia("Seleccione un plato de la lista para actualizar.", "Atención")
                Return
            End If

            If String.IsNullOrWhiteSpace(txtNombrePlato.Text) Then
                MostrarMensajeAdvertencia("El nombre del plato no puede estar vacío.", "Validación")
                Return
            End If

            Dim precioValido As Decimal = 0D
            If Not Decimal.TryParse(txtPrecio.Text.Trim(), precioValido) OrElse precioValido <= 0 Then
                MostrarMensajeAdvertencia("Ingrese un precio válido.", "Validación")
                Return
            End If

            Dim nombre As String = txtNombrePlato.Text.Trim()
            Dim categoria As String = cboCategoria.SelectedItem.ToString()
            Dim tiempo As String = txtTiempoCoccion.Text.Trim()
            Dim disponible As Boolean = chkDisponible.Checked
            Dim descripcion As String = txtDescripcion.Text.Trim()

            Dim exito As Boolean = Data.PlatoDAO.Actualizar(_idPlatoSeleccionado, nombre, categoria, precioValido, tiempo, disponible, descripcion)
            If exito Then
                MostrarMensajeExito("Plato actualizado exitosamente.", "Catálogo Actualizado")
                LimpiarFormulario()
                RefrescarGrilla()
            End If
        End Sub

        Private Sub btnEliminarPlato_Click(sender As Object, e As EventArgs) Handles btnEliminarPlato.Click
            If _idPlatoSeleccionado <= 0 Then
                MostrarMensajeAdvertencia("Seleccione un plato de la lista para eliminar.", "Atención")
                Return
            End If

            If ConfirmarAccion("¿Está seguro de eliminar el plato seleccionado del catálogo?", "Confirmar Eliminación") Then
                Data.PlatoDAO.Eliminar(_idPlatoSeleccionado)
                LimpiarFormulario()
                RefrescarGrilla()
            End If
        End Sub

        Private Sub btnLimpiarPlato_Click(sender As Object, e As EventArgs) Handles btnLimpiarPlato.Click
            LimpiarFormulario()
        End Sub

        Private Sub LimpiarFormulario()
            _idPlatoSeleccionado = 0
            txtNombrePlato.Text = ""
            txtPrecio.Text = ""
            txtTiempoCoccion.Text = ""
            txtDescripcion.Text = ""
            chkDisponible.Checked = True
            cboCategoria.SelectedIndex = 0
            btnGuardarPlato.Enabled = True
            txtNombrePlato.Focus()
        End Sub

        Private Sub dgvCatalogo_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCatalogo.CellClick
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = dgvCatalogo.Rows(e.RowIndex)
                _idPlatoSeleccionado = Convert.ToInt32(row.Cells("ID").Value)
                txtNombrePlato.Text = row.Cells("Nombre").Value.ToString()
                txtPrecio.Text = Convert.ToDecimal(row.Cells("Precio").Value).ToString("F2")
                txtTiempoCoccion.Text = row.Cells("TiempoCoccion").Value.ToString()
                txtDescripcion.Text = row.Cells("Descripcion").Value.ToString()
                chkDisponible.Checked = (row.Cells("Estado").Value.ToString() = "Disponible")

                Dim cat As String = row.Cells("Categoria").Value.ToString()
                If cboCategoria.Items.Contains(cat) Then
                    cboCategoria.SelectedItem = cat
                End If
            End If
        End Sub

        Private Sub btnBuscarPlato_Click(sender As Object, e As EventArgs) Handles btnBuscarPlato.Click
            Dim filtro As String = txtBuscarPlato.Text.Trim().ToLower()
            If String.IsNullOrEmpty(filtro) Then
                RefrescarGrilla()
                Return
            End If

            Dim dt As DataTable = Data.PlatoDAO.ObtenerTodos()
            Dim dv As New DataView(dt)
            dv.RowFilter = String.Format("Nombre LIKE '%{0}%' OR Categoria LIKE '%{0}%' OR Descripcion LIKE '%{0}%'", filtro.Replace("'", "''"))
            dgvCatalogo.DataSource = dv
        End Sub

        Private Sub btnMostrarTodo_Click(sender As Object, e As EventArgs) Handles btnMostrarTodo.Click
            txtBuscarPlato.Text = ""
            RefrescarGrilla()
        End Sub

    End Class
End Namespace
