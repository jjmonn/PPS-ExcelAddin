using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using Model;

  class RHFactEditionController : AFactEditionController<RHEditedFactsModel>
  {
    private UInt32 m_RHAccountId;

    public RHFactEditionController(AddinModuleController p_addinController, UInt32 p_versionId,
      Worksheet p_worksheet, List<Int32> p_periodsList, UInt32 p_RHAccountId) :
      base(p_addinController, Model.CRUD.Account.AccountProcess.FINANCIAL, p_versionId, p_worksheet, p_periodsList)
    {
      m_periodsList = p_periodsList;
      m_RHAccountId = p_RHAccountId;
      EditedFactModel = new RHEditedFactsModel(p_periodsList);
      EditedFactModel.FactsDownloaded += OnFactsDownloaded;
    }
  }
}
