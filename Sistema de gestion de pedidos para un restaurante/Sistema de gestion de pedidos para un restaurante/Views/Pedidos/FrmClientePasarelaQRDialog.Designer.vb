Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Pedidos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmClientePasarelaQRDialog
        Inherits Views.Common.FrmBaseForm

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.pnlHeaderQR = New System.Windows.Forms.Panel()
            Me.lblTituloQR = New System.Windows.Forms.Label()
            Me.lblSubtituloQR = New System.Windows.Forms.Label()

            Me.pnlContenidoQR = New System.Windows.Forms.Panel()
            Me.pnlTimerContainer = New System.Windows.Forms.Panel()
            Me.lblEtiquetaTimer = New System.Windows.Forms.Label()
            Me.lblConteoRegresivo = New System.Windows.Forms.Label()

            Me.picCodigoQR = New System.Windows.Forms.PictureBox()
            Me.lblMontoApagar = New System.Windows.Forms.Label()
            Me.lblDetalleTransferencia = New System.Windows.Forms.Label()
            Me.lblInstruccionesPasarela = New System.Windows.Forms.Label()

            Me.pnlAccionesQR = New System.Windows.Forms.Panel()
            Me.btnSimularPagoExitoso = New System.Windows.Forms.Button()
            Me.btnCancelarPago = New System.Windows.Forms.Button()

            Me.tmrCuentaRegresiva = New System.Windows.Forms.Timer(Me.components)

            Me.pnlHeaderQR.SuspendLayout()
            Me.pnlContenidoQR.SuspendLayout()
            Me.pnlTimerContainer.SuspendLayout()
            CType(Me.picCodigoQR, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlAccionesQR.SuspendLayout()
            Me.SuspendLayout()

            ' pnlHeaderQR
            Me.pnlHeaderQR.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.pnlHeaderQR.Controls.Add(Me.lblSubtituloQR)
            Me.pnlHeaderQR.Controls.Add(Me.lblTituloQR)
            Me.pnlHeaderQR.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderQR.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeaderQR.Name = "pnlHeaderQR"
            Me.pnlHeaderQR.Size = New System.Drawing.Size(560, 75)
            Me.pnlHeaderQR.TabIndex = 0

            ' lblTituloQR
            Me.lblTituloQR.AutoSize = True
            Me.lblTituloQR.Font = New System.Drawing.Font("Segoe UI", 14.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloQR.ForeColor = System.Drawing.Color.White
            Me.lblTituloQR.Location = New System.Drawing.Point(16, 12)
            Me.lblTituloQR.Name = "lblTituloQR"
            Me.lblTituloQR.Size = New System.Drawing.Size(380, 25)
            Me.lblTituloQR.TabIndex = 0
            Me.lblTituloQR.Text = "Pasarela de Pago Digital — QR & Yappy"

            ' lblSubtituloQR
            Me.lblSubtituloQR.AutoSize = True
            Me.lblSubtituloQR.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblSubtituloQR.ForeColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.lblSubtituloQR.Location = New System.Drawing.Point(16, 42)
            Me.lblSubtituloQR.Name = "lblSubtituloQR"
            Me.lblSubtituloQR.Size = New System.Drawing.Size(460, 17)
            Me.lblSubtituloQR.TabIndex = 1
            Me.lblSubtituloQR.Text = "EL BUEN SAZÓN — Escanee el código para procesar su pago directamente."

            ' pnlContenidoQR
            Me.pnlContenidoQR.Controls.Add(Me.lblInstruccionesPasarela)
            Me.pnlContenidoQR.Controls.Add(Me.lblDetalleTransferencia)
            Me.pnlContenidoQR.Controls.Add(Me.lblMontoApagar)
            Me.pnlContenidoQR.Controls.Add(Me.picCodigoQR)
            Me.pnlContenidoQR.Controls.Add(Me.pnlTimerContainer)
            Me.pnlContenidoQR.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenidoQR.Location = New System.Drawing.Point(0, 75)
            Me.pnlContenidoQR.Name = "pnlContenidoQR"
            Me.pnlContenidoQR.Padding = New System.Windows.Forms.Padding(16)
            Me.pnlContenidoQR.Size = New System.Drawing.Size(560, 485)
            Me.pnlContenidoQR.TabIndex = 1

            ' pnlTimerContainer
            Me.pnlTimerContainer.BackColor = System.Drawing.Color.FromArgb(255, 245, 238)
            Me.pnlTimerContainer.Controls.Add(Me.lblConteoRegresivo)
            Me.pnlTimerContainer.Controls.Add(Me.lblEtiquetaTimer)
            Me.pnlTimerContainer.Location = New System.Drawing.Point(16, 12)
            Me.pnlTimerContainer.Name = "pnlTimerContainer"
            Me.pnlTimerContainer.Size = New System.Drawing.Size(528, 55)
            Me.pnlTimerContainer.TabIndex = 0

            ' lblEtiquetaTimer
            Me.lblEtiquetaTimer.AutoSize = True
            Me.lblEtiquetaTimer.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblEtiquetaTimer.ForeColor = System.Drawing.Color.FromArgb(117, 70, 50)
            Me.lblEtiquetaTimer.Location = New System.Drawing.Point(12, 18)
            Me.lblEtiquetaTimer.Name = "lblEtiquetaTimer"
            Me.lblEtiquetaTimer.Size = New System.Drawing.Size(255, 19)
            Me.lblEtiquetaTimer.TabIndex = 0
            Me.lblEtiquetaTimer.Text = "Tiempo Límite para Completar Pago:"

            ' lblConteoRegresivo
            Me.lblConteoRegresivo.Font = New System.Drawing.Font("Segoe UI", 20.0F, System.Drawing.FontStyle.Bold)
            Me.lblConteoRegresivo.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblConteoRegresivo.Location = New System.Drawing.Point(340, 8)
            Me.lblConteoRegresivo.Name = "lblConteoRegresivo"
            Me.lblConteoRegresivo.Size = New System.Drawing.Size(175, 38)
            Me.lblConteoRegresivo.TabIndex = 1
            Me.lblConteoRegresivo.Text = "10:00"
            Me.lblConteoRegresivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' picCodigoQR
            Me.picCodigoQR.BackColor = System.Drawing.Color.White
            Me.picCodigoQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picCodigoQR.Location = New System.Drawing.Point(180, 80)
            Me.picCodigoQR.Name = "picCodigoQR"
            Me.picCodigoQR.Size = New System.Drawing.Size(200, 200)
            Me.picCodigoQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.picCodigoQR.TabIndex = 1
            Me.picCodigoQR.TabStop = False

            ' lblMontoApagar
            Me.lblMontoApagar.Font = New System.Drawing.Font("Segoe UI", 18.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoApagar.ForeColor = System.Drawing.Color.FromArgb(117, 70, 50)
            Me.lblMontoApagar.Location = New System.Drawing.Point(16, 290)
            Me.lblMontoApagar.Name = "lblMontoApagar"
            Me.lblMontoApagar.Size = New System.Drawing.Size(528, 35)
            Me.lblMontoApagar.TabIndex = 2
            Me.lblMontoApagar.Text = "Monto Total: $ 0.00"
            Me.lblMontoApagar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            ' lblDetalleTransferencia
            Me.lblDetalleTransferencia.Font = New System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Bold)
            Me.lblDetalleTransferencia.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblDetalleTransferencia.Location = New System.Drawing.Point(16, 330)
            Me.lblDetalleTransferencia.Name = "lblDetalleTransferencia"
            Me.lblDetalleTransferencia.Size = New System.Drawing.Size(528, 48)
            Me.lblDetalleTransferencia.TabIndex = 3
            Me.lblDetalleTransferencia.Text = "Banco General / Yappy: +507 6200-1122 (@elbuensazon)" & vbCrLf & "Cuenta Corriente: 03-72-01-998877-4"
            Me.lblDetalleTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            ' lblInstruccionesPasarela
            Me.lblInstruccionesPasarela.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblInstruccionesPasarela.ForeColor = System.Drawing.Color.Gray
            Me.lblInstruccionesPasarela.Location = New System.Drawing.Point(16, 385)
            Me.lblInstruccionesPasarela.Name = "lblInstruccionesPasarela"
            Me.lblInstruccionesPasarela.Size = New System.Drawing.Size(528, 42)
            Me.lblInstruccionesPasarela.TabIndex = 4
            Me.lblInstruccionesPasarela.Text = "Al ser confirmada la transacción por la pasarela de pagos, el sistema despachará automáticamente la comanda a la pantalla de la cocina y emitirá la factura digital en PDF."
            Me.lblInstruccionesPasarela.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            ' pnlAccionesQR
            Me.pnlAccionesQR.BackColor = System.Drawing.Color.White
            Me.pnlAccionesQR.Controls.Add(Me.btnCancelarPago)
            Me.pnlAccionesQR.Controls.Add(Me.btnSimularPagoExitoso)
            Me.pnlAccionesQR.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlAccionesQR.Location = New System.Drawing.Point(0, 560)
            Me.pnlAccionesQR.Name = "pnlAccionesQR"
            Me.pnlAccionesQR.Size = New System.Drawing.Size(560, 80)
            Me.pnlAccionesQR.TabIndex = 2

            ' btnSimularPagoExitoso
            Me.btnSimularPagoExitoso.Font = New System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold)
            Me.btnSimularPagoExitoso.Location = New System.Drawing.Point(210, 14)
            Me.btnSimularPagoExitoso.Name = "btnSimularPagoExitoso"
            Me.btnSimularPagoExitoso.Size = New System.Drawing.Size(334, 52)
            Me.btnSimularPagoExitoso.TabIndex = 0
            Me.btnSimularPagoExitoso.Text = "Simular Pago Exitoso (Pasarela)"
            Me.btnSimularPagoExitoso.UseVisualStyleBackColor = True

            ' btnCancelarPago
            Me.btnCancelarPago.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancelarPago.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.btnCancelarPago.Location = New System.Drawing.Point(16, 14)
            Me.btnCancelarPago.Name = "btnCancelarPago"
            Me.btnCancelarPago.Size = New System.Drawing.Size(180, 52)
            Me.btnCancelarPago.TabIndex = 1
            Me.btnCancelarPago.Text = "Cancelar Pago"
            Me.btnCancelarPago.UseVisualStyleBackColor = True

            ' tmrCuentaRegresiva
            Me.tmrCuentaRegresiva.Interval = 1000

            ' FrmClientePasarelaQRDialog
            Me.AcceptButton = Me.btnSimularPagoExitoso
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnCancelarPago
            Me.ClientSize = New System.Drawing.Size(560, 640)
            Me.Controls.Add(Me.pnlContenidoQR)
            Me.Controls.Add(Me.pnlAccionesQR)
            Me.Controls.Add(Me.pnlHeaderQR)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmClientePasarelaQRDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Pasarela de Pago QR — EL BUEN SAZÓN"
            Me.pnlHeaderQR.ResumeLayout(False)
            Me.pnlHeaderQR.PerformLayout()
            Me.pnlContenidoQR.ResumeLayout(False)
            Me.pnlTimerContainer.ResumeLayout(False)
            Me.pnlTimerContainer.PerformLayout()
            CType(Me.picCodigoQR, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlAccionesQR.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeaderQR As Panel
        Friend WithEvents lblTituloQR As Label
        Friend WithEvents lblSubtituloQR As Label
        Friend WithEvents pnlContenidoQR As Panel
        Friend WithEvents pnlTimerContainer As Panel
        Friend WithEvents lblEtiquetaTimer As Label
        Friend WithEvents lblConteoRegresivo As Label
        Friend WithEvents picCodigoQR As PictureBox
        Friend WithEvents lblMontoApagar As Label
        Friend WithEvents lblDetalleTransferencia As Label
        Friend WithEvents lblInstruccionesPasarela As Label
        Friend WithEvents pnlAccionesQR As Panel
        Friend WithEvents btnSimularPagoExitoso As Button
        Friend WithEvents btnCancelarPago As Button
        Friend WithEvents tmrCuentaRegresiva As Timer
    End Class
End Namespace
