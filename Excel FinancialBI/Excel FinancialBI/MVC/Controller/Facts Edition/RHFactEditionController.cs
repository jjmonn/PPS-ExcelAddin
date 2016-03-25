using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using Model;
  using View;

  class RHFactEditionController : AFactEditionController<RHEditedFactsModel>
  {
    RHFactEditionView m_view;
    public override IFactEditionView View { get { return (m_view); } }

    public RHFactEditionController(AddinModuleController p_addinController, UInt32 p_versionId,
      Worksheet p_worksheet, List<Int32> p_periodsList, UInt32 p_RHAccountId) :
      base(p_addinController, Model.CRUD.Account.AccountProcess.FINANCIAL, p_versionId, p_worksheet, p_periodsList)
    {
      m_periodsList = p_periodsList;
      m_RHAccountId = p_RHAccountId;
      EditedFactModel = new RHEditedFactsModel(p_periodsList, p_worksheet);
      EditedFactModel.FactsDownloaded += OnFactsDownloaded;
      m_view = new RHFactEditionView(this, p_worksheet);
    }
  }
}
