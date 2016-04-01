using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;

  class UserAllowedEntityModel : ICRUDModel<UserAllowedEntity>
  {
    static UserAllowedEntityModel s_instance = new UserAllowedEntityModel();
    public static UserAllowedEntityModel Instance { get { return (s_instance); } }
    SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity>> m_userAllowedEntityDic =
      new SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity>>();

    UserAllowedEntityModel()
    {
      CreateCMSG = ClientMessage.CMSG_ADD_USER_ENTITY;
      DeleteCMSG = ClientMessage.CMSG_DEL_USER_ENTITY;
      ListCMSG = ClientMessage.CMSG_LIST_USER_ENTITIES;

      CreateSMSG = ServerMessage.SMSG_ADD_USER_ENTITY_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_USER_ENTITY;
      DeleteSMSG = ServerMessage.SMSG_DEL_USER_ENTITY_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_USER_ENTITIES_ANSWER;

      Build = UserAllowedEntity.BuildUserAllowedEntity;

      InitCallbacks();
    }

    #region CRUD

    public override bool Delete(UInt32 p_id)
    {
      return (false);
    }

    public override bool Update(UserAllowedEntity p_crud)
    {
      return (false);
    }

    public override bool UpdateList(List<UserAllowedEntity> p_crudList, CRUDAction p_action)
    {
      return (false);
    }

    public bool Delete(UInt32 p_id, UInt32 p_userId, UInt32 p_entityId)
    {
      ByteBuffer packet = new ByteBuffer((UInt16)DeleteCMSG);

      packet.WriteUint32(p_id);
      packet.WriteUint32(p_userId);
      packet.WriteUint32(p_entityId);
      packet.Release();
      return NetworkManager.Send(packet);
    }

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        UInt32 count = p_packet.ReadUint32();
        for (Int32 i = 1; i <= count; i++)
        {
          UserAllowedEntity l_allowedEntity = Build(p_packet) as UserAllowedEntity;

          if (m_userAllowedEntityDic.ContainsKey(l_allowedEntity.UserId) == false)
            m_userAllowedEntityDic[l_allowedEntity.UserId] = new MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity>();
          m_userAllowedEntityDic[l_allowedEntity.UserId].Set(l_allowedEntity.Id, l_allowedEntity.EntityId, l_allowedEntity);
        }
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(UserAllowedEntity));
        IsInit = true;
      }
      else
      {
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(UserAllowedEntity));
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer packet)
    {
      UserAllowedEntity l_allowedEntity = Build(packet) as UserAllowedEntity;

      if (m_userAllowedEntityDic.ContainsKey(l_allowedEntity.UserId) == false)
        m_userAllowedEntityDic[l_allowedEntity.UserId] = new MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity>();
      m_userAllowedEntityDic[l_allowedEntity.UserId].Set(l_allowedEntity.Id, l_allowedEntity.EntityId, l_allowedEntity);
      RaiseReadEvent(packet.GetError(), l_allowedEntity);
    }

    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 Id = packet.ReadUint32();

      foreach (MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> userEntityDic in m_userAllowedEntityDic.Values)
      {
        if (userEntityDic.ContainsKey(Id))
        {
          userEntityDic.RemovePrimary(Id);
          break;
        }
      }
      RaiseDeleteEvent(packet.GetError(), Id);
    }
    #endregion

    #region Mapping

    public override UserAllowedEntity GetValue(UInt32 p_id)
    {
      foreach (MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> userEntityDic in m_userAllowedEntityDic.Values)
        if (userEntityDic.ContainsKey(p_id))
          return (userEntityDic.PrimaryKeyItem(p_id));
      return (null);
    }

    public UserAllowedEntity GetValue(UInt32 p_userId, UInt32 p_entityId)
    {
      if (m_userAllowedEntityDic.ContainsKey(p_userId) == false)
        return (null);
      MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> userEntityDic = m_userAllowedEntityDic[p_userId];

      return (userEntityDic.SecondaryKeyItem(p_entityId));
    }

    public SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity>> GetDictionary()
    {
      return (m_userAllowedEntityDic);
    }

    public MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> GetDictionary(UInt32 p_userId)
    {
      if (m_userAllowedEntityDic.ContainsKey(p_userId) == false)
        return (null);
      return (m_userAllowedEntityDic[p_userId]);
    }

    #endregion
  }
}
