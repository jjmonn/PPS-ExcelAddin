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
using System.Globalization;
using Microsoft.VisualBasic;

namespace FBI.MVC.View
{
  using Model;
  using Model.CRUD;
  using Forms;
  using Utils;
  using Controller;
  using Network;

  public partial class ExchangeRatesView : FactBaseView<ExchangeRateVersion, ExchangeRatesController>
  {
    public ExchangeRatesView() : base(RatesVersionModel.Instance)
    {
      m_excelImportController = new ExcelRateController();
    }

    public override void SetController(IController p_controller)
    {
      m_controller = p_controller as ExchangeRatesController;
    }

    public override void LoadView()
    {
      base.LoadView();
      m_dgv.ContextMenuStrip = this.m_dgvMenu;
      SuscribeEvents();
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      m_dgv.CellChangedAndValidated += OnCellChanged;
      m_copyValueDown.Click += CopyValueDown;
      ExchangeRateModel.Instance.ReadEvent += OnModelReadRate;
      ExchangeRateModel.Instance.UpdateEvent += OnModelUpdateRate;
      ExchangeRateModel.Instance.CreationEvent += OnModelUpdateRate;
      ExchangeRateModel.Instance.DeleteEvent += OnModelDeleteRate;
    }

    public override void CloseView()
    {
      base.CloseView();
      ExchangeRateModel.Instance.ReadEvent -= OnModelReadRate;
      ExchangeRateModel.Instance.UpdateEvent -= OnModelUpdateRate;
      ExchangeRateModel.Instance.CreationEvent -= OnModelUpdateRate;
      ExchangeRateModel.Instance.DeleteEvent -= OnModelDeleteRate;
    }

    #region Initialize

    void InitPeriods(List<Int32> p_monthList)
    {
      m_dgv.ClearRows();
      foreach (Int32 l_monthId in p_monthList)
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, (UInt32)l_monthId, DateTime.FromOADate(l_monthId).ToString("Y", CultureInfo.CreateSpecificCulture("en-US")));
    }

    void InitCurrencies(SortedSet<UInt32> p_currencies)
    {
      m_dgv.ClearColumns();
      foreach (UInt32 l_currencyId in p_currencies)
      {
        Currency l_currency = CurrencyModel.Instance.GetValue(l_currencyId);

        if (l_currency != null && l_currency.Id != CurrencyModel.Instance.GetMainCurrency())
          m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, l_currency.Id, l_currency.Name);
      }
    }

    void InitRates(UInt32 p_versionId, List<Int32> p_monthList, SortedSet<UInt32> p_currencies)
    {
      TextBoxEditor l_tbEditor = null;
      foreach (Int32 l_monthId in p_monthList)
        foreach (UInt32 l_currencyId in p_currencies)
        {
          ExchangeRate l_rate = ExchangeRateModel.Instance.GetValue(l_currencyId, p_versionId, (UInt32)l_monthId);

          if (UserModel.Instance.CurrentUserHasRight(Group.Permission.EDIT_RATES) == true)
            l_tbEditor = new TextBoxEditor();
          m_dgv.FillField((UInt32)l_monthId, l_currencyId, ((l_rate != null) ? l_rate.Value : 0), l_tbEditor);
        }
    }

    protected override bool DisplayVersion(UInt32 p_versionId)
    {
      List<Int32> l_monthList = RatesVersionModel.Instance.GetMonthsList(p_versionId);
      SortedSet<UInt32> l_currencies = CurrencyModel.Instance.GetUsedCurrencies();

      if (l_monthList == null)
      {
        Forms.MsgBox.Show(Local.GetValue("exchange_rate_version.error.not_found"));
        return false;
      }
      InitPeriods(l_monthList);
      InitCurrencies(l_currencies);
      InitRates(p_versionId, l_monthList, l_currencies);
      m_dgv.Refresh();
      return (true);
    }

    #endregion

    #region User Callback

    void OnCellChanged(object p_sender, CellEventArgs p_args)
    {
      UInt32 l_period = (UInt32)p_args.Cell.RowItem.ItemValue;
      UInt32 l_currencyId = (UInt32)p_args.Cell.ColumnItem.ItemValue;
      UInt32 l_versionId = m_controller.SelectedVersion;

      SetRate(l_currencyId, l_versionId, l_period, Convert.ToDouble((string)p_args.Cell.Value));
    }

    #endregion

    #region Model Callback

    delegate void OnModelReadRate_delegate(ErrorMessage p_status, ExchangeRate p_rate);
    void OnModelReadRate(ErrorMessage p_status, ExchangeRate p_rate)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelReadRate_delegate func = new OnModelReadRate_delegate(OnModelReadRate);
        Invoke(func, p_status, p_rate);
      }
      else
      {
        TextBoxEditor l_tbEditor = null;
        if (UserModel.Instance.CurrentUserHasRight(Group.Permission.EDIT_RATES) == true)
          l_tbEditor = new TextBoxEditor();
        if (p_rate.RateVersionId == m_controller.SelectedVersion)
          m_dgv.FillField(p_rate.Period, p_rate.DestCurrencyId, p_rate.Value, l_tbEditor);
      }
    }

    void OnModelUpdateRate(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        Forms.MsgBox.Show(Error.GetMessage(p_status));
    }

    void OnModelDeleteRate(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        Forms.MsgBox.Show(Error.GetMessage(p_status));
    }

    #endregion

    void CopyValueDown(object p_sender, EventArgs p_e)
    {
      GridCell l_cell = m_dgv.HoveredCell;
      if (l_cell == null)
        return;

      UInt32 l_currencyId = (UInt32)m_dgv.HoveredColumn.ItemValue;
      UInt32 l_versionId = m_controller.SelectedVersion;
      double l_value = Convert.ToDouble(l_cell.Value);
      for (int i = m_dgv.HoveredRow.ItemIndex; i < m_dgv.RowsHierarchy.Items.Count; ++i)
      {
        UInt32 l_period = (UInt32)m_dgv.RowsHierarchy.Items[i].ItemValue;

        SetRate(l_currencyId, l_versionId, l_period, l_value);
      }
    }

    bool SetRate(UInt32 p_currencyId, UInt32 p_versionId, UInt32 p_period, double p_value)
    {
      ExchangeRate l_rate = ExchangeRateModel.Instance.GetValue(p_currencyId, p_versionId, p_period);

      m_dgv.FillField(p_period, p_currencyId, (l_rate == null) ? 0 : l_rate.Value);
      if (l_rate == null)
      {
        l_rate = new ExchangeRate();
        l_rate.Period = p_period;
        l_rate.DestCurrencyId = p_currencyId;
        l_rate.RateVersionId = p_versionId;
        l_rate.Value = p_value;
        return (m_controller.CreateRate(l_rate));
      }
      else
      {
        l_rate = l_rate.Clone();
        l_rate.Value = p_value;
        return (m_controller.UpdateRate(l_rate));
      }
    }

  }
}
