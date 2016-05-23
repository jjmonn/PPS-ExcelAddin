// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vScrollBar
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
  /// <summary>
  /// Represents a base class for the vHScrollBar and vVScrollBar controls
  /// </summary>
  [ToolboxItem(false)]
  public abstract class vScrollBar : ScrollableControlMiniBase
  {
    private bool drawScrollButtonsBorder = true;
    /// <exclude />
    protected int maxPos = 100;
    /// <exclude />
    protected int smallIncrement = 1;
    /// <exclude />
    protected int largeIncrement = 10;
    /// <exclude />
    protected bool isThumbVisible = true;
    internal int thumbHeight = 10;
    private int newDiff = -1;
    private PaintHelper helper = new PaintHelper();
    private bool scrollButtonsSemiRounded = true;
    private string styleKey = "ScrollBar";
    private int thumbThemeSize = -1;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    /// <exclude />
    protected int minPos;
    /// <exclude />
    protected int currentPos;
    /// <exclude />
    protected int thumbPos;
    /// <exclude />
    protected int targetOffset;
    private ScrollBarAction scrollBarAction;
    private bool isScrollRepeatOn;
    private Timer scrollTimer;
    /// <exclude />
    protected BackgroundElement scrollElement;
    /// <exclude />
    protected BackgroundElement thumbElement;
    /// <exclude />
    protected BackgroundElement buttonIncrement;
    /// <exclude />
    protected BackgroundElement buttonDecrement;
    private bool smartScrollEnabled;
    private int diff;
    private int scrollButtonsRoundedCornersRadius;
    private ControlTheme theme;
    private ControlTheme themeButton;
    private ControlTheme themeThumb;
    private ControlTheme themeScrollElement;

    /// <summary>Gets or sets whether smart scrolling is enabled</summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether the smart scrolling is enabled.")]
    public bool SmartScrollEnabled
    {
      get
      {
        return this.smartScrollEnabled;
      }
      set
      {
        this.smartScrollEnabled = value;
      }
    }

    /// <exclude />
    protected internal abstract Rectangle SmallDecrementRectangle { get; }

    /// <exclude />
    protected abstract Rectangle LargeDecrementRectangle { get; }

    /// <exclude />
    protected abstract Rectangle LargeIncrementRectangle { get; }

    /// <exclude />
    protected internal abstract Rectangle SmallIncrementRectangle { get; }

    /// <exclude />
    protected abstract ArrowDirection SmallDecrementArrowDirection { get; }

    /// <exclude />
    protected abstract ArrowDirection SmallIncrementArrowDirection { get; }

    /// <exclude />
    protected abstract int ThumbArea { get; }

    /// <summary>
    /// Gets or sets the minimum change value of the scroll bar.
    /// </summary>
    [Description("Gets or sets the minimum change value of the scroll bar.")]
    [Category("Behavior")]
    public int Minimum
    {
      get
      {
        return this.minPos;
      }
      set
      {
        if (value > this.maxPos)
          throw new ArgumentOutOfRangeException("Minimum", "Minimum cannot be greater than Maximum.");
        this.minPos = value;
        if (this.Value < this.minPos)
          this.Value = this.minPos;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the maximum change value of the scroll bar.
    /// </summary>
    [Description("Gets or sets the maximum change value of the scroll bar.")]
    [Category("Behavior")]
    public int Maximum
    {
      get
      {
        return this.maxPos;
      }
      set
      {
        if (value < this.minPos)
          throw new ArgumentOutOfRangeException("Maximum", "Maximum cannot be less than Minimum.");
        this.maxPos = value;
        if (this.Value > this.maxPos)
          this.Value = this.maxPos;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the current value of the scroll bar.</summary>
    [Category("Behavior")]
    [Description("Gets or sets the current value of the scroll bar.")]
    public int Value
    {
      get
      {
        return this.currentPos;
      }
      set
      {
        if (value < this.minPos)
          value = this.minPos;
        if (value > this.maxPos)
          value = this.maxPos;
        if (this.currentPos == value)
          return;
        this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, this.currentPos, value));
        this.currentPos = value;
        this.OnValueChanged(EventArgs.Empty);
        this.SyncThumbPositionWithLogicalValue();
      }
    }

    /// <summary>
    /// Gets or sets the small change value of the scroll bar.
    /// </summary>
    [Description("Gets or sets the small change value of the scroll bar.")]
    [Category("Behavior")]
    public int SmallChange
    {
      get
      {
        return this.smallIncrement;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("SmallChange", string.Format("Value must be in range [0,{0}].", (object) int.MaxValue));
        this.smallIncrement = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the large change value of the scroll bar.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the large change value of the scroll bar.")]
    public int LargeChange
    {
      get
      {
        return this.largeIncrement;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("LargeChange", string.Format("Value must be in range [0,{0}].", (object) int.MaxValue));
        this.largeIncrement = value;
        this.Invalidate();
      }
    }

    private int ThumbPosition
    {
      get
      {
        return this.thumbPos;
      }
      set
      {
        this.thumbPos = value;
        int thumbArea = this.ThumbArea;
        if (value < 0)
          this.thumbPos = 0;
        if (value > thumbArea)
          this.thumbPos = thumbArea;
        int num = this.maxPos - this.minPos;
        if (num > 0)
          this.Value = (int) ((double) this.thumbPos / (double) thumbArea * (double) num);
        else
          this.Value = this.minPos;
      }
    }

    /// <exclude />
    protected int ScrollAreaLength
    {
      get
      {
        return !(this is vHScrollBar) ? this.ClientRectangle.Size.Height - this.SmallIncrementRectangle.Height - this.SmallDecrementRectangle.Height : this.ClientRectangle.Size.Width - this.SmallIncrementRectangle.Width - this.SmallDecrementRectangle.Width;
      }
    }

    /// <exclude />
    protected Rectangle ThumbRectangle
    {
      get
      {
        Rectangle decrementRectangle = this.SmallDecrementRectangle;
        int thumbLength = this.GetThumbLength();
        if (this.thumbThemeSize != -1)
        {
          if (this is vHScrollBar)
            return new Rectangle(decrementRectangle.Width + this.thumbPos, this.ClientRectangle.Height / 2 - this.thumbThemeSize / 2, thumbLength, this.thumbThemeSize);
          return new Rectangle(this.ClientRectangle.Width / 2 - this.thumbThemeSize / 2, decrementRectangle.Bottom + this.thumbPos, this.thumbThemeSize, thumbLength);
        }
        if (this is vHScrollBar)
          return new Rectangle(decrementRectangle.Width + this.thumbPos, 0, thumbLength, this.ClientRectangle.Height - 1);
        return new Rectangle(0, decrementRectangle.Bottom + this.thumbPos, this.ClientRectangle.Width - 1, thumbLength);
      }
    }

    /// <exclude />
    protected bool MouseHover
    {
      get
      {
        return this.ClientRectangle.Contains(this.PointToClient(Cursor.Position));
      }
    }

    /// <exclude />
    protected bool SmallIncrementHover
    {
      get
      {
        return this.SmallIncrementRectangle.Contains(this.PointToClient(Cursor.Position));
      }
    }

    /// <exclude />
    protected bool SmallDecrementHover
    {
      get
      {
        return this.SmallDecrementRectangle.Contains(this.PointToClient(Cursor.Position));
      }
    }

    /// <exclude />
    protected bool ThumbHover
    {
      get
      {
        return this.ThumbRectangle.Contains(this.PointToClient(Cursor.Position));
      }
    }

    /// <summary>
    /// Gets or sets the rounded corners radius of the scroll bar buttons.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the rounded corners radius of the scroll bar buttons.")]
    public int ScrollButtonsRoundedCornersRadius
    {
      get
      {
        return this.scrollButtonsRoundedCornersRadius;
      }
      set
      {
        if (value < 0)
          return;
        this.scrollButtonsRoundedCornersRadius = Math.Min(10, value);
        this.buttonIncrement.Radius = this.scrollButtonsRoundedCornersRadius;
        this.buttonDecrement.Radius = this.scrollButtonsRoundedCornersRadius;
        this.scrollElement.Radius = this.scrollButtonsRoundedCornersRadius;
      }
    }

    /// <summary>
    /// Gets or sets whether the scrollbar buttons appear semirounded.
    /// </summary>
    [Description("Gets or sets whether the scrollbar buttons appear semirounded.")]
    [Category("Appearance")]
    public bool ScrollButtonsSemiRounded
    {
      get
      {
        return this.scrollButtonsSemiRounded;
      }
      set
      {
        this.scrollButtonsSemiRounded = value;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    protected internal virtual string StyleKey
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null || this.theme == value)
          return;
        this.theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        this.themeButton = ThemeCache.GetTheme(this.StyleKey + "1", this.VIBlendTheme);
        this.themeThumb = ThemeCache.GetTheme(this.StyleKey + "2", this.VIBlendTheme);
        this.themeScrollElement = ThemeCache.GetTheme(this.StyleKey + "3", this.VIBlendTheme);
        string s1 = this.theme.QueryNamedValueSetter("ScrollBarThumbCornersRadius");
        if (s1 != string.Empty)
        {
          int result = (int) this.themeThumb.Radius;
          if (int.TryParse(s1, out result))
            this.themeThumb.Radius = (float) result;
        }
        string str = this.theme.QueryNamedValueSetter("ScrollBarShowButtonBorder");
        if (str != string.Empty && !bool.TryParse(str, out this.drawScrollButtonsBorder))
          this.drawScrollButtonsBorder = true;
        Color color1 = this.theme.QueryColorSetter("ScrollBarBorderNormal");
        if (color1 != Color.Empty)
          this.themeScrollElement.StyleNormal.BorderColor = color1;
        Color color2 = this.theme.QueryColorSetter("ScrollBorderNormal");
        if (color2 != Color.Empty)
        {
          this.themeButton.StyleNormal.BorderColor = color2;
          this.themeThumb.StyleNormal.BorderColor = color2;
        }
        Color color3 = this.theme.QueryColorSetter("ScrollBorderHighlight");
        if (color3 != Color.Empty)
        {
          this.themeButton.StyleHighlight.BorderColor = color3;
          this.themeThumb.StyleHighlight.BorderColor = color3;
        }
        Color color4 = this.theme.QueryColorSetter("ScrollBorderPressed");
        if (color4 != Color.Empty)
        {
          this.themeButton.StylePressed.BorderColor = color4;
          this.themeThumb.StylePressed.BorderColor = color4;
        }
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("ScrollNormal");
        if (fillStyle1 != null)
        {
          this.themeButton.StyleNormal.FillStyle = fillStyle1;
          this.themeThumb.StyleNormal.FillStyle = fillStyle1;
        }
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("ScrollHighlight");
        if (fillStyle2 != null)
        {
          this.themeButton.StyleHighlight.FillStyle = fillStyle2;
          this.themeThumb.StyleHighlight.FillStyle = fillStyle2;
        }
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("ScrollPressed");
        if (fillStyle3 != null)
        {
          this.themeButton.StylePressed.FillStyle = fillStyle3;
          this.themeThumb.StylePressed.FillStyle = fillStyle3;
        }
        FillStyle fillStyle4 = this.theme.QueryFillStyleSetter("ScrollButtonNormal");
        if (fillStyle4 != null)
          this.themeButton.StyleNormal.FillStyle = fillStyle4;
        FillStyle fillStyle5 = this.theme.QueryFillStyleSetter("ScrollButtonHighlight");
        if (fillStyle5 != null)
          this.themeButton.StyleHighlight.FillStyle = fillStyle5;
        FillStyle fillStyle6 = this.theme.QueryFillStyleSetter("ScrollElementNormal");
        if (fillStyle6 != null)
          this.themeScrollElement.StyleNormal.FillStyle = fillStyle6;
        this.thumbThemeSize = -1;
        string s2 = this.theme.QueryNamedValueSetter("ScrollBarThumbSize");
        int result1;
        if (s2 != string.Empty && int.TryParse(s2, out result1))
          this.thumbThemeSize = result1;
        if (this is vVScrollBar)
        {
          this.theme.SetFillStyleGradientAngle(0);
          this.themeButton.SetFillStyleGradientAngle(0);
          this.themeThumb.SetFillStyleGradientAngle(0);
          this.themeScrollElement.SetFillStyleGradientAngle(0);
        }
        this.buttonIncrement.LoadTheme(this.themeButton);
        this.buttonDecrement.LoadTheme(this.themeButton);
        this.thumbElement.LoadTheme(this.themeThumb);
        this.scrollElement.LoadTheme(this.themeScrollElement);
        this.buttonDecrement.Radius = this.scrollButtonsRoundedCornersRadius;
        this.buttonIncrement.Radius = this.scrollButtonsRoundedCornersRadius;
        this.scrollElement.Radius = this.scrollButtonsRoundedCornersRadius;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
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
        if (this.defaultTheme == value)
          return;
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

    /// <summary>
    /// Occurs when the thumb of the scroll bar has been moved by either a mouse or keyboard action.
    /// </summary>
    public event ScrollEventHandler Scroll;

    /// <summary>
    /// Occurs when the Value property is changed, either by a Scroll event or programmatically.
    /// </summary>
    public event EventHandler ValueChanged;

    /// <summary>Constructor</summary>
    public vScrollBar()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.CreateScrollElements();
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.AllowAnimations = true;
    }

    public void SetDiffFactor(int value)
    {
      this.newDiff = value;
    }

    public int GetDiffFactor()
    {
      this.diff = this.Maximum - this.Minimum;
      if (this.newDiff != -1)
        this.diff = this.newDiff;
      return this.diff;
    }

    /// <exclude />
    protected int GetThumbLength()
    {
      this.diff = this.GetDiffFactor();
      int val1 = 0;
      if (this.diff > 1)
        val1 = (int) ((double) this.ScrollAreaLength / (double) (this.diff + this.ScrollAreaLength) * (double) this.ScrollAreaLength);
      else if (this.diff == 1)
        val1 = this.ScrollAreaLength;
      if (val1 < 10)
        val1 = 10;
      return Math.Min(val1, this.ScrollAreaLength);
    }

    internal void SyncThumbPositionWithLogicalValue()
    {
      double num = (double) (this.maxPos - this.minPos);
      this.thumbPos = num <= 0.0 ? 0 : (int) ((double) this.currentPos / num * (double) this.ThumbArea);
      this.Invalidate();
    }

    private Color GetArrowColor(ControlState state)
    {
      Color color = this.theme.QueryColorSetter("ColorScrollButtonArrow");
      if (color == Color.Empty)
      {
        color = this.themeButton.StyleNormal.BorderColor;
        if (state == ControlState.Pressed)
          color = this.themeButton.StylePressed.BorderColor;
        else if (state == ControlState.Hover)
          color = this.themeButton.StyleHighlight.BorderColor;
        else if (state == ControlState.Disabled)
          color = this.themeButton.StyleDisabled.BorderColor;
      }
      return color;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.ClientRectangle.Width == 0 || (this.ClientRectangle.Height == 0 || this.scrollElement == null || (this.buttonIncrement == null || this.buttonDecrement == null) || this.thumbElement == null))
        return;
      ControlState stateType = this.Enabled ? ControlState.Normal : ControlState.Disabled;
      Rectangle clientRectangle = this.ClientRectangle;
      --clientRectangle.Width;
      --clientRectangle.Height;
      this.scrollElement.Bounds = new Rectangle(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height);
      this.scrollElement.DrawElementFill(e.Graphics, stateType);
      this.scrollElement.DrawElementBorder(e.Graphics, stateType);
      ControlState controlState = this.Enabled ? ControlState.Normal : ControlState.Disabled;
      if (this.MouseHover && controlState == ControlState.Normal)
        controlState = ControlState.Hover;
      int num1 = this is vVScrollBar ? this.Width : this.Height;
      int num2 = (int) (0.5 * (double) num1);
      int width = num1 - num2;
      if (width % 2 != 0)
        --width;
      int height = width / 2;
      if (this is vHScrollBar)
      {
        int num3 = width;
        width = height;
        height = num3;
      }
      int num4 = this.Enabled ? 1 : 0;
      Rectangle incrementRectangle = this.SmallIncrementRectangle;
      --incrementRectangle.Width;
      --incrementRectangle.Height;
      this.buttonIncrement.Bounds = incrementRectangle;
      bool flag1 = this.buttonIncrement.Bounds.Contains(this.PointToClient(Cursor.Position));
      if (controlState == ControlState.Hover && flag1)
      {
        FillStyle fillStyle = this.themeButton.StyleHighlight.FillStyle;
        this.themeButton.StyleHighlight.FillStyle = this.themeThumb.StyleHighlight.FillStyle;
        this.buttonIncrement.DrawElementFill(e.Graphics, controlState);
        this.themeButton.StyleHighlight.FillStyle = fillStyle;
      }
      else
        this.buttonIncrement.DrawElementFill(e.Graphics, controlState);
      if (this.drawScrollButtonsBorder || controlState != ControlState.Normal)
      {
        if (controlState == ControlState.Hover && !flag1)
          this.buttonIncrement.DrawElementBorder(e.Graphics, ControlState.Normal);
        else
          this.buttonIncrement.DrawElementBorder(e.Graphics, controlState);
      }
      Rectangle bounds1 = new Rectangle(this.SmallIncrementRectangle.X + (this.SmallIncrementRectangle.Width - width) / 2, this.SmallIncrementRectangle.Y + (this.SmallIncrementRectangle.Height - height) / 2, width, height);
      this.helper.DrawArrowFigure(e.Graphics, this.GetArrowColor(controlState), bounds1, this.SmallIncrementArrowDirection);
      Rectangle rectangle = new Rectangle(0, 0, this.SmallDecrementRectangle.Width, this.SmallDecrementRectangle.Height);
      --rectangle.Width;
      --rectangle.Height;
      this.buttonDecrement.Bounds = rectangle;
      bool flag2 = this.buttonDecrement.Bounds.Contains(this.PointToClient(Cursor.Position));
      if (controlState == ControlState.Hover && flag2)
      {
        FillStyle fillStyle = this.themeButton.StyleHighlight.FillStyle;
        this.themeButton.StyleHighlight.FillStyle = this.themeThumb.StyleHighlight.FillStyle;
        this.buttonDecrement.DrawElementFill(e.Graphics, controlState);
        this.themeButton.StyleHighlight.FillStyle = fillStyle;
      }
      else
        this.buttonDecrement.DrawElementFill(e.Graphics, controlState);
      if (this.drawScrollButtonsBorder || controlState != ControlState.Normal)
      {
        if (controlState == ControlState.Hover && !flag2)
          this.buttonDecrement.DrawElementBorder(e.Graphics, ControlState.Normal);
        else
          this.buttonDecrement.DrawElementBorder(e.Graphics, controlState);
      }
      Rectangle bounds2 = new Rectangle(this.SmallDecrementRectangle.X + (this.SmallDecrementRectangle.Width - width) / 2, this.SmallDecrementRectangle.Y + (this.SmallDecrementRectangle.Height - height) / 2, width, height);
      this.helper.DrawArrowFigure(e.Graphics, this.GetArrowColor(controlState), bounds2, this.SmallDecrementArrowDirection);
      if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
        this.scrollElement.DrawElementBorder(e.Graphics, stateType);
      this.DrawThumb(e.Graphics);
    }

    private void DrawThumb(Graphics g)
    {
      if (!this.isThumbVisible)
        return;
      ControlState stateType = ControlState.Normal;
      if (this.RectangleToScreen(this.ThumbRectangle).Contains(Cursor.Position))
        stateType = ControlState.Hover;
      if (this.Capture && (!this.RectangleToScreen(this.SmallIncrementRectangle).Contains(Cursor.Position) && this.scrollBarAction == ScrollBarAction.ThumbTrack) && !this.RectangleToScreen(this.SmallDecrementRectangle).Contains(Cursor.Position))
        stateType = ControlState.Hover;
      Rectangle thumbRectangle = this.ThumbRectangle;
      Pen pen1 = new Pen(this.thumbElement.BorderColor);
      if (!this.Enabled)
      {
        stateType = ControlState.Disabled;
        pen1 = new Pen(this.thumbElement.DisabledBorderColor);
      }
      if (this is vVScrollBar)
        --thumbRectangle.Height;
      else
        --thumbRectangle.Width;
      this.thumbElement.Bounds = thumbRectangle;
      this.thumbElement.DrawElementFill(g, stateType);
      this.thumbElement.DrawElementBorder(g, stateType);
      Color color1 = this.Theme.QueryColorSetter("ScrollGrip");
      Color color2 = this.Theme.QueryColorSetter("ScrollGrip2");
      if (color1 != Color.Empty)
        pen1.Color = color1;
      Pen pen2 = (Pen) null;
      if (color2 != Color.Empty)
        pen2 = new Pen(color2);
      if (this is vVScrollBar)
      {
        if (thumbRectangle.Height <= 20 || thumbRectangle.Width <= 10)
          return;
        int num = thumbRectangle.Y + (thumbRectangle.Height - 8) / 2;
        for (int index = 0; index < 4; ++index)
        {
          g.DrawLine(pen1, thumbRectangle.X + 4, num, thumbRectangle.X + 4 + thumbRectangle.Width - 8, num);
          if (pen2 != null)
            g.DrawLine(pen2, thumbRectangle.X + 6, num + 1, thumbRectangle.X + 4 + thumbRectangle.Width - 8, num + 1);
          num += 2;
        }
      }
      else
      {
        if (thumbRectangle.Width <= 20 || thumbRectangle.Height <= 10)
          return;
        int num = thumbRectangle.X + (thumbRectangle.Width - 8) / 2;
        for (int index = 0; index < 4; ++index)
        {
          g.DrawLine(pen1, num, thumbRectangle.Y + 4, num, thumbRectangle.Y + 4 + thumbRectangle.Height - 8);
          if (pen2 != null)
            g.DrawLine(pen2, num + 1, thumbRectangle.Y + 6, num + 1, thumbRectangle.Y + 4 + thumbRectangle.Height - 8);
          num += 2;
        }
      }
    }

    /// <exclude />
    protected void CreateScrollElements()
    {
      if (this.scrollElement == null)
      {
        this.scrollElement = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.scrollElement.Owner = "ScrollBar";
      }
      if (this.thumbElement == null)
      {
        this.thumbElement = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.thumbElement.Owner = "ScrollThumb";
      }
      if (this.buttonIncrement == null)
      {
        this.buttonIncrement = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.buttonIncrement.Owner = "ScrollButton";
      }
      if (this.buttonDecrement == null)
      {
        this.buttonDecrement = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.buttonDecrement.Owner = "ScrollButton";
      }
      this.buttonDecrement.HostingControl = (IScrollableControlBase) this;
      this.buttonIncrement.HostingControl = (IScrollableControlBase) this;
      this.thumbElement.HostingControl = (IScrollableControlBase) this;
      this.scrollElement.HostingControl = (IScrollableControlBase) this;
    }

    private void SmallIncrement()
    {
      int num = this.currentPos + this.smallIncrement;
      if (num > this.maxPos)
        num = this.maxPos;
      this.Value = num;
      this.SyncThumbPositionWithLogicalValue();
    }

    private void SmallDecrement()
    {
      int num = this.currentPos - this.smallIncrement;
      if (num < this.minPos)
        num = this.minPos;
      this.Value = num;
      this.SyncThumbPositionWithLogicalValue();
    }

    private void LargeIncrement()
    {
      int num = this.currentPos + this.largeIncrement;
      if (num > this.maxPos)
        num = this.maxPos;
      this.Value = num;
      this.SyncThumbPositionWithLogicalValue();
    }

    private void LargeDecrement()
    {
      int num = this.currentPos - this.largeIncrement;
      if (num < this.minPos)
        num = this.minPos;
      this.Value = num;
      this.SyncThumbPositionWithLogicalValue();
    }

    private void StartTimer()
    {
      if (this.scrollTimer == null)
      {
        this.scrollTimer = new Timer();
        this.scrollTimer.Tick += new EventHandler(this.OnTimerTick);
      }
      this.scrollTimer.Interval = 250;
      this.scrollTimer.Start();
    }

    /// <exclude />
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      if (this.isThumbVisible && this.ThumbRectangle.Contains(e.X, e.Y))
      {
        this.targetOffset = !(this is vHScrollBar) ? e.Y - this.ThumbRectangle.Top : e.X - this.ThumbRectangle.Left;
        this.Capture = true;
        this.scrollBarAction = ScrollBarAction.ThumbTrack;
      }
      else if (this.SmallDecrementRectangle.Contains(e.X, e.Y))
      {
        this.Capture = true;
        this.scrollBarAction = ScrollBarAction.SmallDecrement;
        this.isScrollRepeatOn = true;
        if (this.currentPos == this.minPos)
          return;
        this.SmallDecrement();
        this.StartTimer();
      }
      else if (this.SmallIncrementRectangle.Contains(e.X, e.Y))
      {
        this.Capture = true;
        this.scrollBarAction = ScrollBarAction.SmallIncrement;
        this.isScrollRepeatOn = true;
        if (this.currentPos == this.maxPos)
          return;
        this.SmallIncrement();
        this.StartTimer();
      }
      else if (this.LargeDecrementRectangle.Contains(e.X, e.Y))
      {
        this.Capture = true;
        this.scrollBarAction = ScrollBarAction.LargeDecrement;
        this.isScrollRepeatOn = true;
        if (this.currentPos == this.minPos)
          return;
        this.LargeDecrement();
        this.StartTimer();
      }
      else
      {
        if (!this.LargeIncrementRectangle.Contains(e.X, e.Y))
          return;
        this.Capture = true;
        this.scrollBarAction = ScrollBarAction.LargeIncrement;
        this.isScrollRepeatOn = true;
        if (this.currentPos == this.maxPos)
          return;
        this.LargeIncrement();
        this.StartTimer();
      }
    }

    /// <exclude />
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.SmartScrollEnabled)
      {
        Point pt = new Point(e.X, e.Y);
        if (this.SmallIncrementRectangle.Contains(pt))
        {
          this.scrollBarAction = ScrollBarAction.SmallIncrement;
          this.isScrollRepeatOn = true;
          this.StartTimer();
        }
        else if (this.SmallDecrementRectangle.Contains(pt))
        {
          this.scrollBarAction = ScrollBarAction.SmallDecrement;
          this.isScrollRepeatOn = true;
          this.StartTimer();
        }
        else if (this.scrollTimer != null)
        {
          this.scrollTimer.Stop();
          this.scrollTimer.Dispose();
          this.scrollTimer = (Timer) null;
        }
      }
      if (e.Button == MouseButtons.Left)
      {
        if (this.scrollBarAction == ScrollBarAction.ThumbTrack)
        {
          if (this.isThumbVisible)
            this.ThumbPosition = !(this is vHScrollBar) ? e.Y - this.targetOffset - this.SmallDecrementRectangle.Height : e.X - this.targetOffset - this.SmallDecrementRectangle.Width;
        }
        else if (this.scrollBarAction == ScrollBarAction.SmallDecrement)
        {
          Rectangle decrementRectangle = this.SmallDecrementRectangle;
          this.isScrollRepeatOn = decrementRectangle.Contains(e.X, e.Y);
          this.Invalidate(decrementRectangle);
          if (this.scrollTimer != null)
            this.scrollTimer.Enabled = this.isScrollRepeatOn;
        }
        else if (this.scrollBarAction == ScrollBarAction.SmallIncrement)
        {
          Rectangle incrementRectangle = this.SmallIncrementRectangle;
          this.isScrollRepeatOn = incrementRectangle.Contains(e.X, e.Y);
          this.Invalidate(incrementRectangle);
          if (this.scrollTimer != null)
            this.scrollTimer.Enabled = this.isScrollRepeatOn;
        }
        else if (this.scrollBarAction == ScrollBarAction.LargeDecrement)
        {
          Rectangle decrementRectangle = this.LargeDecrementRectangle;
          if (this.scrollTimer != null)
            this.scrollTimer.Enabled = decrementRectangle.Contains(e.X, e.Y);
        }
        else if (this.scrollBarAction == ScrollBarAction.LargeIncrement)
        {
          Rectangle incrementRectangle = this.LargeIncrementRectangle;
          if (this.scrollTimer != null)
            this.scrollTimer.Enabled = incrementRectangle.Contains(e.X, e.Y);
        }
      }
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.scrollTimer != null)
      {
        this.scrollTimer.Stop();
        this.scrollTimer.Dispose();
        this.scrollTimer = (Timer) null;
      }
      base.OnMouseLeave(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.Capture = false;
      this.scrollBarAction = ScrollBarAction.None;
      this.isScrollRepeatOn = false;
      this.Invalidate();
      if (this.scrollTimer == null)
        return;
      this.scrollTimer.Dispose();
      this.scrollTimer = (Timer) null;
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.scrollTimer != null)
        this.scrollTimer.Dispose();
      base.Dispose(disposing);
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
      Point client = this.PointToClient(Cursor.Position);
      if (this.scrollTimer.Interval == 250)
        this.scrollTimer.Interval = 50;
      if (this.scrollBarAction == ScrollBarAction.SmallDecrement)
      {
        if (this.currentPos == this.minPos)
          return;
        this.SmallDecrement();
      }
      else if (this.scrollBarAction == ScrollBarAction.SmallIncrement)
      {
        if (this.currentPos == this.maxPos)
          return;
        this.SmallIncrement();
      }
      else if (this.scrollBarAction == ScrollBarAction.LargeDecrement)
      {
        if (!this.LargeDecrementRectangle.Contains(client.X, client.Y) || this.currentPos == this.minPos)
          return;
        this.LargeDecrement();
      }
      else
      {
        if (this.scrollBarAction != ScrollBarAction.LargeIncrement || (!this.LargeIncrementRectangle.Contains(client.X, client.Y) || this.currentPos == this.maxPos))
          return;
        this.LargeIncrement();
      }
    }

    /// <exclude />
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.SyncThumbPositionWithLogicalValue();
    }

    /// <summary>Raises the Scroll event</summary>
    /// <param name="e">A ScrollEventArgs that contains the event data.</param>
    protected virtual void OnScroll(ScrollEventArgs e)
    {
      if (this.Scroll == null)
        return;
      this.Scroll((object) this, e);
    }

    /// <summary>Raises the ValueChanged event.</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }
  }
}
