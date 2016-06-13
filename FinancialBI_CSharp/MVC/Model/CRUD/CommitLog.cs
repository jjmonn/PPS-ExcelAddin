using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class CommitLog : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public string User { get; set; }
    public UInt64 Date { get; set; }
    public UInt32 Period { get; set; }
    public UInt32 EntityId { get; set; }
    public Commit.Status State { get; set; }
    public UInt32 Image { get; set; }

    public CommitLog() { }

    public static CommitLog BuildCommitLog(ByteBuffer p_packet)
    {
      CommitLog l_commitLog = new CommitLog();

      l_commitLog.User = p_packet.ReadString();
      l_commitLog.Date = p_packet.ReadUint64();
      l_commitLog.EntityId = p_packet.ReadUint32();
      l_commitLog.Period = p_packet.ReadUint32();
      l_commitLog.State = (Commit.Status)p_packet.ReadUint8();

      return (l_commitLog);
    }

    [Obsolete("Not implemented", true)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
    }

    public void CopyFrom(CommitLog p_model)
    {
      User = p_model.User;
      Date = p_model.Date;
      EntityId = p_model.EntityId;
      Period = p_model.Period;
      State = p_model.State;
    }

    public CommitLog Clone()
    {
        CommitLog l_clone = new CommitLog();
        l_clone.CopyFrom(this);
        return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      CommitLog l_cmpCommitLog = p_obj as CommitLog;

      if (l_cmpCommitLog == null)
        return 0;
      if (l_cmpCommitLog.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
