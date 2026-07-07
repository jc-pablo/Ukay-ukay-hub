Imports MySql.Data.MySqlClient

Public Class Form12
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConsignorEarningsReport()
    End Sub

    Public Sub LoadConsignorEarningsReport()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT CONCAT(c.first_name, ' ', c.last_name) As 'Consignor', " &
                  "COUNT(t.transaction_id) As 'Item Sold', " &
                  "SUM(t.selling_price) As 'Total Sales', " &
                  "'70%' As 'Rate', " &
                  "SUM(t.selling_price) * 0.70 As 'Total Earned', " &
                  "MAX(p.date_saved) As 'Last Payout' " &
                  "FROM consignors c " &
                  "INNER JOIN inventory i ON c.consignor_id = i.consignor_id " &
                  "INNER JOIN transactions t ON i.item_id = t.item_id " &
                  "LEFT JOIN payouts p ON c.consignor_id = p.consignor_id " &
                  "GROUP BY c.consignor_id"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "ConsignorEarnings")
            dgvConsignorEarnings.DataSource = ds
            dgvConsignorEarnings.DataMember = "ConsignorEarnings"
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

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Dim frm11 As New Form11()
        frm11.Show()
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