﻿using System;
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

      m_view.ScanColumns();
      l_list = m_view.ExtractAccounts();
      foreach (Account l_account in l_list)
        if (AccountModel.Instance.GetValue(l_account.Name) == null)
        {
          if (CreateAccount(l_account) == false)
          {
            Error = "\"" + l_account.Name + "\": " + Error;
            return (false);
          }
        }
        else
        {
          Account l_base = AccountModel.Instance.GetValue(l_account.Name);

          l_base.CopyFrom(l_account);
          if (UpdateAccount(l_base) == false)
          {
            Error = "\"" + l_account.Name + "\": " + Error;
            return (false);
          }
        }
      return (true);
    }
  }
}