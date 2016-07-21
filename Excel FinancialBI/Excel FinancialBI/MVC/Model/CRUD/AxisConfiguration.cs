using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;
  
  public class AxisConfiguration : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public AxisType Axis { get { return (AxisType)Id; } }
    public AxisType AxisOwner { get; set; }
    public bool Owned { get; set; }
    public UInt32 Image { get; set; }

    public AxisConfiguration() { }
    private AxisConfiguration(UInt32 p_id)
    {
      Id = p_id;
    }

    public static AxisConfiguration BuildAxisConfiguration(ByteBuffer p_packet)
    {
      AxisConfiguration l_axisConfiguration = new AxisConfiguration(p_packet.ReadUint32());

      l_axisConfiguration.Owned = p_packet.ReadBool();
      l_axisConfiguration.AxisOwner = (AxisType)p_packet.ReadUint32();
      return (l_axisConfiguration);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteBool(Owned);
      p_packet.WriteUint32((UInt32)AxisOwner);
    }

    public void CopyFrom(AxisConfiguration p_model)
    {
    }

    public AxisConfiguration Clone()
    {
      AxisConfiguration l_clone = new AxisConfiguration(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisConfiguration l_cmpAxis = p_obj as AxisConfiguration;

      if (l_cmpAxis == null)
        return 0;
      if (l_cmpAxis.Axis > Axis)
        return -1;
      else
        return 1;
    }

  }
}