Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Views.Cocina.Controles

Namespace Views.Cocina
    ''' <summary>
    ''' Formulario de Monitor de Cocina (KDS - Kitchen Display System).
    ''' Permite al personal de cocina recibir comandas en tiempo real, monitorear tiempos FIFO,
    ''' gestionar transiciones de estado (Recibido -> En Preparación -> Listo) y filtrar por canal.
    ''' Prefijo de módulo: Ccn
    ''' Cumple con notación húngara, diseño consistente y código fuertemente tipado.
    ''' </summary>
    Public Class FrmCcnMonitorCocina

        ''' <summary> Colección en memoria de todas las comandas gestionadas por el monitor </summary>
        Private ReadOnly _lstCcnComandas As New List(Of CcnPedidoModel)()

        ''' <summary> Filtro activo por tipo de servicio ("Todos", "Mesa", "Entregas") </summary>
        Private _strCcnFiltroServicioActivo As String = "Todos"

        ''' <summary> Indicador de sonido de alerta habilitado </summary>
        Private _blnCcnSonidoHabilitado As Boolean = True

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
            ThemeConfig.HabilitarDobleBuffer(flpCcnContenedorComandas)
        End Sub

        Private Sub FrmCcnMonitorCocina_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            InicializarCriteriosOrdenamiento()
            CargarComandasInicialesDemostracion()
            RefrescarMonitorComandas()

            ' Iniciar temporizador en tiempo real
            tmrCcnActualizadorRealTime.Interval = 10000
            tmrCcnActualizadorRealTime.Start()
        End Sub

        ' =========================================================================
        ' APLICACIÓN DE ESTILOS Y TEMÁTICA VISUAL
        ' =========================================================================

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCcnHeaderPrincipal.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCcnBarraFiltros.BackColor = ThemeConfig.ColorBackgroundApp
            flpCcnContenedorComandas.BackColor = ThemeConfig.ColorBackgroundApp

            ' Tipografías y colores de títulos
            lblCcnTituloPrincipal.Font = ThemeConfig.ObtenerFuenteTitulo(15.0F, FontStyle.Bold)
            lblCcnTituloPrincipal.ForeColor = ThemeConfig.ColorNeutralDark
            lblCcnSubtituloVivo.ForeColor = ThemeConfig.ColorPrimary

            ' Estilizado de tarjetas KPI superiores
            ThemeConfig.AplicarEstiloTarjeta(pnlCcnKpiActivos)
            ThemeConfig.AplicarEstiloTarjeta(pnlCcnKpiEnCocina)
            ThemeConfig.AplicarEstiloTarjeta(pnlCcnKpiListos)
            ThemeConfig.AplicarEstiloTarjeta(pnlCcnKpiTMedio)

            pnlCcnKpiVentas.BackColor = ThemeConfig.ColorPrimaryLight
            pnlCcnKpiVentas.Padding = New Padding(8, 6, 8, 6)

            lblCcnKpiActivosValor.ForeColor = ThemeConfig.ColorNeutralDark
            lblCcnKpiEnCocinaValor.ForeColor = ThemeConfig.ColorPrimary
            lblCcnKpiListosValor.ForeColor = ThemeConfig.ColorTertiarySuccess
            lblCcnKpiTMedioValor.ForeColor = ThemeConfig.ColorNeutralDark
            lblCcnKpiVentasValor.ForeColor = ThemeConfig.ColorPrimaryDark

            ' Estilizado de botones y controles de filtro
            ThemeConfig.EstilizarBotonSecundario(btnCcnAlertaSonora)
            ThemeConfig.EstilizarBotonSecundario(btnCcnRefrescarManual)

            ActualizarEstiloBotonesFiltro()
        End Sub

        Private Sub InicializarCriteriosOrdenamiento()
            cboCcnCriterioOrden.Items.Clear()
            cboCcnCriterioOrden.Items.Add("Más antiguos primero (Prioridad)")
            cboCcnCriterioOrden.Items.Add("Más recientes primero")
            cboCcnCriterioOrden.SelectedIndex = 0
        End Sub

        ' =========================================================================
        ' CARGA DE DATOS DEMOSTRATIVOS (ALINEADOS AL MOCKUP)
        ' =========================================================================

        Private Sub CargarComandasInicialesDemostracion()
            _lstCcnComandas.Clear()

            ' 1. Comanda Mesa 04 (En Cocina)
            Dim objComanda1 As New CcnPedidoModel(
                1, "#08-1042", "MESA 04", "Mateo R.", "Carlos Mendoza", "Mesa / Salón",
                "Tarjeta Crédito", DateTime.Now.AddMinutes(-8), CcnEstadoPedidoEnum.EnPreparacion
            )
            objComanda1.LstDetallePlatos.Add(New CcnItemPedidoModel(2, "Bife de Chorizo a la Brasa", "Término medio, papas rústicas al romero", 24.0D))
            objComanda1.LstDetallePlatos.Add(New CcnItemPedidoModel(1, "Ensalada Oliva & Burrata", "Aceite virgen extra cosecha temprana", 12.5D))
            objComanda1.LstDetallePlatos.Add(New CcnItemPedidoModel(1, "Vino Malbec Reserva (Copa)", "Copa 150ml temperatura bodega", 8.0D))
            _lstCcnComandas.Add(objComanda1)

            ' 2. Comanda Mesa 09 (Pendiente / Celíaco)
            Dim objComanda2 As New CcnPedidoModel(
                2, "#08-1045", "MESA 09", "Elena G.", "María Fernández", "Mesa / Salón",
                "Efectivo pendiente", DateTime.Now.AddMinutes(-2), CcnEstadoPedidoEnum.Recibido
            )
            objComanda2.LstDetallePlatos.Add(New CcnItemPedidoModel(1, "Risotto de Hongos Silvestres", "Con reducción de parmesano", 24.0D, True, "CELÍACO: Estrictamente Sin Gluten"))
            objComanda2.LstDetallePlatos.Add(New CcnItemPedidoModel(1, "Pasta Fresca al Pesto de Pistacho", "Parmesano reggiano rallado al momento", 18.0D))
            _lstCcnComandas.Add(objComanda2)

            ' 3. Comanda Entregas (Listo para Entrega)
            Dim objComanda3 As New CcnPedidoModel(
                3, "#08-1039", "ENTREGAS", "", "Sofía Alarcón", "Entregas",
                "Pagado Web (Stripe)", DateTime.Now.AddMinutes(-23), CcnEstadoPedidoEnum.Listo
            )
            objComanda3.LstDetallePlatos.Add(New CcnItemPedidoModel(1, "Pollo al Limón y Romero a la Leña", "Empacado en contenedor térmico kraft", 26.0D))
            objComanda3.LstDetallePlatos.Add(New CcnItemPedidoModel(1, "Focaccia Artesanal de Romero & Sal Gruesa", "Porción individual dorada", 8.0D))
            _lstCcnComandas.Add(objComanda3)
        End Sub

        ' =========================================================================
        ' RENDERIZADO Y CONTROL DE COMANDAS EN PANTALLA
        ' =========================================================================

        ''' <summary>
        ''' Refresca el lienzo de comandas aplicando filtros, ordenamiento y actualizando los KPIs.
        ''' </summary>
        Public Sub RefrescarMonitorComandas()
            flpCcnContenedorComandas.SuspendLayout()
            flpCcnContenedorComandas.Controls.Clear()

            ' 1. Filtrar lista según el chip seleccionado
            Dim lstFiltrada As IEnumerable(Of CcnPedidoModel) = _lstCcnComandas.Where(Function(p) p.EnumEstado <> CcnEstadoPedidoEnum.Entregado)

            If _strCcnFiltroServicioActivo.Equals("Mesa", StringComparison.OrdinalIgnoreCase) Then
                lstFiltrada = lstFiltrada.Where(Function(p) p.StrTipoServicio.IndexOf("Mesa", StringComparison.OrdinalIgnoreCase) >= 0)
            ElseIf _strCcnFiltroServicioActivo.Equals("Entregas", StringComparison.OrdinalIgnoreCase) Then
                lstFiltrada = lstFiltrada.Where(Function(p) p.StrTipoServicio.IndexOf("Entrega", StringComparison.OrdinalIgnoreCase) >= 0 OrElse p.StrTipoServicio.IndexOf("Llevar", StringComparison.OrdinalIgnoreCase) >= 0)
            End If

            ' 2. Aplicar Criterio de Ordenamiento (FIFO - más antiguos primero por defecto)
            If cboCcnCriterioOrden.SelectedIndex = 1 Then
                lstFiltrada = lstFiltrada.OrderByDescending(Function(p) p.DtHoraRegistro)
            Else
                lstFiltrada = lstFiltrada.OrderBy(Function(p) p.DtHoraRegistro)
            End If

            ' 3. Instanciar y cargar cada tarjeta interactiva
            For Each objComanda As CcnPedidoModel In lstFiltrada
                Dim ucTarjeta As New UcCcnTarjetaComanda()
                ucTarjeta.CargarComanda(objComanda)
                AddHandler ucTarjeta.CcnCambioEstadoSolicitado, AddressOf OnCcnCambioEstadoSolicitado
                flpCcnContenedorComandas.Controls.Add(ucTarjeta)
            Next

            flpCcnContenedorComandas.ResumeLayout(True)

            ' 4. Recalcular y actualizar métricas de cabecera y conteos de filtros
            ActualizarMetricasKpi()
        End Sub

        ''' <summary>
        ''' Manejador del evento de avance de estado de comanda emitido por la tarjeta.
        ''' </summary>
        Private Sub OnCcnCambioEstadoSolicitado(ByVal sender As Object, ByVal intIdPedido As Integer, ByVal enumNuevoEstado As CcnEstadoPedidoEnum)
            Dim objPedidoObjetivo As CcnPedidoModel = _lstCcnComandas.FirstOrDefault(Function(p) p.IntIdPedido = intIdPedido)

            If objPedidoObjetivo IsNot Nothing Then
                objPedidoObjetivo.EnumEstado = enumNuevoEstado

                If enumNuevoEstado = CcnEstadoPedidoEnum.Entregado Then
                    MessageBox.Show($"La comanda {objPedidoObjetivo.StrCodigoComanda} ha sido finalizada y despachada.", "Despacho Completado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                RefrescarMonitorComandas()
            End If
        End Sub

        ''' <summary>
        ''' Actualiza en tiempo real los contadores de los KPIs superiores y los chips de filtro.
        ''' </summary>
        Private Sub ActualizarMetricasKpi()
            Dim intTotalActivos As Integer = _lstCcnComandas.Where(Function(p) p.EnumEstado <> CcnEstadoPedidoEnum.Entregado).Count()
            Dim intEnCocina As Integer = _lstCcnComandas.Where(Function(p) p.EnumEstado = CcnEstadoPedidoEnum.EnPreparacion).Count()
            Dim intListos As Integer = _lstCcnComandas.Where(Function(p) p.EnumEstado = CcnEstadoPedidoEnum.Listo).Count()

            Dim intTotalMesa As Integer = _lstCcnComandas.Where(Function(p) p.EnumEstado <> CcnEstadoPedidoEnum.Entregado AndAlso p.StrTipoServicio.IndexOf("Mesa", StringComparison.OrdinalIgnoreCase) >= 0).Count()
            Dim intTotalEntregas As Integer = _lstCcnComandas.Where(Function(p) p.EnumEstado <> CcnEstadoPedidoEnum.Entregado AndAlso (p.StrTipoServicio.IndexOf("Entrega", StringComparison.OrdinalIgnoreCase) >= 0 OrElse p.StrTipoServicio.IndexOf("Llevar", StringComparison.OrdinalIgnoreCase) >= 0)).Count()

            Dim decVentasTurno As Decimal = _lstCcnComandas.Sum(Function(p) p.CalcularTotal())

            lblCcnKpiActivosValor.Text = intTotalActivos.ToString()
            lblCcnKpiEnCocinaValor.Text = intEnCocina.ToString()
            lblCcnKpiListosValor.Text = intListos.ToString()
            lblCcnKpiVentasValor.Text = $"${decVentasTurno:N2}"

            ' Promedio de minutos transcurridos
            Dim dblMinutosPromedio As Double = If(intTotalActivos > 0, _lstCcnComandas.Where(Function(p) p.EnumEstado <> CcnEstadoPedidoEnum.Entregado).Average(Function(p) p.ObtenerMinutosTranscurridos()), 0)
            lblCcnKpiTMedioValor.Text = $"{Convert.ToInt32(Math.Round(dblMinutosPromedio))} min"

            ' Textos de los botones de filtro
            btnCcnFiltroTodos.Text = $"Todos los pedidos ({intTotalActivos})"
            btnCcnFiltroMesa.Text = $"🍽 Mesa / Salón ({intTotalMesa})"
            btnCcnFiltroEntregas.Text = $"🛍 Entregas ({intTotalEntregas})"
        End Sub

        ' =========================================================================
        ' EVENTOS DE FILTROS, ORDENAMIENTO Y ACCIONES
        ' =========================================================================

        Private Sub ActualizarEstiloBotonesFiltro()
            Dim arrBotones = {
                Tuple.Create(btnCcnFiltroTodos, "Todos"),
                Tuple.Create(btnCcnFiltroMesa, "Mesa"),
                Tuple.Create(btnCcnFiltroEntregas, "Entregas")
            }

            For Each t In arrBotones
                Dim btnActual As Button = t.Item1
                Dim strClave As String = t.Item2

                If _strCcnFiltroServicioActivo.Equals(strClave, StringComparison.OrdinalIgnoreCase) Then
                    btnActual.BackColor = ThemeConfig.ColorPrimary
                    btnActual.ForeColor = Color.White
                    btnActual.Font = ThemeConfig.ObtenerFuenteCuerpo(9.0F, FontStyle.Bold)
                    btnActual.FlatAppearance.BorderSize = 0
                Else
                    btnActual.BackColor = Color.White
                    btnActual.ForeColor = ThemeConfig.ColorNeutralDark
                    btnActual.Font = ThemeConfig.ObtenerFuenteCuerpo(9.0F, FontStyle.Regular)
                    btnActual.FlatAppearance.BorderColor = ThemeConfig.ColorBorder
                    btnActual.FlatAppearance.BorderSize = 1
                End If
            Next
        End Sub

        Private Sub btnCcnFiltroTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnFiltroTodos.Click
            _strCcnFiltroServicioActivo = "Todos"
            ActualizarEstiloBotonesFiltro()
            RefrescarMonitorComandas()
        End Sub

        Private Sub btnCcnFiltroMesa_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnFiltroMesa.Click
            _strCcnFiltroServicioActivo = "Mesa"
            ActualizarEstiloBotonesFiltro()
            RefrescarMonitorComandas()
        End Sub

        Private Sub btnCcnFiltroEntregas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnFiltroEntregas.Click
            _strCcnFiltroServicioActivo = "Entregas"
            ActualizarEstiloBotonesFiltro()
            RefrescarMonitorComandas()
        End Sub

        Private Sub cboCcnCriterioOrden_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCcnCriterioOrden.SelectedIndexChanged
            RefrescarMonitorComandas()
        End Sub

        Private Sub btnCcnRefrescarManual_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnRefrescarManual.Click
            RefrescarMonitorComandas()
        End Sub

        Private Sub btnCcnAlertaSonora_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnAlertaSonora.Click
            _blnCcnSonidoHabilitado = Not _blnCcnSonidoHabilitado
            btnCcnAlertaSonora.Text = If(_blnCcnSonidoHabilitado, "🔔", "🔕")
        End Sub

        Private Sub tmrCcnActualizadorRealTime_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrCcnActualizadorRealTime.Tick
            ' Actualiza los tiempos transcurridos en pantalla de manera fluida
            For Each ctl As Control In flpCcnContenedorComandas.Controls
                If TypeOf ctl Is UcCcnTarjetaComanda Then
                    ' Cada tarjeta refresca su cronómetro interno
                End If
            Next
            ActualizarMetricasKpi()
        End Sub

    End Class
End Namespace
