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
' Last modified: 05/12/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections



Public Class NewEntityUI


#Region "Instance Variables"

    ' Objects
    Private Controller As EntitiesController
  
    ' Variables
    Private entitiesTV As TreeView
    Private categoriesTV As TreeView
    Private categoriesNamesKeysDictionaries As Hashtable
    Private categoriesKeysNamesDictionaries As Hashtable
    Friend currenciesList As List(Of String)
    Private parentTB As New TextBox
    Private isFormExpanded As Boolean
    Private current_parent_entity_id As String

    ' Constants
    Private Const FIXED_ATTRIBUTES_NUMBER As Int32 = 3
    Private Const NAME_TEXT_EDITOR_NAME As String = "NameTB"
    Private Const NAME_LABEL_NAME As String = "NameLabel"
    Friend Const PARENT_TB_NAME As String = "parentTB"
    Private Const PARENT_BUTTON_NAME As String = "ParentNameBT"
    Private Const CURRENCIES_CB_NAME As String = "CurrenciesCB"
    Private Const CURRENCIES_LABEL_NAME As String = "Currency"
    Private Const ROWS_HEIGHT As Int32 = 24
    Private Const PARENT_BUTTON_WIDTH As Int32 = 75
    Private Const PARENT_BUTTON_HEIGHT As Int32 = 28

    ' Expansion Display Constants
    Private Const COLLAPSED_WIDTH As Int32 = 710
    Private Const EXPANDED_WIDTH As Int32 = 1175
    Private Const COLLAPSED_HEIGHT As Int32 = 500
    Private Const EXPANDED_HEIGHT As Int32 = 590

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_controller As EntitiesController, _
                   ByRef input_entities_TV As TreeView, _
                   ByRef input_categories_tree As TreeView, _
                   ByRef input_categories_name_key_dic As Hashtable, _
                   ByRef input_categories_key_name_dic As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        entitiesTV = input_entities_TV
        currenciesList = CurrenciesMapping.getCurrenciesList(CURRENCIES_KEY_VARIABLE)
        categoriesTV = input_categories_tree

        categoriesNamesKeysDictionaries = input_categories_name_key_dic
        categoriesKeysNamesDictionaries = input_categories_key_name_dic

        TableLayoutInit()
        InitializeDisplay()
        HideParentEntitiesTV()

    End Sub

    Private Sub NewEntityUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddEntitiesTV()
        If Not entitiesTV.SelectedNode Is Nothing Then current_parent_entity_id = entitiesTV.SelectedNode.Name Else current_parent_entity_id = ""

    End Sub

    Protected Friend Sub FillIn(ByRef parent_entity_name As String, ByRef attributes As Hashtable)

        parentTB.Text = parent_entity_name
        Me.Controls.Find(CURRENCIES_CB_NAME, True)(0).Text = attributes(ASSETS_CURRENCY_VARIABLE)

        For Each categoryNode As TreeNode In categoriesTV.Nodes
            Me.Controls.Find(categoryNode.Name + "CB", True)(0).Text = categoriesKeysNamesDictionaries(attributes(categoryNode.Name))
        Next

    End Sub


#Region "Display Init"

    Private Sub TableLayoutInit()

        For Each categoryNode As TreeNode In categoriesTV.Nodes
            TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(SizeType.Absolute, ROWS_HEIGHT))
        Next

        AddHandler entitiesTV.NodeMouseClick, AddressOf EntitiesTV_NodeMouseClick
        AddHandler entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown

    End Sub

    ' Initialize display (comboboxes and text editors)
    Private Sub InitializeDisplay()

        ' Entity Name
        Dim nameTextEditor As New TextBox
        nameTextEditor.Name = NAME_TEXT_EDITOR_NAME
        AddControl(0, 1, nameTextEditor)
        AddLabel(0, NAME_LABEL_NAME)

        ' Parent Entity -> 
        parentTB.Name = PARENT_TB_NAME
        parentTB.Enabled = False
        AddControl(1, 1, parentTB)

        ' Parent entity Button
        Dim parentEntityBT As New Button
        parentEntityBT.Name = PARENT_BUTTON_NAME
        parentEntityBT.Text = "Select"
        parentEntityBT.ImageList = ButtonsIL
        parentEntityBT.ImageIndex = 2
        parentEntityBT.TextAlign = Drawing.ContentAlignment.MiddleLeft
        parentEntityBT.ImageAlign = Drawing.ContentAlignment.MiddleRight
        AddControl(1, 0, parentEntityBT)
        parentEntityBT.Dock = DockStyle.None
        parentEntityBT.Width = PARENT_BUTTON_WIDTH

        AddHandler parentEntityBT.Click, AddressOf ParentEntitySelection_Click
        AddHandler parentEntityBT.Enter, AddressOf ParentEntitySelection_Click

        ' currencies -> Combobox
        Dim currenciesCombobox As New ComboBox
        currenciesCombobox.DropDownStyle = ComboBoxStyle.DropDownList
        currenciesCombobox.Name = CURRENCIES_CB_NAME
        For Each curr As String In currenciesList
            currenciesCombobox.Items.Add(curr)
        Next
        AddControl(2, 1, currenciesCombobox)
        AddLabel(2, CURRENCIES_LABEL_NAME)

        AddAttributesComboboxes()

    End Sub

    ' Attributes Dynamic controls Creation
    Private Sub AddAttributesComboboxes()

        Dim rowIndex As Int32 = 3
        For Each categoryNode As TreeNode In categoriesTV.Nodes

            ' Dim tmpHT As New Dictionary(Of String, String)
            Dim newCombobox As New ComboBox

            newCombobox.DropDownStyle = ComboBoxStyle.DropDownList
            newCombobox.Name = categoryNode.Name + "CB"

            For Each categoryValue As TreeNode In categoryNode.Nodes
                '     tmpHT.Add(categoryValue.Text, categoryValue.Name)
                newCombobox.Items.Add(categoryValue.Text)
            Next

            AddControl(rowIndex, 1, newCombobox)
            AddLabel(rowIndex, categoryNode.Text)
            ' categoriesNamesKeysDictionaries.Add(categoryNode.Name, tmpHT)
            rowIndex = rowIndex + 1
        Next
        AddLabel(rowIndex, "")


    End Sub

    ' Label creation
    Private Sub AddLabel(ByRef rowNb As Int32, ByRef labelName As String)

        Dim newLabel As New Label
        newLabel.Text = labelName
        newLabel.TextAlign = Drawing.ContentAlignment.MiddleLeft
        AddControl(rowNb, 0, newLabel)

    End Sub

    ' Add a control to the TableLayoutPanel1
    Private Sub AddControl(ByRef rowNb As Int32, ByRef colNb As Int32, ByRef control As System.Windows.Forms.Control)

        TableLayoutPanel1.Controls.Add(control)
        TableLayoutPanel1.SetRow(control, rowNb)
        TableLayoutPanel1.SetColumn(control, colNb)
        control.Dock = DockStyle.Fill

    End Sub


#End Region


#End Region


#Region "Call backs"

    Private Sub ParentEntitySelection_Click(sender As Object, e As EventArgs)

        DisplayParentEntitiesTV()

    End Sub

    Private Sub CreateEntityBT_Click(sender As Object, e As EventArgs) Handles CreateEntityBT.Click

        Dim new_entity_Name As String = Me.Controls.Find(NAME_TEXT_EDITOR_NAME, True)(0).Text
        If IsFormValid(new_entity_Name) = True Then

            Dim hash As New Hashtable
            hash.Add(ASSETS_NAME_VARIABLE, new_entity_Name)
            If current_parent_entity_id <> "" Then hash.Add(ASSETS_PARENT_ID_VARIABLE, current_parent_entity_id)
            hash.Add(ASSETS_CURRENCY_VARIABLE, Me.Controls.Find(CURRENCIES_CB_NAME, True)(0).Text)
            hash.Add(ASSETS_ALLOW_EDITION_VARIABLE, 1)

            For Each categoryNode As TreeNode In categoriesTV.Nodes
                Dim categoryValueText As String = Me.Controls.Find(categoryNode.Name + "CB", True)(0).Text
                hash.Add(categoryNode.Name, categoriesNamesKeysDictionaries(categoryValueText))
            Next

            Controller.CreateEntity(hash, entitiesTV.SelectedNode)
            Me.Hide()
            Controller.ShowEntitiesMGT()
        Else

        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()
        Controller.ShowEntitiesMGT()

    End Sub


#End Region


#Region "Checks"

    Private Function IsFormValid(ByRef new_entity_name As String) As Boolean

            Dim names_list = cTreeViews_Functions.GetNodesTextsList(entitiesTV)
            If names_list.Contains(new_entity_Name) Then
                MsgBox("This Entity name is already in use. Please choose another one.")
                Return False
            End If
        Return True

    End Function

#End Region


#Region "Utilities"

    Private Sub NewEntityUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Hide()
        e.Cancel = True
        Controller.ShowEntitiesMGT()

    End Sub

    Protected Friend Sub AddEntitiesTV()

        Panel1.Controls.Add(entitiesTV)
        entitiesTV.Dock = DockStyle.Fill

    End Sub

#Region "Parents Accounts Treeview Utilities"

    Private Sub DisplayParentEntitiesTV()

        Me.Width = EXPANDED_WIDTH
        Me.Height = EXPANDED_HEIGHT
        isFormExpanded = True

    End Sub

    Private Sub HideParentEntitiesTV()

        Me.Width = COLLAPSED_WIDTH
        Me.Height = COLLAPSED_HEIGHT
        isFormExpanded = False

    End Sub

    Private Sub EntitiesTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        Select Case e.Node.Nodes.Count
            Case 0
                parentTB.Text = e.Node.Text
                current_parent_entity_id = e.Node.Name
                HideParentEntitiesTV()
        End Select
       
    End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter _
        AndAlso Not EntitiesTV.SelectedNode Is Nothing Then
            parentTB.Text = entitiesTV.SelectedNode.Text
            current_parent_entity_id = entitiesTV.SelectedNode.Name
            HideParentEntitiesTV()
        End If


    End Sub

#End Region



#End Region


    
   
End Class