using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class AccountsView : System.Windows.Forms.UserControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsView));
      this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
      this.AccountsTVPanel = new System.Windows.Forms.Panel();
      this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
      this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.m_accountDescriptionGroupbox = new System.Windows.Forms.GroupBox();
      this.m_descriptionTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.SaveDescriptionBT = new VIBlend.WinForms.Controls.vButton();
      this.EditButtonsImagelist = new System.Windows.Forms.ImageList(this.components);
      this.m_accountFormulaGroupbox = new System.Windows.Forms.GroupBox();
      this.m_cancelFormulaEditionButton = new VIBlend.WinForms.Controls.vButton();
      this.m_formulaEditionButton = new VIBlend.WinForms.Controls.vButton();
      this.m_formulaTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_validateFormulaButton = new VIBlend.WinForms.Controls.vButton();
      this.m_accountInformationGroupbox = new System.Windows.Forms.GroupBox();
      this.ConsolidationOptionComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.CurrencyConversionComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_accountNameLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountFormulaTypeLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountTypeLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountConsolidationOptionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountCurrenciesConversionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.FormulaTypeComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.TypeComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.Name_TB = new VIBlend.WinForms.Controls.vTextBox();
      this.GlobalFactsPanel = new System.Windows.Forms.Panel();
      this.m_globalFactsLabel = new VIBlend.WinForms.Controls.vLabel();
      this.accountsIL = new System.Windows.Forms.ImageList(this.components);
      this.TVRCM = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.AddSubAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.DeleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.m_allocationKeyButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.DropHierarchyToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.MainMenu = new System.Windows.Forms.MenuStrip();
      this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.CreateANewAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.CreateANewCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteAccountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.DropHierarchyToExcelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.DropAllAccountsHierarchyToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DropSelectedAccountHierarchyToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_globalFactsImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_ProcessLabel = new VIBlend.WinForms.Controls.vLabel();
      this.ProcessComboBox = new VIBlend.WinForms.Controls.vComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
      this.SplitContainer1.Panel1.SuspendLayout();
      this.SplitContainer1.Panel2.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
      this.SplitContainer2.Panel1.SuspendLayout();
      this.SplitContainer2.Panel2.SuspendLayout();
      this.SplitContainer2.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.m_accountDescriptionGroupbox.SuspendLayout();
      this.m_accountFormulaGroupbox.SuspendLayout();
      this.m_accountInformationGroupbox.SuspendLayout();
      this.TVRCM.SuspendLayout();
      this.MainMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // SplitContainer1
      // 
      this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
      this.SplitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SplitContainer1.Name = "SplitContainer1";
      // 
      // SplitContainer1.Panel1
      // 
      this.SplitContainer1.Panel1.Controls.Add(this.AccountsTVPanel);
      // 
      // SplitContainer1.Panel2
      // 
      this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
      this.SplitContainer1.Size = new System.Drawing.Size(1472, 1065);
      this.SplitContainer1.SplitterDistance = 427;
      this.SplitContainer1.SplitterWidth = 6;
      this.SplitContainer1.TabIndex = 23;
      // 
      // AccountsTVPanel
      // 
      this.AccountsTVPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AccountsTVPanel.Location = new System.Drawing.Point(2, 54);
      this.AccountsTVPanel.Margin = new System.Windows.Forms.Padding(2);
      this.AccountsTVPanel.Name = "AccountsTVPanel";
      this.AccountsTVPanel.Size = new System.Drawing.Size(423, 988);
      this.AccountsTVPanel.TabIndex = 3;
      // 
      // SplitContainer2
      // 
      this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
      this.SplitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.SplitContainer2.Name = "SplitContainer2";
      // 
      // SplitContainer2.Panel1
      // 
      this.SplitContainer2.Panel1.Controls.Add(this.TableLayoutPanel2);
      // 
      // SplitContainer2.Panel2
      // 
      this.SplitContainer2.Panel2.Controls.Add(this.GlobalFactsPanel);
      this.SplitContainer2.Panel2.Controls.Add(this.m_globalFactsLabel);
      this.SplitContainer2.Size = new System.Drawing.Size(1039, 1065);
      this.SplitContainer2.SplitterDistance = 842;
      this.SplitContainer2.TabIndex = 2;
      // 
      // TableLayoutPanel2
      // 
      this.TableLayoutPanel2.ColumnCount = 1;
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel2.Controls.Add(this.m_accountDescriptionGroupbox, 0, 3);
      this.TableLayoutPanel2.Controls.Add(this.m_accountFormulaGroupbox, 0, 2);
      this.TableLayoutPanel2.Controls.Add(this.m_accountInformationGroupbox, 0, 1);
      this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 4;
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 437F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.77032F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.22968F));
      this.TableLayoutPanel2.Size = new System.Drawing.Size(842, 1065);
      this.TableLayoutPanel2.TabIndex = 1;
      // 
      // m_accountDescriptionGroupbox
      // 
      this.m_accountDescriptionGroupbox.Controls.Add(this.m_descriptionTextBox);
      this.m_accountDescriptionGroupbox.Controls.Add(this.SaveDescriptionBT);
      this.m_accountDescriptionGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountDescriptionGroupbox.Location = new System.Drawing.Point(4, 804);
      this.m_accountDescriptionGroupbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountDescriptionGroupbox.Name = "m_accountDescriptionGroupbox";
      this.m_accountDescriptionGroupbox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountDescriptionGroupbox.Size = new System.Drawing.Size(834, 256);
      this.m_accountDescriptionGroupbox.TabIndex = 20;
      this.m_accountDescriptionGroupbox.TabStop = false;
      this.m_accountDescriptionGroupbox.Text = "Account description";
      // 
      // m_descriptionTextBox
      // 
      this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_descriptionTextBox.BackColor = System.Drawing.Color.White;
      this.m_descriptionTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_descriptionTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_descriptionTextBox.DefaultText = "Empty...";
      this.m_descriptionTextBox.Location = new System.Drawing.Point(9, 32);
      this.m_descriptionTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_descriptionTextBox.MaxLength = 32767;
      this.m_descriptionTextBox.Multiline = true;
      this.m_descriptionTextBox.Name = "m_descriptionTextBox";
      this.m_descriptionTextBox.PasswordChar = '\0';
      this.m_descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_descriptionTextBox.SelectionLength = 0;
      this.m_descriptionTextBox.SelectionStart = 0;
      this.m_descriptionTextBox.Size = new System.Drawing.Size(796, 154);
      this.m_descriptionTextBox.TabIndex = 8;
      this.m_descriptionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_descriptionTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // SaveDescriptionBT
      // 
      this.SaveDescriptionBT.AllowAnimations = true;
      this.SaveDescriptionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.SaveDescriptionBT.BackColor = System.Drawing.Color.Transparent;
      this.SaveDescriptionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.SaveDescriptionBT.ImageKey = "1420498403_340208.ico";
      this.SaveDescriptionBT.ImageList = this.EditButtonsImagelist;
      this.SaveDescriptionBT.Location = new System.Drawing.Point(524, 194);
      this.SaveDescriptionBT.Name = "SaveDescriptionBT";
      this.SaveDescriptionBT.RoundedCornersMask = ((byte)(15));
      this.SaveDescriptionBT.Size = new System.Drawing.Size(280, 43);
      this.SaveDescriptionBT.TabIndex = 7;
      this.SaveDescriptionBT.Text = "[accounts_edition.save_description]";
      this.SaveDescriptionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.SaveDescriptionBT.UseVisualStyleBackColor = true;
      this.SaveDescriptionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // EditButtonsImagelist
      // 
      this.EditButtonsImagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("EditButtonsImagelist.ImageStream")));
      this.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent;
      this.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico");
      this.EditButtonsImagelist.Images.SetKeyName(1, "formula.ico");
      this.EditButtonsImagelist.Images.SetKeyName(2, "imageres_89.ico");
      // 
      // m_accountFormulaGroupbox
      // 
      this.m_accountFormulaGroupbox.Controls.Add(this.m_cancelFormulaEditionButton);
      this.m_accountFormulaGroupbox.Controls.Add(this.m_formulaEditionButton);
      this.m_accountFormulaGroupbox.Controls.Add(this.m_formulaTextBox);
      this.m_accountFormulaGroupbox.Controls.Add(this.m_validateFormulaButton);
      this.m_accountFormulaGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountFormulaGroupbox.Location = new System.Drawing.Point(4, 482);
      this.m_accountFormulaGroupbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountFormulaGroupbox.Name = "m_accountFormulaGroupbox";
      this.m_accountFormulaGroupbox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountFormulaGroupbox.Size = new System.Drawing.Size(834, 312);
      this.m_accountFormulaGroupbox.TabIndex = 19;
      this.m_accountFormulaGroupbox.TabStop = false;
      this.m_accountFormulaGroupbox.Text = "Account formula";
      // 
      // m_cancelFormulaEditionButton
      // 
      this.m_cancelFormulaEditionButton.AllowAnimations = true;
      this.m_cancelFormulaEditionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelFormulaEditionButton.BackColor = System.Drawing.Color.Transparent;
      this.m_cancelFormulaEditionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_cancelFormulaEditionButton.ImageKey = "imageres_89.ico";
      this.m_cancelFormulaEditionButton.ImageList = this.EditButtonsImagelist;
      this.m_cancelFormulaEditionButton.Location = new System.Drawing.Point(683, 249);
      this.m_cancelFormulaEditionButton.Name = "m_cancelFormulaEditionButton";
      this.m_cancelFormulaEditionButton.RoundedCornersMask = ((byte)(15));
      this.m_cancelFormulaEditionButton.Size = new System.Drawing.Size(122, 43);
      this.m_cancelFormulaEditionButton.TabIndex = 8;
      this.m_cancelFormulaEditionButton.Text = "Cancel";
      this.m_cancelFormulaEditionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_cancelFormulaEditionButton.UseVisualStyleBackColor = true;
      this.m_cancelFormulaEditionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_cancelFormulaEditionButton.Visible = false;
      // 
      // m_formulaEditionButton
      // 
      this.m_formulaEditionButton.AllowAnimations = true;
      this.m_formulaEditionButton.BackColor = System.Drawing.Color.Transparent;
      this.m_formulaEditionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_formulaEditionButton.ImageKey = "formula.ico";
      this.m_formulaEditionButton.ImageList = this.EditButtonsImagelist;
      this.m_formulaEditionButton.Location = new System.Drawing.Point(9, 34);
      this.m_formulaEditionButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_formulaEditionButton.Name = "m_formulaEditionButton";
      this.m_formulaEditionButton.RoundedCornersMask = ((byte)(15));
      this.m_formulaEditionButton.Size = new System.Drawing.Size(184, 34);
      this.m_formulaEditionButton.TabIndex = 8;
      this.m_formulaEditionButton.Text = "Edit formula";
      this.m_formulaEditionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_formulaEditionButton.UseVisualStyleBackColor = false;
      this.m_formulaEditionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_formulaTextBox
      // 
      this.m_formulaTextBox.AllowDrop = true;
      this.m_formulaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_formulaTextBox.BackColor = System.Drawing.Color.White;
      this.m_formulaTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_formulaTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_formulaTextBox.DefaultText = "Empty...";
      this.m_formulaTextBox.Enabled = false;
      this.m_formulaTextBox.Location = new System.Drawing.Point(9, 80);
      this.m_formulaTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_formulaTextBox.MaxLength = 32767;
      this.m_formulaTextBox.Multiline = true;
      this.m_formulaTextBox.Name = "m_formulaTextBox";
      this.m_formulaTextBox.PasswordChar = '\0';
      this.m_formulaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_formulaTextBox.SelectionLength = 0;
      this.m_formulaTextBox.SelectionStart = 0;
      this.m_formulaTextBox.Size = new System.Drawing.Size(796, 162);
      this.m_formulaTextBox.TabIndex = 0;
      this.m_formulaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_formulaTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_validateFormulaButton
      // 
      this.m_validateFormulaButton.AllowAnimations = true;
      this.m_validateFormulaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_validateFormulaButton.BackColor = System.Drawing.Color.Transparent;
      this.m_validateFormulaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_validateFormulaButton.ImageKey = "1420498403_340208.ico";
      this.m_validateFormulaButton.ImageList = this.EditButtonsImagelist;
      this.m_validateFormulaButton.Location = new System.Drawing.Point(530, 249);
      this.m_validateFormulaButton.Name = "m_validateFormulaButton";
      this.m_validateFormulaButton.RoundedCornersMask = ((byte)(15));
      this.m_validateFormulaButton.Size = new System.Drawing.Size(122, 43);
      this.m_validateFormulaButton.TabIndex = 7;
      this.m_validateFormulaButton.Text = "Validate";
      this.m_validateFormulaButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_validateFormulaButton.UseVisualStyleBackColor = true;
      this.m_validateFormulaButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_validateFormulaButton.Visible = false;
      // 
      // m_accountInformationGroupbox
      // 
      this.m_accountInformationGroupbox.Controls.Add(this.ProcessComboBox);
      this.m_accountInformationGroupbox.Controls.Add(this.m_ProcessLabel);
      this.m_accountInformationGroupbox.Controls.Add(this.ConsolidationOptionComboBox);
      this.m_accountInformationGroupbox.Controls.Add(this.CurrencyConversionComboBox);
      this.m_accountInformationGroupbox.Controls.Add(this.m_accountNameLabel);
      this.m_accountInformationGroupbox.Controls.Add(this.m_accountFormulaTypeLabel);
      this.m_accountInformationGroupbox.Controls.Add(this.m_accountTypeLabel);
      this.m_accountInformationGroupbox.Controls.Add(this.m_accountConsolidationOptionLabel);
      this.m_accountInformationGroupbox.Controls.Add(this.m_accountCurrenciesConversionLabel);
      this.m_accountInformationGroupbox.Controls.Add(this.FormulaTypeComboBox);
      this.m_accountInformationGroupbox.Controls.Add(this.TypeComboBox);
      this.m_accountInformationGroupbox.Controls.Add(this.Name_TB);
      this.m_accountInformationGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountInformationGroupbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.m_accountInformationGroupbox.Location = new System.Drawing.Point(3, 43);
      this.m_accountInformationGroupbox.Name = "m_accountInformationGroupbox";
      this.m_accountInformationGroupbox.Size = new System.Drawing.Size(836, 431);
      this.m_accountInformationGroupbox.TabIndex = 17;
      this.m_accountInformationGroupbox.TabStop = false;
      this.m_accountInformationGroupbox.Text = "Account information";
      // 
      // ConsolidationOptionComboBox
      // 
      this.ConsolidationOptionComboBox.BackColor = System.Drawing.Color.White;
      this.ConsolidationOptionComboBox.DisplayMember = "";
      this.ConsolidationOptionComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.ConsolidationOptionComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.ConsolidationOptionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.ConsolidationOptionComboBox.DropDownWidth = 465;
      this.ConsolidationOptionComboBox.Location = new System.Drawing.Point(240, 365);
      this.ConsolidationOptionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ConsolidationOptionComboBox.Name = "ConsolidationOptionComboBox";
      this.ConsolidationOptionComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.ConsolidationOptionComboBox.Size = new System.Drawing.Size(465, 34);
      this.ConsolidationOptionComboBox.TabIndex = 32;
      this.ConsolidationOptionComboBox.UseThemeBackColor = false;
      this.ConsolidationOptionComboBox.UseThemeDropDownArrowColor = true;
      this.ConsolidationOptionComboBox.ValueMember = "";
      this.ConsolidationOptionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.ConsolidationOptionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // CurrencyConversionComboBox
      // 
      this.CurrencyConversionComboBox.BackColor = System.Drawing.Color.White;
      this.CurrencyConversionComboBox.DisplayMember = "";
      this.CurrencyConversionComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.CurrencyConversionComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.CurrencyConversionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.CurrencyConversionComboBox.DropDownWidth = 465;
      this.CurrencyConversionComboBox.Location = new System.Drawing.Point(242, 303);
      this.CurrencyConversionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.CurrencyConversionComboBox.Name = "CurrencyConversionComboBox";
      this.CurrencyConversionComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.CurrencyConversionComboBox.Size = new System.Drawing.Size(465, 34);
      this.CurrencyConversionComboBox.TabIndex = 46;
      this.CurrencyConversionComboBox.UseThemeBackColor = false;
      this.CurrencyConversionComboBox.UseThemeDropDownArrowColor = true;
      this.CurrencyConversionComboBox.ValueMember = "";
      this.CurrencyConversionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.CurrencyConversionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountNameLabel
      // 
      this.m_accountNameLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountNameLabel.Ellipsis = false;
      this.m_accountNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountNameLabel.Location = new System.Drawing.Point(30, 63);
      this.m_accountNameLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountNameLabel.Multiline = true;
      this.m_accountNameLabel.Name = "m_accountNameLabel";
      this.m_accountNameLabel.Size = new System.Drawing.Size(194, 34);
      this.m_accountNameLabel.TabIndex = 45;
      this.m_accountNameLabel.Text = "Name";
      this.m_accountNameLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountNameLabel.UseMnemonics = true;
      this.m_accountNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountFormulaTypeLabel
      // 
      this.m_accountFormulaTypeLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountFormulaTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountFormulaTypeLabel.Ellipsis = false;
      this.m_accountFormulaTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountFormulaTypeLabel.Location = new System.Drawing.Point(30, 180);
      this.m_accountFormulaTypeLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountFormulaTypeLabel.Multiline = true;
      this.m_accountFormulaTypeLabel.Name = "m_accountFormulaTypeLabel";
      this.m_accountFormulaTypeLabel.Size = new System.Drawing.Size(194, 34);
      this.m_accountFormulaTypeLabel.TabIndex = 44;
      this.m_accountFormulaTypeLabel.Text = "Formula type";
      this.m_accountFormulaTypeLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountFormulaTypeLabel.UseMnemonics = true;
      this.m_accountFormulaTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountTypeLabel
      // 
      this.m_accountTypeLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountTypeLabel.Ellipsis = false;
      this.m_accountTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountTypeLabel.Location = new System.Drawing.Point(30, 240);
      this.m_accountTypeLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountTypeLabel.Multiline = true;
      this.m_accountTypeLabel.Name = "m_accountTypeLabel";
      this.m_accountTypeLabel.Size = new System.Drawing.Size(194, 34);
      this.m_accountTypeLabel.TabIndex = 43;
      this.m_accountTypeLabel.Text = "Account type";
      this.m_accountTypeLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountTypeLabel.UseMnemonics = true;
      this.m_accountTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountConsolidationOptionLabel
      // 
      this.m_accountConsolidationOptionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountConsolidationOptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountConsolidationOptionLabel.Ellipsis = false;
      this.m_accountConsolidationOptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountConsolidationOptionLabel.Location = new System.Drawing.Point(30, 365);
      this.m_accountConsolidationOptionLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountConsolidationOptionLabel.Multiline = true;
      this.m_accountConsolidationOptionLabel.Name = "m_accountConsolidationOptionLabel";
      this.m_accountConsolidationOptionLabel.Size = new System.Drawing.Size(189, 34);
      this.m_accountConsolidationOptionLabel.TabIndex = 42;
      this.m_accountConsolidationOptionLabel.Text = "Consolidation option";
      this.m_accountConsolidationOptionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountConsolidationOptionLabel.UseMnemonics = true;
      this.m_accountConsolidationOptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountCurrenciesConversionLabel
      // 
      this.m_accountCurrenciesConversionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountCurrenciesConversionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountCurrenciesConversionLabel.Ellipsis = false;
      this.m_accountCurrenciesConversionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountCurrenciesConversionLabel.Location = new System.Drawing.Point(30, 303);
      this.m_accountCurrenciesConversionLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountCurrenciesConversionLabel.Multiline = true;
      this.m_accountCurrenciesConversionLabel.Name = "m_accountCurrenciesConversionLabel";
      this.m_accountCurrenciesConversionLabel.Size = new System.Drawing.Size(189, 34);
      this.m_accountCurrenciesConversionLabel.TabIndex = 41;
      this.m_accountCurrenciesConversionLabel.Text = "Currencies conversion";
      this.m_accountCurrenciesConversionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountCurrenciesConversionLabel.UseMnemonics = true;
      this.m_accountCurrenciesConversionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // FormulaTypeComboBox
      // 
      this.FormulaTypeComboBox.BackColor = System.Drawing.Color.White;
      this.FormulaTypeComboBox.DisplayMember = "";
      this.FormulaTypeComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.FormulaTypeComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.FormulaTypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.FormulaTypeComboBox.DropDownWidth = 465;
      this.FormulaTypeComboBox.Location = new System.Drawing.Point(242, 180);
      this.FormulaTypeComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.FormulaTypeComboBox.Name = "FormulaTypeComboBox";
      this.FormulaTypeComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.FormulaTypeComboBox.Size = new System.Drawing.Size(465, 34);
      this.FormulaTypeComboBox.TabIndex = 32;
      this.FormulaTypeComboBox.UseThemeBackColor = false;
      this.FormulaTypeComboBox.UseThemeDropDownArrowColor = true;
      this.FormulaTypeComboBox.ValueMember = "";
      this.FormulaTypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.FormulaTypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // TypeComboBox
      // 
      this.TypeComboBox.BackColor = System.Drawing.Color.White;
      this.TypeComboBox.DisplayMember = "";
      this.TypeComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.TypeComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.TypeComboBox.DropDownWidth = 465;
      this.TypeComboBox.Location = new System.Drawing.Point(242, 240);
      this.TypeComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TypeComboBox.Name = "TypeComboBox";
      this.TypeComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.TypeComboBox.Size = new System.Drawing.Size(465, 34);
      this.TypeComboBox.TabIndex = 31;
      this.TypeComboBox.UseThemeBackColor = false;
      this.TypeComboBox.UseThemeDropDownArrowColor = true;
      this.TypeComboBox.ValueMember = "";
      this.TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // Name_TB
      // 
      this.Name_TB.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.Name_TB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.Name_TB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.Name_TB.DefaultText = "Empty...";
      this.Name_TB.Location = new System.Drawing.Point(242, 63);
      this.Name_TB.MaxLength = 32767;
      this.Name_TB.Name = "Name_TB";
      this.Name_TB.PasswordChar = '\0';
      this.Name_TB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.Name_TB.SelectionLength = 0;
      this.Name_TB.SelectionStart = 0;
      this.Name_TB.Size = new System.Drawing.Size(464, 34);
      this.Name_TB.TabIndex = 1;
      this.Name_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.Name_TB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // GlobalFactsPanel
      // 
      this.GlobalFactsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.GlobalFactsPanel.Location = new System.Drawing.Point(4, 89);
      this.GlobalFactsPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.GlobalFactsPanel.Name = "GlobalFactsPanel";
      this.GlobalFactsPanel.Size = new System.Drawing.Size(195, 952);
      this.GlobalFactsPanel.TabIndex = 3;
      // 
      // m_globalFactsLabel
      // 
      this.m_globalFactsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_globalFactsLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_globalFactsLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_globalFactsLabel.Ellipsis = false;
      this.m_globalFactsLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_globalFactsLabel.Location = new System.Drawing.Point(18, 43);
      this.m_globalFactsLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_globalFactsLabel.Multiline = true;
      this.m_globalFactsLabel.Name = "m_globalFactsLabel";
      this.m_globalFactsLabel.Size = new System.Drawing.Size(223, 25);
      this.m_globalFactsLabel.TabIndex = 0;
      this.m_globalFactsLabel.Text = "Macro economic indicators";
      this.m_globalFactsLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_globalFactsLabel.UseMnemonics = true;
      this.m_globalFactsLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // accountsIL
      // 
      this.accountsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("accountsIL.ImageStream")));
      this.accountsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.accountsIL.Images.SetKeyName(0, "WC blue.png");
      this.accountsIL.Images.SetKeyName(1, "pencil.ico");
      this.accountsIL.Images.SetKeyName(2, "formula.ico");
      this.accountsIL.Images.SetKeyName(3, "sum purple.png");
      this.accountsIL.Images.SetKeyName(4, "BS Blue.png");
      this.accountsIL.Images.SetKeyName(5, "favicon(81).ico");
      this.accountsIL.Images.SetKeyName(6, "func.png");
      this.accountsIL.Images.SetKeyName(7, "config blue circle.png");
      // 
      // TVRCM
      // 
      this.TVRCM.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.TVRCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSubAccountToolStripMenuItem,
            this.AddCategoryToolStripMenuItem,
            this.ToolStripSeparator1,
            this.DeleteAccountToolStripMenuItem,
            this.ToolStripSeparator3,
            this.m_allocationKeyButton,
            this.ToolStripSeparator4,
            this.DropHierarchyToExcelToolStripMenuItem});
      this.TVRCM.Name = "ContextMenuStripTV";
      this.TVRCM.Size = new System.Drawing.Size(283, 172);
      // 
      // AddSubAccountToolStripMenuItem
      // 
      this.AddSubAccountToolStripMenuItem.Image = global::FBI.Properties.Resources.registry_add;
      this.AddSubAccountToolStripMenuItem.Name = "AddSubAccountToolStripMenuItem";
      this.AddSubAccountToolStripMenuItem.Size = new System.Drawing.Size(282, 30);
      this.AddSubAccountToolStripMenuItem.Text = "Add account";
      // 
      // AddCategoryToolStripMenuItem
      // 
      this.AddCategoryToolStripMenuItem.Image = global::FBI.Properties.Resources.favicon_81_;
      this.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem";
      this.AddCategoryToolStripMenuItem.Size = new System.Drawing.Size(282, 30);
      this.AddCategoryToolStripMenuItem.Text = "Add Category";
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(279, 6);
      // 
      // DeleteAccountToolStripMenuItem
      // 
      this.DeleteAccountToolStripMenuItem.Image = global::FBI.Properties.Resources.registry_delete;
      this.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem";
      this.DeleteAccountToolStripMenuItem.Size = new System.Drawing.Size(282, 30);
      this.DeleteAccountToolStripMenuItem.Text = "Delete Account";
      // 
      // ToolStripSeparator3
      // 
      this.ToolStripSeparator3.Name = "ToolStripSeparator3";
      this.ToolStripSeparator3.Size = new System.Drawing.Size(279, 6);
      // 
      // m_allocationKeyButton
      // 
      this.m_allocationKeyButton.Name = "m_allocationKeyButton";
      this.m_allocationKeyButton.Size = new System.Drawing.Size(282, 30);
      this.m_allocationKeyButton.Text = "Set allocation keys";
      // 
      // ToolStripSeparator4
      // 
      this.ToolStripSeparator4.Name = "ToolStripSeparator4";
      this.ToolStripSeparator4.Size = new System.Drawing.Size(279, 6);
      // 
      // DropHierarchyToExcelToolStripMenuItem
      // 
      this.DropHierarchyToExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.excel_blue2;
      this.DropHierarchyToExcelToolStripMenuItem.Name = "DropHierarchyToExcelToolStripMenuItem";
      this.DropHierarchyToExcelToolStripMenuItem.Size = new System.Drawing.Size(282, 30);
      this.DropHierarchyToExcelToolStripMenuItem.Text = "Drop accounts on Excel";
      // 
      // MainMenu
      // 
      this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
      this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.DropHierarchyToExcelToolStripMenuItem1,
            this.HelpToolStripMenuItem});
      this.MainMenu.Location = new System.Drawing.Point(0, 0);
      this.MainMenu.Name = "MainMenu";
      this.MainMenu.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.MainMenu.Size = new System.Drawing.Size(1472, 35);
      this.MainMenu.TabIndex = 25;
      this.MainMenu.Text = "MenuStrip1";
      // 
      // NewToolStripMenuItem
      // 
      this.NewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateANewAccountToolStripMenuItem,
            this.CreateANewCategoryToolStripMenuItem,
            this.DeleteAccountToolStripMenuItem1,
            this.ToolStripSeparator2});
      this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
      this.NewToolStripMenuItem.Size = new System.Drawing.Size(158, 29);
      this.NewToolStripMenuItem.Text = "[general.account]";
      // 
      // CreateANewAccountToolStripMenuItem
      // 
      this.CreateANewAccountToolStripMenuItem.Image = global::FBI.Properties.Resources.registry_add;
      this.CreateANewAccountToolStripMenuItem.Name = "CreateANewAccountToolStripMenuItem";
      this.CreateANewAccountToolStripMenuItem.Size = new System.Drawing.Size(377, 30);
      this.CreateANewAccountToolStripMenuItem.Text = "[accounts_edition.new_account]";
      // 
      // CreateANewCategoryToolStripMenuItem
      // 
      this.CreateANewCategoryToolStripMenuItem.Image = global::FBI.Properties.Resources.favicon_81_;
      this.CreateANewCategoryToolStripMenuItem.Name = "CreateANewCategoryToolStripMenuItem";
      this.CreateANewCategoryToolStripMenuItem.Size = new System.Drawing.Size(377, 30);
      this.CreateANewCategoryToolStripMenuItem.Text = "[accounts_edition.new_tab_account]";
      // 
      // DeleteAccountToolStripMenuItem1
      // 
      this.DeleteAccountToolStripMenuItem1.Image = global::FBI.Properties.Resources.registry_delete;
      this.DeleteAccountToolStripMenuItem1.Name = "DeleteAccountToolStripMenuItem1";
      this.DeleteAccountToolStripMenuItem1.Size = new System.Drawing.Size(377, 30);
      this.DeleteAccountToolStripMenuItem1.Text = "[accounts_edition.delete_account]";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(374, 6);
      // 
      // DropHierarchyToExcelToolStripMenuItem1
      // 
      this.DropHierarchyToExcelToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem,
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem});
      this.DropHierarchyToExcelToolStripMenuItem1.Name = "DropHierarchyToExcelToolStripMenuItem1";
      this.DropHierarchyToExcelToolStripMenuItem1.Size = new System.Drawing.Size(62, 29);
      this.DropHierarchyToExcelToolStripMenuItem1.Text = "Excel";
      // 
      // DropAllAccountsHierarchyToExcelToolStripMenuItem
      // 
      this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.excel_blue2;
      this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Name = "DropAllAccountsHierarchyToExcelToolStripMenuItem";
      this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Size = new System.Drawing.Size(500, 30);
      this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = "[accounts_edition.drop_to_excel]";
      // 
      // DropSelectedAccountHierarchyToExcelToolStripMenuItem
      // 
      this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.Excel_Green_32x32;
      this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Name = "DropSelectedAccountHierarchyToExcelToolStripMenuItem";
      this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Size = new System.Drawing.Size(500, 30);
      this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = "[accounts_edition.drop_selected_hierarchy_to_excel]";
      // 
      // HelpToolStripMenuItem
      // 
      this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
      this.HelpToolStripMenuItem.Size = new System.Drawing.Size(130, 29);
      this.HelpToolStripMenuItem.Text = "[general.help]";
      // 
      // m_globalFactsImageList
      // 
      this.m_globalFactsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_globalFactsImageList.ImageStream")));
      this.m_globalFactsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_globalFactsImageList.Images.SetKeyName(0, "currency_euro.ico");
      this.m_globalFactsImageList.Images.SetKeyName(1, "chart_line.ico");
      this.m_globalFactsImageList.Images.SetKeyName(2, "money_interest.ico");
      // 
      // m_ProcessLabel
      // 
      this.m_ProcessLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_ProcessLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_ProcessLabel.Ellipsis = false;
      this.m_ProcessLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_ProcessLabel.Location = new System.Drawing.Point(30, 120);
      this.m_ProcessLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_ProcessLabel.Multiline = true;
      this.m_ProcessLabel.Name = "m_ProcessLabel";
      this.m_ProcessLabel.Size = new System.Drawing.Size(194, 34);
      this.m_ProcessLabel.TabIndex = 45;
      this.m_ProcessLabel.Text = "Process Selection";
      this.m_ProcessLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_ProcessLabel.UseMnemonics = true;
      this.m_ProcessLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ProcessComboBox
      // 
      this.ProcessComboBox.BackColor = System.Drawing.Color.White;
      this.ProcessComboBox.DisplayMember = "";
      this.ProcessComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.ProcessComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.ProcessComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.ProcessComboBox.DropDownWidth = 465;
      this.ProcessComboBox.Location = new System.Drawing.Point(242, 120);
      this.ProcessComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ProcessComboBox.Name = "ProcessComboBox";
      this.ProcessComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.ProcessComboBox.Size = new System.Drawing.Size(465, 34);
      this.ProcessComboBox.TabIndex = 33;
      this.ProcessComboBox.UseThemeBackColor = false;
      this.ProcessComboBox.UseThemeDropDownArrowColor = true;
      this.ProcessComboBox.ValueMember = "";
      this.ProcessComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.ProcessComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // AccountsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.MainMenu);
      this.Controls.Add(this.SplitContainer1);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "AccountsView";
      this.Size = new System.Drawing.Size(1472, 1065);
      this.SplitContainer1.Panel1.ResumeLayout(false);
      this.SplitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.SplitContainer2.Panel1.ResumeLayout(false);
      this.SplitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
      this.SplitContainer2.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.m_accountDescriptionGroupbox.ResumeLayout(false);
      this.m_accountFormulaGroupbox.ResumeLayout(false);
      this.m_accountInformationGroupbox.ResumeLayout(false);
      this.TVRCM.ResumeLayout(false);
      this.MainMenu.ResumeLayout(false);
      this.MainMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    internal System.Windows.Forms.SplitContainer SplitContainer1;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
    internal System.Windows.Forms.GroupBox m_accountInformationGroupbox;
    internal VIBlend.WinForms.Controls.vTextBox Name_TB;
    internal System.Windows.Forms.GroupBox m_accountFormulaGroupbox;
    internal System.Windows.Forms.ImageList accountsIL;
    internal System.Windows.Forms.ImageList EditButtonsImagelist;
    internal System.Windows.Forms.ContextMenuStrip TVRCM;
    internal System.Windows.Forms.ToolStripMenuItem AddSubAccountToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem AddCategoryToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem DeleteAccountToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem DropHierarchyToExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.MenuStrip MainMenu;
    internal System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem CreateANewAccountToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem CreateANewCategoryToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem DeleteAccountToolStripMenuItem1;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem DropHierarchyToExcelToolStripMenuItem1;
    internal System.Windows.Forms.ToolStripMenuItem DropAllAccountsHierarchyToExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem DropSelectedAccountHierarchyToExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
    internal System.Windows.Forms.GroupBox m_accountDescriptionGroupbox;
    internal VIBlend.WinForms.Controls.vComboBox FormulaTypeComboBox;
    internal VIBlend.WinForms.Controls.vComboBox TypeComboBox;
    internal VIBlend.WinForms.Controls.vLabel m_accountConsolidationOptionLabel;
    internal VIBlend.WinForms.Controls.vLabel m_accountCurrenciesConversionLabel;
    internal VIBlend.WinForms.Controls.vLabel m_accountNameLabel;
    internal VIBlend.WinForms.Controls.vLabel m_accountFormulaTypeLabel;
    internal VIBlend.WinForms.Controls.vLabel m_accountTypeLabel;
    internal VIBlend.WinForms.Controls.vComboBox ConsolidationOptionComboBox;
    internal VIBlend.WinForms.Controls.vComboBox CurrencyConversionComboBox;
    internal System.Windows.Forms.Panel GlobalFactsPanel;
    internal VIBlend.WinForms.Controls.vLabel m_globalFactsLabel;
    internal System.Windows.Forms.SplitContainer SplitContainer2;
    internal VIBlend.WinForms.Controls.vTextBox m_descriptionTextBox;
    internal VIBlend.WinForms.Controls.vTextBox m_formulaTextBox;
    internal System.Windows.Forms.Panel AccountsTVPanel;
    internal System.Windows.Forms.ImageList m_globalFactsImageList;
    internal VIBlend.WinForms.Controls.vButton m_validateFormulaButton;
    internal VIBlend.WinForms.Controls.vButton SaveDescriptionBT;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
    internal System.Windows.Forms.ToolStripMenuItem m_allocationKeyButton;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    internal VIBlend.WinForms.Controls.vButton m_formulaEditionButton;

    internal VIBlend.WinForms.Controls.vButton m_cancelFormulaEditionButton;
    internal VIBlend.WinForms.Controls.vComboBox ProcessComboBox;
    internal VIBlend.WinForms.Controls.vLabel m_ProcessLabel;
  }
}