namespace FBI.MVC.View
{
  partial class CommitFollowUpView
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
      this.m_dateFromPicker = new System.Windows.Forms.DateTimePicker();
      this.m_dateToPicker = new System.Windows.Forms.DateTimePicker();
      this.m_dateFromText = new System.Windows.Forms.Label();
      this.m_dateToText = new System.Windows.Forms.Label();
      this.m_gridViewPanel = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // m_dateFromPicker
      // 
      this.m_dateFromPicker.Location = new System.Drawing.Point(12, 30);
      this.m_dateFromPicker.Name = "m_dateFromPicker";
      this.m_dateFromPicker.Size = new System.Drawing.Size(187, 20);
      this.m_dateFromPicker.TabIndex = 0;
      // 
      // m_dateToPicker
      // 
      this.m_dateToPicker.Location = new System.Drawing.Point(222, 30);
      this.m_dateToPicker.Name = "m_dateToPicker";
      this.m_dateToPicker.Size = new System.Drawing.Size(200, 20);
      this.m_dateToPicker.TabIndex = 1;
      // 
      // m_dateFromText
      // 
      this.m_dateFromText.AutoSize = true;
      this.m_dateFromText.Location = new System.Drawing.Point(12, 9);
      this.m_dateFromText.Name = "m_dateFromText";
      this.m_dateFromText.Size = new System.Drawing.Size(35, 13);
      this.m_dateFromText.TabIndex = 2;
      this.m_dateFromText.Text = "label1";
      // 
      // m_dateToText
      // 
      this.m_dateToText.AutoSize = true;
      this.m_dateToText.Location = new System.Drawing.Point(219, 9);
      this.m_dateToText.Name = "m_dateToText";
      this.m_dateToText.Size = new System.Drawing.Size(35, 13);
      this.m_dateToText.TabIndex = 3;
      this.m_dateToText.Text = "label2";
      // 
      // m_gridViewPanel
      // 
      this.m_gridViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_gridViewPanel.Location = new System.Drawing.Point(0, 67);
      this.m_gridViewPanel.Name = "m_gridViewPanel";
      this.m_gridViewPanel.Size = new System.Drawing.Size(648, 330);
      this.m_gridViewPanel.TabIndex = 4;
      // 
      // CommitFollowUpView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(648, 397);
      this.Controls.Add(this.m_gridViewPanel);
      this.Controls.Add(this.m_dateToText);
      this.Controls.Add(this.m_dateFromText);
      this.Controls.Add(this.m_dateToPicker);
      this.Controls.Add(this.m_dateFromPicker);
      this.Name = "CommitFollowUpView";
      this.Text = "CommitFollowUpView";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker m_dateFromPicker;
    private System.Windows.Forms.DateTimePicker m_dateToPicker;
    private System.Windows.Forms.Label m_dateFromText;
    private System.Windows.Forms.Label m_dateToText;
    private System.Windows.Forms.Panel m_gridViewPanel;
  }
}