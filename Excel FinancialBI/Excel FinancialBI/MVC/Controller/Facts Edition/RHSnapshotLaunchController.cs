using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using FBI.MVC.View;
  using FBI.MVC.Model.CRUD;


  class RHSnapshotLaunchController : IController
  {
    AddinModuleController m_addinModuleController;
    RHSnapshotLaunchView m_view;
    public IView View { get {return m_view; }}
    public string Error { get; set;}
    private UInt32 m_versionId;

    public RHSnapshotLaunchController(UInt32 p_versionId)
    {
      m_versionId = p_versionId;
      m_view = new RHSnapshotLaunchView();
      m_view.SetController(this);
      LoadView();
    }

    public void LoadView()
    {
      m_view.LoadView(m_versionId);
    }

    public bool LaunchSnapshot(List<Int32> p_periodsList, UInt32 p_accountId)
    {
      // check period list
      // check p_account_id
      m_addinModuleController.LaunchSnapshot(Account.AccountProcess.RH, false);
      return true;
    }

  }
}
