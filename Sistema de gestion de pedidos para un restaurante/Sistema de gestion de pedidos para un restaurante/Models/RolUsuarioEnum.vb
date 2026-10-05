Namespace Models
    ''' <summary>
    ''' Enumeración fuertemente tipada para los roles de usuario del sistema (POO).
    ''' </summary>
    Public Enum RolUsuarioEnum
        ''' <summary> Cliente / Usuario de autoatención o tótem digital </summary>
        Cliente = 1

        ''' <summary> Cajero responsable del procesamiento de pagos y caja </summary>
        Cajero = 2

        ''' <summary> Personal de cocina para monitoreo de comandas KDS </summary>
        Cocina = 3

        ''' <summary> Administrador con acceso completo a todos los módulos </summary>
        Administrador = 4
    End Enum

    ''' <summary>
    ''' Clase auxiliar con métodos de extensión y utilidades para RolUsuarioEnum.
    ''' </summary>
    Public Module RolUsuarioExtensions

        Public Function EsPersonal(rol As RolUsuarioEnum) As Boolean
            Return rol = RolUsuarioEnum.Cajero OrElse rol = RolUsuarioEnum.Cocina OrElse rol = RolUsuarioEnum.Administrador
        End Function

        Public Function EsCliente(rol As RolUsuarioEnum) As Boolean
            Return rol = RolUsuarioEnum.Cliente
        End Function

        Public Function ObtenerEtiqueta(rol As RolUsuarioEnum) As String
            Select Case rol
                Case RolUsuarioEnum.Administrador
                    Return "Administrador"
                Case RolUsuarioEnum.Cajero
                    Return "Cajero / Personal de Sala"
                Case RolUsuarioEnum.Cocina
                    Return "Personal de Cocina (KDS)"
                Case Else
                    Return "Cliente (Autoatención)"
            End Select
        End Function

        Public Function ParsearRol(rolTexto As String) As RolUsuarioEnum
            If String.IsNullOrWhiteSpace(rolTexto) Then Return RolUsuarioEnum.Cliente

            Dim textoLimpio As String = rolTexto.ToLower()
            If textoLimpio.Contains("admin") Then
                Return RolUsuarioEnum.Administrador
            ElseIf textoLimpio.Contains("cajer") OrElse textoLimpio.Contains("caja") Then
                Return RolUsuarioEnum.Cajero
            ElseIf textoLimpio.Contains("cocin") OrElse textoLimpio.Contains("chef") OrElse textoLimpio.Contains("kds") Then
                Return RolUsuarioEnum.Cocina
            Else
                Return RolUsuarioEnum.Cliente
            End If
        End Function

    End Module
End Namespace
