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


Friend Class FactsVersion


#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean = False
    Friend versions_hash As New Hashtable

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

  Friend Sub New()

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_VERSION_ANSWER, AddressOf SMSG_READ_VERSION_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_VERSION_ANSWER, AddressOf SMSG_DELETE_VERSION_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_VERSION_ANSWER, AddressOf SMSG_LIST_VERSION_ANSWER)

  End Sub

    Private Sub SMSG_LIST_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim nb_versions = packet.ReadInt32()
            For i As Int32 = 1 To nb_versions
                Dim tmp_ht As New Hashtable
                GetVersionHTFromPacket(packet, tmp_ht)
                versions_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_VERSION_ANSWER, AddressOf SMSG_CREATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_VERSION, UShort))
        WriteVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            RaiseEvent CreationEvent(True, packet.ReadUint32())
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_VERSION_ANSWER, AddressOf SMSG_CREATE_VERSION_ANSWER)

    End Sub

    Friend Sub CMSG_READ_VERSION(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_VERSION, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetVersionHTFromPacket(packet, ht)
            versions_hash(ht(ID_VARIABLE)) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_VERSION, UShort))
        WriteVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            RaiseEvent UpdateEvent(True, packet.ReadUint32())
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_VERSION_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_VERSION(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_VERSION, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadUint32
            versions_hash.Remove(CInt(id))
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetVersionsNameList(ByRef variable As Object) As List(Of String)

        Dim tmp_list As New List(Of String)
        Dim selection() As String = {}
        For Each id In versions_hash.Keys
            tmp_list.Add(versions_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Function GetVersionsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In versions_hash.Keys
            tmpHT(versions_hash(id)(Key)) = versions_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Periods Interface"

    Friend Function GetYears(ByRef versionsIdDict As Dictionary(Of Int32, String)) As List(Of Int32)

        Dim yearsList As New List(Of Int32)
        For Each versionId As Int32 In versionsIdDict.Keys
            Dim startPeriod As Int32 = versions_hash(versionId)(VERSIONS_START_PERIOD_VAR)
            Dim nbPeriods As UInt16 = versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR)
            Dim timeConfig As UInt16 = versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)

            For Each yearId As Int32 In Period.GetYearsList(startPeriod, nbPeriods, timeConfig)
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
            Dim startPeriod As Int32 = versions_hash(versionId)(VERSIONS_START_PERIOD_VAR)
            Dim nbPeriods As UInt16 = versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR)
            Dim timeConfig As UInt16 = versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)

            Select Case timeConfig
                Case GlobalEnums.TimeConfig.YEARS
                    For Each yearId As Int32 In Period.GetYearsList(startPeriod, nbPeriods, timeConfig)
                        For Each monthId As Int32 In Period.GetMonthsIdsInYear(yearId)
                            If monthsList.Contains(monthId) = False Then
                                monthsList.Add(monthId)
                            End If
                        Next
                    Next

                Case GlobalEnums.TimeConfig.MONTHS
                    For Each monthId As Int32 In Period.GetMonthsList(startPeriod, nbPeriods)
                        If monthsList.Contains(monthId) = False Then
                            monthsList.Add(monthId)
                        End If
                    Next

            End Select
        Next
        monthsList.Sort()
        Return monthsList

    End Function

    Friend Function GetPeriodsList(ByRef versionId As Int32) As Int32()

        Dim startPeriod As Int32 = versions_hash(versionId)(VERSIONS_START_PERIOD_VAR)
        Dim nbPeriods As Int16 = versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR)
        Dim timeConfig As Int16 = versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)

        Select Case timeConfig
            Case GlobalEnums.TimeConfig.YEARS
                Return Period.GetYearsList(startPeriod, nbPeriods, timeConfig)
            Case GlobalEnums.TimeConfig.MONTHS
                Return Period.GetMonthsList(startPeriod, nbPeriods)
            Case Else
                MsgBox("PPS Error N°9: Unknown Time Configuration")
                Return Nothing
        End Select

    End Function

    Friend Function GetPeriodTokensDict(ByRef versionId As Int32) As Dictionary(Of String, String)

        Dim periodsTokens As New Dictionary(Of String, String)
        Select Case versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)
            Case GlobalEnums.TimeConfig.YEARS
                Dim periodIndex As UInt16 = 0
                For Each periodId As UInt32 In GetPeriodsList(versionId)
                    periodsTokens.Add(Computer.YEAR_PERIOD_IDENTIFIER & periodIndex, Computer.YEAR_PERIOD_IDENTIFIER & periodId)
                    periodIndex += 1
                Next

            Case GlobalEnums.TimeConfig.MONTHS
                Dim monthIndex As Int32 = 0
                For Each monthId As Int32 In GetPeriodsList(versionId)
                    periodsTokens.Add(Computer.MONTH_PERIOD_IDENTIFIER & monthIndex, Computer.MONTH_PERIOD_IDENTIFIER & monthId)
                    monthIndex += 1
                Next

                Dim yearIndex As Int32 = 0
                For Each yearId As Int32 In Period.GetYearsList(versions_hash(versionId)(VERSIONS_START_PERIOD_VAR), _
                                                                 versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR), _
                                                                 versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE))
                    periodsTokens.Add(Computer.YEAR_PERIOD_IDENTIFIER & yearIndex, Computer.YEAR_PERIOD_IDENTIFIER & yearId)
                    yearIndex += 1
                Next
        End Select

        Return periodsTokens

    End Function

    Friend Function GetPeriodsDictionary(ByRef versionId As Int32) As Dictionary(Of Int32, List(Of Int32))

        Dim periodsDict As New Dictionary(Of Int32, List(Of Int32))
        Select Case versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)
            Case GlobalEnums.TimeConfig.YEARS
                ' Years only
                For Each periodId As UInt32 In GetPeriodsList(versionId)
                    periodsDict.Add(periodId, New List(Of Int32))
                Next

            Case GlobalEnums.TimeConfig.MONTHS
                ' Years
                For Each yearId As Int32 In Period.GetYearsList(versions_hash(versionId)(VERSIONS_START_PERIOD_VAR), _
                                                                versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR), _
                                                                versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE))
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

            Select Case versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)
                Case GlobalEnums.TimeConfig.YEARS
                    For Each periodId As UInt32 In GetPeriodsList(versionId)
                        If periodsDict.ContainsKey(periodId) = False Then
                            periodsDict.Add(periodId, New List(Of Int32))
                        End If
                    Next

                Case GlobalEnums.TimeConfig.MONTHS
                    ' Years
                    For Each yearId As Int32 In Period.GetYearsList(versions_hash(versionId)(VERSIONS_START_PERIOD_VAR), _
                                                                    versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR), _
                                                                    versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE))
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

    Friend Shared Sub GetVersionHTFromPacket(ByRef packet As ByteBuffer, ByRef version_ht As Hashtable)

        version_ht(ID_VARIABLE) = packet.ReadUint32()
        version_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        version_ht(NAME_VARIABLE) = packet.ReadString()
        version_ht(VERSIONS_LOCKED_VARIABLE) = packet.ReadBool()
        version_ht(VERSIONS_LOCKED_DATE_VARIABLE) = packet.ReadString()
        version_ht(IS_FOLDER_VARIABLE) = packet.ReadBool()
        version_ht(ITEMS_POSITIONS) = packet.ReadUint32()
        version_ht(VERSIONS_TIME_CONFIG_VARIABLE) = packet.ReadUint32()
        version_ht(VERSIONS_RATES_VERSION_ID_VAR) = packet.ReadUint32()
        version_ht(VERSIONS_START_PERIOD_VAR) = packet.ReadUint32()
        version_ht(VERSIONS_NB_PERIODS_VAR) = packet.ReadUint16()
        version_ht(VERSIONS_CREATION_DATE_VARIABLE) = packet.ReadString()
        version_ht(VERSIONS_GLOBAL_FACT_VERSION_ID) = packet.ReadUint32()
        If version_ht(IS_FOLDER_VARIABLE) = True Then
            version_ht(IMAGE_VARIABLE) = 1
        Else
            version_ht(IMAGE_VARIABLE) = 0
        End If


    End Sub

    Private Sub WriteVersionPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteUint8(attributes(VERSIONS_LOCKED_VARIABLE))
        packet.WriteString(attributes(VERSIONS_LOCKED_DATE_VARIABLE))
        packet.WriteUint8(attributes(IS_FOLDER_VARIABLE))
        packet.WriteUint32(attributes(ITEMS_POSITIONS))
        packet.WriteUint32(attributes(VERSIONS_TIME_CONFIG_VARIABLE))
        packet.WriteUint32(attributes(VERSIONS_RATES_VERSION_ID_VAR))
        packet.WriteUint32(attributes(VERSIONS_START_PERIOD_VAR))
        packet.WriteUint16(attributes(VERSIONS_NB_PERIODS_VAR))
        packet.WriteString(attributes(VERSIONS_CREATION_DATE_VARIABLE))
        packet.WriteUint32(attributes(VERSIONS_GLOBAL_FACT_VERSION_ID))

    End Sub

    Friend Sub LoadVersionsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, versions_hash)

    End Sub

    Friend Sub LoadVersionsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, versions_hash)

    End Sub

    Friend Function GetVersionsIDFromName(ByRef versionName As String) As UInt32

        For Each versionId In versions_hash.Keys
            If versions_hash(versionId)(NAME_VARIABLE) = versionName Then
                Return versionId
            End If
        Next
        Return 0

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


  Protected Overrides Sub finalize()

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_VERSION_ANSWER, AddressOf SMSG_LIST_VERSION_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_VERSION_ANSWER, AddressOf SMSG_READ_VERSION_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_VERSION_ANSWER, AddressOf SMSG_DELETE_VERSION_ANSWER)
    MyBase.Finalize()

  End Sub




End Class
