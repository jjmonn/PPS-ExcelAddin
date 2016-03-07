using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class FactTag : CRUDEntity, IComparable
  {
    public enum TagType
    {
      NONE = 0,
      CP,
      RTT,
      Abs,
      ALD,
      CD,
      FF,
      RI,
      CT,
      FC,
      NF,
      IC,
      D,
      A,
      E,
      S,
      RD,
      RG,
      IT,
      SST
    }

    public UInt32 Id { get; set; }
    public TagType Tag { get; set; }
    public UInt32 Image { get; set; }

    public FactTag() { }
    private FactTag(UInt32 p_factId, TagType p_Tag)
    {
      Id = p_factId;
      Tag = p_Tag;
    }

    public static FactTag BuildFactTag(ByteBuffer p_packet)
    {
      FactTag l_fact = new FactTag(p_packet.ReadUint32(), (TagType)p_packet.ReadUint8());

      return (l_fact);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      p_packet.WriteUint32(Id);
      p_packet.WriteUint8((byte)Tag);
    }

    public FactTag Clone()
    {
      FactTag l_clone = new FactTag(Id, Tag);

      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      FactTag l_cmpfact = p_obj as FactTag;

      if (l_cmpfact == null)
        return 0;
      if (l_cmpfact.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
