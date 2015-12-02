Imports VIBlend.WinForms.Controls
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.Utilities
Imports System.Collections.Generic

' AllocationKeysView.vb
'
' User interface for managing allocation keys
'
'
' Created on: 30/11/2015
' Last modified: 01/12/2015


Public Class AllocationKeysView

#Region "Instance variables"

    Private m_controller As AllocationKeysController
    Private m_allocationTextBoxEditor As New TextBoxEditor()
    Private m_isFillingCells As Boolean

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As AllocationKeysController, _
                   ByRef p_accountId As Int32, _
                   ByRef p_entitiesTreeview As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        Dim l_account As CRUD.Account = GlobalVariables.Accounts.GetValue(p_accountId)
        If l_account Is Nothing Then Exit Sub
        m_accountTextBox.Text = l_account.Name

        InitializeDataGridView(p_entitiesTreeview)
        InitializeMultilanguage()

        AddHandler m_allocationsKeysDGV.CellValueChanging, AddressOf DataGridView_CellValueChanging
        AddHandler m_allocationsKeysDGV.CellEditorActivate, AddressOf DataGridView_EditorActivation

    End Sub

    Private Sub InitializeMultilanguage()

        m_accountLabel.Text = Local.GetValue("general.account")

    End Sub

    Private Sub InitializeDataGridView(ByRef p_entitiesTreeview As vTreeView)

        DataGridViewsUtil.DGVRowsInitialize(m_allocationsKeysDGV, p_entitiesTreeview)
        m_allocationsKeysDGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        Dim l_allocationsColumn As HierarchyItem = m_allocationsKeysDGV.ColumnsHierarchy.Items.Add(Local.GetValue("allocationKeys.repartition_column_name"))
        m_allocationsKeysDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        m_allocationTextBoxEditor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL
        m_allocationsKeysDGV.RowsHierarchy.ExpandAllItems()
        m_allocationsKeysDGV.Refresh()

        Dim l_nonEditionCellStyle As GridCellStyle = GridTheme.GetDefaultTheme(m_allocationsKeysDGV.VIBlendTheme).GridCellStyle
        l_nonEditionCellStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.DarkGray)
        l_nonEditionCellStyle.Font = New System.Drawing.Font(m_allocationsKeysDGV.Font.FontFamily, My.Settings.dgvFontSize, Drawing.FontStyle.Bold)
        l_nonEditionCellStyle.TextColor = Drawing.Color.White

        Dim l_editableCellStyle As GridCellStyle = VIBlend.Utilities.GridTheme.GetDefaultTheme(m_allocationsKeysDGV.VIBlendTheme).GridCellStyle
        l_editableCellStyle.TextColor = Drawing.Color.DarkBlue

        For Each l_row As HierarchyItem In m_allocationsKeysDGV.RowsHierarchy.Items
            SpecificyAllocationKeysEditionEnabling(l_row, l_nonEditionCellStyle, l_editableCellStyle)
        Next
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_allocationsKeysDGV)
        '   m_allocationsKeysDGV.ColumnsHierarchy.AutoStretchColumns = True
        m_allocationsKeysDGV.ColumnsHierarchy.Items(0).Width = Me.Width - m_allocationsKeysDGV.RowsHierarchy.Items(0).Width - 20

    End Sub

    Private Sub SpecificyAllocationKeysEditionEnabling(ByRef p_row As HierarchyItem, _
                                                       ByRef p_nonEditionCellStyle As GridCellStyle, _
                                                       ByRef p_editableCellStyle As GridCellStyle)

        p_row.CellsFormatString = "{0:P}"
        p_row.CellsTextAlignment = Drawing.ContentAlignment.MiddleRight
        m_allocationsKeysDGV.CellsArea.SetCellValue(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items(0), 0)

        Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, p_row.ItemValue)
        If l_entity Is Nothing Then Exit Sub
        If l_entity.AllowEdition = False Then
            p_row.Enabled = False
            '  p_row.CellsStyle = p_nonEditionCellStyle
            m_allocationsKeysDGV.CellsArea.SetCellDrawStyle(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items(0), p_nonEditionCellStyle)
        Else
            p_row.CellsEditor = m_allocationTextBoxEditor
            m_allocationsKeysDGV.CellsArea.SetCellDrawStyle(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items(0), p_editableCellStyle)
        End If

        For Each l_childrenRow As HierarchyItem In p_row.Items
            SpecificyAllocationKeysEditionEnabling(l_childrenRow, p_nonEditionCellStyle, p_editableCellStyle)
        Next

    End Sub

#End Region


#Region "Interface"

    Friend Delegate Sub FillAllocationKeysDataGridView_Delegate(ByRef p_entitiesAllocationKeysDictionary As Dictionary(Of Int32, Double))
    Friend Sub FillAllocationKeysDataGridView_ThreadSafe(ByRef p_entitiesAllocationKeysDictionary As Dictionary(Of Int32, Double))

        If Me.m_allocationsKeysDGV.InvokeRequired Then
            Dim MyDelegate As New FillAllocationKeysDataGridView_Delegate(AddressOf FillAllocationKeysDataGridView_ThreadSafe)
            Me.m_allocationsKeysDGV.Invoke(MyDelegate, New Object() {p_entitiesAllocationKeysDictionary})
        Else
            m_isFillingCells = True
            For Each l_row As HierarchyItem In m_allocationsKeysDGV.RowsHierarchy.Items
                FillChildRow(l_row, p_entitiesAllocationKeysDictionary)
            Next
            m_isFillingCells = False
        End If

    End Sub

    Private Sub FillChildRow(ByRef p_row As HierarchyItem, _
                             ByRef p_entitiesAllocationKeysDictionary As Dictionary(Of Int32, Double))

        Dim l_keyValue As Double = p_entitiesAllocationKeysDictionary(p_row.ItemValue)
        m_allocationsKeysDGV.CellsArea.SetCellValue(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items(0), l_keyValue / 100.0)
        For Each l_childRow As HierarchyItem In p_row.Items
            FillChildRow(l_childRow, p_entitiesAllocationKeysDictionary)
        Next

    End Sub

    Friend Delegate Sub SetAllocationKeyValue_Delegate(ByRef p_entitiesDistribution As CRUD.EntityDistribution)
    Friend Sub SetAllocationKeyValue_ThreadSafe(ByRef p_entitiesDistribution As CRUD.EntityDistribution)

        If Me.m_allocationsKeysDGV.InvokeRequired Then
            Dim MyDelegate As New SetAllocationKeyValue_Delegate(AddressOf SetAllocationKeyValue_ThreadSafe)
            Me.m_allocationsKeysDGV.Invoke(MyDelegate, New Object() {p_entitiesDistribution})
        Else
            m_isFillingCells = True
            Dim l_row As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_allocationsKeysDGV.RowsHierarchy, p_entitiesDistribution.EntityId)
            m_allocationsKeysDGV.CellsArea.SetCellValue(l_row, m_allocationsKeysDGV.ColumnsHierarchy.Items(0), p_entitiesDistribution.Percentage / 100.0)
            m_isFillingCells = False
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub DataGridView_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If m_isFillingCells = False Then
            If Not IsNumeric(args.NewValue) Then
                args.Cancel = True
            Else
                Dim l_entitiesAllocationKeysDict = GetEntityAllocationKeysDictionary()
                l_entitiesAllocationKeysDict(args.Cell.RowItem.ItemValue) = args.NewValue
                If IsTotalPercentageValid(l_entitiesAllocationKeysDict) = False Then
                    args.Cancel = True
                    MsgBox(Local.GetValue("allocationKeys.msg_percentageOver100"))
                Else
                    m_controller.UpdateAllocationKey(args.Cell.RowItem.ItemValue, args.NewValue)
                End If
            End If
        End If

    End Sub

    Private Sub DataGridView_EditorActivation(sender As Object, e As EditorActivationCancelEventArgs)

        e.Cell.Value *= 100

    End Sub

#End Region


#Region "Utilities"

    Friend Function GetEntityAllocationKeysDictionary() As Dictionary(Of Int32, Double)

        Dim l_entitiesAllocationsKeysDict As New Dictionary(Of Int32, Double)
        For Each l_row As HierarchyItem In m_allocationsKeysDGV.RowsHierarchy.Items
            SetChildKeys(l_row, l_entitiesAllocationsKeysDict)
        Next
        Return l_entitiesAllocationsKeysDict

    End Function

    Private Sub SetChildKeys(ByRef p_row As HierarchyItem, _
                             ByRef p_entitiesAllocationsKeysDict As Dictionary(Of Int32, Double))

        Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, p_row.ItemValue)
        If l_entity Is Nothing Then Exit Sub
        If l_entity.AllowEdition = True Then
            p_entitiesAllocationsKeysDict.Add(l_entity.Id, m_allocationsKeysDGV.CellsArea.GetCellValue(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items(0)) * 100)
        End If
        For Each l_childRow As HierarchyItem In p_row.Items
            SetChildKeys(l_childRow, p_entitiesAllocationsKeysDict)
        Next

    End Sub

    Private Function IsTotalPercentageValid(ByRef p_entitiesAllocationsKeysDict As Dictionary(Of Int32, Double)) As Boolean

        Dim l_totalPercentage As Double = 0
        For Each entityAllocationKeyPair In p_entitiesAllocationsKeysDict
            l_totalPercentage += entityAllocationKeyPair.Value
            If l_totalPercentage > 100 Then Return False
        Next
        Return True

    End Function

#End Region

End Class