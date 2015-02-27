' EntitiesSelectionBuilderClass.vb
'
' Build an entitiesArray based on a selected categories values
' 
' 
' To do:
'       - Function which takes hastable filter values ARRAY as parameter and builds the selection Dictionary ?
'
'
'
'
'
' Known bugs
'
'
' Last modified: 09/12/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections



Friend Class EntitiesSelectionBuilderClass


#Region "Instance Variables"

    ' Objects
    '   Private EntitiesAttributes As Dictionary(Of String, Hashtable)
    Private CategoriesTVInstance As New TreeView
    Protected Friend EntitiesTV As New TreeView

    ' Variables
    Private SelectionDictionary As New Dictionary(Of String, List(Of String))
    Friend FinalEntitiesList As New List(Of String)
    Friend StrSqlQuery As String
    Friend StrSqlQueryForEntitiesUploadFunctions As String
    Friend isFilterFlag As Boolean


#End Region


    Protected Friend Sub New()

        Entity.LoadEntitiesTree(EntitiesTV)
        Category.LoadCategoriesTree(CategoriesTVInstance)
        TreeViewsUtilities.CheckAllNodes(EntitiesTV)

    End Sub


#Region "Interface"

    ' Produces the final selected entities list from a selected categories treeview
    Public Sub BuildCategoriesFilterFromTreeview(ByRef CategoriesTV As TreeView)

        Reset()
        SelectionDictionaryBuild(CategoriesTV)
        If SQLQueryBuild() = True Then isFilterFlag = True

    End Sub

    ' To be implemented -> will be called from PPSBI
    Public Sub BuildCategoriesFilterFromFilterList(ByRef filterList As List(Of String))

        Reset()
        TransformFilterListIntoSelectionDictionary(filterList)
        If SQLQueryBuild() = True Then isFilterFlag = True

    End Sub


#End Region


#Region "Computation"

    ' Builds an SQL Query WHERE CLAUSE to matching categories filter criterias
    Private Function SQLQueryBuild() As Boolean

        If SelectionDictionary.Count > 0 Then

            Dim values As String

            For Each Category As String In SelectionDictionary.Keys
                Dim tmpStr As String
                values = "'" + Join(SelectionDictionary.Item(Category).ToArray, "','") + "'"
                tmpStr = " NOT " + Category + " IN (" + values + ") AND "
                StrSqlQuery = StrSqlQuery + tmpStr

                Dim tmpStr2 As String
                values = "'" + Join(SelectionDictionary.Item(Category).ToArray, "','") + "'"
                tmpStr2 = " NOT (" + Category + " IN (" + values + ") AND " + ASSETS_ALLOW_EDITION_VARIABLE + "=1)" + " AND "
                StrSqlQueryForEntitiesUploadFunctions = StrSqlQueryForEntitiesUploadFunctions + tmpStr2

            Next
            StrSqlQuery = Left(StrSqlQuery, Len(StrSqlQuery) - Len(" AND "))
            StrSqlQueryForEntitiesUploadFunctions = Left(StrSqlQueryForEntitiesUploadFunctions, _
                                                         Len(StrSqlQueryForEntitiesUploadFunctions) - Len(" AND "))
            Return True
        Else
            Return False
        End If

    End Function


#End Region


#Region "Selection Dictionary Build"

    ' Build the selection Dictionary based on the selected values in Parameter CategoriesTV
    ' The list contains not selected values 
    Private Sub SelectionDictionaryBuild(ByRef CategoriesTV As TreeView)

        Dim ActiveFilterCategoriesList As New List(Of String)

        For Each node As TreeNode In CategoriesTV.Nodes
            Dim tmpList1 As New List(Of String)
            For Each valueNode As TreeNode In node.Nodes
                If valueNode.Checked = True Then tmpList1.Add(valueNode.Name)
            Next
            If tmpList1.Count > 0 Then ActiveFilterCategoriesList.Add(node.Name)
        Next

        For Each Category As String In ActiveFilterCategoriesList

            Dim tmpList As New List(Of String)
            For Each valueNode As TreeNode In CategoriesTV.Nodes.Find(Category, False)(0).Nodes
                If valueNode.Checked = False Then tmpList.Add(valueNode.Name)
            Next
            If tmpList.Count > 0 Then SelectionDictionary.Add(Category, tmpList)

        Next

    End Sub


    ' Build the selection dictionary from a filter list
    ' param: list of categories keys to filter on
    Private Sub TransformFilterListIntoSelectionDictionary(ByRef filterList As List(Of String))

        Dim ActiveFilterCategoriesList As New List(Of TreeNode)

        For Each categoryValue As String In filterList
            Dim RootCategoryNode As TreeNode = TreeViewsUtilities.ReturnRootNodeFromNode(CategoriesTVInstance.Nodes.Find(categoryValue, True)(0))
            If Not ActiveFilterCategoriesList.Contains(RootCategoryNode) Then ActiveFilterCategoriesList.Add(RootCategoryNode)
        Next

        For Each Category As TreeNode In ActiveFilterCategoriesList

            Dim tmpList As New List(Of String)
            For Each valueNode As TreeNode In Category.Nodes
                If Not filterList.Contains(valueNode.Name) Then tmpList.Add(valueNode.Name)
            Next
            If tmpList.Count > 0 Then SelectionDictionary.Add(Category.Name, tmpList)

        Next

    End Sub



#End Region


#Region "Utilities"

    ' reinitializes instances
    Private Sub Reset()

        FinalEntitiesList.Clear()
        SelectionDictionary.Clear()
        StrSqlQuery = ""
        StrSqlQueryForEntitiesUploadFunctions = ""
        isFilterFlag = False

    End Sub


#End Region



End Class
