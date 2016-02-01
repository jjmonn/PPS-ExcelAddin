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
		this.PanelCollapseBT = new VIBlend.WinForms.Controls.vButton();
		this.m_entitySelectionLabel = new System.Windows.Forms.Label();
		this.CategoriesIL = new System.Windows.Forms.ImageList(this.components);
		this.EntitiesTVImageList = new System.Windows.Forms.ImageList(this.components);
		this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
		this.m_rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.UnselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.MainTableLayout.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer).BeginInit();
		this.SplitContainer.Panel2.SuspendLayout();
		this.SplitContainer.SuspendLayout();
		this.m_selectionTableLayout.SuspendLayout();
		this.TableLayoutPanel2.SuspendLayout();
		this.Panel1.SuspendLayout();
		this.m_rightClickMenu.SuspendLayout();
		this.SuspendLayout();
		//
		//MainTableLayout
		//
		this.MainTableLayout.ColumnCount = 1;
		this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.MainTableLayout.Controls.Add(this.SplitContainer, 0, 1);
		this.MainTableLayout.Controls.Add(this.Panel1, 0, 0);
		this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
		this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
		this.MainTableLayout.Name = "MainTableLayout";
		this.MainTableLayout.RowCount = 2;
		this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f));
		this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.MainTableLayout.Size = new System.Drawing.Size(270, 671);
		this.MainTableLayout.TabIndex = 3;
		//
		//SplitContainer
		//
		this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer.Location = new System.Drawing.Point(3, 28);
		this.SplitContainer.Name = "SplitContainer";
		this.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
		//
		//SplitContainer.Panel2
		//
		this.SplitContainer.Panel2.Controls.Add(this.m_selectionTableLayout);
		this.SplitContainer.Size = new System.Drawing.Size(264, 640);
		this.SplitContainer.SplitterDistance = 297;
		this.SplitContainer.TabIndex = 1;
		//
		//m_selectionTableLayout
		//
		this.m_selectionTableLayout.ColumnCount = 1;
		this.m_selectionTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.m_selectionTableLayout.Controls.Add(this.TableLayoutPanel2, 0, 0);
		this.m_selectionTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_selectionTableLayout.Location = new System.Drawing.Point(0, 0);
		this.m_selectionTableLayout.Name = "m_selectionTableLayout";
		this.m_selectionTableLayout.RowCount = 2;
		this.m_selectionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f));
		this.m_selectionTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.m_selectionTableLayout.Size = new System.Drawing.Size(264, 339);
		this.m_selectionTableLayout.TabIndex = 0;
		//
		//TableLayoutPanel2
		//
		this.TableLayoutPanel2.ColumnCount = 2;
		this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25f));
		this.TableLayoutPanel2.Controls.Add(this.SelectionCB, 0, 0);
		this.TableLayoutPanel2.Controls.Add(this.CollapseSelectionBT, 1, 0);
		this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
		this.TableLayoutPanel2.Name = "TableLayoutPanel2";
		this.TableLayoutPanel2.RowCount = 1;
		this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel2.Size = new System.Drawing.Size(264, 25);
		this.TableLayoutPanel2.TabIndex = 1;
		//
		//SelectionCB
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
		this.SelectionCB.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.SelectionCB.Size = new System.Drawing.Size(233, 19);
		this.SelectionCB.TabIndex = 0;
		this.SelectionCB.Text = "[CUI.selection]";
		this.SelectionCB.UseThemeBackColor = false;
		this.SelectionCB.UseThemeDropDownArrowColor = true;
		this.SelectionCB.ValueMember = "";
		this.SelectionCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		this.SelectionCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		//
		//CollapseSelectionBT
		//
		this.CollapseSelectionBT.AllowAnimations = true;
		this.CollapseSelectionBT.BackColor = System.Drawing.Color.Transparent;
		this.CollapseSelectionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.CollapseSelectionBT.ImageKey = "minus";
		this.CollapseSelectionBT.ImageList = this.ExpansionImageList;
		this.CollapseSelectionBT.Location = new System.Drawing.Point(242, 3);
		this.CollapseSelectionBT.Name = "CollapseSelectionBT";
		this.CollapseSelectionBT.PaintBorder = false;
		this.CollapseSelectionBT.RoundedCornersMask = Convert.ToByte(15);
		this.CollapseSelectionBT.Size = new System.Drawing.Size(19, 19);
		this.CollapseSelectionBT.TabIndex = 1;
		this.CollapseSelectionBT.Text = " ";
		this.CollapseSelectionBT.UseVisualStyleBackColor = false;
		this.CollapseSelectionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ExpansionImageList
		//
		this.ExpansionImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ExpansionImageList.ImageStream");
		this.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.ExpansionImageList.Images.SetKeyName(0, "add.ico");
		this.ExpansionImageList.Images.SetKeyName(1, "minus");
		//
		//Panel1
		//
		this.Panel1.Controls.Add(this.PanelCollapseBT);
		this.Panel1.Controls.Add(this.m_entitySelectionLabel);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(0, 0);
		this.Panel1.Margin = new System.Windows.Forms.Padding(0);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(270, 25);
		this.Panel1.TabIndex = 2;
		//
		//PanelCollapseBT
		//
		this.PanelCollapseBT.AllowAnimations = true;
		this.PanelCollapseBT.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.PanelCollapseBT.BackColor = System.Drawing.Color.Transparent;
		this.PanelCollapseBT.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.NONE;
		this.PanelCollapseBT.FlatAppearance.BorderSize = 0;
		this.PanelCollapseBT.ImageKey = "minus";
		this.PanelCollapseBT.ImageList = this.ExpansionImageList;
		this.PanelCollapseBT.Location = new System.Drawing.Point(247, 3);
		this.PanelCollapseBT.Name = "PanelCollapseBT";
		this.PanelCollapseBT.PaintBorder = false;
		this.PanelCollapseBT.RoundedCornersMask = Convert.ToByte(15);
		this.PanelCollapseBT.Size = new System.Drawing.Size(19, 19);
		this.PanelCollapseBT.TabIndex = 2;
		this.PanelCollapseBT.Text = " ";
		this.PanelCollapseBT.UseVisualStyleBackColor = false;
		this.PanelCollapseBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_entitySelectionLabel
		//
		this.m_entitySelectionLabel.AutoSize = true;
		this.m_entitySelectionLabel.Location = new System.Drawing.Point(3, 5);
		this.m_entitySelectionLabel.Name = "m_entitySelectionLabel";
		this.m_entitySelectionLabel.Size = new System.Drawing.Size(131, 15);
		this.m_entitySelectionLabel.TabIndex = 0;
		this.m_entitySelectionLabel.Text = "[CUI.entities_selection]";
		this.m_entitySelectionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		//
		//CategoriesIL
		//
		this.CategoriesIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("CategoriesIL.ImageStream");
		this.CategoriesIL.TransparentColor = System.Drawing.Color.Transparent;
		this.CategoriesIL.Images.SetKeyName(0, "elements.ico");
		this.CategoriesIL.Images.SetKeyName(1, "favicon(81).ico");
		//
		//EntitiesTVImageList
		//
		this.EntitiesTVImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EntitiesTVImageList.ImageStream");
		this.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico");
		this.EntitiesTVImageList.Images.SetKeyName(1, "elements_branch.ico");
		//
		//m_versionsTreeviewImageList
		//
		this.m_versionsTreeviewImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("m_versionsTreeviewImageList.ImageStream");
		this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
		this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
		//
		//m_rightClickMenu
		//
		this.m_rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.SelectAllToolStripMenuItem,
			this.UnselectAllToolStripMenuItem
		});
		this.m_rightClickMenu.Name = "periodsRightClickMenu";
		this.m_rightClickMenu.Size = new System.Drawing.Size(182, 52);
		//
		//SelectAllToolStripMenuItem
		//
		this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
		this.SelectAllToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
		this.SelectAllToolStripMenuItem.Text = "[CUI.select_all]";
		//
		//UnselectAllToolStripMenuItem
		//
		this.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem";
		this.UnselectAllToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
		this.UnselectAllToolStripMenuItem.Text = "[CUI.unselect_all]";
		//
		//CUI2LeftPane
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(207)), Convert.ToInt32(Convert.ToByte(212)), Convert.ToInt32(Convert.ToByte(221)));
		this.Controls.Add(this.MainTableLayout);
		this.Name = "CUI2LeftPane";
		this.Size = new System.Drawing.Size(270, 671);
		this.MainTableLayout.ResumeLayout(false);
		this.SplitContainer.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer).EndInit();
		this.SplitContainer.ResumeLayout(false);
		this.m_selectionTableLayout.ResumeLayout(false);
		this.TableLayoutPanel2.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		this.Panel1.PerformLayout();
		this.m_rightClickMenu.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal System.Windows.Forms.TableLayoutPanel MainTableLayout;
	internal System.Windows.Forms.Label m_entitySelectionLabel;
	internal System.Windows.Forms.SplitContainer SplitContainer;
	internal System.Windows.Forms.TableLayoutPanel m_selectionTableLayout;
	internal VIBlend.WinForms.Controls.vComboBox SelectionCB;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
	internal VIBlend.WinForms.Controls.vButton CollapseSelectionBT;
	internal System.Windows.Forms.ImageList CategoriesIL;
	public System.Windows.Forms.ImageList EntitiesTVImageList;
	internal System.Windows.Forms.Panel Panel1;
	internal VIBlend.WinForms.Controls.vButton PanelCollapseBT;
	public System.Windows.Forms.ImageList ExpansionImageList;
	internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
	internal System.Windows.Forms.ContextMenuStrip m_rightClickMenu;
	internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;

	internal System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem;
}

}
