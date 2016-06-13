// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.SelectionEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  public class SelectionEventArgs : EventArgs
  {
    private DateTime oldDate;
    private DateTime newDate;

    /// <summary>Gets the old selected date.</summary>
    public DateTime OldDate
    {
      get
      {
        return this.oldDate;
      }
    }

    /// <summary>Gets the new selected date.</summary>
    public DateTime NewDate
    {
      get
      {
        return this.newDate;
      }
    }

    public SelectionEventArgs(DateTime oldDate, DateTime newDate)
    {
      this.oldDate = oldDate;
      this.newDate = newDate;
    }
  }
}
