// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vSizingControl
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class vSizingControl : Control
  {
    private SizingDirection sizingDirection = SizingDirection.Both;
    public bool resizeSize = true;
    public bool resizeLocation = true;
    internal static vSizingControl sizingControl;
    private Point downLocation;
    private Rectangle downBounds;
    private Size downSize;
    private Point mouseDownPosition;
    private Point tmpLocation;
    internal BackgroundElement backFill;
    private Control contenCtl;
    public Rectangle PreferredBounds;

    /// <summary>Gets the back ground fill.</summary>
    /// <value>The back ground fill.</value>
    public BackgroundElement BackGroundFill
    {
      get
      {
        return this.backFill;
      }
    }

    /// <summary>Gets or sets the sizing direction.</summary>
    /// <value>The sizing direction.</value>
    [DefaultValue(2)]
    public SizingDirection SizingDirection
    {
      get
      {
        return this.sizingDirection;
      }
      set
      {
        this.sizingDirection = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vSizingControl" /> class.
    /// </summary>
    public vSizingControl()
    {
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vButton());
      this.backFill.LoadTheme(ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE));
      this.ForeColor = this.backFill.ForeColor;
      this.ParentChanged += new EventHandler(this.vSizingControl_ParentChanged);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vSizingControl" /> class.
    /// </summary>
    /// <param name="control">The control.</param>
    public vSizingControl(Control control)
    {
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vButton());
      this.backFill.LoadTheme(ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE));
      this.ForeColor = this.backFill.ForeColor;
      this.contenCtl = control;
    }

    private void vSizingControl_ParentChanged(object sender, EventArgs e)
    {
      if (this.contenCtl != null)
        return;
      this.contenCtl = this.Parent;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.mouseDownPosition = Control.MousePosition;
      this.tmpLocation = this.mouseDownPosition;
      this.downLocation = this.contenCtl.PointToScreen(Point.Empty);
      this.downSize = this.contenCtl.Size;
      this.downBounds = new Rectangle(this.downLocation, this.downSize);
      this.Capture = true;
      vSizingControl.sizingControl = this;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      base.OnPaint(e);
      if (this.Size.Width == 0 || this.Size.Height == 0)
        return;
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      RectangleF rect1 = new RectangleF((float) (this.ClientRectangle.Width / 2 - 14), (float) (this.Size.Height / 2) - 1f, 2f, 2f);
      Color color = Color.White;
      Color color2 = Color.White;
      if (this.backFill.Theme.StyleNormal.FillStyle is FillStyleSolid)
      {
        color = ((FillStyleSolid) this.backFill.Theme.StyleNormal.FillStyle).Color;
        color2 = ((FillStyleSolid) this.backFill.Theme.StyleNormal.FillStyle).Color;
      }
      else if (this.backFill.Theme.StyleNormal.FillStyle is FillStyleGradient)
      {
        color = ((FillStyleGradient) this.backFill.Theme.StyleNormal.FillStyle).Color1;
        color2 = ((FillStyleGradient) this.backFill.Theme.StyleNormal.FillStyle).Color2;
      }
      else if (this.backFill.Theme.StyleNormal.FillStyle is FillStyleGradientEx)
      {
        color = ((FillStyleGradientEx) this.backFill.Theme.StyleNormal.FillStyle).Color1;
        color2 = ((FillStyleGradientEx) this.backFill.Theme.StyleNormal.FillStyle).Color2;
      }
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, color, color2, LinearGradientMode.Vertical);
      graphics.FillRectangle((Brush) linearGradientBrush, this.ClientRectangle);
      if (this.sizingDirection == SizingDirection.Both || this.sizingDirection == SizingDirection.Horizontal)
      {
        Rectangle rect2 = new Rectangle(this.ClientRectangle.Width - 8, (int) rect1.Y + 1, 2, 2);
        RectangleF rect3 = new RectangleF((float) rect2.X + 0.1f, (float) rect2.Y + 0.1f, 2f, 2f);
        graphics.FillRectangle((Brush) new SolidBrush(color), rect3);
        graphics.FillRectangle((Brush) new SolidBrush(this.backFill.ForeColor), rect2);
        rect2 = new Rectangle(this.ClientRectangle.Width - 5, (int) rect1.Y + 1, 2, 2);
        rect3 = new RectangleF((float) rect2.X + 0.1f, (float) rect2.Y + 0.1f, 2f, 2f);
        graphics.FillRectangle((Brush) new SolidBrush(color), rect3);
        graphics.FillRectangle((Brush) new SolidBrush(this.backFill.ForeColor), rect2);
        rect2 = new Rectangle(this.ClientRectangle.Width - 5, (int) rect1.Y - 2, 2, 2);
        rect3 = new RectangleF((float) rect2.X + 0.1f, (float) rect2.Y + 0.1f, 2f, 2f);
        graphics.FillRectangle((Brush) new SolidBrush(color), rect3);
        graphics.FillRectangle((Brush) new SolidBrush(this.backFill.ForeColor), rect2);
      }
      if (this.sizingDirection != SizingDirection.Both && this.sizingDirection != SizingDirection.Vertical)
        return;
      int num = 0;
      while (num++ <= 6)
      {
        RectangleF rect2 = new RectangleF(rect1.X + 0.1f, rect1.Y + 0.1f, 2f, 2f);
        graphics.FillRectangle((Brush) new SolidBrush(color), rect2);
        graphics.FillRectangle((Brush) new SolidBrush(this.backFill.ForeColor), rect1);
        rect1.X += 4f;
        if ((double) rect1.X + 6.0 > (double) (this.Size.Width - 3))
          break;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.Parent.Cursor = Cursors.Arrow;
      base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      int newXOffset;
      int newYOffset;
      this.GetOffsets(out newXOffset, out newYOffset);
      base.OnMouseMove(e);
      if (e.Button != MouseButtons.Left || vSizingControl.sizingControl != this || !(this.tmpLocation != new Point(e.X, e.Y)))
        return;
      Control parentControl;
      int height;
      int width;
      this.ResizeControlInternally(newXOffset, newYOffset, out parentControl, out height, out width);
      if (width == 0 || height == 0)
        return;
      parentControl.Bounds = !this.resizeLocation || !this.resizeSize ? new Rectangle(0, 0, width, height) : new Rectangle(this.downLocation.X, this.downLocation.Y, width, height);
      this.tmpLocation = Control.MousePosition;
    }

    private void GetOffsets(out int newXOffset, out int newYOffset)
    {
      this.SetCursor(new Rectangle(this.ClientRectangle.Width - 10, 0, 10, this.ClientRectangle.Height));
      newXOffset = this.sizingDirection == SizingDirection.Vertical ? 0 : Control.MousePosition.X - this.mouseDownPosition.X;
      newYOffset = this.sizingDirection == SizingDirection.Horizontal ? 0 : Control.MousePosition.Y - this.mouseDownPosition.Y;
      if (this.sizingDirection != SizingDirection.None)
        return;
      newXOffset = 0;
      newYOffset = 0;
    }

    private void ResizeControlInternally(int newXOffset, int newYOffset, out Control parentControl, out int height, out int width)
    {
      parentControl = this.contenCtl;
      height = this.downSize.Height + newYOffset;
      width = this.downSize.Width + newXOffset;
      this.PreferredBounds = new Rectangle(this.downLocation.X, this.downLocation.Y, width, height);
      vSizingControl.ResizeParent(parentControl, ref height, ref width);
    }

    private Rectangle SetCursor(Rectangle rect)
    {
      if (this.RectangleToScreen(rect).Contains(Cursor.Position))
      {
        if (this.sizingDirection == SizingDirection.Both)
          this.contenCtl.Cursor = Cursors.SizeNWSE;
        else if (this.sizingDirection == SizingDirection.Horizontal)
          this.contenCtl.Cursor = Cursors.SizeWE;
        else if (this.sizingDirection == SizingDirection.Vertical)
          this.contenCtl.Cursor = Cursors.SizeNS;
      }
      else if (this.sizingDirection == SizingDirection.Both || this.sizingDirection == SizingDirection.Vertical)
        this.contenCtl.Cursor = Cursors.SizeNS;
      return rect;
    }

    private static void ResizeParent(Control parentControl, ref int height, ref int width)
    {
      if (parentControl.MinimumSize.Height != 0 && height < parentControl.MinimumSize.Height)
        height = parentControl.MinimumSize.Height;
      if (parentControl.MaximumSize.Height != 0 && height > parentControl.MaximumSize.Height)
        height = parentControl.MaximumSize.Height;
      if (parentControl.MinimumSize.Width != 0 && width < parentControl.MinimumSize.Width)
        width = parentControl.MinimumSize.Width;
      if (parentControl.MaximumSize.Width == 0 || width <= parentControl.MaximumSize.Width)
        return;
      width = parentControl.MaximumSize.Width;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (vSizingControl.sizingControl != this)
        return;
      vSizingControl.sizingControl = (vSizingControl) null;
    }
  }
}
