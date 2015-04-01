'ViewsController.vb
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 07/12/2014

Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ViewsController


#Region "Instance Variables"

    ' Objects
    Private DataViewFactory As New SQLDataViews
    Private EntitiesViewsFactory As New SQLEntitiesViews
    Private PrivilegesController As New SQLPrivileges

    ' Variables
    Private CredentialTV As New TreeView
    Private credential_levels_list As New List(Of String)

#End Region


#Region "Tables VIEWS creation / delete"

    Protected Friend Sub CreateAllViews(ByRef entities_credentials_TV As TreeView)

        CredentialTV = entities_credentials_TV
        Dim data_versions_list As List(Of String) = VersionsMapping.GetVersionsList(VERSIONS_CODE_VARIABLE)
        Dim entities_credential_levels_dict = CredentialsLevelsMapping.GetCredentialKeysDictionaries(CREDENTIALS_ASSETID_VARIABLE, CREDENTIALS_ID_VARIABLE)

        For Each entity_id As String In entities_credential_levels_dict.Keys
            Dim credential_level = entities_credential_levels_dict(entity_id)
            BuildCredentialLevelsList(entity_id)

            For Each version_id As String In data_versions_list
                DataViewFactory.ViewCreationQuery(version_id, version_id & credential_level, credential_levels_list)
            Next
            EntitiesViewsFactory.CreateEntitiesView(credential_level, credential_levels_list)
        Next
        PrivilegesController.GrantAllUsersPrivilegesOnAuthorizedDataViews()

    End Sub

    Protected Friend Sub CreateAllEntitiesViews()

        Entity.LoadEntitiesCredentialTree(CredentialTV)
        Dim entities_credential_levels_dict = CredentialsLevelsMapping.GetCredentialKeysDictionaries(CREDENTIALS_ASSETID_VARIABLE, CREDENTIALS_ID_VARIABLE)

        For Each entity_id As String In entities_credential_levels_dict.Keys
            Dim credential_level = entities_credential_levels_dict(entity_id)
            BuildCredentialLevelsList(entity_id)
            EntitiesViewsFactory.CreateEntitiesView(credential_level, credential_levels_list)
        Next

    End Sub

    Protected Friend Sub CreateDataTableViewsForAllCredentialLevels(ByRef version_id As String)

        PrivilegesController.GrantAllUsersPrivilegesOnNewDataTable(version_id)

        Entity.LoadEntitiesCredentialTree(CredentialTV)
        Dim entities_credential_levels_dict = CredentialsLevelsMapping.GetCredentialKeysDictionaries(CREDENTIALS_ASSETID_VARIABLE, CREDENTIALS_ID_VARIABLE)

        For Each entity_id As String In entities_credential_levels_dict.Keys
            Dim credential_level = entities_credential_levels_dict(entity_id)
            BuildCredentialLevelsList(entity_id)
            DataViewFactory.ViewCreationQuery(version_id, version_id & credential_level, credential_levels_list)
        Next
        PrivilegesController.GrantAllUsersPrivilegesOnAuthorizedDataViews()

    End Sub

    Protected Friend Sub DeleteViewsFromCredentialLevel(ByRef credential_level As Int32)

        Dim data_versions_list As List(Of String) = VersionsMapping.GetVersionsList(VERSIONS_CODE_VARIABLE)
        For Each version_id In data_versions_list
            EntitiesViewsFactory.DeleteViewQuery(version_id & credential_level)
        Next
        EntitiesViewsFactory.DeleteViewQuery(ENTITIES_TABLE & credential_level)

    End Sub

    Protected Friend Sub DeleteDataVersionViews(ByRef version_id As String)

        Dim credential_levels_entities_list = CredentialsLevelsMapping.GetCredentialsList(CREDENTIALS_ASSETID_VARIABLE)
        Dim entities_id_Credential_level_Dict = EntitiesMapping.GetEntitiesDictionary(ENTITIES_ID_VARIABLE, ENTITIES_CREDENTIAL_ID_VARIABLE)
        For Each entity_id As String In credential_levels_entities_list
            Dim credential_level = entities_id_Credential_level_Dict(entity_id)
            EntitiesViewsFactory.DeleteViewQuery(version_id & credential_level)
        Next

    End Sub

#End Region


#Region "Credential Levels Utilities"

    Private Sub BuildCredentialLevelsList(ByRef entity_id As String)

        credential_levels_list.Clear()
        Dim base_node = CredentialTV.Nodes.Find(entity_id, True)(0)
        AddChildrenCredentialToList(base_node)

    End Sub

    ' Recursively Adds the credential level (text of the treenodes) to the credentials list if not included
    Private Sub AddChildrenCredentialToList(ByRef inputNode As TreeNode)

        If Not credential_levels_list.Contains(inputNode.Text) Then credential_levels_list.Add(inputNode.Text)
        If inputNode.Nodes.Count > 0 Then
            For Each child As TreeNode In inputNode.Nodes
                AddChildrenCredentialToList(child)
            Next
        End If

    End Sub


#End Region



End Class
