// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DateTimeEditorFormatItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Globalization;

namespace VIBlend.WinForms.Controls
{
  public class DateTimeEditorFormatItem
  {
    private string formatValue;
    private DateTimeFormatInfo info;
    private DateTimeItemType type;

    /// <summary>Gets the format value.</summary>
    /// <value>The format value.</value>
    public string FormatValue
    {
      get
      {
        return this.formatValue;
      }
    }

    /// <summary>Gets the date time info.</summary>
    /// <value>The date time info.</value>
    public DateTimeFormatInfo DateTimeInfo
    {
      get
      {
        return this.info;
      }
    }

    /// <summary>Gets the maximum value.</summary>
    /// <value>The maximum value.</value>
    public int MaximumValue
    {
      get
      {
        switch (this.FormatValue.Length)
        {
          case 1:
            return 9;
          case 2:
            return 99;
          default:
            return 999;
        }
      }
    }

    /// <summary>Gets the item value.</summary>
    /// <value>The item value.</value>
    public int ItemValue
    {
      get
      {
        switch (this.FormatValue.Length)
        {
          case 1:
            return 100;
          case 2:
            return 10;
          default:
            return 1;
        }
      }
    }

    /// <summary>Gets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public DateTimeItemType ItemType
    {
      get
      {
        return this.type;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DateTimeEditorFormatItem" /> class.
    /// </summary>
    /// <param name="formatValue">The format value.</param>
    /// <param name="info">The info.</param>
    /// <param name="type">The type.</param>
    public DateTimeEditorFormatItem(string formatValue, DateTimeFormatInfo info, DateTimeItemType type)
    {
      this.formatValue = formatValue;
      this.info = info;
      this.type = type;
    }

    /// <summary>Gets the date time with offset.</summary>
    /// <param name="offset">The offset.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public DateTime GetDateTimeWithOffset(int offset, DateTime value)
    {
      DateTime dateTime1 = value;
      switch (this.type)
      {
        case DateTimeItemType.Year:
          if (offset == 0)
            offset = 1;
          dateTime1 = dateTime1.AddYears(offset - value.Year);
          break;
        case DateTimeItemType.Month:
          DateTime dateTime2 = dateTime1.AddMonths(offset - dateTime1.Month);
          if (offset == 2 && dateTime1.Day == 29 && dateTime1.Day != dateTime2.Day)
          {
            DateTime dateTime3 = dateTime1;
            while (!DateTime.IsLeapYear(dateTime3.Year))
              dateTime3 = dateTime3.AddYears(1);
            dateTime2 = dateTime3.AddMonths(offset - dateTime3.Month);
          }
          dateTime1 = dateTime2;
          break;
        case DateTimeItemType.Day:
          DateTime dateTime4 = dateTime1.AddDays((double) (offset - dateTime1.Day));
          if (dateTime4.Day != offset)
          {
            if (offset == 29 && dateTime1.Month == 2)
            {
              dateTime4 = dateTime1;
              while (!DateTime.IsLeapYear(dateTime4.Year))
                dateTime4 = dateTime4.AddYears(1);
              dateTime4 = dateTime4.AddDays((double) (offset - dateTime4.Day));
            }
            else
            {
              dateTime4 = dateTime1.AddMonths(1 - dateTime1.Month);
              dateTime4 = dateTime4.AddDays((double) (offset - dateTime1.Day));
            }
          }
          dateTime1 = dateTime4;
          break;
        case DateTimeItemType.Minute:
          dateTime1 = dateTime1.AddMinutes((double) (offset - dateTime1.Minute));
          break;
        case DateTimeItemType.Second:
          dateTime1 = dateTime1.AddSeconds((double) (offset - dateTime1.Second));
          break;
        case DateTimeItemType.Millisecond:
          dateTime1 = dateTime1.AddMilliseconds((double) (offset * this.ItemValue - dateTime1.Millisecond));
          break;
        case DateTimeItemType.FORMAT_hh:
          int num = offset == 12 ? 0 : offset;
          dateTime1 = dateTime1.AddHours((double) (num - dateTime1.Hour % 12));
          break;
        case DateTimeItemType.FORMAT_HH:
          dateTime1 = dateTime1.AddHours((double) (offset - dateTime1.Hour));
          break;
        case DateTimeItemType.FORMAT_AMPM:
          dateTime1 = dateTime1.AddHours((double) (12 * (offset - value.Hour / 12)));
          break;
      }
      return dateTime1;
    }

    /// <summary>Dates the parser.</summary>
    /// <param name="formattedDateTime">The formatted date time.</param>
    /// <returns></returns>
    public string DateParser(DateTime? formattedDateTime)
    {
      if (!formattedDateTime.HasValue)
        return "";
      return formattedDateTime.Value.ToString(this.formatValue.Length == 1 ? 37.ToString() + this.formatValue : this.formatValue, (IFormatProvider) this.info);
    }

    /// <summary>Gets the editor.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public IDateTimeEditor GetDateTimeEditorByItemType(DateTime value)
    {
      switch (this.type)
      {
        case DateTimeItemType.Year:
          return (IDateTimeEditor) new DateTimeFormatDayMonthYearEditor(value.Year, this.FormatValue.Length, this.info, this, true);
        case DateTimeItemType.Month:
          string[] monthsNames = this.FormatValue.Length != 3 ? (this.FormatValue.Length <= 3 ? (string[]) null : this.info.MonthNames) : this.info.AbbreviatedMonthNames;
          return (IDateTimeEditor) new DateTimeFormatDayMonthYearEditor(value.Month, this.FormatValue.Length == 2 ? 2 : 1, monthsNames, this, true);
        case DateTimeItemType.Day:
          int year = value.Year;
          int month = value.Month;
          string[] dayKeys = this.FormatValue.Length != 3 ? (this.FormatValue.Length <= 3 ? (string[]) null : this.info.DayNames) : this.info.AbbreviatedDayNames;
          int day = value.Day;
          if (dayKeys != null)
          {
            int num1 = (int) this.info.Calendar.GetDayOfWeek(value);
          }
          return (IDateTimeEditor) new DateTimeFormatDayMonthYearEditor((object) value, value.Day, 1, DateTime.DaysInMonth(year, month), this.FormatValue.Length == 1 ? 1 : 2, 2, dayKeys, this, true);
        case DateTimeItemType.Minute:
          return (IDateTimeEditor) new DateTimeFormatBaseEditor((long) value.Minute, 0L, 59L, this.FormatValue.Length == 1 ? 1L : 2L, 2L, this);
        case DateTimeItemType.Second:
          return (IDateTimeEditor) new DateTimeFormatBaseEditor((long) value.Second, 0L, 59L, this.FormatValue.Length == 1 ? 1L : 2L, 2L, this);
        case DateTimeItemType.Millisecond:
          return (IDateTimeEditor) new DateTimeFormatBaseEditor((long) (value.Millisecond / this.ItemValue), 0L, (long) this.MaximumValue, (long) this.FormatValue.Length, (long) this.FormatValue.Length, this);
        case DateTimeItemType.FORMAT_hh:
          int num2 = value.Hour % 12;
          if (num2 == 0)
            num2 = 12;
          return (IDateTimeEditor) new DateTimeFormatBaseEditor((long) num2, 1L, 12L, this.FormatValue.Length == 1 ? 1L : 2L, 2L, this);
        case DateTimeItemType.FORMAT_HH:
          return (IDateTimeEditor) new DateTimeFormatBaseEditor((long) value.Hour, 0L, 23L, this.FormatValue.Length == 1 ? 1L : 2L, 2L, this);
        case DateTimeItemType.FORMAT_AMPM:
          return (IDateTimeEditor) new EditorAmPmItem(this.FormatValue, value.Hour / 12, this.info.AMDesignator, this.info.PMDesignator, this);
        case DateTimeItemType.Readonly:
          return (IDateTimeEditor) new DisabledEditor(this.FormatValue, value.Day, this);
        default:
          return (IDateTimeEditor) null;
      }
    }
  }
}
