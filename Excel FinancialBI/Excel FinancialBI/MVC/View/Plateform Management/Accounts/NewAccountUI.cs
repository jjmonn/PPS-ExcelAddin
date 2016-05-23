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

namespace FBI.MVC.View
{
  using Controller;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  public partial class NewAccountUI : Form, IView
  {

    #region Variables

    private AccountController m_controller = null;
    private AccountsView m_view = null;
    private vTreeViewBox m_parentAccountsTreeviewBox = new vTreeViewBox();

    #endregion

    #region Initialize

    public NewAccountUI()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AccountController;
    }

    public void LoadView(AccountsView p_view, vTreeNode p_node)
    {
      m_view = p_view;

      ComboBoxesInit();
      MultilangueSetup();

      InitParentTV(p_node);
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      Addin.SuscribeAutoLock(this);
      CreateAccountBT.Click += OnCreateAccountClick;
      CancelBT.Click += OnCancelClick;
    }

    private void InitParentTV(vTreeNode p_node)
    {
      ParentTVPanel.Controls.Add(m_parentAccountsTreeviewBox);
      m_parentAccountsTreeviewBox.Dock = DockStyle.Fill;
      FbiTreeView<Account>.Load(m_parentAccountsTreeviewBox.TreeView.Nodes, AccountModel.Instance.GetDictionary());
      m_parentAccountsTreeviewBox.TreeView.SelectedNode = p_node;
    }

    private void MultilangueSetup()
    {
      CancelBT.Text = Local.GetValue("general.cancel");
      CreateAccountBT.Text = Local.GetValue("general.create");
      m_accountNameLabel.Text = Local.GetValue("accounts.name");
      m_accountParentLabel.Text = Local.GetValue("accounts.parent");
      m_formulaTypeLabel.Text = Local.GetValue("accounts.formula_type");
      m_formatLabel.Text = Local.GetValue("accounts.format");
      m_recomputeRadioButton.Text = Local.GetValue("accounts.recomputation");
      m_aggregationRadioButton.Text = Local.GetValue("accounts.aggregation");
      m_consolidationOptionLabel.Text = Local.GetValue("accounts.consolidation_option");
      m_endOfPeriodRadioButton.Text = Local.GetValue("accounts.end_of_period_rate");
      m_averageRateRadioButton.Text = Local.GetValue("accounts.average_rate");
      m_conversionOptionLabel.Text = Local.GetValue("accounts.currencies_conversion");
      Text = Local.GetValue("accounts.title_new_account");
    }

    private void ComboBoxesInit()
    {
      foreach (ListItem l_item in m_view.TypeCB.Items)
        TypeComboBox.Items.Add(l_item);
      TypeComboBox.SelectedIndex = 0;
      TypeComboBox.DropDownList = true;

      foreach (ListItem l_item in m_view.ProcessCB.Items)
        ProcessComboBox.Items.Add(l_item);
      ProcessComboBox.SelectedIndex = 0;
      ProcessComboBox.DropDownList = true;

      foreach (ListItem l_item in m_view.FormulaTypeCB.Items)
        FormulaComboBox.Items.Add(l_item);
      FormulaComboBox.SelectedIndex = 0;
      FormulaComboBox.DropDownList = true;
    }

    #endregion

    #region Event

    private void OnCreateAccountClick(object sender, EventArgs e)
    {
      UInt32 l_parentId = 0;
      Int32 l_itemPosition = 0;
      Account l_parentAccount;

      if (m_parentAccountsTreeviewBox.TreeView.SelectedNode != null)
      {
        l_parentAccount = AccountModel.Instance.GetValue((UInt32)m_parentAccountsTreeviewBox.TreeView.SelectedNode.Value);
        if (l_parentAccount != null)
        {
          l_parentId = l_parentAccount.Id;
          l_itemPosition = l_parentAccount.AccountTab;
        }
        else
        {
          l_parentId = 0;
          l_itemPosition = 0;
        }
      }
      else
      {
        if ((Account.FormulaTypes)FormulaComboBox.SelectedItem.Value == Account.FormulaTypes.TITLE)
        {
          l_parentId = 0;
          l_itemPosition = 0;
        }
        else
        {
          Forms.MsgBox.Show(Local.GetValue("accounts.msg_select_parent_account"), Local.GetValue("general.accounts"),
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
      }

      Account.ConsolidationOptions l_consolidation;

      if (m_aggregationRadioButton.Checked)
        l_consolidation = Account.ConsolidationOptions.AGGREGATION;
      else if (m_nonRadioButton.Checked)
        l_consolidation = Account.ConsolidationOptions.NONE;
      else
        l_consolidation = Account.ConsolidationOptions.RECOMPUTATION;

      Account.PeriodAggregationOptions l_period;

      if (m_endOfPeriodRadioButton.Checked)
        l_period = Account.PeriodAggregationOptions.END_PERIOD;
      else
        l_period = Account.PeriodAggregationOptions.AVERAGE_PERIOD;

      if (m_controller.CreateAccount(l_parentId, NameTextBox.Text, (Account.AccountProcess)ProcessComboBox.SelectedItem.Value,
        (Account.FormulaTypes)FormulaComboBox.SelectedItem.Value, "",
        (Account.AccountType)TypeComboBox.SelectedItem.Value, l_consolidation, l_period,
        "t", (UInt32)((Account.FormulaTypes)FormulaComboBox.SelectedItem.Value), l_itemPosition) == false)
        MsgBox.Show(m_controller.Error);
      Close();
    }

    private void OnCancelClick(object p_sender, EventArgs p_e)
    {
      Close();
    }

    #endregion

  }
}
