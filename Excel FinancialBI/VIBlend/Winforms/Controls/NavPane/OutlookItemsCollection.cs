// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.OutlookItemsCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class OutlookItemsCollection : IList<vOutlookItem>, ICollection<vOutlookItem>, IEnumerable<vOutlookItem>, IList, ICollection, IEnumerable
  {
    private List<vOutlookItem> items = new List<vOutlookItem>();

    public vOutlookItem this[int index]
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
        this[index] = (vOutlookItem) value;
      }
    }

    public event EventHandler CollectionChanged;

    public void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    public int IndexOf(vOutlookItem item)
    {
      return this.items.IndexOf(item);
    }

    public void Insert(int index, vOutlookItem item)
    {
      this.items.Insert(index, item);
      this.OnCollectionChanged();
    }

    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
      this.OnCollectionChanged();
    }

    public void Add(vOutlookItem item)
    {
      this.items.Add(item);
      this.OnCollectionChanged();
    }

    public void AddRange(vOutlookItem[] itemsArray)
    {
      for (int index = 0; index < itemsArray.Length; ++index)
        this.items.Add(itemsArray[index]);
      this.OnCollectionChanged();
    }

    public void Clear()
    {
      this.items.Clear();
      this.OnCollectionChanged();
    }

    public bool Contains(vOutlookItem item)
    {
      return this.items.Contains(item);
    }

    public void CopyTo(vOutlookItem[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    public bool Remove(vOutlookItem item)
    {
      bool flag = this.items.Remove(item);
      this.OnCollectionChanged();
      return flag;
    }

    public IEnumerator<vOutlookItem> GetEnumerator()
    {
      return (IEnumerator<vOutlookItem>) this.items.GetEnumerator();
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
      this.Add((vOutlookItem) value);
      return this.items.Count;
    }

    public bool Contains(object value)
    {
      return this.Contains((vOutlookItem) value);
    }

    public int IndexOf(object value)
    {
      return this.IndexOf((vOutlookItem) value);
    }

    public void Insert(int index, object value)
    {
      this.Insert(index, value);
    }

    public void Remove(object value)
    {
      this.Remove((vOutlookItem) value);
    }
  }
}
