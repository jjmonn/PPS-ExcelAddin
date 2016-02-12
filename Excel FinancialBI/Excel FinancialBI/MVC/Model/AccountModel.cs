using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;

  class AccountModel : NamedCRUDModel<Account>
  {
    static AccountModel s_instance = new AccountModel();
    public static AccountModel Instance { get { return (s_instance); } }

    AccountModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_ACCOUNT;
      ReadCMSG = ClientMessage.CMSG_READ_ACCOUNT;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_ACCOUNT;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_ACCOUNT_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_ACCOUNT;
      ListCMSG = ClientMessage.CMSG_LIST_ACCOUNT;

      CreateSMSG = ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_ACCOUNT_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_ACCOUNT_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_ACCOUNT_ANSWER;

      Build = Account.BuildAccount;

      InitCallbacks();
    }

  /*  public List<string> GetAccountsNameList(Account.FormulaTypesLookUpOption LookupOption, Account.AccountProcess p_process)
    {
      List<string> l_accountsList = new List<string>();
      List<Account.FormulaTypes> l_formulaTypesFilter = new List<Account.FormulaTypes>();
      switch (LookupOption)
      {
        case Account.FormulaTypesLookUpOption.ALL:
          l_formulaTypesFilter.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
          l_formulaTypesFilter.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
          l_formulaTypesFilter.Add(Account.FormulaTypes.FORMULA);
          l_formulaTypesFilter.Add(Account.FormulaTypes.HARD_VALUE_INPUT);
          break;

        case Account.FormulaTypesLookUpOption.INPUTS:
          l_formulaTypesFilter.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
          l_formulaTypesFilter.Add(Account.FormulaTypes.HARD_VALUE_INPUT);
          break;

        case Account.FormulaTypesLookUpOption.OUTPUTS:
          l_formulaTypesFilter.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
          l_formulaTypesFilter.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
          l_formulaTypesFilter.Add(Account.FormulaTypes.FORMULA);
          break;

        case Account.FormulaTypesLookUpOption.TITLES:
          l_formulaTypesFilter.Add(Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS);
          l_formulaTypesFilter.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT);
          l_formulaTypesFilter.Add(Account.FormulaTypes.FORMULA);
          break;
      }

      foreach (UInt32 l_id in m_CRUDDic.Keys)
      {
        Account l_account = GetValue(l_id);
        if (l_account == null)
          continue;
        if (l_formulaTypesFilter.Contains(l_account.FormulaType) && l_account.Process == p_process)
        {
          l_accountsList.Add(l_account.Name);
        }
      }
      return l_accountsList;
    }*/

  }
}
