// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vTreeViewActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace VIBlend.WinForms.Controls.Design
{
  public class vTreeViewActionList : DesignerActionList
  {
    public vTreeView TreeView
    {
      get
      {
        return this.Component as vTreeView;
      }
    }

    public bool ShowRootLines
    {
      get
      {
        return this.TreeView.ShowRootLines;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["ShowRootLines"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool CheckBoxes
    {
      get
      {
        return this.TreeView.CheckBoxes;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["CheckBoxes"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool EnableToggleAnimation
    {
      get
      {
        return this.TreeView.EnableToggleAnimation;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["EnableToggleAnimation"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public vTreeNodeCollection Nodes
    {
      get
      {
        return this.TreeView.Nodes;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["Nodes"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool LabelEdit
    {
      get
      {
        return this.TreeView.LabelEdit;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["LabelEdit"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool DefaultExpandCollapseIndicators
    {
      get
      {
        return this.TreeView.DefaultExpandCollapseIndicators;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["DefaultExpandCollapseIndicators"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool EnableToolTips
    {
      get
      {
        return this.TreeView.EnableToolTips;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["EnableToolTips"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool EnableIndicatorsAnimation
    {
      get
      {
        return this.TreeView.EnableIndicatorsAnimation;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["EnableIndicatorsAnimation"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool AllowTriStateCheckBoxes
    {
      get
      {
        return this.TreeView.TriStateMode;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["TriStateMode"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public bool AllowDragAndDrop
    {
      get
      {
        return this.TreeView.AllowDragAndDrop;
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.TreeView)["AllowDragAndDrop"].SetValue((object) this.TreeView, (object) value);
      }
    }

    public vTreeViewActionList(vTreeView view)
      : base((IComponent) view)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("CheckBoxes", "CheckBoxes"), (DesignerActionItem) new DesignerActionPropertyItem("AllowTriStateCheckBoxes", "Allow tri state check boxes"), (DesignerActionItem) new DesignerActionPropertyItem("AllowDragAndDrop", "Allow nodes drag and drop"), (DesignerActionItem) new DesignerActionPropertyItem("EnableToggleAnimation", "Enable Toggle Animation"), (DesignerActionItem) new DesignerActionPropertyItem("EnableIndicatorsAnimation", "Enable expand/collapse indicators animation"), (DesignerActionItem) new DesignerActionPropertyItem("EnableToolTips", "Enable nodes' tooltips"), (DesignerActionItem) new DesignerActionPropertyItem("LabelEdit", "Allow nodes text edit"), (DesignerActionItem) new DesignerActionPropertyItem("DefaultExpandCollapseIndicators", "Use the default plus/minus signs as indicators"), (DesignerActionItem) new DesignerActionPropertyItem("ShowRootLines", "Show nodes' connecting lines"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "OnAddControl", "Add Node", "Properties", "Add Node"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "OnAddChildControl", "Add Child Node", "Properties", "Add Child Node"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "OnRemoveControl", "Remove Node", "Properties", "Remove Node") };
    }

    private void OnRemoveControl()
    {
      try
      {
        if (this.TreeView.Nodes.Count <= 0)
          return;
        IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
        DesignerTransaction transaction = designerHost.CreateTransaction("Delete");
        if (this.TreeView.SelectedNode == null || this.TreeView.SelectedNodes.Length <= 0 || this.TreeView.SelectedNode.Site == null)
          return;
        vTreeNode selectedNode = this.TreeView.SelectedNode;
        designerHost.DestroyComponent((IComponent) selectedNode);
        selectedNode.ParentCollection.Remove(selectedNode);
        transaction.Commit();
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }

    protected virtual void OnAddChildControl()
    {
      if (this.TreeView.SelectedNode == null)
        return;
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Child Node");
      this.TreeView.SelectedNode.Nodes.Add((vTreeNode) designerHost.CreateComponent(typeof (vTreeNode)));
      this.TreeView.Invalidate();
      transaction.Commit();
    }

    protected virtual void OnAddControl()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Node");
      this.TreeView.Nodes.Add((vTreeNode) designerHost.CreateComponent(typeof (vTreeNode)));
      this.TreeView.Invalidate();
      transaction.Commit();
    }
  }
}
