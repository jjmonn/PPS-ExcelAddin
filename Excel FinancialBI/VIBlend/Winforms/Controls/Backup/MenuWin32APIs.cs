// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.MenuWin32APIs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Runtime.InteropServices;

namespace VIBlend.WinForms.Controls
{
  internal class MenuWin32APIs
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("User32.dll")]
    internal static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern int GetLastError();

    [DllImport("user32.dll")]
    internal static extern int ScreenToClient(IntPtr hwnd, POINT pt);

    [DllImport("user32.dll")]
    internal static extern int DrawMenuBar(IntPtr hwnd);

    [DllImport("user32.dll")]
    internal static extern int SetMenuInfo(IntPtr hmenu, ref MENUINFO mi);

    [DllImport("gdi32.dll")]
    internal static extern IntPtr CreatePatternBrush(IntPtr hbmp);

    [DllImport("gdi32.dll")]
    internal static extern bool DeleteObject(IntPtr hObject);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);
  }
}
