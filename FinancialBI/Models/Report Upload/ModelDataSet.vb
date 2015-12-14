' ModelDataSet.vb
'
' This class manages the data sets to be uploaded and downloaded to and from the server
'   
'  
' to do: 
'  
'
' Known bugs:
'       - Orientation: in some cases does not work (max left or right cells)
'
'
' Last modified: 14/12/2015
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
    Friend m_inputsAccountsList As List(Of Account)
    Friend m_periodsDatesList As New List(Of Date)

    ' Axes
    Friend m_dimensionsAddressValueDict As New SafeDictionary(Of Int16, Dictionary(Of String, String))
    Friend m_dimensionsValueAddressDict As New SafeDictionary(Of Int16, Dictionary(Of String, String))

    ' DataSet Cells
    Friend m_datasetCellsDictionary As New SafeDictionary(Of Tuple(Of String, String, String, String), Excel.Range)
    Friend m_datasetCellDimensionsDictionary As New SafeDictionary(Of String, DataSetCellDimensions)
    Friend m_currentVersionId As Int32

    'Flags"
    Friend m_periodFlag As SnapshotResult
    Friend m_accountFlag As SnapshotResult
    Friend m_entityFlag As SnapshotResult
    Friend m_productFlag As SnapshotResult

    Friend m_globalOrientationFlag As Orientations
    Friend m_periodsOrientationFlag As Alignment
    Friend m_accountsOrientationFlag As Alignment
    Friend m_entitiesOrientationFlag As Alignment
    Friend m_productsOrientationFlag As Alignment

    Friend m_processFlag As GlobalEnums.Process

    'Private Const m_accountStringFlag As String = "Account"
    'Private Const m_entityStringFlag As String = "Entity"
    'Private Const m_periodFormatflag As String = "Period"
    'Private Const m_productFormatflag As String = "Product"

    Structure DataSetCellDimensions
        Public m_accountName As String
        Public m_entityName As String
        Public m_period As String
        Public m_product As String
        Public m_value As Double
    End Structure

    Enum SnapshotResult
        ZERO = 0
        ONE
        SEVERAL
    End Enum

    Enum Orientations
        ACCOUNTS_PERIODS = 0
        PERIODS_ACCOUNTS
        ENTITIES_ACCOUNTS
        ACCOUNTS_ENTITIES
        PERIODS_ENTITIES
        ENTITIES_PERIODS

        PRODUCTS_PERIODS
        PERIODS_PRODUCTS

        FINANCIAL_ORIENTATION_ERROR
        PDC_ORIENTATION_ERROR
        ORIENTATION_ERROR
    End Enum

    Enum Alignment
        UNDEFINED = 0
        VERTICAL
        HORIZONTAL
        UNCLEAR
    End Enum

    Enum Dimension
        ACCOUNT = 0
        OUTPUTACCOUNT
        PERIOD
        ENTITY
        PRODUCT
    End Enum

   
#End Region


#Region "Initialize"

    Public Sub New(inputWS As Excel.Worksheet)

        Me.m_excelWorkSheet = inputWS
        ReinitializeDimensionsDict()

    End Sub

    Private Sub ReinitializeDimensionsDict()

        ' Initialization of the dimensions addresses->values dictionary
        m_dimensionsAddressValueDict.Clear()
        m_dimensionsAddressValueDict.Add(Dimension.ACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.OUTPUTACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.ENTITY, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.PERIOD, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.PRODUCT, New SafeDictionary(Of String, String))

        ' Initialization of the dimensions values->addresses dictionary
        m_dimensionsValueAddressDict.Clear()
        m_dimensionsValueAddressDict.Add(Dimension.ACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.OUTPUTACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.ENTITY, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.PERIOD, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.PRODUCT, New SafeDictionary(Of String, String))

    End Sub


#End Region


#Region "Interface"

    Friend Function WsScreenshot() As Boolean

        m_lastCell = GeneralUtilities.GetRealLastCell(m_excelWorkSheet)
        If IsNothing(m_lastCell) Then
            MsgBox("The worksheet is empty")
            Return False
        End If
        Return True

    End Function

    ' Lookup for Versions, Accounts, Periods and Entities
    Friend Sub SnapshotWS()

        ReinitializeDimensionsDict()

        If VersionsIdentify() = False Then
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
        ProductsIdentify()

    End Sub

#End Region


#Region "Snapshot Functions"

#Region "Primary Recognition"

    Private Function VersionsIdentify() As Boolean

        For rowIndex = 1 To m_lastCell.Row
            For columnIndex = 1 To m_lastCell.Column
                Try
                    If VarType(m_excelWorkSheet.Cells(rowIndex, columnIndex).value) = 8 Then
                        Dim version As Version = GlobalVariables.Versions.GetValue(CStr(m_excelWorkSheet.Cells(rowIndex, columnIndex).value))
                        If Not version Is Nothing Then
                            AddinModule.SetCurrentVersionId(version.Id)
                            m_currentVersionId = version.Id
                            Return True
                        End If
                    End If
                Catch ex As Exception
                    Return False
                End Try
            Next
        Next
        Return False

    End Function

    Friend Function AxisElemIdentify(ByRef p_axisType As CRUD.AxisType) As Int32

        Dim l_cellContent As String
        Dim l_axisElem As AxisElem
        For rowIndex = 1 To m_lastCell.Row
            For columnIndex = 1 To m_lastCell.Column
                Try
                    If VarType(m_excelWorkSheet.Cells(rowIndex, columnIndex).value) = 8 Then
                        l_cellContent = m_excelWorkSheet.Cells(rowIndex, columnIndex).value
                        l_axisElem = GlobalVariables.AxisElems.GetValue(p_axisType, l_cellContent)
                        If l_axisElem IsNot Nothing Then
                            Return l_axisElem.Id
                        End If
                    End If
                Catch ex As Exception
                    Return 0
                End Try
            Next
        Next
        Return 0

    End Function

    ' Look for date in the spreasheet, and populate periodsAddressValuesDictionary
    Private Sub DatesIdentify()

        Dim periodStoredAsInt As Int32
        For Each periodId As UInt32 In GlobalVariables.Versions.GetPeriodsList(m_currentVersionId)
            m_periodsDatesList.Add(Date.FromOADate(periodId))
        Next

        For rowIndex = 1 To m_lastCell.Row
            For columnIndex = 1 To m_lastCell.Column
                Try
                    If IsDate(m_excelWorkSheet.Cells(rowIndex, columnIndex).value) Then
                        periodStoredAsInt = CInt(CDate((m_excelWorkSheet.Cells(rowIndex, columnIndex).value)).ToOADate())
                        Dim res As Date = CDate(m_excelWorkSheet.Cells(rowIndex, columnIndex).value)
                        If m_periodsDatesList.Contains(CDate(m_excelWorkSheet.Cells(rowIndex, columnIndex).value)) _
                        AndAlso Not m_dimensionsAddressValueDict(Dimension.PERIOD).ContainsValue(periodStoredAsInt) Then
                            m_dimensionsAddressValueDict(Dimension.PERIOD).Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), periodStoredAsInt)
                            m_dimensionsValueAddressDict(Dimension.PERIOD).Add(periodStoredAsInt, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                    ' Impossible to read cell's value.
                End Try
            Next
        Next

        ' Flag
        Select Case m_dimensionsAddressValueDict(Dimension.PERIOD).Count
            Case 0 : m_periodFlag = snapshotResult.ZERO
            Case 1 : m_periodFlag = snapshotResult.ONE
            Case Else : m_periodFlag = snapshotResult.SEVERAL
        End Select

    End Sub

    ' Identify the mapped accounts in the WS  | CURRENTLY NOT using the Accounts Search Algo
    Friend Sub AccountsIdentify()

        Dim currentStr As String
        Dim outputsAccountsList As List(Of Account) = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS)
        m_inputsAccountsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS)

        For rowIndex = 1 To m_lastCell.Row
            For columnIndex = 1 To m_lastCell.Column
                Try
                    If VarType(m_excelWorkSheet.Cells(rowIndex, columnIndex).value) = 8 Then
                        currentStr = CStr(m_excelWorkSheet.Cells(rowIndex, columnIndex).value)
                        If m_inputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(currentStr)) _
                        AndAlso Not m_dimensionsAddressValueDict(Dimension.ACCOUNT).ContainsValue(currentStr) Then
                            ' Input account
                            m_dimensionsAddressValueDict(Dimension.ACCOUNT).Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_dimensionsValueAddressDict(Dimension.ACCOUNT).Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        ElseIf outputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(currentStr)) _
                        AndAlso m_dimensionsAddressValueDict(Dimension.ACCOUNT).ContainsValue(currentStr) = False Then
                            ' Computed Account
                            m_dimensionsAddressValueDict(Dimension.OUTPUTACCOUNT).Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_dimensionsValueAddressDict(Dimension.OUTPUTACCOUNT).Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Next

        Select Case m_dimensionsAddressValueDict(Dimension.ACCOUNT).Count
            Case 0 : m_accountFlag = SnapshotResult.ZERO
            Case 1 : m_accountFlag = SnapshotResult.ONE
            Case Else : m_accountFlag = SnapshotResult.SEVERAL
        End Select

    End Sub

    ' Identify the assets from mapping      | CURRENTLY NOT using the AssetsSearch Algorithm (>leveinstein Threshold%)
    Private Sub EntitiesIdentify()

        Dim currentStr As String
        For rowIndex = 1 To m_lastCell.Row
            For columnIndex = 1 To m_lastCell.Column
                Try
                    If VarType(m_excelWorkSheet.Cells(rowIndex, columnIndex).value) = 8 Then
                        currentStr = CStr(m_excelWorkSheet.Cells(rowIndex, columnIndex).value)
                        If Not GlobalVariables.AxisElems.GetValue(AxisType.Entities, currentStr) Is Nothing _
                        AndAlso Not m_dimensionsAddressValueDict(Dimension.ENTITY).ContainsValue(currentStr) Then
                            m_dimensionsAddressValueDict(Dimension.ENTITY).Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_dimensionsValueAddressDict(Dimension.ENTITY).Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Next

        Select Case m_dimensionsAddressValueDict(Dimension.ENTITY).Count
            Case 0 : m_EntityFlag = SnapshotResult.ZERO
            Case 1 : m_EntityFlag = SnapshotResult.ONE
            Case Else : m_EntityFlag = SnapshotResult.SEVERAL
        End Select

    End Sub

    ' Identify the mapped products in the WS  
    Friend Sub ProductsIdentify()

        Dim currentStr As String

        For rowIndex = 1 To m_lastCell.Row
            For columnIndex = 1 To m_lastCell.Column
                Try
                    If VarType(m_excelWorkSheet.Cells(rowIndex, columnIndex).value) = 8 Then
                        currentStr = CStr(m_excelWorkSheet.Cells(rowIndex, columnIndex).value)

                        If Not GlobalVariables.AxisElems.GetValue(AxisType.Product, currentStr) Is Nothing _
                        AndAlso Not m_dimensionsAddressValueDict(Dimension.PRODUCT).ContainsValue(currentStr) Then
                            m_dimensionsAddressValueDict(Dimension.PRODUCT).Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_dimensionsValueAddressDict(Dimension.PRODUCT).Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Next

        Select Case m_dimensionsAddressValueDict(Dimension.PRODUCT).Count
            Case 0 : m_productFlag = SnapshotResult.ZERO
            Case 1 : m_productFlag = SnapshotResult.ONE
            Case Else : m_productFlag = SnapshotResult.SEVERAL
        End Select

    End Sub

    Private Function GetRangeAddressFromRowAndColumn(ByRef p_rowIndex As Int32, _
                                                     ByRef p_columnIndex As Int32) As String

        Return Split(m_excelWorkSheet.Columns(p_columnIndex).Address(ColumnAbsolute:=False), ":")(1) & p_rowIndex

    End Function


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
        For Each l_entity As AxisElem In GlobalVariables.AxisElems.GetDictionary(AxisType.Entities).Values
            Delta = GeneralUtilities.Levenshtein(CStr(str), l_entity.Name)
            P = Delta / l_entity.Name.Length
            If P <= 2 / l_entity.Name.Length Then
                AssetResearchAlgo = True
                Exit Function
            End If
        Next

    End Function

#End Region

#End Region


#Region " Orientation Functions"

    ' Determines the worksheet orientations of the ranges
    Public Sub GetOrientations()

        Dim l_financialFlagsCode As String = CStr(m_periodFlag) & CStr(m_accountFlag) & CStr(m_entityFlag)
        Dim l_PDCFlagsCode As String = CStr(m_periodFlag) & CStr(m_productFlag)
   
        If m_periodFlag = SnapshotResult.ZERO _
        Or m_accountFlag = SnapshotResult.ZERO Then
            m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
            Exit Sub
        End If

        Dim l_account As Account = GlobalVariables.Accounts.GetValue(m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value)
        If l_account Is Nothing Then
            m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
            Exit Sub
        End If

        Select Case l_account.Process
            Case Account.AccountProcess.FINANCIAL
                If m_entityFlag <> SnapshotResult.ZERO Then
                    DefineFinancialDimensionsOrientation(l_financialFlagsCode)
                Else
                    m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
                    Exit Sub
                End If

            Case Account.AccountProcess.RH
                If m_productFlag <> SnapshotResult.ZERO Then
                    DefinePDCDimensionsOrientation(l_PDCFlagsCode)
                Else
                    m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
                    Exit Sub
                End If

        End Select

    End Sub

    Private Sub DefinePDCDimensionsOrientation(ByRef p_PDCFlagsCode As Int16)

        Dim l_maxRight As String = ""
        Dim l_maxBelow As String = ""
        Dim l_lastPeriodCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PERIOD)
        Dim l_lastProductCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PRODUCT)

        Select Case p_PDCFlagsCode
            Case "11"           ' one period, one product

                GetMaxs(l_maxRight, l_maxBelow)
                Dim l_maxsFlag As String = l_maxRight + l_maxBelow
                Select Case l_maxsFlag
                    Case Dimension.PRODUCT & Dimension.PERIOD
                        m_productsOrientationFlag = Alignment.HORIZONTAL
                        m_periodsOrientationFlag = Alignment.VERTICAL
                    Case Dimension.PERIOD & Dimension.PRODUCT
                        m_periodsOrientationFlag = Alignment.HORIZONTAL
                        m_productsOrientationFlag = Alignment.VERTICAL
                End Select

            Case "21"           ' several periods, one product

                GetDimensionOrientations(Dimension.PERIOD, m_periodFlag, m_periodsOrientationFlag)
                If m_periodsOrientationFlag = Alignment.HORIZONTAL Then
                    m_productsOrientationFlag = Alignment.VERTICAL
                Else
                    m_productsOrientationFlag = Alignment.HORIZONTAL
                End If

            Case "12"           ' one period, several products
                GetDimensionOrientations(Dimension.PRODUCT, m_productFlag, m_productsOrientationFlag)
                If m_productsOrientationFlag = Alignment.HORIZONTAL Then
                    m_periodsOrientationFlag = Alignment.VERTICAL
                Else
                    m_periodsOrientationFlag = Alignment.HORIZONTAL
                End If

            Case "22"           ' several periods, several products
                GetDimensionOrientations(Dimension.PERIOD, m_periodFlag, m_periodsOrientationFlag)                      ' Dates Orientation
                GetDimensionOrientations(Dimension.PRODUCT, m_productFlag, m_productsOrientationFlag)                   ' Products orientation

        End Select
        DefineGlobalOrientationFlag()
        m_processFlag = GlobalEnums.Process.PDC

    End Sub

    Private Sub DefineFinancialDimensionsOrientation(ByRef p_financialFlagsCode As Int16)

        Dim l_maxRight As String = ""
        Dim l_maxBelow As String = ""
        Dim l_lastPeriodCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PERIOD)
        Dim l_lastAccountCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.ACCOUNT)
        Dim l_lastEntityCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.ENTITY)

        Select Case p_financialFlagsCode
            Case "111"          ' Only one value
                GetMaxs(l_maxRight, l_maxBelow)
                Dim l_maxsFlag As String = l_maxRight + l_maxBelow

                'If MaxRight = MaxBelow then '-> Interface New!

                Select Case l_maxsFlag
                    Case Dimension.ENTITY & Dimension.ACCOUNT
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    Case Dimension.PERIOD & Dimension.ACCOUNT
                        m_periodsOrientationFlag = Alignment.HORIZONTAL
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    Case Dimension.ENTITY & Dimension.PERIOD
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                        m_periodsOrientationFlag = Alignment.VERTICAL
                    Case Dimension.ACCOUNT & Dimension.ENTITY
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    Case Dimension.ACCOUNT & Dimension.PERIOD
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                        m_periodsOrientationFlag = Alignment.VERTICAL
                    Case Dimension.PERIOD & Dimension.ENTITY
                        m_periodsOrientationFlag = Alignment.HORIZONTAL
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    Case Else
                        ' Interface New ! 
                End Select
            Case "112"                          ' 1 period, 1 Account, Several assets
                GetDimensionOrientations(Dimension.ENTITY, m_entityFlag, m_entitiesOrientationFlag)
                If m_entitiesOrientationFlag = Alignment.HORIZONTAL Then
                    If l_lastPeriodCell.Row > l_lastAccountCell.Row Then
                        m_periodsOrientationFlag = Alignment.VERTICAL
                    Else
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    End If
                Else
                    If l_lastPeriodCell.Column > l_lastAccountCell.Column Then
                        m_periodsOrientationFlag = Alignment.HORIZONTAL
                    Else
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                    End If
                End If
            Case "121"                          ' 1 period, Several Accounts, 1 asset
                GetDimensionOrientations(Dimension.ACCOUNT, m_accountFlag, m_accountsOrientationFlag)
                If m_accountsOrientationFlag = Alignment.HORIZONTAL Then
                    If l_lastPeriodCell.Row > l_lastEntityCell.Row Then
                        m_periodsOrientationFlag = Alignment.VERTICAL
                    Else
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    End If
                Else
                    If l_lastPeriodCell.Column > l_lastEntityCell.Column Then
                        m_periodsOrientationFlag = Alignment.HORIZONTAL
                    Else
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                    End If
                End If
            Case "211"                          ' Several periods, 1 Account, 1 Asset
                GetDimensionOrientations(Dimension.PERIOD, m_periodFlag, m_periodsOrientationFlag)
                If m_periodsOrientationFlag = Alignment.HORIZONTAL Then
                    If l_lastAccountCell.Row > l_lastEntityCell.Row Then
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    Else
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    End If
                Else
                    If l_lastAccountCell.Column > l_lastEntityCell.Column Then
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                    Else
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                    End If
                End If
            Case Else
                ' Case "221","212","122","222","322"
                GetDimensionOrientations(Dimension.PERIOD, m_periodFlag, m_periodsOrientationFlag)                      ' Dates Orientation
                GetDimensionOrientations(Dimension.ACCOUNT, m_accountFlag, m_accountsOrientationFlag)                    ' Accounts orientation
                GetDimensionOrientations(Dimension.ENTITY, m_entityFlag, m_entitiesOrientationFlag)                      ' Assets orientation
        End Select
        DefineGlobalOrientationFlag()
        m_processFlag = GlobalEnums.Process.FINANCIAL

    End Sub

    ' Define the cells at the most right and the most bottom
    Private Sub GetMaxs(ByRef p_maxRight As String, ByRef p_maxBelow As String)

        Dim l_lastPeriodCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PERIOD)
        Dim l_lastAccountCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.ACCOUNT)
        Dim l_lastEntityCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.ENTITY)
        Dim l_lastProductCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PRODUCT)

        ' Max right cell identification
        Dim l_rightCellsDict As New Dictionary(Of Int16, Int32)
        l_rightCellsDict.Add(Dimension.PERIOD, l_lastPeriodCell.Column)
        l_rightCellsDict.Add(Dimension.ACCOUNT, l_lastAccountCell.Column)
        l_rightCellsDict.Add(Dimension.ENTITY, l_lastEntityCell.Column)
        l_rightCellsDict.Add(Dimension.PRODUCT, l_lastProductCell.Column)
        Dim l_columnsSortedDict = (From entry In l_rightCellsDict Order By entry.Value Ascending Select entry)
        p_maxRight = l_columnsSortedDict.ElementAt(0).Key

        ' Max below cell identification
        Dim l_belowCellsDict As New Dictionary(Of Int16, Int32)
        l_rightCellsDict.Add(Dimension.PERIOD, l_lastPeriodCell.Row)
        l_rightCellsDict.Add(Dimension.ACCOUNT, l_lastAccountCell.Row)
        l_rightCellsDict.Add(Dimension.ENTITY, l_lastEntityCell.Row)
        l_rightCellsDict.Add(Dimension.PRODUCT, l_lastProductCell.Row)
        Dim l_rowsSortedDict = (From entry In l_belowCellsDict Order By entry.Value Ascending Select entry)
        p_maxBelow = l_rowsSortedDict.ElementAt(0).Key

    End Sub

    Private Sub GetDimensionOrientations(ByRef p_dimension As Int16, _
                                         ByRef p_dimensionSnapshotResultFlag As Int16, _
                                         ByRef p_dimensionOrientationFlag As Int16)

        Dim l_deltaRows, l_deltaColumns As Integer
        If p_dimensionSnapshotResultFlag = SnapshotResult.SEVERAL Then
            l_deltaRows = m_excelWorkSheet.Range(m_dimensionsAddressValueDict(p_dimension).ElementAt(m_dimensionsAddressValueDict(p_dimension).Count - 1).Key).Row _
                          - m_excelWorkSheet.Range(m_dimensionsAddressValueDict(p_dimension).ElementAt(0).Key).Row

            l_deltaColumns = m_excelWorkSheet.Range(m_dimensionsAddressValueDict(p_dimension).ElementAt(m_dimensionsAddressValueDict(p_dimension).Count - 1).Key).Column _
                             - m_excelWorkSheet.Range(m_dimensionsAddressValueDict(p_dimension).ElementAt(0).Key).Column

            If l_deltaRows > 0 AndAlso l_deltaColumns > 0 Then
                p_dimensionOrientationFlag = Alignment.UNCLEAR
                Exit Sub
            End If

            If l_deltaRows > l_deltaColumns Then
                p_dimensionOrientationFlag = Alignment.VERTICAL
            ElseIf l_deltaColumns > l_deltaRows Then
                p_dimensionOrientationFlag = Alignment.HORIZONTAL
            Else
                p_dimensionOrientationFlag = Alignment.UNCLEAR
            End If
        Else
            p_dimensionOrientationFlag = Alignment.UNCLEAR
        End If

    End Sub

    Private Sub DefineGlobalOrientationFlag()

        If m_accountsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_entitiesOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ACCOUNTS_ENTITIES
            Exit Sub
        End If

        If m_accountsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_periodsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ACCOUNTS_PERIODS
            Exit Sub
        End If

        If m_periodsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_accountsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.PERIODS_ACCOUNTS
            Exit Sub
        End If

        If m_periodsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_entitiesOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.PERIODS_ENTITIES
            Exit Sub
        End If

        If m_entitiesOrientationFlag = Alignment.VERTICAL _
        AndAlso m_accountsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ENTITIES_ACCOUNTS
            Exit Sub
        End If

        If m_entitiesOrientationFlag = Alignment.VERTICAL _
        AndAlso m_periodsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ENTITIES_PERIODS
            Exit Sub
        End If

        If m_productsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_periodsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.PRODUCTS_PERIODS
            Exit Sub
        End If

        If m_periodsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_productsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.PERIODS_PRODUCTS
            Exit Sub
        End If

        m_globalOrientationFlag = Orientations.ORIENTATION_ERROR

    End Sub

    Private Function GetLastCellOfDimensionDict(ByRef p_dimension As Int16) As Excel.Range
        Return m_excelWorkSheet.Range(m_dimensionsAddressValueDict(p_dimension).ElementAt(m_dimensionsAddressValueDict(p_dimension).Count - 1).Key)
    End Function

#End Region


#Region "Cells dimensions registering"

    Friend Sub RegisterDimensionsToCellDictionary()

        m_datasetCellsDictionary.Clear()
        m_datasetCellDimensionsDictionary.Clear()

        Select Case m_globalOrientationFlag
            Case Orientations.ACCOUNTS_PERIODS : RegisterDimensionsToCellDictionaryACCOUNTS_PERIODS()
            Case Orientations.PERIODS_ACCOUNTS : RegisterDimensionsToCellDictionaryPERIODS_ACCOUNTS()
            Case Orientations.ACCOUNTS_ENTITIES : RegisterDimensionsToCellDictionaryACCOUNTS_ENTITIES()
            Case Orientations.ENTITIES_ACCOUNTS : RegisterDimensionsToCellDictionaryENTITIES_ACCOUNTS()
            Case Orientations.ENTITIES_PERIODS : RegisterDimensionsToCellDictionaryENTITIES_PERIODS()
            Case Orientations.PERIODS_ENTITIES : RegisterDimensionsToCellDictionaryPERIODS_ENTITIES()

            Case Orientations.PRODUCTS_PERIODS : RegisterDimensionsToCellDictionaryPRODUCTS_PERIODS()
            Case Orientations.PERIODS_PRODUCTS : RegisterDimensionsToCellDictionaryPERIODS_PRODUCTS()
        End Select

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryACCOUNTS_PERIODS()

        Dim l_entityName As String = m_dimensionsAddressValueDict(Dimension.ENTITY).ElementAt(0).Value
        Dim l_periodColumn As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)
            l_period = l_periodAddressValuePair.Value
            l_periodColumn = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column

            ' Inputs cells registering
            For Each AccountAddressValuePair In m_dimensionsAddressValueDict(Dimension.ACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(AccountAddressValuePair.Key).Row, l_periodColumn), _
                                    l_entityName, _
                                    AccountAddressValuePair.Value, _
                                    "", _
                                    l_period)
            Next

            ' Outputs cells registering
            For Each OutputAccountAddressValuePair In m_dimensionsAddressValueDict(Dimension.OUTPUTACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(OutputAccountAddressValuePair.Key).Row, l_periodColumn), _
                                    l_entityName, _
                                    OutputAccountAddressValuePair.Value, _
                                    "", _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPERIODS_ACCOUNTS()

        Dim l_entityName As String = m_dimensionsAddressValueDict(Dimension.ENTITY).ElementAt(0).Value
        Dim l_periodRow As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)
            l_period = l_periodAddressValuePair.Value
            l_periodRow = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row

            ' Inputs cells registering
            For Each l_accountAddressValuePair In m_dimensionsAddressValueDict(Dimension.ACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_periodRow, m_excelWorkSheet.Range(l_accountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_accountAddressValuePair.Value, _
                                    "", _
                                    l_period)
            Next

            ' Outputs cells registering
            For Each l_outputAccountAddressValuePair In m_dimensionsAddressValueDict(Dimension.OUTPUTACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_periodRow, m_excelWorkSheet.Range(l_outputAccountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_outputAccountAddressValuePair.Value, _
                                    "", _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryACCOUNTS_ENTITIES()

        Dim l_period As Int32 = m_dimensionsAddressValueDict(Dimension.PERIOD).ElementAt(0).Value
        Dim l_entityColumn As Int32
        Dim l_entityName As String

        For Each l_entityAddressValuePair In m_dimensionsAddressValueDict(Dimension.ENTITY)
            l_entityName = l_entityAddressValuePair.Value
            l_entityColumn = m_excelWorkSheet.Range(l_entityAddressValuePair.Key).Column

            ' Inputs cells registering
            For Each l_accountAddressValuePair In m_dimensionsAddressValueDict(Dimension.ACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_accountAddressValuePair.Key).Row, l_entityColumn), _
                                    l_entityName, _
                                    l_accountAddressValuePair.Value, _
                                    "", _
                                    l_period)
            Next

            ' Outputs cells registering
            For Each l_outputAccountAddressValuePair In m_dimensionsAddressValueDict(Dimension.OUTPUTACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_outputAccountAddressValuePair.Key).Row, l_entityColumn), _
                                    l_entityName, _
                                    l_outputAccountAddressValuePair.Value, _
                                    "", _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryENTITIES_ACCOUNTS()

        Dim l_periodId As String = m_dimensionsAddressValueDict(Dimension.PERIOD).ElementAt(0).Value
        Dim l_entityRow As Int32
        Dim l_entityName As String

        For Each l_entitiesAddressValuePair In m_dimensionsAddressValueDict(Dimension.ENTITY)
            l_entityName = l_entitiesAddressValuePair.Value
            l_entityRow = m_excelWorkSheet.Range(l_entitiesAddressValuePair.Key).Row

            ' Inputs cells registering
            For Each l_accountAddressValuePair In m_dimensionsAddressValueDict(Dimension.ACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_entityRow, m_excelWorkSheet.Range(l_accountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_accountAddressValuePair.Value, _
                                    "", _
                                    l_periodId)
            Next

            ' Outputs cells registering
            For Each l_outputAccountAddressValuePair In m_dimensionsAddressValueDict(Dimension.OUTPUTACCOUNT)
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_entityRow, m_excelWorkSheet.Range(l_outputAccountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_outputAccountAddressValuePair.Value, _
                                    "", _
                                    l_periodId)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryENTITIES_PERIODS()

        Dim l_accountName As String = m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value
        Dim l_periodColumn As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)
            l_period = l_periodAddressValuePair.Value
            l_periodColumn = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column

            For Each l_entityAddressValuePair In m_dimensionsAddressValueDict(Dimension.ENTITY)
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_entityAddressValuePair.Key).Row, l_periodColumn), _
                                    l_entityAddressValuePair.Value, _
                                    l_accountName, _
                                    "", _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPERIODS_ENTITIES()

        Dim l_accountName As String = m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value
        Dim l_entityColumn As Int32
        Dim l_entityName As String

        For Each l_entityAddressValuePair In m_dimensionsAddressValueDict(Dimension.ENTITY)
            l_entityName = l_entityAddressValuePair.Value
            l_entityColumn = m_excelWorkSheet.Range(l_entityAddressValuePair.Key).Column

            For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row, l_entityColumn), _
                                    l_entityName, _
                                    l_accountName, _
                                    "", _
                                    l_periodAddressValuePair.Value)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPRODUCTS_PERIODS()

        Dim l_periodColumn As Int32
        Dim l_period As String
     
        For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)
            l_period = l_periodAddressValuePair.Value
            l_periodColumn = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column

            For Each l_productAddressValuePair In m_dimensionsAddressValueDict(Dimension.PRODUCT)

                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_productAddressValuePair.Key).Row, l_periodColumn), _
                                    "", _
                                    m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value, _
                                    l_productAddressValuePair.Value, _
                                    l_period)
                ' set BU value ?
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPERIODS_PRODUCTS()

        Dim l_productColumn As Int32
        Dim l_productName As String
    
        For Each l_productAddressValuePair In m_dimensionsAddressValueDict(Dimension.PRODUCT)
            l_productName = l_productAddressValuePair.Value
            l_productColumn = m_excelWorkSheet.Range(l_productAddressValuePair.Key).Column

            For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)


                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row, l_productColumn), _
                                    "", _
                                    m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value, _
                                    l_productName, _
                                    l_periodAddressValuePair.Value)
            Next
        Next

    End Sub

    ' Cells and dimensions registration utility function
    Private Sub RegisterDatasetCell(ByVal p_cell As Excel.Range, _
                                    ByRef p_entityName As String, _
                                    ByRef p_accountName As String, _
                                    ByRef p_productName As String, _
                                    ByRef p_period As String)

        Dim tuple_ As New Tuple(Of String, String, String, String)(p_entityName, p_accountName, p_productName, p_period)
        m_datasetCellsDictionary.Add(tuple_, p_cell)

        Dim tmpStruct As DataSetCellDimensions
        tmpStruct.m_entityName = p_entityName
        tmpStruct.m_accountName = p_accountName
        tmpStruct.m_product = p_productName
        tmpStruct.m_period = p_period
        m_datasetCellDimensionsDictionary.Add(p_cell.Address, tmpStruct)

    End Sub

#End Region


#Region "Cells Values Registration"

    Friend Sub RegisterDataSetCellsValues()

        Select Case m_globalOrientationFlag
            Case Orientations.ACCOUNTS_PERIODS : RegisterDataSetCellsValues(Dimension.ACCOUNT, Dimension.PERIOD)
            Case Orientations.PERIODS_ACCOUNTS : RegisterDataSetCellsValues(Dimension.PERIOD, Dimension.ACCOUNT)
            Case Orientations.ACCOUNTS_ENTITIES : RegisterDataSetCellsValues(Dimension.ACCOUNT, Dimension.ENTITY)
            Case Orientations.ENTITIES_ACCOUNTS : RegisterDataSetCellsValues(Dimension.ENTITY, Dimension.ACCOUNT)
            Case Orientations.ENTITIES_PERIODS : RegisterDataSetCellsValues(Dimension.ENTITY, Dimension.PERIOD)
            Case Orientations.PERIODS_ENTITIES : RegisterDataSetCellsValues(Dimension.PERIOD, Dimension.ENTITY)
            Case Orientations.PRODUCTS_PERIODS : RegisterDataSetCellsValues(Dimension.PRODUCT, Dimension.PERIOD)
            Case Orientations.PERIODS_PRODUCTS : RegisterDataSetCellsValues(Dimension.PERIOD, Dimension.PRODUCT)
        End Select

    End Sub

    Private Sub RegisterDataSetCellsValues(ByRef p_verticalDimension As Dimension, _
                                           ByRef p_horizontalDimension As Dimension)

        Dim l_horizontalDimensionAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_horizontalAddressValuePair In m_dimensionsAddressValueDict(p_horizontalDimension)
            l_horizontalDimensionAddress = l_horizontalAddressValuePair.Key
            For Each l_verticalAddressValuePair In m_dimensionsAddressValueDict(p_verticalDimension)
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_horizontalDimensionAddress).Column, _
                                                m_excelWorkSheet.Range(l_verticalAddressValuePair.Key).Row)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub

#End Region


End Class
