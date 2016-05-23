// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellValueNeededEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a data grid value need event arguments</summary>
  public class CellValueNeededEventArgs
  {
    /// <summary>Cell's Row</summary>
    public HierarchyItem RowItem;
    /// <summary>Cell's Column</summary>
    public HierarchyItem ColumnItem;
    /// <summary>The value of the cell</summary>
    public object CellValue;
    /// <summary>The image index of the cell's image</summary>
    public int CellImageIndex;

    /// <summary>Constructor</summary>
    public CellValueNeededEventArgs(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      this.RowItem = rowItem;
      this.ColumnItem = columnItem;
      this.CellValue = (object) null;
      this.CellImageIndex = -1;
    }
  }
}
