// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRibbonButton control</summary>
  /// <remarks>
  /// A ribbon button control raises an event when the user clicks it, and provides customizable visual appearance, and themes.
  /// </remarks>
  [ToolboxBitmap(typeof (vRibbonButton), "ControlIcons.vRibbonButton.ico")]
  [ToolboxItem(true)]
  [Description("Displays a ribbon button that raises an event when the user clicks it.")]
  public class vRibbonButton : vButton
  {
    private bool paintDefaultFill;
    private bool paintDefaultBorder;
    internal bool isSelected;

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a background")]
    public bool PaintDefaultStateFill
    {
      get
      {
        return this.paintDefaultFill;
      }
      set
      {
        this.paintDefaultFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a border
    /// </summary>
    /// <value><c>true</c> if [paint border]; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to paint a border")]
    [Browsable(false)]
    [Category("Behavior")]
    public bool PaintDefaultStateBorder
    {
      get
      {
        return this.paintDefaultBorder;
      }
      set
      {
        this.paintDefaultBorder = value;
        this.Invalidate();
      }
    }

    protected override Rectangle DrawBorder(Graphics graphics, ControlState controlState, Rectangle drawRect)
    {
      if (this.BorderStyle == ButtonBorderStyle.SOLID && this.PaintBorder)
      {
        Rectangle rectangle = drawRect;
        bool flag = false;
        if (controlState != ControlState.Normal)
        {
          this.backFill.DrawElementBorder(graphics, controlState);
          flag = true;
        }
        else if (controlState == ControlState.Normal && this.PaintDefaultStateBorder)
        {
          this.backFill.DrawElementBorder(graphics, controlState);
          flag = true;
        }
        if (flag)
        {
          rectangle.Inflate(-1, -1);
          Color color = Color.Empty;
          if (controlState == ControlState.Normal)
            color = this.Theme.QueryColorSetter("ButtonNormalInnerBorderColor");
          else if (controlState == ControlState.Hover)
            color = this.Theme.QueryColorSetter("ButtonHighlightInnerBorderColor");
          else if (controlState == ControlState.Pressed)
            color = this.Theme.QueryColorSetter("ButtonPressedInnerBorderColor");
          if ((int) color.A > 0)
          {
            this.backFill.Bounds = rectangle;
            this.backFill.DrawElementBorder(graphics, controlState, color);
          }
        }
      }
      return drawRect;
    }

    protected override void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte roundedCornersMask)
    {
      Graphics graphics1 = graphics;
      Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
      if ((double) this.opacity != 1.0)
        graphics1 = Graphics.FromImage((Image) bitmap);
      SmoothingMode smoothingMode = graphics1.SmoothingMode;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      this.backFill.Opacity = 1f;
      Rectangle drawRect1 = bounds;
      --drawRect1.Height;
      --drawRect1.Width;
      this.backFill.Bounds = drawRect1;
      this.backFill.RoundedCornersBitmask = roundedCornersMask;
      this.backFill.Radius = this.RoundedCornersRadius;
      if (this.isSelected)
        controlState = ControlState.Pressed;
      if (this.PaintFill)
      {
        if (controlState != ControlState.Normal)
          this.backFill.DrawElementFill(graphics1, controlState);
        else if (controlState == ControlState.Normal && this.PaintDefaultStateFill)
          this.backFill.DrawElementFill(graphics1, controlState);
      }
      Rectangle imageAndTextRect = this.GetImageAndTextRect(ref bounds, controlState);
      this.DrawImageAndText(graphics1, controlState, imageAndTextRect);
      Rectangle drawRect2 = this.DrawBorder(graphics1, controlState, drawRect1);
      this.DrawFocusRectangle(graphics1, isFocused, drawRect2, roundedCornersMask);
      graphics1.SmoothingMode = smoothingMode;
      if ((double) this.opacity == 1.0 || bitmap == null)
        return;
      PaintHelper.SetOpacityToImage(bitmap, (byte) ((double) byte.MaxValue * (double) this.opacity));
      graphics.DrawImage((Image) bitmap, 0, 0);
      graphics1.Dispose();
      bitmap.Dispose();
    }
  }
}
