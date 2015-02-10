' AlternativeScenariosUI.vb
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified: 26/01/2015


Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Drawing
Imports Microsoft.Office.Interop
Imports System.IO



Friend Class AlternativeScenariosUI


#Region "Instance Variables"

    Private Controller As AlternativeScenariosController
    Private InputsController As ASInputsController
    Friend PBar As New ProgressBarControl
    Private ExportDGV As vDataGridView
    Private adjustmentsTV As TreeView

    ' Variables
    Private as_panel_lines_index As Int32 = 0
    Private current_report As Object

    ' Constants
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
    Private Const SENSIS_DGV_CELLS_FORMAT = "{0:N2}"
    Private Const MAIN_PANEL_ROW_HEIGHT As Int32 = 310
    Private Const MAIN_PANEL_INITIAL_LINES_NB As Int32 = 4
    Private Const VOLUMES_TAB_CAPTION As String = "Volumes"

#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As AlternativeScenariosController, _
                             ByRef input_as_inputs_controller As ASInputsController, _
                             ByRef sensitivities_dictionary As Dictionary(Of String, Hashtable), _
                             ByRef export_dict As Dictionary(Of String, Dictionary(Of String, String)), _
                             ByRef CBEditor As ComboBoxEditor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        InputsController = input_as_inputs_controller
        InitializeSensitivitiesTab(sensitivities_dictionary)
        InitializeAdjustmentsTV()
        InitializeExportMapping(export_dict, CBEditor)

    End Sub

    Private Sub InitializeSensitivitiesTab(ByRef sensitivities_dictionary As Dictionary(Of String, Hashtable))

        Dim tmp_TabControl As TabControl
        For Each sensi_tab As TabPage In SensitivitiesTabControl.TabPages
            tmp_TabControl = sensi_tab.Controls(0)
            Dim tab_index As Int32 = 0
            For Each sensitivity_item In sensitivities_dictionary.Keys
                If sensi_tab.Text = VOLUMES_TAB_CAPTION Then
                    tmp_TabControl.TabPages.Add(sensitivity_item + " (" + sensitivities_dictionary(sensitivity_item)(GDF_SENSITIVITIES_DEST_UNIT_VAR) + ")")
                Else
                    tmp_TabControl.TabPages.Add(sensitivity_item + " (" + MAIN_CURRENCY + ")")
                End If
                Dim DGV As New vDataGridView
                tmp_TabControl.TabPages(tab_index).Controls.Add(DGV)
                DGV.ColumnsHierarchy.Items.Add("Entities")
                tab_index = tab_index + 1
            Next
        Next

    End Sub

    Private Sub InitializeAdjustmentsTV()

        adjustmentsTV = New TreeView
        Dim adjustments_id_names_dict = AdjustmentsMapping.GetAdjustmentsDictionary(ADJUSTMENTS_ID_VAR, ADJUSTMENTS_NAME_VAR)
        For Each adjustment_id As String In adjustments_id_names_dict.Keys
            adjustmentsTV.Nodes.Add(adjustment_id, adjustments_id_names_dict(adjustment_id))
        Next
        AdjustmentsTVPanel.Controls.Add(adjustmentsTV)
        adjustmentsTV.Dock = DockStyle.Fill

    End Sub

    Private Sub InitializeExportMapping(ByRef export_dict As Dictionary(Of String, Dictionary(Of String, String)), _
                                         ByRef CBEditor As ComboBoxEditor)

        ExportDGV = New vDataGridView
        ExportDGV.RowsHierarchy.Visible = False
        Dim column1 = ExportDGV.ColumnsHierarchy.Items.Add("Sensitivity")
        Dim column2 = ExportDGV.ColumnsHierarchy.Items.Add("Item")
        Dim column3 = ExportDGV.ColumnsHierarchy.Items.Add("Mapped Account")
        column3.CellsEditor = CBEditor

        For Each sensitivity_id As String In export_dict.Keys
            For Each item As String In export_dict(sensitivity_id).Keys
                Dim tmp_row As HierarchyItem = ExportDGV.RowsHierarchy.Items.Add("")
                ExportDGV.CellsArea.SetCellValue(tmp_row, column1, sensitivity_id)
                ExportDGV.CellsArea.SetCellValue(tmp_row, column2, item)
                ExportDGV.CellsArea.SetCellValue(tmp_row, column3, export_dict(sensitivity_id)(item))
            Next
        Next
        ExportMappingPanel.Controls.Add(ExportDGV)
        ExportDGV.Dock = DockStyle.Fill
        DataGridViewsUtil.InitDisplayVDataGridView(ExportDGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(ExportDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        ExportDGV.ColumnsHierarchy.AutoStretchColumns = True
        ExportDGV.BackColor = System.Drawing.SystemColors.Control
        AddHandler ExportDGV.CellValueChanged, AddressOf exportDGV_CellValueChanged


    End Sub

    Private Sub FModellingUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MainPanel.AutoScroll = True
        Me.WindowState = FormWindowState.Maximized
        Me.Controls.Add(PBar)                           ' Progress Bar
        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub AddInputsTabElement(ByRef input_entitiesTV As TreeView, _
                                             ByRef input_versionsTV As TreeView, _
                                             ByRef input_marketpricesTV As TreeView)

        EntitiesTVPanel.Controls.Add(input_entitiesTV)
        VersionsTVpanel.Controls.Add(input_versionsTV)
        MarketPricesTVPanel.Controls.Add(input_marketpricesTV)

        input_entitiesTV.Dock = DockStyle.Fill
        input_versionsTV.Dock = DockStyle.Fill
        input_marketpricesTV.Dock = DockStyle.Fill

    End Sub

    Protected Friend Sub DisplaySensibilitiesTabs(ByRef sensitivities_results As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double()))), _
                                                  ByRef period_list As List(Of Int32), _
                                                  ByRef time_config As String, _
                                                  ByRef entity_node As TreeNode)

        SensitivitiesDGVsInitialization(period_list, time_config, entity_node)
        FillSensitivitiesDGV(sensitivities_results, entity_node)

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ComputeScenarioBT_Click(sender As Object, e As EventArgs) Handles ComputeScenarioBT.Click

        Controller.ComputeAlternativeScenario()

    End Sub

    Private Sub SendToExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToExcelToolStripMenuItem.Click

        Dim ws As Excel.Worksheet = APPS.ActiveSheet
        If TypeOf (current_report) Is vDataGridView Then
            ' use dgvutil > export excel ?

        Else
            Dim destination As Excel.Range = CWorksheetWrittingFunctions.CreateReceptionWS("Scenario", _
                                                                                           {}, _
                                                                                           {})
            ChartsUtilities.ExportChartExcel(current_report, destination.Worksheet, 4)
            '  Dim chart1 As Chart = current_report
            'Dim ms As MemoryStream = New MemoryStream()
            'chart1.SaveImage(ms, ChartImageFormat.Bmp)
            'Dim bm As Bitmap = New Bitmap(ms)
            'Clipboard.SetImage(bm)
            'ws.Paste()
        End If

    End Sub

    Private Sub ReinjectionBT_Click(sender As Object, e As EventArgs) Handles ReinjectionBT.Click

        Dim confirm As Integer = MessageBox.Show("The Impacts of the new Scenario will be" + Chr(13) + Chr(13) + _
                                                 "added to the selected Adjustment." + Chr(13) + Chr(13) + _
                                                 "Do you confirm ?", _
                                                 "Alternative Scenarion Reinjection Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            If adjustmentsTV.SelectedNode Is Nothing Then
                Controller.ScenarioReinjection()
            Else
                Controller.ScenarioReinjection(adjustmentsTV.SelectedNode.Name)
            End If
        End If

    End Sub

#End Region


#Region "Events"

    Protected Friend Sub Reports_MouseClick(sender As Object, e As MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            current_report = sender
        End If

    End Sub

    Private Sub ExportDGV_CellValueChanged(sender As Object, args As CellEventArgs)

        Dim sensitivity As String = ExportDGV.CellsArea.GetCellValue(args.Cell.RowItem, ExportDGV.ColumnsHierarchy.Items(0))
        Dim item As String = ExportDGV.CellsArea.GetCellValue(args.Cell.RowItem, ExportDGV.ColumnsHierarchy.Items(1))
        Controller.UpdateExportMapping(sensitivity, item, args.Cell.Value)

    End Sub

#End Region


#Region "Sensitivities Tab"

    Private Sub SensitivitiesDGVsInitialization(ByRef period_list As List(Of Int32), _
                                                ByRef time_config As String, _
                                                ByRef entity_node As TreeNode)

        Dim tmp_TabControl As TabControl
        For Each sensi_tab As TabPage In SensitivitiesTabControl.TabPages

            tmp_TabControl = sensi_tab.Controls(0)
            For Each tab_ As TabPage In tmp_TabControl.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DGV.Clear()
                DGV.ColumnsHierarchy.Items.Add("Entity")
                DataGridViewsUtil.CreateDGVColumns(DGV, period_list.ToArray, time_config, False)
                AddRow(DGV, entity_node)
            Next

        Next

    End Sub

    Private Sub FillSensitivitiesDGV(ByRef sensitivities_results As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double()))), _
                                     ByRef entity_node As TreeNode)

        ' Volumes Sensitivities
        Dim tab_index As Int32 = 0
        For Each sensistivity_id In sensitivities_results.Keys
            Dim DGV As vDataGridView = VolumesTabControl.TabPages(tab_index).Controls(0)
            FillInRow(sensitivities_results(sensistivity_id)(PSDLLL_Interface.SENSITIVITIES), DGV, entity_node)
            DGVDisplaySetup(DGV)
            tab_index = tab_index + 1
        Next

        ' Revenues Sensitivities
        tab_index = 0
        For Each sensistivity_id In sensitivities_results.Keys
            Dim DGV As vDataGridView = RevenuesTabControl.TabPages(tab_index).Controls(0)
            FillInRow(sensitivities_results(sensistivity_id)(PSDLLL_Interface.INCREMENTAL_REVENUES), DGV, entity_node)
            DGVDisplaySetup(DGV)
            tab_index = tab_index + 1
        Next

        ' Net Result Sensitivities
        tab_index = 0
        For Each sensistivity_id In sensitivities_results.Keys
            Dim DGV As vDataGridView = NetResultTabControl.TabPages(tab_index).Controls(0)
            FillInRow(sensitivities_results(sensistivity_id)(PSDLLL_Interface.INCREMENTAL_NET_RESULT), DGV, entity_node)
            DGVDisplaySetup(DGV)
            tab_index = tab_index + 1
        Next

    End Sub

    Private Sub AddRow(ByRef DGV As vDataGridView, _
                       ByRef node As TreeNode, _
                       Optional ByRef row As HierarchyItem = Nothing)

        Dim sub_row As HierarchyItem
        If Not row Is Nothing Then
            sub_row = row.Items.Add("")
        Else
            sub_row = DGV.RowsHierarchy.Items.Add("")
        End If
        sub_row.CellsFormatString = SENSIS_DGV_CELLS_FORMAT
        DGV.CellsArea.SetCellValue(sub_row, DGV.ColumnsHierarchy.Items(0), node.Text)
        For Each child_node In node.Nodes
            AddRow(DGV, child_node, sub_row)
        Next

    End Sub

    Private Sub FillInRow(ByRef sensitivities As Dictionary(Of String, Double()), _
                          ByRef dgv As vDataGridView, _
                          ByRef node As TreeNode, _
                          Optional ByRef row As HierarchyItem = Nothing)

        If row Is Nothing Then row = dgv.RowsHierarchy.Items(0)
        For j As Int32 = 1 To dgv.ColumnsHierarchy.Items.Count - 1
            dgv.CellsArea.SetCellValue(row, dgv.ColumnsHierarchy.Items(j), _
            sensitivities(node.Name)(j - 1))
        Next
        For Each child_node As TreeNode In node.Nodes
            FillInRow(sensitivities, dgv, child_node, row.Items(child_node.Index))
        Next

    End Sub

    Private Sub DGVDisplaySetup(ByRef DGV As vDataGridView)

        DGV.Dock = DockStyle.Fill
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DataGridViewsUtil.AdjustDGVFColumnWidth(DGV, 0)

        For j As Int32 = 1 To DGV.ColumnsHierarchy.Items.Count - 1
            DGV.ColumnsHierarchy.Items(j).TextAlignment = ContentAlignment.MiddleCenter
            DGV.ColumnsHierarchy.Items(j).CellsTextAlignment = ContentAlignment.MiddleRight
        Next

        DGV.Refresh()
        Try
            DGV.RowsHierarchy.Items(0).Selected = True
        Catch ex As Exception

        End Try
        ' row hierarchy -> bck = white et no right border dans l'idéal (a priori implique modif draw)

    End Sub


#End Region


#Region "Main Tab"

    Protected Friend Sub AddCharts(ByRef base_chart As Chart, _
                                   ByRef new_report As Chart)

        If as_panel_lines_index > MainPanel.RowCount Then MainPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, MAIN_PANEL_ROW_HEIGHT))
        MainPanel.Controls.Add(base_chart, 0, as_panel_lines_index)
        MainPanel.Controls.Add(new_report, 1, as_panel_lines_index)
        base_chart.ContextMenuStrip = ReportsRCM
        new_report.contextmenustrip = ReportsRCM
        base_chart.Dock = DockStyle.Fill
        new_report.Dock = DockStyle.Fill
        as_panel_lines_index = as_panel_lines_index + 1


    End Sub

    Protected Friend Sub AddDGV(ByRef DGV As vDataGridView)

        If as_panel_lines_index > MainPanel.RowCount Then MainPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, MAIN_PANEL_ROW_HEIGHT))

        MainPanel.Controls.Add(DGV, 0, as_panel_lines_index)
        DGV.ContextMenuStrip = ReportsRCM
        DGV.RowsHierarchy.ExpandAllItems()
        'If DGV.Size.Height < MainPanel.RowStyles.Item(as_panel_lines_index).Height Then MainPanel.RowStyles.Item(as_panel_lines_index).Height = DGV.Size.Height
        DGV.Dock = DockStyle.Fill
        MainPanel.SetColumnSpan(DGV, 2)
        as_panel_lines_index = as_panel_lines_index + 1
      
    End Sub


#End Region


#Region "Utilities"

    Protected Friend Sub ClearMainPanel()

        MainPanel.Controls.Clear()
        MainPanel.RowCount = 0
        For i = 0 To MAIN_PANEL_INITIAL_LINES_NB - 1
            MainPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, MAIN_PANEL_ROW_HEIGHT))
        Next
        as_panel_lines_index = 0

    End Sub


#End Region



End Class