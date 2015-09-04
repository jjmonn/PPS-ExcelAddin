' AxisFiltersController.vb
'
' Generic
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/07/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class AxisFiltersController

    ' implementation to be finished !!!!
    ' priority normal 
    '   -> manage actions triggered by tv events -> filters / filters values creation !
    '
    '

#Region "Instance Variables"

    ' Objects
    Protected View As AxisFiltersControl
    Private filtersNodes As New TreeNode
    Private FvTv As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend positions_dictionary As New Dictionary(Of Int32, Double)
    Private filters_names_list As List(Of String)

#End Region


#Region "Initialization"

    Friend Sub New(ByRef axis_id As UInt32)

        ' apply a filter on axis_id in Filters !!! 
        ' priority normal !

        View = New AxisFiltersControl(Me, FvTv)
        AxisFilter.LoadFvTv(FvTv, filtersNodes, axis_id)

        filters_names_list = TreeViewsUtilities.GetNodesTextsList(FvTv)
        positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(FvTv)
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
        View.Dispose()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Function CreateFilter(ByRef filter_name As String) As Boolean

        ' check if name is valid



    End Function

    Protected Friend Sub DeleteFilter(ByRef node As TreeNode)


    End Sub


    Protected Friend Sub DeleteFilterValue(ByRef node As TreeNode)


    End Sub


    Friend Function CreateFilterValue(ByRef filter_value_name As String, _
                                    ByRef parent_node As TreeNode, _
                                    Optional ByRef new_filter_value_id As String = "") As Boolean

        If GlobalVariables.Filters.IsNameValid(filter_value_name) = False Then Return False
        Dim hash As New Hashtable
        hash.Add(PARENT_ID_VARIABLE, CInt(parent_node.Name))
        hash.Add(NAME_VARIABLE, filter_value_name)
        hash.Add(FILTER_IS_PARENT_VARIABLE, 0)
        hash.Add(ITEMS_POSITIONS, 1)
        GlobalVariables.Filters.CMSG_CREATE_FILTER(hash)
        Return True

    End Function

    Private Sub AfterFilterCreation(ByRef ht As Hashtable)

        ' how to return false if no answer from server !??
        ' priority normal
        ' !!!

    End Sub


    Friend Sub UpdateValue(ByRef filter_id As String, ByRef field As String, ByRef value As Object)

        ' value !!!!!!!!!!!! != filter
        '    GlobalVariables.Filters.CMSG_UPDATE_FILTER(filter_id, field, value)

    End Sub

    Friend Function RenameFilterValue(ByRef filter_value_id As Int32, _
                                      ByRef new_name As String) As Boolean

        If GlobalVariables.Filters.IsNameValid(new_name) = True Then
            Dim ht As Hashtable = GlobalVariables.Filters.filters_hash(filter_value_id)
            ht(NAME_VARIABLE) = new_name
            GlobalVariables.Filters.CMSG_UPDATE_FILTER(ht)
            Return True
        Else
            Return False
        End If

    End Function

    Friend Function IsFilter(ByRef id As UInt32) As Boolean

        Dim result As TreeNode() = filtersNodes.Nodes.Find(CStr(id), True)
        If result.Length > 0 Then Return True
        Return False

    End Function


#End Region


#Region "Utilities"

    Sub GenerateNewFilterDefaultValue(ByRef parent_node As TreeNode)

        Dim new_filter_value_name As String = parent_node.Text & " -" & NON_ATTRIBUTED_SUFIX
        Dim new_filter_value_id As String = parent_node.Name & NON_ATTRIBUTED_SUFIX
        CreateFilterValue(new_filter_value_name, parent_node, new_filter_value_id)

    End Sub

    Friend Sub SendNewPositionsToModel()

        ' positions update 
        '       -> filters
        '       -> filters values
        'priority normal
        ' !!!

        'positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(filtersTV)
        'Dim batch_update As New List(Of Object())
        'For Each filter_id In positions_dictionary.Keys
        '    batch_update.Add({filter_id, ITEMS_POSITIONS, positions_dictionary(filter_id)})
        'Next
        'GlobalVariables.Filters.UpdateBatch(batch_update)
        ' CP while updating
        ' review ! priority normal

    End Sub

#End Region




End Class
