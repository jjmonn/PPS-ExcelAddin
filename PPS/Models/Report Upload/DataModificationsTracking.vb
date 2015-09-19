' DataModificationsTracking.vb
'
' Follows the modifications carried on in a worksheet on the data submission area while on submission mode
'
'
'
' To do: 
'       - Modification registering -> version from WS or version BT
'       
'
'
' Known bugs
'       - if orientation <> Ac|Pe DBInputsDictionary not loaded ?
'
'
' Author: Julien Monnereau
' Last modified: 01/09/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Linq
Imports System.Collections


Friend Class DataModificationsTracking


#Region "Instance Variables"

    ' Objects
    Private Dataset As ModelDataSet
    Friend RangeHighlighter As RangeHighlighter
    Friend dataSetRegion As Excel.Range
    Friend outputsRegion As Excel.Range
    Friend CellBeingEdited As Excel.Range
    Private accountsNameTypeDict As Hashtable

    ' Variables
    Friend modifiedCellsList As New List(Of String)

#End Region


    Friend Sub New(ByRef inputDataSet As ModelDataSet)

        Dataset = inputDataSet
        RangeHighlighter = New RangeHighlighter(Dataset.WS)
        accountsNameTypeDict = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_TYPE_VARIABLE)

    End Sub


#Region "Dataset and Outputs Region Set up"

    ' Initializes or Reinitializes the Dataset Region according to Dataset dictionaries and orientation
    Friend Sub InitializeDataSetRegion()

        dataSetRegion = Nothing
        Select Case Dataset.GlobalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : AppendDataRegionRanges(dataSetRegion, Dataset.AccountsAddressValuesDictionary, Dataset.periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : AppendDataRegionRanges(dataSetRegion, Dataset.periodsAddressValuesDictionary, Dataset.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : AppendDataRegionRanges(dataSetRegion, Dataset.AccountsAddressValuesDictionary, Dataset.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : AppendDataRegionRanges(dataSetRegion, Dataset.EntitiesAddressValuesDictionary, Dataset.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : AppendDataRegionRanges(dataSetRegion, Dataset.periodsAddressValuesDictionary, Dataset.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : AppendDataRegionRanges(dataSetRegion, Dataset.EntitiesAddressValuesDictionary, Dataset.periodsAddressValuesDictionary)
            Case Else
                ' PPS error tracking
                Exit Sub
        End Select

    End Sub

    Friend Sub InitializeOutputsRegion()

        outputsRegion = Nothing
        If Dataset.OutputsAccountsAddressvaluesDictionary.Count > 0 Then
            Select Case Dataset.GlobalOrientationFlag
                Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : AppendDataRegionRanges(outputsRegion, Dataset.OutputsAccountsAddressvaluesDictionary, Dataset.periodsAddressValuesDictionary)
                Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : AppendDataRegionRanges(outputsRegion, Dataset.periodsAddressValuesDictionary, Dataset.OutputsAccountsAddressvaluesDictionary)
                Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : AppendDataRegionRanges(outputsRegion, Dataset.OutputsAccountsAddressvaluesDictionary, Dataset.EntitiesAddressValuesDictionary)
                Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : AppendDataRegionRanges(outputsRegion, Dataset.EntitiesAddressValuesDictionary, Dataset.OutputsAccountsAddressvaluesDictionary)
            End Select
        End If

    End Sub

    ' Loop through param dictionaries to build a Dataset Range
    Private Sub AppendDataRegionRanges(ByRef region As Excel.Range, _
                                       ByRef VDict As Dictionary(Of String, String), _
                                       ByRef HDict As Dictionary(Of String, String))

        For Each VArea As Excel.Range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As Excel.Range In TransformDictionaryToRange(HDict).Areas

                Dim appendRange = Dataset.WS.Range(Dataset.WS.Cells(VArea(1).row, HArea(1).column), _
                                                   Dataset.WS.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column))
                If region Is Nothing Then region = appendRange
                region = GlobalVariables.apps.Union(region, appendRange)

            Next
        Next

    End Sub

    ' Transforms a dictionary Addresses | Values into a range
    Private Function TransformDictionaryToRange(ByRef inputDict As Dictionary(Of String, String)) As Excel.Range

        Dim rng As Excel.Range
        rng = Dataset.WS.Range(inputDict.ElementAt(0).Key)
        For Each address As String In inputDict.Keys
            rng = GlobalVariables.apps.Union(rng, Dataset.WS.Range(address))
        Next

        Return rng

    End Function


#End Region


#Region "Modifications Tracking"

    ' Register a modification in the dictionary cellAddress | Values
    Friend Sub RegisterModification(ByRef cellAddress As String)

        If modifiedCellsList.Contains(cellAddress) = False Then modifiedCellsList.Add(cellAddress)
        RangeHighlighter.ColorRangeRed(cellAddress)

    End Sub

    ' Empty the modifications list and set the cells to green
    Friend Sub UnregisterModifications()

        For Each cellAddress As String In modifiedCellsList
            RangeHighlighter.ColorRangeGreen(cellAddress)
        Next
        modifiedCellsList.Clear()

    End Sub

    ' Revert to input/ output color
    Friend Sub DiscardModifications()

        For Each cellAddress As String In modifiedCellsList
             If RangeHighlighter.outputCellsAddresses.Contains(cellAddress) Then
                RangeHighlighter.ColorOutputRange(cellAddress)
            Else
                RangeHighlighter.ColorRangeInputBlue(cellAddress)
            End If
        Next
        modifiedCellsList.Clear()

    End Sub

    Friend Sub UnregisterSingleModification(ByRef cellAddress As String)

        modifiedCellsList.Remove(cellAddress)
        RangeHighlighter.ColorRangeGreen(cellAddress)

    End Sub

    Friend Function GetModificationsListCopy() As List(Of String)

        Dim tmpList As New List(Of String)
        For Each item In modifiedCellsList
            tmpList.Add(item)
        Next
        Return tmpList

    End Function

#End Region


#Region "Inputs/ Outputs Ranges Related Functions"

    ' Interface: Launch Items and Data Ranges Highlight
    Friend Sub HighlightItemsAndDataRanges()

        GlobalVariables.APPS.ScreenUpdating = False
        HeaderRangesInputsHighlight(Dataset.EntitiesAddressValuesDictionary)
        HeaderRangesInputsHighlight(Dataset.AccountsAddressValuesDictionary)
        HeaderRangesInputsHighlight(Dataset.periodsAddressValuesDictionary)
        DataHighlight()
        If Not outputsRegion Is Nothing Then RangeHighlighter.ColorOutputRange(outputsRegion)
        HeaderRangesOutputsHighlight(Dataset.OutputsAccountsAddressvaluesDictionary)
        GlobalVariables.APPS.ScreenUpdating = True

    End Sub

    Friend Sub UnHighlightItemsAndDataRanges()

        RangeHighlighter.RevertToOriginalColors()

    End Sub

    Friend Sub TakeOffFormats()

        RangeHighlighter.RevertToOriginalColors()

    End Sub

    ' Highlight the input headers
    Private Sub HeaderRangesInputsHighlight(ByRef addressDictionary As Dictionary(Of String, String))

        For Each contiguousRange As Excel.Range In TransformDictionaryToRange(addressDictionary).Areas
            RangeHighlighter.HighlightInputRange(contiguousRange)
        Next

    End Sub

    ' Highlight the outputs headers
    Private Sub HeaderRangesOutputsHighlight(ByRef addressDictionary As Dictionary(Of String, String))

        For Each contiguousRange As Excel.Range In TransformDictionaryToRange(addressDictionary).Areas
            RangeHighlighter.ColorOutputRange(contiguousRange)
        Next

    End Sub

    ' Launch highlight data according to orientations
    Private Sub DataHighlight()

        Select Case Dataset.GlobalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : DataAreasHighlight(Dataset.AccountsAddressValuesDictionary, Dataset.periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : DataAreasHighlight(Dataset.periodsAddressValuesDictionary, Dataset.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : DataAreasHighlight(Dataset.AccountsAddressValuesDictionary, Dataset.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : DataAreasHighlight(Dataset.EntitiesAddressValuesDictionary, Dataset.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : DataAreasHighlight(Dataset.periodsAddressValuesDictionary, Dataset.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : DataAreasHighlight(Dataset.EntitiesAddressValuesDictionary, Dataset.periodsAddressValuesDictionary)
            Case Else : Exit Sub
        End Select

    End Sub

    ' Highlight data areas
    Private Sub DataAreasHighlight(ByRef VDict As Dictionary(Of String, String), ByRef HDict As Dictionary(Of String, String))

        For Each VArea As Excel.Range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As Excel.Range In TransformDictionaryToRange(HDict).Areas
                RangeHighlighter.HighlightInputRange(RangeHighlighter.WS.Range(RangeHighlighter.WS.Cells(VArea(1).row, HArea(1).column), _
                                                                               RangeHighlighter.WS.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column)))
            Next
        Next

    End Sub

#End Region


#Region "Initial Dataset Differences with DB"

    ' Identify differences between captured data and and current DB
    ' Param: DBInputsDictionary (from ACQMODEL-> (entity)(account)(period))
    Friend Sub IdentifyDifferencesBtwDataSetAndDB(ByRef p_dataBaseInputsDictionary As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double))))

        Dim periodIdentifyer As String = ""
        Select Case GlobalVariables.Versions.versions_hash(Dataset.m_currentVersionId)(VERSIONS_TIME_CONFIG_VARIABLE)
            Case GlobalEnums.TimeConfig.YEARS : periodIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case GlobalEnums.TimeConfig.MONTHS : periodIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
        End Select

        Dim accountsNameTypeDict As Hashtable = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        For Each entity As String In Dataset.EntitiesValuesAddressDict.Keys
            For Each account As String In Dataset.AccountsValuesAddressDict.Keys

                Select Case accountsNameTypeDict(account)
                    Case GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT
                        Dim period As Integer = CInt(CDbl(Dataset.m_periodsDatesList(0).ToOADate))
                        ' Date from dataset converted to integer to meet DB integer date storage

                        Dim tuple_ As New Tuple(Of String, String, String)(entity, account, period)
                        If Dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                            Dim cell As Excel.Range = Dataset.m_datasetCellsDictionary(tuple_)
                            If cell.Value2 <> p_dataBaseInputsDictionary(entity)(account)(periodIdentifyer & period) Then _
                               RegisterModification(cell)
                        End If
                    Case Else
                        For Each period As String In Dataset.periodsValuesAddressDict.Keys
                            Dim tuple_ As New Tuple(Of String, String, String)(entity, account, period)
                            If Dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                                Dim cell As Excel.Range = Dataset.m_datasetCellsDictionary(tuple_)
                                If cell.Value2 <> p_dataBaseInputsDictionary(entity)(account)(periodIdentifyer & period) Then
                                    RegisterModification(cell)
                                End If
                            End If
                        Next
                End Select
            Next
        Next

    End Sub


#End Region


#Region "Utilities"


    ' Return the excel cell from items
    '    Friend Function GetExcelCell(ByRef entity As String, ByRef account As String, ByRef period As String) As Excel.Range

    '        Dim entityAddress, accountAddress, periodAddress As String
    '        On Error GoTo errorHandler

    '        entityAddress = Dataset.EntitiesValuesAddressDict(entity)
    '        periodAddress = Dataset.periodsValuesAddressDict(period)
    '        If Dataset.AccountsValuesAddressDict.ContainsKey(account) Then
    '            accountAddress = Dataset.AccountsValuesAddressDict(account)
    '        Else
    '            accountAddress = Dataset.OutputsValuesAddressDict(account)
    '        End If
    '        Dim tuple_ As New Tuple(Of String, String, String)()
    '        Return Dataset.GetCellFromItem(entityAddress, accountAddress, periodAddress)
    '        ' PPS Error tracking ' priority normal
    '        ' function could be merged with SubmissionWSController UpdateExcelCell

    'errorHandler:
    '        System.Diagnostics.Debug.WriteLine("DataModification Tacking Get Excel Cell raised an error: a name was not found in dataset values address dictionary.")
    '        Return Nothing


    '    End Function


#End Region




End Class
