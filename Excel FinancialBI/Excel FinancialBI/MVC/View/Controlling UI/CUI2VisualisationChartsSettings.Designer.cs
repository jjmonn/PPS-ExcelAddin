using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class CUI2VisualisationChartsSettings : System.Windows.Forms.Form
{

	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode()]
	protected override void Dispose(bool disposing)
	{
		try {
			if (disposing && components != null) {
				components.Dispose();
			}
		} finally {
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CUI2VisualisationChartsSettings));
		this.m_chartTitleLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_chartTitleTextBox = new VIBlend.WinForms.Controls.vTextBox();
		this.m_chartSerie2Label = new VIBlend.WinForms.Controls.vLabel();
		this.m_chartSerie1Label = new VIBlend.WinForms.Controls.vLabel();
		this.m_serie1ColorPicker = new VIBlend.WinForms.Controls.vColorPicker();
		this.m_serie2ColorPicker = new VIBlend.WinForms.Controls.vColorPicker();
		this.m_AccountLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_ColorLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_serie1TypeComboBox = new VIBlend.WinForms.Controls.vComboBox();
		this.m_serie2TypeComboBox = new VIBlend.WinForms.Controls.vComboBox();
		this.m_typeLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_saveButton = new VIBlend.WinForms.Controls.vButton();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.m_serie1AccountTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.m_serie2AccountTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.SuspendLayout();
		//
		//m_chartTitleLabel
		//
		this.m_chartTitleLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_chartTitleLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_chartTitleLabel.Ellipsis = false;
		this.m_chartTitleLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_chartTitleLabel.Location = new System.Drawing.Point(17, 24);
		this.m_chartTitleLabel.Multiline = true;
		this.m_chartTitleLabel.Name = "m_chartTitleLabel";
		this.m_chartTitleLabel.Size = new System.Drawing.Size(41, 25);
		this.m_chartTitleLabel.TabIndex = 0;
		this.m_chartTitleLabel.Text = FBI.Utils.Local.GetValue("CUI_Charts.chart_title");
		this.m_chartTitleLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_chartTitleLabel.UseMnemonics = true;
		this.m_chartTitleLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_chartTitleTextBox
		//
		this.m_chartTitleTextBox.BackColor = System.Drawing.Color.White;
		this.m_chartTitleTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_chartTitleTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_chartTitleTextBox.DefaultText = "Empty...";
		this.m_chartTitleTextBox.Location = new System.Drawing.Point(86, 26);
		this.m_chartTitleTextBox.MaxLength = 32767;
		this.m_chartTitleTextBox.Name = "m_chartTitleTextBox";
    this.m_chartTitleTextBox.PasswordChar = '\0';
		this.m_chartTitleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_chartTitleTextBox.SelectionLength = 0;
		this.m_chartTitleTextBox.SelectionStart = 0;
		this.m_chartTitleTextBox.Size = new System.Drawing.Size(246, 23);
		this.m_chartTitleTextBox.TabIndex = 1;
		this.m_chartTitleTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_chartTitleTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_chartSerie2Label
		//
		this.m_chartSerie2Label.BackColor = System.Drawing.Color.Transparent;
		this.m_chartSerie2Label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_chartSerie2Label.Ellipsis = false;
		this.m_chartSerie2Label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_chartSerie2Label.Location = new System.Drawing.Point(17, 119);
		this.m_chartSerie2Label.Multiline = true;
		this.m_chartSerie2Label.Name = "m_chartSerie2Label";
		this.m_chartSerie2Label.Size = new System.Drawing.Size(41, 25);
		this.m_chartSerie2Label.TabIndex = 2;
    this.m_chartSerie2Label.Text = FBI.Utils.Local.GetValue("CUI_Charts.serie_2");
		this.m_chartSerie2Label.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_chartSerie2Label.UseMnemonics = true;
		this.m_chartSerie2Label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_chartSerie1Label
		//
		this.m_chartSerie1Label.BackColor = System.Drawing.Color.Transparent;
		this.m_chartSerie1Label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_chartSerie1Label.Ellipsis = false;
		this.m_chartSerie1Label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_chartSerie1Label.Location = new System.Drawing.Point(17, 88);
		this.m_chartSerie1Label.Multiline = true;
		this.m_chartSerie1Label.Name = "m_chartSerie1Label";
		this.m_chartSerie1Label.Size = new System.Drawing.Size(41, 25);
		this.m_chartSerie1Label.TabIndex = 2;
    this.m_chartSerie1Label.Text = FBI.Utils.Local.GetValue("CUI_Charts.serie_1");
		this.m_chartSerie1Label.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.m_chartSerie1Label.UseMnemonics = true;
		this.m_chartSerie1Label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_serie1ColorPicker
		//
		this.m_serie1ColorPicker.BackColor = System.Drawing.Color.White;
		this.m_serie1ColorPicker.BorderColor = System.Drawing.Color.Black;
		this.m_serie1ColorPicker.DropDownHeight = 250;
		this.m_serie1ColorPicker.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_serie1ColorPicker.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_serie1ColorPicker.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
		this.m_serie1ColorPicker.DropDownWidth = 202;
		this.m_serie1ColorPicker.Location = new System.Drawing.Point(347, 90);
		this.m_serie1ColorPicker.Name = "m_serie1ColorPicker";
		this.m_serie1ColorPicker.SelectedColor = System.Drawing.Color.White;
		this.m_serie1ColorPicker.ShowGrip = false;
		this.m_serie1ColorPicker.Size = new System.Drawing.Size(143, 23);
		this.m_serie1ColorPicker.TabIndex = 4;
		this.m_serie1ColorPicker.Text = "255, 255, 255, 255";
		this.m_serie1ColorPicker.UseThemeBackColor = false;
		this.m_serie1ColorPicker.UseThemeDropDownArrowColor = true;
		this.m_serie1ColorPicker.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_serie2ColorPicker
		//
		this.m_serie2ColorPicker.BackColor = System.Drawing.Color.White;
		this.m_serie2ColorPicker.BorderColor = System.Drawing.Color.Black;
		this.m_serie2ColorPicker.DropDownHeight = 250;
		this.m_serie2ColorPicker.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_serie2ColorPicker.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_serie2ColorPicker.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
		this.m_serie2ColorPicker.DropDownWidth = 202;
		this.m_serie2ColorPicker.Location = new System.Drawing.Point(347, 121);
		this.m_serie2ColorPicker.Name = "m_serie2ColorPicker";
		this.m_serie2ColorPicker.SelectedColor = System.Drawing.Color.White;
		this.m_serie2ColorPicker.ShowGrip = false;
		this.m_serie2ColorPicker.Size = new System.Drawing.Size(143, 23);
		this.m_serie2ColorPicker.TabIndex = 5;
		this.m_serie2ColorPicker.Text = "255, 255, 255, 255";
		this.m_serie2ColorPicker.UseThemeBackColor = false;
		this.m_serie2ColorPicker.UseThemeDropDownArrowColor = true;
		this.m_serie2ColorPicker.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_AccountLabel
		//
		this.m_AccountLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_AccountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_AccountLabel.Ellipsis = false;
		this.m_AccountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_AccountLabel.Location = new System.Drawing.Point(86, 59);
		this.m_AccountLabel.Multiline = true;
		this.m_AccountLabel.Name = "m_AccountLabel";
		this.m_AccountLabel.Size = new System.Drawing.Size(246, 25);
		this.m_AccountLabel.TabIndex = 6;
    this.m_AccountLabel.Text = FBI.Utils.Local.GetValue("general.account");
		this.m_AccountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.m_AccountLabel.UseMnemonics = true;
		this.m_AccountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_ColorLabel
		//
		this.m_ColorLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_ColorLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_ColorLabel.Ellipsis = false;
		this.m_ColorLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_ColorLabel.Location = new System.Drawing.Point(347, 62);
		this.m_ColorLabel.Multiline = true;
		this.m_ColorLabel.Name = "m_ColorLabel";
		this.m_ColorLabel.Size = new System.Drawing.Size(143, 25);
		this.m_ColorLabel.TabIndex = 7;
    this.m_ColorLabel.Text = FBI.Utils.Local.GetValue("general.couleur");
		this.m_ColorLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.m_ColorLabel.UseMnemonics = true;
		this.m_ColorLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_serie1TypeComboBox
		//
		this.m_serie1TypeComboBox.BackColor = System.Drawing.Color.White;
		this.m_serie1TypeComboBox.DisplayMember = "";
		this.m_serie1TypeComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_serie1TypeComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_serie1TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_serie1TypeComboBox.DropDownWidth = 226;
		this.m_serie1TypeComboBox.Location = new System.Drawing.Point(503, 90);
		this.m_serie1TypeComboBox.Name = "m_serie1TypeComboBox";
		this.m_serie1TypeComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.m_serie1TypeComboBox.Size = new System.Drawing.Size(226, 23);
		this.m_serie1TypeComboBox.TabIndex = 8;
		this.m_serie1TypeComboBox.UseThemeBackColor = false;
		this.m_serie1TypeComboBox.UseThemeDropDownArrowColor = true;
		this.m_serie1TypeComboBox.ValueMember = "";
		this.m_serie1TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_serie1TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_serie2TypeComboBox
		//
		this.m_serie2TypeComboBox.BackColor = System.Drawing.Color.White;
		this.m_serie2TypeComboBox.DisplayMember = "";
		this.m_serie2TypeComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_serie2TypeComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_serie2TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_serie2TypeComboBox.DropDownWidth = 226;
		this.m_serie2TypeComboBox.Location = new System.Drawing.Point(503, 121);
		this.m_serie2TypeComboBox.Name = "m_serie2TypeComboBox";
		this.m_serie2TypeComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.m_serie2TypeComboBox.Size = new System.Drawing.Size(226, 23);
		this.m_serie2TypeComboBox.TabIndex = 9;
		this.m_serie2TypeComboBox.UseThemeBackColor = false;
		this.m_serie2TypeComboBox.UseThemeDropDownArrowColor = true;
		this.m_serie2TypeComboBox.ValueMember = "";
		this.m_serie2TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_serie2TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_typeLabel
		//
		this.m_typeLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_typeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_typeLabel.Ellipsis = false;
		this.m_typeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_typeLabel.Location = new System.Drawing.Point(503, 62);
		this.m_typeLabel.Multiline = true;
		this.m_typeLabel.Name = "m_typeLabel";
		this.m_typeLabel.Size = new System.Drawing.Size(226, 25);
		this.m_typeLabel.TabIndex = 10;
    this.m_typeLabel.Text = FBI.Utils.Local.GetValue("general.type");
		this.m_typeLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.m_typeLabel.UseMnemonics = true;
		this.m_typeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_saveButton
		//
		this.m_saveButton.AllowAnimations = true;
		this.m_saveButton.BackColor = System.Drawing.Color.Transparent;
		this.m_saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.m_saveButton.ImageKey = "1420498403_340208.ico";
		this.m_saveButton.ImageList = this.ImageList1;
		this.m_saveButton.Location = new System.Drawing.Point(649, 165);
		this.m_saveButton.Name = "m_saveButton";
		this.m_saveButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_saveButton.Size = new System.Drawing.Size(80, 22);
		this.m_saveButton.TabIndex = 13;
    this.m_saveButton.Text = FBI.Utils.Local.GetValue("general.save");
		this.m_saveButton.UseVisualStyleBackColor = false;
		this.m_saveButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico");
		//
		//m_serie1AccountTreeviewBox
		//
		this.m_serie1AccountTreeviewBox.BackColor = System.Drawing.Color.White;
		this.m_serie1AccountTreeviewBox.BorderColor = System.Drawing.Color.Black;
		this.m_serie1AccountTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_serie1AccountTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_serie1AccountTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_serie1AccountTreeviewBox.Location = new System.Drawing.Point(86, 90);
		this.m_serie1AccountTreeviewBox.Name = "m_serie1AccountTreeviewBox";
		this.m_serie1AccountTreeviewBox.Size = new System.Drawing.Size(246, 23);
		this.m_serie1AccountTreeviewBox.TabIndex = 14;
		this.m_serie1AccountTreeviewBox.UseThemeBackColor = false;
		this.m_serie1AccountTreeviewBox.UseThemeDropDownArrowColor = true;
		this.m_serie1AccountTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_serie2AccountTreeviewBox
		//
		this.m_serie2AccountTreeviewBox.BackColor = System.Drawing.Color.White;
		this.m_serie2AccountTreeviewBox.BorderColor = System.Drawing.Color.Black;
		this.m_serie2AccountTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_serie2AccountTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_serie2AccountTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_serie2AccountTreeviewBox.Location = new System.Drawing.Point(86, 121);
		this.m_serie2AccountTreeviewBox.Name = "m_serie2AccountTreeviewBox";
		this.m_serie2AccountTreeviewBox.Size = new System.Drawing.Size(246, 23);
		this.m_serie2AccountTreeviewBox.TabIndex = 15;
		this.m_serie2AccountTreeviewBox.UseThemeBackColor = false;
		this.m_serie2AccountTreeviewBox.UseThemeDropDownArrowColor = true;
		this.m_serie2AccountTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//CUI2VisualisationChartsSettings
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(744, 207);
		this.Controls.Add(this.m_serie2AccountTreeviewBox);
		this.Controls.Add(this.m_serie1AccountTreeviewBox);
		this.Controls.Add(this.m_saveButton);
		this.Controls.Add(this.m_typeLabel);
		this.Controls.Add(this.m_serie2TypeComboBox);
		this.Controls.Add(this.m_serie1TypeComboBox);
		this.Controls.Add(this.m_ColorLabel);
		this.Controls.Add(this.m_AccountLabel);
		this.Controls.Add(this.m_serie2ColorPicker);
		this.Controls.Add(this.m_serie1ColorPicker);
		this.Controls.Add(this.m_chartSerie1Label);
		this.Controls.Add(this.m_chartSerie2Label);
		this.Controls.Add(this.m_chartTitleTextBox);
		this.Controls.Add(this.m_chartTitleLabel);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "CUI2VisualisationChartsSettings";
    this.Text = FBI.Utils.Local.GetValue("CUI_Charts.charts_settings");
		this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.Controls.vLabel m_chartTitleLabel;
	internal VIBlend.WinForms.Controls.vTextBox m_chartTitleTextBox;
	internal VIBlend.WinForms.Controls.vLabel m_chartSerie2Label;
	internal VIBlend.WinForms.Controls.vLabel m_chartSerie1Label;
	internal VIBlend.WinForms.Controls.vColorPicker m_serie1ColorPicker;
	internal VIBlend.WinForms.Controls.vColorPicker m_serie2ColorPicker;
	internal VIBlend.WinForms.Controls.vLabel m_AccountLabel;
	internal VIBlend.WinForms.Controls.vLabel m_ColorLabel;
	internal VIBlend.WinForms.Controls.vComboBox m_serie1TypeComboBox;
	internal VIBlend.WinForms.Controls.vComboBox m_serie2TypeComboBox;
	internal VIBlend.WinForms.Controls.vLabel m_typeLabel;
	internal VIBlend.WinForms.Controls.vButton m_saveButton;
	internal System.Windows.Forms.ImageList ImageList1;
	internal VIBlend.WinForms.Controls.vTreeViewBox m_serie1AccountTreeviewBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox m_serie2AccountTreeviewBox;
}

}
