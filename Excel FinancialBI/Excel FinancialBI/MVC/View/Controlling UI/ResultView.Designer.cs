namespace FBI.MVC.View
{
  partial class ResultView
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultView));
      this.m_dgvMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ExpandAllRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.CollapseAllRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.LogRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.DGVFormatsButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ColumnsAutoSize = new System.Windows.Forms.ToolStripMenuItem();
      this.ColumnsAutoFitBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_dgvMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_dgvMenu
      // 
      this.m_dgvMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.m_dgvMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandAllRightClick,
            this.CollapseAllRightClick,
            this.ToolStripSeparator2,
            this.LogRightClick,
            this.ToolStripSeparator4,
            this.DGVFormatsButton,
            this.ColumnsAutoSize,
            this.ColumnsAutoFitBT});
      this.m_dgvMenu.Name = "DGVsRCM";
      this.m_dgvMenu.Size = new System.Drawing.Size(329, 200);
      // 
      // ExpandAllRightClick
      // 
      this.ExpandAllRightClick.Name = "ExpandAllRightClick";
      this.ExpandAllRightClick.Size = new System.Drawing.Size(328, 26);
      this.ExpandAllRightClick.Text = "[CUI.expand_all]";
      // 
      // CollapseAllRightClick
      // 
      this.CollapseAllRightClick.Name = "CollapseAllRightClick";
      this.CollapseAllRightClick.Size = new System.Drawing.Size(328, 26);
      this.CollapseAllRightClick.Text = "[CUI.collapse_all]";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(325, 6);
      // 
      // LogRightClick
      // 
      this.LogRightClick.Name = "LogRightClick";
      this.LogRightClick.Size = new System.Drawing.Size(328, 26);
      this.LogRightClick.Text = "[CUI.log]";
      // 
      // ToolStripSeparator4
      // 
      this.ToolStripSeparator4.Name = "ToolStripSeparator4";
      this.ToolStripSeparator4.Size = new System.Drawing.Size(325, 6);
      // 
      // DGVFormatsButton
      // 
      this.DGVFormatsButton.Image = ((System.Drawing.Image)(resources.GetObject("DGVFormatsButton.Image")));
      this.DGVFormatsButton.Name = "DGVFormatsButton";
      this.DGVFormatsButton.Size = new System.Drawing.Size(328, 26);
      this.DGVFormatsButton.Text = "[CUI.display_options]";
      // 
      // ColumnsAutoSize
      // 
      this.ColumnsAutoSize.Name = "ColumnsAutoSize";
      this.ColumnsAutoSize.Size = new System.Drawing.Size(328, 26);
      this.ColumnsAutoSize.Text = "[CUI.adjust_columns_size]";
      // 
      // ColumnsAutoFitBT
      // 
      this.ColumnsAutoFitBT.Checked = true;
      this.ColumnsAutoFitBT.CheckOnClick = true;
      this.ColumnsAutoFitBT.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ColumnsAutoFitBT.Name = "ColumnsAutoFitBT";
      this.ColumnsAutoFitBT.Size = new System.Drawing.Size(328, 26);
      this.ColumnsAutoFitBT.Text = "[CUI.automatic_columns_adjustment]";
      // 
      // ResultView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "ResultView";
      this.m_dgvMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.ContextMenuStrip m_dgvMenu;
    public System.Windows.Forms.ToolStripMenuItem ExpandAllRightClick;
    public System.Windows.Forms.ToolStripMenuItem CollapseAllRightClick;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    public System.Windows.Forms.ToolStripMenuItem LogRightClick;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    public System.Windows.Forms.ToolStripMenuItem DGVFormatsButton;
    public System.Windows.Forms.ToolStripMenuItem ColumnsAutoSize;
    public System.Windows.Forms.ToolStripMenuItem ColumnsAutoFitBT;
  }
}
