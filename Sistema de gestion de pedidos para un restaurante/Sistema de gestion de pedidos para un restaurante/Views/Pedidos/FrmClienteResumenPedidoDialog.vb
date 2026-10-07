Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Pedidos
    ''' <summary>
    ''' Cuadro de diálogo de confirmación y rectificación final del pedido del cliente.
    ''' Muestra el resumen completo de cliente, correo, servicio, método de pago, ítems y extras.
    ''' </summary>
    Public Class FrmClienteResumenPedidoDialog

        Private ReadOnly _nombreCliente As String
        Private ReadOnly _correoCliente As String
        Private ReadOnly _servicioMesa As String
        Private ReadOnly _metodoPago As String
        Private ReadOnly _dtCarrito As DataTable
        Private ReadOnly _listaExtras As List(Of Tuple(Of String, Integer, Decimal))
        Private ReadOnly _montoTotal As Decimal

        Public Sub New(nombreCliente As String,
                       correoCliente As String,
                       servicioMesa As String,
                       metodoPago As String,
                       dtCarrito As DataTable,
                       listaExtras As List(Of Tuple(Of String, Integer, Decimal)),
                       montoTotal As Decimal)

            InitializeComponent()

            _nombreCliente = nombreCliente
            _correoCliente = correoCliente
            _servicioMesa = servicioMesa
            _metodoPago = metodoPago
            _dtCarrito = dtCarrito
            _listaExtras = listaExtras
            _montoTotal = montoTotal

            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        Private Sub FrmClienteResumenPedidoDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AplicarTemaVisualDialog()
            CargarDatosCabecera()
            CargarGrillaResumen()
        End Sub

        Private Sub AplicarTemaVisualDialog()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
            grpDatosClienteDialog.ForeColor = ThemeConfig.ColorNeutralDark
            grpDesglose.ForeColor = ThemeConfig.ColorNeutralDark
            pnlPieTotales.BackColor = Color.White
            ThemeConfig.AplicarEstiloTarjeta(pnlPieTotales)

            ThemeConfig.EstilizarBotonPrimario(btnConfirmarFinal)
            ThemeConfig.EstilizarBotonSecundario(btnModificar)
            ThemeConfig.ConfigurarGrillaTouch(dgvResumen)
        End Sub

        Private Sub CargarDatosCabecera()
            lblValCliente.Text = $"Cliente: {_nombreCliente}"
            lblValCorreo.Text = $"Correo: {_correoCliente}"
            lblValServicio.Text = $"Servicio: {_servicioMesa}"
            lblValMetodoPago.Text = $"Método de Pago: {_metodoPago}"
            lblMontoTotalDialog.Text = $"$ {_montoTotal:N2}"
        End Sub

        Private Sub CargarGrillaResumen()
            Dim dtTablaResumen As New DataTable("ResumenItems")
            dtTablaResumen.Columns.Add("Cant", GetType(Integer))
            dtTablaResumen.Columns.Add("Descripción", GetType(String))
            dtTablaResumen.Columns.Add("Precio Unitario", GetType(Decimal))
            dtTablaResumen.Columns.Add("Subtotal", GetType(Decimal))

            ' 1. Agregar platos del carrito
            If _dtCarrito IsNot Nothing Then
                For Each r As DataRow In _dtCarrito.Rows
                    Dim drNew As DataRow = dtTablaResumen.NewRow()
                    drNew("Cant") = Convert.ToInt32(r("Cant"))
                    drNew("Descripción") = r("Plato").ToString()
                    drNew("Precio Unitario") = Convert.ToDecimal(r("Precio"))
                    drNew("Subtotal") = Convert.ToDecimal(r("Subtotal"))
                    dtTablaResumen.Rows.Add(drNew)
                Next
            End If

            ' 2. Agregar bebidas y extras
            If _listaExtras IsNot Nothing Then
                For Each t In _listaExtras
                    Dim drNew As DataRow = dtTablaResumen.NewRow()
                    drNew("Cant") = t.Item2
                    drNew("Descripción") = $"[EXTRA/BEBIDA] {t.Item1}"
                    drNew("Precio Unitario") = t.Item3
                    drNew("Subtotal") = t.Item2 * t.Item3
                    dtTablaResumen.Rows.Add(drNew)
                Next
            End If

            dgvResumen.DataSource = dtTablaResumen

            If dgvResumen.Columns.Count > 0 Then
                dgvResumen.Columns("Cant").Width = 65
                dgvResumen.Columns("Descripción").Width = 320
                dgvResumen.Columns("Precio Unitario").Width = 110
                dgvResumen.Columns("Subtotal").Width = 110

                dgvResumen.Columns("Precio Unitario").DefaultCellStyle.Format = "$ #,##0.00"
                dgvResumen.Columns("Subtotal").DefaultCellStyle.Format = "$ #,##0.00"
            End If
        End Sub

    End Class
End Namespace
