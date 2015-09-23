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

        cacheEntityID = 0
        cacheCurrencyId = 0
        Erase cacheVersions
        If Not cacheComputingHierarchyList Is Nothing Then cacheComputingHierarchyList.Clear()
        If Not cacheFilters Is Nothing Then cacheFilters.Clear()
        If Not cacheAxisFilters Is Nothing Then cacheAxisFilters.Clear()
        GlobalVariables.g_mustResetCache = False

    End Sub

#End Region


    Friend Function MustCompute(ByRef entityId As Int32, _
                               ByRef currencyId As Int32, _
                               ByRef versionIds() As Int32, _
                               ByRef filters As Dictionary(Of Int32, List(Of Int32)), _
                               ByRef axisFilters As Dictionary(Of Int32, List(Of Int32)), _
                               Optional ByRef computingHierarchyList As List(Of String) = Nothing) As Boolean

        ' entityId => included in current scope
        Dim cacheEntityNode As vTreeNode = VTreeViewUtil.FindNode(entitiesTV, cacheEntityID)
        If cacheEntityNode Is Nothing Then Return True
        If VTreeViewUtil.FindNode(cacheEntityNode, entityId, True) Is Nothing Then Return True
        If cacheCurrencyId <> currencyId Then Return True
        For Each versionId In versionIds
            If cacheVersions.Contains(versionId) = False Then Return True
        Next

        ' filters / axis filters
        If Utilities_Functions.DictsCompare(filters, cacheFilters) = False Then Return True
        If Utilities_Functions.DictsCompare(cacheFilters, filters) = False Then Return True
        If Utilities_Functions.DictsCompare(axisFilters, cacheAxisFilters) = False Then Return True
        If Utilities_Functions.DictsCompare(cacheAxisFilters, axisFilters) = False Then Return True

        If computingHierarchyCompareFlag = True Then
            ' decomposition dimensions
            For Each dimensionId As String In computingHierarchyList
                If cacheComputingHierarchyList.Contains(dimensionId) = False Then Return True
            Next
        End If

        Return False

    End Function




End Class
