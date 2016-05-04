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
      this.m_chartTitle = new VIBlend.WinForms.Controls.vTextBox();
      this.m_serieColor = new VIBlend.WinForms.Controls.vColorPicker();
      this.m_AccountLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_ColorLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_saveButton = new VIBlend.WinForms.Controls.vButton();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.m_serie = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_addSerie = new VIBlend.WinForms.Controls.vButton();
      this.m_removeSerie = new VIBlend.WinForms.Controls.vButton();
      this.m_chartTitleLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_serieLabel = new VIBlend.WinForms.Controls.vLabel();
      this.SuspendLayout();
      // 
      // m_chartTitle
      // 
      this.m_chartTitle.BackColor = System.Drawing.Color.White;
      this.m_chartTitle.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_chartTitle.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_chartTitle.DefaultText = "Empty...";
      this.m_chartTitle.Location = new System.Drawing.Point(86, 26);
      this.m_chartTitle.MaxLength = 32767;
      this.m_chartTitle.Name = "m_chartTitle";
      this.m_chartTitle.PasswordChar = '\0';
      this.m_chartTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_chartTitle.SelectionLength = 0;
      this.m_chartTitle.SelectionStart = 0;
      this.m_chartTitle.Size = new System.Drawing.Size(246, 23);
      this.m_chartTitle.TabIndex = 1;
      this.m_chartTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_chartTitle.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_serieColor
      // 
      this.m_serieColor.BackColor = System.Drawing.Color.White;
      this.m_serieColor.BorderColor = System.Drawing.Color.Black;
      this.m_serieColor.DropDownHeight = 250;
      this.m_serieColor.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_serieColor.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_serieColor.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
      this.m_serieColor.DropDownWidth = 202;
      this.m_serieColor.Location = new System.Drawing.Point(347, 100);
      this.m_serieColor.Name = "m_serieColor";
      this.m_serieColor.SelectedColor = System.Drawing.Color.White;
      this.m_serieColor.ShowGrip = false;
      this.m_serieColor.Size = new System.Drawing.Size(143, 23);
      this.m_serieColor.TabIndex = 4;
      this.m_serieColor.Text = "255, 255, 255, 255";
      this.m_serieColor.UseThemeBackColor = false;
      this.m_serieColor.UseThemeDropDownArrowColor = true;
      this.m_serieColor.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_AccountLabel
      // 
      this.m_AccountLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_AccountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_AccountLabel.Ellipsis = false;
      this.m_AccountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_AccountLabel.Location = new System.Drawing.Point(86, 69);
      this.m_AccountLabel.Multiline = true;
      this.m_AccountLabel.Name = "m_AccountLabel";
      this.m_AccountLabel.Size = new System.Drawing.Size(246, 25);
      this.m_AccountLabel.TabIndex = 6;
      this.m_AccountLabel.Text = "[general.account]";
      this.m_AccountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_AccountLabel.UseMnemonics = true;
      this.m_AccountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_ColorLabel
      // 
      this.m_ColorLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_ColorLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_ColorLabel.Ellipsis = false;
      this.m_ColorLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_ColorLabel.Location = new System.Drawing.Point(347, 69);
      this.m_ColorLabel.Multiline = true;
      this.m_ColorLabel.Name = "m_ColorLabel";
      this.m_ColorLabel.Size = new System.Drawing.Size(143, 25);
      this.m_ColorLabel.TabIndex = 7;
      this.m_ColorLabel.Text = "[general.color]";
      this.m_ColorLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_ColorLabel.UseMnemonics = true;
      this.m_ColorLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_saveButton
      // 
      this.m_saveButton.AllowAnimations = true;
      this.m_saveButton.BackColor = System.Drawing.Color.Transparent;
      this.m_saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_saveButton.ImageKey = "1420498403_340208.ico";
      this.m_saveButton.ImageList = this.ImageList1;
      this.m_saveButton.Location = new System.Drawing.Point(394, 173);
      this.m_saveButton.Name = "m_saveButton";
      this.m_saveButton.RoundedCornersMask = ((byte)(15));
      this.m_saveButton.Size = new System.Drawing.Size(96, 22);
      this.m_saveButton.TabIndex = 13;
      this.m_saveButton.Text = "[general.save]";
      this.m_saveButton.UseVisualStyleBackColor = false;
      this.m_saveButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico");
      this.ImageList1.Images.SetKeyName(1, "imageres_89.ico");
      this.ImageList1.Images.SetKeyName(2, "imageres_891.ico");
      // 
      // m_serie
      // 
      this.m_serie.BackColor = System.Drawing.Color.White;
      this.m_serie.BorderColor = System.Drawing.Color.Black;
      this.m_serie.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_serie.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_serie.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_serie.Location = new System.Drawing.Point(86, 100);
      this.m_serie.Name = "m_serie";
      this.m_serie.Size = new System.Drawing.Size(246, 23);
      this.m_serie.TabIndex = 14;
      this.m_serie.UseThemeBackColor = false;
      this.m_serie.UseThemeDropDownArrowColor = true;
      this.m_serie.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_addSerie
      // 
      this.m_addSerie.AllowAnimations = true;
      this.m_addSerie.BackColor = System.Drawing.Color.Transparent;
      this.m_addSerie.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_addSerie.ImageKey = "1420498403_340208.ico";
      this.m_addSerie.Location = new System.Drawing.Point(86, 155);
      this.m_addSerie.Name = "m_addSerie";
      this.m_addSerie.RoundedCornersMask = ((byte)(15));
      this.m_addSerie.Size = new System.Drawing.Size(142, 22);
      this.m_addSerie.TabIndex = 14;
      this.m_addSerie.Text = "[chart.add_serie]";
      this.m_addSerie.UseVisualStyleBackColor = false;
      this.m_addSerie.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_removeSerie
      // 
      this.m_removeSerie.AllowAnimations = true;
      this.m_removeSerie.BackColor = System.Drawing.Color.Transparent;
      this.m_removeSerie.FlatAppearance.BorderSize = 0;
      this.m_removeSerie.ImageKey = "imageres_891.ico";
      this.m_removeSerie.ImageList = this.ImageList1;
      this.m_removeSerie.Location = new System.Drawing.Point(508, 100);
      this.m_removeSerie.Name = "m_removeSerie";
      this.m_removeSerie.RoundedCornersMask = ((byte)(15));
      this.m_removeSerie.Size = new System.Drawing.Size(26, 23);
      this.m_removeSerie.TabIndex = 14;
      this.m_removeSerie.UseVisualStyleBackColor = false;
      this.m_removeSerie.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_chartTitleLabel
      // 
      this.m_chartTitleLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_chartTitleLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_chartTitleLabel.Ellipsis = false;
      this.m_chartTitleLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_chartTitleLabel.Location = new System.Drawing.Point(17, 24);
      this.m_chartTitleLabel.Multiline = true;
      this.m_chartTitleLabel.Name = "m_chartTitleLabel";
      this.m_chartTitleLabel.Size = new System.Drawing.Size(63, 25);
      this.m_chartTitleLabel.TabIndex = 0;
      this.m_chartTitleLabel.Text = "[CUI_Charts.chart_title]";
      this.m_chartTitleLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_chartTitleLabel.UseMnemonics = true;
      this.m_chartTitleLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_serieLabel
      // 
      this.m_serieLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_serieLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_serieLabel.Ellipsis = false;
      this.m_serieLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_serieLabel.Location = new System.Drawing.Point(17, 98);
      this.m_serieLabel.Multiline = true;
      this.m_serieLabel.Name = "m_serieLabel";
      this.m_serieLabel.Size = new System.Drawing.Size(63, 25);
      this.m_serieLabel.TabIndex = 1;
      this.m_serieLabel.Text = "[CUI_Charts.chart_title]";
      this.m_serieLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_serieLabel.UseMnemonics = true;
      this.m_serieLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // CUI2VisualisationChartsSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(546, 207);
      this.Controls.Add(this.m_serieLabel);
      this.Controls.Add(this.m_removeSerie);
      this.Controls.Add(this.m_addSerie);
      this.Controls.Add(this.m_serie);
      this.Controls.Add(this.m_saveButton);
      this.Controls.Add(this.m_ColorLabel);
      this.Controls.Add(this.m_AccountLabel);
      this.Controls.Add(this.m_serieColor);
      this.Controls.Add(this.m_chartTitle);
      this.Controls.Add(this.m_chartTitleLabel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "CUI2VisualisationChartsSettings";
      this.Text = "[CUI_Charts.charts_settings]";
      this.ResumeLayout(false);

  }
  public VIBlend.WinForms.Controls.vTextBox m_chartTitle;
  public VIBlend.WinForms.Controls.vColorPicker m_serieColor;
	public VIBlend.WinForms.Controls.vLabel m_AccountLabel;
  public VIBlend.WinForms.Controls.vLabel m_ColorLabel;
	public VIBlend.WinForms.Controls.vButton m_saveButton;
	public System.Windows.Forms.ImageList ImageList1;
  public VIBlend.WinForms.Controls.vTreeViewBox m_serie;
  public VIBlend.WinForms.Controls.vButton m_addSerie;
  public VIBlend.WinForms.Controls.vButton m_removeSerie;
  public VIBlend.WinForms.Controls.vLabel m_chartTitleLabel;
  public VIBlend.WinForms.Controls.vLabel m_serieLabel;
}

}
