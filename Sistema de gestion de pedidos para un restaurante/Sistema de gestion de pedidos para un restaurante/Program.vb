Imports System
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Views

Namespace Global.Sistema_de_gestion_de_pedidos_para_un_restaurante
    ''' <summary>
    ''' Punto de entrada principal público para la aplicación WinForms en .NET.
    ''' Inicia en la pantalla de autenticación Login.
    ''' </summary>
    Public Module Program
        <STAThread>
        Public Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
            AddHandler Application.ThreadException, Sub(s, e)
                Console.WriteLine($"[AVISO DEL SISTEMA] {e.Exception.Message}")
            End Sub

            Dim diagMsg As String = ""
            Dim esExitoso As Boolean = Data.ConexionBD.ProbarConexion(diagMsg)
            If Not esExitoso Then
                Data.ConexionBD.RegistrarFalloServidor(diagMsg)
            End If
            Console.WriteLine($"[DIAGNÓSTICO POSTGRESQL] {diagMsg}")

            Application.Run(New FrmHome("📲 Cliente (Autoatención)", "Invitado"))
        End Sub

    End Module
End Namespace
