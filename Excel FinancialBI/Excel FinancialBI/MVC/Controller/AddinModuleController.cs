using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FBI.MVC.Controller
{    
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using FBI.MVC.View;
  using Microsoft.Office.Interop.Excel;
  using Utils;

  public class AddinModuleController
  {
    private AddinModule m_view;
    private FactsEditionController m_factsEditionController;
  
   // private ReportEditionController m_reportUploadController;
    public string Error { get; set; }
    // SidePanesControllers

    public AddinModuleController(AddinModule p_view)
    {
      m_view = p_view;
    }

    public bool LaunchRHSnapshotView()
    {
      Version l_version = GetCurrentVersion();
      if (l_version != null)
      {
        RHSnapshotLaunchController l_RHSnapshotController = new RHSnapshotLaunchController(l_version.Id, this);
        return true;
      }
      else
        return false;
    }

    public bool LaunchFinancialSnapshot(bool p_updateCells)
    {
      Version l_version = GetCurrentVersion();
      if (l_version != null)
      {
        m_factsEditionController = new FactsEditionController(this, Account.AccountProcess.FINANCIAL, l_version.Id, m_view.ExcelApp.ActiveSheet as Worksheet, null, 0);
        return m_factsEditionController.Launch(p_updateCells, true);
      }
      else
        return false;
    }

    public bool LaunchRHSnapshot(bool p_updateCells, UInt32 p_versionId,  bool p_displayInitialDifferences, List<Int32> p_periodsList = null, UInt32 p_RHAccount = 0)
    {
        m_factsEditionController = new FactsEditionController(this, Account.AccountProcess.RH, p_versionId, m_view.ExcelApp.ActiveSheet as Worksheet, p_periodsList, p_RHAccount);
        return m_factsEditionController.Launch(p_updateCells, p_displayInitialDifferences);
    }

    public bool LaunchReportEdition()
    {
      Account.AccountProcess l_process = (Account.AccountProcess)FBI.Properties.Settings.Default.processId;
      Version l_version = GetCurrentVersion();
      
      if (l_version != null)
      {
        ReportEditionController l_reportEditionController = new ReportEditionController(l_process, l_version, this, m_view.ReportUploadEntitySelectionSidePane);
        return true;
      }
      else
      {
        Error = Local.GetValue("general.error.no_version_selected");
        return false;
      }
    }

    public bool AssociateExcelWorksheetEvents(Worksheet p_worksheet)
    {
      if (p_worksheet != null && m_factsEditionController != null)
      {
        m_view.WorksheetEvents.ConnectTo(p_worksheet, true);
        m_view.WorksheetEvents.SetController(m_factsEditionController);
        return true;
      }
      else
        m_view.WorksheetEvents.RemoveConnection();
      return false;
     }

    public bool RHFactsSubmission()
    {
      if (m_factsEditionController == null)
      {
        Error = Local.GetValue("upload.edition_ended");
        m_view.m_RHSubmissionRibbon.Visible = false;
        return false;
      }
      m_factsEditionController.CommitFacts();
      return true;
    }

    public static void SetExcelInteractionState(bool p_state)
    {
      AddinModule.CurrentInstance.ExcelApp.Interactive = p_state;
      System.Diagnostics.Debug.WriteLine("Excel interaction set to " + p_state.ToString());
      //AddinModule.CurrentInstance.ExcelApp.ScreenUpdating = p_state; 
    }

    public void CloseEditionMode()
    {
      if (m_factsEditionController != null)
      {
        m_view.WorksheetEvents.RemoveConnection();
        m_factsEditionController.CloseFactsEdition();
        m_factsEditionController = null;
      }
    }

    #region Utils

    private Version GetCurrentVersion()
    {
      Version l_version = VersionModel.Instance.GetValue(FBI.Properties.Settings.Default.version_id);
      if (l_version == null)
      {
        Error = Local.GetValue("versions.select_version");
        return null;
      }
      return l_version;
    }

    #endregion
  }
}
