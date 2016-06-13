using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  class LogController : IController
  {
    LogView m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public LogController()
    {
    }

    void LoadView()
    {
      m_view = new LogView();
      m_view.SetController(this);
      m_view.LoadView();
    }

    public bool ShowView(UInt32 p_entityId, UInt32 p_versionId, UInt32 p_accountId, UInt32 p_period)
    {
      LoadView();
      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, p_entityId);
      Account l_account = AccountModel.Instance.GetValue(p_accountId);

      if (l_entity == null || l_entity.AllowEdition == false)
      {
        Error = Local.GetValue("log.error.entity_must_be_editable");
        return (false);
      }
      if (l_account == null || l_account.FormulaType != Account.FormulaTypes.HARD_VALUE_INPUT)
      {
        Error = Local.GetValue("log.error.account_must_be_input");
        return (false);
      }
      if (p_period == 0)
      {
        Error = Local.GetValue("log.error.no_period");
        return (false);
      }
      m_view.ShowView(p_entityId, p_versionId, p_accountId, p_period);
      return (true);
    }
  }
}
