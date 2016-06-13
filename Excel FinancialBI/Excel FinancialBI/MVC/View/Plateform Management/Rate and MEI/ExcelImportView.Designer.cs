using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class ExcelImportView : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelImportView));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_valuesRangeTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_periodsButton = new System.Windows.Forms.Button();
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_valuesButton = new System.Windows.Forms.Button();
      this.m_periodsRangeTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_periodsLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_valueLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_importButton = new System.Windows.Forms.Button();
      this.m_comboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_descriptionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
      this.TableLayoutPanel1.Controls.Add(this.m_valuesRangeTextBox, 1, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsButton, 2, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_valuesButton, 2, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsRangeTextBox, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_valueLabel, 0, 1);
      this.TableLayoutPanel1.Location = new System.Drawing.Point(12, 67);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(399, 65);
      this.TableLayoutPanel1.TabIndex = 8;
      // 
      // m_valuesRangeTextBox
      // 
      this.m_valuesRangeTextBox.BackColor = System.Drawing.Color.White;
      this.m_valuesRangeTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_valuesRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_valuesRangeTextBox.DefaultText = "Empty...";
      this.m_valuesRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_valuesRangeTextBox.Location = new System.Drawing.Point(123, 35);
      this.m_valuesRangeTextBox.MaxLength = 32767;
      this.m_valuesRangeTextBox.Name = "m_valuesRangeTextBox";
      this.m_valuesRangeTextBox.PasswordChar = '\0';
      this.m_valuesRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_valuesRangeTextBox.SelectionLength = 0;
      this.m_valuesRangeTextBox.SelectionStart = 0;
      this.m_valuesRangeTextBox.Size = new System.Drawing.Size(224, 27);
      this.m_valuesRangeTextBox.TabIndex = 10;
      this.m_valuesRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_valuesRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_periodsButton
      // 
      this.m_periodsButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.m_periodsButton.ImageIndex = 0;
      this.m_periodsButton.ImageList = this.ButtonsImageList;
      this.m_periodsButton.Location = new System.Drawing.Point(352, 2);
      this.m_periodsButton.Margin = new System.Windows.Forms.Padding(2);
      this.m_periodsButton.Name = "m_periodsButton";
      this.m_periodsButton.Size = new System.Drawing.Size(27, 27);
      this.m_periodsButton.TabIndex = 2;
      this.m_periodsButton.UseVisualStyleBackColor = true;
      // 
      // ButtonsImageList
      // 
      this.ButtonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsImageList.ImageStream")));
      this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsImageList.Images.SetKeyName(0, "favicon(161).ico");
      this.ButtonsImageList.Images.SetKeyName(1, "favicon(132).ico");
      this.ButtonsImageList.Images.SetKeyName(2, "favicon(76).ico");
      this.ButtonsImageList.Images.SetKeyName(3, "1420498403_340208.ico");
      // 
      // m_valuesButton
      // 
      this.m_valuesButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
      this.m_valuesButton.ImageIndex = 0;
      this.m_valuesButton.ImageList = this.ButtonsImageList;
      this.m_valuesButton.Location = new System.Drawing.Point(352, 34);
      this.m_valuesButton.Margin = new System.Windows.Forms.Padding(2);
      this.m_valuesButton.Name = "m_valuesButton";
      this.m_valuesButton.Size = new System.Drawing.Size(27, 27);
      this.m_valuesButton.TabIndex = 4;
      this.m_valuesButton.UseVisualStyleBackColor = true;
      // 
      // m_periodsRangeTextBox
      // 
      this.m_periodsRangeTextBox.BackColor = System.Drawing.Color.White;
      this.m_periodsRangeTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_periodsRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_periodsRangeTextBox.DefaultText = "Empty...";
      this.m_periodsRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_periodsRangeTextBox.Location = new System.Drawing.Point(123, 3);
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
      // m_periodsLabel
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
      // m_valueLabel
      // 
      this.m_valueLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_valueLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_valueLabel.Ellipsis = false;
      this.m_valueLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_valueLabel.Location = new System.Drawing.Point(3, 35);
      this.m_valueLabel.Multiline = true;
      this.m_valueLabel.Name = "m_valueLabel";
      this.m_valueLabel.Size = new System.Drawing.Size(80, 25);
      this.m_valueLabel.TabIndex = 12;
      this.m_valueLabel.Text = "Rates";
      this.m_valueLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_valueLabel.UseMnemonics = true;
      this.m_valueLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_importButton
      // 
      this.m_importButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_importButton.ImageIndex = 3;
      this.m_importButton.ImageList = this.ButtonsImageList;
      this.m_importButton.Location = new System.Drawing.Point(319, 148);
      this.m_importButton.Name = "m_importButton";
      this.m_importButton.Size = new System.Drawing.Size(81, 28);
      this.m_importButton.TabIndex = 6;
      this.m_importButton.Text = "Upload";
      this.m_importButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_importButton.UseVisualStyleBackColor = true;
      // 
      // m_comboBox
      // 
      this.m_comboBox.BackColor = System.Drawing.Color.White;
      this.m_comboBox.DisplayMember = "";
      this.m_comboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_comboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_comboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_comboBox.DropDownWidth = 140;
      this.m_comboBox.Location = new System.Drawing.Point(137, 25);
      this.m_comboBox.Name = "m_comboBox";
      this.m_comboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.m_comboBox.Size = new System.Drawing.Size(140, 24);
      this.m_comboBox.TabIndex = 9;
      this.m_comboBox.UseThemeBackColor = false;
      this.m_comboBox.UseThemeDropDownArrowColor = true;
      this.m_comboBox.ValueMember = "";
      this.m_comboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_comboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_descriptionLabel
      // 
      this.m_descriptionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_descriptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_descriptionLabel.Ellipsis = false;
      this.m_descriptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_descriptionLabel.Location = new System.Drawing.Point(15, 25);
      this.m_descriptionLabel.Multiline = true;
      this.m_descriptionLabel.Name = "m_descriptionLabel";
      this.m_descriptionLabel.Size = new System.Drawing.Size(116, 25);
      this.m_descriptionLabel.TabIndex = 12;
      this.m_descriptionLabel.Text = "Currency";
      this.m_descriptionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_descriptionLabel.UseMnemonics = true;
      this.m_descriptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ExcelImportView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(423, 190);
      this.Controls.Add(this.m_descriptionLabel);
      this.Controls.Add(this.m_comboBox);
      this.Controls.Add(this.m_importButton);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ExcelImportView";
      this.Text = "Exchange rates upload";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    public System.Windows.Forms.Button m_periodsButton;
    public System.Windows.Forms.Button m_valuesButton;
    public System.Windows.Forms.ImageList ButtonsImageList;
    public System.Windows.Forms.Button m_importButton;
    public VIBlend.WinForms.Controls.vTextBox m_valuesRangeTextBox;
    public VIBlend.WinForms.Controls.vTextBox m_periodsRangeTextBox;
    public VIBlend.WinForms.Controls.vLabel m_periodsLabel;
    public VIBlend.WinForms.Controls.vLabel m_valueLabel;
    public VIBlend.WinForms.Controls.vComboBox m_comboBox;
    public VIBlend.WinForms.Controls.vLabel m_descriptionLabel;
  }
}