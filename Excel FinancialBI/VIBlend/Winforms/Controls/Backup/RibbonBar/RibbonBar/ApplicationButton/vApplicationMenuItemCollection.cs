// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vApplicationMenuItemCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class vApplicationMenuItemCollection : IList<vApplicationMenuItem>, ICollection<vApplicationMenuItem>, IEnumerable<vApplicationMenuItem>, IList, ICollection, IEnumerable
  {
    private List<vApplicationMenuItem> items = new List<vApplicationMenuItem>();
    private vApplicationMenuItem owner;
    private Control ownerDropDown;

    /// <summary>Gets the owner.</summary>
    /// <value>The owner.</value>
    public Control Owner
    {
      get
      {
        return (Control) this.owner;
      }
    }

    /// <summary>Gets the owner drop down.</summary>
    /// <value>The owner drop down.</value>
    public Control OwnerDropDown
    {
      get
      {
        return this.ownerDropDown;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:VIBlend.WinForms.Controls.vApplicationMenuItem" /> at the specified index.
    /// </summary>
    /// <value></value>
    public vApplicationMenuItem this[int index]
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

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.
    /// </returns>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.
    /// </returns>
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
        this.items[index] = value as vApplicationMenuItem;
      }
    }

    /// <summary>
    /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
    /// </summary>
    /// <value></value>
    /// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.
    /// </returns>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
    /// </returns>
    public object SyncRoot
    {
      get
      {
        return (object) null;
      }
    }

    public event EventHandler MenuItemsChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vApplicationMenuItemCollection" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="ownerDropDown">The owner drop down.</param>
    public vApplicationMenuItemCollection(vApplicationMenuItem owner, Control ownerDropDown)
    {
      this.owner = owner;
      this.ownerDropDown = ownerDropDown;
    }

    protected virtual void OnMenuItemsChanged()
    {
      if (this.MenuItemsChanged == null)
        return;
      this.MenuItemsChanged((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
    /// <returns>
    /// The index of <paramref name="item" /> if found in the list; otherwise, -1.
    /// </returns>
    public int IndexOf(vApplicationMenuItem item)
    {
      return this.items.IndexOf(item);
    }

    /// <summary>
    /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
    /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
    /// </exception>
    public void Insert(int index, vApplicationMenuItem item)
    {
      item.owner = this.owner;
      this.items.Insert(index, item);
      this.OnMenuItemsChanged();
    }

    /// <summary>
    /// Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
    /// </exception>
    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
      this.OnMenuItemsChanged();
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </exception>
    public void Add(vApplicationMenuItem item)
    {
      item.owner = this.owner;
      this.items.Add(item);
      this.OnMenuItemsChanged();
    }

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </exception>
    public void Clear()
    {
      this.items.Clear();
      this.OnMenuItemsChanged();
    }

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
    /// </returns>
    public bool Contains(vApplicationMenuItem item)
    {
      return this.items.Contains(item);
    }

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="array" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="arrayIndex" /> is less than 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// 	<paramref name="array" /> is multidimensional.
    /// -or-
    /// <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.
    /// -or-
    /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
    /// -or-
    /// Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
    /// </exception>
    public void CopyTo(vApplicationMenuItem[] array, int arrayIndex)
    {
      this.items.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </exception>
    public bool Remove(vApplicationMenuItem item)
    {
      item.owner = (vApplicationMenuItem) null;
      this.OnMenuItemsChanged();
      return this.items.Remove(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<vApplicationMenuItem> GetEnumerator()
    {
      return (IEnumerator<vApplicationMenuItem>) this.items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    public void Dispose()
    {
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
    /// <returns>The position into which the new element was inserted.</returns>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// -or-
    /// The <see cref="T:System.Collections.IList" /> has a fixed size.
    /// </exception>
    public int Add(object value)
    {
      this.items.Add(value as vApplicationMenuItem);
      this.OnMenuItemsChanged();
      return this.items.Count;
    }

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.
    /// </summary>
    /// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
    /// <returns>
    /// true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.
    /// </returns>
    public bool Contains(object value)
    {
      return this.items.Contains(value as vApplicationMenuItem);
    }

    /// <summary>
    /// Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
    /// <returns>
    /// The index of <paramref name="value" /> if found in the list; otherwise, -1.
    /// </returns>
    public int IndexOf(object value)
    {
      return this.items.IndexOf(value as vApplicationMenuItem);
    }

    /// <summary>
    /// Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
    /// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// -or-
    /// The <see cref="T:System.Collections.IList" /> has a fixed size.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    /// 	<paramref name="value" /> is null reference in the <see cref="T:System.Collections.IList" />.
    /// </exception>
    public void Insert(int index, object value)
    {
      this.items.Insert(index, value as vApplicationMenuItem);
      this.OnMenuItemsChanged();
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// -or-
    /// The <see cref="T:System.Collections.IList" /> has a fixed size.
    /// </exception>
    public void Remove(object value)
    {
      this.items.Remove(value as vApplicationMenuItem);
      this.OnMenuItemsChanged();
    }

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
    /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="array" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is less than zero.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// 	<paramref name="array" /> is multidimensional.
    /// -or-
    /// <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.
    /// -or-
    /// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
    /// </exception>
    public void CopyTo(Array array, int index)
    {
      this.items.CopyTo((vApplicationMenuItem[]) array, index);
    }
  }
}
