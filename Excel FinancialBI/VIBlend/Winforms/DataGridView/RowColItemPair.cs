// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.RowColItemPair
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  internal class RowColItemPair
  {
    /// <summary>Represents a Row Item field.</summary>
    public HierarchyItem RowItem;
    /// <summary>Represents a Column Item field.</summary>
    public HierarchyItem ColItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.RowColItemPair" /> class.
    /// </summary>
    /// <param name="rowItem">The row item.</param>
    /// <param name="colItem">The col item.</param>
    public RowColItemPair(HierarchyItem rowItem, HierarchyItem colItem)
    {
      this.RowItem = rowItem;
      this.ColItem = colItem;
    }
  }
}
