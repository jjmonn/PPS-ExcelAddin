// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vListItemsActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vListItemsActionList : DesignerActionList
  {
    public Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public ListItemsCollection Items
    {
      get
      {
        return (ListItemsCollection) TypeDescriptor.GetProperties((object) this.ControlBase)["Items"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Items"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vListItemsActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("Items", "Items") };
    }
  }
}
