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

  public partial class FiltersView : UserControl, IView
  {
    private FbiFilterHierarchyTreeView m_tree;
    private FilterController m_controller;
    private FiltersStructController m_filtersStructController;

    public FiltersView()
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
      m_filtersStructController = new FiltersStructController(m_controller.AxisType);
    }

    private void LoadLanguage()
    {
      m_categoriesMenu.Text = Local.GetValue("filters.categories");
      m_addValue.Text = Local.GetValue("filters.new_value");
      m_delete.Text = Local.GetValue("general.delete");
      m_rename.Text = Local.GetValue("general.rename");
      m_editStruct.Text = Local.GetValue("filters.edit_structure");
      m_addValueRightClick.Text = Local.GetValue("filters.new_value");
      m_deleteRightClick.Text = Local.GetValue("general.delete");
      m_renameRightClick.Text = Local.GetValue("general.rename");
      m_expand.Text = Local.GetValue("general.expand_all");
      m_collapse.Text = Local.GetValue("general.collapse_all");
    }

    private void RegisterEvents()
    {
//      m_addValue.Click += OnAddValueClick;
      m_addValueRightClick.Click += OnAddValueClick;
//      m_delete.Click += m_delete_Click;
      m_deleteRightClick.Click += m_delete_Click;
//      m_rename.Click += m_rename_Click;
      m_renameRightClick.Click += m_rename_Click;
      m_editStruct.Click += m_editStruct_Click;
      m_valuePanel.Click += m_deselect;
      m_collapse.Click += m_collapse_Click;
      m_expand.Click += m_expand_Click;
      this.KeyDown += OnKeyDown;
      FilterValueModel.Instance.ReadEvent += OnModelRead;
      FilterValueModel.Instance.CreationEvent += OnModelCreate;
      FilterValueModel.Instance.DeleteEvent += OnModelDelete;
      FilterValueModel.Instance.UpdateEvent += OnModelUpdate;
    }

    private void OnKeyDown(object p_sender, KeyEventArgs p_e)
    {
      switch (p_e.KeyCode)
      {
        case Keys.Delete:
          break;
      }
    }

    #region Utils

    private bool IsCategory()
    {
      if (m_tree.SelectedNode == null)
        return (false);
      return (m_tree.SelectedNode.Nodes.Count > 0);
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

    #endregion

    #region FormEvents

    private void OnAddValueClick(object p_sender, EventArgs p_e)
    {
      UInt32 l_parentId;
      string l_filterName;
      FilterValue l_filter;

      if (m_tree.SelectedNode == null)
      {
        MessageBox.Show(Local.GetValue("filters.msg_no_category_selected"), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      if ((l_filter = FilterValueModel.Instance.GetValue((UInt32)m_tree.SelectedNode.Value)) != null)
      {
        l_parentId = (m_tree.SelectedNode == null ? 0 : (UInt32)m_tree.SelectedNode.Value);
        l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_value_name")).Trim();
        if (!m_controller.Add(l_filterName, l_filter.FilterId, l_parentId))
        {
          MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void m_delete_Click(object sender, EventArgs e)
    {
      if (this.IsCategory() && this.AskConfirmation("filters.msg_delete_category", "filters.delete_category"))
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value);
      }
      else if (this.IsValue() && this.AskConfirmation("filters.msg_delete_value", "filters.delete_value"))
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value);
      }
      else
      {
        MessageBox.Show(Local.GetValue("filters.msg_no_category_selected"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void m_rename_Click(object sender, EventArgs e)
    {
      string l_filterValueName;

      if (m_tree.SelectedNode == null)
      {
        MessageBox.Show(Local.GetValue("filters.msg_no_category_selected"), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      l_filterValueName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name")).Trim();
      if (!m_controller.Update((UInt32)m_tree.SelectedNode.Value, l_filterValueName))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void m_editStruct_Click(object sender, EventArgs e)
    {
      //Open AxisFiltersStructController
    }

    private void m_deselect(object sender, EventArgs e)
    {
      m_tree.SelectedNode = null;
    }

    private void m_expand_Click(object sender, EventArgs e)
    {
      foreach (vTreeNode l_node in m_tree.Nodes)
      {
        l_node.Expand();
      }
    }

    private void m_collapse_Click(object sender, EventArgs e)
    {
      foreach (vTreeNode l_node in m_tree.Nodes)
      {
        l_node.Collapse(false);
      }
    }

    #endregion

    #region ServerEvents

    void OnModelCreate(Network.ErrorMessage p_status, uint p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show("", "filters.new_value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    void OnModelUpdate(Network.ErrorMessage p_status, uint p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show("", "filters.new_value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    void OnModelDelete(ErrorMessage p_status, uint p_id)
    {
      if (p_status == Network.ErrorMessage.SUCCESS)
      {
        vTreeNode l_node;

        if ((l_node = m_tree.Get(p_id, typeof(FilterValue))) != null && l_node.Parent != null)
        {
          l_node.Parent.Nodes.Remove(l_node);
          return;
        }
      }
      MessageBox.Show("CANNOT DELETE, BECAUSE FUCK YOU !", "filters.new_value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    //A FilterValue as changed in the model
    private void OnModelRead(ErrorMessage p_status, FilterValue p_attributes)
    {
      vTreeNode l_node, l_parentNode;

      if (p_status == ErrorMessage.SUCCESS)
      {
        if ((l_node = m_tree.Get(p_attributes.Id, typeof(FilterValue))) == null) //If the node must be created
        {
          if ((l_parentNode = m_tree.Get(p_attributes.ParentId, typeof(FilterValue))) == null) //Cannot update without the parent
            return;
          l_node = new vTreeNode();
          l_node.Value = p_attributes.Id;
          l_node.Text = p_attributes.Name;
          l_node.Tag = p_attributes.GetType();
          l_parentNode.Nodes.Add(l_node);
        }
        else //Else, the node must be updated
        {
          l_node.Text = p_attributes.Name;
        }
      }
    }

    #endregion

    private void m_editStruct_Click_1(object sender, EventArgs e)
    {
      m_filtersStructController.ShowView();
    }

  }
}