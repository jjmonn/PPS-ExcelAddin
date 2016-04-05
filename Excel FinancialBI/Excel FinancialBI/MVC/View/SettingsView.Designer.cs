using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class SettingsView : System.Windows.Forms.Form
  {
    //Form overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing && components != null)
        {
          components.Dispose();
        }
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    //Required by the Windows Form Designer

    private System.ComponentModel.IContainer components;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
      VIBlend.WinForms.DataGridView.DataGridLocalization dataGridLocalization1 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.TabControl1 = new VIBlend.WinForms.Controls.vTabControl();
      this.m_connectionTab = new VIBlend.WinForms.Controls.vTabPage();
      this.m_portTB = new VIBlend.WinForms.Controls.vNumberEditor();
      this.m_saveConnectionButton = new VIBlend.WinForms.Controls.vButton();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.m_portNumberLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_serverAddressTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_serverAddressLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_userTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_userIdLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_formatsTab = new VIBlend.WinForms.Controls.vTabPage();
      this.m_formatsGroup = new VIBlend.WinForms.Controls.vGroupBox();
      this.FormatsDGV = new VIBlend.WinForms.DataGridView.vDataGridView();
      this.m_otherTab = new VIBlend.WinForms.Controls.vTabPage();
      this.m_otherValidateButton = new VIBlend.WinForms.Controls.vButton();
      this.m_languageComboBox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_languageLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_currenciesCombobox = new VIBlend.WinForms.Controls.vComboBox();
      this.m_consolidationCurrencyLabel = new VIBlend.WinForms.Controls.vLabel();
      this.ControlImages = new System.Windows.Forms.ImageList(this.components);
      this.ACFIcon = new System.Windows.Forms.ImageList(this.components);
      this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
      this.Panel1.SuspendLayout();
      this.TabControl1.SuspendLayout();
      this.m_connectionTab.SuspendLayout();
      this.m_formatsTab.SuspendLayout();
      this.m_formatsGroup.SuspendLayout();
      this.m_otherTab.SuspendLayout();
      this.SuspendLayout();
      // 
      // Panel1
      // 
      this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Panel1.Controls.Add(this.TabControl1);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel1.Location = new System.Drawing.Point(0, 0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(462, 317);
      this.Panel1.TabIndex = 0;
      // 
      // TabControl1
      // 
      this.TabControl1.AllowAnimations = true;
      this.TabControl1.Controls.Add(this.m_connectionTab);
      this.TabControl1.Controls.Add(this.m_formatsTab);
      this.TabControl1.Controls.Add(this.m_otherTab);
      this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TabControl1.Location = new System.Drawing.Point(0, 0);
      this.TabControl1.Name = "TabControl1";
      this.TabControl1.Padding = new System.Windows.Forms.Padding(0, 45, 0, 0);
      this.TabControl1.Size = new System.Drawing.Size(460, 315);
      this.TabControl1.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top;
      this.TabControl1.TabIndex = 0;
      this.TabControl1.TabPages.Add(this.m_connectionTab);
      this.TabControl1.TabPages.Add(this.m_formatsTab);
      this.TabControl1.TabPages.Add(this.m_otherTab);
      this.TabControl1.TabsAreaBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
      this.TabControl1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_connectionTab
      // 
      this.m_connectionTab.Controls.Add(this.m_portTB);
      this.m_connectionTab.Controls.Add(this.m_saveConnectionButton);
      this.m_connectionTab.Controls.Add(this.m_portNumberLabel);
      this.m_connectionTab.Controls.Add(this.m_serverAddressTB);
      this.m_connectionTab.Controls.Add(this.m_serverAddressLabel);
      this.m_connectionTab.Controls.Add(this.m_userTB);
      this.m_connectionTab.Controls.Add(this.m_userIdLabel);
      this.m_connectionTab.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_connectionTab.Location = new System.Drawing.Point(0, 45);
      this.m_connectionTab.Name = "m_connectionTab";
      this.m_connectionTab.Padding = new System.Windows.Forms.Padding(3);
      this.m_connectionTab.Size = new System.Drawing.Size(460, 270);
      this.m_connectionTab.TabIndex = 0;
      this.m_connectionTab.Text = "Connection";
      this.m_connectionTab.TooltipText = "Connection";
      this.m_connectionTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_connectionTab.Visible = false;
      // 
      // m_portTB
      // 
      this.m_portTB.BackColor = System.Drawing.Color.White;
      this.m_portTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_portTB.CausesValidation = false;
      this.m_portTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_portTB.CultureInfo = new System.Globalization.CultureInfo("fr");
      this.m_portTB.Cursor = System.Windows.Forms.Cursors.Default;
      this.m_portTB.DecimalPlaces = 0;
      this.m_portTB.DefaultText = "Empty...";
      this.m_portTB.DefaultTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.m_portTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_portTB.Location = new System.Drawing.Point(201, 107);
      this.m_portTB.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.m_portTB.MaxLength = 32767;
      this.m_portTB.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.m_portTB.Name = "m_portTB";
      this.m_portTB.PasswordChar = '\0';
      this.m_portTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_portTB.SelectionLength = 0;
      this.m_portTB.SelectionStart = 0;
      this.m_portTB.Size = new System.Drawing.Size(220, 23);
      this.m_portTB.SpinType = VIBlend.WinForms.Controls.SpinType.SpinDigitWithWrap;
      this.m_portTB.TabIndex = 0;
      this.m_portTB.Text = "0";
      this.m_portTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_portTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_saveConnectionButton
      // 
      this.m_saveConnectionButton.AllowAnimations = true;
      this.m_saveConnectionButton.BackColor = System.Drawing.Color.Transparent;
      this.m_saveConnectionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_saveConnectionButton.ImageKey = "1420498403_340208.ico";
      this.m_saveConnectionButton.ImageList = this.ButtonIcons;
      this.m_saveConnectionButton.Location = new System.Drawing.Point(319, 206);
      this.m_saveConnectionButton.Name = "m_saveConnectionButton";
      this.m_saveConnectionButton.RoundedCornersMask = ((byte)(15));
      this.m_saveConnectionButton.Size = new System.Drawing.Size(102, 30);
      this.m_saveConnectionButton.TabIndex = 20;
      this.m_saveConnectionButton.Text = "Save";
      this.m_saveConnectionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_saveConnectionButton.UseVisualStyleBackColor = false;
      this.m_saveConnectionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // ButtonIcons
      // 
      this.ButtonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonIcons.ImageStream")));
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico");
      this.ButtonIcons.Images.SetKeyName(2, "1420498403_340208.ico");
      this.ButtonIcons.Images.SetKeyName(3, "favicon(97).ico");
      this.ButtonIcons.Images.SetKeyName(4, "imageres_99.ico");
      this.ButtonIcons.Images.SetKeyName(5, "favicon(70).ico");
      this.ButtonIcons.Images.SetKeyName(6, "imageres_82.ico");
      this.ButtonIcons.Images.SetKeyName(7, "refresh greay bcgd.bmp");
      // 
      // m_portNumberLabel
      // 
      this.m_portNumberLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_portNumberLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_portNumberLabel.Ellipsis = false;
      this.m_portNumberLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_portNumberLabel.Location = new System.Drawing.Point(27, 107);
      this.m_portNumberLabel.Multiline = true;
      this.m_portNumberLabel.Name = "m_portNumberLabel";
      this.m_portNumberLabel.Size = new System.Drawing.Size(131, 13);
      this.m_portNumberLabel.TabIndex = 15;
      this.m_portNumberLabel.Text = "Port number";
      this.m_portNumberLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_portNumberLabel.UseMnemonics = true;
      this.m_portNumberLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_serverAddressTB
      // 
      this.m_serverAddressTB.BackColor = System.Drawing.Color.White;
      this.m_serverAddressTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_serverAddressTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_serverAddressTB.DefaultText = "Empty...";
      this.m_serverAddressTB.Location = new System.Drawing.Point(201, 52);
      this.m_serverAddressTB.MaxLength = 32767;
      this.m_serverAddressTB.Name = "m_serverAddressTB";
      this.m_serverAddressTB.PasswordChar = '\0';
      this.m_serverAddressTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_serverAddressTB.SelectionLength = 0;
      this.m_serverAddressTB.SelectionStart = 0;
      this.m_serverAddressTB.Size = new System.Drawing.Size(220, 20);
      this.m_serverAddressTB.TabIndex = 5;
      this.m_serverAddressTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_serverAddressTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_serverAddressLabel
      // 
      this.m_serverAddressLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_serverAddressLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_serverAddressLabel.Ellipsis = false;
      this.m_serverAddressLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_serverAddressLabel.Location = new System.Drawing.Point(27, 55);
      this.m_serverAddressLabel.Multiline = true;
      this.m_serverAddressLabel.Name = "m_serverAddressLabel";
      this.m_serverAddressLabel.Size = new System.Drawing.Size(116, 13);
      this.m_serverAddressLabel.TabIndex = 4;
      this.m_serverAddressLabel.Text = "Server address";
      this.m_serverAddressLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_serverAddressLabel.UseMnemonics = true;
      this.m_serverAddressLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_userTB
      // 
      this.m_userTB.BackColor = System.Drawing.Color.White;
      this.m_userTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_userTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_userTB.DefaultText = "Empty...";
      this.m_userTB.Location = new System.Drawing.Point(201, 153);
      this.m_userTB.MaxLength = 32767;
      this.m_userTB.Name = "m_userTB";
      this.m_userTB.PasswordChar = '\0';
      this.m_userTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_userTB.SelectionLength = 0;
      this.m_userTB.SelectionStart = 0;
      this.m_userTB.Size = new System.Drawing.Size(220, 20);
      this.m_userTB.TabIndex = 1;
      this.m_userTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_userTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_userIdLabel
      // 
      this.m_userIdLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_userIdLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_userIdLabel.Ellipsis = false;
      this.m_userIdLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_userIdLabel.Location = new System.Drawing.Point(27, 156);
      this.m_userIdLabel.Multiline = true;
      this.m_userIdLabel.Name = "m_userIdLabel";
      this.m_userIdLabel.Size = new System.Drawing.Size(78, 13);
      this.m_userIdLabel.TabIndex = 0;
      this.m_userIdLabel.Text = "User Id";
      this.m_userIdLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_userIdLabel.UseMnemonics = true;
      this.m_userIdLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_formatsTab
      // 
      this.m_formatsTab.Controls.Add(this.m_formatsGroup);
      this.m_formatsTab.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_formatsTab.Location = new System.Drawing.Point(0, 45);
      this.m_formatsTab.Name = "m_formatsTab";
      this.m_formatsTab.Padding = new System.Windows.Forms.Padding(3);
      this.m_formatsTab.Size = new System.Drawing.Size(460, 270);
      this.m_formatsTab.TabIndex = 1;
      this.m_formatsTab.Text = "Formats options";
      this.m_formatsTab.TooltipText = "Formats options";
      this.m_formatsTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_formatsTab.Visible = false;
      // 
      // m_formatsGroup
      // 
      this.m_formatsGroup.BackColor = System.Drawing.Color.Transparent;
      this.m_formatsGroup.Controls.Add(this.FormatsDGV);
      this.m_formatsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_formatsGroup.Location = new System.Drawing.Point(4, 4);
      this.m_formatsGroup.Name = "m_formatsGroup";
      this.m_formatsGroup.Size = new System.Drawing.Size(452, 262);
      this.m_formatsGroup.TabIndex = 1;
      this.m_formatsGroup.TabStop = false;
      this.m_formatsGroup.Text = "Reports formats";
      this.m_formatsGroup.UseThemeBorderColor = true;
      this.m_formatsGroup.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // FormatsDGV
      // 
      this.FormatsDGV.AllowAnimations = true;
      this.FormatsDGV.AllowCellMerge = true;
      this.FormatsDGV.AllowClipDrawing = true;
      this.FormatsDGV.AllowContextMenuColumnChooser = true;
      this.FormatsDGV.AllowContextMenuFiltering = true;
      this.FormatsDGV.AllowContextMenuGrouping = true;
      this.FormatsDGV.AllowContextMenuSorting = true;
      this.FormatsDGV.AllowCopyPaste = false;
      this.FormatsDGV.AllowDefaultContextMenu = true;
      this.FormatsDGV.AllowDragDropIndication = true;
      this.FormatsDGV.AllowHeaderItemHighlightOnCellSelection = true;
      this.FormatsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.FormatsDGV.AutoUpdateOnListChanged = false;
      this.FormatsDGV.BackColor = System.Drawing.Color.White;
      this.FormatsDGV.BindingProgressEnabled = false;
      this.FormatsDGV.BindingProgressSampleRate = 20000;
      this.FormatsDGV.BorderColor = System.Drawing.Color.Empty;
      this.FormatsDGV.CellsArea.AllowCellMerge = true;
      this.FormatsDGV.CellsArea.ConditionalFormattingEnabled = false;
      this.FormatsDGV.ColumnsHierarchy.AllowDragDrop = false;
      this.FormatsDGV.ColumnsHierarchy.AllowResize = true;
      this.FormatsDGV.ColumnsHierarchy.AutoStretchColumns = false;
      this.FormatsDGV.ColumnsHierarchy.Fixed = false;
      this.FormatsDGV.ColumnsHierarchy.ShowExpandCollapseButtons = true;
      this.FormatsDGV.EnableColumnChooser = false;
      this.FormatsDGV.EnableResizeToolTip = true;
      this.FormatsDGV.EnableToolTips = true;
      this.FormatsDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
      this.FormatsDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.FormatsDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
      this.FormatsDGV.GroupingEnabled = false;
      this.FormatsDGV.HorizontalScroll = 0;
      this.FormatsDGV.HorizontalScrollBarLargeChange = 20;
      this.FormatsDGV.HorizontalScrollBarSmallChange = 5;
      this.FormatsDGV.ImageList = null;
      this.FormatsDGV.Localization = dataGridLocalization1;
      this.FormatsDGV.Location = new System.Drawing.Point(6, 16);
      this.FormatsDGV.MultipleSelectionEnabled = true;
      this.FormatsDGV.Name = "FormatsDGV";
      this.FormatsDGV.PivotColumnsTotalsEnabled = false;
      this.FormatsDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.FormatsDGV.PivotRowsTotalsEnabled = false;
      this.FormatsDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.FormatsDGV.RowsHierarchy.AllowDragDrop = false;
      this.FormatsDGV.RowsHierarchy.AllowResize = true;
      this.FormatsDGV.RowsHierarchy.CompactStyleRenderingEnabled = false;
      this.FormatsDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
      this.FormatsDGV.RowsHierarchy.Fixed = false;
      this.FormatsDGV.RowsHierarchy.ShowExpandCollapseButtons = true;
      this.FormatsDGV.ScrollBarsEnabled = true;
      this.FormatsDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.FormatsDGV.SelectionBorderEnabled = true;
      this.FormatsDGV.SelectionBorderWidth = 2;
      this.FormatsDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
      this.FormatsDGV.Size = new System.Drawing.Size(440, 229);
      this.FormatsDGV.TabIndex = 0;
      this.FormatsDGV.Text = "VDataGridView1";
      this.FormatsDGV.ToolTipDuration = 5000;
      this.FormatsDGV.ToolTipShowDelay = 1500;
      this.FormatsDGV.VerticalScroll = 0;
      this.FormatsDGV.VerticalScrollBarLargeChange = 20;
      this.FormatsDGV.VerticalScrollBarSmallChange = 5;
      this.FormatsDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
      this.FormatsDGV.VirtualModeCellDefault = false;
      // 
      // m_otherTab
      // 
      this.m_otherTab.Controls.Add(this.m_otherValidateButton);
      this.m_otherTab.Controls.Add(this.m_languageComboBox);
      this.m_otherTab.Controls.Add(this.m_languageLabel);
      this.m_otherTab.Controls.Add(this.m_currenciesCombobox);
      this.m_otherTab.Controls.Add(this.m_consolidationCurrencyLabel);
      this.m_otherTab.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_otherTab.Location = new System.Drawing.Point(0, 45);
      this.m_otherTab.Name = "m_otherTab";
      this.m_otherTab.Padding = new System.Windows.Forms.Padding(3);
      this.m_otherTab.Size = new System.Drawing.Size(460, 270);
      this.m_otherTab.TabIndex = 2;
      this.m_otherTab.Text = "Other";
      this.m_otherTab.TooltipText = "Other";
      this.m_otherTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_otherTab.Visible = false;
      // 
      // m_otherValidateButton
      // 
      this.m_otherValidateButton.AllowAnimations = true;
      this.m_otherValidateButton.BackColor = System.Drawing.Color.Transparent;
      this.m_otherValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_otherValidateButton.ImageKey = "1420498403_340208.ico";
      this.m_otherValidateButton.ImageList = this.ButtonIcons;
      this.m_otherValidateButton.Location = new System.Drawing.Point(315, 178);
      this.m_otherValidateButton.Name = "m_otherValidateButton";
      this.m_otherValidateButton.RoundedCornersMask = ((byte)(15));
      this.m_otherValidateButton.Size = new System.Drawing.Size(102, 30);
      this.m_otherValidateButton.TabIndex = 21;
      this.m_otherValidateButton.Text = "Save";
      this.m_otherValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_otherValidateButton.UseVisualStyleBackColor = false;
      this.m_otherValidateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_languageComboBox
      // 
      this.m_languageComboBox.BackColor = System.Drawing.Color.White;
      this.m_languageComboBox.DisplayMember = "";
      this.m_languageComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_languageComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_languageComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_languageComboBox.DropDownWidth = 210;
      this.m_languageComboBox.Location = new System.Drawing.Point(207, 82);
      this.m_languageComboBox.Name = "m_languageComboBox";
      this.m_languageComboBox.RoundedCornersMaskListItem = ((byte)(15));
      this.m_languageComboBox.Size = new System.Drawing.Size(210, 23);
      this.m_languageComboBox.TabIndex = 1;
      this.m_languageComboBox.UseThemeBackColor = false;
      this.m_languageComboBox.UseThemeDropDownArrowColor = true;
      this.m_languageComboBox.ValueMember = "";
      this.m_languageComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_languageComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_languageLabel
      // 
      this.m_languageLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_languageLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_languageLabel.Ellipsis = false;
      this.m_languageLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_languageLabel.Location = new System.Drawing.Point(22, 82);
      this.m_languageLabel.Multiline = true;
      this.m_languageLabel.Name = "m_languageLabel";
      this.m_languageLabel.Size = new System.Drawing.Size(179, 25);
      this.m_languageLabel.TabIndex = 2;
      this.m_languageLabel.Text = "Language";
      this.m_languageLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_languageLabel.UseMnemonics = true;
      this.m_languageLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_currenciesCombobox
      // 
      this.m_currenciesCombobox.BackColor = System.Drawing.Color.White;
      this.m_currenciesCombobox.DisplayMember = "";
      this.m_currenciesCombobox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_currenciesCombobox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_currenciesCombobox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_currenciesCombobox.DropDownWidth = 210;
      this.m_currenciesCombobox.Location = new System.Drawing.Point(207, 38);
      this.m_currenciesCombobox.Name = "m_currenciesCombobox";
      this.m_currenciesCombobox.RoundedCornersMaskListItem = ((byte)(15));
      this.m_currenciesCombobox.Size = new System.Drawing.Size(210, 23);
      this.m_currenciesCombobox.TabIndex = 0;
      this.m_currenciesCombobox.UseThemeBackColor = false;
      this.m_currenciesCombobox.UseThemeDropDownArrowColor = true;
      this.m_currenciesCombobox.ValueMember = "";
      this.m_currenciesCombobox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      this.m_currenciesCombobox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_consolidationCurrencyLabel
      // 
      this.m_consolidationCurrencyLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_consolidationCurrencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_consolidationCurrencyLabel.Ellipsis = false;
      this.m_consolidationCurrencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_consolidationCurrencyLabel.Location = new System.Drawing.Point(22, 38);
      this.m_consolidationCurrencyLabel.Multiline = true;
      this.m_consolidationCurrencyLabel.Name = "m_consolidationCurrencyLabel";
      this.m_consolidationCurrencyLabel.Size = new System.Drawing.Size(179, 25);
      this.m_consolidationCurrencyLabel.TabIndex = 0;
      this.m_consolidationCurrencyLabel.Text = "Devise de consolidation";
      this.m_consolidationCurrencyLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_consolidationCurrencyLabel.UseMnemonics = true;
      this.m_consolidationCurrencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ControlImages
      // 
      this.ControlImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ControlImages.ImageStream")));
      this.ControlImages.TransparentColor = System.Drawing.Color.Transparent;
      this.ControlImages.Images.SetKeyName(0, "close blue light.ico");
      this.ControlImages.Images.SetKeyName(1, "favicon(99).ico");
      // 
      // ACFIcon
      // 
      this.ACFIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ACFIcon.ImageStream")));
      this.ACFIcon.TransparentColor = System.Drawing.Color.Transparent;
      this.ACFIcon.Images.SetKeyName(0, "ACF Square 2 .1Control bgd.png");
      // 
      // SettingsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(462, 317);
      this.Controls.Add(this.Panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "SettingsView";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Settings";
      this.Panel1.ResumeLayout(false);
      this.TabControl1.ResumeLayout(false);
      this.m_connectionTab.ResumeLayout(false);
      this.m_formatsTab.ResumeLayout(false);
      this.m_formatsGroup.ResumeLayout(false);
      this.m_otherTab.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.Panel Panel1;
    internal VIBlend.WinForms.Controls.vTabControl TabControl1;
    internal VIBlend.WinForms.Controls.vTabPage m_connectionTab;
    internal System.Windows.Forms.ImageList ControlImages;
    internal System.Windows.Forms.ImageList ButtonIcons;
    internal System.Windows.Forms.ImageList ACFIcon;
    internal VIBlend.WinForms.Controls.vTextBox m_userTB;
    internal VIBlend.WinForms.Controls.vLabel m_userIdLabel;
    internal VIBlend.WinForms.Controls.vTextBox m_serverAddressTB;
    internal VIBlend.WinForms.Controls.vLabel m_serverAddressLabel;
    internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
    internal VIBlend.WinForms.Controls.vLabel m_portNumberLabel;
    internal VIBlend.WinForms.Controls.vTabPage m_formatsTab;
    internal VIBlend.WinForms.Controls.vGroupBox m_formatsGroup;
    internal System.Windows.Forms.ColorDialog ColorDialog1;
    internal VIBlend.WinForms.DataGridView.vDataGridView FormatsDGV;
    internal VIBlend.WinForms.Controls.vTabPage m_otherTab;
    internal VIBlend.WinForms.Controls.vComboBox m_currenciesCombobox;
    internal VIBlend.WinForms.Controls.vLabel m_consolidationCurrencyLabel;
    internal VIBlend.WinForms.Controls.vButton m_saveConnectionButton;
    internal VIBlend.WinForms.Controls.vComboBox m_languageComboBox;
    internal VIBlend.WinForms.Controls.vLabel m_languageLabel;
    internal VIBlend.WinForms.Controls.vButton m_otherValidateButton;
    private VIBlend.WinForms.Controls.vNumberEditor m_portTB;
  }
}