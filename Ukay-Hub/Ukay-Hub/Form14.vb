Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MySql.Data.MySqlClient
Imports Ukay_Hub.My.Resources

Public Class Form14
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Dim sql As String
    Dim grandTotal As Integer = 0
    Dim cartItemIDs As New List(Of Integer)()
    Dim cartItems As New List(Of CartItem)()

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
                lblEmpty.Font = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Italic)
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
        cartItems.Add(New CartItem With {.Name = name, .Price = price})

        Dim lblItem As New Label()
        lblItem.Text = name & "   ->   ₱" & price
        lblItem.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular)
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

                lastOrderItems = New List(Of CartItem)(cartItems)
                lastOrderTotal = grandTotal
                lastOrderCustomer = customerName
                lastOrderDate = DateTime.Now
                lastOrderCompleted = True

                Dim receiptMessage As String = "==================================" & vbCrLf &
                                               "  THANKS FOR BUYING FROM UKAYHUB! " & vbCrLf &
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
                cartItems.Clear()

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
    Private Sub btnClearCart_Click(sender As Object, e As EventArgs) Handles btnClearCart.Click
        If cartItemIDs.Count = 0 AndAlso flpCartItems.Controls.Count = 0 Then
            MessageBox.Show("Your shopping cart is currently empty.", "Clear Cart", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to clear all items from your cart?", "Confirm Clear Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirm = DialogResult.Yes Then
            grandTotal = 0
            cartItemIDs.Clear()
            cartItems.Clear()

            flpCartItems.Controls.Clear()
            lblTotalAmount.Text = "₱0"
            pnlCart.Visible = False

            MessageBox.Show("Your cart is now clean!", "Cart Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Dim lastOrderItems As New List(Of CartItem)()
    Dim lastOrderTotal As Integer = 0
    Dim lastOrderCustomer As String = ""
    Dim lastOrderDate As DateTime
    Dim lastOrderCompleted As Boolean = False

    Public Structure CartItem
        Public Name As String
        Public Price As Integer
    End Structure
    Private Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        If Not lastOrderCompleted OrElse lastOrderItems.Count = 0 Then
            MessageBox.Show("There are no completed orders to print a receipt.", "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim downloadsPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
            Dim receiptFolder As String = Path.Combine(downloadsPath, "UkayHub_Reports", "Receipts")
            If Not Directory.Exists(receiptFolder) Then
                Directory.CreateDirectory(receiptFolder)
            End If

            Dim fileName As String = "Receipt_" & lastOrderDate.ToString("yyyyMMdd_HHmmss") & ".pdf"
            Dim savePath As String = Path.Combine(receiptFolder, fileName)

            'PDF Document
            Dim doc As New Document(New iTextSharp.text.Rectangle(300, 550), 20, 20, 20, 20)
            PdfWriter.GetInstance(doc, New FileStream(savePath, FileMode.Create))
            doc.Open()

            ' Fonts
            Dim storeFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, New BaseColor(64, 30, 12))
            Dim subFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, New BaseColor(100, 100, 100))
            Dim labelFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, New BaseColor(139, 0, 0))
            Dim itemFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)
            Dim totalFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD, New BaseColor(139, 0, 0))
            Dim thanksFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.ITALIC, New BaseColor(212, 160, 23))

            ' Logo
            Try
                Using ms As New MemoryStream()
                    Resource1.ukayhub_logo.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    Dim logoImg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms.ToArray())
                    logoImg.ScaleToFit(55.0F, 55.0F)
                    logoImg.Alignment = Element.ALIGN_CENTER
                    doc.Add(logoImg)
                End Using
            Catch

            End Try

            ' Header
            Dim titlePar As New Paragraph("UKAYHUB", storeFont)
            titlePar.Alignment = Element.ALIGN_CENTER
            doc.Add(titlePar)

            Dim subPar As New Paragraph("Store Management System", subFont)
            subPar.Alignment = Element.ALIGN_CENTER
            doc.Add(subPar)

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(New String("-"c, 64)) With {.Alignment = Element.ALIGN_CENTER})

            ' Customer
            doc.Add(New Paragraph("Customer: " & lastOrderCustomer, itemFont))
            doc.Add(New Paragraph("Date: " & lastOrderDate.ToString("yyyy-MM-dd HH:mm:ss"), itemFont))
            doc.Add(New Paragraph(New String("-"c, 64)) With {.Alignment = Element.ALIGN_CENTER})
            doc.Add(New Paragraph(" "))

            ' Items 
            Dim itemsTable As New PdfPTable(2)
            itemsTable.WidthPercentage = 100
            itemsTable.SetWidths(New Single() {65, 35})

            itemsTable.AddCell(New PdfPCell(New Phrase("Item", labelFont)) With {.Border = iTextSharp.text.Rectangle.NO_BORDER, .PaddingBottom = 4})
            itemsTable.AddCell(New PdfPCell(New Phrase("Price", labelFont)) With {.Border = iTextSharp.text.Rectangle.NO_BORDER, .HorizontalAlignment = Element.ALIGN_RIGHT, .PaddingBottom = 4})

            For Each item In lastOrderItems
                itemsTable.AddCell(New PdfPCell(New Phrase(item.Name, itemFont)) With {.Border = iTextSharp.text.Rectangle.NO_BORDER, .PaddingBottom = 3})
                itemsTable.AddCell(New PdfPCell(New Phrase("₱" & item.Price.ToString("N2"), itemFont)) With {.Border = iTextSharp.text.Rectangle.NO_BORDER, .HorizontalAlignment = Element.ALIGN_RIGHT, .PaddingBottom = 3})
            Next

            doc.Add(itemsTable)

            doc.Add(New Paragraph(New String("-"c, 64)) With {.Alignment = Element.ALIGN_CENTER})

            Dim totalPar As New Paragraph("TOTAL: ₱" & lastOrderTotal.ToString("N2"), totalFont)
            totalPar.Alignment = Element.ALIGN_RIGHT
            totalPar.SpacingBefore = 6
            doc.Add(totalPar)

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(New String("-"c, 64)) With {.Alignment = Element.ALIGN_CENTER})

            ' Footer
            Dim thanksPar As New Paragraph("Thank you for shopping with us!", thanksFont)
            thanksPar.Alignment = Element.ALIGN_CENTER
            thanksPar.SpacingBefore = 10
            doc.Add(thanksPar)

            doc.Close()

            Dim result As DialogResult = MessageBox.Show("Receipt saved to:" & vbCrLf & savePath & vbCrLf & vbCrLf & "Open now?", "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If result = DialogResult.Yes Then
                Process.Start(New ProcessStartInfo(savePath) With {.UseShellExecute = True})
            End If

        Catch ex As Exception
            MessageBox.Show("Receipt Generation Error: " & ex.Message, "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class