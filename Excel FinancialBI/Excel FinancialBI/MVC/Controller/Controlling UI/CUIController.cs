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
  using Network;
  using Properties;

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
      this.m_view.LoadView();
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

    public void Compute()
    {
      ComputeConfig l_config = new ComputeConfig();
      ComputeRequest l_request = new ComputeRequest();
      Version l_version = VersionModel.Instance.GetValue(12);

      l_request.StartPeriod = (Int32)l_version.StartPeriod;
      l_request.NbPeriods = 61;//(Int32)l_version.NbPeriod;
      l_request.Versions.Add(l_version.Id);
      //l_request.Versions.Add(11);
      l_request.CurrencyId = Convert.ToUInt32(Settings.Default.currentCurrency);
      l_request.SortList = RightPaneController.GetSort();
      l_request.EntityId = 1;
      l_request.GlobalFactVersionId = l_version.GlobalFactVersionId;
      l_request.RateVersionId = l_version.RateVersionId;
      l_request.Process = Account.AccountProcess.RH;
      l_request.AxisHierarchy = true;

      l_config.Rows = RightPaneController.GetRows();
      l_config.Columns = RightPaneController.GetColumns();
      l_config.Request = l_request;

      ComputeModel.Instance.Compute(l_request);
      ResultController.LoadDGV(l_config);
    }
  }
}
