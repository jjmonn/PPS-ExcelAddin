// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RibbonPaintHelper
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class RibbonPaintHelper
  {
    private Size arrowSize = new Size(5, 3);
    private Size moreSize = new Size(7, 7);

    /// <summary>Gets the size of the arrow.</summary>
    /// <returns></returns>
    public Size GetArrowSize()
    {
      return this.arrowSize;
    }

    /// <summary>Draws an arrow on the specified bounds</summary>
    /// <param name="g"></param>
    /// <param name="bounds"></param>
    /// <param name="c"></param>
    public void DrawRibbonArrow(Graphics g, Rectangle b, Color c, ArrowDirection d)
    {
      GraphicsPath path = new GraphicsPath();
      Rectangle rectangle = b;
      if (b.Width % 2 != 0 && d == ArrowDirection.Up)
        rectangle = new Rectangle(new Point(b.Left - 1, b.Top - 1), new Size(b.Width + 1, b.Height + 1));
      if (d == ArrowDirection.Up)
      {
        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left + rectangle.Width / 2, rectangle.Top);
      }
      else if (d == ArrowDirection.Down)
      {
        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Left + rectangle.Width / 2, rectangle.Bottom);
      }
      else if (d == ArrowDirection.Right)
      {
        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top + rectangle.Height / 2);
        path.AddLine(rectangle.Right, rectangle.Top + rectangle.Height / 2, rectangle.Left, rectangle.Bottom);
      }
      else
      {
        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Left, rectangle.Top + rectangle.Height / 2);
        path.AddLine(rectangle.Left, rectangle.Top + rectangle.Height / 2, rectangle.Right, rectangle.Bottom);
      }
      path.CloseFigure();
      using (SolidBrush solidBrush = new SolidBrush(c))
      {
        SmoothingMode smoothingMode = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.None;
        g.FillPath((Brush) solidBrush, path);
        g.SmoothingMode = smoothingMode;
      }
      path.Dispose();
    }

    /// <summary>Draws the ribbon drop down button arrow.</summary>
    /// <param name="g">The g.</param>
    /// <param name="buttonBounds">The button bounds.</param>
    /// <param name="arrowColor">Color of the arrow.</param>
    public void DrawRibbonDropDownButtonArrow(Graphics g, Rectangle buttonBounds, Color arrowColor)
    {
      Rectangle middleCenterRectangle = this.GetMiddleCenterRectangle(buttonBounds, new Rectangle(Point.Empty, this.arrowSize));
      middleCenterRectangle.Offset(0, 3);
      Color color = ControlPaint.LightLight(arrowColor);
      ControlPaint.Dark(arrowColor);
      Rectangle rect = new Rectangle(new Point(middleCenterRectangle.Left, middleCenterRectangle.Top - 4), new Size(this.arrowSize.Width, 2));
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, arrowColor, color, LinearGradientMode.Vertical))
      {
        SmoothingMode smoothingMode = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.None;
        g.FillRectangle((Brush) linearGradientBrush, rect);
        g.SmoothingMode = smoothingMode;
      }
      this.DrawRibbonArrow(g, middleCenterRectangle, color, ArrowDirection.Down);
      middleCenterRectangle.Offset(0, -1);
      this.DrawRibbonArrow(g, middleCenterRectangle, arrowColor, ArrowDirection.Down);
    }

    /// <summary>Draws the footer button.</summary>
    /// <param name="g">The g.</param>
    /// <param name="color">The color.</param>
    /// <param name="b">The b.</param>
    public void DrawFooterButton(Graphics g, Color color, Rectangle b)
    {
      Color color1 = color;
      Color color2 = ControlPaint.Light(color);
      Rectangle middleCenterRectangle = this.GetMiddleCenterRectangle(b, new Rectangle(Point.Empty, this.moreSize));
      Rectangle rectangle = middleCenterRectangle;
      rectangle.Offset(1, 1);
      this.DrawFooterButton(g, rectangle.Location, color2);
      this.DrawFooterButton(g, middleCenterRectangle.Location, color1);
    }

    /// <summary>Draws the footer button.</summary>
    /// <param name="gr">The gr.</param>
    /// <param name="p">The p.</param>
    /// <param name="color">The color.</param>
    public void DrawFooterButton(Graphics gr, Point p, Color color)
    {
      Point pt1_1 = p;
      Point pt2_1 = new Point(p.X + this.moreSize.Width - 1, p.Y);
      Point pt2_2 = new Point(p.X, p.Y + this.moreSize.Height - 1);
      Point pt2_3 = new Point(p.X + this.moreSize.Width, p.Y + this.moreSize.Height);
      Point point = new Point(pt2_3.X, pt2_3.Y - 3);
      Point pt1_2 = new Point(pt2_3.X - 3, pt2_3.Y);
      Point pt1_3 = new Point(pt2_3.X - 3, pt2_3.Y - 3);
      SmoothingMode smoothingMode = gr.SmoothingMode;
      gr.SmoothingMode = SmoothingMode.None;
      using (Pen pen = new Pen(color))
      {
        gr.DrawLine(pen, pt1_1, pt2_1);
        gr.DrawLine(pen, pt1_1, pt2_2);
        gr.DrawLine(pen, pt1_2, pt2_3);
        gr.DrawLine(pen, point, pt2_3);
        gr.DrawLine(pen, pt1_2, point);
        gr.DrawLine(pen, pt1_3, pt2_3);
      }
      gr.SmoothingMode = smoothingMode;
    }

    /// <summary>Gets the color from hex string.</summary>
    /// <param name="hex">The hex.</param>
    /// <returns></returns>
    public Color GetColorFromHexString(string hex)
    {
      if (hex.StartsWith("#"))
        hex = hex.Substring(1);
      if (hex.Length != 6)
        throw new Exception("Color not valid");
      return Color.FromArgb(int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber), int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber));
    }

    /// <summary>Draws the ribbon arrow shaded.</summary>
    /// <param name="g">The g.</param>
    /// <param name="b">The b.</param>
    /// <param name="arrowColor">Color of the arrow.</param>
    /// <param name="direction">The direction.</param>
    /// <param name="enabled">if set to <c>true</c> [enabled].</param>
    public void DrawArrowWithShadow(Graphics g, Rectangle b, Color arrowColor, ArrowDirection direction, bool enabled)
    {
      Size size = this.arrowSize;
      if (direction == ArrowDirection.Left || direction == ArrowDirection.Right)
        size = new Size(this.arrowSize.Height, this.arrowSize.Width);
      Rectangle middleCenterRectangle = this.GetMiddleCenterRectangle(b, new Rectangle(Point.Empty, size));
      Rectangle b1 = middleCenterRectangle;
      b1.Offset(0, 1);
      Color c1 = ControlPaint.LightLight(arrowColor);
      Color c2 = arrowColor;
      if (!enabled)
      {
        c1 = Color.Transparent;
        c2 = this.GetColorFromHexString("#B7B7B7");
      }
      this.DrawRibbonArrow(g, b1, c1, direction);
      this.DrawRibbonArrow(g, middleCenterRectangle, c2, direction);
    }

    public Rectangle GetMiddleCenterRectangle(Rectangle parent, Rectangle child)
    {
      return new Rectangle(parent.Left + (parent.Width - child.Width) / 2, parent.Top + (parent.Height - child.Height) / 2, child.Width, child.Height);
    }
  }
}
