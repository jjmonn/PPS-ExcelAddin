Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Friend Class FModelingAccount

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend m_fmodelingAccountsHash As New Dictionary(Of Int32, Hashtable)

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As ErrorMessage, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As ErrorMessage, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As ErrorMessage, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As ErrorMessage, ByRef id As UInt32)
    Public Event UpdateListEvent(ByRef status As ErrorMessage, ByRef updateResults As Dictionary(Of Int32, Boolean))

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_READ_FMODELING_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_FMODELING_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_LIST_FMODELING_ACCOUNT_ANSWER)

        state_flag = False

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_LIST_FMODELING_ACCOUNT()
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_FMODELING_ACCOUNT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_LIST_FMODELING_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetFModelingAccountHTFromPacket(packet, tmp_ht)
                m_fmodelingAccountsHash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub


    Friend Sub CMSG_CREATE_FMODELING_ACCOUNT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_FMODELING_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_FMODELING_ACCOUNT, UShort))
        WriteFModelingAccountPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_FMODELING_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            RaiseEvent CreationEvent(packet.GetError(), CInt(packet.ReadUint32()))
        Else
            RaiseEvent CreationEvent(packet.GetError(), Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_FMODELING_ACCOUNT_ANSWER)

    End Sub

    Private Sub SMSG_READ_FMODELING_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim ht As New Hashtable
            GetFModelingAccountHTFromPacket(packet, ht)
            m_fmodelingAccountsHash(CInt(ht(ID_VARIABLE))) = ht
            RaiseEvent Read(packet.GetError(), ht)
        Else
            RaiseEvent Read(packet.GetError(), Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_FMODELING_ACCOUNT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_FMODELING_ACCOUNT, UShort))
        WriteFModelingAccountPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            RaiseEvent UpdateEvent(packet.GetError(), CInt(packet.ReadUint32()))
        Else
            RaiseEvent UpdateEvent(packet.GetError(), Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER)

    End Sub

    Friend Sub CMSG_CRUD_FMODELING_ACCOUNT_LIST(ByRef p_fmodelingAccounts As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CRUD_FMODELING_ACCOUNT_LIST_ANSWER, AddressOf SMSG_CRUD_FMODELING_ACCOUNT_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CRUD_FMODELING_ACCOUNT_LIST, UShort))

        packet.WriteInt32(p_fmodelingAccounts.Count())
        For Each attributes As Hashtable In p_fmodelingAccounts.Values
            packet.WriteUint8(CRUDAction.UPDATE)
            WriteFModelingAccountPacket(packet, attributes)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub SMSG_CRUD_FMODELING_ACCOUNT_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim resultList As New Dictionary(Of Int32, Boolean)
            Dim nbResult As Int32 = packet.ReadInt32()

            For i As Int32 = 1 To nbResult
                Dim id As Int32 = packet.ReadInt32()
                If (resultList.ContainsKey(id)) Then
                    resultList(id) = packet.ReadBool()
                Else
                    resultList.Add(id, packet.ReadBool)
                End If
                packet.ReadString()
            Next

            RaiseEvent UpdateListEvent(packet.GetError(), resultList)
        Else
            RaiseEvent UpdateListEvent(packet.GetError(), Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CRUD_FMODELING_ACCOUNT_LIST_ANSWER, AddressOf SMSG_CRUD_FMODELING_ACCOUNT_LIST_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_FMODELING_ACCOUNT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_FMODELING_ACCOUNT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_FMODELING_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            m_fmodelingAccountsHash.Remove(CInt(id))
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFModelingAccountsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In m_fmodelingAccountsHash.Keys
            tmpHT(m_fmodelingAccountsHash(id)(Key)) = m_fmodelingAccountsHash(id)(Value)
        Next
        Return tmpHT

    End Function

    Friend Function GetIdFromName(ByRef name As String) As Int32

        For Each id As Int32 In m_fmodelingAccountsHash.Keys
            If name = m_fmodelingAccountsHash(id)(NAME_VARIABLE) Then Return id
        Next
        Return 0

    End Function

#End Region


#Region "Utilities"

    Private Shared Sub GetFModelingAccountHTFromPacket(ByRef packet As ByteBuffer, ByRef account_ht As Hashtable)

        account_ht(ID_VARIABLE) = packet.ReadInt32()
        account_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        account_ht(NAME_VARIABLE) = packet.ReadString()
        account_ht(FMODELING_ACCOUNT_TYPE) = packet.ReadInt32()
        account_ht(FMODELING_ACCOUNT_FORMAT_STRING) = packet.ReadString()
        account_ht(ITEMS_POSITIONS) = packet.ReadInt32()
        account_ht(FMODELING_ACCOUNT_ACCOUNT_ID) = packet.ReadInt32()
        account_ht(FMODELING_ACCOUNT_MAPPED_ENTITY) = packet.ReadInt32()
        account_ht(FMODELING_ACCOUNT_SERIE_COLOR) = packet.ReadInt32()
        account_ht(FMODELING_ACCOUNT_SERIE_TYPE) = packet.ReadInt32()
        account_ht(FMODELING_ACCOUNT_SERIE_CHART) = packet.ReadString()

    End Sub

    Private Sub WriteFModelingAccountPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteInt32(attributes(FMODELING_ACCOUNT_TYPE))
        packet.WriteString(attributes(FMODELING_ACCOUNT_FORMAT_STRING))
        packet.WriteInt32(attributes(ITEMS_POSITIONS))
        packet.WriteInt32(attributes(FMODELING_ACCOUNT_ACCOUNT_ID))
        packet.WriteInt32(attributes(FMODELING_ACCOUNT_MAPPED_ENTITY))
        packet.WriteInt32(attributes(FMODELING_ACCOUNT_SERIE_COLOR))
        packet.WriteInt32(attributes(FMODELING_ACCOUNT_SERIE_TYPE))
        packet.WriteString(attributes(FMODELING_ACCOUNT_SERIE_CHART))

    End Sub

    Friend Sub LoadTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, m_fmodelingAccountsHash)

    End Sub

    Friend Sub LoadTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, m_fmodelingAccountsHash)

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_READ_FMODELING_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_FMODELING_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_FMODELING_ACCOUNT_ANSWER, AddressOf SMSG_LIST_FMODELING_ACCOUNT_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
