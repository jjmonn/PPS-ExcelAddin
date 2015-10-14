' AxisControl.vb
'
' to be reviewed for proper disctinction between DGV and control !!
'
'
'
' Known bugs:
'
'
'
' Last modified: 14/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.ComponentModel
Imports Microsoft.Office.Interop
Imports VIBlend.WinForms.Controls


Friend Class AxisView


#Region "Instance Variables"

    ' Objects
    Private Controller As AxisController
    Private axisTV As vTreeView
    Private axisFilterTV As vTreeView
    Private axisFilterValuesTV As vTreeView
    Friend DGV As New vDataGridView
    Private CP As CircularProgressUI

    ' Variables
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


#Region "Initialization"

    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_axisHT As Dictionary(Of Int32, SortableHashtable), _
                   ByRef p_axisTV As vTreeView, _
                   ByRef p_axisFilterValuesTV As vTreeView, _
                   ByRef p_axisFiltersTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = p_controller
        axisTV = p_axisTV
        axisFilterTV = p_axisFiltersTV
        axisFilterValuesTV = p_axisFilterValuesTV

        initFilters()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize(axisTV)
        fillDGV(p_axisHT)

        Me.TableLayoutPanel1.Controls.Add(DGV, 0, 1)
        DGV.Dock = DockStyle.Fill
        DGV.ContextMenuStrip = RCM_TGV
        DGV.RowsHierarchy.AllowDragDrop = True
        DGV.AllowDragDropIndication = True

        AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler DGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        AddHandler DGV.CellValueChanged, AddressOf dataGridView_CellValueChanged
        AddHandler DGV.KeyDown, AddressOf DGV_KeyDown
        DesactivateUnallowed()
    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            DeleteAxisToolStripMenuItem.Enabled = False
            DeleteAxisToolStripMenuItem2.Enabled = False
            CreateAxisToolStripMenuItem.Enabled = False
            CreateNewToolStripMenuItem.Enabled = False
            copy_down_bt.Enabled = False
        End If
    End Sub


#End Region


#Region "Interface"

    Delegate Sub UpdateAxis_Delegate(ByRef ht As Hashtable)
    Friend Sub UpdateAxis(ByRef ht As Hashtable)

        If ht Is Nothing Then Exit Sub
        If InvokeRequired Then
            Dim MyDelegate As New UpdateAxis_Delegate(AddressOf UpdateAxis)
            Me.Invoke(MyDelegate, New Object() {ht})
        Else
            isFillingDGV = True
            FillRow(ht(ID_VARIABLE), ht(NAME_VARIABLE), ht)
            isFillingDGV = False
        End If

    End Sub

    Delegate Sub DeleteAxis_Delegate(ByRef id As Int32)
    Friend Sub DeleteAxis(ByRef id As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New DeleteAxis_Delegate(AddressOf DeleteAxis)
            Me.Invoke(MyDelegate, New Object() {id})
        Else
            rows_id_item_dic(id).Delete()
            rows_id_item_dic.Remove(id)
            DGV.Refresh()
        End If

    End Sub

#End Region


#Region "Calls Back"

    Private Sub CreateAxisOrder()

        Dim axisName As String = InputBox("Enter the new Name", "Creation")
        If axisName <> "" Then
            Controller.CreateAxis(axisName)
        End If

    End Sub

    Private Sub DeleteAxisOrder()

        If Not currentRowItem Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the axis " + Chr(13) + Chr(13) + _
                                                    DGV.CellsArea.GetCellValue(currentRowItem, DGV.ColumnsHierarchy.Items(0)) + Chr(13) + Chr(13) + _
                                                     "This axis and all sub axis will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                     "Axis deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim axisValueId As Int32 = currentRowItem.ItemValue
                Controller.DeleteAxis(axisValueId)
            End If
        Else
            MsgBox("An Axis must be selected in order to be deleted")
        End If
        currentRowItem = Nothing

    End Sub

#Region "DGV Right Click Menu"

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        CopyValueDown()
        DGV.Refresh()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        DropInExcel()

    End Sub

    Private Sub CreateAxisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateAxisToolStripMenuItem.Click

        CreateAxisOrder()

    End Sub

    Private Sub DeleteAxisToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteAxisToolStripMenuItem2.Click

        DeleteAxisOrder()

    End Sub

#End Region

#Region "Main menu"

    Private Sub CreateNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateNewToolStripMenuItem.Click

        CreateAxisOrder()

    End Sub

    Private Sub DeleteAxis_cmd_Click(sender As Object, e As EventArgs) Handles DeleteAxisToolStripMenuItem.Click

        DeleteAxisOrder()

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        DropInExcel()

    End Sub

#End Region

#End Region


#Region "DGV"

    Private Sub InitializeDGVDisplay()

        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AllowResize = True
        ' DGV.RowsHierarchy.AllowDragDrop = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.AllowDragDropIndication = True
        DGV.AllowCopyPaste = True
        DGV.FilterDisplayMode = FilterDisplayMode.Custom
        DGV.VIBlendTheme = DGV_VI_BLEND_STYLE
        DGV.BackColor = SystemColors.Control
        'DGV.ImageList = AxisIL

    End Sub

    Private Sub fillDGV(ByRef axisHT As Dictionary(Of Int32, SortableHashtable))

        isFillingDGV = True
        For Each axisValueId In axisHT.Keys
            FillRow(axisValueId, axisHT(axisValueId)(NAME_VARIABLE), axisHT(axisValueId))
        Next
        isFillingDGV = False
        updateDGVFormat()

    End Sub


#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        DGV.ColumnsHierarchy.Clear()
        columnsVariableItemDictionary.Clear()

        For Each filterNode As vTreeNode In axisFilterTV.Nodes
            CreateSubFilters(filterNode)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As vTreeNode)

        Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(node.Text)

        columnsVariableItemDictionary.Add(node.Value, col)
        col.ItemValue = node.Value
        col.AllowFiltering = True

        For Each childNode As vTreeNode In node.Nodes
            CreateSubFilters(childNode)
        Next

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

    Private Sub DGVRowsInitialize(ByRef axis_tv As vTreeView)

        DGV.RowsHierarchy.Clear()
        rows_id_item_dic.Clear()
        isFillingDGV = True
        For Each node In axis_tv.Nodes
            Dim row As HierarchyItem = CreateRow(node.Value, node.Text)
        Next
        isFillingDGV = False

    End Sub

    Private Function CreateRow(ByRef p_axisId As Int32, ByRef p_axisName As String) As HierarchyItem

        Dim row As HierarchyItem
        row = DGV.RowsHierarchy.Items.Add(p_axisName)
        row.ItemValue = p_axisId
        FormatRow(row, p_axisId)
        Return row

    End Function

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef row_id As Int32)

        If rows_id_item_dic.ContainsKey(row_id) Then
            rows_id_item_dic(row_id) = row
        Else
            rows_id_item_dic.Add(row_id, row)
        End If
        row.TextAlignment = ContentAlignment.MiddleLeft

    End Sub

#End Region


#Region "DGV Filling"

    Friend Sub FillRow(ByVal axisValueId As Int32, ByRef p_axisName As String, _
                       ByVal axis_ht As Hashtable)

        Dim rowItem As HierarchyItem
        If rows_id_item_dic.ContainsKey(axisValueId) Then
            rowItem = rows_id_item_dic(axisValueId)
        Else
            rowItem = CreateRow(axisValueId, p_axisName)
        End If
        For Each filterNode As vTreeNode In axisFilterTV.Nodes
            FillSubFilters(filterNode, axisValueId, rowItem)
        Next

    End Sub

    Private Sub FillSubFilters(ByRef filterNode As vTreeNode, _
                               ByRef axisValueId As Int32, _
                               ByRef rowItem As HierarchyItem)

        Dim combobox As New ComboBoxEditor
        combobox.DropDownList = True
        Dim columnItem As HierarchyItem = columnsVariableItemDictionary(filterNode.Value)
        Dim filterValueId = Controller.GetFilterValueId(CInt(filterNode.Value), axisValueId)
        If (filterValueId = 0) Then
            System.Diagnostics.Debug.WriteLine("AxisControl.FillSubFilters: Invalid filter value id")
            Exit Sub
        End If
        Dim filter_value_name = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(NAME_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, columnItem, filter_value_name)

        ' Filters Choices Setup
        If filterNode.Parent Is Nothing Then
            ' Root Filter
            Dim valuesNames = GlobalVariables.FiltersValues.GetFiltervaluesList(filterNode.Value, NAME_VARIABLE)
            For Each valueName As String In valuesNames
                combobox.Items.Add(valueName)
            Next
        Else
            ' Child Filter
            Dim parentFilterFilterValueId As Int32 = Controller.GetFilterValueId(CInt(filterNode.Parent.Value), axisValueId)     ' Child Filter Id
            Dim filterValuesIds = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds({parentFilterFilterValueId})
            For Each Id As Int32 In filterValuesIds
                combobox.Items.Add(GlobalVariables.FiltersValues.filtervalues_hash(Id)(NAME_VARIABLE))
            Next
        End If

        ' Add ComboBoxEditor to Cell
        If GlobalVariables.Users.CurrentUserIsAdmin() Then DGV.CellsArea.SetCellEditor(rowItem, columnItem, combobox)

        ' Recursive if Filters Children exist
        For Each childFilterNode As vTreeNode In filterNode.Nodes
            FillSubFilters(childFilterNode, axisValueId, rowItem)
        Next

    End Sub

    Private Sub updateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

#End Region


#Region "DGV Updates"

    Friend Sub LoadInstanceVariables_Safe()
        Try
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
    End Sub

    Friend Delegate Sub LoadInstanceVariables_Delegate()
    Friend Sub LoadInstanceVariables()
        If InvokeRequired Then
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            Controller.LoadInstanceVariables()
        End If
    End Sub

    ' Update Parents and Children Filters Cells or comboboxes after a filter cell has been edited
    Friend Sub UpdateAxisFiltersAfterEdition(ByRef axisId As Int32, _
                                                 ByRef filterId As Int32, _
                                                 ByRef filterValueId As Int32)

        isFillingDGV = True
        Dim filterNode As TreeNode = TreeViewsUtilities.FindNode(axisFilterTV.Nodes, filterId, True)
        If (filterNode Is Nothing) Then Exit Sub

        ' Update parent filters recursively
        UpdateParentFiltersValues(rows_id_item_dic(axisId), _
                                 axisId, _
                                 filterNode, _
                                 filterValueId)

        ' Update children filters comboboxes recursively
        For Each childFilterNode As TreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(rows_id_item_dic(axisId), _
                                            childFilterNode, _
                                            {filterValueId})
        Next
        isFillingDGV = False

    End Sub

    Private Sub UpdateParentFiltersValues(ByRef row As HierarchyItem, _
                                          ByRef axisId As Int32, _
                                          ByRef filterNode As TreeNode,
                                          ByRef filterValueId As Int32)
        If filterNode Is Nothing Then Exit Sub

        If Not filterNode.Parent Is Nothing Then
            Dim parentFilterNode As TreeNode = filterNode.Parent
            Dim column As HierarchyItem = columnsVariableItemDictionary(parentFilterNode.Name)
            Dim parentFilterValueId As Int32 = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE)
            Dim filtervaluename = GlobalVariables.FiltersValues.filtervalues_hash(parentFilterValueId)(NAME_VARIABLE)
            DGV.CellsArea.SetCellValue(row, column, filtervaluename)

            ' Recursively update parent filters
            UpdateParentFiltersValues(row, _
                                      axisId, _
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

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteAxis_cmd_Click(sender, e)
        End Select

    End Sub

    Private Sub dataGridView_CellValueChanged(sender As Object, args As CellEventArgs)

        If isFillingDGV = False Then
            If (Not args.Cell.Value Is Nothing AndAlso args.Cell.Value <> "") Then
                Dim axisId As Int32 = args.Cell.RowItem.ItemValue

                Select Case args.Cell.ColumnItem.ItemValue

                    Case NAME_VARIABLE
                        Dim newAxisName As String = args.Cell.Value
                        Controller.UpdateAxis(axisId, _
                                                args.Cell.ColumnItem.ItemValue, _
                                                newAxisName)
                    Case Else
                        Dim filterValueName As String = args.Cell.Value
                        Dim filterValueId As Int32 = GlobalVariables.FiltersValues.GetFilterValueId(filterValueName)
                        If filterValueId = 0 Then
                            MsgBox("The Filter Value Name " & filterValueName & " could not be found.")
                            Exit Sub
                        End If
                        Dim filterId As Int32 = args.Cell.ColumnItem.ItemValue
                        UpdateAxisFiltersAfterEdition(axisId, filterId, filterValueId)
                        Controller.UpdateFilterValue(axisId, filterId, filterValueId)
                End Select

            End If
        End If

    End Sub

#End Region

#End Region


#Region "Utilities"

    Friend Function getCurrentRowItem() As HierarchyItem

        Return currentRowItem

    End Function

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

        Dim cell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS("Axis", _
                                                                                {"Axis Hierarchy"}, _
                                                                                {""})
        Dim nbRows As Int32
        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            DGVRowsCount(item, nbRows)
        Next

        Dim nbcols As Int32 = DGV.ColumnsHierarchy.Items.Count
        ReDim DGVArray(nbRows + 1, nbcols + 2)
        Dim i, j As Int32

        DGVArray(i, 0) = "Axis's Name"
        DGVArray(i, 1) = "Axis's Affiliate's Name"
        For j = 0 To nbcols - 1
            DGVArray(i, j + 2) = DGV.ColumnsHierarchy.Items(j).Caption
        Next

        For Each row In DGV.RowsHierarchy.Items
            fillInDGVArrayRow(row, 1)
        Next

        WorksheetWrittingFunctions.WriteArray(DGVArray, cell)
        'ExcelFormatting.FormatAxisReport(GlobalVariables.APPS.ActiveSheet.range(cell, cell.Offset(UBound(DGVArray, 1), UBound(DGVArray, 2))))

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

#End Region



End Class
