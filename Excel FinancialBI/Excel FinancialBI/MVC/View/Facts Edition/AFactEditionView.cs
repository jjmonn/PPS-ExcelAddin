﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Forms;
  using Utils;

  interface IFactEditionView
  {
    void Close();
    void OpenFactsEdition(bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);
    string Launch(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);
    void Reload(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);
    WorksheetAreaController AreaController { get; set; }
  }

  abstract class AFactEditionView<TModel, TController> : IFactEditionView where TModel : AEditedFactsModel
    where TController : AFactEditionController<TModel>
  {
    public bool IsEditingExcel { get; private set; }
    protected Worksheet m_worksheet;
    protected TController m_controller;
    protected TModel m_model;
    protected RangeHighlighter m_rangeHighlighter;
    protected WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    public WorksheetAreaController AreaController { get; set; }
    protected bool m_init = false;
    protected PBarUI m_progressBar;

    public AFactEditionView(TController p_controller, Worksheet p_worksheet)
    {
      m_rangeHighlighter = new RangeHighlighter(p_worksheet);
      m_controller = p_controller;
      m_worksheet = p_worksheet;
      m_model = m_controller.EditedFactModel;
      m_progressBar = new PBarUI(Local.GetValue("general.loading"), 100);
      AreaController = new WorksheetAreaController(m_controller.Process, m_controller.VersionId, p_worksheet, m_controller.PeriodsList);
    }

    public virtual void LoadView()
    {
      SuscribeEvents();
    }

    protected virtual void SuscribeEvents()
    {
      m_controller.WorksheetChanging += OnWorksheetChanging;
      m_controller.WorksheetChanged += OnWorksheetChanged;
      m_model.FactsDownloaded += OnFactsDownloaded;
      Addin.ConnectionStateEvent += OnServerConnectionChanged;
    }

    public virtual void Close()
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet))
        m_rangeHighlighter.RevertToOriginalColors();
      AreaController.ClearDimensions();
      Addin.ConnectionStateEvent -= OnServerConnectionChanged;
      m_model.FactsDownloaded -= OnFactsDownloaded;

      AddinModule.CurrentInstance.ExcelApp.CellDragAndDrop = true;
      AddinModule.CurrentInstance.m_RHSubmissionRibbon.Visible = false;
      AddinModule.CurrentInstance.m_financialSubmissionRibbon.Visible = false;
    }

    protected abstract void SetEditedFactsStatus();

    public void OpenFactsEdition(bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      m_progressBar.Value = 0;
      m_progressBar.Show();
      m_controller.DownloadFacts(p_updateCells, p_clientId, p_productId, p_adjustmentId);
      ActivateFactEditionRibbon();
      AddinModule.CurrentInstance.ExcelApp.CellDragAndDrop = false;
      m_rangeHighlighter.FillDimensionColor(AreaController);
    }

    public void Reload(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      m_progressBar.Value = 0;
      m_progressBar.Show();
      m_controller.EditedFactModel.RegisterEditedFacts(AreaController, m_controller.VersionId, p_displayInitialDifferences, m_controller.RHAccountId);
      m_controller.DownloadFacts(p_updateCells, p_clientId, p_productId, p_adjustmentId);
    }

    public string Launch(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return Local.GetValue("upload.msg_error_worksheet_closed");
      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet.Cells) == true)
      {
        m_worksheetAnalyzer.Snapshot(AreaController);
        AreaController.DefineOrientation(m_controller.Process);
        m_controller.VersionId = AreaController.VersionId;
        if (AreaController.Entities.m_values == null || AreaController.Entities.m_values.Count != 1)
          m_controller.EntityId = 0;
        else
          m_controller.EntityId = AreaController.Entities.m_values.First().Value.Id;
        if (AreaController.IsValid() == false)
          return AreaController.GetDimensionError();
        m_controller.EditedFactModel.RegisterEditedFacts(AreaController, m_controller.VersionId, p_displayInitialDifferences, m_controller.RHAccountId);
        if (m_controller.VersionId != 0 && (m_controller.PeriodsList == null || m_controller.PeriodsList.Count > 0))
        {
          OpenFactsEdition(p_updateCells, p_clientId, p_productId, p_adjustmentId);
          return ("");
        }
        else
          return Local.GetValue("upload.msg_error_upload");
      }
      return m_worksheetAnalyzer.Error;
    }

    private void ActivateFactEditionRibbon()
    {
      if (m_controller.Process == Account.AccountProcess.FINANCIAL)
      {
        AddinModule.CurrentInstance.m_financialSubmissionRibbon.Visible = true;
        AddinModule.CurrentInstance.m_financialSubmissionRibbon.Activate();
      }
      else
      {
        AddinModule.CurrentInstance.m_RHSubmissionRibbon.Visible = true;
        AddinModule.CurrentInstance.m_RHSubmissionRibbon.Activate();
      }
    }

    #region User callbacks

    public void OnWorksheetChanging(Range p_cell)
    {
      if (IsEditingExcel)
        return;

      EditedFactBase l_fact = m_controller.EditedFactModel.UpdateEditedValueAndTag(p_cell);
      if (l_fact != null)
      {
        m_rangeHighlighter.FillCellColor(l_fact.Cell, l_fact.SetFactValueStatus());
        if (m_controller.AutoCommit)
          m_controller.CommitFacts();
        return;
      }

      IsEditingExcel = true;
      string l_result = AreaController.CellBelongsToDimension(p_cell);
      if (l_result != "")
        p_cell.Value2 = l_result;
      double? l_result2 = m_model.CellBelongToOutput(p_cell);
      if (l_result2 == null)
        l_result2 = m_model.CellBelongToInput(p_cell); 
      if (l_result2 != null)
      {
        try
        {
          if (p_cell.Value2 == null || (double)p_cell.Value2 != l_result2)
            p_cell.Value2 = l_result2;
        }
        catch
        {
          p_cell.Value2 = l_result2;
        }
      }
      IsEditingExcel = false;
    }

    public void OnWorksheetChanged()
    {
      m_model.Refresh();
    }

    #endregion

    #region Model callbacks

    void OnServerConnectionChanged(bool p_connected)
    {
      if (p_connected == false)
      {
        AddinModuleController.SetExcelInteractionState(true);
        MsgBox.Show(Local.GetValue("general.error.server_disconnected"));
      }
    }

    delegate void OnFactsDownloaded_delegate(bool p_success);
    protected virtual void OnFactsDownloaded(bool p_success)
    {
      if (m_progressBar.InvokeRequired)
      {
        OnFactsDownloaded_delegate func = new OnFactsDownloaded_delegate(OnFactsDownloaded);
        m_progressBar.Invoke(func, p_success);
      }
      else
      {
        if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
          return;
        if (p_success == true && m_init == false)
        {
          m_init = true;
          m_controller.AddinController.AssociateExcelWorksheetEvents(m_worksheet);
        }
        SetEditedFactsStatus();
        m_progressBar.Hide();
        AddinModule.CurrentInstance.ExcelApp.Visible = true;
      }
    }

    #endregion
  }
}
