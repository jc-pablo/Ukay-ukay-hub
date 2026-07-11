Imports MySql.Data.MySqlClient

Public Class Form3
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public dbread As MySqlDataReader
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories()
    End Sub

    Private Sub LoadCategories()
        Try
            conn.Open()
            sql = "SELECT c.category_id as '#', c.category_name AS 'Category Name', " &
                  "count(i.item_id) AS 'Item Count', c.date_added AS 'Date Added' " &
                  "FROM categories c " &
                  "LEFT JOIN inventory i ON (c.category_id = i.category_id) " &
                  "GROUP BY c.category_id, c.category_name, c.date_added " &
                  "ORDER BY c.category_id DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "categories")
            dgvCategories.DataSource = ds
            dgvCategories.DataMember = "categories"

        Catch ex As Exception
            MsgBox("Error in collecting data from Database. Error is :" & ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            conn.Open()
            sql = $"INSERT INTO categories (category_name, date_added) " &
                  $"VALUES('{txtCategoryName.Text.Trim()}', NOW())"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery

            If (i > 0) Then
                MsgBox("record saved")
            Else
                MsgBox("record not saved")
            End If

        Catch ex As MySqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()

        txtCategoryName.Clear()
        LoadCategories()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvCategories.SelectedRows.Count = 0 Then
            MsgBox("Please select a category from the list first.")
            Exit Sub
        End If

        Try
            Dim currentId As String = dgvCategories.SelectedRows(0).Cells(0).Value.ToString()

            conn.Open()
            sql = $"UPDATE categories SET category_name = '{txtCategoryName.Text.Trim()}' " &
                  $"WHERE category_id = {currentId}"

            dbcomm = New MySqlCommand(sql, conn)
            Dim i As Integer = dbcomm.ExecuteNonQuery

            If (i > 0) Then
                MsgBox("record saved")
            Else
                MsgBox("record not saved")
            End If

        Catch ex As MySqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()

        txtCategoryName.Clear()
        LoadCategories()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvCategories.SelectedRows.Count = 0 Then
            MsgBox("Please select a category from the list first.")
            Exit Sub
        End If

        Try
            Dim ans = MessageBox.Show("do you want to delete this record?", "record deleted", MessageBoxButtons.YesNoCancel)
            If ans = DialogResult.Yes Then
                Dim currentId As String = dgvCategories.SelectedRows(0).Cells(0).Value.ToString()

                conn.Open()
                sql = $"DELETE FROM categories WHERE category_id = {currentId}"

                dbcomm = New MySqlCommand(sql, conn)
                Dim i As Integer = dbcomm.ExecuteNonQuery

                If (i > 0) Then
                    MsgBox("item deleted")
                Else
                    MsgBox("item not deleted")
                End If
                txtCategoryName.Clear()
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
        LoadCategories()
    End Sub

    Private Sub dgvCategories_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategories.CellClick
        dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = dgvCategories.Rows(e.RowIndex)
            selectedRow.Selected = True
            txtCategoryName.Text = selectedRow.Cells(1).Value.ToString()
        End If
    End Sub

    Private Sub txtSearchCategory_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCategory.TextChanged
        Try
            conn.Open()

            sql = "SELECT c.category_id as '#', c.category_name AS 'Category Name', " &
              "COUNT(i.item_id) AS 'Item Count', c.date_added AS 'Date Added' " &
              "FROM categories c " &
              "LEFT JOIN inventory i ON (c.category_id = i.category_id) " &
              $"WHERE c.category_name LIKE '%{txtSearchCategory.Text.Trim()}%' " &
              "GROUP BY c.category_id, c.category_name, c.date_added " &
              "ORDER BY c.category_id DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "categories")
            dgvCategories.DataSource = ds
            dgvCategories.DataMember = "categories"
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As MySqlException
            MsgBox("Error in collecting data from Database. Error is :" & ex.Message)
        Catch ex As Exception
            MsgBox("Error in collecting data from Database. Error is :" & ex.Message)
        End Try
        conn.Close()
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
End Class