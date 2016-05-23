// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.MENUINFO
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  internal struct MENUINFO
  {
    internal int cbSize;
    internal int fMask;
    internal int dwStyle;
    internal int cyMax;
    internal IntPtr hbrBack;
    internal int dwContextHelpID;
    internal int dwMenuData;

    internal MENUINFO(Control owner)
    {
      this.cbSize = Marshal.SizeOf(typeof (MENUINFO));
      this.fMask = 0;
      this.dwStyle = 0;
      this.cyMax = 0;
      this.hbrBack = IntPtr.Zero;
      this.dwContextHelpID = 0;
      this.dwMenuData = 0;
    }
  }
}
