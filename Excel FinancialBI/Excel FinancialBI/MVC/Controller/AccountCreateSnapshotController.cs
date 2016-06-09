using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using View;
  using Excel;
  using Model.CRUD;

  class AccountCreateSnapshotController : AccountController 
  {
    SafeDictionary<string, Account> m_accountList;
    WorksheetExtractor m_extractor;
    AccountSnapshotSelectionView m_selectionView;
    AccountSnapshotPropertiesView m_propertiesView;

    public AccountCreateSnapshotController(Worksheet p_worksheet)
    {
      m_extractor = new WorksheetExtractor(p_worksheet);
      m_extractor.Extract(p_worksheet.UsedRange);
      m_selectionView = new AccountSnapshotSelectionView(m_extractor.GetExtractedValues<string>());
      m_selectionView.SetController(this);
      m_propertiesView = new AccountSnapshotPropertiesView();
      m_propertiesView.SetController(this);
      LoadView();
    }

    void LoadView()
    {
      m_selectionView.LoadView();
      m_selectionView.Show();
    }

    public void SelectAccounts(SafeDictionary<string, Account> p_accountList)
    {
      m_accountList = p_accountList;
      m_selectionView.Hide();
      m_propertiesView.LoadView(m_accountList);
      m_propertiesView.Show();
    }
  }
}
