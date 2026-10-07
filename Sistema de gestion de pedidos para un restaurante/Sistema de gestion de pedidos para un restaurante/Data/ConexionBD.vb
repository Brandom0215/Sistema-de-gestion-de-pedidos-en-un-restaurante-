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
        Private _hostCustom As String = Nothing
        Private _puertoCustom As Integer? = Nothing
        Private _baseDatosCustom As String = Nothing
        Private _usuarioCustom As String = Nothing
        Private _claveCustom As String = Nothing
        Private _timeoutCustom As Integer? = Nothing

        ''' <summary>
        ''' IP del servidor PostgreSQL universitario (VM 106 asignada).
        ''' </summary>
        Public Property Host As String
            Get
                If Not String.IsNullOrWhiteSpace(_hostCustom) Then Return _hostCustom
                Return ObtenValorConfig("DB_HOST", "10.196.68.15")
            End Get
            Set(value As String)
                _hostCustom = value
            End Set
        End Property

        ''' <summary>
        ''' Puerto de red estándar de PostgreSQL.
        ''' </summary>
        Public Property Puerto As Integer
            Get
                If _puertoCustom.HasValue Then Return _puertoCustom.Value
                Return ObtenValorEnteroConfig("DB_PORT", 5432)
            End Get
            Set(value As Integer)
                _puertoCustom = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre de la base de datos oficial del restaurante.
        ''' </summary>
        Public Property BaseDatos As String
            Get
                If Not String.IsNullOrWhiteSpace(_baseDatosCustom) Then Return _baseDatosCustom
                Return ObtenValorConfig("DB_NAME", "restaurante_db")
            End Get
            Set(value As String)
                _baseDatosCustom = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario del sistema/base de datos.
        ''' </summary>
        Public Property Usuario As String
            Get
                If Not String.IsNullOrWhiteSpace(_usuarioCustom) Then Return _usuarioCustom
                Return ObtenValorConfig("DB_USER", "usuario_restaurante")
            End Get
            Set(value As String)
                _usuarioCustom = value
            End Set
        End Property

        ''' <summary>
        ''' Contraseña del servidor universitario.
        ''' </summary>
        Public Property Clave As String
            Get
                If Not String.IsNullOrWhiteSpace(_claveCustom) Then Return _claveCustom
                Return ObtenValorConfig("DB_PASS", "utpcocle15")
            End Get
            Set(value As String)
                _claveCustom = value
            End Set
        End Property

        ''' <summary>
        ''' Tiempo máximo de espera en segundos para detectar disponibilidad de red.
        ''' </summary>
        Public Property TimeoutSegundos As Integer
            Get
                If _timeoutCustom.HasValue Then Return _timeoutCustom.Value
                Return ObtenValorEnteroConfig("DB_TIMEOUT", 10)
            End Get
            Set(value As Integer)
                _timeoutCustom = value
            End Set
        End Property

        Private _configDict As Dictionary(Of String, String) = Nothing
        Private _configCargada As Boolean = False

        Private Sub CargarDiccionarioConfig()
            If _configCargada Then Return
            _configCargada = True

            If _configDict Is Nothing Then
                _configDict = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            End If

            ' Buscar archivo .env en directorio base, directorio actual y directorios padres (para depuracion en Visual Studio)
            Dim posiblesRutas As New List(Of String)()
            Dim dirBase As String = AppDomain.CurrentDomain.BaseDirectory
            Dim dirActual As String = IO.Directory.GetCurrentDirectory()

            If Not String.IsNullOrWhiteSpace(dirBase) Then
                posiblesRutas.Add(IO.Path.Combine(dirBase, ".env"))
                ' Buscar hasta 5 niveles arriba (para salir de bin/Debug/net10.0-windows)
                Dim dirPadre As IO.DirectoryInfo = IO.Directory.GetParent(dirBase)
                For i As Integer = 1 To 5
                    If dirPadre IsNot Nothing Then
                        posiblesRutas.Add(IO.Path.Combine(dirPadre.FullName, ".env"))
                        dirPadre = dirPadre.Parent
                    Else
                        Exit For
                    End If
                Next
            End If

            If Not String.IsNullOrWhiteSpace(dirActual) Then
                posiblesRutas.Add(IO.Path.Combine(dirActual, ".env"))
            End If

            For Each ruta In posiblesRutas
                If IO.File.Exists(ruta) Then
                    Try
                        Dim lineas = IO.File.ReadAllLines(ruta)
                        For Each linea In lineas
                            Dim txt = linea.Trim()
                            If String.IsNullOrWhiteSpace(txt) OrElse txt.StartsWith("#") OrElse txt.StartsWith("//") Then Continue For
                            If txt.Contains("="c) Then
                                Dim partes = txt.Split("="c, 2)
                                Dim k = partes(0).Trim()
                                Dim v = partes(1).Trim().Trim(""""c, "'"c)
                                If Not _configDict.ContainsKey(k) Then _configDict(k) = v
                            End If
                        Next
                        ' Si encontro y cargo el archivo .env, no necesita seguir buscando
                        If _configDict.Count > 0 Then Exit For
                    Catch
                    End Try
                End If
            Next
        End Sub

        Private Function ObtenValorConfig(nombreClave As String, valorPorDefecto As String) As String
            ' 1. Variable de Entorno del Sistema Operativo
            Dim envVal As String = Environment.GetEnvironmentVariable(nombreClave)
            If Not String.IsNullOrWhiteSpace(envVal) Then Return envVal.Trim()

            ' 2. Archivo .env local
            CargarDiccionarioConfig()
            If _configDict IsNot Nothing AndAlso _configDict.ContainsKey(nombreClave) AndAlso Not String.IsNullOrWhiteSpace(_configDict(nombreClave)) Then
                Return _configDict(nombreClave)
            End If

            ' 3. Valor por defecto genérico
            Return valorPorDefecto
        End Function

        Private Function ObtenValorEnteroConfig(nombreClave As String, valorPorDefecto As Integer) As Integer
            Dim strVal = ObtenValorConfig(nombreClave, "")
            Dim res As Integer = 0
            If Not String.IsNullOrWhiteSpace(strVal) AndAlso Integer.TryParse(strVal, res) Then
                Return res
            End If
            Return valorPorDefecto
        End Function

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
                .Pooling = True,
                .MinPoolSize = 1,
                .MaxPoolSize = 10,
                .ConnectionIdleLifetime = 15,
                .TcpKeepAlive = True
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
                AsegurarEstructuraTablas()
                mensajeDiagnostico = $"Conexión exitosa con el servidor en {Host}:{Puerto} ({BaseDatos})."
                UltimoMensajeEstado = $"Conectado al servidor principal ({Host})"
                Console.WriteLine($"[CONEXIÓN POSTGRESQL EXITOSA] Servidor: {Host}:{Puerto} | BaseDatos: {BaseDatos} | Usuario: {Usuario}")
                System.Diagnostics.Debug.WriteLine($"[CONEXIÓN POSTGRESQL EXITOSA] Servidor: {Host}:{Puerto} | BaseDatos: {BaseDatos}")
                Return True
            Catch ex As Exception
                mensajeDiagnostico = $"No se pudo contactar el servidor ({Host}:{Puerto}): {ex.Message}"
                UltimoMensajeEstado = "Servidor no disponible. Operando en modo local."
                Console.WriteLine($"[CONEXIÓN POSTGRESQL FALLIDA] Host: {Host}:{Puerto} | Error: {ex.Message}")
                System.Diagnostics.Debug.WriteLine($"[CONEXIÓN POSTGRESQL FALLIDA] Host: {Host}:{Puerto} | Error: {ex.Message}")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Ejecuta parches de estructura en la BD PostgreSQL para garantizar compatibilidad multidispositivo.
        ''' </summary>
        Public Sub AsegurarEstructuraTablas()
            If Not DebeUsarPostgreSQL() Then Return
            Try
                EjecutarComando("ALTER TABLE detalle_pedidos ALTER COLUMN id_plato DROP NOT NULL;")
            Catch
            End Try
            Try
                EjecutarComando("ALTER TABLE detalle_pedidos ADD COLUMN IF NOT EXISTS nombre_plato VARCHAR(150);")
            Catch
            End Try
        End Sub

        Private _ultimoIntentoReconexion As DateTime = DateTime.MinValue

        ''' <summary>
        ''' Determina si actualmente se debe intentar operar contra PostgreSQL.
        ''' Si está en modo local, reintenta automáticamente la conexión al servidor cada 10 segundos.
        ''' </summary>
        Public Function DebeUsarPostgreSQL() As Boolean
            If ModoConexion = ModoConexionEnum.ServidorPrincipal Then
                Return True
            End If

            If (DateTime.Now - _ultimoIntentoReconexion).TotalSeconds >= 10 Then
                _ultimoIntentoReconexion = DateTime.Now
                Dim diag As String = ""
                If ProbarConexion(diag) Then
                    _modoConexion = ModoConexionEnum.ServidorPrincipal
                    DispararModoConexionCambiado(ModoConexionEnum.ServidorPrincipal)
                    Return True
                End If
            End If

            Return False
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
