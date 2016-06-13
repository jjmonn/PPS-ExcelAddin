// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DateTimeFormatDayMonthYearEditor
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Globalization;

namespace VIBlend.WinForms.Controls
{
  public class DateTimeFormatDayMonthYearEditor : DateTimeFormatBaseEditor
  {
    private int formatValueLength;
    private DateTimeFormatInfo dateTimeFormatInfo;
    internal bool handleYears;
    internal bool handleDays;
    internal bool handleMonths;
    private string[] days;
    private object currentValue;
    private string[] dateTimeMonths;

    /// <summary>Gets the minimum amount of digits</summary>
    /// <value></value>
    public override long MinPositions
    {
      get
      {
        if (!this.handleYears)
          return base.MinPositions;
        if (this.formatValueLength != 4)
          return base.MinPositions;
        if (this.positions <= 1L)
          return 1;
        if (this.positions >= 4L)
          return 4;
        return this.positions;
      }
    }

    /// <summary>Gets editor's text</summary>
    public override string TextValue
    {
      get
      {
        if (this.handleDays)
        {
          DateTimeFormatInfo info = new DateTimeFormatInfo();
          if (this.days == null)
            return base.TextValue;
          long val = this.Value % 7L + 1L;
          return this.days[this.GetDayOfWeek(info, val)];
        }
        if (!this.handleMonths)
          return base.TextValue;
        if (this.dateTimeMonths == null || this.Value < 1L || this.Value > 12L)
          return base.TextValue;
        return this.dateTimeMonths[this.Value - 1L];
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DateTimeFormatDayMonthYearEditor" /> class.
    /// </summary>
    /// <param name="baseYear">The base year.</param>
    /// <param name="formatValueLength">Length of the format value.</param>
    /// <param name="info">The info.</param>
    /// <param name="item">The item.</param>
    /// <param name="val">if set to <c>true</c> [val].</param>
    public DateTimeFormatDayMonthYearEditor(int baseYear, int formatValueLength, DateTimeFormatInfo info, DateTimeEditorFormatItem item, bool val)
      : base(formatValueLength <= 4 ? 0L : (long) DateTime.MinValue.Year, formatValueLength < 4 ? 99L : (long) DateTime.MaxValue.Year, formatValueLength == 2 ? 2L : 1L, formatValueLength > 3 ? 4L : 2L, item)
    {
      this.InitializeYearEditor(baseYear, formatValueLength, info);
      this.handleYears = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DateTimeFormatDayMonthYearEditor" /> class.
    /// </summary>
    /// <param name="editedValue">The edited value.</param>
    /// <param name="initialValue">The initial value.</param>
    /// <param name="minValue">The min value.</param>
    /// <param name="maxValue">The max value.</param>
    /// <param name="minEditingPositions">The min editing positions.</param>
    /// <param name="maxEditingPositions">The max editing positions.</param>
    /// <param name="dayKeys">The day keys.</param>
    /// <param name="item">The item.</param>
    /// <param name="val">if set to <c>true</c> [val].</param>
    public DateTimeFormatDayMonthYearEditor(object editedValue, int initialValue, int minValue, int maxValue, int minEditingPositions, int maxEditingPositions, string[] dayKeys, DateTimeEditorFormatItem item, bool val)
      : base((long) initialValue, (long) minValue, (long) maxValue, 1L, (long) maxEditingPositions, item)
    {
      this.currentValue = editedValue;
      this.days = dayKeys;
      this.handleDays = true;
    }

    public DateTimeFormatDayMonthYearEditor(int baseValue, int positions, string[] monthsNames, DateTimeEditorFormatItem item, bool val)
      : base((long) baseValue, 1L, 12L, (long) positions, 2L, item)
    {
      this.dateTimeMonths = monthsNames;
      if (this.dateTimeMonths != null && this.dateTimeMonths[12] != null && this.dateTimeMonths[12].Length > 0)
        this.dateTimeMonths = (string[]) null;
      this.handleMonths = true;
    }

    /// <summary>Shifts the year.</summary>
    /// <param name="calendar">The calendar.</param>
    /// <returns></returns>
    private static int RealYear(Calendar calendar)
    {
      return calendar.GetYear(new DateTime(2000, 1, 1)) - 2000;
    }

    private void InitializeYearEditor(int baseYear, int formatValueLength, DateTimeFormatInfo info)
    {
      this.formatValueLength = formatValueLength;
      this.dateTimeFormatInfo = info;
      int num = Math.Max(Math.Min(baseYear + DateTimeFormatDayMonthYearEditor.RealYear(info.Calendar), 9999), 1);
      this.UpdateActiveEditor(formatValueLength < 4 ? (long) (num % 100) : (long) num);
    }

    private long GetDayOfWeek(DateTimeFormatInfo info, long val)
    {
      if (this.currentValue is DateTime)
      {
        DateTime time = new DateTime(((DateTime) this.currentValue).Year, ((DateTime) this.currentValue).Month, ((DateTime) this.currentValue).Day, ((DateTime) this.currentValue).Hour, ((DateTime) this.currentValue).Minute, ((DateTime) this.currentValue).Second, ((DateTime) this.currentValue).Millisecond);
        val = (long) info.Calendar.GetDayOfWeek(time);
      }
      return val;
    }

    /// <summary>Inserts the specified input.</summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public bool Insert(string input)
    {
      if (this.handleDays)
      {
        if (this.days != null)
        {
          bool res1 = false;
          string lower = input.ToLower();
          bool res2 = this.InsertLongString(input, res1, lower);
          if (res2)
            return res2;
          bool flag = this.InsertShortString(res2, lower);
          if (flag)
            return flag;
        }
        return this.InsertString(input);
      }
      if (this.handleMonths && this.dateTimeMonths != null)
      {
        string lower = input.ToLower();
        bool res1 = false;
        bool res2 = this.InsertLongString2(input, lower, res1);
        if (res2)
          return res2;
        bool flag = this.InsertShortString2(lower, res2);
        if (flag)
          return flag;
      }
      return this.InsertString(input);
    }

    private bool InsertShortString(bool res, string lowerInput)
    {
      if (lowerInput.Length == 1)
      {
        for (int index = 0; index < 6; ++index)
        {
          long newValue = (this.Value + (long) index) % 7L + 1L;
          if (this.days[newValue - 1L].ToLower().Substring(0, 1) == lowerInput)
          {
            this.UpdateActiveEditor(newValue);
            res = true;
            break;
          }
        }
      }
      return res;
    }

    private bool InsertLongString(string input, bool res, string lowerInput)
    {
      if (input.Length > 0)
      {
        for (int index = 0; index < 6; ++index)
        {
          long newValue = (this.Value + (long) index) % 7L + 1L;
          if (this.days[newValue - 1L].ToLower() == lowerInput)
          {
            this.UpdateActiveEditor(newValue);
            res = true;
            break;
          }
        }
      }
      return res;
    }

    private bool InsertLongString2(string input, string lowerInput, bool res)
    {
      if (input.Length > 0)
      {
        for (int index = 0; index < 11; ++index)
        {
          long newValue = (this.Value + (long) index) % 12L + 1L;
          if (this.dateTimeMonths[newValue - 1L].ToLower() == lowerInput)
          {
            this.UpdateActiveEditor(newValue);
            res = true;
            break;
          }
        }
      }
      return res;
    }

    private bool InsertShortString2(string lowerInput, bool res)
    {
      if (lowerInput.Length == 1)
      {
        for (int index = 0; index < 11; ++index)
        {
          long newValue = (this.Value + (long) index) % 12L + 1L;
          if (this.dateTimeMonths[newValue - 1L].ToLower().Substring(0, 1) == lowerInput)
          {
            this.UpdateActiveEditor(newValue);
            res = true;
            break;
          }
        }
      }
      return res;
    }
  }
}
