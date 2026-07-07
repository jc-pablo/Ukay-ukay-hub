Imports MySql.Data.MySqlClient

Public Class Form9
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardMetrics()
        LoadDailySalesLog()
    End Sub

    Public Sub LoadDashboardMetrics()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT SUM(selling_price) FROM transactions WHERE DATE(sale_date) = CURDATE()"
            dbcomm = New MySqlCommand(sql, conn)
            Dim todayRes As Object = dbcomm.ExecuteScalar()
            lblTodaySales.Text = "₱" & Val(todayRes.ToString()).ToString("N2")

            sql = "SELECT COUNT(transaction_id) FROM transactions WHERE DATE(sale_date) = CURDATE()"
            dbcomm = New MySqlCommand(sql, conn)
            Dim todayCountRes As Object = dbcomm.ExecuteScalar()

            If todayCountRes Is Nothing OrElse todayCountRes.ToString() = "" Then
                Label24.Text = "0 items sold"
            Else
                Label24.Text = todayCountRes.ToString() & " items sold"
            End If

            sql = "SELECT SUM(selling_price) FROM transactions WHERE YEARWEEK(sale_date, 1) = YEARWEEK(CURDATE(), 1)"
            dbcomm = New MySqlCommand(sql, conn)
            Dim weekRes As Object = dbcomm.ExecuteScalar()
            lblThisWeekSales.Text = "₱" & Val(weekRes.ToString()).ToString("N2")

            sql = "SELECT COUNT(transaction_id) FROM transactions WHERE YEARWEEK(sale_date, 1) = YEARWEEK(CURDATE(), 1)"
            dbcomm = New MySqlCommand(sql, conn)
            Dim weekCountRes As Object = dbcomm.ExecuteScalar()

            If weekCountRes Is Nothing OrElse weekCountRes.ToString() = "" Then
                Label27.Text = "0 items sold"
            Else
                Label27.Text = weekCountRes.ToString() & " items sold"
            End If

            sql = "SELECT AVG(daily_total) FROM (SELECT SUM(selling_price) As daily_total FROM transactions GROUP BY DATE(sale_date)) As temp"
            dbcomm = New MySqlCommand(sql, conn)
            Dim avgRes As Object = dbcomm.ExecuteScalar()
            lblAvgPerDay.Text = "₱" & Val(avgRes.ToString()).ToString("N2")

            sql = "SELECT DATE_FORMAT(MAX(sale_date), '%W') FROM transactions GROUP BY DATE(sale_date) ORDER BY SUM(selling_price) DESC LIMIT 1"
            dbcomm = New MySqlCommand(sql, conn)
            Dim bestDay As Object = dbcomm.ExecuteScalar()

            sql = "SELECT MAX(daily_total) FROM (SELECT SUM(selling_price) as daily_total FROM transactions GROUP BY DATE(sale_date)) as temp"
            dbcomm = New MySqlCommand(sql, conn)
            Dim bestDayTotal As Object = dbcomm.ExecuteScalar()

            If bestDay Is Nothing OrElse bestDay.ToString() = "" Then
                lblBestDay.Text = "No Sales"
                Label33.Text = "₱0.00"
            Else
                lblBestDay.Text = bestDay.ToString()
                Label33.Text = "₱" & Val(bestDayTotal.ToString()).ToString("N2")
            End If
            dgvDailySalesLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Public Sub LoadDailySalesLog()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT DATE(t.sale_date) As 'Date', COUNT(t.transaction_id) As 'Items Sold', SUM(t.selling_price) As 'Total Sales' " &
                  "FROM transactions t GROUP BY DATE(t.sale_date) ORDER BY DATE(t.sale_date) DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "DailyLog")
            dgvDailySalesLog.DataSource = ds
            dgvDailySalesLog.DataMember = "DailyLog"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm1 As New Form1()
        frm1.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim frm2 As New Form2()
        frm2.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim frm3 As New Form3()
        frm3.Show()
        Me.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
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

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim frm7 As New Form7()
        frm7.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim frm8 As New Form8()
        frm8.Show()
        Me.Hide()
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Dim frm10 As New Form10()
        frm10.Show()
        Me.Hide()
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Dim frm11 As New Form11()
        frm11.Show()
        Me.Hide()
    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click
        Dim frm12 As New Form12()
        frm12.Show()
        Me.Hide()
    End Sub


End Class