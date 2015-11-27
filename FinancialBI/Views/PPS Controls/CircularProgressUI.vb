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
Imports System.Windows.Forms


Partial Public Class CircularProgressUI
    Inherits Form


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '  Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        ' Me.AllowTransparency = True
        Me.TopMost = True
 
    End Sub

    Private Sub CircularProgressUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      
    End Sub

    Friend Sub SetmessageAndColor(ByRef inputCirclesColor As Drawing.Color, _
                                  ByRef str As String)

        Label1.Text = str
        CP.CircleColor = inputCirclesColor
        Me.Left = (GlobalVariables.APPS.Width - Me.Width) / 2
        Me.Top = (GlobalVariables.APPS.Height - Me.Height) / 2

    End Sub

    Private Sub lblClose_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Me.Tag = "Cancelled"
        Me.Hide()
    End Sub

    Public Sub SetThinkingBar(ByVal switchedOn As Boolean)
        If switchedOn Then
            ' start
            CP.Start()
        Else
            ' stop
            CP.Stop()
        End If
    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs)
      
    End Sub

 
End Class