// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.RibbonGroupContentDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class RibbonGroupContentDesigner : vDesignerBase
  {
    private vRibbonGroupContent item;

    public override SelectionRules SelectionRules
    {
      get
      {
        return SelectionRules.Locked;
      }
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.item = component as vRibbonGroupContent;
      this.SetContent((Control) this.item.ContentPanel, "ContentPanel");
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
          this.HitTest(RibbonGroupContentDesigner.LoWord((int) m.LParam), RibbonGroupContentDesigner.HiWord((int) m.LParam));
          break;
      }
    }

    public void HitTest(int x, int y)
    {
      ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      if (selectionService == null || selectionService == null)
        return;
      selectionService.SetSelectedComponents((ICollection) new Component[1]
      {
        (Component) this.ParentComponent
      }, SelectionTypes.Click);
    }

    public static int LoWord(int dwValue)
    {
      return dwValue & (int) ushort.MaxValue;
    }

    public static int HiWord(int dwValue)
    {
      return dwValue >> 16 & (int) ushort.MaxValue;
    }

    public bool SetContent(Control control, string property)
    {
      try
      {
        if (control == null)
          throw new ArgumentNullException("child");
        if (property == null)
          throw new ArgumentNullException("name");
        INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
        if (nestedContainer == null)
          return false;
        for (int index = 0; index < nestedContainer.Components.Count; ++index)
        {
          if (nestedContainer.Components[index].Equals((object) control))
            return true;
        }
        nestedContainer.Add((IComponent) control, property);
        return true;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
      return false;
    }
  }
}
