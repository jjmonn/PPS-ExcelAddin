' CDataModificationsTracking.vb
'
' Follows the modifications carried on in a worksheet on the data submission area while on submission mode
'
'
'
' To do: 
'       - Modification registering -> version from WS or version BT
'
'
' Known bugs
'       - if orientation <> Ac|Pe DBInputsDictionary not loaded ?
'
'
' Author: Julien Monnereau
' Last modified: 08/01/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Linq
Imports System.Collections


Friend Class CDataModificationsTracking


#Region "Instance Variables"

    ' Objects
    Private DATASET As ModelDataSet
    Friend RANGEHIGHLIGHTER As CRangeHighlighter
    Friend dataSetRegion As Excel.Range
    Friend outputsRegion As Excel.Range
    Friend CellBeingEdited As Excel.Range

    ' Variables
    'Friend modififiedCellsAddressValuesDictionary As New Dictionary(Of String, Hashtable)
    Friend modifiedCellsList As New List(Of String)

#Region "Constants"

    Public Const MOD_ENTITY As String = ""
    Public Const MOD_ACCOUNT As String = ""
    Public Const MOD_PERIOD As String = ""
    Public Const MOD_VALUE As String = ""
    Public Const MOD_VERSION As String = ""

#End Region


#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As ModelDataSet)

        DATASET = inputDataSet
        RANGEHIGHLIGHTER = New CRangeHighlighter(DATASET.WS)

    End Sub


#End Region


#Region "DataSet and Outputs Region Set up"

    ' Initializes or Reinitializes the Dataset Region according to dataset dictionaries and orientation
    Friend Sub InitializeDataSetRegion()

        Select Case DATASET.GlobalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : AppendDataRegionRanges(dataSetRegion, DATASET.AccountsAddressValuesDictionary, DATASET.periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : AppendDataRegionRanges(dataSetRegion, DATASET.periodsAddressValuesDictionary, DATASET.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : AppendDataRegionRanges(dataSetRegion, DATASET.AccountsAddressValuesDictionary, DATASET.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : AppendDataRegionRanges(dataSetRegion, DATASET.EntitiesAddressValuesDictionary, DATASET.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : AppendDataRegionRanges(dataSetRegion, DATASET.periodsAddressValuesDictionary, DATASET.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : AppendDataRegionRanges(dataSetRegion, DATASET.EntitiesAddressValuesDictionary, DATASET.periodsAddressValuesDictionary)
            Case Else
                ' PPS error tracking
                Exit Sub
        End Select

    End Sub

    Friend Sub InitializeOutputsRegion()

        Select Case DATASET.GlobalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : AppendDataRegionRanges(outputsRegion, DATASET.OutputsAccountsAddressvaluesDictionary, DATASET.periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : AppendDataRegionRanges(outputsRegion, DATASET.periodsAddressValuesDictionary, DATASET.OutputsAccountsAddressvaluesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : AppendDataRegionRanges(outputsRegion, DATASET.OutputsAccountsAddressvaluesDictionary, DATASET.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : AppendDataRegionRanges(outputsRegion, DATASET.EntitiesAddressValuesDictionary, DATASET.OutputsAccountsAddressvaluesDictionary)
        End Select

    End Sub


    ' Loop through param dictionaries to build a dataset Range
    Private Sub AppendDataRegionRanges(ByRef region As Excel.Range, _
                                       ByRef VDict As Dictionary(Of String, String), _
                                       ByRef HDict As Dictionary(Of String, String))

        For Each VArea As Excel.Range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As Excel.Range In TransformDictionaryToRange(HDict).Areas

                Dim appendRange = DATASET.WS.Range(DATASET.WS.Cells(VArea(1).row, HArea(1).column), _
                                                   DATASET.WS.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column))
                If region Is Nothing Then region = appendRange
                region = GlobalVariables.apps.Union(region, appendRange)

            Next
        Next
        ' GlobalVariables.apps.ActiveSheet.protect()

    End Sub

    ' Transforms a dictionary Addresses | Values into a range
    Private Function TransformDictionaryToRange(ByRef inputDict As Dictionary(Of String, String)) As Excel.Range

        Dim rng As Excel.Range
        rng = DATASET.WS.Range(inputDict.ElementAt(0).Key)
        For Each address As String In inputDict.Keys
            rng = GlobalVariables.apps.Union(rng, DATASET.WS.Range(address))
        Next

        Return rng

    End Function


#End Region


#Region "Modifications Tracking"

    ' Register a modification in the dictionary cellAddress | Values
    Friend Sub RegisterModification(ByRef cellAddress As String)

        If modifiedCellsList.Contains(cellAddress) = False Then modifiedCellsList.Add(cellAddress)
        RANGEHIGHLIGHTER.FillCellInred(cellAddress)

    End Sub

    ' Empty the modifications list and set the cells to green
    Friend Sub UnregisterModifications()

        For Each cellAddress As String In modifiedCellsList
            RANGEHIGHLIGHTER.FillCellInGreen(cellAddress)
        Next
        modifiedCellsList.Clear()

    End Sub

    Friend Sub UnregisterSingleModification(ByRef cellAddress As String)

        modifiedCellsList.Remove(cellAddress)
        RANGEHIGHLIGHTER.FillCellInGreen(cellAddress)

    End Sub

    Friend Function GetModificationsListCopy() As List(Of String)

        Dim tmpList As New List(Of String)
        For Each item In modifiedCellsList
            tmpList.Add(item)
        Next
        Return tmpList

    End Function

#End Region


#Region "Inputs Ranges Related Functions"

    ' Interface: Launch Items and Data Ranges Highlight
    Friend Sub HighlightItemsAndDataRanges()

        HeaderRangesIdentify(DATASET.EntitiesAddressValuesDictionary)
        HeaderRangesIdentify(DATASET.AccountsAddressValuesDictionary)
        HeaderRangesIdentify(DATASET.periodsAddressValuesDictionary)

        DataHighlight()

        RANGEHIGHLIGHTER.WS.Protect(, True, False, False, False, True, True, True, True, True, True, True, True, True, True, True)

    End Sub

    Friend Sub UnHighlightItemsAndDataRanges()
        RANGEHIGHLIGHTER.WS.Unprotect()
        RANGEHIGHLIGHTER.DeleteBlueBorders()
    End Sub

    ' Highlight the input ranges
    Private Sub HeaderRangesIdentify(ByRef addressDictionary As Dictionary(Of String, String))

        For Each contiguousRange As Excel.Range In TransformDictionaryToRange(addressDictionary).Areas
            RANGEHIGHLIGHTER.DrawBlueBorderAroundRange(contiguousRange)
        Next

    End Sub

    ' Launch highlight data according to orientations
    Private Sub DataHighlight()

        Select Case DATASET.GlobalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : DataAreasHighlight(DATASET.AccountsAddressValuesDictionary, DATASET.periodsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : DataAreasHighlight(DATASET.periodsAddressValuesDictionary, DATASET.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : DataAreasHighlight(DATASET.AccountsAddressValuesDictionary, DATASET.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : DataAreasHighlight(DATASET.EntitiesAddressValuesDictionary, DATASET.AccountsAddressValuesDictionary)
            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : DataAreasHighlight(DATASET.periodsAddressValuesDictionary, DATASET.EntitiesAddressValuesDictionary)
            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : DataAreasHighlight(DATASET.EntitiesAddressValuesDictionary, DATASET.periodsAddressValuesDictionary)
            Case Else : Exit Sub
        End Select

    End Sub

    ' Highlight data areas
    Private Sub DataAreasHighlight(ByRef VDict As Dictionary(Of String, String), ByRef HDict As Dictionary(Of String, String))

        For Each VArea As Excel.Range In TransformDictionaryToRange(VDict).Areas
            For Each HArea As Excel.Range In TransformDictionaryToRange(HDict).Areas
                RANGEHIGHLIGHTER.DrawBlueBorderAroundRange(RANGEHIGHLIGHTER.WS.Range(RANGEHIGHLIGHTER.WS.Cells(VArea(1).row, HArea(1).column), _
                                                                                     RANGEHIGHLIGHTER.WS.Cells(VArea(VArea.Count).row, HArea(HArea.Count).column)))
            Next
        Next

    End Sub


#End Region


#Region "Initial DataSet Differences with DB"

    ' Identify differences between captured data and and current DB
    ' Param: DBInputsDictionary (from ACQMODEL-> (entity)(account)(period))
    Friend Sub IdentifyDifferencesBtwDataSetAndDB(ByRef DBInputsDictionary As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double))))

        Dim accountsNameTypeDict As Hashtable = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        For Each entity As String In DATASET.dataSetDictionary.Keys
            For Each account As String In DATASET.dataSetDictionary(entity).Keys

                Select Case accountsNameTypeDict(account)
                    Case BALANCE_SHEET_ACCOUNT_FORMULA_TYPE
                        Dim period As Integer = CInt(CDbl(DATASET.periodsDatesList(0).ToOADate))        ' date from dataset converted to integer to meet DB integer date storage
                        If DATASET.dataSetDictionary(entity)(account)(period) <> DBInputsDictionary(entity)(account)(period) Then _
                           RegisterModification(GetExcelCell(entity, account, period).Address)

                    Case Else
                        For Each period As String In DATASET.dataSetDictionary(entity)(account).Keys
                            If DATASET.dataSetDictionary(entity)(account)(period) <> DBInputsDictionary(entity)(account)(period) Then
                                RegisterModification(GetExcelCell(entity, account, period).Address)
                            End If
                        Next
                End Select
            Next
        Next

    End Sub


#End Region


#Region "Utilities"


    ' Return the excel cell from items
    Friend Function GetExcelCell(ByRef entity As String, ByRef account As String, ByRef period As String) As Excel.Range

        Dim entityAddress, accountAddress, periodAddress As String
        If DATASET.EntitiesAddressValuesDictionary.ContainsValue(entity) Then entityAddress = SubmissionWSController.GetDictionaryKey(DATASET.EntitiesAddressValuesDictionary, entity)
        If DATASET.AccountsAddressValuesDictionary.ContainsValue(account) Then
            accountAddress = SubmissionWSController.GetDictionaryKey(DATASET.AccountsAddressValuesDictionary, account)
        ElseIf DATASET.OutputsAccountsAddressvaluesDictionary.ContainsValue(account) Then
            accountAddress = SubmissionWSController.GetDictionaryKey(DATASET.OutputsAccountsAddressvaluesDictionary, account)
        End If
        If DATASET.periodsAddressValuesDictionary.ContainsValue(period) Then periodAddress = SubmissionWSController.GetDictionaryKey(DATASET.periodsAddressValuesDictionary, period)

        If Not entityAddress Is Nothing AndAlso Not accountAddress Is Nothing AndAlso Not periodAddress Is Nothing Then
            Return DATASET.GetCellFromItem(entityAddress, accountAddress, periodAddress)
        Else
            ' PPS Error tracking
            Return Nothing
        End If

    End Function


#End Region




End Class
