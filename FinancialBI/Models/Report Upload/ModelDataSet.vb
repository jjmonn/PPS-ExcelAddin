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
' Last modified: 18/01/2016
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
    Private m_rhAccountName As String

    ' Axes
    Friend m_dimensionsAddressValueDict As New SafeDictionary(Of Int16, Dictionary(Of String, String))
    Friend m_dimensionsValueAddressDict As New SafeDictionary(Of Int16, Dictionary(Of String, String))

    ' DataSet Cells
    Friend m_datasetCellsDictionary As New SafeDictionary(Of Tuple(Of String, String, String, String), Excel.Range)
    Friend m_datasetCellDimensionsDictionary As New SafeDictionary(Of String, DataSetCellDimensions)
    Friend m_currentVersionId As Int32
    ' Friend m_rhAccount As Account

    'Flags"
    Friend m_periodFlag As SnapshotResult
    Friend m_accountFlag As SnapshotResult
    Friend m_entityFlag As SnapshotResult
    Friend m_employeeFlag As SnapshotResult

    Friend m_globalOrientationFlag As Orientations
    Friend m_periodsOrientationFlag As Alignment
    Friend m_accountsOrientationFlag As Alignment
    Friend m_entitiesOrientationFlag As Alignment
    Friend m_employeeOrientationFlag As Alignment

    Friend m_processFlag As Account.AccountProcess
   
    Structure DataSetCellDimensions
        Public m_accountName As String
        Public m_entityName As String
        Public m_period As String
        Public m_employee As String
        Public m_value As Double
        Public m_client As String
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

        EMPLOYEES_PERIODS
        PERIODS_EMPLOYEES

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
        EMPLOYEE
    End Enum

#End Region

#Region "Properties getters"

    Friend ReadOnly Property RhAccountName As String
        Get
            Return m_rhAccountName
        End Get
    End Property

#End Region


#Region "Initialize"

    Public Sub New(ByRef inputWS As Excel.Worksheet, _
                   ByRef p_RHAccountName As String)

        m_rhAccountName = p_RHAccountName
        Me.m_excelWorkSheet = inputWS
        ReinitializeDimensionsDict()
        m_processFlag = My.Settings.processId

    End Sub

    Private Sub ReinitializeDimensionsDict()

        ' Initialization of the dimensions addresses->values dictionary
        m_dimensionsAddressValueDict.Clear()
        m_dimensionsAddressValueDict.Add(Dimension.ACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.OUTPUTACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.ENTITY, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.PERIOD, New SafeDictionary(Of String, String))
        m_dimensionsAddressValueDict.Add(Dimension.EMPLOYEE, New SafeDictionary(Of String, String))

        ' Initialization of the dimensions values->addresses dictionary
        m_dimensionsValueAddressDict.Clear()
        m_dimensionsValueAddressDict.Add(Dimension.ACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.OUTPUTACCOUNT, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.ENTITY, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.PERIOD, New SafeDictionary(Of String, String))
        m_dimensionsValueAddressDict.Add(Dimension.EMPLOYEE, New SafeDictionary(Of String, String))

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
    Friend Sub SnapshotWS(Optional ByRef p_periodsList As List(Of Int32) = Nothing)

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

        DimensionsIdentificationProcess(m_processFlag, p_periodsList)
        SnapshotFlagResultFill()

    End Sub

    Friend Sub Flush()

        m_dimensionsAddressValueDict.Clear()
        m_dimensionsValueAddressDict.Clear()
        m_inputsAccountsList.Clear()
        m_periodsDatesList.Clear()
        m_datasetCellsDictionary.Clear()
        m_datasetCellDimensionsDictionary.Clear()

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

    Private Sub DimensionsIdentificationProcess(ByRef p_process As Account.AccountProcess, _
                                                Optional ByRef p_periodsList As List(Of Int32) = Nothing)

        ' periods init
        If p_periodsList Is Nothing Then
            p_periodsList = GlobalVariables.Versions.GetPeriodsList(m_currentVersionId).ToList
        End If

        m_periodsDatesList.Clear()
        For Each periodId As UInt32 In p_periodsList
            m_periodsDatesList.Add(Date.FromOADate(periodId))
        Next

        ' Accounts init
        Dim l_cell As Excel.Range
        Dim l_cellAddress As String
        Dim l_outputsAccountsList As List(Of Account) = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS, p_process)
        m_inputsAccountsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS, p_process)

        For l_rowIndex = 1 To m_lastCell.Row
            For l_columnIndex = 1 To m_lastCell.Column
                l_cell = m_excelWorkSheet.Cells(l_rowIndex, l_columnIndex)
                If l_cell Is Nothing _
                Or l_cell.EntireRow.Hidden = True _
                Or l_cell.EntireColumn.Hidden = True Then
                    Continue For
                    System.Diagnostics.Debug.WriteLine("Dataset: Snapshot method: DimensionsIdentificationProcess > error in cell identication process: address : ")
                End If
                l_cellAddress = GetRangeAddressFromRowAndColumn(l_rowIndex, l_columnIndex)

                If IsDate(l_cell.Value) Then
                    If DateIdentify(l_cell, l_cellAddress) = False Then
                        StringDimensionsIdentify(p_process, l_cell, l_cellAddress, l_outputsAccountsList)
                    End If
                Else
                    StringDimensionsIdentify(p_process, l_cell, l_cellAddress, l_outputsAccountsList)
                End If
            Next
        Next

    End Sub

    Private Function DateIdentify(ByRef p_cell As Excel.Range, _
                                  ByRef p_cellAddress As String)

        Dim periodStoredAsInt As Int32 = CInt(CDate((p_cell).Value).ToOADate())
        Dim res As Date = CDate(p_cell.Value)
        If m_periodsDatesList.Contains(CDate(p_cell.Value)) _
        AndAlso Not m_dimensionsAddressValueDict(Dimension.PERIOD).ContainsValue(periodStoredAsInt) Then
            m_dimensionsAddressValueDict(Dimension.PERIOD).Add(p_cellAddress, periodStoredAsInt)
            m_dimensionsValueAddressDict(Dimension.PERIOD).Add(periodStoredAsInt, p_cellAddress)
            Return True
        End If
        Return False

    End Function

    Private Sub StringDimensionsIdentify(ByRef p_process As Account.AccountProcess, _
                                         ByRef p_cell As Excel.Range, _
                                         ByRef p_cellAddress As String, _
                                         ByRef m_outputsAccountsList As List(Of Account))

        Select Case m_processFlag
            Case Account.AccountProcess.FINANCIAL : StringDimensionsIdentifyFinancial(p_cell, p_cellAddress, m_outputsAccountsList)
            Case Account.AccountProcess.RH : StringDimensionsIdentifyRH(p_cell, p_cellAddress, m_outputsAccountsList)
        End Select

    End Sub

    Private Sub StringDimensionsIdentifyFinancial(ByRef p_cell As Excel.Range, _
                                                  ByRef p_cellAddress As String, _
                                                  ByRef m_outputsAccountsList As List(Of Account))

        If VarType(p_cell.Value) = 8 Then
            Dim l_currentStr As String = CStr(p_cell.Value2)
            If AccountsIdentify(l_currentStr, p_cell, p_cellAddress, m_outputsAccountsList) = True Then Exit Sub
            If EntitiesIdentify(l_currentStr, p_cell, p_cellAddress) = True Then Exit Sub
        End If

    End Sub

    Private Sub StringDimensionsIdentifyRH(ByRef p_cell As Excel.Range, _
                                           ByRef p_cellAddress As String, _
                                           ByRef m_outputsAccountsList As List(Of Account))

        If VarType(p_cell.Value) = 8 Then
            Dim l_currentStr As String = CStr(p_cell.Value2)
            If EmployeesIdentify(l_currentStr, p_cell, p_cellAddress) = True Then Exit Sub
            '      If AccountsIdentify(l_currentStr, p_cell, p_cellAddress, m_outputsAccountsList) = True Then Exit Sub
            If EntitiesIdentify(l_currentStr, p_cell, p_cellAddress) = True Then Exit Sub
        End If

    End Sub

    Private Function AccountsIdentify(ByRef p_currentStr As String, _
                                      ByRef p_cell As Excel.Range, _
                                      ByRef p_cellAddress As String, _
                                      ByRef m_outputsAccountsList As List(Of Account)) As Boolean

        ' -> amélioration speed: inputs and outputs list = lists of string (direct comparison)
        ' ou bien les dictionaires comparaison devrait contenir les CRUD directement

        If m_inputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(p_currentStr)) _
        AndAlso Not m_dimensionsAddressValueDict(Dimension.ACCOUNT).ContainsValue(p_currentStr) Then
            ' Input account
            m_dimensionsAddressValueDict(Dimension.ACCOUNT).Add(p_cellAddress, p_currentStr)
            m_dimensionsValueAddressDict(Dimension.ACCOUNT).Add(p_currentStr, p_cellAddress)
            Return True
        ElseIf m_outputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(p_currentStr)) _
        AndAlso m_dimensionsAddressValueDict(Dimension.ACCOUNT).ContainsValue(p_currentStr) = False Then
            ' Computed Account
            m_dimensionsAddressValueDict(Dimension.OUTPUTACCOUNT).Add(p_cellAddress, p_currentStr)
            m_dimensionsValueAddressDict(Dimension.OUTPUTACCOUNT).Add(p_currentStr, p_cellAddress)
            Return True
        End If
        Return False

    End Function

    Private Function EntitiesIdentify(ByRef p_currentStr As String, _
                                      ByRef p_cell As Excel.Range, _
                                      ByRef p_cellAddress As String)

        If GlobalVariables.AxisElems.GetDictionary(AxisType.Entities).SecondaryKeys.Contains(p_currentStr) _
        AndAlso Not m_dimensionsAddressValueDict(Dimension.ENTITY).ContainsValue(p_currentStr) Then
            m_dimensionsAddressValueDict(Dimension.ENTITY).Add(p_cellAddress, p_currentStr)
            m_dimensionsValueAddressDict(Dimension.ENTITY).Add(p_currentStr, p_cellAddress)
            Return True
        End If
        Return False

    End Function

    Friend Function EmployeesIdentify(ByRef p_currentStr As String, _
                                      ByRef p_cell As Excel.Range, _
                                      ByRef p_cellAddress As String) As Boolean

        If GlobalVariables.AxisElems.GetDictionary(AxisType.Employee).SecondaryKeys.Contains(p_currentStr) _
        AndAlso Not m_dimensionsAddressValueDict(Dimension.EMPLOYEE).ContainsValue(p_currentStr) Then
            m_dimensionsAddressValueDict(Dimension.EMPLOYEE).Add(p_cellAddress, p_currentStr)
            m_dimensionsValueAddressDict(Dimension.EMPLOYEE).Add(p_currentStr, p_cellAddress)
            Return True
        End If
        Return False

    End Function

    Private Function SnapshotFlagResultFill()

        Select Case m_dimensionsAddressValueDict(Dimension.ACCOUNT).Count
            Case 0 : m_accountFlag = SnapshotResult.ZERO
            Case 1 : m_accountFlag = SnapshotResult.ONE
            Case Else : m_accountFlag = SnapshotResult.SEVERAL
        End Select

        Select Case m_dimensionsAddressValueDict(Dimension.PERIOD).Count
            Case 0 : m_periodFlag = SnapshotResult.ZERO
            Case 1 : m_periodFlag = SnapshotResult.ONE
            Case Else : m_periodFlag = SnapshotResult.SEVERAL
        End Select

        Select Case m_dimensionsAddressValueDict(Dimension.ENTITY).Count
            Case 0 : m_entityFlag = SnapshotResult.ZERO
            Case 1 : m_entityFlag = SnapshotResult.ONE
            Case Else : m_entityFlag = SnapshotResult.SEVERAL
        End Select

        Select Case m_dimensionsAddressValueDict(Dimension.EMPLOYEE).Count
            Case 0 : m_employeeFlag = SnapshotResult.ZERO
            Case 1 : m_employeeFlag = SnapshotResult.ONE
            Case Else : m_employeeFlag = SnapshotResult.SEVERAL
        End Select

    End Function

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
        Dim l_PDCFlagsCode As String = CStr(m_periodFlag) & CStr(m_employeeFlag)

        If m_periodFlag = SnapshotResult.ZERO _
        Or m_entityFlag = SnapshotResult.ZERO Then
            m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
            Exit Sub
        End If

        Select Case m_processFlag
            Case Account.AccountProcess.FINANCIAL
                If m_accountFlag = SnapshotResult.ZERO Then
                    m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
                    Exit Sub
                End If
                DefineFinancialDimensionsOrientation(l_financialFlagsCode)

            Case Account.AccountProcess.RH
                If m_employeeFlag = SnapshotResult.ZERO _
                OrElse m_rhAccountName = "" Then
                    m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
                    Exit Sub
                Else
                    DefinePDCDimensionsOrientation(l_PDCFlagsCode)
                End If

        End Select

    End Sub

    Private Sub DefinePDCDimensionsOrientation(ByRef p_PDCFlagsCode As Int16)

        Dim l_maxRight As String = ""
        Dim l_maxBelow As String = ""
        Dim l_lastPeriodCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PERIOD)
        Dim l_lastProductCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.EMPLOYEE)

        Select Case p_PDCFlagsCode
            Case "11"           ' one period, one product

                GetMaxs(l_maxRight, l_maxBelow)
                Dim l_maxsFlag As String = l_maxRight + l_maxBelow
                Select Case l_maxsFlag
                    Case Dimension.EMPLOYEE & Dimension.PERIOD
                        m_employeeOrientationFlag = Alignment.HORIZONTAL
                        m_periodsOrientationFlag = Alignment.VERTICAL
                    Case Dimension.PERIOD & Dimension.EMPLOYEE
                        m_periodsOrientationFlag = Alignment.HORIZONTAL
                        m_employeeOrientationFlag = Alignment.VERTICAL
                End Select

            Case "21"           ' several periods, one product

                GetDimensionOrientations(Dimension.PERIOD, m_periodFlag, m_periodsOrientationFlag)
                If m_periodsOrientationFlag = Alignment.HORIZONTAL Then
                    m_employeeOrientationFlag = Alignment.VERTICAL
                Else
                    m_employeeOrientationFlag = Alignment.HORIZONTAL
                End If

            Case "12"           ' one period, several products
                GetDimensionOrientations(Dimension.EMPLOYEE, m_employeeFlag, m_employeeOrientationFlag)
                If m_employeeOrientationFlag = Alignment.HORIZONTAL Then
                    m_periodsOrientationFlag = Alignment.VERTICAL
                Else
                    m_periodsOrientationFlag = Alignment.HORIZONTAL
                End If

            Case "22"           ' several periods, several products
                GetDimensionOrientations(Dimension.PERIOD, m_periodFlag, m_periodsOrientationFlag)                      ' Dates Orientation
                GetDimensionOrientations(Dimension.EMPLOYEE, m_employeeFlag, m_employeeOrientationFlag)                   ' Products orientation

        End Select
        DefineGlobalOrientationFlag()
        m_processFlag = Account.AccountProcess.RH

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
        m_processFlag = Account.AccountProcess.FINANCIAL

    End Sub

    ' Define the cells at the most right and the most bottom
    Private Sub GetMaxs(ByRef p_maxRight As String, ByRef p_maxBelow As String)

        Dim l_lastPeriodCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.PERIOD)
        Dim l_lastAccountCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.ACCOUNT)
        Dim l_lastEntityCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.ENTITY)
        Dim l_lastProductCell As Excel.Range = GetLastCellOfDimensionDict(Dimension.EMPLOYEE)

        ' Max right cell identification
        Dim l_rightCellsDict As New Dictionary(Of Int16, Int32)
        l_rightCellsDict.Add(Dimension.PERIOD, l_lastPeriodCell.Column)
        l_rightCellsDict.Add(Dimension.ACCOUNT, l_lastAccountCell.Column)
        l_rightCellsDict.Add(Dimension.ENTITY, l_lastEntityCell.Column)
        l_rightCellsDict.Add(Dimension.EMPLOYEE, l_lastProductCell.Column)
        Dim l_columnsSortedDict = (From entry In l_rightCellsDict Order By entry.Value Ascending Select entry)
        p_maxRight = l_columnsSortedDict.ElementAt(0).Key

        ' Max below cell identification
        Dim l_belowCellsDict As New Dictionary(Of Int16, Int32)
        l_rightCellsDict.Add(Dimension.PERIOD, l_lastPeriodCell.Row)
        l_rightCellsDict.Add(Dimension.ACCOUNT, l_lastAccountCell.Row)
        l_rightCellsDict.Add(Dimension.ENTITY, l_lastEntityCell.Row)
        l_rightCellsDict.Add(Dimension.EMPLOYEE, l_lastProductCell.Row)
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

        If m_employeeOrientationFlag = Alignment.VERTICAL _
        AndAlso m_periodsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.EMPLOYEES_PERIODS
            Exit Sub
        End If

        If m_periodsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_employeeOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.PERIODS_EMPLOYEES
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

            Case Orientations.EMPLOYEES_PERIODS : RegisterDimensionsToCellDictionaryEMPLOYEES_PERIODS()
            Case Orientations.PERIODS_EMPLOYEES : RegisterDimensionsToCellDictionaryPERIODS_EMPLOYEES()
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

    Private Sub RegisterDimensionsToCellDictionaryEMPLOYEES_PERIODS()

        Dim l_periodColumn As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)
            l_period = l_periodAddressValuePair.Value
            l_periodColumn = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column

            For Each l_productAddressValuePair In m_dimensionsAddressValueDict(Dimension.EMPLOYEE)

                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_productAddressValuePair.Key).Row, l_periodColumn), _
                                    m_dimensionsAddressValueDict(Dimension.ENTITY).ElementAt(0).Value, _
                                    m_rhAccountName, _
                                    l_productAddressValuePair.Value, _
                                    l_period)

                ' m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value, _
                ' set BU value ?
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPERIODS_EMPLOYEES()

        Dim l_productColumn As Int32
        Dim l_productName As String

        For Each l_productAddressValuePair In m_dimensionsAddressValueDict(Dimension.EMPLOYEE)
            l_productName = l_productAddressValuePair.Value
            l_productColumn = m_excelWorkSheet.Range(l_productAddressValuePair.Key).Column

            For Each l_periodAddressValuePair In m_dimensionsAddressValueDict(Dimension.PERIOD)


                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row, l_productColumn), _
                                    m_dimensionsAddressValueDict(Dimension.ENTITY).ElementAt(0).Value, _
                                    m_rhAccountName, _
                                    l_productName, _
                                    l_periodAddressValuePair.Value)

                '  m_dimensionsAddressValueDict(Dimension.ACCOUNT).ElementAt(0).Value, _
                '  set BU value ?
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

        Dim tmpStruct As New DataSetCellDimensions
        tmpStruct.m_entityName = p_entityName
        tmpStruct.m_accountName = p_accountName
        tmpStruct.m_employee = p_productName
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
            Case Orientations.EMPLOYEES_PERIODS : RegisterDataSetCellsValues(Dimension.EMPLOYEE, Dimension.PERIOD)
            Case Orientations.PERIODS_EMPLOYEES : RegisterDataSetCellsValues(Dimension.PERIOD, Dimension.EMPLOYEE)
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
                l_datasetCell.m_client = l_cell.Value2
            Next
        Next

    End Sub

#End Region


End Class
