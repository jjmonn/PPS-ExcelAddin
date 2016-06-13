// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vDataGridViewSearchHeaderActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;

namespace VIBlend.WinForms.Controls.Design
{
  public class vDataGridViewSearchHeaderActionList : DesignerActionList
  {
    public vDataGridViewSearchHeader ControlBase
    {
      get
      {
        return this.Component as vDataGridViewSearchHeader;
      }
    }

    public vDataGridView DataGridView
    {
      get
      {
        return (vDataGridView) TypeDescriptor.GetProperties((object) this.ControlBase)["DataGridView"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["DataGridView"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vDataGridViewSearchHeaderActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("DataGridView", "DataGridView") };
    }
  }
}
