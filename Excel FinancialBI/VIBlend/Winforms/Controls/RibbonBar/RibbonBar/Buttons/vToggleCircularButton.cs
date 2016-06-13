// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vToggleCircularButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(true)]
  public class vToggleCircularButton : vToggleButton
  {
    private bool useButtonScheme;
    private ButtonDefaultColorSchemes buttonSchemes;
    private RibbonStyle buttonStyle;
    private ControlTheme myTheme;

    /// <summary>
    /// Gets or sets whether the button should use the built-in color schemes.
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets whether the button should use the built-in color schemes.")]
    [Category("Appearance")]
    public bool UseButtonScheme
    {
      get
      {
        return this.useButtonScheme;
      }
      set
      {
        this.useButtonScheme = value;
        this.Invalidate();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new CheckState Toggle
    {
      get
      {
        return base.Toggle;
      }
      set
      {
        base.Toggle = value;
      }
    }

    /// <summary>Gets or sets the button's color sheme.</summary>
    [Description("Gets or sets the button's color sheme.")]
    [Category("Appearance")]
    public ButtonDefaultColorSchemes ButtonColorScheme
    {
      get
      {
        return this.buttonSchemes;
      }
      set
      {
        this.buttonSchemes = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the button style.</summary>
    /// <value>The button style.</value>
    [Category("Appearance")]
    [Browsable(false)]
    public RibbonStyle ButtonStyle
    {
      get
      {
        for (Control parent = this.Parent; parent != null; parent = parent.Parent)
        {
          if (parent is vRibbonBar)
          {
            this.buttonStyle = (parent as vRibbonBar).RibbonStyle;
            break;
          }
        }
        return this.buttonStyle;
      }
      set
      {
        this.buttonStyle = value;
        this.Invalidate();
      }
    }

    public override ControlTheme Theme
    {
      get
      {
        return this.myTheme;
      }
      set
      {
        this.myTheme = value.CreateCopy();
        FillStyle fillStyle1 = this.myTheme.QueryFillStyleSetter("ApplicationButtonNormal");
        if (fillStyle1 != null)
          this.myTheme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.myTheme.QueryFillStyleSetter("ApplicationButtonHighlight");
        if (fillStyle2 != null)
          this.myTheme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.myTheme.QueryFillStyleSetter("ApplicationButtonPressed");
        if (fillStyle2 != null)
          this.myTheme.StylePressed.FillStyle = fillStyle3;
        this.backFill.LoadTheme(this.myTheme);
        this.backFill.IsAnimated = false;
      }
    }

    public vToggleCircularButton()
    {
      this.StyleKey = "ToggleCircularButton";
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
    }

    protected override void OnMouseMove(MouseEventArgs mevent)
    {
      base.OnMouseMove(mevent);
      if (!this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.Invalidate();
    }

    protected override void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte roundedCornersMask)
    {
      bounds = this.DrawButton(graphics, bounds, controlState);
    }

    internal Rectangle DrawButton(Graphics graphics, Rectangle bounds, ControlState controlState)
    {
      this.backFill.Opacity = this.Opacity;
      int radius = this.backFill.Radius;
      if (this.buttonStyle == RibbonStyle.Office2007)
      {
        this.backFill.Shape = Shapes.Circle;
      }
      else
      {
        this.backFill.Shape = Shapes.RoundedRectangle;
        this.backFill.Radius = 0;
      }
      Rectangle drawRect = bounds;
      --drawRect.Width;
      this.backFill.Bounds = drawRect;
      if (this.PaintFill)
      {
        if (!this.UseButtonScheme)
          this.backFill.DrawElementFill(graphics, controlState);
        else
          drawRect = this.DrawDefaultFill(graphics, controlState, drawRect);
      }
      if (this.PaintBorder)
      {
        if (!this.UseButtonScheme)
        {
          this.backFill.DrawElementBorder(graphics, controlState);
        }
        else
        {
          switch (this.ButtonColorScheme)
          {
            case ButtonDefaultColorSchemes.Blue:
              if (this.backFill.Shape != Shapes.Circle)
              {
                using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 33, 74, 163)))
                {
                  if (controlState != ControlState.Pressed)
                  {
                    graphics.DrawLine(pen, drawRect.X, drawRect.Y, drawRect.X, drawRect.Height);
                    graphics.DrawLine(pen, drawRect.X, drawRect.Y, drawRect.Width, drawRect.Y);
                    graphics.DrawLine(pen, drawRect.X + drawRect.Width, drawRect.Y, drawRect.X + drawRect.Width, drawRect.Height);
                  }
                  else
                    this.backFill.DrawElementBorder(graphics, controlState, Color.FromArgb((int) byte.MaxValue, 33, 74, 163));
                }
                using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 83, 159, 241)))
                {
                  if (controlState != ControlState.Pressed)
                  {
                    graphics.DrawLine(pen, drawRect.X + 1, drawRect.Y + 1, drawRect.X + 1, drawRect.Height);
                    graphics.DrawLine(pen, drawRect.X + 1, drawRect.Y + 1, drawRect.Width - 1, drawRect.Y + 1);
                    graphics.DrawLine(pen, drawRect.X - 1 + drawRect.Width, drawRect.Y + 1, drawRect.X + drawRect.Width - 1, drawRect.Height);
                    break;
                  }
                  drawRect.Inflate(-1, -1);
                  this.backFill.Bounds = drawRect;
                  this.backFill.DrawElementBorder(graphics, controlState, Color.FromArgb((int) byte.MaxValue, 83, 159, 241));
                  break;
                }
              }
              else
              {
                this.backFill.DrawElementBorder(graphics, controlState);
                break;
              }
            case ButtonDefaultColorSchemes.Green:
              if (this.backFill.Shape != Shapes.Circle)
              {
                using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 60, 116, 101)))
                {
                  if (controlState != ControlState.Pressed)
                  {
                    graphics.DrawLine(pen, drawRect.X, drawRect.Y, drawRect.X, drawRect.Height);
                    graphics.DrawLine(pen, drawRect.X, drawRect.Y, drawRect.Width, drawRect.Y);
                    graphics.DrawLine(pen, drawRect.X + drawRect.Width, drawRect.Y, drawRect.X + drawRect.Width, drawRect.Height);
                  }
                  else
                    this.backFill.DrawElementBorder(graphics, controlState, Color.FromArgb((int) byte.MaxValue, 60, 116, 101));
                }
                using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 75, 162, 48)))
                {
                  if (controlState != ControlState.Pressed)
                  {
                    graphics.DrawLine(pen, drawRect.X + 1, drawRect.Y + 1, drawRect.X + 1, drawRect.Height);
                    graphics.DrawLine(pen, drawRect.X + 1, drawRect.Y + 1, drawRect.Width - 1, drawRect.Y + 1);
                    graphics.DrawLine(pen, drawRect.X - 1 + drawRect.Width, drawRect.Y + 1, drawRect.X + drawRect.Width - 1, drawRect.Height);
                    break;
                  }
                  drawRect.Inflate(-1, -1);
                  this.backFill.Bounds = drawRect;
                  this.backFill.DrawElementBorder(graphics, controlState, Color.FromArgb((int) byte.MaxValue, 75, 162, 48));
                  break;
                }
              }
              else
              {
                this.backFill.DrawElementBorder(graphics, controlState);
                break;
              }
            case ButtonDefaultColorSchemes.Orange:
              if (this.backFill.Shape != Shapes.Circle)
              {
                using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 232, 162, 9)))
                {
                  if (controlState != ControlState.Pressed)
                  {
                    graphics.DrawLine(pen, drawRect.X, drawRect.Y, drawRect.X, drawRect.Height);
                    graphics.DrawLine(pen, drawRect.X, drawRect.Y, drawRect.Width, drawRect.Y);
                    graphics.DrawLine(pen, drawRect.X + drawRect.Width, drawRect.Y, drawRect.X + drawRect.Width, drawRect.Height);
                  }
                  else
                    this.backFill.DrawElementBorder(graphics, controlState, Color.FromArgb((int) byte.MaxValue, 232, 162, 9));
                }
                using (Pen pen = new Pen(Color.FromArgb((int) byte.MaxValue, 251, 212, 46)))
                {
                  if (controlState != ControlState.Pressed)
                  {
                    graphics.DrawLine(pen, drawRect.X + 1, drawRect.Y + 1, drawRect.X + 1, drawRect.Height);
                    graphics.DrawLine(pen, drawRect.X + 1, drawRect.Y + 1, drawRect.Width - 1, drawRect.Y + 1);
                    graphics.DrawLine(pen, drawRect.X - 1 + drawRect.Width, drawRect.Y + 1, drawRect.X + drawRect.Width - 1, drawRect.Height);
                    break;
                  }
                  drawRect.Inflate(-1, -1);
                  this.backFill.Bounds = drawRect;
                  this.backFill.DrawElementBorder(graphics, controlState, Color.FromArgb((int) byte.MaxValue, 251, 212, 46));
                  break;
                }
              }
              else
              {
                this.backFill.DrawElementBorder(graphics, controlState);
                break;
              }
          }
        }
      }
      Rectangle imageAndTextRect = this.GetImageAndTextRect(ref bounds, controlState);
      this.DrawImageAndText(graphics, controlState, imageAndTextRect);
      this.backFill.Radius = radius;
      return bounds;
    }

    private Rectangle DrawDefaultFill(Graphics graphics, ControlState controlState, Rectangle drawRect)
    {
      float[] colorOffsets = new float[4]{ 0.0f, 0.9f, 0.9f, 1f };
      Color[] colorStops1 = new Color[4]{ Color.FromArgb((int) byte.MaxValue, 57, 117, 211), Color.FromArgb((int) byte.MaxValue, 39, 95, 179), Color.FromArgb((int) byte.MaxValue, 52, 115, 204), Color.FromArgb((int) byte.MaxValue, 76, 145, 228) };
      switch (this.ButtonColorScheme)
      {
        case ButtonDefaultColorSchemes.Blue:
          if (controlState == ControlState.Hover || controlState == ControlState.Pressed)
            colorStops1 = new Color[4]
            {
              Color.FromArgb((int) byte.MaxValue, 67, 126, 221),
              Color.FromArgb((int) byte.MaxValue, 47, 100, 186),
              Color.FromArgb((int) byte.MaxValue, 45, 101, 188),
              Color.FromArgb((int) byte.MaxValue, 86, 153, 232)
            };
          if (this.ButtonStyle == RibbonStyle.Office2007)
            this.paintHelper.DrawEllipse(graphics, drawRect, colorStops1, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
          else
            this.paintHelper.DrawGradientRectFigure(graphics, drawRect, colorStops1, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
          Rectangle rectangle1 = new Rectangle(drawRect.X, 3 * drawRect.Height / 2, drawRect.Width, drawRect.Height / 4);
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(-5, drawRect.Bottom - 10, drawRect.Width + 11, 20);
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
            {
              pathGradientBrush.CenterColor = Color.FromArgb(110, Color.White);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(5, Color.White)
              };
              graphics.FillPath((Brush) pathGradientBrush, path);
            }
          }
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(-5, drawRect.Top - 15, drawRect.Width + 11, 20);
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
            {
              pathGradientBrush.CenterColor = Color.FromArgb(110, Color.White);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(5, Color.White)
              };
              graphics.FillPath((Brush) pathGradientBrush, path);
              break;
            }
          }
        case ButtonDefaultColorSchemes.Green:
          Color[] colorStops2 = new Color[4]{ Color.FromArgb((int) byte.MaxValue, 62, 143, 48), Color.FromArgb((int) byte.MaxValue, 43, 130, 46), Color.FromArgb((int) byte.MaxValue, 53, 142, 52), Color.FromArgb((int) byte.MaxValue, 73, 158, 60) };
          if (controlState == ControlState.Hover || controlState == ControlState.Pressed)
            colorStops2 = new Color[4]
            {
              Color.FromArgb((int) byte.MaxValue, 72, 153, 58),
              Color.FromArgb((int) byte.MaxValue, 53, 140, 56),
              Color.FromArgb((int) byte.MaxValue, 63, 152, 62),
              Color.FromArgb((int) byte.MaxValue, 83, 168, 70)
            };
          if (this.ButtonStyle == RibbonStyle.Office2007)
            this.paintHelper.DrawEllipse(graphics, drawRect, colorStops2, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
          else
            this.paintHelper.DrawGradientRectFigure(graphics, drawRect, colorStops2, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
          Rectangle rectangle2 = new Rectangle(drawRect.X, 3 * drawRect.Height / 2, drawRect.Width, drawRect.Height / 4);
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(-5, drawRect.Bottom - 10, drawRect.Width + 11, 20);
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
            {
              pathGradientBrush.CenterColor = Color.FromArgb(110, Color.White);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(5, Color.White)
              };
              graphics.FillPath((Brush) pathGradientBrush, path);
            }
          }
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(-5, drawRect.Top - 15, drawRect.Width + 11, 20);
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
            {
              pathGradientBrush.CenterColor = Color.FromArgb(110, Color.White);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(5, Color.White)
              };
              graphics.FillPath((Brush) pathGradientBrush, path);
              break;
            }
          }
        case ButtonDefaultColorSchemes.Orange:
          Color[] colorStops3 = new Color[4]{ Color.FromArgb((int) byte.MaxValue, 244, 195, 33), Color.FromArgb((int) byte.MaxValue, 240, 184, 25), Color.FromArgb((int) byte.MaxValue, 243, 193, 40), Color.FromArgb((int) byte.MaxValue, 245, 202, 59) };
          if (controlState == ControlState.Hover || controlState == ControlState.Pressed)
            colorStops3 = new Color[4]
            {
              Color.FromArgb((int) byte.MaxValue, 249, 200, 38),
              Color.FromArgb((int) byte.MaxValue, 245, 189, 30),
              Color.FromArgb((int) byte.MaxValue, 248, 198, 44),
              Color.FromArgb((int) byte.MaxValue, 248, 216, 111)
            };
          if (this.ButtonStyle == RibbonStyle.Office2007)
            this.paintHelper.DrawEllipse(graphics, drawRect, colorStops3, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
          else
            this.paintHelper.DrawGradientRectFigure(graphics, drawRect, colorStops3, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
          Rectangle rectangle3 = new Rectangle(drawRect.X, 3 * drawRect.Height / 2, drawRect.Width, drawRect.Height / 4);
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(-5, drawRect.Bottom - 10, drawRect.Width + 11, 20);
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
            {
              pathGradientBrush.CenterColor = Color.FromArgb(110, Color.White);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(5, Color.White)
              };
              graphics.FillPath((Brush) pathGradientBrush, path);
            }
          }
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(-5, drawRect.Top - 15, drawRect.Width + 11, 20);
            using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
            {
              pathGradientBrush.CenterColor = Color.FromArgb(110, Color.White);
              pathGradientBrush.SurroundColors = new Color[1]
              {
                Color.FromArgb(5, Color.White)
              };
              graphics.FillPath((Brush) pathGradientBrush, path);
              break;
            }
          }
      }
      return drawRect;
    }
  }
}
