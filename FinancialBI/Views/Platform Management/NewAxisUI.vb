' NewEntity.vb
'
' New entity form
'
'   To do:
'       
'
'   Known bugs:
'       - if new entity categories not filled in issue !!! 
'           -> should have "" and "NS" for each category (hence name-> keys dict in categories should be for each categories)
'
'
' Last modified: 16/11/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class NewAxisUI


#Region "Instance Variables"

    ' Objects
    Private m_controller As AxisController

    ' Constants
    Private Const FIXED_ATTRIBUTES_NUMBER As Int32 = 3

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_currenciesHT As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        If m_controller.GetAxisType() = AxisType.Entities OrElse m_controller.GetAxisType = AxisType.Client Then
            GlobalVariables.AxisElems.LoadAxisTree(m_controller.GetAxisType(), m_parentAxisElemTreeviewBox.TreeView)
        Else
            m_parentEntityLabel.Visible = False
            m_parentAxisElemTreeviewBox.Visible = False
        End If
        MultilanguageSetup()

    End Sub

    'Private Sub LoadCurrencies(ByRef currenciesHt As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

    '    For Each currency As Currency In currenciesHt.Values
    '        Dim LI As New VIBlend.WinForms.Controls.ListItem
    '        LI.Value = currency.Id
    '        LI.Text = currency.Name
    '        CurrenciesComboBox1.Items.Add(LI)
    '    Next

    'End Sub

    Friend Sub SetParentEntityId(ByRef parentEntityId As Int32)

        Dim parentEntityNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(m_parentAxisElemTreeviewBox.TreeView, parentEntityId)
        If parentEntityNode Is Nothing Then
            ' msg error
            Exit Sub
        End If
        m_parentAxisElemTreeviewBox.TreeView.SelectedNode = parentEntityNode

    End Sub

    Private Sub MultilanguageSetup()

        Me.m_parentEntityLabel.Text = Local.GetValue("entities_edition.parent_entity")
        Me.m_nameLabel.Text = Local.GetValue("general.name")
        Me.m_parentAxisElemTreeviewBox.Text = Local.GetValue("entities_edition.parent_entity_selection")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.CreateEntityBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("entities_edition.new_entity")

    End Sub

#End Region


#Region "Interface"

    Delegate Sub TVUpdate_Delegate(ByRef p_node As vTreeNode, _
                             ByRef p_axisElemName As String, _
                             ByRef p_axisElemImage As Int32)
    Friend Sub TVUpdate(ByRef p_node As vTreeNode, _
                        ByRef p_axisElemName As String, _
                        ByRef p_axisElemImage As Int32)

        If Me.m_parentAxisElemTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New TVUpdate_Delegate(AddressOf TVUpdate)
            Me.m_parentAxisElemTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_node, p_axisElemName, p_axisElemImage})
        Else
            p_node.Text = p_axisElemName
            p_node.ImageIndex = p_axisElemImage
            m_parentAxisElemTreeviewBox.TreeView.Refresh()
        End If

    End Sub

    Delegate Sub entityNodeAddition_Delegate(ByRef p_entityId As Int32, _
                                               ByRef p_entityParentId As Int32, _
                                               ByRef p_entityName As String, _
                                               ByRef p_entityImage As Int32)
    Friend Sub entityNodeAddition(ByRef p_entityId As Int32, _
                                   ByRef p_entityParentId As Int32, _
                                   ByRef p_entityName As String, _
                                   ByRef p_entityImage As Int32)

        If Me.m_parentAxisElemTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New entityNodeAddition_Delegate(AddressOf entityNodeAddition)
            Me.m_parentAxisElemTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_entityId, p_entityParentId, p_entityName, p_entityImage})
        Else
            If p_entityParentId = 0 Then
                Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_entityId), p_entityName, m_parentAxisElemTreeviewBox.TreeView, p_entityImage)
                new_node.IsVisible = True
            Else
                Dim l_parentNode As vTreeNode = VTreeViewUtil.FindNode(m_parentAxisElemTreeviewBox.TreeView, p_entityParentId)
                If l_parentNode IsNot Nothing Then
                    Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_entityId), p_entityName, l_parentNode, p_entityImage)
                    new_node.IsVisible = True
                End If
            End If
            m_parentAxisElemTreeviewBox.TreeView.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef p_entity_id As Int32)
    Friend Sub TVNodeDelete(ByRef p_entity_id As Int32)

        If Me.m_parentAxisElemTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.m_parentAxisElemTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_entity_id})
        Else
            Dim l_entityNode As vTreeNode = VTreeViewUtil.FindNode(m_parentAxisElemTreeviewBox.TreeView, p_entity_id)
            If l_entityNode IsNot Nothing Then
                l_entityNode.Remove()
                m_parentAxisElemTreeviewBox.TreeView.Refresh()
            End If
        End If

    End Sub

#End Region


#Region "Call backs"

    Private Sub CreateAxisElemBT_Click(sender As Object, e As EventArgs) Handles CreateEntityBT.Click

        Dim new_entity_Name As String = NameTextBox.Text
        If IsFormValid(new_entity_Name) = True Then
            Dim parentEntityId As Int32 = 0
            If Not m_parentAxisElemTreeviewBox.TreeView.SelectedNode Is Nothing Then
                parentEntityId = m_parentAxisElemTreeviewBox.TreeView.SelectedNode.Value
            End If
            m_controller.CreateAxisElem(new_entity_Name, _
                                    parentEntityId, _
                                    1, _
                                    1)
            Me.Hide()
            m_controller.ShowEntitiesMGT()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()
        m_controller.ShowEntitiesMGT()

    End Sub

#End Region


#Region "Utilities"

    Private Function IsFormValid(ByRef new_entity_name As String) As Boolean

        If new_entity_name = "" Then
            MsgBox(Local.GetValue("entities_edition.msg_entity_name"))
            Return False
        End If
        ' below -> check is on server priority normal
        'If names_list.Contains(new_entity_name) Then
        '    MsgBox("This Entity name is already in use. Please choose another one.")
        '    Return False
        'End If
        'If CurrenciesComboBox1.SelectedItem Is Nothing Then
        '    MsgBox(Local.GetValue("entities_edition.msg_select_currency"))
        '    Return False
        'End If
        Return True

    End Function

    Private Sub NewAxisElemUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Hide()
        e.Cancel = True
        m_controller.ShowEntitiesMGT()

    End Sub


#End Region


End Class