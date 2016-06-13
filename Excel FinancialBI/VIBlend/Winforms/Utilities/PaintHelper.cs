// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.PaintHelper
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class PaintHelper
  {
    private float opacity = 1f;

    public static Rectangle OfficeArrowRectFromBounds(Rectangle bounds)
    {
      Rectangle rectangle = new Rectangle();
      rectangle.Height = 3;
      rectangle.Width = 5;
      rectangle.X = bounds.X + (bounds.Width - rectangle.Width) / 2 + 1;
      rectangle.Y = bounds.Y + (bounds.Height - rectangle.Height) / 2 + 1;
      return rectangle;
    }

    public static void SetOpacityToImage(Bitmap bitmap, byte value)
    {
      for (int y = 0; y < bitmap.Height; ++y)
      {
        for (int x = 0; x < bitmap.Width; ++x)
        {
          int num = (int) bitmap.GetPixel(x, y).A - (int) value;
          if (num < 0)
            num = 0;
          bitmap.SetPixel(x, y, Color.FromArgb((int) (byte) num, bitmap.GetPixel(x, y)));
        }
      }
    }

    public static Rectangle GetButtonRect(Rectangle bounds, bool flat)
    {
      return PaintHelper.GetButtonRect(bounds, flat, SystemInformation.HorizontalScrollBarArrowWidth - 1);
    }

    public static Rectangle GetButtonRect(Rectangle bounds, bool flat, int buttonWidth)
    {
      int num1 = SystemInformation.Border3DSize.Width;
      int num2 = SystemInformation.Border3DSize.Height;
      if (flat)
        num1 = num2 = 1;
      return new Rectangle(bounds.Right - buttonWidth - num1, bounds.Top + num2, buttonWidth, bounds.Height - num2 * 2);
    }

    public static void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, Color backColor, Color foreColor)
    {
      if ((state & DrawItemState.Focus) != DrawItemState.None)
        graphics.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
      else if ((state & DrawItemState.Selected) != DrawItemState.None)
      {
        graphics.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(backColor))
          graphics.FillRectangle((Brush) solidBrush, bounds);
      }
    }

    public static Rectangle GetContentRect(Rectangle bounds, bool flat)
    {
      Rectangle buttonRect = PaintHelper.GetButtonRect(bounds, flat);
      int num1 = SystemInformation.Border3DSize.Width;
      int num2 = SystemInformation.Border3DSize.Height;
      if (flat)
        num1 = num2 = 1;
      return new Rectangle(bounds.Left + num1, bounds.Top + num2, bounds.Width - num1 * 2 - buttonRect.Width, bounds.Height - num2 * 2);
    }

    public void DrawWin7Glow(Graphics g, Rectangle rectangle, int offsetX, int offsetY, Color glowColor)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(offsetX - 5, offsetY, rectangle.Width + 11, rectangle.Height * 2);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.CenterColor = glowColor;
          pathGradientBrush.SurroundColors = new Color[2]
          {
            Color.FromArgb((int) byte.MaxValue, glowColor),
            Color.FromArgb(0, glowColor)
          };
          g.FillPath((Brush) pathGradientBrush, path);
        }
      }
    }

    public void DrawWin7Glow(Graphics g, Rectangle rectangle, int offsetX, int offsetY, Color glowColor, Color[] surroundColors)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(offsetX - 5, offsetY, rectangle.Width + 11, rectangle.Height * 2);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.CenterColor = glowColor;
          pathGradientBrush.SurroundColors = surroundColors;
          g.FillPath((Brush) pathGradientBrush, path);
        }
      }
    }

    public void DrawGlow(Graphics g, Rectangle rectangle, Color glowColor, Color centerColor)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(-5, rectangle.Height / 2 - 10, rectangle.Width + 11, rectangle.Height + 11);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.CenterColor = centerColor;
          pathGradientBrush.SurroundColors = new Color[1]
          {
            Color.FromArgb(0, glowColor)
          };
          g.FillPath((Brush) pathGradientBrush, path);
        }
      }
    }

    public void DrawGlow(Graphics g, Rectangle rectangle, Color glowColor)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(-5, rectangle.Height / 2 - 10, rectangle.Width + 11, rectangle.Height + 11);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.CenterColor = Color.FromArgb(128, glowColor);
          pathGradientBrush.SurroundColors = new Color[1]
          {
            Color.FromArgb(0, glowColor)
          };
          g.FillPath((Brush) pathGradientBrush, path);
        }
      }
    }

    public void DrawGlowEllipse(Graphics g, Rectangle rectangle, Color[] colors, Color centerColor)
    {
      rectangle.Inflate(-1, -1);
      this.DrawCircleButton(g, rectangle, colors);
    }

    private void DrawCircleButton(Graphics g, Rectangle r, Color[] colors)
    {
      Rectangle rect1 = r;
      rect1.Inflate(-1, -1);
      Rectangle rect2 = r;
      rect2.Offset(1, 1);
      rect2.Inflate(2, 2);
      Color color1 = colors[0];
      Color color2 = colors[1];
      Color color3 = colors.Length <= 2 ? colors[1] : colors[2];
      Color whiteSmoke = Color.WhiteSmoke;
      GraphicsPath path1;
      using (path1 = new GraphicsPath())
      {
        path1.AddEllipse(rect2);
        PathGradientBrush pathGradientBrush;
        using (pathGradientBrush = new PathGradientBrush(path1))
        {
          pathGradientBrush.WrapMode = WrapMode.Clamp;
          pathGradientBrush.CenterPoint = new PointF((float) (rect2.Left + rect2.Width / 2), (float) (rect2.Top + rect2.Height / 2));
          pathGradientBrush.CenterColor = Color.FromArgb(180, Color.Black);
          Color[] colorArray = new Color[1]{ Color.Transparent };
          pathGradientBrush.SurroundColors = colorArray;
          pathGradientBrush.Blend = new Blend(3)
          {
            Factors = new float[3]{ 0.0f, 1f, 1f },
            Positions = new float[3]{ 0.0f, 0.2f, 1f }
          };
          g.FillPath((Brush) pathGradientBrush, path1);
        }
      }
      using (Pen pen = new Pen(color1, 1f))
        g.DrawEllipse(pen, r);
      GraphicsPath path2;
      using (path2 = new GraphicsPath())
      {
        path2.AddEllipse(r);
        PathGradientBrush pathGradientBrush;
        using (pathGradientBrush = new PathGradientBrush(path2))
        {
          pathGradientBrush.WrapMode = WrapMode.Clamp;
          pathGradientBrush.CenterPoint = new PointF(Convert.ToSingle(r.Left + r.Width / 2), Convert.ToSingle(r.Bottom));
          pathGradientBrush.CenterColor = color3;
          Color[] colorArray = new Color[1]{ color2 };
          pathGradientBrush.SurroundColors = colorArray;
          pathGradientBrush.Blend = new Blend(3)
          {
            Factors = new float[3]{ 0.0f, 0.8f, 1f },
            Positions = new float[3]{ 0.0f, 0.5f, 1f }
          };
          g.FillPath((Brush) pathGradientBrush, path2);
        }
      }
      Rectangle rect3 = new Rectangle(0, 0, r.Width / 2, r.Height / 2);
      rect3 = new Rectangle(r.X + (r.Width - rect3.Width) / 2, r.Y + r.Height / 2, rect3.Width, rect3.Height);
      GraphicsPath path3;
      using (path3 = new GraphicsPath())
      {
        path3.AddEllipse(rect3);
        PathGradientBrush pathGradientBrush;
        using (pathGradientBrush = new PathGradientBrush(path3))
        {
          pathGradientBrush.WrapMode = WrapMode.Clamp;
          pathGradientBrush.CenterPoint = new PointF(Convert.ToSingle(r.Left + r.Width / 2), Convert.ToSingle(r.Bottom));
          pathGradientBrush.CenterColor = Color.White;
          Color[] colorArray = new Color[1]{ Color.Transparent };
          pathGradientBrush.SurroundColors = colorArray;
          g.FillPath((Brush) pathGradientBrush, path3);
        }
      }
      GraphicsPath path4;
      using (path4 = new GraphicsPath())
      {
        int num1 = 160;
        int num2 = 180 + (180 - num1) / 2;
        path4.AddArc(rect1, (float) num2, (float) num1);
        Point point1 = Point.Round(path4.PathData.Points[0]);
        Point point2 = Point.Round(path4.PathData.Points[path4.PathData.Points.Length - 1]);
        Point point3 = new Point(rect1.Left + rect1.Width / 2, point2.Y - 3);
        Point[] points = new Point[3]{ point2, point3, point1 };
        path4.AddCurve(points);
        PathGradientBrush pathGradientBrush;
        using (pathGradientBrush = new PathGradientBrush(path4))
        {
          pathGradientBrush.WrapMode = WrapMode.Clamp;
          pathGradientBrush.CenterPoint = (PointF) point3;
          pathGradientBrush.CenterColor = Color.Transparent;
          Color[] colorArray = new Color[1]{ whiteSmoke };
          pathGradientBrush.SurroundColors = colorArray;
          Blend blend = new Blend(3) { Factors = new float[3]{ 0.3f, 0.8f, 1f } };
          blend.Positions = new float[3]{ 0.0f, 0.5f, 1f };
          pathGradientBrush.Blend = blend;
          g.FillPath((Brush) pathGradientBrush, path4);
        }
        LinearGradientBrush linearGradientBrush;
        using (linearGradientBrush = new LinearGradientBrush(new Point(r.Left, r.Top), new Point(r.Left, point1.Y), Color.White, Color.Transparent))
        {
          Blend blend = new Blend(4) { Factors = new float[4]{ 0.0f, 0.4f, 0.8f, 1f }, Positions = new float[4]{ 0.0f, 0.3f, 0.4f, 1f } };
          linearGradientBrush.Blend = blend;
          g.FillPath((Brush) linearGradientBrush, path4);
        }
      }
      GraphicsPath path5;
      using (path5 = new GraphicsPath())
      {
        int num1 = 160;
        int num2 = 180 + (180 - num1) / 2;
        path5.AddArc(rect1, (float) num2, (float) num1);
        using (Pen pen = new Pen(Color.White))
          g.DrawPath(pen, path5);
      }
      GraphicsPath path6;
      using (path6 = new GraphicsPath())
      {
        int num1 = 160;
        int num2 = (180 - num1) / 2;
        path6.AddArc(rect1, (float) num2, (float) num1);
        Point point = Point.Round(path6.PathData.Points[0]);
        Rectangle rect4 = rect1;
        rect4.Inflate(-1, -1);
        int num3 = 160;
        int num4 = (180 - num3) / 2;
        path6.AddArc(rect4, (float) num4, (float) num3);
        LinearGradientBrush linearGradientBrush;
        using (linearGradientBrush = new LinearGradientBrush(new Point(rect1.Left, rect1.Bottom), new Point(rect1.Left, point.Y - 1), whiteSmoke, Color.FromArgb(50, whiteSmoke)))
          g.FillPath((Brush) linearGradientBrush, path6);
      }
    }

    private void DrawEllipsesBorder(Graphics g, Color[] colors, SmoothingMode smoothingMode, ref Rectangle rect, GraphicsPath path, ref PathGradientBrush brush, ref Color[] colorArray)
    {
      path.Reset();
      path.AddEllipse(rect);
      ++rect.Width;
      this.DrawFillGlowGradientRectangleBackground(rect, path, colors, 1f, g);
      --rect.Width;
      GraphicsPath path1 = new GraphicsPath();
      rect.Offset(-1, rect.Height / 2);
      path1.AddEllipse(rect);
      brush = new PathGradientBrush(path1);
      ColorBlend colorBlend = new ColorBlend(3);
      colorBlend.Positions = new float[3]{ 0.0f, 0.6f, 1f };
      colorArray = new Color[3]
      {
        Color.Transparent,
        Color.Transparent,
        Color.White
      };
      colorBlend.Colors = colorArray;
      brush.InterpolationColors = colorBlend;
      brush.Dispose();
      brush.Dispose();
      path.Dispose();
      g.SmoothingMode = smoothingMode;
    }

    internal void DrawEllipsesFill(Graphics g, ref Rectangle rectangle, Color[] colors, ref Color centerColor, out SmoothingMode smoothingMode, out Rectangle rect, out GraphicsPath path, out PathGradientBrush brush, out Color[] colorArray)
    {
      Rectangle rectangle1 = rectangle;
      rectangle1.X += 2;
      rectangle1.Y += 2;
      rectangle1.Width -= 2;
      rectangle1.Height -= 2;
      smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      rect = new Rectangle(rectangle1.X + 2, rectangle1.Y + 2, rectangle1.Width - 2, rectangle1.Height - 2);
      path = new GraphicsPath();
      path.AddEllipse(rect);
      brush = new PathGradientBrush(path);
      brush.CenterColor = centerColor;
      colorArray = new Color[1]{ Color.Empty };
      brush.SurroundColors = colorArray;
      brush.FocusScales = new PointF(0.85f, 0.85f);
      brush.Dispose();
      rect = new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height);
      path.Reset();
      path.AddEllipse(rect);
      g.SmoothingMode = SmoothingMode.AntiAlias;
      g.SmoothingMode = smoothingMode;
    }

    public virtual object FillGlowPathForeground(Rectangle bounds, GraphicsPath path, Color[] colors, float borderWidth, Graphics graphics)
    {
      if (bounds.Width > 0 && bounds.Height > 0 && (double) borderWidth > 0.0)
      {
        if (colors[0] == Color.Empty)
          return (object) null;
        SmoothingMode smoothingMode = graphics.SmoothingMode;
        Pen pen = new Pen(colors[1], borderWidth);
        graphics.DrawPath(pen, path);
        pen.Dispose();
        graphics.SmoothingMode = smoothingMode;
      }
      return (object) null;
    }

    private void DrawFillGlowGradientRectangleBackground(Rectangle bounds, GraphicsPath backPath, Color[] colors, float borderWidth, Graphics graphics)
    {
      Orientation orientation = Orientation.Horizontal;
      float num1 = 0.5f;
      float num2 = 1f;
      int offset = 15;
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return;
      Rectangle boundsToUseForBrushes = bounds;
      Color color = colors[0];
      Color color2 = colors[1];
      float gradientOffsetFactor = 10f;
      int num3;
      int gradientOffset;
      Rectangle rectangle2;
      Rectangle rectangle3;
      Rectangle rectangle4;
      int num3_1;
      PaintHelper.CalculateEllipses(orientation, offset, ref boundsToUseForBrushes, ref gradientOffsetFactor, out num3, out gradientOffset, out rectangle2, out rectangle3, out rectangle4, out num3_1);
      float num4 = (double) num1 > 0.0 ? num1 / 100f : 0.0f;
      float num5 = (double) num2 > 0.0 ? num2 / 100f : 0.0f;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(boundsToUseForBrushes.Left - 1, boundsToUseForBrushes.Top - 1, boundsToUseForBrushes.Width + 2, boundsToUseForBrushes.Height + 2), color, color2, (float) num3);
      linearGradientBrush.Blend = new Blend()
      {
        Factors = new float[3]
        {
          0.0f,
          num4,
          num5
        },
        Positions = new float[3]
        {
          0.0f,
          gradientOffsetFactor,
          1f
        }
      };
      this.DrawGradientPathFigure(graphics, backPath, bounds, colors, new float[4]
      {
        0.0f,
        0.5f,
        0.5f,
        1f
      }, GradientStyles.Linear, 90.0, 0.5, 0.5);
      Blend blend = new Blend();
      blend.Factors = new float[3]{ 1f, 1f, num5 };
      float[] numArray = new float[3]{ 0.0f, num5, 1f };
      blend.Positions = numArray;
      linearGradientBrush.Blend = blend;
      Region savedRegion = PaintHelper.SetClipRegion(graphics, new Region(rectangle2), CombineMode.Exclude);
      this.DrawGradientPathFigure(graphics, backPath, bounds, colors, new float[4]
      {
        0.0f,
        0.5f,
        0.5f,
        1f
      }, GradientStyles.Linear, 90.0, 0.5, 0.5);
      PaintHelper.DrawCenterEllipse(backPath, colors, graphics, rectangle4);
      PaintHelper.RestoreClipRegion(graphics, savedRegion);
      if ((double) borderWidth > 0.0)
        graphics.DrawPath(new Pen(color), backPath);
      linearGradientBrush.Dispose();
    }

    private static void CalculateEllipses(Orientation orientation, int offset, ref Rectangle boundsToUseForBrushes, ref float gradientOffsetFactor, out int num, out int gradientOffset, out Rectangle rectangle2, out Rectangle rectangle3, out Rectangle rectangle4, out int num3)
    {
      if (orientation == Orientation.Horizontal)
      {
        gradientOffsetFactor = (float) (offset / boundsToUseForBrushes.Height);
        gradientOffset = offset;
        rectangle2 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top, boundsToUseForBrushes.Width, gradientOffset);
        rectangle3 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top + gradientOffset - 1, boundsToUseForBrushes.Width, boundsToUseForBrushes.Height - gradientOffset + 1);
        num3 = (int) (0.100000001490116 * (double) rectangle3.Width);
        rectangle4 = new Rectangle(rectangle3.Left - num3, rectangle3.Top, rectangle3.Width + 2 * num3, rectangle3.Height * 2);
        num = 90;
      }
      else
      {
        gradientOffsetFactor = (float) (offset / boundsToUseForBrushes.Width);
        gradientOffset = offset;
        rectangle2 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top, gradientOffset, boundsToUseForBrushes.Height);
        rectangle3 = new Rectangle(boundsToUseForBrushes.Left + gradientOffset - 1, boundsToUseForBrushes.Top, boundsToUseForBrushes.Width - gradientOffset + 1, boundsToUseForBrushes.Height);
        num3 = (int) (0.100000001490116 * (double) rectangle3.Height);
        rectangle4 = new Rectangle(rectangle3.Left, rectangle3.Top - num3, rectangle3.Width * 2, rectangle3.Height + 2 * num3);
        num = 0;
      }
    }

    private static Rectangle DrawCenterEllipse(GraphicsPath backPath, Color[] colors, Graphics graphics, Rectangle rectangle4)
    {
      if (rectangle4.Width > 0 && rectangle4.Height > 0)
      {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(rectangle4);
        PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
        pathGradientBrush.CenterColor = ControlPaint.LightLight(colors[0]);
        pathGradientBrush.SurroundColors = new Color[1]
        {
          Color.Transparent
        };
        graphics.FillPath((Brush) pathGradientBrush, backPath);
        pathGradientBrush.Dispose();
        path.Dispose();
      }
      return rectangle4;
    }

    private object FillGlowPathBackground(Rectangle bounds, GraphicsPath path, Color[] colors, float borderWidth, Graphics graphics)
    {
      if (bounds.Width > 0 && bounds.Height > 0 && colors[0] != Color.Empty)
      {
        SmoothingMode smoothingMode = graphics.SmoothingMode;
        Brush brush = (Brush) new SolidBrush(colors[0]);
        graphics.FillPath(brush, path);
        if ((double) borderWidth > 0.0)
        {
          Pen pen = new Pen(brush, borderWidth);
          graphics.DrawPath(pen, path);
          pen.Dispose();
        }
        brush.Dispose();
        graphics.SmoothingMode = smoothingMode;
      }
      return (object) null;
    }

    public static Region SetClipRegion(Graphics graphics, Region region, CombineMode combineMode)
    {
      Region region1 = graphics.Clip.Clone();
      graphics.SetClip(region, combineMode);
      return region1;
    }

    public static void RestoreClipRegion(Graphics graphics, Region savedRegion)
    {
      Region clip = graphics.Clip;
      graphics.SetClip(savedRegion, CombineMode.Replace);
      clip.Dispose();
    }

    public static void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, string text, ImageList imageList, int imageIndex, Font font, Color backColor, Color foreColor, RightToLeft rightToLeft)
    {
      PaintHelper.DrawItem(graphics, bounds, state, text, imageList, imageIndex, font, backColor, foreColor, 0, rightToLeft);
    }

    public static void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, string text, ImageList imageList, int imageIndex, Font font, Color backColor, Color foreColor, int textStartPos, RightToLeft rightToLeft)
    {
      PaintHelper.DrawItem(graphics, bounds, state, backColor, foreColor);
      int num = 0;
      if (imageList != null && imageIndex >= 0 && imageIndex < imageList.Images.Count)
      {
        Rectangle rectangle = new Rectangle(bounds.X + 1, bounds.Y + (bounds.Height - imageList.ImageSize.Height) / 2, imageList.ImageSize.Width, imageList.ImageSize.Height);
        imageList.Draw(graphics, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, imageIndex);
        num = imageList.ImageSize.Width + 2;
      }
      using (StringFormat format = new StringFormat())
      {
        format.Alignment = StringAlignment.Near;
        format.LineAlignment = StringAlignment.Center;
        format.FormatFlags = StringFormatFlags.NoWrap;
        format.Trimming = StringTrimming.EllipsisCharacter;
        if (rightToLeft == RightToLeft.Yes)
          format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        bounds.X += num + textStartPos;
        bounds.Width -= num + textStartPos;
        if (text == null)
          return;
        using (SolidBrush solidBrush = new SolidBrush(foreColor))
          graphics.DrawString(text, font, (Brush) solidBrush, (RectangleF) bounds, format);
      }
    }

    public virtual void DrawPath(Graphics g, GraphicsPath path, Color color, PenAlignment penAlignment, float penWidth)
    {
      this.DrawPath(g, path, color, penAlignment, penWidth, (Brush) null);
    }

    public void DrawPath(Graphics g, GraphicsPath path, Color color, PenAlignment penAlignment, float penWidth, Brush brush)
    {
      using (Pen pen = new Pen(color))
      {
        pen.Width = penWidth;
        pen.Alignment = penAlignment;
        if (brush != null)
          pen.Brush = brush;
        g.DrawPath(pen, path);
      }
    }

    public void DrawPathArrowFigure(Graphics g, Color color, Rectangle bounds, ArrowDirection direction)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Point[] pointArray = new Point[3];
      int x1 = bounds.X + bounds.Width / 2;
      int y1 = bounds.Y + bounds.Height / 2;
      int x2 = bounds.X;
      int x3 = bounds.X + bounds.Width - 1;
      int y2 = bounds.Y;
      int y3 = bounds.Y + bounds.Height - 1;
      if (bounds.Width % 2 == 0)
        ++x3;
      if (bounds.Height % 2 == 0)
        ++y3;
      using (new SolidBrush(color))
      {
        using (Pen pen = new Pen(color))
        {
          switch (direction)
          {
            case ArrowDirection.Left:
              pointArray = new Point[3]
              {
                new Point(x3, y2),
                new Point(x2, y1),
                new Point(x3, y3)
              };
              break;
            case ArrowDirection.Up:
              pointArray = new Point[3]
              {
                new Point(x2, y3),
                new Point(x1, y2),
                new Point(x3, y3)
              };
              break;
            case ArrowDirection.Right:
              pointArray = new Point[3]
              {
                new Point(x2, y2),
                new Point(x3, y1),
                new Point(x2, y3)
              };
              break;
            case ArrowDirection.Down:
              pointArray = new Point[3]
              {
                new Point(x2, y2),
                new Point(x1, y3),
                new Point(x3, y2)
              };
              break;
          }
          g.DrawLine(pen, pointArray[0], pointArray[1]);
          g.DrawLine(pen, pointArray[1], pointArray[2]);
        }
      }
      g.SmoothingMode = smoothingMode;
    }

    public void DrawArrowFigure(Graphics g, Color color, Rectangle bounds, ArrowDirection direction)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.None;
      Point[] points = new Point[3];
      int x1 = bounds.X + bounds.Width / 2;
      int y1 = bounds.Y + bounds.Height / 2;
      int x2 = bounds.X;
      int x3 = bounds.X + bounds.Width - 1;
      int y2 = bounds.Y;
      int y3 = bounds.Y + bounds.Height - 1;
      if (bounds.Width % 2 == 0)
        ++x3;
      if (bounds.Height % 2 == 0)
        ++y3;
      using (SolidBrush solidBrush = new SolidBrush(color))
      {
        using (Pen pen = new Pen(color))
        {
          switch (direction)
          {
            case ArrowDirection.Left:
              points = new Point[3]
              {
                new Point(x3, y2),
                new Point(x2, y1),
                new Point(x3, y3)
              };
              break;
            case ArrowDirection.Up:
              points = new Point[3]
              {
                new Point(x2, y3),
                new Point(x1, y2),
                new Point(x3, y3)
              };
              break;
            case ArrowDirection.Right:
              points = new Point[3]
              {
                new Point(x2, y2),
                new Point(x3, y1),
                new Point(x2, y3)
              };
              break;
            case ArrowDirection.Down:
              points = new Point[3]
              {
                new Point(x2, y2),
                new Point(x3, y2),
                new Point(x1, y3)
              };
              break;
          }
          g.FillPolygon((Brush) solidBrush, points);
          g.DrawPolygon(pen, points);
        }
      }
      g.SmoothingMode = smoothingMode;
    }

    public Rectangle DrawImageAndTextRectangle(Graphics g, bool wrapText, int textImageOffset, Rectangle rect, bool draw, Font font, SolidBrush textBrush, ContentAlignment imageAlign, ContentAlignment textAlign, string text, Image image, TextImageRelation relation)
    {
      SizeF sizeF = g.MeasureString(text, font);
      int width = Size.Ceiling(sizeF).Width;
      int height = Size.Ceiling(sizeF).Height;
      int x = rect.X;
      int y1 = rect.Y;
      Rectangle Bounds = Rectangle.Empty;
      int top = y1;
      int bottom = y1 + rect.Height - image.Height;
      int middle = y1 + rect.Height / 2 - image.Height / 2;
      int left = x;
      int center = x + rect.Width / 2 - image.Width / 2;
      int right = x + rect.Width - image.Width;
      switch (relation)
      {
        case TextImageRelation.Overlay:
          Bounds = rect;
          if (draw)
          {
            PaintHelper.DrawImageAndTextOverlay(g, imageAlign, image, top, bottom, middle, left, center, right);
            break;
          }
          break;
        case TextImageRelation.ImageAboveText:
          Bounds = new Rectangle(x, y1 + textImageOffset + image.Height, rect.Width, rect.Height - image.Height);
          if (draw)
          {
            PaintHelper.DrawImageAboveText(g, imageAlign, image, top, left, center, right);
            break;
          }
          break;
        case TextImageRelation.TextAboveImage:
          Bounds = new Rectangle(x, y1, rect.Width, rect.Height - image.Height);
          if (draw)
          {
            int y2 = y1;
            switch (textAlign)
            {
              case ContentAlignment.BottomCenter:
              case ContentAlignment.BottomRight:
              case ContentAlignment.BottomLeft:
                y2 = bottom;
                break;
              case ContentAlignment.MiddleRight:
              case ContentAlignment.MiddleLeft:
              case ContentAlignment.MiddleCenter:
                y2 = Bounds.Bottom + textImageOffset;
                break;
              case ContentAlignment.TopLeft:
              case ContentAlignment.TopCenter:
              case ContentAlignment.TopRight:
                y2 = top + height + textImageOffset;
                break;
            }
            PaintHelper.DrawTextAboveImage(g, textImageOffset, imageAlign, image, height, y2, left, center, right);
            break;
          }
          break;
        case TextImageRelation.ImageBeforeText:
          Bounds = new Rectangle(x + textImageOffset + image.Width, y1, rect.Width - textImageOffset - image.Width, rect.Height);
          if (draw)
          {
            PaintHelper.DrawImageBeforeText(g, imageAlign, image, top, bottom, middle, left);
            break;
          }
          break;
        case TextImageRelation.TextBeforeImage:
          Bounds = new Rectangle(x + textImageOffset, y1, rect.Width - textImageOffset - image.Width, rect.Height);
          if (draw)
          {
            rect = PaintHelper.DrawTextBeforeImage(g, textImageOffset, rect, imageAlign, image, x, top, bottom, middle);
            break;
          }
          break;
      }
      if (draw)
        this.DrawText(g, Bounds, wrapText, textBrush.Color, text, font, textAlign);
      return rect;
    }

    /// <summary>
    /// CreatePartiallyRoundedPath - Creates a Rounded Rectangle Path with customizable corners
    /// </summary>
    /// <param name="bounds">Rectangle Bounds</param>
    /// <param name="radius">Rounded Corner Radius</param>
    /// <param name="roundedCornersBitmask">
    ///    RoundedCornersBitmask:
    ///    bit0 = 1 -- topLeft corner is rounded
    ///    bit1 = 1 -- topRight corner is rounded
    ///    bit2 = 1 -- bottomLeft corner is rounded
    ///    bit3 = 1 -- bottomRight corner is rounded
    /// </param>
    /// <returns></returns>
    public GraphicsPath CreatePartiallyRoundedPath(Rectangle bounds, int radius, byte roundedCornersBitmask)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if (bounds.Height <= 0 || bounds.Width <= 0)
        return graphicsPath;
      if ((double) radius <= 0.0)
      {
        graphicsPath.AddRectangle(bounds);
        graphicsPath.CloseFigure();
        return graphicsPath;
      }
      float num = (float) radius * 2f;
      SizeF size = new SizeF(num, num);
      RectangleF rect = new RectangleF((PointF) bounds.Location, size);
      if (((int) roundedCornersBitmask & 1) == 1)
        graphicsPath.AddArc(rect, 180f, 90f);
      else
        graphicsPath.AddLine(bounds.Left, bounds.Top, bounds.Left + 1, bounds.Top);
      rect.X = (float) bounds.Right - num;
      if (((int) roundedCornersBitmask & 2) == 2)
        graphicsPath.AddArc(rect, 270f, 90f);
      else
        graphicsPath.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Top + 1);
      rect.Y = (float) bounds.Bottom - num;
      if (((int) roundedCornersBitmask & 8) == 8)
        graphicsPath.AddArc(rect, 0.0f, 90f);
      else
        graphicsPath.AddLine(bounds.Right, bounds.Bottom, bounds.Right - 1, bounds.Bottom);
      rect.X = (float) bounds.Left;
      if (((int) roundedCornersBitmask & 4) == 4)
        graphicsPath.AddArc(rect, 90f, 90f);
      else
        graphicsPath.AddLine(bounds.Left + 1, bounds.Bottom, bounds.Left, bounds.Bottom);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    public StringAlignment GetHorizontalAlignment(ContentAlignment TextAlignment)
    {
      switch (TextAlignment)
      {
        case ContentAlignment.BottomCenter:
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
          return StringAlignment.Center;
        case ContentAlignment.BottomRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.TopRight:
          return StringAlignment.Far;
        case ContentAlignment.BottomLeft:
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
          return StringAlignment.Near;
        default:
          return StringAlignment.Near;
      }
    }

    public StringAlignment GetVerticalAlignment(ContentAlignment TextAlignment)
    {
      switch (TextAlignment)
      {
        case ContentAlignment.BottomCenter:
        case ContentAlignment.BottomRight:
        case ContentAlignment.BottomLeft:
          return StringAlignment.Far;
        case ContentAlignment.MiddleRight:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.MiddleCenter:
          return StringAlignment.Center;
        case ContentAlignment.TopLeft:
        case ContentAlignment.TopCenter:
        case ContentAlignment.TopRight:
          return StringAlignment.Near;
        default:
          return StringAlignment.Near;
      }
    }

    public GraphicsPath GetRoundedPathRect(Rectangle bounds, int radius)
    {
      return this.CreatePartiallyRoundedPath(bounds, radius, byte.MaxValue);
    }

    private static Rectangle DrawTextBeforeImage(Graphics g, int textImageOffset, Rectangle rect, ContentAlignment imageAlign, Image image, int x, int top, int bottom, int middle)
    {
      switch (imageAlign)
      {
        case ContentAlignment.BottomCenter:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, bottom);
          break;
        case ContentAlignment.BottomRight:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, bottom);
          break;
        case ContentAlignment.MiddleRight:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, middle);
          break;
        case ContentAlignment.BottomLeft:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, bottom);
          break;
        case ContentAlignment.TopLeft:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, top);
          break;
        case ContentAlignment.TopCenter:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, top);
          break;
        case ContentAlignment.TopRight:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, top);
          break;
        case ContentAlignment.MiddleLeft:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, middle);
          break;
        case ContentAlignment.MiddleCenter:
          g.DrawImage(image, x + rect.Width - textImageOffset - image.Width, middle);
          break;
      }
      return rect;
    }

    private static void DrawTextAboveImage(Graphics g, int textImageOffset, ContentAlignment imageAlign, Image image, int height, int y, int left, int center, int right)
    {
      switch (imageAlign)
      {
        case ContentAlignment.BottomCenter:
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
          g.DrawImage(image, center, y + textImageOffset + height - image.Height);
          break;
        case ContentAlignment.BottomRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.TopRight:
          g.DrawImage(image, right, y + textImageOffset + height - image.Height);
          break;
        case ContentAlignment.BottomLeft:
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
          g.DrawImage(image, left, y + textImageOffset + height - image.Height);
          break;
      }
    }

    private static void DrawImageAndTextOverlay(Graphics g, ContentAlignment imageAlign, Image image, int top, int bottom, int middle, int left, int center, int right)
    {
      switch (imageAlign)
      {
        case ContentAlignment.BottomCenter:
          g.DrawImage(image, center, bottom);
          break;
        case ContentAlignment.BottomRight:
          g.DrawImage(image, right, bottom);
          break;
        case ContentAlignment.MiddleRight:
          g.DrawImage(image, right, middle);
          break;
        case ContentAlignment.BottomLeft:
          g.DrawImage(image, left, bottom);
          break;
        case ContentAlignment.TopLeft:
          g.DrawImage(image, left, top);
          break;
        case ContentAlignment.TopCenter:
          g.DrawImage(image, center, top);
          break;
        case ContentAlignment.TopRight:
          g.DrawImage(image, right, top);
          break;
        case ContentAlignment.MiddleLeft:
          g.DrawImage(image, left, middle);
          break;
        case ContentAlignment.MiddleCenter:
          g.DrawImage(image, center, middle);
          break;
      }
    }

    private static void DrawImageAboveText(Graphics g, ContentAlignment imageAlign, Image image, int top, int left, int center, int right)
    {
      switch (imageAlign)
      {
        case ContentAlignment.BottomCenter:
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
          g.DrawImage(image, center, top);
          break;
        case ContentAlignment.BottomRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.TopRight:
          g.DrawImage(image, right, top);
          break;
        case ContentAlignment.BottomLeft:
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
          g.DrawImage(image, left, top);
          break;
      }
    }

    private static void DrawImageBeforeText(Graphics g, ContentAlignment imageAlign, Image image, int top, int bottom, int middle, int left)
    {
      switch (imageAlign)
      {
        case ContentAlignment.BottomCenter:
        case ContentAlignment.BottomRight:
        case ContentAlignment.BottomLeft:
          g.DrawImage(image, left, bottom);
          break;
        case ContentAlignment.MiddleRight:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.MiddleCenter:
          g.DrawImage(image, left, middle);
          break;
        case ContentAlignment.TopLeft:
        case ContentAlignment.TopCenter:
        case ContentAlignment.TopRight:
          g.DrawImage(image, left, top);
          break;
      }
    }

    public void DrawText(Graphics g, Rectangle Bounds, bool wrap, Color foreColor, string text, Font font, ContentAlignment textAlign)
    {
      using (Brush brush = (Brush) new SolidBrush(foreColor))
      {
        StringFormat stringFormat = this.GetStringFormat(textAlign);
        stringFormat.FormatFlags = StringFormatFlags.FitBlackBox;
        if (!wrap)
          stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
        stringFormat.Trimming = StringTrimming.EllipsisCharacter;
        stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
        g.DrawString(text, font, brush, (RectangleF) Bounds, stringFormat);
      }
    }

    public virtual void DrawGradientRectFigure(Graphics g, Rectangle rectangle, Color[] colorStops, float[] colorOffsets, GradientStyles style, double angle, double GradientOffset, double GradientOffset2)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Transparent, Color.Transparent, (float) angle))
      {
        linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
        ColorBlend colorBlend = new ColorBlend();
        Color[] colorArray = new Color[colorStops.Length];
        for (int index = 0; index < colorStops.Length; ++index)
          colorArray[index] = Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) colorStops[index].A * (double) this.opacity))), colorStops[index]);
        colorBlend.Colors = colorArray;
        colorBlend.Positions = colorOffsets;
        linearGradientBrush.InterpolationColors = colorBlend;
        g.FillRectangle((Brush) linearGradientBrush, rectangle);
        linearGradientBrush.Dispose();
      }
    }

    public virtual void DrawGradientRectBorder(Graphics g, Rectangle rectangle, Color[] colorStops, float[] colorOffsets, GradientStyles style, double angle, double GradientOffset, double GradientOffset2)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Transparent, Color.Transparent, (float) angle))
      {
        linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
        ColorBlend colorBlend = new ColorBlend();
        Color[] colorArray = new Color[colorStops.Length];
        for (int index = 0; index < colorStops.Length; ++index)
          colorArray[index] = Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) colorStops[index].A * (double) this.opacity))), colorStops[index]);
        colorBlend.Colors = colorArray;
        colorBlend.Positions = colorOffsets;
        linearGradientBrush.InterpolationColors = colorBlend;
        using (Pen pen = new Pen((Brush) linearGradientBrush))
          g.DrawRectangle(pen, rectangle);
        linearGradientBrush.Dispose();
      }
    }

    public Size GetRotatedSize(SizeF bounds, double angle)
    {
      double num1 = (double) bounds.Width;
      double num2 = (double) bounds.Height;
      double num3 = angle * Math.PI / 180.0;
      while (num3 < 0.0)
        num3 += 2.0 * Math.PI;
      double num4;
      double num5;
      double num6;
      double num7;
      if (num3 >= 0.0 && num3 < Math.PI / 2.0 || num3 >= Math.PI && num3 < 3.0 * Math.PI / 2.0)
      {
        num4 = Math.Abs(Math.Cos(num3)) * num1;
        num5 = Math.Abs(Math.Sin(num3)) * num1;
        num6 = Math.Abs(Math.Cos(num3)) * num2;
        num7 = Math.Abs(Math.Sin(num3)) * num2;
      }
      else
      {
        num4 = Math.Abs(Math.Sin(num3)) * num2;
        num5 = Math.Abs(Math.Cos(num3)) * num2;
        num6 = Math.Abs(Math.Sin(num3)) * num1;
        num7 = Math.Abs(Math.Cos(num3)) * num1;
      }
      return new Size((int) Math.Ceiling(num4 + num7), (int) Math.Ceiling(num6 + num5));
    }

    private void DrawRotatedText(Graphics g, Color textColor, float RotationAngle, string text, Font font, ContentAlignment textAlignment, bool multiline, bool ellipsis, Rectangle clientRectangle)
    {
      if (string.IsNullOrEmpty(text))
        return;
      StringFormat format = new StringFormat();
      if (!multiline)
        format.FormatFlags = StringFormatFlags.NoWrap;
      if (ellipsis)
        format.Trimming = StringTrimming.EllipsisCharacter;
      SizeF sizeF = g.MeasureString(text, font);
      float width = sizeF.Width;
      float height = sizeF.Height;
      Rectangle rectangle = clientRectangle;
      double num1 = (double) RotationAngle / 180.0;
      SizeF bounds = new SizeF(width, height);
      float num2 = (float) this.GetRotatedSize(bounds, (double) RotationAngle).Width;
      float num3 = (float) this.GetRotatedSize(bounds, (double) RotationAngle).Height;
      Bitmap bitmap1 = new Bitmap((int) width, (int) height);
      g.SmoothingMode = SmoothingMode.HighQuality;
      g.TextRenderingHint = TextRenderingHint.AntiAlias;
      Graphics graphics = Graphics.FromImage((Image) bitmap1);
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      using (SolidBrush solidBrush = new SolidBrush(textColor))
        graphics.DrawString(text, font, (Brush) solidBrush, (RectangleF) rectangle, format);
      Bitmap bitmap2 = this.RotateBitmap((Image) bitmap1, (double) RotationAngle);
      PointF point = PointF.Empty;
      float x1 = 0.0f;
      float x2 = (float) (rectangle.Width / 2) - num2 / 2f;
      float x3 = (float) rectangle.Width - num2;
      float y1 = 0.0f;
      float y2 = (float) (rectangle.Height / 2) - num3 / 2f;
      float y3 = (float) rectangle.Height - num3;
      switch (textAlignment)
      {
        case ContentAlignment.BottomCenter:
          point = new PointF(x2, y3);
          break;
        case ContentAlignment.BottomRight:
          point = new PointF(x3, y3);
          break;
        case ContentAlignment.MiddleRight:
          point = new PointF(x3, y2);
          break;
        case ContentAlignment.BottomLeft:
          point = new PointF(x1, y3);
          break;
        case ContentAlignment.TopLeft:
          point = PointF.Empty;
          break;
        case ContentAlignment.TopCenter:
          point = new PointF(x2, y1);
          break;
        case ContentAlignment.TopRight:
          point = new PointF(x3, y1);
          break;
        case ContentAlignment.MiddleLeft:
          point = new PointF(point.X, y2);
          break;
        case ContentAlignment.MiddleCenter:
          point = new PointF(x2, y2);
          break;
      }
      g.DrawImage((Image) bitmap2, point);
    }

    /// <summary>Rotates the image.</summary>
    /// <param name="image">The image.</param>
    /// <param name="angle">The angle.</param>
    /// <returns></returns>
    public Bitmap RotateBitmap(Image image, double angle)
    {
      if (image == null)
        throw new ArgumentNullException("image");
      double num1 = (double) image.Width;
      double num2 = (double) image.Height;
      double num3 = angle * Math.PI / 180.0;
      while (num3 < 0.0)
        num3 += 2.0 * Math.PI;
      double num4;
      double num5;
      double num6;
      double num7;
      if (num3 >= 0.0 && num3 < Math.PI / 2.0 || num3 >= Math.PI && num3 < 3.0 * Math.PI / 2.0)
      {
        num4 = Math.Abs(Math.Cos(num3)) * num1;
        num5 = Math.Abs(Math.Sin(num3)) * num1;
        num6 = Math.Abs(Math.Cos(num3)) * num2;
        num7 = Math.Abs(Math.Sin(num3)) * num2;
      }
      else
      {
        num4 = Math.Abs(Math.Sin(num3)) * num2;
        num5 = Math.Abs(Math.Cos(num3)) * num2;
        num6 = Math.Abs(Math.Sin(num3)) * num1;
        num7 = Math.Abs(Math.Cos(num3)) * num1;
      }
      double num8 = (double) ((int) num4 + (int) num7);
      double num9 = (double) ((int) num6 + (int) num5);
      int num10 = (int) num8;
      int num11 = (int) num9;
      Bitmap bitmap = new Bitmap(num10, num11);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Point[] destPoints;
        if (num3 >= 0.0 && num3 < Math.PI / 2.0)
          destPoints = new Point[3]
          {
            new Point((int) num7, 0),
            new Point(num10, (int) num5),
            new Point(0, (int) num6)
          };
        else if (num3 >= Math.PI / 2.0 && num3 < Math.PI)
          destPoints = new Point[3]
          {
            new Point(num10, (int) num5),
            new Point((int) num4, num11),
            new Point((int) num7, 0)
          };
        else if (num3 >= Math.PI && num3 < 3.0 * Math.PI / 2.0)
          destPoints = new Point[3]
          {
            new Point((int) num4, num11),
            new Point(0, (int) num6),
            new Point(num10, (int) num5)
          };
        else
          destPoints = new Point[3]
          {
            new Point(0, (int) num6),
            new Point((int) num7, 0),
            new Point((int) num4, num11)
          };
        graphics.DrawImage(image, destPoints);
      }
      return bitmap;
    }

    public virtual void DrawEllipse(Graphics g, Rectangle rectangle, Color[] colorStops, float[] colorOffsets, GradientStyles style, double angle, double GradientOffset, double GradientOffset2)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Transparent, Color.Transparent, (float) angle))
      {
        ColorBlend colorBlend = new ColorBlend();
        Color[] colorArray = new Color[colorStops.Length];
        for (int index = 0; index < colorStops.Length; ++index)
          colorArray[index] = Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) colorStops[index].A * (double) this.opacity))), colorStops[index]);
        colorBlend.Colors = colorArray;
        colorBlend.Positions = colorOffsets;
        linearGradientBrush.InterpolationColors = colorBlend;
        g.FillEllipse((Brush) linearGradientBrush, rectangle);
        linearGradientBrush.Dispose();
      }
    }

    public virtual void DrawGradientPathFigure(Graphics g, GraphicsPath path, Rectangle rectangle, Color[] colorStops, float[] colorOffsets, GradientStyles style, double angle, double GradientOffset, double GradientOffset2)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Transparent, Color.Transparent, (float) angle))
      {
        ColorBlend colorBlend = new ColorBlend();
        Color[] colorArray = new Color[colorStops.Length];
        for (int index = 0; index < colorStops.Length; ++index)
          colorArray[index] = Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) colorStops[index].A * (double) this.opacity))), colorStops[index]);
        colorBlend.Colors = colorArray;
        if (colorOffsets.Length > colorArray.Length)
        {
          if (colorArray.Length == 2)
            colorOffsets = new float[2]{ 0.0f, 1f };
          else if (colorArray.Length == 3)
            colorOffsets = new float[3]{ 0.0f, 0.5f, 1f };
        }
        colorBlend.Positions = colorOffsets;
        linearGradientBrush.InterpolationColors = colorBlend;
        g.FillPath((Brush) linearGradientBrush, path);
        linearGradientBrush.Dispose();
      }
    }

    public virtual void DrawGradientPathFigure(Graphics g, int radius, Rectangle rectangle, Color[] colorStops, float[] colorOffsets, GradientStyles style, double angle, double GradientOffset, double GradientOffset2)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Transparent, Color.Transparent, (float) angle))
      {
        using (GraphicsPath roundedPathRect = this.GetRoundedPathRect(rectangle, radius))
        {
          ColorBlend colorBlend = new ColorBlend();
          Color[] colorArray = new Color[colorStops.Length];
          for (int index = 0; index < colorStops.Length; ++index)
            colorArray[index] = Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) colorStops[index].A * (double) this.opacity))), colorStops[index]);
          colorBlend.Colors = colorArray;
          colorBlend.Positions = colorOffsets;
          linearGradientBrush.InterpolationColors = colorBlend;
          g.FillPath((Brush) linearGradientBrush, roundedPathRect);
          linearGradientBrush.Dispose();
        }
      }
    }

    public virtual void DrawGradientPartiallyRoundedRectFigure(Graphics g, int radius, Rectangle rectangle, byte roundedCornerBitmask, Color[] colorStops, float[] colorOffsets, GradientStyles style, double angle, double GradientOffset, double GradientOffset2)
    {
      if (radius == 0)
        this.DrawGradientRectFigure(g, rectangle, colorStops, colorOffsets, style, angle, GradientOffset, GradientOffset2);
      else if ((int) roundedCornerBitmask == 15)
      {
        this.DrawGradientPathFigure(g, radius, rectangle, colorStops, colorOffsets, style, angle, GradientOffset, GradientOffset2);
      }
      else
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Transparent, Color.Transparent, (float) angle))
        {
          using (GraphicsPath partiallyRoundedPath = this.CreatePartiallyRoundedPath(rectangle, radius, roundedCornerBitmask))
          {
            ColorBlend colorBlend = new ColorBlend();
            Color[] colorArray = new Color[colorStops.Length];
            for (int index = 0; index < colorStops.Length; ++index)
              colorArray[index] = Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) colorStops[index].A * (double) this.opacity))), colorStops[index]);
            colorBlend.Colors = colorArray;
            colorBlend.Positions = colorOffsets;
            linearGradientBrush.InterpolationColors = colorBlend;
            g.FillPath((Brush) linearGradientBrush, partiallyRoundedPath);
          }
        }
      }
    }

    public StringFormat GetStringFormat(ContentAlignment contentAlignment)
    {
      StringFormat stringFormat = new StringFormat();
      switch (contentAlignment)
      {
        case ContentAlignment.BottomCenter:
          stringFormat.LineAlignment = StringAlignment.Far;
          stringFormat.Alignment = StringAlignment.Center;
          break;
        case ContentAlignment.BottomRight:
          stringFormat.LineAlignment = StringAlignment.Far;
          stringFormat.Alignment = StringAlignment.Far;
          break;
        case ContentAlignment.MiddleRight:
          stringFormat.LineAlignment = StringAlignment.Center;
          stringFormat.Alignment = StringAlignment.Far;
          break;
        case ContentAlignment.BottomLeft:
          stringFormat.LineAlignment = StringAlignment.Far;
          stringFormat.Alignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopLeft:
          stringFormat.LineAlignment = StringAlignment.Near;
          stringFormat.Alignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopCenter:
          stringFormat.LineAlignment = StringAlignment.Near;
          stringFormat.Alignment = StringAlignment.Center;
          break;
        case ContentAlignment.TopRight:
          stringFormat.LineAlignment = StringAlignment.Near;
          stringFormat.Alignment = StringAlignment.Far;
          break;
        case ContentAlignment.MiddleLeft:
          stringFormat.LineAlignment = StringAlignment.Center;
          stringFormat.Alignment = StringAlignment.Near;
          break;
        case ContentAlignment.MiddleCenter:
          stringFormat.LineAlignment = StringAlignment.Center;
          stringFormat.Alignment = StringAlignment.Center;
          break;
      }
      return stringFormat;
    }

    public virtual void DrawBitmap(Graphics g, Image image, int x, int y)
    {
      g.DrawImage(image, new Rectangle(x, y, image.Size.Width, image.Size.Height));
    }

    public virtual void DrawBitmap(Graphics g, Image image, int x, int y, int width, int height)
    {
      this.DrawBitmap(g, image, x, y, width, height, 1.0);
    }

    private float[][] GetImageColorArray(int length, double opacity)
    {
      float[][] numArray1 = new float[length][];
      for (int index = 0; index < length; ++index)
      {
        float[] numArray2 = new float[length];
        numArray2[index] = 1f;
        numArray1[index] = numArray2;
      }
      numArray1[3][3] = (float) opacity;
      return numArray1;
    }

    public virtual void DrawBitmap(Graphics g, Image image, int x, int y, int width, int height, double opacity)
    {
      if (opacity == 1.0)
        g.DrawImage(image, new Rectangle(x, y, width, height));
      else
        this.DrawTransparentImage(g, image, x, y, width, height, opacity);
    }

    private void DrawTransparentImage(Graphics g, Image image, int x, int y, int width, int height, double opacity)
    {
      ColorMatrix newColorMatrix = new ColorMatrix(this.GetImageColorArray(5, opacity));
      using (ImageAttributes imageAttr = new ImageAttributes())
      {
        imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        Rectangle destRect = new Rectangle(x, y, width, height);
        g.DrawImage(image, destRect, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttr);
      }
    }
  }
}
