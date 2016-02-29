using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class SourcedComputeRequest : AComputeRequest
  {
    public List<Fact> FactList { get; set; }
    public UInt32 VersionId { get; set; }
    public List<UInt32> EntityList { get; set; }

    public SourcedComputeRequest()
    {
      EntityList = new List<UInt32>();
      FactList = new List<Fact>();
    }

    public void Dump(ByteBuffer p_packet, UInt32 p_entityId)
    {
      base.Dump(p_packet, VersionId, p_entityId);
      p_packet.WriteInt32(FactList.Count);
      
      foreach (Fact l_fact in FactList)
      {
        p_packet.WriteUint32(l_fact.AccountId);
        p_packet.WriteUint32(l_fact.Period);
        p_packet.WriteDouble(l_fact.Value);
      }
    }
  }
}
