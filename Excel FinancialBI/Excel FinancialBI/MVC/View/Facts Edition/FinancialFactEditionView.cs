﻿using System;
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
      m_controller.WorksheetSelectionChanged += OnWorksheetSelectionChanged;
    }

    void OnWorksheetSelectionChanged(Range p_range)
    {
      Account l_account = m_areaController.GetAccount(p_range);

      if (l_account != null)
        m_controller.DisplayAccountSP(l_account);
    }

    protected override void SetEditedFactsStatus()
    {
      AddinModuleController.SetExcelInteractionState(false);
      foreach (EditedFactBase l_editedFact in m_controller.EditedFactModel.EditedFacts.Values)
        m_rangeHighlighter.FillCellColor(l_editedFact.Cell, l_editedFact.SetFactValueStatus());
      AddinModuleController.SetExcelInteractionState(true);
    }

    #region Model callbacks

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
            m_rangeHighlighter.FillCellGreen(l_fact.Cell);
        }
      }
      else
        MsgBox.Show(Local.GetValue("upload.error.commit_failed") + ": " + Error.GetMessage(p_status));
    }

    #endregion
  }
}
