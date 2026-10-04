Imports System.Drawing
Imports System.Windows.Forms

Namespace Theme
    ''' <summary>
    ''' Configuración centralizada de diseño, tipografía y paleta de colores oficial del sistema.
    ''' Basada en el sistema de diseño moderno con tonos tierra, terracotta y sage green.
    ''' </summary>
    Public Module ThemeConfig

        ' =========================================================================
        ' PALETA DE COLORES OFICIAL (HEX & RGB)
        ' =========================================================================

        ''' <summary> Color Primario Terracota Cálido (#C66B48) </summary>
        Public ReadOnly ColorPrimary As Color = Color.FromArgb(198, 107, 72)

        ''' <summary> Color Primario Oscuro para Hover (#A55234) </summary>
        Public ReadOnly ColorPrimaryDark As Color = Color.FromArgb(165, 82, 52)

        ''' <summary> Color Primario Claro / Soft Badge (#F8ECE7) </summary>
        Public ReadOnly ColorPrimaryLight As Color = Color.FromArgb(248, 236, 231)

        ''' <summary> Color Secundario Marrón Oscuro (#754632) </summary>
        Public ReadOnly ColorSecondary As Color = Color.FromArgb(117, 70, 50)

        ''' <summary> Color Terciario / Estado Exitoso - Verde Sabio / Oliva (#596B4B) </summary>
        Public ReadOnly ColorTertiarySuccess As Color = Color.FromArgb(89, 107, 75)

        ''' <summary> Color Terciario Soft - Fondo de Badges Verdes (#E4EDE0) </summary>
        Public ReadOnly ColorTertiaryLight As Color = Color.FromArgb(228, 237, 224)

        ''' <summary> Neutral Oscuro para Textos Principales (#292B26) </summary>
        Public ReadOnly ColorNeutralDark As Color = Color.FromArgb(41, 43, 38)

        ''' <summary> Fondo Principal del Sistema - Crema Claro (#F4F3ED) </summary>
        Public ReadOnly ColorBackgroundApp As Color = Color.FromArgb(244, 243, 237)

        ''' <summary> Fondo de Menú Lateral / Sidebar (#EFECE6) </summary>
        Public ReadOnly ColorBackgroundSidebar As Color = Color.FromArgb(239, 236, 230)

        ''' <summary> Fondo de Tarjetas y Paneles Blancos (#FFFFFF) </summary>
        Public ReadOnly ColorBackgroundCard As Color = Color.FromArgb(255, 255, 255)

        ''' <summary> Color de Bordes y Límite de Separación (#E2DDD5) </summary>
        Public ReadOnly ColorBorder As Color = Color.FromArgb(226, 221, 213)

        ''' <summary> Texto Secundario y Subtítulos (#787773) </summary>
        Public ReadOnly ColorTextMuted As Color = Color.FromArgb(120, 119, 115)

        ''' <summary> Color de Advertencia / Alerta (#D97724) </summary>
        Public ReadOnly ColorWarning As Color = Color.FromArgb(217, 119, 36)

        ''' <summary> Color Crítico / Error (#B93829) </summary>
        Public ReadOnly ColorDanger As Color = Color.FromArgb(185, 56, 41)

        ''' <summary> Soft Danger Badge (#FCE8E6) </summary>
        Public ReadOnly ColorDangerLight As Color = Color.FromArgb(252, 232, 230)

        ' =========================================================================
        ' TIPOGRAFÍAS DEL SISTEMA (Fallback seguro a Segoe UI si no está Plus Jakarta Sans)
        ' =========================================================================
        Public Function ObtenerFuenteTitulo(Optional tamaño As Single = 16.0F, Optional estilo As FontStyle = FontStyle.Bold) As Font
            Return New Font("Segoe UI", tamaño, estilo)
        End Function

        Public Function ObtenerFuenteSubtitulo(Optional tamaño As Single = 11.0F, Optional estilo As FontStyle = FontStyle.Bold) As Font
            Return New Font("Segoe UI", tamaño, estilo)
        End Function

        Public Function ObtenerFuenteCuerpo(Optional tamaño As Single = 9.5F, Optional estilo As FontStyle = FontStyle.Regular) As Font
            Return New Font("Segoe UI", tamaño, estilo)
        End Function

        ' =========================================================================
        ' HELPERS DE ESTILIZADO DE CONTROLES
        ' =========================================================================

        ''' <summary>
        ''' Habilita DoubleBuffered recursivamente en un control y sus hijos para eliminar parpadeos en WinForms.
        ''' </summary>
        Public Sub HabilitarDobleBuffer(control As Control)
            Try
                Dim pi = GetType(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
                If pi IsNot Nothing Then
                    pi.SetValue(control, True, Nothing)
                End If

                For Each child As Control In control.Controls
                    HabilitarDobleBuffer(child)
                Next
            Catch ex As Exception
                ' Ignorar en entornos de diseño restringidos
            End Try
        End Sub

        ''' <summary>
        ''' Estiliza una tarjeta / card de contenido con bordes limpios y fondo blanco.
        ''' </summary>
        Public Sub AplicarEstiloTarjeta(pnl As Panel)
            pnl.BackColor = ColorBackgroundCard
            pnl.Padding = New Padding(16)
        End Sub

        ''' <summary>
        ''' Estiliza un botón de navegación del Sidebar.
        ''' </summary>
        Public Sub EstilizarBotonNavegacion(btn As Button, esActivo As Boolean)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.Cursor = Cursors.Hand
            btn.TextAlign = ContentAlignment.MiddleLeft
            btn.ImageAlign = ContentAlignment.MiddleLeft
            btn.Padding = New Padding(12, 0, 8, 0)
            btn.Font = ObtenerFuenteCuerpo(9.0F, If(esActivo, FontStyle.Bold, FontStyle.Regular))

            If esActivo Then
                btn.BackColor = Color.FromArgb(230, 224, 215)
                btn.ForeColor = ColorNeutralDark
            Else
                btn.BackColor = ColorBackgroundSidebar
                btn.ForeColor = ColorTextMuted
            End If
        End Sub

        ''' <summary>
        ''' Estiliza un botón primario con color terracota.
        ''' </summary>
        Public Sub EstilizarBotonPrimario(btn As Button)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.BackColor = ColorPrimary
            btn.ForeColor = Color.White
            btn.Font = ObtenerFuenteCuerpo(9.5F, FontStyle.Bold)
            btn.Cursor = Cursors.Hand
        End Sub

        ''' <summary>
        ''' Estiliza un botón secundario / delineado.
        ''' </summary>
        Public Sub EstilizarBotonSecundario(btn As Button)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderColor = ColorBorder
            btn.FlatAppearance.BorderSize = 1
            btn.BackColor = ColorBackgroundCard
            btn.ForeColor = ColorNeutralDark
            btn.Font = ObtenerFuenteCuerpo(9.0F, FontStyle.Bold)
            btn.Cursor = Cursors.Hand
        End Sub

        ''' <summary>
        ''' Estiliza un botón de eliminación / peligro en color rojo suave.
        ''' </summary>
        Public Sub EstilizarBotonEliminar(btn As Button)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.BackColor = Color.FromArgb(190, 60, 60)
            btn.ForeColor = Color.White
            btn.Font = ObtenerFuenteCuerpo(9.0F, FontStyle.Bold)
            btn.Cursor = Cursors.Hand
        End Sub

        ''' <summary>
        ''' Estiliza un Label como Badge / Pill.
        ''' </summary>
        Public Sub EstilizarBadge(lbl As Label, fondo As Color, texto As Color)

            lbl.BackColor = fondo
            lbl.ForeColor = texto
            lbl.Font = ObtenerFuenteCuerpo(8.5F, FontStyle.Bold)
            lbl.TextAlign = ContentAlignment.MiddleCenter
        End Sub

    End Module
End Namespace
