' VersionSelectionUI.vb
' 
' Allows the user to select a version
'
'
'
' To do: 
'       -> to be deleted ? won't be used ...only for versions < 2000
'
'
' Known bugs: 
'
'
' Author: Julien Monnereau
' Last modified: 07/09/2015



Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms



Public Class VersionSelectionUI



#Region "Initialize"


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        VersionsTreeComboBox.TreeView.ImageList = VersioningTVIL
        GlobalVariables.Versions.LoadVersionsTV(VersionsTreeComboBox.TreeView)

    End Sub

#End Region


    ' Insert the selected version in VERSIONEDITBOX if selected node = version
    Private Sub SelectVersionIfNotFolder(ByRef inputNode As VIBlend.WinForms.Controls.vTreeNode)

        If GlobalVariables.Versions.IsVersionValid(inputNode.Value) = True Then
            AddinModule.SetCurrentVersionId(inputNode.Value)
            Me.Dispose()
            Me.Close()
        End If

    End Sub


#Region "Events"

    ' Validate Button and label call back
    Private Sub validate_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click

        If Not VersionsTreeComboBox.TreeView.SelectedNode Is Nothing Then
            SelectVersionIfNotFolder(VersionsTreeComboBox.TreeView.SelectedNode)
        Else
            MsgBox("Please select a version first.")
        End If

    End Sub

    ' Node Double click event
    Private Sub VersionsTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles VersionsTreeComboBox.MouseDoubleClick

        If Not VersionsTreeComboBox.TreeView.HitTest(e.Location) Is Nothing Then
            SelectVersionIfNotFolder(VersionsTreeComboBox.TreeView.HitTest(e.Location))
        End If

    End Sub

  

#End Region




End Class