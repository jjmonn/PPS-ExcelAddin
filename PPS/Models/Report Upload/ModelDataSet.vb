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
Imports CRUD

Friend Class ModelDataSet


#Region "Instance Variables"

    Friend m_excelWorkSheet As Excel.Worksheet
    Friend m_lastCell As Excel.Range

    Private m_GlobalScreenShotFlag(,) As String                   ' Flag array for the entire worksheet
    Friend m_GlobalScreenShot(,) As Object                       ' Worksheet values array
    Private m_accountRefs() As String                             ' String array for account references

    'Lists and Dictionaries
    Friend m_periodsDatesList As New List(Of Date)
    Friend m_inputsAccountsList As List(Of Account)

    ' Axes
    Friend m_periodsAddressValuesDictionary As New Dictionary(Of String, String)
    Friend m_accountsAddressValuesDictionary As New Dictionary(Of String, String)
    Friend m_outputsAccountsAddressvaluesDictionary As New Dictionary(Of String, String)
    Friend m_entitiesAddressValuesDictionary As New Dictionary(Of String, String)
    Friend m_periodsValuesAddressDict As New Dictionary(Of String, String)
    Friend m_accountsValuesAddressDict As New Dictionary(Of String, String)
    Friend m_outputsValuesAddressDict As New Dictionary(Of String, String)
    Friend m_entitiesValuesAddressDict As New Dictionary(Of String, String)

    ' DataSet Cells
    Friend m_datasetCellsDictionary As New Dictionary(Of Tuple(Of String, String, String), Excel.Range)
    Friend m_datasetCellDimensionsDictionary As New Dictionary(Of String, DataSetCellDimensions)
    Friend m_currentVersionId As Int32

    'Flags"
    Friend m_dateFlag As Integer             ' Dates Flag: 0 = no period found; 1 = 1 period found ; 2 = period array found ; 3=worksheet name
    Friend m_accountFlag As Integer          ' Accounts Flag: 0 = no account found; 1 = 1 account found ; 2 = account array found
    Friend m_EntityFlag As Integer            ' Asset Flag: 0 = no asset found; 1 = 1 asset found ; 2 = asset array found
    Friend m_globalOrientationFlag As String  ' Aggregation of the three flags (accounts, assets/ products, periods)
    Friend m_datesOrientationFlag As String
    Friend m_accountsOrientationFlag As String
    Friend m_entitiesOrientationFlag As String

    Private Const m_stringFlag As String = "String"
    Private Const m_accountStringFlag As String = "Account"
    Private Const m_entityStringFlag As String = "Entity"
    Private Const m_periodFormatflag As String = "Period"


    'Constants"
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

    Structure DataSetCellDimensions

        Public m_accountName As String
        Public m_entityName As String
        Public m_period As String
        Public m_value As Double

    End Structure


#End Region


#Region "Initialize"

    Public Sub New(inputWS As Excel.Worksheet)

        Me.m_excelWorkSheet = inputWS

    End Sub

#End Region


#Region " Interface"

    Friend Function WsScreenshot() As Boolean

        m_lastCell = GeneralUtilities.GetRealLastCell(m_excelWorkSheet)
        If IsNothing(m_lastCell) Then
            MsgBox("The worksheet is empty")
            m_GlobalScreenShot = Nothing
            Return False
        End If

        m_GlobalScreenShot = m_excelWorkSheet.Range(m_excelWorkSheet.Cells(1, 1), m_lastCell).Value
        ReDim m_GlobalScreenShotFlag(UBound(m_GlobalScreenShot, 1), UBound(m_GlobalScreenShot, 2))
        Return True

    End Function

    ' Lookup for Versions, Accounts, Periods and Entities
    Friend Sub SnapshotWS()

        m_accountsAddressValuesDictionary.Clear()
        m_outputsAccountsAddressvaluesDictionary.Clear()
        m_periodsAddressValuesDictionary.Clear()
        m_entitiesAddressValuesDictionary.Clear()

        m_accountsValuesAddressDict.Clear()
        m_outputsValuesAddressDict.Clear()
        m_periodsValuesAddressDict.Clear()
        m_entitiesValuesAddressDict.Clear()

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

        Dim i, j As Integer
        For i = LBound(m_GlobalScreenShot, 1) To UBound(m_GlobalScreenShot, 1)
            For j = LBound(m_GlobalScreenShot, 2) To UBound(m_GlobalScreenShot, 2)
                Dim version As Version = GlobalVariables.Versions.GetValue(CStr(m_GlobalScreenShot(i, j)))
                If Not version Is Nothing Then
                    AddinModule.SetCurrentVersionId(version.Id)
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
        For i = LBound(m_GlobalScreenShot, 1) To UBound(m_GlobalScreenShot, 1)
            For j = LBound(m_GlobalScreenShot, 2) To UBound(m_GlobalScreenShot, 2)
                If IsDate(m_GlobalScreenShot(i, j)) Then
                    periodStoredAsInt = CInt(CDate((m_GlobalScreenShot(i, j))).ToOADate())
                    If m_periodsDatesList.Contains(CDate(m_GlobalScreenShot(i, j))) _
                    AndAlso Not m_periodsAddressValuesDictionary.ContainsValue(periodStoredAsInt) Then

                        m_periodsAddressValuesDictionary.Add(Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, periodStoredAsInt)
                        m_periodsValuesAddressDict.Add(periodStoredAsInt, Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)
                        m_GlobalScreenShotFlag(i, j) = m_periodFormatflag

                    End If
                End If
            Next j
        Next i

        ' Flag
        Select Case m_periodsAddressValuesDictionary.Count
            Case 0 : m_dateFlag = 0
            Case 1 : m_dateFlag = 1
            Case Else : m_dateFlag = 2
        End Select

    End Sub

    ' Identify the mapped accounts in the WS  | CURRENTLY NOT using the Accounts Search Algo
    Friend Sub AccountsIdentify()

        Dim i, j As Integer
        m_inputsAccountsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS)
        Dim outputsAccountsList As List(Of Account) = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS)
        m_accountsAddressValuesDictionary.Clear()

        For i = LBound(m_GlobalScreenShot, 1) To UBound(m_GlobalScreenShot, 1)                                          ' Loop into rows of input array
            For j = LBound(m_GlobalScreenShot, 2) To UBound(m_GlobalScreenShot, 2)                                      ' Loop into columns of input array
                If VarType(m_GlobalScreenShot(i, j)) = 8 Then

                    Dim currentValue As String = CStr(m_GlobalScreenShot(i, j))

                    ' Direct match or algo > 50% Index à vérifier selon table
                    ' Trim left and right the cell.value2 !! -> To be tested
                    If m_inputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(currentValue)) _
                    AndAlso Not m_accountsAddressValuesDictionary.ContainsValue(currentValue) Then

                        m_accountsAddressValuesDictionary.Add(Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                            currentValue)
                        m_accountsValuesAddressDict.Add(currentValue, Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)
                        m_GlobalScreenShotFlag(i, j) = m_accountStringFlag

                    ElseIf outputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(currentValue)) _
                    AndAlso m_accountsAddressValuesDictionary.ContainsValue(currentValue) = False Then

                        m_outputsAccountsAddressvaluesDictionary.Add(Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                                   currentValue)
                        m_outputsValuesAddressDict.Add(currentValue, Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)
                        m_GlobalScreenShotFlag(i, j) = m_accountStringFlag

                        '         ElseIf AccountSearchAlgo(GlobalScreenShot(i, j)) = True Then           'Repeated procedure but faster...
                        '
                        '            AccountsAddressValuesDictionary.add(Split(Columns(j).Address(ColumnAbsolute:=False), ":")(1) + i, CStr(GlobalScreenShot(i, j)))
                        '            GlobalScreenShotFlag(i, j) = account_flag
                    End If
                    m_GlobalScreenShotFlag(i, j) = m_stringFlag                                       ' Flag for assets lookup

                End If
            Next j
        Next i

        Select Case m_accountsAddressValuesDictionary.Count
            Case 0 : m_accountFlag = 0
            Case 1 : m_accountFlag = 1
            Case Else : m_accountFlag = 2
        End Select

    End Sub

    ' Identify the assets from mapping      | CURRENTLY NOT using the AssetsSearch Algorithm (>leveinstein Threshold%)
    Private Sub EntitiesIdentify()

        Dim i, j As Integer

        For i = LBound(m_GlobalScreenShot, 1) To UBound(m_GlobalScreenShot, 1)
            For j = LBound(m_GlobalScreenShot, 2) To UBound(m_GlobalScreenShot, 2)

                Select Case m_GlobalScreenShotFlag(i, j)
                    Case m_stringFlag

                        If Not GlobalVariables.Entities.GetValue(CStr(m_GlobalScreenShot(i, j))) Is Nothing _
                        AndAlso Not m_entitiesAddressValuesDictionary.ContainsValue(CStr(m_GlobalScreenShot(i, j))) Then

                            m_entitiesAddressValuesDictionary.Add(Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                                                                CStr(m_GlobalScreenShot(i, j)))
                            m_entitiesValuesAddressDict.Add(CStr(m_GlobalScreenShot(i, j)), Split(m_excelWorkSheet.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i)

                            'ElseIf AssetResearchAlgo(GlobalScreenShot(i, j)) Then
                            '    EntitiesAddressValuesDictionary.Add(Split(WS.Columns(j).Address(ColumnAbsolute:=False), ":")(1) & i, _
                            '                          CStr(GlobalScreenShot(i, j)))

                        End If
                End Select
            Next j
        Next i

        Select Case m_entitiesAddressValuesDictionary.Count
            Case 0 : m_EntityFlag = 0
            Case 1 : m_EntityFlag = 1
            Case Else : m_EntityFlag = 2
        End Select

    End Sub


#End Region


#Region "Recognition Algoritms"

    ' Recognizes whether a cell value is an account 
    <Obsolete("Not implemented", True)>
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
        Return False
    End Function

    ' Returns true if a the input exceeds the Levenstein thresold  (Close to a mapped asset)
    Private Function AssetResearchAlgo(str As Object) As Boolean

        Dim Delta As Integer
        Dim P As Double

        AssetResearchAlgo = False

        For Each l_entity As Entity In GlobalVariables.Entities.GetDictionary().Values
            Delta = GeneralUtilities.Levenshtein(CStr(str), l_entity.Name)
            P = Delta / l_entity.Name.Length
            If P <= 2 / l_entity.Name.Length Then
                AssetResearchAlgo = True
                Exit Function
            End If
        Next

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
        For i = 0 To m_periodsAddressValuesDictionary.Keys.Count - 1
            key = m_periodsAddressValuesDictionary.Keys(i)
            m_periodsAddressValuesDictionary.Item(key) = m_excelWorkSheet.Range(key).Value2
        Next

    End Sub

    ' Update the AccountsAddressValuesDictionary with values of the current worksheet
    Private Sub UpdateAccountsDictionaryValuesWithCurrentWS()

        Dim key As String
        For i = 0 To m_accountsAddressValuesDictionary.Keys.Count - 1
            key = m_accountsAddressValuesDictionary.Keys(i)
            m_accountsAddressValuesDictionary.Item(key) = m_excelWorkSheet.Range(key).Value2
        Next

    End Sub

    ' Update the EntitiesAddressValuesDictionary with values of the current worksheet
    Private Sub UpdateEntitiesDictionaryValuesWithCurrentWS()

        Dim key As String
        For i = 0 To m_entitiesAddressValuesDictionary.Keys.Count - 1
            key = m_entitiesAddressValuesDictionary.Keys(i)
            m_entitiesAddressValuesDictionary.Item(key) = m_excelWorkSheet.Range(key).Value2
        Next

    End Sub



#End Region


#End Region


#Region " Orientation Functions"

    ' Determines the worksheet orientations of the ranges
    Public Sub getOrientations()

        Dim MaxRight As String = ""
        Dim MaxBelow As String = ""
        Dim FlagsCode As String = CStr(m_dateFlag) + CStr(m_accountFlag) + CStr(m_EntityFlag)

        Select Case FlagsCode
            Case "111"          ' Only one value
                GetMaxs(MaxRight, MaxBelow)
                Dim MaxsFlag As String = MaxRight + MaxBelow

                'If MaxRight = MaxBelow then '-> Interface New!

                Select Case MaxsFlag
                    Case "AssetAccount"
                        m_entitiesOrientationFlag = "H"
                        m_accountsOrientationFlag = "V"
                    Case "DateAccount"
                        m_datesOrientationFlag = "H"
                        m_accountsOrientationFlag = "V"
                    Case "AssetDate"
                        m_entitiesOrientationFlag = "H"
                        m_datesOrientationFlag = "V"
                    Case "AccountAsset"
                        m_accountsOrientationFlag = "H"
                        m_entitiesOrientationFlag = "V"
                    Case "AccountDate"
                        m_accountsOrientationFlag = "H"
                        m_datesOrientationFlag = "V"
                    Case "DateAsset"
                        m_datesOrientationFlag = "H"
                        m_entitiesOrientationFlag = "V"
                    Case Else
                        ' Interface New ! 
                End Select
            Case "112"                          ' 1 period, 1 Account, Several assets
                getAssetsOrientations()
                If m_entitiesOrientationFlag = "H" Then
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row Then
                        m_datesOrientationFlag = "V"
                    Else
                        m_accountsOrientationFlag = "V"
                    End If
                Else
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column Then
                        m_datesOrientationFlag = "H"
                    Else
                        m_accountsOrientationFlag = "H"
                    End If
                End If
            Case "121"                          ' 1 period, Several Accounts, 1 asset
                getAccountsOrientations()
                If m_accountsOrientationFlag = "H" Then
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                        m_datesOrientationFlag = "V"
                    Else
                        m_entitiesOrientationFlag = "V"
                    End If
                Else
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                        m_datesOrientationFlag = "H"
                    Else
                        m_entitiesOrientationFlag = "H"
                    End If
                End If
            Case "211"                          ' Several periods, 1 Account, 1 Asset
                getDatesOrientations()
                If m_datesOrientationFlag = "H" Then
                    If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                        m_accountsOrientationFlag = "V"
                    Else
                        m_entitiesOrientationFlag = "V"
                    End If
                Else
                    If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                        m_accountsOrientationFlag = "H"
                    Else
                        m_entitiesOrientationFlag = "H"
                    End If
                End If
            Case "311"                          ' WS Period, 1 Account, 1 Asset
                If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                    m_accountsOrientationFlag = "V"
                    m_entitiesOrientationFlag = "H"
                ElseIf m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row < m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                    m_accountsOrientationFlag = "H"
                    m_entitiesOrientationFlag = "V"
                ElseIf m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                    m_accountsOrientationFlag = "H"
                    m_entitiesOrientationFlag = "V"
                Else
                    m_accountsOrientationFlag = "V"
                    m_entitiesOrientationFlag = "H"
                End If
            Case "312"                          ' WS Period, 1 Account, Several Asset
                getAssetsOrientations()
                If m_entitiesOrientationFlag = "H" Then
                    m_accountsOrientationFlag = "V"
                Else
                    m_accountsOrientationFlag = "H"
                End If
            Case "321"                          ' WS Period, Several Accounts, 1 Asset
                getAccountsOrientations()
                If m_accountsOrientationFlag = "H" Then
                    m_entitiesOrientationFlag = "V"
                Else
                    m_entitiesOrientationFlag = "H"
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
        If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column Then
            If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                MaxRight = m_periodFormatflag
            Else
                MaxRight = m_entityStringFlag
            End If
        Else
            If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                MaxRight = m_accountStringFlag
            Else
                MaxRight = m_entityStringFlag
            End If
        End If
        ' Get Maximum Below cell
        If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row Then
            If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                MaxBelow = m_periodFormatflag
            Else
                MaxBelow = m_entityStringFlag
            End If
        Else
            If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                MaxBelow = m_accountStringFlag
            Else
                MaxBelow = m_entityStringFlag
            End If
        End If
    End Sub

    ' Compute whether the Dates range is Vertical or Horizontal
    Private Sub getDatesOrientations()

        Dim deltaRows, deltaColumns As Integer
        If m_dateFlag = 2 Then
            deltaRows = m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row - _
                                    m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column - _
                           m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                m_datesOrientationFlag = "V"
            Else
                m_datesOrientationFlag = "H"
            End If
        End If

    End Sub

    ' Compute whether the Accounts range is Vertical or Horizontal
    Private Sub getAccountsOrientations()

        Dim deltaRows, deltaColumns As Integer
        If m_accountFlag = 2 Then
            deltaRows = m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row - _
                        m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column - _
                           m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                m_accountsOrientationFlag = "V"
            Else
                m_accountsOrientationFlag = "H"
            End If
        End If

    End Sub

    ' Compute whether the Assets range is Vertical or Horizontal
    Private Sub getAssetsOrientations()

        Dim deltaRows, deltaColumns As Integer
        If m_EntityFlag = 2 Then
            deltaRows = m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row - _
                        m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column - _
                           m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > deltaColumns Then
                m_entitiesOrientationFlag = "V"
            Else
                m_entitiesOrientationFlag = "H"
            End If
        End If

    End Sub

    '
    Private Sub DefineGlobalOrientationFlag()

        If m_accountsOrientationFlag = "V" And m_entitiesOrientationFlag = "H" Then       ' Case Accounts in line / Assets in columns
            m_globalOrientationFlag = DATASET_ACCOUNTS_ENTITIES_OR
        ElseIf m_accountsOrientationFlag = "V" And m_datesOrientationFlag = "H" Then    ' Case Accounts in line / Dates in columns
            m_globalOrientationFlag = DATASET_ACCOUNTS_PERIODS_OR
        ElseIf m_datesOrientationFlag = "V" And m_accountsOrientationFlag = "H" Then    ' Case Dates in line / Accounts in columns
            m_globalOrientationFlag = DATASET_PERIODS_ACCOUNTS_OR
        ElseIf m_datesOrientationFlag = "V" And m_entitiesOrientationFlag = "H" Then      ' Case Dates in line / Assets in columns
            m_globalOrientationFlag = DATASET_PERIODS_ENTITIES_OR
        ElseIf m_entitiesOrientationFlag = "V" And m_accountsOrientationFlag = "H" Then   ' Case Assets in line / Accounts in columns
            m_globalOrientationFlag = DATASET_ENTITIES_ACCOUNTS_OR
        ElseIf m_entitiesOrientationFlag = "V" And m_datesOrientationFlag = "H" Then      ' Case Assets in line / Dates in columns
            m_globalOrientationFlag = DATASET_ENTITIES_PERIODS_OR
        Else
            m_globalOrientationFlag = ORIENTATION_ERROR_FLAG
        End If

    End Sub


#End Region


#Region "Dataset Cells Registering"

    Friend Sub RegisterDimensionsToCellDictionary()

        Dim entityName As String = m_entitiesAddressValuesDictionary.ElementAt(0).Value
        Dim periodColumn As Int32
        Dim period As String

        Dim start_time As DateTime
        Dim elapsed_time As TimeSpan
        Dim stop_time As DateTime
        start_time = Now

        m_datasetCellsDictionary.Clear()
        m_datasetCellDimensionsDictionary.Clear()

        For Each PeriodAddressValuePair In m_periodsAddressValuesDictionary
            period = PeriodAddressValuePair.Value
            periodColumn = m_excelWorkSheet.Range(PeriodAddressValuePair.Key).Column

            ' Inputs cells registering
            For Each AccountAddressValuePair In m_accountsAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(AccountAddressValuePair.Key).Row, periodColumn), _
                                    entityName, _
                                    AccountAddressValuePair.Value, _
                                    period)
            Next

            ' Outputs cells registering
            For Each OutputAccountAddressValuePair In m_outputsAccountsAddressvaluesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(OutputAccountAddressValuePair.Key).Row, periodColumn), _
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

        For Each AccountAddressValuePair In m_accountsAddressValuesDictionary
            accountAddress = AccountAddressValuePair.Key
            For Each PeriodAddressValuePair In m_periodsAddressValuesDictionary
                Dim cell As Excel.Range = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(accountAddress).Row, m_excelWorkSheet.Range(PeriodAddressValuePair.Key).Column)
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
