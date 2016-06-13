// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.GridToolTipEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a GridView ToolTip event argument</summary>
  public class GridToolTipEventArgs : EventArgs
  {
    private string toolTipText;
    private HierarchyItem rowItem;
    private HierarchyItem columnItem;
    private bool handled;

    /// <summary>
    /// HierarchyItem representing the row where the tooltip will be displayed.
    /// </summary>
    public HierarchyItem RowItem
    {
      get
      {
        return this.rowItem;
      }
    }

    /// <summary>
    /// HierarchyItem representing the column where the tooltip will be displayed.
    /// </summary>
    public HierarchyItem ColumnItem
    {
      get
      {
        return this.columnItem;
      }
    }

    /// <summary>Determines whether the event was handled</summary>
    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    /// <summary>Gets or sets the tooltip text</summary>
    public string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        this.toolTipText = value;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="rowItem">HierarchyItem representing the row where the tooltip will be displayed.</param>
    /// <param name="columnItem">HierarchyItem representing the column where the tooltip will be displayed.</param>
    public GridToolTipEventArgs(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      this.rowItem = rowItem;
      this.columnItem = columnItem;
    }
  }
}
