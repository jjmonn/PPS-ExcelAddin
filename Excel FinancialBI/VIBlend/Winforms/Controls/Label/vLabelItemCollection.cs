// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vLabelCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class vLabelCollection : IList<vLabelItem>, IList, ICollection, ICollection<vLabelItem>, IEnumerable<vLabelItem>, IEnumerable
  {
    private List<vLabelItem> items = new List<vLabelItem>();

    public vLabelItem this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        this.items[index] = value;
        this.OnCollectionChanged();
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
        return (object) this.items[index];
      }
      set
      {
        this.items[index] = (vLabelItem) value;
        this.OnCollectionChanged();
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

    public event EventHandler CollectionChanged;

    protected void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    public int IndexOf(vLabelItem item)
    {
      return this.items.IndexOf(item);
    }

    public void Insert(int index, vLabelItem item)
    {
      this.items.Insert(index, item);
      this.OnCollectionChanged();
    }

    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
      this.OnCollectionChanged();
    }

    public void Add(vLabelItem item)
    {
      this.items.Add(item);
      this.OnCollectionChanged();
    }

    public void Clear()
    {
      this.items.Clear();
      this.OnCollectionChanged();
    }

    public bool Contains(vLabelItem item)
    {
      return this.items.Contains(item);
    }

    public void CopyTo(vLabelItem[] array, int arrayIndex)
    {
    }

    public bool Remove(vLabelItem item)
    {
      bool flag = this.items.Remove(item);
      this.OnCollectionChanged();
      return flag;
    }

    public IEnumerator<vLabelItem> GetEnumerator()
    {
      return (IEnumerator<vLabelItem>) this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    public int Add(object value)
    {
      this.items.Add((vLabelItem) value);
      this.OnCollectionChanged();
      return this.items.Count;
    }

    public bool Contains(object value)
    {
      return this.Contains((vLabelItem) value);
    }

    public int IndexOf(object value)
    {
      return this.IndexOf((vLabelItem) value);
    }

    public void Insert(int index, object value)
    {
      this.Insert(index, (vLabelItem) value);
      this.OnCollectionChanged();
    }

    public void Remove(object value)
    {
      this.Remove((vLabelItem) value);
      this.OnCollectionChanged();
    }

    public void CopyTo(Array array, int index)
    {
    }
  }
}
