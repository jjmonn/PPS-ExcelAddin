// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.DateTimeFilter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Globalization;

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>Represents a DateTime filter</summary>
  public class DateTimeFilter : IFilter<DateTime?>, IFilterBase
  {
    private DateTime? filterValue;
    private DateTimeComparisonOperator comparisonOperator;

    /// <summary>Gets or sets the value of the filter</summary>
    public DateTime? Value
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
    public DateTimeComparisonOperator ComparisonOperator
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
        return this.comparisonOperator != DateTimeComparisonOperator.NOT_NULL && this.comparisonOperator == DateTimeComparisonOperator.NULL;
      if (this.comparisonOperator == DateTimeComparisonOperator.NULL)
        return false;
      if (this.comparisonOperator == DateTimeComparisonOperator.NOT_NULL)
        return true;
      DateTime? nullable1 = new DateTime?(DateTime.MinValue);
      try
      {
        nullable1 = new DateTime?(DateTime.Parse(value.ToString(), (IFormatProvider) CultureInfo.InvariantCulture));
      }
      catch (Exception ex)
      {
        if (value.ToString() != "")
          return false;
      }
      switch (this.comparisonOperator)
      {
        case DateTimeComparisonOperator.EQUAL:
          DateTime? nullable2 = nullable1;
          DateTime? nullable3 = this.filterValue;
          if (nullable2.HasValue != nullable3.HasValue)
            return false;
          if (nullable2.HasValue)
            return nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault();
          return true;
        case DateTimeComparisonOperator.NOT_EQUAL:
          DateTime? nullable4 = nullable1;
          DateTime? nullable5 = this.filterValue;
          if (nullable4.HasValue != nullable5.HasValue)
            return true;
          if (nullable4.HasValue)
            return nullable4.GetValueOrDefault() != nullable5.GetValueOrDefault();
          return false;
        case DateTimeComparisonOperator.LESS_THAN:
          DateTime? nullable6 = nullable1;
          DateTime? nullable7 = this.filterValue;
          if (!(nullable6.HasValue & nullable7.HasValue))
            return false;
          return nullable6.GetValueOrDefault() < nullable7.GetValueOrDefault();
        case DateTimeComparisonOperator.LESS_THAN_OR_EQUAL:
          DateTime? nullable8 = nullable1;
          DateTime? nullable9 = this.filterValue;
          if (!(nullable8.HasValue & nullable9.HasValue))
            return false;
          return nullable8.GetValueOrDefault() <= nullable9.GetValueOrDefault();
        case DateTimeComparisonOperator.GREATER_THAN:
          DateTime? nullable10 = nullable1;
          DateTime? nullable11 = this.filterValue;
          if (!(nullable10.HasValue & nullable11.HasValue))
            return false;
          return nullable10.GetValueOrDefault() > nullable11.GetValueOrDefault();
        case DateTimeComparisonOperator.GREATER_THAN_OR_EQUAL:
          DateTime? nullable12 = nullable1;
          DateTime? nullable13 = this.filterValue;
          if (!(nullable12.HasValue & nullable13.HasValue))
            return false;
          return nullable12.GetValueOrDefault() >= nullable13.GetValueOrDefault();
        default:
          return true;
      }
    }
  }
}
