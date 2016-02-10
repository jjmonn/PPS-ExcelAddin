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
    IEditedFactsManager m_editedFactsManager ;
    Account.AccountProcess m_process;
    Dimensions m_dimensions = new Dimensions();
    WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    Worksheet m_worksheet;
    List<UInt32> m_periodsList;
    Version m_version;

    public FactsEditionController(Account.AccountProcess p_process)
    {
      m_process = p_process;
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
      m_worksheet = p_worksheet;

      // TO DO: Clean current status, higlights and so on

      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet) == true)
      {
        m_worksheetAnalyzer.Snapshot(m_dimensions);
        if (m_dimensions.IsValid() == false)
          return false;

        // TO DO : register Editedfact


        switch (m_dimensions.m_process)
        {
          case Account.AccountProcess.FINANCIAL: 
            FinancialFactsDownload(); 
            break;
          case Account.AccountProcess.RH: 
            RHFactsDownload(); 
            break;
        }
        return true;
      }
      return false;
    }

    private void FinancialFactsDownload()
    {
      // TO DO
    }

    private void RHFactsDownload()
    {
      if (m_version != null && m_periodsList.Count > 0)
        m_editedFactsManager.DownloadFacts(m_version, m_periodsList);
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
