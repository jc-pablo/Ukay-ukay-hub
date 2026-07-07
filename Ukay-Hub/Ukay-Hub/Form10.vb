Imports MySql.Data.MySqlClient

Public Class Form10
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTopCategoriesReport()
    End Sub

    Public Sub LoadTopCategoriesReport()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT SUM(selling_price) FROM transactions"
            dbcomm = New MySqlCommand(sql, conn)
            Dim grandTotalSales As Decimal = Val(dbcomm.ExecuteScalar().ToString())

            sql = "SELECT c.category_name As 'Category', COUNT(t.transaction_id) As 'Item Sold', SUM(t.selling_price) As 'Total Revenue' " &
                  "FROM transactions t " &
                  "INNER JOIN inventory i ON t.item_id = i  .item_id " &
                  "INNER JOIN categories c ON i.category_id = c.category_id " &
                  "GROUP BY c.category_name ORDER BY SUM(t.selling_price) DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "RawCategories")

            Dim dt As DataTable = ds.Tables("RawCategories")

            Dim rankCol As New DataColumn("Rank", GetType(Integer))
            Dim pctCol As New DataColumn("% of Sales", GetType(String))

            dt.Columns.Add(rankCol)
            dt.Columns.Add(pctCol)
            rankCol.SetOrdinal(0)

            Dim currentRank As Integer = 1
            For Each row As DataRow In dt.Rows
                row("Rank") = currentRank
                Dim rev As Decimal = 0
                If Not Convert.IsDBNull(row("Total Revenue")) Then
                    rev = Convert.ToDecimal(row("Total Revenue"))
                End If

                If grandTotalSales > 0 Then
                    Dim pct As Decimal = (rev / grandTotalSales) * 100
                    row("% of Sales") = pct.ToString("F2") & "%"
                Else
                    row("% of Sales") = "0.00%"
                End If

                currentRank += 1
            Next

            dgvTopCategories.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
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