' Account2.vb
'
' CRUD for accounts table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 16/07/2015
' Last modified: 10/09/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Friend Class AccountManager

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Private m_accountList As New MultiIndexDictionary(Of UInt32, String, Account)

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Account)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As ErrorMessage, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)
    Public Event UpdateListEvent(ByRef status As Boolean, ByRef updateResults As List(Of Tuple(Of Byte, Boolean, String)))

#End Region

#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ACCOUNT_ANSWER, AddressOf SMSG_READ_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)

        state_flag = False

    End Sub

#End Region

#Region "CRUD"

    Friend Sub CMSG_LIST_ACCOUNT()
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_ACCOUNT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_LIST_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim nb_accounts = packet.ReadInt32()
            For i As Int32 = 1 To nb_accounts
                Dim tmp_account = Account.BuildAccount(packet)

                m_accountList.Set(tmp_account.Id, tmp_account.Name, tmp_account)
            Next

            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub


    Friend Sub CMSG_CREATE_ACCOUNT(ByRef p_account As Account)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ACCOUNT, UShort))
        p_account.Dump(packet, False)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent CreationEvent(True, CInt(packet.ReadUint32()))
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_ACCOUNT_ANSWER)

    End Sub

    Private Sub SMSG_READ_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim l_account = Account.BuildAccount(packet)
            m_accountList.Set(l_account.Id, l_account.Name, l_account)
            RaiseEvent Read(True, l_account)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_ACCOUNT(ByRef p_account As Account)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ACCOUNT, UShort))
        p_account.Dump(packet, True)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            RaiseEvent UpdateEvent(packet.GetError(), CInt(packet.ReadUint32()))
        Else
            RaiseEvent UpdateEvent(packet.GetError(), Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_ANSWER)

    End Sub

    Friend Sub CMSG_CRUD_ACCOUNT_LIST(ByRef p_operations As Dictionary(Of Int32, CRUDAction))

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_LIST_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ACCOUNT_LIST, UShort))
        packet.WriteUint32(p_operations.Count)

        For Each op In p_operations
            Dim l_account = GetAccount(op.Key)

            If l_account Is Nothing Then Continue For
            packet.WriteUint8(op.Value)
            If op.Value = CRUDAction.DELETE Then packet.WriteInt32(op.Key) Else l_account.Dump(packet, True)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub SMSG_UPDATE_ACCOUNT_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim updatesStatus As New List(Of Tuple(Of Byte, Boolean, String))
            For i As Int32 = 0 To packet.ReadUint32()
                Dim action As CRUDAction = packet.ReadUint8()

                If (action = CRUDAction.DELETE Or action = CRUDAction.UPDATE) Then packet.ReadInt32() ' ignore id
                updatesStatus.Add(New Tuple(Of Byte, Boolean, String)(action, packet.ReadBool(), packet.ReadString()))
            Next
            RaiseEvent UpdateListEvent(True, updatesStatus)
        Else
            RaiseEvent UpdateListEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_LIST_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_LIST_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_ACCOUNT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ACCOUNT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            m_accountList.Remove(id)
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region

#Region "Mappings"

    Public Function GetAccount(ByVal p_id As UInt32) As Account
        Return m_accountList(p_id)
    End Function

    Public Function GetAccount(ByVal p_id As Int32) As Account
        Return m_accountList(CUInt(p_id))
    End Function

    Public Function GetAccount(ByVal p_name As String) As Account
        Return m_accountList(p_name)
    End Function

    Friend Function GetAccountsList(ByRef LookupOption As String) As List(Of Account)

        Dim tmp_list As New List(Of Account)
        Dim selection As New List(Of Account.FormulaTypes)
        Select Case LookupOption
            Case GlobalEnums.AccountsLookupOptions.LOOKUP_ALL
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.FORMULA)
                selection.Add(Account.FormulaTypes.HARD_VALUE_INPUT)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.HARD_VALUE_INPUT)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.FORMULA)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_TITLES
                selection.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(Account.FormulaTypes.FORMULA)
        End Select

        For Each id In m_accountList.Keys
            Dim l_account = GetAccount(id)

            If l_account Is Nothing Then Continue For
            If selection.Contains(l_account.FormulaType) Then
                tmp_list.Add(l_account)
            End If
        Next
        Return tmp_list

    End Function

    Friend Function GetAccountsDictionary() As MultiIndexDictionary(Of UInt32, String, Account)

        Return m_accountList

    End Function

    Friend Function GetIdFromName(ByRef name As String) As Int32

        If m_accountList(name) Is Nothing Then Return 0
        Return m_accountList(name).Id

    End Function

#End Region

#Region "Utilities"

    Friend Sub LoadAccountsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, m_accountList)

    End Sub

    Friend Sub LoadAccountsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, m_accountList)

    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ACCOUNT_ANSWER, AddressOf SMSG_READ_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)
        MyBase.Finalize()

    End Sub

End Class
