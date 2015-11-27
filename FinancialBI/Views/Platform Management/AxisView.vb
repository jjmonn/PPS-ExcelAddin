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
' Last modified: 13/11/2015
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
Imports CRUD

Friend Class AxisView


#Region "Instance Variables"

    ' Objects
    Private m_controller As AxisController
    Private m_axisTreeview As vTreeView
    Private m_axisFilterTreeview As vTreeView
    Private m_axisFilterValuesTreeview As vTreeView
    Friend m_axisDataGridView As New vDataGridView
    Private m_CircularProgress As CircularProgressUI

    ' Variables
    Friend m_columnsVariableItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend m_rowsIdsItemDic As New Dictionary(Of Int32, HierarchyItem)
    Private m_dataGridViewArray(,) As String
    Private m_filterGroup As New FilterGroup(Of String)()
    Friend m_currentRowItem As HierarchyItem
    Friend m_isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Friend Const CURRENCY_COLUMN_NAME As String = "Currency"
    Friend Const NAME_COLUMN_NAME As String = "Name"
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_axisTV As vTreeView, _
                   ByRef p_axisFilterValuesTV As vTreeView, _
                   ByRef p_axisFiltersTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        m_axisTreeview = p_axisTV
        m_axisFilterTreeview = p_axisFiltersTV
        m_axisFilterValuesTreeview = p_axisFilterValuesTV

        initFilters()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize(m_axisTreeview)
        fillDGV(m_controller.GetAxisDictionary())

        Me.TableLayoutPanel1.Controls.Add(m_axisDataGridView, 0, 1)
        m_axisDataGridView.Dock = DockStyle.Fill
        m_axisDataGridView.RowsHierarchy.AllowDragDrop = True
        m_axisDataGridView.AllowDragDropIndication = True

        AddHandler m_axisDataGridView.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler m_axisDataGridView.MouseDown, AddressOf axisDataGridViewRightClick
        AddHandler m_axisDataGridView.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        AddHandler m_axisDataGridView.CellValueChanged, AddressOf dataGridView_CellValueChanged
        AddHandler m_axisDataGridView.KeyDown, AddressOf DGV_KeyDown
        DesactivateUnallowed()
        MultilanguageSetup()

    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            DeleteAxisToolStripMenuItem.Enabled = False
            DeleteAxisToolStripMenuItem2.Enabled = False
            CreateAxisToolStripMenuItem.Enabled = False
            CreateNewToolStripMenuItem.Enabled = False
            copy_down_bt.Enabled = False
            CopyDownValuesToolStripMenuItem.Enabled = False
            RenameToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub MultilanguageSetup()

        Me.copy_down_bt.Text = Local.GetValue("general.copy_down")
        Me.drop_to_excel_bt.Text = Local.GetValue("general.drop_on_excel")
        Me.CreateAxisToolStripMenuItem.Text = Local.GetValue("general.create")
        Me.DeleteAxisToolStripMenuItem2.Text = Local.GetValue("general.delete")
        Me.EditToolStripMenuItem.Text = Local.GetValue("general.menu")
        Me.CreateNewToolStripMenuItem.Text = Local.GetValue("general.create")
        Me.DeleteAxisToolStripMenuItem.Text = Local.GetValue("general.delete")
        Me.RenameToolStripMenuItem.Text = Local.GetValue("general.rename")
        Me.CopyDownValuesToolStripMenuItem.Text = Local.GetValue("general.copy_down")
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("general.drop_on_excel")
        Me.RenameToolStripMenuItem1.Text = Local.GetValue("general.rename")

    End Sub


#End Region


#Region "Interface"

    Delegate Sub UpdateAxis_Delegate(ByRef ht As AxisElem)
    Friend Sub UpdateAxis(ByRef ht As AxisElem)

        If ht Is Nothing Then Exit Sub
        If Me.m_axisDataGridView.InvokeRequired Then
            Dim MyDelegate As New UpdateAxis_Delegate(AddressOf UpdateAxis)
            Me.m_axisDataGridView.Invoke(MyDelegate, New Object() {ht})
        Else
            m_isFillingDGV = True
            FillRow(ht.Id, ht.Name, ht)
            updateDGVFormat()
            m_axisDataGridView.Refresh()
            m_isFillingDGV = False
        End If

    End Sub

    Delegate Sub DeleteAxis_Delegate(ByRef id As Int32)
    Friend Sub DeleteAxis(ByRef id As Int32)

        If Me.m_axisDataGridView.InvokeRequired Then
            Dim MyDelegate As New DeleteAxis_Delegate(AddressOf DeleteAxis)
            Me.m_axisDataGridView.Invoke(MyDelegate, New Object() {id})
        ElseIf (m_rowsIdsItemDic.ContainsKey(id)) Then
            Dim l_row As HierarchyItem = m_rowsIdsItemDic(id)
            If l_row IsNot Nothing Then l_row.Delete()
            m_rowsIdsItemDic.Remove(id)
            m_axisDataGridView.Refresh()
        End If

    End Sub

#End Region


#Region "Calls Back"

    Private Sub CreateAxisOrder()

        Dim axisName As String = InputBox(Local.GetValue("axis.msg_enter_name"), Local.GetValue("axis.msg_axis_creation"))
        If axisName <> "" Then
            m_controller.CreateAxis(axisName)
        End If

    End Sub

    Private Sub DeleteAxisOrder()

        If Not m_currentRowItem Is Nothing AndAlso m_axisDataGridView.ColumnsHierarchy.Items.Count > 0 Then
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("axis.msg_axis_delete1") + Chr(13) + Chr(13) + _
                                                    m_axisDataGridView.CellsArea.GetCellValue(m_currentRowItem, m_axisDataGridView.ColumnsHierarchy.Items(0)) + Chr(13) + Chr(13) + _
                                                     Local.GetValue("axis.msg_axis_delete2") + Chr(13) + Chr(13), _
                                                     Local.GetValue("axis.msg_deletion_confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim axisValueId As Int32 = m_currentRowItem.ItemValue
                m_controller.DeleteAxis(axisValueId)
            End If
        Else
            MsgBox(Local.GetValue("axis.msg_select_axis"))
        End If
        m_currentRowItem = Nothing

    End Sub

#Region "DGV Right Click Menu"

    Private Sub AxisDataGridViewRightClick(sender As Object, e As MouseEventArgs)

        If (e.Button <> MouseButtons.Right) Then Exit Sub
        Dim target As HierarchyItem = m_axisDataGridView.RowsHierarchy.HitTest(e.Location)
        If target IsNot Nothing Then
            m_currentRowItem = m_rowsIdsItemDic(target.ItemValue)
        Else
            Dim target2 As GridCell = m_axisDataGridView.CellsArea.HitTest(e.Location)
            If target2 Is Nothing Then Exit Sub
            m_currentRowItem = target2.RowItem
        End If
        m_dataGridViewRightClickMenu.Visible = True
        m_dataGridViewRightClickMenu.Bounds = New Rectangle(MousePosition, New Size(m_dataGridViewRightClickMenu.Width, m_dataGridViewRightClickMenu.Height))


    End Sub

    Private Sub RenameEntityButton_Click(sender As Object, e As EventArgs) Handles RenameToolStripMenuItem.Click, RenameToolStripMenuItem1.Click

        If m_currentRowItem Is Nothing Then
            MsgBox(Local.GetValue("axis.msg_select_axis"))
            Exit Sub
        End If
        Dim newAxisName As String = InputBox(Local.GetValue("axis.msg_enter_name"), , m_currentRowItem.Caption)
        If newAxisName <> "" Then
            m_controller.UpdateAxisName(m_currentRowItem.ItemValue, newAxisName)
        End If

    End Sub

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click, CopyDownValuesToolStripMenuItem.Click

        CopyValueDown()
        m_axisDataGridView.Refresh()

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

        m_axisDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        m_axisDataGridView.ColumnsHierarchy.AutoStretchColumns = True
        m_axisDataGridView.ColumnsHierarchy.AllowResize = True
        ' DGV.RowsHierarchy.AllowDragDrop = True
        m_axisDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = True
        m_axisDataGridView.AllowDragDropIndication = True
        m_axisDataGridView.AllowCopyPaste = True
        m_axisDataGridView.FilterDisplayMode = FilterDisplayMode.Custom
        m_axisDataGridView.VIBlendTheme = DGV_VI_BLEND_STYLE
        m_axisDataGridView.BackColor = SystemColors.Control
        'DGV.ImageList = AxisIL

    End Sub

    Private Sub fillDGV(ByRef axisHT As MultiIndexDictionary(Of UInt32, String, AxisElem))

        m_isFillingDGV = True
        For Each axisValue In axisHT.SortedValues
            FillRow(axisValue.Id, axisValue.Name, axisValue)
        Next
        m_isFillingDGV = False
        updateDGVFormat()

    End Sub


#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        m_axisDataGridView.ColumnsHierarchy.Clear()
        m_columnsVariableItemDictionary.Clear()

        For Each filterNode As vTreeNode In m_axisFilterTreeview.Nodes
            CreateSubFilters(filterNode)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As vTreeNode)

        Dim col As HierarchyItem = m_axisDataGridView.ColumnsHierarchy.Items.Add(node.Text)

        m_columnsVariableItemDictionary.Add(node.Value, col)
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

        m_filterGroup.AddFilter(FilterOperator.AND, stringFilter1)
        m_filterGroup.AddFilter(FilterOperator.AND, stringFilter2)

    End Sub

#End Region


#Region "Rows Initialization"

    Private Sub DGVRowsInitialize(ByRef axis_tv As vTreeView)

        m_axisDataGridView.RowsHierarchy.Clear()
        m_rowsIdsItemDic.Clear()
        m_isFillingDGV = True
        For Each node In axis_tv.Nodes
            Dim row As HierarchyItem = CreateRow(node.Value, node.Text)
        Next
        m_isFillingDGV = False

    End Sub

    Private Function CreateRow(ByRef p_axisId As Int32, ByRef p_axisName As String) As HierarchyItem

        Dim row As HierarchyItem
        row = m_axisDataGridView.RowsHierarchy.Items.Add(p_axisName)
        row.ItemValue = p_axisId
        FormatRow(row, p_axisId)
        Return row

    End Function

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef row_id As Int32)

        If m_rowsIdsItemDic.ContainsKey(row_id) Then
            m_rowsIdsItemDic(row_id) = row
        Else
            m_rowsIdsItemDic.Add(row_id, row)
        End If
        row.TextAlignment = ContentAlignment.MiddleLeft

    End Sub

#End Region


#Region "DGV Filling"

    Friend Sub FillRow(ByVal axisValueId As Int32, ByRef p_axisName As String, _
                       ByVal axis_ht As AxisElem)

        Dim rowItem As HierarchyItem
        If m_rowsIdsItemDic.ContainsKey(axisValueId) Then
            rowItem = m_rowsIdsItemDic(axisValueId)
            rowItem.Caption = p_axisName
        Else
            rowItem = CreateRow(axisValueId, p_axisName)
        End If
        For Each filterNode As vTreeNode In m_axisFilterTreeview.Nodes
            FillSubFilters(filterNode, axisValueId, rowItem)
        Next

    End Sub

    Private Sub FillSubFilters(ByRef filterNode As vTreeNode, _
                               ByRef axisValueId As Int32, _
                               ByRef rowItem As HierarchyItem)

        Dim combobox As New ComboBoxEditor
        combobox.DropDownList = True
        Dim columnItem As HierarchyItem = m_columnsVariableItemDictionary(filterNode.Value)
        Dim filterValueId As UInt32 = m_controller.GetFilterValueId(CInt(filterNode.Value), axisValueId)
        If (filterValueId = 0) Then
            System.Diagnostics.Debug.WriteLine("AxisControl.FillSubFilters: Invalid filter value id")
            Exit Sub
        End If
        Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(filterValueId)

        If filterValue Is Nothing Then Exit Sub
        m_axisDataGridView.CellsArea.SetCellValue(rowItem, columnItem, filterValue.Name)

        ' Filters Choices Setup
        If filterNode.Parent Is Nothing Then
            ' Root Filter
            Dim valuesDic = GlobalVariables.FiltersValues.GetDictionary(filterNode.Value)
            For Each value As FilterValue In valuesDic.Values
                combobox.Items.Add(value.Name)
            Next
        Else
            ' Child Filter
            Dim parentFilterFilterValueId As Int32 = m_controller.GetFilterValueId(CInt(filterNode.Parent.Value), axisValueId)     ' Child Filter Id
            Dim filterValuesIds = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds({parentFilterFilterValueId})
            For Each Id As UInt32 In filterValuesIds
                combobox.Items.Add(GlobalVariables.FiltersValues.GetValueName(Id))
            Next
        End If

        ' Add ComboBoxEditor to Cell
        If GlobalVariables.Users.CurrentUserIsAdmin() Then m_axisDataGridView.CellsArea.SetCellEditor(rowItem, columnItem, combobox)

        ' Recursive if Filters Children exist
        For Each childFilterNode As vTreeNode In filterNode.Nodes
            FillSubFilters(childFilterNode, axisValueId, rowItem)
        Next

    End Sub

    Private Sub updateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(m_axisDataGridView, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_axisDataGridView)
        m_axisDataGridView.Refresh()

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
            m_controller.LoadInstanceVariables()
        End If
    End Sub

    ' Update Parents and Children Filters Cells or comboboxes after a filter cell has been edited
    Friend Sub UpdateAxisFiltersAfterEdition(ByRef axisId As Int32, _
                                            ByRef filterId As Int32, _
                                            ByRef filterValueId As Int32)

        m_isFillingDGV = True
        Dim filterNode As vTreeNode = VTreeViewUtil.FindNode(m_axisFilterTreeview, filterId)
        If (filterNode Is Nothing) Then Exit Sub

        ' Update parent filters recursively
        UpdateParentFiltersValues(m_rowsIdsItemDic(axisId), _
                                 axisId, _
                                 filterNode, _
                                 filterValueId)

        ' Update children filters comboboxes recursively
        For Each childFilterNode As vTreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(m_rowsIdsItemDic(axisId), _
                                            childFilterNode, _
                                            {filterValueId})
        Next
        m_isFillingDGV = False

    End Sub

    Private Sub UpdateParentFiltersValues(ByRef row As HierarchyItem, _
                                          ByRef axisId As Int32, _
                                          ByRef filterNode As vTreeNode,
                                          ByRef filterValueId As UInt32)
        If filterNode Is Nothing Then Exit Sub
        Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(filterValueId)
        If filterValue Is Nothing Then Exit Sub

        If Not filterNode.Parent Is Nothing Then
            Dim parentFilterNode As vTreeNode = filterNode.Parent
            Dim column As HierarchyItem = m_columnsVariableItemDictionary(parentFilterNode.Value)
            Dim parentFilterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(filterValue.ParentId)
            If parentFilterValue Is Nothing Then Exit Sub

            m_axisDataGridView.CellsArea.SetCellValue(row, column, parentFilterValue.Name)

            ' Recursively update parent filters
            UpdateParentFiltersValues(row, _
                                      axisId, _
                                      parentFilterNode, _
                                      parentFilterValue.Id)
        End If

    End Sub

    Private Sub UpdateChildrenFiltersComboBoxes(ByRef row As HierarchyItem, _
                                                ByRef filterNode As vTreeNode,
                                                ByRef parentFilterValueIds() As Int32)

        Dim column As HierarchyItem = m_columnsVariableItemDictionary(filterNode.Value)
        Dim comboBox As New ComboBoxEditor

        Dim filterValuesIds As Int32() = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds(parentFilterValueIds)
        For Each Id As UInt32 In filterValuesIds
            comboBox.Items.Add(GlobalVariables.FiltersValues.GetValueName(Id))
        Next

        ' Set Cell value to nothing
        m_axisDataGridView.CellsArea.SetCellValue(row, column, Nothing)

        ' Add ComboBoxEditor to Cell
        m_axisDataGridView.CellsArea.SetCellEditor(row, column, comboBox)

        ' Recursivly update children comboboxes
        For Each childFilterNode As vTreeNode In filterNode.Nodes
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
                m_axisDataGridView.CloseEditor(True)
            Else
                m_axisDataGridView.CloseEditor(False)
            End If
        End If

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        m_currentRowItem = args.HierarchyItem

    End Sub

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        m_currentRowItem = args.Cell.RowItem

    End Sub

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteAxis_cmd_Click(sender, e)
        End Select

    End Sub

    Private Sub dataGridView_CellValueChanged(sender As Object, args As CellEventArgs)

        If m_isFillingDGV = False Then
            If (Not args.Cell.Value Is Nothing AndAlso args.Cell.Value <> "") Then
                Dim axisId As Int32 = args.Cell.RowItem.ItemValue

                Select Case args.Cell.ColumnItem.ItemValue

                    Case NAME_VARIABLE
                        Dim newAxisName As String = args.Cell.Value
                        'Controller.UpdateAxis(axisId, args.Cell.ColumnItem.ItemValue, newAxisName) ' Nath_TODO
                    Case Else
                        Dim filterValueName As String = args.Cell.Value
                        Dim filterValueId As Int32 = GlobalVariables.FiltersValues.GetValueId(filterValueName)
                        If filterValueId = 0 Then
                            MsgBox(Local.GetValue("axis.msg_filter_value_not_found"))
                            Exit Sub
                        End If
                        Dim filterId As Int32 = args.Cell.ColumnItem.ItemValue
                        UpdateAxisFiltersAfterEdition(axisId, filterId, filterValueId)
                        m_controller.UpdateFilterValue(axisId, filterId, filterValueId)
                End Select

            End If
        End If

    End Sub

#End Region

#End Region


#Region "Utilities"

    Friend Function getCurrentRowItem() As HierarchyItem

        Return m_currentRowItem

    End Function

#Region "Copy Down"

    Friend Sub CopyValueDown()

        If Not m_axisDataGridView.CellsArea.SelectedCells(0) Is Nothing Then
            Dim row As HierarchyItem = m_axisDataGridView.CellsArea.SelectedCells(0).RowItem
            Dim column As HierarchyItem = m_axisDataGridView.CellsArea.SelectedCells(0).ColumnItem
            Dim value As String = m_axisDataGridView.CellsArea.SelectedCells(0).Value
            If row.Items.Count > 0 Then SetValueToChildrenItems(row, column, value) Else SetValueToSibbling(row, column, value)
            m_axisDataGridView.Refresh()
            m_axisDataGridView.Select()
        End If

    End Sub

    Private Sub SetValueToChildrenItems(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        For Each childItem As HierarchyItem In rowItem_.Items
            m_axisDataGridView.CellsArea.SetCellValue(childItem, columnItem_, value)
            If childItem.Items.Count > 0 Then SetValueToChildrenItems(childItem, columnItem_, value)
        Next

    End Sub

    Private Sub SetValueToSibbling(ByRef p_rowItem As HierarchyItem, ByRef p_columnItem As HierarchyItem, ByRef p_value As String)

        Dim l_itemBelow As HierarchyItem = p_rowItem
        While Not l_itemBelow.ItemBelow Is Nothing
            l_itemBelow = l_itemBelow.ItemBelow
            m_axisDataGridView.CellsArea.SetCellValue(l_itemBelow, p_columnItem, p_value)
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
        For Each item As HierarchyItem In m_axisDataGridView.RowsHierarchy.Items
            DGVRowsCount(item, nbRows)
        Next

        Dim nbcols As Int32 = m_axisDataGridView.ColumnsHierarchy.Items.Count
        ReDim m_dataGridViewArray(nbRows + 1, nbcols + 2)
        Dim i, j As Int32

        m_dataGridViewArray(i, 0) = "Axis's Name"
        m_dataGridViewArray(i, 1) = "Axis's Affiliate's Name"
        For j = 0 To nbcols - 1
            m_dataGridViewArray(i, j + 2) = m_axisDataGridView.ColumnsHierarchy.Items(j).Caption
        Next

        For Each row In m_axisDataGridView.RowsHierarchy.Items
            fillInDGVArrayRow(row, 1)
        Next

        WorksheetWrittingFunctions.WriteArray(m_dataGridViewArray, cell)
        'ExcelFormatting.FormatAxisReport(GlobalVariables.APPS.ActiveSheet.range(cell, cell.Offset(UBound(DGVArray, 1), UBound(DGVArray, 2))))

    End Sub

    ' Recursively fill in row array
    Private Sub fillInDGVArrayRow(ByRef inputRow As HierarchyItem, ByRef rowIndex As Int32)

        Dim j As Int32

        m_dataGridViewArray(rowIndex, 0) = m_axisDataGridView.CellsArea.GetCellValue(inputRow, m_axisDataGridView.ColumnsHierarchy.Items(0))
        If Not inputRow.ParentItem Is Nothing Then
            m_dataGridViewArray(rowIndex, 1) = m_axisDataGridView.CellsArea.GetCellValue(inputRow.ParentItem, m_axisDataGridView.ColumnsHierarchy.Items(0))
        Else
            m_dataGridViewArray(rowIndex, 1) = ""
        End If

        For Each column As HierarchyItem In m_axisDataGridView.ColumnsHierarchy.Items
            m_dataGridViewArray(rowIndex, j + 2) = m_axisDataGridView.CellsArea.GetCellValue(inputRow, column)
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
