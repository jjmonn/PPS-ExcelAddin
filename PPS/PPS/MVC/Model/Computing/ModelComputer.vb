Imports Microsoft.Office.Interop
Imports System.Linq

Public Class ModelComputer

    '-----------------------------------------------------------------------------------
    ' This class build a model in a dictionary(key/values) from the formulas given 
    ' in input.
    '
    ' -> Need to separate Model construction (input form DB) from Model use (input-> outputs procedures)
    '
    ' The model dictionary holds all the accounts for an asset or an aggregation and can 
    ' be take back to be displayed or aggregated in another storage unit.
    '-----------------------------------------------------------------------------------

    Public Property ModelDictionary As New Collections.Generic.Dictionary(Of String, Collections.Generic.Dictionary(Of Integer, Excel.Range))
    'Public ModelingApp As Excel.Application
    Public WS As Excel.Worksheet
    Public Property startCell As Excel.Range
    Public Property endCell As Excel.Range
    Public PeriodList() As Integer
    Private AccountsListHT = New Collections.Generic.Dictionary(Of String, Object())
    Private OperandsList As New Collections.Generic.List(Of String)
    Private OperatorsList As New Collections.Generic.List(Of String)
    Private FormulaFlag As New Collections.Generic.List(Of String)
    'Public accountsList As New Collections.Generic.List(Of String)
    Private srv As New ModelServer
    Private mapp As New ModelMapping


    Public Sub New()

        '----------------------------------------------------------------------
        ' Initialization: launch the model build
        '----------------------------------------------------------------------
        buildPeriodList()
        If IsNothing(ModelingApp) Then
            ModelingApp = New Excel.Application
        End If

            Dim WB As Excel.Workbook = CType(ModelingApp.Workbooks.Add(), Excel.Workbook)
            WS = CType(ModelingApp.ActiveSheet, Excel.Worksheet)

            ModelInit()
            DictionaryInit()
            LaunchModeling()
            WS.Range(MODEL_BUILT_CELL_FLAG).Value2 = MODEL_BUILT_FLAG_TRUE

            startCell = WS.Cells(1, 1)

            AccountsListHT = Nothing
            mapp = Nothing
            srv = Nothing
            OperandsList = Nothing
            OperatorsList = Nothing

    End Sub

    Private Sub buildPeriodList()

        '--------------------------------------------------------------------------
        ' Build an array holding the list of periods from opening to closing
        '--------------------------------------------------------------------------
        Dim startPeriod As Integer = MAPP.getOpeningPeriod
        Dim endPeriod As Integer = MAPP.getLastPeriod

        ReDim periodList(endPeriod - startPeriod)
        For i = 0 To endPeriod - startPeriod
            periodList(i) = startPeriod + i
        Next

    End Sub

    Private Sub ModelInit()

        '----------------------------------------------------------------------------------
        ' Loop through all accounts from accounts table 
        ' Launch either SOAC, Formula, BS or OL computation subs according to the account 
        ' formula type.
        '----------------------------------------------------------------------------------
        Dim rstAccounts As ADODB.Recordset
        Dim sqlQuery As String
        Dim i As Integer
        Dim TempArray1(,) As Object

        ' To be sent to CONTROLLER / DB !! 
        sqlQuery = "SELECT " & ACCOUNT_TREE_ID_VARIABLE & ", " _
                   & ACCOUNT_FORMULA_TYPE_VARIABLE & ", " _
                   & ACCOUNT_FORMULA_VARIABLE & ", " _
                   & ACCOUNT_POSITION_VARIABLE _
                   & " FROM " & ACCOUNTS_TEMP_TABLE _
                   & " ORDER BY " & ACCOUNT_POSITION_VARIABLE

        rstAccounts = srv.openRstSQL(sqlQuery)                                  ' Query database
        TempArray1 = rstAccounts.GetRows

        For i = LBound(TempArray1, 2) To UBound(TempArray1, 2)
            Dim temparray2(0 To 2) As Object
            temparray2(0) = TempArray1(1, i)                                    ' Save Formula type into TempArray2
            If TempArray1(1, i) = "HV" Or TempArray1(1, i) = "OB" Then
                temparray2(1) = 1                                               ' Save Calculated = 1 (input HV) into TempArray2
            Else
                temparray2(1) = 0                                               ' Save Calculated = 0 initialized into TempArray2
            End If
            temparray2(2) = TempArray1(2, i)                                    ' Save Formula into TempArray2
            AccountsListHT.Add(CStr(TempArray1(0, i)), temparray2)              ' Save to HT
        Next i

        endCell = WS.Cells(UBound(TempArray1, 2) + 1, UBound(PeriodList) + 1 + 1)

        TempArray1 = Nothing
        rstAccounts.Close()

    End Sub

    Private Sub DictionaryInit()

        '---------------------------------------------------------------------------------------
        ' Init ModelDictionary with CItem arrays
        '---------------------------------------------------------------------------------------
        Dim i, j As Integer

        For Each key As String In AccountsListHT.keys
            Dim tempDictionary As New Collections.Generic.Dictionary(Of Integer, Excel.Range)
            j = 1
            WS.Cells(i + 1, 1).value = mapp.getAccountName(key)
            For Each period As Integer In PeriodList
                Dim range As Excel.Range
                range = WS.Cells(i + 1, j + 1)
                tempDictionary.Add(period, range)
                j = j + 1
            Next
            ModelDictionary.Add(key, tempDictionary)
            i = i + 1
        Next

    End Sub


    Public Sub modelUpdate()

        '----------------------------------------------------------------------------------
        ' Calculate worksheet
        '----------------------------------------------------------------------------------
        WS.Calculate()
    End Sub

    Private Sub LaunchModeling()

        '----------------------------------------------------------------------------------
        ' Launch the loop through accounts
        '----------------------------------------------------------------------------------
        For Each account As String In AccountsListHT.keys                                          ' Loop through accounts
            If AccountsListHT.Item(account)(1) = 0 Then
                Select Case AccountsListHT.Item(account)(0)

                    Case "SOAC" : AggregateChildren(mapp.FindAccountChildren(account), account)    ' Private recursive function
                    Case "F", "BS" : ComputeUnderlyings(FindFormulaUnderlyings(account), account)  ' Computation
                    Case "OL" : SetLineReference(account, CStr(AccountsListHT.Item(account)(2)))   ' Other line reference

                End Select
            End If
        Next

    End Sub

    Private Sub AggregateChildren(ChildrenArray As Collections.Generic.List(Of String), parent As String)

        '--------------------------------------------------------------------------
        ' SOAC Account Children Aggregation
        ' Recursive function : aggregate children if no sub aggregation needed
        ' if sub aggregation needed: the function call itself with new children list
        '--------------------------------------------------------------------------
        If Not ChildrenArray Is Nothing Then
            For Each children As String In ChildrenArray
                If AccountsListHT.Item(children)(1) = 0 Then
                    Select Case AccountsListHT.Item(children)(0)

                        Case "SOAC" : AggregateChildren(mapp.FindAccountChildren(children), children)    ' Recursive call
                        Case "F", "BS" : ComputeUnderlyings(FindFormulaUnderlyings(children), children)
                        Case "OL" : SetLineReference(children, CStr(AccountsListHT.Item(children)(2)))

                    End Select
                End If
            Next
            If AccountsListHT.Item(parent)(1) = 0 Then AggregateProduction(ChildrenArray, parent) ' -> ACTUAL AGGREGATION
        End If
    End Sub

    Private Sub AggregateProduction(ChildrenArray As Collections.Generic.List(Of String), parent As String)

        '--------------------------------------------------------------------------
        ' Set ModelDictionaray parent key reference to SUM(Childen)
        '--------------------------------------------------------------------------
        Dim expression As String

        For Each period As Integer In PeriodList
            expression = "=SUM("
            For Each child As String In ChildrenArray
                expression = expression & CStr(ModelDictionary.Item(child).Item(period).Address) & ","
            Next
            expression = Left(expression, Len(expression) - 1) & ")"
            ModelDictionary.Item(parent).Item(period).Formula = expression
        Next

        AccountsListHT.Item(parent)(1) = 1

    End Sub

    Private Function FindFormulaUnderlyings(ParentID As String) As Collections.Generic.List(Of String)

        '------------------------------------------------------------------------------
        ' Returns an array with involved accounts
        '------------------------------------------------------------------------------
        Dim UnderlyingsList As New Collections.Generic.List(Of String)
        Dim Formula As String = AccountsListHT.Item(ParentID)(2)
        Dim FormulaArray() As String = GetFormulaArray(Formula)
        Dim accountsList As Collections.Generic.List(Of String) = ModelDictionary.Keys.ToList

        For Each item As String In FormulaArray
            If accountsList.Contains(item) Then
                UnderlyingsList.Add(item)                                   ' Save underlyings only
            End If
        Next
        FindFormulaUnderlyings = UnderlyingsList

    End Function

    Private Function GetFormulaArray(Formula As String) As String()

        '------------------------------------------------------------------------------------------
        ' Produce an array with only operands and operators
        ' -> perf -> on peut le remttre à l'endroit du call
        '------------------------------------------------------------------------------------------
        Formula = DivideFormula(Formula)                          ' string with everything separated by "|"
        GetFormulaArray = Split(Formula, FORMULA_SEPARATOR)       ' Array with operands and operators
    End Function

    Private Sub ComputeUnderlyings(Underlyings As Collections.Generic.List(Of String), FormulaID As String)

        '----------------------------------------------------------------------------
        ' Compute the underlyings of an Account formula - Recursive
        ' Launch Computation once all underlying are calculated
        '----------------------------------------------------------------------------
        For Each underlying As String In Underlyings
            If AccountsListHT.Item(underlying)(1) = 0 Then
                Select Case AccountsListHT.Item(underlying)(0)

                    Case "F", "BS" : ComputeUnderlyings(FindFormulaUnderlyings(underlying), underlying)     ' Recursive call
                    Case "SOAC" : AggregateChildren(mapp.FindAccountChildren(underlying), underlying)      ' Go to Aggregate recursive
                    Case "OL" : SetLineReference(underlying, CStr(AccountsListHT.Item(underlying)(2)))

                End Select
            End If
        Next

        If AccountsListHT.Item(FormulaID)(1) = 0 Then
            Select Case AccountsListHT.Item(FormulaID)(0)
                Case "F" : ComputeFormula(FormulaID)
                Case "BS" : ComputeBSItem(FormulaID)
            End Select
        End If

    End Sub

    Private Sub ComputeFormula(FormulaID As String)

        '-----------------------------------------------------------------------------------------
        ' Compute a formula (All underlyings are supposed to be calculated at this point)
        '-----------------------------------------------------------------------------------------
        Dim FormulaArray() As String
        Dim Formula As String

        Formula = AccountsListHT.Item(FormulaID)(2)
        FormulaArray = GetFormulaArray(Formula)
        FormulaToOperandsAndOperators(FormulaArray)                       ' Set up Operands and Operators array (Ivars)
        Compute(FormulaID)
        AccountsListHT.Item(FormulaID)(1) = 1                            ' Update calculated flag

    End Sub

    Private Sub FormulaToOperandsAndOperators(FormulaArray() As String)

        '------------------------------------------------------------------------------
        ' Fill the ivar arrays OperandLlist and OperatorsList from a FormulaArray()
        '------------------------------------------------------------------------------
        Dim i As Integer, j As Integer
        Dim Operators As New Collections.Generic.List(Of String) From {"+", "-", "*", "/", "^", "(", ")"}   ' !!

        For i = LBound(FormulaArray) To UBound(FormulaArray)
            If FormulaArray(i) <> "" Then
                If Operators.Contains(FormulaArray(i)) Then
                    If j - 1 >= 0 Then
                        If FormulaArray(i - 1) = OperatorsList(j - 1) Then                   ' Two operators are following each other
                            OperatorsList(j - 1) = OperatorsList(j - 1) & FormulaArray(i)
                        Else
                            OperatorsList.Add(FormulaArray(i))
                            FormulaFlag.Add(OPERATORS_FLAG)                                  ' Flag operator
                        End If
                    Else
                        OperatorsList.Add(FormulaArray(i))
                        FormulaFlag.Add(OPERATORS_FLAG)                                      ' Flag operator
                    End If
                    j = j + 1
                Else
                    OperandsList.Add(FormulaArray(i))
                    FormulaFlag.Add(OPERANDS_FLAG)                                           ' Flag operand
                End If
            End If
        Next i

    End Sub

    Private Sub Compute(account As String)

        '-----------------------------------------------------------------------
        ' 
        '----------------------------------------------------------------------
        Dim TempStrFormula As String
        Dim OperatorIndex, OperandIndex As Integer

        For Each period As Integer In PeriodList                         ' Loop through periods
            TempStrFormula = "="
            OperatorIndex = 0
            OperandIndex = 0
            For Each flag As String In FormulaFlag                       ' Loop through Accounts
                If flag = OPERANDS_FLAG Then
                    TempStrFormula = TempStrFormula & ModelDictionary.Item(OperandsList(OperandIndex)).Item(period).Address
                    OperandIndex = OperandIndex + 1
                Else
                    TempStrFormula = TempStrFormula & OperatorsList(OperatorIndex)
                    OperatorIndex = OperatorIndex + 1
                End If
            Next
            TempStrFormula = Replace(TempStrFormula, ",", ".")
            ModelDictionary.Item(account).Item(period).Formula = TempStrFormula
        Next

        OperandsList.Clear()
        OperatorsList.Clear()
        FormulaFlag.Clear()

    End Sub

    Private Sub SetLineReference(Account As String, Formula As String)

        '--------------------------------------------------------------------------------
        ' Save an account in the modelDictionary in reference to the specified account
        ' (if the reference is available. If not, computation of the reference)
        '--------------------------------------------------------------------------------
        If AccountsListHT.Item(Formula)(1) = 1 Then
            For Each period As Integer In PeriodList
                ModelDictionary.Item(Account).Item(period).Formula = "=" & ModelDictionary.Item(Formula).Item(period).Address
                AccountsListHT.Item(Account)(1) = 1
            Next
        Else
            Select Case AccountsListHT.Item(Formula)(0)
                Case "SOAC" : AggregateChildren(mapp.FindAccountChildren(Formula), Formula)   ' Private recursive function

                Case "F", "BS" : ComputeUnderlyings(FindFormulaUnderlyings(Formula), Formula) ' Formula Calculation Routine

                Case "OL" : SetLineReference(Formula, CStr(AccountsListHT.Item(Formula)(2)))
            End Select
        End If

    End Sub

    Private Sub ComputeBSItem(AccountID As String)

        '-------------------------------------------------------------------------
        ' Compute BS Account Once underlying are availables
        '-------------------------------------------------------------------------
        Dim Formula As String
        Dim FormulaArray() As String

        Formula = AccountsListHT.Item(AccountID)(2)
        FormulaArray = GetFormulaArray(Formula)
        FormulaToOperandsAndOperators(FormulaArray)               ' Set up Operands and Operators arrays (Ivars)

        ' Initialization : value = OB value (accountKey & OB)
        ModelDictionary(AccountID).ElementAt(0).Value.Value2 = ModelDictionary.Item(AccountID & "OB").ElementAt(0).Value.Value2
        ComputeBS(AccountID)

        AccountsListHT.Item(AccountID)(1) = 1

    End Sub

    Private Sub ComputeBS(account As String)

        '-----------------------------------------------------------------------
        ' 
        '----------------------------------------------------------------------
        Dim TempStrFormula As String
        Dim j, OperatorIndex, OperandIndex As Integer

        For j = LBound(PeriodList) + 1 To UBound(PeriodList)                 ' Loop through periods
            TempStrFormula = "="
            OperatorIndex = 0
            OperandIndex = 0
            For Each flag As String In FormulaFlag                       ' Loop through Accounts
                If flag = OPERANDS_FLAG Then
                    TempStrFormula = TempStrFormula & ModelDictionary.Item(OperandsList(OperandIndex)).ElementAt(j).Value.Address
                    OperandIndex = OperandIndex + 1
                Else
                    TempStrFormula = TempStrFormula & OperatorsList(OperatorIndex)
                    OperatorIndex = OperatorIndex + 1
                End If
            Next
            TempStrFormula = TempStrFormula & "+" & ModelDictionary.Item(account).ElementAt(j - 1).Value.Address
            TempStrFormula = Replace(TempStrFormula, ",", ".")
            ModelDictionary.Item(account).ElementAt(j).Value.Formula = TempStrFormula
        Next j

        OperandsList.Clear()
        OperatorsList.Clear()
        FormulaFlag.Clear()

    End Sub


    ' Get all data functions 
    'Public Sub GetAllData(Affiliates() As String, Periods() As Integer)

    '    '------------------------------------------------------------------
    '    ' Build a data table including all values for all assets included 
    '    ' in affiliates' array 
    '    '------------------------------------------------------------------
    '    AllDataTable = BuildDownloadDataTable()                                 ' Initialize the complete DataTable
    '    Dim HVInputsList() As String                                            ' Initialize Hard value inputs list
    '    Dim AssetsList As Collections.Generic.List(Of String)                   ' Initialize Assets Collection
    '    AssetsList = GetAssetsList(Affiliates)                                  ' Get the assets list
    '    HVInputsList = mapp.getHVInputs                                         ' Get the list of HV accounts
    '    Call PeriodListBuild(Periods(UBound(Periods)))                          ' Build overall period list

    '    For Each asset As String In AssetsList
    '        Dim AssetArray() As String = {asset}                                ' Produce an array of 1 asset
    '        dataHT = New Collections.Generic.Dictionary(Of String, Double())    ' Initialize the item dataHT
    '        CreateAggregDataHT(AssetArray, HVInputsList)                        ' Fill dataHT with HV inputs
    '        Call CalcAlgo()                                                     ' Complete dataHT with all calculations
    '        FillDownloadDataTable(asset)                                        ' add all dataHT.items to datatable
    '    Next


    'End Sub

    'Private Function GetAssetsList(Affiliates) As Collections.Generic.List(Of String)

    '    '--------------------------------------------------------------------------
    '    ' Provide a List of all assets included in affiliates array
    '    '--------------------------------------------------------------------------
    '    Dim AssetsCollection As New Collections.Generic.List(Of String)
    '    For Each affiliate As String In Affiliates
    '        For Each asset As String In mapp.FindAssetChildren(affiliate)
    '            AssetsCollection.Add(asset)
    '        Next
    '    Next
    '    GetAssetsList = AssetsCollection
    'End Function


    'Private Sub FillDownloadDataTable(Asset As String)

    '    '--------------------------------------------------------------------------------------
    '    ' Add rows to the download datatable corresponding to the asset contained in HT
    '    '--------------------------------------------------------------------------------------
    '    For Each account As String In dataHT.Keys                               ' Loop through Accounts (keys)
    '        Dim period As Integer = OpeningPeriod
    '        For Each value As Double In dataHT.Item(account)                    ' Loop through periods/values (element of the array)
    '            Dim tuple() As Object = {Asset, account, period, value}
    '            AllDataTable.Rows.Add(tuple)
    '            period = period + 1
    '        Next
    '    Next

    'End Sub



End Class
