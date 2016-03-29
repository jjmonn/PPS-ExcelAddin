using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Network;
  using Forms;
  using Utils;

  class FinancialFactEditionView : AFactEditionView<FinancialEditedFactsModel, FinancialFactEditionController>
  {
    FinancialEditedFactsModel m_model;

    public FinancialFactEditionView(FinancialFactEditionController p_controller, Worksheet p_worksheet) : base(p_controller, p_worksheet)
    {
      m_model = p_controller.EditedFactModel;
      SuscribeEvents();
    }

    public override void Close()
    {
      base.Close();
      FactsModel.Instance.ReadEvent -= OnFinancialInputDownloaded;
      FactsModel.Instance.UpdateEvent -= OnCommitResult;
      SourcedComputeModel.Instance.ComputeCompleteEvent -= OnFinancialOutputsComputed;
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      FactsModel.Instance.ReadEvent += OnFinancialInputDownloaded;
      SourcedComputeModel.Instance.ComputeCompleteEvent += OnFinancialOutputsComputed;
      FactsModel.Instance.UpdateEvent += OnCommitResult;
    }

    private void SetEditedFactsStatus()
    {
      AddinModuleController.SetExcelInteractionState(false);
      foreach (EditedFactBase l_editedFact in m_controller.EditedFactModel.EditedFacts.Values)
        m_model.RangesHighlighter.FillCellColor(l_editedFact.Cell, l_editedFact.SetFactValueStatus());
      AddinModuleController.SetExcelInteractionState(true);
    }

    private void OnCommitResult(ErrorMessage p_status, CRUDAction p_action, SafeDictionary<string, Tuple<UInt32, ErrorMessage>> p_resultDic)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        foreach (KeyValuePair<string, Tuple<UInt32, ErrorMessage>> l_pair in p_resultDic)
        {
          if (l_pair.Value.Item2 != ErrorMessage.SUCCESS)
            continue;
          EditedFinancialFact l_fact = m_model.EditedFacts[l_pair.Key];

          if (l_fact != null)
            m_model.RangesHighlighter.FillCellGreen(l_fact.Cell);
        }
      }
      else
        MsgBox.Show(Local.GetValue("upload.error.commit_failed") + ": " + Error.GetMessage(p_status));
    }

    private void OnFinancialInputDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        if (m_model.FillFactsDictionnaries(p_fact_list) == false)
          m_model.RaiseFactDownloaded(false);
        m_model.RequestIdList.Remove(p_requestId);
        if (m_model.RequestIdList.Count == 0)
          m_model.ComputeOutputs();
      }
      else
        m_model.RaiseFactDownloaded(false);
    }

    private void OnFinancialOutputsComputed(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        AddinModuleController.SetExcelInteractionState(false);

        foreach (ComputeResult l_result in p_result.Values)
        {
          foreach (KeyValuePair<ResultKey, double> l_valuePair in l_result.Values)
          {
            DimensionKey l_key =
              new DimensionKey(l_valuePair.Key.EntityId, l_valuePair.Key.AccountId, (UInt32)AxisType.Employee, l_valuePair.Key.Period);
            EditedFinancialFact l_fact = m_model.OutputFacts[l_key];

            if (l_fact == null)
              continue;
            l_fact.Value = l_valuePair.Value;
            l_fact.EditedValue = l_valuePair.Value;
            if ((Double.IsNaN(l_valuePair.Value)))
              l_fact.Cell.Value2 = "-";
            else if (Double.IsNegativeInfinity(l_valuePair.Value))
              l_fact.Cell.Value2 = "-inf.";
            else if (Double.IsPositiveInfinity(l_valuePair.Value))
              l_fact.Cell.Value2 = "+inf.";
            else
              l_fact.Cell.Value = l_valuePair.Value;
          }
        }
        AddinModuleController.SetExcelInteractionState(true);
        m_model.RaiseFactDownloaded(true);
      }
      else
        m_model.RaiseFactDownloaded(false);
    }
  }
}
