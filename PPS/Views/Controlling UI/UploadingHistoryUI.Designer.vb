<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UploadingHistoryUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadingHistoryUI))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UploadStateTB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TimeStampTB = New System.Windows.Forms.TextBox()
        Me.UploadStateBT = New System.Windows.Forms.Button()
        Me.LightsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ErrorMessageTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Last Upload State"
        '
        'UploadStateTB
        '
        Me.UploadStateTB.Enabled = False
        Me.UploadStateTB.Location = New System.Drawing.Point(154, 17)
        Me.UploadStateTB.Name = "UploadStateTB"
        Me.UploadStateTB.Size = New System.Drawing.Size(117, 20)
        Me.UploadStateTB.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(286, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Date and Time"
        '
        'TimeStampTB
        '
        Me.TimeStampTB.Enabled = False
        Me.TimeStampTB.Location = New System.Drawing.Point(378, 17)
        Me.TimeStampTB.Name = "TimeStampTB"
        Me.TimeStampTB.Size = New System.Drawing.Size(169, 20)
        Me.TimeStampTB.TabIndex = 3
        '
        'UploadStateBT
        '
        Me.UploadStateBT.FlatAppearance.BorderSize = 0
        Me.UploadStateBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UploadStateBT.ImageList = Me.LightsImageList
        Me.UploadStateBT.Location = New System.Drawing.Point(122, 17)
        Me.UploadStateBT.Name = "UploadStateBT"
        Me.UploadStateBT.Size = New System.Drawing.Size(19, 20)
        Me.UploadStateBT.TabIndex = 4
        Me.UploadStateBT.UseVisualStyleBackColor = True
        '
        'LightsImageList
        '
        Me.LightsImageList.ImageStream = CType(resources.GetObject("LightsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.LightsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.LightsImageList.Images.SetKeyName(0, "favicon(174).ico")
        Me.LightsImageList.Images.SetKeyName(1, "favicon(173).ico")
        '
        'ErrorMessageTB
        '
        Me.ErrorMessageTB.Location = New System.Drawing.Point(23, 82)
        Me.ErrorMessageTB.Multiline = True
        Me.ErrorMessageTB.Name = "ErrorMessageTB"
        Me.ErrorMessageTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ErrorMessageTB.Size = New System.Drawing.Size(523, 163)
        Me.ErrorMessageTB.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Error"
        '
        'Button1
        '
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.ImageKey = "favicon(187).ico"
        Me.Button1.ImageList = Me.ButtonsIL
        Me.Button1.Location = New System.Drawing.Point(466, 265)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 22)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "favicon(187).ico")
        '
        'UploadingHistoryUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 307)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ErrorMessageTB)
        Me.Controls.Add(Me.UploadStateBT)
        Me.Controls.Add(Me.TimeStampTB)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.UploadStateTB)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "UploadingHistoryUI"
        Me.Text = "Upload Status"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UploadStateTB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TimeStampTB As System.Windows.Forms.TextBox
    Friend WithEvents UploadStateBT As System.Windows.Forms.Button
    Friend WithEvents LightsImageList As System.Windows.Forms.ImageList
    Friend WithEvents ErrorMessageTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
End Class
