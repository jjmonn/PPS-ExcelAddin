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
using VIBlend.Utilities;

using DGVDimension = FBI.Forms.BaseFbiDataGridView<uint>.Dimension;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model.CRUD;
  using Model;
  using Forms;

  public partial class AccountSnapshotPropertiesView : Form, IView
  {
    enum Column
    {
      NAME,
      PARENT,
      FORMULA_TYPE,
      ACCOUNT_TYPE,
      CURRENCY_OPTION,
      CONSOLIDATION_OPTION,
    };

    AccountCreateSnapshotController m_controller;
    SafeDictionary<Account.FormulaTypes, ListItem> m_formulaTypeItemDict = new SafeDictionary<Account.FormulaTypes, ListItem>();
    SafeDictionary<Account.AccountType, ListItem> m_accountTypeItemDict = new SafeDictionary<Account.AccountType, ListItem>();
    SafeDictionary<Account.ConversionOptions, ListItem> m_currencyItemDict = new SafeDictionary<Account.ConversionOptions, ListItem>();
    SafeDictionary<Account.ConsolidationOptions, ListItem> m_consoOptionItemDict = new SafeDictionary<Account.ConsolidationOptions, ListItem>();
    SafeDictionary<string, Account> m_accountlist;
    TextBoxEditor m_textBoxEditor;
    GridCell m_selectedCell = null;
    bool m_checkingAccount = false;

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
      m_textBoxEditor = new TextBoxEditor();
      m_accountTV.CheckBoxes = true;
      m_accountTV.ImageList = accountsIL;
      AddListItem(m_formulaTypeItemDict, "accounts.formula_type_input", Account.FormulaTypes.HARD_VALUE_INPUT);
      AddListItem(m_formulaTypeItemDict, "accounts.formula_type_formula", Account.FormulaTypes.FORMULA);
      AddListItem(m_formulaTypeItemDict, "accounts.formula_type_sub", Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
      AddListItem(m_formulaTypeItemDict, "accounts.formula_type_first", Account.FormulaTypes.FIRST_PERIOD_INPUT);
      AddListItem(m_formulaTypeItemDict, "accounts.formula_type_title", Account.FormulaTypes.TITLE);

      AddListItem(m_accountTypeItemDict, "accounts.type_monetary", Account.AccountType.MONETARY);
      AddListItem(m_accountTypeItemDict, "accounts.type_number", Account.AccountType.NUMBER);
      AddListItem(m_accountTypeItemDict, "accounts.type_percentage", Account.AccountType.PERCENTAGE);
      AddListItem(m_accountTypeItemDict, "accounts.type_date", Account.AccountType.DATE);

      AddListItem(m_currencyItemDict, "accounts.currencies_type_average", Account.ConversionOptions.AVERAGE_RATE);
      AddListItem(m_currencyItemDict, "accounts.currencies_type_end", Account.ConversionOptions.END_OF_PERIOD_RATE);

      AddListItem(m_consoOptionItemDict, "accounts.consolidation_type_aggregated", Account.ConsolidationOptions.AGGREGATION);
      AddListItem(m_consoOptionItemDict, "accounts.consolidation_type_recomputed", Account.ConsolidationOptions.RECOMPUTATION);
      AddListItem(m_consoOptionItemDict, "accounts.consolidation_type_none", Account.ConsolidationOptions.NONE);
    }

    ComboBoxEditor GetCB(List<ListItem> p_listItems)
    {
      ComboBoxEditor l_cb = new ComboBoxEditor();

      foreach (ListItem l_item in p_listItems)
        l_cb.Items.Add(l_item);
      l_cb.SelectedIndex = 0;
      l_cb.DropDownList = true;
      return (l_cb);
    }

    void AddListItem<T>(SafeDictionary<T, ListItem> p_collection, string p_local, T p_value)
    {
      ListItem l_item = new ListItem();
      l_item.Text = Local.GetValue(p_local);
      l_item.Value = p_value;
      p_collection[p_value] = l_item;
    }

    public void LoadView(SafeDictionary<string, Account> p_accounts)
    {
      m_accountlist = p_accounts;
      m_dgv.RowsHierarchy.Visible = false;
      m_dgv.ColumnsHierarchy.Visible = false;
      m_dgv.Dock = DockStyle.Fill;

      m_dgv.ClearColumns();
      m_dgv.ClearRows();
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.NAME, "name");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.PARENT, "parent");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.FORMULA_TYPE, "formula type");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.ACCOUNT_TYPE, "account type");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.CURRENCY_OPTION, "currency option");
      m_dgv.SetDimension(DGVDimension.COLUMN, (uint)Column.CONSOLIDATION_OPTION, "consolidation option");

      uint l_index = 0;
      foreach (Account l_account in p_accounts.Values)
      {
        ComboBoxEditor l_formulaTypeCB = GetCB(m_formulaTypeItemDict.Values.ToList());
        ComboBoxEditor l_accountTypeCB = GetCB(m_accountTypeItemDict.Values.ToList());
        ComboBoxEditor l_currencyOptionCB = GetCB(m_currencyItemDict.Values.ToList());
        ComboBoxEditor l_consolidationOptionCB = GetCB(m_consoOptionItemDict.Values.ToList()); 
        
        
        m_dgv.SetDimension(DGVDimension.ROW, l_index, "");
        m_dgv.FillField(l_index, (uint)Column.NAME, l_account.Name, m_textBoxEditor);
        m_dgv.FillField(l_index, (uint)Column.FORMULA_TYPE, m_formulaTypeItemDict[l_account.FormulaType], l_formulaTypeCB);
        m_dgv.FillField(l_index, (uint)Column.PARENT, m_accountTV.FirstVisibleNode.Text);
        m_dgv.FillField(l_index, (uint)Column.ACCOUNT_TYPE, m_accountTypeItemDict[l_account.Type], l_accountTypeCB);
        m_dgv.FillField(l_index, (uint)Column.CURRENCY_OPTION, m_currencyItemDict[l_account.ConversionOptionId], l_currencyOptionCB);
        m_dgv.FillField(l_index, (uint)Column.CONSOLIDATION_OPTION, m_consoOptionItemDict[l_account.ConsolidationOptionId], l_consolidationOptionCB);

        l_formulaTypeCB.SelectedItem = m_formulaTypeItemDict[l_account.FormulaType];
        l_accountTypeCB.SelectedItem = m_accountTypeItemDict[l_account.Type];
        l_currencyOptionCB.SelectedItem = m_currencyItemDict[l_account.ConversionOptionId];
        l_consolidationOptionCB.SelectedItem = m_consoOptionItemDict[l_account.ConsolidationOptionId];
        l_index++;
      }

      m_dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);

      SubscribeEvents();

      DisplayParents(false);
    }

    void SubscribeEvents()
    {
      m_accountTV.NodeChecked += OnAccountChecked;
      m_dgv.CellValidating += OnCellValidating;
      m_dgv.CellMouseDoubleClick += OnCellDoubleClick;
      m_dgv.MouseClick += OnDGVMouseClick;
      AccountModel.Instance.UpdateListEvent += OnUpdateList;
    }

    void OnDGVMouseClick(object sender, MouseEventArgs e)
    {
      DisplayParents(false);
    }

    public void CloseView()
    {
      AccountModel.Instance.UpdateListEvent -= OnUpdateList;
    }

    void OnAccountChecked(object sender, vTreeViewEventArgs p_e)
    {
      if (m_checkingAccount)
        return;
      m_checkingAccount = true;
      foreach (vTreeNode l_node in p_e.Node.TreeView.GetNodes())
        l_node.Checked = CheckState.Unchecked;
      p_e.Node.Checked = CheckState.Checked;
      m_checkingAccount = false;
      if (m_selectedCell != null)
        m_selectedCell.Value = p_e.Node.Text;
      m_accountTV.SelectedNode = p_e.Node;
      m_dgv.Refresh();
    }

    void OnCellValidating(object sender, CellCancelEventArgs p_args)
    {
      if ((uint)p_args.Cell.ColumnItem.ItemValue == (uint)Column.NAME)
      {
        Account l_account = m_accountlist[(string)p_args.Cell.Value];

        if (l_account != null)
        {
          m_accountlist.Remove((string)p_args.Cell.Value);
          l_account.Name = (string)p_args.Cell.EditValue;
          m_accountlist[l_account.Name] = l_account;
        }
      }
    }

    void OnCellDoubleClick(object sender, CellMouseEventArgs p_args)
    {
      if ((uint)p_args.Cell.ColumnItem.ItemValue == (uint)Column.PARENT)
      {
        m_selectedCell = p_args.Cell;
        DisplayParents(true);
        vTreeNode l_node = m_accountTV.FindNode(AccountModel.Instance.GetValueId((string)p_args.Cell.Value));;
        m_accountTV.SelectedNode = l_node;
        l_node.Checked = CheckState.Checked;
      }
    }

    private void OnValidate(object sender, EventArgs e)
    {
      List<Account> l_list = new List<Account>();

      foreach (uint l_row in m_dgv.Rows.Keys)
      {
        Account l_account = m_accountlist[(string)m_dgv.GetCellValue(l_row, (uint)Column.NAME)];

        l_account.FormulaType = GetSelectedValue<Account.FormulaTypes>(l_row, Column.FORMULA_TYPE);
        l_account.ConsolidationOptionId = GetSelectedValue<Account.ConsolidationOptions>(l_row, Column.CONSOLIDATION_OPTION);
        l_account.ConversionOptionId = GetSelectedValue<Account.ConversionOptions>(l_row, Column.CURRENCY_OPTION);
        l_account.Type = GetSelectedValue<Account.AccountType>(l_row, Column.ACCOUNT_TYPE);
        l_account.ParentId = AccountModel.Instance.GetValueId((string)m_dgv.GetCellValue(l_row, (uint)Column.PARENT));
        l_list.Add(l_account);
      }

      if (!m_controller.CreateAccountList(l_list))
        MessageBox.Show(m_controller.Error);
    }

    T GetSelectedValue<T>(uint p_row, Column p_column)
    {
      ComboBoxEditor l_item = (ComboBoxEditor)m_dgv.GetCellEditor(p_row, (uint)p_column);

      if (l_item == null || l_item.SelectedItem == null)
        return (default(T));
      return ((T)l_item.SelectedItem.Value);
    }

    void DisplayParents(bool p_status)
    {
      m_topPanel.ColumnCount = (p_status) ? 2 : 1;
    }

    void OnUpdateList(Network.ErrorMessage p_status, SafeDictionary<CRUDAction, SafeDictionary<uint, Network.ErrorMessage>> p_updateResults)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
      {
        MessageBox.Show(Network.Error.GetMessage(p_status));
        return;
      }
      GridCellStyle l_red = GridTheme.GetDefaultTheme(m_dgv.VIBlendTheme).GridCellStyle;
      GridCellStyle l_green = GridTheme.GetDefaultTheme(m_dgv.VIBlendTheme).GridCellStyle;

      l_red.FillStyle = new FillStyleSolid(Color.Red);
      l_green.FillStyle = new FillStyleSolid(Color.Green);
      foreach (HierarchyItem l_row in m_dgv.RowsHierarchy.Items)
        l_row.CellsStyle = l_red;
      foreach (KeyValuePair<uint, Network.ErrorMessage> l_pair in p_updateResults[CRUDAction.CREATE])
      {
        HierarchyItem l_row = FindRow(AccountModel.Instance.GetValueName(l_pair.Key));

        if (l_row != null)
          l_row.CellsStyle = l_green;
      }
      m_dgv.Select();
      m_dgv.Refresh();
    }

    HierarchyItem FindRow(string p_name)
    {
      foreach (KeyValuePair<uint, HierarchyItem> l_row in m_dgv.Rows)
        if (p_name == (string)m_dgv.GetCellValue(l_row.Key, (uint)Column.NAME))
          return (l_row.Value);
      return (null);
    }
  }
}