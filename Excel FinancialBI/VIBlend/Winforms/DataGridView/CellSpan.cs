// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellSpan
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Defines a Cell span / cell merge inside the data grid's cells area (CellsArea)
  /// </summary>
  public struct CellSpan
  {
    /// <summary>Row HierarchyItem corresponding to the grid cell</summary>
    public HierarchyItem RowItem;
    /// <summary>Column HierarchyItem corresponding to the grid cell</summary>
    public HierarchyItem ColumnItem;
    /// <summary>Number of Rows to span / marge</summary>
    public int RowsCount;
    /// <summary>Number of Columns to span / merge</summary>
    public int ColumnsCount;

    public CellSpan(HierarchyItem rowItem, HierarchyItem columnItem, int rowsCount, int columnsCount)
    {
      this.RowItem = rowItem;
      this.ColumnItem = columnItem;
      this.RowsCount = rowsCount;
      this.ColumnsCount = columnsCount;
    }
  }
}
