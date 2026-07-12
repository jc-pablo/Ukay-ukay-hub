Public Class Form13

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Form15.Show()
        Me.Hide()
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