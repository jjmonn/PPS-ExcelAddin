// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTabCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class vTabCollection : IList, ICollection, IList<vTabPage>, ICollection<vTabPage>, IEnumerable<vTabPage>, IEnumerable
  {
    private List<vTabPage> innerList = new List<vTabPage>();
    private vTabControl vTabControl;

    public vTabPage this[int index]
    {
      get
      {
        if (this.innerList.Count > 0)
          return this.innerList[index];
        return (vTabPage) null;
      }
      set
      {
        this.innerList.Add(value);
        this.vTabControl.Controls.Add((Control) value);
        this.OnCollectionChanged();
      }
    }

    public int Count
    {
      get
      {
        return this.innerList.Count;
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
        return (object) this;
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
        return (object) this.innerList[index];
      }
      set
      {
        this.vTabControl.Controls.Add((Control) (value as vTabPage));
        this.innerList.Add(value as vTabPage);
        this.OnCollectionChanged();
      }
    }

    /// <summary>Occurs when [collection changed].</summary>
    public event EventHandler CollectionChanged;

    public vTabCollection()
    {
    }

    public vTabCollection(vTabControl ownerControl)
      : this()
    {
      this.vTabControl = ownerControl;
    }

    protected virtual void OnCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, EventArgs.Empty);
    }

    public int IndexOf(vTabPage item)
    {
      return this.innerList.IndexOf(item);
    }

    public void Insert(int index, vTabPage item)
    {
      this.vTabControl.Controls.Add((Control) item);
      this.vTabControl.Controls.SetChildIndex((Control) item, index);
      this.innerList.Insert(index, item);
      this.OnCollectionChanged();
    }

    public void RemoveAt(int index)
    {
      vTabPage vTabPage = this.innerList[index];
      if (vTabPage == null)
        return;
      this.vTabControl.Controls.Remove((Control) vTabPage);
      this.innerList.RemoveAt(index);
      this.OnCollectionChanged();
    }

    public void Add(vTabPage item)
    {
      this.vTabControl.Controls.Add((Control) item);
      this.innerList.Add(item);
      this.OnCollectionChanged();
    }

    public void Clear()
    {
      for (int index = 0; index < this.vTabControl.TabPages.Count; ++index)
        this.vTabControl.Controls.Remove((Control) this.vTabControl.TabPages[index]);
      this.innerList.Clear();
      this.OnCollectionChanged();
    }

    public bool Contains(vTabPage item)
    {
      return this.vTabControl.Controls.Contains((Control) item);
    }

    public void CopyTo(vTabPage[] array, int arrayIndex)
    {
      this.vTabControl.Controls.CopyTo((Array) array, arrayIndex);
    }

    public bool Remove(vTabPage item)
    {
      this.vTabControl.Controls.Remove((Control) item);
      this.innerList.Remove(item);
      this.OnCollectionChanged();
      return true;
    }

    public IEnumerator<vTabPage> GetEnumerator()
    {
      return (IEnumerator<vTabPage>) this.innerList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.innerList.GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
      this.vTabControl.Controls.CopyTo(array, index);
    }

    public int Add(object value)
    {
      if (value is vTabPage)
      {
        this.vTabControl.Controls.Add((Control) (value as vTabPage));
        this.innerList.Add(value as vTabPage);
        this.OnCollectionChanged();
      }
      return this.vTabControl.TabPages.Count;
    }

    public bool Contains(object value)
    {
      return this.vTabControl.Controls.Contains((Control) (value as vTabPage));
    }

    public int IndexOf(object value)
    {
      return this.innerList.IndexOf(value as vTabPage);
    }

    public void Insert(int index, object value)
    {
      this.vTabControl.Controls.Add((Control) (value as vTabPage));
      this.vTabControl.Controls.SetChildIndex((Control) (value as vTabPage), index);
      this.innerList.Insert(index, value as vTabPage);
      this.OnCollectionChanged();
    }

    public void Remove(object value)
    {
      if (!(value is vTabPage))
        return;
      this.Remove(value as vTabPage);
    }
  }
}
