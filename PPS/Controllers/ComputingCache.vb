Imports VIBlend.WinForms.Controls
Imports System.Collections.Generic
Imports System.Linq


Public Class ComputingCache


#Region "Instance Variables"

    ' Objects
    Private entitiesTV As New vTreeView

    ' Variables
    Friend cacheEntityID As Int32
    Friend cacheCurrencyId As Int32
    Friend cacheVersions() As Int32
    Friend cacheComputingHierarchyList As List(Of String)
    Friend cacheFilters As Dictionary(Of Int32, List(Of Int32))
    Friend cacheAxisFilters As Dictionary(Of Int32, List(Of Int32))

    ' Flags
    Private computingHierarchyCompareFlag As Boolean


#End Region


#Region "Initialize"

    Friend Sub New(ByRef computingHierarchyCompare As Boolean)

        computingHierarchyCompareFlag = computingHierarchyCompare
        GlobalVariables.Entities.LoadEntitiesTV(entitiesTV)
        ' listen to entities updates !! 

    End Sub

    Friend Sub ResetCache()



    End Sub

#End Region


    Friend Function CheckCache(ByRef entityId As Int32, _
                               ByRef currencyId As Int32, _
                               ByRef versionIds() As Int32, _
                               ByRef filters As Dictionary(Of Int32, List(Of Int32)), _
                               ByRef axisFilters As Dictionary(Of Int32, List(Of Int32)), _
                               Optional ByRef computingHierarchyList As List(Of String) = Nothing) As Boolean

        ' entityId => included in current scope
        Dim cacheEntityNode As vTreeNode = VTreeViewUtil.FindNode(entitiesTV, cacheEntityID)
        If VTreeViewUtil.FindNode(cacheEntityNode, entityId) Is Nothing Then Return False
        If cacheCurrencyId <> currencyId Then Return False
        For Each versionId In versionIds
            If cacheVersions.Contains(versionId) = False Then Return False
        Next

        ' filters / axis filters
        If Utilities_Functions.DictsCompare(filters, cacheFilters) = False Then Return False
        If Utilities_Functions.DictsCompare(axisFilters, cacheAxisFilters) = False Then Return False

        If computingHierarchyCompareFlag = True Then
            ' decomposition dimensions
            For Each dimensionId As String In computingHierarchyList
                If cacheComputingHierarchyList.Contains(dimensionId) = False Then Return False
            Next
        End If

        Return True

    End Function




End Class
