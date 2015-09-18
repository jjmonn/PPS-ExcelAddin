<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CurrenciesView
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
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CurrenciesView))
        Me.VContextMenu1 = New VIBlend.WinForms.Controls.vContextMenu()
        Me.SetMainCurrencyCallBack = New System.Windows.Forms.MenuItem()
        Me.ValidateButton = New VIBlend.WinForms.Controls.vButton()
        Me.m_currenciesDataGridView = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'VContextMenu1
        '
        Me.VContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.SetMainCurrencyCallBack})
        '
        'SetMainCurrencyCallBack
        '
        Me.SetMainCurrencyCallBack.Index = 0
        Me.SetMainCurrencyCallBack.Text = "Set Main Currency"
        '
        'ValidateButton
        '
        Me.ValidateButton.AllowAnimations = True
        Me.ValidateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValidateButton.BackColor = System.Drawing.Color.Transparent
        Me.ValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateButton.ImageKey = "1420498403_340208.ico"
        Me.ValidateButton.ImageList = Me.EditButtonsImagelist
        Me.ValidateButton.Location = New System.Drawing.Point(686, 457)
        Me.ValidateButton.Name = "ValidateButton"
        Me.ValidateButton.RoundedCornersMask = CType(15, Byte)
        Me.ValidateButton.Size = New System.Drawing.Size(76, 30)
        Me.ValidateButton.TabIndex = 1
        Me.ValidateButton.Text = "Save"
        Me.ValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateButton.UseVisualStyleBackColor = False
        Me.ValidateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_currenciesDataGridView
        '
        Me.m_currenciesDataGridView.AllowAnimations = True
        Me.m_currenciesDataGridView.AllowCellMerge = True
        Me.m_currenciesDataGridView.AllowClipDrawing = True
        Me.m_currenciesDataGridView.AllowContextMenuColumnChooser = True
        Me.m_currenciesDataGridView.AllowContextMenuFiltering = True
        Me.m_currenciesDataGridView.AllowContextMenuGrouping = True
        Me.m_currenciesDataGridView.AllowContextMenuSorting = True
        Me.m_currenciesDataGridView.AllowCopyPaste = False
        Me.m_currenciesDataGridView.AllowDefaultContextMenu = True
        Me.m_currenciesDataGridView.AllowDragDropIndication = True
        Me.m_currenciesDataGridView.AllowHeaderItemHighlightOnCellSelection = True
        Me.m_currenciesDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_currenciesDataGridView.AutoUpdateOnListChanged = False
        Me.m_currenciesDataGridView.BackColor = System.Drawing.Color.White
        Me.m_currenciesDataGridView.BindingProgressEnabled = False
        Me.m_currenciesDataGridView.BindingProgressSampleRate = 20000
        Me.m_currenciesDataGridView.BorderColor = System.Drawing.Color.Empty
        Me.m_currenciesDataGridView.CellsArea.AllowCellMerge = True
        Me.m_currenciesDataGridView.CellsArea.ConditionalFormattingEnabled = False
        Me.m_currenciesDataGridView.ColumnsHierarchy.AllowDragDrop = False
        Me.m_currenciesDataGridView.ColumnsHierarchy.AllowResize = True
        Me.m_currenciesDataGridView.ColumnsHierarchy.AutoStretchColumns = False
        Me.m_currenciesDataGridView.ColumnsHierarchy.Fixed = False
        Me.m_currenciesDataGridView.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.m_currenciesDataGridView.EnableColumnChooser = False
        Me.m_currenciesDataGridView.EnableResizeToolTip = True
        Me.m_currenciesDataGridView.EnableToolTips = True
        Me.m_currenciesDataGridView.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.m_currenciesDataGridView.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_currenciesDataGridView.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.m_currenciesDataGridView.GroupingEnabled = False
        Me.m_currenciesDataGridView.HorizontalScroll = 0
        Me.m_currenciesDataGridView.HorizontalScrollBarLargeChange = 20
        Me.m_currenciesDataGridView.HorizontalScrollBarSmallChange = 5
        Me.m_currenciesDataGridView.ImageList = Nothing
        Me.m_currenciesDataGridView.Localization = DataGridLocalization1
        Me.m_currenciesDataGridView.Location = New System.Drawing.Point(17, 38)
        Me.m_currenciesDataGridView.MultipleSelectionEnabled = True
        Me.m_currenciesDataGridView.Name = "m_currenciesDataGridView"
        Me.m_currenciesDataGridView.PivotColumnsTotalsEnabled = False
        Me.m_currenciesDataGridView.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_currenciesDataGridView.PivotRowsTotalsEnabled = False
        Me.m_currenciesDataGridView.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_currenciesDataGridView.RowsHierarchy.AllowDragDrop = False
        Me.m_currenciesDataGridView.RowsHierarchy.AllowResize = True
        Me.m_currenciesDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.m_currenciesDataGridView.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.m_currenciesDataGridView.RowsHierarchy.Fixed = False
        Me.m_currenciesDataGridView.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.m_currenciesDataGridView.ScrollBarsEnabled = True
        Me.m_currenciesDataGridView.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_currenciesDataGridView.SelectionBorderEnabled = True
        Me.m_currenciesDataGridView.SelectionBorderWidth = 2
        Me.m_currenciesDataGridView.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.m_currenciesDataGridView.Size = New System.Drawing.Size(745, 403)
        Me.m_currenciesDataGridView.TabIndex = 2
        Me.m_currenciesDataGridView.Text = "VDataGridView1"
        Me.m_currenciesDataGridView.ToolTipDuration = 5000
        Me.m_currenciesDataGridView.ToolTipShowDelay = 1500
        Me.m_currenciesDataGridView.VerticalScroll = 0
        Me.m_currenciesDataGridView.VerticalScrollBarLargeChange = 20
        Me.m_currenciesDataGridView.VerticalScrollBarSmallChange = 5
        Me.m_currenciesDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        Me.m_currenciesDataGridView.VirtualModeCellDefault = False
        '
        'EditButtonsImagelist
        '
        Me.EditButtonsImagelist.ImageStream = CType(resources.GetObject("EditButtonsImagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent
        Me.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'CurrenciesView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.m_currenciesDataGridView)
        Me.Controls.Add(Me.ValidateButton)
        Me.Name = "CurrenciesView"
        Me.Size = New System.Drawing.Size(776, 511)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VContextMenu1 As VIBlend.WinForms.Controls.vContextMenu
    Friend WithEvents ValidateButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents m_currenciesDataGridView As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents SetMainCurrencyCallBack As System.Windows.Forms.MenuItem
    Friend WithEvents EditButtonsImagelist As System.Windows.Forms.ImageList

End Class
