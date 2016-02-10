using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Forms;
  using Model;
  using View;
  using Model.CRUD;

  class UserController : NameController<UsersView>
  {

    #region Variables

    public override IView View { get { return (m_view); } }

    #endregion

    #region Initialize

    public UserController()
    {
      m_view = new UsersView();
      m_view.SetController(this);
      this.LoadView();
    }

    public override void LoadView()
    {
      m_view.InitView();
    }

    #endregion

  }
}
