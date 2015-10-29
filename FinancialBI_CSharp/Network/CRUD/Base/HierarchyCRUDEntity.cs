using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public interface HierarchyCRUDEntity : CRUDEntity
  {
    UInt32 ParentId { get; set; }
  }
}
