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

  class CommitModel : ICRUDModel<Commit>
  {
    static CommitModel s_instance = new CommitModel();
    public static CommitModel Instance { get { return (s_instance); } }

    SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, Commit>> m_commitDic = new SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, Commit>>();

    CommitModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_COMMIT;
      ReadCMSG = ClientMessage.CMSG_READ_COMMIT;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_COMMIT;
      ListCMSG = ClientMessage.CMSG_LIST_COMMIT;

      ReadSMSG = ServerMessage.SMSG_READ_COMMIT_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_COMMIT_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_COMMIT_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_COMMIT_ANSWER;
      CreateSMSG = ServerMessage.SMSG_CREATE_COMMIT_ANSWER;

      Build = Commit.BuildCommit;

      InitCallbacks();
    }

    #region Mapping

    public void UpdateCommitStatus(UInt32 p_entityId, UInt32 p_period, byte p_value)
    {
      Commit l_commit = GetValue(p_entityId, p_period);

      if (l_commit == null)
      {
        l_commit = new Commit();
        l_commit.Period = p_period;
        l_commit.EntityId = p_entityId;
        l_commit.Value = p_value;
        Create(l_commit);
      }
      else
      {
        l_commit.Value = p_value;
        Update(l_commit);
      }
    }

    public override Commit GetValue(UInt32 p_id)
    {
      foreach (MultiIndexDictionary<UInt32, UInt32, Commit> elem in m_commitDic.Values)
        if (elem.ContainsKey(p_id))
          return (elem.PrimaryKeyItem(p_id));
      return (null);
    }

    public Commit GetValue(UInt32 p_entityId, UInt32 p_period)
    {
      if (m_commitDic.ContainsKey(p_entityId) == false)
        return (null);
      return (m_commitDic[p_entityId].SecondaryKeyItem(p_period));
    }

    public SortedDictionary<UInt32, MultiIndexDictionary<UInt32, UInt32, Commit>> GetDictionary()
    {
      return (m_commitDic);
    }

    public MultiIndexDictionary<UInt32, UInt32, Commit> GetDictionary(UInt32 p_entityId)
    {
      if (m_commitDic.ContainsKey(p_entityId) == false)
        return (null);
      return (m_commitDic[p_entityId]);
    }

    #endregion

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        UInt32 count = p_packet.ReadUint32();
        for (Int32 i = 1; i <= count; i++)
        {
          Commit l_commit = Build(p_packet) as Commit;

          if (m_commitDic.ContainsKey(l_commit.EntityId) == false)
            m_commitDic[l_commit.EntityId] = new MultiIndexDictionary<UInt32, UInt32, Commit>();
          m_commitDic[l_commit.EntityId].Set(l_commit.Id, l_commit.Period, l_commit);
        }
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(Commit));
        IsInit = true;
      }
      else
      {
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(Commit));
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer p_packet)
    {
      Commit l_commit = Build(p_packet) as Commit;

      if (m_commitDic.ContainsKey(l_commit.EntityId) == false)
        m_commitDic[l_commit.EntityId] = new MultiIndexDictionary<UInt32, UInt32, Commit>();
      m_commitDic[l_commit.EntityId].Set(l_commit.Id, l_commit.Period, l_commit);
      RaiseReadEvent(p_packet.GetError(), l_commit);
    }

    protected override void DeleteAnswer(ByteBuffer p_packet)
    {
      UInt32 Id = p_packet.ReadUint32();

      foreach (MultiIndexDictionary<UInt32, UInt32, Commit> elem in m_commitDic.Values)
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

  }
}
