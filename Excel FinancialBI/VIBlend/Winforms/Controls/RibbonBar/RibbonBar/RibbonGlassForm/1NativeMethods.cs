// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.GlobalHook
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class GlobalHook : IDisposable
  {
    private GlobalHook.HookProcCallBack _HookProc;
    private int _hHook;
    private GlobalHook.HookTypes _hookType;

    /// <summary>Gets the type of this hook</summary>
    public GlobalHook.HookTypes HookType
    {
      get
      {
        return this._hookType;
      }
    }

    /// <summary>Gets the handle of the hook</summary>
    public int Handle
    {
      get
      {
        return this._hHook;
      }
    }

    /// <summary>Occours when the hook captures a mouse click</summary>
    public event MouseEventHandler MouseClick;

    /// <summary>Occours when the hook captures a mouse double click</summary>
    public event MouseEventHandler MouseDoubleClick;

    /// <summary>Occours when the hook captures the mouse wheel</summary>
    public event MouseEventHandler MouseWheel;

    /// <summary>
    /// Occours when the hook captures the press of a mouse button
    /// </summary>
    public event MouseEventHandler MouseDown;

    /// <summary>
    /// Occours when the hook captures the release of a mouse button
    /// </summary>
    public event MouseEventHandler MouseUp;

    /// <summary>
    /// Occours when the hook captures the mouse moving over the screen
    /// </summary>
    public event MouseEventHandler MouseMove;

    /// <summary>Occours when a key is pressed</summary>
    public event KeyEventHandler KeyDown;

    /// <summary>Occours when a key is released</summary>
    public event KeyEventHandler KeyUp;

    /// <summary>Occours when a key is pressed</summary>
    public event KeyPressEventHandler KeyPress;

    /// <summary>Creates a new Hook of the specified type</summary>
    /// <param name="hookType"></param>
    public GlobalHook(GlobalHook.HookTypes hookType)
    {
      this._hookType = hookType;
      this.InstallHook();
    }

    ~GlobalHook()
    {
      if (this.Handle == 0)
        return;
      this.Unhook();
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.MouseClick" /> event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMouseClick(MouseEventArgs e)
    {
      if (this.MouseClick == null)
        return;
      this.MouseClick((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.MouseDoubleClick" /> event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.MouseDoubleClick == null)
        return;
      this.MouseDoubleClick((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.MouseWheel" /> event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMouseWheel(MouseEventArgs e)
    {
      if (this.MouseWheel == null)
        return;
      this.MouseWheel((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.MouseDown" /> event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMouseDown(MouseEventArgs e)
    {
      if (this.MouseDown == null)
        return;
      this.MouseDown((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.MouseUp" /> event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMouseUp(MouseEventArgs e)
    {
      if (this.MouseUp == null)
        return;
      this.MouseUp((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.MouseMove" /> event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnMouseMove(MouseEventArgs e)
    {
      if (this.MouseMove == null)
        return;
      this.MouseMove((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.KeyDown" /> event
    /// </summary>
    /// <param name="e">Event Data</param>
    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      if (this.KeyDown == null)
        return;
      this.KeyDown((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.KeyUp" /> event
    /// </summary>
    /// <param name="e">Event Data</param>
    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      if (this.KeyUp == null)
        return;
      this.KeyUp((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VIBlend.WinForms.Controls.GlobalHook.KeyPress" /> event
    /// </summary>
    /// <param name="e">Event Data</param>
    protected virtual void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.KeyPress == null)
        return;
      this.KeyPress((object) this, e);
    }

    /// <summary>Recieves the actual unsafe mouse hook procedure</summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    private int HookProc(int code, IntPtr wParam, IntPtr lParam)
    {
      if (code < 0)
        return WindowsAPI.CallNextHookEx(this.Handle, code, wParam, lParam);
      switch (this.HookType)
      {
        case GlobalHook.HookTypes.Mouse:
          return this.MouseProc(code, wParam, lParam);
        case GlobalHook.HookTypes.Keyboard:
          return this.KeyboardProc(code, wParam, lParam);
        default:
          throw new Exception("HookType not supported");
      }
    }

    /// <summary>Recieves the actual unsafe keyboard hook procedure</summary>
    /// <param name="code"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    private int KeyboardProc(int code, IntPtr wParam, IntPtr lParam)
    {
      WindowsAPI.KeyboardLLHookStruct keyboardLlHookStruct = (WindowsAPI.KeyboardLLHookStruct) Marshal.PtrToStructure(lParam, typeof (WindowsAPI.KeyboardLLHookStruct));
      int int32 = wParam.ToInt32();
      bool flag1 = false;
      if (int32 == 256 || int32 == 260)
      {
        KeyEventArgs e = new KeyEventArgs((Keys) keyboardLlHookStruct.vkCode);
        this.OnKeyDown(e);
        flag1 = e.Handled;
      }
      else if (int32 == 257 || int32 == 261)
      {
        KeyEventArgs e = new KeyEventArgs((Keys) keyboardLlHookStruct.vkCode);
        this.OnKeyUp(e);
        flag1 = e.Handled;
      }
      if (int32 == 256 && this.KeyPress != null)
      {
        byte[] numArray = new byte[256];
        byte[] lpwTransKey = new byte[2];
        WindowsAPI.GetKeyboardState(numArray);
        switch (WindowsAPI.ToAscii(keyboardLlHookStruct.vkCode, keyboardLlHookStruct.scanCode, numArray, lpwTransKey, keyboardLlHookStruct.flags))
        {
          case 1:
          case 2:
            bool flag2 = ((int) WindowsAPI.GetKeyState(16) & 128) == 128;
            bool flag3 = (int) WindowsAPI.GetKeyState(20) != 0;
            char ch = (char) lpwTransKey[0];
            if (flag2 ^ flag3 && char.IsLetter(ch))
              ch = char.ToUpper(ch);
            KeyPressEventArgs e = new KeyPressEventArgs(ch);
            this.OnKeyPress(e);
            flag1 |= e.Handled;
            break;
        }
      }
      if (!flag1)
        return WindowsAPI.CallNextHookEx(this.Handle, code, wParam, lParam);
      return 1;
    }

    /// <summary>Processes Mouse Procedures</summary>
    /// <param name="code"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    private int MouseProc(int code, IntPtr wParam, IntPtr lParam)
    {
      WindowsAPI.MouseLLHookStruct mouseLlHookStruct = (WindowsAPI.MouseLLHookStruct) Marshal.PtrToStructure(lParam, typeof (WindowsAPI.MouseLLHookStruct));
      int int32 = wParam.ToInt32();
      int x = mouseLlHookStruct.pt.x;
      int y = mouseLlHookStruct.pt.y;
      int delta = (int) (short) (mouseLlHookStruct.mouseData >> 16 & (int) ushort.MaxValue);
      if (int32 == 522)
        this.OnMouseWheel(new MouseEventArgs(MouseButtons.None, 0, x, y, delta));
      else if (int32 == 512)
        this.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, x, y, delta));
      else if (int32 == 515)
        this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 0, x, y, delta));
      else if (int32 == 513)
        this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 0, x, y, delta));
      else if (int32 == 514)
      {
        this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 0, x, y, delta));
        this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 0, x, y, delta));
      }
      else if (int32 == 521)
        this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Middle, 0, x, y, delta));
      else if (int32 == 519)
        this.OnMouseDown(new MouseEventArgs(MouseButtons.Middle, 0, x, y, delta));
      else if (int32 == 520)
        this.OnMouseUp(new MouseEventArgs(MouseButtons.Middle, 0, x, y, delta));
      else if (int32 == 518)
        this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Right, 0, x, y, delta));
      else if (int32 == 516)
        this.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 0, x, y, delta));
      else if (int32 == 517)
        this.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 0, x, y, delta));
      else if (int32 == 525)
        this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.XButton1, 0, x, y, delta));
      else if (int32 == 523)
        this.OnMouseDown(new MouseEventArgs(MouseButtons.XButton1, 0, x, y, delta));
      else if (int32 == 524)
        this.OnMouseUp(new MouseEventArgs(MouseButtons.XButton1, 0, x, y, delta));
      return WindowsAPI.CallNextHookEx(this.Handle, code, wParam, lParam);
    }

    /// <summary>Installs the actual unsafe hook</summary>
    private void InstallHook()
    {
      if (this.Handle != 0)
        throw new Exception("Hook is already installed");
      int idHook;
      switch (this.HookType)
      {
        case GlobalHook.HookTypes.Mouse:
          idHook = 14;
          break;
        case GlobalHook.HookTypes.Keyboard:
          idHook = 13;
          break;
        default:
          throw new Exception("HookType is not supported");
      }
      this._HookProc = new GlobalHook.HookProcCallBack(this.HookProc);
      this._hHook = WindowsAPI.SetWindowsHookEx(idHook, this._HookProc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
      if (this.Handle == 0)
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    /// <summary>Unhooks the hook</summary>
    private void Unhook()
    {
      if (this.Handle == 0)
        return;
      if (!WindowsAPI.UnhookWindowsHookEx(this.Handle))
        throw new Win32Exception(Marshal.GetLastWin32Error());
      this._hHook = 0;
    }

    public void Dispose()
    {
      if (this.Handle == 0)
        return;
      this.Unhook();
    }

    /// <summary>Types of available hooks</summary>
    public enum HookTypes
    {
      Mouse,
      Keyboard,
    }

    /// <summary>Delegate used to recieve HookProc</summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    internal delegate int HookProcCallBack(int nCode, IntPtr wParam, IntPtr lParam);
  }
}
