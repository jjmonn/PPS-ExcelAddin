﻿' FModellingExportController.vb
'
' To do: Reijection process can be improved by a different implementation of the database_Downloader (currently too much specific to controllingUI2)
'
'
'    stub on adjustments ID selection to be implemented correctly !! simple
'
'
' Author: Julien Monnereau
' Last modified: 11/05/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections


Friend Class FModelingExportController


#Region "Instance Variables"

    ' Objects
    Private MainView As FModelingUI2
    Friend View As FModelingExportControl
      Private FModellingAccount As FModellingAccount
    Private ExportsTV As New TreeView
    Private EntitiesTV As New TreeView

    ' Variables
    Private export_mappingDGV As New vDataGridView
    Private exports_id_list As List(Of String)
    Private accounts_id_names_dic As Hashtable
    Private accounts_names_id_dic As Hashtable
    Private periods_list As Int32()
    Private CBEditor As New ComboBoxEditor

  
    ' Display Const
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 20
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER

#End Region


#Region "Initialize"

    Friend Sub New(ByRef MainView As FModelingUI2, _
                   ByRef FModellingAccount As FModellingAccount, _
                   ByRef accounts_names_id_dic As Hashtable, _
                   ByRef CBEditor As ComboBoxEditor)

        Me.MainView = MainView
        Me.FModellingAccount = FModellingAccount
        Me.accounts_id_names_dic = accounts_id_names_dic
        Me.accounts_names_id_dic = accounts_names_id_dic
        Me.CBEditor = CBEditor

        View = New FModelingExportControl(Me)

        Globalvariables.Entities.LoadEntitiesTV(EntitiesTV)
        exports_id_list = FModelingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE, FINANCIAL_MODELLING_EXPORT_TYPE)
        InitializeExportMappingDGV()
        InitializeExportsTV()

    End Sub

    Private Sub InitializeExportMappingDGV()

        export_mappingDGV.RowsHierarchy.Visible = False
        Dim column1 = export_mappingDGV.ColumnsHierarchy.Items.Add("Financing Modelling Export")
        Dim column2 = export_mappingDGV.ColumnsHierarchy.Items.Add("Account")
        column1.CellsEditor = CBEditor

        For Each fmodelling_account_id In exports_id_list
            Dim account_id = FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE)
            Dim row = export_mappingDGV.RowsHierarchy.Items.Add(fmodelling_account_id)
            export_mappingDGV.CellsArea.SetCellValue(row, export_mappingDGV.ColumnsHierarchy.Items(0), FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_NAME_VARIABLE))
            If Not account_id Is Nothing Then export_mappingDGV.CellsArea.SetCellValue(row, export_mappingDGV.ColumnsHierarchy.Items(1), accounts_id_names_dic(account_id))
        Next
        DataGridViewsUtil.InitDisplayVDataGridView(export_mappingDGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(export_mappingDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        export_mappingDGV.ColumnsHierarchy.AutoStretchColumns = True
        export_mappingDGV.BackColor = System.Drawing.SystemColors.Control
        AddHandler export_mappingDGV.CellValueChanged, AddressOf ExportMappingDGV_CellValueChanged


    End Sub

    Private Sub InitializeExportsTV()

        For Each fmodelling_account_id In exports_id_list
            Dim node As TreeNode = ExportsTV.Nodes.Add(fmodelling_account_id, FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_NAME_VARIABLE), 0, 0)
            Dim entity_id = FModellingAccount.ReadFModellingAccount(fmodelling_account_id, FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE)
            If Not IsDBNull(entity_id) _
            AndAlso EntitiesTV.Nodes.Find(entity_id, True).Length > 0 Then
                node.Nodes.Add(entity_id, EntitiesTV.Nodes.Find(entity_id, True)(0).Text, 1, 1)
            End If
        Next
        ExportsTV.AllowDrop = True

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub InitializeView()

        View.AddExportTabElements(ExportsTV, EntitiesTV, export_mappingDGV)

    End Sub

    Protected Friend Sub UpdatePeriodList(ByRef input_period_list As Int32())

        periods_list = input_period_list

    End Sub

    Friend Sub Export()

        Dim version_id As String = MainView.getVersion_id()
        Dim dataDict As Dictionary(Of String, Double()) = MainView.getDataDictionary()
        Dim DBUploader As New DataBaseDataUploader
        InitializePBar()

        For Each export_id In exports_id_list
            Dim account_id = FModellingAccount.ReadFModellingAccount(export_id, FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE)
            Dim entity_id = FModellingAccount.ReadFModellingAccount(export_id, FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE)
            For j As Int32 = 0 To periods_list.Length - 1

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

                View.PBar.AddProgress()
            Next
        Next
        EndPbar()
        MsgBox("Financing Successfully Injected.")

    End Sub

#End Region


#Region "Utilities"

    Private Sub ExportMappingDGV_CellValueChanged(sender As Object, args As CellEventArgs)

        FModellingAccount.UpdateFModellingAccount(args.Cell.RowItem.Caption, _
                                                  FINANCIAL_MODELLING_ACCOUNT_ID_VARIABLE, _
                                                  accounts_names_id_dic(args.Cell.Value))

    End Sub

    Friend Sub UpdateMappedEntity(ByRef fmodelling_account_id As String, _
                                  ByRef entity_id As String)

        FModellingAccount.UpdateFModellingAccount(fmodelling_account_id, _
                                                  FINANCIAL_MODELLING_MAPPED_ENTITY_VARIABLE, _
                                                  entity_id)

    End Sub

    Friend Function AreMappingsComplete() As Int32

        For Each row In export_mappingDGV.RowsHierarchy.Items
            If export_mappingDGV.CellsArea.GetCellValue(row, export_mappingDGV.ColumnsHierarchy.Items(1)) = "" Then Return -1
        Next
        For Each node In ExportsTV.Nodes
            If node.nodes.count = 0 Then Return -2
        Next
        Return 0

    End Function

    Private Sub InitializePBar()

        Dim LoadingBarMax As Integer = exports_id_list.Count * periods_list.Length
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    Private Sub EndPbar()

        View.PBar.EndProgress()
        View.PBar.Visible = False

    End Sub

#End Region


End Class
