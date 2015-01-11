Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic



Public Class CubeView


    Friend vDGV As New vDataGridView


    Public Sub New(ByRef columns As List(Of Integer), _
                   main_rows As List(Of String), _
                   subrows() As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Controls.Add(vDGV)
        vDGV.Dock = Windows.Forms.DockStyle.Fill
        DataGridViewsUtil.InitDisplayVDataGridView(vDGV, VIBlend.Utilities.VIBLEND_THEME.OFFICE2010BLACK)
        
        For Each column In columns
            vDGV.ColumnsHierarchy.Items.Add(column)
        Next

        For Each row In main_rows
            Dim item As HierarchyItem = vDGV.RowsHierarchy.Items.Add(row)
            For Each subRow In subrows
                item.Items.Add(subRow)
            Next
        Next


    End Sub

    Friend Sub fill_in(ByRef data_dic As Dictionary(Of String, Double()))

        Dim row_index As Int32 = 0
        For Each key In data_dic.Keys
            Dim i As Int32 = 0
            Dim row As HierarchyItem = vDGV.RowsHierarchy.Items(row_index)
            For Each sub_row As HierarchyItem In row.Items
                For Each column As HierarchyItem In vDGV.ColumnsHierarchy.Items
                    vDGV.CellsArea.SetCellValue(sub_row, column, data_dic(key)(i))
                    i = i + 1
                Next
            Next
            row_index = row_index + 1
        Next
        vDGV.Select()
        vDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        vDGV.ColumnsHierarchy.AutoResize()


    End Sub





End Class