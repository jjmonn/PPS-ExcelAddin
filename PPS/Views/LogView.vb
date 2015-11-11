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
    Private m_cancelExitFlag As Boolean

#End Region


    Friend Sub New(ByRef p_cancelOnExitFlag As Boolean, _
                   Optional ByRef p_entityName As String = "", _
                   Optional ByRef p_accountName As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_cancelExitFlag = p_cancelOnExitFlag
        If p_entityName <> "" Then m_entityTextBox.Text = p_entityName
        If p_accountName <> "" Then m_accountTextBox.Text = p_accountName
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


#Region "Interface"

    Delegate Sub SetEntityAndAccountNames_Delegate(ByRef p_entityName As String, _
                                                   ByRef p_accountName As String)
    Friend Sub SetEntityAndAccountNames(ByRef p_entityName As String, _
                                        ByRef p_accountName As String)

        If InvokeRequired Then
            Dim MyDelegate As New SetEntityAndAccountNames_Delegate(AddressOf SetEntityAndAccountNames)
            Me.Invoke(MyDelegate, New Object() {p_entityName, p_accountName})
        Else
            m_entityTextBox.Text = p_entityName
            m_accountTextBox.Text = p_accountName
        End If

    End Sub

    Delegate Sub DisplayLogAttemp_Delegate(p_logValuesHt As List(Of Hashtable))
    Friend Sub DisplayLogValues(p_logValuesHt As List(Of Hashtable))

        If InvokeRequired Then
            Dim MyDelegate As New DisplayLogAttemp_Delegate(AddressOf DisplayLogValues)
            Me.Invoke(MyDelegate, New Object() {p_logValuesHt})
        Else
            ' gestion du cas où des values  0 sont affichées - priority normal
            m_logDataGridView.RowsHierarchy.Clear()
            For Each logHashtable As Hashtable In p_logValuesHt
                Dim r As HierarchyItem = m_logDataGridView.RowsHierarchy.Items.Add("")
                m_logDataGridView.CellsArea.SetCellValue(r, m_userColumn, logHashtable(FACTLOG_USER_VARIABLE))
                m_logDataGridView.CellsArea.SetCellValue(r, m_dateColumn, logHashtable(FACTLOG_DATE_VARIABLE))
                m_logDataGridView.CellsArea.SetCellValue(r, m_clientColumn, logHashtable(FACTLOG_CLIENT_ID_VARIABLE)) 'GlobalVariables.Clients.Axis_hash(logHashtable(FACTLOG_CLIENT_ID_VARIABLE))(NAME_VARIABLE))
                m_logDataGridView.CellsArea.SetCellValue(r, m_productColumn, logHashtable(FACTLOG_PRODUCT_ID_VARIABLE)) ' GlobalVariables.Products.Axis_hash(logHashtable(FACTLOG_PRODUCT_ID_VARIABLE))(NAME_VARIABLE))
                m_logDataGridView.CellsArea.SetCellValue(r, m_adjustmentColumn, logHashtable(FACTLOG_ADJUSTMENT_ID_VARIABLE)) ' GlobalVariables.Adjustments.Axis_hash(logHashtable(FACTLOG_ADJUSTMENT_ID_VARIABLE))(NAME_VARIABLE))
                m_logDataGridView.CellsArea.SetCellValue(r, m_valueColumn, logHashtable(FACTLOG_VALUE_VARIABLE))
            Next
            If m_cancelExitFlag = False Then Me.Show()
        End If

    End Sub

#End Region


    Private Sub LogView_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If m_cancelExitFlag = True Then
            e.Cancel = True
            Me.Hide()
        End If
    End Sub

End Class