Imports System.Linq

Public Class Snapshot_SelectionUI

    Private snapshotUI As SnapShotUI

    Private Sub Snapshot_SelectionUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '---------------------------------------------------------------------------------
        ' Set up the user form with toggle buttons corresponding to the Headers 
        '---------------------------------------------------------------------------------
        snapshotUI = Me.Owner
        Dim index As Integer
        Const LABELS_HEIGHT As Double = 27
        Const LABELS_WIDTH As Double = 342
        Const LABELS_LEFT As Double = 14
        Const TOP As Double = 25
        Const BUTTON1_LEFT As Double = 380
        Const BUTTON_HEIGHT As Double = LABELS_HEIGHT
        Const BUTTON_WIDTH As Double = 72
        Const BUTTON_SPACE As Double = 3
        Const LINE_SPACE_UP As Double = 20
        Const LINE_SPACE_DOWN As Double = 10
        Const LINE_WIDTH As Double = 1

        For Each key As String In Me.snapshotUI.pHeadersArray.Keys

            Dim L1 As New Windows.Forms.Label                           ' Label
            L1.Name = "L1" & INDEX_SEPARATOR & index
            L1.Top = TOP + (BUTTON_HEIGHT + LINE_SPACE_UP + LINE_SPACE_DOWN + LINE_WIDTH) * index
            L1.Left = LABELS_LEFT
            L1.Height = LABELS_HEIGHT
            L1.Width = LABELS_WIDTH
            L1.Text = Me.snapshotUI.pHeadersArray.Item(key)
            Me.Panel1.Controls.Add(L1)

            Dim TB1 As New Windows.Forms.CheckBox                       ' Toggle Button 1
            TB1.Appearance = Windows.Forms.Appearance.Button
            TB1.Name = "TB1" & INDEX_SEPARATOR & index
            TB1.Top = TOP + (BUTTON_HEIGHT + LINE_SPACE_UP + LINE_SPACE_DOWN + LINE_WIDTH) * index
            TB1.Left = BUTTON1_LEFT
            TB1.Height = BUTTON_HEIGHT
            TB1.Width = BUTTON_WIDTH
            TB1.Text = "Select"
            TB1.Checked = Me.snapshotUI.pHeadersSelection.Item(key)
            TB1.BackColor = Drawing.Color.AliceBlue
            TB1.UseVisualStyleBackColor = True
            TB1.TextAlign = Drawing.ContentAlignment.MiddleCenter
            Me.Panel1.Controls.Add(TB1)

            Dim TB2 As New Windows.Forms.CheckBox                       ' Toggle Button 2
            TB2.Appearance = Windows.Forms.Appearance.Button
            TB2.Name = "TB2" & INDEX_SEPARATOR & index
            TB2.Top = TOP + (BUTTON_HEIGHT + LINE_SPACE_UP + LINE_SPACE_DOWN + LINE_WIDTH) * index
            TB2.Left = BUTTON1_LEFT + BUTTON_WIDTH + BUTTON_SPACE
            TB2.Height = BUTTON_HEIGHT
            TB2.Width = BUTTON_WIDTH
            TB2.Text = "Unselect"
            TB2.BackColor = Drawing.Color.AliceBlue
            TB2.UseVisualStyleBackColor = True
            If TB1.Checked = True Then
                TB2.Checked = False
            Else
                TB2.Checked = True
            End If
            TB2.TextAlign = Drawing.ContentAlignment.MiddleCenter
            Me.Panel1.Controls.Add(TB2)

            Dim Line1 As New System.Windows.Forms.Label                             ' Line
            Line1.Location = New System.Drawing.Point(5, _
                                                       TOP + (BUTTON_HEIGHT + LINE_SPACE_UP) * (index + 1) + LINE_SPACE_DOWN * index)
            Line1.Size = New System.Drawing.Size(LABELS_WIDTH + BUTTON_WIDTH * 2.5 + BUTTON_SPACE * 3 + 5, LINE_WIDTH)
            Line1.BorderStyle = Windows.Forms.BorderStyle.None
            Line1.BackColor = System.Drawing.Color.Black
            Line1.Text = ""
            Me.Panel1.Controls.Add(Line1)

            index = index + 1
        Next

        InitializeClickHandlers(Panel1)

    End Sub

    Private Sub ControlsClick(sender As Object, e As EventArgs)

        '------------------------------------------------------------------------------
        ' Changes selections according to the trigger
        '------------------------------------------------------------------------------
        Dim TBName As String = sender.name
        Dim index As Integer

        If TypeOf sender Is Windows.Forms.CheckBox Then
            index = GetIndex(TBName)
            If TBName.Contains("TB1") Then
                Dim TB2 As Windows.Forms.CheckBox = Panel1.Controls("TB2" & INDEX_SEPARATOR & index)
                If sender.checked = True Then
                    TB2.Checked = False
                    UpdateHeadersSel(True, index)
                Else
                    TB2.Checked = True
                    UpdateHeadersSel(False, index)
                End If
            ElseIf TBName.Contains("TB2") Then
                Dim TB1 As Windows.Forms.CheckBox = Panel1.Controls("TB1" & INDEX_SEPARATOR & index)
                If sender.checked = True Then
                    TB1.Checked = False
                    UpdateHeadersSel(False, index)
                Else
                    TB1.Checked = True
                    UpdateHeadersSel(True, index)
                End If
            End If
        End If

    End Sub

    Private Function GetIndex(name As String) As Integer

        '------------------------------------------------------------------------------
        ' Returns the index of the specified toggle button (index seperated by INDEX_SEPARATOR)
        '------------------------------------------------------------------------------
        GetIndex = CInt(Microsoft.VisualBasic.Right(name, Len(name) - InStr(name, INDEX_SEPARATOR)))

    End Function

    Private Sub InitializeClickHandlers(sender As Windows.Forms.Control, Optional bChilds As Boolean = True)

        '---------------------------------------------------------------------------------------------------
        ' Add a click handler (ControlsClick method) to each control in the specified control
        '---------------------------------------------------------------------------------------------------
        For Each elem As Windows.Forms.Control In sender.Controls
            AddHandler elem.Click, AddressOf ControlsClick              ' Add the control click.handler
            If bChilds AndAlso elem.Controls.Count > 0 Then
                Call InitializeClickHandlers(sender)
            End If
        Next
    End Sub

    Private Sub Cmd_SelectAll_Click(sender As Object, e As EventArgs) Handles Cmd_SelectAll.Click

        '-----------------------------------------------------------------------------------------
        ' Select all Headers
        '-----------------------------------------------------------------------------------------
        For Each control As Windows.Forms.CheckBox In Panel1.Controls.OfType(Of Windows.Forms.CheckBox)()
            If control.Name.Contains("TB1") Then
                control.Checked = True
            ElseIf control.Name.Contains("TB2") Then
                control.Checked = False
            End If
        Next
        For Each key As String In snapshotUI.pHeadersSelection.Keys
            snapshotUI.pHeadersSelection.Item(key) = True
        Next

    End Sub

    Private Sub Cmd_Close_Click(sender As Object, e As EventArgs) Handles Cmd_Close.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub UpdateHeadersSel(Checked As Boolean, index As Integer)

        '--------------------------------------------------------------------------------
        ' Update Headers Selection in snapshot UI when TB clicked
        '--------------------------------------------------------------------------------
        Dim key As String = Me.snapshotUI.pHeadersSelection.ElementAt(index).Key
        Me.snapshotUI.pHeadersSelection.Item(key) = Checked
    End Sub


End Class