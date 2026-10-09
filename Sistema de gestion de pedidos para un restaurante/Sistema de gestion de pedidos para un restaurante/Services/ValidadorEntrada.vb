Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace Services
    ''' <summary>
    ''' Módulo centralizado de validación y control de entradas para interfaces de usuario.
    ''' Implementa bloqueo de caracteres inválidos por teclado en tiempo real (KeyPress),
    ''' límites estrictos de longitud (MaxLength) para evitar desbordamientos de base de datos,
    ''' sanitización contra pegado (Paste) inválido y validaciones lógicas de reglas de negocio.
    ''' </summary>
    Public Module ValidadorEntrada

        ' TLDs no válidos comunes (errores tipográficos frecuentes o dominios ficticios de prueba)
        Private ReadOnly TldsInvalidos As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
            "prueba", "test", "ejemplo", "example", "falso", "fake", "temp", "temporal",
            "invalid", "local", "localhost", "nada", "algo", "demo", "inventado", "bla", "dummy",
            "come", "con", "cmo", "comm", "conm", "xom", "vom", "cm", "om", "coom",
            "nett", "neto", "ner", "orgg", "ogr", "orga", "eddu"
        }

        ' TLDs legítimos reconocidos (genéricos, geográficos de Panamá/Latinoamérica e internacionales principales)
        Private ReadOnly TldsValidos As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
            "com", "net", "org", "edu", "gov", "mil", "int", "info", "biz", "pro", "name",
            "pa", "es", "co", "mx", "cr", "ar", "cl", "pe", "ec", "gt", "hn", "ni", "sv",
            "bo", "py", "uy", "ve", "do", "pr", "us", "ca", "uk", "de", "fr", "it", "br",
            "pt", "io", "ai", "me", "tv", "cc", "app", "dev", "tech", "online", "site",
            "store", "shop", "cloud", "digital", "email", "xyz", "global", "lat", "live", "club"
        }

        ' =========================================================================
        ' 1. CONFIGURACIÓN COMPLETA DE CONTROLES (MaxLength + KeyPress + Sanitización)
        ' =========================================================================

        ''' <summary>
        ''' Configura un TextBox para aceptar únicamente montos monetarios positivos con hasta 2 decimales.
        ''' </summary>
        Public Sub ConfigurarCampoMoneda(txt As TextBox, Optional maxLength As Integer = 11, Optional maxEnteros As Integer = 8, Optional maxDecimales As Integer = 2)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloMoneda(txt, e, maxEnteros, maxDecimales)
            AddHandler txt.TextChanged, Sub(s, e)
                                            If txt.Text.Length > txt.MaxLength Then
                                                Dim pos = Math.Min(txt.SelectionStart, txt.MaxLength)
                                                txt.Text = txt.Text.Substring(0, txt.MaxLength)
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para RUC o Cédula panameña (números, letras DV/provincia, guiones, espacios).
        ''' Convierte automáticamente a mayúsculas y restringe a 30 caracteres (VARCHAR(30)).
        ''' </summary>
        Public Sub ConfigurarCampoRucCedula(txt As TextBox, Optional maxLength As Integer = 30)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            txt.CharacterCasing = CharacterCasing.Upper
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloRucCedula(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            ' Filtrar caracteres no permitidos en caso de pegado con Ctrl+V
                                            Dim filtrado = New String(texto.ToUpper().Where(Function(c) Char.IsLetterOrDigit(c) OrElse c = "-"c OrElse c = " "c).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para Nombre del Cliente o Razón Social.
        ''' Bloquea estrictamente números (0-9) por teclado y sanitiza pegado.
        ''' Restringe a 40 caracteres para un nombre realista y seguro.
        ''' </summary>
        Public Sub ConfigurarCampoRazonSocial(txt As TextBox, Optional maxLength As Integer = 40)
            ConfigurarCampoNombreCliente(txt, maxLength)
        End Sub

        ''' <summary>
        ''' Configura un TextBox para Nombre del Cliente o Razón Social (solo letras, espacios, acentos, signos de nombres).
        ''' BLOQUEA estrictamente números para evitar registros con dígitos en el nombre del cliente.
        ''' </summary>
        Public Sub ConfigurarCampoNombreCliente(txt As TextBox, Optional maxLength As Integer = 40)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloNombreCliente(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            ' En caso de pegado con Ctrl+V: eliminar cualquier dígito numérico y caracteres prohibidos
                                            Dim filtrado = New String(texto.Where(Function(c) (Char.IsLetter(c) OrElse " .,'-".Contains(c)) AndAlso Not Char.IsDigit(c)).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para números telefónicos (+507, guiones, paréntesis, espacios).
        ''' BLOQUEA letras y restringe a 30 caracteres (VARCHAR(30)).
        ''' </summary>
        Public Sub ConfigurarCampoTelefono(txt As TextBox, Optional maxLength As Integer = 30)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloTelefono(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) Char.IsDigit(c) OrElse "+- ()".Contains(c)).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para correo electrónico (sin espacios, caracteres permitidos en emails).
        ''' Restringe a 100 caracteres (VARCHAR(100)).
        ''' </summary>
        Public Sub ConfigurarCampoCorreo(txt As TextBox, Optional maxLength As Integer = 100)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloCorreo(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            ' Eliminar espacios en blanco y caracteres inválidos
                                            Dim filtrado = New String(texto.Where(Function(c) (Char.IsLetterOrDigit(c) OrElse "@._-+".Contains(c)) AndAlso c <> " "c).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para dirección física/fiscal.
        ''' Restringe a 80 caracteres para evitar desbordamientos y textos excesivamente largos.
        ''' </summary>
        Public Sub ConfigurarCampoDireccion(txt As TextBox, Optional maxLength As Integer = 80)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloDireccion(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) Char.IsLetterOrDigit(c) OrElse " #-,./ºª()".Contains(c)).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox de búsqueda de texto o IDs numéricos.
        ''' Bloquea comillas simples (') para evitar roturas de RowFilter/SQL y limita a 50 caracteres.
        ''' </summary>
        Public Sub ConfigurarCampoBusqueda(txt As TextBox, Optional maxLength As Integer = 50)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloBusqueda(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) c <> "'"c AndAlso c <> ";"c AndAlso c <> """"c).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para identificador de Mesa o Tipo de Entrega (ej: "Mesa 01", "Barra 2", "Para Llevar").
        ''' Restringe a 30 caracteres (VARCHAR(30) en BD).
        ''' </summary>
        Public Sub ConfigurarCampoMesa(txt As TextBox, Optional maxLength As Integer = 30)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloMesa(txt, e)
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) Char.IsLetterOrDigit(c) OrElse " -_#/".Contains(c)).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para contraseña (límite de longitud y bloqueo de espacios).
        ''' </summary>
        Public Sub ConfigurarCampoPassword(txt As TextBox, Optional maxLength As Integer = 50)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e)
                                         If Char.IsControl(e.KeyChar) Then Return
                                         If e.KeyChar = " "c Then e.Handled = True
                                     End Sub
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = texto.Replace(" ", "")
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para nombre de productos/platos (letras, dígitos, espacios y puntuación común de menú).
        ''' Restringe a 100 caracteres (VARCHAR(100) en BD).
        ''' </summary>
        Public Sub ConfigurarCampoNombrePlato(txt As TextBox, Optional maxLength As Integer = 100)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e)
                                         If Char.IsControl(e.KeyChar) Then Return
                                         If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                                             e.Handled = True
                                             Return
                                         End If
                                         If e.KeyChar = "'"c OrElse e.KeyChar = ";"c OrElse e.KeyChar = """"c Then
                                             e.Handled = True
                                             Return
                                         End If
                                     End Sub
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) c <> "'"c AndAlso c <> ";"c AndAlso c <> """"c).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox para tiempo de preparación (ej: "15 min", "10-20 min").
        ''' Restringe a 20 caracteres (VARCHAR(20) en BD).
        ''' </summary>
        Public Sub ConfigurarCampoTiempoCoccion(txt As TextBox, Optional maxLength As Integer = 20)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e)
                                         If Char.IsControl(e.KeyChar) Then Return
                                         If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                                             e.Handled = True
                                             Return
                                         End If
                                         If Char.IsLetterOrDigit(e.KeyChar) OrElse " -./~".Contains(e.KeyChar) Then
                                             Return
                                         Else
                                             e.Handled = True
                                         End If
                                     End Sub
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) Char.IsLetterOrDigit(c) OrElse " -./~".Contains(c)).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ''' <summary>
        ''' Configura un TextBox multilínea para descripción general de platos o notas.
        ''' Restringe a 250 caracteres.
        ''' </summary>
        Public Sub ConfigurarCampoDescripcion(txt As TextBox, Optional maxLength As Integer = 250)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e)
                                         If Char.IsControl(e.KeyChar) Then Return
                                         If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                                             e.Handled = True
                                             Return
                                         End If
                                         If e.KeyChar = "'"c OrElse e.KeyChar = ";"c Then
                                             e.Handled = True
                                             Return
                                         End If
                                     End Sub
            AddHandler txt.TextChanged, Sub(s, e)
                                            Dim texto = txt.Text
                                            If String.IsNullOrEmpty(texto) Then Return
                                            Dim filtrado = New String(texto.Where(Function(c) c <> "'"c AndAlso c <> ";"c).ToArray())
                                            If filtrado.Length > txt.MaxLength Then
                                                filtrado = filtrado.Substring(0, txt.MaxLength)
                                            End If
                                            If filtrado <> texto Then
                                                Dim pos = Math.Min(txt.SelectionStart, filtrado.Length)
                                                txt.Text = filtrado
                                                txt.SelectionStart = pos
                                            End If
                                        End Sub
        End Sub

        ' =========================================================================
        ' 2. BLOQUEO EN TIEMPO REAL POR TECLADO (KeyPress)
        ' =========================================================================

        ''' <summary>
        ''' Bloquea cualquier carácter que no sea dígito, tecla de control o punto decimal.
        ''' Admite punto '.' y coma ',' convirtiéndolo automáticamente a punto.
        ''' Limita la cantidad de decimales y longitud máxima de enteros.
        ''' </summary>
        Public Sub PermitirSoloMoneda(txt As TextBox, e As KeyPressEventArgs, Optional maxEnteros As Integer = 8, Optional maxDecimales As Integer = 2)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            If e.KeyChar = ","c Then
                e.KeyChar = "."c
            End If

            If e.KeyChar = "."c Then
                If txt.Text.Contains(".") Then
                    e.Handled = True
                    Return
                End If
                If txt.Text.Length = 0 OrElse txt.SelectionLength = txt.Text.Length Then
                    txt.Text = "0."
                    txt.SelectionStart = txt.Text.Length
                    e.Handled = True
                    Return
                End If
                Return
            End If

            If Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
                Return
            End If

            Dim textoActual As String = txt.Text
            Dim puntoIndex As Integer = textoActual.IndexOf("."c)

            If puntoIndex >= 0 Then
                If txt.SelectionStart > puntoIndex Then
                    Dim parteDecimal As String = textoActual.Substring(puntoIndex + 1)
                    If txt.SelectionLength = 0 AndAlso parteDecimal.Length >= maxDecimales Then
                        e.Handled = True
                        Return
                    End If
                Else
                    Dim parteEntera As String = textoActual.Substring(0, puntoIndex)
                    If txt.SelectionLength = 0 AndAlso parteEntera.Length >= maxEnteros Then
                        e.Handled = True
                        Return
                    End If
                End If
            Else
                If txt.SelectionLength = 0 AndAlso textoActual.Length >= maxEnteros Then
                    e.Handled = True
                    Return
                End If
            End If
        End Sub

        ''' <summary>
        ''' Bloquea caracteres inválidos para RUC o Cédula (solo dígitos, letras, guión y espacio para DV).
        ''' Convierte letras a mayúsculas y bloquea desbordamiento por longitud.
        ''' </summary>
        Public Sub PermitirSoloRucCedula(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            If Char.IsLetter(e.KeyChar) Then
                e.KeyChar = Char.ToUpper(e.KeyChar)
                Return
            End If

            If Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "-"c OrElse e.KeyChar = " "c Then
                Return
            End If

            e.Handled = True
        End Sub

        ''' <summary>
        ''' Bloquea caracteres inválidos para Nombres de Clientes y Razón Social.
        ''' Permite letras (incluyendo vocales con acento, ñ, ü), espacios y signos ortográficos seguros (., ,, -, ').
        ''' BLOQUEA ESTRICTAMENTE dígitos numéricos (0-9) y símbolos especiales.
        ''' </summary>
        Public Sub PermitirSoloNombreCliente(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            ' BLOQUEO ESTRICTO: Los nombres de clientes NO permiten números
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = True
                Return
            End If

            If Char.IsLetter(e.KeyChar) Then Return

            Select Case e.KeyChar
                Case " "c, "."c, ","c, "-"c, "'"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Alias de compatibilidad para PermitirSoloNombreCliente.
        ''' </summary>
        Public Sub PermitirSoloRazonSocial(txt As TextBox, e As KeyPressEventArgs)
            PermitirSoloNombreCliente(txt, e)
        End Sub

        ''' <summary>
        ''' Bloquea caracteres inválidos para números de teléfono (solo dígitos, +, -, espacios y paréntesis).
        ''' BLOQUEA ESTRICTAMENTE letras y caracteres no numéricos.
        ''' </summary>
        Public Sub PermitirSoloTelefono(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            If Char.IsDigit(e.KeyChar) Then Return

            Select Case e.KeyChar
                Case "+"c, "-"c, " "c, "("c, ")"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Bloquea caracteres prohibidos en correos electrónicos (incluyendo espacios en blanco).
        ''' </summary>
        Public Sub PermitirSoloCorreo(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            If e.KeyChar = " "c Then
                e.Handled = True
                Return
            End If

            If Char.IsLetterOrDigit(e.KeyChar) Then Return

            Select Case e.KeyChar
                Case "@"c, "."c, "_"c, "-"c, "+"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Permite caracteres válidos para direcciones fiscales o de entrega.
        ''' Bloquea comillas simples/dobles o caracteres de control inseguros.
        ''' </summary>
        Public Sub PermitirSoloDireccion(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            If Char.IsLetterOrDigit(e.KeyChar) Then Return

            Select Case e.KeyChar
                Case " "c, "#"c, "-"c, "."c, ","c, "/"c, "º"c, "ª"c, "("c, ")"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Permite búsqueda alfanumérica y bloquea comillas simples ('), comillas dobles (") y puntos y coma (;).
        ''' </summary>
        Public Sub PermitirSoloBusqueda(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If

            If e.KeyChar = "'"c OrElse e.KeyChar = ";"c OrElse e.KeyChar = """"c Then
                e.Handled = True
                Return
            End If
        End Sub

        ''' <summary>
        ''' Permite caracteres alfanuméricos, espacios, guiones y símbolos habituales para mesas (ej: "Mesa 01", "Barra #2").
        ''' </summary>
        Public Sub PermitirSoloMesa(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return
            If txt.SelectionLength = 0 AndAlso txt.Text.Length >= txt.MaxLength Then
                e.Handled = True
                Return
            End If
            If Char.IsLetterOrDigit(e.KeyChar) Then Return
            Select Case e.KeyChar
                Case " "c, "-"c, "_"c, "#"c, "/"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ' =========================================================================
        ' 3. VALIDACIONES LÓGICAS EN CÓDIGO (Reglas de Negocio)
        ' =========================================================================

        ''' <summary>
        ''' Valida que el texto represente un monto numérico positivo dentro de un rango permitido.
        ''' </summary>
        Public Function EsMontoValido(texto As String, ByRef monto As Decimal, Optional min As Decimal = 0.01D, Optional max As Decimal = 999999.99D) As Boolean
            If String.IsNullOrWhiteSpace(texto) Then Return False

            Dim textoLimpio As String = texto.Trim().Replace("$", "").Trim()

            If Not (Decimal.TryParse(textoLimpio, NumberStyles.Any, CultureInfo.CurrentCulture, monto) OrElse
                    Decimal.TryParse(textoLimpio, NumberStyles.Any, CultureInfo.InvariantCulture, monto)) Then
                Return False
            End If

            monto = Math.Round(monto, 2)
            Return (monto >= min AndAlso monto <= max)
        End Function

        ''' <summary>
        ''' Valida rigurosamente que el correo electrónico cumpla con el formato estándar (usuario@dominio.tld),
        ''' que el dominio posea una extensión válida existente y NO contenga extensiones de prueba (.prueba, .test)
        ''' ni errores tipográficos evidentes como (.come, .con, .cmo).
        ''' </summary>
        Public Function EsCorreoValido(correo As String) As Boolean
            If String.IsNullOrWhiteSpace(correo) Then Return False
            Dim limpio = correo.Trim().ToLowerInvariant()

            ' 1. Longitud razonable
            If limpio.Length < 6 OrElse limpio.Length > 100 Then Return False

            ' 2. Debe contener exactamente un carácter '@'
            Dim partes = limpio.Split("@"c)
            If partes.Length <> 2 Then Return False

            Dim usuario = partes(0)
            Dim dominio = partes(1)

            ' 3. Validación de la parte local (usuario)
            If String.IsNullOrWhiteSpace(usuario) OrElse usuario.StartsWith(".") OrElse usuario.EndsWith(".") OrElse usuario.Contains("..") Then
                Return False
            End If
            If Not Regex.IsMatch(usuario, "^[a-z0-9]+([._%+-][a-z0-9]+)*$") Then
                Return False
            End If

            ' 4. Validación de la parte del dominio
            If String.IsNullOrWhiteSpace(dominio) OrElse dominio.StartsWith(".") OrElse dominio.EndsWith(".") OrElse dominio.Contains("..") Then
                Return False
            End If

            Dim segmentosDominio = dominio.Split("."c)
            If segmentosDominio.Length < 2 Then Return False

            ' Cada segmento del dominio debe ser alfanumérico válido
            For Each seg In segmentosDominio
                If String.IsNullOrWhiteSpace(seg) OrElse seg.StartsWith("-") OrElse seg.EndsWith("-") Then
                    Return False
                End If
                If Not Regex.IsMatch(seg, "^[a-z0-9-]+$") Then
                    Return False
                End If
            Next

            ' 5. Validación del TLD (último segmento del dominio, ej: "com", "pa", "org")
            Dim tld = segmentosDominio.Last()
            If tld.Length < 2 OrElse tld.Length > 10 OrElse Not Regex.IsMatch(tld, "^[a-z]+$") Then
                Return False
            End If

            ' Bloquear explícitamente TLDs de prueba o errores tipográficos (.prueba, .come, .con, .test, etc.)
            If TldsInvalidos.Contains(tld) Then
                Return False
            End If

            ' 6. Verificación de proveedores de correo masivos populares para evitar typos en sus dominios
            ' (ej: gmail.come -> rechazado; hotmail.con -> rechazado; gmail.prueba -> rechazado)
            Dim nombrePrincipal = segmentosDominio(0)
            Select Case nombrePrincipal
                Case "gmail"
                    If tld <> "com" Then Return False
                Case "hotmail", "outlook", "yahoo"
                    If tld <> "com" AndAlso tld <> "es" AndAlso tld <> "pa" Then Return False
                Case "icloud"
                    If tld <> "com" Then Return False
            End Select

            ' 7. El TLD debe pertenecer a la lista de TLDs oficiales/reconocidos o tener 2 caracteres de país (ej: .pa, .es, .co)
            If Not TldsValidos.Contains(tld) AndAlso tld.Length <> 2 Then
                Return False
            End If

            Return True
        End Function

        ''' <summary>
        ''' Valida que el número de teléfono tenga una cantidad razonable de dígitos (mínimo 7, máximo 15).
        ''' Si es opcional y viene vacío, retorna True.
        ''' </summary>
        Public Function EsTelefonoValido(telefono As String, Optional permitirVacio As Boolean = True) As Boolean
            If String.IsNullOrWhiteSpace(telefono) Then Return permitirVacio

            Dim digitos As Integer = 0
            For Each c In telefono
                If Char.IsDigit(c) Then digitos += 1
            Next

            Return (digitos >= 7 AndAlso digitos <= 15)
        End Function

        ''' <summary>
        ''' Valida que la cédula o RUC tenga un formato y longitud válida (entre 3 y 30 caracteres).
        ''' </summary>
        Public Function EsRucCedulaValida(ruc As String) As Boolean
            If String.IsNullOrWhiteSpace(ruc) Then Return False
            Dim limpio As String = ruc.Trim()
            Return (limpio.Length >= 3 AndAlso limpio.Length <= 30)
        End Function

        ''' <summary>
        ''' Valida que el nombre del cliente o razón social tenga al menos 2 caracteres,
        ''' NO contenga números y posea al menos una letra.
        ''' </summary>
        Public Function EsNombreClienteValido(nombre As String) As Boolean
            If String.IsNullOrWhiteSpace(nombre) Then Return False
            Dim limpio As String = nombre.Trim()
            If limpio.Length < 2 OrElse limpio.Length > 40 Then Return False

            Dim tieneLetra As Boolean = False
            For Each c In limpio
                If Char.IsDigit(c) Then Return False ' Los nombres NO pueden contener dígitos
                If Char.IsLetter(c) Then tieneLetra = True
            Next
            Return tieneLetra
        End Function

        ''' <summary>
        ''' Alias de compatibilidad para EsNombreClienteValido.
        ''' </summary>
        Public Function EsRazonSocialValida(razon As String) As Boolean
            Return EsNombreClienteValido(razon)
        End Function

        ''' <summary>
        ''' Valida la dirección fiscal / de entrega (hasta 80 caracteres para evitar desbordamientos, no vacía si es obligatoria).
        ''' </summary>
        Public Function EsDireccionValida(direccion As String, Optional permitirVacio As Boolean = True) As Boolean
            If String.IsNullOrWhiteSpace(direccion) Then Return permitirVacio
            Dim limpio As String = direccion.Trim()
            Return (limpio.Length >= 3 AndAlso limpio.Length <= 80)
        End Function

        ''' <summary>
        ''' Valida que el identificador de mesa o servicio tenga entre 1 y 30 caracteres.
        ''' </summary>
        Public Function EsMesaValida(mesa As String) As Boolean
            If String.IsNullOrWhiteSpace(mesa) Then Return False
            Dim limpio = mesa.Trim()
            Return (limpio.Length >= 1 AndAlso limpio.Length <= 30)
        End Function

        ''' <summary>
        ''' Valida que la contraseña cumpla con una longitud mínima y máxima segura.
        ''' </summary>
        Public Function EsPasswordValido(password As String, Optional minLength As Integer = 4, Optional maxLength As Integer = 50) As Boolean
            If String.IsNullOrWhiteSpace(password) Then Return False
            Dim limpio = password.Trim()
            Return (limpio.Length >= minLength AndAlso limpio.Length <= maxLength)
        End Function

        ''' <summary>
        ''' Valida que el nombre del plato tenga al menos 2 caracteres y una longitud válida (máx 100).
        ''' </summary>
        Public Function EsNombrePlatoValido(nombre As String) As Boolean
            If String.IsNullOrWhiteSpace(nombre) Then Return False
            Dim limpio = nombre.Trim()
            Return (limpio.Length >= 2 AndAlso limpio.Length <= 100)
        End Function

    End Module
End Namespace
