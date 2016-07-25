using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using FBI.MVC.View;
  using FBI.MVC.Model.CRUD;
  using FBI.MVC.Model;


  class RHSnapshotLaunchController : IController
  {
    AddinModuleController m_addinModuleController;
    RHSnapshotLaunchView m_view;
    public IView View { get {return m_view; }}
    public string Error { get; set;}
    private UInt32 m_versionId;

    public RHSnapshotLaunchController(UInt32 p_versionId, AddinModuleController p_addinModuleController)
    {
      m_versionId = p_versionId;
      m_addinModuleController = p_addinModuleController;
      m_view = new RHSnapshotLaunchView();
      m_view.SetController(this);
      m_view.LoadView(m_versionId);
      m_view.Show();
    }

    public bool LaunchSnapshot(List<Int32> p_periodsList, UInt32 p_accountId, bool p_displayInitialDifferences)
    {
      // TO DO
      // check period list
      // check p_account_id
    
      if (Properties.Settings.Default.includeWeekEnds == false)
          p_periodsList = PeriodModel.FilterWeekEnds(p_periodsList);
        
      m_view.Hide();
      if (m_addinModuleController.LaunchRHSnapshot(false, false, m_versionId, p_displayInitialDifferences, p_periodsList, p_accountId))
      {
        m_view.Close();
        return true;
      }
      else
      {
        // TO DO : display addinmodule error message
        return false;
      }
    }

  }
}
