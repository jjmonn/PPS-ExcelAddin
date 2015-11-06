using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public interface CRUDEntity
  {
    UInt32 Id { get; }
    UInt32 Image { get; set; }
    void Dump(ByteBuffer p_packet, bool p_includeId);
  }
}