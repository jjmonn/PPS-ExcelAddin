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
    Account m_RHAccount;
    Dimensions m_dimensions;
    WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    Worksheet m_worksheet;
    List<Int32> m_periodsList;
    Version m_version;

    public FactsEditionController(Account.AccountProcess p_process, Version p_version, Worksheet p_worksheet)
    {
      m_version = p_version;
      m_worksheet = p_worksheet;
      m_dimensions = new Dimensions(m_version.Id);
      m_process = p_process;
     
      if (p_process == Account.AccountProcess.FINANCIAL)
        m_editedFactsManager = new FinancialEditedFactsManager();
      else
        m_editedFactsManager = new RHEditedFactsManager();

      EventsSubsription();
      Launch();
    }

    private void EventsSubsription()
    {
      m_editedFactsManager.FactsDownloaded += OnFactsDownloaded;
   
      // attention cet event sera lancé après chaque calcul pour le financier (outputs) -> créer un autre event
      // TO DO : recreate ribbons !

    }

      // TO DO : implement events from worksheet !

    private bool Launch()
    {
      // Before = sidepane -> ask to choose a range
      // TO DO : RH process ->
     //   take RH Account as param
  
      // TO DO: Clean current status, higlights and so on
      // Check that a version is selected first

      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet.Cells) == true)
      {
        m_worksheetAnalyzer.Snapshot(m_dimensions);
        m_dimensions.DefineOrientation(m_process);
        if (m_dimensions.IsValid() == false)
          return false;

        m_editedFactsManager.RegisterEditedFacts(m_dimensions, m_worksheet);
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
      
    public void UpdateWorksheetInputs()
    {
      m_editedFactsManager.UpdateWorksheetInputs();
    }

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
      m_editedFactsManager.IdentifyDifferences();
    }

    
  }
}
