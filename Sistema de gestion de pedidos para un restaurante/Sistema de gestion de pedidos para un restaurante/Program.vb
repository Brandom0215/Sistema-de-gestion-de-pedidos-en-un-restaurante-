Imports System
Imports System.Windows.Forms
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Views

Namespace Global.Sistema_de_gestion_de_pedidos_para_un_restaurante
    ''' <summary>
    ''' Punto de entrada principal público para la aplicación WinForms en .NET.
    ''' Inicia directamente en el Módulo del Cliente (Menú Digital / Autoservicio sin login previo).
    ''' </summary>
    Public Module Program
        <STAThread>
        Public Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)

            ' Diagnóstico inicial de conexión al servidor
            Dim diagMsg As String = ""
            Dim esExitoso As Boolean = Data.ConexionBD.ProbarConexion(diagMsg)
            Console.WriteLine("==================================================================")
            Console.WriteLine($"[DIAGNÓSTICO INICIAL] {diagMsg}")
            Console.WriteLine("==================================================================")

            ' Si la conexión falla, mostrar una alerta clara al usuario/compañero en pantalla
            If Not esExitoso Then
                MessageBox.Show($"Atención de Conexión al Servidor PostgreSQL:{Environment.NewLine}{Environment.NewLine}{diagMsg}{Environment.NewLine}{Environment.NewLine}El sistema continuará funcionando en Modo Local de contingencia.", "Estado de Red - Restaurante El Buen Sazón", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            Application.Run(New FrmHome("📲 Cliente (Autoatención)", "Invitado"))
        End Sub

    End Module
End Namespace
