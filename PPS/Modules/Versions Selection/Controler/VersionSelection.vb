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
' Last modified: 25/11/2014


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections




Public Class VersionSelection


#Region "Instance Variables"

    Friend versionsTV As New TreeView
    Friend rates_versionTV As New TreeView
    Private versions_id_list As List(Of String)
    Friend rates_versions_list As List(Of String)
    Friend VIEWOBJECT As Object

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputimagelist As ImageList, inputVIEWOBJECT As Object)

        VIEWOBJECT = inputVIEWOBJECT
        versionsTV.ImageList = inputimagelist
        rates_versionTV.ImageList = inputimagelist
        Version.LoadVersionsTree(versionsTV)
        versionsTV.CollapseAll()
        RateVersion.load_rates_version_tv(rates_versionTV)
        rates_versionTV.CollapseAll()
        rates_versions_list = RateVersionsMapping.GetRatesVersionsList(RATES_VERSIONS_ID_VARIABLE)
        versions_id_list = VersionsMapping.GetVersionsList(RATES_VERSIONS_ID_VARIABLE)

        AddHandler versionsTV.NodeMouseDoubleClick, AddressOf VersionsTV_NodeMouseDoubleClick
        AddHandler versionsTV.KeyPress, AddressOf VersionsTV_KeyPress
        AddHandler rates_versionTV.NodeMouseDoubleClick, AddressOf Rates_VersionsTV_DoubleClick
        AddHandler rates_versionTV.KeyPress, AddressOf Rates_VersionsTV_KeyPress

    End Sub

#End Region


#Region "Set Selected Version"

    ' Set main ribbon/ submission ribbon selected version = selected node
    Friend Overridable Sub SetSelectedVersion()

        If Not versionsTV.SelectedNode Is Nothing _
        AndAlso versions_id_list.Contains(versionsTV.SelectedNode.Name) _
        AndAlso Not rates_versionTV.SelectedNode Is Nothing _
        AndAlso rates_versions_list.Contains(rates_versionTV.SelectedNode.Name) Then
            GLOBALCurrentVersionCode = versionsTV.SelectedNode.Name
            Version_Label.Caption = versionsTV.SelectedNode.Text
            Version_label_Sub_Ribbon.Text = versionsTV.SelectedNode.Text
            GLOBALCurrentRatesVersionCode = rates_versionTV.SelectedNode.Name
            Rates_Version_Label.Caption = rates_versionTV.SelectedNode.Text
            VIEWOBJECT.ClearAndClose()
        End If

    End Sub

#End Region


#Region "Events"


    Private Sub VersionsTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)

        If Not rates_versionTV.SelectedNode Is Nothing Then
            SetSelectedVersion()
        Else
            rates_versionTV.Select()
        End If

    End Sub

    Private Sub VersionsTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Return) Then
            If Not rates_versionTV.SelectedNode Is Nothing Then
                SetSelectedVersion()
            Else
                rates_versionTV.Select()
            End If
        End If

    End Sub

    Friend Overridable Sub Rates_VersionsTV_DoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)

        If Not versionsTV.SelectedNode Is Nothing Then
            SetSelectedVersion()
        Else
            versionsTV.Select()
        End If

    End Sub

    Friend Overridable Sub Rates_VersionsTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Return) Then
            If Not versionsTV.SelectedNode Is Nothing Then
                SetSelectedVersion()
            Else
                versionsTV.Select()
            End If
        End If

    End Sub


#End Region




End Class
