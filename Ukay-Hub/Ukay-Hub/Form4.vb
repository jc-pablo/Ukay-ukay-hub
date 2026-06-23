Imports MySql.Data.MySqlClient
Public Class Form4
    Dim connString As String = "server=localhost;user=root;password=root;database=ukayhub_db"

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDonors()
    End Sub
    Private Sub LoadDonors()
        Dim query As String = "SELECT d.donor_id As '#', d.full_name As 'Name', d.contact_number As 'Contact', " &
                              "COUNT(i.item_id) As 'Items Donated', d.date_registered As 'Date Registered' " &
                              "FROM donors d " &
                              "LEFT JOIN inventory i ON d.donor_id = i.donor_id " &
                              "GROUP BY d.donor_id, d.full_name, d.contact_number, d.date_registered"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvDonors.Columns.Clear()
                    dgvDonors.AutoGenerateColumns = True
                    dgvDonors.DataSource = table
                    dgvDonors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                Catch ex As MySqlException
                    MessageBox.Show("Error loading donors: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrWhiteSpace(txtFullName.Text) Then
            MessageBox.Show("Please enter the donor's full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim query As String = "INSERT INTO donors (full_name, contact_number, email_address, address) " &
                              "VALUES (@name, @contact, @email, @address)"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
                cmd.Parameters.AddWithValue("@contact", txtContactNumber.Text.Trim())
                cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text.Trim())
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim())

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Donor registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearForm()
                    LoadDonors()
                Catch ex As MySqlException
                    MessageBox.Show("Error saving donor: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub ClearForm()
        txtFullName.Clear()
        txtContactNumber.Clear()
        txtEmailAddress.Clear()
        txtAddress.Clear()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
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