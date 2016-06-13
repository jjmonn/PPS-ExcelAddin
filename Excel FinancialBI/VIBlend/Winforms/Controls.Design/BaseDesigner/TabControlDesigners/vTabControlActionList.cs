// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vTabControlActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class vTabControlActionList : DesignerActionList
  {
    public vTabControl TabControl
    {
      get
      {
        return this.Component as vTabControl;
      }
    }

    public vTabCollection TabPages
    {
      get
      {
        return this.TabControl.TabPages;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TabControl)["TabPages"].SetValue((object) this.TabControl, (object) value);
      }
    }

    public int TabsSpacing
    {
      get
      {
        return this.TabControl.TabsSpacing;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TabControl)["TabsSpacing"].SetValue((object) this.TabControl, (object) value);
      }
    }

    public int TabsInitialOffset
    {
      get
      {
        return this.TabControl.TabsInitialOffset;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TabControl)["TabsInitialOffset"].SetValue((object) this.TabControl, (object) value);
      }
    }

    public vTabPageAlignment TabAlignment
    {
      get
      {
        return this.TabControl.TabAlignment;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TabControl)["TabAlignment"].SetValue((object) this.TabControl, (object) value);
      }
    }

    public vTabControlActionList(vTabControl view)
      : base((IComponent) view)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("TabPages", "TabPages"), (DesignerActionItem) new DesignerActionPropertyItem("TabAlignment", "Tabs Alignment"), (DesignerActionItem) new DesignerActionPropertyItem("TabsSpacing", "Tab pages spacing"), (DesignerActionItem) new DesignerActionPropertyItem("TabsInitialOffset", "Tab pages start position") };
    }
  }
}
