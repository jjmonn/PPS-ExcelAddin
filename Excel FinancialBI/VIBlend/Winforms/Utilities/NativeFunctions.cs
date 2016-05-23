// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.NativeFunctions
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class NativeFunctions
  {
    public const int S_OK = 0;
    public const int EP_EDITTEXT = 1;
    public const int ETS_DISABLED = 4;
    public const int ETS_NORMAL = 1;
    public const int ETS_READONLY = 6;
    public const int WM_THEMECHANGED = 794;
    public const int WM_NCPAINT = 133;
    public const int WM_NCCALCSIZE = 131;
    public const int WS_EX_CLIENTEDGE = 512;
    public const int WVR_HREDRAW = 256;
    public const int WVR_VREDRAW = 512;
    public const int WVR_REDRAW = 768;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);

    [DllImport("uxtheme")]
    public static extern int GetThemeBackgroundContentRect(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref NativeFunctions.RECT pBoundingRect, out NativeFunctions.RECT pContentRect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr OpenThemeData(IntPtr hWnd, string classList);

    [DllImport("uxtheme.dll")]
    public static extern int CloseThemeData(IntPtr hTheme);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr rectUpdate, IntPtr hRgnUpdate, uint uFlags);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out NativeFunctions.RECT lpRect);

    [DllImport("gdi32.dll")]
    public static extern int ExcludeClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool RedrawWindow(HandleRef hwnd, IntPtr rcUpdate, HandleRef hrgnUpdate, int flags);

    public struct NCCALCSIZE_PARAMS
    {
      public NativeFunctions.RECT rgrc0;
      public NativeFunctions.RECT rgrc1;
      public NativeFunctions.RECT rgrc2;
      public IntPtr lppos;
    }

    [Serializable]
    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;

      public int Height
      {
        get
        {
          return this.Bottom - this.Top + 1;
        }
      }

      public int Width
      {
        get
        {
          return this.Right - this.Left + 1;
        }
      }

      public Size Size
      {
        get
        {
          return new Size(this.Width, this.Height);
        }
      }

      public Point Location
      {
        get
        {
          return new Point(this.Left, this.Top);
        }
      }

      public RECT(int left_, int top_, int right_, int bottom_)
      {
        this.Left = left_;
        this.Top = top_;
        this.Right = right_;
        this.Bottom = bottom_;
      }

      public static implicit operator Rectangle(NativeFunctions.RECT rect)
      {
        return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
      }

      public static implicit operator NativeFunctions.RECT(Rectangle rect)
      {
        return new NativeFunctions.RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
      }

      public Rectangle ToRectangle()
      {
        return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
      }

      public static NativeFunctions.RECT FromRectangle(Rectangle rectangle)
      {
        return new NativeFunctions.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
      }

      public void Inflate(int width, int height)
      {
        this.Left -= width;
        this.Top -= height;
        this.Right += width;
        this.Bottom += height;
      }

      public override int GetHashCode()
      {
        return this.Left ^ (this.Top << 13 | this.Top >> 19) ^ (this.Width << 26 | this.Width >> 6) ^ (this.Height << 7 | this.Height >> 25);
      }
    }
  }
}
