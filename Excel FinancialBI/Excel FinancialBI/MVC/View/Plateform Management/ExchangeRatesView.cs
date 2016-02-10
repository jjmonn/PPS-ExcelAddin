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

  public partial class ExchangeRatesView : FactBaseView<ExchangeRateVersion>
  {
    ExchangeRatesController m_controller;

    public ExchangeRatesView() : base(RatesVersionModel.Instance)
    {
    }

    override public void SetController(IController p_controller)
    {
      m_controller = p_controller as ExchangeRatesController;
    }

    public override void LoadView()
    {
      base.LoadView();
      m_dgv.ContextMenuStrip = this.m_exchangeRatesRightClickMenu;
      SuscribeEvents();
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      m_versionTV.NodeMouseDown += OnNodeSelect;
      m_renameBT.Click += OnRenameVersionClick;
      m_addFolderRCM.Click += OnCreateFolderClick;
      m_dgv.CellChangedAndValidated += OnCellChanged;
      CopyRateDownToolStripMenuItem.Click += CopyValueDown;
      m_addRatesVersionRCM.Click += OnCreateVersionClick;
      m_deleteVersionRCM.Click += OnDeleteVersionClick;
      ExchangeRateModel.Instance.ReadEvent += OnModelReadRate;
      ExchangeRateModel.Instance.UpdateEvent += OnModelUpdateRate;
      ExchangeRateModel.Instance.CreationEvent += OnModelUpdateRate;
      ExchangeRateModel.Instance.DeleteEvent += OnModelDeleteRate;
      RatesVersionModel.Instance.ReadEvent += OnModelReadVersion;
      RatesVersionModel.Instance.UpdateEvent += OnModelUpdateVersion;
      RatesVersionModel.Instance.CreationEvent += OnModelCreateVersion;
      RatesVersionModel.Instance.DeleteEvent += OnModelDeleteVersion;
    }

    #region Initialize

    void InitPeriods(Int32[] p_monthList)
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

          m_dgv.FillField((UInt32)l_monthId, l_currencyId, ((l_rate != null) ? l_rate.Value : 0), new TextBoxEditor());
        }
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
      m_dgv.Refresh();
    }

    #endregion

    #region User Callback

    void OnNodeSelect(object p_sender, vTreeViewMouseEventArgs p_event)
    {
      m_controller.SelectedVersion = (UInt32)p_event.Node.Value;

      ExchangeRateVersion l_version = RatesVersionModel.Instance.GetValue(m_controller.SelectedVersion);

      if (l_version == null || l_version.IsFolder)
        m_dgv.Clear();
      else
        DisplayVersion(l_version.Id);
    }

    void OnCellChanged(object p_sender, CellEventArgs p_args)
    {
      UInt32 l_period = (UInt32)p_args.Cell.RowItem.ItemValue;
      UInt32 l_currencyId = (UInt32)p_args.Cell.ColumnItem.ItemValue;
      UInt32 l_versionId = m_controller.SelectedVersion;

      SetRate(l_currencyId, l_versionId, l_period, double.Parse((string)p_args.Cell.Value));
    }

    void OnCreateVersionClick(object p_sender, EventArgs p_args)
    {
      m_controller.ShowNewVersionUI();
    }

    void OnCreateFolderClick(object p_sender, EventArgs p_args)
    {
      string l_result = Interaction.InputBox(Local.GetValue("rate_version.create_folder"));

      if (l_result == "")
        return;
      ExchangeRateVersion l_version = new ExchangeRateVersion();

      l_version.ParentId = m_controller.SelectedVersion;
      l_version.IsFolder = true;
      l_version.Name = l_result;
      if (m_controller.CreateVersion(l_version) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnRenameVersionClick(object p_sender, EventArgs p_args)
    {
      string l_result = Interaction.InputBox(Local.GetValue("rate_version.rename"));

      if (l_result == "")
        return;
      ExchangeRateVersion l_version = RatesVersionModel.Instance.GetValue(m_controller.SelectedVersion);
      if (l_version == null)
        return;
      l_version = l_version.Clone();
      l_version.Name = l_result;
      if (m_controller.UpdateVersion(l_version) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnDeleteVersionClick(object p_sender, EventArgs p_args)
    {
      m_controller.DeleteVersion(m_controller.SelectedVersion);
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
        if (p_rate.RateVersionId == m_controller.SelectedVersion)
          m_dgv.FillField(p_rate.Period, p_rate.DestCurrencyId, p_rate.Value, new TextBoxEditor());
      }
    }

    void OnModelUpdateRate(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnModelDeleteRate(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnModelReadVersion(ErrorMessage p_status, ExchangeRateVersion p_version)
    {
      m_versionTV.FindAndAdd(p_version);
    }

    void OnModelUpdateVersion(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    void OnModelCreateVersion(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    delegate void OnModelDeleteVersion_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDeleteVersion(ErrorMessage p_status, UInt32 p_id)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelDeleteVersion_delegate func = new OnModelDeleteVersion_delegate(OnModelDeleteVersion);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
          MessageBox.Show(Error.GetMessage(p_status));
        else
        {
          m_versionTV.FindAndRemove(p_id);
          m_versionTV.Refresh();
        }
      }
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
