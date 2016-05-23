// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Hierarchy
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a base class for a Hierarchy which hosts one or more row, or column items
  /// </summary>
  /// <summary>
  /// Represents a base class for a Hierarchy which hosts one or more row, or column items
  /// </summary>
  public abstract class Hierarchy
  {
    internal Hashtable hashItemStyleNormal = new Hashtable();
    internal Hashtable hashItemStyleSelected = new Hashtable();
    internal Hashtable hashItemStyleDisabled = new Hashtable();
    private nsCollection<HierarchyItemFilter> listFilters = new nsCollection<HierarchyItemFilter>();
    private Hashtable itemValues = new Hashtable();
    private bool isVisible = true;
    private bool isPreRenderRequired = true;
    private bool showExpandCollapseButtons = true;
    private PropertyBagEx<HierarchyItem> itemProperties = new PropertyBagEx<HierarchyItem>();
    private bool allowResize = true;
    /// <exclude />
    private Dictionary<HierarchyItem, Hierarchy.ItemSelection> selectedItems = new Dictionary<HierarchyItem, Hierarchy.ItemSelection>();
    private bool isFixed;
    internal int fixedAreaSize;
    private int maxVisibleLevelDepth;
    private Graphics graphicsContext;
    internal bool SuspendWidthsReset;
    internal bool ColumnsCountRequresUpdate;
    /// <exclude />
    protected nsCollection<int> columnWidths;
    /// <exclude />
    protected HierarchyItemsCollection hierarchyItems;
    /// <exclude />
    protected HierarchyItemsCollection hierarchySummaryItems;
    internal Hashtable hashHiddenItems;
    /// <exclude />
    protected Rectangle bounds;
    internal vDataGridView gridControl;
    /// <exclude />
    protected List<HierarchyItem> visibleLeafItems;
    private bool allowDragDrop;

    /// <summary>
    /// Determines if the Hierarchy is fixed or not. Fixed Hierarchies are not affected by the scrolling of the data grid's content
    /// </summary>
    public bool Fixed
    {
      get
      {
        if (this.gridControl == null)
          return this.isFixed;
        Hierarchy hierarchy = this.IsColumnsHierarchy ? (Hierarchy) this.gridControl.RowsHierarchy : (Hierarchy) this.gridControl.ColumnsHierarchy;
        if (!this.isFixed && hierarchy.SummaryItems.FixedItemsCount <= 0)
          return hierarchy.Items.FixedItemsCount > 0;
        return true;
      }
      set
      {
        this.isFixed = value;
        this.PreRenderRequired = true;
      }
    }

    public nsCollection<HierarchyItemFilter> Filters
    {
      get
      {
        return this.listFilters;
      }
    }

    /// <summary>
    /// Checks if the Hierarchy contains HierarchyItems which are filtered.
    /// </summary>
    public bool IsFiltered
    {
      get
      {
        return this.IsFilteredInternal(this.Items) || this.IsFilteredInternal(this.SummaryItems);
      }
    }

    /// <summary>Checks if the Hierarchy is Sorted</summary>
    public bool IsSorted
    {
      get
      {
        return this.IsSortedInternal(this.Items) || this.IsSortedInternal(this.SummaryItems);
      }
    }

    /// <summary>Gets the Item the Hierarchy is sorted by.</summary>
    public HierarchyItem SortItem { get; internal set; }

    /// <summary>
    /// Gets the sort order direction if the Hierarchy is sorted.
    /// </summary>
    public SortingDirection SortingDirection { get; internal set; }

    internal Hashtable ItemValues
    {
      get
      {
        return this.itemValues;
      }
    }

    internal abstract bool IsColumnsHierarchy { get; }

    /// <summary>Determines whether the hierarchy is visible</summary>
    [DefaultValue(true)]
    public bool Visible
    {
      get
      {
        return this.isVisible;
      }
      set
      {
        if (this.isVisible == value)
          return;
        this.isVisible = value;
        if (this.gridControl == null)
          return;
        this.gridControl.PositionsRecompute();
        this.gridControl.isSyncScrollRequired = true;
        this.gridControl.SynchronizeScrollBars();
        this.gridControl.Refresh();
      }
    }

    /// <summary>Gets the items of the Hierarchy</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HierarchyItemsCollection Items
    {
      get
      {
        return this.hierarchyItems;
      }
    }

    /// <summary>Gets the summary items of the Hierarchy</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HierarchyItemsCollection SummaryItems
    {
      get
      {
        return this.hierarchySummaryItems;
      }
    }

    internal int MaxVisibleLevelDepth
    {
      get
      {
        if (this.maxVisibleLevelDepth == -1)
        {
          List<HierarchyItem> pv = (List<HierarchyItem>) null;
          this.GetVisibleLeafLevelItems(ref pv);
          this.maxVisibleLevelDepth = 0;
          foreach (HierarchyItem hierarchyItem in pv)
          {
            if (hierarchyItem.itemLevel > this.maxVisibleLevelDepth)
              this.maxVisibleLevelDepth = hierarchyItem.itemLevel;
          }
        }
        return this.maxVisibleLevelDepth;
      }
    }

    internal bool PreRenderRequired
    {
      get
      {
        return this.isPreRenderRequired;
      }
      set
      {
        this.isPreRenderRequired = value;
        if (!value)
          return;
        this.maxVisibleLevelDepth = -1;
      }
    }

    /// <summary>
    /// Determines whether to show the expand/collapse buttons on the items hosted by the hierarchy
    /// </summary>
    public bool ShowExpandCollapseButtons
    {
      get
      {
        return this.showExpandCollapseButtons;
      }
      set
      {
        this.showExpandCollapseButtons = value;
      }
    }

    /// <summary>Gets the depth of the hierarchy.</summary>
    /// <remarks>
    /// Use this method do termine how many levels of nesting exist in the hierarchy. For example if you have only a item with one or child items the depth is 2.
    /// If these child items have their own child items, the depth increases.
    /// </remarks>
    public int HierarchyDepth
    {
      get
      {
        int num = 0;
        foreach (HierarchyItem hierarchyItem in this.hierarchyItems)
        {
          int childItemsDepth = hierarchyItem.ChildItemsDepth;
          if (childItemsDepth > num)
            num = childItemsDepth;
        }
        foreach (HierarchyItem hierarchySummaryItem in this.hierarchySummaryItems)
        {
          int childItemsDepth = hierarchySummaryItem.ChildItemsDepth;
          if (childItemsDepth > num)
            num = childItemsDepth;
        }
        return num;
      }
    }

    /// <summary>
    /// Returns the horizontal position of Hierarchy relative to the top-left corner of the grid control
    /// </summary>
    public int X
    {
      get
      {
        if (!this.IsColumnsHierarchy && this.Fixed)
          return 0;
        return this.bounds.X;
      }
      internal set
      {
        this.bounds = new Rectangle(value, this.bounds.Y, this.bounds.Width, this.bounds.Height);
      }
    }

    /// <summary>
    /// Returns the horizontal position of Hierarchy relative to the top-left corner of the grid control
    /// </summary>
    public int Y
    {
      get
      {
        if (this.IsColumnsHierarchy && this.Fixed)
          return 0;
        return this.bounds.Y;
      }
      internal set
      {
        this.bounds = new Rectangle(this.bounds.X, value, this.bounds.Width, this.bounds.Height);
      }
    }

    /// <summary>
    /// Gets the total number of HierarchyItems hosted by the Hierarchy. This includes both Hierarchy items and Summary items. Hidden items are also counted.
    /// </summary>
    public int TotalItemsCount
    {
      get
      {
        int count = this.hierarchyItems.Count;
        for (int index = 0; index < this.hierarchyItems.Count; ++index)
          count += this.hierarchyItems[index].TotalItemsCount;
        for (int index = 0; index < this.hierarchySummaryItems.Count; ++index)
          count += this.hierarchySummaryItems[index].TotalItemsCount;
        return count;
      }
    }

    internal Graphics Graphics
    {
      get
      {
        return this.graphicsContext;
      }
      set
      {
        this.graphicsContext = value;
      }
    }

    internal List<HierarchyItem> RenderedItems
    {
      get
      {
        return this.hierarchyItems.ItemsRendered;
      }
    }

    internal List<HierarchyItem> RenderedSummaryItems
    {
      get
      {
        return this.hierarchySummaryItems.ItemsRendered;
      }
    }

    /// <summary>
    /// Retruns a reference to the Grid control hosting the hierarchy
    /// </summary>
    public vDataGridView GridControl
    {
      get
      {
        if (this.gridControl == null)
          return (vDataGridView) null;
        return this.gridControl;
      }
    }

    /// <summary>Gets the hidden items</summary>
    public HierarchyItem[] HiddenItems
    {
      get
      {
        HierarchyItem[] hierarchyItemArray = new HierarchyItem[this.hashHiddenItems.Count];
        IDictionaryEnumerator enumerator = this.hashHiddenItems.GetEnumerator();
        int num = 0;
        while (enumerator.MoveNext())
          hierarchyItemArray[num++] = (HierarchyItem) enumerator.Key;
        return hierarchyItemArray;
      }
    }

    internal int ColumnsCount
    {
      get
      {
        return this.columnWidths.Count;
      }
    }

    /// <summary>Gets the columns' widths for the hierarchy</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public nsCollection<int> ColumnWidths
    {
      get
      {
        return this.columnWidths;
      }
    }

    internal bool PreRenderSuppressed { get; set; }

    internal int VisibleLeafItemsCount
    {
      get
      {
        return this.visibleLeafItems.Count;
      }
    }

    internal PropertyBagEx<HierarchyItem> ItemProperties
    {
      get
      {
        return this.itemProperties;
      }
    }

    /// <summary>
    /// Gets or sets whether the HierarchyItems resizing is allowed
    /// </summary>
    public virtual bool AllowResize
    {
      get
      {
        return this.allowResize;
      }
      set
      {
        this.allowResize = value;
      }
    }

    /// <summary>
    /// Gets or sets whether drag and drop is enabled for this Hierarchy
    /// </summary>
    public bool AllowDragDrop
    {
      get
      {
        return this.allowDragDrop;
      }
      set
      {
        this.allowDragDrop = value;
      }
    }

    /// <summary>Gets the selected items</summary>
    public HierarchyItem[] SelectedItems
    {
      get
      {
        List<HierarchyItem> hierarchyItemList = new List<HierarchyItem>();
        Dictionary<HierarchyItem, Hierarchy.ItemSelection>.Enumerator enumerator = this.selectedItems.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Value.IsSelected)
            hierarchyItemList.Add(enumerator.Current.Key);
        }
        return hierarchyItemList.ToArray();
      }
    }

    /// <summary>Occurs when the Hierarchy filters have changed.</summary>
    /// <remarks>
    /// Use this event to capture filtering changes that affect the the HierarchyItems in the Hierarchy.
    /// </remarks>
    /// <seealso cref="P:VIBlend.WinForms.DataGridView.Hierarchy.IsFiltered" />
    public event EventHandler FiltersChanged;

    /// <summary>Occurs when the Hierarchy sorting have changed.</summary>
    /// <remarks>
    /// Use this event to capture sorting changes that affect the the HierarchyItems in the Hierarchy.
    /// </remarks>
    /// <seealso cref="P:VIBlend.WinForms.DataGridView.Hierarchy.IsSorted" />
    public event EventHandler SortingChanged;

    /// <summary>Constructor</summary>
    public Hierarchy()
    {
      this.gridControl = (vDataGridView) null;
      this.columnWidths = new nsCollection<int>();
      this.hierarchyItems = new HierarchyItemsCollection((HierarchyItem) null, this);
      this.hierarchySummaryItems = new HierarchyItemsCollection((HierarchyItem) null, this);
      this.visibleLeafItems = new List<HierarchyItem>();
      this.hashHiddenItems = new Hashtable();
      this.listFilters.CollectionChanged += new EventHandler(this.listFilters_CollectionChanged);
    }

    internal void ClearItemStyleOverrides(HierarchyItem item)
    {
      if (item == null)
        return;
      if (this.hashItemStyleNormal.ContainsKey((object) item))
        this.hashItemStyleNormal.Remove((object) item);
      if (this.hashItemStyleSelected.ContainsKey((object) item))
        this.hashItemStyleSelected.Remove((object) item);
      if (!this.hashItemStyleDisabled.ContainsKey((object) item))
        return;
      this.hashItemStyleDisabled.Remove((object) item);
    }

    private void listFilters_CollectionChanged(object sender, EventArgs e)
    {
      this.Items.IsFiltered = true;
      this.Items.IsFilteringInSync = false;
      this.PreRenderRequired = true;
    }

    private bool ApplyFiltersOnItem(HierarchyItem item)
    {
      if (item.IsTotal)
      {
        item.Filtered = false;
        return false;
      }
      item.Filtered = false;
      if (!item.IsRowsHierarchyItem || item.Depth >= this.gridControl.GroupingColumns.Count || !this.gridControl.GroupingEnabled)
      {
        foreach (HierarchyItemFilter listFilter in this.listFilters)
        {
          if (listFilter.Item.IsColumnsHierarchyItem == item.IsColumnsHierarchyItem)
            throw new InvalidOperationException("Filters cannot be applied on items from the same hierarchy");
          if (listFilter.Item.IsColumnsHierarchyItem == this.IsColumnsHierarchy)
            throw new InvalidOperationException("Filters cannot be applied on items from the same hierarchy");
          object obj = item.IsColumnsHierarchyItem ? this.gridControl.CellsArea.GetCellValue(listFilter.Item, item) : this.gridControl.CellsArea.GetCellValue(item, listFilter.Item);
          item.Filtered = !listFilter.Filter.Evaluate(obj);
          if (item.Filtered)
            break;
        }
      }
      if (!item.Filtered)
      {
        foreach (HierarchyItem hierarchyItem in item.Items)
          this.ApplyFiltersOnItem(hierarchyItem);
        foreach (HierarchyItem summaryItem in item.SummaryItems)
          this.ApplyFiltersOnItem(summaryItem);
        int num = item.Items.Count + item.SummaryItems.Count;
        item.Filtered = num != 0 && item.FilteredChildItemsCount == num;
      }
      return item.Filtered;
    }

    protected void ApplyFilters()
    {
      if (!this.Items.IsFilteringInSync)
      {
        foreach (HierarchyItem hierarchyItem in this.Items)
          this.ApplyFiltersOnItem(hierarchyItem);
      }
      if (!this.SummaryItems.IsFilteringInSync)
      {
        foreach (HierarchyItem summaryItem in this.SummaryItems)
          this.ApplyFiltersOnItem(summaryItem);
      }
      if (!this.SummaryItems.IsFilteringInSync || !this.Items.IsFilteringInSync)
        this.UpdateVisibleLeaves();
      this.Items.IsFilteringInSync = true;
      this.SummaryItems.IsFilteringInSync = true;
      this.OnFilterChanged(new EventArgs());
    }

    private bool IsFilteredInternal(HierarchyItemsCollection collection)
    {
      if (collection.IsFiltered)
        return true;
      foreach (HierarchyItem hierarchyItem in collection.Items)
      {
        if (this.IsFilteredInternal(hierarchyItem.Items) || this.IsFilteredInternal(hierarchyItem.SummaryItems))
          return true;
      }
      return false;
    }

    protected virtual void OnFilterChanged(EventArgs e)
    {
      if (this.FiltersChanged == null)
        return;
      this.FiltersChanged((object) this, e);
    }

    private bool IsSortedInternal(HierarchyItemsCollection collection)
    {
      if (collection.IsSorted)
        return true;
      foreach (HierarchyItem hierarchyItem in collection.Items)
      {
        if (this.IsSortedInternal(hierarchyItem.Items) || this.IsSortedInternal(hierarchyItem.SummaryItems))
          return true;
      }
      return false;
    }

    /// <summary>Sorts the Hierarchy by a specefic HierarchyItem</summary>
    /// <param name="item">HierarchyItem to Sort by. This must be a HierarchyItem from the other Hierarchy. For example, the ColumnsHierarchy can be sorted by HierarchyItems from the RowsHierarchy.</param>
    /// <param name="sortingDirection">Sorting direction.</param>
    public void SortBy(HierarchyItem item, SortingDirection sortingDirection)
    {
      this.SortBy(item, sortingDirection, (IComparer<HierarchyItem>) null);
    }

    /// <summary>Sorts the Items in the Hierarchy.</summary>
    /// <param name="item">HierarchyItem to Sort by. This must be a HierarchyItem from the other Hierarchy. For example, the ColumnsHierarchy can be sorted by HierarchyItems from the RowsHierarchy.</param>
    /// <param name="sortingDirection">Sorting direction.</param>
    /// <param name="comparer">HierarchyItem comparer.</param>
    public void SortBy(HierarchyItem item, SortingDirection sortingDirection, IComparer<HierarchyItem> comparer)
    {
      this.SortingDirection = sortingDirection;
      this.SortItem = item;
      if (comparer == null)
        comparer = (IComparer<HierarchyItem>) new Hierarchy.HierarchyItemComparer();
      this.Sort(comparer);
    }

    /// <summary>Sorts the HierarchyItems in the Hierarchy.</summary>
    /// <param name="comparer">Sort Comprarer.</param>
    private void Sort(IComparer<HierarchyItem> comparer)
    {
      if (comparer == null)
        return;
      this.CollectionSort(this.Items, comparer);
      this.CollectionSort(this.SummaryItems, comparer);
      this.PreRender();
      this.UpdateVisibleLeaves();
      if (this.IsColumnsHierarchy)
      {
        this.PreRenderRequired = true;
        this.ColumnsCountRequresUpdate = true;
      }
      this.OnSortingChanged(new EventArgs());
    }

    private void CollectionSort(HierarchyItemsCollection collection, IComparer<HierarchyItem> comparer)
    {
      if (this.gridControl == null || this.gridControl.CellsArea == null || (collection == null || collection.Count == 0))
        return;
      if (collection.IsSorted)
        this.CollectionRemoveSort(collection);
      foreach (HierarchyItem hierarchyItem in collection)
      {
        this.CollectionSort(hierarchyItem.Items, comparer);
        this.CollectionSort(hierarchyItem.SummaryItems, comparer);
      }
      HierarchyItem hierarchyItem1 = collection[collection.Count - 1];
      if (hierarchyItem1.IsTotal)
        collection.RemoveAt(collection.Count - 1);
      Hashtable hashtable = (Hashtable) null;
      if (this.gridControl.CellsArea.CellSpanGroupsCount > 0)
      {
        hashtable = new Hashtable();
        foreach (CellSpan cellSpans in this.gridControl.CellsArea.GetCellSpansList())
        {
          HierarchyItem hierarchyItem2;
          int num;
          if (collection.OwnerHierarchy.IsColumnsHierarchy)
          {
            hierarchyItem2 = cellSpans.ColumnItem;
            num = cellSpans.ColumnsCount;
          }
          else
          {
            hierarchyItem2 = cellSpans.RowItem;
            num = cellSpans.RowsCount;
          }
          if (hierarchyItem2.ParentItem == collection.OwnerItem)
          {
            List<HierarchyItem> hierarchyItemList = new List<HierarchyItem>();
            for (int index = 1; index < num; ++index)
              hierarchyItemList.Add(collection.ItemsRendered[hierarchyItem2.ItemIndex + index]);
            if (!hashtable.Contains((object) hierarchyItem2))
              hashtable.Add((object) hierarchyItem2, (object) hierarchyItemList);
          }
        }
        IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
        while (enumerator.MoveNext())
        {
          foreach (HierarchyItem hierarchyItem2 in (List<HierarchyItem>) enumerator.Value)
            collection.ItemsRendered.Remove(hierarchyItem2);
        }
      }
      if (collection.FixedItemsCount > 0)
      {
        List<HierarchyItem> hierarchyItemList = new List<HierarchyItem>();
        for (int index = 0; index < collection.ItemsRendered.Count && collection.ItemsRendered[index].Fixed; ++index)
          hierarchyItemList.Add(collection.ItemsRendered[index]);
        collection.ItemsRendered.RemoveRange(0, hierarchyItemList.Count);
        collection.ItemsRendered.Sort(comparer);
        hierarchyItemList.Sort(comparer);
        for (int index = 0; index < hierarchyItemList.Count; ++index)
          collection.ItemsRendered.Insert(index, hierarchyItemList[index]);
      }
      else
        collection.ItemsRendered.Sort(comparer);
      if (hashtable != null && hashtable.Count > 0)
      {
        IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
        while (enumerator.MoveNext())
        {
          HierarchyItem hierarchyItem2 = (HierarchyItem) enumerator.Key;
          List<HierarchyItem> hierarchyItemList = (List<HierarchyItem>) enumerator.Value;
          int num = collection.ItemsRendered.IndexOf(hierarchyItem2);
          collection.ItemsRendered.InsertRange(num + 1, (IEnumerable<HierarchyItem>) hierarchyItemList);
        }
      }
      if (hierarchyItem1.IsTotal)
        collection.Add(hierarchyItem1);
      for (int nIndex = 0; nIndex < collection.ItemsRendered.Count; ++nIndex)
        collection.ItemsRendered[nIndex].SetItemIndex(nIndex);
      collection.IsSorted = true;
    }

    /// <summary>
    /// Removes the sorting order previously applied through the SortBy method
    /// </summary>
    public void RemoveSort()
    {
      if (this.SortItem == null || !this.IsSorted || this.SortItem.IsColumnsHierarchyItem == this.IsColumnsHierarchy)
        return;
      this.CollectionRemoveSort(this.Items);
      this.PreRender();
      this.UpdateVisibleLeaves();
      if (this.IsColumnsHierarchy)
      {
        if (this.IsColumnsHierarchy)
          this.ColumnsCountRequresUpdate = true;
        this.Resize();
        this.PreRender();
      }
      this.SortItem = (HierarchyItem) null;
      this.OnSortingChanged(new EventArgs());
    }

    private void CollectionRemoveSort(HierarchyItemsCollection collection)
    {
      collection.RePopulateRenderedItemsBasedOnFixedState();
      foreach (HierarchyItem hierarchyItem in collection)
        this.CollectionRemoveSort(hierarchyItem.Items);
      collection.IsSorted = false;
    }

    protected virtual void OnSortingChanged(EventArgs e)
    {
      if (this.SortingChanged == null)
        return;
      this.SortingChanged((object) this, e);
    }

    internal abstract void Draw();

    /// <summary>Gets a HierarchyItem by its unique ID</summary>
    /// <param name="id">String which uniquely identifies the item within the hierarchy</param>
    /// <returns>The method returns the hierarchy item with the respective unique Id. If no such item exists the method returns NULL.</returns>
    public HierarchyItem GetItemByUniqueID(string id)
    {
      if (id == null || id == "")
        return (HierarchyItem) null;
      string[] strArray1 = id.Split('$');
      if (strArray1.Length == 0)
        return (HierarchyItem) null;
      int num1 = strArray1.Length - 1;
      HierarchyItemsCollection items = this.Items;
      string[] strArray2 = strArray1;
      int index1 = num1;
      int num2 = 1;
      int num3 = index1 - num2;
      int index2 = int.Parse(strArray2[index1], (IFormatProvider) CultureInfo.InvariantCulture);
      HierarchyItem hierarchyItem = items[index2];
      while (hierarchyItem != null && num3 > 0)
        hierarchyItem = hierarchyItem.Items[int.Parse(strArray1[num3--], (IFormatProvider) CultureInfo.InvariantCulture)];
      return hierarchyItem;
    }

    private int BinSearchItems(List<HierarchyItem> array, Point pt)
    {
      return this.BinSearchItems(array, pt, 0, array.Count);
    }

    private int BinSearchItems(List<HierarchyItem> array, Point pt, int arrStart, int arrEnd)
    {
      int num1 = arrStart;
      int num2 = arrEnd;
      if (num2 == num1)
        return -1;
      bool columnsHierarchyItem = array[0].IsColumnsHierarchyItem;
      while (num1 < num2)
      {
        int index = (num1 + num2) / 2;
        HierarchyItem hierarchyItem = array[index];
        if (columnsHierarchyItem)
        {
          if (hierarchyItem.position.X > pt.X)
          {
            num2 = index;
          }
          else
          {
            if ((index + 1 < num2 ? array[index + 1].position.X : hierarchyItem.position.X + hierarchyItem.Width) >= pt.X)
              return index;
            num1 = index + 1;
          }
        }
        else if (hierarchyItem.position.Y > pt.Y)
        {
          num2 = index;
        }
        else
        {
          if ((index + 1 < num2 ? array[index + 1].position.Y : hierarchyItem.position.Y + hierarchyItem.Height) >= pt.Y)
            return index;
          num1 = index + 1;
        }
      }
      return -1;
    }

    /// <summary>
    /// Finds the leaf level HierarchyItem at a specific horizontal or vertical offset
    /// </summary>
    /// <param name="point">Represent the offset relative to the grid's top-left corner</param>
    /// <param name="itemIndex">returns the index of the item</param>
    /// <returns>The leaf level HierarchyItem at the specified point. If no item is matched that method returns null.</returns>
    internal HierarchyItem PointToLeafItem(Point point, ref int itemIndex)
    {
      if (this.PreRenderRequired)
        this.PreRender();
      int num1 = 0;
      if (this.gridControl.ColumnsHierarchy.Visible && this.gridControl.ColumnsHierarchy.Fixed)
        num1 = this.gridControl.ColumnsHierarchy.Height;
      int num2 = 0;
      if (this.gridControl.RowsHierarchy.Visible && this.gridControl.RowsHierarchy.Fixed)
        num2 = this.gridControl.RowsHierarchy.Width;
      if (this.Items.ItemsRendered.Count > 1 && (this.IsColumnsHierarchy && this.fixedAreaSize > 0 && point.X <= this.fixedAreaSize + num2 || !this.IsColumnsHierarchy && this.fixedAreaSize > 0 && point.Y <= this.fixedAreaSize + num1))
      {
        Point point1 = point;
        if (this.IsColumnsHierarchy)
        {
          if (this.gridControl.RowsHierarchy.Visible && this.gridControl.RowsHierarchy.Fixed)
            point1.Offset(-(this.gridControl.RowsHierarchy.X + this.gridControl.RowsHierarchy.Width), 0);
          point1.Y = this.gridControl.ColumnsHierarchy.Height - 1;
        }
        else
        {
          if (this.gridControl.ColumnsHierarchy.Visible && this.gridControl.ColumnsHierarchy.Fixed)
            point1.Offset(0, -(this.gridControl.ColumnsHierarchy.Y + this.gridControl.ColumnsHierarchy.Height));
          point1.X = this.gridControl.RowsHierarchy.Width - 1;
        }
        for (int index1 = 0; index1 < 2; ++index1)
        {
          HierarchyItemsCollection hierarchyItemsCollection = index1 == 0 ? this.Items : this.SummaryItems;
          if (hierarchyItemsCollection.FixedItemsCount > 0)
          {
            int index2 = 0;
            while (hierarchyItemsCollection.ItemsRendered[index2].Fixed)
            {
              if (hierarchyItemsCollection.ItemsRendered[index2].Filtered || hierarchyItemsCollection.ItemsRendered[index2].Hidden)
              {
                ++index2;
              }
              else
              {
                HierarchyItem hierarchyItem1 = hierarchyItemsCollection.ItemsRendered[index2];
                int num3 = hierarchyItem1.IsColumnsHierarchyItem ? hierarchyItem1.X : hierarchyItem1.Y;
                int num4 = hierarchyItem1.IsColumnsHierarchyItem ? num3 + hierarchyItem1.WidthWithChildren : num3 + hierarchyItem1.HeightWithChildren;
                int num5 = hierarchyItem1.IsColumnsHierarchyItem ? point1.X : point1.Y;
                if (num5 >= num3 && num5 <= num4)
                {
                  if (hierarchyItem1.IsRowsHierarchyItem && this.gridControl.RowsHierarchy.CompactStyleRenderingEnabled)
                  {
                    List<HierarchyItem> list = new List<HierarchyItem>();
                    this.GetVisibleLeaves(ref list, hierarchyItem1, true);
                    Point pt = point;
                    pt.Offset(-this.X, -this.Y - this.gridControl.VerticalScroll);
                    int index3 = this.BinSearchItems(list, pt);
                    if (index3 == -1)
                      return hierarchyItem1;
                    itemIndex = index3;
                    return list[index3];
                  }
                  while (hierarchyItem1.Items.ItemsRendered.Count > 0 && hierarchyItem1.Expanded || hierarchyItem1.SummaryItems.ItemsRendered.Count > 0)
                  {
                    HierarchyItem hierarchyItem2 = hierarchyItem1;
                    for (int index3 = 0; index3 < 2; ++index3)
                    {
                      List<HierarchyItem> hierarchyItemList = index3 == 0 ? hierarchyItem1.Items.ItemsRendered : hierarchyItem1.SummaryItems.ItemsRendered;
                      for (int index4 = 0; index4 < hierarchyItemList.Count; ++index4)
                      {
                        HierarchyItem hierarchyItem3 = hierarchyItemList[index4];
                        int num6 = hierarchyItem3.IsColumnsHierarchyItem ? hierarchyItem3.X : hierarchyItem3.Y;
                        int num7 = hierarchyItem3.IsColumnsHierarchyItem ? num6 + hierarchyItem3.WidthWithChildren : num6 + hierarchyItem3.HeightWithChildren;
                        if (num5 >= num6 && num5 <= num7)
                        {
                          hierarchyItem1 = hierarchyItem3;
                          index3 = 3;
                          break;
                        }
                      }
                    }
                    if (hierarchyItem1 == hierarchyItem2)
                      break;
                  }
                  itemIndex = hierarchyItem1.ItemIndex;
                  return hierarchyItem1;
                }
                ++index2;
              }
            }
          }
        }
        return (HierarchyItem) null;
      }
      point.Offset(-this.X, -this.Y);
      List<HierarchyItem> pv = (List<HierarchyItem>) null;
      this.GetVisibleLeafLevelItems(ref pv);
      itemIndex = -1;
      int index = this.BinSearchItems(pv, point);
      if (index == -1 || index < 0 || index >= pv.Count)
        return (HierarchyItem) null;
      itemIndex = index;
      return pv[index];
    }

    internal HierarchyItem HitTest(Point point, bool forceHitTest)
    {
      if (this.PreRenderRequired)
        this.PreRender();
      if (!this.Visible && !forceHitTest)
        return (HierarchyItem) null;
      int itemIndex = -1;
      HierarchyItem leafItem = this.PointToLeafItem(point, ref itemIndex);
      if (itemIndex == -1)
        return (HierarchyItem) null;
      if (this.IsColumnsHierarchy || !((RowsHierarchy) this).CompactStyleRenderingEnabled)
      {
        Point pt = point;
        pt.Offset(-this.X, -this.Y);
        if (leafItem.Fixed)
        {
          if (leafItem.IsRowsHierarchyItem)
            pt.Offset(0, -this.gridControl.VerticalScroll);
          else
            pt.Offset(-this.gridControl.HorizontalScroll, 0);
        }
        HierarchyItem hierarchyItem1 = leafItem;
        HierarchyItem hierarchyItem2 = hierarchyItem1.HitTest(pt, false);
        if (hierarchyItem2 != null)
          return hierarchyItem2;
        while (hierarchyItem1 != null)
        {
          hierarchyItem1 = hierarchyItem1.ParentItem;
          if (hierarchyItem1 != null)
          {
            HierarchyItem hierarchyItem3 = hierarchyItem1.HitTest(pt, false);
            if (hierarchyItem3 != null)
              return hierarchyItem3;
          }
        }
      }
      if (leafItem.Fixed)
      {
        point.Offset(-this.X, -this.Y - this.gridControl.VerticalScroll);
        return leafItem.HitTest(point);
      }
      point.Offset(-this.X, -this.Y);
      return leafItem.HitTest(point);
    }

    /// <summary>
    /// Performs a HitTest at a specific point within the Hierarchy
    /// </summary>
    /// <param name="point">Point representing the X, Y coordinate to test. The point must be relative to the top-left corner of the grid control</param>
    /// <returns>The method returns the HierarchyItem located at the position pointed by the point parameter. If there is no HierarchyItem at this position the method returns NULL.</returns>
    public HierarchyItem HitTest(Point point)
    {
      return this.HitTest(point, false);
    }

    /// <summary>
    /// Returns the item displayed immediatelly after a specified item
    /// </summary>
    /// <param name="item">HierarchyItem to retrive the next item for.</param>
    /// <returns>The item after the specified item, or null if a next item does not exist.</returns>
    public HierarchyItem GetNextItem(HierarchyItem item)
    {
      if (this is ColumnsHierarchy)
        return this.HitTest(new Point(item.X + item.WidthWithChildren + 1, item.Y + 1));
      return this.HitTest(new Point(item.X + 1, item.Y + item.HeightWithChildren + 1));
    }

    /// <summary>
    /// Returns the item displayed immediatelly before a specified item
    /// </summary>
    /// <param name="item">HierarchyItem to retrive the previous item for.</param>
    /// <returns>The item displayed before the specified item, or null if a previous item does not exist.</returns>
    public HierarchyItem GetPreviousItem(HierarchyItem item)
    {
      if (this is ColumnsHierarchy)
        return this.HitTest(new Point(item.X - 1, item.Y + 1));
      return this.HitTest(new Point(item.X + 1, item.Y - 1));
    }

    internal int GetGridHeight()
    {
      return this.gridControl.GetHeight();
    }

    internal int GetGridWidth()
    {
      return this.gridControl.Width;
    }

    internal abstract void Resize();

    /// <summary>Expands all items in the Hierarchy</summary>
    public void ExpandAllItems()
    {
      foreach (HierarchyItem hierarchyItem in this.hierarchyItems)
        hierarchyItem.ExpandInternal(true, false, true);
      foreach (HierarchyItem hierarchySummaryItem in this.hierarchySummaryItems)
        hierarchySummaryItem.ExpandInternal(true, false, true);
      this.UpdateVisibleLeaves();
      this.PreRenderRequired = true;
      this.gridControl.Invalidate();
    }

    /// <summary>Collapses all items in the Hierarchy</summary>
    public void CollapseAllItems()
    {
      foreach (HierarchyItem hierarchyItem in this.hierarchyItems)
        hierarchyItem.ExpandInternal(false, false, true);
      foreach (HierarchyItem hierarchySummaryItem in this.hierarchySummaryItems)
        hierarchySummaryItem.ExpandInternal(false, false, true);
      this.UpdateVisibleLeaves();
      this.PreRenderRequired = true;
      this.gridControl.Invalidate();
    }

    internal int AddColumn(int nWidth)
    {
      if (nWidth < HierarchyItem.MinimumWidth)
        nWidth = HierarchyItem.MinimumWidth;
      this.columnWidths.Add(nWidth);
      return this.columnWidths.Count - 1;
    }

    internal void InsertColumn(int nIndex, int nWidth)
    {
      if (nWidth < HierarchyItem.MinimumWidth)
        nWidth = HierarchyItem.MinimumWidth;
      this.columnWidths.Insert(nIndex, nWidth);
      this.PreRenderRequired = true;
    }

    internal void DeleteColumn(int nIndex)
    {
      this.columnWidths.RemoveAt(nIndex);
      this.PreRenderRequired = true;
    }

    internal int GetColumnWidth(int nIndex)
    {
      if (nIndex < 0 || nIndex >= this.columnWidths.Count)
        return HierarchyItem.DefaultColumnWidth;
      int num = this.columnWidths[nIndex];
      if (num <= HierarchyItem.MinimumWidth)
      {
        num = HierarchyItem.MinimumWidth;
        this.columnWidths[nIndex] = num;
      }
      return num;
    }

    /// <summary>Sets the width of a column within the hierarchy</summary>
    /// <param name="columnIndex">The zero-based index of the column</param>
    /// <param name="width">The new width of the column</param>
    /// <returns>The method returns if the new width is applied</returns>
    public virtual bool SetColumnWidth(int columnIndex, int width)
    {
      if (columnIndex >= this.columnWidths.Count)
      {
        this.UpdateColumnsCount();
        if (columnIndex >= this.columnWidths.Count)
          return false;
      }
      if (width < HierarchyItem.MinimumWidth)
        width = HierarchyItem.MinimumWidth;
      if (this.IsColumnsHierarchy && this.gridControl.GroupingEnabled && columnIndex < this.gridControl.GroupingColumns.Count)
        width = this.gridControl.RowsHierarchy.CompactStyleRenderingItemsIndent + 2;
      this.columnWidths[columnIndex] = width;
      this.PreRenderRequired = true;
      return true;
    }

    /// <summary>Sets the height of the row.</summary>
    /// <param name="hierarchyItem">The hierarchy item.</param>
    /// <param name="hieght">The hieght.</param>
    /// <returns></returns>
    public bool SetRowHeight(HierarchyItem hierarchyItem, int hieght)
    {
      if (hieght < 15 || hieght > 500)
        return false;
      hierarchyItem.hierarchyItemHeight = hieght;
      this.PreRenderRequired = true;
      return true;
    }

    internal void DeleteAllColumns()
    {
      this.columnWidths.Clear();
      this.PreRenderRequired = true;
    }

    /// <summary>
    /// Automatically resizes the items in the Hierarchy for best visual appearance
    /// </summary>
    public abstract void AutoResize();

    /// <summary>
    /// Automatically resizes the items in the Hierarchy for best visual appearance
    /// </summary>
    public abstract void AutoResize(AutoResizeMode autoResizeMode);

    /// <summary>Removes all HierarchyItems and their settings.</summary>
    public void Clear()
    {
      this.Reset();
    }

    internal virtual void Reset()
    {
      this.hierarchyItems.Clear();
      this.hierarchySummaryItems.Clear();
      if (!this.SuspendWidthsReset)
        this.columnWidths.Clear();
      this.selectedItems.Clear();
      this.hashHiddenItems.Clear();
      this.itemProperties.Clear();
      this.ClearItemStyleOverrides();
    }

    internal void ClearItemStyleOverrides()
    {
      this.hashItemStyleNormal.Clear();
      this.hashItemStyleSelected.Clear();
      this.hashItemStyleDisabled.Clear();
    }

    /// <summary>Retrieves all child HierarchyItems at all levels</summary>
    /// <returns>List of HierarchyItems</returns>
    public List<HierarchyItem> GetChildItemsRecursive()
    {
      List<HierarchyItem> list = new List<HierarchyItem>();
      this.GetChildItems(ref list);
      return list;
    }

    private void GetChildItems(ref List<HierarchyItem> list)
    {
      foreach (HierarchyItem hierarchyItem in this.Items)
      {
        list.Add(hierarchyItem);
        hierarchyItem.GetChildItems(ref list);
      }
      foreach (HierarchyItem summaryItem in this.SummaryItems)
      {
        list.Add(summaryItem);
        summaryItem.GetChildItems(ref list);
      }
    }

    internal bool GetVisibleLeaves(ref List<HierarchyItem> list, HierarchyItem item, bool isTreeStyle)
    {
      if (item.Hidden || item.Filtered)
        return true;
      if (item.RenderedItems.Count == 0 && item.RenderedSummaryItems.Count == 0)
      {
        list.Add(item);
        return true;
      }
      if (isTreeStyle)
        list.Add(item);
      if (item.Expanded)
      {
        foreach (HierarchyItem renderedItem in item.RenderedItems)
        {
          if (!renderedItem.Hidden && !renderedItem.Filtered)
          {
            if (item.RenderedItems.Count > 0 && renderedItem.Expanded || renderedItem.HasVisibleSummaryItems)
              this.GetVisibleLeaves(ref list, renderedItem, isTreeStyle);
            else
              list.Add(renderedItem);
          }
        }
      }
      foreach (HierarchyItem renderedSummaryItem in item.RenderedSummaryItems)
      {
        if (!renderedSummaryItem.Hidden && !renderedSummaryItem.Filtered)
          list.Add(renderedSummaryItem);
      }
      return true;
    }

    internal void GetVisibleLeafLevelItems(ref List<HierarchyItem> pv)
    {
      if (this.visibleLeafItems.Count == 0 || this.gridControl.InDesignMode)
        this.UpdateVisibleLeaves();
      pv = this.visibleLeafItems;
    }

    internal void UpdateVisibleLeaves()
    {
      this.visibleLeafItems.Clear();
      if (this.RenderedItems.Count + this.RenderedSummaryItems.Count == 0)
        return;
      bool isTreeStyle = !this.IsColumnsHierarchy && ((RowsHierarchy) this).CompactStyleRenderingEnabled;
      foreach (HierarchyItem renderedItem in this.RenderedItems)
      {
        if (!renderedItem.Hidden && !renderedItem.Filtered)
        {
          if (renderedItem.Expanded || renderedItem.HasVisibleSummaryItems)
            this.GetVisibleLeaves(ref this.visibleLeafItems, renderedItem, isTreeStyle);
          else
            this.visibleLeafItems.Add(renderedItem);
        }
      }
      foreach (HierarchyItem renderedSummaryItem in this.RenderedSummaryItems)
      {
        if (!renderedSummaryItem.Hidden && !renderedSummaryItem.Filtered)
        {
          if (renderedSummaryItem.Expanded || renderedSummaryItem.HasVisibleSummaryItems)
            this.GetVisibleLeaves(ref this.visibleLeafItems, renderedSummaryItem, isTreeStyle);
          else
            this.visibleLeafItems.Add(renderedSummaryItem);
        }
      }
      this.visibleLeafItems.TrimExcess();
      this.UpdateColumnsIndexes();
    }

    internal void SetItemLevelAndColumn(HierarchyItem item, int depth)
    {
      item.column = depth;
      item.itemLevel = depth;
      foreach (HierarchyItem hierarchyItem in item.Items)
        this.SetItemLevelAndColumn(hierarchyItem, depth + 1);
      foreach (HierarchyItem summaryItem in item.SummaryItems)
        this.SetItemLevelAndColumn(summaryItem, depth + 1);
    }

    internal virtual void UpdateColumnsIndexes()
    {
      foreach (HierarchyItem hierarchyItem in this.Items)
        this.SetItemLevelAndColumn(hierarchyItem, 0);
      foreach (HierarchyItem summaryItem in this.SummaryItems)
        this.SetItemLevelAndColumn(summaryItem, 0);
    }

    internal void UpdateColumnsCount()
    {
      int totalItemsCount = this.TotalItemsCount;
      int columnsCount = this.ColumnsCount;
      int hierarchyDepth = this.HierarchyDepth;
      if (this.IsColumnsHierarchy)
      {
        for (int index = columnsCount; index < totalItemsCount; ++index)
          this.AddColumn(HierarchyItem.DefaultColumnWidth);
      }
      else
      {
        for (int index = columnsCount; index < this.HierarchyDepth; ++index)
          this.AddColumn(HierarchyItem.DefaultColumnWidth);
      }
      this.ColumnsCountRequresUpdate = false;
    }

    /// <summary>Sets the status of all hidden items to visible.</summary>
    public void ShowHiddenItems()
    {
      IDictionaryEnumerator enumerator = this.hashHiddenItems.GetEnumerator();
      while (enumerator.MoveNext())
        ((HierarchyItem) enumerator.Key).SetHidden(false, false);
      this.PreRenderRequired = true;
    }

    /// <summary>Checks if a specific item is a Summary Item</summary>
    /// <param name="item">The item to check</param>
    /// <returns>The method returns true if the item is a summary item</returns>
    public bool IsSummaryItem(HierarchyItem item)
    {
      foreach (HierarchyItem hierarchySummaryItem in this.hierarchySummaryItems)
      {
        if (item == hierarchySummaryItem)
          return true;
      }
      return false;
    }

    internal abstract void PreRender();

    private void ClearTotals(HierarchyItemsCollection collection)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].IsTotal)
        {
          collection.RemoveAt(index);
          --index;
        }
        else
          this.ClearTotals(collection[index].Items);
      }
    }

    internal void AddTotals(HierarchyItemsCollection collection, Dictionary<HierarchyItem, List<HierarchyItem>> hashTotals, PivotTotalsMode totalsMode)
    {
      this.ClearTotals(collection);
      if (collection.Count == 0)
        return;
      List<HierarchyItem> hierarchyItemList = new List<HierarchyItem>();
      foreach (HierarchyItem hierarchyItem in collection)
      {
        if (hierarchyItem.Items.Count > 0 && (totalsMode == PivotTotalsMode.DISPLAY_SUBTOTALS || (totalsMode | PivotTotalsMode.DISPLAY_BOTH) == PivotTotalsMode.DISPLAY_BOTH))
          this.AddTotals(hierarchyItem.Items, hashTotals, totalsMode);
        hierarchyItemList.Add(hierarchyItem);
      }
      if (collection.ownerItem == null && totalsMode == PivotTotalsMode.DISPLAY_SUBTOTALS)
        return;
      HierarchyItem key = collection.Add(this.GridControl.Localization.GetString(collection.ownerItem != null ? LocalizationNames.PivotTableSubTotalText : LocalizationNames.PivotTableGrandTotalText));
      hashTotals.Add(key, hierarchyItemList);
      key.IsTotal = true;
    }

    /// <summary>
    /// Begins selection update. Marks all selected items for unselection.
    /// </summary>
    internal void BeginSelectionUpdate()
    {
      Dictionary<HierarchyItem, Hierarchy.ItemSelection>.Enumerator enumerator = this.selectedItems.GetEnumerator();
      while (enumerator.MoveNext())
        this.selectedItems[enumerator.Current.Key].IsSelected = false;
    }

    /// <summary>
    /// Ends selection update. Removes all items which are marked as unselected.
    /// </summary>
    internal void EndSelectionUpdate()
    {
      List<HierarchyItem> hierarchyItemList = new List<HierarchyItem>();
      Dictionary<HierarchyItem, Hierarchy.ItemSelection>.Enumerator enumerator = this.selectedItems.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (!enumerator.Current.Value.IsSelected)
          hierarchyItemList.Add(enumerator.Current.Key);
      }
      foreach (HierarchyItem key in hierarchyItemList)
      {
        this.selectedItems.Remove(key);
        this.GridControl.OnHierarchyItemSelectionChanged(key);
      }
    }

    /// <summary>Selects an item in the Hierarchy</summary>
    /// <param name="item">Item to select</param>
    public void SelectItem(HierarchyItem item)
    {
      if (item == null || item.IsColumnsHierarchyItem != this.IsColumnsHierarchy || this.IsColumnsHierarchy && ((ColumnsHierarchy) this).IsGroupingColumn(item))
        return;
      if (!this.selectedItems.ContainsKey(item))
      {
        this.selectedItems.Add(item, new Hierarchy.ItemSelection(true));
        this.GridControl.OnHierarchyItemSelectionChanged(item);
      }
      else
        this.selectedItems[item].IsSelected = true;
      if (!this.IsColumnsHierarchy && ((RowsHierarchy) item.Hierarchy).CompactStyleRenderingEnabled)
        return;
      foreach (HierarchyItem hierarchyItem in item.Items)
        this.SelectItem(hierarchyItem);
      foreach (HierarchyItem summaryItem in item.SummaryItems)
        this.SelectItem(summaryItem);
    }

    /// <summary>UnSelects an item in the Hierarchy</summary>
    /// <param name="item">Item to UnSelect</param>
    public void UnSelectItem(HierarchyItem item)
    {
      if (item == null || item.IsColumnsHierarchyItem != this.IsColumnsHierarchy)
        return;
      if (this.selectedItems.ContainsKey(item))
      {
        this.selectedItems.Remove(item);
        this.GridControl.OnHierarchyItemSelectionChanged(item);
      }
      if (!this.IsColumnsHierarchy && ((RowsHierarchy) item.Hierarchy).CompactStyleRenderingEnabled)
        return;
      foreach (HierarchyItem hierarchyItem in item.Items)
        this.UnSelectItem(hierarchyItem);
      foreach (HierarchyItem summaryItem in item.SummaryItems)
        this.UnSelectItem(summaryItem);
    }

    /// <summary>Unselects all selected items.</summary>
    public void ClearSelection()
    {
      Dictionary<HierarchyItem, Hierarchy.ItemSelection>.Enumerator enumerator = this.selectedItems.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current.Value.IsSelected)
        {
          enumerator.Current.Value.IsSelected = false;
          this.GridControl.OnHierarchyItemSelectionChanged(enumerator.Current.Key);
        }
      }
      this.selectedItems.Clear();
    }

    private void ApplySelectionToParentItem(HierarchyItem item)
    {
      HierarchyItem parentItem = item.ParentItem;
      if (parentItem == null)
        return;
      Hierarchy hierarchyHost = item.HierarchyHost;
      bool flag = true;
      if (parentItem.Expanded)
      {
        foreach (HierarchyItem hierarchyItem in parentItem.Items)
        {
          if (!hierarchyHost.IsItemSelected(hierarchyItem) && !hierarchyItem.Hidden)
          {
            flag = false;
            break;
          }
        }
      }
      if (flag)
      {
        foreach (HierarchyItem summaryItem in parentItem.SummaryItems)
        {
          if (!hierarchyHost.IsItemSelected(summaryItem) && !summaryItem.Hidden)
          {
            flag = false;
            break;
          }
        }
      }
      if (!flag)
        return;
      if (!this.selectedItems.ContainsKey(parentItem))
      {
        hierarchyHost.selectedItems.Add(parentItem, new Hierarchy.ItemSelection(true));
        this.GridControl.OnHierarchyItemSelectionChanged(item);
      }
      else
        this.selectedItems[parentItem].IsSelected = true;
      this.ApplySelectionToParentItem(parentItem);
    }

    internal void ApplySelectionToParentItems()
    {
      foreach (HierarchyItem selectedItem in this.SelectedItems)
        this.ApplySelectionToParentItem(selectedItem);
    }

    /// <summary>Checks if an item is selected</summary>
    /// <param name="item">Item to check</param>
    /// <returns>The method returns true if the respective item is selected. If the item is not selected, or cannot be found within the hierarchy, the method returns false.</returns>
    public bool IsItemSelected(HierarchyItem item)
    {
      if (item == null || !this.selectedItems.ContainsKey(item))
        return false;
      return this.selectedItems[item].IsSelected;
    }

    private class HierarchyItemComparer : IComparer<HierarchyItem>
    {
      public int Compare(HierarchyItem item1, HierarchyItem item2)
      {
        vDataGridView gridControl = item1.Hierarchy.GridControl;
        HierarchyItem sortItem = item1.Hierarchy.SortItem;
        object obj1 = (object) null;
        object obj2 = (object) null;
        GridCell x = (GridCell) null;
        GridCell y = (GridCell) null;
        CellsArea cellsArea = item1.Hierarchy.GridControl.CellsArea;
        if (sortItem.IsColumnsHierarchyItem)
        {
          if (gridControl.GridCellSortComparer != null)
          {
            x = new GridCell(item1, sortItem, cellsArea);
            y = new GridCell(item2, sortItem, cellsArea);
          }
          else
          {
            obj1 = cellsArea.GetCellValue(item1, sortItem);
            obj2 = cellsArea.GetCellValue(item2, sortItem);
          }
        }
        else if (gridControl.GridCellSortComparer != null)
        {
          x = new GridCell(sortItem, item1, cellsArea);
          y = new GridCell(sortItem, item2, cellsArea);
        }
        else
        {
          obj1 = cellsArea.GetCellValue(sortItem, item1);
          obj2 = cellsArea.GetCellValue(sortItem, item2);
        }
        if (item1.Hierarchy.SortingDirection == SortingDirection.Descending)
        {
          if (gridControl.GridCellSortComparer != null)
          {
            GridCell gridCell = x;
            x = y;
            y = gridCell;
          }
          else
          {
            object obj3 = obj1;
            obj1 = obj2;
            obj2 = obj3;
          }
        }
        if (gridControl.GridCellSortComparer != null)
          return gridControl.GridCellSortComparer.Compare(x, y);
        if (obj1 == null)
          obj1 = (object) "";
        if (obj2 == null)
          obj2 = (object) "";
        if (obj1 is DateTime && obj2 is DateTime)
          return ((DateTime) obj1).CompareTo((DateTime) obj2);
        if (obj1 is double && obj2 is double)
          return ((double) obj1).CompareTo((double) obj2);
        if (obj1 is float && obj2 is float)
          return ((float) obj1).CompareTo((float) obj2);
        if (obj1 is int && obj2 is int)
          return ((int) obj1).CompareTo((int) obj2);
        if (obj1 is string && obj2 is string)
          return ((string) obj1).CompareTo((string) obj2);
        if (obj1 is bool && obj2 is bool)
          return ((bool) obj1).CompareTo((bool) obj2);
        double result1 = 0.0;
        double result2 = 0.0;
        if (double.TryParse(obj1.ToString(), out result1) && double.TryParse(obj2.ToString(), out result2))
          return result1.CompareTo(result2);
        return obj1.ToString().CompareTo(obj2.ToString());
      }
    }

    internal class ItemSelection
    {
      public bool IsSelected { get; set; }

      public ItemSelection(bool IsSelected)
      {
        this.IsSelected = IsSelected;
      }
    }
  }
}
