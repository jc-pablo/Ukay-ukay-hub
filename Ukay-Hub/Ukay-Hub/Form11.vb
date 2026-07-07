Imports MySql.Data.MySqlClient

Public Class Form11
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryCounters()
        LoadCategoryBreakdown()
    End Sub

    Public Sub LoadInventoryCounters()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT COUNT(*) FROM inventory WHERE status = 'Available'"
            dbcomm = New MySqlCommand(sql, conn)
            lblAvailableCount.Text = dbcomm.ExecuteScalar().ToString()

            sql = "SELECT COUNT(*) FROM inventory WHERE status = 'Sold'"
            dbcomm = New MySqlCommand(sql, conn)
            lblSoldCount.Text = dbcomm.ExecuteScalar().ToString()

            sql = "SELECT COUNT(*) FROM inventory WHERE status = 'Reserved'"
            dbcomm = New MySqlCommand(sql, conn)
            lblReservedCount.Text = dbcomm.ExecuteScalar().ToString()

            sql = "SELECT COUNT(*) FROM inventory"
            dbcomm = New MySqlCommand(sql, conn)
            lblTotalCount.Text = dbcomm.ExecuteScalar().ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Public Sub LoadCategoryBreakdown()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT c.category_name As 'Category', " &
                  "SUM(IF(i.status = 'Available', 1, 0)) As 'Available', " &
                  "SUM(IF(i.status = 'Sold', 1, 0)) As 'Sold', " &
                  "SUM(IF(i.status = 'Reserved', 1, 0)) As 'Reserved', " &
                  "COUNT(i.item_id) As 'Total' " &
                  "FROM categories c " &
                  "LEFT JOIN inventory i ON c.category_id = i.category_id " &
                  "GROUP BY c.category_name"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "CategoryBreakdown")
            dgvInventoryCategory.DataSource = ds
            dgvInventoryCategory.DataMember = "CategoryBreakdown"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
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