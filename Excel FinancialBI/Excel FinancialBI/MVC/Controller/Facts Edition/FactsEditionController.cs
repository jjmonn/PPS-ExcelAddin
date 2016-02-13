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


  class FactsEditionController
  {
    IEditedFactsManager m_editedFactsManager;
    Account.AccountProcess m_process;
    Account m_RHAccount;
    Dimensions m_dimensions;
    WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    Worksheet m_worksheet;
    List<UInt32> m_periodsList;
    Version m_version;

    public FactsEditionController(Account.AccountProcess p_process, Account p_RHAccount = null, List<UInt32> p_periodList = null)
    {
      UInt32 l_versionId = FBI.Properties.Settings.Default.version_id;
      m_dimensions = new Dimensions(l_versionId);
      m_process = p_process;
      if (p_process == Account.AccountProcess.RH)
      {
        m_RHAccount = p_RHAccount;
        m_periodsList = p_periodList;
      }
      
      if (p_process == Account.AccountProcess.FINANCIAL)
        m_editedFactsManager = new FinancialEditedFactsManager();
      else
        m_editedFactsManager = new RHEditedFactsManager();

      EventsSubsription();
    }
    
    private void EventsSubsription()
    {
      m_editedFactsManager.FactsDownloaded += AfterHRFactsDownloaded;
    }

    public bool Launch(Worksheet p_worksheet)
    {
      // Before = sidepane -> ask to choose a range
      // TO DO : RH process -> take RH Account as param
      m_worksheet = p_worksheet;

      // TO DO: Clean current status, higlights and so on
      // Check that a version is selected first

      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet.Cells) == true)
      {
        m_worksheetAnalyzer.Snapshot(m_dimensions);
        m_dimensions.DefineOrientation(m_process);
        if (m_dimensions.IsValid() == false)
          return false;

        m_editedFactsManager.RegisterEditedFacts(m_dimensions);
        if (m_version != null && m_periodsList.Count > 0)
        {
          m_editedFactsManager.DownloadFacts(m_version, m_periodsList);
          return true;
        }
        else
          return false;
      }
      return false;
    }

    public void UpdateWorksheetInputs()
    {
      m_editedFactsManager.UpdateWorksheetInputs();
    }

    public void UpdateWorksheetOutputs()
    {
      ((FinancialEditedFactsManager)m_editedFactsManager).UpdateWorkSheetOutputs();
    }

    public void CommitFacts()
    {
      m_editedFactsManager.CommitDifferences();
    }

    // BELOW : to be checked
    private void AfterHRFactsDownloaded(bool p_sucess)
    {
      m_editedFactsManager.IdentifyDifferences();
     // TO DO : ?
    }

    private void AfterOutputsComputed(bool p_sucess)
    {
     UpdateWorksheetOutputs();
    }

    
  }
}
