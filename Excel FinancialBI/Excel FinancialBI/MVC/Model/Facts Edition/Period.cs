using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.FactsEdition
{
  using Network;

  public class Period
  {
    public Period(UInt32 p_id)
    {
      this.Id = p_id;
    }

    public UInt32 Id { get; private set; }
    public string Name { get; set; }
    // format ?
    // time config ?
 
  }
}
