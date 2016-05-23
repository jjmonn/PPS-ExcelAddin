// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.BoundValueField
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a data binding relationship between a column in the data source, and a Summary HierarchyItem which specifies a Pivot table value item
  /// </summary>
  public class BoundValueField : BoundField
  {
    private PivotFieldFunction pivotFunction;

    /// <summary>
    /// Specifies the pivot table data aggregation function. This function will be used to calculate the values of the data grid cells
    /// </summary>
    public PivotFieldFunction Function
    {
      get
      {
        return this.pivotFunction;
      }
      set
      {
        this.pivotFunction = value;
      }
    }

    /// <summary>BoundValueField constructor.</summary>
    /// <param name="text">Text to display in the HierarchyItem.</param>
    /// <param name="dataField">DataField to bind to.</param>
    /// <param name="function">Data aggregation function.</param>
    public BoundValueField(string text, string dataField, PivotFieldFunction function)
      : base(text, dataField)
    {
      this.pivotFunction = function;
    }
  }
}
