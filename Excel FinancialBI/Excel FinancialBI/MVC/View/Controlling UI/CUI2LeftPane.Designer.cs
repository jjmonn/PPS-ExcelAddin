using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class CUI2LeftPane : System.Windows.Forms.UserControl
{

	//UserControl overrides dispose to clean up the component list.
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CUI2LeftPane));
      this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
      this.SplitContainer = new System.Windows.Forms.SplitContainer();
      this.m_selectionTableLayout = new System.Windows.Forms.TableLayoutPanel();
      this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.SelectionCB = new VIBlend.WinForms.Controls.vComboBox();
      this.CollapseSelectionBT = new VIBlend.WinForms.Controls.vButton();
      this.ExpansionImageList = new System.Windows.Forms.ImageList(this.components);
      this.Panel1 = new System.Windows.Forms.Panel();
      this.m_entitySelectionLabel = new System.Windows.Forms.Label();
      this.CategoriesIL = new System.Windows.Forms.ImageList(this.components);
      this.EntitiesTVImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.UnselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.Panel2 = new VIBlend.WinForms.Controls.vSplitterPanel();
      this.MainTableLayout.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
      this.SplitContainer.Panel2.SuspendLayout();
      this.SplitContainer.SuspendLayout();
      this.m_selectionTableLayout.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.m_rightClickMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainTableLayout
      // 
      this.MainTableLayout.ColumnCount = 1;
      this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.MainTableLayout.Controls.Add(this.SplitContainer, 0, 1);
      this.MainTableLayout.Controls.Add(this.Panel1, 0, 0);
      this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
      this.MainTableLayout.Name = "MainTableLayout";
      this.MainTableLayout.RowCount = 2;
      this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.MainTableLayout.Size = new System.Drawing.Size(270, 671);
      this.MainTableLayout.TabIndex = 3;
      // 
      // SplitContainer
      // 
      this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SplitContainer.Location = new System.Drawing.Point(3, 28);
      this.SplitContainer.Name = "SplitContainer";
      this.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // SplitContainer.Panel2
      // 
      this.SplitContainer.Panel2.Controls.Add(this.m_selectionTableLayout);
      this.SplitContainer.Size = new System.Drawing.Size(264, 640);
      this.SplitContainer.SplitterDistance = 296;
      this.SplitContainer.TabIndex = 1;
      // 
      // m_selectionTableLayout
      // 
      this.m_selectionTableLayout.ColumnCount = 1;
      this.m_selectionTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.m_selectionTableLayout.Controls.Add(this.TableLayoutPanel2, 0, 0);
      this.m_selectionTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_selectionTableLayout.Location = new System.Drawing.Point(0, 0);
      this.m_selectionTableLayout.Name = "m_selectionTableLayout";
      this.m_selectionTableLayout.RowCount = 2;
      this.m_selectionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.m_selectionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.m_selectionTableLayout.Size = new System.Drawing.Size(264, 340);
      this.m_selectionTableLayout.TabIndex = 0;
      // 
      // TableLayoutPanel2
      // 
      this.TableLayoutPanel2.ColumnCount = 2;
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel2.Controls.Add(this.SelectionCB, 0, 0);
      this.TableLayoutPanel2.Controls.Add(this.CollapseSelectionBT, 1, 0);
      this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 1;
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel2.Size = new System.Drawing.Size(264, 25);
      this.TableLayoutPanel2.TabIndex = 1;
      // 
      // SelectionCB
      // 
      this.SelectionCB.BackColor = System.Drawing.Color.White;
      this.SelectionCB.DisplayMember = "";
      this.SelectionCB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SelectionCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.SelectionCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.SelectionCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.SelectionCB.DropDownWidth = 233;
      this.SelectionCB.Location = new System.Drawing.Point(3, 3);
      this.SelectionCB.Name = "SelectionCB";
      this.SelectionCB.RoundedCornersMaskListItem = ((byte)(15));
      this.SelectionCB.Size = new System.Drawing.Size(233, 19);
      this.SelectionCB.TabIndex = 0;
      this.SelectionCB.Text = "[CUI.selection]";
      this.SelectionCB.UseThemeBackColor = false;
      this.SelectionCB.UseThemeDropDownArrowColor = true;
      this.SelectionCB.ValueMember = "";
      this.SelectionCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.SelectionCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // CollapseSelectionBT
      // 
      this.CollapseSelectionBT.AllowAnimations = true;
      this.CollapseSelectionBT.BackColor = System.Drawing.Color.Transparent;
      this.CollapseSelectionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CollapseSelectionBT.ImageKey = "minus";
      this.CollapseSelectionBT.ImageList = this.ExpansionImageList;
      this.CollapseSelectionBT.Location = new System.Drawing.Point(242, 3);
      this.CollapseSelectionBT.Name = "CollapseSelectionBT";
      this.CollapseSelectionBT.PaintBorder = false;
      this.CollapseSelectionBT.RoundedCornersMask = ((byte)(15));
      this.CollapseSelectionBT.Size = new System.Drawing.Size(19, 19);
      this.CollapseSelectionBT.TabIndex = 1;
      this.CollapseSelectionBT.Text = " ";
      this.CollapseSelectionBT.UseVisualStyleBackColor = false;
      this.CollapseSelectionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.CollapseSelectionBT.Click += new System.EventHandler(this.OnFilterPanelCollapseClick);
      // 
      // ExpansionImageList
      // 
      this.ExpansionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ExpansionImageList.ImageStream")));
      this.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ExpansionImageList.Images.SetKeyName(0, "add.ico");
      this.ExpansionImageList.Images.SetKeyName(1, "minus");
      // 
      // Panel1
      // 
      this.Panel1.Controls.Add(this.m_entitySelectionLabel);
      this.Panel1.Location = new System.Drawing.Point(3, 3);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(264, 19);
      this.Panel1.TabIndex = 2;
      // 
      // m_entitySelectionLabel
      // 
      this.m_entitySelectionLabel.AutoSize = true;
      this.m_entitySelectionLabel.Location = new System.Drawing.Point(3, 5);
      this.m_entitySelectionLabel.Name = "m_entitySelectionLabel";
      this.m_entitySelectionLabel.Size = new System.Drawing.Size(115, 13);
      this.m_entitySelectionLabel.TabIndex = 0;
      this.m_entitySelectionLabel.Text = "[CUI.entities_selection]";
      this.m_entitySelectionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // CategoriesIL
      // 
      this.CategoriesIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CategoriesIL.ImageStream")));
      this.CategoriesIL.TransparentColor = System.Drawing.Color.Transparent;
      this.CategoriesIL.Images.SetKeyName(0, "elements.ico");
      this.CategoriesIL.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // EntitiesTVImageList
      // 
      this.EntitiesTVImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("EntitiesTVImageList.ImageStream")));
      this.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico");
      this.EntitiesTVImageList.Images.SetKeyName(1, "elements_branch.ico");
      // 
      // m_versionsTreeviewImageList
      // 
      this.m_versionsTreeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_versionsTreeviewImageList.ImageStream")));
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // m_rightClickMenu
      // 
      this.m_rightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.m_rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAllToolStripMenuItem,
            this.UnselectAllToolStripMenuItem});
      this.m_rightClickMenu.Name = "periodsRightClickMenu";
      this.m_rightClickMenu.Size = new System.Drawing.Size(166, 48);
      // 
      // SelectAllToolStripMenuItem
      // 
      this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
      this.SelectAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.SelectAllToolStripMenuItem.Text = "[CUI.select_all]";
      // 
      // UnselectAllToolStripMenuItem
      // 
      this.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem";
      this.UnselectAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
      this.UnselectAllToolStripMenuItem.Text = "[CUI.unselect_all]";
      // 
      // Panel2
      // 
      this.Panel2.BackColor = System.Drawing.Color.White;
      this.Panel2.BorderColor = System.Drawing.Color.Silver;
      this.Panel2.Location = new System.Drawing.Point(41, 0);
      this.Panel2.Name = "Panel2";
      this.Panel2.Size = new System.Drawing.Size(34, 23);
      this.Panel2.TabIndex = 2;
      // 
      // CUI2LeftPane
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.Controls.Add(this.MainTableLayout);
      this.Name = "CUI2LeftPane";
      this.Size = new System.Drawing.Size(270, 671);
      this.MainTableLayout.ResumeLayout(false);
      this.SplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
      this.SplitContainer.ResumeLayout(false);
      this.m_selectionTableLayout.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.m_rightClickMenu.ResumeLayout(false);
      this.ResumeLayout(false);

	}
	public System.Windows.Forms.TableLayoutPanel MainTableLayout;
	public System.Windows.Forms.Label m_entitySelectionLabel;
	public System.Windows.Forms.SplitContainer SplitContainer;
	public System.Windows.Forms.TableLayoutPanel m_selectionTableLayout;
	public VIBlend.WinForms.Controls.vComboBox SelectionCB;
	public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
	public VIBlend.WinForms.Controls.vButton CollapseSelectionBT;
	public System.Windows.Forms.ImageList CategoriesIL;
  public System.Windows.Forms.ImageList EntitiesTVImageList;
	public System.Windows.Forms.ImageList ExpansionImageList;
	public System.Windows.Forms.ImageList m_versionsTreeviewImageList;
	public System.Windows.Forms.ContextMenuStrip m_rightClickMenu;
	public System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;

	public System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem;
  public System.Windows.Forms.Panel Panel1;
  private VIBlend.WinForms.Controls.vSplitterPanel Panel2;
}

}
