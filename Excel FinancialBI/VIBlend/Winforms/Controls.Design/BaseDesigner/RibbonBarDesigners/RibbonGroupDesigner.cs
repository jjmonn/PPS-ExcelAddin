// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.RibbonGroupDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class RibbonGroupDesigner : vDesignerBase
  {
    private vRibbonGroup item;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.item = component as vRibbonGroup;
      this.SetContent((Control) this.item.Content, "vRibbonGroupContent");
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 513)
        return;
      ((ISelectionService) this.GetService(typeof (ISelectionService))).SetSelectedComponents((ICollection) new ArrayList()
      {
        (object) this.item
      });
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
