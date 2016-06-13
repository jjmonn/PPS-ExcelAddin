// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DisabledEditor
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  public class DisabledEditor : IDateTimeEditor
  {
    private string formatValue;
    private int basevalue;
    private DateTimeEditorFormatItem item;

    public string TextValue
    {
      get
      {
        return this.formatValue;
      }
    }

    public DisabledEditor(string formatValue, int baseValue, DateTimeEditorFormatItem item)
    {
      this.formatValue = formatValue;
      this.basevalue = baseValue;
      this.item = item;
    }

    /// <summary>Inserts the specified inserted.</summary>
    /// <param name="inserted">The inserted.</param>
    /// <returns></returns>
    public bool InsertString(string inserted)
    {
      return false;
    }

    /// <summary>Gets the text.</summary>
    /// <returns></returns>
    public long GetTextValue()
    {
      return -1;
    }

    /// <summary>Ups this instance.</summary>
    /// <returns></returns>
    public bool Up()
    {
      return false;
    }

    /// <summary>Downs this instance.</summary>
    /// <returns></returns>
    public bool Down()
    {
      return false;
    }

    /// <summary>Gets the item.</summary>
    /// <returns></returns>
    public DateTimeEditorFormatItem GetDateTimeItem()
    {
      return this.item;
    }
  }
}
