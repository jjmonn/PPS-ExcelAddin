// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.SplitterControl
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a splitter control.</summary>
  [Browsable(false)]
  [ToolboxItem(false)]
  public class SplitterControl : Control, IScrollableControlBase
  {
    private Point clickPoint = new Point();
    private Size initialSizeControl1 = new Size();
    private Size initialSizeControl2 = new Size();
    internal Size control1MinSize = new Size(20, 20);
    internal Size control2MinSize = new Size(20, 20);
    private Point lastPoint = Point.Empty;
    private Point rightPoint = Point.Empty;
    private Point leftPoint = Point.Empty;
    private Point bottomPoint = Point.Empty;
    private Point topPoint = Point.Empty;
    private bool allowAnimations = true;
    private string styleKey = "Splitter";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    internal Orientation orientation;
    private SplitContainerBase splitContainerBase;
    private vSizeType oldSizeTypeControl1;
    private vSizeType oldSizeTypeControl2;
    private BackgroundElement backFill;
    private ControlState currentState;
    private AnimationManager manager;
    protected ControlTheme theme;

    /// <summary>Gets the workspace layout.</summary>
    public SplitContainerBase SplitContainerBase
    {
      get
      {
        return this.splitContainerBase;
      }
      internal set
      {
        this.splitContainerBase = value;
      }
    }

    /// <exclude />
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.manager == null)
          this.manager = new AnimationManager((Control) this);
        return this.manager;
      }
      set
      {
        this.manager = value;
      }
    }

    /// <summary>Determines whether to use animations</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DefaultValue(false)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        if (this.backFill != null)
          this.backFill.IsAnimated = value;
        this.allowAnimations = value;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    [Browsable(false)]
    [DefaultValue("Button")]
    public virtual string StyleKey
    {
      get
      {
        return this.styleKey;
      }
      set
      {
        this.styleKey = value;
      }
    }

    /// <summary>Gets or sets the theme of the control</summary>
    [Description("Gets or sets the theme of the control")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    public virtual ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = !(this.StyleKey != "Splitter") ? ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme) : ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme, value);
        this.backFill.LoadTheme(this.theme);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes
    /// </summary>
    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        ControlTheme defaultTheme;
        try
        {
          defaultTheme = ControlTheme.GetDefaultTheme(this.defaultTheme);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          return;
        }
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>Gets or sets the control1.</summary>
    /// <value>The control1.</value>
    public Control Control1 { get; set; }

    /// <summary>Gets or sets the control2.</summary>
    /// <value>The control2.</value>
    public Control Control2 { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.SplitterControl" /> class.
    /// </summary>
    public SplitterControl()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.DesignMode)
        return;
      if (e.Button == MouseButtons.Left)
      {
        this.SplitContainerBase.isResizing = true;
        this.clickPoint = Cursor.Position;
        this.Capture = true;
        this.initialSizeControl1 = this.Control1.Size;
        this.initialSizeControl2 = this.Control2.Size;
        this.oldSizeTypeControl1 = this.SplitContainerBase.GetControlSize(this.Control1);
        this.oldSizeTypeControl2 = this.SplitContainerBase.GetControlSize(this.Control2);
        this.SplitContainerBase.SuspendLayout(true);
        foreach (Control layoutControl in this.SplitContainerBase.layoutControls)
        {
          if (this.SplitContainerBase.GetControlSize(layoutControl).UnitType == SizeUnitType.Star)
            this.SplitContainerBase.SetControlSize(layoutControl, new vSizeType(layoutControl.Size, SizeUnitType.Star));
        }
        this.SplitContainerBase.SetControlSize(this.Control1, new vSizeType(this.Control1.Size, SizeUnitType.Pixel));
        this.SplitContainerBase.SetControlSize(this.Control2, new vSizeType(this.Control2.Size, SizeUnitType.Pixel));
        this.SplitContainerBase.ResumeSuspendedLayout(false);
        this.SplitContainerBase.Refresh();
        if (this.orientation == Orientation.Horizontal)
          this.Cursor = Cursors.VSplit;
        else
          this.Cursor = Cursors.HSplit;
        this.currentState = ControlState.Pressed;
        this.Invalidate();
      }
      base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.currentState = ControlState.Normal;
      this.Invalidate();
      base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.DesignMode)
        return;
      Point position = Cursor.Position;
      if (this.orientation == Orientation.Horizontal)
        this.Cursor = Cursors.VSplit;
      else if (this.orientation == Orientation.Vertical)
        this.Cursor = Cursors.HSplit;
      this.currentState = ControlState.Hover;
      if (this.Capture)
      {
        this.currentState = ControlState.Pressed;
        this.Invalidate();
        this.SplitContainerBase.SuspendLayout(true);
        Point point1 = new Point(position.X - this.clickPoint.X, position.Y - this.clickPoint.Y);
        Point point2 = new Point(position.X - this.clickPoint.X, position.Y - this.clickPoint.Y);
        if (this.SplitContainerBase.Orientation == Orientation.Horizontal)
        {
          if (point1.X > SystemInformation.DragSize.Width)
          {
            if (position.X > this.lastPoint.X && this.Control2.Width <= this.control2MinSize.Width)
            {
              this.lastPoint = Cursor.Position;
              if (this.rightPoint.Equals((object) Point.Empty))
                this.rightPoint = Cursor.Position;
              base.OnMouseMove(e);
              this.SplitContainerBase.ResumeSuspendedLayout(true);
              return;
            }
            if (!this.rightPoint.Equals((object) Point.Empty) && position.X >= this.rightPoint.X)
            {
              this.lastPoint = Cursor.Position;
              base.OnMouseMove(e);
              this.SplitContainerBase.ResumeSuspendedLayout(true);
              return;
            }
            vSizeType sizeType = new vSizeType(new Size(this.initialSizeControl1.Width + point2.X, this.initialSizeControl1.Height), SizeUnitType.Pixel);
            this.SplitContainerBase.SetControlSize(this.Control2, new vSizeType(new Size(this.initialSizeControl2.Width - point2.X, this.initialSizeControl2.Height), SizeUnitType.Pixel));
            this.SplitContainerBase.SetControlSize(this.Control1, sizeType);
          }
          else if (point1.X < -SystemInformation.DragSize.Width)
          {
            if (position.X < this.lastPoint.X && this.Control1.Width <= this.control1MinSize.Width)
            {
              this.lastPoint = Cursor.Position;
              if (this.leftPoint.Equals((object) Point.Empty))
                this.leftPoint = Cursor.Position;
              base.OnMouseMove(e);
              this.SplitContainerBase.ResumeSuspendedLayout(true);
              return;
            }
            if (!this.leftPoint.Equals((object) Point.Empty) && position.X <= this.leftPoint.X)
            {
              this.lastPoint = Cursor.Position;
              base.OnMouseMove(e);
              this.SplitContainerBase.ResumeSuspendedLayout(true);
              return;
            }
            vSizeType sizeType1 = new vSizeType(new Size(this.initialSizeControl1.Width + point2.X, this.initialSizeControl1.Height), SizeUnitType.Pixel);
            vSizeType sizeType2 = new vSizeType(new Size(this.initialSizeControl2.Width - point2.X, this.initialSizeControl1.Height), SizeUnitType.Pixel);
            this.SplitContainerBase.SetControlSize(this.Control1, sizeType1);
            this.SplitContainerBase.SetControlSize(this.Control2, sizeType2);
          }
        }
        else if (point1.Y > SystemInformation.DragSize.Height)
        {
          if (position.Y > this.lastPoint.Y && this.Control2.Height <= this.control2MinSize.Height || this.initialSizeControl2.Height - point2.Y <= this.control2MinSize.Height)
          {
            this.lastPoint = Cursor.Position;
            if (this.bottomPoint.Equals((object) Point.Empty))
              this.bottomPoint = Cursor.Position;
            base.OnMouseMove(e);
            this.SplitContainerBase.ResumeSuspendedLayout(true);
            return;
          }
          if (!this.bottomPoint.Equals((object) Point.Empty) && position.Y >= this.bottomPoint.Y)
          {
            this.lastPoint = Cursor.Position;
            base.OnMouseMove(e);
            this.SplitContainerBase.ResumeSuspendedLayout(true);
            return;
          }
          vSizeType sizeType1 = new vSizeType(new Size(this.initialSizeControl1.Width, this.initialSizeControl1.Height + point2.Y), SizeUnitType.Pixel);
          vSizeType sizeType2 = new vSizeType(new Size(this.initialSizeControl2.Width, this.initialSizeControl2.Height - point2.Y), SizeUnitType.Pixel);
          this.SplitContainerBase.SetControlSize(this.Control1, sizeType1);
          this.SplitContainerBase.SetControlSize(this.Control2, sizeType2);
        }
        else if (point1.Y < -SystemInformation.DragSize.Height)
        {
          if (position.Y < this.lastPoint.Y && this.Control1.Height <= this.control1MinSize.Height || this.initialSizeControl1.Height + point2.Y <= this.control2MinSize.Height)
          {
            this.lastPoint = Cursor.Position;
            if (this.topPoint.Equals((object) Point.Empty))
              this.topPoint = Cursor.Position;
            base.OnMouseMove(e);
            this.SplitContainerBase.ResumeSuspendedLayout(true);
            return;
          }
          if (!this.topPoint.Equals((object) Point.Empty) && position.Y <= this.topPoint.Y)
          {
            this.lastPoint = Cursor.Position;
            base.OnMouseMove(e);
            this.SplitContainerBase.ResumeSuspendedLayout(true);
            return;
          }
          vSizeType sizeType1 = new vSizeType(new Size(this.initialSizeControl1.Width, this.initialSizeControl1.Height + point2.Y), SizeUnitType.Pixel);
          vSizeType sizeType2 = new vSizeType(new Size(this.initialSizeControl2.Width, this.initialSizeControl2.Height - point2.Y), SizeUnitType.Pixel);
          this.SplitContainerBase.SetControlSize(this.Control1, sizeType1);
          this.SplitContainerBase.SetControlSize(this.Control2, sizeType2);
        }
        this.SplitContainerBase.ResumeSuspendedLayout(true);
      }
      else
        this.Invalidate();
      this.lastPoint = Cursor.Position;
      base.OnMouseMove(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.DesignMode)
        return;
      base.OnMouseUp(e);
      this.currentState = !this.ClientRectangle.Contains(e.Location) ? ControlState.Normal : ControlState.Hover;
      this.Capture = false;
      this.rightPoint = this.leftPoint = this.bottomPoint = this.topPoint = Point.Empty;
      this.SplitContainerBase.isResizing = false;
      if (this.Control1 != null && this.Control2 != null)
      {
        this.SplitContainerBase.SuspendLayout(true);
        this.SplitContainerBase.SetControlSize(this.Control1, new vSizeType(new Size(this.Control1.Size.Width, this.Control1.Size.Height), this.oldSizeTypeControl1.UnitType));
        this.SplitContainerBase.SetControlSize(this.Control2, new vSizeType(new Size(this.Control2.Size.Width, this.Control2.Size.Height), this.oldSizeTypeControl2.UnitType));
        this.SplitContainerBase.ResumeSuspendedLayout(true);
      }
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.backFill.Bounds = this.ClientRectangle;
      if (!this.Enabled)
        this.currentState = ControlState.Disabled;
      if (this.orientation != Orientation.Vertical)
        this.backFill.DrawStandardFillWithCustomGradientOffsets(e.Graphics, this.currentState, GradientStyles.Linear, 0.0, 0.5, 0.5);
      else
        this.backFill.DrawStandardFillWithCustomGradientOffsets(e.Graphics, this.currentState, GradientStyles.Linear, 90.0, 0.5, 0.5);
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      using (Pen pen = new Pen(this.backFill.BorderColor))
      {
        if (this.orientation == Orientation.Vertical)
        {
          int num = this.Width / 2 - 8;
          for (int index = 0; index < 8; ++index)
          {
            e.Graphics.DrawLine(pen, (float) num, (float) (this.Height / 2), (float) num + 0.5f, (float) (this.Height / 2));
            e.Graphics.DrawLine(Pens.White, (float) (num + 1), (float) (this.Height / 2 + 1), (float) num + 1.5f, (float) (this.Height / 2 + 1));
            num += 2;
          }
        }
        else
        {
          int num = this.Height / 2 - 8;
          for (int index = 0; index < 8; ++index)
          {
            e.Graphics.DrawLine(pen, (float) (this.Width / 2), (float) num, (float) (this.Width / 2), (float) num + 0.5f);
            e.Graphics.DrawLine(Pens.White, (float) (this.Width / 2 + 1), (float) num + 1f, (float) (this.Width / 2 + 1), (float) num + 1.5f);
            num += 2;
          }
        }
      }
    }
  }
}
