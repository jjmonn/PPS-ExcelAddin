﻿// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vDataGridViewGroupingHeaderDesignerBase
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;

namespace VIBlend.WinForms.Controls.Design
{
  public class vDataGridViewGroupingHeaderDesignerBase : vDesignerBase
  {
    private Control ctrl;

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        vDataGridViewGroupingHeaderActionList headerActionList = new vDataGridViewGroupingHeaderActionList((Control) (this.Control as vDataGridViewGroupsHeader));
        actionListCollection.Add((DesignerActionList) headerActionList);
        return actionListCollection;
      }
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.ctrl = component as Control;
    }
  }
}
