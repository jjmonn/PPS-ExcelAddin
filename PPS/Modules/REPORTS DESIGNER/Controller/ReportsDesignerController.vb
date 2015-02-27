' ReportsDesignerController.vb
'
' 
'
'
'
' Author: Julien Monnereau
' Last modified: 09/02/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ReportsDesignerController


#Region "Instance Variables"

    ' Objects
    Private Reports As Report
    Private View As ReportsDesignerUI
    Private ReportsTV As New TreeView

    ' Variables
    Private positions_dictionary As New Dictionary(Of String, Double)
    Private accounts_name_id_dic As Hashtable

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef table_name As String, _
                             Optional ByRef accounts_id_short_list As List(Of String) = Nothing)

        Reports = New Report(table_name)
        Report.LoadReportsTV(ReportsTV)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ReportsTV)
        accounts_name_id_dic = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_ID_VARIABLE)
        View = New ReportsDesignerUI(Me, ReportsTV)
        Dim accounts_names_list As List(Of String) = AccountsMapping.GetAccountsNamesList(AccountsMapping.LOOKUP_ALL)
        If Not accounts_id_short_list Is Nothing Then FilterAccountsNames(accounts_id_short_list, accounts_names_list)
        View.InitializeDisplay(accounts_names_list)
        View.Show()

    End Sub

#End Region


#Region "CRUD Interface"

#Region "Updates"

#Region "Report"

    Protected Friend Sub UpdateReportPalette(ByRef report_id As String, ByRef palette As String)

        Reports.UpdateReport(report_id, REPORTS_PALETTE_VAR, palette)

    End Sub

    Protected Friend Sub UpdateReportType(ByRef report_id As String, ByRef type As String)

        Reports.UpdateReport(report_id, REPORTS_TYPE_VAR, type)

    End Sub

    Protected Friend Sub UpdateReportAxis1(ByRef report_id As String, ByRef axis1_name As String)

        Reports.UpdateReport(report_id, REPORTS_AXIS1_NAME_VAR, axis1_name)

    End Sub

    Protected Friend Sub UpdateReportAxis2(ByRef report_id As String, ByRef axis2_name As String)

        Reports.UpdateReport(report_id, REPORTS_AXIS2_NAME_VAR, axis2_name)

    End Sub

#End Region

#Region "Serie"

    Protected Friend Sub UpdateSerieAccountID(ByRef serie_id As String, ByRef account_name As String)

        Reports.UpdateReport(serie_id, REPORTS_ACCOUNT_ID, accounts_name_id_dic(account_name))

    End Sub

    Protected Friend Sub UpdateSerieType(ByRef serie_id As String, ByRef type As String)

        Reports.UpdateReport(serie_id, CONTROL_CHART_TYPE_VARIABLE, Type)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

    Protected Friend Sub UpdateSerieColor(ByRef serie_id As String, ByRef color As Int32)

        Reports.UpdateReport(serie_id, CONTROL_CHART_COLOR_VARIABLE, color)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

    ' not used
    Protected Friend Sub UpdateSerieChart(ByRef serie_id As String, ByRef report_id As String)

        Reports.UpdateReport(serie_id, CONTROL_CHART_PARENT_ID_VARIABLE, report_id)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

    Protected Friend Sub UpdateSerieAxis(ByRef serie_id As String, ByRef axis As String)

        Reports.UpdateReport(serie_id, REPORTS_SERIE_AXIS_VAR, axis)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

    Protected Friend Sub UpdateSerieWidth(ByRef serie_id As String, ByRef width As String)

        Reports.UpdateReport(serie_id, REPORTS_SERIE_WIDTH_VAR, width)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

    Protected Friend Sub UpdateSerieDisplayValues(ByRef serie_id As String, ByRef display_values As Int32)

        Reports.UpdateReport(serie_id, REPORTS_DISPLAY_VALUES_VAR, display_values)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

    Protected Friend Sub UpdateSerieUnit(ByRef serie_id As String, ByRef unit As String)

        Reports.UpdateReport(serie_id, REPORTS_SERIE_UNIT_VAR, unit)
        UpdateSeriePreview(serie_id, View.current_report_node.Index)

    End Sub

#End Region

#End Region

    Protected Friend Sub CreateReport(ByRef name As String)

        Dim id As String = TreeViewsUtilities.GetNewNodeKey(ReportsTV, REPORTS_TOKEN_SIZE)
        Dim HT As New Hashtable
        HT.Add(REPORTS_ID_VAR, id)
        HT.Add(REPORTS_NAME_VAR, name)
        HT.Add(ITEMS_POSITIONS, 1)
        Reports.CreateReport(HT)
        ReportsTV.Nodes.Add(id, name, 0, 0)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ReportsTV)
        Reports.UpdateReport(id, ITEMS_POSITIONS, positions_dictionary(id))
        ReportsTV.SelectedNode = ReportsTV.Nodes.Find(id, True)(0)
        View.DisplayPreviewChart(Reports.GetSerieHT(id), ReportsTV.SelectedNode)

    End Sub

    Protected Friend Sub DeleteReport(ByRef reports_node As TreeNode)

        For Each node In reports_node.Nodes
            Reports.DeleteReport(node.Name)
        Next
        Reports.DeleteReport(reports_node.Name)
        reports_node.Remove()
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ReportsTV)
        SendNewPositionsToModel()

    End Sub

    Protected Friend Sub CreateSerie(ByRef reports_node As TreeNode, ByRef name As String)

        Dim id As String = TreeViewsUtilities.GetNewNodeKey(ReportsTV, REPORTS_TOKEN_SIZE)
        Dim HT As New Hashtable
        HT.Add(REPORTS_ID_VAR, id)
        HT.Add(REPORTS_PARENT_ID_VAR, reports_node.Name)
        HT.Add(REPORTS_NAME_VAR, name)
        HT.Add(ITEMS_POSITIONS, 1)
        Reports.CreateReport(HT)
        reports_node.Nodes.Add(id, name, 1, 1)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ReportsTV)
        Reports.UpdateReport(id, ITEMS_POSITIONS, positions_dictionary(id))
        ReportsTV.SelectedNode = ReportsTV.Nodes.Find(id, True)(0)
        View.DisplayPreviewChart(Reports.GetSerieHT(ReportsTV.SelectedNode.Parent.Name), ReportsTV.SelectedNode.Parent)

    End Sub

    Protected Friend Function GetSerieHT(ByRef serie_id As String)

        Return Reports.GetSerieHT(serie_id)

    End Function

    Protected Friend Sub UpdateName(ByRef node As TreeNode, ByRef name As String)

        Reports.UpdateReport(node.Name, REPORTS_NAME_VAR, name)
        node.Text = name

    End Sub

    Protected Friend Sub DeleteSerie(ByRef serie_node As TreeNode)

        Reports.DeleteReport(serie_node.Name)
        View.DisplayPreviewChart(Reports.GetSerieHT(serie_node.Parent.Name), serie_node.Parent)
        serie_node.Remove()
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ReportsTV)
        SendNewPositionsToModel()

    End Sub

#End Region


#Region "Display Interface"

    Protected Friend Sub DisplayChartSeries(ByRef report_node As TreeNode)

        For Each serie_node As TreeNode In report_node.Nodes
            View.DisplayPreviewSerie(Reports.GetSerieHT(serie_node.Name), serie_node.Index)
        Next

    End Sub

#End Region


#Region "Utilities"

    Private Sub SendNewPositionsToModel()

        For Each report_id In positions_dictionary.Keys
            Reports.UpdateReport(report_id, ITEMS_POSITIONS, positions_dictionary(report_id))
        Next

    End Sub

    Private Sub UpdateSeriePreview(ByRef serie_id As String, _
                                   ByRef serie_index As Int32)

        Dim report_id As String = Reports.ReadReport(serie_id, REPORTS_PARENT_ID_VAR)
        If Reports.ReadReport(report_id, REPORTS_TYPE_VAR) = CHART_REPORT_TYPE Then
            View.DisplayPreviewChart(Reports.GetSerieHT(report_id), ReportsTV.Nodes.Find(report_id, True)(0))
        End If

    End Sub

    Private Sub FilterAccountsNames(ByRef accounts_id_short_list As List(Of String), _
                                    ByRef accounts_name_list As List(Of String))

        For Each account_id As String In View.accounts_id_name_dic.Keys
            If accounts_id_short_list.Contains(account_id) = False Then accounts_name_list.Remove(View.accounts_id_name_dic(account_id))
        Next

    End Sub

#End Region



End Class
