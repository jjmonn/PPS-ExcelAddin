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

  public partial class SubmissionsControlUI : Form, IView
  {
    public SubmissionsControlUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      Label1.Text = Local.GetValue("general.version");
      Label2.Text = Local.GetValue("general.entity");
      Label3.Text = Local.GetValue("general.currency");
      this.Text = Local.GetValue("submissionsFollowUp.submissions_controls");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
