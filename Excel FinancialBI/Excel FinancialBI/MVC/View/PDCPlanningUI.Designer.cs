using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class PDCPlanningUI : System.Windows.Forms.Form
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
		VIBlend.WinForms.DataGridView.PivotDesignPanelLocalization PivotDesignPanelLocalization1 = new VIBlend.WinForms.DataGridView.PivotDesignPanelLocalization();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDCPlanningUI));
		this.m_PDCDataGridView = new VIBlend.WinForms.DataGridView.vDataGridView();
		this.m_TabPages = new VIBlend.WinForms.Controls.vTabControl();
		this.m_PDCTab = new VIBlend.WinForms.Controls.vTabPage();
		this.m_PDCDataGridViewPivotDesign = new VIBlend.WinForms.DataGridView.vDataGridViewPivotDesign();
		this.m_PDCSplitContainer = new VIBlend.WinForms.Controls.vSplitContainer();
		this.m_TabPages.SuspendLayout();
		this.m_PDCTab.SuspendLayout();
		this.m_PDCSplitContainer.Panel1.SuspendLayout();
		this.m_PDCSplitContainer.Panel2.SuspendLayout();
		this.m_PDCSplitContainer.SuspendLayout();
		this.SuspendLayout();
		//
		//m_PDCDataGridView
		//
		this.m_PDCDataGridView.AllowAnimations = true;
		this.m_PDCDataGridView.AllowCellMerge = true;
		this.m_PDCDataGridView.AllowClipDrawing = true;
		this.m_PDCDataGridView.AllowContextMenuColumnChooser = true;
		this.m_PDCDataGridView.AllowContextMenuFiltering = true;
		this.m_PDCDataGridView.AllowContextMenuGrouping = true;
		this.m_PDCDataGridView.AllowContextMenuSorting = true;
		this.m_PDCDataGridView.AllowCopyPaste = false;
		this.m_PDCDataGridView.AllowDefaultContextMenu = true;
		this.m_PDCDataGridView.AllowDragDropIndication = true;
		this.m_PDCDataGridView.AllowHeaderItemHighlightOnCellSelection = true;
		this.m_PDCDataGridView.AutoUpdateOnListChanged = false;
		this.m_PDCDataGridView.BackColor = System.Drawing.SystemColors.Control;
		this.m_PDCDataGridView.BindingProgressEnabled = false;
		this.m_PDCDataGridView.BindingProgressSampleRate = 20000;
		this.m_PDCDataGridView.BorderColor = System.Drawing.Color.Empty;
		this.m_PDCDataGridView.CellsArea.AllowCellMerge = true;
		this.m_PDCDataGridView.CellsArea.ConditionalFormattingEnabled = false;
		this.m_PDCDataGridView.ColumnsHierarchy.AllowDragDrop = false;
		this.m_PDCDataGridView.ColumnsHierarchy.AllowResize = true;
		this.m_PDCDataGridView.ColumnsHierarchy.AutoStretchColumns = false;
		this.m_PDCDataGridView.ColumnsHierarchy.Fixed = false;
		this.m_PDCDataGridView.ColumnsHierarchy.ShowExpandCollapseButtons = true;
		this.m_PDCDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_PDCDataGridView.EnableColumnChooser = false;
		this.m_PDCDataGridView.EnableResizeToolTip = true;
		this.m_PDCDataGridView.EnableToolTips = true;
		this.m_PDCDataGridView.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
		this.m_PDCDataGridView.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
		this.m_PDCDataGridView.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
		this.m_PDCDataGridView.GroupingEnabled = false;
		this.m_PDCDataGridView.HorizontalScroll = 0;
		this.m_PDCDataGridView.HorizontalScrollBarLargeChange = 20;
		this.m_PDCDataGridView.HorizontalScrollBarSmallChange = 5;
		this.m_PDCDataGridView.ImageList = null;
		this.m_PDCDataGridView.Localization = DataGridLocalization1;
		this.m_PDCDataGridView.Location = new System.Drawing.Point(0, 0);
		this.m_PDCDataGridView.MultipleSelectionEnabled = true;
		this.m_PDCDataGridView.Name = "m_PDCDataGridView";
		this.m_PDCDataGridView.PivotColumnsTotalsEnabled = false;
		this.m_PDCDataGridView.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
		this.m_PDCDataGridView.PivotRowsTotalsEnabled = false;
		this.m_PDCDataGridView.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
		this.m_PDCDataGridView.RowsHierarchy.AllowDragDrop = false;
		this.m_PDCDataGridView.RowsHierarchy.AllowResize = true;
		this.m_PDCDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = false;
		this.m_PDCDataGridView.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
		this.m_PDCDataGridView.RowsHierarchy.Fixed = false;
		this.m_PDCDataGridView.RowsHierarchy.ShowExpandCollapseButtons = true;
		this.m_PDCDataGridView.ScrollBarsEnabled = true;
		this.m_PDCDataGridView.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
		this.m_PDCDataGridView.SelectionBorderEnabled = true;
		this.m_PDCDataGridView.SelectionBorderWidth = 2;
		this.m_PDCDataGridView.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
		this.m_PDCDataGridView.Size = new System.Drawing.Size(343, 476);
		this.m_PDCDataGridView.TabIndex = 0;
		this.m_PDCDataGridView.Text = "VDataGridView1";
		this.m_PDCDataGridView.ToolTipDuration = 5000;
		this.m_PDCDataGridView.ToolTipShowDelay = 1500;
		this.m_PDCDataGridView.VerticalScroll = 0;
		this.m_PDCDataGridView.VerticalScrollBarLargeChange = 20;
		this.m_PDCDataGridView.VerticalScrollBarSmallChange = 5;
		this.m_PDCDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_PDCDataGridView.VirtualModeCellDefault = false;
		//
		//m_TabPages
		//
		this.m_TabPages.AllowAnimations = true;
		this.m_TabPages.Controls.Add(this.m_PDCTab);
		this.m_TabPages.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_TabPages.Location = new System.Drawing.Point(0, 0);
		this.m_TabPages.Name = "m_TabPages";
		this.m_TabPages.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
		this.m_TabPages.Size = new System.Drawing.Size(699, 514);
		this.m_TabPages.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top;
		this.m_TabPages.TabIndex = 1;
		this.m_TabPages.TabPages.Add(this.m_PDCTab);
		this.m_TabPages.TabsAreaBackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(247)), Convert.ToInt32(Convert.ToByte(243)), Convert.ToInt32(Convert.ToByte(247)));
		this.m_TabPages.TabsInitialOffset = 15;
		this.m_TabPages.TitleHeight = 30;
		this.m_TabPages.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_PDCTab
		//
		this.m_PDCTab.Controls.Add(this.m_PDCSplitContainer);
		this.m_PDCTab.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_PDCTab.Location = new System.Drawing.Point(0, 30);
		this.m_PDCTab.Name = "m_PDCTab";
		this.m_PDCTab.Padding = new System.Windows.Forms.Padding(0);
		this.m_PDCTab.Size = new System.Drawing.Size(699, 484);
		this.m_PDCTab.TabIndex = 3;
		this.m_PDCTab.Text = "PDC";
		this.m_PDCTab.TooltipText = "TabPage";
		this.m_PDCTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_PDCTab.Visible = false;
		//
		//m_PDCDataGridViewPivotDesign
		//
		this.m_PDCDataGridViewPivotDesign.AreasOrientation = System.Windows.Forms.Orientation.Horizontal;
		this.m_PDCDataGridViewPivotDesign.DataGridView = null;
		this.m_PDCDataGridViewPivotDesign.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_PDCDataGridViewPivotDesign.Localization = PivotDesignPanelLocalization1;
		this.m_PDCDataGridViewPivotDesign.Location = new System.Drawing.Point(0, 0);
		this.m_PDCDataGridViewPivotDesign.Name = "m_PDCDataGridViewPivotDesign";
		this.m_PDCDataGridViewPivotDesign.Size = new System.Drawing.Size(344, 476);
		this.m_PDCDataGridViewPivotDesign.TabIndex = 1;
		this.m_PDCDataGridViewPivotDesign.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_PDCSplitContainer
		//
		this.m_PDCSplitContainer.AllowAnimations = true;
		this.m_PDCSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_PDCSplitContainer.Location = new System.Drawing.Point(4, 4);
		this.m_PDCSplitContainer.Name = "m_PDCSplitContainer";
		this.m_PDCSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
		//
		//m_PDCSplitContainer.Panel1
		//
		this.m_PDCSplitContainer.Panel1.BackColor = System.Drawing.Color.White;
		this.m_PDCSplitContainer.Panel1.BorderColor = System.Drawing.Color.Silver;
		this.m_PDCSplitContainer.Panel1.Controls.Add(this.m_PDCDataGridView);
		this.m_PDCSplitContainer.Panel1.Location = new System.Drawing.Point(0, 0);
		this.m_PDCSplitContainer.Panel1.Name = "Panel1";
		this.m_PDCSplitContainer.Panel1.Size = new System.Drawing.Size(343, 476);
		this.m_PDCSplitContainer.Panel1.TabIndex = 1;
		//
		//m_PDCSplitContainer.Panel2
		//
		this.m_PDCSplitContainer.Panel2.BackColor = System.Drawing.Color.White;
		this.m_PDCSplitContainer.Panel2.BorderColor = System.Drawing.Color.Silver;
		this.m_PDCSplitContainer.Panel2.Controls.Add(this.m_PDCDataGridViewPivotDesign);
		this.m_PDCSplitContainer.Panel2.Location = new System.Drawing.Point(347, 0);
		this.m_PDCSplitContainer.Panel2.Name = "Panel2";
		this.m_PDCSplitContainer.Panel2.Size = new System.Drawing.Size(344, 476);
		this.m_PDCSplitContainer.Panel2.TabIndex = 2;
		this.m_PDCSplitContainer.Size = new System.Drawing.Size(691, 476);
		this.m_PDCSplitContainer.SplitterSize = 4;
		this.m_PDCSplitContainer.StyleKey = "Splitter";
		this.m_PDCSplitContainer.TabIndex = 2;
		this.m_PDCSplitContainer.Text = "VSplitContainer1";
		this.m_PDCSplitContainer.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		//
		//PDCPlanningUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(699, 514);
		this.Controls.Add(this.m_TabPages);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "PDCPlanningUI";
		this.Text = "Consolidation Plan de charge";
		this.m_TabPages.ResumeLayout(false);
		this.m_PDCTab.ResumeLayout(false);
		this.m_PDCSplitContainer.Panel1.ResumeLayout(false);
		this.m_PDCSplitContainer.Panel2.ResumeLayout(false);
		this.m_PDCSplitContainer.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.DataGridView.vDataGridView m_PDCDataGridView;
	internal VIBlend.WinForms.Controls.vTabControl m_TabPages;
	internal VIBlend.WinForms.Controls.vTabPage m_PDCTab;
	internal VIBlend.WinForms.Controls.vSplitContainer m_PDCSplitContainer;
	internal VIBlend.WinForms.DataGridView.vDataGridViewPivotDesign m_PDCDataGridViewPivotDesign;
}

}
