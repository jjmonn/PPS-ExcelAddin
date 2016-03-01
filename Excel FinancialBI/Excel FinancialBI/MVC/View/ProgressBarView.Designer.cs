namespace FBI.MVC.View
{
  partial class ProgressBarView
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBarView));
      this.m_progressBar = new VIBlend.WinForms.Controls.vProgressBar();
      this.m_label = new VIBlend.WinForms.Controls.vLabel();
      this.m_cancelButton = new VIBlend.WinForms.Controls.vButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // m_progressBar
      // 
      this.m_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_progressBar.BackColor = System.Drawing.Color.Transparent;
      this.m_progressBar.Location = new System.Drawing.Point(30, 19);
      this.m_progressBar.Name = "m_progressBar";
      this.m_progressBar.RoundedCornersMask = ((byte)(15));
      this.m_progressBar.Size = new System.Drawing.Size(520, 21);
      this.m_progressBar.TabIndex = 0;
      this.m_progressBar.Text = "vProgressBar1";
      this.m_progressBar.Value = 0;
      this.m_progressBar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_label
      // 
      this.m_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_label.BackColor = System.Drawing.Color.Transparent;
      this.m_label.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_label.Ellipsis = false;
      this.m_label.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_label.Location = new System.Drawing.Point(-1, 50);
      this.m_label.Multiline = true;
      this.m_label.Name = "m_label";
      this.m_label.Size = new System.Drawing.Size(576, 25);
      this.m_label.TabIndex = 1;
      this.m_label.Text = " ";
      this.m_label.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_label.UseMnemonics = true;
      this.m_label.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.AllowAnimations = true;
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.BackColor = System.Drawing.Color.Transparent;
      this.m_cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_cancelButton.ImageKey = "delete.ico";
      this.m_cancelButton.ImageList = this.imageList1;
      this.m_cancelButton.Location = new System.Drawing.Point(255, 84);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.RoundedCornersMask = ((byte)(15));
      this.m_cancelButton.Size = new System.Drawing.Size(89, 30);
      this.m_cancelButton.TabIndex = 2;
      this.m_cancelButton.Text = "Cancel";
      this.m_cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_cancelButton.UseVisualStyleBackColor = false;
      this.m_cancelButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "delete.ico");
      this.imageList1.Images.SetKeyName(1, "upload.png");
      // 
      // ProgressBarView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(577, 125);
      this.Controls.Add(this.m_cancelButton);
      this.Controls.Add(this.m_label);
      this.Controls.Add(this.m_progressBar);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ProgressBarView";
      this.ResumeLayout(false);

    }

    #endregion

    private VIBlend.WinForms.Controls.vProgressBar m_progressBar;
    private VIBlend.WinForms.Controls.vLabel m_label;
    private VIBlend.WinForms.Controls.vButton m_cancelButton;
    private System.Windows.Forms.ImageList imageList1;
  }
}