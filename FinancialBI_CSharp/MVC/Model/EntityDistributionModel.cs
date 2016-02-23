using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class EntityDistributionModel : ICRUDModel<EntityDistribution>
  {
    static EntityDistributionModel s_instance = new EntityDistributionModel();
    public static EntityDistributionModel Instance { get { return (s_instance); } }

    SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, EntityDistribution>> m_CRUDDic;

    EntityDistributionModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_ENTITY_DISTRIBUTION;
      ReadCMSG = ClientMessage.CMSG_READ_ENTITY_DISTRIBUTION;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_ENTITY_DISTRIBUTION;
      ListCMSG = ClientMessage.CMSG_LIST_ENTITY_DISTRIBUTION;

      ReadSMSG = ServerMessage.SMSG_READ_ENTITY_DISTRIBUTION_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_ENTITY_DISTRIBUTION_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_ENTITY_DISTRIBUTION_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_ENTITY_DISTRIBUTION_ANSWER;
      CreateSMSG = ServerMessage.SMSG_CREATE_ENTITY_DISTRIBUTION_ANSWER;

      Build = EntityDistribution.BuildEntityDistribution;

      InitCallbacks();
    }

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        UInt32 count = p_packet.ReadUint32();
        for (Int32 i = 1; i <= count; i++)
        {
          EntityDistribution l_commit = Build(p_packet) as EntityDistribution;

          if (m_CRUDDic.ContainsKey(l_commit.AccountId) == false)
            m_CRUDDic[l_commit.AccountId] = new MultiIndexDictionary<UInt32, UInt32, EntityDistribution>();
          m_CRUDDic[l_commit.AccountId].Set(l_commit.Id, l_commit.EntityId, l_commit);
        }
        RaiseObjectInitializedEvent();
        IsInit = true;
      }
      else
      {
        RaiseObjectInitializedEvent();
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer p_packet)
    {
      EntityDistribution l_commit = Build(p_packet) as EntityDistribution;

      if (m_CRUDDic.ContainsKey(l_commit.AccountId) == false)
        m_CRUDDic[l_commit.AccountId] = new MultiIndexDictionary<UInt32, UInt32, EntityDistribution>();
      m_CRUDDic[l_commit.AccountId].Set(l_commit.Id, l_commit.EntityId, l_commit);
      RaiseReadEvent(p_packet.GetError(), l_commit);
    }

    protected override void DeleteAnswer(ByteBuffer p_packet)
    {
      UInt32 Id = p_packet.ReadUint32();

      foreach (MultiIndexDictionary<UInt32, UInt32, EntityDistribution> elem in m_CRUDDic.Values)
      {
        if (elem.ContainsKey(Id))
        {
          elem.RemovePrimary(Id);
          break;
        }
      }
      RaiseDeleteEvent(p_packet.GetError(), Id);
    }

    #endregion

    #region Mapping

    public override CRUDEntity GetValue(UInt32 p_id)
    {
      foreach (MultiIndexDictionary<UInt32, UInt32, EntityDistribution> elem in m_CRUDDic.Values)
        if (elem.ContainsKey(p_id))
          return elem.PrimaryKeyItem(p_id);
      return null;
    }

    public EntityDistribution GetValue(UInt32 p_entityId, UInt32 p_accountId)
    {
      if (m_CRUDDic.ContainsKey(p_accountId) == false)
        return (null);
      return (m_CRUDDic[p_accountId].SecondaryKeyItem(p_entityId));
    }

    public SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, EntityDistribution>> GetDictionary()
    {
      return (m_CRUDDic);
    }

    public MultiIndexDictionary<UInt32, UInt32, EntityDistribution> GetDictionary(UInt32 p_accountId)
    {
      if (m_CRUDDic.ContainsKey(p_accountId) == false)
        return (null);
      return (m_CRUDDic[p_accountId]);
    }

    #endregion
  }
}
