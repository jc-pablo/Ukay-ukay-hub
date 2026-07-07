Imports MySql.Data.MySqlClient

Public Class Form2
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardOverview()
    End Sub

    Public Sub LoadDashboardOverview()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT COUNT(*) FROM inventory"
            dbcomm = New MySqlCommand(sql, conn)
            Dim totalItems As Object = dbcomm.ExecuteScalar()
            Label23.Text = Val(totalItems.ToString()).ToString("N0")

            sql = "SELECT COUNT(*) FROM inventory WHERE status = 'Available'"
            dbcomm = New MySqlCommand(sql, conn)
            Dim availableItems As Object = dbcomm.ExecuteScalar()
            Label26.Text = Val(availableItems.ToString()).ToString("N0")

            sql = "SELECT COUNT(transaction_id) FROM transactions WHERE DATE(sale_date) = CURDATE()"
            dbcomm = New MySqlCommand(sql, conn)
            Dim soldToday As Object = dbcomm.ExecuteScalar()
            Label28.Text = Val(soldToday.ToString()).ToString("N0")

            sql = "SELECT SUM(selling_price) FROM transactions WHERE DATE(sale_date) = CURDATE()"
            dbcomm = New MySqlCommand(sql, conn)
            Dim revenueToday As Object = dbcomm.ExecuteScalar()

            If revenueToday Is Nothing OrElse revenueToday.ToString() = "" Then
                Label29.Text = "₱0.00 revenue"
            Else
                Dim totalRevenue As Decimal = Convert.ToDecimal(revenueToday)
                Label29.Text = "₱" & totalRevenue.ToString("N2") & " revenue"
            End If

            sql = "SELECT COUNT(*) FROM transactions WHERE payout_status = 'Unpaid'"
            dbcomm = New MySqlCommand(sql, conn)
            Dim pendingPayouts As Object = dbcomm.ExecuteScalar()
            Label31.Text = Val(pendingPayouts.ToString()).ToString("N0")

        Catch ex As Exception
            MsgBox("Error loading dashboard data: " & ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy hh:mm:ss tt")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim frm3 As New Form3()
        frm3.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim frm3 As New Form3()
        frm3.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblLogout.LinkClicked
        Dim frm1 As New Form1()
        frm1.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel9_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        Dim frm1 As New Form1()
        frm1.Show()
        Me.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim frm4 As New Form4()
        frm4.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim frm4 As New Form4()
        frm4.Show()
        Me.Hide()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Dim frm5 As New Form5()
        frm5.Show()
        Me.Hide()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Dim frm6 As New Form6()
        frm6.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Dim frm5 As New Form5()
        frm5.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Dim frm6 As New Form6()
        frm6.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        Dim frm7 As New Form7()
        frm7.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim frm7 As New Form7()
        frm7.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        Dim frm8 As New Form8()
        frm8.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim frm8 As New Form8()
        frm8.Show()
        Me.Hide()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub
End Class