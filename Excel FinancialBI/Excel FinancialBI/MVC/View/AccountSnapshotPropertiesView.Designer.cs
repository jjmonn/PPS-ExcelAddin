namespace FBI.MVC.View
{
  partial class AccountSnapshotPropertiesView
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountSnapshotPropertiesView));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_topPanel = new System.Windows.Forms.TableLayoutPanel();
      this.m_accountTV = new FBI.Forms.AccountTreeView();
      this.m_dgvPanel = new System.Windows.Forms.Panel();
      this.m_validateBT = new VIBlend.WinForms.Controls.vButton();
      this.accountsIL = new System.Windows.Forms.ImageList(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.copyDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tableLayoutPanel1.SuspendLayout();
      this.m_topPanel.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.m_topPanel, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.m_validateBT, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 404);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // m_topPanel
      // 
      this.m_topPanel.ColumnCount = 2;
      this.m_topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.m_topPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.m_topPanel.Controls.Add(this.m_accountTV, 1, 0);
      this.m_topPanel.Controls.Add(this.m_dgvPanel, 0, 0);
      this.m_topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_topPanel.Location = new System.Drawing.Point(3, 3);
      this.m_topPanel.Name = "m_topPanel";
      this.m_topPanel.RowCount = 1;
      this.m_topPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.m_topPanel.Size = new System.Drawing.Size(775, 368);
      this.m_topPanel.TabIndex = 2;
      // 
      // m_accountTV
      // 
      this.m_accountTV.AccessibleName = "TreeView";
      this.m_accountTV.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
      this.m_accountTV.BackColor = System.Drawing.Color.White;
      this.m_accountTV.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountTV.IndicatorsColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      this.m_accountTV.IndicatorsHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      this.m_accountTV.Location = new System.Drawing.Point(578, 3);
      this.m_accountTV.Name = "m_accountTV";
      this.m_accountTV.PaintNodesDefaultBorder = false;
      this.m_accountTV.PaintNodesDefaultFill = false;
      this.m_accountTV.ScrollPosition = new System.Drawing.Point(0, 0);
      this.m_accountTV.SelectedNode = null;
      this.m_accountTV.Size = new System.Drawing.Size(194, 362);
      this.m_accountTV.TabIndex = 1;
      this.m_accountTV.Text = "vTreeView1";
      this.m_accountTV.UseThemeBackColor = false;
      this.m_accountTV.UseThemeIndicatorsColor = false;
      this.m_accountTV.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK;
      this.m_accountTV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK;
      // 
      // m_dgvPanel
      // 
      this.m_dgvPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_dgvPanel.Location = new System.Drawing.Point(3, 3);
      this.m_dgvPanel.Name = "m_dgvPanel";
      this.m_dgvPanel.Size = new System.Drawing.Size(569, 362);
      this.m_dgvPanel.TabIndex = 2;
      // 
      // m_validateBT
      // 
      this.m_validateBT.AllowAnimations = true;
      this.m_validateBT.BackColor = System.Drawing.Color.Transparent;
      this.m_validateBT.Dock = System.Windows.Forms.DockStyle.Right;
      this.m_validateBT.Location = new System.Drawing.Point(608, 377);
      this.m_validateBT.Name = "m_validateBT";
      this.m_validateBT.RoundedCornersMask = ((byte)(15));
      this.m_validateBT.Size = new System.Drawing.Size(170, 24);
      this.m_validateBT.TabIndex = 1;
      this.m_validateBT.Text = "vButton1";
      this.m_validateBT.UseVisualStyleBackColor = false;
      this.m_validateBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_validateBT.Click += new System.EventHandler(this.OnValidate);
      // 
      // accountsIL
      // 
      this.accountsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("accountsIL.ImageStream")));
      this.accountsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.accountsIL.Images.SetKeyName(0, "WC blue.png");
      this.accountsIL.Images.SetKeyName(1, "pencil.ico");
      this.accountsIL.Images.SetKeyName(2, "formula.ico");
      this.accountsIL.Images.SetKeyName(3, "sum purple.png");
      this.accountsIL.Images.SetKeyName(4, "BS Blue.png");
      this.accountsIL.Images.SetKeyName(5, "favicon(81).ico");
      this.accountsIL.Images.SetKeyName(6, "func.png");
      this.accountsIL.Images.SetKeyName(7, "config blue circle.png");
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyDownToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
      // 
      // copyDownToolStripMenuItem
      // 
      this.copyDownToolStripMenuItem.Name = "copyDownToolStripMenuItem";
      this.copyDownToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.copyDownToolStripMenuItem.Text = "Copy Down";
      // 
      // AccountSnapshotPropertiesView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(781, 404);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "AccountSnapshotPropertiesView";
      this.Text = "AccountSnapshotSelectionView";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.m_topPanel.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel m_topPanel;
    private VIBlend.WinForms.Controls.vButton m_validateBT;
    private FBI.Forms.AccountTreeView m_accountTV;
    public System.Windows.Forms.ImageList accountsIL;
    private System.Windows.Forms.Panel m_dgvPanel;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem copyDownToolStripMenuItem;
  }
}