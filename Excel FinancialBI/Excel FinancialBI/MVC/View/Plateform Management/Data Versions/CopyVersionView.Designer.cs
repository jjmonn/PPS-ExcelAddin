namespace FBI.MVC.View
{
  partial class CopyVersionView
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyVersionView));
      this.m_nbPeriods = new VIBlend.WinForms.Controls.vNumericUpDown();
      this.m_versionNameLabel = new System.Windows.Forms.Label();
      this.m_versionNameTextbox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_CancelButton = new VIBlend.WinForms.Controls.vButton();
      this.m_copyVersionButton = new VIBlend.WinForms.Controls.vButton();
      this.m_nbPeriodsLabel = new System.Windows.Forms.Label();
      this.m_startingPeriodDatePicker = new VIBlend.WinForms.Controls.vDatePicker();
      this.m_startingPeriodLabel = new System.Windows.Forms.Label();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.m_copiedVersionNameLabel = new System.Windows.Forms.Label();
      this.m_copiedVersionName = new VIBlend.WinForms.Controls.vTextBox();
      this.SuspendLayout();
      // 
      // m_nbPeriods
      // 
      this.m_nbPeriods.BackColor = System.Drawing.Color.White;
      this.m_nbPeriods.DropDownArrowBackgroundEnabled = true;
      this.m_nbPeriods.EnableBorderHighlight = false;
      this.m_nbPeriods.Location = new System.Drawing.Point(226, 176);
      this.m_nbPeriods.MaxLength = 32767;
      this.m_nbPeriods.Name = "m_nbPeriods";
      this.m_nbPeriods.OverrideBackColor = System.Drawing.Color.White;
      this.m_nbPeriods.OverrideBorderColor = System.Drawing.Color.Gray;
      this.m_nbPeriods.OverrideFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.m_nbPeriods.OverrideForeColor = System.Drawing.Color.Black;
      this.m_nbPeriods.Size = new System.Drawing.Size(279, 22);
      this.m_nbPeriods.TabIndex = 34;
      this.m_nbPeriods.UseThemeForeColor = true;
      this.m_nbPeriods.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_versionNameLabel
      // 
      this.m_versionNameLabel.AutoSize = true;
      this.m_versionNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_versionNameLabel.Location = new System.Drawing.Point(29, 80);
      this.m_versionNameLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_versionNameLabel.Name = "m_versionNameLabel";
      this.m_versionNameLabel.Size = new System.Drawing.Size(45, 15);
      this.m_versionNameLabel.TabIndex = 39;
      this.m_versionNameLabel.Text = "Name";
      // 
      // m_versionNameTextbox
      // 
      this.m_versionNameTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_versionNameTextbox.BackColor = System.Drawing.Color.White;
      this.m_versionNameTextbox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_versionNameTextbox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_versionNameTextbox.DefaultText = "";
      this.m_versionNameTextbox.Location = new System.Drawing.Point(225, 80);
      this.m_versionNameTextbox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.m_versionNameTextbox.MaximumSize = new System.Drawing.Size(400, 4);
      this.m_versionNameTextbox.MaxLength = 32767;
      this.m_versionNameTextbox.MinimumSize = new System.Drawing.Size(280, 20);
      this.m_versionNameTextbox.Name = "m_versionNameTextbox";
      this.m_versionNameTextbox.PasswordChar = '\0';
      this.m_versionNameTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_versionNameTextbox.SelectionLength = 0;
      this.m_versionNameTextbox.SelectionStart = 0;
      this.m_versionNameTextbox.Size = new System.Drawing.Size(280, 20);
      this.m_versionNameTextbox.TabIndex = 31;
      this.m_versionNameTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_versionNameTextbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_CancelButton
      // 
      this.m_CancelButton.AllowAnimations = true;
      this.m_CancelButton.BackColor = System.Drawing.Color.Transparent;
      this.m_CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_CancelButton.ImageKey = "imageres_89.ico";
      this.m_CancelButton.ImageList = this.ButtonIcons;
      this.m_CancelButton.Location = new System.Drawing.Point(226, 233);
      this.m_CancelButton.Name = "m_CancelButton";
      this.m_CancelButton.RoundedCornersMask = ((byte)(15));
      this.m_CancelButton.Size = new System.Drawing.Size(107, 30);
      this.m_CancelButton.TabIndex = 38;
      this.m_CancelButton.Text = "Cancel";
      this.m_CancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_CancelButton.UseVisualStyleBackColor = true;
      this.m_CancelButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_copyVersionButton
      // 
      this.m_copyVersionButton.AllowAnimations = true;
      this.m_copyVersionButton.BackColor = System.Drawing.Color.Transparent;
      this.m_copyVersionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_copyVersionButton.ImageKey = "1420498403_340208.ico";
      this.m_copyVersionButton.ImageList = this.ButtonIcons;
      this.m_copyVersionButton.Location = new System.Drawing.Point(398, 233);
      this.m_copyVersionButton.Name = "m_copyVersionButton";
      this.m_copyVersionButton.RoundedCornersMask = ((byte)(15));
      this.m_copyVersionButton.Size = new System.Drawing.Size(107, 30);
      this.m_copyVersionButton.TabIndex = 37;
      this.m_copyVersionButton.Text = "Copy";
      this.m_copyVersionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_copyVersionButton.UseVisualStyleBackColor = true;
      this.m_copyVersionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_nbPeriodsLabel
      // 
      this.m_nbPeriodsLabel.AutoSize = true;
      this.m_nbPeriodsLabel.Location = new System.Drawing.Point(29, 176);
      this.m_nbPeriodsLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_nbPeriodsLabel.Name = "m_nbPeriodsLabel";
      this.m_nbPeriodsLabel.Size = new System.Drawing.Size(109, 15);
      this.m_nbPeriodsLabel.TabIndex = 42;
      this.m_nbPeriodsLabel.Text = "Number of periods";
      // 
      // m_startingPeriodDatePicker
      // 
      this.m_startingPeriodDatePicker.BackColor = System.Drawing.Color.White;
      this.m_startingPeriodDatePicker.BorderColor = System.Drawing.Color.Black;
      this.m_startingPeriodDatePicker.Culture = new System.Globalization.CultureInfo("");
      this.m_startingPeriodDatePicker.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_startingPeriodDatePicker.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_startingPeriodDatePicker.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None;
      this.m_startingPeriodDatePicker.FormatValue = "dd MMM yyyy";
      this.m_startingPeriodDatePicker.Location = new System.Drawing.Point(225, 131);
      this.m_startingPeriodDatePicker.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
      this.m_startingPeriodDatePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
      this.m_startingPeriodDatePicker.Name = "m_startingPeriodDatePicker";
      this.m_startingPeriodDatePicker.ShowGrip = false;
      this.m_startingPeriodDatePicker.Size = new System.Drawing.Size(280, 23);
      this.m_startingPeriodDatePicker.TabIndex = 33;
      this.m_startingPeriodDatePicker.Text = "VDatePicker1";
      this.m_startingPeriodDatePicker.UseThemeBackColor = false;
      this.m_startingPeriodDatePicker.UseThemeDropDownArrowColor = true;
      this.m_startingPeriodDatePicker.Value = new System.DateTime(2015, 12, 11, 9, 57, 35, 808);
      this.m_startingPeriodDatePicker.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_startingPeriodLabel
      // 
      this.m_startingPeriodLabel.AutoSize = true;
      this.m_startingPeriodLabel.Location = new System.Drawing.Point(29, 131);
      this.m_startingPeriodLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_startingPeriodLabel.Name = "m_startingPeriodLabel";
      this.m_startingPeriodLabel.Size = new System.Drawing.Size(59, 15);
      this.m_startingPeriodLabel.TabIndex = 41;
      this.m_startingPeriodLabel.Text = "Start date";
      // 
      // ButtonIcons
      // 
      this.ButtonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonIcons.ImageStream")));
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "favicon(81) (1).ico");
      this.ButtonIcons.Images.SetKeyName(1, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(2, "1420498403_340208.ico");
      // 
      // m_copiedVersionNameLabel
      // 
      this.m_copiedVersionNameLabel.AutoSize = true;
      this.m_copiedVersionNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.764706F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_copiedVersionNameLabel.Location = new System.Drawing.Point(29, 36);
      this.m_copiedVersionNameLabel.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
      this.m_copiedVersionNameLabel.Name = "m_copiedVersionNameLabel";
      this.m_copiedVersionNameLabel.Size = new System.Drawing.Size(62, 15);
      this.m_copiedVersionNameLabel.TabIndex = 43;
      this.m_copiedVersionNameLabel.Text = "Copy from";
      // 
      // m_copiedVersionName
      // 
      this.m_copiedVersionName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_copiedVersionName.BackColor = System.Drawing.Color.White;
      this.m_copiedVersionName.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_copiedVersionName.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_copiedVersionName.DefaultText = "";
      this.m_copiedVersionName.Enabled = false;
      this.m_copiedVersionName.Location = new System.Drawing.Point(226, 36);
      this.m_copiedVersionName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
      this.m_copiedVersionName.MaximumSize = new System.Drawing.Size(400, 4);
      this.m_copiedVersionName.MaxLength = 32767;
      this.m_copiedVersionName.MinimumSize = new System.Drawing.Size(280, 20);
      this.m_copiedVersionName.Name = "m_copiedVersionName";
      this.m_copiedVersionName.PasswordChar = '\0';
      this.m_copiedVersionName.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_copiedVersionName.SelectionLength = 0;
      this.m_copiedVersionName.SelectionStart = 0;
      this.m_copiedVersionName.Size = new System.Drawing.Size(280, 20);
      this.m_copiedVersionName.TabIndex = 44;
      this.m_copiedVersionName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_copiedVersionName.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // CopyVersionView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(559, 295);
      this.Controls.Add(this.m_copiedVersionName);
      this.Controls.Add(this.m_copiedVersionNameLabel);
      this.Controls.Add(this.m_startingPeriodDatePicker);
      this.Controls.Add(this.m_nbPeriods);
      this.Controls.Add(this.m_versionNameLabel);
      this.Controls.Add(this.m_versionNameTextbox);
      this.Controls.Add(this.m_CancelButton);
      this.Controls.Add(this.m_copyVersionButton);
      this.Controls.Add(this.m_nbPeriodsLabel);
      this.Controls.Add(this.m_startingPeriodLabel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "CopyVersionView";
      this.Text = "Version Copy";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public VIBlend.WinForms.Controls.vNumericUpDown m_nbPeriods;
    public System.Windows.Forms.Label m_versionNameLabel;
    public VIBlend.WinForms.Controls.vTextBox m_versionNameTextbox;
    public VIBlend.WinForms.Controls.vButton m_CancelButton;
    public VIBlend.WinForms.Controls.vButton m_copyVersionButton;
    public System.Windows.Forms.Label m_nbPeriodsLabel;
    public VIBlend.WinForms.Controls.vDatePicker m_startingPeriodDatePicker;
    public System.Windows.Forms.Label m_startingPeriodLabel;
    public System.Windows.Forms.ImageList ButtonIcons;
    public System.Windows.Forms.Label m_copiedVersionNameLabel;
    public VIBlend.WinForms.Controls.vTextBox m_copiedVersionName;
  }
}