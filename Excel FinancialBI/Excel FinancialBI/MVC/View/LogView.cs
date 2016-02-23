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

  public partial class LogView : Form, IView
  {
    public LogView()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("general.log");
      VLabel1.Text = Local.GetValue("general.entity");
      VLabel2.Text = Local.GetValue("general.account");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
