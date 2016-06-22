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
    #region Variables

    public override IView View { get { return (m_view); } }
    private const string ACCOUNTS_FORBIDDEN_CHARACTERS = "+-*=<>^?:;![]";
    public BNF m_bnf = new BNF();
    AllocationKeysController m_allocationKeyController;

    #endregion

    #region Initialize

    public AccountController()
    {
      m_view = new AccountsView();
      m_view.SetController(this);
      FbiGrammar.AddGrammar(m_bnf);
      m_allocationKeyController = new AllocationKeysController();
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

    public void ShowView(vTreeNode p_node)
    {
      Account l_account = AccountModel.Instance.GetValue(p_node.Text);

      if (l_account != null)
        if (l_account.FormulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_account.FormulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
          m_allocationKeyController.ShowView(l_account);
    }

    #endregion

    #region Server

    public bool UpdateAccountList(List<Account> p_accountList)
    {
      return (AccountModel.Instance.UpdateList(p_accountList, CRUDAction.UPDATE));
    }

    public bool CreateAccountList(List<Account> p_accountList)
    {
      foreach (Account l_account in p_accountList)
      {
        if (l_account.Type == Account.AccountType.PERCENTAGE)
          l_account.PeriodAggregationOptionId = Account.PeriodAggregationOptions.NO_AGGREGATION;
        l_account.ConversionOptionId = (l_account.Type == Account.AccountType.MONETARY) ? Account.ConversionOptions.AVERAGE_RATE : Account.ConversionOptions.NO_CONVERSION;
        if (CheckAccountValidity(l_account) == false)
        {
          Error = Local.GetValue("accounts.error.create") + " \"" + l_account.Name + "\": " + Error;
          return (false);
        }
      }
      return (AccountModel.Instance.UpdateList(p_accountList, CRUDAction.CREATE));
    }

    public bool UpdateAccount(Account p_account)
    {
      if (CheckAccountValidity(p_account) == false)
      {
        Error = Local.GetValue("accounts.error.update") + ": " + Error;
        return (false);
      }
      
      if (p_account.Type != Account.AccountType.MONETARY)
        p_account.ConversionOptionId = Account.ConversionOptions.NO_CONVERSION;
      else if (p_account.ConversionOptionId == Account.ConversionOptions.NO_CONVERSION)
        p_account.ConversionOptionId = Account.ConversionOptions.AVERAGE_RATE;
      if (p_account.Type == Account.AccountType.PERCENTAGE)
        p_account.PeriodAggregationOptionId = Account.PeriodAggregationOptions.NO_AGGREGATION;

      if (AccountModel.Instance.Update(p_account) == false)
      {
        Error = Local.GetValue("accounts.error.update") + ": " + Local.GetValue("general.error.system");
        return (false);
      }
      return (true);
    }

    SortedSet<Account> GetDependantAccounts(UInt32 p_id)
    {
      SortedSet<Account> l_dependantAccounts = new SortedSet<Account>();

      List<Account> l_children = AccountModel.Instance.GetChildren(p_id);

      foreach (Account l_child in l_children)
      {
        SortedSet<Account> l_childDep = GetDependantAccounts(l_child.Id);

        foreach (Account l_dep in l_childDep)
          l_dependantAccounts.Add(l_dep);
      }
      foreach (Account l_account in AccountModel.Instance.GetDictionary().Values)
      {
        FbiGrammar.ClearAccounts();
        bool l_result = m_bnf.Parse(l_account.Formula, FbiGrammar.TO_HUMAN);
        if (l_result && FbiGrammar.Accounts.Contains(p_id))
          l_dependantAccounts.Add(l_account);
      }
      l_dependantAccounts.Remove(AccountModel.Instance.GetValue(p_id));
      return (l_dependantAccounts);
    }

    public bool DeleteAccount(UInt32 p_id)
    {
      SortedSet<Account> l_dependantAccounts = GetDependantAccounts(p_id);

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
        Error = Local.GetValue("accounts.error.invalid_parent");
      else if (Enum.IsDefined(typeof(Account.AccountProcess), p_account.Process) == false)
        Error = Local.GetValue("accounts.error.invalid_process");
      else if (Enum.IsDefined(typeof(Account.AccountType), p_account.Type) == false)
        Error = Local.GetValue("accounts.error.invalid_type");
      else if (Enum.IsDefined(typeof(Account.ConsolidationOptions), p_account.ConsolidationOptionId) == false)
        Error = Local.GetValue("accounts.error.invalid_consolidation");
      else if (Enum.IsDefined(typeof(Account.Format), p_account.FormatId) == false)
        Error = Local.GetValue("accounts.error.invalid_format");
      else
        return (true);
      return (false);
    }

    public bool CreateAccount(Account p_account)
    {
      if (AccountModel.Instance.GetDictionary().Count == 0)
        p_account.ItemPosition = 0;
      else
      {
        Account l_account = AccountModel.Instance.GetDictionary().SortedValues.Last();
        p_account.ItemPosition = l_account.ItemPosition + 1;
      }

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
      Account.Format p_formatId, UInt32 p_image, Int32 p_itemPosition)
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
      if (l_new.Type == Account.AccountType.PERCENTAGE)
        l_new.PeriodAggregationOptionId = Account.PeriodAggregationOptions.NO_AGGREGATION;
      l_new.ConversionOptionId = (p_type == Account.AccountType.MONETARY) ? Account.ConversionOptions.AVERAGE_RATE : Account.ConversionOptions.NO_CONVERSION;
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
