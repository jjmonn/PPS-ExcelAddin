// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vTreeViewDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vTreeViewDesigner : vDesignerBase
  {
    private vTreeView view;
    private DesignerVerb addControl;
    private DesignerVerb addChildControl;
    private DesignerVerb removeControl;
    private bool isControl;
    private ISelectionService selectionService;
    private IDesignerHost designerHost;
    private IComponentChangeService componentChangeService;

    public override DesignerVerbCollection Verbs
    {
      get
      {
        return new DesignerVerbCollection(new DesignerVerb[3]{ this.addControl, this.addChildControl, this.removeControl });
      }
    }

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        vTreeViewActionList treeViewActionList = new vTreeViewActionList(this.Control as vTreeView);
        actionListCollection.Add((DesignerActionList) treeViewActionList);
        return actionListCollection;
      }
    }

    public override ICollection AssociatedComponents
    {
      get
      {
        ArrayList arrayList = new ArrayList();
        arrayList.AddRange(base.AssociatedComponents);
        if (this.Control != null)
        {
          foreach (vTreeNode node in this.view.Nodes)
          {
            if (node != null && node.Site != null)
              arrayList.Add((object) node);
          }
        }
        return (ICollection) arrayList;
      }
    }

    public vTreeViewDesigner()
    {
      this.addControl = new DesignerVerb("Add Root Node", new EventHandler(this.OnAddControl));
      this.addChildControl = new DesignerVerb("Add Child Node", new EventHandler(this.OnAddChildControl));
      this.removeControl = new DesignerVerb("Delete", new EventHandler(this.OnRemoveControl));
    }

    private void OnMoveUp(object sender, EventArgs e)
    {
      if (this.view == null || this.view.SelectedNode == null || this.view.SelectedNode.Site == null)
        return;
      this.view.SelectedNode = this.view.SelectedNode.PrevSiblingNode;
    }

    private void OnMoveDown(object sender, EventArgs e)
    {
      if (this.view == null || this.view.SelectedNode == null || this.view.SelectedNode.Site == null)
        return;
      this.view.SelectedNode = this.view.SelectedNode.NextSiblingNode;
    }

    private void OnRemoveControl(object sender, EventArgs e)
    {
      try
      {
        if (this.view.Nodes.Count <= 0)
          return;
        IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        DesignerTransaction transaction = designerHost.CreateTransaction("Delete");
        if (this.view.SelectedNode == null || this.view.SelectedNodes.Length <= 0 || this.view.SelectedNode.Site == null)
          return;
        vTreeNode selectedNode = this.view.SelectedNode;
        designerHost.DestroyComponent((IComponent) selectedNode);
        selectedNode.ParentCollection.Remove(selectedNode);
        transaction.Commit();
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }

    private void OnAddChildControl(object sender, EventArgs e)
    {
      if (this.view.SelectedNode == null)
        return;
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Child Node");
      this.view.SelectedNode.Nodes.Add((vTreeNode) designerHost.CreateComponent(typeof (vTreeNode)));
      this.view.Invalidate();
      transaction.Commit();
    }

    private void OnAddControl(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Node");
      this.view.Nodes.Add((vTreeNode) designerHost.CreateComponent(typeof (vTreeNode)));
      this.view.Invalidate();
      transaction.Commit();
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.view = component as vTreeView;
      this.selectionService = (ISelectionService) this.GetService(typeof (ISelectionService));
      this.designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      this.componentChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
      this.Wire();
    }

    private void RemoveNodes(vTreeNode parentNode, vTreeNodeCollection nodes)
    {
      foreach (vTreeNode node in nodes)
      {
        if (node != null && node.Site != null)
        {
          this.designerHost.DestroyComponent((IComponent) node);
          this.RemoveNodes(node, node.Nodes);
        }
      }
      if (parentNode == null)
        return;
      parentNode.Nodes.Clear();
    }

    protected virtual void ComponentRemoving(object sender, ComponentEventArgs e)
    {
      if (this.view == null || this.view.Disposing)
        return;
      this.selectionService.SetSelectedComponents((ICollection) null);
      this.view.selection.Clear();
      if (e.Component is vTreeNode && !this.isControl)
      {
        vTreeNode vTreeNode = e.Component as vTreeNode;
        if (vTreeNode.Site != null)
        {
          try
          {
            if (this.view.Nodes.Contains((object) vTreeNode))
              this.view.Nodes.Remove(vTreeNode);
            this.RemoveNodes(vTreeNode, vTreeNode.Nodes);
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.InnerException.StackTrace);
          }
        }
      }
      if (!(e.Component is vTreeView))
        return;
      this.isControl = true;
      try
      {
        foreach (vTreeNode node in this.view.Nodes)
        {
          if (node != null && node.Site != null)
          {
            this.designerHost.DestroyComponent((IComponent) node);
            this.RemoveNodes(node, node.Nodes);
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.InnerException.StackTrace);
      }
      this.isControl = false;
      if (this.view == null)
        return;
      this.view.Nodes.Clear();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.UnWire();
      base.Dispose(disposing);
    }

    private void UnWire()
    {
      if (this.componentChangeService == null)
        return;
      this.componentChangeService.ComponentRemoving -= new ComponentEventHandler(this.ComponentRemoving);
    }

    private void Wire()
    {
      if (this.componentChangeService == null)
        return;
      this.componentChangeService.ComponentRemoving += new ComponentEventHandler(this.ComponentRemoving);
    }

    protected override void WndProc(ref Message msg)
    {
      if (msg.Msg == 513 && this.view != null)
      {
        Point client = this.view.PointToClient(Cursor.Position);
        vTreeNode vTreeNode = this.view.HitTest(client);
        if (vTreeNode != null && vTreeNode.LabelBounds.Contains(client))
        {
          this.view.SelectedNode = vTreeNode;
          this.selectionService.SetSelectedComponents((ICollection) new ArrayList()
          {
            (object) vTreeNode
          });
        }
      }
      base.WndProc(ref msg);
    }
  }
}
