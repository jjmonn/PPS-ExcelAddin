'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 06/01/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.Utilities
Imports System.Drawing


Friend Class SubmissionsControlUI


#Region "Instance Variables"

    ' Objects
    Private Controller As SubmissionsControlsController
    Protected Friend PBar As New ProgressBarControl

    ' Variables
    Private EntitiesTV As TreeView
    Private ControlsDGV As New vDataGridView
    Private charts_dic As Dictionary(Of String, Chart)
    Private current_node As TreeNode

    ' Display
    Private Const DGV_CELLS_FONT_SIZE = 9
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
    Private green_c_style As GridCellStyle
    Private red_c_style As GridCellStyle
    Private Const CHARTS_PANEL_LAYOUT_NB_COLUMNS As Int32 = 4
    Private CHARTS_PANEL_LAYOUT_NB_ROWS As Int32 = 3

#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As SubmissionsControlsController, _
                             ByRef input_entitiesTV As TreeView, _
                             ByRef input_charts_dictionary As Dictionary(Of String, Chart))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        EntitiesTV = input_entitiesTV
        charts_dic = input_charts_dictionary
        EntitiesTV.ImageList = EntitiesTVImageList
        input_entitiesTV.CollapseAll()
        EntitiesTVPanel.Controls.Add(EntitiesTV)
        input_entitiesTV.Dock = DockStyle.Fill
        EntitiesTV.ContextMenuStrip = TVRCM
        AddHandler input_entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler EntitiesTV.NodeMouseClick, AddressOf entitiesTV_node_mouse_click
        AddHandler EntitiesTV.AfterSelect, AddressOf entitiesTV_AfterSelect
        AddHandler EntitiesTV.NodeMouseDoubleClick, AddressOf entitiesTV_NodeMouseDoubleClick

        InitializeCharts()
        InitializeControlsDGV()
        InitializeFormats()

    End Sub

    Private Sub InitializeCharts()

        Dim column_index As Int32 = 1
        Dim columns_count As Int32 = 1
        Dim row_index As Int32 = 1
        Dim rows_count As Int32 = 1
        For Each chart_ As Chart In charts_dic.Values
            ChartsTableLayoutPanel.Controls.Add(chart_, column_index, row_index)
            chart_.Dock = DockStyle.Fill
            column_index = column_index + 1
            rows_count = rows_count + 1
            If columns_count = CHARTS_PANEL_LAYOUT_NB_COLUMNS Then
                row_index = row_index + 1
                rows_count = rows_count + 1
                column_index = 0
                columns_count = 0
            End If
            If rows_count = CHARTS_PANEL_LAYOUT_NB_ROWS Then
                ChartsTableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, chart_.Height))
                CHARTS_PANEL_LAYOUT_NB_ROWS = CHARTS_PANEL_LAYOUT_NB_ROWS + 1
            End If
        Next

    End Sub

    Private Sub InitializeControlsDGV()

        'Dim controls_id_name_dic = ControlsMapping.GetControlsDictionary(CONTROL_ID_VARIABLE, CONTROL_NAME_VARIABLE)
        'ControlsDGV.RowsHierarchy.Visible = False
        'ControlsDGV.ColumnsHierarchy.Items.Add("Controls")

        'For Each control_id In controls_id_name_dic.Keys
        '    Dim row = ControlsDGV.RowsHierarchy.Items.Add(control_id)
        '    ControlsDGV.CellsArea.SetCellValue(row, ControlsDGV.ColumnsHierarchy.Items(0), controls_id_name_dic(control_id))
        'Next

        'DataGridViewsUtil.InitDisplayVDataGridView(ControlsDGV, DGV_THEME)
        'DataGridViewsUtil.DGVSetHiearchyFontSize(ControlsDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        'ControlsDGV.ColumnsHierarchy.AutoStretchColumns = True
        'ControlsDGV.BackColor = System.Drawing.Color.White

        'ControlsDGVPanel.Controls.Add(ControlsDGV)
        'ControlsDGV.Dock = DockStyle.Fill

    End Sub

    Private Sub InitializeFormats()

        green_c_style = GridTheme.GetDefaultTheme(ControlsDGV.VIBlendTheme).GridCellStyle
        green_c_style.TextColor = Color.LimeGreen
        green_c_style.Font = New Font(Me.Font.FontFamily, DGV_CELLS_FONT_SIZE)

        red_c_style = GridTheme.GetDefaultTheme(ControlsDGV.VIBlendTheme).GridCellStyle
        red_c_style.TextColor = Color.Red
        red_c_style.Font = New Font(Me.Font.FontFamily, DGV_CELLS_FONT_SIZE)
       
    End Sub

    Private Sub SubmissionControlUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized
        Me.Controls.Add(PBar)                           ' Progress Bar
        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar
        ChartsTableLayoutPanel.Visible = False
        Controller.ControlSubmissions()
        ChartsTableLayoutPanel.Visible = True

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub UpdateControlDGV(ByRef successfull_controls_list As List(Of String))

        For Each row In ControlsDGV.RowsHierarchy.Items
            If successfull_controls_list.Contains(row.Caption) Then
                ControlsDGV.CellsArea.SetCellDrawStyle(row, ControlsDGV.ColumnsHierarchy.Items(0), green_c_style)
            Else
                ControlsDGV.CellsArea.SetCellDrawStyle(row, ControlsDGV.ColumnsHierarchy.Items(0), red_c_style)
            End If
        Next
        ControlsDGV.Refresh()

    End Sub

#End Region


#Region "Calls Backs"

    Private Sub DisplayEntityControlsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayEntityControlsToolStripMenuItem.Click

        If Not current_node Is Nothing Then Controller.DisplayEntityControls(current_node.Name)

    End Sub

    Private Sub RefreshBT_Click(sender As Object, e As EventArgs) Handles RefreshBT.Click

        Controller.ControlSubmissions()
        If Not current_node Is Nothing Then Controller.DisplayEntityControls(current_node.Name)

    End Sub

#End Region


#Region "Events"

    Private Sub EntitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter : If Not EntitiesTV.SelectedNode Is Nothing Then Controller.DisplayEntityControls(EntitiesTV.SelectedNode.Name)
        End Select

    End Sub

    Private Sub entitiesTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        current_node = e.Node

    End Sub

    Private Sub entitiesTV_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_node = e.Node

    End Sub

    Private Sub entitiesTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_node = e.Node
        Controller.DisplayEntityControls(current_node.Name)

    End Sub


#End Region


End Class