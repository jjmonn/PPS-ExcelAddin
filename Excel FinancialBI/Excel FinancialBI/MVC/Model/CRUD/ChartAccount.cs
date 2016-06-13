using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class ChartAccount : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 ChartId { get; set; }
    public UInt32 AccountId { get; set; }
    public Int32 Color { get; set; }
    public UInt32 Image { get; set; }

    public ChartAccount() { }
    private ChartAccount(UInt32 p_id)
    {
      Id = p_id;
    }

    public static ChartAccount BuildChartAccount(ByteBuffer p_packet)
    {
      ChartAccount l_chartAcc = new ChartAccount(p_packet.ReadUint32());

      l_chartAcc.ChartId = p_packet.ReadUint32();
      l_chartAcc.AccountId = p_packet.ReadUint32();
      l_chartAcc.Color = p_packet.ReadInt32();

      return (l_chartAcc);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ChartId);
      p_packet.WriteUint32(AccountId);
      p_packet.WriteInt32(Color);
    }

    public void CopyFrom(ChartAccount p_model)
    {
      ChartId = p_model.ChartId;
      AccountId = p_model.AccountId;
      Color = p_model.Color;
    }

    public ChartAccount Clone()
    {
      ChartAccount l_clone = new ChartAccount(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      ChartAccount l_cmpChartAcc = p_obj as ChartAccount;

      if (l_cmpChartAcc == null)
        return 0;
      if (l_cmpChartAcc.ChartId > ChartId)
        return -1;
      else
        return 1;
    }
  }
}
