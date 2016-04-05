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
  using Model.CRUD;

  class RHFactEditionController : AFactEditionController<RHEditedFactsModel>
  {
    RHFactEditionView m_view;
    public override IFactEditionView View { get { return (m_view); } }
    FactsRHCommit m_factsCommit;
    RangeHighlighter m_highlighter;

    public RHFactEditionController(AddinModuleController p_addinController, UInt32 p_versionId,
      Worksheet p_worksheet, List<Int32> p_periodsList, UInt32 p_RHAccountId) :
      base(p_addinController, Account.AccountProcess.RH, p_versionId, p_worksheet, p_periodsList)
    {
      PeriodsList = p_periodsList;
      RHAccountId = p_RHAccountId;
      EditedFactModel = new RHEditedFactsModel(p_periodsList, p_worksheet);
      m_view = new RHFactEditionView(this, p_worksheet);
      m_view.LoadView();
      m_statusView = new StatusReportInterfaceUI(EditedFactModel);
    }

    public override void Close()
    {
      base.Close();
      if (m_factsCommit != null)
        m_factsCommit.UnSuscribeEvents();
    }

    public override void CommitFacts() // TODO: extract RangeHighlighter from FactsRHCommit
    {
      m_factsCommit = new FactsRHCommit(EditedFactModel, PeriodsList, m_view.RangeHighlighter, RHAccountId, VersionId);
      // TO DO
      // Antiduplicate system

      List<string> l_clientsToBeCreated = EditedFactModel.GetClientsToBeCreatedList();
      if (l_clientsToBeCreated.Count > 0)
      {
        UnreferencedClientsController l_unreferencedClientsController = new UnreferencedClientsController(l_clientsToBeCreated);
        l_unreferencedClientsController.OnClientsCreated += OnUndefinedClientsCreated;
        return;
      }
      m_factsCommit.Commit();
    }

    private void OnUndefinedClientsCreated(bool p_status)
    {
      if (p_status == true)
        CommitFacts();
    }
  }
}
