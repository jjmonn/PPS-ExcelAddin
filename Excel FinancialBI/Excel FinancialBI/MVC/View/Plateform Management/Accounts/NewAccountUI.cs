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
    private AccountController m_controller = null;
    private AccountsView m_view = null;
    private vTreeViewBox m_parentAccountsTreeviewBox = new vTreeViewBox();

    public NewAccountUI()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as AccountController;
    }

    public void LoadView(AccountsView p_view, vTreeNode p_node)
    {
      this.m_view = p_view;

      this.ComboBoxesInit();
      this.MultilangueSetup();

      this.InitParentTV(p_node);

      this.CreateAccountBT.Click += OnCreateAccountClick;
      this.CancelBT.Click += OnCancelClick;

    }

    private void InitParentTV(vTreeNode p_node)
    {
      this.ParentTVPanel.Controls.Add(this.m_parentAccountsTreeviewBox);
      this.m_parentAccountsTreeviewBox.Dock = DockStyle.Fill;
      FbiTreeView<Account>.Load(this.m_parentAccountsTreeviewBox.TreeView.Nodes, AccountModel.Instance.GetDictionary());
      this.m_parentAccountsTreeviewBox.TreeView.SelectedNode = p_node;
    }

    private void OnCreateAccountClick(object sender, EventArgs e)
    {
      if (this.NameTextBox.Text != "")
      {
        if (this.m_controller.AccountNameCheck(this.NameTextBox.Text))
        {
          UInt32 l_parentId = 0;
          Int32 l_itemPosition = 0;
          Account l_parentAccount;

          if (this.m_parentAccountsTreeviewBox.TreeView.SelectedNode != null)
          {
            l_parentAccount = AccountModel.Instance.GetValue((UInt32)this.m_parentAccountsTreeviewBox.TreeView.SelectedNode.Value);
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
            if ((Account.FormulaTypes)this.FormulaComboBox.SelectedItem.Value == Account.FormulaTypes.TITLE)
            {
              l_parentId = 0;
              l_itemPosition = 0;
            }
            else
            {
              MessageBox.Show(Local.GetValue("accounts_edition.msg_select_parent_account"), Local.GetValue("general.accounts"),
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
            }
          }

          Account.ConsolidationOptions l_concolidation;

          if (this.m_aggregationRadioButton.Checked)
            l_concolidation = Account.ConsolidationOptions.AGGREGATION;
          else if (this.m_nonRadioButton.Checked)
            l_concolidation = Account.ConsolidationOptions.NONE;
          else
            l_concolidation = Account.ConsolidationOptions.RECOMPUTATION;

          Account.PeriodAggregationOptions l_period;

          if (this.m_endOfPeriodRadioButton.Checked)
            l_period = Account.PeriodAggregationOptions.END_PERIOD;
          else
            l_period = Account.PeriodAggregationOptions.AVERAGE_PERIOD;

          this.m_controller.CreateAccount(l_parentId, this.NameTextBox.Text, (Account.AccountProcess)this.ProcessComboBox.SelectedItem.Value,
            (Account.FormulaTypes)this.FormulaComboBox.SelectedItem.Value, "",
            (Account.AccountType)this.TypeComboBox.SelectedItem.Value, l_concolidation, l_period,
            "t", (UInt32)((Account.FormulaTypes)this.FormulaComboBox.SelectedItem.Value), l_itemPosition); //TODO : "t" typedef

          this.Close();
        }
      }
      else
        MessageBox.Show(Local.GetValue("accounts_edition.msg_name_empty"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void OnCancelClick(object p_sender, EventArgs p_e)
    {
      this.Close();
    }

    private void MultilangueSetup()
    {
      this.CancelBT.Text = Local.GetValue("general.cancel");
      this.CreateAccountBT.Text = Local.GetValue("general.create");
      this.m_accountNameLabel.Text = Local.GetValue("accounts_edition.account_name");
      this.m_accountParentLabel.Text = Local.GetValue("accounts_edition.account_parent");
      this.m_formulaTypeLabel.Text = Local.GetValue("accounts_edition.formula_type");
      this.m_formatLabel.Text = Local.GetValue("accounts_edition.account_format");
      this.m_recomputeRadioButton.Text = Local.GetValue("accounts_edition.recomputation");
      this.m_aggregationRadioButton.Text = Local.GetValue("accounts_edition.aggregation");
      this.m_consolidationOptionLabel.Text = Local.GetValue("accounts_edition.consolidation_option");
      this.m_endOfPeriodRadioButton.Text = Local.GetValue("accounts_edition.end_of_period_rate");
      this.m_averageRateRadioButton.Text = Local.GetValue("accounts_edition.average_rate");
      this.m_conversionOptionLabel.Text = Local.GetValue("accounts_edition.currencies_conversion");
      this.Text = Local.GetValue("accounts_edition.title_new_account");
    }

    private void ComboBoxesInit()
    {
      foreach (ListItem l_item in m_view.TypeComboBox.Items)
        this.TypeComboBox.Items.Add(l_item);
      this.TypeComboBox.SelectedIndex = 0;
      this.TypeComboBox.DropDownList = true;

      foreach (ListItem l_item in m_view.ProcessComboBox.Items)
        this.ProcessComboBox.Items.Add(l_item);
      this.ProcessComboBox.SelectedIndex = 0;
      this.ProcessComboBox.DropDownList = true;

      foreach (ListItem l_item in m_view.FormulaTypeComboBox.Items)
        this.FormulaComboBox.Items.Add(l_item);
      this.FormulaComboBox.SelectedIndex = 0;
      this.FormulaComboBox.DropDownList = true;
    }
  }
}
