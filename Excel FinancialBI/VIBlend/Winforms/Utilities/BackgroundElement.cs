// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.BackgroundElement
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class BackgroundElement
  {
    private float opacity = 1f;
    private string owner = "Fill";
    private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
    private ContentAlignment imageAlign = ContentAlignment.MiddleLeft;
    private Hashtable fillbmps = new Hashtable();
    internal bool enableSmoothingMode = true;
    public byte RoundedCornersBitmask = 15;
    private int maxFrameRate = 8;
    private PaintHelper paintHelper;
    private Rectangle bounds;
    public Bitmap Bitmap;
    private IScrollableControlBase hostingControl;
    internal ControlState lastState;
    private bool isanimated;
    private bool textWrap;
    private ControlTheme theme;
    private Image image;
    internal string text;
    private bool animating;
    private bool backAnimating;
    internal float[] animatingStartColorOffsets;
    internal double animatingStartGradientAngle;
    internal double animatingStartGradientOffset;
    internal double animatingStartGradientOffset2;
    internal GradientStyles animatingStartGradientStyle;
    internal Color animatingStartColor1;
    internal Color animatingStartColor2;
    internal Color animatingStartColor3;
    internal Color animatingStartColor4;
    internal Color animatingEndColor1;
    internal Color animatingEndColor2;
    internal Color animatingEndColor3;
    internal Color animatingEndColor4;
    internal Color animatingColor1;
    internal Color animatingColor2;
    internal Color animatingColor3;
    internal Color animatingColor4;
    internal int currentFrame;

    public IScrollableControlBase HostingControl
    {
      get
      {
        return this.hostingControl;
      }
      set
      {
        this.hostingControl = value;
      }
    }

    internal PaintHelper PaintHelper
    {
      get
      {
        return this.paintHelper;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="!:BackGroundElement" /> has opacity.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if has opacity draw with opacity; otherwise not, <c>false</c>.
    /// </value>
    public float Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        if ((double) value < 0.0)
          value = 0.0f;
        if ((double) value > 1.0)
          value = 1f;
        this.opacity = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is animated.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is animated; otherwise, <c>false</c>.
    /// </value>
    public bool IsAnimated
    {
      get
      {
        return this.isanimated;
      }
      set
      {
        this.isanimated = value;
      }
    }

    public Size Size
    {
      get
      {
        return this.bounds.Size;
      }
      set
      {
        this.bounds.Size = value;
      }
    }

    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
      set
      {
        this.bounds = value;
      }
    }

    public bool TextWrap
    {
      get
      {
        return this.textWrap;
      }
      set
      {
        this.textWrap = value;
      }
    }

    public string Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    public int Radius
    {
      get
      {
        if (this.theme == null)
          return 0;
        return (int) this.theme.Radius;
      }
      set
      {
        if (this.theme == null)
          return;
        this.theme.Radius = (float) value;
      }
    }

    public Shapes Shape
    {
      get
      {
        return this.theme.ShapeType;
      }
      set
      {
        this.theme.ShapeType = value;
      }
    }

    public Color BorderColor
    {
      get
      {
        return this.theme.StyleNormal.BorderColor;
      }
      set
      {
        this.theme.StyleNormal.BorderColor = value;
      }
    }

    public Color HighlightBorderColor
    {
      get
      {
        return this.theme.StyleHighlight.BorderColor;
      }
      set
      {
        this.theme.StyleHighlight.BorderColor = value;
      }
    }

    public Color PressedBorderColor
    {
      get
      {
        return this.theme.StylePressed.BorderColor;
      }
      set
      {
        this.theme.StylePressed.BorderColor = value;
      }
    }

    public Color DisabledBorderColor
    {
      get
      {
        return this.theme.StyleDisabled.BorderColor;
      }
      set
      {
        this.theme.StyleDisabled.BorderColor = value;
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlign;
      }
      set
      {
        this.textAlign = value;
      }
    }

    /// <summary>Gets or sets the image alignment.</summary>
    /// <value>The image alignment.</value>
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlign;
      }
      set
      {
        this.imageAlign = value;
      }
    }

    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
    }

    /// <summary>Gets or sets the image.</summary>
    /// <value>The image.</value>
    [Description("Gets or sets the image displayed in the header.")]
    [Category("Appearance")]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
      }
    }

    /// <summary>Gets or sets whether the smoothing mode is enabled.</summary>
    public bool EnableSmoothingMode
    {
      get
      {
        return this.enableSmoothingMode;
      }
      set
      {
        this.enableSmoothingMode = value;
      }
    }

    public int MaxFrameRate
    {
      get
      {
        return this.maxFrameRate;
      }
      set
      {
        this.maxFrameRate = value;
      }
    }

    public bool Animating
    {
      get
      {
        return this.animating;
      }
      set
      {
        this.animating = value;
      }
    }

    public bool BackAnimating
    {
      get
      {
        return this.backAnimating;
      }
      set
      {
        this.backAnimating = value;
      }
    }

    public Color ForeColor
    {
      get
      {
        return this.theme.StyleNormal.TextColor;
      }
      set
      {
        this.theme.StyleNormal.TextColor = value;
      }
    }

    public Color PressedForeColor
    {
      get
      {
        return this.theme.StylePressed.TextColor;
      }
      set
      {
        this.theme.StylePressed.TextColor = value;
      }
    }

    public Color HighlightForeColor
    {
      get
      {
        return this.theme.StyleHighlight.TextColor;
      }
      set
      {
        this.theme.StyleHighlight.TextColor = value;
      }
    }

    public Color DisabledForeColor
    {
      get
      {
        return this.theme.StyleDisabled.TextColor;
      }
      set
      {
        this.theme.StyleDisabled.TextColor = value;
      }
    }

    public Font Font
    {
      get
      {
        return this.theme.StyleNormal.Font;
      }
      set
      {
        this.theme.StyleNormal.Font = value;
      }
    }

    public Font PressedFont
    {
      get
      {
        return this.theme.StylePressed.Font;
      }
      set
      {
        this.theme.StylePressed.Font = value;
      }
    }

    public Font HighLightFont
    {
      get
      {
        return this.theme.StyleHighlight.Font;
      }
      set
      {
        this.theme.StyleHighlight.Font = value;
      }
    }

    public Font DisabledFont
    {
      get
      {
        return this.theme.StyleDisabled.Font;
      }
      set
      {
        this.theme.StyleDisabled.Font = value;
      }
    }

    public BackgroundElement(Rectangle bounds, IScrollableControlBase hostingControl)
    {
      this.paintHelper = new PaintHelper();
      this.bounds = bounds;
      this.hostingControl = hostingControl;
    }

    public void SetLastState(ControlState state)
    {
      this.lastState = state;
    }

    public void LoadTheme(ControlTheme theme)
    {
      this.animatingStartColorOffsets = (float[]) null;
      this.Animating = false;
      this.BackAnimating = false;
      this.theme = theme;
    }

    /// <summary>Draws the element text and image.</summary>
    /// <param name="g">The g.</param>
    /// <param name="stateType">Type of the state.</param>
    /// <param name="text">The text.</param>
    /// <param name="image">The image.</param>
    /// <param name="textBounds">The text bounds.</param>
    /// <param name="txtImageRelation">The TXT image relation.</param>
    public void DrawElementTextAndImage(Graphics g, ControlState stateType, string text, Image image, Rectangle textBounds, TextImageRelation txtImageRelation)
    {
      if (this.Image != null)
        image = this.Image;
      if (image != null)
      {
        Color color = this.ForeColor;
        Font font = this.Font;
        if (stateType == ControlState.Pressed)
        {
          color = this.PressedForeColor;
          Font pressedFont = this.PressedFont;
        }
        else if (stateType == ControlState.Hover)
        {
          color = this.HighlightForeColor;
          Font highLightFont = this.HighLightFont;
        }
        else if (stateType == ControlState.Disabled)
        {
          color = this.DisabledForeColor;
          Font disabledFont = this.DisabledFont;
        }
        this.paintHelper.DrawImageAndTextRectangle(g, this.TextWrap, 0, textBounds, true, this.Font, new SolidBrush(color), this.imageAlign, this.textAlign, text, image, txtImageRelation);
      }
      else
        this.DrawElementText(g, stateType, text, textBounds);
    }

    /// <summary>Draws the element text and image.</summary>
    /// <param name="g">The g.</param>
    /// <param name="stateType">Type of the state.</param>
    /// <param name="text">The text.</param>
    /// <param name="image">The image.</param>
    /// <param name="textBounds">The text bounds.</param>
    /// <param name="txtImageRelation">The TXT image relation.</param>
    /// <param name="color">The color.</param>
    public void DrawElementTextAndImage(Graphics g, ControlState stateType, string text, Image image, Rectangle textBounds, TextImageRelation txtImageRelation, Color color)
    {
      if (image != null)
      {
        Font font = this.Font;
        if (stateType == ControlState.Pressed)
        {
          Font pressedFont = this.PressedFont;
        }
        else if (stateType == ControlState.Hover)
        {
          Font highLightFont = this.HighLightFont;
        }
        else if (stateType == ControlState.Disabled)
        {
          Font disabledFont = this.DisabledFont;
        }
        this.paintHelper.DrawImageAndTextRectangle(g, this.TextWrap, 0, textBounds, true, this.Font, new SolidBrush(color), this.imageAlign, this.textAlign, text, image, txtImageRelation);
      }
      else
        this.DrawElementText(g, stateType, text, textBounds, color);
    }

    public void DrawElementText(Graphics g, ControlState stateType, string text, Rectangle textBounds, Color color)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      this.lastState = stateType;
      this.text = text;
      Font font = this.Font;
      if (stateType == ControlState.Pressed)
        font = this.PressedFont;
      else if (stateType == ControlState.Hover)
        font = this.HighLightFont;
      else if (stateType == ControlState.Disabled)
        font = this.DisabledFont;
      Color foreColor = color;
      if ((double) this.opacity != 1.0)
      {
        int alpha = (int) foreColor.A - (int) ((double) this.opacity * (double) byte.MaxValue);
        if (alpha <= 0)
          alpha = 0;
        foreColor = Color.FromArgb(alpha, (int) foreColor.R, (int) foreColor.G, (int) foreColor.B);
      }
      this.paintHelper.DrawText(g, textBounds, this.TextWrap, foreColor, text, font, this.TextAlignment);
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementText(Graphics g, ControlState stateType, string text, Rectangle textBounds)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      this.lastState = stateType;
      this.text = text;
      Color color = this.ForeColor;
      Font font = this.Font;
      if (stateType == ControlState.Pressed)
      {
        color = this.PressedForeColor;
        font = this.PressedFont;
      }
      else if (stateType == ControlState.Hover)
      {
        color = this.HighlightForeColor;
        font = this.HighLightFont;
      }
      else if (stateType == ControlState.Disabled)
      {
        color = this.DisabledForeColor;
        font = this.DisabledFont;
      }
      Color foreColor = color;
      if ((double) this.opacity != 1.0)
      {
        int alpha = (int) foreColor.A - (int) ((double) this.opacity * (double) byte.MaxValue);
        if (alpha <= 0)
          alpha = 0;
        foreColor = Color.FromArgb(alpha, (int) foreColor.R, (int) foreColor.G, (int) foreColor.B);
      }
      this.paintHelper.DrawText(g, textBounds, this.TextWrap, foreColor, text, font, this.TextAlignment);
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementBorder(Graphics g, ControlState stateType, Color color)
    {
      this.lastState = stateType;
      if (this.bounds == Rectangle.Empty)
        return;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Alignment = PenAlignment.Center;
        pen.Color = color;
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) pen.Color.A < (int) byte.MaxValue)
          alpha = (int) pen.Color.A;
        pen.Color = Color.FromArgb(alpha, pen.Color);
        if (this.Shape == Shapes.Default)
          g.DrawRectangle(pen, this.bounds);
        else if (this.Shape == Shapes.RoundedRectangle)
        {
          GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(this.bounds, this.Radius, this.RoundedCornersBitmask);
          g.DrawPath(pen, partiallyRoundedPath);
          partiallyRoundedPath.Dispose();
        }
        pen.Dispose();
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementBorder(Graphics g, ControlState stateType, Point[] points, Color color)
    {
      this.lastState = stateType;
      if (this.bounds == Rectangle.Empty)
        return;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Alignment = PenAlignment.Center;
        switch (stateType)
        {
          case ControlState.Hover:
            pen.Color = this.theme.StyleHighlight.BorderColor;
            break;
          case ControlState.Pressed:
            pen.Color = this.theme.StylePressed.BorderColor;
            break;
          case ControlState.Disabled:
            pen.Color = this.theme.StyleDisabled.BorderColor;
            break;
          case ControlState.DisabledPressed:
            pen.Color = this.theme.StyleDisabledPressed.BorderColor;
            break;
          default:
            pen.Color = this.theme.StyleNormal.BorderColor;
            break;
        }
        if (color != Color.Empty)
          pen.Color = color;
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) pen.Color.A < (int) byte.MaxValue)
          alpha = (int) pen.Color.A;
        pen.Color = Color.FromArgb(alpha, pen.Color);
        g.DrawLines(pen, points);
        pen.Dispose();
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementBorder(Graphics g, ControlState stateType, Point[] points)
    {
      this.lastState = stateType;
      if (this.bounds == Rectangle.Empty)
        return;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Alignment = PenAlignment.Center;
        switch (stateType)
        {
          case ControlState.Hover:
            pen.Color = this.theme.StyleHighlight.BorderColor;
            break;
          case ControlState.Pressed:
            pen.Color = this.theme.StylePressed.BorderColor;
            break;
          case ControlState.Disabled:
            pen.Color = this.theme.StyleDisabled.BorderColor;
            break;
          case ControlState.DisabledPressed:
            pen.Color = this.theme.StyleDisabledPressed.BorderColor;
            break;
          default:
            pen.Color = this.theme.StyleNormal.BorderColor;
            break;
        }
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) pen.Color.A < (int) byte.MaxValue)
          alpha = (int) pen.Color.A;
        pen.Color = Color.FromArgb(alpha, pen.Color);
        g.DrawLines(pen, points);
        pen.Dispose();
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementBorder(Graphics g, ControlState stateType)
    {
      this.lastState = stateType;
      if (this.bounds == Rectangle.Empty)
        return;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Alignment = PenAlignment.Center;
        switch (stateType)
        {
          case ControlState.Hover:
            pen.Color = this.theme.StyleHighlight.BorderColor;
            break;
          case ControlState.Pressed:
            pen.Color = this.theme.StylePressed.BorderColor;
            break;
          case ControlState.Disabled:
            pen.Color = this.theme.StyleDisabled.BorderColor;
            break;
          case ControlState.DisabledPressed:
            pen.Color = this.theme.StyleDisabledPressed.BorderColor;
            break;
          default:
            pen.Color = this.theme.StyleNormal.BorderColor;
            break;
        }
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) pen.Color.A < (int) byte.MaxValue)
          alpha = (int) pen.Color.A;
        pen.Color = Color.FromArgb(alpha, pen.Color);
        if (this.Shape == Shapes.Default)
          g.DrawRectangle(pen, this.bounds);
        else if (this.Shape == Shapes.RoundedRectangle)
        {
          GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(this.bounds, this.Radius, this.RoundedCornersBitmask);
          g.DrawPath(pen, partiallyRoundedPath);
          partiallyRoundedPath.Dispose();
        }
        pen.Dispose();
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementBorder(Graphics g, GraphicsPath path, Color color)
    {
      if (this.bounds == Rectangle.Empty)
        return;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(color))
      {
        pen.Alignment = PenAlignment.Center;
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) pen.Color.A < (int) byte.MaxValue)
          alpha = (int) pen.Color.A;
        pen.Color = Color.FromArgb(alpha, pen.Color);
        if (this.Shape == Shapes.Default)
          g.DrawRectangle(pen, this.bounds);
        else if (this.Shape == Shapes.RoundedRectangle)
        {
          g.DrawPath(pen, path);
          path.Dispose();
        }
        pen.Dispose();
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawElementBorder(Graphics g, ControlState stateType, GraphicsPath path)
    {
      this.lastState = stateType;
      if (this.bounds == Rectangle.Empty)
        return;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Alignment = PenAlignment.Center;
        switch (stateType)
        {
          case ControlState.Hover:
            pen.Color = this.theme.StyleHighlight.BorderColor;
            break;
          case ControlState.Pressed:
            pen.Color = this.theme.StylePressed.BorderColor;
            break;
          case ControlState.Disabled:
            pen.Color = this.theme.StyleDisabled.BorderColor;
            break;
          case ControlState.DisabledPressed:
            pen.Color = this.theme.StyleDisabledPressed.BorderColor;
            break;
          default:
            pen.Color = this.theme.StyleNormal.BorderColor;
            break;
        }
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) pen.Color.A < (int) byte.MaxValue)
          alpha = (int) pen.Color.A;
        pen.Color = Color.FromArgb(alpha, pen.Color);
        if (this.Shape == Shapes.Default)
          g.DrawRectangle(pen, this.bounds);
        else if (this.Shape == Shapes.RoundedRectangle)
        {
          g.DrawPath(pen, path);
          path.Dispose();
        }
        pen.Dispose();
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawStandardFill(Graphics g, Color color)
    {
      int num = 4;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.HighSpeed;
      if (this.bounds == Rectangle.Empty)
      {
        g.SmoothingMode = smoothingMode;
      }
      else
      {
        Color[] colors = new Color[num];
        float[] colorOffsets = new float[num];
        GradientStyles gradientStyle = GradientStyles.Linear;
        double gradientAngle = 90.0;
        double gradientOffset = 0.5;
        double gradientOffset2 = 0.5;
        this.InitializeStateFillColors(ControlState.Normal, num, ref colors, ref colorOffsets, ref gradientStyle, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
        Rectangle bounds = this.Bounds;
        this.lastState = ControlState.Normal;
        g.SmoothingMode = smoothingMode;
        for (int index = 0; index < colors.Length; ++index)
          colors[index] = color;
        SolidBrush solidBrush = new SolidBrush(colors[0]);
        if (this.Shape == Shapes.Circle)
        {
          g.FillEllipse((Brush) solidBrush, bounds);
        }
        else
        {
          byte roundedCornersBitmask = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
          if ((int) roundedCornersBitmask == 0)
          {
            g.FillRectangle((Brush) solidBrush, bounds);
          }
          else
          {
            GraphicsPath partiallyRoundedPath = this.PaintHelper.CreatePartiallyRoundedPath(bounds, this.Radius, roundedCornersBitmask);
            g.FillPath((Brush) solidBrush, partiallyRoundedPath);
          }
        }
        solidBrush.Dispose();
      }
    }

    public void DrawStandardFill(Graphics g, ControlState stateType, GradientStyles gradientStyle, GraphicsPath path)
    {
      int num = 4;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.HighSpeed;
      if (this.bounds == Rectangle.Empty)
      {
        g.SmoothingMode = smoothingMode;
      }
      else
      {
        Color[] colors = new Color[num];
        float[] colorOffsets = new float[num];
        GradientStyles gradientStyle1 = GradientStyles.Linear;
        double gradientAngle = 90.0;
        double gradientOffset = 0.5;
        double gradientOffset2 = 0.5;
        this.InitializeStateFillColors(stateType, num, ref colors, ref colorOffsets, ref gradientStyle1, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
        Rectangle bounds = this.Bounds;
        this.lastState = stateType;
        g.SmoothingMode = smoothingMode;
        switch (gradientStyle)
        {
          case GradientStyles.Solid:
            SolidBrush solidBrush = new SolidBrush(colors[0]);
            if (this.Shape == Shapes.Circle)
              g.FillEllipse((Brush) solidBrush, bounds);
            else if ((this.Radius == 0 ? 0 : (int) this.RoundedCornersBitmask) == 0)
              g.FillRectangle((Brush) solidBrush, bounds);
            else
              g.FillPath((Brush) solidBrush, path);
            solidBrush.Dispose();
            break;
          case GradientStyles.Linear:
            if (this.Shape == Shapes.Circle)
            {
              this.paintHelper.DrawEllipse(g, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
              break;
            }
            this.paintHelper.DrawGradientPathFigure(g, path, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
            break;
        }
      }
    }

    /// <summary>Draws the standard background fill.</summary>
    /// <param name="g">The g.</param>
    /// <param name="stateType">Type of the state.</param>
    /// <param name="gradientStyle">The gradient style.</param>
    public void DrawStandardFill(Graphics g, ControlState stateType, GradientStyles gradientStyle)
    {
      int num = 4;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.HighSpeed;
      if (this.bounds == Rectangle.Empty)
      {
        g.SmoothingMode = smoothingMode;
      }
      else
      {
        Color[] colors = new Color[num];
        float[] colorOffsets = new float[num];
        GradientStyles gradientStyle1 = GradientStyles.Linear;
        double gradientAngle = 90.0;
        double gradientOffset = 0.5;
        double gradientOffset2 = 0.5;
        this.InitializeStateFillColors(stateType, num, ref colors, ref colorOffsets, ref gradientStyle1, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
        Rectangle bounds = this.Bounds;
        this.lastState = stateType;
        g.SmoothingMode = smoothingMode;
        switch (gradientStyle)
        {
          case GradientStyles.Solid:
            SolidBrush solidBrush = new SolidBrush(colors[0]);
            if (this.Shape == Shapes.Circle)
            {
              g.FillEllipse((Brush) solidBrush, bounds);
            }
            else
            {
              byte roundedCornersBitmask = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
              if ((int) roundedCornersBitmask == 0)
              {
                g.FillRectangle((Brush) solidBrush, bounds);
              }
              else
              {
                GraphicsPath partiallyRoundedPath = this.PaintHelper.CreatePartiallyRoundedPath(bounds, this.Radius, roundedCornersBitmask);
                g.FillPath((Brush) solidBrush, partiallyRoundedPath);
              }
            }
            solidBrush.Dispose();
            break;
          case GradientStyles.Linear:
            if (this.Shape == Shapes.Circle)
            {
              this.paintHelper.DrawEllipse(g, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
              break;
            }
            byte roundedCornerBitmask = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
            switch (roundedCornerBitmask)
            {
              case 0:
                this.paintHelper.DrawGradientRectFigure(g, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                return;
              case 15:
                this.paintHelper.DrawGradientPathFigure(g, this.Radius, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                return;
              default:
                this.paintHelper.DrawGradientPartiallyRoundedRectFigure(g, this.Radius, bounds, roundedCornerBitmask, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                return;
            }
        }
      }
    }

    public void DrawStandardFillWithCustomGradientOffsets(Graphics g, ControlState stateType, GradientStyles gradientStyle, double gradientAngle, double gradientOffset, double gradientOffset2)
    {
      int num1 = 4;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      if (this.bounds == Rectangle.Empty)
      {
        g.SmoothingMode = smoothingMode;
      }
      else
      {
        Color[] colors = new Color[num1];
        float[] colorOffsets = new float[num1];
        GradientStyles gradientStyle1 = GradientStyles.Linear;
        double gradientAngle1 = gradientAngle;
        double gradientOffset1 = gradientOffset;
        double gradientOffset2_1 = gradientOffset2;
        this.InitializeStateFillColors(stateType, num1, ref colors, ref colorOffsets, ref gradientStyle1, ref gradientAngle1, ref gradientOffset1, ref gradientOffset2_1);
        double num2 = gradientOffset;
        gradientOffset2_1 = gradientOffset2;
        if (colorOffsets.Length > 3)
        {
          colorOffsets[0] = 0.0f;
          colorOffsets[1] = (float) num2;
          colorOffsets[2] = (float) gradientOffset2;
          colorOffsets[3] = 1f;
        }
        Rectangle bounds = this.Bounds;
        this.lastState = stateType;
        g.SmoothingMode = smoothingMode;
        switch (gradientStyle)
        {
          case GradientStyles.Solid:
            SolidBrush solidBrush = new SolidBrush(colors[0]);
            if (this.Shape == Shapes.Circle)
            {
              g.FillEllipse((Brush) solidBrush, bounds);
            }
            else
            {
              byte roundedCornersBitmask = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
              if ((int) roundedCornersBitmask == 0)
              {
                g.FillRectangle((Brush) solidBrush, bounds);
              }
              else
              {
                GraphicsPath partiallyRoundedPath = this.PaintHelper.CreatePartiallyRoundedPath(bounds, this.Radius, roundedCornersBitmask);
                g.FillPath((Brush) solidBrush, partiallyRoundedPath);
              }
            }
            solidBrush.Dispose();
            break;
          case GradientStyles.Linear:
            if (this.Shape == Shapes.Circle)
            {
              this.paintHelper.DrawEllipse(g, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
              break;
            }
            byte roundedCornerBitmask = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
            switch (roundedCornerBitmask)
            {
              case 0:
                this.paintHelper.DrawGradientRectFigure(g, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                return;
              case 15:
                this.paintHelper.DrawGradientPathFigure(g, this.Radius, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                return;
              default:
                this.paintHelper.DrawGradientPartiallyRoundedRectFigure(g, this.Radius, bounds, roundedCornerBitmask, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                return;
            }
        }
      }
    }

    public void DrawElementFill(Graphics g, ControlState stateType)
    {
      int num = 4;
      Color[] colors = new Color[num];
      float[] colorOffsets = new float[num];
      GradientStyles gradientStyle = GradientStyles.Linear;
      double gradientAngle = 90.0;
      double gradientOffset = 0.5;
      double gradientOffset2 = 0.5;
      SmoothingMode smoothingMode = g.SmoothingMode;
      if (this.enableSmoothingMode)
        g.SmoothingMode = SmoothingMode.AntiAlias;
      if (!this.Animating && !this.BackAnimating && !this.IsAnimated || !this.hostingControl.AllowAnimations)
      {
        if (this.bounds == Rectangle.Empty)
        {
          g.SmoothingMode = smoothingMode;
          return;
        }
        this.InitializeStateFillColors(stateType, num, ref colors, ref colorOffsets, ref gradientStyle, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
        Rectangle bounds = this.Bounds;
        if (!this.hostingControl.AllowAnimations || !this.IsAnimated)
        {
          switch (gradientStyle)
          {
            case GradientStyles.Solid:
              SolidBrush solidBrush = new SolidBrush(colors[0]);
              byte roundedCornersBitmask = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
              if ((int) roundedCornersBitmask == 0)
              {
                g.FillRectangle((Brush) solidBrush, bounds);
              }
              else
              {
                GraphicsPath partiallyRoundedPath = this.PaintHelper.CreatePartiallyRoundedPath(bounds, this.Radius, roundedCornersBitmask);
                g.FillPath((Brush) solidBrush, partiallyRoundedPath);
              }
              solidBrush.Dispose();
              break;
            case GradientStyles.Linear:
              {
                if (this.Shape == Shapes.Circle)
                {
                  this.paintHelper.DrawGlowEllipse(g, bounds, colors, Color.White);
                  break;
                }
                byte roundedCornerBitmask = this.Radius == 0 ? (byte)0 : this.RoundedCornersBitmask;
                switch (roundedCornerBitmask)
                {
                  case 0:
                    this.paintHelper.DrawGradientRectFigure(g, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                    break;
                  case 15:
                    this.paintHelper.DrawGradientPathFigure(g, this.Radius, bounds, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                    break;
                  default:
                    this.paintHelper.DrawGradientPartiallyRoundedRectFigure(g, this.Radius, bounds, roundedCornerBitmask, colors, colorOffsets, GradientStyles.Linear, gradientAngle, gradientOffset, gradientOffset2);
                    break;
                }
              }
              break;
          }
        }
        else
        {
          this.InitializeStateFillColors(stateType, num, ref colors, ref colorOffsets, ref gradientStyle, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
          this.DrawAnimatedFill(g, stateType, colors, colorOffsets, gradientStyle, gradientAngle, gradientOffset, gradientOffset2);
          this.AnimatedColorsFill(g);
        }
      }
      else if (this.IsAnimated)
      {
        if (this.animatingStartColorOffsets == null)
        {
          this.InitializeStateFillColors(stateType, num, ref colors, ref colorOffsets, ref gradientStyle, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
          this.DrawAnimatedFill(g, stateType, colors, colorOffsets, gradientStyle, gradientAngle, gradientOffset, gradientOffset2);
          this.AnimatedColorsFill(g);
        }
        else
          this.AnimatedColorsFill(g);
      }
      if (this.IsAnimated)
        this.ChangeAnimationStyle(stateType);
      this.lastState = stateType;
      g.SmoothingMode = smoothingMode;
    }

    private void InitializeStateFillColors(ControlState state, int num, ref Color[] colors, ref float[] colorOffsets, ref GradientStyles gradientStyle, ref double gradientAngle, ref double gradientOffset, ref double gradientOffset2)
    {
      VisualStyle visualStyle;
      switch (state)
      {
        case ControlState.Normal:
          visualStyle = this.theme.StyleNormal;
          break;
        case ControlState.Hover:
          visualStyle = this.theme.StyleHighlight;
          break;
        case ControlState.Pressed:
          visualStyle = this.theme.StylePressed;
          break;
        case ControlState.Disabled:
          visualStyle = this.theme.StyleDisabled;
          break;
        case ControlState.DisabledPressed:
          visualStyle = this.theme.StyleDisabledPressed;
          break;
        default:
          visualStyle = this.theme.StyleNormal;
          break;
      }
      FillStyle fillStyle = visualStyle.FillStyle;
      int colorsNumber = fillStyle.ColorsNumber;
      colors = new Color[Math.Min(Math.Max(colorsNumber, 1), num)];
      colorOffsets = new float[Math.Min(Math.Max(colorsNumber, 1), num)];
      if (colors.Length > 0)
      {
        gradientStyle = GradientStyles.Solid;
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) fillStyle.Colors[0].A < (int) byte.MaxValue)
          alpha = (int) fillStyle.Colors[0].A;
        colors[0] = Color.FromArgb(alpha, fillStyle.Colors[0]);
        colorOffsets[0] = 0.0f;
      }
      if (colors.Length > 1)
      {
        gradientStyle = GradientStyles.Linear;
        if (fillStyle is FillStyleGradientEx)
          gradientAngle = (double) ((FillStyleGradientEx) fillStyle).GradientAngle;
        else if (fillStyle is FillStyleGradient)
          gradientAngle = ((FillStyleGradient) fillStyle).LinearGradientMode != LinearGradientMode.Horizontal ? 90.0 : 0.0;
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) fillStyle.Colors[1].A < (int) byte.MaxValue)
          alpha = (int) fillStyle.Colors[1].A;
        colors[1] = Color.FromArgb(alpha, fillStyle.Colors[1]);
        colorOffsets[colors.Length - 1] = 1f;
      }
      if (colors.Length > 2)
      {
        int alpha = (int) ((double) byte.MaxValue * (double) this.Opacity);
        if ((int) fillStyle.Colors[2].A < (int) byte.MaxValue)
          alpha = (int) fillStyle.Colors[2].A;
        colors[2] = Color.FromArgb(alpha, fillStyle.Colors[2]);
        colorOffsets[1] = ((FillStyleGradientEx) fillStyle).GradientOffset;
        gradientOffset = (double) ((FillStyleGradientEx) fillStyle).GradientOffset;
        gradientAngle = (double) ((FillStyleGradientEx) fillStyle).GradientAngle;
      }
      if (colors.Length <= 3)
        return;
      int alpha1 = (int) ((double) byte.MaxValue * (double) this.Opacity);
      if ((int) fillStyle.Colors[3].A < (int) byte.MaxValue)
        alpha1 = (int) fillStyle.Colors[3].A;
      colors[3] = Color.FromArgb(alpha1, fillStyle.Colors[3]);
      colorOffsets[1] = ((FillStyleGradientEx) fillStyle).GradientOffset;
      colorOffsets[2] = ((FillStyleGradientEx) fillStyle).GradientOffset2;
      gradientOffset2 = (double) ((FillStyleGradientEx) fillStyle).GradientOffset2;
      gradientAngle = (double) ((FillStyleGradientEx) fillStyle).GradientAngle;
    }

    private void ChangeAnimationStyle(ControlState stateType)
    {
      if (this.lastState == stateType)
        return;
      if (this.lastState != stateType)
      {
        if (stateType == ControlState.Hover || stateType == ControlState.Pressed)
        {
          this.Animating = true;
          this.BackAnimating = false;
          this.currentFrame = 0;
        }
        else
        {
          this.currentFrame = this.MaxFrameRate;
          this.BackAnimating = true;
          this.Animating = false;
        }
        Color[] colors = new Color[4];
        float[] colorOffsets = new float[4];
        GradientStyles gradientStyle = GradientStyles.Linear;
        double gradientAngle = 90.0;
        double gradientOffset = 0.5;
        double gradientOffset2 = 0.5;
        this.InitializeStateFillColors(stateType, 4, ref colors, ref colorOffsets, ref gradientStyle, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
        if (this.Animating)
        {
          if (colors.Length > 0)
            this.animatingEndColor1 = colors[0];
          if (colors.Length > 1)
            this.animatingEndColor2 = colors[1];
          if (colors.Length > 2)
            this.animatingEndColor3 = colors[2];
          if (colors.Length > 3)
            this.animatingEndColor4 = colors[3];
        }
        else
        {
          if (colors.Length > 0)
            this.animatingStartColor1 = colors[0];
          if (colors.Length > 1)
            this.animatingStartColor2 = colors[1];
          if (colors.Length > 2)
            this.animatingStartColor3 = colors[2];
          if (colors.Length > 3)
            this.animatingStartColor4 = colors[3];
        }
        this.InitializeStateFillColors(this.lastState, 4, ref colors, ref colorOffsets, ref gradientStyle, ref gradientAngle, ref gradientOffset, ref gradientOffset2);
        if (this.Animating)
        {
          if (colors.Length > 0)
            this.animatingStartColor1 = colors[0];
          if (colors.Length > 1)
            this.animatingStartColor2 = colors[1];
          if (colors.Length > 2)
            this.animatingStartColor3 = colors[2];
          if (colors.Length > 3)
            this.animatingStartColor4 = colors[3];
        }
        else
        {
          if (colors.Length > 0)
            this.animatingEndColor1 = colors[0];
          if (colors.Length > 1)
            this.animatingEndColor2 = colors[1];
          if (colors.Length > 2)
            this.animatingEndColor3 = colors[2];
          if (colors.Length > 3)
            this.animatingEndColor4 = colors[3];
        }
      }
      this.hostingControl.AnimationManager.AddBackGroundElement(this);
    }

    private void AnimatedColorsFill(Graphics g)
    {
      Color[] colorArray = new Color[this.animatingStartColorOffsets.Length];
      colorArray[0] = this.animatingColor1;
      if (colorArray.Length > 1)
        colorArray[1] = this.animatingColor2;
      if (colorArray.Length > 2)
        colorArray[2] = this.animatingColor3;
      if (colorArray.Length > 3)
        colorArray[3] = this.animatingColor4;
      byte num = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
      switch (this.animatingStartGradientStyle)
      {
        case GradientStyles.Solid:
          SolidBrush solidBrush = new SolidBrush(colorArray[0]);
          if (this.Radius == 0)
          {
            g.FillRectangle((Brush) solidBrush, this.bounds);
          }
          else
          {
            GraphicsPath partiallyRoundedPath = this.PaintHelper.CreatePartiallyRoundedPath(this.bounds, this.Radius, num);
            g.FillPath((Brush) solidBrush, partiallyRoundedPath);
          }
          solidBrush.Dispose();
          break;
        case GradientStyles.Linear:
          if (this.Shape == Shapes.Circle)
          {
            this.paintHelper.DrawGlowEllipse(g, this.bounds, colorArray, Color.White);
            break;
          }
          this.paintHelper.DrawGradientPartiallyRoundedRectFigure(g, this.Radius, this.bounds, num, colorArray, this.animatingStartColorOffsets, GradientStyles.Linear, this.animatingStartGradientAngle, this.animatingStartGradientOffset, this.animatingStartGradientOffset2);
          break;
      }
    }

    private void DrawAnimatedFill(Graphics g, ControlState stateType, Color[] colors, float[] colorOffsets, GradientStyles style, double gradientAngle, double gradientOffset, double gradientOffset2)
    {
      FillStyle fillStyle1 = this.theme.StyleNormal.FillStyle;
      this.animatingStartColor1 = fillStyle1.Colors[0];
      this.animatingStartColor2 = fillStyle1.Colors[0];
      this.animatingStartColor3 = fillStyle1.Colors[0];
      this.animatingStartColor4 = fillStyle1.Colors[0];
      FillStyle fillStyle2 = this.theme.StylePressed.FillStyle;
      if (stateType == ControlState.Pressed)
      {
        this.animatingStartColor1 = fillStyle2.Colors[0];
        this.animatingStartColor2 = fillStyle2.Colors[0];
        this.animatingStartColor3 = fillStyle2.Colors[0];
        this.animatingStartColor4 = fillStyle2.Colors[0];
      }
      if (fillStyle1.Colors.Length > 0)
        this.animatingEndColor1 = fillStyle1.Colors[0];
      if (fillStyle1.Colors.Length > 1)
        this.animatingEndColor2 = fillStyle1.Colors[1];
      if (fillStyle1.Colors.Length > 2)
        this.animatingEndColor3 = fillStyle1.Colors[2];
      if (fillStyle1.Colors.Length > 3)
        this.animatingEndColor4 = fillStyle1.Colors[3];
      if (colors.Length > 0)
        this.animatingEndColor1 = colors[0];
      if (colors.Length > 1)
        this.animatingEndColor2 = colors[1];
      if (colors.Length > 2)
        this.animatingEndColor3 = colors[2];
      if (colors.Length > 3)
        this.animatingEndColor4 = colors[3];
      Color[] colorArray = new Color[colors.Length];
      int colorsNumber = fillStyle1.ColorsNumber;
      Color[] colors1 = new Color[fillStyle1.ColorsNumber];
      float[] colorOffsets1 = new float[fillStyle1.ColorsNumber];
      GradientStyles gradientStyle = GradientStyles.Linear;
      double gradientAngle1 = 0.0;
      double gradientOffset1 = 0.5;
      double gradientOffset2_1 = 0.5;
      this.InitializeStateFillColors(stateType, colorsNumber, ref colors1, ref colorOffsets1, ref gradientStyle, ref gradientAngle1, ref gradientOffset1, ref gradientOffset2_1);
      this.InitializeAnimatingProperties(colorOffsets1, gradientStyle, gradientAngle1, gradientOffset1, gradientOffset2_1);
      this.animatingColor1 = colors1[0];
      if (colors1.Length > 1)
        this.animatingColor2 = colors1[1];
      if (colors1.Length > 2)
        this.animatingColor3 = colors1[2];
      if (colors1.Length > 3)
        this.animatingColor4 = colors1[3];
      colorArray[0] = this.animatingStartColor1;
      if (colorArray.Length > 1)
        colorArray[1] = this.animatingStartColor2;
      if (colorArray.Length > 2)
        colorArray[2] = this.animatingStartColor3;
      if (colorArray.Length > 3)
        colorArray[3] = this.animatingStartColor4;
      byte num = this.Radius == 0 ? (byte) 0 : this.RoundedCornersBitmask;
      switch (this.animatingStartGradientStyle)
      {
        case GradientStyles.Solid:
          SolidBrush solidBrush = new SolidBrush(colors[0]);
          if (this.Radius == 0)
          {
            g.FillRectangle((Brush) solidBrush, this.bounds);
          }
          else
          {
            GraphicsPath partiallyRoundedPath = this.PaintHelper.CreatePartiallyRoundedPath(this.bounds, this.Radius, num);
            g.FillPath((Brush) solidBrush, partiallyRoundedPath);
          }
          solidBrush.Dispose();
          break;
        case GradientStyles.Linear:
          if (this.Shape == Shapes.Circle)
          {
            this.paintHelper.DrawGlowEllipse(g, this.bounds, colors1, Color.White);
            break;
          }
          this.paintHelper.DrawGradientPartiallyRoundedRectFigure(g, this.Radius, this.bounds, num, colors1, this.animatingStartColorOffsets, GradientStyles.Linear, this.animatingStartGradientAngle, this.animatingStartGradientOffset, this.animatingStartGradientOffset2);
          break;
      }
      if (!this.IsAnimated || stateType == this.lastState)
        return;
      this.Animate(g, colors, this.bounds, colorOffsets, style, gradientAngle, gradientOffset, gradientOffset2);
      this.animating = true;
    }

    private void Animate(Graphics g, Color[] colorStops, Rectangle rect, float[] colorOffsets, GradientStyles style, double angle, double gradientOffset, double gradientOffset2)
    {
      this.animating = true;
    }

    private void InitializeAnimatingProperties(float[] colorOffsets, GradientStyles style, double gradientAngle, double gradientOffset, double gradientOffset2)
    {
      this.animatingStartColorOffsets = colorOffsets;
      this.animatingStartGradientAngle = gradientAngle;
      this.animatingStartGradientStyle = style;
      this.animatingStartGradientOffset = gradientOffset;
      this.animatingStartGradientAngle = gradientAngle;
      this.animatingStartGradientOffset2 = gradientOffset2;
    }
  }
}
