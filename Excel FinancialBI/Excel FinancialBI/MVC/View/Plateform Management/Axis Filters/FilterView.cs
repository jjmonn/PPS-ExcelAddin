using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Utils;
  using Forms;
  using Model;
  using Controller;
  using Model.CRUD;
  using Network;

  public partial class FilterView : UserControl, IView
  {
    private FbiFilterHierarchyTreeView m_tree;
    private FilterController m_controller;
    private RightManager m_rightMgr = new RightManager();

    public FilterView()
    {
      InitializeComponent();
    }

    public void LoadView()
    {
      try
      {
        m_tree = new FbiFilterHierarchyTreeView(m_controller.AxisType, true);
        m_tree.ContextMenuStrip = m_contextRightClick;
        m_tree.Dock = DockStyle.Fill;
        m_valuePanel.Controls.Add(m_tree, 0, 1);
        this.RegisterEvents();
        this.MultilangueSetup();
        this.DefineUIPermissions();
        this.DesactivateUnallowed();
      }
      catch (Exception e)
      {
        MessageBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.StackTrace);
      }
    }

    private void DesactivateUnallowed()
    {
      m_rightMgr.Enable(UserModel.Instance.GetCurrentUserRights());
    }

    private void DefineUIPermissions()
    {
      m_rightMgr[m_renameRightClick] = Group.Permission.EDIT_AXIS;
      m_rightMgr[m_deleteRightClick] = Group.Permission.DELETE_AXIS;
      m_rightMgr[m_addValueRightClick] = Group.Permission.CREATE_AXIS;
      m_rightMgr[m_editStruct] = Group.Permission.EDIT_AXIS;
    }

    private void MultilangueSetup()
    {
      this.m_editStruct.Text = Local.GetValue("filters.edit_structure");
      this.m_addValueRightClick.Text = Local.GetValue("filters.new_value");
      this.m_deleteRightClick.Text = Local.GetValue("general.delete");
      this.m_renameRightClick.Text = Local.GetValue("general.rename");
      this.m_expand.Text = Local.GetValue("general.expand_all");
      this.m_collapse.Text = Local.GetValue("general.collapse_all");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as FilterController;
    }

    private void RegisterEvents()
    {
      m_tree.KeyDown += OnTreeKeyDown;
      m_addValueRightClick.Click += OnAddValueClick;
      m_deleteRightClick.Click += OnDeleteClick;
      m_renameRightClick.Click += OnRenameClick;
      m_editStruct.Click += OnEditStructClick;
      m_expand.Click += OnExpandClick;
      m_collapse.Click += OnCollapseClick;
      m_tree.NodeMouseDown += OnTreeMouseDown;
      m_tree.NodeDropped += OnTreeNodeDropped;
      FilterValueModel.Instance.ReadEvent += OnModelRead;
      FilterValueModel.Instance.CreationEvent += OnModelCreate;
      FilterValueModel.Instance.DeleteEvent += OnModelDelete;
      FilterValueModel.Instance.UpdateEvent += OnModelUpdate;
    }

    private void OnTreeNodeDropped(vTreeNode p_draggedNode, vTreeNode p_targetNode)
    {
      if (p_targetNode == null || p_draggedNode.Equals(p_targetNode))
        return;

      m_tree.DoDragDrop(p_draggedNode, DragDropEffects.None);
      if (this.Update(p_draggedNode, p_targetNode))
      {
        p_draggedNode.Remove();
      }
    }

    private void OnTreeMouseDown(object sender, vTreeViewMouseEventArgs e)
    {
      vTreeNode l_node;

      l_node = e.Node;
      if (l_node != null && ModifierKeys.HasFlag(Keys.Control) == true && this.IsValue(l_node))
      {
        m_tree.DoDragDrop(l_node, DragDropEffects.Move);
      }
    }

    public void Reload()
    {
      m_tree.Nodes.Clear();
      m_tree.Load();
      m_tree.Refresh();
    }

    #region Utils

    private bool IsCategory(vTreeNode p_node)
    {
      if (p_node == null)
        return (false);
      return (p_node.Parent == null);
    }

    private bool IsValue(vTreeNode p_node)
    {
      if (p_node == null)
        return (false);
      return (!this.IsCategory(p_node));
    }

    private bool AskConfirmation(string msg, string title)
    {
      if (MessageBox.Show(Local.GetValue(msg), Local.GetValue(title), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
        return (true);
      return (false);
    }

    private bool Create(vTreeNode p_atNode, string p_name)
    {
      Filter l_filter;
      FilterValue l_filterValue;

      if (this.IsCategory(p_atNode))
      {
        if ((l_filter = FilterModel.Instance.GetValue((UInt32)p_atNode.Value)) != null)
          return (m_controller.Add(p_name, 0, l_filter.Id, typeof(FilterValue)));
        return (false);
      }
      if ((l_filterValue = FilterValueModel.Instance.GetValue((UInt32)p_atNode.Value)) != null)
      {
        if ((l_filter = FilterModel.Instance.GetChild(l_filterValue.FilterId, m_controller.AxisType)) == null)
          return (false);
        return (m_controller.Add(p_name, l_filterValue.Id, l_filter.Id, typeof(FilterValue)));
      }
      return (false);
    }

    private bool Update(vTreeNode p_node, vTreeNode p_atNode)
    {
      Filter l_filter;
      FilterValue l_filterValue;

      if (this.IsCategory(p_atNode))
      {
        if ((l_filter = FilterModel.Instance.GetValue((UInt32)p_atNode.Value)) != null)
          return (m_controller.UpdateValue((UInt32)p_node.Value, 0, l_filter.Id));
        return (false);
      }
      if ((l_filterValue = FilterValueModel.Instance.GetValue((UInt32)p_atNode.Value)) != null)
      {
        if ((l_filter = FilterModel.Instance.GetChild(l_filterValue.FilterId, m_controller.AxisType)) == null)
          return (false);
        return (m_controller.UpdateValue((UInt32)p_node.Value, l_filterValue.Id, l_filter.Id));
      }
      return (false);
    }

    private bool Remove(vTreeNode p_node)
    {
      if (this.IsCategory(p_node))
      {
        return (m_controller.Remove((UInt32)p_node.Value, typeof(Filter)));
      }
      return (m_controller.Remove((UInt32)p_node.Value, typeof(FilterValue)));
    }

    private bool IsNodeSelected(string title)
    {
      if (m_tree.SelectedNode == null)
      {
        MessageBox.Show(Local.GetValue("filters.error.no_selection"), Local.GetValue(title), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return (false);
      }
      return (true);
    }

    #endregion

    #region FormEvents

    private void OnTreeKeyDown(object p_sender, KeyEventArgs p_e)
    {
      switch (p_e.KeyCode)
      {
        case Keys.Delete:
          this.OnDeleteClick(null, null);
          break;
        case Keys.Back:
          this.OnDeleteClick(null, null);
          break;
        case Keys.Space:
          this.OnRenameClick(null, null);
          break;
      }
    }

    private void CreateValue(vTreeNode p_node)
    {
      if (this.IsCategory(p_node))
      {
        this.CreateValueFromCategory(p_node);
      }
      else
      {
        this.CreateValueFromValue(p_node);
      }
    }

    private void CreateValueFromValue(vTreeNode p_node)
    {
      Filter l_filter;
      FilterValue l_value;
      string l_filterName;

      if ((l_value = FilterValueModel.Instance.GetValue((UInt32)p_node.Value)) != null)
      {
        if ((l_filter = FilterModel.Instance.GetChild(l_value.FilterId, m_controller.AxisType)) == null) //Can't go deeper, if you known what I mean ;)
        {
          MessageBox.Show(Local.GetValue("filters.error.no_child"), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return;
        }
        l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_value_name")).Trim();
        if (!m_controller.Add(l_filterName, l_value.Id, l_filter.Id, typeof(FilterValue)))
        {
          MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void CreateValueFromCategory(vTreeNode p_node)
    {
      string l_filterName;

      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_value_name")).Trim();
      if (!this.Create(p_node, l_filterName))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void OnAddValueClick(object p_sender, EventArgs p_e)
    {
      if (!this.IsNodeSelected("filters.new_value"))
        return;
      this.CreateValue(m_tree.SelectedNode);
    }

    private void OnDeleteClick(object p_sender, EventArgs p_e)
    {
      if (!this.IsNodeSelected("general.delete"))
        return;
      if (this.AskConfirmation("filters.confirmation_delete", "filters.confirmation_delete"))
      {
        if (!this.Remove(m_tree.SelectedNode))
        {
          MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void OnRenameClick(object p_sender, EventArgs p_e)
    {
      vTreeNode l_node;
      string l_filterName;

      l_node = m_tree.SelectedNode;
      if (!this.IsNodeSelected("filters.new_value"))
        return;
      if (!this.IsCategory(l_node))
      {
        l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name")).Trim();
        if (!m_controller.Update((UInt32)l_node.Value, l_filterName, typeof(FilterValue)))
          MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void OnEditStructClick(object p_sender, EventArgs p_e)
    {
      m_controller.ShowFilterStructView();
    }

    private void OnExpandClick(object sender, EventArgs e)
    {
      foreach (vTreeNode l_node in m_tree.Nodes)
      {
        l_node.Expand();
      }
    }

    private void OnCollapseClick(object sender, EventArgs e)
    {
      foreach (vTreeNode l_node in m_tree.Nodes)
      {
        l_node.Collapse(false);
      }
    }

    #endregion

    #region ServerEvents

    delegate void OnModelCreate_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelCreate(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnModelCreate_delegate func = new OnModelCreate_delegate(OnModelCreate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show("{CREATE}", "filters.new_value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          this.DesactivateUnallowed();
        }
      }
    }

    delegate void OnModelUpdate_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelUpdate(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnModelUpdate_delegate func = new OnModelUpdate_delegate(OnModelUpdate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
        {
          MessageBox.Show("{UPDATE}", "filters.new_category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        this.DesactivateUnallowed();
      }
    }

    delegate void OnModelDelete_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDelete(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnModelDelete_delegate func = new OnModelDelete_delegate(OnModelDelete);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status == Network.ErrorMessage.SUCCESS)
        {
          vTreeNode l_node;

          if ((l_node = m_tree.Get(p_id, typeof(FilterValue))) != null && l_node.Parent != null)
          {
            l_node.Parent.Nodes.Remove(l_node);
          }
          else if (l_node != null && l_node.Parent == null)
          {
            m_tree.Nodes.Remove(l_node);
          }
          m_tree.Refresh();
          return;
        }
        MessageBox.Show("{DELETE}", "general.delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, FilterValue p_attributes);
    void OnModelRead(Network.ErrorMessage p_status, FilterValue p_attributes)
    {
      if (InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        vTreeNode l_node, l_parentNode;

        if (p_status == ErrorMessage.SUCCESS)
        {
          if ((l_node = m_tree.Get(p_attributes.Id, typeof(FilterValue))) == null) //If the node must be created
          {
            l_node = new vTreeNode();
            l_node.Value = p_attributes.Id;
            l_node.Text = p_attributes.Name;
            l_node.Tag = typeof(FilterValue);
            if ((l_parentNode = m_tree.Get(p_attributes.ParentId, typeof(FilterValue))) == null) //If the parent is null, add to the tree -> Category, else, add the node to the parent
            {
              if ((l_parentNode = m_tree.Get(p_attributes.FilterId, typeof(Filter))) == null) //Get the filterNode -> Category
                return;
            }
            l_parentNode.Nodes.Add(l_node);
          }
          else //Else, the node must be updated
          {
            l_node.Text = p_attributes.Name;
          }
          m_tree.Refresh();
          this.DesactivateUnallowed();
        }
      }
    }

    #endregion
  }
}