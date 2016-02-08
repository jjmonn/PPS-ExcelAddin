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

  public partial class NewAxisUI : Form, IView
  {
    AxisController m_controller;

    public NewAxisUI()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisController;
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
      if (m_controller.Create(l_newElem) == false)
        MessageBox.Show(m_controller.Error);
      Hide();
    }
  }
}
