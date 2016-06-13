' Version.vb
'
' CRUD for versions table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 29/07/2015
' Last modified: 11/12/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD


Class VersionManager : Inherits NamedCRUDManager(Of NamedHierarchyCRUDEntity)

#Region "Instance variables"
    Protected CopyCMSG As ServerMessage = ClientMessage.CMSG_COPY_VERSION
    Protected CopySMSG As ServerMessage = ServerMessage.SMSG_COPY_VERSION_ANSWER
    Public Event CopyEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_VERSION
        ReadCMSG = ClientMessage.CMSG_READ_VERSION
        UpdateCMSG = ClientMessage.CMSG_UPDATE_VERSION
        UpdateListCMSG = ClientMessage.CMSG_CRUD_VERSION_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_VERSION
        ListCMSG = ClientMessage.CMSG_LIST_VERSION

        CreateSMSG = ServerMessage.SMSG_CREATE_VERSION_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_VERSION_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_VERSION_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_VERSION_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_VERSION_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_VERSION_ANSWER

        Build = AddressOf Version.BuildVersion

        InitCallbacks()

    End Sub

    Protected Shadows Sub InitCallbacks()
        MyBase.InitCallbacks()
        NetworkManager.GetInstance().SetCallback(CopySMSG, AddressOf CopyAnswer)
    End Sub

    Protected Overrides Sub finalize()
        NetworkManager.GetInstance().RemoveCallback(CopySMSG, AddressOf CopyAnswer)
    End Sub

#End Region

#Region "CRUD"

    Public Sub Copy(p_originId As UInt32, p_new As Version)
        Dim packet As New ByteBuffer(CUShort(CopyCMSG))

        packet.WriteInt32(p_originId)
        packet.WriteString(p_new.Name)
        packet.WriteUint16(p_new.StartPeriod)
        packet.WriteUint16(p_new.NbPeriod)
        packet.WriteUint32(p_new.RateVersionId)
        packet.WriteUint32(p_new.GlobalFactVersionId)

        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub CopyAnswer(packet As ByteBuffer)
        RaiseEvent CopyEvent(packet.GetError(), packet.ReadUint32())
    End Sub

#End Region

#Region "General Periods Utilities interface"

    Friend Function GetPeriodsList(ByRef versionId As UInt32) As Int32()

        Dim version As Version = GetValue(versionId)
        If version Is Nothing Then Return Nothing

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS : Return Period.GetYearsList(version.StartPeriod, version.NbPeriod, version.TimeConfiguration)
            Case CRUD.TimeConfig.MONTHS : Return Period.GetMonthsList(version.StartPeriod, version.NbPeriod, version.TimeConfiguration)
            Case CRUD.TimeConfig.WEEK : Return Period.GetWeeksList(version.StartPeriod, version.NbPeriod, version.TimeConfiguration)
            Case CRUD.TimeConfig.DAYS : Return Period.GetDaysList(version.StartPeriod, version.NbPeriod)
            Case Else
                MsgBox("PPS Error N°9: Unknown Time Configuration")
                Return Nothing
        End Select

    End Function

#Region "Multiple versions Periods Interface"

    Friend Function GetYears(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim l_yearsList As New List(Of Int32)
        For Each l_versionId As UInt32 In versionsIdDict.Keys
            Dim l_version As Version = GetValue(l_versionId)
            If l_version Is Nothing Then Continue For

            For Each l_yearId As Int32 In Period.GetYearsList(l_version.StartPeriod, l_version.NbPeriod, l_version.TimeConfiguration)
                If l_yearsList.Contains(l_yearId) = False Then
                    l_yearsList.Add(l_yearId)
                End If
            Next
        Next
        l_yearsList.Sort()
        Return l_yearsList

    End Function

    Friend Function GetMonths(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim l_monthsList As New List(Of Int32)
        For Each l_versionId As Int32 In versionsIdDict.Keys
            Dim l_version As Version = GetValue(l_versionId)
            If l_version Is Nothing Then Continue For

            For Each l_monthId As Int32 In Period.GetMonthsList(l_version.StartPeriod, l_version.NbPeriod, l_version.TimeConfiguration)
                If l_monthsList.Contains(l_monthId) = False Then
                    l_monthsList.Add(l_monthId)
                End If
            Next
        Next
        l_monthsList.Sort()
        Return l_monthsList

    End Function

    Friend Function GetWeeks(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim l_weeksList As New List(Of Int32)
        For Each l_versionId As Int32 In versionsIdDict.Keys
            Dim l_version As Version = GetValue(l_versionId)
            If l_version Is Nothing Then Continue For

            For Each l_weekId As Int32 In Period.GetWeeksList(l_version.StartPeriod, l_version.NbPeriod, l_version.TimeConfiguration)
                If l_weeksList.Contains(l_weekId) = False Then
                    l_weeksList.Add(l_weekId)
                End If
            Next
        Next
        l_weeksList.Sort()
        Return l_weeksList

    End Function

    Friend Function GetDays(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim l_daysList As New List(Of Int32)
        For Each l_versionId As Int32 In versionsIdDict.Keys
            Dim l_version As Version = GetValue(l_versionId)
            If l_version Is Nothing Then Continue For

            If l_version.TimeConfiguration = TimeConfig.DAYS Then
                For Each l_daysId As Int32 In Period.GetDaysList(l_version.StartPeriod, l_version.NbPeriod)
                    If l_daysList.Contains(l_daysId) = False Then
                        l_daysList.Add(l_daysId)
                    End If
                Next
            End If
        Next
        l_daysList.Sort()
        Return l_daysList

    End Function

#End Region

#Region "Periods Dictionnaries"

    ' *****************************************************************************************************************************************
    ' to be reviewed -> change process -> do not use a dictionary but dynamic periods list generation (using period function fomr Period.vb)
    ' (optional)
    ' *****************************************************************************************************************************************

    Friend Function GetPeriodsDictionary(ByRef p_versionId As UInt32) As Dictionary(Of Int32, List(Of Int32))

        Dim periodsDict As New SafeDictionary(Of Int32, List(Of Int32))
        Dim version As Version = GetValue(p_versionId)
        If version Is Nothing Then Return Nothing

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS
                ' Years only
                For Each periodId As UInt32 In GetPeriodsList(p_versionId)
                    periodsDict.Add(periodId, New List(Of Int32))
                Next

            Case CRUD.TimeConfig.MONTHS
                ' Years
                For Each yearId As Int32 In Period.GetYearsList(version.StartPeriod, _
                                                                version.NbPeriod, _
                                                                version.TimeConfiguration)
                    periodsDict.Add(yearId, New List(Of Int32))
                Next

                ' Months
                For Each monthId As Int32 In GetPeriodsList(p_versionId)
                    periodsDict(Period.GetYearIdFromMonthID(monthId)).Add(monthId)
                Next

        End Select
        Return periodsDict

    End Function

    Friend Function GetPeriodsDictionary(ByRef p_versionsIdDict As Dictionary(Of Int32, String)) As Dictionary(Of Int32, List(Of Int32))

        Dim l_periodsDict As New SafeDictionary(Of Int32, List(Of Int32))
        For Each l_versionId As Int32 In p_versionsIdDict.Keys
            Dim l_version As Version = GetValue(l_versionId)
            If l_version Is Nothing Then Continue For

            Select Case l_version.TimeConfiguration
                Case CRUD.TimeConfig.YEARS
                    For Each periodId As UInt32 In GetPeriodsList(l_versionId)
                        If l_periodsDict.ContainsKey(periodId) = False Then
                            l_periodsDict.Add(periodId, New List(Of Int32))
                        End If
                    Next

                Case CRUD.TimeConfig.MONTHS
                    ' Years
                    For Each yearId As Int32 In Period.GetYearsList(l_version.StartPeriod, _
                                                                    l_version.NbPeriod, _
                                                                    l_version.TimeConfiguration)
                        If l_periodsDict.ContainsKey(yearId) = False Then
                            l_periodsDict.Add(yearId, New List(Of Int32))
                        End If
                    Next

                    ' Months
                    For Each monthId As Int32 In GetPeriodsList(l_versionId)
                        Dim yearId As Int32 = Period.GetYearIdFromMonthID(monthId)
                        If l_periodsDict(yearId).Contains(monthId) = False Then
                            l_periodsDict(yearId).Add(monthId)
                        End If
                    Next

            End Select
        Next
        Return l_periodsDict

    End Function

#End Region

#Region "Periods Tokens (for computing and CUI2)"

    Friend Function GetPeriodTokensDict(ByRef p_versionId As UInt32, _
                                        ByRef p_periods As Int32()) As Dictionary(Of String, String)

            Dim l_periodsTokensDict As New SafeDictionary(Of String, String)
            Dim l_version As Version = GetValue(p_versionId)
            If l_version Is Nothing Then Return l_periodsTokensDict
    
        Select Case l_version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS
                AddYearsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)

            Case CRUD.TimeConfig.MONTHS
                AddYearsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)
                AddMonthsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)

            Case CRUD.TimeConfig.WEEK
                AddYearsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)
                AddMonthsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)
                AddWeeksToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)

            Case CRUD.TimeConfig.DAYS
                AddYearsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)
                AddMonthsToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)
                AddWeeksToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)
                AddDaysToPeriodTokensDict(l_version, l_periodsTokensDict, p_periods)

        End Select
        Return l_periodsTokensDict

    End Function

    Private Sub AddYearsToPeriodTokensDict(ByRef p_version As Version, _
                                           ByRef p_periodsTokensDict As SafeDictionary(Of String, String), _
                                           ByVal p_periodsShortList As Int32())

        Dim l_yearIndex As Int32 = 0
        Dim l_periodsList = Period.GetYearsList(p_version.StartPeriod, p_version.NbPeriod, p_version.TimeConfiguration)
        If p_periodsShortList IsNot Nothing Then
            Dim l_periodsShortList = p_periodsShortList.ToList
            ' Include last period's Year id in short list
            Dim l_lastFilterYear As Int32 = Period.GetYearIdFromPeriodId(l_periodsShortList(l_periodsShortList.Count - 1))
            If l_periodsShortList.Contains(l_lastFilterYear) = False Then l_periodsShortList.Add(l_lastFilterYear)
            ' Filter
            l_periodsList = Period.FilterPeriodList(l_periodsList, l_periodsShortList).ToArray
        End If

        For Each l_yearId As Int32 In l_periodsList
            p_periodsTokensDict.Add(Computer.YEAR_PERIOD_IDENTIFIER & l_yearIndex, Computer.YEAR_PERIOD_IDENTIFIER & l_yearId)
            l_yearIndex += 1
        Next

    End Sub

    Private Sub AddMonthsToPeriodTokensDict(ByRef p_version As Version, _
                                            ByRef p_periodsTokensDict As SafeDictionary(Of String, String), _
                                            ByVal p_periodsShortList As Int32())
        Dim l_monthIndex As Int32 = 0
        Dim l_periodsList = Period.GetMonthsList(p_version.StartPeriod, p_version.NbPeriod, p_version.TimeConfiguration)
        If p_periodsShortList IsNot Nothing Then
            Dim l_periodsShortList = p_periodsShortList.ToList
            ' Include last period's month id in short list
            Dim l_lastFilterMonth As Int32 = Period.GetMonthIdFromPeriodId(l_periodsShortList(l_periodsShortList.Count - 1))
            If l_periodsShortList.Contains(l_lastFilterMonth) = False Then l_periodsShortList.Add(l_lastFilterMonth)
            ' Filter
            l_periodsList = Period.FilterPeriodList(l_periodsList, l_periodsShortList.ToList).ToArray
        End If

        For Each l_monthId As Int32 In l_periodsList
            p_periodsTokensDict.Add(Computer.MONTH_PERIOD_IDENTIFIER & l_monthIndex, Computer.MONTH_PERIOD_IDENTIFIER & l_monthId)
            l_monthIndex += 1
        Next
    End Sub

    Private Sub AddWeeksToPeriodTokensDict(ByRef p_version As Version, _
                                           ByRef p_periodsTokensDict As SafeDictionary(Of String, String), _
                                           ByVal p_periodsShortList As Int32())
        Dim l_weekIndex As Int32 = 0
        Dim l_periodsList = Period.GetWeeksList(p_version.StartPeriod, p_version.NbPeriod, p_version.TimeConfiguration)
        If p_periodsShortList IsNot Nothing Then
            Dim l_periodsShortList = p_periodsShortList.ToList
            ' Include last period's week id in short list
            Dim l_lastFilterWeek As Int32 = Period.GetWeekIdFromPeriodId(l_periodsShortList(l_periodsShortList.Count - 1))
            If l_periodsShortList.Contains(l_lastFilterWeek) = False Then l_periodsShortList.Add(l_lastFilterWeek)
            ' Filter
            l_periodsList = Period.FilterPeriodList(l_periodsList, l_periodsShortList.ToList).ToArray
        End If

        For Each l_weekId As Int32 In l_periodsList
            p_periodsTokensDict.Add(Computer.WEEK_PERIOD_IDENTIFIER & l_weekIndex, Computer.WEEK_PERIOD_IDENTIFIER & l_weekId)
            l_weekIndex += 1
        Next
    End Sub

    Private Sub AddDaysToPeriodTokensDict(ByRef p_version As Version, _
                                          ByRef p_periodsTokensDict As SafeDictionary(Of String, String), _
                                          ByVal p_periodsShortList As Int32())

        If p_version.TimeConfiguration = TimeConfig.DAYS Then
            Dim l_dayIndex As Int32 = 0
            Dim l_periodsList = Period.GetDaysList(p_version.StartPeriod, p_version.NbPeriod)
            If p_periodsShortList IsNot Nothing Then
                l_periodsList = Period.FilterPeriodList(l_periodsList, p_periodsShortList.ToList).ToArray
            End If

            For Each l_dayId As Int32 In l_periodsList
                p_periodsTokensDict.Add(Computer.DAY_PERIOD_IDENTIFIER & l_dayIndex, Computer.DAY_PERIOD_IDENTIFIER & l_dayId)
                l_dayIndex += 1
            Next
        End If
    End Sub


#End Region

#End Region

#Region "Utilities"

    Friend Sub LoadVersionsTV(ByRef p_treeview As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(p_treeview, m_CRUDDic)
        TreeViewsUtilities.LoadTreeviewIcons(p_treeview, m_CRUDDic)

    End Sub

    Friend Sub LoadVersionsTV(ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(p_treeview, m_CRUDDic)
        VTreeViewUtil.LoadTreeviewIcons(p_treeview, m_CRUDDic)

    End Sub

    Friend Function IsVersionValid(ByRef versionId As UInt32) As Boolean

        Dim version As Version = GetValue(versionId)
        If version Is Nothing Then Return False

        If version.IsFolder = False Then Return True
        Return False

    End Function

#End Region

#Region "Versions Comparison Utilities"

    'Friend Function IdentifyVersionsComparison(ByRef versionsIds() As UInt32) As UInt16

    '    If versionsIds.Length = 0 Then Return Nothing
    '    Dim timeconfig As UInt32 = versions_hash(versionsIds(0))(VERSIONS_TIME_CONFIG_VARIABLE)
    '    If versionsIds.Count > 1 Then
    '        For Each version_id As String In versionsIds
    '            If timeconfig <> versions_hash(version_id)(VERSIONS_TIME_CONFIG_VARIABLE) Then
    '                Return GlobalEnums.VersionComparisonConfig.Y_M_VERSIONS_COMPARISON
    '            End If
    '        Next
    '    End If

    '    Select Case timeconfig
    '        Case MONTHLY_TIME_CONFIGURATION : Return GlobalEnums.VersionComparisonConfig.M_VERSIONS_COMPARISON
    '        Case YEARLY_TIME_CONFIGURATION : Return GlobalEnums.VersionComparisonConfig.Y_VERSIONS_COMPARISON
    '        Case Else : Return Nothing
    '    End Select

    'End Function

#End Region

End Class
