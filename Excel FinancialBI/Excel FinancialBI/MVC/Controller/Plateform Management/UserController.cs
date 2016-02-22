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

    public void UpdateUser(User p_user)
    {
      if (p_user != null)
        UserModel.Instance.Update(p_user);
    }

    public void DeleteUserAllowed(UInt32 p_id, UInt32 p_userId, UInt32 p_entityId)
    {
      UserAllowedEntityModel.Instance.Delete(p_id, p_userId, p_entityId);
    }

    public void CreateUserAllowed(UserAllowedEntity p_userAllowed)
    {
      if (p_userAllowed != null)
        UserAllowedEntityModel.Instance.Create(p_userAllowed);
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
