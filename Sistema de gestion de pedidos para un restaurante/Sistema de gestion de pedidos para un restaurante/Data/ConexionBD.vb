Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Npgsql

Namespace Data
    ''' <summary>
    ''' Enumeración de entornos de conexión del sistema.
    ''' </summary>
    Public Enum ModoConexionEnum
        ''' <summary>
        ''' Modo Servidor: Conexión activa al servidor PostgreSQL de la universidad (Modo principal del sistema).
        ''' </summary>
        ServidorPrincipal

        ''' <summary>
        ''' Modo Local: Modo de contingencia para trabajar cuando no hay conexión disponible al servidor.
        ''' </summary>
        ModoLocal
    End Enum

    ''' <summary>
    ''' Gestor centralizado de conexión a la Base de Datos PostgreSQL del Restaurante El Buen Sazón.
    ''' Su modo de operación principal es el servidor central de la universidad (VPS UTP Coclé).
    ''' En caso de interrupción de red o indisponibilidad, activa automáticamente el modo local de contingencia.
    ''' </summary>
    Public Module ConexionBD

        ' =========================================================================
        ' 1. CONFIGURACIÓN DEL SERVIDOR PRINCIPAL (VPS UTP COCLÉ)
        ' =========================================================================
        ''' <summary>
        ''' IP del servidor PostgreSQL universitario (Rango UTP Coclé: 10.196.68.12 al 15).
        ''' </summary>
        Public Property Host As String = "10.196.68.12"

        ''' <summary>
        ''' Puerto de red estándar de PostgreSQL.
        ''' </summary>
        Public Property Puerto As Integer = 5432

        ''' <summary>
        ''' Nombre de la base de datos oficial del restaurante.
        ''' </summary>
        Public Property BaseDatos As String = "RestauranteDB"

        ''' <summary>
        ''' Usuario del sistema/base de datos (Proporcionado: ubuntu).
        ''' </summary>
        Public Property Usuario As String = "ubuntu"

        ''' <summary>
        ''' Contraseña del servidor universitario (Proporcionada: utpcocle).
        ''' </summary>
        Public Property Clave As String = "utpcocle"

        ''' <summary>
        ''' Tiempo máximo de espera en segundos para detectar disponibilidad de red.
        ''' </summary>
        Public Property TimeoutSegundos As Integer = 3

        ' =========================================================================
        ' 2. ESTADO DEL SISTEMA Y GESTIÓN AUTOMÁTICA DE CONEXIÓN
        ' =========================================================================
        ' El modo principal y predeterminado es siempre el Servidor Central
        Private _modoConexion As ModoConexionEnum = ModoConexionEnum.ServidorPrincipal

        ''' <summary>
        ''' Entorno activo de datos. Por defecto intenta conectarse al servidor principal.
        ''' </summary>
        Public Property ModoConexion As ModoConexionEnum
            Get
                Return _modoConexion
            End Get
            Set(value As ModoConexionEnum)
                If _modoConexion <> value Then
                    _modoConexion = value
                    UltimoMensajeEstado = If(value = ModoConexionEnum.ServidorPrincipal,
                                             $"Conectado al servidor principal ({Host}:{Puerto})",
                                             "Servidor no disponible. Operando en modo local.")
                    DispararModoConexionCambiado(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Controla que la notificación amigable de cambio a modo local se presente una sola vez por sesión.
        ''' </summary>
        Public Property AvisoContingenciaMostrado As Boolean = False

        ''' <summary>
        ''' Mensaje legible del estado actual de la conexión.
        ''' </summary>
        Public Property UltimoMensajeEstado As String = "Iniciando conexión con el servidor principal..."

        ' Suscripciones de eventos para actualización reactiva en vistas
        Private ReadOnly _listenersModoConexion As New List(Of Action(Of ModoConexionEnum))()

        Public Sub SuscribirModoConexionCambiado(callback As Action(Of ModoConexionEnum))
            If callback IsNot Nothing AndAlso Not _listenersModoConexion.Contains(callback) Then
                _listenersModoConexion.Add(callback)
            End If
        End Sub

        Public Sub DesuscribirModoConexionCambiado(callback As Action(Of ModoConexionEnum))
            _listenersModoConexion.Remove(callback)
        End Sub

        Private Sub DispararModoConexionCambiado(nuevoModo As ModoConexionEnum)
            For Each cb In _listenersModoConexion.ToArray()
                Try
                    cb.Invoke(nuevoModo)
                Catch
                End Try
            Next
        End Sub

        ''' <summary>
        ''' Registra automáticamente que el servidor no respondió y activa el modo local de contingencia.
        ''' </summary>
        Public Sub RegistrarFalloServidor(Optional detalleError As String = "")
            If _modoConexion <> ModoConexionEnum.ModoLocal Then
                _modoConexion = ModoConexionEnum.ModoLocal
                UltimoMensajeEstado = "Servidor no disponible. Operando en modo local."
                DispararModoConexionCambiado(ModoConexionEnum.ModoLocal)
            End If
        End Sub

        ''' <summary>
        ''' Intenta reconectar con el servidor principal y actualiza el estado.
        ''' </summary>
        Public Function ReintentarConexionServidor(ByRef mensajeResultado As String) As Boolean
            Dim diagTecnico As String = ""
            Dim conectado As Boolean = ProbarConexion(diagTecnico)

            If conectado Then
                _modoConexion = ModoConexionEnum.ServidorPrincipal
                AvisoContingenciaMostrado = False
                UltimoMensajeEstado = $"Conectado al servidor principal ({Host})"
                mensajeResultado = "¡Excelente! Se ha restablecido la comunicación con el servidor principal del restaurante."
                DispararModoConexionCambiado(ModoConexionEnum.ServidorPrincipal)
                Return True
            Else
                _modoConexion = ModoConexionEnum.ModoLocal
                UltimoMensajeEstado = "Servidor no disponible. Operando en modo local."
                mensajeResultado = "El servidor principal aún no está disponible en esta red. El sistema continuará trabajando en modo local para no detener la operación."
                DispararModoConexionCambiado(ModoConexionEnum.ModoLocal)
                Return False
            End If
        End Function

        ''' <summary>
        ''' Retorna una descripción clara y breve del estado para mostrar en la interfaz de usuario.
        ''' </summary>
        Public Function ObtenerDescripcionModo() As String
            If ModoConexion = ModoConexionEnum.ServidorPrincipal Then
                Return $"🟢 Servidor ({Host})"
            Else
                Return "🟡 Modo Local (Sin Conexión)"
            End If
        End Function

        ''' <summary>
        ''' Construye la cadena de conexión para Npgsql.
        ''' </summary>
        Public Function ObtenerCadenaConexion() As String
            Dim builder As New NpgsqlConnectionStringBuilder() With {
                .Host = Host,
                .Port = Puerto,
                .Database = BaseDatos,
                .Username = Usuario,
                .Password = Clave,
                .Timeout = TimeoutSegundos,
                .CommandTimeout = 5,
                .Pooling = True
            }
            Return builder.ConnectionString
        End Function

        ''' <summary>
        ''' Crea una nueva instancia de conexión a PostgreSQL.
        ''' </summary>
        Public Function CrearConexion() As NpgsqlConnection
            Return New NpgsqlConnection(ObtenerCadenaConexion())
        End Function

        ''' <summary>
        ''' Realiza una prueba activa de conexión al servidor sin bloquear la interfaz.
        ''' </summary>
        Public Function ProbarConexion(ByRef mensajeDiagnostico As String) As Boolean
            Try
                Using conn As NpgsqlConnection = CrearConexion()
                    conn.Open()
                    Using cmd As New NpgsqlCommand("SELECT 1;", conn)
                        cmd.ExecuteScalar()
                    End Using
                End Using
                mensajeDiagnostico = $"Conexión exitosa con el servidor en {Host}:{Puerto} ({BaseDatos})."
                UltimoMensajeEstado = $"Conectado al servidor principal ({Host})"
                Return True
            Catch ex As Exception
                mensajeDiagnostico = $"No se pudo contactar el servidor ({Host}:{Puerto}): {ex.Message}"
                UltimoMensajeEstado = "Servidor no disponible. Operando en modo local."
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Determina si actualmente se debe intentar operar contra PostgreSQL.
        ''' </summary>
        Public Function DebeUsarPostgreSQL() As Boolean
            Return (ModoConexion = ModoConexionEnum.ServidorPrincipal)
        End Function

        ''' <summary>
        ''' Ejecuta una consulta SQL en PostgreSQL y retorna un DataTable con los resultados.
        ''' </summary>
        Public Function EjecutarConsultaDataTable(sql As String, ParamArray parametros() As NpgsqlParameter) As DataTable
            Dim dt As New DataTable()
            Using conn As NpgsqlConnection = CrearConexion()
                conn.Open()
                Using cmd As New NpgsqlCommand(sql, conn)
                    If parametros IsNot Nothing Then
                        cmd.Parameters.AddRange(parametros)
                    End If
                    Using da As New NpgsqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        ''' <summary>
        ''' Ejecuta un comando SQL de acción (INSERT, UPDATE, DELETE) en PostgreSQL.
        ''' </summary>
        Public Function EjecutarComando(sql As String, ParamArray parametros() As NpgsqlParameter) As Integer
            Using conn As NpgsqlConnection = CrearConexion()
                conn.Open()
                Using cmd As New NpgsqlCommand(sql, conn)
                    If parametros IsNot Nothing Then
                        cmd.Parameters.AddRange(parametros)
                    End If
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using
        End Function

    End Module
End Namespace
