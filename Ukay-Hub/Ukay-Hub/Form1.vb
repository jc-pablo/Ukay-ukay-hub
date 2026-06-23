Imports MySql.Data.MySqlClient
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim isLoginValid As Boolean = AuthenticateUser(txtUsername.Text, txtPassword.Text)
        If isLoginValid Then
            Dim frm2 As New Form2()
            frm2.Show()
            Me.Hide()
        End If
    End Sub

    Private Function AuthenticateUser(username As String, password As String) As Boolean
        Dim isValid As Boolean = False
        Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"
        Dim query As String = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)

                Try
                    conn.Open()
                    Dim userCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If userCount > 0 Then
                        MessageBox.Show("Login Successful! Welcome to UkayHub.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        isValid = True
                    Else
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        isValid = False
                    End If

                Catch ex As MySqlException
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    isValid = False
                End Try
            End Using
        End Using

        Return isValid
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.PasswordChar = "*"c
    End Sub
End Class
