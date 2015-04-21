Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections

' ProductsControl.vb
'
' 
'
'
'
' Author: Julien Monnereau
' Last modified: 20/04/2015


Friend Class ProductsController


#Region "Instance Variables"

    ' Objects
    Private view As ProductsControl
    Private products As Product
    Private products_categoriesTV As New TreeView

    ' Variable
    Private products_list As List(Of String)
    Friend categoriesNameKeyDic As Hashtable


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        products = New Product
        Category.LoadCategoryCodeTV(products_categoriesTV, ControllingUI2Controller.PRODUCT_CATEGORY_CODE)
        products_list = ProductsMapping.GetproductsIDList()
        categoriesNameKeyDic = CategoriesMapping.GetCategoryDictionary(ControllingUI2Controller.PRODUCT_CATEGORY_CODE, _
                                                                       ANALYSIS_AXIS_NAME_VAR, _
                                                                       ANALYSIS_AXIS_ID_VAR)
        view = New ProductsControl(Me, _
                                   products_categoriesTV, _
                                   getProductsHash(), _
                                   categoriesNameKeyDic)
    
    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel)

        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Function getProductsHash() As Dictionary(Of String, Hashtable)

        Dim tmp_dict As New Dictionary(Of String, Hashtable)
        For Each product_id As String In products_list
            tmp_dict.Add(product_id, products.GetRecord(product_id, products_categoriesTV))
        Next
        Return tmp_dict

    End Function

    Public Sub close()

        View.closeControl()
        ' View.Dispose()
        'View = Nothing
        products.RST.Close()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub updateValue(ByRef category_id As String, _
                                     ByRef item_id As String, _
                                     ByRef value As String)



    End Sub

#End Region



End Class
