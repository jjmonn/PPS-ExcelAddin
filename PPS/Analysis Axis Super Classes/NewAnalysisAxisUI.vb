' NewAnalysisAxisUI.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 22/04/2015

Imports System.Windows.Forms


Friend Class NewAnalysisAxisUI


#Region "Instance Variables"

    ' Objects
    Private categoriesTV As TreeView
    Protected Friend nameTextEditor As TextBox

    ' Constants
    Private Const NAME_TEXT_EDITOR_NAME As String = "NameTB"
    Private Const ROWS_HEIGHT As Int32 = 24


#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef analysis_axis_text As String, _
                             ByRef input_categoriesTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        categoriesTV = input_categoriesTV
        Me.Text = analysis_axis_text
        intializeUI()

    End Sub

#End Region


#Region "Utilities"

    ' Attributes Dynamic controls Creation
    Private Sub intializeUI()

        ' Analysis Axis Name
        nameTextEditor = New TextBox
        AddControl(0, 1, nameTextEditor)
        AddLabel(0, "Name")

        Dim rowIndex As Int32 = 3
        For Each categoryNode As TreeNode In categoriesTV.Nodes

            TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(SizeType.Absolute, ROWS_HEIGHT))

            Dim newCombobox As New ComboBox
            newCombobox.DropDownStyle = ComboBoxStyle.DropDownList
            newCombobox.Name = categoryNode.Name + "CB"

            For Each categoryValue As TreeNode In categoryNode.Nodes
                newCombobox.Items.Add(categoryValue.Text)
            Next

            AddControl(rowIndex, 1, newCombobox)
            AddLabel(rowIndex, categoryNode.Text)
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


#Region "Events"

    Private Sub UI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Hide()
        e.Cancel = True

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()

    End Sub

#End Region



 
End Class