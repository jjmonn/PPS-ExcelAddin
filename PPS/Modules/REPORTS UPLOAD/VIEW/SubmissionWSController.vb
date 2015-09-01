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
' Last modified: 01/09/2015


Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Linq


Friend Class SubmissionWSController


#Region "Instance Variables"

    ' Objects
    Private DataSet As ModelDataSet
    Private AcquisitionModel As AcquisitionModel
    Private DataModificationsTracker As DataModificationsTracking
    Private GeneralSubmissionController As GeneralSubmissionControler


    ' Variables
    Private disableWSChange As Boolean
    Friend ws As Excel.Worksheet

    ' const 
    Private MAX_NB_ROWS As UInt16 = 16384

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputGeneralSubmissionController As GeneralSubmissionControler, _
                   ByRef inputDataSet As ModelDataSet, _
                   ByRef inputAcquisitionModel As AcquisitionModel, _
                   ByRef inputDataModificationTracker As DataModificationsTracking)

        GeneralSubmissionController = inputGeneralSubmissionController
        DataSet = inputDataSet
        AcquisitionModel = inputAcquisitionModel
        DataModificationsTracker = inputDataModificationTracker

    End Sub

    Friend Sub AssociateWS(ByRef WS As Excel.Worksheet)

        AddHandler WS.Change, AddressOf Worksheet_Change
        AddHandler WS.BeforeRightClick, AddressOf Worksheet_BeforeRightClick
        Me.ws = WS

        ' AddHandler thisworkbook.SheetSelectionChange, AddressOf Worksheet_SelectionChange
        'WS.Protect(DrawingObjects:=False, _
        '           Contents:=True, _
        '           Scenarios:= _
        '           False, _
        '           AllowFormattingCells:=True, _
        '           AllowFormattingColumns:=True, _
        '           AllowFormattingRows:=True, _
        '           AllowInsertingHyperlinks:=True, _
        '           AllowSorting:= _
        '           True, _
        '           AllowFiltering:=True, _
        '           AllowUsingPivotTables:=True)

    End Sub

#End Region


#Region "Update Worksheet"

    Friend Sub updateCalculatedItemsOnWS(ByRef entityName As String)

        Dim t1 = Date.Now
        ' for each AcquisitionModel. EntitiesList ?! priority normal
        For Each accountName In AcquisitionModel.outputsList
            For Each period In AcquisitionModel.currentPeriodList

                Dim value As Double = AcquisitionModel.GetCalculatedValue(entityName, _
                                                                          DataSet.AccountsNameKeyDictionary(accountName), _
                                                                          AcquisitionModel.periodsIdentifyer & period)
                If Double.IsNaN(value) Then
                    value = 0
                End If
                UpdateExcelWS(entityName, accountName, period, value)
            Next
        Next
        System.Diagnostics.Debug.WriteLine("Update excel computed values => " & (Date.Now - t1).Milliseconds & " ms")

    End Sub

    ' Update the value on the excel worksheet
    Friend Sub UpdateExcelWS(ByRef entity As String, _
                             ByRef account As String, _
                             ByRef period As String, _
                             ByRef value As Double)

        Dim entityAddress, accountAddress, periodAddress As String
        '    Try
        entityAddress = DataSet.EntitiesValuesAddressDict(entity)
        periodAddress = DataSet.periodsValuesAddressDict(period)
        If DataSet.AccountsValuesAddressDict.ContainsKey(account) Then
            accountAddress = DataSet.AccountsValuesAddressDict(account)
        Else
            accountAddress = DataSet.OutputsValuesAddressDict(account)
        End If
        DataSet.UpdateExcelCell(entityAddress, accountAddress, periodAddress, value, True)
        'Catch ex As Exception
        '    System.Diagnostics.Debug.WriteLine("Update Excel Worksheet for outputs raised an error: a name was not found in dataset values address dictionary.")
        '    Return Nothing
        'End Try

        ' PPS Error tracking priority normal

    End Sub

    Friend Sub updateInputsOnWS()

        For Each EntityAddress As String In DataSet.EntitiesAddressValuesDictionary.Keys
            Dim entity_name As String = DataSet.EntitiesAddressValuesDictionary(EntityAddress)

            For Each AccountAddress As String In DataSet.AccountsAddressValuesDictionary.Keys
                If DataSet.AccountsAddressValuesDictionary.ContainsKey(AccountAddress) Then
                    Dim account_name As String = DataSet.AccountsAddressValuesDictionary(AccountAddress)

                    For Each PeriodAddress As String In DataSet.periodsAddressValuesDictionary.Keys
                        Dim period = DataSet.periodsAddressValuesDictionary(PeriodAddress)
                        Dim periodToken As String = AcquisitionModel.periodsIdentifyer & period
                        If AcquisitionModel.dataBaseInputsDictionary(entity_name)(account_name).ContainsKey(periodToken) Then
                            DataSet.UpdateExcelCell(EntityAddress, _
                                                    AccountAddress, _
                                                    PeriodAddress, _
                                                    AcquisitionModel.dataBaseInputsDictionary(entity_name)(account_name)(periodToken), False)
                        End If
                    Next
                End If
            Next
        Next

    End Sub

#End Region


#Region "Events"

    ' Listen to changes in associated worksheet
    Friend Sub Worksheet_Change(ByVal Target As Excel.Range)

        ' If row or column insertion then exit and relaunch dataset snapshot
        If Target.Count > MAX_NB_ROWS Then
            GeneralSubmissionController.RefreshSnapshot(False)
            Exit Sub
        End If

        Dim modelUpdateFlag As Boolean = False
        Dim cell_itemsHT As Hashtable
        Dim dependents_cells As Excel.Range = Nothing
        If GeneralSubmissionController.isUpdating = False AndAlso disableWSChange = False Then

            For Each cell As Excel.Range In Target.Cells

                Dim intersect = GlobalVariables.APPS.Intersect(cell, DataModificationsTracker.dataSetRegion)
                If Not intersect Is Nothing Then

                    cell_itemsHT = getAxisFromCell(cell)
                    If IsNumeric(cell.Value) Then
                        If AcquisitionModel.CheckIfBSCalculatedItem(cell_itemsHT(ModelDataSet.ACCOUNT_ITEM), cell_itemsHT(ModelDataSet.PERIOD_ITEM)) = False Then

                            ' Cell modification registration
                            modelUpdateFlag = True
                            GeneralSubmissionController.UpdateModelFromExcelUpdate(cell_itemsHT(ModelDataSet.ENTITY_ITEM), _
                                                                                   cell_itemsHT(ModelDataSet.ACCOUNT_ITEM), _
                                                                                   cell_itemsHT(ModelDataSet.PERIOD_ITEM), _
                                                                                   cell.Value2, _
                                                                                   cell.Address)

                            ' Register modification in dependant cells
                            On Error Resume Next
                            dependents_cells = cell.Dependents
                            If Not dependents_cells Is Nothing Then
                                For Each dependant_cell As Excel.Range In dependents_cells
                                    intersect = GlobalVariables.APPS.Intersect(dependant_cell, DataModificationsTracker.dataSetRegion)
                                    If Not intersect Is Nothing Then
                                        Dim dependant_cell_itemsHT As Hashtable = getAxisFromCell(dependant_cell)
                                        GeneralSubmissionController.UpdateModelFromExcelUpdate(dependant_cell_itemsHT(ModelDataSet.ENTITY_ITEM), _
                                                                                              dependant_cell_itemsHT(ModelDataSet.ACCOUNT_ITEM), _
                                                                                              dependant_cell_itemsHT(ModelDataSet.PERIOD_ITEM), _
                                                                                              dependant_cell.Value2, _
                                                                                              dependant_cell.Address)
                                    End If
                                Next
                            End If
                            If GeneralSubmissionController.autoCommitFlag = True Then GeneralSubmissionController.DataSubmission()

                        End If
                    Else
                        ' Utile ?
                        disableWSChange = True
                        cell.Value = DataSet.dataSetDictionary(cell_itemsHT(ModelDataSet.ENTITY_ITEM))(cell_itemsHT(ModelDataSet.ACCOUNT_ITEM))(cell_itemsHT(ModelDataSet.PERIOD_ITEM))
                        disableWSChange = False
                    End If
                Else
                    On Error Resume Next
                    Dim intersectOutput = GlobalVariables.APPS.Intersect(cell, DataModificationsTracker.outputsRegion)
                    If Not intersectOutput Is Nothing Then
                        disableWSChange = True
                        cell.Value = DataSet.OutputCellsAddressValuesDictionary(cell.Address)
                        disableWSChange = False
                    End If
                End If
            Next
            If modelUpdateFlag = True Then
                GlobalVariables.APPS.Interactive = False
                GeneralSubmissionController.UpdateCalculatedItems(cell_itemsHT(ModelDataSet.ENTITY_ITEM))
            End If
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

    Friend Sub Worksheet_SelectionChange(ByVal Sh As Object, ByVal Target As Excel.Range)

        ' doea not work yet : error in addin express Get Thisworkbook function
        Dim ws As Excel.Worksheet = CType(Sh, Excel.Worksheet)
        MsgBox("worksheet changed: " & ws.Name)

        ' if ok 
        '  -> change active GRS in addin ()
        '  -> update ribbon
        ' Entity
        ' Currency 
        ' Product
        ' Client

    End Sub

#End Region


#Region "Utilities"

    Private Function getAxisFromCell(ByRef cell As Excel.Range) As Hashtable

        Dim ht As New Hashtable
        ht.Add(ModelDataSet.ENTITY_ITEM, DataSet.CellsAddressItemsDictionary(cell.Address)(ModelDataSet.ENTITY_ITEM))
        ht.Add(ModelDataSet.ACCOUNT_ITEM, DataSet.CellsAddressItemsDictionary(cell.Address)(ModelDataSet.ACCOUNT_ITEM))
        ht.Add(ModelDataSet.PERIOD_ITEM, DataSet.CellsAddressItemsDictionary(cell.Address)(ModelDataSet.PERIOD_ITEM))
        Return ht

    End Function

#End Region



End Class
