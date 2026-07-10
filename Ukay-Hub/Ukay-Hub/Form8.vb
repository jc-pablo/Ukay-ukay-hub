Imports MySql.Data.MySqlClient

Public Class Form8
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet
    Dim dtItems As DataTable

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConsignors()
        LoadPayoutHistory()
    End Sub

    Private Sub LoadConsignors()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT consignor_id, CONCAT(first_name, ' ', last_name) As c_name FROM consignors"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "consignors")

            cmbConsignor.DataSource = ds.Tables("consignors")
            cmbConsignor.DisplayMember = "c_name"
            cmbConsignor.ValueMember = "consignor_id"
            cmbConsignor.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub LoadPayoutHistory()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT p.payout_id As '#', CONCAT(c.first_name, ' ', c.last_name) As 'Consignor', " &
              "p.payout_period As 'Period', p.date_saved As 'Date Saved', p.consignor_id " &
              "FROM payouts p INNER JOIN consignors c ON p.consignor_id = c.consignor_id ORDER BY p.date_saved DESC"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dtHistory As New DataTable()
            adapter.Fill(dtHistory)
            dtHistory.Columns.Add("Items", GetType(Integer))
            dtHistory.Columns.Add("Total Sales", GetType(Decimal))
            dtHistory.Columns.Add("Payout", GetType(Decimal))

            For Each row As DataRow In dtHistory.Rows
                Dim consignorId As String = row("consignor_id").ToString()
                Dim period As String = row("Period").ToString()

                Dim dates() As String = period.Split(New String() {" - "}, StringSplitOptions.None)
                Dim dateFrom As String = DateTime.ParseExact(dates(0), "MM/dd/yyyy", Nothing).ToString("yyyy-MM-dd")
                Dim dateTo As String = DateTime.ParseExact(dates(1), "MM/dd/yyyy", Nothing).ToString("yyyy-MM-dd")

                Dim itemSql As String = "SELECT t.selling_price FROM transactions t " &
                                   "INNER JOIN inventory i ON t.item_id = i.item_id " &
                                   "WHERE i.consignor_id = @consignorId AND t.payout_status = 'Paid' " &
                                   "AND t.sale_date BETWEEN @dateFrom AND @dateTo"

                Dim itemsCount As Integer = 0
                Dim totalSales As Decimal = 0

                Using cmdItems As New MySqlCommand(itemSql, conn)
                    cmdItems.Parameters.AddWithValue("@consignorId", consignorId)
                    cmdItems.Parameters.AddWithValue("@dateFrom", dateFrom)
                    cmdItems.Parameters.AddWithValue("@dateTo", dateTo)

                    Using reader As MySqlDataReader = cmdItems.ExecuteReader()
                        While reader.Read()
                            itemsCount += 1
                            totalSales += Convert.ToDecimal(reader("selling_price"))
                        End While
                    End Using
                End Using

                Dim payoutAmount As Decimal = totalSales * 0.7D

                row("Items") = itemsCount
                row("Total Sales") = totalSales
                row("Payout") = payoutAmount

            Next
            dgvPayoutHistory.DataSource = dtHistory


        Catch ex As Exception

        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnCompute_Click(sender As Object, e As EventArgs) Handles btnCompute.Click
        If cmbConsignor.SelectedIndex = -1 Then
            MsgBox("Please select a consignor first.")
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT t.transaction_id, i.item_name As 'Item', t.selling_price As 'Sale Price', t.sale_date As 'Date' " &
                  "FROM transactions t " &
                  "INNER JOIN inventory i ON t.item_id = i.item_id " &
                  $"WHERE i.consignor_id = '{cmbConsignor.SelectedValue.ToString()}' AND t.payout_status = 'Unpaid' " &
                  $"AND t.sale_date BETWEEN '{dtpFrom.Value.ToString("yyyy-MM-dd")}' AND '{dtpTo.Value.ToString("yyyy-MM-dd")}'"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "items_sold")

            dtItems = ds.Tables("items_sold")
            dtItems.Columns.Add("Commission", GetType(Decimal))

            Dim itemsCount As Integer = 0
            Dim totalSales As Decimal = 0
            Dim totalPayout As Decimal = 0

            For Each row As DataRow In dtItems.Rows
                Dim salePrice As Decimal = Convert.ToDecimal(row("Sale Price"))
                Dim consignorShare As Decimal = salePrice * 0.7D

                row("Commission") = consignorShare

                itemsCount += 1
                totalSales += salePrice
                totalPayout += consignorShare
            Next

            dgvItemsSold.DataSource = dtItems
            lblSummaryNameDate.Text = cmbConsignor.Text & " -- " & DateTime.Now.ToString("MM/dd/yyyy")
            lblItemsSold.Text = itemsCount.ToString()
            lblTotalSales.Text = totalSales.ToString("F2")
            lblCommissionRate.Text = "70%"
            lblTotalPayout.Text = totalPayout.ToString("F2")

        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub btnSavePayout_Click(sender As Object, e As EventArgs) Handles btnSavePayout.Click
        If dtItems Is Nothing OrElse dtItems.Rows.Count = 0 Then
            MsgBox("No earnings computed to save.")
            Exit Sub
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim payoutId As String = DateTime.Now.ToString("MMddHHmmss")
            Dim period As String = dtpFrom.Value.ToString("MM/dd/yyyy") & " - " & dtpTo.Value.ToString("MM/dd/yyyy")

            sql = "INSERT INTO payouts (payout_id, consignor_id, payout_period, date_saved) " &
              "VALUES (@payoutId, @consignorId, @period, CURDATE())"

            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@payoutId", payoutId)
            dbcomm.Parameters.AddWithValue("@consignorId", cmbConsignor.SelectedValue.ToString())
            dbcomm.Parameters.AddWithValue("@period", period)

            Dim i As Integer = dbcomm.ExecuteNonQuery()
            For Each row As DataRow In dtItems.Rows
                Dim transId As String = row("transaction_id").ToString()
                sql = "UPDATE transactions SET payout_status = 'Paid' WHERE transaction_id = @transId"

                Using dbcommUpdate As New MySqlCommand(sql, conn)
                    dbcommUpdate.Parameters.AddWithValue("@transId", transId)
                    dbcommUpdate.ExecuteNonQuery()
                End Using
            Next

            MsgBox("Payout records successfully processed and saved!")

            dgvItemsSold.DataSource = Nothing
            LoadPayoutHistory()

        Catch ex As Exception
            MsgBox("Error saving payout: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvPayoutHistory.SelectedRows.Count = 0 Then
            MsgBox("Please select a payout record from the history grid to delete.")
            Exit Sub
        End If

        Dim dialogResult As DialogResult = MessageBox.Show("Are you sure you want to delete this payout record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If dialogResult = DialogResult.Yes Then
            Try
                If conn.State = ConnectionState.Closed Then conn.Open()

                Dim selectedPayoutId As String = ""
                If dgvPayoutHistory.Columns.Contains("#") Then
                    selectedPayoutId = dgvPayoutHistory.SelectedRows(0).Cells("#").Value.ToString()
                ElseIf dgvPayoutHistory.Columns.Contains("payout_id") Then
                    selectedPayoutId = dgvPayoutHistory.SelectedRows(0).Cells("payout_id").Value.ToString()
                Else
                    selectedPayoutId = dgvPayoutHistory.SelectedRows(0).Cells(0).Value.ToString()
                End If

                sql = "DELETE FROM payouts WHERE payout_id = @payoutId"
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.Parameters.AddWithValue("@payoutId", selectedPayoutId)

                Dim i As Integer = dbcomm.ExecuteNonQuery()

                If i > 0 Then
                    MsgBox("Payout record successfully deleted.")
                    LoadPayoutHistory()
                Else
                    MsgBox("Record could not be found or deleted.")
                End If

            Catch ex As Exception
                MsgBox("Error deleting record: " & ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then conn.Close()
            End Try
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        cmbConsignor.SelectedIndex = -1
        dtpFrom.Value = DateTime.Now
        dtpTo.Value = DateTime.Now

        dgvItemsSold.DataSource = Nothing
        If dtItems IsNot Nothing Then dtItems.Clear()

        lblSummaryNameDate.Text = "Name -- Date"
        lblItemsSold.Text = "0"
        lblTotalSales.Text = "0.00"
        lblCommissionRate.Text = "0%"
        lblTotalPayout.Text = "0.00"
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