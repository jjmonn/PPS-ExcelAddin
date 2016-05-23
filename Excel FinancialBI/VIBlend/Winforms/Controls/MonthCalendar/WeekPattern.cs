// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.WeekPattern
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class WeekPattern
  {
    private int numberOfWeeksToNextOccurence = 1;
    private bool occurOnMonday = true;
    private bool occurOnTuesday;
    private bool occurOnWednesday;
    private bool occurOnThursday;
    private bool occurOnFriday;
    private bool occurOnSaturday;
    private bool occurOnSunday;

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on Tuesday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnTuesday
    {
      get
      {
        return this.occurOnTuesday;
      }
      set
      {
        this.occurOnTuesday = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on Wednesday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnWednesday
    {
      get
      {
        return this.occurOnWednesday;
      }
      set
      {
        this.occurOnWednesday = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on Thursday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnThursday
    {
      get
      {
        return this.occurOnThursday;
      }
      set
      {
        this.occurOnThursday = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on Friday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnFriday
    {
      get
      {
        return this.occurOnFriday;
      }
      set
      {
        this.occurOnFriday = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on Saturday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnSaturday
    {
      get
      {
        return this.occurOnSaturday;
      }
      set
      {
        this.occurOnSaturday = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on Sunday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnSunday
    {
      get
      {
        return this.occurOnSunday;
      }
      set
      {
        this.occurOnSunday = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the day occurs on monday
    /// </summary>
    [DefaultValue(false)]
    public bool OccurOnMonday
    {
      get
      {
        return this.occurOnMonday;
      }
      set
      {
        this.occurOnMonday = value;
      }
    }

    /// <summary>Gets or sets the number of weeks to next occurence.</summary>
    /// <value>The number of weeks to next occurence.</value>
    [DefaultValue(1)]
    public int WeeksToNextOccurences
    {
      get
      {
        return this.numberOfWeeksToNextOccurence;
      }
      set
      {
        if (value < 1)
          return;
        this.numberOfWeeksToNextOccurence = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.WeekPattern" /> class.
    /// </summary>
    /// <param name="numberOfWeeksToNextOccurence">The number of weeks to next occurence.</param>
    public WeekPattern(int numberOfWeeksToNextOccurence)
    {
      this.numberOfWeeksToNextOccurence = numberOfWeeksToNextOccurence;
    }

    public override string ToString()
    {
      return "Weeks to next occurence: " + (object) this.numberOfWeeksToNextOccurence;
    }

    public WeekPattern Clone()
    {
      return new WeekPattern(this.WeeksToNextOccurences) { OccurOnFriday = this.OccurOnFriday, OccurOnMonday = this.OccurOnMonday, OccurOnSaturday = this.OccurOnSaturday, OccurOnSunday = this.OccurOnSunday, WeeksToNextOccurences = this.WeeksToNextOccurences, OccurOnWednesday = this.occurOnWednesday, OccurOnTuesday = this.OccurOnTuesday, OccurOnThursday = this.OccurOnThursday };
    }
  }
}
