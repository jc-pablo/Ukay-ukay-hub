<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form14
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form14))
        Me.flpProducts = New System.Windows.Forms.FlowLayoutPanel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.lblCart = New System.Windows.Forms.LinkLabel()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.pnlCart = New System.Windows.Forms.Panel()
        Me.lblCartTitle = New System.Windows.Forms.Label()
        Me.flpCartItems = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblTotaltext = New System.Windows.Forms.Label()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.btnCheckout = New System.Windows.Forms.Button()
        Me.pnlCart.SuspendLayout()
        Me.SuspendLayout()
        '
        'flpProducts
        '
        Me.flpProducts.AutoScroll = True
        Me.flpProducts.BackgroundImage = CType(resources.GetObject("flpProducts.BackgroundImage"), System.Drawing.Image)
        Me.flpProducts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpProducts.Location = New System.Drawing.Point(3, 182)
        Me.flpProducts.Name = "flpProducts"
        Me.flpProducts.Size = New System.Drawing.Size(1919, 877)
        Me.flpProducts.TabIndex = 0
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkColor = System.Drawing.Color.RosyBrown
        Me.LinkLabel1.Location = New System.Drawing.Point(1804, 115)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(68, 24)
        Me.LinkLabel1.TabIndex = 43
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Logout"
        '
        'lblCart
        '
        Me.lblCart.ActiveLinkColor = System.Drawing.Color.White
        Me.lblCart.AutoSize = True
        Me.lblCart.BackColor = System.Drawing.Color.Transparent
        Me.lblCart.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCart.LinkColor = System.Drawing.Color.RosyBrown
        Me.lblCart.Location = New System.Drawing.Point(1709, 115)
        Me.lblCart.Name = "lblCart"
        Me.lblCart.Size = New System.Drawing.Size(43, 24)
        Me.lblCart.TabIndex = 44
        Me.lblCart.TabStop = True
        Me.lblCart.Text = "Cart"
        '
        'TextBox5
        '
        Me.TextBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(107, 115)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(875, 38)
        Me.TextBox5.TabIndex = 56
        '
        'pnlCart
        '
        Me.pnlCart.Controls.Add(Me.btnCheckout)
        Me.pnlCart.Controls.Add(Me.lblTotalAmount)
        Me.pnlCart.Controls.Add(Me.lblTotaltext)
        Me.pnlCart.Controls.Add(Me.flpCartItems)
        Me.pnlCart.Controls.Add(Me.lblCartTitle)
        Me.pnlCart.Location = New System.Drawing.Point(1521, 173)
        Me.pnlCart.Name = "pnlCart"
        Me.pnlCart.Size = New System.Drawing.Size(401, 886)
        Me.pnlCart.TabIndex = 57
        Me.pnlCart.Visible = False
        '
        'lblCartTitle
        '
        Me.lblCartTitle.AutoSize = True
        Me.lblCartTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblCartTitle.Font = New System.Drawing.Font("Georgia", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCartTitle.ForeColor = System.Drawing.Color.Maroon
        Me.lblCartTitle.Location = New System.Drawing.Point(20, 21)
        Me.lblCartTitle.Name = "lblCartTitle"
        Me.lblCartTitle.Size = New System.Drawing.Size(195, 41)
        Me.lblCartTitle.TabIndex = 58
        Me.lblCartTitle.Text = "Your Cart"
        '
        'flpCartItems
        '
        Me.flpCartItems.AutoScroll = True
        Me.flpCartItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpCartItems.Location = New System.Drawing.Point(6, 68)
        Me.flpCartItems.Name = "flpCartItems"
        Me.flpCartItems.Size = New System.Drawing.Size(394, 674)
        Me.flpCartItems.TabIndex = 59
        '
        'lblTotaltext
        '
        Me.lblTotaltext.AutoSize = True
        Me.lblTotaltext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotaltext.ForeColor = System.Drawing.Color.Black
        Me.lblTotaltext.Location = New System.Drawing.Point(12, 759)
        Me.lblTotaltext.Name = "lblTotaltext"
        Me.lblTotaltext.Size = New System.Drawing.Size(56, 24)
        Me.lblTotaltext.TabIndex = 60
        Me.lblTotaltext.Text = "Total:"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = True
        Me.lblTotalAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.ForeColor = System.Drawing.Color.DarkRed
        Me.lblTotalAmount.Location = New System.Drawing.Point(64, 759)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(35, 24)
        Me.lblTotalAmount.TabIndex = 61
        Me.lblTotalAmount.Text = "₱0"
        '
        'btnCheckout
        '
        Me.btnCheckout.BackColor = System.Drawing.Color.Firebrick
        Me.btnCheckout.Font = New System.Drawing.Font("Nirmala Text", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckout.ForeColor = System.Drawing.Color.White
        Me.btnCheckout.Location = New System.Drawing.Point(16, 786)
        Me.btnCheckout.Name = "btnCheckout"
        Me.btnCheckout.Size = New System.Drawing.Size(153, 50)
        Me.btnCheckout.TabIndex = 62
        Me.btnCheckout.Text = "Checkout "
        Me.btnCheckout.UseVisualStyleBackColor = False
        '
        'Form14
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1924, 1061)
        Me.Controls.Add(Me.pnlCart)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.lblCart)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.flpProducts)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MinimizeBox = False
        Me.Name = "Form14"
        Me.Text = "Form14"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCart.ResumeLayout(False)
        Me.pnlCart.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents flpProducts As FlowLayoutPanel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents lblCart As LinkLabel
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents pnlCart As Panel
    Friend WithEvents lblCartTitle As Label
    Friend WithEvents flpCartItems As FlowLayoutPanel
    Friend WithEvents lblTotalAmount As Label
    Friend WithEvents lblTotaltext As Label
    Friend WithEvents btnCheckout As Button
End Class
