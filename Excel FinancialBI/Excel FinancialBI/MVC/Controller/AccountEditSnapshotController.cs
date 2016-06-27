using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;

  class AccountEditSnapshotController : AccountController
  {
    AccountEditSnapshot m_view;
    public IView View { get { return (m_view); } }

    public AccountEditSnapshotController(Worksheet p_worksheet)
    {
      m_view = new AccountEditSnapshot(p_worksheet);
      m_view.SetController(this);
    }

    public bool CreateReport()
    {
      m_view.CreateReport();
      return (true);
    }

    public bool LaunchSnapshot()
    {
      List<Account> l_list;
      List<Account> l_createList = new List<Account>();
      List<Account> l_updateList = new List<Account>();

      m_view.ScanColumns();

      l_list = m_view.ExtractAccounts();
      if (l_list == null)
      {
        Error = m_view.Error;
        return (false);
      }

      foreach (Account l_account in l_list)
        if (AccountModel.Instance.GetValue(l_account.Name) == null)
          l_createList.Add(l_account);
        else
          l_updateList.Add(l_account);
      if (CreateAccountList(l_createList) == false)
        return (false);
      else
      {
        foreach (Account l_account in l_updateList)
          if (CheckAccountValidity(l_account) == false)
            return (false);
        if (UpdateAccountList(l_updateList) == false)
          return (false);
      }
      return (true);
    }
  }
}
