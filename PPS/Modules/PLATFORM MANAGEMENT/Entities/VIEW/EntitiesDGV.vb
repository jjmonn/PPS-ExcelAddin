﻿' EntitiesTGV.vb
'
'
' To do: Should inherit from analysisDGV and override init functions !
'
'
' Known bugs: 
'       - dgv row filling crashes if entity has "" value for a category
'           -> will be an issue in implementing no categories value for conso entities
'
'
' Last modified: 21/04/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.Collections
Imports Microsoft.Office.Interop
Imports VIBlend.WinForms.DataGridView.Filters



Friend Class EntitiesDGV


#Region "Instance Variables"

    ' Objects
    Protected Friend DGV As New vDataGridView

    ' Variables
    Private categoriesTV As TreeView
    Private categoriesNameKeyDic As Hashtable
    Protected Friend columnsDictionary As New Dictionary(Of String, HierarchyItem)
    Protected Friend columnsCaptionID As New Dictionary(Of String, String)
    Protected Friend rows_id_item_dic As New Dictionary(Of String, HierarchyItem)
    Private DGVArray(,) As String
    Private filterGroup As New FilterGroup(Of String)()
    Protected Friend currenciesList As List(Of String)
    Protected Friend currentRowItem As HierarchyItem
    Protected Friend isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Friend DGV_FONT_SIZE As Single = 8
    Protected Friend Const CURRENCY_COLUMN_NAME As String = "Currency"
    Private Const NAME_COLUMN_NAME As String = "Name"
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef entitiesTV As TreeView, _
                             ByRef input_categoriesTV As TreeView, _
                             ByRef entities_dict As Dictionary(Of String, Hashtable), _
                             ByRef input_categoriesKeyNameDic As Hashtable)

        currenciesList = CurrenciesMapping.getCurrenciesList(CURRENCIES_KEY_VARIABLE)
        categoriesTV = input_categoriesTV

        initFilters()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize(entitiesTV)
        fillDGV(entities_dict)

        AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler DGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick

    End Sub

    Private Sub InitializeDGVDisplay()

        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AllowResize = True
        'DGV.RowsHierarchy.AllowDragDrop = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.AllowDragDropIndication = True
        DGV.AllowCopyPaste = True
        DGV.FilterDisplayMode = FilterDisplayMode.Custom
        DGV.VIBlendTheme = DGV_VI_BLEND_STYLE
        DGV.BackColor = SystemColors.Control
        'DGV.ImageList = EntitiesIL

    End Sub

#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        DGV.ColumnsHierarchy.Clear()
        columnsDictionary.Clear()

        DGV.ColumnsHierarchy.Items.Add("Entity")
        Dim col1 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(CURRENCY_COLUMN_NAME)
        columnsDictionary.Add(ENTITIES_CURRENCY_VARIABLE, col1)
        columnsCaptionID.Add(CURRENCY_COLUMN_NAME, ENTITIES_CURRENCY_VARIABLE)
        InitCurrenciesCB()
        col1.AllowFiltering = True
        ' CreateFilter(col1)

        For Each rootNode As TreeNode In categoriesTV.Nodes
            Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(rootNode.Text)
            columnsDictionary.Add(rootNode.Name, col)
            columnsCaptionID.Add(rootNode.Text, rootNode.Name)
            InitComboBox(col, rootNode)
            col.AllowFiltering = True
            '   CreateFilter(col)
        Next

    End Sub

    Private Sub CreateFilter(ByRef column As HierarchyItem)

        Dim filter As New HierarchyItemFilter()
        filter.Item = column
        filter.Filter = filterGroup
        DGV.RowsHierarchy.Filters.Add(filter)

    End Sub

    Private Sub initFilters()

        Dim stringFilter1 As New StringFilter()
        stringFilter1.ComparisonOperator = StringFilterComparisonOperator.NOT_EMPTY
        stringFilter1.Value = ""

        Dim stringFilter2 As New StringFilter()
        stringFilter1.ComparisonOperator = StringFilterComparisonOperator.CONTAINS
        stringFilter1.Value = "a"

        filterGroup.AddFilter(FilterOperator.AND, stringFilter1)
        filterGroup.AddFilter(FilterOperator.AND, stringFilter2)

    End Sub

#End Region

#Region "Rows Initialization"

    Private Sub DGVRowsInitialize(ByRef entities_tv As TreeView)

        DGV.RowsHierarchy.Clear()
        rows_id_item_dic.Clear()

        isFillingDGV = True
        For Each node In entities_tv.Nodes
            Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add("")
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), node.text)
            FormatRow(row, node.name)
            addChildrenRows(node, row)
        Next
        isFillingDGV = False

    End Sub

    Private Sub addChildrenRows(ByRef node As TreeNode, ByRef row As HierarchyItem)

        For Each child_node In node.Nodes
            Dim sub_row As HierarchyItem = row.Items.Add("")
            DGV.CellsArea.SetCellValue(sub_row, DGV.ColumnsHierarchy.Items(0), child_node.Text)
            FormatRow(sub_row, child_node.Name)
            addChildrenRows(child_node, sub_row)
        Next

    End Sub

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef row_id As String)

        rows_id_item_dic.Add(row_id, row)
        row.TextAlignment = ContentAlignment.MiddleLeft

    End Sub

#End Region

#Region "ComboBoxes Utilities"

    Private Sub InitComboBox(ByRef columnHierarchyItem As HierarchyItem, _
                             ByRef inputNode As TreeNode)


        Dim comboBox As New ComboBoxEditor()
        comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        comboBox.DropDownWidth = CB_WIDTH

        Dim tmpDictionary As New Dictionary(Of String, String)
        For Each node As TreeNode In inputNode.Nodes
            comboBox.Items.Add(node.Text)
            tmpDictionary.Add(node.Text, node.Name)
        Next

        columnHierarchyItem.CellsEditor = comboBox
        AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

    Private Sub InitCurrenciesCB()

        Dim comboBox As New ComboBoxEditor()
        comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        comboBox.DropDownWidth = CB_WIDTH
        For Each currency_ As String In currenciesList
            comboBox.Items.Add(currency_)
        Next

        columnsDictionary(ENTITIES_CURRENCY_VARIABLE).CellsEditor = comboBox
        AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

#End Region

    Private Sub fillDGV(ByRef entities_dict As Dictionary(Of String, Hashtable))

        isFillingDGV = True
        Dim column As HierarchyItem
        Dim category_value As String
        For Each entity_id In entities_dict.Keys
            Dim rowItem = rows_id_item_dic(entity_id)

            column = columnsDictionary(ENTITIES_CURRENCY_VARIABLE)
            DGV.CellsArea.SetCellValue(rowItem, column, entities_dict(entity_id)(ENTITIES_CURRENCY_VARIABLE))

            For Each root_category_node As TreeNode In categoriesTV.Nodes
                column = columnsDictionary(root_category_node.Name)
                category_value = categoriesTV.Nodes.Find((entities_dict(entity_id)(root_category_node.Name)), True)(0).Text
                DGV.CellsArea.SetCellValue(rowItem, column, category_value)
            Next
            If entities_dict(entity_id)(ENTITIES_ALLOW_EDITION_VARIABLE) = 0 Then rowItem.ImageIndex = 0 Else rowItem.ImageIndex = 1

        Next
        isFillingDGV = False
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_FONT_SIZE, DGV_FONT_SIZE)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

#End Region


#Region "DGV Events"

    Friend Sub comboTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Escape Then
            If e.KeyCode = Keys.Enter Then
                DGV.CloseEditor(True)
            Else
                DGV.CloseEditor(False)
            End If
        End If

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        currentRowItem = args.HierarchyItem

    End Sub

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        currentRowItem = args.Cell.RowItem

    End Sub

#End Region


#Region "Copy Down"

    Friend Sub CopyValueDown()

        If Not DGV.CellsArea.SelectedCells(0) Is Nothing Then
            Dim row As HierarchyItem = DGV.CellsArea.SelectedCells(0).RowItem
            Dim column As HierarchyItem = DGV.CellsArea.SelectedCells(0).ColumnItem
            Dim value As String = DGV.CellsArea.SelectedCells(0).Value

            SetValueToChildrenItems(row, column, value)
            DGV.Refresh()
            DGV.Select()
        End If

    End Sub

    Private Sub SetValueToChildrenItems(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        For Each childItem As HierarchyItem In rowItem_.Items
            DGV.CellsArea.SetCellValue(childItem, columnItem_, value)
            If childItem.Items.Count > 0 Then SetValueToChildrenItems(childItem, columnItem_, value)
        Next

    End Sub

    Private Sub SetValueToSibbling(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        Dim parent As HierarchyItem = rowItem_.ParentItem
        Dim currentItem = rowItem_
        While Not currentItem.ItemBelow Is Nothing

            currentItem = currentItem.ItemBelow
            If currentItem.ParentItem.GetUniqueID = parent.GetUniqueID Then
                DGV.CellsArea.SetCellValue(currentItem, columnItem_, value)
                If currentItem.Items.Count > 0 Then SetValueToChildrenItems(currentItem, columnItem_, value)
            End If

        End While

    End Sub


#End Region


#Region "Excel Drop"

    ' Drop the current TGV in a new worksheet
    Friend Sub DropInExcel()

        Dim cell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(ENTITIES_TABLE, _
                                                                                {"Entities Hierarchy"}, _
                                                                                {""})
        Dim nbRows As Int32
        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            DGVRowsCount(item, nbRows)
        Next

        Dim nbcols As Int32 = DGV.ColumnsHierarchy.Items.Count
        ReDim DGVArray(nbRows + 1, nbcols + 2)
        Dim i, j As Int32

        DGVArray(i, 0) = "Entity's Name"
        DGVArray(i, 1) = "Entity's Affiliate's Name"
        For j = 0 To nbcols - 1
            DGVArray(i, j + 2) = DGV.ColumnsHierarchy.Items(j).Caption
        Next

        For Each row In DGV.RowsHierarchy.Items
            fillInDGVArrayRow(row, 1)
        Next

        WorksheetWrittingFunctions.WriteArray(DGVArray, cell)
        ExcelEntitiesReportFormatting.FormatEntitiesReport(GlobalVariables.APPS.ActiveSheet.range(cell, cell.Offset(UBound(DGVArray, 1), UBound(DGVArray, 2))))

    End Sub

    ' Recursively fill in row array
    Private Sub fillInDGVArrayRow(ByRef inputRow As HierarchyItem, ByRef rowIndex As Int32)

        Dim j As Int32

        DGVArray(rowIndex, 0) = DGV.CellsArea.GetCellValue(inputRow, DGV.ColumnsHierarchy.Items(0))
        If Not inputRow.ParentItem Is Nothing Then
            DGVArray(rowIndex, 1) = DGV.CellsArea.GetCellValue(inputRow.ParentItem, DGV.ColumnsHierarchy.Items(0))
        Else
            DGVArray(rowIndex, 1) = ""
        End If

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            DGVArray(rowIndex, j + 2) = DGV.CellsArea.GetCellValue(inputRow, column)
            j = j + 1
        Next
        rowIndex = rowIndex + 1

        For Each childRow As HierarchyItem In inputRow.Items
            fillInDGVArrayRow(childRow, rowIndex)
        Next


    End Sub

    Private Sub DGVRowsCount(ByRef item As HierarchyItem, ByRef nbrows As Int32)

        ' -> maybe use tv total nodes count
        nbrows = nbrows + item.Items.Count
        For Each row As HierarchyItem In item.Items
            DGVRowsCount(row, nbrows)
        Next

    End Sub


#End Region


End Class
