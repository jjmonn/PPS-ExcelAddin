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
' Last modified: 17/07/2015


Friend Class Computer

#Region "Intance Variables"

    ' Events
    Public Event ComputationAnswered()

    ' Variables
    Private dataMap As Collections.Hashtable
    Private versions_comp_flag As Collections.Generic.Dictionary(Of UInt32, Boolean)
    Private request_id As UInt32

    ' Constants
    Friend Const FILTERS_DECOMPOSITION_IDENTIFIER As String = "F"
    Friend Const AXIS_DECOMPOSITION_IDENTIFIER As String = "A"
    Friend Const FILTERS_TOKEN_SEPARATOR As String = "#"


#End Region


#Region "Computing Request"

    ' manage request IDs !!!!! -> binded with versions
    ' à vérifier avec la méthode de réceptio nath !! priority high
    Friend Sub CMSG_COMPUTE_REQUEST(ByRef versions_id As UInt32(), _
                                    ByRef entity_id As UInt32, _
                                    ByRef currency_id As UInt32, _
                                    Optional ByRef filters As Dictionary(Of UInt32, List(Of UInt32)) = Nothing, _
                                    Optional ByRef axis_filters As Dictionary(Of UInt32, List(Of UInt32)) = Nothing, _
                                    Optional ByRef hierarchy As List(Of String) = Nothing)

        dataMap = New Collections.Hashtable
        versions_comp_flag.Clear()
        For Each id In versions_id
            versions_comp_flag.Add(id, False)
        Next

        For Each version_id In versions_id

            NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
            Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_COMPUTE_REQUEST, UShort))
            packet.AssignRequestId()
            packet.WriteUint32(version_id)                                               ' version_id
            packet.WriteUint32(entity_id)                                                ' entity_id
            packet.WriteUint32(currency_id)                                              ' currency_id

            ' Loop through filters
            If Not filters Is Nothing Then
                packet.WriteUint32(GetTotalFiltersDictionariesValues(filters))          ' number of filters_values
                For Each Filter_id In filters.Keys
                    For Each filter_value_id In filters(Filter_id)
                        packet.WriteUint16(Filter_id)                                   ' filter_id
                        packet.WriteUint32(GlobalVariables.Filters.filters_hash(Filter_id)(FILTER_AXIS_ID_VARIABLE))              ' axis_id 
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
            End If

            If Not hierarchy Is Nothing Then
                packet.WriteUint32(hierarchy.Count)                                      ' decomposition hierarchy size
                For Each item In hierarchy
                    packet.WriteUint32(GetDecompositionQueryType(item))                  ' axis or filter
                    packet.WriteUint32(GetItemID(item))                                  ' decomposition item
                Next
            Else
                packet.WriteUint32(0)                                                    ' decomposition hierarchy size = 0
            End If

            packet.Release()
            NetworkManager.GetInstance().Send(packet)
        Next

    End Sub

#End Region


#Region "Computing Response"

    ' dataMap: [version_id][filter_id][entity_id][account_id][period]
    Private Sub SMSG_COMPUTE_RESULT(packet As ByteBuffer)

        Dim check_request_id = packet.GetRequestId()
        Dim version_id As UInt32 '  associate version_id to resuqest_id
        Dim filters_dict As New Dictionary(Of String, UInt32)
        dataMap(version_id) = New Hashtable

        FillResultData(packet, _
                       version_id, _
                       filters_dict)

        versions_comp_flag(version_id) = True
        If AreAllVersionsComputed() = True Then
            ' NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
            ' RaiseEvent ComputationAnswered()
        End If

    End Sub

    Private Sub FillResultData(ByRef packet As ByteBuffer, _
                               ByRef version_id As UInt32, _
                               ByRef filter_dict As Dictionary(Of String, UInt32))

        Dim filter_code As String = ""
        Dim isFiltered As Boolean = packet.ReadBool()
        If isFiltered = True Then

            Dim axis As UInt32 = packet.ReadUint32()
            Dim isAxis As Boolean = packet.ReadBool()
            If isAxis Then
                filter_code = AXIS_DECOMPOSITION_IDENTIFIER & axis
            Else
                filter_code = FILTERS_DECOMPOSITION_IDENTIFIER & packet.ReadUint32
            End If
            filter_dict.Add(filter_code, 0)
        End If

        Dim nb_results As UInt32 = packet.ReadInt32
        For result_index As UInt32 = 1 To nb_results
            FillAccountData(packet, _
                            version_id, _
                            isFiltered, _
                            filter_code, _
                            filter_dict)
        Next

        Dim nb_result_children As UInt32 = packet.ReadUint32
        For child_result_index As UInt32 = 1 To nb_result_children
            FillResultData(packet, _
                           version_id, _
                           filter_dict)
        Next

        If isFiltered = True Then filter_dict.Remove(filter_code)

    End Sub

    Private Sub FillAccountData(ByRef packet As ByteBuffer, _
                                ByRef version_id As UInt32, _
                                isFiltered As Boolean, _
                                ByRef filter_code As String, _
                                ByRef filter_dict As Dictionary(Of String, UInt32))

        If isFiltered = True Then
            filter_dict(filter_code) = packet.ReadUint32()
        Else
            filter_dict(filter_code) = 0
        End If

        Dim filter_token As String = GetFiltersToken(filter_dict)
        dataMap(version_id)(filter_token) = New Hashtable

        Dim nb_accounts As UInt32 = packet.ReadUint32()
        Dim entity_id As UInt32 = packet.ReadUint32()
        dataMap(version_id)(filter_token)(entity_id) = New Hashtable

        For account_index As UInt32 = 1 To nb_accounts
            Dim account_id As UInt32 = packet.ReadUint32()
            dataMap(version_id)(filter_token)(entity_id)(account_id) = New Hashtable

            Dim nb_periods As UInt32 = packet.ReadUint32()
            For period_index As UInt32 = 0 To nb_periods - 1
                dataMap(version_id)(filter_token)(entity_id)(account_id)(period_index) = packet.ReadDouble()
            Next
        Next

        Dim nb_children_entities As UInt32 = packet.ReadUint32()
        For children_index As UInt32 = 1 To nb_children_entities
            FillAccountData(packet, _
                            version_id, _
                            isFiltered, _
                            filter_code, _
                            filter_dict)
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

    Friend Function GetTotalFiltersDictionariesValues(ByRef dict As Dictionary(Of UInt32, List(Of UInt32))) As UInt32

        Dim counter As UInt32 = 0
        For Each key In dict.Keys
            For Each value In dict(key)
                counter += 1
            Next
        Next
        Return counter

    End Function

    Friend Shared Function GetFiltersToken(ByRef dict As Dictionary(Of String, UInt32)) As String

        Dim token As String = ""
        If dict.Count > 0 Then
            For Each filter_code As String In dict.Keys
                token = token & filter_code & dict(filter_code) & FILTERS_TOKEN_SEPARATOR
            Next
            token = Left(token, Len(token) - 1)
        End If
        Return ""

    End Function

#End Region


End Class
