Imports MySql.Data.MySqlClient
Public Class Form7
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"
    Dim itemTable As New DataTable()

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAvailableItems()
        LoadSalesHistoryGrid()
        dtpSaleDate.Value = DateTime.Now
    End Sub
    Private Sub LoadAvailableItems()
        Dim query As String = "SELECT i.item_id, CONCAT(i.item_name, ' (', c.category_name, ')') As item_display, " &
                              "i.price, i.source_type, COALESCE(con.commission_rate, 0) As rate " &
                              "FROM inventory i " &
                              "LEFT JOIN categories c ON i.category_id = c.category_id " &
                              "LEFT JOIN consignors con ON i.consignor_id = con.consignor_id " &
                              "WHERE i.status = 'Available' ORDER BY i.item_name ASC"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    itemTable.Clear()
                    adapter.Fill(itemTable)

                    cmbItemSelection.DataSource = itemTable
                    cmbItemSelection.DisplayMember = "item_display"
                    cmbItemSelection.ValueMember = "item_id"
                    cmbItemSelection.SelectedIndex = -1
                    ClearCalculatedLabels()
                Catch ex As MySqlException
                    MessageBox.Show("Error fetching inventory stock: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub cmbItemSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItemSelection.SelectedIndexChanged
        If cmbItemSelection.SelectedIndex <> -1 AndAlso itemTable.Rows.Count > 0 Then
            Dim selectedRow As DataRow = itemTable.Rows(cmbItemSelection.SelectedIndex)

            Dim listedPrice As Decimal = Convert.ToDecimal(selectedRow("price"))
            Dim source As String = selectedRow("source_type").ToString()
            Dim rate As Decimal = Convert.ToDecimal(selectedRow("rate"))

            Dim consignorCut As Decimal = 0
            If source = "Consigned" Then
                consignorCut = listedPrice * (rate / 100)
            End If
            Dim storeCut As Decimal = listedPrice - consignorCut

            lblListedPrice.Text = listedPrice.ToString("F2")
            lblSourceType.Text = source
            lblConsignorShare.Text = consignorCut.ToString("F2")
            lblShareRevenue.Text = storeCut.ToString("F2")
            txtSellingPrice.Text = listedPrice.ToString("F2")
        End If
    End Sub

    Private Sub btnRecordSale_Click(sender As Object, e As EventArgs) Handles btnRecordSale.Click
        If cmbItemSelection.SelectedIndex = -1 Then
            MessageBox.Show("Please pick an available item to finalize sale.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(cmbItemSelection.SelectedValue)
        Dim sellingPrice As Decimal = Convert.ToDecimal(txtSellingPrice.Text)
        Dim source As String = lblSourceType.Text
        Dim consignorShare As Decimal = Convert.ToDecimal(lblConsignorShare.Text)
        Dim storeShare As Decimal = sellingPrice - consignorShare
        Dim payoutStatus As String = If(source = "Consigned", "Unpaid", "N/A")

        Dim insertQuery As String = "INSERT INTO transactions (item_id, selling_price, consignor_share, store_share, sale_date, payout_status) " &
                                    "VALUES (@itemId, @sellPrice, @conShare, @storeShare, @saleDate, @status);" &
                                    "UPDATE inventory SET status = 'Sold' WHERE item_id = @itemId;"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(insertQuery, conn)
                cmd.Parameters.AddWithValue("@itemId", itemId)
                cmd.Parameters.AddWithValue("@sellPrice", sellingPrice)
                cmd.Parameters.AddWithValue("@conShare", consignorShare)
                cmd.Parameters.AddWithValue("@storeShare", storeShare)
                cmd.Parameters.AddWithValue("@saleDate", dtpSaleDate.Value)
                cmd.Parameters.AddWithValue("@status", payoutStatus)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Sale transaction logged securely!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ClearForm()
                    LoadAvailableItems()
                    LoadSalesHistoryGrid()
                Catch ex As MySqlException
                    MessageBox.Show("Error execution failed: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadSalesHistoryGrid()
        Dim query As String = "SELECT t.transaction_id As '#', i.item_name As 'Item', t.selling_price As 'Sale Price', " &
                              "i.source_type As 'Source', t.consignor_share As 'Consignor Share', t.sale_date As 'Date' " &
                              "FROM transactions t INNER JOIN inventory i ON t.item_id = i.item_id ORDER BY t.transaction_id DESC"
        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgvSalesHistory.DataSource = dt
                    dgvSalesHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As Exception
                End Try
            End Using
        End Using
    End Sub

    Private Sub ClearForm()
        cmbItemSelection.SelectedIndex = -1
        txtSellingPrice.Clear()
        ClearCalculatedLabels()
    End Sub

    Private Sub ClearCalculatedLabels()
        lblListedPrice.Text = "0.00"
        lblSourceType.Text = "---"
        lblConsignorShare.Text = "0.00"
        lblShareRevenue.Text = "0.00"
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