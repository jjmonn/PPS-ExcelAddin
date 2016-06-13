// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vRibbonBarDesignerActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vRibbonBarDesignerActionList : DesignerActionList
  {
    public virtual Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public List<TabContext> TabContexts
    {
      get
      {
        return (List<TabContext>) TypeDescriptor.GetProperties((object) this.ControlBase)["TabContexts"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["TabContexts"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vTabCollection TabPages
    {
      get
      {
        return (vTabCollection) TypeDescriptor.GetProperties((object) this.ControlBase)["TabPages"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["TabPages"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vRibbonBarDesignerActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("TabPages", "Ribbon TabPages"), (DesignerActionItem) new DesignerActionPropertyItem("TabContexts", "Ribbon TabContexts") };
    }
  }
}
