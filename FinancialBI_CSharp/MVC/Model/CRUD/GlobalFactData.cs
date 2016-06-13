using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class GlobalFactData : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 GlobalFactId { get; set; }
    public UInt32 Period { get; set; }
    public UInt32 VersionId { get; set; }
    public double Value { get; set; }
    public UInt32 Image { get; set; }

    public GlobalFactData() { }
    private GlobalFactData(UInt32 p_id)
    {
      Id = p_id;
    }

    public static GlobalFactData BuildGlobalFact(ByteBuffer p_packet)
    {
      GlobalFactData l_globalFact = new GlobalFactData(p_packet.ReadUint32());

      l_globalFact.GlobalFactId = p_packet.ReadUint32();
      l_globalFact.Period = p_packet.ReadUint32();
      l_globalFact.VersionId = p_packet.ReadUint32();
      l_globalFact.Value = p_packet.ReadDouble();
      return (l_globalFact);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(GlobalFactId);
      p_packet.WriteUint32(Period);
      p_packet.WriteUint32(VersionId);
      p_packet.WriteDouble(Value);
    }

    public void CopyFrom(GlobalFactData p_model)
    {
      GlobalFactId = p_model.GlobalFactId;
      Period = p_model.Period;
      VersionId = p_model.VersionId;
      Value = p_model.Value;
    }

    public GlobalFactData Clone()
    {
      GlobalFactData l_clone = new GlobalFactData(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      GlobalFactData l_cmp = p_obj as GlobalFactData;

      if (l_cmp == null)
        return 0;
      if (l_cmp.GlobalFactId > GlobalFactId)
        return -1;
      else
        return 1;
    }
  }
}
