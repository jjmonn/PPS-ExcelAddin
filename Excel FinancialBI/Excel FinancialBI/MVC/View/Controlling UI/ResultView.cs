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
