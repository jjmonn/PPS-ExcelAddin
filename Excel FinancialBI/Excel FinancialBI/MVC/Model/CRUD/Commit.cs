using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class Commit : CRUDEntity, IComparable
  {
    #region Enums

    public enum Status
    {
      NOT_EDITED = 0x00,
      EDITED = 0x01,
      COMMITTED = 0x02
    }
    #endregion

    public UInt32 Id { get; set; }
    public UInt32 Period { get; set; }
    public UInt32 EntityId { get; set; }
    public byte Value { get; set; }
    public UInt32 Image { get; set; }

    public Commit() { }
    private Commit(UInt32 p_submissionId, UInt32 p_period, UInt32 p_entityId, byte p_value)
    {
      Id = p_submissionId;
      Period = p_period;
      EntityId = p_entityId;
      Value = p_value;
    }

    public static Commit BuildCommit(ByteBuffer p_packet)
    {
      Commit l_submission = new Commit(p_packet.ReadUint32(),
                                               p_packet.ReadUint32(),
                                               p_packet.ReadUint32(),
                                               p_packet.ReadUint8());

      return (l_submission);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(Period);
      p_packet.WriteUint32(EntityId);
      p_packet.WriteUint8(Value);
    }

    public Commit Clone()
    {
      Commit l_clone = new Commit(Id, Period, EntityId, Value);

      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Commit l_cmpSubmission = p_obj as Commit;

      if (l_cmpSubmission == null)
        return 0;
      if (l_cmpSubmission.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
