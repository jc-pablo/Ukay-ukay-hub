Imports MySql.Data.MySqlClient
Public Class Form12
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConsignorEarningsLedger()
    End Sub

    Private Sub LoadConsignorEarningsLedger()
        Dim query As String = "SELECT c.full_name As 'Consignor', " &
                              "COUNT(t.transaction_id) As 'Item Sold', " &
                              "COALESCE(SUM(t.selling_price), 0) As 'Total Sales', " &
                              "CONCAT(c.commission_rate, '%') As 'Rate', " &
                              "COALESCE(SUM(t.consignor_share), 0) As 'Total Earned', " &
                              "COALESCE((SELECT p.date_saved FROM payouts p WHERE p.consignor_id = c.consignor_id ORDER BY p.date_saved DESC LIMIT 1), 'No Payouts Yet') As 'Last Payout' " &
                              "FROM consignors c " &
                              "LEFT JOIN inventory i ON c.consignor_id = i.consignor_id " &
                              "LEFT JOIN transactions t ON i.item_id = t.item_id AND i.status = 'Sold' " &
                              "GROUP BY c.consignor_id, c.full_name, c.commission_rate"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvConsignorEarningsReport.DataSource = table
                    dgvConsignorEarningsReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As Exception
                End Try
            End Using
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

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Dim frm11 As New Form11()
        frm11.Show()
        Me.Hide()
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Dim frm10 As New Form10()
        frm10.Show()
        Me.Hide()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub
End Class