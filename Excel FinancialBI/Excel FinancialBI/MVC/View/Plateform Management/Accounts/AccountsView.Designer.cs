﻿using Microsoft.VisualBasic;
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
            this.m_formulaTextBox = new VIBlend.WinForms.Controls.vRichTextBox();
            this.m_validateFormulaButton = new VIBlend.WinForms.Controls.vButton();
            this.m_accountInformationGroupbox = new System.Windows.Forms.GroupBox();
            this.m_formatCB = new VIBlend.WinForms.Controls.vComboBox();
            this.m_formatLabel = new VIBlend.WinForms.Controls.vLabel();
            this.ProcessCB = new VIBlend.WinForms.Controls.vComboBox();
            this.m_ProcessLabel = new VIBlend.WinForms.Controls.vLabel();
            this.ConsolidationOptionCB = new VIBlend.WinForms.Controls.vComboBox();
            this.CurrencyCB = new VIBlend.WinForms.Controls.vComboBox();
            this.m_accountNameLabel = new VIBlend.WinForms.Controls.vLabel();
            this.m_accountFormulaTypeLabel = new VIBlend.WinForms.Controls.vLabel();
            this.m_accountTypeLabel = new VIBlend.WinForms.Controls.vLabel();
            this.m_accountConsolidationOptionLabel = new VIBlend.WinForms.Controls.vLabel();
            this.m_accountCurrenciesConversionLabel = new VIBlend.WinForms.Controls.vLabel();
            this.FormulaTypeCB = new VIBlend.WinForms.Controls.vComboBox();
            this.TypeCB = new VIBlend.WinForms.Controls.vComboBox();
            this.Name_TB = new VIBlend.WinForms.Controls.vTextBox();
            this.GlobalFactsPanel = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.m_expandRightBT = new VIBlend.WinForms.Controls.vButton();
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
            this.m_dropToExcelRightClickMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateANewAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateANewCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteAccountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DropHierarchyToExcelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_globalFactsImageList = new System.Windows.Forms.ImageList(this.components);
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
            this.GlobalFactsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.TVRCM.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.AccountsTVPanel);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
            this.SplitContainer1.Size = new System.Drawing.Size(1308, 852);
            this.SplitContainer1.SplitterDistance = 324;
            this.SplitContainer1.SplitterWidth = 5;
            this.SplitContainer1.TabIndex = 23;
            // 
            // AccountsTVPanel
            // 
            this.AccountsTVPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsTVPanel.Location = new System.Drawing.Point(3, 43);
            this.AccountsTVPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AccountsTVPanel.Name = "AccountsTVPanel";
            this.AccountsTVPanel.Size = new System.Drawing.Size(320, 805);
            this.AccountsTVPanel.TabIndex = 3;
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.SplitContainer2.Panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SplitContainer2.Size = new System.Drawing.Size(979, 852);
            this.SplitContainer2.SplitterDistance = 935;
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
            this.TableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 4;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 398F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.77032F));
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.22968F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(935, 852);
            this.TableLayoutPanel2.TabIndex = 1;
            // 
            // m_accountDescriptionGroupbox
            // 
            this.m_accountDescriptionGroupbox.Controls.Add(this.m_descriptionTextBox);
            this.m_accountDescriptionGroupbox.Controls.Add(this.SaveDescriptionBT);
            this.m_accountDescriptionGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_accountDescriptionGroupbox.Location = new System.Drawing.Point(4, 665);
            this.m_accountDescriptionGroupbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountDescriptionGroupbox.Name = "m_accountDescriptionGroupbox";
            this.m_accountDescriptionGroupbox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountDescriptionGroupbox.Size = new System.Drawing.Size(927, 183);
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
            this.m_descriptionTextBox.Location = new System.Drawing.Point(8, 26);
            this.m_descriptionTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_descriptionTextBox.MaxLength = 32767;
            this.m_descriptionTextBox.Multiline = true;
            this.m_descriptionTextBox.Name = "m_descriptionTextBox";
            this.m_descriptionTextBox.PasswordChar = '\0';
            this.m_descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.m_descriptionTextBox.SelectionLength = 0;
            this.m_descriptionTextBox.SelectionStart = 0;
            this.m_descriptionTextBox.Size = new System.Drawing.Size(893, 100);
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
            this.SaveDescriptionBT.Location = new System.Drawing.Point(653, 132);
            this.SaveDescriptionBT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveDescriptionBT.Name = "SaveDescriptionBT";
            this.SaveDescriptionBT.RoundedCornersMask = ((byte)(15));
            this.SaveDescriptionBT.Size = new System.Drawing.Size(249, 34);
            this.SaveDescriptionBT.TabIndex = 7;
            this.SaveDescriptionBT.Text = "[accounts.save_description]";
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
            this.m_accountFormulaGroupbox.Location = new System.Drawing.Point(4, 434);
            this.m_accountFormulaGroupbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountFormulaGroupbox.Name = "m_accountFormulaGroupbox";
            this.m_accountFormulaGroupbox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountFormulaGroupbox.Size = new System.Drawing.Size(927, 223);
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
            this.m_cancelFormulaEditionButton.Location = new System.Drawing.Point(791, 172);
            this.m_cancelFormulaEditionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_cancelFormulaEditionButton.Name = "m_cancelFormulaEditionButton";
            this.m_cancelFormulaEditionButton.RoundedCornersMask = ((byte)(15));
            this.m_cancelFormulaEditionButton.Size = new System.Drawing.Size(108, 34);
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
            this.m_formulaEditionButton.Location = new System.Drawing.Point(8, 27);
            this.m_formulaEditionButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_formulaEditionButton.Name = "m_formulaEditionButton";
            this.m_formulaEditionButton.RoundedCornersMask = ((byte)(15));
            this.m_formulaEditionButton.Size = new System.Drawing.Size(164, 27);
            this.m_formulaEditionButton.TabIndex = 8;
            this.m_formulaEditionButton.Text = "Edit formula";
            this.m_formulaEditionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_formulaEditionButton.UseVisualStyleBackColor = false;
            this.m_formulaEditionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // m_formulaTextBox
            // 
            this.m_formulaTextBox.AllowAnimations = false;
            this.m_formulaTextBox.AllowDrop = true;
            this.m_formulaTextBox.AllowFocused = false;
            this.m_formulaTextBox.AllowHighlight = false;
            this.m_formulaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_formulaTextBox.BackColor = System.Drawing.Color.White;
            this.m_formulaTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.m_formulaTextBox.Enabled = false;
            this.m_formulaTextBox.GleamWidth = 1;
            this.m_formulaTextBox.Location = new System.Drawing.Point(8, 64);
            this.m_formulaTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_formulaTextBox.MaxLength = 32767;
            this.m_formulaTextBox.Multiline = true;
            this.m_formulaTextBox.Name = "m_formulaTextBox";
            this.m_formulaTextBox.Readonly = false;
            this.m_formulaTextBox.Size = new System.Drawing.Size(893, 103);
            this.m_formulaTextBox.TabIndex = 0;
            this.m_formulaTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
            // 
            // m_validateFormulaButton
            // 
            this.m_validateFormulaButton.AllowAnimations = true;
            this.m_validateFormulaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_validateFormulaButton.BackColor = System.Drawing.Color.Transparent;
            this.m_validateFormulaButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_validateFormulaButton.ImageKey = "1420498403_340208.ico";
            this.m_validateFormulaButton.ImageList = this.EditButtonsImagelist;
            this.m_validateFormulaButton.Location = new System.Drawing.Point(657, 172);
            this.m_validateFormulaButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_validateFormulaButton.Name = "m_validateFormulaButton";
            this.m_validateFormulaButton.RoundedCornersMask = ((byte)(15));
            this.m_validateFormulaButton.Size = new System.Drawing.Size(108, 34);
            this.m_validateFormulaButton.TabIndex = 7;
            this.m_validateFormulaButton.Text = "Validate";
            this.m_validateFormulaButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_validateFormulaButton.UseVisualStyleBackColor = true;
            this.m_validateFormulaButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.m_validateFormulaButton.Visible = false;
            // 
            // m_accountInformationGroupbox
            // 
            this.m_accountInformationGroupbox.Controls.Add(this.m_formatCB);
            this.m_accountInformationGroupbox.Controls.Add(this.m_formatLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.ProcessCB);
            this.m_accountInformationGroupbox.Controls.Add(this.m_ProcessLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.ConsolidationOptionCB);
            this.m_accountInformationGroupbox.Controls.Add(this.CurrencyCB);
            this.m_accountInformationGroupbox.Controls.Add(this.m_accountNameLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.m_accountFormulaTypeLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.m_accountTypeLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.m_accountConsolidationOptionLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.m_accountCurrenciesConversionLabel);
            this.m_accountInformationGroupbox.Controls.Add(this.FormulaTypeCB);
            this.m_accountInformationGroupbox.Controls.Add(this.TypeCB);
            this.m_accountInformationGroupbox.Controls.Add(this.Name_TB);
            this.m_accountInformationGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_accountInformationGroupbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_accountInformationGroupbox.Location = new System.Drawing.Point(3, 34);
            this.m_accountInformationGroupbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_accountInformationGroupbox.Name = "m_accountInformationGroupbox";
            this.m_accountInformationGroupbox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_accountInformationGroupbox.Size = new System.Drawing.Size(929, 394);
            this.m_accountInformationGroupbox.TabIndex = 17;
            this.m_accountInformationGroupbox.TabStop = false;
            this.m_accountInformationGroupbox.Text = "Account information";
            // 
            // m_formatCB
            // 
            this.m_formatCB.BackColor = System.Drawing.Color.White;
            this.m_formatCB.DisplayMember = "";
            this.m_formatCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
            this.m_formatCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
            this.m_formatCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
            this.m_formatCB.DropDownWidth = 413;
            this.m_formatCB.Location = new System.Drawing.Point(213, 340);
            this.m_formatCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_formatCB.Name = "m_formatCB";
            this.m_formatCB.RoundedCornersMaskListItem = ((byte)(15));
            this.m_formatCB.Size = new System.Drawing.Size(413, 27);
            this.m_formatCB.TabIndex = 33;
            this.m_formatCB.UseThemeBackColor = false;
            this.m_formatCB.UseThemeDropDownArrowColor = true;
            this.m_formatCB.ValueMember = "";
            this.m_formatCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.m_formatCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // m_formatLabel
            // 
            this.m_formatLabel.BackColor = System.Drawing.Color.Transparent;
            this.m_formatLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
            this.m_formatLabel.Ellipsis = false;
            this.m_formatLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.m_formatLabel.Location = new System.Drawing.Point(27, 340);
            this.m_formatLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_formatLabel.Multiline = true;
            this.m_formatLabel.Name = "m_formatLabel";
            this.m_formatLabel.Size = new System.Drawing.Size(168, 27);
            this.m_formatLabel.TabIndex = 43;
            this.m_formatLabel.Text = "Format";
            this.m_formatLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_formatLabel.UseMnemonics = true;
            this.m_formatLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // ProcessCB
            // 
            this.ProcessCB.BackColor = System.Drawing.Color.White;
            this.ProcessCB.DisplayMember = "";
            this.ProcessCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
            this.ProcessCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
            this.ProcessCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
            this.ProcessCB.DropDownWidth = 413;
            this.ProcessCB.Location = new System.Drawing.Point(215, 96);
            this.ProcessCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ProcessCB.Name = "ProcessCB";
            this.ProcessCB.RoundedCornersMaskListItem = ((byte)(15));
            this.ProcessCB.Size = new System.Drawing.Size(413, 27);
            this.ProcessCB.TabIndex = 33;
            this.ProcessCB.UseThemeBackColor = false;
            this.ProcessCB.UseThemeDropDownArrowColor = true;
            this.ProcessCB.ValueMember = "";
            this.ProcessCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.ProcessCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // m_ProcessLabel
            // 
            this.m_ProcessLabel.BackColor = System.Drawing.Color.Transparent;
            this.m_ProcessLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
            this.m_ProcessLabel.Ellipsis = false;
            this.m_ProcessLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.m_ProcessLabel.Location = new System.Drawing.Point(27, 96);
            this.m_ProcessLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_ProcessLabel.Multiline = true;
            this.m_ProcessLabel.Name = "m_ProcessLabel";
            this.m_ProcessLabel.Size = new System.Drawing.Size(172, 27);
            this.m_ProcessLabel.TabIndex = 45;
            this.m_ProcessLabel.Text = "Process Selection";
            this.m_ProcessLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_ProcessLabel.UseMnemonics = true;
            this.m_ProcessLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // ConsolidationOptionCB
            // 
            this.ConsolidationOptionCB.BackColor = System.Drawing.Color.White;
            this.ConsolidationOptionCB.DisplayMember = "";
            this.ConsolidationOptionCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
            this.ConsolidationOptionCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
            this.ConsolidationOptionCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
            this.ConsolidationOptionCB.DropDownWidth = 413;
            this.ConsolidationOptionCB.Location = new System.Drawing.Point(213, 292);
            this.ConsolidationOptionCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConsolidationOptionCB.Name = "ConsolidationOptionCB";
            this.ConsolidationOptionCB.RoundedCornersMaskListItem = ((byte)(15));
            this.ConsolidationOptionCB.Size = new System.Drawing.Size(413, 27);
            this.ConsolidationOptionCB.TabIndex = 32;
            this.ConsolidationOptionCB.UseThemeBackColor = false;
            this.ConsolidationOptionCB.UseThemeDropDownArrowColor = true;
            this.ConsolidationOptionCB.ValueMember = "";
            this.ConsolidationOptionCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.ConsolidationOptionCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // CurrencyCB
            // 
            this.CurrencyCB.BackColor = System.Drawing.Color.White;
            this.CurrencyCB.DisplayMember = "";
            this.CurrencyCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
            this.CurrencyCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
            this.CurrencyCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
            this.CurrencyCB.DropDownWidth = 413;
            this.CurrencyCB.Location = new System.Drawing.Point(215, 242);
            this.CurrencyCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CurrencyCB.Name = "CurrencyCB";
            this.CurrencyCB.RoundedCornersMaskListItem = ((byte)(15));
            this.CurrencyCB.Size = new System.Drawing.Size(413, 27);
            this.CurrencyCB.TabIndex = 46;
            this.CurrencyCB.UseThemeBackColor = false;
            this.CurrencyCB.UseThemeDropDownArrowColor = true;
            this.CurrencyCB.ValueMember = "";
            this.CurrencyCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.CurrencyCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // m_accountNameLabel
            // 
            this.m_accountNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.m_accountNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
            this.m_accountNameLabel.Ellipsis = false;
            this.m_accountNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.m_accountNameLabel.Location = new System.Drawing.Point(27, 50);
            this.m_accountNameLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountNameLabel.Multiline = true;
            this.m_accountNameLabel.Name = "m_accountNameLabel";
            this.m_accountNameLabel.Size = new System.Drawing.Size(172, 27);
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
            this.m_accountFormulaTypeLabel.Location = new System.Drawing.Point(27, 144);
            this.m_accountFormulaTypeLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountFormulaTypeLabel.Multiline = true;
            this.m_accountFormulaTypeLabel.Name = "m_accountFormulaTypeLabel";
            this.m_accountFormulaTypeLabel.Size = new System.Drawing.Size(172, 27);
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
            this.m_accountTypeLabel.Location = new System.Drawing.Point(27, 192);
            this.m_accountTypeLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountTypeLabel.Multiline = true;
            this.m_accountTypeLabel.Name = "m_accountTypeLabel";
            this.m_accountTypeLabel.Size = new System.Drawing.Size(172, 27);
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
            this.m_accountConsolidationOptionLabel.Location = new System.Drawing.Point(27, 292);
            this.m_accountConsolidationOptionLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountConsolidationOptionLabel.Multiline = true;
            this.m_accountConsolidationOptionLabel.Name = "m_accountConsolidationOptionLabel";
            this.m_accountConsolidationOptionLabel.Size = new System.Drawing.Size(168, 27);
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
            this.m_accountCurrenciesConversionLabel.Location = new System.Drawing.Point(27, 242);
            this.m_accountCurrenciesConversionLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_accountCurrenciesConversionLabel.Multiline = true;
            this.m_accountCurrenciesConversionLabel.Name = "m_accountCurrenciesConversionLabel";
            this.m_accountCurrenciesConversionLabel.Size = new System.Drawing.Size(168, 27);
            this.m_accountCurrenciesConversionLabel.TabIndex = 41;
            this.m_accountCurrenciesConversionLabel.Text = "Currencies conversion";
            this.m_accountCurrenciesConversionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_accountCurrenciesConversionLabel.UseMnemonics = true;
            this.m_accountCurrenciesConversionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // FormulaTypeCB
            // 
            this.FormulaTypeCB.BackColor = System.Drawing.Color.White;
            this.FormulaTypeCB.DisplayMember = "";
            this.FormulaTypeCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
            this.FormulaTypeCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
            this.FormulaTypeCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
            this.FormulaTypeCB.DropDownWidth = 413;
            this.FormulaTypeCB.Location = new System.Drawing.Point(215, 144);
            this.FormulaTypeCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FormulaTypeCB.Name = "FormulaTypeCB";
            this.FormulaTypeCB.RoundedCornersMaskListItem = ((byte)(15));
            this.FormulaTypeCB.Size = new System.Drawing.Size(413, 27);
            this.FormulaTypeCB.TabIndex = 32;
            this.FormulaTypeCB.UseThemeBackColor = false;
            this.FormulaTypeCB.UseThemeDropDownArrowColor = true;
            this.FormulaTypeCB.ValueMember = "";
            this.FormulaTypeCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.FormulaTypeCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // TypeCB
            // 
            this.TypeCB.BackColor = System.Drawing.Color.White;
            this.TypeCB.DisplayMember = "";
            this.TypeCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
            this.TypeCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
            this.TypeCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
            this.TypeCB.DropDownWidth = 413;
            this.TypeCB.Location = new System.Drawing.Point(215, 192);
            this.TypeCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TypeCB.Name = "TypeCB";
            this.TypeCB.RoundedCornersMaskListItem = ((byte)(15));
            this.TypeCB.Size = new System.Drawing.Size(413, 27);
            this.TypeCB.TabIndex = 31;
            this.TypeCB.UseThemeBackColor = false;
            this.TypeCB.UseThemeDropDownArrowColor = true;
            this.TypeCB.ValueMember = "";
            this.TypeCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            this.TypeCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // Name_TB
            // 
            this.Name_TB.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Name_TB.BoundsOffset = new System.Drawing.Size(1, 1);
            this.Name_TB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.Name_TB.DefaultText = "Empty...";
            this.Name_TB.Location = new System.Drawing.Point(215, 50);
            this.Name_TB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name_TB.MaxLength = 32767;
            this.Name_TB.Name = "Name_TB";
            this.Name_TB.PasswordChar = '\0';
            this.Name_TB.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Name_TB.SelectionLength = 0;
            this.Name_TB.SelectionStart = 0;
            this.Name_TB.Size = new System.Drawing.Size(412, 27);
            this.Name_TB.TabIndex = 1;
            this.Name_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Name_TB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // GlobalFactsPanel
            // 
            this.GlobalFactsPanel.Controls.Add(this.splitContainer3);
            this.GlobalFactsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GlobalFactsPanel.Location = new System.Drawing.Point(0, 0);
            this.GlobalFactsPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GlobalFactsPanel.Name = "GlobalFactsPanel";
            this.GlobalFactsPanel.Size = new System.Drawing.Size(40, 852);
            this.GlobalFactsPanel.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.m_expandRightBT);
            this.splitContainer3.Size = new System.Drawing.Size(40, 852);
            this.splitContainer3.SplitterDistance = 39;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // m_expandRightBT
            // 
            this.m_expandRightBT.AllowAnimations = true;
            this.m_expandRightBT.BackColor = System.Drawing.Color.Transparent;
            this.m_expandRightBT.Location = new System.Drawing.Point(8, 7);
            this.m_expandRightBT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_expandRightBT.Name = "m_expandRightBT";
            this.m_expandRightBT.RoundedCornersMask = ((byte)(15));
            this.m_expandRightBT.Size = new System.Drawing.Size(28, 26);
            this.m_expandRightBT.TabIndex = 2;
            this.m_expandRightBT.Text = "-";
            this.m_expandRightBT.UseVisualStyleBackColor = false;
            this.m_expandRightBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
            // 
            // m_globalFactsLabel
            // 
            this.m_globalFactsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_globalFactsLabel.BackColor = System.Drawing.Color.Transparent;
            this.m_globalFactsLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
            this.m_globalFactsLabel.Ellipsis = false;
            this.m_globalFactsLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.m_globalFactsLabel.Location = new System.Drawing.Point(4, 14);
            this.m_globalFactsLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_globalFactsLabel.Multiline = true;
            this.m_globalFactsLabel.Name = "m_globalFactsLabel";
            this.m_globalFactsLabel.Size = new System.Drawing.Size(84, 20);
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
            this.m_dropToExcelRightClickMenu});
            this.TVRCM.Name = "ContextMenuStripTV";
            this.TVRCM.Size = new System.Drawing.Size(244, 172);
            // 
            // AddSubAccountToolStripMenuItem
            // 
            this.AddSubAccountToolStripMenuItem.Image = global::FBI.Properties.Resources.Financial_BI_dark_blue_add;
            this.AddSubAccountToolStripMenuItem.Name = "AddSubAccountToolStripMenuItem";
            this.AddSubAccountToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.AddSubAccountToolStripMenuItem.Text = "Add account";
            // 
            // AddCategoryToolStripMenuItem
            // 
            this.AddCategoryToolStripMenuItem.Image = global::FBI.Properties.Resources.favicon_81_;
            this.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem";
            this.AddCategoryToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.AddCategoryToolStripMenuItem.Text = "Add Category";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(240, 6);
            // 
            // DeleteAccountToolStripMenuItem
            // 
            this.DeleteAccountToolStripMenuItem.Image = global::FBI.Properties.Resources.imageres_89;
            this.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem";
            this.DeleteAccountToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.DeleteAccountToolStripMenuItem.Text = "Delete Account";
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(240, 6);
            // 
            // m_allocationKeyButton
            // 
            this.m_allocationKeyButton.Name = "m_allocationKeyButton";
            this.m_allocationKeyButton.Size = new System.Drawing.Size(243, 30);
            this.m_allocationKeyButton.Text = "Set allocation keys";
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(240, 6);
            // 
            // m_dropToExcelRightClickMenu
            // 
            this.m_dropToExcelRightClickMenu.Image = global::FBI.Properties.Resources.Excel_dark_24_24;
            this.m_dropToExcelRightClickMenu.Name = "m_dropToExcelRightClickMenu";
            this.m_dropToExcelRightClickMenu.Size = new System.Drawing.Size(243, 30);
            this.m_dropToExcelRightClickMenu.Text = "Drop accounts on Excel";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.DropHierarchyToExcelToolStripMenuItem1,
            this.HelpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(314, 28);
            this.MainMenu.TabIndex = 25;
            this.MainMenu.Text = "MenuStrip1";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateANewAccountToolStripMenuItem,
            this.CreateANewCategoryToolStripMenuItem,
            this.ToolStripSeparator2,
            this.DeleteAccountToolStripMenuItem1});
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(136, 24);
            this.NewToolStripMenuItem.Text = "[general.account]";
            // 
            // CreateANewAccountToolStripMenuItem
            // 
            this.CreateANewAccountToolStripMenuItem.Image = global::FBI.Properties.Resources.Financial_BI_dark_blue_add;
            this.CreateANewAccountToolStripMenuItem.Name = "CreateANewAccountToolStripMenuItem";
            this.CreateANewAccountToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.CreateANewAccountToolStripMenuItem.Text = "[accounts.new_account]";
            // 
            // CreateANewCategoryToolStripMenuItem
            // 
            this.CreateANewCategoryToolStripMenuItem.Image = global::FBI.Properties.Resources.favicon_81_;
            this.CreateANewCategoryToolStripMenuItem.Name = "CreateANewCategoryToolStripMenuItem";
            this.CreateANewCategoryToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
            this.CreateANewCategoryToolStripMenuItem.Text = "[accounts.new_tab_account]";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(265, 6);
            // 
            // DeleteAccountToolStripMenuItem1
            // 
            this.DeleteAccountToolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DeleteAccountToolStripMenuItem1.Image = global::FBI.Properties.Resources.imageres_891;
            this.DeleteAccountToolStripMenuItem1.Name = "DeleteAccountToolStripMenuItem1";
            this.DeleteAccountToolStripMenuItem1.Size = new System.Drawing.Size(268, 26);
            this.DeleteAccountToolStripMenuItem1.Text = "[accounts.delete_account]";
            // 
            // DropHierarchyToExcelToolStripMenuItem1
            // 
            this.DropHierarchyToExcelToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem,
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem});
            this.DropHierarchyToExcelToolStripMenuItem1.Name = "DropHierarchyToExcelToolStripMenuItem1";
            this.DropHierarchyToExcelToolStripMenuItem1.Size = new System.Drawing.Size(55, 24);
            this.DropHierarchyToExcelToolStripMenuItem1.Text = "Excel";
            // 
            // DropAllAccountsHierarchyToExcelToolStripMenuItem
            // 
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.Excel_dark_24_24;
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Name = "DropAllAccountsHierarchyToExcelToolStripMenuItem";
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Size = new System.Drawing.Size(374, 26);
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = "[accounts.drop_to_excel]";
            this.DropAllAccountsHierarchyToExcelToolStripMenuItem.Click += new System.EventHandler(this.OnDropAllAccountOnExcel);
            // 
            // DropSelectedAccountHierarchyToExcelToolStripMenuItem
            // 
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.Excel_Green_32x32;
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Name = "DropSelectedAccountHierarchyToExcelToolStripMenuItem";
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Size = new System.Drawing.Size(374, 26);
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = "[accounts.drop_selected_hierarchy_to_excel]";
            this.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Click += new System.EventHandler(this.OnDropSelectedAccountToExcel);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
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
            // AccountsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.SplitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AccountsView";
            this.Size = new System.Drawing.Size(1308, 852);
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
            this.GlobalFactsPanel.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.TVRCM.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    public System.Windows.Forms.SplitContainer SplitContainer1;
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
    public System.Windows.Forms.GroupBox m_accountInformationGroupbox;
    public VIBlend.WinForms.Controls.vTextBox Name_TB;
    public System.Windows.Forms.GroupBox m_accountFormulaGroupbox;
    public System.Windows.Forms.ImageList accountsIL;
    public System.Windows.Forms.ImageList EditButtonsImagelist;
    public System.Windows.Forms.ContextMenuStrip TVRCM;
    public System.Windows.Forms.ToolStripMenuItem AddSubAccountToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem AddCategoryToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem DeleteAccountToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem m_dropToExcelRightClickMenu;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    public System.Windows.Forms.MenuStrip MainMenu;
    public System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem CreateANewAccountToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem CreateANewCategoryToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem DeleteAccountToolStripMenuItem1;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    public System.Windows.Forms.ToolStripMenuItem DropHierarchyToExcelToolStripMenuItem1;
    public System.Windows.Forms.ToolStripMenuItem DropAllAccountsHierarchyToExcelToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem DropSelectedAccountHierarchyToExcelToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
    public System.Windows.Forms.GroupBox m_accountDescriptionGroupbox;
    public VIBlend.WinForms.Controls.vComboBox FormulaTypeCB;
    public VIBlend.WinForms.Controls.vComboBox TypeCB;
    public VIBlend.WinForms.Controls.vLabel m_accountConsolidationOptionLabel;
    public VIBlend.WinForms.Controls.vLabel m_accountCurrenciesConversionLabel;
    public VIBlend.WinForms.Controls.vLabel m_accountNameLabel;
    public VIBlend.WinForms.Controls.vLabel m_accountFormulaTypeLabel;
    public VIBlend.WinForms.Controls.vLabel m_accountTypeLabel;
    public VIBlend.WinForms.Controls.vComboBox ConsolidationOptionCB;
    public VIBlend.WinForms.Controls.vComboBox CurrencyCB;
    public System.Windows.Forms.Panel GlobalFactsPanel;
    public VIBlend.WinForms.Controls.vLabel m_globalFactsLabel;
    public System.Windows.Forms.SplitContainer SplitContainer2;
    public VIBlend.WinForms.Controls.vTextBox m_descriptionTextBox;
    public VIBlend.WinForms.Controls.vRichTextBox m_formulaTextBox;
    public System.Windows.Forms.Panel AccountsTVPanel;
    public System.Windows.Forms.ImageList m_globalFactsImageList;
    public VIBlend.WinForms.Controls.vButton m_validateFormulaButton;
    public VIBlend.WinForms.Controls.vButton SaveDescriptionBT;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
    public System.Windows.Forms.ToolStripMenuItem m_allocationKeyButton;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    public VIBlend.WinForms.Controls.vButton m_formulaEditionButton;

    public VIBlend.WinForms.Controls.vButton m_cancelFormulaEditionButton;
    public VIBlend.WinForms.Controls.vComboBox ProcessCB;
    public VIBlend.WinForms.Controls.vLabel m_ProcessLabel;
    public VIBlend.WinForms.Controls.vComboBox m_formatCB;
    public VIBlend.WinForms.Controls.vLabel m_formatLabel;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private VIBlend.WinForms.Controls.vButton m_expandRightBT;
  }
}