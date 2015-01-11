﻿' CVersioningCONTROLER.vb
' 
' VersionsTable/ UI interface support functions
'
' To do:
'       - Improve process on Creation/ delete -> if one step fails !
'      
'
'
' Known bugs:
'
' 
' Last modified: 05/01/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections




Friend Class DataVersionsController


#Region "Instance Variables"

    'Objects
    Private Versions As Version
    Private ViewObject As VersioningManagementUI
    Private ViewsController As New ViewsController
    Private SQLVersions As New SQLVersions
    Private NewVersionUI As NewDataVersionUI

    ' Variables
    Private versionsTV As New TreeView
    Friend versionsNamesList As New List(Of String)
    Friend positions_dictionary As New Dictionary(Of String, Double)


    ' Constants
    Private FORBIDEN_CHARS As String() = {","}


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Versions = New Version
        If Versions.object_is_alive = False Then
            MsgBox("There was a network error. Please try again or contact your PPS contact.")
            Exit Sub
        End If

        Version.LoadVersionsTree(versionsTV)
        ViewObject = New VersioningManagementUI(Me, versionsTV)
        versionsNamesList = cTreeViews_Functions.GetNodesTextsList(versionsTV)
        NewVersionUI = New NewDataVersionUI(Me)
        positions_dictionary = cTreeViews_Functions.GeneratePositionsDictionary(versionsTV)
        ViewObject.Show()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub CreateVersion(ByRef hash As Hashtable, _
                                       Optional ByRef parent_node As TreeNode = Nothing, _
                                       Optional ByRef origin_version_id As String = "")

        Dim new_id As String = cTreeViews_Functions.GetNewNodeKey(versionsTV, VERSIONS_TOKEN_SIZE)
        hash.Add(RATES_VERSIONS_ID_VARIABLE, new_id)
        hash.Add(ITEMS_POSITIONS, 1)
        Dim table_creation_success As Boolean

        If origin_version_id = "" Then
            table_creation_success = SQLVersions.CreateNewVersionTable(new_id)
        Else
            table_creation_success = SQLVersions.CreateNewVersionTableFrom(new_id, origin_version_id)
        End If
        If table_creation_success Then
            Versions.CreateVersion(hash)
            AddNode(new_id, hash(VERSIONS_NAME_VARIABLE), 0, parent_node)
            ViewsController.CreateDataTableViewsForAllCredentialLevels(new_id)
        Else
            MsgBox("The Version could not be created. PPS Error 8. Please contact PPS Team if the error persists.")
        End If
        NewVersionUI.Hide()

    End Sub

    Protected Friend Sub CreateFolder(ByRef folder_name As String, _
                                      Optional parent_node As TreeNode = Nothing)

        Dim new_id As String = cTreeViews_Functions.GetNewNodeKey(versionsTV, VERSIONS_TOKEN_SIZE)
        Dim hash As New Hashtable
        hash.Add(RATES_VERSIONS_ID_VARIABLE, new_id)
        hash.Add(VERSIONS_IS_FOLDER_VARIABLE, 1)
        hash.Add(ITEMS_POSITIONS, 1)
        If Not parent_node Is Nothing Then hash.Add(VERSIONS_PARENT_CODE_VARIABLE, parent_node.Name)
       
        Versions.CreateVersion(hash)
        AddNode(new_id, folder_name, 1, parent_node)

    End Sub

    Protected Friend Function GetRecord(ByRef version_id As String) As Hashtable

        Return Versions.GetRecord(version_id)

    End Function

    Protected Friend Sub UpdateParent(ByRef version_id As String, ByRef parent_id As String)

        Versions.UpdateVersion(version_id, VERSIONS_PARENT_CODE_VARIABLE, parent_id)

    End Sub

    Protected Friend Sub UpdateName(ByRef version_id As String, ByRef name As String)

        Versions.UpdateVersion(version_id, VERSIONS_NAME_VARIABLE, name)

    End Sub

    Protected Friend Sub DeleteVersions(ByRef node As TreeNode)

        Dim versions_to_delete = cTreeViews_Functions.GetNodesKeysList(node)
        versions_to_delete.Reverse()
        For Each version_id In versions_to_delete
            If IsFolder(version_id) Then
                Delete(version_id)
            Else
                DeleteVersion(version_id)
            End If
        Next
        node.Remove()

    End Sub

    Private Sub DeleteVersion(ByRef version_id As String)

        SQLVersions.DeleteVersionQueries(version_id)
        ViewsController.DeleteDataVersionViews(version_id)
        Delete(version_id)

    End Sub

    Private Sub Delete(ByRef version_id As String)

        Versions.DeleteVersion(version_id)
        versionsNamesList.Remove(Versions.ReadVersion(version_id, VERSIONS_NAME_VARIABLE))

    End Sub

    Protected Friend Sub LockVersion(ByRef version_id As String)

        SQLVersions.LockDataTable(version_id)
        Versions.UpdateVersion(version_id, VERSIONS_LOCKED_VARIABLE, 1)
        Versions.UpdateVersion(version_id, VERSIONS_LOCKED_DATE_VARIABLE, Format(Now, "short Date"))

    End Sub

    Protected Friend Sub UnlockVersion(ByRef version_id As String)

        SQLVersions.UnlockDataTable(version_id)
        Versions.UpdateVersion(version_id, VERSIONS_LOCKED_VARIABLE, 0)
        Versions.UpdateVersion(version_id, VERSIONS_LOCKED_DATE_VARIABLE, "NA")

    End Sub

#End Region


#Region "utilities"

    Protected Friend Function IsFolder(ByRef version_id As String) As Boolean

        If Versions.ReadVersion(version_id, VERSIONS_IS_FOLDER_VARIABLE) = 1 Then Return True
        Return False

    End Function

    Protected Friend Sub ShowVersionsMGT()

        ViewObject.Show()

    End Sub

    Protected Friend Sub ShowNewVersionUI(Optional ByRef input_parent_node As TreeNode = Nothing)

        NewVersionUI.PreFill(input_parent_node)
        NewVersionUI.Show()
        NewVersionUI.HideVersionsTV()

    End Sub

    Protected Friend Function IsNameValid(ByRef name As String)

        If versionsNamesList.Contains(name) Then Return False
        If name.Length > DATA_VERSION_NAME_MAX_LENGTH Then Return False
        For Each char_ In FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        Return True

    End Function

    Protected Friend Sub AddNode(ByRef id As String, _
                                 ByRef name As String, _
                                 ByRef is_folder As Int32, _
                                 Optional ByRef parent_node As TreeNode = Nothing)

        If parent_node Is Nothing Then
            versionsTV.Nodes.Add(id, name, is_folder, is_folder)
        Else
            parent_node.Nodes.Add(id, name, is_folder, is_folder)
        End If
        positions_dictionary = cTreeViews_Functions.GeneratePositionsDictionary(versionsTV)
        Versions.UpdateVersion(id, ITEMS_POSITIONS, positions_dictionary(id))

    End Sub

    Protected Friend Sub SendNewPositionsToModel()

        For Each category_id In positions_dictionary.Keys
            Versions.UpdateVersion(category_id, ITEMS_POSITIONS, positions_dictionary(category_id))
        Next

    End Sub


#End Region



End Class
