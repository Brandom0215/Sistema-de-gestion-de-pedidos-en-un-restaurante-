Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Services
    ''' <summary>
    ''' Proveedor centralizado para generar y obtener representaciones gráficas de los platos gastronómicos.
    ''' Prefijo de módulo: Ccn
    ''' </summary>
    Public Module CcnImagenPlatoHelper

        ''' <summary>
        ''' Genera una imagen vectorial de alta resolución del plato solicitado.
        ''' </summary>
        ''' <param name="strNombrePlato">Nombre del plato o receta</param>
        ''' <param name="intAncho">Ancho deseado de la imagen</param>
        ''' <param name="intAlto">Alto deseado de la imagen</param>
        Public Function GenerarImagenPlato(ByVal strNombrePlato As String,
                                           Optional ByVal intAncho As Integer = 80,
                                           Optional ByVal intAlto As Integer = 65) As Bitmap
            If intAncho <= 0 Then intAncho = 80
            If intAlto <= 0 Then intAlto = 65

            Dim bmp As New Bitmap(intAncho, intAlto)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.InterpolationMode = InterpolationMode.HighQualityBicubic

                ' 1. Fondo suave con bordes redondeados simulados
                Dim rectFondo As New Rectangle(0, 0, intAncho, intAlto)
                Using brushFondo As New LinearGradientBrush(rectFondo, Color.FromArgb(246, 244, 238), Color.FromArgb(235, 230, 222), 45.0F)
                    g.FillRectangle(brushFondo, rectFondo)
                End Using

                ' 2. Dimensiones del plato
                Dim intCenterX As Integer = intAncho \ 2
                Dim intCenterY As Integer = (intAlto \ 2) - 3
                Dim intRadioX As Integer = (intAncho \ 2) - 8
                Dim intRadioY As Integer = (intAlto \ 2) - 10

                ' Sombra suave del plato
                Using brushSombra As New SolidBrush(Color.FromArgb(30, 0, 0, 0))
                    g.FillEllipse(brushSombra, intCenterX - intRadioX + 2, intCenterY - intRadioY + 4, intRadioX * 2, intRadioY * 2)
                End Using

                ' Cerámica Blanca Exterior
                Using brushPlato As New SolidBrush(Color.White)
                    g.FillEllipse(brushPlato, intCenterX - intRadioX, intCenterY - intRadioY, intRadioX * 2, intRadioY * 2)
                End Using
                Using penBorde As New Pen(Color.FromArgb(215, 210, 200), 1.5F)
                    g.DrawEllipse(penBorde, intCenterX - intRadioX, intCenterY - intRadioY, intRadioX * 2, intRadioY * 2)
                End Using

                ' Cerámica Interior
                Dim intRadioIntX As Integer = CInt(intRadioX * 0.72)
                Dim intRadioIntY As Integer = CInt(intRadioY * 0.72)
                Using brushInterior As New SolidBrush(Color.FromArgb(252, 251, 248))
                    g.FillEllipse(brushInterior, intCenterX - intRadioIntX, intCenterY - intRadioIntY, intRadioIntX * 2, intRadioIntY * 2)
                End Using

                ' 3. Selección temática del alimento según el plato
                Dim colorComida As Color = ThemeConfig.ColorPrimary
                Dim strIcono As String = "🥩"

                Dim strPlatoNormalizado As String = If(strNombrePlato, "").ToLowerInvariant()

                If strPlatoNormalizado.Contains("ceviche") OrElse strPlatoNormalizado.Contains("pescado") Then
                    colorComida = Color.FromArgb(235, 170, 60)
                    strIcono = "🐟"
                ElseIf strPlatoNormalizado.Contains("pollo") Then
                    colorComida = Color.FromArgb(215, 135, 50)
                    strIcono = "🍗"
                ElseIf strPlatoNormalizado.Contains("hamburguesa") Then
                    colorComida = Color.FromArgb(160, 75, 40)
                    strIcono = "🍔"
                ElseIf strPlatoNormalizado.Contains("pasta") OrElse strPlatoNormalizado.Contains("fettuccine") OrElse strPlatoNormalizado.Contains("risotto") Then
                    colorComida = Color.FromArgb(230, 195, 85)
                    strIcono = "🍝"
                ElseIf strPlatoNormalizado.Contains("ensalada") OrElse strPlatoNormalizado.Contains("burrata") Then
                    colorComida = Color.FromArgb(89, 107, 75)
                    strIcono = "🥗"
                ElseIf strPlatoNormalizado.Contains("mofongo") OrElse strPlatoNormalizado.Contains("chivo") Then
                    colorComida = Color.FromArgb(175, 100, 55)
                    strIcono = "🍲"
                ElseIf strPlatoNormalizado.Contains("sancocho") Then
                    colorComida = Color.FromArgb(200, 120, 50)
                    strIcono = "🍲"
                ElseIf strPlatoNormalizado.Contains("focaccia") OrElse strPlatoNormalizado.Contains("pan") Then
                    colorComida = Color.FromArgb(220, 175, 90)
                    strIcono = "🥖"
                End If

                ' Porción de alimento
                Using brushComida As New SolidBrush(colorComida)
                    g.FillEllipse(brushComida, intCenterX - (intRadioIntX \ 2), intCenterY - (intRadioIntY \ 2), intRadioIntX, intRadioIntY)
                End Using

                ' Ícono central del plato
                Dim fontIcono As New Font("Segoe UI Emoji", If(intAncho > 100, 16.0F, 11.0F), FontStyle.Regular)
                Using brushTexto As New SolidBrush(Color.White)
                    Dim sizeIcono As SizeF = g.MeasureString(strIcono, fontIcono)
                    g.DrawString(strIcono, fontIcono, brushTexto, intCenterX - (sizeIcono.Width / 2.0F), intCenterY - (sizeIcono.Height / 2.0F))
                End Using
            End Using

            Return bmp
        End Function

    End Module
End Namespace
