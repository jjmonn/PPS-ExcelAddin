''
''
''
''
''
''
''
''
''

'Imports Microsoft.Office.Interop
'Imports System.Linq

'Public Class SimpleDisplaySnapshotUI

'    Friend DataSet As CModelDataSet
'    Private WS As Excel.Worksheet
'    Private MSA As MultipleSheetsAcquisition
'    Friend pHeadersArray As Collections.Generic.Dictionary(Of String, String)                    ' Copy of "V" dictionary
'    Friend pRowsArray As Collections.Generic.Dictionary(Of String, String)                       ' Copy of "H" dictionary
'    Friend pHeadersSelection As New Collections.Generic.Dictionary(Of String, Boolean)  ' Headers Selection tracking
'    Friend pRowsSelection As New Collections.Generic.Dictionary(Of String, Boolean)   ' Rows Selection tracking
'    Private HeadersFlag As String
'    Private RowsFlag As String

'    Friend Sub New(ByRef inputWS As Excel.Worksheet, ByRef inputDS As CModelDataSet)

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call
'        Me.WS = inputWS
'        DataSet = inputDS


'    End Sub

'    Private Sub SnapShotUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'        '--------------------------------------------------------------------------
'        ' Initializes the User Form
'        '--------------------------------------------------------------------------
'        MSA = Me.Owner

'    End Sub

'    Public Sub initView()

'        DisplayDataGridView()
'        DisplaySetUp()
'        SelectionDictionariesInit()

'    End Sub


'    Private Sub DisplayDataGridView()

'        '----------------------------------------------------------------------------------
'        ' Send the right arrays to build the DataGridView Data Source
'        '----------------------------------------------------------------------------------
'        'DataSet.getDataSet()

'        If DataSet.accountsOrientation = "V" Then
'            pRowsArray = DataSet.AccountsAddressValuesDictionary
'            RowsFlag = ACCOUNT_FLAG
'        ElseIf DataSet.accountsOrientation = "H" Then
'            pHeadersArray = DataSet.AccountsAddressValuesDictionary
'            HeadersFlag = ACCOUNT_FLAG
'        Else
'            Select Case DataSet.pAccountAddressValueFlag
'                Case VALUE_FLAG : Flag1_TB.Text = DataSet.AccountsAddressValuesDictionary.ElementAt(0).Value
'                Case ADDRESS_FLAG : Flag1_TB.Text = WS.Range(DataSet.AccountsAddressValuesDictionary.ElementAt(0).Key).Value2
'            End Select
'        End If

'        If DataSet.assetsOrientation = "V" Then
'            pRowsArray = DataSet.EntitiesAddressValuesDictionary
'            RowsFlag = ASSET_FLAG
'        ElseIf DataSet.assetsOrientation = "H" Then
'            pHeadersArray = DataSet.EntitiesAddressValuesDictionary
'            HeadersFlag = ASSET_FLAG
'        Else
'            Select Case DataSet.pAssetAddressValueFlag
'                Case VALUE_FLAG : Flag1_TB.Text = DataSet.EntitiesAddressValuesDictionary.ElementAt(0).Value
'                Case ADDRESS_FLAG : Flag1_TB.Text = WS.Range(DataSet.EntitiesAddressValuesDictionary.ElementAt(0).Key).Value2
'            End Select
'        End If

'        If DataSet.datesOrientation = "V" Then
'            pRowsArray = DataSet.periodsAddressValuesDictionary
'            RowsFlag = PERIOD_FLAG
'        ElseIf DataSet.datesOrientation = "H" Then
'            pHeadersArray = DataSet.periodsAddressValuesDictionary
'            HeadersFlag = PERIOD_FLAG
'        Else
'            Select Case DataSet.pDateAddressValueFlag
'                Case VALUE_FLAG : Flag1_TB.Text = DataSet.periodsAddressValuesDictionary.ElementAt(0).Value
'                Case ADDRESS_FLAG : Flag1_TB.Text = WS.Range(DataSet.periodsAddressValuesDictionary.ElementAt(0).Key).Value2
'            End Select
'        End If

'        DataGridView1.DataSource = BuildHeadersTable(pHeadersArray, pRowsArray)

'    End Sub

'    Private Sub DisplaySetUp()

'        '--------------------------------------------------------------------------------------------
'        ' Set up the disply of the datagridview
'        '--------------------------------------------------------------------------------------------
'        DataGridView1.BackgroundColor = Drawing.Color.White
'        DataGridView1.Columns(0).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
'        DataGridView1.Columns(1).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader

'    End Sub

'    Private Sub SelectionDictionariesInit()

'        '----------------------------------------------------------------------------------
'        ' Initialize the dictionaries keeping track of the selection to zero
'        '----------------------------------------------------------------------------------
'        For Each key As String In pHeadersArray.Keys
'            pHeadersSelection.Item(key) = True
'        Next
'        For Each key As String In pRowsArray.Keys
'            pRowsSelection.Item(key) = True
'        Next

'    End Sub

'    Private Sub validate_cmd_Click(sender As Object, e As EventArgs) Handles validate_cmd.Click

'        '------------------------------------------------------------------------------
'        ' Send changes or validate the DataSet structure
'        '------------------------------------------------------------------------------
'        ' Consistency Checks ! 
'        ' Mapping if necessary !
'        RowsSelectionBuild()
'        NewPaddressesDictionaries()
'        DataSet.getDataSet()
'        ' MSA.launchBatchUplaodIdenticalStructure()

'    End Sub

'    Private Sub RowsSelectionBuild()

'        '-----------------------------------------------------------------------------------------
'        ' Build the Rows selection dictionary
'        '-----------------------------------------------------------------------------------------
'        Dim index As Integer
'        Dim key As String
'        For Each row As Data.DataRow In DataGridView1.DataSource.rows
'            key = pRowsSelection.ElementAt(index).Key
'            pRowsSelection.Item(key) = row.Item(SELECTION_COLUMN_TITLE)
'            index = index + 1
'        Next

'    End Sub

'    Private Sub NewPaddressesDictionaries()

'        '-----------------------------------------------------------------------------------------
'        ' Build new Header and rows dictionaries based on selection dictionaries and 
'        ' assign them to the right dictionaries of the ModelDataSetInstance
'        '-----------------------------------------------------------------------------------------
'        Dim TempDictionary As New Collections.Generic.Dictionary(Of String, String)
'        Select Case HeadersFlag
'            Case ACCOUNT_FLAG
'                For Each key As String In pHeadersArray.Keys
'                    If pHeadersSelection.Item(key) = True Then
'                        TempDictionary.Add(key, pHeadersArray.Item(key))
'                    End If
'                Next
'                DataSet.AccountsAddressValuesDictionary = TempDictionary
'            Case ASSET_FLAG
'                For Each key As String In pHeadersArray.Keys
'                    If pHeadersSelection.Item(key) = True Then
'                        TempDictionary.Add(key, pHeadersArray.Item(key))
'                    End If
'                Next
'                DataSet.EntitiesAddressValuesDictionary = TempDictionary
'            Case PERIOD_FLAG
'                For Each key As String In pHeadersArray.Keys
'                    If pHeadersSelection.Item(key) = True Then
'                        TempDictionary.Add(key, pHeadersArray.Item(key))
'                    End If
'                Next
'                DataSet.periodsAddressValuesDictionary = TempDictionary
'        End Select
'        pHeadersArray = Nothing
'        Dim TempDictionary2 As New Collections.Generic.Dictionary(Of String, String)

'        Select Case RowsFlag
'            Case ACCOUNT_FLAG
'                For Each key As String In pRowsArray.Keys
'                    If pRowsSelection.Item(key) = True Then
'                        TempDictionary2.Add(key, pRowsArray.Item(key))
'                    End If
'                Next
'                DataSet.AccountsAddressValuesDictionary = TempDictionary2
'            Case ASSET_FLAG
'                For Each key As String In pRowsArray.Keys
'                    If pRowsSelection.Item(key) = True Then
'                        TempDictionary2.Add(key, pRowsArray.Item(key))
'                    End If
'                Next
'                DataSet.EntitiesAddressValuesDictionary = TempDictionary2
'            Case PERIOD_FLAG
'                For Each key As String In pRowsArray.Keys
'                    If pRowsSelection.Item(key) = True Then
'                        TempDictionary2.Add(key, pRowsArray.Item(key))
'                    End If
'                Next
'                DataSet.periodsAddressValuesDictionary = TempDictionary2
'        End Select
'        pRowsArray = Nothing

'    End Sub

'    Private Sub BT_ChangeSelection_Click(sender As Object, e As EventArgs) Handles BT_ChangeSelection.Click

'        '-----------------------------------------------------------------------------------------
'        ' Launch the Snapshot Selection UI
'        '-----------------------------------------------------------------------------------------
'        Dim SelectUI As New SnapshotColumnsSelectionUI                                                     ' Selection UI
'        Me.AddOwnedForm(SelectUI)
'        SelectUI.Show()

'    End Sub

'    Private Sub BT_Edit_Click(sender As Object, e As EventArgs) Handles BT_Edit.Click

'        '----------------------------------------------------------------------------
'        ' Display a selection UI allowing the user to select the rows and columns
'        ' Relaunch the dataset initialization based on those ranges
'        '----------------------------------------------------------------------------
'        Dim TemplateEditUI As New ManualRangeSel_UI
'        Me.AddOwnedForm(TemplateEditUI)
'        Me.Hide()
'        TemplateEditUI.Show()

'    End Sub

'    Private Sub SnapShotUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

'        '-------------------------------------------------------------------------
'        ' Resize the dataGridView1 according to the new size of the UI
'        '-------------------------------------------------------------------------
'        DataGridView1.Width = Me.Width - 35
'        DataGridView1.Height = Me.Height - DataGridView1.Top - 40

'    End Sub


'End Class