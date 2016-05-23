// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vTrackControlsActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vTrackControlsActionList : DesignerActionListBase
  {
    public override Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public int Value
    {
      get
      {
        return (int) TypeDescriptor.GetProperties((object) this.ControlBase)["Value"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Value"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public int Maximum
    {
      get
      {
        return (int) TypeDescriptor.GetProperties((object) this.ControlBase)["Maximum"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Maximum"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public int Minimum
    {
      get
      {
        return (int) TypeDescriptor.GetProperties((object) this.ControlBase)["Minimum"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Minimum"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vTrackControlsActionList(Control ctrl)
      : base(ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("Value", "Value"), (DesignerActionItem) new DesignerActionPropertyItem("Minimum", "Minimum"), (DesignerActionItem) new DesignerActionPropertyItem("Maximum", "Maximum") };
    }
  }
}
