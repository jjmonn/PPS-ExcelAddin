using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
 using Network;

  class LegalHoliday : CRUDEntity
  {
    public UInt32 Id { get; private set; }
    public UInt32 EmployeeId { get; set; }
    public UInt32 Period { get; set; }
    public UInt32 Image { get; set; }

    public LegalHoliday() { }
    private LegalHoliday(UInt32 p_id)
    {
      Id = p_id;
    }

    public static LegalHoliday BuildLegalHoliday(ByteBuffer p_packet)
    {
      LegalHoliday l_entity = new LegalHoliday(p_packet.ReadUint32());

      l_entity.EmployeeId = p_packet.ReadUint32();
      l_entity.Period = p_packet.ReadUint32();

      return (l_entity);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(EmployeeId);
      p_packet.WriteUint32(Period);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      LegalHoliday l_cmpEntity = p_obj as LegalHoliday;

      if (l_cmpEntity == null)
        return 0;
      if (l_cmpEntity.EmployeeId > EmployeeId)
        return -1;
      else
        return 1;
    }
  }
}
