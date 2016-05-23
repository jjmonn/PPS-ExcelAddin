// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.PivotFieldFunction
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents an aggregation function which will be used to calculate the value of a grid cell
  /// when the grid is bound to a data source in a pivot table mode
  /// </summary>
  public enum PivotFieldFunction
  {
    Sum,
    Count,
    Average,
    Max,
    Min,
    Product,
  }
}
