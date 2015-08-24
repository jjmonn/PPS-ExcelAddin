' CurrenciesControl
'
' User interface for adding/ deleting currencies and seeing/ modifying the exchange rates
'
'
' To do:
'       - Chart automatically reload when changing version
'       - Add rename rate version
'       - PRIORITY HIGH / SIMPLIFY (NO CURRENCY MANAGEMENT here) + right click node won t work with vtreeview
'
'
' Known bugs:
'
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.Controls


Friend Class CurrenciesControl


#Region "Instance Variables"

    ' Objects
    Friend Controller As ExchangeRatesController
    Private rates_versionsTV As vTreeView
    Friend ratesView As RatesView
    Protected Friend rates_DGV As New vDataGridView
    Private chart As Chart

    ' Variables
    Private mainMenuFlag As Boolean
    Private currenciesMenuFlag As Boolean
    Private VersionsMenuFlag As Boolean = True
    Private right_clicked_node As vTreeNode

    ' Menu
    Private version_splitter_distance As Double = 240
    Private chart_splitter_distance As Double = 500
    Private isVersionDisplayed As Boolean
    Private isChartDisplayed As Boolean

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_controller As ExchangeRatesController, _
                   ByRef input_rates_versionsTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        rates_versionsTV = input_rates_versionsTV
        SplitContainer1.Panel1.Controls.Add(rates_versionsTV)
        rates_versionsTV.Dock = DockStyle.Fill
        SplitContainer1.Panel2.Controls.Add(rates_DGV)
        rates_DGV.Dock = DockStyle.Fill
        rates_DGV.ContextMenuStrip = dgvRCM

        Dim ht As New Hashtable
        ht.Add(REPORTS_NAME_VAR, "Exchange Rates")
        chart = ChartsUtilities.CreateChart(ht)
        SplitContainer2.Panel2.Controls.Add(chart)
        chart.Dock = DockStyle.Fill
        chart.BorderlineColor = Drawing.Color.Gray
        chart.BorderlineWidth = 1
        ratesView = New RatesView(rates_DGV, chart)
        ratesView.AttributeController(Controller)

        rates_versionsTV.ContextMenuStrip = VersionsRCMenu
        AddHandler rates_versionsTV.MouseClick, AddressOf rates_version_MouseClick
        AddHandler rates_versionsTV.KeyPress, AddressOf rates_versionsTV_KeyPress
        AddHandler rates_versionsTV.MouseDoubleClick, AddressOf rates_versionsTV_NodeMouseDoubleClick

    End Sub

    Private Sub CurrenciesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CollapseChartPane()
        rates_DGV.AllowCopyPaste = True
        rates_DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        rates_DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        rates_DGV.Refresh()
        rates_DGV.Select()
        'ExpandChartPane()

    End Sub

    Friend Sub closeControl()


    End Sub


#End Region


#Region "Call Backs"

#Region "Main Menu Call backs"

    Private Sub VersionsToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If isVersionDisplayed = True Then
            CollapseVersionPane()
            isVersionDisplayed = False
        Else
            ExpandVersionPane()
            isVersionDisplayed = True
        End If
    End Sub

    Private Sub chart_display_Click(sender As Object, e As EventArgs) Handles chart_button.Click

        If isChartDisplayed = True Then
            CollapseChartPane()
        Else
            ExpandChartPane()
        End If

    End Sub

    Private Sub ImportFromExcelToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Controller.ImportRatesFromExcel()

    End Sub


#End Region

#Region "Versions Right Click Menu"

    Private Sub select_version_Click(sender As Object, e As EventArgs) Handles select_version.Click

        If Controller.IsFolderVersion(right_clicked_node.Value) = False Then Controller.ChangeVersion(right_clicked_node.Value)

    End Sub

    Private Sub AddFolderRCM_Click(sender As Object, e As EventArgs) Handles AddFolderRCM.Click

        If Not right_clicked_node Is Nothing Then
            If right_clicked_node.SelectedImageIndex = 1 Then
                Dim name As String = InputBox("Please enter a name for the new Folder")
                If Len(name) < 50 Then Controller.CreateVersion(name, 1, , , right_clicked_node.Value) Else MsgBox("The name cannot exceed 50 characters")
            Else
                MsgBox("A folder can only be added to another folder")
            End If
        Else
            Dim name As String = InputBox("Please enter a name for the new Folder")
            If Len(name) < 50 Then Controller.CreateVersion(name, 1) Else MsgBox("The name cannot exceed 50 characters")
        End If

    End Sub

    Private Sub AddRatesVersionRCM_Click(sender As Object, e As EventArgs) Handles AddRatesVersionRCM.Click

        If Not right_clicked_node Is Nothing Then
            If Controller.IsFolderVersion(right_clicked_node.Value) = True Then
                Controller.ShowNewRatesVersion(right_clicked_node)
            Else
                MsgBox("A Version can only be added under a folder")
            End If
        Else
            Controller.ShowNewRatesVersion()
        End If

    End Sub

    Private Sub DeleteVersionBT_Click(sender As Object, e As EventArgs) Handles DeleteVersionRCM.Click

        If Not right_clicked_node Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the version " + Chr(13) + Chr(13) + _
                                                      right_clicked_node.Text + Chr(13) + Chr(13) + _
                                                      "This version and all sub versions will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                      "Version deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Controller.DeleteRatesVersion(right_clicked_node.value)
            End If
        End If

    End Sub

#End Region

#Region "DGV Right Click Menu"

    Private Sub expand_periods_Click(sender As Object, e As EventArgs) Handles expand_periods.Click

        rates_DGV.RowsHierarchy.ExpandAllItems()

    End Sub

    Private Sub collapse_periods_Click(sender As Object, e As EventArgs) Handles collapse_periods.Click

        rates_DGV.RowsHierarchy.CollapseAllItems()

    End Sub

    Private Sub CopyRateDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyRateDownToolStripMenuItem.Click

        ratesView.CopyRateValueDown()

    End Sub

#End Region

#End Region


#Region "Events"

    Private Sub rates_version_MouseClick(sender As Object, e As MouseEventArgs)

        ' VersionsToolStripMenuItem_Click(sender, e)  ' Display Versions pane

    End Sub

#Region "Versions TV Events"

    Private Sub rates_versionsTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If e.Node.SelectedImageIndex = 0 Then Controller.ChangeVersion(e.Node.Name)

    End Sub

    Private Sub rates_versionsTV_KeyPress(sender As Object, e As KeyPressEventArgs)

        If e.KeyChar = Chr(13) AndAlso Not rates_versionsTV.SelectedNode Is Nothing _
        AndAlso rates_versionsTV.SelectedNode.SelectedImageIndex = 0 Then Controller.ChangeVersion(rates_versionsTV.SelectedNode.Value)

        '   If e.KeyChar = Chr(10) Then DeleteVersionBT_Click(sender, e)

    End Sub

    Private Sub rates_versionsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Right _
        AndAlso Not rates_versionsTV.HitTest(e.Location) Is Nothing Then
            right_clicked_node = rates_versionsTV.HitTest(e.Location)
        Else
            right_clicked_node = Nothing
        End If

    End Sub

#End Region


#End Region


#Region "Menu Utilities"

    Private Sub CollapseVersionPane()

        SplitContainer1.SplitterDistance = 0
        SplitContainer1.Panel1.Hide()

    End Sub

    Private Sub ExpandVersionPane()

        SplitContainer1.SplitterDistance = version_splitter_distance
        SplitContainer1.Panel1.Show()

    End Sub

    Private Sub CollapseChartPane()

        SplitContainer2.SplitterDistance = SplitContainer2.Height
        SplitContainer2.Panel2.Hide()
        isChartDisplayed = False

    End Sub

    Private Sub ExpandChartPane()

        SplitContainer2.SplitterDistance = chart_splitter_distance
        SplitContainer2.Panel2.Show()
        isChartDisplayed = True

    End Sub


#End Region



End Class
