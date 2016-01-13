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
' Last modified: 13/01/2016
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
                   ByRef p_axisType As AxisType)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        If m_controller.GetAxisType() = AxisType.Entities OrElse m_controller.GetAxisType = AxisType.Client Then
            GlobalVariables.AxisElems.LoadHierarchyAxisTree(m_controller.GetAxisType(), m_parentAxisElemTreeviewBox.TreeView)
        Else
            m_parentAxisLabel.Visible = False
            m_parentAxisElemTreeviewBox.Visible = False
        End If

        MultilanguageSetup(p_axisType)

    End Sub

    'Private Sub LoadCurrencies(ByRef currenciesHt As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

    '    For Each currency As Currency In currenciesHt.Values
    '        Dim LI As New VIBlend.WinForms.Controls.ListItem
    '        LI.Value = currency.Id
    '        LI.Text = currency.Name
    '        CurrenciesComboBox1.Items.Add(LI)
    '    Next

    'End Sub

    Friend Sub SetParentAxisId(ByRef p_parentAxisId As Int32)

        Dim parentAxisNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(m_parentAxisElemTreeviewBox.TreeView, p_parentAxisId)
        If parentAxisNode Is Nothing Then
            ' msg error
            Exit Sub
        End If
        m_parentAxisElemTreeviewBox.TreeView.SelectedNode = parentAxisNode

    End Sub

    Private Sub MultilanguageSetup(ByRef p_axisType As AxisType)

        Me.m_nameLabel.Text = Local.GetValue("general.name")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.CreateAxisBT.Text = Local.GetValue("general.create")

        Select Case p_axisType
            Case AxisType.Entities
                Me.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_entity")
                Me.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_entity_selection")
                Me.Text = Local.GetValue("axis_edition.new_entity")

            Case AxisType.Client
                Me.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_client")
                Me.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_client_selection")
                Me.Text = Local.GetValue("axis_edition.new_client")

            Case AxisType.Product
                Me.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_product")
                Me.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_product_selection")
                Me.Text = Local.GetValue("axis_edition.new_product")

            Case AxisType.Adjustment
                Me.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_adjustment")
                Me.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_adjustment_selection")
                Me.Text = Local.GetValue("axis_edition.new_adjustment")

            Case AxisType.Employee
                Me.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_employee")
                Me.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_employee_selection")
                Me.Text = Local.GetValue("axis_edition.new_employee")

        End Select

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

    Delegate Sub AxisNodeAddition_Delegate(ByRef p_axisId As Int32, _
                                               ByRef p_axisParentId As Int32, _
                                               ByRef p_axisName As String, _
                                               ByRef p_axisImage As Int32)
    Friend Sub AxisNodeAddition(ByRef p_AxisId As Int32, _
                                   ByRef p_axisParentId As Int32, _
                                   ByRef p_axisName As String, _
                                   ByRef p_axisImage As Int32)

        If Me.m_parentAxisElemTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New AxisNodeAddition_Delegate(AddressOf AxisNodeAddition)
            Me.m_parentAxisElemTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_AxisId, p_axisParentId, p_axisName, p_axisImage})
        Else
            If p_axisParentId = 0 Then
                Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_AxisId), p_axisName, m_parentAxisElemTreeviewBox.TreeView, p_axisImage)
                new_node.IsVisible = True
            Else
                Dim l_parentNode As vTreeNode = VTreeViewUtil.FindNode(m_parentAxisElemTreeviewBox.TreeView, p_axisParentId)
                If l_parentNode IsNot Nothing Then
                    Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_AxisId), p_axisName, l_parentNode, p_axisImage)
                    new_node.IsVisible = True
                End If
            End If
            m_parentAxisElemTreeviewBox.TreeView.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef p_axisId As Int32)
    Friend Sub TVNodeDelete(ByRef p_axis_id As Int32)

        If Me.m_parentAxisElemTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.m_parentAxisElemTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_axis_id})
        Else
            Dim l_axisNode As vTreeNode = VTreeViewUtil.FindNode(m_parentAxisElemTreeviewBox.TreeView, p_axis_id)
            If l_axisNode IsNot Nothing Then
                l_axisNode.Remove()
                m_parentAxisElemTreeviewBox.TreeView.Refresh()
            End If
        End If

    End Sub

#End Region


#Region "Call backs"

    Private Sub CreateAxisElemBT_Click(sender As Object, e As EventArgs) Handles CreateAxisBT.Click

        Dim l_newAxisName As String = NameTextBox.Text
        If IsFormValid(l_newAxisName) = True Then
            Dim l_parentAxisId As Int32 = 0
            If Not m_parentAxisElemTreeviewBox.TreeView.SelectedNode Is Nothing Then
                If m_controller.GetAxisType() = AxisType.Entities OrElse m_controller.GetAxisType = AxisType.Client Then
                    l_parentAxisId = m_parentAxisElemTreeviewBox.TreeView.SelectedNode.Value
                End If
            End If
            m_controller.CreateAxisElem(l_newAxisName, _
                                    l_parentAxisId, _
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

    Private Function IsFormValid(ByRef l_newAxisName As String) As Boolean

        If l_newAxisName = "" Then
            MsgBox(Local.GetValue("axis_edition.msg_entity_name"))
            Return False
        End If
        ' below -> check is on server priority normal
        'If names_list.Contains(new_entity_name) Then
        '    MsgBox("This Entity name is already in use. Please choose another one.")
        '    Return False
        'End If
        'If CurrenciesComboBox1.SelectedItem Is Nothing Then
        '    MsgBox(Local.GetValue("axis_edition.msg_select_currency"))
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