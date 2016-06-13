// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ValueChangingEditorEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  public class ValueChangingEditorEventArgs : CancelEventArgs
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

    public ValueChangingEditorEventArgs(Decimal value)
    {
      this.value = value;
    }
  }
}
