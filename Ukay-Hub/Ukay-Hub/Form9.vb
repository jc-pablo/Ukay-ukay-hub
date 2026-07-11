Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MySql.Data.MySqlClient
Imports Ukay_Hub.My.Resources

Public Class Form9
    Dim conn As New MySqlConnection("server=localhost;user=root;password=root;database=ukayukay_db")
    Public sql As String
    Public dbcomm As MySqlCommand
    Public DataAdapter1 As MySqlDataAdapter
    Public ds As DataSet

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardMetrics()
        LoadDailySalesLog()
    End Sub

    Public Sub LoadDashboardMetrics()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            sql = "SELECT SUM(selling_price) FROM transactions WHERE DATE(sale_date) = CURDATE()"
            dbcomm = New MySqlCommand(sql, conn)
            Dim todayRes As Object = dbcomm.ExecuteScalar()
            lblTodaySales.Text = "₱" & Val(todayRes.ToString()).ToString("N2")

            sql = "SELECT COUNT(transaction_id) FROM transactions WHERE DATE(sale_date) = CURDATE()"
            dbcomm = New MySqlCommand(sql, conn)
            Dim todayCountRes As Object = dbcomm.ExecuteScalar()

            If todayCountRes Is Nothing OrElse todayCountRes.ToString() = "" Then
                Label24.Text = "0 items sold"
            Else
                Label24.Text = todayCountRes.ToString() & " items sold"
            End If

            sql = "SELECT SUM(selling_price) FROM transactions WHERE YEARWEEK(sale_date, 1) = YEARWEEK(CURDATE(), 1)"
            dbcomm = New MySqlCommand(sql, conn)
            Dim weekRes As Object = dbcomm.ExecuteScalar()
            lblThisWeekSales.Text = "₱" & Val(weekRes.ToString()).ToString("N2")

            sql = "SELECT COUNT(transaction_id) FROM transactions WHERE YEARWEEK(sale_date, 1) = YEARWEEK(CURDATE(), 1)"
            dbcomm = New MySqlCommand(sql, conn)
            Dim weekCountRes As Object = dbcomm.ExecuteScalar()

            If weekCountRes Is Nothing OrElse weekCountRes.ToString() = "" Then
                Label27.Text = "0 items sold"
            Else
                Label27.Text = weekCountRes.ToString() & " items sold"
            End If

            sql = "SELECT AVG(daily_total) FROM (SELECT SUM(selling_price) As daily_total FROM transactions GROUP BY DATE(sale_date)) As temp"
            dbcomm = New MySqlCommand(sql, conn)
            Dim avgRes As Object = dbcomm.ExecuteScalar()
            lblAvgPerDay.Text = "₱" & Val(avgRes.ToString()).ToString("N2")

            sql = "SELECT DATE_FORMAT(MAX(sale_date), '%W') FROM transactions GROUP BY DATE(sale_date) ORDER BY SUM(selling_price) DESC LIMIT 1"
            dbcomm = New MySqlCommand(sql, conn)
            Dim bestDay As Object = dbcomm.ExecuteScalar()

            sql = "SELECT MAX(daily_total) FROM (SELECT SUM(selling_price) as daily_total FROM transactions GROUP BY DATE(sale_date)) as temp"
            dbcomm = New MySqlCommand(sql, conn)
            Dim bestDayTotal As Object = dbcomm.ExecuteScalar()

            If bestDay Is Nothing OrElse bestDay.ToString() = "" Then
                lblBestDay.Text = "No Sales"
                Label33.Text = "₱0.00"
            Else
                lblBestDay.Text = bestDay.ToString()
                Label33.Text = "₱" & Val(bestDayTotal.ToString()).ToString("N2")
            End If
            dgvDailySalesLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Public Sub LoadDailySalesLog()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            sql = "SELECT DATE(t.sale_date) As 'Date', COUNT(t.transaction_id) As 'Items Sold', SUM(t.selling_price) As 'Total Sales' " &
                  "FROM transactions t GROUP BY DATE(t.sale_date) ORDER BY DATE(t.sale_date) DESC"

            DataAdapter1 = New MySqlDataAdapter(sql, conn)
            ds = New DataSet()
            DataAdapter1.Fill(ds, "DailyLog")
            dgvDailySalesLog.DataSource = ds
            dgvDailySalesLog.DataMember = "DailyLog"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
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

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
        Dim frm10 As New Form10()
        frm10.Show()
        Me.Hide()
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
        Dim frm11 As New Form11()
        frm11.Show()
        Me.Hide()
    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click
        Dim frm12 As New Form12()
        frm12.Show()
        Me.Hide()
    End Sub

    Private Sub btnPrintReport_Click(sender As Object, e As EventArgs) Handles btnPrintReport.Click
        Dim thisPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
        Dim repFolder As String = Path.Combine(thisPath, "UkayHub_Reports")
        Dim assetsFolder As String = Path.Combine(repFolder, "Assets")
        If Not Directory.Exists(repFolder) Then
            Directory.CreateDirectory(repFolder)
        End If
        If Not Directory.Exists(assetsFolder) Then
            Directory.CreateDirectory(assetsFolder)
        End If

        Dim savePath As String = Path.Combine(repFolder, "UkayHub_Reports.pdf")

        ' pdf
        Dim doc As New Document(PageSize.A4, 40, 40, 50, 50)
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(savePath, FileMode.Create))

        doc.Open()

        ' Fonts
        Dim mainTitleFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, New BaseColor(64, 30, 12))
        Dim subHeaderFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, New BaseColor(100, 100, 100))
        Dim sectionHeaderFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 13, iTextSharp.text.Font.BOLD, New BaseColor(139, 0, 0))
        Dim tableHeaderFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, BaseColor.White)
        Dim cellFont As iTextSharp.text.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL)

        Dim dateFrom As String = ""
        Dim dateTo As String = ""
        Dim reportDate As String = DateTime.Now.ToString("yyyy-MM-dd")
        Dim reportTime As String = DateTime.Now.ToString("hh:mm tt")

        ' header
        Dim headerTable As New PdfPTable(2)
        headerTable.WidthPercentage = 100
        headerTable.SetWidths(New Single() {1.5F, 8.5F})
        headerTable.SpacingAfter = 15.0F

        Dim logoCell As New PdfPCell()
        logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER
        logoCell.VerticalAlignment = Element.ALIGN_MIDDLE
        Try
            Using ms As New MemoryStream()
                Resource1.ukayhub_logo.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim logoImg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms.ToArray())
                logoImg.ScaleToFit(65.0F, 65.0F)
                logoCell.AddElement(logoImg)
            End Using
        Catch
            logoCell.AddElement(New Paragraph(""))
        End Try
        headerTable.AddCell(logoCell)

        Dim textCell As New PdfPCell()
        textCell.Border = iTextSharp.text.Rectangle.NO_BORDER
        textCell.VerticalAlignment = Element.ALIGN_MIDDLE
        textCell.AddElement(New Paragraph("UKAYHUB STORE MANAGEMENT SYSTEM", mainTitleFont))
        textCell.AddElement(New Paragraph("FINANCIAL REVENUE REPORT", subHeaderFont))
        textCell.AddElement(New Paragraph("Report As of: " & reportDate & " at " & reportTime, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.ITALIC)))

        headerTable.AddCell(textCell)

        doc.Add(headerTable)
        doc.Add(New Paragraph(New String("-"c, 128)) With {.Alignment = Element.ALIGN_CENTER})
        doc.Add(New Paragraph(vbCrLf))

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            ' DAILY SALES LOG

            doc.Add(New Paragraph("Daily Sales Performance Log", sectionHeaderFont))
            doc.Add(New Paragraph(vbCrLf))

            Dim t1 As New PdfPTable(3)
            t1.WidthPercentage = 100
            t1.SetWidths(New Single() {40, 30, 30})

            t1.AddCell(New PdfPCell(New Phrase("Date", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t1.AddCell(New PdfPCell(New Phrase("Items Sold", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t1.AddCell(New PdfPCell(New Phrase("Total Sales", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})

            sql = "SELECT DATE(sale_date) As 'Date', 
                   COUNT(transaction_id) As 'ItemsSold',        
                   SUM(selling_price) As 'TotalSales' 
                   FROM transactions GROUP BY DATE(sale_date) ORDER BY DATE(sale_date) DESC"
            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        t1.AddCell(New PdfPCell(New Phrase(Convert.ToDateTime(reader("Date")).ToString("yyyy-MM-dd"), cellFont)) With {.Padding = 5})
                        t1.AddCell(New PdfPCell(New Phrase(reader("ItemsSold").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 5})
                        t1.AddCell(New PdfPCell(New Phrase("₱" & Convert.ToDecimal(reader("TotalSales")).ToString("N2"), cellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Padding = 5})
                    End While
                End Using
            End Using
            doc.Add(t1)
            doc.Add(New Paragraph(vbCrLf))

            ' TOP SELLING CATEGORIES 

            doc.Add(New Paragraph("Top Selling Categories & Revenue Share Visualizer", sectionHeaderFont))
            doc.Add(New Paragraph(vbCrLf))

            Dim grandTotalSales As Decimal = 0
            sql = "SELECT IFNULL(SUM(selling_price), 0) FROM transactions"
            Using cmdTotal As New MySqlCommand(sql, conn)
                grandTotalSales = Convert.ToDecimal(cmdTotal.ExecuteScalar())
            End Using

            Dim t2 As New PdfPTable(5)
            t2.WidthPercentage = 100
            t2.SetWidths(New Single() {8, 32, 17, 22, 21})

            t2.AddCell(New PdfPCell(New Phrase("Rank", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t2.AddCell(New PdfPCell(New Phrase("Category", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0)})
            t2.AddCell(New PdfPCell(New Phrase("Items Sold", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t2.AddCell(New PdfPCell(New Phrase("Total Revenue", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t2.AddCell(New PdfPCell(New Phrase("% Share", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})

            sql = "SELECT c.category_name As 'Category', COUNT(t.transaction_id) As 'ItemSold', SUM(t.selling_price) As 'TotalRevenue' " &
          "FROM transactions t INNER JOIN inventory i ON t.item_id = i.item_id " &
          "INNER JOIN categories c ON i.category_id = c.category_id " &
          "GROUP BY c.category_name ORDER BY SUM(t.selling_price) DESC"

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    Dim currentRank As Integer = 1
                    While reader.Read()
                        Dim rev As Decimal = Convert.ToDecimal(reader("TotalRevenue"))
                        Dim pct As Single = If(grandTotalSales > 0, CSng((rev / grandTotalSales) * 100), 0.0F)

                        t2.AddCell(New PdfPCell(New Phrase(currentRank.ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t2.AddCell(New PdfPCell(New Phrase(reader("Category").ToString(), cellFont)) With {.Padding = 4})
                        t2.AddCell(New PdfPCell(New Phrase(reader("ItemSold").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t2.AddCell(New PdfPCell(New Phrase("₱" & rev.ToString("N2"), cellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Padding = 4})
                        t2.AddCell(New PdfPCell(New Phrase(pct.ToString("F2") & "%", cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})

                        currentRank += 1
                    End While
                End Using
            End Using
            doc.Add(t2)
            doc.Add(New Paragraph(vbCrLf))

            ' INVENTORY CATEGORY BREAKDOWN

            doc.Add(New Paragraph("Stock & Inventory Category Breakdown", sectionHeaderFont))
            doc.Add(New Paragraph(vbCrLf))

            Dim t3 As New PdfPTable(5)
            t3.WidthPercentage = 100
            t3.SetWidths(New Single() {32, 17, 17, 17, 17})

            t3.AddCell(New PdfPCell(New Phrase("Category", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0)})
            t3.AddCell(New PdfPCell(New Phrase("Available", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t3.AddCell(New PdfPCell(New Phrase("Sold", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t3.AddCell(New PdfPCell(New Phrase("Reserved", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t3.AddCell(New PdfPCell(New Phrase("Total Items", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})

            sql = "SELECT c.category_name, SUM(IF(i.status = 'Available', 1, 0)) As 'Avail', SUM(IF(i.status = 'Sold', 1, 0)) As 'Sld', " &
          "SUM(IF(i.status = 'Reserved', 1, 0)) As 'Res', COUNT(i.item_id) As 'Tot' " &
          "FROM categories c LEFT JOIN inventory i ON c.category_id = i.category_id GROUP BY c.category_name"

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        t3.AddCell(New PdfPCell(New Phrase(reader("category_name").ToString(), cellFont)) With {.Padding = 4})
                        t3.AddCell(New PdfPCell(New Phrase(reader("Avail").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t3.AddCell(New PdfPCell(New Phrase(reader("Sld").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t3.AddCell(New PdfPCell(New Phrase(reader("Res").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t3.AddCell(New PdfPCell(New Phrase(reader("Tot").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                    End While
                End Using
            End Using
            doc.Add(t3)
            doc.Add(New Paragraph(vbCrLf))

            ' CONSIGNOR EARNINGS REPORT

            doc.Add(New Paragraph("Consignor Financial Earnings Status", sectionHeaderFont))
            doc.Add(New Paragraph(vbCrLf))

            Dim t4 As New PdfPTable(6)
            t4.WidthPercentage = 100
            t4.SetWidths(New Single() {25, 12, 16, 11, 16, 20})

            t4.AddCell(New PdfPCell(New Phrase("Consignor Name", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0)})
            t4.AddCell(New PdfPCell(New Phrase("Items Sold", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t4.AddCell(New PdfPCell(New Phrase("Total Sales", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t4.AddCell(New PdfPCell(New Phrase("Rate", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t4.AddCell(New PdfPCell(New Phrase("Total Earned", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})
            t4.AddCell(New PdfPCell(New Phrase("Last Payout Date", tableHeaderFont)) With {.BackgroundColor = New BaseColor(139, 0, 0), .HorizontalAlignment = Element.ALIGN_CENTER})

            sql = "SELECT CONCAT(c.first_name, ' ', c.last_name) As 'ConsignorName', COUNT(t.transaction_id) As 'ItemSold', " &
          "SUM(t.selling_price) As 'TotalSales', SUM(t.selling_price) * 0.70 As 'TotalEarned', MAX(p.date_saved) As 'LastPayout' " &
          "FROM consignors c INNER JOIN inventory i ON c.consignor_id = i.consignor_id " &
          "INNER JOIN transactions t ON i.item_id = t.item_id LEFT JOIN payouts p ON c.consignor_id = p.consignor_id GROUP BY c.consignor_id"

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim payoutDate As String = If(IsDBNull(reader("LastPayout")), "N/A", Convert.ToDateTime(reader("LastPayout")).ToString("yyyy-MM-dd"))

                        t4.AddCell(New PdfPCell(New Phrase(reader("ConsignorName").ToString(), cellFont)) With {.Padding = 4})
                        t4.AddCell(New PdfPCell(New Phrase(reader("ItemSold").ToString(), cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t4.AddCell(New PdfPCell(New Phrase("₱" & Convert.ToDecimal(reader("TotalSales")).ToString("N2"), cellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Padding = 4})
                        t4.AddCell(New PdfPCell(New Phrase("70%", cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                        t4.AddCell(New PdfPCell(New Phrase("₱" & Convert.ToDecimal(reader("TotalEarned")).ToString("N2"), cellFont)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .Padding = 4})
                        t4.AddCell(New PdfPCell(New Phrase(payoutDate, cellFont)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .Padding = 4})
                    End While
                End Using
            End Using
            doc.Add(t4)

        Catch ex As Exception
            MsgBox("PDF Generation Error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            conn.Close()
        End Try

        doc.Close()
        Try
            Dim isDocumentOpen As Boolean = False

            Dim result As DialogResult = MessageBox.Show("Reports saved to:" & vbCrLf & savePath & vbCrLf & vbCrLf & "Open now?", "UkayHub Reports", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If result = DialogResult.Yes Then
                Process.Start(New ProcessStartInfo(savePath) With {.UseShellExecute = True})
            End If

        Catch ex As Exception
            MessageBox.Show("PDF Generation Error: " & ex.Message, "Print Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
End Class