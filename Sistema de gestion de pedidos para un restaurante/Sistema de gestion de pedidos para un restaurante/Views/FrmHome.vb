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
        Private _rolUsuario As String = "📲 Cliente (Autoatención)"

        ''' <summary> Nombre del usuario autenticado </summary>
        Private _nombreUsuario As String = "Invitado"

        ''' <summary> Estado colapsado / desplegado de la barra lateral (Menú Hamburguesa) </summary>
        Private _sidebarColapsado As Boolean = False

        Public Sub New()
            InitializeComponent()
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

            ' 3. Configurar sesión y cargar vista inicial (inicia en Módulo Cliente)
            EstablecerSesion(_rolUsuario, _nombreUsuario)
        End Sub

        ''' <summary>
        ''' Configura dinámicamente la sesión del sistema entre Modo Cliente (Invitado/Autoservicio) y Acceso Personal (Staff).
        ''' </summary>
        Public Sub EstablecerSesion(rol As String, usuario As String)
            _rolUsuario = rol
            _nombreUsuario = usuario

            Dim enumRol As Models.RolUsuarioEnum = Models.RolUsuarioExtensions.ParsearRol(_rolUsuario)
            Dim rolLimpio As String = Models.RolUsuarioExtensions.ObtenerEtiqueta(enumRol)

            lblNombreUsuario.Text = _nombreUsuario
            lblRolUsuario.Text = rolLimpio
            lblAvatarIcono.Text = If(Not String.IsNullOrEmpty(usuario), usuario.Substring(0, 1).ToUpper(), "U")

            AplicarPermisosPorRol()

            If Models.RolUsuarioExtensions.EsCliente(enumRol) Then
                Dim tituloVista As String = If(usuario.Equals("Invitado", StringComparison.OrdinalIgnoreCase), "Carta & Menú Digital", $"Carta & Menú Digital ({_nombreUsuario})")
                lblTituloModuloTop.Text = tituloVista
                SeleccionarBotonNavegacion(btnNavCliente, tituloVista)
                AbrirFormularioEnPanel(Of Pedidos.FrmClienteMenu)()
            ElseIf enumRol = Models.RolUsuarioEnum.Cocina Then
                SeleccionarBotonNavegacion(btnNavCocina, "Monitor de Cocina (KDS)")
                AbrirFormularioEnPanel(Of Cocina.FrmCcnMonitorCocina)()
            ElseIf enumRol = Models.RolUsuarioEnum.Cajero Then
                SeleccionarBotonNavegacion(btnNavCobroAdmin, "Caja & Procesamiento de Pagos")
                AbrirFormularioEnPanel(Of Caja.FrmCajaCobros)()
            Else
                ' Administrador por defecto en Métricas & Ingresos del Negocio
                SeleccionarBotonNavegacion(btnNavMetricas, "Métricas & Rendimiento del Negocio")
                AbrirFormularioEnPanel(Of Dashboards.FrmDashboardGeneral)()
            End If
        End Sub

        ''' <summary>
        ''' Restringe y configura las opciones del menú lateral según el ROL del usuario autenticado.
        ''' Administrador: Exclusivamente Métricas de Ingresos y Gestión de Menú/Platos.
        ''' Cajero: Toma de pedidos y Caja & Cobros.
        ''' Cocina: Monitor de cocina KDS.
        ''' Cliente: Carta y pedidos digitales.
        ''' </summary>
        Private Sub AplicarPermisosPorRol()
            Dim enumRol As Models.RolUsuarioEnum = Models.RolUsuarioExtensions.ParsearRol(_rolUsuario)
            Dim rolLimpio As String = Models.RolUsuarioExtensions.ObtenerEtiqueta(enumRol)
            lblEstadoBaseDatos.Text = $"Conectado: {rolLimpio}"

            If Models.RolUsuarioExtensions.EsCliente(enumRol) Then
                ' En Modo Cliente se muestra Acceso Personal en TopBar y se oculta Cerrar Sesión en Sidebar
                btnAccesoPersonal.Visible = True
                btnCerrarSesion.Visible = False

                ' EL CLIENTE ÚNICAMENTE ACCEDE A SU CARTA Y PEDIDOS
                btnNavCliente.Visible = True
                btnNavCliente.Text = "  Carta & Pedidos"
                btnNavCocina.Visible = False
                btnNavCobroAdmin.Visible = False
                btnNavFacturacion.Visible = False
                btnNavMetricas.Visible = False
                btnNavCatalogo.Visible = False
            Else
                ' En Modo Personal (Staff) se oculta el botón de acceso y se habilita Cerrar Sesión
                btnAccesoPersonal.Visible = False
                btnCerrarSesion.Visible = True

                Select Case enumRol
                    Case Models.RolUsuarioEnum.Cocina
                        ' PERSONAL DE COCINA ÚNICAMENTE ACCEDE AL MONITOR KDS
                        btnNavCliente.Visible = False
                        btnNavCocina.Visible = True
                        btnNavCocina.Text = "  Monitor de Cocina"
                        btnNavCobroAdmin.Visible = False
                        btnNavFacturacion.Visible = False
                        btnNavMetricas.Visible = False
                        btnNavCatalogo.Visible = False
                    Case Models.RolUsuarioEnum.Cajero
                        ' CAJERO TIENE ACCESO A TOMA DE PEDIDOS, CAJA/COBROS E HISTORIAL DE FACTURAS
                        btnNavCliente.Visible = True
                        btnNavCliente.Text = "  Toma de Pedidos"
                        btnNavCocina.Visible = False
                        btnNavCobroAdmin.Visible = True
                        btnNavCobroAdmin.Text = "  Caja & Cobros"
                        btnNavFacturacion.Visible = True
                        btnNavFacturacion.Text = "  Historial Facturas"
                        btnNavMetricas.Visible = False
                        btnNavCatalogo.Visible = False
                    Case Else
                        ' ADMINISTRADOR: ÚNICAMENTE MÉTRICAS/INGRESOS Y GESTIÓN DE MENÚ/PLATOS
                        ' Todos los demás apartados (Pedidos, Cocina, Caja/Cobros, Facturas) se quitan de su vista
                        btnNavCliente.Visible = False
                        btnNavCocina.Visible = False
                        btnNavCobroAdmin.Visible = False
                        btnNavFacturacion.Visible = False
                        btnNavMetricas.Visible = True
                        btnNavMetricas.Text = "  Métricas & Ingresos"
                        btnNavCatalogo.Visible = True
                        btnNavCatalogo.Text = "  Menú & Platos"
                End Select
            End If

            ReorganizarBotonesMenu()
        End Sub

        ''' <summary>
        ''' Distribuye verticalmente de forma ordenada los botones principales visibles del sidebar adaptándose al modo colapsado/desplegado.
        ''' </summary>
        Private Sub ReorganizarBotonesMenu()
            Dim intPosicionY As Integer = 110
            Dim arrBotones = {btnNavCliente, btnNavCocina, btnNavCobroAdmin, btnNavFacturacion, btnNavMetricas, btnNavCatalogo}
            For Each btn In arrBotones
                If btn IsNot Nothing AndAlso btn.Visible Then
                    btn.Location = New Point(6, intPosicionY)
                    btn.Width = If(_sidebarColapsado, 58, 238)
                    intPosicionY += 56
                End If
            Next

            If btnCerrarSesion IsNot Nothing AndAlso btnCerrarSesion.Visible Then
                btnCerrarSesion.Location = New Point(6, pnlSidebar.Height - 110)
                btnCerrarSesion.Width = If(_sidebarColapsado, 58, 238)
            End If
        End Sub

        ''' <summary>
        ''' Evento Clic del Botón Hamburguesa para desplegar o retraer el menú lateral.
        ''' </summary>
        Private Sub btnToggleSidebar_Click(sender As Object, e As EventArgs) Handles btnToggleSidebar.Click
            _sidebarColapsado = Not _sidebarColapsado
            AplicarEstadoSidebar()
        End Sub

        ''' <summary>
        ''' Aplica la transformación visual del sidebar ocultándolo por completo (100% pantalla completa) o mostrando el menú lateral (250px).
        ''' </summary>
        Public Sub AplicarEstadoSidebar()
            If pnlSidebar Is Nothing OrElse pnlTopBar Is Nothing Then Return
            pnlSidebar.SuspendLayout()
            pnlTopBar.SuspendLayout()

            If _sidebarColapsado Then
                pnlSidebar.Visible = False
            Else
                pnlSidebar.Width = 250
                pnlSidebar.Visible = True
                AplicarPermisosPorRol()
            End If

            pnlSidebar.ResumeLayout(True)
            pnlTopBar.ResumeLayout(True)

            ' Notificar al formulario activo para que adapte su diseño responsivo
            If _formularioActivo IsNot Nothing Then
                _formularioActivo.PerformLayout()
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

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            If pnlSidebar Is Nothing OrElse pnlTopBar Is Nothing Then Return

            pnlSidebar.BackColor = ThemeConfig.ColorBackgroundSidebar
            pnlTopBar.BackColor = ThemeConfig.ColorBackgroundCard
            If pnlContenedorPrincipal IsNot Nothing Then pnlContenedorPrincipal.BackColor = ThemeConfig.ColorBackgroundApp
            If pnlBarraIndicadorMenu IsNot Nothing Then pnlBarraIndicadorMenu.BackColor = ThemeConfig.ColorPrimary

            ' Estilizar botón Hamburguesa Táctil
            If btnToggleSidebar IsNot Nothing Then
                btnToggleSidebar.FlatStyle = FlatStyle.Flat
                btnToggleSidebar.FlatAppearance.BorderSize = 0
                btnToggleSidebar.BackColor = ThemeConfig.ColorPrimaryLight
                btnToggleSidebar.ForeColor = ThemeConfig.ColorPrimary
                btnToggleSidebar.Cursor = Cursors.Hand
            End If

            ' Identidad visual del restaurante en sidebar
            If pnlLogoEmblema IsNot Nothing Then pnlLogoEmblema.BackColor = ThemeConfig.ColorPrimary
            If lblLogoEmblemaTexto IsNot Nothing Then lblLogoEmblemaTexto.ForeColor = Color.White
            If lblNombreRestaurante IsNot Nothing Then
                lblNombreRestaurante.ForeColor = ThemeConfig.ColorNeutralDark
                lblNombreRestaurante.Font = ThemeConfig.ObtenerFuenteTitulo(12.5F, FontStyle.Bold)
            End If
            If lblEsloganRestaurante IsNot Nothing Then
                lblEsloganRestaurante.ForeColor = ThemeConfig.ColorPrimary
                lblEsloganRestaurante.Font = ThemeConfig.ObtenerFuenteCuerpo(7.5F, FontStyle.Bold)
            End If
            If pnlLogoSeparador IsNot Nothing Then pnlLogoSeparador.BackColor = ThemeConfig.ColorBorder

            ' Barra superior y perfil de usuario
            If lblTituloModuloTop IsNot Nothing Then
                lblTituloModuloTop.ForeColor = ThemeConfig.ColorNeutralDark
                lblTituloModuloTop.Font = ThemeConfig.ObtenerFuenteTitulo(15.0F, FontStyle.Bold)
            End If

            If lblFechaHoraSistema IsNot Nothing Then
                lblFechaHoraSistema.ForeColor = ThemeConfig.ColorNeutralDark
                lblFechaHoraSistema.Font = ThemeConfig.ObtenerFuenteSubtitulo(9.5F, FontStyle.Bold)
            End If

            If pnlAvatar IsNot Nothing Then pnlAvatar.BackColor = ThemeConfig.ColorPrimaryLight
            If lblAvatarIcono IsNot Nothing Then lblAvatarIcono.ForeColor = ThemeConfig.ColorPrimary

            If pnlSidebarFooter IsNot Nothing Then pnlSidebarFooter.BackColor = ThemeConfig.ColorBackgroundSidebar
            If lblEstadoBaseDatos IsNot Nothing Then lblEstadoBaseDatos.ForeColor = ThemeConfig.ColorTertiarySuccess

            If lblNombreUsuario IsNot Nothing Then lblNombreUsuario.Text = _nombreUsuario
            If lblRolUsuario IsNot Nothing Then lblRolUsuario.Text = Models.RolUsuarioExtensions.ObtenerEtiqueta(Models.RolUsuarioExtensions.ParsearRol(_rolUsuario))

            If btnAccesoPersonal IsNot Nothing Then ThemeConfig.EstilizarBotonPrimario(btnAccesoPersonal)
            If btnCerrarSesion IsNot Nothing Then ThemeConfig.EstilizarBotonEliminar(btnCerrarSesion)

            Dim botonesNav = {btnNavCliente, btnNavCocina, btnNavCobroAdmin, btnNavFacturacion, btnNavMetricas, btnNavCatalogo}
            For Each btn In botonesNav
                If btn IsNot Nothing Then
                    ThemeConfig.EstilizarBotonNavegacion(btn, False)
                End If
            Next
        End Sub


        Private Sub SeleccionarBotonNavegacion(btnSeleccionado As Button, tituloModulo As String)
            If btnSeleccionado Is Nothing Then Return

            Dim botonesNav = {btnNavCliente, btnNavCocina, btnNavCobroAdmin, btnNavFacturacion, btnNavMetricas, btnNavCatalogo}
            For Each btn In botonesNav
                If btn IsNot Nothing Then
                    ThemeConfig.EstilizarBotonNavegacion(btn, False)
                End If
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

        Private Sub btnNavCliente_Click(sender As Object, e As EventArgs) Handles btnNavCliente.Click
            Dim tituloVista As String = If(_rolUsuario.Contains("Cliente"), "Carta & Menú Digital", "Carta & Pedidos")
            SeleccionarBotonNavegacion(btnNavCliente, tituloVista)
            AbrirFormularioEnPanel(Of Pedidos.FrmClienteMenu)()
        End Sub

        Private Sub btnNavCocina_Click(sender As Object, e As EventArgs) Handles btnNavCocina.Click
            SeleccionarBotonNavegacion(btnNavCocina, "Monitor de Cocina (KDS)")
            AbrirFormularioEnPanel(Of Cocina.FrmCcnMonitorCocina)()
        End Sub

        Private Sub btnNavCobroAdmin_Click(sender As Object, e As EventArgs) Handles btnNavCobroAdmin.Click
            Dim tituloVista As String = If(_rolUsuario.Contains("Cajero"), "Caja & Procesamiento de Pagos", "Caja y Administración")
            SeleccionarBotonNavegacion(btnNavCobroAdmin, tituloVista)
            AbrirFormularioEnPanel(Of Caja.FrmCajaCobros)()
        End Sub

        Private Sub btnNavFacturacion_Click(sender As Object, e As EventArgs) Handles btnNavFacturacion.Click
            SeleccionarBotonNavegacion(btnNavFacturacion, "Historial de Facturación & Archivo Fiscal")
            AbrirFormularioEnPanel(Of Facturacion.FrmFacturacionPDF)()
        End Sub

        Private Sub btnNavMetricas_Click(sender As Object, e As EventArgs) Handles btnNavMetricas.Click
            SeleccionarBotonNavegacion(btnNavMetricas, "Métricas & Rendimiento del Negocio")
            AbrirFormularioEnPanel(Of Dashboards.FrmDashboardGeneral)()
        End Sub

        Private Sub btnNavCatalogo_Click(sender As Object, e As EventArgs) Handles btnNavCatalogo.Click
            SeleccionarBotonNavegacion(btnNavCatalogo, "Gestión de Menú y Catálogo de Platos")
            AbrirFormularioEnPanel(Of Catalogo.FrmCatalogo)()
        End Sub

        Private Sub tmrRelojSistema_Tick(sender As Object, e As EventArgs) Handles tmrRelojSistema.Tick
            ActualizarRelojSistema()
        End Sub

        Private Sub ActualizarRelojSistema()
            Dim ahora As DateTime = DateTime.Now
            Dim diaSemana As String = ahora.ToString("dddd", New System.Globalization.CultureInfo("es-ES"))
            diaSemana = Char.ToUpper(diaSemana(0)) & diaSemana.Substring(1)
            lblFechaHoraSistema.Text = $"{diaSemana}, {ahora:dd MMM} • {ahora:HH:mm:ss}"
        End Sub

        ''' <summary>
        ''' Despliega la pantalla informativa temporal para la Carta Digital del Cliente.
        ''' </summary>
        Private Sub MostrarModuloClienteEnConstruccion()
            If _formularioActivo IsNot Nothing Then
                _formularioActivo.Close()
                _formularioActivo.Dispose()
                _formularioActivo = Nothing
            End If

            Dim pnlContenedor As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = ThemeConfig.ColorBackgroundApp
            }

            Dim pnlTarjeta As New Panel With {
                .Size = New Size(560, 290),
                .BackColor = Color.White
            }
            pnlTarjeta.Location = New Point(Math.Max(20, (pnlContenedorPrincipal.Width - pnlTarjeta.Width) \ 2),
                                            Math.Max(20, (pnlContenedorPrincipal.Height - pnlTarjeta.Height) \ 2))
            ThemeConfig.AplicarEstiloTarjeta(pnlTarjeta)

            Dim lblIcono As New Label With {
                .Text = "🍽️",
                .Font = New Font("Segoe UI Emoji", 36.0F, FontStyle.Regular),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Top,
                .Height = 70
            }

            Dim lblTitulo As New Label With {
                .Text = "Carta & Menú Digital",
                .Font = ThemeConfig.ObtenerFuenteTitulo(15.0F, FontStyle.Bold),
                .ForeColor = ThemeConfig.ColorNeutralDark,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Top,
                .Height = 35
            }

            Dim lblEstadoBadge As New Label With {
                .Text = "En producción. Pronto estará disponible.",
                .Font = ThemeConfig.ObtenerFuenteSubtitulo(12.0F, FontStyle.Bold),
                .ForeColor = ThemeConfig.ColorPrimary,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Top,
                .Height = 35
            }

            Dim lblDetalle As New Label With {
                .Text = "Esta sección se encuentra actualmente en desarrollo y se diseñará próximamente para la atención digital de pedidos de los clientes.",
                .Font = ThemeConfig.ObtenerFuenteSubtitulo(10.0F, FontStyle.Regular),
                .ForeColor = ThemeConfig.ColorTextMuted,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Fill,
                .Padding = New Padding(25, 5, 25, 10)
            }

            pnlTarjeta.Controls.Add(lblDetalle)
            pnlTarjeta.Controls.Add(lblEstadoBadge)
            pnlTarjeta.Controls.Add(lblTitulo)
            pnlTarjeta.Controls.Add(lblIcono)

            pnlContenedor.Controls.Add(pnlTarjeta)

            AddHandler pnlContenedor.Resize, Sub(s, ev)
                                                 pnlTarjeta.Location = New Point(Math.Max(20, (pnlContenedor.Width - pnlTarjeta.Width) \ 2),
                                                                                 Math.Max(20, (pnlContenedor.Height - pnlTarjeta.Height) \ 2))
                                             End Sub

            pnlContenedorPrincipal.SuspendLayout()
            pnlContenedorPrincipal.Controls.Clear()
            pnlContenedorPrincipal.Controls.Add(pnlContenedor)
            pnlContenedorPrincipal.ResumeLayout(True)
        End Sub

        ''' <summary>
        ''' Abre el modal de inicio de sesión para que el personal (Cocina, Caja, Admin) acceda a sus paneles.
        ''' </summary>
        Private Sub btnAccesoPersonal_Click(sender As Object, e As EventArgs) Handles btnAccesoPersonal.Click
            Using frmLog As New Auth.FrmLogin()
                If frmLog.ShowDialog(Me) = DialogResult.OK Then
                    EstablecerSesion(frmLog.RolAutenticado, frmLog.UsuarioAutenticado)
                End If
            End Using
        End Sub

        ''' <summary>
        ''' Cierra la sesión activa del personal y regresa a la Carta & Menú Digital del Cliente.
        ''' </summary>
        Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
            If ConfirmarAccion("¿Desea cerrar la sesión de personal y regresar a la Carta & Menú Digital?", "Cerrar Sesión de Personal") Then
                EstablecerSesion("Cliente", "Invitado")
            End If
        End Sub

    End Class

End Namespace
