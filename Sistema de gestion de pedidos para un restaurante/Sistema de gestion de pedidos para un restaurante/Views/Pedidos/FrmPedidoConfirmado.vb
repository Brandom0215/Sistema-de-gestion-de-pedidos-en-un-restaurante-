Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Pedidos
    ''' <summary>
    ''' Segunda Interfaz requerida por el profesor:
    ''' Muestra el pedido confirmado con la imagen del plato principal (PictureBox).
    ''' </summary>
    Public Class FrmPedidoConfirmado

        Private _idPedido As Integer
        Private _cliente As String
        Private _mesa As String
        Private _plato As String
        Private _acomp As String
        Private _servicio As String

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Sub New(idPedido As Integer, cliente As String, mesa As String, plato As String, acomp As String, servicio As String)
            Me.New()
            _idPedido = idPedido
            _cliente = cliente
            _mesa = mesa
            _plato = plato
            _acomp = acomp
            _servicio = servicio
        End Sub

        Private Sub FrmPedidoConfirmado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            CargarDatosDetalle()
            GenerarImagenPlatoPrincipal()
        End Sub

        Private Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundSidebar
            pnlCardModal.BackColor = Color.White

            lblTituloModal.ForeColor = ThemeConfig.ColorSecondary
            lblInfoCliente.ForeColor = ThemeConfig.ColorNeutralDark
            lblInfoPlato.ForeColor = ThemeConfig.ColorPrimary
            lblEstadoPedido.ForeColor = ThemeConfig.ColorTertiarySuccess

            ThemeConfig.EstilizarBotonPrimario(btnCerrarModal)
        End Sub

        Private Sub CargarDatosDetalle()
            lblTituloModal.Text = $"✅ Pedido Confirmado #{If(_idPedido > 0, _idPedido.ToString("D2"), "01")}"
            lblInfoCliente.Text = $"👤 Cliente: {_cliente}"
            lblInfoMesaServicio.Text = $"🪑 Servicio: {_servicio} • Ubicación: {_mesa}"
            lblInfoPlato.Text = $"🍽️ Plato Principal: {_plato}"
            lblInfoAcompanamientos.Text = $"🍟 Acompañamientos: {_acomp}"
        End Sub

        ''' <summary>
        ''' Renderiza una imagen vectorial atractiva del plato principal en el PictureBox
        ''' según el plato seleccionado en el pedido.
        ''' </summary>
        Private Sub GenerarImagenPlatoPrincipal()
            Dim width As Integer = picPlatoPrincipal.Width
            Dim height As Integer = picPlatoPrincipal.Height
            If width <= 0 OrElse height <= 0 Then Return

            Dim bmp As New Bitmap(width, height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                ' Fondo elegante con gradiente leve
                Dim rectFondo As New Rectangle(0, 0, width, height)
                Using brushFondo As New Drawing2D.LinearGradientBrush(rectFondo, Color.FromArgb(248, 246, 240), Color.FromArgb(235, 228, 218), 45.0F)
                    g.FillRectangle(brushFondo, rectFondo)
                End Using

                ' Dibujar Plato Cerámico
                Dim platoCenterX As Integer = width \ 2
                Dim platoCenterY As Integer = height \ 2
                Dim platoRadiusX As Integer = 150
                Dim platoRadiusY As Integer = 75

                ' Sombra del plato
                Using brushSombra As New SolidBrush(Color.FromArgb(40, 0, 0, 0))
                    g.FillEllipse(brushSombra, platoCenterX - platoRadiusX + 5, platoCenterY - platoRadiusY + 10, platoRadiusX * 2, platoRadiusY * 2)
                End Using

                ' Cerámica Exterior
                Using brushPlato As New SolidBrush(Color.White)
                    g.FillEllipse(brushPlato, platoCenterX - platoRadiusX, platoCenterY - platoRadiusY, platoRadiusX * 2, platoRadiusY * 2)
                End Using
                Using penBorde As New Pen(Color.FromArgb(210, 205, 195), 3)
                    g.DrawEllipse(penBorde, platoCenterX - platoRadiusX, platoCenterY - platoRadiusY, platoRadiusX * 2, platoRadiusY * 2)
                End Using

                ' Cerámica Interior
                Dim interiorRadiusX As Integer = 110
                Dim interiorRadiusY As Integer = 55
                Using brushInterior As New SolidBrush(Color.FromArgb(250, 249, 246))
                    g.FillEllipse(brushInterior, platoCenterX - interiorRadiusX, platoCenterY - interiorRadiusY, interiorRadiusX * 2, interiorRadiusY * 2)
                End Using

                ' Dibujar Alimento / Guarnición según la receta seleccionada
                Dim colorComida As Color = Color.FromArgb(198, 107, 72) ' Terracota por defecto
                Dim iconoPlato As String = "🥩"

                If _plato.Contains("Ceviche") Then
                    colorComida = Color.FromArgb(230, 160, 50)
                    iconoPlato = "🐟"
                ElseIf _plato.Contains("Pollo") Then
                    colorComida = Color.FromArgb(205, 130, 60)
                    iconoPlato = "🍗"
                ElseIf _plato.Contains("Hamburguesa") Then
                    colorComida = Color.FromArgb(160, 70, 40)
                    iconoPlato = "🍔"
                ElseIf _plato.Contains("Fettuccine") OrElse _plato.Contains("Pasta") Then
                    colorComida = Color.FromArgb(225, 190, 80)
                    iconoPlato = "🍝"
                ElseIf _plato.Contains("Ensalada") Then
                    colorComida = Color.FromArgb(89, 107, 75)
                    iconoPlato = "🥗"
                End If

                ' Porción de comida en el plato
                Using brushComida As New SolidBrush(colorComida)
                    g.FillEllipse(brushComida, platoCenterX - 70, platoCenterY - 35, 140, 70)
                End Using

                ' Título del Plato sobreimpreso en el banner inferior
                Using brushBanner As New SolidBrush(Color.FromArgb(200, 117, 70, 50))
                    g.FillRectangle(brushBanner, 0, height - 35, width, 35)
                End Using

                Dim fontTexto As Font = ThemeConfig.ObtenerFuenteSubtitulo(10.0F, FontStyle.Bold)
                Dim textoFoto As String = $"{iconoPlato} {_plato}"
                TextRenderer.DrawText(g, textoFoto, fontTexto, New Rectangle(0, height - 35, width, 35), Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            End Using

            picPlatoPrincipal.Image = bmp
        End Sub

        Private Sub btnCerrarModal_Click(sender As Object, e As EventArgs) Handles btnCerrarModal.Click
            Me.Close()
        End Sub

    End Class
End Namespace
