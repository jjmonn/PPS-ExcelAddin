using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class LogView : System.Windows.Forms.Form
{

	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode()]
	protected override void Dispose(bool disposing)
	{
		try {
			if (disposing && components != null) {
				components.Dispose();
			}
		} finally {
			base.Dispose(disposing);
		}
	}

	//Required by the Windows Form Designer

  private System.ComponentModel.IContainer components = null;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
      VIBlend.WinForms.DataGridView.DataGridLocalization dataGridLocalization1 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
      this.m_entityTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_entityLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_periodLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_periodTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_versionTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_versionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_logDataGridView = new FBI.Forms.FbiDataGridView();
      this.SuspendLayout();
      // 
      // m_entityTB
      // 
      this.m_entityTB.BackColor = System.Drawing.Color.White;
      this.m_entityTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_entityTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_entityTB.DefaultText = "Empty...";
      this.m_entityTB.Location = new System.Drawing.Point(74, 11);
      this.m_entityTB.MaxLength = 32767;
      this.m_entityTB.Name = "m_entityTB";
      this.m_entityTB.PasswordChar = '\0';
      this.m_entityTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_entityTB.SelectionLength = 0;
      this.m_entityTB.SelectionStart = 0;
      this.m_entityTB.Size = new System.Drawing.Size(164, 23);
      this.m_entityTB.TabIndex = 1;
      this.m_entityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_entityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_entityLabel
      // 
      this.m_entityLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_entityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_entityLabel.Ellipsis = false;
      this.m_entityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_entityLabel.Location = new System.Drawing.Point(12, 11);
      this.m_entityLabel.Multiline = true;
      this.m_entityLabel.Name = "m_entityLabel";
      this.m_entityLabel.Size = new System.Drawing.Size(56, 23);
      this.m_entityLabel.TabIndex = 3;
      this.m_entityLabel.Text = "Entity";
      this.m_entityLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
      this.m_entityLabel.UseMnemonics = true;
      this.m_entityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountLabel
      // 
      this.m_accountLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountLabel.Ellipsis = false;
      this.m_accountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountLabel.Location = new System.Drawing.Point(244, 11);
      this.m_accountLabel.Multiline = true;
      this.m_accountLabel.Name = "m_accountLabel";
      this.m_accountLabel.Size = new System.Drawing.Size(68, 23);
      this.m_accountLabel.TabIndex = 4;
      this.m_accountLabel.Text = "Account";
      this.m_accountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
      this.m_accountLabel.UseMnemonics = true;
      this.m_accountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_periodLabel
      // 
      this.m_periodLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_periodLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_periodLabel.Ellipsis = false;
      this.m_periodLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_periodLabel.Location = new System.Drawing.Point(488, 11);
      this.m_periodLabel.Multiline = true;
      this.m_periodLabel.Name = "m_periodLabel";
      this.m_periodLabel.Size = new System.Drawing.Size(62, 23);
      this.m_periodLabel.TabIndex = 6;
      this.m_periodLabel.Text = "Period";
      this.m_periodLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
      this.m_periodLabel.UseMnemonics = true;
      this.m_periodLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountTB
      // 
      this.m_accountTB.BackColor = System.Drawing.Color.White;
      this.m_accountTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_accountTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_accountTB.DefaultText = "Empty...";
      this.m_accountTB.Location = new System.Drawing.Point(318, 11);
      this.m_accountTB.MaxLength = 32767;
      this.m_accountTB.Name = "m_accountTB";
      this.m_accountTB.PasswordChar = '\0';
      this.m_accountTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_accountTB.SelectionLength = 0;
      this.m_accountTB.SelectionStart = 0;
      this.m_accountTB.Size = new System.Drawing.Size(164, 23);
      this.m_accountTB.TabIndex = 2;
      this.m_accountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_accountTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_periodTB
      // 
      this.m_periodTB.BackColor = System.Drawing.Color.White;
      this.m_periodTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_periodTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_periodTB.DefaultText = "Empty...";
      this.m_periodTB.Location = new System.Drawing.Point(556, 11);
      this.m_periodTB.MaxLength = 32767;
      this.m_periodTB.Name = "m_periodTB";
      this.m_periodTB.PasswordChar = '\0';
      this.m_periodTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_periodTB.SelectionLength = 0;
      this.m_periodTB.SelectionStart = 0;
      this.m_periodTB.Size = new System.Drawing.Size(164, 23);
      this.m_periodTB.TabIndex = 2;
      this.m_periodTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_periodTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_versionTB
      // 
      this.m_versionTB.BackColor = System.Drawing.Color.White;
      this.m_versionTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_versionTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_versionTB.DefaultText = "Empty...";
      this.m_versionTB.Location = new System.Drawing.Point(801, 11);
      this.m_versionTB.MaxLength = 32767;
      this.m_versionTB.Name = "m_versionTB";
      this.m_versionTB.PasswordChar = '\0';
      this.m_versionTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_versionTB.SelectionLength = 0;
      this.m_versionTB.SelectionStart = 0;
      this.m_versionTB.Size = new System.Drawing.Size(164, 23);
      this.m_versionTB.TabIndex = 7;
      this.m_versionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_versionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_versionLabel
      // 
      this.m_versionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_versionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_versionLabel.Ellipsis = false;
      this.m_versionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_versionLabel.Location = new System.Drawing.Point(726, 11);
      this.m_versionLabel.Multiline = true;
      this.m_versionLabel.Name = "m_versionLabel";
      this.m_versionLabel.Size = new System.Drawing.Size(69, 23);
      this.m_versionLabel.TabIndex = 8;
      this.m_versionLabel.Text = "Version";
      this.m_versionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
      this.m_versionLabel.UseMnemonics = true;
      this.m_versionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_logDataGridView
      // 
      this.m_logDataGridView.AllowAnimations = true;
      this.m_logDataGridView.AllowCellMerge = true;
      this.m_logDataGridView.AllowClipDrawing = true;
      this.m_logDataGridView.AllowContextMenuColumnChooser = true;
      this.m_logDataGridView.AllowContextMenuFiltering = true;
      this.m_logDataGridView.AllowContextMenuGrouping = true;
      this.m_logDataGridView.AllowContextMenuSorting = true;
      this.m_logDataGridView.AllowCopyPaste = false;
      this.m_logDataGridView.AllowDefaultContextMenu = true;
      this.m_logDataGridView.AllowDragDropIndication = true;
      this.m_logDataGridView.AllowHeaderItemHighlightOnCellSelection = true;
      this.m_logDataGridView.AutoUpdateOnListChanged = false;
      this.m_logDataGridView.BackColor = System.Drawing.Color.White;
      this.m_logDataGridView.BindingProgressEnabled = false;
      this.m_logDataGridView.BindingProgressSampleRate = 20000;
      this.m_logDataGridView.BorderColor = System.Drawing.Color.Empty;
      this.m_logDataGridView.CellsArea.AllowCellMerge = true;
      this.m_logDataGridView.CellsArea.ConditionalFormattingEnabled = false;
      this.m_logDataGridView.ColumnsHierarchy.AllowDragDrop = false;
      this.m_logDataGridView.ColumnsHierarchy.AllowResize = true;
      this.m_logDataGridView.ColumnsHierarchy.AutoStretchColumns = false;
      this.m_logDataGridView.ColumnsHierarchy.Fixed = false;
      this.m_logDataGridView.ColumnsHierarchy.ShowExpandCollapseButtons = true;
      this.m_logDataGridView.EnableColumnChooser = false;
      this.m_logDataGridView.EnableResizeToolTip = true;
      this.m_logDataGridView.EnableToolTips = true;
      this.m_logDataGridView.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
      this.m_logDataGridView.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_logDataGridView.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
      this.m_logDataGridView.GroupingEnabled = false;
      this.m_logDataGridView.HorizontalScroll = 0;
      this.m_logDataGridView.HorizontalScrollBarLargeChange = 20;
      this.m_logDataGridView.HorizontalScrollBarSmallChange = 5;
      this.m_logDataGridView.ImageList = null;
      this.m_logDataGridView.Localization = dataGridLocalization1;
      this.m_logDataGridView.Location = new System.Drawing.Point(0, 40);
      this.m_logDataGridView.MultipleSelectionEnabled = true;
      this.m_logDataGridView.Name = "m_logDataGridView";
      this.m_logDataGridView.PivotColumnsTotalsEnabled = false;
      this.m_logDataGridView.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_logDataGridView.PivotRowsTotalsEnabled = false;
      this.m_logDataGridView.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_logDataGridView.RowsHierarchy.AllowDragDrop = false;
      this.m_logDataGridView.RowsHierarchy.AllowResize = true;
      this.m_logDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = false;
      this.m_logDataGridView.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
      this.m_logDataGridView.RowsHierarchy.Fixed = false;
      this.m_logDataGridView.RowsHierarchy.ShowExpandCollapseButtons = true;
      this.m_logDataGridView.ScrollBarsEnabled = true;
      this.m_logDataGridView.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_logDataGridView.SelectionBorderEnabled = true;
      this.m_logDataGridView.SelectionBorderWidth = 2;
      this.m_logDataGridView.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
      this.m_logDataGridView.Size = new System.Drawing.Size(988, 211);
      this.m_logDataGridView.TabIndex = 0;
      this.m_logDataGridView.Text = "VDataGridView1";
      this.m_logDataGridView.ToolTipDuration = 5000;
      this.m_logDataGridView.ToolTipShowDelay = 1500;
      this.m_logDataGridView.VerticalScroll = 0;
      this.m_logDataGridView.VerticalScrollBarLargeChange = 20;
      this.m_logDataGridView.VerticalScrollBarSmallChange = 5;
      this.m_logDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_logDataGridView.VirtualModeCellDefault = false;
      // 
      // LogView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(988, 251);
      this.Controls.Add(this.m_versionTB);
      this.Controls.Add(this.m_periodTB);
      this.Controls.Add(this.m_versionLabel);
      this.Controls.Add(this.m_accountTB);
      this.Controls.Add(this.m_periodLabel);
      this.Controls.Add(this.m_accountLabel);
      this.Controls.Add(this.m_entityLabel);
      this.Controls.Add(this.m_entityTB);
      this.Controls.Add(this.m_logDataGridView);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "LogView";
      this.Text = "[general.log]";
      this.ResumeLayout(false);

  }
  public VIBlend.WinForms.Controls.vTextBox m_entityTB;
	public VIBlend.WinForms.Controls.vLabel m_entityLabel;
	public VIBlend.WinForms.Controls.vLabel m_accountLabel;
  public VIBlend.WinForms.Controls.vLabel m_periodLabel;
  public VIBlend.WinForms.Controls.vTextBox m_accountTB;
  public VIBlend.WinForms.Controls.vTextBox m_periodTB;
  public VIBlend.WinForms.Controls.vTextBox m_versionTB;
  public VIBlend.WinForms.Controls.vLabel m_versionLabel;
  public FBI.Forms.FbiDataGridView m_logDataGridView;
}

}
