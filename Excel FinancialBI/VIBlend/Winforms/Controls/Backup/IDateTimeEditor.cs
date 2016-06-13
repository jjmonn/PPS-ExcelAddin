// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.IDateTimeEditor
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  /// <summary>Base Class For All Editors</summary>
  public interface IDateTimeEditor
  {
    string TextValue { get; }

    DateTimeEditorFormatItem GetDateTimeItem();

    bool InsertString(string inserted);

    long GetTextValue();

    bool Up();

    bool Down();
  }
}
