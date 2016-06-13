using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class VersionSelectionUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionSelectionUI));
      this.VersioningTVIL = new System.Windows.Forms.ImageList(this.components);
      this.ImageList2 = new System.Windows.Forms.ImageList(this.components);
      this.ValidateButton = new System.Windows.Forms.Button();
      this.VersionsTreeComboBox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.SuspendLayout();
      //
      //VersioningTVIL
      //
      this.VersioningTVIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("VersioningTVIL.ImageStream");
      this.VersioningTVIL.TransparentColor = System.Drawing.Color.Transparent;
      this.VersioningTVIL.Images.SetKeyName(0, "icons-blue.png");
      this.VersioningTVIL.Images.SetKeyName(1, "DB Grey.png");
      //
      //ImageList2
      //
      this.ImageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList2.ImageStream");
      this.ImageList2.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList2.Images.SetKeyName(0, "favicon(16).ico");
      this.ImageList2.Images.SetKeyName(1, "favicon(25).ico");
      this.ImageList2.Images.SetKeyName(2, "favicon(28).ico");
      this.ImageList2.Images.SetKeyName(3, "favicon(76).ico");
      //
      //ValidateButton
      //
      this.ValidateButton.FlatAppearance.BorderSize = 0;
      this.ValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ValidateButton.ImageKey = "favicon(76).ico";
      this.ValidateButton.ImageList = this.ImageList2;
      this.ValidateButton.Location = new System.Drawing.Point(239, 72);
      this.ValidateButton.Name = "ValidateButton";
      this.ValidateButton.Size = new System.Drawing.Size(91, 30);
      this.ValidateButton.TabIndex = 17;
      this.ValidateButton.Text = "Validate";
      this.ValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ValidateButton.UseVisualStyleBackColor = true;
      //
      //VersionsTreeComboBox
      //
      this.VersionsTreeComboBox.BackColor = System.Drawing.Color.White;
      this.VersionsTreeComboBox.BorderColor = System.Drawing.Color.Black;
      this.VersionsTreeComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.VersionsTreeComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.VersionsTreeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.VersionsTreeComboBox.Location = new System.Drawing.Point(21, 24);
      this.VersionsTreeComboBox.Name = "VersionsTreeComboBox";
      this.VersionsTreeComboBox.Size = new System.Drawing.Size(307, 25);
      this.VersionsTreeComboBox.TabIndex = 18;
      this.VersionsTreeComboBox.Text = "Select version";
      this.VersionsTreeComboBox.UseThemeBackColor = false;
      this.VersionsTreeComboBox.UseThemeDropDownArrowColor = true;
      this.VersionsTreeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //VersionSelectionUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(344, 156);
      this.Controls.Add(this.VersionsTreeComboBox);
      this.Controls.Add(this.ValidateButton);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "VersionSelectionUI";
      this.Text = "Select a version";
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.ImageList VersioningTVIL;
    public System.Windows.Forms.Button ValidateButton;
    public System.Windows.Forms.ImageList ImageList2;
    public VIBlend.WinForms.Controls.vTreeViewBox VersionsTreeComboBox;
  }

}