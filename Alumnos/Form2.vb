Imports System.Net.Http.Headers
Imports MySqlConnector

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
    End Sub

    Private Sub CargarDatos()
        Dim conectaConexion As String =
            "Server=localhost;" &
            "Port=3307;" &
            "User ID=root;" &
            "Password=1234;" &
            "Database=Escuela;"

        Dim consultaSQL As String = "SELECT IdAlumno, Nombre, Edad, Correo, Carrera, Universidad FROM Escuela.Alumnos;"

        Try
            Using conexion As New MySqlConnection(conectaConexion)
                Using adaptador As New MySqlDataAdapter(consultaSQL, conexion)
                    Dim tablaAlumnos As New DataTable()
                    adaptador.Fill(tablaAlumnos)

                    DataGridView1.DataSource = tablaAlumnos
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Error de MySQL al cargar datos: " & ex.Message,
                            "Error de Base de Datos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message,
                            "Error General",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
                String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
                String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
                String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
                String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
                String.IsNullOrWhiteSpace(TextBox6.Text) Then
            MessageBox.Show("Ingrese todos los valores", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim edadValida As Integer
        Dim idValido As Integer


        If Not Integer.TryParse(TextBox1.Text, idValido) Then
            MessageBox.Show("Ingrese su ID correcto", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        If Not Integer.TryParse(TextBox3.Text, edadValida) Then
            MessageBox.Show("Ingrese un numero entero valido para la edad.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim conectaConexion As String =
            "Server=localhost;" &
            "port=3307;" &
            "User ID=root;" &
            "Password=1234;" &
            "Database=Escuela;"

        Dim actualizarAlumno As String =
            "UPDATE Escuela.Alumnos SET " &
            "Nombre = @Nombre, " &
            "Edad = @Edad, " &
            "Correo = @Correo, " &
            "Carrera = @Carrera, " &
            "Universidad = @Universidad " &
            "WHERE IdAlumno = @IdAlumno;"

        Try
            Using conexion As New MySqlConnection(conectaConexion)
                conexion.Open()

                Using comando As New MySqlCommand(actualizarAlumno, conexion)
                    comando.Parameters.AddWithValue("@IdAlumno", idValido)
                    comando.Parameters.AddWithValue("@Nombre", TextBox2.Text.Trim())
                    comando.Parameters.AddWithValue("@Edad", edadValida)
                    comando.Parameters.AddWithValue("@Correo", TextBox4.Text.Trim())
                    comando.Parameters.AddWithValue("@Carrera", TextBox5.Text.Trim())
                    comando.Parameters.AddWithValue("@Universidad", TextBox6.Text.Trim())

                    Dim filasAfectadas As Integer = comando.ExecuteNonQuery()

                    If filasAfectadas > 0 Then
                        MessageBox.Show("¡Datos del alumno actualizados correctamente!", "Actualizacion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()
                        TextBox5.Clear()
                        TextBox6.Clear()

                        CargarDatos()

                    Else
                        MessageBox.Show("No se encontro ningun alumno con ese ID.", "Sin cambios", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error de MySQL: " & ex.Message,
                        "Error de Base de Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message,
                            "Error General",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Ingresa el ID del alumno que deseas eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idEliminar As Integer
        If Not Integer.TryParse(TextBox1.Text, idEliminar) Then
            MessageBox.Show("Ingrese un número válido", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim respuesta As DialogResult = MessageBox.Show("¿Estas seguro de eliminar al alumno con ID ?" & idEliminar & "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)


        If respuesta = DialogResult.No Then Exit Sub

        Dim conexion1 As String =
            "server=localhost;" &
            "Port=3307;" &
            "User ID=root;" &
            "Password=1234;" &
            "Database=Escuela;"

        Dim eliminarAlumno As String = "DELETE FROM Escuela.Alumnos WHERE IdAlumno = @idAlumno;"

        Try
            Using conecta As New MySqlConnection(conexion1)
                conecta.Open()

                Using comando2 As New MySqlCommand(eliminarAlumno, conecta)
                    comando2.Parameters.AddWithValue("@idAlumno", idEliminar)

                    Dim afectados As Integer = comando2.ExecuteNonQuery()

                    If afectados > 0 Then
                        MessageBox.Show("Datos del alumno eliminados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TextBox1.Clear()
                        CargarDatos()
                    Else
                        MessageBox.Show("Datos del alumno eliminados. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error de MySQL: " & ex.Message,
                        "Error de Base de Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message,
                            "Error General",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Debe ingresar el ID del alumno")
            Exit Sub
        End If

        Dim idBusca As Integer
        If Not Integer.TryParse(TextBox1.Text, idBusca) Then
            MessageBox.Show("El fomato no es valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim conexion2 As String =
            "Server=localhost;" &
            "Port=3307;" &
            "User ID=root;" &
            "Password=1234;" &
            "Database=Escuela;"

        Dim buscaAlumno As String = "SELECT IdAlumno, Nombre, Edad, Correo, Carrera, Universidad FROM Escuela.Alumnos WHERE IdAlumno = @idAlumno;"

        Try
            Using conecta2 As New MySqlConnection(conexion2)
                Using comando3 As New MySqlCommand(buscaAlumno, conecta2)
                    comando3.Parameters.AddWithValue("@idAlumno", idBusca)
                    Using adaptador As New MySqlDataAdapter(comando3)
                        Dim tablaResultado As New DataTable()
                        adaptador.Fill(tablaResultado)

                        If tablaResultado.Rows.Count > 0 Then
                            DataGridView1.DataSource = tablaResultado
                            TextBox1.Clear()
                        Else
                            MessageBox.Show("No se encontro ningún alumno con ese ID")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error de MySQL: " & ex.Message,
                        "Error de Base de Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error: " & ex.Message,
                            "Error General",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub
End Class