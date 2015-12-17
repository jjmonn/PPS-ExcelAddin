' UsersControl.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 25/06/2015


Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports System.Windows.Forms
Imports CRUD

Friend Class UsersControl


#Region "Instance variables"

    ' Objects
    Private m_controller As UsersController
    Private m_dataGridView As New vDataGridView
    Private m_columnsVariableItemDictionary As New SafeDictionary(Of String, HierarchyItem)
    Private m_displayEntities As Boolean = False
    Private m_selectedUser As UInt32 = 0
    Private m_currentNode As VIBlend.WinForms.Controls.vTreeNode
    Private m_isLoading As Boolean
    Friend m_rowsIdsItemDic As New SafeDictionary(Of UInt32, HierarchyItem)

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As UsersController)

        InitializeComponent()

        m_controller = p_controller
        Me.LayoutPanel.Controls.Add(m_dataGridView, 0, 0)
        m_dataGridView.Dock = DockStyle.Fill
        ColumnsInitialize()
        RowsInitialize()
        InitGroupList()
        InitEntitiesList()
        InitializeFormats()

        m_dataGridView.VIBlendTheme = VIBLEND_THEME.OFFICESILVER
        m_dataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

        VTreeViewUtil.LoadTreeview(EntitiesTV, GlobalVariables.AxisElems.GetDictionary(AxisType.Entities))
        VTreeViewUtil.InitTVFormat(EntitiesTV)
        EntitiesTV.TriStateMode = True
        EntitiesTV.CheckBoxes = True
        DisplayEntities = False

        AddHandler GlobalVariables.Users.UpdateEvent, AddressOf Controller_Update
        AddHandler m_dataGridView.CellMouseClick, AddressOf m_datagridview_click
        AddHandler GlobalVariables.UserAllowedEntities.Read, AddressOf UserAllowedEntities_READ
        AddHandler GlobalVariables.UserAllowedEntities.DeleteEvent, AddressOf UserAllowedEntities_DELETE
        AddHandler EntitiesTV.NodeChecked, AddressOf entitiesTreeView_Check
        AddHandler EntitiesTV.MouseClick, AddressOf entitiesTreeView_Click
        AddHandler GlobalVariables.Users.Read, AddressOf User_READ

    End Sub

    Private Property DisplayEntities As Boolean
        Set(value As Boolean)
            m_displayEntities = value
            If (value = True) Then
                Me.LayoutPanel.ColumnStyles(1).Width = 350
                LoadCurrent()
            Else
                Me.LayoutPanel.ColumnStyles(1).Width = 0
            End If
            Me.LayoutPanel.ResumeLayout()
        End Set
        Get
            Return m_displayEntities
        End Get
    End Property

    Private Sub LoadCurrent()
        m_isLoading = True
        CheckEntityTree(EntitiesTV.Nodes, False)
        EntitiesTV.Refresh()
        m_isLoading = False
    End Sub

    Private Function SetCaseStatus(ByRef p_case As vTreeNode, ByRef p_parentChecked As Boolean) As Boolean
        p_case.ShowCheckBox = True
        If (p_parentChecked = True) Then
            p_case.ShowCheckBox = False
            Return True
        End If
        If (m_controller.IsAllowedEntity(m_selectedUser, p_case.Value)) Then
            p_case.Checked = CheckState.Checked
            Return True
        End If
        p_case.Checked = CheckState.Unchecked
        Return False
    End Function

    Private Sub CheckEntityTree(ByRef p_node As vTreeNodeCollection, ByRef p_parentChecked As Boolean)
        For Each entityBox In p_node
            Dim checkedParent = SetCaseStatus(entityBox, p_parentChecked)
            For Each child In entityBox.Nodes
                Dim checkedChild = SetCaseStatus(child, checkedParent)
                CheckEntityTree(child.Nodes, checkedChild)
            Next
        Next
    End Sub

    Private Sub ColumnsInitialize()

        m_dataGridView.ColumnsHierarchy.Clear()

        ' User Name Column
        Dim nameColumn As HierarchyItem = m_dataGridView.ColumnsHierarchy.Items.Add("Name")
        nameColumn.ItemValue = NAME_VARIABLE
        m_columnsVariableItemDictionary.Add(NAME_VARIABLE, nameColumn)
        nameColumn.AllowFiltering = True

        ' User Group Column
        Dim groupColumn As HierarchyItem = m_dataGridView.ColumnsHierarchy.Items.Add("Group")
        groupColumn.ItemValue = GROUP_ID_VARIABLE
        m_columnsVariableItemDictionary.Add(GROUP_ID_VARIABLE, groupColumn)
        groupColumn.AllowFiltering = True

        ' User Entities Column
        Dim entitiesColumn As HierarchyItem = m_dataGridView.ColumnsHierarchy.Items.Add("Entities")
        entitiesColumn.ItemValue = ENTITIES_VARIABLE
        m_columnsVariableItemDictionary.Add(ENTITIES_VARIABLE, entitiesColumn)
        entitiesColumn.AllowFiltering = True

    End Sub

    Friend Sub RowsInitialize()

        RemoveHandler m_dataGridView.CellValueChanged, AddressOf DataGridView_ValueChanged

        m_dataGridView.RowsHierarchy.Clear()
        For Each user In m_controller.GetUserList().Values
            CreateRow(user)
        Next

        AddHandler m_dataGridView.CellValueChanged, AddressOf DataGridView_ValueChanged

    End Sub

    Private Function CreateRow(ByRef user As User) As HierarchyItem

        Dim row As HierarchyItem
        row = m_dataGridView.RowsHierarchy.Items.Add("")
        row.ItemValue = user.Id
        m_rowsIdsItemDic(user.Id) = row
        FillRow(user, row)

        Return row

    End Function

    Private Sub FillRow(ByRef p_user As User, ByRef p_row As HierarchyItem)
        m_dataGridView.CellsArea.SetCellValue(p_row, m_columnsVariableItemDictionary(NAME_VARIABLE), p_user.Name)
        m_dataGridView.CellsArea.SetCellValue(p_row, m_columnsVariableItemDictionary(GROUP_ID_VARIABLE), m_controller.GetGroupName(p_user.GroupId))
        m_dataGridView.CellsArea.SetCellValue(p_row, m_columnsVariableItemDictionary(ENTITIES_VARIABLE), GenerateEntityList(p_user.Id))
    End Sub

    Private Function GenerateEntityList(ByRef p_userId As UInt32) As String
        Dim allowedEntities As MultiIndexDictionary(Of UInt32, UInt32, UserAllowedEntity) = GlobalVariables.UserAllowedEntities.GetDictionary(p_userId)
        Dim entitiesStr As String = ""

        If allowedEntities IsNot Nothing Then
            For Each userEntity As UserAllowedEntity In allowedEntities.SortedValues
                Dim entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, userEntity.EntityId)
                If entity Is Nothing Then Continue For

                entitiesStr &= entity.Name & "; "
            Next
        End If
        Return entitiesStr
    End Function

    Private Sub InitGroupList()

        Dim list As New ComboBoxEditor()
        list.DropDownList = True

        For Each group As Group In m_controller.GetGroupList().Values
            Dim listItem As New ListItem
            listItem.Value = group.Id
            listItem.Text = group.Name
            list.Items.Add(listItem)
        Next
        m_columnsVariableItemDictionary(GROUP_ID_VARIABLE).CellsEditor = list

    End Sub

    Private Sub InitEntitiesList()

        'Dim list As New vTreeViewBox

        'VTreeViewUtil.InitTVFormat(list.TreeView)
        'VTreeViewUtil.LoadTreeview(list.TreeView, GlobalVariables.AxisElems.GetDictionary(AxisType.Entities))

        'm_columnsVariableItemDictionary(ENTITIES_VARIABLE).CellsEditor = list

    End Sub

    Private Sub InitializeFormats()

        m_dataGridView.BackColor = System.Drawing.SystemColors.Control
        m_dataGridView.ColumnsHierarchy.AutoStretchColumns = True
        m_dataGridView.RowsHierarchy.Visible = False
        m_dataGridView.Dock = DockStyle.Fill

    End Sub

    Private Sub UpdateUser(ByRef p_userId As UInt32)
        Dim user As User = GlobalVariables.Users.GetValue(p_userId)
        Dim row As HierarchyItem = m_rowsIdsItemDic(user.Id)

        m_isLoading = True
        If row IsNot Nothing Then
            FillRow(user, row)
        Else
            CreateRow(user)
        End If
        m_isLoading = False
    End Sub

#End Region

#Region "CRUD Event"

    Private Sub Controller_Update(ByRef state As Boolean, ByRef id As Int32)
        RowsInitialize()
    End Sub

    Delegate Sub UserAllowedEntities_READ_Delegate(ByRef p_status As ErrorMessage, ByRef p_userAllowedEntity As UserAllowedEntity)
    Private Sub UserAllowedEntities_READ(ByRef p_status As ErrorMessage, ByRef p_userAllowedEntity As UserAllowedEntity)
        If InvokeRequired Then
            Dim MyDelegate As New UserAllowedEntities_READ_Delegate(AddressOf UserAllowedEntities_READ)
            Me.Invoke(MyDelegate, New Object() {p_status, p_userAllowedEntity})
        Else
            If p_status <> ErrorMessage.SUCCESS Then Exit Sub

            UpdateUser(p_userAllowedEntity.UserId)
            LoadCurrent()
            m_dataGridView.Refresh()
        End If
    End Sub

    Delegate Sub UserAllowedEntities_DELETE_Delegate(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)
    Private Sub UserAllowedEntities_DELETE(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)
        If InvokeRequired Then
            Dim MyDelegate As New UserAllowedEntities_DELETE_Delegate(AddressOf UserAllowedEntities_DELETE)
            Me.Invoke(MyDelegate, New Object() {p_status, p_id})
        Else
            If p_status <> ErrorMessage.SUCCESS Then Exit Sub

            For Each l_user As User In GlobalVariables.Users.GetDictionary().Values
                UpdateUser(l_user.Id)
            Next
            m_dataGridView.Refresh()
            LoadCurrent()
        End If
    End Sub

    Delegate Sub User_READ_Delegate(ByRef p_status As ErrorMessage, ByRef p_user As User)
    Private Sub User_READ(ByRef p_status As ErrorMessage, ByRef p_user As User)
        If InvokeRequired Then
            Dim MyDelegate As New User_READ_Delegate(AddressOf User_READ)
            Me.Invoke(MyDelegate, New Object() {p_status, p_user})
        Else
            If p_status <> ErrorMessage.SUCCESS Then Exit Sub

            UpdateUser(p_user.Id)
            m_dataGridView.Refresh()
        End If
    End Sub

#End Region

#Region "UI Event"

    Private Sub DataGridView_ValueChanged(sender As Object, args As CellEventArgs)

        If m_isLoading = True Then Exit Sub
        Dim list As ComboBoxEditor = args.Cell.Editor

        Select Case args.Cell.ColumnItem.ItemValue

            Case GROUP_ID_VARIABLE
                Dim groupId As Int32 = list.SelectedItem.Value

                m_controller.SetUserGroup(args.Cell.RowItem.ItemValue, groupId)
        End Select

    End Sub

    Private Sub m_datagridview_click(sender As Object, args As CellMouseEventArgs)

        Dim list As ComboBoxEditor = args.Cell.Editor

        Select Case args.Cell.ColumnItem.ItemValue

            Case ENTITIES_VARIABLE
                Dim value As GridCell = m_columnsVariableItemDictionary(NAME_VARIABLE).Cells.GetValue(args.Cell.RowItem.ItemIndex)
                If value Is Nothing Then Exit Select

                If value.Value <> GlobalVariables.Users.GetCurrentUser().Name Then
                    DisplayEntities = DisplayEntities = False
                    m_selectedUser = GlobalVariables.Users.GetValueId(value.Value)
                End If

        End Select

    End Sub

    Private Sub entitiesTreeView_Check(sender As Object, e As vTreeViewEventArgs)
        If m_isLoading = True Then Exit Sub
        If m_currentNode Is Nothing Then Exit Sub
        On Error Resume Next
        If e.Node.Checked = CheckState.Checked Then
            m_controller.AddAllowedEntity(m_selectedUser, e.Node.Value)
        Else
            m_controller.RemoveAllowedEntity(m_selectedUser, e.Node.Value)
        End If
    End Sub

    Private Sub entitiesTreeView_Click(sender As Object, e As MouseEventArgs)

        m_currentNode = VTreeViewUtil.GetNodeAtPosition(EntitiesTV, e.Location)

    End Sub

#End Region

End Class
