Public Class ModelMapping

    '-------------------------------------------------------------------------------------
    ' ModelMapping instances 
    ' Manage Connections and requests with Accounts and Assets Table mainly
    '-------------------------------------------------------------------------------------

    ' Instance variables :
    Private srv As ModelServer
    Private rst As New ADODB.Recordset                                  ' Used
    Public Property accountsRST As New ADODB.Recordset                          ' Used
    Private assetsRST As New ADODB.Recordset                            ' Used
    'Private WS As Excel.Worksheet = CreateObject("Excel.worksheet") ' Used

    ' Properties
    Public Property pUnMappedAccounts As Object()
    Public Property pUnMappedAssets As Object()


    Public Sub New()

        '-----------------------------------------------------------------------------------
        ' Class constructor
        '-----------------------------------------------------------------------------------
        srv = New ModelServer
        Me.rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Me.accountsRST = srv.openRst(ACCOUNTS_TEMP_TABLE)
        Me.assetsRST = srv.openRst(ASSETS_TABLE)

    End Sub


    Public Sub rstMainLoad()

        '---------------------------------------------------------------------
        ' Load the accounts and assets mapping
        '---------------------------------------------------------------------
        accountsRST = srv.openRst(ACCOUNTS_TEMP_TABLE)
        assetsRST = srv.openRst(ASSETS_TABLE)
       
    End Sub

    Public Function GetAccountReferences() As String()

        '-----------------------------------------------------------------------------
        ' Provides an array containing the account references table form the DataBase
        '-----------------------------------------------------------------------------
        Dim TempArray(,) As Object
        Me.rst = srv.openRst(ACCOUNTS_REFERENCES_TABLE)                            ' Upload Accounts Reference table in record set
        TempArray = Me.rst.GetRows(, , ACCOUNTS_REFERENCES_ID_VARIABLE)

        Dim stringArray(UBound(TempArray, 2)) As String
        For i = LBound(TempArray, 2) To UBound(TempArray, 2)
            stringArray(i) = CStr(TempArray(0, i))                                 ' Transform into a string array
        Next i
        GetAccountReferences = stringArray

    End Function

    Public Function IsAccountInMapping(Account As String) As String

        '------------------------------------------------------------------------------
        ' Check whether an account belongs to the mapping and return its key if yes
        ' Output: "" if the account is not in the mapping, a key if it is in mapping
        '------------------------------------------------------------------------------
        Dim criteria As String
        Account = Replace(Account, "'", "")                             '   !!! "'" to be handled (double quotes !!!)
        criteria = ACCOUNT_NAME_VARIABLE & "=" & "'" & Account & "'"
        accountsRST.Find(criteria, , , 1)                                                ' Look for the item

        If accountsRST.EOF Or accountsRST.BOF Then
            IsAccountInMapping = ""                                                      ' Item not found
            Exit Function
        End If
        IsAccountInMapping = accountsRST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value          ' Item found

    End Function

    Public Function getAccountName(key As String) As String

        '------------------------------------------------------------------------------------
        ' Return an account's name if in mapping
        ' Input: Account's key (string)
        '------------------------------------------------------------------------------------
        Dim criteria As String
        criteria = ACCOUNT_TREE_ID_VARIABLE & "='" & key & "'"
        accountsRST.Find(criteria, , , 1)                                                ' Look for the item

        If accountsRST.EOF Or accountsRST.BOF Then
            getAccountName = ""                                                          ' Item not found
            Exit Function
        End If
        getAccountName = accountsRST.Fields(ACCOUNT_NAME_VARIABLE).Value                 ' Item found


    End Function

    Public Function IsAssetInMapping(Asset As String) As String

        '---------------------------------------------------------------------------
        ' Check whether an asset belongs to the mapping and return its key if yes
        ' Output: "" if the asset is not in the mapping, a key if it is in mapping
        '---------------------------------------------------------------------------
        Dim criteria As String
        Asset = Replace(Asset, "'", "")     ' "'" to be handled (double quotes !!)
        criteria = ASSETS_NAME_VARIABLE & "=" & "'" & Asset & "'"
        assetsRST.Find(criteria, , , 1)                                             ' Look for the item

        If assetsRST.EOF Or assetsRST.BOF Then
            IsAssetInMapping = ""                                                   ' Item not found
            Exit Function
        End If
        IsAssetInMapping = assetsRST.Fields(ASSETS_TREE_ID_VARIABLE).Value          ' Item found

    End Function

    Public Function getAssetName(key As String) As String

        '------------------------------------------------------------------------------------
        ' Return an asset's name if in mapping
        ' Input: asset's key (string)
        '------------------------------------------------------------------------------------
        Dim criteria As String
        criteria = ASSETS_TREE_ID_VARIABLE & "='" & key & "'"
        assetsRST.Find(criteria, , , 1)

        If assetsRST.EOF Or assetsRST.BOF Then
            getAssetName = ""                                                          ' Item not found
            Exit Function
        End If
        getAssetName = assetsRST.Fields(ASSETS_NAME_VARIABLE).Value                    ' Item found

    End Function

    Public Function NameList(mapping As String) As String()

        '----------------------------------------------------------------------------------------
        ' Input: table: accounts or assets
        ' Output: array containing the names list of the specified table ("accounts" or "assets"
        '----------------------------------------------------------------------------------------
        Dim TempArray(,) As Object

        Select Case mapping
            Case "accounts"
                TempArray = accountsRST.GetRows(, , ACCOUNT_NAME_VARIABLE)
            Case "assets"
                TempArray = assetsRST.GetRows(, , ASSETS_NAME_VARIABLE)
        End Select

        Dim stringArray(UBound(TempArray, 2)) As String                 ' Transform into string array
        For i = LBound(TempArray, 2) To UBound(TempArray, 2)
            stringArray(i) = CStr(TempArray(0, i))
        Next i
        NameList = stringArray                                          ' Assign array to returned value

    End Function

    Public Sub generateUnmapped(mapping As String, inputArray() As Object)

        '---------------------------------------------------------------------------
        ' Generate Unmapped Accounts array (property)
        '   Inputs: 1. Mapping: accounts or assets
        '           2. Input array
        '   No output
        '---------------------------------------------------------------------------
        Dim TempArray(0) As Object
        Dim variable, criteria As String
        Dim rst As New ADODB.Recordset
        Dim j As Integer

        Select Case mapping
            Case "accounts"
                variable = ACCOUNT_NAME_VARIABLE
                rst = accountsRST
            Case "assets"
                variable = ASSETS_NAME_VARIABLE
                rst = assetsRST
            Case Else
                MsgBox("Error PP005")
                Exit Sub
        End Select

        For i = LBound(inputArray) To UBound(inputArray)
            criteria = variable & "=" & "'" & inputArray(i) & "'"
            rst.Find(criteria, , , 1)                                           ' Look for the item
            If rst.EOF Or rst.BOF Then
                ReDim Preserve TempArray(j)                                     ' Item not found
                TempArray(j) = inputArray(i)
                j = j + 1
            End If
        Next i

        'rst.Close()
        Select Case mapping
            Case "accounts" : pUnMappedAccounts = TempArray
            Case "assets" : pUnMappedAssets = TempArray
        End Select

    End Sub

    Public Function isKeyInMapping(mapping As String, key As String) As Boolean

        '-----------------------------------------------------------------------
        ' Check if the Key is in the mapping
        ' Input: mapping ("accounts" or "assets"), key
        ' Ouput: 1 if true, 0 if false
        '-----------------------------------------------------------------------
        Dim variable, criteria As String
        Dim rst As ADODB.Recordset
        Select Case mapping
            Case "accounts"
                rst = accountsRST
                variable = ACCOUNT_TREE_ID_VARIABLE
            Case "assets"
                rst = assetsRST
                variable = ASSETS_TREE_ID_VARIABLE
            Case Else
                MsgBox("Error PP005")
                isKeyInMapping = False
                Exit Function
        End Select

        criteria = variable & "=" & "'" & key & "'"
        rst.Find(criteria, , , 1)

        If rst.EOF Or rst.BOF Then
            isKeyInMapping = False                        ' Item not found
            Exit Function
        End If

        isKeyInMapping = True

    End Function

    Public Function GetValue(table As String, key As String, outputVariable As String, keyVariable As String) As Object

        '---------------------------------------------------------------
        ' Get a specified Value from a specified table
        ' Carefull : launch a new query/ associated transactions costs
        '  Inputs: 1. Table
        '          2. InputValue: table, key, output variable, keyvariable
        '          3. OutputVariable: desired corresponding value
        '---------------------------------------------------------------
        Dim sql As String
        Dim TempArray(,) As Object
        sql = "SELECT " & outputVariable & " FROM " & table & _
              " WHERE " & keyVariable & " ='" & key & " '"
        rst = srv.openRstSQL(sql)
        TempArray = rst.GetRows
        GetValue = TempArray(0, 0)
        rst.Close()

    End Function

    Public Function getCategories() As Object()

        '---------------------------------------------------------------
        ' Function: Get Categories STUB !!!
        '---------------------------------------------------------------
        Dim categories() As Object
        categories = {"Category1", "Category2", "Category3", "Category4"}
        MsgBox("GetCategories() Function in mapping needs to be implemented (stub)")
        getCategories = categories

    End Function

    Public Function getCurrencies() As Object()

        '---------------------------------------------------------------
        ' Function: Get Currencies STUB !!!
        '---------------------------------------------------------------
        Dim Currencies() As Object
        Currencies = {"EUR", "USD", "GBP", "NOK", "CAD"}
        MsgBox("GetCurrencies() Function in mapping needs to be implemented (stub)")
        getCurrencies = Currencies

    End Function

    Public Function getCountries() As Object()

        '---------------------------------------------------------------
        ' Function: Get Country STUB !!!
        '---------------------------------------------------------------
        Dim countries() As Object
        countries = {"France", "England", "USA", "Italy", "Spain", "Nederlands", "Germany", "Australia"}
        MsgBox("GetCountries() Function in mapping needs to be implemented (stub)")
        getCountries = countries

    End Function

    Public Function getHVInputs() As String()

        '-----------------------------------------------------------------
        ' Return Hard Value inputs (HV and OB formula type) IDs List
        '-----------------------------------------------------------------
        Dim TempArray(,) As Object
        accountsRST.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE & "=" & "'HV'" & _
                 " OR " & ACCOUNT_FORMULA_TYPE_VARIABLE & "=" & "'OB'"
        ' -> "OR" in filter to be tested 

        TempArray = accountsRST.GetRows(, , ACCOUNT_TREE_ID_VARIABLE)

        Dim stringArray(UBound(TempArray, 2)) As String
        For i = LBound(TempArray, 2) To UBound(TempArray, 2)
            stringArray(i) = CStr(TempArray(0, i))
        Next i
        getHVInputs = stringArray
        accountsRST.Filter = ""

    End Function

    Public Function getHVNameList() As Collections.Generic.List(Of String)

        '--------------------------------------------------------------------------
        ' Return Hard Value inputs (HV and OB formula type) Names List
        '--------------------------------------------------------------------------
        Dim nameList As New Collections.Generic.List(Of String)
        accountsRST.Filter = ACCOUNT_FORMULA_TYPE_VARIABLE & "=" & "'HV'" & _
                 " OR " & ACCOUNT_FORMULA_TYPE_VARIABLE & "=" & "'BS'"

        accountsRST.MoveFirst()
        Do While accountsRST.EOF = False
            nameList.Add(accountsRST.Fields(ACCOUNT_NAME_VARIABLE).Value)
            accountsRST.MoveNext()
        Loop

        getHVNameList = nameList
        accountsRST.Filter = ""

    End Function

    Public Function FindAssetChildren(Asset As String) As Collections.Generic.List(Of String)

        '------------------------------------------------------------------------
        ' Return Asset Children ID List
        '------------------------------------------------------------------------
        Dim ChildrenList As New Collections.Generic.List(Of String)
        assetsRST.Filter = ASSETS_PARENT_ID_VARIABLE & "=" & "'" & Asset & "'"
        If assetsRST.EOF Or assetsRST.BOF Then
            FindAssetChildren = Nothing                                         ' No children
            Exit Function                                                       ' Check what if no children  = OK
        End If
        assetsRST.MoveFirst()
        Do While assetsRST.EOF = False
            ChildrenList.Add(CStr(assetsRST.Fields(ASSETS_TREE_ID_VARIABLE).Value))
            assetsRST.MoveNext()
        Loop
        FindAssetChildren = ChildrenList
        assetsRST.Filter = ""

    End Function

    Public Function FindAccountChildren(Account As String) As Collections.Generic.List(Of String)

        '------------------------------------------------------------------------
        ' Return Account Children List
        '------------------------------------------------------------------------
        Dim ChildrenList As New Collections.Generic.List(Of String)
        accountsRST.Filter = ACCOUNT_PARENT_ID_VARIABLE & "=" & "'" & Account & "'"
        If accountsRST.EOF Or accountsRST.BOF Then
            FindAccountChildren = Nothing                                 ' No children
            Exit Function                                                 ' Check if it's working
        End If
        accountsRST.MoveFirst()
        Do While accountsRST.EOF = False
            ChildrenList.Add(CStr(accountsRST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value))
            accountsRST.MoveNext()
        Loop
        FindAccountChildren = ChildrenList
        accountsRST.Filter = ""

    End Function

    Public Function GetFormulaType(AccountKey As String) As String

        '------------------------------------------------------------------------
        '  Returns the variable "Formula type" for a given account
        '------------------------------------------------------------------------
        Dim criteria As String
        criteria = ACCOUNT_TREE_ID_VARIABLE & "=" & "'" & AccountKey & "'"
        accountsRST.Find(criteria, , , 1)

        If accountsRST.EOF Or accountsRST.BOF Then
            MsgBox("Key not in mapping, check modeldatabase. Error PP001")          ' Item not found
            GetFormulaType = "Key Not in mapping"
            Exit Function
        End If
        GetFormulaType = CStr(accountsRST.Fields(ACCOUNT_FORMULA_TYPE_VARIABLE).Value)

    End Function

    Public Function GetCurrency(AssetID As String) As String

        '------------------------------------------------------------------------
        ' Return Asset currency
        '------------------------------------------------------------------------
        Dim criteria As String
        criteria = ASSETS_TREE_ID_VARIABLE & "=" & "'" & AssetID & "'"
        assetsRST.Find(criteria, , , 1)

        If assetsRST.EOF Or assetsRST.BOF Then
            MsgBox("Key not in mapping, check modeldatabase. Error PP002")                        ' Item not found
            GetCurrency = "Key not in mapping"
            Exit Function
        End If
        GetCurrency = CStr(assetsRST.Fields(ASSETS_CURRENCY_VARIABLE).Value)

    End Function

    Public Function getOpeningPeriod() As Integer

        '-------------------------------------------------------
        ' Return the opening period (variable stored in DB)
        '-------------------------------------------------------

        Return GetValue(EXTRA_DATA_TABLE_NAME, _
                        STARTING_PERIOD_KEY, _
                        EXTRA_DATA_VALUE_VARIABLE, _
                        EXTRA_DATA_KEY_VARIABLE)

    End Function

    Public Function getLastPeriod() As Integer

        '-------------------------------------------------------
        ' Return the last period (variable stored in DB)
        '-------------------------------------------------------
        Return GetValue(EXTRA_DATA_TABLE_NAME, _
                        ENDING_PERIOD_KEY, _
                        EXTRA_DATA_VALUE_VARIABLE, _
                        EXTRA_DATA_KEY_VARIABLE)

    End Function

End Class
