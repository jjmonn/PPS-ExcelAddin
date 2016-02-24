using System;
//using System.Collections.Generic;
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

  class AddinModuleController
  {
    private AddinModule m_view;
    private FactsEditionController m_factsEditionController;
    private ReportUploadController m_reportUploadController;
    public string Error { get; set; }
    // SidePanesControllers

    public AddinModuleController(AddinModule p_view)
    {
      m_view = p_view;
    }

    public bool LaunchRHSnapshotView()
    {
      UInt32 l_versionId = GetCurrentVersion();
      if (l_versionId != 0)
      {
        RHSnapshotLaunchController l_RHSnapshotController = new RHSnapshotLaunchController(l_versionId);
        return true;
      }
      else
        return false;
    }

    public bool LaunchSnapshot(Account.AccountProcess p_process, bool p_updateCells)
    {
      UInt32 l_versionId = GetCurrentVersion();
      if (l_versionId != 0)
      {
        m_factsEditionController = new FactsEditionController(p_process, l_versionId, m_view.ExcelApp.ActiveSheet as Worksheet, p_updateCells);
        return true;
      }
      else
        return false;
    }

    private UInt32 GetCurrentVersion()
    {
      Version l_version = VersionModel.Instance.GetValue(FBI.Properties.Settings.Default.version_id);
      if (l_version == null)
      {
        Error = Local.GetValue("versions.select_version");
        return 0;
      }
      return l_version.Id;  
    }

  }
}
