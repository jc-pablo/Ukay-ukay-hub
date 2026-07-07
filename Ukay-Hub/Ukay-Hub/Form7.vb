Imports MySql.Data.MySqlClient

Public Class Form7
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAvailableItems()
        LoadTransactionHistory()
    End Sub

    Private Sub LoadAvailableItems()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT item_id, item_name, price, source_type FROM inventory WHERE status = 'Available'"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "available_items")

            cmbItem.DataSource = ds.Tables("available_items")
            cmbItem.DisplayMember = "item_name"
            cmbItem.ValueMember = "item_id"
            cmbItem.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub LoadTransactionHistory()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT t.transaction_id As 'Transaction ID', i.item_name As 'Item', t.selling_price As 'Sold Price', t.sale_date As 'Date' " &
                  "FROM transactions t INNER JOIN inventory i ON t.item_id = i.item_id ORDER BY t.sale_date DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "history")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "history"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim selectedTransId As String = row.Cells("Transaction ID").Value.ToString()

            Try
                If conn.State = ConnectionState.Closed Then conn.Open()
                sql = $"SELECT t.transaction_id, t.item_id, t.selling_price, t.sale_date FROM transactions t WHERE t.transaction_id = '{selectedTransId}'"
                dbcomm = New MySqlCommand(sql, conn)
                dbread = dbcomm.ExecuteReader()

                If dbread.Read() Then
                    Dim transId As String = dbread("transaction_id").ToString()
                    Dim sellPrice As String = Convert.ToDecimal(dbread("selling_price")).ToString("F2")
                    Dim saleDate As DateTime = Convert.ToDateTime(dbread("sale_date"))
                    Dim itemId As String = dbread("item_id").ToString()

                    dbread.Close()

                    txtTransactionId.Text = transId
                    txtSellingPrice.Text = sellPrice
                    dtpSaleDate.Value = saleDate
                    cmbItem.SelectedValue = itemId
                Else
                    dbread.Close()
                End If
            Catch ex As Exception
                MsgBox("Error selecting record: " & ex.Message)
                conn.Close()
            End Try
            conn.Close()
        End If
    End Sub

    Private Sub btnRecordSale_Click(sender As Object, e As EventArgs) Handles btnRecordSale.Click
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = $"INSERT INTO transactions (transaction_id, item_id, selling_price, sale_date, payout_status) " &
                  $"VALUES('{txtTransactionId.Text.Trim()}', '{cmbItem.SelectedValue}', {Val(txtSellingPrice.Text)}, '{dtpSaleDate.Value.ToString("yyyy-MM-dd")}', 'Unpaid'); " &
                  $"UPDATE inventory SET status = 'Sold' WHERE item_id = '{cmbItem.SelectedValue}'"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery()

            If (i > 0) Then
                MsgBox("record saved")
            Else
                MsgBox("record not saved")
            End If
        Catch ex As MySqlException
            If ex.Number = 1062 Then
                MsgBox("Transaction ID already exists!")
            Else
                MsgBox(ex.Message)
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()

        txtTransactionId.Clear()
        txtSellingPrice.Clear()
        cmbItem.SelectedIndex = -1
        LoadAvailableItems()
        LoadTransactionHistory()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = $"UPDATE transactions SET item_id = '{cmbItem.SelectedValue}', selling_price = {Val(txtSellingPrice.Text)}, sale_date = '{dtpSaleDate.Value.ToString("yyyy-MM-dd")}' " &
                  $"WHERE transaction_id = '{txtTransactionId.Text.Trim()}'"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery()

            If (i > 0) Then
                MsgBox("record saved")
            Else
                MsgBox("record not saved")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()

        txtTransactionId.Clear()
        txtSellingPrice.Clear()
        cmbItem.SelectedIndex = -1
        LoadAvailableItems()
        LoadTransactionHistory()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim ans = MessageBox.Show("Do you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
        If ans = DialogResult.Yes Then
            Try
                If conn.State = ConnectionState.Closed Then conn.Open()
                sql = $"UPDATE inventory SET status = 'Available' WHERE item_id = '{cmbItem.SelectedValue}'; " &
                      $"DELETE FROM transactions WHERE transaction_id = '{txtTransactionId.Text.Trim()}'"

                dbcomm = New MySqlCommand(sql, conn)
                Dim i As Integer = dbcomm.ExecuteNonQuery()

                If (i > 0) Then
                    MsgBox("Record deleted and item reverted to Available!")
                Else
                    MsgBox("No record was deleted.")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try
            conn.Close()

            txtTransactionId.Clear()
            txtSellingPrice.Clear()
            cmbItem.SelectedIndex = -1
            LoadAvailableItems()
            LoadTransactionHistory()
        End If
    End Sub

    Private Sub cmbItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItem.SelectedIndexChanged
        If cmbItem.SelectedIndex <> -1 AndAlso TypeOf cmbItem.SelectedItem Is DataRowView Then
            Dim row As DataRowView = CType(cmbItem.SelectedItem, DataRowView)
            Dim listedPrice As Decimal = Convert.ToDecimal(row("price"))
            Dim source As String = row("source_type").ToString()

            lblListedPrice.Text = listedPrice.ToString("F2")
            lblSource.Text = source
            txtSellingPrice.Text = listedPrice.ToString("F2")

            Dim consignorShare As Decimal = 0
            Dim storeRevenue As Decimal = 0

            If source = "Consigned" Then
                consignorShare = listedPrice * 0.7D
                storeRevenue = listedPrice * 0.3D
            ElseIf source = "Donated" Then
                consignorShare = 0
                storeRevenue = listedPrice
            End If

            lblConsignorShare.Text = consignorShare.ToString("F2")
            lblShareRevenue.Text = storeRevenue.ToString("F2")
        End If
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        LoadTransactionHistory()
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

End Class