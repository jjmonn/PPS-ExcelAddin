using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.DataGridView;
using VIBlend.WinForms.Controls;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Controller;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  using DGV = FBI.Forms.BaseFbiDataGridView<Model.CRUD.ResultKey>;

  class ResultView : AResultView<ResultController>
  {
    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      LogRightClick.Click += OnLogClick;
    }

    void OnLogClick(object p_sender, EventArgs p_e)
    {
      if (m_tabCtrl.SelectedTab == null || m_tabCtrl.SelectedTab.Controls.Count <= 0)
        return;
      DGV l_dgv = m_tabCtrl.SelectedTab.Controls[0] as DGV;
      HierarchyItem l_row = l_dgv.HoveredRow;
      HierarchyItem l_column = l_dgv.HoveredColumn;

      if (l_row == null || l_column == null)
        return;
      ResultKey l_key = l_dgv.HierarchyItems[l_row] + l_dgv.HierarchyItems[l_column];

      if (m_controller.ShowLog((l_key.EntityId == 0)
        ? m_computeConfig.Request.EntityId : l_key.EntityId, l_key.VersionId, l_key.AccountId, (UInt32)l_key.Period) == false)
        MessageBox.Show(m_controller.Error);
    }

    public void SetVersionVisible(UInt32 p_versionId, bool p_visible)
    {
      foreach (vTabPage l_tab in m_tabCtrl.TabPages)
      {
        if (l_tab.Controls.Count > 0)
        {
          DGV l_dgv = l_tab.Controls[0] as DGV;
          SetHierachyItemVisible(p_versionId, p_visible, l_dgv.Rows);
          SetHierachyItemVisible(p_versionId, p_visible, l_dgv.Columns);
          l_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
        }
      }
    }

  }
}
