' SettingMainUI.vb
'
' Display settings controls
'
'
' To do: 
'       - Implement connection and reinitialize password
'       - Need to initialize GLOBAL variables when connection reinitialized ? !!!
'
'
'
' Known bugs:
'
'
'
' Last modified: 07/07/2014
' Author: Julien Monnereau


Imports System.Drawing
Imports System.Windows.Forms



Public Class SettingMainUI


#Region "Initialize"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None


        ' Event handlers
        AddHandler Panel1.Paint, AddressOf Panel1_Paint
        AddHandler Panel1.MouseMove, AddressOf panel1_MouseMove
        AddHandler Panel1.MouseDown, AddressOf form_MouseDown
        AddHandler Panel1.MouseUp, AddressOf form_MouseUp


    End Sub



    Private Sub SettingMainUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ServerAddressTB.Text = SERVER_LOCATION
    End Sub


#End Region




#Region "Call backs"


#Region "Connection Tab"

    ' Call back Connection
    Private Sub ConnectionBT_Click(sender As Object, e As EventArgs) Handles ConnectionBT.Click

        ConnectioN = OpenConnection(IDTB.Text, PWDTB.Text)
        If Not ConnectioN Is Nothing Then
            SQLCredentials.SetGLOBALUserCredential()
            MsgBox("Connection succeeded")
        Else
            MsgBox("Connection failed")
        End If

    End Sub

    ' Call back Diconnection
    Private Sub DiconnectBT_Click(sender As Object, e As EventArgs) Handles DiconnectBT.Click

        ConnectioN.Close()
        ConnectioN = Nothing

    End Sub

    ' Call back Password reinit
    Private Sub ReinitPwdBT_Click(sender As Object, e As EventArgs) Handles ReinitPwdBT.Click

        Dim CHANGEPWD As New ChangePasswordUI
        CHANGEPWD.Show()

    End Sub

#End Region




#End Region




#Region "Events"


    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics
        Dim sText As String
        Dim iX As Integer
        Dim iY As Integer
        Dim sizeText As SizeF
        Dim ctlTab As TabControl

        ctlTab = CType(sender, TabControl)

        g = e.Graphics

        sText = ctlTab.TabPages(e.Index).Text
        sizeText = g.MeasureString(sText, ctlTab.Font)
        iX = e.Bounds.Left + 6
        iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2
        g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY)
    End Sub


#End Region



   
  
End Class