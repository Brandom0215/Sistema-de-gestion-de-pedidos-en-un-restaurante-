Imports System.Drawing
Imports System.Windows.Forms

Namespace Views.Pedidos
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class FrmClienteResumenPedidoDialog
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
            Me.pnlHeaderDialog = New System.Windows.Forms.Panel()
            Me.lblTituloDialog = New System.Windows.Forms.Label()
            Me.lblSubtituloDialog = New System.Windows.Forms.Label()

            Me.pnlContenido = New System.Windows.Forms.Panel()
            Me.grpDatosClienteDialog = New System.Windows.Forms.GroupBox()
            Me.lblValCliente = New System.Windows.Forms.Label()
            Me.lblValCorreo = New System.Windows.Forms.Label()
            Me.lblValServicio = New System.Windows.Forms.Label()
            Me.lblValMetodoPago = New System.Windows.Forms.Label()

            Me.grpDesglose = New System.Windows.Forms.GroupBox()
            Me.dgvResumen = New System.Windows.Forms.DataGridView()

            Me.pnlPieTotales = New System.Windows.Forms.Panel()
            Me.lblEtiquetaTotalDialog = New System.Windows.Forms.Label()
            Me.lblMontoTotalDialog = New System.Windows.Forms.Label()
            Me.btnModificar = New System.Windows.Forms.Button()
            Me.btnConfirmarFinal = New System.Windows.Forms.Button()

            Me.pnlHeaderDialog.SuspendLayout()
            Me.pnlContenido.SuspendLayout()
            Me.grpDatosClienteDialog.SuspendLayout()
            Me.grpDesglose.SuspendLayout()
            CType(Me.dgvResumen, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pnlPieTotales.SuspendLayout()
            Me.SuspendLayout()

            ' pnlHeaderDialog
            Me.pnlHeaderDialog.BackColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.pnlHeaderDialog.Controls.Add(Me.lblSubtituloDialog)
            Me.pnlHeaderDialog.Controls.Add(Me.lblTituloDialog)
            Me.pnlHeaderDialog.Dock = System.Windows.Forms.DockStyle.Top
            Me.pnlHeaderDialog.Location = New System.Drawing.Point(0, 0)
            Me.pnlHeaderDialog.Name = "pnlHeaderDialog"
            Me.pnlHeaderDialog.Size = New System.Drawing.Size(680, 75)
            Me.pnlHeaderDialog.TabIndex = 0

            ' lblTituloDialog
            Me.lblTituloDialog.AutoSize = True
            Me.lblTituloDialog.Font = New System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold)
            Me.lblTituloDialog.ForeColor = System.Drawing.Color.White
            Me.lblTituloDialog.Location = New System.Drawing.Point(16, 12)
            Me.lblTituloDialog.Name = "lblTituloDialog"
            Me.lblTituloDialog.Size = New System.Drawing.Size(430, 28)
            Me.lblTituloDialog.TabIndex = 0
            Me.lblTituloDialog.Text = "Resumen y Rectificación del Pedido"

            ' lblSubtituloDialog
            Me.lblSubtituloDialog.AutoSize = True
            Me.lblSubtituloDialog.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            Me.lblSubtituloDialog.ForeColor = System.Drawing.Color.FromArgb(244, 243, 237)
            Me.lblSubtituloDialog.Location = New System.Drawing.Point(16, 43)
            Me.lblSubtituloDialog.Name = "lblSubtituloDialog"
            Me.lblSubtituloDialog.Size = New System.Drawing.Size(530, 17)
            Me.lblSubtituloDialog.TabIndex = 1
            Me.lblSubtituloDialog.Text = "EL BUEN SAZÓN — Verifique los ítems y datos antes de enviar a cocina y facturación."

            ' pnlContenido
            Me.pnlContenido.Controls.Add(Me.pnlPieTotales)
            Me.pnlContenido.Controls.Add(Me.grpDesglose)
            Me.pnlContenido.Controls.Add(Me.grpDatosClienteDialog)
            Me.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pnlContenido.Location = New System.Drawing.Point(0, 75)
            Me.pnlContenido.Name = "pnlContenido"
            Me.pnlContenido.Padding = New System.Windows.Forms.Padding(12)
            Me.pnlContenido.Size = New System.Drawing.Size(680, 575)
            Me.pnlContenido.TabIndex = 1

            ' grpDatosClienteDialog
            Me.grpDatosClienteDialog.Controls.Add(Me.lblValMetodoPago)
            Me.grpDatosClienteDialog.Controls.Add(Me.lblValServicio)
            Me.grpDatosClienteDialog.Controls.Add(Me.lblValCorreo)
            Me.grpDatosClienteDialog.Controls.Add(Me.lblValCliente)
            Me.grpDatosClienteDialog.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.grpDatosClienteDialog.Location = New System.Drawing.Point(12, 10)
            Me.grpDatosClienteDialog.Name = "grpDatosClienteDialog"
            Me.grpDatosClienteDialog.Size = New System.Drawing.Size(656, 110)
            Me.grpDatosClienteDialog.TabIndex = 0
            Me.grpDatosClienteDialog.TabStop = False
            Me.grpDatosClienteDialog.Text = "Datos del Cliente & Tipo de Cobro"

            ' lblValCliente
            Me.lblValCliente.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblValCliente.Location = New System.Drawing.Point(12, 26)
            Me.lblValCliente.Name = "lblValCliente"
            Me.lblValCliente.Size = New System.Drawing.Size(310, 32)
            Me.lblValCliente.TabIndex = 0
            Me.lblValCliente.Text = "Cliente: --"

            ' lblValCorreo
            Me.lblValCorreo.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblValCorreo.Location = New System.Drawing.Point(330, 26)
            Me.lblValCorreo.Name = "lblValCorreo"
            Me.lblValCorreo.Size = New System.Drawing.Size(314, 32)
            Me.lblValCorreo.TabIndex = 1
            Me.lblValCorreo.Text = "Correo: --"

            ' lblValServicio
            Me.lblValServicio.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblValServicio.Location = New System.Drawing.Point(12, 65)
            Me.lblValServicio.Name = "lblValServicio"
            Me.lblValServicio.Size = New System.Drawing.Size(310, 32)
            Me.lblValServicio.TabIndex = 2
            Me.lblValServicio.Text = "Servicio: --"

            ' lblValMetodoPago
            Me.lblValMetodoPago.Font = New System.Drawing.Font("Segoe UI", 10.0F)
            Me.lblValMetodoPago.Location = New System.Drawing.Point(330, 65)
            Me.lblValMetodoPago.Name = "lblValMetodoPago"
            Me.lblValMetodoPago.Size = New System.Drawing.Size(314, 32)
            Me.lblValMetodoPago.TabIndex = 3
            Me.lblValMetodoPago.Text = "Método de Pago: --"

            ' grpDesglose
            Me.grpDesglose.Controls.Add(Me.dgvResumen)
            Me.grpDesglose.Font = New System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold)
            Me.grpDesglose.Location = New System.Drawing.Point(12, 126)
            Me.grpDesglose.Name = "grpDesglose"
            Me.grpDesglose.Size = New System.Drawing.Size(656, 300)
            Me.grpDesglose.TabIndex = 1
            Me.grpDesglose.TabStop = False
            Me.grpDesglose.Text = "Rectificación de Productos y Extras Seleccionados"

            ' dgvResumen
            Me.dgvResumen.AllowUserToAddRows = False
            Me.dgvResumen.AllowUserToDeleteRows = False
            Me.dgvResumen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.dgvResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgvResumen.Location = New System.Drawing.Point(12, 24)
            Me.dgvResumen.MultiSelect = False
            Me.dgvResumen.Name = "dgvResumen"
            Me.dgvResumen.ReadOnly = True
            Me.dgvResumen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgvResumen.Size = New System.Drawing.Size(632, 262)
            Me.dgvResumen.TabIndex = 0

            ' pnlPieTotales
            Me.pnlPieTotales.BackColor = System.Drawing.Color.White
            Me.pnlPieTotales.Controls.Add(Me.btnConfirmarFinal)
            Me.pnlPieTotales.Controls.Add(Me.btnModificar)
            Me.pnlPieTotales.Controls.Add(Me.lblMontoTotalDialog)
            Me.pnlPieTotales.Controls.Add(Me.lblEtiquetaTotalDialog)
            Me.pnlPieTotales.Location = New System.Drawing.Point(12, 434)
            Me.pnlPieTotales.Name = "pnlPieTotales"
            Me.pnlPieTotales.Size = New System.Drawing.Size(656, 125)
            Me.pnlPieTotales.TabIndex = 2

            ' lblEtiquetaTotalDialog
            Me.lblEtiquetaTotalDialog.AutoSize = True
            Me.lblEtiquetaTotalDialog.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.lblEtiquetaTotalDialog.Location = New System.Drawing.Point(12, 14)
            Me.lblEtiquetaTotalDialog.Name = "lblEtiquetaTotalDialog"
            Me.lblEtiquetaTotalDialog.Size = New System.Drawing.Size(185, 21)
            Me.lblEtiquetaTotalDialog.TabIndex = 0
            Me.lblEtiquetaTotalDialog.Text = "TOTAL NETO A PAGAR:"

            ' lblMontoTotalDialog
            Me.lblMontoTotalDialog.Font = New System.Drawing.Font("Segoe UI", 20.0F, System.Drawing.FontStyle.Bold)
            Me.lblMontoTotalDialog.ForeColor = System.Drawing.Color.FromArgb(198, 107, 72)
            Me.lblMontoTotalDialog.Location = New System.Drawing.Point(220, 4)
            Me.lblMontoTotalDialog.Name = "lblMontoTotalDialog"
            Me.lblMontoTotalDialog.Size = New System.Drawing.Size(424, 38)
            Me.lblMontoTotalDialog.TabIndex = 1
            Me.lblMontoTotalDialog.Text = "$ 0.00"
            Me.lblMontoTotalDialog.TextAlign = System.Drawing.ContentAlignment.MiddleRight

            ' btnModificar
            Me.btnModificar.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnModificar.Font = New System.Drawing.Font("Segoe UI", 11.0F, System.Drawing.FontStyle.Bold)
            Me.btnModificar.Location = New System.Drawing.Point(12, 58)
            Me.btnModificar.Name = "btnModificar"
            Me.btnModificar.Size = New System.Drawing.Size(260, 52)
            Me.btnModificar.TabIndex = 2
            Me.btnModificar.Text = "Modificar Pedido"
            Me.btnModificar.UseVisualStyleBackColor = True

            ' btnConfirmarFinal
            Me.btnConfirmarFinal.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.btnConfirmarFinal.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Bold)
            Me.btnConfirmarFinal.Location = New System.Drawing.Point(285, 58)
            Me.btnConfirmarFinal.Name = "btnConfirmarFinal"
            Me.btnConfirmarFinal.Size = New System.Drawing.Size(359, 52)
            Me.btnConfirmarFinal.TabIndex = 3
            Me.btnConfirmarFinal.Text = "Confirmar y Enviar Pedido"
            Me.btnConfirmarFinal.UseVisualStyleBackColor = True

            ' FrmClienteResumenPedidoDialog
            Me.AcceptButton = Me.btnConfirmarFinal
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnModificar
            Me.ClientSize = New System.Drawing.Size(680, 650)
            Me.Controls.Add(Me.pnlContenido)
            Me.Controls.Add(Me.pnlHeaderDialog)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmClienteResumenPedidoDialog"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Resumen y Rectificación de Pedido"
            Me.pnlHeaderDialog.ResumeLayout(False)
            Me.pnlHeaderDialog.PerformLayout()
            Me.pnlContenido.ResumeLayout(False)
            Me.grpDatosClienteDialog.ResumeLayout(False)
            Me.grpDesglose.ResumeLayout(False)
            CType(Me.dgvResumen, System.ComponentModel.ISupportInitialize).EndInit()
            Me.pnlPieTotales.ResumeLayout(False)
            Me.pnlPieTotales.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents pnlHeaderDialog As Panel
        Friend WithEvents lblTituloDialog As Label
        Friend WithEvents lblSubtituloDialog As Label
        Friend WithEvents pnlContenido As Panel
        Friend WithEvents grpDatosClienteDialog As GroupBox
        Friend WithEvents lblValCliente As Label
        Friend WithEvents lblValCorreo As Label
        Friend WithEvents lblValServicio As Label
        Friend WithEvents lblValMetodoPago As Label
        Friend WithEvents grpDesglose As GroupBox
        Friend WithEvents dgvResumen As DataGridView
        Friend WithEvents pnlPieTotales As Panel
        Friend WithEvents lblEtiquetaTotalDialog As Label
        Friend WithEvents lblMontoTotalDialog As Label
        Friend WithEvents btnModificar As Button
        Friend WithEvents btnConfirmarFinal As Button
    End Class
End Namespace
