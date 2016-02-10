using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class NewGlobalFactUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGlobalFactUI));
      this.m_nameLabel = new System.Windows.Forms.Label();
      this.NameTB = new System.Windows.Forms.TextBox();
      this.CancelBT = new System.Windows.Forms.Button();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.ValidateBT = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      //m_nameLabel
      //
      this.m_nameLabel.AutoSize = true;
      this.m_nameLabel.Location = new System.Drawing.Point(46, 49);
      this.m_nameLabel.Name = "m_nameLabel";
      this.m_nameLabel.Size = new System.Drawing.Size(35, 13);
      this.m_nameLabel.TabIndex = 0;
      this.m_nameLabel.Text = "Name";
      //
      //NameTB
      //
      this.NameTB.Location = new System.Drawing.Point(171, 49);
      this.NameTB.Name = "NameTB";
      this.NameTB.Size = new System.Drawing.Size(160, 20);
      this.NameTB.TabIndex = 3;
      //
      //CancelBT
      //
      this.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CancelBT.ImageKey = "imageres_89.ico";
      this.CancelBT.ImageList = this.ButtonIcons;
      this.CancelBT.Location = new System.Drawing.Point(49, 111);
      this.CancelBT.Name = "CancelBT";
      this.CancelBT.Size = new System.Drawing.Size(86, 26);
      this.CancelBT.TabIndex = 25;
      this.CancelBT.Text = "Cancel";
      this.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CancelBT.UseVisualStyleBackColor = true;
      //
      //ButtonIcons
      //
      this.ButtonIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonIcons.ImageStream");
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "favicon(97).ico");
      this.ButtonIcons.Images.SetKeyName(1, "imageres_99.ico");
      this.ButtonIcons.Images.SetKeyName(2, "folder 1.ico");
      this.ButtonIcons.Images.SetKeyName(3, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(4, "1420498403_340208.ico");
      //
      //ValidateBT
      //
      this.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ValidateBT.ImageKey = "1420498403_340208.ico";
      this.ValidateBT.ImageList = this.ButtonIcons;
      this.ValidateBT.Location = new System.Drawing.Point(245, 111);
      this.ValidateBT.Name = "ValidateBT";
      this.ValidateBT.Size = new System.Drawing.Size(86, 26);
      this.ValidateBT.TabIndex = 24;
      this.ValidateBT.Text = "Create";
      this.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ValidateBT.UseVisualStyleBackColor = true;
      //
      //NewGlobalFactUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(394, 156);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.ValidateBT);
      this.Controls.Add(this.NameTB);
      this.Controls.Add(this.m_nameLabel);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "NewGlobalFactUI";
      this.Text = "New_global_fact";
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    public System.Windows.Forms.Label m_nameLabel;
    public System.Windows.Forms.TextBox NameTB;
    public System.Windows.Forms.Button CancelBT;
    public System.Windows.Forms.Button ValidateBT;
    public System.Windows.Forms.ImageList ButtonIcons;
  }
}