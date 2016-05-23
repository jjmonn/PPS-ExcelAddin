// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vToolStripDropDown
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class vToolStripDropDown : ToolStripDropDown
  {
    private bool focusOnPopUp = true;
    private bool resize = true;
    private Timer indicatorsTimer1 = new Timer();
    private Timer indicatorsTimer2 = new Timer();
    private Panel panel = new Panel();
    internal const int WM_NCHITTEST = 132;
    internal const int WM_NCACTIVATE = 134;
    internal const int WS_EX_NOACTIVATE = 134217728;
    internal const int HTTRANSPARENT = -1;
    internal const int HTLEFT = 10;
    internal const int HTRIGHT = 11;
    internal const int HTTOP = 12;
    internal const int HTTOPLEFT = 13;
    internal const int HTTOPRIGHT = 14;
    internal const int HTBOTTOM = 15;
    internal const int HTBOTTOMLEFT = 16;
    internal const int HTBOTTOMRIGHT = 17;
    internal const int WM_USER = 1024;
    internal const int WM_REFLECT = 8192;
    internal const int WM_COMMAND = 273;
    internal const int CBN_DROPDOWN = 7;
    internal const int WM_GETMINMAXINFO = 36;
    private vToolStripDropDown ownerPopup;
    private vToolStripDropDown childPopup;
    private ToolStripControlHost host;
    private Control content;
    private vSizingControl grip;

    public Control Content
    {
      get
      {
        return this.content;
      }
    }

    public bool CanResize
    {
      get
      {
        return this.resize;
      }
      set
      {
        this.resize = value;
        if (!value)
        {
          this.grip.Visible = false;
          this.Panel.Controls.Remove((Control) this.grip);
        }
        else
        {
          this.grip.Visible = true;
          this.Panel.Controls.Add((Control) this.grip);
        }
      }
    }

    public bool FocusOnPopUp
    {
      get
      {
        return this.focusOnPopUp;
      }
      set
      {
        this.focusOnPopUp = value;
      }
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 134217728;
        return createParams;
      }
    }

    public Panel Panel
    {
      get
      {
        return this.panel;
      }
    }

    /// <summary>Occurs when hide animation has finished</summary>
    public event EventHandler HideAnimationFinished;

    public vToolStripDropDown()
      : this((Control) new Panel())
    {
    }

    public vToolStripDropDown(Control content)
    {
      if (content == null)
        return;
      this.grip = new vSizingControl((Control) this.panel);
      this.grip.Height = 8;
      this.grip.resizeLocation = false;
      this.grip.Dock = DockStyle.Bottom;
      this.panel.Dock = DockStyle.Fill;
      content.Dock = DockStyle.Fill;
      this.panel.Controls.Add(content);
      this.panel.Controls.Add((Control) this.grip);
      this.panel.MinimumSize = new Size(250, 250);
      this.panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.content = (Control) this.panel;
      this.AutoSize = true;
      this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.host = new ToolStripControlHost(this.content);
      this.Padding = this.Margin = this.host.Padding = this.host.Margin = Padding.Empty;
      this.Size = this.content.Size;
      this.content.Location = Point.Empty;
      this.Items.Add((ToolStripItem) this.host);
      content.Disposed += (EventHandler) ((sender, e) =>
      {
        this.content = (Control) null;
        this.Dispose(true);
      });
      content.RegionChanged += (EventHandler) ((sender, e) => this.InvalidateRegion());
      this.InvalidateRegion();
      this.indicatorsTimer1.Tick += new EventHandler(this.indicatorsTimer1_Tick);
      this.indicatorsTimer1.Interval = 10;
      this.indicatorsTimer2.Tick += new EventHandler(this.indicatorsTimer2_Tick);
      this.indicatorsTimer2.Interval = 10;
      this.Opacity = 0.0;
      this.Opening += new CancelEventHandler(this.vToolStripDropDown_Opening);
      this.content.SizeChanged += new EventHandler(this.content_SizeChanged);
      this.content.LocationChanged += new EventHandler(this.content_LocationChanged);
      this.grip.MouseMove += new MouseEventHandler(this.grip_MouseMove);
    }

    private void grip_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      Size size = this.content.Size;
      this.content.MinimumSize = this.grip.PreferredBounds.Size;
      this.MinimumSize = this.grip.PreferredBounds.Size;
      this.Refresh();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
    }

    private void content_LocationChanged(object sender, EventArgs e)
    {
    }

    private void content_SizeChanged(object sender, EventArgs e)
    {
      this.content.Location = Point.Empty;
      this.Size = this.content.Size;
      this.MinimumSize = this.content.Size;
      this.content.MinimumSize = this.MinimumSize;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
    }

    protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
    {
      base.OnClosing(e);
    }

    private void vToolStripDropDown_Opening(object sender, CancelEventArgs e)
    {
      this.StartAnimation();
    }

    private void indicatorsTimer2_Tick(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (this.Opacity < 1.0)
      {
        this.Opacity += 0.05;
        if (this.Opacity > 1.0)
          this.Opacity = 1.0;
      }
      else
        this.indicatorsTimer2.Stop();
      this.Invalidate();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.indicatorsTimer1.Dispose();
        this.indicatorsTimer2.Dispose();
      }
      base.Dispose(disposing);
    }

    private void indicatorsTimer1_Tick(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (this.Opacity < 0.05)
        this.Opacity = 0.0;
      if (this.Opacity > 0.0)
      {
        this.Opacity -= 0.05;
      }
      else
      {
        this.indicatorsTimer1.Stop();
        this.Hide();
        this.OnHideAnimationFinished();
      }
      this.Invalidate();
    }

    protected virtual void OnHideAnimationFinished()
    {
      if (this.HideAnimationFinished == null)
        return;
      this.HideAnimationFinished((object) this, EventArgs.Empty);
    }

    internal void StartForwardsPlusMinusAnimation()
    {
      if (this.DesignMode)
        return;
      if (this.indicatorsTimer1 != null)
        this.indicatorsTimer1.Start();
      if (this.indicatorsTimer2 == null)
        return;
      this.indicatorsTimer2.Stop();
    }

    internal void StartAnimation()
    {
      if (this.DesignMode)
        return;
      if (this.indicatorsTimer1 != null)
        this.indicatorsTimer1.Stop();
      if (this.indicatorsTimer2 == null)
        return;
      this.indicatorsTimer2.Start();
    }

    protected void InvalidateRegion()
    {
    }

    public void Show(Control control)
    {
      if (control == null)
        throw new ArgumentNullException("control");
      this.SetOwner(control);
      this.Show(control, control.ClientRectangle);
    }

    public void Show(Control control, Rectangle area)
    {
      if (control == null)
        throw new ArgumentNullException("control");
      this.SetOwner(control);
      Point point = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
      Rectangle workingArea = Screen.FromControl(control).WorkingArea;
      if (point.X + this.Size.Width > workingArea.Left + workingArea.Width)
        point.X = workingArea.Left + workingArea.Width - this.Size.Width;
      if (point.Y + this.Size.Height > workingArea.Top + workingArea.Height)
        point.Y -= this.Size.Height + area.Height;
      point = control.PointToClient(point);
      this.Show(control, point, ToolStripDropDownDirection.BelowRight);
    }

    private void SetOwner(Control control)
    {
      if (control == null)
        return;
      if (control is vToolStripDropDown)
      {
        vToolStripDropDown toolStripDropDown = control as vToolStripDropDown;
        this.ownerPopup = toolStripDropDown;
        this.ownerPopup.childPopup = this;
        this.OwnerItem = toolStripDropDown.Items[0];
      }
      else
      {
        if (control.Parent == null)
          return;
        this.SetOwner(control.Parent);
      }
    }

    protected override void OnOpening(CancelEventArgs e)
    {
      if (this.content.IsDisposed || this.content.Disposing)
      {
        e.Cancel = true;
      }
      else
      {
        this.InvalidateRegion();
        base.OnOpening(e);
      }
    }

    protected override void OnOpened(EventArgs e)
    {
      if (this.focusOnPopUp)
        this.content.Focus();
      base.OnOpened(e);
    }

    protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
    {
      vToolStripDropDown toolStripDropDown = this.ownerPopup;
      base.OnClosed(e);
      this.Show(Cursor.Position);
    }

    internal static int HIWORD(int n)
    {
      return n >> 16 & (int) ushort.MaxValue;
    }

    internal static int HIWORD(IntPtr n)
    {
      return vToolStripDropDown.HIWORD((int) (long) n);
    }

    internal static int LOWORD(int n)
    {
      return n & (int) ushort.MaxValue;
    }

    internal static int LOWORD(IntPtr n)
    {
      return vToolStripDropDown.LOWORD((int) (long) n);
    }

    internal struct MINMAXINFO
    {
      public Point reserved;
      public Size maxSize;
      public Point maxPosition;
      public Size minTrackSize;
      public Size maxTrackSize;
    }
  }
}
