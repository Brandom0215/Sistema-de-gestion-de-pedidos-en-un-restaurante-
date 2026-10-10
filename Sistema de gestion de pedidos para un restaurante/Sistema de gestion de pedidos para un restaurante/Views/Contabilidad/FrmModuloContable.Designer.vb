Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Contabilidad
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmModuloContable
        Inherits Views.Common.FrmBaseForm

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        Friend WithEvents pnlHeaderContainer As Panel
        Friend WithEvents lblTitulo As Label
        Friend WithEvents lblSubtitulo As Label
        Friend WithEvents btnActualizarTodo As Button
        Friend WithEvents btnExportarInformeCPA As Button

        Friend WithEvents tabContabilidad As TabControl
        Friend WithEvents tabLibroDiario As TabPage
        Friend WithEvents tabLibroMayor As TabPage
        Friend WithEvents tabEstadosFinancieros As TabPage
        Friend WithEvents tabCatalogoCuentas As TabPage
        Friend WithEvents tabPlanilla As TabPage
        Friend WithEvents tabPeriodosCierre As TabPage
        Friend WithEvents tabAuditoria As TabPage

        ' Tab 1: Libro Diario
        Friend WithEvents pnlFiltrosDiario As Panel
        Friend WithEvents lblFechaDesde As Label
        Friend WithEvents dtpFechaDesde As DateTimePicker
        Friend WithEvents lblFechaHasta As Label
        Friend WithEvents dtpFechaHasta As DateTimePicker
        Friend WithEvents lblModulo As Label
        Friend WithEvents cboModuloOrigen As ComboBox
        Friend WithEvents btnFiltrarDiario As Button
        Friend WithEvents splitDiario As SplitContainer
        Friend WithEvents dgvLibroDiario As DataGridView
        Friend WithEvents dgvDetalleAsiento As DataGridView
        Friend WithEvents pnlBadgePartidaDoble As Panel
        Friend WithEvents lblEstadoPartidaDoble As Label

        ' Tab 2: Libro Mayor
        Friend WithEvents pnlFiltrosMayor As Panel
        Friend WithEvents lblCuentaMayor As Label
        Friend WithEvents cboCuentaMayor As ComboBox
        Friend WithEvents btnFiltrarMayor As Button
        Friend WithEvents dgvLibroMayor As DataGridView
        Friend WithEvents pnlMayorTotales As Panel
        Friend WithEvents lblTotalDebeMayor As Label
        Friend WithEvents lblTotalHaberMayor As Label
        Friend WithEvents lblSaldoMayor As Label

        ' Tab 1: Resumen Financiero
        Friend WithEvents pnlKpiContainer As Panel
        Friend WithEvents pnlCardVentas As Panel
        Friend WithEvents lblTituloKpiVentas As Label
        Friend WithEvents lblMontoKpiVentas As Label
        Friend WithEvents pnlCardGastos As Panel
        Friend WithEvents lblTituloKpiGastos As Label
        Friend WithEvents lblMontoKpiGastos As Label
        Friend WithEvents pnlCardNomina As Panel
        Friend WithEvents lblTituloKpiNomina As Label
        Friend WithEvents lblMontoKpiNomina As Label
        Friend WithEvents pnlCardUtilidad As Panel
        Friend WithEvents lblTituloKpiUtilidad As Label
        Friend WithEvents lblMontoKpiUtilidad As Label
        Friend WithEvents pnlCardItbms As Panel
        Friend WithEvents lblTituloKpiItbms As Label
        Friend WithEvents lblMontoKpiItbms As Label
        Friend WithEvents pnlSelectorReporte As Panel
        Friend WithEvents rdoEstadoResultados As RadioButton
        Friend WithEvents rdoBalanceGeneral As RadioButton
        Friend WithEvents rdoBalanzaComprobacion As RadioButton
        Friend WithEvents dgvEstadosFinancieros As DataGridView
        Friend WithEvents lblResumenFinanciero As Label

        ' Tab 4: Catálogo de Cuentas
        Friend WithEvents pnlFiltroCatalogo As Panel
        Friend WithEvents lblBuscarCuenta As Label
        Friend WithEvents txtBuscarCuenta As TextBox
        Friend WithEvents dgvCatalogo As DataGridView

        ' Tab 5: Planilla
        Friend WithEvents splitPlanilla As SplitContainer
        Friend WithEvents dgvEmpleados As DataGridView
        Friend WithEvents pnlResumenPlanilla As Panel
        Friend WithEvents lblTituloResumenPlanilla As Label
        Friend WithEvents lblDetalleSalariosBrutos As Label
        Friend WithEvents lblDetalleRetencionObrera As Label
        Friend WithEvents lblDetalleSalarioNetoACH As Label
        Friend WithEvents lblDetalleCargasPatronales As Label
        Friend WithEvents lblDetalleCostoTotalEmpresa As Label
        Friend WithEvents btnRegistrarPlanillaBD As Button

        ' Tab 6: Períodos y Cierre
        Friend WithEvents splitPeriodos As SplitContainer
        Friend WithEvents grpPeriodos As GroupBox
        Friend WithEvents dgvPeriodos As DataGridView
        Friend WithEvents btnCerrarPeriodo As Button
        Friend WithEvents grpMovimientosCaja As GroupBox
        Friend WithEvents dgvMovimientosCaja As DataGridView
        Friend WithEvents btnRegistrarAperturaGasto As Button

        ' Tab 7: Auditoría
        Friend WithEvents dgvAuditoria As DataGridView

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.pnlHeaderContainer = New System.Windows.Forms.Panel()
            Me.lblTitulo = New System.Windows.Forms.Label()
            Me.lblSubtitulo = New System.Windows.Forms.Label()
            Me.btnActualizarTodo = New System.Windows.Forms.Button()
            Me.btnExportarInformeCPA = New System.Windows.Forms.Button()

            Me.tabContabilidad = New System.Windows.Forms.TabControl()
            Me.tabLibroDiario = New System.Windows.Forms.TabPage()
            Me.tabLibroMayor = New System.Windows.Forms.TabPage()
            Me.tabEstadosFinancieros = New System.Windows.Forms.TabPage()
            Me.tabCatalogoCuentas = New System.Windows.Forms.TabPage()
            Me.tabPlanilla = New System.Windows.Forms.TabPage()
            Me.tabPeriodosCierre = New System.Windows.Forms.TabPage()
            Me.tabAuditoria = New System.Windows.Forms.TabPage()

            ' 1. Libro Diario
            Me.pnlFiltrosDiario = New System.Windows.Forms.Panel()
            Me.lblFechaDesde = New System.Windows.Forms.Label()
            Me.dtpFechaDesde = New System.Windows.Forms.DateTimePicker()
            Me.lblFechaHasta = New System.Windows.Forms.Label()
            Me.dtpFechaHasta = New System.Windows.Forms.DateTimePicker()
            Me.lblModulo = New System.Windows.Forms.Label()
            Me.cboModuloOrigen = New System.Windows.Forms.ComboBox()
            Me.btnFiltrarDiario = New System.Windows.Forms.Button()
            Me.splitDiario = New System.Windows.Forms.SplitContainer()
            Me.dgvLibroDiario = New System.Windows.Forms.DataGridView()
            Me.dgvDetalleAsiento = New System.Windows.Forms.DataGridView()
            Me.pnlBadgePartidaDoble = New System.Windows.Forms.Panel()
            Me.lblEstadoPartidaDoble = New System.Windows.Forms.Label()

            ' 2. Libro Mayor
            Me.pnlFiltrosMayor = New System.Windows.Forms.Panel()
            Me.lblCuentaMayor = New System.Windows.Forms.Label()
            Me.cboCuentaMayor = New System.Windows.Forms.ComboBox()
            Me.btnFiltrarMayor = New System.Windows.Forms.Button()
            Me.dgvLibroMayor = New System.Windows.Forms.DataGridView()
            Me.pnlMayorTotales = New System.Windows.Forms.Panel()
            Me.lblTotalDebeMayor = New System.Windows.Forms.Label()
            Me.lblTotalHaberMayor = New System.Windows.Forms.Label()
            Me.lblSaldoMayor = New System.Windows.Forms.Label()

            ' Resumen Financiero y KPIs
            Me.pnlKpiContainer = New System.Windows.Forms.Panel()
            Me.pnlCardVentas = New System.Windows.Forms.Panel()
            Me.lblTituloKpiVentas = New System.Windows.Forms.Label()
            Me.lblMontoKpiVentas = New System.Windows.Forms.Label()
            Me.pnlCardGastos = New System.Windows.Forms.Panel()
            Me.lblTituloKpiGastos = New System.Windows.Forms.Label()
            Me.lblMontoKpiGastos = New System.Windows.Forms.Label()
            Me.pnlCardNomina = New System.Windows.Forms.Panel()
            Me.lblTituloKpiNomina = New System.Windows.Forms.Label()
            Me.lblMontoKpiNomina = New System.Windows.Forms.Label()
            Me.pnlCardUtilidad = New System.Windows.Forms.Panel()
            Me.lblTituloKpiUtilidad = New System.Windows.Forms.Label()
            Me.lblMontoKpiUtilidad = New System.Windows.Forms.Label()
            Me.pnlCardItbms = New System.Windows.Forms.Panel()
            Me.lblTituloKpiItbms = New System.Windows.Forms.Label()
            Me.lblMontoKpiItbms = New System.Windows.Forms.Label()
            Me.pnlSelectorReporte = New System.Windows.Forms.Panel()
            Me.rdoEstadoResultados = New System.Windows.Forms.RadioButton()
            Me.rdoBalanceGeneral = New System.Windows.Forms.RadioButton()
            Me.rdoBalanzaComprobacion = New System.Windows.Forms.RadioButton()
            Me.lblResumenFinanciero = New System.Windows.Forms.Label()
            Me.dgvEstadosFinancieros = New System.Windows.Forms.DataGridView()

            ' 4. Catálogo
            Me.pnlFiltroCatalogo = New System.Windows.Forms.Panel()
            Me.lblBuscarCuenta = New System.Windows.Forms.Label()
            Me.txtBuscarCuenta = New System.Windows.Forms.TextBox()
            Me.dgvCatalogo = New System.Windows.Forms.DataGridView()

            ' 5. Planilla
            Me.splitPlanilla = New System.Windows.Forms.SplitContainer()
            Me.dgvEmpleados = New System.Windows.Forms.DataGridView()
            Me.pnlResumenPlanilla = New System.Windows.Forms.Panel()
            Me.lblTituloResumenPlanilla = New System.Windows.Forms.Label()
            Me.lblDetalleSalariosBrutos = New System.Windows.Forms.Label()
            Me.lblDetalleRetencionObrera = New System.Windows.Forms.Label()
            Me.lblDetalleSalarioNetoACH = New System.Windows.Forms.Label()
            Me.lblDetalleCargasPatronales = New System.Windows.Forms.Label()
            Me.lblDetalleCostoTotalEmpresa = New System.Windows.Forms.Label()
            Me.btnRegistrarPlanillaBD = New System.Windows.Forms.Button()

            ' 6. Períodos
            Me.splitPeriodos = New System.Windows.Forms.SplitContainer()
            Me.grpPeriodos = New System.Windows.Forms.GroupBox()
            Me.dgvPeriodos = New System.Windows.Forms.DataGridView()
            Me.btnCerrarPeriodo = New System.Windows.Forms.Button()
            Me.grpMovimientosCaja = New System.Windows.Forms.GroupBox()
            Me.dgvMovimientosCaja = New System.Windows.Forms.DataGridView()
            Me.btnRegistrarAperturaGasto = New System.Windows.Forms.Button()

            ' 7. Auditoría
            Me.dgvAuditoria = New System.Windows.Forms.DataGridView()

            ' Setup Panels
            Me.pnlHeaderContainer.SuspendLayout()
            Me.tabContabilidad.SuspendLayout()
            Me.tabEstadosFinancieros.SuspendLayout()
            Me.pnlKpiContainer.SuspendLayout()
            Me.pnlCardVentas.SuspendLayout()
            Me.pnlCardGastos.SuspendLayout()
            Me.pnlCardNomina.SuspendLayout()
            Me.pnlCardUtilidad.SuspendLayout()
            Me.pnlCardItbms.SuspendLayout()
            Me.tabLibroDiario.SuspendLayout()
            Me.tabLibroMayor.SuspendLayout()
            Me.tabCatalogoCuentas.SuspendLayout()
            Me.tabPlanilla.SuspendLayout()
            Me.tabPeriodosCierre.SuspendLayout()
            Me.tabAuditoria.SuspendLayout()
            CType(Me.splitDiario, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitDiario.Panel1.SuspendLayout()
            Me.splitDiario.Panel2.SuspendLayout()
            Me.splitDiario.SuspendLayout()
            CType(Me.dgvLibroDiario, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dgvDetalleAsiento, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.splitPlanilla, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitPlanilla.Panel1.SuspendLayout()
            Me.splitPlanilla.Panel2.SuspendLayout()
            Me.splitPlanilla.SuspendLayout()
            CType(Me.dgvEmpleados, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.splitPeriodos, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitPeriodos.Panel1.SuspendLayout()
            Me.splitPeriodos.Panel2.SuspendLayout()
            Me.splitPeriodos.SuspendLayout()
            Me.grpPeriodos.SuspendLayout()
            CType(Me.dgvPeriodos, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpMovimientosCaja.SuspendLayout()
            CType(Me.dgvMovimientosCaja, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dgvLibroMayor, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dgvEstadosFinancieros, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dgvCatalogo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.dgvAuditoria, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()

            '
            ' pnlHeaderContainer
            '
            Me.pnlHeaderContainer.Controls.Add(Me.lblFechaDesde)
            Me.pnlHeaderContainer.Controls.Add(Me.dtpFechaDesde)
            Me.pnlHeaderContainer.Controls.Add(Me.lblFechaHasta)
            Me.pnlHeaderContainer.Controls.Add(Me.dtpFechaHasta)
            Me.pnlHeaderContainer.Controls.Add(Me.btnActualizarTodo)
            Me.pnlHeaderContainer.Controls.Add(Me.btnExportarInformeCPA)
            Me.pnlHeaderContainer.Controls.Add(Me.lblSubtitulo)
            Me.pnlHeaderContainer.Controls.Add(Me.lblTitulo)
            Me.pnlHeaderContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderContainer.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeaderContainer.Name = "pnlHeaderContainer"
            Me.pnlHeaderContainer.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
            Me.pnlHeaderContainer.Size = New System.Drawing.Size(1100, 75)
            Me.pnlHeaderContainer.TabIndex = 0

            '
            ' lblTitulo
            '
            Me.lblTitulo.AutoSize = True
            Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold)
            Me.lblTitulo.Location = New System.Drawing.Point(12, 10)
            Me.lblTitulo.Name = "lblTitulo"
            Me.lblTitulo.Size = New System.Drawing.Size(260, 28)
            Me.lblTitulo.TabIndex = 0
            Me.lblTitulo.Text = "Contabilidad y Finanzas"

            '
            ' lblSubtitulo
            '
            Me.lblSubtitulo.AutoSize = True
            Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 8.75F)
            Me.lblSubtitulo.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtitulo.Location = New System.Drawing.Point(14, 40)
            Me.lblSubtitulo.Name = "lblSubtitulo"
            Me.lblSubtitulo.Size = New System.Drawing.Size(350, 15)
            Me.lblSubtitulo.TabIndex = 1
            Me.lblSubtitulo.Text = "Libros contables, estados financieros y control fiscal"

            '
            ' lblFechaDesde
            '
            Me.lblFechaDesde.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblFechaDesde.AutoSize = True
            Me.lblFechaDesde.Location = New System.Drawing.Point(440, 24)
            Me.lblFechaDesde.Name = "lblFechaDesde"
            Me.lblFechaDesde.Size = New System.Drawing.Size(42, 15)
            Me.lblFechaDesde.Text = "Desde:"

            '
            ' dtpFechaDesde
            '
            Me.dtpFechaDesde.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpFechaDesde.Location = New System.Drawing.Point(485, 20)
            Me.dtpFechaDesde.Name = "dtpFechaDesde"
            Me.dtpFechaDesde.Size = New System.Drawing.Size(95, 23)
            Me.dtpFechaDesde.TabIndex = 2

            '
            ' lblFechaHasta
            '
            Me.lblFechaHasta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblFechaHasta.AutoSize = True
            Me.lblFechaHasta.Location = New System.Drawing.Point(588, 24)
            Me.lblFechaHasta.Name = "lblFechaHasta"
            Me.lblFechaHasta.Size = New System.Drawing.Size(40, 15)
            Me.lblFechaHasta.Text = "Hasta:"

            '
            ' dtpFechaHasta
            '
            Me.dtpFechaHasta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dtpFechaHasta.Location = New System.Drawing.Point(632, 20)
            Me.dtpFechaHasta.Name = "dtpFechaHasta"
            Me.dtpFechaHasta.Size = New System.Drawing.Size(95, 23)
            Me.dtpFechaHasta.TabIndex = 3

            '
            ' btnActualizarTodo
            '
            Me.btnActualizarTodo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnActualizarTodo.Location = New System.Drawing.Point(738, 16)
            Me.btnActualizarTodo.Name = "btnActualizarTodo"
            Me.btnActualizarTodo.Size = New System.Drawing.Size(120, 34)
            Me.btnActualizarTodo.TabIndex = 4
            Me.btnActualizarTodo.Text = "🔄 Actualizar"
            Me.btnActualizarTodo.UseVisualStyleBackColor = True

            '
            ' btnExportarInformeCPA
            '
            Me.btnExportarInformeCPA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnExportarInformeCPA.Location = New System.Drawing.Point(868, 16)
            Me.btnExportarInformeCPA.Name = "btnExportarInformeCPA"
            Me.btnExportarInformeCPA.Size = New System.Drawing.Size(215, 34)
            Me.btnExportarInformeCPA.TabIndex = 5
            Me.btnExportarInformeCPA.Text = "📑 Exportar Informe CPA"
            Me.btnExportarInformeCPA.UseVisualStyleBackColor = True

            '
            ' tabContabilidad
            '
            Me.tabContabilidad.Controls.Add(Me.tabEstadosFinancieros)
            Me.tabContabilidad.Controls.Add(Me.tabLibroDiario)
            Me.tabContabilidad.Controls.Add(Me.tabPlanilla)
            Me.tabContabilidad.Controls.Add(Me.tabPeriodosCierre)
            Me.tabContabilidad.Controls.Add(Me.tabCatalogoCuentas)
            Me.tabContabilidad.Controls.Add(Me.tabAuditoria)
            Me.tabContabilidad.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabContabilidad.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Regular)
            Me.tabContabilidad.ItemSize = New System.Drawing.Size(130, 28)
            Me.tabContabilidad.Location = New System.Drawing.Point(0, 75)
            Me.tabContabilidad.Name = "tabContabilidad"
            Me.tabContabilidad.SelectedIndex = 0
            Me.tabContabilidad.Size = New System.Drawing.Size(1100, 605)
            Me.tabContabilidad.TabIndex = 1

            '
            ' TAB 1: LIBRO DIARIO
            '
            Me.tabLibroDiario.Controls.Add(Me.splitDiario)
            Me.tabLibroDiario.Controls.Add(Me.pnlFiltrosDiario)
            Me.tabLibroDiario.Location = New System.Drawing.Point(4, 32)
            Me.tabLibroDiario.Name = "tabLibroDiario"
            Me.tabLibroDiario.Padding = New System.Windows.Forms.Padding(6)
            Me.tabLibroDiario.Size = New System.Drawing.Size(1092, 569)
            Me.tabLibroDiario.Text = "📖 Libro Diario"
            Me.tabLibroDiario.UseVisualStyleBackColor = True

            ' pnlFiltrosDiario
            Me.pnlFiltrosDiario.Controls.Add(Me.lblModulo)
            Me.pnlFiltrosDiario.Controls.Add(Me.cboModuloOrigen)
            Me.pnlFiltrosDiario.Controls.Add(Me.btnFiltrarDiario)
            Me.pnlFiltrosDiario.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltrosDiario.Location = New System.Drawing.Point(6, 6)
            Me.pnlFiltrosDiario.Name = "pnlFiltrosDiario"
            Me.pnlFiltrosDiario.Size = New System.Drawing.Size(1080, 42)
            Me.pnlFiltrosDiario.TabIndex = 0

            Me.lblModulo.AutoSize = True
            Me.lblModulo.Location = New System.Drawing.Point(8, 14)
            Me.lblModulo.Name = "lblModulo"
            Me.lblModulo.Size = New System.Drawing.Size(110, 15)
            Me.lblModulo.Text = "Filtrar por Módulo:"

            Me.cboModuloOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboModuloOrigen.FormattingEnabled = True
            Me.cboModuloOrigen.Items.AddRange(New Object() {"TODOS", "VENTAS", "CAJA", "NOMINA", "DEVOLUCIONES", "AJUSTES"})
            Me.cboModuloOrigen.Location = New System.Drawing.Point(125, 10)
            Me.cboModuloOrigen.Name = "cboModuloOrigen"
            Me.cboModuloOrigen.Size = New System.Drawing.Size(140, 23)
            Me.cboModuloOrigen.SelectedIndex = 0

            Me.btnFiltrarDiario.Location = New System.Drawing.Point(275, 8)
            Me.btnFiltrarDiario.Name = "btnFiltrarDiario"
            Me.btnFiltrarDiario.Size = New System.Drawing.Size(105, 27)
            Me.btnFiltrarDiario.TabIndex = 6
            Me.btnFiltrarDiario.Text = "🔍 Filtrar"
            Me.btnFiltrarDiario.UseVisualStyleBackColor = True

            ' splitDiario
            Me.splitDiario.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitDiario.Location = New System.Drawing.Point(6, 54)
            Me.splitDiario.Name = "splitDiario"
            Me.splitDiario.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.splitDiario.Size = New System.Drawing.Size(1080, 509)
            Me.splitDiario.SplitterDistance = 270
            Me.splitDiario.TabIndex = 1

            Me.dgvLibroDiario.AllowUserToAddRows = False
            Me.dgvLibroDiario.AllowUserToDeleteRows = False
            Me.dgvLibroDiario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvLibroDiario.BackgroundColor = System.Drawing.Color.White
            Me.dgvLibroDiario.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvLibroDiario.Location = New System.Drawing.Point(0, 0)
            Me.dgvLibroDiario.MultiSelect = False
            Me.dgvLibroDiario.Name = "dgvLibroDiario"
            Me.dgvLibroDiario.ReadOnly = True
            Me.dgvLibroDiario.RowHeadersVisible = False
            Me.dgvLibroDiario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvLibroDiario.Size = New System.Drawing.Size(1080, 270)
            Me.dgvLibroDiario.TabIndex = 0

            Me.splitDiario.Panel1.Controls.Add(Me.dgvLibroDiario)

            Me.splitDiario.Panel2.Controls.Add(Me.dgvDetalleAsiento)
            Me.splitDiario.Panel2.Controls.Add(Me.pnlBadgePartidaDoble)

            Me.pnlBadgePartidaDoble.Controls.Add(Me.lblEstadoPartidaDoble)
            Me.pnlBadgePartidaDoble.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlBadgePartidaDoble.Location = New System.Drawing.Point(0, 0)
            Me.pnlBadgePartidaDoble.Name = "pnlBadgePartidaDoble"
            Me.pnlBadgePartidaDoble.Size = New System.Drawing.Size(1080, 32)
            Me.pnlBadgePartidaDoble.TabIndex = 0

            Me.lblEstadoPartidaDoble.AutoSize = True
            Me.lblEstadoPartidaDoble.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblEstadoPartidaDoble.Location = New System.Drawing.Point(8, 8)
            Me.lblEstadoPartidaDoble.Name = "lblEstadoPartidaDoble"
            Me.lblEstadoPartidaDoble.Size = New System.Drawing.Size(460, 15)
            Me.lblEstadoPartidaDoble.Text = "⚖️ Detalle de Partida Doble del Asiento Seleccionado (Debe = Haber Verificado)"

            Me.dgvDetalleAsiento.AllowUserToAddRows = False
            Me.dgvDetalleAsiento.AllowUserToDeleteRows = False
            Me.dgvDetalleAsiento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvDetalleAsiento.BackgroundColor = System.Drawing.Color.White
            Me.dgvDetalleAsiento.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvDetalleAsiento.Location = New System.Drawing.Point(0, 32)
            Me.dgvDetalleAsiento.MultiSelect = False
            Me.dgvDetalleAsiento.Name = "dgvDetalleAsiento"
            Me.dgvDetalleAsiento.ReadOnly = True
            Me.dgvDetalleAsiento.RowHeadersVisible = False
            Me.dgvDetalleAsiento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvDetalleAsiento.Size = New System.Drawing.Size(1080, 203)
            Me.dgvDetalleAsiento.TabIndex = 1

            '
            ' TAB 2: LIBRO MAYOR
            '
            Me.tabLibroMayor.Controls.Add(Me.dgvLibroMayor)
            Me.tabLibroMayor.Controls.Add(Me.pnlMayorTotales)
            Me.tabLibroMayor.Controls.Add(Me.pnlFiltrosMayor)
            Me.tabLibroMayor.Location = New System.Drawing.Point(4, 32)
            Me.tabLibroMayor.Name = "tabLibroMayor"
            Me.tabLibroMayor.Padding = New System.Windows.Forms.Padding(6)
            Me.tabLibroMayor.Size = New System.Drawing.Size(1092, 569)
            Me.tabLibroMayor.Text = "📚 Libro Mayor"
            Me.tabLibroMayor.UseVisualStyleBackColor = True

            Me.pnlFiltrosMayor.Controls.Add(Me.lblCuentaMayor)
            Me.pnlFiltrosMayor.Controls.Add(Me.cboCuentaMayor)
            Me.pnlFiltrosMayor.Controls.Add(Me.btnFiltrarMayor)
            Me.pnlFiltrosMayor.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltrosMayor.Location = New System.Drawing.Point(6, 6)
            Me.pnlFiltrosMayor.Name = "pnlFiltrosMayor"
            Me.pnlFiltrosMayor.Size = New System.Drawing.Size(1080, 48)
            Me.pnlFiltrosMayor.TabIndex = 0

            Me.lblCuentaMayor.AutoSize = True
            Me.lblCuentaMayor.Location = New System.Drawing.Point(6, 16)
            Me.lblCuentaMayor.Name = "lblCuentaMayor"
            Me.lblCuentaMayor.Size = New System.Drawing.Size(148, 15)
            Me.lblCuentaMayor.Text = "Filtrar por Cuenta Contable:"

            Me.cboCuentaMayor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCuentaMayor.FormattingEnabled = True
            Me.cboCuentaMayor.Location = New System.Drawing.Point(160, 12)
            Me.cboCuentaMayor.Name = "cboCuentaMayor"
            Me.cboCuentaMayor.Size = New System.Drawing.Size(380, 23)
            Me.cboCuentaMayor.TabIndex = 1

            Me.btnFiltrarMayor.Location = New System.Drawing.Point(550, 10)
            Me.btnFiltrarMayor.Name = "btnFiltrarMayor"
            Me.btnFiltrarMayor.Size = New System.Drawing.Size(110, 27)
            Me.btnFiltrarMayor.TabIndex = 2
            Me.btnFiltrarMayor.Text = "🔍 Consultar"
            Me.btnFiltrarMayor.UseVisualStyleBackColor = True

            Me.dgvLibroMayor.AllowUserToAddRows = False
            Me.dgvLibroMayor.AllowUserToDeleteRows = False
            Me.dgvLibroMayor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvLibroMayor.BackgroundColor = System.Drawing.Color.White
            Me.dgvLibroMayor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvLibroMayor.Location = New System.Drawing.Point(6, 54)
            Me.dgvLibroMayor.MultiSelect = False
            Me.dgvLibroMayor.Name = "dgvLibroMayor"
            Me.dgvLibroMayor.ReadOnly = True
            Me.dgvLibroMayor.RowHeadersVisible = False
            Me.dgvLibroMayor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvLibroMayor.Size = New System.Drawing.Size(1080, 465)
            Me.dgvLibroMayor.TabIndex = 1

            Me.pnlMayorTotales.Controls.Add(Me.lblSaldoMayor)
            Me.pnlMayorTotales.Controls.Add(Me.lblTotalHaberMayor)
            Me.pnlMayorTotales.Controls.Add(Me.lblTotalDebeMayor)
            Me.pnlMayorTotales.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlMayorTotales.Location = New System.Drawing.Point(6, 519)
            Me.pnlMayorTotales.Name = "pnlMayorTotales"
            Me.pnlMayorTotales.Size = New System.Drawing.Size(1080, 44)
            Me.pnlMayorTotales.TabIndex = 2

            Me.lblTotalDebeMayor.AutoSize = True
            Me.lblTotalDebeMayor.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblTotalDebeMayor.Location = New System.Drawing.Point(12, 12)
            Me.lblTotalDebeMayor.Name = "lblTotalDebeMayor"
            Me.lblTotalDebeMayor.Size = New System.Drawing.Size(155, 17)
            Me.lblTotalDebeMayor.Text = "Total Débitos: $ 0.00"

            Me.lblTotalHaberMayor.AutoSize = True
            Me.lblTotalHaberMayor.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblTotalHaberMayor.Location = New System.Drawing.Point(260, 12)
            Me.lblTotalHaberMayor.Name = "lblTotalHaberMayor"
            Me.lblTotalHaberMayor.Size = New System.Drawing.Size(155, 17)
            Me.lblTotalHaberMayor.Text = "Total Créditos: $ 0.00"

            Me.lblSaldoMayor.AutoSize = True
            Me.lblSaldoMayor.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.lblSaldoMayor.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblSaldoMayor.Location = New System.Drawing.Point(520, 11)
            Me.lblSaldoMayor.Name = "lblSaldoMayor"
            Me.lblSaldoMayor.Size = New System.Drawing.Size(210, 19)
            Me.lblSaldoMayor.Text = "Saldo Neto Cuenta: $ 0.00"

            '
            ' TAB 1: RESUMEN FINANCIERO Y KPIS (DASHBOARD EJECUTIVO)
            '
            Me.tabEstadosFinancieros.Controls.Add(Me.dgvEstadosFinancieros)
            Me.tabEstadosFinancieros.Controls.Add(Me.pnlSelectorReporte)
            Me.tabEstadosFinancieros.Controls.Add(Me.pnlKpiContainer)
            Me.tabEstadosFinancieros.Location = New System.Drawing.Point(4, 32)
            Me.tabEstadosFinancieros.Name = "tabEstadosFinancieros"
            Me.tabEstadosFinancieros.Padding = New System.Windows.Forms.Padding(6)
            Me.tabEstadosFinancieros.Size = New System.Drawing.Size(1092, 569)
            Me.tabEstadosFinancieros.Text = "📊 Resumen Financiero"
            Me.tabEstadosFinancieros.UseVisualStyleBackColor = True

            '
            ' pnlKpiContainer
            '
            Me.pnlKpiContainer.Controls.Add(Me.pnlCardVentas)
            Me.pnlKpiContainer.Controls.Add(Me.pnlCardGastos)
            Me.pnlKpiContainer.Controls.Add(Me.pnlCardNomina)
            Me.pnlKpiContainer.Controls.Add(Me.pnlCardUtilidad)
            Me.pnlKpiContainer.Controls.Add(Me.pnlCardItbms)
            Me.pnlKpiContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlKpiContainer.Location = New System.Drawing.Point(6, 6)
            Me.pnlKpiContainer.Name = "pnlKpiContainer"
            Me.pnlKpiContainer.Size = New System.Drawing.Size(1080, 74)
            Me.pnlKpiContainer.TabIndex = 0

            ' pnlCardVentas
            Me.pnlCardVentas.BackColor = System.Drawing.Color.FromArgb(238, 247, 241)
            Me.pnlCardVentas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCardVentas.Controls.Add(Me.lblTituloKpiVentas)
            Me.pnlCardVentas.Controls.Add(Me.lblMontoKpiVentas)
            Me.pnlCardVentas.Location = New System.Drawing.Point(4, 4)
            Me.pnlCardVentas.Name = "pnlCardVentas"
            Me.pnlCardVentas.Size = New System.Drawing.Size(200, 64)
            Me.pnlCardVentas.TabIndex = 0

            Me.lblTituloKpiVentas.AutoSize = True
            Me.lblTituloKpiVentas.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloKpiVentas.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50)
            Me.lblTituloKpiVentas.Location = New System.Drawing.Point(8, 7)
            Me.lblTituloKpiVentas.Name = "lblTituloKpiVentas"
            Me.lblTituloKpiVentas.Size = New System.Drawing.Size(126, 13)
            Me.lblTituloKpiVentas.Text = "VENTAS / INGRESOS"

            Me.lblMontoKpiVentas.AutoSize = True
            Me.lblMontoKpiVentas.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoKpiVentas.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
            Me.lblMontoKpiVentas.Location = New System.Drawing.Point(8, 28)
            Me.lblMontoKpiVentas.Name = "lblMontoKpiVentas"
            Me.lblMontoKpiVentas.Size = New System.Drawing.Size(57, 21)
            Me.lblMontoKpiVentas.Text = "$ 0.00"

            ' pnlCardGastos
            Me.pnlCardGastos.BackColor = System.Drawing.Color.FromArgb(254, 242, 242)
            Me.pnlCardGastos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCardGastos.Controls.Add(Me.lblTituloKpiGastos)
            Me.pnlCardGastos.Controls.Add(Me.lblMontoKpiGastos)
            Me.pnlCardGastos.Location = New System.Drawing.Point(212, 4)
            Me.pnlCardGastos.Name = "pnlCardGastos"
            Me.pnlCardGastos.Size = New System.Drawing.Size(200, 64)
            Me.pnlCardGastos.TabIndex = 1

            Me.lblTituloKpiGastos.AutoSize = True
            Me.lblTituloKpiGastos.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloKpiGastos.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28)
            Me.lblTituloKpiGastos.Location = New System.Drawing.Point(8, 7)
            Me.lblTituloKpiGastos.Name = "lblTituloKpiGastos"
            Me.lblTituloKpiGastos.Size = New System.Drawing.Size(128, 13)
            Me.lblTituloKpiGastos.Text = "GASTOS OPERATIVOS"

            Me.lblMontoKpiGastos.AutoSize = True
            Me.lblMontoKpiGastos.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoKpiGastos.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
            Me.lblMontoKpiGastos.Location = New System.Drawing.Point(8, 28)
            Me.lblMontoKpiGastos.Name = "lblMontoKpiGastos"
            Me.lblMontoKpiGastos.Size = New System.Drawing.Size(57, 21)
            Me.lblMontoKpiGastos.Text = "$ 0.00"

            ' pnlCardNomina
            Me.pnlCardNomina.BackColor = System.Drawing.Color.FromArgb(239, 246, 255)
            Me.pnlCardNomina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCardNomina.Controls.Add(Me.lblTituloKpiNomina)
            Me.pnlCardNomina.Controls.Add(Me.lblMontoKpiNomina)
            Me.pnlCardNomina.Location = New System.Drawing.Point(420, 4)
            Me.pnlCardNomina.Name = "pnlCardNomina"
            Me.pnlCardNomina.Size = New System.Drawing.Size(210, 64)
            Me.pnlCardNomina.TabIndex = 2

            Me.lblTituloKpiNomina.AutoSize = True
            Me.lblTituloKpiNomina.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloKpiNomina.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216)
            Me.lblTituloKpiNomina.Location = New System.Drawing.Point(8, 7)
            Me.lblTituloKpiNomina.Name = "lblTituloKpiNomina"
            Me.lblTituloKpiNomina.Size = New System.Drawing.Size(155, 13)
            Me.lblTituloKpiNomina.Text = "PLANILLA (7 EMPLEADOS)"

            Me.lblMontoKpiNomina.AutoSize = True
            Me.lblMontoKpiNomina.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoKpiNomina.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
            Me.lblMontoKpiNomina.Location = New System.Drawing.Point(8, 28)
            Me.lblMontoKpiNomina.Name = "lblMontoKpiNomina"
            Me.lblMontoKpiNomina.Size = New System.Drawing.Size(57, 21)
            Me.lblMontoKpiNomina.Text = "$ 0.00"

            ' pnlCardUtilidad
            Me.pnlCardUtilidad.BackColor = System.Drawing.Color.FromArgb(240, 253, 244)
            Me.pnlCardUtilidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCardUtilidad.Controls.Add(Me.lblTituloKpiUtilidad)
            Me.pnlCardUtilidad.Controls.Add(Me.lblMontoKpiUtilidad)
            Me.pnlCardUtilidad.Location = New System.Drawing.Point(638, 4)
            Me.pnlCardUtilidad.Name = "pnlCardUtilidad"
            Me.pnlCardUtilidad.Size = New System.Drawing.Size(210, 64)
            Me.pnlCardUtilidad.TabIndex = 3

            Me.lblTituloKpiUtilidad.AutoSize = True
            Me.lblTituloKpiUtilidad.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloKpiUtilidad.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61)
            Me.lblTituloKpiUtilidad.Location = New System.Drawing.Point(8, 7)
            Me.lblTituloKpiUtilidad.Name = "lblTituloKpiUtilidad"
            Me.lblTituloKpiUtilidad.Size = New System.Drawing.Size(134, 13)
            Me.lblTituloKpiUtilidad.Text = "UTILIDAD OPERATIVA"

            Me.lblMontoKpiUtilidad.AutoSize = True
            Me.lblMontoKpiUtilidad.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoKpiUtilidad.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
            Me.lblMontoKpiUtilidad.Location = New System.Drawing.Point(8, 28)
            Me.lblMontoKpiUtilidad.Name = "lblMontoKpiUtilidad"
            Me.lblMontoKpiUtilidad.Size = New System.Drawing.Size(57, 21)
            Me.lblMontoKpiUtilidad.Text = "$ 0.00"

            ' pnlCardItbms
            Me.pnlCardItbms.BackColor = System.Drawing.Color.FromArgb(254, 252, 232)
            Me.pnlCardItbms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCardItbms.Controls.Add(Me.lblTituloKpiItbms)
            Me.pnlCardItbms.Controls.Add(Me.lblMontoKpiItbms)
            Me.pnlCardItbms.Location = New System.Drawing.Point(856, 4)
            Me.pnlCardItbms.Name = "pnlCardItbms"
            Me.pnlCardItbms.Size = New System.Drawing.Size(210, 64)
            Me.pnlCardItbms.TabIndex = 4

            Me.lblTituloKpiItbms.AutoSize = True
            Me.lblTituloKpiItbms.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloKpiItbms.ForeColor = System.Drawing.Color.FromArgb(161, 98, 7)
            Me.lblTituloKpiItbms.Location = New System.Drawing.Point(8, 7)
            Me.lblTituloKpiItbms.Name = "lblTituloKpiItbms"
            Me.lblTituloKpiItbms.Size = New System.Drawing.Size(115, 13)
            Me.lblTituloKpiItbms.Text = "ITBMS DGI (7%)"

            Me.lblMontoKpiItbms.AutoSize = True
            Me.lblMontoKpiItbms.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoKpiItbms.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59)
            Me.lblMontoKpiItbms.Location = New System.Drawing.Point(8, 28)
            Me.lblMontoKpiItbms.Name = "lblMontoKpiItbms"
            Me.lblMontoKpiItbms.Size = New System.Drawing.Size(57, 21)
            Me.lblMontoKpiItbms.Text = "$ 0.00"

            '
            ' pnlSelectorReporte
            '
            Me.pnlSelectorReporte.Controls.Add(Me.lblResumenFinanciero)
            Me.pnlSelectorReporte.Controls.Add(Me.rdoBalanzaComprobacion)
            Me.pnlSelectorReporte.Controls.Add(Me.rdoBalanceGeneral)
            Me.pnlSelectorReporte.Controls.Add(Me.rdoEstadoResultados)
            Me.pnlSelectorReporte.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlSelectorReporte.Location = New System.Drawing.Point(6, 80)
            Me.pnlSelectorReporte.Name = "pnlSelectorReporte"
            Me.pnlSelectorReporte.Size = New System.Drawing.Size(1080, 44)
            Me.pnlSelectorReporte.TabIndex = 1

            Me.rdoEstadoResultados.AutoSize = True
            Me.rdoEstadoResultados.Checked = True
            Me.rdoEstadoResultados.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.rdoEstadoResultados.Location = New System.Drawing.Point(8, 12)
            Me.rdoEstadoResultados.Name = "rdoEstadoResultados"
            Me.rdoEstadoResultados.Size = New System.Drawing.Size(165, 19)
            Me.rdoEstadoResultados.TabIndex = 0
            Me.rdoEstadoResultados.TabStop = True
            Me.rdoEstadoResultados.Text = "Ingresos vs Gastos (P&L)"
            Me.rdoEstadoResultados.UseVisualStyleBackColor = True

            Me.rdoBalanceGeneral.AutoSize = True
            Me.rdoBalanceGeneral.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.rdoBalanceGeneral.Location = New System.Drawing.Point(185, 12)
            Me.rdoBalanceGeneral.Name = "rdoBalanceGeneral"
            Me.rdoBalanceGeneral.Size = New System.Drawing.Size(168, 19)
            Me.rdoBalanceGeneral.TabIndex = 1
            Me.rdoBalanceGeneral.Text = "Activos, Pasivos y Capital"
            Me.rdoBalanceGeneral.UseVisualStyleBackColor = True

            Me.rdoBalanzaComprobacion.AutoSize = True
            Me.rdoBalanzaComprobacion.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.rdoBalanzaComprobacion.Location = New System.Drawing.Point(365, 12)
            Me.rdoBalanzaComprobacion.Name = "rdoBalanzaComprobacion"
            Me.rdoBalanzaComprobacion.Size = New System.Drawing.Size(165, 19)
            Me.rdoBalanzaComprobacion.TabIndex = 2
            Me.rdoBalanzaComprobacion.Text = "Balanza de Comprobación"
            Me.rdoBalanzaComprobacion.UseVisualStyleBackColor = True

            Me.lblResumenFinanciero.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblResumenFinanciero.AutoSize = True
            Me.lblResumenFinanciero.Font = New System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic)
            Me.lblResumenFinanciero.ForeColor = System.Drawing.Color.Gray
            Me.lblResumenFinanciero.Location = New System.Drawing.Point(740, 14)
            Me.lblResumenFinanciero.Name = "lblResumenFinanciero"
            Me.lblResumenFinanciero.Size = New System.Drawing.Size(325, 15)
            Me.lblResumenFinanciero.TabIndex = 3
            Me.lblResumenFinanciero.Text = "Moneda Oficial: Balboa / USD (PAB) | Normativa DGI Panamá"

            '
            ' dgvEstadosFinancieros
            '
            Me.dgvEstadosFinancieros.AllowUserToAddRows = False
            Me.dgvEstadosFinancieros.AllowUserToDeleteRows = False
            Me.dgvEstadosFinancieros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvEstadosFinancieros.BackgroundColor = System.Drawing.Color.White
            Me.dgvEstadosFinancieros.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvEstadosFinancieros.Location = New System.Drawing.Point(6, 124)
            Me.dgvEstadosFinancieros.MultiSelect = False
            Me.dgvEstadosFinancieros.Name = "dgvEstadosFinancieros"
            Me.dgvEstadosFinancieros.ReadOnly = True
            Me.dgvEstadosFinancieros.RowHeadersVisible = False
            Me.dgvEstadosFinancieros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvEstadosFinancieros.Size = New System.Drawing.Size(1080, 439)
            Me.dgvEstadosFinancieros.TabIndex = 2

            '
            ' TAB 4: CATÁLOGO DE CUENTAS
            '
            Me.tabCatalogoCuentas.Controls.Add(Me.dgvCatalogo)
            Me.tabCatalogoCuentas.Controls.Add(Me.pnlFiltroCatalogo)
            Me.tabCatalogoCuentas.Location = New System.Drawing.Point(4, 32)
            Me.tabCatalogoCuentas.Name = "tabCatalogoCuentas"
            Me.tabCatalogoCuentas.Padding = New System.Windows.Forms.Padding(6)
            Me.tabCatalogoCuentas.Size = New System.Drawing.Size(1092, 569)
            Me.tabCatalogoCuentas.Text = "🗂️ Catálogo de Cuentas"
            Me.tabCatalogoCuentas.UseVisualStyleBackColor = True

            Me.pnlFiltroCatalogo.Controls.Add(Me.lblBuscarCuenta)
            Me.pnlFiltroCatalogo.Controls.Add(Me.txtBuscarCuenta)
            Me.pnlFiltroCatalogo.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlFiltroCatalogo.Location = New System.Drawing.Point(6, 6)
            Me.pnlFiltroCatalogo.Name = "pnlFiltroCatalogo"
            Me.pnlFiltroCatalogo.Size = New System.Drawing.Size(1080, 48)
            Me.pnlFiltroCatalogo.TabIndex = 0

            Me.lblBuscarCuenta.AutoSize = True
            Me.lblBuscarCuenta.Location = New System.Drawing.Point(8, 16)
            Me.lblBuscarCuenta.Name = "lblBuscarCuenta"
            Me.lblBuscarCuenta.Size = New System.Drawing.Size(148, 15)
            Me.lblBuscarCuenta.TabIndex = 0
            Me.lblBuscarCuenta.Text = "Buscar por Código/Nombre:"

            Me.txtBuscarCuenta.Location = New System.Drawing.Point(162, 12)
            Me.txtBuscarCuenta.Name = "txtBuscarCuenta"
            Me.txtBuscarCuenta.Size = New System.Drawing.Size(320, 23)
            Me.txtBuscarCuenta.TabIndex = 1

            Me.dgvCatalogo.AllowUserToAddRows = False
            Me.dgvCatalogo.AllowUserToDeleteRows = False
            Me.dgvCatalogo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvCatalogo.BackgroundColor = System.Drawing.Color.White
            Me.dgvCatalogo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvCatalogo.Location = New System.Drawing.Point(6, 54)
            Me.dgvCatalogo.MultiSelect = False
            Me.dgvCatalogo.Name = "dgvCatalogo"
            Me.dgvCatalogo.ReadOnly = True
            Me.dgvCatalogo.RowHeadersVisible = False
            Me.dgvCatalogo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvCatalogo.Size = New System.Drawing.Size(1080, 509)
            Me.dgvCatalogo.TabIndex = 1

            '
            ' TAB 5: PLANILLA Y COLABORADORES
            '
            Me.tabPlanilla.Controls.Add(Me.splitPlanilla)
            Me.tabPlanilla.Location = New System.Drawing.Point(4, 32)
            Me.tabPlanilla.Name = "tabPlanilla"
            Me.tabPlanilla.Padding = New System.Windows.Forms.Padding(6)
            Me.tabPlanilla.Size = New System.Drawing.Size(1092, 569)
            Me.tabPlanilla.Text = "👥 Planilla & Nómina (7 Colaboradores)"
            Me.tabPlanilla.UseVisualStyleBackColor = True

            Me.splitPlanilla.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitPlanilla.Location = New System.Drawing.Point(6, 6)
            Me.splitPlanilla.Name = "splitPlanilla"
            Me.splitPlanilla.Panel1.Controls.Add(Me.dgvEmpleados)
            Me.splitPlanilla.Panel2.Controls.Add(Me.pnlResumenPlanilla)
            Me.splitPlanilla.Size = New System.Drawing.Size(1080, 557)
            Me.splitPlanilla.SplitterDistance = 650
            Me.splitPlanilla.TabIndex = 0

            Me.dgvEmpleados.AllowUserToAddRows = False
            Me.dgvEmpleados.AllowUserToDeleteRows = False
            Me.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvEmpleados.BackgroundColor = System.Drawing.Color.White
            Me.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvEmpleados.Location = New System.Drawing.Point(0, 0)
            Me.dgvEmpleados.MultiSelect = False
            Me.dgvEmpleados.Name = "dgvEmpleados"
            Me.dgvEmpleados.ReadOnly = True
            Me.dgvEmpleados.RowHeadersVisible = False
            Me.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvEmpleados.Size = New System.Drawing.Size(650, 557)
            Me.dgvEmpleados.TabIndex = 0

            Me.pnlResumenPlanilla.BackColor = System.Drawing.Color.FromArgb(248, 246, 242)
            Me.pnlResumenPlanilla.Controls.Add(Me.btnRegistrarPlanillaBD)
            Me.pnlResumenPlanilla.Controls.Add(Me.lblDetalleCostoTotalEmpresa)
            Me.pnlResumenPlanilla.Controls.Add(Me.lblDetalleCargasPatronales)
            Me.pnlResumenPlanilla.Controls.Add(Me.lblDetalleSalarioNetoACH)
            Me.pnlResumenPlanilla.Controls.Add(Me.lblDetalleRetencionObrera)
            Me.pnlResumenPlanilla.Controls.Add(Me.lblDetalleSalariosBrutos)
            Me.pnlResumenPlanilla.Controls.Add(Me.lblTituloResumenPlanilla)
            Me.pnlResumenPlanilla.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlResumenPlanilla.Location = New System.Drawing.Point(0, 0)
            Me.pnlResumenPlanilla.Name = "pnlResumenPlanilla"
            Me.pnlResumenPlanilla.Padding = New System.Windows.Forms.Padding(16)
            Me.pnlResumenPlanilla.Size = New System.Drawing.Size(426, 557)
            Me.pnlResumenPlanilla.TabIndex = 0

            Me.lblTituloResumenPlanilla.AutoSize = True
            Me.lblTituloResumenPlanilla.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloResumenPlanilla.Location = New System.Drawing.Point(16, 16)
            Me.lblTituloResumenPlanilla.Name = "lblTituloResumenPlanilla"
            Me.lblTituloResumenPlanilla.Size = New System.Drawing.Size(325, 21)
            Me.lblTituloResumenPlanilla.TabIndex = 0
            Me.lblTituloResumenPlanilla.Text = "💼 Resumen de Planilla (Leyes de Panamá)"

            Me.lblDetalleSalariosBrutos.AutoSize = True
            Me.lblDetalleSalariosBrutos.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblDetalleSalariosBrutos.Location = New System.Drawing.Point(16, 60)
            Me.lblDetalleSalariosBrutos.Name = "lblDetalleSalariosBrutos"
            Me.lblDetalleSalariosBrutos.Size = New System.Drawing.Size(260, 19)
            Me.lblDetalleSalariosBrutos.TabIndex = 1
            Me.lblDetalleSalariosBrutos.Text = "Total Salarios Brutos (7 Empleados): $ 0.00"

            Me.lblDetalleRetencionObrera.AutoSize = True
            Me.lblDetalleRetencionObrera.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblDetalleRetencionObrera.ForeColor = System.Drawing.Color.FromArgb(185, 56, 41)
            Me.lblDetalleRetencionObrera.Location = New System.Drawing.Point(16, 100)
            Me.lblDetalleRetencionObrera.Name = "lblDetalleRetencionObrera"
            Me.lblDetalleRetencionObrera.Size = New System.Drawing.Size(280, 19)
            Me.lblDetalleRetencionObrera.TabIndex = 2
            Me.lblDetalleRetencionObrera.Text = "(-) Retención Obrera (CSS 9.75% + SE 1.25%): $ 0.00"

            Me.lblDetalleSalarioNetoACH.AutoSize = True
            Me.lblDetalleSalarioNetoACH.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetalleSalarioNetoACH.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblDetalleSalarioNetoACH.Location = New System.Drawing.Point(16, 140)
            Me.lblDetalleSalarioNetoACH.Name = "lblDetalleSalarioNetoACH"
            Me.lblDetalleSalarioNetoACH.Size = New System.Drawing.Size(265, 20)
            Me.lblDetalleSalarioNetoACH.TabIndex = 3
            Me.lblDetalleSalarioNetoACH.Text = "(=) Total Neto a Pagar por ACH: $ 0.00"

            Me.lblDetalleCargasPatronales.AutoSize = True
            Me.lblDetalleCargasPatronales.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblDetalleCargasPatronales.Location = New System.Drawing.Point(16, 190)
            Me.lblDetalleCargasPatronales.Name = "lblDetalleCargasPatronales"
            Me.lblDetalleCargasPatronales.Size = New System.Drawing.Size(290, 19)
            Me.lblDetalleCargasPatronales.TabIndex = 4
            Me.lblDetalleCargasPatronales.Text = "(+) Cargas Sociales Patronales (15.00% CSS/SE): $ 0.00"

            Me.lblDetalleCostoTotalEmpresa.AutoSize = True
            Me.lblDetalleCostoTotalEmpresa.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetalleCostoTotalEmpresa.ForeColor = System.Drawing.Color.FromArgb(117, 70, 50)
            Me.lblDetalleCostoTotalEmpresa.Location = New System.Drawing.Point(16, 230)
            Me.lblDetalleCostoTotalEmpresa.Name = "lblDetalleCostoTotalEmpresa"
            Me.lblDetalleCostoTotalEmpresa.Size = New System.Drawing.Size(300, 20)
            Me.lblDetalleCostoTotalEmpresa.TabIndex = 5
            Me.lblDetalleCostoTotalEmpresa.Text = "Total Gasto Planilla Asiento Cuadrado: $ 0.00"

            Me.btnRegistrarPlanillaBD.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.btnRegistrarPlanillaBD.Location = New System.Drawing.Point(16, 280)
            Me.btnRegistrarPlanillaBD.Name = "btnRegistrarPlanillaBD"
            Me.btnRegistrarPlanillaBD.Size = New System.Drawing.Size(350, 45)
            Me.btnRegistrarPlanillaBD.TabIndex = 6
            Me.btnRegistrarPlanillaBD.Text = "🏦 Registrar Asiento de Nómina en BD"
            Me.btnRegistrarPlanillaBD.UseVisualStyleBackColor = True

            '
            ' TAB 6: PERÍODOS Y CIERRE
            '
            Me.tabPeriodosCierre.Controls.Add(Me.splitPeriodos)
            Me.tabPeriodosCierre.Location = New System.Drawing.Point(4, 32)
            Me.tabPeriodosCierre.Name = "tabPeriodosCierre"
            Me.tabPeriodosCierre.Padding = New System.Windows.Forms.Padding(6)
            Me.tabPeriodosCierre.Size = New System.Drawing.Size(1092, 569)
            Me.tabPeriodosCierre.Text = "🔒 Períodos & Cierre Mensual"
            Me.tabPeriodosCierre.UseVisualStyleBackColor = True

            Me.splitPeriodos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitPeriodos.Location = New System.Drawing.Point(6, 6)
            Me.splitPeriodos.Name = "splitPeriodos"
            Me.splitPeriodos.Panel1.Controls.Add(Me.grpPeriodos)
            Me.splitPeriodos.Panel2.Controls.Add(Me.grpMovimientosCaja)
            Me.splitPeriodos.Size = New System.Drawing.Size(1080, 557)
            Me.splitPeriodos.SplitterDistance = 500
            Me.splitPeriodos.TabIndex = 0

            Me.grpPeriodos.Controls.Add(Me.dgvPeriodos)
            Me.grpPeriodos.Controls.Add(Me.btnCerrarPeriodo)
            Me.grpPeriodos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpPeriodos.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpPeriodos.Location = New System.Drawing.Point(0, 0)
            Me.grpPeriodos.Name = "grpPeriodos"
            Me.grpPeriodos.Padding = New System.Windows.Forms.Padding(8)
            Me.grpPeriodos.Size = New System.Drawing.Size(500, 557)
            Me.grpPeriodos.TabIndex = 0
            Me.grpPeriodos.TabStop = False
            Me.grpPeriodos.Text = "Períodos Contables & Bloqueo Mensual"

            Me.btnCerrarPeriodo.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.btnCerrarPeriodo.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnCerrarPeriodo.Location = New System.Drawing.Point(8, 509)
            Me.btnCerrarPeriodo.Name = "btnCerrarPeriodo"
            Me.btnCerrarPeriodo.Size = New System.Drawing.Size(484, 40)
            Me.btnCerrarPeriodo.TabIndex = 1
            Me.btnCerrarPeriodo.Text = "🔒 Ejecutar Cierre Mensual del Período Seleccionado"
            Me.btnCerrarPeriodo.UseVisualStyleBackColor = True

            Me.dgvPeriodos.AllowUserToAddRows = False
            Me.dgvPeriodos.AllowUserToDeleteRows = False
            Me.dgvPeriodos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvPeriodos.BackgroundColor = System.Drawing.Color.White
            Me.dgvPeriodos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvPeriodos.Location = New System.Drawing.Point(8, 25)
            Me.dgvPeriodos.MultiSelect = False
            Me.dgvPeriodos.Name = "dgvPeriodos"
            Me.dgvPeriodos.ReadOnly = True
            Me.dgvPeriodos.RowHeadersVisible = False
            Me.dgvPeriodos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvPeriodos.Size = New System.Drawing.Size(484, 484)
            Me.dgvPeriodos.TabIndex = 0

            Me.grpMovimientosCaja.Controls.Add(Me.dgvMovimientosCaja)
            Me.grpMovimientosCaja.Controls.Add(Me.btnRegistrarAperturaGasto)
            Me.grpMovimientosCaja.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grpMovimientosCaja.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.grpMovimientosCaja.Location = New System.Drawing.Point(0, 0)
            Me.grpMovimientosCaja.Name = "grpMovimientosCaja"
            Me.grpMovimientosCaja.Padding = New System.Windows.Forms.Padding(8)
            Me.grpMovimientosCaja.Size = New System.Drawing.Size(576, 557)
            Me.grpMovimientosCaja.TabIndex = 0
            Me.grpMovimientosCaja.TabStop = False
            Me.grpMovimientosCaja.Text = "Movimientos y Arqueos de Caja (movimientos_caja)"

            Me.btnRegistrarAperturaGasto.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.btnRegistrarAperturaGasto.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.btnRegistrarAperturaGasto.Location = New System.Drawing.Point(8, 509)
            Me.btnRegistrarAperturaGasto.Name = "btnRegistrarAperturaGasto"
            Me.btnRegistrarAperturaGasto.Size = New System.Drawing.Size(560, 40)
            Me.btnRegistrarAperturaGasto.TabIndex = 1
            Me.btnRegistrarAperturaGasto.Text = "💵 Registrar Gasto Menor / Ajuste de Caja en BD"
            Me.btnRegistrarAperturaGasto.UseVisualStyleBackColor = True

            Me.dgvMovimientosCaja.AllowUserToAddRows = False
            Me.dgvMovimientosCaja.AllowUserToDeleteRows = False
            Me.dgvMovimientosCaja.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvMovimientosCaja.BackgroundColor = System.Drawing.Color.White
            Me.dgvMovimientosCaja.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvMovimientosCaja.Location = New System.Drawing.Point(8, 25)
            Me.dgvMovimientosCaja.MultiSelect = False
            Me.dgvMovimientosCaja.Name = "dgvMovimientosCaja"
            Me.dgvMovimientosCaja.ReadOnly = True
            Me.dgvMovimientosCaja.RowHeadersVisible = False
            Me.dgvMovimientosCaja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvMovimientosCaja.Size = New System.Drawing.Size(560, 484)
            Me.dgvMovimientosCaja.TabIndex = 0

            '
            ' TAB 7: AUDITORÍA
            '
            Me.tabAuditoria.Controls.Add(Me.dgvAuditoria)
            Me.tabAuditoria.Location = New System.Drawing.Point(4, 32)
            Me.tabAuditoria.Name = "tabAuditoria"
            Me.tabAuditoria.Padding = New System.Windows.Forms.Padding(6)
            Me.tabAuditoria.Size = New System.Drawing.Size(1092, 569)
            Me.tabAuditoria.Text = "🛡️ Auditoría Contable"
            Me.tabAuditoria.UseVisualStyleBackColor = True

            Me.dgvAuditoria.AllowUserToAddRows = False
            Me.dgvAuditoria.AllowUserToDeleteRows = False
            Me.dgvAuditoria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvAuditoria.BackgroundColor = System.Drawing.Color.White
            Me.dgvAuditoria.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgvAuditoria.Location = New System.Drawing.Point(6, 6)
            Me.dgvAuditoria.MultiSelect = False
            Me.dgvAuditoria.Name = "dgvAuditoria"
            Me.dgvAuditoria.ReadOnly = True
            Me.dgvAuditoria.RowHeadersVisible = False
            Me.dgvAuditoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvAuditoria.Size = New System.Drawing.Size(1080, 557)
            Me.dgvAuditoria.TabIndex = 0

            '
            ' Form Base
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1100, 680)
            Me.Controls.Add(Me.tabContabilidad)
            Me.Controls.Add(Me.pnlHeaderContainer)
            Me.Name = "FrmModuloContable"
            Me.Text = "Módulo de Contabilidad Oficial & DGI Panamá"

            Me.pnlHeaderContainer.ResumeLayout(False)
            Me.pnlHeaderContainer.PerformLayout()
            Me.tabContabilidad.ResumeLayout(False)
            Me.tabLibroDiario.ResumeLayout(False)
            Me.tabLibroMayor.ResumeLayout(False)
            Me.tabEstadosFinancieros.ResumeLayout(False)
            Me.tabCatalogoCuentas.ResumeLayout(False)
            Me.tabPlanilla.ResumeLayout(False)
            Me.tabPeriodosCierre.ResumeLayout(False)
            Me.tabAuditoria.ResumeLayout(False)
            Me.pnlFiltrosDiario.ResumeLayout(False)
            Me.pnlFiltrosDiario.PerformLayout()
            Me.splitDiario.Panel1.ResumeLayout(False)
            Me.splitDiario.Panel2.ResumeLayout(False)
            CType(Me.splitDiario, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitDiario.ResumeLayout(False)
            CType(Me.dgvLibroDiario, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dgvDetalleAsiento, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlBadgePartidaDoble.ResumeLayout(False)
            Me.pnlBadgePartidaDoble.PerformLayout()
            Me.pnlFiltrosMayor.ResumeLayout(False)
            Me.pnlFiltrosMayor.PerformLayout()
            CType(Me.dgvLibroMayor, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlMayorTotales.ResumeLayout(False)
            Me.pnlMayorTotales.PerformLayout()
            Me.pnlSelectorReporte.ResumeLayout(False)
            Me.pnlSelectorReporte.PerformLayout()
            CType(Me.dgvEstadosFinancieros, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlFiltroCatalogo.ResumeLayout(False)
            Me.pnlFiltroCatalogo.PerformLayout()
            CType(Me.dgvCatalogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitPlanilla.Panel1.ResumeLayout(False)
            Me.splitPlanilla.Panel2.ResumeLayout(False)
            CType(Me.splitPlanilla, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitPlanilla.ResumeLayout(False)
            CType(Me.dgvEmpleados, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlResumenPlanilla.ResumeLayout(False)
            Me.pnlResumenPlanilla.PerformLayout()
            Me.splitPeriodos.Panel1.ResumeLayout(False)
            Me.splitPeriodos.Panel2.ResumeLayout(False)
            CType(Me.splitPeriodos, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitPeriodos.ResumeLayout(False)
            Me.grpPeriodos.ResumeLayout(False)
            CType(Me.dgvPeriodos, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpMovimientosCaja.ResumeLayout(False)
            CType(Me.dgvMovimientosCaja, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.dgvAuditoria, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

    End Class
End Namespace

