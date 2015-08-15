Imports System.Collections
Imports System.Collections.Generic


' Adjustment2.vb
'
' CRUD for adjustments table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/07/2015
' Last modified: 15/08/2015



Friend Class Adjustment

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend adjustments_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event AdjustmentCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event AdjustmentRead(ByRef attributes As Hashtable)
    Public Event AdjustmentUpdateEvent()
    Public Event AdjustmentDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        LoadAdjustmentsTable()

    End Sub

    Friend Sub LoadAdjustmentsTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_ADJUSTMENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAdjustmentHTFromPacket(packet, tmp_ht)
                adjustments_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
            server_response_flag = True
            RaiseEvent ObjectInitialized()
        Else
            server_response_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_ADJUSTMENT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ADJUSTMENT, UShort))
        WriteAdjustmentPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MsgBox(packet.ReadString())
            Dim tmp_ht As New Hashtable
            GetAdjustmentHTFromPacket(packet, tmp_ht)
            adjustments_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)
            RaiseEvent AdjustmentCreationEvent(tmp_ht)
        Else

        End If

    End Sub

    Friend Shared Sub CMSG_READ_ADJUSTMENT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ADJUSTMENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentHTFromPacket(packet, ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
            RaiseEvent AdjustmentRead(ht)
        Else

        End If

    End Sub

    Friend Sub CMSG_UPDATE_ADJUSTMENT(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = adjustments_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENT, UShort))
        WriteAdjustmentPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_ADJUSTMENT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENT, UShort))
        WriteAdjustmentPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentHTFromPacket(packet, ht)
            adjustments_hash(ht(ID_VARIABLE)) = ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)
            RaiseEvent AdjustmentUpdateEvent()
        Else

        End If

    End Sub

    Friend Sub CMSG_DELETE_ADJUSTMENT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ADJUSTMENT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ADJUSTMENT_ANSWER()

        '  If packet.ReadInt32() = 0 Then
        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
        RaiseEvent AdjustmentDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetAdjustmentsNames() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In adjustments_hash.Keys
            tmp_list.Add(NAME_VARIABLE)
        Next
        Return tmp_list

    End Function

    Friend Function GetAdjustmentsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In adjustments_hash.Keys
            tmpHT(adjustments_hash(id)(Key)) = adjustments_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetAdjustmentHTFromPacket(ByRef packet As ByteBuffer, ByRef adjustment_ht As Hashtable)

        adjustment_ht(ID_VARIABLE) = packet.ReadUint32()
        adjustment_ht(NAME_VARIABLE) = packet.ReadString()

    End Sub

    Private Sub WriteAdjustmentPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub


#End Region


#Region "TV Loading"

    Friend Sub LoadadjustmentsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        TV.Nodes.Clear()
        For Each id As Int32 In adjustments_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, adjustments_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = True
        Next

    End Sub

    Friend Sub LoadadjustmentsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As Int32 In adjustments_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, adjustments_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = True
        Next

    End Sub

#End Region

End Class
