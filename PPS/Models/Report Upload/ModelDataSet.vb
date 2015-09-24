' ModelDataSet.vb
'
' This class manages the data sets to be uploaded and downloaded to and from the server
'   > Look for version (if not found take ribbon version)   
'   > Look for periods (period dates list according to version)
'   > Look for Entities and Accounts according to DB (possibility to use recognition algorithms)
'   
'   > Describe Get Data and write back processes
'
'
'
' to do: 
'       - Write method: implement version choice (currently using the VERSION BUTTON)
'       - Try to factor getDataSet
'       - swap GetData. v and h dicts
'
' Known bugs:
'       - Orientation: in some cases does not work (max left or right cells)
'       - if data = period -> bug
'
'
' Last modified: 23/09/2015
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Linq
Imports System.Collections.Generic
Imports System.Collections


Friend Class ModelDataSet


#Region "Instance Variables"

#Region "Objects"

    Friend WS As Excel.Worksheet
    Friend lastCell As Excel.Range

#End Region

#Region "Arrays"

    Private GlobalScreenShotFlag(,) As String                   ' Flag array for the entire worksheet
    Friend GlobalScreenShot(,) As Object                       ' Worksheet values array
    Private accountRefs() As String                             ' String array for account references
    '  Friend dataSetDictionary As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double)))

#End Region

#Region "Lists and Dictionaries"

    Friend m_entitiesNameList As List(Of String)                                    ' String array containing the list of assets
    Friend m_periodsDatesList As New List(Of Date)
    Friend m_inputsAccountsList As List(Of String)
    Friend m_entitiesNameIdDictionary As Hashtable
    Friend m_accountsNameIdDictionary As Hashtable

    ' Axes
    Friend periodsAddressValuesDictionary As New Dictionary(Of String, String)
    Friend AccountsAddressValuesDictionary As New Dictionary(Of String, String)
    Friend OutputsAccountsAddressvaluesDictionary As New Dictionary(Of String, String)
    Friend EntitiesAddressValuesDictionary As New Dictionary(Of String, String)
    Friend periodsValuesAddressDict As New Dictionary(Of String, String)
    Friend AccountsValuesAddressDict As New Dictionary(Of String, String)
    Friend OutputsValuesAddressDict As New Dictionary(Of String, String)
    Friend EntitiesValuesAddressDict As New Dictionary(Of String, String)

    ' DataSet Cells
    Friend m_datasetCellsDictionary As New Dictionary(Of Tuple(Of String, String, String), Excel.Range)
    Friend m_datasetCellDimensionsDictionary As New Dictionary(Of String, DataSetCellDimensions)
    Friend m_currentVersionId As Int32

#End Region

#Region "Flags"

    Friend pDateFlag As Integer             ' Dates Flag: 0 = no period found; 1 = 1 period found ; 2 = period array found ; 3=worksheet name
    Friend pAccountFlag As Integer          ' Accounts Flag: 0 = no account found; 1 = 1 account found ; 2 = account array found
    Friend pAssetFlag As Integer            ' Asset Flag: 0 = no asset found; 1 = 1 asset found ; 2 = asset array found
    Friend GlobalOrientationFlag As String  ' Aggregation of the three flags (accounts, assets/ products, periods)
    Friend datesOrientation As String
    Friend accountsOrientation As String
    Friend assetsOrientation As String

    Private Const string_flag As String = "String"
    Private Const account_flag As String = "Account"
    Private Const entity_flag As String = "Entity"
    Private Const period_flag As String = "Period"

#End Region

#Region "Constants"

    Private Const NULL_VALUE As String = "Not found"
    Public Const DATASET_ACCOUNTS_PERIODS_OR As String = "acVda"
    Public Const DATASET_PERIODS_ACCOUNTS_OR As String = "daVac"
    Public Const DATASET_ENTITIES_ACCOUNTS_OR As String = "asVac"
    Public Const DATASET_ACCOUNTS_ENTITIES_OR As String = "acVas"
    Public Const DATASET_PERIODS_ENTITIES_OR As String = "daVas"
    Public Const DATASET_ENTITIES_PERIODS_OR As String = "asVda"
    Public Const ENTITY_ITEM As String = "EntityItem"
    Public Const ACCOUNT_ITEM As String = "AccountItem"
    Public Const PERIOD_ITEM As String = "PeriodItem"

#End Region

    Structure DataSetCellDimensions

        Public m_accountName As String
        Public m_entityName As String
        Public m_period As String
        Public m_value As Double

    End Structure


#End Region


#Region "Initialize"

    Public Sub New(inputWS As Excel.Worksheet)

        Me.WS = inputWS
        m_entitiesNameIdDictionary = GlobalVariables.Entities.GetEntitiesDictionary(NAME_VARIABLE, ID_VARIABLE)
        m_accountsNameIdDictionary = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ID_VARIABLE)

    End Sub

#End Region


#Region " Interface"

    Friend Function WsScreenshot() As Boolean

        lastCell = Utilities_Functions.GetRealLastCell(WS)
        If IsNothing(lastCell) Then
            MsgBox("The worksheet is empty")
            GlobalScreenShot = Nothing
            Return False
        End If

        GlobalScreenShot = WS.Range(WS.Cells(1, 1), lastCell).Value
        ReDim GlobalScreenShotFlag(UBound(GlobalScreenShot, 1), UBound(GlobalScreenShot, 2))
        Return True

    End Function

    ' Lookup for Versions, Accounts, Periods and Entities
    Friend Sub SnapshotWS()

        AccountsAddressValuesDictionary.Clear()
        OutputsAccountsAddressvaluesDictionary.Clear()
        periodsAddressValuesDictionary.Clear()
        EntitiesAddressValuesDictionary.Clear()

        AccountsValuesAddressDict.Clear()
        OutputsValuesAddressDict.Clear()
        periodsValuesAddressDict.Clear()
        EntitiesValuesAddressDict.Clear()

        If Not VersionsIdentify() Then
            If GlobalVariables.Versions.IsVersionValid(My.Settings.version_id) = True Then
                m_currentVersionId = My.Settings.version_id
            Else
                MsgBox("A version must be selected or populated in the worksheet.")
                Dim versionSelectionUI As New VersionSelectionUI
                versionSelectionUI.Show()
                ' priority high !!
                ' idéal = UI version selection en background et doit être filled pour continuer
            End If
        End If

        DatesIdentify()
        AccountsIdentify()
        EntitiesIdentify()

    End Sub

    'Friend Sub GetDataSet()

    '    ' priority normal : adapt accountsPeriods get data method to ther orientation + simplify the dictionaries and model dataset storage process !! 
    '    Select Case GlobalOrientationFlag
    '        Case DATASET_ACCOUNTS_ENTITIES_OR : getData(AccountsAddressValuesDictionary, EntitiesAddressValuesDictionary)
    '        Case DATASET_ACCOUNTS_PERIODS_OR : GetDataOrientationAccountsPeriods()
    '        Case DATASET_PERIODS_ACCOUNTS_OR : getData(periodsAddressValuesDictionary, AccountsAddressValuesDictionary)
    '        Case DATASET_PERIODS_ENTITIES_OR : getData(periodsAddressValuesDictionary, EntitiesAddressValuesDictionary)
    '        Case DATASET_ENTITIES_ACCOUNTS_OR : getData(EntitiesAddressValuesDictionary, AccountsAddressValuesDictionary)
    '        Case DATASET_ENTITIES_PERIODS_OR : getData(EntitiesAddressValuesDictionary, periodsAddressValuesDictionary)
    '    End Select

    'End Sub

#End Region


#Region "Snapshot Functions"


#Region "Primary Recognition"

    Private Function VersionsIdentify() As Boolean

        Dim versionsNameList As List(Of String) = GlobalVariables.Versions.GetVersionsNameList(NAME_VARIABLE)
        Dim versionsNameCodeDictionary As Hashtable = GlobalVariables.Versions.GetVersionsDictionary(NAME_VARIABLE, ID_VARIABLE)

        Dim i, j As Integer
        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)
                If versionsNameList.Contains(CStr(GlobalScreenShot(i, j))) Then
                    m_currentVersionId = versionsNameCodeDictionary(CStr(GlobalScreenShot(i, j)))
                    ' ask confirmation first -> priority high
                    AddinModule.SetCurrentVersionId(m_currentVersionId)
                    Return True
                End If
            Next j
        Next i
        Return False

    End Function

    ' Look for date in the spreasheet, and populate periodsAddressValuesDictionary
    Private Sub DatesIdentify()

        m_periodsDatesList.Clear()
        For Each periodId As UInt32 In GlobalVariables.Versions.GetPeriodsList(m_currentVersionId)
            m_periodsDatesList.Add(Date.FromOADate(periodId))
        Next

        Dim i, j, periodStoredAsInt As Integer
        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)
                If IsDate(GlobalScreenShot(i, j)) Then
                    periodStoredAsInt = CInt(CDate((GlobalScreenShot(i, j))).ToOADate())
                    If m_periodsDatesList.Contains(CDate(GlobalScreenShot(i, j))) _
                    AndAlso Not periodsAddressValuesDictionary.ContainsValue(periodStoredAsInt) Then

                        periodsAddressValuesDictionary.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, periodStoredAsInt)
                        periodsValuesAddressDict.Add(periodStoredAsInt, Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)
                        GlobalScreenShotFlag(i, j) = period_flag

                    End If
                End If
            Next j
        Next i

        ' Flag
        Select Case periodsAddressValuesDictionary.Count
            Case 0 : pDateFlag = 0
            Case 1 : pDateFlag = 1
            Case Else : pDateFlag = 2
        End Select

    End Sub

    ' Identify the mapped accounts in the WS  | CURRENTLY NOT using the Accounts Search Algo
    Friend Sub AccountsIdentify()

        Dim i, j As Integer
        m_inputsAccountsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS, NAME_VARIABLE)
        Dim outputsAccountsList As List(Of String) = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS, NAME_VARIABLE)
        AccountsAddressValuesDictionary.Clear()

        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)                                          ' Loop into rows of input array
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)                                      ' Loop into columns of input array
                If VarType(GlobalScreenShot(i, j)) = 8 Then

                    Dim currentValue As String = CStr(GlobalScreenShot(i, j))

                    ' Direct match or algo > 50% Index à vérifier selon table
                    ' Trim left and right the cell.value2 !! -> To be tested
                    If m_inputsAccountsList.Contains(currentValue) _
                    AndAlso Not AccountsAddressValuesDictionary.ContainsValue(currentValue) Then

                        AccountsAddressValuesDictionary.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                            currentValue)
                        AccountsValuesAddressDict.Add(currentValue, Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)
                        GlobalScreenShotFlag(i, j) = account_flag

                    ElseIf outputsAccountsList.Contains(currentValue) _
                    AndAlso AccountsAddressValuesDictionary.ContainsValue(currentValue) = False Then

                        OutputsAccountsAddressvaluesDictionary.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                                   currentValue)
                        OutputsValuesAddressDict.Add(currentValue, Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)
                        GlobalScreenShotFlag(i, j) = account_flag

                        '         ElseIf AccountSearchAlgo(GlobalScreenShot(i, j)) = True Then           'Repeated procedure but faster...
                        '
                        '            AccountsAddressValuesDictionary.add(Split(Columns(j).Address(ColumnAbsolute:=False), ":")(1) + i, CStr(GlobalScreenShot(i, j)))
                        '            GlobalScreenShotFlag(i, j) = account_flag
                    End If
                    GlobalScreenShotFlag(i, j) = string_flag                                       ' Flag for assets lookup

                End If
            Next j
        Next i

        Select Case AccountsAddressValuesDictionary.Count
            Case 0 : pAccountFlag = 0
            Case 1 : pAccountFlag = 1
            Case Else : pAccountFlag = 2
        End Select

    End Sub

    ' Identify the assets from mapping      | CURRENTLY NOT using the AssetsSearch Algorithm (>leveinstein Threshold%)
    Private Sub EntitiesIdentify()

        Dim i, j As Integer
        m_entitiesNameList = GlobalVariables.Entities.GetEntitiesList(NAME_VARIABLE)

        For i = LBound(GlobalScreenShot, 1) To UBound(GlobalScreenShot, 1)
            For j = LBound(GlobalScreenShot, 2) To UBound(GlobalScreenShot, 2)

                Select Case GlobalScreenShotFlag(i, j)
                    Case string_flag

                        If m_entitiesNameList.Contains(CStr(GlobalScreenShot(i, j))) _
                        AndAlso Not EntitiesAddressValuesDictionary.ContainsValue(CStr(GlobalScreenShot(i, j))) Then

                            EntitiesAddressValuesDictionary.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                                CStr(GlobalScreenShot(i, j)))
                            EntitiesValuesAddressDict.Add(CStr(GlobalScreenShot(i, j)), Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)

                            'ElseIf AssetResearchAlgo(GlobalScreenShot(i, j)) Then
                            '    EntitiesAddressValuesDictionary.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                            '                          CStr(GlobalScreenShot(i, j)))

                        End If
                End Select
            Next j
        Next i

        Select Case EntitiesAddressValuesDictionary.Count
            Case 0 : pAssetFlag = 0
            Case 1 : pAssetFlag = 1
            Case Else : pAssetFlag = 2
        End Select

    End Sub


#End Region


#Region "Recognition Algoritms"

    ' Recognizes whether a cell value is an account 
    Private Function AccountSearchAlgo(inputValue As Object) As Boolean

        'Dim WordsArray() As String
        'Dim i, matchIndex, Result As Integer
        'Dim matches, nbWords As Double
        'Dim inputStr As String
        'inputStr = CStr(inputValue)

        '' Split inputStr, clean and count words
        'WordsArray = Split(inputStr)                                          ' Split the account into words
        'For i = LBound(WordsArray) To UBound(WordsArray)
        '    WordsArray(i) = Trimany(WordsArray(i), CHARL)                       ' Excludes begining and end special characters
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

    End Function

    ' Returns true if a the input exceeds the Levenstein thresold  (Close to a mapped asset)
    Private Function AssetResearchAlgo(str As Object) As Boolean

        Dim i, Delta As Integer
        Dim P As Double

        AssetResearchAlgo = False

        For i = 0 To m_entitiesNameList.Count - 1
            Delta = Utilities_Functions.Levenshtein(CStr(str), CStr(m_entitiesNameList(i)))
            P = Delta / Len(m_entitiesNameList(i))
            If P <= 2 / Len(m_entitiesNameList(i)) Then
                AssetResearchAlgo = True
                Exit Function
            End If
        Next i

    End Function

#End Region


#Region "Dictionaries Values Update with current Worksheet"

    ' Launch the update of the pAddress dates, accounts and assets to  insert the values of the current WS
    Public Sub UpdateDictionariesValuesWithCurrentWS()

        UpdatePeriodDictionaryValuesWithCurrentWS()
        UpdateAccountsDictionaryValuesWithCurrentWS()
        UpdateEntitiesDictionaryValuesWithCurrentWS()

    End Sub

    ' Update the periodsAddressValuesDictionary with values of the current worksheet
    Private Sub UpdatePeriodDictionaryValuesWithCurrentWS()

        Dim key As String
        For i = 0 To periodsAddressValuesDictionary.Keys.Count - 1
            key = periodsAddressValuesDictionary.Keys(i)
            periodsAddressValuesDictionary.Item(key) = WS.Range(key).Value2
        Next

    End Sub

    ' Update the AccountsAddressValuesDictionary with values of the current worksheet
    Private Sub UpdateAccountsDictionaryValuesWithCurrentWS()

        Dim key As String
        For i = 0 To AccountsAddressValuesDictionary.Keys.Count - 1
            key = AccountsAddressValuesDictionary.Keys(i)
            AccountsAddressValuesDictionary.Item(key) = WS.Range(key).Value2
        Next

    End Sub

    ' Update the EntitiesAddressValuesDictionary with values of the current worksheet
    Private Sub UpdateEntitiesDictionaryValuesWithCurrentWS()

        Dim key As String
        For i = 0 To EntitiesAddressValuesDictionary.Keys.Count - 1
            key = EntitiesAddressValuesDictionary.Keys(i)
            EntitiesAddressValuesDictionary.Item(key) = WS.Range(key).Value2
        Next

    End Sub



#End Region


#End Region


#Region " Orientation Functions"

    ' Determines the worksheet orientations of the ranges
    Public Sub getOrientations()

        Dim MaxRight As String = ""
        Dim MaxBelow As String = ""
        Dim FlagsCode As String = CStr(pDateFlag) + CStr(pAccountFlag) + CStr(pAssetFlag)

        Select Case FlagsCode
            Case "111"          ' Only one value
                GetMaxs(MaxRight, MaxBelow)
                Dim MaxsFlag As String = MaxRight + MaxBelow

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
                    If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row Then
                        datesOrientation = "V"
                    Else
                        accountsOrientation = "V"
                    End If
                Else
                    If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Column Then
                        datesOrientation = "H"
                    Else
                        accountsOrientation = "H"
                    End If
                End If
            Case "121"                          ' 1 period, Several Accounts, 1 asset
                getAccountsOrientations()
                If accountsOrientation = "H" Then
                    If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row Then
                        datesOrientation = "V"
                    Else
                        assetsOrientation = "V"
                    End If
                Else
                    If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Column Then
                        datesOrientation = "H"
                    Else
                        assetsOrientation = "H"
                    End If
                End If
            Case "211"                          ' Several periods, 1 Account, 1 Asset
                getDatesOrientations()
                If datesOrientation = "H" Then
                    If WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row Then
                        accountsOrientation = "V"
                    Else
                        assetsOrientation = "V"
                    End If
                Else
                    If WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Column Then
                        accountsOrientation = "H"
                    Else
                        assetsOrientation = "H"
                    End If
                End If
            Case "311"                          ' WS Period, 1 Account, 1 Asset
                If WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row Then
                    accountsOrientation = "V"
                    assetsOrientation = "H"
                ElseIf WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row < WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row Then
                    accountsOrientation = "H"
                    assetsOrientation = "V"
                ElseIf WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Column Then
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

        DefineGlobalOrientationFlag()

    End Sub

    ' Define the cells at the most right and the most bottom
    Private Sub GetMaxs(ByRef MaxRight As String, ByRef MaxBelow As String)

        ' Get Maximum Right cell
        If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Column Then
            If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Column Then
                MaxRight = period_flag
            Else
                MaxRight = entity_flag
            End If
        Else
            If WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Column > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Column Then
                MaxRight = account_flag
            Else
                MaxRight = entity_flag
            End If
        End If
        ' Get Maximum Below cell
        If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row Then
            If WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row Then
                MaxBelow = period_flag
            Else
                MaxBelow = entity_flag
            End If
        Else
            If WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row > WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row Then
                MaxBelow = account_flag
            Else
                MaxBelow = entity_flag
            End If
        End If
    End Sub

    ' Compute whether the Dates range is Vertical or Horizontal
    Private Sub getDatesOrientations()

        Dim deltaRows, deltaColumns As Integer
        If pDateFlag = 2 Then
            deltaRows = WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Row - _
                                    WS.Range(periodsAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = WS.Range(periodsAddressValuesDictionary.ElementAt(periodsAddressValuesDictionary.Count - 1).Key).Column - _
                           WS.Range(periodsAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                datesOrientation = "V"
            Else
                datesOrientation = "H"
            End If
        End If

    End Sub

    ' Compute whether the Accounts range is Vertical or Horizontal
    Private Sub getAccountsOrientations()

        Dim deltaRows, deltaColumns As Integer
        If pAccountFlag = 2 Then
            deltaRows = WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Row - _
                        WS.Range(AccountsAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = WS.Range(AccountsAddressValuesDictionary.ElementAt(AccountsAddressValuesDictionary.Count - 1).Key).Column - _
                           WS.Range(AccountsAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                accountsOrientation = "V"
            Else
                accountsOrientation = "H"
            End If
        End If

    End Sub

    ' Compute whether the Assets range is Vertical or Horizontal
    Private Sub getAssetsOrientations()

        Dim deltaRows, deltaColumns As Integer
        If pAssetFlag = 2 Then
            deltaRows = WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Row - _
                        WS.Range(EntitiesAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = WS.Range(EntitiesAddressValuesDictionary.ElementAt(EntitiesAddressValuesDictionary.Count - 1).Key).Column - _
                           WS.Range(EntitiesAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                assetsOrientation = "V"
            Else
                assetsOrientation = "H"
            End If
        End If

    End Sub

    '
    Private Sub DefineGlobalOrientationFlag()

        If accountsOrientation = "V" And assetsOrientation = "H" Then       ' Case Accounts in line / Assets in columns
            GlobalOrientationFlag = DATASET_ACCOUNTS_ENTITIES_OR
        ElseIf accountsOrientation = "V" And datesOrientation = "H" Then    ' Case Accounts in line / Dates in columns
            GlobalOrientationFlag = DATASET_ACCOUNTS_PERIODS_OR
        ElseIf datesOrientation = "V" And accountsOrientation = "H" Then    ' Case Dates in line / Accounts in columns
            GlobalOrientationFlag = DATASET_PERIODS_ACCOUNTS_OR
        ElseIf datesOrientation = "V" And assetsOrientation = "H" Then      ' Case Dates in line / Assets in columns
            GlobalOrientationFlag = DATASET_PERIODS_ENTITIES_OR
        ElseIf assetsOrientation = "V" And accountsOrientation = "H" Then   ' Case Assets in line / Accounts in columns
            GlobalOrientationFlag = DATASET_ENTITIES_ACCOUNTS_OR
        ElseIf assetsOrientation = "V" And datesOrientation = "H" Then      ' Case Assets in line / Dates in columns
            GlobalOrientationFlag = DATASET_ENTITIES_PERIODS_OR
        Else
            GlobalOrientationFlag = ORIENTATION_ERROR_FLAG
        End If

    End Sub


#End Region


#Region "Dataset Cells Registering"

    Friend Sub RegisterDimensionsToCellDictionary()

        Dim entityName As String = EntitiesAddressValuesDictionary.ElementAt(0).Value
        Dim periodColumn As Int32
        Dim period As String

        Dim start_time As DateTime
        Dim elapsed_time As TimeSpan
        Dim stop_time As DateTime
        start_time = Now

        m_datasetCellsDictionary.Clear()
        m_datasetCellDimensionsDictionary.Clear()

        For Each PeriodAddressValuePair In periodsAddressValuesDictionary
            period = PeriodAddressValuePair.Value
            periodColumn = WS.Range(PeriodAddressValuePair.Key).Column

            ' Inputs cells registering
            For Each AccountAddressValuePair In AccountsAddressValuesDictionary
                RegisterDatasetCell(WS.Cells(WS.Range(AccountAddressValuePair.Key).Row, periodColumn), _
                                    entityName, _
                                    AccountAddressValuePair.Value, _
                                    period)
            Next

            ' Outputs cells registering
            For Each OutputAccountAddressValuePair In OutputsAccountsAddressvaluesDictionary
                RegisterDatasetCell(WS.Cells(WS.Range(OutputAccountAddressValuePair.Key).Row, periodColumn), _
                                    entityName, _
                                    OutputAccountAddressValuePair.Value, _
                                    period)
            Next
        Next

        stop_time = Now
        elapsed_time = stop_time.Subtract(start_time)
        System.Diagnostics.Debug.WriteLine(elapsed_time.TotalSeconds.ToString("0.000000"))

    End Sub

    Friend Sub RegisterDataSetCellsValues()

        Dim accountAddress As String
        Dim start_time As DateTime
        Dim elapsed_time As TimeSpan
        Dim stop_time As DateTime
        start_time = Now

        For Each AccountAddressValuePair In AccountsAddressValuesDictionary
            accountAddress = AccountAddressValuePair.Key
            For Each PeriodAddressValuePair In periodsAddressValuesDictionary
                Dim cell As Excel.Range = WS.Cells(WS.Range(accountAddress).Row, WS.Range(PeriodAddressValuePair.Key).Column)
                Dim datasetCell As DataSetCellDimensions = m_datasetCellDimensionsDictionary(cell.Address)
                datasetCell.m_value = cell.Value2
            Next
        Next
        stop_time = Now
        elapsed_time = stop_time.Subtract(start_time)
        System.Diagnostics.Debug.WriteLine(elapsed_time.TotalSeconds.ToString("0.000000"))

    End Sub

    'Private Sub GetData(VerticalDictionaryAddressValues As Dictionary(Of String, String), _
    '                    HorizontalDictionaryAddressValues As Dictionary(Of String, String))

    '    m_datasetCellsDictionary.Clear()
    '    Select Case GlobalOrientationFlag

    '        Case DATASET_ACCOUNTS_PERIODS_OR       ' Lines : Accounts | Columns : Date
    '            Dim accountsDictionary As New Dictionary(Of String, Dictionary(Of String, Double))
    '            For Each accountAddress As String In VerticalDictionaryAddressValues.Keys

    '                Dim account As String = VerticalDictionaryAddressValues.Item(accountAddress)
    '                Dim periodsDictionary As New Dictionary(Of String, Double)

    '                For Each periodAddress As String In HorizontalDictionaryAddressValues.Keys
    '                    Dim period = HorizontalDictionaryAddressValues.Item(periodAddress)
    '                    Dim cell As Excel.Range = WS.Cells(WS.Range(accountAddress).Row, WS.Range(periodAddress).Column)

    '                    ' Cell retreiving dictionaries Registering
    '                    RegisterCellAddressDimensions(cell, EntitiesAddressValuesDictionary.ElementAt(0).Value, account, period)
    '                    Dim tuple_ As New Tuple(Of String, String, String)(EntitiesAddressValuesDictionary.ElementAt(0).Value, account, period)
    '                    DimensionsToCellDictionary.Add(tuple_, cell)

    '                    periodsDictionary.Add(period, cell.Value2)
    '                Next
    '                accountsDictionary.Add(account, periodsDictionary)

    '            Next
    '            dataSetDictionary.Add(EntitiesAddressValuesDictionary.ElementAt(0).Value, accountsDictionary)

    '        Case DATASET_PERIODS_ACCOUNTS_OR                      ' Lines : Dates | Columns : Accounts
    '            Dim accountsDictionary As New Dictionary(Of String, Dictionary(Of String, Double))
    '            For Each accountAddress As String In HorizontalDictionaryAddressValues.Keys                       ' accounts
    '                Dim account As String = HorizontalDictionaryAddressValues.Item(accountAddress)

    '                Dim periodsDictionary As New Dictionary(Of String, Double)
    '                For Each periodAddress As String In VerticalDictionaryAddressValues.Keys                      ' periods
    '                    Dim period = HorizontalDictionaryAddressValues.Item(periodAddress)
    '                    Dim cell As Excel.Range = WS.Cells(WS.Range(periodAddress).Row, WS.Range(accountAddress).Column)
    '                    RegisterCellAddressDimensions(cell, EntitiesAddressValuesDictionary.ElementAt(0).Value, account, period)
    '                    periodsDictionary.Add(period, cell.Value2)
    '                Next
    '                accountsDictionary.Add(account, periodsDictionary)

    '            Next
    '            dataSetDictionary.Add(EntitiesAddressValuesDictionary.ElementAt(0).Value, accountsDictionary)


    '        Case DATASET_ACCOUNTS_ENTITIES_OR                     ' Lines : Accounts | Columns : Assets
    '            For Each entityAddress As String In HorizontalDictionaryAddressValues.Keys
    '                Dim entity As String = HorizontalDictionaryAddressValues.Item(entityAddress)
    '                Dim accountsDictionary As New Dictionary(Of String, Dictionary(Of String, Double))

    '                For Each accountAddress As String In VerticalDictionaryAddressValues.Keys
    '                    Dim account As String = VerticalDictionaryAddressValues.Item(accountAddress)
    '                    Dim periodsDictionary As New Dictionary(Of String, Double)
    '                    Dim cell As Excel.Range = WS.Cells(WS.Range(accountAddress).Row, WS.Range(entityAddress).Column)
    '                    RegisterCellAddressDimensions(cell, entity, account, periodsAddressValuesDictionary.ElementAt(0).Value)
    '                    periodsDictionary.Add(periodsAddressValuesDictionary.ElementAt(0).Value, cell.Value2)
    '                    accountsDictionary.Add(account, periodsDictionary)
    '                Next
    '                dataSetDictionary.Add(entity, accountsDictionary)
    '            Next


    '        Case DATASET_ENTITIES_ACCOUNTS_OR                ' Lines : Assets | Columns : Accounts

    '            For Each entityAddress As String In VerticalDictionaryAddressValues.Keys
    '                Dim entity As String = VerticalDictionaryAddressValues.Item(entityAddress)
    '                Dim accountsDictionary As New Dictionary(Of String, Dictionary(Of String, Double))
    '                For Each accountAddress As String In HorizontalDictionaryAddressValues.Keys
    '                    Dim account As String = HorizontalDictionaryAddressValues.Item(accountAddress)
    '                    Dim periodsDictionary As New Dictionary(Of String, Double)
    '                    Dim cell As Excel.Range = WS.Cells(WS.Range(entityAddress).Row, WS.Range(accountAddress).Column)
    '                    RegisterCellAddressDimensions(cell, entity, account, periodsAddressValuesDictionary.ElementAt(0).Value)
    '                    periodsDictionary.Add(periodsAddressValuesDictionary.ElementAt(0).Value, cell.Value2)
    '                    accountsDictionary.Add(account, periodsDictionary)
    '                Next
    '                dataSetDictionary.Add(entity, accountsDictionary)
    '            Next


    '        Case DATASET_PERIODS_ENTITIES_OR                  ' Lines : Dates | Columns : Assets

    '            For Each entityAddress As String In HorizontalDictionaryAddressValues.Keys
    '                Dim entity As String = HorizontalDictionaryAddressValues.Item(entityAddress)
    '                Dim accountsDictionary As New Dictionary(Of String, Dictionary(Of String, Double))
    '                Dim periodsDictionary As New Dictionary(Of String, Double)
    '                For Each periodAddress As String In VerticalDictionaryAddressValues.Keys
    '                    Dim period As String = VerticalDictionaryAddressValues.Item(periodAddress)
    '                    Dim cell As Excel.Range = WS.Cells(WS.Range(periodAddress).Row, WS.Range(entityAddress).Column)
    '                    RegisterCellAddressDimensions(cell, entity, AccountsAddressValuesDictionary.ElementAt(0).Value, period)
    '                    periodsDictionary.Add(period, cell.Value2)
    '                Next
    '                accountsDictionary.Add(AccountsAddressValuesDictionary.ElementAt(0).Value, periodsDictionary)
    '                dataSetDictionary.Add(entity, accountsDictionary)
    '            Next


    '        Case DATASET_ENTITIES_PERIODS_OR                ' Lines : Assets | Columns : Dates

    '            For Each entityAddress As String In VerticalDictionaryAddressValues.Keys
    '                Dim entity As String = VerticalDictionaryAddressValues.Item(entityAddress)
    '                Dim accountsDictionary As New Dictionary(Of String, Dictionary(Of String, Double))

    '                Dim periodsDictionary As New Dictionary(Of String, Double)
    '                For Each periodAddress As String In HorizontalDictionaryAddressValues.Keys
    '                    Dim period As String = HorizontalDictionaryAddressValues.Item(periodAddress)
    '                    Dim cell As Excel.Range = WS.Cells(WS.Range(entityAddress).Row, WS.Range(periodAddress).Column)
    '                    RegisterCellAddressDimensions(cell, entity, AccountsAddressValuesDictionary.ElementAt(0).Value, period)
    '                    periodsDictionary.Add(period, cell.Value2)
    '                Next
    '                accountsDictionary.Add(AccountsAddressValuesDictionary.ElementAt(0).Value, periodsDictionary)
    '                dataSetDictionary.Add(entity, accountsDictionary)
    '            Next

    '    End Select


    'End Sub

    Private Sub RegisterDatasetCell(ByVal p_cell As Excel.Range, _
                                    ByRef p_entityName As String, _
                                    ByRef p_accountName As String, _
                                    ByRef p_period As String)

        Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, p_accountName, p_period)
        m_datasetCellsDictionary.Add(tuple_, p_cell)

        Dim tmpStruct As DataSetCellDimensions
        tmpStruct.m_entityName = p_entityName
        tmpStruct.m_accountName = p_accountName
        tmpStruct.m_period = p_period
        m_datasetCellDimensionsDictionary.Add(p_cell.Address, tmpStruct)

    End Sub


#End Region


#Region "Utilities"

    'Friend Function GetCellFromItem(ByRef entityAddress As String, _
    '                                ByRef accountAddress As String, _
    '                                ByRef periodAddress As String) As Excel.Range

    '    Select Case GlobalOrientationFlag
    '        Case DATASET_ACCOUNTS_PERIODS_OR : Return WS.Cells(WS.Range(accountAddress).Row, WS.Range(periodAddress).Column)
    '        Case DATASET_PERIODS_ACCOUNTS_OR : Return WS.Cells(WS.Range(periodAddress).Row, WS.Range(accountAddress).Column)
    '        Case DATASET_ACCOUNTS_ENTITIES_OR : Return WS.Cells(WS.Range(accountAddress).Row, WS.Range(entityAddress).Column)
    '        Case DATASET_ENTITIES_ACCOUNTS_OR : Return WS.Cells(WS.Range(entityAddress).Row, WS.Range(accountAddress).Column)
    '        Case DATASET_PERIODS_ENTITIES_OR : Return WS.Cells(WS.Range(periodAddress).Row, WS.Range(entityAddress).Column)
    '        Case DATASET_ENTITIES_PERIODS_OR : Return WS.Cells(WS.Range(entityAddress).Row, WS.Range(periodAddress).Column)
    '        Case Else
    '            System.Diagnostics.Debug.WriteLine("Model DataSet - GetCellFromItem() Error: GlobalOrientationFlag non valid or non initialized.")
    '            Return Nothing
    '    End Select

    'End Function

    'Friend Function GetCellAddress(ByRef entityAddress As String, _
    '                                ByRef accountAddress As String, _
    '                                ByRef periodAddress As String) As String

    '    Select Case GlobalOrientationFlag
    '        Case DATASET_ACCOUNTS_PERIODS_OR : Return WS.Cells(WS.Range(accountAddress).Row, WS.Range(periodAddress).Column).address
    '        Case DATASET_PERIODS_ACCOUNTS_OR : Return WS.Cells(WS.Range(periodAddress).Row, WS.Range(accountAddress).Column).address
    '        Case DATASET_ACCOUNTS_ENTITIES_OR : Return WS.Cells(WS.Range(accountAddress).Row, WS.Range(entityAddress).Column).address
    '        Case DATASET_ENTITIES_ACCOUNTS_OR : Return WS.Cells(WS.Range(entityAddress).Row, WS.Range(accountAddress).Column).address
    '        Case DATASET_PERIODS_ENTITIES_OR : Return WS.Cells(WS.Range(periodAddress).Row, WS.Range(entityAddress).Column).address
    '        Case DATASET_ENTITIES_PERIODS_OR : Return WS.Cells(WS.Range(entityAddress).Row, WS.Range(periodAddress).Column).address
    '        Case Else
    '            System.Diagnostics.Debug.WriteLine("Model DataSet - GetCellFromItem() Error: GlobalOrientationFlag non valid or non initialized.")
    '            Return Nothing
    '    End Select

    'End Function

   
#End Region


End Class
