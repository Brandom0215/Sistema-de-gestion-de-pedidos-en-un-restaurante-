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
            btn.Padding = New Padding(16, 0, 8, 0)
            btn.Font = ObtenerFuenteCuerpo(10.0F, If(esActivo, FontStyle.Bold, FontStyle.Regular))

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
            btn.Font = ObtenerFuenteCuerpo(10.5F, FontStyle.Bold)
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
            btn.Font = ObtenerFuenteCuerpo(10.0F, FontStyle.Bold)
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
            btn.Font = ObtenerFuenteCuerpo(10.0F, FontStyle.Bold)
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

        ''' <summary>
        ''' Estiliza una DataGridView optimizándola para pantallas táctiles (Touch POS / KDS).
        ''' Incrementa la altura de filas y encabezados para permitir toque de dedos sin errores.
        ''' </summary>
        Public Sub ConfigurarGrillaTouch(dgv As DataGridView)
            dgv.BackgroundColor = Color.White
            dgv.DefaultCellStyle.SelectionBackColor = ColorPrimaryLight
            dgv.DefaultCellStyle.SelectionForeColor = ColorNeutralDark
            dgv.DefaultCellStyle.Font = ObtenerFuenteCuerpo(10.5F, FontStyle.Regular)
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorSecondary
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = ObtenerFuenteSubtitulo(10.5F, FontStyle.Bold)
            dgv.EnableHeadersVisualStyles = False
            dgv.RowTemplate.Height = 42
            dgv.ColumnHeadersHeight = 40
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        End Sub

        ''' <summary>
        ''' Renderiza una casilla de selección (CheckBox) con un cuadro táctil gigante de 28x28px
        ''' y una marca de verificado (✔) visible, optimizada para interacción con dedos.
        ''' </summary>
        Public Sub AplicarDibujoTouchCheckBox(chk As CheckBox)
            chk.AutoSize = False
            chk.Height = Math.Max(chk.Height, 44)
            chk.Font = ObtenerFuenteSubtitulo(11.5F, FontStyle.Bold)
            chk.Cursor = Cursors.Hand

            ' Forzar invalidez y repintado al cambiar de estado Checked
            AddHandler chk.CheckedChanged, Sub(s As Object, e As EventArgs)
                                                chk.Invalidate()
                                            End Sub

            AddHandler chk.Paint, Sub(sender As Object, e As PaintEventArgs)
                Dim c As CheckBox = CType(sender, CheckBox)
                e.Graphics.Clear(c.BackColor)
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias

                ' Dibujar caja del checkbox táctil de 28x28px
                Dim boxSize As Integer = 28
                Dim boxRect As New Rectangle(2, (c.Height - boxSize) \ 2, boxSize, boxSize)

                If c.Checked Then
                    Using brushFondo As New SolidBrush(ColorPrimary),
                          penBorde As New Pen(ColorPrimaryDark, 2.0F)
                        e.Graphics.FillRectangle(brushFondo, boxRect)
                        e.Graphics.DrawRectangle(penBorde, boxRect)

                        ' Dibujar marca de verificado (✔) en blanco grueso
                        Using penCheck As New Pen(Color.White, 3.5F)
                            e.Graphics.DrawLines(penCheck, {
                                New Point(boxRect.Left + 5, boxRect.Top + 14),
                                New Point(boxRect.Left + 11, boxRect.Top + 21),
                                New Point(boxRect.Left + 22, boxRect.Top + 7)
                            })
                        End Using
                    End Using
                Else
                    Using brushFondo As New SolidBrush(Color.White),
                          penBorde As New Pen(ColorBorder, 2.0F)
                        e.Graphics.FillRectangle(brushFondo, boxRect)
                        e.Graphics.DrawRectangle(penBorde, boxRect)
                    End Using
                End If

                ' Dibujar texto de la etiqueta al lado del cuadro gigante de 28x28px
                Dim textRect As New Rectangle(38, 0, c.Width - 40, c.Height)
                TextRenderer.DrawText(e.Graphics, c.Text, c.Font, textRect, c.ForeColor, TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.WordBreak)
            End Sub
        End Sub

        ''' <summary>
        ''' Reemplaza la apariencia visual de un NumericUpDown por un Stepper Táctil gigante con botones [-] y [+] de 40x40px,
        ''' manteniendo sincronización bidireccional con el control NumericUpDown original y todos sus eventos.
        ''' </summary>
        Public Sub ReemplazarNumericUpDownConTouchStepper(num As NumericUpDown)
            If num Is Nothing OrElse num.Parent Is Nothing Then Return

            Dim parent = num.Parent
            num.Visible = False

            Dim pnlStepper As New Panel With {
                .Size = New Size(130, 42),
                .Location = num.Location,
                .BackColor = Color.White,
                .Tag = "TouchStepper"
            }

            ' Botón Menos [-] (40x40px táctil)
            Dim btnMenos As New Button With {
                .Text = "-",
                .Font = ObtenerFuenteTitulo(15.0F, FontStyle.Bold),
                .Size = New Size(40, 40),
                .Location = New Point(1, 1),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = ColorBackgroundSidebar,
                .ForeColor = ColorNeutralDark,
                .Cursor = Cursors.Hand,
                .Enabled = num.Enabled,
                .UseMnemonic = False
            }
            btnMenos.FlatAppearance.BorderSize = 1
            btnMenos.FlatAppearance.BorderColor = ColorBorder

            ' Label de Valor Centrado (44x40px)
            Dim lblValor As New Label With {
                .Text = num.Value.ToString(),
                .Font = ObtenerFuenteTitulo(13.0F, FontStyle.Bold),
                .Size = New Size(44, 40),
                .Location = New Point(43, 1),
                .TextAlign = ContentAlignment.MiddleCenter,
                .ForeColor = ColorNeutralDark,
                .Enabled = num.Enabled,
                .UseMnemonic = False
            }

            ' Botón Más [+] (40x40px táctil terracota)
            Dim btnMas As New Button With {
                .Text = "+",
                .Font = ObtenerFuenteTitulo(15.0F, FontStyle.Bold),
                .Size = New Size(40, 40),
                .Location = New Point(89, 1),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = If(num.Enabled, ColorPrimary, Color.LightGray),
                .ForeColor = If(num.Enabled, Color.White, Color.DarkGray),
                .Cursor = Cursors.Hand,
                .Enabled = num.Enabled,
                .UseMnemonic = False
            }
            btnMas.FlatAppearance.BorderSize = 0

            ' Eventos de interacción y actualización sincronizada
            AddHandler btnMenos.Click, Sub(s, e)
                If num.Value > num.Minimum Then
                    num.Value -= 1
                End If
            End Sub

            AddHandler btnMas.Click, Sub(s, e)
                If num.Value < num.Maximum Then
                    num.Value += 1
                End If
            End Sub

            AddHandler num.ValueChanged, Sub(s, e)
                lblValor.Text = Convert.ToInt32(num.Value).ToString()
            End Sub

            AddHandler num.EnabledChanged, Sub(s, e)
                btnMenos.Enabled = num.Enabled
                btnMas.Enabled = num.Enabled
                lblValor.Enabled = num.Enabled
                If num.Enabled Then
                    btnMas.BackColor = ColorPrimary
                    btnMas.ForeColor = Color.White
                    btnMenos.BackColor = ColorBackgroundSidebar
                    lblValor.ForeColor = ColorNeutralDark
                Else
                    btnMas.BackColor = Color.FromArgb(220, 220, 220)
                    btnMas.ForeColor = Color.Gray
                    btnMenos.BackColor = Color.FromArgb(235, 235, 235)
                    lblValor.ForeColor = Color.Gray
                End If
            End Sub

            pnlStepper.Controls.Add(btnMenos)
            pnlStepper.Controls.Add(lblValor)
            pnlStepper.Controls.Add(btnMas)

            parent.Controls.Add(pnlStepper)
        End Sub

        ''' <summary>
        ''' Crea un selector de cantidad táctil (Touch Stepper) con botones gigantes [-] y [+] 
        ''' de 40x40px separados por un contador central en negrita.
        ''' </summary>
        Public Function CrearControlTouchStepper(initialValue As Integer, minValue As Integer, maxValue As Integer, onValueChanged As Action(Of Integer)) As Panel
            Dim pnlStepper As New Panel With {
                .Size = New Size(136, 42),
                .BackColor = Color.White
            }

            Dim valorActual As Integer = initialValue

            ' Botón Menos [-] (40x40px táctil)
            Dim btnMenos As New Button With {
                .Text = "-",
                .Font = ObtenerFuenteTitulo(15.0F, FontStyle.Bold),
                .Size = New Size(40, 40),
                .Location = New Point(1, 1),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = ColorBackgroundSidebar,
                .ForeColor = ColorNeutralDark,
                .Cursor = Cursors.Hand,
                .UseMnemonic = False
            }
            btnMenos.FlatAppearance.BorderSize = 1
            btnMenos.FlatAppearance.BorderColor = ColorBorder

            ' Contador central [ 1 ]
            Dim lblValor As New Label With {
                .Text = valorActual.ToString(),
                .Font = ObtenerFuenteTitulo(12.5F, FontStyle.Bold),
                .Size = New Size(50, 40),
                .Location = New Point(43, 1),
                .TextAlign = ContentAlignment.MiddleCenter,
                .ForeColor = ColorNeutralDark,
                .UseMnemonic = False
            }

            ' Botón Más [+] (40x40px táctil terracota)
            Dim btnMas As New Button With {
                .Text = "+",
                .Font = ObtenerFuenteTitulo(15.0F, FontStyle.Bold),
                .Size = New Size(40, 40),
                .Location = New Point(95, 1),
                .FlatStyle = FlatStyle.Flat,
                .BackColor = ColorPrimary,
                .ForeColor = Color.White,
                .Cursor = Cursors.Hand,
                .UseMnemonic = False
            }
            btnMas.FlatAppearance.BorderSize = 0

            AddHandler btnMenos.Click, Sub(s, e)
                If valorActual > minValue Then
                    valorActual -= 1
                    lblValor.Text = valorActual.ToString()
                    onValueChanged?.Invoke(valorActual)
                End If
            End Sub

            AddHandler btnMas.Click, Sub(s, e)
                If valorActual < maxValue Then
                    valorActual += 1
                    lblValor.Text = valorActual.ToString()
                    onValueChanged?.Invoke(valorActual)
                End If
            End Sub

            pnlStepper.Controls.Add(btnMenos)
            pnlStepper.Controls.Add(lblValor)
            pnlStepper.Controls.Add(btnMas)

            Return pnlStepper
        End Function

        ''' <summary>
        ''' Libera de forma recursiva y explícita todos los manejadores GDI, controles y recursos
        ''' de un contenedor antes de vaciarlo, evitando saturación de memoria RAM y congelamientos en PCs de bajos recursos.
        ''' </summary>
        Public Sub LimpiarYDestruirControles(contenedor As Control)
            If contenedor Is Nothing Then Return
            Try
                contenedor.SuspendLayout()
                While contenedor.Controls.Count > 0
                    Dim ctl = contenedor.Controls(0)
                    contenedor.Controls.RemoveAt(0)
                    Try
                        ' Si el control tiene hijos recursivos
                        If ctl.HasChildren Then
                            LimpiarYDestruirControles(ctl)
                        End If
                        ctl.Dispose()
                    Catch
                    End Try
                End While
            Finally
                contenedor.ResumeLayout(False)
            End Try
        End Sub

        ''' <summary>
        ''' Aplica estilo visual moderno, colores y fuentes institucionales a un DataGridView.
        ''' </summary>
        Public Sub EstilizarDataGrid(dgv As DataGridView)
            If dgv Is Nothing Then Return
            dgv.BackgroundColor = Color.White
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            dgv.GridColor = ColorBorder
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 230, 220)
            dgv.DefaultCellStyle.SelectionForeColor = ColorNeutralDark
            dgv.DefaultCellStyle.Font = ObtenerFuenteCuerpo(9.0F)
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorSecondary
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = ObtenerFuenteSubtitulo(9.0F, FontStyle.Bold)
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgv.EnableHeadersVisualStyles = False
            dgv.RowTemplate.Height = 28
            dgv.ColumnHeadersHeight = 32
        End Sub

    End Module
End Namespace
