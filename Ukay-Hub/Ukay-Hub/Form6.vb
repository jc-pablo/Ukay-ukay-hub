Imports MySql.Data.MySqlClient
Public Class Form6
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"
    Dim selectedSourceType As String = ""
    Dim currentSelectedItemId As Integer = 0
    Private Sub LoadInventoryGrid()
        Dim query As String = "SELECT i.item_id As '#', i.item_name As 'Item Name', c.category_name As 'Category', " &
                          "i.item_condition As 'COND.', i.source_type As 'Source', " &
                          "COALESCE(d.full_name, con.full_name, 'No Supplier') As 'Supplier', " &
                          "i.price As 'Price', i.status As 'Status' " &
                          "FROM inventory i " &
                          "LEFT JOIN categories c ON i.category_id = c.category_id " &
                          "LEFT JOIN donors d ON i.donor_id = d.donor_id " &
                          "LEFT JOIN consignors con ON i.consignor_id = con.consignor_id"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvInventory.Columns.Clear()
                    dgvInventory.AutoGenerateColumns = True
                    dgvInventory.DataSource = table
                    dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As MySqlException
                    MessageBox.Show("Error loading inventory grid data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub LoadCategoryComboBox()
        Dim query As String = "SELECT category_id, category_name FROM categories"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    cmbCategory.DataSource = table
                    cmbCategory.DisplayMember = "category_name"
                    cmbCategory.ValueMember = "category_id"
                    cmbCategory.SelectedIndex = -1
                Catch ex As MySqlException
                    MessageBox.Show("Error loading categories to dropdown: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub radDonated_CheckedChanged(sender As Object, e As EventArgs) Handles radDonated.CheckedChanged
        If radDonated.Checked Then
            selectedSourceType = "Donated"
            LoadDonorsIntoComboBox()
        End If
    End Sub

    Private Sub radConsigned_CheckedChanged(sender As Object, e As EventArgs) Handles radConsigned.CheckedChanged
        If radConsigned.Checked Then
            selectedSourceType = "Consigned"
            LoadConsignorsIntoComboBox()
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtItemName.Text) OrElse cmbCategory.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtPrice.Text) Then
            MessageBox.Show("Please fill out the Item Name, Category, and Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim price As Decimal
        Decimal.TryParse(txtPrice.Text, price)

        Dim query As String = ""

        If currentSelectedItemId = 0 Then
            query = "INSERT INTO inventory (item_name, description, category_id, item_condition, source_type, price, status, donor_id, consignor_id) " &
                "VALUES (@name, @desc, @catId, @cond, @source, @price, @status, @donorId, @consignorId)"
        Else
            query = "UPDATE inventory SET item_name=@name, description=@desc, category_id=@catId, item_condition=@cond, " &
                "source_type=@source, price=@price, status=@status, donor_id=@donorId, consignor_id=@consignorId " &
                "WHERE item_id=@itemId"
        End If

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", txtItemName.Text.Trim())
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim())
                cmd.Parameters.AddWithValue("@catId", cmbCategory.SelectedValue)
                cmd.Parameters.AddWithValue("@cond", If(cmbCondition.SelectedIndex <> -1, cmbCondition.SelectedItem.ToString(), DBNull.Value))
                cmd.Parameters.AddWithValue("@source", If(selectedSourceType <> "", selectedSourceType, DBNull.Value))
                cmd.Parameters.AddWithValue("@price", price)
                cmd.Parameters.AddWithValue("@status", If(cmbStatus.SelectedIndex <> -1, cmbStatus.SelectedItem.ToString(), "Available"))
                If selectedSourceType = "Donated" AndAlso cmbSupplier.SelectedIndex <> -1 Then
                    cmd.Parameters.AddWithValue("@donorId", cmbSupplier.SelectedValue)
                    cmd.Parameters.AddWithValue("@consignorId", DBNull.Value)
                ElseIf selectedSourceType = "Consigned" AndAlso cmbSupplier.SelectedIndex <> -1 Then
                    cmd.Parameters.AddWithValue("@donorId", DBNull.Value)
                    cmd.Parameters.AddWithValue("@consignorId", cmbSupplier.SelectedValue)
                Else
                    cmd.Parameters.AddWithValue("@donorId", DBNull.Value)
                    cmd.Parameters.AddWithValue("@consignorId", DBNull.Value)
                End If

                If currentSelectedItemId <> 0 Then
                    cmd.Parameters.AddWithValue("@itemId", currentSelectedItemId)
                End If

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Inventory record processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ClearForm()
                    LoadInventoryGrid()
                Catch ex As MySqlException
                    MessageBox.Show("Error saving inventory changes: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub


    Private Sub ClearForm()
        txtItemName.Clear()
        txtDescription.Clear()
        cmbCategory.SelectedIndex = -1
        cmbCondition.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1
        txtPrice.Clear()
        selectedSourceType = ""
        cmbSupplier.DataSource = Nothing

        radDonated.Checked = False
        radConsigned.Checked = False
        currentSelectedItemId = 0
        btnSave.Text = "Save"
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvInventory.Columns(0).DataPropertyName = "#"
        dgvInventory.Columns(1).DataPropertyName = "Item Name"
        dgvInventory.Columns(2).DataPropertyName = "Category"
        dgvInventory.Columns(3).DataPropertyName = "COND."
        dgvInventory.Columns(4).DataPropertyName = "Source"
        dgvInventory.Columns(5).DataPropertyName = "Price"
        dgvInventory.Columns(6).DataPropertyName = "Status"

        LoadCategoryComboBox()
        LoadInventoryGrid()
        cmbCondition.Items.AddRange(New String() {"Brand New", "Like New", "Good", "Fair"})
        cmbStatus.Items.AddRange(New String() {"Available", "Sold", "Reserved"})
    End Sub
    Private Sub LoadDonorsIntoComboBox()
        Dim query As String = "SELECT donor_id As id, full_name As name FROM donors ORDER BY full_name ASC"
        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    cmbSupplier.DataSource = table
                    cmbSupplier.DisplayMember = "name"
                    cmbSupplier.ValueMember = "id"
                    cmbSupplier.SelectedIndex = -1
                Catch ex As MySqlException
                    MessageBox.Show("Error loading donors dropdown: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadConsignorsIntoComboBox()
        Dim query As String = "SELECT consignor_id As id, full_name As name FROM consignors ORDER BY full_name ASC"
        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    cmbSupplier.DataSource = table
                    cmbSupplier.DisplayMember = "name"
                    cmbSupplier.ValueMember = "id"
                    cmbSupplier.SelectedIndex = -1
                Catch ex As MySqlException
                    MessageBox.Show("Error loading consignors dropdown: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub dgvInventory_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInventory.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvInventory.Rows(e.RowIndex)
            currentSelectedItemId = Convert.ToInt32(row.Cells("#").Value)
            txtItemName.Text = row.Cells("Item Name").Value.ToString()
            txtPrice.Text = row.Cells("Price").Value.ToString()

            cmbCategory.Text = row.Cells("Category").Value.ToString()
            cmbCondition.Text = row.Cells("COND.").Value.ToString()
            cmbStatus.Text = row.Cells("Status").Value.ToString()

            Dim source As String = row.Cells("Source").Value.ToString()

            If source = "Donated" Then
                radDonated.PerformClick()
                cmbSupplier.Text = row.Cells("Supplier").Value.ToString()
            ElseIf source = "Consigned" Then
                radConsigned.PerformClick()
                cmbSupplier.Text = row.Cells("Supplier").Value.ToString()
            Else
                selectedSourceType = ""
                cmbSupplier.DataSource = Nothing
            End If

            btnSave.Text = "Update"
        End If
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


End Class