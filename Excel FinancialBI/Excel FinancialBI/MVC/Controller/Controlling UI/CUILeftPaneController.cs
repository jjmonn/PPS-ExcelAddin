using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;

  class CUILeftPaneController : IController
  {

    #region Variables

    private CUIController m_parentController;
    private CUI2LeftPane m_view;
    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    #endregion

    #region Initialize

    public CUILeftPaneController(CUIController p_parentController)
    {
      m_parentController = p_parentController;
      this.m_view = new CUI2LeftPane();
      this.m_view.SetController(this);
      this.LoadView();
    }

    private void LoadView()
    {
      this.m_view.InitView();
    }

    #endregion

  }
}
