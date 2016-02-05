using System;

namespace FBI.MVC.View
{
  partial class ConnectionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSidePane));
      this.m_userLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_userNameTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_passwordTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_passwordLabel = new VIBlend.WinForms.Controls.vLabel();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.m_circularProgress2 = new VIBlend.WinForms.Controls.vCircularProgressBar();
      this.ConnectionBT = new VIBlend.WinForms.Controls.vButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.m_cancelButton = new VIBlend.WinForms.Controls.vButton();
      this.Panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_userLabel
      // 
      this.m_userLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_userLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_userLabel.Ellipsis = false;
      this.m_userLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_userLabel.Location = new System.Drawing.Point(52, 55);
      this.m_userLabel.Multiline = true;
      this.m_userLabel.Name = "m_userLabel";
      this.m_userLabel.Size = new System.Drawing.Size(141, 16);
      this.m_userLabel.TabIndex = 1;
      this.m_userLabel.Text = "User ID";
      this.m_userLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_userLabel.UseMnemonics = true;
      this.m_userLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_userNameTextBox
      // 
      this.m_userNameTextBox.BackColor = System.Drawing.Color.White;
      this.m_userNameTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_userNameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_userNameTextBox.DefaultText = "";
      this.m_userNameTextBox.Location = new System.Drawing.Point(53, 90);
      this.m_userNameTextBox.MaxLength = 32767;
      this.m_userNameTextBox.Name = "m_userNameTextBox";
      this.m_userNameTextBox.PasswordChar = '\0';
      this.m_userNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_userNameTextBox.SelectionLength = 0;
      this.m_userNameTextBox.SelectionStart = 0;
      this.m_userNameTextBox.Size = new System.Drawing.Size(140, 21);
      this.m_userNameTextBox.TabIndex = 2;
      this.m_userNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_userNameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_passwordTextBox
      // 
      this.m_passwordTextBox.BackColor = System.Drawing.Color.White;
      this.m_passwordTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_passwordTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_passwordTextBox.DefaultText = "";
      this.m_passwordTextBox.Location = new System.Drawing.Point(53, 168);
      this.m_passwordTextBox.MaxLength = 32767;
      this.m_passwordTextBox.Name = "m_passwordTextBox";
      this.m_passwordTextBox.PasswordChar = '*';
      this.m_passwordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_passwordTextBox.SelectionLength = 0;
      this.m_passwordTextBox.SelectionStart = 0;
      this.m_passwordTextBox.Size = new System.Drawing.Size(140, 21);
      this.m_passwordTextBox.TabIndex = 3;
      this.m_passwordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_passwordTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_passwordLabel
      // 
      this.m_passwordLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_passwordLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_passwordLabel.Ellipsis = false;
      this.m_passwordLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_passwordLabel.Location = new System.Drawing.Point(52, 136);
      this.m_passwordLabel.Multiline = true;
      this.m_passwordLabel.Name = "m_passwordLabel";
      this.m_passwordLabel.Size = new System.Drawing.Size(141, 16);
      this.m_passwordLabel.TabIndex = 6;
      this.m_passwordLabel.Text = "Password";
      this.m_passwordLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_passwordLabel.UseMnemonics = true;
      this.m_passwordLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // Panel1
      // 
      this.Panel1.Controls.Add(this.m_circularProgress2);
      this.Panel1.Controls.Add(this.ConnectionBT);
      this.Panel1.Controls.Add(this.m_cancelButton);
      this.Panel1.Controls.Add(this.m_userLabel);
      this.Panel1.Controls.Add(this.m_userNameTextBox);
      this.Panel1.Controls.Add(this.m_passwordLabel);
      this.Panel1.Controls.Add(this.m_passwordTextBox);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel1.Location = new System.Drawing.Point(0, 0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(256, 673);
      this.Panel1.TabIndex = 14;
      // 
      // m_circularProgress2
      // 
      this.m_circularProgress2.AllowAnimations = true;
      this.m_circularProgress2.BackColor = System.Drawing.Color.Transparent;
      this.m_circularProgress2.IndicatorsCount = 10;
      this.m_circularProgress2.Location = new System.Drawing.Point(73, 256);
      this.m_circularProgress2.Maximum = 100;
      this.m_circularProgress2.Minimum = 0;
      this.m_circularProgress2.Name = "m_circularProgress2";
      this.m_circularProgress2.Size = new System.Drawing.Size(100, 88);
      this.m_circularProgress2.TabIndex = 7;
      this.m_circularProgress2.Text = "VCircularProgressBar1";
      this.m_circularProgress2.UseThemeBackground = false;
      this.m_circularProgress2.Value = 0;
      this.m_circularProgress2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.STEEL;
      // 
      // ConnectionBT
      // 
      this.ConnectionBT.AllowAnimations = true;
      this.ConnectionBT.BackColor = System.Drawing.Color.Transparent;
      this.ConnectionBT.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.ConnectionBT.ImageKey = "upload.png";
      this.ConnectionBT.ImageList = this.imageList1;
      this.ConnectionBT.Location = new System.Drawing.Point(73, 206);
      this.ConnectionBT.Name = "ConnectionBT";
      this.ConnectionBT.RoundedCornersMask = ((byte)(15));
      this.ConnectionBT.Size = new System.Drawing.Size(100, 47);
      this.ConnectionBT.TabIndex = 4;
      this.ConnectionBT.Text = "Connection";
      this.ConnectionBT.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.ConnectionBT.UseVisualStyleBackColor = false;
      this.ConnectionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.ConnectionBT.Click += new System.EventHandler(this.ConnectionBT_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "delete.ico");
      this.imageList1.Images.SetKeyName(1, "upload.png");
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.AllowAnimations = true;
      this.m_cancelButton.BackColor = System.Drawing.Color.Transparent;
      this.m_cancelButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.m_cancelButton.ImageKey = "delete.ico";
      this.m_cancelButton.ImageList = this.imageList1;
      this.m_cancelButton.Location = new System.Drawing.Point(73, 369);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.RoundedCornersMask = ((byte)(15));
      this.m_cancelButton.Size = new System.Drawing.Size(100, 47);
      this.m_cancelButton.TabIndex = 5;
      this.m_cancelButton.Text = "Cancel";
      this.m_cancelButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.m_cancelButton.UseVisualStyleBackColor = false;
      this.m_cancelButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_cancelButton.Click += new System.EventHandler(this.m_cancelButton_Click);
      // 
      // ConnectionSidePane
      // 
      this.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.ClientSize = new System.Drawing.Size(256, 673);
      this.Controls.Add(this.Panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = new System.Drawing.Point(0, 0);
      this.Name = "ConnectionSidePane";
      this.Text = "[connection.connection]";
      this.ADXBeforeTaskPaneShow += new AddinExpress.XL.ADXBeforeTaskPaneShowEventHandler(this.ConnectionSidePane_ADXBeforeTaskPaneShow);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionSidePane_FormClosing);
      this.Panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal VIBlend.WinForms.Controls.vLabel m_userLabel;
    internal VIBlend.WinForms.Controls.vTextBox m_userNameTextBox;
    internal VIBlend.WinForms.Controls.vTextBox m_passwordTextBox;
    internal VIBlend.WinForms.Controls.vLabel m_passwordLabel;
    internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
    internal System.Windows.Forms.Panel Panel1;
    internal VIBlend.WinForms.Controls.vButton ConnectionBT;
    internal VIBlend.WinForms.Controls.vButton m_cancelButton;

    internal VIBlend.WinForms.Controls.vCircularProgressBar m_circularProgress2;
    private System.Windows.Forms.ImageList imageList1;

  }

    #endregion

}


