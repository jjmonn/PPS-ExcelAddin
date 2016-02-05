using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  interface IPlatformManagementController
  {
    void Close();
    void AddControlToPanel(System.Windows.Forms.Panel p_panel, FBI.MVC.View.PlatformMGTGeneralUI p_platformMgtUI);
  
  }
}
