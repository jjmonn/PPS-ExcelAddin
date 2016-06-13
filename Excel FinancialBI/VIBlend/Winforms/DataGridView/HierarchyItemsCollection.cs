// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.HierarchyItemsCollection
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a collection of HierarchyItems</summary>
  [Serializable]
  public class HierarchyItemsCollection : IEnumerable<HierarchyItem>, IList, ICollection, IEnumerable
  {
    private static List<HierarchyItem> empty = new List<HierarchyItem>();
    private Dictionary<int, HierarchyItem> itemsHash = new Dictionary<int, HierarchyItem>();
    private Random random = new Random(int.MaxValue);
    private byte collectionFlags;
    private CollectionStorage storage;
    public HierarchyItem ownerItem;
    public Hierarchy parentHierarchy;
    internal int FixedItemsCount;

    private bool IsInitialized
    {
      get
      {
        return this.storage != null;
      }
    }

    internal bool IsSorted
    {
      get
      {
        return ((int) this.collectionFlags & 1) == 1;
      }
      set
      {
        if (value)
          this.collectionFlags |= (byte) 1;
        else
          this.collectionFlags &= (byte) 254;
      }
    }

    internal bool IsFiltered
    {
      get
      {
        return ((int) this.collectionFlags & 2) == 2;
      }
      set
      {
        if (value)
          this.collectionFlags |= (byte) 2;
        else
          this.collectionFlags &= (byte) 253;
      }
    }

    private bool IsSortOrderInSync
    {
      get
      {
        return ((int) this.collectionFlags & 4) == 4;
      }
      set
      {
        if (value)
          this.collectionFlags |= (byte) 4;
        else
          this.collectionFlags &= (byte) 251;
      }
    }

    internal bool IsFilteringInSync
    {
      get
      {
        return ((int) this.collectionFlags & 8) == 8;
      }
      set
      {
        if (value)
          this.collectionFlags |= (byte) 8;
        else
          this.collectionFlags &= (byte) 247;
      }
    }

    internal List<HierarchyItem> ItemsRendered
    {
      get
      {
        if (!this.IsInitialized)
          return HierarchyItemsCollection.empty;
        return this.storage.itemsRendered;
      }
    }

    internal List<HierarchyItem> Items
    {
      get
      {
        if (!this.IsInitialized || !this.IsInitialized)
          return HierarchyItemsCollection.empty;
        return this.storage.items;
      }
    }

    /// <summary>
    /// Gets the number of elements actually contained in the collection
    /// </summary>
    public virtual int Count
    {
      get
      {
        if (this.storage == null)
          return 0;
        return this.storage.items.Count;
      }
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    public virtual HierarchyItem this[int index]
    {
      get
      {
        if (this.storage == null)
          return (HierarchyItem) null;
        if (index >= this.storage.items.Count)
          return (HierarchyItem) null;
        this.storage.items[index].parentItem = this.ownerItem;
        return this.storage.items[index];
      }
      set
      {
        if (this.storage == null)
          return;
        this.storage.items[index] = value;
      }
    }

    /// <exclude />
    public virtual Hierarchy OwnerHierarchy
    {
      get
      {
        return this.parentHierarchy;
      }
    }

    /// <exclude />
    public virtual HierarchyItem OwnerItem
    {
      get
      {
        return this.ownerItem;
      }
      internal set
      {
        this.ownerItem = value;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) null;
      }
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    object IList.this[int index]
    {
      get
      {
        if (this.storage == null)
          return (object) null;
        this.storage.items[index].parentItem = this.ownerItem;
        return (object) this.storage.items[index];
      }
      set
      {
        if (this.storage == null)
          return;
        this.storage.items[index] = value as HierarchyItem;
      }
    }

    /// <summary>Occurs when the collection has changed</summary>
    public event EventHandler CollectionChanged;

    /// <summary>Constructor</summary>
    /// <param name="parentItem">HierarchyItem which hosts the collection</param>
    /// <param name="parentHierarchy">Hierarchy which hosts the collection</param>
    public HierarchyItemsCollection(HierarchyItem parentItem, Hierarchy parentHierarchy)
    {
      this.ownerItem = parentItem;
      this.parentHierarchy = parentHierarchy;
    }

    /// <summary>Raises the CollectionChanged event</summary>
    protected void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    private void Init()
    {
      this.storage = new CollectionStorage();
    }

    internal void Reserve(int size)
    {
      if (this.storage == null)
        this.Init();
      this.storage.items.Capacity = size;
      this.storage.itemsRendered.Capacity = size;
    }

    internal void SetFixedState(HierarchyItem item, bool isFixed)
    {
      if (item == null || item.Hierarchy != this.parentHierarchy)
        return;
      this.RePopulateRenderedItemsBasedOnFixedState();
    }

    internal void RePopulateRenderedItemsBasedOnFixedState()
    {
      if (this.storage == null)
        return;
      this.storage.itemsRendered.Clear();
      this.FixedItemsCount = 0;
      for (int index = 0; index < this.storage.items.Count; ++index)
      {
        HierarchyItem hierarchyItem = this.storage.items[index];
        if (hierarchyItem.Fixed)
          this.storage.itemsRendered.Insert(this.FixedItemsCount++, hierarchyItem);
        else
          this.storage.itemsRendered.Add(hierarchyItem);
      }
      for (int nIndex = 0; nIndex < this.storage.itemsRendered.Count; ++nIndex)
        this.storage.itemsRendered[nIndex].SetItemIndex(nIndex);
      this.IsSortOrderInSync = false;
      this.parentHierarchy.PreRenderRequired = true;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
    }

    /// <summary>Adds a new item to the collection</summary>
    /// <param name="ItemText">Item text</param>
    /// <returns>The method returns a reference to the HierarchyItem object which is added</returns>
    public virtual HierarchyItem Add(string ItemText)
    {
      if (this.ownerItem != null && this.ownerItem.IsSummaryItem)
        throw new InvalidOperationException("SummaryItems cannot contain child items");
      if (ItemText == null)
        ItemText = "";
      return this.Add(new HierarchyItem(this.parentHierarchy.IsColumnsHierarchy, this.parentHierarchy, this.ownerItem, this.Count, ItemText));
    }

    /// <summary>Adds a new item to the collection</summary>
    /// <param name="item">Reference to the HierarchyItem object to add</param>
    /// <returns>The method returns a reference to the HierarchyItem object which is added</returns>
    public virtual HierarchyItem Add(HierarchyItem item)
    {
      if (this.ownerItem != null && this.ownerItem.Hierarchy != null && this.ownerItem.IsSummaryItem)
        throw new InvalidOperationException("SummaryItems cannot contain child items");
      if (this.storage == null)
        this.Init();
      int hashCode = item.GetHashCode();
      if (this.itemsHash.ContainsKey(hashCode))
      {
        int num = 0;
        while (num <= 10)
        {
          int key = this.random.Next();
          if (!this.itemsHash.ContainsKey(key))
          {
            item.hashCode = key;
            this.itemsHash.Add(key, item);
            break;
          }
        }
      }
      else
        this.itemsHash.Add(hashCode, item);
      item.Hierarchy = item.Items.parentHierarchy = item.SummaryItems.parentHierarchy = this.parentHierarchy;
      item.parentItem = this.ownerItem;
      item.Items.OwnerItem = item;
      item.SummaryItems.OwnerItem = item;
      if (item.parentItem != null && item.Fixed)
      {
        item.Fixed = false;
        item.Hierarchy.PreRenderRequired = true;
        this.parentHierarchy.ColumnsCountRequresUpdate = true;
      }
      item.itemLevel = this.ownerItem == null ? 0 : this.ownerItem.itemLevel + 1;
      if (this.parentHierarchy != null)
        item.Column = !this.parentHierarchy.IsColumnsHierarchy ? item.itemLevel : this.storage.items.Count;
      item.SetItemIndex(this.storage.items.Count);
      this.storage.items.Add(item);
      if (!item.Fixed)
      {
        this.storage.itemsRendered.Add(item);
      }
      else
      {
        this.storage.itemsRendered.Insert(this.FixedItemsCount++, item);
        for (int nIndex = 0; nIndex < this.storage.itemsRendered.Count; ++nIndex)
          this.storage.itemsRendered[nIndex].SetItemIndex(nIndex);
      }
      this.IsSortOrderInSync = false;
      this.IsFilteringInSync = false;
      if (this.parentHierarchy != null)
      {
        this.parentHierarchy.ColumnsCountRequresUpdate = true;
        this.parentHierarchy.PreRenderRequired = true;
      }
      this.OnCollectionChanged();
      return item;
    }

    /// <summary>Removes all items from the collection</summary>
    public void Clear()
    {
      if (this.storage != null)
      {
        foreach (HierarchyItem hierarchyItem in this.storage.items)
          hierarchyItem.Fixed = false;
        this.storage.items.Clear();
        this.storage.itemsRendered.Clear();
        this.IsSortOrderInSync = false;
        this.IsFilteringInSync = false;
        this.storage = (CollectionStorage) null;
      }
      this.FixedItemsCount = 0;
      if (this.parentHierarchy != null)
      {
        this.parentHierarchy.PreRenderRequired = true;
        this.parentHierarchy.ColumnsCountRequresUpdate = true;
      }
      this.OnCollectionChanged();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An IEnumerator for the entire collection.</returns>
    public virtual IEnumerator<HierarchyItem> GetEnumerator()
    {
      if (this.storage == null)
        return (IEnumerator<HierarchyItem>) HierarchyItemsCollection.empty.GetEnumerator();
      return (IEnumerator<HierarchyItem>) this.storage.items.GetEnumerator();
    }

    /// <summary>
    /// Searches for the specified Item and returns the zero-based index of the first occurrence within the entire collection
    /// </summary>
    /// <param name="item">The item to location in the collection</param>
    /// <returns>The zero-based index of the first occurrence of item within the entire collection, if found; otherwise, -1.</returns>
    public virtual int IndexOf(HierarchyItem item)
    {
      if (this.storage == null)
        return -1;
      return this.storage.items.IndexOf(item);
    }

    /// <summary>
    /// Inserts a HierarchyItem into the collection at the specified index
    /// </summary>
    /// <param name="item">HierarchyItem to insert</param>
    /// <param name="index">The zero-based index at which item should be inserted</param>
    public virtual void Insert(HierarchyItem item, int index)
    {
      if (this.storage == null)
        this.Init();
      if (this.ownerItem != null && this.ownerItem.IsSummaryItem)
        throw new InvalidOperationException("SummaryItems cannot contain child items");
      if (item == null)
        throw new Exception("Invalid argument. HierarchyItem cannot be null");
      if (index < 0 || index > this.Count)
        throw new Exception("Invalid argument. Index is out of range");
      item.Hierarchy = this.parentHierarchy;
      item.parentItem = this.ownerItem;
      if (item.parentItem != null && item.Fixed)
      {
        item.Fixed = false;
        item.Hierarchy.PreRenderRequired = true;
        item.Hierarchy.ColumnsCountRequresUpdate = true;
      }
      if (index >= this.storage.items.Count || index < 0)
      {
        this.Add(item);
      }
      else
      {
        item.parentItem = this.ownerItem;
        item.itemLevel = this.ownerItem == null ? 0 : this.ownerItem.itemLevel + 1;
        this.storage.items.Insert(index, item);
      }
      this.RePopulateRenderedItemsBasedOnFixedState();
      for (int nIndex = 0; nIndex < this.storage.items.Count; ++nIndex)
      {
        this.storage.items[nIndex].SetItemIndex(nIndex);
        this.storage.itemsRendered[nIndex].SetItemIndex(nIndex);
      }
      this.IsSortOrderInSync = false;
      this.IsFilteringInSync = false;
      if (this.parentHierarchy != null)
      {
        this.parentHierarchy.ColumnsCountRequresUpdate = true;
        this.parentHierarchy.PreRenderRequired = true;
      }
      this.OnCollectionChanged();
    }

    /// <summary>
    /// Removes the first occurrence of a specific HierarchyItem from the collection
    /// </summary>
    /// <param name="item">The HierarchyItem to remove from the collection</param>
    public virtual void Remove(HierarchyItem item)
    {
      if (this.storage == null)
        return;
      if (this.storage.items != null && this.storage.items.Contains(item))
      {
        this.storage.itemsRendered.Remove(item);
        this.IsSortOrderInSync = false;
        this.IsFilteringInSync = false;
        if (item.Fixed)
        {
          --this.FixedItemsCount;
          item.Fixed = false;
          item.Hierarchy.ColumnsCountRequresUpdate = true;
          item.Hierarchy.PreRenderRequired = true;
        }
        item.parentItem = this.ownerItem;
        this.storage.items.Remove(item);
        for (int nIndex = 0; nIndex < this.storage.itemsRendered.Count; ++nIndex)
          this.storage.itemsRendered[nIndex].SetItemIndex(nIndex);
        this.OnCollectionChanged();
      }
      if (this.parentHierarchy == null)
        return;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
      this.parentHierarchy.PreRenderRequired = true;
    }

    /// <summary>
    /// Removes the HierarchyItem at the specified index of the collection
    /// </summary>
    /// <param name="index">The zero-based index of the HierarchyItem to remove</param>
    public void RemoveAt(int index)
    {
      if (this.storage == null)
        return;
      if (index < 0 || index >= this.storage.items.Count)
        throw new IndexOutOfRangeException();
      HierarchyItem hierarchyItem = this.storage.items[index];
      this.storage.itemsRendered.Remove(hierarchyItem);
      if (hierarchyItem.Fixed)
      {
        --this.FixedItemsCount;
        hierarchyItem.Fixed = false;
        hierarchyItem.Hierarchy.PreRenderRequired = true;
        hierarchyItem.Hierarchy.ColumnsCountRequresUpdate = true;
      }
      this.storage.items.RemoveAt(index);
      for (int nIndex = 0; nIndex < this.storage.itemsRendered.Count; ++nIndex)
        this.storage.itemsRendered[nIndex].SetItemIndex(nIndex);
      this.IsSortOrderInSync = false;
      this.IsFilteringInSync = false;
      this.OnCollectionChanged();
      if (this.parentHierarchy == null)
        return;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
      this.parentHierarchy.PreRenderRequired = true;
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (this.storage == null)
        return;
      this.storage.items.CopyTo(array as HierarchyItem[], index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    int IList.Add(object value)
    {
      if (this.storage == null)
        this.Init();
      if (this.ownerItem != null && this.ownerItem.IsSummaryItem)
        throw new InvalidOperationException("SummaryItems cannot contain child items");
      if (!(value is HierarchyItem))
        return -1;
      int num = this.storage.items.IndexOf(this.Add(value as HierarchyItem));
      if (this.parentHierarchy != null)
      {
        this.parentHierarchy.PreRenderRequired = true;
        this.parentHierarchy.ColumnsCountRequresUpdate = true;
      }
      return num;
    }

    void IList.Clear()
    {
      this.Clear();
      if (this.ownerItem == null && this.parentHierarchy != null)
        this.parentHierarchy.DeleteAllColumns();
      if (this.parentHierarchy == null)
        return;
      this.parentHierarchy.PreRenderRequired = true;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
    }

    bool IList.Contains(object value)
    {
      if (this.storage == null)
        return false;
      return this.storage.items.Contains(value as HierarchyItem);
    }

    int IList.IndexOf(object value)
    {
      if (this.storage == null)
        return -1;
      return this.storage.items.IndexOf(value as HierarchyItem);
    }

    void IList.Insert(int index, object value)
    {
      if (this.storage == null)
        this.Init();
      if (this.ownerItem != null && this.ownerItem.IsSummaryItem)
        throw new InvalidOperationException("SummaryItems cannot contain child items");
      this.Insert(value as HierarchyItem, index);
      if (this.parentHierarchy == null)
        return;
      this.parentHierarchy.PreRenderRequired = true;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
    }

    void IList.Remove(object value)
    {
      if (this.storage == null)
        return;
      this.Remove(value as HierarchyItem);
      if (this.parentHierarchy == null)
        return;
      this.parentHierarchy.PreRenderRequired = true;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
    }

    void IList.RemoveAt(int index)
    {
      if (this.storage == null)
        return;
      this.Remove(this.storage.items[index]);
      if (this.parentHierarchy == null)
        return;
      this.parentHierarchy.PreRenderRequired = true;
      this.parentHierarchy.ColumnsCountRequresUpdate = true;
    }
  }
}
