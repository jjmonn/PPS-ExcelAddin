namespace FBI.MVC.View
{
  partial class ChartPanelSelection
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
      this.m_list = new VIBlend.WinForms.Controls.vListBox();
      this.m_text = new VIBlend.WinForms.Controls.vTextBox();
      this.m_next = new VIBlend.WinForms.Controls.vButton();
      this.m_save = new VIBlend.WinForms.Controls.vButton();
      this.SuspendLayout();
      // 
      // m_list
      // 
      this.m_list.Location = new System.Drawing.Point(12, 12);
      this.m_list.Name = "m_list";
      this.m_list.RoundedCornersMaskListItem = ((byte)(15));
      this.m_list.Size = new System.Drawing.Size(326, 184);
      this.m_list.TabIndex = 0;
      this.m_list.Text = "vListBox1";
      this.m_list.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_list.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_text
      // 
      this.m_text.BackColor = System.Drawing.Color.White;
      this.m_text.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_text.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_text.DefaultText = "Empty...";
      this.m_text.Location = new System.Drawing.Point(357, 12);
      this.m_text.MaxLength = 32767;
      this.m_text.Name = "m_text";
      this.m_text.PasswordChar = '\0';
      this.m_text.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_text.SelectionLength = 0;
      this.m_text.SelectionStart = 0;
      this.m_text.Size = new System.Drawing.Size(234, 26);
      this.m_text.TabIndex = 1;
      this.m_text.Text = "vTextBox1";
      this.m_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_text.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_next
      // 
      this.m_next.AllowAnimations = true;
      this.m_next.BackColor = System.Drawing.Color.Transparent;
      this.m_next.Location = new System.Drawing.Point(357, 169);
      this.m_next.Name = "m_next";
      this.m_next.RoundedCornersMask = ((byte)(15));
      this.m_next.Size = new System.Drawing.Size(234, 27);
      this.m_next.TabIndex = 3;
      this.m_next.Text = "vButton1";
      this.m_next.UseVisualStyleBackColor = false;
      this.m_next.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_save
      // 
      this.m_save.AllowAnimations = true;
      this.m_save.BackColor = System.Drawing.Color.Transparent;
      this.m_save.Location = new System.Drawing.Point(357, 44);
      this.m_save.Name = "m_save";
      this.m_save.PressedTextColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.m_save.RoundedCornersMask = ((byte)(15));
      this.m_save.Size = new System.Drawing.Size(234, 27);
      this.m_save.TabIndex = 2;
      this.m_save.Text = "vButton1";
      this.m_save.UseVisualStyleBackColor = false;
      this.m_save.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ChartPanelSelection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(603, 215);
      this.Controls.Add(this.m_save);
      this.Controls.Add(this.m_list);
      this.Controls.Add(this.m_next);
      this.Controls.Add(this.m_text);
      this.Name = "ChartPanelSelection";
      this.Text = "ChartPanelSelection";
      this.ResumeLayout(false);

    }

    #endregion

    private VIBlend.WinForms.Controls.vListBox m_list;
    private VIBlend.WinForms.Controls.vTextBox m_text;
    private VIBlend.WinForms.Controls.vButton m_next;
    private VIBlend.WinForms.Controls.vButton m_save;
  }
}