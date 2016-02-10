using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class ExcelExchangeRatesImportUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelExchangeRatesImportUI));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_ratesRangeTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.periods_edit_BT = new System.Windows.Forms.Button();
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.rates_edit_BT = new System.Windows.Forms.Button();
      this.m_periodsRangeTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_periodsLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_ratesLabel = new VIBlend.WinForms.Controls.vLabel();
      this.import_BT = new System.Windows.Forms.Button();
      this.m_currencyComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_currencyLabel = new VIBlend.WinForms.Controls.vLabel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      //
      //TableLayoutPanel1
      //
      this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46f));
      this.TableLayoutPanel1.Controls.Add(this.m_ratesRangeTextBox, 1, 1);
      this.TableLayoutPanel1.Controls.Add(this.periods_edit_BT, 2, 0);
      this.TableLayoutPanel1.Controls.Add(this.rates_edit_BT, 2, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsRangeTextBox, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_ratesLabel, 0, 1);
      this.TableLayoutPanel1.Location = new System.Drawing.Point(12, 67);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(399, 65);
      this.TableLayoutPanel1.TabIndex = 8;
      //
      //m_ratesRangeTextBox
      //
      this.m_ratesRangeTextBox.BackColor = System.Drawing.Color.White;
      this.m_ratesRangeTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_ratesRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
      this.m_ratesRangeTextBox.DefaultText = "Empty...";
      this.m_ratesRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_ratesRangeTextBox.Location = new System.Drawing.Point(124, 35);
      this.m_ratesRangeTextBox.MaxLength = 32767;
      this.m_ratesRangeTextBox.Name = "m_ratesRangeTextBox";
      this.m_ratesRangeTextBox.PasswordChar = '\0';
      this.m_ratesRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_ratesRangeTextBox.SelectionLength = 0;
      this.m_ratesRangeTextBox.SelectionStart = 0;
      this.m_ratesRangeTextBox.Size = new System.Drawing.Size(225, 27);
      this.m_ratesRangeTextBox.TabIndex = 10;
      this.m_ratesRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_ratesRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //periods_edit_BT
      //
      this.periods_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.periods_edit_BT.ImageIndex = 0;
      this.periods_edit_BT.ImageList = this.ButtonsImageList;
      this.periods_edit_BT.Location = new System.Drawing.Point(354, 2);
      this.periods_edit_BT.Margin = new System.Windows.Forms.Padding(2);
      this.periods_edit_BT.Name = "periods_edit_BT";
      this.periods_edit_BT.Size = new System.Drawing.Size(27, 27);
      this.periods_edit_BT.TabIndex = 2;
      this.periods_edit_BT.UseVisualStyleBackColor = true;
      //
      //ButtonsImageList
      //
      this.ButtonsImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsImageList.ImageStream");
      this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsImageList.Images.SetKeyName(0, "favicon(161).ico");
      this.ButtonsImageList.Images.SetKeyName(1, "favicon(132).ico");
      this.ButtonsImageList.Images.SetKeyName(2, "favicon(76).ico");
      this.ButtonsImageList.Images.SetKeyName(3, "1420498403_340208.ico");
      //
      //rates_edit_BT
      //
      this.rates_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.rates_edit_BT.ImageIndex = 0;
      this.rates_edit_BT.ImageList = this.ButtonsImageList;
      this.rates_edit_BT.Location = new System.Drawing.Point(354, 34);
      this.rates_edit_BT.Margin = new System.Windows.Forms.Padding(2);
      this.rates_edit_BT.Name = "rates_edit_BT";
      this.rates_edit_BT.Size = new System.Drawing.Size(27, 27);
      this.rates_edit_BT.TabIndex = 4;
      this.rates_edit_BT.UseVisualStyleBackColor = true;
      //
      //m_periodsRangeTextBox
      //
      this.m_periodsRangeTextBox.BackColor = System.Drawing.Color.White;
      this.m_periodsRangeTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_periodsRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
      this.m_periodsRangeTextBox.DefaultText = "Empty...";
      this.m_periodsRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_periodsRangeTextBox.Location = new System.Drawing.Point(124, 3);
      this.m_periodsRangeTextBox.MaxLength = 32767;
      this.m_periodsRangeTextBox.Name = "m_periodsRangeTextBox";
      this.m_periodsRangeTextBox.PasswordChar = '\0';
      this.m_periodsRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_periodsRangeTextBox.SelectionLength = 0;
      this.m_periodsRangeTextBox.SelectionStart = 0;
      this.m_periodsRangeTextBox.Size = new System.Drawing.Size(225, 26);
      this.m_periodsRangeTextBox.TabIndex = 9;
      this.m_periodsRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_periodsRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_periodsLabel
      //
      this.m_periodsLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_periodsLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_periodsLabel.Ellipsis = false;
      this.m_periodsLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_periodsLabel.Location = new System.Drawing.Point(3, 3);
      this.m_periodsLabel.Multiline = true;
      this.m_periodsLabel.Name = "m_periodsLabel";
      this.m_periodsLabel.Size = new System.Drawing.Size(80, 25);
      this.m_periodsLabel.TabIndex = 11;
      this.m_periodsLabel.Text = "Periods";
      this.m_periodsLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_periodsLabel.UseMnemonics = true;
      this.m_periodsLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_ratesLabel
      //
      this.m_ratesLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_ratesLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_ratesLabel.Ellipsis = false;
      this.m_ratesLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_ratesLabel.Location = new System.Drawing.Point(3, 35);
      this.m_ratesLabel.Multiline = true;
      this.m_ratesLabel.Name = "m_ratesLabel";
      this.m_ratesLabel.Size = new System.Drawing.Size(80, 25);
      this.m_ratesLabel.TabIndex = 12;
      this.m_ratesLabel.Text = "Rates";
      this.m_ratesLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_ratesLabel.UseMnemonics = true;
      this.m_ratesLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //import_BT
      //
      this.import_BT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.import_BT.ImageIndex = 3;
      this.import_BT.ImageList = this.ButtonsImageList;
      this.import_BT.Location = new System.Drawing.Point(319, 148);
      this.import_BT.Name = "import_BT";
      this.import_BT.Size = new System.Drawing.Size(81, 28);
      this.import_BT.TabIndex = 6;
      this.import_BT.Text = "Upload";
      this.import_BT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.import_BT.UseVisualStyleBackColor = true;
      //
      //m_currencyComboBox
      //
      this.m_currencyComboBox.BackColor = System.Drawing.Color.White;
      this.m_currencyComboBox.DisplayMember = "";
      this.m_currencyComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_currencyComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_currencyComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_currencyComboBox.DropDownWidth = 108;
      this.m_currencyComboBox.Location = new System.Drawing.Point(137, 25);
      this.m_currencyComboBox.Name = "m_currencyComboBox";
      this.m_currencyComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
      this.m_currencyComboBox.Size = new System.Drawing.Size(108, 24);
      this.m_currencyComboBox.TabIndex = 9;
      this.m_currencyComboBox.UseThemeBackColor = false;
      this.m_currencyComboBox.UseThemeDropDownArrowColor = true;
      this.m_currencyComboBox.ValueMember = "";
      this.m_currencyComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_currencyComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_currencyLabel
      //
      this.m_currencyLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_currencyLabel.Ellipsis = false;
      this.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_currencyLabel.Location = new System.Drawing.Point(76, 28);
      this.m_currencyLabel.Multiline = true;
      this.m_currencyLabel.Name = "m_currencyLabel";
      this.m_currencyLabel.Size = new System.Drawing.Size(53, 25);
      this.m_currencyLabel.TabIndex = 12;
      this.m_currencyLabel.Text = "Currency";
      this.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_currencyLabel.UseMnemonics = true;
      this.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //ExcelExchangeRatesImportUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(423, 190);
      this.Controls.Add(this.m_currencyLabel);
      this.Controls.Add(this.m_currencyComboBox);
      this.Controls.Add(this.import_BT);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "ExcelExchangeRatesImportUI";
      this.Text = "Exchange rates upload";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    public System.Windows.Forms.Button periods_edit_BT;
    public System.Windows.Forms.Button rates_edit_BT;
    public System.Windows.Forms.ImageList ButtonsImageList;
    public System.Windows.Forms.Button import_BT;
    public VIBlend.WinForms.Controls.vTextBox m_ratesRangeTextBox;
    public VIBlend.WinForms.Controls.vTextBox m_periodsRangeTextBox;
    public VIBlend.WinForms.Controls.vLabel m_periodsLabel;
    public VIBlend.WinForms.Controls.vLabel m_ratesLabel;
    public VIBlend.WinForms.Controls.vComboBox m_currencyComboBox;
    public VIBlend.WinForms.Controls.vLabel m_currencyLabel;
  }
}