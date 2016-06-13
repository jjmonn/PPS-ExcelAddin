// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vMaskBoxActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vMaskBoxActionList : DesignerActionListBase
  {
    public override Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public DefaultMasks DefaultMask
    {
      get
      {
        return (DefaultMasks) TypeDescriptor.GetProperties((object) this.ControlBase)["DefaultMask"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["DefaultMask"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public string Mask
    {
      get
      {
        return (string) TypeDescriptor.GetProperties((object) this.ControlBase)["Mask"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Mask"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vMaskBoxActionList(Control ctrl)
      : base(ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("DefaultMask", "DefaultMask"), (DesignerActionItem) new DesignerActionPropertyItem("Mask", "Mask") };
    }
  }
}
