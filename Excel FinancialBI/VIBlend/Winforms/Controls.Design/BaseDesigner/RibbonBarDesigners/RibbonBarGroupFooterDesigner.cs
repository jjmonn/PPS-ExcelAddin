// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.RibbonGroupFooterDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class RibbonGroupFooterDesigner : ParentControlDesigner
  {
    private vRibbonGroupFooter footer;
    private ISelectionService selectionService;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.footer = component as vRibbonGroupFooter;
      this.selectionService = (ISelectionService) this.GetService(typeof (ISelectionService));
    }

    protected override bool GetHitTest(Point point)
    {
      return true;
    }

    protected override void WndProc(ref Message msg)
    {
      if (msg.Msg == 513)
      {
        this.selectionService.SetSelectedComponents((ICollection) null);
        this.selectionService.SetSelectedComponents((ICollection) new ArrayList()
        {
          (object) this.footer.RibbonGroup
        });
      }
      base.WndProc(ref msg);
    }
  }
}
