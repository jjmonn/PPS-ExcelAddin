// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.NumericFilter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Globalization;

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>Represents a numeric filter</summary>
  public class NumericFilter : IFilter<double?>, IFilterBase
  {
    private double? filterValue;
    private NumericComparisonOperator comparisonOperator;

    /// <summary>Gets or sets the value of the filter</summary>
    public double? Value
    {
      get
      {
        return this.filterValue;
      }
      set
      {
        this.filterValue = value;
      }
    }

    /// <summary>Gets or sets the comparison operator of the filter</summary>
    public NumericComparisonOperator ComparisonOperator
    {
      get
      {
        return this.comparisonOperator;
      }
      set
      {
        this.comparisonOperator = value;
      }
    }

    bool IFilterBase.Evaluate(object value)
    {
      if (value == null || value == DBNull.Value)
        return this.comparisonOperator != NumericComparisonOperator.NOT_NULL && this.comparisonOperator == NumericComparisonOperator.NULL;
      if (this.comparisonOperator == NumericComparisonOperator.NULL)
        return false;
      if (this.comparisonOperator == NumericComparisonOperator.NOT_NULL)
        return true;
      double num1 = 0.0;
      try
      {
        num1 = double.Parse(value.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (Exception ex)
      {
        if (value.ToString() != "")
          return false;
      }
      switch (this.comparisonOperator)
      {
        case NumericComparisonOperator.EQUAL:
          double num2 = num1;
          double? nullable1 = this.filterValue;
          if (num2 == nullable1.GetValueOrDefault())
            return nullable1.HasValue;
          return false;
        case NumericComparisonOperator.NOT_EQUAL:
          double num3 = num1;
          double? nullable2 = this.filterValue;
          if (num3 == nullable2.GetValueOrDefault())
            return !nullable2.HasValue;
          return true;
        case NumericComparisonOperator.LESS_THAN:
          double num4 = num1;
          double? nullable3 = this.filterValue;
          if (num4 < nullable3.GetValueOrDefault())
            return nullable3.HasValue;
          return false;
        case NumericComparisonOperator.LESS_THAN_OR_EQUAL:
          double num5 = num1;
          double? nullable4 = this.filterValue;
          if (num5 <= nullable4.GetValueOrDefault())
            return nullable4.HasValue;
          return false;
        case NumericComparisonOperator.GREATER_THAN:
          double num6 = num1;
          double? nullable5 = this.filterValue;
          if (num6 > nullable5.GetValueOrDefault())
            return nullable5.HasValue;
          return false;
        case NumericComparisonOperator.GREATER_THAN_OR_EQUAL:
          double num7 = num1;
          double? nullable6 = this.filterValue;
          if (num7 >= nullable6.GetValueOrDefault())
            return nullable6.HasValue;
          return false;
        default:
          return true;
      }
    }
  }
}
