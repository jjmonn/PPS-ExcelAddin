// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.OutlookItemDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class OutlookItemDesigner : ParentControlDesigner
  {
    private vOutlookItem item;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.item = component as vOutlookItem;
      if (this.item == null || this.item.HeaderControl == null)
        return;
      this.SetPanelToHostChildren((Control) this.item.HeaderControl, "vNavPaneHeader");
    }

    public bool SetPanelToHostChildren(Control childControl, string controlName)
    {
      try
      {
        INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
        if (nestedContainer == null)
          return false;
        for (int index = 0; index < nestedContainer.Components.Count; ++index)
        {
          if (nestedContainer.Components[index].Equals((object) childControl))
            return true;
        }
        nestedContainer.Add((IComponent) childControl, controlName);
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
