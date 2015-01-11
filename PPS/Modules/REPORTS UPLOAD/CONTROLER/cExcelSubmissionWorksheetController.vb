' cExcelSubmissionWorksheetController.vb
'
' Controls worksheet connected to GRS -> events / update cells ... etc
'
' To do: 
'       - 
'       - 
'
' Known bugs: 
'       - 
'
' Author: Julien Monnereau
' Last modified: 22/10/2014


Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VIBlend.WinForms.DataGridView


Friend Class cExcelSubmissionWorksheetController


#Region "Instance Variables"

    ' Objects
    Private DATASET As CModelDataSet
    Private ACQMODEL As CAcquisitionModel
    Private DATAMODTRACKER As CDataModificationsTracking
    Private GRS As CGeneralReportSubmissionControler

    ' Variables
    Private disableWSChange As Boolean

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputGRS As CGeneralReportSubmissionControler, _
                   ByRef inputDataSet As CModelDataSet, _
                   ByRef inputAcquisitionModel As CAcquisitionModel, _
                   ByRef inputDataModificationTracker As CDataModificationsTracking)

        GRS = inputGRS
        DATASET = inputDataSet
        ACQMODEL = inputAcquisitionModel
        DATAMODTRACKER = inputDataModificationTracker

    End Sub

    Friend Sub AssociateWS(ByRef WS As Excel.Worksheet)

        AddHandler WS.Change, AddressOf Worksheet_Change
        AddHandler WS.BeforeRightClick, AddressOf Worksheet_BeforeRightClick

    End Sub

#End Region


#Region "Update Worksheet"

    ' Update the value on the excel worksheet with the value edited in the DGV
    Friend Function UpdateExcelWS(ByRef entity As String, _
                                  ByRef account As String, _
                                  ByRef period As String, _
                                  ByRef value As Double) As Excel.Range

        Dim entityAddress, accountAddress, periodAddress As String
        If DATASET.EntitiesAddressValuesDictionary.ContainsValue(entity) Then entityAddress = GetDictionaryKey(DATASET.EntitiesAddressValuesDictionary, entity)
        If DATASET.AccountsAddressValuesDictionary.ContainsValue(account) Then
            accountAddress = GetDictionaryKey(DATASET.AccountsAddressValuesDictionary, account)
        ElseIf DATASET.OutputsAccountsAddressvaluesDictionary.ContainsValue(account) Then
            accountAddress = GetDictionaryKey(DATASET.OutputsAccountsAddressvaluesDictionary, account)
        End If

        If DATASET.periodsAddressValuesDictionary.ContainsValue(period) Then periodAddress = GetDictionaryKey(DATASET.periodsAddressValuesDictionary, period)

        If Not entityAddress Is Nothing AndAlso Not accountAddress Is Nothing AndAlso Not periodAddress Is Nothing Then
            DATASET.UpdateExcelCell(entityAddress, accountAddress, periodAddress, value)
            Return DATASET.GetCellFromItem(entityAddress, accountAddress, periodAddress)
        Else
            ' PPS Error tracking
            Return Nothing
        End If

    End Function

    Friend Sub UpdateCalculatedItemsOnWS(ByRef entityName As String)

        For Each accountName In ACQMODEL.outputsList
            For Each period In ACQMODEL.currentPeriodlist
                Dim value As Double = ACQMODEL.GetCalculatedValue(DATASET.AccountsNameKeyDictionary(accountName), period)
                UpdateExcelWS(entityName, accountName, period, value)
            Next
        Next

    End Sub

#End Region


#Region "Events"

    Friend Sub Worksheet_Change(ByVal Target As Excel.Range)

        Dim entityItem As String
        Dim modelUpdateFlag As Boolean = False

        If GRS.isUpdating = False AndAlso disableWSChange = False Then
            For Each cell As Excel.Range In Target.Cells

                Dim intersect = APPS.Intersect(cell, DATAMODTRACKER.dataSetRegion)
                If Not intersect Is Nothing Then

                    entityItem = DATASET.CellsAddressItemsDictionary(cell.Address)(CModelDataSet.ENTITY_ITEM)
                    Dim accountItem As String = DATASET.CellsAddressItemsDictionary(cell.Address)(CModelDataSet.ACCOUNT_ITEM)
                    Dim periodItem As String = DATASET.CellsAddressItemsDictionary(cell.Address)(CModelDataSet.PERIOD_ITEM)

                    If IsNumeric(cell.Value) Then
                        If ACQMODEL.CheckIfBSCalculatedItem(accountItem, periodItem) = False Then

                            modelUpdateFlag = True
                            GRS.UpdateDGVFromExcelUpdate(entityItem, accountItem, periodItem, cell.Value2, cell.Address)
                            If GRS.autoCommitFlag = True Then GRS.Submit()

                        End If
                    Else
                        disableWSChange = True
                        cell.Value = DATASET.dataSetDictionary(entityItem)(accountItem)(periodItem)
                        disableWSChange = False
                    End If
                Else
                    On Error Resume Next
                    Dim intersectOutput = APPS.Intersect(cell, DATAMODTRACKER.outputsRegion)
                    If Not intersectOutput Is Nothing Then
                        disableWSChange = True
                        cell.Value = DATASET.OutputCellsAddressValuesDictionary(cell.Address)
                        disableWSChange = False
                    End If
                End If
            Next
            If modelUpdateFlag = True Then GRS.UpdateAcquModel(entityItem)
        End If

    End Sub

    Friend Sub Worksheet_BeforeRightClick(ByVal Target As Range, ByRef Cancel As Boolean)

        Dim newButton As CommandBarButton
        On Error Resume Next
        APPS.CommandBars("Cell").Controls("Account Details").Delete()
        newButton = APPS.CommandBars("Cell").Controls.Add(Temporary:=True)
        newButton.Caption = "Account Details"
        newButton.Style = MsoButtonStyle.msoButtonCaption
        ' rajouter image -> icone PPS
        AddHandler newButton.Click, AddressOf ShowAccountDetailsRightClick
        On Error GoTo 0

    End Sub

    Public Sub ShowAccountDetailsRightClick(ctrl As CommandBarButton, ByRef cancelDefault As Boolean)

        MsgBox("account(s detail")


    End Sub


#End Region


#Region "Utilities"

    Public Shared Function GetDictionaryKey(ByRef dic As Dictionary(Of String, String), ByRef value As String) As String

        For Each key As String In dic.Keys
            If dic(key) = value Then Return key
        Next
        Return Nothing

    End Function


#End Region



End Class
