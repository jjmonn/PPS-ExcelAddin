using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Forms;
  using FBI;
  using Utils;

  public partial class NewAxisUI<TController> : Form, IView where TController : class, IAxisController
  {
    TController m_controller;

    public NewAxisUI()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as TController;
    }

    public void LoadView()
    {
      switch (m_controller.AxisType)
      {
        case AxisType.Entities:
          FbiTreeView<AxisElem>.Load(m_parentAxisElemTreeviewBox.TreeView.Nodes, AxisElemModel.Instance.GetDictionary(m_controller.AxisType));
          break;
        default:
          m_parentAxisLabel.Visible = false;
          m_parentAxisElemTreeviewBox.Visible = false;
          break;
      }
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {

      this.m_nameLabel.Text = Local.GetValue("general.name");
      this.CancelBT.Text = Local.GetValue("general.cancel");
      this.CreateAxisBT.Text = Local.GetValue("general.create");

      switch (m_controller.AxisType)
      {
        case AxisType.Entities:
          this.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_entity");
          this.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_entity_selection");
          this.Text = Local.GetValue("axis_edition.new_entity");
          break;
        case AxisType.Client:
          this.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_client");
          this.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_client_selection");
          this.Text = Local.GetValue("axis_edition.new_client");
          break;
        case AxisType.Product:
          this.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_product");
          this.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_product_selection");
          this.Text = Local.GetValue("axis_edition.new_product");
          break;
        case AxisType.Adjustment:
          this.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_adjustment");
          this.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_adjustment_selection");
          this.Text = Local.GetValue("axis_edition.new_adjustment");
          break;
        case AxisType.Employee:
          this.m_parentAxisLabel.Text = Local.GetValue("axis_edition.parent_employee");
          this.m_parentAxisElemTreeviewBox.Text = Local.GetValue("axis_edition.parent_employee_selection");
          this.Text = Local.GetValue("axis_edition.new_employee");
          break;
      }
    }

    public UInt32 ParentAxisElemId
    {
      set
      {
        vTreeNode parentAxisNode =  FbiTreeView<AxisElem>.FindNode(m_parentAxisElemTreeviewBox.TreeView, value);
        if (parentAxisNode == null)
          return;
        m_parentAxisElemTreeviewBox.TreeView.SelectedNode = parentAxisNode;
      }
    }

    private void CancelBT_Click(object p_sender, EventArgs p_e)
    {
      Hide();
    }

    private void CreateAxisBT_Click(object p_sender, EventArgs p_e)
    {
      UInt32 l_parentAxisId = 0;
      if ((m_parentAxisElemTreeviewBox.TreeView.SelectedNode != null))
        if (m_controller.AxisType == AxisType.Entities || m_controller.AxisType == AxisType.Client)
          l_parentAxisId = (UInt32)m_parentAxisElemTreeviewBox.TreeView.SelectedNode.Value;
      AxisElem l_newElem = new AxisElem();

      l_newElem.Name = m_nameTextBox.Text;
      l_newElem.Axis = m_controller.AxisType;
      l_newElem.ParentId = l_parentAxisId;
      l_newElem.AllowEdition = true;
      if (m_controller.CreateAxisElem(l_newElem) == false)
        MessageBox.Show(m_controller.Error);
      Hide();
    }
  }
}
