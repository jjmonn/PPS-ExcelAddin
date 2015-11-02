' SettingMainUI.vb
'
' Display settings controls
'
'
' To do: 
'
'
' Known bugs:
'
'
'
' Last modified: 24/10/2015
' Author: Julien Monnereau


Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls
Imports VIBlend.Utilities
Imports CRUD


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

        If GlobalVariables.AuthenticationFlag = True Then
            For Each currency As Currency In GlobalVariables.Currencies.GetDictionary().Values
                Dim li As New ListItem
                li.Value = currency.Id
                li.Text = currency.Name
                CurrenciesCombobox.Items.Add(li)
                If li.Value = My.Settings.currentCurrency Then
                    li.IsChecked = True
                    CurrenciesCombobox.SelectedItem = li
                End If
            Next
        End If

    End Sub

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


#End Region


#Region "Call backs"

    Private Sub CloseBT_Click(sender As Object, e As EventArgs)

        Me.Dispose()

    End Sub

    Private Sub m_saveButton_Click(sender As Object, e As EventArgs) Handles m_saveButton.Click

        My.Settings.port_number = PortTB.Text
        My.Settings.Save()
        My.Settings.user = IDTB.Text
        My.Settings.Save()
        My.Settings.serverIp = ServerAddressTB.Text
        My.Settings.Save()
        My.Settings.Save()
        If IsNothing(CurrenciesCombobox.SelectedItem) = False Then
            My.Settings.currentCurrency = CurrenciesCombobox.SelectedItem.Value
        End If
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

        FormatsDGV.ColumnsHierarchy.Items.Add(Local.GetValue("settings.format_preview"))
        Dim col1 As HierarchyItem = FormatsDGV.ColumnsHierarchy.Items.Add(Local.GetValue("settings.text_color"))
        Dim col2 As HierarchyItem = FormatsDGV.ColumnsHierarchy.Items.Add(Local.GetValue("settings.background_color"))
        FormatsDGV.ColumnsHierarchy.Items.Add(Local.GetValue("settings.bold"))
        FormatsDGV.ColumnsHierarchy.Items.Add(Local.GetValue("settings.italic"))
        FormatsDGV.ColumnsHierarchy.Items.Add(Local.GetValue("settings.border"))

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
        CreateFormatRow(Local.GetValue("settings.title"), "t", checkBoxEditor)
        CreateFormatRow(Local.GetValue("settings.important"), "i", checkBoxEditor)
        CreateFormatRow(Local.GetValue("settings.normal"), "n", checkBoxEditor)
        CreateFormatRow(Local.GetValue("settings.detail"), "d", checkBoxEditor)
        AddHandler checkBoxEditor.CheckedChanged, AddressOf dataGridView_Checkedchanged
        isFillingDGV = False

    End Sub

    Private Sub CreateFormatRow(ByRef p_formatName As String, _
                                ByRef p_formatId As String, _
                                ByRef checkBoxEditor As CheckBoxEditor)

        Dim row As HierarchyItem = FormatsDGV.RowsHierarchy.Items.Add(p_formatName)
        row.ItemValue = p_formatId
        FormatsDGV.CellsArea.SetCellValue(row, FormatsDGV.ColumnsHierarchy.Items(0), p_formatName)
        FormatsDGV.CellsArea.SetCellEditor(row, FormatsDGV.ColumnsHierarchy.Items(3), CheckBoxEditor)
        FormatsDGV.CellsArea.SetCellEditor(row, FormatsDGV.ColumnsHierarchy.Items(4), CheckBoxEditor)
        FormatsDGV.CellsArea.SetCellEditor(row, FormatsDGV.ColumnsHierarchy.Items(5), CheckBoxEditor)

    End Sub

    Friend Sub FillFormatsDGV()

        isFillingDGV = True
        FillRow(Formats.GetFormat("t"), FormatsDGV.RowsHierarchy.Items(0))
        FillRow(Formats.GetFormat("i"), FormatsDGV.RowsHierarchy.Items(1))
        FillRow(Formats.GetFormat("n"), FormatsDGV.RowsHierarchy.Items(2))
        FillRow(Formats.GetFormat("d"), FormatsDGV.RowsHierarchy.Items(3))
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
                            Case "t" : My.Settings.titleFontColor = ColorDialog1.Color
                            Case "i" : My.Settings.importantFontColor = ColorDialog1.Color
                            Case "n" : My.Settings.normalFontColor = ColorDialog1.Color
                            Case "d" : My.Settings.detailFontColor = ColorDialog1.Color
                        End Select
                        FormatsDGV.CloseEditor(False)
                    End If
                Case 2
                    If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                        DataGridViewsUtil.SetCellFillColor(currentFormatDGVcell, ColorDialog1.Color.ToArgb)
                        Select Case currentFormatDGVcell.RowItem.Caption
                            Case "t" : My.Settings.titleBackColor = ColorDialog1.Color
                            Case "i" : My.Settings.importantBackColor = ColorDialog1.Color
                            Case "n" : My.Settings.normalBackColor = ColorDialog1.Color
                            Case "d" : My.Settings.detailBackColor = ColorDialog1.Color
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
                    Case "t" : My.Settings.titleFontBold = checkBox.Checked
                    Case "i" : My.Settings.importantFontBold = checkBox.Checked
                    Case "n" : My.Settings.normalFontBold = checkBox.Checked
                    Case "d" : My.Settings.detailFontBold = checkBox.Checked
                End Select
            Case 4
                Dim checkBox As vCheckBox = TryCast(FormatsDGV.CellsArea.GetCellEditor(currentFormatDGVcell.RowItem, FormatsDGV.ColumnsHierarchy.Items(4)).Control, vCheckBox)
                Select Case currentFormatDGVcell.RowItem.Caption
                    Case "t" : My.Settings.titleFontItalic = checkBox.Checked
                    Case "i" : My.Settings.importantFontItalic = checkBox.Checked
                    Case "n" : My.Settings.normalFontItalic = checkBox.Checked
                    Case "d" : My.Settings.detailFontItalic = checkBox.Checked
                End Select
            Case 5
                Dim checkBox As vCheckBox = TryCast(FormatsDGV.CellsArea.GetCellEditor(currentFormatDGVcell.RowItem, FormatsDGV.ColumnsHierarchy.Items(5)).Control, vCheckBox)
                Select Case currentFormatDGVcell.RowItem.Caption
                    Case "t" : My.Settings.titleBordersPresent = checkBox.Checked
                    Case "i" : My.Settings.importantBordersPresent = checkBox.Checked
                    Case "n" : My.Settings.normalBordersPresent = checkBox.Checked
                    Case "d" : My.Settings.detailBordersPresent = checkBox.Checked
                End Select
        End Select
        My.Settings.Save()
        FormatPreview(currentFormatDGVcell.RowItem, Formats.GetFormat(currentFormatDGVcell.RowItem.Caption))


    End Sub

#End Region


#End Region


End Class