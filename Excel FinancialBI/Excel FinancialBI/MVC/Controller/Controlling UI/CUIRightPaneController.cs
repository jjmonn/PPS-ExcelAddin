using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;

  enum CUIDimension
  {
    DAY,
    WEEK,
    MONTH,
    YEAR,
    CLIENT,
    PRODUCT,
    EMPLOYEE,
    ENTITY,
    FILTER_VALUE, 
    ACCOUNT
  }

  class CUIRightPaneController : IController
  {
    #region Variables

    private CUI2RightPane m_view;
    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    #endregion

    #region Initialize

    public CUIRightPaneController()
    {
      m_view = new CUI2RightPane();
      m_view.SetController(this);
      LoadView();
    }

    private void LoadView()
    {
      m_view.LoadView();    
    }

    #endregion
  }
}
