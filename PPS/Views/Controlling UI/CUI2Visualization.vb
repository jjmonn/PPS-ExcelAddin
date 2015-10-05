Imports System.Collections
Imports System.Windows.Forms.DataVisualization.Charting

Public Class CUI2Visualization

#Region "Instance variables"

    Private m_controller As ControllingUIController
    Private m_chartTab As Chart()

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As ControllingUIController)

        InitializeComponent()
        m_controller = p_controller
        m_chartTab = {Chart1, Chart2, Chart3, Chart4}

    End Sub

#End Region

#Region "Interface"

    Friend Sub BindData(ByRef p_chartNb As UInt32, ByRef p_serieName As String, _
                        ByRef p_dataX As IEnumerable, ByRef p_dataY As IEnumerable, _
                              Optional ByRef p_axisType As AxisType = AxisType.Primary)

        If p_chartNb > m_chartTab.Length Then Exit Sub

        ChartsUtilities.BindSerieToChart(m_chartTab(p_chartNb), p_serieName, p_axisType, p_dataX, p_dataY)

    End Sub

    Friend Sub RefreshData(ByRef p_serieName As String, _
                           ByRef p_dataX As IEnumerable, ByRef p_dataY As IEnumerable)

        For Each chart As Chart In m_chartTab
            If Not chart.Series(p_serieName) Is Nothing Then _
                chart.Series(p_serieName).Points.DataBindXY(p_dataX, p_dataY)
        Next

    End Sub

#End Region

End Class
