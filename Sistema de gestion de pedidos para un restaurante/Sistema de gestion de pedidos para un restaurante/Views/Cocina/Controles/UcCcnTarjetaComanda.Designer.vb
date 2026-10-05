Namespace Views.Cocina.Controles
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UcCcnTarjetaComanda
        Inherits System.Windows.Forms.UserControl

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
            Me.pnlCcnBordeSuperior = New System.Windows.Forms.Panel()
            Me.pnlCcnHeaderCard = New System.Windows.Forms.Panel()
            Me.lblCcnBadgeEstado = New System.Windows.Forms.Label()
            Me.lblCcnTagMesa = New System.Windows.Forms.Label()
            Me.pnlCcnSubheader = New System.Windows.Forms.Panel()
            Me.lblCcnTiempoTranscurrido = New System.Windows.Forms.Label()
            Me.lblCcnMozoOCliente = New System.Windows.Forms.Label()
            Me.flpCcnPlatos = New System.Windows.Forms.FlowLayoutPanel()
            Me.pnlCcnFooterCard = New System.Windows.Forms.Panel()
            Me.btnCcnAccionPrincipal = New System.Windows.Forms.Button()
            Me.pnlCcnInfoPago = New System.Windows.Forms.Panel()
            Me.lblCcnMontoTotal = New System.Windows.Forms.Label()
            Me.lblCcnMetodoPago = New System.Windows.Forms.Label()
            Me.pnlCcnHeaderCard.SuspendLayout()
            Me.pnlCcnSubheader.SuspendLayout()
            Me.pnlCcnFooterCard.SuspendLayout()
            Me.pnlCcnInfoPago.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlCcnBordeSuperior
            '
            Me.pnlCcnBordeSuperior.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.pnlCcnBordeSuperior.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnBordeSuperior.Location = New System.Drawing.Point(0, 0)
            Me.pnlCcnBordeSuperior.Name = "pnlCcnBordeSuperior"
            Me.pnlCcnBordeSuperior.Size = New System.Drawing.Size(326, 4)
            Me.pnlCcnBordeSuperior.TabIndex = 0
            '
            'pnlCcnHeaderCard
            '
            Me.pnlCcnHeaderCard.Controls.Add(Me.lblCcnBadgeEstado)
            Me.pnlCcnHeaderCard.Controls.Add(Me.lblCcnTagMesa)
            Me.pnlCcnHeaderCard.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnHeaderCard.Location = New System.Drawing.Point(0, 4)
            Me.pnlCcnHeaderCard.Name = "pnlCcnHeaderCard"
            Me.pnlCcnHeaderCard.Padding = New System.Windows.Forms.Padding(12, 10, 12, 4)
            Me.pnlCcnHeaderCard.Size = New System.Drawing.Size(326, 38)
            Me.pnlCcnHeaderCard.TabIndex = 1
            '
            'lblCcnBadgeEstado
            '
            Me.lblCcnBadgeEstado.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.lblCcnBadgeEstado.Dock = System.Windows.Forms.DockStyle.Right
            Me.lblCcnBadgeEstado.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnBadgeEstado.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnBadgeEstado.Location = New System.Drawing.Point(194, 10)
            Me.lblCcnBadgeEstado.Name = "lblCcnBadgeEstado"
            Me.lblCcnBadgeEstado.Size = New System.Drawing.Size(120, 24)
            Me.lblCcnBadgeEstado.TabIndex = 1
            Me.lblCcnBadgeEstado.Text = "En Cocina"
            Me.lblCcnBadgeEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblCcnTagMesa
            '
            Me.lblCcnTagMesa.AutoSize = True
            Me.lblCcnTagMesa.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblCcnTagMesa.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnTagMesa.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnTagMesa.Location = New System.Drawing.Point(12, 10)
            Me.lblCcnTagMesa.Name = "lblCcnTagMesa"
            Me.lblCcnTagMesa.Size = New System.Drawing.Size(128, 19)
            Me.lblCcnTagMesa.TabIndex = 0
            Me.lblCcnTagMesa.Text = "MESA 04  #08-1042"
            '
            'pnlCcnSubheader
            '
            Me.pnlCcnSubheader.Controls.Add(Me.lblCcnTiempoTranscurrido)
            Me.pnlCcnSubheader.Controls.Add(Me.lblCcnMozoOCliente)
            Me.pnlCcnSubheader.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnSubheader.Location = New System.Drawing.Point(0, 42)
            Me.pnlCcnSubheader.Name = "pnlCcnSubheader"
            Me.pnlCcnSubheader.Padding = New System.Windows.Forms.Padding(12, 2, 12, 6)
            Me.pnlCcnSubheader.Size = New System.Drawing.Size(326, 26)
            Me.pnlCcnSubheader.TabIndex = 2
            '
            'lblCcnTiempoTranscurrido
            '
            Me.lblCcnTiempoTranscurrido.Dock = System.Windows.Forms.DockStyle.Right
            Me.lblCcnTiempoTranscurrido.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular)
            Me.lblCcnTiempoTranscurrido.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnTiempoTranscurrido.Location = New System.Drawing.Point(180, 2)
            Me.lblCcnTiempoTranscurrido.Name = "lblCcnTiempoTranscurrido"
            Me.lblCcnTiempoTranscurrido.Size = New System.Drawing.Size(134, 18)
            Me.lblCcnTiempoTranscurrido.TabIndex = 1
            Me.lblCcnTiempoTranscurrido.Text = "⏱ 13:45 (Hace 8m)"
            Me.lblCcnTiempoTranscurrido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblCcnMozoOCliente
            '
            Me.lblCcnMozoOCliente.AutoSize = True
            Me.lblCcnMozoOCliente.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblCcnMozoOCliente.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
            Me.lblCcnMozoOCliente.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnMozoOCliente.Location = New System.Drawing.Point(12, 2)
            Me.lblCcnMozoOCliente.Name = "lblCcnMozoOCliente"
            Me.lblCcnMozoOCliente.Size = New System.Drawing.Size(86, 15)
            Me.lblCcnMozoOCliente.TabIndex = 0
            Me.lblCcnMozoOCliente.Text = "Mozo: Mateo R."
            '
            'flpCcnPlatos
            '
            Me.flpCcnPlatos.AutoScroll = True
            Me.flpCcnPlatos.BackColor = System.Drawing.Color.FromArgb(252, 251, 249)
            Me.flpCcnPlatos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpCcnPlatos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.flpCcnPlatos.Location = New System.Drawing.Point(0, 68)
            Me.flpCcnPlatos.Name = "flpCcnPlatos"
            Me.flpCcnPlatos.Padding = New System.Windows.Forms.Padding(10, 6, 10, 6)
            Me.flpCcnPlatos.Size = New System.Drawing.Size(326, 252)
            Me.flpCcnPlatos.TabIndex = 3
            Me.flpCcnPlatos.WrapContents = False
            '
            'pnlCcnFooterCard
            '
            Me.pnlCcnFooterCard.Controls.Add(Me.btnCcnAccionPrincipal)
            Me.pnlCcnFooterCard.Controls.Add(Me.pnlCcnInfoPago)
            Me.pnlCcnFooterCard.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlCcnFooterCard.Location = New System.Drawing.Point(0, 320)
            Me.pnlCcnFooterCard.Name = "pnlCcnFooterCard"
            Me.pnlCcnFooterCard.Padding = New System.Windows.Forms.Padding(12, 6, 12, 10)
            Me.pnlCcnFooterCard.Size = New System.Drawing.Size(326, 76)
            Me.pnlCcnFooterCard.TabIndex = 4
            '
            'btnCcnAccionPrincipal
            '
            Me.btnCcnAccionPrincipal.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.btnCcnAccionPrincipal.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnAccionPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnCcnAccionPrincipal.FlatAppearance.BorderSize = 0
            Me.btnCcnAccionPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnAccionPrincipal.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.btnCcnAccionPrincipal.ForeColor = System.Drawing.Color.White
            Me.btnCcnAccionPrincipal.Location = New System.Drawing.Point(12, 34)
            Me.btnCcnAccionPrincipal.Name = "btnCcnAccionPrincipal"
            Me.btnCcnAccionPrincipal.Size = New System.Drawing.Size(302, 32)
            Me.btnCcnAccionPrincipal.TabIndex = 1
            Me.btnCcnAccionPrincipal.Text = "🍳 Enviar a Cocina"
            Me.btnCcnAccionPrincipal.UseVisualStyleBackColor = False
            '
            'pnlCcnInfoPago
            '
            Me.pnlCcnInfoPago.Controls.Add(Me.lblCcnMontoTotal)
            Me.pnlCcnInfoPago.Controls.Add(Me.lblCcnMetodoPago)
            Me.pnlCcnInfoPago.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnInfoPago.Location = New System.Drawing.Point(12, 6)
            Me.pnlCcnInfoPago.Name = "pnlCcnInfoPago"
            Me.pnlCcnInfoPago.Size = New System.Drawing.Size(302, 28)
            Me.pnlCcnInfoPago.TabIndex = 0
            '
            'lblCcnMontoTotal
            '
            Me.lblCcnMontoTotal.Dock = System.Windows.Forms.DockStyle.Right
            Me.lblCcnMontoTotal.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnMontoTotal.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnMontoTotal.Location = New System.Drawing.Point(162, 0)
            Me.lblCcnMontoTotal.Name = "lblCcnMontoTotal"
            Me.lblCcnMontoTotal.Size = New System.Drawing.Size(140, 28)
            Me.lblCcnMontoTotal.TabIndex = 1
            Me.lblCcnMontoTotal.Text = "Total: $68.50"
            Me.lblCcnMontoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblCcnMetodoPago
            '
            Me.lblCcnMetodoPago.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblCcnMetodoPago.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular)
            Me.lblCcnMetodoPago.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnMetodoPago.Location = New System.Drawing.Point(0, 0)
            Me.lblCcnMetodoPago.Name = "lblCcnMetodoPago"
            Me.lblCcnMetodoPago.Size = New System.Drawing.Size(156, 28)
            Me.lblCcnMetodoPago.TabIndex = 0
            Me.lblCcnMetodoPago.Text = "💳 Tarjeta Crédito"
            Me.lblCcnMetodoPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'UcCcnTarjetaComanda
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.flpCcnPlatos)
            Me.Controls.Add(Me.pnlCcnFooterCard)
            Me.Controls.Add(Me.pnlCcnSubheader)
            Me.Controls.Add(Me.pnlCcnHeaderCard)
            Me.Controls.Add(Me.pnlCcnBordeSuperior)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.Margin = New System.Windows.Forms.Padding(8)
            Me.Name = "UcCcnTarjetaComanda"
            Me.Size = New System.Drawing.Size(326, 396)
            Me.pnlCcnHeaderCard.ResumeLayout(False)
            Me.pnlCcnHeaderCard.PerformLayout()
            Me.pnlCcnSubheader.ResumeLayout(False)
            Me.pnlCcnSubheader.PerformLayout()
            Me.pnlCcnFooterCard.ResumeLayout(False)
            Me.pnlCcnInfoPago.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCcnBordeSuperior As System.Windows.Forms.Panel
        Friend WithEvents pnlCcnHeaderCard As System.Windows.Forms.Panel
        Friend WithEvents lblCcnBadgeEstado As System.Windows.Forms.Label
        Friend WithEvents lblCcnTagMesa As System.Windows.Forms.Label
        Friend WithEvents pnlCcnSubheader As System.Windows.Forms.Panel
        Friend WithEvents lblCcnTiempoTranscurrido As System.Windows.Forms.Label
        Friend WithEvents lblCcnMozoOCliente As System.Windows.Forms.Label
        Friend WithEvents flpCcnPlatos As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents pnlCcnFooterCard As System.Windows.Forms.Panel
        Friend WithEvents btnCcnAccionPrincipal As System.Windows.Forms.Button
        Friend WithEvents pnlCcnInfoPago As System.Windows.Forms.Panel
        Friend WithEvents lblCcnMontoTotal As System.Windows.Forms.Label
        Friend WithEvents lblCcnMetodoPago As System.Windows.Forms.Label
    End Class
End Namespace
