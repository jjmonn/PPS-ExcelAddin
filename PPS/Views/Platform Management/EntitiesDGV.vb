' EntitiesTGV.vb
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
' Last modified: 04/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.Collections
Imports Microsoft.Office.Interop
Imports VIBlend.WinForms.DataGridView.Filters
Imports System.Linq
Imports VIBlend.WinForms.Controls



Friend Class EntitiesDGV


#Region "Instance Variables"

    ' Objects
    Friend DGV As New vDataGridView

    ' Variables
    Private entitiesFilterTV As TreeView
    Private entitiesFilterValuesTV As TreeView
    Private categoriesNameKeyDic As Hashtable
    Friend columnsVariableItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend rows_id_item_dic As New Dictionary(Of Int32, HierarchyItem)

    Private DGVArray(,) As String
    Private filterGroup As New FilterGroup(Of String)()
    Friend currentRowItem As HierarchyItem
    Friend isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Friend Const CURRENCY_COLUMN_NAME As String = "Currency"
    Friend Const NAME_COLUMN_NAME As String = "Name"
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7

#End Region


#Region "Initialize"

    Friend Sub New(ByRef entitiesTV As TreeView, _
                   ByRef input_entitiesFilterTV As TreeView, _
                   ByRef p_entitiesFilterValuesTV As TreeView, _
                   ByRef input_categoriesKeyNameDic As Hashtable)

        entitiesFilterTV = input_entitiesFilterTV
        entitiesFilterValuesTV = p_entitiesFilterValuesTV

        initFilters()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize(entitiesTV)
        fillDGV()

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

    Private Sub fillDGV()

        isFillingDGV = True
        For Each entity_id In GlobalVariables.Entities.entities_hash.Keys
            fillRow(entity_id, GlobalVariables.Entities.entities_hash(entity_id))
        Next
        isFillingDGV = False
        updateDGVFormat()

    End Sub


#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        DGV.ColumnsHierarchy.Clear()
        columnsVariableItemDictionary.Clear()

        ' Entities Name Column
        Dim nameColumn As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Entity")
        nameColumn.ItemValue = NAME_VARIABLE

        Dim nameTextBox As New TextBoxEditor()
        nameTextBox.ActivationFlags = EditorActivationFlags.KEY_PRESS_ENTER
        nameColumn.CellsEditor = nameTextBox
        columnsVariableItemDictionary.Add(NAME_VARIABLE, nameColumn)

        ' Entities Currency Column
        Dim currencyColumn As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(CURRENCY_COLUMN_NAME)
        columnsVariableItemDictionary.Add(ENTITIES_CURRENCY_VARIABLE, currencyColumn)
        currencyColumn.ItemValue = ENTITIES_CURRENCY_VARIABLE
        InitCurrenciesComboBox()
        currencyColumn.AllowFiltering = True
        ' CreateFilter(col1)

        For Each rootNode As TreeNode In entitiesFilterTV.Nodes
            CreateSubFilters(rootNode)
            '   CreateFilter(col)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As TreeNode)

        Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(node.Text)

        columnsVariableItemDictionary.Add(node.Name, col)
        col.ItemValue = node.Name
        col.AllowFiltering = True

        For Each childNode As TreeNode In node.Nodes
            CreateSubFilters(childNode)
        Next

    End Sub

    'Private Sub CreateFilter(ByRef column As HierarchyItem)

    '    Dim filter As New HierarchyItemFilter()
    '    filter.Item = column
    '    filter.Filter = filterGroup
    '    DGV.RowsHierarchy.Filters.Add(filter)

    'End Sub

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
        For Each node In entities_tv.Nodes
            addRow(node)
        Next

    End Sub

    Friend Sub addRow(ByRef node As TreeNode, _
                      Optional ByRef parent_row As HierarchyItem = Nothing)

        isFillingDGV = True
        Dim row As HierarchyItem
        If parent_row Is Nothing Then
            row = DGV.RowsHierarchy.Items.Add("")
        Else
            row = parent_row.Items.Add("")
        End If
        row.ItemValue = CInt(node.Name)
        DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), node.Text)
        FormatRow(row, CInt(node.Name))
        For Each child_node In node.Nodes
            addRow(child_node, row)
        Next
        isFillingDGV = False

    End Sub

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef row_id As Int32)

        If rows_id_item_dic.ContainsKey(row_id) Then
            rows_id_item_dic(row_id) = row
        Else
            rows_id_item_dic.Add(row_id, row)
        End If
        row.TextAlignment = ContentAlignment.MiddleLeft

    End Sub

#End Region


#Region "ComboBoxes Utilities"

    'Private Sub InitComboBox(ByRef columnHierarchyItem As HierarchyItem, _
    '                         ByRef inputNode As TreeNode)

    '    Dim comboBox As New ComboBoxEditor()
    '    comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
    '    comboBox.DropDownWidth = CB_WIDTH

    '    For Each node As TreeNode In inputNode.Nodes
    '        comboBox.Items.Add(node.Text)
    '    Next

    '    columnHierarchyItem.CellsEditor = comboBox
    '    AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    'End Sub

    Private Sub InitCurrenciesComboBox()

        Dim comboBox As New ComboBoxEditor()
        comboBox.DropDownList = True
        comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        comboBox.DropDownWidth = CB_WIDTH
        For Each currencyId As Int32 In GlobalVariables.Currencies.currencies_hash.Keys
            comboBox.Items.Add(GlobalVariables.Currencies.currencies_hash(currencyId)(NAME_VARIABLE))
        Next

        columnsVariableItemDictionary(ENTITIES_CURRENCY_VARIABLE).CellsEditor = comboBox
        AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

#End Region

   
#End Region


#Region "DGV Filling"

    Friend Sub FillRow(ByVal entity_id As Int32, _
                       ByVal entity_ht As Hashtable)

        Dim rowItem = rows_id_item_dic(entity_id)
        Dim column As HierarchyItem = columnsVariableItemDictionary(ENTITIES_CURRENCY_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, columnsVariableItemDictionary(NAME_VARIABLE), entity_ht(NAME_VARIABLE))
        DGV.CellsArea.SetCellValue(rowItem, column, GlobalVariables.Currencies.currencies_hash(CInt(entity_ht(ENTITIES_CURRENCY_VARIABLE)))(NAME_VARIABLE))
        For Each filterNode As TreeNode In entitiesFilterTV.Nodes
            FillSubFilters(filterNode, entity_id, rowItem)
        Next
        If entity_ht(ENTITIES_ALLOW_EDITION_VARIABLE) = 0 Then rowItem.ImageIndex = 0 Else rowItem.ImageIndex = 1

    End Sub

    Private Sub FillSubFilters(ByRef filterNode As TreeNode, _
                               ByRef entity_id As Int32, _
                               ByRef rowItem As HierarchyItem)

        Dim combobox As New ComboBoxEditor
        combobox.DropDownList = True
        Dim columnItem As HierarchyItem = columnsVariableItemDictionary(filterNode.Name)
        Dim filterValueId = GlobalVariables.EntitiesFilters.GetFilterValueId(CInt(filterNode.Name), entity_id)
        Dim filter_value_name = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(NAME_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, columnItem, filter_value_name)

        ' Filters Choices Setup
        If filterNode.Parent Is Nothing Then
            ' Root Filter
            Dim valuesNames = GlobalVariables.FiltersValues.GetFiltervaluesList(filterNode.Name, NAME_VARIABLE)
            For Each valueName As String In valuesNames
                combobox.Items.Add(valueName)
            Next
        Else
            ' Child Filter
            Dim parentFilterFilterValueId As Int32 = GlobalVariables.EntitiesFilters.GetFilterValueId(filterNode.Parent.Name, entity_id)     ' Child Filter Id
            Dim filterValuesIds = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds({parentFilterFilterValueId})
            For Each Id As Int32 In filterValuesIds
                combobox.Items.Add(GlobalVariables.FiltersValues.filtervalues_hash(Id)(NAME_VARIABLE))
            Next
        End If
     
        ' Add ComboBoxEditor to Cell
        DGV.CellsArea.SetCellEditor(rowItem, columnItem, combobox)

        ' Recursive if Filters Children exist
        For Each childFilterNode As TreeNode In filterNode.Nodes
            FillSubFilters(childFilterNode, entity_id, rowItem)
        Next

    End Sub

    Private Sub updateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

#End Region


#Region "DGV Updates"

    ' Update Parents and Children Filters Cells or comboboxes after a filter cell has been edited
    Friend Sub UpdateEntitiesFiltersAfterEdition(ByRef entityId As Int32, _
                                                 ByRef filterId As Int32, _
                                                 ByRef filterValueId As Int32)

        isFillingDGV = True
        Dim filterNode As TreeNode = entitiesFilterTV.Nodes.Find(filterId, True)(0)

        ' Update parent filters recursively
        UpdateParentFiltersValues(rows_id_item_dic(entityId), _
                                 entityId, _
                                 filterNode, _
                                 filterValueId)

        ' Update children filters comboboxes recursively
        For Each childFilterNode As TreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(rows_id_item_dic(entityId), _
                                            childFilterNode, _
                                            {filterValueId})
        Next
        isFillingDGV = False

    End Sub

    Private Sub UpdateParentFiltersValues(ByRef row As HierarchyItem, _
                                          ByRef entityId As Int32, _
                                          ByRef filterNode As TreeNode,
                                          ByRef filterValueId As Int32)

        If Not filterNode.Parent Is Nothing Then
            Dim parentFilterNode As TreeNode = filterNode.Parent
            Dim column As HierarchyItem = columnsVariableItemDictionary(parentFilterNode.Name)
            Dim parentFilterValueId As Int32 = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE)
            Dim filtervaluename = GlobalVariables.FiltersValues.filtervalues_hash(parentFilterValueId)(NAME_VARIABLE)
            DGV.CellsArea.SetCellValue(row, column, filtervaluename)

            ' Recursively update parent filters
            UpdateParentFiltersValues(row, _
                                      entityId, _
                                      parentFilterNode, _
                                      parentFilterValueId)
        End If

    End Sub

    Private Sub UpdateChildrenFiltersComboBoxes(ByRef row As HierarchyItem, _
                                                ByRef filterNode As TreeNode,
                                                ByRef parentFilterValueIds() As Int32)

        Dim column As HierarchyItem = columnsVariableItemDictionary(filterNode.Name)
        Dim comboBox As New ComboBoxEditor

        Dim filterValuesIds As Int32() = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds(parentFilterValueIds)
        For Each Id As Int32 In filterValuesIds
            comboBox.Items.Add(GlobalVariables.FiltersValues.filtervalues_hash(Id)(NAME_VARIABLE))
        Next

        ' Set Cell value to nothing
        DGV.CellsArea.SetCellValue(row, column, Nothing)

        ' Add ComboBoxEditor to Cell
        DGV.CellsArea.SetCellEditor(row, column, comboBox)

        ' Recursivly update children comboboxes
        For Each childFilterNode As TreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(row, _
                                            childFilterNode, _
                                            filterValuesIds)
        Next

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
            If row.Items.Count > 0 Then SetValueToChildrenItems(row, column, value) Else SetValueToSibbling(row, column, value)
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

        Dim cell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS("Entities", _
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
        ExcelFormatting.FormatEntitiesReport(GlobalVariables.APPS.ActiveSheet.range(cell, cell.Offset(UBound(DGVArray, 1), UBound(DGVArray, 2))))

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
