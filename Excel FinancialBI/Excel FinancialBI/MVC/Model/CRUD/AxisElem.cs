using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public enum AxisType : byte
  {
    Entities = 1,
    Client,
    Product,
    Adjustment,
    Employee
  }

  public class AxisElem : NamedHierarchyCRUDEntity, AxedCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public Int32 ItemPosition { get; set; }
    public AxisType Axis { get; set; }
    public bool AllowEdition { get; set; }
    public UInt32 Image { get; set; }

    public AxisElem() { }
    private AxisElem(UInt32 p_id)
    {
      Id = p_id;
    }

    public static AxisElem BuildAxis(ByteBuffer p_packet)
    {
      AxisElem l_axisElem = new AxisElem(p_packet.ReadUint32());

      l_axisElem.ParentId = p_packet.ReadUint32();
      l_axisElem.Name = p_packet.ReadString();
      l_axisElem.Axis = (AxisType)p_packet.ReadUint32();
      l_axisElem.ItemPosition = p_packet.ReadInt32();
      l_axisElem.AllowEdition = p_packet.ReadBool();
      if (l_axisElem.AllowEdition)
        l_axisElem.Image = 1;
      else
        l_axisElem.Image = 0;

      return (l_axisElem);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteString(Name);
      p_packet.WriteUint32((UInt32)Axis);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteBool(AllowEdition);
    }

    public void CopyFrom(AxisElem p_model)
    {
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      Axis = p_model.Axis;
      ItemPosition = p_model.ItemPosition;
      AllowEdition = p_model.AllowEdition;
    }

    public AxisElem Clone()
    {
      AxisElem l_clone = new AxisElem(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisElem l_cmpAxis = p_obj as AxisElem;

      if (l_cmpAxis == null)
        return 0;
      if (l_cmpAxis.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }

    //public override bool Equals(object p_obj)
    //{
    //  if (p_obj.GetType() != this.GetType())
    //    return (false);
    //  AxisElem l_obj = p_obj as AxisElem;

    //  return (l_obj.Id == Id);
    //}

    //public override Int32 GetHashCode()
    //{
    //  return (Int32)Id;
    //}

  }
}