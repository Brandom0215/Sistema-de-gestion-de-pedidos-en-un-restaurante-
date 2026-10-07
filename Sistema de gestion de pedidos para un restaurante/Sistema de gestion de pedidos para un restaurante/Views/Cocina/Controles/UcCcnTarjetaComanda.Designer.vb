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
            Me.pnlCcnInfoClientePlato = New System.Windows.Forms.Panel()
            Me.pnlCcnMetadatos = New System.Windows.Forms.Panel()
            Me.lblCcnTiempoTranscurrido = New System.Windows.Forms.Label()
            Me.lblCcnTipoServicioMesa = New System.Windows.Forms.Label()
            Me.lblCcnNombreCliente = New System.Windows.Forms.Label()
            Me.picCcnMiniaturaPlato = New System.Windows.Forms.PictureBox()
            Me.pnlCcnCuerpoPlatos = New System.Windows.Forms.Panel()
            Me.pnlCcnFooterCard = New System.Windows.Forms.Panel()
            Me.btnCcnAccionPrincipal = New System.Windows.Forms.Button()
            Me.pnlCcnInfoPago = New System.Windows.Forms.Panel()
            Me.lblCcnMontoTotal = New System.Windows.Forms.Label()
            Me.lblCcnBadgePago = New System.Windows.Forms.Label()
            Me.pnlCcnHeaderCard.SuspendLayout()
            Me.pnlCcnInfoClientePlato.SuspendLayout()
            Me.pnlCcnMetadatos.SuspendLayout()
            CType(Me.picCcnMiniaturaPlato, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.pnlCcnBordeSuperior.Size = New System.Drawing.Size(338, 4)
            Me.pnlCcnBordeSuperior.TabIndex = 0
            '
            'pnlCcnHeaderCard
            '
            Me.pnlCcnHeaderCard.Controls.Add(Me.lblCcnBadgeEstado)
            Me.pnlCcnHeaderCard.Controls.Add(Me.lblCcnTagMesa)
            Me.pnlCcnHeaderCard.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnHeaderCard.Location = New System.Drawing.Point(0, 4)
            Me.pnlCcnHeaderCard.Name = "pnlCcnHeaderCard"
            Me.pnlCcnHeaderCard.Padding = New System.Windows.Forms.Padding(14, 10, 14, 4)
            Me.pnlCcnHeaderCard.Size = New System.Drawing.Size(370, 48)
            Me.pnlCcnHeaderCard.TabIndex = 1
            '
            'lblCcnBadgeEstado
            '
            Me.lblCcnBadgeEstado.BackColor = System.Drawing.Color.FromArgb(248, 236, 231)
            Me.lblCcnBadgeEstado.Dock = System.Windows.Forms.DockStyle.Right
            Me.lblCcnBadgeEstado.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnBadgeEstado.ForeColor = System.Drawing.Color.FromArgb(165, 82, 52)
            Me.lblCcnBadgeEstado.Location = New System.Drawing.Point(210, 10)
            Me.lblCcnBadgeEstado.Name = "lblCcnBadgeEstado"
            Me.lblCcnBadgeEstado.Size = New System.Drawing.Size(146, 34)
            Me.lblCcnBadgeEstado.TabIndex = 1
            Me.lblCcnBadgeEstado.Text = "En Cocina"
            Me.lblCcnBadgeEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblCcnBadgeEstado.UseMnemonic = False
            '
            'lblCcnTagMesa
            '
            Me.lblCcnTagMesa.AutoSize = True
            Me.lblCcnTagMesa.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblCcnTagMesa.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnTagMesa.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnTagMesa.Location = New System.Drawing.Point(14, 10)
            Me.lblCcnTagMesa.Name = "lblCcnTagMesa"
            Me.lblCcnTagMesa.Size = New System.Drawing.Size(160, 21)
            Me.lblCcnTagMesa.TabIndex = 0
            Me.lblCcnTagMesa.Text = "MESA 04 #08-1042"
            Me.lblCcnTagMesa.UseMnemonic = False
            '
            'pnlCcnInfoClientePlato
            '
            Me.pnlCcnInfoClientePlato.BackColor = System.Drawing.Color.FromArgb(250, 249, 246)
            Me.pnlCcnInfoClientePlato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pnlCcnInfoClientePlato.Controls.Add(Me.pnlCcnMetadatos)
            Me.pnlCcnInfoClientePlato.Controls.Add(Me.picCcnMiniaturaPlato)
            Me.pnlCcnInfoClientePlato.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnInfoClientePlato.Location = New System.Drawing.Point(0, 52)
            Me.pnlCcnInfoClientePlato.Name = "pnlCcnInfoClientePlato"
            Me.pnlCcnInfoClientePlato.Padding = New System.Windows.Forms.Padding(10, 8, 10, 8)
            Me.pnlCcnInfoClientePlato.Size = New System.Drawing.Size(370, 88)
            Me.pnlCcnInfoClientePlato.TabIndex = 2
            '
            'pnlCcnMetadatos
            '
            Me.pnlCcnMetadatos.Controls.Add(Me.lblCcnTiempoTranscurrido)
            Me.pnlCcnMetadatos.Controls.Add(Me.lblCcnTipoServicioMesa)
            Me.pnlCcnMetadatos.Controls.Add(Me.lblCcnNombreCliente)
            Me.pnlCcnMetadatos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCcnMetadatos.Location = New System.Drawing.Point(96, 8)
            Me.pnlCcnMetadatos.Name = "pnlCcnMetadatos"
            Me.pnlCcnMetadatos.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
            Me.pnlCcnMetadatos.Size = New System.Drawing.Size(262, 70)
            Me.pnlCcnMetadatos.TabIndex = 1
            '
            'lblCcnTiempoTranscurrido
            '
            Me.lblCcnTiempoTranscurrido.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnTiempoTranscurrido.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Regular)
            Me.lblCcnTiempoTranscurrido.ForeColor = System.Drawing.Color.FromArgb(120, 119, 115)
            Me.lblCcnTiempoTranscurrido.Location = New System.Drawing.Point(8, 44)
            Me.lblCcnTiempoTranscurrido.Name = "lblCcnTiempoTranscurrido"
            Me.lblCcnTiempoTranscurrido.Size = New System.Drawing.Size(254, 22)
            Me.lblCcnTiempoTranscurrido.TabIndex = 2
            Me.lblCcnTiempoTranscurrido.Text = "⏱ 13:45 (Hace 8m)"
            Me.lblCcnTiempoTranscurrido.UseMnemonic = False
            '
            'lblCcnTipoServicioMesa
            '
            Me.lblCcnTipoServicioMesa.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnTipoServicioMesa.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnTipoServicioMesa.ForeColor = System.Drawing.Color.FromArgb(117, 70, 50)
            Me.lblCcnTipoServicioMesa.Location = New System.Drawing.Point(8, 22)
            Me.lblCcnTipoServicioMesa.Name = "lblCcnTipoServicioMesa"
            Me.lblCcnTipoServicioMesa.Size = New System.Drawing.Size(254, 22)
            Me.lblCcnTipoServicioMesa.TabIndex = 1
            Me.lblCcnTipoServicioMesa.Text = "🪑 Comer en Local • Mesa 04"
            Me.lblCcnTipoServicioMesa.UseMnemonic = False
            '
            'lblCcnNombreCliente
            '
            Me.lblCcnNombreCliente.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblCcnNombreCliente.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnNombreCliente.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnNombreCliente.Location = New System.Drawing.Point(8, 0)
            Me.lblCcnNombreCliente.Name = "lblCcnNombreCliente"
            Me.lblCcnNombreCliente.Size = New System.Drawing.Size(254, 22)
            Me.lblCcnNombreCliente.TabIndex = 0
            Me.lblCcnNombreCliente.Text = "👤 Cliente: Carlos Mendoza"
            Me.lblCcnNombreCliente.UseMnemonic = False
            '
            'picCcnMiniaturaPlato
            '
            Me.picCcnMiniaturaPlato.BackColor = System.Drawing.Color.White
            Me.picCcnMiniaturaPlato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picCcnMiniaturaPlato.Cursor = System.Windows.Forms.Cursors.Hand
            Me.picCcnMiniaturaPlato.Dock = System.Windows.Forms.DockStyle.Left
            Me.picCcnMiniaturaPlato.Location = New System.Drawing.Point(10, 8)
            Me.picCcnMiniaturaPlato.Name = "picCcnMiniaturaPlato"
            Me.picCcnMiniaturaPlato.Size = New System.Drawing.Size(86, 70)
            Me.picCcnMiniaturaPlato.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.picCcnMiniaturaPlato.TabIndex = 0
            Me.picCcnMiniaturaPlato.TabStop = False
            '
            'pnlCcnCuerpoPlatos
            '
            Me.pnlCcnCuerpoPlatos.AutoScroll = True
            Me.pnlCcnCuerpoPlatos.BackColor = System.Drawing.Color.FromArgb(252, 251, 249)
            Me.pnlCcnCuerpoPlatos.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCcnCuerpoPlatos.Location = New System.Drawing.Point(0, 140)
            Me.pnlCcnCuerpoPlatos.Name = "pnlCcnCuerpoPlatos"
            Me.pnlCcnCuerpoPlatos.Padding = New System.Windows.Forms.Padding(14, 8, 14, 8)
            Me.pnlCcnCuerpoPlatos.Size = New System.Drawing.Size(370, 230)
            Me.pnlCcnCuerpoPlatos.TabIndex = 3
            '
            'pnlCcnFooterCard
            '
            Me.pnlCcnFooterCard.Controls.Add(Me.btnCcnAccionPrincipal)
            Me.pnlCcnFooterCard.Controls.Add(Me.pnlCcnInfoPago)
            Me.pnlCcnFooterCard.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.pnlCcnFooterCard.Location = New System.Drawing.Point(0, 370)
            Me.pnlCcnFooterCard.Name = "pnlCcnFooterCard"
            Me.pnlCcnFooterCard.Padding = New System.Windows.Forms.Padding(14, 8, 14, 12)
            Me.pnlCcnFooterCard.Size = New System.Drawing.Size(370, 108)
            Me.pnlCcnFooterCard.TabIndex = 4
            '
            'btnCcnAccionPrincipal
            '
            Me.btnCcnAccionPrincipal.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.btnCcnAccionPrincipal.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnCcnAccionPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
            Me.btnCcnAccionPrincipal.FlatAppearance.BorderSize = 0
            Me.btnCcnAccionPrincipal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnCcnAccionPrincipal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.btnCcnAccionPrincipal.ForeColor = System.Drawing.Color.White
            Me.btnCcnAccionPrincipal.Location = New System.Drawing.Point(14, 46)
            Me.btnCcnAccionPrincipal.Name = "btnCcnAccionPrincipal"
            Me.btnCcnAccionPrincipal.Size = New System.Drawing.Size(342, 50)
            Me.btnCcnAccionPrincipal.TabIndex = 1
            Me.btnCcnAccionPrincipal.Text = "👨‍🍳 Iniciar Preparación"
            Me.btnCcnAccionPrincipal.UseVisualStyleBackColor = False
            Me.btnCcnAccionPrincipal.UseMnemonic = False
            '
            'pnlCcnInfoPago
            '
            Me.pnlCcnInfoPago.Controls.Add(Me.lblCcnMontoTotal)
            Me.pnlCcnInfoPago.Controls.Add(Me.lblCcnBadgePago)
            Me.pnlCcnInfoPago.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlCcnInfoPago.Location = New System.Drawing.Point(14, 8)
            Me.pnlCcnInfoPago.Name = "pnlCcnInfoPago"
            Me.pnlCcnInfoPago.Size = New System.Drawing.Size(342, 38)
            Me.pnlCcnInfoPago.TabIndex = 0
            '
            'lblCcnMontoTotal
            '
            Me.lblCcnMontoTotal.Dock = System.Windows.Forms.DockStyle.Right
            Me.lblCcnMontoTotal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
            Me.lblCcnMontoTotal.ForeColor = System.Drawing.Color.FromArgb(41, 43, 38)
            Me.lblCcnMontoTotal.Location = New System.Drawing.Point(172, 0)
            Me.lblCcnMontoTotal.Name = "lblCcnMontoTotal"
            Me.lblCcnMontoTotal.Size = New System.Drawing.Size(170, 38)
            Me.lblCcnMontoTotal.TabIndex = 1
            Me.lblCcnMontoTotal.Text = "Total: $68.50"
            Me.lblCcnMontoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.lblCcnMontoTotal.UseMnemonic = False
            '
            'lblCcnBadgePago
            '
            Me.lblCcnBadgePago.BackColor = System.Drawing.Color.FromArgb(228, 237, 224)
            Me.lblCcnBadgePago.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblCcnBadgePago.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
            Me.lblCcnBadgePago.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblCcnBadgePago.Location = New System.Drawing.Point(0, 0)
            Me.lblCcnBadgePago.Name = "lblCcnBadgePago"
            Me.lblCcnBadgePago.Size = New System.Drawing.Size(170, 38)
            Me.lblCcnBadgePago.TabIndex = 0
            Me.lblCcnBadgePago.Text = "🟢 PAGADO"
            Me.lblCcnBadgePago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.lblCcnBadgePago.UseMnemonic = False
            '
            'UcCcnTarjetaComanda
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Controls.Add(Me.pnlCcnCuerpoPlatos)
            Me.Controls.Add(Me.pnlCcnFooterCard)
            Me.Controls.Add(Me.pnlCcnInfoClientePlato)
            Me.Controls.Add(Me.pnlCcnHeaderCard)
            Me.Controls.Add(Me.pnlCcnBordeSuperior)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            Me.Margin = New System.Windows.Forms.Padding(12)
            Me.Name = "UcCcnTarjetaComanda"
            Me.Size = New System.Drawing.Size(370, 480)
            Me.pnlCcnHeaderCard.ResumeLayout(False)
            Me.pnlCcnHeaderCard.PerformLayout()
            Me.pnlCcnInfoClientePlato.ResumeLayout(False)
            Me.pnlCcnMetadatos.ResumeLayout(False)
            CType(Me.picCcnMiniaturaPlato, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlCcnFooterCard.ResumeLayout(False)
            Me.pnlCcnInfoPago.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCcnBordeSuperior As System.Windows.Forms.Panel
        Friend WithEvents pnlCcnHeaderCard As System.Windows.Forms.Panel
        Friend WithEvents lblCcnBadgeEstado As System.Windows.Forms.Label
        Friend WithEvents lblCcnTagMesa As System.Windows.Forms.Label
        Friend WithEvents pnlCcnInfoClientePlato As System.Windows.Forms.Panel
        Friend WithEvents picCcnMiniaturaPlato As System.Windows.Forms.PictureBox
        Friend WithEvents pnlCcnMetadatos As System.Windows.Forms.Panel
        Friend WithEvents lblCcnNombreCliente As System.Windows.Forms.Label
        Friend WithEvents lblCcnTipoServicioMesa As System.Windows.Forms.Label
        Friend WithEvents lblCcnTiempoTranscurrido As System.Windows.Forms.Label
        Friend WithEvents pnlCcnCuerpoPlatos As System.Windows.Forms.Panel
        Friend WithEvents pnlCcnFooterCard As System.Windows.Forms.Panel
        Friend WithEvents btnCcnAccionPrincipal As System.Windows.Forms.Button
        Friend WithEvents pnlCcnInfoPago As System.Windows.Forms.Panel
        Friend WithEvents lblCcnMontoTotal As System.Windows.Forms.Label
        Friend WithEvents lblCcnBadgePago As System.Windows.Forms.Label
    End Class
End Namespace
