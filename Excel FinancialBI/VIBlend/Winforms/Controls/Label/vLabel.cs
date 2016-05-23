// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vLabel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vLabel control</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vLabel), "ControlIcons.Label.ico")]
  [Description("Displays a descriptive text or an image. The control allows the user to set opacity, and rotate the content.")]
  public class vLabel : ScrollableControlMiniBase
  {
    private bool multiLine = true;
    private ContentAlignment textAlignment = ContentAlignment.TopLeft;
    private ContentAlignment imageAlignment = ContentAlignment.TopLeft;
    private bool useMnemonics = true;
    private float opacity = 1f;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Image image;
    private double rotationAngle;
    private LabelItemStyle displayStyle;
    private BackgroundElement backFill;
    private bool paintFill;
    private bool paintBorder;
    private bool elipsis;
    private bool paintclientarea;
    private ControlTheme theme;

    /// <summary>Gets or sets the opacity of the vLabel control</summary>
    /// <remarks>The opacity property must be a value between 0 and 1</remarks>
    [Category("Behavior")]
    [DefaultValue(1f)]
    public float Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        this.opacity = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines whether the background will be painted in the full area.
    /// </summary>
    [Description("Determines whether the background will be painted in the full area.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool PaintClientArea
    {
      get
      {
        return this.paintclientarea;
      }
      set
      {
        this.paintclientarea = value;
        this.Invalidate();
      }
    }

    /// <summary>Determines whether the background will be painted</summary>
    [Category("Behavior")]
    [Description("Determines whether the background will be painted")]
    [DefaultValue(false)]
    public bool PaintFill
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

    /// <summary>Determines whether the border will be painted</summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a border")]
    [DefaultValue(false)]
    public bool PaintBorder
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

    /// <summary>Gets the default size of the control.</summary>
    /// <value></value>
    /// <returns>
    /// The default <see cref="T:System.Drawing.Size" /> of the control.
    /// </returns>
    protected override Size DefaultSize
    {
      get
      {
        return new Size(80, 25);
      }
    }

    /// <summary>Gets or sets the style of the vLabel control</summary>
    [Category("Behavior")]
    public LabelItemStyle DisplayStyle
    {
      get
      {
        return this.displayStyle;
      }
      set
      {
        this.displayStyle = value;
      }
    }

    /// <summary>Gets or sets the image of the vLabel control</summary>
    [Category("Behavior")]
    [DefaultValue(null)]
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

    /// <summary>
    /// Gets or sets the image alignment of the vLabel control
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the image alignment of the vLabel control")]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlignment;
      }
      set
      {
        this.imageAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment of the vLabel control</summary>
    /// <value>The text alignment.</value>
    [Category("Behavior")]
    [Description("Gets or sets the text alignment of the vLabel control")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether mnemonic will be used
    /// </summary>
    /// <value><c>true</c> if UseMnemonics a mnemonic characted is recognized; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether mnemonic will be used")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool UseMnemonics
    {
      get
      {
        return this.useMnemonics;
      }
      set
      {
        this.useMnemonics = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the rotation angle of the vLabel control</summary>
    [Category("Behavior")]
    [DefaultValue(0.0)]
    [Description("Gets or sets the rotation angle of the vLabel control")]
    public double RotationAngle
    {
      get
      {
        return this.rotationAngle;
      }
      set
      {
        this.rotationAngle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.vLabel" /> is drawn with elipsis.
    /// </summary>
    /// <value><c>true</c> if multiline draws the text on multiple lines; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the text is drawn with elipsis")]
    public bool Ellipsis
    {
      get
      {
        return this.elipsis;
      }
      set
      {
        this.elipsis = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.vLabel" /> is multiline.
    /// </summary>
    /// <value><c>true</c> if multiline draws the text on multiple lines; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether the text is drawn on multiple lines")]
    [Category("Behavior")]
    public bool Multiline
    {
      get
      {
        return this.multiLine;
      }
      set
      {
        this.multiLine = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the text associated with this control.</summary>
    /// <value></value>
    /// <returns>The text associated with this control.</returns>
    [DefaultValue("")]
    [Description("Display Text")]
    [Category("Appearance")]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control")]
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

    static vLabel()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vLabelItem" /> class.
    /// </summary>
    public vLabel()
    {
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.Text = "Label";
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
      Graphics graphics = e.Graphics;
      if (this.DisplayStyle == LabelItemStyle.ImageOnly)
        this.DrawImage(graphics);
      else
        this.DrawText(graphics);
    }

    private void DrawImage(Graphics g)
    {
      if (this.Image == null)
        return;
      double num1 = this.rotationAngle / 180.0;
      Size size = new Size(this.Image.Width, this.Image.Height);
      Bitmap bitmap1 = new Bitmap(this.Image.Width, this.Image.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap1);
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      graphics.DrawImage(this.Image, Point.Empty);
      Bitmap bitmap2 = this.RotateBitmap((Image) bitmap1, this.RotationAngle);
      Rectangle rectangle = new Rectangle(Point.Empty, size);
      float width = this.GetRotatedSize((SizeF) size, this.RotationAngle).Width;
      float height = this.GetRotatedSize((SizeF) size, this.RotationAngle).Height;
      PointF point = PointF.Empty;
      float x1 = 0.0f;
      float x2 = (float) (rectangle.Width / 2) - width / 2f;
      float x3 = (float) rectangle.Width - width;
      float y1 = 0.0f;
      float y2 = (float) (rectangle.Height / 2) - height / 2f;
      float y3 = (float) rectangle.Height - height;
      switch (this.imageAlignment)
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
      byte num2 = (byte) ((double) byte.MaxValue - (double) byte.MaxValue * (double) this.opacity);
      PaintHelper.SetOpacityToImage(bitmap2, num2);
      g.DrawImage((Image) bitmap2, point);
    }

    private void DrawText(Graphics g)
    {
      if (string.IsNullOrEmpty(this.Text))
        return;
      StringFormat format = new StringFormat();
      if (this.UseMnemonics)
        format.HotkeyPrefix = HotkeyPrefix.Show;
      if (!this.Multiline)
        format.FormatFlags = StringFormatFlags.NoWrap;
      if (this.Ellipsis)
        format.Trimming = StringTrimming.EllipsisCharacter;
      if (this.RightToLeft == RightToLeft.Yes)
        format.FormatFlags = StringFormatFlags.DirectionRightToLeft;
      Size size1 = Size.Ceiling(this.GetRotatedSize((SizeF) this.ClientRectangle.Size, this.RotationAngle));
      Size size2 = Size.Ceiling(g.MeasureString(this.Text, this.Font, size1.Width, format));
      float width = (float) size2.Width;
      float height = (float) size2.Height;
      Rectangle clientRectangle = this.ClientRectangle;
      double num1 = this.rotationAngle / 180.0;
      SizeF bounds = new SizeF(width, height);
      float num2 = (float) (int) this.GetRotatedSize(bounds, this.RotationAngle).Width;
      float num3 = (float) (int) this.GetRotatedSize(bounds, this.RotationAngle).Height;
      Bitmap bitmap1 = new Bitmap((int) width, (int) height);
      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
      g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
      Graphics g1 = Graphics.FromImage((Image) bitmap1);
      g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
      g1.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        g1.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      this.backFill.Opacity = this.opacity;
      if (!this.PaintClientArea)
      {
        if (this.PaintFill)
        {
          this.backFill.Bounds = new Rectangle(0, 0, (int) bounds.Width - 1, (int) bounds.Height - 1);
          this.backFill.DrawElementFill(g1, ControlState.Normal);
        }
        if (this.PaintBorder)
        {
          this.backFill.Bounds = new Rectangle(0, 0, (int) bounds.Width - 1, (int) bounds.Height - 1);
          this.backFill.DrawElementBorder(g1, ControlState.Normal);
        }
      }
      else
      {
        if (this.PaintFill)
        {
          this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
          this.backFill.DrawElementFill(g, ControlState.Normal);
        }
        if (this.PaintBorder)
        {
          this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
          this.backFill.DrawElementBorder(g, ControlState.Normal);
        }
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int) (byte) ((double) byte.MaxValue * (double) this.opacity), this.ForeColor)))
      {
        Rectangle rectangle = new Rectangle(Point.Empty, size1);
        g1.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle, format);
      }
      Bitmap bitmap2 = this.RotateBitmap((Image) bitmap1, this.RotationAngle);
      PointF pointF = PointF.Empty;
      float x1 = 0.0f;
      float x2 = (float) clientRectangle.Width / 2f - num2 / 2f;
      float x3 = (float) clientRectangle.Width - num2;
      float y1 = 0.0f;
      float y2 = (float) clientRectangle.Height / 2f - num3 / 2f;
      float y3 = (float) clientRectangle.Height - num3;
      switch (this.TextAlignment)
      {
        case ContentAlignment.BottomCenter:
          pointF = new PointF(x2, y3);
          break;
        case ContentAlignment.BottomRight:
          pointF = new PointF(x3, y3);
          break;
        case ContentAlignment.MiddleRight:
          pointF = new PointF(x3, y2);
          break;
        case ContentAlignment.BottomLeft:
          pointF = new PointF(x1, y3);
          break;
        case ContentAlignment.TopLeft:
          pointF = PointF.Empty;
          break;
        case ContentAlignment.TopCenter:
          pointF = new PointF(x2, y1);
          break;
        case ContentAlignment.TopRight:
          pointF = new PointF(x3, y1);
          break;
        case ContentAlignment.MiddleLeft:
          pointF = new PointF(pointF.X, y2);
          break;
        case ContentAlignment.MiddleCenter:
          pointF = new PointF(x2, y2);
          break;
      }
      g.DrawImage((Image) bitmap2, Point.Ceiling(pointF));
    }

    private SizeF GetRotatedSize(SizeF bounds, double angle)
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
      return new SizeF((float) (num4 + num7), (float) (num6 + num5));
    }

    private Bitmap RotateBitmap(Image image, double angle)
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
      double num8 = num4 + num7;
      double num9 = num6 + num5;
      double num10 = num8;
      double num11 = num9;
      Bitmap bitmap = new Bitmap((int) num10, (int) num11);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        PointF[] destPoints;
        if (num3 >= 0.0 && num3 < Math.PI / 2.0)
          destPoints = new PointF[3]
          {
            new PointF((float) num7, 0.0f),
            new PointF((float) num10, (float) num5),
            new PointF(0.0f, (float) num6)
          };
        else if (num3 >= Math.PI / 2.0 && num3 < Math.PI)
          destPoints = new PointF[3]
          {
            new PointF((float) num10, (float) num5),
            new PointF((float) num4, (float) num11),
            new PointF((float) num7, 0.0f)
          };
        else if (num3 >= Math.PI && num3 < 3.0 * Math.PI / 2.0)
          destPoints = new PointF[3]
          {
            new PointF((float) num4, (float) num11),
            new PointF(0.0f, (float) num6),
            new PointF((float) num10, (float) num5)
          };
        else
          destPoints = new PointF[3]
          {
            new PointF(0.0f, (float) num6),
            new PointF((float) num7, 0.0f),
            new PointF((float) num4, (float) num11)
          };
        graphics.DrawImage(image, destPoints);
      }
      return bitmap;
    }

    private StringAlignment GetHorizontalAlignment()
    {
      switch (this.TextAlignment)
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

    private StringAlignment GetVerticalAlignment()
    {
      switch (this.TextAlignment)
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
  }
}
