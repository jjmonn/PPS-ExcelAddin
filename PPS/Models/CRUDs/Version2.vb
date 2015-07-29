' Version.vb
'
' CRUD for versions table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 29/07/2015
' Last modified: 29/07/2015


Imports System.Collections
Imports System.Collections.Generic



Friend Class AccoVersion2unt

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend versions_hash As New Hashtable

    ' Events
    Public Event VersionCreationEvent(ByRef attributes As Hashtable)
    Public Event VersionUpdateEvent()
    Public Event VersionDeleteEvent()


#End Region


#Region "Init"

    Friend Sub New()

        state_flag = False
        LoadVersionsTable()
        Dim time_stamp = Timer
        'Do
        '    'If Timer - time_stamp > GlobalVariables.timeOut Then
        '    '    state_flag = False
        '    '    Exit Do
        '    'End If
        'Loop While server_response_flag = False
        state_flag = True

    End Sub

    Friend Sub LoadVersionsTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_VERSION_ANSWER, AddressOf SMSG_LIST_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_VERSION, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_VERSION_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim nb_versions = packet.ReadInt32()
            For i As Int32 = 0 To nb_versions - 1
                Dim tmp_ht As New Hashtable
                GetVersionHTFromPacket(packet, tmp_ht)
                versions_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_VERSION_ANSWER, AddressOf SMSG_LIST_VERSION_ANSWER)
            server_response_flag = True
        Else
            server_response_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_VERSION_ANSWER, AddressOf SMSG_CREATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_VERSION, UShort))
        WriteVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_VERSION_ANSWER(packet As ByteBuffer)

        MsgBox(packet.ReadString())
        ' we should receive the version and the confirmation that the version has been created
        Dim tmp_ht As New Hashtable
        GetVersionHTFromPacket(packet, tmp_ht)
        versions_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_VERSION_ANSWER, AddressOf SMSG_CREATE_VERSION_ANSWER)
        RaiseEvent VersionCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_VERSION(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_VERSION_ANSWER, AddressOf SMSG_READ_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_VERSION, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_VERSION_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetVersionHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_VERSION_ANSWER, AddressOf SMSG_READ_VERSION_ANSWER)
        ' Send the ht to the object which demanded it

    End Sub

    Friend Sub CMSG_UPDATE_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_VERSION, UShort))
        WriteVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_VERSION_ANSWER(packet As ByteBuffer)

        ' Confirmation => return version
        Dim ht As New Hashtable
        GetVersionHTFromPacket(packet, ht)
        versions_hash(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_VERSION_ANSWER, AddressOf SMSG_UPDATE_VERSION_ANSWER)
        RaiseEvent VersionUpdateEvent()

    End Sub

    Friend Sub CMSG_DELETE_VERSION(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_VERSION_ANSWER, AddressOf SMSG_DELETE_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_VERSION, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_VERSION_ANSWER()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_VERSION_ANSWER, AddressOf SMSG_DELETE_VERSION_ANSWER)
        RaiseEvent VersionDeleteEvent()

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetVersionsList(ByRef LookupOption As String, ByRef variable As String)

        'Dim tmp_list As New List(Of String)
        'Dim selection() As String = {}
        'Select Case LookupOption
        '    Case GlobalEnums.VersionsLookupOptions.LOOKUP_ALL : selection = {GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_VERSIONS, _
        '                                                                    GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
        '                                                                    GlobalEnums.FormulaTypes.FORMULA, _
        '                                                                    GlobalEnums.FormulaTypes.HARD_VALUE_INPUT}

        '    Case GlobalEnums.VersionsLookupOptions.LOOKUP_INPUTS : selection = {GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
        '                                                                        GlobalEnums.FormulaTypes.HARD_VALUE_INPUT}

        '    Case GlobalEnums.VersionsLookupOptions.LOOKUP_OUTPUTS : selection = {GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_VERSIONS, _
        '                                                                         GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
        '                                                                         GlobalEnums.FormulaTypes.FORMULA}

        '    Case GlobalEnums.VersionsLookupOptions.LOOKUP_TITLES : selection = {GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_VERSIONS, _
        '                                                                         GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT, _
        '                                                                         GlobalEnums.FormulaTypes.FORMULA}
        'End Select

        'For Each id In versions_hash.Keys
        '    If selection.ToString.Contains(versions_hash(id)(VERSION_FORMULA_TYPE_VARIABLE)) Then
        '        tmp_list.Add(versions_hash(id)(variable))
        '    End If
        'Next
        'Return tmp_list

    End Function

    Friend Function GetVersionsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In versions_hash.Keys
            tmpHT(versions_hash(id)(Key)) = versions_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetVersionHTFromPacket(ByRef packet As ByteBuffer, ByRef version_ht As Hashtable)

        version_ht(ID_VARIABLE) = packet.ReadUint32()
        version_ht(PARENT_ID_VARIABLE) = packet.ReadInt32()
        version_ht(NAME_VARIABLE) = packet.ReadInt32()
        version_ht(VERSIONS_LOCKED_VARIABLE) = packet.ReadBool()

        version_ht(VERSIONS_CREATION_DATE_VARIABLE) = packet.ReadString()

        version_ht(VERSIONS_LOCKED_DATE_VARIABLE) = packet.ReadInt32()
        version_ht(VERSIONS_IS_FOLDER_VARIABLE) = packet.ReadUint16()
        version_ht(ITEMS_POSITIONS) = packet.ReadInt32()
        version_ht(VERSIONS_TIME_CONFIG_VARIABLE) = packet.ReadInt32()
        version_ht(VERSIONS_RATES_VERSION_ID_VAR) = packet.ReadInt32()
        version_ht(VERSIONS_START_PERIOD_VAR) = packet.ReadInt32()
        version_ht(VERSIONS_NB_PERIODS_VAR) = packet.ReadInt32()


    End Sub

    Private Sub WriteVersionPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteInt32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

        packet.WriteInt32(attributes(ITEMS_POSITIONS))


    End Sub

    Friend Sub LoadVersionsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, versions_hash)

    End Sub

    'Private Function GetVersionHashShortListedByFormulaType(ByRef formula_type As String)

    '    Dim tmp_acc_hash As New Hashtable
    '    For Each id In versions_hash.Keys
    '        If versions_hash(id)(VERSION_FORMULA_TYPE_VARIABLE) = formula_type Then
    '            tmp_acc_hash(id) = versions_hash(id)
    '        End If
    '    Next
    '    Return tmp_acc_hash

    'End Function

#End Region







End Class
