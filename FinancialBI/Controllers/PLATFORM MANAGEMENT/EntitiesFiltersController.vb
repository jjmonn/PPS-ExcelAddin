﻿'' EntitiesCategoriesController.vb
''
''
''
'' Author: Julien Monnereau
'' Last modified: 25/06/2015


'Imports System.Windows.Forms
'Imports System.Collections
'Imports System.Collections.Generic


'Friend Class EntitiesCategoriesController : Inherits AxisFiltersController



'#Region "Instance Variables"

'    ' Objects
'    Private SQLEntities As New SQLEntities

'#End Region


'#Region "Initialize"

'    Protected Friend Sub New()

'        MyBase.New(GlobalEnums.AnalysisAxis.ENTITIES)
'        MyBase.View.CategoriesToolStripMenuItem.Text = "Entities Categories"

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

'        MyBase.AddNode(new_category_id, category_name, 1)

'        ' Create new filter ! priority normal
'        ' -> relative to filters_table

'        GenerateNewCategoryDefaultValue(MyBase.CategoriesTV.Nodes.Find(new_category_id, True)(0))
'        Return True

'    End Function

'    Protected Friend Overrides Sub DeleteCategory(ByRef node As TreeNode)

'        For Each child_node In node.Nodes
'            MyBase.Categories.DeleteCategory(child_node.Name)
'            MyBase.categories_names_list.Remove(child_node.text)
'        Next
'        MyBase.Categories.DeleteCategory(node.Name)

'        ' Relative to filters table -> priority normal
'        'SQLEntities.DeleteEntitiesVariable(node.Name)

'        MyBase.categories_names_list.Remove(node.Text)
'        node.Remove()
'        '    EntitiesSQLViews.CreateAllEntitiesViews()

'    End Sub

'    Protected Friend Overrides Sub DeleteCategoryValue(ByRef node As TreeNode)

'        SQLEntities.ReplaceEntitiesCategoryValue(node.Parent.Name, node.Name)
'        MyBase.Categories.DeleteCategory(node.Name)
'        MyBase.categories_names_list.Remove(node.Text)
'        node.Remove()

'    End Sub

'#End Region


'End Class

