' CUI2VisualisationChartsSettings.vb
'
' Edition of the user's preferences for CUI2 visualization
'
'
' Author: Julien Monnereau
' Created: 15/10/2015
' Last modified: 25/10/2015


Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.Controls
Imports Microsoft.Office.Interop
Imports System.Windows.Forms



Public Class CUI2VisualisationChartsSettings


#Region "Instance Variables"

    Private m_chart As Chart
    Private m_chartIndex As Int32

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_chart As Chart, _
                   ByRef p_chartIndex As Int32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_chart = p_chart
        m_chartIndex = p_chartIndex
        VTreeViewUtil.LoadTreeview(m_serie1AccountTreeviewBox.TreeView, GlobalVariables.Accounts.m_accountsHash)
        VTreeViewUtil.LoadTreeview(m_serie2AccountTreeviewBox.TreeView, GlobalVariables.Accounts.m_accountsHash)
        InitializeTypesComboboxes()
        LoadCurrentSettings()

        AddHandler m_serie1AccountTreeviewBox.TreeView.KeyDown, AddressOf Serie1TV_KeyDown
        AddHandler m_serie2AccountTreeviewBox.TreeView.KeyDown, AddressOf Serie2TV_KeyDown

    End Sub

    Private Sub InitializeTypesComboboxes()

        Dim lineItem As New ListItem
        lineItem.Value = SeriesChartType.Line
        lineItem.Text = "Line"
        m_serie1TypeComboBox.Items.Add(lineItem)
        m_serie2TypeComboBox.Items.Add(lineItem)

        Dim barItem As New ListItem
        barItem.Value = SeriesChartType.Column
        barItem.Text = "Bar"
        m_serie1TypeComboBox.Items.Add(barItem)
        m_serie2TypeComboBox.Items.Add(barItem)

        Dim stackedbarItem As New ListItem
        stackedbarItem.Value = SeriesChartType.StackedColumn
        stackedbarItem.Text = "Stacked bar"
        m_serie1TypeComboBox.Items.Add(stackedbarItem)
        m_serie2TypeComboBox.Items.Add(stackedbarItem)

        Dim areaItem As New ListItem
        areaItem.Value = SeriesChartType.Area
        areaItem.Text = "Area"
        m_serie1TypeComboBox.Items.Add(areaItem)
        m_serie2TypeComboBox.Items.Add(areaItem)


    End Sub

    Private Sub LoadCurrentSettings()

        Select Case m_chartIndex
            Case 1
                m_chartTitleTextBox.Text = My.Settings.chart1Title
                ' Serie 1
                Dim serie1AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie1AccountTreeviewBox.TreeView, _
                                                                            My.Settings.chart1Serie1AccountId)
                If IsNothing(serie1AccountNode) = False Then
                    m_serie1AccountTreeviewBox.TreeView.SelectedNode = serie1AccountNode
                End If
                m_serie1ColorPicker.SelectedColor = My.Settings.chart1Serie1Color
                GeneralUtilities.SetSelectedItem(m_serie1TypeComboBox, My.Settings.chart1Serie1Type)
                ' Serie 2
                Dim serie2AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie2AccountTreeviewBox.TreeView, _
                                                                          My.Settings.chart1Serie2AccountId)
                If IsNothing(serie2AccountNode) = False Then
                    m_serie2AccountTreeviewBox.TreeView.SelectedNode = serie2AccountNode
                End If
                m_serie2ColorPicker.SelectedColor = My.Settings.chart1Serie2Color
                GeneralUtilities.SetSelectedItem(m_serie2TypeComboBox, My.Settings.chart1Serie2Type)

            Case 2
                m_chartTitleTextBox.Text = My.Settings.chart2Title
                ' Serie 1
                Dim serie1AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie1AccountTreeviewBox.TreeView, _
                                                                            My.Settings.chart2Serie1AccountId)
                If IsNothing(serie1AccountNode) = False Then
                    m_serie1AccountTreeviewBox.TreeView.SelectedNode = serie1AccountNode
                End If
                m_serie1ColorPicker.SelectedColor = My.Settings.chart2Serie1Color
                GeneralUtilities.SetSelectedItem(m_serie1TypeComboBox, My.Settings.chart2Serie1Type)
                ' Serie 2
                Dim serie2AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie2AccountTreeviewBox.TreeView, _
                                                                          My.Settings.chart2Serie2AccountId)
                If IsNothing(serie2AccountNode) = False Then
                    m_serie2AccountTreeviewBox.TreeView.SelectedNode = serie2AccountNode
                End If
                m_serie2ColorPicker.SelectedColor = My.Settings.chart2Serie2Color
                GeneralUtilities.SetSelectedItem(m_serie2TypeComboBox, My.Settings.chart2Serie2Type)

            Case 3
                m_chartTitleTextBox.Text = My.Settings.chart3Title
                ' Serie 1
                Dim serie1AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie1AccountTreeviewBox.TreeView, _
                                                                            My.Settings.chart3Serie1AccountId)
                If IsNothing(serie1AccountNode) = False Then
                    m_serie1AccountTreeviewBox.TreeView.SelectedNode = serie1AccountNode
                End If
                m_serie1ColorPicker.SelectedColor = My.Settings.chart3Serie1Color
                GeneralUtilities.SetSelectedItem(m_serie1TypeComboBox, My.Settings.chart3Serie1Type)
                ' Serie 2
                Dim serie2AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie2AccountTreeviewBox.TreeView, _
                                                                          My.Settings.chart3Serie2AccountId)
                If IsNothing(serie2AccountNode) = False Then
                    m_serie2AccountTreeviewBox.TreeView.SelectedNode = serie2AccountNode
                End If
                m_serie2ColorPicker.SelectedColor = My.Settings.chart3Serie2Color
                GeneralUtilities.SetSelectedItem(m_serie2TypeComboBox, My.Settings.chart3Serie2Type)

            Case 4
                m_chartTitleTextBox.Text = My.Settings.chart4Title
                ' Serie 1
                Dim serie1AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie1AccountTreeviewBox.TreeView, _
                                                                            My.Settings.chart4Serie1AccountId)
                If IsNothing(serie1AccountNode) = False Then
                    m_serie1AccountTreeviewBox.TreeView.SelectedNode = serie1AccountNode
                End If
                m_serie1ColorPicker.SelectedColor = My.Settings.chart4Serie1Color
                GeneralUtilities.SetSelectedItem(m_serie1TypeComboBox, My.Settings.chart4Serie1Type)
                ' Serie 2
                Dim serie2AccountNode As vTreeNode = VTreeViewUtil.FindNode(m_serie2AccountTreeviewBox.TreeView, _
                                                                          My.Settings.chart4Serie2AccountId)
                If IsNothing(serie2AccountNode) = False Then
                    m_serie2AccountTreeviewBox.TreeView.SelectedNode = serie2AccountNode
                End If
                m_serie2ColorPicker.SelectedColor = My.Settings.chart4Serie2Color
                GeneralUtilities.SetSelectedItem(m_serie2TypeComboBox, My.Settings.chart4Serie2Type)

        End Select


    End Sub

#End Region


#Region "Call Backs"

    Private Sub m_saveButton_Click(sender As Object, e As EventArgs) Handles m_saveButton.Click

        ' apply changes on chart
        ' au fur et à mesure ?

        Select Case m_chartIndex
            Case 1
                My.Settings.chart1Title = m_chartTitleTextBox.Text
                ' Serie 1
                If Not m_serie1AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart1Serie1AccountId = m_serie1AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart1Serie1AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart1Serie1Color = m_serie1ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart1Serie1Type = m_serie1TypeComboBox.SelectedItem.Value
                My.Settings.Save()

                ' Serie 2
                If Not m_serie2AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart1Serie2AccountId = m_serie2AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart1Serie2AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart1Serie2Color = m_serie2ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart1Serie2Type = m_serie2TypeComboBox.SelectedItem.Value
                My.Settings.Save()

            Case 2
                My.Settings.chart2Title = m_chartTitleTextBox.Text
                ' Serie 1
                If Not m_serie1AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart2Serie1AccountId = m_serie1AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart2Serie1AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart2Serie1Color = m_serie1ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart2Serie1Type = m_serie1TypeComboBox.SelectedItem.Value
                My.Settings.Save()

                ' Serie 2
                If Not m_serie2AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart2Serie2AccountId = m_serie2AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart2Serie2AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart2Serie2Color = m_serie2ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart2Serie2Type = m_serie2TypeComboBox.SelectedItem.Value
                My.Settings.Save()
            Case 3
                My.Settings.chart3Title = m_chartTitleTextBox.Text
                ' Serie 1
                If Not m_serie1AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart3Serie1AccountId = m_serie1AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart3Serie1AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart3Serie1Color = m_serie1ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart3Serie1Type = m_serie1TypeComboBox.SelectedItem.Value
                My.Settings.Save()

                ' Serie 2
                If Not m_serie2AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart3Serie2AccountId = m_serie2AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart3Serie2AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart3Serie2Color = m_serie2ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart3Serie2Type = m_serie2TypeComboBox.SelectedItem.Value
                My.Settings.Save()

            Case 4
                My.Settings.chart4Title = m_chartTitleTextBox.Text
                ' Serie 1
                If m_serie1AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart4Serie1AccountId = m_serie1AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart4Serie1AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart4Serie1Color = m_serie1ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart4Serie1Type = m_serie1TypeComboBox.SelectedItem.Value
                My.Settings.Save()

                ' Serie 2
                If Not m_serie2AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then
                    My.Settings.chart4Serie2AccountId = m_serie2AccountTreeviewBox.TreeView.SelectedNode.Value
                    My.Settings.Save()
                Else
                    My.Settings.chart4Serie2AccountId = 0
                    My.Settings.Save()
                End If
                My.Settings.chart4Serie2Color = m_serie2ColorPicker.SelectedColor
                My.Settings.Save()
                My.Settings.chart4Serie2Type = m_serie2TypeComboBox.SelectedItem.Value
                My.Settings.Save()
        End Select


    End Sub

#End Region

#Region "Events"

    Private Sub Serie1TV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete
                If Not m_serie1AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then m_serie1AccountTreeviewBox.TreeView.SelectedNode = Nothing
                m_serie1AccountTreeviewBox.Text = ""
        End Select

    End Sub

    Private Sub Serie2TV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete
                If Not m_serie2AccountTreeviewBox.TreeView.SelectedNode Is Nothing Then m_serie2AccountTreeviewBox.TreeView.SelectedNode = Nothing
                m_serie2AccountTreeviewBox.Text = ""
        End Select

    End Sub

#End Region

End Class