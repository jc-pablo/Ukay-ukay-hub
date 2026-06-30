Imports MySql.Data.MySqlClient

Public Class Form6
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayukay_db"
    Dim selectedSourceType As String = ""

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbCondition.Items.AddRange(New String() {"Brand New", "Like New", "Good", "Fair"})
        cmbStatus.Items.AddRange(New String() {"Available", "Sold", "Reserved"})

        LoadCategoryComboBox()
        LoadInventoryGrid()
    End Sub

    Private Sub LoadInventoryGrid()
        Dim query As String = "SELECT i.item_id As '#', i.item_name As 'Item Name', c.category_name As 'Category', " &
                              "i.item_condition As 'COND.', i.source_type As 'Source', " &
                              "d.full_name As 'Donor', CONCAT(con.first_name, ' ', con.last_name) As 'Consignor', " &
                              "i.price As 'Price', i.status As 'Status' " &
                              "FROM inventory i " &
                              "LEFT JOIN categories c ON i.category_id = c.category_id " &
                              "LEFT JOIN donors d ON i.donor_id = d.donor_id " &
                              "LEFT JOIN consignors con ON i.consignor_id = con.consignor_id " &
                              "ORDER BY i.item_id DESC"

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
        Dim query As String = "SELECT consignor_id As id, CONCAT(first_name, ' ', last_name) As name FROM consignors ORDER BY name ASC"
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtItemId.Text) OrElse String.IsNullOrWhiteSpace(txtItemName.Text) OrElse cmbCategory.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtPrice.Text) Then
            MessageBox.Show("Please fill out the Item ID, Item Name, Category, and Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim price As Decimal
        Decimal.TryParse(txtPrice.Text, price)

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("INSERT INTO inventory (item_id, item_name, description, category_id, item_condition, source_type, price, status, donor_id, consignor_id) " &
                                      "VALUES (@itemId, @name, @desc, @catId, @cond, @source, @price, @status, @donorId, @consignorId)", conn)

                cmd.Parameters.AddWithValue("@itemId", txtItemId.Text.Trim())
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

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Inventory record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadInventoryGrid()
                Catch ex As MySqlException
                    MessageBox.Show("Error saving inventory record: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(txtItemId.Text) Then
            MessageBox.Show("Please select an item from the list to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtItemName.Text) OrElse cmbCategory.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtPrice.Text) Then
            MessageBox.Show("Please fill out the Item Name, Category, and Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim price As Decimal
        Decimal.TryParse(txtPrice.Text, price)

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand("UPDATE inventory SET item_name=@name, description=@desc, category_id=@catId, item_condition=@cond, " &
                                      "source_type=@source, price=@price, status=@status, donor_id=@donorId, consignor_id=@consignorId " &
                                      "WHERE item_id=@itemId", conn)

                cmd.Parameters.AddWithValue("@itemId", txtItemId.Text.Trim())
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

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Inventory record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadInventoryGrid()
                Catch ex As MySqlException
                    MessageBox.Show("Error updating inventory record: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtItemId.Text) Then
            MessageBox.Show("Please select an item from the list to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this inventory record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim query As String = "DELETE FROM inventory WHERE item_id=@itemId"
            Using conn As New MySqlConnection(connString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@itemId", txtItemId.Text.Trim())
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Item deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearForm()
                        LoadInventoryGrid()
                    Catch ex As MySqlException
                        MessageBox.Show("Error deleting record: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim query As String = "SELECT i.item_id As '#', i.item_name As 'Item Name', c.category_name As 'Category', " &
                              "i.item_condition As 'COND.', i.source_type As 'Source', " &
                              "d.full_name As 'Donor', CONCAT(con.first_name, ' ', con.last_name) As 'Consignor', " &
                              "i.price As 'Price', i.status As 'Status' " &
                              "FROM inventory i " &
                              "LEFT JOIN categories c ON i.category_id = c.category_id " &
                              "LEFT JOIN donors d ON i.donor_id = d.donor_id " &
                              "LEFT JOIN consignors con ON i.consignor_id = con.consignor_id " &
                              "WHERE i.item_name LIKE @search OR i.item_id LIKE @search OR c.category_name LIKE @search " &
                              "ORDER BY i.item_id DESC"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvInventory.DataSource = table
                Catch ex As Exception
                End Try
            End Using
        End Using
    End Sub

    Private Sub dgvInventory_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInventory.CellClick
        dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvInventory.Rows(e.RowIndex)

            txtItemId.Text = row.Cells("#").Value.ToString()
            txtItemId.Enabled = False

            txtItemName.Text = row.Cells("Item Name").Value.ToString()
            txtPrice.Text = row.Cells("Price").Value.ToString()

            cmbCategory.Text = row.Cells("Category").Value.ToString()
            cmbCondition.Text = row.Cells("COND.").Value.ToString()
            cmbStatus.Text = row.Cells("Status").Value.ToString()

            Dim source As String = row.Cells("Source").Value.ToString()

            If source = "Donated" Then
                radDonated.Checked = True
                cmbSupplier.Text = row.Cells("Donor").Value.ToString()
            ElseIf source = "Consigned" Then
                radConsigned.Checked = True
                cmbSupplier.Text = row.Cells("Consignor").Value.ToString()
            Else
                selectedSourceType = ""
                cmbSupplier.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub ClearForm()
        txtItemId.Clear()
        txtItemName.Clear()
        txtDescription.Clear()
        cmbCategory.SelectedIndex = -1
        cmbCondition.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1
        txtPrice.Clear()
        txtSearch.Clear()
        selectedSourceType = ""
        cmbSupplier.DataSource = Nothing

        radDonated.Checked = False
        radConsigned.Checked = False
        txtItemId.Enabled = True
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