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
' Last modified: 04/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class NewEntityUI


#Region "Instance Variables"

    ' Objects
    Private Controller As EntitiesController
    Private entitiesTV As vTreeView

    ' Variables
    Private parentTB As New TextBox
    Private isFormExpanded As Boolean

    ' Constants
    Private Const FIXED_ATTRIBUTES_NUMBER As Int32 = 3

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As EntitiesController, _
                   ByRef p_entitiesTV As vTreeView, _
                   ByRef p_currenciesHT As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = p_controller
        entitiesTV = p_entitiesTV
        LoadCurrencies(p_currenciesHT)

    End Sub

    Private Sub LoadCurrencies(ByRef currenciesHt As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))

        For Each currency As Currency In currenciesHt.Values
            Dim LI As New VIBlend.WinForms.Controls.ListItem
            LI.Value = currency.Id
            LI.Text = currency.Name
            CurrenciesComboBox1.Items.Add(LI)
        Next

    End Sub

    Friend Sub SetParentEntityId(ByRef parentEntityId As Int32)

        Dim parentEntityNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(ParentEntityTreeViewBox.TreeView, parentEntityId)
        ParentEntityTreeViewBox.TreeView.SelectedNode = parentEntityNode

    End Sub

    Private Sub NewEntityUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        VTreeViewUtil.LoadParentsTreeviewBox(ParentEntityTreeViewBox, entitiesTV)

    End Sub

#End Region


#Region "Call backs"

    Private Sub CreateEntityBT_Click(sender As Object, e As EventArgs) Handles CreateEntityBT.Click

        Dim new_entity_Name As String = NameTextBox.Text
        If IsFormValid(new_entity_Name) = True Then
            Dim parentEntityId As Int32 = 0
            If Not ParentEntityTreeViewBox.TreeView.SelectedNode Is Nothing Then
                parentEntityId = ParentEntityTreeViewBox.TreeView.SelectedNode.Value
            End If
            Controller.CreateEntity(new_entity_Name, _
                                    CurrenciesComboBox1.SelectedItem.Value, _
                                    parentEntityId, _
                                    1, _
                                    1)
            Me.Hide()
            Controller.ShowEntitiesMGT()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()
        Controller.ShowEntitiesMGT()

    End Sub

#End Region


#Region "Utilities"

    Private Function IsFormValid(ByRef new_entity_name As String) As Boolean

        If new_entity_name = "" Then
            MsgBox("Please enter a Name for the New Entity")
        End If
        ' below -> check is on server 
        'If names_list.Contains(new_entity_name) Then
        '    MsgBox("This Entity name is already in use. Please choose another one.")
        '    Return False
        'End If
        If CurrenciesComboBox1.SelectedItem Is Nothing Then
            MsgBox("A currency must be selected.")
            Return False
        End If
        Return True

    End Function

    Private Sub NewEntityUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Hide()
        e.Cancel = True
        Controller.ShowEntitiesMGT()

    End Sub


#End Region


End Class