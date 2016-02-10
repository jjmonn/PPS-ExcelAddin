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

  public partial class PBarUI : Form, IView
  {
    public PBarUI()
    {
      InitializeComponent();
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("upload.uploading");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
