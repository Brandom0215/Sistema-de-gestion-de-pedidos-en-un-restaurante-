Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Views.Cocina.Controles

Namespace Views.Cocina
    ''' <summary>
    ''' Formulario de Monitor de Cocina (KDS - Kitchen Display System).
    ''' Permite al personal de cocina recibir comandas en tiempo real, monitorear tiempos FIFO,
    ''' gestionar transiciones de estado (Recibido -> En Preparación -> Listo -> Entregado) y filtrar por canal.
    ''' Conectado bidireccionalmente con el repositorio DAO para reflejar pedidos de clientes,
    ''' pagos realizados en Caja y facturación electrónica PDF.
    ''' Prefijo de módulo: Ccn
    ''' </summary>
    Public Class FrmCcnMonitorCocina

        ''' <summary> Colección en memoria de todas las comandas gestionadas por el monitor </summary>
        Private ReadOnly _lstCcnComandas As New List(Of CcnPedidoModel)()

        ''' <summary> Filtro activo por tipo de servicio ("Todos", "Mesa", "Entregas") </summary>
        Private _strCcnFiltroServicioActivo As String = "Todos"

        ''' <summary> Indicador de sonido de alerta habilitado </summary>
        Private _blnCcnSonidoHabilitado As Boolean = True

        ''' <summary> Nombre del usuario cliente si la sesión pertenece a un cliente autenticado </summary>
        Private ReadOnly _strClienteLogueado As String = String.Empty

        Public Sub New()
            Me.New(String.Empty)
        End Sub

        Public Sub New(ByVal strClienteLogueado As String)
            InitializeComponent()
            _strClienteLogueado = strClienteLogueado
            ThemeConfig.HabilitarDobleBuffer(flpCcnContenedorComandas)
        End Sub

        Private Sub FrmCcnMonitorCocina_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            InicializarCriteriosOrdenamiento()

            If Not String.IsNullOrWhiteSpace(_strClienteLogueado) Then
                lblCcnTituloPrincipal.Text = $"Menú Digital & Comandas ({_strClienteLogueado})"
            End If

            SincronizarComandasDesdeDAO()
            RefrescarMonitorComandas()

            ' Suscribir a notificaciones de persistencia en tiempo real de PedidoDAO
            PedidoDAO.SuscribirPedidoRegistrado(AddressOf OnPedidoRegistradoDesdeDAO)
            PedidoDAO.SuscribirPedidoModificado(AddressOf OnPedidoModificadoDesdeDAO)

            ' Iniciar temporizador en tiempo real (3 segundos para respuesta inmediata)
            tmrCcnActualizadorRealTime.Interval = 3000
            tmrCcnActualizadorRealTime.Start()
        End Sub

        Private Sub FrmCcnMonitorCocina_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
            tmrCcnActualizadorRealTime.Stop()
            PedidoDAO.DesuscribirPedidoRegistrado(AddressOf OnPedidoRegistradoDesdeDAO)
            PedidoDAO.DesuscribirPedidoModificado(AddressOf OnPedidoModificadoDesdeDAO)
        End Sub

        ''' <summary>
        ''' Notificación reactiva inmediata cuando el cliente confirma un pedido en la base de datos.
        ''' </summary>
        Private Sub OnPedidoRegistradoDesdeDAO(ByVal idPedido As Integer)
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Of Integer)(AddressOf OnPedidoRegistradoDesdeDAO), idPedido)
                Return
            End If

            SincronizarComandasDesdeDAO()
            RefrescarMonitorComandas()

            If _blnCcnSonidoHabilitado Then
                Try
                    System.Media.SystemSounds.Asterisk.Play()
                Catch
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Notificación reactiva cuando un pedido cambia de estado en Caja o Facturación.
        ''' </summary>
        Private Sub OnPedidoModificadoDesdeDAO(ByVal idPedido As Integer)
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Of Integer)(AddressOf OnPedidoModificadoDesdeDAO), idPedido)
                Return
            End If

            SincronizarComandasDesdeDAO()
            RefrescarMonitorComandas()
        End Sub

        ' =========================================================================
        ' APLICACIÓN DE ESTILOS Y TEMÁTICA VISUAL
        ' =========================================================================

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            pnlCcnHeaderPrincipal.BackColor = ThemeConfig.ColorBackgroundApp
            pnlCcnBarraFiltros.BackColor = ThemeConfig.ColorBackgroundApp
            flpCcnContenedorComandas.BackColor = ThemeConfig.ColorBackgroundApp

            ' Tipografías y colores de títulos
            lblCcnTituloPrincipal.Font = ThemeConfig.ObtenerFuenteTitulo(16.5F, FontStyle.Bold)
            lblCcnTituloPrincipal.ForeColor = ThemeConfig.ColorNeutralDark
            lblCcnSubtituloVivo.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            lblCcnSubtituloVivo.ForeColor = ThemeConfig.ColorPrimary

            ' Estilizado de tarjetas KPI superiores con espaciado óptimo (sin Padding de 16px)
            Dim arrKpis = {pnlCcnKpiActivos, pnlCcnKpiEnCocina, pnlCcnKpiListos, pnlCcnKpiTMedio}
            For Each pnl In arrKpis
                pnl.BackColor = Color.White
                pnl.Padding = New Padding(4)
            Next

            pnlCcnKpiVentas.BackColor = ThemeConfig.ColorPrimaryLight
            pnlCcnKpiVentas.Padding = New Padding(4)

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
            cboCcnCriterioOrden.Items.Add("⭐ Cola FIFO (Primero en Entrar, Primero en Salir)")
            cboCcnCriterioOrden.Items.Add("Más recientes primero")
            cboCcnCriterioOrden.SelectedIndex = 0
        End Sub

        ' =========================================================================
        ' SINCRONIZACIÓN DE COMANDAS DESDE EL REPOSITORIO DAO (CLIENTE, CAJA, FACTURACIÓN)
        ' =========================================================================

        ''' <summary>
        ''' Carga y sincroniza las órdenes en tiempo real desde el repositorio DAO centralizado.
        ''' </summary>
        Public Sub SincronizarComandasDesdeDAO()
            _lstCcnComandas.Clear()
            Dim lstDesdeDAO As List(Of CcnPedidoModel) = PedidoDAO.ObtenerComandasCocina()
            If lstDesdeDAO IsNot Nothing AndAlso lstDesdeDAO.Count > 0 Then
                _lstCcnComandas.AddRange(lstDesdeDAO)
            End If
        End Sub

        ' =========================================================================
        ' RENDERIZADO Y CONTROL DE COMANDAS EN PANTALLA
        ' =========================================================================

        ''' <summary>
        ''' Refresca el lienzo de comandas aplicando filtros, ordenamiento FIFO y actualizando los KPIs.
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
                ' Ley de FIFO: Primero en Entrar, Primero en Salir / Atender
                lstFiltrada = lstFiltrada.OrderBy(Function(p) p.DtHoraRegistro).ThenBy(Function(p) p.IntIdPedido)
            End If

            ' 3. Instanciar y cargar cada tarjeta interactiva asignando el turno FIFO correspondiente
            Dim intPosicionFifo As Integer = 1
            For Each objComanda As CcnPedidoModel In lstFiltrada
                objComanda.IntPosicionFifo = intPosicionFifo
                intPosicionFifo += 1

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
                ' Validación de seguridad: no permitir transición a Entregado si no ha sido pagado
                If enumNuevoEstado = CcnEstadoPedidoEnum.Entregado AndAlso Not objPedidoObjetivo.BlnEstaPagado Then
                    MessageBox.Show(
                        $"No es posible entregar la comanda {objPedidoObjetivo.StrCodigoComanda} porque su pago aún no ha sido confirmado en el Módulo de Cobro / Caja.",
                        "Entrega No Permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )
                    Return
                End If

                objPedidoObjetivo.EnumEstado = enumNuevoEstado

                ' Sincronizar actualización de ciclo de vida con el repositorio centralizado DAO
                Dim strEstadoDAOCocina As String = "RECIBIDO"
                Select Case enumNuevoEstado
                    Case CcnEstadoPedidoEnum.EnPreparacion
                        strEstadoDAOCocina = "EN_PREPARACION"
                    Case CcnEstadoPedidoEnum.Listo
                        strEstadoDAOCocina = "LISTO"
                    Case CcnEstadoPedidoEnum.Entregado
                        strEstadoDAOCocina = "ENTREGADO"
                    Case Else
                        strEstadoDAOCocina = "RECIBIDO"
                End Select
                PedidoDAO.ActualizarEstadoCocina(intIdPedido, strEstadoDAOCocina)

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
            SincronizarComandasDesdeDAO()
            RefrescarMonitorComandas()
        End Sub

        Private Sub btnCcnAlertaSonora_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCcnAlertaSonora.Click
            _blnCcnSonidoHabilitado = Not _blnCcnSonidoHabilitado
            btnCcnAlertaSonora.Text = If(_blnCcnSonidoHabilitado, "🔔", "🔕")
        End Sub

        ''' <summary>
        ''' Actualiza el cronómetro visual de espera FIFO y recalcula las métricas KPI periódicamente.
        ''' </summary>
        Private Sub tmrCcnActualizadorRealTime_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrCcnActualizadorRealTime.Tick
            For Each ctl As Control In flpCcnContenedorComandas.Controls
                If TypeOf ctl Is UcCcnTarjetaComanda Then
                    DirectCast(ctl, UcCcnTarjetaComanda).ActualizarCronometro()
                End If
            Next
            ActualizarMetricasKpi()
        End Sub

    End Class
End Namespace
