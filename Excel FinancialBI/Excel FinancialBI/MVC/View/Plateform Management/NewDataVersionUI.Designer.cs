using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class NewDataVersionUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDataVersionUI));
      this.m_factVersionLabel = new System.Windows.Forms.Label();
      this.m_rateVersionLabel = new System.Windows.Forms.Label();
      this.m_nbPeriodsLabel = new System.Windows.Forms.Label();
      this.m_startingPeriodLabel = new System.Windows.Forms.Label();
      this.m_periodConfigLabel = new System.Windows.Forms.Label();
      this.m_versionNameLabel = new System.Windows.Forms.Label();
      this.NameTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_timeConfigCB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_exchangeRatesVersionVTreeviewbox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_factsVersionVTreeviewbox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_parentVersionsTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_copyCheckBox = new VIBlend.WinForms.Controls.vCheckBox();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.BigIcons = new System.Windows.Forms.ImageList(this.components);
      this.CancelBT = new VIBlend.WinForms.Controls.vButton();
      this.CreateVersionBT = new VIBlend.WinForms.Controls.vButton();
      this.NbPeriodsNUD = new VIBlend.WinForms.Controls.vNumericUpDown();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_startingPeriodDatePicker = new VIBlend.WinForms.Controls.vDatePicker();
      this.SuspendLayout();
      //
      //m_factVersionLabel
      //
      this.m_factVersionLabel.AutoSize = true;
      this.m_factVersionLabel.Location = new System.Drawing.Point(34, 297);
      this.m_factVersionLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_factVersionLabel.Name = "m_factVersionLabel";
      this.m_factVersionLabel.Size = new System.Drawing.Size(142, 13);
      this.m_factVersionLabel.TabIndex = 30;
      this.m_factVersionLabel.Text = "[facts_versions.fact_version]";
      //
      //m_rateVersionLabel
      //
      this.m_rateVersionLabel.AutoSize = true;
      this.m_rateVersionLabel.Location = new System.Drawing.Point(34, 259);
      this.m_rateVersionLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_rateVersionLabel.Name = "m_rateVersionLabel";
      this.m_rateVersionLabel.Size = new System.Drawing.Size(200, 13);
      this.m_rateVersionLabel.TabIndex = 28;
      this.m_rateVersionLabel.Text = "[facts_versions.exchange_rates_version]";
      //
      //m_nbPeriodsLabel
      //
      this.m_nbPeriodsLabel.AutoSize = true;
      this.m_nbPeriodsLabel.Location = new System.Drawing.Point(34, 217);
      this.m_nbPeriodsLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_nbPeriodsLabel.Name = "m_nbPeriodsLabel";
      this.m_nbPeriodsLabel.Size = new System.Drawing.Size(56, 13);
      this.m_nbPeriodsLabel.TabIndex = 25;
      this.m_nbPeriodsLabel.Text = "Number of";
      //
      //m_startingPeriodLabel
      //
      this.m_startingPeriodLabel.AutoSize = true;
      this.m_startingPeriodLabel.Location = new System.Drawing.Point(34, 172);
      this.m_startingPeriodLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_startingPeriodLabel.Name = "m_startingPeriodLabel";
      this.m_startingPeriodLabel.Size = new System.Drawing.Size(153, 13);
      this.m_startingPeriodLabel.TabIndex = 17;
      this.m_startingPeriodLabel.Text = "[facts_versions.starting_period]";
      //
      //m_periodConfigLabel
      //
      this.m_periodConfigLabel.AutoSize = true;
      this.m_periodConfigLabel.Location = new System.Drawing.Point(34, 125);
      this.m_periodConfigLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_periodConfigLabel.Name = "m_periodConfigLabel";
      this.m_periodConfigLabel.Size = new System.Drawing.Size(148, 13);
      this.m_periodConfigLabel.TabIndex = 15;
      this.m_periodConfigLabel.Text = "[facts_versions.period_config]";
      //
      //m_versionNameLabel
      //
      this.m_versionNameLabel.AutoSize = true;
      this.m_versionNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
      this.m_versionNameLabel.Location = new System.Drawing.Point(34, 36);
      this.m_versionNameLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_versionNameLabel.Name = "m_versionNameLabel";
      this.m_versionNameLabel.Size = new System.Drawing.Size(179, 13);
      this.m_versionNameLabel.TabIndex = 7;
      this.m_versionNameLabel.Text = "[facts_versions.version_name]";
      //
      //NameTB
      //
      this.NameTB.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
      this.NameTB.BackColor = System.Drawing.Color.White;
      this.NameTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.NameTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
      this.NameTB.DefaultText = "Empty...";
      this.NameTB.Location = new System.Drawing.Point(347, 36);
      this.NameTB.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.NameTB.MaximumSize = new System.Drawing.Size(400, 4);
      this.NameTB.MaxLength = 32767;
      this.NameTB.MinimumSize = new System.Drawing.Size(280, 20);
      this.NameTB.Name = "NameTB";
      this.NameTB.PasswordChar = '\0';
      this.NameTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.NameTB.SelectionLength = 0;
      this.NameTB.SelectionStart = 0;
      this.NameTB.Size = new System.Drawing.Size(280, 20);
      this.NameTB.TabIndex = 13;
      this.NameTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.NameTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_timeConfigCB
      //
      this.m_timeConfigCB.BackColor = System.Drawing.Color.White;
      this.m_timeConfigCB.DisplayMember = "";
      this.m_timeConfigCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_timeConfigCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_timeConfigCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_timeConfigCB.DropDownWidth = 280;
      this.m_timeConfigCB.Location = new System.Drawing.Point(347, 123);
      this.m_timeConfigCB.Name = "m_timeConfigCB";
      this.m_timeConfigCB.RoundedCornersMaskListItem = Convert.ToByte(15);
      this.m_timeConfigCB.Size = new System.Drawing.Size(280, 21);
      this.m_timeConfigCB.TabIndex = 16;
      this.m_timeConfigCB.UseThemeBackColor = false;
      this.m_timeConfigCB.UseThemeDropDownArrowColor = true;
      this.m_timeConfigCB.ValueMember = "";
      this.m_timeConfigCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_timeConfigCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_exchangeRatesVersionVTreeviewbox
      //
      this.m_exchangeRatesVersionVTreeviewbox.BackColor = System.Drawing.Color.White;
      this.m_exchangeRatesVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black;
      this.m_exchangeRatesVersionVTreeviewbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_exchangeRatesVersionVTreeviewbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_exchangeRatesVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_exchangeRatesVersionVTreeviewbox.Location = new System.Drawing.Point(347, 259);
      this.m_exchangeRatesVersionVTreeviewbox.Name = "m_exchangeRatesVersionVTreeviewbox";
      this.m_exchangeRatesVersionVTreeviewbox.Size = new System.Drawing.Size(280, 23);
      this.m_exchangeRatesVersionVTreeviewbox.TabIndex = 31;
      this.m_exchangeRatesVersionVTreeviewbox.UseThemeBackColor = false;
      this.m_exchangeRatesVersionVTreeviewbox.UseThemeDropDownArrowColor = true;
      this.m_exchangeRatesVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_factsVersionVTreeviewbox
      //
      this.m_factsVersionVTreeviewbox.BackColor = System.Drawing.Color.White;
      this.m_factsVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black;
      this.m_factsVersionVTreeviewbox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_factsVersionVTreeviewbox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_factsVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_factsVersionVTreeviewbox.Location = new System.Drawing.Point(347, 295);
      this.m_factsVersionVTreeviewbox.Name = "m_factsVersionVTreeviewbox";
      this.m_factsVersionVTreeviewbox.Size = new System.Drawing.Size(280, 23);
      this.m_factsVersionVTreeviewbox.TabIndex = 32;
      this.m_factsVersionVTreeviewbox.UseThemeBackColor = false;
      this.m_factsVersionVTreeviewbox.UseThemeDropDownArrowColor = true;
      this.m_factsVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_parentVersionsTreeviewBox
      //
      this.m_parentVersionsTreeviewBox.BackColor = System.Drawing.Color.White;
      this.m_parentVersionsTreeviewBox.BorderColor = System.Drawing.Color.Black;
      this.m_parentVersionsTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_parentVersionsTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_parentVersionsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_parentVersionsTreeviewBox.Location = new System.Drawing.Point(347, 78);
      this.m_parentVersionsTreeviewBox.Name = "m_parentVersionsTreeviewBox";
      this.m_parentVersionsTreeviewBox.Size = new System.Drawing.Size(280, 23);
      this.m_parentVersionsTreeviewBox.TabIndex = 33;
      this.m_parentVersionsTreeviewBox.Text = "VTreeViewBox1";
      this.m_parentVersionsTreeviewBox.UseThemeBackColor = false;
      this.m_parentVersionsTreeviewBox.UseThemeDropDownArrowColor = true;
      this.m_parentVersionsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_copyCheckBox
      //
      this.m_copyCheckBox.BackColor = System.Drawing.Color.Transparent;
      this.m_copyCheckBox.Location = new System.Drawing.Point(37, 76);
      this.m_copyCheckBox.Name = "m_copyCheckBox";
      this.m_copyCheckBox.Size = new System.Drawing.Size(175, 24);
      this.m_copyCheckBox.TabIndex = 34;
      this.m_copyCheckBox.Text = "Create copy from";
      this.m_copyCheckBox.UseVisualStyleBackColor = false;
      this.m_copyCheckBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //ButtonIcons
      //
      this.ButtonIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonIcons.ImageStream");
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "favicon(81) (1).ico");
      this.ButtonIcons.Images.SetKeyName(1, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(2, "1420498403_340208.ico");
      //
      //BigIcons
      //
      this.BigIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("BigIcons.ImageStream");
      this.BigIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.BigIcons.Images.SetKeyName(0, "favicon(230).ico");
      //
      //CancelBT
      //
      this.CancelBT.AllowAnimations = true;
      this.CancelBT.BackColor = System.Drawing.Color.Transparent;
      this.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CancelBT.ImageKey = "imageres_89.ico";
      this.CancelBT.ImageList = this.ButtonIcons;
      this.CancelBT.Location = new System.Drawing.Point(348, 349);
      this.CancelBT.Name = "CancelBT";
      this.CancelBT.RoundedCornersMask = Convert.ToByte(15);
      this.CancelBT.Size = new System.Drawing.Size(119, 30);
      this.CancelBT.TabIndex = 23;
      this.CancelBT.Text = "[general.cancel]";
      this.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CancelBT.UseVisualStyleBackColor = true;
      this.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //CreateVersionBT
      //
      this.CreateVersionBT.AllowAnimations = true;
      this.CreateVersionBT.BackColor = System.Drawing.Color.Transparent;
      this.CreateVersionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CreateVersionBT.ImageKey = "1420498403_340208.ico";
      this.CreateVersionBT.ImageList = this.ButtonIcons;
      this.CreateVersionBT.Location = new System.Drawing.Point(508, 349);
      this.CreateVersionBT.Name = "CreateVersionBT";
      this.CreateVersionBT.RoundedCornersMask = Convert.ToByte(15);
      this.CreateVersionBT.Size = new System.Drawing.Size(119, 30);
      this.CreateVersionBT.TabIndex = 22;
      this.CreateVersionBT.Text = "[general.create]";
      this.CreateVersionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CreateVersionBT.UseVisualStyleBackColor = true;
      this.CreateVersionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //NbPeriodsNUD
      //
      this.NbPeriodsNUD.BackColor = System.Drawing.Color.White;
      this.NbPeriodsNUD.DropDownArrowBackgroundEnabled = true;
      this.NbPeriodsNUD.EnableBorderHighlight = false;
      this.NbPeriodsNUD.Location = new System.Drawing.Point(348, 217);
      this.NbPeriodsNUD.MaxLength = 32767;
      this.NbPeriodsNUD.Name = "NbPeriodsNUD";
      this.NbPeriodsNUD.OverrideBackColor = System.Drawing.Color.White;
      this.NbPeriodsNUD.OverrideBorderColor = System.Drawing.Color.Gray;
      this.NbPeriodsNUD.OverrideFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
      this.NbPeriodsNUD.OverrideForeColor = System.Drawing.Color.Black;
      this.NbPeriodsNUD.Size = new System.Drawing.Size(279, 22);
      this.NbPeriodsNUD.TabIndex = 37;
      this.NbPeriodsNUD.UseThemeForeColor = true;
      this.NbPeriodsNUD.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_versionsTreeviewImageList
      //
      this.m_versionsTreeviewImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("m_versionsTreeviewImageList.ImageStream");
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      //
      //m_startingPeriodDatePicker
      //
      this.m_startingPeriodDatePicker.BackColor = System.Drawing.Color.White;
      this.m_startingPeriodDatePicker.BorderColor = System.Drawing.Color.Black;
      this.m_startingPeriodDatePicker.Culture = new System.Globalization.CultureInfo("");
      this.m_startingPeriodDatePicker.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_startingPeriodDatePicker.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_startingPeriodDatePicker.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
      this.m_startingPeriodDatePicker.FormatValue = "dd MMM yyyy";
      this.m_startingPeriodDatePicker.Location = new System.Drawing.Point(347, 172);
      this.m_startingPeriodDatePicker.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
      this.m_startingPeriodDatePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
      this.m_startingPeriodDatePicker.Name = "m_startingPeriodDatePicker";
      this.m_startingPeriodDatePicker.ShowGrip = false;
      this.m_startingPeriodDatePicker.Size = new System.Drawing.Size(280, 23);
      this.m_startingPeriodDatePicker.TabIndex = 39;
      this.m_startingPeriodDatePicker.Text = "VDatePicker1";
      this.m_startingPeriodDatePicker.UseThemeBackColor = false;
      this.m_startingPeriodDatePicker.UseThemeDropDownArrowColor = true;
      this.m_startingPeriodDatePicker.Value = new System.DateTime(2015, 12, 11, 9, 57, 35, 808);
      this.m_startingPeriodDatePicker.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //NewDataVersionUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(672, 439);
      this.Controls.Add(this.m_startingPeriodDatePicker);
      this.Controls.Add(this.NbPeriodsNUD);
      this.Controls.Add(this.m_versionNameLabel);
      this.Controls.Add(this.NameTB);
      this.Controls.Add(this.m_factVersionLabel);
      this.Controls.Add(this.m_copyCheckBox);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.m_parentVersionsTreeviewBox);
      this.Controls.Add(this.m_rateVersionLabel);
      this.Controls.Add(this.m_periodConfigLabel);
      this.Controls.Add(this.CreateVersionBT);
      this.Controls.Add(this.m_timeConfigCB);
      this.Controls.Add(this.m_nbPeriodsLabel);
      this.Controls.Add(this.m_startingPeriodLabel);
      this.Controls.Add(this.m_factsVersionVTreeviewbox);
      this.Controls.Add(this.m_exchangeRatesVersionVTreeviewbox);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "NewDataVersionUI";
      this.Text = "[facts_versions.version_new]";
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    internal System.Windows.Forms.Label m_startingPeriodLabel;
    internal System.Windows.Forms.Label m_periodConfigLabel;
    internal System.Windows.Forms.Label m_versionNameLabel;
    internal VIBlend.WinForms.Controls.vTextBox NameTB;
    internal VIBlend.WinForms.Controls.vComboBox m_timeConfigCB;
    internal VIBlend.WinForms.Controls.vButton CancelBT;
    internal VIBlend.WinForms.Controls.vButton CreateVersionBT;
    internal System.Windows.Forms.ImageList ButtonIcons;
    internal System.Windows.Forms.ImageList BigIcons;
    internal System.Windows.Forms.Label m_nbPeriodsLabel;
    internal System.Windows.Forms.Label m_rateVersionLabel;
    internal System.Windows.Forms.Label m_factVersionLabel;
    internal VIBlend.WinForms.Controls.vTreeViewBox m_exchangeRatesVersionVTreeviewbox;
    internal VIBlend.WinForms.Controls.vTreeViewBox m_factsVersionVTreeviewbox;
    internal VIBlend.WinForms.Controls.vTreeViewBox m_parentVersionsTreeviewBox;
    internal VIBlend.WinForms.Controls.vCheckBox m_copyCheckBox;
    internal VIBlend.WinForms.Controls.vNumericUpDown NbPeriodsNUD;
    internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    internal VIBlend.WinForms.Controls.vDatePicker m_startingPeriodDatePicker;
  }
}