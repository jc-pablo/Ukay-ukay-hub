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
                  "p.payout_period As 'Period', p.date_saved As 'Date Saved' " &
                  "FROM payouts p INNER JOIN consignors c ON p.consignor_id = c.consignor_id ORDER BY p.date_saved DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "payouts")
            dgvPayoutHistory.DataSource = ds
            dgvPayoutHistory.DataMember = "payouts"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
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
            Dim payoutId As String = "PAY-" & DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim period As String = dtpFrom.Value.ToString("MM/dd/yyyy") & " - " & dtpTo.Value.ToString("MM/dd/yyyy")

            sql = $"INSERT INTO payouts (payout_id, consignor_id, payout_period, date_saved) VALUES ('{payoutId}', '{cmbConsignor.SelectedValue.ToString()}', '{period}', CURDATE())"
            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery()

            If (i > 0) Then
                MsgBox("record saved")
            Else
                MsgBox("record not saved")
            End If

            For Each row As DataRow In dtItems.Rows
                Dim transId As String = row("transaction_id").ToString()
                sql = $"UPDATE transactions SET payout_status = 'Paid' WHERE transaction_id = '{transId}'"
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.ExecuteNonQuery()
            Next

            MsgBox("Payout records successfully processed and saved!")
            conn.Close()

            dgvItemsSold.DataSource = Nothing
            LoadPayoutHistory()
        Catch ex As Exception
            MsgBox(ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
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