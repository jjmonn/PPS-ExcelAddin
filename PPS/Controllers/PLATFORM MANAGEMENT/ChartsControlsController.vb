'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/05/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class ChartsControlsController

#Region "Instance Variables"

    ' Objects
    Private ControlCharts As New ControlChart
    Private View As ControlsControl
    Private ChartsTV As New TreeView

    ' Variables
    Private positions_dictionary As New Dictionary(Of String, Double)


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        ControlChart.LoadControlChartsTree(ChartsTV)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ChartsTV)

    End Sub

#End Region


#Region "Interface"

    Friend Sub InitializeDisplay(ByRef input_view As ControlsControl)

        View = input_view
        View.DisplayChartsInit(ChartsTV)

    End Sub

    Friend Sub CreateControlChart(ByRef name As String)

        Dim HT As New Hashtable
        HT.Add(CONTROL_CHART_NAME_VARIABLE, name)
        HT.Add(ITEMS_POSITIONS, 1)
        ControlCharts.CreateControlChart(HT)

        ' after update  priority normal to be implemented !!! 
        'ChartsTV.Nodes.Add(id, name, 0, 0)
        'positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ChartsTV)
        'ControlCharts.UpdateControlChart(id, ITEMS_POSITIONS, positions_dictionary(id))

    End Sub

    Protected Friend Function GetChartPalette(ByRef chart_id As String) As Object

        Return ControlCharts.ReadControlChart(chart_id, CONTROL_CHART_PALETTE_VARIABLE)

    End Function

    Friend Sub UpdateChartName(ByRef controlchart_id As String, ByRef name As String)

        ControlCharts.UpdateControlChart(controlchart_id, CONTROL_CHART_NAME_VARIABLE, name)

    End Sub

    Friend Sub DeleteChart(ByRef controlchart_node As TreeNode)

        For Each node In controlchart_node.Nodes
            ControlCharts.DeleteControlChart(node.Name)
        Next
        ControlCharts.DeleteControlChart(controlchart_node.Name)
        controlchart_node.Remove()
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ChartsTV)
        SendNewPositionsToModel()

    End Sub

    Friend Sub CreateSerie(ByRef controlchart_node As TreeNode, ByRef name As String)

        Dim HT As New Hashtable
        HT.Add(CONTROL_CHART_PARENT_ID_VARIABLE, controlchart_node.Name)
        HT.Add(CONTROL_CHART_NAME_VARIABLE, name)
        HT.Add(ITEMS_POSITIONS, 1)
        ControlCharts.CreateControlChart(HT)

        ' after update  priority normal to be implemented !!! 
        'controlchart_node.Nodes.Add(id, name, 1, 1)
        'positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ChartsTV)
        'ControlCharts.UpdateControlChart(id, ITEMS_POSITIONS, positions_dictionary(id))

    End Sub

    Protected Friend Function GetSerieHT(ByRef serie_id As String)

        Return ControlCharts.GetSerieHT(serie_id)

    End Function

    Protected Friend Sub UpdateChartPalette(ByRef chart_id As String, ByRef palette As String)

        ControlCharts.UpdateControlChart(chart_id, CONTROL_CHART_PALETTE_VARIABLE, palette)

    End Sub

    Protected Friend Sub UpdateName(ByRef node As TreeNode, ByRef name As String)

        ControlCharts.UpdateControlChart(node.Name, CONTROL_CHART_NAME_VARIABLE, name)
        node.Text = name

    End Sub

    Protected Friend Sub UpdateSerieAccountID(ByRef serie_id As String, ByRef account_id As String)

        ControlCharts.UpdateControlChart(serie_id, CONTROL_CHART_ACCOUNT_ID_VARIABLE, account_id)

    End Sub

    Protected Friend Sub UpdateSerieType(ByRef serie_id As String, ByRef type As String)

        ControlCharts.UpdateControlChart(serie_id, CONTROL_CHART_TYPE_VARIABLE, type)

    End Sub

    Protected Friend Sub UpdateSerieColor(ByRef serie_id As String, ByRef color As String)

        ControlCharts.UpdateControlChart(serie_id, CONTROL_CHART_COLOR_VARIABLE, color)

    End Sub

    Protected Friend Sub UpdateSerieChart(ByRef serie_id As String, ByRef chart_id As String)

        ControlCharts.UpdateControlChart(serie_id, CONTROL_CHART_PARENT_ID_VARIABLE, chart_id)

    End Sub

    Protected Friend Sub DeleteSerie(ByRef serie_node As TreeNode)

        ControlCharts.DeleteControlChart(serie_node.Name)
        serie_node.Remove()
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(ChartsTV)
        SendNewPositionsToModel()

    End Sub

#End Region


#Region "Utilities"

    Private Sub SendNewPositionsToModel()

        For Each chart_id In positions_dictionary.Keys
            ControlCharts.UpdateControlChart(chart_id, ITEMS_POSITIONS, positions_dictionary(chart_id))
        Next

    End Sub

#End Region



End Class
