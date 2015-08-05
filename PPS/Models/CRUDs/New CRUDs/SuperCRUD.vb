Imports System.Collections
Imports System.Collections.Generic


' SuperCRUD.vb
'
' SUPER class for CRUD - relation with server DB tables
' 
'
'
' Author: Julien Monnereau
' Created: 05/08/2015
' Last modified: 05/08/2015


Friend Class SuperCRUD


#Region "Instance variables"

    ' Variables
    Friend serverResponseFlag As Boolean
    Friend objectHash As New Hashtable
    Private requestId As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event RowCreationEvent(ByRef ht As Hashtable)
    Public Shared Event RowRead(ByRef ht As Hashtable)
    Public Event RowUpdateEvent(ByRef ht As Hashtable)
    Public Event RowDeleteEvent(ByRef id As UInt32)

#End Region


#Region "Init"

    Friend Sub New()

        LoadTable()

    End Sub

    Friend Sub LoadTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_PRODUCT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 0 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                '         GetProductHTFromPacket(packet, tmp_ht)
                objectHash(tmp_ht(ID_VARIABLE)) = tmp_ht
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_ANSWER)
            serverResponseFlag = True
            RaiseEvent ObjectInitialized()
        Else
            serverResponseFlag = False
        End If

    End Sub

#End Region


End Class
