' DataGridViewUtil.vb
' 
' VDataGridView formatting utilities functions
' Drop to worksheet range formatting
'     
'
' To do: 
'       - put excel formats procedure into excelFormatting module
'       - periods formatting in the headers
'       - Externalize drop to excel functions
'       -  Rows formatting : put the variable in the DB
'       -  DGV cells lines
'
'
'
' Known Bugs:
'
'
'
' Last modified date: 17/09/2015
' Author: Julien Monnereau


Imports System.Drawing
Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.Utilities
Imports Microsoft.Office.Interop
Imports CRUD


Friend Class DataGridViewsUtil


#Region "Instance Variables"

    ' 
    ' Constants
    Private FIXED_SINGLE_HIERARCHY_COLUMN_WIDTH As Single = 100
    Public Const AVERAGE_CHAR_SIZE As Int32 = 9
    Public Const INDENT_CHAR_SIZE As Int32 = 2
    Private Const BASIC_DGV_REPORT_THEME As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Private Const BASIC_DGV_REPORT_FONT_SIZE As Single = 8
    Private Const BASIC_DGV_REPORT_FIRST_COLUMN_CAPTION As String = ""

    Friend Shared ADJUSTMENTS_ROW_THEME As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010BLACK
    Friend Shared ADJUSTMENTS_COLOR As Color = Color.Blue
    Friend Shared ENTITIES_ROWS_THEME As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010BLUE
    ' format break down to be enhanced !!
    Friend Shared DGV_HEIGHT_MARGIN As Int32 = 10

#End Region


    Friend Sub New()

    End Sub


#Region "ControllingUI2 DGVs Formatting"

    ' vDgv DISPLAY Initialization
    Friend Shared Sub InitDisplayVDataGridView(ByRef vDataGridView As vDataGridView, _
                                               ByRef theme As VIBLEND_THEME)

        With vDataGridView
            .VIBlendTheme = theme
            .GroupingDefaultHeaderTextVisible = True
            .BackColor = Color.White
            .GridLinesDisplayMode = GridLinesDisplayMode.DISPLAY_NONE
            .ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            '.ColumnsHierarchy.AutoStretchColumns = True
        End With

    End Sub

    ' vDgv Display After Populating ' note controlling ui2 specificity !!
    Friend Sub FormatDGVs(ByRef vDGV As vDataGridView, _
                          ByRef currencyId As Int32)

        ' priority normal => implement format CRUD
        '     Dim InputsFormatsDictionary = FormatsMapping.GetFormatTable(INPUT_FORMAT_CODE)
        Dim formatCode, fmtStr As String
        Dim indent As Int32
        For Each row In vDGV.RowsHierarchy.Items
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(row.Caption)

            If l_account Is Nothing Then Continue For
            formatCode = l_account.FormatId

            Dim HANStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(vDGV.VIBlendTheme).HierarchyItemStyleNormal
            Dim HASStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(vDGV.VIBlendTheme).HierarchyItemStyleSelected
            Dim HENStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(ENTITIES_ROWS_THEME).HierarchyItemStyleNormal
            Dim HESStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(ENTITIES_ROWS_THEME).HierarchyItemStyleSelected
            Dim CAStyle As GridCellStyle = GridTheme.GetDefaultTheme(vDGV.VIBlendTheme).GridCellStyle
            Dim CEStyle As GridCellStyle = GridTheme.GetDefaultTheme(ENTITIES_ROWS_THEME).GridCellStyle

            HANStyle.Font = New System.Drawing.Font(vDGV.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
            HASStyle.Font = New System.Drawing.Font(vDGV.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
            HENStyle.Font = New System.Drawing.Font(vDGV.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
            HESStyle.Font = New System.Drawing.Font(vDGV.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
            CAStyle.Font = New System.Drawing.Font(vDGV.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
            CEStyle.Font = New System.Drawing.Font(vDGV.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)

            ' priority normal => implement format CRUD
            'priority high !!!!!!!!!!!!!!!!!!!!
            'If InputsFormatsDictionary(formatCode)(FORMAT_BOLD_VARIABLE) = 1 Then
            '    HANStyle.Font = New System.Drawing.Font(HANStyle.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Bold)
            '    HASStyle.Font = New System.Drawing.Font(HASStyle.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Bold)
            '    CAStyle.Font = New System.Drawing.Font(CAStyle.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Bold)
            'End If
            'If InputsFormatsDictionary(formatCode)(FORMAT_ITALIC_VARIABLE) = 1 Then
            '    HANStyle.Font = New System.Drawing.Font(HANStyle.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Italic)
            '    HASStyle.Font = New System.Drawing.Font(HASStyle.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Italic)
            '    CAStyle.Font = New System.Drawing.Font(CAStyle.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Italic)
            'End If

            '' Colors 
            'If Not IsDBNull(InputsFormatsDictionary(formatCode)(FORMAT_TEXT_COLOR_VARIABLE)) Then
            '    CAStyle.TextColor = System.Drawing.Color.FromArgb(InputsFormatsDictionary(formatCode)(FORMAT_TEXT_COLOR_VARIABLE))
            'End If
            'If Not IsDBNull(InputsFormatsDictionary(formatCode)(FORMAT_BCKGD_VARIABLE)) Then
            '    CAStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.FromArgb(InputsFormatsDictionary(formatCode)(FORMAT_BCKGD_VARIABLE)))
            'End If
            Dim l_currency As Currency = GlobalVariables.Currencies.GetValue(currencyId)

            If l_currency is Nothing then Continue For
            Select Case (l_account.Type)
                ' nombe de chiffres après la virgule à variabiliser -> settings !!!!
                Case Account.AccountType.MONETARY : fmtStr = "{0:" & l_currency.Symbol & "#,##0;(" & l_currency.Symbol & "#,##0)}"
                Case Account.AccountType.PERCENTAGE : fmtStr = "{0:P}"        ' put this in a table ?
                Case Account.AccountType.NUMBER : fmtStr = "{0:N}"        ' further evolution set unit ?
                Case Else : fmtStr = "{0:C0}"
            End Select

            '      indent = InputsFormatsDictionary(formatCode)(FORMAT_INDENT_VARIABLE)
            If row.ParentItem Is Nothing Then
                format_row(row, formatCode, fmtStr, CAStyle, HANStyle, HASStyle, indent, CAStyle, CEStyle, HANStyle, HASStyle, HENStyle, HESStyle)
            Else
                format_row(row, formatCode, fmtStr, CEStyle, HENStyle, HESStyle, indent, CAStyle, CEStyle, HANStyle, HASStyle, HENStyle, HESStyle)
            End If
        Next

        If vDGV.ColumnsHierarchy.Items(0).Items.Count = 0 Then
            For Each item In vDGV.ColumnsHierarchy.Items
                item.Width = FIXED_SINGLE_HIERARCHY_COLUMN_WIDTH
            Next
        Else
            vDGV.ColumnsHierarchy.ExpandAllItems()
            For Each item In vDGV.ColumnsHierarchy.Items
                AdjustChildrenHierarchyItemSize(item)
            Next
        End If
        '  vDgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        ' vdgv.ColumnsHierarchy.AutoStretchColumns = True
        '   vDgv.Refresh()


    End Sub

    'If InputFormatsDictionary.Item(formatCode).Item(FORMAT_BORDER_VARIABLE) = 1 Then
    '    CStyle.BorderColor = Color.DarkBlue                    
    'Else
    '    CStyle.BorderColor = Color.Transparent
    'End If

    'If InputFormatsDictionary.Item(formatCode).Item(FORMAT_DGV_BACKGROUND_VARIABLE) <> "" Then
    '    ' HStyle.FillStyle = New FillStyleSolid(Color.FromArgb(InputFormatsDictionary.Item(formatCode).Item(FORMAT_DGV_BACKGROUND_VARIABLE)))
    '    CStyle.FillStyle = New FillStyleSolid(Color.FromArgb(InputFormatsDictionary.Item(formatCode).Item(FORMAT_DGV_BACKGROUND_VARIABLE)))
    'End If
    ' HStyle.TextColor = Color.FromArgb(InputFormatsDictionary.Item(formatCode).Item(FORMAT_TEXT_COLOR_VARIABLE))
    ' CStyle.TextColor = Color.FromArgb(InputFormatsDictionary.Item(formatCode).Item(FORMAT_TEXT_COLOR_VARIABLE))
    Private Sub format_row(ByRef row As HierarchyItem, _
                           ByRef formatCode As String, _
                           ByRef format_string As String, _
                           ByRef CStyle As GridCellStyle, _
                           ByRef HNStyle As HierarchyItemStyle, _
                           ByRef HSStyle As HierarchyItemStyle, _
                           ByRef indent As Int32, _
                           ByRef CAStyle As GridCellStyle, _
                           ByRef CEStyle As GridCellStyle, _
                           ByRef HANStyle As HierarchyItemStyle, _
                           ByRef HASStyle As HierarchyItemStyle, _
                           ByRef HENStyle As HierarchyItemStyle, _
                           ByRef HESStyle As HierarchyItemStyle)

        Select Case indent
            Case 1 : row.Caption = "   " & row.Caption
            Case 2 : row.Caption = "      " & row.Caption
            Case 3 : row.Caption = "            " & row.Caption
        End Select

        row.HierarchyItemStyleNormal = HNStyle
        row.HierarchyItemStyleSelected = HSStyle
        row.CellsStyle = CStyle
        row.CellsFormatString = format_string
        row.CellsTextAlignment = ContentAlignment.MiddleRight
        'If row.Items.Count > 0 Then
        '    For Each item As HierarchyItem In row.Items
        '        item.HierarchyItemStyleNormal = HNStyle
        '        item.HierarchyItemStyleSelected = HSStyle
        '        item.CellsStyle = CStyle
        '        item.CellsFormatString = fmtStr
        '        item.CellsTextAlignment = ContentAlignment.MiddleRight
        '    Next
        'End If
        For Each sub_row In row.Items
            format_row(sub_row, formatCode, format_string, CEStyle, HENStyle, HESStyle, indent, CAStyle, CEStyle, HANStyle, HASStyle, HENStyle, HESStyle)
        Next

    End Sub

    Friend Shared Sub AdjustChildrenHierarchyItemSize(ByRef item As HierarchyItem)

        item.AutoResize(AutoResizeMode.FIT_ALL)
        item.TextAlignment = ContentAlignment.MiddleRight
        If item.Items.Count > 0 Then
            For Each subItem As HierarchyItem In item.Items
                subItem.AutoResize(AutoResizeMode.FIT_ALL)
                If subItem.Items.Count > 0 Then AdjustChildrenHierarchyItemSize(subItem)
            Next
        End If

    End Sub

    Friend Shared Sub EqualizeColumnsAndRowsHierarchyWidth(ByRef DGV As vDataGridView)

        Dim width As Int32 = DGV.Width / (DGV.ColumnsHierarchy.Items.Count + 1)
        DGV.RowsHierarchy.Items(0).Width = width
        For Each item In DGV.ColumnsHierarchy.Items
            item.Width = width
        Next

    End Sub


#End Region


#Region "Font Size"


    ' set row font size / row cells


    Friend Shared Sub DGVSetHiearchyFontSize(ByRef DGV As vDataGridView, _
                                                ByRef itemsFontSize As Single, _
                                                Optional ByRef cellsFontSize As Single = 0)

        Dim itemStyleNormal As HierarchyItemStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).HierarchyItemStyleNormal
        Dim itemStyleSelected As HierarchyItemStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).HierarchyItemStyleSelected
        itemStyleNormal.Font = New System.Drawing.Font(DGV.Font.FontFamily, itemsFontSize)
        itemStyleSelected.Font = New System.Drawing.Font(DGV.Font.FontFamily, itemsFontSize)

        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            SetSubItemsFontSizes(item, _
                                 itemStyleNormal, _
                                 itemStyleSelected)
        Next
        For Each item In DGV.ColumnsHierarchy.Items
            SetSubItemsFontSizes(item, _
                                 itemStyleNormal, _
                                 itemStyleSelected)
        Next

        If cellsFontSize <> 0 Then SetDGVCellsFontSize(DGV, cellsFontSize)

    End Sub

    Friend Shared Sub SetSubItemsFontSizes(ByRef item As HierarchyItem, _
                                            ByRef itemStyleNormal As HierarchyItemStyle, _
                                            ByRef itemStyleSelected As HierarchyItemStyle)

        item.HierarchyItemStyleNormal = itemStyleNormal
        item.HierarchyItemStyleSelected = itemStyleSelected
        For Each subItem As HierarchyItem In item.Items
            SetSubItemsFontSizes(subItem, _
                                 itemStyleNormal, _
                                 itemStyleSelected)
        Next

    End Sub

    Friend Shared Sub SetDGVCellsFontSize(ByRef DGV As vDataGridView, ByRef fontSize As Single)

        Dim CStyleN As GridCellStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).GridCellStyle
        CStyleN.Font = New System.Drawing.Font(DGV.Font.FontFamily, fontSize, FontStyle.Regular)

        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            SetItemsCellsFontSize(item, CStyleN)
        Next

    End Sub

    Friend Shared Sub SetItemsCellsFontSize(ByRef item As HierarchyItem, _
                                                     ByRef cstyleN As GridCellStyle)

        item.CellsStyle = cstyleN
        For Each subItem In item.Items
            SetItemsCellsFontSize(subItem, cstyleN)
        Next

    End Sub

#End Region


#Region "ControllingUI2 DGV Columns Initialization"

    ' Initializes VDatagridViewColumns for NO VERSIONNING DGV 
    Friend Shared Function CreateDGVColumns(ByRef DataGridView As vDataGridView, _
                                             ByRef periodList() As Int32, _
                                             ByRef timeConfig As String, _
                                             ByRef columns_reinit As Boolean) As Dictionary(Of Integer, Int32)

        Dim tmpDict As New Dictionary(Of Integer, Int32)
        Dim i As Int32 = 0
        Dim periodStr As String

        If columns_reinit = True Then DataGridView.ColumnsHierarchy.Clear()
        For Each period As Int32 In periodList
            Select Case timeConfig
                Case MONTHLY_TIME_CONFIGURATION : periodStr = Format(DateTime.FromOADate(period), "Short date")
                Case Else : periodStr = Format(DateTime.FromOADate(period), "Short date")
            End Select
            Dim column_ As HierarchyItem = DataGridView.ColumnsHierarchy.Items.Add(periodStr)
            column_.TextAlignment = ContentAlignment.MiddleCenter
            tmpDict.Add(period, i)
            i = i + 1
        Next

        Return tmpDict

    End Function


    ' Initializes the column for a DGV displaying VERSION below periods
    ' Param DataGridView: the VDataGridview to init 
    ' Param periodList(): array of periods to be displayed
    ' Param Versions(): array of the versions to be displayed
    Friend Shared Function CreateDGVColumns(ByRef DataGridView As vDataGridView, _
                                             ByRef periodList() As UInt32, _
                                             ByRef Versions() As UInt32, _
                                             ByRef timeConfig As String) _
                                             As Dictionary(Of Integer, Int32)

        DataGridView.ColumnsHierarchy.Clear()
        Dim tmpDict As New Dictionary(Of Integer, Int32)
        Dim periodStr As String

        Dim i As Int32 = 0
        For Each period As Int32 In periodList
            Select Case timeConfig
                Case MONTHLY_TIME_CONFIGURATION : periodStr = Format(DateTime.FromOADate(period), "Short date")
                Case Else : periodStr = Format(DateTime.FromOADate(period), "Short date")
            End Select
            Dim column_ As HierarchyItem = DataGridView.ColumnsHierarchy.Items.Add(periodStr)
            tmpDict.Add(period, i)
            For Each Version As String In Versions
                Dim SubColumn As HierarchyItem = column_.Items.Add(Version)
            Next
            i = i + 1
        Next
        Return tmpDict

    End Function


#End Region


#Region "ControllingUI2 DGV rows initialization"

    ' Initializes VDataGridViewRows Hierarchy
    ' Calls the 2 private subs below
    ' param index: tab# (corresponds to the node#)
    Friend Function CreatesVDataGridViewRowsHierarchy(ByRef dgv As vDataGridView, _
                                                             ByRef index As Integer, _
                                                             ByRef AccountsTV As TreeView, _
                                                             Optional ByRef entitiesArray() As String = Nothing, _
                                                             Optional ByRef entity_node As TreeNode = Nothing) _
                                                             As Dictionary(Of String, Int32)

        Dim KeyLineDictionary As New Dictionary(Of String, Int32)
        Dim line_index As Int32 = 0

        dgv.RowsHierarchy.Clear()
        If Not entitiesArray Is Nothing Then
            If UBound(entitiesArray) > 1 Then
                For Each account As TreeNode In AccountsTV.Nodes.Item(index).Nodes
                    FillInSubRowsHierarchySeveralEntities(account, _
                                                          line_index, _
                                                          dgv, _
                                                          KeyLineDictionary, _
                                                          entitiesArray)
                Next
            Else
                For Each account As TreeNode In AccountsTV.Nodes.Item(index).Nodes
                    FillInSubRowsHierarchyOneEntity(account, _
                                                    line_index, _
                                                    dgv, _
                                                    KeyLineDictionary)
                Next
            End If
        Else
            ' attention below à vérifier ... plutôt one entity
            For Each account_node As TreeNode In AccountsTV.Nodes.Item(index).Nodes
                FillInSubRowsHierarchySeveralEntities(account_node, _
                                                      line_index, _
                                                      dgv, _
                                                      KeyLineDictionary, , _
                                                      entity_node)
            Next
        End If
        Return KeyLineDictionary

    End Function


    ' Recursively creates rows and sub rows for the current node and children - Case SEVERAL ENTITIES
    Private Sub FillInSubRowsHierarchySeveralEntities(ByRef account_node As TreeNode, _
                                                      ByRef line_index As Int32, _
                                                      ByRef dgv As vDataGridView, _
                                                      ByRef KeyLineDictionary As Dictionary(Of String, Int32), _
                                                      Optional ByRef entitiesArray() As String = Nothing, _
                                                      Optional ByRef entity_node As TreeNode = Nothing)

        ' -> currently no subtitles -> all nodes are processed ' -> list of processed nodes from ACCMAPP
        Dim row As HierarchyItem = dgv.RowsHierarchy.Items.Add(account_node.Text)
        KeyLineDictionary.Add(account_node.Name, line_index)
        line_index = line_index + 1
        If Not entitiesArray Is Nothing Then
            For i As Int32 = 1 To UBound(entitiesArray)
                Dim sub_row As HierarchyItem = row.Items.Add(GlobalVariables.AxisElems.GetValueId(AxisType.Entities, entitiesArray(i)))
            Next
        Else
            For Each child In entity_node.Nodes
                SetUpRowsHierarchyEntitiesHierarchy(row, child, dgv)
            Next
        End If
        If account_node.Nodes.Count > 0 Then
            For Each node As TreeNode In account_node.Nodes
                Dim l_account As Account = GlobalVariables.Accounts.GetValue(CInt(node.Name))

                If l_account Is Nothing Then Continue For
                If l_account.FormatId <> TITLE_FORMAT_CODE Then
                    FillInSubRowsHierarchySeveralEntities(node, _
                                                          line_index, _
                                                          dgv, _
                                                          KeyLineDictionary,
                                                          entitiesArray,
                                                          entity_node)
                End If
            Next
        End If

    End Sub

    ' Recursively creates rows and sub rows for the current node and children - Case ONE ENTITY
    Friend Sub FillInSubRowsHierarchyOneEntity(ByRef inputNode As TreeNode, _
                                                        ByRef LineIndex As Int32, _
                                                        ByRef dataGridView As vDataGridView, _
                                                        ByRef KeyLineDictionary As Dictionary(Of String, Int32))

        Dim Row As HierarchyItem = dataGridView.RowsHierarchy.Items.Add(inputNode.Text)
        KeyLineDictionary.Add(inputNode.Name, LineIndex)
        LineIndex = LineIndex + 1
        If inputNode.Nodes.Count > 0 Then
            For Each account As TreeNode In inputNode.Nodes
                FillInSubRowsHierarchyOneEntity(account, LineIndex, dataGridView, KeyLineDictionary)
            Next
        End If

    End Sub

    Private Sub SetUpRowsHierarchyEntitiesHierarchy(ByRef row As HierarchyItem, _
                                                    ByRef entity_node As TreeNode, _
                                                    ByRef DGV As vDataGridView)

        If entity_node.Checked = True Then
            Dim sub_row As HierarchyItem = row.Items.Add(GlobalVariables.AxisElems.GetValueId(AxisType.Entities, CStr(entity_node.Name)))
            For Each child In entity_node.Nodes
                SetUpRowsHierarchyEntitiesHierarchy(sub_row, child, DGV)
            Next
        End If

    End Sub


#End Region


#Region "Alternative Scenarios Report DGV Utility"

    Friend Shared Function CreateASDGVReport(ByRef period_list As List(Of Int32), _
                                                      ByRef time_config As String) As vDataGridView

        Dim DGV As New vDataGridView
        DGV.VIBlendTheme = BASIC_DGV_REPORT_THEME
        DGV.BackColor = Color.White
        DGV.ColumnsHierarchy.Items.Add(BASIC_DGV_REPORT_FIRST_COLUMN_CAPTION)
        CreateDGVColumns(DGV, period_list.ToArray, time_config, False)
        For j As Int32 = 1 To DGV.ColumnsHierarchy.Items.Count - 1
            DGV.ColumnsHierarchy.Items(j).CellsFormatString = DEFAULT_FORMAT_STRING
            DGV.ColumnsHierarchy.Items(j).CellsTextAlignment = ContentAlignment.MiddleRight
            DGV.ColumnsHierarchy.Items(j).TextAlignment = ContentAlignment.MiddleCenter
        Next

        DGV.RowsHierarchy.Items.Add("Base Scenario")
        DGV.RowsHierarchy.Items.Add("New Scenario")
        DGVSetHiearchyFontSize(DGV, BASIC_DGV_REPORT_FONT_SIZE, BASIC_DGV_REPORT_FONT_SIZE)
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.GridLinesDisplayMode = GridLinesDisplayMode.DISPLAY_COLUMN_LINES
        DGV.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.Refresh()
        Return DGV

    End Function

    Friend Shared Sub AddSerieToBasicDGVReport(ByRef row As HierarchyItem, _
                                                        ByRef serie_name As String, _
                                                        ByRef values As Double())

        ' evolution -> border + bold around  important items !!
        Dim sub_row As HierarchyItem = row.Items.Add("")
        row.DataGridView.CellsArea.SetCellValue(sub_row, row.DataGridView.ColumnsHierarchy.Items(0), serie_name)
        For j As Int32 = 1 To row.DataGridView.ColumnsHierarchy.Items.Count - 1
            row.DataGridView.CellsArea.SetCellValue(sub_row, row.DataGridView.ColumnsHierarchy.Items(j), values(j - 1))
        Next

    End Sub

    Friend Shared Sub FormatBasicDGV(ByRef DGV As vDataGridView)

        DGV.ColumnsHierarchy.AutoStretchColumns = True
        AdjustDGVFColumnWidth(DGV, 0)
        Try
            DGV.Refresh()
            DGV.RowsHierarchy.Items(0).Selected = True
        Catch ex As Exception

        End Try

    End Sub

#End Region


#Region "Send to Excel Functions"

    Friend Shared Sub CopyDGVToExcelGeneric(ByRef DGV As vDataGridView, _
                                            ByRef dest_range As Excel.Range, _
                                            ByRef i As Int32, _
                                            ByRef p_copyOnlyExpanded As Boolean)

        Dim j As Int32 = 1
        Dim nb_columns_floors As Int32 = i

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            SetupColumnsTitles(column, dest_range, i, j, nb_columns_floors, p_copyOnlyExpanded)
        Next

        j = 0
        i = i + 1
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items
            SetupRowsTitles(row, dest_range, i, j, p_copyOnlyExpanded)
        Next

        j = 1
        i = nb_columns_floors + 1
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items
            CopyRowHierarchy(row, dest_range, i, j, p_copyOnlyExpanded)
        Next
        dest_range.Worksheet.Range(dest_range.Worksheet.Cells(1, 1), dest_range.Offset(i, j)).Columns.AutoFit()

    End Sub

    Friend Shared Sub SetupColumnsTitles(ByRef column As HierarchyItem, _
                                         ByRef range As Excel.Range, _
                                         ByRef i As Int32, _
                                         ByRef j As Int32, _
                                         ByRef parent_j As Int32, _
                                         ByRef p_copyOnlyExpanded As Boolean)

      

            range.Offset(i, j).Value = column.Caption
            range.Offset(i, j).Font.Bold = True
            ' FormatRangeFromHierarchyItem(range.Offset(i, j), column)
            j = j + 1

        If p_copyOnlyExpanded = False _
      Or p_copyOnlyExpanded = True And column.Expanded = True Then
            If column.Items.Count > 0 Then
                For Each sub_column As HierarchyItem In column.Items
                    i = i + 1
                    SetupColumnsTitles(sub_column, range, i, j, parent_j, p_copyOnlyExpanded)
                Next
                parent_j = parent_j + column.Items.Count
            End If

        End If

    End Sub

    Friend Shared Sub SetupRowsTitles(ByRef row As HierarchyItem, _
                                      ByRef range As Excel.Range, _
                                      ByRef i As Int32, _
                                      ByRef j As Int32, _
                                      ByRef p_copyOnlyExpanded As Boolean)

     
        range.Offset(i, j).Value = row.Caption
        FormatRangeFromHierarchyItem(range.Offset(i, j), row)
        i = i + 1
        Dim sub_rows_nb As Int32 = 0
        Dim group_start_row As Int32 = i + range.Row - 1

        If p_copyOnlyExpanded = False _
        Or p_copyOnlyExpanded = True And row.Expanded = True Then


            For Each sub_row As HierarchyItem In row.Items
                SetupRowsTitles(sub_row, range, i, j, p_copyOnlyExpanded)
                sub_rows_nb = sub_rows_nb + sub_row.Items.Count
            Next

            If row.Items.Count > 0 Then
                Dim ws As Excel.Worksheet = range.Worksheet
                Dim nb_sub_items As Int32 = 0
                DataGridViewsUtil.CountSubItemsNb(row, nb_sub_items)
                Dim grouped_range As Excel.Range = ws.Range(ws.Cells(group_start_row + 1, 1), ws.Cells(group_start_row + nb_sub_items, 2))
                grouped_range.Rows.Group(Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            End If

        End If

    End Sub

    Friend Shared Sub CopyRowHierarchy(ByRef row As HierarchyItem, _
                                        ByRef range As Excel.Range, _
                                        ByRef i As Int32, _
                                        ByRef j As Int32, _
                                        ByRef p_copyOnlyExpanded As Boolean)

        j = 1
        Dim ws As Excel.Worksheet = range.Worksheet
        For Each column As HierarchyItem In row.DataGridView.ColumnsHierarchy.Items
            CopyColumnHierarchy(row, column, range, i, j, p_copyOnlyExpanded)
        Next
        FormatRangeFromGridCell(ws.Range(range.Offset(i, 1), range.Offset(i, j)), row)
        i = i + 1

        If p_copyOnlyExpanded = False _
        Or p_copyOnlyExpanded = False AndAlso row.Expanded = True Then

            For Each sub_row In row.Items
                CopyRowHierarchy(sub_row, range, i, j, p_copyOnlyExpanded)
            Next
        End If

    End Sub

    Friend Shared Sub CopyColumnHierarchy(ByRef row As HierarchyItem, _
                                          ByRef column As HierarchyItem, _
                                          ByRef range As Excel.Range, _
                                          ByRef i As Int32, _
                                          ByRef j As Int32, _
                                          ByRef p_copyOnlyExpanded As Boolean)

        range.Offset(i, j).Value = row.DataGridView.CellsArea.GetCellValue(row, column)
        j = j + 1

        If p_copyOnlyExpanded = False _
        Or p_copyOnlyExpanded = True AndAlso column.Expanded = True Then
            For Each sub_column In column.Items
                CopyColumnHierarchy(row, sub_column, range, i, j, p_copyOnlyExpanded)
            Next
        End If

    End Sub

#End Region


#Region "Versions Comparison"

    Friend Sub AddVersionComparison(ByRef DGV As vDataGridView)

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            Dim newSubColumn As HierarchyItem = column.Items.Add(column.Items(1).Caption + " vs." + column.Items(0).Caption)
            For Each row As HierarchyItem In DGV.RowsHierarchy.Items
                AddChildrenVersionsComparison(DGV, row, column)
            Next
            newSubColumn.Resizable = True
            newSubColumn.AutoResize(AutoResizeMode.FIT_ALL)
            column.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT)
        Next
        DGV.ColumnsHierarchy.Items(0).Collapse()
        DGV.ColumnsHierarchy.Items(0).Expand()

    End Sub

    Private Sub AddChildrenVersionsComparison(ByRef DGV As vDataGridView, _
                                              ByRef row As HierarchyItem, _
                                              ByRef column As HierarchyItem)

        Dim delta As Double = DGV.CellsArea.GetCellValue(row, column.Items(1)) - DGV.CellsArea.GetCellValue(row, column.Items(0))
        DGV.CellsArea.SetCellValue(row, column.Items(2), delta)

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).GridCellStyle
        If delta > 0 Then
            CStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.Green)
        Else
            CStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.Red)
        End If
        DGV.CellsArea.SetCellDrawStyle(row, column, CStyle)
        For Each subRow In row.Items
            AddChildrenVersionsComparison(DGV, subRow, column)
        Next

    End Sub

    Friend Sub RemoveVersionsComparison(ByRef dgv As vDataGridView)

        For Each column As HierarchyItem In dgv.ColumnsHierarchy.Items
            column.Items(2).Delete()
            column.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT)
        Next

    End Sub


#Region "Switch Versions"

    Friend Sub SwitchVersionsOrderWithoutComparison(ByRef DGV As vDataGridView)

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            Dim subColumn0 = column.Items(0)
            Dim valuesDic As New Dictionary(Of HierarchyItem, Object)
            For Each row In DGV.RowsHierarchy.Items
                SaveRowHierarchy(row, subColumn0, DGV, valuesDic)
            Next
            Dim newSubColumn As HierarchyItem = column.Items.Add(subColumn0.Caption)
            newSubColumn.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT)
            For Each row In DGV.RowsHierarchy.Items
                FillInNewColumn(row, newSubColumn, DGV, valuesDic)
            Next
            subColumn0.Delete()
        Next
        DGV.ColumnsHierarchy.Items(0).Collapse()
        DGV.ColumnsHierarchy.Items(0).Expand()

    End Sub

    Private Sub SaveRowHierarchy(ByRef row As HierarchyItem, _
                                 ByRef column As HierarchyItem, _
                                 ByRef DGV As vDataGridView, _
                                 ByRef valuesDic As Dictionary(Of HierarchyItem, Object))

        valuesDic.Add(row, DGV.CellsArea.GetCellValue(row, column))
        For Each subRow In row.Items
            SaveRowHierarchy(subRow, column, DGV, valuesDic)
        Next

    End Sub

    Private Sub FillInNewColumn(ByRef row As HierarchyItem, _
                                ByRef column As HierarchyItem, _
                                ByRef dgv As vDataGridView, _
                                ByRef valuesDic As Dictionary(Of HierarchyItem, Object))

        dgv.CellsArea.SetCellValue(row, column, valuesDic(row))
        For Each subRow In row.Items
            FillInNewColumn(subRow, column, dgv, valuesDic)
        Next

    End Sub


#End Region


#End Region


#Region "Formatting Utilities"

    Friend Shared Sub FormatDGVRowsHierarchy(ByRef dgv As vDataGridView)

        Dim maxLength As Int32 = 0
        Dim depth As Int32 = 0

        If dgv.RowsHierarchy.Items.Count > 0 Then
            For Each item In dgv.RowsHierarchy.Items
                GetItemMaxLength(item, maxLength, depth)
            Next
            maxLength = maxLength * AVERAGE_CHAR_SIZE + depth * INDENT_CHAR_SIZE
            For Each item In dgv.RowsHierarchy.Items
                item.Width = maxLength
            Next
        End If

    End Sub

    Friend Shared Sub AdjustDGVFColumnWidth(ByRef DGV As vDataGridView, _
                                                     ByRef column_index As Int32)

        Dim maxLength As Int32 = 0
        Dim characters_size As Single
        Try
            characters_size = DGV.RowsHierarchy.Items(0).CellsStyle.Font.Size
        Catch ex As Exception
            characters_size = BASIC_DGV_REPORT_FONT_SIZE
        End Try


        For Each item In DGV.RowsHierarchy.Items
            GetColumnMaxLength(item, column_index, maxLength)
        Next
        DGV.ColumnsHierarchy.Items(column_index).Width = maxLength * 0.7 * characters_size

    End Sub

    Friend Shared Sub GetItemMaxLength(ByRef item As HierarchyItem, _
                                                ByRef maxLength As Int32, _
                                                ByRef depth As Int32)

        If item.Caption.Length > maxLength Then
            maxLength = item.Caption.Length
        End If
        If item.Items.Count > 0 Then depth = depth + 1
        For Each subItem As HierarchyItem In item.Items
            GetItemMaxLength(subItem, maxLength, depth)
        Next

    End Sub

    Friend Shared Sub GetColumnMaxLength(ByRef item As HierarchyItem, _
                                                ByVal column_index As Int32, _
                                                ByRef maxLength As Int32)

        Dim text As String = item.DataGridView.CellsArea.GetCellValue(item, item.DataGridView.ColumnsHierarchy.Items(column_index))
        If Not text Is Nothing AndAlso text.Length > maxLength Then
            maxLength = text.Length
        End If
        For Each subItem As HierarchyItem In item.Items
            GetColumnMaxLength(subItem, column_index, maxLength)
        Next

    End Sub

    Friend Shared Sub SetColumnsMinWidth(ByRef DGV As vDataGridView, _
                                                  ByRef min_width As Int32)

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            If column.Width < min_width Then column.Width = min_width
        Next

    End Sub

#Region "Format vDGV -> Excel"

    Friend Shared Sub FormatRangeFromGridCell(ByRef xlRange As Excel.Range, _
                                              ByRef item As HierarchyItem)

        '   xlRange.NumberFormat = "# ##0 €"
        Select Case item.CellsFormatString
            Case "{0:C0}" : xlRange.NumberFormat = "# ##0 €"
            Case "{0:P}" : xlRange.NumberFormat = "0,00%"
            Case "{0:N}" : xlRange.NumberFormat = "General"
            Case "{0:N2}" : xlRange.NumberFormat = "# ##0"
            Case "{0:C0}" : xlRange.NumberFormat = "# ##0 €"
        End Select
        '   xlRange.Interior.Color = item.CellsStyle.FillStyle.Colors(0)
        Try
            xlRange.Font.Color = item.CellsStyle.TextColor.ToArgb()
            xlRange.Font.Bold = item.CellsStyle.Font.Bold
            xlRange.Font.Italic = item.CellsStyle.Font.Italic
        Catch ex As Exception
        End Try

    End Sub

    Friend Shared Sub FormatRangeFromHierarchyItem(ByRef xlRange As Excel.Range, _
                                                            ByRef item As HierarchyItem)

        If Not item.HierarchyItemStyleNormal Is Nothing Then
            xlRange.Interior.Color = item.HierarchyItemStyleNormal.FillStyle.Colors(0)
            xlRange.Font.Color = item.HierarchyItemStyleNormal.TextColor.ToArgb()
            xlRange.Font.Bold = item.HierarchyItemStyleNormal.Font.Bold
            xlRange.Font.Italic = item.HierarchyItemStyleNormal.Font.Italic
        End If

    End Sub

#End Region

#End Region


#Region "DGV Utilities"

    Friend Shared Sub CountSubItemsNb(ByRef item As HierarchyItem, _
                                                ByRef nb_items As Int32)

        For Each sub_item As HierarchyItem In item.Items
            nb_items = nb_items + 1
            CountSubItemsNb(sub_item, nb_items)
        Next

    End Sub

    Friend Shared Function GetHierarchyItemFromId(ByRef p_hierarchy As Hierarchy, _
                                                  ByRef p_id As String) As HierarchyItem

        For Each item In p_hierarchy.Items
            If item.ItemValue = p_id Then
                Return item
            End If
            For Each subItem In item.Items
                If subItem.ItemValue = p_id Then
                    Return subItem
                End If
                Dim subResult As HierarchyItem = GetHierarchyItemFromId(subItem, p_id)
                If Not subResult Is Nothing Then
                    Return subResult
                End If
            Next
        Next
        Return Nothing

    End Function

    Private Shared Function GetHierarchyItemFromId(ByRef p_parentItem As HierarchyItem, _
                                                   ByRef p_id As String) As HierarchyItem

        For Each item In p_parentItem.Items
            If item.ItemValue = p_id Then
                Return item
            End If
            For Each subItem In item.Items
                If subItem.ItemValue = p_id Then
                    Return subItem
                End If
                Dim subResult As HierarchyItem = GetHierarchyItemFromId(subItem, p_id)
                If Not subResult Is Nothing Then
                    Return subResult
                End If
            Next
        Next
        Return Nothing

    End Function

    Friend Shared Function GetDGVHeight(ByRef DGV As vDataGridView) As Int32

        Dim height As Int32 = DGV_HEIGHT_MARGIN
        Try
            height = height + DGV.ColumnsHierarchy.Items(0).Height
            height = height + (DGV.RowsHierarchy.Items.Count * DGV.RowsHierarchy.Items(0).Height)
        Catch ex As Exception
        End Try
        Return height

    End Function

    Friend Shared Sub CopyValueRight(ByRef DGV As vDataGridView, _
                                               ByRef cell As GridCell)

        Dim row As HierarchyItem = cell.RowItem
        Dim column_index As Int32 = cell.ColumnItem.ItemIndex
        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            If column.ItemIndex > column_index Then DGV.CellsArea.SetCellValue(row, column, cell.Value)
        Next

    End Sub

    Public Shared Sub SetCellFillColor(ByRef cell As GridCell, _
                                       ByRef color_int As Int32)

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(cell.ColumnItem.DataGridView.VIBlendTheme).GridCellStyle
        CStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.FromArgb(color_int))
        cell.DrawStyle = CStyle

    End Sub

    Public Shared Sub SetCellFillColor(ByRef cell As GridCell, _
                                      ByRef color As System.Drawing.Color)

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(cell.ColumnItem.DataGridView.VIBlendTheme).GridCellStyle
        CStyle.FillStyle = New FillStyleSolid(color)
        cell.DrawStyle = CStyle

    End Sub

#End Region


#Region "Positions"

    Friend Shared Function GeneratePositionsDictionary(ByRef p_dataGridView As vDataGridView) As Dictionary(Of Int32, Double)

        Dim positionsDictionary As New Dictionary(Of Int32, Double)
        Dim currentPosition As Int32 = 0
        For Each rowItem As HierarchyItem In p_dataGridView.RowsHierarchy.Items
            AddItemToPositionDictionary(rowItem, currentPosition, positionsDictionary)
        Next
        Return positionsDictionary

    End Function

    Private Shared Sub AddItemToPositionDictionary(ByRef p_rowItem As HierarchyItem, _
                                                   ByRef currentPosition As Int32, _
                                                   ByRef positionsDictionary As Dictionary(Of Int32, Double))

        If (positionsDictionary.ContainsKey(p_rowItem.ItemValue)) Then
            positionsDictionary(p_rowItem.ItemValue) = currentPosition
        Else
            positionsDictionary.Add(p_rowItem.ItemValue, currentPosition)
        End If
        currentPosition += 1
        For Each subRowItem As HierarchyItem In p_rowItem.Items
            AddItemToPositionDictionary(subRowItem, currentPosition, positionsDictionary)
        Next

    End Sub

#End Region



End Class
