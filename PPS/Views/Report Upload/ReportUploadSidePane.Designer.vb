<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportUploadSidePane
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportUploadSidePane))
        Me.m_accountTypeTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.VLabel4 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel3 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel2 = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.m_formulaTextBox = New System.Windows.Forms.TextBox()
        Me.m_descriptionTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'm_accountTypeTextBox
        '
        Me.m_accountTypeTextBox.BackColor = System.Drawing.Color.White
        Me.m_accountTypeTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_accountTypeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_accountTypeTextBox.DefaultText = "Empty..."
        Me.m_accountTypeTextBox.Enabled = False
        Me.m_accountTypeTextBox.Location = New System.Drawing.Point(14, 609)
        Me.m_accountTypeTextBox.MaxLength = 32767
        Me.m_accountTypeTextBox.Name = "m_accountTypeTextBox"
        Me.m_accountTypeTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_accountTypeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_accountTypeTextBox.SelectionLength = 0
        Me.m_accountTypeTextBox.SelectionStart = 0
        Me.m_accountTypeTextBox.Size = New System.Drawing.Size(251, 23)
        Me.m_accountTypeTextBox.TabIndex = 15
        Me.m_accountTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_accountTypeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel4
        '
        Me.VLabel4.BackColor = System.Drawing.Color.Transparent
        Me.VLabel4.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel4.Ellipsis = False
        Me.VLabel4.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel4.Location = New System.Drawing.Point(14, 580)
        Me.VLabel4.Multiline = True
        Me.VLabel4.Name = "VLabel4"
        Me.VLabel4.Size = New System.Drawing.Size(136, 23)
        Me.VLabel4.TabIndex = 14
        Me.VLabel4.Text = "Account's type"
        Me.VLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel4.UseMnemonics = True
        Me.VLabel4.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel3
        '
        Me.VLabel3.BackColor = System.Drawing.Color.Transparent
        Me.VLabel3.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel3.Ellipsis = False
        Me.VLabel3.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel3.Location = New System.Drawing.Point(14, 328)
        Me.VLabel3.Multiline = True
        Me.VLabel3.Name = "VLabel3"
        Me.VLabel3.Size = New System.Drawing.Size(124, 23)
        Me.VLabel3.TabIndex = 12
        Me.VLabel3.Text = "Description"
        Me.VLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel3.UseMnemonics = True
        Me.VLabel3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel2
        '
        Me.VLabel2.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel2.Ellipsis = False
        Me.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.Location = New System.Drawing.Point(12, 77)
        Me.VLabel2.Multiline = True
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Size = New System.Drawing.Size(92, 23)
        Me.VLabel2.TabIndex = 10
        Me.VLabel2.Text = "Formula"
        Me.VLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel2.UseMnemonics = True
        Me.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountTextBox
        '
        Me.m_accountTextBox.BackColor = System.Drawing.Color.White
        Me.m_accountTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_accountTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_accountTextBox.DefaultText = "Empty..."
        Me.m_accountTextBox.Enabled = False
        Me.m_accountTextBox.Location = New System.Drawing.Point(12, 42)
        Me.m_accountTextBox.MaxLength = 32767
        Me.m_accountTextBox.Name = "m_accountTextBox"
        Me.m_accountTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_accountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_accountTextBox.SelectionLength = 0
        Me.m_accountTextBox.SelectionStart = 0
        Me.m_accountTextBox.Size = New System.Drawing.Size(251, 23)
        Me.m_accountTextBox.TabIndex = 9
        Me.m_accountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_accountTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(12, 13)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(92, 23)
        Me.VLabel1.TabIndex = 8
        Me.VLabel1.Text = "Account"
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_formulaTextBox
        '
        Me.m_formulaTextBox.Enabled = False
        Me.m_formulaTextBox.Location = New System.Drawing.Point(12, 106)
        Me.m_formulaTextBox.Multiline = True
        Me.m_formulaTextBox.Name = "m_formulaTextBox"
        Me.m_formulaTextBox.Size = New System.Drawing.Size(249, 196)
        Me.m_formulaTextBox.TabIndex = 16
        '
        'm_descriptionTextBox
        '
        Me.m_descriptionTextBox.Enabled = False
        Me.m_descriptionTextBox.Location = New System.Drawing.Point(14, 357)
        Me.m_descriptionTextBox.Multiline = True
        Me.m_descriptionTextBox.Name = "m_descriptionTextBox"
        Me.m_descriptionTextBox.Size = New System.Drawing.Size(249, 201)
        Me.m_descriptionTextBox.TabIndex = 17
        '
        'ReportUploadSidePane
        '
        Me.ClientSize = New System.Drawing.Size(304, 830)
        Me.Controls.Add(Me.m_descriptionTextBox)
        Me.Controls.Add(Me.m_formulaTextBox)
        Me.Controls.Add(Me.m_accountTypeTextBox)
        Me.Controls.Add(Me.VLabel4)
        Me.Controls.Add(Me.VLabel3)
        Me.Controls.Add(Me.VLabel2)
        Me.Controls.Add(Me.m_accountTextBox)
        Me.Controls.Add(Me.VLabel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ReportUploadSidePane"
        Me.Text = "Account's details"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents m_accountTypeTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents VLabel4 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel3 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel2 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_formulaTextBox As System.Windows.Forms.TextBox
    Friend WithEvents m_descriptionTextBox As System.Windows.Forms.TextBox

End Class
