// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.KeyTipEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class KeyTipEventArgs : EventArgs
  {
    private Control contentControl;

    public Control ContentControl
    {
      get
      {
        return this.contentControl;
      }
    }

    public KeyTipEventArgs(Control contentControl)
    {
      this.contentControl = contentControl;
    }
  }
}
