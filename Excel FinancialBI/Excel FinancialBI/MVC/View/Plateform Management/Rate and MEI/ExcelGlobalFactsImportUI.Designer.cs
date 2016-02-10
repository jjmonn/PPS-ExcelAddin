using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class ExcelGlobalFactsImportUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelGlobalFactsImportUI));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_factsRangeTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.periods_edit_BT = new System.Windows.Forms.Button();
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.rates_edit_BT = new System.Windows.Forms.Button();
      this.m_periodsRangeTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_periodsLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_valuesLabel = new VIBlend.WinForms.Controls.vLabel();
      this.import_BT = new System.Windows.Forms.Button();
      this.m_factsComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_globalFactLabel = new VIBlend.WinForms.Controls.vLabel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      //
      //TableLayoutPanel1
      //
      this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47f));
      this.TableLayoutPanel1.Controls.Add(this.m_factsRangeTextBox, 1, 1);
      this.TableLayoutPanel1.Controls.Add(this.periods_edit_BT, 2, 0);
      this.TableLayoutPanel1.Controls.Add(this.rates_edit_BT, 2, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsRangeTextBox, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_valuesLabel, 0, 1);
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
      //m_factsRangeTextBox
      //
      this.m_factsRangeTextBox.BackColor = System.Drawing.Color.White;
      this.m_factsRangeTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_factsRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
      this.m_factsRangeTextBox.DefaultText = "Empty...";
      this.m_factsRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_factsRangeTextBox.Location = new System.Drawing.Point(124, 35);
      this.m_factsRangeTextBox.MaxLength = 32767;
      this.m_factsRangeTextBox.Name = "m_factsRangeTextBox";
      this.m_factsRangeTextBox.PasswordChar = '\0';
      this.m_factsRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_factsRangeTextBox.SelectionLength = 0;
      this.m_factsRangeTextBox.SelectionStart = 0;
      this.m_factsRangeTextBox.Size = new System.Drawing.Size(224, 27);
      this.m_factsRangeTextBox.TabIndex = 10;
      this.m_factsRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_factsRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //periods_edit_BT
      //
      this.periods_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.periods_edit_BT.ImageIndex = 0;
      this.periods_edit_BT.ImageList = this.ButtonsImageList;
      this.periods_edit_BT.Location = new System.Drawing.Point(353, 2);
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
      this.rates_edit_BT.Location = new System.Drawing.Point(353, 34);
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
      this.m_periodsRangeTextBox.Size = new System.Drawing.Size(224, 26);
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
      //m_valuesLabel
      //
      this.m_valuesLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_valuesLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_valuesLabel.Ellipsis = false;
      this.m_valuesLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_valuesLabel.Location = new System.Drawing.Point(3, 35);
      this.m_valuesLabel.Multiline = true;
      this.m_valuesLabel.Name = "m_valuesLabel";
      this.m_valuesLabel.Size = new System.Drawing.Size(80, 25);
      this.m_valuesLabel.TabIndex = 12;
      this.m_valuesLabel.Text = "Values";
      this.m_valuesLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_valuesLabel.UseMnemonics = true;
      this.m_valuesLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //import_BT
      //
      this.import_BT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.import_BT.ImageIndex = 3;
      this.import_BT.ImageList = this.ButtonsImageList;
      this.import_BT.Location = new System.Drawing.Point(302, 148);
      this.import_BT.Name = "import_BT";
      this.import_BT.Size = new System.Drawing.Size(91, 28);
      this.import_BT.TabIndex = 6;
      this.import_BT.Text = "Upload";
      this.import_BT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.import_BT.UseVisualStyleBackColor = true;
      //
      //m_factsComboBox
      //
      this.m_factsComboBox.BackColor = System.Drawing.Color.White;
      this.m_factsComboBox.DisplayMember = "";
      this.m_factsComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_factsComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_factsComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_factsComboBox.DropDownWidth = 202;
      this.m_factsComboBox.Location = new System.Drawing.Point(160, 24);
      this.m_factsComboBox.Name = "m_factsComboBox";
      this.m_factsComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
      this.m_factsComboBox.Size = new System.Drawing.Size(202, 24);
      this.m_factsComboBox.TabIndex = 9;
      this.m_factsComboBox.UseThemeBackColor = false;
      this.m_factsComboBox.UseThemeDropDownArrowColor = true;
      this.m_factsComboBox.ValueMember = "";
      this.m_factsComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_factsComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_globalFactLabel
      //
      this.m_globalFactLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_globalFactLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_globalFactLabel.Ellipsis = false;
      this.m_globalFactLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_globalFactLabel.Location = new System.Drawing.Point(15, 28);
      this.m_globalFactLabel.Multiline = true;
      this.m_globalFactLabel.Name = "m_globalFactLabel";
      this.m_globalFactLabel.Size = new System.Drawing.Size(135, 25);
      this.m_globalFactLabel.TabIndex = 12;
      this.m_globalFactLabel.Text = "Macro economic indicator";
      this.m_globalFactLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_globalFactLabel.UseMnemonics = true;
      this.m_globalFactLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //ExcelFactsValuesImportUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(423, 190);
      this.Controls.Add(this.m_globalFactLabel);
      this.Controls.Add(this.m_factsComboBox);
      this.Controls.Add(this.import_BT);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "ExcelFactsValuesImportUI";
      this.Text = "Global facts upload";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    public System.Windows.Forms.Button periods_edit_BT;
    public System.Windows.Forms.Button rates_edit_BT;
    public System.Windows.Forms.ImageList ButtonsImageList;
    public System.Windows.Forms.Button import_BT;
    public VIBlend.WinForms.Controls.vTextBox m_factsRangeTextBox;
    public VIBlend.WinForms.Controls.vTextBox m_periodsRangeTextBox;
    public VIBlend.WinForms.Controls.vLabel m_periodsLabel;
    public VIBlend.WinForms.Controls.vLabel m_valuesLabel;
    public VIBlend.WinForms.Controls.vComboBox m_factsComboBox;
    public VIBlend.WinForms.Controls.vLabel m_globalFactLabel;
  }
}