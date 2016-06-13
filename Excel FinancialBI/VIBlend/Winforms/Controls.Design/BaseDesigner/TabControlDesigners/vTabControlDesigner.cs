// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vTabControlDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  internal class vTabControlDesigner : vDesignerBase
  {
    private DesignerVerb addControl;
    private DesignerVerb removeControl;
    private vTabControl tabControl;
    private IComponentChangeService service;

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        vTabControlActionList controlActionList = new vTabControlActionList(this.Control as vTabControl);
        actionListCollection.Add((DesignerActionList) controlActionList);
        return actionListCollection;
      }
    }

    protected override bool DrawGrid
    {
      get
      {
        return false;
      }
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        return new DesignerVerbCollection(new DesignerVerb[2]{ this.addControl, this.removeControl });
      }
    }

    public vTabControlDesigner()
    {
      this.addControl = new DesignerVerb("Add Page", new EventHandler(this.OnAddControl));
      this.removeControl = new DesignerVerb("Remove Page", new EventHandler(this.OnRemoveControl));
    }

    protected override bool GetHitTest(Point point)
    {
      if (this.tabControl != null && this.tabControl.HitTest(point) != null)
      {
        if (this.tabControl.Controls.Count > 0)
        {
          Rectangle screen1 = this.tabControl.Controls[0].RectangleToScreen(this.tabControl.Controls[0].ClientRectangle);
          Rectangle screen2 = this.tabControl.Controls[1].RectangleToScreen(this.tabControl.Controls[0].ClientRectangle);
          if (screen1.Contains(point) || screen2.Contains(point))
            return true;
        }
        return false;
      }
      if (this.tabControl != null && this.tabControl.Controls.Count > 0)
      {
        Rectangle screen1 = this.tabControl.Controls[0].RectangleToScreen(this.tabControl.Controls[0].ClientRectangle);
        Rectangle screen2 = this.tabControl.Controls[1].RectangleToScreen(this.tabControl.Controls[0].ClientRectangle);
        if (screen1.Contains(point) || screen2.Contains(point))
          return true;
      }
      return base.GetHitTest(point);
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.service = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
      this.tabControl = (vTabControl) this.Control;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      foreach (Component tabContext in this.tabControl.TabContexts)
        tabContext.Dispose();
    }

    private void OnAddControl(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Page");
      vTabPage vTabPage = (vTabPage) designerHost.CreateComponent(typeof (vTabPage));
      this.tabControl.TabPages.Add(vTabPage);
      vTabPage.Dock = DockStyle.Fill;
      this.tabControl.Invalidate();
      transaction.Commit();
    }

    private void OnRemoveControl(object sender, EventArgs e)
    {
      if (this.tabControl.Controls.Count <= 2)
        return;
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Remove Page");
      if (this.tabControl.SelectedTab == null)
        this.tabControl.SelectedTab = this.tabControl.Controls[2] as vTabPage;
      if (this.tabControl.SelectedTab == null)
        return;
      designerHost.DestroyComponent((IComponent) this.tabControl.SelectedTab);
      this.tabControl.TabPages.Remove(this.tabControl.SelectedTab);
      this.tabControl.Invalidate();
      transaction.Commit();
    }

    protected override void WndProc(ref Message msg)
    {
      if (msg.Msg == 513)
      {
        vTabPage vTabPage = this.tabControl.HitTest(this.tabControl.PointToClient(Cursor.Position));
        if (vTabPage != null)
        {
          this.tabControl.SelectedTab = vTabPage;
          ((ISelectionService) this.GetService(typeof (ISelectionService))).SetSelectedComponents((ICollection) new ArrayList()
          {
            (object) vTabPage
          });
        }
      }
      if (msg.Msg == 512)
        this.tabControl.Invalidate();
      if (msg.Msg == 675)
        this.tabControl.Invalidate();
      base.WndProc(ref msg);
    }
  }
}
