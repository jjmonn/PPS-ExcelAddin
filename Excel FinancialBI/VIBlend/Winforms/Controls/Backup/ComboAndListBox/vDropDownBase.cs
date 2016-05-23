// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vDropDownBase
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vDropDownBase control</summary>
  /// <remarks>
  /// vDropDownBase is a base class for several drop-down controls
  /// </remarks>
  [ToolboxItem(false)]
  public class vDropDownBase : Control, IMessageFilter
  {
    internal IntPtr activeHwnd = IntPtr.Zero;
    private Rectangle backupBounds = Rectangle.Empty;
    private Size maximum = Size.Empty;
    private Size minimum = new Size(10, 10);
    private Size preferredSize = new Size(200, 200);
    private HandleRef HWND_TOP = new HandleRef((object) null, IntPtr.Zero);
    private HandleRef HWND_TOPMOST = new HandleRef((object) null, new IntPtr(-1));
    private bool sendWin32Messages;
    private bool canCapture;

    /// <summary>
    /// Gets or sets the preferred size of the drop down control
    /// </summary>
    public new Size PreferredSize
    {
      get
      {
        return this.preferredSize;
      }
      set
      {
        this.preferredSize = value;
        this.Size = this.preferredSize;
      }
    }

    /// <summary>
    /// Gets a CreateParams on the base class when creating a window.
    /// </summary>
    protected override CreateParams CreateParams
    {
      get
      {
        return this.GetNewParams();
      }
    }

    public event EventHandler DropDownClosing;

    /// <summary>Occurs when the Dropdown closes</summary>
    public event EventHandler DropDownClose;

    /// <summary>Occurs when the Dropdown opens</summary>
    public event EventHandler DropDownOpen;

    /// <summary>Constructor</summary>
    public vDropDownBase()
    {
      this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.MouseWheel += new MouseEventHandler(this.PopUp_MouseWheel);
      this.SizeChanged += new EventHandler(this.vDropDownBase_SizeChanged);
      this.Visible = false;
    }

    private void vDropDownBase_SizeChanged(object sender, EventArgs e)
    {
      this.preferredSize = this.Size;
    }

    private void PopUp_MouseWheel(object sender, MouseEventArgs e)
    {
      if (!(this.Controls[0] is vListBox))
        return;
      if (e.Delta > 0)
      {
        vListBox vListBox = this.Controls[0] as vListBox;
        vListBox.vscroll.Value -= vListBox.ItemHeight;
      }
      else
      {
        vListBox vListBox = this.Controls[0] as vListBox;
        vListBox.vscroll.Value += vListBox.ItemHeight;
      }
    }

    /// <summary>Updates the client ractangle for the control</summary>
    protected void RefreshVisibleArea()
    {
      this.minimum = new Size(this.Width, 10);
      this.maximum = this.Size;
    }

    /// <summary>Opens the DropDown</summary>
    /// <param name="direction">Determines whether to open the DropDown in a up or down direction.</param>
    /// <param name="point">Locaion to show the DropDown</param>
    public void OpenControl(ArrowDirection direction, Point point)
    {
      this.Size = this.PreferredSize;
      this.Location = point;
      this.RefreshVisibleArea();
      this.Visible = true;
    }

    internal ArrowDirection GetShowDirection(ArrowDirection direction, Point location)
    {
      Rectangle screenRect;
      Rectangle popupRect;
      location = this.CalculateShowLocation(direction, location, out screenRect, out popupRect);
      vDropDownBase.GetFinalShowDirection(popupRect, screenRect, ref direction);
      return direction;
    }

    public Point CalculateShowLocation(ArrowDirection direction, Point location, out Rectangle screenRect, out Rectangle popupRect)
    {
      screenRect = this != null ? Screen.GetWorkingArea((Control) this) : Screen.PrimaryScreen.WorkingArea;
      popupRect = new Rectangle(location, this.Size);
      switch (direction)
      {
        case ArrowDirection.Left:
          location = new Point(popupRect.Left - this.Size.Width, popupRect.Top);
          break;
        case ArrowDirection.Up:
          location = new Point(popupRect.Left, popupRect.Top - this.Size.Height);
          break;
      }
      return location;
    }

    /// <summary>Closes the DropDown</summary>
    public void CloseControl()
    {
      if (!this.Visible)
        return;
      this.Visible = false;
    }

    /// <exclude />
    protected override void WndProc(ref Message m)
    {
      int msg = m.Msg;
      if (msg <= 28)
      {
        switch (msg)
        {
          case 6:
            m.Result = IntPtr.Zero;
            return;
          case 8:
            m.Result = new IntPtr(1);
            break;
          case 28:
            this.CloseControl();
            break;
        }
      }
      else
      {
        if (msg == 134)
        {
          this.HandleNCActivate(ref m);
          return;
        }
        if (msg != 513 && msg == 514)
        {
          base.WndProc(ref m);
          if (this.canCapture)
            return;
          this.Capture = true;
          return;
        }
      }
      base.WndProc(ref m);
    }

    private void HandleNCActivate(ref Message m)
    {
      if (m.WParam != IntPtr.Zero)
      {
        if (!this.sendWin32Messages)
        {
          this.sendWin32Messages = true;
          vDropDownBase.SendMessage(new HandleRef((object) this, this.activeHwnd), 134, (IntPtr) 1, IntPtr.Zero);
          vDropDownBase.RedrawWindow(new HandleRef((object) this, this.activeHwnd), IntPtr.Zero, new HandleRef((object) this, IntPtr.Zero), 1025);
          m.WParam = (IntPtr) 1;
          this.sendWin32Messages = false;
        }
        this.DefWndProc(ref m);
      }
      else
        base.WndProc(ref m);
    }

    /// <summary>Sets the control to the specified visible state.</summary>
    /// <param name="value">true to make the control visible; otherwise, false.</param>
    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      if (value)
        this.SetVisibleTrueCore();
      else
        this.SetVisibleFalseCore();
    }

    private void SetVisibleFalseCore()
    {
      this.canCapture = true;
      this.Capture = false;
      Application.RemoveMessageFilter((IMessageFilter) this);
      if (this.activeHwnd != IntPtr.Zero)
        vDropDownBase.SetActiveWindow(new HandleRef((object) this, this.activeHwnd));
      this.OnDropDownClosed();
    }

    private void SetVisibleTrueCore()
    {
      this.OnDropDownOpened();
      this.canCapture = false;
      this.activeHwnd = vDropDownBase.GetActiveWindow();
      if (!(this.activeHwnd != IntPtr.Zero))
        return;
      this.SetWindowLong(new HandleRef((object) this, this.Handle), -8, new HandleRef((object) this, this.activeHwnd));
      vDropDownBase.SetWindowPos(new HandleRef((object) this, this.Handle), this.HWND_TOPMOST, 0, 0, 0, 0, 8211);
      Application.AddMessageFilter((IMessageFilter) this);
    }

    /// <summary>Called when popup is closing</summary>
    protected virtual void OnDropDownClosing()
    {
      if (this.DropDownClosing == null)
        return;
      this.DropDownClosing((object) this, EventArgs.Empty);
    }

    /// <summary>Raises the DropDownClose event.</summary>
    protected virtual void OnDropDownClosed()
    {
      if (this.DropDownClose == null)
        return;
      this.DropDownClose((object) this, EventArgs.Empty);
    }

    /// <summary>Raises the DropDownOpen event.</summary>
    protected virtual void OnDropDownOpened()
    {
      if (this.DropDownOpen == null)
        return;
      this.DropDownOpen((object) this, EventArgs.Empty);
    }

    /// <exclude />
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetActiveWindow(HandleRef hWnd);

    private Point GetNewLocation(Control control, ArrowDirection popupDirection, Point location, Size popupSize, ref bool corected)
    {
      Rectangle screenRect = control != null ? Screen.GetWorkingArea(control) : Screen.PrimaryScreen.WorkingArea;
      Rectangle popupRect = new Rectangle(location, popupSize);
      Point point = location;
      switch (popupDirection)
      {
        case ArrowDirection.Left:
          location = new Point(popupRect.Left - popupSize.Width, popupRect.Top);
          break;
        case ArrowDirection.Up:
          location = new Point(popupRect.Left, popupRect.Top - popupSize.Height);
          break;
      }
      vDropDownBase.GetNewLocation(ref popupDirection, ref location, ref popupSize, ref corected, ref screenRect, ref popupRect, ref point);
      popupRect.Location = location;
      return this.MovePopupToVisibleArea(popupRect, screenRect).Location;
    }

    private static void GetNewLocation(ref ArrowDirection popupDirection, ref Point location, ref Size popupSize, ref bool corected, ref Rectangle screenRect, ref Rectangle popupRect, ref Point point)
    {
      if (!vDropDownBase.GetFinalShowDirection(popupRect, screenRect, ref popupDirection))
        return;
      switch (popupDirection)
      {
        case ArrowDirection.Left:
          location = new Point(popupRect.Left - popupSize.Width, popupRect.Top);
          break;
        case ArrowDirection.Up:
          location = new Point(popupRect.Left, popupRect.Top - popupSize.Height);
          break;
        case ArrowDirection.Right:
          location = new Point(popupRect.Right, popupRect.Top);
          break;
        case ArrowDirection.Down:
          location = new Point(popupRect.Left, popupRect.Bottom);
          break;
      }
      popupRect = new Rectangle(location, popupSize);
      if (vDropDownBase.GetFinalShowDirection(popupRect, screenRect, ref popupDirection))
        location = point;
      corected = true;
    }

    private Rectangle MovePopupToVisibleArea(Rectangle popupBounds, Rectangle screenRect)
    {
      vDropDownBase.CalculateRestrictedLocation(ref popupBounds, ref screenRect);
      return popupBounds;
    }

    private static void CalculateRestrictedLocation(ref Rectangle bounds, ref Rectangle rect)
    {
      if (rect.IsEmpty)
        return;
      if (bounds.Right > rect.Right)
        bounds.X -= bounds.Right - rect.Right;
      if (bounds.Bottom > rect.Bottom)
        bounds.Y -= bounds.Bottom - rect.Bottom;
      if (bounds.X < rect.X)
        bounds.X = rect.X;
      if (bounds.Y >= rect.Y)
        return;
      bounds.Y = rect.Y;
    }

    private static bool GetFinalShowDirection(Rectangle popupRect, Rectangle screenRect, ref ArrowDirection res)
    {
      bool flag = true;
      if (popupRect.Left < screenRect.Left && res == ArrowDirection.Left)
      {
        res = ArrowDirection.Right;
        return flag;
      }
      if (popupRect.Top < screenRect.Top && res == ArrowDirection.Up)
      {
        res = ArrowDirection.Down;
        return flag;
      }
      if (popupRect.Right > screenRect.Right && res == ArrowDirection.Right)
      {
        res = ArrowDirection.Left;
        return flag;
      }
      if (popupRect.Bottom <= screenRect.Bottom || res != ArrowDirection.Down)
        return false;
      res = ArrowDirection.Up;
      return flag;
    }

    /// <exclude />
    [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

    /// <exclude />
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);

    /// <exclude />
    public IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
    {
      if (IntPtr.Size == 4)
        return vDropDownBase.SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
      return vDropDownBase.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
    }

    /// <exclude />
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetActiveWindow();

    /// <exclude />
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

    /// <exclude />
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool RedrawWindow(HandleRef hwnd, IntPtr rcUpdate, HandleRef hrgnUpdate, int flags);

    /// <exclude />
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>Filters out a message before it is dispatched.</summary>
    /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
    /// <returns></returns>
    public bool PreFilterMessage(ref Message m)
    {
      bool flag = true;
      if (m.Msg == 513 && !this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
      {
        this.OnDropDownClosing();
        this.Hide();
        return flag;
      }
      if (m.Msg == 256)
      {
        if ((int) m.WParam == 13 || (int) m.WParam == 9)
        {
          this.OnDropDownClosing();
          this.Hide();
        }
        if ((int) m.WParam == 27)
        {
          this.OnDropDownClosing();
          this.Hide();
          return flag;
        }
      }
      return !flag;
    }

    private CreateParams GetNewParams()
    {
      CreateParams createParams = base.CreateParams;
      createParams.Style &= -79691777;
      createParams.Style |= -2080374784;
      createParams.ExStyle = 65544;
      createParams.ClassStyle = 2056;
      if (Environment.OSVersion.Version.Major > 5 || Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1)
        createParams.ClassStyle |= 131072;
      return createParams;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
      base.OnPaint(e);
    }
  }
}
