// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ValueChangedEditorEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  public class ValueChangedEditorEventArgs : EventArgs
  {
    private Decimal value;

    /// <summary>Gets the decimal value.</summary>
    public Decimal Value
    {
      get
      {
        return this.value;
      }
    }

    public ValueChangedEditorEventArgs(Decimal value)
    {
      this.value = value;
    }
  }
}
