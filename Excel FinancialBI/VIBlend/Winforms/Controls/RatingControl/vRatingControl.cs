// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRatingControl
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRatingControl</summary>
  /// <remarks>
  /// A vRatingControl displays a rating bar, and allows the user to select a rating level.
  /// </remarks>
  [ToolboxBitmap(typeof (vRatingControl), "ControlIcons.RatingControl.ico")]
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Displays a rating bar, and allows the user to select a rating level.")]
  public class vRatingControl : ScrollableControlMiniBase
  {
    private int maxValue = 5;
    private int spacing = 3;
    private vRatingControlShapes shape = vRatingControlShapes.Star;
    private int indicatorSize = 12;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Color ratingColor = Color.Yellow;
    private bool useRatingColor = true;
    private float rating;
    private float savedRating;
    protected BackgroundElement backFill;
    private ControlTheme theme;

    /// <summary>Gets or sets the rating.</summary>
    /// <value>The rating.</value>
    [DefaultValue(0)]
    [Category("Behavior")]
    [Description("Gets or sets the rating.")]
    public float Value
    {
      get
      {
        return this.rating;
      }
      set
      {
        if ((double) this.rating == (double) value)
          return;
        this.rating = value;
        this.OnValueChanged();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the shape.</summary>
    /// <value>The shape.</value>
    [Category("Behavior")]
    [DefaultValue("Shape.Standard")]
    [Description("Gets or sets the shape.")]
    public vRatingControlShapes Shape
    {
      get
      {
        return this.shape;
      }
      set
      {
        this.shape = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the rating positions number.</summary>
    /// <value>The rating positions number.</value>
    [DefaultValue(5)]
    [Category("Behavior")]
    [Description("Gets or sets the rating positions number.")]
    public int MaxValue
    {
      get
      {
        return this.maxValue;
      }
      set
      {
        this.maxValue = value;
        if ((double) this.Value <= (double) value)
          return;
        this.Value = (float) value;
      }
    }

    /// <summary>Gets or sets the width of the rating indicator.</summary>
    /// <value>The width of the rating indicator.</value>
    [DefaultValue(12)]
    [Category("Behavior")]
    [Description("Gets or sets the width of the rating indicator.")]
    public int ValueIndicatorSize
    {
      get
      {
        return this.indicatorSize;
      }
      set
      {
        this.indicatorSize = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control</summary>
    [Description("Gets or sets the theme of the control")]
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

    /// <summary>Gets or sets the rating positions number.</summary>
    /// <value>The rating positions number.</value>
    [Category("Behavior")]
    [DefaultValue(3)]
    [Description("Gets or sets the rating positions number.")]
    public int Spacing
    {
      get
      {
        return this.spacing;
      }
      set
      {
        this.spacing = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the rating.</summary>
    /// <value>The color of the rating.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Yellow")]
    [Description("Gets or sets the color of the rating.")]
    public Color RatingColor
    {
      get
      {
        return this.ratingColor;
      }
      set
      {
        this.ratingColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the rating color.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use the rating color.")]
    public bool UseRatingColor
    {
      get
      {
        return this.useRatingColor;
      }
      set
      {
        this.useRatingColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Occurs when value has changed</summary>
    [Description("Occurs when value has changed")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    static vRatingControl()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:RatingControl" /> class.
    /// </summary>
    public vRatingControl()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
    }

    /// <summary>Called when value has changed</summary>
    public void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int num = this.indicatorSize;
      int y = num;
      if (this.Shape == vRatingControlShapes.Star)
        this.DrawStars(e.Graphics, num, y, num / 2, num, this.MaxValue);
      else
        this.DrawRectangle(e.Graphics, num, y, num, num, this.MaxValue);
      e.Graphics.SmoothingMode = smoothingMode;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      int x = e.Location.X;
      int num1 = this.indicatorSize;
      int num2 = num1;
      int num3 = 1;
      if (this.shape == vRatingControlShapes.Star)
        num3 = 2;
      if (x > this.MaxValue * (num3 * num1 + this.spacing))
      {
        this.Value = (float) this.MaxValue;
        this.Refresh();
      }
      else
      {
        for (int index = 0; index <= this.MaxValue; ++index)
        {
          if (this.Shape == vRatingControlShapes.Star)
          {
            if (x < num2)
            {
              this.Value = (float) index;
              this.Refresh();
              break;
            }
          }
          else if (x < num2)
          {
            this.Value = (float) index;
            this.Refresh();
            break;
          }
          if (this.Shape == vRatingControlShapes.Star)
            num2 += num1 * 2 + this.spacing;
          else
            num2 += num1 + this.spacing;
        }
        base.OnMouseMove(e);
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Capture = false;
      this.savedRating = this.Value;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.Value = this.savedRating;
      base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.savedRating = this.Value;
      this.Capture = true;
      base.OnMouseDown(e);
    }

    private void DrawRectangle(Graphics g, int x, int y, int inner, int outer, int number)
    {
      Brush brush1 = (Brush) new SolidBrush(this.backFill.Theme.StyleNormal.FillStyle.Colors[0]);
      Brush brush2 = (Brush) new SolidBrush(Color.FromArgb((int) sbyte.MaxValue, this.backFill.Theme.StyleNormal.FillStyle.Colors[0]));
      Color color1 = this.theme.QueryColorSetter("RatingColor");
      Color color2 = this.theme.QueryColorSetter("RatingColor");
      if (this.UseRatingColor)
      {
        color2 = this.RatingColor;
        brush1 = (Brush) new SolidBrush(color2);
        brush2 = (Brush) new SolidBrush(Color.FromArgb((int) sbyte.MaxValue, color2));
      }
      Pen pen = new Pen(this.backFill.BorderColor);
      if (!color1.IsEmpty)
      {
        brush1 = (Brush) new SolidBrush(color1);
        brush2 = (Brush) new SolidBrush(Color.FromArgb(128, color2));
        pen = new Pen(color2);
      }
      int x1 = x;
      int y1 = y;
      bool flag = false;
      for (int index = 0; index < number; ++index)
      {
        int width = inner;
        int height = outer;
        Rectangle rect = new Rectangle(x1, y1, width, height);
        float num = this.Value;
        while ((double) num > 0.0 && (double) num != 0.5)
        {
          --num;
          if ((double) num < 0.0)
          {
            num = 0.5f;
            break;
          }
        }
        if ((double) num == 0.5 && !flag && (double) index >= (double) this.Value - 0.5)
        {
          g.FillRectangle(brush2, rect);
          flag = true;
        }
        else if ((double) index < (double) this.Value && (double) this.Value > 0.0)
          g.FillRectangle(brush1, rect);
        g.DrawRectangle(pen, rect);
        x1 += width + this.spacing;
      }
    }

    /// <summary>Draws the stars.</summary>
    private void DrawStars(Graphics g, int x, int y, int inner, int outer, int number)
    {
      Brush brush1 = (Brush) new SolidBrush(this.backFill.Theme.StyleNormal.FillStyle.Colors[0]);
      Brush brush2 = (Brush) new SolidBrush(Color.FromArgb((int) sbyte.MaxValue, this.backFill.Theme.StyleNormal.FillStyle.Colors[0]));
      Color color1 = this.theme.QueryColorSetter("RatingColor");
      Color color2 = this.theme.QueryColorSetter("RatingColor");
      if (this.UseRatingColor)
      {
        color2 = this.RatingColor;
        brush1 = (Brush) new SolidBrush(color2);
        brush2 = (Brush) new SolidBrush(Color.FromArgb((int) sbyte.MaxValue, color2));
      }
      Pen pen = new Pen(this.backFill.BorderColor);
      if (!color1.IsEmpty)
      {
        brush1 = (Brush) new SolidBrush(color2);
        brush2 = (Brush) new SolidBrush(Color.FromArgb(128, color2));
        pen = new Pen(color1);
      }
      int num1 = x;
      int num2 = y;
      bool flag = false;
      for (int index1 = 0; index1 < number; ++index1)
      {
        PointF[] points = new PointF[10];
        double num3 = Math.PI / 2.0;
        double num4 = 2.0 * Math.PI / 5.0;
        double num5 = num4 / 2.0;
        for (int index2 = 0; index2 < 5; ++index2)
        {
          points[index2 * 2].X = (float) num1 + (float) Math.Cos((double) index2 * num4 - num3) * (float) outer;
          points[index2 * 2 + 1].X = (float) num1 + (float) Math.Cos((double) index2 * num4 + num5 - num3) * (float) inner;
          points[index2 * 2].Y = (float) num2 + (float) Math.Sin((double) index2 * num4 - num3) * (float) outer;
          points[index2 * 2 + 1].Y = (float) num2 + (float) Math.Sin((double) index2 * num4 + num5 - num3) * (float) inner;
        }
        float num6 = this.Value;
        while ((double) num6 > 0.0 && (double) num6 != 0.5)
        {
          --num6;
          if ((double) num6 < 0.0)
          {
            num6 = 0.5f;
            break;
          }
        }
        if ((double) num6 == 0.5 && !flag && (double) index1 >= (double) this.Value - 0.5)
        {
          g.FillPolygon(brush2, points);
          flag = true;
        }
        else if ((double) index1 < (double) this.Value && (double) this.Value > 0.0)
          g.FillPolygon(brush1, points);
        g.DrawPolygon(pen, points);
        num1 += outer * 2 + this.spacing;
      }
    }
  }
}
