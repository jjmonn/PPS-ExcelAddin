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
  using Utils;

  class RHFactEditionView : AFactEditionView<RHEditedFactsModel, RHFactEditionController>
  {
    RHEditedFactsModel m_model;

    public RHFactEditionView(RHFactEditionController p_controller, Worksheet p_worksheet) : base(p_controller, p_worksheet)
    {
      m_model = p_controller.EditedFactModel;
    }

    public override void Close()
    {
      base.Close();
      FactsModel.Instance.UpdateEvent -= OnFactsUpdates;
      FactTagModel.Instance.UpdateListEvent -= OnFactTagsUpdates;
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      FactsModel.Instance.UpdateEvent += OnFactsUpdates;
      FactTagModel.Instance.UpdateListEvent += OnFactTagsUpdates;
    }

    protected override void SetEditedFactsStatus()
    {
      if (m_model.DisplayInitialDifference == true)
      {
        AddinModuleController.SetExcelInteractionState(false);
        foreach (EditedRHFact l_editedFact in m_controller.EditedFactModel.EditedFacts.Values)
          m_rangeHighlighter.FillCellColor(l_editedFact.Cell, l_editedFact.SetFactValueStatus());
        AddinModuleController.SetExcelInteractionState(true);
      }
    }

    #region Model callbacks

    void OnFactsUpdates(ErrorMessage p_status, CRUDAction p_action, SafeDictionary<string, Tuple<UInt32, ErrorMessage>> p_resultsDict)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;

      if (p_status == ErrorMessage.SUCCESS)
        foreach (KeyValuePair<string, Tuple<UInt32, ErrorMessage>> l_pair in p_resultsDict)
          if (l_pair.Value.Item2 == ErrorMessage.SUCCESS)
          {
            EditedFactBase l_fact = m_model.EditedFacts[l_pair.Key];

            if (l_fact != null)
              m_rangeHighlighter.FillCellGreen(l_fact.Cell);
          }
    }

    void OnFactTagsUpdates(ErrorMessage p_status, SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> p_updateResults)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;

      foreach (KeyValuePair<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> l_result in p_updateResults)
      {
        foreach (KeyValuePair<UInt32, ErrorMessage> l_pair in l_result.Value)
        {

        }
      }
    }

    #endregion

  }
}
