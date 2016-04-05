using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class ControllingUI_2 : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllingUI_2));
      this.EntitiesRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.RefreshRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.PeriodsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.UnselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AdjustmentsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.SelectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.UnselectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
      this.m_expandLeftBT = new VIBlend.WinForms.Controls.vButton();
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.MenuImageList = new System.Windows.Forms.ImageList(this.components);
      this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
      this.m_expandRightBT = new VIBlend.WinForms.Controls.vButton();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.m_entityLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_currencyLabel = new VIBlend.WinForms.Controls.vLabel();
      this.CurrencyTB = new VIBlend.WinForms.Controls.vTextBox();
      this.EntityTB = new VIBlend.WinForms.Controls.vTextBox();
      this.MainMenu = new System.Windows.Forms.MenuStrip();
      this.ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DropOnExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.BusinessControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionComparisonButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionSwitchButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_hideVersionButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_refreshButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_chartBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ExpansionImageList = new System.Windows.Forms.ImageList(this.components);
      this.EntitiesRCMenu.SuspendLayout();
      this.PeriodsRCMenu.SuspendLayout();
      this.AdjustmentsRCMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
      this.SplitContainer1.Panel1.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
      this.SplitContainer2.Panel1.SuspendLayout();
      this.SplitContainer2.Panel2.SuspendLayout();
      this.SplitContainer2.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.MainMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // EntitiesRCMenu
      // 
      this.EntitiesRCMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.EntitiesRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshRightClick});
      this.EntitiesRCMenu.Name = "ContextMenuStripEntitiesNodes";
      this.EntitiesRCMenu.Size = new System.Drawing.Size(145, 30);
      // 
      // RefreshRightClick
      // 
      this.RefreshRightClick.Image = ((System.Drawing.Image)(resources.GetObject("RefreshRightClick.Image")));
      this.RefreshRightClick.Name = "RefreshRightClick";
      this.RefreshRightClick.Size = new System.Drawing.Size(144, 26);
      this.RefreshRightClick.Text = "[CUI.refresh]";
      // 
      // PeriodsRCMenu
      // 
      this.PeriodsRCMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.PeriodsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAllToolStripMenuItem,
            this.UnselectAllToolStripMenuItem});
      this.PeriodsRCMenu.Name = "periodsRightClickMenu";
      this.PeriodsRCMenu.Size = new System.Drawing.Size(166, 48);
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
      // AdjustmentsRCMenu
      // 
      this.AdjustmentsRCMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.AdjustmentsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAllToolStripMenuItem1,
            this.UnselectAllToolStripMenuItem1});
      this.AdjustmentsRCMenu.Name = "AdjustmentsRCM";
      this.AdjustmentsRCMenu.Size = new System.Drawing.Size(166, 48);
      // 
      // SelectAllToolStripMenuItem1
      // 
      this.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1";
      this.SelectAllToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
      this.SelectAllToolStripMenuItem1.Text = "[CUI.select_all]";
      // 
      // UnselectAllToolStripMenuItem1
      // 
      this.UnselectAllToolStripMenuItem1.Name = "UnselectAllToolStripMenuItem1";
      this.UnselectAllToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
      this.UnselectAllToolStripMenuItem1.Text = "[CUI.unselect_all]";
      // 
      // SplitContainer1
      // 
      this.SplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SplitContainer1.BackColor = System.Drawing.SystemColors.Control;
      this.SplitContainer1.Location = new System.Drawing.Point(0, 3);
      this.SplitContainer1.Name = "SplitContainer1";
      // 
      // SplitContainer1.Panel1
      // 
      this.SplitContainer1.Panel1.Controls.Add(this.m_expandLeftBT);
      this.SplitContainer1.Size = new System.Drawing.Size(787, 463);
      this.SplitContainer1.SplitterDistance = 144;
      this.SplitContainer1.TabIndex = 8;
      // 
      // m_expandLeftBT
      // 
      this.m_expandLeftBT.AllowAnimations = true;
      this.m_expandLeftBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_expandLeftBT.BackColor = System.Drawing.Color.Transparent;
      this.m_expandLeftBT.Location = new System.Drawing.Point(120, 3);
      this.m_expandLeftBT.Name = "m_expandLeftBT";
      this.m_expandLeftBT.RoundedCornersMask = ((byte)(15));
      this.m_expandLeftBT.Size = new System.Drawing.Size(21, 21);
      this.m_expandLeftBT.TabIndex = 0;
      this.m_expandLeftBT.Text = "-";
      this.m_expandLeftBT.UseVisualStyleBackColor = false;
      this.m_expandLeftBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // ButtonsImageList
      // 
      this.ButtonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsImageList.ImageStream")));
      this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsImageList.Images.SetKeyName(0, "tablet_computer.ico");
      // 
      // MenuImageList
      // 
      this.MenuImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MenuImageList.ImageStream")));
      this.MenuImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.MenuImageList.Images.SetKeyName(0, "elements.ico");
      this.MenuImageList.Images.SetKeyName(1, "favicon(2).ico");
      this.MenuImageList.Images.SetKeyName(2, "element_branch2.ico");
      this.MenuImageList.Images.SetKeyName(3, "tablet_computer.ico");
      // 
      // SplitContainer2
      // 
      this.SplitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SplitContainer2.BackColor = System.Drawing.Color.Transparent;
      this.SplitContainer2.Location = new System.Drawing.Point(0, 62);
      this.SplitContainer2.Name = "SplitContainer2";
      // 
      // SplitContainer2.Panel1
      // 
      this.SplitContainer2.Panel1.Controls.Add(this.SplitContainer1);
      // 
      // SplitContainer2.Panel2
      // 
      this.SplitContainer2.Panel2.Controls.Add(this.m_expandRightBT);
      this.SplitContainer2.Size = new System.Drawing.Size(995, 469);
      this.SplitContainer2.SplitterDistance = 790;
      this.SplitContainer2.TabIndex = 8;
      // 
      // m_expandRightBT
      // 
      this.m_expandRightBT.AllowAnimations = true;
      this.m_expandRightBT.BackColor = System.Drawing.Color.Transparent;
      this.m_expandRightBT.Location = new System.Drawing.Point(3, 3);
      this.m_expandRightBT.Name = "m_expandRightBT";
      this.m_expandRightBT.RoundedCornersMask = ((byte)(15));
      this.m_expandRightBT.Size = new System.Drawing.Size(21, 21);
      this.m_expandRightBT.TabIndex = 1;
      this.m_expandRightBT.Text = "-";
      this.m_expandRightBT.UseVisualStyleBackColor = false;
      this.m_expandRightBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // Panel1
      // 
      this.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.Panel1.Controls.Add(this.m_entityLabel);
      this.Panel1.Controls.Add(this.m_currencyLabel);
      this.Panel1.Controls.Add(this.CurrencyTB);
      this.Panel1.Controls.Add(this.EntityTB);
      this.Panel1.Controls.Add(this.MainMenu);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.Panel1.Location = new System.Drawing.Point(0, 0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(995, 61);
      this.Panel1.TabIndex = 9;
      // 
      // m_entityLabel
      // 
      this.m_entityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_entityLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_entityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_entityLabel.Ellipsis = false;
      this.m_entityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_entityLabel.Location = new System.Drawing.Point(622, 9);
      this.m_entityLabel.Multiline = true;
      this.m_entityLabel.Name = "m_entityLabel";
      this.m_entityLabel.Size = new System.Drawing.Size(69, 18);
      this.m_entityLabel.TabIndex = 7;
      this.m_entityLabel.Text = "Entity";
      this.m_entityLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
      this.m_entityLabel.UseMnemonics = true;
      this.m_entityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_currencyLabel
      // 
      this.m_currencyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_currencyLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_currencyLabel.Ellipsis = false;
      this.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_currencyLabel.Location = new System.Drawing.Point(839, 9);
      this.m_currencyLabel.Multiline = true;
      this.m_currencyLabel.Name = "m_currencyLabel";
      this.m_currencyLabel.Size = new System.Drawing.Size(69, 18);
      this.m_currencyLabel.TabIndex = 6;
      this.m_currencyLabel.Text = "Currency";
      this.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
      this.m_currencyLabel.UseMnemonics = true;
      this.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // CurrencyTB
      // 
      this.CurrencyTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CurrencyTB.BackColor = System.Drawing.Color.White;
      this.CurrencyTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.CurrencyTB.DefaultText = "";
      this.CurrencyTB.Enabled = false;
      this.CurrencyTB.Location = new System.Drawing.Point(914, 5);
      this.CurrencyTB.MaxLength = 32767;
      this.CurrencyTB.Name = "CurrencyTB";
      this.CurrencyTB.PasswordChar = '\0';
      this.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.CurrencyTB.SelectionLength = 0;
      this.CurrencyTB.SelectionStart = 0;
      this.CurrencyTB.Size = new System.Drawing.Size(69, 23);
      this.CurrencyTB.TabIndex = 5;
      this.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // EntityTB
      // 
      this.EntityTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.EntityTB.BackColor = System.Drawing.Color.White;
      this.EntityTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.EntityTB.DefaultText = "";
      this.EntityTB.Enabled = false;
      this.EntityTB.Location = new System.Drawing.Point(697, 5);
      this.EntityTB.MaxLength = 32767;
      this.EntityTB.Name = "EntityTB";
      this.EntityTB.PasswordChar = '\0';
      this.EntityTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.EntityTB.SelectionLength = 0;
      this.EntityTB.SelectionStart = 0;
      this.EntityTB.Size = new System.Drawing.Size(136, 23);
      this.EntityTB.TabIndex = 4;
      this.EntityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.EntityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // MainMenu
      // 
      this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
      this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
      this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelToolStripMenuItem,
            this.BusinessControlToolStripMenuItem,
            this.m_refreshButton,
            this.m_chartBT});
      this.MainMenu.Location = new System.Drawing.Point(0, 0);
      this.MainMenu.Name = "MainMenu";
      this.MainMenu.ShowItemToolTips = true;
      this.MainMenu.Size = new System.Drawing.Size(454, 55);
      this.MainMenu.TabIndex = 0;
      this.MainMenu.Text = "[CUI.main_menu]";
      // 
      // ExcelToolStripMenuItem
      // 
      this.ExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropOnExcelToolStripMenuItem,
            this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem});
      this.ExcelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExcelToolStripMenuItem.Image")));
      this.ExcelToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem";
      this.ExcelToolStripMenuItem.Size = new System.Drawing.Size(124, 51);
      this.ExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]";
      this.ExcelToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.ExcelToolStripMenuItem.ToolTipText = "[CUI.drop_on_excel_tooltip]";
      // 
      // DropOnExcelToolStripMenuItem
      // 
      this.DropOnExcelToolStripMenuItem.Name = "DropOnExcelToolStripMenuItem";
      this.DropOnExcelToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
      this.DropOnExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]";
      // 
      // DropOnlyTheVisibleItemsOnExcelToolStripMenuItem
      // 
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Name = "DropOnlyTheVisibleItemsOnExcelToolStripMenuItem";
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = "Drop only the visible items on Excel";
      // 
      // BusinessControlToolStripMenuItem
      // 
      this.BusinessControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_versionComparisonButton,
            this.m_versionSwitchButton,
            this.m_hideVersionButton});
      this.BusinessControlToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("BusinessControlToolStripMenuItem.Image")));
      this.BusinessControlToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.BusinessControlToolStripMenuItem.Name = "BusinessControlToolStripMenuItem";
      this.BusinessControlToolStripMenuItem.Size = new System.Drawing.Size(156, 51);
      this.BusinessControlToolStripMenuItem.Text = "[CUI.performance_review]";
      this.BusinessControlToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.BusinessControlToolStripMenuItem.ToolTipText = "[CUI.performance_review_tooltip]";
      // 
      // m_versionComparisonButton
      // 
      this.m_versionComparisonButton.Name = "m_versionComparisonButton";
      this.m_versionComparisonButton.Size = new System.Drawing.Size(257, 22);
      this.m_versionComparisonButton.Text = "[CUI.display_versions_comparison]";
      // 
      // m_versionSwitchButton
      // 
      this.m_versionSwitchButton.Name = "m_versionSwitchButton";
      this.m_versionSwitchButton.Size = new System.Drawing.Size(257, 22);
      this.m_versionSwitchButton.Text = "[CUI.switch_versions]";
      // 
      // m_hideVersionButton
      // 
      this.m_hideVersionButton.Name = "m_hideVersionButton";
      this.m_hideVersionButton.Size = new System.Drawing.Size(257, 22);
      this.m_hideVersionButton.Text = "[CUI.take_off_comparison]";
      // 
      // m_refreshButton
      // 
      this.m_refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("m_refreshButton.Image")));
      this.m_refreshButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_refreshButton.Name = "m_refreshButton";
      this.m_refreshButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
      this.m_refreshButton.Size = new System.Drawing.Size(85, 51);
      this.m_refreshButton.Text = "[CUI.refresh]";
      this.m_refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.m_refreshButton.ToolTipText = "[CUI.refresh_tooltip]";
      // 
      // m_chartBT
      // 
      this.m_chartBT.Image = ((System.Drawing.Image)(resources.GetObject("m_chartBT.Image")));
      this.m_chartBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_chartBT.Name = "m_chartBT";
      this.m_chartBT.Size = new System.Drawing.Size(81, 51);
      this.m_chartBT.Text = "[CUI.charts]";
      this.m_chartBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      // 
      // ExpansionImageList
      // 
      this.ExpansionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ExpansionImageList.ImageStream")));
      this.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ExpansionImageList.Images.SetKeyName(0, "add.ico");
      this.ExpansionImageList.Images.SetKeyName(1, "minus");
      // 
      // ControllingUI_2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.ClientSize = new System.Drawing.Size(995, 529);
      this.Controls.Add(this.Panel1);
      this.Controls.Add(this.SplitContainer2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "ControllingUI_2";
      this.Text = "[CUI.financials]";
      this.EntitiesRCMenu.ResumeLayout(false);
      this.PeriodsRCMenu.ResumeLayout(false);
      this.AdjustmentsRCMenu.ResumeLayout(false);
      this.SplitContainer1.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.SplitContainer2.Panel1.ResumeLayout(false);
      this.SplitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
      this.SplitContainer2.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.MainMenu.ResumeLayout(false);
      this.MainMenu.PerformLayout();
      this.ResumeLayout(false);

	}
	public System.Windows.Forms.ContextMenuStrip EntitiesRCMenu;
	public System.Windows.Forms.ContextMenuStrip PeriodsRCMenu;
	public System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;
	public System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem;
  public System.Windows.Forms.ToolStripMenuItem RefreshRightClick;
	public System.Windows.Forms.ContextMenuStrip AdjustmentsRCMenu;
	public System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem1;
  public System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem1;
	public System.Windows.Forms.SplitContainer SplitContainer1;
	public System.Windows.Forms.ImageList MenuImageList;
	public System.Windows.Forms.SplitContainer SplitContainer2;
  public System.Windows.Forms.ImageList ButtonsImageList;
	public System.Windows.Forms.Panel Panel1;
	public System.Windows.Forms.MenuStrip MainMenu;
	public System.Windows.Forms.ToolStripMenuItem m_refreshButton;
	public System.Windows.Forms.ToolStripMenuItem BusinessControlToolStripMenuItem;
	public System.Windows.Forms.ToolStripMenuItem m_versionComparisonButton;
	public System.Windows.Forms.ToolStripMenuItem m_versionSwitchButton;
	public System.Windows.Forms.ToolStripMenuItem m_hideVersionButton;
	public System.Windows.Forms.ToolStripMenuItem ExcelToolStripMenuItem;
  public System.Windows.Forms.ToolStripMenuItem DropOnExcelToolStripMenuItem;
	public VIBlend.WinForms.Controls.vTextBox CurrencyTB;
	public VIBlend.WinForms.Controls.vTextBox EntityTB;
  public System.Windows.Forms.ImageList ExpansionImageList;
	public System.Windows.Forms.ToolStripMenuItem m_chartBT;
  public System.Windows.Forms.ToolStripMenuItem DropOnlyTheVisibleItemsOnExcelToolStripMenuItem;
  private VIBlend.WinForms.Controls.vLabel m_currencyLabel;
  private VIBlend.WinForms.Controls.vLabel m_entityLabel;
  private VIBlend.WinForms.Controls.vButton m_expandLeftBT;
  private VIBlend.WinForms.Controls.vButton m_expandRightBT;
}

}
