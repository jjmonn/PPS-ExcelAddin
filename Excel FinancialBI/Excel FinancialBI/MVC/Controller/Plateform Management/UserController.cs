using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Forms;
  using Model;
  using Utils;
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
      m_view.LoadView();
    }

    #endregion

    #region Server

    public bool UpdateUser(User p_user)
    {
      if (p_user != null)
      {
        if (IsNameValid(p_user.Name) == false)
          return (false);
        if (UserModel.Instance.Update(p_user))
          return (true);
      }
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool DeleteUserAllowed(UInt32 p_id, UInt32 p_userId, UInt32 p_entityId)
    {
      if (UserAllowedEntityModel.Instance.Delete(p_id, p_userId, p_entityId))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool CreateUserAllowed(UserAllowedEntity p_userAllowed)
    {
      if (p_userAllowed != null)
        if (UserAllowedEntityModel.Instance.Create(p_userAllowed))
          return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    #endregion

    #region Check

    public bool IsAllowedEntity(UInt32 p_userId, UInt32 p_entityId)
    {
      MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> l_entitiesAllowed = UserAllowedEntityModel.Instance.GetDictionary(p_userId);

      if (l_entitiesAllowed != null)
        return (l_entitiesAllowed.ContainsSecondaryKey(p_entityId));
      return (false);
    }

    #endregion

  }
}
