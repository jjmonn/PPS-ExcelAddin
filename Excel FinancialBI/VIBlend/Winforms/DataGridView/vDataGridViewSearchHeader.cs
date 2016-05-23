// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.vDataGridViewSearchHeader
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView.Filters;

namespace VIBlend.WinForms.DataGridView
{
  [Description("Displays DataGrid Search Header")]
  [Designer("VIBlend.WinForms.Controls.Design.vDataGridViewSearchHeaderDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vDataGridViewSearchHeader : Control
  {
    private bool renderRequired = true;
    private bool synchronizeDataGridViewTheme = true;
    private GridTheme theme = new GridTheme();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Color textColor = Color.Black;
    private vComboBox searchbycombo = new vComboBox();
    private vComboBox searchconditions = new vComboBox();
    private vLabel label = new vLabel();
    private vLabel label2 = new vLabel();
    private vButton button = new vButton();
    private vTextBox textbox = new vTextBox();
    private List<int> totalFoundRowsCount = new List<int>();
    private bool showFoundRowsCount = true;
    private List<int> foundRows = new List<int>();
    private string searchbystring = "Search By:";
    private string gostring = "Go";
    private string condition = "Condition:";
    private vDataGridView associatedDataGrid;

    /// <summary>Gets or sets the associated data grid view.</summary>
    /// <value>The data grid view.</value>
    [Category("Behavior")]
    [Description("Gets or sets the associated data grid view.")]
    public vDataGridView DataGridView
    {
      get
      {
        return this.associatedDataGrid;
      }
      set
      {
        if (this.associatedDataGrid != null)
          this.UnwireEvents();
        this.associatedDataGrid = value;
        if (value == null)
          return;
        this.WireEvents();
        this.renderRequired = true;
        this.VIBlendTheme = value.VIBlendTheme;
        this.Theme = value.Theme;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is theme synchronized.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is theme synchronized; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool IsThemeSynchronized
    {
      get
      {
        return this.synchronizeDataGridViewTheme;
      }
      set
      {
        this.synchronizeDataGridViewTheme = value;
      }
    }

    [Description("Gets or sets the grid theme")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        this.theme = value;
        this.Refresh();
      }
    }

    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        try
        {
          this.Theme = GridTheme.GetDefaultTheme(value);
          this.defaultTheme = value;
          this.Invalidate();
          this.SetTheme(value);
        }
        catch (Exception ex)
        {
        }
      }
    }

    /// <summary>Gets or sets the color of the text.</summary>
    /// <value>The color of the text.</value>
    [Category("Appearance")]
    public Color TextColor
    {
      get
      {
        return this.textColor;
      }
      set
      {
        this.textColor = value;
        this.label.ForeColor = value;
        this.label2.ForeColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the count of found rows.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShowFoundRowsCount
    {
      get
      {
        return this.showFoundRowsCount;
      }
      set
      {
        this.showFoundRowsCount = value;
      }
    }

    /// <summary>Gets or sets the search by string.</summary>
    /// <value>The search by string.</value>
    [Category("Behavior")]
    [DefaultValue("Search By:")]
    public string SearchByString
    {
      get
      {
        return this.searchbystring;
      }
      set
      {
        this.searchbystring = value;
        this.label.Text = value;
      }
    }

    /// <summary>Gets or sets the Go string.</summary>
    /// <value>The Go string.</value>
    [Category("Behavior")]
    [DefaultValue("Go")]
    public string GoString
    {
      get
      {
        return this.gostring;
      }
      set
      {
        this.gostring = value;
        this.button.Text = value;
      }
    }

    /// <summary>Gets or sets the Condition string.</summary>
    /// <value>The Condition string.</value>
    [DefaultValue("Condition:")]
    [Category("Behavior")]
    public string Condition
    {
      get
      {
        return this.condition;
      }
      set
      {
        this.condition = value;
        this.label2.Text = value;
      }
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.UnwireEvents();
    }

    /// <summary>Wires the events.</summary>
    public void WireEvents()
    {
      if (this.DataGridView == null)
        return;
      this.DataGridView.BindingComplete += new vDataGridView.DataBindingCompleteEventHandler(this.DataGridView_BindingComplete);
    }

    private void DataGridView_BindingComplete(object sender, EventArgs e)
    {
      this.RefreshSearchHeader();
    }

    private void DataGridView_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!e.PropertyName.Equals("VIBlendTheme") || !this.IsThemeSynchronized)
        return;
      this.VIBlendTheme = this.DataGridView.VIBlendTheme;
    }

    /// <summary>Unwires the events.</summary>
    public void UnwireEvents()
    {
      if (this.DataGridView == null)
        return;
      this.DataGridView.GroupingColumns.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.GroupingColumns_CollectionChanged);
      this.DataGridView.PropertyChanged -= new PropertyChangedEventHandler(this.DataGridView_PropertyChanged);
    }

    private void GroupingColumns_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      if (this.DataGridView.suspendGroupingColumnsCollectionChanged)
        return;
      this.renderRequired = true;
      this.DataGridView.Refresh();
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
    }

    /// <summary>Sets the theme.</summary>
    /// <param name="theme">The theme.</param>
    public void SetTheme(VIBLEND_THEME theme)
    {
      this.searchbycombo.VIBlendTheme = theme;
      this.searchconditions.VIBlendTheme = theme;
      this.label.VIBlendTheme = theme;
      this.label2.VIBlendTheme = theme;
      this.button.VIBlendTheme = theme;
      this.textbox.VIBlendTheme = theme;
    }

    /// <summary>Refreshes the search header.</summary>
    public void RefreshSearchHeader()
    {
      this.Controls.Clear();
      this.Invalidate();
    }

    /// <summary>Refreshes the control.</summary>
    public void RefreshControl()
    {
      if (this.DataGridView == null)
        return;
      this.Controls.Clear();
      this.button.Click -= new EventHandler(this.button_Click);
      this.button.Click += new EventHandler(this.button_Click);
      this.label.Text = this.searchbystring;
      this.Controls.Add((Control) this.label);
      this.searchbycombo.Width = 100;
      this.searchbycombo.Height = 20;
      this.searchbycombo.VIBlendTheme = this.VIBlendTheme;
      this.label.VIBlendTheme = this.VIBlendTheme;
      this.label.Width = 100;
      Graphics graphics = this.CreateGraphics();
      this.label.Width = (int) graphics.MeasureString(this.label.Text, this.Font).Width + 10;
      for (int index = 0; index < this.DataGridView.ColumnsHierarchy.Items.Count; ++index)
      {
        HierarchyItem hierarchyItem = this.DataGridView.ColumnsHierarchy.Items[index];
        if (!hierarchyItem.Hidden)
          this.searchbycombo.Items.Add(new VIBlend.WinForms.Controls.ListItem()
          {
            Text = hierarchyItem.Caption,
            Value = (object) hierarchyItem
          });
      }
      this.searchbycombo.Location = new Point(this.label.Width + 5, 0);
      this.searchbycombo.SelectedIndex = 0;
      this.Controls.Add((Control) this.searchbycombo);
      List<string> stringList = new List<string>();
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorContains));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorContainsCaseSensitive));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorDoesNotContain));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorDoesNotContainCaseSensitive));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorStartsWith));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorStartsWithCaseSensitive));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorEndsWith));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorEndsWithCaseSensitive));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorEqual));
      stringList.Add(this.DataGridView.Localization.GetString(LocalizationNames.FilterOperatorEqualCaseSensitive));
      this.searchconditions.Items.Clear();
      foreach (string ItemText in stringList)
        this.searchconditions.Items.Add(ItemText);
      SizeF sizeF = graphics.MeasureString(this.condition, this.Font);
      this.label2.Text = this.condition;
      this.label2.Width = (int) sizeF.Width + 5;
      this.label2.Location = new Point(this.searchbycombo.Right + 5, 0);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.searchconditions);
      this.searchconditions.Width = 100;
      this.searchconditions.Height = 20;
      this.searchconditions.Location = new Point(this.label2.Right + 5, 0);
      this.searchconditions.SelectedIndex = 0;
      this.textbox.Width = 100;
      this.textbox.Height = 20;
      this.textbox.Location = new Point(this.searchconditions.Right + 5, 0);
      this.Controls.Add((Control) this.textbox);
      this.button.Width = (int) graphics.MeasureString(this.gostring, this.Font).Width + 10;
      this.button.Height = this.searchconditions.Height;
      this.button.Text = this.gostring;
      this.button.Location = new Point(this.textbox.Right + 5, 0);
      this.Controls.Add((Control) this.button);
      this.textbox.TextChanged += new EventHandler(this.textbox_TextChanged);
      this.searchbycombo.SelectedIndexChanged += new EventHandler(this.searchbycombo_SelectedIndexChanged);
      this.searchconditions.SelectedIndexChanged += new EventHandler(this.searchconditions_SelectedIndexChanged);
    }

    private void searchconditions_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.foundRows.Clear();
    }

    private void searchbycombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.foundRows.Clear();
    }

    private void textbox_TextChanged(object sender, EventArgs e)
    {
      this.foundRows.Clear();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.Controls.Count != 0)
        return;
      this.RefreshControl();
    }

    /// <summary>Finds all rows.</summary>
    /// <param name="searchValue">The search value.</param>
    /// <param name="column">The column.</param>
    /// <param name="comparisonOperator">The comparison operator.</param>
    /// <returns></returns>
    public List<int> FindAllRows(string searchValue, HierarchyItem column, StringFilterComparisonOperator comparisonOperator)
    {
      if (this.DataGridView == null)
        return new List<int>();
      FilterGroup<string> filterGroup = new FilterGroup<string>();
      StringFilter stringFilter = new StringFilter();
      stringFilter.Value = searchValue;
      filterGroup.AddFilter(FilterOperator.AND, (IFilterBase) stringFilter);
      stringFilter.ComparisonOperator = comparisonOperator;
      List<int> intList = new List<int>();
      List<HierarchyItem> pv = new List<HierarchyItem>();
      this.DataGridView.RowsHierarchy.GetVisibleLeafLevelItems(ref pv);
      for (int index = 0; index < pv.Count; ++index)
      {
        object cellValue = this.DataGridView.CellsArea.GetCellValue(pv[index], column);
        if (filterGroup.Evaluate(cellValue))
          intList.Add(index);
      }
      return intList;
    }

    private void button_Click(object sender, EventArgs e)
    {
      if (this.searchbycombo.SelectedIndex < 0)
        return;
      VIBlend.WinForms.Controls.ListItem listItem = this.searchbycombo.Items[this.searchbycombo.SelectedIndex];
      FilterGroup<string> filterGroup = new FilterGroup<string>();
      StringFilter stringFilter = new StringFilter();
      stringFilter.Value = this.textbox.Text;
      filterGroup.AddFilter(FilterOperator.AND, (IFilterBase) stringFilter);
      switch (this.searchconditions.SelectedIndex)
      {
        case 0:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.CONTAINS;
          break;
        case 1:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.CONTAINS_CASE_SENSITIVE;
          break;
        case 2:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.DOES_NOT_CONTAIN;
          break;
        case 3:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.DOES_NOT_CONTAIN_CASE_SENSITIVE;
          break;
        case 4:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.STARTS_WITH;
          break;
        case 5:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.STARTS_WITH_CASE_SENSITIVE;
          break;
        case 6:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.ENDS_WITH;
          break;
        case 7:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.ENDS_WITH_CASE_SENSITIVE;
          break;
        case 8:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.EQUAL;
          break;
        case 9:
          stringFilter.ComparisonOperator = StringFilterComparisonOperator.EQUAL_CASE_SENSITIVE;
          break;
      }
      List<HierarchyItem> pv = new List<HierarchyItem>();
      this.DataGridView.RowsHierarchy.GetVisibleLeafLevelItems(ref pv);
      HierarchyItem hierarchyItem = (HierarchyItem) listItem.Value;
      int num = -1;
      HierarchyItem rowItem1 = (HierarchyItem) null;
      if (this.foundRows.Count == 0 && this.ShowFoundRowsCount)
        this.totalFoundRowsCount = this.FindAllRows(this.textbox.Text, hierarchyItem, stringFilter.ComparisonOperator);
      for (int index = 0; index < pv.Count; ++index)
      {
        HierarchyItem rowItem2 = pv[index];
        object cellValue = this.DataGridView.CellsArea.GetCellValue(rowItem2, hierarchyItem);
        if (filterGroup.Evaluate(cellValue) && !this.foundRows.Contains(index))
        {
          num = index;
          rowItem1 = rowItem2;
          this.foundRows.Add(index);
          break;
        }
      }
      if (num == -1 && this.foundRows.Count > 0)
      {
        rowItem1 = pv[this.foundRows[0]];
        num = this.foundRows[0];
        this.foundRows.Clear();
        this.foundRows.Add(num);
      }
      if (num >= 0)
      {
        this.DataGridView.EnsureVisible(rowItem1);
        this.DataGridView.CellsArea.ClearSelection();
        this.DataGridView.RowsHierarchy.ClearSelection();
        if (this.DataGridView.SelectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT)
          this.DataGridView.RowsHierarchy.SelectItem(rowItem1);
        else
          this.DataGridView.CellsArea.SelectCell(rowItem1, hierarchyItem);
        this.DataGridView.Refresh();
      }
      if (!this.ShowFoundRowsCount)
        return;
      string text = this.GoString + " (" + (object) (this.totalFoundRowsCount.IndexOf(num) + 1) + "/" + (object) this.totalFoundRowsCount.Count + ")";
      this.button.Text = text;
      this.button.Width = (int) this.CreateGraphics().MeasureString(text, this.button.Font).Width + 15;
    }
  }
}
