Imports MySql.Data.MySqlClient
Public Class Form5
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConsignors()
    End Sub
    Private Sub LoadConsignors()
        Dim query As String = "SELECT c.consignor_id As '#', c.full_name As 'Name', c.contact_number As 'Contact', " &
                              "CONCAT(c.commission_rate, '%') As 'Commission', " &
                              "COUNT(i.item_id) As 'Items Listed', " &
                              "COALESCE(SUM(CASE WHEN i.status = 'Sold' THEN i.price ELSE 0 END), 0) As 'Total Earned' " &
                              "FROM consignors c " &
                              "LEFT JOIN inventory i ON c.consignor_id = i.consignor_id " &
                              "GROUP BY c.consignor_id, c.full_name, c.contact_number, c.commission_rate"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvConsignors.Columns.Clear()
                    dgvConsignors.AutoGenerateColumns = True
                    dgvConsignors.DataSource = table
                    dgvConsignors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As MySqlException
                    MessageBox.Show("Error loading consignors: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If String.IsNullOrWhiteSpace(txtFullName.Text) OrElse String.IsNullOrWhiteSpace(txtCommissionRate.Text) Then
            MessageBox.Show("Please fill out both Name and Commission Rate fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim commission As Decimal
        If Not Decimal.TryParse(txtCommissionRate.Text, commission) Then
            MessageBox.Show("Please enter a valid number for the commission percentage rate.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim query As String = "INSERT INTO consignors (full_name, contact_number, email_address, commission_rate) " &
                              "VALUES (@name, @contact, @email, @rate)"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
                cmd.Parameters.AddWithValue("@contact", txtContactNumber.Text.Trim())
                cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim())
                cmd.Parameters.AddWithValue("@rate", commission)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Consignor registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadConsignors()
                Catch ex As MySqlException
                    MessageBox.Show("Error saving consignor: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub ClearForm()
        txtFullName.Clear()
        txtContactNumber.Clear()
        txtEmailAddress.Clear()
        txtCommissionRate.Clear()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
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