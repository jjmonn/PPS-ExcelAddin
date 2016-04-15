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
  using Utils.BNF;

  class AccountController : NameController<AccountsView>
  {
    #region Variables

    public override IView View { get { return (m_view); } }
    private const string ACCOUNTS_FORBIDDEN_CHARACTERS = "+-*=<>^?:;![]";
    public SimpleBnf BNF = new SimpleBnf();
    public FbiGrammar Grammar = new FbiGrammar();

    #endregion

    #region Initialize

    public AccountController()
    {
      m_view = new AccountsView();
      m_view.SetController(this);
      BNF.AddRule("fbi_to_grammar", Grammar.ToGrammar);
      BNF.AddRule("fbi_to_human_grammar", Grammar.ToHuman);
      this.LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
    }

    #endregion

    #region CreateUI

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
          new AllocationKeysController(l_account);
    }

    #endregion

    #region Server

    public bool UpdateAccountList(List<Account> p_accountList)
    {
      AccountModel.Instance.UpdateList(p_accountList, CRUDAction.UPDATE);
      return (true);
    }

    public bool UpdateAccount(Account p_account)
    {
      if (CheckAccountValidity(p_account) == false)
      {
        Error = Local.GetValue("accounts.error.update") + ": " + Error;
        return (false);
      }
      if (AccountModel.Instance.Update(p_account) == false)
      {
        Error = Local.GetValue("accounts.error.update") + ": " + Local.GetValue("general.error.system");
        return (false);
      }
      return (true);
    }

    List<Account> GetDependantAccounts(UInt32 p_id)
    {
      List<Account> l_dependantAccounts = AccountModel.Instance.GetChildren(p_id);

      foreach (Account l_account in AccountModel.Instance.GetDictionary().Values)
      {
        bool l_result = BNF.Parse("fbi_to_human_grammar", l_account.Formula);
        if (l_result)
          if (Grammar.Accounts.Contains(p_id))
            l_dependantAccounts.Add(l_account);
      }
      return (l_dependantAccounts);
    }

    public bool DeleteAccount(UInt32 p_id)
    {
      List<Account> l_dependantAccounts = GetDependantAccounts(p_id);

      if (l_dependantAccounts.Count > 0)
      {
        string l_msg = "";
        foreach (Account l_account in l_dependantAccounts)
          l_msg += " - " + l_account.Name + "\n\r";
        Error = Local.GetValue("accounts.msg_dependant_accounts") + "\n\r" + l_msg + "\n\r" + Local.GetValue("accounts.msg_formula_to_be_changed");
      }
      else if (AccountModel.Instance.GetValue(p_id) == null)
        Error = Local.GetValue("general.error.not_found");
      else if (AccountModel.Instance.Delete(p_id) == false)
        Error = Local.GetValue("general.error.system");
      else
        return (true);
      return (false);
    }

    public bool CheckAccountValidity(Account p_account)
    {
      if (p_account == null)
        Error = Local.GetValue("general.error.system");
      else if (IsNameValid(p_account.Name) == false)
        return (false);
      else if (p_account.ParentId != 0 && AccountModel.Instance.GetValue(p_account.ParentId) == null)
        Error = Local.GetValue("general.error.invalid_attribute");
      else if (Enum.IsDefined(typeof(Account.AccountProcess), p_account.Process) == false)
        Error = Local.GetValue("general.error.invalid_attribute");
      else if (Enum.IsDefined(typeof(Account.AccountType), p_account.Type) == false)
        Error = Local.GetValue("general.error.invalid_attribute");
      else if (Enum.IsDefined(typeof(Account.ConsolidationOptions), p_account.ConsolidationOptionId) == false)
        Error = Local.GetValue("general.error.invalid_attribute");
      else
        return (true);
      return (false);
    }

    public bool CreateAccount(Account p_account)
    {
      if (CheckAccountValidity(p_account) == false)
      {
        Error = Local.GetValue("accounts.error.create") + ": " + Error;
        return (false);
      }
      else if (AccountModel.Instance.GetValue(p_account.Name) != null)
        Error = Local.GetValue("general.error.name_already_used");
      else if (AccountModel.Instance.Create(p_account) == false)
        Error = Local.GetValue("general.error.system");
      else
        return (true);
      Error = Local.GetValue("accounts.error.create") + ": " + Error;
      return (false);
    }

    public bool CreateAccount(UInt32 p_parentId, string p_name, Account.AccountProcess p_process, Account.FormulaTypes p_formulaType, string p_formula,
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
      return CreateAccount(l_new);
    }

    #endregion

    #region Check

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

    #endregion

  }
}
