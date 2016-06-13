// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.RowsHierarchy
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Collections.Generic;
using System.Drawing;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a Rows Hierarchy</summary>
  public class RowsHierarchy : Hierarchy
  {
    private List<HierarchyItem> itemsStart = new List<HierarchyItem>();
    private List<HierarchyItem> itemsEnd = new List<HierarchyItem>();
    internal int rBegScroll = -1;
    internal int rEndScroll = -1;
    private Point scrollOffsetSaved = new Point(0, 0);
    private int compactStyleRenderingItemsIndent = 15;
    private bool isCompactStyleRenderingEnabled;
    private bool rendering;

    internal override bool IsColumnsHierarchy
    {
      get
      {
        return false;
      }
    }

    internal int ScrollViewStartRow
    {
      get
      {
        this.UpdateScrollVisibility();
        return this.rBegScroll;
      }
    }

    internal int ScrollViewEndRow
    {
      get
      {
        this.UpdateScrollVisibility();
        return this.rEndScroll;
      }
    }

    /// <summary>
    /// Gets or sets whether the Rows hierarchy is rendered in compact style where each child item appears under the parent item instead of on its right side
    /// </summary>
    public bool CompactStyleRenderingEnabled
    {
      get
      {
        return this.isCompactStyleRenderingEnabled;
      }
      set
      {
        if (this.isCompactStyleRenderingEnabled == value)
          return;
        this.isCompactStyleRenderingEnabled = value;
        this.PreRenderRequired = true;
      }
    }

    /// <summary>
    /// Gets or sets the child items indent in the Rows hierarchy when the hierarchy is rendered in compact style.
    /// </summary>
    public int CompactStyleRenderingItemsIndent
    {
      get
      {
        return this.compactStyleRenderingItemsIndent;
      }
      set
      {
        this.compactStyleRenderingItemsIndent = value;
        this.PreRenderRequired = true;
      }
    }

    /// <summary>Gets the Height of the Hierarchy</summary>
    public int Height
    {
      get
      {
        return this.bounds.Height;
      }
    }

    /// <summary>Gets the Width of the Hierarchy</summary>
    public int Width
    {
      get
      {
        return this.bounds.Right - this.bounds.Left;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="gridControl">Reference to the grid control hosting the hierarchy</param>
    public RowsHierarchy(vDataGridView gridControl)
    {
      this.gridControl = gridControl;
      this.hierarchyItems.parentHierarchy = (Hierarchy) this;
      this.CompactStyleRenderingEnabled = false;
      this.CompactStyleRenderingItemsIndent = 15;
    }

    private void ResetVisibleRowsRange()
    {
      this.rBegScroll = -1;
      this.rEndScroll = -1;
      this.itemsStart.Clear();
      this.itemsEnd.Clear();
    }

    private void UpdateVisibleRowsRangeBasedOnScrollPosition()
    {
      this.PointToLeafItem(new Point(1, 1), ref this.rBegScroll);
      this.PointToLeafItem(new Point(1, this.gridControl.Height), ref this.rEndScroll);
      if (this.rBegScroll <= 0)
        this.rBegScroll = 0;
      if (this.rBegScroll < this.rEndScroll && this.rEndScroll < this.visibleLeafItems.Count)
        return;
      this.rEndScroll = this.visibleLeafItems.Count - 1;
    }

    private void UpdateScrollVisibility()
    {
      if (this.itemsStart.Count > 0 && this.itemsEnd.Count > 0 && this.scrollOffsetSaved.Y == this.gridControl.ScrollOffset.Y || this.visibleLeafItems.Count == 0)
        return;
      this.ResetVisibleRowsRange();
      this.UpdateVisibleRowsRangeBasedOnScrollPosition();
      HierarchyItem hierarchyItem1 = this.visibleLeafItems[this.rBegScroll];
      if (!this.IsColumnsHierarchy)
      {
        int num = this.CompactStyleRenderingEnabled ? 1 : 0;
      }
      for (; hierarchyItem1 != null; hierarchyItem1 = hierarchyItem1.ParentItem)
        this.itemsStart.Insert(0, hierarchyItem1);
      for (HierarchyItem hierarchyItem2 = this.visibleLeafItems[this.rEndScroll] ?? this.visibleLeafItems[this.visibleLeafItems.Count - 1]; hierarchyItem2 != null; hierarchyItem2 = hierarchyItem2.ParentItem)
        this.itemsEnd.Insert(0, hierarchyItem2);
      this.scrollOffsetSaved = this.gridControl.ScrollOffset;
    }

    internal override void Draw()
    {
      if (this.PreRenderRequired)
        this.PreRender();
      this.UpdateScrollVisibility();
      if (this.itemsStart.Count == 0 || this.itemsEnd.Count == 0)
        return;
      Rectangle rectangle = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width, this.bounds.Height + 1000);
      int y = this.bounds.Y;
      int count = this.hierarchyItems.ItemsRendered.Count;
      if (this.hierarchyItems.ItemsRendered.Count + this.hierarchySummaryItems.ItemsRendered.Count == 0)
        return;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int num1 = this.itemsEnd[0].ItemIndex;
        int num2 = this.itemsStart[0].ItemIndex;
        List<HierarchyItem> itemsRendered;
        if (index1 == 0)
        {
          itemsRendered = this.Items.ItemsRendered;
          if (itemsRendered.Count != 0 && !this.itemsStart[0].IsSummaryItem)
          {
            if (this.itemsEnd[0].IsSummaryItem)
              num1 = itemsRendered.Count - 1;
          }
          else
            continue;
        }
        else
        {
          itemsRendered = this.SummaryItems.ItemsRendered;
          if (!this.itemsStart[0].IsSummaryItem)
            num2 = 0;
          if (!this.itemsEnd[0].IsSummaryItem)
            continue;
        }
        if (num2 == -1)
          num2 = 0;
        if (num1 == -1)
          num1 = itemsRendered.Count - 1;
        for (int index2 = num2; index2 <= num1; ++index2)
        {
          HierarchyItem hierarchyItem = itemsRendered[index2];
          if (hierarchyItem.HierarchyHost == null)
          {
            hierarchyItem.HierarchyHost = (Hierarchy) this;
            hierarchyItem.IsColumnsHierarchyItem = false;
          }
          if (!hierarchyItem.Hidden)
          {
            int heightWithChildren = hierarchyItem.HeightWithChildren;
            if (y + heightWithChildren < 0)
              y += heightWithChildren;
            if (hierarchyItem.Draw(this.Graphics))
              y += heightWithChildren;
            else
              break;
          }
        }
        int num3 = index1 == 0 ? this.Items.FixedItemsCount : this.SummaryItems.FixedItemsCount;
        if (index1 == 0)
        {
          for (int index2 = 0; index2 < num3; ++index2)
          {
            HierarchyItem hierarchyItem = itemsRendered[index2];
            if (!hierarchyItem.Hidden && !hierarchyItem.Draw(this.Graphics))
              break;
          }
        }
      }
    }

    /// <summary>Sets the width of a column within the hierarchy</summary>
    /// <param name="columnIndex">Zero-based index of the column</param>
    /// <param name="width">New width in pixels</param>
    /// <returns>The method returns true if the new width is applied</returns>
    public override bool SetColumnWidth(int columnIndex, int width)
    {
      if (columnIndex >= this.columnWidths.Count)
        this.UpdateColumnsCount();
      if (this.CompactStyleRenderingEnabled)
        columnIndex = 0;
      this.columnWidths[columnIndex] = width;
      this.PreRenderRequired = true;
      return true;
    }

    internal override void Resize()
    {
      int height = 0;
      int width = 0;
      for (int index = 0; index < 2; ++index)
      {
        List<HierarchyItem> hierarchyItemList = index == 0 ? this.hierarchyItems.ItemsRendered : this.hierarchySummaryItems.ItemsRendered;
        if (hierarchyItemList != null)
        {
          foreach (HierarchyItem hierarchyItem in hierarchyItemList)
          {
            if (!hierarchyItem.Hidden)
            {
              height += hierarchyItem.HeightWithChildren;
              int widthWithChildren = hierarchyItem.WidthWithChildren;
              if (width < widthWithChildren)
                width = widthWithChildren;
            }
          }
        }
      }
      this.bounds = new Rectangle(this.bounds.Left, this.bounds.Top, width, height);
    }

    internal override void PreRender()
    {
      if (this.PreRenderSuppressed || this.rendering)
        return;
      this.rendering = true;
      if (this.ColumnsCountRequresUpdate)
        this.UpdateColumnsCount();
      while (this.ColumnsCount < this.HierarchyDepth)
        this.columnWidths.Add(HierarchyItem.DefaultColumnWidth);
      this.UpdateVisibleLeaves();
      if (this.CompactStyleRenderingEnabled)
      {
        if (this.columnWidths.Count == 0)
          this.AddColumn(HierarchyItem.DefaultColumnWidth);
        int num = this.columnWidths[0];
        int hierarchyDepth = this.HierarchyDepth;
        if (num < hierarchyDepth * this.CompactStyleRenderingItemsIndent)
          num = hierarchyDepth * this.CompactStyleRenderingItemsIndent;
        this.columnWidths[0] = num;
      }
      this.ApplyFilters();
      this.Resize();
      this.fixedAreaSize = 0;
      Rectangle rectangle = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width, this.bounds.Height + 1000);
      int y = 0;
      if (this.hierarchyItems.Count + this.hierarchySummaryItems.Count == 0)
      {
        if (this.gridControl != null)
          this.gridControl.isSyncScrollRequired = true;
        this.rendering = false;
      }
      else
      {
        for (int index1 = 0; index1 < 2; ++index1)
        {
          List<HierarchyItem> hierarchyItemList = index1 == 0 ? this.RenderedItems : this.RenderedSummaryItems;
          foreach (HierarchyItem hierarchyItem in hierarchyItemList)
          {
            if (hierarchyItem.HierarchyHost == null)
            {
              hierarchyItem.HierarchyHost = (Hierarchy) this;
              hierarchyItem.IsColumnsHierarchyItem = false;
            }
            if (!hierarchyItem.Hidden)
            {
              int heightWithChildren = hierarchyItem.HeightWithChildren;
              if (hierarchyItem.PreRender(0, y, this.Graphics))
                y += heightWithChildren;
              else
                break;
            }
          }
          int num = index1 == 0 ? this.Items.FixedItemsCount : this.SummaryItems.FixedItemsCount;
          if (index1 == 0)
          {
            for (int index2 = 0; index2 < num; ++index2)
            {
              HierarchyItem hierarchyItem = hierarchyItemList[index2];
              if (!hierarchyItem.Hidden)
                this.fixedAreaSize += hierarchyItem.HeightWithChildren;
            }
          }
        }
        this.ResetVisibleRowsRange();
        if (this.Width == 0)
          this.AutoResize();
        this.PreRenderRequired = false;
        this.rendering = false;
        if (this.gridControl == null)
          return;
        this.gridControl.isSyncScrollRequired = true;
      }
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
      this.PreRenderSuppressed = true;
      if (this.ColumnsCountRequresUpdate)
        this.UpdateColumnsCount();
      if (this.RenderedItems.Count + this.RenderedSummaryItems.Count == 0)
        return;
      for (int index = 0; index < this.columnWidths.Count; ++index)
        this.columnWidths[index] = 0;
      foreach (HierarchyItem renderedItem in this.RenderedItems)
        renderedItem.AutoResize(autoResizeMode);
      foreach (HierarchyItem renderedSummaryItem in this.RenderedSummaryItems)
        renderedSummaryItem.AutoResize(autoResizeMode);
      this.PreRenderSuppressed = false;
    }
  }
}
