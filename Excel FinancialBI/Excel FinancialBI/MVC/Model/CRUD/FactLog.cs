using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class FactLog : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public string User { get; set; }
    public UInt64 Date { get; set; }
    public UInt32 ClientId { get; set; }
    public UInt32 ProductId { get; set; }
    public UInt32 AdjustmentId { get; set; }
    public UInt32 EmployeeId { get; set; }
    public double Value { get; set; }
    public UInt32 Image { get; set; }

    public FactLog() { }

    public static FactLog BuildFactLog(ByteBuffer p_packet)
    {
      FactLog l_factLog = new FactLog();

      l_factLog.User = p_packet.ReadString();
      l_factLog.Date = p_packet.ReadUint64();
      l_factLog.ClientId = p_packet.ReadUint32();
      l_factLog.ProductId = p_packet.ReadUint32();
      l_factLog.AdjustmentId = p_packet.ReadUint32();
      l_factLog.EmployeeId = p_packet.ReadUint32();
      l_factLog.Value = p_packet.ReadDouble();

      return (l_factLog);
    }

    [Obsolete("Not implemented", true)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
    }

    public void CopyFrom(FactLog p_model)
    {
      User = p_model.User;
      Date = p_model.Date;
      ClientId = p_model.ClientId;
      ProductId = p_model.ProductId;
      AdjustmentId = p_model.AdjustmentId;
      EmployeeId = p_model.EmployeeId;
      Value = p_model.Value;
    }

    public FactLog Clone()
    {
        FactLog l_clone = new FactLog();
        l_clone.CopyFrom(this);
        return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      FactLog l_cmpFactLog = p_obj as FactLog;

      if (l_cmpFactLog == null)
        return 0;
      if (l_cmpFactLog.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
