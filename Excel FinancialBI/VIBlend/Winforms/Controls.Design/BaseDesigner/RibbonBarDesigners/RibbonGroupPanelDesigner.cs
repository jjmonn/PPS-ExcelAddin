// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.RibbonGroupPanelDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class RibbonGroupPanelDesigner : ScrollableControlDesigner
  {
    public override SelectionRules SelectionRules
    {
      get
      {
        return SelectionRules.Locked;
      }
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (!(m.HWnd == this.Control.Handle))
        return;
      switch (m.Msg)
      {
        case 514:
        case 517:
          this.HitTest(RibbonGroupPanelDesigner.LoWord((int) m.LParam), RibbonGroupPanelDesigner.HiWord((int) m.LParam));
          break;
      }
    }

    public void HitTest(int x, int y)
    {
      ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      if (selectionService == null || selectionService == null)
        return;
      bool flag = false;
      foreach (Component selectedComponent in (IEnumerable) selectionService.GetSelectedComponents())
      {
        if (selectedComponent is RibbonGroupPanel)
        {
          flag = true;
          break;
        }
      }
      int num = flag ? 1 : 0;
    }

    public static int LoWord(int dwValue)
    {
      return dwValue & (int) ushort.MaxValue;
    }

    public static int HiWord(int dwValue)
    {
      return dwValue >> 16 & (int) ushort.MaxValue;
    }
  }
}
