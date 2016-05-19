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

  public partial class AxisOwnedView : AxisBaseView<AxisOwnedController>
  {
    FbiTreeView<AxisElem> m_ownerTV;
    HierarchyItem m_draggingRow;

    public AxisOwnedView()
    {
    }

    public override void LoadView()
    {
      base.LoadView();
      m_ownerTV = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(m_controller.OwnerType), null, true);
      m_ownerTV.ImageList = EntitiesIL;

      vSplitContainer l_splitContainer = new vSplitContainer();
      l_splitContainer.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      TableLayoutPanel1.Controls.Add(l_splitContainer, 0, 1);
      l_splitContainer.Dock = DockStyle.Fill;
      l_splitContainer.SplitterSize = 2;
      l_splitContainer.SplitterDistance = 50;

      l_splitContainer.Panel1.Controls.Add(m_ownerTV);
      l_splitContainer.Panel2.Controls.Add(m_dgv);
      m_ownerTV.Dock = DockStyle.Fill;
      m_dgv.Dock = DockStyle.Fill;
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      m_dgv.MouseDown += OnDGVMouseDown;
      m_ownerTV.NodeMouseDown += OnNodeSelect;
      m_ownerTV.Dropped += OnOwnerTVNodeDropped;
      m_ownerTV.DragOver += OnOwnerTVDragOver;
      AxisElemModel.Instance.CreationEvent += OnModelCreateAxisElem;
      AxisOwnerModel.Instance.ReadEvent += OnModelReadAxisOwner;
      AxisOwnerModel.Instance.CreationEvent += OnModelCreateAxisOwner;
      AxisOwnerModel.Instance.UpdateEvent += OnModelUpdateAxisOwner;
    }

    public override void CloseView()
    {
      base.CloseView();
      AxisElemModel.Instance.CreationEvent -= OnModelCreateAxisElem;
      AxisOwnerModel.Instance.ReadEvent -= OnModelReadAxisOwner;
      AxisOwnerModel.Instance.CreationEvent -= OnModelCreateAxisOwner;
      AxisOwnerModel.Instance.UpdateEvent -= OnModelUpdateAxisOwner;
    }
    
    #region User Callback

    void OnDGVMouseDown(object p_sender, MouseEventArgs p_args)
    {
      HierarchyItem l_row = (m_dgv.HoveredRow != null) ? m_dgv.HoveredRow : m_dgv.HitTestRow(p_args.Location);
      if (l_row == null)
        return;
      if (p_args.Button == MouseButtons.Left)
        if (ModifierKeys.HasFlag(Keys.Control))
        {
          m_draggingRow = l_row;
          m_dgv.DoDragDrop(l_row, DragDropEffects.Move);
        }
    }


    void OnOwnerTVDragOver(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Move;
    }

    void OnOwnerTVNodeDropped(object p_sender, DragEventArgs p_e)
    {
      vTreeNode l_node = m_ownerTV.FindAtPosition(PointToClient(new Point(p_e.X, p_e.Y - 25)));

      if (l_node == null || m_draggingRow == null)
        return;
      UInt32 l_axisId = (UInt32)m_draggingRow.ItemValue;

      m_draggingRow = null;
      AxisElem l_owner = AxisElemModel.Instance.GetValue(m_controller.OwnerType, (UInt32)l_node.Value);

      if (l_owner == null)
        return;

      AxisOwner l_axisOwner = AxisOwnerModel.Instance.GetValue(l_axisId);
      if (l_axisOwner == null)
        return;
      l_axisOwner = l_axisOwner.Clone();
      l_axisOwner.OwnerId = l_owner.Id;
      if (m_controller.UpdateAxisOwner(l_axisOwner) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }

    void OnNodeSelect(object p_sender, vTreeViewMouseEventArgs p_args)
    {
      m_controller.SelectedOwner = (UInt32)p_args.Node.Value;
      DisplayAxis(m_controller.SelectedOwner);
    }

    void DisplayAxis(UInt32 p_ownerId)
    {
      AxisElem l_owner = AxisElemModel.Instance.GetValue(p_ownerId);

      if (l_owner == null || l_owner.AllowEdition == false)
        return;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic = new MultiIndexDictionary<uint, string, AxisElem>();

      foreach (AxisOwner l_axisOwner in AxisOwnerModel.Instance.GetDictionary().Values)
      {
        if (l_axisOwner.OwnerId != p_ownerId)
          continue;
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(l_axisOwner.Id);

        if (l_axisElem == null)
          continue;
        l_axisElemDic.Set(l_axisElem.Id, l_axisElem.Name, l_axisElem);
      }
      LoadDGV(l_axisElemDic, AxisFilterModel.Instance.GetDictionary(m_controller.AxisType).SortedValues);
    }

    #endregion

    #region Model Callback

    void OnModelCreateAxisElem(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
      {
        Forms.MsgBox.Show(Error.GetMessage(p_status));
        return;
      }
      AxisOwner l_axisOwner = new AxisOwner();

      l_axisOwner.Id = p_id;
      l_axisOwner.OwnerId = m_controller.SelectedOwner;
      if (m_controller.CreateAxisOwner(l_axisOwner) == false)
        Forms.MsgBox.Show(m_controller.Error);
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
        if (p_axisOwner.OwnerId != m_controller.SelectedOwner)
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
        Forms.MsgBox.Show(Error.GetMessage(p_status));
    }

    void OnModelUpdateAxisOwner(ErrorMessage p_status, UInt32 p_axisOwnerId)
    {
      if (p_status != ErrorMessage.SUCCESS)
        Forms.MsgBox.Show(Error.GetMessage(p_status));
    }

    #endregion
  }
}
