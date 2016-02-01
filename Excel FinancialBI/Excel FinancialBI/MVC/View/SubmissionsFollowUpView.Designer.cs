using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class SubmissionsFollowUpView : System.Windows.Forms.Form
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

	private System.ComponentModel.IContainer components;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		VIBlend.WinForms.DataGridView.DataGridLocalization DataGridLocalization1 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubmissionsFollowUpView));
		this.m_submissionsDGV = new VIBlend.WinForms.DataGridView.vDataGridView();
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.m_startDateLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_endDateLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_startDate = new VIBlend.WinForms.Controls.vDatePicker();
		this.m_endDate = new VIBlend.WinForms.Controls.vDatePicker();
		this.m_cellsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.m_hierarchyRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.CopyDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.CollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.TableLayoutPanel1.SuspendLayout();
		this.Panel1.SuspendLayout();
		this.m_cellsRightClickMenu.SuspendLayout();
		this.m_hierarchyRightClickMenu.SuspendLayout();
		this.SuspendLayout();
		//
		//m_submissionsDGV
		//
		this.m_submissionsDGV.AllowAnimations = true;
		this.m_submissionsDGV.AllowCellMerge = true;
		this.m_submissionsDGV.AllowClipDrawing = true;
		this.m_submissionsDGV.AllowContextMenuColumnChooser = true;
		this.m_submissionsDGV.AllowContextMenuFiltering = true;
		this.m_submissionsDGV.AllowContextMenuGrouping = true;
		this.m_submissionsDGV.AllowContextMenuSorting = true;
		this.m_submissionsDGV.AllowCopyPaste = false;
		this.m_submissionsDGV.AllowDefaultContextMenu = true;
		this.m_submissionsDGV.AllowDragDropIndication = true;
		this.m_submissionsDGV.AllowHeaderItemHighlightOnCellSelection = true;
		this.m_submissionsDGV.AutoUpdateOnListChanged = false;
		this.m_submissionsDGV.BackColor = System.Drawing.SystemColors.Control;
		this.m_submissionsDGV.BindingProgressEnabled = false;
		this.m_submissionsDGV.BindingProgressSampleRate = 20000;
		this.m_submissionsDGV.BorderColor = System.Drawing.Color.Empty;
		this.m_submissionsDGV.CellsArea.AllowCellMerge = true;
		this.m_submissionsDGV.CellsArea.ConditionalFormattingEnabled = false;
		this.m_submissionsDGV.ColumnsHierarchy.AllowDragDrop = false;
		this.m_submissionsDGV.ColumnsHierarchy.AllowResize = true;
		this.m_submissionsDGV.ColumnsHierarchy.AutoStretchColumns = false;
		this.m_submissionsDGV.ColumnsHierarchy.Fixed = false;
		this.m_submissionsDGV.ColumnsHierarchy.ShowExpandCollapseButtons = true;
		this.m_submissionsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_submissionsDGV.EnableColumnChooser = false;
		this.m_submissionsDGV.EnableResizeToolTip = true;
		this.m_submissionsDGV.EnableToolTips = true;
		this.m_submissionsDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
		this.m_submissionsDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
		this.m_submissionsDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
		this.m_submissionsDGV.GroupingEnabled = false;
		this.m_submissionsDGV.HorizontalScroll = 0;
		this.m_submissionsDGV.HorizontalScrollBarLargeChange = 20;
		this.m_submissionsDGV.HorizontalScrollBarSmallChange = 5;
		this.m_submissionsDGV.ImageList = null;
		this.m_submissionsDGV.Localization = DataGridLocalization1;
		this.m_submissionsDGV.Location = new System.Drawing.Point(3, 35);
		this.m_submissionsDGV.MultipleSelectionEnabled = true;
		this.m_submissionsDGV.Name = "m_submissionsDGV";
		this.m_submissionsDGV.PivotColumnsTotalsEnabled = false;
		this.m_submissionsDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
		this.m_submissionsDGV.PivotRowsTotalsEnabled = false;
		this.m_submissionsDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
		this.m_submissionsDGV.RowsHierarchy.AllowDragDrop = false;
		this.m_submissionsDGV.RowsHierarchy.AllowResize = true;
		this.m_submissionsDGV.RowsHierarchy.CompactStyleRenderingEnabled = false;
		this.m_submissionsDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
		this.m_submissionsDGV.RowsHierarchy.Fixed = false;
		this.m_submissionsDGV.RowsHierarchy.ShowExpandCollapseButtons = true;
		this.m_submissionsDGV.ScrollBarsEnabled = true;
		this.m_submissionsDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
		this.m_submissionsDGV.SelectionBorderEnabled = true;
		this.m_submissionsDGV.SelectionBorderWidth = 2;
		this.m_submissionsDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
		this.m_submissionsDGV.Size = new System.Drawing.Size(890, 646);
		this.m_submissionsDGV.TabIndex = 0;
		this.m_submissionsDGV.Text = "VDataGridView1";
		this.m_submissionsDGV.ToolTipDuration = 5000;
		this.m_submissionsDGV.ToolTipShowDelay = 1500;
		this.m_submissionsDGV.VerticalScroll = 0;
		this.m_submissionsDGV.VerticalScrollBarLargeChange = 20;
		this.m_submissionsDGV.VerticalScrollBarSmallChange = 5;
		this.m_submissionsDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_submissionsDGV.VirtualModeCellDefault = false;
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Controls.Add(this.m_submissionsDGV, 0, 1);
		this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 2;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(896, 684);
		this.TableLayoutPanel1.TabIndex = 1;
		//
		//Panel1
		//
		this.Panel1.Controls.Add(this.m_startDateLabel);
		this.Panel1.Controls.Add(this.m_endDateLabel);
		this.Panel1.Controls.Add(this.m_startDate);
		this.Panel1.Controls.Add(this.m_endDate);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(0, 0);
		this.Panel1.Margin = new System.Windows.Forms.Padding(0);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(896, 32);
		this.Panel1.TabIndex = 1;
		//
		//m_startDateLabel
		//
		this.m_startDateLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_startDateLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_startDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_startDateLabel.Ellipsis = false;
		this.m_startDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_startDateLabel.Location = new System.Drawing.Point(458, 3);
		this.m_startDateLabel.Multiline = true;
		this.m_startDateLabel.Name = "m_startDateLabel";
		this.m_startDateLabel.Size = new System.Drawing.Size(105, 26);
		this.m_startDateLabel.TabIndex = 3;
		this.m_startDateLabel.Text = "Start date";
		this.m_startDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.m_startDateLabel.UseMnemonics = true;
		this.m_startDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_endDateLabel
		//
		this.m_endDateLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_endDateLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_endDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_endDateLabel.Ellipsis = false;
		this.m_endDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_endDateLabel.Location = new System.Drawing.Point(676, 3);
		this.m_endDateLabel.Multiline = true;
		this.m_endDateLabel.Name = "m_endDateLabel";
		this.m_endDateLabel.Size = new System.Drawing.Size(114, 26);
		this.m_endDateLabel.TabIndex = 2;
		this.m_endDateLabel.Text = "End date";
		this.m_endDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.m_endDateLabel.UseMnemonics = true;
		this.m_endDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_startDate
		//
		this.m_startDate.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_startDate.BackColor = System.Drawing.Color.White;
		this.m_startDate.BorderColor = System.Drawing.Color.Black;
		this.m_startDate.Culture = new System.Globalization.CultureInfo("");
		this.m_startDate.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_startDate.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_startDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
		this.m_startDate.FormatValue = "";
		this.m_startDate.Location = new System.Drawing.Point(567, 3);
		this.m_startDate.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
		this.m_startDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
		this.m_startDate.Name = "m_startDate";
		this.m_startDate.ShowGrip = false;
		this.m_startDate.Size = new System.Drawing.Size(100, 26);
		this.m_startDate.TabIndex = 1;
		this.m_startDate.Text = "VDatePicker1";
		this.m_startDate.UseThemeBackColor = false;
		this.m_startDate.UseThemeDropDownArrowColor = true;
		this.m_startDate.Value = new System.DateTime(2016, 1, 4, 9, 28, 7, 919);
		this.m_startDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_endDate
		//
		this.m_endDate.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_endDate.BackColor = System.Drawing.Color.White;
		this.m_endDate.BorderColor = System.Drawing.Color.Black;
		this.m_endDate.Culture = new System.Globalization.CultureInfo("");
		this.m_endDate.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_endDate.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_endDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
		this.m_endDate.FormatValue = "";
		this.m_endDate.Location = new System.Drawing.Point(793, 3);
		this.m_endDate.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
		this.m_endDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
		this.m_endDate.Name = "m_endDate";
		this.m_endDate.ShowGrip = false;
		this.m_endDate.Size = new System.Drawing.Size(100, 26);
		this.m_endDate.TabIndex = 0;
		this.m_endDate.Text = "VDatePicker1";
		this.m_endDate.UseThemeBackColor = false;
		this.m_endDate.UseThemeDropDownArrowColor = true;
		this.m_endDate.Value = new System.DateTime(2016, 1, 4, 9, 27, 20, 177);
		this.m_endDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_cellsRightClickMenu
		//
		this.m_cellsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.CopyDownToolStripMenuItem });
		this.m_cellsRightClickMenu.Name = "m_cellsRightClickMenu";
		this.m_cellsRightClickMenu.Size = new System.Drawing.Size(149, 28);
		//
		//m_hierarchyRightClickMenu
		//
		this.m_hierarchyRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ExpandAllToolStripMenuItem,
			this.CollapseAllToolStripMenuItem
		});
		this.m_hierarchyRightClickMenu.Name = "m_hierarchyRightClickMenu";
		this.m_hierarchyRightClickMenu.Size = new System.Drawing.Size(153, 74);
		//
		//CopyDownToolStripMenuItem
		//
		this.CopyDownToolStripMenuItem.Name = "CopyDownToolStripMenuItem";
		this.CopyDownToolStripMenuItem.Size = new System.Drawing.Size(148, 24);
		this.CopyDownToolStripMenuItem.Text = "Copy down";
		//
		//ExpandAllToolStripMenuItem
		//
		this.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem";
		this.ExpandAllToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
		this.ExpandAllToolStripMenuItem.Text = "Expand all";
		//
		//CollapseAllToolStripMenuItem
		//
		this.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem";
		this.CollapseAllToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
		this.CollapseAllToolStripMenuItem.Text = "Collapse all";
		//
		//SubmissionsFollowUpView
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(896, 684);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "SubmissionsFollowUpView";
		this.Text = "Submissions Tracking";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		this.m_cellsRightClickMenu.ResumeLayout(false);
		this.m_hierarchyRightClickMenu.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.DataGridView.vDataGridView m_submissionsDGV;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal System.Windows.Forms.Panel Panel1;
	internal VIBlend.WinForms.Controls.vLabel m_startDateLabel;
	internal VIBlend.WinForms.Controls.vLabel m_endDateLabel;
	internal VIBlend.WinForms.Controls.vDatePicker m_startDate;
	internal VIBlend.WinForms.Controls.vDatePicker m_endDate;
	internal System.Windows.Forms.ContextMenuStrip m_cellsRightClickMenu;
	internal System.Windows.Forms.ContextMenuStrip m_hierarchyRightClickMenu;
	internal System.Windows.Forms.ToolStripMenuItem CopyDownToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem ExpandAllToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem CollapseAllToolStripMenuItem;
}

}
