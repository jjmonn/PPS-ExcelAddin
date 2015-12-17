' DataModificationsTracking.vb
'
' Follows the modifications carried on in a worksheet on the data submission area while on submission mode
'
'
' Author: Julien Monnereau
' Last modified: 15/12/2015


Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Collections.Generic
Imports System.Linq
Imports System.Collections
Imports CRUD


Friend Class DataModificationsTracking

#Region "Instance Variables"

    ' Objects
    Private m_dataset As ModelDataSet
    Friend m_rangeHighlighter As RangeHighlighter
    Friend m_dataSetRegion As Range
    Friend m_outputsRegion As Range
    Friend m_cellBeingEdited As Range

    ' Variables
    Friend m_modifiedCellsList As New List(Of String)
    Private m_startPeriod As Int32
    Private m_formatCondition As FormatCondition

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_dataSet As ModelDataSet)

        m_dataset = p_dataSet
        m_rangeHighlighter = New RangeHighlighter(m_dataset.m_excelWorkSheet)
        '     m_accountsNameTypeDict = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_TYPE_VARIABLE)

    End Sub

#End Region

#Region "Dataset and Outputs Region Set up"

    ' to be simplified => we already have the information with dataset dictionaries
    ' Initializes or Reinitializes the Dataset Region according to Dataset dictionaries and orientation
    Friend Sub InitializeDataSetRegion()

        m_dataSetRegion = Nothing
        Select Case m_dataset.m_globalOrientationFlag
            Case ModelDataSet.Orientations.ACCOUNTS_PERIODS : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
            Case ModelDataSet.Orientations.PERIODS_ACCOUNTS : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT))
            Case ModelDataSet.Orientations.ACCOUNTS_ENTITIES : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY))
            Case ModelDataSet.Orientations.ENTITIES_ACCOUNTS : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT))
            Case ModelDataSet.Orientations.PERIODS_ENTITIES : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY))
            Case ModelDataSet.Orientations.ENTITIES_PERIODS : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))

            Case ModelDataSet.Orientations.EMPLOYEES_PERIODS : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
            Case ModelDataSet.Orientations.PERIODS_EMPLOYEES : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE))
            Case Else
                ' PPS error tracking
                Exit Sub
        End Select

    End Sub

    Friend Sub InitializeOutputsRegion()

        m_outputsRegion = Nothing
        If m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT).Count > 0 Then
            Select Case m_dataset.m_globalOrientationFlag
                Case ModelDataSet.Orientations.ACCOUNTS_PERIODS : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
                Case ModelDataSet.Orientations.PERIODS_ACCOUNTS : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT))
                Case ModelDataSet.Orientations.ACCOUNTS_ENTITIES : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY))
                Case ModelDataSet.Orientations.ENTITIES_ACCOUNTS : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT))
            End Select
        End If

    End Sub

    ' Loop through param dictionaries to build a Dataset Range
    Private Sub AppendDataRegionRanges(ByRef region As Range, _
                                       ByRef VDict As Dictionary(Of String, String), _
                                       ByRef HDict As Dictionary(Of String, String))

        For Each VArea As range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As range In TransformDictionaryToRange(HDict).Areas

                Dim appendRange = m_dataset.m_excelWorkSheet.Range(m_dataset.m_excelWorkSheet.Cells(VArea(1).row, HArea(1).column), _
                                                   m_dataset.m_excelWorkSheet.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column))
                If region Is Nothing Then region = appendRange
                region = GlobalVariables.apps.Union(region, appendRange)

            Next
        Next

    End Sub

    ' Transforms a dictionary Addresses | Values into a range
    Private Function TransformDictionaryToRange(ByRef inputDict As Dictionary(Of String, String)) As Range

        If inputDict.Count = 0 Then Return Nothing
        Dim rng As range
        rng = m_dataset.m_excelWorkSheet.Range(inputDict.ElementAt(0).Key)
        For Each address As String In inputDict.Keys
            rng = GlobalVariables.apps.Union(rng, m_dataset.m_excelWorkSheet.Range(address))
        Next
        Return rng

    End Function

#End Region

#Region "Modifications Tracking"

    ' Register a modification in the dictionary cellAddress | Values
    Friend Sub RegisterModification(ByRef cellAddress As String)

        If m_modifiedCellsList.Contains(cellAddress) = False Then m_modifiedCellsList.Add(cellAddress)
        m_rangeHighlighter.ColorRangeRed(cellAddress)

    End Sub

    ' Empty the modifications list and set the cells to green
    Friend Sub UnregisterModifications()

        For Each cellAddress As String In m_modifiedCellsList
            m_rangeHighlighter.ColorRangeGreen(cellAddress)
        Next
        m_modifiedCellsList.Clear()

    End Sub

    ' Revert to input/ output color
    Friend Sub DiscardModifications()

        For Each cellAddress As String In m_modifiedCellsList
            If m_rangeHighlighter.outputCellsAddresses.Contains(cellAddress) Then
                m_rangeHighlighter.ColorOutputRange(cellAddress)
            Else
                m_rangeHighlighter.ColorRangeInputBlue(cellAddress)
            End If
        Next
        m_modifiedCellsList.Clear()

    End Sub

    Friend Sub UnregisterSingleModification(ByRef cellAddress As String)

        m_modifiedCellsList.Remove(cellAddress)
        m_rangeHighlighter.ColorRangeGreen(cellAddress)

    End Sub

    Friend Function GetModificationsListCopy() As List(Of String)

        Dim tmpList As New List(Of String)
        For Each item In m_modifiedCellsList
            tmpList.Add(item)
        Next
        Return tmpList

    End Function

#End Region

#Region "Inputs/ Outputs Ranges Related Functions"

    ' Interface: Launch Items and Data Ranges Highlight
    Friend Sub HighlightItemsAndDataRanges()

        GlobalVariables.APPS.ScreenUpdating = False
        DisableConditionalFormatting(m_dataset.m_excelWorkSheet.Range(m_dataset.m_excelWorkSheet.Range("A1"), _
                                                                      m_dataset.m_lastCell))

        Dim version As Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)
        If version Is Nothing Then Exit Sub
        m_startPeriod = version.StartPeriod

        ' Headers Coloring
        HeaderRangesInputsHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT))
        HeaderRangesInputsHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY))
        HeaderRangesInputsHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
        HeaderRangesOutputsHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT))
        HeaderRangesOutputsHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE))

        ' Data coloring
        DataHighlight()
        If Not m_outputsRegion Is Nothing Then m_rangeHighlighter.ColorOutputRange(m_outputsRegion)
        GlobalVariables.APPS.ScreenUpdating = True

    End Sub

    Friend Sub UnHighlightItemsAndDataRanges()
        m_rangeHighlighter.RevertToOriginalColors()
        EnableConditionalFormatting(m_dataset.m_excelWorkSheet.Range(m_dataset.m_excelWorkSheet.Range("A1"), _
                                                                      m_dataset.m_lastCell))
    End Sub

    Friend Sub TakeOffFormatsAndEnableConditionalFormatting()
        m_rangeHighlighter.RevertToOriginalColors()
        EnableConditionalFormatting(m_dataset.m_excelWorkSheet.Range(m_dataset.m_excelWorkSheet.Range("A1"), _
                                                                      m_dataset.m_lastCell))
    End Sub

    Private Sub HeaderRangesInputsHighlight(ByRef p_addressDictionary As Dictionary(Of String, String))

        If p_addressDictionary.Count > 0 Then
            For Each l_contiguousRange As Range In TransformDictionaryToRange(p_addressDictionary).Areas
                m_rangeHighlighter.HighlightInputRange(l_contiguousRange)
            Next
        End If

    End Sub

    ' Highlight the outputs headers
    Private Sub HeaderRangesOutputsHighlight(ByRef p_addressDictionary As Dictionary(Of String, String))

        If p_addressDictionary.Count > 0 Then
            For Each l_contiguousRange As range In TransformDictionaryToRange(p_addressDictionary).Areas
                m_rangeHighlighter.ColorOutputRange(l_contiguousRange)
            Next
        End If

    End Sub

    Private Sub DataHighlight()

        Select Case m_dataset.m_globalOrientationFlag
            Case ModelDataSet.Orientations.ACCOUNTS_PERIODS : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.aCCOUNT), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
            Case ModelDataSet.Orientations.PERIODS_ACCOUNTS : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.aCCOUNT))
            Case ModelDataSet.Orientations.ACCOUNTS_ENTITIES : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.aCCOUNT), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY))
            Case ModelDataSet.Orientations.ENTITIES_ACCOUNTS : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.aCCOUNT))
            Case ModelDataSet.Orientations.PERIODS_ENTITIES : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY))
            Case ModelDataSet.Orientations.ENTITIES_PERIODS : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
            Case ModelDataSet.Orientations.EMPLOYEES_PERIODS : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD))
            Case ModelDataSet.Orientations.PERIODS_EMPLOYEES : DataAreasHighlight(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD), m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE))
            Case Else : Exit Sub
        End Select

    End Sub

    Friend Sub HighlightsFPIOutputPart()

        Dim tuple_ As Tuple(Of String, String, String, String)
        ' tuple -> entity, account, product, period
        Dim excelCell As range

        For Each tupleCellPair In m_dataset.m_datasetCellsDictionary
            tuple_ = tupleCellPair.Key
            excelCell = tupleCellPair.Value
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(tuple_.Item2)

            If l_account Is Nothing Then Continue For
            If l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT Then
                If tuple_.Item4 <> m_startPeriod Then
                    m_rangeHighlighter.ColorOutputRange(excelCell)
                End If
            End If
        Next

    End Sub

    ' Highlight data areas
    Private Sub DataAreasHighlight(ByRef p_verticalDictionary As Dictionary(Of String, String), _
                                   ByRef p_horizontalDictionary As Dictionary(Of String, String))

        If p_verticalDictionary.Count = 0 OrElse p_horizontalDictionary.Count = 0 Then Exit Sub
        For Each l_verticalArea As range In TransformDictionaryToRange(p_verticalDictionary).Areas
            For Each l_horizontalArea As range In TransformDictionaryToRange(p_horizontalDictionary).Areas
                Dim l_range = m_rangeHighlighter.WS.Range(m_rangeHighlighter.WS.Cells(l_verticalArea(1).row, l_horizontalArea(1).column), _
                                                          m_rangeHighlighter.WS.Cells(l_verticalArea(l_verticalArea.Count).row, l_horizontalArea(l_horizontalArea.Count).column))
                m_rangeHighlighter.HighlightInputRange(l_range)
            Next
        Next

    End Sub

#End Region

#Region "Initial Dataset Differences with DB"

    ' Identify differences between captured data and and current DB
    ' Param: DBInputsDictionary (from ACQMODEL-> (entity)(account)(period))
    Friend Sub IdentifyFinancialDifferencesBtwDataSetAndDB(ByRef p_dataBaseInputsDictionary As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double))))

        Dim periodIdentifyer As String = ""
        Dim version As Version = GlobalVariables.Versions.GetValue(m_dataset.m_currentVersionId)
        If version Is Nothing Then Exit Sub

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS : periodIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.MONTHS : periodIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.DAYS : periodIdentifyer = Computer.DAY_PERIOD_IDENTIFIER
        End Select

        On Error Resume Next
        For Each entity As String In m_dataset.m_dimensionsValueAddressDict(ModelDataSet.Dimension.ENTITY).Keys
            For Each elem As String In m_dataset.m_dimensionsValueAddressDict(ModelDataSet.Dimension.ACCOUNT).Keys
                Dim l_account As Account = GlobalVariables.Accounts.GetValue(elem)
                If l_account Is Nothing Then Continue For

                Select Case l_account.FormulaType
                    Case Account.FormulaTypes.FIRST_PERIOD_INPUT
                        Dim period As Integer = CInt(CDbl(m_dataset.m_periodsDatesList(0).ToOADate))
                        ' Date from dataset converted to integer to meet DB integer date storage

                        Dim tuple_ As New Tuple(Of String, String, String, String)(entity, l_account.Name, "", period)
                        If m_dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                            Dim cell As range = m_dataset.m_datasetCellsDictionary(tuple_)
                            If cell.Value2 <> p_dataBaseInputsDictionary(entity)(l_account.Name)(periodIdentifyer & period) Then _
                               RegisterModification(cell.Address)
                        End If
                    Case Else
                        For Each period As String In m_dataset.m_dimensionsValueAddressDict(ModelDataSet.Dimension.PERIOD).Keys
                            Dim tuple_ As New Tuple(Of String, String, String, String)(entity, l_account.Name, "", period)
                            If m_dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                                Dim cell As range = m_dataset.m_datasetCellsDictionary(tuple_)
                                If cell.Value2 <> p_dataBaseInputsDictionary(entity)(l_account.Name)(periodIdentifyer & period) Then
                                    RegisterModification(cell.Address)
                                End If
                            End If
                        Next
                End Select
            Next
        Next

    End Sub

    ' p_factsDictionary dimensions : : (productId)(period) -> Fact
    ' to do : use account Name ?
    Friend Sub IdentifyRHDifferencesBtwDataSetAndDB(ByRef p_accountName As String, _
                                                    ByRef p_factsDictionary As SafeDictionary(Of String, SafeDictionary(Of String, Fact)))

        Dim periodIdentifyer As String = ""
        Dim version As Version = GlobalVariables.Versions.GetValue(m_dataset.m_currentVersionId)
        If version Is Nothing Then Exit Sub

        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS : periodIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.MONTHS : periodIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.DAYS : periodIdentifyer = Computer.DAY_PERIOD_IDENTIFIER
        End Select

        On Error Resume Next
        For Each l_productName As String In m_dataset.m_dimensionsValueAddressDict(ModelDataSet.Dimension.EMPLOYEE).Keys
            For Each p_periodId As String In m_dataset.m_dimensionsValueAddressDict(ModelDataSet.Dimension.PERIOD).Keys
                ' Dimensions : (entity)(account)(product)(period)
                Dim tuple_ As New Tuple(Of String, String, String, String)("", p_accountName, l_productName, p_periodId)
                If m_dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                    Dim cell As range = m_dataset.m_datasetCellsDictionary(tuple_)
                    If p_factsDictionary.ContainsKey(l_productName) = False _
                    OrElse p_factsDictionary(l_productName).ContainsKey(periodIdentifyer & p_periodId) = False Then
                        RegisterModification(cell.Address)
                    Else
                        Dim l_client As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Client, p_factsDictionary(l_productName)(periodIdentifyer & p_periodId).ClientId)
                        If l_client Is Nothing Then Continue For
                        If cell.Value2 <> l_client.Name Then
                            RegisterModification(cell.Address)
                        End If
                    End If
                End If
            Next

        Next

    End Sub

#End Region

#Region "Conditional Formatting"

    Private Sub DisableConditionalFormatting(ByVal p_selection As Range)

        Dim l_conditionCell As Range = m_dataset.m_excelWorkSheet.Range(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD).ElementAt(0).Key)
        Dim l_conditionCellValue As String = m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD).ElementAt(0).Value
        m_formatCondition = AddConditionExpression(p_selection, "=1=1") 'l_conditionCell.Address & "=" & l_conditionCellValue
        m_formatCondition.SetFirstPriority()
        m_formatCondition.StopIfTrue = True

    End Sub

    Private Sub EnableConditionalFormatting(ByRef p_selection As Range)

        Dim f_conditions As FormatConditions = RangeFormatConditions(p_selection)
        '      f_conditions.Item(1).Delete()
        Dim fc As FormatCondition = RangeFormatConditions(f_conditions)
        '      fc.Delete()

        '    p_selection.FormatConditions(1).Delete()
        m_formatCondition.stopiftrue = False
        DeleteFormatCondition(m_formatCondition)
        '    m_formatCondition.Delete()
        ' ne marche pas !!! enlever la première rule !

    End Sub

    Public Shared Function AddConditionValue(R As Range, ConditionOperator As XlFormatConditionOperator, Formula As String) As FormatCondition
        Return CType(R.FormatConditions.GetType().InvokeMember("Add", _
                                                               System.Reflection.BindingFlags.InvokeMethod, _
                                                               Nothing, _
                                                               CType(R.FormatConditions, Object), _
                                                               New Object() {XlFormatConditionType.xlCellValue, ConditionOperator, Formula}) _
                                                               , FormatCondition)
    End Function

    Public Shared Function AddConditionExpression(R As Range, Formula As String) As FormatCondition

        Return CType(R.FormatConditions.GetType().InvokeMember("Add", _
                                                               System.Reflection.BindingFlags.InvokeMethod, _
                                                               Nothing, _
                                                               CType(R.FormatConditions, Object), _
                                                               New Object() {XlFormatConditionType.xlExpression, Type.Missing, Formula}), FormatCondition)

    End Function

    Public Shared Function RangeFormatConditions(R As Range) As FormatConditions

    
        '  Dim fc = FormatConditions.GetType().InvokeMember("AddDatabar", BindingFlags.InvokeMethod, Nothing, FormatConditions, null)

        Return CType(R.GetType().InvokeMember("FormatConditions", _
                                                System.Reflection.BindingFlags.GetProperty, _
                                                Nothing, _
                                                CType(R, Range), _
                                                New Object() {}),  _
                                                FormatConditions)

    End Function

    Public Shared Function RangeFormatConditions(p_formatConditions As FormatConditions) As FormatCondition

             Return CType(p_formatConditions.GetType().InvokeMember("Item", _
                                                               System.Reflection.BindingFlags.InvokeMethod, _
                                                                Nothing, _
                                                                CType(p_formatConditions, FormatConditions), _
                                                                New Object() {1}),  _
                                                                FormatCondition)

    End Function

    Public Shared Sub DeleteFormatCondition(p_formatCondition As FormatCondition)

        p_formatCondition.GetType().InvokeMember("delete", _
                                                 System.Reflection.BindingFlags.InvokeMethod, _
                                                 Nothing, _
                                                 CType(p_formatCondition, FormatCondition), _
                                                 New Object() {})

    End Sub

#End Region

End Class
