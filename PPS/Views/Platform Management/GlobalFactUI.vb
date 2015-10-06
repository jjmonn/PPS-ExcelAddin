Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.Controls
Imports System.Drawing


Friend Class GlobalFactUI

#Region "Instance Variables"

    ' Objects
    Friend m_controller As GlobalFactController
    Friend m_dataGridView As New vDataGridView
    Private m_versionsTV As vTreeView
    Private m_columnsVariableItemDictionary As New Dictionary(Of String, HierarchyItem)

    ' Variables
    Friend m_currentVersionId As Int32
    Private m_isFillingCells As Boolean
    Private m_isCopyingValueDown As Boolean = False
    Private m_textBoxEditor As New TextBoxEditor()
    Private m_selectedFact As Int32

    ' Constants
    Private LINES_WIDTH As Single = 3

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As GlobalFactController, _
                   ByRef p_ratesVersionsTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        m_versionsTV = p_ratesVersionsTV

        SplitContainer1.Panel1.Controls.Add(m_versionsTV)
        SplitContainer1.Panel2.Controls.Add(m_dataGridView)

        m_versionsTV.Dock = DockStyle.Fill
        m_dataGridView.Dock = DockStyle.Fill
        m_dataGridView.ContextMenuStrip = dgvRCM
        m_versionsTV.ContextMenuStrip = VersionsRCMenu
        m_versionsTV.ImageList = ratesVersionsIL
        VTreeViewUtil.InitTVFormat(m_versionsTV)

        AddHandler m_dataGridView.CellValueChanging, AddressOf DataGridView_CellValueChanging
        AddHandler m_versionsTV.KeyPress, AddressOf VersionsTV_KeyPress
        AddHandler m_versionsTV.MouseDoubleClick, AddressOf VersionsTV_MouseDoubleClick
        AddHandler GlobalVariables.GlobalFacts.Read, AddressOf ReloadUI
        AddHandler m_dataGridView.MouseDown, AddressOf FactRightClick

        DesactivateUnallowed()
    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            AddRatesVersionRCM.Enabled = False
            DeleteVersionRCM.Enabled = False
            AddFolderRCM.Enabled = False
            ImportFromExcelToolStripMenuItem.Enabled = False
        End If
    End Sub

    Delegate Sub ReloadUI_Delegate()
    Friend Sub ReloadUI()
        If InvokeRequired Then
            Dim MyDelegate As New ReloadUI_Delegate(AddressOf ReloadUI)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            If m_versionsTV.SelectedNode Is Nothing Then Exit Sub
            ChangeRatesVersionDisplayRequest(m_versionsTV.SelectedNode.Value)
        End If
    End Sub

    Private Sub ManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        m_dataGridView.AllowCopyPaste = True
        m_dataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        m_dataGridView.RowsHierarchy.CompactStyleRenderingEnabled = True
        m_dataGridView.Refresh()
        m_dataGridView.Select()

    End Sub

    Friend Sub closeControl()


    End Sub

#End Region


#Region "Interface"

    Private Sub ChangeRatesVersionDisplayRequest(ByRef p_ratesVersionId As Int32)

        m_controller.DisplayFacts(p_ratesVersionId)

    End Sub

    Delegate Sub TVUpdate_Delegate(ByRef ratesVersion_id As Int32, _
                                   ByRef ratesVersion_parent_id As Int32, _
                                   ByRef ratesVersion_name As String, _
                                   ByRef ratesVersion_image As Int32)
    Friend Sub TVUpdate(ByRef ratesVersion_id As Int32, _
                        ByRef ratesVersion_parent_id As Int32, _
                        ByRef ratesVersion_name As String, _
                        ByRef ratesVersion_image As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVUpdate_Delegate(AddressOf TVUpdate)
            Me.Invoke(MyDelegate, New Object() {ratesVersion_id, ratesVersion_parent_id, ratesVersion_name, ratesVersion_image})
        Else
            If ratesVersion_parent_id = 0 Then
                VTreeViewUtil.AddNode(ratesVersion_id, ratesVersion_name, m_versionsTV, ratesVersion_image)
            Else
                Dim ratesVersionNode As vTreeNode = VTreeViewUtil.FindNode(m_versionsTV, ratesVersion_parent_id)
                If Not ratesVersionNode Is Nothing Then
                    VTreeViewUtil.AddNode(ratesVersion_id, ratesVersion_name, ratesVersionNode, ratesVersion_image)
                End If
            End If
            m_versionsTV.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef p_ratesVersionId As Int32)
    Friend Sub TVNodeDelete(ByRef p_ratesVersionId As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.Invoke(MyDelegate, New Object() {p_ratesVersionId})
        Else
            Dim ratesVersionNode As vTreeNode = VTreeViewUtil.FindNode(m_versionsTV, p_ratesVersionId)
            If Not ratesVersionNode Is Nothing Then
                ratesVersionNode.Remove()
                m_versionsTV.Refresh()
            End If
        End If

    End Sub


#End Region


#Region "Call Backs"

    Private Sub ImportFromExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromExcelToolStripMenuItem.Click
        m_controller.ImportRatesFromExcel()
    End Sub

#Region "Versions Right Click Menu"

    Private Sub Select_version_Click(sender As Object, e As EventArgs) Handles select_version.Click

        If (m_versionsTV.SelectedNode Is Nothing) Then Exit Sub
        If m_controller.IsFolderVersion(m_versionsTV.SelectedNode.Value) = False Then
            ChangeRatesVersionDisplayRequest(m_versionsTV.SelectedNode.Value)
        End If

    End Sub

    Private Sub AddFolderRCM_Click(sender As Object, e As EventArgs) Handles AddFolderRCM.Click

        If Not m_versionsTV.SelectedNode Is Nothing Then
            If m_controller.IsFolderVersion(m_versionsTV.SelectedNode.Value) = False Then
                Dim name As String = InputBox("Please enter a name for the new Folder")
                If Len(name) < NAMES_MAX_LENGTH AndAlso Len(name) > 0 Then
                    m_controller.CreateVersion(m_versionsTV.SelectedNode.Value, name, 1, 0, 0)
                Else
                    MsgBox("The name cannot exceed " & NAMES_MAX_LENGTH & " characters")
                End If
            Else
                MsgBox("A folder can only be added to another folder")
            End If
        Else
            Dim name As String = InputBox("Please enter a name for the new Folder")
            If Len(name) < NAMES_MAX_LENGTH AndAlso Len(name) > 0 Then
                m_controller.CreateVersion(0, name, 1, 0, 0)
            Else
                MsgBox("The name cannot exceed 50 characters")
            End If
        End If

    End Sub

    Private Sub AddVersionRCM_Click(sender As Object, e As EventArgs) Handles AddRatesVersionRCM.Click

        If Not m_versionsTV.SelectedNode Is Nothing Then
            If m_controller.IsFolderVersion(m_versionsTV.SelectedNode.Value) = True Then
                m_controller.ShowNewFactVersion(m_versionsTV.SelectedNode.Value)
            Else
                MsgBox("A Version can only be added under a folder")
            End If
        Else
            m_controller.ShowNewFactVersion(0)
        End If

    End Sub

    Private Sub DeleteVersionBT_Click(sender As Object, e As EventArgs) Handles DeleteVersionRCM.Click

        If Not m_versionsTV.SelectedNode Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the version " + Chr(13) + Chr(13) + _
                                                      m_versionsTV.SelectedNode.Text + Chr(13) + Chr(13) + _
                                                      "This version and all sub versions will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                      "Version deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                m_controller.DeleteRatesVersion(m_versionsTV.SelectedNode.Value)
            End If
        End If

    End Sub

    Private Sub CreateNewFact_Click(sender As Object, e As EventArgs) Handles CreateNewFact.Click
        m_controller.ShowNewFact()
    End Sub

    Private Sub CreateNewFact2_Click(sender As Object, e As EventArgs) Handles CreateNewFact2.Click
        m_controller.ShowNewFact()
    End Sub

#End Region

#Region "m_ratesDataGridView Right Click Menu"

    Private Sub FactRightClick(sender As Object, e As MouseEventArgs)
        If (e.Button <> MouseButtons.Right) Then Exit Sub
        Dim target As HierarchyItem = m_dataGridView.ColumnsHierarchy.HitTest(e.Location)
        If target Is Nothing Then Exit Sub

        FactRightClickMenu.Visible = True
        FactRightClickMenu.Bounds = New Rectangle(MousePosition, New Size(FactRightClickMenu.Width, FactRightClickMenu.Height))
        m_selectedFact = target.ItemValue
    End Sub

    Private Sub CopyFactDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyFactDownToolStripMenuItem.Click
        CopyFactValueDown()
    End Sub

#End Region

#End Region

#Region "Events"

    Private Sub RenameVersionBT_Click(sender As Object, e As EventArgs) Handles RenameVersionBT.Click

        If m_versionsTV.SelectedNode Is Nothing Then Exit Sub
        Dim name As String = InputBox("Enter new name", "Rename version")

        If name <> "" Then m_controller.UpdateVersionName(m_versionsTV.SelectedNode.Value, name)
        m_versionsTV.SelectedNode.Remove()
    End Sub

    Private Sub DataGridView_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If m_isFillingCells = False Then
            If Not IsNumeric(args.NewValue) Then
                args.Cancel = True
            Else
                m_controller.UpdateFactData(args.Cell.RowItem.ItemValue, args.Cell.ColumnItem.ItemValue, m_currentVersionId, CInt(args.NewValue))
            End If
        End If

    End Sub


    Private Sub VersionsTV_MouseDoubleClick(sender As Object, e As MouseEventArgs)

        If m_versionsTV.SelectedNode Is Nothing Then Exit Sub
        If m_controller.IsFolderVersion(m_versionsTV.SelectedNode.Value) = False Then
            ChangeRatesVersionDisplayRequest(m_versionsTV.SelectedNode.Value)
            DesactivateUnallowed()
        End If

    End Sub

    Private Sub VersionsTV_KeyPress(sender As Object, e As KeyPressEventArgs)

        If e.KeyChar = Chr(13) AndAlso Not m_versionsTV.SelectedNode Is Nothing _
        AndAlso m_versionsTV.SelectedNode.SelectedImageIndex = 0 Then
            ChangeRatesVersionDisplayRequest(m_versionsTV.SelectedNode.Value)
            DesactivateUnallowed()
        End If
        '   If e.KeyChar = Chr(10) Then DeleteVersionBT_Click(sender, e)

    End Sub

    Private Sub RenameBT_Click(sender As Object, e As EventArgs) Handles RenameBT.Click

        Dim name As String = InputBox("Enter new name", "Rename fact")

        If name <> "" And Not m_controller.IsUsedName(name) Then m_controller.UpdateFactName(m_selectedFact, name)

    End Sub

    Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteBT.Click
        m_controller.DeleteFact(m_selectedFact)
    End Sub

#End Region


#Region "Facts m_ratesDataGridView"

    Friend Sub InitializeDGV(ByRef currenciesList As List(Of Int32), _
                             ByRef monthsIdList As List(Of Int32), _
                             ByRef p_versionid As Int32)

        m_currentVersionId = p_versionid
        m_dataGridView.RowsHierarchy.Clear()
        m_dataGridView.ColumnsHierarchy.Clear()
        InitColumns(currenciesList)
        InitRows(monthsIdList)
        DataGridViewsUtil.DGVSetHiearchyFontSize(m_dataGridView, My.Settings.tablesFontSize, My.Settings.tablesFontSize)
        m_dataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_dataGridView)
        m_dataGridView.RowsHierarchy.CompactStyleRenderingEnabled = False
        m_dataGridView.BackColor = Color.White
        m_dataGridView.Refresh()

    End Sub

    Private Sub InitColumns(ByRef currenciesList As List(Of Int32))

        m_dataGridView.ColumnsHierarchy.Clear()
        m_columnsVariableItemDictionary.Clear()

        For Each fact In m_controller.GetGlobalFactList()
            Dim column As HierarchyItem = m_dataGridView.ColumnsHierarchy.Items.Add(fact.Value(NAME_VARIABLE))

            m_columnsVariableItemDictionary.Add(fact.Value(NAME_VARIABLE), column)
            column.ItemValue = CInt(fact.Value(ID_VARIABLE))
            column.AllowFiltering = False
            m_dataGridView.ContextMenu = Nothing

        Next

    End Sub

    Private Sub InitRows(ByRef p_monthsIdList As List(Of Int32))

        For Each monthId As Int32 In p_monthsIdList
            Dim period As Date = Date.FromOADate(monthId)
            Dim row As HierarchyItem = m_dataGridView.RowsHierarchy.Items.Add(Format(period, "MMMMMMMM yyyy"))
            row.ItemValue = monthId
            m_textBoxEditor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL
            If GlobalVariables.Users.CurrentUserIsAdmin() Then row.CellsEditor = m_textBoxEditor

            m_isFillingCells = True
            For Each column In m_columnsVariableItemDictionary
                Dim fact As Double = m_controller.GetFact(monthId, column.Value.ItemValue, m_currentVersionId)
                m_dataGridView.CellsArea.SetCellValue(row, column.Value, fact)
            Next
            m_isFillingCells = False
        Next

    End Sub

    Friend Sub UpdateCell(ByRef p_currencyId As Int32, _
                          ByRef p_period As Int32, _
                          ByRef p_value As Double)

        m_dataGridView.CellsArea.SetCellValue(DataGridViewsUtil.GetHierarchyItemFromId(m_dataGridView.RowsHierarchy, p_period), _
                                                   DataGridViewsUtil.GetHierarchyItemFromId(m_dataGridView.ColumnsHierarchy, p_currencyId), _
                                                   p_value)

    End Sub

    Private Sub CopyValueIntoCellsBelow(ByRef parent_row As HierarchyItem, _
                                        ByRef start_index As Int32, _
                                        ByRef column As HierarchyItem, _
                                        ByRef value As Double)

        For i As Int32 = start_index To parent_row.Items.Count - 1
            m_dataGridView.CellsArea.SetCellValue(parent_row.Items(i), column, value)
        Next

    End Sub

    Friend Sub CopyFactValueDown()

        Dim value As Double = m_dataGridView.CellsArea.SelectedCells(0).Value
        Dim column As HierarchyItem = m_dataGridView.CellsArea.SelectedCells(0).ColumnItem
        Dim row As HierarchyItem = m_dataGridView.CellsArea.SelectedCells(0).RowItem
        m_isCopyingValueDown = True
        For i As Int32 = row.ItemIndex To m_dataGridView.RowsHierarchy.Items.Count - 1
            m_dataGridView.CellsArea.SetCellValue(m_dataGridView.RowsHierarchy.Items(i), column, value)
        Next
        m_isCopyingValueDown = False

    End Sub

#End Region

End Class
