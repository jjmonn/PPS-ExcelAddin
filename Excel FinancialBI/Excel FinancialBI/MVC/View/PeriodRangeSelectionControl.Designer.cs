using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class PeriodRangeSelectionControl : System.Windows.Forms.UserControl
{

	//UserControl overrides dispose to clean up the component list.
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

  private System.ComponentModel.IContainer components = null;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		this.m_endWeekTB = new VIBlend.WinForms.Controls.vTextBox();
		this.m_startWeekTB = new VIBlend.WinForms.Controls.vTextBox();
		this.m_startDateLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_endDateLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_startDate = new VIBlend.WinForms.Controls.vDatePicker();
		this.m_endDate = new VIBlend.WinForms.Controls.vDatePicker();
		this.SuspendLayout();
		//
		//m_endWeekTB
		//
		this.m_endWeekTB.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_endWeekTB.BackColor = System.Drawing.Color.White;
		this.m_endWeekTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_endWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_endWeekTB.DefaultText = "Empty...";
		this.m_endWeekTB.Enabled = false;
		this.m_endWeekTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.764706f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
		this.m_endWeekTB.Location = new System.Drawing.Point(107, 108);
		this.m_endWeekTB.MaxLength = 32767;
		this.m_endWeekTB.Name = "m_endWeekTB";
		this.m_endWeekTB.PasswordChar = '\0';
		this.m_endWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_endWeekTB.SelectionLength = 0;
		this.m_endWeekTB.SelectionStart = 0;
		this.m_endWeekTB.Size = new System.Drawing.Size(122, 23);
		this.m_endWeekTB.TabIndex = 15;
		this.m_endWeekTB.Text = " ";
		this.m_endWeekTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_endWeekTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_startWeekTB
		//
		this.m_startWeekTB.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_startWeekTB.BackColor = System.Drawing.Color.White;
		this.m_startWeekTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_startWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_startWeekTB.DefaultText = "Empty...";
		this.m_startWeekTB.Enabled = false;
		this.m_startWeekTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.764706f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
		this.m_startWeekTB.Location = new System.Drawing.Point(107, 40);
		this.m_startWeekTB.MaxLength = 32767;
		this.m_startWeekTB.Name = "m_startWeekTB";
		this.m_startWeekTB.PasswordChar = '\0';
		this.m_startWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_startWeekTB.SelectionLength = 0;
		this.m_startWeekTB.SelectionStart = 0;
		this.m_startWeekTB.Size = new System.Drawing.Size(122, 23);
		this.m_startWeekTB.TabIndex = 10;
		this.m_startWeekTB.Text = " ";
		this.m_startWeekTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_startWeekTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_startDateLabel
		//
		this.m_startDateLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_startDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_startDateLabel.Ellipsis = false;
		this.m_startDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.764706f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
		this.m_startDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_startDateLabel.Location = new System.Drawing.Point(6, 8);
		this.m_startDateLabel.Multiline = true;
		this.m_startDateLabel.Name = "m_startDateLabel";
		this.m_startDateLabel.Size = new System.Drawing.Size(100, 26);
		this.m_startDateLabel.TabIndex = 14;
		this.m_startDateLabel.Text = "Start date";
		this.m_startDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_startDateLabel.UseMnemonics = true;
		this.m_startDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_endDateLabel
		//
		this.m_endDateLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_endDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_endDateLabel.Ellipsis = false;
		this.m_endDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.764706f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
		this.m_endDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_endDateLabel.Location = new System.Drawing.Point(6, 76);
		this.m_endDateLabel.Multiline = true;
		this.m_endDateLabel.Name = "m_endDateLabel";
		this.m_endDateLabel.Size = new System.Drawing.Size(100, 26);
		this.m_endDateLabel.TabIndex = 13;
		this.m_endDateLabel.Text = "End date";
		this.m_endDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_endDateLabel.UseMnemonics = true;
		this.m_endDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_startDate
		//
		this.m_startDate.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_startDate.BackColor = System.Drawing.Color.White;
		this.m_startDate.BorderColor = System.Drawing.Color.Black;
		this.m_startDate.Culture = new System.Globalization.CultureInfo("");
		this.m_startDate.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_startDate.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_startDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
		this.m_startDate.FormatValue = "";
		this.m_startDate.Location = new System.Drawing.Point(107, 8);
		this.m_startDate.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
		this.m_startDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
		this.m_startDate.Name = "m_startDate";
		this.m_startDate.ShowGrip = false;
		this.m_startDate.Size = new System.Drawing.Size(122, 26);
		this.m_startDate.TabIndex = 12;
		this.m_startDate.Text = "VDatePicker1";
		this.m_startDate.UseThemeBackColor = false;
		this.m_startDate.UseThemeDropDownArrowColor = true;
		this.m_startDate.Value = new System.DateTime(2016, 1, 4, 9, 28, 7, 919);
		this.m_startDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_endDate
		//
		this.m_endDate.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_endDate.BackColor = System.Drawing.Color.White;
		this.m_endDate.BorderColor = System.Drawing.Color.Black;
		this.m_endDate.Culture = new System.Globalization.CultureInfo("");
		this.m_endDate.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_endDate.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_endDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
		this.m_endDate.FormatValue = "";
		this.m_endDate.Location = new System.Drawing.Point(107, 76);
		this.m_endDate.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
		this.m_endDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
		this.m_endDate.Name = "m_endDate";
		this.m_endDate.ShowGrip = false;
		this.m_endDate.Size = new System.Drawing.Size(122, 26);
		this.m_endDate.TabIndex = 11;
		this.m_endDate.Text = "VDatePicker1";
		this.m_endDate.UseThemeBackColor = false;
		this.m_endDate.UseThemeDropDownArrowColor = true;
		this.m_endDate.Value = new System.DateTime(2016, 1, 4, 9, 27, 20, 177);
		this.m_endDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//PeriodRangeSelectionControl
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.m_endWeekTB);
		this.Controls.Add(this.m_startWeekTB);
		this.Controls.Add(this.m_startDateLabel);
		this.Controls.Add(this.m_endDateLabel);
		this.Controls.Add(this.m_startDate);
		this.Controls.Add(this.m_endDate);
		this.Name = "PeriodRangeSelectionControl";
		this.Size = new System.Drawing.Size(239, 137);
		this.ResumeLayout(false);

	}
	public VIBlend.WinForms.Controls.vTextBox m_endWeekTB;
	public VIBlend.WinForms.Controls.vTextBox m_startWeekTB;
	public VIBlend.WinForms.Controls.vLabel m_startDateLabel;
	public VIBlend.WinForms.Controls.vLabel m_endDateLabel;
	public VIBlend.WinForms.Controls.vDatePicker m_startDate;

	public VIBlend.WinForms.Controls.vDatePicker m_endDate;
}

}
