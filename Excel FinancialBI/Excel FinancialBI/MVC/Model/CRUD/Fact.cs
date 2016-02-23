using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class Fact : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 EntityId { get; set; }
    public UInt32 AccountId { get; set; }
    public UInt32 Period { get; set; }
    public UInt32 VersionId { get; set; }
    public UInt32 ClientId { get; set; }
    public UInt32 ProductId { get; set; }
    public UInt32 AdjustmentId { get; set; }
    public UInt32 EmployeeId { get; set; }
    public double Value { get; set; }
    public UInt32 Image { get; set; }

    public Fact() { }
    private Fact(UInt32 p_id)
    {
      Id = p_id;
    }

    public static Fact BuildFact(ByteBuffer p_packet)
    {
      Fact l_fact = new Fact(p_packet.ReadUint32());

      l_fact.EntityId = p_packet.ReadUint32();
      l_fact.AccountId = p_packet.ReadUint32();
      l_fact.Period = p_packet.ReadUint32();
      l_fact.VersionId = p_packet.ReadUint32();
      l_fact.ClientId = p_packet.ReadUint32();
      l_fact.ProductId = p_packet.ReadUint32();
      l_fact.AdjustmentId = p_packet.ReadUint32();
      l_fact.EmployeeId = p_packet.ReadUint32();
      l_fact.Value = p_packet.ReadDouble();

      return (l_fact);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(EntityId);
      p_packet.WriteUint32(AccountId);
      p_packet.WriteUint32(Period);
      p_packet.WriteUint32(VersionId);
      p_packet.WriteUint32(ClientId);
      p_packet.WriteUint32(ProductId);
      p_packet.WriteUint32(AdjustmentId);
      p_packet.WriteUint32(EmployeeId);
      p_packet.WriteDouble(Value);
    }

    public void CopyFrom(Fact p_model)
    {
      EntityId = p_model.EntityId;
      AccountId = p_model.AccountId;
      Period = p_model.Period;
      VersionId = p_model.VersionId;
      ClientId = p_model.ClientId;
      ProductId = p_model.ProductId;
      AdjustmentId = p_model.AdjustmentId;
      EmployeeId = p_model.EmployeeId;
      Value = p_model.Value;
    }

    public Fact Clone()
    {
        Fact l_clone = new Fact(Id);
        l_clone.CopyFrom(this);
        return (l_clone);
    }


    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Fact l_cmpFact = p_obj as Fact;

      if (l_cmpFact == null)
        return 0;
      if (l_cmpFact.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
