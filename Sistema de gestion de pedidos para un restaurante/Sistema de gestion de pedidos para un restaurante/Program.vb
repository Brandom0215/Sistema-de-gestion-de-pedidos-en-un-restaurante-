Imports System
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Views

Namespace Global.Sistema_de_gestion_de_pedidos_para_un_restaurante
    ''' <summary>
    ''' Punto de entrada principal público para la aplicación WinForms en .NET.
    ''' </summary>
    Public Module Program
        <STAThread>
        Public Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Auth.FrmLogin())
        End Sub

    End Module
End Namespace
