Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmHome
        Inherits System.Windows.Forms.Form

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        ' Contenedores Principales
        Friend WithEvents pnlSidebar As System.Windows.Forms.Panel
        Friend WithEvents pnlTopBar As System.Windows.Forms.Panel
        Friend WithEvents pnlContenedorPrincipal As System.Windows.Forms.Panel

        ' Controles de Sidebar
        Friend WithEvents pnlLogoContainer As System.Windows.Forms.Panel
        Friend WithEvents lblNombreRestaurante As System.Windows.Forms.Label
        Friend WithEvents lblEsloganRestaurante As System.Windows.Forms.Label
        Friend WithEvents pnlBarraIndicadorMenu As System.Windows.Forms.Panel

        ' Botones de Navegación del Sistema (Basados en Documentacion.md)
        Friend WithEvents btnNavDashboard As System.Windows.Forms.Button
        Friend WithEvents btnNavCatalogo As System.Windows.Forms.Button
        Friend WithEvents btnNavPedidos As System.Windows.Forms.Button
        Friend WithEvents btnNavCocina As System.Windows.Forms.Button
        Friend WithEvents btnNavCaja As System.Windows.Forms.Button
        Friend WithEvents btnNavFacturacion As System.Windows.Forms.Button
        Friend WithEvents btnNavReportes As System.Windows.Forms.Button

        Friend WithEvents pnlSidebarFooter As System.Windows.Forms.Panel
        Friend WithEvents lblEstadoBaseDatos As System.Windows.Forms.Label

        ' Controles de TopBar
        Friend WithEvents lblTituloModuloTop As System.Windows.Forms.Label
        Friend WithEvents lblFechaHoraSistema As System.Windows.Forms.Label
        Friend WithEvents pnlUserProfile As System.Windows.Forms.Panel
        Friend WithEvents lblNombreUsuario As System.Windows.Forms.Label
        Friend WithEvents lblRolUsuario As System.Windows.Forms.Label

        ' Temporizador de Sistema
        Friend WithEvents tmrRelojSistema As System.Windows.Forms.Timer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            
            ' Instanciación
            Me.pnlSidebar = New System.Windows.Forms.Panel()
            Me.pnlTopBar = New System.Windows.Forms.Panel()
            Me.pnlContenedorPrincipal = New System.Windows.Forms.Panel()
            
            Me.pnlLogoContainer = New System.Windows.Forms.Panel()
            Me.lblNombreRestaurante = New System.Windows.Forms.Label()
            Me.lblEsloganRestaurante = New System.Windows.Forms.Label()
            Me.pnlBarraIndicadorMenu = New System.Windows.Forms.Panel()
            
            Me.btnNavDashboard = New System.Windows.Forms.Button()
            Me.btnNavCatalogo = New System.Windows.Forms.Button()
            Me.btnNavPedidos = New System.Windows.Forms.Button()
            Me.btnNavCocina = New System.Windows.Forms.Button()
            Me.btnNavCaja = New System.Windows.Forms.Button()
            Me.btnNavFacturacion = New System.Windows.Forms.Button()
            Me.btnNavReportes = New System.Windows.Forms.Button()
            
            Me.pnlSidebarFooter = New System.Windows.Forms.Panel()
            Me.lblEstadoBaseDatos = New System.Windows.Forms.Label()
            
            Me.lblTituloModuloTop = New System.Windows.Forms.Label()
            Me.lblFechaHoraSistema = New System.Windows.Forms.Label()
            Me.pnlUserProfile = New System.Windows.Forms.Panel()
            Me.lblNombreUsuario = New System.Windows.Forms.Label()
            Me.lblRolUsuario = New System.Windows.Forms.Label()
            
            Me.btnCerrarSesion = New System.Windows.Forms.Button()
            Me.tmrRelojSistema = New System.Windows.Forms.Timer(Me.components)

            
            Me.pnlSidebar.SuspendLayout()
            Me.pnlLogoContainer.SuspendLayout()
            Me.pnlSidebarFooter.SuspendLayout()
            Me.pnlTopBar.SuspendLayout()
            Me.pnlUserProfile.SuspendLayout()
            Me.SuspendLayout()

            ' 
            ' pnlSidebar
            ' 
            Me.pnlSidebar.Controls.Add(Me.btnCerrarSesion)
            Me.pnlSidebar.Controls.Add(Me.btnNavReportes)
            Me.pnlSidebar.Controls.Add(Me.btnNavFacturacion)
            Me.pnlSidebar.Controls.Add(Me.btnNavCaja)
            Me.pnlSidebar.Controls.Add(Me.btnNavCocina)
            Me.pnlSidebar.Controls.Add(Me.btnNavPedidos)
            Me.pnlSidebar.Controls.Add(Me.btnNavCatalogo)
            Me.pnlSidebar.Controls.Add(Me.btnNavDashboard)
            Me.pnlSidebar.Controls.Add(Me.pnlBarraIndicadorMenu)
            Me.pnlSidebar.Controls.Add(Me.pnlSidebarFooter)
            Me.pnlSidebar.Controls.Add(Me.pnlLogoContainer)
            Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlSidebar.Location = New System.Drawing.Point(0, 0)
            Me.pnlSidebar.Name = "pnlSidebar"
            Me.pnlSidebar.Size = New System.Drawing.Size(250, 750)
            Me.pnlSidebar.TabIndex = 0


            ' 
            ' pnlLogoContainer
            ' 
            Me.pnlLogoContainer.Controls.Add(Me.lblEsloganRestaurante)
            Me.pnlLogoContainer.Controls.Add(Me.lblNombreRestaurante)
            Me.pnlLogoContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlLogoContainer.Location = New System.Drawing.Point(0, 0)
            Me.pnlLogoContainer.Name = "pnlLogoContainer"
            Me.pnlLogoContainer.Size = New System.Drawing.Size(250, 90)
            Me.pnlLogoContainer.TabIndex = 0

            ' 
            ' lblNombreRestaurante
            ' 
            Me.lblNombreRestaurante.AutoSize = True
            Me.lblNombreRestaurante.Font = New System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombreRestaurante.Location = New System.Drawing.Point(20, 20)
            Me.lblNombreRestaurante.Name = "lblNombreRestaurante"
            Me.lblNombreRestaurante.Size = New System.Drawing.Size(195, 31)
            Me.lblNombreRestaurante.Text = "EL BUEN SAZÓN"
            Me.lblNombreRestaurante.UseMnemonic = False

            ' 
            ' lblEsloganRestaurante
            ' 
            Me.lblEsloganRestaurante.AutoSize = True
            Me.lblEsloganRestaurante.Font = New System.Drawing.Font("Segoe UI", 8.0F, System.Drawing.FontStyle.Bold)
            Me.lblEsloganRestaurante.ForeColor = System.Drawing.Color.Gray
            Me.lblEsloganRestaurante.Location = New System.Drawing.Point(22, 53)
            Me.lblEsloganRestaurante.Name = "lblEsloganRestaurante"
            Me.lblEsloganRestaurante.Size = New System.Drawing.Size(180, 19)
            Me.lblEsloganRestaurante.Text = "GASTRONOMÍA & SABOR"
            Me.lblEsloganRestaurante.UseMnemonic = False


            ' 
            ' pnlBarraIndicadorMenu
            ' 
            Me.pnlBarraIndicadorMenu.Location = New System.Drawing.Point(0, 100)
            Me.pnlBarraIndicadorMenu.Name = "pnlBarraIndicadorMenu"
            Me.pnlBarraIndicadorMenu.Size = New System.Drawing.Size(5, 45)
            Me.pnlBarraIndicadorMenu.TabIndex = 1

            ' 
            ' btnNavDashboard
            ' 
            Me.btnNavDashboard.Location = New System.Drawing.Point(6, 100)
            Me.btnNavDashboard.Name = "btnNavDashboard"
            Me.btnNavDashboard.Size = New System.Drawing.Size(238, 45)
            Me.btnNavDashboard.TabIndex = 2
            Me.btnNavDashboard.Text = "  📊 Dashboard General"
            Me.btnNavDashboard.UseVisualStyleBackColor = True
            Me.btnNavDashboard.UseMnemonic = False

            ' 
            ' btnNavCatalogo
            ' 
            Me.btnNavCatalogo.Location = New System.Drawing.Point(6, 150)
            Me.btnNavCatalogo.Name = "btnNavCatalogo"
            Me.btnNavCatalogo.Size = New System.Drawing.Size(238, 45)
            Me.btnNavCatalogo.TabIndex = 3
            Me.btnNavCatalogo.Text = "  🍽 Menú & Catálogo"
            Me.btnNavCatalogo.UseVisualStyleBackColor = True
            Me.btnNavCatalogo.UseMnemonic = False

            ' 
            ' btnNavPedidos
            ' 
            Me.btnNavPedidos.Location = New System.Drawing.Point(6, 200)
            Me.btnNavPedidos.Name = "btnNavPedidos"
            Me.btnNavPedidos.Size = New System.Drawing.Size(238, 45)
            Me.btnNavPedidos.TabIndex = 4
            Me.btnNavPedidos.Text = "  🛒 Toma de Pedidos"
            Me.btnNavPedidos.UseVisualStyleBackColor = True
            Me.btnNavPedidos.UseMnemonic = False

            ' 
            ' btnNavCocina
            ' 
            Me.btnNavCocina.Location = New System.Drawing.Point(6, 250)
            Me.btnNavCocina.Name = "btnNavCocina"
            Me.btnNavCocina.Size = New System.Drawing.Size(238, 45)
            Me.btnNavCocina.TabIndex = 5
            Me.btnNavCocina.Text = "  👨‍🍳 Monitor Cocina KDS"
            Me.btnNavCocina.UseVisualStyleBackColor = True
            Me.btnNavCocina.UseMnemonic = False

            ' 
            ' btnNavCaja
            ' 
            Me.btnNavCaja.Location = New System.Drawing.Point(6, 300)
            Me.btnNavCaja.Name = "btnNavCaja"
            Me.btnNavCaja.Size = New System.Drawing.Size(238, 45)
            Me.btnNavCaja.TabIndex = 6
            Me.btnNavCaja.Text = "  💵 Caja & Cobros"
            Me.btnNavCaja.UseVisualStyleBackColor = True
            Me.btnNavCaja.UseMnemonic = False

            ' 
            ' btnNavFacturacion
            ' 
            Me.btnNavFacturacion.Location = New System.Drawing.Point(6, 350)
            Me.btnNavFacturacion.Name = "btnNavFacturacion"
            Me.btnNavFacturacion.Size = New System.Drawing.Size(238, 45)
            Me.btnNavFacturacion.TabIndex = 7
            Me.btnNavFacturacion.Text = "  🧾 Facturación & PDF"
            Me.btnNavFacturacion.UseVisualStyleBackColor = True
            Me.btnNavFacturacion.UseMnemonic = False

            ' 
            ' btnNavReportes
            ' 
            Me.btnNavReportes.Location = New System.Drawing.Point(6, 400)
            Me.btnNavReportes.Name = "btnNavReportes"
            Me.btnNavReportes.Size = New System.Drawing.Size(238, 45)
            Me.btnNavReportes.TabIndex = 8
            Me.btnNavReportes.Text = "  📈 Reportes & Ventas"
            Me.btnNavReportes.UseVisualStyleBackColor = True
            Me.btnNavReportes.UseMnemonic = False

            ' 
            ' btnCerrarSesion
            ' 
            Me.btnCerrarSesion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnCerrarSesion.Location = New System.Drawing.Point(6, 640)
            Me.btnCerrarSesion.Name = "btnCerrarSesion"
            Me.btnCerrarSesion.Size = New System.Drawing.Size(238, 42)
            Me.btnCerrarSesion.TabIndex = 10
            Me.btnCerrarSesion.Text = "  🚪 Cerrar Sesión"
            Me.btnCerrarSesion.UseVisualStyleBackColor = True
            Me.btnCerrarSesion.UseMnemonic = False

            ' 
            ' pnlSidebarFooter
            ' 
            Me.pnlSidebarFooter.Controls.Add(Me.lblEstadoBaseDatos)
            Me.pnlSidebarFooter.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlSidebarFooter.Location = New System.Drawing.Point(0, 690)
            Me.pnlSidebarFooter.Name = "pnlSidebarFooter"
            Me.pnlSidebarFooter.Size = New System.Drawing.Size(250, 60)
            Me.pnlSidebarFooter.TabIndex = 9

            ' 
            ' lblEstadoBaseDatos
            ' 
            Me.lblEstadoBaseDatos.Font = New System.Drawing.Font("Segoe UI", 8.5F)
            Me.lblEstadoBaseDatos.Location = New System.Drawing.Point(12, 18)
            Me.lblEstadoBaseDatos.Name = "lblEstadoBaseDatos"
            Me.lblEstadoBaseDatos.Size = New System.Drawing.Size(226, 25)
            Me.lblEstadoBaseDatos.Text = "● PostgreSQL: Conectado"
            Me.lblEstadoBaseDatos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblEstadoBaseDatos.UseMnemonic = False

            ' 
            ' pnlTopBar
            ' 
            Me.pnlTopBar.Controls.Add(Me.lblTituloModuloTop)
            Me.pnlTopBar.Controls.Add(Me.pnlUserProfile)
            Me.pnlTopBar.Controls.Add(Me.lblFechaHoraSistema)
            Me.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlTopBar.Location = New System.Drawing.Point(250, 0)
            Me.pnlTopBar.Name = "pnlTopBar"
            Me.pnlTopBar.Size = New System.Drawing.Size(1000, 75)
            Me.pnlTopBar.TabIndex = 1
            ' 

            ' lblTituloModuloTop
            ' 
            Me.lblTituloModuloTop.AutoSize = True
            Me.lblTituloModuloTop.Font = New System.Drawing.Font("Segoe UI", 14.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloModuloTop.Location = New System.Drawing.Point(20, 20)
            Me.lblTituloModuloTop.Name = "lblTituloModuloTop"
            Me.lblTituloModuloTop.Size = New System.Drawing.Size(220, 32)
            Me.lblTituloModuloTop.Text = "Dashboard General"
            Me.lblTituloModuloTop.UseMnemonic = False

            ' 
            ' lblFechaHoraSistema
            ' 
            Me.lblFechaHoraSistema.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblFechaHoraSistema.AutoSize = True
            Me.lblFechaHoraSistema.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblFechaHoraSistema.Location = New System.Drawing.Point(620, 26)
            Me.lblFechaHoraSistema.Name = "lblFechaHoraSistema"
            Me.lblFechaHoraSistema.Size = New System.Drawing.Size(170, 21)
            Me.lblFechaHoraSistema.Text = "📅 Domingo, 04 Oct • 14:10"

            ' 
            ' pnlUserProfile
            ' 
            Me.pnlUserProfile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pnlUserProfile.Controls.Add(Me.lblRolUsuario)
            Me.pnlUserProfile.Controls.Add(Me.lblNombreUsuario)
            Me.pnlUserProfile.Location = New System.Drawing.Point(820, 12)
            Me.pnlUserProfile.Name = "pnlUserProfile"
            Me.pnlUserProfile.Size = New System.Drawing.Size(165, 50)

            ' 
            ' lblNombreUsuario
            ' 
            Me.lblNombreUsuario.AutoSize = True
            Me.lblNombreUsuario.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblNombreUsuario.Location = New System.Drawing.Point(5, 8)
            Me.lblNombreUsuario.Name = "lblNombreUsuario"
            Me.lblNombreUsuario.Size = New System.Drawing.Size(120, 21)
            Me.lblNombreUsuario.Text = "Administrador"

            ' 
            ' lblRolUsuario
            ' 
            Me.lblRolUsuario.AutoSize = True
            Me.lblRolUsuario.Font = New System.Drawing.Font("Segoe UI", 8.0F)
            Me.lblRolUsuario.ForeColor = System.Drawing.Color.Gray
            Me.lblRolUsuario.Location = New System.Drawing.Point(5, 28)
            Me.lblRolUsuario.Name = "lblRolUsuario"
            Me.lblRolUsuario.Size = New System.Drawing.Size(110, 19)
            Me.lblRolUsuario.Text = "Usuario Activo"

            ' 
            ' pnlContenedorPrincipal
            ' 
            Me.pnlContenedorPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenedorPrincipal.Location = New System.Drawing.Point(250, 75)
            Me.pnlContenedorPrincipal.Name = "pnlContenedorPrincipal"
            Me.pnlContenedorPrincipal.Size = New System.Drawing.Size(1000, 675)
            Me.pnlContenedorPrincipal.TabIndex = 2

            ' 
            ' tmrRelojSistema
            ' 
            Me.tmrRelojSistema.Enabled = True
            Me.tmrRelojSistema.Interval = 1000

            ' 
            ' FrmHome
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0F, 20.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1250, 750)
            Me.Controls.Add(Me.pnlContenedorPrincipal)
            Me.Controls.Add(Me.pnlTopBar)
            Me.Controls.Add(Me.pnlSidebar)
            Me.MinimumSize = New System.Drawing.Size(1100, 680)
            Me.Name = "FrmHome"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Sistema de Gestión de Pedidos de Restaurante"
            Me.pnlSidebar.ResumeLayout(False)
            Me.pnlLogoContainer.ResumeLayout(False)
            Me.pnlLogoContainer.PerformLayout()
            Me.pnlSidebarFooter.ResumeLayout(False)
            Me.pnlTopBar.ResumeLayout(False)
            Me.pnlTopBar.PerformLayout()
            Me.pnlUserProfile.ResumeLayout(False)
            Me.pnlUserProfile.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents btnCerrarSesion As Button
    End Class


End Namespace
