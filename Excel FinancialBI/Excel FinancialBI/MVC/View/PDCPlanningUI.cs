using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;

  public partial class PDCPlanningUI : Form, IView
  {
    public PDCPlanningUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("upload.title_pdc_planning_ui"); 
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
