<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogView
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
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogView))
        Me.m_logDataGridView = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.m_entityTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_accountTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel2 = New VIBlend.WinForms.Controls.vLabel()
        Me.SuspendLayout()
        '
        'm_logDataGridView
        '
        Me.m_logDataGridView.AllowAnimations = True
        Me.m_logDataGridView.AllowCellMerge = True
        Me.m_logDataGridView.AllowClipDrawing = True
        Me.m_logDataGridView.AllowContextMenuColumnChooser = True
        Me.m_logDataGridView.AllowContextMenuFiltering = True
        Me.m_logDataGridView.AllowContextMenuGrouping = True
        Me.m_logDataGridView.AllowContextMenuSorting = True
        Me.m_logDataGridView.AllowCopyPaste = False
        Me.m_logDataGridView.AllowDefaultContextMenu = True
        Me.m_logDataGridView.AllowDragDropIndication = True
        Me.m_logDataGridView.AllowHeaderItemHighlightOnCellSelection = True
        Me.m_logDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_logDataGridView.AutoUpdateOnListChanged = False
        Me.m_logDataGridView.BackColor = System.Drawing.Color.White
        Me.m_logDataGridView.BindingProgressEnabled = False
        Me.m_logDataGridView.BindingProgressSampleRate = 20000
        Me.m_logDataGridView.BorderColor = System.Drawing.Color.Empty
        Me.m_logDataGridView.CellsArea.AllowCellMerge = True
        Me.m_logDataGridView.CellsArea.ConditionalFormattingEnabled = False
        Me.m_logDataGridView.ColumnsHierarchy.AllowDragDrop = False
        Me.m_logDataGridView.ColumnsHierarchy.AllowResize = True
        Me.m_logDataGridView.ColumnsHierarchy.AutoStretchColumns = False
        Me.m_logDataGridView.ColumnsHierarchy.Fixed = False
        Me.m_logDataGridView.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.m_logDataGridView.EnableColumnChooser = False
        Me.m_logDataGridView.EnableResizeToolTip = True
        Me.m_logDataGridView.EnableToolTips = True
        Me.m_logDataGridView.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.m_logDataGridView.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_logDataGridView.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.m_logDataGridView.GroupingEnabled = False
        Me.m_logDataGridView.HorizontalScroll = 0
        Me.m_logDataGridView.HorizontalScrollBarLargeChange = 20
        Me.m_logDataGridView.HorizontalScrollBarSmallChange = 5
        Me.m_logDataGridView.ImageList = Nothing
        Me.m_logDataGridView.Localization = DataGridLocalization1
        Me.m_logDataGridView.Location = New System.Drawing.Point(0, 45)
        Me.m_logDataGridView.MultipleSelectionEnabled = True
        Me.m_logDataGridView.Name = "m_logDataGridView"
        Me.m_logDataGridView.PivotColumnsTotalsEnabled = False
        Me.m_logDataGridView.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_logDataGridView.PivotRowsTotalsEnabled = False
        Me.m_logDataGridView.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_logDataGridView.RowsHierarchy.AllowDragDrop = False
        Me.m_logDataGridView.RowsHierarchy.AllowResize = True
        Me.m_logDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.m_logDataGridView.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.m_logDataGridView.RowsHierarchy.Fixed = False
        Me.m_logDataGridView.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.m_logDataGridView.ScrollBarsEnabled = True
        Me.m_logDataGridView.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_logDataGridView.SelectionBorderEnabled = True
        Me.m_logDataGridView.SelectionBorderWidth = 2
        Me.m_logDataGridView.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.m_logDataGridView.Size = New System.Drawing.Size(574, 206)
        Me.m_logDataGridView.TabIndex = 0
        Me.m_logDataGridView.Text = "VDataGridView1"
        Me.m_logDataGridView.ToolTipDuration = 5000
        Me.m_logDataGridView.ToolTipShowDelay = 1500
        Me.m_logDataGridView.VerticalScroll = 0
        Me.m_logDataGridView.VerticalScrollBarLargeChange = 20
        Me.m_logDataGridView.VerticalScrollBarSmallChange = 5
        Me.m_logDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_logDataGridView.VirtualModeCellDefault = False
        '
        'm_entityTextBox
        '
        Me.m_entityTextBox.BackColor = System.Drawing.Color.White
        Me.m_entityTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_entityTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_entityTextBox.DefaultText = "Empty..."
        Me.m_entityTextBox.Location = New System.Drawing.Point(68, 11)
        Me.m_entityTextBox.MaxLength = 32767
        Me.m_entityTextBox.Name = "m_entityTextBox"
        Me.m_entityTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_entityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_entityTextBox.SelectionLength = 0
        Me.m_entityTextBox.SelectionStart = 0
        Me.m_entityTextBox.Size = New System.Drawing.Size(218, 23)
        Me.m_entityTextBox.TabIndex = 1
        Me.m_entityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_entityTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountTextBox
        '
        Me.m_accountTextBox.BackColor = System.Drawing.Color.White
        Me.m_accountTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_accountTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_accountTextBox.DefaultText = "Empty..."
        Me.m_accountTextBox.Location = New System.Drawing.Point(362, 11)
        Me.m_accountTextBox.MaxLength = 32767
        Me.m_accountTextBox.Name = "m_accountTextBox"
        Me.m_accountTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_accountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_accountTextBox.SelectionLength = 0
        Me.m_accountTextBox.SelectionStart = 0
        Me.m_accountTextBox.Size = New System.Drawing.Size(198, 23)
        Me.m_accountTextBox.TabIndex = 2
        Me.m_accountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_accountTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(21, 11)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(41, 23)
        Me.VLabel1.TabIndex = 3
        Me.VLabel1.Text = Local.GetValue("general.entity")
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel2
        '
        Me.VLabel2.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel2.Ellipsis = False
        Me.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.Location = New System.Drawing.Point(306, 11)
        Me.VLabel2.Multiline = True
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Size = New System.Drawing.Size(50, 23)
        Me.VLabel2.TabIndex = 4
        Me.VLabel2.Text = Local.GetValue("general.account")
        Me.VLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel2.UseMnemonics = True
        Me.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'LogView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 251)
        Me.Controls.Add(Me.VLabel2)
        Me.Controls.Add(Me.VLabel1)
        Me.Controls.Add(Me.m_accountTextBox)
        Me.Controls.Add(Me.m_entityTextBox)
        Me.Controls.Add(Me.m_logDataGridView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LogView"
        Me.Text = Local.GetValue("general.log")
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_logDataGridView As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents m_entityTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_accountTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel2 As VIBlend.WinForms.Controls.vLabel
End Class
