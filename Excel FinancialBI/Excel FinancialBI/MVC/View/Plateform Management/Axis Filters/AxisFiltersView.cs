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

  public partial class AxisFiltersView : UserControl, IView
  {
    private AxisFilterController m_controller;
    private AxisType m_axisType;

    private FbiTreeView<FilterValue> m_tree;

    public AxisFiltersView(AxisType p_axisType)
    {
      InitializeComponent();
      try
      {
        m_axisType = p_axisType;
        m_tree = new FbiTreeView<FilterValue>();
        foreach (MultiIndexDictionary<UInt32, string, FilterValue> l_dic in FilterValueModel.Instance.GetDictionary().Values)
        {
          if (!m_tree.Append(l_dic))
          {
            throw new Exception("[FbiTreeView] Cannot append dictionnary. Either the dictionnary is null or incorrect");
          }
        }
        m_tree.ContextMenuStrip = m_contextRightClick;
        m_tree.Dock = DockStyle.Fill;
        m_valuePanel.Controls.Add(m_tree, 0, 1);
        this.RegisterEvents();
        this.LoadLanguage();
      }
      catch (Exception e)
      {
        MessageBox.Show(Local.GetValue("CUI.msg_error_system"), Local.GetValue("filters.categories"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Debug.WriteLine(e.Message);
      }
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisFilterController;
    }

    public AxisType AxisType
    {
      get { return (m_axisType); }
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
      m_addValue.Click += m_addValue_Click;
      m_addValueRightClick.Click += m_addValue_Click;
      m_delete.Click += m_delete_Click;
      m_deleteRightClick.Click += m_delete_Click;
      m_rename.Click += m_rename_Click;
      m_renameRightClick.Click += m_rename_Click;
      m_editStruct.Click += m_editStruct_Click;
      m_valuePanel.Click += m_valuePanel_Click;
    }

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

    private void m_addValue_Click(object sender, EventArgs e)
    {
      string l_filterName;

      //You can only add a value if you selected a category !
      if (m_tree.SelectedNode == null || this.IsValue())
      {
        MessageBox.Show(Local.GetValue("filters.msg_no_category_selected"), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      l_filterName = Interaction.InputBox(Local.GetValue("filters.msg_new_value_name")).Trim();
      if (!m_controller.Add(l_filterName, (UInt32)FbiTreeView<FilterValue>.GetRoot(m_tree.SelectedNode).Value, (UInt32)m_tree.SelectedNode.Parent.Value))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error), Local.GetValue("filters.new_value"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      //UPDATE THE TREEVIEW
    }

    private void m_delete_Click(object sender, EventArgs e)
    {
      if (this.IsCategory() && this.AskConfirmation("filters.msg_delete_category", "filters.delete_category"))
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value);
        //UPDATE TREEVIEW!!!
      }
      else if (this.IsValue() && this.AskConfirmation("filters.msg_delete_value", "filters.delete_value"))
      {
        m_controller.Remove((UInt32)m_tree.SelectedNode.Value);
        //UPDATE TREEVIEW!!!
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
      //UPDATE TREEVIEW!!!
    }

    private void m_editStruct_Click(object sender, EventArgs e)
    {
      //Open AxisFiltersStructController
    }

    private void m_valuePanel_Click(object sender, EventArgs e)
    {
      m_tree.SelectedNode = null;
    }
  }
}
