// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.BoundFieldEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents an event argument for HierarchyItem data binding event.
  /// </summary>
  /// <remarks>
  /// GridView triggers HierarchyItem data binding events when a column from the data source is being bound.
  /// These events are useful when the user wants to intercept the data binding process for perticular fields and configure
  /// the properties of the respective HierarchyItem. For example, you can use this event to associate the data bound HierarchyItem
  /// with a grid cell editor, specify data formatting rules, text alignment properties and more.
  /// </remarks>
  public class BoundFieldEventArgs : EventArgs
  {
    private BoundField boundField;
    private HierarchyItem hierarchyItem;
    private vDataGridView gridControl;

    /// <summary>Reference to the BoundField</summary>
    public BoundField BoundField
    {
      get
      {
        return this.boundField;
      }
    }

    /// <summary>
    /// Gets the index of corresponding row or column in the data source.
    /// </summary>
    public int BoundFieldIndex
    {
      get
      {
        return this.hierarchyItem.BoundFieldIndex;
      }
    }

    /// <summary>Reference to the HierarchyItem</summary>
    public HierarchyItem HierarchyItem
    {
      get
      {
        return this.hierarchyItem;
      }
    }

    /// <summary>Reference to the grid control</summary>
    public vDataGridView GridControl
    {
      get
      {
        return this.gridControl;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="hierarchyItem">The BoundField HierarchyItem</param>
    /// <param name="boundField">The BoundField</param>
    /// <param name="gridControl">The GridControl</param>
    public BoundFieldEventArgs(HierarchyItem hierarchyItem, BoundField boundField, vDataGridView gridControl)
    {
      this.hierarchyItem = hierarchyItem;
      this.boundField = boundField;
      this.gridControl = gridControl;
    }
  }
}
