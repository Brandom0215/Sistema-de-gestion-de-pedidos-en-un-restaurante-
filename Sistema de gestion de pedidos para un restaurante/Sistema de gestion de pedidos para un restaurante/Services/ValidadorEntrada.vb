Imports System
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace Services
    ''' <summary>
    ''' Módulo centralizado de validación y control de entradas para interfaces de usuario.
    ''' Implementa bloqueo de caracteres inválidos por teclado en tiempo real (KeyPress),
    ''' límites estrictos de longitud (MaxLength) para evitar desbordamientos de base de datos
    ''' y validaciones lógicas de tipos de datos, montos, identificaciones y formatos.
    ''' </summary>
    Public Module ValidadorEntrada

        ' Expresión regular robusta para verificación de correos electrónicos
        Private ReadOnly RegexEmail As New Regex("^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", RegexOptions.Compiled Or RegexOptions.IgnoreCase)

        ' =========================================================================
        ' 1. CONFIGURACIÓN COMPLETA DE CONTROLES (MaxLength + KeyPress)
        ' =========================================================================

        ''' <summary>
        ''' Configura un TextBox para aceptar únicamente montos monetarios positivos con hasta 2 decimales.
        ''' </summary>
        Public Sub ConfigurarCampoMoneda(txt As TextBox, Optional maxLength As Integer = 11, Optional maxEnteros As Integer = 8, Optional maxDecimales As Integer = 2)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloMoneda(txt, e, maxEnteros, maxDecimales)
        End Sub

        ''' <summary>
        ''' Configura un TextBox para RUC o Cédula panameña (números, letras DV/provincia, guiones).
        ''' Convierte automáticamente a mayúsculas y restringe a 30 caracteres (VARCHAR(30)).
        ''' </summary>
        Public Sub ConfigurarCampoRucCedula(txt As TextBox, Optional maxLength As Integer = 30)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            txt.CharacterCasing = CharacterCasing.Upper
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloRucCedula(txt, e)
        End Sub

        ''' <summary>
        ''' Configura un TextBox para Nombre del Cliente o Razón Social (letras, espacios, puntuación segura).
        ''' Restringe a 100 caracteres (VARCHAR(100)).
        ''' </summary>
        Public Sub ConfigurarCampoRazonSocial(txt As TextBox, Optional maxLength As Integer = 100)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloRazonSocial(txt, e)
        End Sub

        ''' <summary>
        ''' Configura un TextBox para números telefónicos (+507, guiones, paréntesis, espacios).
        ''' Restringe a 30 caracteres (VARCHAR(30)).
        ''' </summary>
        Public Sub ConfigurarCampoTelefono(txt As TextBox, Optional maxLength As Integer = 30)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloTelefono(txt, e)
        End Sub

        ''' <summary>
        ''' Configura un TextBox para correo electrónico (sin espacios, caracteres permitidos en emails).
        ''' Restringe a 100 caracteres (VARCHAR(100)).
        ''' </summary>
        Public Sub ConfigurarCampoCorreo(txt As TextBox, Optional maxLength As Integer = 100)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloCorreo(txt, e)
        End Sub

        ''' <summary>
        ''' Configura un TextBox para dirección física/fiscal.
        ''' Restringe a 200 caracteres para evitar desbordamientos.
        ''' </summary>
        Public Sub ConfigurarCampoDireccion(txt As TextBox, Optional maxLength As Integer = 200)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloDireccion(txt, e)
        End Sub

        ''' <summary>
        ''' Configura un TextBox de búsqueda de texto o IDs numéricos.
        ''' Bloquea comillas simples (') para evitar roturas de RowFilter/SQL y limita a 50 caracteres.
        ''' </summary>
        Public Sub ConfigurarCampoBusqueda(txt As TextBox, Optional maxLength As Integer = 50)
            If txt Is Nothing Then Return
            txt.MaxLength = maxLength
            AddHandler txt.KeyPress, Sub(s, e) PermitirSoloBusqueda(txt, e)
        End Sub

        ' =========================================================================
        ' 2. BLOQUEO EN TIEMPO REAL POR TECLADO (KeyPress)
        ' =========================================================================

        ''' <summary>
        ''' Bloquea cualquier carácter que no sea dígito, tecla de control o punto decimal.
        ''' Admite punto '.' y coma ',' convirtiéndolo automáticamente a punto.
        ''' Limita la cantidad de decimales al escribir después del punto.
        ''' </summary>
        Public Sub PermitirSoloMoneda(txt As TextBox, e As KeyPressEventArgs, Optional maxEnteros As Integer = 8, Optional maxDecimales As Integer = 2)
            ' 1. Siempre permitir teclas de control (Backspace, Delete, Ctrl+C, Ctrl+V, etc.)
            If Char.IsControl(e.KeyChar) Then Return

            ' 2. Normalizar coma del teclado numérico a punto decimal
            If e.KeyChar = ","c Then
                e.KeyChar = "."c
            End If

            ' 3. Manejo del punto decimal
            If e.KeyChar = "."c Then
                ' No permitir si ya contiene un punto
                If txt.Text.Contains(".") Then
                    e.Handled = True
                    Return
                End If
                ' Si el campo está vacío y escribe '.', convertir a '0.'
                If txt.Text.Length = 0 OrElse txt.SelectionLength = txt.Text.Length Then
                    txt.Text = "0."
                    txt.SelectionStart = txt.Text.Length
                    e.Handled = True
                    Return
                End If
                Return
            End If

            ' 4. Solo permitir dígitos numéricos
            If Not Char.IsDigit(e.KeyChar) Then
                e.Handled = True
                Return
            End If

            ' 5. Validar posiciones de decimales vs enteros
            Dim textoActual As String = txt.Text
            Dim puntoIndex As Integer = textoActual.IndexOf("."c)

            If puntoIndex >= 0 Then
                ' El cursor está después del punto decimal
                If txt.SelectionStart > puntoIndex Then
                    Dim parteDecimal As String = textoActual.Substring(puntoIndex + 1)
                    ' Si no hay texto seleccionado para reemplazar y ya tiene el máximo de decimales, bloquear
                    If txt.SelectionLength = 0 AndAlso parteDecimal.Length >= maxDecimales Then
                        e.Handled = True
                        Return
                    End If
                Else
                    ' El cursor está antes del punto decimal
                    Dim parteEntera As String = textoActual.Substring(0, puntoIndex)
                    If txt.SelectionLength = 0 AndAlso parteEntera.Length >= maxEnteros Then
                        e.Handled = True
                        Return
                    End If
                End If
            Else
                ' Aún no hay punto decimal: limitar longitud de la parte entera
                If txt.SelectionLength = 0 AndAlso textoActual.Length >= maxEnteros Then
                    e.Handled = True
                    Return
                End If
            End If
        End Sub

        ''' <summary>
        ''' Bloquea caracteres inválidos para RUC o Cédula (solo dígitos, letras, guión y espacio para DV).
        ''' </summary>
        Public Sub PermitirSoloRucCedula(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            ' Convertir letras a mayúscula automáticamente
            If Char.IsLetter(e.KeyChar) Then
                e.KeyChar = Char.ToUpper(e.KeyChar)
                Return
            End If

            ' Dígitos, guión '-' y espacio ' ' (para DV 89)
            If Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "-"c OrElse e.KeyChar = " "c Then
                Return
            End If

            ' Cualquier otro carácter queda bloqueado
            e.Handled = True
        End Sub

        ''' <summary>
        ''' Bloquea caracteres inválidos para Nombres de Clientes y Razón Social.
        ''' Permite letras (incluyendo acentos españoles), números, espacios y signos legales comunes (&, ., ,, ', -).
        ''' </summary>
        Public Sub PermitirSoloRazonSocial(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            ' Letras (incluye acentos, ñ, ü) y dígitos
            If Char.IsLetterOrDigit(e.KeyChar) Then Return

            ' Caracteres seguros de nombres y empresas
            Select Case e.KeyChar
                Case " "c, "."c, ","c, "-"c, "'"c, "&"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Bloquea caracteres inválidos para números de teléfono (solo dígitos, +, -, espacios y paréntesis).
        ''' </summary>
        Public Sub PermitirSoloTelefono(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If Char.IsDigit(e.KeyChar) Then Return

            Select Case e.KeyChar
                Case "+"c, "-"c, " "c, "("c, ")"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Bloquea caracteres prohibidos en correos electrónicos (incluyendo espacios).
        ''' </summary>
        Public Sub PermitirSoloCorreo(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            ' Espacios estrictamente bloqueados en email
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
        ''' Bloquea comillas o caracteres de control inseguros.
        ''' </summary>
        Public Sub PermitirSoloDireccion(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            If Char.IsLetterOrDigit(e.KeyChar) Then Return

            Select Case e.KeyChar
                Case " "c, "#"c, "-"c, "."c, ","c, "/"c, "º"c, "ª"c, "("c, ")"c
                    Return
                Case Else
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' Permite búsqueda alfanumérica y bloquea comillas simples (') y puntos y coma (;) que puedan romper filtros.
        ''' </summary>
        Public Sub PermitirSoloBusqueda(txt As TextBox, e As KeyPressEventArgs)
            If Char.IsControl(e.KeyChar) Then Return

            ' Bloquear comillas y punto y coma
            If e.KeyChar = "'"c OrElse e.KeyChar = ";"c OrElse e.KeyChar = """"c Then
                e.Handled = True
                Return
            End If
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

            ' Intentar parsear con cultura actual o invariante
            If Not (Decimal.TryParse(textoLimpio, NumberStyles.Any, CultureInfo.CurrentCulture, monto) OrElse
                    Decimal.TryParse(textoLimpio, NumberStyles.Any, CultureInfo.InvariantCulture, monto)) Then
                Return False
            End If

            ' Redondear a 2 decimales para precisión contable
            monto = Math.Round(monto, 2)
            Return (monto >= min AndAlso monto <= max)
        End Function

        ''' <summary>
        ''' Valida que el correo electrónico cumpla con el formato estándar (usuario@dominio.com).
        ''' </summary>
        Public Function EsCorreoValido(correo As String) As Boolean
            If String.IsNullOrWhiteSpace(correo) Then Return False
            Return RegexEmail.IsMatch(correo.Trim())
        End Function

        ''' <summary>
        ''' Valida que el número de teléfono tenga una cantidad razonable de dígitos (mínimo 7).
        ''' Si es opcional y viene vacío, retorna True.
        ''' </summary>
        Public Function EsTelefonoValido(telefono As String, Optional permitirVacio As Boolean = True) As Boolean
            If String.IsNullOrWhiteSpace(telefono) Then Return permitirVacio

            ' Contar solo dígitos numéricos
            Dim digitos As Integer = 0
            For Each c In telefono
                If Char.IsDigit(c) Then digitos += 1
            Next

            Return (digitos >= 7 AndAlso digitos <= 15)
        End Function

        ''' <summary>
        ''' Valida que la cédula o RUC tenga un formato y longitud mínima válida (al menos 3 caracteres).
        ''' </summary>
        Public Function EsRucCedulaValida(ruc As String) As Boolean
            If String.IsNullOrWhiteSpace(ruc) Then Return False
            Dim limpio As String = ruc.Trim()
            Return (limpio.Length >= 3 AndAlso limpio.Length <= 30)
        End Function

        ''' <summary>
        ''' Valida que la razón social o nombre de cliente tenga al menos 2 caracteres.
        ''' </summary>
        Public Function EsRazonSocialValida(razon As String) As Boolean
            If String.IsNullOrWhiteSpace(razon) Then Return False
            Dim limpio As String = razon.Trim()
            Return (limpio.Length >= 2 AndAlso limpio.Length <= 100)
        End Function

    End Module
End Namespace
