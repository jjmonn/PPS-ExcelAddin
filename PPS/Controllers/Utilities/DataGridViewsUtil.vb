﻿' DataGridViewUtil.vb
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
' Last modified date: 27/01/2014
' Author: Julien Monnereau


Imports System.Drawing
Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports SpannedDataGridViewNet2
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.Utilities
Imports Microsoft.Office.Interop


Friend Class DataGridViewsUtil


#Region "Instance Variables"

    ' Objects
    Private EXCELFORMATER As CExcelFormatting
    Private EntitiesTokenNamesDict As Hashtable
    Private AccountsKeysFormatTypesDic As Dictionary(Of String, Dictionary(Of String, String))
    Private AccountNamesKeysDic As Hashtable
    Private accFtypes As Hashtable

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


#End Region


    Protected Friend Sub New()

        EntitiesTokenNamesDict = EntitiesMapping.GetEntitiesDictionary(ASSETS_TREE_ID_VARIABLE, ASSETS_NAME_VARIABLE)
        AccountsKeysFormatTypesDic = AccountsMapping.GetAccountsKeysFormatsTypesDictionary()
        AccountNamesKeysDic = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_ID_VARIABLE)
        accFtypes = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        EXCELFORMATER = New CExcelFormatting

    End Sub


#Region "ControllingUI2 DGVs Formatting"

    ' vDgv DISPLAY Initialization
    Protected Friend Shared Sub InitDisplayVDataGridView(ByRef vDataGridView As vDataGridView, _
                                                         ByRef theme As VIBLEND_THEME)

        With vDataGridView
            .VIBlendTheme = theme
            .GroupingDefaultHeaderTextVisible = True
            .BackColor = Color.White
            .GridLinesDisplayMode = GridLinesDisplayMode.DISPLAY_NONE
            .ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            '.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            '.ColumnsHierarchy.AutoStretchColumns = True
            '.FilterDisplayMode = FilterDisplayMode.Basic
        End With



    End Sub

    ' vDgv Display After Populating ' note controlling ui2 specificity !!
    Protected Friend Sub FormatDGVs(ByRef tabsControl As TabControl)

        Dim formatCode, account_id, fmtStr As String
        Dim indent As Int32
        For Each tab_ As TabPage In tabsControl.TabPages
            Dim vDgv As vDataGridView = tab_.Controls(0)
            For Each row In vDgv.RowsHierarchy.Items
                formatCode = AccountsKeysFormatTypesDic.Item(AccountNamesKeysDic.Item(row.Caption)).Item(ACCOUNT_FORMAT_VARIABLE)
                account_id = AccountNamesKeysDic.Item(row.Caption)

                Dim HANStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(vDgv.VIBlendTheme).HierarchyItemStyleNormal
                Dim HASStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(vDgv.VIBlendTheme).HierarchyItemStyleSelected
                Dim HENStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(ENTITIES_ROWS_THEME).HierarchyItemStyleNormal
                Dim HESStyle As VIBlend.Utilities.HierarchyItemStyle = GridTheme.GetDefaultTheme(ENTITIES_ROWS_THEME).HierarchyItemStyleSelected
                Dim CAStyle As GridCellStyle = GridTheme.GetDefaultTheme(vDgv.VIBlendTheme).GridCellStyle
                Dim CEStyle As GridCellStyle = GridTheme.GetDefaultTheme(ENTITIES_ROWS_THEME).GridCellStyle

                HANStyle.Font = New System.Drawing.Font(vDgv.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
                HASStyle.Font = New System.Drawing.Font(vDgv.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
                HENStyle.Font = New System.Drawing.Font(vDgv.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
                HESStyle.Font = New System.Drawing.Font(vDgv.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
                CAStyle.Font = New System.Drawing.Font(vDgv.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
                CEStyle.Font = New System.Drawing.Font(vDgv.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)

                If EXCELFORMATER.InputFormatsDictionary.Item(formatCode).Item(FORMAT_BOLD_VARIABLE) = 1 Then
                    HANStyle.Font = New System.Drawing.Font(vDgv.Font, FontStyle.Bold)
                    HASStyle.Font = New System.Drawing.Font(vDgv.Font, FontStyle.Bold)
                    CAStyle.Font = New System.Drawing.Font(vDgv.Font, FontStyle.Bold)
                End If
                If EXCELFORMATER.InputFormatsDictionary.Item(formatCode).Item(FORMAT_ITALIC_VARIABLE) = 1 Then
                    HANStyle.Font = New System.Drawing.Font(vDgv.Font, FontStyle.Italic)
                    HASStyle.Font = New System.Drawing.Font(vDgv.Font, FontStyle.Italic)
                    CAStyle.Font = New System.Drawing.Font(vDgv.Font, FontStyle.Italic)
                End If
                If formatCode = "l" Then
                    CAStyle.TextColor = Color.Gray
                End If
                Select Case (AccountsKeysFormatTypesDic.Item(account_id).Item(ACCOUNT_TYPE_VARIABLE))
                    Case "MO" : fmtStr = "{0:C0}"
                    Case "RA" : fmtStr = "{0:P}"        ' put this in a table
                    Case "OP" : fmtStr = "{0:N}"        ' further evolution set unit
                    Case "NU" : fmtStr = "{0:N2}"
                    Case Else : fmtStr = "{0:C0}"
                End Select

                indent = EXCELFORMATER.InputFormatsDictionary.Item(formatCode).Item(FORMAT_INDENT_VARIABLE)
                If row.ParentItem Is Nothing Then
                    format_row(row, formatCode, fmtStr, CAStyle, HANStyle, HASStyle, indent, CAStyle, CEStyle, HANStyle, HASStyle, HENStyle, HESStyle)
                Else
                    format_row(row, formatCode, fmtStr, CEStyle, HENStyle, HESStyle, indent, CAStyle, CEStyle, HANStyle, HASStyle, HENStyle, HESStyle)
                End If
            Next

            If vDgv.ColumnsHierarchy.Items(0).Items.Count = 0 Then
                For Each item In vDgv.ColumnsHierarchy.Items
                    item.Width = FIXED_SINGLE_HIERARCHY_COLUMN_WIDTH
                Next
            Else
                vDgv.ColumnsHierarchy.ExpandAllItems()
                For Each item In vDgv.ColumnsHierarchy.Items
                    AdjustChildrenHierarchyItemSize(item)
                Next
            End If
            vDgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            ' vdgv.ColumnsHierarchy.AutoStretchColumns = True
            vDgv.Refresh()
        Next

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

    Protected Friend Shared Sub AdjustChildrenHierarchyItemSize(ByRef item As HierarchyItem)

        item.AutoResize(AutoResizeMode.FIT_ALL)
        item.TextAlignment = ContentAlignment.MiddleRight
        If item.Items.Count > 0 Then
            For Each subItem As HierarchyItem In item.Items
                subItem.AutoResize(AutoResizeMode.FIT_ALL)
                If subItem.Items.Count > 0 Then AdjustChildrenHierarchyItemSize(subItem)
            Next
        End If

    End Sub

    Protected Friend Shared Sub EqualizeColumnsAndRowsHierarchyWidth(ByRef DGV As vDataGridView)

        Dim width As Int32 = DGV.Width / (DGV.ColumnsHierarchy.Items.Count + 1)
        DGV.RowsHierarchy.Items(0).Width = width
        For Each item In DGV.ColumnsHierarchy.Items
            item.Width = width
        Next

    End Sub


#End Region


#Region "Font Size"

    Protected Friend Shared Sub DGVSetHiearchyFontSize(ByRef DGV As vDataGridView, _
                                                     itemsFontSize As Single, _
                                                     cellsFontSize As Single)

        Dim itemStyleNormal As HierarchyItemStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).HierarchyItemStyleNormal
        itemStyleNormal.Font = New System.Drawing.Font(DGV.Font.FontFamily, itemsFontSize)
        Dim itemStyleSelected As HierarchyItemStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).HierarchyItemStyleSelected
        itemStyleSelected.Font = New System.Drawing.Font(DGV.Font.FontFamily, itemsFontSize)

        Dim CStyleNormal As GridCellStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).GridCellStyle
        CStyleNormal.Font = New System.Drawing.Font(DGV.Font.FontFamily, cellsFontSize, FontStyle.Regular)

        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            SetSubItemsFontSizes(item, _
                                 itemStyleNormal, _
                                 itemStyleSelected, _
                                 CStyleNormal)
        Next
        For Each item In DGV.ColumnsHierarchy.Items
            SetSubItemsFontSizes(item, _
                                 itemStyleNormal, _
                                 itemStyleSelected, _
                                 CStyleNormal)
        Next

    End Sub

    Protected Friend Shared Sub SetSubItemsFontSizes(ByRef item As HierarchyItem, _
                                                     ByRef itemStyleNormal As HierarchyItemStyle, _
                                                     ByRef itemStyleSelected As HierarchyItemStyle, _
                                                     ByRef cStyleNormal As GridCellStyle)

        item.HierarchyItemStyleNormal = itemStyleNormal
        item.HierarchyItemStyleSelected = itemStyleSelected
        item.CellsStyle = cStyleNormal
        For Each subItem As HierarchyItem In item.Items
            SetSubItemsFontSizes(subItem, _
                                 itemStyleNormal, _
                                 itemStyleSelected, _
                                 cStyleNormal)
        Next

    End Sub

    Protected Friend Shared Sub SetDGVCellsFontSize(ByRef DGV As vDataGridView, ByRef fontSize As Single)

        Dim CStyleN As GridCellStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).GridCellStyle
        CStyleN.Font = New System.Drawing.Font(DGV.Font.FontFamily, fontSize, FontStyle.Regular)

        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            SetItemsCellsFontSize(item, CStyleN)
        Next

    End Sub

    Protected Friend Shared Sub SetItemsCellsFontSize(ByRef item As HierarchyItem, _
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
                                             ByRef periodList() As Integer, _
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
                                     ByRef periodList() As Integer, _
                                     ByRef Versions() As String, _
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
    Protected Friend Function CreatesVDataGridViewRowsHierarchy(ByRef dgv As vDataGridView, _
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
                                                          EntitiesTokenNamesDict,
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
                                                      KeyLineDictionary, _
                                                      EntitiesTokenNamesDict, _
                                                      , entity_node)
            Next
        End If
        Return KeyLineDictionary

    End Function


    ' Recursively creates rows and sub rows for the current node and children - Case SEVERAL ENTITIES
    Private Sub FillInSubRowsHierarchySeveralEntities(ByRef account_node As TreeNode, _
                                                      ByRef line_index As Int32, _
                                                      ByRef dgv As vDataGridView, _
                                                      ByRef KeyLineDictionary As Dictionary(Of String, Int32), _
                                                      ByRef entities_token_names_dict As Hashtable, _
                                                      Optional ByRef entitiesArray() As String = Nothing, _
                                                      Optional ByRef entity_node As TreeNode = Nothing)

        ' -> currently no subtitles -> all nodes are processed ' -> list of processed nodes from ACCMAPP
        Dim row As HierarchyItem = dgv.RowsHierarchy.Items.Add(account_node.Text)
        KeyLineDictionary.Add(account_node.Name, line_index)
        line_index = line_index + 1
        If Not entitiesArray Is Nothing Then
            For i As Int32 = 1 To UBound(entitiesArray)
                Dim sub_row As HierarchyItem = row.Items.Add(entities_token_names_dict.Item(entitiesArray(i)))
            Next
        Else
            For Each child In entity_node.Nodes
                SetUpRowsHierarchyEntitiesHierarchy(row, child, dgv, entities_token_names_dict)
            Next
        End If
        If account_node.Nodes.Count > 0 Then
            For Each account As TreeNode In account_node.Nodes
                If accFtypes(account) <> TITLE_FORMAT_CODE Then
                    FillInSubRowsHierarchySeveralEntities(account, _
                                                          line_index, _
                                                          dgv, _
                                                          KeyLineDictionary, _
                                                          entities_token_names_dict,
                                                          entitiesArray,
                                                          entity_node)
                End If
            Next
        End If

    End Sub

    ' Recursively creates rows and sub rows for the current node and children - Case ONE ENTITY
    Protected Friend Sub FillInSubRowsHierarchyOneEntity(ByRef inputNode As TreeNode, _
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
                                                    ByRef DGV As vDataGridView, _
                                                    ByRef entities_token_names_dict As Hashtable)

        If entity_node.Checked = True Then
        Dim sub_row As HierarchyItem = row.Items.Add(EntitiesTokenNamesDict.Item(entity_node.Name))
        For Each child In entity_node.Nodes
            SetUpRowsHierarchyEntitiesHierarchy(sub_row, child, DGV, entities_token_names_dict)
        Next
        End If

    End Sub


#End Region


#Region "Alternative Scenarios Report DGV Utility"

    Protected Friend Shared Function CreateASDGVReport(ByRef period_list As List(Of Integer), ByRef time_config As String) As vDataGridView

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

    Protected Friend Shared Sub AddSerieToBasicDGVReport(ByRef row As HierarchyItem, _
                                                         ByRef serie_name As String, _
                                                         ByRef values As Double())

        ' evolution -> border + bold around  important items !!
        Dim sub_row As HierarchyItem = row.Items.Add("")
        row.DataGridView.CellsArea.SetCellValue(sub_row, row.DataGridView.ColumnsHierarchy.Items(0), serie_name)
        For j As Int32 = 1 To row.DataGridView.ColumnsHierarchy.Items.Count - 1
            row.DataGridView.CellsArea.SetCellValue(sub_row, row.DataGridView.ColumnsHierarchy.Items(j), values(j - 1))
        Next

    End Sub

    Protected Friend Shared Sub FormatBasicDGV(ByRef DGV As vDataGridView)

        DGV.ColumnsHierarchy.AutoStretchColumns = True
        AdjustDGVFColumnWidth(DGV, 0)
        Try
            DGV.Refresh()
            DGV.RowsHierarchy.Items(0).Selected = True
        Catch ex As Exception

        End Try

    End Sub

#End Region


#Region "Adjustments Formatting"

    Protected Friend Shared Sub FormatAdjustmentRow(ByRef row As HierarchyItem)

        Dim itemStyleNormal As HierarchyItemStyle = GridTheme.GetDefaultTheme(ADJUSTMENTS_ROW_THEME).HierarchyItemStyleNormal
        Dim itemStyleSelected As HierarchyItemStyle = GridTheme.GetDefaultTheme(ADJUSTMENTS_ROW_THEME).HierarchyItemStyleSelected
        Dim CStyleNormal As GridCellStyle = GridTheme.GetDefaultTheme(ADJUSTMENTS_ROW_THEME).GridCellStyle

        ' itemStyleNormal.TextColor = ADJUSTMENTS_COLOR
        ' itemStyleSelected.TextColor = ADJUSTMENTS_COLOR
        CStyleNormal.TextColor = ADJUSTMENTS_COLOR

        itemStyleSelected.Font = New System.Drawing.Font(row.DataGridView.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
        itemStyleNormal.Font = New System.Drawing.Font(row.DataGridView.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE)
        CStyleNormal.Font = New System.Drawing.Font(row.DataGridView.Font.FontFamily, BASIC_DGV_REPORT_FONT_SIZE, FontStyle.Regular)

        row.HierarchyItemStyleNormal = itemStyleNormal
        row.HierarchyItemStyleSelected = itemStyleSelected
        row.CellsStyle = CStyleNormal

        row.CellsFormatString = row.ParentItem.CellsFormatString
        row.CellsTextAlignment = ContentAlignment.MiddleRight

    End Sub


#End Region


#Region "Send to Excel Functions"

    ' One Version - Top Entity
    Protected Friend Function WriteCurrentEntityToExcel(ByRef destination As Excel.Range, ByRef DatagridView As vDataGridView) As Int32

        Dim i As Int32 = 1
        Dim j As Int32 = 1
        APPS.ScreenUpdating = False
        destination.Value2 = DatagridView.Name

        For Each item As HierarchyItem In DatagridView.ColumnsHierarchy.Items
            destination.Offset(0, j).Value2 = item.Caption
            j = j + 1
        Next

        For Each row As HierarchyItem In DatagridView.RowsHierarchy.Items
            destination.Offset(i, 0).Value2 = row.Caption
            j = 1
            For Each column As HierarchyItem In DatagridView.ColumnsHierarchy.Items
                destination.Offset(i, j).Value2 = DatagridView.CellsArea.GetCellValue(row, column)
                j = j + 1
            Next
            i = i + 1
        Next

        Dim test = destination.Row
        EXCELFORMATER.FormatExcelRangeAs(destination.Worksheet.Range(destination, destination.Offset(i - 1, DatagridView.ColumnsHierarchy.Items.Count)), _
                                         INPUT_FORMAT_CODE)
        APPS.ScreenUpdating = True
        Return i + 1

    End Function

    ' Several Versions, Top Entity
    Protected Friend Function writeControllingCurrentEntityToExcel(ByRef destination As Excel.Range, _
                                                         ByRef dataGridView As vDataGridView) As Integer

        Dim i As Int32 = 1
        Dim j As Int32 = 1

        APPS.ScreenUpdating = False
        destination.Offset(1, 0).Value2 = dataGridView.Name
        destination.Offset(1, 0).Rows.Font.Bold = True

        ' Headers
        For Each item As HierarchyItem In dataGridView.ColumnsHierarchy.Items
            destination.Offset(0, j).Value2 = item.Caption
            destination.Worksheet.Range(destination.Offset(0, j), _
                                        destination.Offset(0, j + dataGridView.ColumnsHierarchy.Items(0).Items.Count)). _
                                        HorizontalAlignment = HorizontalAlignment.Center

            For Each subItem In item.Items
                destination.Offset(1, j).Value2 = subItem.Caption
                j = j + 1
            Next
        Next

        ' Values
        i = 2
        For Each row As HierarchyItem In dataGridView.RowsHierarchy.Items
            destination.Offset(i, 0).Value2 = row.Caption
            j = 1
            For Each column As HierarchyItem In dataGridView.ColumnsHierarchy.Items
                For Each item As HierarchyItem In column.Items
                    destination.Offset(i, j).Value2 = dataGridView.CellsArea.GetCellValue(row, item)
                    j = j + 1
                Next
            Next
            i = i + 1
        Next

        Dim test = destination.Row
        EXCELFORMATER.FormatExcelRangeAs(destination.Worksheet.Range(destination.Offset(1, 0), _
                                       destination.Offset(i - 1, (dataGridView.ColumnsHierarchy.Items(0).Items.Count * _
                                       dataGridView.ColumnsHierarchy.Items.Count))), _
                                       INPUT_FORMAT_CODE)
        APPS.ScreenUpdating = True
        Return i + 1

    End Function

    ' improve process -> here only write down on excel
    ' formats taken care in controllingDropOnExcel
#Region "Generic DGV Export to Excel"


    Protected Friend Shared Function CopyDGVToExcelGeneric(ByRef DGV As vDataGridView, _
                                                      ByRef dest_range As Excel.Range, _
                                                      ByRef info_array As String())

        Dim i As Int32 = 1
        Dim j As Int32 = 1
        Dim nb_columns_floors As Int32 = 1
     
        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            SetupColumnsTitles(column, dest_range, i, j, nb_columns_floors)
        Next

        j = 0
        i = i + 1
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items
            SetupRowsTitles(row, dest_range, i, j)
        Next

        j = 1
        i = nb_columns_floors + 1
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items
            CopyRowHierarchy(row, dest_range, i, j)
            i = i + 1
        Next
        Return i

    End Function

    Protected Friend Shared Sub SetupColumnsTitles(ByRef column As HierarchyItem, _
                                                   ByRef range As Excel.Range, _
                                                   ByRef i As Int32, _
                                                   ByRef j As Int32, _
                                                   ByRef parent_j As Int32)

        range.Offset(i, j).Value = column.Caption
        j = j + 1
        If column.Items.Count > 0 Then
            For Each sub_column As HierarchyItem In column.Items
                i = i + 1
                SetupColumnsTitles(sub_column, range, i, j, parent_j)
            Next
            parent_j = parent_j + column.Items.Count
        End If

    End Sub

    Protected Friend Shared Sub SetupRowsTitles(ByRef row As HierarchyItem, _
                                                  ByRef range As Excel.Range, _
                                                  ByRef i As Int32, _
                                                  ByRef j As Int32)

        range.Offset(i, j).Value = row.Caption
        i = i + 1
        For Each sub_row As HierarchyItem In row.Items
            SetupRowsTitles(sub_row, range, i, j)
        Next

    End Sub

    Protected Friend Shared Sub CopyRowHierarchy(ByRef row As HierarchyItem, _
                                                 ByRef range As Excel.Range, _
                                                 ByRef i As Int32, ByRef j As Int32)

        j = 1
        For Each column As HierarchyItem In row.DataGridView.ColumnsHierarchy.Items
            CopyColumnHierarchy(row, column, range, i, j)
        Next
        i = i + 1
        For Each sub_row In row.Items
            CopyRowHierarchy(sub_row, range, i, j)
        Next

    End Sub

    Protected Friend Shared Sub CopyColumnHierarchy(ByRef row As HierarchyItem, ByRef column As HierarchyItem, _
                                                    ByRef range As Excel.Range, _
                                                    ByRef i As Int32, ByRef j As Int32)

        range.Offset(i, j).Value = row.DataGridView.CellsArea.GetCellValue(row, column)
        j = j + 1
        For Each sub_column In column.Items
            CopyColumnHierarchy(row, sub_column, range, i, j)
        Next

    End Sub

#End Region

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

    Protected Friend Shared Sub FormatDGVRowsHierarchy(ByRef dgv As vDataGridView)

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

    Protected Friend Shared Sub AdjustDGVFColumnWidth(ByRef DGV As vDataGridView, _
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
        DGV.ColumnsHierarchy.Items(column_index).Width = maxLength * characters_size

    End Sub

    Protected Friend Shared Sub GetItemMaxLength(ByRef item As HierarchyItem, _
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

    Protected Friend Shared Sub GetColumnMaxLength(ByRef item As HierarchyItem, _
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


#End Region


End Class
