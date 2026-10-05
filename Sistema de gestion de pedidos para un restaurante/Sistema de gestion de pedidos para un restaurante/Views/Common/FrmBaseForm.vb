Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Theme

Namespace Views.Common
    ''' <summary>
    ''' Clase Base de Formularios (POO).
    ''' Proporciona herencia visual unificada, doble búfer automático para evitar parpadeos,
    ''' y métodos de notificación y diálogo altamente reutilizables.
    ''' </summary>
    Public Class FrmBaseForm
        Inherits Form

        Public Sub New()
            MyBase.New()
            ThemeConfig.HabilitarDobleBuffer(Me)
        End Sub

        ''' <summary>
        ''' Aplica la temática visual oficial y estilos de controles.
        ''' Puede ser sobreescrito por cualquier formulario derivado (Polimorfismo).
        ''' </summary>
        Protected Overridable Sub AplicarTemaVisual()
            Me.BackColor = ThemeConfig.ColorBackgroundApp
        End Sub

        ''' <summary>
        ''' Muestra un diálogo estándar de confirmación con opciones Sí y No.
        ''' </summary>
        Protected Function ConfirmarAccion(mensaje As String, Optional titulo As String = "Confirmar Acción") As Boolean
            Return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
        End Function

        ''' <summary>
        ''' Muestra un mensaje modal de éxito estandarizado.
        ''' </summary>
        Protected Sub MostrarMensajeExito(mensaje As String, Optional titulo As String = "Operación Exitosa")
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        ''' <summary>
        ''' Muestra un mensaje modal de advertencia estandarizado.
        ''' </summary>
        Protected Sub MostrarMensajeAdvertencia(mensaje As String, Optional titulo As String = "Atención")
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

        ''' <summary>
        ''' Muestra un mensaje modal de error estandarizado.
        ''' </summary>
        Protected Sub MostrarMensajeError(mensaje As String, Optional titulo As String = "Error")
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

    End Class
End Namespace
