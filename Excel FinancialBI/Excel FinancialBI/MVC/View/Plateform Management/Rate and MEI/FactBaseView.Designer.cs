using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  using Model.CRUD;
  partial class FactBaseView<TVersion, TControllerType> : System.Windows.Forms.UserControl
  {

    //UserControl overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing && components != null)
        {
          components.Dispose();
        }
      }
      finally
      {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactBaseView));
      this.MenuButtonIL = new System.Windows.Forms.ImageList(this.components);
      this.m_versionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.select_version = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.m_addRatesVersionRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.m_addFolderRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.m_deleteVersionRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.m_renameBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_dgvMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_importExcelRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.m_copyValueDown = new System.Windows.Forms.ToolStripMenuItem();
      this.TableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.m_mainContainer = new System.Windows.Forms.SplitContainer();
      this.m_circularProgress = new VIBlend.WinForms.Controls.vCircularProgressBar();
      this.TableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.m_versionTopMenu = new FBI.Forms.FbiToolStripMenuItem();
      this.m_importExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionNamePanel = new System.Windows.Forms.Panel();
      this.VersionLabel = new System.Windows.Forms.Label();
      this.rates_version_TB = new System.Windows.Forms.TextBox();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_deleteBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.m_gfactMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_newGFact = new System.Windows.Forms.ToolStripMenuItem();
      this.m_deleteGFact = new System.Windows.Forms.ToolStripMenuItem();
      this.m_renameGFact = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionMenu.SuspendLayout();
      this.m_dgvMenu.SuspendLayout();
      this.TableLayoutPanel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_mainContainer)).BeginInit();
      this.m_mainContainer.Panel2.SuspendLayout();
      this.m_mainContainer.SuspendLayout();
      this.TableLayoutPanel5.SuspendLayout();
      this.MenuStrip1.SuspendLayout();
      this.m_versionNamePanel.SuspendLayout();
      this.m_gfactMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // MenuButtonIL
      // 
      this.MenuButtonIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MenuButtonIL.ImageStream")));
      this.MenuButtonIL.TransparentColor = System.Drawing.Color.Transparent;
      this.MenuButtonIL.Images.SetKeyName(0, "expand right.png");
      this.MenuButtonIL.Images.SetKeyName(1, "expandleft.png");
      this.MenuButtonIL.Images.SetKeyName(2, "favicon(120).ico");
      this.MenuButtonIL.Images.SetKeyName(3, "favicon(125).ico");
      this.MenuButtonIL.Images.SetKeyName(4, "favicon(217).ico");
      this.MenuButtonIL.Images.SetKeyName(5, "favicon(126).ico");
      // 
      // m_versionMenu
      // 
      this.m_versionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.select_version,
            this.ToolStripSeparator2,
            this.m_addRatesVersionRCM,
            this.m_addFolderRCM,
            this.ToolStripSeparator6,
            this.m_deleteVersionRCM,
            this.m_renameBT});
      this.m_versionMenu.Name = "VersionsRCMenu";
      this.m_versionMenu.Size = new System.Drawing.Size(147, 126);
      // 
      // select_version
      // 
      this.select_version.Name = "select_version";
      this.select_version.Size = new System.Drawing.Size(146, 22);
      this.select_version.Text = "Select version";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(143, 6);
      // 
      // m_addRatesVersionRCM
      // 
      this.m_addRatesVersionRCM.Image = global::FBI.Properties.Resources.elements3_add;
      this.m_addRatesVersionRCM.Name = "m_addRatesVersionRCM";
      this.m_addRatesVersionRCM.Size = new System.Drawing.Size(146, 22);
      this.m_addRatesVersionRCM.Text = "New version";
      // 
      // m_addFolderRCM
      // 
      this.m_addFolderRCM.Image = global::FBI.Properties.Resources.folder_open_add;
      this.m_addFolderRCM.Name = "m_addFolderRCM";
      this.m_addFolderRCM.Size = new System.Drawing.Size(146, 22);
      this.m_addFolderRCM.Text = "New folder)";
      // 
      // ToolStripSeparator6
      // 
      this.ToolStripSeparator6.Name = "ToolStripSeparator6";
      this.ToolStripSeparator6.Size = new System.Drawing.Size(143, 6);
      // 
      // m_deleteVersionRCM
      // 
      this.m_deleteVersionRCM.Image = global::FBI.Properties.Resources.elements3_delete;
      this.m_deleteVersionRCM.Name = "m_deleteVersionRCM";
      this.m_deleteVersionRCM.Size = new System.Drawing.Size(146, 22);
      this.m_deleteVersionRCM.Text = "Delete";
      // 
      // m_renameBT
      // 
      this.m_renameBT.Name = "m_renameBT";
      this.m_renameBT.Size = new System.Drawing.Size(146, 22);
      this.m_renameBT.Text = "Rename";
      // 
      // m_dgvMenu
      // 
      this.m_dgvMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_importExcelRightClick,
            this.m_copyValueDown});
      this.m_dgvMenu.Name = "dgvRCM";
      this.m_dgvMenu.Size = new System.Drawing.Size(153, 70);
      // 
      // m_importExcelRightClick
      // 
      this.m_importExcelRightClick.Image = global::FBI.Properties.Resources.excel;
      this.m_importExcelRightClick.Name = "m_importExcelRightClick";
      this.m_importExcelRightClick.Size = new System.Drawing.Size(152, 22);
      this.m_importExcelRightClick.Text = "Import";
      // 
      // m_copyValueDown
      // 
      this.m_copyValueDown.Image = global::FBI.Properties.Resources.Download;
      this.m_copyValueDown.Name = "m_copyValueDown";
      this.m_copyValueDown.Size = new System.Drawing.Size(152, 22);
      this.m_copyValueDown.Text = "Copy down";
      // 
      // TableLayoutPanel4
      // 
      this.TableLayoutPanel4.ColumnCount = 1;
      this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel4.Controls.Add(this.m_mainContainer, 0, 1);
      this.TableLayoutPanel4.Controls.Add(this.TableLayoutPanel5, 0, 0);
      this.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
      this.TableLayoutPanel4.Name = "TableLayoutPanel4";
      this.TableLayoutPanel4.RowCount = 2;
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel4.Size = new System.Drawing.Size(886, 605);
      this.TableLayoutPanel4.TabIndex = 5;
      // 
      // m_mainContainer
      // 
      this.m_mainContainer.BackColor = System.Drawing.SystemColors.Control;
      this.m_mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_mainContainer.Location = new System.Drawing.Point(0, 32);
      this.m_mainContainer.Margin = new System.Windows.Forms.Padding(0);
      this.m_mainContainer.Name = "m_mainContainer";
      // 
      // m_mainContainer.Panel2
      // 
      this.m_mainContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
      this.m_mainContainer.Panel2.Controls.Add(this.m_circularProgress);
      this.m_mainContainer.Size = new System.Drawing.Size(886, 573);
      this.m_mainContainer.SplitterDistance = 191;
      this.m_mainContainer.SplitterWidth = 3;
      this.m_mainContainer.TabIndex = 7;
      // 
      // m_circularProgress
      // 
      this.m_circularProgress.AllowAnimations = true;
      this.m_circularProgress.BackColor = System.Drawing.Color.Transparent;
      this.m_circularProgress.IndicatorsCount = 8;
      this.m_circularProgress.Location = new System.Drawing.Point(220, 208);
      this.m_circularProgress.Maximum = 100;
      this.m_circularProgress.Minimum = 0;
      this.m_circularProgress.Name = "m_circularProgress";
      this.m_circularProgress.Size = new System.Drawing.Size(85, 85);
      this.m_circularProgress.TabIndex = 0;
      this.m_circularProgress.Text = "VCircularProgressBar1";
      this.m_circularProgress.UseThemeBackground = false;
      this.m_circularProgress.Value = 0;
      this.m_circularProgress.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLUE;
      // 
      // TableLayoutPanel5
      // 
      this.TableLayoutPanel5.ColumnCount = 3;
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 97.02381F));
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.976191F));
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
      this.TableLayoutPanel5.Controls.Add(this.MenuStrip1, 0, 0);
      this.TableLayoutPanel5.Controls.Add(this.m_versionNamePanel, 2, 0);
      this.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
      this.TableLayoutPanel5.Name = "TableLayoutPanel5";
      this.TableLayoutPanel5.RowCount = 1;
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel5.Size = new System.Drawing.Size(882, 32);
      this.TableLayoutPanel5.TabIndex = 2;
      // 
      // MenuStrip1
      // 
      this.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_versionTopMenu,
            this.m_importExcelMenu,
            this.ToolStripMenuItem1});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(169, 24);
      this.MenuStrip1.TabIndex = 5;
      this.MenuStrip1.Text = "MenuStrip1";
      // 
      // m_versionTopMenu
      // 
      this.m_versionTopMenu.Image = global::FBI.Properties.Resources.elements2;
      this.m_versionTopMenu.Name = "m_versionTopMenu";
      this.m_versionTopMenu.Size = new System.Drawing.Size(78, 20);
      this.m_versionTopMenu.Text = "Versions";
      // 
      // m_importExcelMenu
      // 
      this.m_importExcelMenu.Image = global::FBI.Properties.Resources.excel_blue2;
      this.m_importExcelMenu.Name = "m_importExcelMenu";
      this.m_importExcelMenu.Size = new System.Drawing.Size(71, 20);
      this.m_importExcelMenu.Text = "Import";
      // 
      // ToolStripMenuItem1
      // 
      this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
      this.ToolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
      // 
      // m_versionNamePanel
      // 
      this.m_versionNamePanel.Controls.Add(this.VersionLabel);
      this.m_versionNamePanel.Controls.Add(this.rates_version_TB);
      this.m_versionNamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_versionNamePanel.Location = new System.Drawing.Point(672, 0);
      this.m_versionNamePanel.Margin = new System.Windows.Forms.Padding(0);
      this.m_versionNamePanel.Name = "m_versionNamePanel";
      this.m_versionNamePanel.Size = new System.Drawing.Size(210, 32);
      this.m_versionNamePanel.TabIndex = 6;
      // 
      // VersionLabel
      // 
      this.VersionLabel.AutoSize = true;
      this.VersionLabel.Location = new System.Drawing.Point(8, 7);
      this.VersionLabel.Name = "VersionLabel";
      this.VersionLabel.Size = new System.Drawing.Size(42, 13);
      this.VersionLabel.TabIndex = 3;
      this.VersionLabel.Text = "Version";
      // 
      // rates_version_TB
      // 
      this.rates_version_TB.Enabled = false;
      this.rates_version_TB.Location = new System.Drawing.Point(54, 3);
      this.rates_version_TB.Margin = new System.Windows.Forms.Padding(1);
      this.rates_version_TB.Name = "rates_version_TB";
      this.rates_version_TB.Size = new System.Drawing.Size(130, 20);
      this.rates_version_TB.TabIndex = 2;
      // 
      // m_versionsTreeviewImageList
      // 
      this.m_versionsTreeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_versionsTreeviewImageList.ImageStream")));
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // m_gfactMenu
      // 
      this.m_gfactMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_newGFact,
            this.m_deleteGFact,
            this.m_renameGFact});
      this.m_gfactMenu.Name = "m_gfactMenu";
      this.m_gfactMenu.Size = new System.Drawing.Size(118, 70);
      // 
      // m_newGFact
      // 
      this.m_newGFact.Image = global::FBI.Properties.Resources.elements_add;
      this.m_newGFact.Name = "m_newGFact";
      this.m_newGFact.Size = new System.Drawing.Size(117, 22);
      this.m_newGFact.Text = "New";
      // 
      // m_deleteGFact
      // 
      this.m_deleteGFact.Image = global::FBI.Properties.Resources.elements_delete;
      this.m_deleteGFact.Name = "m_deleteGFact";
      this.m_deleteGFact.Size = new System.Drawing.Size(117, 22);
      this.m_deleteGFact.Text = "Delete";
      // 
      // m_renameGFact
      // 
      this.m_renameGFact.Name = "m_renameGFact";
      this.m_renameGFact.Size = new System.Drawing.Size(117, 22);
      this.m_renameGFact.Text = "Rename";
      // 
      // FactBaseView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.Controls.Add(this.TableLayoutPanel4);
      this.Name = "FactBaseView";
      this.Size = new System.Drawing.Size(886, 605);
      this.m_versionMenu.ResumeLayout(false);
      this.m_dgvMenu.ResumeLayout(false);
      this.TableLayoutPanel4.ResumeLayout(false);
      this.m_mainContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.m_mainContainer)).EndInit();
      this.m_mainContainer.ResumeLayout(false);
      this.TableLayoutPanel5.ResumeLayout(false);
      this.TableLayoutPanel5.PerformLayout();
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.m_versionNamePanel.ResumeLayout(false);
      this.m_versionNamePanel.PerformLayout();
      this.m_gfactMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.ImageList MenuButtonIL;
    internal System.Windows.Forms.ContextMenuStrip m_versionMenu;
    internal System.Windows.Forms.ToolStripMenuItem select_version;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem m_addRatesVersionRCM;
    internal System.Windows.Forms.ToolStripMenuItem m_addFolderRCM;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteVersionRCM;
    internal System.Windows.Forms.ContextMenuStrip m_dgvMenu;
    internal System.Windows.Forms.ToolStripMenuItem m_copyValueDown;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel4;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel5;
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem m_importExcelMenu;
    internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
    internal FBI.Forms.FbiToolStripMenuItem m_versionTopMenu;
    internal System.Windows.Forms.Label VersionLabel;
    internal System.Windows.Forms.TextBox rates_version_TB;
    internal System.Windows.Forms.SplitContainer m_mainContainer;
    internal System.Windows.Forms.Panel m_versionNamePanel;
    internal System.Windows.Forms.ToolStripMenuItem m_renameBT;
    internal System.Windows.Forms.ToolStripMenuItem m_importExcelRightClick;
    internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    internal VIBlend.WinForms.Controls.vCircularProgressBar m_circularProgress;

    internal System.ComponentModel.BackgroundWorker m_deleteBackgroundWorker;
    protected System.Windows.Forms.ToolStripMenuItem m_newGFact;
    protected System.Windows.Forms.ToolStripMenuItem m_deleteGFact;
    protected System.Windows.Forms.ToolStripMenuItem m_renameGFact;
    protected System.Windows.Forms.ContextMenuStrip m_gfactMenu;
  }
}