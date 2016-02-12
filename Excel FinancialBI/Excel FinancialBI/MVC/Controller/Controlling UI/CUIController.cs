using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;

  class CUIController : IController
  {

    #region Variables

    private ControllingUI_2 m_view;

    public CUILeftPaneController LeftPaneController { get; set; }
    public CUIRightPaneController RightPaneController { get; set; }

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
      LeftPaneController = new CUILeftPaneController();
      RightPaneController = new CUIRightPaneController();
    }

    #endregion

  }
}
