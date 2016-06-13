// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vPanelDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vPanelDesigner : vDesignerBase
  {
    private vPanel panel;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.panel = component as vPanel;
      this.SetChildAsContainer((Control) this.panel.Content, "Content");
    }

    protected override bool GetHitTest(Point point)
    {
      return true;
    }

    public bool SetChildAsContainer(Control control, string name)
    {
      try
      {
        INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
        if (nestedContainer == null)
          return false;
        for (int index = 0; index < nestedContainer.Components.Count; ++index)
        {
          if (nestedContainer.Components[index].Equals((object) control))
            return true;
        }
        nestedContainer.Add((IComponent) control, name);
        return true;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
      return false;
    }
  }
}
