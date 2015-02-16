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
' Last modified: 13/02/2014


Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.DataGridView
Imports System.Collections


Friend Class FModellingUI


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

    ' Variables
    Private constraints_splitter_distance As Double = 175
    Private is_constraint_displayed As Boolean
    Private displayed_scenario_id As String = ""
    Private current_scenario_node As TreeNode = Nothing

    ' Constants
    Private Const OUTPUT_PANEL_DGV_ROWS_HEIGHT As Int32 = 220
    Private Const OUTPUT_PANEL_CHART_ROWS_HEIGHT As Int32 = 250
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
        ScenariosTV.ImageList = TVImageList
        scenarioTVPanel.Controls.Add(ScenariosTV)
        ScenariosTV.Dock = DockStyle.Fill
        scenarioTVPanel.ContextMenuStrip = TreeviewRightClick
        ScenariiPanelLayout.AutoScroll = True

        AddHandler ScenariosTV.AfterSelect, AddressOf TV_AfterSelect
        AddHandler ScenariosTV.NodeMouseClick, AddressOf tv_node_mouse_click
        AddHandler ScenariosTV.NodeMouseDoubleClick, AddressOf tv_node_mouse_doubleclick
        InitializeToolTips()

    End Sub

    Private Sub InitializeToolTips()

        Dim ToolTip1 As ToolTip = New ToolTip()
        ToolTip1.SetToolTip(NewScenarioBT, "New Scenario")
        ToolTip1.SetToolTip(NewConstraintBT, "New Target")
        ToolTip1.SetToolTip(Refresh2BT, "Refresh Scenarios")
        ToolTip1.SetToolTip(EditConstraintBT, "Edit Scenario")
        ToolTip1.SetToolTip(DeleteConstraintBT, "Delete Target")
        ToolTip1.SetToolTip(RefreshBT, "Refresh Scenario")
        ToolTip1.SetToolTip(HideConstraintsBT, "Minimize")

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
                                             ByRef input_versionsTV As TreeView, _
                                             ByRef input_mappingDGV As vDataGridView, _
                                             ByRef input_inputsDGV As vDataGridView)

        EntitiesTVPanel.Controls.Add(input_entitiesTV)
        VersionsTVpanel.Controls.Add(input_versionsTV)
        MappingDGVPanel.Controls.Add(input_mappingDGV)
        InputsDGVPanel.Controls.Add(input_inputsDGV)
        input_entitiesTV.Dock = DockStyle.Fill
        input_versionsTV.Dock = DockStyle.Fill
        input_mappingDGV.Dock = DockStyle.Fill
        input_inputsDGV.Dock = DockStyle.Fill

        VersionsTV = input_versionsTV
        EntitiesTV = input_entitiesTV
        cTreeViews_Functions.CheckAllNodes(EntitiesTV)
        AddHandler VersionsTV.AfterSelect, AddressOf VersionsTV_AfterSelect
        AddHandler EntitiesTV.AfterSelect, AddressOf EntitiesTV_AfterSelect
        AddHandler VersionsTV.KeyDown, AddressOf versionsTV_KeyDown
        AddHandler EntitiesTV.KeyDown, AddressOf entitiesTV_KeyDown


    End Sub

    Protected Friend Sub AddScenario(ByRef scenario As Scenario, ByRef index As Int32)

        DisplayInputsForScenario(scenario)
        ExpandConstraintPane()
        AddOutputControls(index, scenario)
        scenario.OutputDGV.Refresh()
        scenario.OutputDGV.Select()

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

    Protected Friend Sub ExportScenarioToSeparateUI(ByRef scenario_text As String, _
                                                    ByRef inputDGV As vDataGridView, _
                                                    ByRef outputDGV As vDataGridView, _
                                                    ByRef chart As Chart)

        Dim genericUI As New GenericView(scenario_text)
        Dim pane As New TableLayoutPanel

        pane.RowStyles.Add(New RowStyle(SizeType.Absolute, inputDGV.Height))
        pane.RowStyles.Add(New RowStyle(SizeType.Absolute, outputDGV.Height))
        pane.RowStyles.Add(New RowStyle(SizeType.Absolute, chart.Height))

        pane.Controls.Add(inputDGV, 0, 0)
        pane.Controls.Add(outputDGV, 0, 1)
        pane.Controls.Add(chart, 0, 2)
        genericUI.Controls.Add(pane)

        pane.Dock = DockStyle.Fill
        inputDGV.Dock = DockStyle.Fill
        outputDGV.Dock = DockStyle.Fill
        chart.Dock = DockStyle.Fill
        genericUI.BackColor = Drawing.Color.White
        genericUI.Show()

    End Sub

#End Region


#Region "Call Backs"

#Region "Inputs Tab"

    Private Sub LaunchConsoBT_Click(sender As Object, e As EventArgs) Handles LaunchConsoBT.Click

        If Not EntitiesTV.SelectedNode Is Nothing Then
            If Not VersionsTV.SelectedNode Is Nothing Then
                If InputsController.IsVersionValid(VersionsTV.SelectedNode.Name) = False Then
                    MsgBox("The Version currently selected is a folder. Please select a Version.")
                    Exit Sub
                End If
                InputsController.ComputeEntity(VersionsTV.SelectedNode.Name, EntitiesTV.SelectedNode)
                ValidateInputsBT.Select()
            Else
                MsgBox("A Version must be selected.")
            End If
        Else
            MsgBox("An entity must be selected.")
        End If

    End Sub

    Private Sub ValidateInputsBT_Click(sender As Object, e As EventArgs) Handles ValidateInputsBT.Click

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
                SimulationsController.AddConstraint(current_scenario_node.Name)
            Else
                SimulationsController.AddConstraint(current_scenario_node.Parent.Name)
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

    Private Sub EditConstraintBT_Click(sender As Object, e As EventArgs) Handles EditConstraintBT.Click

        If is_constraint_displayed = True Then
            CollapseConstraintPane()
        Else
            ExpandConstraintPane()
        End If

    End Sub

#End Region

#Region "Input DGV Panel Buttons"

    Private Sub RefreshBT_Click_1(sender As Object, e As EventArgs) Handles RefreshBT.Click

        If displayed_scenario_id <> "" Then SimulationsController.ComputeScenario(displayed_scenario_id)

    End Sub

    Private Sub DeleteConstraintBT_Click(sender As Object, e As EventArgs) Handles DeleteConstraintBT.Click

        If displayed_scenario_id <> "" Then SimulationsController.DeleteDGVActiveConstraint(displayed_scenario_id)

    End Sub

    Private Sub HideConstraintsBT_Click_1(sender As Object, e As EventArgs) Handles HideConstraintsBT.Click

        CollapseConstraintPane()

    End Sub

    Private Sub AddConstraintBT_Click(sender As Object, e As EventArgs) Handles AddConstraintBT.Click

        If displayed_scenario_id <> "" Then SimulationsController.AddConstraint(displayed_scenario_id)

    End Sub

#End Region

#Region "Scenarios TV Right Click Menu"

    Private Sub EditConstraintRCTV_Click(sender As Object, e As EventArgs) Handles EditConstraintRCTV.Click

        If Not current_scenario_node Is Nothing Then
            If current_scenario_node.Parent Is Nothing Then
                DisplayInputsForScenario(SimulationsController.GetScenario(current_scenario_node.Name))
            Else
                DisplayInputsForScenario(SimulationsController.GetScenario(current_scenario_node.Parent.Name))
            End If
        Else
            MsgBox("Please Select a Scenario.")
        End If

    End Sub

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
                    If current_scenario_node.Name = displayed_scenario_id Then ClearConstraintsEdition()
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

        SimulationsController.AddConstraint(displayed_scenario_id)

    End Sub

    Private Sub DeleteConstraintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteConstraintToolStripMenuItem.Click

        SimulationsController.DeleteDGVActiveConstraint(displayed_scenario_id)

    End Sub

    Private Sub RefreshScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshScenarioToolStripMenuItem.Click

        SimulationsController.ComputeScenario(displayed_scenario_id)

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

    End Sub

    Private Sub tv_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_scenario_node = e.Node

    End Sub

    Private Sub tv_node_mouse_doubleclick(sender As Object, e As TreeNodeMouseClickEventArgs)

        EditConstraintRCTV_Click(sender, e)

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

#End Region


#Region "Utilities"

    Private Sub CollapseConstraintPane()

        SplitContainer2.SplitterDistance = 0
        SplitContainer2.Panel1.Hide()
        is_constraint_displayed = False

    End Sub

    Private Sub ExpandConstraintPane()

        SplitContainer2.SplitterDistance = constraints_splitter_distance
        SplitContainer2.Panel1.Show()
        is_constraint_displayed = True

    End Sub

    Private Sub DisplayInputsForScenario(ByRef scenario As Scenario)

        InputDGPanel.Controls.Clear()
        InputDGPanel.Controls.Add(scenario.InputsDGV)
        scenario.InputsDGV.Dock = DockStyle.Fill

        Dim dividend_formula_option = scenario.dividend_formula_option
        If dividend_formula_option <> -1 Then
            DividendFormulaCB.SelectedItem = dividend_formula_option
            DividendFormulaRB.Checked = True
        Else
            DividendFormulaCB.SelectedItem = ""
            DividendFormulaRB.Checked = False
        End If

        displayed_scenario_id = scenario.scenario_id
        Displayed_ScenarioTB.Text = ScenariosTV.Nodes.Find(scenario.scenario_id, True)(0).Text

    End Sub

    Private Sub ClearConstraintsEdition()

        InputDGPanel.Controls.Clear()
        displayed_scenario_id = ""
        Displayed_ScenarioTB.Text = ""

    End Sub

    Protected Friend Sub RedrawOutputs()

        ScenariiPanelLayout.Controls.Clear()
        ScenariiPanelLayout.RowCount = 0

        Dim index As Int32 = 0
        For Each scenario_node In ScenariosTV.Nodes
            AddOutputControls(index, SimulationsController.GetScenario(scenario_node.name))
        Next

    End Sub

    Private Sub AddOutputControls(ByRef index As Int32, _
                                  ByRef scenario As Scenario)

        ScenariiPanelLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, OUTPUT_PANEL_DGV_ROWS_HEIGHT))
        ScenariiPanelLayout.Controls.Add(scenario.OutputDGV, 0, index)
        ScenariiPanelLayout.Controls.Add(GetNewExportButton, 1, index)
        ScenariiPanelLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, OUTPUT_PANEL_CHART_ROWS_HEIGHT))
        index = index + 1
        ScenariiPanelLayout.Controls.Add(scenario.Outputchart, 0, index)
        index = index + 1
        scenario.OutputDGV.Dock = DockStyle.Fill
        scenario.Outputchart.Dock = DockStyle.Fill


    End Sub

    Private Function GetNewExportButton() As Button

        Dim BT As New Button
        BT.ImageList = ButtonsImageList
        BT.FlatStyle = FlatStyle.Flat
        BT.FlatAppearance.BorderSize = 0
        BT.ImageKey = "blue.jpg"
        AddHandler BT.Click, AddressOf BT_Export_OnClick
        Return BT

    End Function

#End Region



End Class