using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.View
{
  using Controller;
  using Model.CRUD;
  using Model;
  using Network;

  class RHFactEditionView : AFactEditionView<RHEditedFactsModel, RHFactEditionController>
  {
    RHEditedFactsModel m_model;

    public RHFactEditionView(RHFactEditionController p_controller, Worksheet p_worksheet) : base(p_controller, p_worksheet)
    {
      m_model = p_controller.EditedFactModel;
      SuscribeEvents();
    }

    ~RHFactEditionView()
    {
      FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      FactsModel.Instance.ReadEvent += AfterRHFactsDownloaded;
    }

    private void SetEditedFactsStatus()
    {
      if (m_model.DisplayInitialDifference == true)
      {
        AddinModuleController.SetExcelInteractionState(false);
        foreach (EditedRHFact l_editedFact in m_controller.EditedFactModel.EditedFacts.Values)
          m_model.RangesHighlighter.FillCellColor(l_editedFact.Cell, l_editedFact.SetFactValueStatus());
        AddinModuleController.SetExcelInteractionState(true);
      }
    }

    private void AfterRHFactsDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      // TO DO : time up to manage the case where the server stops answering or the connection is lost : 
      //          -> In this case notify user and exit fact edition 
      AddinModuleController.SetExcelInteractionState(false);
      if (p_status == ErrorMessage.SUCCESS)
      {
        if (m_model.FillEditedFacts(p_fact_list) == false)
        {
          SetEditedFactsStatus();
          AddinModuleController.SetExcelInteractionState(true);
          m_model.RaiseFactDownloaded(false);
        }
        m_model.RequestIdList.Remove(p_requestId);
        if (m_model.RequestIdList.Count == 0)
        {
          SetEditedFactsStatus();
          AddinModuleController.SetExcelInteractionState(true);
          m_model.RaiseFactDownloaded(true);
        }
      }
      else
      {
        SetEditedFactsStatus();
        AddinModuleController.SetExcelInteractionState(true);
        m_model.RaiseFactDownloaded(false);
      }
    }

  }
}
