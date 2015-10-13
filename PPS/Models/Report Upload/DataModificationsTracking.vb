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
    Private m_dataset As ModelDataSet
    Friend m_rangeHighlighter As RangeHighlighter
    Friend m_dataSetRegion As Excel.Range
    Friend m_outputsRegion As Excel.Range
    Friend m_cellBeingEdited As Excel.Range
    '   Private m_accountsNameTypeDict As Hashtable
    Private m_accountsNameFormulaTypeDict As Hashtable

    ' Variables
    Friend m_modifiedCellsList As New List(Of String)
    Private m_startPeriod As Int32

#End Region


    Friend Sub New(ByRef p_dataSet As ModelDataSet)

        m_dataset = p_dataSet
        m_rangeHighlighter = New RangeHighlighter(m_dataset.m_excelWorkSheet)
        '     m_accountsNameTypeDict = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_TYPE_VARIABLE)
        m_accountsNameFormulaTypeDict = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)

    End Sub


#Region "Dataset and Outputs Region Set up"

    ' to be simplified => we already have the information with dataset dictionaries
    ' Initializes or Reinitializes the Dataset Region according to Dataset dictionaries and orientation
    Friend Sub InitializeDataSetRegion()

        m_dataSetRegion = Nothing
        Select Case m_dataset.m_globalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_accountsAddressValuesDictionary, m_dataset.m_periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_periodsAddressValuesDictionary, m_dataset.m_accountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_accountsAddressValuesDictionary, m_dataset.m_entitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_entitiesAddressValuesDictionary, m_dataset.m_accountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_periodsAddressValuesDictionary, m_dataset.m_entitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : AppendDataRegionRanges(m_dataSetRegion, m_dataset.m_entitiesAddressValuesDictionary, m_dataset.m_periodsAddressValuesDictionary)
            Case Else
                ' PPS error tracking
                Exit Sub
        End Select

    End Sub

    Friend Sub InitializeOutputsRegion()

        m_outputsRegion = Nothing
        If m_dataset.m_outputsAccountsAddressvaluesDictionary.Count > 0 Then
            Select Case m_dataset.m_globalOrientationFlag
                Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_outputsAccountsAddressvaluesDictionary, m_dataset.m_periodsAddressValuesDictionary)
                Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_periodsAddressValuesDictionary, m_dataset.m_outputsAccountsAddressvaluesDictionary)
                Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_outputsAccountsAddressvaluesDictionary, m_dataset.m_entitiesAddressValuesDictionary)
                Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : AppendDataRegionRanges(m_outputsRegion, m_dataset.m_entitiesAddressValuesDictionary, m_dataset.m_outputsAccountsAddressvaluesDictionary)
            End Select
        End If

    End Sub

    ' Loop through param dictionaries to build a Dataset Range
    Private Sub AppendDataRegionRanges(ByRef region As Excel.Range, _
                                       ByRef VDict As Dictionary(Of String, String), _
                                       ByRef HDict As Dictionary(Of String, String))

        For Each VArea As Excel.Range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As Excel.Range In TransformDictionaryToRange(HDict).Areas

                Dim appendRange = m_dataset.m_excelWorkSheet.Range(m_dataset.m_excelWorkSheet.Cells(VArea(1).row, HArea(1).column), _
                                                   m_dataset.m_excelWorkSheet.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column))
                If region Is Nothing Then region = appendRange
                region = GlobalVariables.apps.Union(region, appendRange)

            Next
        Next

    End Sub

    ' Transforms a dictionary Addresses | Values into a range
    Private Function TransformDictionaryToRange(ByRef inputDict As Dictionary(Of String, String)) As Excel.Range

        Dim rng As Excel.Range
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
        m_startPeriod = GlobalVariables.Versions.versions_hash(Int(My.Settings.version_id))(VERSIONS_START_PERIOD_VAR)

        ' Headers Coloring
        HeaderRangesInputsHighlight(m_dataset.m_accountsAddressValuesDictionary)
        HeaderRangesInputsHighlight(m_dataset.m_entitiesAddressValuesDictionary)
        HeaderRangesInputsHighlight(m_dataset.m_periodsAddressValuesDictionary)
        HeaderRangesOutputsHighlight(m_dataset.m_outputsAccountsAddressvaluesDictionary)

        ' Data coloring
        DataHighlight()
        If Not m_outputsRegion Is Nothing Then m_rangeHighlighter.ColorOutputRange(m_outputsRegion)
        GlobalVariables.APPS.ScreenUpdating = True

    End Sub

    Friend Sub UnHighlightItemsAndDataRanges()

        m_rangeHighlighter.RevertToOriginalColors()

    End Sub

    Friend Sub TakeOffFormats()

        m_rangeHighlighter.RevertToOriginalColors()

    End Sub

    Private Sub HeaderRangesInputsHighlight(ByRef addressDictionary As Dictionary(Of String, String))

        For Each contiguousRange As Excel.Range In TransformDictionaryToRange(addressDictionary).Areas
            m_rangeHighlighter.HighlightInputRange(contiguousRange)
        Next

    End Sub

    ' Highlight the outputs headers
    Private Sub HeaderRangesOutputsHighlight(ByRef addressDictionary As Dictionary(Of String, String))

        For Each contiguousRange As Excel.Range In TransformDictionaryToRange(addressDictionary).Areas
            m_rangeHighlighter.ColorOutputRange(contiguousRange)
        Next

    End Sub

    Private Sub DataHighlight()

        Select Case m_dataset.m_globalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : DataAreasHighlight(m_dataset.m_accountsAddressValuesDictionary, m_dataset.m_periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : DataAreasHighlight(m_dataset.m_periodsAddressValuesDictionary, m_dataset.m_accountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : DataAreasHighlight(m_dataset.m_accountsAddressValuesDictionary, m_dataset.m_entitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : DataAreasHighlight(m_dataset.m_entitiesAddressValuesDictionary, m_dataset.m_accountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : DataAreasHighlight(m_dataset.m_periodsAddressValuesDictionary, m_dataset.m_entitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : DataAreasHighlight(m_dataset.m_entitiesAddressValuesDictionary, m_dataset.m_periodsAddressValuesDictionary)
            Case Else : Exit Sub
        End Select

    End Sub

    Friend Sub HighlightsFPIOutputPart()

        Dim tuple_ As Tuple(Of String, String, String)
        Dim excelCell As Excel.Range

        For Each tupleCellPair In m_dataset.m_datasetCellsDictionary
            tuple_ = tupleCellPair.Key
            excelCell = tupleCellPair.Value

            If m_accountsNameFormulaTypeDict(tuple_.Item2) = GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT Then
                If tuple_.Item3 <> m_startPeriod Then
                    m_rangeHighlighter.ColorOutputRange(excelCell)
                End If
            End If
        Next

    End Sub

    ' Highlight data areas
    Private Sub DataAreasHighlight(ByRef VDict As Dictionary(Of String, String), ByRef HDict As Dictionary(Of String, String))

        For Each VArea As Excel.Range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As Excel.Range In TransformDictionaryToRange(HDict).Areas
                m_rangeHighlighter.HighlightInputRange(m_rangeHighlighter.WS.Range(m_rangeHighlighter.WS.Cells(VArea(1).row, HArea(1).column), _
                                                                               m_rangeHighlighter.WS.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column)))
            Next
        Next

    End Sub

#End Region


#Region "Initial Dataset Differences with DB"

    ' Identify differences between captured data and and current DB
    ' Param: DBInputsDictionary (from ACQMODEL-> (entity)(account)(period))
    Friend Sub IdentifyDifferencesBtwDataSetAndDB(ByRef p_dataBaseInputsDictionary As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double))))

        Dim periodIdentifyer As String = ""
        Select Case GlobalVariables.Versions.versions_hash(m_dataset.m_currentVersionId)(VERSIONS_TIME_CONFIG_VARIABLE)
            Case GlobalEnums.TimeConfig.YEARS : periodIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case GlobalEnums.TimeConfig.MONTHS : periodIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
        End Select

        Dim accountsNameTypeDict As Hashtable = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        For Each entity As String In m_dataset.m_entitiesValuesAddressDict.Keys
            For Each account As String In m_dataset.m_accountsValuesAddressDict.Keys

                Select Case accountsNameTypeDict(account)
                    Case GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT
                        Dim period As Integer = CInt(CDbl(m_dataset.m_periodsDatesList(0).ToOADate))
                        ' Date from dataset converted to integer to meet DB integer date storage

                        Dim tuple_ As New Tuple(Of String, String, String)(entity, account, period)
                        If m_dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                            Dim cell As Excel.Range = m_dataset.m_datasetCellsDictionary(tuple_)
                            If cell.Value2 <> p_dataBaseInputsDictionary(entity)(account)(periodIdentifyer & period) Then _
                               RegisterModification(cell.Address)
                        End If
                    Case Else
                        For Each period As String In m_dataset.m_periodsValuesAddressDict.Keys
                            Dim tuple_ As New Tuple(Of String, String, String)(entity, account, period)
                            If m_dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                                Dim cell As Excel.Range = m_dataset.m_datasetCellsDictionary(tuple_)
                                If cell.Value2 <> p_dataBaseInputsDictionary(entity)(account)(periodIdentifyer & period) Then
                                    RegisterModification(cell.Address)
                                End If
                            End If
                        Next
                End Select
            Next
        Next

    End Sub


#End Region


End Class
