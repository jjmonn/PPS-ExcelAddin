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
      this.m_versionsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
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
      this.m_CreationDateTextbox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_lockedLabel = new System.Windows.Forms.Label();
      this.m_lockedDateLabel = new System.Windows.Forms.Label();
      this.LockedDateT = new VIBlend.WinForms.Controls.vTextBox();
      this.m_nameTextbox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_creationDateLabel = new System.Windows.Forms.Label();
      this.m_lockCombobox = new System.Windows.Forms.CheckBox();
      this.m_timeConfigTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_startPeriodTextbox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_nbPeriodsTextbox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_factsVersionVTreeviewbox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_exchangeRatesVersionVTreeviewbox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_formulaPeriodLabel = new System.Windows.Forms.Label();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_nbFormulaPeriodCB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_formulaPeriodIndexCB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_nbFormulaPeriodLabel = new System.Windows.Forms.Label();
      this.m_formulaPeriodIndexLabel = new System.Windows.Forms.Label();
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
      this.m_versionsRightClickMenu.SuspendLayout();
      this.TableLayoutPanel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
      this.SplitContainer1.Panel1.SuspendLayout();
      this.SplitContainer1.Panel2.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
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
      // m_versionsRightClickMenu
      // 
      this.m_versionsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_new_VersionRCMButton,
            this.m_copyVersionRCMButton,
            this.m_newFolderRCMButton,
            this.ToolStripSeparator2,
            this.m_renameRCMButton,
            this.ToolStripSeparator1,
            this.m_deleteRCMButton});
      this.m_versionsRightClickMenu.Name = "RCM_TV";
      this.m_versionsRightClickMenu.Size = new System.Drawing.Size(144, 126);
      // 
      // m_new_VersionRCMButton
      // 
      this.m_new_VersionRCMButton.Image = global::FBI.Properties.Resources.elements3_add;
      this.m_new_VersionRCMButton.Name = "m_new_VersionRCMButton";
      this.m_new_VersionRCMButton.Size = new System.Drawing.Size(143, 22);
      this.m_new_VersionRCMButton.Text = "New_version";
      // 
      // m_copyVersionRCMButton
      // 
      this.m_copyVersionRCMButton.Name = "m_copyVersionRCMButton";
      this.m_copyVersionRCMButton.Size = new System.Drawing.Size(143, 22);
      this.m_copyVersionRCMButton.Text = "Copy version";
      // 
      // m_newFolderRCMButton
      // 
      this.m_newFolderRCMButton.Image = global::FBI.Properties.Resources.folder2;
      this.m_newFolderRCMButton.Name = "m_newFolderRCMButton";
      this.m_newFolderRCMButton.Size = new System.Drawing.Size(143, 22);
      this.m_newFolderRCMButton.Text = "New_folder";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(140, 6);
      // 
      // m_renameRCMButton
      // 
      this.m_renameRCMButton.Name = "m_renameRCMButton";
      this.m_renameRCMButton.Size = new System.Drawing.Size(143, 22);
      this.m_renameRCMButton.Text = "Rename";
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(140, 6);
      // 
      // m_deleteRCMButton
      // 
      this.m_deleteRCMButton.Image = global::FBI.Properties.Resources.elements3_delete;
      this.m_deleteRCMButton.Name = "m_deleteRCMButton";
      this.m_deleteRCMButton.Size = new System.Drawing.Size(143, 22);
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
      this.SplitContainer1.SplitterDistance = 292;
      this.SplitContainer1.TabIndex = 2;
      // 
      // m_versionsTVPanel
      // 
      this.m_versionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_versionsTVPanel.Location = new System.Drawing.Point(0, 0);
      this.m_versionsTVPanel.Name = "m_versionsTVPanel";
      this.m_versionsTVPanel.Size = new System.Drawing.Size(292, 599);
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
      this.TableLayoutPanel2.Controls.Add(this.m_lockCombobox, 1, 2);
      this.TableLayoutPanel2.Controls.Add(this.m_timeConfigTB, 1, 4);
      this.TableLayoutPanel2.Controls.Add(this.m_startPeriodTextbox, 1, 5);
      this.TableLayoutPanel2.Controls.Add(this.m_nbPeriodsTextbox, 1, 6);
      this.TableLayoutPanel2.Controls.Add(this.m_factsVersionVTreeviewbox, 1, 8);
      this.TableLayoutPanel2.Controls.Add(this.m_exchangeRatesVersionVTreeviewbox, 1, 7);
      this.TableLayoutPanel2.Controls.Add(this.m_formulaPeriodLabel, 0, 9);
      this.TableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 9);
      this.TableLayoutPanel2.Location = new System.Drawing.Point(28, 18);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 10;
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.TableLayoutPanel2.Size = new System.Drawing.Size(679, 464);
      this.TableLayoutPanel2.TabIndex = 0;
      // 
      // m_globalFactsVersionLabel
      // 
      this.m_globalFactsVersionLabel.AutoSize = true;
      this.m_globalFactsVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_globalFactsVersionLabel.Location = new System.Drawing.Point(3, 383);
      this.m_globalFactsVersionLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_globalFactsVersionLabel.Name = "m_globalFactsVersionLabel";
      this.m_globalFactsVersionLabel.Size = new System.Drawing.Size(281, 30);
      this.m_globalFactsVersionLabel.TabIndex = 24;
      this.m_globalFactsVersionLabel.Text = "Global facts version";
      // 
      // m_exchangeRatesVersionLabel
      // 
      this.m_exchangeRatesVersionLabel.AutoSize = true;
      this.m_exchangeRatesVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_exchangeRatesVersionLabel.Location = new System.Drawing.Point(3, 336);
      this.m_exchangeRatesVersionLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_exchangeRatesVersionLabel.Name = "m_exchangeRatesVersionLabel";
      this.m_exchangeRatesVersionLabel.Size = new System.Drawing.Size(281, 40);
      this.m_exchangeRatesVersionLabel.TabIndex = 22;
      this.m_exchangeRatesVersionLabel.Text = "Exchange rates version";
      // 
      // m_numberOfYearsLabel
      // 
      this.m_numberOfYearsLabel.AutoSize = true;
      this.m_numberOfYearsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_numberOfYearsLabel.Location = new System.Drawing.Point(3, 289);
      this.m_numberOfYearsLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_numberOfYearsLabel.Name = "m_numberOfYearsLabel";
      this.m_numberOfYearsLabel.Size = new System.Drawing.Size(281, 40);
      this.m_numberOfYearsLabel.TabIndex = 20;
      this.m_numberOfYearsLabel.Text = "Number of periods";
      // 
      // m_startingPeriodLabel
      // 
      this.m_startingPeriodLabel.AutoSize = true;
      this.m_startingPeriodLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_startingPeriodLabel.Location = new System.Drawing.Point(3, 242);
      this.m_startingPeriodLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_startingPeriodLabel.Name = "m_startingPeriodLabel";
      this.m_startingPeriodLabel.Size = new System.Drawing.Size(281, 40);
      this.m_startingPeriodLabel.TabIndex = 17;
      this.m_startingPeriodLabel.Text = "Starting period";
      // 
      // m_periodConfigLabel
      // 
      this.m_periodConfigLabel.AutoSize = true;
      this.m_periodConfigLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_periodConfigLabel.Location = new System.Drawing.Point(3, 195);
      this.m_periodConfigLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_periodConfigLabel.Name = "m_periodConfigLabel";
      this.m_periodConfigLabel.Size = new System.Drawing.Size(281, 40);
      this.m_periodConfigLabel.TabIndex = 15;
      this.m_periodConfigLabel.Text = "Period config";
      // 
      // m_nameLabel
      // 
      this.m_nameLabel.AutoSize = true;
      this.m_nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_nameLabel.Location = new System.Drawing.Point(3, 7);
      this.m_nameLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_nameLabel.Name = "m_nameLabel";
      this.m_nameLabel.Size = new System.Drawing.Size(281, 40);
      this.m_nameLabel.TabIndex = 7;
      this.m_nameLabel.Text = "Version name";
      // 
      // m_CreationDateTextbox
      // 
      this.m_CreationDateTextbox.BackColor = System.Drawing.Color.White;
      this.m_CreationDateTextbox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_CreationDateTextbox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_CreationDateTextbox.DefaultText = "Empty...";
      this.m_CreationDateTextbox.Enabled = false;
      this.m_CreationDateTextbox.Location = new System.Drawing.Point(290, 52);
      this.m_CreationDateTextbox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.m_CreationDateTextbox.MaximumSize = new System.Drawing.Size(400, 4);
      this.m_CreationDateTextbox.MaxLength = 32767;
      this.m_CreationDateTextbox.MinimumSize = new System.Drawing.Size(280, 20);
      this.m_CreationDateTextbox.Name = "m_CreationDateTextbox";
      this.m_CreationDateTextbox.PasswordChar = '\0';
      this.m_CreationDateTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_CreationDateTextbox.SelectionLength = 0;
      this.m_CreationDateTextbox.SelectionStart = 0;
      this.m_CreationDateTextbox.Size = new System.Drawing.Size(386, 20);
      this.m_CreationDateTextbox.TabIndex = 3;
      this.m_CreationDateTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_CreationDateTextbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_lockedLabel
      // 
      this.m_lockedLabel.AutoSize = true;
      this.m_lockedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_lockedLabel.Location = new System.Drawing.Point(3, 101);
      this.m_lockedLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_lockedLabel.Name = "m_lockedLabel";
      this.m_lockedLabel.Size = new System.Drawing.Size(281, 40);
      this.m_lockedLabel.TabIndex = 10;
      this.m_lockedLabel.Text = "Version locked";
      // 
      // m_lockedDateLabel
      // 
      this.m_lockedDateLabel.AutoSize = true;
      this.m_lockedDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_lockedDateLabel.Location = new System.Drawing.Point(3, 148);
      this.m_lockedDateLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_lockedDateLabel.Name = "m_lockedDateLabel";
      this.m_lockedDateLabel.Size = new System.Drawing.Size(281, 40);
      this.m_lockedDateLabel.TabIndex = 11;
      this.m_lockedDateLabel.Text = "Locked date";
      // 
      // LockedDateT
      // 
      this.LockedDateT.BackColor = System.Drawing.Color.White;
      this.LockedDateT.BoundsOffset = new System.Drawing.Size(1, 1);
      this.LockedDateT.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.LockedDateT.DefaultText = "Empty...";
      this.LockedDateT.Enabled = false;
      this.LockedDateT.Location = new System.Drawing.Point(290, 146);
      this.LockedDateT.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.LockedDateT.MaximumSize = new System.Drawing.Size(400, 4);
      this.LockedDateT.MaxLength = 32767;
      this.LockedDateT.MinimumSize = new System.Drawing.Size(280, 20);
      this.LockedDateT.Name = "LockedDateT";
      this.LockedDateT.PasswordChar = '\0';
      this.LockedDateT.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.LockedDateT.SelectionLength = 0;
      this.LockedDateT.SelectionStart = 0;
      this.LockedDateT.Size = new System.Drawing.Size(386, 20);
      this.LockedDateT.TabIndex = 12;
      this.LockedDateT.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.LockedDateT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_nameTextbox
      // 
      this.m_nameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_nameTextbox.BackColor = System.Drawing.Color.White;
      this.m_nameTextbox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_nameTextbox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_nameTextbox.DefaultText = "Empty...";
      this.m_nameTextbox.Enabled = false;
      this.m_nameTextbox.Location = new System.Drawing.Point(290, 5);
      this.m_nameTextbox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.m_nameTextbox.MaximumSize = new System.Drawing.Size(400, 4);
      this.m_nameTextbox.MaxLength = 32767;
      this.m_nameTextbox.MinimumSize = new System.Drawing.Size(280, 20);
      this.m_nameTextbox.Name = "m_nameTextbox";
      this.m_nameTextbox.PasswordChar = '\0';
      this.m_nameTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_nameTextbox.SelectionLength = 0;
      this.m_nameTextbox.SelectionStart = 0;
      this.m_nameTextbox.Size = new System.Drawing.Size(386, 20);
      this.m_nameTextbox.TabIndex = 13;
      this.m_nameTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_nameTextbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_creationDateLabel
      // 
      this.m_creationDateLabel.AutoSize = true;
      this.m_creationDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_creationDateLabel.Location = new System.Drawing.Point(3, 54);
      this.m_creationDateLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_creationDateLabel.Name = "m_creationDateLabel";
      this.m_creationDateLabel.Size = new System.Drawing.Size(281, 40);
      this.m_creationDateLabel.TabIndex = 6;
      this.m_creationDateLabel.Text = "Creation date";
      // 
      // m_lockCombobox
      // 
      this.m_lockCombobox.AutoSize = true;
      this.m_lockCombobox.Location = new System.Drawing.Point(290, 104);
      this.m_lockCombobox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
      this.m_lockCombobox.Name = "m_lockCombobox";
      this.m_lockCombobox.Size = new System.Drawing.Size(15, 14);
      this.m_lockCombobox.TabIndex = 14;
      this.m_lockCombobox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.m_lockCombobox.UseVisualStyleBackColor = true;
      // 
      // m_timeConfigTB
      // 
      this.m_timeConfigTB.BackColor = System.Drawing.Color.White;
      this.m_timeConfigTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_timeConfigTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_timeConfigTB.DefaultText = "Empty...";
      this.m_timeConfigTB.Enabled = false;
      this.m_timeConfigTB.Location = new System.Drawing.Point(290, 191);
      this.m_timeConfigTB.MaxLength = 32767;
      this.m_timeConfigTB.Name = "m_timeConfigTB";
      this.m_timeConfigTB.PasswordChar = '\0';
      this.m_timeConfigTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_timeConfigTB.SelectionLength = 0;
      this.m_timeConfigTB.SelectionStart = 0;
      this.m_timeConfigTB.Size = new System.Drawing.Size(386, 20);
      this.m_timeConfigTB.TabIndex = 18;
      this.m_timeConfigTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_timeConfigTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_startPeriodTextbox
      // 
      this.m_startPeriodTextbox.BackColor = System.Drawing.Color.White;
      this.m_startPeriodTextbox.DisplayMember = "";
      this.m_startPeriodTextbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_startPeriodTextbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_startPeriodTextbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_startPeriodTextbox.DropDownWidth = 386;
      this.m_startPeriodTextbox.Enabled = false;
      this.m_startPeriodTextbox.Location = new System.Drawing.Point(290, 238);
      this.m_startPeriodTextbox.Name = "m_startPeriodTextbox";
      this.m_startPeriodTextbox.RoundedCornersMaskListItem = ((byte)(15));
      this.m_startPeriodTextbox.Size = new System.Drawing.Size(386, 21);
      this.m_startPeriodTextbox.TabIndex = 19;
      this.m_startPeriodTextbox.UseThemeBackColor = false;
      this.m_startPeriodTextbox.UseThemeDropDownArrowColor = true;
      this.m_startPeriodTextbox.ValueMember = "";
      this.m_startPeriodTextbox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_startPeriodTextbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_nbPeriodsTextbox
      // 
      this.m_nbPeriodsTextbox.BackColor = System.Drawing.Color.White;
      this.m_nbPeriodsTextbox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_nbPeriodsTextbox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_nbPeriodsTextbox.DefaultText = "Empty...";
      this.m_nbPeriodsTextbox.Enabled = false;
      this.m_nbPeriodsTextbox.Location = new System.Drawing.Point(290, 285);
      this.m_nbPeriodsTextbox.MaxLength = 32767;
      this.m_nbPeriodsTextbox.Name = "m_nbPeriodsTextbox";
      this.m_nbPeriodsTextbox.PasswordChar = '\0';
      this.m_nbPeriodsTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_nbPeriodsTextbox.SelectionLength = 0;
      this.m_nbPeriodsTextbox.SelectionStart = 0;
      this.m_nbPeriodsTextbox.Size = new System.Drawing.Size(384, 20);
      this.m_nbPeriodsTextbox.TabIndex = 21;
      this.m_nbPeriodsTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_nbPeriodsTextbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_factsVersionVTreeviewbox
      // 
      this.m_factsVersionVTreeviewbox.BackColor = System.Drawing.Color.White;
      this.m_factsVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black;
      this.m_factsVersionVTreeviewbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_factsVersionVTreeviewbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_factsVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_factsVersionVTreeviewbox.Location = new System.Drawing.Point(290, 379);
      this.m_factsVersionVTreeviewbox.Name = "m_factsVersionVTreeviewbox";
      this.m_factsVersionVTreeviewbox.Size = new System.Drawing.Size(384, 23);
      this.m_factsVersionVTreeviewbox.TabIndex = 26;
      this.m_factsVersionVTreeviewbox.Text = " ";
      this.m_factsVersionVTreeviewbox.UseThemeBackColor = false;
      this.m_factsVersionVTreeviewbox.UseThemeDropDownArrowColor = true;
      this.m_factsVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_exchangeRatesVersionVTreeviewbox
      // 
      this.m_exchangeRatesVersionVTreeviewbox.BackColor = System.Drawing.Color.White;
      this.m_exchangeRatesVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black;
      this.m_exchangeRatesVersionVTreeviewbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_exchangeRatesVersionVTreeviewbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_exchangeRatesVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_exchangeRatesVersionVTreeviewbox.Location = new System.Drawing.Point(290, 332);
      this.m_exchangeRatesVersionVTreeviewbox.Name = "m_exchangeRatesVersionVTreeviewbox";
      this.m_exchangeRatesVersionVTreeviewbox.Size = new System.Drawing.Size(386, 23);
      this.m_exchangeRatesVersionVTreeviewbox.TabIndex = 25;
      this.m_exchangeRatesVersionVTreeviewbox.Text = " ";
      this.m_exchangeRatesVersionVTreeviewbox.UseThemeBackColor = false;
      this.m_exchangeRatesVersionVTreeviewbox.UseThemeDropDownArrowColor = true;
      this.m_exchangeRatesVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_formulaPeriodLabel
      // 
      this.m_formulaPeriodLabel.AutoSize = true;
      this.m_formulaPeriodLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_formulaPeriodLabel.Location = new System.Drawing.Point(3, 413);
      this.m_formulaPeriodLabel.Name = "m_formulaPeriodLabel";
      this.m_formulaPeriodLabel.Size = new System.Drawing.Size(281, 51);
      this.m_formulaPeriodLabel.TabIndex = 28;
      this.m_formulaPeriodLabel.Text = "label3";
      this.m_formulaPeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.m_nbFormulaPeriodCB, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.m_formulaPeriodIndexCB, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.m_nbFormulaPeriodLabel, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.m_formulaPeriodIndexLabel, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(290, 416);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(386, 45);
      this.tableLayoutPanel1.TabIndex = 29;
      // 
      // m_nbFormulaPeriodCB
      // 
      this.m_nbFormulaPeriodCB.BackColor = System.Drawing.Color.White;
      this.m_nbFormulaPeriodCB.DisplayMember = "";
      this.m_nbFormulaPeriodCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_nbFormulaPeriodCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_nbFormulaPeriodCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_nbFormulaPeriodCB.DropDownWidth = 187;
      this.m_nbFormulaPeriodCB.Location = new System.Drawing.Point(196, 20);
      this.m_nbFormulaPeriodCB.Name = "m_nbFormulaPeriodCB";
      this.m_nbFormulaPeriodCB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_nbFormulaPeriodCB.Size = new System.Drawing.Size(187, 22);
      this.m_nbFormulaPeriodCB.TabIndex = 1;
      this.m_nbFormulaPeriodCB.UseThemeBackColor = false;
      this.m_nbFormulaPeriodCB.UseThemeDropDownArrowColor = true;
      this.m_nbFormulaPeriodCB.ValueMember = "";
      this.m_nbFormulaPeriodCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_nbFormulaPeriodCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_formulaPeriodIndexCB
      // 
      this.m_formulaPeriodIndexCB.BackColor = System.Drawing.Color.White;
      this.m_formulaPeriodIndexCB.DisplayMember = "";
      this.m_formulaPeriodIndexCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_formulaPeriodIndexCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_formulaPeriodIndexCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_formulaPeriodIndexCB.DropDownWidth = 187;
      this.m_formulaPeriodIndexCB.Location = new System.Drawing.Point(3, 20);
      this.m_formulaPeriodIndexCB.Name = "m_formulaPeriodIndexCB";
      this.m_formulaPeriodIndexCB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_formulaPeriodIndexCB.Size = new System.Drawing.Size(187, 22);
      this.m_formulaPeriodIndexCB.TabIndex = 0;
      this.m_formulaPeriodIndexCB.UseThemeBackColor = false;
      this.m_formulaPeriodIndexCB.UseThemeDropDownArrowColor = true;
      this.m_formulaPeriodIndexCB.ValueMember = "";
      this.m_formulaPeriodIndexCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_formulaPeriodIndexCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_nbFormulaPeriodLabel
      // 
      this.m_nbFormulaPeriodLabel.AutoSize = true;
      this.m_nbFormulaPeriodLabel.Location = new System.Drawing.Point(196, 0);
      this.m_nbFormulaPeriodLabel.Name = "m_nbFormulaPeriodLabel";
      this.m_nbFormulaPeriodLabel.Size = new System.Drawing.Size(35, 13);
      this.m_nbFormulaPeriodLabel.TabIndex = 1;
      this.m_nbFormulaPeriodLabel.Text = "label2";
      // 
      // m_formulaPeriodIndexLabel
      // 
      this.m_formulaPeriodIndexLabel.AutoSize = true;
      this.m_formulaPeriodIndexLabel.Location = new System.Drawing.Point(3, 0);
      this.m_formulaPeriodIndexLabel.Name = "m_formulaPeriodIndexLabel";
      this.m_formulaPeriodIndexLabel.Size = new System.Drawing.Size(35, 13);
      this.m_formulaPeriodIndexLabel.TabIndex = 0;
      this.m_formulaPeriodIndexLabel.Text = "label1";
      // 
      // MenuStrip1
      // 
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionsToolStripMenuItem});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(1049, 24);
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
      this.VersionsToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
      this.VersionsToolStripMenuItem.Text = "Versions";
      // 
      // m_newVersionMenuBT
      // 
      this.m_newVersionMenuBT.Image = global::FBI.Properties.Resources.elements3_add;
      this.m_newVersionMenuBT.Name = "m_newVersionMenuBT";
      this.m_newVersionMenuBT.Size = new System.Drawing.Size(137, 22);
      this.m_newVersionMenuBT.Text = "Add version";
      // 
      // m_newFolderMenuBT
      // 
      this.m_newFolderMenuBT.Image = global::FBI.Properties.Resources.favicon_81_;
      this.m_newFolderMenuBT.Name = "m_newFolderMenuBT";
      this.m_newFolderMenuBT.Size = new System.Drawing.Size(137, 22);
      this.m_newFolderMenuBT.Text = "Add folder";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
      // 
      // m_renameMenuBT
      // 
      this.m_renameMenuBT.Name = "m_renameMenuBT";
      this.m_renameMenuBT.Size = new System.Drawing.Size(137, 22);
      this.m_renameMenuBT.Text = "Rename";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(134, 6);
      // 
      // m_deleteVersionMenuBT
      // 
      this.m_deleteVersionMenuBT.Image = global::FBI.Properties.Resources.elements3_delete;
      this.m_deleteVersionMenuBT.Name = "m_deleteVersionMenuBT";
      this.m_deleteVersionMenuBT.Size = new System.Drawing.Size(137, 22);
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
      this.m_versionsRightClickMenu.ResumeLayout(false);
      this.TableLayoutPanel3.ResumeLayout(false);
      this.TableLayoutPanel3.PerformLayout();
      this.SplitContainer1.Panel1.ResumeLayout(false);
      this.SplitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.ImageList ButtonsImageList;
    public System.Windows.Forms.ContextMenuStrip m_versionsRightClickMenu;
    public System.Windows.Forms.ToolStripMenuItem m_new_VersionRCMButton;
    public System.Windows.Forms.ToolStripMenuItem m_newFolderRCMButton;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    public System.Windows.Forms.ToolStripMenuItem m_renameRCMButton;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    public System.Windows.Forms.ToolStripMenuItem m_deleteRCMButton;
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel3;
    public System.Windows.Forms.MenuStrip MenuStrip1;
    public System.Windows.Forms.ToolStripMenuItem VersionsToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem m_newVersionMenuBT;
    public System.Windows.Forms.ToolStripMenuItem m_newFolderMenuBT;
    public System.Windows.Forms.ToolStripMenuItem m_deleteVersionMenuBT;
    public System.Windows.Forms.SplitContainer SplitContainer1;
    public System.Windows.Forms.Panel m_versionsTVPanel;
    public System.Windows.Forms.ToolStripMenuItem m_renameMenuBT;
    public System.ComponentModel.BackgroundWorker BackgroundWorker1;

    public System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    private System.Windows.Forms.ToolStripMenuItem m_copyVersionRCMButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
    public System.Windows.Forms.Label m_globalFactsVersionLabel;
    public System.Windows.Forms.Label m_exchangeRatesVersionLabel;
    public System.Windows.Forms.Label m_numberOfYearsLabel;
    public System.Windows.Forms.Label m_startingPeriodLabel;
    public System.Windows.Forms.Label m_periodConfigLabel;
    public System.Windows.Forms.Label m_nameLabel;
    public VIBlend.WinForms.Controls.vTextBox m_CreationDateTextbox;
    public System.Windows.Forms.Label m_lockedLabel;
    public System.Windows.Forms.Label m_lockedDateLabel;
    public VIBlend.WinForms.Controls.vTextBox LockedDateT;
    public VIBlend.WinForms.Controls.vTextBox m_nameTextbox;
    public System.Windows.Forms.Label m_creationDateLabel;
    public System.Windows.Forms.CheckBox m_lockCombobox;
    public VIBlend.WinForms.Controls.vTextBox m_timeConfigTB;
    public VIBlend.WinForms.Controls.vComboBox m_startPeriodTextbox;
    public VIBlend.WinForms.Controls.vTextBox m_nbPeriodsTextbox;
    public VIBlend.WinForms.Controls.vTreeViewBox m_exchangeRatesVersionVTreeviewbox;
    public VIBlend.WinForms.Controls.vTreeViewBox m_factsVersionVTreeviewbox;
    private System.Windows.Forms.Label m_nbFormulaPeriodLabel;
    private System.Windows.Forms.Label m_formulaPeriodIndexLabel;
    private VIBlend.WinForms.Controls.vComboBox m_nbFormulaPeriodCB;
    private VIBlend.WinForms.Controls.vComboBox m_formulaPeriodIndexCB;
    private System.Windows.Forms.Label m_formulaPeriodLabel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
  }
}