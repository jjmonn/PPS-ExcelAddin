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
using VIBlend.WinForms.DataGridView;
using FBI.Forms;

namespace FBI.MVC.View
{
  using Model;
  using Model.CRUD;
  using Controller;
  using Utils;
  using Network;

  public partial class EmployeeView : AxisBaseView<EmployeeController>
  {
    FbiTreeView<AxisElem> m_entitiesTV;
    HierarchyItem m_draggingRow;

    public EmployeeView()
    {

    }

    public override void LoadView()
    {
      base.LoadView();
      m_entitiesTV = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities), null, true);
      m_entitiesTV.ImageList = EntitiesIL;

      vSplitContainer l_splitContainer = new vSplitContainer();
      l_splitContainer.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      TableLayoutPanel1.Controls.Add(l_splitContainer, 0, 1);
      l_splitContainer.Dock = DockStyle.Fill;
      l_splitContainer.SplitterSize = 2;
      l_splitContainer.SplitterDistance = 50;

      l_splitContainer.Panel1.Controls.Add(m_entitiesTV);
      l_splitContainer.Panel2.Controls.Add(m_dgv);
      m_entitiesTV.Dock = DockStyle.Fill;
      m_dgv.Dock = DockStyle.Fill;
      SuscribeEvents();
    }

    public void SuscribeEvents()
    {
      m_dgv.MouseDown += OnDGVMouseDown;
      m_entitiesTV.NodeMouseDown += OnNodeSelect;
      m_entitiesTV.Dropped += OnTVNodeDropped;
      AxisElemModel.Instance.CreationEvent += OnModelCreateAxisElem;
      AxisOwnerModel.Instance.ReadEvent += OnModelReadAxisOwner;
      AxisOwnerModel.Instance.CreationEvent += OnModelCreateAxisOwner;
      AxisOwnerModel.Instance.UpdateEvent += OnModelUpdateAxisOwner;
    }
    
    #region User Callback

    void OnDGVMouseDown(object p_sender, MouseEventArgs p_args)
    {
      if (m_dgv.HoveredRow == null)
        return;
      if (p_args.Button == MouseButtons.Left)
        if (ModifierKeys.HasFlag(Keys.Control))
        {
          m_dgv.DoDragDrop(m_dgv.HoveredRow, DragDropEffects.Move);
          m_draggingRow = m_dgv.HoveredRow;
        }
    }

    void OnTVNodeDropped(object p_sender, DragEventArgs p_e)
    {
      vTreeNode l_node = m_entitiesTV.FindAtPosition(new Point(p_e.X, p_e.Y));

      if (l_node == null || m_draggingRow == null)
        return;
      UInt32 l_employeeId = (UInt32)m_draggingRow.ItemValue;

      m_draggingRow = null;
      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)l_node.Value);

      if (l_entity == null)
        return;

      AxisOwner l_axisOwner = AxisOwnerModel.Instance.GetValue(l_employeeId);
      if (l_axisOwner == null)
        return;
      l_axisOwner = l_axisOwner.Clone();
      l_axisOwner.OwnerId = l_entity.Id;
      if (m_controller.UpdateAxisOwner(l_axisOwner) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnNodeSelect(object p_sender, vTreeViewMouseEventArgs p_args)
    {
      m_controller.SelectedEntity = (UInt32)p_args.Node.Value;
      DisplayEmployees(m_controller.SelectedEntity);
    }

    void DisplayEmployees(UInt32 p_entityId)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(p_entityId);

      if (l_entity == null || l_entity.AllowEdition == false)
        return;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic = new MultiIndexDictionary<uint, string, AxisElem>();

      foreach (AxisOwner l_axisOwner in AxisOwnerModel.Instance.GetDictionary().Values)
      {
        if (l_axisOwner.OwnerId != p_entityId)
          continue;
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(l_axisOwner.Id);

        if (l_axisElem == null)
          continue;
        l_axisElemDic.Set(l_axisElem.Id, l_axisElem.Name, l_axisElem);
      }
      LoadDGV(l_axisElemDic, AxisFilterModel.Instance.GetDictionary(AxisType.Employee).SortedValues);
    }

    #endregion

    #region Model Callback

    void OnModelCreateAxisElem(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Error.GetMessage(p_status));
        return;
      }
      AxisOwner l_axisOwner = new AxisOwner();

      l_axisOwner.Id = p_id;
      l_axisOwner.OwnerId = m_controller.SelectedEntity;
      if (m_controller.CreateAxisOwner(l_axisOwner) == false)
        MessageBox.Show(m_controller.Error);
    }

    delegate void OnModelReadAxisOwner_delegate(ErrorMessage p_status, AxisOwner p_axisOwner);
    void OnModelReadAxisOwner(ErrorMessage p_status, AxisOwner p_axisOwner)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelReadAxisOwner_delegate func = new OnModelReadAxisOwner_delegate(OnModelReadAxisOwner);
        Invoke(func, p_status, p_axisOwner);
      }
      else
      {
        if (p_axisOwner.OwnerId != m_controller.SelectedEntity)
        {
          m_dgv.DeleteRow(p_axisOwner.Id);
          m_dgv.Refresh();
          return;
        }
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(p_axisOwner.Id);

        if (l_axisElem == null)
          return;
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, l_axisElem.Id, l_axisElem.Name);
      }
    }

    void OnModelCreateAxisOwner(ErrorMessage p_status, UInt32 p_axisOwnerId)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnModelUpdateAxisOwner(ErrorMessage p_status, UInt32 p_axisOwnerId)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    #endregion
  }
}
