Imports MySql.Data.MySqlClient

Public Class Form14
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Dim sql As String
    Dim grandTotal As Integer = 0
    Dim cartItemIDs As New List(Of Integer)()

    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProductsToShop()
        lblTotalAmount.Text = "₱0"
    End Sub

    Public Sub LoadProductsToShop()
        flpProducts.Controls.Clear()

        Try
            conn.Open()
            sql = "SELECT item_id, item_name, description, price FROM inventory WHERE status = 'Available'"

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
                    card.ItemDescription = row("description").ToString()
                    card.Price = Convert.ToInt32(row("price"))
                    card.Tag = row("item_id")
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
        Dim card As ucProductCard = CType(sender, ucProductCard)
        Dim itemId As Integer = Convert.ToInt32(card.Tag)
        cartItemIDs.Add(itemId)

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
        Dim frm13 As New Form13()
        frm13.Show()
        Me.Hide()
    End Sub

    Private Sub flpProducts_Paint(sender As Object, e As PaintEventArgs) Handles flpProducts.Paint

    End Sub

    Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        If cartItemIDs.Count = 0 Then
            MessageBox.Show("Your shopping cart is empty!", "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to checkout these items?", "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirm = DialogResult.Yes Then
            Dim transaction As MySqlTransaction = Nothing
            Dim customerName As String = "Valued Customer"
            Dim receiptItems As String = ""

            Try
                conn.Open()
                transaction = conn.BeginTransaction()

                sql = "SELECT full_name FROM customers ORDER BY user_id DESC LIMIT 1"
                Using cmdUser As New MySqlCommand(sql, conn, transaction)
                    Dim userObj As Object = cmdUser.ExecuteScalar()
                    If userObj IsNot Nothing AndAlso userObj.ToString() <> "" Then
                        customerName = userObj.ToString()
                    End If
                End Using

                For Each itemId As Integer In cartItemIDs
                    sql = "SELECT item_name FROM inventory WHERE item_id = @id"
                    Using cmdName As New MySqlCommand(sql, conn, transaction)
                        cmdName.Parameters.AddWithValue("@id", itemId)
                        Dim nameObj As Object = cmdName.ExecuteScalar()
                        If nameObj IsNot Nothing Then
                            receiptItems &= "- " & nameObj.ToString() & vbCrLf
                        End If
                    End Using

                    Dim sqlTransactions As String = "INSERT INTO transactions (item_id, selling_price, sale_date, payout_status) " &
                                                    "VALUES (@item_id, (SELECT price FROM inventory WHERE item_id = @item_id), CURDATE(), 'Unpaid')"

                    Using cmdTrans As New MySqlCommand(sqlTransactions, conn, transaction)
                        cmdTrans.Parameters.AddWithValue("@item_id", itemId)
                        cmdTrans.ExecuteNonQuery()
                    End Using

                    Dim sqlUpdate As String = "UPDATE inventory SET status = 'Sold' WHERE item_id = @id"
                    Using cmdUpdate As New MySqlCommand(sqlUpdate, conn, transaction)
                        cmdUpdate.Parameters.AddWithValue("@id", itemId)
                        cmdUpdate.ExecuteNonQuery()
                    End Using
                Next

                transaction.Commit()

                Dim receiptMessage As String = "==================================" & vbCrLf &
                                               "            UKAYHUB RECEIPT       " & vbCrLf &
                                               "==================================" & vbCrLf &
                                               "Customer: " & customerName & vbCrLf &
                                               "Date: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & vbCrLf &
                                               "----------------------------------" & vbCrLf &
                                               "Items Purchased:" & vbCrLf &
                                               receiptItems &
                                               "----------------------------------" & vbCrLf &
                                               "Grand Total: ₱" & grandTotal.ToString("N2") & vbCrLf &
                                               "==================================" & vbCrLf &
                                               "Thank you for shopping with us!"

                MessageBox.Show(receiptMessage, "Order Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                grandTotal = 0
                lblTotalAmount.Text = "₱0"
                flpCartItems.Controls.Clear()
                cartItemIDs.Clear()

                conn.Close()
                LoadProductsToShop()

            Catch ex As Exception
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                MessageBox.Show("An error occurred while saving your order: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End If
    End Sub
End Class