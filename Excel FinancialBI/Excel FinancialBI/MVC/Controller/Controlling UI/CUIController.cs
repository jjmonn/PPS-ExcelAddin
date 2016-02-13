using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;
  using Model;

  class CUIController : IController
  {
    #region Variables

    private ControllingUI_2 m_view;

    public CUILeftPaneController LeftPaneController { get; set; }
    public CUIRightPaneController RightPaneController { get; set; }
    public ResultController ResultController { get; set; }

    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    #endregion

    #region Initialize

    public CUIController()
    {
      this.m_view = new ControllingUI_2();
      this.m_view.SetController(this);
      this.LoadView();
    }

    private void LoadView()
    {
      this.m_view.InitView();
    }

    #endregion

    #region Pane

    public void CreatePane()
    {
      LeftPaneController = new CUILeftPaneController(this);
      RightPaneController = new CUIRightPaneController(this);
      ResultController = new ResultController(this);
    }

    #endregion

    public void Compute(CUIDimensionConf p_rows, CUIDimensionConf p_columns)
    {
      ComputeConfig l_config = new ComputeConfig();

      l_config.Request = new ComputeRequest();

      l_config.Request.StartPeriod = (Int32)VersionModel.Instance.GetValue(Convert.ToUInt32(Properties.Settings.Default.version_id)).StartPeriod;
      l_config.Request.NbPeriods = (Int32)VersionModel.Instance.GetValue(Convert.ToUInt32(Properties.Settings.Default.version_id)).NbPeriod;
      l_config.Request.VersionId = Convert.ToUInt32(Properties.Settings.Default.version_id);
      l_config.Rows = p_rows;
      l_config.Columns = p_columns;
      ResultController.LoadDGV(l_config);
    }
  }
}
