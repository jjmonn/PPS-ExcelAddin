// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeNodeCollection
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a collection of tree nodes.</summary>
  public class vTreeNodeCollection : IList, ICollection, IEnumerable<vTreeNode>, IEnumerable
  {
    private List<vTreeNode> innerList;
    private vTreeView ownerTree;
    private vTreeNode ownerNode;

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of elements contained in the <see cref="T:System.Collections.ICollection" />.
    /// </returns>
    public int Count
    {
      get
      {
        return this.innerList.Count;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:VIBlend.WinForms.Controls.vTreeNode" /> with the specified i.
    /// </summary>
    /// <value></value>
    public vTreeNode this[int i]
    {
      get
      {
        return this.innerList[i];
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

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.
    /// </returns>
    public bool IsReadOnly
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
        this.innerList[index] = value as vTreeNode;
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
        return true;
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
        return (object) this;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vTreeNodeCollection" /> class.
    /// </summary>
    protected vTreeNodeCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vTreeNodeCollection" /> class.
    /// </summary>
    /// <param name="owner">The owner.</param>
    /// <param name="parentNode">The parent node.</param>
    public vTreeNodeCollection(vTreeView owner, vTreeNode parentNode)
    {
      this.ownerNode = parentNode;
      this.ownerTree = owner;
      this.innerList = new List<vTreeNode>();
    }

    /// <summary>Gets the owner node.</summary>
    /// <returns></returns>
    public vTreeNode GetOwnerNode()
    {
      return this.ownerNode;
    }

    /// <summary>Gets the owner tree.</summary>
    /// <returns></returns>
    public vTreeView GetOwnerTree()
    {
      return this.ownerTree;
    }

    /// <summary>Adds the specified node.</summary>
    /// <param name="node">The node.</param>
    public void Add(vTreeNode node)
    {
      this.innerList.Add(node);
      this.UpdateOwnerTreeView(node);
      node.Index = this.innerList.Count - 1;
      node.Parent = this.ownerNode;
      vTreeNode vTreeNode = this.ownerNode;
      if (this.ownerTree == null || this.ownerTree.IsUpdating)
        return;
      this.ownerTree.InvalidateLayout();
    }

    /// <summary>Adds the specified node.</summary>
    /// <param name="node">The node.</param>
    /// <param name="expand">if set to <c>true</c> [expand].</param>
    public void Add(vTreeNode node, bool expand)
    {
      this.innerList.Add(node);
      this.UpdateOwnerTreeView(node);
      node.Index = this.innerList.IndexOf(node);
      node.Parent = this.ownerNode;
      if (this.ownerNode != null && expand)
        this.ownerNode.Expand();
      if (this.ownerTree == null)
        return;
      this.ownerTree.InvalidateLayout();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<vTreeNode> GetEnumerator()
    {
      return (IEnumerator<vTreeNode>) this.innerList.GetEnumerator();
    }

    /// <summary>Gets the index.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    public int GetIndex(vTreeNode node)
    {
      return node.Index;
    }

    /// <summary>Inserts the specified i.</summary>
    /// <param name="i">The i.</param>
    /// <param name="node">The node.</param>
    public void Insert(int i, vTreeNode node)
    {
      this.innerList.Insert(i, node);
      node.TreeView = this.ownerTree;
      node.Index = this.innerList.IndexOf(node);
      node.Parent = this.ownerNode;
      if (this.ownerTree != null)
        this.ownerTree.InvalidateLayout();
      for (int index = 0; index < this.innerList.Count; ++index)
        this.innerList[index].Index = index;
    }

    /// <summary>Removes the specified child.</summary>
    /// <param name="child">The child.</param>
    public void Remove(vTreeNode child)
    {
      this.innerList.Remove(child);
      child.Index = -1;
      child.Parent = (vTreeNode) null;
      if (this.ownerTree != null)
      {
        this.ownerTree.InvalidateLayout();
        this.ownerTree.selection.Clear();
      }
      for (int index = 0; index < this.innerList.Count; ++index)
        this.innerList[index].Index = index;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private void UpdateOwnerTreeView(vTreeNode node)
    {
      node.TreeView = this.ownerTree;
      if (node == null)
        return;
      foreach (vTreeNode node1 in node.Nodes)
        this.UpdateOwnerTreeView(node1);
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
      vTreeNode node = (vTreeNode) null;
      if (!(value is vTreeNode))
      {
        if (value is string)
          node = new vTreeNode((string) value);
      }
      else
        node = (vTreeNode) value;
      if (node == null)
        throw new Exception(string.Format("{0} is not a valid tree node type", (object) value.GetType()));
      int count = this.innerList.Count;
      this.innerList.Add(node);
      this.UpdateOwnerTreeView(node);
      node.Index = count;
      node.Parent = this.ownerNode;
      if (this.ownerNode != null)
        this.ownerNode.Expand();
      if (this.ownerTree != null)
        this.ownerTree.InvalidateLayout();
      return this.innerList.Count;
    }

    /// <summary>Adds the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <param name="expand">if set to <c>true</c> [expand].</param>
    /// <returns></returns>
    public int Add(object value, bool expand)
    {
      vTreeNode node = (vTreeNode) null;
      if (!(value is vTreeNode))
      {
        if (value is string)
          node = new vTreeNode((string) value);
      }
      else
        node = (vTreeNode) value;
      if (node == null)
        throw new Exception(string.Format("{0} is not a valid tree node type", (object) value.GetType()));
      this.innerList.Add(node);
      this.UpdateOwnerTreeView(node);
      node.Index = this.innerList.IndexOf(value as vTreeNode);
      node.Parent = this.ownerNode;
      if (this.ownerNode != null && expand)
        this.ownerNode.Expand();
      if (this.ownerTree != null)
        this.ownerTree.InvalidateLayout();
      return this.innerList.Count;
    }

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// </exception>
    public void Clear()
    {
      this.innerList.Clear();
      if (this.ownerTree == null)
        return;
      if (this.ownerTree.SelectedNodes.Length > 0)
        this.ownerTree.selection.Clear();
      this.ownerTree.InvalidateLayout();
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
      return this.innerList.Contains(value as vTreeNode);
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
      return this.innerList.IndexOf(value as vTreeNode);
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
      this.innerList.Insert(index, value as vTreeNode);
      (value as vTreeNode).TreeView = this.ownerTree;
      (value as vTreeNode).Index = this.innerList.IndexOf(value as vTreeNode);
      (value as vTreeNode).Parent = this.ownerNode;
      if (this.ownerTree == null)
        return;
      this.ownerTree.InvalidateLayout();
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
      this.innerList.Remove(value as vTreeNode);
      (value as vTreeNode).Index = this.innerList.IndexOf(value as vTreeNode);
      (value as vTreeNode).Parent = (vTreeNode) null;
      if (this.ownerTree != null)
      {
        this.ownerTree.InvalidateLayout();
        this.ownerTree.selection.Clear();
      }
      for (int index = 0; index < this.innerList.Count; ++index)
        this.innerList[index].Index = index;
    }

    /// <summary>
    /// Removes the <see cref="T:System.Collections.IList" /> item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// -or-
    /// The <see cref="T:System.Collections.IList" /> has a fixed size.
    /// </exception>
    public void RemoveAt(int index)
    {
      this.innerList.RemoveAt(index);
      if (this.ownerTree != null)
      {
        this.ownerTree.InvalidateLayout();
        this.ownerTree.selection.Clear();
      }
      for (int index1 = 0; index1 < this.innerList.Count; ++index1)
        this.innerList[index1].Index = index1;
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
      this.innerList.CopyTo((vTreeNode[]) array, index);
    }
  }
}
