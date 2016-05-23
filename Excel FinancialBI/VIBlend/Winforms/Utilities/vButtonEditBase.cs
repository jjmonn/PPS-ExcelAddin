// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.vButtonEditBase
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  [ToolboxItem(false)]
  public class vButtonEditBase : vEditorControlBase
  {
    private int vButtonWidth = 15;
    private const int vBorderWidth = 1;

    /// <summary>Gets the button rectangle.</summary>
    /// <value>The button rectangle.</value>
    public Rectangle ButtonRectangle
    {
      get
      {
        return this.PerformBoundsCalculations(this.ClientRectangle);
      }
    }

    [DefaultValue(16)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    public virtual int VButtonWidth
    {
      get
      {
        return this.vButtonWidth;
      }
      set
      {
        if (value < 8)
          value = 8;
        else if (value > this.Width - 16)
          value = this.Width - 16;
        this.vButtonWidth = value;
        this.PerformEditLayout();
        this.Invalidate();
      }
    }

    protected bool Hover
    {
      get
      {
        return this.PerformBoundsCalculations(this.ClientRectangle).Contains(this.PointToClient(Cursor.Position));
      }
    }

    [Category("Action")]
    public event EventHandler vButtonClick;

    [Category("Action")]
    public event EventHandler DrawvButton;

    /// <summary>Performs the bounds calculations.</summary>
    public Rectangle PerformBoundsCalculations(Rectangle bounds)
    {
      Rectangle rectangle = this.PerformBoundsCalculations(bounds, this.vButtonWidth);
      if (this.RightToLeft != RightToLeft.No)
        rectangle.X = 1;
      return rectangle;
    }

    private Rectangle PerformBoundsCalculations(Rectangle bounds, int buttonWidth)
    {
      return new Rectangle(bounds.Right - buttonWidth - 1, bounds.Top + 1, buttonWidth, bounds.Height - 2);
    }

    protected virtual void DrawMe()
    {
      if (this.DrawvButton == null)
        return;
      this.DrawvButton((object) this, EventArgs.Empty);
    }

    protected override void OnPaint(PaintEventArgs p)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(p);
      this.DrawMe();
    }

    protected Rectangle InnerRect(Rectangle bounds)
    {
      Rectangle rectangle = this.PerformBoundsCalculations(bounds);
      return new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2 - rectangle.Width, bounds.Height - 2);
    }

    protected override Rectangle InnerRect()
    {
      Rectangle rectangle1 = this.InnerRect(this.ClientRectangle);
      Rectangle rectangle2 = this.PerformBoundsCalculations(this.ClientRectangle);
      rectangle1.Width -= 2;
      rectangle2.Width += 2;
      if (this.RightToLeft == RightToLeft.No)
      {
        rectangle1.X += 2;
        return rectangle1;
      }
      rectangle1.X += rectangle2.Width;
      return rectangle1;
    }

    protected virtual void OnButtonClick(EventArgs e)
    {
      if (this.vButtonClick == null)
        return;
      this.vButtonClick((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (!this.Hover)
        return;
      this.OnButtonClick(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.Focus();
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.Invalidate();
    }
  }
}
