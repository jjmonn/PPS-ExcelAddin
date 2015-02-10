' CurrenciesManagementUI
'
' User interface for adding/ deleting currencies and seeing/ modifying the exchange rates
'
'
' To do:
'       - CHART display
'       - chart automatically reload when changing version
'       - Add rename rate version
'
' Known bugs:
'
'
'
' Author: Julien Monnereau
' Last modified: 08/02/2015


Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting


Friend Class CurrenciesManagementUI


#Region "Instance Variables"

    ' Objects
    Friend Controller As CExchangeRatesCONTROLER
    Friend ratesView As RatesView
    Private chart As Chart

    ' Variables
    Private mainMenuFlag As Boolean
    Private currenciesMenuFlag As Boolean
    Private VersionsMenuFlag As Boolean = True
    Private right_clicked_node As TreeNode

    ' Menu
    Private version_splitter_distance As Double = 240
    Private chart_splitter_distance As Double = 500
    Private isVersionDisplayed As Boolean
    Private isChartDisplayed As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = New CExchangeRatesCONTROLER(Me)
        Dim ht As New Hashtable
        ht.Add(REPORTS_NAME_VAR, "Exchange Rates")
        chart = ChartsUtilities.CreateChart(ht)
        SplitContainer2.Panel2.Controls.Add(chart)
        chart.Dock = DockStyle.Fill
        chart.BorderlineColor = Drawing.Color.Gray
        chart.BorderlineWidth = 1
        ratesView = New RatesView(rates_DGV, chart)
        If Controller.object_is_alive = False Then
            MsgBox("There seems to be a network connection issue. You should try again and contact the PPS team if the error persist.")
            Me.Dispose()
        End If
        ratesView.AttributeController(Controller)


    End Sub

    Private Sub CurrenciesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized
        '  CollapseChartPane()
        rates_DGV.AllowCopyPaste = True
        rates_DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        rates_DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        rates_DGV.Refresh()
        rates_DGV.Select()
        ExpandChartPane()

    End Sub

#End Region


#Region "Call Backs"

#Region "Main Menu Call backs"

    Private Sub VersionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles rates_versions_button.Click

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
            isChartDisplayed = False
        Else
            ExpandChartPane()
            isChartDisplayed = True
        End If

    End Sub

    Private Sub ImportFromExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromExcelToolStripMenuItem.Click

        Controller.ImportRatesFromExcel()

    End Sub

#End Region

#Region "Currencies Call backs"

    ' New currency call back
    Private Sub NewCurrencyBT_Click(sender As Object, e As EventArgs) Handles AddNewCurrencyToolStripMenuItem.Click, _
                                                                              AddCurrencyToolStripMenuItem.Click
        Controller.AddNewCurrency()

    End Sub

    ' Delete currency -> Managed by PPS Team
    'Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the currency " + Chr(13) + Chr(13) + _
    '                                      curr + Chr(13) + Chr(13), _
    '                                      "Currency deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    'If confirm = DialogResult.Yes Then


#End Region

#Region "Versions Right Click Menu"

    Private Sub select_version_Click(sender As Object, e As EventArgs) Handles select_version.Click

        If right_clicked_node.SelectedImageIndex = 0 Then Controller.ChangeVersion(right_clicked_node.Name)

    End Sub

    Private Sub AddFolderRCM_Click(sender As Object, e As EventArgs) Handles AddFolderRCM.Click

        If Not right_clicked_node Is Nothing Then
            If right_clicked_node.SelectedImageIndex = 1 Then
                Dim name As String = InputBox("Please enter a name for the new Folder")
                If Len(name) < 50 Then Controller.CreateVersion(name, 1, , , right_clicked_node) Else MsgBox("The name cannot exceed 50 characters")
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
            If Controller.IsFolderVersion(right_clicked_node.Name) = True Then
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
                Controller.DeleteVersionsOrFolder(right_clicked_node)
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

    Private Sub rates_version_MouseClick(sender As Object, e As MouseEventArgs) Handles rates_version_TB.MouseClick

        VersionsToolStripMenuItem_Click(sender, e)  ' Display Versions pane

    End Sub

#Region "Versions TV Events"

    Private Sub versionsTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles versionsTV.NodeMouseDoubleClick

        If e.Node.SelectedImageIndex = 0 Then Controller.ChangeVersion(e.Node.Name)

    End Sub

    Private Sub versionsTV_KeyPress(sender As Object, e As KeyPressEventArgs) Handles versionsTV.KeyPress

        If e.KeyChar = Chr(13) AndAlso Not versionsTV.SelectedNode Is Nothing _
        AndAlso versionsTV.SelectedNode.SelectedImageIndex = 0 Then Controller.ChangeVersion(versionsTV.SelectedNode.Name)

        '   If e.KeyChar = Chr(10) Then DeleteVersionBT_Click(sender, e)

    End Sub

    Private Sub versionsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles versionsTV.NodeMouseClick

        If e.Button = Windows.Forms.MouseButtons.Right Then right_clicked_node = e.Node Else right_clicked_node = Nothing

    End Sub

#End Region


#End Region


#Region "Utilities"

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

    End Sub

    Private Sub ExpandChartPane()

        SplitContainer2.SplitterDistance = chart_splitter_distance
        SplitContainer2.Panel1.Show()

    End Sub

#End Region

    Private Sub UI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.CloseExchangeRates()

    End Sub


#End Region



End Class