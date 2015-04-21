' ProductsControl.vb
'
' 
'
'
'
' Author: Julien Monnereau
' Last modified: 20/04/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Collections.Generic


Friend Class ProductsControl


#Region "Instance variables"

    ' Objects
    Private productsDGV As AnalysisAxisDGV
    Private controller As ProductsController

    ' Variables
    Friend categoriesNameKeyDic As Hashtable

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_controller As ProductsController, _
                             ByRef categoriesTV As Windows.Forms.TreeView, _
                             ByRef values_dict As Dictionary(Of String, Hashtable), _
                             ByRef input_categoriesNameKeyDic As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        controller = input_controller
        productsDGV = New AnalysisAxisDGV(categoriesTV, values_dict)
        Me.Controls.Add(productsDGV.DGV)
        productsDGV.DGV.Dock = Windows.Forms.DockStyle.Fill

        AddHandler productsDGV.DGV.CellValueChanged, AddressOf dataGridView_CellValueChanged
        
    End Sub

    Protected Friend Sub closeControl()

       
    End Sub

#End Region


#Region "Events"

    Private Sub dataGridView_CellValueChanged(sender As Object, args As CellEventArgs)

        If productsDGV.isFillingDGV = False Then
            Dim value As Object = categoriesNameKeyDic(args.Cell.Value)
            controller.updateValue(args.Cell.RowItem.Caption, _
                                   productsDGV.columnsCaptionIDDict(args.Cell.ColumnItem.Caption), _
                                   value)
        End If

    End Sub



#End Region




End Class
