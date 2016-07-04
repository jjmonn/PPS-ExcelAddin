using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;

  class AxisConfigurationModel : SimpleCRUDModel<AxisConfiguration>
  {
    static AxisConfigurationModel s_instance = new AxisConfigurationModel();
    public static AxisConfigurationModel Instance { get { return (s_instance); } }

    AxisConfigurationModel()
    {
      ListSMSG = ServerMessage.SMSG_AXIS_CONFIGURATION_LIST;

      Build = AxisConfiguration.BuildAxisConfiguration;

      InitCallbacks();
    }
  }
}
