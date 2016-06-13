// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vCircularButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vCircularButton control</summary>
  /// <remarks>
  /// A circular button control raises an event when the user clicks it, and provides customizable visual appearance, and themes.
  /// </remarks>
  [ToolboxItem(true)]
  [Description("Displays a circular button that raises an event when the user clicks it.")]
  [ToolboxBitmap(typeof (vCircularButton), "ControlIcons.vCircularButton.ico")]
  public class vCircularButton : vButton
  {
    /// <summary>Gets or sets the theme of the control</summary>
    [Description("Gets or sets the theme of the control")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
    public override ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        this.theme = value.CreateCopy();
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("ApplicationButtonNormal");
        if (fillStyle1 != null)
          this.theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("ApplicationButtonHighlight");
        if (fillStyle2 != null)
          this.theme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("ApplicationButtonPressed");
        if (fillStyle2 != null)
          this.theme.StylePressed.FillStyle = fillStyle3;
        this.backFill.LoadTheme(this.theme);
      }
    }

    /// <summary>vCircularButton constructor</summary>
    public vCircularButton()
    {
      this.StyleKey = "CircularButton";
    }

    protected override void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte roundedCornersMask)
    {
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      this.backFill.Opacity = this.Opacity;
      this.backFill.Shape = Shapes.Circle;
      Rectangle clientRectangle = this.ClientRectangle;
      --clientRectangle.Height;
      --clientRectangle.Width;
      this.backFill.Bounds = clientRectangle;
      if (this.PaintFill)
        this.backFill.DrawElementFill(graphics, controlState);
      if (this.PaintBorder)
        this.backFill.DrawElementBorder(graphics, controlState);
      Rectangle imageAndTextRect = this.GetImageAndTextRect(ref bounds, controlState);
      this.DrawImageAndText(graphics, controlState, imageAndTextRect);
      graphics.SmoothingMode = smoothingMode;
    }
  }
}
