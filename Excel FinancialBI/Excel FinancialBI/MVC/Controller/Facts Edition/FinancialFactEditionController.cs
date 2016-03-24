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

  class FinancialFactEditionController : AFactEditionController<FinancialEditedFactsModel>
  {
    public FinancialFactEditionController(AddinModuleController p_addinController, UInt32 p_versionId, Worksheet p_worksheet) :
      base(p_addinController, Account.AccountProcess.FINANCIAL, p_versionId, p_worksheet)
    {
      EditedFactModel = new FinancialEditedFactsModel();
      EditedFactModel.FactsDownloaded += OnFactsDownloaded;
    }
  }
}
