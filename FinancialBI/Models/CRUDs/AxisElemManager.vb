Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls
Imports System.Linq
Imports CRUD


Public Class AxisElemManager : Inherits AxedCRUDManager(Of AxisElem)

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_ELEM
        ReadCMSG = ClientMessage.CMSG_READ_AXIS_ELEM
        UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_ELEM
        UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_ELEM_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_AXIS_ELEM
        ListCMSG = ClientMessage.CMSG_LIST_AXIS_ELEM

        CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_ELEM_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_AXIS_ELEM_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_ELEM_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_ELEM_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_ELEM_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_AXIS_ELEM_ANSWER

        Build = AddressOf AxisElem.BuildAxis

        InitCallbacks()

    End Sub

#End Region

#Region "Mapping"

    Friend Function GetDefaultAxis() As Int32
        Return 1
    End Function

    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        For Each axis As AxisType In m_CRUDDic.Keys
            If Not m_CRUDDic(axis)(name) Is Nothing Then Return False
        Next
        Return True

    End Function

    Friend Function GetLowerCaseNamesId(ByRef p_axis As AxisType) As SafeDictionary(Of String, UInt32)

        Dim l_lowerCaseNamesIdDict As New SafeDictionary(Of String, UInt32)
        For Each l_axisElem As AxisElem In m_CRUDDic(p_axis).SortedValues
            l_lowerCaseNamesIdDict.Add(Strings.LCase(l_axisElem.Name), l_axisElem.Id)
        Next
        Return l_lowerCaseNamesIdDict

    End Function

#End Region

#Region "Utilities"

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, m_CRUDDic(AxisType.Entities))
        TreeViewsUtilities.SetTreeviewIconsHiearachy(TV)

    End Sub

    Friend Sub LoadEntitiesTV(ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(p_treeview, m_CRUDDic(AxisType.Entities))
        VTreeViewUtil.SetTreeviewIconsHiearachy(p_treeview)

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView, _
                              ByRef nodes_icon_dic As Dictionary(Of UInt32, Int32))

        Dim tmp_ht As New MultiIndexDictionary(Of UInt32, String, NamedHierarchyCRUDEntity)
        For Each id As UInt32 In nodes_icon_dic.Keys
            Dim l_entity As AxisElem = GetValue(AxisType.Entities, id)
            If l_entity Is Nothing Then Continue For
            l_entity = l_entity.Clone()

            l_entity.Image = nodes_icon_dic(id)
            tmp_ht.Set(l_entity.Id, l_entity.Name, l_entity)
        Next
        TreeViewsUtilities.LoadTreeview(TV, tmp_ht)

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, _
                            ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView)

        p_treeview.Nodes.Clear()
        For Each l_axisElem As AxisElem In m_CRUDDic(p_axis).SortedValues
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(l_axisElem.Id, l_axisElem.Name, p_treeview, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next
        VTreeViewUtil.SetTreeviewIconsHiearachy(p_treeview)

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, _
                            ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView, _
                            ByRef p_axisParentId As UInt32)

        p_treeview.Nodes.Clear()
        For Each l_axisElem As AxisElem In m_CRUDDic(p_axis).SortedValues
            Dim l_axisParent As CRUD.AxisParent = GlobalVariables.AxisParents.GetValue(l_axisElem.Id)
            If l_axisParent IsNot Nothing _
            AndAlso l_axisParent.ParentId = p_axisParentId Then
                Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(l_axisElem.Id, l_axisElem.Name, p_treeview, 0)
                node.Checked = Windows.Forms.CheckState.Checked
            End If
        Next
        VTreeViewUtil.SetTreeviewIconsHiearachy(p_treeview)

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, _
                            ByRef p_treenode As VIBlend.WinForms.Controls.vTreeNode)

        p_treenode.Nodes.Clear()
        For Each l_axisElem As AxisElem In m_CRUDDic(p_axis).SortedValues
            Dim newNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(l_axisElem.Id, l_axisElem.Name, p_treenode, 0)
            newNode.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, _
                            ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView, _
                            ByRef p_filterList As List(Of UInt32))

        p_treeview.Nodes.Clear()
        For Each l_axisElem As AxisElem In m_CRUDDic(p_axis).SortedValues
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(l_axisElem.Id, l_axisElem.Name, p_treeview, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next
        VTreeViewUtil.SetTreeviewIconsHiearachy(p_treeview)

    End Sub

    Friend Function GetAxisListFilteredOnAxisParent(ByRef p_axis As AxisType, _
                                                    ByRef p_axisParentId As UInt32) As List(Of AxisElem)

        Dim l_list As New List(Of AxisElem)
        For Each l_axisElem As AxisElem In m_CRUDDic(p_axis).SortedValues
            Dim l_axisParent As CRUD.AxisParent = GlobalVariables.AxisParents.GetValue(l_axisElem.Id)
            If l_axisParent IsNot Nothing _
            AndAlso l_axisParent.ParentId = p_axisParentId Then
                l_list.Add(l_axisElem)
            End If
        Next
        Return l_list

    End Function

#End Region

End Class
