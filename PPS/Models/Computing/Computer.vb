Imports System.Collections.Generic
Imports System.Collections
Imports CRUD

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
' Last modified: 20/08/2015


Friend Class Computer


#Region "Instance Variables"

    ' Events
    Public Event ComputationAnswered(ByRef entityId As String, ByRef status As Boolean, ByRef requestId As Int32)

    ' Variables
    Private dataMap As Dictionary(Of String, Double)
    Private versions_comp_flag As New Collections.Generic.Dictionary(Of UInt32, Boolean)
    Private requestIdVersionIdDict As New Dictionary(Of Int32, Int32)
    Private requestIdEntityIdDict As New Dictionary(Of Int32, Int32)

    ' Computing
    Private isAxis As Boolean
    Private isFiltered As Boolean
    Private axisId As Int32
    Private versionId As Int32
    Private entityId As Int32
    Private accountId As Int32
    Private periodId As Int32
    Private filterToken As String
    Private periodIdentifier As Char
    Private periodsTokenDict As Dictionary(Of String, String)
    Private filtersDict As New Dictionary(Of String, Int32)

    ' Constants
    Friend Const FILTERS_DECOMPOSITION_IDENTIFIER As Char = "F"
    Friend Const AXIS_DECOMPOSITION_IDENTIFIER As Char = "A"
    Friend Const TOKEN_SEPARATOR As Char = "#"
    Friend Const YEAR_PERIOD_IDENTIFIER As Char = "y"
    Friend Const MONTH_PERIOD_IDENTIFIER As Char = "m"


#End Region


    ' dataMap: [version_id][filter_id][entity_id][account_id][period]
    Friend Function GetData() As Dictionary(Of String, Double)
        Return dataMap
    End Function


#Region "Computation Query Interfaces"

    ' Query
    Friend Function CMSG_COMPUTE_REQUEST(ByRef versions_id As Int32(), _
                                        ByRef entity_id As Int32, _
                                        ByRef currency_id As Int32, _
                                        Optional ByRef filters As Dictionary(Of Int32, List(Of Int32)) = Nothing, _
                                        Optional ByRef axis_filters As Dictionary(Of Int32, List(Of Int32)) = Nothing, _
                                        Optional ByRef hierarchy As List(Of String) = Nothing) As Int32

        dataMap = New Dictionary(Of String, Double)
        requestIdVersionIdDict.Clear()
        versions_comp_flag.Clear()
        For Each id In versions_id
            versions_comp_flag.Add(id, False)
        Next

        For Each version_id In versions_id

            NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
            Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_COMPUTE_REQUEST, UShort))
            Dim requestId As Int32 = packet.AssignRequestId()
            requestIdVersionIdDict.Add(requestId, version_id)
            requestIdEntityIdDict.Add(requestId, entity_id)
            packet.WriteUint32(version_id)                                               ' version_id
            packet.WriteUint32(entity_id)                                                ' entity_id
            packet.WriteUint32(currency_id)                                              ' currency_id

            ' Loop through filters
            If Not filters Is Nothing Then
                packet.WriteUint32(GetTotalFiltersDictionariesValues(filters))          ' number of filters_values
                For Each Filter_id In filters.Keys
                    For Each filter_value_id In filters(Filter_id)
                        packet.WriteUint32(GlobalVariables.Filters.GetAxisOfFilter(Filter_id))              ' axis_id 
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
                Dim axis_id As AxisType
                For Each item In hierarchy
                    Dim query_type As UInt32 = GetDecompositionQueryType(item)
                    If query_type = GlobalEnums.DecompositionQueryType.AXIS Then
                        axis_id = GetItemID(item)
                        packet.WriteInt32(axis_id)
                        packet.WriteBool(True)
                    Else
                        axis_id = GlobalVariables.Filters.GetAxisOfFilter(GetItemID(item))
                        packet.WriteInt32(axis_id)
                        packet.WriteBool(False)
                        packet.WriteUint32(GetItemID(item))
                    End If
                Next
            Else
                packet.WriteUint32(0)                                                    ' decomposition hierarchy size = 0
            End If
            packet.Release()
            NetworkManager.GetInstance().Send(packet)
            If versions_id.Length = 1 Then
                Return requestId
            End If
        Next
        Return 0

    End Function

    ' Server Answer
    Private Sub SMSG_COMPUTE_RESULT(packet As ByteBuffer)

        Try
            If packet.GetError() = ErrorMessage.SUCCESS Then
                Dim request_id As Int32 = packet.GetRequestId()
                If requestIdVersionIdDict.ContainsKey(request_id) = False Then
                    'MsgBox("The server returned an unregistered compute request id.")
                    Exit Sub
                End If
                Dim version As Version = GlobalVariables.Versions.GetValue(requestIdVersionIdDict(request_id))
                If version Is Nothing Then
                    MsgBox("Compute returned a result for an invalid version.")
                    Exit Sub
                End If
                requestIdVersionIdDict.Remove(request_id)

                filtersDict.Clear()

                versionId = version.Id
                periodsTokenDict = GlobalVariables.Versions.GetPeriodTokensDict(version.Id)

                Select Case version.TimeConfiguration
                    Case CRUD.TimeConfig.YEARS : periodIdentifier = YEAR_PERIOD_IDENTIFIER
                    Case CRUD.TimeConfig.MONTHS : periodIdentifier = MONTH_PERIOD_IDENTIFIER
                End Select

                FillResultData(packet)

                versions_comp_flag(versionId) = True
                If AreAllVersionsComputed() = True Then
                    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_COMPUTE_RESULT, AddressOf SMSG_COMPUTE_RESULT)
                    RaiseEvent ComputationAnswered(requestIdEntityIdDict(request_id), packet.GetError(), request_id)
                    requestIdEntityIdDict.Remove(request_id)
                End If
            Else
                RaiseEvent ComputationAnswered(0, packet.GetError(), 0)
            End If
        Catch ex As OutOfMemoryException
            System.Diagnostics.Debug.WriteLine(ex.Message)
            MsgBox("Unable to display result: Request too complex")
            RaiseEvent ComputationAnswered(0, False, 0)
        End Try


    End Sub

#End Region


#Region "DataMap Filling"

    Private Sub FillResultData(ByRef packet As ByteBuffer)

        Dim filterCode As String = ""
        isFiltered = packet.ReadBool()
        If isFiltered = True Then
            axisId = packet.ReadUint8()
            isAxis = packet.ReadBool()
            If isAxis = True Then
                filterCode = AXIS_DECOMPOSITION_IDENTIFIER & axisId
                filtersDict(filterCode) = packet.ReadUint32
            Else
                filterCode = FILTERS_DECOMPOSITION_IDENTIFIER & packet.ReadUint32
                filtersDict(filterCode) = packet.ReadUint32
            End If

        End If

        filterToken = GetFiltersToken(filtersDict)
        System.Diagnostics.Debug.WriteLine("filter Token:" & filterToken)

        FillEntityData(packet)

        For child_result_index As Int32 = 1 To packet.ReadUint32()
            FillResultData(packet)
        Next

        If isFiltered = True Then
            filtersDict.Remove(filterCode)
        End If

    End Sub

    Private Sub FillEntityData(ByRef packet As ByteBuffer)

        entityId = packet.ReadUint32()
        System.Diagnostics.Debug.WriteLine("entityId:" & entityId)

        For account_index As Int32 = 1 To packet.ReadUint32()
            accountId = packet.ReadUint32()

            ' Non aggregated data
            For period_index As Int16 = 0 To packet.ReadUint16() - 1
                dataMap(versionId & TOKEN_SEPARATOR & _
                        filterToken & TOKEN_SEPARATOR & _
                        entityId & TOKEN_SEPARATOR & _
                        accountId & TOKEN_SEPARATOR & _
                        periodsTokenDict(periodIdentifier & period_index)) _
                        = packet.ReadDouble()
            Next

            ' Aggreagted data
            For aggregationIndex As Int32 = 0 To packet.ReadUint32() - 1
                dataMap(versionId & TOKEN_SEPARATOR & _
                        filterToken & TOKEN_SEPARATOR & _
                        entityId & TOKEN_SEPARATOR & _
                        accountId & TOKEN_SEPARATOR & _
                        periodsTokenDict(YEAR_PERIOD_IDENTIFIER & aggregationIndex)) _
                        = packet.ReadDouble()
            Next
        Next

        ' Dim nb_children_entities As UInt32 = 
        For children_index As Int32 = 1 To packet.ReadUint32()
            FillEntityData(packet)
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
