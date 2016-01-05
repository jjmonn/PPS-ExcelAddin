Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls

' UnReferencedClientsUI.vb
'
'
' Created by: Julien Monnereau
' Last modified: 17/12/2015

Public Class UnReferencedClientsUI

#Region "Instance variables"

    Private m_reportUploadController As ReportUploadControler
    Private m_axisController As New AxisController(CRUD.AxisType.Client)
    Private m_clientsDataGrid As New vDataGridView

    Private m_clientsNamesList As List(Of String)
    Private m_isUpdatingDGV As Boolean
    Private m_similarClientsColumn As HierarchyItem
    Private m_replaceOptionColumn As HierarchyItem
    Private m_createColumn As HierarchyItem
    Private m_replaceCheckEditorsItemsDict As New SafeDictionary(Of CheckBoxEditor, HierarchyItem)
    Private m_createCheckEditorsItemsDict As New SafeDictionary(Of CheckBoxEditor, HierarchyItem)

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_reportUploadController As ReportUploadControler, _
                   ByRef p_clientsNameList As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_reportUploadController = p_reportUploadController
        m_clientsNamesList = p_clientsNameList
        m_clientsDGVPanel.Controls.Add(m_clientsDataGrid)
        m_clientsDataGrid.Dock = Windows.Forms.DockStyle.Fill

        MultilanguageSetup()
        InitializeClientsDataGridColumns()
        InitializeClientsDataGridRows()
        InitializeDGVFormat()

        AddHandler m_axisController.AxisCreated, AddressOf AfterClientCreation
     
    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("clientAutoCreation.unerefenced_clients")
        m_createAllButton.Text = Local.GetValue("general.validate")

    End Sub

    Private Sub InitializeClientsDataGridColumns()

        m_similarClientsColumn = m_clientsDataGrid.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.similar_clients_column_name"))
        m_replaceOptionColumn = m_clientsDataGrid.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.replace_column_name"))
        m_createColumn = m_clientsDataGrid.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.create_column_name"))

    End Sub

    Private Sub InitializeClientsDataGridRows()

        m_isUpdatingDGV = True
        For Each l_clientName In m_clientsNamesList
            Dim l_row As HierarchyItem = m_clientsDataGrid.RowsHierarchy.Items.Add(l_clientName)
            Dim l_replaceOptionCheckBox As New CheckBoxEditor
            m_clientsDataGrid.CellsArea.SetCellEditor(l_row, m_replaceOptionColumn, l_replaceOptionCheckBox)
            m_replaceCheckEditorsItemsDict.Add(l_replaceOptionCheckBox, l_row)
            AddHandler l_replaceOptionCheckBox.CheckedChanged, AddressOf ReplaceCheckBoxChanged

            Dim l_createOptionCheckBox As New CheckBoxEditor
            m_clientsDataGrid.CellsArea.SetCellEditor(l_row, m_createColumn, l_createOptionCheckBox)
            m_createCheckEditorsItemsDict.Add(l_createOptionCheckBox, l_row)
            AddHandler l_createOptionCheckBox.CheckedChanged, AddressOf CreateCheckBoxChanged
        Next
        m_isUpdatingDGV = False

    End Sub

    Private Sub InitializeDGVFormat()

        m_clientsDataGrid.ContextMenuStrip = m_DGVContextMenuStrip
        m_clientsDataGrid.BackColor = System.Drawing.SystemColors.Control
        m_clientsDataGrid.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        '  m_clientsDataGrid.ColumnsHierarchy.AutoStretchColumns = True
        m_clientsDataGrid.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER

    End Sub

#End Region

#Region "Call backs"

    Private Sub m_createAllButton_Click(sender As Object, e As EventArgs) Handles m_createAllButton.Click

        Dim l_clientsToBeCreated As New List(Of String)
        For Each l_row In m_clientsDataGrid.RowsHierarchy.Items
            Dim l_mustBeCreated As Boolean = CType(m_clientsDataGrid.CellsArea.GetCellValue(l_row, m_createColumn), Boolean)
            If l_mustBeCreated = True Then l_clientsToBeCreated.Add(l_row.Caption)
        Next

        For Each l_clientName As String In l_clientsToBeCreated
            m_axisController.CreateAxisElem(l_clientName)
        Next

    End Sub

    Private Sub UnselectBothOptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectBothOptionsToolStripMenuItem.Click

        If m_clientsDataGrid.CellsArea.SelectedCellsCount > 0 Then
            Dim l_row = m_clientsDataGrid.CellsArea.SelectedCells(0).RowItem
            If l_row IsNot Nothing Then
                m_isUpdatingDGV = True
                m_clientsDataGrid.CellsArea.SetCellValue(l_row, m_createColumn, False)
                m_clientsDataGrid.CellsArea.SetCellValue(l_row, m_replaceOptionColumn, False)
                m_isUpdatingDGV = False
            End If
        End If
    End Sub

    Private Sub SelectAllOnColumnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllOnColumnToolStripMenuItem.Click
        m_isUpdatingDGV = True

        If m_clientsDataGrid.CellsArea.SelectedCellsCount > 0 Then
            Dim l_column = m_clientsDataGrid.CellsArea.SelectedCells(0).ColumnItem
            If l_column IsNot Nothing Then
                For Each l_row In m_clientsDataGrid.RowsHierarchy.Items
                    m_clientsDataGrid.CellsArea.SetCellValue(l_row, l_column, True)
                Next
            End If
        End If
        m_isUpdatingDGV = False
    End Sub

    Private Sub UnselectAllOnColumnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectAllOnColumnToolStripMenuItem.Click
        m_isUpdatingDGV = True
        If m_clientsDataGrid.CellsArea.SelectedCellsCount > 0 Then
            Dim l_column = m_clientsDataGrid.CellsArea.SelectedCells(0).ColumnItem
            If l_column IsNot Nothing Then
                For Each l_row In m_clientsDataGrid.RowsHierarchy.Items
                    m_clientsDataGrid.CellsArea.SetCellValue(l_row, l_column, False)
                Next
            End If
        End If
        m_isUpdatingDGV = False
    End Sub

#End Region

#Region "Events"

    Private Sub CreateCheckBoxChanged(sender As Object, e As EventArgs)

        If m_isUpdatingDGV = False Then
            m_isUpdatingDGV = True
            Dim l_checkBox As CheckBoxEditor = CType(sender, CheckBoxEditor)
            Dim l_row = m_createCheckEditorsItemsDict(l_checkBox)
            If l_row IsNot Nothing Then
                Dim l_value As Boolean = l_checkBox.EditorValue
                m_clientsDataGrid.CellsArea.SetCellValue(l_row, m_replaceOptionColumn, l_value = False)
            End If
            m_isUpdatingDGV = False
        End If

    End Sub

    Private Sub ReplaceCheckBoxChanged(sender As Object, e As EventArgs)

        If m_isUpdatingDGV = False Then
            m_isUpdatingDGV = True
            Dim l_checkBox As CheckBoxEditor = CType(sender, CheckBoxEditor)
            Dim l_row = m_replaceCheckEditorsItemsDict(l_checkBox)
            If l_row IsNot Nothing Then
                Dim l_value As Boolean = l_checkBox.EditorValue
                m_clientsDataGrid.CellsArea.SetCellValue(l_row, m_createColumn, l_value = False)
            End If
            m_isUpdatingDGV = False
        End If


    End Sub

    Private Sub AfterClientCreation(ByRef p_status As Boolean, ByRef p_clientId As Int32)

        If p_status = ErrorMessage.SUCCESS Then
            Dim l_client As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Client, p_clientId)
            If l_client Is Nothing Then Exit Sub
            Dim l_row As HierarchyItem = Nothing
            For Each row In m_clientsDataGrid.RowsHierarchy.Items
                If row.Caption = l_client.Name Then l_row = row
            Next
            If Not l_row Is Nothing Then l_row.Delete()
        End If

        ' Resume submission if everything has been treated
        If m_clientsDataGrid.RowsHierarchy.Items.Count = 0 Then
            m_reportUploadController.DataSubmission()
        End If

    End Sub

#End Region

   
End Class