' LogView;VB
'
' Displays the log information
'
'
' Author: Julien Monnereau
' Last modified: 10/10/2015


Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView


Friend Class LogView

#Region "Instance Variables"

    Private m_userColumn As HierarchyItem
    Private m_dateColumn As HierarchyItem
    Private m_clientColumn As HierarchyItem
    Private m_productColumn As HierarchyItem
    Private m_adjustmentColumn As HierarchyItem
    Private m_valueColumn As HierarchyItem

#End Region



    Friend Sub New(ByRef p_entityName As String, _
                   ByRef p_accountName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_entityTextBox.Text = p_entityName
        m_accountTextBox.Text = p_accountName
        InitializeLogDataGridView()
     
    End Sub

    Private Sub InitializeLogDataGridView()

        m_logDataGridView.RowsHierarchy.Visible = False
        m_userColumn = m_logDataGridView.ColumnsHierarchy.Items.Add("User")
        m_dateColumn = m_logDataGridView.ColumnsHierarchy.Items.Add("Date")
        m_clientColumn = m_logDataGridView.ColumnsHierarchy.Items.Add("Client")
        m_productColumn = m_logDataGridView.ColumnsHierarchy.Items.Add("Product")
        m_adjustmentColumn = m_logDataGridView.ColumnsHierarchy.Items.Add("Adjustment")
        m_valueColumn = m_logDataGridView.ColumnsHierarchy.Items.Add("Value")
        ' m_logDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        m_logDataGridView.ColumnsHierarchy.AutoStretchColumns = True

    End Sub

    Friend Sub DisplayLogValues(ByRef p_logHashtable As List(Of Hashtable))

        ' gestion du cas où des values  0 sont affichées

        For Each logHashtable As Hashtable In p_logHashtable
            Dim r As HierarchyItem = m_logDataGridView.RowsHierarchy.Items.Add("")
            m_logDataGridView.CellsArea.SetCellValue(r, m_userColumn, logHashtable(FACTLOG_USER_VARIABLE))
            m_logDataGridView.CellsArea.SetCellValue(r, m_dateColumn, Format(Date.FromOADate(logHashtable(FACTLOG_DATE_VARIABLE)), "mm yyyy"))
            m_logDataGridView.CellsArea.SetCellValue(r, m_clientColumn, logHashtable(FACTLOG_CLIENT_ID_VARIABLE)) 'GlobalVariables.Clients.Axis_hash(logHashtable(FACTLOG_CLIENT_ID_VARIABLE))(NAME_VARIABLE))
            m_logDataGridView.CellsArea.SetCellValue(r, m_productColumn, logHashtable(FACTLOG_PRODUCT_ID_VARIABLE)) ' GlobalVariables.Products.Axis_hash(logHashtable(FACTLOG_PRODUCT_ID_VARIABLE))(NAME_VARIABLE))
            m_logDataGridView.CellsArea.SetCellValue(r, m_adjustmentColumn, logHashtable(FACTLOG_ADJUSTMENT_ID_VARIABLE)) ' GlobalVariables.Adjustments.Axis_hash(logHashtable(FACTLOG_ADJUSTMENT_ID_VARIABLE))(NAME_VARIABLE))
            m_logDataGridView.CellsArea.SetCellValue(r, m_valueColumn, logHashtable(FACTLOG_VALUE_VARIABLE))
        Next
        Me.Show()

    End Sub

End Class