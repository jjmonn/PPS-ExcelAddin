Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

' Filter.vb
'
' CRUD for filters table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 22/07/2015
' Last modified: 02/09/2015

Class FilterManager : Inherits AxedCRUDManager(Of Filter)

#Region "Instance variables"

    ' Variables
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Constants
    Private FORBIDEN_CHARS = {","}

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_FILTER
        ReadCMSG = ClientMessage.CMSG_READ_FILTER
        UpdateCMSG = ClientMessage.CMSG_UPDATE_FILTER
        UpdateListCMSG = ClientMessage.CMSG_CRUD_FILTER_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_FILTER
        ListCMSG = ClientMessage.CMSG_LIST_FILTER

        CreateSMSG = ServerMessage.SMSG_CREATE_FILTER_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_FILTER_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_FILTER_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_FILTER_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_FILTER_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_FILTER_ANSWER

        Build = AddressOf Filter.BuildFilter

        InitCallbacks()

    End Sub

#End Region

#Region "Utilities"

#Region "Treeviews"

    Friend Sub LoadFiltersNode(ByRef node As Windows.Forms.TreeNode, _
                               ByRef p_axisType As AxisType)

        TreeViewsUtilities.LoadTreeview(node, m_CRUDDic(p_axisType))

    End Sub

    Friend Sub LoadFiltersTV(ByRef TV As Windows.Forms.TreeView, _
                            ByRef p_axisType As AxisType)

        TreeViewsUtilities.LoadTreeview(TV, m_CRUDDic(p_axisType))

    End Sub

    Friend Sub LoadFiltersNode(ByRef node As VIBlend.WinForms.Controls.vTreeNode, _
                               ByRef p_axisType As AxisType)

        VTreeViewUtil.LoadTreenode(node, m_CRUDDic(p_axisType))

    End Sub

    Friend Sub LoadFiltersTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                             ByRef p_axisType As AxisType)

        VTreeViewUtil.LoadTreeview(TV, m_CRUDDic(p_axisType))

    End Sub

#End Region

    ' Recursive
    Friend Function GetMostNestedFilterId(ByRef filter_id As Int32) As Int32

        Dim children_filter_id As Int32 = GetFilterChild(filter_id)
        If children_filter_id = -1 Then
            Return filter_id
        Else
            Return GetMostNestedFilterId(children_filter_id)
        End If

    End Function

    Friend Function GetFilterChild(ByRef filter_id As Int32) As Int32

        For Each axis In m_CRUDDic.Keys
            For Each filter As Filter In GetDictionary(axis).Values
                If filter.ParentId = filter_id Then Return filter.Id
            Next
        Next

        Return -1

    End Function

    Friend Function GetAxisOfFilter(ByRef filter_id As UInt32) As AxisType
        For Each axis In m_CRUDDic.Keys
            If Not GetValue(axis, filter_id) Is Nothing Then Return axis
        Next
        Return Nothing
    End Function

    ' should load treenode instead ? => filters tv not displayed - priority normal (function to be implemented) !
    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ In FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        For Each axis In m_CRUDDic.Keys
            If Not GetDictionary(axis)(name) Is Nothing Then Return False
        Next
        Return True

    End Function

#End Region

End Class
