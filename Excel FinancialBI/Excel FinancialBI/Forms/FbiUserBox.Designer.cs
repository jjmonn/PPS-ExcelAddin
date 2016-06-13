namespace FBI.Forms
{
  partial class FbiUserBoxForm
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
      this.m_combo = new VIBlend.WinForms.Controls.vComboBox();
      this.m_text = new VIBlend.WinForms.Controls.vTextBox();
      this.m_title = new VIBlend.WinForms.Controls.vLabel();
      this.m_ok = new VIBlend.WinForms.Controls.vButton();
      this.m_cancel = new VIBlend.WinForms.Controls.vButton();
      this.m_combo.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_combo
      // 
      this.m_combo.BackColor = System.Drawing.Color.White;
      this.m_combo.Controls.Add(this.m_text);
      this.m_combo.DisplayMember = "";
      this.m_combo.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_combo.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_combo.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_combo.DropDownWidth = 258;
      this.m_combo.Location = new System.Drawing.Point(12, 49);
      this.m_combo.Name = "m_combo";
      this.m_combo.RoundedCornersMaskListItem = ((byte)(15));
      this.m_combo.Size = new System.Drawing.Size(258, 25);
      this.m_combo.TabIndex = 0;
      this.m_combo.Text = "vComboBox1";
      this.m_combo.UseThemeBackColor = false;
      this.m_combo.UseThemeDropDownArrowColor = true;
      this.m_combo.ValueMember = "";
      this.m_combo.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_combo.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_text
      // 
      this.m_text.BackColor = System.Drawing.Color.White;
      this.m_text.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_text.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_text.DefaultText = "Empty...";
      this.m_text.Location = new System.Drawing.Point(0, 1);
      this.m_text.MaxLength = 32767;
      this.m_text.Name = "m_text";
      this.m_text.PasswordChar = '\0';
      this.m_text.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_text.SelectionLength = 0;
      this.m_text.SelectionStart = 0;
      this.m_text.Size = new System.Drawing.Size(258, 23);
      this.m_text.TabIndex = 1;
      this.m_text.Text = "vTextBox1";
      this.m_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_text.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_title
      // 
      this.m_title.BackColor = System.Drawing.Color.Transparent;
      this.m_title.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_title.Ellipsis = false;
      this.m_title.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_title.Location = new System.Drawing.Point(12, 12);
      this.m_title.Multiline = true;
      this.m_title.Name = "m_title";
      this.m_title.Size = new System.Drawing.Size(258, 31);
      this.m_title.TabIndex = 1;
      this.m_title.Text = "vLabel1";
      this.m_title.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_title.UseMnemonics = true;
      this.m_title.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_ok
      // 
      this.m_ok.AllowAnimations = true;
      this.m_ok.BackColor = System.Drawing.Color.Transparent;
      this.m_ok.Location = new System.Drawing.Point(326, 12);
      this.m_ok.Name = "m_ok";
      this.m_ok.RoundedCornersMask = ((byte)(15));
      this.m_ok.Size = new System.Drawing.Size(73, 21);
      this.m_ok.TabIndex = 2;
      this.m_ok.Text = "OK";
      this.m_ok.UseVisualStyleBackColor = false;
      this.m_ok.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_ok.Click += new System.EventHandler(this.m_ok_Click);
      // 
      // m_cancel
      // 
      this.m_cancel.AllowAnimations = true;
      this.m_cancel.BackColor = System.Drawing.Color.Transparent;
      this.m_cancel.Location = new System.Drawing.Point(326, 39);
      this.m_cancel.Name = "m_cancel";
      this.m_cancel.RoundedCornersMask = ((byte)(15));
      this.m_cancel.Size = new System.Drawing.Size(73, 21);
      this.m_cancel.TabIndex = 3;
      this.m_cancel.Text = "Cancel";
      this.m_cancel.UseVisualStyleBackColor = false;
      this.m_cancel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_cancel.Click += new System.EventHandler(this.m_cancel_Click);
      // 
      // FbiUserBoxForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(411, 93);
      this.Controls.Add(this.m_cancel);
      this.Controls.Add(this.m_ok);
      this.Controls.Add(this.m_title);
      this.Controls.Add(this.m_combo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "FbiUserBoxForm";
      this.Text = "FbiUserBox";
      this.m_combo.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private VIBlend.WinForms.Controls.vComboBox m_combo;
    private VIBlend.WinForms.Controls.vLabel m_title;
    private VIBlend.WinForms.Controls.vButton m_ok;
    private VIBlend.WinForms.Controls.vButton m_cancel;
    private VIBlend.WinForms.Controls.vTextBox m_text;
  }
}