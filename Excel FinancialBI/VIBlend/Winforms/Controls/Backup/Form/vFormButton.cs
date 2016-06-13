// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vFormButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  public class vFormButton : vButton
  {
    private bool ribbonStyle;
    private vFormButtonType buttonType;

    /// <summary>
    /// Gets or sets a value indicating whether the button is drawn in ribbon style
    /// </summary>
    public bool RibbonStyle
    {
      get
      {
        return this.ribbonStyle;
      }
      set
      {
        this.ribbonStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the type of the button.</summary>
    /// <value>The type of the button.</value>
    public vFormButtonType ButtonType
    {
      get
      {
        return this.buttonType;
      }
      set
      {
        this.buttonType = value;
        this.Invalidate();
      }
    }

    public vFormButton()
    {
      this.ShowFocusRectangle = false;
    }

    private void DrawOffice2010Button(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte mask)
    {
      Rectangle rect = new Rectangle(bounds.X + bounds.Width / 4, bounds.Height - bounds.Height / 4 - 4, bounds.Width - bounds.Width / 4 - bounds.Width / 4, 4);
      Color color1 = this.backFill.Theme.QueryColorSetter("RibbonFormButtonBorder");
      Color color2 = this.backFill.Theme.QueryColorSetter("RibbonFormButtonHighlightBorder");
      FillStyleGradientEx fillStyleGradientEx1 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonFormButtonNormal");
      FillStyleGradientEx fillStyleGradientEx2 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonFormButtonHighlight");
      FillStyleGradientEx fillStyleGradientEx3 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonFormButtonPressed");
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      if (controlState == ControlState.Hover || controlState == ControlState.Pressed)
      {
        ControlTheme copy = this.theme.CreateCopy();
        BackgroundElement backgroundElement = new BackgroundElement(bounds, (IScrollableControlBase) this);
        backgroundElement.LoadTheme(copy);
        this.backFill.IsAnimated = true;
        if (this.buttonType == vFormButtonType.CloseButton)
        {
          fillStyleGradientEx2 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonFormCloseButtonHighlight");
          fillStyleGradientEx3 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonFormCloseButtonPressed");
          color1 = this.backFill.Theme.QueryColorSetter("RibbonFormCloseButtonBorder");
          color2 = this.backFill.Theme.QueryColorSetter("RibbonFormCloseButtonBorder");
        }
        copy.StyleHighlight.FillStyle = (FillStyle) fillStyleGradientEx2;
        copy.StylePressed.FillStyle = (FillStyle) fillStyleGradientEx3;
        copy.StylePressed.BorderColor = color2;
        copy.StyleHighlight.BorderColor = color2;
        Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
        backgroundElement.Bounds = rectangle;
        backgroundElement.DrawElementFill(graphics, controlState);
        backgroundElement.Bounds = rectangle;
        backgroundElement.DrawElementBorder(graphics, controlState);
      }
      if (this.buttonType == vFormButtonType.MinimizeButton)
      {
        using (Pen pen = new Pen(fillStyleGradientEx1.Color1))
        {
          for (int index = 0; index < 5; ++index)
          {
            Point pt1 = new Point(bounds.X + bounds.Width / 2 - 5, bounds.Height - bounds.Height / 4 - index);
            Point pt2 = new Point(bounds.X + bounds.Width / 2 + 5, bounds.Height - bounds.Height / 4 - index);
            graphics.DrawLine(pen, pt1, pt2);
          }
          rect = new Rectangle(bounds.X + bounds.Width / 2 - 5, rect.Y, 10, rect.Height);
          pen.Color = color1;
          graphics.DrawRectangle(pen, rect);
        }
      }
      else if (this.buttonType == vFormButtonType.MaximizeButton)
      {
        rect = new Rectangle(bounds.X + bounds.Width / 2 - 5, bounds.Height / 2 - 3, 10, 7);
        using (Pen pen = new Pen(this.ForeColor))
        {
          if (this.FindForm().WindowState == FormWindowState.Maximized)
          {
            rect = new Rectangle(bounds.X + bounds.Width / 2 - 2, bounds.Height / 2 - 5, 10, 8);
            pen.Color = color1;
            using (SolidBrush solidBrush = new SolidBrush(fillStyleGradientEx1.Color1))
            {
              graphics.FillRectangle((Brush) solidBrush, rect);
              graphics.DrawRectangle(pen, rect);
              rect.Inflate(-3, -3);
              FillStyleGradientEx fillStyleGradientEx4 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonTitle");
              solidBrush.Color = fillStyleGradientEx4.Color1;
              graphics.FillRectangle((Brush) solidBrush, rect);
              graphics.DrawRectangle(pen, rect);
            }
          }
          rect = new Rectangle(bounds.X + bounds.Width / 2 - 5, bounds.Height / 2 - 2, 10, 8);
          pen.Color = color1;
          using (SolidBrush solidBrush = new SolidBrush(fillStyleGradientEx1.Color1))
          {
            graphics.FillRectangle((Brush) solidBrush, rect);
            graphics.DrawRectangle(pen, rect);
            rect.Inflate(-3, -3);
            FillStyleGradientEx fillStyleGradientEx4 = (FillStyleGradientEx) this.backFill.Theme.QueryFillStyleSetter("RibbonTitle");
            solidBrush.Color = fillStyleGradientEx4.Color1;
            graphics.FillRectangle((Brush) solidBrush, rect);
            graphics.DrawRectangle(pen, rect);
          }
        }
      }
      else if (this.buttonType == vFormButtonType.CloseButton)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, fillStyleGradientEx1.Color1, fillStyleGradientEx1.Color1, LinearGradientMode.Vertical))
        {
          Rectangle rectangle = bounds;
          if (controlState == ControlState.Pressed)
          {
            ++rectangle.X;
            ++rectangle.Y;
          }
          using (Pen pen = new Pen((Brush) linearGradientBrush))
          {
            int num1 = rectangle.X + rectangle.Width / 2 - 7;
            int num2 = num1 + 7;
            int y1 = rectangle.Y - 2 + rectangle.Height / 2;
            int y2 = y1 + 6;
            for (int index = 0; index < 5; ++index)
            {
              if (index == 0)
              {
                pen.Color = color1;
                graphics.DrawLine(pen, num1 + 1, y1, num2, y2);
                graphics.DrawLine(pen, num2, y1, num1 + 1, y2);
                graphics.DrawLine(pen, num1 + 5, y1, num2 + 4, y2);
                graphics.DrawLine(pen, num2 + 4, y1, num1 + 5, y2);
                graphics.DrawLine(pen, num1 + 1, y1 - 1, num1 + 4, y1 - 1);
                graphics.DrawLine(pen, num2, y1 - 1, num2 + 4, y1 - 1);
                graphics.DrawLine(pen, num1 + 1, y2 + 1, num1 + 4, y2 + 1);
                graphics.DrawLine(pen, num2, y2 + 1, num2 + 4, y2 + 1);
              }
              else if (index == 4)
              {
                pen.Color = color1;
              }
              else
              {
                pen.Color = fillStyleGradientEx1.Color1;
                graphics.DrawLine(pen, num1 + 1 + index, y1, num2 + index, y2);
                graphics.DrawLine(pen, num2 + index, y1, num1 + 1 + index, y2);
              }
            }
          }
        }
      }
      graphics.SmoothingMode = smoothingMode;
    }

    protected override void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte mask)
    {
      if (this.VIBlendTheme == VIBLEND_THEME.STEEL || this.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLACK || (this.VIBlendTheme == VIBLEND_THEME.OFFICE2010SILVER || this.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLUE))
      {
        this.DrawOffice2010Button(graphics, bounds, controlState, isFocused, mask);
      }
      else
      {
        if (!this.ribbonStyle)
          base.DrawControl(graphics, bounds, controlState, isFocused, mask);
        else if (this.controlState != ControlState.Normal)
          base.DrawControl(graphics, bounds, ControlState.Normal, isFocused, mask);
        Form form = this.FindForm();
        if (form != null && form.WindowState == FormWindowState.Maximized && this.ribbonStyle)
          graphics.FillRectangle(Brushes.Transparent, this.ClientRectangle);
        SmoothingMode smoothingMode = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Color color = this.backFill.BorderColor;
        Color glowColor = this.Theme.StyleNormal.FillStyle.Colors[0];
        Color baseColor = this.backFill.Theme.QueryColorSetter("RibbonFormButtonBorder");
        if (baseColor != Color.Empty)
          color = ControlPaint.Light(baseColor);
        if (!this.ribbonStyle)
        {
          if (controlState == ControlState.Pressed)
          {
            color = this.backFill.PressedBorderColor;
            glowColor = this.Theme.StylePressed.FillStyle.Colors[0];
          }
          else if (controlState == ControlState.Hover)
          {
            color = this.backFill.HighlightBorderColor;
            glowColor = this.Theme.StyleHighlight.FillStyle.Colors[0];
          }
        }
        if (this.buttonType == vFormButtonType.MinimizeButton)
        {
          Rectangle rect = new Rectangle(bounds.X + bounds.Width / 4, bounds.Height - bounds.Height / 4 - 4, bounds.Width - bounds.Width / 4 - bounds.Width / 4, 4);
          if (!this.ribbonStyle)
          {
            using (Pen pen = new Pen(this.ForeColor))
            {
              for (int index = 0; index < 5; ++index)
              {
                Point pt1 = new Point(bounds.X + bounds.Width / 4, bounds.Height - bounds.Height / 4 - index);
                Point pt2 = new Point(bounds.X + bounds.Width - bounds.Width / 4, bounds.Height - bounds.Height / 4 - index);
                graphics.DrawLine(pen, pt1, pt2);
              }
              pen.Color = color;
              graphics.DrawRectangle(pen, rect);
            }
          }
          else
          {
            rect = new Rectangle(bounds.X + bounds.Width / 4 - 1, bounds.Height / 4, 3 * bounds.Width / 4 + 1, 3 * bounds.Height / 4);
            if (controlState == ControlState.Pressed)
            {
              ++rect.X;
              ++rect.Y;
            }
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, ControlPaint.LightLight(color), ControlPaint.Light(color), LinearGradientMode.Vertical))
            {
              using (Pen pen1 = new Pen((Brush) linearGradientBrush))
              {
                graphics.DrawLine(pen1, rect.X + 1, rect.Height - 1, rect.Width - 1, rect.Height - 1);
                using (Pen pen2 = new Pen(ControlPaint.Dark(this.backFill.BorderColor)))
                  graphics.DrawLine(pen2, rect.X + 1, rect.Height, rect.Width - 1, rect.Height);
                pen1.Color = ControlPaint.LightLight(this.backFill.BorderColor);
                graphics.DrawLine(pen1, rect.X + 1, rect.Height - 2, rect.Width - 1, rect.Height - 2);
              }
            }
          }
        }
        else if (this.buttonType == vFormButtonType.MaximizeButton)
        {
          Rectangle rect1 = new Rectangle(bounds.X + bounds.Width / 3, bounds.Height / 8, bounds.Width / 2 + 1, bounds.Height / 2);
          if (!this.ribbonStyle)
          {
            using (Pen pen = new Pen(this.ForeColor))
            {
              if (this.FindForm().WindowState == FormWindowState.Maximized)
              {
                pen.Color = color;
                using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
                {
                  graphics.FillRectangle((Brush) solidBrush, rect1);
                  graphics.DrawRectangle(pen, rect1);
                  rect1.Inflate(-rect1.Width / 3, -rect1.Height / 3);
                  solidBrush.Color = glowColor;
                  graphics.FillRectangle((Brush) solidBrush, rect1);
                  graphics.DrawRectangle(pen, rect1);
                }
              }
              Rectangle rect2 = new Rectangle(bounds.X + bounds.Width / 4 - 1, bounds.Height / 4, bounds.Width / 2 + 1, bounds.Height / 2);
              pen.Color = color;
              using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
              {
                graphics.FillRectangle((Brush) solidBrush, rect2);
                graphics.DrawRectangle(pen, rect2);
                rect2.Inflate(-rect2.Width / 3, -rect2.Height / 3);
                solidBrush.Color = glowColor;
                graphics.FillRectangle((Brush) solidBrush, rect2);
                graphics.DrawRectangle(pen, rect2);
              }
            }
          }
          else
          {
            using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, ControlPaint.LightLight(color), ControlPaint.Light(color), LinearGradientMode.Vertical))
            {
              if (this.FindForm().WindowState == FormWindowState.Maximized)
              {
                using (Pen pen = new Pen((Brush) linearGradientBrush))
                {
                  Rectangle rect2 = new Rectangle(bounds.X + bounds.Width / 3, 3, bounds.Width / 2 + 1, bounds.Height / 2 - 2);
                  if (controlState == ControlState.Pressed)
                  {
                    ++rect2.X;
                    ++rect2.Y;
                  }
                  graphics.FillRectangle((Brush) linearGradientBrush, new Rectangle(rect2.X, rect2.Y, rect2.Width, 3));
                  pen.Brush = (Brush) new SolidBrush(color);
                  graphics.DrawRectangle(pen, rect2);
                }
              }
              using (Pen pen = new Pen((Brush) linearGradientBrush))
              {
                Rectangle rect2 = new Rectangle(bounds.X + bounds.Width / 4, bounds.Height / 4 + 2, bounds.Width / 2 + 1, bounds.Height / 2 - 2);
                if (controlState == ControlState.Pressed)
                {
                  ++rect2.X;
                  ++rect2.Y;
                }
                graphics.FillRectangle((Brush) linearGradientBrush, new Rectangle(rect2.X, rect2.Y, rect2.Width, 3));
                pen.Brush = (Brush) new SolidBrush(color);
                graphics.DrawRectangle(pen, rect2);
              }
            }
          }
        }
        else if (this.buttonType == vFormButtonType.CloseButton)
        {
          using (Pen pen1 = new Pen(this.ForeColor))
          {
            Rectangle rect = new Rectangle(bounds.X + bounds.Width / 4 - 1, bounds.Height / 4, 3 * bounds.Width / 4 + 1, 3 * bounds.Height / 4);
            if (!this.ribbonStyle)
            {
              for (int index = 0; index < 3; ++index)
              {
                graphics.DrawLine(pen1, rect.X + 1 + index, rect.Y + 1, rect.Width - 3 + index, rect.Height - 1);
                graphics.DrawLine(pen1, rect.Width - 3 + index, rect.Y + 1, rect.X + 1 + index, rect.Height - 1);
              }
            }
            if (this.ribbonStyle)
            {
              using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, ControlPaint.Light(color), color, LinearGradientMode.Vertical))
              {
                if (controlState == ControlState.Pressed)
                {
                  ++rect.X;
                  ++rect.Y;
                }
                using (Pen pen2 = new Pen((Brush) linearGradientBrush))
                {
                  for (int index = 0; index < 3; ++index)
                  {
                    graphics.DrawLine(pen2, rect.X + 1 + index, rect.Y + 1, rect.Width - 3 + index, rect.Height - 1);
                    graphics.DrawLine(pen2, rect.Width - 3 + index, rect.Y + 1, rect.X + 1 + index, rect.Height - 1);
                  }
                }
                using (Pen pen2 = new Pen(ControlPaint.Light(this.backFill.BorderColor)))
                {
                  graphics.DrawLine(pen2, rect.X + 1, rect.Height, rect.X + 4, rect.Height);
                  graphics.DrawLine(pen2, rect.Width - 3, rect.Height, rect.Width, rect.Height);
                }
              }
            }
          }
        }
        graphics.SmoothingMode = smoothingMode;
        if (this.ribbonStyle)
          return;
        PaintHelper paintHelper = new PaintHelper();
        GraphicsPath path = new GraphicsPath();
        Rectangle rectangle = new Rectangle(bounds.X, bounds.Y + 7, bounds.Width, bounds.Height);
        path.AddEllipse(rectangle);
        graphics.Clip = new Region(path);
        paintHelper.DrawGlow(graphics, rectangle, glowColor);
        graphics.ResetClip();
      }
    }
  }
}
