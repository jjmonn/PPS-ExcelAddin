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
' Last modified: 25/11/2015
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
    Friend m_dateFlag As Integer
    Friend m_accountFlag As Integer
    Friend m_EntityFlag As Integer
    Friend m_globalOrientationFlag As String
    Friend m_datesOrientationFlag As String
    Friend m_accountsOrientationFlag As String
    Friend m_entitiesOrientationFlag As String

    Private Const m_stringFlag As String = "String"
    Private Const m_accountStringFlag As String = "Account"
    Private Const m_entityStringFlag As String = "Entity"
    Private Const m_periodFormatflag As String = "Period"

    Structure DataSetCellDimensions

        Public m_accountName As String
        Public m_entityName As String
        Public m_period As String
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
        ORIENTATION_ERROR
    End Enum

    Enum Alignment
        VERTICAL = 0
        HORIZONTAL
        UNCLEAR
    End Enum

#End Region


#Region "Initialize"

    Public Sub New(inputWS As Excel.Worksheet)

        Me.m_excelWorkSheet = inputWS

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

        m_accountsAddressValuesDictionary.Clear()
        m_outputsAccountsAddressvaluesDictionary.Clear()
        m_periodsAddressValuesDictionary.Clear()
        m_entitiesAddressValuesDictionary.Clear()

        m_accountsValuesAddressDict.Clear()
        m_outputsValuesAddressDict.Clear()
        m_periodsValuesAddressDict.Clear()
        m_entitiesValuesAddressDict.Clear()

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
                        AndAlso Not m_periodsAddressValuesDictionary.ContainsValue(periodStoredAsInt) Then
                            m_periodsAddressValuesDictionary.Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), periodStoredAsInt)
                            m_periodsValuesAddressDict.Add(periodStoredAsInt, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                    ' Impossible to read cell's value.
                End Try
            Next
        Next

        ' Flag
        Select Case m_periodsAddressValuesDictionary.Count
            Case 0 : m_dateFlag = snapshotResult.ZERO
            Case 1 : m_dateFlag = snapshotResult.ONE
            Case Else : m_dateFlag = snapshotResult.SEVERAL
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
                        AndAlso Not m_accountsAddressValuesDictionary.ContainsValue(currentStr) Then
                            ' Input account
                            m_accountsAddressValuesDictionary.Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_accountsValuesAddressDict.Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        ElseIf outputsAccountsList.Contains(GlobalVariables.Accounts.GetValue(currentStr)) _
                        AndAlso m_accountsAddressValuesDictionary.ContainsValue(currentStr) = False Then
                            ' Computed Account
                            m_outputsAccountsAddressvaluesDictionary.Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_outputsValuesAddressDict.Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Next

        Select Case m_accountsAddressValuesDictionary.Count
            Case 0 : m_accountFlag = snapshotResult.ZERO
            Case 1 : m_accountFlag = snapshotResult.ONE
            Case Else : m_accountFlag = snapshotResult.SEVERAL
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
                        AndAlso Not m_entitiesAddressValuesDictionary.ContainsValue(currentStr) Then
                            m_entitiesAddressValuesDictionary.Add(GetRangeAddressFromRowAndColumn(rowIndex, columnIndex), currentStr)
                            m_entitiesValuesAddressDict.Add(currentStr, GetRangeAddressFromRowAndColumn(rowIndex, columnIndex))
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
        Next

        Select Case m_entitiesAddressValuesDictionary.Count
            Case 0 : m_EntityFlag = SnapshotResult.ZERO
            Case 1 : m_EntityFlag = snapshotResult.ONE
            Case Else : m_EntityFlag = snapshotResult.SEVERAL
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

        Dim MaxRight As String = ""
        Dim MaxBelow As String = ""
        Dim FlagsCode As String = CStr(m_dateFlag) + CStr(m_accountFlag) + CStr(m_EntityFlag)

        If m_accountFlag = SnapshotResult.ZERO _
        Or m_EntityFlag = SnapshotResult.ZERO _
        Or m_dateFlag = SnapshotResult.ZERO Then
            m_globalOrientationFlag = Orientations.ORIENTATION_ERROR
            Exit Sub
        End If

        Select Case FlagsCode
            Case "111"          ' Only one value
                GetMaxs(MaxRight, MaxBelow)
                Dim MaxsFlag As String = MaxRight + MaxBelow

                'If MaxRight = MaxBelow then '-> Interface New!

                Select Case MaxsFlag
                    Case "AssetAccount"
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    Case "DateAccount"
                        m_datesOrientationFlag = Alignment.HORIZONTAL
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    Case "AssetDate"
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                        m_datesOrientationFlag = Alignment.VERTICAL
                    Case "AccountAsset"
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    Case "AccountDate"
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                        m_datesOrientationFlag = Alignment.VERTICAL
                    Case "DateAsset"
                        m_datesOrientationFlag = Alignment.HORIZONTAL
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    Case Else
                        ' Interface New ! 
                End Select
            Case "112"                          ' 1 period, 1 Account, Several assets
                GetAssetsOrientations()
                If m_entitiesOrientationFlag = Alignment.HORIZONTAL Then
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row Then
                        m_datesOrientationFlag = Alignment.VERTICAL
                    Else
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    End If
                Else
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column Then
                        m_datesOrientationFlag = Alignment.HORIZONTAL
                    Else
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                    End If
                End If
            Case "121"                          ' 1 period, Several Accounts, 1 asset
                GetAccountsOrientations()
                If m_accountsOrientationFlag = Alignment.HORIZONTAL Then
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                        m_datesOrientationFlag = Alignment.VERTICAL
                    Else
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    End If
                Else
                    If m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                        m_datesOrientationFlag = Alignment.HORIZONTAL
                    Else
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                    End If
                End If
            Case "211"                          ' Several periods, 1 Account, 1 Asset
                GetDatesOrientations()
                If m_datesOrientationFlag = Alignment.HORIZONTAL Then
                    If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                        m_accountsOrientationFlag = Alignment.VERTICAL
                    Else
                        m_entitiesOrientationFlag = Alignment.VERTICAL
                    End If
                Else
                    If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                        m_accountsOrientationFlag = Alignment.HORIZONTAL
                    Else
                        m_entitiesOrientationFlag = Alignment.HORIZONTAL
                    End If
                End If
            Case "311"                          ' WS Period, 1 Account, 1 Asset
                If m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                    m_accountsOrientationFlag = Alignment.VERTICAL
                    m_entitiesOrientationFlag = Alignment.HORIZONTAL
                ElseIf m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row < m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row Then
                    m_accountsOrientationFlag = Alignment.HORIZONTAL
                    m_entitiesOrientationFlag = Alignment.VERTICAL
                ElseIf m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column > m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column Then
                    m_accountsOrientationFlag = Alignment.HORIZONTAL
                    m_entitiesOrientationFlag = Alignment.VERTICAL
                Else
                    m_accountsOrientationFlag = Alignment.VERTICAL
                    m_entitiesOrientationFlag = Alignment.HORIZONTAL
                End If
            Case "312"                          ' WS Period, 1 Account, Several Asset
                GetAssetsOrientations()
                If m_entitiesOrientationFlag = Alignment.HORIZONTAL Then
                    m_accountsOrientationFlag = Alignment.VERTICAL
                Else
                    m_accountsOrientationFlag = Alignment.HORIZONTAL
                End If
            Case "321"                          ' WS Period, Several Accounts, 1 Asset
                GetAccountsOrientations()
                If m_accountsOrientationFlag = Alignment.HORIZONTAL Then
                    m_entitiesOrientationFlag = Alignment.VERTICAL
                Else
                    m_entitiesOrientationFlag = Alignment.HORIZONTAL
                End If
            Case Else
                ' Case "221","212","122","222","322"
                GetDatesOrientations()                       ' Dates Orientation
                GetAccountsOrientations()                    ' Accounts orientation
                GetAssetsOrientations()                      ' Assets orientation
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

    Private Sub GetDatesOrientations()

        Dim deltaRows, deltaColumns As Integer
        If m_dateFlag = SnapshotResult.SEVERAL Then
            deltaRows = m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Row - _
                                               m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(m_periodsAddressValuesDictionary.Count - 1).Key).Column - _
                           m_excelWorkSheet.Range(m_periodsAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > 0 AndAlso deltaColumns > 0 Then
                m_datesOrientationFlag = Alignment.UNCLEAR
                Exit Sub
            End If

            If deltaRows > deltaColumns Then
                m_datesOrientationFlag = Alignment.VERTICAL
            ElseIf deltaColumns > deltaRows Then
                m_datesOrientationFlag = Alignment.HORIZONTAL
            Else
                m_datesOrientationFlag = Alignment.UNCLEAR
            End If
        Else
            m_datesOrientationFlag = Alignment.UNCLEAR
        End If

    End Sub

    Private Sub GetAccountsOrientations()

        Dim deltaRows, deltaColumns As Integer
        If m_accountFlag = SnapshotResult.SEVERAL Then
            deltaRows = m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Row - _
                        m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(m_accountsAddressValuesDictionary.Count - 1).Key).Column - _
                           m_excelWorkSheet.Range(m_accountsAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > 0 AndAlso deltaColumns > 0 Then
                m_accountsOrientationFlag = Alignment.UNCLEAR
                Exit Sub
            End If

            If deltaRows > deltaColumns Then
                m_accountsOrientationFlag = Alignment.VERTICAL
            ElseIf deltaColumns > deltaRows Then
                m_accountsOrientationFlag = Alignment.HORIZONTAL
            Else
                m_accountsOrientationFlag = Alignment.UNCLEAR
            End If
        Else
            m_accountsOrientationFlag = Alignment.UNCLEAR
        End If

    End Sub

    Private Sub GetAssetsOrientations()

        Dim deltaRows, deltaColumns As Integer
        If m_EntityFlag = SnapshotResult.SEVERAL Then
            deltaRows = m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Row - _
                        m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(0).Key).Row

            deltaColumns = m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(m_entitiesAddressValuesDictionary.Count - 1).Key).Column - _
                           m_excelWorkSheet.Range(m_entitiesAddressValuesDictionary.ElementAt(0).Key).Column

            If deltaRows > 0 AndAlso deltaColumns > 0 Then
                m_entitiesOrientationFlag = Alignment.UNCLEAR
                Exit Sub
            End If

            If deltaRows > deltaColumns Then
                m_entitiesOrientationFlag = Alignment.VERTICAL
            ElseIf deltaColumns > deltaRows Then
                m_entitiesOrientationFlag = Alignment.HORIZONTAL
            Else
                m_entitiesOrientationFlag = Alignment.UNCLEAR
            End If
        Else
            m_entitiesOrientationFlag = Alignment.UNCLEAR
        End If

    End Sub

    Private Sub DefineGlobalOrientationFlag()

        If m_accountsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_entitiesOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ACCOUNTS_ENTITIES
            Exit Sub
        End If

        If m_accountsOrientationFlag = Alignment.VERTICAL _
        AndAlso m_datesOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ACCOUNTS_PERIODS
            Exit Sub
        End If

        If m_datesOrientationFlag = Alignment.VERTICAL _
        AndAlso m_accountsOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.PERIODS_ACCOUNTS
            Exit Sub
        End If

        If m_datesOrientationFlag = Alignment.VERTICAL _
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
        AndAlso m_datesOrientationFlag = Alignment.HORIZONTAL Then
            m_globalOrientationFlag = Orientations.ENTITIES_PERIODS
            Exit Sub
        End If

        m_globalOrientationFlag = Orientations.ORIENTATION_ERROR

    End Sub

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
        End Select

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryACCOUNTS_PERIODS()

        Dim l_entityName As String = m_entitiesAddressValuesDictionary.ElementAt(0).Value
        Dim l_periodColumn As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_periodsAddressValuesDictionary
            l_period = l_periodAddressValuePair.Value
            l_periodColumn = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column

            ' Inputs cells registering
            For Each AccountAddressValuePair In m_accountsAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(AccountAddressValuePair.Key).Row, l_periodColumn), _
                                    l_entityName, _
                                    AccountAddressValuePair.Value, _
                                    l_period)
            Next

            ' Outputs cells registering
            For Each OutputAccountAddressValuePair In m_outputsAccountsAddressvaluesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(OutputAccountAddressValuePair.Key).Row, l_periodColumn), _
                                    l_entityName, _
                                    OutputAccountAddressValuePair.Value, _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPERIODS_ACCOUNTS()

        Dim l_entityName As String = m_entitiesAddressValuesDictionary.ElementAt(0).Value
        Dim l_periodRow As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_periodsAddressValuesDictionary
            l_period = l_periodAddressValuePair.Value
            l_periodRow = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row

            ' Inputs cells registering
            For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_periodRow, m_excelWorkSheet.Range(l_accountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_accountAddressValuePair.Value, _
                                    l_period)
            Next

            ' Outputs cells registering
            For Each l_outputAccountAddressValuePair In m_outputsAccountsAddressvaluesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_periodRow, m_excelWorkSheet.Range(l_outputAccountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_outputAccountAddressValuePair.Value, _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryACCOUNTS_ENTITIES()

        Dim l_period As Int32 = m_periodsAddressValuesDictionary.ElementAt(0).Value
        Dim l_entityColumn As Int32
        Dim l_entityName As String

        For Each l_entityAddressValuePair In m_entitiesAddressValuesDictionary
            l_entityName = l_entityAddressValuePair.Value
            l_entityColumn = m_excelWorkSheet.Range(l_entityAddressValuePair.Key).Column

            ' Inputs cells registering
            For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_accountAddressValuePair.Key).Row, l_entityColumn), _
                                    l_entityName, _
                                    l_accountAddressValuePair.Value, _
                                    l_period)
            Next

            ' Outputs cells registering
            For Each l_outputAccountAddressValuePair In m_outputsAccountsAddressvaluesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_outputAccountAddressValuePair.Key).Row, l_entityColumn), _
                                    l_entityName, _
                                    l_outputAccountAddressValuePair.Value, _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryENTITIES_ACCOUNTS()

        Dim l_periodId As String = m_periodsAddressValuesDictionary.ElementAt(0).Value
        Dim l_entityRow As Int32
        Dim l_entityName As String

        For Each l_entitiesAddressValuePair In m_entitiesAddressValuesDictionary
            l_entityName = l_entitiesAddressValuePair.Value
            l_entityRow = m_excelWorkSheet.Range(l_entitiesAddressValuePair.Key).Row

            ' Inputs cells registering
            For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_entityRow, m_excelWorkSheet.Range(l_accountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_accountAddressValuePair.Value, _
                                    l_periodId)
            Next

            ' Outputs cells registering
            For Each l_outputAccountAddressValuePair In m_outputsAccountsAddressvaluesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(l_entityRow, m_excelWorkSheet.Range(l_outputAccountAddressValuePair.Key).Column), _
                                    l_entityName, _
                                    l_outputAccountAddressValuePair.Value, _
                                    l_periodId)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryENTITIES_PERIODS()

        Dim l_accountName As String = m_accountsAddressValuesDictionary.ElementAt(0).Value
        Dim l_periodColumn As Int32
        Dim l_period As String

        For Each l_periodAddressValuePair In m_periodsAddressValuesDictionary
            l_period = l_periodAddressValuePair.Value
            l_periodColumn = m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column

            For Each l_entityAddressValuePair In m_entitiesAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_entityAddressValuePair.Key).Row, l_periodColumn), _
                                    l_entityAddressValuePair.Value, _
                                    l_accountName, _
                                    l_period)
            Next
        Next

    End Sub

    Private Sub RegisterDimensionsToCellDictionaryPERIODS_ENTITIES()

        Dim l_accountName As String = m_accountsAddressValuesDictionary.ElementAt(0).Value
        Dim l_entityColumn As Int32
        Dim l_entityName As String

        For Each l_entityAddressValuePair In m_entitiesAddressValuesDictionary
            l_entityName = l_entityAddressValuePair.Value
            l_entityColumn = m_excelWorkSheet.Range(l_entityAddressValuePair.Key).Column

            For Each l_periodAddressValuePair In m_periodsAddressValuesDictionary
                RegisterDatasetCell(m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row, l_entityColumn), _
                                    l_entityName, _
                                    l_accountName, _
                                    l_periodAddressValuePair.Value)
            Next
        Next

    End Sub

    ' Cells and dimensions registration utility function
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


#Region "Cells Values Registration"

    Friend Sub RegisterDataSetCellsValues()

        Select Case m_globalOrientationFlag
            Case Orientations.ACCOUNTS_PERIODS : RegisterDataSetCellsValuesACCOUNTS_PERIODS()
            Case Orientations.PERIODS_ACCOUNTS : RegisterDataSetCellsValuesPERIODS_ACCOUNTS()
            Case Orientations.ACCOUNTS_ENTITIES : RegisterDataSetCellsValuesACCOUNTS_ENTITIES()
            Case Orientations.ENTITIES_ACCOUNTS : RegisterDataSetCellsValuesENTITIES_ACCOUNTS()
            Case Orientations.ENTITIES_PERIODS : RegisterDataSetCellsValuesENTITIES_PERIODS()
            Case Orientations.PERIODS_ENTITIES : RegisterDataSetCellsValuesPERIODS_ENTITIES()
        End Select

    End Sub

    Private Sub RegisterDataSetCellsValuesACCOUNTS_PERIODS()

        Dim l_accountAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
            l_accountAddress = l_accountAddressValuePair.Key
            For Each l_periodAddressValuePair In m_periodsAddressValuesDictionary
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_accountAddress).Row, _
                                                m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Column)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub

    Private Sub RegisterDataSetCellsValuesPERIODS_ACCOUNTS()

        Dim l_accountAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
            l_accountAddress = l_accountAddressValuePair.Key
            For Each l_periodAddressValuePair In m_periodsAddressValuesDictionary
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_periodAddressValuePair.Key).Row, _
                                                m_excelWorkSheet.Range(l_accountAddress).Column)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub

    Private Sub RegisterDataSetCellsValuesACCOUNTS_ENTITIES()

        Dim l_accountAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
            l_accountAddress = l_accountAddressValuePair.Key
            For Each l_entitiesAddressValuePair In m_entitiesAddressValuesDictionary
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_accountAddress).Row, _
                                                m_excelWorkSheet.Range(l_entitiesAddressValuePair.Key).Column)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub

    Private Sub RegisterDataSetCellsValuesENTITIES_ACCOUNTS()

        Dim l_accountAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_accountAddressValuePair In m_accountsAddressValuesDictionary
            l_accountAddress = l_accountAddressValuePair.Key
            For Each l_entitiesAddressValuePair In m_entitiesAddressValuesDictionary
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_entitiesAddressValuePair.Key).Row, _
                                                m_excelWorkSheet.Range(l_accountAddress).Column)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub

    Private Sub RegisterDataSetCellsValuesENTITIES_PERIODS()

        Dim l_entityAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_entitiesAddressValuePair In m_entitiesAddressValuesDictionary
            l_entityAddress = l_entitiesAddressValuePair.Key
            For Each l_periodsAddressValuePair In m_periodsAddressValuesDictionary
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_entityAddress).Row, _
                                                m_excelWorkSheet.Range(l_periodsAddressValuePair.Key).Column)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub

    Private Sub RegisterDataSetCellsValuesPERIODS_ENTITIES()

        Dim l_entityAddress As String
        Dim l_cell As Excel.Range
        Dim l_datasetCell As DataSetCellDimensions

        For Each l_entitiesAddressValuePair In m_entitiesAddressValuesDictionary
            l_entityAddress = l_entitiesAddressValuePair.Key
            For Each l_periodsAddressValuePair In m_periodsAddressValuesDictionary
                l_cell = m_excelWorkSheet.Cells(m_excelWorkSheet.Range(l_periodsAddressValuePair.Key).Row, _
                                                m_excelWorkSheet.Range(l_entityAddress).Column)
                l_datasetCell = m_datasetCellDimensionsDictionary(l_cell.Address)
                l_datasetCell.m_value = l_cell.Value2
            Next
        Next

    End Sub


#End Region


End Class
