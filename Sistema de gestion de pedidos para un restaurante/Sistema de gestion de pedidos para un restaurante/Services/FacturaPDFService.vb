Imports System
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Text

Namespace Services
    ''' <summary>
    ''' Servicio autónomo para la generación de Facturas Digitales en formato PDF 1.4 estándar (RF-007, RN-010, RNF-004).
    ''' Genera un documento PDF binario válido sin requerir dependencias externas de terceros,
    ''' garantizando portabilidad absoluta en cualquier entorno .NET Windows.
    ''' </summary>
    Public Class FacturaPDFService

        ''' <summary>
        ''' Genera el archivo PDF de la factura fiscal en la ruta especificada.
        ''' </summary>
        Public Shared Sub GenerarFacturaPDF(rutaArchivo As String, pedido As DataRow)
            If pedido Is Nothing Then
                Throw New ArgumentNullException(NameOf(pedido), "El registro de pedido no puede ser nulo.")
            End If

            ' 1. Extraer datos del pedido
            Dim idPedido As Integer = Convert.ToInt32(pedido("ID"))
            Dim numFactura As String = If(pedido("NumeroFactura") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("NumeroFactura").ToString()), pedido("NumeroFactura").ToString(), $"FAC-2026-{1000 + idPedido}")
            Dim clienteNombre As String = If(pedido("RazonSocial") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("RazonSocial").ToString()), pedido("RazonSocial").ToString(), pedido("Cliente").ToString())
            Dim rucCedula As String = If(pedido("RUC_Cedula") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("RUC_Cedula").ToString()), pedido("RUC_Cedula").ToString(), "Consumidor Final")
            Dim telefono As String = If(pedido("TelefonoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("TelefonoCliente").ToString()), pedido("TelefonoCliente").ToString(), "N/A")
            Dim correo As String = If(pedido("CorreoCliente") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("CorreoCliente").ToString()), pedido("CorreoCliente").ToString(), "cliente@restaurante.com")
            Dim direccion As String = If(pedido("DireccionFiscal") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("DireccionFiscal").ToString()), pedido("DireccionFiscal").ToString(), "Ciudad de Panamá")
            Dim mesaServicio As String = $"{pedido("TipoServicio")} • Mesa: {pedido("Mesa")}"
            Dim metodoPago As String = If(pedido("MetodoPago") IsNot DBNull.Value AndAlso Not String.IsNullOrWhiteSpace(pedido("MetodoPago").ToString()), pedido("MetodoPago").ToString(), "Efectivo")
            Dim fechaEmision As String = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

            Dim plato As String = pedido("PlatoPrincipal").ToString()
            Dim acompanamientos As String = pedido("Acompanamientos").ToString()
            Dim total As Decimal = If(pedido("Total") IsNot DBNull.Value, Convert.ToDecimal(pedido("Total")), 15.0D)
            Dim subtotal As Decimal = If(pedido("Subtotal") IsNot DBNull.Value, Convert.ToDecimal(pedido("Subtotal")), Math.Round(total / 1.07D, 2))
            Dim impuesto As Decimal = If(pedido("Impuesto") IsNot DBNull.Value, Convert.ToDecimal(pedido("Impuesto")), Math.Round(total - subtotal, 2))
            Dim montoRecibido As Decimal = If(pedido("MontoRecibido") IsNot DBNull.Value AndAlso Convert.ToDecimal(pedido("MontoRecibido")) > 0D, Convert.ToDecimal(pedido("MontoRecibido")), total)
            Dim cambio As Decimal = If(pedido("Cambio") IsNot DBNull.Value, Convert.ToDecimal(pedido("Cambio")), (montoRecibido - total))

            ' 2. Construir flujo de comandos PostScript / PDF para tamaño A4 (595 x 842 pt)
            Dim streamContent As New StringBuilder()

            ' Colores de la paleta oficial (RGB normalizado 0.0 - 1.0)
            ' Terracota: 198/255 = 0.776, 107/255 = 0.420, 72/255 = 0.282
            ' Secundario Marron: 117/255 = 0.459, 70/255 = 0.275, 50/255 = 0.196
            ' Fondo suave: 244/255 = 0.957, 243/255 = 0.953, 237/255 = 0.929

            ' Encabezado Terracota
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.776 0.420 0.282 rg")
            streamContent.AppendLine("0 740 595 102 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("Q")

            ' Texto del Encabezado
            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 20 Tf")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("40 795 Td")
            streamContent.AppendLine("(RESTAURANTE EL BUEN SAZON) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 10 Tf")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("40 778 Td")
            streamContent.AppendLine("(Sabor Tradicional y Excelencia Gastronomica) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 9 Tf")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("40 762 Td")
            streamContent.AppendLine("(RUC: 155698421-2-2024 DV 89  |  Tel: +507 223-9000  |  Ciudad de Panama) Tj")
            streamContent.AppendLine("ET")

            ' Recuadro Factura en Cabecera
            streamContent.AppendLine("q")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("390 755 165 65 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("0.459 0.275 0.196 RG")
            streamContent.AppendLine("1 w")
            streamContent.AppendLine("390 755 165 65 re")
            streamContent.AppendLine("S")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 11 Tf")
            streamContent.AppendLine("0.776 0.420 0.282 rg")
            streamContent.AppendLine("400 802 Td")
            streamContent.AppendLine("(RECIBO / FACTURA) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 12 Tf")
            streamContent.AppendLine("0.16 0.17 0.15 rg")
            streamContent.AppendLine("400 785 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF(numFactura)}) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 8 Tf")
            streamContent.AppendLine("0.47 0.47 0.45 rg")
            streamContent.AppendLine("400 768 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF(fechaEmision)}) Tj")
            streamContent.AppendLine("ET")

            ' Cuadro 1: Datos de la Transacción y Cliente
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.96 0.95 0.93 rg")
            streamContent.AppendLine("40 615 515 105 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("0.88 0.86 0.83 RG")
            streamContent.AppendLine("1 w")
            streamContent.AppendLine("40 615 515 105 re")
            streamContent.AppendLine("S")
            streamContent.AppendLine("Q")

            ' Título Datos Cliente
            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 10 Tf")
            streamContent.AppendLine("0.459 0.275 0.196 rg")
            streamContent.AppendLine("55 700 Td")
            streamContent.AppendLine("(DATOS DEL CLIENTE) Tj")
            streamContent.AppendLine("ET")

            ' Contenido Datos Cliente (Columna Izquierda)
            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 9 Tf")
            streamContent.AppendLine("0.16 0.17 0.15 rg")
            streamContent.AppendLine("55 680 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Cliente / Razon: " & clienteNombre)}) Tj")
            streamContent.AppendLine("0 -15 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("RUC / Cedula:    " & rucCedula)}) Tj")
            streamContent.AppendLine("0 -15 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Telefono:        " & telefono)}) Tj")
            streamContent.AppendLine("0 -15 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Direccion:       " & direccion)}) Tj")
            streamContent.AppendLine("ET")

            ' Contenido Datos Transacción (Columna Derecha)
            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 9 Tf")
            streamContent.AppendLine("0.16 0.17 0.15 rg")
            streamContent.AppendLine("330 680 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("No. Pedido:      #" & idPedido.ToString())}) Tj")
            streamContent.AppendLine("0 -15 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Servicio:        " & mesaServicio)}) Tj")
            streamContent.AppendLine("0 -15 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Metodo de Pago:  " & metodoPago)}) Tj")
            streamContent.AppendLine("0 -15 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Correo:          " & correo)}) Tj")
            streamContent.AppendLine("ET")

            ' Encabezado de la Tabla de Productos
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.459 0.275 0.196 rg")
            streamContent.AppendLine("40 575 515 25 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 9 Tf")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("50 584 Td")
            streamContent.AppendLine("(CANT) Tj")
            streamContent.AppendLine("40 0 Td")
            streamContent.AppendLine("(DESCRIPCION DEL PLATO / DETALLE) Tj")
            streamContent.AppendLine("330 0 Td")
            streamContent.AppendLine("(PRECIO) Tj")
            streamContent.AppendLine("50 0 Td")
            streamContent.AppendLine("(TOTAL) Tj")
            streamContent.AppendLine("ET")

            ' Fila del Producto
            streamContent.AppendLine("q")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("40 515 515 60 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("0.88 0.86 0.83 RG")
            streamContent.AppendLine("1 w")
            streamContent.AppendLine("40 515 515 60 re")
            streamContent.AppendLine("S")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 9 Tf")
            streamContent.AppendLine("0.16 0.17 0.15 rg")
            streamContent.AppendLine("55 555 Td")
            streamContent.AppendLine("(1) Tj")
            streamContent.AppendLine("35 0 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF(plato)}) Tj")
            streamContent.AppendLine("330 0 Td")
            streamContent.AppendLine($"(${total.ToString("N2", CultureInfo.InvariantCulture)}) Tj")
            streamContent.AppendLine("50 0 Td")
            streamContent.AppendLine($"(${total.ToString("N2", CultureInfo.InvariantCulture)}) Tj")
            streamContent.AppendLine("ET")

            ' Acompañamientos en subtexto
            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 8 Tf")
            streamContent.AppendLine("0.47 0.47 0.45 rg")
            streamContent.AppendLine("90 538 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Acompanamiento: " & acompanamientos)}) Tj")
            streamContent.AppendLine("0 -12 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Modalidad: " & pedido("TipoServicio").ToString() & " - Cocina Confirmada")}) Tj")
            streamContent.AppendLine("ET")

            ' Cuadro de Totales e Impuestos (Alineado a la derecha)
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.97 0.97 0.96 rg")
            streamContent.AppendLine("315 375 240 120 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("0.88 0.86 0.83 RG")
            streamContent.AppendLine("1 w")
            streamContent.AppendLine("315 375 240 120 re")
            streamContent.AppendLine("S")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 9 Tf")
            streamContent.AppendLine("0.25 0.25 0.25 rg")
            streamContent.AppendLine("330 475 Td")
            streamContent.AppendLine("(Subtotal:) Tj")
            streamContent.AppendLine("140 0 Td")
            streamContent.AppendLine($"(${subtotal.ToString("N2", CultureInfo.InvariantCulture)}) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 9 Tf")
            streamContent.AppendLine("0.25 0.25 0.25 rg")
            streamContent.AppendLine("330 455 Td")
            streamContent.AppendLine("(Impuesto ITBMS (7%):) Tj")
            streamContent.AppendLine("140 0 Td")
            streamContent.AppendLine($"(${impuesto.ToString("N2", CultureInfo.InvariantCulture)}) Tj")
            streamContent.AppendLine("ET")

            ' Barra destacada de TOTAL
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.776 0.420 0.282 rg")
            streamContent.AppendLine("320 415 230 28 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 11 Tf")
            streamContent.AppendLine("1 1 1 rg")
            streamContent.AppendLine("330 424 Td")
            streamContent.AppendLine("(TOTAL:) Tj")
            streamContent.AppendLine("135 0 Td")
            streamContent.AppendLine($"(${total.ToString("N2", CultureInfo.InvariantCulture)}) Tj")
            streamContent.AppendLine("ET")

            ' Desglose de Pago Recibido y Vuelto
            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 8.5 Tf")
            streamContent.AppendLine("0.35 0.35 0.35 rg")
            streamContent.AppendLine("330 398 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Monto Recibido: $" & montoRecibido.ToString("N2", CultureInfo.InvariantCulture))}) Tj")
            streamContent.AppendLine("0 -13 Td")
            streamContent.AppendLine($"({SanitizarTextoPDF("Cambio / Vuelto:  $" & cambio.ToString("N2", CultureInfo.InvariantCulture))}) Tj")
            streamContent.AppendLine("ET")

            ' Cuadro de Certificación Fiscal y Seguridad
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.93 0.95 0.91 rg")
            streamContent.AppendLine("40 375 260 120 re")
            streamContent.AppendLine("f")
            streamContent.AppendLine("0.35 0.42 0.29 RG")
            streamContent.AppendLine("1 w")
            streamContent.AppendLine("40 375 260 120 re")
            streamContent.AppendLine("S")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 9.5 Tf")
            streamContent.AppendLine("0.22 0.32 0.18 rg")
            streamContent.AppendLine("52 475 Td")
            streamContent.AppendLine("(INFORMACION DEL PAGO) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 8 Tf")
            streamContent.AppendLine("0.25 0.25 0.25 rg")
            streamContent.AppendLine("52 455 Td")
            streamContent.AppendLine("(Comprobante de compra registrado) Tj")
            streamContent.AppendLine("0 -12 Td")
            streamContent.AppendLine($"(Estado de Cuenta: PAGADO) Tj")
            streamContent.AppendLine("0 -12 Td")
            streamContent.AppendLine($"(Atendido en: Caja Principal) Tj")
            streamContent.AppendLine("0 -12 Td")
            streamContent.AppendLine($"(Restaurante El Buen Sazon) Tj")
            streamContent.AppendLine("ET")

            ' Mensaje de Agradecimiento y Pie
            streamContent.AppendLine("q")
            streamContent.AppendLine("0.776 0.420 0.282 RG")
            streamContent.AppendLine("1 w")
            streamContent.AppendLine("40 110 515 0 re")
            streamContent.AppendLine("S")
            streamContent.AppendLine("Q")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F2 10 Tf")
            streamContent.AppendLine("0.776 0.420 0.282 rg")
            streamContent.AppendLine("195 90 Td")
            streamContent.AppendLine("(Muchas gracias por su preferencia!) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 8 Tf")
            streamContent.AppendLine("0.5 0.5 0.5 rg")
            streamContent.AppendLine("120 75 Td")
            streamContent.AppendLine("(Gracias por su visita y preferencia. Conserve este comprobante para sus registros.) Tj")
            streamContent.AppendLine("ET")

            streamContent.AppendLine("BT")
            streamContent.AppendLine("/F1 7.5 Tf")
            streamContent.AppendLine("0.6 0.6 0.6 rg")
            streamContent.AppendLine("145 60 Td")
            streamContent.AppendLine("(Sistema de Gestion de Pedidos para Restaurantes  -  Desarrollado en VB.NET) Tj")
            streamContent.AppendLine("ET")

            ' 3. Compilar la estructura completa del documento PDF
            Dim contentBytes As Byte() = Encoding.ASCII.GetBytes(streamContent.ToString())
            ConstruirArchivoPDF(rutaArchivo, contentBytes)
        End Sub

        ''' <summary>
        ''' Ensambla los objetos PDF 1.4 y escribe el archivo en disco.
        ''' </summary>
        Private Shared Sub ConstruirArchivoPDF(rutaArchivo As String, contentBytes As Byte())
            Dim dir = Path.GetDirectoryName(rutaArchivo)
            If Not String.IsNullOrEmpty(dir) AndAlso Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            Using fs As New FileStream(rutaArchivo, FileMode.Create, FileAccess.Write)
                Using writer As New StreamWriter(fs, Encoding.ASCII)
                    Dim posicionesObjetos As New List(Of Long)()

                    ' Encabezado PDF 1.4
                    writer.Write("%PDF-1.4" & vbCrLf)
                    writer.Write("%âãÏÓ" & vbCrLf)
                    writer.Flush()

                    ' Objeto 1: Catalog
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("1 0 obj" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write("/Type /Catalog" & vbCrLf)
                    writer.Write("/Pages 2 0 R" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("endobj" & vbCrLf)
                    writer.Flush()

                    ' Objeto 2: Pages
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("2 0 obj" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write("/Type /Pages" & vbCrLf)
                    writer.Write("/Kids [3 0 R]" & vbCrLf)
                    writer.Write("/Count 1" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("endobj" & vbCrLf)
                    writer.Flush()

                    ' Objeto 3: Page (A4: 595 x 842 pt)
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("3 0 obj" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write("/Type /Page" & vbCrLf)
                    writer.Write("/Parent 2 0 R" & vbCrLf)
                    writer.Write("/MediaBox [0 0 595 842]" & vbCrLf)
                    writer.Write("/Contents 4 0 R" & vbCrLf)
                    writer.Write("/Resources <<" & vbCrLf)
                    writer.Write("  /Font <<" & vbCrLf)
                    writer.Write("    /F1 5 0 R" & vbCrLf)
                    writer.Write("    /F2 6 0 R" & vbCrLf)
                    writer.Write("  >>" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("endobj" & vbCrLf)
                    writer.Flush()

                    ' Objeto 4: Stream del Contenido
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("4 0 obj" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write($"/Length {contentBytes.Length}" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("stream" & vbCrLf)
                    writer.Flush()

                    fs.Write(contentBytes, 0, contentBytes.Length)
                    fs.Flush()

                    writer.Write(vbCrLf & "endstream" & vbCrLf)
                    writer.Write("endobj" & vbCrLf)
                    writer.Flush()

                    ' Objeto 5: Fuente Helvetica Regular (F1)
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("5 0 obj" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write("/Type /Font" & vbCrLf)
                    writer.Write("/Subtype /Type1" & vbCrLf)
                    writer.Write("/BaseFont /Helvetica" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("endobj" & vbCrLf)
                    writer.Flush()

                    ' Objeto 6: Fuente Helvetica Bold (F2)
                    posicionesObjetos.Add(fs.Position)
                    writer.Write("6 0 obj" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write("/Type /Font" & vbCrLf)
                    writer.Write("/Subtype /Type1" & vbCrLf)
                    writer.Write("/BaseFont /Helvetica-Bold" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("endobj" & vbCrLf)
                    writer.Flush()

                    ' Tabla XREF
                    Dim inicioXref As Long = fs.Position
                    writer.Write("xref" & vbCrLf)
                    writer.Write($"0 {posicionesObjetos.Count + 1}" & vbCrLf)
                    writer.Write("0000000000 65535 f " & vbCrLf)
                    For Each pos In posicionesObjetos
                        writer.Write(pos.ToString("D10") & " 00000 n " & vbCrLf)
                    Next

                    ' Trailer
                    writer.Write("trailer" & vbCrLf)
                    writer.Write("<<" & vbCrLf)
                    writer.Write($"/Size {posicionesObjetos.Count + 1}" & vbCrLf)
                    writer.Write("/Root 1 0 R" & vbCrLf)
                    writer.Write(">>" & vbCrLf)
                    writer.Write("startxref" & vbCrLf)
                    writer.Write(inicioXref.ToString() & vbCrLf)
                    writer.Write("%%EOF" & vbCrLf)
                    writer.Flush()
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Limpia caracteres especiales para su renderizado ASCII seguro dentro de streams PDF.
        ''' </summary>
        Private Shared Function SanitizarTextoPDF(texto As String) As String
            If String.IsNullOrEmpty(texto) Then Return String.Empty
            Dim sb As New StringBuilder(texto)
            sb.Replace("\", "\\")
            sb.Replace("(", "\(")
            sb.Replace(")", "\)")
            sb.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
            sb.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
            sb.Replace("ñ", "n").Replace("Ñ", "N")
            Return sb.ToString()
        End Function

    End Class
End Namespace
