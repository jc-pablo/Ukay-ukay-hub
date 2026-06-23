Imports MySql.Data.MySqlClient
Public Class Form3
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"
    Private Sub LoadCategories()
        Dim query As String = "SELECT c.category_id As '#', " &
                          "c.category_name As 'Category Name', " &
                          "COUNT(i.item_id) As 'Item Count', " &
                          "c.date_added As 'Date Added' " &
                          "FROM categories c " &
                          "LEFT JOIN inventory i ON c.category_id = i.category_id " &
                          "GROUP BY c.category_id, c.category_name, c.date_added"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    dgvCategories.Columns.Clear()
                    dgvCategories.AutoGenerateColumns = True
                    dgvCategories.DataSource = table
                    dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As MySqlException
                    MessageBox.Show("Error loading categories: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtCategoryName.Text) Then
            MessageBox.Show("Please enter a category name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim query As String = "INSERT INTO categories (category_name) VALUES (@name)"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", txtCategoryName.Text.Trim())

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Category saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    txtCategoryName.Clear()
                    LoadCategories()
                Catch ex As MySqlException
                    If ex.Number = 1062 Then
                        MessageBox.Show("This category already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        MessageBox.Show("Error saving category: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Try
            End Using
        End Using
    End Sub
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvCategories.Columns(0).DataPropertyName = "category_id"
        dgvCategories.Columns(1).DataPropertyName = "category_name"
        dgvCategories.Columns(2).DataPropertyName = "item_count"
        dgvCategories.Columns(3).DataPropertyName = "date_added"

        LoadCategories()
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtCategoryName.Clear()
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