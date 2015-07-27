'' ClientsCategoriesController.vb
''
''
''
'' Author: Julien Monnereau
'' Last modified: 27/04/2015


'Imports System.Windows.Forms
'Imports System.Collections
'Imports System.Collections.Generic


'Friend Class ClientsCategoriesController
'    : Inherits AxisFiltersController



'#Region "Initialize"

'    Protected Friend Sub New()

'        MyBase.New(GlobalEnums.AnalysisAxis.CLIENTS)
'        MyBase.View.CategoriesToolStripMenuItem.Text = "Clients Categories"

'    End Sub

'#End Region


'#Region "Interface"

'    Protected Friend Overrides Function CreateCategory(ByRef category_name As String) As Boolean

'        If MyBase.Categories.isNameValid(category_name) = False Then Return False
'        Dim new_category_id As String = MyBase.Categories.getNewId()

'        Dim hash As New Hashtable
'        hash.Add(ID_VARIABLE, new_category_id)
'        hash.Add(NAME_VARIABLE, category_name)
'        hash.Add(CATEGORY_IS_CATEGORY_VARIABLE, 1)
'        hash.Add(ITEMS_POSITIONS, 1)
'        MyBase.Categories.CreateCategory(hash)
'        Client.CreateNewClientsVariable(new_category_id)

'        MyBase.AddNode(new_category_id, category_name, 1)

'        GenerateNewCategoryDefaultValue(MyBase.CategoriesTV.Nodes.Find(new_category_id, True)(0))
'        Return True

'    End Function

'    Protected Friend Overrides Sub Deletefilter(ByRef node As TreeNode)

'        For Each child_node In node.Nodes
'            MyBase.Categories.DeleteCategory(child_node.Name)
'            MyBase.categories_names_list.Remove(child_node.text)
'        Next
'        MyBase.Categories.DeleteCategory(node.Name)
'        Client.DeleteClientsVariable(node.Name)

'        MyBase.categories_names_list.Remove(node.Text)
'        node.Remove()

'    End Sub

'    Protected Friend Overrides Sub DeleteCategoryValue(ByRef node As TreeNode)

'        Client.ReplaceClientsCategoryValue(node.Parent.Name, node.Name)
'        MyBase.Categories.DeleteCategory(node.Name)
'        MyBase.categories_names_list.Remove(node.Text)
'        node.Remove()

'    End Sub

'#End Region


'End Class
