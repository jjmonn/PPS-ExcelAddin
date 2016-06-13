// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ToolTipEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ToolTipEventArgs : EventArgs
  {
    private string toolTipText;
    private bool handled;

    /// <summary>Determines whether the event was handled</summary>
    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    /// <summary>Gets or sets the tooltip text</summary>
    public string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        this.toolTipText = value;
      }
    }
  }
}
