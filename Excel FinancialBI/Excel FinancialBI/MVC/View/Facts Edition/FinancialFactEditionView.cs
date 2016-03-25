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

  class FinancialFactEditionView : AFactEditionView<FinancialEditedFactsModel, FinancialFactEditionController>
  {
    FinancialEditedFactsModel m_model;

    public FinancialFactEditionView(FinancialFactEditionController p_controller, Worksheet p_worksheet) : base(p_controller, p_worksheet)
    {
      m_model = p_controller.EditedFactModel;
      SuscribeEvents();
    }

    ~FinancialFactEditionView()
    {
      FactsModel.Instance.ReadEvent -= OnFinancialInputDownloaded;
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      FactsModel.Instance.ReadEvent += OnFinancialInputDownloaded;
      SourcedComputeModel.Instance.ComputeCompleteEvent += OnFinancialOutputsComputed;
    }

    private void OnFinancialInputDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        if (m_model.FillFactsDictionnaries(p_fact_list) == false)
        {
          m_model.RaiseFactDownloaded(false);
          FactsModel.Instance.ReadEvent -= OnFinancialInputDownloaded;
        }
        m_model.RequestIdList.Remove(p_requestId);
        if (m_model.RequestIdList.Count == 0)
          m_model.ComputeOutputs();
      }
      else
      {
        m_model.RaiseFactDownloaded(false);
        FactsModel.Instance.ReadEvent -= OnFinancialInputDownloaded;
      }
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
