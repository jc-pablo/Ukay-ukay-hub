Imports MySql.Data.MySqlClient

Public Class Form15
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            conn.Open()
            sql = "SELECT * FROM customers WHERE username = @username AND password = @password"

            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@username", txtUsername.Text.Trim())
            dbcomm.Parameters.AddWithValue("@password", txtPassword.Text.Trim())

            dbread = dbcomm.ExecuteReader()

            If dbread.Read() Then
                MsgBox("Login Successful! Welcome to UkayHub.")

                dbread.Close()
                conn.Close()

                Dim frm14 As New Form14()
                frm14.Show()
                Me.Hide()
            Else
                MsgBox("Invalid username or password.")
                dbread.Close()
                conn.Close()
            End If

        Catch ex As MySqlException
            MsgBox("Database error: " & ex.Message)
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.PasswordChar = "*"c
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm13 As New Form13()
        frm13.Show()
        Me.Hide()
    End Sub
End Class