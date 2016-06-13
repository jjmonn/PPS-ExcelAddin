// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DayPattern
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  [TypeConverter(typeof (DayPatternConverter))]
  public class DayPattern
  {
    private int numberOfDaysToNextOccurence = 1;

    /// <summary>Gets or sets the days to next occurence.</summary>
    /// <value>The days to next occurence.</value>
    [DefaultValue(1)]
    public int DaysToNextOccurence
    {
      get
      {
        return this.numberOfDaysToNextOccurence;
      }
      set
      {
        if (value < 1)
          return;
        this.numberOfDaysToNextOccurence = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DayPattern" /> class.
    /// </summary>
    public DayPattern()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DayPattern" /> class.
    /// </summary>
    /// <param name="numberOfDaysToNextOccurence">The number of days to next occurence.</param>
    public DayPattern(int numberOfDaysToNextOccurence)
    {
      this.numberOfDaysToNextOccurence = numberOfDaysToNextOccurence;
    }

    public DayPattern Clone()
    {
      return new DayPattern() { DaysToNextOccurence = this.DaysToNextOccurence };
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
      return this.DaysToNextOccurence.ToString();
    }
  }
}
