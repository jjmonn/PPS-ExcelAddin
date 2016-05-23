// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vDateTimePickerActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vDateTimePickerActionList : DesignerActionList
  {
    public Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public DateTime Value
    {
      get
      {
        return (DateTime) TypeDescriptor.GetProperties((object) this.ControlBase)["Value"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Value"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public string FormatValue
    {
      get
      {
        return (string) TypeDescriptor.GetProperties((object) this.ControlBase)["FormatValue"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["FormatValue"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public DefaultDateTimePatterns DefaultDateTimeFormat
    {
      get
      {
        return (DefaultDateTimePatterns) TypeDescriptor.GetProperties((object) this.ControlBase)["DefaultDateTimeFormat"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["DefaultDateTimeFormat"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vDateTimePickerActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("DefaultDateTimeFormat", "DefaultDateTimeFormat"), (DesignerActionItem) new DesignerActionPropertyItem("FormatValue", "FormatValue"), (DesignerActionItem) new DesignerActionPropertyItem("Value", "Value") };
    }
  }
}
