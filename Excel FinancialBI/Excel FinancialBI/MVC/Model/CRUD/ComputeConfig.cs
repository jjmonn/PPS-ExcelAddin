using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class ComputeConfig
  {
    public ComputeRequest Request { get; set; }
    public CUIDimensionConf Rows { get; set; }
    public CUIDimensionConf Columns { get; set; }
  }
}
