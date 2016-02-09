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
		this.DataGridViewsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ExpandAllRightClick = new System.Windows.Forms.ToolStripMenuItem();
		this.CollapseAllRightClick = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.LogRightClick = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.DGVFormatsButton = new System.Windows.Forms.ToolStripMenuItem();
		this.ColumnsAutoSize = new System.Windows.Forms.ToolStripMenuItem();
		this.ColumnsAutoFitBT = new System.Windows.Forms.ToolStripMenuItem();
		this.AdjustmentsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.SelectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.UnselectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.m_progressBar = new VIBlend.WinForms.Controls.vProgressBar();
		this.DGVsControlTab = new VIBlend.WinForms.Controls.vTabControl();
		this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
		this.MenuImageList = new System.Windows.Forms.ImageList(this.components);
		this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.VersionTB = new VIBlend.WinForms.Controls.vTextBox();
		this.CurrencyTB = new VIBlend.WinForms.Controls.vTextBox();
		this.EntityTB = new VIBlend.WinForms.Controls.vTextBox();
		this.m_currencyLabel = new System.Windows.Forms.Label();
		this.m_versionLabel = new System.Windows.Forms.Label();
		this.m_entityLabel = new System.Windows.Forms.Label();
		this.MainMenu = new System.Windows.Forms.MenuStrip();
		this.ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.DropOnExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.BusinessControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.VersionsComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.SwitchVersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.HideVersionsComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ChartBT = new System.Windows.Forms.ToolStripMenuItem();
		this.ExpansionImageList = new System.Windows.Forms.ImageList(this.components);
		this.EntitiesRCMenu.SuspendLayout();
		this.PeriodsRCMenu.SuspendLayout();
		this.DataGridViewsRCMenu.SuspendLayout();
		this.AdjustmentsRCMenu.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).BeginInit();
		this.SplitContainer2.Panel1.SuspendLayout();
		this.SplitContainer2.SuspendLayout();
		this.Panel1.SuspendLayout();
		this.MainMenu.SuspendLayout();
		this.SuspendLayout();
		//
		//EntitiesRCMenu
		//
		this.EntitiesRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.RefreshRightClick });
		this.EntitiesRCMenu.Name = "ContextMenuStripEntitiesNodes";
		this.EntitiesRCMenu.Size = new System.Drawing.Size(155, 28);
		//
		//RefreshRightClick
		//
		this.RefreshRightClick.Image = (System.Drawing.Image)resources.GetObject("RefreshRightClick.Image");
		this.RefreshRightClick.Name = "RefreshRightClick";
		this.RefreshRightClick.Size = new System.Drawing.Size(154, 24);
		this.RefreshRightClick.Text = "[CUI.refresh]";
		//
		//PeriodsRCMenu
		//
		this.PeriodsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.SelectAllToolStripMenuItem,
			this.UnselectAllToolStripMenuItem
		});
		this.PeriodsRCMenu.Name = "periodsRightClickMenu";
		this.PeriodsRCMenu.Size = new System.Drawing.Size(182, 52);
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
		//DataGridViewsRCMenu
		//
		this.DataGridViewsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ExpandAllRightClick,
			this.CollapseAllRightClick,
			this.ToolStripSeparator2,
			this.LogRightClick,
			this.ToolStripSeparator4,
			this.DGVFormatsButton,
			this.ColumnsAutoSize,
			this.ColumnsAutoFitBT
		});
		this.DataGridViewsRCMenu.Name = "DGVsRCM";
		this.DataGridViewsRCMenu.Size = new System.Drawing.Size(306, 160);
		//
		//ExpandAllRightClick
		//
		this.ExpandAllRightClick.Name = "ExpandAllRightClick";
		this.ExpandAllRightClick.Size = new System.Drawing.Size(305, 24);
		this.ExpandAllRightClick.Text = "[CUI.expand_all]";
		//
		//CollapseAllRightClick
		//
		this.CollapseAllRightClick.Name = "CollapseAllRightClick";
		this.CollapseAllRightClick.Size = new System.Drawing.Size(305, 24);
		this.CollapseAllRightClick.Text = "[CUI.collapse_all]";
		//
		//ToolStripSeparator2
		//
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(302, 6);
		//
		//LogRightClick
		//
		this.LogRightClick.Name = "LogRightClick";
		this.LogRightClick.Size = new System.Drawing.Size(305, 24);
		this.LogRightClick.Text = "[CUI.log]";
		//
		//ToolStripSeparator4
		//
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		this.ToolStripSeparator4.Size = new System.Drawing.Size(302, 6);
		//
		//DGVFormatsButton
		//
		this.DGVFormatsButton.Image = (System.Drawing.Image)resources.GetObject("DGVFormatsButton.Image");
		this.DGVFormatsButton.Name = "DGVFormatsButton";
		this.DGVFormatsButton.Size = new System.Drawing.Size(305, 24);
		this.DGVFormatsButton.Text = "[CUI.display_options]";
		//
		//ColumnsAutoSize
		//
		this.ColumnsAutoSize.Name = "ColumnsAutoSize";
		this.ColumnsAutoSize.Size = new System.Drawing.Size(305, 24);
		this.ColumnsAutoSize.Text = "[CUI.adjust_columns_size]";
		//
		//ColumnsAutoFitBT
		//
		this.ColumnsAutoFitBT.Checked = true;
		this.ColumnsAutoFitBT.CheckOnClick = true;
		this.ColumnsAutoFitBT.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ColumnsAutoFitBT.Name = "ColumnsAutoFitBT";
		this.ColumnsAutoFitBT.Size = new System.Drawing.Size(305, 24);
		this.ColumnsAutoFitBT.Text = "[CUI.automatic_columns_adjustment]";
		//
		//AdjustmentsRCMenu
		//
		this.AdjustmentsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.SelectAllToolStripMenuItem1,
			this.UnselectAllToolStripMenuItem1
		});
		this.AdjustmentsRCMenu.Name = "AdjustmentsRCM";
		this.AdjustmentsRCMenu.Size = new System.Drawing.Size(182, 52);
		//
		//SelectAllToolStripMenuItem1
		//
		this.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1";
		this.SelectAllToolStripMenuItem1.Size = new System.Drawing.Size(181, 24);
		this.SelectAllToolStripMenuItem1.Text = "[CUI.select_all]";
		//
		//UnselectAllToolStripMenuItem1
		//
		this.UnselectAllToolStripMenuItem1.Name = "UnselectAllToolStripMenuItem1";
		this.UnselectAllToolStripMenuItem1.Size = new System.Drawing.Size(181, 24);
		this.UnselectAllToolStripMenuItem1.Text = "[CUI.unselect_all]";
		//
		//SplitContainer1
		//
		this.SplitContainer1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(207)), Convert.ToInt32(Convert.ToByte(212)), Convert.ToInt32(Convert.ToByte(221)));
		this.SplitContainer1.Location = new System.Drawing.Point(0, 3);
		this.SplitContainer1.Name = "SplitContainer1";
		//
		//SplitContainer1.Panel2
		//
		this.SplitContainer1.Panel2.Controls.Add(this.m_progressBar);
		this.SplitContainer1.Panel2.Controls.Add(this.DGVsControlTab);
		this.SplitContainer1.Size = new System.Drawing.Size(861, 407);
		this.SplitContainer1.SplitterDistance = 159;
		this.SplitContainer1.TabIndex = 8;
		//
		//m_progressBar
		//
		this.m_progressBar.BackColor = System.Drawing.Color.Transparent;
		this.m_progressBar.Location = new System.Drawing.Point(272, 310);
		this.m_progressBar.Name = "m_progressBar";
		this.m_progressBar.RoundedCornersMask = Convert.ToByte(15);
		this.m_progressBar.Size = new System.Drawing.Size(272, 18);
		this.m_progressBar.TabIndex = 3;
		this.m_progressBar.Text = "VProgressBar1";
		this.m_progressBar.Value = 0;
		this.m_progressBar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//DGVsControlTab
		//
		this.DGVsControlTab.AllowAnimations = true;
		this.DGVsControlTab.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGVsControlTab.Location = new System.Drawing.Point(0, 0);
		this.DGVsControlTab.Name = "DGVsControlTab";
		this.DGVsControlTab.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
		this.DGVsControlTab.Size = new System.Drawing.Size(698, 407);
		this.DGVsControlTab.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top;
		this.DGVsControlTab.TabIndex = 0;
		this.DGVsControlTab.TabsAreaBackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(207)), Convert.ToInt32(Convert.ToByte(212)), Convert.ToInt32(Convert.ToByte(221)));
		this.DGVsControlTab.TabsInitialOffset = 5;
		this.DGVsControlTab.TitleHeight = 25;
		this.DGVsControlTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		//
		//ButtonsImageList
		//
		this.ButtonsImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsImageList.ImageStream");
		this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.ButtonsImageList.Images.SetKeyName(0, "tablet_computer.ico");
		//
		//MenuImageList
		//
		this.MenuImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("MenuImageList.ImageStream");
		this.MenuImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.MenuImageList.Images.SetKeyName(0, "elements.ico");
		this.MenuImageList.Images.SetKeyName(1, "favicon(2).ico");
		this.MenuImageList.Images.SetKeyName(2, "element_branch2.ico");
		this.MenuImageList.Images.SetKeyName(3, "tablet_computer.ico");
		//
		//SplitContainer2
		//
		this.SplitContainer2.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.SplitContainer2.BackColor = System.Drawing.Color.Transparent;
		this.SplitContainer2.Location = new System.Drawing.Point(0, 62);
		this.SplitContainer2.Name = "SplitContainer2";
		//
		//SplitContainer2.Panel1
		//
		this.SplitContainer2.Panel1.Controls.Add(this.SplitContainer1);
		this.SplitContainer2.Size = new System.Drawing.Size(1086, 413);
		this.SplitContainer2.SplitterDistance = 864;
		this.SplitContainer2.TabIndex = 8;
		//
		//Panel1
		//
		this.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.Panel1.Controls.Add(this.VersionTB);
		this.Panel1.Controls.Add(this.CurrencyTB);
		this.Panel1.Controls.Add(this.EntityTB);
		this.Panel1.Controls.Add(this.m_currencyLabel);
		this.Panel1.Controls.Add(this.m_versionLabel);
		this.Panel1.Controls.Add(this.m_entityLabel);
		this.Panel1.Controls.Add(this.MainMenu);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.Panel1.Location = new System.Drawing.Point(0, 0);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(1086, 61);
		this.Panel1.TabIndex = 9;
		//
		//VersionTB
		//
		this.VersionTB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.VersionTB.BackColor = System.Drawing.Color.White;
		this.VersionTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.VersionTB.DefaultText = "";
		this.VersionTB.Enabled = false;
		this.VersionTB.Location = new System.Drawing.Point(929, 5);
		this.VersionTB.MaxLength = 32767;
		this.VersionTB.Name = "VersionTB";
		this.VersionTB.PasswordChar = '\0';
		this.VersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.VersionTB.SelectionLength = 0;
		this.VersionTB.SelectionStart = 0;
		this.VersionTB.Size = new System.Drawing.Size(154, 23);
		this.VersionTB.TabIndex = 6;
		this.VersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.VersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//CurrencyTB
		//
		this.CurrencyTB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.CurrencyTB.BackColor = System.Drawing.Color.White;
		this.CurrencyTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.CurrencyTB.DefaultText = "";
		this.CurrencyTB.Enabled = false;
		this.CurrencyTB.Location = new System.Drawing.Point(800, 5);
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
		//EntityTB
		//
		this.EntityTB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.EntityTB.BackColor = System.Drawing.Color.White;
		this.EntityTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.EntityTB.DefaultText = "";
		this.EntityTB.Enabled = false;
		this.EntityTB.Location = new System.Drawing.Point(597, 5);
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
		//m_currencyLabel
		//
		this.m_currencyLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_currencyLabel.AutoSize = true;
		this.m_currencyLabel.Location = new System.Drawing.Point(739, 9);
		this.m_currencyLabel.Name = "m_currencyLabel";
		this.m_currencyLabel.Size = new System.Drawing.Size(82, 15);
		this.m_currencyLabel.TabIndex = 3;
		this.m_currencyLabel.Text = "[CUI.currency]";
		//
		//m_versionLabel
		//
		this.m_versionLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_versionLabel.AutoSize = true;
		this.m_versionLabel.Location = new System.Drawing.Point(875, 9);
		this.m_versionLabel.Name = "m_versionLabel";
		this.m_versionLabel.Size = new System.Drawing.Size(75, 15);
		this.m_versionLabel.TabIndex = 2;
		this.m_versionLabel.Text = "[CUI.version]";
		//
		//m_entityLabel
		//
		this.m_entityLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_entityLabel.AutoSize = true;
		this.m_entityLabel.Location = new System.Drawing.Point(555, 9);
		this.m_entityLabel.Name = "m_entityLabel";
		this.m_entityLabel.Size = new System.Drawing.Size(64, 15);
		this.m_entityLabel.TabIndex = 1;
		this.m_entityLabel.Text = "[CUI.entity]";
		//
		//MainMenu
		//
		this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
		this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ExcelToolStripMenuItem,
			this.BusinessControlToolStripMenuItem,
			this.RefreshToolStripMenuItem,
			this.ChartBT
		});
		this.MainMenu.Location = new System.Drawing.Point(0, 0);
		this.MainMenu.Name = "MainMenu";
		this.MainMenu.ShowItemToolTips = true;
		this.MainMenu.Size = new System.Drawing.Size(515, 59);
		this.MainMenu.TabIndex = 0;
		this.MainMenu.Text = "[CUI.main_menu]";
		//
		//ExcelToolStripMenuItem
		//
		this.ExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.DropOnExcelToolStripMenuItem,
			this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem
		});
		this.ExcelToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("ExcelToolStripMenuItem.Image");
		this.ExcelToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem";
		this.ExcelToolStripMenuItem.Size = new System.Drawing.Size(141, 55);
		this.ExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]";
		this.ExcelToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.ExcelToolStripMenuItem.ToolTipText = "[CUI.drop_on_excel_tooltip]";
		//
		//DropOnExcelToolStripMenuItem
		//
		this.DropOnExcelToolStripMenuItem.Name = "DropOnExcelToolStripMenuItem";
		this.DropOnExcelToolStripMenuItem.Size = new System.Drawing.Size(294, 24);
		this.DropOnExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]";
		//
		//DropOnlyTheVisibleItemsOnExcelToolStripMenuItem
		//
		this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Name = "DropOnlyTheVisibleItemsOnExcelToolStripMenuItem";
		this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Size = new System.Drawing.Size(294, 24);
		this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = "Drop only the visible items on Excel";
		//
		//BusinessControlToolStripMenuItem
		//
		this.BusinessControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.VersionsComparisonToolStripMenuItem,
			this.SwitchVersionsToolStripMenuItem,
			this.HideVersionsComparisonToolStripMenuItem
		});
		this.BusinessControlToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("BusinessControlToolStripMenuItem.Image");
		this.BusinessControlToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.BusinessControlToolStripMenuItem.Name = "BusinessControlToolStripMenuItem";
		this.BusinessControlToolStripMenuItem.Size = new System.Drawing.Size(177, 55);
		this.BusinessControlToolStripMenuItem.Text = "[CUI.performance_review]";
		this.BusinessControlToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.BusinessControlToolStripMenuItem.ToolTipText = "[CUI.performance_review_tooltip]";
		//
		//VersionsComparisonToolStripMenuItem
		//
		this.VersionsComparisonToolStripMenuItem.Name = "VersionsComparisonToolStripMenuItem";
		this.VersionsComparisonToolStripMenuItem.Size = new System.Drawing.Size(287, 24);
		this.VersionsComparisonToolStripMenuItem.Text = "[CUI.display_versions_comparison]";
		//
		//SwitchVersionsToolStripMenuItem
		//
		this.SwitchVersionsToolStripMenuItem.Name = "SwitchVersionsToolStripMenuItem";
		this.SwitchVersionsToolStripMenuItem.Size = new System.Drawing.Size(287, 24);
		this.SwitchVersionsToolStripMenuItem.Text = "[CUI.switch_versions]";
		//
		//HideVersionsComparisonToolStripMenuItem
		//
		this.HideVersionsComparisonToolStripMenuItem.Name = "HideVersionsComparisonToolStripMenuItem";
		this.HideVersionsComparisonToolStripMenuItem.Size = new System.Drawing.Size(287, 24);
		this.HideVersionsComparisonToolStripMenuItem.Text = "[CUI.take_off_comparison]";
		//
		//RefreshToolStripMenuItem
		//
		this.RefreshToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("RefreshToolStripMenuItem.Image");
		this.RefreshToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
		this.RefreshToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space);
		this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(97, 55);
		this.RefreshToolStripMenuItem.Text = "[CUI.refresh]";
		this.RefreshToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.RefreshToolStripMenuItem.ToolTipText = "[CUI.refresh_tooltip]";
		//
		//ChartBT
		//
		this.ChartBT.Image = global::FBI.Properties.Resources.chart_pie;
		this.ChartBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
		this.ChartBT.Name = "ChartBT";
		this.ChartBT.Size = new System.Drawing.Size(92, 55);
		this.ChartBT.Text = "[CUI.charts]";
		this.ChartBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		//
		//ExpansionImageList
		//
		this.ExpansionImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ExpansionImageList.ImageStream");
		this.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.ExpansionImageList.Images.SetKeyName(0, "add.ico");
		this.ExpansionImageList.Images.SetKeyName(1, "minus");
		//
		//ControllingUI_2
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(207)), Convert.ToInt32(Convert.ToByte(212)), Convert.ToInt32(Convert.ToByte(221)));
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.ClientSize = new System.Drawing.Size(1086, 475);
		this.Controls.Add(this.Panel1);
		this.Controls.Add(this.SplitContainer2);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Margin = new System.Windows.Forms.Padding(2);
		this.Name = "ControllingUI_2";
		this.Text = "[CUI.financials]";
		this.EntitiesRCMenu.ResumeLayout(false);
		this.PeriodsRCMenu.ResumeLayout(false);
		this.DataGridViewsRCMenu.ResumeLayout(false);
		this.AdjustmentsRCMenu.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.SplitContainer2.Panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).EndInit();
		this.SplitContainer2.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		this.Panel1.PerformLayout();
		this.MainMenu.ResumeLayout(false);
		this.MainMenu.PerformLayout();
		this.ResumeLayout(false);

	}
	internal System.Windows.Forms.ContextMenuStrip EntitiesRCMenu;
	internal System.Windows.Forms.ContextMenuStrip PeriodsRCMenu;
	internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem RefreshRightClick;
	internal System.Windows.Forms.ContextMenuStrip DataGridViewsRCMenu;
	internal System.Windows.Forms.ToolStripMenuItem LogRightClick;
	internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
	internal System.Windows.Forms.ContextMenuStrip AdjustmentsRCMenu;
	internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem1;
	internal System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem1;
	internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
	internal System.Windows.Forms.ToolStripMenuItem DGVFormatsButton;
	internal System.Windows.Forms.SplitContainer SplitContainer1;
	internal System.Windows.Forms.ImageList MenuImageList;
	internal System.Windows.Forms.SplitContainer SplitContainer2;
	internal System.Windows.Forms.ImageList ButtonsImageList;
	internal VIBlend.WinForms.Controls.vTabControl DGVsControlTab;
	internal System.Windows.Forms.Panel Panel1;
	internal System.Windows.Forms.MenuStrip MainMenu;
	internal System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem BusinessControlToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem VersionsComparisonToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem SwitchVersionsToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem HideVersionsComparisonToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem ExcelToolStripMenuItem;
	internal System.Windows.Forms.ToolStripMenuItem DropOnExcelToolStripMenuItem;
	internal System.Windows.Forms.Label m_currencyLabel;
	internal System.Windows.Forms.Label m_versionLabel;
	internal System.Windows.Forms.Label m_entityLabel;
	internal VIBlend.WinForms.Controls.vTextBox VersionTB;
	internal VIBlend.WinForms.Controls.vTextBox CurrencyTB;
	internal VIBlend.WinForms.Controls.vTextBox EntityTB;
	public System.Windows.Forms.ImageList ExpansionImageList;
	internal System.Windows.Forms.ToolStripMenuItem ColumnsAutoFitBT;
	internal System.Windows.Forms.ToolStripMenuItem ColumnsAutoSize;
	internal System.Windows.Forms.ToolStripMenuItem ExpandAllRightClick;
	internal System.Windows.Forms.ToolStripMenuItem CollapseAllRightClick;
	internal System.Windows.Forms.ToolStripMenuItem ChartBT;
	internal System.Windows.Forms.ToolStripMenuItem DropOnlyTheVisibleItemsOnExcelToolStripMenuItem;
	internal VIBlend.WinForms.Controls.vProgressBar m_progressBar;
}

}