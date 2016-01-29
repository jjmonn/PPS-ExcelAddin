using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class NewAccountUI : System.Windows.Forms.Form
  {

    //Form overrides dispose to clean up the component list.
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAccountUI));
      this.CancelBT = new VIBlend.WinForms.Controls.vButton();
      this.ButtonsIL = new System.Windows.Forms.ImageList(this.components);
      this.CreateAccountBT = new VIBlend.WinForms.Controls.vButton();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.accountsIL = new System.Windows.Forms.ImageList(this.components);
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_accountNameLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountParentLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_formulaTypeLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_formatLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_consolidationOptionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_conversionOptionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.ParentTVPanel = new System.Windows.Forms.Panel();
      this.NameTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.FormulaComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.TypeComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.GroupBox1 = new VIBlend.WinForms.Controls.vGroupBox();
      this.m_nonRadioButton = new VIBlend.WinForms.Controls.vRadioButton();
      this.m_recomputeRadioButton = new VIBlend.WinForms.Controls.vRadioButton();
      this.m_aggregationRadioButton = new VIBlend.WinForms.Controls.vRadioButton();
      this.GroupBox2 = new VIBlend.WinForms.Controls.vGroupBox();
      this.m_endOfPeriodRadioButton = new VIBlend.WinForms.Controls.vRadioButton();
      this.m_averageRateRadioButton = new VIBlend.WinForms.Controls.vRadioButton();
      this.TableLayoutPanel1.SuspendLayout();
      this.GroupBox1.SuspendLayout();
      this.GroupBox2.SuspendLayout();
      this.SuspendLayout();
      //
      //CancelBT
      //
      this.CancelBT.AllowAnimations = true;
      this.CancelBT.BackColor = System.Drawing.Color.Transparent;
      this.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CancelBT.ImageKey = "delete-2-xxl.png";
      this.CancelBT.ImageList = this.ButtonsIL;
      this.CancelBT.Location = new System.Drawing.Point(573, 278);
      this.CancelBT.Name = "CancelBT";
      this.CancelBT.RoundedCornersMask = Convert.ToByte(15);
      this.CancelBT.Size = new System.Drawing.Size(100, 33);
      this.CancelBT.TabIndex = 10;
      this.CancelBT.Text = "Cancel";
      this.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CancelBT.UseVisualStyleBackColor = true;
      this.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //ButtonsIL
      //
      this.ButtonsIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsIL.ImageStream");
      this.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsIL.Images.SetKeyName(0, "delete-2-xxl.png");
      this.ButtonsIL.Images.SetKeyName(1, "favicon(97).ico");
      this.ButtonsIL.Images.SetKeyName(2, "favicon(76).ico");
      this.ButtonsIL.Images.SetKeyName(3, "favicon(7).ico");
      //
      //CreateAccountBT
      //
      this.CreateAccountBT.AllowAnimations = true;
      this.CreateAccountBT.BackColor = System.Drawing.Color.Transparent;
      this.CreateAccountBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CreateAccountBT.ImageKey = "1420498403_340208.ico";
      this.CreateAccountBT.ImageList = this.ButtonIcons;
      this.CreateAccountBT.Location = new System.Drawing.Point(455, 278);
      this.CreateAccountBT.Name = "CreateAccountBT";
      this.CreateAccountBT.RoundedCornersMask = Convert.ToByte(15);
      this.CreateAccountBT.Size = new System.Drawing.Size(100, 33);
      this.CreateAccountBT.TabIndex = 9;
      this.CreateAccountBT.Text = "Create";
      this.CreateAccountBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CreateAccountBT.UseVisualStyleBackColor = true;
      this.CreateAccountBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //ButtonIcons
      //
      this.ButtonIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonIcons.ImageStream");
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(1, "1420498403_340208.ico");
      //
      //accountsIL
      //
      this.accountsIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("accountsIL.ImageStream");
      this.accountsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.accountsIL.Images.SetKeyName(0, "favicon(217).ico");
      this.accountsIL.Images.SetKeyName(1, "entity icon 3.jpg");
      this.accountsIL.Images.SetKeyName(2, "imageres_9.ico");
      this.accountsIL.Images.SetKeyName(3, "imageres_148.ico");
      this.accountsIL.Images.SetKeyName(4, "imageres_10.ico");
      this.accountsIL.Images.SetKeyName(5, "imageres_1013.ico");
      this.accountsIL.Images.SetKeyName(6, "imageres_100.ico");
      this.accountsIL.Images.SetKeyName(7, "star1.jpg");
      this.accountsIL.Images.SetKeyName(8, "imageres_190.ico");
      this.accountsIL.Images.SetKeyName(9, "imageres_81.ico");
      //
      //TableLayoutPanel1
      //
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.48015f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.51984f));
      this.TableLayoutPanel1.Controls.Add(this.m_accountNameLabel, 0, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_accountParentLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_formulaTypeLabel, 0, 2);
      this.TableLayoutPanel1.Controls.Add(this.m_formatLabel, 0, 3);
      this.TableLayoutPanel1.Controls.Add(this.m_consolidationOptionLabel, 0, 4);
      this.TableLayoutPanel1.Controls.Add(this.m_conversionOptionLabel, 0, 5);
      this.TableLayoutPanel1.Controls.Add(this.ParentTVPanel, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.NameTextBox, 1, 1);
      this.TableLayoutPanel1.Controls.Add(this.FormulaComboBox, 1, 2);
      this.TableLayoutPanel1.Controls.Add(this.TypeComboBox, 1, 3);
      this.TableLayoutPanel1.Controls.Add(this.GroupBox1, 1, 4);
      this.TableLayoutPanel1.Controls.Add(this.GroupBox2, 1, 5);
      this.TableLayoutPanel1.Location = new System.Drawing.Point(13, 19);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 6;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(660, 241);
      this.TableLayoutPanel1.TabIndex = 36;
      //
      //m_accountNameLabel
      //
      this.m_accountNameLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountNameLabel.Ellipsis = false;
      this.m_accountNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountNameLabel.Location = new System.Drawing.Point(3, 43);
      this.m_accountNameLabel.Multiline = true;
      this.m_accountNameLabel.Name = "m_accountNameLabel";
      this.m_accountNameLabel.Size = new System.Drawing.Size(85, 15);
      this.m_accountNameLabel.TabIndex = 31;
      this.m_accountNameLabel.Text = "Account name";
      this.m_accountNameLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountNameLabel.UseMnemonics = true;
      this.m_accountNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_accountParentLabel
      //
      this.m_accountParentLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountParentLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountParentLabel.Ellipsis = false;
      this.m_accountParentLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountParentLabel.Location = new System.Drawing.Point(3, 3);
      this.m_accountParentLabel.Multiline = true;
      this.m_accountParentLabel.Name = "m_accountParentLabel";
      this.m_accountParentLabel.Size = new System.Drawing.Size(88, 15);
      this.m_accountParentLabel.TabIndex = 30;
      this.m_accountParentLabel.Text = "Account parent";
      this.m_accountParentLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountParentLabel.UseMnemonics = true;
      this.m_accountParentLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_formulaTypeLabel
      //
      this.m_formulaTypeLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_formulaTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_formulaTypeLabel.Ellipsis = false;
      this.m_formulaTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_formulaTypeLabel.Location = new System.Drawing.Point(3, 83);
      this.m_formulaTypeLabel.Multiline = true;
      this.m_formulaTypeLabel.Name = "m_formulaTypeLabel";
      this.m_formulaTypeLabel.Size = new System.Drawing.Size(78, 15);
      this.m_formulaTypeLabel.TabIndex = 32;
      this.m_formulaTypeLabel.Text = "Formula type";
      this.m_formulaTypeLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_formulaTypeLabel.UseMnemonics = true;
      this.m_formulaTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_formatLabel
      //
      this.m_formatLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_formatLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_formatLabel.Ellipsis = false;
      this.m_formatLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_formatLabel.Location = new System.Drawing.Point(3, 123);
      this.m_formatLabel.Multiline = true;
      this.m_formatLabel.Name = "m_formatLabel";
      this.m_formatLabel.Size = new System.Drawing.Size(88, 15);
      this.m_formatLabel.TabIndex = 33;
      this.m_formatLabel.Text = "Account format";
      this.m_formatLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_formatLabel.UseMnemonics = true;
      this.m_formatLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_consolidationOptionLabel
      //
      this.m_consolidationOptionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_consolidationOptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_consolidationOptionLabel.Ellipsis = false;
      this.m_consolidationOptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_consolidationOptionLabel.Location = new System.Drawing.Point(3, 163);
      this.m_consolidationOptionLabel.Multiline = true;
      this.m_consolidationOptionLabel.Name = "m_consolidationOptionLabel";
      this.m_consolidationOptionLabel.Size = new System.Drawing.Size(119, 15);
      this.m_consolidationOptionLabel.TabIndex = 34;
      this.m_consolidationOptionLabel.Text = "Consolidation option";
      this.m_consolidationOptionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_consolidationOptionLabel.UseMnemonics = true;
      this.m_consolidationOptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_conversionOptionLabel
      //
      this.m_conversionOptionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_conversionOptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_conversionOptionLabel.Ellipsis = false;
      this.m_conversionOptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_conversionOptionLabel.Location = new System.Drawing.Point(3, 203);
      this.m_conversionOptionLabel.Multiline = true;
      this.m_conversionOptionLabel.Name = "m_conversionOptionLabel";
      this.m_conversionOptionLabel.Size = new System.Drawing.Size(128, 15);
      this.m_conversionOptionLabel.TabIndex = 35;
      this.m_conversionOptionLabel.Text = "Currencies conversion";
      this.m_conversionOptionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_conversionOptionLabel.UseMnemonics = true;
      this.m_conversionOptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //ParentTVPanel
      //
      this.ParentTVPanel.Location = new System.Drawing.Point(171, 3);
      this.ParentTVPanel.Name = "ParentTVPanel";
      this.ParentTVPanel.Size = new System.Drawing.Size(486, 26);
      this.ParentTVPanel.TabIndex = 14;
      //
      //NameTextBox
      //
      this.NameTextBox.BackColor = System.Drawing.Color.White;
      this.NameTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.NameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
      this.NameTextBox.DefaultText = "Empty...";
      this.NameTextBox.Location = new System.Drawing.Point(171, 43);
      this.NameTextBox.MaxLength = 32767;
      this.NameTextBox.Name = "NameTextBox";
      this.NameTextBox.PasswordChar = '\0';
      this.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.NameTextBox.SelectionLength = 0;
      this.NameTextBox.SelectionStart = 0;
      this.NameTextBox.Size = new System.Drawing.Size(486, 26);
      this.NameTextBox.TabIndex = 1;
      this.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.NameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //FormulaComboBox
      //
      this.FormulaComboBox.BackColor = System.Drawing.Color.White;
      this.FormulaComboBox.DisplayMember = "";
      this.FormulaComboBox.DropDownList = true;
      this.FormulaComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.FormulaComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.FormulaComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.FormulaComboBox.DropDownWidth = 486;
      this.FormulaComboBox.Location = new System.Drawing.Point(171, 83);
      this.FormulaComboBox.Name = "FormulaComboBox";
      this.FormulaComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
      this.FormulaComboBox.Size = new System.Drawing.Size(486, 26);
      this.FormulaComboBox.TabIndex = 2;
      this.FormulaComboBox.Text = " ";
      this.FormulaComboBox.UseThemeBackColor = false;
      this.FormulaComboBox.UseThemeDropDownArrowColor = true;
      this.FormulaComboBox.ValueMember = "";
      this.FormulaComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.FormulaComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //TypeComboBox
      //
      this.TypeComboBox.BackColor = System.Drawing.Color.White;
      this.TypeComboBox.DisplayMember = "";
      this.TypeComboBox.DropDownList = true;
      this.TypeComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.TypeComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.TypeComboBox.DropDownWidth = 486;
      this.TypeComboBox.Location = new System.Drawing.Point(171, 123);
      this.TypeComboBox.Name = "TypeComboBox";
      this.TypeComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
      this.TypeComboBox.Size = new System.Drawing.Size(486, 26);
      this.TypeComboBox.TabIndex = 3;
      this.TypeComboBox.Text = " ";
      this.TypeComboBox.UseThemeBackColor = false;
      this.TypeComboBox.UseThemeDropDownArrowColor = true;
      this.TypeComboBox.ValueMember = "";
      this.TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //GroupBox1
      //
      this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
      this.GroupBox1.Controls.Add(this.m_nonRadioButton);
      this.GroupBox1.Controls.Add(this.m_recomputeRadioButton);
      this.GroupBox1.Controls.Add(this.m_aggregationRadioButton);
      this.GroupBox1.Location = new System.Drawing.Point(168, 160);
      this.GroupBox1.Margin = new System.Windows.Forms.Padding(0);
      this.GroupBox1.Name = "GroupBox1";
      this.GroupBox1.Size = new System.Drawing.Size(489, 40);
      this.GroupBox1.TabIndex = 12;
      this.GroupBox1.TabStop = false;
      this.GroupBox1.UseThemeBorderColor = true;
      this.GroupBox1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_nonRadioButton
      //
      this.m_nonRadioButton.BackColor = System.Drawing.Color.Transparent;
      this.m_nonRadioButton.Flat = true;
      this.m_nonRadioButton.Image = null;
      this.m_nonRadioButton.Location = new System.Drawing.Point(230, 2);
      this.m_nonRadioButton.Name = "m_nonRadioButton";
      this.m_nonRadioButton.Size = new System.Drawing.Size(104, 24);
      this.m_nonRadioButton.TabIndex = 6;
      this.m_nonRadioButton.Text = "None";
      this.m_nonRadioButton.UseVisualStyleBackColor = false;
      this.m_nonRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_recomputeRadioButton
      //
      this.m_recomputeRadioButton.AutoSize = true;
      this.m_recomputeRadioButton.BackColor = System.Drawing.Color.Transparent;
      this.m_recomputeRadioButton.Flat = true;
      this.m_recomputeRadioButton.Image = null;
      this.m_recomputeRadioButton.Location = new System.Drawing.Point(115, 3);
      this.m_recomputeRadioButton.Name = "m_recomputeRadioButton";
      this.m_recomputeRadioButton.Size = new System.Drawing.Size(109, 19);
      this.m_recomputeRadioButton.TabIndex = 5;
      this.m_recomputeRadioButton.Text = "Recomputation";
      this.m_recomputeRadioButton.UseVisualStyleBackColor = true;
      this.m_recomputeRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_aggregationRadioButton
      //
      this.m_aggregationRadioButton.AutoSize = true;
      this.m_aggregationRadioButton.BackColor = System.Drawing.Color.Transparent;
      this.m_aggregationRadioButton.Checked = true;
      this.m_aggregationRadioButton.Flat = true;
      this.m_aggregationRadioButton.Image = null;
      this.m_aggregationRadioButton.Location = new System.Drawing.Point(18, 3);
      this.m_aggregationRadioButton.Name = "m_aggregationRadioButton";
      this.m_aggregationRadioButton.Size = new System.Drawing.Size(91, 19);
      this.m_aggregationRadioButton.TabIndex = 4;
      this.m_aggregationRadioButton.TabStop = true;
      this.m_aggregationRadioButton.Text = "Aggregation";
      this.m_aggregationRadioButton.UseVisualStyleBackColor = true;
      this.m_aggregationRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //GroupBox2
      //
      this.GroupBox2.BackColor = System.Drawing.Color.Transparent;
      this.GroupBox2.Controls.Add(this.m_endOfPeriodRadioButton);
      this.GroupBox2.Controls.Add(this.m_averageRateRadioButton);
      this.GroupBox2.Location = new System.Drawing.Point(168, 200);
      this.GroupBox2.Margin = new System.Windows.Forms.Padding(0);
      this.GroupBox2.Name = "GroupBox2";
      this.GroupBox2.Size = new System.Drawing.Size(489, 40);
      this.GroupBox2.TabIndex = 13;
      this.GroupBox2.TabStop = false;
      this.GroupBox2.UseThemeBorderColor = true;
      this.GroupBox2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_endOfPeriodRadioButton
      //
      this.m_endOfPeriodRadioButton.AutoSize = true;
      this.m_endOfPeriodRadioButton.BackColor = System.Drawing.Color.Transparent;
      this.m_endOfPeriodRadioButton.Flat = true;
      this.m_endOfPeriodRadioButton.Image = null;
      this.m_endOfPeriodRadioButton.Location = new System.Drawing.Point(115, 4);
      this.m_endOfPeriodRadioButton.Name = "m_endOfPeriodRadioButton";
      this.m_endOfPeriodRadioButton.Size = new System.Drawing.Size(122, 19);
      this.m_endOfPeriodRadioButton.TabIndex = 8;
      this.m_endOfPeriodRadioButton.Text = "End of period rate";
      this.m_endOfPeriodRadioButton.UseVisualStyleBackColor = true;
      this.m_endOfPeriodRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_averageRateRadioButton
      //
      this.m_averageRateRadioButton.AutoSize = true;
      this.m_averageRateRadioButton.BackColor = System.Drawing.Color.Transparent;
      this.m_averageRateRadioButton.Checked = true;
      this.m_averageRateRadioButton.Flat = true;
      this.m_averageRateRadioButton.Image = null;
      this.m_averageRateRadioButton.Location = new System.Drawing.Point(18, 4);
      this.m_averageRateRadioButton.Name = "m_averageRateRadioButton";
      this.m_averageRateRadioButton.Size = new System.Drawing.Size(93, 19);
      this.m_averageRateRadioButton.TabIndex = 6;
      this.m_averageRateRadioButton.TabStop = true;
      this.m_averageRateRadioButton.Text = "Average rate";
      this.m_averageRateRadioButton.UseVisualStyleBackColor = true;
      this.m_averageRateRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //NewAccountUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(684, 327);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.CreateAccountBT);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "NewAccountUI";
      this.Text = "New account";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.GroupBox1.ResumeLayout(false);
      this.GroupBox1.PerformLayout();
      this.GroupBox2.ResumeLayout(false);
      this.GroupBox2.PerformLayout();
      this.ResumeLayout(false);

    }
    internal VIBlend.WinForms.Controls.vButton CancelBT;
    internal VIBlend.WinForms.Controls.vButton CreateAccountBT;
    internal System.Windows.Forms.ImageList ButtonIcons;
    internal System.Windows.Forms.ImageList ButtonsIL;
    internal System.Windows.Forms.ImageList accountsIL;
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    internal VIBlend.WinForms.Controls.vLabel m_accountNameLabel;
    internal VIBlend.WinForms.Controls.vLabel m_accountParentLabel;
    internal VIBlend.WinForms.Controls.vLabel m_formulaTypeLabel;
    internal VIBlend.WinForms.Controls.vLabel m_formatLabel;
    internal VIBlend.WinForms.Controls.vGroupBox GroupBox1;
    internal VIBlend.WinForms.Controls.vRadioButton m_recomputeRadioButton;
    internal VIBlend.WinForms.Controls.vRadioButton m_aggregationRadioButton;
    internal VIBlend.WinForms.Controls.vLabel m_consolidationOptionLabel;
    internal VIBlend.WinForms.Controls.vGroupBox GroupBox2;
    internal VIBlend.WinForms.Controls.vRadioButton m_endOfPeriodRadioButton;
    internal VIBlend.WinForms.Controls.vRadioButton m_averageRateRadioButton;
    internal VIBlend.WinForms.Controls.vLabel m_conversionOptionLabel;
    internal System.Windows.Forms.Panel ParentTVPanel;
    internal VIBlend.WinForms.Controls.vTextBox NameTextBox;
    internal VIBlend.WinForms.Controls.vComboBox FormulaComboBox;
    internal VIBlend.WinForms.Controls.vComboBox TypeComboBox;
    internal VIBlend.WinForms.Controls.vRadioButton m_nonRadioButton;
  }
}