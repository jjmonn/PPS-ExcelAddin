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
    Private m_axisController As New AxisController(GlobalVariables.AxisElems, GlobalVariables.AxisFilters, CRUD.AxisType.Client)
    Private m_clientsDataGrid As New vDataGridView

    Private m_clientsNamesList As List(Of String)

    Private m_similarClientsColumn As HierarchyItem
    Private m_replaceOptionColumn As HierarchyItem
    Private m_createColumn As HierarchyItem
    Private m_createButtonRowItemDict As New SafeDictionary(Of vButton, HierarchyItem)

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

        AddHandler m_axisController.AxisCreated, AddressOf AfterClientCreation

    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("clientAutoCreation.unerefenced_clients")
        m_createAllButton.Text = Local.GetValue("clientAutoCreation.create_all")
        m_replaceSelectionButton.Text = Local.GetValue("clientAutoCreation.replace_selection")


    End Sub

    Private Sub InitializeClientsDataGridColumns()

        m_createColumn = m_clientsDataGrid.ColumnsHierarchy.Items.Add(Local.GetValue(""))
        m_similarClientsColumn = m_clientsDataGrid.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.similar_clients_column_name"))
        m_replaceOptionColumn = m_clientsDataGrid.ColumnsHierarchy.Items.Add(Local.GetValue("clientAutoCreation.Replace"))

    End Sub

    Private Sub InitializeClientsDataGridRows()

        For Each l_clientName In m_clientsNamesList
            Dim l_row As HierarchyItem = m_clientsDataGrid.RowsHierarchy.Items.Add(l_clientName)
            Dim l_replaceOptionCheckBox As New CheckBoxEditor
            m_clientsDataGrid.CellsArea.SetCellEditor(l_row, m_replaceOptionColumn, l_replaceOptionCheckBox)

            Dim l_createButton As New vButton
            l_createButton.Text = Local.GetValue("general.create")
            m_clientsDataGrid.CellsArea.SetCellEditor(l_row, m_createColumn, l_createButton)
            m_createButtonRowItemDict.Add(l_createButton, l_row)
            AddHandler l_createButton.Click, AddressOf ClientCreationButton_OnClick
        Next

    End Sub

#End Region

#Region "Call backs"

    Private Sub m_createAllButton_Click(sender As Object, e As EventArgs) Handles m_createAllButton.Click

        Dim l_clientsToBeCreated As New List(Of String)
        For Each l_row In m_clientsDataGrid.RowsHierarchy.Items
            Dim l_mustBeReplaced As Boolean = CType(m_clientsDataGrid.CellsArea.GetCellValue(l_row, m_replaceOptionColumn), Boolean)
            If l_mustBeReplaced = False Then l_clientsToBeCreated.Add(l_row.Caption)
        Next

        For Each l_clientName As String In l_clientsToBeCreated
            m_axisController.CreateAxis(l_clientName)
        Next
        
    End Sub

    Private Sub m_replaceSelectionButton_Click(sender As Object, e As EventArgs) Handles m_replaceSelectionButton.Click


    End Sub

    Private Sub ClientCreationButton_OnClick(sender As Object, e As EventArgs)

        Dim l_createButton As vButton = CType(sender, vButton)
        Dim l_row As HierarchyItem = m_createButtonRowItemDict(l_createButton)
        m_axisController.CreateAxis(l_row.Caption)

    End Sub

    Private Sub m_resumeDataSubmissionButton_Click(sender As Object, e As EventArgs) Handles m_resumeDataSubmissionButton.Click

        If m_clientsDataGrid.RowsHierarchy.Items.Count = 0 Then
            m_reportUploadController.DataSubmission()
        Else
            MsgBox(Local.GetValue("clientAutoCreation.msg_all_clients_must_be_treated"))
        End If

    End Sub

#End Region


#Region "Events"

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

    End Sub


#End Region


   
End Class