using System;
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

  interface IFactEditionView
  {
    void Close();
    void OpenFactsEdition(bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);
  }

  abstract class AFactEditionView<TModel, TController> : IFactEditionView where TModel : AEditedFactsModel
    where TController : AFactEditionController<TModel>
  {
    public bool IsEditingExcel { get; private set; }
    protected RangeHighlighter m_rangeHighlighter;
    protected Worksheet m_worksheet;
    protected TController m_controller;
    private bool m_autoCommit;

    public AFactEditionView(TController p_controller, Worksheet p_worksheet)
    {
      m_controller = p_controller;
      m_worksheet = p_worksheet;
      m_rangeHighlighter = new RangeHighlighter(m_worksheet);
    }

    protected virtual void SuscribeEvents()
    {
      m_controller.WorksheetChanging += OnWorksheetChanging;
      m_controller.WorksheetChanged += OnWorksheetChanged;
    }

    public void OpenFactsEdition(bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      m_controller.DownloadFacts(p_updateCells, p_clientId, p_productId, p_adjustmentId);
      ActivateFactEditionRibbon();
      AddinModule.CurrentInstance.ExcelApp.CellDragAndDrop = false;
      m_rangeHighlighter.FillDimensionColor(m_controller.AreaController);
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

    public void Close()
    {
      m_rangeHighlighter.RevertToOriginalColors();
      m_rangeHighlighter = null;

      AddinModule.CurrentInstance.ExcelApp.CellDragAndDrop = true;
      AddinModule.CurrentInstance.m_RHSubmissionRibbon.Visible = false;
      AddinModule.CurrentInstance.m_financialSubmissionRibbon.Visible = false;
    }

    public void SetAutoCommit(bool p_value)
    {
      m_autoCommit = p_value;
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
        return;
      }

      IsEditingExcel = true;
      string l_result = m_controller.AreaController.CellBelongsToDimension(p_cell);
      if (l_result != "")
        p_cell.Value2 = l_result;
      double? l_result2 = m_controller.EditedFactModel.CellBelongToOutput(p_cell);

      if (l_result2 != null)
        p_cell.Value2 = l_result2;

      // if cell belongs to dimension
      //   -> cancel modification and put back the dimension value

      // if financial -> launch compute at the end of the cells range loop
      // financial -> dependant cells



      // if cell belongs to output
      //   -> cancel modification and put back the output value 

      // TO DO  : antiduplicate system (au préalable -> comparaison des strings)

      if (m_autoCommit == true)
        m_controller.CommitFacts();

      IsEditingExcel = false;
    }

    public void OnWorksheetChanged()
    {
      m_controller.EditedFactModel.Refresh();
    }

    public void BeforeRightClick()
    {
      // TO DO
    }

    #endregion
  }
}
