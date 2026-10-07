Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Pedidos
    ''' <summary>
    ''' Cuadro de diálogo modal de simulación de Pasarela de Pago Digital por QR / Yappy / Transferencia.
    ''' Implementa un margen de hasta 10 minutos (600 segundos) para completar la transacción.
    ''' </summary>
    Public Class FrmClientePasarelaQRDialog

        Private ReadOnly _nombreCliente As String
        Private ReadOnly _montoTotal As Decimal
        Private ReadOnly _metodoPago As String
        Private _segundosRestantes As Integer = 600 ' 10 minutos exactos de margen

        Public Sub New(nombreCliente As String, montoTotal As Decimal, metodoPago As String)
            InitializeComponent()
            _nombreCliente = nombreCliente
            _montoTotal = montoTotal
            _metodoPago = metodoPago
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmClientePasarelaQRDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisual()
            GenerarImagenQRSimulada()
            lblMontoApagar.Text = $"Monto Total: $ {_montoTotal:N2}"
            ActualizarEtiquetaTimer()
            tmrCuentaRegresiva.Start()
        End Sub

        Protected Overrides Sub AplicarTemaVisual()
            MyBase.AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            pnlHeaderQR.BackColor = ThemeConfig.ColorPrimary
            pnlAccionesQR.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlAccionesQR)

            ThemeConfig.EstilizarBotonPrimario(btnSimularPagoExitoso)
            ThemeConfig.EstilizarBotonSecundario(btnCancelarPago)
        End Sub

        Private Sub GenerarImagenQRSimulada()
            Dim bmp As New Bitmap(200, 200)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                Using brushOscuro As New SolidBrush(Color.FromArgb(41, 43, 38))
                    ' Dibujar patrones simulados de código QR
                    g.FillRectangle(brushOscuro, 15, 15, 45, 45)
                    g.FillRectangle(Brushes.White, 25, 25, 25, 25)
                    g.FillRectangle(brushOscuro, 30, 30, 15, 15)

                    g.FillRectangle(brushOscuro, 140, 15, 45, 45)
                    g.FillRectangle(Brushes.White, 150, 25, 25, 25)
                    g.FillRectangle(brushOscuro, 155, 30, 15, 15)

                    g.FillRectangle(brushOscuro, 15, 140, 45, 45)
                    g.FillRectangle(Brushes.White, 25, 150, 25, 25)
                    g.FillRectangle(brushOscuro, 30, 155, 15, 15)

                    ' Cuadrícula interna simulada de datos
                    Dim rnd As New Random(12345)
                    For x As Integer = 15 To 180 Step 12
                        For y As Integer = 15 To 180 Step 12
                            If (x < 65 AndAlso y < 65) OrElse (x > 130 AndAlso y < 65) OrElse (x < 65 AndAlso y > 130) Then
                                Continue For
                            End If
                            If rnd.Next(0, 2) = 1 Then
                                g.FillRectangle(brushOscuro, x, y, 10, 10)
                            End If
                        Next
                    Next

                    ' Logotipo o texto central
                    g.FillRectangle(New SolidBrush(ThemeConfig.ColorPrimary), 75, 75, 50, 50)
                    Using fontLogo As New Font("Segoe UI", 9.0F, FontStyle.Bold)
                        g.DrawString("YAPPY", fontLogo, Brushes.White, 78, 90)
                    End Using
                End Using
            End Using
            picCodigoQR.Image = bmp
        End Sub

        Private Sub tmrCuentaRegresiva_Tick(sender As Object, e As EventArgs) Handles tmrCuentaRegresiva.Tick
            _segundosRestantes -= 1
            ActualizarEtiquetaTimer()

            If _segundosRestantes <= 0 Then
                tmrCuentaRegresiva.Stop()
                MessageBox.Show(
                    "El tiempo de margen de 10 minutos para completar la transacción ha expirado. Su pedido no ha sido procesado.",
                    "Tiempo Expirado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        End Sub

        Private Sub ActualizarEtiquetaTimer()
            Dim minutos As Integer = _segundosRestantes \ 60
            Dim segundos As Integer = _segundosRestantes Mod 60
            lblConteoRegresivo.Text = $"{minutos:D2}:{segundos:D2}"

            ' Cambiar a color de alerta si quedan menos de 2 minutos
            If _segundosRestantes <= 120 Then
                lblConteoRegresivo.ForeColor = Color.FromArgb(198, 40, 40)
            Else
                lblConteoRegresivo.ForeColor = ThemeConfig.ColorPrimary
            End If
        End Sub

        Private Sub btnSimularPagoExitoso_Click(sender As Object, e As EventArgs) Handles btnSimularPagoExitoso.Click
            tmrCuentaRegresiva.Stop()
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub FrmClientePasarelaQRDialog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            tmrCuentaRegresiva.Stop()
        End Sub

    End Class
End Namespace
