Imports MySql.Data.MySqlClient
Public Class Form11
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryStatusCounters()
        LoadInventoryCategoryBreakdown()
    End Sub

    Private Sub LoadInventoryStatusCounters()
        Dim query As String = "SELECT " &
                              "COUNT(CASE WHEN status = 'Available' THEN 1 END) As AvailableCount, " &
                              "COUNT(CASE WHEN status = 'Sold' THEN 1 END) As SoldCount, " &
                              "COUNT(CASE WHEN status = 'Reserved' THEN 1 END) As ReservedCount, " &
                              "COUNT(*) As TotalCount FROM inventory"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        lblAvailableCard.Text = reader("AvailableCount").ToString()
                        lblSold.Text = reader("SoldCount").ToString()
                        lblReserved.Text = reader("ReservedCount").ToString()
                        lblTotal.Text = reader("TotalCount").ToString()
                    End If
                    reader.Close()
                Catch ex As Exception
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadInventoryCategoryBreakdown()
        Dim query As String = "SELECT c.category_name As 'Category', " &
                              "COUNT(CASE WHEN i.status = 'Available' THEN 1 END) As 'Available', " &
                              "COUNT(CASE WHEN i.status = 'Sold' THEN 1 END) As 'Sold', " &
                              "COUNT(CASE WHEN i.status = 'Reserved' THEN 1 END) As 'Reserved', " &
                              "COUNT(i.item_id) As 'Total' " &
                              "FROM categories c " &
                              "LEFT JOIN inventory i ON c.category_id = i.category_id " &
                              "GROUP BY c.category_id, c.category_name"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvInventoryCategory.DataSource = table
                    dgvInventoryCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                Catch ex As Exception
                End Try
            End Using
        End Using
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

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Dim frm10 As New Form10()
        frm10.Show()
        Me.Hide()
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click
        Dim frm9 As New Form9()
        frm9.Show()
        Me.Hide()
    End Sub
End Class