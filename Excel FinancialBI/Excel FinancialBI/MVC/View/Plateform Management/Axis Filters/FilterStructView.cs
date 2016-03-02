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
  using Model;
  using Forms;
  using Utils;
  using Network;
  using Controller;
  using Model.CRUD;

  public partial class FilterStructView : Form, IView
  {
    private FilterController m_controller;
    private FbiTreeView<Filter> m_tree;

    public FilterStructView()
    {
      InitializeComponent();
    }

    public void LoadView()
    {
      try
      {
        m_tree = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(m_controller.AxisType));
        m_tree.Dock = DockStyle.Fill;
        m_tree.ContextMenuStrip = m_structureTreeviewRightClickMenu;
        m_filterPanel.Content.Controls.Add(m_tree);
        this.LoadLanguage();
        this.SuscribeEvents();
        this.MultilangueSetup();
      }
      catch (Exception e)
      {
        Forms.MsgBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message);
      }
    }

    private void MultilangueSetup()
    {

      this.m_addFilter.Text = Local.GetValue("general.create");
      this.m_deleteFilter.Text = Local.GetValue("general.delete");
      this.m_createSubCategory.Text = Local.GetValue("general.create_sub_category");
      this.m_renameButton.Text = Local.GetValue("general.rename");
      this.m_deleteButton.Text = Local.GetValue("general.delete");
      this.Text = Local.GetValue("filters.title_filters_structure");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as FilterController;
    }

    private void LoadLanguage()
    {
      m_addFilter.Text = Local.GetValue("general.create");
      m_deleteFilter.Text = Local.GetValue("general.delete");
      m_createSubCategory.Text = Local.GetValue("filters.create_sub_category");
      m_renameButton.Text = Local.GetValue("general.rename");
      m_deleteButton.Text = Local.GetValue("general.delete");
      this.Text = Local.GetValue("filters.title_filters_structure");
    }

    private void SuscribeEvents()
    {
      m_tree.KeyDown += OnTreeKeyDown;
      m_addFilter.Click += OnAddCategory;
      m_createSubCategory.Click += OnAddSubCategory;
      m_deleteFilter.Click += OnDeleteCategory;
      m_deleteButton.Click += OnDeleteCategory;
      m_renameButton.Click += OnRenameCategory;
      FilterModel.Instance.ReadEvent += OnModelRead;
      FilterModel.Instance.CreationEvent += OnModelCreate;
      FilterModel.Instance.DeleteEvent += OnModelDelete;
      FilterModel.Instance.UpdateEvent += OnModelUpdate;
      Addin.SuscribeAutoLock(this);
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      FilterModel.Instance.ReadEvent -= OnModelRead;
      FilterModel.Instance.CreationEvent -= OnModelCreate;
      FilterModel.Instance.DeleteEvent -= OnModelDelete;
      FilterModel.Instance.UpdateEvent -= OnModelUpdate;
    }

    #region Utils

    private bool HasChild()
    {
      if (m_tree.SelectedNode == null)
        return (false);
      return (m_tree.SelectedNode.Nodes.Count > 0);
    }

    private bool IsCategorySelected()
    {
      if (m_tree.SelectedNode == null)
      {
        Forms.MsgBox.Show(Local.GetValue("filters.msg_no_category_selected"), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return (false);
      }
      return (true);
    }

    #endregion

    #region FormEvents

    private void OnTreeKeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          this.OnDeleteCategory(null, null);
          break;
        case Keys.Back:
          this.OnDeleteCategory(null, null);
          break;
        case Keys.Space:
          this.OnRenameCategory(null, null);
          break;
      }
    }

    private void OnAddCategory(object sender, EventArgs e)
    {
      string l_filterName;

      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name"), Local.GetValue("filters.new_category")).Trim();
      if (!m_controller.Add(l_filterName, 0, 0, typeof(Filter)))
      {
        Forms.MsgBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void OnAddSubCategory(object sender, EventArgs e)
    {
      string l_filterName;

      if (!this.IsCategorySelected())
        return;
      if (this.HasChild())
      {
        Forms.MsgBox.Show("filters.error.has_child", "filters.new_category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name"), Local.GetValue("filters.new_category")).Trim();
      if (!m_controller.Add(l_filterName, (UInt32)m_tree.SelectedNode.Value, 0, typeof(Filter)))
      {
        Forms.MsgBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void OnDeleteCategory(object sender, EventArgs e)
    {
      DialogResult l_confirm;

      if (!this.IsCategorySelected())
        return;
      l_confirm = MessageBox.Show(Local.GetValue("filters.msg_delete_category"), Local.GetValue("filters.delete_category"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (l_confirm == DialogResult.Yes)
      {
        if (!m_controller.Remove((UInt32)m_tree.SelectedNode.Value, typeof(Filter)))
        {
          Forms.MsgBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.delete_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void OnRenameCategory(object sender, EventArgs e)
    {
      string l_filterName;

      if (!this.IsCategorySelected())
        return;
      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name")).Trim();
      if (!m_controller.Update((UInt32)m_tree.SelectedNode.Value, l_filterName, typeof(Filter)))
      {
        Forms.MsgBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
          Forms.MsgBox.Show("", "filters.new_category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          Forms.MsgBox.Show("", "filters.new_category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    delegate void OnModelDelete_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDelete(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (m_tree.InvokeRequired)
      {
        OnModelDelete_delegate func = new OnModelDelete_delegate(OnModelDelete);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status == Network.ErrorMessage.SUCCESS)
        {
          vTreeNode l_node;

          if ((l_node = m_tree.FindNode(p_id)) != null && l_node.Parent != null)
          {
            l_node.Parent.Nodes.Remove(l_node);
          }
          else if (l_node != null && l_node.Parent == null)
          {
            m_tree.Nodes.Remove(l_node);
          }
          return;
        }
        Forms.MsgBox.Show("{DELETE}", "general.delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, Filter p_attributes);
    void OnModelRead(Network.ErrorMessage p_status, Filter p_attributes)
    {
      if (m_tree.InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        vTreeNode l_node, l_parentNode;

        if (p_status == ErrorMessage.SUCCESS)
        {
          if ((l_node = m_tree.FindNode(p_attributes.Id)) == null) //If the node must be created
          {
            l_node = new vTreeNode();
            l_node.Value = p_attributes.Id;
            l_node.Text = p_attributes.Name;
            if ((l_parentNode = m_tree.FindNode(p_attributes.ParentId)) == null) //If the parent is null, add to the tree (new category).
            {
              m_tree.Nodes.Add(l_node);
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
