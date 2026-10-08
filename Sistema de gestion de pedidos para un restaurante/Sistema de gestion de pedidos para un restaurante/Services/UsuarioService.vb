Imports System
Imports System.Collections.Generic
Imports System.Data
Imports Npgsql
Imports BCrypt.Net
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Data
Imports Sistema_de_gestion_de_pedidos_para_un_restaurante.Models

Namespace Services
    ''' <summary>
    ''' Implementación orientada a objetos del servicio de gestión de usuarios en PostgreSQL (POO).
    ''' </summary>
    Public Class UsuarioService
        Implements IUsuarioService

        Public Function Autenticar(usuarioInput As String, passwordInput As String, ByRef rolRetornado As String) As Boolean Implements IUsuarioService.Autenticar
            If String.IsNullOrWhiteSpace(usuarioInput) OrElse String.IsNullOrWhiteSpace(passwordInput) Then
                Return False
            End If

            Dim inputUsr As String = usuarioInput.Trim()
            Dim inputPwd As String = passwordInput.Trim()

            Try
                Dim sql As String = "SELECT id_usuario, nombre_usuario, password_hash, nombre_completo, rol FROM usuarios WHERE LOWER(nombre_usuario) = LOWER(@u) OR LOWER(nombre_completo) = LOWER(@u) LIMIT 1;"
                Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, New NpgsqlParameter("@u", inputUsr))

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim row = dt.Rows(0)
                    Dim hashAlmacenado As String = row("password_hash").ToString()
                    Dim strRol As String = row("rol").ToString()

                    Dim esValido As Boolean = False
                    Try
                        esValido = BCrypt.Net.BCrypt.Verify(inputPwd, hashAlmacenado)
                    Catch
                        esValido = String.Equals(inputPwd, hashAlmacenado, StringComparison.Ordinal)
                    End Try

                    If esValido Then
                        Dim enumRol As RolUsuarioEnum = RolUsuarioExtensions.ParsearRol(strRol)
                        rolRetornado = RolUsuarioExtensions.ObtenerEtiqueta(enumRol)
                        Return True
                    End If
                End If
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try

            Return False
        End Function

        Public Function RegistrarCliente(nombre As String, telefono As String, correo As String, password As String) As Boolean Implements IUsuarioService.RegistrarCliente
            If String.IsNullOrWhiteSpace(correo) OrElse String.IsNullOrWhiteSpace(password) Then
                Return False
            End If

            Dim nuevoCorreo As String = correo.Trim().ToLower()
            Dim strNombre As String = If(String.IsNullOrWhiteSpace(nombre), "Cliente", nombre.Trim())
            Dim strPassword As String = password.Trim()

            Try
                Dim sqlExiste As String = "SELECT COUNT(*) FROM usuarios WHERE LOWER(nombre_usuario) = LOWER(@correo);"
                Dim dtEx = ConexionBD.EjecutarConsultaDataTable(sqlExiste, New NpgsqlParameter("@correo", nuevoCorreo))
                If dtEx IsNot Nothing AndAlso Convert.ToInt32(dtEx.Rows(0)(0)) > 0 Then
                    Return False
                End If

                Dim pwdHash As String = BCrypt.Net.BCrypt.HashPassword(strPassword)

                Dim sqlIns As String = "INSERT INTO usuarios (nombre_usuario, password_hash, nombre_completo, rol) VALUES (@user, @hash, @nombre, 'Cliente');"
                Dim p1 As New NpgsqlParameter("@user", nuevoCorreo)
                Dim p2 As New NpgsqlParameter("@hash", pwdHash)
                Dim p3 As New NpgsqlParameter("@nombre", strNombre)

                Dim filas = ConexionBD.EjecutarComando(sqlIns, p1, p2, p3)
                Return (filas > 0)
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
                Throw
            End Try
        End Function

        Public Function ObtenerTodos() As IReadOnlyList(Of UsuarioModel) Implements IUsuarioService.ObtenerTodos
            Dim lista As New List(Of UsuarioModel)()
            Try
                Dim sql As String = "SELECT id_usuario, nombre_usuario, password_hash, nombre_completo, rol, fecha_registro FROM usuarios ORDER BY id_usuario ASC;"
                Dim dt = ConexionBD.EjecutarConsultaDataTable(sql)
                If dt IsNot Nothing Then
                    For Each row As DataRow In dt.Rows
                        Dim nombre As String = row("nombre_completo").ToString()
                        Dim user As String = row("nombre_usuario").ToString()
                        Dim hash As String = row("password_hash").ToString()
                        Dim strRol As String = row("rol").ToString()
                        Dim enumRol As RolUsuarioEnum = RolUsuarioExtensions.ParsearRol(strRol)

                        lista.Add(New UsuarioModel(nombre, user, hash, "", enumRol))
                    Next
                End If
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
            End Try
            Return lista.AsReadOnly()
        End Function

        Public Function BuscarPorCorreo(correo As String) As UsuarioModel Implements IUsuarioService.BuscarPorCorreo
            If String.IsNullOrWhiteSpace(correo) Then Return Nothing
            Dim target As String = correo.Trim().ToLower()
            Try
                Dim sql As String = "SELECT id_usuario, nombre_usuario, password_hash, nombre_completo, rol FROM usuarios WHERE LOWER(nombre_usuario) = LOWER(@c) OR LOWER(nombre_completo) = LOWER(@c) LIMIT 1;"
                Dim dt = ConexionBD.EjecutarConsultaDataTable(sql, New NpgsqlParameter("@c", target))
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim row = dt.Rows(0)
                    Return New UsuarioModel(
                        row("nombre_completo").ToString(),
                        row("nombre_usuario").ToString(),
                        row("password_hash").ToString(),
                        "",
                        RolUsuarioExtensions.ParsearRol(row("rol").ToString())
                    )
                End If
            Catch ex As Exception
                ConexionBD.RegistrarFalloServidor(ex.Message)
            End Try
            Return Nothing
        End Function

    End Class
End Namespace
