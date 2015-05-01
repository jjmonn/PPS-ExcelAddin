' cExcelSubmissionWorksheetController.vb
'
' Controls worksheet connected to GeneralSubmissionController -> events / update cells ... etc
'
' To do: 
'       - 
'       - 
'
' Known bugs: 
'       - 
'
' Author: Julien Monnereau
' Last modified: 28/04/2015


Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VIBlend.WinForms.DataGridView


Friend Class SubmissionWSController


#Region "Instance Variables"

    ' Objects
    Private DataSet As ModelDataSet
    Private AcquisitionModel As AcquisitionModel
    Private DataModificationsTracker As CDataModificationsTracking
    Private GeneralSubmissionController As GeneralSubmissionControler

    ' Variables
    Private disableWSChange As Boolean

#End Region

    ' Below -> OK
#Region "Initialize"

    Friend Sub New(ByRef inputGeneralSubmissionController As GeneralSubmissionControler, _
                   ByRef inputDataSet As ModelDataSet, _
                   ByRef inputAcquisitionModel As AcquisitionModel, _
                   ByRef inputDataModificationTracker As CDataModificationsTracking)

        GeneralSubmissionController = inputGeneralSubmissionController
        DataSet = inputDataSet
        AcquisitionModel = inputAcquisitionModel
        DataModificationsTracker = inputDataModificationTracker

    End Sub

    Friend Sub AssociateWS(ByRef WS As Excel.Worksheet)

        AddHandler WS.Change, AddressOf Worksheet_Change
        AddHandler WS.BeforeRightClick, AddressOf Worksheet_BeforeRightClick

    End Sub

#End Region


#Region "Update Worksheet"

    ' Update the value on the excel worksheet
    Friend Function UpdateExcelWS(ByRef entity As String, _
                                  ByRef account As String, _
                                  ByRef period As String, _
                                  ByRef value As Double) As Excel.Range

        Dim entityAddress, accountAddress, periodAddress As String
        If DataSet.EntitiesAddressValuesDictionary.ContainsValue(entity) Then entityAddress = GetDictionaryKey(DataSet.EntitiesAddressValuesDictionary, entity)
        If DataSet.AccountsAddressValuesDictionary.ContainsValue(account) Then
            accountAddress = GetDictionaryKey(DataSet.AccountsAddressValuesDictionary, account)
        ElseIf DataSet.OutputsAccountsAddressvaluesDictionary.ContainsValue(account) Then
            accountAddress = GetDictionaryKey(DataSet.OutputsAccountsAddressvaluesDictionary, account)
        End If

        If DataSet.periodsAddressValuesDictionary.ContainsValue(period) Then periodAddress = GetDictionaryKey(DataSet.periodsAddressValuesDictionary, period)

        If Not entityAddress Is Nothing AndAlso Not accountAddress Is Nothing AndAlso Not periodAddress Is Nothing Then
            DataSet.UpdateExcelCell(entityAddress, accountAddress, periodAddress, value, True)
            Return DataSet.GetCellFromItem(entityAddress, accountAddress, periodAddress)
        Else
            ' PPS Error tracking
            Return Nothing
        End If

    End Function

    Friend Sub updateCalculatedItemsOnWS(ByRef entityName As String)

        For Each accountName In AcquisitionModel.outputsList
            For Each period In AcquisitionModel.currentPeriodlist
                Dim value As Double = AcquisitionModel.GetCalculatedValue(DataSet.AccountsNameKeyDictionary(accountName), period)
                UpdateExcelWS(entityName, accountName, period, value)
            Next
        Next

    End Sub

    Protected Friend Sub updateInputsOnWS(ByRef entityName As String)

        For Each EntityAddress As String In DataSet.EntitiesAddressValuesDictionary.Keys
            Dim entity_name As String = DataSet.EntitiesAddressValuesDictionary(EntityAddress)

            For Each AccountAddress As String In DataSet.AccountsAddressValuesDictionary.Keys
                If DataSet.AccountsAddressValuesDictionary.ContainsKey(AccountAddress) Then
                    Dim account_name As String = DataSet.AccountsAddressValuesDictionary(AccountAddress)

                    For Each PeriodAddress As String In DataSet.periodsAddressValuesDictionary.Keys
                        Dim period = DataSet.periodsAddressValuesDictionary(PeriodAddress)
                        DataSet.UpdateExcelCell(EntityAddress, _
                                                AccountAddress, _
                                                PeriodAddress, _
                                                AcquisitionModel.DBInputsDictionary(entity_name)(account_name)(period), False)
                    Next
                End If
            Next
        Next

    End Sub

#End Region


#Region "Events"

    Friend Sub Worksheet_Change(ByVal Target As Excel.Range)

        Dim entityItem As String
        Dim modelUpdateFlag As Boolean = False

        If GeneralSubmissionController.isUpdating = False AndAlso disableWSChange = False Then
            For Each cell As Excel.Range In Target.Cells

                Dim intersect = GlobalVariables.apps.Intersect(cell, DataModificationsTracker.DataSetRegion)
                If Not intersect Is Nothing Then

                    entityItem = DataSet.CellsAddressItemsDictionary(cell.Address)(ModelDataSet.ENTITY_ITEM)
                    Dim accountItem As String = DataSet.CellsAddressItemsDictionary(cell.Address)(ModelDataSet.ACCOUNT_ITEM)
                    Dim periodItem As String = DataSet.CellsAddressItemsDictionary(cell.Address)(ModelDataSet.PERIOD_ITEM)

                    If IsNumeric(cell.Value) Then
                        If AcquisitionModel.CheckIfBSCalculatedItem(accountItem, periodItem) = False Then

                            modelUpdateFlag = True
                            GeneralSubmissionController.UpdateDGVFromExcelUpdate(entityItem, accountItem, periodItem, cell.Value2, cell.Address)
                            If GeneralSubmissionController.autoCommitFlag = True Then GeneralSubmissionController.Submit()

                        End If
                    Else
                        disableWSChange = True
                        cell.Value = DataSet.DataSetDictionary(entityItem)(accountItem)(periodItem)
                        disableWSChange = False
                    End If
                Else
                    On Error Resume Next
                    Dim intersectOutput = GlobalVariables.apps.Intersect(cell, DataModificationsTracker.outputsRegion)
                    If Not intersectOutput Is Nothing Then
                        disableWSChange = True
                        cell.Value = DataSet.OutputCellsAddressValuesDictionary(cell.Address)
                        disableWSChange = False
                    End If
                End If
            Next
            If modelUpdateFlag = True Then GeneralSubmissionController.UpdateCalculatedItems(entityItem)
        End If

    End Sub

    Friend Sub Worksheet_BeforeRightClick(ByVal Target As Range, ByRef Cancel As Boolean)

        Dim newButton As CommandBarButton
        On Error Resume Next
        GlobalVariables.APPS.CommandBars("Cell").Controls("Account Details").Delete()
        newButton = GlobalVariables.APPS.CommandBars("Cell").Controls.Add(Temporary:=True)
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
