using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public interface NamedHierarchyCRUDEntity : NamedCRUDEntity
  {
    UInt32 ParentId { get; set; }
  }
}
