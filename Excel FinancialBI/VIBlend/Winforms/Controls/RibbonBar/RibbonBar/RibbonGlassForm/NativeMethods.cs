// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.WindowsAPI
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace VIBlend.WinForms.Controls
{
  public static class WindowsAPI
  {
    public const int WM_MOUSEFIRST = 512;
    public const int WM_MOUSEMOVE = 512;
    public const int WM_LBUTTONDOWN = 513;
    public const int WM_LBUTTONUP = 514;
    public const int WM_LBUTTONDBLCLK = 515;
    public const int WM_RBUTTONDOWN = 516;
    public const int WM_RBUTTONUP = 517;
    public const int WM_RBUTTONDBLCLK = 518;
    public const int WM_MBUTTONDOWN = 519;
    public const int WM_MBUTTONUP = 520;
    public const int WM_MBUTTONDBLCLK = 521;
    public const int WM_MOUSEWHEEL = 522;
    public const int WM_XBUTTONDOWN = 523;
    public const int WM_XBUTTONUP = 524;
    public const int WM_XBUTTONDBLCLK = 525;
    public const int WM_MOUSELAST = 525;
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    public const int WM_SYSKEYDOWN = 260;
    public const int WM_SYSKEYUP = 261;
    public const byte VK_SHIFT = 16;
    public const byte VK_CAPITAL = 20;
    public const byte VK_NUMLOCK = 144;
    private const int DTT_COMPOSITED = 8192;
    private const int DTT_GLOWSIZE = 2048;
    private const int DT_SINGLELINE = 32;
    private const int DT_CENTER = 1;
    private const int DT_VCENTER = 4;
    private const int DT_NOPREFIX = 2048;
    /// <summary>Enables the drop shadow effect on a window</summary>
    public const int CS_DROPSHADOW = 131072;
    /// <summary>
    /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
    /// </summary>
    public const int WH_MOUSE_LL = 14;
    /// <summary>
    /// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
    /// </summary>
    public const int WH_KEYBOARD_LL = 13;
    /// <summary>
    /// Installs a hook procedure that monitors mouse messages.
    /// </summary>
    public const int WH_MOUSE = 7;
    /// <summary>
    /// Installs a hook procedure that monitors keystroke messages.
    /// </summary>
    public const int WH_KEYBOARD = 2;
    /// <summary>
    /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window.
    /// </summary>
    public const int WM_NCLBUTTONUP = 162;
    /// <summary>
    /// The WM_SIZE message is sent to a window after its size has changed.
    /// </summary>
    public const int WM_SIZE = 5;
    /// <summary>
    /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized).
    /// </summary>
    public const int WM_ERASEBKGND = 20;
    /// <summary>
    /// The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated.
    /// </summary>
    public const int WM_NCCALCSIZE = 131;
    /// <summary>
    /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released.
    /// </summary>
    public const int WM_NCHITTEST = 132;
    /// <summary>
    /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window.
    /// </summary>
    public const int WM_NCMOUSEMOVE = 160;
    /// <summary>
    /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
    /// </summary>
    public const int WM_NCMOUSELEAVE = 674;
    /// <summary>An uncompressed format.</summary>
    public const int BI_RGB = 0;
    /// <summary>
    /// The BITMAPINFO structure contains an array of literal RGB values.
    /// </summary>
    public const int DIB_RGB_COLORS = 0;
    /// <summary>
    /// Copies the source rectangle directly to the destination rectangle.
    /// </summary>
    public const int SRCCOPY = 13369376;
    private static bool allowGlass;

    /// <summary>Gets if the current OS is Windows NT or later</summary>
    public static bool IsWindows
    {
      get
      {
        return Environment.OSVersion.Platform == PlatformID.Win32NT;
      }
    }

    /// <summary>
    /// Gets a value indicating if operating system is vista or higher
    /// </summary>
    public static bool IsVista
    {
      get
      {
        if (WindowsAPI.IsWindows)
          return Environment.OSVersion.Version.Major >= 6;
        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating if operating system is xp or higher
    /// </summary>
    public static bool IsXP
    {
      get
      {
        if (WindowsAPI.IsWindows)
          return Environment.OSVersion.Version.Major >= 5;
        return false;
      }
    }

    /// <summary>
    /// If set to true and the OS Glass effect is enabled, the form resembles the standard glass Form.
    /// However, if the property value is false, the Form renders the Form without the glass effect.
    /// </summary>
    [Description("If set to true and the OS Glass effect is enabled, the form resembles the standard glass Form.")]
    [Category("Appearance")]
    [Browsable(false)]
    public static bool AllowGlass
    {
      get
      {
        return WindowsAPI.allowGlass;
      }
      set
      {
        WindowsAPI.allowGlass = value;
      }
    }

    /// <summary>Gets if computer is glass capable and enabled</summary>
    public static bool IsGlassEnabled
    {
      get
      {
        if (!WindowsAPI.IsVista)
          return false;
        int pfEnabled = 0;
        WindowsAPI.DwmIsCompositionEnabled(ref pfEnabled);
        if (pfEnabled > 0)
          return WindowsAPI.AllowGlass;
        return false;
      }
    }

    [DllImport("user32")]
    internal static extern bool GetCursorPos(out WindowsAPI.POINT lpPoint);

    /// <summary>
    /// The ToAscii function translates the specified virtual-key code and keyboard state to the corresponding character or characters.
    /// </summary>
    /// <param name="uVirtKey"></param>
    /// <param name="uScanCode"></param>
    /// <param name="lpbKeyState"></param>
    /// <param name="lpwTransKey"></param>
    /// <param name="fuState"></param>
    /// <returns></returns>
    [DllImport("user32")]
    internal static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

    /// <summary>
    /// The GetKeyboardState function copies the status of the 256 virtual keys to the specified buffer.
    /// </summary>
    /// <param name="pbKeyState"></param>
    /// <returns></returns>
    [DllImport("user32")]
    internal static extern int GetKeyboardState(byte[] pbKeyState);

    /// <summary>
    /// This function retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled on or off — alternating each time the key is pressed.
    /// </summary>
    /// <param name="vKey"></param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    internal static extern short GetKeyState(int vKey);

    /// <summary>
    /// Installs an application-defined hook procedure into a hook chain
    /// </summary>
    /// <param name="idHook"></param>
    /// <param name="lpfn"></param>
    /// <param name="hInstance"></param>
    /// <param name="threadId"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    internal static extern int SetWindowsHookEx(int idHook, GlobalHook.HookProcCallBack lpfn, IntPtr hInstance, int threadId);

    /// <summary>
    /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
    /// </summary>
    /// <param name="idHook"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    internal static extern bool UnhookWindowsHookEx(int idHook);

    /// <summary>
    /// Passes the hook information to the next hook procedure in the current hook chain
    /// </summary>
    /// <param name="idHook"></param>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    internal static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// This function retrieves a handle to a display device context (DC) for the client area of the specified window.
    /// </summary>
    /// <param name="hdc"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    internal static extern IntPtr GetDC(IntPtr hdc);

    /// <summary>
    /// The SaveDC function saves the current state of the specified device context (DC) by copying data describing selected objects and graphic modes
    /// </summary>
    /// <param name="hdc"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    internal static extern int SaveDC(IntPtr hdc);

    /// <summary>
    /// This function releases a device context (DC), freeing it for use by other applications. The effect of ReleaseDC depends on the type of device context.
    /// </summary>
    /// <param name="hdc"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    internal static extern int ReleaseDC(IntPtr hdc, int state);

    /// <summary>
    /// Draws text using the color and font defined by the visual style. Extends DrawThemeText by allowing additional text format options.
    /// </summary>
    /// <param name="hTheme"></param>
    /// <param name="hdc"></param>
    /// <param name="iPartId"></param>
    /// <param name="iStateId"></param>
    /// <param name="text"></param>
    /// <param name="iCharCount"></param>
    /// <param name="dwFlags"></param>
    /// <param name="pRect"></param>
    /// <param name="pOptions"></param>
    /// <returns></returns>
    [DllImport("UxTheme.dll", CharSet = CharSet.Unicode)]
    private static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref WindowsAPI.RECT pRect, ref WindowsAPI.DTTOPTS pOptions);

    /// <summary>
    /// Draws text using the color and font defined by the visual style.
    /// </summary>
    /// <param name="hTheme"></param>
    /// <param name="hdc"></param>
    /// <param name="iPartId"></param>
    /// <param name="iStateId"></param>
    /// <param name="text"></param>
    /// <param name="iCharCount"></param>
    /// <param name="dwFlags1"></param>
    /// <param name="dwFlags2"></param>
    /// <param name="pRect"></param>
    /// <returns></returns>
    [DllImport("UxTheme.dll")]
    internal static extern int DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags1, int dwFlags2, ref WindowsAPI.RECT pRect);

    /// <summary>
    /// The CreateDIBSection function creates a DIB that applications can write to directly.
    /// </summary>
    /// <param name="hdc"></param>
    /// <param name="pbmi"></param>
    /// <param name="iUsage"></param>
    /// <param name="ppvBits"></param>
    /// <param name="hSection"></param>
    /// <param name="dwOffset"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateDIBSection(IntPtr hdc, ref WindowsAPI.BITMAPINFO pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

    /// <summary>
    /// This function transfers pixels from a specified source rectangle to a specified destination rectangle, altering the pixels according to the selected raster operation (ROP) code.
    /// </summary>
    /// <param name="hdc"></param>
    /// <param name="nXDest"></param>
    /// <param name="nYDest"></param>
    /// <param name="nWidth"></param>
    /// <param name="nHeight"></param>
    /// <param name="hdcSrc"></param>
    /// <param name="nXSrc"></param>
    /// <param name="nYSrc"></param>
    /// <param name="dwRop"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

    /// <summary>
    /// This function creates a memory device context (DC) compatible with the specified device.
    /// </summary>
    /// <param name="hDC"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    /// <summary>
    /// This function selects an object into a specified device context. The new object replaces the previous object of the same type.
    /// </summary>
    /// <param name="hDC"></param>
    /// <param name="hObject"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    /// <summary>
    /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
    /// </summary>
    /// <param name="hObject"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    internal static extern bool DeleteObject(IntPtr hObject);

    /// <summary>
    /// The DeleteDC function deletes the specified device context (DC).
    /// </summary>
    /// <param name="hdc"></param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    internal static extern bool DeleteDC(IntPtr hdc);

    /// <summary>Extends the window frame behind the client area.</summary>
    /// <param name="hdc"></param>
    /// <param name="marInset"></param>
    /// <returns></returns>
    [DllImport("dwmapi.dll")]
    internal static extern int DwmExtendFrameIntoClientArea(IntPtr hdc, ref WindowsAPI.MARGINS marInset);

    /// <summary>
    /// Default window procedure for Desktop Window Manager (DWM) hit-testing within the non-client area.
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="msg"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    [DllImport("dwmapi.dll")]
    internal static extern int DwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

    /// <summary>
    /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled.
    /// </summary>
    /// <param name="pfEnabled"></param>
    /// <returns></returns>
    [DllImport("dwmapi.dll")]
    internal static extern int DwmIsCompositionEnabled(ref int pfEnabled);

    /// <summary>Sends the specified message to a window or windows</summary>
    /// <param name="hWnd"></param>
    /// <param name="Msg"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    /// <summary>Equivalent to the HiWord C Macro</summary>
    /// <param name="dwValue"></param>
    /// <returns></returns>
    public static int HiWord(int dwValue)
    {
      return dwValue >> 16 & (int) ushort.MaxValue;
    }

    /// <summary>Equivalent to the LoWord C Macro</summary>
    /// <param name="dwValue"></param>
    /// <returns></returns>
    public static int LoWord(int dwValue)
    {
      return dwValue & (int) ushort.MaxValue;
    }

    /// <summary>Equivalent to the MakeLParam C Macro</summary>
    /// <param name="LoWord"></param>
    /// <param name="HiWord"></param>
    /// <returns></returns>
    public static IntPtr MakeLParam(int LoWord, int HiWord)
    {
      return new IntPtr(HiWord << 16 | LoWord & (int) ushort.MaxValue);
    }

    /// <summary>Fills an area for glass rendering</summary>
    /// <param name="gph"></param>
    /// <param name="rgn"></param>
    public static void FillForGlass(Graphics g, Rectangle r)
    {
      WindowsAPI.RECT rect = new WindowsAPI.RECT();
      rect.Left = r.Left;
      rect.Right = r.Right;
      rect.Top = r.Top;
      rect.Bottom = r.Bottom;
      IntPtr hdc = g.GetHdc();
      IntPtr compatibleDc = WindowsAPI.CreateCompatibleDC(hdc);
      IntPtr hObject = IntPtr.Zero;
      WindowsAPI.BITMAPINFO pbmi = new WindowsAPI.BITMAPINFO();
      pbmi.bmiHeader.biHeight = -(rect.Bottom - rect.Top);
      pbmi.bmiHeader.biWidth = rect.Right - rect.Left;
      pbmi.bmiHeader.biPlanes = (short) 1;
      pbmi.bmiHeader.biSize = Marshal.SizeOf(typeof (WindowsAPI.BITMAPINFOHEADER));
      pbmi.bmiHeader.biBitCount = (short) 32;
      pbmi.bmiHeader.biCompression = 0;
      if (WindowsAPI.SaveDC(compatibleDc) != 0)
      {
        IntPtr dibSection = WindowsAPI.CreateDIBSection(compatibleDc, ref pbmi, 0U, 0, IntPtr.Zero, 0U);
        if (!(dibSection == IntPtr.Zero))
        {
          hObject = WindowsAPI.SelectObject(compatibleDc, dibSection);
          WindowsAPI.BitBlt(hdc, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, compatibleDc, 0, 0, 13369376U);
        }
        WindowsAPI.SelectObject(compatibleDc, hObject);
        WindowsAPI.DeleteObject(dibSection);
        WindowsAPI.ReleaseDC(compatibleDc, -1);
        WindowsAPI.DeleteDC(compatibleDc);
      }
      g.ReleaseHdc();
    }

    /// <summary>Draws theme text on glass.</summary>
    /// <param name="hwnd"></param>
    /// <param name="text"></param>
    /// <param name="font"></param>
    /// <param name="ctlrct"></param>
    /// <param name="iglowSize"></param>
    /// <remarks>This method is courtesy of 版权所有 (I hope the name's right)</remarks>
    public static void DrawTextOnGlass(IntPtr hwnd, string text, Font font, Rectangle ctlrct, int iglowSize)
    {
      if (!WindowsAPI.IsGlassEnabled)
        return;
      WindowsAPI.RECT rect = new WindowsAPI.RECT();
      WindowsAPI.RECT pRect = new WindowsAPI.RECT();
      rect.Left = ctlrct.Left;
      rect.Right = ctlrct.Right;
      rect.Top = ctlrct.Top;
      rect.Bottom = ctlrct.Bottom;
      pRect.Left = 0;
      pRect.Top = 0;
      pRect.Right = rect.Right - rect.Left;
      pRect.Bottom = rect.Bottom - rect.Top;
      IntPtr dc = WindowsAPI.GetDC(hwnd);
      IntPtr compatibleDc = WindowsAPI.CreateCompatibleDC(dc);
      IntPtr num = IntPtr.Zero;
      int dwFlags = 2085;
      WindowsAPI.BITMAPINFO pbmi = new WindowsAPI.BITMAPINFO();
      pbmi.bmiHeader.biHeight = -(rect.Bottom - rect.Top);
      pbmi.bmiHeader.biWidth = rect.Right - rect.Left;
      pbmi.bmiHeader.biPlanes = (short) 1;
      pbmi.bmiHeader.biSize = Marshal.SizeOf(typeof (WindowsAPI.BITMAPINFOHEADER));
      pbmi.bmiHeader.biBitCount = (short) 32;
      pbmi.bmiHeader.biCompression = 0;
      if (WindowsAPI.SaveDC(compatibleDc) == 0)
        return;
      IntPtr dibSection = WindowsAPI.CreateDIBSection(compatibleDc, ref pbmi, 0U, 0, IntPtr.Zero, 0U);
      if (dibSection == IntPtr.Zero)
        return;
      IntPtr hObject1 = WindowsAPI.SelectObject(compatibleDc, dibSection);
      IntPtr hfont = font.ToHfont();
      IntPtr hObject2 = WindowsAPI.SelectObject(compatibleDc, hfont);
      try
      {
        WindowsAPI.DTTOPTS l_truc = new WindowsAPI.DTTOPTS()
        {
          dwSize = (uint) Marshal.SizeOf(typeof (WindowsAPI.DTTOPTS)),
          dwFlags = 10240U,
          iGlowSize = iglowSize
        };
        WindowsAPI.DrawThemeTextEx(new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active).Handle, compatibleDc, 0, 0, text, -1, dwFlags, ref pRect, ref l_truc);
        WindowsAPI.BitBlt(dc, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, compatibleDc, 0, 0, 13369376U);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
      WindowsAPI.SelectObject(compatibleDc, hObject1);
      WindowsAPI.SelectObject(compatibleDc, hObject2);
      WindowsAPI.DeleteObject(dibSection);
      WindowsAPI.DeleteObject(hfont);
      WindowsAPI.ReleaseDC(compatibleDc, -1);
      WindowsAPI.DeleteDC(compatibleDc);
    }

    /// <summary>
    /// Contains information about a mouse event passed to a WH_MOUSE hook procedure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class MouseLLHookStruct
    {
      public WindowsAPI.POINT pt;
      public int mouseData;
      public int flags;
      public int time;
      public int extraInfo;
    }

    /// <summary>
    /// Contains information about a low-level keyboard input event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class KeyboardLLHookStruct
    {
      public int vkCode;
      public int scanCode;
      public int flags;
      public int time;
      public int dwExtraInfo;
    }

    /// <summary>
    /// Contains information about a mouse event passed to a WH_MOUSE hook procedure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class MouseHookStruct
    {
      public WindowsAPI.POINT pt;
      public int hwnd;
      public int wHitTestCode;
      public int dwExtraInfo;
    }

    /// <summary>Represents a point</summary>
    internal struct POINT
    {
      public int x;
      public int y;
    }

    /// <summary>Defines the options for the DrawThemeTextEx function.</summary>
    internal struct DTTOPTS
    {
      public uint dwSize;
      public uint dwFlags;
      public uint crText;
      public uint crBorder;
      public uint crShadow;
      public int iTextShadowType;
      public WindowsAPI.POINT ptShadowOffset;
      public int iBorderSize;
      public int iFontPropId;
      public int iColorPropId;
      public int iStateId;
      public int fApplyOverlay;
      public int iGlowSize;
      public IntPtr pfnDrawTextCallback;
      public int lParam;
    }

    /// <summary>
    /// This structure describes a color consisting of relative intensities of red, green, and blue.
    /// </summary>
    private struct RGBQUAD
    {
      public byte rgbBlue;
      public byte rgbGreen;
      public byte rgbRed;
      public byte rgbReserved;
    }

    /// <summary>
    /// This structure contains information about the dimensions and color format of a device-independent bitmap (DIB).
    /// </summary>
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

    /// <summary>
    /// This structure defines the dimensions and color information of a Windows-based device-independent bitmap (DIB).
    /// </summary>
    private struct BITMAPINFO
    {
      public WindowsAPI.BITMAPINFOHEADER bmiHeader;
      public WindowsAPI.RGBQUAD bmiColors;
    }

    /// <summary>
    /// Describes the width, height, and location of a rectangle.
    /// </summary>
    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    /// <summary>
    /// The NCCALCSIZE_PARAMS structure contains information that an application can use
    /// while processing the WM_NCCALCSIZE message to calculate the size, position, and
    /// valid contents of the client area of a window.
    /// </summary>
    public struct NCCALCSIZE_PARAMS
    {
      public WindowsAPI.RECT rect0;
      public WindowsAPI.RECT rect1;
      public WindowsAPI.RECT rect2;
      public IntPtr lppos;
    }

    /// <summary>Used to specify margins of a window</summary>
    internal struct MARGINS
    {
      public int cxLeftWidth;
      public int cxRightWidth;
      public int cyTopHeight;
      public int cyBottomHeight;

      public MARGINS(int Left, int Right, int Top, int Bottom)
      {
        this.cxLeftWidth = Left;
        this.cxRightWidth = Right;
        this.cyTopHeight = Top;
        this.cyBottomHeight = Bottom;
      }
    }
  }
}
