namespace FBI.MVC.View
{
    public partial class ReportUploadEntitySelectionSidePane
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
  
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
  
        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportUploadEntitySelectionSidePane));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_entitySelectionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_periodsSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountSelectionComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
      this.m_periodsSelectionPanel = new System.Windows.Forms.Panel();
      this.m_entitiesImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_treeviewPanel = new System.Windows.Forms.Panel();
      this.m_buttonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Controls.Add(this.m_entitySelectionLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsSelectionLabel, 0, 2);
      this.TableLayoutPanel1.Controls.Add(this.m_accountSelectionLabel, 0, 4);
      this.TableLayoutPanel1.Controls.Add(this.m_accountSelectionComboBox, 0, 5);
      this.TableLayoutPanel1.Controls.Add(this.m_validateButton, 0, 6);
      this.TableLayoutPanel1.Controls.Add(this.m_periodsSelectionPanel, 0, 3);
      this.TableLayoutPanel1.Controls.Add(this.m_treeviewPanel, 0, 1);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 7;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 203F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(258, 876);
      this.TableLayoutPanel1.TabIndex = 7;
      // 
      // m_entitySelectionLabel
      // 
      this.m_entitySelectionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_entitySelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_entitySelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_entitySelectionLabel.Ellipsis = false;
      this.m_entitySelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_entitySelectionLabel.Location = new System.Drawing.Point(3, 3);
      this.m_entitySelectionLabel.Multiline = true;
      this.m_entitySelectionLabel.Name = "m_entitySelectionLabel";
      this.m_entitySelectionLabel.Size = new System.Drawing.Size(252, 17);
      this.m_entitySelectionLabel.TabIndex = 4;
      this.m_entitySelectionLabel.Text = "Entity selection";
      this.m_entitySelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_entitySelectionLabel.UseMnemonics = true;
      this.m_entitySelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_periodsSelectionLabel
      // 
      this.m_periodsSelectionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_periodsSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_periodsSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_periodsSelectionLabel.Ellipsis = false;
      this.m_periodsSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_periodsSelectionLabel.Location = new System.Drawing.Point(3, 533);
      this.m_periodsSelectionLabel.Multiline = true;
      this.m_periodsSelectionLabel.Name = "m_periodsSelectionLabel";
      this.m_periodsSelectionLabel.Size = new System.Drawing.Size(252, 17);
      this.m_periodsSelectionLabel.TabIndex = 3;
      this.m_periodsSelectionLabel.Text = "Periods selection";
      this.m_periodsSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_periodsSelectionLabel.UseMnemonics = true;
      this.m_periodsSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountSelectionLabel
      // 
      this.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountSelectionLabel.Ellipsis = false;
      this.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountSelectionLabel.Location = new System.Drawing.Point(3, 759);
      this.m_accountSelectionLabel.Multiline = true;
      this.m_accountSelectionLabel.Name = "m_accountSelectionLabel";
      this.m_accountSelectionLabel.Size = new System.Drawing.Size(252, 17);
      this.m_accountSelectionLabel.TabIndex = 5;
      this.m_accountSelectionLabel.Text = "Account selection";
      this.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountSelectionLabel.UseMnemonics = true;
      this.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_accountSelectionComboBox
      // 
      this.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White;
      this.m_accountSelectionComboBox.DisplayMember = "";
      this.m_accountSelectionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountSelectionComboBox.DropDownList = true;
      this.m_accountSelectionComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_accountSelectionComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_accountSelectionComboBox.DropDownWidth = 252;
      this.m_accountSelectionComboBox.Location = new System.Drawing.Point(3, 782);
      this.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox";
      this.m_accountSelectionComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.m_accountSelectionComboBox.Size = new System.Drawing.Size(252, 17);
      this.m_accountSelectionComboBox.TabIndex = 2;
      this.m_accountSelectionComboBox.UseThemeBackColor = false;
      this.m_accountSelectionComboBox.UseThemeDropDownArrowColor = true;
      this.m_accountSelectionComboBox.ValueMember = "";
      this.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_validateButton
      // 
      this.m_validateButton.AllowAnimations = true;
      this.m_validateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
      this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_validateButton.ImageKey = "1420498403_340208.ico";
      this.m_validateButton.ImageList = this.m_buttonsImageList;
      this.m_validateButton.Location = new System.Drawing.Point(3, 849);
      this.m_validateButton.Name = "m_validateButton";
      this.m_validateButton.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
      this.m_validateButton.RoundedCornersMask = ((byte)(15));
      this.m_validateButton.Size = new System.Drawing.Size(98, 24);
      this.m_validateButton.TabIndex = 7;
      this.m_validateButton.Text = "Validate";
      this.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_validateButton.UseVisualStyleBackColor = false;
      this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_periodsSelectionPanel
      // 
      this.m_periodsSelectionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_periodsSelectionPanel.Location = new System.Drawing.Point(3, 556);
      this.m_periodsSelectionPanel.Name = "m_periodsSelectionPanel";
      this.m_periodsSelectionPanel.Size = new System.Drawing.Size(252, 197);
      this.m_periodsSelectionPanel.TabIndex = 8;
      // 
      // m_entitiesImageList
      // 
      this.m_entitiesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_entitiesImageList.ImageStream")));
      this.m_entitiesImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_entitiesImageList.Images.SetKeyName(0, "favicon(81) (1).ico");
      this.m_entitiesImageList.Images.SetKeyName(1, "elements_branch.ico");
      // 
      // m_treeviewPanel
      // 
      this.m_treeviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_treeviewPanel.Location = new System.Drawing.Point(3, 26);
      this.m_treeviewPanel.Name = "m_treeviewPanel";
      this.m_treeviewPanel.Size = new System.Drawing.Size(252, 501);
      this.m_treeviewPanel.TabIndex = 9;
      // 
      // m_buttonsImageList
      // 
      this.m_buttonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_buttonsImageList.ImageStream")));
      this.m_buttonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_buttonsImageList.Images.SetKeyName(0, "1420498403_340208.ico");
      // 
      // ReportUploadEntitySelectionSidePane
      // 
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(258, 876);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = new System.Drawing.Point(0, 0);
      this.Name = "ReportUploadEntitySelectionSidePane";
      this.Text = "Data Edition";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        public VIBlend.WinForms.Controls.vLabel m_entitySelectionLabel;
        public VIBlend.WinForms.Controls.vLabel m_periodsSelectionLabel;
        public VIBlend.WinForms.Controls.vLabel m_accountSelectionLabel;
        public VIBlend.WinForms.Controls.vComboBox m_accountSelectionComboBox;
        public VIBlend.WinForms.Controls.vButton m_validateButton;
        public System.Windows.Forms.Panel m_periodsSelectionPanel;
        private System.Windows.Forms.ImageList m_entitiesImageList;
        private System.Windows.Forms.Panel m_treeviewPanel;
        private System.Windows.Forms.ImageList m_buttonsImageList;

    }
}
