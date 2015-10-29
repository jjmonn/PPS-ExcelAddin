using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class FModelingAccount : NamedHierarchyCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public UInt32 Type { get; set; }
    public string FormatString { get; set; }
    public Int32 ItemPosition { get; set; }
    public UInt32 AccountId { get; set; }
    public UInt32 MappedEntity { get; set; }
    public UInt32 SerieColor { get; set; }
    public UInt32 SerieType { get; set; }
    public string SerieChart { get; set; }

    public FModelingAccount() { }
    private FModelingAccount(UInt32 p_id)
    {
      Id = p_id;
    }

    public static FModelingAccount BuildFModelingAccount(ByteBuffer p_packet)
    {
      FModelingAccount l_modelAccount = new FModelingAccount(p_packet.ReadUint32());

      l_modelAccount.ParentId = p_packet.ReadUint32();
      l_modelAccount.Name = p_packet.ReadString();
      l_modelAccount.Type = p_packet.ReadUint32();
      l_modelAccount.ItemPosition = p_packet.ReadInt32();
      l_modelAccount.AccountId = p_packet.ReadUint32();
      l_modelAccount.MappedEntity = p_packet.ReadUint32();
      l_modelAccount.SerieColor = p_packet.ReadUint32();
      l_modelAccount.SerieType = p_packet.ReadUint32();
      l_modelAccount.SerieChart = p_packet.ReadString();

      return (l_modelAccount);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteString(Name);
      p_packet.WriteUint32(Type);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteUint32(AccountId);
      p_packet.WriteUint32(MappedEntity);
      p_packet.WriteUint32(SerieColor);
      p_packet.WriteUint32(SerieType);
      p_packet.WriteString(SerieChart);
    }

    public void CopyFrom(FModelingAccount p_model)
    {
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      Type = p_model.Type;
      ItemPosition = p_model.ItemPosition;
      AccountId = p_model.AccountId;
      MappedEntity = p_model.MappedEntity;
      SerieColor = p_model.SerieColor;
      SerieType = p_model.SerieType;
      SerieChart = p_model.SerieChart;
    }

    public FModelingAccount Clone()
    {
      FModelingAccount l_clone = new FModelingAccount(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      FModelingAccount l_cmp = p_obj as FModelingAccount;

      if (l_cmp == null)
        return 0;
      if (l_cmp.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }
  }
}
