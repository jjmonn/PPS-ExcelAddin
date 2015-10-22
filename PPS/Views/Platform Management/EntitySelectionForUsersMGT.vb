'' EntitySelectionForUsersMGT.vb
''
'' Entities Treeview for the selection and holds the key | names dictionary
''
''
'' To do:
''
''
''
'' Known bugs :
''
'' Last modified: 04/12/2014
'' Author: Julien Monnereau


'Imports VIBlend.WinForms.DataGridView
'Imports System.Drawing
'Imports System.Windows.Forms


'Public Class EntitySelectionForUsersMGT

'#Region "Instance variables and constants"

'    Private PARENTOBJECT_ As Object
'    Private Const OPACITY_LEVEL As Double = 1

'#End Region


'#Region "Initialize"

'    Public Sub New(ByRef input_parent As Object)

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        entitiesTV.ImageList = EntitiesTVImageList
'        Globalvariables.Entities.LoadEntitiesTV(entitiesTV)
'        TreeViewsUtilities.set_TV_basics_icon_index(entitiesTV)
'        entitiesTV.CollapseAll()
'        PARENTOBJECT_ = input_parent

'    End Sub

'    Private Sub EntitySelectionForUsersMGT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'        Me.TopMost = True
'        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
'        Me.Opacity = OPACITY_LEVEL

'    End Sub


'#End Region


'#Region "Events"

'    Private Sub entitiesTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs) Handles entitiesTV.NodeMouseDoubleClick

'        PARENTOBJECT_.ValidateNewEntity(entitiesTV.SelectedNode.Text, entitiesTV.SelectedNode.Name)
'        Me.Hide()

'    End Sub

'    Private Sub entitiesTV_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles entitiesTV.KeyDown

'        Select Case e.KeyCode
'            Case Keys.Enter
'                PARENTOBJECT_.ValidateNewEntity(entitiesTV.SelectedNode.Text, entitiesTV.SelectedNode.Name)
'                Me.Hide()
'            Case Keys.Escape
'                Me.Hide()
'        End Select

'    End Sub

'#End Region







'End Class