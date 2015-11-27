'' NewUserUI.vb
''
'' Display user name, credential type and conso level choices for a new user
''
''
'' to do:
''       - CREATE DB USER/ send pwd -> all or nothing
''       - choose entityt location doesn't work
''       
''       
''
'' Known bugs
''
''
'' Author: Julien Monnereau
'' Last modified: 25/06/2015


'Imports System.Windows.Forms
'Imports System.Drawing
'Imports System.Net.Mail
'Imports VIBlend.WinForms.DataGridView



'Friend Class NewUserUI


'#Region "Instance Variables"

'    ' Objects
'    Private CONTROLLER As UsersController
'    Private EntitySelection As New EntitySelectionForUsersMGT(Me)

'    ' Variables
'    Friend parent_item As HierarchyItem
'    Private entityID As String
'    Private entityName As String
'    Private toolTip As New ToolTip()

'#End Region


'#Region "Initialization"

'    Protected Friend Sub New(ByRef input_controller As UsersController)

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        CONTROLLER = input_controller
'        Me.AddOwnedForm(EntitySelection)
'        InitCredentialTypesCB()
'        toolTip.SetToolTip(emailLabel, "User's Email address is used to send the password")

'    End Sub

'    Private Sub InitCredentialTypesCB()

'        'Dim credentials_types_list = CredentialsTypesMapping.GetCredentialsTypesList(CREDENTIALS_DESCRIPTION_VARIABLE)
'        'For Each Type As String In credentials_types_list
'        '    CredentialTypeCB.Items.Add(Type)
'        'Next
'        'CredentialTypeCB.Text = DEFAULT_CREDENTIAL_TYPE

'    End Sub


'#End Region


'#Region "Call backs"


'    Private Sub CreateBT_Click(sender As Object, e As EventArgs) Handles CreateBT.Click

'        Try
'            Dim address As MailAddress = New MailAddress(emailTB.Text)
'        Catch ex As Exception
'            MsgBox("The user's Email address is not valid." + Chr(13) + "A valid Email address is needed")
'            Exit Sub
'        End Try

'        Dim user_id As String = UserIDTB.Text
'        If CONTROLLER.IsUserNameAlreadyInUse(UserIDTB.Text) = True Then
'            MsgBox("This name already exists, please enter another name")
'            Exit Sub
'        ElseIf Len(user_id) > USERS_ID_MAX_SIZE Then
'            MsgBox("The User cannot exceed " + USERS_ID_MAX_SIZE + " characters.")
'            Exit Sub
'        End If

'        If entityID = "" Then
'            MsgBox("An Credential Level must be selected.")
'            Exit Sub
'        Else
'            If EntitySelection.entitiesTV.Nodes.Find(entityID, True).Length = 0 Then
'                MsgBox("The Credential Level is not valid.")
'                Exit Sub
'            End If
'        End If
'        If CredentialTypeCB.Text = "" Then
'            MsgBox("A Credential Type must be selected.")
'            Exit Sub
'        End If

'        '  CONTROLLER.CreateUser(user_id, entityID, CredentialTypeCB.Text, emailTB.Text, parent_item)
'        Me.Hide()

'    End Sub

'    ' Show Entity Credential Selection
'    Private Sub ChooseEntityBT_Click(sender As Object, e As EventArgs) Handles ChooseEntityBT.Click

'        EntitySelection.Left = ChooseEntityBT.Left + Me.Left
'        EntitySelection.Top = ChooseEntityBT.Top + Me.Top
'        EntitySelection.Show()

'    End Sub

'    ' Trigered by ENTSEL
'    Public Sub ValidateNewEntity(ByRef inputEntityName As String, ByRef inputEntityID As String)

'        If inputEntityID <> "" Then ConsoEntityTB.Text = inputEntityName
'        entityID = inputEntityID
'        entityName = inputEntityName

'    End Sub


'#End Region


'#Region "Events"

'    Private Sub ConsoEntityTB_Enter(sender As Object, e As EventArgs) Handles ConsoEntityTB.Enter

'        ChooseEntityBT_Click(sender, e)

'    End Sub

'    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

'        Me.Hide()
'        'CONTROLLER.ShowUsersMGTUI()

'    End Sub

'    Private Sub NewUserUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

'        e.Cancel = True
'        Me.Hide()
'        'CONTROLLER.ShowUsersMGTUI()

'    End Sub

'#End Region



'End Class