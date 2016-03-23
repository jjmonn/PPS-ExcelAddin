using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using FBI.MVC.View;
  using Utils;
  using VIBlend.WinForms.Controls;
  using Network;

  public class FactsEditionController
  {
    private AddinModuleController m_addinModuleController;
    private IEditedFactsModel m_editedFactsManager;
 //   private ExcelWorksheetEvents m_ExcelSheetEvents;
    private RangeHighlighter m_rangeHighlighter;
    private Account.AccountProcess m_process;
    private UInt32 m_RHAccountId;
    private WorksheetAreaController m_areaController;
    private WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    private Worksheet m_worksheet;
    private List<Int32> m_periodsList;
    private UInt32 m_versionId;
    private bool m_autoCommit;
 

    public FactsEditionController(AddinModuleController p_addinModuleController, Account.AccountProcess p_process, UInt32 p_versionId, Worksheet p_worksheet, List<Int32> p_periodsList = null, UInt32 p_RHAccountId = 0)
    {
      m_addinModuleController = p_addinModuleController;
      m_versionId = p_versionId;
      m_worksheet = p_worksheet;
      m_areaController = new WorksheetAreaController(m_versionId, p_periodsList);
      m_rangeHighlighter = new RangeHighlighter(this);
      m_process = p_process;
      m_periodsList = p_periodsList;
      m_RHAccountId = p_RHAccountId;

      if (p_process == Account.AccountProcess.FINANCIAL)
        m_editedFactsManager = new FinancialEditedFactsModel();
      else
        m_editedFactsManager = new RHEditedFactsModel(p_periodsList);

      m_editedFactsManager.FactsDownloaded += OnFactsDownloaded;
   //   m_editedFactsManager.OnCommitError -= OnCommitError;
    }

    public bool Launch(bool p_updateCells, bool p_displayInitialDifferences)
    {
      // TO DO: Compatibilité resfresh
      // Clean current status, higlights and so on

      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet.Cells) == true)
      {
        m_worksheetAnalyzer.Snapshot(m_areaController);
        m_areaController.DefineOrientation(m_process);
        if (m_areaController.IsValid() == false)
          return false;

        m_editedFactsManager.RegisterEditedFacts(m_areaController, m_worksheet, m_versionId, m_rangeHighlighter, p_displayInitialDifferences, m_RHAccountId);
        if (m_versionId != 0 && m_periodsList.Count > 0)
        {
          OpenFactsEdition(p_updateCells);
          return true;
        }
        else
          return false;
      }
      return false;
    }

    private void OpenFactsEdition(bool p_updateCells)
    {
      m_editedFactsManager.DownloadFacts(m_periodsList, p_updateCells);
      ActivateFactEditionRibbon();
      AddinModule.CurrentInstance.ExcelApp.CellDragAndDrop = false;
      // TO DO : subsribe to commit error event ?
      m_worksheetAnalyzer = null;
    }

    private void ActivateFactEditionRibbon()
    {
      if (m_process == Account.AccountProcess.FINANCIAL)
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
  
    public void CloseFactsEdition()
    {
      m_editedFactsManager.FactsDownloaded -= OnFactsDownloaded;
      m_editedFactsManager.UnsubsribeEvents();
    
      //  TO DO : m_editedFactsManager.OnCommitError -= OnCommitError;

      m_rangeHighlighter.RevertToOriginalColors();
      m_editedFactsManager = null;
      m_rangeHighlighter = null;
      m_areaController = null;

      AddinModule.CurrentInstance.ExcelApp.CellDragAndDrop = true;
      AddinModule.CurrentInstance.m_RHSubmissionRibbon.Visible = false;
      AddinModule.CurrentInstance.m_financialSubmissionRibbon.Visible = false;
    }
 
    public void UpdateWorksheetOutputs()
    {
      ((FinancialEditedFactsModel)m_editedFactsManager).UpdateWorkSheetOutputs();
    }

    public void OnWorksheetChange(Range p_cell)
    {
      if (m_editedFactsManager.UpdateEditedValueAndTag(p_cell))
        return;


      // if financial -> launch compute at the end of the cells range loop
      // financial -> dependant cells

      // if cell belongs to dimension
      //   -> cancel modification and put back the dimension value

      // if cell belongs to output
      //   -> cancel modification and put back the output value 

      // TO DO  : antiduplicate system (au préalable -> comparaison des strings)

      if (m_autoCommit == true)
        m_editedFactsManager.Commit();

    }

    public void BeforeRightClick()
    {
      // TO DO
    }

    public void CommitFacts()
    {
      m_editedFactsManager.Commit();
    }

    public void SetAutoCommit(bool p_value)
    {
      m_autoCommit = p_value; 
    }

    private void OnFactsDownloaded(bool p_success)
    {
      if (p_success == true)
        m_addinModuleController.AssociateExcelWorksheetEvents(m_worksheet);
      else
      {
        // exit mode
      }
    }

    private void OnCommitError(string p_address, ErrorMessage p_error)
    {
       // TO DO
      // write in commit log
    }
   
    // Commit failed as whole method -> view message box

  }
}
