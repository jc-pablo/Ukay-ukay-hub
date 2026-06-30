Imports MySql.Data.MySqlClient

Public Class Form5
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Dim sql As String
    Dim dbcomm As MySqlCommand
    Dim DataAdapter1 As MySqlDataAdapter
    Dim ds As DataSet

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConsignorData()
    End Sub

    Private Sub LoadConsignorData()
        Try
            conn.Open()
            sql = "SELECT c.consignor_id As '#', " &
              "CONCAT(c.first_name, ' ', c.last_name) As 'Name', " &
              "c.phone As 'Contact', " &
              "'10%' As 'Commission', " &
              "COUNT(i.item_id) As 'Items Listed', " &
              "IFNULL(SUM(CASE WHEN i.status = 'Sold' THEN i.price ELSE 0 END), 0.00) As 'TotalPriceSold' " &
              "FROM consignors c " &
              "LEFT JOIN inventory i ON c.consignor_id = i.consignor_id " &
              "GROUP BY c.consignor_id, c.first_name, c.last_name, c.phone " &
              "ORDER BY c.consignor_id DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            DataAdapter1.Fill(dt)

            dt.Columns.Add("Earned", GetType(Decimal))

            For Each row As DataRow In dt.Rows
                Dim Sales As Decimal = Convert.ToDecimal(row("TotalPriceSold"))
                row("Earned") = Sales * 0.1D
            Next

            dt.Columns.Remove("TotalPriceSold")
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox("Error loading consignor data: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim fullName As String = txtFullName.Text.Trim()
        Dim firstName As String = fullName
        Dim lastName As String = " "

        If fullName.Contains(" ") Then
            Dim parts As String() = fullName.Split(New Char() {" "c}, 2)
            firstName = parts(0)
            lastName = parts(1)
        End If

        Try
            conn.Open()
            sql = "INSERT INTO consignors (consignor_id, first_name, last_name, phone, email) VALUES (@id, @fname, @lname, @phone, @email)"
            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@id", txtConsignorId.Text.Trim())
            dbcomm.Parameters.AddWithValue("@fname", firstName)
            dbcomm.Parameters.AddWithValue("@lname", lastName)
            dbcomm.Parameters.AddWithValue("@phone", txtContactNumber.Text.Trim())
            dbcomm.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim())

            Dim i As Integer = dbcomm.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Consignor record saved successfully!")
                ClearFields()
            Else
                MsgBox("Record not saved.")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        LoadConsignorData()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim fullName As String = txtFullName.Text.Trim()
        Dim firstName As String = fullName
        Dim lastName As String = " "

        If fullName.Contains(" ") Then
            Dim parts As String() = fullName.Split(New Char() {" "c}, 2)
            firstName = parts(0)
            lastName = parts(1)
        End If

        Try
            conn.Open()
            sql = "UPDATE consignors SET first_name=@fname, last_name=@lname, phone=@phone, email=@email WHERE consignor_id=@id"
            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@fname", firstName)
            dbcomm.Parameters.AddWithValue("@lname", lastName)
            dbcomm.Parameters.AddWithValue("@phone", txtContactNumber.Text.Trim())
            dbcomm.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim())
            dbcomm.Parameters.AddWithValue("@id", txtConsignorId.Text.Trim())

            Dim i As Integer = dbcomm.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Consignor record updated successfully!")
            Else
                MsgBox("Record not updated.")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        LoadConsignorData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(txtConsignorId.Text) Then
            MsgBox("Please select a consignor to delete.")
            Return
        End If

        Dim ans = MessageBox.Show("Are you sure you want to delete this consignor record?", "Confirm Delete", MessageBoxButtons.YesNo)
        If ans = DialogResult.Yes Then
            Try
                conn.Open()
                sql = "DELETE FROM consignors WHERE consignor_id=@id"
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.Parameters.AddWithValue("@id", txtConsignorId.Text.Trim())

                Dim i As Integer = dbcomm.ExecuteNonQuery()
                If i > 0 Then
                    MsgBox("Consignor record deleted.")
                    ClearFields()
                Else
                    MsgBox("Record not deleted.")
                End If
            Catch ex As MySqlException
                MsgBox(ex.Message)
            Finally
                conn.Close()
            End Try
            LoadConsignorData()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            conn.Open()
            sql = "SELECT c.consignor_id As '#', " &
              "CONCAT(c.first_name, ' ', c.last_name) As 'Name', " &
              "c.phone As 'Contact', " &
              "'10%' As 'Commission', " &
              "COUNT(i.item_id) As 'Items Listed', " &
              "IFNULL(SUM(CASE WHEN i.status = 'Sold' THEN i.price ELSE 0 END), 0.00) As 'TotalPriceSold' " &
              "FROM consignors c " &
              "LEFT JOIN inventory i ON c.consignor_id = i.consignor_id " &
              "WHERE c.first_name LIKE @search OR c.last_name LIKE @search OR c.consignor_id LIKE @search " &
              "GROUP BY c.consignor_id, c.first_name, c.last_name, c.phone"

            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")

            DataAdapter1 = New MySqlDataAdapter(dbcomm)
            Dim dt As New DataTable()
            DataAdapter1.Fill(dt)
            dt.Columns.Add("Total Earned", GetType(Decimal))
            For Each row As DataRow In dt.Rows
                Dim totalSales As Decimal = Convert.ToDecimal(row("TotalPriceSold"))
                row("Total Earned") = totalSales * 0.1D
            Next
            dt.Columns.Remove("TotalPriceSold")

            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim selectedId As String = row.Cells("#").Value.ToString()

            Try
                conn.Open()
                sql = "SELECT * FROM consignors WHERE consignor_id=@id"
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.Parameters.AddWithValue("@id", selectedId)
                Dim dbread As MySqlDataReader = dbcomm.ExecuteReader()

                If dbread.Read() Then
                    txtConsignorId.Text = dbread("consignor_id").ToString()
                    txtFullName.Text = dbread("first_name").ToString() & " " & dbread("last_name").ToString()
                    txtContactNumber.Text = dbread("phone").ToString()
                    txtEmailAddress.Text = dbread("email").ToString()
                End If
                dbread.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtConsignorId.Clear()
        txtFullName.Clear()
        txtContactNumber.Clear()
        txtEmailAddress.Clear()
        txtSearch.Clear()
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