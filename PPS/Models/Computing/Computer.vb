Imports System.Collections.Generic
Imports System.Collections

' Computer.vb
'
' Computing interface with c++ server
'
'
' To do:
'       - 
'
'
' Known bugs:
'       - 
'
'
' Author: Julien Monnereau Julien
' Created: 17/07/2015
' Last modified: 03/08/2015


Friend Class Computer

#Region "Intance Variables"

    ' Events
    Public Event ComputationAnswered()

    ' Variables
    Private dataMap As Collections.Hashtable
    Private versions_comp_flag As New Collections.Generic.Dictionary(Of UInt32, Boolean)
    Private requestIdVersionIdDict As New Dictionary(Of UInt32, UInt32)

    ' Constants
    Friend Const FILTERS_DECOMPOSITION_IDENTIFIER As Char = "F"
    Friend Const AXIS_DECOMPOSITION_IDENTIFIER As Char = "A"
    Friend Const FILTERS_TOKEN_SEPARATOR As Char = "#"
    Friend Const YEAR_PERIOD_IDENTIFIER As Char = "y"
    Friend Const MONTH_PERIOD_IDENTIFIER As Char = "m"


#End Region


#Region "Interface"

    ' manage request IDs !!!!! -> binded with versions
    ' à vérifier avec la méthode de réception nath !! priority high
    Friend Sub CMSG_COMPUTE_REQUEST(ByRef versions_id As Int32(), _
                                    ByRef entity_id As Int32, _
                                    ByRef currency_id As Int32, _
                                    Optional ByRef filters As Dictionary(Of Int32, List(Of Int32)) = Nothing, _
                                    Optional ByRef axis_filters As Dictionary(Of Int32, List(Of Int32)) = Nothing, _
                                    Optional ByRef hierarchy As List(Of String) = Nothing)

        dataMap = New Collections.Hashtable




        requestIdVersionIdDict.Clear()
        versions_comp_flag.Clear()
        For Each id In versions_id
            versions_comp_flag.Add(id, False)
        Next

        For Each version_id In versions_id

            NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
            Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_COMPUTE_REQUEST, UShort))
            requestIdVersionIdDict.Add(packet.AssignRequestId(), version_id)
            packet.WriteUint32(version_id)                                               ' version_id
            packet.WriteUint32(entity_id)                                                ' entity_id
            packet.WriteUint32(currency_id)                                              ' currency_id

            ' Loop through filters
            If Not filters Is Nothing Then
                packet.WriteUint32(GetTotalFiltersDictionariesValues(filters))          ' number of filters_values
                For Each Filter_id In filters.Keys
                    For Each filter_value_id In filters(Filter_id)
                        packet.WriteUint32(GlobalVariables.Filters.filters_hash(Filter_id)(FILTER_AXIS_ID_VARIABLE))              ' axis_id 
                        packet.WriteUint32(Filter_id)                                   ' filter_id
                        packet.WriteUint32(filter_value_id)                             ' filter_value_id
                    Next
                Next
            Else
                packet.WriteInt32(0)                                                    ' number of filters = 0
            End If

            ' Loop through Axis direct filters
            If Not axis_filters Is Nothing Then
                packet.WriteUint32(GetTotalFiltersDictionariesValues(axis_filters))     ' number of filters_values
                For Each axis_id In axis_filters.Keys
                    For Each axis_value_id In axis_filters(axis_id)
                        packet.WriteUint32(axis_id)                                     ' axis_id
                        packet.WriteUint32(axis_value_id)                               ' axis_value_id
                    Next
                Next
            Else
                packet.WriteInt32(0)                                                    ' number of axis filters = 0
            End If

            If Not hierarchy Is Nothing Then
                packet.WriteUint32(hierarchy.Count)                                      ' decomposition hierarchy size
                For Each item In hierarchy
                    Dim axis_id As UInt32
                    Dim isAxis As Boolean
                    Dim query_type As UInt32 = GetDecompositionQueryType(item)
                    If query_type = GlobalEnums.DecompositionQueryType.AXIS Then
                        axis_id = GetItemID(item)
                        isAxis = True
                    Else
                        axis_id = GlobalVariables.Filters.filters_hash(GetItemID(item))(FILTER_AXIS_ID_VARIABLE)
                        isAxis = False
                    End If
                    packet.WriteUint32(axis_id)                                          ' axis_id
                    packet.WriteUint8(isAxis)                                            ' is axis ?
                Next
            Else
                packet.WriteUint32(0)                                                    ' decomposition hierarchy size = 0
            End If
            packet.Release()
            NetworkManager.GetInstance().Send(packet)
        Next

    End Sub

    Friend Function GetData() As Collections.Hashtable

        Return dataMap

    End Function

#End Region


#Region "Computing Response"

    ' dataMap: [version_id][filter_id][entity_id][account_id][period]
    Private Sub SMSG_COMPUTE_RESULT(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim request_id = packet.GetRequestId()
            Dim version_id As Int32 = requestIdVersionIdDict(request_id)
            requestIdVersionIdDict.Remove(request_id)

            Dim filters_dict As New Dictionary(Of String, Int32)

            dataMap(version_id) = New Hashtable

            Dim periodsTokenDict As Dictionary(Of String, String) = GlobalVariables.Versions.GetPeriodTokensDict(version_id)

            Dim periodIdentifier As Char
            Select Case GlobalVariables.Versions.versions_hash(version_id)(VERSIONS_TIME_CONFIG_VARIABLE)
                Case GlobalEnums.TimeConfig.YEARS : periodIdentifier = YEAR_PERIOD_IDENTIFIER
                Case GlobalEnums.TimeConfig.MONTHS : periodIdentifier = MONTH_PERIOD_IDENTIFIER
            End Select

            FillResultData(packet, _
                           version_id, _
                           filters_dict, _
                           periodIdentifier, _
                           periodsTokenDict)

            versions_comp_flag(version_id) = True
            If AreAllVersionsComputed() = True Then
                NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
                RaiseEvent ComputationAnswered()
            End If
        Else
            ' server answered error
        End If

    End Sub

    Private Sub FillResultData(ByRef packet As ByteBuffer, _
                               ByRef version_id As Int32, _
                               ByRef filter_dict As Dictionary(Of String, Int32), _
                               ByRef periodIdentifier As Char, _
                               ByRef periodsTokenDict As Dictionary(Of String, String))

        Dim isAxis As Boolean
        Dim filter_code As String = ""
        Dim isFiltered As Boolean = packet.ReadBool()
        If isFiltered = True Then

            Dim axis As UInt32 = packet.ReadUint8()
            isAxis = packet.ReadBool()
            If isAxis = False Then
                filter_code = FILTERS_DECOMPOSITION_IDENTIFIER & packet.ReadUint32
            Else
                filter_code = AXIS_DECOMPOSITION_IDENTIFIER & axis
            End If
            filter_dict.Add(filter_code, 0)
        End If

        Dim nb_results As UInt32 = packet.ReadUint32
        For result_index As UInt32 = 1 To nb_results
            FillAccountData(packet, _
                            version_id, _
                            isFiltered, _
                            isAxis, _
                            filter_code, _
                            filter_dict, _
                             periodIdentifier, _
                            periodsTokenDict)
        Next

        Dim nb_result_children As UInt32 = packet.ReadUint32
        For child_result_index As UInt32 = 1 To nb_result_children
            FillResultData(packet, _
                           version_id, _
                           filter_dict, _
                           periodIdentifier, _
                           periodsTokenDict)
        Next

        If isFiltered = True Then filter_dict.Remove(filter_code)

    End Sub

    Private Sub FillAccountData(ByRef packet As ByteBuffer, _
                                ByRef version_id As Int32, _
                                ByRef isFiltered As Boolean, _
                                ByRef isAxis As Boolean, _
                                ByRef filter_code As String, _
                                ByRef filter_dict As Dictionary(Of String, Int32), _
                                ByRef periodIdentifier As Char, _
                                ByRef periodsTokenDict As Dictionary(Of String, String))

        If isFiltered = True Then
            If isAxis = True Then
                filter_dict(filter_code) = packet.ReadUint32()  ' same for us here a priori, to be checked
            Else
                filter_dict(filter_code) = packet.ReadUint32()
            End If
        Else
            filter_dict(filter_code) = 0
        End If

        Dim filter_token As String = GetFiltersToken(filter_dict)
        System.Diagnostics.Debug.Write("filter Token:" & filter_token & Chr(13))
        If dataMap(version_id).containskey(filter_token) = False Then
            dataMap(version_id)(filter_token) = New Hashtable
        End If

        Dim entity_id As Int32 = packet.ReadUint32()
        System.Diagnostics.Debug.Write("entityId:" & entity_id & Chr(13))
        Dim nb_accounts As UInt32 = packet.ReadUint32()
        ' If dataMap(version_id)(filter_token).containskey(entity_id) = False Then
        dataMap(version_id)(filter_token)(entity_id) = New Hashtable
        ' End If

        For account_index As UInt32 = 1 To nb_accounts
            Dim account_id As Int32 = packet.ReadUint32()
            dataMap(version_id)(filter_token)(entity_id)(account_id) = New Hashtable

            ' Non aggregated data
            Dim nb_periods As UInt16 = packet.ReadUint16()
            If nb_periods > 0 Then
                For period_index As UInt16 = 0 To nb_periods - 1
                    dataMap(version_id) _
                           (filter_token) _
                           (entity_id) _
                           (account_id) _
                           (periodsTokenDict(periodIdentifier & period_index)) _
                           = packet.ReadDouble()
                Next
            End If

            ' Aggreagted data
            Dim nbAggregations As UInt32 = packet.ReadUint32()
            For aggregationIndex As UInt32 = 1 To nbAggregations
                dataMap(version_id) _
                    (filter_token) _
                    (entity_id) _
                    (account_id) _
                    (periodsTokenDict(YEAR_PERIOD_IDENTIFIER & aggregationIndex)) _
                    = packet.ReadDouble()
            Next
        Next

        ' Clean filter code out after ?

        Dim nb_children_entities As UInt32 = packet.ReadUint32()
        For children_index As UInt32 = 1 To nb_children_entities
            FillAccountData(packet, _
                            version_id, _
                            isFiltered, _
                            isAxis, _
                            filter_code, _
                            filter_dict, _
                            periodIdentifier, _
                            periodsTokenDict)
        Next

    End Sub

#End Region


#Region "Utilities"

    Private Function AreAllVersionsComputed() As Boolean

        For Each flag In versions_comp_flag.Values
            If flag = False Then Return False
        Next
        Return True

    End Function

    Friend Shared Function GetDecompositionQueryType(ByRef str As String) As UInt32

        Select Case Left(str, 1)
            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER : Return GlobalEnums.DecompositionQueryType.AXIS
            Case Computer.FILTERS_DECOMPOSITION_IDENTIFIER : Return GlobalEnums.DecompositionQueryType.FILTER
        End Select

    End Function

    Friend Shared Function GetItemID(ByRef str As String) As UInt32

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
                token = token & filter_code & dict(filter_code) & FILTERS_TOKEN_SEPARATOR
            Next
            Return Left(token, Len(token) - 1)
        Else
            Return 0
        End If

    End Function

#End Region


End Class
