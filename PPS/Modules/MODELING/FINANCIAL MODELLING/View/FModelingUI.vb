' FModellingUI.vb
'
'
' To do:
'       - 
'       - 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 18/02/2014


Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.DataGridView
Imports System.Collections


Friend Class FModelingUI


#Region "Instance Variables"

    ' Objects
    Private SimulationsController As FModellingSimulationsControler
    Private InputsController As FModellingInputsController
    Private ExportsController As FModellingExportController
    Private ScenariosTV As TreeView
    Private VersionsTV As TreeView
    Private EntitiesTV As TreeView
    Private ExportsTV As TreeView
    Private EntitiesTV2 As TreeView
    Friend PBar As New ProgressBarControl
    Private ScenarioCB As ComboBox
    Private ScenariiPanelLayout As New TableLayoutPanel

    ' Variables
    Private active_scenario_id As String = ""
    Private current_scenario_node As TreeNode = Nothing
    Private row_index As Int32 = 0

    ' Constants
    Private Const OUTPUT_PANEL_DGV_ROWS_HEIGHT As Int32 = 220
    Private Const OUTPUT_PANEL_CHART_ROWS_HEIGHT As Int32 = 200
    Private Const OUTPUT_PANEL_SECOND_COLUMN_P As Int32 = 8
    Private Const OUTPUT_PANEL_TITLE_ROW_HEIGHT As Int32 = 20
    Private Const OUTPUT_PANEL_ROWS_MARGIN As Int32 = 9
    Private Const MAPPING_PANEL_ROWS_HEIGHT As Int32 = 24
    Private EXPORT_BUTTON_SIZE As Int32 = 22

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_simulations_controller As FModellingSimulationsControler, _
                   ByRef input_inputs_controller As FModellingInputsController, _
                   ByRef input_exports_controller As FModellingExportController, _
                   ByRef input_scenariosTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SimulationsController = input_simulations_controller
        InputsController = input_inputs_controller
        ExportsController = input_exports_controller
        ScenariosTV = input_scenariosTV
        InitializeDisplay()
        InitializeToolTips()
        AddHandler ScenariosTV.AfterSelect, AddressOf TV_AfterSelect
        AddHandler ScenariosTV.NodeMouseClick, AddressOf tv_node_mouse_click
       
    End Sub

    Private Sub InitializeDisplay()

        ScenariosTV.ImageList = TVImageList
        scenarioTVPanel.Controls.Add(ScenariosTV)
        ScenariosTV.Dock = DockStyle.Fill
        scenarioTVPanel.ContextMenuStrip = TreeviewRightClick

        SplitContainer1.Panel2.Controls.Add(ScenariiPanelLayout)
        ScenariiPanelLayout.Dock = DockStyle.Fill
        ScenariiPanelLayout.AutoScroll = True
        ScenariiPanelLayout.ColumnCount = ScenariiPanelLayout.ColumnCount + 1
        ScenariiPanelLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100 - OUTPUT_PANEL_SECOND_COLUMN_P))
        ScenariiPanelLayout.ColumnCount = ScenariiPanelLayout.ColumnCount + 1
        ScenariiPanelLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, OUTPUT_PANEL_SECOND_COLUMN_P))

    End Sub

    Private Sub InitializeToolTips()

        Dim ToolTip1 As ToolTip = New ToolTip()
        ToolTip1.SetToolTip(NewScenarioBT, "New Scenario")
        ToolTip1.SetToolTip(NewConstraintBT, "New Target")
        ToolTip1.SetToolTip(Refresh2BT, "Refresh Scenarios")
  
    End Sub

    Private Sub FModellingUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized
        Me.Controls.Add(PBar)                           ' Progress Bar
        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub AddInputsTabElement(ByRef input_entitiesTV As TreeView, _
                                             ByRef input_versionsTV As TreeView)

        EntitiesTVPanel.Controls.Add(input_entitiesTV)
        VersionsTVpanel.Controls.Add(input_versionsTV)
        input_entitiesTV.Dock = DockStyle.Fill
        input_versionsTV.Dock = DockStyle.Fill

        VersionsTV = input_versionsTV
        EntitiesTV = input_entitiesTV
        cTreeViews_Functions.CheckAllNodes(EntitiesTV)
        AddHandler VersionsTV.AfterSelect, AddressOf VersionsTV_AfterSelect
        AddHandler EntitiesTV.AfterSelect, AddressOf EntitiesTV_AfterSelect
        AddHandler VersionsTV.KeyDown, AddressOf versionsTV_KeyDown
        AddHandler EntitiesTV.KeyDown, AddressOf entitiesTV_KeyDown

    End Sub

    Protected Friend Sub AddScenario(ByRef scenario As Scenario)

        AddOutputControls(scenario)
        scenario.ScenarioDGV.Refresh()
        scenario.ScenarioDGV.Select()
        current_scenario_node = ScenariosTV.SelectedNode
        active_scenario_id = scenario.scenario_id

    End Sub

    Protected Friend Sub AddConstraint(ByRef f_account_name As String, _
                                       ByRef scenario_id As String, _
                                       Optional ByRef default_value As Double = 0)

        SimulationsController.AddConstraint(current_scenario_node.Name, _
                                            f_account_name, _
                                            default_value)

    End Sub

    Protected Friend Sub AddExportTabElements(ByRef input_exportTV As TreeView, _
                                              ByRef input_entitiesTV As TreeView, _
                                              ByRef input_export_mapping As vDataGridView,
                                              ByRef input_scenarioCB As ComboBox)

        ExportsTVPanel.Controls.Add(input_exportTV)
        EntitiesTV2Panel.Controls.Add(input_entitiesTV)
        ExportMappingPanel.Controls.Add(input_export_mapping)
        ScenariosCBPanel.Controls.Add(input_scenarioCB)
        input_exportTV.Dock = DockStyle.Fill
        input_entitiesTV.Dock = DockStyle.Fill
        input_export_mapping.Dock = DockStyle.Fill
        input_scenarioCB.Dock = DockStyle.Fill
        ExportsTV = input_exportTV
        EntitiesTV2 = input_entitiesTV
        ScenarioCB = input_scenarioCB
        EntitiesTV2.CollapseAll()
        ExportsTV.ExpandAll()
        EntitiesTV2.ImageList = EntitiesTVImageList
        ExportsTV.ImageList = ExportTVImageList

        AddHandler EntitiesTV2.ItemDrag, AddressOf TV_ItemDrag
        AddHandler EntitiesTV2.DragEnter, AddressOf TV_DragEnter
        AddHandler ExportsTV.DragOver, AddressOf TV_DragOver
        AddHandler ExportsTV.DragDrop, AddressOf TV_DragDrop

    End Sub

    Protected Friend Sub ExportScenarioToSeparateUI(ByRef scenario_id As String, _
                                                    ByRef inputDGV As vDataGridView, _
                                                    ByRef scenarioDGV As vDataGridView, _
                                                    ByRef chart As Chart)

        Dim genericUI As New GenericView("Scenario: " & ScenariosTV.Nodes.Find(scenario_id, True)(0).Text)
        Dim pane As New TableLayoutPanel
        pane.AutoScroll = True
        pane.RowCount = pane.RowCount + 1
        pane.RowStyles.Add(New RowStyle(SizeType.Absolute, DataGridViewsUtil.GetDGVHeight(inputDGV)))
        pane.RowCount = pane.RowCount + 1
        pane.RowStyles.Add(New RowStyle(SizeType.Absolute, scenarioDGV.Height))
        pane.RowCount = pane.RowCount + 1
        pane.RowStyles.Add(New RowStyle(SizeType.Absolute, chart.Height))
        pane.RowCount = pane.RowCount + 1
        pane.RowStyles.Add(New RowStyle(SizeType.Percent, 15))

        pane.Controls.Add(inputDGV, 0, 0)
        pane.Controls.Add(scenarioDGV, 0, 1)
        pane.Controls.Add(chart, 0, 2)
        genericUI.Controls.Add(pane)

        pane.Dock = DockStyle.Fill
        inputDGV.Dock = DockStyle.Fill
        scenarioDGV.Dock = DockStyle.Fill
        chart.Dock = DockStyle.Fill
        inputDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        scenarioDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        inputDGV.Refresh()
        scenarioDGV.Refresh()
        genericUI.BackColor = Drawing.Color.White
        genericUI.Show()

        AddHandler genericUI.ResizeEnd, AddressOf ExportedUI_ResizeEnd

    End Sub

#End Region


#Region "Call Backs"

#Region "Inputs Tab"

    Private Sub EditMappingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditMappingToolStripMenuItem.Click

        InputsController.DisplayInputsMapping()

    End Sub

    Private Sub DisplayConsolidatedInputsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayConsolidatedInputsToolStripMenuItem.Click

        InputsController.DisplayInputsDGV()

    End Sub

    Private Sub LaunchConsoBT_Click(sender As Object, e As EventArgs) Handles LaunchConsoBT.Click

        If Not EntitiesTV.SelectedNode Is Nothing Then
            If Not VersionsTV.SelectedNode Is Nothing Then
                If InputsController.IsVersionValid(VersionsTV.SelectedNode.Name) = False Then
                    MsgBox("The Version currently selected is a folder. Please select a Version.")
                    Exit Sub
                End If
                InputsController.ComputeEntity(VersionsTV.SelectedNode.Name, EntitiesTV.SelectedNode)
                ValidateInputs(sender, e)
            Else
                MsgBox("A Version must be selected.")
            End If
        Else
            MsgBox("An entity must be selected.")
        End If

    End Sub

    Private Sub ValidateInputs(sender As Object, e As EventArgs)

        If Not InputsController.periods_list Is Nothing Then
            If InputsController.IsMappingComplete() = False Then
                Dim confirm As Integer = MessageBox.Show("All the inputs are not mapped. If you validate the Inputs now some of them will be initialized to zero. Do you want to validate?", _
                                                         "Inputs Validation", _
                                                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    SimulationsController.InitializeInputs()
                    NewScenarioBT_Click(sender, e)
                End If
            Else
                SimulationsController.InitializeInputs()
                NewScenarioBT_Click(sender, e)
            End If
        Else
            MsgBox("A consolidation must be launched first.")
        End If
    End Sub

#End Region

#Region "Simulation Tab"

#Region "Scenarios TV Buttons"

    Private Sub Refresh2BT_Click(sender As Object, e As EventArgs) Handles Refresh2BT.Click

        SimulationsController.ComputeAllScenarios()

    End Sub

    Private Sub NewConstraintBT_Click(sender As Object, e As EventArgs) Handles NewConstraintBT.Click

        If Not current_scenario_node Is Nothing Then
            If current_scenario_node.Parent Is Nothing Then
                Dim constraint_input_UI As New NewConstraintUI(Me, SimulationsController.Outputs_name_id_dic, _
                                                               current_scenario_node.Name)
                constraint_input_UI.Show()
            Else
                Dim constraint_input_UI As New NewConstraintUI(Me, SimulationsController.Outputs_name_id_dic, _
                                                               current_scenario_node.Parent.Name)
                constraint_input_UI.Show()
            End If
        Else
            MsgBox("Please Select a Scenario.")
        End If

    End Sub

    Private Sub NewScenarioBT_Click(sender As Object, e As EventArgs) Handles NewScenarioBT.Click

        Dim name = InputBox("Please enter the name of the New Scenario :")
        If name <> "" Then
            If Not cTreeViews_Functions.GetNodesTextsList(ScenariosTV).Contains(name) Then
                SimulationsController.NewScenario(name)
            Else
                MsgBox("This name is already in use. Please choose another one")
            End If
        End If

    End Sub


#End Region

#Region "Scenarios TV Right Click Menu"

    Private Sub NewScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewScenarioTVRCM.Click

        NewScenarioBT_Click(sender, e)

    End Sub

    Private Sub NewConstraintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewConstraintToolStripMenuItem.Click

        NewConstraintBT_Click(sender, e)

    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click

        If Not current_scenario_node Is Nothing Then
            If current_scenario_node.Parent Is Nothing Then
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Scenario: " + Chr(13) + current_scenario_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Scenario deletion confirmation", _
                                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If confirm = DialogResult.Yes Then
                    SimulationsController.DeleteScenario(current_scenario_node)
                End If
            Else
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Constraint: " + Chr(13) + current_scenario_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Constraint deletion confirmation", _
                                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If confirm = DialogResult.Yes Then
                    SimulationsController.DeleteConstraint(current_scenario_node.Parent.Name, current_scenario_node)
                End If
            End If
        Else
            MsgBox("Please select a Scenario or a Constraint.")
        End If

    End Sub

    Private Sub ResfreshScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResfreshScenarioToolStripMenuItem.Click

        If Not current_scenario_node Is Nothing Then
            SimulationsController.ComputeScenario(current_scenario_node.Name)
        End If

    End Sub

    Private Sub RefreshAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshAllToolStripMenuItem.Click

        SimulationsController.ComputeAllScenarios()

    End Sub

#End Region

#Region "Inputs DGVs Right Click Menu"

    Private Sub AddConstraintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddConstraintToolStripMenuItem.Click

        Dim constraint_input_UI As New NewConstraintUI(Me, SimulationsController.Outputs_name_id_dic, _
                                                       active_scenario_id)
        constraint_input_UI.Show()

    End Sub

    Private Sub DeleteConstraintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteConstraintToolStripMenuItem.Click

        SimulationsController.DeleteDGVActiveConstraint(active_scenario_id)

    End Sub

    Private Sub RefreshScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshScenarioToolStripMenuItem.Click

        SimulationsController.ComputeScenario(active_scenario_id)

    End Sub

    Private Sub CopyValueRightBT_Click(sender As Object, e As EventArgs) Handles CopyValueRightBT.Click

        SimulationsController.CopyValueRight(active_scenario_id)

    End Sub

    Private Sub SerieMGTRCMBT_Click(sender As Object, e As EventArgs) Handles SerieMGTRCMBT.Click

        SimulationsController.ShowFAccountsConfig()

    End Sub

#End Region

#End Region

#Region "Outputs Tab"

    Private Sub ReinjectionBT_Click(sender As Object, e As EventArgs) Handles ReinjectionBT.Click

        If ScenarioCB.SelectedItem <> "" Then
            Select Case ExportsController.AreMappingsComplete
                Case -1 : MsgBox("All Exports must be assigned to an Account")
                Case -2 : MsgBox("All Exports must be assigned to an Entity")
                Case 0 : ExportsController.Export()
            End Select
        Else
            MsgBox("A Scenario must be selected.")
        End If
    End Sub

#End Region

#End Region


#Region "Events"

#Region "Inputs Tabs Events"

    Private Sub VersionsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        If InputsController.IsVersionValid(e.Node.Name) Then VersionTB.Text = e.Node.Text

    End Sub

    Private Sub EntitiesTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        EntityTB.Text = e.Node.Text

    End Sub

    Private Sub versionsTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter
                EntitiesTV.Select()
                If Not EntitiesTV.Nodes(0) Is Nothing Then EntitiesTV.SelectedNode = EntitiesTV.Nodes(0)
        End Select

    End Sub

    Private Sub entitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter : LaunchConsoBT.Select()
        End Select

    End Sub

#End Region

#Region "Simulations Tabs Events"

    Private Sub TV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        current_scenario_node = e.Node
        active_scenario_id = current_scenario_node.Name

    End Sub

    Private Sub tv_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_scenario_node = e.Node

    End Sub

    Private Sub BT_Export_OnClick(sender As Object, e As EventArgs)

        Dim row_index As Int32 = ScenariiPanelLayout.GetCellPosition(sender).Row
        SimulationsController.ExportScenarioToUI(ScenariosTV.Nodes.Item(row_index).Name)

    End Sub


#End Region

#Region "Export Tab"

    Private Sub TabPage3_Enter(sender As Object, e As EventArgs) Handles TabPage3.Enter

        ExportsController.GenerateScenarioCB(ScenariosTV)

    End Sub

#Region "TVs Drag and Drop"

    Private Sub TV_ItemDrag(sender As Object, e As ItemDragEventArgs)

        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)

    End Sub

    Private Sub TV_DragEnter(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub TV_DragOver(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, Windows.Forms.TreeView)
        Dim pt As Drawing.Point = CType(sender, TreeView).PointToClient(New Drawing.Point(e.X, e.Y))
        Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, if so no need to validate again
        If Not (selectedTreeview.SelectedNode Is targetNode) Then       'Select the node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
        End If
        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub TV_DragDrop(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        Dim selectedTreeview As TreeView = CType(sender, Windows.Forms.TreeView)
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        Dim targetNode As TreeNode = selectedTreeview.SelectedNode
        If targetNode.Parent Is Nothing Then
            If targetNode.Nodes.Count > 0 Then targetNode.Nodes(0).Remove()
        Else
            targetNode = targetNode.Parent
            targetNode.Nodes(0).Remove()
        End If
        ' dropNode.Remove()             'Remove the drop node from its current location
        dropNode = dropNode.Clone()

        If targetNode Is Nothing Then
            selectedTreeview.Nodes.Add(dropNode)
        Else
            targetNode.Nodes.Add(dropNode)
        End If
        ExportsController.UpdateMappedEntity(targetNode.Name, dropNode.Name)
        dropNode.EnsureVisible()                                        ' Ensure the newley created node is visible to the user and 
        selectedTreeview.SelectedNode = dropNode                        ' Select it
        dropNode.StateImageIndex = 1
        dropNode.SelectedImageIndex = 1

    End Sub

#End Region

#End Region

#Region "Exported UI"

    Private Sub ExportedUI_ResizeEnd(sender As Object, e As EventArgs)

        sender.controls(0).getcontrolfromposition(0, 0).ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        sender.controls(0).getcontrolfromposition(0, 1).ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        sender.controls(0).getcontrolfromposition(0, 0).Refresh()
        sender.controls(0).getcontrolfromposition(0, 1).Refresh()

    End Sub

#End Region

#End Region


#Region "Utilities"

    Protected Friend Sub RedrawOutputs()

        ScenariiPanelLayout.Controls.Clear()
        ScenariiPanelLayout.RowCount = 0
        ScenariiPanelLayout.RowStyles.Clear()

        row_index = 0
        For Each scenario_node In ScenariosTV.Nodes
            AddOutputControls(SimulationsController.GetScenario(scenario_node.name))
        Next

    End Sub

    Private Sub AddOutputControls(ByRef scenario As Scenario)

        ScenariiPanelLayout.RowCount = ScenariiPanelLayout.RowCount + 1
        ScenariiPanelLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, OUTPUT_PANEL_TITLE_ROW_HEIGHT))
        ScenariiPanelLayout.RowCount = ScenariiPanelLayout.RowCount + 1
        ScenariiPanelLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, DataGridViewsUtil.GetDGVHeight(scenario.ScenarioDGV)))
        ScenariiPanelLayout.RowCount = ScenariiPanelLayout.RowCount + 1
        ScenariiPanelLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, OUTPUT_PANEL_CHART_ROWS_HEIGHT))
        ScenariiPanelLayout.RowCount = ScenariiPanelLayout.RowCount + 1
        ScenariiPanelLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, OUTPUT_PANEL_ROWS_MARGIN))

        Dim label As New Label
        label.Text = "Scenario " & ScenariosTV.Nodes.Find(scenario.scenario_id, True)(0).Text
        label.Margin = New Padding(3, 3, 0, 0)
        label.ForeColor = Drawing.Color.DarkBlue
        label.Font = New Drawing.Font(label.Font.FontFamily, 9, Drawing.FontStyle.Bold)

        ScenariiPanelLayout.Controls.Add(label, 0, row_index)
        ScenariiPanelLayout.Controls.Add(GetNewRefreshButton, 1, row_index)
        row_index = row_index + 1
        ScenariiPanelLayout.Controls.Add(scenario.ScenarioDGV, 0, row_index)
        ScenariiPanelLayout.Controls.Add(GetNewExportButton, 1, row_index)
        row_index = row_index + 1
        ScenariiPanelLayout.Controls.Add(scenario.Outputchart, 0, row_index)
        row_index = row_index + 2

        scenario.ScenarioDGV.Dock = DockStyle.Fill
        scenario.Outputchart.Dock = DockStyle.Fill

    End Sub

    Private Function GetNewExportButton() As Button

        Dim BT As New Button
        BT.ImageList = ButtonsImageList
        BT.Margin = New Padding(0, 0, 0, 0)
        BT.FlatStyle = FlatStyle.Flat
        BT.FlatAppearance.BorderSize = 0
        BT.ImageKey = "blue.jpg"
        AddHandler BT.Click, AddressOf BT_Export_OnClick
        Return BT

    End Function

    Private Function GetNewRefreshButton() As Button

        Dim BT As New Button
        BT.ImageList = ButtonsImageList
        BT.Margin = New Padding(0, 0, 0, 0)
        BT.FlatStyle = FlatStyle.Flat
        BT.FlatAppearance.BorderSize = 0
        BT.ImageKey = "refresh_icon(1).ico"
        AddHandler BT.Click, AddressOf RefreshScenarioToolStripMenuItem_Click
        Return BT

    End Function

#End Region

    
   
End Class