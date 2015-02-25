' CircularProgressUI.vb
'
' Display circular progress
' 
' To do:
'       - 
'
' Known bugs:
'       -
'
'
'
'
' Last modified: 29/12/2014
' Author: Julien Monnereau



Imports ProgressControls


Public Class CircularProgressUI


    Public Sub New(ByRef inputCirclesColor As Drawing.Color, _
                   ByRef str As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        ' Me.AllowTransparency = True
        Me.TopMost = True

        Label1.Text = str
        CP.CircleColor = inputCirclesColor


    End Sub

    Private Sub CircularProgressUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (GlobalVariables.apps.Width - Me.Width) / 2
        Me.Top = (GlobalVariables.apps.Height - Me.Height) / 2
        Label1.Left = (Me.Width - Label1.Width) / 2
        CP.Start()

    End Sub

End Class