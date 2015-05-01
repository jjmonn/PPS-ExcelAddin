' AnalysisAxisCategoriesController.vb
'
' Generic
'
'
'
' Author: Julien Monnereau
' Last modified: 27/04/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend MustInherit Class AnalysisAxisCategoriesController


#Region "Instance Variables"

    ' Objects
    Protected Categories As AnalysisAxisCategory
    Protected View As AnalysisCategoriesControl
    Protected CategoriesTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend positions_dictionary As New Dictionary(Of String, Double)
    Protected categories_names_list As List(Of String)

#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef category_code As String)

        Categories = New AnalysisAxisCategory(category_code)
        View = New AnalysisCategoriesControl(Me, CategoriesTV)
        AnalysisAxisCategory.LoadCategoryCodeTV(CategoriesTV, category_code)
        categories_names_list = TreeViewsUtilities.GetNodesTextsList(CategoriesTV)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
        View.Show()

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.closeControl()

    End Sub

    Protected Friend Sub sendCloseOrder()

        View.Dispose()
        Categories.closeRST()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "Interface"

    Protected Friend MustOverride Function CreateCategory(ByRef category_name As String) As Boolean
    Protected Friend MustOverride Sub DeleteCategory(ByRef node As TreeNode)
    Protected Friend MustOverride Sub DeleteCategoryValue(ByRef node As TreeNode)


    Protected Friend Function CreateCategoryValue(ByRef category_value_name As String, _
                                                  ByRef parent_node As TreeNode, _
                                                  Optional ByRef new_category_value_id As String = "") As Boolean

        If Categories.isNameValid(category_value_name) = False Then Return False
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

    Protected Friend Function RenameCategoryValue(ByRef category_value_id As String, _
                                                  ByRef new_name As String) As Boolean

        If Categories.isNameValid(new_name) = False Then Return False
        Categories.UpdateCategory(category_value_id, CATEGORY_NAME_VARIABLE, new_name)
        Return True

    End Function

    Protected Friend Function IsCategory(ByRef id As String) As Boolean

        If Categories.ReadCategory(id, CATEGORY_IS_CATEGORY_VARIABLE) = 1 Then Return True Else Return False

    End Function

#End Region


#Region "Utilities"

    Protected Sub AddNode(ByRef new_id As String, _
                        ByRef new_name As String, _
                        ByRef image_index As Int32, _
                        Optional ByRef parent_node As TreeNode = Nothing)

        If parent_node Is Nothing Then
            CategoriesTV.Nodes.Add(new_id, new_name, image_index, image_index)
        Else
            parent_node.Nodes.Add(new_id, new_name, image_index, image_index)
        End If
        categories_names_list.Add(new_name)

    End Sub

    Protected Sub GenerateNewCategoryDefaultValue(ByRef parent_node As TreeNode)

        Dim new_category_value_name As String = parent_node.Text & " -" & NON_ATTRIBUTED_SUFIX
        Dim new_category_value_id As String = parent_node.Name & NON_ATTRIBUTED_SUFIX
        CreateCategoryValue(new_category_value_name, parent_node, new_category_value_id)

    End Sub

    Protected Friend Sub SendNewPositionsToModel()

        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
        For Each category_id In positions_dictionary.Keys
            Categories.UpdateCategory(category_id, ITEMS_POSITIONS, positions_dictionary(category_id))
        Next

    End Sub

#End Region




End Class
