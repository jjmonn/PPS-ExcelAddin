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

  public partial class GenericView : Form, IView
  {
    public GenericView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {

    }

    public void LoadView()
    {

    }
  }
}
