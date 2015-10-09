' CurrenciesControl
'
' User interface for modifying the exchange rates
'
'
' To do:
'       - Add rename rate version
'       
'
' Known bugs:
'
'
'
' Author: Julien Monnereau
' Last modified: 19/09/2015


Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.Controls
Imports System.Drawing


Friend Class ExchangeRatesView


#Region "Instance Variables"

    ' Objects
    Friend m_controller As ExchangeRatesController
    Friend m_ratesDataGridView As New vDataGridView
    Private m_ratesVersionsTV As vTreeView

    ' Variables
    Private m_mainCurrency As Int32
    Friend m_currentRatesVersionId As Int32
    Private m_isFillingCells As Boolean
    Private m_isCopyingValueDown As Boolean = False
    Private m_currencyTextBoxEditor As New TextBoxEditor()


    ' Constants
    Private LINES_WIDTH As Single = 3

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As ExchangeRatesController, _
                   ByRef p_ratesVersionsTV As vTreeView, _
                   ByRef p_mainCurrency As Int32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        m_ratesVersionsTV = p_ratesVersionsTV
        m_mainCurrency = p_mainCurrency

        SplitContainer1.Panel1.Controls.Add(m_ratesVersionsTV)
        SplitContainer1.Panel2.Controls.Add(m_ratesDataGridView)

        m_ratesVersionsTV.Dock = DockStyle.Fill
        m_ratesDataGridView.Dock = DockStyle.Fill
        m_ratesDataGridView.ContextMenuStrip = dgvRCM
        m_ratesVersionsTV.ContextMenuStrip = VersionsRCMenu
        m_ratesVersionsTV.ImageList = ratesVersionsIL
        VTreeViewUtil.InitTVFormat(m_ratesVersionsTV)

        AddHandler m_ratesDataGridView.CellValueChanging, AddressOf RatesDataGridView_CellValueChanging
        AddHandler m_ratesVersionsTV.KeyPress, AddressOf Rates_versionsTV_KeyPress
        AddHandler m_ratesVersionsTV.MouseDoubleClick, AddressOf RatesVersionsTV_MouseDoubleClick
        '     AddHandler m_ratesVersionsTV.MouseClick, AddressOf Rates_versionsTV_MouseClick
        DesactivateUnallowed()
    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            AddRatesVersionRCM.Enabled = False
            DeleteVersionRCM.Enabled = False
            AddFolderRCM.Enabled = False
            CreateFolderToolStripMenuItem.Enabled = False
            CreateVersionToolStripMenuItem.Enabled = False
            DeleteToolStripMenuItem.Enabled = False
            ImportFromExcelToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub CurrenciesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        m_ratesDataGridView.AllowCopyPaste = True
        m_ratesDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        m_ratesDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = True
        m_ratesDataGridView.Refresh()
        m_ratesDataGridView.Select()
    
    End Sub

    Friend Sub closeControl()


    End Sub

#End Region


#Region "Interface"

    Private Sub ChangeRatesVersionDisplayRequest(ByRef p_ratesVersionId As Int32)

        m_controller.DisplayRates(p_ratesVersionId)

    End Sub

    Delegate Sub TVUpdate_Delegate(ByRef ratesVersion_id As Int32, _
                                   ByRef ratesVersion_parent_id As Int32, _
                                   ByRef ratesVersion_name As String, _
                                   ByRef ratesVersion_image As Int32)
    Friend Sub TVUpdate(ByRef ratesVersion_id As Int32, _
                        ByRef ratesVersion_parent_id As Int32, _
                        ByRef ratesVersion_name As String, _
                        ByRef ratesVersion_image As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVUpdate_Delegate(AddressOf TVUpdate)
            Me.Invoke(MyDelegate, New Object() {ratesVersion_id, ratesVersion_parent_id, ratesVersion_name, ratesVersion_image})
        Else
            If ratesVersion_parent_id = 0 Then
                VTreeViewUtil.AddNode(ratesVersion_id, ratesVersion_name, m_ratesVersionsTV, ratesVersion_image)
            Else
                Dim ratesVersionNode As vTreeNode = VTreeViewUtil.FindNode(m_ratesVersionsTV, ratesVersion_parent_id)
                If Not ratesVersionNode Is Nothing Then
                    VTreeViewUtil.AddNode(ratesVersion_id, ratesVersion_name, ratesVersionNode, ratesVersion_image)
                End If
            End If
            m_ratesVersionsTV.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef p_ratesVersionId As Int32)
    Friend Sub TVNodeDelete(ByRef p_ratesVersionId As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.Invoke(MyDelegate, New Object() {p_ratesVersionId})
        Else
            Dim ratesVersionNode As vTreeNode = VTreeViewUtil.FindNode(m_ratesVersionsTV, p_ratesVersionId)
            If Not ratesVersionNode Is Nothing Then
                ratesVersionNode.Remove()
                m_ratesVersionsTV.Refresh()
            End If
        End If

    End Sub


#End Region


#Region "Call Backs"

#Region "Versions Right Click Menu"

    Private Sub Select_version_Click(sender As Object, e As EventArgs) Handles select_version.Click, DisplayRatesToolStripMenuItem.Click

        If m_ratesVersionsTV.SelectedNode Is Nothing Then Exit Sub
        If m_controller.IsFolderVersion(m_ratesVersionsTV.SelectedNode.Value) = False Then
            ChangeRatesVersionDisplayRequest(m_ratesVersionsTV.SelectedNode.Value)
        End If

    End Sub

    Private Sub AddFolderRCM_Click(sender As Object, e As EventArgs) Handles AddFolderRCM.Click

        If Not m_ratesVersionsTV.SelectedNode Is Nothing Then
            If m_controller.IsFolderVersion(m_ratesVersionsTV.SelectedNode.Value) = False Then
                Dim name As String = InputBox("Please enter a name for the new Folder")
                If Len(name) < NAMES_MAX_LENGTH Then
                    m_controller.CreateVersion(m_ratesVersionsTV.SelectedNode.Value, name, 1, 0, 0)
                Else
                    MsgBox("The name cannot exceed " & NAMES_MAX_LENGTH & " characters")
                End If
            Else
                MsgBox("A folder can only be added to another folder")
            End If
        Else
            Dim name As String = InputBox("Please enter a name for the new Folder")
            If Len(name) < NAMES_MAX_LENGTH Then
                m_controller.CreateVersion(0, name, 1, 0, 0)
            Else
                MsgBox("The name cannot exceed 50 characters")
            End If
        End If

    End Sub

    Private Sub AddRatesVersionRCM_Click(sender As Object, e As EventArgs) Handles AddRatesVersionRCM.Click

        If Not m_ratesVersionsTV.SelectedNode Is Nothing Then
            If m_controller.IsFolderVersion(m_ratesVersionsTV.SelectedNode.Value) = True Then
                m_controller.ShowNewRatesVersion(m_ratesVersionsTV.SelectedNode.Value)
            Else
                MsgBox("A Version can only be added under a folder")
            End If
        Else
            m_controller.ShowNewRatesVersion(0)
        End If

    End Sub

    Private Sub DeleteVersionBT_Click(sender As Object, e As EventArgs) Handles DeleteVersionRCM.Click

        If Not m_ratesVersionsTV.SelectedNode Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the version " + Chr(13) + Chr(13) + _
                                                      m_ratesVersionsTV.SelectedNode.Text + Chr(13) + Chr(13) + _
                                                      "This version and all sub versions will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                      "Version deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                m_controller.DeleteRatesVersion(m_ratesVersionsTV.SelectedNode.Value)
            End If
        End If

    End Sub

#End Region

#Region "m_ratesDataGridView Right Click Menu"

    Private Sub expand_periods_Click(sender As Object, e As EventArgs)
        m_ratesDataGridView.RowsHierarchy.ExpandAllItems()
    End Sub

    Private Sub collapse_periods_Click(sender As Object, e As EventArgs)
        m_ratesDataGridView.RowsHierarchy.CollapseAllItems()
    End Sub

    Private Sub CopyRateDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyRateDownToolStripMenuItem.Click
        CopyRateValueDown()
    End Sub

    Private Sub ImportFromExcelToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ImportFromExcelToolStripMenuItem1.Click
        If m_ratesDataGridView.CellsArea.SelectedCells.Length > 0 Then
            m_controller.ImportRatesFromExcel(m_ratesDataGridView.CellsArea.SelectedCells(0).ColumnItem.ItemValue)
        Else
            m_controller.ImportRatesFromExcel()
        End If
    End Sub

#End Region

#End Region


#Region "Events"

    Private Sub RenameBT_Click(sender As Object, e As EventArgs) Handles RenameBT.Click

        If m_ratesVersionsTV.SelectedNode Is Nothing Then Exit Sub
        Dim name As String = InputBox("Enter new name", "Rename version")

        If name <> "" Then m_controller.UpdateVersionName(m_ratesVersionsTV.SelectedNode.Value, name)
        m_ratesVersionsTV.SelectedNode.Remove()
    End Sub

    Private Sub RatesDataGridView_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If m_isFillingCells = False Then
            If Not IsNumeric(args.NewValue) Then
                args.Cancel = True
            Else
                Dim destinationCurrency As Int32 = args.Cell.ColumnItem.ItemValue
                Dim period As String = args.Cell.RowItem.ItemValue
                m_controller.UpdateRate(destinationCurrency, period, args.NewValue)
            End If
        End If

    End Sub

    'Private Sub Rates_versionsTV_MouseClick(sender As Object, e As MouseEventArgs)

    '    If Not m_ratesVersionsTV.HitTest(e.Location) Is Nothing Then
    '        m_rightClickedNode = m_ratesVersionsTV.HitTest(e.Location)
    '    Else
    '        m_rightClickedNode = Nothing
    '    End If

    'End Sub

    Private Sub RatesVersionsTV_MouseDoubleClick(sender As Object, e As MouseEventArgs)

        If m_ratesVersionsTV.SelectedNode Is Nothing Then Exit Sub
        If m_controller.IsFolderVersion(m_ratesVersionsTV.SelectedNode.Value) = False Then
            ChangeRatesVersionDisplayRequest(m_ratesVersionsTV.SelectedNode.Value)
        End If

    End Sub

    Private Sub Rates_versionsTV_KeyPress(sender As Object, e As KeyPressEventArgs)

        If e.KeyChar = Chr(13) AndAlso Not m_ratesVersionsTV.SelectedNode Is Nothing _
        AndAlso m_ratesVersionsTV.SelectedNode.SelectedImageIndex = 0 Then
            ChangeRatesVersionDisplayRequest(m_ratesVersionsTV.SelectedNode.Value)
        End If
        '   If e.KeyChar = Chr(10) Then DeleteVersionBT_Click(sender, e)

    End Sub


#End Region


#Region "Exchange Rates m_ratesDataGridView"

    Friend Sub InitializeDGV(ByRef currenciesList As List(Of Int32), _
                             ByRef monthsIdList As List(Of Int32), _
                             ByRef p_ratesVersionid As Int32)

        m_currentRatesVersionId = p_ratesVersionid
        m_ratesDataGridView.RowsHierarchy.Clear()
        m_ratesDataGridView.ColumnsHierarchy.Clear()
        InitColumns(currenciesList)
        InitRows(monthsIdList)
        DataGridViewsUtil.DGVSetHiearchyFontSize(m_ratesDataGridView, My.Settings.tablesFontSize, My.Settings.tablesFontSize)
        m_ratesDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_ratesDataGridView)
        m_ratesDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = False
        m_ratesDataGridView.BackColor = Color.White
        m_ratesDataGridView.Refresh()

    End Sub

    Private Sub InitColumns(ByRef currenciesList As List(Of Int32))

        Dim mainCurrencyName As String = GlobalVariables.Currencies.currencies_hash(m_mainCurrency)(NAME_VARIABLE)
        For Each currencyId As UInt32 In currenciesList
            If currencyId <> m_mainCurrency Then
                Dim destinationCurrencyName As String = GlobalVariables.Currencies.currencies_hash(CInt(currencyId))(NAME_VARIABLE)
                Dim col As HierarchyItem = m_ratesDataGridView.ColumnsHierarchy.Items.Add(mainCurrencyName & "/" & destinationCurrencyName)
                col.ItemValue = currencyId
            End If
        Next

    End Sub

    Private Sub InitRows(ByRef p_monthsIdList As List(Of Int32))

        For Each monthId As Int32 In p_monthsIdList
            Dim period As Date = Date.FromOADate(monthId)
            Dim row As HierarchyItem = m_ratesDataGridView.RowsHierarchy.Items.Add(Format(period, "MMM yyyy"))
            row.ItemValue = monthId
            m_currencyTextBoxEditor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL
            If GlobalVariables.Users.CurrentUserIsAdmin() Then row.CellsEditor = m_currencyTextBoxEditor
        Next

    End Sub

    Friend Sub DisplayRatesVersionValues(ByRef p_exchangeRatesHahstable As Dictionary(Of Tuple(Of Int32, Int32, Int32), Double))

        m_isFillingCells = True
        Dim currencyId As Int32
        For Each column As HierarchyItem In m_ratesDataGridView.ColumnsHierarchy.Items
            currencyId = CInt(column.ItemValue)

            For Each row As HierarchyItem In m_ratesDataGridView.RowsHierarchy.Items
                Dim period As Int32 = CInt(row.ItemValue)

                Dim rateTuple As New Tuple(Of Int32, Int32, Int32)(currencyId, m_currentRatesVersionId, period)
                If p_exchangeRatesHahstable.ContainsKey(rateTuple) = True Then
                    m_ratesDataGridView.CellsArea.SetCellValue(row, column, p_exchangeRatesHahstable(rateTuple))
                Else
                    m_ratesDataGridView.CellsArea.SetCellValue(row, column, 0)
                End If

            Next
        Next
        m_isFillingCells = False
        m_ratesDataGridView.Refresh()

    End Sub

    Friend Sub UpdateCell(ByRef p_currencyId As Int32, _
                          ByRef p_period As Int32, _
                          ByRef p_value As Double)

        m_ratesDataGridView.CellsArea.SetCellValue(DataGridViewsUtil.GetHierarchyItemFromId(m_ratesDataGridView.RowsHierarchy, p_period), _
                                                   DataGridViewsUtil.GetHierarchyItemFromId(m_ratesDataGridView.ColumnsHierarchy, p_currencyId), _
                                                   p_value)

    End Sub

    Friend Sub CopyRateValueDown()

        Dim value As Double = m_ratesDataGridView.CellsArea.SelectedCells(0).Value
        Dim column As HierarchyItem = m_ratesDataGridView.CellsArea.SelectedCells(0).ColumnItem
        Dim row As HierarchyItem = m_ratesDataGridView.CellsArea.SelectedCells(0).RowItem
        m_isCopyingValueDown = True
        For i As Int32 = row.ItemIndex To m_ratesDataGridView.RowsHierarchy.Items.Count - 1
            m_ratesDataGridView.CellsArea.SetCellValue(m_ratesDataGridView.RowsHierarchy.Items(i), column, value)
        Next
        m_isCopyingValueDown = False

    End Sub

#Region "Chart Display"

    'Friend Sub DisplayPriceCurve(ByRef curr As String, _
    '                             ByRef values As Double(), _
    '                             ByRef color_index As Int32)

    '    Dim new_serie As New Series(curr)
    '    Chart.Series.Add(curr)
    '    new_serie.ChartArea = "ChartArea1"
    '    Chart.Series(curr).ChartType = SeriesChartType.Line
    '    Chart.Series(curr).Color = Color.FromArgb(colors_palette(color_index)(PPS_COLORS_RED_VAR), colors_palette(color_index)(PPS_COLORS_GREEN_VAR), colors_palette(color_index)(PPS_COLORS_BLUE_VAR))
    '    Chart.Series(curr).BorderWidth = LINES_WIDTH
    '    Chart.Series(curr).Points.DataBindXY(charts_periods, values)

    'End Sub

    'Private Sub UpdateSerie(ByRef curr As String, _
    '                        ByRef column_curr As Int32)

    '    Dim values(charts_periods.Count - 1) As Double
    '    Dim i As Int32 = 0
    '    For Each row As HierarchyItem In m_ratesDataGridView.RowsHierarchy.Items
    '        For Each sub_row As HierarchyItem In row.Items
    '            values(i) = m_ratesDataGridView.CellsArea.GetCellValue(sub_row, m_ratesDataGridView.ColumnsHierarchy.Items(column_curr))
    '            i = i + 1
    '        Next
    '    Next
    '    Chart.Series(curr).Points.DataBindXY(charts_periods, values)
    '    Chart.Update()

    'End Sub

#End Region

#End Region


End Class
