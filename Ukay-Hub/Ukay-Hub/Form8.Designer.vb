<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form8
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form8))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbConsignors = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCompute = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSavePayoutRecord = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblRcptNameDate = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblRcptTotalPayout = New System.Windows.Forms.Label()
        Me.lblRcptItemsSold = New System.Windows.Forms.Label()
        Me.lblRcptTotalSales = New System.Windows.Forms.Label()
        Me.lblRcptCommRate = New System.Windows.Forms.Label()
        Me.dgvUnpaidItems = New System.Windows.Forms.DataGridView()
        Me.colCnNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnSalePrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnCommission = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTrDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.dgvPayoutHistory = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnConsignor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnTotalSales = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnPayout = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCnDateSaved = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvUnpaidItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPayoutHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(59, 163)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(274, 569)
        Me.Panel1.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.LightGray
        Me.Label16.Location = New System.Drawing.Point(27, 521)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(49, 13)
        Me.Label16.TabIndex = 15
        Me.Label16.Text = "Analytics"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(26, 497)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 24)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Reports"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(28, 452)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 13)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Consignor Earnings"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(27, 428)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(83, 24)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Payouts"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.LightGray
        Me.Label12.Location = New System.Drawing.Point(28, 380)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Record Sales"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(25, 356)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(129, 24)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Transactions"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.LightGray
        Me.Label10.Location = New System.Drawing.Point(26, 313)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Item Management"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(27, 289)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 24)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Inventory"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.LightGray
        Me.Label8.Location = New System.Drawing.Point(27, 244)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Manage Consignors"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(26, 220)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 24)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Consignors"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.LightGray
        Me.Label6.Location = New System.Drawing.Point(26, 178)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Manage Donors"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(26, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 24)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Donors"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.LightGray
        Me.Label4.Location = New System.Drawing.Point(27, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Manage Categories"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(25, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Categories"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.LightGray
        Me.Label2.Location = New System.Drawing.Point(26, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Overview"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(25, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dashboard"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.RosyBrown
        Me.Label17.Location = New System.Drawing.Point(399, 40)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(73, 20)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Payouts"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkColor = System.Drawing.Color.RosyBrown
        Me.LinkLabel1.Location = New System.Drawing.Point(1830, 36)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(68, 24)
        Me.LinkLabel1.TabIndex = 8
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Logout"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.SandyBrown
        Me.Label19.Location = New System.Drawing.Point(411, 188)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(182, 18)
        Me.Label19.TabIndex = 27
        Me.Label19.Text = "CONSIGNOR EARNINGS"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Georgia", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Label18.Location = New System.Drawing.Point(408, 154)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(133, 34)
        Me.Label18.TabIndex = 26
        Me.Label18.Text = "Payouts"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Chocolate
        Me.Label26.Location = New System.Drawing.Point(820, 382)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(223, 15)
        Me.Label26.TabIndex = 44
        Me.Label26.Text = "------- Item Sold (Selected Period)"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Chocolate
        Me.Label21.Location = New System.Drawing.Point(425, 382)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(164, 15)
        Me.Label21.TabIndex = 43
        Me.Label21.Text = "------- Compute Earnings"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Georgia", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(422, 307)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(294, 34)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "Consignor Payouts"
        '
        'cmbConsignors
        '
        Me.cmbConsignors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbConsignors.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbConsignors.FormattingEnabled = True
        Me.cmbConsignors.Location = New System.Drawing.Point(428, 437)
        Me.cmbConsignors.Name = "cmbConsignors"
        Me.cmbConsignors.Size = New System.Drawing.Size(353, 28)
        Me.cmbConsignors.TabIndex = 63
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.DimGray
        Me.Label29.Location = New System.Drawing.Point(425, 418)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(159, 16)
        Me.Label29.TabIndex = 62
        Me.Label29.Text = "SELECT CONSIGNOR"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.DimGray
        Me.Label24.Location = New System.Drawing.Point(613, 476)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 16)
        Me.Label24.TabIndex = 75
        Me.Label24.Text = "DATE TO"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.DimGray
        Me.Label30.Location = New System.Drawing.Point(425, 476)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(95, 16)
        Me.Label30.TabIndex = 73
        Me.Label30.Text = "DATE FROM"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.Linen
        Me.btnClear.Font = New System.Drawing.Font("Nirmala Text", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.Black
        Me.btnClear.Location = New System.Drawing.Point(590, 543)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(119, 50)
        Me.btnClear.TabIndex = 78
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnCompute
        '
        Me.btnCompute.BackColor = System.Drawing.Color.DimGray
        Me.btnCompute.Font = New System.Drawing.Font("Nirmala Text", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompute.ForeColor = System.Drawing.Color.White
        Me.btnCompute.Location = New System.Drawing.Point(428, 543)
        Me.btnCompute.Name = "btnCompute"
        Me.btnCompute.Size = New System.Drawing.Size(156, 50)
        Me.btnCompute.TabIndex = 77
        Me.btnCompute.Text = "Compute"
        Me.btnCompute.UseVisualStyleBackColor = False
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.DimGray
        Me.Label32.Location = New System.Drawing.Point(425, 599)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(352, 16)
        Me.Label32.TabIndex = 79
        Me.Label32.Text = "---------------------------------------------------------------------"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(421, 618)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(360, 323)
        Me.PictureBox1.TabIndex = 80
        Me.PictureBox1.TabStop = False
        '
        'btnSavePayoutRecord
        '
        Me.btnSavePayoutRecord.BackColor = System.Drawing.Color.DimGray
        Me.btnSavePayoutRecord.Font = New System.Drawing.Font("Nirmala Text", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSavePayoutRecord.ForeColor = System.Drawing.Color.White
        Me.btnSavePayoutRecord.Location = New System.Drawing.Point(428, 936)
        Me.btnSavePayoutRecord.Name = "btnSavePayoutRecord"
        Me.btnSavePayoutRecord.Size = New System.Drawing.Size(353, 50)
        Me.btnSavePayoutRecord.TabIndex = 81
        Me.btnSavePayoutRecord.Text = "Save Payout Record"
        Me.btnSavePayoutRecord.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.DimGray
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.PeachPuff
        Me.Label22.Location = New System.Drawing.Point(502, 696)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(150, 15)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "EARNINGS SUMMARY"
        '
        'lblRcptNameDate
        '
        Me.lblRcptNameDate.AutoSize = True
        Me.lblRcptNameDate.BackColor = System.Drawing.Color.DimGray
        Me.lblRcptNameDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRcptNameDate.ForeColor = System.Drawing.Color.PeachPuff
        Me.lblRcptNameDate.Location = New System.Drawing.Point(502, 719)
        Me.lblRcptNameDate.Name = "lblRcptNameDate"
        Me.lblRcptNameDate.Size = New System.Drawing.Size(82, 13)
        Me.lblRcptNameDate.TabIndex = 83
        Me.lblRcptNameDate.Text = "Name -- Date"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.DimGray
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.SeaShell
        Me.Label25.Location = New System.Drawing.Point(502, 748)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(66, 13)
        Me.Label25.TabIndex = 84
        Me.Label25.Text = "Items Sold"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.DimGray
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.SeaShell
        Me.Label27.Location = New System.Drawing.Point(502, 773)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(71, 13)
        Me.Label27.TabIndex = 85
        Me.Label27.Text = "Total Sales"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.DimGray
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.SeaShell
        Me.Label28.Location = New System.Drawing.Point(502, 796)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(103, 13)
        Me.Label28.TabIndex = 86
        Me.Label28.Text = "Commission Rate"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.DimGray
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.LemonChiffon
        Me.Label31.Location = New System.Drawing.Point(502, 821)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(86, 15)
        Me.Label31.TabIndex = 87
        Me.Label31.Text = "Total Payout"
        '
        'lblRcptTotalPayout
        '
        Me.lblRcptTotalPayout.AutoSize = True
        Me.lblRcptTotalPayout.BackColor = System.Drawing.Color.DimGray
        Me.lblRcptTotalPayout.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRcptTotalPayout.ForeColor = System.Drawing.Color.LemonChiffon
        Me.lblRcptTotalPayout.Location = New System.Drawing.Point(500, 848)
        Me.lblRcptTotalPayout.Name = "lblRcptTotalPayout"
        Me.lblRcptTotalPayout.Size = New System.Drawing.Size(51, 25)
        Me.lblRcptTotalPayout.TabIndex = 88
        Me.lblRcptTotalPayout.Text = "009"
        '
        'lblRcptItemsSold
        '
        Me.lblRcptItemsSold.AutoSize = True
        Me.lblRcptItemsSold.BackColor = System.Drawing.Color.DimGray
        Me.lblRcptItemsSold.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRcptItemsSold.ForeColor = System.Drawing.Color.White
        Me.lblRcptItemsSold.Location = New System.Drawing.Point(658, 746)
        Me.lblRcptItemsSold.Name = "lblRcptItemsSold"
        Me.lblRcptItemsSold.Size = New System.Drawing.Size(15, 15)
        Me.lblRcptItemsSold.TabIndex = 89
        Me.lblRcptItemsSold.Text = "0"
        '
        'lblRcptTotalSales
        '
        Me.lblRcptTotalSales.AutoSize = True
        Me.lblRcptTotalSales.BackColor = System.Drawing.Color.DimGray
        Me.lblRcptTotalSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRcptTotalSales.ForeColor = System.Drawing.Color.White
        Me.lblRcptTotalSales.Location = New System.Drawing.Point(658, 771)
        Me.lblRcptTotalSales.Name = "lblRcptTotalSales"
        Me.lblRcptTotalSales.Size = New System.Drawing.Size(15, 15)
        Me.lblRcptTotalSales.TabIndex = 90
        Me.lblRcptTotalSales.Text = "0"
        '
        'lblRcptCommRate
        '
        Me.lblRcptCommRate.AutoSize = True
        Me.lblRcptCommRate.BackColor = System.Drawing.Color.DimGray
        Me.lblRcptCommRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRcptCommRate.ForeColor = System.Drawing.Color.White
        Me.lblRcptCommRate.Location = New System.Drawing.Point(658, 796)
        Me.lblRcptCommRate.Name = "lblRcptCommRate"
        Me.lblRcptCommRate.Size = New System.Drawing.Size(27, 15)
        Me.lblRcptCommRate.TabIndex = 91
        Me.lblRcptCommRate.Text = "0%"
        '
        'dgvUnpaidItems
        '
        Me.dgvUnpaidItems.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Maroon
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnpaidItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUnpaidItems.ColumnHeadersHeight = 40
        Me.dgvUnpaidItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCnNumber, Me.colCnItem, Me.colCnSalePrice, Me.colCnCommission, Me.colTrDate})
        Me.dgvUnpaidItems.EnableHeadersVisualStyles = False
        Me.dgvUnpaidItems.Location = New System.Drawing.Point(823, 418)
        Me.dgvUnpaidItems.Name = "dgvUnpaidItems"
        Me.dgvUnpaidItems.Size = New System.Drawing.Size(1026, 231)
        Me.dgvUnpaidItems.TabIndex = 92
        '
        'colCnNumber
        '
        Me.colCnNumber.HeaderText = "#"
        Me.colCnNumber.Name = "colCnNumber"
        Me.colCnNumber.Width = 60
        '
        'colCnItem
        '
        Me.colCnItem.HeaderText = "Item "
        Me.colCnItem.Name = "colCnItem"
        Me.colCnItem.Width = 285
        '
        'colCnSalePrice
        '
        Me.colCnSalePrice.HeaderText = "Sale Price"
        Me.colCnSalePrice.Name = "colCnSalePrice"
        Me.colCnSalePrice.Width = 230
        '
        'colCnCommission
        '
        Me.colCnCommission.HeaderText = "Commission"
        Me.colCnCommission.Name = "colCnCommission"
        Me.colCnCommission.Width = 250
        '
        'colTrDate
        '
        Me.colTrDate.HeaderText = "Date"
        Me.colTrDate.Name = "colTrDate"
        Me.colTrDate.Width = 165
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Chocolate
        Me.Label37.Location = New System.Drawing.Point(820, 669)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(137, 15)
        Me.Label37.TabIndex = 93
        Me.Label37.Text = "------- Payout History"
        '
        'dgvPayoutHistory
        '
        Me.dgvPayoutHistory.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Maroon
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPayoutHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPayoutHistory.ColumnHeadersHeight = 40
        Me.dgvPayoutHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.colCnConsignor, Me.colCnPeriod, Me.colCnItems, Me.colCnTotalSales, Me.colCnPayout, Me.colCnDateSaved})
        Me.dgvPayoutHistory.EnableHeadersVisualStyles = False
        Me.dgvPayoutHistory.Location = New System.Drawing.Point(823, 696)
        Me.dgvPayoutHistory.Name = "dgvPayoutHistory"
        Me.dgvPayoutHistory.Size = New System.Drawing.Size(1026, 231)
        Me.dgvPayoutHistory.TabIndex = 94
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "#"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 60
        '
        'colCnConsignor
        '
        Me.colCnConsignor.HeaderText = "Consignor"
        Me.colCnConsignor.Name = "colCnConsignor"
        Me.colCnConsignor.Width = 220
        '
        'colCnPeriod
        '
        Me.colCnPeriod.HeaderText = "Period"
        Me.colCnPeriod.Name = "colCnPeriod"
        Me.colCnPeriod.Width = 150
        '
        'colCnItems
        '
        Me.colCnItems.HeaderText = "Items"
        Me.colCnItems.Name = "colCnItems"
        Me.colCnItems.Width = 120
        '
        'colCnTotalSales
        '
        Me.colCnTotalSales.HeaderText = "Total Sales"
        Me.colCnTotalSales.Name = "colCnTotalSales"
        Me.colCnTotalSales.Width = 175
        '
        'colCnPayout
        '
        Me.colCnPayout.HeaderText = "Payout"
        Me.colCnPayout.Name = "colCnPayout"
        Me.colCnPayout.Width = 135
        '
        'colCnDateSaved
        '
        Me.colCnDateSaved.HeaderText = "Date Saved"
        Me.colCnDateSaved.Name = "colCnDateSaved"
        Me.colCnDateSaved.Width = 135
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(425, 506)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(148, 31)
        Me.dtpFrom.TabIndex = 95
        '
        'dtpTo
        '
        Me.dtpTo.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(613, 506)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(149, 31)
        Me.dtpTo.TabIndex = 96
        '
        'Form8
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1924, 1061)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.dgvPayoutHistory)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.dgvUnpaidItems)
        Me.Controls.Add(Me.lblRcptCommRate)
        Me.Controls.Add(Me.lblRcptTotalSales)
        Me.Controls.Add(Me.lblRcptItemsSold)
        Me.Controls.Add(Me.lblRcptTotalPayout)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.lblRcptNameDate)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.btnSavePayoutRecord)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnCompute)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.cmbConsignors)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form8"
        Me.Text = "Form8"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvUnpaidItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPayoutHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents cmbConsignors As ComboBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents btnCompute As Button
    Friend WithEvents Label32 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnSavePayoutRecord As Button
    Friend WithEvents Label22 As Label
    Friend WithEvents lblRcptNameDate As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents lblRcptTotalPayout As Label
    Friend WithEvents lblRcptItemsSold As Label
    Friend WithEvents lblRcptTotalSales As Label
    Friend WithEvents lblRcptCommRate As Label
    Friend WithEvents dgvUnpaidItems As DataGridView
    Friend WithEvents Label37 As Label
    Friend WithEvents colCnNumber As DataGridViewTextBoxColumn
    Friend WithEvents colCnItem As DataGridViewTextBoxColumn
    Friend WithEvents colCnSalePrice As DataGridViewTextBoxColumn
    Friend WithEvents colCnCommission As DataGridViewTextBoxColumn
    Friend WithEvents colTrDate As DataGridViewTextBoxColumn
    Friend WithEvents dgvPayoutHistory As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents colCnConsignor As DataGridViewTextBoxColumn
    Friend WithEvents colCnPeriod As DataGridViewTextBoxColumn
    Friend WithEvents colCnItems As DataGridViewTextBoxColumn
    Friend WithEvents colCnTotalSales As DataGridViewTextBoxColumn
    Friend WithEvents colCnPayout As DataGridViewTextBoxColumn
    Friend WithEvents colCnDateSaved As DataGridViewTextBoxColumn
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
End Class
