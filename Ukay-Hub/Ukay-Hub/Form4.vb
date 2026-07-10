Imports MySql.Data.MySqlClient

Public Class Form4
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Dim sql As String
    Dim dbcomm As MySqlCommand
    Dim DataAdapter1 As MySqlDataAdapter
    Dim ds As DataSet

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDonorData()
    End Sub

    Private Sub LoadDonorData()
        sql = "SELECT d.donor_id As '#', " &
              "d.full_name As 'Name', " &
              "d.contact_number As 'Contact', " &
              "COUNT(i.item_id) As 'Items Donated', " &
              "d.date_registered As 'Date Registered' " &
              "FROM donors d " &
              "LEFT JOIN inventory i ON d.donor_id = i.donor_id " &
              "GROUP BY d.donor_id, d.full_name, d.contact_number, d.date_registered " &
              "ORDER BY d.donor_id DESC"
        Try
            conn.Open()
            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "donors")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "donors"
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            MsgBox("Error loading donor data: " & ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        sql = "INSERT INTO donors (full_name, contact_number, email_address, address) VALUES (@name, @contact, @email, @address)"
        Try
            conn.Open()
            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
            dbcomm.Parameters.AddWithValue("@contact", txtContactNumber.Text.Trim())
            dbcomm.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim())
            dbcomm.Parameters.AddWithValue("@address", txtAddress.Text.Trim())

            Dim i As Integer = dbcomm.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Donor record saved successfully!")
                ClearFields()
            Else
                MsgBox("Record not saved.")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()
        LoadDonorData()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        sql = "UPDATE donors SET full_name=@name, contact_number=@contact, email_address=@email, address=@address WHERE donor_id=@id"
        Try
            conn.Open()
            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
            dbcomm.Parameters.AddWithValue("@contact", txtContactNumber.Text.Trim())
            dbcomm.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim())
            dbcomm.Parameters.AddWithValue("@address", txtAddress.Text.Trim())
            dbcomm.Parameters.AddWithValue("@id", selectedDonorId)

            Dim i As Integer = dbcomm.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Donor record updated successfully!")
            Else
                MsgBox("Record not updated.")
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        conn.Close()
        LoadDonorData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(selectedDonorId) Then
            MsgBox("Please select a donor to delete.")
            Return
        End If

        Dim ans = MessageBox.Show("Are you sure you want to delete this donor record?", "Confirm Delete", MessageBoxButtons.YesNo)
        If ans = DialogResult.Yes Then
            sql = "DELETE FROM donors WHERE donor_id=@id"
            Try
                conn.Open()
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.Parameters.AddWithValue("@id", selectedDonorId)

                Dim i As Integer = dbcomm.ExecuteNonQuery()
                If i > 0 Then
                    MsgBox("Donor record deleted.")
                    ClearFields()
                Else
                    MsgBox("Record not deleted.")
                End If
            Catch ex As MySqlException
                MsgBox(ex.Message)
            End Try
            conn.Close()
            LoadDonorData()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        sql = "SELECT d.donor_id As '#', " &
              "d.full_name As 'Name', " &
              "d.contact_number As 'Contact', " &
              "COUNT(i.item_id) As 'Items Donated', " &
              "d.date_registered As 'Date Registered' " &
              "FROM donors d " &
              "LEFT JOIN inventory i ON d.donor_id = i.donor_id " &
              "WHERE d.full_name LIKE @search OR d.donor_id LIKE @search " &
              "GROUP BY d.donor_id, d.full_name, d.contact_number, d.date_registered " &
              "ORDER BY d.donor_id DESC"
        Try
            conn.Open()
            dbcomm = New MySqlCommand(sql, conn)
            dbcomm.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")

            DataAdapter1 = New MySqlDataAdapter(dbcomm)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "donors")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "donors"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim selectedId As String = row.Cells(0).Value.ToString()

            Try
                conn.Open()
                sql = "SELECT * FROM donors WHERE donor_id=@id"
                dbcomm = New MySqlCommand(sql, conn)
                dbcomm.Parameters.AddWithValue("@id", selectedId)
                Dim dbread As MySqlDataReader = dbcomm.ExecuteReader()

                If dbread.Read() Then
                    selectedDonorId = dbread("donor_id").ToString()
                    txtFullName.Text = dbread("full_name").ToString()
                    txtContactNumber.Text = dbread("contact_number").ToString()
                    txtEmailAddress.Text = dbread("email_address").ToString()
                    txtAddress.Text = dbread("address").ToString()
                End If
                dbread.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            conn.Close()
        End If
    End Sub
    Private selectedDonorId As String = ""
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        selectedDonorId = ""
        txtFullName.Clear()
        txtContactNumber.Clear()
        txtEmailAddress.Clear()
        txtAddress.Clear()
        txtSearch.Clear()
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

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm1 As New Form1()
        frm1.Show()
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