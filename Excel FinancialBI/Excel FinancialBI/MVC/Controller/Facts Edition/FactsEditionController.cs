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
  using Utils;
  using VIBlend.WinForms.Controls;

  public class FactsEditionController
  {
    IEditedFactsManager m_editedFactsManager;
    Account.AccountProcess m_process;
    UInt32 m_RHAccountId;
    Dimensions m_dimensions;
    WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    Worksheet m_worksheet;
    List<Int32> m_periodsList;
    UInt32 m_versionId;

    public FactsEditionController(Account.AccountProcess p_process, UInt32 p_versionId, Worksheet p_worksheet, bool p_updateCells, List<Int32> p_periodsList = null, UInt32 p_RHAccountId = 0)
    {
      m_versionId = p_versionId;
      m_worksheet = p_worksheet;
      m_dimensions = new Dimensions(m_versionId, p_periodsList);
      m_process = p_process;
      m_periodsList = p_periodsList;
      m_RHAccountId = p_RHAccountId;

      if (p_process == Account.AccountProcess.FINANCIAL)
        m_editedFactsManager = new FinancialEditedFactsManager();
      else
        m_editedFactsManager = new RHEditedFactsManager();

      EventsSubsription();
      Launch(p_updateCells);
    }

    private void EventsSubsription()
    {
      m_editedFactsManager.FactsDownloaded += OnFactsDownloaded;
   
      // attention cet event sera lancé après chaque calcul pour le financier (outputs) -> créer un autre event
      // TO DO : recreate ribbons !

    }

      // TO DO : implement events from worksheet !

    private bool Launch(bool p_updateCells)
    {
      // TO DO: Compatibilité resfresh
      // Clean current status, higlights and so on
      // Check that a version is selected first

      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet.Cells) == true)
      {
        m_worksheetAnalyzer.Snapshot(m_dimensions);
        m_dimensions.DefineOrientation(m_process);
        if (m_dimensions.IsValid() == false)
          return false;

        m_editedFactsManager.RegisterEditedFacts(m_dimensions, m_worksheet, m_versionId, m_RHAccountId);
        if (m_versionId != 0 && m_periodsList.Count > 0)
        {
          m_editedFactsManager.DownloadFacts(m_versionId, m_periodsList, p_updateCells);
          return true;
        }
        else
          return false;
      }
      return false;
    }

    private void OnFactsEditionInitialized()
    {
        // show corresponding ribbon
        if (m_process == Account.AccountProcess.FINANCIAL)
        {
            AddinModule.CurrentInstance.m_financialbiRibbon.Visible = true;
            // TO DO subsribe to events
        }
        else
        {
           // AddinModule.CurrentInstance.m_RHSubmissionRibbon.Visible = true;
           // subsribe to events
        }
        
    }

    public void CloseInstance()
    {
        // TO DO Hide corresponding ribbon
        // unsuscribe from event
        // close current instances
        // find a way to close this instance

    }
      
    //public void UpdateWorksheetInputs()
    //{
    //  m_editedFactsManager.UpdateWorksheetInputs();
    //}

    public void UpdateWorksheetOutputs()
    {
      ((FinancialEditedFactsManager)m_editedFactsManager).UpdateWorkSheetOutputs();
    }

    private void OnWorksheetChange()
    {
      // TO DO

      // if cell belongs to edited fact
      //   -> update edited fact status ()
      //      if financial -> launch compute at the end of the cells range loop
      
      // if cell belongs to dimension
      //   -> cancel modification and put back the dimension value

      // if cell belongs to output
      //   -> cancel modification and put back the output value 

      // TO DO  : antiduplicate system (au préalable -> comparaison des strings)
   

    }

    private void BeforeRightClick()
    {
      // TO DO
    }

    public void CommitFacts()
    {
      m_editedFactsManager.CommitDifferences();
    }

    public void SetAutoCommit(bool p_value)
    {
      m_editedFactsManager.m_autoCommit = p_value; 
    }

    private void OnFactsDownloaded(bool p_sucess)
    {
      // Attention : below : directement géré dans les FactsEditionManager ?
    }

    
  }
}
