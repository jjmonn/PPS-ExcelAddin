using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Utils;
  using Model;
  using Controller;
  using Model.CRUD;

  public partial class ExcelImportView : Form, IView
  {
    public enum ViewType
    {
      EXCHANGE_RATE,
      GLOBAL_FACT
    };

    private ViewType m_viewType;

    private List<Int32> m_periods = null;
    private List<double> m_values = null;

    private IExcelImportController m_controller;
    private SafeDictionary<ViewType, Func<bool>> m_locals = new SafeDictionary<ViewType, Func<bool>>();
    private string m_selectionText;

    private delegate bool ValidationFunction<T>(string p_param, ref T p_val);

    public ExcelImportView(ViewType p_type)
    {
      InitializeComponent();
      m_viewType = p_type;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as IExcelImportController;
    }

    public void LoadView()
    {
      try
      {
        m_comboBox.DropDownList = true;
        this.LoadViewTypes();
        this.RegisterEvents();
        m_locals[m_viewType]();
        MultilangueSetup();
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.StackTrace);
      }
    }

    public void Reload()
    {
      m_periods = null;
      m_values = null;
      m_periodsRangeTextBox.Text = "";
      m_valuesRangeTextBox.Text = "";
      this.Show();
    }

    private void MultilangueSetup()
    {
      this.m_periodsLabel.Text = Local.GetValue("upload.periods");
      this.m_importButton.Text = Local.GetValue("upload.upload");
    }

    private void RegisterEvents()
    {
      this.FormClosing += OnClosing;
      m_periodsButton.Click += OnPeriodButtonClick;
      m_valuesButton.Click += OnValButtonClick;
      m_importButton.Click += OnImportClick;
    }

    #region ViewType

    private void LoadViewTypes()
    {
      m_locals[ViewType.EXCHANGE_RATE] = this.SetExchangeRate;
      m_locals[ViewType.GLOBAL_FACT] = this.SetGlobalFact;
    }

    private bool SetExchangeRate()
    {
      this.Text = Local.GetValue("upload.exchange_rates_upload");
      m_descriptionLabel.Text = Local.GetValue("general.currency");
      m_selectionText = Local.GetValue("upload.msg_rates_selection");
      m_valueLabel.Text = Local.GetValue("upload.rates");
      foreach (UInt32 l_currency in CurrencyModel.Instance.GetUsedCurrencies())
      {
        ListItem l_item = new ListItem();
        l_item.Value = l_currency;
        l_item.Text = CurrencyModel.Instance.GetValueName(l_currency);
        m_comboBox.Items.Add(l_item);
      }
      return (true);
    }

    private bool SetGlobalFact()
    {
      this.Text = Local.GetValue("upload.global_facts_upload");
      m_descriptionLabel.Text = Local.GetValue("general.macro_economic_indicator");
      m_selectionText = Local.GetValue("upload.msg_values_selection");
      m_valueLabel.Text = Local.GetValue("upload.values");
      foreach (GlobalFact l_fact in GlobalFactModel.Instance.GetDictionary().Values)
      {
        ListItem l_item = new ListItem();
        l_item.Value = l_fact.Id;
        l_item.Text = l_fact.Name;
        m_comboBox.Items.Add(l_item);
      }
      return (true);
    }

    #endregion

    #region Utils

    private bool GetDateTime<T>(string p_param, ref T p_val) where T : struct
    {
      DateTime l_datetime = new DateTime();
      if (DateTime.TryParse(p_param, out l_datetime))
      {
        if (l_datetime.Day != DateTime.DaysInMonth(l_datetime.Year, l_datetime.Month))
          return (false);
        p_val = (T)(object)((Int32)l_datetime.ToOADate());
        return (true);
      }
      return (false);
    }

    private bool GetDouble<T>(string p_param, ref T p_val) where T : struct
    {
      double l_double;
      if (Double.TryParse(p_param, out l_double))
      {
        p_val = (T)(object)l_double;
        return (true);
      }
      return (false);
    }

    private List<T> GetRangeValue<T>(Range p_range, ValidationFunction<T> p_func) where T : struct
    {
      T l_type = new T();
      List<T> l_list = new List<T>();

      if (p_range.Columns.Count > 1 && p_range.Rows.Count > 1)
        return (null);
      if (p_range.Rows.Count > 1 && p_range.Columns.Count > 1)
        return (null);
      foreach (Range l_range in p_range.Cells)
      {
        if (l_range.Value == null)
          return (null);
        if (p_func(l_range.Value.ToString(), ref l_type))
          l_list.Add(l_type);
        else
          return (null);
      }
      return (l_list);
    }

    private Range ExcelSelection(string p_localName)
    {
      object l_range;
      this.TopMost = false;

      l_range = AddinModule.CurrentInstance.ExcelApp.InputBox(Local.GetValue(p_localName), Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 8);
      if (l_range == null || l_range is Boolean)
        return (null);
      return ((Range)l_range);
    }

    #endregion

    private void OnImportClick(object sender, EventArgs e)
    {
      if (m_comboBox.SelectedItem == null)
      {
        MessageBox.Show(Local.GetValue("upload.no_item_selected"));
        return;
      }
      if (!m_controller.Create((UInt32)m_comboBox.SelectedItem.Value, m_periods, m_values))
      {
        MessageBox.Show(Local.GetValue(m_controller.Error));
        return;
      }
      this.Close();
    }

    private void OnPeriodButtonClick(object sender, EventArgs e)
    {
      Range l_rangePeriods = this.ExcelSelection("upload.msg_periods_selection");

      if (l_rangePeriods != null)
      {
        if ((m_periods = this.GetRangeValue<Int32>(l_rangePeriods, this.GetDateTime)) == null)
          MessageBox.Show(Local.GetValue("upload.incorrect_value"));
        else
          m_periodsRangeTextBox.Text = l_rangePeriods.Address;
      }
    }

    private void OnValButtonClick(object sender, EventArgs e)
    {
      Range l_rangeValues = this.ExcelSelection(m_selectionText);

      if (l_rangeValues != null)
      {
        if ((m_values = this.GetRangeValue<double>(l_rangeValues, this.GetDouble)) == null)
          MessageBox.Show(Local.GetValue("upload.incorrect_value"));
        else
          m_valuesRangeTextBox.Text = l_rangeValues.Address;
      }
    }

    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      this.Exit();
    }

    private void Exit()
    {
      m_comboBox.CloseDropDown();
      this.Hide();
    }

  }
}
