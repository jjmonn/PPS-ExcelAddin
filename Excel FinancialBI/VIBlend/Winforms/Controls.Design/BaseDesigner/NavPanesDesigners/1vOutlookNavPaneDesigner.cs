﻿// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vOutlookNavPaneActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class vOutlookNavPaneActionList : DesignerActionList
  {
    public vOutlookNavPane NavPane
    {
      get
      {
        return this.Component as vOutlookNavPane;
      }
    }

    public OutlookItemsCollection Items
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

    public vOutlookNavPaneActionList(vOutlookNavPane pane)
      : base((IComponent) pane)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("Items", "Items") };
    }
  }
}
