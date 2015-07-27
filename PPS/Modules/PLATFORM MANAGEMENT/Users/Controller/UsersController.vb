' UsersController.vb
'
' 
' To do: 
'       - if change pwd = success then send pwd else try again ?
'   
'
' Known bugs: 
'       - issues with cmd. -> execute sql queries -> find a way more secure or manage potential errors...
'
'
' Author: Julien Monnereau
' Last modified: 25/06/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms



Friend Class UsersController


#Region "Instance variables"

    ' Objects
    Private View As UsersControl
    Private NewUserUI As NewUserUI
    Private usersTV As New TreeView
    Private PasswordGenerator As New PasswordGenerator
    Private SMTPConnection As New SMTPConnection
    Private PlatformMGTUI As PlatformMGTGeneralUI

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        View = New UsersControl(Me)
       
        View.UsersTGVMGT.InitializeTGVRows(usersTV)
        DisplayUsersTGV()
        NewUserUI = New NewUserUI(Me)

    End Sub

    Friend Sub DisplayUsersTGV()

        'Dim users_dictionary As New Dictionary(Of String, Hashtable)
        'Dim users_list = TreeViewsUtilities.GetNodesKeysList(usersTV)
        'For Each user_id In users_list
        '    Dim tmp_hash As New Hashtable
        '    tmp_hash.Add(USERS_IS_FOLDER_VARIABLE, Users.ReadUser(user_id, USERS_IS_FOLDER_VARIABLE))
        '    tmp_hash.Add(USERS_CREDENTIAL_TYPE_VARIABLE, Users.ReadUser(user_id, USERS_CREDENTIAL_TYPE_VARIABLE))
        '    tmp_hash.Add(USERS_ENTITY_ID_VARIABLE, Users.ReadUser(user_id, USERS_ENTITY_ID_VARIABLE))
        '    tmp_hash.Add(USERS_EMAIL_VARIABLE, Users.ReadUser(user_id, USERS_EMAIL_VARIABLE))
        '    users_dictionary.Add(user_id, tmp_hash)
        'Next
        'View.UsersTGVMGT.FillUsersTGV(users_dictionary)

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Windows.Forms.Panel, _
                              ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.Dispose()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "Interface"

    'Friend Sub CreateUser(ByRef user_id As String, _
    '                      ByRef entity_id As String, _
    '                      ByRef credential_type As String, _
    '                      ByRef user_email As String, _
    '                      Optional ByRef parent_item As HierarchyItem = Nothing)

    '    CredentialsControler.CreateCredentialLevelIfNeeded(entity_id)
    '    Dim credential_level = CredentialsControler.GetCredentialLevel(entity_id)
    '    Dim tmpHT As New Hashtable
    '    tmpHT.Add(USERS_ID_VARIABLE, user_id)
    '    If parent_item Is Nothing Then tmpHT.Add(USERS_PARENT_ID_VARIABLE, DBNull.Value) Else tmpHT.Add(USERS_PARENT_ID_VARIABLE, parent_item.Caption)
    '    tmpHT.Add(USERS_IS_FOLDER_VARIABLE, 0)
    '    tmpHT.Add(ITEMS_POSITIONS, 1)
    '    tmpHT.Add(USERS_ENTITY_ID_VARIABLE, entity_id)
    '    tmpHT.Add(USERS_CREDENTIAL_LEVEL_VARIABLE, credential_level)
    '    tmpHT.Add(USERS_CREDENTIAL_TYPE_VARIABLE, credential_type)
    '    tmpHT.Add(USERS_EMAIL_VARIABLE, user_email)
    '    Users.CreateUser(tmpHT)

    '    Dim pwd As String = PasswordGenerator.GeneratePassword()
    '    PrivilegesController.CreateUser(user_id, pwd)
    '    PrivilegesController.GrantPrivilegesToUser(user_id, entity_id, credential_type, credential_level)
    '    SMTPConnection.SendPassword(pwd, user_email, user_id)

    '    CreateRowAndNode(user_id, False, parent_item)
    '    UpdateUsersPositions()
    '    DisplayUsersTGV()
    '    View.Show()

    'End Sub

    'Friend Sub CreateFolder(ByRef user_id As String, _
    '                        Optional ByRef parent_item As HierarchyItem = Nothing)

    '    Dim tmpHT As New Hashtable
    '    tmpHT.Add(USERS_ID_VARIABLE, user_id)
    '    If parent_item Is Nothing Then tmpHT.Add(USERS_PARENT_ID_VARIABLE, DBNull.Value) Else tmpHT.Add(USERS_PARENT_ID_VARIABLE, parent_item.Caption)
    '    tmpHT.Add(USERS_IS_FOLDER_VARIABLE, 1)
    '    tmpHT.Add(ITEMS_POSITIONS, 1)

    '    Users.CreateUser(tmpHT)
    '    CreateRowAndNode(user_id, True, parent_item)
    '    UpdateUsersPositions()
    '    DisplayUsersTGV()

    'End Sub

    'Friend Sub DeleteUser(ByRef user_id As String)

    '    ' What if query fails ? !
    '    If Users.ReadUser(user_id, USERS_CREDENTIAL_TYPE_VARIABLE) = DATA_BASE_MANAGER_CREDENTIAL_TYPE Then ReCreateAllViewsFromCurrentUser()
    '    Users.DeleteUser(user_id)
    '    PrivilegesController.DropUser(user_id)
    '    DeleteRowAndNode(user_id)
    '    View.UsersTGVMGT.currentRowItem = Nothing

    'End Sub

    'Friend Sub DeleteFolder(ByRef user_id As String)

    '    Dim node = usersTV.Nodes.Find(user_id, True)(0)
    '    Dim users_to_delete_list = TreeViewsUtilities.GetNodesKeysList(node)
    '    users_to_delete_list.Reverse()
    '    For Each item_id In users_to_delete_list
    '        If Users.ReadUser(item_id, USERS_IS_FOLDER_VARIABLE) = 0 Then
    '            DeleteUser(item_id)
    '        Else
    '            Users.DeleteUser(item_id)
    '        End If
    '    Next
    '    DeleteRowAndNode(user_id)
    '    View.UsersTGVMGT.currentRowItem = Nothing

    'End Sub

    'Public Sub ReiniatilizePassword(ByRef user_id As String)

    '    Dim pwd As String = PasswordGenerator.GeneratePassword()
    '    PrivilegesController.ChangePassword(user_id, pwd)
    '    SMTPConnection.SendPassword(pwd, Users.ReadUser(user_id, USERS_EMAIL_VARIABLE), user_id)

    'End Sub

    'Friend Sub UpdateUserCredentialLevel(ByRef user_id As String, ByRef entity_id As String)

    '    CredentialsControler.CreateCredentialLevelIfNeeded(entity_id)
    '    PrivilegesController.RevokeAllPrivileges(user_id)
    '    Dim credential_level As Int32 = CredentialsControler.GetCredentialLevel(entity_id)
    '    Users.UpdateUser(user_id, USERS_CREDENTIAL_LEVEL_VARIABLE, credential_level)
    '    Users.UpdateUser(user_id, USERS_ENTITY_ID_VARIABLE, entity_id)
    '    Dim credential_type As String = Users.ReadUser(user_id, USERS_CREDENTIAL_TYPE_VARIABLE)
    '    PrivilegesController.GrantPrivilegesToUser(user_id, entity_id, credential_type, credential_level)

    'End Sub

    'Friend Sub UpdateUserCredentialType(ByRef user_id As String, ByRef credential_type As String)

    '    If credential_type = DATA_USER_CREDENTIAL_TYPE Then
    '        ReCreateAllViewsFromCurrentUser()
    '    End If
    '    Users.UpdateUser(user_id, USERS_CREDENTIAL_TYPE_VARIABLE, credential_type)
    '    Dim entity_id As String = Users.ReadUser(user_id, USERS_ENTITY_ID_VARIABLE)
    '    Dim credential_level As Int32 = Users.ReadUser(user_id, USERS_CREDENTIAL_LEVEL_VARIABLE)
    '    If credential_level <> -1 Then
    '        PrivilegesController.RevokeAllPrivileges(user_id)
    '        PrivilegesController.GrantPrivilegesToUser(user_id, entity_id, credential_type, credential_level)
    '    Else
    '        MsgBox("A Credential Entity must be granted first.")
    '    End If

    'End Sub

    'Friend Sub UpdateUserEmail(ByRef user_id As String, ByRef email As String)

    '    Users.UpdateUser(user_id, USERS_EMAIL_VARIABLE, email)

    'End Sub

#End Region


#Region "Utilities"

    Friend Function IsUSerIDAlreadyInUse(ByRef user_id As String) As Boolean

        If usersTV.Nodes.Find(user_id, True).Length > 0 Then Return True Else Return False

    End Function

    Private Sub CreateRowAndNode(ByRef user_id As String, _
                                 ByRef is_folder As Boolean, _
                                 Optional ByRef parent_item As HierarchyItem = Nothing)

        If parent_item Is Nothing Then
            usersTV.Nodes.Add(user_id, user_id)
        Else
            usersTV.Nodes.Find(parent_item.Caption, True)(0).Nodes.Add(user_id, user_id)
        End If
        View.UsersTGVMGT.AddRow(user_id, is_folder, parent_item)

    End Sub

    Private Sub DeleteRowAndNode(ByRef user_id As String)

        usersTV.Nodes.Find(user_id, True)(0).Remove()
        View.UsersTGVMGT.rowsIDItem(user_id).Delete()
        View.UsersTGVMGT.rowsIDItem.Remove(user_id)

    End Sub

    Friend Sub ShowNewUserUI()

        NewUserUI.UserIDTB.Text = ""
        NewUserUI.parent_item = View.UsersTGVMGT.currentRowItem
        NewUserUI.Show()

    End Sub

    Friend Sub ShowUsersMGTUI()

        View.Show()

    End Sub

    Private Sub ReCreateAllViewsFromCurrentUser()

        ' class to be deleted => on server !!!!! priority normal

        MsgBox("users mgt to be implemented on server")
        Dim entities_credentials_tv As New TreeView
        '  Entity.LoadEntitiesCredentialTree(entities_credentials_tv)
        'ViewsController.CreateAllViews(entities_credentials_tv)

    End Sub

#End Region



End Class
