// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.MonthPattern
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace VIBlend.WinForms.Controls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class MonthPattern
  {
    private int month = 1;
    private int day = 1;

    /// <summary>Gets or sets the day.</summary>
    /// <value>The day.</value>
    [DefaultValue(1)]
    public int OccurenceDay
    {
      get
      {
        return this.day;
      }
      set
      {
        if (value < 1 || value > CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(DateTime.Now.Year, this.month))
          return;
        this.day = value;
      }
    }

    /// <summary>Gets or sets the month.</summary>
    /// <value>The month.</value>
    [DefaultValue(1)]
    public int MonthsToNextOccurence
    {
      get
      {
        return this.month;
      }
      set
      {
        if (value < 1 || value > 12)
          return;
        this.month = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.MonthPattern" /> class.
    /// </summary>
    /// <param name="month">The month.</param>
    /// <param name="day">The day.</param>
    public MonthPattern(int month, int day)
    {
      if (month >= 1 && month <= 12)
        this.month = month;
      if (day < 1 || day > CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(DateTime.Now.Year, this.month))
        return;
      this.day = day;
    }

    public MonthPattern Clone()
    {
      return new MonthPattern(this.month, this.day) { MonthsToNextOccurence = this.MonthsToNextOccurence, OccurenceDay = this.OccurenceDay };
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
      return "Months to next occurence:" + (object) this.MonthsToNextOccurence + " Occurence Day:" + (object) this.OccurenceDay;
    }
  }
}
