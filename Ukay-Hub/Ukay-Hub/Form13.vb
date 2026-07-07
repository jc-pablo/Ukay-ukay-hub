Imports MySql.Data.MySqlClient

Public Class Form13
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = InputBox("Enter your Username:", "UkayHub Login")

        If String.IsNullOrWhiteSpace(username) Then
            MessageBox.Show("Username is required to log in.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim password As String = InputBox("Enter your Password:", "UkayHub Login")

        If String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Password is required to log in.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT COUNT(*) FROM customers WHERE username = @username AND password = @password"
            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@username", username.Trim())
            dbcomm.Parameters.AddWithValue("@password", password.Trim())

            Dim userExists As Integer = Convert.ToInt32(dbcomm.ExecuteScalar())

            If userExists > 0 Then
                MessageBox.Show("Login successful! Welcome to UkayHub.", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Form14.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid username or password! No matching record found.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Database Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm16 As New Form16()
        frm16.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm1 As New Form1()
        frm1.Show()
        Me.Hide()
    End Sub
End Class