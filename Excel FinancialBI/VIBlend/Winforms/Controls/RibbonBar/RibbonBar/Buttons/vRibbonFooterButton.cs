// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonFooterButton
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
  [ToolboxItem(false)]
  public class vRibbonFooterButton : vButton
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonFooterButton" /> class.
    /// </summary>
    public vRibbonFooterButton()
    {
      this.ShowFocusRectangle = false;
    }

    protected override void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte roundedCornersMask)
    {
      this.backFill.Radius = 0;
      if (controlState != ControlState.Normal && this.Enabled)
        base.DrawControl(graphics, bounds, controlState, isFocused, roundedCornersMask);
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      using (Pen pen = new Pen(this.backFill.BorderColor))
      {
        if (!this.Enabled)
          pen.Color = this.backFill.DisabledBorderColor;
        new RibbonPaintHelper().DrawFooterButton(graphics, this.backFill.BorderColor, this.ClientRectangle);
      }
      graphics.SmoothingMode = smoothingMode;
      vRibbonGroupContent ribbonGroupContent = this.Parent as vRibbonGroupContent;
      if (ribbonGroupContent == null || (ribbonGroupContent.RectangleToScreen(ribbonGroupContent.ClientRectangle).Contains(Cursor.Position) || !ribbonGroupContent.drawContentFill))
        return;
      ribbonGroupContent.drawContentFill = false;
      ribbonGroupContent.Invalidate();
    }
  }
}
