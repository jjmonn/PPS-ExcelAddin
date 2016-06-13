// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ImageAndTextHelper
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class ImageAndTextHelper
  {
    private static readonly ContentAlignment isRight = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
    private static readonly ContentAlignment isBottom = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
    private static readonly ContentAlignment isCenter = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
    private static readonly ContentAlignment isMiddle = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;
    private static readonly ContentAlignment isTop = ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight;
    private static readonly ContentAlignment isLeft = ContentAlignment.TopLeft | ContentAlignment.MiddleLeft | ContentAlignment.BottomLeft;

    public static StringFormat GetStringFormat(Control ctl, ContentAlignment textAlign, bool showEllipsis, bool useMnemonic)
    {
      StringFormat format = ImageAndTextHelper.InitializeStringFormatAlignment(textAlign);
      ImageAndTextHelper.InitializeStringFormatFlags(ctl, showEllipsis, useMnemonic, format);
      return format;
    }

    private static void InitializeStringFormatFlags(Control ctl, bool showEllipsis, bool useMnemonic, StringFormat format)
    {
      if (ctl.RightToLeft == RightToLeft.Yes)
        format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      if (showEllipsis)
      {
        format.Trimming = StringTrimming.EllipsisCharacter;
        format.FormatFlags |= StringFormatFlags.LineLimit;
      }
      format.HotkeyPrefix = useMnemonic ? HotkeyPrefix.Show : HotkeyPrefix.None;
      if (!ctl.AutoSize)
        return;
      format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
    }

    public static void DrawDisabledText(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
    {
      if (graphics == null)
        return;
      layoutRectangle.Offset(1f, 1f);
      using (SolidBrush solidBrush = new SolidBrush(ControlPaint.LightLight(color)))
      {
        graphics.DrawString(s, font, (Brush) solidBrush, layoutRectangle, format);
        layoutRectangle.Offset(-1f, -1f);
        color = ControlPaint.Dark(color);
        solidBrush.Color = color;
        graphics.DrawString(s, font, (Brush) solidBrush, layoutRectangle, format);
      }
    }

    public static void GetTextImageRectangle(Rectangle bounds, TextImageRelation relation, ContentAlignment imageAlignment, ContentAlignment textAlignment, out Rectangle imageRectangle, out Rectangle textRectangle)
    {
      textRectangle = Rectangle.Empty;
      imageRectangle = Rectangle.Empty;
    }

    public static Size GetTextSize(Graphics graphics, string text, Font font, Size proposedSize)
    {
      Size size = Size.Empty;
      using (StringFormat stringFormat = new StringFormat())
        return Size.Ceiling(graphics.MeasureString(text, font, new SizeF((float) proposedSize.Width, (float) proposedSize.Height), stringFormat));
    }

    public static void InitializeTextAndImageRectangles(Graphics g, Rectangle bounds, Image image, string Text, Font TextFont, TextImageRelation relation, ContentAlignment imageAlignment, ContentAlignment textAlignment, Padding textMargin, Padding imageMargin, out Rectangle imageRectangle, out Rectangle textRectangle)
    {
      imageRectangle = Rectangle.Empty;
      textRectangle = Rectangle.Empty;
      switch (relation)
      {
        case TextImageRelation.Overlay:
          imageRectangle = bounds;
          textRectangle = bounds;
          break;
        case TextImageRelation.ImageAboveText:
          imageRectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height / 2);
          textRectangle = new Rectangle(bounds.X, bounds.Y + bounds.Height / 2, bounds.Width, bounds.Height / 2);
          break;
        case TextImageRelation.TextAboveImage:
          textRectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height / 2);
          imageRectangle = new Rectangle(bounds.X, bounds.Y + bounds.Height / 2, bounds.Width, bounds.Height / 2);
          break;
        case TextImageRelation.ImageBeforeText:
          imageRectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width / 2, bounds.Height);
          textRectangle = new Rectangle(bounds.X + bounds.Width / 2, bounds.Y, bounds.Width / 2, bounds.Height);
          break;
        case TextImageRelation.TextBeforeImage:
          textRectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width / 2, bounds.Height);
          imageRectangle = new Rectangle(bounds.X + bounds.Width / 2, bounds.Y, bounds.Width / 2, bounds.Height);
          break;
      }
      Rectangle alignmentRectangle1 = textRectangle;
      Size sz = Size.Ceiling(g.MeasureString(Text, TextFont));
      ContentAlignment alignment1 = textAlignment;
      ImageAndTextHelper.GetAlignmentRectangle(ref alignmentRectangle1, ref sz, alignment1, textMargin);
      textRectangle = alignmentRectangle1;
      if (textRectangle.X < bounds.X)
        textRectangle.X = bounds.X;
      if (textRectangle.Right > bounds.Right)
      {
        int num = textRectangle.Right - bounds.Right;
        textRectangle.Width -= num;
      }
      if (textRectangle.Bottom > bounds.Bottom)
      {
        int num = textRectangle.Bottom - bounds.Bottom;
        textRectangle.Height -= num;
      }
      if (textRectangle.Y < bounds.Y)
        textRectangle.Y = bounds.Y;
      Rectangle alignmentRectangle2 = imageRectangle;
      Size size = image.Size;
      ContentAlignment alignment2 = imageAlignment;
      ImageAndTextHelper.GetAlignmentRectangle(ref alignmentRectangle2, ref size, alignment2, imageMargin);
      imageRectangle = alignmentRectangle2;
      if (imageRectangle.X < bounds.X)
        imageRectangle.X = bounds.X;
      if (imageRectangle.Right > bounds.Right)
      {
        int num = imageRectangle.Right - bounds.Right;
        imageRectangle.Width -= num;
      }
      if (imageRectangle.Bottom > bounds.Bottom)
      {
        int num = imageRectangle.Bottom - bounds.Bottom;
        imageRectangle.Height -= num;
      }
      if (imageRectangle.Y >= bounds.Y)
        return;
      imageRectangle.Y = bounds.Y;
    }

    private static void GetAlignmentRectangle(ref Rectangle alignmentRectangle, ref Size sz, ContentAlignment alignment, Padding margin)
    {
      int x1 = alignmentRectangle.X + margin.Left + alignmentRectangle.Width / 2 - sz.Width / 2 - margin.Right;
      int y1 = alignmentRectangle.Y + margin.Top + alignmentRectangle.Height / 2 - sz.Height / 2 - margin.Bottom;
      int y2 = alignmentRectangle.Y + margin.Top;
      int y3 = alignmentRectangle.Bottom - sz.Height - margin.Bottom;
      int x2 = alignmentRectangle.X + margin.Left;
      int x3 = alignmentRectangle.Right - sz.Width - margin.Right;
      switch (alignment)
      {
        case ContentAlignment.BottomCenter:
          alignmentRectangle = new Rectangle(x1, y3, sz.Width, sz.Height);
          break;
        case ContentAlignment.BottomRight:
          alignmentRectangle = new Rectangle(x3, y3, sz.Width, sz.Height);
          break;
        case ContentAlignment.MiddleRight:
          alignmentRectangle = new Rectangle(x3, y1, sz.Width, sz.Height);
          break;
        case ContentAlignment.BottomLeft:
          alignmentRectangle = new Rectangle(x2, y3, sz.Width, sz.Height);
          break;
        case ContentAlignment.TopLeft:
          alignmentRectangle = new Rectangle(x2, y2, sz.Width, sz.Height);
          break;
        case ContentAlignment.TopCenter:
          alignmentRectangle = new Rectangle(x1, y2, sz.Width, sz.Height);
          break;
        case ContentAlignment.TopRight:
          alignmentRectangle = new Rectangle(x3, y2, sz.Width, sz.Height);
          break;
        case ContentAlignment.MiddleLeft:
          alignmentRectangle = new Rectangle(x2, y1, sz.Width, sz.Height);
          break;
        case ContentAlignment.MiddleCenter:
          alignmentRectangle = new Rectangle(x1, y1, sz.Width, sz.Height);
          break;
      }
    }

    public static Rectangle GetImageRectangle(Image image, Rectangle r, ContentAlignment imageAlignment)
    {
      Size size = image.Size;
      int x = r.X + 2;
      int num = r.Y + 2;
      if ((imageAlignment & ImageAndTextHelper.isRight) != (ContentAlignment) 0)
        x = r.X + r.Width - 4 - size.Width;
      else if ((imageAlignment & ImageAndTextHelper.isCenter) != (ContentAlignment) 0)
        x = r.X + (r.Width - size.Width) / 2;
      int y = (imageAlignment & ImageAndTextHelper.isBottom) == (ContentAlignment) 0 ? ((imageAlignment & ImageAndTextHelper.isTop) == (ContentAlignment) 0 ? r.Y + (r.Height - size.Height) / 2 : r.Y + 2) : r.Y + r.Height - 4 - size.Height;
      return new Rectangle(x, y, size.Width, size.Height);
    }

    public static Rectangle GetBackgroundImageRectangle(Rectangle bounds, Image backgroundImage, ImageLayout imageLayout)
    {
      Rectangle rectangle = bounds;
      if (backgroundImage != null)
      {
        switch (imageLayout)
        {
          case ImageLayout.None:
            rectangle.Size = backgroundImage.Size;
            return rectangle;
          case ImageLayout.Tile:
            return rectangle;
          case ImageLayout.Center:
            rectangle.Size = backgroundImage.Size;
            Size size1 = bounds.Size;
            if (size1.Width > rectangle.Width)
              rectangle.X = (size1.Width - rectangle.Width) / 2;
            if (size1.Height > rectangle.Height)
              rectangle.Y = (size1.Height - rectangle.Height) / 2;
            return rectangle;
          case ImageLayout.Stretch:
            rectangle.Size = bounds.Size;
            return rectangle;
          case ImageLayout.Zoom:
            Size size2 = backgroundImage.Size;
            float num1 = (float) bounds.Width / (float) size2.Width;
            float num2 = (float) bounds.Height / (float) size2.Height;
            if ((double) num1 >= (double) num2)
            {
              rectangle.Height = bounds.Height;
              rectangle.Width = (int) ((double) size2.Width * (double) num2 + 0.5);
              if (bounds.X >= 0)
                rectangle.X = (bounds.Width - rectangle.Width) / 2;
              return rectangle;
            }
            rectangle.Width = bounds.Width;
            rectangle.Height = (int) ((double) size2.Height * (double) num1 + 0.5);
            if (bounds.Y >= 0)
              rectangle.Y = (bounds.Height - rectangle.Height) / 2;
            return rectangle;
        }
      }
      return rectangle;
    }

    internal static StringAlignment GetAlignment(ContentAlignment align)
    {
      if ((align & ImageAndTextHelper.isRight) != (ContentAlignment) 0)
        return StringAlignment.Far;
      return (align & ImageAndTextHelper.isCenter) != (ContentAlignment) 0 ? StringAlignment.Center : StringAlignment.Near;
    }

    internal static StringFormat InitializeStringFormatAlignment(ContentAlignment align)
    {
      return new StringFormat() { Alignment = ImageAndTextHelper.GetAlignment(align), LineAlignment = ImageAndTextHelper.GetLineAlignment(align) };
    }

    internal static StringAlignment GetLineAlignment(ContentAlignment align)
    {
      if ((align & ImageAndTextHelper.isBottom) != (ContentAlignment) 0)
        return StringAlignment.Far;
      return (align & ImageAndTextHelper.isMiddle) != (ContentAlignment) 0 ? StringAlignment.Center : StringAlignment.Near;
    }
  }
}
