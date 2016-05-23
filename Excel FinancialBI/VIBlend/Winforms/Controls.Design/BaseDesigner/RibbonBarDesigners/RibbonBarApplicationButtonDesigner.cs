// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vRibbonApplicationButtonDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  public class vRibbonApplicationButtonDesigner : ParentControlDesigner
  {
    protected override bool GetHitTest(Point point)
    {
      return true;
    }

    public override void Initialize(IComponent component)
    {
      try
      {
        base.Initialize(component);
        vRibbonApplicationButton applicationButton = this.Component as vRibbonApplicationButton;
        if (applicationButton == null || applicationButton.Content == null)
          return;
        this.SetPanelDesignTimeHost((Control) applicationButton.Content, "Content");
      }
      catch (Exception ex)
      {
      }
    }

    public bool SetPanelDesignTimeHost(Control panel, string property)
    {
      INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
      if (nestedContainer == null)
        return false;
      for (int index = 0; index < nestedContainer.Components.Count; ++index)
      {
        if (nestedContainer.Components[index].Equals((object) panel))
          return true;
      }
      nestedContainer.Add((IComponent) panel, property);
      return true;
    }
  }
}
