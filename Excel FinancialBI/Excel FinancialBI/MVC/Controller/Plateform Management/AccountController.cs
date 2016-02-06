using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  class AccountController : NameController
  {

    private AccountsView m_view;
    private FbiTreeView<Account> m_accountTV;
    private FbiTreeView<GlobalFact> m_globalFactTV;

    public AccountController(AccountsView p_view)
    {
      m_view = p_view;
      m_view.SetController(this);
      this.LoadView();
    }

    public override void LoadView()
    {
      m_accountTV = new FbiTreeView<Account>(AccountModel.Instance.GetDictionary());
      m_globalFactTV = new FbiTreeView<GlobalFact>(GlobalFactModel.Instance.GetDictionary());
      m_view.InitView(m_accountTV, m_globalFactTV);
    }

    public override void Close()
    {
      //TODO : Do
    }

  }
}
