namespace FBI.MVC.View
{
  partial class PeriodsComparisonSelectionView
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeriodsComparisonSelectionView));
      this.m_version1Label = new VIBlend.WinForms.Controls.vLabel();
      this.m_period1Label = new VIBlend.WinForms.Controls.vLabel();
      this.m_groupbox1 = new VIBlend.WinForms.Controls.vGroupBox();
      this.m_period1CB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_groupbox2 = new VIBlend.WinForms.Controls.vGroupBox();
      this.m_period2CB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_version2Label = new VIBlend.WinForms.Controls.vLabel();
      this.m_period2Label = new VIBlend.WinForms.Controls.vLabel();
      this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
      this.m_groupbox1.SuspendLayout();
      this.m_groupbox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_version1Label
      // 
      this.m_version1Label.BackColor = System.Drawing.Color.Transparent;
      this.m_version1Label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_version1Label.Ellipsis = false;
      this.m_version1Label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_version1Label.Location = new System.Drawing.Point(6, 27);
      this.m_version1Label.Multiline = true;
      this.m_version1Label.Name = "m_version1Label";
      this.m_version1Label.Size = new System.Drawing.Size(177, 44);
      this.m_version1Label.TabIndex = 0;
      this.m_version1Label.Text = "Version";
      this.m_version1Label.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_version1Label.UseMnemonics = true;
      this.m_version1Label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_period1Label
      // 
      this.m_period1Label.BackColor = System.Drawing.Color.Transparent;
      this.m_period1Label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_period1Label.Ellipsis = false;
      this.m_period1Label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_period1Label.Location = new System.Drawing.Point(6, 103);
      this.m_period1Label.Multiline = true;
      this.m_period1Label.Name = "m_period1Label";
      this.m_period1Label.Size = new System.Drawing.Size(80, 20);
      this.m_period1Label.TabIndex = 5;
      this.m_period1Label.Text = "Period";
      this.m_period1Label.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_period1Label.UseMnemonics = true;
      this.m_period1Label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_groupbox1
      // 
      this.m_groupbox1.BackColor = System.Drawing.Color.Transparent;
      this.m_groupbox1.Controls.Add(this.m_period1CB);
      this.m_groupbox1.Controls.Add(this.m_version1Label);
      this.m_groupbox1.Controls.Add(this.m_period1Label);
      this.m_groupbox1.Location = new System.Drawing.Point(17, 27);
      this.m_groupbox1.Name = "m_groupbox1";
      this.m_groupbox1.Size = new System.Drawing.Size(189, 171);
      this.m_groupbox1.TabIndex = 8;
      this.m_groupbox1.TabStop = false;
      this.m_groupbox1.Text = "Column 1";
      this.m_groupbox1.UseThemeBorderColor = true;
      this.m_groupbox1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_period1CB
      // 
      this.m_period1CB.BackColor = System.Drawing.Color.White;
      this.m_period1CB.DisplayMember = "";
      this.m_period1CB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_period1CB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_period1CB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_period1CB.DropDownWidth = 177;
      this.m_period1CB.Location = new System.Drawing.Point(6, 129);
      this.m_period1CB.Name = "m_period1CB";
      this.m_period1CB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_period1CB.Size = new System.Drawing.Size(177, 23);
      this.m_period1CB.TabIndex = 11;
      this.m_period1CB.Text = "vComboBox1";
      this.m_period1CB.UseThemeBackColor = false;
      this.m_period1CB.UseThemeDropDownArrowColor = true;
      this.m_period1CB.ValueMember = "";
      this.m_period1CB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_period1CB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_groupbox2
      // 
      this.m_groupbox2.BackColor = System.Drawing.Color.Transparent;
      this.m_groupbox2.Controls.Add(this.m_period2CB);
      this.m_groupbox2.Controls.Add(this.m_version2Label);
      this.m_groupbox2.Controls.Add(this.m_period2Label);
      this.m_groupbox2.Location = new System.Drawing.Point(227, 27);
      this.m_groupbox2.Name = "m_groupbox2";
      this.m_groupbox2.Size = new System.Drawing.Size(189, 171);
      this.m_groupbox2.TabIndex = 9;
      this.m_groupbox2.TabStop = false;
      this.m_groupbox2.Text = "Column 2";
      this.m_groupbox2.UseThemeBorderColor = true;
      this.m_groupbox2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_period2CB
      // 
      this.m_period2CB.BackColor = System.Drawing.Color.White;
      this.m_period2CB.DisplayMember = "";
      this.m_period2CB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_period2CB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_period2CB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_period2CB.DropDownWidth = 177;
      this.m_period2CB.Location = new System.Drawing.Point(6, 129);
      this.m_period2CB.Name = "m_period2CB";
      this.m_period2CB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_period2CB.Size = new System.Drawing.Size(177, 23);
      this.m_period2CB.TabIndex = 12;
      this.m_period2CB.Text = "vComboBox2";
      this.m_period2CB.UseThemeBackColor = false;
      this.m_period2CB.UseThemeDropDownArrowColor = true;
      this.m_period2CB.ValueMember = "";
      this.m_period2CB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_period2CB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_version2Label
      // 
      this.m_version2Label.BackColor = System.Drawing.Color.Transparent;
      this.m_version2Label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_version2Label.Ellipsis = false;
      this.m_version2Label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_version2Label.Location = new System.Drawing.Point(6, 27);
      this.m_version2Label.Multiline = true;
      this.m_version2Label.Name = "m_version2Label";
      this.m_version2Label.Size = new System.Drawing.Size(177, 44);
      this.m_version2Label.TabIndex = 0;
      this.m_version2Label.Text = "Version";
      this.m_version2Label.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_version2Label.UseMnemonics = true;
      this.m_version2Label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_period2Label
      // 
      this.m_period2Label.BackColor = System.Drawing.Color.Transparent;
      this.m_period2Label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_period2Label.Ellipsis = false;
      this.m_period2Label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_period2Label.Location = new System.Drawing.Point(6, 103);
      this.m_period2Label.Multiline = true;
      this.m_period2Label.Name = "m_period2Label";
      this.m_period2Label.Size = new System.Drawing.Size(80, 20);
      this.m_period2Label.TabIndex = 5;
      this.m_period2Label.Text = "Period";
      this.m_period2Label.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_period2Label.UseMnemonics = true;
      this.m_period2Label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_validateButton
      // 
      this.m_validateButton.AllowAnimations = true;
      this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
      this.m_validateButton.Location = new System.Drawing.Point(316, 204);
      this.m_validateButton.Name = "m_validateButton";
      this.m_validateButton.RoundedCornersMask = ((byte)(15));
      this.m_validateButton.Size = new System.Drawing.Size(100, 30);
      this.m_validateButton.TabIndex = 10;
      this.m_validateButton.Text = "Validate";
      this.m_validateButton.UseVisualStyleBackColor = false;
      this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_validateButton.Click += new System.EventHandler(this.OnValidate);
      // 
      // PeriodsComparisonSelectionView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(440, 246);
      this.Controls.Add(this.m_validateButton);
      this.Controls.Add(this.m_groupbox2);
      this.Controls.Add(this.m_groupbox1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PeriodsComparisonSelectionView";
      this.Text = "Periods and versions selection";
      this.m_groupbox1.ResumeLayout(false);
      this.m_groupbox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private VIBlend.WinForms.Controls.vLabel m_version1Label;
    private VIBlend.WinForms.Controls.vLabel m_period1Label;
    private VIBlend.WinForms.Controls.vGroupBox m_groupbox1;
    private VIBlend.WinForms.Controls.vGroupBox m_groupbox2;
    private VIBlend.WinForms.Controls.vLabel m_version2Label;
    private VIBlend.WinForms.Controls.vLabel m_period2Label;
    private VIBlend.WinForms.Controls.vButton m_validateButton;
    private VIBlend.WinForms.Controls.vComboBox m_period1CB;
    private VIBlend.WinForms.Controls.vComboBox m_period2CB;
  }
}