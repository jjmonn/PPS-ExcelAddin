' UsersTGV.vb
'
' Manages the Users tree grid view in UsersManagementUI.vb
'
' To do :
'       - 
'
' Known bugs:
'       -
'
'
' Author: Julien Monnereau
' Last modified: 05/01/2014


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports System.Drawing


Friend Class UsersTGV


#Region "Instance Variables"

    ' Objects
    Private CONTROLLER As UsersController
    Private TGV As vDataGridView
    Private ParentView As UsersManagementUI

    ' Variables
    Friend columnsDictionary As New Dictionary(Of String, HierarchyItem)
    Friend rowsIDItem As New Dictionary(Of String, HierarchyItem)
    Friend entitiesIDName As Hashtable
    Friend tmp_entity_name As String
    Friend tmp_entity_id As String
    Friend currentRowItem As HierarchyItem
    Private credentials_types_list As List(Of String)

    ' Editors
    Private CredentialTypeBoxEditor As New ComboBoxEditor()
    Private EntitySelectionTBEditor As New TextBoxEditor()
    Private EmailTBEditor As New TextBoxEditor()

    ' Constants
    Friend Const USER_CREDENTIAL_TYPE_COLUMN_NAME = "Credential Type"
    Friend Const USER_ENTITY_COLUMN_NAME = "Credential Level"
    Friend Const USER_EMAIL_COLUMN_NAME = "User's Email"
    Private Const CREDENTIAL_CB_WIDTH As Int32 = 30
    Private Const CREDENTIAL_CB_NB_ITEMS_DISPLYED As Int32 = 7
    Private Const TGV_FONT_SIZE As Single = 8


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef inputTGV As vDataGridView, _
                   ByRef input_parent_view As UsersManagementUI)

        ParentView = input_parent_view
        TGV = inputTGV
        InitializeTGVColumns()
        entitiesIDName = EntitiesMapping.GetEntitiesDictionary(ASSETS_TREE_ID_VARIABLE, ASSETS_NAME_VARIABLE)
        credentials_types_list = CredentialsTypesMapping.GetCredentialsTypesList(CREDENTIALS_DESCRIPTION_VARIABLE)

        InitCredentialTypesComboBox()
        TGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        AddHandler TGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler TGV.HierarchyItemMouseClick, AddressOf usersTGV_HierarchyItemMouseClick
        AddHandler TGV.CellEndEdit, AddressOf dataGrid_CellEndEdit
        AddHandler TGV.CellBeginEdit, AddressOf dataGrid_CellBeginEdit


    End Sub

    Friend Sub SetController(ByRef input_controller As UsersController)

        CONTROLLER = input_controller

    End Sub

    Private Sub InitCredentialTypesComboBox()

        CredentialTypeBoxEditor.DropDownHeight = CredentialTypeBoxEditor.ItemHeight * CREDENTIAL_CB_NB_ITEMS_DISPLYED
        CredentialTypeBoxEditor.DropDownWidth = CREDENTIAL_CB_WIDTH
        Dim credentialsList = CredentialsTypesMapping.GetCredentialsTypesList(CREDENTIALS_DESCRIPTION_VARIABLE)

        For Each credential As String In credentialsList
            CredentialTypeBoxEditor.Items.Add(credential)
        Next

        AddHandler CredentialTypeBoxEditor.EditBase.TextBox.KeyDown, AddressOf CredentialTypeCBEditor_KeyDown

    End Sub

    Private Sub InitializeTGVColumns()

        Dim col1 As HierarchyItem = TGV.ColumnsHierarchy.Items.Add(USER_CREDENTIAL_TYPE_COLUMN_NAME)
        columnsDictionary.Add(USERS_CREDENTIAL_TYPE_VARIABLE, col1)
        col1.CellsEditor = CredentialTypeBoxEditor

        Dim col2 As HierarchyItem = TGV.ColumnsHierarchy.Items.Add(USER_ENTITY_COLUMN_NAME)
        columnsDictionary.Add(USERS_ENTITY_ID_VARIABLE, col2)
        col2.CellsEditor = EntitySelectionTBEditor

        Dim col3 As HierarchyItem = TGV.ColumnsHierarchy.Items.Add(USER_EMAIL_COLUMN_NAME)
        columnsDictionary.Add(USERS_EMAIL_VARIABLE, col3)
        col3.CellsEditor = EmailTBEditor

        TGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DataGridViewsUtil.FormatDGVRowsHierarchy(TGV)
        DataGridViewsUtil.AdjustDGVFColumnWidth(TGV, 0)

    End Sub

    Friend Sub InitializeTGVRows(ByRef users_tv As TreeView)

        TGV.RowsHierarchy.Clear()
        rowsIDItem.Clear()
        For Each node In users_tv.Nodes
            Dim row As HierarchyItem = TGV.RowsHierarchy.Items.Add(node.name)
            rowsIDItem.Add(node.name, row)
            addChildrenRows(node, row)
        Next
        DataGridViewsUtil.DGVSetHiearchyFontSize(TGV, TGV_FONT_SIZE, TGV_FONT_SIZE)

    End Sub

    Private Sub addChildrenRows(ByRef node As TreeNode, ByRef row As HierarchyItem)

        For Each child_node In node.Nodes
            Dim sub_row As HierarchyItem = row.Items.Add(child_node.name)
            rowsIDItem.Add(child_node.Name, sub_row)
            addChildrenRows(child_node, sub_row)
        Next

    End Sub

#End Region


#Region "Interface"

    Friend Sub FillUsersTGV(ByRef users_dict As Dictionary(Of String, Hashtable))

        Dim row As HierarchyItem
        Dim hash As Hashtable
        For Each user_id In users_dict.Keys

            row = rowsIDItem(user_id)
            hash = users_dict(user_id)
            row.ImageIndex = hash(USERS_IS_FOLDER_VARIABLE)
            TGV.CellsArea.SetCellValue(row, columnsDictionary(USERS_CREDENTIAL_TYPE_VARIABLE), _
                                      hash(USERS_CREDENTIAL_TYPE_VARIABLE))

            TGV.CellsArea.SetCellValue(row, columnsDictionary(USERS_ENTITY_ID_VARIABLE), _
                                       entitiesIDName(hash(USERS_ENTITY_ID_VARIABLE)))

            TGV.CellsArea.SetCellValue(row, columnsDictionary(USERS_EMAIL_VARIABLE), _
                                       hash(USERS_EMAIL_VARIABLE))
        Next
        TGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        TGV.ColumnsHierarchy.AutoStretchColumns = True
        DataGridViewsUtil.FormatDGVRowsHierarchy(TGV)
        TGV.Refresh()

    End Sub

    Friend Sub AddRow(ByRef user_id As String, _
                      ByRef is_folder As Boolean, _
                      Optional ByRef parent_row As HierarchyItem = Nothing)

        Dim sub_row As HierarchyItem
        If parent_row Is Nothing Then
            sub_row = TGV.RowsHierarchy.Items.Add(user_id)
        Else
            sub_row = parent_row.Items.Add(user_id)
        End If
        rowsIDItem.Add(user_id, sub_row)
        If is_folder Then sub_row.ImageIndex = 1 Else sub_row.ImageIndex = 0

    End Sub

#End Region


#Region "Events"

    ' if right click menu managed here -> must set parent_item before sending new user creation !!

    Private Sub CredentialTypeCBEditor_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter
                TGV.CloseEditor(True)
            Case Keys.Escape
                TGV.CloseEditor(False)
        End Select

    End Sub

    Private Sub usersTGV_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        currentRowItem = args.HierarchyItem

    End Sub

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        currentRowItem = args.Cell.RowItem

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        If rowsIDItem.ContainsValue(args.HierarchyItem) Then
            currentRowItem = args.HierarchyItem
        End If

    End Sub

    Private Sub dataGrid_CellBeginEdit(ByVal sender As Object, ByVal args As CellCancelEventArgs)

        If args.Cell.ColumnItem Is columnsDictionary(USERS_ENTITY_ID_VARIABLE) Then
            tmp_entity_id = ""
            If args.Cell.Editor.EditorType = CellEditorType.TextBox Then
                ParentView.ShowEntitiesSelection()
            End If
        End If

    End Sub

    Private Sub dataGrid_CellEndEdit(ByVal sender As Object, ByVal args As CellCancelEventArgs)

        If args.Cell.RowItem.ImageIndex = 0 Then
            Dim cell As GridCell = args.Cell
            If args.Cell.ColumnItem Is columnsDictionary(USERS_ENTITY_ID_VARIABLE) Then
                If tmp_entity_id <> "" _
                AndAlso ParentView.EntitySelectionUI.entitiesTV.Nodes.Find(tmp_entity_id, True).Length > 0 Then
                    TGV.CellsArea.SetCellValue(cell.RowItem, cell.ColumnItem, tmp_entity_name)
                    CONTROLLER.UpdateUserCredentialLevel(args.Cell.RowItem.Caption, tmp_entity_id)
                    ParentView.HideEntitySelection()
                End If
            ElseIf args.Cell.ColumnItem Is columnsDictionary(USERS_CREDENTIAL_TYPE_VARIABLE) Then
                If credentials_types_list.Contains(cell.Editor.EditorValue) Then
                    CONTROLLER.UpdateUserCredentialType(cell.RowItem.Caption, cell.Editor.EditorValue)
                End If
            Else
                CONTROLLER.UpdateUserEmail(cell.RowItem.Caption, cell.Editor.EditorValue)
            End If
        End If

    End Sub

#End Region


#Region "Utilities"

    ' triggered by ENTSEL -> validate the entity choice
    Public Sub ValidateNewEntity(ByRef inputEntityName As String, ByRef inputEntityID As String)

        Me.tmp_entity_id = inputEntityID
        Me.tmp_entity_name = inputEntityName
        TGV.CloseEditor(True)

    End Sub


#End Region



End Class
