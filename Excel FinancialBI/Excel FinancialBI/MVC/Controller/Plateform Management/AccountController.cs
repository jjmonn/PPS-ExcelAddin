using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.Controls;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  class AccountController : NameController<AccountsView>
  {
    public override IView View { get { return (m_view); } }

    public AccountController()
    {
      m_view = new AccountsView();
      m_view.SetController(this);
      this.LoadView();
    }

    public override void LoadView()
    {
      m_view.InitView();
    }

    public void CreateNewUI(vTreeNode p_node)
    {
      NewAccountUI l_newUi = new NewAccountUI();
      l_newUi.SetController(this);
      l_newUi.LoadView(this.m_view, p_node);
      l_newUi.ShowDialog();
    }

    public void CreateAllocationKeysView(vTreeNode p_node)
    {
      Account l_account = AccountModel.Instance.GetValue(p_node.Text);

      if (l_account != null)
        if (l_account.FormulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_account.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
        {
          AllocationKeysView l_allocationKeyView = new AllocationKeysView();
          l_allocationKeyView.SetController(null);
          //l_allocationKeyView.LoadView(l_account);
          l_allocationKeyView.ShowDialog();
        }
    }

    public void UpdateAccount(Account p_account)
    {
      if (p_account != null)
        AccountModel.Instance.Update(p_account);
    }

    public void DeleteAccount(vTreeNode p_node)
    {
      List<String> l_dependantAccounts = this.ExistingDependantAccounts(p_node);
      if (l_dependantAccounts.Count > 0)
      {
        string l_msg = "";
        foreach (string l_account in l_dependantAccounts)
          l_msg += " - " + l_account + "\n\r";
        MessageBox.Show(Local.GetValue("accounts_edition.msg_dependant_accounts") + "\n\r" +
                     l_msg + "\n\r" +
                     Local.GetValue("accounts_edition.msg_formula_to_be_changed"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      else
      {
        string l_result = PasswordBox.Open(Local.GetValue("accounts_edition.msg_account_deletion1") + "\n\r" + "\n"
          + Local.GetValue("accounts_edition.msg_account_deletion4")
          , Local.GetValue("accounts_edition.msg_account_deletion_confirmation"));

        if (l_result != PasswordBox.Canceled && l_result == Addin.Password)
        {
          if (MessageBox.Show(Local.GetValue("accounts_edition.msg_account_deletion3") + "\n\r" + "\n"
          + Local.GetValue("accounts_edition.msg_account_deletion2"),
            Local.GetValue("accounts_edition.msg_account_deletion_confirmation"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            this.DeleteAccount((UInt32)p_node.Value);
        }
        else if (l_result != PasswordBox.Canceled)
          MessageBox.Show(Local.GetValue("accounts_edition.msg_incorrect_password"), Local.GetValue("general.accounts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public void DeleteAccount(UInt32 p_id)
    {
      if (AccountModel.Instance.GetValue(p_id) != null)
      {
        AccountModel.Instance.Delete(p_id);
      }
      else
        Error = Local.GetValue("general.error.system");
    }

    public void CreateAccount(Account p_account)
    {
      if (p_account != null)
        AccountModel.Instance.Create(p_account);
    }

    public void CreateAccount(UInt32 p_parentId, string p_name, Account.AccountProcess p_process, Account.FormulaTypes p_formulaType, string p_formula,
      Account.AccountType p_type, Account.ConsolidationOptions p_consolidationOptionId, Account.PeriodAggregationOptions p_periodAggregationOptionId,
      string p_formatId, UInt32 p_image, Int32 p_itemPosition)
    {
      Account l_new = new Account();
      l_new.ParentId = p_parentId;
      l_new.Name = p_name;
      l_new.Process = p_process;
      l_new.FormulaType = p_formulaType;
      l_new.Formula = p_formula;
      l_new.Type = p_type;
      l_new.ConsolidationOptionId = p_consolidationOptionId;
      l_new.PeriodAggregationOptionId = p_periodAggregationOptionId;
      l_new.FormatId = p_formatId;
      l_new.Image = p_image;
      l_new.ItemPosition = p_itemPosition;
      AccountModel.Instance.Create(l_new);
    }

    public bool AccountNameCheck(string p_accountName)
    {
      //TODO : error
      if (p_accountName == "" || !this.IsNameValid(p_accountName) || AccountModel.Instance.GetValue(p_accountName) != null)
      {
        return (false);
      }
     return (true);
    }

    public List<string> ExistingDependantAccounts(vTreeNode p_node)
    {
      List<string> l_dependantAccounts = new List<string>();
      List<UInt32> l_accountKeyList = FbiTreeView<Account>.GetNodesIdsUint(p_node);
      l_accountKeyList.Reverse();

      List<UInt32> l_dependantAccountsId = this.DependenciesLoopCheck(l_accountKeyList);
      foreach (UInt32 l_dependantAccountId in l_dependantAccountsId)
      {
        Account l_account = AccountModel.Instance.GetValue(l_dependantAccountId);
        if (l_account != null)
          l_dependantAccounts.Add(l_account.Name);
      }
      return (l_dependantAccounts);
    }

    private List<UInt32> DependenciesLoopCheck(List<UInt32> p_accountKeyList)
    {
      List<UInt32> l_dependenciesList = new List<UInt32>();

      foreach (UInt32 l_key in p_accountKeyList)
        this.CheckForDependencies(l_key, l_dependenciesList);
      List<UInt32> l_uniqueDependenciesList = l_dependenciesList.Distinct().ToList();
      foreach (UInt32 l_accountId in p_accountKeyList)
      {
        if (l_uniqueDependenciesList.Contains(l_accountId))
          l_uniqueDependenciesList.Remove(l_accountId);
      }
      return (l_uniqueDependenciesList);
    }

    private void CheckForDependencies(UInt32 p_key, List<UInt32> p_dependenciesList)
    {
      MultiIndexDictionary<UInt32, string, Account> l_accounts = AccountModel.Instance.GetDictionary();

      if (l_accounts != null)
      {
        foreach (UInt32 l_accountId in l_accounts.Keys)
        {
          Account l_account = AccountModel.Instance.GetValue(l_accountId);
          if (l_account != null)
            if (l_account.Formula != null && l_account.Formula.Contains(p_key.ToString()))
              p_dependenciesList.Add(l_accountId);
        }
      }
    }

  }
}
