// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vNumericUpDownBase
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  [ToolboxItem(false)]
  public class vNumericUpDownBase : vButtonEditBase
  {
    private Timer timer = new Timer();
    private SpinDirection direction;
    internal bool isDownPressed;
    internal bool isUpPressed;
    private int tickCount;

    [Browsable(false)]
    public bool IsDownRepeatButtonMouseOver
    {
      get
      {
        return this.RectangleToScreen(this.GetDownRepeatButtonBounds(this.ClientRectangle)).Contains(Cursor.Position);
      }
    }

    [Browsable(false)]
    public bool IsUpRepeatButtonMouseOver
    {
      get
      {
        return this.RectangleToScreen(this.GetUpRepeatButtonBounds(this.ClientRectangle)).Contains(Cursor.Position);
      }
    }

    /// <summary>Occurs when the down button is clicked.</summary>
    [Category("Behavior")]
    public event EventHandler DownRepeatButtonClick;

    /// <summary>Occurs when the up button is clicked.</summary>
    [Category("Behavior")]
    public event EventHandler UpRepeatButtonClick;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vNumericUpDownBase" /> class.
    /// </summary>
    public vNumericUpDownBase()
    {
      this.timer.Tick += new EventHandler(this.Tick);
      this.TextBox.TextAlign = HorizontalAlignment.Right;
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    protected virtual void HandleDownClick()
    {
    }

    protected virtual void HandleUpClick()
    {
    }

    protected Rectangle GetDownRepeatButtonBounds(Rectangle clientRect)
    {
      Rectangle rectangle = this.PerformBoundsCalculations(clientRect);
      int height = rectangle.Height / 2;
      return new Rectangle(rectangle.X - 1, rectangle.Y + height - 1, rectangle.Width, height);
    }

    protected Rectangle GetUpRepeatButtonBounds(Rectangle clientRect)
    {
      Rectangle rectangle = this.PerformBoundsCalculations(clientRect);
      int height = rectangle.Height / 2;
      return new Rectangle(rectangle.X - 1, rectangle.Y, rectangle.Width, height);
    }

    public void CallDownButtonClick(EventArgs e)
    {
      this.OnDownRepeatButtonClick(e);
    }

    public void CallUpButtonClick(EventArgs e)
    {
      this.OnUpRepeatButtonClick(e);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (!this.Hover)
        return;
      if (this.IsUpRepeatButtonMouseOver)
        this.OnUpRepeatButtonClick(e);
      if (this.IsDownRepeatButtonMouseOver)
        this.OnDownRepeatButtonClick(e);
      this.OnButtonClick(e);
    }

    protected virtual void OnDownRepeatButtonClick(EventArgs e)
    {
      if (this.DownRepeatButtonClick == null)
        return;
      this.DownRepeatButtonClick((object) this, e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if ((e.Button & MouseButtons.Left) <= MouseButtons.None)
        return;
      if (this.Hover)
      {
        if (this.IsUpRepeatButtonMouseOver)
        {
          this.direction = SpinDirection.Up;
          this.HandleUpClick();
        }
        if (this.IsDownRepeatButtonMouseOver)
        {
          this.direction = SpinDirection.Down;
          this.HandleDownClick();
        }
        this.tickCount = 0;
        this.timer.Interval = 300;
        this.timer.Enabled = true;
        this.isUpPressed = this.IsUpRepeatButtonMouseOver;
        this.isDownPressed = this.IsDownRepeatButtonMouseOver;
      }
      this.Focus();
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if ((e.Button & MouseButtons.Left) <= MouseButtons.None)
        return;
      this.isUpPressed = false;
      this.isDownPressed = false;
      this.timer.Enabled = false;
      this.Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (e.Delta > 0)
        this.HandleUpClick();
      else
        this.HandleDownClick();
    }

    protected virtual void OnUpRepeatButtonClick(EventArgs e)
    {
      if (this.UpRepeatButtonClick == null)
        return;
      this.UpRepeatButtonClick((object) this, e);
    }

    private void Tick(object sender, EventArgs e)
    {
      if (this.direction == SpinDirection.Up)
        this.HandleUpClick();
      else
        this.HandleDownClick();
      ++this.tickCount;
      if (this.tickCount > 2 && this.timer.Interval == 300)
        this.timer.Interval = 100;
      if (this.tickCount <= 10 || this.timer.Interval != 100)
        return;
      this.timer.Interval = 30;
    }
  }
}
