// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RibbonGroupPanel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  [Designer("VIBlend.WinForms.Controls.Design.RibbonGroupPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class RibbonGroupPanel : Panel
  {
    public RibbonGroupPanel()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.AutoScroll = false;
    }
  }
}
