Imports MySqlConnector

Public Class Form1
    ' 1. Cargar los datos automáticamente al abrir el formulario
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDatos()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim miFormulario As New Form2()
        miFormulario.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox5.Text) Then
            MessageBox.Show("Por favor, completa todos los campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Validar que la edad sea un número entero válido
        Dim edadValida As Integer
        If Not Integer.TryParse(TextBox2.Text, edadValida) Then
            MessageBox.Show("Ingrese un número entero válido para la edad.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Conexión con la base de datos (corregido User ID)
        Dim conectaConexion As String =
            "Server=localhost;" &
            "Port=3307;" &
            "User ID=root;" &
            "Password=1234;" &
            "Database=Escuela;"

        Dim insertarAlumnos As String =
            "INSERT INTO Escuela.Alumnos (Nombre, Edad, Correo, Carrera, Universidad) " &
            "VALUES (@Nombre, @Edad, @Correo, @Carrera, @Universidad);"

        Try
            Using conexion As New MySqlConnection(conectaConexion)
                conexion.Open()

                Using comando As New MySqlCommand(insertarAlumnos, conexion)
                    comando.Parameters.AddWithValue("@Nombre", TextBox1.Text.Trim())
                    comando.Parameters.AddWithValue("@Edad", edadValida)
                    comando.Parameters.AddWithValue("@Correo", TextBox3.Text.Trim())
                    comando.Parameters.AddWithValue("@Carrera", TextBox4.Text.Trim())
                    comando.Parameters.AddWithValue("@Universidad", TextBox5.Text.Trim())

                    Dim filasAfectadas As Integer = comando.ExecuteNonQuery()

                    If filasAfectadas > 0 Then
                        ' Corregidas las comas en lugar de &
                        MessageBox.Show("¡Alumno registrado correctamente!",
                                        "Éxito",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)

                        ' Limpiar los textos después de guardar
                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()
                        TextBox5.Clear()

                        ' 2. Actualizar el DataGridView con el nuevo alumno
                        CargarDatos()
                    End If
                End Using
            End Using

            ' Corregido: MySqlException primero, Exception después
        Catch ex As MySqlException
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
End Class