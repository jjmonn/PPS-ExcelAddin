using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Model;
  using Forms;
  using Utils;
  using Controller;
  using Model.CRUD;
  using Microsoft.VisualBasic;

  public partial class AxisFilterStructView : Form, IView
  {
    private IController m_controller;
    private AxisType m_axisType;

    private FbiTreeView<Filter> m_tree;

    public AxisFilterStructView()
    {
      InitializeComponent();
      try
      {
        m_tree = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(m_axisType));
      }
      catch
      {
        //TODO
      }
      m_filterPanel.Controls.Add(m_tree);
      this.LoadLanguage();
      this.RegisterEvents();
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
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller;
    }

    private void m_tree_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          this.m_addFilter_Click(sender, e);
          break;
      }
    }

    private void m_addFilter_Click(object sender, EventArgs e)
    {
      string l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_category_name"), Local.GetValue("filters.new_category"));
      if (l_filterName.Trim() == "")
      {
        //FilterModel.Instance.Create();
      }
    }

    private void m_deleteFilter_Click(object sender, EventArgs e)
    {
      DialogResult l_confirm;

      if (m_tree.SelectedNode == null)
        return;
      l_confirm = MessageBox.Show(Local.GetValue(""), Local.GetValue(""), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
      if (l_confirm == DialogResult.Yes)
      {
        //m_controller.DeleteFilter(m_tree.SelectedNode.Value);
      }
    }

    private void m_filterPanel_Click(object sender, EventArgs e)
    {
      if (m_tree.SelectedNode == null)
        return;
      m_tree.SelectedNode = null;
    }
  }
}
