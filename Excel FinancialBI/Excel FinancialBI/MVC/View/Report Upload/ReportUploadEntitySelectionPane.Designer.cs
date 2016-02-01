using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class ReportUploadEntitySelectionPane : AddinExpress.XL.ADXExcelTaskPane
{

	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode()]
	protected override void Dispose(bool disposing)
	{
		if (disposing) {
			if ((components != null)) {
				components.Dispose();
			}
		}
		base.Dispose(disposing);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportUploadEntitySelectionPane));
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.EntitiesTVImageList = new System.Windows.Forms.ImageList(this.components);
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.m_entitySelectionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_periodsSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_accountSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_accountSelectionComboBox = new VIBlend.WinForms.Controls.vComboBox();
		this.m_entitiesTV = new VIBlend.WinForms.Controls.vTreeView();
		this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
		this.m_periodsSelectionPanel = new System.Windows.Forms.Panel();
		this.TableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico");
		//
		//EntitiesTVImageList
		//
		this.EntitiesTVImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EntitiesTVImageList.ImageStream");
		this.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico");
		this.EntitiesTVImageList.Images.SetKeyName(1, "cloud_dark.ico");
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Controls.Add(this.m_entitySelectionLabel, 0, 0);
		this.TableLayoutPanel1.Controls.Add(this.m_periodsSelectionLabel, 0, 2);
		this.TableLayoutPanel1.Controls.Add(this.m_accountSelectionLabel, 0, 4);
		this.TableLayoutPanel1.Controls.Add(this.m_accountSelectionComboBox, 0, 5);
		this.TableLayoutPanel1.Controls.Add(this.m_entitiesTV, 0, 1);
		this.TableLayoutPanel1.Controls.Add(this.m_validateButton, 0, 6);
		this.TableLayoutPanel1.Controls.Add(this.m_periodsSelectionPanel, 0, 3);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 7;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 203f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(259, 685);
		this.TableLayoutPanel1.TabIndex = 6;
		//
		//m_entitySelectionLabel
		//
		this.m_entitySelectionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_entitySelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_entitySelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_entitySelectionLabel.Ellipsis = false;
		this.m_entitySelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_entitySelectionLabel.Location = new System.Drawing.Point(3, 3);
		this.m_entitySelectionLabel.Multiline = true;
		this.m_entitySelectionLabel.Name = "m_entitySelectionLabel";
		this.m_entitySelectionLabel.Size = new System.Drawing.Size(253, 18);
		this.m_entitySelectionLabel.TabIndex = 4;
		this.m_entitySelectionLabel.Text = "Entity selection";
		this.m_entitySelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_entitySelectionLabel.UseMnemonics = true;
		this.m_entitySelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_periodsSelectionLabel
		//
		this.m_periodsSelectionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_periodsSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_periodsSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_periodsSelectionLabel.Ellipsis = false;
		this.m_periodsSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_periodsSelectionLabel.Location = new System.Drawing.Point(3, 331);
		this.m_periodsSelectionLabel.Multiline = true;
		this.m_periodsSelectionLabel.Name = "m_periodsSelectionLabel";
		this.m_periodsSelectionLabel.Size = new System.Drawing.Size(253, 18);
		this.m_periodsSelectionLabel.TabIndex = 3;
		this.m_periodsSelectionLabel.Text = "Periods selection";
		this.m_periodsSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_periodsSelectionLabel.UseMnemonics = true;
		this.m_periodsSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountSelectionLabel
		//
		this.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_accountSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_accountSelectionLabel.Ellipsis = false;
		this.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_accountSelectionLabel.Location = new System.Drawing.Point(3, 558);
		this.m_accountSelectionLabel.Multiline = true;
		this.m_accountSelectionLabel.Name = "m_accountSelectionLabel";
		this.m_accountSelectionLabel.Size = new System.Drawing.Size(253, 21);
		this.m_accountSelectionLabel.TabIndex = 5;
		this.m_accountSelectionLabel.Text = "Account selection";
		this.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_accountSelectionLabel.UseMnemonics = true;
		this.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountSelectionComboBox
		//
		this.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White;
		this.m_accountSelectionComboBox.DisplayMember = "";
		this.m_accountSelectionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_accountSelectionComboBox.DropDownList = true;
		this.m_accountSelectionComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_accountSelectionComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_accountSelectionComboBox.DropDownWidth = 253;
		this.m_accountSelectionComboBox.Location = new System.Drawing.Point(3, 585);
		this.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox";
		this.m_accountSelectionComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.m_accountSelectionComboBox.Size = new System.Drawing.Size(253, 23);
		this.m_accountSelectionComboBox.TabIndex = 2;
		this.m_accountSelectionComboBox.UseThemeBackColor = false;
		this.m_accountSelectionComboBox.UseThemeDropDownArrowColor = true;
		this.m_accountSelectionComboBox.ValueMember = "";
		this.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_entitiesTV
		//
		this.m_entitiesTV.AccessibleName = "TreeView";
		this.m_entitiesTV.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
		this.m_entitiesTV.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_entitiesTV.ItemHeight = 17;
		this.m_entitiesTV.Location = new System.Drawing.Point(3, 27);
		this.m_entitiesTV.Name = "m_entitiesTV";
		this.m_entitiesTV.ScrollPosition = new System.Drawing.Point(0, 0);
		this.m_entitiesTV.SelectedNode = null;
		this.m_entitiesTV.Size = new System.Drawing.Size(253, 298);
		this.m_entitiesTV.TabIndex = 6;
		this.m_entitiesTV.Text = "VTreeView1";
		this.m_entitiesTV.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK;
		this.m_entitiesTV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK;
		//
		//m_validateButton
		//
		this.m_validateButton.AllowAnimations = true;
		this.m_validateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
		this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
		this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.m_validateButton.ImageKey = "1420498403_340208.ico";
		this.m_validateButton.ImageList = this.ImageList1;
		this.m_validateButton.Location = new System.Drawing.Point(3, 658);
		this.m_validateButton.Name = "m_validateButton";
		this.m_validateButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_validateButton.Size = new System.Drawing.Size(98, 24);
		this.m_validateButton.TabIndex = 7;
		this.m_validateButton.Text = "Validate";
		this.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_validateButton.UseVisualStyleBackColor = false;
		this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_periodsSelectionPanel
		//
		this.m_periodsSelectionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_periodsSelectionPanel.Location = new System.Drawing.Point(3, 355);
		this.m_periodsSelectionPanel.Name = "m_periodsSelectionPanel";
		this.m_periodsSelectionPanel.Size = new System.Drawing.Size(253, 197);
		this.m_periodsSelectionPanel.TabIndex = 8;
		//
		//ReportUploadEntitySelectionPane
		//
		this.BackColor = System.Drawing.SystemColors.GrayText;
		this.ClientSize = new System.Drawing.Size(259, 685);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Location = new System.Drawing.Point(0, 0);
		this.Name = "ReportUploadEntitySelectionPane";
		this.Text = "Data Edition";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal System.Windows.Forms.ImageList ImageList1;
	internal System.Windows.Forms.ImageList EntitiesTVImageList;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal VIBlend.WinForms.Controls.vLabel m_entitySelectionLabel;
	internal VIBlend.WinForms.Controls.vLabel m_periodsSelectionLabel;
	internal VIBlend.WinForms.Controls.vLabel m_accountSelectionLabel;
	internal VIBlend.WinForms.Controls.vComboBox m_accountSelectionComboBox;
	internal VIBlend.WinForms.Controls.vTreeView m_entitiesTV;
	internal VIBlend.WinForms.Controls.vButton m_validateButton;

	internal System.Windows.Forms.Panel m_periodsSelectionPanel;
}

}
