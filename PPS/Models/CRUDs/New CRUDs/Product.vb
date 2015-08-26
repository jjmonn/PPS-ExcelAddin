Imports System.Collections
Imports System.Collections.Generic


' Product2.vb
'
' CRUD for products table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/07/2015
' Last modified: 26/08/2015



Friend Class Product

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean = False
    Friend products_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event ProductCreationEvent(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event ProductUpdateEvent(ByRef status As Boolean, ByRef ht As Hashtable)
    Public Event ProductDeleteEvent(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_PRODUCT_ANSWER, AddressOf SMSG_READ_PRODUCT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_PRODUCT_ANSWER, AddressOf SMSG_DELETE_PRODUCT_ANSWER)
        LoadProductsTable()

    End Sub

    Friend Sub LoadProductsTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_PRODUCT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetProductHTFromPacket(packet, tmp_ht)
                products_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_PRODUCT_ANSWER)

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_PRODUCT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_PRODUCT_ANSWER, AddressOf SMSG_CREATE_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCT, UShort))
        WriteProductPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MsgBox(packet.ReadString())
            Dim tmp_ht As New Hashtable
            GetProductHTFromPacket(packet, tmp_ht)
            RaiseEvent ProductCreationEvent(True, tmp_ht)
        Else
            RaiseEvent ProductCreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_PRODUCT_ANSWER, AddressOf SMSG_CREATE_PRODUCT_ANSWER)

    End Sub

    Friend Sub CMSG_READ_PRODUCT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetProductHTFromPacket(packet, ht)
            products_hash(ht(ID_VARIABLE)) = ht
        Else
        End If

    End Sub

    Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

        ' to be implemented !!!! priority normal

        request_id.Clear()
        For Each update As Object() In updates


        Next


    End Sub

    Friend Sub CMSG_UPDATE_PRODUCT(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = products_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_PRODUCT_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_PRODUCT, UShort))
        WriteProductPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_PRODUCT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_PRODUCT_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_PRODUCT, UShort))
        WriteProductPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetProductHTFromPacket(packet, ht)
            RaiseEvent ProductUpdateEvent(True, ht)
        Else
            RaiseEvent ProductUpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_PRODUCT_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_PRODUCT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_PRODUCT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            products_hash.Remove(id)
            RaiseEvent ProductDeleteEvent(True, id)
        Else
            RaiseEvent ProductDeleteEvent(False, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetProductsNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In products_hash.Keys
            tmp_list.Add(products_hash(id)(NAME_VARIABLE))
        Next
        Return tmp_list

    End Function

    Friend Function GetProductsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In products_hash.Keys
            tmpHT(products_hash(id)(Key)) = products_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetProductHTFromPacket(ByRef packet As ByteBuffer, ByRef product_ht As Hashtable)

        product_ht(ID_VARIABLE) = packet.ReadUint32()
        product_ht(NAME_VARIABLE) = packet.ReadString()

    End Sub

    Private Sub WriteProductPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub


    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        If GetProductsNameList(NAME_VARIABLE).Contains(name) Then Return False
        Return True

    End Function

#End Region


#Region "TV Loading"

    Friend Sub LoadproductsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        TV.Nodes.Clear()
        For Each id As Int32 In products_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, products_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Friend Sub LoadproductsTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As Int32 In products_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, products_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_PRODUCT_ANSWER, AddressOf SMSG_READ_PRODUCT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_PRODUCT_ANSWER, AddressOf SMSG_DELETE_PRODUCT_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
