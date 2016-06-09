namespace FBI.MVC.View
{
  partial class AccountSnapshotPropertiesView
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      VIBlend.WinForms.DataGridView.DataGridLocalization dataGridLocalization2 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
      this.m_dgv = new FBI.Forms.FbiDataGridView();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_validateBT = new VIBlend.WinForms.Controls.vButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_dgv
      // 
      this.m_dgv.AllowAnimations = true;
      this.m_dgv.AllowCellMerge = true;
      this.m_dgv.AllowClipDrawing = true;
      this.m_dgv.AllowContextMenuColumnChooser = true;
      this.m_dgv.AllowContextMenuFiltering = true;
      this.m_dgv.AllowContextMenuGrouping = true;
      this.m_dgv.AllowContextMenuSorting = true;
      this.m_dgv.AllowCopyPaste = false;
      this.m_dgv.AllowDefaultContextMenu = true;
      this.m_dgv.AllowDragDropIndication = true;
      this.m_dgv.AllowHeaderItemHighlightOnCellSelection = true;
      this.m_dgv.AutoUpdateOnListChanged = false;
      this.m_dgv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
      this.m_dgv.BindingProgressEnabled = false;
      this.m_dgv.BindingProgressSampleRate = 20000;
      this.m_dgv.BorderColor = System.Drawing.Color.Empty;
      this.m_dgv.CellsArea.AllowCellMerge = true;
      this.m_dgv.CellsArea.ConditionalFormattingEnabled = false;
      this.m_dgv.ColumnsHierarchy.AllowDragDrop = false;
      this.m_dgv.ColumnsHierarchy.AllowResize = true;
      this.m_dgv.ColumnsHierarchy.AutoStretchColumns = false;
      this.m_dgv.ColumnsHierarchy.Fixed = false;
      this.m_dgv.ColumnsHierarchy.ShowExpandCollapseButtons = true;
      this.m_dgv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_dgv.EnableColumnChooser = false;
      this.m_dgv.EnableResizeToolTip = true;
      this.m_dgv.EnableToolTips = true;
      this.m_dgv.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
      this.m_dgv.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_dgv.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
      this.m_dgv.GroupingEnabled = false;
      this.m_dgv.HorizontalScroll = 0;
      this.m_dgv.HorizontalScrollBarLargeChange = 20;
      this.m_dgv.HorizontalScrollBarSmallChange = 5;
      this.m_dgv.ImageList = null;
      this.m_dgv.Localization = dataGridLocalization2;
      this.m_dgv.Location = new System.Drawing.Point(3, 3);
      this.m_dgv.MultipleSelectionEnabled = true;
      this.m_dgv.Name = "m_dgv";
      this.m_dgv.PivotColumnsTotalsEnabled = false;
      this.m_dgv.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_dgv.PivotRowsTotalsEnabled = false;
      this.m_dgv.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_dgv.RowsHierarchy.AllowDragDrop = false;
      this.m_dgv.RowsHierarchy.AllowResize = true;
      this.m_dgv.RowsHierarchy.CompactStyleRenderingEnabled = false;
      this.m_dgv.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
      this.m_dgv.RowsHierarchy.Fixed = false;
      this.m_dgv.RowsHierarchy.ShowExpandCollapseButtons = true;
      this.m_dgv.ScrollBarsEnabled = true;
      this.m_dgv.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_dgv.SelectionBorderEnabled = true;
      this.m_dgv.SelectionBorderWidth = 2;
      this.m_dgv.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
      this.m_dgv.Size = new System.Drawing.Size(686, 367);
      this.m_dgv.TabIndex = 0;
      this.m_dgv.Text = "vDataGridView1";
      this.m_dgv.ToolTipDuration = 5000;
      this.m_dgv.ToolTipShowDelay = 1500;
      this.m_dgv.VerticalScroll = 0;
      this.m_dgv.VerticalScrollBarLargeChange = 20;
      this.m_dgv.VerticalScrollBarSmallChange = 5;
      this.m_dgv.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_dgv.VirtualModeCellDefault = false;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.m_dgv, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.m_validateBT, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.57426F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.425743F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(692, 404);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // m_validateBT
      // 
      this.m_validateBT.AllowAnimations = true;
      this.m_validateBT.BackColor = System.Drawing.Color.Transparent;
      this.m_validateBT.Dock = System.Windows.Forms.DockStyle.Right;
      this.m_validateBT.Location = new System.Drawing.Point(494, 376);
      this.m_validateBT.Name = "m_validateBT";
      this.m_validateBT.RoundedCornersMask = ((byte)(15));
      this.m_validateBT.Size = new System.Drawing.Size(195, 25);
      this.m_validateBT.TabIndex = 1;
      this.m_validateBT.Text = "vButton1";
      this.m_validateBT.UseVisualStyleBackColor = false;
      this.m_validateBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_validateBT.Click += new System.EventHandler(this.OnValidate);
      // 
      // AccountSnapshotPropertiesView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(692, 404);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "AccountSnapshotPropertiesView";
      this.Text = "AccountSnapshotSelectionView";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private FBI.Forms.FbiDataGridView m_dgv;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private VIBlend.WinForms.Controls.vButton m_validateBT;
  }
}