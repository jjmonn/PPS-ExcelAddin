﻿using System;
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

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using FBI.Forms;
  using Utils;
  using Network;

  public partial class CurrenciesView : UserControl, IView
  {
    FbiDataGridView m_dgv = new FbiDataGridView();
    CurrenciesController m_controller;
    public CurrenciesView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as CurrenciesController;
    }

    public void LoadView()
    {
      this.dgvLoad();
      this.suscribeEvents();
      this.Controls.Add(m_dgv);
    }

    private void dgvLoad()
    {
      MultiIndexDictionary<uint, string, Currency> l_currencyDic = CurrencyModel.Instance.GetDictionary();

      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 0, "Active");
      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 1, "Name");
      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 2, "Symbol");
      foreach (Currency l_currency in l_currencyDic.Values)
      {
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, l_currency.Id, "");
        m_dgv.FillField(l_currency.Id, 0, l_currency.InUse, new CheckBoxEditor());
        m_dgv.FillField(l_currency.Id, 1, l_currency.Name);
        m_dgv.FillField(l_currency.Id, 2, l_currency.Symbol);
      }
    }

    private void suscribeEvents()
    {
      m_dgv.CellChangedAndValidated += OnDgvCellChangedAndValidated;
      CurrencyModel.Instance.ReadEvent += OnModelRead;
    }

    void OnDgvCellChangedAndValidated(object sender, CellEventArgs args)
    {
      UInt32 l_currencyId = (UInt32)args.Cell.RowItem.ItemValue;
      UInt32 l_columnId = (UInt32)args.Cell.ColumnItem.ItemValue;
      if (l_columnId == 0)
        ChangeInUseValue(l_currencyId, (bool)args.Cell.Value);
      else if (l_columnId == 1)
        ChangeNameValue(l_currencyId, (string)args.Cell.Value);
      else
        ChangeSymbolValue(l_currencyId, (string)args.Cell.Value);
    }

    private void ChangeInUseValue(uint p_currencyId, bool p_newValue)
    {
      Currency l_currency = CurrencyModel.Instance.GetValue(p_currencyId);
      m_controller.UpdateCurrency(l_currency, p_newValue, l_currency.Name, l_currency.Symbol);
    }

    private void ChangeNameValue(uint p_currencyId, string p_newValue)
    {
      Currency l_currency = CurrencyModel.Instance.GetValue(p_currencyId);
      m_controller.UpdateCurrency(l_currency, l_currency.InUse, p_newValue, l_currency.Symbol);
    }

    private void ChangeSymbolValue(uint p_currencyId, string p_newValue)
    {
      Currency l_currency = CurrencyModel.Instance.GetValue(p_currencyId);
      m_controller.UpdateCurrency(l_currency, l_currency.InUse, l_currency.Name, p_newValue);
    }

    private void UpdateCurrencyRow(Currency p_currency)
    {
      m_dgv.FillField(p_currency.Id, 0, p_currency.InUse);
      m_dgv.FillField(p_currency.Id, 1, p_currency.Name);
      m_dgv.FillField(p_currency.Id, 2, p_currency.Symbol);
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, Currency p_attributes);
    void OnModelRead(ErrorMessage p_status, Currency p_attributes)
    {
      if (InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        switch (p_status)
        {
          case ErrorMessage.SUCCESS:
            UpdateCurrencyRow(p_attributes);
            break;
          case ErrorMessage.PERMISSION_DENIED:
            MessageBox.Show(Local.GetValue("general.error.permission_denied"));
            break;
          case ErrorMessage.NOT_FOUND:
            MessageBox.Show(Local.GetValue("general.error.not_found"));
            break;
          default:
            MessageBox.Show(Local.GetValue("general.error.system"));
            break;
        }
      }
    }
  }
}
