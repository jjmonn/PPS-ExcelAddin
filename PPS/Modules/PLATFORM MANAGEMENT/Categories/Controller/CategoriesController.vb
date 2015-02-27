' CategoriesController.vb
'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 11/12/2014


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class CategoriesController


#Region "Instance Variables"

    ' Objects
    Private Categories As New Category
    Private ViewObject As CategoriesManagementUI
    Private ViewsController As New ViewsController
    Private SQLEntities As New SQLEntities
    Private CategoriesTV As New TreeView

    ' Variables
    Friend positions_dictionary As New Dictionary(Of String, Double)
    Private categories_names_list As List(Of String)

    ' Constants
    Private FORBIDEN_CHARS As String() = {","}
    

#End Region


#Region "Initialization"

    Friend Sub New()

        ViewObject = New CategoriesManagementUI(Me, CategoriesTV)
        Category.LoadCategoriesTree(CategoriesTV)
        categories_names_list = TreeViewsUtilities.GetNodesTextsList(CategoriesTV)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
        ViewObject.Show()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Function CreateCategory(ByRef category_name As String) As Boolean

        If CategoryNameCheck(category_name) = False Then Return False
        Dim new_category_id As String = TreeViewsUtilities.GetNewNodeKey(CategoriesTV, CATEGORIES_TOKEN_SIZE)
        Dim hash As New Hashtable
        hash.Add(CATEGORY_ID_VARIABLE, new_category_id)
        hash.Add(CATEGORY_NAME_VARIABLE, category_name)
        hash.Add(CATEGORY_IS_CATEGORY_VARIABLE, 1)
        hash.Add(ITEMS_POSITIONS, 1)
        Categories.CreateCategory(hash)

        AddNode(new_category_id, category_name, 1)
        SQLEntities.CreateNewEntitiesVariable(new_category_id)
        ViewsController.CreateAllEntitiesViews()
        GenerateNewCategoryDefaultValue(CategoriesTV.Nodes.Find(new_category_id, True)(0))
        Return True

    End Function

    Protected Friend Function CreateCategoryValue(ByRef category_value_name As String, _
                                                  ByRef parent_node As TreeNode, _
                                                  Optional ByRef new_category_value_id As String = "") As Boolean

        If CategoryNameCheck(category_value_name) = False Then Return False
        If new_category_value_id = "" Then new_category_value_id = TreeViewsUtilities.GetNewNodeKey(CategoriesTV, CATEGORIES_TOKEN_SIZE)
        Dim hash As New Hashtable
        hash.Add(CATEGORY_ID_VARIABLE, new_category_value_id)
        hash.Add(CATEGORY_PARENT_ID_VARIABLE, parent_node.Name)
        hash.Add(CATEGORY_NAME_VARIABLE, category_value_name)
        hash.Add(CATEGORY_IS_CATEGORY_VARIABLE, 0)
        hash.Add(ITEMS_POSITIONS, 1)
        Categories.CreateCategory(hash)
        AddNode(new_category_value_id, category_value_name, 0, parent_node)
        Return True

    End Function

    Protected Friend Sub UpdateValue(ByRef category_id As String, ByRef field As String, ByRef value As Object)

        Categories.UpdateCategory(category_id, field, value)

    End Sub

    Protected Friend Sub DeleteCategory(ByRef node As TreeNode)

        For Each child_node In node.Nodes
            Categories.DeleteCategory(child_node.Name)
            categories_names_list.Remove(child_node.text)
        Next
        Categories.DeleteCategory(node.Name)
        SQLEntities.DeleteEntitiesVariable(node.Name)
        ViewsController.CreateAllEntitiesViews()
        categories_names_list.Remove(node.Text)
        node.Remove()

    End Sub

    Protected Friend Sub DeleteCategoryValue(ByRef node As TreeNode)

        SQLEntities.ReplaceEntitiesCategoryValue(node.Parent.Name, node.Name)
        Categories.DeleteCategory(node.Name)
        categories_names_list.Remove(node.Text)
        node.Remove()

    End Sub

    Protected Friend Function RenameCategoryValue(ByRef category_value_id As String, _
                                                  ByRef new_name As String) As Boolean

        If CategoryNameCheck(new_name) = False Then Return False
        Categories.UpdateCategory(category_value_id, CATEGORY_NAME_VARIABLE, new_name)
        Return True

    End Function

    Protected Friend Function IsCategory(ByRef id As String) As Boolean

        If Categories.ReadCategory(id, CATEGORY_IS_CATEGORY_VARIABLE) = 1 Then Return True Else Return False

    End Function

#End Region


#Region "Utilities"

    Private Function CategoryNameCheck(ByRef name As String) As Boolean

        If categories_names_list.Contains(name) Then Return False
        If name.Length > CATEGORIES_NAME_MAX_LENGTH Then Return False
        For Each char_ In FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        Return True

    End Function

    Private Sub GenerateNewCategoryDefaultValue(ByRef parent_node As TreeNode)

        Dim new_category_value_name As String = parent_node.Text & " -" & NON_ATTRIBUTED_SUFIX
        Dim new_category_value_id As String = parent_node.Name & NON_ATTRIBUTED_SUFIX
        CreateCategoryValue(new_category_value_name, parent_node, new_category_value_id)

    End Sub

    Private Sub AddNode(ByRef new_id As String, _
                        ByRef new_name As String, _
                        ByRef image_index As Int32, _
                        Optional ByRef parent_node As TreeNode = Nothing)

        If parent_node Is Nothing Then
            CategoriesTV.Nodes.Add(new_id, new_name, image_index, image_index)
        Else
            parent_node.Nodes.Add(new_id, new_name, image_index, image_index)
        End If
        categories_names_list.Add(new_name)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
        Categories.UpdateCategory(new_id, ITEMS_POSITIONS, positions_dictionary(new_id))

    End Sub

    Protected Friend Sub SendNewPositionsToModel()

        For Each category_id In positions_dictionary.Keys
            Categories.UpdateCategory(category_id, ITEMS_POSITIONS, positions_dictionary(category_id))
        Next

    End Sub

#End Region



End Class

