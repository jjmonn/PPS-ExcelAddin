// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RibbonTextRenderer
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace VIBlend.WinForms.Controls
{
  internal class RibbonTextRenderer
  {
    private const int DTT_COMPOSITED = 8192;
    private const int DTT_GLOWSIZE = 2048;
    private const int DT_SINGLELINE = 32;
    private const int DT_CENTER = 1;
    private const int DT_VCENTER = 4;
    private const int DT_NOPREFIX = 2048;
    private const int SRCCOPY = 13369376;
    private const int BI_RGB = 0;
    private const int DIB_RGB_COLORS = 0;

    [DllImport("dwmapi.dll")]
    private static extern void DwmIsCompositionEnabled(ref int enabledptr);

    [DllImport("dwmapi.dll")]
    private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref RibbonTextRenderer.MARGINS margin);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetDC(IntPtr hdc);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern int SaveDC(IntPtr hdc);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int ReleaseDC(IntPtr hdc, int state);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

    [DllImport("UxTheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RibbonTextRenderer.RECT pRect, ref RibbonTextRenderer.DTTOPTS pOptions);

    [DllImport("UxTheme.dll", SetLastError = true)]
    private static extern int DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags1, int dwFlags2, ref RibbonTextRenderer.RECT pRect);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern IntPtr CreateDIBSection(IntPtr hdc, ref RibbonTextRenderer.BITMAPINFO pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

    private bool IsCompositionEnabled()
    {
      if (Environment.OSVersion.Version.Major < 6)
        return false;
      int enabledptr = 0;
      RibbonTextRenderer.DwmIsCompositionEnabled(ref enabledptr);
      return enabledptr > 0;
    }

    public void FillBlackRegion(Graphics gph, Rectangle rgn)
    {
      RibbonTextRenderer.RECT rect = new RibbonTextRenderer.RECT();
      rect.left = rgn.Left;
      rect.right = rgn.Right;
      rect.top = rgn.Top;
      rect.bottom = rgn.Bottom;
      IntPtr hdc = gph.GetHdc();
      IntPtr compatibleDc = RibbonTextRenderer.CreateCompatibleDC(hdc);
      IntPtr hObject = IntPtr.Zero;
      RibbonTextRenderer.BITMAPINFO pbmi = new RibbonTextRenderer.BITMAPINFO();
      pbmi.bmiHeader.biHeight = -(rect.bottom - rect.top);
      pbmi.bmiHeader.biWidth = rect.right - rect.left;
      pbmi.bmiHeader.biPlanes = (short) 1;
      pbmi.bmiHeader.biSize = Marshal.SizeOf(typeof (RibbonTextRenderer.BITMAPINFOHEADER));
      pbmi.bmiHeader.biBitCount = (short) 32;
      pbmi.bmiHeader.biCompression = 0;
      if (RibbonTextRenderer.SaveDC(compatibleDc) != 0)
      {
        IntPtr dibSection = RibbonTextRenderer.CreateDIBSection(compatibleDc, ref pbmi, 0U, 0, IntPtr.Zero, 0U);
        if (!(dibSection == IntPtr.Zero))
        {
          hObject = RibbonTextRenderer.SelectObject(compatibleDc, dibSection);
          RibbonTextRenderer.BitBlt(hdc, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, compatibleDc, 0, 0, 13369376U);
        }
        RibbonTextRenderer.SelectObject(compatibleDc, hObject);
        RibbonTextRenderer.DeleteObject(dibSection);
        RibbonTextRenderer.ReleaseDC(compatibleDc, -1);
        RibbonTextRenderer.DeleteDC(compatibleDc);
      }
      gph.ReleaseHdc();
    }

    public void DrawTextOnGlass(IntPtr hwnd, string text, Font font, Rectangle ctlrct, int iglowSize)
    {
      if (!this.IsCompositionEnabled())
        return;
      RibbonTextRenderer.RECT rect = new RibbonTextRenderer.RECT();
      RibbonTextRenderer.RECT pRect = new RibbonTextRenderer.RECT();
      rect.left = ctlrct.Left;
      rect.right = ctlrct.Right + 2 * iglowSize;
      rect.top = ctlrct.Top;
      rect.bottom = ctlrct.Bottom + 2 * iglowSize;
      pRect.left = 0;
      pRect.top = 0;
      pRect.right = rect.right - rect.left;
      pRect.bottom = rect.bottom - rect.top;
      IntPtr dc = RibbonTextRenderer.GetDC(hwnd);
      IntPtr compatibleDc = RibbonTextRenderer.CreateCompatibleDC(dc);
      IntPtr num = IntPtr.Zero;
      int dwFlags = 2085;
      RibbonTextRenderer.BITMAPINFO pbmi = new RibbonTextRenderer.BITMAPINFO();
      pbmi.bmiHeader.biHeight = -(rect.bottom - rect.top);
      pbmi.bmiHeader.biWidth = rect.right - rect.left;
      pbmi.bmiHeader.biPlanes = (short) 1;
      pbmi.bmiHeader.biSize = Marshal.SizeOf(typeof (RibbonTextRenderer.BITMAPINFOHEADER));
      pbmi.bmiHeader.biBitCount = (short) 32;
      pbmi.bmiHeader.biCompression = 0;
      if (RibbonTextRenderer.SaveDC(compatibleDc) == 0)
        return;
      IntPtr dibSection = RibbonTextRenderer.CreateDIBSection(compatibleDc, ref pbmi, 0U, 0, IntPtr.Zero, 0U);
      if (dibSection == IntPtr.Zero)
        return;
      IntPtr hObject1 = RibbonTextRenderer.SelectObject(compatibleDc, dibSection);
      IntPtr hfont = font.ToHfont();
      IntPtr hObject2 = RibbonTextRenderer.SelectObject(compatibleDc, hfont);
      try
      {
        RibbonTextRenderer.DTTOPTS l_truc = new RibbonTextRenderer.DTTOPTS()
                {
                  dwSize = (uint) Marshal.SizeOf(typeof (RibbonTextRenderer.DTTOPTS)),
                  dwFlags = 10240U,
                  iGlowSize = iglowSize
                };
        RibbonTextRenderer.DrawThemeTextEx(new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active).Handle, compatibleDc, 0, 0, text, -1, dwFlags, ref pRect, ref l_truc);

        RibbonTextRenderer.BitBlt(dc, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, compatibleDc, 0, 0, 13369376U);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
      RibbonTextRenderer.SelectObject(compatibleDc, hObject1);
      RibbonTextRenderer.SelectObject(compatibleDc, hObject2);
      RibbonTextRenderer.DeleteObject(dibSection);
      RibbonTextRenderer.DeleteObject(hfont);
      RibbonTextRenderer.ReleaseDC(compatibleDc, -1);
      RibbonTextRenderer.DeleteDC(compatibleDc);
    }

    private struct MARGINS
    {
      public int m_Left;
      public int m_Right;
      public int m_Top;
      public int m_Buttom;
    }

    private struct POINTAPI
    {
      public int x;
      public int y;
    }

    private struct DTTOPTS
    {
      public uint dwSize;
      public uint dwFlags;
      public uint crText;
      public uint crBorder;
      public uint crShadow;
      public int iTextShadowType;
      public RibbonTextRenderer.POINTAPI ptShadowOffset;
      public int iBorderSize;
      public int iFontPropId;
      public int iColorPropId;
      public int iStateId;
      public int fApplyOverlay;
      public int iGlowSize;
      public IntPtr pfnDrawTextCallback;
      public int lParam;
    }

    private struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    private struct BITMAPINFOHEADER
    {
      public int biSize;
      public int biWidth;
      public int biHeight;
      public short biPlanes;
      public short biBitCount;
      public int biCompression;
      public int biSizeImage;
      public int biXPelsPerMeter;
      public int biYPelsPerMeter;
      public int biClrUsed;
      public int biClrImportant;
    }

    private struct RGBQUAD
    {
      public byte rgbBlue;
      public byte rgbGreen;
      public byte rgbRed;
      public byte rgbReserved;
    }

    private struct BITMAPINFO
    {
      public RibbonTextRenderer.BITMAPINFOHEADER bmiHeader;
      public RibbonTextRenderer.RGBQUAD bmiColors;
    }
  }
}
