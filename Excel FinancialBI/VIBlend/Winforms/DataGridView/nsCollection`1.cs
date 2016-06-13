// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.nsCollection`1
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.DataGridView
{
  /// <exclude />
  [Serializable]
  public class nsCollection<T> : IEnumerable<T>, IList, ICollection, IEnumerable
  {
    private List<T> children;
    private object owner;
    public ColumnsHierarchy ColumnsHierarchy;
    public RowsHierarchy RowsHierarchy;

    public virtual int Count
    {
      get
      {
        if (this.children != null)
          return this.children.Count;
        return 0;
      }
    }

    public virtual T this[int i]
    {
      get
      {
        if (i < this.children.Count)
          return this.children[i];
        return default (T);
      }
      set
      {
        this.children[i] = value;
        this.OnCollectionChanged();
      }
    }

    public virtual object Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
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
        return (object) this.children[index];
      }
      set
      {
        this.children[index] = (T) value;
      }
    }

    public event EventHandler CollectionChanged;

    public nsCollection(object owner)
    {
      this.children = new List<T>();
      this.owner = owner;
    }

    public nsCollection()
    {
      this.children = new List<T>();
      this.owner = (object) null;
    }

    internal nsCollection(object owner, int capacity)
    {
      this.children = new List<T>(capacity);
      this.owner = owner;
    }

    public void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    public virtual void Add(T item)
    {
      this.children.Add(item);
      this.OnCollectionChanged();
    }

    public void AddRange(nsCollection<T> itemsCollection)
    {
      this.children.AddRange((IEnumerable<T>) itemsCollection);
      this.OnCollectionChanged();
    }

    public void Clear()
    {
      this.children.Clear();
      this.OnCollectionChanged();
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
      return (IEnumerator<T>) this.children.GetEnumerator();
    }

    public virtual int GetIndex(T item)
    {
      return this.children.IndexOf(item);
    }

    public virtual void Insert(T item, int i)
    {
      if (i >= this.children.Count)
      {
        this.Add(item);
      }
      else
      {
        this.children.Insert(i, item);
        this.OnCollectionChanged();
      }
    }

    public virtual void Remove(T child)
    {
      if (this.children == null || !this.children.Contains(child))
        return;
      this.children.Remove(child);
      this.OnCollectionChanged();
    }

    public void RemoveAt(int index)
    {
      if (this.children == null)
        return;
      this.children.RemoveAt(index);
      this.OnCollectionChanged();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.children.CopyTo(array as T[], index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    int IList.Add(object value)
    {
      this.Add((T) value);
      return this.children.IndexOf((T) value);
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      return this.children.Contains((T) value);
    }

    int IList.IndexOf(object value)
    {
      return this.children.IndexOf((T) value);
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
      this.Remove(this.children[index]);
    }
  }
}
