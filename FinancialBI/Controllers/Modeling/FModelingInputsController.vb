﻿' FModellingSimulationsControler.vb
'
'
' 
' To do:
'       - Adaptation of the process to monthly configurations
'       -> or at least prevent from opening monthly configured version ! 
'       -> or compute yearly aggrgations in input !!
'
'
' Author: Julien Monnereau
' Last modified: 17/07/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports CRUD


Friend Class FModelingInputsController


#Region "Instance Variables"

    ' Object 
    Private MainView As FModelingUI2
    Friend View As FModelingInputsControl
    Private VersionsTV As New TreeView
    Private EntitiesTV As New TreeView
    Friend InputsDGV As New vDataGridView
    Friend m_mappingDGV As New vDataGridView

    ' Variables
    Private inputs_list As List(Of String)
    Friend inputs_mapping As Dictionary(Of Int32, Int32)
    Private accounts_id_list As List(Of Account)
    Friend periods_list As List(Of Int32)
    Private current_conso_data_dic As New SafeDictionary(Of Int32, Double())
    '    Private versions_id_list As List(Of String)

    ' Display
    Friend CBEditor As New ComboBoxEditor
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 20
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER


#End Region


#Region "Initialize"

    Friend Sub New(ByRef MainView As FModelingUI2)

        Me.MainView = MainView
  
        View = New FModelingInputsControl(Me)
        GlobalVariables.Versions.LoadVersionsTV(VersionsTV)
        TreeViewsUtilities.CheckAllNodes(EntitiesTV)
        GlobalVariables.AxisElems.LoadEntitiesTV(EntitiesTV)
        '      inputs_list = FModelingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE, FINANCIAL_MODELLING_INPUT_TYPE)
        accounts_id_list = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_ALL, Account.AccountProcess.FINANCIAL)

        InitializeMappingDGV()
        InitializeInputsDGV()

    End Sub

    Private Sub InitializeMappingDGV()

        'InitializeMappingCB()
        'm_mappingDGV.RowsHierarchy.Visible = False
        'Dim column1 = m_mappingDGV.ColumnsHierarchy.Items.Add("Accounts")
        'Dim column2 = m_mappingDGV.ColumnsHierarchy.Items.Add("Financing Modelling Inputs")
        'column1.CellsEditor = CBEditor

        'For Each fmodelling_account_id In inputs_list
        '    Dim account_id = FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE)
        '    Dim row = m_mappingDGV.RowsHierarchy.Items.Add(fmodelling_account_id)
        '    m_mappingDGV.CellsArea.SetCellValue(row, m_mappingDGV.ColumnsHierarchy.Items(1), FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_NAME_VARIABLE))
        '    Dim l_account As Account = GlobalVariables.Accounts.GetValue(account_id)
        '    If l_account IsNot Nothing Then
        '        m_mappingDGV.CellsArea.SetCellValue(row, m_mappingDGV.ColumnsHierarchy.Items(0), l_account.Name)
        '    End If
        'Next
        'DataGridViewsUtil.InitDisplayVDataGridView(m_mappingDGV, DGV_THEME)
        'DataGridViewsUtil.DGVSetHiearchyFontSize(m_mappingDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        'm_mappingDGV.ColumnsHierarchy.AutoStretchColumns = True
        'm_mappingDGV.BackColor = System.Drawing.SystemColors.Control
        'AddHandler m_mappingDGV.CellValueChanged, AddressOf MappingDGV_CellValueChanged

    End Sub

    Private Sub InitializeMappingCB()

        CBEditor.DropDownHeight = CBEditor.ItemHeight * CB_NB_ITEMS_DISPLAYED
        CBEditor.DropDownWidth = CB_WIDTH
        For Each l_account As Account In GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_ALL, Account.AccountProcess.FINANCIAL)
            Dim li As New VIBlend.WinForms.Controls.ListItem
            li.Value = l_account.Id
            li.Text = l_account.Name
            CBEditor.Items.Add(li)
        Next

    End Sub

    Private Sub InitializeInputsDGV()

        InputsDGV.RowsHierarchy.Visible = False
        InputsDGV.ColumnsHierarchy.Items.Add("Financing Modelling Input")
        DataGridViewsUtil.InitDisplayVDataGridView(InputsDGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(InputsDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        InputsDGV.ColumnsHierarchy.AutoStretchColumns = True
        InputsDGV.BackColor = System.Drawing.SystemColors.Control

    End Sub

    Protected Friend Sub InitializeView()

        View.AddInputsTabElement(EntitiesTV, VersionsTV)

    End Sub

#End Region


#Region "Interface"

    ' So far only "Years" period config accepted
    Friend Sub ComputeEntity(ByRef version_node As TreeNode, _
                             ByRef entity_node As TreeNode)

        ' to be reimplemented with new model (computer.vb) priority normal-> 

        'Dim time_configuration As String = Versions.ReadVersion(version_node.Name, VERSIONS_TIME_CONFIG_VARIABLE)
        'Model.init_computer_complete_mode(entity_node)
        'InitializePBar()
        'periods_list = Versions.GetPeriodList(version_node.Name)
        'Dim nb_periods As Int32 = Versions.ReadVersion(version_node.Name, VERSIONS_NB_PERIODS_VAR)
        'Dim start_period As Int32 = Versions.ReadVersion(version_node.Name, VERSIONS_START_PERIOD_VAR)
        'Dim rates_version As String = Versions.ReadVersion(version_node.Name, VERSIONS_RATES_VERSION_ID_VAR)
        'Versions.Close()
        'Model.compute_selection_complete(version_node.Name, _
        '                                 time_configuration, _
        '                                 rates_version, _
        '                                 periods_list, _
        '                                 my.settings.mainCurrency, _
        '                                 start_period, _
        '                                 nb_periods, _
        '                                 View.PBar)

        'BuildDataDic(entity_node.Name)
        'InitInputsDGVColumns()
        'FillInInputsDGV()
        'View.PBar.EndProgress()
        'MainView.setVersionAndPeriods(periods_list, version_node.Name, version_node.Text)

    End Sub

    Friend Sub DisplayInputsDGV()

        Dim genericUI As New GenericView("Consolidated Inputs")
        genericUI.Controls.Add(InputsDGV)
        InputsDGV.Dock = DockStyle.Fill
        genericUI.Show()

    End Sub

    Friend Sub sendInputsToSimulationController()

        MainView.sendInputsToSimulationController(InputsDGV)
        MainView.displaySimulation()

    End Sub

#End Region


#Region "Utilities"

    Private Sub InitializePBar()

        ' to be reimplemented : priority normal
        Dim LoadingBarMax As Integer = 0 ' !!
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    Private Sub BuildDataDic(ByRef entity_id As String)

        current_conso_data_dic.Clear()
        'Dim tmp_data_array = Model.GetEntityArray(entity_id)
        ' -> get from computer.vb 
        ' new! priority normal

        Dim i As Int32 = 0
        'For Each account_id In Model.get_model_accounts_list
        '    Dim account_array(periods_list.Count - 1) As Double
        '    For j = 0 To periods_list.Count - 1
        '        account_array(j) = tmp_data_array(i)
        '        i = i + 1
        '    Next
        '    current_conso_data_dic.Add(account_id, account_array)
        'Next

    End Sub

    Private Sub InitInputsDGVColumns()

        InputsDGV.ColumnsHierarchy.Clear()
        InputsDGV.ColumnsHierarchy.Items.Add("Financing Modelling Input")
        For Each period In periods_list
            Dim column = InputsDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            column.CellsFormatString = DEFAULT_FORMAT_STRING
            column.CellsTextAlignment = DataGridViewContentAlignment.MiddleRight
        Next
        DataGridViewsUtil.DGVSetHiearchyFontSize(InputsDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        InputsDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

    End Sub

    Private Sub FillInInputsDGV()

        InputsDGV.RowsHierarchy.Clear()
        For Each row In m_mappingDGV.RowsHierarchy.Items
            ' attention est-ton sûr que l'account id soit sur la première colonne ?
            Dim l_accountId As Int32 = m_mappingDGV.ColumnsHierarchy.Items(0).ItemValue
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(l_accountId)
            If l_account IsNot Nothing Then
                Dim fmodelling_account_name = m_mappingDGV.CellsArea.GetCellValue(row, m_mappingDGV.ColumnsHierarchy.Items(1))
                Dim input_row = InputsDGV.RowsHierarchy.Items.Add(row.Caption)
                InputsDGV.CellsArea.SetCellValue(input_row, InputsDGV.ColumnsHierarchy.Items(0), fmodelling_account_name)
                For j = 1 To InputsDGV.ColumnsHierarchy.Items.Count - 1
                    InputsDGV.CellsArea.SetCellValue(input_row, InputsDGV.ColumnsHierarchy.Items(j), current_conso_data_dic(l_accountId)(j - 1))
                Next
            End If
        Next
        InputsDGV.Refresh()
        InputsDGV.Select()

    End Sub

    Friend Function IsVersionValid(ByRef version_id As String) As Boolean

        Return GlobalVariables.Versions.IsVersionValid(version_id)

    End Function

    Private Sub MappingDGV_CellValueChanged(sender As Object, args As CellEventArgs)

        FillInInputsDGV()
        ' l'id du compte pourrait être en itemValue ?
        Dim l_accountName As String = args.Cell.Value
        Dim l_account As Account = GlobalVariables.Accounts.GetValue(l_accountName)
        If l_account IsNot Nothing Then
            'FModellingAccount.UpdateFModellingAccount(args.Cell.RowItem.Caption, _
            '                                          FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE, _
            '                                          l_account.Id)
        End If

    End Sub

    Protected Friend Function IsMappingComplete() As Boolean

        For Each row In m_mappingDGV.RowsHierarchy.Items
            If m_mappingDGV.CellsArea.GetCellValue(row, m_mappingDGV.ColumnsHierarchy.Items(0)) = "" Then Return False
        Next
        Return True

    End Function


#End Region




End Class
