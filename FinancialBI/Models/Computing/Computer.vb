Imports System.Collections.Generic
Imports System.Collections
Imports CRUD

' Computer.vb
'
' Computing interface with c++ server
'
' To do:
'       - 
'
' Author: Julien Monnereau Julien
' Created: 17/07/2015
' Last modified: 13/01/2016


Friend Class Computer


#Region "Instance Variables"

    ' Events
    Public Event ComputationAnswered(ByRef entityId As String, ByRef status As Boolean, ByRef requestId As Int32)

    ' Variables
    Private m_dataMap As SafeDictionary(Of String, Double)
    Private m_versionsComputationQueue As SafeDictionary(Of Int32, Dictionary(Of Int32, Boolean))
    Private m_requestIdVersionIdDict As New SafeDictionary(Of Int32, Int32)
    Private m_requestIdEntityIdDict As New SafeDictionary(Of Int32, Int32)
    Private m_requestIdPeriodsDict As New SafeDictionary(Of Int32, Int32())

    ' Computing
    Private m_isAxis As Boolean
    Private m_isFiltered As Boolean
    Private m_axisId As Int32
    '  Private m_versionId As Int32
    Private m_entityId As Int32
    Private m_accountId As Int32
    Private m_periodId As Int32
    Private m_filterToken As String
    Private m_periodIdentifier As Char
    Private m_periodsTokenDict As SafeDictionary(Of String, String)
    Private m_filtersDict As New SafeDictionary(Of String, Int32)

    ' Constants
    Friend Const FILTERS_DECOMPOSITION_IDENTIFIER As Char = "F"
    Friend Const AXIS_DECOMPOSITION_IDENTIFIER As Char = "A"
    Friend Const TOKEN_SEPARATOR As Char = "#"
    Friend Const YEAR_PERIOD_IDENTIFIER As Char = "y"
    Friend Const MONTH_PERIOD_IDENTIFIER As Char = "m"
    Friend Const WEEK_PERIOD_IDENTIFIER As Char = "w"
    Friend Const DAY_PERIOD_IDENTIFIER As Char = "d"

#End Region


    ' dataMap: [version_id][filter_id][entity_id][account_id][period]
    Friend Function GetData() As SafeDictionary(Of String, Double)
        Return m_dataMap
    End Function


#Region "Computation Query Interfaces"

    ' Query
    Friend Function CMSG_COMPUTE_REQUEST(ByRef p_versionsIds As Int32(), _
                                         ByRef p_entitiesIds As List(Of Int32), _
                                         ByRef p_process As CRUD.Account.AccountProcess, _
                                         Optional ByRef p_currencyId As Int32 = 0, _
                                         Optional ByRef p_filters As Dictionary(Of Int32, List(Of Int32)) = Nothing, _
                                         Optional ByRef p_axisFilters As Dictionary(Of Int32, List(Of Int32)) = Nothing, _
                                         Optional ByRef p_hierarchy As List(Of String) = Nothing, _
                                         Optional ByRef p_periods As Int32() = Nothing, _
                                         Optional ByRef p_axisHierarchyDecomposition As Boolean = False) As Int32

        m_dataMap = New SafeDictionary(Of String, Double)
        m_requestIdVersionIdDict.Clear()
        m_requestIdEntityIdDict.Clear()
        m_versionsComputationQueue = New SafeDictionary(Of Int32, Dictionary(Of Int32, Boolean))

        ' Initializing versions to be computed
        For Each l_versionId As Int32 In p_versionsIds
            ' Initializing entities to be computed
            Dim l_entitiesIds As New SafeDictionary(Of Int32, Boolean)
            For Each l_entityId As Int32 In p_entitiesIds
                l_entitiesIds.Add(l_entityId, False)
            Next
            m_versionsComputationQueue.Add(l_versionId, l_entitiesIds)
        Next

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
        ' Start versions computing loop
        For Each l_versionId In p_versionsIds

            ' Version setup
            Dim l_version As Version = GlobalVariables.Versions.GetValue(l_versionId)
            If l_version Is Nothing Then Continue For

            ' Periods setup
            If p_periods Is Nothing Then
                p_periods = GlobalVariables.Versions.GetPeriodsList(l_versionId)
            End If
            If p_periods.Length = 0 Then Continue For

            ' Start entities computing loop
            For Each l_entityId As Int32 In p_entitiesIds

                If p_currencyId = 0 Then
                    Dim l_entityCurrency As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(l_entityId)
                    If l_entityCurrency Is Nothing Then
                        System.Diagnostics.Debug.WriteLine("Compute: no entity currency found for entityId = " & l_entityId)
                        Exit For
                    End If
                    p_currencyId = l_entityCurrency.CurrencyId
                End If

                Dim l_packet As New ByteBuffer(CType(ClientMessage.CMSG_COMPUTE_REQUEST, UShort))

                Dim l_requestId As Int32 = l_packet.AssignRequestId()
                m_requestIdVersionIdDict.Add(l_requestId, l_versionId)
                m_requestIdEntityIdDict.Add(l_requestId, l_entityId)
                m_requestIdPeriodsDict.Add(l_requestId, p_periods)
                l_packet.WriteUint32(p_process)
                l_packet.WriteUint32(l_versionId)                                               ' version_id
                l_packet.WriteUint32(l_version.GlobalFactVersionId)                             ' global facts version id
                l_packet.WriteUint32(l_version.RateVersionId)                                   ' rates version id
                l_packet.WriteUint32(l_entityId)                                                ' entity_id
                l_packet.WriteUint32(p_currencyId)                                              ' currency_id
                l_packet.WriteUint32(p_periods(0))
                l_packet.WriteUint32(p_periods.Length)
                l_packet.WriteUint32(0) ' nb accounts
                l_packet.WriteBool(p_axisHierarchyDecomposition)                                ' axis hierarchy decomposition
                l_packet.WriteBool(True)                                                        ' entity decomposition

                ' Loop through filters
                If Not p_filters Is Nothing Then
                    l_packet.WriteUint32(GetTotalFiltersDictionariesValues(p_filters))          ' number of filters_values
                    For Each Filter_id In p_filters.Keys
                        For Each filter_value_id In p_filters(Filter_id)
                            l_packet.WriteUint32(GlobalVariables.Filters.GetAxisOfFilter(Filter_id))              ' axis_id 
                            l_packet.WriteUint32(Filter_id)                                   ' filter_id
                            l_packet.WriteUint32(filter_value_id)                             ' filter_value_id
                        Next
                    Next
                Else
                    l_packet.WriteInt32(0)                                                    ' number of filters = 0
                End If

                ' Loop through Axis direct filters
                If Not p_axisFilters Is Nothing Then
                    l_packet.WriteUint32(GetTotalFiltersDictionariesValues(p_axisFilters))     ' number of filters_values
                    For Each axis_id In p_axisFilters.Keys
                        For Each axis_value_id In p_axisFilters(axis_id)
                            l_packet.WriteUint32(axis_id)                                     ' axis_id
                            l_packet.WriteUint32(axis_value_id)                               ' axis_value_id
                        Next
                    Next
                Else
                    l_packet.WriteInt32(0)                                                    ' number of axis filters = 0
                End If

                If Not p_hierarchy Is Nothing Then
                    l_packet.WriteUint32(p_hierarchy.Count)                                      ' decomposition hierarchy size
                    Dim axis_id As AxisType
                    For Each item In p_hierarchy
                        Dim query_type As UInt32 = GetDecompositionQueryType(item)
                        If query_type = GlobalEnums.DecompositionQueryType.AXIS Then
                            axis_id = GetItemID(item)
                            l_packet.WriteInt32(axis_id)
                            l_packet.WriteBool(True)
                        Else
                            axis_id = GlobalVariables.Filters.GetAxisOfFilter(GetItemID(item))
                            l_packet.WriteInt32(axis_id)
                            l_packet.WriteBool(False)
                            l_packet.WriteUint32(GetItemID(item))
                        End If
                    Next
                Else
                    l_packet.WriteUint32(0)                                                    ' decomposition hierarchy size = 0
                End If
                l_packet.Release()
                NetworkManager.GetInstance().Send(l_packet)
                If p_versionsIds.Length = 1 AndAlso p_entitiesIds.Count = 1 Then
                    Return l_requestId
                End If
            Next
            ' End entities computing loop
        Next
        ' End versions computing loop
        Return 0

    End Function

    ' Server Answer
    Private Sub SMSG_COMPUTE_RESULT(p_packet As ByteBuffer)

        Try
            If p_packet.GetError() = ErrorMessage.SUCCESS Then
                Dim request_id As Int32 = p_packet.GetRequestId()
                If m_requestIdVersionIdDict.ContainsKey(request_id) = False Then
                    Diagnostics.Debug.WriteLine("The server returned an unregistered compute request id.")
                    Exit Sub
                End If
                Dim version As Version = GlobalVariables.Versions.GetValue(m_requestIdVersionIdDict(request_id))
                m_requestIdVersionIdDict.Remove(request_id)
                If version Is Nothing Then
                    MsgBox(Local.GetValue("CUI.msg_return_invalid_version")) ' msg_local
                    Exit Sub
                End If
                '     m_versionId = version.Id

                Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, m_requestIdEntityIdDict(request_id))
                If l_entity Is Nothing Then
                    MsgBox(Local.GetValue("CUI.msg_return_invalid_entity")) ' msg_local
                    Exit Sub
                End If

                m_filtersDict.Clear()
                m_periodsTokenDict = GlobalVariables.Versions.GetPeriodTokensDict(version.Id, m_requestIdPeriodsDict(request_id))
                m_requestIdPeriodsDict.Remove(request_id)
                Select Case version.TimeConfiguration
                    Case CRUD.TimeConfig.YEARS : m_periodIdentifier = YEAR_PERIOD_IDENTIFIER
                    Case CRUD.TimeConfig.MONTHS : m_periodIdentifier = MONTH_PERIOD_IDENTIFIER
                    Case CRUD.TimeConfig.DAYS : m_periodIdentifier = DAY_PERIOD_IDENTIFIER
                End Select

                ' Fill m_dataMap
                FillResultData(p_packet, version.Id)

                ' Register computed entities and versions of the Queue
                m_versionsComputationQueue(CInt(version.Id))(CInt(l_entity.Id)) = True
                If AreAllVersionsComputed() = True Then
                    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
                    RaiseEvent ComputationAnswered(m_requestIdEntityIdDict(request_id), p_packet.GetError(), request_id)
                    m_requestIdEntityIdDict.Remove(request_id)
                End If
            Else
                Select Case (CType(p_packet.GetError(), ErrorMessage))
                    Case ErrorMessage.SYSTEM
                        MsgBox(Local.GetValue("CUI.msg_error_system"))
                    Case ErrorMessage.PERMISSION_DENIED
                        MsgBox(Local.GetValue("CUI.msg_permission_denied"))
                End Select

                RaiseEvent ComputationAnswered(0, p_packet.GetError(), 0)
            End If


        Catch ex As OutOfMemoryException
            System.Diagnostics.Debug.WriteLine(ex.Message)
            MsgBox(Local.GetValue("CUI.msg_out_of_memory"))
            RaiseEvent ComputationAnswered(0, p_packet.GetError(), 0)
        End Try


    End Sub

#End Region


#Region "DataMap Filling"

    Private Sub FillResultData(ByRef packet As ByteBuffer, _
                               ByRef p_versionId As Int32)

        Dim filterCode As String = ""
        m_isFiltered = packet.ReadBool()
        If m_isFiltered = True Then
            m_axisId = packet.ReadUint8()
            m_isAxis = packet.ReadBool()
            If m_isAxis = True Then
                filterCode = AXIS_DECOMPOSITION_IDENTIFIER & m_axisId
                m_filtersDict(filterCode) = packet.ReadUint32
            Else
                filterCode = FILTERS_DECOMPOSITION_IDENTIFIER & packet.ReadUint32
                m_filtersDict(filterCode) = packet.ReadUint32
            End If

        End If

        m_filterToken = GetFiltersToken(m_filtersDict)
        '        System.Diagnostics.Debug.WriteLine("filter Token:" & m_filterToken)

        FillEntityData(packet, p_versionId)

        For child_result_index As Int32 = 1 To packet.ReadUint32()
            FillResultData(packet, p_versionId)
        Next

        If m_isFiltered = True Then
            m_filtersDict.Remove(filterCode)
        End If

    End Sub

    Private Sub FillEntityData(ByRef packet As ByteBuffer, _
                               ByRef p_versionId As Int32)

        Dim l_aggregationPeriodsIdentifier As Char = ""
        Select Case m_periodIdentifier
            Case MONTH_PERIOD_IDENTIFIER : l_aggregationPeriodsIdentifier = YEAR_PERIOD_IDENTIFIER
            Case DAY_PERIOD_IDENTIFIER : l_aggregationPeriodsIdentifier = WEEK_PERIOD_IDENTIFIER
        End Select

        m_entityId = packet.ReadUint32()
        ' System.Diagnostics.Debug.WriteLine("entityId:" & m_entityId)

        For account_index As Int32 = 1 To packet.ReadUint32()
            m_accountId = packet.ReadUint32()

            ' Non aggregated data
            For period_index As Int16 = 0 To packet.ReadUint16() - 1
                m_dataMap(p_versionId & TOKEN_SEPARATOR & _
                        m_filterToken & TOKEN_SEPARATOR & _
                        m_entityId & TOKEN_SEPARATOR & _
                        m_accountId & TOKEN_SEPARATOR & _
                        m_periodsTokenDict(m_periodIdentifier & period_index)) _
                        = packet.ReadDouble()
            Next

            ' Aggregated data
            For aggregationIndex As Int32 = 0 To packet.ReadUint32() - 1
                m_dataMap(p_versionId & TOKEN_SEPARATOR & _
                        m_filterToken & TOKEN_SEPARATOR & _
                        m_entityId & TOKEN_SEPARATOR & _
                        m_accountId & TOKEN_SEPARATOR & _
                        m_periodsTokenDict(l_aggregationPeriodsIdentifier & aggregationIndex)) _
                        = packet.ReadDouble()
            Next
        Next

        ' Dim nb_children_entities As UInt32 = 
        For children_index As Int32 = 1 To packet.ReadUint32()
            FillEntityData(packet, p_versionId)
        Next

    End Sub

#End Region


#Region "Utilities"

    Private Function AreAllVersionsComputed() As Boolean

        For Each l_versionId As Int32 In m_versionsComputationQueue.Keys
            For Each l_entityIdFlagPair In m_versionsComputationQueue(l_versionId)
                If l_entityIdFlagPair.Value = False Then Return False
            Next
        Next
        Return True

    End Function

    Friend Shared Function GetDecompositionQueryType(ByRef str As String) As UInt32

        Select Case Left(str, 1)
            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER : Return GlobalEnums.DecompositionQueryType.AXIS
            Case Computer.FILTERS_DECOMPOSITION_IDENTIFIER : Return GlobalEnums.DecompositionQueryType.FILTER
        End Select
        Return Nothing
    End Function

    Friend Shared Function GetItemID(ByRef str As String) As Int32

        Return CInt(Right(str, Len(str) - 1))

    End Function

    Friend Function GetTotalFiltersDictionariesValues(ByRef dict As Dictionary(Of Int32, List(Of Int32))) As Int32

        Dim counter As Int32 = 0
        For Each key In dict.Keys
            For Each value In dict(key)
                counter += 1
            Next
        Next
        Return counter

    End Function

    Friend Shared Function GetFiltersToken(ByRef dict As Dictionary(Of String, Int32)) As String

        Dim token As String = ""
        If dict.Count > 0 Then
            For Each filter_code As String In dict.Keys
                token = token & filter_code & dict(filter_code) & TOKEN_SEPARATOR
            Next
            Return Left(token, Len(token) - 1)
        Else
            Return 0
        End If

    End Function

#End Region


End Class
