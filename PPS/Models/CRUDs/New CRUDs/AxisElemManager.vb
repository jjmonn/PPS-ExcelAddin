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
#End Region

#Region "Utilities"
    Friend Sub LoadAxisTV(ByVal p_axis As AxisType, ByRef TV As vTreeView)

        TV.Nodes.Clear()
        VTreeViewUtil.LoadTreeview(TV, m_CRUDDic(p_axis))

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        TV.Nodes.Clear()
        For Each axisElem As AxisElem In m_CRUDDic(p_axis).Values
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(axisElem.Id, axisElem.Name, TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next
    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, ByRef DestNode As VIBlend.WinForms.Controls.vTreeNode)

        DestNode.Nodes.Clear()
        For Each axisElem As AxisElem In m_CRUDDic(p_axis).Values
            Dim newNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(axisElem.Id, axisElem.Name, DestNode, 0)
            newNode.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub


    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each axisElem As AxisElem In m_CRUDDic(p_axis).Values
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(axisElem.Id, axisElem.Name, TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub
#End Region

End Class
