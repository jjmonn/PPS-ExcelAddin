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

    public FilterView()
    {
      InitializeComponent();
    }

    public void LoadView()
    {
      try
      {
        m_tree = new FbiFilterHierarchyTreeView(m_controller.AxisType);
        m_tree.ContextMenuStrip = m_contextRightClick;
        m_tree.Dock = DockStyle.Fill;
        m_valuePanel.Controls.Add(m_tree, 0, 1);
        this.RegisterEvents();
        this.LoadLanguage();
      }
      catch (Exception e)
      {
        MessageBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message + e.StackTrace);
      }
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as FilterController;
    }

    private void LoadLanguage()
    {
      m_editStruct.Text = Local.GetValue("filters.edit_structure");
      m_addValueRightClick.Text = Local.GetValue("filters.new_value");
      m_deleteRightClick.Text = Local.GetValue("general.delete");
      m_renameRightClick.Text = Local.GetValue("general.rename");
      m_expand.Text = Local.GetValue("general.expand_all");
      m_collapse.Text = Local.GetValue("general.collapse_all");
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
      FilterValueModel.Instance.ReadEvent += OnModelRead;
      FilterValueModel.Instance.CreationEvent += OnModelCreate;
      FilterValueModel.Instance.DeleteEvent += OnModelDelete;
      FilterValueModel.Instance.UpdateEvent += OnModelUpdate;
    }

    public void Reload()
    {
      m_tree.Nodes.Clear();
      m_tree.Load();
      m_tree.Refresh();
    }

    #region Utils

    private bool IsCategory()
    {
      if (m_tree.SelectedNode == null)
        return (false);
      return (m_tree.SelectedNode.Parent == null);
    }

    private bool IsValue()
    {
      if (m_tree.SelectedNode == null)
        return (false);
      return (!this.IsCategory());
    }

    private bool AskConfirmation(string msg, string title)
    {
      if (MessageBox.Show(Local.GetValue(msg), Local.GetValue(title), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
        return (true);
      return (false);
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

    private void CreateValue()
    {
      if (this.IsCategory())
      {
        this.CreateValueFromCategory();
      }
      else
      {
        this.CreateValueFromValue();
      }
    }

    private void CreateValueFromValue()
    {
      Filter l_filter;
      FilterValue l_value;
      string l_filterName;
      
      if ((l_value = FilterValueModel.Instance.GetValue((UInt32)m_tree.SelectedNode.Value)) != null)
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

    private void CreateValueFromCategory()
    {
      Filter l_value;
      string l_filterName;

      if ((l_value = FilterModel.Instance.GetValue((UInt32)m_tree.SelectedNode.Value)) != null)
      {
        l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_value_name")).Trim();
        if (!m_controller.Add(l_filterName, 0, l_value.Id, typeof(FilterValue)))
        {
          MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void OnAddValueClick(object p_sender, EventArgs p_e)
    {
      if (!this.IsNodeSelected("filters.new_value"))
        return;
      this.CreateValue();
    }

    private void OnDeleteClick(object p_sender, EventArgs p_e)
    {
      if (!this.IsNodeSelected("general.delete"))
        return;
      if (this.IsCategory() && this.AskConfirmation("filters.msg_delete_category", "filters.delete_category"))
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value, typeof(Filter));
      }
      else if (this.IsValue() && this.AskConfirmation("filters.msg_delete_value", "filters.delete_value"))
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value, typeof(FilterValue));
      }
      else
      {
        MessageBox.Show(Local.GetValue("filters.error.no_selection"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void OnRenameClick(object p_sender, EventArgs p_e)
    {
      string l_filterName;

      if (!this.IsNodeSelected("filters.new_value"))
        return;
      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name")).Trim();
      if (this.IsCategory())
      {
        if (!m_controller.Update((UInt32)m_tree.SelectedNode.Value, l_filterName, typeof(Filter)))
          MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        if (!m_controller.Update((UInt32)m_tree.SelectedNode.Value, l_filterName, typeof(FilterValue)))
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
    void OnModelCreate(Network.ErrorMessage p_status, uint p_id)
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
        }
      }
    }

    delegate void OnModelUpdate_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelUpdate(Network.ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnModelUpdate_delegate func = new OnModelUpdate_delegate(OnModelUpdate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show("{UPDATE}", "filters.new_category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    delegate void OnModelDelete_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDelete(Network.ErrorMessage p_status, uint p_id)
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
        }
      }
    }

    #endregion
  }
}