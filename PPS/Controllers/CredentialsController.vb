' CredentialsKeysManagement.vb
'
' Transactions with CredentialsKeys table
' Register new credential key 
' Recompute entities ID | credential keys mapping 
' Update entities table
'
'
'
' To do:
'       - Check that the root node has a credential level and that there is only one root node
'       
'
' Known bugs:
'
'
'
' Author: Julien Monnereau
' Last modified: 09/12/2014


Imports System.Collections.Generic
Imports System.Windows.Forms


Public Class CredentialsController


#Region "Instance Variables"

    ' Objects
    Private CredentialLevels As CredentialLevel
    Private entities_credentials_TV As New TreeView
    Private ViewsController As New ViewsController
    Private Entities As New Entity


#End Region


#Region "Initialization"

    Public Sub New()

        CredentialLevels = New CredentialLevel
        If CredentialLevels.object_is_alive = False Then
            ' cannot continue !!
            Exit Sub
        End If

    End Sub


#End Region


#Region "Interface"

    Friend Function GetCredentialLevel(ByRef entity_id As String) As Int32

        Return CredentialLevels.ReadCredential_level(entity_id)

    End Function

    Friend Sub CreateCredentialLevelIfNeeded(ByRef entity_id As String)

        Dim credential_level = CredentialLevels.ReadCredential_level(entity_id)
        If credential_level = -1 Then CreateCredentialLevel(entity_id)

    End Sub

    Friend Sub CreateCredentialLevel(ByRef entity_id As String)

        Dim new_credential_level = CredentialsLevelsMapping.GetMaxCredentialKey + 1
        CredentialLevels.CreateCredential_level(entity_id, new_credential_level)
        ReComputeCredentialKeysMapping()
        UpdateEntitiesCredentialsLevels()
        ViewsController.CreateAllViews(entities_credentials_TV)

    End Sub

    Friend Sub DeleteCredentialKey(ByRef entity_id As String)

        If CredentialLevels.ReadCredential_level(entity_id) <> -1 Then

            Dim credential_level As Int32 = CredentialLevels.ReadCredential_level(entity_id)
            CredentialLevels.DeleteCredential_level(entity_id)
            ViewsController.DeleteViewsFromCredentialLevel(credential_level)
            ReComputeCredentialKeysMapping()
            UpdateEntitiesCredentialsLevels()
            ViewsController.CreateAllViews(entities_credentials_TV)
            SQLCredentials.UnvalidateCredentialLevelInUsers(credential_level)

        End If

    End Sub

#End Region


#Region "Credential keys Mapping recomputation"

    ' ReCompute the entitiesID | credential keys mapping
    Private Sub ReComputeCredentialKeysMapping()

        Entity.LoadEntitiesTree(entities_credentials_TV)

        ' preliminary check -> check that the root node has a credential level and that there is only one root node !
        For Each node As TreeNode In entities_credentials_TV.Nodes
            AssignCredentialKeyToAssetID(node)
        Next

    End Sub

    ' Recursive - Assign cred key to the param node
    Private Sub AssignCredentialKeyToAssetID(ByRef inputNode As TreeNode)

        Dim credential_level = CredentialLevels.ReadCredential_level(inputNode.Name)
        If credential_level <> -1 Then
            inputNode.Text = credential_level
        Else
            If Not inputNode.Parent Is Nothing Then
                inputNode.Text = inputNode.Parent.Text
            Else
                inputNode.Text = DEFAULT_CREDENTIAL_KEY
            End If
        End If

        If inputNode.Nodes.Count > 0 Then
            For Each child As TreeNode In inputNode.Nodes
                AssignCredentialKeyToAssetID(child)
            Next
        End If

    End Sub


#End Region


#Region "Utilities"

    Private Sub UpdateEntitiesCredentialsLevels()

        Dim entities_list = TreeViewsUtilities.GetNodesKeysList(entities_credentials_TV)
        For Each entity_id In entities_list
            Entities.UpdateEntity(entity_id, ENTITIES_CREDENTIAL_ID_VARIABLE, entities_credentials_TV.Nodes.Find(entity_id, True)(0).Text)
        Next

    End Sub


#End Region



End Class
