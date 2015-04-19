' GenericSelectionBuilder.vb
'
'  Generates Filtered lists from categories selection treeviews for analysis axis
'  Works for Clients and Products (Entities has ESB)
'
'
'
' Author: Julien Monnereau
' Last modified: 17/04/2015


Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class GenericSelectionBuilder

    Protected Friend Shared Function getFilteredList(ByRef categoriesTV As TreeView, _
                                                     ByRef analysis_axis As String) As List(Of String)

        Dim strSqlQuery As String = SQLQueryBuild(SelectionDictionaryBuild(categoriesTV))
        Return SQLFilterListsGenerators.GetAnalysisAxisFilterList(analysis_axis, strSqlQuery)
 
    End Function

    ' Builds a Selection Dictionary from the categories TV
    ' Keys -> Categories to be filtered on
    ' Values -> Filtered (Selected) Values
    Protected Friend Shared Function SelectionDictionaryBuild(ByRef categoriesTV As TreeView) As Dictionary(Of String, List(Of String))

        Dim selectionDictionary As New Dictionary(Of String, List(Of String))
        Dim ActiveFilterCategoriesList As New List(Of String)

        For Each node As TreeNode In categoriesTV.Nodes
            Dim tmpList1 As New List(Of String)
            For Each valueNode As TreeNode In node.Nodes
                If valueNode.Checked = False Then tmpList1.Add(valueNode.Name)
            Next
            If tmpList1.Count > 0 Then ActiveFilterCategoriesList.Add(node.Name)
        Next

        For Each Category As String In ActiveFilterCategoriesList
            Dim tmpList As New List(Of String)
            For Each valueNode As TreeNode In categoriesTV.Nodes.Find(Category, False)(0).Nodes
                If valueNode.Checked = False Then tmpList.Add(valueNode.Name)
            Next
            If tmpList.Count > 0 Then selectionDictionary.Add(Category, tmpList)
        Next
        Return selectionDictionary

    End Function


    ' Builds an SQL Query WHERE CLAUSE to match categories filter criterias
    Protected Friend Shared Function SQLQueryBuild(ByRef SelectionDictionary As Dictionary(Of String, List(Of String))) As String

        Dim strSqlQuery As String = ""
        If SelectionDictionary.Count > 0 Then
            Dim values As String
            For Each Category As String In SelectionDictionary.Keys
                Dim tmpStr As String
                values = "'" + Join(SelectionDictionary.Item(Category).ToArray, "','") + "'"
                tmpStr = " NOT " + Category + " IN (" + values + ") AND "
                strSqlQuery = strSqlQuery + tmpStr
            Next
            strSqlQuery = Left(strSqlQuery, Len(strSqlQuery) - Len(" AND "))
            Return strSqlQuery
        Else
            Return ""
        End If

    End Function


End Class
