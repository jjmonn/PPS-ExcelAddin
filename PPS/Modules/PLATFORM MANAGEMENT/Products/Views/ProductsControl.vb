' ProductsControl.vb
'
' Drop to excel !!
'
'
'
' Author: Julien Monnereau
' Last modified: 22/04/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class ProductsControl


#Region "Instance variables"

    ' Objects
    Private productsDGV As AnalysisAxisDGV
    Private controller As ProductsController
    Private newAnalysisAxisUI As NewAnalysisAxisUI
    Private categoriesTV As TreeView

    ' Variables
    Private categoriesNameKeyDic As Hashtable

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef controller As ProductsController, _
                             ByRef categoriesTV As Windows.Forms.TreeView, _
                             ByRef values_dict As Hashtable, _
                             ByRef input_categoriesNameKeyDic As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.controller = controller
        Me.categoriesTV = categoriesTV
        Me.categoriesNameKeyDic = input_categoriesNameKeyDic
        newAnalysisAxisUI = New NewAnalysisAxisUI("Product", categoriesTV)
        productsDGV = New AnalysisAxisDGV(categoriesTV, values_dict)
        TableLayoutPanel1.Controls.Add(productsDGV.DGV, 0, 1)
        productsDGV.DGV.Dock = Windows.Forms.DockStyle.Fill
        productsDGV.DGV.ContextMenuStrip = RCM_TGV

        AddHandler productsDGV.DGV.CellValueChanging, AddressOf DGV_CellValueChanging
        AddHandler productsDGV.DGV.KeyDown, AddressOf DGV_KeyDown
        AddHandler newAnalysisAxisUI.ValidateBT.Click, AddressOf NewAnalysisAxisValidate_Click

    End Sub

    Protected Friend Sub closeControl()


    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub addProductRow(ByRef item_id As String, _
                                       ByRef itemHT As Hashtable)

        productsDGV.isFillingDGV = True
        productsDGV.addRow(item_id, itemHT)
        productsDGV.isFillingDGV = False

    End Sub

#End Region


#Region "Call Backs"

    Private Sub NewProductMBT_Click(sender As Object, e As EventArgs) Handles NewProductMBT.Click, NewProductRCMBT.Click

        newAnalysisAxisUI.Show()

    End Sub

    Private Sub DeleteProductMBT_Click(sender As Object, e As EventArgs) Handles DeleteProductMBT.Click, DeleteProductRCMBT.Click

        If Not productsDGV.current_row Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Product " + Chr(13) + Chr(13) + _
                                                productsDGV.DGV.CellsArea.GetCellValue(productsDGV.current_row, productsDGV.DGV.ColumnsHierarchy.Items(0)),
                                                "Entity deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                controller.deleteproduct(productsDGV.current_row.Caption)
                productsDGV.current_row.Delete()
                productsDGV.current_row = Nothing
                productsDGV.DGV.Refresh()
            End If
        Else
            MsgBox("A product must be selected first")
        End If

    End Sub

    Private Sub ExcelDropMBT_Click(sender As Object, e As EventArgs) Handles ExcelDropMBT.Click, DropToExcelRCMBT.Click

        ' to be implemented !

    End Sub

    Private Sub copy_down_bt_Click(sender As Object, e As EventArgs) Handles CopyDownRCMBT.Click

        ' to be implemented from entities 

    End Sub

#End Region


#Region "Events"

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteProductMBT_Click(sender, e)
        End Select

    End Sub

    Private Sub DGV_CellValueChanging(sender As Object, args As CellCancelEventArgs)

        If productsDGV.isFillingDGV = False Then
            Dim value As Object
            Select Case args.Cell.ColumnItem.ItemIndex
                Case 0 : If controller.updateProductName(args.Cell.RowItem.Caption, args.Cell.Editor.EditorValue) = False Then args.Cancel = True
                Case Else
                    If categoriesNameKeyDic.ContainsKey(args.Cell.Editor.EditorValue) = True Then
                        value = categoriesNameKeyDic(args.Cell.Editor.EditorValue)
                        controller.updateProductCategory(args.Cell.RowItem.Caption, _
                                                         productsDGV.columnsCaptionIDDict(args.Cell.ColumnItem.Caption), _
                                                         value)
                    Else
                        args.Cancel = True
                    End If
            End Select
        End If

    End Sub

    Private Sub NewAnalysisAxisValidate_Click(sender As Object, e As EventArgs)

        Dim hash As New Hashtable
        hash.Add(NAME_VARIABLE, newAnalysisAxisUI.nameTextEditor.Text)
        For Each categoryNode As TreeNode In categoriesTV.Nodes
            Dim categoryValueText As String = newAnalysisAxisUI.Controls.Find(categoryNode.Name + "CB", True)(0).Text
            If categoryValueText <> "" Then
                hash.Add(categoryNode.Name, categoriesNameKeyDic(categoryValueText))
            Else
                hash.Add(categoryNode.Name, categoryNode.Name & NON_ATTRIBUTED_SUFIX)
            End If
        Next
        controller.createProduct(hash)
        newAnalysisAxisUI.Hide()
        productsDGV.DGV.Refresh()

    End Sub

#End Region


End Class
