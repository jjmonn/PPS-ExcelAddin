// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RecurrenceRange
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class RecurrenceRange
  {
    private DateTime recurrenceStartDate = DateTime.MinValue;
    private DateTime recurrenceEndDate = DateTime.MaxValue;
    private int numberOfOccurences = 10;

    /// <summary>
    /// Gets or sets a value indicating the number of occurences
    /// </summary>
    [DefaultValue(10)]
    public int NumberOfOccurences
    {
      get
      {
        return this.numberOfOccurences;
      }
      set
      {
        this.numberOfOccurences = value;
      }
    }

    /// <summary>Gets or sets the start date.</summary>
    /// <value>The start date.</value>
    public DateTime StartDate
    {
      get
      {
        return this.recurrenceStartDate;
      }
      set
      {
        if (!(value < this.EndDate))
          return;
        this.recurrenceStartDate = value;
      }
    }

    /// <summary>Gets or sets the end date.</summary>
    /// <value>The end date.</value>
    public DateTime EndDate
    {
      get
      {
        return this.recurrenceEndDate;
      }
      set
      {
        if (!(value > this.StartDate))
          return;
        this.recurrenceEndDate = value;
      }
    }

    public RecurrenceRange Clone()
    {
      return new RecurrenceRange() { StartDate = this.StartDate, NumberOfOccurences = this.NumberOfOccurences };
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
      return this.StartDate.ToShortDateString() + "-" + this.EndDate.ToShortDateString();
    }
  }
}
