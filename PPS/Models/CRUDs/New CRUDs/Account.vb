' Account2.vb
'
' CRUD for accounts table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 16/07/2015
' Last modified: 02/09/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Friend Class Account

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend accounts_hash As New Dictionary(Of Int32, Hashtable)
  
    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)
    Public Event UpdateListEvent(ByRef status As Boolean, ByRef updateResults As List(Of Boolean))

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ACCOUNT_ANSWER, AddressOf SMSG_READ_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)

        state_flag = False

    End Sub

    Private Sub SMSG_LIST_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim tmpPositionsDic As New Dictionary(Of Int32, Int32)
            Dim tmpAccountsHT As New Hashtable
            Dim nb_accounts = packet.ReadInt32()
            For i As Int32 = 1 To nb_accounts
                Dim tmp_ht As New Hashtable
                GetAccountHTFromPacket(packet, tmp_ht)
                tmp_ht(IMAGE_VARIABLE) = tmp_ht(ACCOUNT_FORMULA_TYPE_VARIABLE)
                tmpAccountsHT(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
                tmpPositionsDic.Add(tmp_ht(ID_VARIABLE), tmp_ht(ITEMS_POSITIONS))
            Next
            Dim sorted = From pair In tmpPositionsDic Order By pair.Value
            Dim sortedAccountsDict = sorted.ToDictionary(Function(p) p.Key, Function(p) p.Value)

            accounts_hash.Clear()
            For Each accountId As Int32 In sortedAccountsDict.Keys
                accounts_hash.Add(accountId, tmpAccountsHT(accountId))
            Next

            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_ACCOUNT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ACCOUNT, UShort))
        WriteAccountPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            RaiseEvent CreationEvent(True, CInt(packet.ReadUint32()))
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_ACCOUNT_ANSWER)

    End Sub

    Friend Sub CMSG_READ_ACCOUNT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ACCOUNT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAccountHTFromPacket(packet, ht)
            accounts_hash(CInt(ht(ID_VARIABLE))) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_ACCOUNT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ACCOUNT, UShort))
        WriteAccountPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ACCOUNT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            RaiseEvent UpdateEvent(True, CInt(packet.ReadUint32()))
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_LIST_ACCOUNT(ByRef accountsAttributes As List(Of Hashtable))

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ACCOUNT_LIST_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ACCOUNT_LIST, UShort))
        packet.WriteUint32(accountsAttributes.Count)
        For Each ht As Hashtable In accountsAttributes
            WriteAccountPacket(packet, ht)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub SMSG_UPDATE_ACCOUNT_LIST_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim updatesStatus As New List(Of Boolean)
            For i As Int32 = 0 To packet.ReadUint32()
                updatesStatus.Add(packet.ReadBool())
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

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            accounts_hash.Remove(CInt(id))
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetAccountsList(ByRef LookupOption As String, ByRef variable As String)

        Dim tmp_list As New List(Of String)
        Dim selection As New List(Of String)
        Select Case LookupOption
            Case GlobalEnums.AccountsLookupOptions.LOOKUP_ALL
                selection.Add(GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(GlobalEnums.FormulaTypes.FORMULA)
                selection.Add(GlobalEnums.FormulaTypes.HARD_VALUE_INPUT)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS
                selection.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(GlobalEnums.FormulaTypes.HARD_VALUE_INPUT)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS
                selection.Add(GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(GlobalEnums.FormulaTypes.FORMULA)

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_TITLES
                selection.Add(GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS)
                selection.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
                selection.Add(GlobalEnums.FormulaTypes.FORMULA)
        End Select

        For Each id In accounts_hash.Keys
            If selection.Contains(accounts_hash(id)(ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                tmp_list.Add(accounts_hash(id)(variable))
            End If
        Next
        Return tmp_list

    End Function

    Friend Function GetAccountsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In accounts_hash.Keys
            tmpHT(accounts_hash(id)(Key)) = accounts_hash(id)(Value)
        Next
        Return tmpHT

    End Function

    Friend Function GetIdFromName(ByRef name As String) As Int32

        For Each id As Int32 In accounts_hash.Keys
            If name = accounts_hash(id)(NAME_VARIABLE) Then Return id
        Next
        Return 0

    End Function

#End Region


#Region "Utilities"

    Private Shared Sub GetAccountHTFromPacket(ByRef packet As ByteBuffer, ByRef account_ht As Hashtable)

        account_ht(ID_VARIABLE) = packet.ReadInt32()
        account_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        account_ht(NAME_VARIABLE) = packet.ReadString()
        account_ht(ACCOUNT_FORMULA_TYPE_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_FORMULA_VARIABLE) = packet.ReadString()
        account_ht(ACCOUNT_TYPE_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_CONVERSION_OPTION_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_FORMAT_VARIABLE) = packet.ReadString()
        account_ht(ACCOUNT_IMAGE_VARIABLE) = packet.ReadUint32()
        account_ht(ITEMS_POSITIONS) = packet.ReadInt32()
        account_ht(ACCOUNT_TAB_VARIABLE) = packet.ReadInt32()

    End Sub

    Private Sub WriteAccountPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_FORMULA_TYPE_VARIABLE))
        packet.WriteString(attributes(ACCOUNT_FORMULA_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_TYPE_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_CONVERSION_OPTION_VARIABLE))
        packet.WriteString(attributes(ACCOUNT_FORMAT_VARIABLE))
        packet.WriteUint32(attributes(ACCOUNT_IMAGE_VARIABLE))
        packet.WriteInt32(attributes(ITEMS_POSITIONS))
        packet.WriteInt32(attributes(ACCOUNT_TAB_VARIABLE))

    End Sub

    Friend Sub LoadAccountsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, accounts_hash)

    End Sub

    Friend Sub LoadAccountsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, accounts_hash)

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ACCOUNT_ANSWER, AddressOf SMSG_READ_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_ACCOUNT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
