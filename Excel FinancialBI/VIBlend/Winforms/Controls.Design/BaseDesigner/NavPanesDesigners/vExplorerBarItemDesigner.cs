// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vExplorerBarItemDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  internal class vExplorerBarItemDesigner : ParentControlDesigner
  {
    private vExplorerBarItem item;

    public override SelectionRules SelectionRules
    {
      get
      {
        return SelectionRules.TopSizeable | SelectionRules.BottomSizeable;
      }
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        return new DesignerVerbCollection(new DesignerVerb[2]{ new DesignerVerb("Expand Item", new EventHandler(this.Expand)), new DesignerVerb("Collapse Item", new EventHandler(this.Collapse)) });
      }
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.item = component as vExplorerBarItem;
      if (this.item == null || this.item.HeaderControl == null || this.item.ItemPanel == null)
        return;
      this.SetChildAsContainer((Control) this.item.HeaderControl, "vNavPaneHeader");
      this.SetChildAsContainer((Control) this.item.ItemPanel, "ItemPanel");
    }

    protected virtual void Expand(object sender, EventArgs e)
    {
      (this.Control as vExplorerBarItem).Expanded = true;
    }

    protected virtual void Collapse(object sender, EventArgs e)
    {
      (this.Control as vExplorerBarItem).Expanded = false;
    }

    protected override bool GetHitTest(Point point)
    {
      return true;
    }

    public bool SetChildAsContainer(Control ctrl, string propertyName)
    {
      try
      {
        INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
        if (nestedContainer == null)
          return false;
        for (int index = 0; index < nestedContainer.Components.Count; ++index)
        {
          if (nestedContainer.Components[index].Equals((object) ctrl))
            return true;
        }
        nestedContainer.Add((IComponent) ctrl, propertyName);
        return true;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
      return false;
    }
  }
}
