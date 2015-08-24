' rate_version.vb
'
' CRUD for rate_versions table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/08/2015
' Last modified: 24/08/2015


Imports System.Collections
Imports System.Collections.Generic



Public Class RatesVersion


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean = False
    Friend rate_versions_hash As New Hashtable

    ' Events
    Public Event ObjectInitialized()
    Public Event rate_versionCreationEvent(ByRef attributes As Hashtable)
    Public Event rate_versionUpdateEvent(ByRef attributes As Hashtable)
    Public Event rate_versionDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_RATE_VERSION_ANSWER, AddressOf SMSG_READ_RATE_VERSION_ANSWER)
        LoadRateVersionsTable()

    End Sub

    Friend Sub LoadRateVersionsTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_RATE_VERSION_ANSWER, AddressOf SMSG_LIST_rate_version_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_RATE_VERSION, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_rate_version_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim nb_rate_versions = packet.ReadInt32()
            For i As Int32 = 1 To nb_rate_versions
                Dim tmp_ht As New Hashtable
                GetRateVersionHTFromPacket(packet, tmp_ht)
                rate_versions_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_RATE_VERSION_ANSWER, AddressOf SMSG_LIST_rate_version_ANSWER)
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_RATE_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_RATE_VERSION_ANSWER, AddressOf SMSG_CREATE_RATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_RATE_VERSION, UShort))
        WriteRateVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_RATE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim tmp_ht As New Hashtable
            GetRateVersionHTFromPacket(packet, tmp_ht)
            rate_versions_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_RATE_VERSION_ANSWER, AddressOf SMSG_CREATE_RATE_VERSION_ANSWER)
            RaiseEvent rate_versionCreationEvent(tmp_ht)
        Else

        End If

    End Sub

    Friend Sub CMSG_READ_RATE_VERSION(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_RATE_VERSION, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_RATE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetRateVersionHTFromPacket(packet, ht)

            ' update hash => to be applied in all CRUDs priority high
            ' + take off callback at the closing of the object
            GetRateVersionHTFromPacket(packet, ht)
            rate_versions_hash(ht(ID_VARIABLE)) = ht

        Else

        End If

    End Sub

    Friend Sub CMSG_UPDATE_RATE_VERSION(ByRef id As UInt32, _
                                        ByRef variable As String, _
                                        ByRef value As String)

        Dim attributes As Hashtable = rate_versions_hash(id)
        attributes(variable) = value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_RATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_RATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_RATE_VERSION, UShort))
        WriteRateVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_RATE_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_RATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_RATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_RATE_VERSION, UShort))
        WriteRateVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_RATE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            ' Confirmation => return rate_version
            Dim ht As New Hashtable
            '**********************************************
            ' l'update ne se fait pas ici mais dans le READ => synchro CLIENT/ SERVER FOR ALL CLIENTS
            ' priority high 
            '**********************************************

            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_RATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_RATE_VERSION_ANSWER)
            RaiseEvent rate_versionUpdateEvent(ht)
        Else

        End If

    End Sub

    Friend Sub CMSG_DELETE_RATE_VERSION(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_RATE_VERSION_ANSWER, AddressOf SMSG_DELETE_RATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_RATE_VERSION, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_RATE_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadUint32
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_RATE_VERSION_ANSWER, AddressOf SMSG_DELETE_RATE_VERSION_ANSWER)
            RaiseEvent rate_versionDeleteEvent(id)
        Else

        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetRateVersionsNameList(ByRef variable As Object) As List(Of String)

        Dim tmp_list As New List(Of String)
        Dim selection() As String = {}
        For Each id In rate_versions_hash.Keys
            tmp_list.Add(rate_versions_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Function GetRateVersionsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In rate_versions_hash.Keys
            tmpHT(rate_versions_hash(id)(Key)) = rate_versions_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetRateVersionHTFromPacket(ByRef packet As ByteBuffer, ByRef rate_version_ht As Hashtable)

        rate_version_ht(ID_VARIABLE) = packet.ReadUint32()
        rate_version_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        rate_version_ht(NAME_VARIABLE) = packet.ReadString()
        rate_version_ht(IS_FOLDER_VARIABLE) = packet.ReadBool()
        rate_version_ht(ITEMS_POSITIONS) = packet.ReadUint32()
        rate_version_ht(VERSIONS_START_PERIOD_VAR) = packet.ReadUint32()
        rate_version_ht(VERSIONS_NB_PERIODS_VAR) = packet.ReadUint16()
        If rate_version_ht(IS_FOLDER_VARIABLE) = True Then
            rate_version_ht(IMAGE_VARIABLE) = 1
        Else
            rate_version_ht(IMAGE_VARIABLE) = 0
        End If

    End Sub

    Private Sub WriteRateVersionPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteUint8(attributes(IS_FOLDER_VARIABLE))
        packet.WriteUint32(attributes(ITEMS_POSITIONS))
        packet.WriteUint32(attributes(VERSIONS_START_PERIOD_VAR))
        packet.WriteUint16(attributes(VERSIONS_NB_PERIODS_VAR))

    End Sub

    Friend Sub LoadRateVersionsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, rate_versions_hash)

    End Sub

    Friend Sub LoadRateVersionsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, rate_versions_hash)

    End Sub

    Friend Function GetRateVersionsIDFromName(ByRef rate_versionName As String) As Int32

        For Each rate_versionId In rate_versions_hash.Keys
            If rate_versions_hash(rate_versionId)(NAME_VARIABLE) = rate_versionName Then
                Return rate_versionId
            End If
        Next
        Return 0

    End Function

    Friend Function GetMonthsList(ByRef versionId As Int32) As List(Of Int32)

        Dim periodList As New List(Of Int32)
        For Each monthId As Int32 In Period.GetMonthsList(rate_versions_hash(versionId)(VERSIONS_START_PERIOD_VAR), _
                                      rate_versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR))
            periodList.Add(monthId)
        Next
        Return periodList

    End Function


#End Region


    ' below to be copied in all curezd
    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_RATE_VERSION_ANSWER, AddressOf SMSG_READ_RATE_VERSION_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
