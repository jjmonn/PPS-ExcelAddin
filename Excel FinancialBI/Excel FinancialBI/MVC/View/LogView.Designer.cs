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
		VIBlend.WinForms.DataGridView.DataGridLocalization DataGridLocalization1 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
		this.m_logDataGridView = new VIBlend.WinForms.DataGridView.vDataGridView();
		this.m_entityTextBox = new VIBlend.WinForms.Controls.vTextBox();
		this.m_accountTextBox = new VIBlend.WinForms.Controls.vTextBox();
		this.VLabel1 = new VIBlend.WinForms.Controls.vLabel();
		this.VLabel2 = new VIBlend.WinForms.Controls.vLabel();
		this.SuspendLayout();
		//
		//m_logDataGridView
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
		this.m_logDataGridView.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
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
		this.m_logDataGridView.Localization = DataGridLocalization1;
		this.m_logDataGridView.Location = new System.Drawing.Point(0, 45);
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
		this.m_logDataGridView.Size = new System.Drawing.Size(574, 206);
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
		//m_entityTextBox
		//
		this.m_entityTextBox.BackColor = System.Drawing.Color.White;
		this.m_entityTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_entityTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_entityTextBox.DefaultText = "Empty...";
		this.m_entityTextBox.Location = new System.Drawing.Point(68, 11);
		this.m_entityTextBox.MaxLength = 32767;
		this.m_entityTextBox.Name = "m_entityTextBox";
		this.m_entityTextBox.PasswordChar = '\0';
		this.m_entityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_entityTextBox.SelectionLength = 0;
		this.m_entityTextBox.SelectionStart = 0;
		this.m_entityTextBox.Size = new System.Drawing.Size(218, 23);
		this.m_entityTextBox.TabIndex = 1;
		this.m_entityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_entityTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountTextBox
		//
		this.m_accountTextBox.BackColor = System.Drawing.Color.White;
		this.m_accountTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_accountTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_accountTextBox.DefaultText = "Empty...";
		this.m_accountTextBox.Location = new System.Drawing.Point(362, 11);
		this.m_accountTextBox.MaxLength = 32767;
		this.m_accountTextBox.Name = "m_accountTextBox";
		this.m_accountTextBox.PasswordChar = '\0';
		this.m_accountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_accountTextBox.SelectionLength = 0;
		this.m_accountTextBox.SelectionStart = 0;
		this.m_accountTextBox.Size = new System.Drawing.Size(198, 23);
		this.m_accountTextBox.TabIndex = 2;
		this.m_accountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_accountTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//VLabel1
		//
		this.VLabel1.BackColor = System.Drawing.Color.Transparent;
		this.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.VLabel1.Ellipsis = false;
		this.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.VLabel1.Location = new System.Drawing.Point(21, 11);
		this.VLabel1.Multiline = true;
		this.VLabel1.Name = "VLabel1";
		this.VLabel1.Size = new System.Drawing.Size(41, 23);
		this.VLabel1.TabIndex = 3;
    this.VLabel1.Text = FBI.Utils.Local.GetValue("general.entity");
		this.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.VLabel1.UseMnemonics = true;
		this.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//VLabel2
		//
		this.VLabel2.BackColor = System.Drawing.Color.Transparent;
		this.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.VLabel2.Ellipsis = false;
		this.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.VLabel2.Location = new System.Drawing.Point(306, 11);
		this.VLabel2.Multiline = true;
		this.VLabel2.Name = "VLabel2";
		this.VLabel2.Size = new System.Drawing.Size(50, 23);
		this.VLabel2.TabIndex = 4;
    this.VLabel2.Text = FBI.Utils.Local.GetValue("general.account");
		this.VLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.VLabel2.UseMnemonics = true;
		this.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//LogView
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(572, 251);
		this.Controls.Add(this.VLabel2);
		this.Controls.Add(this.VLabel1);
		this.Controls.Add(this.m_accountTextBox);
		this.Controls.Add(this.m_entityTextBox);
		this.Controls.Add(this.m_logDataGridView);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "LogView";
    this.Text = FBI.Utils.Local.GetValue("general.log");
		this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.DataGridView.vDataGridView m_logDataGridView;
	internal VIBlend.WinForms.Controls.vTextBox m_entityTextBox;
	internal VIBlend.WinForms.Controls.vTextBox m_accountTextBox;
	internal VIBlend.WinForms.Controls.vLabel VLabel1;
	internal VIBlend.WinForms.Controls.vLabel VLabel2;
}

}
