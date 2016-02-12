using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class DimensionElem : NamedHierarchyCRUDEntity
  {
    public DimensionElem(UInt32 p_id, string p_name, CuiDgvConf p_conf, bool p_deletable = true, bool p_draggable = true, UInt32 p_parentId = 0)
    {
      Id = p_id;
      Name = p_name;
      Conf = p_conf;
      ParentId = p_parentId;
      Deletable = p_deletable;
      Draggable = p_draggable;
    }
    public UInt32 Id { get; set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public CuiDgvConf Conf { get; set; }
    public UInt32 Image { get; set; }
    public bool Deletable { get; set; }
    public bool Draggable { get; set; }
    public void Dump(ByteBuffer p_packet, bool p_id) { }
  }
}
