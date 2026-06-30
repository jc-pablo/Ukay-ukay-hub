Imports MySql.Data.MySqlClient

Public Class Form14
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Dim sql As String
    Dim grandTotal As Integer = 0

    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProductsToShop()
        lblTotalAmount.Text = "₱0"
    End Sub

    Public Sub LoadProductsToShop()
        flpProducts.Controls.Clear()

        Try
            conn.Open()
            sql = "SELECT item_id, item_name, price FROM inventory WHERE status = 'Available'"

            Dim dt As New DataTable()
            Dim da As New MySqlDataAdapter(sql, conn)
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                Dim lblEmpty As New Label()
                lblEmpty.Text = "The shop is currently empty. No items found in the database."
                lblEmpty.Font = New Font("Segoe UI", 14, FontStyle.Italic)
                lblEmpty.ForeColor = Color.Gray
                lblEmpty.AutoSize = True
                flpProducts.Controls.Add(lblEmpty)
            Else
                For Each row As DataRow In dt.Rows
                    Dim card As New ucProductCard()
                    card.ItemName = row("item_name").ToString()
                    card.Price = Convert.ToInt32(row("price"))
                    AddHandler card.AddToCart, AddressOf PagMayNagAddToCart

                    flpProducts.Controls.Add(card)

                Next
            End If

        Catch ex As Exception
            MsgBox("Error loading products: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub PagMayNagAddToCart(sender As Object, name As String, price As Integer)
        pnlCart.Visible = True
        pnlCart.BringToFront()

        Dim lblItem As New Label()
        lblItem.Text = name & "   ->   ₱" & price
        lblItem.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblItem.AutoSize = True

        flpCartItems.Controls.Add(lblItem)
        grandTotal = grandTotal + price
        lblTotalAmount.Text = "₱" & grandTotal.ToString()
    End Sub

    Private Sub lblCart_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCart.LinkClicked
        If pnlCart.Visible = False Then
            pnlCart.Visible = True
            pnlCart.BringToFront()
        Else
            pnlCart.Visible = False
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm1 As New Form1()
        frm1.Show()
        Me.Hide()
    End Sub
End Class