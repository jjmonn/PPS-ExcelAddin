using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class BaseVersion : NamedHierarchyCRUDEntity
  {
    public UInt32 Id { get; protected set; }
    public UInt32 ParentId { get; set; }
    public bool IsFolder { get; set; }
    public UInt32 StartPeriod { get; set; }
    public UInt16 NbPeriod { get; set; }
    public UInt32 Image { get; set; }
    public string Name { get; set; }
    public Int32 ItemPosition { get; set; }

    protected BaseVersion() { }
  }
}
