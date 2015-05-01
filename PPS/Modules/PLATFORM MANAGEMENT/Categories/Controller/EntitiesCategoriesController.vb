' EntitiesCategoriesController.vb
'
'
'
' Author: Julien Monnereau
' Last modified: 28/04/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class EntitiesCategoriesController : Inherits AnalysisAxisCategoriesController



#Region "Instance Variables"

    ' Objects
    Private EntitiesSQLViews As New ViewsController
    Private SQLEntities As New SQLEntities

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        MyBase.New(ControllingUI2Controller.ENTITY_CATEGORY_CODE)
        MyBase.View.CategoriesToolStripMenuItem.Text = "Entities Categories"

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

        MyBase.AddNode(new_category_id, category_name, 1)
        SQLEntities.CreateNewEntitiesVariable(new_category_id)
        EntitiesSQLViews.CreateAllEntitiesViews()
        GenerateNewCategoryDefaultValue(MyBase.CategoriesTV.Nodes.Find(new_category_id, True)(0))
        Return True

    End Function

    Protected Friend Overrides Sub DeleteCategory(ByRef node As TreeNode)

        For Each child_node In node.Nodes
            MyBase.Categories.DeleteCategory(child_node.Name)
            MyBase.categories_names_list.Remove(child_node.text)
        Next
        MyBase.Categories.DeleteCategory(node.Name)
        SQLEntities.DeleteEntitiesVariable(node.Name)
        MyBase.categories_names_list.Remove(node.Text)
        node.Remove()
        EntitiesSQLViews.CreateAllEntitiesViews()

    End Sub

    Protected Friend Overrides Sub DeleteCategoryValue(ByRef node As TreeNode)

        SQLEntities.ReplaceEntitiesCategoryValue(node.Parent.Name, node.Name)
        MyBase.Categories.DeleteCategory(node.Name)
        MyBase.categories_names_list.Remove(node.Text)
        node.Remove()

    End Sub

#End Region


End Class

