Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Pedidos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmPedidoConfirmado
        Inherits System.Windows.Forms.Form

        'Form reemplaza a Dispose para limpiar la lista de componentes.
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
            Me.pnlCardModal = New System.Windows.Forms.Panel()
            Me.lblTituloModal = New System.Windows.Forms.Label()
            Me.lblSubtituloModal = New System.Windows.Forms.Label()
            Me.picPlatoPrincipal = New System.Windows.Forms.PictureBox()
            Me.pnlDetalles = New System.Windows.Forms.Panel()
            Me.lblInfoCliente = New System.Windows.Forms.Label()
            Me.lblInfoMesaServicio = New System.Windows.Forms.Label()
            Me.lblInfoPlato = New System.Windows.Forms.Label()
            Me.lblInfoAcompanamientos = New System.Windows.Forms.Label()
            Me.lblEstadoPedido = New System.Windows.Forms.Label()
            Me.btnCerrarModal = New System.Windows.Forms.Button()

            Me.pnlCardModal.SuspendLayout()
            CType(Me.picPlatoPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlDetalles.SuspendLayout()
            Me.SuspendLayout()
            '
            ' pnlCardModal
            '
            Me.pnlCardModal.BackColor = System.Drawing.Color.White
            Me.pnlCardModal.Controls.Add(Me.btnCerrarModal)
            Me.pnlCardModal.Controls.Add(Me.lblEstadoPedido)
            Me.pnlCardModal.Controls.Add(Me.pnlDetalles)
            Me.pnlCardModal.Controls.Add(Me.picPlatoPrincipal)
            Me.pnlCardModal.Controls.Add(Me.lblSubtituloModal)
            Me.pnlCardModal.Controls.Add(Me.lblTituloModal)
            Me.pnlCardModal.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlCardModal.Location = New System.Drawing.Point(15, 15)
            Me.pnlCardModal.Name = "pnlCardModal"
            Me.pnlCardModal.Padding = New System.Windows.Forms.Padding(20)
            Me.pnlCardModal.Size = New System.Drawing.Size(520, 470)
            Me.pnlCardModal.TabIndex = 0
            '
            ' lblTituloModal
            '
            Me.lblTituloModal.AutoSize = True
            Me.lblTituloModal.Font = New System.Drawing.Font("Segoe UI", 14.0F, System.Drawing.FontStyle.Bold)
            Me.lblTituloModal.Location = New System.Drawing.Point(20, 20)
            Me.lblTituloModal.Name = "lblTituloModal"
            Me.lblTituloModal.Size = New System.Drawing.Size(250, 25)
            Me.lblTituloModal.TabIndex = 0
            Me.lblTituloModal.Text = "✅ Pedido Confirmado #00"
            Me.lblTituloModal.UseMnemonic = False
            '
            ' lblSubtituloModal
            '
            Me.lblSubtituloModal.AutoSize = True
            Me.lblSubtituloModal.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblSubtituloModal.ForeColor = System.Drawing.Color.Gray
            Me.lblSubtituloModal.Location = New System.Drawing.Point(22, 48)
            Me.lblSubtituloModal.Name = "lblSubtituloModal"
            Me.lblSubtituloModal.Size = New System.Drawing.Size(325, 15)
            Me.lblSubtituloModal.TabIndex = 1
            Me.lblSubtituloModal.Text = "Comanda lista para envío a la pantalla de cocina y facturación."
            Me.lblSubtituloModal.UseMnemonic = False
            '
            ' picPlatoPrincipal
            '
            Me.picPlatoPrincipal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.picPlatoPrincipal.Location = New System.Drawing.Point(25, 75)
            Me.picPlatoPrincipal.Name = "picPlatoPrincipal"
            Me.picPlatoPrincipal.Size = New System.Drawing.Size(470, 200)
            Me.picPlatoPrincipal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
            Me.picPlatoPrincipal.TabIndex = 2
            Me.picPlatoPrincipal.TabStop = False
            '
            ' pnlDetalles
            '
            Me.pnlDetalles.BackColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.pnlDetalles.Controls.Add(Me.lblInfoAcompanamientos)
            Me.pnlDetalles.Controls.Add(Me.lblInfoPlato)
            Me.pnlDetalles.Controls.Add(Me.lblInfoMesaServicio)
            Me.pnlDetalles.Controls.Add(Me.lblInfoCliente)
            Me.pnlDetalles.Location = New System.Drawing.Point(25, 290)
            Me.pnlDetalles.Name = "pnlDetalles"
            Me.pnlDetalles.Padding = New System.Windows.Forms.Padding(12)
            Me.pnlDetalles.Size = New System.Drawing.Size(470, 115)
            Me.pnlDetalles.TabIndex = 3
            '
            ' lblInfoCliente
            '
            Me.lblInfoCliente.AutoSize = True
            Me.lblInfoCliente.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblInfoCliente.Location = New System.Drawing.Point(12, 12)
            Me.lblInfoCliente.Name = "lblInfoCliente"
            Me.lblInfoCliente.Size = New System.Drawing.Size(126, 17)
            Me.lblInfoCliente.TabIndex = 0
            Me.lblInfoCliente.Text = "👤 Cliente: Juan Pérez"
            Me.lblInfoCliente.UseMnemonic = False
            '
            ' lblInfoMesaServicio
            '
            Me.lblInfoMesaServicio.AutoSize = True
            Me.lblInfoMesaServicio.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblInfoMesaServicio.Location = New System.Drawing.Point(12, 35)
            Me.lblInfoMesaServicio.Name = "lblInfoMesaServicio"
            Me.lblInfoMesaServicio.Size = New System.Drawing.Size(210, 15)
            Me.lblInfoMesaServicio.TabIndex = 1
            Me.lblInfoMesaServicio.Text = "🪑 Servicio: En Mesa • Mesa #04"
            Me.lblInfoMesaServicio.UseMnemonic = False
            '
            ' lblInfoPlato
            '
            Me.lblInfoPlato.AutoSize = True
            Me.lblInfoPlato.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.lblInfoPlato.Location = New System.Drawing.Point(12, 58)
            Me.lblInfoPlato.Name = "lblInfoPlato"
            Me.lblInfoPlato.Size = New System.Drawing.Size(240, 15)
            Me.lblInfoPlato.TabIndex = 2
            Me.lblInfoPlato.Text = "🍽️ Plato: Lomo a la Brasa ($18.50)"
            Me.lblInfoPlato.UseMnemonic = False
            '
            ' lblInfoAcompanamientos
            '
            Me.lblInfoAcompanamientos.AutoSize = True
            Me.lblInfoAcompanamientos.Font = New System.Drawing.Font("Segoe UI", 9.0F)
            Me.lblInfoAcompanamientos.Location = New System.Drawing.Point(12, 80)
            Me.lblInfoAcompanamientos.Name = "lblInfoAcompanamientos"
            Me.lblInfoAcompanamientos.Size = New System.Drawing.Size(310, 15)
            Me.lblInfoAcompanamientos.TabIndex = 3
            Me.lblInfoAcompanamientos.Text = "🍟 Acompañamientos: Papas Fritas, Ensalada Fresca"
            Me.lblInfoAcompanamientos.UseMnemonic = False
            '
            ' lblEstadoPedido
            '
            Me.lblEstadoPedido.AutoSize = True
            Me.lblEstadoPedido.Font = New System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            Me.lblEstadoPedido.ForeColor = System.Drawing.Color.FromArgb(89, 107, 75)
            Me.lblEstadoPedido.Location = New System.Drawing.Point(25, 422)
            Me.lblEstadoPedido.Name = "lblEstadoPedido"
            Me.lblEstadoPedido.Size = New System.Drawing.Size(260, 17)
            Me.lblEstadoPedido.TabIndex = 4
            Me.lblEstadoPedido.Text = "🟢 Estado: ENVIADO A COCINA (KDS)"
            Me.lblEstadoPedido.UseMnemonic = False
            '
            ' btnCerrarModal
            '
            Me.btnCerrarModal.Font = New System.Drawing.Font("Segoe UI", 9.0F, System.Drawing.FontStyle.Bold)
            Me.btnCerrarModal.Location = New System.Drawing.Point(375, 415)
            Me.btnCerrarModal.Name = "btnCerrarModal"
            Me.btnCerrarModal.Size = New System.Drawing.Size(120, 32)
            Me.btnCerrarModal.TabIndex = 5
            Me.btnCerrarModal.Text = "Cerrar"
            Me.btnCerrarModal.UseVisualStyleBackColor = True
            Me.btnCerrarModal.UseMnemonic = False
            '
            ' FrmPedidoConfirmado
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(239, 236, 230)
            Me.ClientSize = New System.Drawing.Size(550, 500)
            Me.Controls.Add(Me.pnlCardModal)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmPedidoConfirmado"
            Me.Padding = New System.Windows.Forms.Padding(15)
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Segunda Interfaz — Pedido Confirmado (PictureBox)"
            Me.pnlCardModal.ResumeLayout(False)
            Me.pnlCardModal.PerformLayout()
            CType(Me.picPlatoPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlDetalles.ResumeLayout(False)
            Me.pnlDetalles.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlCardModal As Panel
        Friend WithEvents lblTituloModal As Label
        Friend WithEvents lblSubtituloModal As Label
        Friend WithEvents picPlatoPrincipal As PictureBox
        Friend WithEvents pnlDetalles As Panel
        Friend WithEvents lblInfoCliente As Label
        Friend WithEvents lblInfoMesaServicio As Label
        Friend WithEvents lblInfoPlato As Label
        Friend WithEvents lblInfoAcompanamientos As Label
        Friend WithEvents lblEstadoPedido As Label
        Friend WithEvents btnCerrarModal As Button
    End Class
End Namespace
