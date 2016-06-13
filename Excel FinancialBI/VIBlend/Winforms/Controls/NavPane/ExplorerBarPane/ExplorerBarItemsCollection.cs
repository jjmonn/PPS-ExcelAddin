// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ExplorerBarItemsCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ExplorerBarItemsCollection : IList<vExplorerBarItem>, ICollection<vExplorerBarItem>, IEnumerable<vExplorerBarItem>, IList, ICollection, IEnumerable
  {
    private List<vExplorerBarItem> items = new List<vExplorerBarItem>();

    public vExplorerBarItem this[int index]
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

    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return true;
      }
    }

    public object SyncRoot
    {
      get
      {
        return (object) null;
      }
    }

    public bool IsFixedSize
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
        return (object) this[index];
      }
      set
      {
        this[index] = (vExplorerBarItem) value;
      }
    }

    public event EventHandler CollectionChanged;

    public void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    public int IndexOf(vExplorerBarItem item)
    {
      return this.items.IndexOf(item);
    }

    public void Insert(int index, vExplorerBarItem item)
    {
      this.items.Insert(index, item);
      this.OnCollectionChanged();
    }

    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
      this.OnCollectionChanged();
    }

    public void Add(vExplorerBarItem item)
    {
      this.items.Add(item);
      this.OnCollectionChanged();
    }

    public void Clear()
    {
      this.items.Clear();
      this.OnCollectionChanged();
    }

    public bool Contains(vExplorerBarItem item)
    {
      return this.items.Contains(item);
    }

    public void CopyTo(vExplorerBarItem[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    public bool Remove(vExplorerBarItem item)
    {
      bool flag = this.items.Remove(item);
      this.OnCollectionChanged();
      return flag;
    }

    public IEnumerator<vExplorerBarItem> GetEnumerator()
    {
      return (IEnumerator<vExplorerBarItem>) this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
      this.CopyTo(array, index);
    }

    public int Add(object value)
    {
      this.Add((vExplorerBarItem) value);
      return this.items.Count;
    }

    public bool Contains(object value)
    {
      return this.Contains((vExplorerBarItem) value);
    }

    public int IndexOf(object value)
    {
      return this.IndexOf((vExplorerBarItem) value);
    }

    public void Insert(int index, object value)
    {
      this.Insert(index, value);
    }

    public void Remove(object value)
    {
      this.Remove((vExplorerBarItem) value);
    }
  }
}
