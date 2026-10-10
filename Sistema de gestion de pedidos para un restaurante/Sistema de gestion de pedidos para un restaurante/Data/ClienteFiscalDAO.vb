Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports Npgsql
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services

Namespace Data
    ''' <summary>
    ''' Objeto de Acceso a Datos (DAO) para la gestión unificada de Clientes Fiscales (DGI Panamá).
    ''' Implementa validación estricta de entradas, límites de caracteres, prevención de duplicados
    ''' mediante RUC/Cédula y soporte híbrido (PostgreSQL con caché y respaldo local de contingencia).
    ''' </summary>
    Public Module ClienteFiscalDAO

        Private ReadOnly _clientesLocales As New Dictionary(Of String, ClienteFiscalModel)(StringComparer.OrdinalIgnoreCase)
        Private ReadOnly _lockObj As New Object()
        Private _ultimoIdLocal As Integer = 100

        Sub New()
            ' Inicializar clientes semilla en memoria para contingencia
            RegistrarEnMemoria(New ClienteFiscalModel(1, "8-800-1234", "55", "NATURAL", "Carlos Mendoza", "Panamá, Bella Vista", "+507 6200-1122", "carlos.mendoza@email.com", True))
            RegistrarEnMemoria(New ClienteFiscalModel(2, "8-750-5678", "12", "NATURAL", "Ana Lucía Torres", "Penonomé, Coclé", "+507 6555-8899", "ana.torres@email.com", True))
            RegistrarEnMemoria(New ClienteFiscalModel(3, "155698421-2-2024", "89", "JURIDICA", "Corporación Gastronómica S.A.", "Calle 50, Plaza Morazán", "+507 264-5500", "facturacion@corpgastro.pa", True))
            RegistrarEnMemoria(New ClienteFiscalModel(4, "8-912-3401", "33", "NATURAL", "David Moreno Castillo", "San Francisco, Calle 74", "+507 6890-1122", "david.moreno@gmail.com", True))
            RegistrarEnMemoria(New ClienteFiscalModel(5, "155700124-1-2023", "45", "JURIDICA", "Inversiones del Istmo S.A.", "Costa del Este, Torre Financial", "+507 300-8800", "pagos@inversionesistmo.pa", True))
            RegistrarEnMemoria(New ClienteFiscalModel(6, "8-605-4432", "78", "NATURAL", "Luis González", "El Cangrejo, Vía Argentina", "+507 6344-9988", "luis.gonzalez@outlook.com", True))
        End Sub

        Private Sub RegistrarEnMemoria(cliente As ClienteFiscalModel)
            SyncLock _lockObj
                If cliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cliente.RucCedula) Then
                    _clientesLocales(cliente.RucCedula) = cliente
                    If cliente.IdClienteFiscal >= _ultimoIdLocal Then
                        _ultimoIdLocal = cliente.IdClienteFiscal + 1
                    End If
                End If
            End SyncLock
        End Sub

        ''' <summary>
        ''' Busca un cliente fiscal por su RUC o Cédula normalizada.
        ''' </summary>
        Public Function BuscarPorRuc(rucCedula As String) As ClienteFiscalModel
            Dim rucLimpio As String = ValidadorEntrada.NormalizarRucCedula(rucCedula)
            If String.IsNullOrWhiteSpace(rucLimpio) Then Return Nothing

            ' 1. Intentar consulta en PostgreSQL si hay conexión activa
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT id_cliente_fiscal, ruc_cedula, dv, tipo_persona, razon_social, direccion_fiscal, telefono, correo, activo " &
                                        "FROM clientes_fiscales WHERE ruc_cedula = @ruc LIMIT 1;"
                    Dim pRuc As New NpgsqlParameter("@ruc", rucLimpio)
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, pRuc)
                    If dt.Rows.Count > 0 Then
                        Dim row = dt.Rows(0)
                        Dim model As New ClienteFiscalModel(
                            Convert.ToInt32(row("id_cliente_fiscal")),
                            row("ruc_cedula").ToString(),
                            If(row("dv") Is DBNull.Value, "", row("dv").ToString()),
                            row("tipo_persona").ToString(),
                            row("razon_social").ToString(),
                            If(row("direccion_fiscal") Is DBNull.Value, "", row("direccion_fiscal").ToString()),
                            If(row("telefono") Is DBNull.Value, "", row("telefono").ToString()),
                            If(row("correo") Is DBNull.Value, "", row("correo").ToString()),
                            Convert.ToBoolean(row("activo"))
                        )
                        RegistrarEnMemoria(model)
                        Return model
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            ' 2. Búsqueda en memoria local
            SyncLock _lockObj
                If _clientesLocales.ContainsKey(rucLimpio) Then
                    Return _clientesLocales(rucLimpio)
                End If
            End SyncLock

            Return Nothing
        End Function

        ''' <summary>
        ''' Obtiene un cliente fiscal existente o lo crea de forma atómica y sanitizada,
        ''' evitando duplicados de clientes en la base de datos (idempotencia fiscal).
        ''' </summary>
        Public Function ObtenerOCrearClienteFiscal(rucCedula As String,
                                                   razonSocial As String,
                                                   Optional dv As String = "",
                                                   Optional tipoPersona As String = "NATURAL",
                                                   Optional direccion As String = "",
                                                   Optional telefono As String = "",
                                                   Optional correo As String = "") As ClienteFiscalModel

            ' A. Validaciones y sanitización estricta de parámetros
            Dim rucLimpio As String = ValidadorEntrada.NormalizarRucCedula(rucCedula)
            If String.IsNullOrWhiteSpace(rucLimpio) Then
                rucLimpio = "CONSUMIDOR-FINAL"
                tipoPersona = "CONSUMIDOR_FINAL"
            End If

            Dim razonLimpia As String = ValidadorEntrada.SanitizarTextoSeguro(razonSocial, 120)
            If String.IsNullOrWhiteSpace(razonLimpia) Then
                razonLimpia = "Consumidor Final"
            End If

            Dim dvLimpio As String = ValidadorEntrada.SanitizarTextoSeguro(dv, 5).Trim()
            Dim direccionLimpia As String = ValidadorEntrada.SanitizarTextoSeguro(direccion, 150)
            Dim telefonoLimpio As String = ValidadorEntrada.SanitizarTextoSeguro(telefono, 30)
            Dim correoLimpio As String = If(ValidadorEntrada.EsCorreoValido(correo), correo.Trim().ToLowerInvariant(), "")

            ' B. Verificar si ya existe para REUTILIZAR y EVITAR DUPLICADOS
            Dim clienteExistente = BuscarPorRuc(rucLimpio)
            If clienteExistente IsNot Nothing Then
                ' Actualizar datos de contacto si antes no los tenía
                If String.IsNullOrWhiteSpace(clienteExistente.Telefono) AndAlso Not String.IsNullOrWhiteSpace(telefonoLimpio) Then
                    clienteExistente.Telefono = telefonoLimpio
                End If
                If String.IsNullOrWhiteSpace(clienteExistente.Correo) AndAlso Not String.IsNullOrWhiteSpace(correoLimpio) Then
                    clienteExistente.Correo = correoLimpio
                End If
                Return clienteExistente
            End If

            ' C. Inserción segura en PostgreSQL
            Dim nuevoId As Integer = 0
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Using conn = ConexionBD.CrearConexion()
                        conn.Open()
                        Dim sqlInsert As String = "INSERT INTO clientes_fiscales (ruc_cedula, dv, tipo_persona, razon_social, direccion_fiscal, telefono, correo, activo) " &
                                                  "VALUES (@ruc, @dv, @tipo, @razon, @dir, @tel, @email, TRUE) " &
                                                  "ON CONFLICT (ruc_cedula) DO UPDATE SET razon_social = EXCLUDED.razon_social " &
                                                  "RETURNING id_cliente_fiscal;"
                        Using cmd As New NpgsqlCommand(sqlInsert, conn)
                            cmd.Parameters.AddWithValue("@ruc", rucLimpio)
                            cmd.Parameters.AddWithValue("@dv", If(String.IsNullOrWhiteSpace(dvLimpio), CObj(DBNull.Value), CObj(dvLimpio)))
                            cmd.Parameters.AddWithValue("@tipo", tipoPersona.ToUpperInvariant())
                            cmd.Parameters.AddWithValue("@razon", razonLimpia)
                            cmd.Parameters.AddWithValue("@dir", If(String.IsNullOrWhiteSpace(direccionLimpia), CObj(DBNull.Value), CObj(direccionLimpia)))
                            cmd.Parameters.AddWithValue("@tel", If(String.IsNullOrWhiteSpace(telefonoLimpio), CObj(DBNull.Value), CObj(telefonoLimpio)))
                            cmd.Parameters.AddWithValue("@email", If(String.IsNullOrWhiteSpace(correoLimpio), CObj(DBNull.Value), CObj(correoLimpio)))
                            Dim res = cmd.ExecuteScalar()
                            If res IsNot Nothing AndAlso Not DBNull.Value.Equals(res) Then
                                nuevoId = Convert.ToInt32(res)
                            End If
                        End Using
                    End Using
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            ' Fallback / ID local si PostgreSQL no respondió
            If nuevoId <= 0 Then
                SyncLock _lockObj
                    _ultimoIdLocal += 1
                    nuevoId = _ultimoIdLocal
                End SyncLock
            End If

            Dim nuevoCliente As New ClienteFiscalModel(nuevoId, rucLimpio, dvLimpio, tipoPersona, razonLimpia, direccionLimpia, telefonoLimpio, correoLimpio, True)
            RegistrarEnMemoria(nuevoCliente)
            Return nuevoCliente
        End Function

        ''' <summary>
        ''' Obtiene todos los clientes fiscales registrados para listados y filtros de búsqueda.
        ''' </summary>
        Public Function ObtenerTodos() As List(Of ClienteFiscalModel)
            Dim lista As New List(Of ClienteFiscalModel)()

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT id_cliente_fiscal, ruc_cedula, dv, tipo_persona, razon_social, direccion_fiscal, telefono, correo, activo " &
                                        "FROM clientes_fiscales ORDER BY razon_social ASC;"
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    For Each row As DataRow In dt.Rows
                        Dim c As New ClienteFiscalModel(
                            Convert.ToInt32(row("id_cliente_fiscal")),
                            row("ruc_cedula").ToString(),
                            If(row("dv") Is DBNull.Value, "", row("dv").ToString()),
                            row("tipo_persona").ToString(),
                            row("razon_social").ToString(),
                            If(row("direccion_fiscal") Is DBNull.Value, "", row("direccion_fiscal").ToString()),
                            If(row("telefono") Is DBNull.Value, "", row("telefono").ToString()),
                            If(row("correo") Is DBNull.Value, "", row("correo").ToString()),
                            Convert.ToBoolean(row("activo"))
                        )
                        lista.Add(c)
                        RegistrarEnMemoria(c)
                    Next
                    If lista.Count > 0 Then Return lista
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Return _clientesLocales.Values.OrderBy(Function(x) x.RazonSocial).ToList()
            End SyncLock
        End Function

    End Module
End Namespace

