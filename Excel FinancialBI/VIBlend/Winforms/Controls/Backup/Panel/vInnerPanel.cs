// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vContentPanel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  [ToolboxItem(false)]
  [Designer("VIBlend.WinForms.Controls.Design.vInnerPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vContentPanel : Panel
  {
    public vContentPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.Scroll += new ScrollEventHandler(this.vInnerPanel_Scroll);
      this.Layout += new LayoutEventHandler(this.vInnerPanel_Layout);
    }

    private void vInnerPanel_Layout(object sender, LayoutEventArgs e)
    {
      this.Invalidate();
    }

    private void vInnerPanel_Scroll(object sender, ScrollEventArgs e)
    {
      this.Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Invalidate();
    }
  }
}
