// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.TrialForm
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities.Properties;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class TrialForm : Form
  {
    private Timer timer = new Timer();
    private string controlName;
    /// <summary>Required designer variable.</summary>
    private IContainer components;
    private Button button1;
    private PictureBox pictureBox1;
    private Label labelEvalText;
    private Label label2;
    private Label label3;
    private LinkLabel linkLabel1;

    public string ControlName
    {
      set
      {
        this.controlName = value;
        this.labelEvalText.Text = "You are using an Evaluation Version of VIBlend " + this.controlName;
        this.labelEvalText.Refresh();
      }
    }

    public TrialForm()
    {
      this.InitializeComponent();
      this.button1.Enabled = false;
      this.button1.Refresh();
      this.timer.Interval = 1000;
      this.timer.Start();
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      this.timer.Dispose();
      this.button1.Enabled = true;
      this.button1.Refresh();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://www.viblend.com");
    }

    /// <summary>Clean up any resources being used.</summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TrialForm));
      this.button1 = new Button();
      this.labelEvalText = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.button1.BackColor = Color.DodgerBlue;
      this.button1.FlatAppearance.BorderColor = Color.Blue;
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.ForeColor = Color.White;
      this.button1.Location = new Point(265, 202);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Ok";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.labelEvalText.ForeColor = Color.White;
      this.labelEvalText.Location = new Point(23, 63);
      this.labelEvalText.Name = "labelEvalText";
      this.labelEvalText.Size = new Size(316, 41);
      this.labelEvalText.TabIndex = 2;
      this.labelEvalText.Text = "You are using an Evaluation Version of VIBlend";
      this.label2.ForeColor = Color.White;
      this.label2.Location = new Point(22, 112);
      this.label2.Name = "label2";
      this.label2.Size = new Size(322, 40);
      this.label2.TabIndex = 3;
      this.label2.Text = "The EULA allows you to use this unlicensed version of the software only for pre-purchase Evaluation purposes.";
      this.label3.ForeColor = Color.White;
      this.label3.Location = new Point(23, 168);
      this.label3.Name = "label3";
      this.label3.Size = new Size(193, 18);
      this.label3.TabIndex = 4;
      this.label3.Text = "You can purchase a licensed copy at:";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.linkLabel1.LinkColor = Color.FromArgb(192, 192, (int) byte.MaxValue);
      this.linkLabel1.Location = new Point(23, 184);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(160, 24);
      this.linkLabel1.TabIndex = 5;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "www.viblend.com";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.pictureBox1.Image = (Image) Resources.WinformsLogo21;
      this.pictureBox1.Location = new Point(10, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(338, 50);
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Black;
      this.ClientSize = new Size(352, 237);
      this.ControlBox = false;
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.labelEvalText);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.button1);
      this.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Pixel);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TrialForm";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "VIBlend Controls Trial Version";
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
