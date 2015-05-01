' ProductsControl.vb
'
' 
'
'
'
' Author: Julien Monnereau
' Last modified: 21/04/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ProductsController


#Region "Instance Variables"

    ' Objects
    Private view As ProductsControl
    Private products As Product
    Private products_categoriesTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variable
    Private products_list As List(Of String)
    Friend categoriesNameKeyDic As Hashtable


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        products = New Product
        AnalysisAxisCategory.LoadCategoryCodeTV(products_categoriesTV, ControllingUI2Controller.PRODUCT_CATEGORY_CODE)
        products_list = ProductsMapping.GetproductsIDList()
        categoriesNameKeyDic = CategoriesMapping.GetCategoryDictionary(ControllingUI2Controller.PRODUCT_CATEGORY_CODE, _
                                                                       ANALYSIS_AXIS_NAME_VAR, _
                                                                       ANALYSIS_AXIS_ID_VAR)
        view = New ProductsControl(Me, _
                                   products_categoriesTV, _
                                   getProductsHash(), _
                                   categoriesNameKeyDic)
    
    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(view)
        view.Dock = Windows.Forms.DockStyle.Fill

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
        view.Dispose()
        products.RST.Close()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub createProduct(ByRef hash As Hashtable)

        If products.isNameValid(hash(ANALYSIS_AXIS_NAME_VAR)) = True Then
            hash.Add(ANALYSIS_AXIS_ID_VAR, products.getNewId())
            products.CreateProduct(hash)
            view.addProductRow(hash(ANALYSIS_AXIS_ID_VAR), hash)
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
        End If
      
    End Sub

    Protected Friend Function updateProductName(ByRef item_id As String, _
                                                ByRef value As String) As Boolean

        If products.isNameValid(value) = True Then
            products.UpdateProduct(item_id, ANALYSIS_AXIS_NAME_VAR, value)
            Return True
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
            Return False
        End If

    End Function

    Protected Friend Sub updateProductCategory(ByRef item_id As String, _
                                               ByRef category_id As String, _
                                               ByRef value As String)

        products.UpdateProduct(item_id, category_id, value)

    End Sub

    Protected Friend Sub deleteproduct(ByRef product_id As String)

        products.deleteProduct(product_id)

    End Sub

#End Region



End Class
