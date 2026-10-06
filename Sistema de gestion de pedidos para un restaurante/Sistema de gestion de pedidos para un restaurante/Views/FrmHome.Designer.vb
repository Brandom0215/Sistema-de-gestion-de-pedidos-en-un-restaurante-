Imports System.Drawing
Imports System.Windows.Forms

Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmHome
        Inherits Common.FrmBaseForm

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
    Friend WithEvents pnlLogoEmblema As System.Windows.Forms.Panel
    Friend WithEvents lblLogoEmblemaTexto As System.Windows.Forms.Label
    Friend WithEvents lblNombreRestaurante As System.Windows.Forms.Label
    Friend WithEvents lblEsloganRestaurante As System.Windows.Forms.Label
    Friend WithEvents pnlLogoSeparador As System.Windows.Forms.Panel
    Friend WithEvents pnlBarraIndicadorMenu As System.Windows.Forms.Panel

    ' Botones de Navegación del Sistema (Reorganizados según Rol)
    Friend WithEvents btnNavCliente As System.Windows.Forms.Button
    Friend WithEvents btnNavCocina As System.Windows.Forms.Button
    Friend WithEvents btnNavCobroAdmin As System.Windows.Forms.Button
    Friend WithEvents btnNavMetricas As System.Windows.Forms.Button
    Friend WithEvents btnNavCatalogo As System.Windows.Forms.Button

    Friend WithEvents pnlSidebarFooter As System.Windows.Forms.Panel
    Friend WithEvents lblEstadoBaseDatos As System.Windows.Forms.Label

    ' Controles de TopBar
    Friend WithEvents lblTituloModuloTop As System.Windows.Forms.Label
    Friend WithEvents lblFechaHoraSistema As System.Windows.Forms.Label
    Friend WithEvents pnlUserProfile As System.Windows.Forms.Panel
    Friend WithEvents pnlAvatar As System.Windows.Forms.Panel
    Friend WithEvents lblAvatarIcono As System.Windows.Forms.Label
    Friend WithEvents lblNombreUsuario As System.Windows.Forms.Label
    Friend WithEvents lblRolUsuario As System.Windows.Forms.Label
    Friend WithEvents btnAccesoPersonal As System.Windows.Forms.Button

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
        Me.pnlLogoEmblema = New System.Windows.Forms.Panel()
        Me.lblLogoEmblemaTexto = New System.Windows.Forms.Label()
        Me.lblNombreRestaurante = New System.Windows.Forms.Label()
        Me.lblEsloganRestaurante = New System.Windows.Forms.Label()
        Me.pnlLogoSeparador = New System.Windows.Forms.Panel()
        Me.pnlBarraIndicadorMenu = New System.Windows.Forms.Panel()

        Me.btnNavCliente = New System.Windows.Forms.Button()
        Me.btnNavCocina = New System.Windows.Forms.Button()
        Me.btnNavCobroAdmin = New System.Windows.Forms.Button()
        Me.btnNavMetricas = New System.Windows.Forms.Button()
        Me.btnNavCatalogo = New System.Windows.Forms.Button()

        Me.pnlSidebarFooter = New System.Windows.Forms.Panel()
        Me.lblEstadoBaseDatos = New System.Windows.Forms.Label()

        Me.lblTituloModuloTop = New System.Windows.Forms.Label()
        Me.lblFechaHoraSistema = New System.Windows.Forms.Label()
        Me.pnlUserProfile = New System.Windows.Forms.Panel()
        Me.pnlAvatar = New System.Windows.Forms.Panel()
        Me.lblAvatarIcono = New System.Windows.Forms.Label()
        Me.lblNombreUsuario = New System.Windows.Forms.Label()
        Me.lblRolUsuario = New System.Windows.Forms.Label()

        Me.btnCerrarSesion = New System.Windows.Forms.Button()
        Me.btnAccesoPersonal = New System.Windows.Forms.Button()
        Me.tmrRelojSistema = New System.Windows.Forms.Timer(Me.components)


        Me.pnlSidebar.SuspendLayout()
        Me.pnlLogoContainer.SuspendLayout()
        Me.pnlLogoEmblema.SuspendLayout()
        Me.pnlSidebarFooter.SuspendLayout()
        Me.pnlTopBar.SuspendLayout()
        Me.pnlUserProfile.SuspendLayout()
        Me.pnlAvatar.SuspendLayout()
        Me.SuspendLayout()

        ' 
        ' pnlSidebar
        ' 
        Me.pnlSidebar.Controls.Add(Me.btnCerrarSesion)
        Me.pnlSidebar.Controls.Add(Me.btnNavCatalogo)
        Me.pnlSidebar.Controls.Add(Me.btnNavMetricas)
        Me.pnlSidebar.Controls.Add(Me.btnNavCobroAdmin)
        Me.pnlSidebar.Controls.Add(Me.btnNavCocina)
        Me.pnlSidebar.Controls.Add(Me.btnNavCliente)
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
        Me.pnlLogoContainer.Controls.Add(Me.pnlLogoSeparador)
        Me.pnlLogoContainer.Controls.Add(Me.lblEsloganRestaurante)
        Me.pnlLogoContainer.Controls.Add(Me.lblNombreRestaurante)
        Me.pnlLogoContainer.Controls.Add(Me.pnlLogoEmblema)
        Me.pnlLogoContainer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLogoContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlLogoContainer.Name = "pnlLogoContainer"
        Me.pnlLogoContainer.Size = New System.Drawing.Size(250, 105)
        Me.pnlLogoContainer.TabIndex = 0

        ' 
        ' pnlLogoEmblema
        ' 
        Me.pnlLogoEmblema.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
        Me.pnlLogoEmblema.Controls.Add(Me.lblLogoEmblemaTexto)
        Me.pnlLogoEmblema.Location = New System.Drawing.Point(16, 20)
        Me.pnlLogoEmblema.Name = "pnlLogoEmblema"
        Me.pnlLogoEmblema.Size = New System.Drawing.Size(42, 42)
        Me.pnlLogoEmblema.TabIndex = 0

        ' 
        ' lblLogoEmblemaTexto
        ' 
        Me.lblLogoEmblemaTexto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLogoEmblemaTexto.Font = New System.Drawing.Font("Georgia", 13.0F, System.Drawing.FontStyle.Bold)
        Me.lblLogoEmblemaTexto.ForeColor = System.Drawing.Color.White
        Me.lblLogoEmblemaTexto.Location = New System.Drawing.Point(0, 0)
        Me.lblLogoEmblemaTexto.Name = "lblLogoEmblemaTexto"
        Me.lblLogoEmblemaTexto.Size = New System.Drawing.Size(42, 42)
        Me.lblLogoEmblemaTexto.Text = "BS"
        Me.lblLogoEmblemaTexto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLogoEmblemaTexto.UseMnemonic = False

        ' 
        ' lblNombreRestaurante
        ' 
        Me.lblNombreRestaurante.AutoSize = True
        Me.lblNombreRestaurante.Font = New System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold)
        Me.lblNombreRestaurante.Location = New System.Drawing.Point(65, 18)
        Me.lblNombreRestaurante.Name = "lblNombreRestaurante"
        Me.lblNombreRestaurante.Size = New System.Drawing.Size(165, 25)
        Me.lblNombreRestaurante.Text = "EL BUEN SAZÓN"
        Me.lblNombreRestaurante.UseMnemonic = False

        ' 
        ' lblEsloganRestaurante
        ' 
        Me.lblEsloganRestaurante.AutoSize = True
        Me.lblEsloganRestaurante.Font = New System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold)
        Me.lblEsloganRestaurante.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
        Me.lblEsloganRestaurante.Location = New System.Drawing.Point(67, 44)
        Me.lblEsloganRestaurante.Name = "lblEsloganRestaurante"
        Me.lblEsloganRestaurante.Size = New System.Drawing.Size(155, 15)
        Me.lblEsloganRestaurante.Text = "GASTRONOMÍA & SABOR"
        Me.lblEsloganRestaurante.UseMnemonic = False

        ' 
        ' pnlLogoSeparador
        ' 
        Me.pnlLogoSeparador.BackColor = System.Drawing.Color.FromArgb(226, 221, 213)
        Me.pnlLogoSeparador.Location = New System.Drawing.Point(14, 85)
        Me.pnlLogoSeparador.Name = "pnlLogoSeparador"
        Me.pnlLogoSeparador.Size = New System.Drawing.Size(222, 1)
        Me.pnlLogoSeparador.TabIndex = 1


        ' 
        ' pnlBarraIndicadorMenu
        ' 
        Me.pnlBarraIndicadorMenu.Location = New System.Drawing.Point(0, 100)
        Me.pnlBarraIndicadorMenu.Name = "pnlBarraIndicadorMenu"
        Me.pnlBarraIndicadorMenu.Size = New System.Drawing.Size(5, 45)
        Me.pnlBarraIndicadorMenu.TabIndex = 1

        ' 
        ' btnNavCliente
        ' 
        Me.btnNavCliente.Location = New System.Drawing.Point(6, 110)
        Me.btnNavCliente.Name = "btnNavCliente"
        Me.btnNavCliente.Size = New System.Drawing.Size(238, 48)
        Me.btnNavCliente.TabIndex = 2
        Me.btnNavCliente.Text = "  📲 Módulo Cliente"
        Me.btnNavCliente.UseVisualStyleBackColor = True
        Me.btnNavCliente.UseMnemonic = False

        ' 
        ' btnNavCocina
        ' 
        Me.btnNavCocina.Location = New System.Drawing.Point(6, 168)
        Me.btnNavCocina.Name = "btnNavCocina"
        Me.btnNavCocina.Size = New System.Drawing.Size(238, 48)
        Me.btnNavCocina.TabIndex = 3
        Me.btnNavCocina.Text = "  👨‍🍳 Módulo Cocina"
        Me.btnNavCocina.UseVisualStyleBackColor = True
        Me.btnNavCocina.UseMnemonic = False

        ' 
        ' btnNavCobroAdmin
        ' 
        Me.btnNavCobroAdmin.Location = New System.Drawing.Point(6, 226)
        Me.btnNavCobroAdmin.Name = "btnNavCobroAdmin"
        Me.btnNavCobroAdmin.Size = New System.Drawing.Size(238, 48)
        Me.btnNavCobroAdmin.TabIndex = 4
        Me.btnNavCobroAdmin.Text = "  💵 Módulo Cobro / Admin"
        Me.btnNavCobroAdmin.UseVisualStyleBackColor = True
        Me.btnNavCobroAdmin.UseMnemonic = False

        ' 
        ' btnNavMetricas
        ' 
        Me.btnNavMetricas.Location = New System.Drawing.Point(6, 110)
        Me.btnNavMetricas.Name = "btnNavMetricas"
        Me.btnNavMetricas.Size = New System.Drawing.Size(238, 48)
        Me.btnNavMetricas.TabIndex = 5
        Me.btnNavMetricas.Text = "  📊 Métricas & Ingresos"
        Me.btnNavMetricas.UseVisualStyleBackColor = True
        Me.btnNavMetricas.UseMnemonic = False
        Me.btnNavMetricas.Visible = False

        ' 
        ' btnNavCatalogo
        ' 
        Me.btnNavCatalogo.Location = New System.Drawing.Point(6, 168)
        Me.btnNavCatalogo.Name = "btnNavCatalogo"
        Me.btnNavCatalogo.Size = New System.Drawing.Size(238, 48)
        Me.btnNavCatalogo.TabIndex = 6
        Me.btnNavCatalogo.Text = "  🍽️ Menú & Platos"
        Me.btnNavCatalogo.UseVisualStyleBackColor = True
        Me.btnNavCatalogo.UseMnemonic = False
        Me.btnNavCatalogo.Visible = False

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
        Me.lblEstadoBaseDatos.Text = "● Sistema: Listo (En Memoria)"
        Me.lblEstadoBaseDatos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblEstadoBaseDatos.UseMnemonic = False

        ' 
        ' pnlTopBar
        ' 
        Me.pnlTopBar.Controls.Add(Me.btnAccesoPersonal)
        Me.pnlTopBar.Controls.Add(Me.lblTituloModuloTop)
        Me.pnlTopBar.Controls.Add(Me.pnlUserProfile)
        Me.pnlTopBar.Controls.Add(Me.lblFechaHoraSistema)
        Me.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopBar.Location = New System.Drawing.Point(250, 0)
        Me.pnlTopBar.Name = "pnlTopBar"
        Me.pnlTopBar.Size = New System.Drawing.Size(1000, 75)
        Me.pnlTopBar.TabIndex = 1
        ' 
        ' btnAccesoPersonal
        ' 
        Me.btnAccesoPersonal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAccesoPersonal.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.btnAccesoPersonal.Location = New System.Drawing.Point(400, 18)
        Me.btnAccesoPersonal.Name = "btnAccesoPersonal"
        Me.btnAccesoPersonal.Size = New System.Drawing.Size(175, 40)
        Me.btnAccesoPersonal.TabIndex = 3
        Me.btnAccesoPersonal.Text = "Acceso Personal"
        Me.btnAccesoPersonal.UseVisualStyleBackColor = True
        Me.btnAccesoPersonal.UseMnemonic = False
        ' 
        ' lblTituloModuloTop
        ' 
        Me.lblTituloModuloTop.AutoSize = True
        Me.lblTituloModuloTop.Font = New System.Drawing.Font("Segoe UI", 15.0F, System.Drawing.FontStyle.Bold)
        Me.lblTituloModuloTop.Location = New System.Drawing.Point(22, 20)
        Me.lblTituloModuloTop.Name = "lblTituloModuloTop"
        Me.lblTituloModuloTop.Size = New System.Drawing.Size(265, 32)
        Me.lblTituloModuloTop.Text = "Carta & Menú Digital"
        Me.lblTituloModuloTop.UseMnemonic = False
        ' 
        ' lblFechaHoraSistema
        ' 
        Me.lblFechaHoraSistema.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFechaHoraSistema.AutoSize = True
        Me.lblFechaHoraSistema.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblFechaHoraSistema.Location = New System.Drawing.Point(595, 26)
        Me.lblFechaHoraSistema.Name = "lblFechaHoraSistema"
        Me.lblFechaHoraSistema.Size = New System.Drawing.Size(185, 21)
        Me.lblFechaHoraSistema.Text = "Lunes, 05 oct. • 16:00:00"
        ' 
        ' pnlUserProfile
        ' 
        Me.pnlUserProfile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlUserProfile.Controls.Add(Me.lblRolUsuario)
        Me.pnlUserProfile.Controls.Add(Me.lblNombreUsuario)
        Me.pnlUserProfile.Controls.Add(Me.pnlAvatar)
        Me.pnlUserProfile.Location = New System.Drawing.Point(795, 12)
        Me.pnlUserProfile.Name = "pnlUserProfile"
        Me.pnlUserProfile.Size = New System.Drawing.Size(190, 52)
        ' 
        ' pnlAvatar
        ' 
        Me.pnlAvatar.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
        Me.pnlAvatar.Controls.Add(Me.lblAvatarIcono)
        Me.pnlAvatar.Location = New System.Drawing.Point(4, 8)
        Me.pnlAvatar.Name = "pnlAvatar"
        Me.pnlAvatar.Size = New System.Drawing.Size(36, 36)
        Me.pnlAvatar.TabIndex = 0
        ' 
        ' lblAvatarIcono
        ' 
        Me.lblAvatarIcono.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAvatarIcono.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
        Me.lblAvatarIcono.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
        Me.lblAvatarIcono.Location = New System.Drawing.Point(0, 0)
        Me.lblAvatarIcono.Name = "lblAvatarIcono"
        Me.lblAvatarIcono.Size = New System.Drawing.Size(36, 36)
        Me.lblAvatarIcono.Text = "I"
        Me.lblAvatarIcono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAvatarIcono.UseMnemonic = False
        ' 
        ' lblNombreUsuario
        ' 
        Me.lblNombreUsuario.AutoSize = True
        Me.lblNombreUsuario.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
        Me.lblNombreUsuario.Location = New System.Drawing.Point(46, 6)
        Me.lblNombreUsuario.Name = "lblNombreUsuario"
        Me.lblNombreUsuario.Size = New System.Drawing.Size(70, 21)
        Me.lblNombreUsuario.Text = "Invitado"
        ' 
        ' lblRolUsuario
        ' 
        Me.lblRolUsuario.AutoSize = True
        Me.lblRolUsuario.Font = New System.Drawing.Font("Segoe UI", 8.0F)
        Me.lblRolUsuario.ForeColor = System.Drawing.Color.Gray
        Me.lblRolUsuario.Location = New System.Drawing.Point(46, 27)
        Me.lblRolUsuario.Name = "lblRolUsuario"
        Me.lblRolUsuario.Size = New System.Drawing.Size(130, 19)
        Me.lblRolUsuario.Text = "Cliente (Autoatención)"

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
