using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Model;
  using Model.CRUD;
  using Forms;

  public partial class ExchangeRatesView : FactBaseView<ExchangeRateVersion>
  {
    public ExchangeRatesView() : base(RatesVersionModel.Instance)
    {
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      m_versionTV.NodeMouseDown += OnNodeSelect;
    }

    void OnNodeSelect(object p_sender, vTreeViewMouseEventArgs p_event)
    {
      // iterate through periods
      m_dgv.SetDimension(FbiDataGridView<string>.Dimension.COLUMN, 0, "");
    }
  }
}
