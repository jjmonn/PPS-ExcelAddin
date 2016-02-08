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
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.View
{
  using Model;
  using Model.CRUD;
  using Forms;
  using Utils;
  using Controller;

  public partial class ExchangeRatesView : FactBaseView<ExchangeRateVersion>
  {
    ExchangeRatesController m_controller;

    public ExchangeRatesView() : base(RatesVersionModel.Instance)
    {
      SuscribeEvents();
    }

    override public void SetController(IController p_controller)
    {
      m_controller = p_controller as ExchangeRatesController;
    }

    void SuscribeEvents()
    {
      m_versionTV.NodeMouseDown += OnNodeSelect;
    }

    void OnNodeSelect(object p_sender, vTreeViewMouseEventArgs p_event)
    {
      DisplayVersion((UInt32)p_event.Node.Value);
    }

    void DisplayVersion(UInt32 p_versionId)
    {
      Int32[] l_monthList = RatesVersionModel.Instance.GetMonthsList(p_versionId);
      SortedSet<UInt32> l_currencies = CurrencyModel.Instance.GetUsedCurrencies();

      if (l_monthList == null)
      {
        MessageBox.Show(Local.GetValue("exchange_rate_version.error.not_found"));
        return;
      }
      InitPeriods(l_monthList);
      InitCurrencies(l_currencies);
      InitRates(p_versionId, l_monthList, l_currencies);
    }

    void InitPeriods(Int32[] p_monthList)
    {
      m_dgv.RowsHierarchy.Clear();
      foreach (Int32 l_monthId in p_monthList)
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, (UInt32)l_monthId, DateTime.FromOADate(l_monthId).ToShortDateString());
    }

    void InitCurrencies(SortedSet<UInt32> p_currencies)
    {
      m_dgv.ColumnsHierarchy.Clear();
      foreach (UInt32 l_currencyId in p_currencies)
      {
        Currency l_currency = CurrencyModel.Instance.GetValue(l_currencyId);

        if (l_currency != null)
          m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)l_currency.Id, l_currency.Name);
      }
    }

    void InitRates(UInt32 p_versionId, Int32[] p_monthList, SortedSet<UInt32> p_currencies)
    {
      foreach (Int32 l_monthId in p_monthList)
        foreach (UInt32 l_currencyId in p_currencies)
        {
          ExchangeRate l_rate = ExchangeRateModel.Instance.GetValue(l_currencyId, p_versionId, (UInt32)l_monthId);

          if (l_rate != null)
            m_dgv.FillField((UInt32)l_monthId, l_currencyId, ((l_rate != null) ? l_rate.Value : 0), new TextBoxEditor());
        }
    }
  }
}
