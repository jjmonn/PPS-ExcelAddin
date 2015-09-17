﻿' SettingMainUI.vb
'
' Display settings controls
'
'
' To do: 
'       - Implement connection and reinitialize password
'       - Need to initialize GLOBAL variables when connection reinitialized ? !!!
'
'
'
' Known bugs:
'
'
'
' Last modified: 17/09/2015
' Author: Julien Monnereau


Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls
Imports VIBlend.Utilities



Friend Class SettingUI


#Region "Instance Variables"

    Private isFillingDGV As Boolean
    Private currentFormatDGVcell As GridCell


#End Region


#Region "Initialize"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeFormatsDGVDisplay()
        AddHandler FormatsDGV.CellBeginEdit, AddressOf FormatsDGV_CellBeginEdit
        AddHandler FormatsDGV.CellMouseEnter, AddressOf FormatsDGV_CellMouseEnter

    End Sub

    Private Sub SettingMainUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ServerAddressTB.Text = My.Settings.serverIp
        PortTB.Text = My.Settings.port_number
        IDTB.Text = My.Settings.user
        FormatsDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        FormatsDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        FillFormatsDGV()

    End Sub


#End Region


#Region "Call backs"

    Private Sub CloseBT_Click(sender As Object, e As EventArgs)

        Me.Dispose()

    End Sub

#End Region


#Region "Events"

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem

        Dim g As Graphics
        Dim sText As String
        Dim iX As Integer
        Dim iY As Integer
        Dim sizeText As SizeF
        Dim ctlTab As TabControl

        ctlTab = CType(sender, TabControl)

        g = e.Graphics

        sText = ctlTab.TabPages(e.Index).Text
        sizeText = g.MeasureString(sText, ctlTab.Font)
        iX = e.Bounds.Left + 6
        iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2
        g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY)
    End Sub


    Private Sub PortTB_TextChanged(sender As Object, e As EventArgs) Handles PortTB.TextChanged

        My.Settings.port_number = PortTB.Text
        My.Settings.Save()

    End Sub

    Private Sub IDTB_TextChanged(sender As Object, e As EventArgs) Handles IDTB.TextChanged

        My.Settings.user = IDTB.Text
        My.Settings.Save()

    End Sub

    Private Sub ServerAddressTB_TextChanged(sender As Object, e As EventArgs) Handles ServerAddressTB.TextChanged

        My.Settings.serverIp = ServerAddressTB.Text
        My.Settings.Save()

    End Sub

    Private Sub databasesCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles databasesCB.SelectedIndexChanged

        My.Settings.database = databasesCB.Text
        My.Settings.Save()

    End Sub


#End Region


#Region "Formats DGV"

    Private Sub InitializeFormatsDGVDisplay()

        InitializeFormatsDGVColumns()
        InitializeFormatsDGVRows()
        FormatsDGV.ColumnsHierarchy.AllowResize = True
        FormatsDGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        FormatsDGV.AllowCopyPaste = True
        DataGridViewsUtil.DGVSetHiearchyFontSize(FormatsDGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        FormatsDGV.BackColor = Color.White

    End Sub

    Private Sub InitializeFormatsDGVColumns()

        Dim TBEditor As New TextBoxEditor

        FormatsDGV.ColumnsHierarchy.Items.Add("Format Preview")
        Dim col1 As HierarchyItem = FormatsDGV.ColumnsHierarchy.Items.Add("Text Color")
        Dim col2 As HierarchyItem = FormatsDGV.ColumnsHierarchy.Items.Add("Background Color")
        FormatsDGV.ColumnsHierarchy.Items.Add("Bold")
        FormatsDGV.ColumnsHierarchy.Items.Add("Italic")
        FormatsDGV.ColumnsHierarchy.Items.Add("Border")

        col1.CellsEditor = TBEditor
        col2.CellsEditor = TBEditor

        For j = 0 To FormatsDGV.ColumnsHierarchy.Items.Count - 1
            FormatsDGV.ColumnsHierarchy.Items(j).TextAlignment = Drawing.ContentAlignment.MiddleCenter
            FormatsDGV.ColumnsHierarchy.Items(j).CellsTextAlignment = Drawing.ContentAlignment.MiddleCenter
        Next

    End Sub

    Private Sub InitializeFormatsDGVRows()

        isFillingDGV = True
        Dim checkBoxEditor As New CheckBoxEditor
        CreateFormatRow("Title", checkBoxEditor)
        CreateFormatRow("Important", checkBoxEditor)
        CreateFormatRow("Normal", checkBoxEditor)
        CreateFormatRow("Detail", checkBoxEditor)
        AddHandler checkBoxEditor.CheckedChanged, AddressOf dataGridView_Checkedchanged
        isFillingDGV = False

    End Sub

    Private Sub CreateFormatRow(ByRef formatName As String, _
                                ByRef checkBoxEditor As CheckBoxEditor)

        Dim row As HierarchyItem = FormatsDGV.RowsHierarchy.Items.Add(formatName)
        FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(0), formatName)
        FormatsDGV.CellsArea.SetCellEditor(row, FormatsDGV.ColumnsHierarchy.Items(3), checkBoxEditor)
        FormatsDGV.CellsArea.SetCellEditor(row, FormatsDGV.ColumnsHierarchy.Items(4), checkBoxEditor)
        FormatsDGV.CellsArea.SetCellEditor(row, FormatsDGV.ColumnsHierarchy.Items(5), checkBoxEditor)

    End Sub

    Friend Sub FillFormatsDGV()

        isFillingDGV = True
        FillRow(Formats.GetFormat("Title"), FormatsDGV.RowsHierarchy.Items(0))
        FillRow(Formats.GetFormat("Important"), FormatsDGV.RowsHierarchy.Items(1))
        FillRow(Formats.GetFormat("Normal"), FormatsDGV.RowsHierarchy.Items(2))
        FillRow(Formats.GetFormat("Detail"), FormatsDGV.RowsHierarchy.Items(3))
        isFillingDGV = False
        DataGridViewsUtil.FormatDGVRowsHierarchy(FormatsDGV)
        FormatsDGV.ColumnsHierarchy.AutoStretchColumns = True
        FormatsDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        ' FormatsDGV()
        FormatsDGV.Refresh()

    End Sub

    Private Sub FillRow(ByRef format As Formats.FinancialBIFormat, _
                        ByRef row As HierarchyItem)

        FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(1), "")
        FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(2), "")
        DataGridViewsUtil.SetCellFillColor(row.Cells(1), format.textColor)
        DataGridViewsUtil.SetCellFillColor(row.Cells(2), format.backColor)

        ' Bold
        If format.isBold = True Then
            FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(3), True)
        Else
            FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(3), False)
        End If

        ' Italic
        If format.isItalic = True Then
            FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(4), True)
        Else
            FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(4), False)
        End If

        ' Borders
        If format.bordersPresent = True Then
            FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(5), True)
        Else
            FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(5), False)
        End If

        FormatPreview(row, format)
      
    End Sub

    Friend Sub FormatPreview(ByVal row As HierarchyItem, ByVal format As Formats.FinancialBIFormat)

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(FormatsDGV.VIBlendTheme).GridCellStyle
        CStyle.TextColor = format.textColor
        CStyle.FillStyle = New FillStyleSolid(format.backColor)
        If format.isBold = True Then CStyle.Font = New System.Drawing.Font(CStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)
        If format.isItalic = True Then CStyle.Font = New System.Drawing.Font(CStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Italic)
        FormatsDGV.CellsArea.SetCellDrawStyle(row, FormatsDGV.ColumnsHierarchy.Items(0), CStyle)

    End Sub


#Region "Formats DGV Events"

    Private Sub FormatsDGV_CellMouseEnter(ByVal sender As Object, ByVal args As CellEventArgs)

        currentFormatDGVcell = args.Cell

    End Sub

    Private Sub FormatsDGV_CellBeginEdit(sender As Object, e As EventArgs)

        If isFillingDGV = False Then
            Select Case currentFormatDGVcell.ColumnItem.ItemIndex
                Case 1
                    If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                        DataGridViewsUtil.SetCellFillColor(currentFormatDGVcell, ColorDialog1.Color.ToArgb)
                        Select Case currentFormatDGVcell.RowItem.Caption
                            Case "Title" : My.Settings.titleFontColor = ColorDialog1.Color
                            Case "Important" : My.Settings.importantFontColor = ColorDialog1.Color
                            Case "Normal" : My.Settings.normalFontColor = ColorDialog1.Color
                            Case "Detail" : My.Settings.detailFontColor = ColorDialog1.Color
                        End Select
                        FormatsDGV.CloseEditor(False)
                    End If
                Case 2
                    If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                        DataGridViewsUtil.SetCellFillColor(currentFormatDGVcell, ColorDialog1.Color.ToArgb)
                        Select Case currentFormatDGVcell.RowItem.Caption
                            Case "Title" : My.Settings.titleBackColor = ColorDialog1.Color
                            Case "Important" : My.Settings.importantBackColor = ColorDialog1.Color
                            Case "Normal" : My.Settings.normalBackColor = ColorDialog1.Color
                            Case "Detail" : My.Settings.detailBackColor = ColorDialog1.Color
                        End Select
                        FormatsDGV.CloseEditor(False)
                    End If
            End Select
         End If
        My.Settings.Save()
        FormatPreview(currentFormatDGVcell.RowItem, Formats.GetFormat(currentFormatDGVcell.RowItem.Caption))

    End Sub

    Private Sub dataGridView_Checkedchanged(sender As Object, e As EventArgs)

        Select Case currentFormatDGVcell.ColumnItem.ItemIndex
            Case 3
                Dim checkBox As vCheckBox = TryCast(FormatsDGV.CellsArea.GetCellEditor(currentFormatDGVcell.RowItem, FormatsDGV.ColumnsHierarchy.Items(3)).Control, vCheckBox)
                Select Case currentFormatDGVcell.RowItem.Caption
                    Case "Title" : My.Settings.titleFontBold = checkBox.Checked
                    Case "Important" : My.Settings.importantFontBold = checkBox.Checked
                    Case "Normal" : My.Settings.normalFontBold = checkBox.Checked
                    Case "Detail" : My.Settings.detailFontBold = checkBox.Checked
                End Select
            Case 4
                Dim checkBox As vCheckBox = TryCast(FormatsDGV.CellsArea.GetCellEditor(currentFormatDGVcell.RowItem, FormatsDGV.ColumnsHierarchy.Items(4)).Control, vCheckBox)
                Select Case currentFormatDGVcell.RowItem.Caption
                    Case "Title" : My.Settings.titleFontItalic = checkBox.Checked
                    Case "Important" : My.Settings.importantFontItalic = checkBox.Checked
                    Case "Normal" : My.Settings.normalFontItalic = checkBox.Checked
                    Case "Detail" : My.Settings.detailFontItalic = checkBox.Checked
                End Select
            Case 5
                Dim checkBox As vCheckBox = TryCast(FormatsDGV.CellsArea.GetCellEditor(currentFormatDGVcell.RowItem, FormatsDGV.ColumnsHierarchy.Items(5)).Control, vCheckBox)
                Select Case currentFormatDGVcell.RowItem.Caption
                    Case "Title" : My.Settings.titleBordersPresent = checkBox.Checked
                    Case "Important" : My.Settings.importantBordersPresent = checkBox.Checked
                    Case "Normal" : My.Settings.normalBordersPresent = checkBox.Checked
                    Case "Detail" : My.Settings.detailBordersPresent = checkBox.Checked
                End Select
       End Select
        My.Settings.Save()
        FormatPreview(currentFormatDGVcell.RowItem, Formats.GetFormat(currentFormatDGVcell.RowItem.Caption))


    End Sub

#End Region


#End Region



End Class