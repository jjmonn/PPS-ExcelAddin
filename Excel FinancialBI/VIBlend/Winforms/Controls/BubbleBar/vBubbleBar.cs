// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vBubbleBar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vBubbleBar control</summary>
  /// <remarks>
  /// A vBubbleBar can host and arrange multiple rectangular controls (usually buttons with images) in an animated selection list.
  /// </remarks>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Arranges multiple rectangular controls (usually buttons) in an animated selection list.")]
  [ToolboxBitmap(typeof (vBubbleBar), "ControlIcons.vBubbleBar.ico")]
  [ToolboxItem(true)]
  public class vBubbleBar : Panel, IScrollableControlBase
  {
    private SizeF scale = new SizeF(1f, 1f);
    private SizeF hoveredControlScale = new SizeF(0.4f, 0.4f);
    private Hashtable table = new Hashtable();
    private Hashtable wantedSizes = new Hashtable();
    private Timer timer = new Timer();
    private Timer upDownTimer = new Timer();
    private int itemWidth = 50;
    private int itemPadding = 6;
    private int frames = 2;
    private int currentOffset = -1;
    private Timer boundsTimer = new Timer();
    private bool autoAdjustBounds = true;
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Control hoveredControl;
    private BackgroundElement backFill;
    private bool animating;
    private bool bounceAnimating;
    private int step;
    private Control selectedControl;
    private int currentStep;
    private int currentFrame;
    private bool paintFill;
    private bool paintBorder;
    private bool isAnimating;
    private BubbleBarPosition position;
    private AnimationManager manager;
    private ControlTheme theme;

    /// <summary>Gets or sets the items position.</summary>
    /// <value>The items position.</value>
    [Description("Gets or sets the items position. Available postions are Near, Far and Center.")]
    [Category("Behavior")]
    public BubbleBarPosition ItemsPosition
    {
      get
      {
        return this.position;
      }
      set
      {
        this.position = value;
        this.PerformLayout();
      }
    }

    /// <summary>
    /// Determines the space in pixels between the items of the bubble bar
    /// </summary>
    [DefaultValue(6)]
    [Category("Appearance")]
    public int ItemsSpacing
    {
      get
      {
        return this.itemPadding;
      }
      set
      {
        this.itemPadding = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of an item.</summary>
    /// <remarks>
    /// vBubbleBar control may contain multiple items. All items have equal width and height.
    /// </remarks>
    [Description("Gets or sets the BubbleBar Items Size.")]
    [Category("Behavior")]
    public int ItemsSize
    {
      get
      {
        return this.itemWidth;
      }
      set
      {
        this.itemWidth = value;
        this.wantedSizes.Clear();
        this.table.Clear();
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          this.table.Add((object) control, (object) new Size(this.ItemsSize, this.ItemsSize));
          this.wantedSizes.Add((object) control, (object) new Size(this.ItemsSize, this.ItemsSize));
        }
        this.PerformLayout();
      }
    }

    /// <summary>
    /// Determines whether to fill the background of the panel control
    /// </summary>
    [Description("Determines whether to fill the background of the panel control")]
    [Category("Behavior")]
    public bool BackgroundFillEnabled
    {
      get
      {
        return this.paintFill;
      }
      set
      {
        this.paintFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the panel's border
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to display the border of the Panel control")]
    public bool DisplayBorder
    {
      get
      {
        return this.paintBorder;
      }
      set
      {
        this.paintBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Checks if the control is currently animating</summary>
    [Browsable(false)]
    public bool IsAnimating
    {
      get
      {
        return this.isAnimating;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to auto adjust control bounds
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to auto adjust control bounds")]
    public bool AutoAdjustBounds
    {
      get
      {
        return this.autoAdjustBounds;
      }
      set
      {
        this.autoAdjustBounds = value;
        this.Invalidate();
      }
    }

    /// <exclude />
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
    [Browsable(false)]
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

    /// <summary>Gets or sets the theme of the control</summary>
    [Description("Gets or sets the theme of the control")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = value;
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

    /// <summary>Occurs when a control is clicked</summary>
    [Category("Action")]
    public event EventHandler ControlClicked;

    /// <summary>Occurs when animation has finished</summary>
    [Category("Action")]
    public event EventHandler AnimationFinished;

    /// <summary>Occurs when animation has started</summary>
    [Category("Action")]
    public event EventHandler AnimationStarted;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vBubbleBar" /> class.
    /// </summary>
    public vBubbleBar()
    {
      this.ControlAdded += new ControlEventHandler(this.MyControl_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(this.MyControl_ControlRemoved);
      this.timer.Interval = 1;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.upDownTimer.Interval = 1;
      this.upDownTimer.Tick += new EventHandler(this.upDownTimer_Tick);
      this.SetStyle(ControlStyles.ResizeRedraw, false);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
      this.boundsTimer.Tick += new EventHandler(this.boundsTimer_Tick);
      this.boundsTimer.Interval = 1500;
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.boundsTimer.Dispose();
        this.upDownTimer.Dispose();
        this.timer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void boundsTimer_Tick(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position) || this.animating || this.bounceAnimating)
        return;
      this.ResetControl();
      this.boundsTimer.Stop();
      this.isAnimating = false;
      this.OnAnimationFinished();
      this.hoveredControl = (Control) null;
    }

    /// <summary>Raises the ControlClicked event</summary>
    protected virtual void OnControlClicked(Control control)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      if (this.ControlClicked == null)
        return;
      this.ControlClicked((object) control, EventArgs.Empty);
    }

    /// <summary>Raises the AnimationStarted event</summary>
    protected virtual void OnAnimationStarted()
    {
      if (this.AnimationStarted == null)
        return;
      this.AnimationStarted((object) this, EventArgs.Empty);
    }

    /// <summary>Raises the AnimationFinished event</summary>
    protected virtual void OnAnimationFinished()
    {
      if (this.AnimationFinished == null)
        return;
      this.AnimationFinished((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.backFill.Bounds = this.ClientRectangle;
      int width = 0;
      int num = 0;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        num = Math.Max(this.Controls[index].Size.Height + this.itemPadding, num);
        width += this.Controls[index].Size.Width + this.itemPadding;
      }
      if (this.BackgroundFillEnabled)
      {
        this.backFill.Bounds = new Rectangle(0, 0, width, num);
        this.backFill.DrawElementFill(e.Graphics, ControlState.Normal);
      }
      this.backFill.Bounds = new Rectangle(0, 0, width - 1, num - 1);
      if (this.DisplayBorder)
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      if (!this.DesignMode)
        return;
      e.Graphics.DrawString("Drag and Drop controls into the client area.", this.Font, Brushes.Black, (RectangleF) this.ClientRectangle, new StringFormat()
      {
        LineAlignment = StringAlignment.Center,
        Alignment = StringAlignment.Center
      });
    }

    /// <summary>
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (!this.animating && !this.bounceAnimating)
      {
        Point point = Point.Empty;
        int num1 = 0;
        switch (this.ItemsPosition)
        {
          case BubbleBarPosition.Near:
            point = Point.Empty;
            break;
          case BubbleBarPosition.Center:
            int num2 = 0;
            for (int index = 0; index < this.Controls.Count / 2; ++index)
            {
              Control control = this.Controls[index];
              num1 += this.ItemsSize;
              num2 += control.Width;
            }
            int num3 = num2 - num1;
            point = new Point(num1 - num3 / 2 + this.Controls.Count / 2 * this.ItemsSpacing, 0);
            break;
          case BubbleBarPosition.Far:
            foreach (Control control in (ArrangedElementCollection) this.Controls)
              num1 += control.Width;
            point = new Point(this.Width - num1 - this.Controls.Count * this.ItemsSpacing, 0);
            break;
        }
        if (this.wantedSizes.Count == this.Controls.Count)
        {
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            Size size = (Size) this.wantedSizes[(object) control];
            control.Size = size;
            control.Location = point;
            point = new Point(point.X + control.Size.Width + this.itemPadding, point.Y);
          }
        }
      }
      base.OnLayout(levent);
    }

    /// <summary>Handles the Tick event of the timer control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void timer_Tick(object sender, EventArgs e)
    {
      Point point = Point.Empty;
      int num1 = 0;
      switch (this.ItemsPosition)
      {
        case BubbleBarPosition.Near:
          point = Point.Empty;
          break;
        case BubbleBarPosition.Center:
          int num2 = 0;
          for (int index = 0; index < this.Controls.Count / 2; ++index)
          {
            Control control = this.Controls[index];
            num1 += this.ItemsSize;
            num2 += control.Width;
          }
          int num3 = num2 - num1;
          point = new Point(num1 - num3 / 2 + this.Controls.Count / 2 * this.ItemsSpacing, 0);
          break;
        case BubbleBarPosition.Far:
          foreach (Control control in (ArrangedElementCollection) this.Controls)
            num1 += control.Width;
          point = new Point(this.Width - num1 - this.Controls.Count * this.ItemsSpacing, 0);
          break;
      }
      this.step = 1;
      Size size1 = new Size(this.Width, 0);
      if (this.BackgroundFillEnabled || this.DisplayBorder)
        this.Invalidate();
      if (this.wantedSizes.Count != this.Controls.Count)
        return;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        Size size2 = (Size) this.wantedSizes[(object) control];
        Size size3 = (Size) this.table[(object) control];
        int width = control.Size.Width + this.step;
        int height = control.Size.Height + this.step;
        if (control.Size.Width >= size2.Width)
        {
          if (control.Size.Width == size2.Width)
          {
            width = size2.Width;
          }
          else
          {
            width = control.Size.Width - this.step;
            if (width < size3.Width)
              width = size3.Width;
          }
        }
        if (control.Size.Height >= size2.Height)
        {
          if (control.Size.Height == size2.Height)
          {
            height = size2.Height;
          }
          else
          {
            height = control.Size.Height - this.step;
            if (height < size3.Height)
              height = size3.Height;
          }
        }
        control.Size = new Size(width, height);
        control.Location = point;
        point = new Point(point.X + control.Size.Width + this.itemPadding, point.Y);
      }
      int num4 = 0;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        Size size2 = (Size) this.wantedSizes[(object) control];
        size1.Height = Math.Max(size1.Height, size2.Height);
        if (control.Size == size2)
          ++num4;
      }
      size1.Height += 10;
      if (num4 == this.Controls.Count)
      {
        this.timer.Stop();
        this.animating = false;
        this.isAnimating = false;
        this.OnAnimationFinished();
        this.PerformLayout();
        this.step = 0;
      }
      if (!this.AutoAdjustBounds)
        return;
      this.Size = size1;
    }

    private void MyControl_ControlRemoved(object sender, ControlEventArgs e)
    {
      e.Control.MouseMove -= new MouseEventHandler(this.Control_MouseMove);
      e.Control.MouseLeave -= new EventHandler(this.Control_MouseLeave);
      e.Control.MouseClick -= new MouseEventHandler(this.Control_MouseClick);
      this.table.Remove((object) e.Control);
      this.wantedSizes.Remove((object) e.Control);
    }

    private void MyControl_ControlAdded(object sender, ControlEventArgs e)
    {
      e.Control.MouseMove += new MouseEventHandler(this.Control_MouseMove);
      e.Control.MouseLeave += new EventHandler(this.Control_MouseLeave);
      e.Control.MouseClick += new MouseEventHandler(this.Control_MouseClick);
      this.table.Add((object) e.Control, (object) new Size(this.ItemsSize, this.ItemsSize));
      this.wantedSizes.Add((object) e.Control, (object) new Size(this.ItemsSize, this.ItemsSize));
    }

    private void Control_MouseLeave(object sender, EventArgs e)
    {
      if (!this.bounceAnimating && !this.animating)
        this.ResetControl();
      if (this.boundsTimer.Enabled)
        return;
      this.boundsTimer.Start();
      this.isAnimating = true;
      this.OnAnimationStarted();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.bounceAnimating || this.animating)
        return;
      this.ResetControl();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.boundsTimer.Start();
      this.isAnimating = true;
      this.OnAnimationStarted();
    }

    /// <summary>Resets the control.</summary>
    private void ResetControl()
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position) || this.Controls.Count <= 0 || this.animating)
        return;
      this.wantedSizes.Clear();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        this.wantedSizes.Add((object) control, (object) (Size) this.table[(object) control]);
      this.hoveredControl = this.Controls[0];
      this.animating = true;
      this.timer.Start();
      this.isAnimating = true;
      this.OnAnimationStarted();
    }

    /// <summary>Calculates the bounds.</summary>
    private void CalculateBounds()
    {
      if (this.hoveredControl != null)
      {
        this.wantedSizes.Clear();
        int num1 = this.Controls.IndexOf(this.hoveredControl);
        if (num1 >= 0)
        {
          int num2 = 0;
          for (int index = num1 - 1; index >= 0; --index)
          {
            ++num2;
            float num3 = this.hoveredControlScale.Width - 0.1f * (float) num2;
            if ((double) num3 < 0.0)
              num3 = 0.0f;
            Control control = this.Controls[index];
            Size size = (Size) this.table[(object) control];
            float num4 = (float) size.Width + (float) size.Width * num3;
            float num5 = (float) size.Height + (float) size.Height * num3;
            this.wantedSizes.Add((object) control, (object) new Size((int) num4, (int) num5));
          }
          int num6 = 0;
          for (int index = num1; index < this.Controls.Count; ++index)
          {
            ++num6;
            float num3 = this.hoveredControlScale.Width - 0.1f * (float) num6;
            if (index == num1)
            {
              num3 = this.hoveredControlScale.Width;
              --num6;
            }
            if ((double) num3 < 0.0)
              num3 = 0.0f;
            Control control = this.Controls[index];
            Size size = (Size) this.table[(object) control];
            float num4 = (float) size.Width + (float) size.Width * num3;
            float num5 = (float) size.Height + (float) size.Height * num3;
            this.wantedSizes.Add((object) control, (object) new Size((int) num4, (int) num5));
          }
        }
      }
      else
      {
        this.wantedSizes.Clear();
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          this.wantedSizes.Add((object) control, (object) (Size) this.table[(object) control]);
      }
      this.animating = true;
      this.timer.Start();
      this.isAnimating = true;
      this.OnAnimationStarted();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
    }

    private void Control_MouseClick(object sender, MouseEventArgs e)
    {
      Control control = sender as Control;
      this.selectedControl = control;
      this.upDownTimer.Start();
      this.isAnimating = true;
      this.OnAnimationStarted();
      if (this.AutoAdjustBounds)
        this.Height = this.Height + this.Height / 2;
      this.bounceAnimating = true;
      this.OnControlClicked(control);
    }

    private void upDownTimer_Tick(object sender, EventArgs e)
    {
      if (this.BackgroundFillEnabled || this.DisplayBorder)
        this.Invalidate();
      ++this.currentStep;
      Rectangle rectangle1 = new Rectangle(Point.Empty, (Size) this.wantedSizes[(object) this.selectedControl]);
      Rectangle rectangle2 = this.selectedControl.Bounds;
      if (this.currentOffset == -1)
        this.currentOffset = (this.Size.Height - rectangle2.Bottom) / 2;
      int val1 = this.Size.Height - this.currentOffset;
      if (this.currentFrame % 2 == 0)
      {
        if (rectangle2.Bottom < val1)
        {
          rectangle2 = new Rectangle(rectangle2.X, Math.Min(val1, rectangle2.Y + this.currentStep), rectangle2.Width, rectangle2.Height);
        }
        else
        {
          ++this.currentFrame;
          this.currentStep = 0;
          this.currentOffset = -1;
        }
      }
      else if (rectangle2.Top > rectangle1.Top)
      {
        rectangle2 = new Rectangle(rectangle2.X, Math.Max(0, rectangle2.Y - this.currentStep), rectangle2.Width, rectangle2.Height);
      }
      else
      {
        ++this.currentFrame;
        this.currentStep = 0;
        this.currentOffset = -1;
      }
      if (this.currentFrame == this.frames)
      {
        this.currentStep = 0;
        this.currentFrame = 0;
        this.upDownTimer.Stop();
        this.isAnimating = false;
        this.OnAnimationFinished();
        if (this.AutoAdjustBounds)
        {
          int val2 = 0;
          foreach (Control control in (ArrangedElementCollection) this.Controls)
            val2 = Math.Max(control.Height, val2);
          this.Height = val2 + 10;
        }
        this.bounceAnimating = false;
        this.currentOffset = -1;
      }
      this.selectedControl.Bounds = rectangle2;
    }

    private void Control_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.bounceAnimating)
        return;
      Control control = sender as Control;
      if (this.hoveredControl != null && (double) this.hoveredControl.Size.Width != (double) this.scale.Width * (double) this.ItemsSize)
        this.hoveredControl = (Control) null;
      if (this.hoveredControl == control)
        return;
      this.hoveredControl = control;
      this.CalculateBounds();
    }
  }
}
