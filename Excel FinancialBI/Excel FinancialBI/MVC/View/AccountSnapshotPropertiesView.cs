using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;
using VIBlend.WinForms.Controls;

using DGVDimension = FBI.Forms.BaseFbiDataGridView<uint>.Dimension;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model.CRUD;
  using Model;

  public partial class AccountSnapshotPropertiesView : Form, IView
  {
    enum Column
    {
      NAME,
      FORMULA_TYPE,
      ACCOUNT_TYPE,
      CURRENCY_OPTION,
      CONSOLIDATION_OPTION
    };

    AccountCreateSnapshotController m_controller;
    SafeDictionary<Account.FormulaTypes, ListItem> m_formulaTypeItemDict = new SafeDictionary<Account.FormulaTypes, ListItem>();
    SafeDictionary<Account.AccountType, ListItem> m_accountTypeItemDict = new SafeDictionary<Account.AccountType, ListItem>();
    SafeDictionary<Account.ConversionOptions, ListItem> m_currencyItemDict = new SafeDictionary<Account.ConversionOptions, ListItem>();
    SafeDictionary<Account.ConsolidationOptions, ListItem> m_consoOptionItemDict = new SafeDictionary<Account.ConsolidationOptions, ListItem>();
    ComboBoxEditor m_formulaTypeCB = new ComboBoxEditor();
    ComboBoxEditor m_accountTypeCB = new ComboBoxEditor();
    ComboBoxEditor m_currencyOptionCB = new ComboBoxEditor();
    ComboBoxEditor m_consolidationOptionCB = new ComboBoxEditor();

    public AccountSnapshotPropertiesView()
    {
      InitializeComponent();
      LoadLocals();
      LoadEditors();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AccountCreateSnapshotController;
    }

    void LoadLocals()
    {
      m_validateBT.Text = Local.GetValue("general.export_selected_account");
    }

    void LoadEditors()
    {
      AddListItem(m_formulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_input", Account.FormulaTypes.HARD_VALUE_INPUT);
      AddListItem(m_formulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_formula", Account.FormulaTypes.FORMULA);
      AddListItem(m_formulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_sub", Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
      AddListItem(m_formulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_first", Account.FormulaTypes.FIRST_PERIOD_INPUT);
      AddListItem(m_formulaTypeCB, m_formulaTypeItemDict, "accounts.formula_type_title", Account.FormulaTypes.TITLE);

      AddListItem(m_accountTypeCB, m_accountTypeItemDict, "accounts.type_monetary", Account.AccountType.MONETARY);
      AddListItem(m_accountTypeCB, m_accountTypeItemDict, "accounts.type_number", Account.AccountType.NUMBER);
      AddListItem(m_accountTypeCB, m_accountTypeItemDict, "accounts.type_percentage", Account.AccountType.PERCENTAGE);
      AddListItem(m_accountTypeCB, m_accountTypeItemDict, "accounts.type_date", Account.AccountType.DATE);

      AddListItem(m_currencyOptionCB, m_currencyItemDict, "accounts.currencies_type_average", Account.ConversionOptions.AVERAGE_RATE);
      AddListItem(m_currencyOptionCB, m_currencyItemDict, "accounts.currencies_type_end", Account.ConversionOptions.END_OF_PERIOD_RATE);

      AddListItem(m_consolidationOptionCB, m_consoOptionItemDict, "accounts.consolidation_type_aggregated", Account.ConsolidationOptions.AGGREGATION);
      AddListItem(m_consolidationOptionCB, m_consoOptionItemDict, "accounts.consolidation_type_recomputed", Account.ConsolidationOptions.RECOMPUTATION);
      AddListItem(m_consolidationOptionCB, m_consoOptionItemDict, "accounts.consolidation_type_none", Account.ConsolidationOptions.NONE);
    }

    void AddListItem<T>(ComboBoxEditor p_cb, SafeDictionary<T, ListItem> p_collection, string p_local, T p_value)
    {
      ListItem l_item = new ListItem();
      l_item.Text = Local.GetValue(p_local);
      l_item.Value = p_value;
      p_cb.Items.Add(l_item);
      p_collection[p_value] = l_item;
      p_cb.SelectedIndex = 0;
      p_cb.DropDownList = true;
    }

    public void LoadView(List<Account> p_accounts)
    {
      m_dgv.RowsHierarchy.Visible = false;
      m_dgv.ColumnsHierarchy.Visible = false;
      m_dgv.Dock = DockStyle.Fill;

      m_dgv.ClearColumns();
      m_dgv.ClearRows();
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.NAME, "name");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.FORMULA_TYPE, "formula type");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.ACCOUNT_TYPE, "account type");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.CURRENCY_OPTION, "currency option");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.CONSOLIDATION_OPTION, "consolidation option");

      uint l_index = 0;
      foreach (Account l_account in p_accounts)
      {
        m_dgv.SetDimension(DGVDimension.ROW, l_index, "");
        m_dgv.FillField(l_index, (uint)Column.NAME, l_account.Name);
        m_dgv.FillField(l_index, (uint)Column.FORMULA_TYPE, m_formulaTypeItemDict[l_account.FormulaType], m_formulaTypeCB);
        m_dgv.FillField(l_index, (uint)Column.ACCOUNT_TYPE, m_accountTypeItemDict[l_account.Type], m_accountTypeCB);
        m_dgv.FillField(l_index, (uint)Column.CURRENCY_OPTION, m_currencyItemDict[l_account.ConversionOptionId], m_currencyOptionCB);
        m_dgv.FillField(l_index, (uint)Column.CONSOLIDATION_OPTION, m_consoOptionItemDict[l_account.ConsolidationOptionId], m_consolidationOptionCB);

        m_formulaTypeCB.SelectedItem = m_formulaTypeItemDict[l_account.FormulaType];
        m_accountTypeCB.SelectedItem = m_accountTypeItemDict[l_account.Type];
        m_currencyOptionCB.SelectedItem = m_currencyItemDict[l_account.ConversionOptionId];
        m_consolidationOptionCB.SelectedItem = m_consoOptionItemDict[l_account.ConsolidationOptionId];
        l_index++;
      }

      m_dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
    }

    private void OnValidate(object sender, EventArgs e)
    {
    
    }

  }
}