using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class DirectorySnapshotUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectorySnapshotUI));
		this.m_directoryLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.m_directoryTextBox = new VIBlend.WinForms.Controls.vTextBox();
		this.m_accountSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_accountSelectionComboBox = new VIBlend.WinForms.Controls.vComboBox();
		this.m_periodSelectionPanel = new System.Windows.Forms.Panel();
		this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.m_worksheetTargetName = new VIBlend.WinForms.Controls.vTextBox();
		this.m_worksheetNameLabel = new VIBlend.WinForms.Controls.vLabel();
		this.SuspendLayout();
		//
		//m_directoryLabel
		//
		this.m_directoryLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_directoryLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_directoryLabel.Ellipsis = false;
		this.m_directoryLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_directoryLabel.Location = new System.Drawing.Point(20, 19);
		this.m_directoryLabel.Multiline = true;
		this.m_directoryLabel.Name = "m_directoryLabel";
		this.m_directoryLabel.Size = new System.Drawing.Size(110, 18);
		this.m_directoryLabel.TabIndex = 0;
		this.m_directoryLabel.Text = "Directory";
		this.m_directoryLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_directoryLabel.UseMnemonics = true;
		this.m_directoryLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_directoryTextBox
		//
		this.m_directoryTextBox.BackColor = System.Drawing.Color.White;
		this.m_directoryTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_directoryTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_directoryTextBox.DefaultText = "Empty...";
		this.m_directoryTextBox.Location = new System.Drawing.Point(136, 14);
		this.m_directoryTextBox.MaxLength = 32767;
		this.m_directoryTextBox.Name = "m_directoryTextBox";
		this.m_directoryTextBox.PasswordChar = '\0';
		this.m_directoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_directoryTextBox.SelectionLength = 0;
		this.m_directoryTextBox.SelectionStart = 0;
		this.m_directoryTextBox.Size = new System.Drawing.Size(299, 23);
		this.m_directoryTextBox.TabIndex = 1;
		this.m_directoryTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_directoryTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountSelectionLabel
		//
		this.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_accountSelectionLabel.Ellipsis = false;
		this.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_accountSelectionLabel.Location = new System.Drawing.Point(20, 262);
		this.m_accountSelectionLabel.Multiline = true;
		this.m_accountSelectionLabel.Name = "m_accountSelectionLabel";
		this.m_accountSelectionLabel.Size = new System.Drawing.Size(134, 23);
		this.m_accountSelectionLabel.TabIndex = 10;
		this.m_accountSelectionLabel.Text = "Account selection";
		this.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_accountSelectionLabel.UseMnemonics = true;
		this.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountSelectionComboBox
		//
		this.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White;
		this.m_accountSelectionComboBox.DisplayMember = "";
		this.m_accountSelectionComboBox.DropDownList = true;
		this.m_accountSelectionComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_accountSelectionComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_accountSelectionComboBox.DropDownWidth = 275;
		this.m_accountSelectionComboBox.Location = new System.Drawing.Point(160, 262);
		this.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox";
		this.m_accountSelectionComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.m_accountSelectionComboBox.Size = new System.Drawing.Size(275, 23);
		this.m_accountSelectionComboBox.TabIndex = 9;
		this.m_accountSelectionComboBox.UseThemeBackColor = false;
		this.m_accountSelectionComboBox.UseThemeDropDownArrowColor = true;
		this.m_accountSelectionComboBox.ValueMember = "";
		this.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_periodSelectionPanel
		//
		this.m_periodSelectionPanel.Location = new System.Drawing.Point(20, 105);
		this.m_periodSelectionPanel.Name = "m_periodSelectionPanel";
		this.m_periodSelectionPanel.Size = new System.Drawing.Size(415, 129);
		this.m_periodSelectionPanel.TabIndex = 8;
		//
		//m_validateButton
		//
		this.m_validateButton.AllowAnimations = true;
		this.m_validateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
		this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
		this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.m_validateButton.ImageKey = "1420498403_340208.ico";
		this.m_validateButton.ImageList = this.ImageList1;
		this.m_validateButton.Location = new System.Drawing.Point(344, 305);
		this.m_validateButton.Name = "m_validateButton";
		this.m_validateButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_validateButton.Size = new System.Drawing.Size(91, 24);
		this.m_validateButton.TabIndex = 7;
		this.m_validateButton.Text = "Validate";
		this.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_validateButton.UseVisualStyleBackColor = false;
		this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico");
		//
		//m_worksheetTargetName
		//
		this.m_worksheetTargetName.BackColor = System.Drawing.Color.White;
		this.m_worksheetTargetName.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_worksheetTargetName.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_worksheetTargetName.DefaultText = "Empty...";
		this.m_worksheetTargetName.Location = new System.Drawing.Point(136, 54);
		this.m_worksheetTargetName.MaxLength = 32767;
		this.m_worksheetTargetName.Name = "m_worksheetTargetName";
		this.m_worksheetTargetName.PasswordChar = '\0';
		this.m_worksheetTargetName.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_worksheetTargetName.SelectionLength = 0;
		this.m_worksheetTargetName.SelectionStart = 0;
		this.m_worksheetTargetName.Size = new System.Drawing.Size(299, 23);
		this.m_worksheetTargetName.TabIndex = 12;
		this.m_worksheetTargetName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_worksheetTargetName.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_worksheetNameLabel
		//
		this.m_worksheetNameLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_worksheetNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_worksheetNameLabel.Ellipsis = false;
		this.m_worksheetNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_worksheetNameLabel.Location = new System.Drawing.Point(20, 59);
		this.m_worksheetNameLabel.Multiline = true;
		this.m_worksheetNameLabel.Name = "m_worksheetNameLabel";
		this.m_worksheetNameLabel.Size = new System.Drawing.Size(110, 18);
		this.m_worksheetNameLabel.TabIndex = 11;
		this.m_worksheetNameLabel.Text = "Worksheet Name";
		this.m_worksheetNameLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_worksheetNameLabel.UseMnemonics = true;
		this.m_worksheetNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//DirectorySnapshotUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(447, 349);
		this.Controls.Add(this.m_worksheetTargetName);
		this.Controls.Add(this.m_worksheetNameLabel);
		this.Controls.Add(this.m_accountSelectionLabel);
		this.Controls.Add(this.m_accountSelectionComboBox);
		this.Controls.Add(this.m_periodSelectionPanel);
		this.Controls.Add(this.m_validateButton);
		this.Controls.Add(this.m_directoryTextBox);
		this.Controls.Add(this.m_directoryLabel);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "DirectorySnapshotUI";
		this.Text = "Directory Snapshot";
		this.ResumeLayout(false);

	}
	public VIBlend.WinForms.Controls.vLabel m_directoryLabel;
	public System.Windows.Forms.FolderBrowserDialog m_FolderBrowserDialog1;
	public VIBlend.WinForms.Controls.vTextBox m_directoryTextBox;
	public VIBlend.WinForms.Controls.vLabel m_accountSelectionLabel;
	public VIBlend.WinForms.Controls.vComboBox m_accountSelectionComboBox;
	public System.Windows.Forms.Panel m_periodSelectionPanel;
	public VIBlend.WinForms.Controls.vButton m_validateButton;
	public System.Windows.Forms.ImageList ImageList1;
	public VIBlend.WinForms.Controls.vTextBox m_worksheetTargetName;
	public VIBlend.WinForms.Controls.vLabel m_worksheetNameLabel;
}

}
