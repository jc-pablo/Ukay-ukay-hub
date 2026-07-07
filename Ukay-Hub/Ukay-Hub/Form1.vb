Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            conn.Open()
            sql = $"SELECT * FROM users WHERE username = '{txtUsername.Text.Trim()}' AND password = '{txtPassword.Text.Trim()}'"

            dbcomm = New MySqlCommand(sql, conn)
            dbread = dbcomm.ExecuteReader()

            If dbread.Read() Then
                MsgBox("Login Successful! Welcome to UkayHub.")

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm13 As New Form13()
        frm13.Show()
        Me.Hide()
    End Sub
End Class
