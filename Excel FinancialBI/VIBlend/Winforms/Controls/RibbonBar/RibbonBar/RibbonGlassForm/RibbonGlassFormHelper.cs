// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.GlassFormUtility
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  public class GlassFormUtility
  {
    private PaintHelper helper = new PaintHelper();
    private FormWindowState lastState;
    private OfficeRibbonForm form;
    private Padding margins;
    private bool marginsChecked;
    private bool frameExtended;
    private vRibbonBar ribbon;

    /// <summary>Gets or sets the Ribbon related with the form</summary>
    public vRibbonBar Ribbon
    {
      get
      {
        return this.ribbon;
      }
      internal set
      {
        this.ribbon = value;
        if (value != null)
          this.ribbon.DropDownStyleChanged += new EventHandler(this.ribbon_DropDownStyleChanged);
        this.UpdateRibbonConditions();
      }
    }

    /// <summary>Gets the form this class is helping</summary>
    public OfficeRibbonForm Form
    {
      get
      {
        return this.form;
      }
    }

    /// <summary>Gets the margins of the non-client area</summary>
    public Padding Margins
    {
      get
      {
        return this.margins;
      }
    }

    /// <summary>
    /// Gets or sets if the margins are already checked by WndProc
    /// </summary>
    private bool MarginsChecked
    {
      get
      {
        return this.marginsChecked;
      }
      set
      {
        this.marginsChecked = value;
      }
    }

    /// <summary>
    /// Gets if the <see cref="P:VIBlend.WinForms.Controls.GlassFormUtility.Form" /> is currently in Designer mode
    /// </summary>
    private bool DesignMode
    {
      get
      {
        if (this.Form != null && this.Form.Site != null)
          return this.Form.Site.DesignMode;
        return false;
      }
    }

    /// <summary>Creates a new helper for the specified form</summary>
    /// <param name="f"></param>
    public GlassFormUtility(OfficeRibbonForm f)
    {
      this.form = f;
      this.form.Load += new EventHandler(this.Form_Activated);
      this.form.Activated += new EventHandler(this.form_Activated);
      this.form.Deactivate += new EventHandler(this.form_Deactivate);
      this.form.Paint += new PaintEventHandler(this.Form_Paint);
      this.form.ResizeEnd += new EventHandler(this.form_ResizeEnd);
      this.form.Resize += new EventHandler(this.form_Resize);
      this.form.Layout += new LayoutEventHandler(this.form_Layout);
      this.form.LostFocus += new EventHandler(this.form_LostFocus);
    }

    private void form_LostFocus(object sender, EventArgs e)
    {
      this.form.Invalidate();
    }

    private void form_Activated(object sender, EventArgs e)
    {
      this.form.Refresh();
    }

    private void form_Deactivate(object sender, EventArgs e)
    {
      this.form.Refresh();
    }

    private void form_Layout(object sender, LayoutEventArgs e)
    {
      if (this.lastState == this.form.WindowState)
        return;
      if (!WindowsAPI.IsGlassEnabled)
        this.Form.Invalidate();
      this.lastState = this.form.WindowState;
    }

    private void form_Resize(object sender, EventArgs e)
    {
      this.UpdateRibbonConditions();
      using (Graphics graphics = this.Form.CreateGraphics())
      {
        using (Brush brush = (Brush) new SolidBrush(this.Form.BackColor))
          graphics.FillRectangle(brush, Rectangle.FromLTRB(this.Margins.Left - 8, this.Margins.Top + 8, this.Form.Width - this.Margins.Right - 8, this.Form.Height - this.Margins.Bottom - 8));
      }
    }

    private void form_ResizeEnd(object sender, EventArgs e)
    {
      this.UpdateRibbonConditions();
      this.Form.Refresh();
    }

    private void ribbon_DropDownStyleChanged(object sender, EventArgs e)
    {
      this.UpdateRibbonConditions();
    }

    /// <summary>
    /// Checks if ribbon should be docked or floating and updates its size
    /// </summary>
    private void UpdateRibbonConditions()
    {
      if (this.Ribbon == null)
        return;
      if (this.DesignMode)
      {
        this.Ribbon.Dock = DockStyle.Top;
      }
      else
      {
        if (this.Ribbon.Dock != DockStyle.None)
          this.Ribbon.Dock = DockStyle.None;
        if (!WindowsAPI.IsGlassEnabled)
        {
          if (this.Form.WindowState == FormWindowState.Maximized)
            this.SetMargins(new Padding(0, this.Ribbon.Height, 0, 0));
          else
            this.SetMargins(new Padding(8, this.Ribbon.Height + 1, 8, 8));
        }
        if (WindowsAPI.IsGlassEnabled)
          this.Ribbon.SetBounds(this.Margins.Left, 1, this.Form.Width - this.Margins.Horizontal, this.Ribbon.Height);
        else if (this.Form.WindowState == FormWindowState.Maximized)
          this.Ribbon.SetBounds(0, 1, this.Form.Width, this.Ribbon.Height);
        else
          this.Ribbon.SetBounds(7, 1, this.Form.Width - 14, this.Ribbon.Height);
        if (WindowsAPI.IsGlassEnabled)
          return;
        this.Ribbon.Invalidate();
        this.Form.Invalidate();
      }
    }

    /// <summary>Called when helped form is activated</summary>
    /// <param name="sender">Object that raised the event</param>
    /// <param name="e">Event data</param>
    public void Form_Paint(object sender, PaintEventArgs e)
    {
      if (this.DesignMode)
        this.RenderTitle(e);
      else if (WindowsAPI.IsGlassEnabled)
      {
        WindowsAPI.FillForGlass(e.Graphics, new Rectangle(0, 0, this.Form.Width, this.Form.Height));
        using (Brush brush = (Brush) new SolidBrush(this.Form.BackColor))
          e.Graphics.FillRectangle(brush, Rectangle.FromLTRB(this.Margins.Left, this.Margins.Top, this.Form.Width - this.Margins.Right, this.Form.Height - this.Margins.Bottom));
      }
      else
        this.RenderTitle(e);
    }

    /// <summary>
    /// Creates a rectangle with the specified corners rounded
    /// </summary>
    /// <param name="r"></param>
    /// <param name="radius"></param>
    /// <param name="corners"></param>
    /// <returns></returns>
    public static GraphicsPath RoundRectangle(Rectangle r, int radius)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int num1 = radius * 2;
      int num2 = num1;
      int num3 = num1;
      int num4 = num1;
      int num5 = num1;
      graphicsPath.AddLine(r.Left + num2, r.Top, r.Right - num3, r.Top);
      if (num3 > 0)
        graphicsPath.AddArc(Rectangle.FromLTRB(r.Right - num3, r.Top, r.Right, r.Top + num3), -90f, 90f);
      graphicsPath.AddLine(r.Right, r.Top + num3, r.Right, r.Bottom - num4);
      if (num4 > 0)
        graphicsPath.AddArc(Rectangle.FromLTRB(r.Right - num4, r.Bottom - num4, r.Right, r.Bottom), 0.0f, 90f);
      graphicsPath.AddLine(r.Right - num4, r.Bottom, r.Left + num5, r.Bottom);
      if (num5 > 0)
        graphicsPath.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - num5, r.Left + num5, r.Bottom), 90f, 90f);
      graphicsPath.AddLine(r.Left, r.Bottom - num5, r.Left, r.Top + num2);
      if (num2 > 0)
        graphicsPath.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + num2, r.Top + num2), 180f, 90f);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    /// <summary>Draws the title bar of the form when not in glass</summary>
    /// <param name="e"></param>
    private void RenderTitle(PaintEventArgs e)
    {
      Rectangle rectangle1 = new Rectangle(Point.Empty, this.Form.Size);
      Rectangle rectangle2 = new Rectangle(Point.Empty, new Size(rectangle1.Width - 1, rectangle1.Height - 1));
      int num = 6;
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      if (!this.DesignMode)
        Licensing.LICheck((Control) this.Form);
      Rectangle rect1 = new Rectangle(this.Form.Padding.Left - 1, this.Form.Padding.Top - 1, this.Form.Width - this.Form.Padding.Horizontal + 1, this.Form.Height - this.Form.Padding.Vertical + 1);
      if (this.Ribbon != null)
        rect1 = new Rectangle(this.Form.Padding.Left - 1, 30, this.Form.Width - this.Form.Padding.Horizontal + 1, this.Form.Height - this.Form.Padding.Vertical + 1 - 30);
      int borderOffset = num;
      if (this.Form.WindowState != FormWindowState.Maximized)
      {
        RectangleF rectangleF = new RectangleF((PointF) this.Form.PointToScreen(Point.Empty), (SizeF) this.Form.Size);
        Rectangle rectangle3 = new Rectangle(0, 0, this.Form.Width, this.Form.Height);
        this.Form.titleFill.Bounds = new Rectangle(this.Form.TitleRectangle.X - 1, this.Form.TitleRectangle.Y - 1, this.Form.Width - 2, this.Form.Height - 2);
        Rectangle rectangle4 = new Rectangle(0, 0, this.Form.Width, this.Form.Padding.Top);
        Rectangle rectangle5 = new Rectangle(0, this.Form.Height - this.Form.Padding.Bottom, this.Form.Width, this.Form.Padding.Bottom);
        Rectangle rectangle6 = new Rectangle(0, 0, this.Form.Padding.Left + 1, this.Form.Height);
        Rectangle rectangle7 = new Rectangle(this.Form.Width - this.Form.Padding.Right - 1, 0, this.Form.Padding.Right + 1, this.Form.Height);
        using (SolidBrush solidBrush = new SolidBrush(this.Form.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, rect1);
        this.Form.titleFill.Bounds = rectangle4;
        this.Form.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        this.Form.titleFill.Bounds = rectangle5;
        this.Form.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        this.Form.titleFill.Bounds = rectangle6;
        this.Form.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        this.Form.titleFill.Bounds = rectangle7;
        this.Form.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        Rectangle rectangle8 = new Rectangle(this.Form.TitleRectangle.X - 1, this.Form.TitleRectangle.Y - 1, this.Form.Width - 2, this.Form.TitleHeight);
        this.Form.titleFill.Bounds = rectangle8;
        if (this.Ribbon != null)
        {
          rectangle8 = new Rectangle(this.Form.TitleRectangle.X - 1, this.Form.TitleRectangle.Y + 1, this.Form.Width - 2, 29);
          this.Form.titleFill.Bounds = rectangle8;
        }
        this.Form.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        using (Pen pen1 = new Pen(this.Form.titleFill.BorderColor))
        {
          using (Pen pen2 = new Pen(ControlPaint.Light(Color.FromArgb(150, this.Form.titleFill.BorderColor))))
          {
            Color borderColor = this.Form.Theme.StyleNormal.BorderColor;
            if (!borderColor.Equals((object) Color.Empty))
              pen2.Color = borderColor;
            if (this.Ribbon == null)
            {
              e.Graphics.DrawRectangle(pen2, rect1);
            }
            else
            {
              Rectangle rect2 = new Rectangle(rect1.X, 138, rect1.Width, this.Form.Height - 140 - num);
              if (rect2.Width > 0)
              {
                if (rect2.Height > 0)
                  e.Graphics.DrawRectangle(pen2, rect2);
              }
            }
          }
          Rectangle bounds = new Rectangle(0, 0, this.Form.Width - 1, this.Form.Height - 1);
          GraphicsPath roundedPathRect1 = this.helper.GetRoundedPathRect(bounds, 5);
          e.Graphics.DrawPath(pen1, roundedPathRect1);
          Color color = this.Form.Theme.QueryColorSetter("RibbonFormBorder2");
          if (!color.Equals((object) Color.Empty))
          {
            bounds.Inflate(-1, -1);
            GraphicsPath roundedPathRect2 = this.helper.GetRoundedPathRect(bounds, 5);
            pen1.Color = color;
            e.Graphics.DrawPath(pen1, roundedPathRect2);
          }
        }
      }
      else
      {
        this.Form.titleFill.Bounds = new Rectangle(0, -1, this.Form.Width, 30);
        this.Form.titleFill.Opacity = 1f;
        this.Form.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        rect1 = new Rectangle(0, this.Form.TitleHeight, this.Form.Width, this.Form.Height - this.Form.TitleHeight);
        if (this.Ribbon != null)
          rect1 = new Rectangle(0, 30, this.Form.Width, this.Form.Height - 30);
        using (SolidBrush solidBrush = new SolidBrush(this.Form.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, rect1);
        borderOffset = 0;
      }
      if (this.Ribbon == null)
        this.Form.DrawText(e, 0, borderOffset);
      e.Graphics.SmoothingMode = smoothingMode;
    }

    /// <summary>Called when helped form is activated</summary>
    /// <param name="sender">Object that raised the event</param>
    /// <param name="e">Event data</param>
    protected virtual void Form_Activated(object sender, EventArgs e)
    {
      if (this.DesignMode || this.Ribbon == null)
        return;
      WindowsAPI.MARGINS marInset = new WindowsAPI.MARGINS(this.Margins.Left, this.Margins.Right, this.Margins.Bottom + this.Ribbon.TitleHeight, this.Margins.Bottom);
      if (!WindowsAPI.IsVista || this.frameExtended || !WindowsAPI.IsGlassEnabled)
        return;
      WindowsAPI.DwmExtendFrameIntoClientArea(this.Form.Handle, ref marInset);
      this.frameExtended = true;
    }

    [DllImport("USER32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

    public Point GetPointFromPtr(IntPtr ptr)
    {
      Point point = new Point((int) ptr);
      RECT lpRect = new RECT();
      GlassFormUtility.GetWindowRect(this.Form.Handle, ref lpRect);
      point.X -= lpRect.left;
      point.Y -= lpRect.top;
      return point;
    }

    /// <summary>
    /// Processes the WndProc for a form with a Ribbbon. Returns true if message has been handled
    /// </summary>
    /// <param name="m">Message to process</param>
    /// <returns><c>true</c> if message has been handled. <c>false</c> otherwise</returns>
    public virtual bool WndProc(ref Message m)
    {
      if (this.DesignMode)
        return false;
      bool flag = false;
      if (m.Msg == 165)
        this.Form.titleMenu.Show((Control) this.Form, this.GetPointFromPtr(m.LParam));
      if (m.Msg == 787)
        this.Form.SetTaskBarMenu();
      IntPtr result;
      if (WindowsAPI.IsVista && WindowsAPI.IsGlassEnabled && WindowsAPI.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, out result) == 1)
      {
        m.Result = result;
        flag = true;
      }
      if (!flag)
      {
        if (m.Msg == 131 && (int) m.WParam == 1)
        {
          if (WindowsAPI.IsGlassEnabled)
          {
            WindowsAPI.NCCALCSIZE_PARAMS nccalcsizeParams = (WindowsAPI.NCCALCSIZE_PARAMS) Marshal.PtrToStructure(m.LParam, typeof (WindowsAPI.NCCALCSIZE_PARAMS));
            if (!this.MarginsChecked)
            {
              if (WindowsAPI.IsGlassEnabled)
                this.SetMargins(new Padding(nccalcsizeParams.rect2.Left - nccalcsizeParams.rect1.Left, this.Ribbon.Height, nccalcsizeParams.rect1.Right - nccalcsizeParams.rect2.Right, nccalcsizeParams.rect1.Bottom - nccalcsizeParams.rect2.Bottom));
              else if (this.Form.WindowState == FormWindowState.Maximized)
                this.SetMargins(new Padding(0, this.Ribbon.Height, 0, 0));
              else
                this.SetMargins(new Padding(8, this.Ribbon.Height, 8, 8));
              this.MarginsChecked = true;
            }
            Marshal.StructureToPtr((object) nccalcsizeParams, m.LParam, false);
            m.Result = IntPtr.Zero;
            flag = true;
          }
        }
        else if (m.Msg == 132 && (int) m.Result == 0)
        {
          m.Result = new IntPtr(Convert.ToInt32((object) this.NonClientHitTest(new Point(WindowsAPI.LoWord((int) m.LParam), WindowsAPI.HiWord((int) m.LParam)))));
          flag = true;
        }
        else if (this.Ribbon != null && m.Msg != 274 && (m.Msg != 71 && m.Msg != 70))
        {
          int msg = m.Msg;
        }
      }
      return flag;
    }

    /// <summary>
    /// Performs hit test for mouse on the non client area of the form
    /// </summary>
    /// <param name="form">Form to check bounds</param>
    /// <param name="dwmMargins">Margins of non client area</param>
    /// <param name="lparam">Lparam of</param>
    /// <returns></returns>
    public virtual GlassFormUtility.NonClientHitTestResult NonClientHitTest(Point hitPoint)
    {
      if (this.form.WindowState == FormWindowState.Maximized)
        return GlassFormUtility.NonClientHitTestResult.Caption;
      if (this.Form.RectangleToScreen(new Rectangle(0, 0, this.Margins.Left, this.Margins.Left)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.TopLeft;
      if (this.Form.RectangleToScreen(new Rectangle(this.Form.Width - this.Margins.Right, 0, this.Margins.Right, this.Margins.Right)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.TopRight;
      if (this.Form.RectangleToScreen(new Rectangle(0, this.Form.Height - this.Margins.Bottom, this.Margins.Left, this.Margins.Bottom)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.BottomLeft;
      if (this.Form.RectangleToScreen(new Rectangle(this.Form.Width - this.Margins.Right, this.Form.Height - this.Margins.Bottom, this.Margins.Right, this.Margins.Bottom)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.BottomRight;
      if (this.Form.RectangleToScreen(new Rectangle(0, 0, this.Form.Width, this.Margins.Left)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.Top;
      if (this.Form.RectangleToScreen(new Rectangle(0, this.Margins.Left, this.Form.Width, 30 - this.Margins.Left)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.Caption;
      if (this.Form.RectangleToScreen(new Rectangle(0, 0, this.Margins.Left, this.Form.Height)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.Left;
      if (this.Form.RectangleToScreen(new Rectangle(this.Form.Width - this.Margins.Right, 0, this.Margins.Right, this.Form.Height)).Contains(hitPoint))
        return GlassFormUtility.NonClientHitTestResult.Right;
      return this.Form.RectangleToScreen(new Rectangle(0, this.Form.Height - this.Margins.Bottom, this.Form.Width, this.Margins.Bottom)).Contains(hitPoint) ? GlassFormUtility.NonClientHitTestResult.Bottom : GlassFormUtility.NonClientHitTestResult.Client;
    }

    /// <summary>
    /// Sets the value of the <see cref="P:VIBlend.WinForms.Controls.GlassFormUtility.Margins" /> property;
    /// </summary>
    /// <param name="p"></param>
    private void SetMargins(Padding p)
    {
      this.margins = p;
      Padding padding = p;
      if (this.DesignMode)
        return;
      this.Form.Padding = padding;
    }

    /// <summary>
    /// Possible results of a hit test on the non client area of a form
    /// </summary>
    public enum NonClientHitTestResult
    {
      Nowhere = 0,
      Client = 1,
      Caption = 2,
      GrowBox = 4,
      MinimizeButton = 8,
      MaximizeButton = 9,
      Left = 10,
      Right = 11,
      Top = 12,
      TopLeft = 13,
      TopRight = 14,
      Bottom = 15,
      BottomLeft = 16,
      BottomRight = 17,
    }
  }
}
