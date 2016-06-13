// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DateTimeFormatBaseEditor
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Globalization;

namespace VIBlend.WinForms.Controls
{
  public class DateTimeFormatBaseEditor : IDateTimeEditor
  {
    private long minValue;
    private long maxValue;
    private long minEditPositions;
    private long maxEditPositions;
    private long value;
    protected internal long positions;
    private DateTimeEditorFormatItem item;

    protected bool HasDigits
    {
      get
      {
        return this.positions > 0L;
      }
    }

    /// <summary>Gets the min available value</summary>
    public long MinimumValue
    {
      get
      {
        return this.minValue;
      }
      set
      {
        this.minValue = value;
      }
    }

    /// <summary>Gets the max available value</summary>
    public long MaximumValue
    {
      get
      {
        return this.maxValue;
      }
      set
      {
        this.maxValue = value;
      }
    }

    /// <summary>Gets the minimum amount of digits</summary>
    public virtual long MinPositions
    {
      get
      {
        return this.minEditPositions;
      }
    }

    /// <summary>Gets the maximum amount of digits</summary>
    public long MaxPositions
    {
      get
      {
        return this.maxEditPositions;
      }
    }

    /// <summary>Gets or sets the current value</summary>
    public long Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    /// <summary>Updates editor's text</summary>
    public virtual string TextValue
    {
      get
      {
        return this.Value.ToString("d" + this.MinPositions.ToString("d2", (IFormatProvider) CultureInfo.InvariantCulture), (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DateTimeFormatBaseEditor" /> class.
    /// </summary>
    /// <param name="initialValue">The initial value.</param>
    /// <param name="minValue">The min value.</param>
    /// <param name="maxValue">The max value.</param>
    /// <param name="minEditPositions">The min edit positions.</param>
    /// <param name="maxEditPositions">The max edit positions.</param>
    /// <param name="item">The item.</param>
    public DateTimeFormatBaseEditor(long initialValue, long minValue, long maxValue, long minEditPositions, long maxEditPositions, DateTimeEditorFormatItem item)
      : this(minValue, maxValue, minEditPositions, maxEditPositions, item)
    {
      this.UpdateActiveEditor(initialValue);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DateTimeFormatBaseEditor" /> class.
    /// </summary>
    /// <param name="minValue">The min value.</param>
    /// <param name="maxValue">The max value.</param>
    /// <param name="minEditPositions">The min edit positions.</param>
    /// <param name="maxEditPositions">The max edit positions.</param>
    /// <param name="item">The item.</param>
    public DateTimeFormatBaseEditor(long minValue, long maxValue, long minEditPositions, long maxEditPositions, DateTimeEditorFormatItem item)
    {
      this.InitializeFields(minValue, maxValue, minEditPositions, maxEditPositions, item);
    }

    /// <summary>Updates the  current value</summary>
    /// <param name="newValue"></param>
    protected void UpdateActiveEditor(long newValue)
    {
      this.value = newValue;
      this.positions = 0L;
    }

    private void InitializeFields(long minValue, long maxValue, long minEditPositions, long maxEditPositions, DateTimeEditorFormatItem item)
    {
      this.minValue = minValue;
      this.maxValue = maxValue;
      this.minEditPositions = minEditPositions;
      this.maxEditPositions = maxEditPositions;
      this.UpdateActiveEditor(minValue);
      this.item = item;
    }

    public DateTimeEditorFormatItem GetDateTimeItem()
    {
      return this.item;
    }

    /// <summary>Inserts the specified insered value.</summary>
    /// <param name="inseredValue">The insered value.</param>
    /// <returns></returns>
    public bool InsertString(string inseredValue)
    {
      if (inseredValue.Length == 0)
        return this.Delete();
      bool res = true;
      string tmp;
      long entries;
      bool insertionString = this.GetInsertionString(inseredValue, res, out tmp, out entries);
      if (!insertionString)
        return insertionString;
      tmp = this.SetValueByString(tmp, entries);
      return true;
    }

    private string SetValueByString(string tmp, long entries)
    {
      tmp = this.FixValueString(tmp);
      this.value = long.Parse(tmp, (IFormatProvider) CultureInfo.InvariantCulture);
      this.positions += entries;
      return tmp;
    }

    private string FixValueString(string tmp)
    {
      if ((long) tmp.Length > this.MaxPositions)
        tmp = tmp.Substring(tmp.Length - (int) this.MaxPositions);
      while (long.Parse(tmp, (IFormatProvider) CultureInfo.InvariantCulture) > this.MaximumValue)
        tmp = tmp.Substring(1);
      return tmp;
    }

    private bool GetInsertionString(string inseredValue, bool res, out string tmp, out long entries)
    {
      string @string = this.value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
      tmp = this.InitializeValueString(@string);
      if ((long) tmp.Length >= this.MaxPositions)
        tmp = "";
      entries = 0L;
      foreach (char c in inseredValue)
      {
        if (char.IsDigit(c))
        {
          tmp = tmp + (object) c;
          ++entries;
        }
        else
        {
          res = false;
          break;
        }
      }
      return res;
    }

    private string InitializeValueString(string formattedValue)
    {
      string str = "";
      if (this.HasDigits)
        str = formattedValue;
      return str;
    }

    /// <summary>Deletes this instance.</summary>
    /// <returns></returns>
    public bool Delete()
    {
      if (this.Value == this.MinimumValue && !this.HasDigits)
        return false;
      this.UpdateActiveEditor(this.MinimumValue);
      return true;
    }

    /// <summary>Ups this instance.</summary>
    /// <returns></returns>
    public bool Up()
    {
      long newValue = this.CorrectMaximumValue(this.Value + 1L);
      if (newValue == this.Value && !this.HasDigits)
        return false;
      this.UpdateActiveEditor(newValue);
      return true;
    }

    private long CorrectMaximumValue(long val)
    {
      if (val > this.MaximumValue)
        val = this.MinimumValue;
      return val;
    }

    /// <summary>Downs this instance.</summary>
    /// <returns></returns>
    public bool Down()
    {
      long newValue = this.CorrectMinimumValue(this.Value - 1L);
      if (newValue == this.Value && !this.HasDigits)
        return false;
      this.UpdateActiveEditor(newValue);
      return true;
    }

    private long CorrectMinimumValue(long val)
    {
      if (val < this.MinimumValue)
        val = this.MaximumValue;
      return val;
    }

    /// <summary>Gets the text.</summary>
    /// <returns></returns>
    public long GetTextValue()
    {
      if (this.Value >= this.MinimumValue && this.Value <= this.MaximumValue)
        return this.Value;
      return this.MinimumValue;
    }
  }
}
