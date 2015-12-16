' CVersionSelection.vb
'
' Model for versions selections UI/ Panes
'
' To do:
'       - 
'
'
'
' Known Bugs: 
'       - 
'
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections




Friend Class VersionSelection


#Region "Instance Variables"

    Friend versionsTV As New TreeView
    Friend View As Object

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputimagelist As ImageList, inputView As Object)

        View = inputView
        versionsTV.ImageList = inputimagelist
        GlobalVariables.Versions.LoadVersionsTV(versionsTV)
        versionsTV.CollapseAll()

        AddHandler versionsTV.NodeMouseDoubleClick, AddressOf VersionsTV_NodeMouseDoubleClick
        AddHandler versionsTV.KeyPress, AddressOf VersionsTV_KeyPress

    End Sub

#End Region


#Region "Interface"

    ' Set main ribbon/ submission ribbon selected version = selected node
    Friend Overridable Sub SetSelectedVersion()

        If Not versionsTV.SelectedNode Is Nothing AndAlso _
         GlobalVariables.Versions.IsVersionValid(CInt(versionsTV.SelectedNode.Name)) = True Then
            AddinModule.SetCurrentVersionId(versionsTV.SelectedNode.Name)
            View.ClearAndClose()
        End If

    End Sub

#End Region

#Region "Events"

    Private Sub VersionsTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)
        SetSelectedVersion()
    End Sub

    Private Sub VersionsTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Return) Then
            SetSelectedVersion()
        End If
    End Sub

#End Region


End Class
