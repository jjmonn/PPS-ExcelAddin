// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.BoundItemsCollection`1
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a list of BoundField or BoundValueField items which represent data binding assiciations between columns in the data source and HierarchyItems in the data grid
  /// </summary>
  [Serializable]
  public class BoundItemsCollection<T> : IEnumerable<T>, IList, ICollection, IEnumerable
  {
    /// <exclude />
    protected List<T> items;

    /// <summary>Returns the number of items in the collection</summary>
    public virtual int Count
    {
      get
      {
        if (this.items != null)
          return this.items.Count;
        return 0;
      }
    }

    /// <summary>
    /// Returns a reference to a item located at a specific index
    /// </summary>
    public virtual T this[int i]
    {
      get
      {
        if (i < this.items.Count)
          return this.items[i];
        return default (T);
      }
      set
      {
        this.items[i] = value;
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
        this.items[index] = (T) value;
      }
    }

    /// <summary>Occurs when the collection has changed</summary>
    public event EventHandler<CollectionChangedEventArgs> CollectionChanged;

    public BoundItemsCollection()
    {
      this.items = new List<T>();
    }

    /// <exclude />
    protected void OnCollectionChanged(CollectionChangedReason reason)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, new CollectionChangedEventArgs(reason));
    }

    /// <summary>Adds an item to the collection</summary>
    public virtual T Add(T item)
    {
      if ((object) item == null || (object) item == null)
        throw new Exception("Invalid argument. item must be a valid type and cannot be null");
      this.items.Add(item);
      this.OnCollectionChanged(CollectionChangedReason.Add);
      return item;
    }

    /// <summary>Removes all items form the collection</summary>
    public void Clear()
    {
      this.items.Clear();
      this.OnCollectionChanged(CollectionChangedReason.Reset);
    }

    /// <summary>
    /// Returns an enumerator to support iterating through the collection
    /// </summary>
    public virtual IEnumerator<T> GetEnumerator()
    {
      return (IEnumerator<T>) this.items.GetEnumerator();
    }

    /// <summary>Returns the index of an item</summary>
    public virtual int IndexOf(T item)
    {
      return this.items.IndexOf(item);
    }

    /// <summary>Inserts an item at a specific position</summary>
    public virtual void Insert(T item, int i)
    {
      if ((object) item == null)
        throw new Exception("Invalid argument. item cannot be null");
      if (i < 0 || i > this.Count)
        throw new Exception("Invalid argument. Index is out of range");
      if (i >= this.items.Count)
        this.Add(item);
      else
        this.items.Insert(i, item);
      this.OnCollectionChanged(CollectionChangedReason.Add);
    }

    public virtual bool Contains(T item)
    {
      if ((object) item == null)
        return false;
      return this.items.Contains(item);
    }

    /// <summary>Removes an item from the collection</summary>
    public virtual void Remove(T item)
    {
      if (this.items == null || !this.items.Contains(item))
        return;
      this.items.Remove(item);
      this.OnCollectionChanged(CollectionChangedReason.Remove);
    }

    /// <summary>Removes an item located at a specific index</summary>
    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
      this.OnCollectionChanged(CollectionChangedReason.Remove);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.items.CopyTo(array as T[], index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    int IList.Add(object value)
    {
      return this.items.IndexOf(this.Add((T) value));
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      return this.items.Contains((T) value);
    }

    int IList.IndexOf(object value)
    {
      return this.items.IndexOf((T) value);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert((T) value, index);
    }

    void IList.Remove(object value)
    {
      this.Remove((T) value);
    }

    void IList.RemoveAt(int index)
    {
      this.Remove(this.items[index]);
    }
  }
}
