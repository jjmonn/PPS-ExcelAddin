using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;

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

    private CUIController m_parentController;
    private CUI2RightPane m_view;
    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    #endregion

    #region Initialize

    public CUIRightPaneController(CUIController p_parentController)
    {
      m_parentController = p_parentController;
      m_view = new CUI2RightPane();
      m_view.SetController(this);
      LoadView();
    }

    private void LoadView()
    {
      m_view.LoadView();    
    }

    public void Update(CUIDimensionConf p_row, CUIDimensionConf p_column)
    {
      m_parentController.Compute(p_row, p_column);
    }

    #endregion
  }
}
