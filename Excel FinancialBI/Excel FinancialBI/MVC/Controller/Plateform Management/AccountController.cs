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

    private const string ACCOUNTS_FORBIDDEN_CHARACTERS = "+-*=<>^?:;![]";

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

    public void UpdateAccount(Account p_account)
    {
      if (p_account != null)
        AccountModel.Instance.Update(p_account);
    }

    public void DeleteAccount(UInt32 p_id)
    {
      if (AccountModel.Instance.GetValue(p_id) != null)
      {
        //AccountModel.Instance.Delete(p_id);
      }
      else
        Error = Local.GetValue("general.error.system");
    }

    public void CreateAccount(Account p_account)
    {
      if (p_account != null)
        AccountModel.Instance.Create(p_account);
    }

    public bool AccountNameCheck(string p_accountName)
    {
      //TODO : maybe string len + error
      if (p_accountName == null)
      {
        return (false);
      }
      if (AccountModel.Instance.GetValue(p_accountName) != null)
      {
        return (false);
      }
      if (!CheckAccountNameForForbiddenCharacters(p_accountName))
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

    private bool CheckAccountNameForForbiddenCharacters(string p_accountName)
    {
      foreach (char l_char in ACCOUNTS_FORBIDDEN_CHARACTERS)
      {
        if (p_accountName.Contains(p_accountName))
          return (false);
      }
      return (true);
    }

  }
}
