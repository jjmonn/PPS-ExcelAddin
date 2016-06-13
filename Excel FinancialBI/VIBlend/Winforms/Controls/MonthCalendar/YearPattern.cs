// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.YearPattern
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace VIBlend.WinForms.Controls
{
  [TypeConverter(typeof (YearPatternConverter))]
  public class YearPattern
  {
    private int month = 1;
    private int day = 1;

    /// <summary>Gets or sets the month.</summary>
    /// <value>The month.</value>
    [DefaultValue(1)]
    public int Month
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

    /// <summary>Gets or sets the day.</summary>
    /// <value>The day.</value>
    [DefaultValue(1)]
    public int Day
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.YearPattern" /> class.
    /// </summary>
    /// <param name="month">The month.</param>
    /// <param name="day">The day.</param>
    public YearPattern(int month, int day)
    {
      if (month >= 1 && month <= 12)
        this.month = month;
      if (day < 1 || day > CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(DateTime.Now.Year, this.month))
        return;
      this.day = day;
    }

    public YearPattern Clone()
    {
      return new YearPattern(this.Month, this.Day);
    }
  }
}
