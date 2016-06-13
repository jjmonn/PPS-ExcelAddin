// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.StringFilter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Text.RegularExpressions;

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>Represents a filter for string types</summary>
  public class StringFilter : IFilter<string>, IFilterBase
  {
    private StringFilterComparisonOperator comparisonOperator = StringFilterComparisonOperator.NOT_EMPTY;
    private string filterValue;

    /// <summary>Gets or sets the value of the filter</summary>
    public string Value
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

    /// <summary>Gets or sets the comparision operator of the filter</summary>
    public StringFilterComparisonOperator ComparisonOperator
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
        return this.comparisonOperator == StringFilterComparisonOperator.NULL;
      string input;
      try
      {
        input = (string) value;
      }
      catch (Exception ex)
      {
        return true;
      }
      switch (this.comparisonOperator)
      {
        case StringFilterComparisonOperator.EMPTY:
          return input == string.Empty;
        case StringFilterComparisonOperator.NOT_EMPTY:
          return input != string.Empty;
        case StringFilterComparisonOperator.NOT_NULL:
          return input != null;
        case StringFilterComparisonOperator.CONTAINS:
          return input.IndexOf(this.filterValue, StringComparison.CurrentCultureIgnoreCase) != -1;
        case StringFilterComparisonOperator.CONTAINS_CASE_SENSITIVE:
          return input.Contains(this.filterValue);
        case StringFilterComparisonOperator.DOES_NOT_CONTAIN:
          return input.IndexOf(this.filterValue, StringComparison.CurrentCultureIgnoreCase) == -1;
        case StringFilterComparisonOperator.DOES_NOT_CONTAIN_CASE_SENSITIVE:
          return !input.Contains(this.filterValue);
        case StringFilterComparisonOperator.STARTS_WITH:
          return input.StartsWith(this.filterValue, StringComparison.CurrentCultureIgnoreCase);
        case StringFilterComparisonOperator.STARTS_WITH_CASE_SENSITIVE:
          return input.StartsWith(this.filterValue, StringComparison.CurrentCulture);
        case StringFilterComparisonOperator.ENDS_WITH:
          return input.EndsWith(this.filterValue, StringComparison.CurrentCultureIgnoreCase);
        case StringFilterComparisonOperator.ENDS_WITH_CASE_SENSITIVE:
          return input.EndsWith(this.filterValue, StringComparison.CurrentCulture);
        case StringFilterComparisonOperator.EQUAL:
          return input.Equals(this.filterValue, StringComparison.CurrentCultureIgnoreCase);
        case StringFilterComparisonOperator.EQUAL_CASE_SENSITIVE:
          return input.Equals(this.filterValue, StringComparison.CurrentCulture);
        case StringFilterComparisonOperator.REGEX:
          return Regex.Match(input, this.filterValue).Success;
        default:
          return false;
      }
    }
  }
}
