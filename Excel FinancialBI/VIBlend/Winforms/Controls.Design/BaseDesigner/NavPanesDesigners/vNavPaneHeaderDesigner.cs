// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.NavPaneHeaderDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class NavPaneHeaderDesigner : ParentControlDesigner
  {
    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
    }

    protected override bool GetHitTest(Point point)
    {
      return true;
    }
  }
}
