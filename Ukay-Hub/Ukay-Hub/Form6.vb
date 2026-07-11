Imports MySql.Data.MySqlClient

Public Class Form6
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Dim selectedSourceType As String = ""
    Dim selectedItemId As String = ""

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbCondition.Items.AddRange(New String() {"Brand New", "Like New", "Good", "Fair"})
        cmbStatus.Items.AddRange(New String() {"Available", "Sold", "Reserved"})

        LoadCategoryComboBox()
        LoadInventoryGrid()
    End Sub

    Public Sub LoadInventoryGrid()
        sql = "SELECT i.item_id As '#', i.item_name As 'Item Name', c.category_name As 'Category', " &
              "i.item_condition As 'COND.', i.source_type As 'Source', " &
              "d.full_name As 'Donor', CONCAT(con.first_name, ' ', con.last_name) As 'Consignor', " &
              "i.price As 'Price', i.status As 'Status' " &
              "FROM inventory i " &
              "LEFT JOIN categories c ON i.category_id = c.category_id " &
              "LEFT JOIN donors d ON i.donor_id = d.donor_id " &
              "LEFT JOIN consignors con ON i.consignor_id = con.consignor_id " &
              "ORDER BY i.item_id DESC"
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "inventory")

            dgvInventory.DataSource = ds
            dgvInventory.DataMember = "inventory"
            dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As MySqlException
            MsgBox("Error loading inventory grid data: " & ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub LoadCategoryComboBox()
        sql = "SELECT category_id, category_name FROM categories"
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "categories")

            cmbCategory.DataSource = ds.Tables("categories")
            cmbCategory.DisplayMember = "category_name"
            cmbCategory.ValueMember = "category_id"
            cmbCategory.SelectedIndex = -1
        Catch ex As MySqlException
            MsgBox("Error loading categories to dropdown: " & ex.Message)
            conn.Close()
        End Try
        conn.Close()
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
        sql = "SELECT donor_id As id, full_name As name FROM donors ORDER BY full_name ASC"
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "donors")

            cmbSupplier.DataSource = ds.Tables("donors")
            cmbSupplier.DisplayMember = "name"
            cmbSupplier.ValueMember = "id"
            cmbSupplier.SelectedIndex = -1
        Catch ex As MySqlException
            MsgBox("Error loading donors dropdown: " & ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub LoadConsignorsIntoComboBox()
        sql = "SELECT consignor_id As id, CONCAT(first_name, ' ', last_name) As name FROM consignors ORDER BY name ASC"
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "consignors")

            cmbSupplier.DataSource = ds.Tables("consignors")
            cmbSupplier.DisplayMember = "name"
            cmbSupplier.ValueMember = "id"
            cmbSupplier.SelectedIndex = -1
        Catch ex As MySqlException
            MsgBox("Error loading consignors dropdown: " & ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not String.IsNullOrWhiteSpace(selectedItemId) Then
            MsgBox("Clear the form first before adding a new item record.")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtItemName.Text) OrElse cmbCategory.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtPrice.Text) Then
            MsgBox("Please fill out the Item Name, Category, and Price.")
            Exit Sub
        End If

        Try
            Dim catId As String = cmbCategory.SelectedValue.ToString()
            Dim condStr As String = If(cmbCondition.SelectedIndex <> -1, $"'{cmbCondition.SelectedItem.ToString()}'", "NULL")
            Dim srcStr As String = If(selectedSourceType <> "", $"'{selectedSourceType}'", "NULL")
            Dim statusStr As String = If(cmbStatus.SelectedIndex <> -1, $"'{cmbStatus.SelectedItem.ToString()}'", "'Available'")

            Dim donorId As String = "NULL"
            Dim consignorId As String = "NULL"

            If selectedSourceType = "Donated" AndAlso cmbSupplier.SelectedIndex <> -1 Then
                donorId = cmbSupplier.SelectedValue.ToString()
            ElseIf selectedSourceType = "Consigned" AndAlso cmbSupplier.SelectedIndex <> -1 Then
                consignorId = cmbSupplier.SelectedValue.ToString()
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = $"INSERT INTO inventory (item_name, description, category_id, item_condition, source_type, price, status, donor_id, consignor_id) " &
                  $"VALUES('{txtItemName.Text.Trim()}', '{txtDescription.Text.Trim()}', {catId}, {condStr}, {srcStr}, {Val(txtPrice.Text)}, {statusStr}, {donorId}, {consignorId})"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery

            If (i > 0) Then
                MsgBox("Record saved successfully.")

                If Application.OpenForms().OfType(Of Form14).Any() Then
                    Form14.LoadProductsToShop()
                End If
            Else
                MsgBox("Record not saved.")
            End If

            ClearForm()
            conn.Close()
            LoadInventoryGrid()
        Catch ex As MySqlException
            MsgBox("Error saving inventory record: " & ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(selectedItemId) Then
            MsgBox("Please select an item from the list to update.")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtItemName.Text) OrElse cmbCategory.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtPrice.Text) Then
            MsgBox("Please fill out the Item Name, Category, and Price.")
            Exit Sub
        End If

        Try
            Dim catId As String = cmbCategory.SelectedValue.ToString()
            Dim condStr As String = If(cmbCondition.SelectedIndex <> -1, $"'{cmbCondition.SelectedItem.ToString()}'", "NULL")
            Dim srcStr As String = If(selectedSourceType <> "", $"'{selectedSourceType}'", "NULL")
            Dim statusStr As String = If(cmbStatus.SelectedIndex <> -1, $"'{cmbStatus.SelectedItem.ToString()}'", "'Available'")

            Dim donorId As String = "NULL"
            Dim consignorId As String = "NULL"

            If selectedSourceType = "Donated" AndAlso cmbSupplier.SelectedIndex <> -1 Then
                donorId = cmbSupplier.SelectedValue.ToString()
            ElseIf selectedSourceType = "Consigned" AndAlso cmbSupplier.SelectedIndex <> -1 Then
                consignorId = cmbSupplier.SelectedValue.ToString()
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = $"UPDATE inventory SET " &
                  $"item_name='{txtItemName.Text.Trim()}', " &
                  $"description='{txtDescription.Text.Trim()}', " &
                  $"category_id={catId}, " &
                  $"item_condition={condStr}, " &
                  $"source_type={srcStr}, " &
                  $"price={Val(txtPrice.Text)}, " &
                  $"status={statusStr}, " &
                  $"donor_id={donorId}, " &
                  $"consignor_id={consignorId} " &
                  $"WHERE item_id={selectedItemId}"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery

            If (i > 0) Then
                MsgBox("Record updated successfully.")

                If Application.OpenForms().OfType(Of Form14).Any() Then
                    Form14.LoadProductsToShop()
                End If
            Else
                MsgBox("Record not saved.")
            End If

            ClearForm()
            conn.Close()
            LoadInventoryGrid()
        Catch ex As MySqlException
            MsgBox("Error updating inventory record: " & ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(selectedItemId) Then
            MsgBox("Please select an item from the list to delete.")
            Exit Sub
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Try
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                sql = $"DELETE FROM inventory WHERE item_id={selectedItemId}"
                dbcomm = New MySqlCommand(sql, conn)
                Dim i As Integer = dbcomm.ExecuteNonQuery

                If (i > 0) Then
                    MsgBox("Item deleted successfully.")

                    If Application.OpenForms().OfType(Of Form14).Any() Then
                        Form14.LoadProductsToShop()
                    End If
                Else
                    MsgBox("Item not deleted.")
                End If

                ClearForm()
                conn.Close()
                LoadInventoryGrid()
            Catch ex As MySqlException
                MsgBox("Error deleting record: " & ex.Message)
                conn.Close()
            End Try
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            sql = "SELECT i.item_id As '#', i.item_name As 'Item Name', c.category_name As 'Category', " &
                  "i.item_condition As 'COND.', i.source_type As 'Source', " &
                  "d.full_name As 'Donor', CONCAT(con.first_name, ' ', con.last_name) As 'Consignor', " &
                  "i.price As 'Price', i.status As 'Status' " &
                  "FROM inventory i " &
                  "LEFT JOIN categories c ON i.category_id = c.category_id " &
                  "LEFT JOIN donors d ON i.donor_id = d.donor_id " &
                  "LEFT JOIN consignors con ON i.consignor_id = con.consignor_id " &
                  $"WHERE i.item_name LIKE '%{txtSearch.Text.Trim()}%' " &
                  $"OR i.item_id LIKE '%{txtSearch.Text.Trim()}%' " &
                  $"OR c.category_name LIKE '%{txtSearch.Text.Trim()}%' " &
                  "ORDER BY i.item_id DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "inventory")
            dgvInventory.DataSource = ds
            dgvInventory.DataMember = "inventory"
        Catch ex As Exception
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub dgvInventory_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInventory.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvInventory.Rows(e.RowIndex)
            Dim selectedId As String = row.Cells(0).Value.ToString()

            Try
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                sql = $"SELECT * FROM inventory WHERE item_id={selectedId}"
                dbcomm = New MySqlCommand(sql, conn)
                dbread = dbcomm.ExecuteReader()

                If dbread.Read() Then
                    selectedItemId = dbread("item_id").ToString()
                    txtItemName.Text = dbread("item_name").ToString()
                    txtDescription.Text = dbread("description").ToString()
                    txtPrice.Text = dbread("price").ToString()
                    cmbCategory.SelectedValue = dbread("category_id")
                    cmbCondition.Text = dbread("item_condition").ToString()
                    cmbStatus.Text = dbread("status").ToString()

                    Dim source As String = dbread("source_type").ToString()
                    Dim donorId As Object = dbread("donor_id")
                    Dim consignorId As Object = dbread("consignor_id")

                    dbread.Close()

                    If source = "Donated" Then
                        radDonated.Checked = True
                        cmbSupplier.SelectedValue = donorId
                    Else
                        radConsigned.Checked = True
                        cmbSupplier.SelectedValue = consignorId
                    End If
                Else
                    dbread.Close()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try
            conn.Close()
        End If
    End Sub

    Private Sub ClearForm()
        selectedItemId = ""
        txtItemName.Clear()
        txtDescription.Clear()
        txtPrice.Clear()
        cmbCategory.SelectedIndex = -1
        cmbCondition.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1
        cmbSupplier.SelectedIndex = -1
        radDonated.Checked = False
        radConsigned.Checked = False
        selectedSourceType = ""
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
        LoadInventoryGrid()
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

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub
End Class