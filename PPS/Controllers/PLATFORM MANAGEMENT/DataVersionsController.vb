' CVersioningCONTROLER.vb
' 
' VersionsTable/ UI interface support functions
'
' To do:
'     - 
'
'
' Known bugs:
'
' 
' Last modified: 24/08/2015
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
    Private versionsTV As New VIBlend.WinForms.Controls.vTreeView
    Friend versionsNamesList As New List(Of String)
    Friend positions_dictionary As New Dictionary(Of Int32, Int32)
    Friend rates_versions_name_id_dic As Hashtable

    Private deletedVersionId As Int32 = 0

    ' Constants
    Private FORBIDEN_CHARS As String() = {","}


#End Region


#Region "Initialize"

    Friend Sub New()

        GlobalVariables.Versions.LoadVersionsTV(versionsTV)
        rates_versions_name_id_dic = GlobalVariables.RatesVersions.GetRateVersionsDictionary(NAME_VARIABLE, ID_VARIABLE)
        View = New VersionsControl(Me, versionsTV)
        versionsNamesList = GlobalVariables.Versions.GetVersionsNameList(NAME_VARIABLE)
        NewVersionUI = New NewDataVersionUI(Me)
        positions_dictionary = VTreeViewUtil.GeneratePositionsDictionary(versionsTV)

        AddHandler GlobalVariables.Versions.CreationEvent, AddressOf AfterCreate
        AddHandler GlobalVariables.Versions.UpdateEvent, AddressOf AfterUpdate
        AddHandler GlobalVariables.Versions.DeleteEvent, AddressOf AfterDelete

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
                             Optional ByRef origin_version_id As String = "")

        ' implement copy creation 
        ' priority high
        ' quid ratesversion id priority high
        GlobalVariables.Versions.CMSG_CREATE_VERSION(hash)
        NewVersionUI.Hide()

    End Sub

    Private Sub AfterCreate(ByRef status As Boolean, ByRef id As Int32)

        ' not here -> deplaced in after update from server priority high

        'If ht(PARENT_ID_VARIABLE) <> 0 Then
        '    Dim parentNode As VIBlend.WinForms.Controls.vTreeNode = Nothing
        '    parentNode = VTreeViewUtil.FindNode(versionsTV, ht(PARENT_ID_VARIABLE))
        '    VTreeViewUtil.AddNode(ht(ID_VARIABLE), ht(NAME_VARIABLE), parentNode)
        'Else
        '    VTreeViewUtil.AddNode(ht(ID_VARIABLE), ht(NAME_VARIABLE), versionsTV)
        'End If

    End Sub

    Friend Sub UpdateParent(ByRef version_id As String, ByRef parent_id As Object)

        Update(version_id, PARENT_ID_VARIABLE, parent_id)

    End Sub

    Friend Sub UpdateName(ByRef version_id As String, ByRef name As String)

        ' set the names check every where at the same place priority high
        Update(version_id, NAME_VARIABLE, name)

    End Sub

    Friend Sub UpdateRatesVersion_id(ByRef version_id As UInt32, ByRef rates_version_id As UInt32)

        Update(version_id, VERSIONS_RATES_VERSION_ID_VAR, rates_version_id)

    End Sub

    Private Sub Update(ByRef id As Int32, _
                       ByRef variable As String, _
                       ByRef value As Object)


        Dim ht As Hashtable = GlobalVariables.Versions.versions_hash(id)
        ht(variable) = value
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(ht)

    End Sub


    Private Sub AfterUpdate(ByRef status As Boolean, ByRef id As Int32)

        ' to be implemented
        ' priority normal

    End Sub

    Friend Sub DeleteVersions(ByRef node As VIBlend.WinForms.Controls.vTreeNode)

        ' priority high 
        ' ask for confirmation (deletion of all subversions)

        deletedVersionId = node.Value
        GlobalVariables.Versions.CMSG_DELETE_VERSION(node.Value)

    End Sub

    Private Sub AfterDelete(ByRef status As Boolean, ByRef id As UInt32)

        ' ask to change current version if deleted
        ' priority high
        Dim deletedVersionName = GlobalVariables.Versions.versions_hash(deletedVersionId)(NAME_VARIABLE)
        If My.Settings.version_id = deletedVersionId Then
            ' change current version 
        End If
        If versionsNamesList.Contains(deletedVersionName) Then
            versionsNamesList.Remove(deletedVersionName)
        End If
        Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(versionsTV, id)
        If Not node Is Nothing Then
            node.Remove()
        End If

    End Sub

    Friend Sub LockVersion(ByRef version_id As UInt32)

        ' lock version ? 
        ' priority normal => nath server
        Dim ht As Hashtable = GlobalVariables.Versions.versions_hash(version_id)
        ht(VERSIONS_LOCKED_VARIABLE) = 1
        ht(VERSIONS_LOCKED_DATE_VARIABLE) = Format(Now, "short Date")
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(ht)
  
    End Sub

    Friend Sub UnlockVersion(ByRef version_id As UInt32)

        ' lock version ? 
        ' priority normal => nath server
        Dim ht As Hashtable = GlobalVariables.Versions.versions_hash(version_id)
        ht(VERSIONS_LOCKED_VARIABLE) = 0
        ht(VERSIONS_LOCKED_DATE_VARIABLE) = "NA"
        GlobalVariables.Versions.CMSG_UPDATE_VERSION(ht)

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

    Friend Sub ShowNewVersionUI(Optional ByRef input_parent_node As VIBlend.WinForms.Controls.vTreeNode = Nothing)

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

        Dim rates_version_start_period As Int32 = GlobalVariables.RatesVersions.rate_versions_hash(rates_version_id)(VERSIONS_START_PERIOD_VAR)
        Dim rates_version_nb_periods As Int32 = GlobalVariables.RatesVersions.rate_versions_hash(rates_version_id)(VERSIONS_NB_PERIODS_VAR)
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
            VTreeViewUtil.AddNode(id, name, versionsTV, is_folder)
        Else
            VTreeViewUtil.AddNode(id, name, parent_node, is_folder)
        End If
    
    End Sub

    Friend Sub SendNewPositionsToModel()

        ' Caution: to be sent only once -> transaction intensive
        ' -> + ill refresh view each time !!!!! 
        positions_dictionary = VTreeViewUtil.GeneratePositionsDictionary(versionsTV)
        For Each category_id In positions_dictionary.Keys
            Update(category_id, ITEMS_POSITIONS, positions_dictionary(category_id))
        Next

    End Sub

    Friend Function GetRatesVersionNameFromId(ByRef rateVersionId As Int32) As String

        Return GlobalVariables.RatesVersions.rate_versions_hash(rateVersionId)(NAME_VARIABLE)

    End Function

#End Region



End Class
