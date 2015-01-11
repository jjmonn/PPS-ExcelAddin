Imports Microsoft.Office.Interop
Imports System.Linq

Public Class ModelDataSet


    '--------------------------------------------------------------------------------------------------
    ' This class manages the data sets to be uploaded and downloaded to and from the server
    '   - Accounts Array (dimmension 1: values , dimension 2: Human formulas, dimension 3: Format)
    '   
    '
    '-------------------------------------------------------------------------------------------------

    'Private instance variables
    Private WS As Excel.Worksheet
    Private GlobalScreenShotFlag(,) As String       ' Flag array for the entire worksheet
    Private GlobalScreenShot(,) As Object           ' Worksheet values array
    Private lastCell As Excel.Range                 ' Last used cell in the worksheet
    Private Mapp As ModelMapping                    ' Model mapping instance
    Private globalFlag As String                    ' Aggregation of the three flags (accounts, assets/ products, periods)
    Private accountRefs() As String                 ' String array for account references
    Private assetsList() As String                  ' String array containing the list of assets

    'Properties
    Public Property pDataSet As Object(,)           ' Array containing the data
    Public Property pAddressDatesList As New Collections.Generic.Dictionary(Of String, String)
    Public Property pAddressAccountsList As New Collections.Generic.Dictionary(Of String, String)
    Public Property pAddressAssetsList As New Collections.Generic.Dictionary(Of String, String)


    'Public Property pDataRange As Excel.Range      ' Range of the dataset
    Public Property pDataArray As Double(,)         ' Hold row data

    'Properties Flag
    Public Property pDateFlag As Integer             ' Dates Flag: 0 = no period found; 1 = 1 period found ; 2 = period array found ; 3=worksheet name
    Public Property pAccountFlag As Integer          ' Accounts Flag: 0 = no account found; 1 = 1 account found ; 2 = account array found
    Public Property pAssetFlag As Integer            ' Asset Flag: 0 = no asset found; 1 = 1 asset found ; 2 = asset array found
    Public Property pAssetAddressValueFlag As String '
    Public Property pAccountAddressValueFlag As String
    Public Property pDateAddressValueFlag As String
    Public Property datesOrientation As String
    Public Property accountsOrientation As String
    Public Property assetsOrientation As String


    Public Sub New()

        WS = apps.ActiveSheet
        Dim cell2 As Excel.Range = GetRealLastCell(WS)  'import function here ? !!

        If IsNothing(cell2) Then
            MsgBox("The worksheet is empty")
            Exit Sub
        End If
        GlobalScreenShot = WS.Range(WS.Cells(1, 1), cell2).Value
        ReDim GlobalScreenShotFlag(UBound(GlobalScreenShot, 1), UBound(GlobalScreenShot, 2))

        Mapp = New ModelMapping
        accountRefs = Mapp.GetAccountReferences

        DatesIdentify()
        AccountsIdentify()
        AssetsIdentify()

        If pAddressDatesList.Count - 1 > 0 Then removeDuplicatesDates()
        If pAddressAccountsList.Count - 1 > 0 Then removeDuplicatesAccounts()
        If pAddressAssetsList.Count - 1 > 0 Then removeDuplicatesAssets()

        Erase GlobalScreenShot                                                      ' Remove global screenshots from memory
        Erase GlobalScreenShotFlag

        pAssetAddressValueFlag = ADDRESS_FLAG                                       ' Initialize flags to addresses
        pAccountAddressValueFlag = ADDRESS_FLAG
        pDateAddressValueFlag = ADDRESS_FLAG

    End Sub

    Private Sub DatesIdentify()

        '---------------------------------------------------------------
        ' Look for date in the spreasheet, returns true if it founds any
        ' and populate pAddressDatesList
        '---------------------------------------------------------------
        Dim datesSearchRange As New Collections.Generic.List(Of String)
        Dim i, j As Integer

        ' Define dates range
        For i = Year(DateValue(Now)) - EPSILON_MINUS To Year(DateValue(Now)) + EPSILON_PLUS
            datesSearchRange.Add(i)
        Next

        ' global screenshot pourrait être une hashtable ou un Excel.range tout simplement - address/value !
        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)
                If datesSearchRange.Contains(GlobalScreenShot(i, j)) Then
                    pAddressDatesList.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, GlobalScreenShot(i, j))
                    GlobalScreenShotFlag(i, j) = "period"
                End If
            Next j
        Next i

        ' Flag
        Select Case pAddressDatesList.Count
            Case 0 : pDateFlag = 0                                                            ' No period found
            Case 1                                                                       ' One period found
                pDateFlag = 1
                pDateAddressValueFlag = ADDRESS_FLAG
            Case Else                                                                    ' Periods Array found
                pDateFlag = 2
                pDateAddressValueFlag = ADDRESS_FLAG
        End Select

    End Sub

    Private Sub AccountsIdentify()

        '---------------------------------------------------------------
        ' Identify the mapped accounts in the WS
        ' Possibility to use the Accounts Search Algo
        '---------------------------------------------------------------
        Dim i, j, nbWords As Integer
        Dim AccountsHVList As Collections.Generic.List(Of String)
        AccountsHVList = Mapp.getHVNameList              ' Get HV ID Accounts list

        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)                                        ' Loop into rows of input array
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)                                      ' Loop into columns of input array
                If VarType(GlobalScreenShot(i, j)) = 8 Then

                    ' Direct match or algo > 50% Index à vérifier selon table
                    ' Trim left and right the cell.value2 !! -> To be tested

                    If AccountsHVList.Contains(CStr(GlobalScreenShot(i, j))) Then       ' Currently looking into HV inputs list

                        pAddressAccountsList.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                 CStr(GlobalScreenShot(i, j)))                                                                 ' Save coordinates
                        GlobalScreenShotFlag(i, j) = "Account"

                        '         ElseIf AccountSearchAlgo(GlobalScreenShot(i, j)) = True Then           'Repeated procedure but faster...
                        '
                        '            pAddressAccountsList.add(Split(Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, CStr(GlobalScreenShot(i, j)))
                        '            GlobalScreenShotFlag(i, j) = "Account"
                    End If

                    nbWords = Len(GlobalScreenShot(i, j)) - Len(Replace(GlobalScreenShot(i, j), " ", "")) + 1

                    If nbWords <= 3 Then
                        GlobalScreenShotFlag(i, j) = "ShortString"                                        ' Flag for assets lookup
                    Else
                        GlobalScreenShotFlag(i, j) = "LongString"
                    End If

                End If
            Next j
        Next i

        Select Case pAddressAccountsList.Count
            Case 0 : pAccountFlag = 0
            Case 1
                pAccountFlag = 1
            Case Else
                pAccountFlag = 2
        End Select

    End Sub

    'Private Function AccountSearchAlgo(inputValue As Object) As Boolean

    '----------------------------------------------------------------
    ' Recognizes whether a cell value is an account 
    '----------------------------------------------------------------

    'Dim WordsArray() As String
    'Dim i,matchIndex,Result As Integer
    'Dim matches, nbWords As Double
    'Dim inputStr As String
    'inputStr = CStr(inputValue)

    '' Split inputStr, clean and count words
    'WordsArray = Split(inputStr)                                          ' Split the account into words
    'For i = LBound(WordsArray) To UBound(WordsArray)
    '    WordsArray(i) = TrimAny(WordsArray(i), CHARL)                       ' Excludes begining and end special characters
    '    ' Here need to adapt TRIM FUNCTION in utilities function module
    'Next i

    ''WordsArray = StringClean(WordsArray)                                 ' Clean words (exclude special characters)
    'nbWords = UBound(WordsArray) - LBound(WordsArray) + 1

    '' For each word : check isInArray(references)
    'matchIndex = 0
    'For i = LBound(WordsArray) To UBound(WordsArray)
    '    On Error Resume Next
    '    matchIndex = WS.Application.WorksheetFunction.Match(WordsArray(i), accountRefs, 0)
    '    If Err.Number > 0 Then matchIndex = 0
    '    If matchIndex > 0 Then matches = matches + 1
    'Next i

    '' Return true if score >= threshold
    'If matches / nbWords >= ACCOUNTS_SEARCH_ALGO_THRESHOLD Then Result = 1

    'AccountSearchAlgo = Result

    'End Function

    Private Sub AssetsIdentify()

        '---------------------------------------------------------------
        ' Identify the assets from mapping
        ' Uses the AssetsSearch Algorithm (>leveinstein Threshold%)
        '---------------------------------------------------------------
        Dim i, j As Integer

        assetsList = Mapp.NameList("assets")                      ' Get Assets List
        ' -> Unefficient !! ?

        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)

                Select Case GlobalScreenShotFlag(i, j)
                    Case "ShortString"
                        If lookUpArray(CStr(GlobalScreenShot(i, j)), assetsList) > 0 Then
                            pAddressAssetsList.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                   CStr(GlobalScreenShot(i, j)))
                        ElseIf AssetResearchAlgo(GlobalScreenShot(i, j)) Then
                            pAddressAssetsList.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                   CStr(GlobalScreenShot(i, j)))
                        End If
                End Select
            Next j
        Next i

        Select Case pAddressAssetsList.Count
            Case 0 : pAssetFlag = 0
            Case 1
                pAssetFlag = 1
                pAssetAddressValueFlag = ADDRESS_FLAG
            Case Else : pAssetFlag = 2
                pAssetAddressValueFlag = ADDRESS_FLAG
        End Select

    End Sub

    Private Function AssetResearchAlgo(str As Object) As Boolean

        '---------------------------------------------------------------
        ' Returns true if a the input exceeds the Levenstein thresold
        ' (Close to a mapped asset)
        '---------------------------------------------------------------

        Dim i, Delta As Integer
        Dim P As Double

        AssetResearchAlgo = False

        For i = LBound(assetsList) To UBound(assetsList)
            Delta = Levenshtein(CStr(str), CStr(assetsList(i)))
            P = Delta / Len(assetsList(i))
            If P <= LEVENSTEIN_THRESOLHD Then
                AssetResearchAlgo = True
                Exit Function
            End If
        Next i

    End Function

    'Private Sub removeDuplicates(str As String)

    '    '---------------------------------------------------------------------
    '    ' Remove duplicates value in input arrays
    '    '---------------------------------------------------------------------

    '    Dim i, j, Flag, NewIndex As Integer
    '    Dim TempArray(0), tempValues(0), inputArray() As Object

    '    Select Case str
    '        Case "dates" : inputArray = pAddressDatesList
    '        Case "accounts" : inputArray = pAddressAccountsList
    '        Case "assets" : inputArray = pAddressAssetsList
    '        Case Else : inputArray = {""}
    '    End Select

    '    For i = LBound(inputArray) To UBound(inputArray)
    '        Flag = 0
    '        For j = LBound(TempArray) To UBound(TempArray) - 1
    '            If lookUpArray(WS.Range(inputArray(i)), tempValues) > 0 Then Flag = Flag + 1
    '        Next j
    '        If Flag = 0 Then
    '            TempArray(NewIndex) = inputArray(i)
    '            tempValues(NewIndex) = WS.Range(inputArray(i))
    '            ReDim Preserve TempArray(UBound(TempArray) + 1)
    '            ReDim Preserve tempValues(UBound(tempValues) + 1)
    '            NewIndex = NewIndex + 1
    '        End If
    '    Next i

    '    ReDim Preserve TempArray(NewIndex - 1)

    '    Select Case str
    '        Case "dates" : pAddressDatesList = TempArray
    '        Case "accounts" : pAddressAccountsList = TempArray
    '        Case "assets" : pAddressAssetsList = TempArray
    '    End Select

    'End Sub

    Private Sub removeDuplicatesDates()

        '---------------------------------------------------------------------
        ' Remove duplicates value in the Dates List
        '---------------------------------------------------------------------
        Dim TempHT As New Collections.Generic.Dictionary(Of String, String)
        For Each key As String In pAddressDatesList.Keys
            If Not TempHT.ContainsValue(pAddressDatesList.Item(key)) Then
                TempHT.Add(key, pAddressDatesList.Item(key))
            End If
        Next
        pAddressDatesList = TempHT

    End Sub

    Private Sub removeDuplicatesAssets()

        '---------------------------------------------------------------------
        ' Remove duplicates value in the Dates List
        '---------------------------------------------------------------------
        Dim TempHT As New Collections.Generic.Dictionary(Of String, String)
        For Each key As String In pAddressAssetsList.Keys
            If Not TempHT.ContainsValue(pAddressAssetsList.Item(key)) Then
                TempHT.Add(key, pAddressAssetsList.Item(key))
            End If
        Next
        pAddressAssetsList = TempHT

    End Sub

    Private Sub removeDuplicatesAccounts()

        '---------------------------------------------------------------------
        ' Remove duplicates value in the Accounts List
        '---------------------------------------------------------------------
        Dim TempHT As New Collections.Generic.Dictionary(Of String, String)
        For Each key As String In pAddressAccountsList.Keys
            If Not TempHT.ContainsValue(pAddressAccountsList.Item(key)) Then
                TempHT.Add(key, pAddressAccountsList.Item(key))
            End If
        Next
        pAddressAccountsList = TempHT

    End Sub


    Private Sub redimFlags()

        '-----------------------------------------------------------------
        ' Flag redimension: in which case ?
        '-----------------------------------------------------------------

        ' Redim Asset flags   
        Select Case pAddressAssetsList.Count
            Case 0 : pAssetFlag = 0
            Case 1 : pAssetFlag = 1
            Case Else : pAssetFlag = 2
        End Select

        ' Redim Accounts flag
        Select Case pAddressAccountsList.Count
            Case 0 : pAccountFlag = 0
            Case 1 : pAccountFlag = 1
            Case Else : pAccountFlag = 2
        End Select

        Select Case pAddressDatesList.Count
            Case 0 : pDateFlag = 0
            Case 1 : pDateFlag = 1
            Case Else : pDateFlag = 2
        End Select

    End Sub

    Public Sub getDataSet()

        '-----------------------------------------------------------------------------
        ' Computes which parameter to send to the GetData() function
        ' Choose which arrays to send and send the vertical array fisrt
        ' According to the flags and orientation 
        '-----------------------------------------------------------------------------
        getOrientations()                                                   ' Computes the ranges orientation
        If accountsOrientation = "V" And assetsOrientation = "H" Then       ' Case Accounts in line / Assets in columns
            getData(pAddressAccountsList, pAddressAssetsList, "acVas")
        ElseIf accountsOrientation = "V" And datesOrientation = "H" Then    ' Case Accounts in line / Dates in columns
            getData(pAddressAccountsList, pAddressDatesList, "acVda")
        ElseIf datesOrientation = "V" And accountsOrientation = "H" Then    ' Case Dates in line / Accounts in columns
            getData(pAddressDatesList, pAddressAccountsList, "daVac")
        ElseIf datesOrientation = "V" And assetsOrientation = "H" Then      ' Case Dates in line / Assets in columns
            getData(pAddressDatesList, pAddressAssetsList, "daVas")
        ElseIf assetsOrientation = "V" And accountsOrientation = "H" Then   ' Case Assets in line / Accounts in columns
            getData(pAddressAssetsList, pAddressAccountsList, "asVac")
        ElseIf assetsOrientation = "V" And datesOrientation = "H" Then      ' Case Assets in line / Dates in columns
            getData(pAddressAssetsList, pAddressDatesList, "asVda")
        End If
    End Sub

    Private Sub getOrientations()

        '-------------------------------------------------------------------------
        ' Get worksheet orientations of the ranges
        ' ------------------------------------------------------------------------

        Dim MaxRight As String = ""
        Dim MaxBelow As String = ""
        Dim FlagsCode As String = CStr(pDateFlag) & CStr(pAccountFlag) & CStr(pAssetFlag)

        Select Case FlagsCode
            Case "111"          ' Only one value
                GetMaxs(MaxRight, MaxBelow)
                Dim MaxsFlag As String = MaxRight & MaxBelow

                'If MaxRight = MaxBelow then '-> Interface New!

                Select Case MaxsFlag
                    Case "AssetAccount"
                        assetsOrientation = "H"
                        accountsOrientation = "V"
                    Case "DateAccount"
                        datesOrientation = "H"
                        accountsOrientation = "V"
                    Case "AssetDate"
                        assetsOrientation = "H"
                        datesOrientation = "V"
                    Case "AccountAsset"
                        accountsOrientation = "H"
                        assetsOrientation = "V"
                    Case "AccountDate"
                        accountsOrientation = "H"
                        datesOrientation = "V"
                    Case "DateAsset"
                        datesOrientation = "H"
                        assetsOrientation = "V"
                    Case Else
                        ' Interface New ! 
                End Select
            Case "112"                          ' 1 period, 1 Account, Several assets
                getAssetsOrientations()
                If assetsOrientation = "H" Then
                    If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Row > WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row Then
                        datesOrientation = "V"
                    Else
                        accountsOrientation = "V"
                    End If
                Else
                    If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Column > WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Column Then
                        datesOrientation = "H"
                    Else
                        accountsOrientation = "H"
                    End If
                End If
            Case "121"                          ' 1 period, Several Accounts, 1 asset
                getAccountsOrientations()
                If accountsOrientation = "H" Then
                    If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Row > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row Then
                        datesOrientation = "V"
                    Else
                        assetsOrientation = "V"
                    End If
                Else
                    If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Column > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Column Then
                        datesOrientation = "H"
                    Else
                        assetsOrientation = "H"
                    End If
                End If
            Case "211"                          ' Several periods, 1 Account, 1 Asset
                getDatesOrientations()
                If datesOrientation = "H" Then
                    If WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row Then
                        accountsOrientation = "V"
                    Else
                        assetsOrientation = "V"
                    End If
                Else
                    If WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Column > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Column Then
                        accountsOrientation = "H"
                    Else
                        assetsOrientation = "H"
                    End If
                End If
            Case "311"                          ' WS Period, 1 Account, 1 Asset
                If WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row Then
                    accountsOrientation = "V"
                    assetsOrientation = "H"
                ElseIf WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row < WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row Then
                    accountsOrientation = "H"
                    assetsOrientation = "V"
                ElseIf WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Column > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Column Then
                    accountsOrientation = "H"
                    assetsOrientation = "V"
                Else
                    accountsOrientation = "V"
                    assetsOrientation = "H"
                End If
            Case "312"                          ' WS Period, 1 Account, Several Asset
                getAssetsOrientations()
                If assetsOrientation = "H" Then
                    accountsOrientation = "V"
                Else
                    accountsOrientation = "H"
                End If
            Case "321"                          ' WS Period, Several Accounts, 1 Asset
                getAccountsOrientations()
                If accountsOrientation = "H" Then
                    assetsOrientation = "V"
                Else
                    assetsOrientation = "H"
                End If
            Case Else
                ' Case "221","212","122","222","322"
                getDatesOrientations()                       ' Dates Orientation
                getAccountsOrientations()                    ' Accounts orientation
                getAssetsOrientations()                      ' Assets orientation
        End Select
    End Sub

    Private Sub GetMaxs(ByRef MaxRight As String, ByRef MaxBelow As String)

        '--------------------------------------------------------------------------
        ' Define the cells at the most right and the most bottom
        '--------------------------------------------------------------------------
        ' Get Maximum Right cell
        If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Column > WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Column Then
            If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Column > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Column Then
                MaxRight = "Date"
            Else
                MaxRight = "Asset"
            End If
        Else
            If WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Column > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Column Then
                MaxRight = "Account"
            Else
                MaxRight = "Asset"
            End If
        End If
        ' Get Maximum Below cell
        If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Row > WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row Then
            If WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Row > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row Then
                MaxBelow = "Date"
            Else
                MaxBelow = "Asset"
            End If
        Else
            If WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row > WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row Then
                MaxBelow = "Account"
            Else
                MaxBelow = "Asset"
            End If
        End If
    End Sub

    Private Sub getDatesOrientations()

        '-------------------------------------------------------------------
        ' Compute whether the Dates range is Vertical or Horizontal
        '-------------------------------------------------------------------
        Dim deltaRows, deltaColumns As Integer
        If pDateFlag = 2 Then
            deltaRows = WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Row - _
                                    WS.Range(pAddressDatesList.ElementAt(0).Key).Row

            deltaColumns = WS.Range(pAddressDatesList.ElementAt(pAddressDatesList.Count - 1).Key).Column - _
                           WS.Range(pAddressDatesList.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                datesOrientation = "V"
            Else
                datesOrientation = "H"
            End If
        End If

    End Sub

    Private Sub getAccountsOrientations()

        '-------------------------------------------------------------------
        ' Compute whether the Accounts range is Vertical or Horizontal
        '-------------------------------------------------------------------
        Dim deltaRows, deltaColumns As Integer
        If pAccountFlag = 2 Then
            deltaRows = WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Row - _
                        WS.Range(pAddressAccountsList.ElementAt(0).Key).Row

            deltaColumns = WS.Range(pAddressAccountsList.ElementAt(pAddressAccountsList.Count - 1).Key).Column - _
                           WS.Range(pAddressAccountsList.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                accountsOrientation = "V"
            Else
                accountsOrientation = "H"
            End If
        End If

    End Sub

    Private Sub getAssetsOrientations()
        '-------------------------------------------------------------------
        ' Compute whether the Assets range is Vertical or Horizontal
        '-------------------------------------------------------------------
        Dim deltaRows, deltaColumns As Integer
        If pAssetFlag = 2 Then
            deltaRows = WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Row - _
                        WS.Range(pAddressAssetsList.ElementAt(0).Key).Row

            deltaColumns = WS.Range(pAddressAssetsList.ElementAt(pAddressAssetsList.Count - 1).Key).Column - _
                           WS.Range(pAddressAssetsList.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                assetsOrientation = "V"
            Else
                assetsOrientation = "H"
            End If
        End If

    End Sub

    Public Sub getData(array_1 As Collections.Generic.Dictionary(Of String, String), _
                        array_2 As Collections.Generic.Dictionary(Of String, String), code As String)

        '-----------------------------------------------------------------------------
        ' Save the data from WS to pDataArray according to the input orientation code
        ' and 2 inputs pAddressAccountsList, pAddressAssetsList or pAddressDatesList
        '-----------------------------------------------------------------------------

        Dim i As Integer, j As Integer, Index As Integer
        Dim Data As Double
        Index = -1
        ReDim pDataSet((array_1.Count - 1 + 1) * (array_2.Count - 1 + 1) - 1, 3)
        ReDim pDataArray(array_1.Count - 1, array_2.Count - 1)

        Select Case code
            Case "acVda"                                                           ' Lines : Accounts | Columns : Date
                For Each Key1 As String In array_1.Keys
                    j = 0
                    For Each key2 As String In array_2.Keys
                        Data = WS.Cells(WS.Range(Key1).Row, WS.Range(key2).Column).value2
                        pDataArray(i, j) = Data
                        Index = Index + 1
                        pDataSet(Index, DATA_ARRAY_ACCOUNT_COLUMN) = array_1.Item(Key1)                    ' Account value
                        pDataSet(Index, DATA_ARRAY_PERIOD_COLUMN) = array_2.Item(key2)                     ' Period value
                        pDataSet(Index, DATA_ARRAY_DATA_COLUMN) = Data                                     ' Save the value
                        pDataSet(Index, DATA_ARRAY_ASSET_COLUMN) = pAddressAssetsList.ElementAt(0).Value   ' Asset Value (by value)
                        j = j + 1
                    Next
                    i = i + 1
                Next
            Case "daVac"                                                           ' Lines : Dates | Columns : Accounts
                For Each key1 As String In array_1.Keys
                    j = 0
                    For Each key2 As String In array_2.Keys
                        Data = WS.Cells(WS.Range(key1).Row, WS.Range(key2).Column).value2
                        pDataArray(i, j) = Data
                        Index = Index + 1
                        pDataSet(Index, DATA_ARRAY_ACCOUNT_COLUMN) = array_2.Item(key2)                    ' Account value
                        pDataSet(Index, DATA_ARRAY_PERIOD_COLUMN) = array_1.Item(key1)                     ' Period value
                        pDataSet(Index, DATA_ARRAY_DATA_COLUMN) = Data                                     ' Save the value
                        pDataSet(Index, DATA_ARRAY_ASSET_COLUMN) = pAddressAssetsList.ElementAt(0).Value   ' Asset Value (by address)
                        j = j + 1
                    Next
                    i = i + 1
                Next
            Case "acVas"                                                           ' Lines : Accounts | Columns : Assets
                For Each key1 As String In array_1.Keys
                    j = 0
                    For Each key2 As String In array_2.Keys
                        Data = WS.Cells(WS.Range(key1).Row, WS.Range(key2).Column).value2
                        pDataArray(i, j) = Data
                        Index = Index + 1
                        pDataSet(Index, DATA_ARRAY_ACCOUNT_COLUMN) = array_1.Item(key1)                   ' Account value
                        pDataSet(Index, DATA_ARRAY_ASSET_COLUMN) = array_2.Item(key2)                     ' Period value
                        pDataSet(Index, DATA_ARRAY_DATA_COLUMN) = Data                                    ' Save the value
                        pDataSet(Index, DATA_ARRAY_PERIOD_COLUMN) = pAddressDatesList.ElementAt(0).Value  ' Asset Value (by value)
                        j = j + 1
                    Next
                    i = i + 1
                Next
            Case "asVac"                                                           ' Lines : Assets | Columns : Accounts
                For Each key1 As String In array_1.Keys
                    j = 0
                    For Each key2 As String In array_2.Keys
                        Data = WS.Cells(WS.Range(key1).Row, WS.Range(key2).Column).value2
                        pDataArray(i, j) = Data
                        Index = Index + 1
                        pDataSet(Index, DATA_ARRAY_ASSET_COLUMN) = array_1.Item(key1)                     ' Account value
                        pDataSet(Index, DATA_ARRAY_ACCOUNT_COLUMN) = array_2.Item(key2)                   ' Period value
                        pDataSet(Index, DATA_ARRAY_DATA_COLUMN) = Data                                    ' Save the value
                        pDataSet(Index, DATA_ARRAY_PERIOD_COLUMN) = pAddressDatesList.ElementAt(0).Value  ' Asset Value (by value)
                        j = j + 1
                    Next
                    i = i + 1
                Next
            Case "daVas"                                                           ' Lines : Dates | Columns : Assets
                For Each key1 As String In array_1.Keys
                    j = 0
                    For Each key2 As String In array_2.Keys
                        Data = WS.Cells(WS.Range(key1).Row, WS.Range(key2).Column).value2
                        pDataArray(i, j) = Data
                        Index = Index + 1
                        pDataSet(Index, DATA_ARRAY_PERIOD_COLUMN) = array_1.Item(key1)                        ' Account value
                        pDataSet(Index, DATA_ARRAY_ASSET_COLUMN) = array_2.Item(key2)                         ' Period value
                        pDataSet(Index, DATA_ARRAY_DATA_COLUMN) = Data                                        ' Save the value
                        pDataSet(Index, DATA_ARRAY_ACCOUNT_COLUMN) = pAddressAccountsList.ElementAt(0).Value  ' Asset Value (by value)
                        j = j + 1
                    Next
                    i = i + 1
                Next
            Case "asVda"                                                           ' Lines : Assets | Columns : Dates
                For Each key1 As String In array_1.Keys
                    j = 0
                    For Each key2 As String In array_2.Keys
                        Data = WS.Cells(WS.Range(key1).Row, WS.Range(key2).Column).value2
                        pDataArray(i, j) = Data
                        Index = Index + 1
                        pDataSet(Index, DATA_ARRAY_ASSET_COLUMN) = array_1.Item(key1)                         ' Account value
                        pDataSet(Index, DATA_ARRAY_PERIOD_COLUMN) = array_2.Item(key2)                        ' Period value
                        pDataSet(Index, DATA_ARRAY_DATA_COLUMN) = Data                                        ' Save the value
                        pDataSet(Index, DATA_ARRAY_ACCOUNT_COLUMN) = pAddressAccountsList.ElementAt(0).Value  ' Asset Value (by value)
                        j = j + 1
                    Next
                    i = i + 1
                Next
        End Select

        ' pDataSet Redimension
        Dim TempArray(0 To Index, 0 To 3) As Object
        For i = LBound(pDataSet, 1) To Index
            TempArray(i, DATA_ARRAY_DATA_COLUMN) = pDataSet(i, DATA_ARRAY_DATA_COLUMN)
            TempArray(i, DATA_ARRAY_ACCOUNT_COLUMN) = pDataSet(i, DATA_ARRAY_ACCOUNT_COLUMN)
            TempArray(i, DATA_ARRAY_ASSET_COLUMN) = pDataSet(i, DATA_ARRAY_ASSET_COLUMN)
            TempArray(i, DATA_ARRAY_PERIOD_COLUMN) = pDataSet(i, DATA_ARRAY_PERIOD_COLUMN)
        Next i
        pDataSet = TempArray

    End Sub


End Class
