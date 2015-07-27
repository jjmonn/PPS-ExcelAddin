' DataHistoryUI.vb
'
' Display Data History in a DGV
'
'
' Author: Julien Monnereau
' Last modified: 08/01/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections


Friend Class DataHistoryUI


#Region "Instance Variables"

    ' Objects
    Private DGV As New vDataGridView

    ' Constants
    Friend Const DGV_FONT_SIZE As Single = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
    Private Const VALUES_FORMAT_STRING As String = "{0:C0}"

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef data_history_list As List(Of Hashtable), _
                             ByRef client_id_name_dict As Hashtable, _
                             ByRef product_id_name_dict As Hashtable, _
                             ByRef adjustment_id_name_dict As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.        
        InitializeDGV()

        If Not data_history_list Is Nothing Then
            data_history_list.Reverse()
            For Each ht In data_history_list
                Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add("")
                DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), ht(LOG_TIME_VARIABLE))
                DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(1), ht(LOG_USER_ID_VARIABLE))
                DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(2), client_id_name_dict(ht(LOG_CLIENT_ID_VARIABLE)))
                DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(3), product_id_name_dict(ht(LOG_PRODUCT_ID_VARIABLE)))
                DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(4), adjustment_id_name_dict(ht(LOG_ADJUSTMENT_ID_VARIABLE)))
                DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(5), ht(LOG_VALUE_VARIABLE))
            Next
            DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            DGV.ColumnsHierarchy.AutoStretchColumns = True
        End If

    End Sub

    Private Sub InitializeDGV()

        DGV.RowsHierarchy.Visible = False
        DGV.ColumnsHierarchy.Items.Add("Date")
        DGV.ColumnsHierarchy.Items.Add("User")
        DGV.ColumnsHierarchy.Items.Add("Client")
        DGV.ColumnsHierarchy.Items.Add("Product")
        DGV.ColumnsHierarchy.Items.Add("Adjustment")
        DGV.ColumnsHierarchy.Items.Add("Value")
        DGV.ColumnsHierarchy.Items(2).CellsFormatString = VALUES_FORMAT_STRING
        DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_FONT_SIZE, DGV_FONT_SIZE)
        Me.Controls.Add(DGV)
        DGV.Dock = Windows.Forms.DockStyle.Fill

    End Sub

   

#End Region


   
End Class