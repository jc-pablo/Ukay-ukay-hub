Imports MySql.Data.MySqlClient
Public Class Form10
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"

    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTopCategoriesGrid()
    End Sub
    Private Sub LoadTopCategoriesGrid()
        Dim query As String = "SET @rank := 0; " &
                              "SELECT (@rank := @rank + 1) As 'Rank', " &
                              "c.category_name As 'Category', " &
                              "COUNT(t.transaction_id) As 'Item Sold', " &
                              "SUM(t.selling_price) As 'Total Revenue', " &
                              "CONCAT(ROUND((SUM(t.selling_price) / (SELECT SUM(selling_price) FROM transactions)) * 100, 2), '%') As '% of Sales' " &
                              "FROM transactions t " &
                              "INNER JOIN inventory i ON t.item_id = i.item_id " &
                              "INNER JOIN categories c ON i.category_id = c.category_id " &
                              "GROUP BY c.category_id, c.category_name " &
                              "ORDER BY SUM(t.selling_price) DESC"

        Using conn As New MySqlConnection(connString)

            conn.ConnectionString = connString & ";AllowUserVariables=True"
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvTopCategories.DataSource = table
                    dgvTopCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As MySqlException
                    MessageBox.Show("Error gathering categories: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim frm2 As New Form2()
        frm2.Show()
        Me.Hide()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm1 As New Form1()
        frm1.Show()
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

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click
        Dim frm12 As New Form12()
        frm12.Show()
        Me.Hide()
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Dim frm11 As New Form11()
        frm11.Show()
        Me.Hide()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub
End Class