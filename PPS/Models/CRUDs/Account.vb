' Account2.vb
'
' CRUD for accounts table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 16/07/2015
' Last modified: 17/07/2015


Imports System.Collections
Imports System.Collections.Generic



Friend Class Account

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend accounts_hash As New Hashtable

    ' Events
    Public Event AccountCreationEvent(ByRef attributes As Hashtable)
    Public Event AccountUpdateEvent()
    Public Event AccountDeleteEvent()


#End Region


#Region "Init"

    Friend Sub New()

        LoadAccountsTable()
        Dim time_stamp = Timer
        Do
            If Timer - time_stamp > GlobalVariables.timeOut Then
                state_flag = False
                Exit Do
            End If
        Loop While server_response_flag = True
        state_flag = True

    End Sub

    Friend Sub LoadAccountsTable()

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_LIST_ACCOUNT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ACCOUNT_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            Dim tmp_ht As New Hashtable
            GetAccountHTFromPacket(packet, tmp_ht)
            accounts_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        Next
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)
        server_response_flag = True

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_ACCOUNT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_ACCOUNT, UShort))
        WriteAccountPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ACCOUNT_ANSWER(packet As ByteBuffer)

        MsgBox(packet.ReadString())
        ' we should receive the account and the confirmation that the account has been created
        Dim tmp_ht As New Hashtable
        GetAccountHTFromPacket(packet, tmp_ht)
        accounts_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_CREATE_ACCOUNT_ANSWER)
        RaiseEvent AccountCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_ACCOUNT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_READ_ACCOUNT_ANSWER, AddressOf SMSG_READ_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_ACCOUNT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_ACCOUNT_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetAccountHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER, AddressOf SMSG_READ_ACCOUNT_ANSWER)
        ' Send the ht to the object which demanded it

    End Sub

    Friend Sub CMSG_UPDATE_ACCOUNT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_ACCOUNT, UShort))
        WriteAccountPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ACCOUNT_ANSWER(packet As ByteBuffer)

        ' Confirmation => return account
        Dim ht As New Hashtable
        GetAccountHTFromPacket(packet, ht)
        accounts_hash(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER, AddressOf SMSG_UPDATE_ACCOUNT_ANSWER)
        RaiseEvent AccountUpdateEvent()

    End Sub

    Friend Sub CMSG_DELETE_ACCOUNT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_ACCOUNT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_DELETE_ACCOUNT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ACCOUNT_ANSWER()

        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER, AddressOf SMSG_DELETE_ACCOUNT_ANSWER)
        RaiseEvent AccountDeleteEvent()

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetAccountsList(ByRef LookupOption As String, ByRef variable As String)

        Dim tmp_list As New List(Of String)
        Dim selection() As String = {}
        Select Case LookupOption
            Case GlobalEnums.AccountsLookupOptions.LOOKUP_ALL : selection = {GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS, _
                                                                            GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
                                                                            GlobalEnums.FormulaTypes.FORMULA, _
                                                                            GlobalEnums.FormulaTypes.HARD_VALUE_INPUT}

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS : selection = {GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
                                                                                GlobalEnums.FormulaTypes.HARD_VALUE_INPUT}

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS : selection = {GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS, _
                                                                                 GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
                                                                                 GlobalEnums.FormulaTypes.FORMULA}

            Case GlobalEnums.AccountsLookupOptions.LOOKUP_TITLES : selection = {GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS, _
                                                                                 GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
                                                                                 GlobalEnums.FormulaTypes.FORMULA}
        End Select

        For Each id In accounts_hash.Keys
            If selection.ToString.Contains(accounts_hash(id)(ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
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

#End Region


#Region "Utilities"

    Friend Shared Sub GetAccountHTFromPacket(ByRef packet As ByteBuffer, ByRef account_ht As Hashtable)

        account_ht(ID_VARIABLE) = packet.ReadInt32()
        account_ht(PARENT_ID_VARIABLE) = packet.ReadInt32()
        account_ht(NAME_VARIABLE) = packet.ReadString()
        account_ht(ACCOUNT_FORMULA_TYPE_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_FORMULA_VARIABLE) = packet.ReadString()
        account_ht(ACCOUNT_TYPE_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_CONVERSION_OPTION_VARIABLE) = packet.ReadInt32()
        account_ht(ACCOUNT_FORMAT_VARIABLE) = packet.ReadString()
        account_ht(ACCOUNT_IMAGE_VARIABLE) = packet.ReadInt32()
        account_ht(ITEMS_POSITIONS) = packet.ReadInt32()
        account_ht(ACCOUNT_TAB_VARIABLE) = packet.ReadInt32()

    End Sub

    Private Sub WriteAccountPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteInt32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_FORMULA_TYPE_VARIABLE))
        packet.WriteString(attributes(ACCOUNT_FORMULA_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_TYPE_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_CONVERSION_OPTION_VARIABLE))
        packet.WriteString(attributes(ACCOUNT_FORMAT_VARIABLE))
        packet.WriteInt32(attributes(ACCOUNT_IMAGE_VARIABLE))
        packet.WriteInt32(attributes(ITEMS_POSITIONS))
        packet.WriteInt32(attributes(ACCOUNT_TAB_VARIABLE))

    End Sub

    Friend Sub LoadAccountsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, accounts_hash)

    End Sub

    'Private Function GetAccountHashShortListedByFormulaType(ByRef formula_type As String)

    '    Dim tmp_acc_hash As New Hashtable
    '    For Each id In accounts_hash.Keys
    '        If accounts_hash(id)(ACCOUNT_FORMULA_TYPE_VARIABLE) = formula_type Then
    '            tmp_acc_hash(id) = accounts_hash(id)
    '        End If
    '    Next
    '    Return tmp_acc_hash

    'End Function

#End Region







End Class
