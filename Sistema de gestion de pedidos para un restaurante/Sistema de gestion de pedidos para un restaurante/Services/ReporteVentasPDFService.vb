Imports System
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Text

Namespace Services
    ''' <summary>
    ''' Servicio autónomo para la exportación del Reporte Consolidado de Ventas y Arqueo de Caja
    ''' en formato estándar PDF 1.4 sin dependencias de librerías externas de terceros (RF-013).
    ''' </summary>
    Public Class ReporteVentasPDFService

        ''' <summary>
        ''' Genera el archivo PDF del reporte de ventas con desglose de transacciones y arqueo de caja.
        ''' </summary>
        Public Shared Sub GenerarReporteVentasPDF(rutaArchivo As String,
                                                  dtTransacciones As DataTable,
                                                  fechaInicio As DateTime,
                                                  fechaFin As DateTime,
                                                  totalVentas As Decimal,
                                                  totalEfectivo As Decimal,
                                                  totalTarjeta As Decimal,
                                                  ticketPromedio As Decimal,
                                                  fondoInicial As Decimal)
            If String.IsNullOrWhiteSpace(rutaArchivo) Then
                Throw New ArgumentException("La ruta del archivo no puede ser nula ni vacía.", NameOf(rutaArchivo))
            End If

            Dim sb As New StringBuilder()

            ' 1. Encabezado Terracota Oficial (0.776 0.420 0.282)
            sb.AppendLine("q")
            sb.AppendLine("0.776 0.420 0.282 rg")
            sb.AppendLine("0 742 595 100 re")
            sb.AppendLine("f")
            sb.AppendLine("Q")

            ' Título y Marca en Encabezado
            sb.AppendLine("BT")
            sb.AppendLine("/F2 19 Tf")
            sb.AppendLine("1 1 1 rg")
            sb.AppendLine("40 802 Td")
            sb.AppendLine("(RESTAURANTE EL BUEN SAZON) Tj")
            sb.AppendLine("ET")

            sb.AppendLine("BT")
            sb.AppendLine("/F1 11 Tf")
            sb.AppendLine("1 1 1 rg")
            sb.AppendLine("40 784 Td")
            sb.AppendLine("(REPORTE EJECUTIVO DE VENTAS Y ARQUEO DE CAJA - RF-013) Tj")
            sb.AppendLine("ET")

            sb.AppendLine("BT")
            sb.AppendLine("/F1 8.5 Tf")
            sb.AppendLine("0.96 0.96 0.96 rg")
            sb.AppendLine("40 762 Td")
            Dim periodoStr As String = SanitizarTextoPDF($"Periodo Filtrado: {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}  |  Generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
            sb.AppendLine($"({periodoStr}) Tj")
            sb.AppendLine("ET")

            ' 2. Sección de Tarjetas KPI Resumen (Y = 660 a 725)
            DibujarTarjetaKPI(sb, 40, 665, 120, 60, "TOTAL VENTAS", $"${totalVentas.ToString("N2", CultureInfo.InvariantCulture)}", "0.776 0.420 0.282")
            DibujarTarjetaKPI(sb, 172, 665, 120, 60, "VENTAS EFECTIVO", $"${totalEfectivo.ToString("N2", CultureInfo.InvariantCulture)}", "0.22 0.55 0.32")
            DibujarTarjetaKPI(sb, 304, 665, 120, 60, "TARJETAS / QR", $"${totalTarjeta.ToString("N2", CultureInfo.InvariantCulture)}", "0.18 0.45 0.70")
            DibujarTarjetaKPI(sb, 436, 665, 120, 60, "TICKET PROMEDIO", $"${ticketPromedio.ToString("N2", CultureInfo.InvariantCulture)}", "0.45 0.27 0.19")

            ' 3. Título de la Tabla de Transacciones
            sb.AppendLine("BT")
            sb.AppendLine("/F2 11 Tf")
            sb.AppendLine("0.20 0.20 0.20 rg")
            sb.AppendLine("40 640 Td")
            sb.AppendLine("(DETALLE DE TRANSACCIONES Y COMANDAS REGISTRADAS) Tj")
            sb.AppendLine("ET")

            ' 4. Cabecera de la Tabla (Y = 618)
            sb.AppendLine("q")
            sb.AppendLine("0.459 0.275 0.196 rg")
            sb.AppendLine("40 618 515 18 re")
            sb.AppendLine("f")
            sb.AppendLine("Q")

            sb.AppendLine("BT")
            sb.AppendLine("/F2 8.5 Tf")
            sb.AppendLine("1 1 1 rg")
            sb.AppendLine("46 623 Td")
            sb.AppendLine("(NRO. ORDEN) Tj")
            sb.AppendLine("90 0 Td")
            sb.AppendLine("(CLIENTE) Tj")
            sb.AppendLine("140 0 Td")
            sb.AppendLine("(METODO PAGO) Tj")
            sb.AppendLine("110 0 Td")
            sb.AppendLine("(ESTADO) Tj")
            sb.AppendLine("75 0 Td")
            sb.AppendLine("(TOTAL) Tj")
            sb.AppendLine("60 0 Td")
            sb.AppendLine("(HORA) Tj")
            sb.AppendLine("ET")

            ' 5. Filas de Transacciones
            Dim currentY As Single = 600.0F
            Dim filaIndex As Integer = 0

            If dtTransacciones IsNot Nothing AndAlso dtTransacciones.Rows.Count > 0 Then
                For Each row As DataRow In dtTransacciones.Rows
                    If currentY < 230.0F Then Exit For ' Limitar a 1 página elegante

                    ' Zebra striping
                    If filaIndex Mod 2 = 1 Then
                        sb.AppendLine("q")
                        sb.AppendLine("0.97 0.96 0.95 rg")
                        sb.AppendLine($"40 {currentY - 3:F1} 515 16 re")
                        sb.AppendLine("f")
                        sb.AppendLine("Q")
                    End If

                    ' Línea inferior sutil
                    sb.AppendLine("q")
                    sb.AppendLine("0.88 0.86 0.84 RG")
                    sb.AppendLine("0.5 w")
                    sb.AppendLine($"40 {currentY - 3:F1} 515 0 re")
                    sb.AppendLine("S")
                    sb.AppendLine("Q")

                    Dim nroOrden As String = SanitizarTextoPDF(If(row("NroOrden") IsNot DBNull.Value, row("NroOrden").ToString(), "ORD-0000"))
                    Dim cliente As String = SanitizarTextoPDF(If(row("Cliente") IsNot DBNull.Value, row("Cliente").ToString(), "Cliente"))
                    If cliente.Length > 20 Then cliente = cliente.Substring(0, 18) & ".."

                    Dim metodo As String = SanitizarTextoPDF(If(row("MetodoPago") IsNot DBNull.Value, row("MetodoPago").ToString(), "Efectivo"))
                    Dim estado As String = SanitizarTextoPDF(If(row("Estado") IsNot DBNull.Value, row("Estado").ToString(), "PAGADO"))

                    Dim montoVal As Decimal = 0D
                    If row("MontoTotal") IsNot DBNull.Value Then Decimal.TryParse(row("MontoTotal").ToString(), montoVal)
                    Dim montoStr As String = $"${montoVal.ToString("N2", CultureInfo.InvariantCulture)}"

                    Dim horaStr As String = SanitizarTextoPDF(If(row("Hora") IsNot DBNull.Value, row("Hora").ToString(), "--:--"))
                    If horaStr.Length > 8 Then horaStr = horaStr.Substring(0, 8)

                    sb.AppendLine("BT")
                    sb.AppendLine("/F1 8 Tf")
                    sb.AppendLine("0.18 0.18 0.18 rg")
                    sb.AppendLine($"46 {currentY:F1} Td")
                    sb.AppendLine($"({nroOrden}) Tj")
                    sb.AppendLine($"90 0 Td")
                    sb.AppendLine($"({cliente}) Tj")
                    sb.AppendLine($"140 0 Td")
                    sb.AppendLine($"({metodo}) Tj")
                    sb.AppendLine($"110 0 Td")
                    sb.AppendLine($"({estado}) Tj")
                    sb.AppendLine($"75 0 Td")
                    sb.AppendLine($"({montoStr}) Tj")
                    sb.AppendLine($"60 0 Td")
                    sb.AppendLine($"({horaStr}) Tj")
                    sb.AppendLine("ET")

                    currentY -= 17.0F
                    filaIndex += 1
                Next
            Else
                sb.AppendLine("BT")
                sb.AppendLine("/F1 9 Tf")
                sb.AppendLine("0.5 0.5 0.5 rg")
                sb.AppendLine($"200 {currentY:F1} Td")
                sb.AppendLine("(No se registraron transacciones en este rango de fechas) Tj")
                sb.AppendLine("ET")
                currentY -= 20.0F
            End If

            ' 6. Caja de Arqueo y Conciliación Fiscal (Y = 135 a 210)
            Dim totalEsperado As Decimal = fondoInicial + totalEfectivo
            sb.AppendLine("q")
            sb.AppendLine("0.96 0.95 0.93 rg")
            sb.AppendLine("40 135 515 75 re")
            sb.AppendLine("f")
            sb.AppendLine("0.776 0.420 0.282 RG")
            sb.AppendLine("1 w")
            sb.AppendLine("40 135 515 75 re")
            sb.AppendLine("S")
            sb.AppendLine("Q")

            sb.AppendLine("BT")
            sb.AppendLine("/F2 10 Tf")
            sb.AppendLine("0.776 0.420 0.282 rg")
            sb.AppendLine("55 192 Td")
            sb.AppendLine("(CONCILIACION FISCAL Y ARQUEO DE CAJA DEL TURNO) Tj")
            sb.AppendLine("ET")

            sb.AppendLine("BT")
            sb.AppendLine("/F1 8.5 Tf")
            sb.AppendLine("0.25 0.25 0.25 rg")
            sb.AppendLine("55 174 Td")
            sb.AppendLine($"({SanitizarTextoPDF($"Fondo Inicial de Apertura en Caja:  ${fondoInicial.ToString("N2", CultureInfo.InvariantCulture)}")}) Tj")
            sb.AppendLine("0 -13 Td")
            sb.AppendLine($"({SanitizarTextoPDF($"Recaudacion en Efectivo:             +${totalEfectivo.ToString("N2", CultureInfo.InvariantCulture)}")}) Tj")
            sb.AppendLine("0 -13 Td")
            sb.AppendLine($"({SanitizarTextoPDF($"Recaudacion en Tarjetas / QR:         +${totalTarjeta.ToString("N2", CultureInfo.InvariantCulture)}")}) Tj")
            sb.AppendLine("ET")

            sb.AppendLine("BT")
            sb.AppendLine("/F2 10 Tf")
            sb.AppendLine("0.22 0.55 0.32 rg")
            sb.AppendLine("330 160 Td")
            sb.AppendLine($"({SanitizarTextoPDF($"TOTAL ESPERADO EN CAJA: ${totalEsperado.ToString("N2", CultureInfo.InvariantCulture)}")}) Tj")
            sb.AppendLine("ET")

            ' 7. Pie de Página
            sb.AppendLine("q")
            sb.AppendLine("0.776 0.420 0.282 RG")
            sb.AppendLine("0.8 w")
            sb.AppendLine("40 70 515 0 re")
            sb.AppendLine("S")
            sb.AppendLine("Q")

            sb.AppendLine("BT")
            sb.AppendLine("/F1 7.5 Tf")
            sb.AppendLine("0.5 0.5 0.5 rg")
            sb.AppendLine("120 54 Td")
            sb.AppendLine("(Documento confidencial generado por el Modulo Administrativo de El Buen Sazon.) Tj")
            sb.AppendLine("0 -11 Td")
            sb.AppendLine("(Valido para auditoria interna de finanzas y control contable del restaurante.) Tj")
            sb.AppendLine("ET")

            ' 8. Compilar a archivo PDF binario estándar
            Dim contentBytes As Byte() = Encoding.ASCII.GetBytes(sb.ToString())
            ConstruirArchivoPDF(rutaArchivo, contentBytes)
        End Sub

        Private Shared Sub DibujarTarjetaKPI(sb As StringBuilder, x As Single, y As Single, w As Single, h As Single,
                                             titulo As String, valor As String, colorRgb As String)
            ' Fondo tarjeta
            sb.AppendLine("q")
            sb.AppendLine("1 1 1 rg")
            sb.AppendLine($"{x:F1} {y:F1} {w:F1} {h:F1} re")
            sb.AppendLine("f")
            sb.AppendLine("0.88 0.86 0.84 RG")
            sb.AppendLine("0.8 w")
            sb.AppendLine($"{x:F1} {y:F1} {w:F1} {h:F1} re")
            sb.AppendLine("S")
            sb.AppendLine("Q")

            ' Barra superior de acento
            sb.AppendLine("q")
            sb.AppendLine($"{colorRgb} rg")
            sb.AppendLine($"{x:F1} {y + h - 4:F1} {w:F1} 4 re")
            sb.AppendLine("f")
            sb.AppendLine("Q")

            ' Título KPI
            sb.AppendLine("BT")
            sb.AppendLine("/F2 7.5 Tf")
            sb.AppendLine("0.45 0.45 0.45 rg")
            sb.AppendLine($"{x + 8:F1} {y + h - 17:F1} Td")
            sb.AppendLine($"({SanitizarTextoPDF(titulo)}) Tj")
            sb.AppendLine("ET")

            ' Valor KPI
            sb.AppendLine("BT")
            sb.AppendLine("/F2 14 Tf")
            sb.AppendLine($"{colorRgb} rg")
            sb.AppendLine($"{x + 8:F1} {y + 12:F1} Td")
            sb.AppendLine($"({SanitizarTextoPDF(valor)}) Tj")
            sb.AppendLine("ET")
        End Sub

        Private Shared Sub ConstruirArchivoPDF(rutaArchivo As String, contentBytes As Byte())
            Dim dir = Path.GetDirectoryName(rutaArchivo)
            If Not String.IsNullOrEmpty(dir) AndAlso Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            Using fs As New FileStream(rutaArchivo, FileMode.Create, FileAccess.Write)
                Using writer As New StreamWriter(fs, Encoding.ASCII)
                    Dim posicionesObjetos As New List(Of Long)()

                    ' Header PDF 1.4
                    writer.Write("%PDF-1.4" & vbCrLf)
                    writer.Write("%âãÏÓ" & vbCrLf)
                    writer.Flush()

                    ' Obj 1: Catalog
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("1 0 obj" & vbCrLf & "<<" & vbCrLf & "/Type /Catalog" & vbCrLf & "/Pages 2 0 R" & vbCrLf & ">>" & vbCrLf & "endobj" & vbCrLf)
                    writer.Flush()

                    ' Obj 2: Pages
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("2 0 obj" & vbCrLf & "<<" & vbCrLf & "/Type /Pages" & vbCrLf & "/Kids [3 0 R]" & vbCrLf & "/Count 1" & vbCrLf & ">>" & vbCrLf & "endobj" & vbCrLf)
                    writer.Flush()

                    ' Obj 3: Page (A4)
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("3 0 obj" & vbCrLf & "<<" & vbCrLf & "/Type /Page" & vbCrLf & "/Parent 2 0 R" & vbCrLf & "/MediaBox [0 0 595 842]" & vbCrLf & "/Contents 4 0 R" & vbCrLf)
                    writer.Write("/Resources <<" & vbCrLf & "  /Font <<" & vbCrLf & "    /F1 5 0 R" & vbCrLf & "    /F2 6 0 R" & vbCrLf & "  >>" & vbCrLf & ">>" & vbCrLf & ">>" & vbCrLf & "endobj" & vbCrLf)
                    writer.Flush()

                    ' Obj 4: Content Stream
                    posicionesObjetos.Add(fs.Position)
                    writer.Write($"4 0 obj" & vbCrLf & "<<" & vbCrLf & $"/Length {contentBytes.Length}" & vbCrLf & ">>" & vbCrLf & "stream" & vbCrLf)
                    writer.Flush()
                    fs.Write(contentBytes, 0, contentBytes.Length)
                    fs.Flush()
                    writer.Write(vbCrLf & "endstream" & vbCrLf & "endobj" & vbCrLf)
                    writer.Flush()

                    ' Obj 5: Helvetica Regular
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("5 0 obj" & vbCrLf & "<<" & vbCrLf & "/Type /Font" & vbCrLf & "/Subtype /Type1" & vbCrLf & "/BaseFont /Helvetica" & vbCrLf & ">>" & vbCrLf & "endobj" & vbCrLf)
                    writer.Flush()

                    ' Obj 6: Helvetica Bold
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("6 0 obj" & vbCrLf & "<<" & vbCrLf & "/Type /Font" & vbCrLf & "/Subtype /Type1" & vbCrLf & "/BaseFont /Helvetica-Bold" & vbCrLf & ">>" & vbCrLf & "endobj" & vbCrLf)
                    writer.Flush()

                    ' XREF
                    Dim inicioXref As Long = fs.Position
                    writer.Write("xref" & vbCrLf)
                    writer.Write($"0 {posicionesObjetos.Count + 1}" & vbCrLf)
                    writer.Write("0000000000 65535 f " & vbCrLf)
                    For Each pos In posicionesObjetos
                        writer.Write(pos.ToString("D10") & " 00000 n " & vbCrLf)
                    Next

                    ' Trailer
                    writer.Write("trailer" & vbCrLf & "<<" & vbCrLf & $"/Size {posicionesObjetos.Count + 1}" & vbCrLf & "/Root 1 0 R" & vbCrLf & ">>" & vbCrLf & "startxref" & vbCrLf & inicioXref.ToString() & vbCrLf & "%%EOF" & vbCrLf)
                    writer.Flush()
                End Using
            End Using
        End Sub

        Private Shared Function SanitizarTextoPDF(texto As String) As String
            If String.IsNullOrEmpty(texto) Then Return String.Empty
            Dim sb As New StringBuilder(texto)
            sb.Replace("\", "\\").Replace("(", "\(").Replace(")", "\)")
            sb.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            sb.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
            sb.Replace("ñ", "n").Replace("Ñ", "N")
            Return sb.ToString()
        End Function

    End Class
End Namespace
