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
' Last modified: 20/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections




Friend Class VersionSelection


#Region "Instance Variables"

    Friend versionsTV As New TreeView
    Private versions_id_list As List(Of String)
    Friend View As Object

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputimagelist As ImageList, inputView As Object)

        View = inputView
        versionsTV.ImageList = inputimagelist
        Version.LoadVersionsTree(versionsTV)
        versionsTV.CollapseAll()
        versions_id_list = VersionsMapping.GetVersionsList(RATES_VERSIONS_ID_VARIABLE)

        AddHandler versionsTV.NodeMouseDoubleClick, AddressOf VersionsTV_NodeMouseDoubleClick
        AddHandler versionsTV.KeyPress, AddressOf VersionsTV_KeyPress

    End Sub

#End Region


#Region "Interface"

    ' Set main ribbon/ submission ribbon selected version = selected node
    Friend Overridable Sub SetSelectedVersion()

        If Not versionsTV.SelectedNode Is Nothing _
        AndAlso versions_id_list.Contains(versionsTV.SelectedNode.Name) Then

            Dim version_id = versionsTV.SelectedNode.Name
            GlobalVariables.GLOBALCurrentVersionCode = version_id
            GlobalVariables.Version_Label.Caption = versionsTV.SelectedNode.Text
            GlobalVariables.Version_label_Sub_Ribbon.Text = versionsTV.SelectedNode.Text
            If My.Settings.version_id <> version_id Then My.Settings.version_id = version_id
            View.ClearAndClose()

        End If

    End Sub

    Protected Friend Function IsVersionValid(ByRef version_id As String) As Boolean

        Return versions_id_list.Contains(version_id)

    End Function


#End Region

    Private Sub SetAssociatedRatesVersion_id(ByRef version_id As String)

        Dim Versions As New Version
        GlobalVariables.GLOBALCurrentRatesVersionCode = Versions.ReadVersion(version_id, VERSIONS_RATES_VERSION_ID_VAR)
        Versions.Close()
        Dim RatesVersions As New RateVersion
        GlobalVariables.Rates_Version_Label.Caption = RatesVersions.ReadVersion(GlobalVariables.GLOBALCurrentRatesVersionCode, NAME_VARIABLE)
        RatesVersions = Nothing

    End Sub


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
