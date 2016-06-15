namespace FBI.MVC.View
{
  partial class AccountSnapshotSelectionView
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountSnapshotSelectionView));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_validateBT = new VIBlend.WinForms.Controls.vButton();
      this.m_RCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_selectAllBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_unselectAllBT = new System.Windows.Forms.ToolStripMenuItem();
      this.tableLayoutPanel1.SuspendLayout();
      this.m_RCMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.m_validateBT, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(524, 404);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // m_validateBT
      // 
      this.m_validateBT.AllowAnimations = true;
      this.m_validateBT.BackColor = System.Drawing.Color.Transparent;
      this.m_validateBT.Dock = System.Windows.Forms.DockStyle.Right;
      this.m_validateBT.Location = new System.Drawing.Point(326, 377);
      this.m_validateBT.Name = "m_validateBT";
      this.m_validateBT.RoundedCornersMask = ((byte)(15));
      this.m_validateBT.Size = new System.Drawing.Size(195, 24);
      this.m_validateBT.TabIndex = 1;
      this.m_validateBT.Text = "vButton1";
      this.m_validateBT.UseVisualStyleBackColor = false;
      this.m_validateBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_validateBT.Click += new System.EventHandler(this.OnValidate);
      // 
      // m_RCMenu
      // 
      this.m_RCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_selectAllBT,
            this.m_unselectAllBT});
      this.m_RCMenu.Name = "m_RCMenu";
      this.m_RCMenu.Size = new System.Drawing.Size(153, 70);
      // 
      // m_selectAllBT
      // 
      this.m_selectAllBT.Name = "m_selectAllBT";
      this.m_selectAllBT.Size = new System.Drawing.Size(152, 22);
      this.m_selectAllBT.Text = "Select all";
      // 
      // m_unselectAllBT
      // 
      this.m_unselectAllBT.Name = "m_unselectAllBT";
      this.m_unselectAllBT.Size = new System.Drawing.Size(152, 22);
      this.m_unselectAllBT.Text = "Unselect all";
      // 
      // AccountSnapshotSelectionView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(524, 404);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "AccountSnapshotSelectionView";
      this.Text = "AccountSnapshotSelectionView";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.m_RCMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private VIBlend.WinForms.Controls.vButton m_validateBT;
    private System.Windows.Forms.ContextMenuStrip m_RCMenu;
    private System.Windows.Forms.ToolStripMenuItem m_selectAllBT;
    private System.Windows.Forms.ToolStripMenuItem m_unselectAllBT;
  }
}