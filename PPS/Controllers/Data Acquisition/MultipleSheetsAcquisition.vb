'' MultipleSheetsAcquisition.vb
''
'' Manage upload for workbooks
''
'' to do : 
''           - review process (post snapshot and dataset modifications)
''
''
''
'' Last modified: 02/06/2014
''
'' Auhtor: Julien Monnereau



'Imports Microsoft.Office.Interop
'Imports System.Linq
'Imports System.Collections

'Public Class MultipleSheetsAcquisition


'    Friend DataSet As CModelDataSet


'    Public Sub New()

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        For Each ws As Excel.Worksheet In GlobalVariables.apps.ActiveWorkbook.Worksheets
'            CLB_worksheets.Items.Add(ws.Name, True)
'        Next

'    End Sub

'    Private Sub upload_cmd_Click(sender As Object, e As EventArgs) Handles upload_cmd.Click

'        '-----------------------------------------------------------------------------------
'        ' Launch worksheets batch upload/ Snapshot
'        '-----------------------------------------------------------------------------------
'        If RB_Structure.Checked = True Then

'            DataSet = New CModelDataSet(GlobalVariables.apps.Worksheets(CLB_worksheets.CheckedItems(0)))
'            DataSet.getDataSet()
'            Dim SDD As New SimpleDisplaySnapshotUI(GlobalVariables.apps.Worksheets(CLB_worksheets.CheckedItems(0)), DataSet)
'            Me.AddOwnedForm(SDD)
'            Me.Hide()
'            SDD.Show()
'            SDD.initView()

'        Else

'            For Each ws In CLB_worksheets.CheckedItems
'                'Dim SUI As New SnapShotUI(ws)
'                ' Procedure à revoir
'            Next

'        End If

'    End Sub

'    Public Sub launchBatchUplaodIdenticalStructure()

'        '----------------------------------------------------------------------
'        ' Sub triggered by validation in SimpleDisplaySnapshotUI
'        ' Iterate through selected workesheet, getDataSet and update in DB
'        '----------------------------------------------------------------------
'        Dim DB As New cDataBaseDataUploader
'        Dim ENTMAPP As New EntitiesMappingTransactions
'        Dim errorList As New Collections.Generic.List(Of String)
'        Dim EntitiesNameEditionAllowanceDictionary As Hashtable = ENTMAPP.GetEntitiesDictionary(ASSETS_NAME_VARIABLE, ASSETS_ALLOW_EDITION_VARIABLE)

'        For Each worksheetName As String In CLB_worksheets.CheckedItems

'            Dim test As String = worksheetName
'            DataSet.WS = GlobalVariables.apps.Worksheets(worksheetName)
'            DataSet.UpdateDictionariesValuesWithCurrentWS()
'            DataSet.getOrientations()
'            DataSet.getDataSet()

'            ' Here need to extend the check depending on  the data being submitted !!
'            If EntitiesNameEditionAllowanceDictionary.Item(DataSet.EntitiesAddressValuesDictionary.ElementAt(0).Value) = 1 Then
'                DB.UpdateDataBase(DataSet.dataSetDictionary)
'            Else
'                errorList.Add(worksheetName)
'            End If
'        Next

'        If errorList.Count = 0 Then
'            MsgBox("Upload Successful")
'        Else
'            Dim errorSTR As String = ""
'            For Each error_ As String In errorList
'                errorSTR = errorSTR + "-" + error_ + Chr(13)
'            Next

'            MsgBox("There have been some errors during the upload: " + Chr(13) _
'                   + " The following worksheets could not be submitted because " + Chr(13) _
'                   + "they were not allowed for edition or not present in the Structure Organization:" + Chr(13) + Chr(13) _
'                   + errorSTR + Chr(13) + Chr(13) _
'                   + "The description of the data submitted need to be reviewed or inserted in the Organization Structure.")
'        End If


'    End Sub


'    '############# Call Backs #############

'    Private Sub selectAll_cmd_Click(sender As Object, e As EventArgs) Handles selectAll_cmd.Click

'        '-------------------------------------------------------------------------------------
'        ' Select all items in the check box list
'        '-------------------------------------------------------------------------------------
'        For i As Integer = 0 To CLB_worksheets.Items.Count - 1
'            CLB_worksheets.SetItemChecked(i, True)
'        Next

'    End Sub

'    Private Sub SelectNone_cmd_Click(sender As Object, e As EventArgs) Handles SelectNone_cmd.Click

'        '-------------------------------------------------------------------------------------
'        ' Unselect all items in the check box list
'        '-------------------------------------------------------------------------------------
'        For i As Integer = 0 To CLB_worksheets.Items.Count - 1
'            CLB_worksheets.SetItemChecked(i, False)
'        Next

'    End Sub



'End Class