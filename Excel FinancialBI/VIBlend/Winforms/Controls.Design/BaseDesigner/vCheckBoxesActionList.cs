// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vCheckBoxesActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vCheckBoxesActionList : DesignerActionList
  {
    public Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public List<vCheckBox> CheckBoxes
    {
      get
      {
        return (List<vCheckBox>) TypeDescriptor.GetProperties((object) this.ControlBase)["CheckBoxes"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["CheckBoxes"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vCheckBoxesActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("CheckBoxes", "CheckBoxes") };
    }
  }
}
