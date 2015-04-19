' AccountsMappingTransactions.vb
'
' Manages the transaction with ACCOUNTS table in DB. Create the accounts related dictionaries
'
'
' To do : 
'
'
' Know bugs :
' 
'
'Last modified: 27/02/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Collections

Friend Class AccountsMapping


#Region "Constants"

    Protected Friend Const LOOKUP_OUTPUTS As String = "Outputs"
    Protected Friend Const LOOKUP_INPUTS As String = "Inputs"
    Protected Friend Const LOOKUP_ALL As String = "Everything"
    Protected Friend Const LOOKUP_TITLES As String = "Titles"

#End Region


#Region "LISTS"

    ' Return a list of Accounts NAMES.Param: 
    '  - LOOKUP_ALL 
    '  - LOOKUP_INPUTS
    '  - LOOKUP_OUTPUTS
    '  - LOOKUP_TITLES
    Protected Friend Shared Function GetAccountsNamesList(ByRef LookupOption As String) As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Sort = ITEMS_POSITIONS

        Select Case LookupOption
            Case LOOKUP_ALL : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "<>'T'"
            Case LOOKUP_INPUTS : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "='HV'" _
                                                  + "OR " + ACCOUNT_FORMULA_TYPE_VARIABLE + "='BS'"
            Case LOOKUP_OUTPUTS : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "<>'HV'" _
                                                   + "AND " + ACCOUNT_FORMULA_TYPE_VARIABLE + "<>'T'"
            Case LOOKUP_TITLES : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "='T'"
        End Select

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(ACCOUNT_NAME_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function

    ' Return a list of Accounts KEYS. Param: LOOKUP_ALL , LOOKUP_INPUTS, LOOKUP_OUTPUTS
    Protected Friend Shared Function GetAccountsKeysList(ByRef LookupOption As String) As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)

        Select Case LookupOption
            Case LOOKUP_ALL : srv.rst.Filter = ""
            Case LOOKUP_INPUTS : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "='HV'" _
                                                  + "OR " + ACCOUNT_FORMULA_TYPE_VARIABLE + "='BS'"
            Case LOOKUP_OUTPUTS : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "<>'HV'" _
                                                   + ACCOUNT_FORMULA_TYPE_VARIABLE + "<>'T'"
            Case LOOKUP_TITLES : srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "='T'"
        End Select

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(ACCOUNT_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function

    Protected Friend Shared Function GetRecomputedAccountsIDsList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = ACCOUNT_RECOMPUTATION_OPTION_VARIABLE + "='" + RECOMPUTATION_CODE + "'"

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(ACCOUNT_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function

#End Region


#Region "DICTIONARIES"

    ' AccountsDictionary. Param 1: key. Param 2: value
    Public Shared Function GetAccountsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)

        srv.rst.Filter = ""
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    ' AccountsTypesDictionary. Param 1: key. Param 2: value
    Public Shared Function GetAccountsTypesDictionary(ByRef Key As String, ByRef Value As String) As Dictionary(Of String, String)

        Dim typesDict As New Dictionary(Of String, String)
        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            While srv.rst.EOF = False
                typesDict.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
                srv.rst.MoveNext()
            End While
        End If

        srv.rst.Close()
        srv = Nothing
        Return typesDict

    End Function


#End Region


#Region "DICTIONARY OF DICTIONARIES"

    ' Returns a AccountName(type,format) dictionary
    Public Shared Function GetAccountsKeysFormatsTypesDictionary() As Dictionary(Of String, Dictionary(Of String, String))

        Dim keysTypesFormatsDict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)

        srv.rst.MoveFirst()
        If srv.rst.EOF = True Or srv.rst.BOF = True Then
            srv.rst.Close()
            Return Nothing
        Else
            While srv.rst.EOF = False
                Dim tmpDic As New Dictionary(Of String, String)
                tmpDic.Add(ACCOUNT_TYPE_VARIABLE, srv.rst.Fields(ACCOUNT_TYPE_VARIABLE).Value)
                tmpDic.Add(ACCOUNT_FORMAT_VARIABLE, srv.rst.Fields(ACCOUNT_FORMAT_VARIABLE).Value)

                keysTypesFormatsDict.Add(srv.rst.Fields(ACCOUNT_ID_VARIABLE).Value, tmpDic)
                srv.rst.MoveNext()
            End While
            srv.rst.Close()
            srv = Nothing
            Return keysTypesFormatsDict
        End If

    End Function


#End Region


#Region "TABLE"

    ' Return accounts table arrays for model construction
    Protected Friend Shared Function GetAccountsTableArrays() As Dictionary(Of String, String())

        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE + "<>'" + FORMULA_TYPE_TITLE + "'"

        Dim accountTableArrays As New Dictionary(Of String, String())
        Dim nbAccounts As Integer = srv.rst.RecordCount
        Dim i As Integer = 0
        Dim tokensArray(nbAccounts - 1) As String
        Dim parentsArray(nbAccounts - 1) As String
        Dim formulaTypesArray(nbAccounts - 1) As String
        Dim formulaArray(nbAccounts - 1) As String
        Dim conversionFlag(nbAccounts - 1) As String

        If srv.rst.EOF = False Then
            srv.rst.MoveFirst()
            srv.rst.Sort = ITEMS_POSITIONS
            Do While srv.rst.EOF = False
                tokensArray(i) = srv.rst.Fields(ACCOUNT_ID_VARIABLE).Value
                parentsArray(i) = srv.rst.Fields(ACCOUNT_PARENT_ID_VARIABLE).Value
                formulaTypesArray(i) = srv.rst.Fields(ACCOUNT_FORMULA_TYPE_VARIABLE).Value
                formulaArray(i) = srv.rst.Fields(ACCOUNT_FORMULA_VARIABLE).Value
                conversionFlag(i) = srv.rst.Fields(ACCOUNT_CONVERSION_FLAG_VARIABLE).Value
                srv.rst.MoveNext()
                i = i + 1
            Loop
        End If
        accountTableArrays.Add(ACCOUNT_ID_VARIABLE, tokensArray)
        accountTableArrays.Add(ACCOUNT_PARENT_ID_VARIABLE, parentsArray)
        accountTableArrays.Add(ACCOUNT_FORMULA_TYPE_VARIABLE, formulaTypesArray)
        accountTableArrays.Add(ACCOUNT_FORMULA_VARIABLE, formulaArray)
        accountTableArrays.Add(ACCOUNT_CONVERSION_FLAG_VARIABLE, conversionFlag)

        srv.rst.Close()
        srv = Nothing
        Return accountTableArrays

    End Function

    ' Return Account Children List
    Protected Friend Shared Function FindAccountChildren(Account As String) As List(Of String)

        Dim srv = New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR)

        Dim ChildrenList As New List(Of String)
        srv.rst.Filter = ACCOUNT_PARENT_ID_VARIABLE + "=" + "'" + Account + "'"
        If srv.rst.EOF Or srv.rst.BOF Then
            srv.rst.Close()
            Return Nothing                                 ' No children
            Exit Function                                                 ' Check if it's working
        End If
        srv.rst.MoveFirst()
        Do While srv.rst.EOF = False
            ChildrenList.Add(CStr(srv.rst.Fields(ACCOUNT_ID_VARIABLE).Value))
            srv.rst.MoveNext()
        Loop

        srv.rst.Close()
        srv = Nothing
        Return ChildrenList


    End Function

#End Region



End Class
