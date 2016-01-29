using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class GlobalFactUI : System.Windows.Forms.UserControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalFactUI));
      this.MenuButtonIL = new System.Windows.Forms.ImageList(this.components);
      this.VersionsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.select_version = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.AddRatesVersionRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.AddFolderRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.DeleteVersionRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.RenameVersionBT = new System.Windows.Forms.ToolStripMenuItem();
      this.dgvRCM = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.CreateNewFact = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.CopyFactDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ImportFromExcelBT = new System.Windows.Forms.ToolStripMenuItem();
      this.TableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
      this.m_circularProgress = new VIBlend.WinForms.Controls.vCircularProgressBar();
      this.TableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.VersionLabel = new System.Windows.Forms.Label();
      this.version_TB = new System.Windows.Forms.TextBox();
      this.FactRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.CreateNewFact2 = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.RenameBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.m_importFromExcelBT2 = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_deleteBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.VersionsRCMenu.SuspendLayout();
      this.dgvRCM.SuspendLayout();
      this.TableLayoutPanel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
      this.SplitContainer1.Panel2.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      this.TableLayoutPanel5.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.FactRightClickMenu.SuspendLayout();
      this.SuspendLayout();
      //
      //MenuButtonIL
      //
      this.MenuButtonIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("MenuButtonIL.ImageStream");
      this.MenuButtonIL.TransparentColor = System.Drawing.Color.Transparent;
      this.MenuButtonIL.Images.SetKeyName(0, "expand right.png");
      this.MenuButtonIL.Images.SetKeyName(1, "expandleft.png");
      this.MenuButtonIL.Images.SetKeyName(2, "favicon(120).ico");
      this.MenuButtonIL.Images.SetKeyName(3, "favicon(125).ico");
      this.MenuButtonIL.Images.SetKeyName(4, "favicon(217).ico");
      this.MenuButtonIL.Images.SetKeyName(5, "favicon(126).ico");
      //
      //VersionsRCMenu
      //
      this.VersionsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.select_version,
			this.ToolStripSeparator2,
			this.AddRatesVersionRCM,
			this.AddFolderRCM,
			this.ToolStripSeparator6,
			this.DeleteVersionRCM,
			this.RenameVersionBT
		});
      this.VersionsRCMenu.Name = "VersionsRCMenu";
      this.VersionsRCMenu.Size = new System.Drawing.Size(150, 126);
      //
      //select_version
      //
      this.select_version.Image = global::FBI.Properties.Resources.config_circle_green;
      this.select_version.Name = "select_version";
      this.select_version.Size = new System.Drawing.Size(149, 22);
      this.select_version.Text = "Select version";
      //
      //ToolStripSeparator2
      //
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(146, 6);
      //
      //AddRatesVersionRCM
      //
      this.AddRatesVersionRCM.Image = global::FBI.Properties.Resources.elements3_add;
      this.AddRatesVersionRCM.Name = "AddRatesVersionRCM";
      this.AddRatesVersionRCM.Size = new System.Drawing.Size(149, 22);
      this.AddRatesVersionRCM.Text = "Create version";
      //
      //AddFolderRCM
      //
      this.AddFolderRCM.Image = global::FBI.Properties.Resources.folder_open_add;
      this.AddFolderRCM.Name = "AddFolderRCM";
      this.AddFolderRCM.Size = new System.Drawing.Size(149, 22);
      this.AddFolderRCM.Text = "Add folder";
      //
      //ToolStripSeparator6
      //
      this.ToolStripSeparator6.Name = "ToolStripSeparator6";
      this.ToolStripSeparator6.Size = new System.Drawing.Size(146, 6);
      //
      //DeleteVersionRCM
      //
      this.DeleteVersionRCM.Image = global::FBI.Properties.Resources.elements3_delete;
      this.DeleteVersionRCM.Name = "DeleteVersionRCM";
      this.DeleteVersionRCM.Size = new System.Drawing.Size(149, 22);
      this.DeleteVersionRCM.Text = "Delete";
      //
      //RenameVersionBT
      //
      this.RenameVersionBT.Name = "RenameVersionBT";
      this.RenameVersionBT.Size = new System.Drawing.Size(149, 22);
      this.RenameVersionBT.Text = "Delete";
      //
      //dgvRCM
      //
      this.dgvRCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CreateNewFact,
			this.ToolStripSeparator3,
			this.CopyFactDownToolStripMenuItem,
			this.ImportFromExcelBT
		});
      this.dgvRCM.Name = "dgvRCM";
      this.dgvRCM.Size = new System.Drawing.Size(136, 76);
      //
      //CreateNewFact
      //
      this.CreateNewFact.Image = global::FBI.Properties.Resources.elements_add;
      this.CreateNewFact.Name = "CreateNewFact";
      this.CreateNewFact.Size = new System.Drawing.Size(135, 22);
      this.CreateNewFact.Text = "New";
      //
      //ToolStripSeparator3
      //
      this.ToolStripSeparator3.Name = "ToolStripSeparator3";
      this.ToolStripSeparator3.Size = new System.Drawing.Size(132, 6);
      //
      //CopyFactDownToolStripMenuItem
      //
      this.CopyFactDownToolStripMenuItem.Image = global::FBI.Properties.Resources.Download;
      this.CopyFactDownToolStripMenuItem.Name = "CopyFactDownToolStripMenuItem";
      this.CopyFactDownToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.CopyFactDownToolStripMenuItem.Text = "Copy down";
      //
      //ImportFromExcelBT
      //
      this.ImportFromExcelBT.Image = global::FBI.Properties.Resources.excel_blue2;
      this.ImportFromExcelBT.Name = "ImportFromExcelBT";
      this.ImportFromExcelBT.Size = new System.Drawing.Size(135, 22);
      this.ImportFromExcelBT.Text = "Import";
      //
      //TableLayoutPanel4
      //
      this.TableLayoutPanel4.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
      this.TableLayoutPanel4.ColumnCount = 1;
      this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.TableLayoutPanel4.Controls.Add(this.SplitContainer1, 0, 1);
      this.TableLayoutPanel4.Controls.Add(this.TableLayoutPanel5, 0, 0);
      this.TableLayoutPanel4.Location = new System.Drawing.Point(0, 20);
      this.TableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
      this.TableLayoutPanel4.Name = "TableLayoutPanel4";
      this.TableLayoutPanel4.RowCount = 2;
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32f));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.TableLayoutPanel4.Size = new System.Drawing.Size(886, 585);
      this.TableLayoutPanel4.TabIndex = 5;
      //
      //SplitContainer1
      //
      this.SplitContainer1.BackColor = System.Drawing.SystemColors.Control;
      this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SplitContainer1.Location = new System.Drawing.Point(0, 32);
      this.SplitContainer1.Margin = new System.Windows.Forms.Padding(0);
      this.SplitContainer1.Name = "SplitContainer1";
      //
      //SplitContainer1.Panel2
      //
      this.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
      this.SplitContainer1.Panel2.Controls.Add(this.m_circularProgress);
      this.SplitContainer1.Size = new System.Drawing.Size(886, 553);
      this.SplitContainer1.SplitterDistance = 191;
      this.SplitContainer1.SplitterWidth = 3;
      this.SplitContainer1.TabIndex = 7;
      //
      //m_circularProgress
      //
      this.m_circularProgress.AllowAnimations = true;
      this.m_circularProgress.BackColor = System.Drawing.Color.Transparent;
      this.m_circularProgress.IndicatorsCount = 8;
      this.m_circularProgress.Location = new System.Drawing.Point(260, 239);
      this.m_circularProgress.Maximum = 100;
      this.m_circularProgress.Minimum = 0;
      this.m_circularProgress.Name = "m_circularProgress";
      this.m_circularProgress.Size = new System.Drawing.Size(85, 85);
      this.m_circularProgress.TabIndex = 1;
      this.m_circularProgress.Text = "VCircularProgressBar1";
      this.m_circularProgress.UseThemeBackground = false;
      this.m_circularProgress.Value = 0;
      this.m_circularProgress.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLUE;
      //
      //TableLayoutPanel5
      //
      this.TableLayoutPanel5.ColumnCount = 3;
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 97.02381f));
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.976191f));
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 249f));
      this.TableLayoutPanel5.Controls.Add(this.Panel1, 2, 0);
      this.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
      this.TableLayoutPanel5.Name = "TableLayoutPanel5";
      this.TableLayoutPanel5.RowCount = 1;
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.TableLayoutPanel5.Size = new System.Drawing.Size(882, 32);
      this.TableLayoutPanel5.TabIndex = 2;
      //
      //Panel1
      //
      this.Panel1.Controls.Add(this.VersionLabel);
      this.Panel1.Controls.Add(this.version_TB);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel1.Location = new System.Drawing.Point(632, 0);
      this.Panel1.Margin = new System.Windows.Forms.Padding(0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(250, 32);
      this.Panel1.TabIndex = 6;
      //
      //VersionLabel
      //
      this.VersionLabel.AutoSize = true;
      this.VersionLabel.Location = new System.Drawing.Point(8, 7);
      this.VersionLabel.Name = "VersionLabel";
      this.VersionLabel.Size = new System.Drawing.Size(42, 13);
      this.VersionLabel.TabIndex = 3;
      this.VersionLabel.Text = "Version";
      //
      //version_TB
      //
      this.version_TB.Enabled = false;
      this.version_TB.Location = new System.Drawing.Point(60, 6);
      this.version_TB.Margin = new System.Windows.Forms.Padding(1);
      this.version_TB.Name = "version_TB";
      this.version_TB.Size = new System.Drawing.Size(183, 20);
      this.version_TB.TabIndex = 2;
      //
      //FactRightClickMenu
      //
      this.FactRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CreateNewFact2,
			this.DeleteBT,
			this.ToolStripSeparator1,
			this.RenameBT,
			this.ToolStripSeparator4,
			this.m_importFromExcelBT2
		});
      this.FactRightClickMenu.Name = "ContextMenuStrip1";
      this.FactRightClickMenu.Size = new System.Drawing.Size(169, 104);
      //
      //CreateNewFact2
      //
      this.CreateNewFact2.Image = global::FBI.Properties.Resources.elements_add;
      this.CreateNewFact2.Name = "CreateNewFact2";
      this.CreateNewFact2.Size = new System.Drawing.Size(168, 22);
      this.CreateNewFact2.Text = "New";
      //
      //DeleteBT
      //
      this.DeleteBT.Image = global::FBI.Properties.Resources.elements_delete;
      this.DeleteBT.Name = "DeleteBT";
      this.DeleteBT.Size = new System.Drawing.Size(168, 22);
      this.DeleteBT.Text = "Delete";
      //
      //ToolStripSeparator1
      //
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(165, 6);
      //
      //RenameBT
      //
      this.RenameBT.Name = "RenameBT";
      this.RenameBT.Size = new System.Drawing.Size(168, 22);
      this.RenameBT.Text = "Rename";
      //
      //ToolStripSeparator4
      //
      this.ToolStripSeparator4.Name = "ToolStripSeparator4";
      this.ToolStripSeparator4.Size = new System.Drawing.Size(165, 6);
      //
      //m_importFromExcelBT2
      //
      this.m_importFromExcelBT2.Image = global::FBI.Properties.Resources.excel_blue2;
      this.m_importFromExcelBT2.Name = "m_importFromExcelBT2";
      this.m_importFromExcelBT2.Size = new System.Drawing.Size(168, 22);
      this.m_importFromExcelBT2.Text = "Import from Excel";
      //
      //m_versionsTreeviewImageList
      //
      this.m_versionsTreeviewImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("m_versionsTreeviewImageList.ImageStream");
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      //
      //GlobalFactUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.Controls.Add(this.TableLayoutPanel4);
      this.Name = "GlobalFactUI";
      this.Size = new System.Drawing.Size(886, 605);
      this.VersionsRCMenu.ResumeLayout(false);
      this.dgvRCM.ResumeLayout(false);
      this.TableLayoutPanel4.ResumeLayout(false);
      this.SplitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.TableLayoutPanel5.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.FactRightClickMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.ImageList MenuButtonIL;
    internal System.Windows.Forms.ContextMenuStrip VersionsRCMenu;
    internal System.Windows.Forms.ToolStripMenuItem select_version;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem AddRatesVersionRCM;
    internal System.Windows.Forms.ToolStripMenuItem AddFolderRCM;
    internal System.Windows.Forms.ToolStripMenuItem DeleteVersionRCM;
    internal System.Windows.Forms.ContextMenuStrip dgvRCM;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
    internal System.Windows.Forms.ToolStripMenuItem CopyFactDownToolStripMenuItem;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel4;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel5;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
    internal System.Windows.Forms.Label VersionLabel;
    internal System.Windows.Forms.TextBox version_TB;
    internal System.Windows.Forms.SplitContainer SplitContainer1;
    internal System.Windows.Forms.Panel Panel1;
    internal System.Windows.Forms.ContextMenuStrip FactRightClickMenu;
    internal System.Windows.Forms.ToolStripMenuItem RenameBT;
    internal System.Windows.Forms.ToolStripMenuItem DeleteBT;
    internal System.Windows.Forms.ToolStripMenuItem CreateNewFact;
    internal System.Windows.Forms.ToolStripMenuItem CreateNewFact2;
    internal System.Windows.Forms.ToolStripMenuItem RenameVersionBT;
    internal System.Windows.Forms.ToolStripMenuItem ImportFromExcelBT;
    internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    internal System.Windows.Forms.ToolStripMenuItem m_importFromExcelBT2;
    internal VIBlend.WinForms.Controls.vCircularProgressBar m_circularProgress;

    internal System.ComponentModel.BackgroundWorker m_deleteBackgroundWorker;
  }
}