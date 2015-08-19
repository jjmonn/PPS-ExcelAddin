' CVersioningCONTROLER.vb
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
' Last modified: 03/08/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class DataVersionsController


#Region "Instance Variables"

    'Objects
    Private View As VersionsControl
    Private NewVersionUI As NewDataVersionUI
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private versionsTV As New TreeView
    Friend versionsNamesList As New List(Of String)
    Friend positions_dictionary As New Dictionary(Of String, Double)
    Friend rates_versions_id_name_dic As Dictionary(Of String, String)
    Friend rates_versions_name_id_dic As Dictionary(Of String, String)

    Private tmpVersionName As String = ""

    ' Constants
    Private FORBIDEN_CHARS As String() = {","}


#End Region


#Region "Initialize"

    Friend Sub New()

        GlobalVariables.Versions.LoadVersionsTV(versionsTV)
        rates_versions_id_name_dic = RateVersionsMapping.GetRatesVersionDictionary(RATES_VERSIONS_ID_VARIABLE, NAME_VARIABLE)
        rates_versions_name_id_dic = RateVersionsMapping.GetRatesVersionDictionary(NAME_VARIABLE, RATES_VERSIONS_ID_VARIABLE)
        View = New VersionsControl(Me, versionsTV)
        versionsNamesList = TreeViewsUtilities.GetNodesTextsList(versionsTV)
        NewVersionUI = New NewDataVersionUI(Me)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(versionsTV)

        AddHandler GlobalVariables.Versions.VersionCreationEvent, AddressOf AfterCreate
        AddHandler GlobalVariables.Versions.VersionUpdateEvent, AddressOf AfterUpdate
        AddHandler GlobalVariables.Versions.VersionDeleteEvent, AddressOf AfterDelete

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.closeControl()

    End Sub

    Protected Friend Sub sendCloseOrder()

        View.Dispose()
        PlatformMGTUI.displayControl()

    End Sub


#End Region


#Region "Interface"

    Friend Sub CreateVersion(ByRef hash As Hashtable, _
                             Optional ByRef parent_node As TreeNode = Nothing, _
                             Optional ByRef origin_version_id As String = "")

        ' quid ratesversion id 
        ' 

        hash.Add(ITEMS_POSITIONS, 1)

        GlobalVariables.Versions.CMSG_CREATE_VERSION(hash)
        NewVersionUI.Hide()

    End Sub

    Friend Sub CreateFolder(ByRef folder_name As String, _
                            Optional parent_node As TreeNode = Nothing)

        Dim hash As New Hashtable
        hash.Add(IS_FOLDER_VARIABLE, 1)
        hash.Add(ITEMS_POSITIONS, 1)
        If Not parent_node Is Nothing Then hash.Add(PARENT_ID_VARIABLE, parent_node.Name)

        GlobalVariables.Versions.CMSG_CREATE_VERSION(hash)
       
    End Sub

    Private Sub AfterCreate(ByRef ht As Hashtable)

        Dim parentNode As TreeNode = Nothing
        If ht(PARENT_ID_VARIABLE) <> 0 Then
            parentNode = versionsTV.Nodes.Find(ht(PARENT_ID_VARIABLE), True)(0)
        End If
        AddNode(ht(ID_VARIABLE), ht(NAME_VARIABLE), 0, parentNode)


    End Sub

    Friend Sub UpdateParent(ByRef version_id As String, ByRef parent_id As Object)

        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, PARENT_ID_VARIABLE, parent_id)

    End Sub

    Friend Sub UpdateName(ByRef version_id As String, ByRef name As String)

        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, NAME_VARIABLE, name)

    End Sub

    Friend Sub UpdateRatesVersion_id(ByRef version_id As UInt32, ByRef rates_version_id As UInt32)

        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, VERSIONS_RATES_VERSION_ID_VAR, rates_version_id)

    End Sub

    Private Sub AfterUpdate(ByRef ht As Hashtable)

        ' to be implemented
        ' priority normal

    End Sub

    Friend Sub DeleteVersions(ByRef node As TreeNode)

        Dim versions_to_delete = TreeViewsUtilities.GetNodesKeysList(node)
        versions_to_delete.Reverse()
        For Each version_id In versions_to_delete
            DeleteVersion(version_id)
        Next

    End Sub

    Private Sub DeleteVersion(ByRef version_id As String)

        tmpVersionName = GlobalVariables.Versions.versions_hash(version_id)(NAME_VARIABLE)
        GlobalVariables.Versions.CMSG_DELETE_VERSION(version_id)

    End Sub

    
    Private Sub AfterDelete(ByRef id As UInt32)

        If versionsNamesList.Contains(tmpVersionName) Then
            versionsNamesList.Remove(tmpVersionName)
        End If
        Dim nodes() As TreeNode = versionsTV.Nodes.Find(id, True)
        If nodes.Length > 0 Then
            nodes(0).Remove()
        End If

    End Sub

    Friend Sub LockVersion(ByRef version_id As UInt32)

        ' lock version ? 
        ' priority normal => nath server
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, VERSIONS_LOCKED_VARIABLE, 1)
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, VERSIONS_LOCKED_DATE_VARIABLE, Format(Now, "short Date"))

    End Sub

    Friend Sub UnlockVersion(ByRef version_id As UInt32)

        ' lock version ? 
        ' priority normal => nath server
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, VERSIONS_LOCKED_VARIABLE, 0)
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(version_id, VERSIONS_LOCKED_DATE_VARIABLE, "NA")

    End Sub

#End Region


#Region "utilities"

    Friend Function IsFolder(ByRef version_id As String) As Boolean

        If GlobalVariables.Versions.versions_hash(version_id)(IS_FOLDER_VARIABLE) = 1 Then Return True
        Return False

    End Function

    Friend Sub ShowVersionsMGT()

        View.Show()

    End Sub

    Friend Sub ShowNewVersionUI(Optional ByRef input_parent_node As TreeNode = Nothing)

        NewVersionUI.PreFill(input_parent_node)
        NewVersionUI.Show()
        NewVersionUI.HideVersionsTV()

    End Sub

    Friend Function IsNameValid(ByRef name As String)

        If versionsNamesList.Contains(name) Then Return False
        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ In FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        Return True

    End Function

    Friend Function IsRatesVersionValid(ByRef start_period As Int32, _
                                                 ByRef nb_periods As Int32, _
                                                 ByRef rates_version_id As String) As Boolean

        Dim RatesVersions As New RateVersion
        Dim rates_version_start_period As Int32 = RatesVersions.ReadVersion(rates_version_id, RATES_VERSIONS_START_PERIOD_VAR)
        Dim rates_version_nb_periods As Int32 = RatesVersions.ReadVersion(rates_version_id, RATES_VERSIONS_NB_PERIODS_VAR)
        If start_period >= rates_version_start_period AndAlso _
           nb_periods <= rates_version_nb_periods Then
            Return True
        End If
        Return False

    End Function

    Friend Sub AddNode(ByRef id As String, _
                                ByRef name As String, _
                                ByRef is_folder As Int32, _
                                Optional ByRef parent_node As TreeNode = Nothing)

        If parent_node Is Nothing Then
            versionsTV.Nodes.Add(id, name, is_folder, is_folder)
        Else
            parent_node.Nodes.Add(id, name, is_folder, is_folder)
        End If
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(versionsTV)
      
    End Sub

    Friend Sub SendNewPositionsToModel()

        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(versionsTV)
        For Each category_id In positions_dictionary.Keys
            GlobalVariables.Versions.CMSG_UPDATE_VERSION(category_id, ITEMS_POSITIONS, positions_dictionary(category_id))
        Next

    End Sub


#End Region



End Class
