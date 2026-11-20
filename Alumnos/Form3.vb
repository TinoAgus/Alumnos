Imports System.Net.Http
Imports MySqlConnector



Public Class Form3

    Private cadenaConexion As String =
        "Server=localhost;" &
        "Port=3307;" &
        "User ID=root;" &
        "Password=1234;" &
        "Connection Timeout=5;"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Inicia un bloque para detectar errores durante la conexión.
        Try

            ' Crea un objeto de conexión utilizando la cadena definida.
            ' Using garantiza que la conexión se libere al terminar
            ' el bloque, incluso si ocurre un error.
            Using conexion As New MySqlConnection(cadenaConexion)

                ' Abre la conexión con el servidor MySQL.
                conexion.Open()

                ' Muestra un mensaje para confirmar que la conexión
                ' se estableció correctamente.
                MessageBox.Show(
                    "¡Conexión exitosa con MySQL!" &
                    vbCrLf & "Servidor: localhost" &
                    vbCrLf & "Puerto: 3307" &
                    vbCrLf & "Usuario: root",
                    "Conexión exitosa",             ' Título del mensaje.
                    MessageBoxButtons.OK,           ' Botón Aceptar.
                    MessageBoxIcon.Information      ' Icono informativo.
                )

                ' Al terminar este bloque, la conexión se libera
                ' automáticamente mediante Using.
            End Using

            ' Captura los errores específicos del proveedor MySQL,
            ' por ejemplo, credenciales incorrectas o problemas del servidor.
        Catch ex As MySqlException

            ' Informa al usuario del error recibido desde MySQL.
            MessageBox.Show(
                "No fue posible conectar con MySQL." &
                vbCrLf & vbCrLf &
                "Error: " & ex.Message,
                "Error de conexión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

            ' Captura otros errores generales de la aplicación
            ' que no estén incluidos en MySqlException.
        Catch ex As Exception

            ' Muestra la descripción del error general.
            MessageBox.Show(
                "Ocurrió un error: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

            ' Finaliza el bloque de manejo de errores.
        End Try

        ' Finaliza el procedimiento del evento del botón.
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cadenaConexion As String =
        "Server=localhost;" &
        "Port=3307;" &
        "User ID=root;" &
        "Password=1234;"

        Try
            Using conexion As New MySqlConnection(cadenaConexion)
                conexion.Open()

                ' 1. Crear la base de datos "Escuela" (o "Alumnos" según tu preferencia)
                Dim sqlBD As String =
                "CREATE DATABASE IF NOT EXISTS Escuela " &
                "DEFAULT CHARACTER SET utf8mb4 " &
                "COLLATE utf8mb4_unicode_ci;"

                Using comando As New MySqlCommand(sqlBD, conexion)
                    comando.ExecuteNonQuery()
                End Using

                ' 2. Crear la tabla dentro de la BD "Escuela"
                Dim sqlAlumnos As String =
                "CREATE TABLE IF NOT EXISTS Escuela.Alumnos (" &
                "IdAlumno INT AUTO_INCREMENT PRIMARY KEY, " &
                "Nombre VARCHAR(100) NOT NULL, " &
                "Edad INT NOT NULL, " &
                "Correo VARCHAR(150) NULL, " &
                "Carrera VARCHAR(100) NOT NULL, " &
                "Universidad VARCHAR(100) NOT NULL" &
                ");"

                Using comando As New MySqlCommand(sqlAlumnos, conexion)
                    comando.ExecuteNonQuery()
                End Using

                MessageBox.Show(
                "Base de datos y tabla creadas correctamente.",
                "Proceso completado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            End Using

        Catch ex As MySqlException
            MessageBox.Show(
            "Error de MySQL: " & ex.Message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show(
            "Error: " & ex.Message,
            "Error general",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Alumnos As New Form1()
        Alumnos.Show()
    End Sub
End Class