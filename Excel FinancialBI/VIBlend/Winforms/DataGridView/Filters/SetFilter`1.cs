// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.SetFilter`1
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>
  /// Represents a Filter that uses a set of filter items with true and false states.
  /// </summary>
  public class SetFilter<T> : IFilterBase
  {
    private Dictionary<T, bool> items = new Dictionary<T, bool>();
    private bool isStateAllTrue = true;

    /// <summary>Gets the items count.</summary>
    /// <value>The items count.</value>
    public int ItemsCount
    {
      get
      {
        return this.items.Count;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is state all true.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is state all true; otherwise, <c>false</c>.
    /// </value>
    public bool IsStateAllTrue
    {
      get
      {
        return this.isStateAllTrue;
      }
    }

    bool IFilterBase.Evaluate(object value)
    {
      if (this.isStateAllTrue)
        return true;
      try
      {
        if (this is SetFilter<double?> || this is SetFilter<double>)
          value = (object) double.Parse(value.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
        else if (this is SetFilter<DateTime?> || this is SetFilter<DateTime>)
        {
          DateTime dateTime = DateTime.MinValue;
          value = (object) DateTime.Parse(value.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
        }
        else if (this is SetFilter<string>)
        {
          if (value is DBNull)
            value = (object) string.Empty;
          if (value != null)
            value = (object) value.ToString();
        }
        T key = (T) value;
        if (!this.items.ContainsKey(key))
          return true;
        return this.items[key];
      }
      catch (Exception ex)
      {
        return true;
      }
    }

    /// <summary>Adds the item.</summary>
    /// <param name="item">The item.</param>
    public void AddItem(T item)
    {
      if (this.items.ContainsKey(item))
        return;
      this.items.Add(item, true);
    }

    /// <summary>Removes the item.</summary>
    /// <param name="item">The item.</param>
    public void RemoveItem(T item)
    {
      if (!this.items.ContainsKey(item))
        return;
      bool flag = this.items[item];
      this.items.Remove(item);
      if (this.isStateAllTrue || flag)
        return;
      this.EvaluateAllItemsState();
    }

    /// <summary>Determines whether the specified item contains item.</summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if the specified item contains item; otherwise, <c>false</c>.
    /// </returns>
    public bool ContainsItem(T item)
    {
      return this.items.ContainsKey(item);
    }

    /// <summary>Sets the state of the item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="state">if set to <c>true</c> [state].</param>
    public void SetItemState(T item, bool state)
    {
      if (!this.items.ContainsKey(item))
        return;
      this.items[item] = state;
      if (!this.isStateAllTrue && state)
      {
        this.EvaluateAllItemsState();
      }
      else
      {
        if (!this.isStateAllTrue || state)
          return;
        this.isStateAllTrue = false;
      }
    }

    /// <summary>Gets the state of the item.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public bool GetItemState(T item)
    {
      if (this.items.ContainsKey(item))
        return false;
      return this.items[item];
    }

    private void EvaluateAllItemsState()
    {
      Dictionary<T, bool>.Enumerator enumerator = this.items.GetEnumerator();
      this.isStateAllTrue = true;
      while (enumerator.MoveNext())
      {
        if (!enumerator.Current.Value)
        {
          this.isStateAllTrue = false;
          break;
        }
      }
    }

    /// <summary>Sets the state of all items.</summary>
    /// <param name="state">if set to <c>true</c> [state].</param>
    public void SetAllItemsState(bool state)
    {
      Dictionary<T, bool>.Enumerator enumerator = this.items.GetEnumerator();
      while (enumerator.MoveNext())
        this.items[enumerator.Current.Key] = state;
      this.isStateAllTrue = state;
    }
  }
}
