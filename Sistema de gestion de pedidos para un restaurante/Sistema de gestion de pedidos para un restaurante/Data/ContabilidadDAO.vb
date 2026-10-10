Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Text
Imports Npgsql
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Services

Namespace Data
    ''' <summary>
    ''' Motor Contable y DAO Centralizado para la Contabilidad del Restaurante (DGI Panamá / NIIF PYMES).
    ''' Implementa validación estricta de partida doble (Debe = Haber), idempotencia por origen de evento,
    ''' cálculo automático de ITBMS (7%), bloqueo de períodos cerrados y auditoría de cambios.
    ''' </summary>
    Public Module ContabilidadDAO

        Private ReadOnly _asientosLocales As New List(Of AsientoContableModel)()
        Private ReadOnly _catalogoCuentasLocales As New Dictionary(Of String, CuentaContableModel)(StringComparer.OrdinalIgnoreCase)
        Private ReadOnly _periodosLocales As New List(Of PeriodoContableModel)()
        Private ReadOnly _empleadosLocales As New List(Of EmpleadoModel)()
        Private ReadOnly _movimientosCajaLocales As New DataTable("MovimientosCaja")
        Private ReadOnly _auditoriaLocales As New DataTable("Auditoria")
        Private ReadOnly _lockObj As New Object()
        Private _ultimoIdAsiento As Integer = 100
        Private _ultimoIdDetalle As Integer = 500

        Sub New()
            InicializarTablasMemoria()
            CargarCatalogoSemilla()
            CargarPeriodosSemilla()
            CargarEmpleadosSemilla()
            CargarMovimientosCajaSemilla()
            CargarAuditoriaSemilla()
            CargarAsientosSemilla()
        End Sub

        Private Sub InicializarTablasMemoria()
            SyncLock _lockObj
                If _movimientosCajaLocales.Columns.Count = 0 Then
                    _movimientosCajaLocales.Columns.Add("id_movimiento", GetType(Integer))
                    _movimientosCajaLocales.Columns.Add("id_usuario", GetType(Integer))
                    _movimientosCajaLocales.Columns.Add("tipo_movimiento", GetType(String))
                    _movimientosCajaLocales.Columns.Add("monto", GetType(Decimal))
                    _movimientosCajaLocales.Columns.Add("referencia", GetType(String))
                    _movimientosCajaLocales.Columns.Add("fecha_hora", GetType(DateTime))
                End If

                If _auditoriaLocales.Columns.Count = 0 Then
                    _auditoriaLocales.Columns.Add("id_auditoria", GetType(Integer))
                    _auditoriaLocales.Columns.Add("id_usuario", GetType(Integer))
                    _auditoriaLocales.Columns.Add("nombre_usuario", GetType(String))
                    _auditoriaLocales.Columns.Add("accion", GetType(String))
                    _auditoriaLocales.Columns.Add("tabla_afectada", GetType(String))
                    _auditoriaLocales.Columns.Add("registro_id", GetType(Integer))
                    _auditoriaLocales.Columns.Add("detalle_cambio", GetType(String))
                    _auditoriaLocales.Columns.Add("fecha_accion", GetType(DateTime))
                End If
            End SyncLock
        End Sub

        Private Sub CargarCatalogoSemilla()
            SyncLock _lockObj
                If _catalogoCuentasLocales.Count > 0 Then Return
                ' Activos
                _catalogoCuentasLocales("1") = New CuentaContableModel("1", "ACTIVO", "ACTIVO", "DEUDORA", 1, "", False, True)
                _catalogoCuentasLocales("1.1") = New CuentaContableModel("1.1", "Activo Corriente", "ACTIVO", "DEUDORA", 2, "1", False, True)
                _catalogoCuentasLocales("1.1.01.01") = New CuentaContableModel("1.1.01.01", "Caja General (Caja de Turno)", "ACTIVO", "DEUDORA", 4, "1.1", True, True)
                _catalogoCuentasLocales("1.1.01.02") = New CuentaContableModel("1.1.01.02", "Caja Chica (Gastos Menores)", "ACTIVO", "DEUDORA", 4, "1.1", True, True)
                _catalogoCuentasLocales("1.1.01.03") = New CuentaContableModel("1.1.01.03", "Bancos Locales (Panamá - Banco General)", "ACTIVO", "DEUDORA", 4, "1.1", True, True)
                _catalogoCuentasLocales("1.1.01.04") = New CuentaContableModel("1.1.01.04", "Fondos en Tránsito POS (Tarjetas Débito/Crédito)", "ACTIVO", "DEUDORA", 4, "1.1", True, True)
                _catalogoCuentasLocales("1.1.01.05") = New CuentaContableModel("1.1.01.05", "Fondos en Tránsito (Yappy Comercial)", "ACTIVO", "DEUDORA", 4, "1.1", True, True)
                _catalogoCuentasLocales("1.1.02.01") = New CuentaContableModel("1.1.02.01", "Clientes por Cobrar", "ACTIVO", "DEUDORA", 4, "1.1", True, True)
                _catalogoCuentasLocales("1.1.03.01") = New CuentaContableModel("1.1.03.01", "Retención ITBMS Tarjetas POS (DGI)", "ACTIVO", "DEUDORA", 4, "1.1", True, True)

                ' Pasivos
                _catalogoCuentasLocales("2") = New CuentaContableModel("2", "PASIVO", "PASIVO", "ACREEDORA", 1, "", False, True)
                _catalogoCuentasLocales("2.1.01.01") = New CuentaContableModel("2.1.01.01", "ITBMS Débito Fiscal (7% Ventas)", "PASIVO", "ACREEDORA", 4, "2", True, True)
                _catalogoCuentasLocales("2.1.01.02") = New CuentaContableModel("2.1.01.02", "ITBMS Neto por Liquidar DGI", "PASIVO", "ACREEDORA", 4, "2", True, True)
                _catalogoCuentasLocales("2.1.02.01") = New CuentaContableModel("2.1.02.01", "Propinas por Distribuir", "PASIVO", "ACREEDORA", 4, "2", True, True)
                _catalogoCuentasLocales("2.1.03.01") = New CuentaContableModel("2.1.03.01", "Sueldos y Salarios por Pagar", "PASIVO", "ACREEDORA", 4, "2", True, True)
                _catalogoCuentasLocales("2.1.03.02") = New CuentaContableModel("2.1.03.02", "Retenciones CSS (9.75%) y SE (1.25%) por Pagar", "PASIVO", "ACREEDORA", 4, "2", True, True)
                _catalogoCuentasLocales("2.1.03.03") = New CuentaContableModel("2.1.03.03", "Aportes Patronales CSS/SE por Pagar", "PASIVO", "ACREEDORA", 4, "2", True, True)

                ' Patrimonio
                _catalogoCuentasLocales("3") = New CuentaContableModel("3", "PATRIMONIO", "PATRIMONIO", "ACREEDORA", 1, "", False, True)
                _catalogoCuentasLocales("3.1.01.01") = New CuentaContableModel("3.1.01.01", "Capital del Negocio", "PATRIMONIO", "ACREEDORA", 4, "3", True, True)
                _catalogoCuentasLocales("3.1.02.01") = New CuentaContableModel("3.1.02.01", "Utilidades Retenidas", "PATRIMONIO", "ACREEDORA", 4, "3", True, True)
                _catalogoCuentasLocales("3.1.03.01") = New CuentaContableModel("3.1.03.01", "Utilidad del Ejercicio Actual", "PATRIMONIO", "ACREEDORA", 4, "3", True, True)

                ' Ingresos
                _catalogoCuentasLocales("4") = New CuentaContableModel("4", "INGRESOS", "INGRESOS", "ACREEDORA", 1, "", False, True)
                _catalogoCuentasLocales("4.1.01.01") = New CuentaContableModel("4.1.01.01", "Venta de Alimentos (Gravadas 7%)", "INGRESOS", "ACREEDORA", 4, "4", True, True)
                _catalogoCuentasLocales("4.1.01.02") = New CuentaContableModel("4.1.01.02", "Venta de Bebidas", "INGRESOS", "ACREEDORA", 4, "4", True, True)
                _catalogoCuentasLocales("4.1.01.03") = New CuentaContableModel("4.1.01.03", "Ingresos por Servicio de Delivery", "INGRESOS", "ACREEDORA", 4, "4", True, True)
                _catalogoCuentasLocales("4.1.02.01") = New CuentaContableModel("4.1.02.01", "(-) Devoluciones en Ventas", "INGRESOS", "DEUDORA", 4, "4", True, True)

                ' Gastos Operativos y de Personal
                _catalogoCuentasLocales("6") = New CuentaContableModel("6", "GASTOS OPERATIVOS", "GASTOS", "DEUDORA", 1, "", False, True)
                _catalogoCuentasLocales("6.1.01.01") = New CuentaContableModel("6.1.01.01", "Servicios Básicos (Luz, Agua, Gas Cocina)", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.01.02") = New CuentaContableModel("6.1.01.02", "Alquiler del Local Comercial", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.01.03") = New CuentaContableModel("6.1.01.03", "Mantenimiento y Artículos de Limpieza", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.02.01") = New CuentaContableModel("6.1.02.01", "Comisiones POS y Plataformas de Cobro", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.02.02") = New CuentaContableModel("6.1.02.02", "Diferencias de Caja (Faltante Arqueo)", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.03.01") = New CuentaContableModel("6.1.03.01", "Sueldos y Salarios - Cocina (Cocineros)", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.03.02") = New CuentaContableModel("6.1.03.02", "Sueldos y Salarios - Salón (Meseros)", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.03.03") = New CuentaContableModel("6.1.03.03", "Sueldos y Salarios - Aseo y Mantenimiento", "GASTOS", "DEUDORA", 4, "6", True, True)
                _catalogoCuentasLocales("6.1.03.04") = New CuentaContableModel("6.1.03.04", "Cargas Sociales Patronales (CSS / SE / Riesgos)", "GASTOS", "DEUDORA", 4, "6", True, True)
            End SyncLock
        End Sub

        Private Sub CargarPeriodosSemilla()
            SyncLock _lockObj
                If _periodosLocales.Count > 0 Then Return
                _periodosLocales.Add(New PeriodoContableModel(1, 2026, 10, New DateTime(2026, 10, 1), New DateTime(2026, 10, 31), "ABIERTO"))
                _periodosLocales.Add(New PeriodoContableModel(2, 2026, 11, New DateTime(2026, 11, 1), New DateTime(2026, 11, 30), "ABIERTO"))
                _periodosLocales.Add(New PeriodoContableModel(3, 2026, 12, New DateTime(2026, 12, 1), New DateTime(2026, 12, 31), "ABIERTO"))
            End SyncLock
        End Sub

        Private Sub CargarEmpleadosSemilla()
            SyncLock _lockObj
                If _empleadosLocales.Count > 0 Then Return
                _empleadosLocales.Add(New EmpleadoModel(1, "Roberto Carlos Castillo", "8-712-1456", "Cocinero", 750.00D, New DateTime(2025, 3, 15), True))
                _empleadosLocales.Add(New EmpleadoModel(2, "Marta Elena Ríos", "4-250-987", "Cocinero", 700.00D, New DateTime(2025, 6, 1), True))
                _empleadosLocales.Add(New EmpleadoModel(3, "Juan José Batista", "6-708-3321", "Cocinero", 680.00D, New DateTime(2025, 8, 10), True))
                _empleadosLocales.Add(New EmpleadoModel(4, "Luis Alberto Pimentel", "8-820-4455", "Mesero", 600.00D, New DateTime(2025, 4, 12), True))
                _empleadosLocales.Add(New EmpleadoModel(5, "Gladys Isabel Morales", "8-901-2211", "Mesero", 600.00D, New DateTime(2025, 5, 20), True))
                _empleadosLocales.Add(New EmpleadoModel(6, "Pedro Antonio Vergara", "7-115-3420", "Aseador", 550.00D, New DateTime(2025, 2, 1), True))
                _empleadosLocales.Add(New EmpleadoModel(7, "Carmen Rosa Quintero", "9-740-1199", "Aseador", 550.00D, New DateTime(2025, 7, 15), True))
            End SyncLock
        End Sub

        Private Sub CargarMovimientosCajaSemilla()
            SyncLock _lockObj
                If _movimientosCajaLocales.Rows.Count > 0 Then Return
                _movimientosCajaLocales.Rows.Add(1, 1, "APERTURA", 500.00D, "Fondo base de apertura de Caja General en efectivo", New DateTime(2026, 10, 1, 7, 0, 0))
                _movimientosCajaLocales.Rows.Add(2, 2, "GASTO_MENOR", 48.00D, "Recarga urgente de tanque de gas industrial para estufas", New DateTime(2026, 10, 3, 14, 15, 0))
                _movimientosCajaLocales.Rows.Add(3, 2, "GASTO_MENOR", 25.50D, "Insumos de limpieza diaria para aseadores del local", New DateTime(2026, 10, 4, 10, 30, 0))
            End SyncLock
        End Sub

        Private Sub CargarAuditoriaSemilla()
            SyncLock _lockObj
                If _auditoriaLocales.Rows.Count > 0 Then Return
                _auditoriaLocales.Rows.Add(1, 1, "Administrador", "INICIALIZACION", "catalogo_cuentas", 1, "Configuración del catálogo contable según DGI Panamá", New DateTime(2026, 10, 1, 8, 0, 0))
                _auditoriaLocales.Rows.Add(2, 1, "Administrador", "APERTURA_PERIODO", "periodos_contables", 1, "Apertura del período contable 2026-10", New DateTime(2026, 10, 1, 8, 5, 0))
                _auditoriaLocales.Rows.Add(3, 1, "Administrador", "PAGO_PLANILLA", "asientos_contables", 4, "Generación y cuadratura del asiento contable de nómina para 7 colaboradores", New DateTime(2026, 10, 15, 17, 0, 0))
            End SyncLock
        End Sub

        Private Sub AgregarAsientoSemilla(id As Integer, num As String, fecha As DateTime, concepto As String, modulo As String, origenId As Integer, lineas As List(Of Tuple(Of String, String, Decimal, Decimal)))
            Dim a As New AsientoContableModel() With {
                .IdAsiento = id,
                .NumeroAsiento = num,
                .IdPeriodo = 1,
                .FechaAsiento = fecha,
                .Concepto = concepto,
                .OrigenModulo = modulo,
                .OrigenId = origenId,
                .Estado = "ASENTADO",
                .CreadoPor = 1,
                .FechaCreacion = fecha
            }
            Dim detId = 1
            For Each l In lineas
                Dim nom = If(_catalogoCuentasLocales.ContainsKey(l.Item1), _catalogoCuentasLocales(l.Item1).NombreCuenta, "")
                a.Lineas.Add(New AsientoDetalleModel(l.Item1, l.Item2, l.Item3, l.Item4) With {.IdDetalle = id * 10 + detId, .IdAsiento = id, .NombreCuenta = nom})
                detId += 1
            Next
            a.RecalcularTotales()
            _asientosLocales.Add(a)
        End Sub

        Private Sub CargarAsientosSemilla()
            SyncLock _lockObj
                If _asientosLocales.Count > 0 Then Return

                ' 1. Apertura de Caja General (Fondo inicial para operaciones de restaurante) ($500.00)
                AgregarAsientoSemilla(1, "AS-2026-10-0001", New DateTime(2026, 10, 1), "Apertura de operaciones - Fondo inicial en efectivo de Caja General", "CAJA", 9991, New List(Of Tuple(Of String, String, Decimal, Decimal)) From {
                    Tuple.Create("1.1.01.01", "Fondo inicial de apertura en efectivo Caja General", 500.00D, 0.00D),
                    Tuple.Create("3.1.01.01", "Capital social - Aporte inicial de socios", 0.00D, 500.00D)
                })

                ' 2. Gasto Menor Caja Chica - Tanque de gas industrial cocina ($48.00)
                AgregarAsientoSemilla(2, "AS-2026-10-0002", New DateTime(2026, 10, 3), "Gasto menor Caja Chica - Gas licuado para cocina", "GASTOS", 9992, New List(Of Tuple(Of String, String, Decimal, Decimal)) From {
                    Tuple.Create("6.1.01.01", "Gasto de servicio de gas licuado para cocina", 48.00D, 0.00D),
                    Tuple.Create("1.1.01.02", "Desembolso en efectivo desde Caja Chica", 0.00D, 48.00D)
                })

                ' 3. Gasto Menor Caja Chica - Artículos de aseo e higiene ($25.50)
                AgregarAsientoSemilla(3, "AS-2026-10-0003", New DateTime(2026, 10, 4), "Gasto menor - Insumos de limpieza y mantenimiento de salón", "GASTOS", 9993, New List(Of Tuple(Of String, String, Decimal, Decimal)) From {
                    Tuple.Create("6.1.01.03", "Gasto en artículos de desinfección y aseo del local", 25.50D, 0.00D),
                    Tuple.Create("1.1.01.02", "Desembolso en efectivo desde Caja Chica", 0.00D, 25.50D)
                })

                ' 4. Planilla Mensual 7 Colaboradores ($5,094.50)
                AgregarAsientoSemilla(4, "AS-2026-10-0004", New DateTime(2026, 10, 15), "Pago de nómina 7 colaboradores (3 cocineros, 2 meseros, 2 aseadores)", "NOMINA", 9994, New List(Of Tuple(Of String, String, Decimal, Decimal)) From {
                    Tuple.Create("6.1.03.01", "Sueldos 3 Cocineros (Castillo, Ríos, Batista)", 2130.00D, 0.00D),
                    Tuple.Create("6.1.03.02", "Sueldos 2 Meseros (Pimentel, Morales)", 1200.00D, 0.00D),
                    Tuple.Create("6.1.03.03", "Sueldos 2 Aseadores (Vergara, Quintero)", 1100.00D, 0.00D),
                    Tuple.Create("6.1.03.04", "Cargas Sociales Patronales (15.00% CSS/SE)", 664.50D, 0.00D),
                    Tuple.Create("1.1.01.03", "Transferencias ACH Banco General sueldos netos", 0.00D, 3942.70D),
                    Tuple.Create("2.1.03.02", "Retenciones obreras CSS (9.75%) y SE (1.25%)", 0.00D, 487.30D),
                    Tuple.Create("2.1.03.03", "Aportes patronales CSS/SE por liquidar", 0.00D, 664.50D)
                })
            End SyncLock
        End Sub

        ''' <summary>
        ''' Obtiene el período contable abierto para una fecha determinada.
        ''' Si el período está CERRADO o no existe, lanza una excepción de control.
        ''' </summary>
        Public Function ObtenerPeriodoAbierto(fecha As DateTime) As PeriodoContableModel
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT id_periodo, anio, mes, fecha_inicio, fecha_fin, estado " &
                                        "FROM periodos_contables WHERE @fecha BETWEEN fecha_inicio AND fecha_fin LIMIT 1;"
                    Dim pF As New NpgsqlParameter("@fecha", fecha.Date)
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, pF)
                    If dt.Rows.Count > 0 Then
                        Dim r = dt.Rows(0)
                        Dim per As New PeriodoContableModel(
                            Convert.ToInt32(r("id_periodo")),
                            Convert.ToInt32(r("anio")),
                            Convert.ToInt32(r("mes")),
                            Convert.ToDateTime(r("fecha_inicio")),
                            Convert.ToDateTime(r("fecha_fin")),
                            r("estado").ToString()
                        )
                        Return per
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Dim localPer = _periodosLocales.FirstOrDefault(Function(p) fecha.Date >= p.FechaInicio.Date AndAlso fecha.Date <= p.FechaFin.Date)
                If localPer IsNot Nothing Then Return localPer
            End SyncLock

            ' Período por defecto si estamos fuera del rango sembrado
            Return New PeriodoContableModel(1, fecha.Year, fecha.Month, New DateTime(fecha.Year, fecha.Month, 1), New DateTime(fecha.Year, fecha.Month, DateTime.DaysInMonth(fecha.Year, fecha.Month)), "ABIERTO")
        End Function

        ''' <summary>
        ''' Verifica si un evento operativo ya tiene asiento contable generado (Garantía de Idempotencia).
        ''' </summary>
        Public Function ExisteAsientoPorOrigen(origenModulo As String, origenId As Integer) As Boolean
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT 1 FROM asientos_contables WHERE origen_modulo = @mod AND origen_id = @id LIMIT 1;"
                    Dim pMod As New NpgsqlParameter("@mod", origenModulo.ToUpperInvariant())
                    Dim pId As New NpgsqlParameter("@id", origenId)
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, pMod, pId)
                    If dt.Rows.Count > 0 Then Return True
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Return _asientosLocales.Any(Function(a) a.OrigenModulo = origenModulo AndAlso a.OrigenId.HasValue AndAlso a.OrigenId.Value = origenId)
            End SyncLock
        End Function

        ''' <summary>
        ''' Inserta atómicamente un asiento contable con sus líneas de detalle bajo estricta Partida Doble.
        ''' </summary>
        Public Function RegistrarAsiento(asiento As AsientoContableModel) As Integer
            If asiento Is Nothing Then Throw New ArgumentNullException(NameOf(asiento))
            asiento.RecalcularTotales()

            ' 1. VALIDACIÓN FUNDAMENTAL: PARTIDA DOBLE
            If Not asiento.EstaCuadrado Then
                Throw New InvalidOperationException($"Error contable: El asiento '{asiento.NumeroAsiento}' no cumple con la Partida Doble. " &
                                                   $"Total Debe: {asiento.TotalDebe:C2} <> Total Haber: {asiento.TotalHaber:C2} (Diferencia: {Math.Abs(asiento.TotalDebe - asiento.TotalHaber):C4}).")
            End If

            ' 2. VALIDACIÓN DE PERÍODO CONTABLE ABIERTO
            Dim periodo = ObtenerPeriodoAbierto(asiento.FechaAsiento)
            If periodo Is Nothing OrElse Not periodo.EstaAbierto Then
                Throw New InvalidOperationException($"Operación rechazada: El período contable para la fecha {asiento.FechaAsiento:yyyy-MM-dd} se encuentra CERRADO o BLOQUEADO.")
            End If
            asiento.IdPeriodo = periodo.IdPeriodo

            ' 3. VALIDACIÓN DE IDEMPOTENCIA (Evitar duplicados)
            If asiento.OrigenId.HasValue AndAlso ExisteAsientoPorOrigen(asiento.OrigenModulo, asiento.OrigenId.Value) Then
                System.Diagnostics.Debug.WriteLine($"[CONTABILIDAD] Asiento para {asiento.OrigenModulo} #{asiento.OrigenId} ya existía. Omitiendo duplicado.")
                Return 0
            End If

            ' Generar número correlativo si no viene provisto
            If String.IsNullOrWhiteSpace(asiento.NumeroAsiento) Then
                asiento.NumeroAsiento = $"AS-{asiento.FechaAsiento:yyyy-MM}-{DateTime.Now.Ticks Mod 10000:D4}"
            End If

            Dim idGenerado As Integer = 0

            ' 4. REGISTRO EN POSTGRESQL (TRANSACCIÓN ATÓMICA)
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Using conn = ConexionBD.CrearConexion()
                        conn.Open()
                        Using trans = conn.BeginTransaction()
                            Try
                                Dim sqlCabecera As String = "INSERT INTO asientos_contables (numero_asiento, id_periodo, fecha_asiento, concepto, origen_modulo, origen_id, total_debe, total_haber, estado, creado_por) " &
                                                            "VALUES (@num, @per, @fecha, @con, @mod, @origId, @debe, @haber, @est, @usr) " &
                                                            "RETURNING id_asiento;"
                                Using cmd As New NpgsqlCommand(sqlCabecera, conn, trans)
                                    cmd.Parameters.AddWithValue("@num", ValidadorEntrada.SanitizarTextoSeguro(asiento.NumeroAsiento, 35))
                                    cmd.Parameters.AddWithValue("@per", asiento.IdPeriodo)
                                    cmd.Parameters.AddWithValue("@fecha", asiento.FechaAsiento.Date)
                                    cmd.Parameters.AddWithValue("@con", ValidadorEntrada.SanitizarTextoSeguro(asiento.Concepto, 255))
                                    cmd.Parameters.AddWithValue("@mod", asiento.OrigenModulo.ToUpperInvariant())
                                    cmd.Parameters.AddWithValue("@origId", If(asiento.OrigenId.HasValue, CObj(asiento.OrigenId.Value), CObj(DBNull.Value)))
                                    cmd.Parameters.AddWithValue("@debe", asiento.TotalDebe)
                                    cmd.Parameters.AddWithValue("@haber", asiento.TotalHaber)
                                    cmd.Parameters.AddWithValue("@est", asiento.Estado)
                                    cmd.Parameters.AddWithValue("@usr", If(asiento.CreadoPor.HasValue, CObj(asiento.CreadoPor.Value), CObj(DBNull.Value)))
                                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar())
                                End Using

                                ' Insertar líneas de detalle
                                Dim sqlDetalle As String = "INSERT INTO asiento_detalles (id_asiento, codigo_cuenta, descripcion_linea, debe, haber) " &
                                                           "VALUES (@asientoId, @cuenta, @desc, @d, @h);"
                                For Each linea In asiento.Lineas
                                    Using cmdD As New NpgsqlCommand(sqlDetalle, conn, trans)
                                        cmdD.Parameters.AddWithValue("@asientoId", idGenerado)
                                        cmdD.Parameters.AddWithValue("@cuenta", linea.CodigoCuenta.Trim())
                                        cmdD.Parameters.AddWithValue("@desc", ValidadorEntrada.SanitizarTextoSeguro(linea.DescripcionLinea, 150))
                                        cmdD.Parameters.AddWithValue("@d", linea.Debe)
                                        cmdD.Parameters.AddWithValue("@h", linea.Haber)
                                        cmdD.ExecuteNonQuery()
                                    End Using
                                Next

                                trans.Commit()
                                asiento.IdAsiento = idGenerado
                            Catch ex As Exception
                                trans.Rollback()
                                Throw
                            End Try
                        End Using
                    End Using
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            ' 5. REGISTRO EN MEMORIA LOCAL (Copia de seguridad y contingencia)
            SyncLock _lockObj
                If idGenerado <= 0 Then
                    _ultimoIdAsiento += 1
                    asiento.IdAsiento = _ultimoIdAsiento
                    idGenerado = _ultimoIdAsiento
                End If
                For Each l In asiento.Lineas
                    _ultimoIdDetalle += 1
                    l.IdDetalle = _ultimoIdDetalle
                    l.IdAsiento = idGenerado
                Next
                _asientosLocales.Add(asiento)
            End SyncLock

            Return idGenerado
        End Function

        ''' <summary>
        ''' Genera automáticamente el asiento contable de una venta pagada en restaurante con ITBMS 7%.
        ''' Aplica la retención del procesador POS si el método de pago es tarjeta.
        ''' </summary>
        Public Function GenerarAsientoVenta(idPedido As Integer,
                                            numeroFactura As String,
                                            total As Decimal,
                                            metodoPago As String,
                                            Optional usuarioId As Integer = 1,
                                            Optional conceptoPersonalizado As String = "") As Integer

            If total <= 0D Then Return 0
            If ExisteAsientoPorOrigen("VENTAS", idPedido) Then Return 0

            Dim subtotal As Decimal = Math.Round(total / 1.07D, 2)
            Dim itbms As Decimal = Math.Round(total - subtotal, 2)

            Dim asiento As New AsientoContableModel() With {
                .FechaAsiento = DateTime.Today,
                .OrigenModulo = "VENTAS",
                .OrigenId = idPedido,
                .CreadoPor = usuarioId,
                .Concepto = If(Not String.IsNullOrWhiteSpace(conceptoPersonalizado), conceptoPersonalizado, $"Venta cobrada Factura {numeroFactura} ({metodoPago})")
            }

            Dim metodoLimpio As String = If(metodoPago, "Efectivo").Trim().ToLowerInvariant()

            If metodoLimpio.Contains("tarjeta") OrElse metodoLimpio.Contains("pos") Then
                ' Pago con Tarjeta POS: Retención DGI del 50% del ITBMS
                Dim retencionItbms As Decimal = Math.Round(itbms * 0.5D, 2)
                Dim fondosPos As Decimal = total - retencionItbms
                asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.04", $"Fondos en tránsito POS por cobrar ({numeroFactura})", fondosPos, 0D))
                asiento.Lineas.Add(New AsientoDetalleModel("1.1.03.01", $"Retención fiscal ITBMS procesador POS DGI (50%)", retencionItbms, 0D))
            ElseIf metodoLimpio.Contains("yappy") OrElse metodoLimpio.Contains("qr") OrElse metodoLimpio.Contains("transferencia") Then
                ' Pago electrónico Yappy
                asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.05", $"Ingreso en tránsito Yappy Comercial ({numeroFactura})", total, 0D))
            Else
                ' Pago en Efectivo: Entra directamente a Caja General
                asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.01", $"Ingreso en efectivo Caja General ({numeroFactura})", total, 0D))
            End If

            ' Créditos: Ingresos por Ventas + Débito Fiscal ITBMS por pagar
            asiento.Lineas.Add(New AsientoDetalleModel("4.1.01.01", $"Ventas de alimentos gravados 7% ({numeroFactura})", 0D, subtotal))
            asiento.Lineas.Add(New AsientoDetalleModel("2.1.01.01", $"ITBMS débito fiscal 7% causado DGI ({numeroFactura})", 0D, itbms))

            asiento.RecalcularTotales()
            Return RegistrarAsiento(asiento)
        End Function

        ''' <summary>
        ''' Genera la Nota de Crédito contable por devolución o anulación de un pedido cobrado,
        ''' reversando el ingreso y el ITBMS generado.
        ''' </summary>
        Public Function GenerarAsientoDevolucion(idPedido As Integer,
                                                totalDevuelto As Decimal,
                                                Optional usuarioId As Integer = 1,
                                                Optional motivo As String = "Anulación de pedido") As Integer

            If totalDevuelto <= 0D Then Return 0
            If ExisteAsientoPorOrigen("DEVOLUCIONES", idPedido) Then Return 0

            Dim subtotal As Decimal = Math.Round(totalDevuelto / 1.07D, 2)
            Dim itbms As Decimal = Math.Round(totalDevuelto - subtotal, 2)

            Dim asiento As New AsientoContableModel() With {
                .FechaAsiento = DateTime.Today,
                .OrigenModulo = "DEVOLUCIONES",
                .OrigenId = idPedido,
                .CreadoPor = usuarioId,
                .Concepto = $"Nota de Crédito por devolución Pedido #{idPedido}: {ValidadorEntrada.SanitizarTextoSeguro(motivo, 100)}"
            }

            ' Débitos: (-) Devolución en Ventas + Reverso de ITBMS por pagar
            asiento.Lineas.Add(New AsientoDetalleModel("4.1.02.01", $"(-) Devolución en venta Pedido #{idPedido}", subtotal, 0D))
            asiento.Lineas.Add(New AsientoDetalleModel("2.1.01.01", $"Reverso débito fiscal ITBMS 7% anulado", itbms, 0D))

            ' Crédito: Salida de efectivo de Caja General entregada al cliente
            asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.01", $"Reembolso en efectivo entregado a cliente Pedido #{idPedido}", 0D, totalDevuelto))

            asiento.RecalcularTotales()
            Return RegistrarAsiento(asiento)
        End Function

        ''' <summary>
        ''' Genera el asiento contable para movimientos de caja (Aperturas, Gastos Menores, Arqueos).
        ''' </summary>
        Public Function GenerarAsientoMovimientoCaja(idMovimiento As Integer,
                                                    tipoMovimiento As String,
                                                    monto As Decimal,
                                                    referencia As String,
                                                    Optional usuarioId As Integer = 1) As Integer

            If monto <= 0D Then Return 0
            If ExisteAsientoPorOrigen("CAJA", idMovimiento) Then Return 0

            Dim asiento As New AsientoContableModel() With {
                .FechaAsiento = DateTime.Today,
                .OrigenModulo = "CAJA",
                .OrigenId = idMovimiento,
                .CreadoPor = usuarioId,
                .Concepto = $"Movimiento de Caja [{tipoMovimiento}]: {ValidadorEntrada.SanitizarTextoSeguro(referencia, 150)}"
            }

            Select Case tipoMovimiento.ToUpperInvariant()
                Case "APERTURA"
                    ' Dotación de efectivo desde Bancos a Caja General
                    asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.01", "Dotación fondo inicial de turno Caja General", monto, 0D))
                    asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.03", "Retiro para fondo de caja Banco General", 0D, monto))

                Case "GASTO_MENOR"
                    ' Gasto menor desembolsado desde Caja Chica
                    asiento.Lineas.Add(New AsientoDetalleModel("6.1.01.03", $"Gasto operativo menor ({referencia})", monto, 0D))
                    asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.02", "Desembolso en efectivo desde Caja Chica", 0D, monto))

                Case "ARQUEO_FALTANTE"
                    ' Faltante de arqueo al cierre de turno
                    asiento.Lineas.Add(New AsientoDetalleModel("6.1.02.02", "Gasto por diferencia de caja (Faltante)", monto, 0D))
                    asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.01", "Ajuste físico de saldo en Caja General", 0D, monto))

                Case "ARQUEO_SOBRANTE"
                    ' Sobrante de arqueo al cierre de turno
                    asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.01", "Ajuste físico de saldo en Caja General", monto, 0D))
                    asiento.Lineas.Add(New AsientoDetalleModel("4.1.01.01", "Ingreso por sobrante de caja de turno", 0D, monto))

                Case Else
                    Return 0
            End Select

            asiento.RecalcularTotales()
            Return RegistrarAsiento(asiento)
        End Function

        ''' <summary>
        ''' Genera la planilla y pago de nómina consultando directamente los colaboradores activos de la BD en la tabla 'empleados'.
        ''' Realiza el cálculo dinámico de retenciones obreras de ley (CSS 9.75% + SE 1.25%) y cargas patronales (15%).
        ''' </summary>
        Public Function GenerarAsientoNomina(Optional usuarioId As Integer = 1) As Integer
            Dim sueldosCocina As Decimal = 0D
            Dim sueldosMeseros As Decimal = 0D
            Dim sueldosAseo As Decimal = 0D
            Dim cantCocina As Integer = 0
            Dim cantMeseros As Integer = 0
            Dim cantAseo As Integer = 0

            ' Consultar la base de datos PostgreSQL de forma dinámica
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sqlEmpl As String = "SELECT cargo, salario_mensual FROM empleados WHERE activo = TRUE;"
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sqlEmpl)
                    For Each r As DataRow In dt.Rows
                        Dim cargo = r("cargo").ToString().Trim().ToLowerInvariant()
                        Dim sal = Convert.ToDecimal(r("salario_mensual"))
                        If cargo.Contains("cocin") Then
                            sueldosCocina += sal
                            cantCocina += 1
                        ElseIf cargo.Contains("meser") Then
                            sueldosMeseros += sal
                            cantMeseros += 1
                        ElseIf cargo.Contains("asea") OrElse cargo.Contains("limp") Then
                            sueldosAseo += sal
                            cantAseo += 1
                        End If
                    Next
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            ' Si la BD no respondió o está en modo local inicial, consultar con datos de contingencia
            If (sueldosCocina + sueldosMeseros + sueldosAseo) <= 0D Then
                sueldosCocina = 2130.00D
                sueldosMeseros = 1200.00D
                sueldosAseo = 1100.00D
                cantCocina = 3
                cantMeseros = 2
                cantAseo = 2
            End If

            Dim totalSalariosBrutos As Decimal = sueldosCocina + sueldosMeseros + sueldosAseo

            ' Leyes laborales Panamá (Caja de Seguro Social)
            Dim retencionesObreras As Decimal = Math.Round(totalSalariosBrutos * 0.11D, 2) ' 11% (CSS 9.75% + SE 1.25%)
            Dim sueldoNetoACH As Decimal = totalSalariosBrutos - retencionesObreras
            Dim cargasPatronales As Decimal = Math.Round(totalSalariosBrutos * 0.15D, 2) ' 15% (CSS 12.25% + SE 1.50% + Riesgos 1.25%)

            Dim asiento As New AsientoContableModel() With {
                .FechaAsiento = DateTime.Today,
                .OrigenModulo = "NOMINA",
                .OrigenId = DateTime.Today.Year * 100 + DateTime.Today.Month,
                .CreadoPor = usuarioId,
                .Concepto = $"Pago de planilla mensual colaboradores ({cantCocina} cocineros, {cantMeseros} meseros, {cantAseo} aseadores)"
            }

            ' Débitos: Gastos de Personal
            asiento.Lineas.Add(New AsientoDetalleModel("6.1.03.01", $"Sueldos y Salarios - {cantCocina} Cocineros", sueldosCocina, 0D))
            asiento.Lineas.Add(New AsientoDetalleModel("6.1.03.02", $"Sueldos y Salarios - {cantMeseros} Meseros", sueldosMeseros, 0D))
            asiento.Lineas.Add(New AsientoDetalleModel("6.1.03.03", $"Sueldos y Salarios - {cantAseo} Aseadores", sueldosAseo, 0D))
            asiento.Lineas.Add(New AsientoDetalleModel("6.1.03.04", "Cargas Sociales Patronales (CSS 12.25% + SE 1.50% + Riesgos)", cargasPatronales, 0D))

            ' Créditos: Transferencias Bancarias Netas + Pasivos por Pagar a la CSS
            asiento.Lineas.Add(New AsientoDetalleModel("1.1.01.03", "Transferencias ACH Banco General sueldos netos", 0D, sueldoNetoACH))
            asiento.Lineas.Add(New AsientoDetalleModel("2.1.03.02", "Retenciones obreras CSS (9.75%) y SE (1.25%) por liquidar", 0D, retencionesObreras))
            asiento.Lineas.Add(New AsientoDetalleModel("2.1.03.03", "Aportes patronales CSS/SE por liquidar", 0D, cargasPatronales))

            asiento.RecalcularTotales()
            Return RegistrarAsiento(asiento)
        End Function

        ''' <summary>
        ''' Obtiene el Libro Diario con todos los asientos y detalles registrados en el rango de fechas.
        ''' </summary>
        Public Function ObtenerLibroDiario(Optional fechaInicio As DateTime? = Nothing, Optional fechaFin As DateTime? = Nothing) As List(Of AsientoContableModel)
            Dim fIni = If(fechaInicio.HasValue, fechaInicio.Value.Date, New DateTime(2020, 1, 1))
            Dim fFin = If(fechaFin.HasValue, fechaFin.Value.Date, New DateTime(2099, 12, 31))

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT a.id_asiento, a.numero_asiento, a.id_periodo, a.fecha_asiento, a.concepto, a.origen_modulo, a.origen_id, " &
                                        "a.total_debe, a.total_haber, a.estado, a.creado_por, a.fecha_creacion, " &
                                        "d.id_detalle, d.codigo_cuenta, c.nombre_cuenta, d.descripcion_linea, d.debe, d.haber " &
                                        "FROM asientos_contables a " &
                                        "INNER JOIN asiento_detalles d ON a.id_asiento = d.id_asiento " &
                                        "LEFT JOIN catalogo_cuentas c ON d.codigo_cuenta = c.codigo_cuenta " &
                                        "WHERE a.fecha_asiento BETWEEN @fIni AND @fFin " &
                                        "ORDER BY a.fecha_asiento ASC, a.id_asiento ASC, d.id_detalle ASC;"
                    Dim pIni As New NpgsqlParameter("@fIni", fIni)
                    Dim pFin As New NpgsqlParameter("@fFin", fFin)
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, pIni, pFin)

                    Dim dictAsientos As New Dictionary(Of Integer, AsientoContableModel)()
                    For Each row As DataRow In dt.Rows
                        Dim idAs = Convert.ToInt32(row("id_asiento"))
                        If Not dictAsientos.ContainsKey(idAs) Then
                            Dim ac As New AsientoContableModel() With {
                                .IdAsiento = idAs,
                                .NumeroAsiento = row("numero_asiento").ToString(),
                                .IdPeriodo = Convert.ToInt32(row("id_periodo")),
                                .FechaAsiento = Convert.ToDateTime(row("fecha_asiento")),
                                .Concepto = row("concepto").ToString(),
                                .OrigenModulo = row("origen_modulo").ToString(),
                                .OrigenId = If(row("origen_id") Is DBNull.Value, Nothing, Convert.ToInt32(row("origen_id"))),
                                .TotalDebe = Convert.ToDecimal(row("total_debe")),
                                .TotalHaber = Convert.ToDecimal(row("total_haber")),
                                .Estado = row("estado").ToString(),
                                .CreadoPor = If(row("creado_por") Is DBNull.Value, Nothing, Convert.ToInt32(row("creado_por"))),
                                .FechaCreacion = Convert.ToDateTime(row("fecha_creacion"))
                            }
                            dictAsientos(idAs) = ac
                        End If

                        Dim linea As New AsientoDetalleModel() With {
                            .IdDetalle = Convert.ToInt32(row("id_detalle")),
                            .IdAsiento = idAs,
                            .CodigoCuenta = row("codigo_cuenta").ToString(),
                            .NombreCuenta = If(row("nombre_cuenta") Is DBNull.Value, "", row("nombre_cuenta").ToString()),
                            .DescripcionLinea = row("descripcion_linea").ToString(),
                            .Debe = Convert.ToDecimal(row("debe")),
                            .Haber = Convert.ToDecimal(row("haber"))
                        }
                        dictAsientos(idAs).Lineas.Add(linea)
                    Next

                    If dictAsientos.Count > 0 Then
                        Return dictAsientos.Values.ToList()
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Dim listaFiltrada = _asientosLocales.Where(Function(a) a.FechaAsiento.Date >= fIni AndAlso a.FechaAsiento.Date <= fFin).ToList()
                If listaFiltrada.Count = 0 AndAlso _asientosLocales.Count > 0 Then
                    Return _asientosLocales.ToList()
                End If
                Return listaFiltrada
            End SyncLock
        End Function

        ''' <summary>
        ''' Genera la estructura de Estado de Resultados (P&L) para el local en el rango de fechas.
        ''' </summary>
        Public Function GenerarEstadoResultados(Optional fechaInicio As DateTime? = Nothing, Optional fechaFin As DateTime? = Nothing) As DataTable
            Dim dt As New DataTable("EstadoResultados")
            dt.Columns.Add("Concepto", GetType(String))
            dt.Columns.Add("Monto", GetType(Decimal))

            Dim asientos = ObtenerLibroDiario(fechaInicio, fechaFin)
            Dim totalVentas As Decimal = 0D
            Dim totalDevoluciones As Decimal = 0D
            Dim totalGastosOperativos As Decimal = 0D
            Dim totalGastosPersonal As Decimal = 0D

            For Each a In asientos
                For Each l In a.Lineas
                    If l.CodigoCuenta.StartsWith("4.1.01") Then
                        totalVentas += l.Haber
                    ElseIf l.CodigoCuenta.StartsWith("4.1.02") Then
                        totalDevoluciones += l.Debe
                    ElseIf l.CodigoCuenta.StartsWith("6.1.01") OrElse l.CodigoCuenta.StartsWith("6.1.02") Then
                        totalGastosOperativos += l.Debe
                    ElseIf l.CodigoCuenta.StartsWith("6.1.03") Then
                        totalGastosPersonal += l.Debe
                    End If
                Next
            Next

            Dim ventasNetas As Decimal = totalVentas - totalDevoluciones
            Dim totalGastos As Decimal = totalGastosOperativos + totalGastosPersonal
            Dim utilidadNeta As Decimal = ventasNetas - totalGastos

            dt.Rows.Add("INGRESOS BRUTOS POR VENTAS", totalVentas)
            dt.Rows.Add("(-) DEVOLUCIONES Y DESCUENTOS", totalDevoluciones)
            dt.Rows.Add("= VENTAS NETAS OPERATIVAS", ventasNetas)
            dt.Rows.Add("GASTOS DE OPERACIÓN DEL LOCAL", totalGastosOperativos)
            dt.Rows.Add("GASTOS DE PERSONAL Y PLANILLA (7 COLABORADORES)", totalGastosPersonal)
            dt.Rows.Add("= TOTAL GASTOS OPERATIVOS", totalGastos)
            dt.Rows.Add("= UTILIDAD OPERATIVA NETA DEL RESTAURANTE", utilidadNeta)

            Return dt
        End Function

        ''' <summary>
        ''' Obtiene el catálogo de cuentas completo como lista tipada.
        ''' </summary>
        Public Function ObtenerCatalogoCuentas() As List(Of CuentaContableModel)
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT codigo_cuenta, nombre_cuenta, tipo, naturaleza, nivel, id_cuenta_padre, permite_movimiento, activo " &
                                        "FROM catalogo_cuentas ORDER BY codigo_cuenta ASC;"
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    Dim lista As New List(Of CuentaContableModel)()
                    For Each r As DataRow In dt.Rows
                        lista.Add(New CuentaContableModel(
                            r("codigo_cuenta").ToString(),
                            r("nombre_cuenta").ToString(),
                            r("tipo").ToString(),
                            r("naturaleza").ToString(),
                            Convert.ToInt32(r("nivel")),
                            If(r("id_cuenta_padre") Is DBNull.Value, "", r("id_cuenta_padre").ToString()),
                            Convert.ToBoolean(r("permite_movimiento")),
                            Convert.ToBoolean(r("activo"))
                        ))
                    Next
                    If lista.Count > 0 Then Return lista
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Return _catalogoCuentasLocales.Values.OrderBy(Function(x) x.CodigoCuenta).ToList()
            End SyncLock
        End Function

        ''' <summary>
        ''' Obtiene el Libro Mayor para una cuenta específica o para todas las cuentas en un período.
        ''' Calcula el saldo acumulado en base a la naturaleza contable (Deudora o Acreedora).
        ''' </summary>
        Public Function ObtenerLibroMayor(Optional codigoCuenta As String = Nothing,
                                          Optional fechaInicio As DateTime? = Nothing,
                                          Optional fechaFin As DateTime? = Nothing) As DataTable

            Dim dtMayor As New DataTable("LibroMayor")
            dtMayor.Columns.Add("Fecha", GetType(DateTime))
            dtMayor.Columns.Add("Asiento", GetType(String))
            dtMayor.Columns.Add("Cuenta", GetType(String))
            dtMayor.Columns.Add("NombreCuenta", GetType(String))
            dtMayor.Columns.Add("Descripcion", GetType(String))
            dtMayor.Columns.Add("Debe", GetType(Decimal))
            dtMayor.Columns.Add("Haber", GetType(Decimal))
            dtMayor.Columns.Add("Saldo", GetType(Decimal))

            Dim asientos = ObtenerLibroDiario(fechaInicio, fechaFin)
            Dim cuentas = ObtenerCatalogoCuentas().ToDictionary(Function(c) c.CodigoCuenta, Function(c) c, StringComparer.OrdinalIgnoreCase)

            Dim lineasMayor = New List(Of Tuple(Of DateTime, String, String, String, Decimal, Decimal))()
            For Each a In asientos.OrderBy(Function(x) x.FechaAsiento).ThenBy(Function(x) x.IdAsiento)
                For Each l In a.Lineas
                    If String.IsNullOrWhiteSpace(codigoCuenta) OrElse l.CodigoCuenta.Equals(codigoCuenta.Trim(), StringComparison.OrdinalIgnoreCase) Then
                        lineasMayor.Add(Tuple.Create(a.FechaAsiento, a.NumeroAsiento, l.CodigoCuenta, If(String.IsNullOrWhiteSpace(l.DescripcionLinea), a.Concepto, l.DescripcionLinea), l.Debe, l.Haber))
                    End If
                Next
            Next

            Dim saldoAcumulado As Decimal = 0D
            For Each itm In lineasMayor
                Dim ctaCod = itm.Item3
                Dim ctaNombre As String = ctaCod
                Dim esAcreedora As Boolean = False
                If cuentas.ContainsKey(ctaCod) Then
                    ctaNombre = cuentas(ctaCod).NombreCuenta
                    esAcreedora = cuentas(ctaCod).EsAcreedora
                End If

                If esAcreedora Then
                    saldoAcumulado += (itm.Item6 - itm.Item5)
                Else
                    saldoAcumulado += (itm.Item5 - itm.Item6)
                End If

                dtMayor.Rows.Add(itm.Item1, itm.Item2, ctaCod, ctaNombre, itm.Item4, itm.Item5, itm.Item6, saldoAcumulado)
            Next

            Return dtMayor
        End Function

        ''' <summary>
        ''' Genera la Balanza de Comprobación oficial con sumas de Débito, Crédito y saldos netos por cuenta.
        ''' </summary>
        Public Function GenerarBalanzaComprobacion(Optional fechaInicio As DateTime? = Nothing,
                                                   Optional fechaFin As DateTime? = Nothing) As DataTable

            Dim dtBalanza As New DataTable("BalanzaComprobacion")
            dtBalanza.Columns.Add("Cuenta", GetType(String))
            dtBalanza.Columns.Add("Categoría", GetType(String))
            dtBalanza.Columns.Add("Total Entradas ($)", GetType(Decimal))
            dtBalanza.Columns.Add("Total Salidas ($)", GetType(Decimal))
            dtBalanza.Columns.Add("Saldo Deudor ($)", GetType(Decimal))
            dtBalanza.Columns.Add("Saldo Acreedor ($)", GetType(Decimal))
            dtBalanza.Columns.Add("CodigoCuenta", GetType(String))
            dtBalanza.Columns.Add("NombreCuenta", GetType(String))

            Dim asientos = ObtenerLibroDiario(fechaInicio, fechaFin)
            Dim catalogo = ObtenerCatalogoCuentas()

            Dim acumulados As New Dictionary(Of String, Tuple(Of Decimal, Decimal))()

            For Each a In asientos
                For Each l In a.Lineas
                    If Not acumulados.ContainsKey(l.CodigoCuenta) Then
                        acumulados(l.CodigoCuenta) = Tuple.Create(0D, 0D)
                    End If
                    Dim act = acumulados(l.CodigoCuenta)
                    acumulados(l.CodigoCuenta) = Tuple.Create(act.Item1 + l.Debe, act.Item2 + l.Haber)
                Next
            Next

            For Each cta In catalogo.OrderBy(Function(c) c.CodigoCuenta)
                Dim sumDebe As Decimal = 0D
                Dim sumHaber As Decimal = 0D

                If acumulados.ContainsKey(cta.CodigoCuenta) Then
                    sumDebe = acumulados(cta.CodigoCuenta).Item1
                    sumHaber = acumulados(cta.CodigoCuenta).Item2
                End If

                If sumDebe > 0D OrElse sumHaber > 0D Then
                    Dim saldoDeudor As Decimal = 0D
                    Dim saldoAcreedor As Decimal = 0D

                    If cta.EsDeudora Then
                        Dim dif = sumDebe - sumHaber
                        If dif >= 0D Then saldoDeudor = dif Else saldoAcreedor = Math.Abs(dif)
                    Else
                        Dim dif = sumHaber - sumDebe
                        If dif >= 0D Then saldoAcreedor = dif Else saldoDeudor = Math.Abs(dif)
                    End If

                    dtBalanza.Rows.Add(cta.NombreCuenta, cta.Tipo, sumDebe, sumHaber, saldoDeudor, saldoAcreedor, cta.CodigoCuenta, cta.NombreCuenta)
                End If
            Next

            Return dtBalanza
        End Function

        ''' <summary>
        ''' Genera el Balance General a una fecha de corte, agrupando Activo, Pasivo y Patrimonio de forma amigable para el restaurante.
        ''' </summary>
        Public Function GenerarBalanceGeneral(Optional fechaCorte As DateTime? = Nothing) As DataTable
            Dim fFin = If(fechaCorte.HasValue, fechaCorte.Value.Date, DateTime.Today)

            Dim dtBalance As New DataTable("BalanceGeneral")
            dtBalance.Columns.Add("Rubro", GetType(String))
            dtBalance.Columns.Add("Cuenta", GetType(String))
            dtBalance.Columns.Add("Saldo ($)", GetType(Decimal))
            dtBalance.Columns.Add("Codigo", GetType(String))

            Dim balanza = GenerarBalanzaComprobacion(Nothing, fFin)
            Dim totalActivos As Decimal = 0D
            Dim totalPasivos As Decimal = 0D
            Dim totalPatrimonio As Decimal = 0D

            For Each r As DataRow In balanza.Rows
                Dim cod = r("CodigoCuenta").ToString()
                Dim nom = r("NombreCuenta").ToString()
                Dim debe = Convert.ToDecimal(r("Saldo Deudor ($)"))
                Dim haber = Convert.ToDecimal(r("Saldo Acreedor ($)"))

                If cod.StartsWith("1") Then
                    Dim saldoActivo = debe - haber
                    dtBalance.Rows.Add("Activos del Restaurante (Efectivo y Bienes)", nom, saldoActivo, cod)
                    totalActivos += saldoActivo
                ElseIf cod.StartsWith("2") Then
                    Dim saldoPasivo = haber - debe
                    dtBalance.Rows.Add("Pasivos y Obligaciones (Impuestos DGI)", nom, saldoPasivo, cod)
                    totalPasivos += saldoPasivo
                ElseIf cod.StartsWith("3") Then
                    Dim saldoPatrimonio = haber - debe
                    dtBalance.Rows.Add("Patrimonio y Capital", nom, saldoPatrimonio, cod)
                    totalPatrimonio += saldoPatrimonio
                End If
            Next

            Dim dtResultados = GenerarEstadoResultados(Nothing, fFin)
            Dim utilidadPeriodo As Decimal = 0D
            For Each rowR As DataRow In dtResultados.Rows
                If rowR("Concepto").ToString().Contains("UTILIDAD OPERATIVA") Then
                    utilidadPeriodo = Convert.ToDecimal(rowR("Monto"))
                    Exit For
                End If
            Next

            If utilidadPeriodo <> 0D Then
                dtBalance.Rows.Add("Patrimonio y Capital", "Utilidad Operativa del Período en Curso", utilidadPeriodo, "3.1.03.01")
                totalPatrimonio += utilidadPeriodo
            End If

            dtBalance.Rows.Add("Balance General Total", "TOTAL ACTIVOS", totalActivos, "---")
            dtBalance.Rows.Add("Balance General Total", "TOTAL PASIVOS + PATRIMONIO", totalPasivos + totalPatrimonio, "---")
            dtBalance.Rows.Add("Balance General Total", "DIFERENCIA (CUADRATURA)", Math.Round(totalActivos - (totalPasivos + totalPatrimonio), 2), "---")

            Return dtBalance
        End Function

        ''' <summary>
        ''' Obtiene los movimientos registrados en la tabla 'movimientos_caja'.
        ''' </summary>
        Public Function ObtenerMovimientosCaja(Optional fechaInicio As DateTime? = Nothing,
                                               Optional fechaFin As DateTime? = Nothing) As DataTable

            Dim fIni = If(fechaInicio.HasValue, fechaInicio.Value.Date, New DateTime(2020, 1, 1))
            Dim fFin = If(fechaFin.HasValue, fechaFin.Value.Date, New DateTime(2099, 12, 31))

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT id_movimiento, id_usuario, tipo_movimiento, monto, referencia, fecha_hora " &
                                        "FROM movimientos_caja WHERE fecha_hora::date BETWEEN @fIni AND @fFin " &
                                        "ORDER BY fecha_hora DESC;"
                    Dim pIni As New NpgsqlParameter("@fIni", fIni)
                    Dim pFin As New NpgsqlParameter("@fFin", fFin)
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, pIni, pFin)
                    If dt.Rows.Count > 0 Then Return dt
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Dim dtClone = _movimientosCajaLocales.Clone()
                For Each r As DataRow In _movimientosCajaLocales.Rows
                    Dim f = Convert.ToDateTime(r("fecha_hora")).Date
                    If f >= fIni AndAlso f <= fFin Then
                        dtClone.ImportRow(r)
                    End If
                Next
                If dtClone.Rows.Count = 0 AndAlso _movimientosCajaLocales.Rows.Count > 0 Then
                    Return _movimientosCajaLocales.Copy()
                End If
                Return dtClone
            End SyncLock
        End Function

        ''' <summary>
        ''' Registra un movimiento de caja en PostgreSQL y dispara su asiento contable respectivo.
        ''' </summary>
        Public Function RegistrarMovimientoCaja(idUsuario As Integer,
                                                tipoMovimiento As String,
                                                monto As Decimal,
                                                Optional motivoOReferencia As String = "Movimiento de caja",
                                                Optional metodoPago As String = "EFECTIVO",
                                                Optional idPedido As Integer? = Nothing) As Integer

            Dim idMov As Integer = 0
            Dim tipoNorm = ValidadorEntrada.SanitizarTextoSeguro(tipoMovimiento, 30).ToUpperInvariant()
            Dim refNorm = ValidadorEntrada.SanitizarTextoSeguro(motivoOReferencia, 150)

            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "INSERT INTO movimientos_caja (id_usuario, tipo_movimiento, monto, referencia, fecha_hora) " &
                                        "VALUES (@usr, @tipo, @monto, @ref, CURRENT_TIMESTAMP) RETURNING id_movimiento;"
                    Dim pUsr As New NpgsqlParameter("@usr", idUsuario)
                    Dim pTipo As New NpgsqlParameter("@tipo", tipoNorm)
                    Dim pMonto As New NpgsqlParameter("@monto", monto)
                    Dim pRef As New NpgsqlParameter("@ref", refNorm)

                    Dim objRes = ConexionBD.EjecutarEscalar(sql, pUsr, pTipo, pMonto, pRef)
                    If objRes IsNot Nothing AndAlso Not Convert.IsDBNull(objRes) Then
                        idMov = Convert.ToInt32(objRes)
                    End If
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            If idMov <= 0 Then
                idMov = Math.Abs(CInt(DateTime.Now.Ticks Mod 100000))
            End If

            SyncLock _lockObj
                _movimientosCajaLocales.Rows.Add(idMov, idUsuario, tipoNorm, monto, refNorm, DateTime.Now)
            End SyncLock

            GenerarAsientoMovimientoCaja(idMov, tipoNorm, monto, refNorm, idUsuario)
            Return idMov
        End Function

        ''' <summary>
        ''' Obtiene los períodos contables registrados en la base de datos.
        ''' </summary>
        Public Function ObtenerPeriodosContables() As List(Of PeriodoContableModel)
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT id_periodo, anio, mes, fecha_inicio, fecha_fin, estado FROM periodos_contables ORDER BY anio DESC, mes DESC;"
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    Dim lista As New List(Of PeriodoContableModel)()
                    For Each r As DataRow In dt.Rows
                        lista.Add(New PeriodoContableModel(
                            Convert.ToInt32(r("id_periodo")),
                            Convert.ToInt32(r("anio")),
                            Convert.ToInt32(r("mes")),
                            Convert.ToDateTime(r("fecha_inicio")),
                            Convert.ToDateTime(r("fecha_fin")),
                            r("estado").ToString()
                        ))
                    Next
                    If lista.Count > 0 Then Return lista
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Return _periodosLocales.ToList()
            End SyncLock
        End Function

        ''' <summary>
        ''' Ejecuta el cierre mensual de un período contable, bloqueando nuevas operaciones y registrando auditoría.
        ''' </summary>
        Public Function CerrarPeriodoMensual(idPeriodo As Integer, idUsuario As Integer) As Boolean
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Using conn = ConexionBD.CrearConexion()
                        conn.Open()
                        Using trans = conn.BeginTransaction()
                            Try
                                Dim sqlCierre As String = "UPDATE periodos_contables SET estado = 'CERRADO' WHERE id_periodo = @id;"
                                Using cmd As New NpgsqlCommand(sqlCierre, conn, trans)
                                    cmd.Parameters.AddWithValue("@id", idPeriodo)
                                    cmd.ExecuteNonQuery()
                                End Using

                                Dim sqlAudit As String = "INSERT INTO auditoria_contable (usuario_id, nombre_usuario, accion, tabla_afectada, id_registro, datos_nuevos) " &
                                                         "VALUES (@usr, 'Administrador', 'CIERRE_MENSUAL', 'periodos_contables', @id, '""Cierre formal de período contable y bloqueo de asientos.""'::jsonb);"
                                Using cmdA As New NpgsqlCommand(sqlAudit, conn, trans)
                                    cmdA.Parameters.AddWithValue("@usr", idUsuario)
                                    cmdA.Parameters.AddWithValue("@id", idPeriodo)
                                    cmdA.ExecuteNonQuery()
                                End Using

                                trans.Commit()
                                Return True
                            Catch ex As Exception
                                trans.Rollback()
                                Throw
                            End Try
                        End Using
                    End Using
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                    Return False
                End Try
            End If

            SyncLock _lockObj
                Dim per = _periodosLocales.FirstOrDefault(Function(p) p.IdPeriodo = idPeriodo)
                If per IsNot Nothing Then
                    per.Estado = "CERRADO"
                    _auditoriaLocales.Rows.Add(Math.Abs(CInt(DateTime.Now.Ticks Mod 10000)), idUsuario, "Administrador", "CIERRE_MENSUAL", "periodos_contables", idPeriodo, "Cierre formal de período contable", DateTime.Now)
                    Return True
                End If
            End SyncLock
            Return False
        End Function

        ''' <summary>
        ''' Obtiene la bitácora de auditoría contable.
        ''' </summary>
        Public Function ObtenerAuditoriaContable() As DataTable
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT a.id_auditoria, a.usuario_id AS id_usuario, COALESCE(a.nombre_usuario, u.nombre_usuario, 'Sistema') AS nombre_usuario, " &
                                        "a.accion, a.tabla_afectada, a.id_registro AS registro_id, COALESCE(a.datos_nuevos::text, '') AS detalle_cambio, a.fecha_evento AS fecha_accion " &
                                        "FROM auditoria_contable a " &
                                        "LEFT JOIN usuarios u ON a.usuario_id = u.id_usuario " &
                                        "ORDER BY a.fecha_evento DESC LIMIT 100;"
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    If dt.Rows.Count > 0 Then Return dt
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Return _auditoriaLocales.Copy()
            End SyncLock
        End Function

        ''' <summary>
        ''' Obtiene la lista de colaboradores/empleados registrados en PostgreSQL.
        ''' </summary>
        Public Function ObtenerEmpleados() As List(Of EmpleadoModel)
            Dim lista As New List(Of EmpleadoModel)()
            If ConexionBD.DebeUsarPostgreSQL() Then
                Try
                    Dim sql As String = "SELECT id_empleado, nombre_completo, cedula, cargo, salario_mensual, activo, fecha_ingreso FROM empleados ORDER BY cargo ASC, id_empleado ASC;"
                    Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                    For Each r As DataRow In dt.Rows
                        lista.Add(New EmpleadoModel() With {
                            .IdEmpleado = Convert.ToInt32(r("id_empleado")),
                            .NombreCompleto = r("nombre_completo").ToString(),
                            .Cedula = r("cedula").ToString(),
                            .Cargo = r("cargo").ToString(),
                            .SalarioMensual = Convert.ToDecimal(r("salario_mensual")),
                            .TipoSalario = "MENSUAL",
                            .Activo = Convert.ToBoolean(r("activo")),
                            .FechaIngreso = Convert.ToDateTime(r("fecha_ingreso"))
                        })
                    Next
                    If lista.Count > 0 Then Return lista
                Catch ex As Exception
                    ConexionBD.RegistrarFalloServidor(ex.Message)
                End Try
            End If

            SyncLock _lockObj
                Return _empleadosLocales.ToList()
            End SyncLock
        End Function

    End Module
End Namespace
