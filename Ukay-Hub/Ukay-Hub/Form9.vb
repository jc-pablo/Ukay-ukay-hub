Imports MySql.Data.MySqlClient

Public Class Form9
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardMetrics()
        LoadDailySalesLog()
    End Sub
    Private Sub LoadDailySalesLog()
        Dim query As String = "SELECT DISTINCT t.sale_date As 'Date', " &
                              "DAYNAME(t.sale_date) As 'Day', " &
                              "COUNT(t.transaction_id) As 'Item Sold', " &
                              "SUM(t.selling_price) As 'Revenue', " &
                              "SUM(t.consignor_share) As 'Consignor Payable' " &
                              "FROM transactions t " &
                              "GROUP BY t.sale_date " &
                              "ORDER BY t.sale_date DESC"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvDailySalesLog.DataSource = table
                    dgvDailySalesLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As Exception
                End Try
            End Using
        End Using
    End Sub



    Private Sub LoadDashboardMetrics()
        Dim qToday As String = "SELECT COALESCE(SUM(selling_price), 0) FROM transactions WHERE sale_date = CURDATE()"
        Dim qWeek As String = "SELECT COALESCE(SUM(selling_price), 0) FROM transactions WHERE sale_date >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)"
        Dim qAvg As String = "SELECT COALESCE(AVG(daily_total), 0) FROM (SELECT SUM(selling_price) AS daily_total FROM transactions GROUP BY sale_date) AS daily_summary"
        Dim qBestDay As String = "SELECT COALESCE(DAYNAME(sale_date), 'None') FROM transactions GROUP BY sale_date ORDER BY SUM(selling_price) DESC LIMIT 1"

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                Using cmd As New MySqlCommand(qToday, conn)
                    lblTdaySales.Text = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("P#,##0.00") ' Today's Sales Card
                End Using

                Using cmd As New MySqlCommand(qWeek, conn)
                    lblWeekSale.Text = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("P#,##0.00")
                End Using

                Using cmd As New MySqlCommand(qAvg, conn)
                    lblAVGDay.Text = Convert.ToDecimal(cmd.ExecuteScalar()).ToString("P#,##0.00")
                End Using

                Using cmd As New MySqlCommand(qBestDay, conn)
                    lblBestDay.Text = cmd.ExecuteScalar().ToString()
                End Using

            Catch ex As MySqlException
                MessageBox.Show("Error displaying cards: " & ex.Message, "Metrics Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
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