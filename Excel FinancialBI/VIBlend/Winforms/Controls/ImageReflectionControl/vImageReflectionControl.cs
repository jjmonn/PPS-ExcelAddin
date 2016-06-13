// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vImageReflectionControl
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vImageReflectionControl control</summary>
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vImageReflectionControl), "ControlIcons.ImageReflectionControl.ico")]
  [Description("Displays a reflection image of a control or another image.")]
  public class vImageReflectionControl : Control
  {
    private int reflectivity = 90;
    private bool drawControlReflection = true;
    private bool enableOpacityEffect = true;
    private Control content;
    private Image image;

    /// <summary>
    /// Gets or sets a value indicating whether to draw only content's reflection
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to draw only content's reflection")]
    public bool DrawContentReflection
    {
      get
      {
        return this.drawControlReflection;
      }
      set
      {
        this.drawControlReflection = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image opacity effect is enabled
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the image opacity effect is enabled")]
    public bool EnableOpacityEffect
    {
      get
      {
        return this.enableOpacityEffect;
      }
      set
      {
        this.enableOpacityEffect = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the content.</summary>
    /// <value>The content.</value>
    [Category("Behavior")]
    [Description("Gets or sets the content.")]
    public Control Content
    {
      get
      {
        return this.content;
      }
      set
      {
        this.content = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the reflection factor.</summary>
    [Description("Gets or sets the reflection factor.")]
    [Category("Behavior")]
    public int ReflectionFactor
    {
      get
      {
        return this.reflectivity;
      }
      set
      {
        if (value < 30 || value > (int) byte.MaxValue)
          return;
        this.reflectivity = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the image.</summary>
    [Category("Behavior")]
    [Description("Gets or sets the image.")]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        this.Invalidate();
      }
    }

    /// <summary>vImageReflectionControl constructor</summary>
    public vImageReflectionControl()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.drawControlReflection && this.content != null)
      {
        Bitmap bitmap = new Bitmap(this.content.Width, this.content.Height);
        if (this.content.IsDisposed)
        {
          this.content = (Control) null;
        }
        else
        {
          this.content.DrawToBitmap(bitmap, new Rectangle(0, 0, this.content.Width, this.content.Height));
          Image mirroredImage = this.GetMirroredImage((Image) bitmap, this.BackColor, this.ReflectionFactor);
          e.Graphics.DrawImage(mirroredImage, Point.Empty);
        }
      }
      else
      {
        if (this.Image == null)
          return;
        Image image = this.DrawReflectionEffect(this.Image, this.BackColor, this.ReflectionFactor);
        e.Graphics.DrawImage(image, Point.Empty);
      }
    }

    internal Image GetMirroredImage(Image image, Color backColor, int refFactor)
    {
      int height1 = (int) ((double) image.Height + (double) image.Height * ((double) refFactor / (double) byte.MaxValue));
      Bitmap bitmap = new Bitmap(image.Width, height1, PixelFormat.Format24bppRgb);
      bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
      using (Graphics graphics1 = Graphics.FromImage((Image) bitmap))
      {
        graphics1.Clear(backColor);
        graphics1.InterpolationMode = InterpolationMode.HighQualityBicubic;
        Rectangle rectangle = new Rectangle(0, 0, image.Size.Width, image.Size.Height);
        int height2 = image.Height * refFactor / (int) byte.MaxValue;
        Image image1 = (Image) new Bitmap(image.Width, height2);
        using (Graphics graphics2 = Graphics.FromImage(image1))
          graphics2.DrawImage(image, new Rectangle(0, 0, image1.Width, image1.Height), 0, image.Height - image1.Height, image1.Width, image1.Height, GraphicsUnit.Pixel);
        image1.RotateFlip(RotateFlipType.Rotate180FlipX);
        Rectangle rect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height * refFactor / (int) byte.MaxValue);
        graphics1.DrawImage(image1, rect);
        if (this.EnableOpacityEffect)
        {
          LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb((int) byte.MaxValue - refFactor, backColor), backColor, 90f, false);
          graphics1.FillRectangle((Brush) linearGradientBrush, rect);
        }
      }
      return (Image) bitmap;
    }

    internal Image DrawReflectionEffect(Image image, Color backColor, int refFactor)
    {
      int height1 = (int) ((double) image.Height + (double) image.Height * ((double) refFactor / (double) byte.MaxValue));
      Bitmap bitmap = new Bitmap(image.Width, height1, PixelFormat.Format24bppRgb);
      bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
      using (Graphics graphics1 = Graphics.FromImage((Image) bitmap))
      {
        graphics1.Clear(backColor);
        graphics1.DrawImage(image, new Point(0, 0));
        graphics1.InterpolationMode = InterpolationMode.HighQualityBicubic;
        Rectangle rectangle = new Rectangle(0, image.Size.Height, image.Size.Width, image.Size.Height);
        int height2 = image.Height * refFactor / (int) byte.MaxValue;
        Image image1 = (Image) new Bitmap(image.Width, height2);
        using (Graphics graphics2 = Graphics.FromImage(image1))
          graphics2.DrawImage(image, new Rectangle(0, 0, image1.Width, image1.Height), 0, image.Height - image1.Height, image1.Width, image1.Height, GraphicsUnit.Pixel);
        image1.RotateFlip(RotateFlipType.Rotate180FlipX);
        Rectangle rect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height * refFactor / (int) byte.MaxValue);
        graphics1.DrawImage(image1, rect);
        if (this.EnableOpacityEffect)
        {
          LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb((int) byte.MaxValue - refFactor, backColor), backColor, 90f, false);
          graphics1.FillRectangle((Brush) linearGradientBrush, rect);
        }
      }
      return (Image) bitmap;
    }
  }
}
