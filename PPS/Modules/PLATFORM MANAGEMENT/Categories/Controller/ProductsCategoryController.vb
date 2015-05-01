' productsCategoriesController.vb
'
'
'
' Author: Julien Monnereau
' Last modified: 27/04/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class productsCategoriesController
    : Inherits AnalysisAxisCategoriesController



#Region "Initialize"

    Protected Friend Sub New()

        MyBase.New(ControllingUI2Controller.product_CATEGORY_CODE)
        MyBase.View.CategoriesToolStripMenuItem.Text = "Products Category"

    End Sub

#End Region


#Region "Interface"

    Protected Friend Overrides Function CreateCategory(ByRef category_name As String) As Boolean

        If MyBase.Categories.isNameValid(category_name) = False Then Return False
        Dim new_category_id As String = MyBase.Categories.getNewId()

        Dim hash As New Hashtable
        hash.Add(CATEGORY_ID_VARIABLE, new_category_id)
        hash.Add(CATEGORY_NAME_VARIABLE, category_name)
        hash.Add(CATEGORY_IS_CATEGORY_VARIABLE, 1)
        hash.Add(ITEMS_POSITIONS, 1)
        MyBase.Categories.CreateCategory(hash)
        Product.CreateNewProductsVariable(new_category_id)

        MyBase.AddNode(new_category_id, category_name, 1)

        GenerateNewCategoryDefaultValue(MyBase.CategoriesTV.Nodes.Find(new_category_id, True)(0))
        Return True

    End Function

    Protected Friend Overrides Sub DeleteCategory(ByRef node As TreeNode)

        For Each child_node In node.Nodes
            MyBase.Categories.DeleteCategory(child_node.Name)
            MyBase.categories_names_list.Remove(child_node.text)
        Next
        MyBase.Categories.DeleteCategory(node.Name)
        Product.DeleteProductsVariable(node.Name)

        MyBase.categories_names_list.Remove(node.Text)
        node.Remove()

    End Sub

    Protected Friend Overrides Sub DeleteCategoryValue(ByRef node As TreeNode)

        Product.ReplaceProductsCategoryValue(node.Parent.Name, node.Name)
        MyBase.Categories.DeleteCategory(node.Name)
        MyBase.categories_names_list.Remove(node.Text)
        node.Remove()

    End Sub

#End Region


End Class
