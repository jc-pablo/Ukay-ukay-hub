Imports MySql.Data.MySqlClient

Public Class Form16
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
          String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
          String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
          String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
          String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
          String.IsNullOrWhiteSpace(TextBox6.Text) Then

            MessageBox.Show("Please fill out all required fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "INSERT INTO customers (full_name, username, email, contact_no, address, password) " &
                  "VALUES (@fullname, @username, @email, @contact, @address, @password)"

            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@fullname", TextBox1.Text.Trim())
            dbcomm.Parameters.AddWithValue("@username", TextBox2.Text.Trim())
            dbcomm.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
            dbcomm.Parameters.AddWithValue("@contact", TextBox4.Text.Trim())
            dbcomm.Parameters.AddWithValue("@address", TextBox5.Text.Trim())
            dbcomm.Parameters.AddWithValue("@password", TextBox6.Text.Trim())

            dbcomm.ExecuteNonQuery()

            MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Form13.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Username might already be taken or database error: " & ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm13 As New Form13()
        frm13.Show()
        Me.Hide()
    End Sub
End Class