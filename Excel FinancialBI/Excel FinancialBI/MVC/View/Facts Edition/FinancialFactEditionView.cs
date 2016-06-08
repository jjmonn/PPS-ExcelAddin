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
    int m_computePercentage = 0;
    int m_downloadPercentage = 0;

    public FinancialFactEditionView(FinancialFactEditionController p_controller, Worksheet p_worksheet) : base(p_controller, p_worksheet)
    {
    }

    public override void Close()
    {
      base.Close();
      FactsModel.Instance.UpdateEvent -= OnCommitResult;
    }

    protected override void SuscribeEvents()
    {
      base.SuscribeEvents();
      FactsModel.Instance.UpdateEvent += OnCommitResult;
      m_controller.EditedFactModel.ComputeFailed += OnComputeFailed;
      m_controller.WorksheetSelectionChanged += OnWorksheetSelectionChanged;
      m_controller.EditedFactModel.ComputeProgress += OnComputeProgress;
      m_controller.EditedFactModel.DownloadProgress += OnDownloadProgress;
    }

    delegate void OnComputeProgress_delegate(int p_percentage);
    void OnComputeProgress(int p_percentage)
    {
      if (m_progressBar.InvokeRequired)
      {
        OnComputeProgress_delegate func = new OnComputeProgress_delegate(OnComputeProgress);
        m_progressBar.Invoke(func, p_percentage);
      }
      else
      {
        m_computePercentage = p_percentage;
        m_progressBar.Value = (int)(m_computePercentage * 0.2) + (int)(m_downloadPercentage * 0.6);
      }
    }

    delegate void OnDownloadProgress_delegate(int p_percentage);
    void OnDownloadProgress(int p_percentage)
    {
      if (m_progressBar.InvokeRequired)
      {
        OnDownloadProgress_delegate func = new OnDownloadProgress_delegate(OnDownloadProgress);
        m_progressBar.Invoke(func, p_percentage);
      }
      else
      {
        m_downloadPercentage = p_percentage;
        m_progressBar.Value = (int)(m_computePercentage * 0.2) + (int)(m_downloadPercentage * 0.6);
      }
    }

    void OnComputeFailed()
    {
      MsgBox.Show(Local.GetValue("upload.error.failed_compute"));
    }

    void OnWorksheetSelectionChanged(Range p_range)
    {
      Account l_account = AreaController.GetAccount(p_range);

      if (l_account != null)
        m_controller.DisplayAccountSP(l_account);
    }

    delegate void SetEditedFactsStatus_delegate();
    protected override void SetEditedFactsStatus()
    {
      if (m_progressBar.InvokeRequired)
      {
        SetEditedFactsStatus_delegate func = new SetEditedFactsStatus_delegate(SetEditedFactsStatus);
        m_progressBar.Invoke(func);
      }
      else
      {
        int l_count = 0;
        int l_nbFacts = m_controller.EditedFactModel.EditedFacts.Count;
        foreach (EditedFactBase l_editedFact in m_controller.EditedFactModel.EditedFacts.Values)
        {
          m_rangeHighlighter.FillCellColor(l_editedFact.Cell, l_editedFact.SetFactValueStatus());
          if (l_count % 100 == 0)
            m_progressBar.Value = (int)((l_count / (double)l_nbFacts * 100) * 0.2 + m_computePercentage * 0.2 + m_downloadPercentage * 0.6);
          l_count++;
        }
        m_computePercentage = 0;
        m_downloadPercentage = 0;
      }
    }

    #region Model callbacks

    private void OnCommitResult(ErrorMessage p_status, CRUDAction p_action, SafeDictionary<string, Tuple<UInt32, ErrorMessage>> p_resultDic)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;
      if (p_status == ErrorMessage.SUCCESS)
      {
        foreach (KeyValuePair<string, Tuple<UInt32, ErrorMessage>> l_pair in p_resultDic)
        {
          if (l_pair.Value.Item2 != ErrorMessage.SUCCESS)
            continue;
          EditedFinancialFact l_fact = m_model.EditedFacts[l_pair.Key];

          if (l_fact != null)
            m_rangeHighlighter.FillCellGreen(l_fact.Cell);
        }
      }
      else
        MsgBox.Show(Local.GetValue("upload.error.commit_failed") + ": " + Error.GetMessage(p_status));
      m_controller.EditedFactModel.m_nbRequest--;
      AddinModuleController.SetExcelInteractionState(m_controller.EditedFactModel.m_nbRequest <= 0);
    }

    protected override void OnFactsDownloaded(bool p_success)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;
      if (m_init == false)
        foreach (EditedFinancialFact l_fact in m_model.OutputFacts.Values)
          m_rangeHighlighter.FillCellColor(l_fact.Cell, EditedFactStatus.OutputEqual);
      base.OnFactsDownloaded(p_success);
      m_controller.EditedFactModel.m_nbRequest--;
      AddinModuleController.SetExcelInteractionState(m_controller.EditedFactModel.m_nbRequest <= 0);
    }

    #endregion
  }
}
