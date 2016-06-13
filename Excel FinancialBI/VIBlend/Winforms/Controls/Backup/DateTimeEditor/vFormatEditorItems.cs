// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.EditorAmPmItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  public class EditorAmPmItem : IDateTimeEditor
  {
    protected string amString;
    protected string pmString;
    private int value;
    private string format;
    private DateTimeEditorFormatItem item;

    /// <summary>Gets the text.</summary>
    /// <value>The text.</value>
    public string TextValue
    {
      get
      {
        string str = this.amString;
        if (this.value != 0)
          str = this.pmString;
        if (this.format.Length == 1 && str.Length > 1)
          str = str.Substring(0, 1);
        return str;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.EditorAmPmItem" /> class.
    /// </summary>
    /// <param name="formatValue">The format value.</param>
    /// <param name="baseValue">The base value.</param>
    /// <param name="am">The am.</param>
    /// <param name="pm">The pm.</param>
    /// <param name="item">The item.</param>
    public EditorAmPmItem(string formatValue, int baseValue, string am, string pm, DateTimeEditorFormatItem item)
    {
      this.InitializeEditor(formatValue, baseValue, am, pm, item);
    }

    /// <summary>Inserts the specified inserted.</summary>
    /// <param name="inserted">The inserted.</param>
    /// <returns></returns>
    public bool InsertString(string inserted)
    {
      if (inserted.Length == 0)
        return this.Delete();
      bool res = false;
      return this.GetInsertionResult(inserted, res);
    }

    private bool GetInsertionResult(string inserted, bool res)
    {
      if (this.amString.Length > 0 && this.pmString.Length > 0)
      {
        char amChar = this.amString[0];
        char newChar = inserted[0];
        char pmChar = this.pmString[0];
        res = this.InitValueWithFullFormat(res, amChar, newChar, pmChar);
      }
      else if (this.pmString.Length > 0)
      {
        this.value = 1;
        res = true;
      }
      else if (this.amString.Length > 0)
      {
        this.value = 0;
        res = true;
      }
      return res;
    }

    private bool InitValueWithFullFormat(bool res, char amChar, char newChar, char pmChar)
    {
      if (amChar.ToString().ToLower() == newChar.ToString().ToLower())
      {
        this.value = 0;
        res = true;
      }
      else if (pmChar.ToString().ToLower() == newChar.ToString().ToLower())
      {
        this.value = 1;
        res = true;
      }
      return res;
    }

    /// <summary>Deletes this instance.</summary>
    /// <returns></returns>
    public bool Delete()
    {
      bool flag = true;
      if (this.amString.Length == 0 && this.pmString.Length != 0)
      {
        if (this.value == 0)
          return false;
        this.value = 0;
      }
      else
      {
        if (this.value == 1)
          return false;
        this.value = 1;
      }
      return flag;
    }

    /// <summary>Ups this instance.</summary>
    /// <returns></returns>
    public bool Up()
    {
      this.value = 1 - this.value;
      return true;
    }

    /// <summary>Downs this instance.</summary>
    /// <returns></returns>
    public bool Down()
    {
      return this.Up();
    }

    /// <summary>Gets the text.</summary>
    /// <returns></returns>
    public long GetTextValue()
    {
      return (long) this.value;
    }

    private void InitializeEditor(string formatValue, int baseValue, string am, string pm, DateTimeEditorFormatItem item)
    {
      this.InitializeFields(formatValue, baseValue, am, pm);
      this.SetStrings();
      this.item = item;
    }

    private void SetStrings()
    {
      if (!(this.amString == this.pmString))
        return;
      this.amString = "<" + this.amString;
      this.pmString = ">" + this.pmString;
    }

    private void InitializeFields(string formatValue, int baseValue, string am, string pm)
    {
      this.value = baseValue;
      this.format = formatValue;
      this.amString = am;
      this.pmString = pm;
    }

    /// <summary>Gets the item.</summary>
    /// <returns></returns>
    public DateTimeEditorFormatItem GetDateTimeItem()
    {
      return this.item;
    }
  }
}
