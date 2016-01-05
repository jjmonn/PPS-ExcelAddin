' SubmissionsFollowUpView
'
' Allows users to update and consult the status of submission for entities each week
'
'
' Created by: Julien Monnereau
' Created on: 04/01/2016
' Last modified: 04/01/2016


Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.Utilities
Imports System.Windows.Forms


Public Class SubmissionsFollowUpView

#Region "Instance variables"

    Private m_commitManager As New CommitManager
    Private m_periods As New List(Of Int32)
    Private m_isEditingDGV As Boolean
    Private m_entitiesTreeview As New vTreeView
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER

#End Region

#Region "Initialization"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_commitManager.Initialize()
        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTreeview)
        MultilanguageSetup()
        PeriodsRangeSetup()
        DGVRowsInitialize()
        DGVColumnsInitialize()
        FormatsInitialize()
        FillDGV()

        ' m_submissionsDGV.ContextMenuStrip = m_cellsRightClickMenu

        AddHandler m_endDate.ValueChanged, AddressOf m_endDate_ValueChanged
        AddHandler m_startDate.ValueChanged, AddressOf m_startDate_ValueChanged
        AddHandler m_submissionsDGV.HierarchyItemMouseClick, AddressOf DataGridViewRightClick

    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("submissionsFollowUp.submissions_tracking")
        Me.m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date")
        Me.m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date")

    End Sub

    Private Sub PeriodsRangeSetup()

        Dim l_todaysDate As Date = Today.Date
        m_startDate.Text = l_todaysDate.AddDays(-7)
        m_endDate.Text = l_todaysDate.AddDays(7 * 2)
        DefinePeriods(m_startDate.Text, m_endDate.Text)

    End Sub

    Private Function GetStatusComboBox() As ComboBoxEditor

        Dim l_statusCombobox As New ComboBoxEditor
        l_statusCombobox.DropDownList = True

        Dim l_redStatus As New ListItem
        l_redStatus.Text = Local.GetValue("submissionsFollowUp.red_status")
        l_redStatus.Value = CRUD.Commit.Status.NOT_EDITED
        l_statusCombobox.Items.Add(l_redStatus)

        Dim l_orangeStatus As New ListItem
        l_orangeStatus.Text = Local.GetValue("submissionsFollowUp.orange_status")
        l_orangeStatus.Value = CRUD.Commit.Status.EDITED
        l_statusCombobox.Items.Add(l_orangeStatus)

        Dim l_greenStatus As New ListItem
        l_greenStatus.Text = Local.GetValue("submissionsFollowUp.green_status")
        l_greenStatus.Value = CRUD.Commit.Status.COMMITTED
        l_statusCombobox.Items.Add(l_greenStatus)

        Return l_statusCombobox

    End Function

    Private Sub FormatsInitialize()

        m_submissionsDGV.VIBlendTheme = DGV_VI_BLEND_STYLE
        m_submissionsDGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        m_submissionsDGV.BackColor = System.Drawing.SystemColors.Control

        m_startDate.FormatValue = "MM-dd-yy"
        m_endDate.FormatValue = "MM-dd-yy"
        '   m_startDate.DateTimeEditor.DefaultDateTimeFormat = VIBlend.WinForms.Controls.DefaultDateTimePatterns.ShortDatePattern
        '   m_endDate.DateTimeEditor.DefaultDateTimeFormat = VIBlend.WinForms.Controls.DefaultDateTimePatterns.ShortDatePattern

    End Sub

#End Region

#Region "DGV"

    Private Sub DGVRowsInitialize()

        DataGridViewsUtil.DGVRowsInitialize(m_submissionsDGV, m_entitiesTreeview)
        Dim l_statusCombobox = GetStatusComboBox()

        For Each l_row As HierarchyItem In m_submissionsDGV.RowsHierarchy.Items
            RowInitialize(l_row, l_statusCombobox)
        Next
        m_submissionsDGV.RowsHierarchy.ExpandAllItems()
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_submissionsDGV)

    End Sub

    Private Sub RowInitialize(ByRef p_row As HierarchyItem, ByRef p_statusCombobox As ComboBoxEditor)

        Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, p_row.ItemValue)
        If l_entity IsNot Nothing Then
            If l_entity.AllowEdition = True Then
                p_row.CellsEditor = p_statusCombobox
            End If
        End If

        For Each l_subRow In p_row.Items
            RowInitialize(l_subRow, p_statusCombobox)
        Next

    End Sub

    Private Sub DGVColumnsInitialize()

        m_submissionsDGV.ColumnsHierarchy.Clear()
        For Each l_period As Int32 In m_periods
            Dim l_periodCaption As String = "Week " & Period.GetWeekNumberFromDateId(l_period) & ", " & Year(Date.FromOADate(l_period))
            Dim l_column = m_submissionsDGV.ColumnsHierarchy.Items.Add(l_periodCaption)
            l_column.ItemValue = l_period
        Next

    End Sub

    Private Sub FillDGV()

        m_isEditingDGV = True
        For Each l_row As HierarchyItem In m_submissionsDGV.RowsHierarchy.Items
            FillRow(l_row)
        Next
        m_isEditingDGV = False
        m_submissionsDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

    End Sub

    Private Sub FillRow(ByRef p_row As HierarchyItem)

        Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, p_row.ItemValue)
        If l_entity IsNot Nothing Then

            If l_entity.AllowEdition = True Then
                Dim l_submissions = m_commitManager.GetDictionary(l_entity.Id)
                If l_submissions IsNot Nothing Then

                    For Each l_column As HierarchyItem In m_submissionsDGV.ColumnsHierarchy.Items
                        Dim l_period As UInt32 = l_column.ItemValue
                        If l_submissions.ContainsSecondaryKey(l_period) Then
                            Dim l_text As String = ""
                            Select Case l_submissions.SecondaryKeyItem(l_period).Value
                                Case CRUD.Commit.Status.NOT_EDITED : l_text = Local.GetValue("submissionsFollowUp.red_status")
                                Case CRUD.Commit.Status.EDITED : l_text = Local.GetValue("submissionsFollowUp.orange_status")
                                Case CRUD.Commit.Status.COMMITTED : l_text = Local.GetValue("submissionsFollowUp.green_status")
                            End Select
                            m_submissionsDGV.CellsArea.SetCellValue(p_row, l_column, l_text)
                        Else
                            m_submissionsDGV.CellsArea.SetCellValue(p_row, l_column, Local.GetValue("submissionsFollowUp.red_status"))
                        End If
                    Next
                Else
                    For Each l_column As HierarchyItem In m_submissionsDGV.ColumnsHierarchy.Items
                        m_submissionsDGV.CellsArea.SetCellValue(p_row, l_column, Local.GetValue("submissionsFollowUp.red_status"))
                    Next
                End If
                ' End if l_submission isnot nothing
            End If
            ' End if l_entity is allowed edition
        End If

        For Each l_subRow In p_row.Items
            FillRow(l_subRow)
        Next

    End Sub

#End Region

#Region "Events"

#Region "Date time pickers events"

    Private Sub m_endDate_ValueChanged(sender As Object, e As EventArgs)
        UpdateFollowingPeriodChange()
    End Sub

    Private Sub m_startDate_ValueChanged(sender As Object, e As EventArgs)
        UpdateFollowingPeriodChange()
    End Sub

    Private Sub UpdateFollowingPeriodChange()

        DefinePeriods(m_startDate.DateTimeEditor.Value, m_endDate.DateTimeEditor.Value)
        DGVColumnsInitialize()
        FillDGV()
        m_submissionsDGV.Refresh()

    End Sub

#End Region

#Region "DGV Events"

    Private Sub m_submissionsDGV_CellValueChanged(sender As Object, args As VIBlend.WinForms.DataGridView.CellEventArgs) Handles m_submissionsDGV.CellValueChanged

        Dim l_value As Byte
        Dim l_color As System.Drawing.Color
        Dim l_textColor As System.Drawing.Color
        Select Case args.Cell.Value
            Case Local.GetValue("submissionsFollowUp.red_status")
                l_value = CRUD.Commit.Status.NOT_EDITED
                l_color = Drawing.Color.DarkRed
                l_textColor = Drawing.Color.White
            Case Local.GetValue("submissionsFollowUp.orange_status")
                l_value = CRUD.Commit.Status.EDITED
                l_color = Drawing.Color.Orange
                l_textColor = Drawing.Color.Black
            Case Local.GetValue("submissionsFollowUp.green_status")
                l_value = CRUD.Commit.Status.COMMITTED
                l_color = Drawing.Color.Green
                l_textColor = Drawing.Color.White
        End Select

        If m_isEditingDGV = False Then
            m_commitManager.UpdateCommitStatus(args.Cell.RowItem.ItemValue, args.Cell.ColumnItem.ItemValue, l_value)
        End If

        If args.Cell.RowItem.ParentItem IsNot Nothing Then
            UpdateConsoEntity(args.Cell.RowItem.ParentItem, args.Cell.ColumnItem)
        End If

        Dim l_fs As FillStyle = New FillStyleSolid(l_color)
        Dim l_cs As GridCellStyle = GridTheme.GetDefaultTheme(m_submissionsDGV.VIBlendTheme).GridCellStyle

        l_cs.TextColor = l_textColor
        l_cs.FillStyle = l_fs
        args.Cell.DrawStyle = l_cs

    End Sub

    ' Recursively update entities above
    Private Sub UpdateConsoEntity(ByRef p_row As HierarchyItem, ByVal p_column As HierarchyItem)

        Dim l_editionflag = m_isEditingDGV
        m_isEditingDGV = True
        Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, p_row.ItemValue)
        If l_entity Is Nothing Then Exit Sub

        If l_entity.AllowEdition = False Then
            Dim l_status As Int32 = 0
            For Each l_subRows As HierarchyItem In p_row.Items
                Select Case m_submissionsDGV.CellsArea.GetCellValue(l_subRows, p_column)
                    Case Local.GetValue("submissionsFollowUp.red_status") : l_status += CRUD.Commit.Status.NOT_EDITED
                    Case Local.GetValue("submissionsFollowUp.orange_status") : l_status += CRUD.Commit.Status.EDITED
                    Case Local.GetValue("submissionsFollowUp.green_status") : l_status += CRUD.Commit.Status.COMMITTED
                End Select
            Next

            If l_status = p_row.Items.Count * CRUD.Commit.Status.COMMITTED Then
                m_submissionsDGV.CellsArea.SetCellValue(p_row, p_column, Local.GetValue("submissionsFollowUp.red_status"))
            ElseIf l_status > p_row.Items.Count * CRUD.Commit.Status.NOT_EDITED Then
                m_submissionsDGV.CellsArea.SetCellValue(p_row, p_column, Local.GetValue("submissionsFollowUp.orange_status"))
            Else
                m_submissionsDGV.CellsArea.SetCellValue(p_row, p_column, Local.GetValue("submissionsFollowUp.red_status"))
            End If
        End If

        If p_row.ParentItem IsNot Nothing Then
            UpdateConsoEntity(p_row.ParentItem, p_column)
        End If
        m_isEditingDGV = l_editionflag

    End Sub

    Private Sub DataGridViewRightClick(sender As Object, e As MouseEventArgs)

        If (e.Button <> MouseButtons.Right) Then Exit Sub
        m_hierarchyRightClickMenu.Visible = True
        m_hierarchyRightClickMenu.Bounds = New Drawing.Rectangle(MousePosition, New Drawing.Size(m_hierarchyRightClickMenu.Width, m_hierarchyRightClickMenu.Height))

    End Sub

#End Region

#End Region

#Region "Call backs"

    Private Sub ExpandAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpandAllToolStripMenuItem.Click
        m_submissionsDGV.RowsHierarchy.ExpandAllItems()
    End Sub

    Private Sub CollapseAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollapseAllToolStripMenuItem.Click
        m_submissionsDGV.RowsHierarchy.CollapseAllItems()
    End Sub

#End Region

#Region "Utilities"

    Private Sub DefinePeriods(ByVal p_startDate As Date, ByVal p_endDate As Date)

        m_periods.Clear()
        While p_startDate < p_endDate
            m_periods.Add(Period.GetWeekIdFromPeriodId(p_startDate.ToOADate))
            p_startDate = p_startDate.AddDays(7)
        End While

    End Sub

#End Region


End Class