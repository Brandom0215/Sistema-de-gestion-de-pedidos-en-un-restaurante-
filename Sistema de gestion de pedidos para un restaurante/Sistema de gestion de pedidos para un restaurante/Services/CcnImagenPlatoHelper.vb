Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Services
    ''' <summary>
    ''' Proveedor centralizado para generar y obtener representaciones gráficas de los platos gastronómicos.
    ''' Prefijo de módulo: Ccn
    ''' </summary>
    Public Module CcnImagenPlatoHelper

        ''' <summary>
        ''' Genera o carga la imagen de alta resolución del plato solicitado.
        ''' Si existe un archivo real en Resources/Platos, lo carga automáticamente.
        ''' </summary>
        Public Function GenerarImagenPlato(ByVal strNombrePlato As String,
                                           Optional ByVal intAncho As Integer = 80,
                                           Optional ByVal intAlto As Integer = 65) As Bitmap
            If intAncho <= 0 Then intAncho = 80
            If intAlto <= 0 Then intAlto = 65

            ' 0. Buscar si existe una imagen real en la carpeta Resources/Platos
            Dim strRutaReal As String = BuscarRutaImagenReal(strNombrePlato)
            If Not String.IsNullOrEmpty(strRutaReal) AndAlso File.Exists(strRutaReal) Then
                Try
                    Using imgOriginal As Image = Image.FromFile(strRutaReal)
                        Dim bmpReal As New Bitmap(intAncho, intAlto)
                        Using gReal As Graphics = Graphics.FromImage(bmpReal)
                            gReal.InterpolationMode = InterpolationMode.HighQualityBicubic
                            gReal.DrawImage(imgOriginal, 0, 0, intAncho, intAlto)
                        End Using
                        Return bmpReal
                    End Using
                Catch
                    ' Si la imagen no se puede leer, continuar con el gráfico por defecto
                End Try
            End If

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

                If strPlatoNormalizado.Contains("pescado") OrElse strPlatoNormalizado.Contains("corvina") Then
                    colorComida = Color.FromArgb(235, 170, 60)
                    strIcono = "🐟"
                ElseIf strPlatoNormalizado.Contains("hojaldre") OrElse strPlatoNormalizado.Contains("carimañola") OrElse strPlatoNormalizado.Contains("tortilla") Then
                    colorComida = Color.FromArgb(225, 165, 45)
                    strIcono = "🫓"
                ElseIf strPlatoNormalizado.Contains("sancocho") OrElse strPlatoNormalizado.Contains("sopa") Then
                    colorComida = Color.FromArgb(200, 120, 50)
                    strIcono = "🍲"
                ElseIf strPlatoNormalizado.Contains("tamal") Then
                    colorComida = Color.FromArgb(100, 140, 60)
                    strIcono = "🫔"
                ElseIf strPlatoNormalizado.Contains("ropa vieja") OrElse strPlatoNormalizado.Contains("bistec") OrElse strPlatoNormalizado.Contains("lengua") Then
                    colorComida = Color.FromArgb(160, 70, 35)
                    strIcono = "🥩"
                ElseIf strPlatoNormalizado.Contains("pollo") OrElse strPlatoNormalizado.Contains("arroz con pollo") Then
                    colorComida = Color.FromArgb(215, 135, 50)
                    strIcono = "🍗"
                ElseIf strPlatoNormalizado.Contains("chicha") OrElse strPlatoNormalizado.Contains("limonada") OrElse strPlatoNormalizado.Contains("soda") Then
                    colorComida = Color.FromArgb(240, 140, 30)
                    strIcono = "🍹"
                ElseIf strPlatoNormalizado.Contains("cerdo") OrElse strPlatoNormalizado.Contains("saao") Then
                    colorComida = Color.FromArgb(180, 90, 45)
                    strIcono = "🥩"
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

        Private Function BuscarRutaImagenReal(strNombrePlato As String) As String
            If String.IsNullOrWhiteSpace(strNombrePlato) Then Return String.Empty
            Dim strBaseDir As String = AppDomain.CurrentDomain.BaseDirectory
            Dim strRutaCarpeta As String = Path.Combine(strBaseDir, "Resources", "Platos")
            If Not Directory.Exists(strRutaCarpeta) Then
                strRutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Platos")
            End If

            If Not Directory.Exists(strRutaCarpeta) Then Return String.Empty

            Dim strNombreLimpio As String = strNombrePlato.ToLowerInvariant()
            For Each archivo In Directory.GetFiles(strRutaCarpeta)
                Dim nombreArchivoSinExt As String = Path.GetFileNameWithoutExtension(archivo).ToLowerInvariant()
                If strNombreLimpio.Contains(nombreArchivoSinExt) OrElse nombreArchivoSinExt.Contains(strNombreLimpio) Then
                    Return archivo
                End If
            Next

            Return String.Empty
        End Function

    End Module
End Namespace
