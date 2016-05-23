// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ColumnChooser
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  public class ColumnChooser : RibbonForm
  {
    private vDataGridView dataGrid;
    private vCheckedListBox checkedListBox;

    /// <summary>Gets or sets the data grid.</summary>
    /// <value>The data grid.</value>
    public vDataGridView DataGrid
    {
      get
      {
        return this.dataGrid;
      }
      internal set
      {
        this.dataGrid = value;
        if (value == null)
          return;
        this.dataGrid.PropertyChanged += new PropertyChangedEventHandler(this.dataGrid_PropertyChanged);
        this.dataGrid.ColumnsHierarchy.Items.CollectionChanged += new EventHandler(this.Items_CollectionChanged);
        this.VIBlendTheme = this.dataGrid.VIBlendTheme;
        this.checkedListBox.VIBlendTheme = this.dataGrid.VIBlendTheme;
        this.Populate();
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.ColumnChooser" /> class.
    /// </summary>
    public ColumnChooser()
    {
      this.checkedListBox = new vCheckedListBox();
      this.TopLevel = true;
      this.checkedListBox.Dock = DockStyle.Fill;
      this.Controls.Add((Control) this.checkedListBox);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Size = new Size(200, 350);
      this.checkedListBox.CheckOnClick = true;
      this.MinimizeBox = false;
      this.MaximizeBox = false;
      this.ShowInTaskbar = false;
      this.ShowIcon = false;
      this.Text = "Column Chooser";
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.Focus();
    }

    private void Items_CollectionChanged(object sender, EventArgs e)
    {
      this.Populate();
    }

    /// <summary>Popuplates this instance.</summary>
    public void Populate()
    {
      this.checkedListBox.Items.Clear();
      for (int index = 0; index < this.DataGrid.ColumnsHierarchy.Items.Count; ++index)
      {
        HierarchyItem hierarchyItem = this.DataGrid.ColumnsHierarchy.Items[index];
        this.checkedListBox.Items.Add(new VIBlend.WinForms.Controls.ListItem()
        {
          Text = hierarchyItem.Caption,
          IsChecked = new bool?(!hierarchyItem.Hidden),
          Tag = (object) hierarchyItem
        });
        this.checkedListBox.ItemChecked -= new ItemCheckChangedEventHandler(this.checkedListBox_ItemChecked);
        this.checkedListBox.ItemChecked += new ItemCheckChangedEventHandler(this.checkedListBox_ItemChecked);
      }
    }

    private void checkedListBox_ItemChecked(object sender, ItemCheckChangedEventArgs args)
    {
      HierarchyItem hierarchyItem = (HierarchyItem) args.Item.Tag;
      bool flag = true;
      if (!args.Item.IsChecked.HasValue)
        flag = false;
      if (args.Item.IsChecked.HasValue && !args.Item.IsChecked.Value)
        flag = false;
      hierarchyItem.Hidden = !flag;
      this.DataGrid.Invalidate();
    }

    private void dataGrid_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "VIBlendTheme"))
        return;
      this.VIBlendTheme = this.dataGrid.VIBlendTheme;
      this.checkedListBox.VIBlendTheme = this.dataGrid.VIBlendTheme;
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.ClientSize = new Size(800, 788);
      this.Name = "ColumnChooser";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
    }
  }
}
