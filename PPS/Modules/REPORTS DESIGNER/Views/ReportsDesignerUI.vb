'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 09/02/2015


Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections
Imports System.Collections.Generic


Friend Class ReportsDesignerUI


#Region "Instance Variables"

    ' Objects
    Private Controller As ReportsDesignerController
    Private reportsTV As TreeView
    Protected Friend chart As Chart

    ' Variables
    Private chart_x_serie As Int32() = {1, 2, 3, 4, 5}
    Protected Friend current_report_node As TreeNode
    Private current_serie_id As String = ""
    Private current_report_id As String = ""
    Private isDisplayingSerie As Boolean
    Protected Friend accounts_id_name_dic As Hashtable

    ' Constants
    Protected Friend Const REPORT_TYPE_CHART_DISPLAY As String = "Chart"
    Protected Friend Const REPORT_TYPE_TABLE_DISPLAY As String = "Table"
    Private Const DEFAULT_SERIE_WIDTH As Decimal = 20

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_controller As ReportsDesignerController, _
                             ByRef input_reportsTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        reportsTV = input_reportsTV
        ReportsTVPanel.Controls.Add(reportsTV)
        reportsTV.Dock = DockStyle.Fill
        reportsTV.ImageList = ReportsTVImageList
        reportsTV.ContextMenuStrip = TVRCM
        Dim ht As New Hashtable
        ht.Add(REPORTS_NAME_VAR, "Chart Preview")
        chart = ChartsUtilities.CreateChart(ht)
        ChartPanel.Controls.Add(chart)
        chart.Dock = DockStyle.Fill
        accounts_id_name_dic = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_NAME_VARIABLE)
        AddHandler reportsTV.AfterSelect, AddressOf reportsTV_AfterSelect
        AddHandler reportsTV.NodeMouseClick, AddressOf reportsTV_node_mouse_click
        AddHandler reportsTV.KeyDown, AddressOf reportsTV_KeyDown

    End Sub

    Protected Friend Sub InitializeDisplay(ByRef accounts_names_list As List(Of String))


        Dim palettes_list As List(Of String) = PalettesMapping.GetPalettesList
        For Each palette In palettes_list
            ReportPaletteCB.Items.Add(palette)
        Next

        For Each account_name In accounts_names_list
            ItemCB.Items.Add(account_name)
        Next

        For Each serie_type In SeriesMapping.GetSerieTypesList
            TypeCB.Items.Add(serie_type)
        Next

        ReportPaletteCB.Items.Add("")
        ItemCB.Items.Add("")
        TypeCB.Items.Add("")

    End Sub

    Private Sub ReportsMGTUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized

    End Sub

#End Region


#Region "Display"

    Private Sub DisplayDataIfSerie()

        If Not current_report_node.Parent Is Nothing Then
            current_serie_id = current_report_node.Name
            DisplaySerieData(current_serie_id)
            If current_report_node.Parent.Name <> current_report_id Then
                current_report_id = current_report_node.Parent.Name
                DisplayReportOptions(current_report_id)
                DisplayPreviewChart(Controller.GetSerieHT(current_report_id), current_report_node.Parent)
            End If
        Else
            ClearSerieDisplay()
            If current_report_node.Name <> current_report_id Then
                DisplayReportOptions(current_report_node.Name)
                DisplayPreviewChart(Controller.GetSerieHT(current_report_id), current_report_node)
            End If
        End If

    End Sub

    Private Sub DisplaySerieData(ByRef serie_id As String)

        If serie_id <> "" Then
            isDisplayingSerie = True
            Dim ht As Hashtable = Controller.GetSerieHT(serie_id)
            NameTB.Text = current_report_node.Text
            If Not IsDBNull(ht(REPORTS_TYPE_VAR)) Then TypeCB.Text = ht(REPORTS_TYPE_VAR) Else TypeCB.Text = ""
            If Not IsDBNull(ht(REPORTS_COLOR_VAR)) Then ColorBT.BackColor = System.Drawing.Color.FromArgb(ht(REPORTS_COLOR_VAR)) Else ColorBT.BackColor = Drawing.Color.White
            If Not IsDBNull(ht(REPORTS_ACCOUNT_ID)) Then ItemCB.Text = accounts_id_name_dic(ht(REPORTS_ACCOUNT_ID)) Else ItemCB.Text = ""
            If Not IsDBNull(ht(REPORTS_SERIE_AXIS_VAR)) Then AxisCB.SelectedItem = ht(REPORTS_SERIE_AXIS_VAR) Else AxisCB.Text = ""
            If Not IsDBNull(ht(REPORTS_SERIE_UNIT_VAR)) Then UnitTB.Text = ht(REPORTS_SERIE_UNIT_VAR) Else UnitTB.Text = ""
            If ht(REPORTS_DISPLAY_VALUES_VAR) = 1 Then ValuesDisplayRB.Checked = True Else ValuesDisplayRB.Checked = False
            If Not IsDBNull(ht(REPORTS_SERIE_WIDTH_VAR)) Then WidthNumericUpDown.Value = ht(REPORTS_SERIE_WIDTH_VAR) Else WidthNumericUpDown.Value = DEFAULT_SERIE_WIDTH
            isDisplayingSerie = False
        End If

    End Sub

    Private Sub ClearSerieDisplay()

        isDisplayingSerie = True
        NameTB.Text = ""
        TypeCB.Text = ""
        ColorBT.BackColor = Drawing.Color.White
        ItemCB.Text = ""
        AxisCB.Text = ""
        UnitTB.Text = ""
        ValuesDisplayRB.Checked = False
        WidthNumericUpDown.Value = DEFAULT_SERIE_WIDTH
        isDisplayingSerie = False
        current_serie_id = ""

    End Sub

    Private Sub DisplayReportOptions(ByRef report_id As String)

        current_report_id = report_id
        Dim ht As Hashtable = Controller.GetSerieHT(report_id)
        isDisplayingSerie = True
        If Not IsDBNull(ht(REPORTS_NAME_VAR)) Then ReportNameTB.Text = ht(REPORTS_NAME_VAR) Else ReportNameTB.Text = ""
        If Not IsDBNull(ht(REPORTS_TYPE_VAR)) Then If ht(REPORTS_TYPE_VAR) = CHART_REPORT_TYPE Then ReportTypeCB.Text = REPORT_TYPE_CHART_DISPLAY Else ReportTypeCB.Text = REPORT_TYPE_TABLE_DISPLAY
        If Not IsDBNull(ht(REPORTS_AXIS1_NAME_VAR)) Then Axis1TB.Text = ht(REPORTS_AXIS1_NAME_VAR) Else Axis1TB.Text = ""
        If Not IsDBNull(ht(REPORTS_AXIS2_NAME_VAR)) Then Axis2TB.Text = ht(REPORTS_AXIS2_NAME_VAR) Else Axis2TB.Text = ""
        If Not IsDBNull(ht(REPORTS_PALETTE_VAR)) Then ReportPaletteCB.Text = ht(REPORTS_PALETTE_VAR) Else ReportPaletteCB.Text = ""
        isDisplayingSerie = False

    End Sub

    Private Sub CleaRreportOptions()

        isDisplayingSerie = True
        ReportNameTB.Text = ""
        ReportTypeCB.Text = ""
        Axis1TB.Text = ""
        Axis2TB.Text = ""
        ReportPaletteCB.SelectedItem = ""
        isDisplayingSerie = False
        current_report_id = ""

    End Sub

#End Region


#Region "Call backs"

    Private Sub NewreportBT_Click(sender As Object, e As EventArgs) Handles NewReportBT.Click

        Dim name = InputBox("Please enter the Name of the New report: ")
        If name <> "" Then Controller.CreateReport(name)

    End Sub

    Private Sub NewSerieBT_Click(sender As Object, e As EventArgs) Handles NewSerieBT.Click

        If Not current_report_node Is Nothing Then
            Dim name As String = InputBox("new Serie Name: ")
            If name <> "" Then
                If current_report_node.Parent Is Nothing Then
                    Controller.CreateSerie(current_report_node, name)
                    current_report_node.ExpandAll()
                Else
                    Controller.CreateSerie(current_report_node.Parent, name)
                    current_report_node.Parent.ExpandAll()
                End If
            End If
        Else
            MsgBox("A report must be selected to add a new Serie.")
        End If

    End Sub

    Private Sub DeletereportsBT_Click(sender As Object, e As EventArgs) Handles DeleteReportBT.Click

        If Not current_report_node Is Nothing Then
            If current_report_node.Parent Is Nothing Then
                If current_report_id = current_report_node.Name Then CleaRreportOptions()
                Controller.DeleteReport(current_report_node)
            Else
                If current_serie_id = current_report_node.Name Then ClearSerieDisplay()
                Controller.DeleteSerie(current_report_node)
            End If
        Else
            MsgBox("A report or a Serie must be selected.")
        End If

    End Sub

#Region "Right Click Menu"

    Private Sub DeleteRCM_Click(sender As Object, e As EventArgs) Handles DeleteRCM.Click

        DeletereportsBT_Click(sender, e)

    End Sub

    Private Sub NewSerieRCM_Click(sender As Object, e As EventArgs) Handles NewSerieRCM.Click

        NewSerieBT_Click(sender, e)

    End Sub

    Private Sub NewreportRCM_Click(sender As Object, e As EventArgs) Handles NewreportRCM.Click

        NewreportBT_Click(sender, e)

    End Sub

    Private Sub RenameRCBT_Click(sender As Object, e As EventArgs) Handles RenameRCM.Click

        If Not current_report_node Is Nothing Then
            Dim name = InputBox("New Name: ")
            If name <> "" Then
                Controller.UpdateName(current_report_node, name)
                If current_report_id = current_report_node.Name Then ReportNameTB.Text = name Else NameTB.Text = name
            End If
        Else
            MsgBox("A report or a Serie must be selected.")
        End If

    End Sub

#End Region

#End Region


#Region "Reports and Series Events"

#Region "Reports"

    Private Sub chart_paletteCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ReportPaletteCB.SelectedIndexChanged

        If current_report_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateReportPalette(current_report_id, ReportPaletteCB.SelectedText)

    End Sub

    Private Sub ReportTypeCB_SelectedIndexChanged(sender As Object, e As EventArgs)

        If current_report_id <> "" AndAlso isDisplayingSerie = False Then
            Select Case ReportTypeCB.SelectedText
                Case REPORT_TYPE_CHART_DISPLAY : Controller.UpdateReportType(current_report_id, CHART_REPORT_TYPE)
                Case REPORT_TYPE_TABLE_DISPLAY : Controller.UpdateReportType(current_report_id, TABLE_REPORT_TYPE)
            End Select
        End If

    End Sub

    Private Sub Axis1TB_TextChanged(sender As Object, e As EventArgs) Handles Axis1TB.TextChanged

        If current_report_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateReportAxis1(current_report_id, Axis1TB.Text)

    End Sub

    Private Sub Axis2TB_TextChanged(sender As Object, e As EventArgs) Handles Axis2TB.TextChanged

        If current_report_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateReportAxis2(current_report_id, Axis2TB.Text)

    End Sub

#End Region

#Region "Series"

    Private Sub ItemCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ItemCB.SelectedIndexChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateSerieAccountID(current_serie_id, ItemCB.Text)

    End Sub

    Private Sub TypeCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeCB.SelectedIndexChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateSerieType(current_serie_id, TypeCB.Text)

    End Sub

    Private Sub AxisCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AxisCB.SelectedIndexChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateSerieAxis(current_serie_id, AxisCB.Text)

    End Sub

    Private Sub WidthNumericUpDown_ValueChanged(sender As Object, e As EventArgs) Handles WidthNumericUpDown.ValueChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateSerieWidth(current_serie_id, WidthNumericUpDown.Value)

    End Sub

    Private Sub UnitTB_TextChanged(sender As Object, e As EventArgs) Handles UnitTB.TextChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then Controller.UpdateSerieUnit(current_serie_id, UnitTB.Text)

    End Sub

    Private Sub ValuesDisplayRB_CheckedChanged(sender As Object, e As EventArgs) Handles ValuesDisplayRB.CheckedChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then
            If ValuesDisplayRB.Checked = True Then
                Controller.UpdateSerieDisplayValues(current_serie_id, 1)
            Else
                Controller.UpdateSerieDisplayValues(current_serie_id, 0)
            End If
        End If

    End Sub

    Private Sub ColorBT_Click(sender As Object, e As EventArgs) Handles ColorBT.Click

        If current_serie_id <> "" Then
            If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                ColorBT.BackColor = ColorDialog1.Color
                Controller.UpdateSerieColor(current_serie_id, ColorDialog1.Color.ToArgb)
            End If
        End If

    End Sub

#End Region

#End Region


#Region "Report TV Events"

    Private Sub reportsTV_KeyDown(sender As Object, e As KeyEventArgs)

        current_report_node = reportsTV.SelectedNode
        Select Case e.KeyCode
            Case Keys.Enter : DisplayDataIfSerie()
            Case Keys.Delete : DeletereportsBT_Click(sender, e)
        End Select

    End Sub

    Private Sub reportsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        current_report_node = e.Node
        DisplayDataIfSerie()

    End Sub

    Private Sub reportsTV_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_report_node = e.Node
        DisplayDataIfSerie()

    End Sub

#End Region


#Region "Preview Chart"

    Protected Friend Sub DisplayPreviewChart(ByRef chartHT As Hashtable, _
                                             ByRef report_node As TreeNode)

        If chartHT(REPORTS_TYPE_VAR) = CHART_REPORT_TYPE Then
            chart.Visible = True
            ChartPanel.Controls.Clear()
            chart = ChartsUtilities.CreateChart(chartHT)
            ChartPanel.Controls.Add(chart)
            chart.Dock = DockStyle.Fill
            Controller.DisplayChartSeries(report_node)
        Else
            chart.Visible = False
        End If

    End Sub

    Protected Friend Sub DisplayPreviewSerie(ByRef serieHT As Hashtable, _
                                             ByRef serie_index As Int32)

        ChartsUtilities.AddSerieToChart(chart, serieHT)
        Dim dumb_y_serie(chart_x_serie.Length - 1) As Double
        For i As Int32 = 0 To chart_x_serie.Length - 1
            dumb_y_serie(i) = CInt(Int((9 * Rnd()) + 1))
        Next
        chart.Series(serie_index).Points.DataBindXY(chart_x_serie, dumb_y_serie)

    End Sub

#End Region




End Class