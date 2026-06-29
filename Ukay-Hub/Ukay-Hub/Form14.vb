Public Class Form14

    Dim grandTotal As Integer = 0

    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Pagkabukas ng shop, automatic na hihilahin ang laman ng database
        LoadProductsToShop()

        lblTotalAmount.Text = "₱0"
    End Sub

    Public Sub LoadProductsToShop()
        ' 1. Linisin muna ang panel para walang maiwang luma kapag nag-refresh
        flpProducts.Controls.Clear()

        ' =========================================================================
        ' ANG SQL SETUP NG PARTNER MO (Dito niya isasaksak ang query niya)
        ' =========================================================================
        ' Dim dt As New DataTable()
        ' Dim query As String = "SELECT item_name, price FROM tbl_items WHERE status = 'Available'"
        ' (Dito rin ilalagay yung MySqlDataAdapter o SqlDataAdapter para mapuno ang 'dt')
        ' =========================================================================

        ' 2. PANSAMANTALANG LOGIC HABANG WALA PANG MGA ROWS ANG TABLE:
        ' (Kapag nilagyan na ito ng partner mo ng database connection, papalitan niya lang ito ng 'If dt.Rows.Count = 0 Then')
        If True Then
            Dim lblEmpty As New Label()
            lblEmpty.Text = "The shop is currently empty. No items found in the database."
            lblEmpty.Font = New Font("Segoe UI", 14, FontStyle.Italic)
            lblEmpty.ForeColor = Color.Gray
            lblEmpty.AutoSize = True
            flpProducts.Controls.Add(lblEmpty)
            Return
        End If

        ' 3. ANG DYNAMIC CONNECT: Loop para sa bawat row na magmumula sa database
        ' Ito lang ang kailangan mong isulat para awtomatikong mag-duplicate ang ucProductCard mo!

        ' For Each row As DataRow In dt.Rows
        '     ' Kusa itong gagawa ng bagong card kada linya/row sa database
        '     Dim card As New ucProductCard()
        '
        '     ' I-pasa ang data mula sa mga columns ng database niyo papunta sa card properties
        '     card.ItemName = row("item_name").ToString()
        '     card.Price = Convert.ToInt32(row("price"))
        '
        '     ' Isalpak ang card sa FlowLayoutPanel ng shop mo
        '     flpProducts.Controls.Add(card)
        ' Next

    End Sub

    Private Sub PagMayNagAddToCart(sender As Object, name As String, price As Integer)
        ' HAKBANG A: Kusang ilabas at paibabawin ang cart panel sa kanan
        pnlCart.Visible = True
        pnlCart.BringToFront()

        ' HAKBANG B: Gumawa ng bagong text line para sa item na binili
        Dim lblItem As New Label()
        lblItem.Text = name & "   ->   ₱" & price
        lblItem.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblItem.AutoSize = True

        ' HAKBANG C: Isalpak ang ginawang text line sa loob ng flpCartItems (kusa itong bababa)
        flpCartItems.Controls.Add(lblItem)


        ' HAKBANG D: ANG FORMULA NG PAG-ADD SA TOTAL
        ' Kung magkano ang dating grandTotal, idadagdag lang ang presyo ng bagong item
        grandTotal = grandTotal + price

        ' HAKBANG E: I-display ang bagong kabuuang halaga sa iyong label
        lblTotalAmount.Text = "₱" & grandTotal.ToString()
    End Sub

    ' Para sa LinkLabel ng cart mo
    Private Sub lblCart_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCart.LinkClicked
        If pnlCart.Visible = False Then
            pnlCart.Visible = True
            pnlCart.BringToFront()
        Else
            pnlCart.Visible = False
        End If
    End Sub
End Class