Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader

    ' LOGIN BUTTON (Patterned after Form1 Button1_Click / Button1_Click ng prof mo)
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            conn.Open()
            ' Ginamit ang string interpolation at tinanggal ang @ parameters para sa istilo ng prof mo
            sql = $"SELECT * FROM users WHERE username = '{txtUsername.Text.Trim()}' AND password = '{txtPassword.Text.Trim()}'"

            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            ' Tinitingnan kung may nahanap na row gamit ang dbread.Read()
            If dbread.Read() Then
                MsgBox("Login Successful! Welcome to UkayHub.")

                ' Kailangang isara ang reader at connection bago magpakita ng bagong form
                dbread.Close()
                conn.Close()

                Dim frm2 As New Form2()
                frm2.Show()
                Me.Hide()
            Else
                MsgBox("Invalid username or password.")
                dbread.Close()
                conn.Close()
            End If

        Catch ex As MySqlException
            MsgBox(ex.Message)
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.PasswordChar = "*"c
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Frm13 As New Form13()
        Frm13.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Frm3 As New Form3()
        Frm3.Show()
        Me.Hide()
    End Sub
End Class
