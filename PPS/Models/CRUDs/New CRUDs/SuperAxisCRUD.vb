Imports System.Collections
Imports System.Collections.Generic


Public MustInherit Class SuperAxisCRUD

    ' Events
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)

    Protected Sub OnRead(ByRef status As Boolean, ByRef attributes As Hashtable)
        RaiseEvent Read(status, attributes)
    End Sub

    Protected Sub OnCreate(ByRef status As Boolean, ByRef id As Int32)
        RaiseEvent CreationEvent(status, id)
    End Sub

    Protected Sub OnUpdate(ByRef status As Boolean, ByRef id As Int32)
        RaiseEvent UpdateEvent(status, id)
    End Sub

    Protected Sub OnDelete(ByRef status As Boolean, ByRef id As UInt32)
        RaiseEvent DeleteEvent(status, id)
    End Sub


    ' Variables
    Friend Axis_hash As New Hashtable

    ' CRUD Methods
    Friend MustOverride Sub CMSG_CREATE_AXIS(ByRef ht As Hashtable)
    Friend MustOverride Sub CMSG_UPDATE_AXIS(ByRef ht As Hashtable)
    Friend MustOverride Sub CMSG_DELETE_AXIS(ByRef id As UInt32)

    Friend Function GetAxisValueId(ByRef name As String) As Int32
        For Each id In Axis_hash.Keys
            If Axis_hash(id)(NAME_VARIABLE) = name Then
                Return id
            End If
        Next
        Return 0
    End Function
    ' Mappings
    Friend Function GetAxisNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In Axis_hash.Keys
            tmp_list.Add(Axis_hash(id)(NAME_VARIABLE))
        Next
        Return tmp_list

    End Function

    Friend Function GetAxisDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In Axis_hash.Keys
            tmpHT(Axis_hash(id)(Key)) = Axis_hash(id)(Value)
        Next
        Return tmpHT

    End Function


    ' Utilities Methods
    Friend Function GetAxisId(ByRef name As String) As Int32

        For Each id As Int32 In Axis_hash.Keys
            If name = Axis_hash(id)(NAME_VARIABLE) Then Return id
        Next
        Return 0

    End Function

    Protected Sub GetAxisHTFromPacket(ByRef packet As ByteBuffer, ByRef axis_ht As Hashtable)

        axis_ht(ID_VARIABLE) = packet.ReadUint32()
        axis_ht(NAME_VARIABLE) = packet.ReadString()

    End Sub

    Protected Sub WriteAxisPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub

    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        If GetAxisNameList(NAME_VARIABLE).Contains(name) Then Return False
        Return True

    End Function


    ' Treeeviews Loading
    Friend Sub LoadAxisTV(ByRef TV As Windows.Forms.TreeView)

        TV.Nodes.Clear()
        TreeViewsUtilities.LoadTreeview(TV, Axis_hash)

    End Sub

    Friend Sub LoadAxisTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        TV.Nodes.Clear()
        For Each id As Int32 In Axis_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, Axis_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Friend Sub LoadAxisTree(ByRef DestNode As VIBlend.WinForms.Controls.vTreeNode)

        DestNode.Nodes.Clear()
        For Each id As Int32 In Axis_hash.Keys
            Dim newNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, Axis_hash(id)(NAME_VARIABLE), DestNode, 0)
            newNode.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub


    Friend Sub LoadAxisTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As Int32 In Axis_hash.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, Axis_hash(id)(NAME_VARIABLE), TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub




End Class
