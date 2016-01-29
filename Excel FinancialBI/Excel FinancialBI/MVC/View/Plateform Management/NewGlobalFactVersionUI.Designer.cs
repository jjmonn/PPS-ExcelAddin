using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class NewGlobalFactVersionUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGlobalFactVersionUI));
      this.m_nameLabel = new System.Windows.Forms.Label();
      this.m_startingPeriodLabel = new System.Windows.Forms.Label();
      this.m_numberPeriodsLabel = new System.Windows.Forms.Label();
      this.NameTB = new System.Windows.Forms.TextBox();
      this.StartPeriodNUD = new System.Windows.Forms.NumericUpDown();
      this.m_nb_years = new System.Windows.Forms.NumericUpDown();
      this.CancelBT = new System.Windows.Forms.Button();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.ValidateBT = new System.Windows.Forms.Button();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_circularProgress = new VIBlend.WinForms.Controls.vCircularProgressBar();
      this.m_creationBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      ((System.ComponentModel.ISupportInitialize)this.StartPeriodNUD).BeginInit();
      ((System.ComponentModel.ISupportInitialize)this.m_nb_years).BeginInit();
      this.SuspendLayout();
      //
      //m_nameLabel
      //
      this.m_nameLabel.AutoSize = true;
      this.m_nameLabel.Location = new System.Drawing.Point(28, 31);
      this.m_nameLabel.Name = "m_nameLabel";
      this.m_nameLabel.Size = new System.Drawing.Size(35, 13);
      this.m_nameLabel.TabIndex = 0;
      this.m_nameLabel.Text = "Name";
      //
      //m_startingPeriodLabel
      //
      this.m_startingPeriodLabel.AutoSize = true;
      this.m_startingPeriodLabel.Location = new System.Drawing.Point(28, 79);
      this.m_startingPeriodLabel.Name = "m_startingPeriodLabel";
      this.m_startingPeriodLabel.Size = new System.Drawing.Size(76, 13);
      this.m_startingPeriodLabel.TabIndex = 1;
      this.m_startingPeriodLabel.Text = "starting_period";
      //
      //m_numberPeriodsLabel
      //
      this.m_numberPeriodsLabel.AutoSize = true;
      this.m_numberPeriodsLabel.Location = new System.Drawing.Point(28, 121);
      this.m_numberPeriodsLabel.Name = "m_numberPeriodsLabel";
      this.m_numberPeriodsLabel.Size = new System.Drawing.Size(50, 13);
      this.m_numberPeriodsLabel.TabIndex = 2;
      this.m_numberPeriodsLabel.Text = "nb_years";
      //
      //NameTB
      //
      this.NameTB.Location = new System.Drawing.Point(153, 31);
      this.NameTB.Name = "NameTB";
      this.NameTB.Size = new System.Drawing.Size(160, 20);
      this.NameTB.TabIndex = 3;
      //
      //StartPeriodNUD
      //
      this.StartPeriodNUD.Location = new System.Drawing.Point(153, 77);
      this.StartPeriodNUD.Maximum = new decimal(new int[] {
			3000,
			0,
			0,
			0
		});
      this.StartPeriodNUD.Name = "StartPeriodNUD";
      this.StartPeriodNUD.Size = new System.Drawing.Size(100, 20);
      this.StartPeriodNUD.TabIndex = 4;
      //
      //m_nb_years
      //
      this.m_nb_years.Location = new System.Drawing.Point(153, 119);
      this.m_nb_years.Maximum = new decimal(new int[] {
			70,
			0,
			0,
			0
		});
      this.m_nb_years.Name = "m_nb_years";
      this.m_nb_years.Size = new System.Drawing.Size(100, 20);
      this.m_nb_years.TabIndex = 5;
      //
      //CancelBT
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
      this.ValidateBT.Location = new System.Drawing.Point(273, 168);
      this.ValidateBT.Name = "ValidateBT";
      this.ValidateBT.Size = new System.Drawing.Size(86, 26);
      this.ValidateBT.TabIndex = 24;
      this.ValidateBT.Text = "Create";
      this.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ValidateBT.UseVisualStyleBackColor = true;
      //
      //m_versionsTreeviewImageList
      //
      this.m_versionsTreeviewImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("m_versionsTreeviewImageList.ImageStream");
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      //
      //m_circularProgress
      //
      this.m_circularProgress.AllowAnimations = true;
      this.m_circularProgress.BackColor = System.Drawing.Color.Transparent;
      this.m_circularProgress.IndicatorsCount = 8;
      this.m_circularProgress.Location = new System.Drawing.Point(159, 68);
      this.m_circularProgress.Maximum = 100;
      this.m_circularProgress.Minimum = 0;
      this.m_circularProgress.Name = "m_circularProgress";
      this.m_circularProgress.Size = new System.Drawing.Size(75, 75);
      this.m_circularProgress.TabIndex = 27;
      this.m_circularProgress.Text = "VCircularProgressBar1";
      this.m_circularProgress.UseThemeBackground = false;
      this.m_circularProgress.Value = 0;
      this.m_circularProgress.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLUE;
      //
      //NewGlobalFactVersionUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(392, 211);
      this.Controls.Add(this.m_circularProgress);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.ValidateBT);
      this.Controls.Add(this.m_nb_years);
      this.Controls.Add(this.StartPeriodNUD);
      this.Controls.Add(this.NameTB);
      this.Controls.Add(this.m_numberPeriodsLabel);
      this.Controls.Add(this.m_startingPeriodLabel);
      this.Controls.Add(this.m_nameLabel);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "NewGlobalFactVersionUI";
      this.Text = "new_version";
      ((System.ComponentModel.ISupportInitialize)this.StartPeriodNUD).EndInit();
      ((System.ComponentModel.ISupportInitialize)this.m_nb_years).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    internal System.Windows.Forms.Label m_nameLabel;
    internal System.Windows.Forms.Label m_startingPeriodLabel;
    internal System.Windows.Forms.Label m_numberPeriodsLabel;
    internal System.Windows.Forms.TextBox NameTB;
    internal System.Windows.Forms.NumericUpDown StartPeriodNUD;
    internal System.Windows.Forms.NumericUpDown m_nb_years;
    internal System.Windows.Forms.Button CancelBT;
    internal System.Windows.Forms.Button ValidateBT;
    internal System.Windows.Forms.ImageList ButtonIcons;
    internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
    internal VIBlend.WinForms.Controls.vCircularProgressBar m_circularProgress;
    internal System.ComponentModel.BackgroundWorker m_creationBackgroundWorker;
  }
}