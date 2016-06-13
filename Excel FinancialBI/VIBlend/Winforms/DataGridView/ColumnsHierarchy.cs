// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ColumnsHierarchy
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.Drawing;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a ColumnsHierarchy</summary>
  public class ColumnsHierarchy : Hierarchy
  {
    private Dictionary<HierarchyItem, int> proportions = new Dictionary<HierarchyItem, int>();
    private bool rendering;
    private bool isAutoStretchColumns;

    internal override bool IsColumnsHierarchy
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    /// Gets or sets whether to automatically resize the columns when the total width is less than the width of the grid control.
    /// </summary>
    public bool AutoStretchColumns
    {
      get
      {
        return this.isAutoStretchColumns;
      }
      set
      {
        this.isAutoStretchColumns = value;
        this.PreRenderRequired = true;
        if (!value)
          return;
        this.ResizeColumnsToFitGridWidth();
      }
    }

    /// <summary>Gets the Height of the Hierarchy in pixels</summary>
    public int Height
    {
      get
      {
        return this.bounds.Bottom - this.bounds.Top;
      }
    }

    /// <summary>Gets the Width of the Hierarchy in pixels</summary>
    public int Width
    {
      get
      {
        return this.bounds.Right - this.bounds.Left;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="gridControl">Reference to the grid control hosting the hierarchy</param>
    public ColumnsHierarchy(vDataGridView gridControl)
    {
      this.gridControl = gridControl;
      this.hierarchyItems.parentHierarchy = (Hierarchy) this;
      this.AutoStretchColumns = false;
    }

    internal override void Draw()
    {
      Graphics graphics = this.Graphics;
      if (this.PreRenderRequired)
        this.PreRender();
      Rectangle rectangle = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width + 1000, this.bounds.Height);
      if (this.RenderedItems.Count + this.RenderedSummaryItems.Count == 0)
        return;
      int x = this.bounds.X;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        List<HierarchyItem> hierarchyItemList = index1 == 0 ? this.RenderedItems : this.RenderedSummaryItems;
        int num = index1 == 0 ? this.Items.FixedItemsCount : this.SummaryItems.FixedItemsCount;
        foreach (HierarchyItem hierarchyItem in hierarchyItemList)
        {
          if (hierarchyItem.HierarchyHost == null)
          {
            hierarchyItem.HierarchyHost = (Hierarchy) this;
            hierarchyItem.IsColumnsHierarchyItem = true;
          }
          if (!hierarchyItem.Hidden)
          {
            int widthWithChildren = hierarchyItem.WidthWithChildren;
            if (x + widthWithChildren < 0)
              x += widthWithChildren;
            else if (hierarchyItem.Draw(graphics))
              x += widthWithChildren;
            else
              break;
          }
        }
        if (index1 == 0)
        {
          for (int index2 = 0; index2 < num; ++index2)
          {
            HierarchyItem hierarchyItem = hierarchyItemList[index2];
            if (!hierarchyItem.Hidden && !hierarchyItem.Draw(graphics))
              break;
          }
        }
      }
    }

    internal override void PreRender()
    {
      if (this.PreRenderSuppressed || this.rendering)
        return;
      this.rendering = true;
      this.fixedAreaSize = 0;
      this.ApplyFilters();
      this.Resize();
      Rectangle rectangle = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width + 1000, this.bounds.Height);
      if (this.RenderedItems.Count + this.RenderedSummaryItems.Count == 0)
      {
        if (this.gridControl != null)
          this.gridControl.isSyncScrollRequired = true;
        this.rendering = false;
      }
      else
      {
        int x = 0;
        for (int index1 = 0; index1 < 2; ++index1)
        {
          List<HierarchyItem> hierarchyItemList = index1 == 0 ? this.RenderedItems : this.RenderedSummaryItems;
          int num = index1 == 0 ? this.Items.FixedItemsCount : this.SummaryItems.FixedItemsCount;
          foreach (HierarchyItem hierarchyItem in hierarchyItemList)
          {
            if (hierarchyItem.HierarchyHost == null)
            {
              hierarchyItem.HierarchyHost = (Hierarchy) this;
              hierarchyItem.IsColumnsHierarchyItem = true;
            }
            if (!hierarchyItem.Hidden)
            {
              int widthWithChildren = hierarchyItem.WidthWithChildren;
              if (hierarchyItem.PreRender(x, 0, this.gridControl.Graphics))
                x += widthWithChildren;
              else
                break;
            }
          }
          if (index1 == 0)
          {
            for (int index2 = 0; index2 < num; ++index2)
            {
              HierarchyItem hierarchyItem = hierarchyItemList[index2];
              if (!hierarchyItem.Hidden)
                this.fixedAreaSize += hierarchyItem.WidthWithChildren;
            }
          }
        }
        this.PreRenderRequired = false;
        if (this.gridControl != null)
          this.gridControl.isSyncScrollRequired = true;
        this.rendering = false;
      }
    }

    /// <summary>
    /// Resizes the width of the columns to fit to the grid's width.
    /// </summary>
    public void ResizeColumnsToFitGridWidth()
    {
      int width = this.gridControl.GetWidth();
      if (this.gridControl.vScroll.Visible || this.gridControl.RowsHierarchy.Height > this.gridControl.Height && this.gridControl.ScrollBarsEnabled)
        width -= Math.Max(this.gridControl.vScroll.Width, 15);
      if (this.bounds.X > 0)
        width -= this.bounds.X;
      if (!this.gridControl.vScroll.Visible)
        --width;
      bool flag = false;
      if (this.bounds.Width != width)
      {
        this.bounds.Width = width;
        flag = true;
      }
      if ((double) width < 0.0)
        return;
      List<HierarchyItem> pv = new List<HierarchyItem>();
      this.GetVisibleLeafLevelItems(ref pv);
      if (pv.Count == 0)
        return;
      int num1 = 0;
      for (int index = 0; index < pv.Count && (this.IsGroupingColumn(pv[index]) && this.gridControl.GroupingEnabled); ++index)
        ++num1;
      int index1 = pv.Count - 1;
      int num2 = 0;
      int num3 = -1;
      if (num3 != -1)
      {
        if (num3 == pv[index1].column)
        {
          --index1;
        }
        else
        {
          num1 = num3 + 1;
          for (int index2 = num1; index2 < pv.Count && pv[index2].column != num3; ++index2)
            index1 = index2;
        }
        this.proportions.Clear();
        for (int index2 = 0; index2 <= num3; ++index2)
          width -= pv[index2].Width;
        if (width < 0)
          return;
      }
      if (index1 <= num1)
        index1 = num1;
      for (int index2 = num1; index2 <= index1 && index2 < pv.Count; ++index2)
      {
        int num4 = pv[index2].Width;
        if (num4 <= HierarchyItem.MinimumWidth)
          num4 = HierarchyItem.MinimumWidth;
        if (!this.proportions.ContainsKey(pv[index2]))
        {
          this.proportions.Add(pv[index2], num4);
          num2 += num4;
        }
        else
          num2 += this.proportions[pv[index2]];
      }
      for (int index2 = num1; index2 <= index1 && index2 < pv.Count; ++index2)
      {
        int num4 = this.proportions[pv[index2]] * width / num2;
        if (num4 >= HierarchyItem.MinimumWidth)
          pv[index2].Width = num4;
      }
      if (!flag && !this.GridControl.hScroll.Visible)
        return;
      Size size = new Size(this.Width + (this.GridControl.RowsHierarchy.Visible ? this.GridControl.RowsHierarchy.Width : 0), this.GridControl.RowsHierarchy.Height + (this.Visible ? this.Height : 0));
      if (this.GridControl.ScrollableAreaSize.Width == size.Width)
        return;
      this.GridControl.ScrollableAreaSize = new Size(size.Width, this.GridControl.ScrollableAreaSize.Height);
      this.GridControl.SynchronizeScrollBars();
    }

    private void ResizeColumnsToFitGridWidth(int resizeColumn)
    {
      if (!this.AutoStretchColumns || this.gridControl == null || this.gridControl.hScroll.Visible)
        return;
      double num1 = (double) this.gridControl.Width;
      if (this.gridControl.vScroll.Visible || this.gridControl.RowsHierarchy.Height > this.gridControl.Height && this.gridControl.ScrollBarsEnabled)
        num1 -= (double) Math.Max(this.gridControl.vScroll.Width, 15);
      if (this.bounds.Width > 0)
        num1 -= (double) this.bounds.Width;
      if (this.bounds.X > 0)
        num1 -= (double) this.bounds.X;
      if (num1 < 0.0)
        return;
      List<HierarchyItem> pv = (List<HierarchyItem>) null;
      this.GetVisibleLeafLevelItems(ref pv);
      if (pv.Count == 0)
        return;
      int num2 = 0;
      for (int index = 0; index < pv.Count && (this.IsGroupingColumn(pv[index]) && this.gridControl.GroupingEnabled); ++index)
        ++num2;
      int index1 = pv.Count - 1;
      if (resizeColumn != -1)
      {
        if (resizeColumn == pv[index1].column)
        {
          --index1;
        }
        else
        {
          num2 = resizeColumn + 1;
          for (int index2 = num2; index2 < pv.Count && pv[index2].column != resizeColumn; ++index2)
            index1 = index2;
        }
      }
      if (index1 <= num2)
        index1 = num2;
      double num3 = num1 / (double) (index1 - num2 + 1);
      if (num3 < 1.0)
        num3 = 1.0;
      double num4 = 0.0;
      for (int index2 = num2; index2 <= index1 && index2 < pv.Count && num4 < num1; ++index2)
      {
        pv[index2].Width += (int) num3;
        num4 += num3;
        if (index2 == index1)
          pv[index2].Width += (int) (num1 - num4);
      }
      this.bounds.Width += (int) num1;
    }

    internal override void Resize()
    {
      int height = 0;
      int width = 0;
      for (int index = 0; index < 2; ++index)
      {
        foreach (HierarchyItem hierarchyItem in index == 0 ? this.RenderedItems : this.RenderedSummaryItems)
        {
          int num = hierarchyItem.HeightWithChildren + 1;
          if (height < num)
            height = num;
          width += hierarchyItem.WidthWithChildren;
        }
      }
      this.bounds = new Rectangle(this.bounds.Left, this.bounds.Top, width, height);
      if (this.gridControl != null && this.AutoStretchColumns)
      {
        if (this.GridControl.Capture && this.gridControl.resizingItem != null && this.gridControl.resizingItem.IsColumnsHierarchyItem)
          this.ResizeColumnsToFitGridWidth(this.gridControl.resizingItem.column);
        else
          this.ResizeColumnsToFitGridWidth(-1);
      }
      this.PreRenderRequired = true;
    }

    /// <summary>
    /// Resizes the items in the hierarchy for optimal visual appearance
    /// </summary>
    public override void AutoResize()
    {
      this.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT);
    }

    /// <summary>
    /// Resizes the items in the hierarchy for optimal visual appearance
    /// </summary>
    public override void AutoResize(AutoResizeMode autoResizeMode)
    {
      if (this.PreRenderRequired)
        this.PreRender();
      if (this.ColumnsCountRequresUpdate)
        this.UpdateColumnsCount();
      for (int index = 0; index < this.columnWidths.Count; ++index)
        this.columnWidths[index] = 0;
      this.UpdateColumnsIndexes();
      this.PreRenderSuppressed = true;
      foreach (HierarchyItem hierarchyItem in this.Items)
        hierarchyItem.AutoResize(autoResizeMode);
      foreach (HierarchyItem summaryItem in this.SummaryItems)
        summaryItem.AutoResize(autoResizeMode);
      this.Resize();
      this.PreRenderSuppressed = false;
    }

    internal override void UpdateColumnsIndexes()
    {
      List<HierarchyItem> hierarchyItemList = this.visibleLeafItems;
      for (int num, index = num = hierarchyItemList.Count - 1; index >= 0; --index)
      {
        HierarchyItem hierarchyItem1 = hierarchyItemList[index];
        hierarchyItem1.Column = index;
        HierarchyItem hierarchyItem2 = hierarchyItem1.ParentItem;
        while (hierarchyItem2 != null)
        {
          if (hierarchyItem2.Hierarchy != hierarchyItem1.Hierarchy)
          {
            hierarchyItem2 = (HierarchyItem) null;
          }
          else
          {
            hierarchyItem2.Column = index;
            hierarchyItem1 = hierarchyItem2;
            hierarchyItem2 = hierarchyItem1.ParentItem;
          }
        }
      }
    }

    internal bool IsGroupingColumn(HierarchyItem item)
    {
      return item != null && this.gridControl != null && (item.ItemIndex < this.gridControl.GroupingColumns.Count && this.gridControl.GroupingEnabled);
    }
  }
}
