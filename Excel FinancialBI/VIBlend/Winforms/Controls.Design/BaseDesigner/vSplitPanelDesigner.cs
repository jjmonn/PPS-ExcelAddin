// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vSplitPanelDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class vSplitPanelDesigner : ParentControlDesigner
  {
    protected override bool AllowControlLasso
    {
      get
      {
        return false;
      }
    }

    public override SelectionRules SelectionRules
    {
      get
      {
        return SelectionRules.Locked;
      }
    }
  }
}
