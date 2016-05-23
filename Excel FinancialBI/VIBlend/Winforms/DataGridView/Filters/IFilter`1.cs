// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.IFilter`1
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>Generic filter interface for type T</summary>
  public interface IFilter<T> : IFilterBase
  {
    /// <summary>Filter's value</summary>
    T Value { get; set; }
  }
}
