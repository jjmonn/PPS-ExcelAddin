' Version.vb
'
' CRUD for versions table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 29/07/2015
' Last modified: 02/09/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD


Friend Class VersionManager : Inherits NamedCRUDManager(Of NamedHierarchyCRUDEntity)

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

#Region "Periods Interface"

    Friend Function GetYears(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim yearsList As New List(Of Int32)
        For Each versionId As UInt32 In versionsIdDict.Keys
            Dim version As Version = GetValue(versionId)
            If version Is Nothing Then Continue For

            For Each yearId As Int32 In Period.GetYearsList(version.StartPeriod, version.NbPeriod, version.TimeConfiguration)
                If yearsList.Contains(yearId) = False Then
                    yearsList.Add(yearId)
                End If
            Next
        Next
        yearsList.Sort()
        Return yearsList

    End Function

    ' reimplement get months / like get years
    Friend Function GetMonths(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim monthsList As New List(Of Int32)

        For Each versionId As Int32 In versionsIdDict.Keys
            Dim version As Version = GetValue(versionId)
            If version Is Nothing Then Continue For

            Select Case version.TimeConfiguration
                Case CRUD.TimeConfig.YEARS
                    For Each yearId As Int32 In Period.GetYearsList(version.StartPeriod, version.NbPeriod, version.TimeConfiguration)
                        For Each monthId As Int32 In Period.GetMonthsIdsInYear(yearId, version.StartPeriod, version.NbPeriod)
                            If monthsList.Contains(monthId) = False Then
                                monthsList.Add(monthId)
                            End If
                        Next
                    Next

                Case CRUD.TimeConfig.MONTHS
                    For Each monthId As Int32 In Period.GetMonthsList(version.StartPeriod, version.NbPeriod)
                        If monthsList.Contains(monthId) = False Then
                            monthsList.Add(monthId)
                        End If
                    Next

            End Select
        Next
        monthsList.Sort()
        Return monthsList

    End Function

    Friend Function GetPeriodsList(ByRef versionId As UInt32) As Int32()

        Dim version As Version = GetValue(versionId)
        If version Is Nothing Then Return Nothing

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS
                Return Period.GetYearsList(version.StartPeriod, version.NbPeriod, version.TimeConfiguration)
            Case CRUD.TimeConfig.MONTHS
                Return Period.GetMonthsList(version.StartPeriod, version.NbPeriod)
            Case Else
                MsgBox("PPS Error N°9: Unknown Time Configuration")
                Return Nothing
        End Select

    End Function

    Friend Function GetPeriodTokensDict(ByRef versionId As UInt32) As Dictionary(Of String, String)

        Dim periodsTokens As New Dictionary(Of String, String)
        Dim version As Version = GetValue(versionId)
        If version Is Nothing Then Return periodsTokens

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS
                Dim periodIndex As UInt16 = 0
                For Each periodId As UInt32 In GetPeriodsList(versionId)
                    periodsTokens.Add(Computer.YEAR_PERIOD_IDENTIFIER & periodIndex, Computer.YEAR_PERIOD_IDENTIFIER & periodId)
                    periodIndex += 1
                Next

            Case CRUD.TimeConfig.MONTHS
                Dim monthIndex As Int32 = 0
                For Each monthId As Int32 In GetPeriodsList(versionId)
                    periodsTokens.Add(Computer.MONTH_PERIOD_IDENTIFIER & monthIndex, Computer.MONTH_PERIOD_IDENTIFIER & monthId)
                    monthIndex += 1
                Next

                Dim yearIndex As Int32 = 0
                For Each yearId As Int32 In Period.GetYearsList(version.StartPeriod, _
                                                                 version.NbPeriod, _
                                                                 version.TimeConfiguration)
                    periodsTokens.Add(Computer.YEAR_PERIOD_IDENTIFIER & yearIndex, Computer.YEAR_PERIOD_IDENTIFIER & yearId)
                    yearIndex += 1
                Next
        End Select

        Return periodsTokens

    End Function

    Friend Function GetPeriodsDictionary(ByRef versionId As UInt32) As Dictionary(Of Int32, List(Of Int32))

        Dim periodsDict As New Dictionary(Of Int32, List(Of Int32))
        Dim version As Version = GetValue(versionId)
        If version Is Nothing Then Return Nothing

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS
                ' Years only
                For Each periodId As UInt32 In GetPeriodsList(versionId)
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
                For Each monthId As Int32 In GetPeriodsList(versionId)
                    periodsDict(Period.GetYearIdFromMonthID(monthId)).Add(monthId)
                Next

        End Select
        Return periodsDict

    End Function

    Friend Function GetPeriodsDictionary(ByRef versionsIdDict As Dictionary(Of Int32, String)) As Dictionary(Of Int32, List(Of Int32))

        Dim periodsDict As New Dictionary(Of Int32, List(Of Int32))
        For Each versionId As Int32 In versionsIdDict.Keys
            Dim version As Version = GetValue(versionId)
            If version Is Nothing Then Continue For

            Select Case version.TimeConfiguration
                Case CRUD.TimeConfig.YEARS
                    For Each periodId As UInt32 In GetPeriodsList(versionId)
                        If periodsDict.ContainsKey(periodId) = False Then
                            periodsDict.Add(periodId, New List(Of Int32))
                        End If
                    Next

                Case CRUD.TimeConfig.MONTHS
                    ' Years
                    For Each yearId As Int32 In Period.GetYearsList(version.StartPeriod, _
                                                                    version.NbPeriod, _
                                                                    version.TimeConfiguration)
                        If periodsDict.ContainsKey(yearId) = False Then
                            periodsDict.Add(yearId, New List(Of Int32))
                        End If
                    Next

                    ' Months
                    For Each monthId As Int32 In GetPeriodsList(versionId)
                        Dim yearId As Int32 = Period.GetYearIdFromMonthID(monthId)
                        If periodsDict(yearId).Contains(monthId) = False Then
                            periodsDict(yearId).Add(monthId)
                        End If
                    Next

            End Select
        Next
        Return periodsDict

    End Function


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
