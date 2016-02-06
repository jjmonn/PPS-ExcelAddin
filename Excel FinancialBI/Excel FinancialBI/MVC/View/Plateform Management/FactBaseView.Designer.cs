using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class FactBaseView<TCrudType> : System.Windows.Forms.UserControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactBaseView<TCrudType>));
      this.MenuButtonIL = new System.Windows.Forms.ImageList(this.components);
      this.VersionsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.select_version = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.AddRatesVersionRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.AddFolderRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.DeleteVersionRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.RenameBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_exchangeRatesRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ImportFromExcelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.CopyRateDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.TableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.m_mainContainer = new System.Windows.Forms.SplitContainer();
      this.m_circularProgress = new VIBlend.WinForms.Controls.vCircularProgressBar();
      this.TableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.DisplayRatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.CreateFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.CreateVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ImportFromExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionNamePanel = new System.Windows.Forms.Panel();
      this.VersionLabel = new System.Windows.Forms.Label();
      this.rates_version_TB = new System.Windows.Forms.TextBox();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_deleteBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.VersionsRCMenu.SuspendLayout();
      this.m_exchangeRatesRightClickMenu.SuspendLayout();
      this.TableLayoutPanel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_mainContainer)).BeginInit();
      this.m_mainContainer.Panel2.SuspendLayout();
      this.m_mainContainer.SuspendLayout();
      this.TableLayoutPanel5.SuspendLayout();
      this.MenuStrip1.SuspendLayout();
      this.m_versionNamePanel.SuspendLayout();
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
      // VersionsRCMenu
      // 
      this.VersionsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.select_version,
            this.ToolStripSeparator2,
            this.AddRatesVersionRCM,
            this.AddFolderRCM,
            this.ToolStripSeparator6,
            this.DeleteVersionRCM,
            this.RenameBT});
      this.VersionsRCMenu.Name = "VersionsRCMenu";
      this.VersionsRCMenu.Size = new System.Drawing.Size(147, 126);
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
      // AddRatesVersionRCM
      // 
      this.AddRatesVersionRCM.Image = global::FBI.Properties.Resources.elements3_add;
      this.AddRatesVersionRCM.Name = "AddRatesVersionRCM";
      this.AddRatesVersionRCM.Size = new System.Drawing.Size(146, 22);
      this.AddRatesVersionRCM.Text = "New version";
      // 
      // AddFolderRCM
      // 
      this.AddFolderRCM.Image = global::FBI.Properties.Resources.folder_open_add;
      this.AddFolderRCM.Name = "AddFolderRCM";
      this.AddFolderRCM.Size = new System.Drawing.Size(146, 22);
      this.AddFolderRCM.Text = "New folder)";
      // 
      // ToolStripSeparator6
      // 
      this.ToolStripSeparator6.Name = "ToolStripSeparator6";
      this.ToolStripSeparator6.Size = new System.Drawing.Size(143, 6);
      // 
      // DeleteVersionRCM
      // 
      this.DeleteVersionRCM.Image = global::FBI.Properties.Resources.elements3_delete;
      this.DeleteVersionRCM.Name = "DeleteVersionRCM";
      this.DeleteVersionRCM.Size = new System.Drawing.Size(146, 22);
      this.DeleteVersionRCM.Text = "Delete";
      // 
      // RenameBT
      // 
      this.RenameBT.Name = "RenameBT";
      this.RenameBT.Size = new System.Drawing.Size(146, 22);
      this.RenameBT.Text = "Rename";
      // 
      // m_exchangeRatesRightClickMenu
      // 
      this.m_exchangeRatesRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportFromExcelToolStripMenuItem1,
            this.CopyRateDownToolStripMenuItem});
      this.m_exchangeRatesRightClickMenu.Name = "dgvRCM";
      this.m_exchangeRatesRightClickMenu.Size = new System.Drawing.Size(136, 48);
      // 
      // ImportFromExcelToolStripMenuItem1
      // 
      this.ImportFromExcelToolStripMenuItem1.Image = global::FBI.Properties.Resources.excel;
      this.ImportFromExcelToolStripMenuItem1.Name = "ImportFromExcelToolStripMenuItem1";
      this.ImportFromExcelToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
      this.ImportFromExcelToolStripMenuItem1.Text = "Import";
      // 
      // CopyRateDownToolStripMenuItem
      // 
      this.CopyRateDownToolStripMenuItem.Image = global::FBI.Properties.Resources.Download;
      this.CopyRateDownToolStripMenuItem.Name = "CopyRateDownToolStripMenuItem";
      this.CopyRateDownToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.CopyRateDownToolStripMenuItem.Text = "Copy down";
      // 
      // TableLayoutPanel4
      // 
      this.TableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TableLayoutPanel4.ColumnCount = 1;
      this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel4.Controls.Add(this.m_mainContainer, 0, 1);
      this.TableLayoutPanel4.Controls.Add(this.TableLayoutPanel5, 0, 0);
      this.TableLayoutPanel4.Location = new System.Drawing.Point(0, 20);
      this.TableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
      this.TableLayoutPanel4.Name = "TableLayoutPanel4";
      this.TableLayoutPanel4.RowCount = 2;
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel4.Size = new System.Drawing.Size(886, 585);
      this.TableLayoutPanel4.TabIndex = 5;
      // 
      // SplitContainer1
      // 
      this.m_mainContainer.BackColor = System.Drawing.SystemColors.Control;
      this.m_mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_mainContainer.Location = new System.Drawing.Point(0, 32);
      this.m_mainContainer.Margin = new System.Windows.Forms.Padding(0);
      this.m_mainContainer.Name = "SplitContainer1";
      // 
      // SplitContainer1.Panel2
      // 
      this.m_mainContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
      this.m_mainContainer.Panel2.Controls.Add(this.m_circularProgress);
      this.m_mainContainer.Size = new System.Drawing.Size(886, 553);
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
            this.ToolStripMenuItem2,
            this.ImportFromExcelToolStripMenuItem,
            this.ToolStripMenuItem1});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(169, 24);
      this.MenuStrip1.TabIndex = 5;
      this.MenuStrip1.Text = "MenuStrip1";
      // 
      // ToolStripMenuItem2
      // 
      this.ToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayRatesToolStripMenuItem,
            this.ToolStripSeparator5,
            this.CreateFolderToolStripMenuItem,
            this.CreateVersionToolStripMenuItem,
            this.ToolStripSeparator4,
            this.DeleteToolStripMenuItem});
      this.ToolStripMenuItem2.Image = global::FBI.Properties.Resources.elements2;
      this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
      this.ToolStripMenuItem2.Size = new System.Drawing.Size(78, 20);
      this.ToolStripMenuItem2.Text = "Versions";
      // 
      // DisplayRatesToolStripMenuItem
      // 
      this.DisplayRatesToolStripMenuItem.Name = "DisplayRatesToolStripMenuItem";
      this.DisplayRatesToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.DisplayRatesToolStripMenuItem.Text = "Display rates";
      // 
      // ToolStripSeparator5
      // 
      this.ToolStripSeparator5.Name = "ToolStripSeparator5";
      this.ToolStripSeparator5.Size = new System.Drawing.Size(137, 6);
      // 
      // CreateFolderToolStripMenuItem
      // 
      this.CreateFolderToolStripMenuItem.Image = global::FBI.Properties.Resources.folder_open_add;
      this.CreateFolderToolStripMenuItem.Name = "CreateFolderToolStripMenuItem";
      this.CreateFolderToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.CreateFolderToolStripMenuItem.Text = "New folder";
      // 
      // CreateVersionToolStripMenuItem
      // 
      this.CreateVersionToolStripMenuItem.Image = global::FBI.Properties.Resources.elements3_add;
      this.CreateVersionToolStripMenuItem.Name = "CreateVersionToolStripMenuItem";
      this.CreateVersionToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.CreateVersionToolStripMenuItem.Text = "New version";
      // 
      // ToolStripSeparator4
      // 
      this.ToolStripSeparator4.Name = "ToolStripSeparator4";
      this.ToolStripSeparator4.Size = new System.Drawing.Size(137, 6);
      // 
      // DeleteToolStripMenuItem
      // 
      this.DeleteToolStripMenuItem.Image = global::FBI.Properties.Resources.elements3_delete;
      this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
      this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.DeleteToolStripMenuItem.Text = "Delete";
      // 
      // ImportFromExcelToolStripMenuItem
      // 
      this.ImportFromExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.excel_blue2;
      this.ImportFromExcelToolStripMenuItem.Name = "ImportFromExcelToolStripMenuItem";
      this.ImportFromExcelToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
      this.ImportFromExcelToolStripMenuItem.Text = "Import";
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
      // FactBaseView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.Controls.Add(this.TableLayoutPanel4);
      this.Name = "FactBaseView";
      this.Size = new System.Drawing.Size(886, 605);
      this.VersionsRCMenu.ResumeLayout(false);
      this.m_exchangeRatesRightClickMenu.ResumeLayout(false);
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
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.ImageList MenuButtonIL;
    internal System.Windows.Forms.ContextMenuStrip VersionsRCMenu;
    internal System.Windows.Forms.ToolStripMenuItem select_version;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem AddRatesVersionRCM;
    internal System.Windows.Forms.ToolStripMenuItem AddFolderRCM;
    internal System.Windows.Forms.ToolStripMenuItem DeleteVersionRCM;
    internal System.Windows.Forms.ContextMenuStrip m_exchangeRatesRightClickMenu;
    internal System.Windows.Forms.ToolStripMenuItem CopyRateDownToolStripMenuItem;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel4;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel5;
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem ImportFromExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
    internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
    internal System.Windows.Forms.ToolStripMenuItem DisplayRatesToolStripMenuItem;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
    internal System.Windows.Forms.ToolStripMenuItem CreateFolderToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem CreateVersionToolStripMenuItem;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    internal System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
    internal System.Windows.Forms.Label VersionLabel;
    internal System.Windows.Forms.TextBox rates_version_TB;
    internal System.Windows.Forms.SplitContainer m_mainContainer;
    internal System.Windows.Forms.Panel m_versionNamePanel;
    internal System.Windows.Forms.ToolStripMenuItem RenameBT;
    internal System.Windows.Forms.ToolStripMenuItem ImportFromExcelToolStripMenuItem1;
    internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    internal VIBlend.WinForms.Controls.vCircularProgressBar m_circularProgress;

    internal System.ComponentModel.BackgroundWorker m_deleteBackgroundWorker;
  }
}