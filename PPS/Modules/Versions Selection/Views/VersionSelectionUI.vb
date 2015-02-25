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
' Last modified: 11/12/2014



Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms



Public Class VersionSelectionUI


#Region "Instance variables"

    Private versions_id_list As List(Of String)

#End Region


#Region "Initialize"


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        VersionsTV.ImageList = VersioningTVIL
        Version.LoadVersionsTree(VersionsTV)
        VersionsTV.ExpandAll()
        versions_id_list = VersionsMapping.GetVersionsList(RATES_VERSIONS_ID_VARIABLE)

    End Sub


    Private Sub VersionSelectionUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Top = Cursor.Position.Y
        Me.Left = Cursor.Position.X

    End Sub


#End Region


#Region "Events"

    ' Validate Button and label call back
    Private Sub validate_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click

        If Not VersionsTV.SelectedNode Is Nothing Then
            SelectVersionIfNotFolder(VersionsTV.SelectedNode)
            Me.Dispose()
            Me.Close()
            Exit Sub
        Else
            MsgBox("A folder has been selected and cannot therefore been set up as a version." + Chr(13) + Chr(13) _
                    + "Please select a version.")
        End If

    End Sub

    ' Node Double click event
    Private Sub VersionsTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles VersionsTV.NodeMouseDoubleClick

        SelectVersionIfNotFolder(e.Node)
        Me.Dispose()
        Me.Close()
        Exit Sub

    End Sub

    ' Insert the selected version in VERSIONEDITBOX if selected node = version
    Private Sub SelectVersionIfNotFolder(ByRef inputNode As TreeNode)

        If versions_id_list.Contains(inputNode.Name) Then
            GlobalVariables.Version_Label.Caption = inputNode.Text
            GlobalVariables.Version_label_Sub_Ribbon.Text = inputNode.Text
        End If

    End Sub


#End Region




End Class