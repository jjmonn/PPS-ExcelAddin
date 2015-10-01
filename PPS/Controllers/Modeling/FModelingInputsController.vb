' FModellingSimulationsControler.vb
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


Friend Class FModelingInputsController


#Region "Instance Variables"

    ' Object 
    Private MainView As FModelingUI2
    Friend View As FModelingInputsControl
     Private FModellingAccount As FModellingAccount
    Private VersionsTV As New TreeView
    Private EntitiesTV As New TreeView
    Friend InputsDGV As New vDataGridView
    Friend MappingDGV As New vDataGridView

    ' Variables
    Private inputs_list As List(Of String)
    Friend accounts_names_id_dic As Hashtable
    Friend inputs_mapping As Dictionary(Of String, String)
    Private accounts_id_list As List(Of String)
    Friend periods_list As List(Of Int32)
    Private current_conso_data_dic As New Dictionary(Of String, Double())
    Private versions_id_list As List(Of String)

    ' Display
    Friend CBEditor As New ComboBoxEditor
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 20
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef MainView As FModelingUI2, _
                             ByRef FModellingAccount As FModellingAccount)

        Me.MainView = MainView
        Me.FModellingAccount = FModellingAccount

        View = New FModelingInputsControl(Me)


        GlobalVariables.Versions.LoadVersionsTV(VersionsTV)
        TreeViewsUtilities.CheckAllNodes(EntitiesTV)
        Globalvariables.Entities.LoadEntitiesTV(EntitiesTV)
        inputs_list = FModelingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE, FINANCIAL_MODELLING_INPUT_TYPE)
        accounts_names_id_dic = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ID_VARIABLE)
        accounts_id_list = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_ALL, ID_VARIABLE)
        versions_id_list = GlobalVariables.Versions.versions_hash.Keys

        InitializeMappingDGV()
        InitializeInputsDGV()

    End Sub

    Private Sub InitializeMappingDGV()

        InitializeMappingCB()
        MappingDGV.RowsHierarchy.Visible = False
        Dim column1 = MappingDGV.ColumnsHierarchy.Items.Add("Accounts")
        Dim column2 = MappingDGV.ColumnsHierarchy.Items.Add("Financing Modelling Inputs")
        column1.CellsEditor = CBEditor

        For Each fmodelling_account_id In inputs_list
            Dim account_id = FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE)
            Dim row = MappingDGV.RowsHierarchy.Items.Add(fmodelling_account_id)
            MappingDGV.CellsArea.SetCellValue(row, MappingDGV.ColumnsHierarchy.Items(1), FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_NAME_VARIABLE))
            If Not account_id Is Nothing Then MappingDGV.CellsArea.SetCellValue(row, MappingDGV.ColumnsHierarchy.Items(0), GlobalVariables.Accounts.m_accountsHash(account_id)(NAME_VARIABLE))
        Next
        DataGridViewsUtil.InitDisplayVDataGridView(MappingDGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(MappingDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        MappingDGV.ColumnsHierarchy.AutoStretchColumns = True
        MappingDGV.BackColor = System.Drawing.SystemColors.Control
        AddHandler MappingDGV.CellValueChanged, AddressOf MappingDGV_CellValueChanged

    End Sub

    Private Sub InitializeMappingCB()

        CBEditor.DropDownHeight = CBEditor.ItemHeight * CB_NB_ITEMS_DISPLAYED
        CBEditor.DropDownWidth = CB_WIDTH
        Dim accounts_list = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_ALL, NAME_VARIABLE)
        For Each account_name In accounts_list
            CBEditor.Items.Add(account_name)
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
        For Each row In MappingDGV.RowsHierarchy.Items
            Dim account_name = MappingDGV.CellsArea.GetCellValue(row, MappingDGV.ColumnsHierarchy.Items(0))
            If account_name <> "" Then

                Dim fmodelling_account_name = MappingDGV.CellsArea.GetCellValue(row, MappingDGV.ColumnsHierarchy.Items(1))
                Dim account_id = accounts_names_id_dic(account_name)
                Dim input_row = InputsDGV.RowsHierarchy.Items.Add(row.Caption)
                InputsDGV.CellsArea.SetCellValue(input_row, InputsDGV.ColumnsHierarchy.Items(0), fmodelling_account_name)

                For j = 1 To InputsDGV.ColumnsHierarchy.Items.Count - 1
                    InputsDGV.CellsArea.SetCellValue(input_row, InputsDGV.ColumnsHierarchy.Items(j), current_conso_data_dic(account_id)(j - 1))
                Next
            End If
        Next
        InputsDGV.Refresh()
        InputsDGV.Select()

    End Sub

    Protected Friend Function IsVersionValid(ByRef version_id As String) As Boolean

        If versions_id_list.Contains(version_id) Then Return True
        Return False

    End Function

    Private Sub MappingDGV_CellValueChanged(sender As Object, args As CellEventArgs)

        FillInInputsDGV()
        FModellingAccount.UpdateFModellingAccount(args.Cell.RowItem.Caption, _
                                                  FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE, _
                                                  accounts_names_id_dic(args.Cell.Value))

    End Sub

    Protected Friend Function IsMappingComplete() As Boolean

        For Each row In MappingDGV.RowsHierarchy.Items
            If MappingDGV.CellsArea.GetCellValue(row, MappingDGV.ColumnsHierarchy.Items(0)) = "" Then Return False
        Next
        Return True

    End Function


#End Region




End Class
