<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessSelectionTaskPane
    Inherits AddinExpress.XL.ADXExcelTaskPane

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProcessSelectionTaskPane))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_processSelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_validateButton = New VIBlend.WinForms.Controls.vButton()
        Me.BTsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_processSelectionLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_validateButton, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.882352!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.11765!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(296, 708)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'm_processSelectionLabel
        '
        Me.m_processSelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_processSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_processSelectionLabel.Ellipsis = False
        Me.m_processSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_processSelectionLabel.Location = New System.Drawing.Point(3, 4)
        Me.m_processSelectionLabel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.m_processSelectionLabel.Multiline = True
        Me.m_processSelectionLabel.Name = "m_processSelectionLabel"
        Me.m_processSelectionLabel.Size = New System.Drawing.Size(98, 15)
        Me.m_processSelectionLabel.TabIndex = 2
        Me.m_processSelectionLabel.Text = "Select a Process"
        Me.m_processSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_processSelectionLabel.UseMnemonics = True
        Me.m_processSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_validateButton
        '
        Me.m_validateButton.AllowAnimations = True
        Me.m_validateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_validateButton.ImageKey = "1420498403_340208.ico"
        Me.m_validateButton.ImageList = Me.BTsIL
        Me.m_validateButton.Location = New System.Drawing.Point(3, 625)
        Me.m_validateButton.Name = "m_validateButton"
        Me.m_validateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_validateButton.Size = New System.Drawing.Size(95, 23)
        Me.m_validateButton.TabIndex = 3
        Me.m_validateButton.Text = "Validate"
        Me.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_validateButton.UseVisualStyleBackColor = True
        Me.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'BTsIL
        '
        Me.BTsIL.ImageStream = CType(resources.GetObject("BTsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BTsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.BTsIL.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'ProcessSelectionTaskPane
        '
        Me.ClientSize = New System.Drawing.Size(296, 708)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ProcessSelectionTaskPane"
        Me.Text = "Process Selection"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_processSelectionLabel As viblend.winforms.controls.vLabel
    Friend WithEvents m_validateButton As viblend.winforms.controls.vButton
    Friend WithEvents BTsIL As System.Windows.Forms.ImageList

End Class
