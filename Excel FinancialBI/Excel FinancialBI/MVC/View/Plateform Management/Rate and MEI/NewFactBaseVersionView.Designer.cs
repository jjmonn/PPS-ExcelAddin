﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class NewFactBaseVersionView<TController, TVersion> : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFactBaseVersionView));
      this.m_nameLabel = new System.Windows.Forms.Label();
      this.m_startingPeriodLabel = new System.Windows.Forms.Label();
      this.m_numberPeriodsLabel = new System.Windows.Forms.Label();
      this.NameTB = new System.Windows.Forms.TextBox();
      this.CancelBT = new System.Windows.Forms.Button();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.ValidateBT = new System.Windows.Forms.Button();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_creationBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.m_startPeriod = new System.Windows.Forms.DateTimePicker();
      this.m_nbPeriod = new VIBlend.WinForms.Controls.vNumericUpDown();
      this.SuspendLayout();
      // 
      // m_nameLabel
      // 
      this.m_nameLabel.AutoSize = true;
      this.m_nameLabel.Location = new System.Drawing.Point(28, 31);
      this.m_nameLabel.Name = "m_nameLabel";
      this.m_nameLabel.Size = new System.Drawing.Size(35, 13);
      this.m_nameLabel.TabIndex = 0;
      this.m_nameLabel.Text = "Name";
      // 
      // m_startingPeriodLabel
      // 
      this.m_startingPeriodLabel.AutoSize = true;
      this.m_startingPeriodLabel.Location = new System.Drawing.Point(28, 79);
      this.m_startingPeriodLabel.Name = "m_startingPeriodLabel";
      this.m_startingPeriodLabel.Size = new System.Drawing.Size(76, 13);
      this.m_startingPeriodLabel.TabIndex = 1;
      this.m_startingPeriodLabel.Text = "starting_period";
      // 
      // m_numberPeriodsLabel
      // 
      this.m_numberPeriodsLabel.AutoSize = true;
      this.m_numberPeriodsLabel.Location = new System.Drawing.Point(28, 121);
      this.m_numberPeriodsLabel.Name = "m_numberPeriodsLabel";
      this.m_numberPeriodsLabel.Size = new System.Drawing.Size(50, 13);
      this.m_numberPeriodsLabel.TabIndex = 2;
      this.m_numberPeriodsLabel.Text = "nb_years";
      // 
      // NameTB
      // 
      this.NameTB.Location = new System.Drawing.Point(153, 31);
      this.NameTB.Name = "NameTB";
      this.NameTB.Size = new System.Drawing.Size(160, 20);
      this.NameTB.TabIndex = 3;
      // 
      // CancelBT
      // 
      this.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CancelBT.ImageKey = "imageres_89.ico";
      this.CancelBT.ImageList = this.ButtonIcons;
      this.CancelBT.Location = new System.Drawing.Point(153, 168);
      this.CancelBT.Name = "CancelBT";
      this.CancelBT.Size = new System.Drawing.Size(86, 26);
      this.CancelBT.TabIndex = 25;
      this.CancelBT.Text = "cancel";
      this.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CancelBT.UseVisualStyleBackColor = true;
      // 
      // ButtonIcons
      // 
      this.ButtonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonIcons.ImageStream")));
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "favicon(97).ico");
      this.ButtonIcons.Images.SetKeyName(1, "imageres_99.ico");
      this.ButtonIcons.Images.SetKeyName(2, "folder 1.ico");
      this.ButtonIcons.Images.SetKeyName(3, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(4, "1420498403_340208.ico");
      // 
      // ValidateBT
      // 
      this.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ValidateBT.ImageKey = "1420498403_340208.ico";
      this.ValidateBT.ImageList = this.ButtonIcons;
      this.ValidateBT.Location = new System.Drawing.Point(273, 168);
      this.ValidateBT.Name = "ValidateBT";
      this.ValidateBT.Size = new System.Drawing.Size(86, 26);
      this.ValidateBT.TabIndex = 24;
      this.ValidateBT.Text = "Create";
      this.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ValidateBT.UseVisualStyleBackColor = true;
      // 
      // m_versionsTreeviewImageList
      // 
      this.m_versionsTreeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_versionsTreeviewImageList.ImageStream")));
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // m_startPeriod
      // 
      this.m_startPeriod.BackColor = System.Drawing.Color.White;
      this.m_startPeriod.CustomFormat = "MMM yyyy";
      this.m_startPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.m_startPeriod.Location = new System.Drawing.Point(153, 79);
      this.m_startPeriod.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
      this.m_startPeriod.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
      this.m_startPeriod.Name = "m_startPeriod";
      this.m_startPeriod.Size = new System.Drawing.Size(100, 20);
      this.m_startPeriod.TabIndex = 28;
      this.m_startPeriod.Value = new System.DateTime(2016, 12, 30, 0, 0, 0, 0);
      // 
      // m_nbPeriod
      // 
      this.m_nbPeriod.BackColor = System.Drawing.Color.White;
      this.m_nbPeriod.DropDownArrowBackgroundEnabled = true;
      this.m_nbPeriod.EnableBorderHighlight = false;
      this.m_nbPeriod.Location = new System.Drawing.Point(153, 121);
      this.m_nbPeriod.MaxLength = 1000;
      this.m_nbPeriod.Name = "m_nbPeriod";
      this.m_nbPeriod.OverrideBackColor = System.Drawing.Color.White;
      this.m_nbPeriod.OverrideBorderColor = System.Drawing.Color.Gray;
      this.m_nbPeriod.OverrideFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.m_nbPeriod.OverrideForeColor = System.Drawing.Color.Black;
      this.m_nbPeriod.Size = new System.Drawing.Size(100, 20);
      this.m_nbPeriod.TabIndex = 29;
      this.m_nbPeriod.UseThemeForeColor = true;
      this.m_nbPeriod.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // NewFactBaseVersionView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(392, 211);
      this.Controls.Add(this.m_nbPeriod);
      this.Controls.Add(this.m_startPeriod);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.ValidateBT);
      this.Controls.Add(this.NameTB);
      this.Controls.Add(this.m_numberPeriodsLabel);
      this.Controls.Add(this.m_startingPeriodLabel);
      this.Controls.Add(this.m_nameLabel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "NewFactBaseVersionView";
      this.Text = "new_version";
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    public System.Windows.Forms.Label m_nameLabel;
    public System.Windows.Forms.Label m_startingPeriodLabel;
    public System.Windows.Forms.Label m_numberPeriodsLabel;
    public System.Windows.Forms.TextBox NameTB;
    public System.Windows.Forms.Button CancelBT;
    public System.Windows.Forms.Button ValidateBT;
    public System.Windows.Forms.ImageList ButtonIcons;
    public System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    public System.ComponentModel.BackgroundWorker m_creationBackgroundWorker;
    protected System.Windows.Forms.DateTimePicker m_startPeriod;
    protected VIBlend.WinForms.Controls.vNumericUpDown m_nbPeriod;
  }
}