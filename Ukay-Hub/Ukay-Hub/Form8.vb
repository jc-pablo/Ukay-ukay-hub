Imports MySql.Data.MySqlClient

Public Class Form8
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"
    Dim unpaidItemsTable As New DataTable()

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConsignorsDropdown()
        LoadPayoutHistoryGrid()
    End Sub

    Private Sub LoadConsignorsDropdown()
        Dim query As String = "SELECT consignor_id, full_name FROM consignors ORDER BY full_name ASC"
        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    cmbConsignors.DataSource = dt
                    cmbConsignors.DisplayMember = "full_name"
                    cmbConsignors.ValueMember = "consignor_id"
                    cmbConsignors.SelectedIndex = -1
                Catch ex As Exception
                End Try
            End Using
        End Using
    End Sub
    Private Sub btnCompute_Click(sender As Object, e As EventArgs) Handles btnCompute.Click
        If cmbConsignors.SelectedIndex = -1 Then
            MessageBox.Show("Please select a consignor first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim consignorId As Integer = Convert.ToInt32(cmbConsignors.SelectedValue)
        Dim dateFrom As String = dtpFrom.Value.ToString("yyyy-MM-dd")
        Dim dateTo As String = dtpTo.Value.ToString("yyyy-MM-dd")

        Dim query As String = "SELECT t.transaction_id As '#', i.item_name As 'Item', t.selling_price As 'Sale Price', " &
                          "t.consignor_share As 'Commission', t.payout_status As 'Status', t.sale_date As 'Date' " &
                          "FROM transactions t " &
                          "INNER JOIN inventory i ON t.item_id = i.item_id " &
                          "WHERE i.consignor_id = @consignorId AND i.status = 'Sold' " &
                          "AND t.sale_date BETWEEN @from AND @to"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@consignorId", consignorId)
                cmd.Parameters.AddWithValue("@from", dateFrom)
                cmd.Parameters.AddWithValue("@to", dateTo)

                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    unpaidItemsTable.Clear()
                    adapter.Fill(unpaidItemsTable)
                    dgvUnpaidItems.DataSource = unpaidItemsTable
                    dgvUnpaidItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    Dim totalSales As Decimal = 0
                    Dim totalPayout As Decimal = 0
                    Dim unpaidItemCount As Integer = 0

                    For Each row As DataRow In unpaidItemsTable.Rows
                        If row("Status").ToString() = "Unpaid" Then
                            totalSales += Convert.ToDecimal(row("Sale Price"))
                            totalPayout += Convert.ToDecimal(row("Commission"))
                            unpaidItemCount += 1
                        End If
                    Next
                    Dim rateQuery As String = "SELECT commission_rate FROM consignors WHERE consignor_id = @id"
                    Dim rateCmd As New MySqlCommand(rateQuery, conn)
                    rateCmd.Parameters.AddWithValue("@id", consignorId)
                    conn.Open()
                    Dim currentRate As String = rateCmd.ExecuteScalar().ToString()

                    lblRcptNameDate.Text = cmbConsignors.Text & " -- " & DateTime.Now.ToString("MM/dd/yy")
                    lblRcptItemsSold.Text = unpaidItemCount.ToString()
                    lblRcptTotalSales.Text = totalSales.ToString("F2")
                    lblRcptCommRate.Text = currentRate & "%"
                    lblRcptTotalPayout.Text = totalPayout.ToString("F2")

                Catch ex As MySqlException
                    MessageBox.Show("Computation anomaly: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub btnSavePayoutRecord_Click(sender As Object, e As EventArgs) Handles btnSavePayoutRecord.Click
        If unpaidItemsTable.Rows.Count = 0 Then
            MessageBox.Show("No computed items found to execute payment.", "System Halt", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim consignorId As Integer = Convert.ToInt32(cmbConsignors.SelectedValue)
        Dim periodString As String = dtpFrom.Value.ToString("MM/dd/yy") & " to " & dtpTo.Value.ToString("MM/dd/yy")
        Dim count As Integer = Convert.ToInt32(lblRcptItemsSold.Text)
        Dim salesVal As Decimal = Convert.ToDecimal(lblRcptTotalSales.Text)
        Dim payoutVal As Decimal = Convert.ToDecimal(lblRcptTotalPayout.Text)

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim logQuery As String = "INSERT INTO payouts_history (consignor_id, payout_period, items_sold_count, total_sales_value, payout_amount) " &
                                         "VALUES (@conId, @period, @count, @sales, @payout)"
                Using cmdLog As New MySqlCommand(logQuery, conn)
                    cmdLog.Parameters.AddWithValue("@conId", consignorId)
                    cmdLog.Parameters.AddWithValue("@period", periodString)
                    cmdLog.Parameters.AddWithValue("@count", count)
                    cmdLog.Parameters.AddWithValue("@sales", salesVal)
                    cmdLog.Parameters.AddWithValue("@payout", payoutVal)
                    cmdLog.ExecuteNonQuery()
                End Using

                For Each row As DataRow In unpaidItemsTable.Rows
                    If row("Status").ToString() = "Unpaid" Then
                        Dim transId As Integer = Convert.ToInt32(row("#"))
                        Dim updateQuery As String = "UPDATE transactions SET payout_status = 'Paid' WHERE transaction_id = " & transId
                        Using cmdUpdate As New MySqlCommand(updateQuery, conn)
                            cmdUpdate.ExecuteNonQuery()
                        End Using
                    End If
                Next

                MessageBox.Show("Payout settled completely and flagged in system archive!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                LoadPayoutHistoryGrid()

            Catch ex As MySqlException
                MessageBox.Show("Error sealing payout session: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadPayoutHistoryGrid()
        Dim query As String = "SELECT p.payout_id As '#', c.full_name As 'Consignor', p.payout_period As 'Period', " &
                              "p.items_sold_count As 'Items', p.total_sales_value As 'Total Sales', " &
                              "p.payout_amount As 'Payout', p.date_saved As 'Date Saved' " &
                              "FROM payouts p INNER JOIN consignors c ON ph.consignor_id = c.consignor_id ORDER BY p.payout_id DESC"
        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvPayoutHistory.DataSource = dt
                    dgvPayoutHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As Exception
                End Try
            End Using
        End Using
    End Sub

    Private Sub ClearForm()
        cmbConsignors.SelectedIndex = -1
        unpaidItemsTable.Clear()
        dgvUnpaidItems.DataSource = Nothing
        lblRcptNameDate.Text = "Name -- Date"
        lblRcptItemsSold.Text = "0"
        lblRcptTotalSales.Text = "0"
        lblRcptCommRate.Text = "0%"
        lblRcptTotalPayout.Text = "000"
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
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

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub
End Class