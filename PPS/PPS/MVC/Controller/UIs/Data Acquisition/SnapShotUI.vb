Imports Microsoft.Office.Interop
Imports System.Linq

Public Class SnapShotUI

    Public Property DataSet As ModelDataSet
    Private WS As Excel.Worksheet
    Public Property pHeadersArray As Collections.Generic.Dictionary(Of String, String)                    ' Copy of "V" dictionary
    Public Property pRowsArray As Collections.Generic.Dictionary(Of String, String)                       ' Copy of "H" dictionary
    Public Property pHeadersSelection As New Collections.Generic.Dictionary(Of String, Boolean)  ' Headers Selection tracking
    Public Property pRowsSelection As New Collections.Generic.Dictionary(Of String, Boolean)   ' Rows Selection tracking
    Private HeadersFlag As String
    Private RowsFlag As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        WS = apps.ActiveSheet
        DataSet = New ModelDataSet()
        DataSet.getDataSet()
        DisplayDataGridView()
        DisplaySetUp()
        SelectionDictionariesInit()

    End Sub

    Private Sub SnapShotUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '--------------------------------------------------------------------------
        ' Initializes the User Form
        '--------------------------------------------------------------------------
      ' Ici check de l'existence de valeurs / sinon -> interface de selection manuelle des ranges

        'If DataSet.pAddressAssetsList.Count = 0 Then
        '    MsgBox("The application did not find any Entity, please provide an Entity and relaunch the snapshot")
        '    ' -> Proposition pour lancer le range enter manuel ? !! 
        '    Me.Close()
        'End If
        'If DataSet.pAddressAccountsList.Count = 0 Then
        '    MsgBox("The application did not find any Account, please provide an Account and relaunch the snapshot")
        '    Me.Close()
        'End If
        'If DataSet.pAddressDatesList.Count = 0 Then
        '    MsgBox("The application did not find any Period, please provide a Period and relaunch the snapshot")
        '    Me.Close()
        'End If

    End Sub

    Private Sub DisplayDataGridView()

        '----------------------------------------------------------------------------------
        ' Send the right arrays to build the DataGridView Data Source
        '----------------------------------------------------------------------------------
        DataSet.getDataSet()

        If DataSet.accountsOrientation = "V" Then
            pRowsArray = DataSet.pAddressAccountsList
            RowsFlag = ACCOUNT_FLAG
        ElseIf DataSet.accountsOrientation = "H" Then
            pHeadersArray = DataSet.pAddressAccountsList
            HeadersFlag = ACCOUNT_FLAG
        Else
            Select Case DataSet.pAccountAddressValueFlag
                Case VALUE_FLAG : Flag1_TB.Text = DataSet.pAddressAccountsList.ElementAt(0).Value
                Case ADDRESS_FLAG : Flag1_TB.Text = WS.Range(DataSet.pAddressAccountsList.ElementAt(0).Key).Value2
            End Select
        End If

        If DataSet.assetsOrientation = "V" Then
            pRowsArray = DataSet.pAddressAssetsList
            RowsFlag = ASSET_FLAG
        ElseIf DataSet.assetsOrientation = "H" Then
            pHeadersArray = DataSet.pAddressAssetsList
            HeadersFlag = ASSET_FLAG
        Else
            Select Case DataSet.pAssetAddressValueFlag
                Case VALUE_FLAG : Flag1_TB.Text = DataSet.pAddressAssetsList.ElementAt(0).Value
                Case ADDRESS_FLAG : Flag1_TB.Text = WS.Range(DataSet.pAddressAssetsList.ElementAt(0).Key).Value2
            End Select
        End If

        If DataSet.datesOrientation = "V" Then
            pRowsArray = DataSet.pAddressDatesList
            RowsFlag = PERIOD_FLAG
        ElseIf DataSet.datesOrientation = "H" Then
            pHeadersArray = DataSet.pAddressDatesList
            HeadersFlag = PERIOD_FLAG
        Else
            Select Case DataSet.pDateAddressValueFlag
                Case VALUE_FLAG : Flag1_TB.Text = DataSet.pAddressDatesList.ElementAt(0).Value
                Case ADDRESS_FLAG : Flag1_TB.Text = WS.Range(DataSet.pAddressDatesList.ElementAt(0).Key).Value2
            End Select
        End If

        DataGridView1.DataSource = GetDataTable(pHeadersArray, pRowsArray, DataSet.pDataArray)

    End Sub

    Private Sub DisplaySetUp()

        '--------------------------------------------------------------------------------------------
        ' Set up the disply of the datagridview
        '--------------------------------------------------------------------------------------------
        DataGridView1.BackgroundColor = Drawing.Color.White
        DataGridView1.Columns(0).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        DataGridView1.Columns(1).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader

    End Sub

    Private Sub SelectionDictionariesInit()

        '----------------------------------------------------------------------------------
        ' Initialize the dictionaries keeping track of the selection to zero
        '----------------------------------------------------------------------------------
        For Each key As String In pHeadersArray.Keys
            pHeadersSelection.Item(key) = True
        Next
        For Each key As String In pRowsArray.Keys
            pRowsSelection.Item(key) = True
        Next

    End Sub

    Private Sub Upload_BT_Click(sender As Object, e As EventArgs) Handles Upload_BT.Click

        '------------------------------------------------------------------------------
        ' Launch the upload to the Data base
        '------------------------------------------------------------------------------
        Dim DB As New ModelDatabase
        ' Consistency Checks ! 
        ' Mapping if necessary !
        RowsSelectionBuild()
        NewPaddressesDictionaries()
        DataSet.getDataSet()
        DB.UpdateDataBase(DataSet.pDataSet)


    End Sub

    Private Sub RowsSelectionBuild()

        '-----------------------------------------------------------------------------------------
        ' Build the Rows selection dictionary
        '-----------------------------------------------------------------------------------------
        Dim index As Integer
        Dim key As String
        For Each row As Data.DataRow In DataGridView1.DataSource.rows
            key = pRowsSelection.ElementAt(index).Key
            pRowsSelection.Item(key) = row.Item(SELECTION_COLUMN_TITLE)
            index = index + 1
        Next

    End Sub

    Private Sub NewPaddressesDictionaries()

        '-----------------------------------------------------------------------------------------
        ' Build new Header and rows dictionaries based on selection dictionaries and 
        ' assign them to the right dictionaries of the ModelDataSetInstance
        '-----------------------------------------------------------------------------------------
        Dim TempDictionary As New Collections.Generic.Dictionary(Of String, String)
        Select Case HeadersFlag
            Case ACCOUNT_FLAG
                For Each key As String In pHeadersArray.Keys
                    If pHeadersSelection.Item(key) = True Then
                        TempDictionary.Add(key, pHeadersArray.Item(key))
                    End If
                Next
                DataSet.pAddressAccountsList = TempDictionary
            Case ASSET_FLAG
                For Each key As String In pHeadersArray.Keys
                    If pHeadersSelection.Item(key) = True Then
                        TempDictionary.Add(key, pHeadersArray.Item(key))
                    End If
                Next
                DataSet.pAddressAssetsList = TempDictionary
            Case PERIOD_FLAG
                For Each key As String In pHeadersArray.Keys
                    If pHeadersSelection.Item(key) = True Then
                        TempDictionary.Add(key, pHeadersArray.Item(key))
                    End If
                Next
                DataSet.pAddressDatesList = TempDictionary
        End Select
        pHeadersArray = Nothing
        Dim TempDictionary2 As New Collections.Generic.Dictionary(Of String, String)

        Select Case RowsFlag
            Case ACCOUNT_FLAG
                For Each key As String In pRowsArray.Keys
                    If pRowsSelection.Item(key) = True Then
                        TempDictionary2.Add(key, pRowsArray.Item(key))
                    End If
                Next
                DataSet.pAddressAccountsList = TempDictionary2
            Case ASSET_FLAG
                For Each key As String In pRowsArray.Keys
                    If pRowsSelection.Item(key) = True Then
                        TempDictionary2.Add(key, pRowsArray.Item(key))
                    End If
                Next
                DataSet.pAddressAssetsList = TempDictionary2
            Case PERIOD_FLAG
                For Each key As String In pRowsArray.Keys
                    If pRowsSelection.Item(key) = True Then
                        TempDictionary2.Add(key, pRowsArray.Item(key))
                    End If
                Next
                DataSet.pAddressDatesList = TempDictionary2
        End Select
        pRowsArray = Nothing

    End Sub

    Private Sub BT_ChangeSelection_Click(sender As Object, e As EventArgs) Handles BT_ChangeSelection.Click

        '-----------------------------------------------------------------------------------------
        ' Launch the Snapshot Selection UI
        '-----------------------------------------------------------------------------------------
        Dim SelectUI As New Snapshot_SelectionUI                                                     ' Selection UI
        Me.AddOwnedForm(SelectUI)
        SelectUI.Show()
    End Sub


    Private Sub BT_Edit_Click(sender As Object, e As EventArgs) Handles BT_Edit.Click

        '----------------------------------------------------------------------------
        ' Display a selection UI allowing the user to select the rows and columns
        ' Relaunch the dataset initialization based on those ranges
        '----------------------------------------------------------------------------
        'Dim TemplateEditUI As New ManualRangeSel_UI
        'Me.AddOwnedForm(TemplateEditUI)
        'Me.Hide()
        'TemplateEditUI.Show()


    End Sub

    Private Sub SnapShotUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        '-------------------------------------------------------------------------
        ' Resize the dataGridView1 according to the new size of the UI
        '-------------------------------------------------------------------------
        DataGridView1.Width = Me.Width - 35
        DataGridView1.Height = Me.Height - DataGridView1.Top - 40

    End Sub


End Class