// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vNavPaneActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class vNavPaneActionList : DesignerActionList
  {
    public vNavPane NavPane
    {
      get
      {
        return this.Component as vNavPane;
      }
    }

    public NavPaneItemsCollection Items
    {
      get
      {
        return this.NavPane.Items;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.NavPane)["Items"].SetValue((object) this.NavPane, (object) value);
      }
    }

    public vNavPaneActionList(vNavPane pane)
      : base((IComponent) pane)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("Items", "Items") };
    }
  }
}
