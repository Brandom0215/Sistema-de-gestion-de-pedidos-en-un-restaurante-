Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views
    ''' <summary>
    ''' Formulario Principal Contenedor (Dashboard Base) que aloja la barra lateral de navegación,
    ''' la barra superior de estado y el panel central desacoplado donde se incrustan los formularios hijos.
    ''' </summary>
    Public Class FrmHome

        ''' <summary> Referencia al formulario hijo actualmente incrustado en el panel principal </summary>
        Private _formularioActivo As Form = Nothing

        ''' <summary> Botón de navegación actualmente seleccionado </summary>
        Private _botonMenuSeleccionado As Button = Nothing

        ''' <summary> Rol del usuario autenticado en la sesión </summary>
        Private _rolUsuario As String = "👑 Administrador"

        ''' <summary> Nombre del usuario autenticado </summary>
        Private _nombreUsuario As String = "admin"

        Public Sub New()
            InitializeComponent()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Public Sub New(rolUsuario As String, nombreUsuario As String)
            Me.New()
            _rolUsuario = rolUsuario
            _nombreUsuario = nombreUsuario
        End Sub

        Private Sub FrmHome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' 1. Configurar estilos visuales y paleta de colores
            AplicarTemaVisual()

            ' 2. Actualizar reloj inicial
            ActualizarRelojSistema()

            ' 3. Aplicar permisos y visibilidad estricta del Sidebar según el ROL
            AplicarPermisosPorRol()

            ' 4. Cargar la vista inicial según el ROL del usuario
            If _rolUsuario.Contains("Cliente") Then
                lblTituloModuloTop.Text = $"📲 Menú Digital de Pedidos (Cliente: {_nombreUsuario})"
                SeleccionarBotonNavegacion(btnNavPedidos, "Toma de Pedidos & Menú Digital")
                AbrirFormularioEnPanel(Of Pedidos.FrmPedidos)()
            ElseIf _rolUsuario.Contains("Cocina") Then
                SeleccionarBotonNavegacion(btnNavCocina, "Monitor de Cocina (KDS)")
                AbrirFormularioEnPanel(Of Cocina.FrmCcnMonitorCocina)()
            Else
                ' Administrador / Cajero por defecto en Dashboard
                SeleccionarBotonNavegacion(btnNavDashboard, "Dashboard General")
                AbrirFormularioEnPanel(Of Dashboards.FrmDashboardGeneral)()
            End If
        End Sub

        ''' <summary>
        ''' Restringe y oculta las opciones del menú lateral según el ROL del usuario autenticado.
        ''' </summary>
        Private Sub AplicarPermisosPorRol()
            ' Actualizar etiqueta de pie de sidebar con el usuario y rol
            lblEstadoBaseDatos.Text = $"🟢 ROL: {_rolUsuario.Replace("👑 ", "").Replace("💵 ", "").Replace("🍳 ", "").Replace("📲 ", "")}"

            If _rolUsuario.Contains("Cliente") Then
                ' EL CLIENTE ÚNICAMENTE ACCEDE A SU VISTA DE PEDIDOS / MENÚ DIGITAL
                btnNavDashboard.Visible = False
                btnNavCatalogo.Visible = False
                btnNavCocina.Visible = False
                btnNavCaja.Visible = False
                btnNavFacturacion.Visible = False
                btnNavReportes.Visible = False
                
                btnNavPedidos.Visible = True
                btnNavPedidos.Text = "  📲 Mi Pedido / Menú Digital"
                btnNavPedidos.Location = New Point(0, 70)
            ElseIf _rolUsuario.Contains("Cocina") Then
                ' PERSONAL DE COCINA ÚNICAMENTE ACCEDE A KDS Y CONSULTA DE PEDIDOS
                btnNavDashboard.Visible = False
                btnNavCatalogo.Visible = False
                btnNavCocina.Visible = True
                btnNavCaja.Visible = False
                btnNavFacturacion.Visible = False
                btnNavReportes.Visible = False
                btnNavPedidos.Visible = True
            ElseIf _rolUsuario.Contains("Cajero") Then
                ' CAJERO ACCEDE A PEDIDOS, CAJA Y FACTURACIÓN
                btnNavDashboard.Visible = True
                btnNavCatalogo.Visible = False
                btnNavCocina.Visible = False
                btnNavCaja.Visible = True
                btnNavFacturacion.Visible = True
                btnNavReportes.Visible = False
                btnNavPedidos.Visible = True
            Else
                ' ADMINISTRADOR TIENE ACCESO COMPLETO
                btnNavDashboard.Visible = True
                btnNavCatalogo.Visible = True
                btnNavCocina.Visible = True
                btnNavCaja.Visible = True
                btnNavFacturacion.Visible = True
                btnNavReportes.Visible = True
                btnNavPedidos.Visible = True
            End If
        End Sub



        ' =========================================================================
        ' MÉTODOS DE ARQUITECTURA: CARGA DESACOPLADA DE FORMULARIOS HIJOS
        ' =========================================================================

        ''' <summary>
        ''' Método genérico desacoplado para incrustar formularios dentro de PnlContenedorPrincipal.
        ''' Permite a cualquier integrante del equipo instanciar y mostrar su módulo sin parpadeos.
        ''' </summary>
        ''' <typeparam name="T">Tipo del Formulario a instanciar</typeparam>
        Public Sub AbrirFormularioEnPanel(Of T As {Form, New})()
            If _formularioActivo IsNot Nothing AndAlso _formularioActivo.GetType() Is GetType(T) Then
                Return
            End If

            If _formularioActivo IsNot Nothing Then
                _formularioActivo.Close()
                _formularioActivo.Dispose()
                _formularioActivo = Nothing
            End If

            _formularioActivo = New T()
            _formularioActivo.TopLevel = False
            _formularioActivo.FormBorderStyle = FormBorderStyle.None
            _formularioActivo.Dock = DockStyle.Fill

            pnlContenedorPrincipal.SuspendLayout()
            pnlContenedorPrincipal.Controls.Clear()
            pnlContenedorPrincipal.Controls.Add(_formularioActivo)
            pnlContenedorPrincipal.Tag = _formularioActivo
            _formularioActivo.Show()
            _formularioActivo.BringToFront()
            pnlContenedorPrincipal.ResumeLayout(True)
        End Sub

        ''' <summary>
        ''' Sobrecarga para incrustar una instancia existente de un formulario hijo.
        ''' </summary>
        Public Sub AbrirFormularioEnPanel(ByVal formularioHijo As Form)
            If formularioHijo Is Nothing Then Return

            If _formularioActivo IsNot Nothing Then
                _formularioActivo.Close()
                _formularioActivo.Dispose()
                _formularioActivo = Nothing
            End If

            _formularioActivo = formularioHijo
            _formularioActivo.TopLevel = False
            _formularioActivo.FormBorderStyle = FormBorderStyle.None
            _formularioActivo.Dock = DockStyle.Fill

            pnlContenedorPrincipal.SuspendLayout()
            pnlContenedorPrincipal.Controls.Clear()
            pnlContenedorPrincipal.Controls.Add(_formularioActivo)
            pnlContenedorPrincipal.Tag = _formularioActivo
            _formularioActivo.Show()
            _formularioActivo.BringToFront()
            pnlContenedorPrincipal.ResumeLayout(True)
        End Sub

        ' =========================================================================
        ' ESTILIZADO Y TEMÁTICA VISUAL
        ' =========================================================================

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlSidebar.BackColor = ThemeConfig.ColorBackgroundSidebar
            pnlTopBar.BackColor = ThemeConfig.ColorBackgroundCard
            pnlContenedorPrincipal.BackColor = ThemeConfig.ColorBackgroundApp
            pnlBarraIndicadorMenu.BackColor = ThemeConfig.ColorPrimary

            lblNombreRestaurante.ForeColor = ThemeConfig.ColorNeutralDark
            lblNombreRestaurante.Font = ThemeConfig.ObtenerFuenteTitulo(13.5F, FontStyle.Bold)
            lblEsloganRestaurante.ForeColor = ThemeConfig.ColorTextMuted

            lblTituloModuloTop.ForeColor = ThemeConfig.ColorNeutralDark
            lblTituloModuloTop.Font = ThemeConfig.ObtenerFuenteTitulo(14.0F, FontStyle.Bold)

            lblFechaHoraSistema.ForeColor = ThemeConfig.ColorNeutralDark
            lblFechaHoraSistema.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.5F, FontStyle.Bold)

            pnlSidebarFooter.BackColor = ThemeConfig.ColorBackgroundSidebar
            lblEstadoBaseDatos.ForeColor = ThemeConfig.ColorTertiarySuccess

            lblNombreUsuario.Text = _nombreUsuario
            lblRolUsuario.Text = _rolUsuario.Replace("👑 ", "").Replace("💵 ", "").Replace("🍳 ", "").Replace("📲 ", "")

            ThemeConfig.EstilizarBotonEliminar(btnCerrarSesion)

            Dim botonesNav = {btnNavDashboard, btnNavCatalogo, btnNavPedidos, btnNavCocina, btnNavCaja, btnNavFacturacion, btnNavReportes}
            For Each btn In botonesNav
                ThemeConfig.EstilizarBotonNavegacion(btn, False)
            Next
        End Sub


        Private Sub SeleccionarBotonNavegacion(btnSeleccionado As Button, tituloModulo As String)
            If btnSeleccionado Is Nothing Then Return

            Dim botonesNav = {btnNavDashboard, btnNavCatalogo, btnNavPedidos, btnNavCocina, btnNavCaja, btnNavFacturacion, btnNavReportes}
            For Each btn In botonesNav
                ThemeConfig.EstilizarBotonNavegacion(btn, False)
            Next

            ThemeConfig.EstilizarBotonNavegacion(btnSeleccionado, True)
            _botonMenuSeleccionado = btnSeleccionado
            lblTituloModuloTop.Text = tituloModulo

            pnlBarraIndicadorMenu.Location = New Point(0, btnSeleccionado.Location.Y)
            pnlBarraIndicadorMenu.Height = btnSeleccionado.Height
        End Sub

        ' =========================================================================
        ' EVENTOS DE NAVEGACIÓN Y RELOJ
        ' =========================================================================

        Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
            SeleccionarBotonNavegacion(btnNavDashboard, "Dashboard General")
            AbrirFormularioEnPanel(Of Dashboards.FrmDashboardGeneral)()
        End Sub

        Private Sub btnNavCatalogo_Click(sender As Object, e As EventArgs) Handles btnNavCatalogo.Click
            SeleccionarBotonNavegacion(btnNavCatalogo, "Menú & Catálogo de Productos")
            MostrarMensajeModulo("Menú & Catálogo", "Gestión de platos, categorías, precios e insumos de disponibilidad (RF-002, RF-003, RF-012).")
        End Sub

        Private Sub btnNavPedidos_Click(sender As Object, e As EventArgs) Handles btnNavPedidos.Click
            SeleccionarBotonNavegacion(btnNavPedidos, "Gestión de Pedidos (RestauranteDB)")
            AbrirFormularioEnPanel(Of Pedidos.FrmPedidos)()
        End Sub


        Private Sub btnNavCocina_Click(sender As Object, e As EventArgs) Handles btnNavCocina.Click
            SeleccionarBotonNavegacion(btnNavCocina, "Monitor de Cocina (KDS)")
            AbrirFormularioEnPanel(Of Cocina.FrmCcnMonitorCocina)()
        End Sub

        Private Sub btnNavCaja_Click(sender As Object, e As EventArgs) Handles btnNavCaja.Click
            SeleccionarBotonNavegacion(btnNavCaja, "Caja & Procesamiento de Pagos")
            MostrarMensajeModulo("Caja & Cobros", "Aprobación de pagos en efectivo, integración de pasarelas y liberación de comandas a cocina (RF-006, RF-011).")
        End Sub

        Private Sub btnNavFacturacion_Click(sender As Object, e As EventArgs) Handles btnNavFacturacion.Click
            SeleccionarBotonNavegacion(btnNavFacturacion, "Facturación & Comprobantes PDF")
            MostrarMensajeModulo("Facturación & PDF", "Generación e impresión de facturas en PDF inalterables con número correlativo único (RF-007, RN-009, RN-010).")
        End Sub

        Private Sub btnNavReportes_Click(sender As Object, e As EventArgs) Handles btnNavReportes.Click
            SeleccionarBotonNavegacion(btnNavReportes, "Reportes de Ventas & Cierre")
            MostrarMensajeModulo("Reportes & Ventas", "Generación de métricas diarias, reporte de platos más vendidos y cierre de caja (RF-013).")
        End Sub

        Private Sub tmrRelojSistema_Tick(sender As Object, e As EventArgs) Handles tmrRelojSistema.Tick
            ActualizarRelojSistema()
        End Sub

        Private Sub ActualizarRelojSistema()
            Dim ahora As DateTime = DateTime.Now
            Dim diaSemana As String = ahora.ToString("dddd", New System.Globalization.CultureInfo("es-ES"))
            diaSemana = Char.ToUpper(diaSemana(0)) & diaSemana.Substring(1)
            lblFechaHoraSistema.Text = $"📅 {diaSemana}, {ahora:dd MMM} • {ahora:HH:mm:ss}"
        End Sub

        Private Sub MostrarMensajeModulo(nombreModulo As String, descripcionFuncional As String)
            If _formularioActivo IsNot Nothing Then
                _formularioActivo.Close()
                _formularioActivo.Dispose()
                _formularioActivo = Nothing
            End If
            Dim pnlPlaceholder As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = ThemeConfig.ColorBackgroundApp
            }

            Dim lblInfo As New Label With {
                .Text = $"📌 Módulo: {nombreModulo}{vbCrLf}{vbCrLf}{descripcionFuncional}{vbCrLf}{vbCrLf}Listo para incrustar el formulario correspondiente del equipo.{vbCrLf}Ejemplo: 'FrmHome.AbrirFormularioEnPanel(Of Frm{nombreModulo.Replace(" ", "")})()'",
                .Font = ThemeConfig.ObtenerFuenteSubtitulo(11.0F, FontStyle.Regular),
                .ForeColor = ThemeConfig.ColorNeutralDark,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Fill,
                .Padding = New Padding(30)
            }

            pnlPlaceholder.Controls.Add(lblInfo)

            pnlContenedorPrincipal.Controls.Clear()
            pnlContenedorPrincipal.Controls.Add(pnlPlaceholder)
        End Sub

        ''' <summary>
        ''' Cierra la sesión activa del usuario y regresa al formulario de Inicio de Sesión (FrmLogin).
        ''' </summary>
        Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
            Dim confirmacion = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual y regresar al inicio de sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirmacion = DialogResult.Yes Then
                Dim frmLog As New Auth.FrmLogin()
                Me.Hide()
                frmLog.ShowDialog()
                Me.Close()
            End If
        End Sub

    End Class

End Namespace
