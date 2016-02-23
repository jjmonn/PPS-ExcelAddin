using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class GlobalFact : NamedCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public string Name { get; set; }
    public UInt32 Image { get; set; }

    public GlobalFact() { }
    private GlobalFact(UInt32 p_id)
    {
      Id = p_id;
    }

    public static GlobalFact BuildGlobalFact(ByteBuffer p_packet)
    {
      GlobalFact l_globalFact = new GlobalFact(p_packet.ReadUint32());

      l_globalFact.Name = p_packet.ReadString();
      return (l_globalFact);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteString(Name);
    }

    public void CopyFrom(GlobalFact p_model)
    {
      Name = p_model.Name;
    }

    public GlobalFact Clone()
    {
      GlobalFact l_clone = new GlobalFact(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      GlobalFact l_cmp = p_obj as GlobalFact;

      if (l_cmp == null)
        return 0;
      if (l_cmp.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
