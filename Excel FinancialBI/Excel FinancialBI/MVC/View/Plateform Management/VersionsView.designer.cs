using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class VersionsView : System.Windows.Forms.UserControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionsView));
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.RCM_TV = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_new_VersionRCMButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_copyVersionRCMButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_newFolderRCMButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.m_renameRCMButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_deleteRCMButton = new System.Windows.Forms.ToolStripMenuItem();
      this.TableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
      this.m_versionsTVPanel = new System.Windows.Forms.Panel();
      this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.m_globalFactsVersionLabel = new System.Windows.Forms.Label();
      this.m_exchangeRatesVersionLabel = new System.Windows.Forms.Label();
      this.m_numberOfYearsLabel = new System.Windows.Forms.Label();
      this.m_startingPeriodLabel = new System.Windows.Forms.Label();
      this.m_periodConfigLabel = new System.Windows.Forms.Label();
      this.m_nameLabel = new System.Windows.Forms.Label();
      this.m_CreationDateTextbox = new System.Windows.Forms.TextBox();
      this.m_lockedLabel = new System.Windows.Forms.Label();
      this.m_lockedDateLabel = new System.Windows.Forms.Label();
      this.LockedDateT = new System.Windows.Forms.TextBox();
      this.m_nameTextbox = new System.Windows.Forms.TextBox();
      this.m_creationDateLabel = new System.Windows.Forms.Label();
      this.lockedCB = new System.Windows.Forms.CheckBox();
      this.m_timeConfigTB = new System.Windows.Forms.TextBox();
      this.m_startPeriodTextbox = new System.Windows.Forms.ComboBox();
      this.m_nbPeriodsTextbox = new System.Windows.Forms.TextBox();
      this.m_exchangeRatesVersionVTreeviewbox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_factsVersionVTreeviewbox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.VersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_newVersionMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_newFolderMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.m_renameMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.m_deleteVersionMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.RCM_TV.SuspendLayout();
      this.TableLayoutPanel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
      this.SplitContainer1.Panel1.SuspendLayout();
      this.SplitContainer1.Panel2.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.MenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ButtonsImageList
      // 
      this.ButtonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsImageList.ImageStream")));
      this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsImageList.Images.SetKeyName(0, "add blue.jpg");
      this.ButtonsImageList.Images.SetKeyName(1, "favicon(188).ico");
      this.ButtonsImageList.Images.SetKeyName(2, "favicon(81).ico");
      this.ButtonsImageList.Images.SetKeyName(3, "imageres_89.ico");
      this.ButtonsImageList.Images.SetKeyName(4, "favicon(2).ico");
      // 
      // RCM_TV
      // 
      this.RCM_TV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_new_VersionRCMButton,
            this.m_copyVersionRCMButton,
            this.m_newFolderRCMButton,
            this.ToolStripSeparator2,
            this.m_renameRCMButton,
            this.ToolStripSeparator1,
            this.m_deleteRCMButton});
      this.RCM_TV.Name = "RCM_TV";
      this.RCM_TV.Size = new System.Drawing.Size(159, 136);
      // 
      // m_new_VersionRCMButton
      // 
      this.m_new_VersionRCMButton.Image = global::FBI.Properties.Resources.elements3_add;
      this.m_new_VersionRCMButton.Name = "m_new_VersionRCMButton";
      this.m_new_VersionRCMButton.Size = new System.Drawing.Size(158, 24);
      this.m_new_VersionRCMButton.Text = "New_version";
      // 
      // m_copyVersionRCMButton
      // 
      this.m_copyVersionRCMButton.Name = "m_copyVersionRCMButton";
      this.m_copyVersionRCMButton.Size = new System.Drawing.Size(158, 24);
      this.m_copyVersionRCMButton.Text = "Copy version";
      // 
      // m_newFolderRCMButton
      // 
      this.m_newFolderRCMButton.Image = global::FBI.Properties.Resources.folder2;
      this.m_newFolderRCMButton.Name = "m_newFolderRCMButton";
      this.m_newFolderRCMButton.Size = new System.Drawing.Size(158, 24);
      this.m_newFolderRCMButton.Text = "New_folder";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(155, 6);
      // 
      // m_renameRCMButton
      // 
      this.m_renameRCMButton.Name = "m_renameRCMButton";
      this.m_renameRCMButton.Size = new System.Drawing.Size(158, 24);
      this.m_renameRCMButton.Text = "Rename";
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(155, 6);
      // 
      // m_deleteRCMButton
      // 
      this.m_deleteRCMButton.Image = global::FBI.Properties.Resources.elements3_delete;
      this.m_deleteRCMButton.Name = "m_deleteRCMButton";
      this.m_deleteRCMButton.Size = new System.Drawing.Size(158, 24);
      this.m_deleteRCMButton.Text = "Delete";
      // 
      // TableLayoutPanel3
      // 
      this.TableLayoutPanel3.ColumnCount = 1;
      this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel3.Controls.Add(this.SplitContainer1, 0, 1);
      this.TableLayoutPanel3.Controls.Add(this.MenuStrip1, 0, 0);
      this.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel3.Name = "TableLayoutPanel3";
      this.TableLayoutPanel3.RowCount = 2;
      this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.863813F));
      this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.13618F));
      this.TableLayoutPanel3.Size = new System.Drawing.Size(1049, 635);
      this.TableLayoutPanel3.TabIndex = 2;
      // 
      // SplitContainer1
      // 
      this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SplitContainer1.Location = new System.Drawing.Point(3, 33);
      this.SplitContainer1.Name = "SplitContainer1";
      // 
      // SplitContainer1.Panel1
      // 
      this.SplitContainer1.Panel1.Controls.Add(this.m_versionsTVPanel);
      // 
      // SplitContainer1.Panel2
      // 
      this.SplitContainer1.Panel2.Controls.Add(this.TableLayoutPanel2);
      this.SplitContainer1.Size = new System.Drawing.Size(1043, 599);
      this.SplitContainer1.SplitterDistance = 293;
      this.SplitContainer1.TabIndex = 2;
      // 
      // m_versionsTVPanel
      // 
      this.m_versionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_versionsTVPanel.Location = new System.Drawing.Point(0, 0);
      this.m_versionsTVPanel.Name = "m_versionsTVPanel";
      this.m_versionsTVPanel.Size = new System.Drawing.Size(293, 599);
      this.m_versionsTVPanel.TabIndex = 1;
      // 
      // TableLayoutPanel2
      // 
      this.TableLayoutPanel2.ColumnCount = 2;
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.32902F));
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.67098F));
      this.TableLayoutPanel2.Controls.Add(this.m_globalFactsVersionLabel, 0, 8);
      this.TableLayoutPanel2.Controls.Add(this.m_exchangeRatesVersionLabel, 0, 7);
      this.TableLayoutPanel2.Controls.Add(this.m_numberOfYearsLabel, 0, 6);
      this.TableLayoutPanel2.Controls.Add(this.m_startingPeriodLabel, 0, 5);
      this.TableLayoutPanel2.Controls.Add(this.m_periodConfigLabel, 0, 4);
      this.TableLayoutPanel2.Controls.Add(this.m_nameLabel, 0, 0);
      this.TableLayoutPanel2.Controls.Add(this.m_CreationDateTextbox, 1, 1);
      this.TableLayoutPanel2.Controls.Add(this.m_lockedLabel, 0, 2);
      this.TableLayoutPanel2.Controls.Add(this.m_lockedDateLabel, 0, 3);
      this.TableLayoutPanel2.Controls.Add(this.LockedDateT, 1, 3);
      this.TableLayoutPanel2.Controls.Add(this.m_nameTextbox, 1, 0);
      this.TableLayoutPanel2.Controls.Add(this.m_creationDateLabel, 0, 1);
      this.TableLayoutPanel2.Controls.Add(this.lockedCB, 1, 2);
      this.TableLayoutPanel2.Controls.Add(this.m_timeConfigTB, 1, 4);
      this.TableLayoutPanel2.Controls.Add(this.m_startPeriodTextbox, 1, 5);
      this.TableLayoutPanel2.Controls.Add(this.m_nbPeriodsTextbox, 1, 6);
      this.TableLayoutPanel2.Controls.Add(this.m_exchangeRatesVersionVTreeviewbox, 1, 7);
      this.TableLayoutPanel2.Controls.Add(this.m_factsVersionVTreeviewbox, 1, 8);
      this.TableLayoutPanel2.Location = new System.Drawing.Point(28, 18);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 9;
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
      this.TableLayoutPanel2.Size = new System.Drawing.Size(679, 392);
      this.TableLayoutPanel2.TabIndex = 0;
      // 
      // m_globalFactsVersionLabel
      // 
      this.m_globalFactsVersionLabel.AutoSize = true;
      this.m_globalFactsVersionLabel.Location = new System.Drawing.Point(3, 359);
      this.m_globalFactsVersionLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_globalFactsVersionLabel.Name = "m_globalFactsVersionLabel";
      this.m_globalFactsVersionLabel.Size = new System.Drawing.Size(113, 15);
      this.m_globalFactsVersionLabel.TabIndex = 24;
      this.m_globalFactsVersionLabel.Text = "Global facts version";
      // 
      // m_exchangeRatesVersionLabel
      // 
      this.m_exchangeRatesVersionLabel.AutoSize = true;
      this.m_exchangeRatesVersionLabel.Location = new System.Drawing.Point(3, 315);
      this.m_exchangeRatesVersionLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_exchangeRatesVersionLabel.Name = "m_exchangeRatesVersionLabel";
      this.m_exchangeRatesVersionLabel.Size = new System.Drawing.Size(134, 15);
      this.m_exchangeRatesVersionLabel.TabIndex = 22;
      this.m_exchangeRatesVersionLabel.Text = "Exchange rates version";
      // 
      // m_numberOfYearsLabel
      // 
      this.m_numberOfYearsLabel.AutoSize = true;
      this.m_numberOfYearsLabel.Location = new System.Drawing.Point(3, 271);
      this.m_numberOfYearsLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_numberOfYearsLabel.Name = "m_numberOfYearsLabel";
      this.m_numberOfYearsLabel.Size = new System.Drawing.Size(109, 15);
      this.m_numberOfYearsLabel.TabIndex = 20;
      this.m_numberOfYearsLabel.Text = "Number of periods";
      // 
      // m_startingPeriodLabel
      // 
      this.m_startingPeriodLabel.AutoSize = true;
      this.m_startingPeriodLabel.Location = new System.Drawing.Point(3, 227);
      this.m_startingPeriodLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_startingPeriodLabel.Name = "m_startingPeriodLabel";
      this.m_startingPeriodLabel.Size = new System.Drawing.Size(87, 15);
      this.m_startingPeriodLabel.TabIndex = 17;
      this.m_startingPeriodLabel.Text = "Starting period";
      // 
      // m_periodConfigLabel
      // 
      this.m_periodConfigLabel.AutoSize = true;
      this.m_periodConfigLabel.Location = new System.Drawing.Point(3, 183);
      this.m_periodConfigLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_periodConfigLabel.Name = "m_periodConfigLabel";
      this.m_periodConfigLabel.Size = new System.Drawing.Size(79, 15);
      this.m_periodConfigLabel.TabIndex = 15;
      this.m_periodConfigLabel.Text = "Period config";
      // 
      // m_nameLabel
      // 
      this.m_nameLabel.AutoSize = true;
      this.m_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_nameLabel.Location = new System.Drawing.Point(3, 7);
      this.m_nameLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_nameLabel.Name = "m_nameLabel";
      this.m_nameLabel.Size = new System.Drawing.Size(95, 15);
      this.m_nameLabel.TabIndex = 7;
      this.m_nameLabel.Text = "Version name";
      // 
      // m_CreationDateTextbox
      // 
      this.m_CreationDateTextbox.Enabled = false;
      this.m_CreationDateTextbox.Location = new System.Drawing.Point(290, 49);
      this.m_CreationDateTextbox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.m_CreationDateTextbox.MaximumSize = new System.Drawing.Size(400, 4);
      this.m_CreationDateTextbox.MinimumSize = new System.Drawing.Size(280, 20);
      this.m_CreationDateTextbox.Name = "m_CreationDateTextbox";
      this.m_CreationDateTextbox.Size = new System.Drawing.Size(386, 20);
      this.m_CreationDateTextbox.TabIndex = 3;
      // 
      // m_lockedLabel
      // 
      this.m_lockedLabel.AutoSize = true;
      this.m_lockedLabel.Location = new System.Drawing.Point(3, 95);
      this.m_lockedLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_lockedLabel.Name = "m_lockedLabel";
      this.m_lockedLabel.Size = new System.Drawing.Size(87, 15);
      this.m_lockedLabel.TabIndex = 10;
      this.m_lockedLabel.Text = "Version locked";
      // 
      // m_lockedDateLabel
      // 
      this.m_lockedDateLabel.AutoSize = true;
      this.m_lockedDateLabel.Location = new System.Drawing.Point(3, 139);
      this.m_lockedDateLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_lockedDateLabel.Name = "m_lockedDateLabel";
      this.m_lockedDateLabel.Size = new System.Drawing.Size(74, 15);
      this.m_lockedDateLabel.TabIndex = 11;
      this.m_lockedDateLabel.Text = "Locked date";
      // 
      // LockedDateT
      // 
      this.LockedDateT.Enabled = false;
      this.LockedDateT.Location = new System.Drawing.Point(290, 137);
      this.LockedDateT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.LockedDateT.MaximumSize = new System.Drawing.Size(400, 4);
      this.LockedDateT.MinimumSize = new System.Drawing.Size(280, 20);
      this.LockedDateT.Name = "LockedDateT";
      this.LockedDateT.Size = new System.Drawing.Size(386, 20);
      this.LockedDateT.TabIndex = 12;
      // 
      // m_nameTextbox
      // 
      this.m_nameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_nameTextbox.Enabled = false;
      this.m_nameTextbox.Location = new System.Drawing.Point(290, 5);
      this.m_nameTextbox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.m_nameTextbox.MaximumSize = new System.Drawing.Size(400, 4);
      this.m_nameTextbox.MinimumSize = new System.Drawing.Size(280, 20);
      this.m_nameTextbox.Name = "m_nameTextbox";
      this.m_nameTextbox.Size = new System.Drawing.Size(386, 20);
      this.m_nameTextbox.TabIndex = 13;
      // 
      // m_creationDateLabel
      // 
      this.m_creationDateLabel.AutoSize = true;
      this.m_creationDateLabel.Location = new System.Drawing.Point(3, 51);
      this.m_creationDateLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_creationDateLabel.Name = "m_creationDateLabel";
      this.m_creationDateLabel.Size = new System.Drawing.Size(80, 15);
      this.m_creationDateLabel.TabIndex = 6;
      this.m_creationDateLabel.Text = "Creation date";
      // 
      // lockedCB
      // 
      this.lockedCB.AutoSize = true;
      this.lockedCB.Location = new System.Drawing.Point(290, 98);
      this.lockedCB.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
      this.lockedCB.Name = "lockedCB";
      this.lockedCB.Size = new System.Drawing.Size(15, 14);
      this.lockedCB.TabIndex = 14;
      this.lockedCB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.lockedCB.UseVisualStyleBackColor = true;
      // 
      // m_timeConfigTB
      // 
      this.m_timeConfigTB.Enabled = false;
      this.m_timeConfigTB.Location = new System.Drawing.Point(290, 179);
      this.m_timeConfigTB.Name = "m_timeConfigTB";
      this.m_timeConfigTB.Size = new System.Drawing.Size(386, 20);
      this.m_timeConfigTB.TabIndex = 18;
      // 
      // m_startPeriodTextbox
      // 
      this.m_startPeriodTextbox.Enabled = false;
      this.m_startPeriodTextbox.Location = new System.Drawing.Point(290, 223);
      this.m_startPeriodTextbox.Name = "m_startPeriodTextbox";
      this.m_startPeriodTextbox.Size = new System.Drawing.Size(386, 21);
      this.m_startPeriodTextbox.TabIndex = 19;
      // 
      // m_nbPeriodsTextbox
      // 
      this.m_nbPeriodsTextbox.Enabled = false;
      this.m_nbPeriodsTextbox.Location = new System.Drawing.Point(290, 267);
      this.m_nbPeriodsTextbox.Name = "m_nbPeriodsTextbox";
      this.m_nbPeriodsTextbox.Size = new System.Drawing.Size(384, 20);
      this.m_nbPeriodsTextbox.TabIndex = 21;
      // 
      // m_exchangeRatesVersionVTreeviewbox
      // 
      this.m_exchangeRatesVersionVTreeviewbox.BackColor = System.Drawing.Color.White;
      this.m_exchangeRatesVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black;
      this.m_exchangeRatesVersionVTreeviewbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_exchangeRatesVersionVTreeviewbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_exchangeRatesVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_exchangeRatesVersionVTreeviewbox.Location = new System.Drawing.Point(290, 311);
      this.m_exchangeRatesVersionVTreeviewbox.Name = "m_exchangeRatesVersionVTreeviewbox";
      this.m_exchangeRatesVersionVTreeviewbox.Size = new System.Drawing.Size(386, 23);
      this.m_exchangeRatesVersionVTreeviewbox.TabIndex = 25;
      this.m_exchangeRatesVersionVTreeviewbox.Text = " ";
      this.m_exchangeRatesVersionVTreeviewbox.UseThemeBackColor = false;
      this.m_exchangeRatesVersionVTreeviewbox.UseThemeDropDownArrowColor = true;
      this.m_exchangeRatesVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_factsVersionVTreeviewbox
      // 
      this.m_factsVersionVTreeviewbox.BackColor = System.Drawing.Color.White;
      this.m_factsVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black;
      this.m_factsVersionVTreeviewbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_factsVersionVTreeviewbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_factsVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_factsVersionVTreeviewbox.Location = new System.Drawing.Point(290, 355);
      this.m_factsVersionVTreeviewbox.Name = "m_factsVersionVTreeviewbox";
      this.m_factsVersionVTreeviewbox.Size = new System.Drawing.Size(384, 23);
      this.m_factsVersionVTreeviewbox.TabIndex = 26;
      this.m_factsVersionVTreeviewbox.Text = " ";
      this.m_factsVersionVTreeviewbox.UseThemeBackColor = false;
      this.m_factsVersionVTreeviewbox.UseThemeDropDownArrowColor = true;
      this.m_factsVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // MenuStrip1
      // 
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionsToolStripMenuItem});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(1049, 27);
      this.MenuStrip1.TabIndex = 0;
      this.MenuStrip1.Text = "MenuStrip1";
      // 
      // VersionsToolStripMenuItem
      // 
      this.VersionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_newVersionMenuBT,
            this.m_newFolderMenuBT,
            this.toolStripSeparator3,
            this.m_renameMenuBT,
            this.toolStripSeparator4,
            this.m_deleteVersionMenuBT});
      this.VersionsToolStripMenuItem.Name = "VersionsToolStripMenuItem";
      this.VersionsToolStripMenuItem.Size = new System.Drawing.Size(72, 23);
      this.VersionsToolStripMenuItem.Text = "Versions";
      // 
      // m_newVersionMenuBT
      // 
      this.m_newVersionMenuBT.Image = global::FBI.Properties.Resources.elements3_add;
      this.m_newVersionMenuBT.Name = "m_newVersionMenuBT";
      this.m_newVersionMenuBT.Size = new System.Drawing.Size(151, 24);
      this.m_newVersionMenuBT.Text = "Add version";
      // 
      // m_newFolderMenuBT
      // 
      this.m_newFolderMenuBT.Image = global::FBI.Properties.Resources.favicon_81_;
      this.m_newFolderMenuBT.Name = "m_newFolderMenuBT";
      this.m_newFolderMenuBT.Size = new System.Drawing.Size(151, 24);
      this.m_newFolderMenuBT.Text = "Add folder";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(148, 6);
      // 
      // m_renameMenuBT
      // 
      this.m_renameMenuBT.Name = "m_renameMenuBT";
      this.m_renameMenuBT.Size = new System.Drawing.Size(151, 24);
      this.m_renameMenuBT.Text = "Rename";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(148, 6);
      // 
      // m_deleteVersionMenuBT
      // 
      this.m_deleteVersionMenuBT.Image = global::FBI.Properties.Resources.elements3_delete;
      this.m_deleteVersionMenuBT.Name = "m_deleteVersionMenuBT";
      this.m_deleteVersionMenuBT.Size = new System.Drawing.Size(151, 24);
      this.m_deleteVersionMenuBT.Text = "Delete";
      // 
      // m_versionsTreeviewImageList
      // 
      this.m_versionsTreeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_versionsTreeviewImageList.ImageStream")));
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // VersionsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.TableLayoutPanel3);
      this.Name = "VersionsView";
      this.Size = new System.Drawing.Size(1049, 635);
      this.RCM_TV.ResumeLayout(false);
      this.TableLayoutPanel3.ResumeLayout(false);
      this.TableLayoutPanel3.PerformLayout();
      this.SplitContainer1.Panel1.ResumeLayout(false);
      this.SplitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.ImageList ButtonsImageList;
    internal System.Windows.Forms.ContextMenuStrip RCM_TV;
    internal System.Windows.Forms.ToolStripMenuItem m_new_VersionRCMButton;
    internal System.Windows.Forms.ToolStripMenuItem m_newFolderRCMButton;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem m_renameRCMButton;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteRCMButton;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel3;
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem VersionsToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem m_newVersionMenuBT;
    internal System.Windows.Forms.ToolStripMenuItem m_newFolderMenuBT;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteVersionMenuBT;
    internal System.Windows.Forms.SplitContainer SplitContainer1;
    internal System.Windows.Forms.Panel m_versionsTVPanel;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
    internal System.Windows.Forms.Label m_exchangeRatesVersionLabel;
    internal System.Windows.Forms.Label m_numberOfYearsLabel;
    internal System.Windows.Forms.Label m_startingPeriodLabel;
    internal System.Windows.Forms.Label m_periodConfigLabel;
    internal System.Windows.Forms.Label m_nameLabel;
    internal System.Windows.Forms.TextBox m_CreationDateTextbox;
    internal System.Windows.Forms.Label m_lockedLabel;
    internal System.Windows.Forms.Label m_lockedDateLabel;
    internal System.Windows.Forms.TextBox LockedDateT;
    internal System.Windows.Forms.TextBox m_nameTextbox;
    internal System.Windows.Forms.Label m_creationDateLabel;
    internal System.Windows.Forms.CheckBox lockedCB;
    internal System.Windows.Forms.TextBox m_timeConfigTB;
    internal System.Windows.Forms.ComboBox m_startPeriodTextbox;
    internal System.Windows.Forms.TextBox m_nbPeriodsTextbox;
    internal System.Windows.Forms.ToolStripMenuItem m_renameMenuBT;
    internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
    internal System.Windows.Forms.Label m_globalFactsVersionLabel;
    internal VIBlend.WinForms.Controls.vTreeViewBox m_exchangeRatesVersionVTreeviewbox;
    internal VIBlend.WinForms.Controls.vTreeViewBox m_factsVersionVTreeviewbox;

    internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    private System.Windows.Forms.ToolStripMenuItem m_copyVersionRCMButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
  }
}