' FModellingExportController.vb
'
' To do: Reijection process can be improved by a different implementation of the database_Downloader (currently too much specific to controllingUI2)
'
'
'    stub on adjustments ID selection to be implemented correctly !! simple
'   - reimplement export priority high !!!!
'
'
'
'
' Author: Julien Monnereau
' Last modified: 01/09/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports CRUD


Friend Class FModelingExportController


#Region "Instance Variables"

    ' Objects
    Private m_mainView As FModelingUI2
    Friend m_exportView As FModelingExportControl
    Private m_exportsTreeview As New TreeView
    Private m_EntitiesTreeview As New TreeView

    ' Variables
    Private m_exportMappingDGV As New vDataGridView
    Private m_exportsIdsList As List(Of String)
    Private m_periodsList As Int32()
    Private m_comboBoxEditor As New ComboBoxEditor


    ' Display Const
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 20
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER

#End Region


#Region "Initialize"

    Friend Sub New(ByRef MainView As FModelingUI2, _
                   ByRef CBEditor As ComboBoxEditor)

        Me.m_mainView = MainView
        Me.m_comboBoxEditor = CBEditor
        m_exportView = New FModelingExportControl(Me)
        GlobalVariables.AxisElems.LoadEntitiesTV(m_EntitiesTreeview)
        '    m_exportsIdsList = FModelingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE, FINANCIAL_MODELLING_EXPORT_TYPE)
        InitializeExportMappingDGV()
        InitializeExportsTV()

    End Sub

    Private Sub InitializeExportMappingDGV()

        'm_exportMappingDGV.RowsHierarchy.Visible = False
        'Dim column1 = m_exportMappingDGV.ColumnsHierarchy.Items.Add("Financing Modelling Export")
        'Dim column2 = m_exportMappingDGV.ColumnsHierarchy.Items.Add("Account")
        'column1.CellsEditor = m_comboBoxEditor

        'For Each fmodelling_account_id In m_exportsIdsList
        '    Dim account_id = GlobalVariables.fmodellingaccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE)
        '    Dim row = m_exportMappingDGV.RowsHierarchy.Items.Add(fmodelling_account_id)
        '    Dim l_account As Account = GlobalVariables.Accounts.GetValue(account_id)
        '    If l_account IsNot Nothing Then
        '        m_exportMappingDGV.CellsArea.SetCellValue(row, m_exportMappingDGV.ColumnsHierarchy.Items(0), globalvariables.fmodellingaccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_NAME_VARIABLE))
        '        m_exportMappingDGV.CellsArea.SetCellValue(row, m_exportMappingDGV.ColumnsHierarchy.Items(1), l_account.Name)
        '    End If
        'Next
        'DataGridViewsUtil.InitDisplayVDataGridView(m_exportMappingDGV, DGV_THEME)
        'DataGridViewsUtil.DGVSetHiearchyFontSize(m_exportMappingDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        'm_exportMappingDGV.ColumnsHierarchy.AutoStretchColumns = True
        'm_exportMappingDGV.BackColor = System.Drawing.SystemColors.Control
        'AddHandler m_exportMappingDGV.CellValueChanged, AddressOf ExportMappingDGV_CellValueChanged


    End Sub

    Private Sub InitializeExportsTV()

        'For Each fmodelling_account_id In m_exportsIdsList
        '    Dim node As TreeNode = m_exportsTreeview.Nodes.Add(fmodelling_account_id, globalvariables.fmodellingaccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_NAME_VARIABLE), 0, 0)
        '    Dim entity_id = globalvariables.fmodellingaccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE)
        '    If Not IsDBNull(entity_id) _
        '    AndAlso m_EntitiesTreeview.Nodes.Find(entity_id, True).Length > 0 Then
        '        node.Nodes.Add(entity_id, m_EntitiesTreeview.Nodes.Find(entity_id, True)(0).Text, 1, 1)
        '    End If
        'Next
        'm_exportsTreeview.AllowDrop = True

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub InitializeView()

        m_exportView.AddExportTabElements(m_exportsTreeview, m_EntitiesTreeview, m_exportMappingDGV)

    End Sub

    Protected Friend Sub UpdatePeriodList(ByRef input_period_list As Int32())

        m_periodsList = input_period_list

    End Sub

    Friend Sub Export()

        Dim version_id As String = m_mainView.getVersion_id()
        Dim dataDict As Dictionary(Of String, Double()) = m_mainView.getDataDictionary()
        '   Dim DBUploader As New DataBaseDataUploader
        ' to be reimplemented priority high !!!!!!

        InitializePBar()

        For Each export_id In m_exportsIdsList
            '     Dim account_id = globalvariables.fmodellingaccount.ReadFModellingAccount(export_id, FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE)
            '    Dim entity_id = globalvariables.fmodellingaccount.ReadFModellingAccount(export_id, FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE)
            For j As Int32 = 0 To m_periodsList.Length - 1

                ' use server upload object (to be designed) priority high !!

                'Dim current_value = DataBaseDataDownloader.GetSingleValue(version_id, _
                '                                                          entity_id, _
                '                                                          account_id, _
                '                                                          periods_list(j), _
                '                                                          DEFAULT_ANALYSIS_AXIS_ID)

                'Dim new_value = current_value + dataDict(export_id)(j)
                'If new_value <> current_value Then _
                'DBUploader.UpdateSingleValue(entity_id, _
                '                            account_id, _
                '                            periods_list(j), _
                '                            new_value, _
                '                            version_id, _
                '                            DEFAULT_ANALYSIS_AXIS_ID, _
                '                            DEFAULT_ANALYSIS_AXIS_ID, _
                '                            DEFAULT_ANALYSIS_AXIS_ID)

                ' !!!!! STUB adjustment id must be validated in some mapping !!!

                m_exportView.PBar.AddProgress()
            Next
        Next
        EndPbar()
        MsgBox("Financing Successfully Injected.")

    End Sub

#End Region


#Region "Utilities"

    Private Sub ExportMappingDGV_CellValueChanged(sender As Object, args As CellEventArgs)

        Dim l_accountName As String = args.Cell.Value
        Dim l_account As Account = GlobalVariables.Accounts.GetValue(l_accountName)
        If l_account IsNot Nothing Then
            'globalvariables.fmodellingaccount.UpdateFModellingAccount(args.Cell.RowItem.Caption, _
            '                                          FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE, _
            '                                          l_account.Id)
        End If

    End Sub

    Friend Sub UpdateMappedEntity(ByRef fmodelling_account_id As String, _
                                  ByRef entity_id As String)

        'globalvariables.fmodellingaccount.UpdateFModellingAccount(fmodelling_account_id, _
        '                                          FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE, _
        '                                          entity_id)

    End Sub

    Friend Function AreMappingsComplete() As Int32

        For Each row In m_exportMappingDGV.RowsHierarchy.Items
            If m_exportMappingDGV.CellsArea.GetCellValue(row, m_exportMappingDGV.ColumnsHierarchy.Items(1)) = "" Then Return -1
        Next
        For Each node In m_exportsTreeview.Nodes
            If node.nodes.count = 0 Then Return -2
        Next
        Return 0

    End Function

    Private Sub InitializePBar()

        Dim LoadingBarMax As Integer = m_exportsIdsList.Count * m_periodsList.Length
        m_exportView.PBar.Launch(1, LoadingBarMax)

    End Sub

    Private Sub EndPbar()

        m_exportView.PBar.EndProgress()
        m_exportView.PBar.Visible = False

    End Sub

#End Region


End Class
