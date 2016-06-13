Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports VIBlend.WinForms.Controls

Public Class ProcessSelectionTaskPane

    Private m_processesTreeview As New vTreeView

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        InitializeComponent()
        VTreeViewUtil.InitTVFormat(m_processesTreeview)
        TableLayoutPanel1.Controls.Add(m_processesTreeview, 0, 1)
        m_processesTreeview.Dock = Windows.Forms.DockStyle.Fill
        AddHandler m_processesTreeview.MouseDoubleClick, AddressOf ProcessTV_MouseDoubleClick
        AddHandler m_processesTreeview.KeyPress, AddressOf ProcessTV_KeyPress
    End Sub

    Private Sub ProcessSelectionTaskPane_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeProcessChoices()
        InitializeMultiLanguage()
    End Sub

    Private Sub InitializeProcessChoices()
        m_processesTreeview.Nodes.Clear()
        VTreeViewUtil.AddNode(CRUD.Account.AccountProcess.FINANCIAL, Local.GetValue("process.process_financial"), m_processesTreeview)
        VTreeViewUtil.AddNode(CRUD.Account.AccountProcess.RH, Local.GetValue("process.process_rh"), m_processesTreeview)
    End Sub

    Private Sub InitializeMultiLanguage()

        m_processSelectionLabel.Text = Local.GetValue("process.select_process")
        m_validateButton.Text = Local.GetValue("general.validate")
        Me.Text = Local.GetValue("process.process_selection")

    End Sub

#End Region

#Region "Events"

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.ProcessSelectionTaskPaneVisible
    End Sub

    Private Sub ProcessTV_MouseDoubleClick(sender As Object, e As Windows.Forms.MouseEventArgs)
        Dim l_node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_processesTreeview, e.Location)
        If l_node IsNot Nothing Then
            AddinModule.SetCurrentProcessId(l_node.Value)
            Me.Hide()
        End If

    End Sub

    Private Sub ProcessTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = ChrW(Windows.Forms.Keys.Return) Then
            If m_processesTreeview.SelectedNode IsNot Nothing Then
                AddinModule.SetCurrentProcessId(m_processesTreeview.SelectedNode.Value)
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub ProcessSelectionTaskPane_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click
        If m_processesTreeview.SelectedNode IsNot Nothing Then
            AddinModule.SetCurrentProcessId(m_processesTreeview.SelectedNode.Value)
            Me.Hide()
        End If
    End Sub

#End Region



  

End Class
