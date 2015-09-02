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
' Last modified: 02/09/2015



Friend Class Adjustment


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend adjustments_hash As New Hashtable
    
    ' Events
    Public Event ObjectInitialized(ByRef status As Boolean)
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)

#End Region


#Region "Init"

  Friend Sub New()

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
    state_flag = False

  End Sub

    Private Sub SMSG_LIST_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAdjustmentHTFromPacket(packet, tmp_ht)
                adjustments_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            RaiseEvent ObjectInitialized(True)
            state_flag = True
        Else
            RaiseEvent ObjectInitialized(False)
            state_flag = False
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
            RaiseEvent CreationEvent(True, packet.ReadUint32())
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)

    End Sub

    Friend Shared Sub CMSG_READ_ADJUSTMENT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ADJUSTMENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentHTFromPacket(packet, ht)
            adjustments_hash(ht(ID_VARIABLE)) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

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
            RaiseEvent UpdateEvent(True, packet.ReadUint32())
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_ADJUSTMENT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ADJUSTMENT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            adjustments_hash.Remove(id)
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If
  
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
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Friend Sub LoadadjustmentsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As Int32 In adjustments_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, adjustments_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

#End Region


  Protected Overrides Sub finalize()

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
    MyBase.Finalize()

  End Sub
    

End Class
