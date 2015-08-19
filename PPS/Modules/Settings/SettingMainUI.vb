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
' Last modified: 18/08/2015
' Author: Julien Monnereau


Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic



Friend Class SettingMainUI


#Region "Initialize"

    Protected Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        

    End Sub

    Private Sub SettingMainUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ServerAddressTB.Text = My.Settings.serverIp
        PortTB.Text = My.Settings.port_number
        IDTB.Text = My.Settings.user
      

    End Sub

   
#End Region


#Region "Call backs"

    Private Sub CloseBT_Click(sender As Object, e As EventArgs)

        Me.Dispose()

    End Sub

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

 
    Private Sub PortTB_TextChanged(sender As Object, e As EventArgs) Handles PortTB.TextChanged

        My.Settings.port_number = PortTB.Text
        My.Settings.Save()

    End Sub

    Private Sub IDTB_TextChanged(sender As Object, e As EventArgs) Handles IDTB.TextChanged

        My.Settings.user = IDTB.Text
        My.Settings.Save()

    End Sub

    Private Sub ServerAddressTB_TextChanged(sender As Object, e As EventArgs) Handles ServerAddressTB.TextChanged

        My.Settings.serverIp = ServerAddressTB.Text
        My.Settings.Save()

    End Sub

    Private Sub databasesCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles databasesCB.SelectedIndexChanged

        My.Settings.database = databasesCB.text
        My.Settings.Save()

    End Sub


#End Region




  
End Class