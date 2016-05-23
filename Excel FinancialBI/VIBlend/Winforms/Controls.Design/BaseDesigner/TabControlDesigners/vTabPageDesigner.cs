// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vTabPageDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VIBlend.WinForms.Controls.Design
{
  internal class vTabPageDesigner : ParentControlDesigner
  {
    private DesignerVerb moveLeft;
    private DesignerVerb moveRight;
    private vTabPage tabPage;

    public override SelectionRules SelectionRules
    {
      get
      {
        return SelectionRules.None;
      }
    }

    public vTabPageDesigner()
    {
      this.moveLeft = new DesignerVerb("Move Left", new EventHandler(this.OnMoveLeft));
      this.moveRight = new DesignerVerb("Move Right", new EventHandler(this.OnMoveRight));
    }

    public override bool CanBeParentedTo(IDesigner parentDesigner)
    {
      return parentDesigner.Component is vTabControl;
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.tabPage = component as vTabPage;
    }

    private void OnMoveLeft(object sender, EventArgs e)
    {
      int childIndex = this.tabPage.Parent.Controls.GetChildIndex((Control) this.tabPage);
      if (childIndex <= 0)
        return;
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      IComponentChangeService componentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
      DesignerTransaction transaction = designerHost.CreateTransaction("Move Left");
      componentChangeService.OnComponentChanging((object) this.tabPage.Parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) this.tabPage.Parent)["Controls"]);
      this.tabPage.Parent.Controls.SetChildIndex((Control) this.tabPage, childIndex - 1);
      componentChangeService.OnComponentChanged((object) this.tabPage.Parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) this.tabPage.Parent)["Controls"], (object) null, (object) null);
      transaction.Commit();
      this.tabPage.Parent.Invalidate();
    }

    private void OnMoveRight(object sender, EventArgs e)
    {
      int childIndex = this.tabPage.Parent.Controls.GetChildIndex((Control) this.tabPage);
      if (childIndex >= this.tabPage.Parent.Controls.Count - 1)
        return;
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      IComponentChangeService componentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
      DesignerTransaction transaction = designerHost.CreateTransaction("Move Right");
      componentChangeService.OnComponentChanging((object) this.tabPage.Parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) this.tabPage.Parent)["Controls"]);
      this.tabPage.Parent.Controls.SetChildIndex((Control) this.tabPage, childIndex + 1);
      componentChangeService.OnComponentChanged((object) this.tabPage.Parent, (MemberDescriptor) TypeDescriptor.GetProperties((object) this.tabPage.Parent)["Controls"], (object) null, (object) null);
      transaction.Commit();
      this.tabPage.Parent.Invalidate();
    }

    protected override void WndProc(ref Message m)
    {
      int msg = m.Msg;
      base.WndProc(ref m);
    }

    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
      if (this.tabPage == null)
        return;
      using (Pen pen = new Pen(SystemColors.ControlDark))
      {
        pen.DashStyle = DashStyle.Dash;
        pe.Graphics.DrawRectangle(pen, 0, 0, this.tabPage.Width - 1, this.tabPage.Height - 1);
      }
    }
  }
}
