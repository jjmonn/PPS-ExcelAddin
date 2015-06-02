<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FModelingInputsControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FModelingInputsControl))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VersionsTVpanel = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.EntitiesTVPanel = New System.Windows.Forms.Panel()
        Me.EntityTB = New System.Windows.Forms.TextBox()
        Me.LaunchConsoBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.55141!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.69576!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.75282!))
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsTVpanel, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTB, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesTVPanel, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.EntityTB, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LaunchConsoBT, 5, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.90172!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.098282!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(473, 418)
        Me.TableLayoutPanel1.TabIndex = 12
        '
        'VersionsTVpanel
        '
        Me.VersionsTVpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionsTVpanel.Location = New System.Drawing.Point(8, 59)
        Me.VersionsTVpanel.Name = "VersionsTVpanel"
        Me.VersionsTVpanel.Size = New System.Drawing.Size(129, 293)
        Me.VersionsTVpanel.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 36)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "1. Select the Input Version"
        '
        'VersionTB
        '
        Me.VersionTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionTB.Location = New System.Drawing.Point(8, 358)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(129, 20)
        Me.VersionTB.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(163, 36)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "2. Select the Entity Level to work on"
        '
        'EntitiesTVPanel
        '
        Me.EntitiesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTVPanel.Location = New System.Drawing.Point(163, 59)
        Me.EntitiesTVPanel.Name = "EntitiesTVPanel"
        Me.EntitiesTVPanel.Size = New System.Drawing.Size(133, 293)
        Me.EntitiesTVPanel.TabIndex = 9
        '
        'EntityTB
        '
        Me.EntityTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntityTB.Location = New System.Drawing.Point(163, 358)
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.Size = New System.Drawing.Size(133, 20)
        Me.EntityTB.TabIndex = 15
        '
        'LaunchConsoBT
        '
        Me.LaunchConsoBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.LaunchConsoBT.FlatAppearance.BorderSize = 0
        Me.LaunchConsoBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace
        Me.LaunchConsoBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LaunchConsoBT.ImageKey = "1420498403_340208.ico"
        Me.LaunchConsoBT.ImageList = Me.ButtonsImageList
        Me.LaunchConsoBT.Location = New System.Drawing.Point(319, 355)
        Me.LaunchConsoBT.Margin = New System.Windows.Forms.Padding(0)
        Me.LaunchConsoBT.Name = "LaunchConsoBT"
        Me.LaunchConsoBT.Size = New System.Drawing.Size(82, 26)
        Me.LaunchConsoBT.TabIndex = 3
        Me.LaunchConsoBT.Text = "Validate"
        Me.LaunchConsoBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LaunchConsoBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'VersionsTVIcons
        '
        Me.VersionsTVIcons.ImageStream = CType(resources.GetObject("VersionsTVIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersionsTVIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.VersionsTVIcons.Images.SetKeyName(0, "DB Grey.png")
        Me.VersionsTVIcons.Images.SetKeyName(1, "favicon(81).ico")
        Me.VersionsTVIcons.Images.SetKeyName(2, "icons-blue.png")
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "favicon(110).ico")
        '
        'FModelingInputsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "FModelingInputsControl"
        Me.Size = New System.Drawing.Size(473, 418)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VersionsTVpanel As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents EntitiesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents EntityTB As System.Windows.Forms.TextBox
    Friend WithEvents LaunchConsoBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList

End Class
