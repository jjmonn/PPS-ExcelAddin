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

  public partial class FBIFunctionUI : Form, IView
  {
    public FBIFunctionUI()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
