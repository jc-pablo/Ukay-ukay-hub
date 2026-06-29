Public Class ucProductCard
    Public Property ItemName As String
        Get
            Return lblTitle.Text
        End Get
        Set(value As String)
            lblTitle.Text = value
        End Set
    End Property

    Public Property Price As Integer
        Get
            Return Convert.ToInt32(lblPrice.Text.Replace("₱", ""))
        End Get
        Set(value As Integer)
            lblPrice.Text = "₱" & value.ToString()
        End Set
    End Property

    Public Event AddToCartClicked(sender As Object, name As String, price As Integer)

    Private Sub btnAddToCart_Click(sender As Object, e As EventArgs) Handles btnAddToCart.Click
        RaiseEvent AddToCartClicked(Me, ItemName, Price)
    End Sub
End Class
