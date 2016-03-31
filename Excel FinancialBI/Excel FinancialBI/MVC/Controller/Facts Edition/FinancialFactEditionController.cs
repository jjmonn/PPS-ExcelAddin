using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using Model;
  using Model.CRUD;
  using View;

  class FinancialFactEditionController : AFactEditionController<FinancialEditedFactsModel>
  {
    FinancialFactEditionView m_view;
    public override IFactEditionView View { get { return (m_view); } }

    public FinancialFactEditionController(AddinModuleController p_addinController, UInt32 p_versionId, Worksheet p_worksheet) :
      base(p_addinController, Account.AccountProcess.FINANCIAL, p_versionId, p_worksheet)
    {
      EditedFactModel = new FinancialEditedFactsModel(p_worksheet);
      m_view = new FinancialFactEditionView(this, p_worksheet);
      m_view.LoadView();
    }
  }
}
