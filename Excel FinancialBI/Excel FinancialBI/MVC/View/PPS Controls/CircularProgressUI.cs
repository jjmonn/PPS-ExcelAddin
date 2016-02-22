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
  using FBI;
  using Utils;

  public partial class CircularProgressUI : Form, IView
  {
    public CircularProgressUI()
    {
      InitializeComponent();
      LoadView();
    }

    public void LoadView()
    {
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      Label1.Text = Local.GetValue("general.initializing");
      Text = Local.GetValue("general.initializing");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
