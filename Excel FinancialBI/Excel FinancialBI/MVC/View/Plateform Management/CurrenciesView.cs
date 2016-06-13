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

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using FBI.Forms;
  using Utils;
  using Network;
  

  public partial class CurrenciesView : UserControl, IPlatformMgtView
  {
    FbiDataGridView m_dgv = new FbiDataGridView();
    CurrenciesController m_controller;
    CheckBoxEditor m_editor = new CheckBoxEditor();

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
      this.DGVLoad();
      m_dgv.RowsHierarchy.Visible = false;
      m_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);

      this.Controls.Add(m_dgv);
      this.MultilangueSetup();
      SuscribeEvents();
    }
  
    private void MultilangueSetup()
    {
      this.SetMainCurrencyCallBack.Text = Local.GetValue("currencies.set_main_currency");
    }

    private void DGVLoad()
    {
      MultiIndexDictionary<uint, string, Currency> l_currencyDic = CurrencyModel.Instance.GetDictionary();

      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 0, "Active");
      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 1, "Name");
      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 2, "Symbol");
      foreach (Currency l_currency in l_currencyDic.SortedValues)
      {
        m_dgv.SetDimension<Currency>(FbiDataGridView.Dimension.ROW, l_currency.Id, "", 0, null, 0);
        if (UserModel.Instance.CurrentUserHasRight(Group.Permission.EDIT_CURRENCIES) == true)
        {
          if (l_currency.Id != CurrencyModel.Instance.GetMainCurrency())
            m_dgv.FillField(l_currency.Id, 0, l_currency.InUse, m_editor);
          m_dgv.FillField(l_currency.Id, 1, l_currency.Name, new TextBoxEditor());
          m_dgv.FillField(l_currency.Id, 2, l_currency.Symbol, new TextBoxEditor());
        }
        else
        {
          m_dgv.FillField(l_currency.Id, 0, l_currency.InUse);
          m_dgv.FillField(l_currency.Id, 1, l_currency.Name);
          m_dgv.FillField(l_currency.Id, 2, l_currency.Symbol);
        }
      }
    }

    private void SuscribeEvents()
    {
      m_dgv.CellChangedAndValidated += OnDgvCellChangedAndValidated;
      m_editor.CheckedChanged += OnCheckChanged;
      CurrencyModel.Instance.ReadEvent += OnModelRead;
      CurrencyModel.Instance.DeleteEvent += OnModelDelete;
      CurrencyModel.Instance.UpdateEvent += OnModelUpdate;
      Addin.SuscribeAutoLock(this);
    }

    public void CloseView()
    {
      CurrencyModel.Instance.ReadEvent -= OnModelRead;
      CurrencyModel.Instance.DeleteEvent -= OnModelDelete;
      CurrencyModel.Instance.UpdateEvent -= OnModelUpdate;
    }

    delegate void OnModelDelete_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDelete(ErrorMessage p_status, UInt32 p_id)
    {
      switch (p_status)
      {
        case ErrorMessage.SUCCESS:
          m_dgv.DeleteRow(p_id);
          break;
        case ErrorMessage.PERMISSION_DENIED:
          Forms.MsgBox.Show(Local.GetValue("general.error.permission_denied"));
          break;
        case ErrorMessage.NOT_FOUND:
          Forms.MsgBox.Show(Local.GetValue("general.error.not_found"));
          break;
        default:
          Forms.MsgBox.Show(Local.GetValue("general.error.system"));
          break;
      }
    }

    void OnDgvCellChangedAndValidated(object sender, CellEventArgs args)
    {
      UInt32 l_currencyId = (UInt32)args.Cell.RowItem.ItemValue;
      UInt32 l_columnId = (UInt32)args.Cell.ColumnItem.ItemValue;
      if (l_columnId == 1)
        ChangeNameValue(l_currencyId, (string)args.Cell.Value);
      else if (l_columnId == 2)
        ChangeSymbolValue(l_currencyId, (string)args.Cell.Value);
    }

    void OnCheckChanged(object sender, EventArgs args)
    {
      if (m_dgv.HoveredRow == null || m_dgv.HoveredRow.ItemValue == null)
        return;
      UInt32 l_currencyId = (UInt32)m_dgv.HoveredRow.ItemValue;

      ChangeInUseValue(l_currencyId, (bool)m_editor.EditorValue);
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

    private void UpdateOrAddCurrencyRow(Currency p_currency)
    {
      if (m_dgv.Rows.ContainsKey(p_currency.Id) == false)
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, p_currency.Id, "", 0, AxisElemModel.Instance, 0);
      m_dgv.FillField(p_currency.Id, 0, p_currency.InUse);
      m_dgv.FillField(p_currency.Id, 1, p_currency.Name);
      m_dgv.FillField(p_currency.Id, 2, p_currency.Symbol);
      m_editor.EditorValue = p_currency.InUse;
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
            UpdateOrAddCurrencyRow(p_attributes);
            break;
          case ErrorMessage.PERMISSION_DENIED:
            Forms.MsgBox.Show(Local.GetValue("general.error.permission_denied"));
            break;
          case ErrorMessage.NOT_FOUND:
            Forms.MsgBox.Show(Local.GetValue("general.error.not_found"));
            break;
          default:
            Forms.MsgBox.Show(Local.GetValue("general.error.system"));
            break;
        }
      }
    }

    delegate void OnModelUpdate_delegate(ErrorMessage p_status, uint p_id);
    void OnModelUpdate(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnModelUpdate_delegate func = new OnModelUpdate_delegate(OnModelUpdate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        Currency l_currency = CurrencyModel.Instance.GetValue(p_id);

        switch (p_status)
        {
          case ErrorMessage.SUCCESS:
            return;
          case ErrorMessage.PERMISSION_DENIED:
            MessageBox.Show(Local.GetValue("currencies.error.currency_used") + ": " + Error.GetMessage(p_status));
            break;
          default:
            MessageBox.Show(Error.GetMessage(p_status));
            break;
        }
        if (l_currency != null)
          UpdateOrAddCurrencyRow(l_currency);
      }
    }
  }
}
