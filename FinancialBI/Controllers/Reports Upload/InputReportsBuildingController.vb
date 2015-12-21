Imports Microsoft.Office.Interop
Imports System.Collections.Generic

' InputReportController.vb
'
' Generates the input report on worksheet
'
' Created on: 16/12/2015
' Created by: Julien Monnereau
' Last modified: 16/12/2015


Friend Class InputReportsBuildingController


    Friend Sub InputReportPaneCallBack_ReportCreation(ByRef p_process As CRUD.Account.AccountProcess)

        GlobalVariables.APPS.ScreenUpdating = False
        Dim l_version As CRUD.Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)
        If l_version Is Nothing Then Exit Sub

        Dim l_entityId As Int32 = GlobalVariables.InputReportTaskPane.m_entitiesTV.SelectedNode.Value
        Dim l_entityName As String = GlobalVariables.InputReportTaskPane.m_entitiesTV.SelectedNode.Text
        Dim l_entity As CRUD.EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(l_entityId)
        If l_entity Is Nothing Then Exit Sub

        Select Case p_process
            Case CRUD.Account.AccountProcess.FINANCIAL : InputReportCreationProcessFinancial(l_entityName, l_entity, l_version)
            Case CRUD.Account.AccountProcess.RH
                Dim l_accountName As String = GlobalVariables.InputReportTaskPane.m_accountSelectionComboBox.Text
                InputReportCreationProcessRH(l_entityName, l_accountName, l_entityId, l_version)
        End Select

        GlobalVariables.InputReportTaskPane.Hide()
        GlobalVariables.InputReportTaskPane.Close()
        GlobalVariables.APPS.Interactive = True
        GlobalVariables.APPS.ScreenUpdating = True

        GlobalVariables.Addin.AssociateReportUploadControler(True)

    End Sub

#Region "Financial Report"

    Private Sub InputReportCreationProcessFinancial(ByRef p_entityName As String, _
                                                    ByRef p_entityCurrency As CRUD.EntityCurrency, _
                                                    ByRef p_version As CRUD.Version)

        Dim currency As CRUD.Currency = GlobalVariables.Currencies.GetValue(p_entityCurrency.CurrencyId)
        If currency Is Nothing Then Exit Sub

        Dim currentcell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(p_entityName, _
                                                                                     {Local.GetValue("general.entity"), _
                                                                                      Local.GetValue("general.currency"), _
                                                                                      Local.GetValue("general.Version")}, _
                                                                                     {p_entityName, currency.Name, GlobalVariables.VersionButton.Caption})

        If currentcell Is Nothing Then
            MsgBox(Local.GetValue("upload.msg_error_upload"))
            Exit Sub
        End If

        Dim timeConfig As UInt32 = p_version.TimeConfiguration
        Dim periodlist As Int32() = GlobalVariables.Versions.GetPeriodsList(My.Settings.version_id)

        If periodlist Is Nothing Then Exit Sub
        GlobalVariables.APPS.Interactive = False
        InsertFinancialInputReportOnWS(currentcell, _
                                       periodlist, _
                                       timeConfig)

        ExcelFormatting.FormatFinancialExcelRange(currentcell, currency.Id, Date.FromOADate(periodlist(0)))

    End Sub

    Private Sub InsertFinancialInputReportOnWS(ByVal p_destinationcell As Excel.Range, _
                                               ByRef p_periodList As Int32(), _
                                               ByRef p_timeConfig As CRUD.TimeConfig)

        Dim accountsTV As New Windows.Forms.TreeView
        GlobalVariables.Accounts.LoadAccountsTV(accountsTV)
        WorksheetWrittingFunctions.WriteAccountsFromTreeView(accountsTV, p_destinationcell, p_periodList)
        accountsTV.Dispose()

    End Sub

#End Region

#Region "RH Report"

    Private Sub InputReportCreationProcessRH(ByRef p_entityName As String, _
                                             ByRef p_accountName As String, _
                                             ByRef p_entityId As UInt32, _
                                             ByRef p_version As CRUD.Version)

        Dim currentcell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(p_entityName, _
                                                                                                    {Local.GetValue("general.account"), _
                                                                                                     Local.GetValue("general.entity"), _
                                                                                                     Local.GetValue("general.Version")}, _
                                                                                                    {p_accountName, _
                                                                                                     p_entityName, _
                                                                                                     GlobalVariables.VersionButton.Caption})
        If currentcell Is Nothing Then
            MsgBox(Local.GetValue("upload.msg_error_upload"))
            Exit Sub
        End If

        ' ask for a period range ? !!  ->  skipable
        ' start period / end period

        Dim periodlist As Int32() = GlobalVariables.Versions.GetPeriodsList(My.Settings.version_id)
        currentcell = currentcell.Offset(1, 0)

        If periodlist Is Nothing Then Exit Sub
        GlobalVariables.APPS.Interactive = False
        InsertRHInputReportOnWS(currentcell, _
                                p_entityId, _
                                periodlist, _
                                p_version.TimeConfiguration)

        ExcelFormatting.FormatRHExcelRange(currentcell, Date.FromOADate(periodlist(0)))


    End Sub

    Private Sub InsertRHInputReportOnWS(ByVal p_destinationCell As Excel.Range, _
                                        ByRef p_entityId As UInt32, _
                                        ByRef p_periodList As Int32(), _
                                        ByRef p_timeConfig As CRUD.TimeConfig)

        ' Drop periods on worksheets
        WorksheetWrittingFunctions.WritePeriodsOnWorksheet(p_destinationCell, p_periodList, p_timeConfig)

        ' Employees drop on worksheet
        Dim p_employeesNameList As New Collections.Generic.List(Of String)
        For Each l_employee As CRUD.AxisElem In GlobalVariables.AxisElems.GetAxisListFilteredOnAxisParent(CRUD.AxisType.Employee, p_entityId)
            p_employeesNameList.Add(l_employee.Name)
        Next
        WorksheetWrittingFunctions.WriteListOnExcel(p_destinationCell, p_employeesNameList)

    End Sub


#End Region






End Class
