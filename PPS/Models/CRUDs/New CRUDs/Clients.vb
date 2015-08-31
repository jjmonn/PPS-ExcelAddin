Imports System.Collections
Imports System.Collections.Generic


' Client.vb
'
' CRUD for clients table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 26/08/2015



Friend Class Client

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend clients_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event ClientCreationEvent(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event ClientUpdateEvent(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event ClientDeleteEvent(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CLIENT_ANSWER, AddressOf SMSG_READ_CLIENT_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CLIENT_ANSWER, AddressOf SMSG_DELETE_CLIENT_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CLIENT_ANSWER, AddressOf SMSG_LIST_CLIENT_ANSWER)

        state_flag = False

    End Sub

  Private Sub SMSG_LIST_CLIENT_ANSWER(packet As ByteBuffer)

    If packet.ReadInt32() = 0 Then
      For i As Int32 = 1 To packet.ReadInt32()
        Dim tmp_ht As New Hashtable
        GetClientHTFromPacket(packet, tmp_ht)
        clients_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
      Next
      state_flag = True
      RaiseEvent ObjectInitialized()
    Else
      state_flag = False
    End If

  End Sub

#End Region


#Region "CRUD"

  Friend Sub CMSG_CREATE_CLIENT(ByRef attributes As Hashtable)

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_CREATE_CLIENT_ANSWER)
    Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CLIENT, UShort))
    WriteClientPacket(packet, attributes)
    packet.Release()
    NetworkManager.GetInstance().Send(packet)

  End Sub

  Private Sub SMSG_CREATE_CLIENT_ANSWER(packet As ByteBuffer)

    If packet.ReadInt32() = 0 Then
      Dim tmp_ht As New Hashtable
      GetClientHTFromPacket(packet, tmp_ht)
      RaiseEvent ClientCreationEvent(True, tmp_ht)
    Else
      RaiseEvent ClientCreationEvent(False, Nothing)
    End If
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_CREATE_CLIENT_ANSWER)

  End Sub

  Friend Sub CMSG_READ_CLIENT(ByRef id As UInt32)

    Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_CLIENT, UShort))
    packet.Release()
    NetworkManager.GetInstance().Send(packet)

  End Sub

  Private Sub SMSG_READ_CLIENT_ANSWER(packet As ByteBuffer)

    If packet.ReadInt32() = 0 Then
      Dim ht As New Hashtable
      GetClientHTFromPacket(packet, ht)
      clients_hash(ht(ID_VARIABLE)) = ht
      ' qui delete !!! priority high nath server ?
    Else
    End If

  End Sub

  Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

    ' to be implemented !!!! priority normal

    request_id.Clear()
    For Each update As Object() In updates


    Next


  End Sub

  Friend Sub CMSG_UPDATE_CLIENT(ByRef id As UInt32, _
                                ByRef updated_var As String, _
                                ByRef new_value As String)

    Dim tmp_ht As Hashtable = clients_hash(id).clone ' check clone !!!!
    tmp_ht(updated_var) = new_value

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)
    Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENT, UShort))
    WriteClientPacket(packet, tmp_ht)
    packet.Release()
    NetworkManager.GetInstance().Send(packet)

  End Sub

  Friend Sub CMSG_UPDATE_CLIENT(ByRef attributes As Hashtable)

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)
    Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENT, UShort))
    WriteClientPacket(packet, attributes)
    packet.Release()
    NetworkManager.GetInstance().Send(packet)

  End Sub

  Private Sub SMSG_UPDATE_CLIENT_ANSWER(packet As ByteBuffer)

    If packet.ReadInt32() = 0 Then
      Dim ht As New Hashtable
      GetClientHTFromPacket(packet, ht)
      RaiseEvent ClientUpdateEvent(True, ht)
    Else
      RaiseEvent ClientUpdateEvent(False, Nothing)
    End If
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)

  End Sub

  Friend Sub CMSG_DELETE_CLIENT(ByRef id As UInt32)

    Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_CLIENT, UShort))
    packet.WriteUint32(id)
    packet.Release()
    NetworkManager.GetInstance().Send(packet)

  End Sub

  Private Sub SMSG_DELETE_CLIENT_ANSWER(packet As ByteBuffer)

    If packet.ReadInt32() = 0 Then
      Dim id As UInt32 = packet.ReadInt32
      clients_hash.Remove(id)
      RaiseEvent ClientDeleteEvent(True, id)
    Else
      RaiseEvent ClientDeleteEvent(False, 0)
    End If

  End Sub

#End Region


#Region "Mappings"

  Friend Function GetClientsNameList() As List(Of String)

    Dim tmp_list As New List(Of String)
    For Each id In clients_hash.Keys
      tmp_list.Add(clients_hash(id)(NAME_VARIABLE))
    Next
    Return tmp_list

  End Function

  Friend Function GetClientsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

    Dim tmpHT As New Hashtable
    For Each id In clients_hash.Keys
      tmpHT(clients_hash(id)(Key)) = clients_hash(id)(Value)
    Next
    Return tmpHT

  End Function

#End Region


#Region "Utilities"

  Friend Shared Sub GetClientHTFromPacket(ByRef packet As ByteBuffer, ByRef client_ht As Hashtable)

    client_ht(ID_VARIABLE) = packet.ReadUint32()
    client_ht(NAME_VARIABLE) = packet.ReadString()

  End Sub

  Private Sub WriteClientPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

    If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
    packet.WriteString(attributes(NAME_VARIABLE))

  End Sub

  Friend Function IsNameValid(ByRef name As String) As Boolean

    If name.Length > NAMES_MAX_LENGTH Then Return False
    For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
      If name.Contains(char_) Then Return False
    Next
    If name = "" Then Return False
    If GetClientsNameList(NAME_VARIABLE).Contains(name) Then Return False
    Return True

  End Function

#End Region


#Region "TV Loading"

  Friend Sub LoadClientsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

    TV.Nodes.Clear()
    For Each id As Int32 In clients_hash.Keys
      Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, clients_hash(id)(NAME_VARIABLE), TV, 0)
      node.Checked = Windows.Forms.CheckState.Checked
    Next

  End Sub

  Friend Sub LoadClientsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                             ByRef filter_list As List(Of UInt32))

    TV.Nodes.Clear()
    For Each id As Int32 In clients_hash.Keys
      Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, clients_hash(id)(NAME_VARIABLE), TV, 0)
      node.Checked = Windows.Forms.CheckState.Checked
    Next

  End Sub

#End Region


  Protected Overrides Sub finalize()

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CLIENT_ANSWER, AddressOf SMSG_READ_CLIENT_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CLIENT_ANSWER, AddressOf SMSG_DELETE_CLIENT_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CLIENT_ANSWER, AddressOf SMSG_LIST_CLIENT_ANSWER)
    MyBase.Finalize()

  End Sub



End Class
