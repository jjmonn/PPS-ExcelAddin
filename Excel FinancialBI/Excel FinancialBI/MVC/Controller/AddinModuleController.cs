using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{    
  using Model;
  using Model.CRUD;
  using View;
  using Utils;

  public class AddinModuleController
  {
    private AddinModule m_view;
    private IFactEditionController m_factsEditionController;
  
   // private ReportEditionController m_reportUploadController;
    public string Error { get; set; }
    // SidePanesControllers

    private UInt32 m_submissionClientId;
    private UInt32 m_submissionProductId;
    private UInt32 m_submissionAdjustmentId;
    public UInt32 SubmissionClientId
    {
      get { return (m_submissionClientId); }
      set { m_view.SubmissionClientId = (m_submissionClientId = value); }
    }
    public UInt32 SubmissionProductId
    {
      get { return (m_submissionProductId); }
      set { m_view.SubmissionProductId = (m_submissionProductId = value); }
    }
    public UInt32 SubmissionAdjustmentId
    {
      get { return (m_submissionAdjustmentId); }
      set { m_view.SubmissionAdjustmentId = (m_submissionAdjustmentId = value); }
    }

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

    public bool LaunchFinancialSnapshot(bool p_updateCells, UInt32 p_versionId)
    {
        m_view.InitFinancialSubmissionRibon();
        FinancialFactEditionController l_editionController = new FinancialFactEditionController(this, p_versionId, m_view.ExcelApp.ActiveSheet as Worksheet);
        m_factsEditionController = l_editionController;
        bool l_result = m_factsEditionController.Launch(p_updateCells, true, SubmissionClientId, 
          SubmissionProductId, SubmissionAdjustmentId);
        if (l_result)
          m_view.SubmissionVersionName = VersionModel.Instance.GetValueName(l_editionController.AreaController.VersionId);
        return (l_result);
    }

    public bool LaunchRHSnapshot(bool p_updateCells, UInt32 p_versionId, bool p_displayInitialDifferences, List<Int32> p_periodsList = null, UInt32 p_RHAccount = 0)
    {
      RHFactEditionController l_editionController = new RHFactEditionController(this, p_versionId, m_view.ExcelApp.ActiveSheet as Worksheet, p_periodsList, p_RHAccount);
      m_factsEditionController = l_editionController;
      bool l_result = m_factsEditionController.Launch(p_updateCells, p_displayInitialDifferences, 0, 0, 0);
      if (l_result)
        m_view.SubmissionVersionName = VersionModel.Instance.GetValueName(l_editionController.AreaController.VersionId);
      return (l_result);
    }

    public bool LaunchReportEdition()
    {
      Account.AccountProcess l_process = (Account.AccountProcess)FBI.Properties.Settings.Default.processId;
      Version l_version = GetCurrentVersion();

      SubmissionClientId = (UInt32)AxisType.Client;
      SubmissionProductId = (UInt32)AxisType.Product;
      SubmissionAdjustmentId = (UInt32)AxisType.Adjustment;

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

    public bool FactsSubmission()
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

    public void RefreshInputs()
    {
      if (m_factsEditionController != null)
      {
        if (m_factsEditionController.GetType() == typeof(FinancialFactEditionController))
        {
          FinancialFactEditionController l_controller = m_factsEditionController as FinancialFactEditionController;

          l_controller.DownloadFacts(true, SubmissionClientId, SubmissionProductId, SubmissionAdjustmentId);
        }
        else if (m_factsEditionController.GetType() == typeof(RHFactEditionController))
        {
          RHFactEditionController l_controller = m_factsEditionController as RHFactEditionController;

          l_controller.DownloadFacts(true, 0, 0, 0);
        }
      }
    }

    public static void SetExcelInteractionState(bool p_state)
    {
      AddinModule.CurrentInstance.ExcelApp.Interactive = p_state;
      System.Diagnostics.Debug.WriteLine("Excel interaction set to " + p_state.ToString());
    }

    public void CloseEditionMode()
    {
      if (m_factsEditionController != null)
      {
        m_view.WorksheetEvents.RemoveConnection();
        m_factsEditionController.Close();
        m_factsEditionController = null;
      }
    }

    #region Utils

    public Version GetCurrentVersion()
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
