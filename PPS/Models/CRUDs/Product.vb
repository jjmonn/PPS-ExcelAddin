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
' Last modified: 24/07/2015



Friend Class Product

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend products_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ProductCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event ProductRead(ByRef attributes As Hashtable)
    Public Event ProductUpdateEvent(ByRef ht As Hashtable)
    Public Event ProductDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        LoadProductsTable()
        'Dim time_stamp = Timer
        'Do
        '    If Timer - time_stamp > GlobalVariables.timeOut Then
        '        state_flag = False
        '        Exit Do
        '    End If
        'Loop While server_response_flag = True
        'state_flag = True

    End Sub

    Friend Sub LoadProductsTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_PRODUCT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_PRODUCT_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            Dim tmp_ht As New Hashtable
            GetProductHTFromPacket(packet, tmp_ht)
            products_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        Next
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_PRODUCT_ANSWER)
        server_response_flag = True

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

        MsgBox(packet.ReadString())
        Dim tmp_ht As New Hashtable
        GetProductHTFromPacket(packet, tmp_ht)
        products_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_PRODUCT_ANSWER, AddressOf SMSG_CREATE_PRODUCT_ANSWER)
        RaiseEvent ProductCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_PRODUCT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_PRODUCT_ANSWER, AddressOf SMSG_READ_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_PRODUCT_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetProductHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_PRODUCT_ANSWER, AddressOf SMSG_READ_PRODUCT_ANSWER)
        RaiseEvent ProductRead(ht)

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

        Dim ht As New Hashtable
        GetProductHTFromPacket(packet, ht)
        products_hash(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_PRODUCT_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_ANSWER)
        RaiseEvent ProductUpdateEvent(ht)

    End Sub

    Friend Sub CMSG_DELETE_PRODUCT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_PRODUCT_ANSWER, AddressOf SMSG_DELETE_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_PRODUCT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_PRODUCT_ANSWER()

        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_PRODUCT_ANSWER, AddressOf SMSG_DELETE_PRODUCT_ANSWER)
        RaiseEvent ProductDeleteEvent(id)

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

        product_ht(ID_VARIABLE) = packet.ReadInt32()
        product_ht(NAME_VARIABLE) = packet.ReadString()

    End Sub

    Private Sub WriteProductPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub

    Friend Sub LoadProductsTree(ByRef TV As Windows.Forms.TreeView)

        TV.Nodes.Clear()
        For Each id As UInt32 In products_hash.Keys
            Dim node As Windows.Forms.TreeNode = TV.Nodes.Add(CStr(id), _
                                                              products_hash(id)(NAME_VARIABLE), _
                                                              0, 0)
            node.Checked = True
        Next

    End Sub

    Friend Sub LoadProductsTree(ByRef TV As Windows.Forms.TreeView, _
                                ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As UInt32 In products_hash.Keys
            Dim node As Windows.Forms.TreeNode = TV.Nodes.Add(CStr(id), _
                                                              products_hash(id)(NAME_VARIABLE), _
                                                              0, 0)
            node.Checked = True
        Next

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



End Class
