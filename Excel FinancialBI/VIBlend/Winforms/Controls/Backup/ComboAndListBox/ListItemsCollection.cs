// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemsCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a collection of ListItemsCollection</summary>
  [Serializable]
  public class ListItemsCollection : IEnumerable<ListItem>, IList, ICollection, IEnumerable
  {
    private System.Collections.Generic.List<ListItem> unsortedList = new System.Collections.Generic.List<ListItem>();
    private System.Collections.Generic.List<ListItem> items;

    /// <summary>Gets the inner list.</summary>
    /// <value>The inner list.</value>
    public System.Collections.Generic.List<ListItem> InnerList
    {
      get
      {
        return this.items;
      }
    }

    /// <summary>Gets the un sorted list.</summary>
    /// <value>The un sorted list.</value>
    public System.Collections.Generic.List<ListItem> List
    {
      get
      {
        return this.unsortedList;
      }
    }

    /// <summary>
    /// Gets the number of elements actually contained in the collection
    /// </summary>
    public virtual int Count
    {
      get
      {
        if (this.items != null)
          return this.items.Count;
        return 0;
      }
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    public virtual ListItem this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        this.items[index] = value;
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
        return (object) this.items[index];
      }
      set
      {
        this.items[index] = value as ListItem;
      }
    }

    /// <summary>Occurs when the collection has changed</summary>
    public event EventHandler CollectionChanged;

    /// <summary>Constructor</summary>
    public ListItemsCollection()
    {
      this.items = new System.Collections.Generic.List<ListItem>();
    }

    /// <summary>Raises the CollectionChanged event.</summary>
    protected void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    /// <summary>Adds a new item to the collection</summary>
    /// <param name="ItemText">Item text</param>
    /// <returns>The method returns a reference to the ListItem object which is added</returns>
    public virtual ListItem Add(string ItemText)
    {
      if (ItemText == null)
        ItemText = "";
      return this.Add(new ListItem() { Text = ItemText, Description = "" });
    }

    /// <summary>Adds a new item to the collection</summary>
    /// <param name="item">Reference to the ListItem object to add</param>
    /// <returns>The method returns a reference to the ListItem object which is added</returns>
    public virtual ListItem Add(ListItem item)
    {
      this.List.Add(item);
      this.items.Add(item);
      this.OnCollectionChanged();
      return item;
    }

    /// <summary>Adds the range.</summary>
    /// <param name="collection">The collection.</param>
    public virtual void AddRange(IEnumerable<ListItem> collection)
    {
      this.InnerList.AddRange(collection);
      this.List.AddRange(collection);
    }

    /// <summary>
    /// Determines whether the specified item is in the collection.
    /// </summary>
    public bool Contains(ListItem item)
    {
      if (this.items.Count > 0)
        return this.items.Contains(item);
      return false;
    }

    /// <summary>Removes all items from the collection</summary>
    public void Clear()
    {
      this.items.Clear();
      this.List.Clear();
      this.OnCollectionChanged();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An IEnumerator for the entire collection.</returns>
    public virtual IEnumerator<ListItem> GetEnumerator()
    {
      return (IEnumerator<ListItem>) this.items.GetEnumerator();
    }

    /// <summary>
    /// Searches for the specified Item and returns the zero-based index of the first occurrence within the entire collection
    /// </summary>
    /// <param name="item">The item to location in the collection</param>
    /// <returns>The zero-based index of the first occurrence of item within the entire collection, if found; otherwise, -1.</returns>
    public virtual int IndexOf(ListItem item)
    {
      return this.items.IndexOf(item);
    }

    /// <summary>
    /// Inserts a ListItem into the collection at the specified index
    /// </summary>
    /// <param name="item">ListItem to insert</param>
    /// <param name="index">The zero-based index at which item should be inserted</param>
    public virtual void Insert(string item, int index)
    {
      this.Insert(new ListItem()
      {
        Text = item,
        Description = ""
      }, index);
    }

    /// <summary>
    /// Inserts a ListItem into the collection at the specified index
    /// </summary>
    /// <param name="item">ListItem to insert</param>
    /// <param name="index">The zero-based index at which item should be inserted</param>
    public virtual void Insert(ListItem item, int index)
    {
      if (item == null)
        throw new Exception("Invalid argument. ListItem cannot be null");
      if (index < 0 || index > this.Count)
        throw new Exception("Invalid argument. Index is out of range");
      if (index >= this.items.Count)
      {
        this.Add(item);
      }
      else
      {
        this.items.Insert(index, item);
        this.List.Insert(index, item);
      }
      this.OnCollectionChanged();
    }

    /// <summary>
    /// Removes the first occurrence of a specific ListItem from the collection
    /// </summary>
    /// <param name="item">The ListItem to remove from the collection</param>
    public virtual void Remove(ListItem item)
    {
      if (item == null || !this.items.Contains(item))
        return;
      this.items.Remove(item);
      this.List.Remove(item);
      this.OnCollectionChanged();
    }

    /// <summary>
    /// Removes the ListItem at the specified index of the collection
    /// </summary>
    /// <param name="index">The zero-based index of the ListItem to remove</param>
    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
      this.List.RemoveAt(index);
      this.OnCollectionChanged();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.items.CopyTo(array as ListItem[], index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    int IList.Add(object value)
    {
      if (!(value is ListItem))
        return -1;
      return this.items.IndexOf(this.Add(value as ListItem));
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      return this.items.Contains(value as ListItem);
    }

    int IList.IndexOf(object value)
    {
      return this.items.IndexOf(value as ListItem);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(value as ListItem, index);
    }

    void IList.Remove(object value)
    {
      this.Remove(value as ListItem);
    }

    void IList.RemoveAt(int index)
    {
      this.Remove(this.items[index]);
    }
  }
}
