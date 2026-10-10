Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Contabilidad
    ''' <summary>
    ''' Formulario de Control Contable y Cumplimiento Fiscal DGI Panamá (NIIF para PYMES).
    ''' Exclusivo para el rol Administrador.
    ''' Gestiona Libro Diario, Partida Doble, Libro Mayor, Balanza de Comprobación,
    ''' Estados Financieros (P&L y Balance General), Planilla y Cierre de Período.
    ''' </summary>
    Public Class FrmModuloContable

        Private _listaAsientos As New List(Of AsientoContableModel)()
        Private _listaCuentas As New List(Of CuentaContableModel)()
        Private _listaEmpleados As New List(Of EmpleadoModel)()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub FrmModuloContable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()

            dtpFechaDesde.Value = New DateTime(2026, 1, 1)
            dtpFechaHasta.Value = New DateTime(2026, 12, 31)

            CargarDatosCompletos()
        End Sub

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()

            pnlHeaderContainer.BackColor = ThemeConfig.ColorBackgroundApp
            lblTitulo.ForeColor = ThemeConfig.ColorNeutralDark
            lblSubtitulo.ForeColor = ThemeConfig.ColorTextMuted

            ThemeConfig.EstilizarBotonPrimario(btnExportarInformeCPA)
            ThemeConfig.EstilizarBotonSecundario(btnActualizarTodo)
            ThemeConfig.EstilizarBotonSecundario(btnFiltrarDiario)
            ThemeConfig.EstilizarBotonSecundario(btnFiltrarMayor)
            ThemeConfig.EstilizarBotonPrimario(btnRegistrarPlanillaBD)
            ThemeConfig.EstilizarBotonEliminar(btnCerrarPeriodo)
            ThemeConfig.EstilizarBotonSecundario(btnRegistrarAperturaGasto)

            ThemeConfig.EstilizarDataGrid(dgvLibroDiario)
            ThemeConfig.EstilizarDataGrid(dgvDetalleAsiento)
            ThemeConfig.EstilizarDataGrid(dgvLibroMayor)
            ThemeConfig.EstilizarDataGrid(dgvEstadosFinancieros)
            ThemeConfig.EstilizarDataGrid(dgvCatalogo)
            ThemeConfig.EstilizarDataGrid(dgvEmpleados)
            ThemeConfig.EstilizarDataGrid(dgvPeriodos)
            ThemeConfig.EstilizarDataGrid(dgvMovimientosCaja)
            ThemeConfig.EstilizarDataGrid(dgvAuditoria)

            pnlBadgePartidaDoble.BackColor = ThemeConfig.ColorTertiaryLight
            lblEstadoPartidaDoble.ForeColor = ThemeConfig.ColorTertiarySuccess
        End Sub

        Public Sub CargarDatosCompletos()
            Try
                Cursor = Cursors.WaitCursor
                CargarCatalogo()
                CargarLibroDiario()
                CargarEstadosFinancieros()
                CargarPlanilla()
                CargarPeriodosYMovimientos()
                CargarAuditoria()
            Catch ex As Exception
                MostrarMensajeError($"Error al consultar datos contables: {ex.Message}")
            Finally
                Cursor = Cursors.Default
            End Try
        End Sub

        Private Sub btnActualizarTodo_Click(sender As Object, e As EventArgs) Handles btnActualizarTodo.Click
            CargarDatosCompletos()
            MostrarMensajeExito("Datos contables y fiscales actualizados correctamente desde la BD.", "Actualización Completa")
        End Sub

        ' =========================================================================
        ' 1. TAB: LIBRO DIARIO Y DETALLE DE PARTIDA DOBLE
        ' =========================================================================

        Private Sub CargarLibroDiario()
            Dim fIni = dtpFechaDesde.Value.Date
            Dim fFin = dtpFechaHasta.Value.Date
            Dim modSel = cboModuloOrigen.SelectedItem?.ToString()

            _listaAsientos = ContabilidadDAO.ObtenerLibroDiario(fIni, fFin)

            If Not String.IsNullOrWhiteSpace(modSel) AndAlso Not modSel.Equals("TODOS", StringComparison.OrdinalIgnoreCase) Then
                _listaAsientos = _listaAsientos.Where(Function(a) a.OrigenModulo.Equals(modSel, StringComparison.OrdinalIgnoreCase)).ToList()
            End If

            Dim dt As New DataTable()
            dt.Columns.Add("Nro Asiento", GetType(String))
            dt.Columns.Add("Fecha", GetType(String))
            dt.Columns.Add("Módulo", GetType(String))
            dt.Columns.Add("Concepto", GetType(String))
            dt.Columns.Add("Total Debe ($)", GetType(Decimal))
            dt.Columns.Add("Total Haber ($)", GetType(Decimal))
            dt.Columns.Add("Cuadrado", GetType(String))
            dt.Columns.Add("Estado", GetType(String))

            For Each a In _listaAsientos
                Dim cuadrado As String = If(Math.Abs(a.TotalDebe - a.TotalHaber) < 0.001D, "✅ SÍ", "❌ NO")
                dt.Rows.Add(a.NumeroAsiento, a.FechaAsiento.ToString("yyyy-MM-dd"), a.OrigenModulo, a.Concepto, a.TotalDebe, a.TotalHaber, cuadrado, a.Estado)
            Next

            dgvLibroDiario.DataSource = dt
            If dgvLibroDiario.Rows.Count > 0 Then
                dgvLibroDiario.Rows(0).Selected = True
                MostrarDetalleAsiento(0)
            Else
                dgvDetalleAsiento.DataSource = Nothing
                lblEstadoPartidaDoble.Text = "ℹ️ No hay asientos registrados en el rango de fechas seleccionado."
            End If
        End Sub

        Private Sub btnFiltrarDiario_Click(sender As Object, e As EventArgs) Handles btnFiltrarDiario.Click
            CargarLibroDiario()
        End Sub

        Private Sub dgvLibroDiario_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLibroDiario.SelectionChanged
            If dgvLibroDiario.SelectedRows.Count > 0 Then
                Dim idx = dgvLibroDiario.SelectedRows(0).Index
                MostrarDetalleAsiento(idx)
            End If
        End Sub

        Private Sub MostrarDetalleAsiento(idx As Integer)
            If idx < 0 OrElse idx >= _listaAsientos.Count Then Return
            Dim asiento = _listaAsientos(idx)

            Dim dtD As New DataTable()
            dtD.Columns.Add("Cuenta Contable", GetType(String))
            dtD.Columns.Add("Descripción de la Operación", GetType(String))
            dtD.Columns.Add("Entrada / Debe ($)", GetType(Decimal))
            dtD.Columns.Add("Salida / Haber ($)", GetType(Decimal))

            For Each l In asiento.Lineas
                Dim nomCuenta = If(Not String.IsNullOrWhiteSpace(l.NombreCuenta), l.NombreCuenta, l.CodigoCuenta)
                dtD.Rows.Add(nomCuenta, l.DescripcionLinea, l.Debe, l.Haber)
            Next

            dgvDetalleAsiento.DataSource = dtD

            Dim balanceado = asiento.EstaCuadrado
            If balanceado Then
                lblEstadoPartidaDoble.Text = $"⚖️ Partida Doble Verificada: Debe ${asiento.TotalDebe:N2} = Haber ${asiento.TotalHaber:N2} (100% Cuadrado en USD/Balboas)"
                lblEstadoPartidaDoble.ForeColor = ThemeConfig.ColorTertiarySuccess
                pnlBadgePartidaDoble.BackColor = ThemeConfig.ColorTertiaryLight
            Else
                lblEstadoPartidaDoble.Text = $"⚠️ Descuadre Contable: Debe ${asiento.TotalDebe:N2} <> Haber ${asiento.TotalHaber:N2} (Diferencia: ${Math.Abs(asiento.TotalDebe - asiento.TotalHaber):N2})"
                lblEstadoPartidaDoble.ForeColor = ThemeConfig.ColorDanger
                pnlBadgePartidaDoble.BackColor = ThemeConfig.ColorDangerLight
            End If
        End Sub

        ' =========================================================================
        ' 2. TAB: LIBRO MAYOR
        ' =========================================================================

        Private Sub CargarLibroMayor()
            Dim ctaSeleccionada As String = Nothing
            If cboCuentaMayor.SelectedItem IsNot Nothing Then
                Dim sel = cboCuentaMayor.SelectedItem.ToString()
                If Not sel.StartsWith("TODAS", StringComparison.OrdinalIgnoreCase) Then
                    Dim partes = sel.Split("-"c)
                    If partes.Length > 0 Then ctaSeleccionada = partes(0).Trim()
                End If
            End If

            Dim dtMayor = ContabilidadDAO.ObtenerLibroMayor(ctaSeleccionada, dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date)
            dgvLibroMayor.DataSource = dtMayor

            Dim totalDebe As Decimal = 0D
            Dim totalHaber As Decimal = 0D
            Dim saldoFinal As Decimal = 0D

            If dtMayor IsNot Nothing AndAlso dtMayor.Rows.Count > 0 Then
                For Each r As DataRow In dtMayor.Rows
                    totalDebe += Convert.ToDecimal(r("Debe"))
                    totalHaber += Convert.ToDecimal(r("Haber"))
                Next
                saldoFinal = Convert.ToDecimal(dtMayor.Rows(dtMayor.Rows.Count - 1)("Saldo"))
            End If

            lblTotalDebeMayor.Text = $"Total Débitos: $ {totalDebe:N2}"
            lblTotalHaberMayor.Text = $"Total Créditos: $ {totalHaber:N2}"
            lblSaldoMayor.Text = $"Saldo Neto Cuenta: $ {saldoFinal:N2}"
        End Sub

        Private Sub btnFiltrarMayor_Click(sender As Object, e As EventArgs) Handles btnFiltrarMayor.Click
            CargarLibroMayor()
        End Sub

        ' =========================================================================
        ' 3. TAB: ESTADOS FINANCIEROS (NIIF / CPA)
        ' =========================================================================

        Private Sub CargarEstadosFinancieros()
            Dim fIni = dtpFechaDesde.Value.Date
            Dim fFin = dtpFechaHasta.Value.Date

            If rdoEstadoResultados.Checked Then
                dgvEstadosFinancieros.DataSource = ContabilidadDAO.GenerarEstadoResultados(fIni, fFin)
            ElseIf rdoBalanceGeneral.Checked Then
                dgvEstadosFinancieros.DataSource = ContabilidadDAO.GenerarBalanceGeneral(fFin)
            ElseIf rdoBalanzaComprobacion.Checked Then
                dgvEstadosFinancieros.DataSource = ContabilidadDAO.GenerarBalanzaComprobacion(fIni, fFin)
            End If

            ' Ocultar columnas técnicas con números y códigos contables que enredan al usuario
            If dgvEstadosFinancieros.Columns.Contains("Codigo") Then
                dgvEstadosFinancieros.Columns("Codigo").Visible = False
            End If
            If dgvEstadosFinancieros.Columns.Contains("CodigoCuenta") Then
                dgvEstadosFinancieros.Columns("CodigoCuenta").Visible = False
            End If
            If dgvEstadosFinancieros.Columns.Contains("NombreCuenta") Then
                dgvEstadosFinancieros.Columns("NombreCuenta").Visible = False
            End If

            CalcularKpisEjecutivos(fIni, fFin)
        End Sub

        Private Sub CalcularKpisEjecutivos(fIni As DateTime, fFin As DateTime)
            Dim asientos = ContabilidadDAO.ObtenerLibroDiario(fIni, fFin)

            Dim totVentas As Decimal = 0D
            Dim totDevoluciones As Decimal = 0D
            Dim totGastosOperativos As Decimal = 0D
            Dim totNomina As Decimal = 0D
            Dim totItbmsCredito As Decimal = 0D
            Dim totItbmsDebito As Decimal = 0D

            For Each a In asientos
                For Each l In a.Lineas
                    If l.CodigoCuenta.StartsWith("4.1.01") Then
                        totVentas += l.Haber
                    ElseIf l.CodigoCuenta.StartsWith("4.1.02") Then
                        totDevoluciones += l.Debe
                    ElseIf l.CodigoCuenta.StartsWith("6.1.01") OrElse l.CodigoCuenta.StartsWith("6.1.02") Then
                        totGastosOperativos += l.Debe
                    ElseIf l.CodigoCuenta.StartsWith("6.1.03") Then
                        totNomina += l.Debe
                    ElseIf l.CodigoCuenta.StartsWith("2.1.01.01") Then
                        totItbmsCredito += l.Haber
                        totItbmsDebito += l.Debe
                    End If
                Next
            Next

            Dim ventasNetas = totVentas - totDevoluciones
            Dim utilidad = ventasNetas - (totGastosOperativos + totNomina)
            Dim itbmsPagar = Math.Max(0D, totItbmsCredito - totItbmsDebito)

            lblMontoKpiVentas.Text = $"$ {ventasNetas:N2}"
            lblMontoKpiGastos.Text = $"$ {totGastosOperativos:N2}"
            lblMontoKpiNomina.Text = $"$ {totNomina:N2}"
            lblMontoKpiUtilidad.Text = $"$ {utilidad:N2}"
            lblMontoKpiItbms.Text = $"$ {itbmsPagar:N2}"

            If utilidad >= 0D Then
                lblMontoKpiUtilidad.ForeColor = Color.FromArgb(21, 128, 61)
            Else
                lblMontoKpiUtilidad.ForeColor = Color.FromArgb(185, 28, 28)
            End If
        End Sub

        Private Sub rdoReporte_CheckedChanged(sender As Object, e As EventArgs) Handles rdoEstadoResultados.CheckedChanged, rdoBalanceGeneral.CheckedChanged, rdoBalanzaComprobacion.CheckedChanged
            Dim rdo = TryCast(sender, RadioButton)
            If rdo IsNot Nothing AndAlso rdo.Checked Then
                CargarEstadosFinancieros()
            End If
        End Sub

        Private Sub dtpFechas_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged, dtpFechaHasta.ValueChanged
            ' Actualiza la vista si las fechas globales son modificadas
            CargarEstadosFinancieros()
        End Sub

        ' =========================================================================
        ' 4. TAB: CATÁLOGO DE CUENTAS
        ' =========================================================================

        Private Sub CargarCatalogo()
            _listaCuentas = ContabilidadDAO.ObtenerCatalogoCuentas()

            ' Rellenar ComboBox de Libro Mayor
            cboCuentaMayor.Items.Clear()
            cboCuentaMayor.Items.Add("TODAS LAS CUENTAS CON MOVIMIENTO")
            For Each c In _listaCuentas.Where(Function(x) x.PermiteMovimiento)
                cboCuentaMayor.Items.Add($"{c.CodigoCuenta} - {c.NombreCuenta}")
            Next
            If cboCuentaMayor.Items.Count > 0 Then cboCuentaMayor.SelectedIndex = 0

            FiltrarCatalogo()
        End Sub

        Private Sub txtBuscarCuenta_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCuenta.TextChanged
            FiltrarCatalogo()
        End Sub

        Private Sub FiltrarCatalogo()
            Dim filtro = txtBuscarCuenta.Text.Trim().ToLowerInvariant()
            Dim cuentasFiltradas = _listaCuentas

            If Not String.IsNullOrWhiteSpace(filtro) Then
                cuentasFiltradas = _listaCuentas.Where(Function(c) c.CodigoCuenta.ToLowerInvariant().Contains(filtro) OrElse c.NombreCuenta.ToLowerInvariant().Contains(filtro)).ToList()
            End If

            Dim dtC As New DataTable()
            dtC.Columns.Add("Nombre de la Cuenta", GetType(String))
            dtC.Columns.Add("Tipo", GetType(String))
            dtC.Columns.Add("Naturaleza", GetType(String))
            dtC.Columns.Add("Acepta Movimiento", GetType(String))
            dtC.Columns.Add("Codigo", GetType(String))

            For Each c In cuentasFiltradas
                dtC.Rows.Add(c.NombreCuenta, c.Tipo, c.Naturaleza, If(c.PermiteMovimiento, "Sí", "No (Mayor)"), c.CodigoCuenta)
            Next

            dgvCatalogo.DataSource = dtC
            If dgvCatalogo.Columns.Contains("Codigo") Then
                dgvCatalogo.Columns("Codigo").Visible = False
            End If
        End Sub

        Private Sub tabContabilidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabContabilidad.SelectedIndexChanged
            If tabContabilidad.SelectedTab Is tabLibroDiario Then
                CargarLibroDiario()
            ElseIf tabContabilidad.SelectedTab Is tabEstadosFinancieros Then
                CargarEstadosFinancieros()
            End If
        End Sub

        ' =========================================================================
        ' 5. TAB: PLANILLA Y NÓMINA (7 EMPLEADOS)
        ' =========================================================================

        Private Sub CargarPlanilla()
            _listaEmpleados = ContabilidadDAO.ObtenerEmpleados()

            Dim dtE As New DataTable()
            dtE.Columns.Add("ID", GetType(Integer))
            dtE.Columns.Add("Nombre Colaborador", GetType(String))
            dtE.Columns.Add("Cédula", GetType(String))
            dtE.Columns.Add("Cargo", GetType(String))
            dtE.Columns.Add("Salario Bruto", GetType(Decimal))
            dtE.Columns.Add("CSS Obrero (9.75%)", GetType(Decimal))
            dtE.Columns.Add("SE Obrero (1.25%)", GetType(Decimal))
            dtE.Columns.Add("Neto a Cobrar", GetType(Decimal))

            Dim totalBruto As Decimal = 0D
            Dim totalRetencionObrera As Decimal = 0D
            Dim totalNetoACH As Decimal = 0D

            For Each emp In _listaEmpleados.Where(Function(x) x.Activo)
                Dim cssObrero = Math.Round(emp.SalarioMensual * 0.0975D, 2)
                Dim seObrero = Math.Round(emp.SalarioMensual * 0.0125D, 2)
                Dim retObrera = cssObrero + seObrero
                Dim neto = emp.SalarioMensual - retObrera

                totalBruto += emp.SalarioMensual
                totalRetencionObrera += retObrera
                totalNetoACH += neto

                dtE.Rows.Add(emp.IdEmpleado, emp.NombreCompleto, emp.Cedula, emp.Cargo, emp.SalarioMensual, cssObrero, seObrero, neto)
            Next

            dgvEmpleados.DataSource = dtE

            Dim cargasPatronales = Math.Round(totalBruto * 0.15D, 2)
            Dim costoTotal = totalBruto + cargasPatronales

            lblDetalleSalariosBrutos.Text = $"Total Salarios Brutos ({_listaEmpleados.Count} Colaboradores): $ {totalBruto:N2}"
            lblDetalleRetencionObrera.Text = $"(-) Retención Obrera (CSS 9.75% + SE 1.25% = 11%): $ {totalRetencionObrera:N2}"
            lblDetalleSalarioNetoACH.Text = $"(=) Total Neto ACH Banco General a Pagar: $ {totalNetoACH:N2}"
            lblDetalleCargasPatronales.Text = $"(+) Cargas Sociales Patronales (15.00% CSS/SE): $ {cargasPatronales:N2}"
            lblDetalleCostoTotalEmpresa.Text = $"Total Gasto Planilla Asiento Cuadrado: $ {costoTotal:N2}"
        End Sub

        Private Sub btnRegistrarPlanillaBD_Click(sender As Object, e As EventArgs) Handles btnRegistrarPlanillaBD.Click
            If Not ConfirmarAccion("¿Desea registrar el asiento contable oficial de nómina para el mes actual en PostgreSQL?", "Confirmar Pago de Planilla") Then
                Return
            End If

            Try
                Dim idAsiento = ContabilidadDAO.GenerarAsientoNomina(1)
                If idAsiento > 0 Then
                    MostrarMensajeExito($"Asiento de Nómina registrado exitosamente en la BD con ID #{idAsiento}. Debe = Haber perfectamente balanceado.", "Planilla Procesada")
                    CargarDatosCompletos()
                Else
                    MostrarMensajeAdvertencia("El asiento de nómina para este mes ya se encontraba registrado en la base de datos (Garantía de idempotencia).", "Registro Existente")
                End If
            Catch ex As Exception
                MostrarMensajeError($"Error al registrar nómina: {ex.Message}")
            End Try
        End Sub

        ' =========================================================================
        ' 6. TAB: PERÍODOS CONTABLES Y MOVIMIENTOS DE CAJA
        ' =========================================================================

        Private Sub CargarPeriodosYMovimientos()
            Dim listaPer = ContabilidadDAO.ObtenerPeriodosContables()
            Dim dtP As New DataTable()
            dtP.Columns.Add("ID", GetType(Integer))
            dtP.Columns.Add("Año", GetType(Integer))
            dtP.Columns.Add("Mes", GetType(Integer))
            dtP.Columns.Add("Inicio", GetType(String))
            dtP.Columns.Add("Fin", GetType(String))
            dtP.Columns.Add("Estado", GetType(String))

            For Each p In listaPer
                dtP.Rows.Add(p.IdPeriodo, p.Anio, p.Mes, p.FechaInicio.ToString("yyyy-MM-dd"), p.FechaFin.ToString("yyyy-MM-dd"), p.Estado)
            Next
            dgvPeriodos.DataSource = dtP

            ' Movimientos de caja
            dgvMovimientosCaja.DataSource = ContabilidadDAO.ObtenerMovimientosCaja(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date)
        End Sub

        Private Sub btnCerrarPeriodo_Click(sender As Object, e As EventArgs) Handles btnCerrarPeriodo.Click
            If dgvPeriodos.SelectedRows.Count = 0 Then
                MostrarMensajeAdvertencia("Seleccione un período contable de la lista para ejecutar el cierre mensual.", "Seleccionar Período")
                Return
            End If

            Dim fila = dgvPeriodos.SelectedRows(0)
            Dim idPer = Convert.ToInt32(fila.Cells("ID").Value)
            Dim estActual = fila.Cells("Estado").Value.ToString()

            If estActual.Equals("CERRADO", StringComparison.OrdinalIgnoreCase) Then
                MostrarMensajeAdvertencia("El período seleccionado ya se encuentra formalmente CERRADO.", "Período Cerrado")
                Return
            End If

            If Not ConfirmarAccion($"¿Está seguro de cerrar el período contable #{idPer}? Una vez cerrado, no se permitirán nuevos asientos ni modificaciones en ese rango de fechas.", "Confirmar Cierre Mensual") Then
                Return
            End If

            Dim ok = ContabilidadDAO.CerrarPeriodoMensual(idPer, 1)
            If ok Then
                MostrarMensajeExito($"Período #{idPer} cerrado correctamente. Registro de auditoría guardado.", "Cierre Exitoso")
                CargarPeriodosYMovimientos()
                CargarAuditoria()
            Else
                MostrarMensajeError("No fue posible cerrar el período. Verifique la conexión con el servidor.")
            End If
        End Sub

        Private Sub btnRegistrarAperturaGasto_Click(sender As Object, e As EventArgs) Handles btnRegistrarAperturaGasto.Click
            Dim motivoInput = InputBox("Ingrese la descripción del gasto menor de caja chica o ajuste:", "Registro de Movimiento de Caja", "Compra de insumos menores y limpieza")
            If String.IsNullOrWhiteSpace(motivoInput) Then Return

            Dim montoStr = InputBox("Ingrese el monto en USD/Balboas:", "Monto del Movimiento", "25.00")
            Dim montoVal As Decimal = 0D
            If Not Decimal.TryParse(montoStr, montoVal) OrElse montoVal <= 0D Then
                MostrarMensajeAdvertencia("Monto inválido. Ingrese una cantidad mayor a cero.", "Monto Inválido")
                Return
            End If

            Try
                Dim idMov = ContabilidadDAO.RegistrarMovimientoCaja(1, "GASTO_MENOR", montoVal, "EFECTIVO", motivoInput)
                MostrarMensajeExito($"Movimiento de Caja Chica registrado exitosamente con ID #{idMov} y su asiento contable de partida doble generado en la BD.", "Registro Guardado")
                CargarPeriodosYMovimientos()
                CargarLibroDiario()
            Catch ex As Exception
                MostrarMensajeError($"Error al registrar movimiento: {ex.Message}")
            End Try
        End Sub

        ' =========================================================================
        ' 7. TAB: AUDITORÍA CONTABLE
        ' =========================================================================

        Private Sub CargarAuditoria()
            dgvAuditoria.DataSource = ContabilidadDAO.ObtenerAuditoriaContable()
        End Sub

        ' =========================================================================
        ' EXPORTAR INFORME OFICIAL PARA CONTADOR PÚBLICO AUTORIZADO (CPA / DGI)
        ' =========================================================================

        Private Sub btnExportarInformeCPA_Click(sender As Object, e As EventArgs) Handles btnExportarInformeCPA.Click
            Using sfd As New SaveFileDialog()
                sfd.Filter = "Documento de Texto Formateado (*.txt)|*.txt|Reporte Contable (*.csv)|*.csv"
                sfd.FileName = $"Informe_Contable_CPA_DGI_{DateTime.Today:yyyyMMdd}.txt"

                If sfd.ShowDialog(Me) = DialogResult.OK Then
                    Try
                        Dim sb As New StringBuilder()
                        sb.AppendLine("================================================================================")
                        sb.AppendLine("      RESTAURANTE EL SABOR PANAMEÑO - REPORTE CONTABLE Y FISCAL DGI")
                        sb.AppendLine("      CUMPLIMIENTO CON NORMATIVA DE LA DIRECCIÓN GENERAL DE INGRESOS (DGI)")
                        sb.AppendLine("      MARCO CONTABLE: NIIF PARA LAS PYMES (REPÚBLICA DE PANAMÁ)")
                        sb.AppendLine("================================================================================")
                        sb.AppendLine($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
                        sb.AppendLine($"Período de Análisis: {dtpFechaDesde.Value:dd/MM/yyyy} al {dtpFechaHasta.Value:dd/MM/yyyy}")
                        sb.AppendLine("Moneda Oficial: USD / Balboa (Paridad 1:1)")
                        sb.AppendLine("Tratamiento Fiscal: ITBMS General (7%) • Retención POS (50% del ITBMS)")
                        sb.AppendLine("Destinatario: Contador Público Autorizado (CPA) / Auditoría Fiscal")
                        sb.AppendLine("--------------------------------------------------------------------------------")
                        sb.AppendLine()

                        sb.AppendLine("1. ESTADO DE RESULTADOS INTEGRAL (P&L)")
                        sb.AppendLine("--------------------------------------------------------------------------------")
                        Dim dtResultados = ContabilidadDAO.GenerarEstadoResultados(dtpFechaDesde.Value.Date, dtpFechaHasta.Value.Date)
                        For Each r As DataRow In dtResultados.Rows
                            sb.AppendLine(String.Format("{0,-55} {1,20:C2}", r("Concepto").ToString(), Convert.ToDecimal(r("Monto"))))
                        Next
                        sb.AppendLine()

                        sb.AppendLine("2. BALANCE GENERAL CONDENSADO")
                        sb.AppendLine("--------------------------------------------------------------------------------")
                        Dim dtBalance = ContabilidadDAO.GenerarBalanceGeneral(dtpFechaHasta.Value.Date)
                        For Each r As DataRow In dtBalance.Rows
                            sb.AppendLine(String.Format("{0,-12} {1,-10} {2,-35} {3,18:C2}", r("Seccion").ToString(), r("Codigo").ToString(), r("Cuenta").ToString(), Convert.ToDecimal(r("Saldo"))))
                        Next
                        sb.AppendLine()

                        sb.AppendLine("3. RESUMEN DE PLANILLA Y CARGAS SOCIALES (7 COLABORADORES)")
                        sb.AppendLine("--------------------------------------------------------------------------------")
                        For Each emp In _listaEmpleados.Where(Function(x) x.Activo)
                            Dim ret = Math.Round(emp.SalarioMensual * 0.11D, 2)
                            Dim neto = emp.SalarioMensual - ret
                            sb.AppendLine(String.Format("Colaborador: {0,-25} | Cargo: {1,-15} | Bruto: {2,10:C2} | CSS/SE (11%): {3,9:C2} | Neto: {4,10:C2}", emp.NombreCompleto, emp.Cargo, emp.SalarioMensual, ret, neto))
                        Next
                        sb.AppendLine()

                        sb.AppendLine("4. CERTIFICACIÓN DE EQUILIBRIO DE PARTIDA DOBLE (LIBRO DIARIO)")
                        sb.AppendLine("--------------------------------------------------------------------------------")
                        Dim totalDebeDiario As Decimal = _listaAsientos.Sum(Function(x) x.TotalDebe)
                        Dim totalHaberDiario As Decimal = _listaAsientos.Sum(Function(x) x.TotalHaber)
                        sb.AppendLine($"Total Asientos Registrados: {_listaAsientos.Count}")
                        sb.AppendLine($"Total Débitos Acumulados:   ${totalDebeDiario:N2}")
                        sb.AppendLine($"Total Créditos Acumulados:  ${totalHaberDiario:N2}")
                        sb.AppendLine($"Diferencia de Cuadratura:   ${Math.Abs(totalDebeDiario - totalHaberDiario):N4}")
                        sb.AppendLine($"Certificación de Partida Doble: {If(Math.Abs(totalDebeDiario - totalHaberDiario) < 0.01D, "APROBADO Y BALANCEADO", "DESBALANCE DETECTADO")}")
                        sb.AppendLine()
                        sb.AppendLine("================================================================================")
                        sb.AppendLine("           FIN DEL INFORME CONTABLE OFICIAL PARA EL CPA / DGI")
                        sb.AppendLine("================================================================================")

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8)
                        MostrarMensajeExito($"Informe oficial exportado exitosamente a:{Environment.NewLine}{sfd.FileName}", "Exportación Exitosa")
                    Catch ex As Exception
                        MostrarMensajeError($"Error al guardar el archivo: {ex.Message}")
                    End Try
                End If
            End Using
        End Sub

    End Class
End Namespace

