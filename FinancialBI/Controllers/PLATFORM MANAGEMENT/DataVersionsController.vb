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
Imports CRUD

Friend Class DataVersionsController

#Region "Instance Variables"

    'Objects
    Private m_view As VersionsControl
    Private m_newVersionUI As NewDataVersionUI
    Private m_platformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private m_versionsTV As New VIBlend.WinForms.Controls.vTreeView
    Friend m_positionsDictionary As New SafeDictionary(Of Int32, Int32)
    Private m_isClosing As Boolean = False

    Private m_deletedVersionId As Int32 = 0

    ' Constants
    Private FORBIDEN_CHARS As String() = {","}


#End Region

#Region "Initialize"

    Friend Sub New()

        GlobalVariables.Versions.LoadVersionsTV(m_versionsTV)
        VTreeViewUtil.InitTVFormat(m_versionsTV)
        m_view = New VersionsControl(Me, m_versionsTV)
        m_newVersionUI = New NewDataVersionUI(Me)
        m_positionsDictionary = VTreeViewUtil.GeneratePositionsDictionary(m_versionsTV)

        AddHandler GlobalVariables.Versions.CreationEvent, AddressOf AfterCreate
        AddHandler GlobalVariables.Versions.Read, AddressOf AfterRead
        AddHandler GlobalVariables.Versions.UpdateEvent, AddressOf AfterUpdate
        AddHandler GlobalVariables.Versions.CopyEvent, AddressOf AfterCopy
        AddHandler GlobalVariables.Versions.DeleteEvent, AddressOf AfterDelete

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.m_platformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        m_isClosing = True
        SendNewPositionsToModel()
        m_view.Dispose()
        m_newVersionUI.Dispose()

    End Sub

#End Region

#Region "Event"

    Private Sub AfterRead(ByRef status As ErrorMessage, ByRef p_version As Version)

        If status = ErrorMessage.SUCCESS _
        AndAlso m_isClosing = False Then
            m_view.AfterRead(p_version)
            m_newVersionUI.AfterRead(p_version)
        End If

    End Sub

    Private Sub AfterCreate(ByRef status As ErrorMessage, ByRef id As Int32)

        Dim message As String = Local.GetValue("facts_versions.msg_error_create") & ": "

        Select Case status
            Case ErrorMessage.SUCCESS
                Exit Sub
            Case ErrorMessage.INVALID_ATTRIBUTE
                message = Local.GetValue("facts_versions.msg_invalid_attribute")
            Case ErrorMessage.SYSTEM
                message = Local.GetValue("facts_versions.msg_system")
            Case Else
                message = Local.GetValue("facts_versions.msg_unknown")
        End Select

        MsgBox(message)

    End Sub

    Private Sub AfterDelete(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status = ErrorMessage.SUCCESS _
         AndAlso m_isClosing = False Then
            m_view.AfterDelete(id)
            m_newVersionUI.AfterDelete(id)
        End If

        Dim message As String = Local.GetValue("facts_versions.msg_error_delete") & ": "

        Select Case status
            Case ErrorMessage.SUCCESS
                Exit Sub
            Case ErrorMessage.NOT_FOUND
                message &= Local.GetValue("facts_versions.msg_not_found")
            Case ErrorMessage.SYSTEM
                message &= Local.GetValue("facts_versions.msg_system")
            Case Else
                message &= Local.GetValue("facts_versions.msg_unknown")
        End Select

        MsgBox(message)

    End Sub

    Private Sub AfterUpdate(ByRef status As ErrorMessage, ByRef id As Int32)

        Dim message As String = Local.GetValue("facts_versions.msg_error_update") & ": "

        Select Case status
            Case ErrorMessage.SUCCESS
                Exit Sub
            Case ErrorMessage.NOT_FOUND
                message &= Local.GetValue("facts_versions.msg_not_found")
            Case Else
                message &= Local.GetValue("facts_versions.msg_unknown")
        End Select

        MsgBox(message)

    End Sub

    Private Sub AfterCopy(ByRef status As ErrorMessage, ByRef id As Int32)

        Dim message As String = Local.GetValue("facts_versions.msg_error_copy") & ": "

        Select Case status
            Case ErrorMessage.SUCCESS
                Exit Sub
            Case ErrorMessage.NOT_FOUND
                message &= Local.GetValue("facts_versions.msg_not_found")
            Case ErrorMessage.SYSTEM
                message &= Local.GetValue("facts_versions.msg_system")
            Case Else
                message &= Local.GetValue("facts_versions.msg_unknown")
        End Select

        MsgBox(message)

    End Sub

#End Region

#Region "Interface"

    Friend Sub CreateVersion(ByRef p_version As Version)

        GlobalVariables.Versions.Create(p_version)
        m_newVersionUI.Hide()

    End Sub

    Friend Sub CopyVersion(ByRef origin_version_id As UInt32, _
                           ByRef p_version As Version)

        GlobalVariables.Versions.Copy(origin_version_id, p_version)
        m_newVersionUI.Hide()

    End Sub

    Friend Function GetVersion(ByVal p_id As UInt32) As Version
        Return GlobalVariables.Versions.GetValue(p_id)
    End Function

    Friend Function GetVersionCopy(ByVal p_id As UInt32) As Version
        Dim version As Version = GetVersion(p_id)
        If version Is Nothing Then Return Nothing
        Return version.Clone()
    End Function

    Friend Sub UpdateParent(ByRef version_id As String, ByRef parent_id As Object)

        Dim version As Version = GetVersionCopy(version_id)
        If version Is Nothing Then Exit Sub

        version.ParentId = parent_id
        Update(version)

    End Sub

    Friend Sub UpdateName(ByRef version_id As String, ByRef name As String)

        ' set the names check every where at the same place priority high
        Dim version As Version = GetVersionCopy(version_id)
        If version Is Nothing Then Exit Sub

        version.Name = name
        Update(version)

    End Sub

    Friend Sub UpdateRatesVersion_id(ByRef version_id As UInt32, ByRef rates_version_id As UInt32)

        Dim version As Version = GetVersionCopy(version_id)
        If version Is Nothing Then Exit Sub

        version.RateVersionId = rates_version_id
        Update(version)

    End Sub

    Friend Sub UpdateFactVersion_id(ByRef version_id As UInt32, ByRef fact_version_id As UInt32)

        Dim version As Version = GetVersionCopy(version_id)
        If version Is Nothing Then Exit Sub

        version.GlobalFactVersionId = fact_version_id
        Update(version)

    End Sub

    Private Sub Update(ByRef p_version As Version)

        GlobalVariables.Versions.Update(p_version)

    End Sub

    Friend Sub DeleteVersions(ByRef node As VIBlend.WinForms.Controls.vTreeNode)

        ' priority high 
        ' ask for confirmation (deletion of all subversions)
        GlobalVariables.Versions.Delete(node.Value)

    End Sub

    Friend Sub LockVersion(ByRef version_id As UInt32)

        Dim version As Version = GetVersion(version_id)
        If version Is Nothing Then Exit Sub

        version.Locked = True
        version.LockDate = Format(Now, "short Date")
        Update(version)

    End Sub

    Friend Sub UnlockVersion(ByRef version_id As UInt32)

        Dim version As Version = GetVersion(version_id)
        If version Is Nothing Then Exit Sub

        version.Locked = False
        version.LockDate = "NA"
        Update(version)

    End Sub

#End Region

#Region "utilities"

    Friend Function IsFolder(ByRef version_id As String) As Boolean

        Dim version As Version = GetVersion(version_id)
        If version Is Nothing Then Return False
        Return version.IsFolder

    End Function

    Friend Sub ShowVersionsMGT()

        m_view.Show()

    End Sub

    Friend Sub ShowNewVersionUI(Optional ByRef input_parent_node As VIBlend.WinForms.Controls.vTreeNode = Nothing)

        m_newVersionUI.PreFill(input_parent_node)
        m_newVersionUI.Show()

    End Sub

    Friend Function IsNameValid(ByRef name As String)

        If Not GlobalVariables.Versions.GetValue(name) Is Nothing Then Return False
        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ In FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        Return True

    End Function

    Friend Function IsRatesVersionValid(ByRef p_ratesVersionId As Int32) As Boolean

        Dim l_version As ExchangeRateVersion = GlobalVariables.RatesVersions.GetValue(p_ratesVersionId)
        If l_version Is Nothing OrElse l_version.IsFolder = True Then
            Return False
        Else
            Return True
        End If

    End Function

    Friend Function IsRatesVersionCompatibleWithPeriods(ByRef start_period As Int32, _
                                                        ByRef nb_periods As Int32, _
                                                        ByRef rates_version_id As UInt32) As Boolean

        Dim l_version As ExchangeRateVersion = GlobalVariables.RatesVersions.GetValue(rates_version_id)
        If l_version Is Nothing Then Return False

        Dim rates_version_start_period As Int32 = l_version.StartPeriod
        Dim rates_version_nb_periods As Int32 = l_version.NbPeriod
        If start_period >= rates_version_start_period AndAlso _
           nb_periods <= rates_version_nb_periods Then
            Return True
        End If
        Return False

    End Function

    Friend Function IsFactsVersionValid(ByRef p_factsVersionId As UInt32) As Boolean

        Dim version As GlobalFactVersion = GlobalVariables.GlobalFactsVersions.GetValue(p_factsVersionId)
        If version Is Nothing OrElse version.IsFolder = True Then
            Return False
        Else
            Return True
        End If

    End Function

    Friend Function IsFactVersionCompatibleWithPeriods(ByRef start_period As Int32, _
                                                        ByRef nb_periods As Int32, _
                                                        ByRef fact_version_id As UInt32) As Boolean

        Dim version As GlobalFactVersion = GlobalVariables.GlobalFactsVersions.GetValue(fact_version_id)
        If version Is Nothing Then Return False

        If start_period >= version.StartPeriod AndAlso _
           nb_periods <= version.NbPeriod Then
            Return True
        End If
        Return False

    End Function

    Friend Sub AddNode(ByRef id As String, _
                        ByRef name As String, _
                        ByRef is_folder As Int32, _
                        Optional ByRef parent_node As TreeNode = Nothing)

        If parent_node Is Nothing Then
            VTreeViewUtil.AddNode(id, name, m_versionsTV, is_folder)
        Else
            VTreeViewUtil.AddNode(id, name, parent_node, is_folder)
        End If

    End Sub

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim listversions As New List(Of CRUDEntity)
        m_positionsDictionary = VTreeViewUtil.GeneratePositionsDictionary(m_versionsTV)
        For Each version_id As UInt32 In m_positionsDictionary.Keys
            position = m_positionsDictionary(version_id)
            Dim l_version1 As Version = GetVersion(version_id)
            If l_version1 Is Nothing Then Continue For
            If position <> l_version1.ItemPosition Then
                Dim l_version = GetVersionCopy(version_id)
                If l_version IsNot Nothing Then
                    l_version.ItemPosition = position
                    listversions.Add(l_version)
                End If
            End If
        Next
        GlobalVariables.Versions.UpdateList(listversions)

    End Sub

    Friend Function GetRatesVersionNameFromId(ByRef rateVersionId As UInt32) As String

        Return GlobalVariables.RatesVersions.GetValueName(rateVersionId)

    End Function

    Friend Function GetFactVersionNameFromId(ByRef p_factVersionId As UInt32) As String

        Return GlobalVariables.GlobalFactsVersions.GetValueName(p_factVersionId)

    End Function

#End Region

End Class
