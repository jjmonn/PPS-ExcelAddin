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
    Private productsFiltersTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variable
    Friend categoriesNameKeyDic As Hashtable


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        GlobalVariables.Filters.LoadFiltersTV(productsFiltersTV, GlobalEnums.AnalysisAxis.PRODUCTS)
        GlobalVariables.Filters.GetFiltersDictionary(GlobalEnums.AnalysisAxis.PRODUCTS, NAME_VARIABLE, ID_VARIABLE)

        view = New ProductsControl(Me, _
                                   productsFiltersTV, _
                                   getProductsHash(), _
                                   categoriesNameKeyDic)

        AddHandler GlobalVariables.Products.CreationEvent, AddressOf AfterProductCreation
        AddHandler GlobalVariables.Products.UpdateEvent, AddressOf AfterProductUpdate
        AddHandler GlobalVariables.Products.DeleteEvent, AddressOf AfterproductDelete


    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(view)
        view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Function getProductsHash() As Hashtable

        Dim tmp_dict As New Hashtable

        ' same issue as in entitiesController
        ' function to be placed elsewhere (filters values ?)
        ' priority normal !! !

        'For Each product_id As String In products_list
        '    tmp_dict.Add(product_id, products.GetRecord(product_id, productsFiltersTV))
        'Next
        Return tmp_dict

    End Function

    Public Sub close()

        view.closeControl()
        view.Dispose()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "Interface"

    Friend Sub createProduct(ByRef hash As Hashtable)

        If GlobalVariables.Products.IsNameValid(hash(NAME_VARIABLE)) = True Then
            GlobalVariables.Products.CMSG_CREATE_PRODUCT(hash)
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
        End If

    End Sub

    Private Sub AfterProductCreation(ByRef status As Boolean, ByRef id As Int32)

        ' to be validated/ reviewed
        ' priority normal !!! 
        ' view.addProductRow(ht(ID_VARIABLE), ht)

    End Sub


    Friend Function updateProductName(ByRef item_id As String, _
                                      ByRef value As String) As Boolean

        If GlobalVariables.Products.IsNameValid(value) = True Then
            Update(item_id, NAME_VARIABLE, value)
            Return True
        Else
            MsgBox("Invalid Name. Names must be unique and not empty.")
            Return False
        End If

    End Function

    Private Sub Update(ByRef id As Int32, _
                     ByRef variable As String, _
                     ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.Products.products_hash(id)
        ht(variable) = value
        GlobalVariables.Products.CMSG_UPDATE_PRODUCT(ht)

    End Sub


    Private Sub AfterProductUpdate(ByRef status As Boolean, ByRef id As Int32)

        ' to be reviewed -> priority normal !

    End Sub

    Friend Sub UpdateProductCategory(ByRef item_id As String, _
                                     ByRef category_id As String, _
                                     ByRef value As String)

        ' attention only update most nested filter
        Update(item_id, category_id, value)

    End Sub

    Friend Sub deleteproduct(ByRef product_id As String)

        GlobalVariables.Products.CMSG_DELETE_PRODUCT(product_id)

    End Sub

    Private Sub AfterproductDelete(ByRef status As Boolean, ByRef id As UInt32)

        ' to be implemented
        ' priority normal

    End Sub

#End Region



End Class
