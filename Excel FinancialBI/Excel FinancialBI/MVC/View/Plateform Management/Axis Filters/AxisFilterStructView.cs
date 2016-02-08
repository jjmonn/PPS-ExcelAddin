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

namespace FBI.MVC.View
{
  using Model;
  using Forms;
  using Utils;
  using Controller;
  using Model.CRUD;

  public partial class AxisFilterStructView : Form, IView
  {
    private AxisFiltersStructController m_controller;
    private AxisType m_axisType;

    private FbiTreeView<Filter> m_tree;

    public AxisFilterStructView(AxisType p_axisType)
    {
      InitializeComponent();
      try
      {
        m_axisType = p_axisType;
        m_tree = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(m_axisType));
        m_tree.ContextMenuStrip = m_structureTreeviewRightClickMenu;
        m_filterPanel.Controls.Add(m_tree);
        this.LoadLanguage();
        this.RegisterEvents();
      }
      catch (Exception e)
      {
        MessageBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message);
      }
    }

    public AxisType AxisType
    {
      get { return (m_axisType); }
    }

    //TODO Drag and drop

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisFiltersStructController;
    }

    private void LoadLanguage()
    {
      m_addFilter.Text = Local.GetValue("general.create");
      m_deleteFilter.Text = Local.GetValue("general.delete");
      m_createCategoryUnderCurrentCategoryButton.Text = Local.GetValue("general.create_category_under_category");
      m_renameButton.Text = Local.GetValue("general.renamme");
      m_deleteButton.Text = Local.GetValue("general.delete");
      this.Text = Local.GetValue("filters.title_filters_structure");
    }

    private void RegisterEvents()
    {
      m_tree.KeyDown += m_tree_KeyDown;
      m_deleteFilter.Click += m_deleteFilter_Click;
      m_filterPanel.Click += m_filterPanel_Click;
      m_renameButton.Click += m_renameButton_Click;
      m_createCategoryUnderCurrentCategoryButton.Click += m_createCategoryUnderCurrentCategoryButton_Click;
    }

    private void m_tree_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          this.m_addFilter_Click(sender, e);
          break;
        //ECHAP, REMOVE SELECTED ELEM
      }
    }

    private void m_addFilter_Click(object sender, EventArgs e)
    {
      string l_filterName;

      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name"), Local.GetValue("filters.new_category")).Trim();
      if (!m_controller.Add(l_filterName, 0))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void m_createCategoryUnderCurrentCategoryButton_Click(object sender, EventArgs e)
    {
      string l_filterName;

      if (m_tree.SelectedNode == null)
      {
        MessageBox.Show(Local.GetValue("filters.msg_no_category_selected"), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name"), Local.GetValue("filters.new_category")).Trim();
      if (!m_controller.Add(l_filterName, (UInt32)m_tree.SelectedNode.Value))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void m_deleteFilter_Click(object sender, EventArgs e)
    {
      DialogResult l_confirm;

      if (m_tree.SelectedNode == null)
        return;
      l_confirm = MessageBox.Show(Local.GetValue("filters.msg_delete_category"), Local.GetValue("filters.delete_category"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (l_confirm == DialogResult.Yes)
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value);
      }
    }

    private void m_filterPanel_Click(object sender, EventArgs e)
    {
      m_tree.SelectedNode = null;
    }

    private void m_renameButton_Click(object sender, EventArgs e)
    {
      string l_filterName;

      if (m_tree.SelectedNode == null)
        return;
      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name")).Trim();
      if (!m_controller.Update((UInt32)m_tree.SelectedNode.Value, l_filterName))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_category"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
  }
}
