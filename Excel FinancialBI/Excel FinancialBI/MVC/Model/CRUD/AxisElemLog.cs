using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class AxisElemLog : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public string User { get; set; }
    public UInt64 Date { get; set; }
    public string Name { get; set; }
    public CRUDAction Operation { get; set; }
    public AxisType Axis { get; set; }
    public UInt32 Image { get; set; }

    public AxisElemLog() { }

    public static AxisElemLog BuildAxisElemLog(ByteBuffer p_packet)
    {
      AxisElemLog l_axisElemLog = new AxisElemLog();

      l_axisElemLog.User = p_packet.ReadString();
      l_axisElemLog.Date = p_packet.ReadUint64();
      l_axisElemLog.Axis = (AxisType)p_packet.ReadUint32();
      l_axisElemLog.Name = p_packet.ReadString();
      l_axisElemLog.Operation = (CRUDAction)p_packet.ReadUint8();

      return (l_axisElemLog);
    }

    [Obsolete("Not implemented", true)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
    }

    public void CopyFrom(AxisElemLog p_model)
    {
      User = p_model.User;
      Date = p_model.Date;
      Axis = p_model.Axis;
      Name = p_model.Name;
      Operation = p_model.Operation;
    }

    public AxisElemLog Clone()
    {
        AxisElemLog l_clone = new AxisElemLog();
        l_clone.CopyFrom(this);
        return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisElemLog l_cmpAxisElemLog = p_obj as AxisElemLog;

      if (l_cmpAxisElemLog == null)
        return 0;
      if (l_cmpAxisElemLog.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
