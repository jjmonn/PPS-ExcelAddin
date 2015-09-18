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

Friend Class UsersControl


#Region "Instance variables"

    ' Objects
    Private m_controller As UsersController
    Private m_dataGridView As New vDataGridView
    Private m_columnsVariableItemDictionary As New Dictionary(Of String, HierarchyItem)

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As UsersController)
        InitializeComponent()

        m_controller = p_controller
        Me.Controls.Add(m_dataGridView)
        m_dataGridView.Dock = DockStyle.Fill
        ColumnsInitialize()
        RowsInitialize()
        InitGroupList()

        m_dataGridView.VIBlendTheme = VIBLEND_THEME.OFFICESILVER
        m_dataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

        AddHandler GlobalVariables.Users.UpdateEvent, AddressOf Controller_Update

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

    End Sub

    Friend Sub RowsInitialize()

        RemoveHandler m_dataGridView.CellValueChanged, AddressOf DataGridView_ValueChanged

        m_dataGridView.RowsHierarchy.Clear()
        For Each user In m_controller.GetUserList()
            CreateRow(user.Value)
        Next

        AddHandler m_dataGridView.CellValueChanged, AddressOf DataGridView_ValueChanged

    End Sub

    Private Function CreateRow(ByRef user As Hashtable) As HierarchyItem

        Dim row As HierarchyItem
        row = m_dataGridView.RowsHierarchy.Items.Add("")
        row.ItemValue = user(ID_VARIABLE)
        m_dataGridView.CellsArea.SetCellValue(row, m_columnsVariableItemDictionary(NAME_VARIABLE), user(NAME_VARIABLE))
        m_dataGridView.CellsArea.SetCellValue(row, m_columnsVariableItemDictionary(GROUP_ID_VARIABLE), m_controller.GetGroupName(user(GROUP_ID_VARIABLE)))
        Return row

    End Function

    Private Sub InitGroupList()

        Dim list As New ComboBoxEditor()
        list.DropDownList = True

        For Each group In m_controller.GetGroupList()
            Dim listItem As New ListItem
            listItem.Value = group.Value(ID_VARIABLE)
            listItem.Text = group.Value(NAME_VARIABLE)
            list.Items.Add(listItem)
        Next
        m_columnsVariableItemDictionary(GROUP_ID_VARIABLE).CellsEditor = list

    End Sub

#End Region

#Region "CRUD Event"

    Private Sub Controller_Update(ByRef state As Boolean, ByRef id As Int32)
        RowsInitialize()
    End Sub

#End Region

#Region "UI Event"

    Private Sub DataGridView_ValueChanged(sender As Object, args As CellEventArgs)

        Dim list As ComboBoxEditor = args.Cell.Editor

        Select Case args.Cell.ColumnItem.ItemValue

            Case GROUP_ID_VARIABLE
                Dim groupId As Int32 = list.SelectedItem.Value

                m_controller.SetUserGroup(args.Cell.RowItem.ItemValue, groupId)
        End Select

    End Sub

#End Region

End Class
