// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.FilterGroup`1
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>Represents a group of filters</summary>
  public class FilterGroup<T> : IFilterBase
  {
    private List<IFilterBase> filters = new List<IFilterBase>();
    private List<FilterOperator> operators = new List<FilterOperator>();

    /// <summary>Gets the number of filters in the group</summary>
    public int FiltersCount
    {
      get
      {
        return this.filters.Count;
      }
    }

    /// <summary>Adds a filter to the group</summary>
    /// <param name="filterOperator">The logical operator which concatinates this filter to the previous filters in the group</param>
    /// <param name="filter">The new filter to add</param>
    public void AddFilter(FilterOperator filterOperator, IFilterBase filter)
    {
      this.filters.Add(filter);
      this.operators.Add(filterOperator);
    }

    /// <summary>Removes a filter from the group</summary>
    /// <param name="filter">A reference to the filter that will be removed</param>
    public void RemoveFilter(IFilterBase filter)
    {
      int index = this.filters.IndexOf(filter);
      if (index == -1)
        return;
      this.filters.RemoveAt(index);
      this.operators.RemoveAt(index);
    }

    /// <summary>Returns the logical operator at a specific index</summary>
    public FilterOperator GetOperatorAt(int index)
    {
      if (index < 0 || index >= this.operators.Count)
        throw new IndexOutOfRangeException();
      return this.operators[index];
    }

    /// <summary>Sets the logical operator at a specific index</summary>
    public void SetOperatorAt(int index, FilterOperator filterOperator)
    {
      if (index < 0 || index >= this.operators.Count)
        throw new IndexOutOfRangeException();
      this.operators[index] = filterOperator;
    }

    /// <summary>Gets a reference to the filter at a specific index</summary>
    public IFilterBase GetFilterAt(int index)
    {
      if (index < 0 || index >= this.filters.Count)
        throw new IndexOutOfRangeException();
      return this.filters[index];
    }

    /// <summary>Sets the filter at a specific index</summary>
    /// <param name="index">The index of the filter in the group</param>
    /// <param name="filter">A reference to the new filter value</param>
    public void SetFilterAt(int index, IFilterBase filter)
    {
      if (index < 0 || index >= this.filters.Count)
        throw new IndexOutOfRangeException();
      this.filters[index] = filter;
    }

    /// <summary>Removes all filters from the group</summary>
    public void Clear()
    {
      this.filters.Clear();
      this.operators.Clear();
    }

    /// <summary>
    /// Evaluates the filter expression represented by the group against a specific value
    /// </summary>
    /// <param name="value">The value to evaluate</param>
    /// <returns>Returns true if the value passes through the filter</returns>
    public bool Evaluate(object value)
    {
      bool flag1 = true;
      for (int index = 0; index < this.filters.Count; ++index)
      {
        bool flag2 = this.filters[index].Evaluate(value);
        if (index == 0)
        {
          flag1 = flag2;
        }
        else
        {
          flag1 = this.operators[index] != FilterOperator.OR ? flag1 && flag2 : flag1 || flag2;
          if (!flag1)
            break;
        }
      }
      return flag1;
    }
  }
}
