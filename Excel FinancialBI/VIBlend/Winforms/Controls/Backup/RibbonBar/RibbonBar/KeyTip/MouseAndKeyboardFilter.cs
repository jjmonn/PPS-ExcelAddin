// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.MouseAndKeyboardFilter
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a MouseAndKeyboardFilter</summary>
  public class MouseAndKeyboardFilter : IMessageFilter
  {
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    private const int WM_SYSKEYDOWN = 260;
    private const int WM_SYSKEYUP = 261;
    public const int WM_LBUTTONDBLCLK = 515;
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_LBUTTONUP = 514;
    public const int WM_MBUTTONDBLCLK = 521;
    public const int WM_MBUTTONDOWN = 519;
    public const int WM_MBUTTONUP = 520;
    public const int WM_RBUTTONDBLCLK = 518;
    public const int WM_RBUTTONDOWN = 516;
    public const int WM_RBUTTONUP = 517;
    public const int WM_NCLBUTTONDOWN = 161;
    public const int WM_NCLBUTTONUP = 162;

    public event KeyEventHandler KeyDown;

    public event KeyEventHandler KeyUp;

    public event EventHandler MouseEventFired;

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      if (this.KeyDown == null)
        return;
      this.KeyDown((object) null, e);
    }

    protected virtual void OnMouseEventFired()
    {
      if (this.MouseEventFired == null)
        return;
      this.MouseEventFired((object) null, EventArgs.Empty);
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      if (this.KeyUp == null)
        return;
      this.KeyUp((object) null, e);
    }

    public bool PreFilterMessage(ref Message m)
    {
      if (m.Msg == 256 || m.Msg == 260)
        this.OnKeyDown(new KeyEventArgs((Keys) (int) m.WParam));
      else if (m.Msg == 261 || m.Msg == 257)
        this.OnKeyUp(new KeyEventArgs((Keys) (int) m.WParam));
      if (m.Msg == 515 || m.Msg == 514 || (m.Msg == 518 || m.Msg == 519) || (m.Msg == 517 || m.Msg == 516 || (m.Msg == 513 || m.Msg == 161)) || m.Msg == 162)
        this.OnMouseEventFired();
      return false;
    }
  }
}
