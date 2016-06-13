// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.FilterForm
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <exclude />
  internal class FilterForm : RibbonForm
  {
    private string filterButtonText = "Custom Filter";
    private string filterButtonText2 = "Basic Filter";
    private HierarchyItem filterItem;
    /// <summary>Required designer variable.</summary>
    private IContainer components;
    public vLabel labelShowItemsWhere;
    public vComboBox comboBoxFilters1;
    public vTextBox textBoxValue1;
    public vRadioButton radioButtonFilterAND;
    public vRadioButton radioButtonFilterOR;
    public vTextBox textBoxValue2;
    public vComboBox comboBoxFilters2;
    public vButton btnFilterWindowOk;
    public vButton btnFilterWindowCancel;
    public vLabel labelValue1;
    public vLabel vLabel1;
    public vLabel textBlockFilterBy;
    public vToggleButton buttonCustomFilter;
    public vCheckedListBox filterCheckedListBox;
    public vDateTimePicker dateTimeEditorValue1;
    public vDateTimePicker dateTimeEditorValue2;

    /// <summary>Gets or sets the filter item.</summary>
    /// <value>The filter item.</value>
    public HierarchyItem FilterItem
    {
      get
      {
        return this.filterItem;
      }
      set
      {
        this.filterItem = value;
      }
    }

    public FilterForm()
    {
      this.InitializeComponent();
      this.Shown += new EventHandler(this.FilterForm_Shown);
    }

    private void SetChildControlsTheme()
    {
      this.btnFilterWindowOk.VIBlendTheme = this.VIBlendTheme;
      this.btnFilterWindowCancel.VIBlendTheme = this.VIBlendTheme;
      this.comboBoxFilters1.VIBlendTheme = this.VIBlendTheme;
      this.comboBoxFilters2.VIBlendTheme = this.VIBlendTheme;
      this.textBoxValue1.VIBlendTheme = this.VIBlendTheme;
      this.textBoxValue2.VIBlendTheme = this.VIBlendTheme;
      this.radioButtonFilterAND.VIBlendTheme = this.VIBlendTheme;
      this.radioButtonFilterOR.VIBlendTheme = this.VIBlendTheme;
      this.textBlockFilterBy.VIBlendTheme = this.VIBlendTheme;
      this.labelShowItemsWhere.VIBlendTheme = this.VIBlendTheme;
      this.labelValue1.VIBlendTheme = this.VIBlendTheme;
      this.labelValue1.VIBlendTheme = this.VIBlendTheme;
      this.dateTimeEditorValue1.VIBlendTheme = this.VIBlendTheme;
      this.dateTimeEditorValue2.VIBlendTheme = this.VIBlendTheme;
      this.BackColor = Color.WhiteSmoke;
      this.Refresh();
    }

    private void FilterForm_Shown(object sender, EventArgs e)
    {
      if (this.filterItem != null && this.filterItem.Caption != "")
      {
        this.textBlockFilterBy.Text = "Filter by: " + this.filterItem.Caption;
        this.textBlockFilterBy.Visible = true;
      }
      else
        this.textBlockFilterBy.Visible = false;
      if (this.filterItem != null)
        this.labelShowItemsWhere.Text = string.Format("Show {0} where:", this.filterItem.IsColumnsHierarchyItem ? (object) "rows" : (object) "columns");
      this.SetChildControlsTheme();
    }

    private void buttonCustomFilter_ToggleStateChanged(object sender, EventArgs e)
    {
      if (this.buttonCustomFilter.Text.Equals(this.FilterItem.DataGridView.Localization.GetString(LocalizationNames.FilterButtonCustomFilter)))
        this.buttonCustomFilter.Text = this.FilterItem.DataGridView.Localization.GetString(LocalizationNames.FilterButtonBasicFilter);
      else
        this.buttonCustomFilter.Text = this.FilterItem.DataGridView.Localization.GetString(LocalizationNames.FilterButtonCustomFilter);
      this.UpdateControlsVisibility();
    }

    internal void UpdateControlsVisibility()
    {
      if (this.buttonCustomFilter.Toggle == CheckState.Checked)
      {
        this.comboBoxFilters2.Visible = true;
        this.comboBoxFilters1.Visible = true;
        this.labelShowItemsWhere.Visible = true;
        this.labelValue1.Visible = true;
        this.vLabel1.Visible = true;
        this.textBoxValue1.Visible = true;
        this.textBoxValue2.Height = 23;
        this.textBoxValue1.Height = 23;
        this.dateTimeEditorValue2.Height = 23;
        this.dateTimeEditorValue1.Height = 23;
        this.textBoxValue2.Visible = true;
        this.dateTimeEditorValue1.Visible = true;
        this.dateTimeEditorValue2.Visible = true;
        this.filterCheckedListBox.Visible = false;
        if (this.FilterItem != null)
          this.FilterItem.DataGridView.UpdateValueInputVisibility();
      }
      else
      {
        this.comboBoxFilters2.Visible = false;
        this.comboBoxFilters1.Visible = false;
        this.labelShowItemsWhere.Visible = false;
        this.labelValue1.Visible = false;
        this.vLabel1.Visible = false;
        this.textBoxValue1.Visible = false;
        this.textBoxValue2.Visible = false;
        this.dateTimeEditorValue1.Visible = false;
        this.dateTimeEditorValue2.Visible = false;
        this.textBlockFilterBy.Visible = true;
        this.filterCheckedListBox.Visible = true;
      }
      this.textBoxValue1.PerformLayout();
      this.textBoxValue2.PerformLayout();
      this.dateTimeEditorValue1.PerformLayout();
      this.dateTimeEditorValue2.PerformLayout();
    }

    /// <summary>Clean up any resources being used.</summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterForm));
      this.labelShowItemsWhere = new vLabel();
      this.comboBoxFilters1 = new vComboBox();
      this.textBoxValue1 = new vTextBox();
      this.dateTimeEditorValue1 = new vDateTimePicker();
      this.radioButtonFilterAND = new vRadioButton();
      this.radioButtonFilterOR = new vRadioButton();
      this.textBoxValue2 = new vTextBox();
      this.comboBoxFilters2 = new vComboBox();
      this.btnFilterWindowOk = new vButton();
      this.btnFilterWindowCancel = new vButton();
      this.labelValue1 = new vLabel();
      this.vLabel1 = new vLabel();
      this.textBlockFilterBy = new vLabel();
      this.buttonCustomFilter = new vToggleButton();
      this.filterCheckedListBox = new vCheckedListBox();
      this.dateTimeEditorValue2 = new vDateTimePicker();
      this.textBoxValue1.SuspendLayout();
      this.SuspendLayout();
      this.closeButton.Location = new Point(230, 4);
      this.labelShowItemsWhere.BackColor = Color.Transparent;
      this.labelShowItemsWhere.DisplayStyle = LabelItemStyle.TextOnly;
      this.labelShowItemsWhere.Ellipsis = false;
      this.labelShowItemsWhere.ImageAlignment = ContentAlignment.TopLeft;
      this.labelShowItemsWhere.Location = new Point(13, 87);
      this.labelShowItemsWhere.Multiline = false;
      this.labelShowItemsWhere.Name = "labelShowItemsWhere";
      this.labelShowItemsWhere.PaintBorder = false;
      this.labelShowItemsWhere.PaintFill = false;
      this.labelShowItemsWhere.Size = new Size(217, 219);
      this.labelShowItemsWhere.TabIndex = 0;
      this.labelShowItemsWhere.Text = "Show rows where:";
      this.labelShowItemsWhere.TextAlignment = ContentAlignment.TopLeft;
      this.labelShowItemsWhere.UseMnemonics = true;
      this.labelShowItemsWhere.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.comboBoxFilters1.AutoCompleteEnabled = false;
      this.comboBoxFilters1.BackColor = Color.White;
      this.comboBoxFilters1.BorderColor = Color.Black;
      this.comboBoxFilters1.DisplayMember = "";
      this.comboBoxFilters1.DropDownArrowBackgroundEnabled = true;
      this.comboBoxFilters1.DropDownArrowColor = Color.Black;
      this.comboBoxFilters1.DropDownHeight = 100;
      this.comboBoxFilters1.DropDownMaximumSize = new Size(1000, 1000);
      this.comboBoxFilters1.DropDownMinimumSize = new Size(10, 10);
      this.comboBoxFilters1.DropDownResizeDirection = SizingDirection.Both;
      this.comboBoxFilters1.DropDownWidth = 131;
      this.comboBoxFilters1.ItemHeight = 17;
      this.comboBoxFilters1.Location = new Point(13, 108);
      this.comboBoxFilters1.Name = "comboBoxFilters1";
      this.comboBoxFilters1.RoundedCornersMaskListItem = (byte) 15;
      this.comboBoxFilters1.RoundedCornersRadiusListItem = 3;
      this.comboBoxFilters1.Size = new Size(217, 23);
      this.comboBoxFilters1.TabIndex = 1;
      this.comboBoxFilters1.Text = "vComboBox1";
      this.comboBoxFilters1.UseThemeBackColor = false;
      this.comboBoxFilters1.UseThemeBorderColor = true;
      this.comboBoxFilters1.UseThemeDropDownArrowColor = true;
      this.comboBoxFilters1.UseThemeFont = true;
      this.comboBoxFilters1.UseThemeForeColor = false;
      this.comboBoxFilters1.ValueMember = "";
      this.comboBoxFilters1.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.comboBoxFilters1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.textBoxValue1.AllowAnimations = false;
      this.textBoxValue1.BackColor = Color.White;
      this.textBoxValue1.BoundsOffset = new Size(0, 0);
      this.textBoxValue1.ControlBorderColor = Color.FromArgb(39, 39, 39);
      this.textBoxValue1.ControlHighlightBorderColor = Color.DimGray;
      this.textBoxValue1.DefaultText = "";
      this.textBoxValue1.DefaultTextColor = Color.Black;
      this.textBoxValue1.DefaultTextFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxValue1.EnableBorderHighlight = false;
      this.textBoxValue1.GleamWidth = 1;
      this.textBoxValue1.Location = new Point(13, 159);
      this.textBoxValue1.MaxLength = (int) short.MaxValue;
      this.textBoxValue1.Multiline = false;
      this.textBoxValue1.Name = "textBoxValue1";
      this.textBoxValue1.PasswordChar = char.MinValue;
      this.textBoxValue1.Readonly = false;
      this.textBoxValue1.ScrollBars = ScrollBars.None;
      this.textBoxValue1.SelectionLength = 0;
      this.textBoxValue1.SelectionStart = 0;
      this.textBoxValue1.ShowDefaultText = false;
      this.textBoxValue1.Size = new Size(217, 23);
      this.textBoxValue1.TabIndex = 2;
      this.textBoxValue1.TextAlign = HorizontalAlignment.Left;
      this.textBoxValue1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.dateTimeEditorValue1.BackColor = Color.White;
      this.dateTimeEditorValue1.Culture = new CultureInfo("");
      this.dateTimeEditorValue1.DefaultDateTimeFormat = DefaultDateTimePatterns.Custom;
      this.dateTimeEditorValue1.FormatValue = "";
      this.dateTimeEditorValue1.Location = new Point(13, 159);
      this.dateTimeEditorValue1.Name = "dateTimeEditorValue1";
      this.dateTimeEditorValue1.Size = new Size(221, 23);
      this.dateTimeEditorValue1.TabIndex = 0;
      this.dateTimeEditorValue1.Text = "01/27/2011 14:05:19";
      this.dateTimeEditorValue1.Value = new DateTime?(new DateTime(2011, 1, 27, 14, 5, 19, 582));
      this.dateTimeEditorValue1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.radioButtonFilterAND.AllowAnimations = false;
      this.radioButtonFilterAND.AutoSize = true;
      this.radioButtonFilterAND.BackColor = Color.Transparent;
      this.radioButtonFilterAND.CheckMarkColor = Color.Black;
      this.radioButtonFilterAND.Flat = true;
      this.radioButtonFilterAND.Image = (Image) null;
      this.radioButtonFilterAND.Location = new Point(27, 194);
      this.radioButtonFilterAND.Name = "radioButtonFilterAND";
      this.radioButtonFilterAND.Size = new Size(44, 17);
      this.radioButtonFilterAND.TabIndex = 3;
      this.radioButtonFilterAND.TabStop = true;
      this.radioButtonFilterAND.Text = "&And";
      this.radioButtonFilterAND.UseThemeCheckMarkColors = false;
      this.radioButtonFilterAND.UseThemeFont = false;
      this.radioButtonFilterAND.UseThemeTextColor = false;
      this.radioButtonFilterAND.UseVisualStyleBackColor = true;
      this.radioButtonFilterAND.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.radioButtonFilterOR.AllowAnimations = false;
      this.radioButtonFilterOR.AutoSize = true;
      this.radioButtonFilterOR.BackColor = Color.Transparent;
      this.radioButtonFilterOR.CheckMarkColor = Color.Black;
      this.radioButtonFilterOR.Flat = true;
      this.radioButtonFilterOR.Image = (Image) null;
      this.radioButtonFilterOR.Location = new Point(77, 194);
      this.radioButtonFilterOR.Name = "radioButtonFilterOR";
      this.radioButtonFilterOR.Size = new Size(36, 17);
      this.radioButtonFilterOR.TabIndex = 4;
      this.radioButtonFilterOR.TabStop = true;
      this.radioButtonFilterOR.Text = "&Or";
      this.radioButtonFilterOR.UseThemeCheckMarkColors = false;
      this.radioButtonFilterOR.UseThemeFont = false;
      this.radioButtonFilterOR.UseThemeTextColor = false;
      this.radioButtonFilterOR.UseVisualStyleBackColor = true;
      this.radioButtonFilterOR.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.textBoxValue2.AllowAnimations = false;
      this.textBoxValue2.BackColor = Color.White;
      this.textBoxValue2.BoundsOffset = new Size(0, 0);
      this.textBoxValue2.ControlBorderColor = Color.FromArgb(39, 39, 39);
      this.textBoxValue2.ControlHighlightBorderColor = Color.DimGray;
      this.textBoxValue2.DefaultText = "";
      this.textBoxValue2.DefaultTextColor = Color.Black;
      this.textBoxValue2.DefaultTextFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxValue2.EnableBorderHighlight = false;
      this.textBoxValue2.GleamWidth = 1;
      this.textBoxValue2.Location = new Point(13, 277);
      this.textBoxValue2.MaxLength = (int) short.MaxValue;
      this.textBoxValue2.Multiline = false;
      this.textBoxValue2.Name = "textBoxValue2";
      this.textBoxValue2.PasswordChar = char.MinValue;
      this.textBoxValue2.Readonly = false;
      this.textBoxValue2.ScrollBars = ScrollBars.None;
      this.textBoxValue2.SelectionLength = 0;
      this.textBoxValue2.SelectionStart = 0;
      this.textBoxValue2.ShowDefaultText = false;
      this.textBoxValue2.Size = new Size(217, 23);
      this.textBoxValue2.TabIndex = 6;
      this.textBoxValue2.TextAlign = HorizontalAlignment.Left;
      this.textBoxValue2.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.comboBoxFilters2.AutoCompleteEnabled = false;
      this.comboBoxFilters2.BackColor = Color.White;
      this.comboBoxFilters2.BorderColor = Color.Black;
      this.comboBoxFilters2.DisplayMember = "";
      this.comboBoxFilters2.DropDownArrowBackgroundEnabled = true;
      this.comboBoxFilters2.DropDownArrowColor = Color.Black;
      this.comboBoxFilters2.DropDownHeight = 100;
      this.comboBoxFilters2.DropDownMaximumSize = new Size(1000, 1000);
      this.comboBoxFilters2.DropDownMinimumSize = new Size(10, 10);
      this.comboBoxFilters2.DropDownResizeDirection = SizingDirection.Both;
      this.comboBoxFilters2.DropDownWidth = 131;
      this.comboBoxFilters2.ItemHeight = 17;
      this.comboBoxFilters2.Location = new Point(13, 225);
      this.comboBoxFilters2.Name = "comboBoxFilters2";
      this.comboBoxFilters2.RoundedCornersMaskListItem = (byte) 15;
      this.comboBoxFilters2.RoundedCornersRadiusListItem = 3;
      this.comboBoxFilters2.Size = new Size(217, 23);
      this.comboBoxFilters2.TabIndex = 5;
      this.comboBoxFilters2.Text = "vComboBox2";
      this.comboBoxFilters2.UseThemeBackColor = false;
      this.comboBoxFilters2.UseThemeBorderColor = true;
      this.comboBoxFilters2.UseThemeDropDownArrowColor = true;
      this.comboBoxFilters2.UseThemeFont = true;
      this.comboBoxFilters2.UseThemeForeColor = false;
      this.comboBoxFilters2.ValueMember = "";
      this.comboBoxFilters2.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.comboBoxFilters2.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.btnFilterWindowOk.BackColor = Color.Transparent;
      this.btnFilterWindowOk.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.SOLID;
      this.btnFilterWindowOk.DialogResult = DialogResult.OK;
      this.btnFilterWindowOk.HighlightTextColor = Color.Black;
      this.btnFilterWindowOk.Location = new Point(70, 312);
      this.btnFilterWindowOk.Name = "btnFilterWindowOk";
      this.btnFilterWindowOk.PressedTextColor = Color.Black;
      this.btnFilterWindowOk.RoundedCornersMask = (byte) 15;
      this.btnFilterWindowOk.Size = new Size(75, 23);
      this.btnFilterWindowOk.StyleKey = "Button";
      this.btnFilterWindowOk.TabIndex = 7;
      this.btnFilterWindowOk.Text = "&Ok";
      this.btnFilterWindowOk.UseThemeTextColor = true;
      this.btnFilterWindowOk.UseVisualStyleBackColor = true;
      this.btnFilterWindowOk.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.btnFilterWindowCancel.BackColor = Color.Transparent;
      this.btnFilterWindowCancel.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.SOLID;
      this.btnFilterWindowCancel.DialogResult = DialogResult.Cancel;
      this.btnFilterWindowCancel.HighlightTextColor = Color.Black;
      this.btnFilterWindowCancel.Location = new Point(151, 312);
      this.btnFilterWindowCancel.Name = "btnFilterWindowCancel";
      this.btnFilterWindowCancel.PressedTextColor = Color.Black;
      this.btnFilterWindowCancel.RoundedCornersMask = (byte) 15;
      this.btnFilterWindowCancel.Size = new Size(75, 23);
      this.btnFilterWindowCancel.StyleKey = "Button";
      this.btnFilterWindowCancel.TabIndex = 8;
      this.btnFilterWindowCancel.Text = "&Cancel";
      this.btnFilterWindowCancel.UseThemeTextColor = true;
      this.btnFilterWindowCancel.UseVisualStyleBackColor = true;
      this.btnFilterWindowCancel.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.labelValue1.BackColor = Color.Transparent;
      this.labelValue1.DisplayStyle = LabelItemStyle.TextOnly;
      this.labelValue1.Ellipsis = false;
      this.labelValue1.ImageAlignment = ContentAlignment.TopLeft;
      this.labelValue1.Location = new Point(13, 140);
      this.labelValue1.Multiline = false;
      this.labelValue1.Name = "labelValue1";
      this.labelValue1.PaintBorder = false;
      this.labelValue1.PaintFill = false;
      this.labelValue1.Size = new Size(131, 15);
      this.labelValue1.TabIndex = 9;
      this.labelValue1.Text = "Value:";
      this.labelValue1.TextAlignment = ContentAlignment.TopLeft;
      this.labelValue1.UseMnemonics = true;
      this.labelValue1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.vLabel1.BackColor = Color.Transparent;
      this.vLabel1.DisplayStyle = LabelItemStyle.TextOnly;
      this.vLabel1.Ellipsis = false;
      this.vLabel1.ImageAlignment = ContentAlignment.TopLeft;
      this.vLabel1.Location = new Point(13, 259);
      this.vLabel1.Multiline = false;
      this.vLabel1.Name = "vLabel1";
      this.vLabel1.PaintBorder = false;
      this.vLabel1.PaintFill = false;
      this.vLabel1.Size = new Size(131, 17);
      this.vLabel1.TabIndex = 10;
      this.vLabel1.Text = "Value:";
      this.vLabel1.TextAlignment = ContentAlignment.TopLeft;
      this.vLabel1.UseMnemonics = true;
      this.vLabel1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.textBlockFilterBy.BackColor = Color.Transparent;
      this.textBlockFilterBy.DisplayStyle = LabelItemStyle.TextOnly;
      this.textBlockFilterBy.Ellipsis = false;
      this.textBlockFilterBy.ImageAlignment = ContentAlignment.TopLeft;
      this.textBlockFilterBy.Location = new Point(13, 35);
      this.textBlockFilterBy.Multiline = false;
      this.textBlockFilterBy.Name = "textBlockFilterBy";
      this.textBlockFilterBy.PaintBorder = false;
      this.textBlockFilterBy.PaintFill = false;
      this.textBlockFilterBy.Size = new Size(217, 21);
      this.textBlockFilterBy.TabIndex = 11;
      this.textBlockFilterBy.Text = "Filter by: ";
      this.textBlockFilterBy.TextAlignment = ContentAlignment.TopLeft;
      this.textBlockFilterBy.UseMnemonics = true;
      this.textBlockFilterBy.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.buttonCustomFilter.BackColor = Color.Transparent;
      this.buttonCustomFilter.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.SOLID;
      this.buttonCustomFilter.HighlightTextColor = Color.Black;
      this.buttonCustomFilter.Location = new Point(142, 62);
      this.buttonCustomFilter.Name = "buttonCustomFilter";
      this.buttonCustomFilter.PressedTextColor = Color.Black;
      this.buttonCustomFilter.RoundedCornersMask = (byte) 15;
      this.buttonCustomFilter.Size = new Size(88, 23);
      this.buttonCustomFilter.StyleKey = "Button";
      this.buttonCustomFilter.TabIndex = 12;
      this.buttonCustomFilter.Text = "Custom Filter";
      this.buttonCustomFilter.Toggle = CheckState.Unchecked;
      this.buttonCustomFilter.UseThemeTextColor = true;
      this.buttonCustomFilter.UseVisualStyleBackColor = false;
      this.buttonCustomFilter.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.buttonCustomFilter.ToggleStateChanged += new EventHandler(this.buttonCustomFilter_ToggleStateChanged);
      this.filterCheckedListBox.AllowAnimations = false;
      this.filterCheckedListBox.AllowDragDrop = false;
      this.filterCheckedListBox.AllowMultipleSelection = false;
      this.filterCheckedListBox.AllowSelection = true;
      this.filterCheckedListBox.BackColor = Color.White;
      this.filterCheckedListBox.BorderColor = Color.Black;
      this.filterCheckedListBox.CheckOnClick = false;
      this.filterCheckedListBox.DropFeedbackColor = Color.Blue;
      this.filterCheckedListBox.HotTrack = true;
      this.filterCheckedListBox.ItemHeight = 17;
      this.filterCheckedListBox.Location = new Point(17, 90);
      this.filterCheckedListBox.Name = "filterCheckedListBox";
      this.filterCheckedListBox.RoundedCornersMaskListItem = (byte) 15;
      this.filterCheckedListBox.RoundedCornersRadiusListItem = 1;
      this.filterCheckedListBox.Size = new Size(214, 213);
      this.filterCheckedListBox.SmartScrollEnabled = false;
      this.filterCheckedListBox.TabIndex = 14;
      this.filterCheckedListBox.Text = "vCheckedListBox1";
      this.filterCheckedListBox.UseThemeBackColor = true;
      this.filterCheckedListBox.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.filterCheckedListBox.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.dateTimeEditorValue2.BackColor = Color.White;
      this.dateTimeEditorValue2.Culture = new CultureInfo("");
      this.dateTimeEditorValue2.DefaultDateTimeFormat = DefaultDateTimePatterns.Custom;
      this.dateTimeEditorValue2.FormatValue = "";
      this.dateTimeEditorValue2.Location = new Point(11, 277);
      this.dateTimeEditorValue2.Name = "dateTimeEditorValue2";
      this.dateTimeEditorValue2.Size = new Size(216, 23);
      this.dateTimeEditorValue2.TabIndex = 0;
      this.dateTimeEditorValue2.Text = "01/27/2011 14:05:50";
      this.dateTimeEditorValue2.Value = new DateTime?(new DateTime(2011, 1, 27, 14, 5, 50, 803));
      this.dateTimeEditorValue2.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnFilterWindowCancel;
      this.ClientSize = new Size(252, 350);
      this.Controls.Add((Control) this.dateTimeEditorValue1);
      this.Controls.Add((Control) this.dateTimeEditorValue2);
      this.Controls.Add((Control) this.filterCheckedListBox);
      this.Controls.Add((Control) this.buttonCustomFilter);
      this.Controls.Add((Control) this.textBlockFilterBy);
      this.Controls.Add((Control) this.vLabel1);
      this.Controls.Add((Control) this.labelValue1);
      this.Controls.Add((Control) this.btnFilterWindowCancel);
      this.Controls.Add((Control) this.btnFilterWindowOk);
      this.Controls.Add((Control) this.textBoxValue2);
      this.Controls.Add((Control) this.comboBoxFilters2);
      this.Controls.Add((Control) this.radioButtonFilterOR);
      this.Controls.Add((Control) this.radioButtonFilterAND);
      this.Controls.Add((Control) this.textBoxValue1);
      this.Controls.Add((Control) this.comboBoxFilters1);
      this.Controls.Add((Control) this.labelShowItemsWhere);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FilterForm";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Filter Criteria Definition";
      this.Controls.SetChildIndex((Control) this.maximizeButton, 0);
      this.Controls.SetChildIndex((Control) this.closeButton, 0);
      this.Controls.SetChildIndex((Control) this.minimizeButton, 0);
      this.Controls.SetChildIndex((Control) this.labelShowItemsWhere, 0);
      this.Controls.SetChildIndex((Control) this.comboBoxFilters1, 0);
      this.Controls.SetChildIndex((Control) this.textBoxValue1, 0);
      this.Controls.SetChildIndex((Control) this.radioButtonFilterAND, 0);
      this.Controls.SetChildIndex((Control) this.radioButtonFilterOR, 0);
      this.Controls.SetChildIndex((Control) this.comboBoxFilters2, 0);
      this.Controls.SetChildIndex((Control) this.textBoxValue2, 0);
      this.Controls.SetChildIndex((Control) this.btnFilterWindowOk, 0);
      this.Controls.SetChildIndex((Control) this.btnFilterWindowCancel, 0);
      this.Controls.SetChildIndex((Control) this.labelValue1, 0);
      this.Controls.SetChildIndex((Control) this.vLabel1, 0);
      this.Controls.SetChildIndex((Control) this.textBlockFilterBy, 0);
      this.Controls.SetChildIndex((Control) this.buttonCustomFilter, 0);
      this.Controls.SetChildIndex((Control) this.filterCheckedListBox, 0);
      this.Controls.SetChildIndex((Control) this.dateTimeEditorValue2, 0);
      this.textBoxValue1.ResumeLayout(false);
      this.textBoxValue1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
