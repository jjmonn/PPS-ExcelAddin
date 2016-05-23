// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.BoundFieldFilter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Collections.Generic;
using VIBlend.WinForms.DataGridView.Filters;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a data field filter used during the data binding process
  /// </summary>
  public class BoundFieldFilter
  {
    /// <summary>The name of the column from the data source</summary>
    /// <remarks>
    /// When you bind to a database table, this property should be set to the name of the column that you want to bind.
    /// If you want to bind to a list of objects, the value of this property should match the name of an object's public property which you want to bind.
    /// Note that the value of this property is case sensitive
    /// </remarks>
    public string DataField { get; set; }

    /// <summary>Collection of filters applied to the DataField</summary>
    public List<IFilterBase> Filters { get; private set; }

    /// <summary>BoundFieldFilter constructor</summary>
    public BoundFieldFilter()
    {
      this.Filters = new List<IFilterBase>();
    }
  }
}
